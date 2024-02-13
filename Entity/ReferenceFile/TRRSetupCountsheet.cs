using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class TRRSetupCountsheet
    {

        private static string Conn;
        public virtual string Connection { get; set; }

        public virtual string RecordID { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual string ClassCode { get; set; }
        public virtual string SizeCode { get; set; }
        public virtual string RollID { get; set; }
        public virtual decimal RollQty { get; set; }
        public virtual decimal RemainingQty { get; set; }
        public virtual string Width { get; set; }
        public virtual string Shade { get; set; }
        public virtual string Shrinkage { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        
        public DataTable getdata(string Code, string Conn)
        {
            DataTable a;
            a = Gears.RetriveData2("SELECT * FROM WMS.CountSheetSetup WHERE RecordID = '" + Code + "' AND TransType = 'PRCRCR'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                RecordID = dtRow["RecordID"].ToString();
                DocNumber = dtRow["TransDoc"].ToString();
                ItemCode = dtRow["ItemCode"].ToString();
                ColorCode = dtRow["ColorCode"].ToString();
                ClassCode = dtRow["ClassCode"].ToString();
                SizeCode = dtRow["SizeCode"].ToString();
                RollID = dtRow["LineNumber"].ToString();
                RollQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OriginalBaseQty"]) ? 0 : dtRow["OriginalBaseQty"]); 
                RemainingQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["RemainingBaseQty"]) ? 0 : dtRow["RemainingBaseQty"]);
                Width = dtRow["Width"].ToString();
                Shade = dtRow["Shade"].ToString();
                Shrinkage = dtRow["Shrinkage"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                
                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString(); 
            }
            return a;
        }

        public void UpdateData(TRRSetupCountsheet _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "RecordID", _ent.RecordID);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "OriginalBaseQty", _ent.RollQty);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "RemainingBaseQty", _ent.RemainingQty);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Width", _ent.Width);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Shade", _ent.Shade);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Shrinkage", _ent.Shrinkage);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Field9", _ent.Field9);
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("TRRSET", _ent.RecordID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(TRRSetupCountsheet _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "RecordID", _ent.RecordID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("TRRSET", _ent.RecordID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
