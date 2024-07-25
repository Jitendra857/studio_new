using iTextSharp.text.pdf;
using Microsoft.Extensions.Hosting;
using PIOAccount.Classes;
using Soc_Management_Web.Classes;
using Soc_Management_Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text;
using Microsoft.AspNetCore.Hosting;

namespace Soc_Management_Web.Common
{
    public class MyBackgroundMailservice : BackgroundService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MyBackgroundMailservice(IWebHostEnvironment webHost)
        {
            _hostingEnvironment = webHost;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;
                DateTime nextRunTime = now.Date.AddHours(11); // Next run time is today at 8 AM
                if (now >= nextRunTime)
                {
                    // If it's already past 8 AM today, schedule for tomorrow
                    nextRunTime = nextRunTime.AddDays(1);
                }

                TimeSpan timeUntilEvent = nextRunTime - now;
                await Task.Delay(timeUntilEvent, stoppingToken); // Delay until the next run time
                SendMail();
            }
            //while (!stoppingToken.IsCancellationRequested)
            //{

            //    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Wait for 60 minutes
            //    SendMail();
            //}
        }

        private void SendMail()
        {
            List<Schedulemail> schedulemails = new List<Schedulemail>();
            DbConnection ObjDBConnection = new DbConnection();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@flag", 1);
            DataTable DtEmp = ObjDBConnection.CallStoreProcedure("GeteventforemailToday", sqlParameters);
            if (DtEmp.Rows.Count > 0)
            {
                for (int i = 0; i < DtEmp.Rows.Count; i++)
                {
                    Schedulemail sm = new Schedulemail();
                    sm.sl =Convert.ToInt32(DtEmp.Rows[i]["sl"].ToString());
                    sm.person =DtEmp.Rows[i]["Person"].ToString();
                    sm.ordtittle = DtEmp.Rows[i]["OrdTitle"].ToString();
                    sm.party = DtEmp.Rows[i]["PartyName"].ToString();
                    sm.totime = DtEmp.Rows[i]["ToTime"].ToString();
                    sm.funadd = DtEmp.Rows[i]["FunAdd"].ToString();
                    sm.fromtime = DtEmp.Rows[i]["FromTime"].ToString();
                    sm.date = DtEmp.Rows[i]["FromDate"].ToString();
                    sm.cabname = DtEmp.Rows[i]["CatNm"].ToString();
                    schedulemails.Add(sm);
                }
                GeneratePdf(schedulemails);
            }
        }

        private void GeneratePdf(List<Schedulemail> schedulemails)
        {
            Document doc = new Document(PageSize.A4.Rotate());
            string filePath = "";
            ReportFileInfo fileInfo = new ReportFileInfo();


            using (MemoryStream memoryStream = new MemoryStream())
            {
               
                PdfWriter pdfWriter = PdfWriter.GetInstance(doc, memoryStream);

                // Add an event handler to add page numbers
                pdfWriter.PageEvent = new PageNumberEventHandler();

                doc.Open();

                PdfPTable tableLayout = new PdfPTable(7);

                tableLayout = Add_Content_To_PDF(tableLayout, schedulemails);
                tableLayout.HeaderRows = 7;
                doc.Add(tableLayout);
                doc.Close();
                string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Reports");
                string fileName = DateTime.Now.ToString("yyyyMMdd")+ "_SchedulerRegister" + ".pdf";

                filePath = Path.Combine(folderPath, fileName);
                fileInfo.FileName = fileName;
                fileInfo.FilePath = filePath;
                
                System.IO.File.WriteAllBytes(filePath, memoryStream.ToArray());
                string mail = "dholakiastudioahmedabad@gmail.com,kaivaldholakia@gmail.com," +
                    "dholakiaalpesh@gmail.com,avishrivastava2@gmail.com,joshijitu9587@gmail.com";
                string[] mail1 = mail.Split(",");
                foreach (var item in mail1)
                {
                    NotificationService.SendMail("Hi , Today Event Schedule , please " +
                    " <a href='http://studio.pioerp.com/Reports/" + fileInfo.FileName + "' target='_blank'>click here</a> to get event list", item, "Event Schedule List");
                }

            }                                                                       
        }          
        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, List<Schedulemail> sms)
        {

            List<InqueryReportModel> Inquery1 = new List<InqueryReportModel>();

           
            float[] headers = { 5,22, 23, 20, 10,10,10 };  //Header Widths
            tableLayout.SetWidths(headers);        //Set the pdf headers
            tableLayout.WidthPercentage = 100;       //Set the PDF File witdh percentage

           




            tableLayout.AddCell(new PdfPCell(new Phrase(System.DateTime.Now.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingTop = -4, PaddingBottom = 2, HorizontalAlignment = Element.ALIGN_LEFT });

            //tableLayout.AddCell(new PdfPCell(new Phrase(System.DateTime.Now.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 8, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingTop = -4, PaddingBottom = 2, HorizontalAlignment = Element.ALIGN_LEFT });

                tableLayout.AddCell(new PdfPCell(new Phrase("Scheduler Register", new Font(Font.FontFamily.HELVETICA, 15, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Black)))) { Colspan = 7, Border = 0, PaddingBottom = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                

                tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.FontFamily.HELVETICA, 9, 0, new iTextSharp.text.BaseColor(System.Drawing.Color.Maroon)))) { Colspan = 7, Border = 0, BorderWidthBottom = 1, PaddingBottom = 3, HorizontalAlignment = Element.ALIGN_CENTER });



            AddCellToBodyleft0borderhead(tableLayout, "Sl No");
            AddCellToBodyleft0borderhead(tableLayout, "Party Name");
            AddCellToBodyleft0borderhead(tableLayout, "Function & Address");
            AddCellToBodyleft0borderhead(tableLayout, "Event Tittle");
            AddCellToBodyleft0borderhead(tableLayout, "Time");
            AddCellToBodyleft0borderhead(tableLayout, "Category");
            AddCellToBodyleft0borderhead(tableLayout, "Person");

            string party = "";
            //var events =Inquery.
            foreach (var item in sms)
            {
              
                if (party != item.party && party!="")
                {
                    party = item.party;
                    AddCellToBodyright0border(tableLayout, "");
                    AddCellToBodyright0border(tableLayout, "");
                    AddCellToBodyright0border(tableLayout, "");
                    AddCellToBodyright0border(tableLayout, "");
                    AddCellToBodyright0border(tableLayout, "");
                    AddCellToBodyright0border(tableLayout, "");
                    AddCellToBodyright0border(tableLayout, "");
                }
                else
                {
                    
                      party = item.party;
                    AddCellToBodyleft0border(tableLayout, item.sl.ToString());
                    AddCellToBodyleft0border1(tableLayout, item.party);
                    AddCellToBodyleft0border(tableLayout, item.funadd);
                    AddCellToBodyleft0border(tableLayout, item.ordtittle);
                    AddCellToBodyleft0border(tableLayout, item.fromtime + "-" + item.totime);
                    AddCellToBodyleft0border(tableLayout, item.cabname);
                    AddCellToBodyleft0border(tableLayout, item.person);
                }

                


            }
          
            return tableLayout;
        }
        private static void AddCellToBodyleft0border1(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                BorderWidthRight = 1
              ,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                Padding = 2,
                BackgroundColor = iTextSharp.text.BaseColor.WHITE
            });
        }
        private static void AddCellToBodyleft0border(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 0, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT,
                BorderWidthRight = 1
              ,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
        }
        private static void AddCellToBodyright0border(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 0, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT,
                Border=0,BorderWidthBottom=1,
                BackgroundColor = iTextSharp.text.BaseColor.WHITE });
        }
        private static void AddCellToBodyleft0borderhead(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT,
                BorderWidthRight = 1
              ,
                BorderWidthBottom = 1,
                BorderWidthTop = 0,
                BorderWidthLeft = 0, PaddingBottom = 0, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE });
        }
        private static void AddCellToBodyright0borderhead(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 9, 1, iTextSharp.text.BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_LEFT,
                BorderWidthRight = 0
              , BorderWidthBottom = 0,
                BorderWidthTop = 0  ,
              BorderWidthLeft = 1
              , PaddingBottom = 0, Padding = 2, BackgroundColor = iTextSharp.text.BaseColor.WHITE }) ;
        }

    }
}
