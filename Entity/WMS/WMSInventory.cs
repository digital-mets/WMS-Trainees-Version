using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class WMSInventory
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        //public static string DocNumber;
        public virtual string RecordId { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual decimal EndingBalance { get; set; }
        public virtual string AsOfDate { get; set; }
        public virtual string UnitOfMeasure { get; set; }
        public virtual decimal PalletEndingBal { get; set; }
        public virtual string ProdNum { get; set; }
        public virtual string RRDate { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }


        public DataTable getdata(string Id, string Conn)
        {
            DataTable a;
            if (Id != null)
            {
                a = Gears.RetriveData2("SELECT A.*, B.FullName AS Added, C.FullName AS LastEdited FROM Masterfile.WMSInventory A "
                    + " LEFT JOIN IT.Users B ON A.AddedBy = B.UserID LEFT JOIN IT.Users C ON A.LastEditedBy = C.UserID WHERE RecordId = '" + Id + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    RecordId = dtRow["RecordId"].ToString();
                    BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                    WarehouseCode = dtRow["WarehouseCode"].ToString();
                    ServiceType = dtRow["ServiceType"].ToString();
                    EndingBalance = Convert.ToDecimal(dtRow["EndingBalance"].ToString());
                    AsOfDate = dtRow["AsOfDate"].ToString();
                    UnitOfMeasure = dtRow["UnitOfMeasure"].ToString();
                    PalletEndingBal = Convert.ToDecimal(dtRow["PalletEndingBal"].ToString());
                    ProdNum = dtRow["ProdNum"].ToString();
                    RRDate = dtRow["RRDate"].ToString();
                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();


                    AddedBy = dtRow["Added"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEdited"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(WMSInventory _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            //Docnum = _ent.DocNumber;
            //string "Masterfile.WMSInventory" = "Masterfile.WMSInventory";
            //string "0"1 = "0";
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            //DT1.Rows.Add("Masterfile.WMSInventory", "0", "RecordId", _ent.RecordId);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "EndingBalance", _ent.EndingBalance);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "AsOfDate", _ent.AsOfDate);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "UnitOfMeasure", _ent.UnitOfMeasure);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "PalletEndingBal", _ent.PalletEndingBal);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "ProdNum", _ent.ProdNum);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "RRDate", _ent.RRDate);

            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.WMSInventory", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.WMSInventory", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Masterfile.WMSInventory", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSINV", _ent.RecordId, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // KMM add Conn
        }

        public void UpdateData(WMSInventory _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            //Docnum = _ent.DocNumber;
            //string "Masterfile.WMSInventory" = "Masterfile.WMSInventory";
            //string "0" = "set";

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.WMSInventory", "cond", "RecordId", _ent.RecordId);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "EndingBalance", _ent.EndingBalance);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "AsOfDate", _ent.AsOfDate);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "UnitOfMeasure", _ent.UnitOfMeasure);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "PalletEndingBal", _ent.PalletEndingBal);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "ProdNum", _ent.ProdNum);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "RRDate", _ent.RRDate);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.WMSInventory", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("WMSINV", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }

        public void DeleteData(WMSInventory _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.WMSInventory", "cond", "RecordId", _ent.RecordId);

            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSINV", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
