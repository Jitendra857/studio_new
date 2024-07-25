using Microsoft.AspNetCore.Mvc.Rendering;
using PIOAccount.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PIOAccount.Classes
{
    public class GdnTransactionHelper
    {
        public DbConnection ObjDBConnection = new DbConnection();

        public List<SelectListItem> GetGodownListDropdown(int companyId)
        {
            List<SelectListItem> GodownList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "GDN");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtGodown = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtGodown != null && DtGodown.Rows.Count > 0)
                {
                    for (int i = 0; i < DtGodown.Rows.Count; i++)
                    {
                        SelectListItem GodownItem = new SelectListItem();
                        GodownItem.Text = DtGodown.Rows[i]["MScNm"].ToString();
                        GodownItem.Value = DtGodown.Rows[i]["MscVou"].ToString();
                        GodownList.Add(GodownItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return GodownList;
        }

        public List<SelectListItem> GetAcctListDropdown(int companyId)
        {
            List<SelectListItem> AccList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@AccVou", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@CmpVou", companyId);
                DataTable DtAcc = ObjDBConnection.CallStoreProcedure("GetAccountDetails", sqlParameters);
                if (DtAcc != null && DtAcc.Rows.Count > 0)
                {
                    for (int i = 0; i < DtAcc.Rows.Count; i++)
                    {
                        SelectListItem AccItem = new SelectListItem();
                        AccItem.Text = DtAcc.Rows[i]["AccNm"].ToString();
                        AccItem.Value = DtAcc.Rows[i]["AccVou"].ToString();
                        AccList.Add(AccItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return AccList;
        }

        public List<SelectListItem> GetPrdListDropdown(int companyId)
        {
            List<SelectListItem> PrdList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@PrdVou", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@CmpVou", companyId);
                DataTable DtPrd = ObjDBConnection.CallStoreProcedure("GetProductDetails", sqlParameters);
                if (DtPrd != null && DtPrd.Rows.Count > 0)
                {
                    for (int i = 0; i < DtPrd.Rows.Count; i++)
                    {
                        SelectListItem PrdItem = new SelectListItem();
                        PrdItem.Text = DtPrd.Rows[i]["PrdName"].ToString();
                        PrdItem.Value = DtPrd.Rows[i]["PrdVou"].ToString();
                        PrdList.Add(PrdItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return PrdList;
        }
    }
}
