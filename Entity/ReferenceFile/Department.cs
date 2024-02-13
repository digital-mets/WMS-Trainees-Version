using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DepartmentMasterfile
    {
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN

        

        private static string ID;
        public virtual string DepartmentCode { get; set; }
        public virtual string DepartmentName { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public virtual bool IsDefault { get; set; }

        //
        public DataTable getdata(string DepartmentCode, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from Masterfile.Department where DepartmentCode = '" + DepartmentCode + "'", Conn);

           

            foreach (DataRow dtRow in a.Rows)
            {
                DepartmentCode = dtRow["DepartmentCode"].ToString();
                DepartmentName = dtRow["DepartmentName"].ToString();
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                IsDefault = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsDefault"]) ? false : dtRow["IsDefault"]);

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                DeactivatedDate = dtRow["DeactivatedDate"].ToString();

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
        public void InsertData(DepartmentMasterfile _ent)
        {

            Conn = _ent.Connection; //ADD CONN
            ID = _ent.DepartmentCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Department", "0", "DepartmentCode", _ent.DepartmentCode);
            DT1.Rows.Add("Masterfile.Department", "0", "DepartmentName", _ent.DepartmentName);
            DT1.Rows.Add("Masterfile.Department", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Department", "0", "IsDefault", _ent.IsDefault);

            

            DT1.Rows.Add("Masterfile.Department", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Department", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Department", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Department", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Department", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Department", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Department", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Department", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Department", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.Department", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Department", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
          


            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(DepartmentMasterfile _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            ID = _ent.DepartmentCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.Department", "cond", "DepartmentCode", ID);
            DT1.Rows.Add("Masterfile.Department", "set", "DepartmentName", _ent.DepartmentName);
            DT1.Rows.Add("Masterfile.Department", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Department", "set", "IsDefault", _ent.IsDefault);

            DT1.Rows.Add("Masterfile.Department", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Department", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Department", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Department", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Department", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Department", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Department", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Department", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Department", "set", "Field9", _ent.Field9);
            
            DT1.Rows.Add("Masterfile.Department", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Department", "set", "LastEditedDate", _ent.LastEditedDate);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFDPT", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

        }
        public void DeleteData(DepartmentMasterfile _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            ID = _ent.DepartmentCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Department", "cond", "DepartmentCode", ID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFDPT", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
