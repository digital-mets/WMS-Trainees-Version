using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Waist
    {
        private static string Conn; 
        public virtual string Connection { get; set; } 

        public virtual string WaistCode { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public DataTable getdata(string Code, string Conn) //Ter
        {
            DataTable a;

                a = Gears.RetriveData2("select * from Masterfile.Waist where WaistCode= '" + Code+ "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    WaistCode = dtRow["WaistCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
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

            return a;
        }

        public void InsertData(Waist _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Waist", "0", "WaistCode", _ent.WaistCode);
            DT1.Rows.Add("Masterfile.Waist", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Waist", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Waist", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Waist", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Waist", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Waist", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public void UpdateData(Waist _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Waist", "cond", "WaistCode", _ent.WaistCode);
            DT1.Rows.Add("Masterfile.Waist", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Waist", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Waist", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Waist", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Waist", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Waist", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFWAST", _ent.WaistCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(Waist _ent)
        {
            Conn = _ent.Connection; //Ter
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Waist", "cond", "WaistCode", _ent.WaistCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFWAST", _ent.WaistCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
