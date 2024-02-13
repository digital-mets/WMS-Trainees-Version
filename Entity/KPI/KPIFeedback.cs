using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class KPIFeedback
    {

       public virtual string Name { get; set; }
        public virtual string Company { get; set; }
        public virtual string Feedback { get; set; }
        public virtual string Action { get; set; }
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
        public DataTable getdata(string Name)
        {
            DataTable a;

  
            a = Gears.RetriveData2("select * from KPI.Feedback where RecordID = '" + Name + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                Name = dtRow["Name"].ToString();
                Company = dtRow["Company"].ToString();
                Feedback = dtRow["Feedback"].ToString();
                Action = dtRow["Action"].ToString();
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
        public void InsertData(KPIFeedback _ent)
        {
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("KPI.Feedback", "0", "Name", _ent.Name);
            DT1.Rows.Add("KPI.Feedback", "0", "Company", _ent.Company);
            DT1.Rows.Add("KPI.Feedback", "0", "Feedback", _ent.Feedback);
            DT1.Rows.Add("KPI.Feedback", "0", "Action", _ent.Action);
            DT1.Rows.Add("KPI.Feedback", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Feedback", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Feedback", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Feedback", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Feedback", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Feedback", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Feedback", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Feedback", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Feedback", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Feedback", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("KPI.Feedback", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1);
        }
        public void UpdateData(KPIFeedback _ent)
        {
         

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Feedback", "cond", "Name", _ent.Name);
            DT1.Rows.Add("KPI.Feedback", "set", "Company", _ent.Company);
            DT1.Rows.Add("KPI.Feedback", "set", "Feedback", _ent.Feedback);
            DT1.Rows.Add("KPI.Feedback", "set", "Action", _ent.Action);
            DT1.Rows.Add("KPI.Feedback", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Feedback", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Feedback", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Feedback", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Feedback", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Feedback", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Feedback", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Feedback", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Feedback", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Feedback", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("KPI.Feedback", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1);

            Functions.AuditTrail("KPIFeedback", Name, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(KPIFeedback _ent)
        {
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Feedback", "cond", "Name", _ent.Name);
            DT1.Rows.Add("KPI.Feedback", "cond", "Company", _ent.Company);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("KPIFeedback", Name, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
