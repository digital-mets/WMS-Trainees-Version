using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class KPIEquivalentsRate
    {

       public virtual string EntityCode { get; set; }
        public virtual string KPICode { get; set; }
        public virtual string Lower { get; set; }
        public virtual string Upper { get; set; }
        public virtual string Rating { get; set; }
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
        public DataTable getdata(string EntityCode, string KPICode)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from KPI.EquivalentsRate where EntityCode = '" + EntityCode + "' and KPICode = '" + KPICode + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                EntityCode = dtRow["EntityCode"].ToString();
                KPICode = dtRow["KPICode"].ToString();
                Lower = dtRow["Lower"].ToString();
                Upper = dtRow["Upper"].ToString();
                Rating = dtRow["Rating"].ToString();
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

            return a;
        }
        public void InsertData(KPIEquivalentsRate _ent)
        {
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("KPI.EquivalentsRate", "0", "EntityCode", _ent.EntityCode);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "KPICode", _ent.KPICode);

            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Lower", _ent.Lower);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Upper", _ent.Upper);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Rating", _ent.Rating);

            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("KPI.EquivalentsRate", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1);
        }
        public void UpdateData(KPIEquivalentsRate _ent)
        {
         

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.EquivalentsRate", "cond", "EntityCode", _ent.EntityCode);
            DT1.Rows.Add("KPI.EquivalentsRate", "cond", "KPICode", _ent.KPICode);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Lower", _ent.Lower);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Upper", _ent.Upper);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Rating", _ent.Rating);

            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("KPI.EquivalentsRate", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1);

            Functions.AuditTrail("KPIEquivalentsRate", EntityCode + "" + KPICode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(KPIEquivalentsRate _ent)
        {
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.EquivalentsRate", "cond", "EntityCode", _ent.EntityCode);
              DT1.Rows.Add("KPI.EquivalentsRate", "cond", "KPICode", _ent.KPICode);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("KPIEquivalentsRate", EntityCode + "" + KPICode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
