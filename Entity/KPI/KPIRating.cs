using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class KPIRating
    {

       public virtual decimal PercentageCriteria { get; set; }
        public virtual string RatingDescription { get; set; }
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
        public DataTable getdata(string PercentageCriteria)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from KPI.Rating where PercentageCriteria = '" + PercentageCriteria + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                PercentageCriteria = dtRow["PercentageCriteria"].ToString();
                RatingDescription = dtRow["RatingDescription"].ToString();
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
        public void InsertData(KPIRating _ent)
        {
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("KPI.Rating", "0", "PercentageCriteria", _ent.PercentageCriteria);
            DT1.Rows.Add("KPI.Rating", "0", "RatingDescription", _ent.RatingDescription);
        
            DT1.Rows.Add("KPI.Rating", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Rating", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Rating", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Rating", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Rating", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Rating", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Rating", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Rating", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Rating", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Rating", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("KPI.Rating", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1);
        }
        public void UpdateData(KPIRating _ent)
        {
         

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Rating", "cond", "PercentageCriteria", _ent.PercentageCriteria);
            DT1.Rows.Add("KPI.Rating", "set", "RatingDescription", _ent.RatingDescription);

            DT1.Rows.Add("KPI.Rating", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Rating", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Rating", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Rating", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Rating", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Rating", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Rating", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Rating", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Rating", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Rating", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("KPI.Rating", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1);

            Functions.AuditTrail("KPIRating", Convert.ToInt32(PercentageCriteria).ToString(), LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(KPIRating _ent)
        {
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Rating", "cond", "PercentageCriteria", _ent.PercentageCriteria);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("KPIRating", Convert.ToInt32(PercentageCriteria).ToString(), LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
