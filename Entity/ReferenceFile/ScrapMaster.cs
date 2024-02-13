using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ScrapMaster
    {
        private static string Conn;
        public virtual string Connection { get; set; }

        public virtual string ItemCode { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual string Unit { get; set; }
        public virtual decimal ShelfLife { get; set; }
        public virtual string Classification { get; set; }
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

            a = Gears.RetriveData2("select * from Masterfile.Item where ItemCode= '" + Code + "'", Conn); //Ter
            foreach (DataRow dtRow in a.Rows)
            {
                ItemCode = dtRow["ItemCode"].ToString();
                Weight = Convert.ToDecimal(dtRow["Weight"]);
                ShelfLife = Convert.ToDecimal(dtRow["ShelfLife"]);
                Classification = dtRow["Classification"].ToString();
                Unit = dtRow["UnitBase"].ToString();
               

                //Description = dtRow["Description"].ToString();
                
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

        public void InsertData(ScrapMaster _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Item", "0", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("Masterfile.Item", "0", "Weight", _ent.Weight);
            DT1.Rows.Add("Masterfile.Item", "0", "ShelfLife", _ent.ShelfLife);
            DT1.Rows.Add("Masterfile.Item", "0", "Classification", _ent.Classification);
            DT1.Rows.Add("Masterfile.Item", "0", "ItemCategoryCode", "007");

            //DT1.Rows.Add("Masterfile.Item", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Item", "0", "UnitBase", _ent.Unit);
            DT1.Rows.Add("Masterfile.Item", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Item", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Item", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Item", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public void UpdateData(ScrapMaster _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.ItemCode);
            //DT1.Rows.Add("Masterfile.Item", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Item", "set", "Weight", _ent.Weight);
            DT1.Rows.Add("Masterfile.Item", "set", "ShelfLife", _ent.ShelfLife);
            DT1.Rows.Add("Masterfile.Item", "set", "Classification", _ent.Classification);
            DT1.Rows.Add("Masterfile.Item", "set", "UnitBase", _ent.Unit);
            DT1.Rows.Add("Masterfile.Item", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Item", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Item", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Item", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSCM", _ent.ItemCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(ScrapMaster _ent)
        {
            Conn = _ent.Connection; //Ter
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.ItemCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSCM", _ent.ItemCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
