using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Status
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        public virtual string StatusCode { get; set; }
        public virtual string Description { get; set; }
        public virtual int Sequence { get; set; }
        public virtual decimal MarkDownRate { get; set; }
        public virtual int Modulus { get; set; }
        public virtual string Adjustment { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

        public virtual string StatusType { get; set; }
        public virtual string OutrightSales { get; set; }
        public virtual string CostOfSales { get; set; }
        public virtual string SalesReturn { get; set; }
        public virtual string GrossSalesRetail { get; set; }
        public virtual string RetailDiscount { get; set; }
        public virtual string OutletMargin { get; set; }
        
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public DataTable getdata(string MFStatus, string Conn) //Ter
        {
            DataTable a;

            if (MFStatus != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.StockStatus where StatusCode = '" + MFStatus + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    StatusCode = dtRow["StatusCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    Sequence = Convert.ToInt32(dtRow["Sequence"].ToString());
                    MarkDownRate = Convert.ToDecimal(dtRow["MarkDownRate"].ToString());
                    Modulus = Convert.ToInt32(dtRow["Modulus"].ToString());
                    Adjustment = dtRow["Adjustment"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]) ;
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();

                    StatusType = dtRow["StatusType"].ToString();
                    OutrightSales = dtRow["OutrightSales"].ToString();
                    CostOfSales = dtRow["CostOfSales"].ToString();
                    SalesReturn = dtRow["SalesReturn"].ToString();
                    GrossSalesRetail = dtRow["GrossSalesRetail"].ToString();
                    RetailDiscount = dtRow["RetailDiscount"].ToString();
                    OutletMargin = dtRow["OutletMargin"].ToString();

                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn); //Ter
            }
            return a;
        }
        public void InsertData(Status _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.StockStatus", "0", "StatusCode", _ent.StatusCode);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Sequence", _ent.Sequence);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "MarkDownRate", _ent.MarkDownRate);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Modulus", _ent.Modulus);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Adjustment", _ent.Adjustment);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.StockStatus", "0", "StatusType", _ent.StatusType);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "OutrightSales", _ent.OutrightSales);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "CostOfSales", _ent.CostOfSales);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "SalesReturn", _ent.SalesReturn);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "GrossSalesRetail", _ent.GrossSalesRetail);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "RetailDiscount", _ent.RetailDiscount);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "OutletMargin", _ent.OutletMargin);

            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.StockStatus", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER

            Functions.AuditTrail("REFSTATUS", _ent.StatusCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // TER
        }

        public void UpdateData(Status _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.StockStatus", "cond", "StatusCode", _ent.StatusCode);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Sequence", _ent.Sequence);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "MarkDownRate", _ent.MarkDownRate);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Modulus", _ent.Modulus);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Adjustment", _ent.Adjustment);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.StockStatus", "set", "StatusType", _ent.StatusType);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "OutrightSales", _ent.OutrightSales);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "CostOfSales", _ent.CostOfSales);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "SalesReturn", _ent.SalesReturn);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "GrossSalesRetail", _ent.GrossSalesRetail);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "RetailDiscount", _ent.RetailDiscount);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "OutletMargin", _ent.OutletMargin);

            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.StockStatus", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter

            Functions.AuditTrail("REFSTATUS", _ent.StatusCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter
        }
        public void DeleteData(Status _ent)
        {
            Conn = _ent.Connection; //Ter
            StatusCode = _ent.StatusCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.StockStatus", "cond", "StatusCode", _ent.StatusCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSTATUS", _ent.StatusCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
