﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIOAccount.Classes;
using Soc_Management_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Soc_Management_Web.Controllers
{
    public class DropdownController : Controller
    {
        MasterDropdownHelper mddhelper = new MasterDropdownHelper();
        // Save Term and condition data
        [HttpPost]
        public JsonResult GetJobddl()
        {
            try
            {
                int companyId = Convert.ToInt32(GetIntSession("CompanyId"));
                var data = mddhelper.GetDrop("Job",companyId);
                return Json(new { data });
            }
            catch (Exception)
            {

                return null;
            }  
            //var filteredJobs = from job in data
            //                   where job.TextName.ToUpper().StartsWith(Prefix.ToUpper())
            //                   select new DropdownMasterModel
            //                   {
            //                       Value = job.Value,
            //                       TextName = job.TextName
            //                   };

          
        }
        [HttpPost]
        public JsonResult GetEvent()
        {
            try
            {
            int companyId = Convert.ToInt32(GetIntSession("CompanyId"));
                var data = mddhelper.GetDrop("Event",companyId);
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
           
           

        }

        [HttpPost]
        public JsonResult GetCustomer()
        {
            try
            {
                var data = mddhelper.GetDrop("Customer");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }
        [HttpPost]
        public JsonResult GetVenue()
        {
            try
            {
              
            int companyId = Convert.ToInt32(GetIntSession("CompanyId"));
                var data = mddhelper.GetDrop("Venue",companyId);
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception)
            {

                return null;
            }


        }

        public JsonResult getNoasEditale(long no,string type)
        {
            try
            {

			int companyId = Convert.ToInt32(GetIntSession("CompanyId"));
                var data = mddhelper.getAutono(no, type,companyId);
                return Json(data.Select(x => new { value = x.Value }));
            }
            catch (Exception)
            {

                return null;
            }


        }

        [HttpPost]
        public JsonResult GetjobDetailsById(int Id)
        {
            try
            {
			int companyId = Convert.ToInt32(GetIntSession("CompanyId"));
                var data = mddhelper.GetJobDetails("",companyId);
                var filteredJobs = from job in data
                                   where job.jobid==Id
                                   select job;
                return Json(new { filteredJobs });
            }
            catch (Exception)
            {

                return null;
            }



        }

        [HttpPost]
        public JsonResult GetExtraItem()
        {
            try
            {
                var data = mddhelper.GetDrop("ExtraItem");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult GetTermsAndCOndition(int type)
        {
            try
            {
                var data = mddhelper.GetTermsAndCondition(type);
                return Json(data.Select(x => new { label = x.Value, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult GetTermsAndCOndition1(int type)
        {
            try
            {
                var data = mddhelper.GetTermsAndCondition(type);
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult GetStatus()
        {
            try
            {
                int companyId = Convert.ToInt32(GetIntSession("CompanyId"));
                var data = mddhelper.GetDrop("Status",companyId);
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }

        public JsonResult Cateegory()
        {
            try
            {
                var data = mddhelper.GetDrop("Category");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }
        public JsonResult GetPersonwithcategory(string Category,long Id)
        {
            try
            {
                var data = mddhelper.GetpersonbyCategory(Id,Category);
                return Json(data.Select(x => new { label = x.Text, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult GetInqSubMobFooter(int type)
        {
            try
            {
                var data = mddhelper.GetInqSubMobFootern(type);
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult GetOrdSubMobFooter(int type)
        {
            try
            {
                var data = mddhelper.GetOrdSubMobFootern(type);
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }
        }
        [HttpPost]
        public JsonResult InQueryTittle()
        {
            try
            {
                var data = mddhelper.GetDrop("InQueryTittle");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }
        [HttpPost]
        public JsonResult Refrence()
        {
            try
            {
                var data = mddhelper.GetDrop("Refrence");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }

        [HttpPost]
        public JsonResult InSubQueryTittle()
        {
            try
            {
                var data = mddhelper.GetDrop("InSubQueryTittle");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }

        [HttpPost]
        public JsonResult InqueryNo()
        {
            try
            {
                var data = mddhelper.GetDrop("InqueryNo");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }
        public JsonResult OrderNo()
        {
            try
            {
                var data = mddhelper.GetDrop("OrderNo");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }
        public JsonResult VPLocation()
        {
            try
            {
                var data = mddhelper.GetDrop("VPLocation");
                return Json(data.Select(x => new { label = x.TextName, value = x.Value }));
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }
        public JsonResult messagecontent(long id,string templatetype, string fors)
        {
            try
            {
                var data = mddhelper.getsmstemplate(id,templatetype,fors);
                return Json(data);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return Json(null);
            }



        }

        public PartialViewResult Showsmstemplateview(int customerid = 0,string types="",long Id=0)
        {


            List<SMSTemplate> SelectAll = new List<SMSTemplate>();
            try
            {
                SelectAll = mddhelper.Showsmstemplatedata(customerid, types,Id);
                
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return PartialView("_SmsTemplate", SelectAll);
        }
        public long GetIntSession(string key)
        {
            return HttpContext.Session.GetInt32(key).HasValue ? Convert.ToInt64(HttpContext.Session.GetInt32(key).Value) : 0;
        }
    }
}
