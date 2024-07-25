using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PIOAccount.Models;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PIOAccount.Classes
{
    public class ProductHelpers
    {

        public DbConnection ObjDBConnection = new DbConnection();

        public List<SelectListItem> GetProductMasterDropdown(int companyId)
        {
            List<SelectListItem> productList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@PrdVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtProductList = ObjDBConnection.CallStoreProcedure("GetProductDetails", sqlParameters);
                if (DtProductList != null && DtProductList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProductList.Rows.Count; i++)
                    {
                        SelectListItem productItem = new SelectListItem();
                        productItem.Text = DtProductList.Rows[i]["PrdName"].ToString();
                        productItem.Value = DtProductList.Rows[i]["PrdVou"].ToString();
                        productList.Add(productItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productList;
        }

        public List<SelectListItem> GetPrdTypeWiseProductDropdown(int companyId, string PrdType)
        {
            List<SelectListItem> productList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@PrdVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                sqlParameters[5] = new SqlParameter("@PrdType", PrdType);
                DataTable DtProductList = ObjDBConnection.CallStoreProcedure("GetPrdTypeProductDetails", sqlParameters);
                if (DtProductList != null && DtProductList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProductList.Rows.Count; i++)
                    {
                        SelectListItem productItem = new SelectListItem();
                        productItem.Text = DtProductList.Rows[i]["PrdNm"].ToString();
                        productItem.Value = DtProductList.Rows[i]["PrdVou"].ToString();
                        productList.Add(productItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productList;
        }


        public List<SelectListItem> GetMasterMenuDropdown(int companyId)
        {
            List<SelectListItem> MenuList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];
                DataTable DtMenuList = ObjDBConnection.CallStoreProcedure("GetMasterMenuList", sqlParameters);
                if (DtMenuList != null && DtMenuList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtMenuList.Rows.Count; i++)
                    {
                        SelectListItem menuItem = new SelectListItem();
                        menuItem.Text = DtMenuList.Rows[i]["Name"].ToString();
                        menuItem.Value = DtMenuList.Rows[i]["ModuleID"].ToString();
                        MenuList.Add(menuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MenuList;
        }

        public List<CustomDropDown> GetProductCustomDropdown(int companyId)
        {
            List<CustomDropDown> productList = new List<CustomDropDown>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@PrdVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtProductList = ObjDBConnection.CallStoreProcedure("GetProductDetails", sqlParameters);
                if (DtProductList != null && DtProductList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProductList.Rows.Count; i++)
                    {
                        CustomDropDown productItem = new CustomDropDown();
                        productItem.Text = DtProductList.Rows[i]["PrdName"].ToString() + " - " + DtProductList.Rows[i]["PrdCode"].ToString();
                        productItem.Value = DtProductList.Rows[i]["PrdVou"].ToString();
                        productList.Add(productItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productList;
        }

        public List<SelectListItem> GetProductYesNo()
        {
            List<SelectListItem> productList = new List<SelectListItem>();
            try
            {
                productList.Add(new SelectListItem
                {
                    Text = "Yes",
                    Value = "0"
                });

                productList.Add(new SelectListItem
                {
                    Text = "No",
                    Value = "1"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productList;
        }

        public List<SelectListItem> GetCoilType()
        {
            List<SelectListItem> coiltypeList = new List<SelectListItem>();
            try
            {
                coiltypeList.Add(new SelectListItem
                {
                    Text = "Coil",
                    Value = "1"
                });

                coiltypeList.Add(new SelectListItem
                {
                    Text = "Pipe",
                    Value = "2"
                });
                coiltypeList.Add(new SelectListItem
                {
                    Text = "Other",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return coiltypeList;
        }
        public List<SelectListItem> GetlstProdDropdown(int CompanyId)
        {
            List<SelectListItem> ProdList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@CmpVou", CompanyId);

                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("usp_getProdList", sqlParameters);
                if (DtProdList != null && DtProdList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProdList.Rows.Count; i++)
                    {
                        SelectListItem RefbyItem = new SelectListItem();
                        RefbyItem.Text = DtProdList.Rows[i]["ProdName"].ToString();
                        RefbyItem.Value = DtProdList.Rows[i]["ProdVou"].ToString();
                        ProdList.Add(RefbyItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ProdList;
        }

        public List<SelectListItem> GetInqStatusList()
        {
            List<SelectListItem> StsList = new List<SelectListItem>();
            try
            {
                StsList.Add(new SelectListItem
                {
                    Text = "In Progress",
                    Value = "1"
                });

                StsList.Add(new SelectListItem
                {
                    Text = "Confirm",
                    Value = "2"
                });

                StsList.Add(new SelectListItem
                {
                    Text = "Cancel",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StsList;
        }
        public int GetLstInqNo( int flag, int CompanyId=0)
        {
            int InquiryNo;
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@FLG", flag);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("usp_Inquiry_detail", sqlParameters);
                InquiryNo = Convert.ToInt32(DtProdList.Rows[0]["SlNo"].ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return InquiryNo;
        }
        public int GetLstInqNo()
        {
            int InquiryNo;
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@FLG", 2);
                //sqlParameters[1] = new SqlParameter("@CmpVou", CompnayId);
                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("usp_Inquiry_detail", sqlParameters);
                InquiryNo = Convert.ToInt32(DtProdList.Rows[0]["InqNo"].ToString());
                  
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return InquiryNo;
        }
        public int GetSlNo(int a, int CompnayId)
        {
            int SlNO;
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@FLG", 3);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompnayId);
                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("usp_Inquiry_detail", sqlParameters);
                SlNO = Convert.ToInt32(DtProdList.Rows[0]["SlNo"].ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SlNO;
        }
        public List<SelectListItem> GetCustomerList(int CompanyId)
        {
            List<SelectListItem> CustList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Flg", 1);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("usp_Inquiry_detail", sqlParameters);
                if (DtProdList != null && DtProdList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProdList.Rows.Count; i++)
                    {
                        SelectListItem CustItem = new SelectListItem();
                        CustItem.Value = DtProdList.Rows[i]["AccVou"].ToString();
                        CustItem.Text = DtProdList.Rows[i]["AccNm"].ToString() ;
                        CustList.Add(CustItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CustList;
        }

        public DataTable DSGetCategoryList(int pid, int flag)
        {
            DataTable DtProdList = new DataTable();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Flg", flag);
                sqlParameters[1] = new SqlParameter("@parentID", pid);

               DtProdList = ObjDBConnection.CallStoreProcedure("usp_webCatMaster_list", sqlParameters);
                if (DtProdList != null && DtProdList.Rows.Count > 0)
                {
                    return DtProdList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DtProdList;
        }
        public List<SelectListItem> GetCategoryList(long pid,int flag)
        {
            List<SelectListItem> CatList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Flg", flag);
                sqlParameters[1] = new SqlParameter("@parentID", pid);

                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("usp_webCatMaster_list", sqlParameters);
                if (DtProdList != null && DtProdList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProdList.Rows.Count; i++)
                    {
                        SelectListItem CatItem = new SelectListItem();
                        CatItem.Value = DtProdList.Rows[i]["ID"].ToString();
                        CatItem.Text = DtProdList.Rows[i]["CatName"].ToString();
                        CatList.Add(CatItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CatList;
        }
        public List<SelectListItem> GetCustomerListOnly(int CompanyId=0)
        {
            List<SelectListItem> CustList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Flg", 1);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("sp_getonlycustomer", sqlParameters);
                if (DtProdList != null && DtProdList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProdList.Rows.Count; i++)
                    {
                        SelectListItem CustItem = new SelectListItem();
                        CustItem.Value = DtProdList.Rows[i]["AccVou"].ToString();
                        CustItem.Text = DtProdList.Rows[i]["AccNm"].ToString();
                        CustList.Add(CustItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CustList;
        }
        public List<SelectListItem> GetVideoTypeList()
        {
            List<SelectListItem> shifList = new List<SelectListItem>();
            try
            {
                shifList.Add(new SelectListItem
                {
                    Text = "Video + Photos",
                    Value = "1"
                });

                shifList.Add(new SelectListItem
                {
                    Text = "Video",
                    Value = "2"
                });

                shifList.Add(new SelectListItem
                {
                    Text = "Photos",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return shifList;
        }

        public List<SelectListItem> Pending()
        {
            List<SelectListItem> Pending = new List<SelectListItem>();
            try
            {
                Pending.Add(new SelectListItem
                {
                    Text = "ALL",
                    Value = "1"
                });
                Pending.Add(new SelectListItem
                {
                    Text = "Yes",
                    Value = "2"
                });

                Pending.Add(new SelectListItem
                {
                    Text = "No",
                    Value = "3"
                });

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Pending;
        }
        public List<SelectListItem> UserType()
        {
            List<SelectListItem> ut = new List<SelectListItem>();
            try
            {
                ut.Add(new SelectListItem
                {
                    Text = "Admin",
                    Value = "1"
                });

                ut.Add(new SelectListItem
                {
                    Text = "Photographer",
                    Value = "2"
                });
                ut.Add(new SelectListItem
                {
                    Text = "Customer",
                    Value = "3"
                });
                ut.Add(new SelectListItem
                {
                    Text = "Developer",
                    Value = "4"
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ut;
        }
        public List<SelectListItem> OrderBySchedul()
        {
            List<SelectListItem> ord = new List<SelectListItem>();
            try
            {
                ord.Add(new SelectListItem
                {
                    Text = "Date",
                    Value = "1"
                });

                ord.Add(new SelectListItem
                {
                    Text = "Category",
                    Value = "2"
                });
                ord.Add(new SelectListItem
                {
                    Text = "Customer",
                    Value = "2"
                });
                ord.Add(new SelectListItem
                {
                    Text = "Person",
                    Value = "2"
                });



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ord;
        }
        public List<SelectListItem> GetlstExtraItemsDropdown(int CompanyId)
        {
            List<SelectListItem> ProdList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@EitVou", 0);
                sqlParameters[1] = new SqlParameter("@EitNm", "");
                sqlParameters[2] = new SqlParameter("@EitAmt", 0);
                sqlParameters[3] = new SqlParameter("@Flg", 5);
                sqlParameters[4] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("Usp_ExtraItems_Insert", sqlParameters);
                if (DtProdList != null && DtProdList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProdList.Rows.Count; i++)
                    {
                        SelectListItem RefbyItem = new SelectListItem();
                        RefbyItem.Text = DtProdList.Rows[i]["EitNm"].ToString() +"_"+ DtProdList.Rows[i]["EitAmt"].ToString();
                        RefbyItem.Value = DtProdList.Rows[i]["EitVou"].ToString();
                        ProdList.Add(RefbyItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ProdList;
        }
        public List<SelectListItem> GetCategorylist(int CompanyId)         // Get Category List
        {
            List<SelectListItem> EveList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Type", "Category");
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtEveList = ObjDBConnection.CallStoreProcedure("Usp_GetDropdownData", sqlParameters);
                if (DtEveList != null && DtEveList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtEveList.Rows.Count; i++)
                    {
                        SelectListItem Eve = new SelectListItem();
                        Eve.Text = DtEveList.Rows[i]["TextName"].ToString();
                        Eve.Value = DtEveList.Rows[i]["Value"].ToString();
                        EveList.Add(Eve);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EveList;
        }

       
        public List<SelectListItem> GetEventLst(int CompanyId)         // Get Event List
        {
            List<SelectListItem> EveList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@FLG", "4");
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtEveList = ObjDBConnection.CallStoreProcedure("usp_InquiryMst_Insert", sqlParameters);
                if (DtEveList != null && DtEveList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtEveList.Rows.Count; i++)
                    {
                        SelectListItem Eve = new SelectListItem();
                        Eve.Text = DtEveList.Rows[i]["EveNm"].ToString();
                        Eve.Value = DtEveList.Rows[i]["EveVou"].ToString();
                        EveList.Add(Eve);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EveList;
        }

        public List<SelectListItem> GetJobMasterLst(int CompanyId)         // Get Job List
        {
            List<SelectListItem> JobList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@FLG", "7");
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtJobList = ObjDBConnection.CallStoreProcedure("Usp_JobMst_Main_Insert", sqlParameters);
                if (DtJobList != null && DtJobList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtJobList.Rows.Count; i++)
                    {
                        SelectListItem Job = new SelectListItem();
                        Job.Text = DtJobList.Rows[i]["JobNm"].ToString();
                        Job.Value = DtJobList.Rows[i]["JobVou"].ToString();
                        JobList.Add(Job);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JobList;
        }

        public List<SelectListItem> GetlstTearmAndConditionDropdown(int CompanyId)
        {
            List<SelectListItem> ProdList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@TncVou", 0);
                sqlParameters[1] = new SqlParameter("@TncType", 0);
                sqlParameters[2] = new SqlParameter("@TncNm", "");
                sqlParameters[3] = new SqlParameter("@TncDesc", "");
                sqlParameters[4] = new SqlParameter("@FLG", "5");
                sqlParameters[5] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtProdList = ObjDBConnection.CallStoreProcedure("Usp_TermsAndCond_Insert", sqlParameters);
                if (DtProdList != null && DtProdList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProdList.Rows.Count; i++)
                    {
                        SelectListItem RefbyItem = new SelectListItem();
                        RefbyItem.Text = DtProdList.Rows[i]["TncNm"].ToString();
                        RefbyItem.Value = DtProdList.Rows[i]["TncVou"].ToString();
                        ProdList.Add(RefbyItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ProdList;
        }
        public List<SelectListItem> GetShift()
        {
            List<SelectListItem> shifList = new List<SelectListItem>();
            try
            {
                shifList.Add(new SelectListItem
                {
                    Text = "First",
                    Value = "1"
                });

                shifList.Add(new SelectListItem
                {
                    Text = "Second",
                    Value = "2"
                });

                shifList.Add(new SelectListItem
                {
                    Text = "Office",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return shifList;
        }


        public List<SelectListItem> GetShiftNew()
        {
            List<SelectListItem> shifList = new List<SelectListItem>();
            try
            {
                shifList.Add(new SelectListItem
                {
                    Text = "First",
                    Value = "1"
                });

                shifList.Add(new SelectListItem
                {
                    Text = "Second",
                    Value = "2"
                });
                shifList.Add(new SelectListItem
                {
                    Text = "Both",
                    Value = "3"
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return shifList;
        }

        public List<SelectListItem> GetDataType()
        {
            List<SelectListItem> DataTypeList = new List<SelectListItem>();
            try
            {
                DataTypeList.Add(new SelectListItem
                {
                    Text = "Character",
                    Value = "1"
                });

                DataTypeList.Add(new SelectListItem
                {
                    Text = "Numeric",
                    Value = "2"
                });
                DataTypeList.Add(new SelectListItem
                {
                    Text = "Date",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataTypeList;
        }

        public List<SelectListItem> GetTextAlign()
        {
            List<SelectListItem> AlignList = new List<SelectListItem>();
            try
            {
                AlignList.Add(new SelectListItem
                {
                    Text = "L",
                    Value = "1"
                });

                AlignList.Add(new SelectListItem
                {
                    Text = "R",
                    Value = "2"
                });
                AlignList.Add(new SelectListItem
                {
                    Text = "C",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AlignList;
        }

        public List<SelectListItem> GetReportView()
        {
            List<SelectListItem> TypeList = new List<SelectListItem>();
            try
            {
                TypeList.Add(new SelectListItem
                {
                    Text = "View",
                    Value = "1"
                });

                TypeList.Add(new SelectListItem
                {
                    Text = "Report",
                    Value = "2"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TypeList;
        }


        public List<SelectListItem> GetMenuMasterDropdown()
        {
            List<SelectListItem> MenuList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@MnuVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                DataTable DtMenuList = ObjDBConnection.CallStoreProcedure("GetMenuMstDetails", sqlParameters);
                if (DtMenuList != null && DtMenuList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtMenuList.Rows.Count; i++)
                    {
                        SelectListItem menuItem = new SelectListItem();
                        menuItem.Text = DtMenuList.Rows[i]["Name"].ToString();
                        menuItem.Value = DtMenuList.Rows[i]["ModuleId"].ToString();
                        MenuList.Add(menuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MenuList;
        }

        public List<SelectListItem> GetTrnTypeDropdown()
        {
            List<SelectListItem> MenuList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@skiprecord", 0);
                sqlParameters[1] = new SqlParameter("@pagesize", 0);
                DataTable DtMenuList = ObjDBConnection.CallStoreProcedure("GetTranTypeDetails", sqlParameters);
                if (DtMenuList != null && DtMenuList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtMenuList.Rows.Count; i++)
                    {
                        SelectListItem menuItem = new SelectListItem();
                        menuItem.Text = DtMenuList.Rows[i]["TrnType"].ToString();
                        menuItem.Value = DtMenuList.Rows[i]["SrNo"].ToString();
                        MenuList.Add(menuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MenuList;
        }

        public List<SelectListItem> GetGroupList(int CompanyId)
        {
            List<SelectListItem> AreaList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Flg", 6);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtStateList = ObjDBConnection.CallStoreProcedure("Usp_ContactMst_Insert", sqlParameters);
                if (DtStateList != null && DtStateList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtStateList.Rows.Count; i++)
                    {
                        SelectListItem AreaItem = new SelectListItem();
                        AreaItem.Text = DtStateList.Rows[i]["AccGroup"].ToString();
                        AreaItem.Value = DtStateList.Rows[i]["AccGroup"].ToString();
                        AreaList.Add(AreaItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AreaList.Distinct().ToList();
        }

        public List<SelectListItem> GetAreaList(int CompanyId) 
        {
            List<SelectListItem> AreaList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Flg", 5);
                sqlParameters[1] = new SqlParameter("@Cmpvou", CompanyId);
                DataTable DtStateList = ObjDBConnection.CallStoreProcedure("Usp_ContactMst_Insert", sqlParameters);
                if (DtStateList != null && DtStateList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtStateList.Rows.Count; i++)
                    {
                        SelectListItem AreaItem = new SelectListItem();
                        AreaItem.Text = DtStateList.Rows[i]["AccAreaNm"].ToString();
                        AreaItem.Value = DtStateList.Rows[i]["AccAreaNm"].ToString();
                        AreaList.Add(AreaItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AreaList.Distinct().ToList();
        }
        public List<SelectListItem> GetStateMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "STA");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtStateList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtStateList != null && DtStateList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtStateList.Rows.Count; i++)
                    {
                        SelectListItem StateItem = new SelectListItem();
                        StateItem.Text = DtStateList.Rows[i]["MscNm"].ToString();
                        StateItem.Value = DtStateList.Rows[i]["MscVou"].ToString();
                        StateList.Add(StateItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StateList;
        }
        public List<SelectListItem> GetCityMasterDropdown1(int companyId, int isAdministrator)
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@CtyVou", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 0);
                sqlParameters[2] = new SqlParameter("@CmpVou", companyId);
                DataTable DtStateList = ObjDBConnection.CallStoreProcedure("GetCityDetailsByState", sqlParameters);

                if (DtStateList != null && DtStateList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtStateList.Rows.Count; i++)
                    {
                        SelectListItem StateItem = new SelectListItem();
                        StateItem.Text = DtStateList.Rows[i]["CtyNm"].ToString();
                        StateItem.Value = DtStateList.Rows[i]["CtyVou"].ToString();
                        StateList.Add(StateItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StateList;
        }

        public List<SelectListItem> GetProductGroupDropdown()    // Product Group Dropdown
        {
            List<SelectListItem> PrdGrList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@ProdVou", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 4);
                DataTable DtProdGrList = ObjDBConnection.CallStoreProcedure("Usp_ProductMaster_Insert", sqlParameters);

                if (DtProdGrList != null && DtProdGrList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProdGrList.Rows.Count; i++)
                    {
                        SelectListItem StateItem = new SelectListItem();
                        StateItem.Text = DtProdGrList.Rows[i]["PgrNm"].ToString();
                        StateItem.Value = DtProdGrList.Rows[i]["PgrVou"].ToString();
                        PrdGrList.Add(StateItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PrdGrList;
        }
        public List<SelectListItem> GetTransMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> TransList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "TRN");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtTransList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtTransList != null && DtTransList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtTransList.Rows.Count; i++)
                    {
                        SelectListItem StateItem = new SelectListItem();
                        StateItem.Text = DtTransList.Rows[i]["MscNm"].ToString();
                        StateItem.Value = DtTransList.Rows[i]["MscVou"].ToString();
                        TransList.Add(StateItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TransList;
        }

        public List<SelectListItem> GetGradeMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> GradeList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "GRD");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtGradeList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtGradeList != null && DtGradeList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtGradeList.Rows.Count; i++)
                    {
                        SelectListItem GradeItem = new SelectListItem();
                        GradeItem.Text = DtGradeList.Rows[i]["MscNm"].ToString();
                        GradeItem.Value = DtGradeList.Rows[i]["MscVou"].ToString();
                        GradeList.Add(GradeItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GradeList;
        }
        public List<SelectListItem> GetQualityMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> QualityList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "QLT");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtQualityList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtQualityList != null && DtQualityList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtQualityList.Rows.Count; i++)
                    {
                        SelectListItem QualityItem = new SelectListItem();
                        QualityItem.Text = DtQualityList.Rows[i]["MscNm"].ToString();
                        QualityItem.Value = DtQualityList.Rows[i]["MscVou"].ToString();
                        QualityList.Add(QualityItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return QualityList;
        }
        public List<SelectListItem> GetFinishMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> FinishList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "FIN");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtFinishList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtFinishList != null && DtFinishList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtFinishList.Rows.Count; i++)
                    {
                        SelectListItem FinishItem = new SelectListItem();
                        FinishItem.Text = DtFinishList.Rows[i]["MscNm"].ToString();
                        FinishItem.Value = DtFinishList.Rows[i]["MscVou"].ToString();
                        FinishList.Add(FinishItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinishList;
        }

        public List<SelectListItem> GetLotPrcTypMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> FinishList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "PRC");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtFinishList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtFinishList != null && DtFinishList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtFinishList.Rows.Count; i++)
                    {
                        SelectListItem FinishItem = new SelectListItem();
                        FinishItem.Text = DtFinishList.Rows[i]["MscNm"].ToString();
                        FinishItem.Value = DtFinishList.Rows[i]["MscVou"].ToString();
                        FinishList.Add(FinishItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinishList;
        }

        public List<SelectListItem> GetCompanyMasterDropdown(long companyId, long isAdministrator)
        {
            List<SelectListItem> CompanyList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@CmpVou", companyId);
                DataTable DtCompany = ObjDBConnection.CallStoreProcedure("GetDepartmentDetail", sqlParameters);
                if (DtCompany != null && DtCompany.Rows.Count > 0)
                {
                    for (int i = 0; i < DtCompany.Rows.Count; i++)
                    {
                        SelectListItem CompanyItem = new SelectListItem();
                        CompanyItem.Text = DtCompany.Rows[i]["DepName"].ToString();
                        CompanyItem.Value = DtCompany.Rows[i]["DepVou"].ToString();
                        CompanyList.Add(CompanyItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CompanyList;
        }

        public List<SelectListItem> GetGoDownMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> GoDownList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@CmpVou", companyId);
                DataTable DtGoDownList = ObjDBConnection.CallStoreProcedure("GetGoDownDetail", sqlParameters);
                if (DtGoDownList != null && DtGoDownList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtGoDownList.Rows.Count; i++)
                    {
                        SelectListItem GoDownItem = new SelectListItem();
                        GoDownItem.Text = DtGoDownList.Rows[i]["GdnNm"].ToString();
                        GoDownItem.Value = DtGoDownList.Rows[i]["GdnVou"].ToString();
                        GoDownList.Add(GoDownItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GoDownList;
        }

        public List<SelectListItem> GetLocationMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> LocationList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@CmpVou", companyId);
                DataTable DtLocationList = ObjDBConnection.CallStoreProcedure("GetLocationDetail", sqlParameters);
                if (DtLocationList != null && DtLocationList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLocationList.Rows.Count; i++)
                    {
                        SelectListItem LocationItem = new SelectListItem();
                        LocationItem.Text = DtLocationList.Rows[i]["LocNm"].ToString();
                        LocationItem.Value = DtLocationList.Rows[i]["LocVou"].ToString();
                        LocationList.Add(LocationItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LocationList;
        }

        public List<SelectListItem> GetSupplierMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> SupplierList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@CmpVou", companyId);
                DataTable DtSupplierList = ObjDBConnection.CallStoreProcedure("GetAccountDetail", sqlParameters);
                if (DtSupplierList != null && DtSupplierList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtSupplierList.Rows.Count; i++)
                    {
                        SelectListItem SupplierItem = new SelectListItem();
                        SupplierItem.Text = DtSupplierList.Rows[i]["AccNm"].ToString();
                        SupplierItem.Value = DtSupplierList.Rows[i]["AccVou"].ToString();
                        SupplierList.Add(SupplierItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SupplierList;
        }


        public List<SelectListItem> GetProductMasterDropdown(int companyId, int isAdministrator, String PrdType)
        {
            List<SelectListItem> ProductList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@CmpVou", companyId);
                sqlParameters[1] = new SqlParameter("@flg", 1);
                sqlParameters[2] = new SqlParameter("@PrdTyp", PrdType);
                DataTable DtProductList = ObjDBConnection.CallStoreProcedure("GetProductDetail", sqlParameters);
                if (DtProductList != null && DtProductList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProductList.Rows.Count; i++)
                    {
                        SelectListItem ProductItem = new SelectListItem();
                        ProductItem.Text = DtProductList.Rows[i]["PrdNm"].ToString();
                        ProductItem.Value = DtProductList.Rows[i]["PrdVou"].ToString();
                        ProductList.Add(ProductItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ProductList;
        }

        public List<SelectListItem> GetLotMasterDropdown(int companyId, int isAdministrator)
        {
            List<SelectListItem> LotList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "LOT");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtLotList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtLotList != null && DtLotList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLotList.Rows.Count; i++)
                    {
                        SelectListItem LotItem = new SelectListItem();
                        LotItem.Text = DtLotList.Rows[i]["MscNm"].ToString();
                        LotItem.Value = DtLotList.Rows[i]["MscVou"].ToString();
                        LotList.Add(LotItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LotList;
        }
        public List<SelectListItem> GetVoucherTypeDropdown(string type, int companyId)
        {
            List<SelectListItem> VouTypeList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@VTYTRNMSCCD", type);
                sqlParameters[1] = new SqlParameter("@CmpVou", companyId);
                DataTable DtVouTyList = ObjDBConnection.CallStoreProcedure("GetVoucherTypeDetail", sqlParameters);
                if (DtVouTyList != null && DtVouTyList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtVouTyList.Rows.Count; i++)
                    {
                        SelectListItem VouTypeItem = new SelectListItem();
                        VouTypeItem.Text = DtVouTyList.Rows[i]["VtyType"].ToString();
                        VouTypeItem.Value = DtVouTyList.Rows[i]["VtyVou"].ToString();
                        VouTypeList.Add(VouTypeItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return VouTypeList;
        }

        public List<SelectListItem> GetLotMasterDropdown_1(int companyId, int isAdministrator)
        {
            List<SelectListItem> LotList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "LOT");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtLotList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtLotList != null && DtLotList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLotList.Rows.Count; i++)
                    {
                        SelectListItem LotItem = new SelectListItem();
                        LotItem.Text = DtLotList.Rows[i]["MscNm"].ToString();
                        LotItem.Value = DtLotList.Rows[i]["MscNm"].ToString();
                        LotList.Add(LotItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LotList;
        }
        public List<SelectListItem> GetLotMasterDropdown_2(int companyId, int isAdministrator)
        {
            List<SelectListItem> LotList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "LOT");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                //if (isAdministrator == 1)
                //    companyId = 0;
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtLotList = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtLotList != null && DtLotList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLotList.Rows.Count; i++)
                    {
                        SelectListItem LotItem = new SelectListItem();
                        LotItem.Text = DtLotList.Rows[i]["MscNm"].ToString();
                        LotItem.Value = DtLotList.Rows[i]["MscVou"].ToString();
                        LotList.Add(LotItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LotList;
        }

        public List<SelectListItem> GetStockYN()
        {
            List<SelectListItem> StockYNList = new List<SelectListItem>();
            try
            {
                StockYNList.Add(new SelectListItem
                {
                    Text = "Yes",
                    Value = "1"
                });

                StockYNList.Add(new SelectListItem
                {
                    Text = "No",
                    Value = "2"
                });
                StockYNList.Add(new SelectListItem
                {
                    Text = "InProcess",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StockYNList;
        }
        public List<SelectListItem> GetCategoryType()
        {
            List<SelectListItem> StockYNList = new List<SelectListItem>();
            try
            {
                StockYNList.Add(new SelectListItem
                {
                    Text = "General",
                    Value = "1"
                });

                StockYNList.Add(new SelectListItem
                {
                    Text = "Extra",
                    Value = "2"
                });
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StockYNList;
        }

        public List<SelectListItem> GetTncType()
        {
            List<SelectListItem> TncTypeList = new List<SelectListItem>();
            try
            {
                TncTypeList.Add(new SelectListItem
                {
                    Text = "Inclusive",
                    Value = "1"
                });

                TncTypeList.Add(new SelectListItem
                {
                    Text = "Exclusive",
                    Value = "2"
                });

                TncTypeList.Add(new SelectListItem
                {
                    Text = "Terms & Condition",
                    Value = "3"
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TncTypeList;
        }


        public List<SelectListItem> GetMainCoilType()
        {
            List<SelectListItem> CoilTypeList = new List<SelectListItem>();
            try
            {
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "Slited Coil",
                    Value = "1"
                });
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "Bal. Patta",
                    Value = "2"
                });
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "Scrap",
                    Value = "3"
                });
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "M.Coil",
                    Value = "4"
                });
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "In Trans",
                    Value = "5"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CoilTypeList;
        }

        public List<SelectListItem> GetPicklingStatus()
        {
            List<SelectListItem> StatusList = new List<SelectListItem>();
            try
            {
                StatusList.Add(new SelectListItem
                {
                    Text = "AP",
                    Value = "1"
                });
                StatusList.Add(new SelectListItem
                {
                    Text = "Final Pickling",
                    Value = "2"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusList;
        }

        public List<SelectListItem> GetStraightingStatus()
        {
            List<SelectListItem> StatusList = new List<SelectListItem>();
            try
            {
                StatusList.Add(new SelectListItem
                {
                    Text = "AP",
                    Value = "1"
                });
                StatusList.Add(new SelectListItem
                {
                    Text = "Final Straighting",
                    Value = "2"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusList;
        }

        public List<SelectListItem> GetInwardCoilType()
        {
            List<SelectListItem> CoilTypeList = new List<SelectListItem>();
            try
            {
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "Slited Coil",
                    Value = "1"
                });
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "Bal. Patta",
                    Value = "2"
                });
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "Scrap",
                    Value = "3"
                });
                CoilTypeList.Add(new SelectListItem
                {
                    Text = "M.Coil",
                    Value = "4"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CoilTypeList;
        }


        public List<SelectListItem> GetRecIss()
        {
            List<SelectListItem> RecIssList = new List<SelectListItem>();
            try
            {
                RecIssList.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "1"
                });

                RecIssList.Add(new SelectListItem
                {
                    Text = "Recieve",
                    Value = "2"
                });
                RecIssList.Add(new SelectListItem
                {
                    Text = "Issue",
                    Value = "3"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RecIssList;
        }

        public List<CustomDropDown> GetProductMasterWithCodeDropdown(int companyId)
        {
            List<CustomDropDown> productList = new List<CustomDropDown>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@PrdVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtProductList = ObjDBConnection.CallStoreProcedure("GetProductDetails", sqlParameters);
                if (DtProductList != null && DtProductList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtProductList.Rows.Count; i++)
                    {
                        CustomDropDown productItem = new CustomDropDown();
                        productItem.Text = DtProductList.Rows[i]["PrdNm"].ToString();
                        productItem.Value = DtProductList.Rows[i]["PrdVou"].ToString();
                        productItem.Value1 = DtProductList.Rows[i]["PrdCd"].ToString();
                        productList.Add(productItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productList;
        }

        public List<SelectListItem> GetSupplierDropdown(int CompanyId)
        {
            List<SelectListItem> SupplierList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@MacRemks", "");
                sqlParameters[1] = new SqlParameter("@Flg", 4);
                sqlParameters[2] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtSupplierList = ObjDBConnection.CallStoreProcedure("Usp_ProductSupplier_Insert", sqlParameters);
                if (DtSupplierList != null && DtSupplierList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtSupplierList.Rows.Count; i++)
                    {
                        SelectListItem SupplierItem = new SelectListItem();
                        SupplierItem.Text = DtSupplierList.Rows[i]["AccAdd"].ToString();
                        SupplierItem.Value = DtSupplierList.Rows[i]["AccVou"].ToString();
                        SupplierList.Add(SupplierItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SupplierList;
        }

        public List<SelectListItem> GetProductDropdown(int CompanyId)                  // Product DropDown
        {
            List<SelectListItem> ProductList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@MacVou", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 5);
                sqlParameters[2] = new SqlParameter("@CmpVou", CompanyId);
                DataTable DtSupplierList = ObjDBConnection.CallStoreProcedure("Usp_ProductSupplier_Insert", sqlParameters);
                if (DtSupplierList != null && DtSupplierList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtSupplierList.Rows.Count; i++)
                    {
                        SelectListItem ProductItem = new SelectListItem();
                        ProductItem.Text = DtSupplierList.Rows[i]["ProdName"].ToString();
                        ProductItem.Value = DtSupplierList.Rows[i]["ProdVou"].ToString();
                        ProductList.Add(ProductItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ProductList;
        }

        public List<SelectListItem> GetSubCategoryDropdown()                  // Subcategory DropDown
        {
            List<SelectListItem> SubCategoryList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@SubCatMstVou", "0");
                sqlParameters[1] = new SqlParameter("@FLG", 4);
                DataTable DtSubCatList = ObjDBConnection.CallStoreProcedure("usp_SubCatMaster_Insert", sqlParameters);
                if (DtSubCatList != null && DtSubCatList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtSubCatList.Rows.Count; i++)
                    {
                        SelectListItem SubCatItem = new SelectListItem();
                        SubCatItem.Text = DtSubCatList.Rows[i]["CatNm"].ToString();
                        SubCatItem.Value = DtSubCatList.Rows[i]["CatVou"].ToString();
                        SubCategoryList.Add(SubCatItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SubCategoryList;
        }
        public List<SelectListItem> GetCategoryDropdown(int companyId=0)
        {
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];
                //sqlParameters[0] = new SqlParameter("@CmpVou", companyId);
                DataTable DtCategoryList = ObjDBConnection.CallStoreProcedure("usp_getCategory", sqlParameters);
                if (DtCategoryList != null && DtCategoryList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtCategoryList.Rows.Count; i++)
                    {
                        SelectListItem CategoryItem = new SelectListItem();
                        CategoryItem.Text = DtCategoryList.Rows[i]["CatNm"].ToString();
                        CategoryItem.Value = DtCategoryList.Rows[i]["CatVou"].ToString();
                        CategoryList.Add(CategoryItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CategoryList;
        }
        public List<SelectListItem> GetGroupDropdown()
        {
            List<SelectListItem> GroupList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[0];

                DataTable DtGroupList = ObjDBConnection.CallStoreProcedure("usp_getGroup", sqlParameters);
                if (DtGroupList != null && DtGroupList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtGroupList.Rows.Count; i++)
                    {
                        SelectListItem CategoryItem = new SelectListItem();
                        CategoryItem.Text = DtGroupList.Rows[i]["CngNm"].ToString();
                        CategoryItem.Value = DtGroupList.Rows[i]["CngVou"].ToString();
                        GroupList.Add(CategoryItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GroupList;
        }

        public List<SelectListItem> GetlstRefByDropdown(int companyId)
        {
            List<SelectListItem> GroupList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@CmpVou", companyId);

                DataTable DtRefList = ObjDBConnection.CallStoreProcedure("usp_getRefBy_Contact", sqlParameters);
                if (DtRefList != null && DtRefList.Rows.Count > 0)
                {
                    for (int i = 0; i < DtRefList.Rows.Count; i++)
                    {
                        SelectListItem RefbyItem = new SelectListItem();
                        RefbyItem.Text = DtRefList.Rows[i]["AccountRef"].ToString();
                       // RefbyItem.Value = DtRefList.Rows[i]["AcrVou"].ToString();
                        GroupList.Add(RefbyItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GroupList;
        }

        public int GetMaxCode(int CompanyId) 
        {
            int maxcode = 0;
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@FLG", 3);
                sqlParameters[1] = new SqlParameter("@CmpVou", CompanyId);

                DataTable DtRefList = ObjDBConnection.CallStoreProcedure("Usp_ContactMst_Insert", sqlParameters);
                maxcode = Convert.ToInt32(DtRefList.Rows[0]["Code"].ToString());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maxcode;
        }
        public List<SelectListItem> GetTypeList()
        {
            List<SelectListItem> AlignList = new List<SelectListItem>();
            try
            {
                AlignList.Add(new SelectListItem
                {
                    Text = "Textbox",
                    Value = "1"
                });

                AlignList.Add(new SelectListItem
                {
                    Text = "Dropdown",
                    Value = "2"
                });
                AlignList.Add(new SelectListItem
                {
                    Text = "Radio",
                    Value = "3"
                });
                AlignList.Add(new SelectListItem
                {
                    Text = "Checkbox",
                    Value = "4"
                });
                AlignList.Add(new SelectListItem
                {
                    Text = "Textarea",
                    Value = "5"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AlignList;
        }
    }
}
