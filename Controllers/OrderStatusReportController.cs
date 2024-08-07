﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PIOAccount.Classes;
using PIOAccount.Controllers;
using PIOAccount.Models;
using Soc_Management_Web.Classes;
using Soc_Management_Web.Common;
using Soc_Management_Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Soc_Management_Web.Controllers
{
    public class OrderStatusReportController : BaseController
    {
        DbConnection ObjDBConnection = new DbConnection();
        ProductHelpers objProductHelper = new ProductHelpers();
        private readonly IWebHostEnvironment _hostingEnvironment;
        MasterDropdownHelper master = new MasterDropdownHelper();
        public OrderStatusReportController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(long id)
        {
            ReportmodelParameter model = new ReportmodelParameter();
            bool isreturn = false;
            INIT(ref isreturn);
            if (isreturn)
            {
                return RedirectToAction("index", "dashboard");
            }
            long userId = GetIntSession("UserId");
            long societyid = id;
            int companyId = Convert.ToInt32(GetIntSession("CompanyId"));
            int administrator = Convert.ToInt32(GetIntSession("IsAdministrator"));
            int clientId = 0;


            model.lstperson = objProductHelper.GetCustomerList(companyId);
            model.lstcusomer = objProductHelper.GetCustomerListOnly(companyId);
            model.lslCategory = objProductHelper.GetCategorylist(companyId);
            model.lstpendingsatus = objProductHelper.Pending();
            model.lstsortorder = objProductHelper.OrderBySchedul();
            model.lsteventstittle = objProductHelper.GetEventLst(companyId);
            model.inqstatuslst = objProductHelper.GetInqStatusList();
            model.lstPending = objProductHelper.Pending();
            model.lstjob = master.GetDropgen("Job", companyId);
            model.lststatus = master.GetDropgen("Status", companyId);
            model.lstoccassiontittle = master.GetDropgen("OrdTiitle", companyId);
            model.lstjob = master.GetDropgen("Job", companyId);
            model.lstlayout = master.GetReportType("Schedulle");


            return View(model);
        }

        private void INIT(ref bool isReturn)
        {
            #region User Rights
            long userId = GetIntSession("UserId");
            UserFormRightModel userFormRights = new UserFormRightModel();
            string currentURL = GetCurrentURL();
            userFormRights = GetUserRights(userId, currentURL);
            if (userFormRights == null)
            {
                SetErrorMessage("You do not have right to access requested page. Please contact admin for more detail.");
                isReturn = true;
            }
            ViewBag.userRight = userFormRights;

            #endregion

            #region Dynamic Report

            if (userFormRights != null)
            {
                ViewBag.layoutList = GetGridLayoutDropDown(DbConnection.GridTypeView, userFormRights.ModuleId);
                ViewBag.pageNoList = GetPageNo();
            }

            #endregion
        }


        public PartialViewResult OrderStatusReport(ReportmodelParameterpost reportmodel)
        {

            Document doc = new Document();
            string filePath = "";
            ReportFileInfo fileInfo = new ReportFileInfo();
            if (reportmodel.layout == "" || reportmodel.layout == null || reportmodel.layout == "Select")
            {
                reportmodel.layout = "Header";
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                List<InquerystatusReportModel> Inquery = getReportcontent(reportmodel);

                if (Inquery.Count > 0)
                {
                    // Associate the Document with the MemoryStream
                    PdfWriter pdfWriter = PdfWriter.GetInstance(doc, memoryStream);

                    // Add an event handler to add page numbers
                    pdfWriter.PageEvent = new PageNumberEventHandler();
                    if (reportmodel.layout == "Letterpad")
                    {
                        MyPageEventHandler pageEventHandler = new MyPageEventHandler();
                        pdfWriter.PageEvent = pageEventHandler;
                        reportmodel.layout = "Header";
                    }
                    doc.Open();

                    PdfPTable tableLayout = new PdfPTable(7);

                    tableLayout = Add_Content_To_PDF(tableLayout, Inquery, reportmodel.layout, reportmodel);
                    tableLayout.HeaderRows = 7;
                    doc.Add(tableLayout);

                    // Close the Document to finish the PDF creation
                    doc.Close();

                    // Write the content of the MemoryStream to a file
                    string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Reports");
                    string fileName = Inquery[0].Reportitle.ToString() + ".pdf";

                    filePath = Path.Combine(folderPath, fileName);
                    fileInfo.FileName = fileName;
                    fileInfo.FilePath = filePath;
                    fileInfo.Id = 0;
                    fileInfo.Title = Inquery[0].InqTitle;
                    System.IO.File.WriteAllBytes(filePath, memoryStream.ToArray());
                }
                else
                {
                    fileInfo.Title = "Data not found";

                }
            }

            TempData["fileInfo"] = fileInfo.FileName;

            return PartialView("_report", fileInfo);
        }
        public JsonResult OrderStatusexcel(ReportmodelParameterpost rm)
        {
            List<InquerystatusReportModel> Inquery = new List<InquerystatusReportModel>();

            var sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@OccfromDate", rm.occasionfromDate);
            sqlParameters[1] = new SqlParameter("@OccToDate", rm.occasiontoDate);
            sqlParameters[2] = new SqlParameter("@OrdfromNo", rm.orderFromNo);
            sqlParameters[3] = new SqlParameter("@OrdToNo", rm.orderToNo);
            sqlParameters[4] = new SqlParameter("@Customer", rm.customer);
            sqlParameters[5] = new SqlParameter("@EventTitle", rm.eventTittle);
            sqlParameters[6] = new SqlParameter("@OCCTittle", rm.occasionTitle);
            sqlParameters[7] = new SqlParameter("@Job", rm.job);
            sqlParameters[8] = new SqlParameter("@JobStatus", rm.orderStatus);
            sqlParameters[9] = new SqlParameter("@orderby", rm.sortorder);
            DataTable dt = ObjDBConnection.CallStoreProcedure("GetOrderRegisterReport", sqlParameters);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow item in dt.Rows)
                {
                    InquerystatusReportModel inq = new InquerystatusReportModel();

                    inq.Sl = Convert.ToInt32(item["Sl"].ToString());
                    inq.Id = Convert.ToInt32(item["Id"].ToString());
                    inq.InqDate = item["InqDate"].ToString();
                    inq.Customer = item["AccNm"].ToString();
                    inq.InqTitle = item["InqTitle"].ToString();
                    inq.CompanyTitle = item["companyName"].ToString();
                    inq.companyAddres = item["Addrescompany"].ToString();
                    inq.Reportitle = item["REPORTTittle"].ToString();
                    inq.Statuss = item["Statuss"].ToString();
                    inq.Amount = Convert.ToDecimal(item["NetAmount"].ToString());
                    Inquery.Add(inq);
                }
            }


            return Json(new { success = true, result = Inquery });
        }

        private List<InquerystatusReportModel> getReportcontent(ReportmodelParameterpost rm)
        {
            List<InquerystatusReportModel> Inquery = new List<InquerystatusReportModel>();

            var sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@OccfromDate", rm.occasionfromDate);
            sqlParameters[1] = new SqlParameter("@OccToDate", rm.occasiontoDate);
            sqlParameters[2] = new SqlParameter("@OrdfromNo", rm.orderFromNo);
            sqlParameters[3] = new SqlParameter("@OrdToNo", rm.orderToNo);
            sqlParameters[4] = new SqlParameter("@Customer", rm.customer);
            sqlParameters[5] = new SqlParameter("@EventTitle", rm.eventTittle);
            sqlParameters[6] = new SqlParameter("@OCCTittle", rm.occasionTitle);
            sqlParameters[7] = new SqlParameter("@Job", rm.job);
            sqlParameters[8] = new SqlParameter("@JobStatus", rm.orderStatus);
            sqlParameters[9] = new SqlParameter("@orderby", rm.sortorder);
            DataTable dt = ObjDBConnection.CallStoreProcedure("GetOrderRegisterReport", sqlParameters);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow item in dt.Rows)
                {
                    InquerystatusReportModel inq = new InquerystatusReportModel();

                    inq.Sl = Convert.ToInt32(item["Sl"].ToString());
                    inq.Id = Convert.ToInt32(item["Id"].ToString());
                    inq.InqDate = item["InqDate"].ToString();
                    inq.Customer = item["AccNm"].ToString();
                    inq.InqTitle = item["InqTitle"].ToString();
                    inq.CompanyTitle = item["companyName"].ToString();
                    inq.companyAddres = item["Addrescompany"].ToString();
                    inq.Reportitle = item["REPORTTittle"].ToString();
                    inq.Statuss = item["Statuss"].ToString();
                    inq.Amount = Convert.ToDecimal(item["NetAmount"].ToString());
                    Inquery.Add(inq);
                }
            }



            return Inquery;
        }



        // for pdf report





        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, List<InquerystatusReportModel> Inquery, string layout, ReportmodelParameterpost reportmodel)
        {

            List<InqueryReportModel> Inquery1 = new List<InqueryReportModel>();


            float[] headers = { 8, 8, 10, 26, 26, 10, 12 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage
            tableLayout.AddCell(new PdfPCell(new Phrase(" Date : " + System.DateTime.Now.ToString("dd/MM/yyy"), new Font(Font.FontFamily.HELVETICA, 7, 1, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingTop = -20, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_LEFT });

            if (layout.ToString() != "Header")
            {

                tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.FontFamily.HELVETICA, 10, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingBottom = 35, HorizontalAlignment = Element.ALIGN_CENTER });
                tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.FontFamily.HELVETICA, 10, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingBottom = 10, HorizontalAlignment = Element.ALIGN_CENTER });
                tableLayout.AddCell(new PdfPCell(new Phrase("  ", new Font(Font.FontFamily.HELVETICA, 10, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });

            }
            //Add Title to the PDF file at the top
            if (layout.ToString() == "Header")
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(Inquery[0].CompanyTitle, new Font(Font.FontFamily.HELVETICA, 17, 1, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingTop = -4, PaddingBottom = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                tableLayout.AddCell(new PdfPCell(new Phrase(Inquery[0].companyAddres, new Font(Font.FontFamily.HELVETICA, 10, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingBottom = 2, HorizontalAlignment = Element.ALIGN_CENTER });


                tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 22, 1, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, BorderWidthBottom = 0, PaddingBottom = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 22, 1, new iTextSharp.text.BaseColor(System.Drawing.Color.Maroon)))) { Colspan = 7, Border = 0, BorderWidthBottom = 0, PaddingBottom = 3, HorizontalAlignment = Element.ALIGN_CENTER });
            }
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 2, Border = 0, PaddingBottom = 4, PaddingTop = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            tableLayout.AddCell(new PdfPCell(new Phrase(Inquery[0].Reportitle, new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 3, Border = 0, BorderWidthBottom = 1, PaddingTop = 2, PaddingBottom = 4, HorizontalAlignment = Element.ALIGN_CENTER });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 12, 1, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 2, Border = 0, PaddingBottom = 4, PaddingTop = 2, HorizontalAlignment = Element.ALIGN_CENTER });

            // Customer details
            tableLayout.AddCell(new PdfPCell(new Phrase("Date : " + reportmodel.occasionfromDate + " To " + reportmodel.occasiontoDate, new Font(Font.FontFamily.HELVETICA, 9, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingTop = 3, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT });

            AddCellToBodyDottedBottomBorder(tableLayout, "");
            AddCellToBodyDottedBottomBorder(tableLayout, "");
            AddCellToBodyDottedBottomBorder(tableLayout, "");
            AddCellToBodyDottedBottomBorder(tableLayout, "");
            AddCellToBodyDottedBottomBorder(tableLayout, "");
            AddCellToBodyDottedBottomBorder(tableLayout, "");
            AddCellToBodyDottedBottomBorder(tableLayout, "");



            AddCellToBodyBold(tableLayout, "Sr No");
            AddCellToBodyBold(tableLayout, "Ord No");
            AddCellToBodyBold(tableLayout, "Ord Date");
            AddCellToBodyBold(tableLayout, "Party Name");
            AddCellToBodyBold(tableLayout, "Event Title");
            AddCellToBodyBold(tableLayout, "Amount");
            AddCellToBodyBold(tableLayout, "Status");
            //var events =Inquery.
            foreach (var item in Inquery)
            {


                AddCellToBody(tableLayout, item.Sl.ToString());
                AddCellToBody(tableLayout, item.Id.ToString());
                AddCellToBody(tableLayout, item.InqDate);
                AddCellToBody(tableLayout, item.Customer);
                AddCellToBody(tableLayout, item.InqTitle);
                AddCellToBody(tableLayout, item.Amount.ToString());
                AddCellToBody(tableLayout, item.Statuss);




            }



            return tableLayout;
        }

        // Method to add single cell to the header
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 10, 1, iTextSharp.text.BaseColor.WHITE))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(0, 51, 102) });
        }


        private static void AddCellToBodyBold(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 1, PaddingBottom = 1, Padding = 4, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
        }
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 0, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 1, PaddingBottom = 1, Padding = 3, BackgroundColor = iTextSharp.text.BaseColor.WHITE }); ;
        }
        private static void AddCellToBodyleft(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 6, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0, BorderWidthRight = 0, PaddingBottom = 1, Padding = 3, BackgroundColor = iTextSharp.text.BaseColor.WHITE }); ;
        }

        private static void AddCellToBody2(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT, Border = 0, Colspan = 2, PaddingBottom = 0, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
        }

        private static void AddCellToBodyBotoomborder(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                BorderWidthBottom = 1,
                PaddingBottom = 1,
                Padding = 2,
                BackgroundColor = iTextSharp.text.BaseColor.WHITE
            });
        }

        private static void AddCellToBodyDottedBottomBorder(PdfPTable tableLayout, string cellText)
        {
            PdfPCell cell = new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = Rectangle.BOTTOM_BORDER,
                BorderWidthBottom = -1f, // Set a negative value to create a dotted line
                PaddingBottom = 1,
                Padding = 2,
                BackgroundColor = iTextSharp.text.BaseColor.WHITE
            };

            tableLayout.AddCell(cell);
        }
    }
}
