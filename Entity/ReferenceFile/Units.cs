using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Units
    {
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN
        public virtual string UnitUnitCode { get; set; }
        public virtual string UnitDescription { get; set; }
        public virtual bool UnitIsRounded { get; set; }
        public virtual bool UnitIsInactive { get; set; }
        public virtual string UnitRecordID { get; set; }

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
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }
        public DataTable getdata(string Unicode, string Conn)
        {
            DataTable a;

            if (Unicode != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.Unit where UnitCode = '" + Unicode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    UnitUnitCode = dtRow["UnitCode"].ToString();
                    UnitDescription = dtRow["Description"].ToString();
                    UnitIsRounded = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsRounded"]) ? false : dtRow["IsRounded"]);
                    UnitIsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);

                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();

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
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(Units _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Unit", "0", "UnitCode", _ent.UnitUnitCode);
            DT1.Rows.Add("Masterfile.Unit", "0", "Description", _ent.UnitDescription);
            DT1.Rows.Add("Masterfile.Unit", "0", "IsRounded", _ent.UnitIsRounded);
            DT1.Rows.Add("Masterfile.Unit", "0", "IsInactive", _ent.UnitIsInactive);

            DT1.Rows.Add("Masterfile.Unit", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Unit", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFUNIT", _ent.UnitUnitCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        
        }

        public void UpdateData(Units _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Unit", "cond", "UnitCode", _ent.UnitUnitCode);
            DT1.Rows.Add("Masterfile.Unit", "set", "Description", _ent.UnitDescription);
            DT1.Rows.Add("Masterfile.Unit", "set", "IsRounded", _ent.UnitIsRounded);
            DT1.Rows.Add("Masterfile.Unit", "set", "IsInactive", _ent.UnitIsInactive);

            DT1.Rows.Add("Masterfile.Unit", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Unit", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.", "set", "Field9", _ent.Field9);
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFUNIT", _ent.UnitUnitCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
      
        }
        public void DeleteData(Units _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            UnitUnitCode = _ent.UnitUnitCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Unit", "cond", "UnitCode", _ent.UnitUnitCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFUNIT", _ent.UnitUnitCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
