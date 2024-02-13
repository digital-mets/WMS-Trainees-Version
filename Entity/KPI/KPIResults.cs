using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class KPIResults
    {
       public virtual string Company { get; set; }
      
       public virtual string EntityCode { get; set; }
        public virtual string EmployeeName { get; set; }
        public virtual string KPICode { get; set; }
        public virtual decimal Year { get; set; }
        public virtual decimal Month { get; set; }
        public virtual decimal Value { get; set; }
        public virtual string Rating1 { get; set; }
        public virtual decimal Rating { get; set; }
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
        public DataTable getdata(string RecID)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from KPI.Results A left join  KPI.Rating B on A.Rating=B.PercentageCriteria where RecordID = '" + RecID + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                Company = dtRow["Company"].ToString();
                EntityCode = dtRow["EntityCode"].ToString();
                EmployeeName = dtRow["EmployeeName"].ToString();
                KPICode = dtRow["KPICode"].ToString();
                Year = Convert.ToDecimal(Convert.IsDBNull(dtRow["Year"]) ? 0 : dtRow["Year"]);
                Month = Convert.ToDecimal(Convert.IsDBNull(dtRow["Month"]) ? 0 : dtRow["Month"]);
                Value = Convert.ToDecimal(Convert.IsDBNull(dtRow["Value"]) ? 0 : dtRow["Value"]); 
                Rating = Convert.ToDecimal(Convert.IsDBNull(dtRow["Rating"]) ? 0 : dtRow["Rating"]);
                Rating1 = dtRow["RatingDescription"].ToString();
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
        public void InsertData(KPIResults _ent)
        {
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Results", "0", "Company", _ent.Company);
            
            DT1.Rows.Add("KPI.Results", "0", "EntityCode", _ent.EntityCode);
            DT1.Rows.Add("KPI.Results", "0", "EmployeeName", _ent.EmployeeName);
            DT1.Rows.Add("KPI.Results", "0", "KPICode", _ent.KPICode);
            DT1.Rows.Add("KPI.Results", "0", "Year", _ent.Year);
            DT1.Rows.Add("KPI.Results", "0", "Month", _ent.Month);
            DT1.Rows.Add("KPI.Results", "0", "Value", _ent.Value);
            DT1.Rows.Add("KPI.Results", "0", "Rating", _ent.Rating);
            DT1.Rows.Add("KPI.Results", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Results", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Results", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Results", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Results", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Results", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Results", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Results", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Results", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Results", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("KPI.Results", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1);
        }
        public void UpdateData(KPIResults _ent)
        {


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable(); 
            DT1.Rows.Add("KPI.Results", "cond", "Company", _ent.Company);
            DT1.Rows.Add("KPI.Results", "cond", "EntityCode", _ent.EntityCode);
            DT1.Rows.Add("KPI.Results", "cond", "EmployeeName", _ent.EmployeeName);
            DT1.Rows.Add("KPI.Results", "cond", "KPICode", _ent.KPICode);
            DT1.Rows.Add("KPI.Results", "set", "Year", _ent.Year);
            DT1.Rows.Add("KPI.Results", "set", "Month", _ent.Month);
            DT1.Rows.Add("KPI.Results", "set", "Value", _ent.Value);
            DT1.Rows.Add("KPI.Results", "set", "Rating", _ent.Rating);

            DT1.Rows.Add("KPI.Results", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Results", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Results", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Results", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Results", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Results", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Results", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Results", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Results", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Results", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("KPI.Results", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1);

            Functions.AuditTrail("KPIResults", EntityCode + " " + EmployeeName + " " + KPICode + " " + Company, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(KPIResults _ent)
        {
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Results", "cond", "Company", _ent.Company);
            DT1.Rows.Add("KPI.Results", "cond", "EntityCode", _ent.EntityCode);
            DT1.Rows.Add("KPI.Results", "cond", "EmployeeName", _ent.EmployeeName);
            DT1.Rows.Add("KPI.Results", "cond", "KPICode", _ent.KPICode);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("KPIResults", EntityCode + " " + EmployeeName + " " + KPICode + " " + Company, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
