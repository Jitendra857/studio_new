using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PIOAccount.Models;

namespace PIOAccount.Classes
{
    public class TaxMasterHelpers
    {

        public DbConnection ObjDBConnection = new DbConnection();

        public List<CustomDropDown> GetSalesAccountDropdown(int companyId, int type)
        {
            List<CustomDropDown> SalesAcount = new List<CustomDropDown>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@AccVOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", type);
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtSalesAcc = ObjDBConnection.CallStoreProcedure("GetAccountDetails", sqlParameters);
                if (DtSalesAcc != null && DtSalesAcc.Rows.Count > 0)
                {
                    for (int i = 0; i < DtSalesAcc.Rows.Count; i++)
                    {
                        CustomDropDown SalesAccItem = new CustomDropDown();
                        SalesAccItem.Text = DtSalesAcc.Rows[i]["AccNm"].ToString()+" - "+ DtSalesAcc.Rows[i]["AccCity"].ToString() + " - " + DtSalesAcc.Rows[i]["AccCd"].ToString();
                        SalesAccItem.Value = DtSalesAcc.Rows[i]["AccVou"].ToString();
                        SalesAcount.Add(SalesAccItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return SalesAcount;
        }
        public List<SelectListItem> GetGodownDropdown(int companyId)
        {
            List<SelectListItem> GodownList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@GdnVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtGodown = ObjDBConnection.CallStoreProcedure("GetGodownDetails", sqlParameters);
                if (DtGodown != null && DtGodown.Rows.Count > 0)
                {
                    for (int i = 0; i < DtGodown.Rows.Count; i++)
                    {
                        SelectListItem GodownItem = new SelectListItem();
                        GodownItem.Text = DtGodown.Rows[i]["GdnNm"].ToString();
                        GodownItem.Value = DtGodown.Rows[i]["GdnVou"].ToString();
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
        public List<SelectListItem> GetLocationDropdown(int companyId)
        {
            List<SelectListItem> LocationList = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@LocVou", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtLoc = ObjDBConnection.CallStoreProcedure("GetLocationDetails", sqlParameters);
                if (DtLoc != null && DtLoc.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLoc.Rows.Count; i++)
                    {
                        SelectListItem LocationItem = new SelectListItem();
                        LocationItem.Text = DtLoc.Rows[i]["LocNm"].ToString();
                        LocationItem.Value = DtLoc.Rows[i]["LocVou"].ToString();
                        LocationList.Add(LocationItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return LocationList;
        }

        public List<SelectListItem> GetDepartmentDropdown(int companyId = 0)
        {
            List<SelectListItem> Department = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtDept = ObjDBConnection.CallStoreProcedure("GetDepartmentDetails", sqlParameters);
                if (DtDept != null && DtDept.Rows.Count > 0)
                {
                    for (int i = 0; i < DtDept.Rows.Count; i++)
                    {
                        SelectListItem DepartmentItem = new SelectListItem();
                        DepartmentItem.Text = DtDept.Rows[i]["DepName"].ToString();
                        DepartmentItem.Value = DtDept.Rows[i]["DepVou"].ToString();
                        Department.Add(DepartmentItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Department;
        }
        public List<SelectListItem> GetBiltyLayoutDropdown()
        {
            List<SelectListItem> Layout = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", 'B');
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                DataTable DtLayout = ObjDBConnection.CallStoreProcedure("GetLayoutDetails", sqlParameters);
                if (DtLayout != null && DtLayout.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLayout.Rows.Count; i++)
                    {
                        SelectListItem LayoutItem = new SelectListItem();
                        LayoutItem.Text = DtLayout.Rows[i]["RptDesc"].ToString();
                        LayoutItem.Value = DtLayout.Rows[i]["RptVou"].ToString();
                        Layout.Add(LayoutItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Layout;
        }
        public List<SelectListItem> GetMemoLayoutDropdown()
        {
            List<SelectListItem> Layout = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", 'M');
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                DataTable DtLayout = ObjDBConnection.CallStoreProcedure("GetLayoutDetails", sqlParameters);
                if (DtLayout != null && DtLayout.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLayout.Rows.Count; i++)
                    {
                        SelectListItem LayoutItem = new SelectListItem();
                        LayoutItem.Text = DtLayout.Rows[i]["RptDesc"].ToString();
                        LayoutItem.Value = DtLayout.Rows[i]["RptVou"].ToString();
                        Layout.Add(LayoutItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Layout;
        }
        public List<SelectListItem> GetInvoiceLayoutDropdown()
        {
            List<SelectListItem> Layout = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", 'I');
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                DataTable DtLayout = ObjDBConnection.CallStoreProcedure("GetLayoutDetails", sqlParameters);
                if (DtLayout != null && DtLayout.Rows.Count > 0)
                {
                    for (int i = 0; i < DtLayout.Rows.Count; i++)
                    {
                        SelectListItem LayoutItem = new SelectListItem();
                        LayoutItem.Text = DtLayout.Rows[i]["RptDesc"].ToString();
                        LayoutItem.Value = DtLayout.Rows[i]["RptVou"].ToString();
                        Layout.Add(LayoutItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Layout;
        }

        public List<SelectListItem> GetTaxDropdown()
        {
            List<SelectListItem> TaxMst = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@TaxVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                DataTable DtTax = ObjDBConnection.CallStoreProcedure("GetTaxMstDetails", sqlParameters);
                if (DtTax != null && DtTax.Rows.Count > 0)
                {
                    for (int i = 0; i < DtTax.Rows.Count; i++)
                    {
                        SelectListItem TaxMstItem = new SelectListItem();
                        TaxMstItem.Text = DtTax.Rows[i]["TaxName"].ToString();
                        TaxMstItem.Value = DtTax.Rows[i]["TaxVou"].ToString();
                        TaxMst.Add(TaxMstItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return TaxMst;
        }

        public List<CustomDropDown> GetTaxCustomDropdown(int companyId = 0)
        {
            List<CustomDropDown> TaxMst = new List<CustomDropDown>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@TaxVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtTax = ObjDBConnection.CallStoreProcedure("GetTaxMstDetails", sqlParameters);
                if (DtTax != null && DtTax.Rows.Count > 0)
                {
                    for (int i = 0; i < DtTax.Rows.Count; i++)
                    {
                        CustomDropDown TaxMstItem = new CustomDropDown();
                        TaxMstItem.Text = DtTax.Rows[i]["TaxName"].ToString();
                        TaxMstItem.Value = DtTax.Rows[i]["TaxVou"].ToString();
                        TaxMstItem.Value1 = DtTax.Rows[i]["TaxRate"].ToString();
                        TaxMstItem.Value2 = DtTax.Rows[i]["TaxRate2"].ToString();
                        TaxMst.Add(TaxMstItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return TaxMst;
        }

        public List<SelectListItem> GetVehicleDropdown(int companyId)
        {
            List<SelectListItem> Vehicle = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@VehVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtVehi = ObjDBConnection.CallStoreProcedure("GetVehicleMstDetails", sqlParameters);
                if (DtVehi != null && DtVehi.Rows.Count > 0)
                {
                    for (int i = 0; i < DtVehi.Rows.Count; i++)
                    {
                        SelectListItem VehicleItem = new SelectListItem();
                        VehicleItem.Text = DtVehi.Rows[i]["VehName"].ToString();
                        VehicleItem.Value = DtVehi.Rows[i]["VehVou"].ToString();
                        Vehicle.Add(VehicleItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Vehicle;
        }

        public List<SelectListItem> GetDriverDropdown(int companyId)
        {
            List<SelectListItem> Driver = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                DataTable DtDrvier = ObjDBConnection.CallStoreProcedure("GetDriverDetails", sqlParameters);
                if (DtDrvier != null && DtDrvier.Rows.Count > 0)
                {
                    for (int i = 0; i < DtDrvier.Rows.Count; i++)
                    {
                        SelectListItem DriverItem = new SelectListItem();
                        DriverItem.Text = DtDrvier.Rows[i]["DrvName"].ToString();
                        DriverItem.Value = DtDrvier.Rows[i]["DrvVou"].ToString();
                        Driver.Add(DriverItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Driver;
        }

        public List<SelectListItem> GetTransportNameDropdown(int companyId)
        {
            List<SelectListItem> VehTransportName = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "TRN");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtTransName = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtTransName != null && DtTransName.Rows.Count > 0)
                {
                    for (int i = 0; i < DtTransName.Rows.Count; i++)
                    {
                        SelectListItem TransportNameItem = new SelectListItem();
                        TransportNameItem.Text = DtTransName.Rows[i]["MscNm"].ToString();
                        TransportNameItem.Value = DtTransName.Rows[i]["MscVou"].ToString();
                        VehTransportName.Add(TransportNameItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return VehTransportName;
        }

        public List<SelectListItem> GetCityNameDropdown(int companyId)
        {
            List<SelectListItem> VehTransportName = new List<SelectListItem>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@VOU", 0);
                sqlParameters[1] = new SqlParameter("@Type", "CTY");
                sqlParameters[2] = new SqlParameter("@Flg", 2);
                sqlParameters[3] = new SqlParameter("@skiprecord", 0);
                sqlParameters[4] = new SqlParameter("@pagesize", 0);
                sqlParameters[5] = new SqlParameter("@CmpVou", companyId);
                DataTable DtTransName = ObjDBConnection.CallStoreProcedure("GetMscMstDetails", sqlParameters);
                if (DtTransName != null && DtTransName.Rows.Count > 0)
                {
                    for (int i = 0; i < DtTransName.Rows.Count; i++)
                    {
                        SelectListItem TransportNameItem = new SelectListItem();
                        TransportNameItem.Text = DtTransName.Rows[i]["MscNm"].ToString();
                        TransportNameItem.Value = DtTransName.Rows[i]["MscVou"].ToString();
                        VehTransportName.Add(TransportNameItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return VehTransportName;
        }

        public List<SelectListItem> GetVoucherTransactionType()
        {
            List<SelectListItem> TransactionType = new List<SelectListItem>();
            try
            {
                TransactionType.Add(new SelectListItem
                {
                    Text = "SAL",
                    Value = "SAL"
                });

                TransactionType.Add(new SelectListItem
                {
                    Text = "PUR",
                    Value = "PUR"
                });
                TransactionType.Add(new SelectListItem
                {
                    Text = "REC",
                    Value = "REC"
                });
                TransactionType.Add(new SelectListItem
                {
                    Text = "PAY",
                    Value = "PAY"
                });
                TransactionType.Add(new SelectListItem
                {
                    Text = "JV",
                    Value = "JV"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TransactionType;
        }

        public List<SelectListItem> GetUnitMode()
        {
            List<SelectListItem> UnitList = new List<SelectListItem>();
            try
            {
                UnitList.Add(new SelectListItem
                {
                    Text = "Bags",
                    Value = "0"
                });

                UnitList.Add(new SelectListItem
                {
                    Text = "Box",
                    Value = "1"
                });
                UnitList.Add(new SelectListItem
                {
                    Text = "Cutta",
                    Value = "2"
                });
                UnitList.Add(new SelectListItem
                {
                    Text = "Thela",
                    Value = "3"
                });
                UnitList.Add(new SelectListItem
                {
                    Text = "None",
                    Value = "4"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UnitList;
        }
        public List<SelectListItem> GetActiveYesNo()
        {
            List<SelectListItem> activeList = new List<SelectListItem>();
            try
            {
                activeList.Add(new SelectListItem
                {
                    Text = "Yes",
                    Value = "0"
                });

                activeList.Add(new SelectListItem
                {
                    Text = "No",
                    Value = "1"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return activeList;
        }
        public List<CustomDropDown> GetAccountCustomDropdown(int companyId, int acctype)
        {
            List<CustomDropDown> SalesAcount = new List<CustomDropDown>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@AccVOU", 0);
                sqlParameters[1] = new SqlParameter("@Flg", 2);
                sqlParameters[2] = new SqlParameter("@skiprecord", 0);
                sqlParameters[3] = new SqlParameter("@pagesize", 0);
                sqlParameters[4] = new SqlParameter("@CmpVou", companyId);
                sqlParameters[5] = new SqlParameter("@Type", acctype);
                DataTable DtSalesAcc = ObjDBConnection.CallStoreProcedure("GetAccountDetails", sqlParameters);
                if (DtSalesAcc != null && DtSalesAcc.Rows.Count > 0)
                {
                    for (int i = 0; i < DtSalesAcc.Rows.Count; i++)
                    {
                        CustomDropDown SalesAccItem = new CustomDropDown();
                        SalesAccItem.Text = DtSalesAcc.Rows[i]["AccNm"].ToString();
                        SalesAccItem.Value = DtSalesAcc.Rows[i]["AccVou"].ToString();
                        SalesAccItem.Value1 = DtSalesAcc.Rows[i]["AccCity"].ToString();
                        SalesAcount.Add(SalesAccItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return SalesAcount;
        }

        public List<CustomDropDown> GetVoucherTypeList(string VTypTrnType, int companyId)
        {
            List<CustomDropDown> objlist = new List<CustomDropDown>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@VtyType", VTypTrnType);
                sqlParameters[1] = new SqlParameter("@Flg", "2");
                sqlParameters[2] = new SqlParameter("@cmpvou", companyId);
                DataTable DtVTypeMst = ObjDBConnection.CallStoreProcedure("GetVoucherTypeDetails", sqlParameters);
                if (DtVTypeMst != null && DtVTypeMst.Rows.Count > 0)
                {
                    for (int i = 0; i < DtVTypeMst.Rows.Count; i++)
                    {
                        CustomDropDown obj = new CustomDropDown();
                        obj.Value = DtVTypeMst.Rows[i]["VtyVou"].ToString();
                        obj.Text = DtVTypeMst.Rows[i]["VtyType"].ToString();
                        objlist.Add(obj);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objlist;
        }

    }
}
