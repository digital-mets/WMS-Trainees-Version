using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class RCountSheetSetUp
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string RecordId { get; set; }
        public virtual string TransType { get; set; }
        public virtual string TransDoc { get; set; }
        public virtual string TransLine { get; set; }
        public virtual string LineNumber { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual string ClassCode { get; set; }
        public virtual string SizeCode { get; set; }
        public virtual string PalletID { get; set; }
        public virtual string Location { get; set; }
        public virtual string ExpirationDate { get; set; }
        public virtual string MfgDate { get; set; }
        public virtual string RRdate { get; set; }
        public virtual decimal OriginalBulkQty { get; set; }
        public virtual decimal OriginalBaseQty { get; set; }
        public virtual string OriginalLocation { get; set; }
        public virtual decimal RemainingBulkQty { get; set; }
        public virtual decimal RemainingBaseQty { get; set; }
        public virtual decimal PickedBulkQty { get; set; }
        public virtual decimal PickedBaseQty { get; set; }
        public virtual decimal ReservedBulkQty { get; set; }
        public virtual decimal ReservedBaseQty { get; set; }
        public virtual decimal OriginalCost { get; set; }
        public virtual decimal UnitCost { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string PutawayDate { get; set; }
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




        public DataTable getdata(string Count, string Conn)//KMM add Conn
        {
            DataTable a;

            if (Count != null)
            {
                a = Gears.RetriveData2("select * from WMS.CountSheetSetUp where RecordId = '" + Count + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {
                    RecordId = dtRow["RecordId"].ToString();
                    TransType = dtRow["TransType"].ToString();
                    TransDoc = dtRow["TransDoc"].ToString();
                    TransLine = dtRow["TransLine"].ToString();
                    LineNumber = dtRow["LineNumber"].ToString();
                    ItemCode = dtRow["ItemCode"].ToString();
                    ColorCode = dtRow["ColorCode"].ToString();
                    ClassCode = dtRow["ClassCode"].ToString();
                    SizeCode = dtRow["SizeCode"].ToString();
                    PalletID = dtRow["PalletID"].ToString();
                    Location = dtRow["Location"].ToString();
                    ExpirationDate = dtRow["ExpirationDate"].ToString();
                    MfgDate = dtRow["MfgDate"].ToString();
                    RRdate = dtRow["RRdate"].ToString();
                    OriginalBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OriginalBulkQty"].ToString()) ? "0" : dtRow["OriginalBulkQty"].ToString());
                    OriginalBaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OriginalBaseQty"].ToString()) ? "0" : dtRow["OriginalBaseQty"].ToString());
                    OriginalLocation = dtRow["OriginalLocation"].ToString();
                    RemainingBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["RemainingBulkQty"].ToString()) ? "0" : dtRow["RemainingBulkQty"].ToString());
                    RemainingBaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["RemainingBaseQty"].ToString()) ? "0" : dtRow["RemainingBaseQty"].ToString());
                    PickedBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["PickedBulkQty"].ToString()) ? "0" : dtRow["PickedBulkQty"].ToString());
                    PickedBaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["PickedBaseQty"].ToString()) ? "0" : dtRow["PickedBaseQty"].ToString());
                    ReservedBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["ReservedBulkQty"].ToString()) ? "0" : dtRow["ReservedBulkQty"].ToString());
                    ReservedBaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["ReservedBaseQty"].ToString()) ? "0" : dtRow["ReservedBaseQty"].ToString());
                    OriginalCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OriginalCost"].ToString()) ? "0" : dtRow["OriginalCost"].ToString());
                    UnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["UnitCost"].ToString()) ? "0" : dtRow["UnitCost"].ToString());
                    SubmittedDate = dtRow["SubmittedDate"].ToString();
                    PutawayDate = dtRow["PutawayDate"].ToString();

                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();

                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                }

            }

            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
            return a;
        }
        public void InsertData(RCountSheetSetUp _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //DT1.Rows.Add("WMS.CountSheetSetUp", "0", "RecordId", _ent.RecordId);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", _ent.TransType);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", _ent.TransDoc);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", _ent.TransLine);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", _ent.LineNumber);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", _ent.ColorCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", _ent.ClassCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", _ent.SizeCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "PalletID", _ent.PalletID);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Location", _ent.Location);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "ExpirationDate", _ent.ExpirationDate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "MfgDate", _ent.MfgDate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "RRdate", _ent.RRdate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "OriginalBulkQty", _ent.OriginalBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "OriginalBaseQty", _ent.OriginalBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "OriginalLocation", _ent.OriginalLocation);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "RemainingBulkQty", _ent.RemainingBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "RemainingBaseQty", _ent.RemainingBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "PickedBulkQty", _ent.PickedBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "PickedBaseQty", _ent.PickedBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "ReservedBulkQty", _ent.ReservedBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "ReservedBaseQty", _ent.ReservedBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "OriginalCost", _ent.OriginalCost);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "UnitCost", _ent.UnitCost);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "SubmittedDate", _ent.SubmittedDate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "PutawayDate", _ent.PutawayDate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.CountSheetSetUp", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);//KMM add Conn

           // Functions.AuditTrail("REFSETUP", _ent.RecordId, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }

        public void UpdateData(RCountSheetSetUp _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.CountSheetSetUp", "cond", "RecordId", _ent.RecordId);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "TransType", _ent.TransType);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "TransDoc", _ent.TransDoc);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "TransLine", _ent.TransLine);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "LineNumber", _ent.LineNumber);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "ColorCode", _ent.ColorCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "ClassCode", _ent.ClassCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "SizeCode", _ent.SizeCode);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "PalletID", _ent.PalletID);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Location", _ent.Location);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "ExpirationDate", _ent.ExpirationDate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "MfgDate", _ent.MfgDate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "RRdate", _ent.RRdate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBulkQty", _ent.OriginalBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBaseQty", _ent.OriginalBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalLocation", _ent.OriginalLocation);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "RemainingBulkQty", _ent.RemainingBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "RemainingBaseQty", _ent.RemainingBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "PickedBulkQty", _ent.PickedBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "PickedBaseQty", _ent.PickedBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "ReservedBulkQty", _ent.ReservedBulkQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "ReservedBaseQty", _ent.ReservedBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalCost", _ent.OriginalCost);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "UnitCost", _ent.UnitCost);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "SubmittedDate", _ent.SubmittedDate);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "PutawayDate", _ent.PutawayDate);

            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.CountSheetSetUp", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFSETUP", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn
        }
        
        public void DeleteData(RCountSheetSetUp _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.CountSheetSetUp", "cond", "RecordId", _ent.RecordId);
            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFSETUP", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn



        }
    }
}
