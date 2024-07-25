

using Microsoft.AspNetCore.Mvc.Rendering;
using PIOAccount.Models;
using Soc_Management_Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PIOAccount.Classes
{
    public class MasterDropdownHelper
    {
        public DbConnection ObjDBConnection = new DbConnection();
        public List<DropdownMasterModel> GetDrop(string type="",int CompanyId=0)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<DropdownMasterModel> lstjob = new List<DropdownMasterModel>();
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Type", type);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("Usp_GetDropdownData", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    DropdownMasterModel obj = new DropdownMasterModel();
                    obj.Value = Dtjob.Rows[item]["Value"].ToString();
                    obj.TextName = Dtjob.Rows[item]["TextName"].ToString();
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public List<DropdownMasterModel> getAutono(long no,string type, int CompanyId)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<DropdownMasterModel> lstjob = new List<DropdownMasterModel>();
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@No", no);
                sqlParameters[1] = new SqlParameter("@flag", type);
                sqlParameters[2] = new SqlParameter("@CmpVou", CompanyId);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("getVerifyAutogenenoverify", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    DropdownMasterModel obj = new DropdownMasterModel();
                    obj.Value = Dtjob.Rows[item]["cnt"].ToString();
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public List<SelectListItem> GetDropgen(string type = "", int CompanyId=0)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<SelectListItem> lstjob = new List<SelectListItem>();
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Type", type);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("Usp_GetDropdownData", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = Dtjob.Rows[item]["Value"].ToString();
                    obj.Text = Dtjob.Rows[item]["TextName"].ToString();
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<SelectListItem> GetpersonbyCategory(long Id,string category)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<SelectListItem> lstjob = new List<SelectListItem>();
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Category", category);
                sqlParameters[1] = new SqlParameter("@Id", Id);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("Usp_GetpersonbyCategory", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = Dtjob.Rows[item]["Value"].ToString();
                    obj.Text = Dtjob.Rows[item]["TextName"].ToString();
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<DropdownMasterModel> GetTermsAndCondition(int type)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<DropdownMasterModel> lstjob = new List<DropdownMasterModel>();
                SqlParameter[] sqlParameters = new SqlParameter[2];
                var typeS="";
                if(type == 1)
                {
                    typeS = "Inclusive";
                }
                else if(type == 2)
                {
                    typeS = "Exclusive";
                }
                else if(type == 3)
                {
                    typeS = "Terms & Condition";
                }
                else if(type == 4)
                {
                    typeS = "Accomodation";
                }
                else
                {
                    typeS = "Traveling";
                }
                sqlParameters[0] = new SqlParameter("@TncType", typeS);
                sqlParameters[1] = new SqlParameter("@FLG", "6");
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("Usp_TermsAndCond_Insert", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    DropdownMasterModel obj = new DropdownMasterModel();
                    obj.Value = Dtjob.Rows[item]["TncDesc"].ToString();
                    obj.TextName = Dtjob.Rows[item]["TncNm"].ToString();
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<DropdownMasterModel> GetInqSubMobFootern(int type)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<DropdownMasterModel> lstjob = new List<DropdownMasterModel>();
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@queryType", type);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("Get_Inq_Sub_Mob_Footer", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    DropdownMasterModel obj = new DropdownMasterModel();
                    obj.Value = Dtjob.Rows[item]["Result"].ToString();
                    obj.TextName = Dtjob.Rows[item]["Result"].ToString();
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<DropdownMasterModel> GetOrdSubMobFootern(int type)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<DropdownMasterModel> lstjob = new List<DropdownMasterModel>();
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@queryType", type);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("Get_Ord_Sub_Mob_Footer", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    DropdownMasterModel obj = new DropdownMasterModel();
                    obj.Value = Dtjob.Rows[item]["Result"].ToString();
                    obj.TextName = Dtjob.Rows[item]["Result"].ToString();
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<JobModel> GetJobDetails(string Id, int CompanyId)
        {
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<JobModel> lstjob = new List<JobModel>();
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Type", "Job");
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("Usp_GetDropdownData", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    JobModel obj = new JobModel();
                    obj.jobid = Convert.ToInt32(Dtjob.Rows[item]["Value"].ToString());
                    obj.jobworkname = Dtjob.Rows[item]["TextName"].ToString();
                    obj.rate = Convert.ToDecimal(Dtjob.Rows[item]["JobRt"].ToString());
                    lstjob.Add(obj);
                }
                return lstjob;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<SelectListItem> GetRecordingType()
        {
            try
            {

                    List<SelectListItem> staticOptions = new List<SelectListItem>
                    {
                    new SelectListItem { Value = "Video", Text = "Video" },
                    new SelectListItem { Value = "Photo", Text = "Photo" },
                    new SelectListItem { Value = "Photography", Text = "Photography" }
                    };
                return staticOptions;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public string getsmstemplate(long id,string tempalete ,string fors)
        {
            string d = "";
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                List<JobModel> lstjob = new List<JobModel>();
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@id", id);
                sqlParameters[1] = new SqlParameter("@tempalete", tempalete);
                sqlParameters[2] = new SqlParameter("@fors", fors);
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("rc_gettemplatedata", sqlParameters);
                 d = Dtjob.Rows[0]["sms"].ToString();
                return d;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return d;
        }

        public List<SelectListItem> GetReportType(string types)
        {
            List<SelectListItem> TypeList = new List<SelectListItem>();
            try
            {
                TypeList.Add(new SelectListItem
                {
                    Text = "Header",
                    Value = "1"
                });

                TypeList.Add(new SelectListItem
                {
                    Text = "NoHeader",
                    Value = "2"
                });
                TypeList.Add(new SelectListItem
                {
                    Text = "Letterpad",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TypeList;
        }

        public List<SMSTemplate> Showsmstemplatedata(long customerid,string types,long Id)
        {
            List<SMSTemplate> dta = new List<SMSTemplate>();
            try
            {
                DbConnection ObjDBConnection = new DbConnection();
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@cusid", customerid);
                sqlParameters[1] = new SqlParameter("@type", types);
                
                DataTable Dtjob = ObjDBConnection.CallStoreProcedure("prcgetsendtosms", sqlParameters);
                for (var item = 0; item < Dtjob.Rows.Count; item++)
                {
                    SMSTemplate obj = new SMSTemplate();
                    obj.sl = Convert.ToInt32(Dtjob.Rows[item]["sl"].ToString());
                    obj.customer = Dtjob.Rows[item]["cs"].ToString();
                    obj.category = Dtjob.Rows[item]["cat"].ToString();
                    obj.mobile = Dtjob.Rows[item]["mobile"].ToString();
                    obj.Id = Id;
                    obj.types = types;
                    dta.Add(obj);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dta;
        }

    }
}

