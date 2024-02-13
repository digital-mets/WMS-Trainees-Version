using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class PMCompany
    {

       public virtual string CompanyCode { get; set; }
        public virtual string CompanyDescription { get; set; }
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
        public DataTable getdata(string CompanyCode)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from KPI.Company where CompanyCode = '" + CompanyCode + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                CompanyCode = dtRow["CompanyCode"].ToString();
                CompanyDescription = dtRow["CompanyDescription"].ToString();
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
        public void InsertData(PMCompany _ent)
        {
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("KPI.Company", "0", "CompanyCode", _ent.CompanyCode);
            DT1.Rows.Add("KPI.Company", "0", "CompanyDescription", _ent.CompanyDescription);
        
            DT1.Rows.Add("KPI.Company", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Company", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Company", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Company", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Company", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Company", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Company", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Company", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Company", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Company", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("KPI.Company", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1);
        }
        public void UpdateData(PMCompany _ent)
        {
         

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Company", "cond", "CompanyCode", _ent.CompanyCode);
            DT1.Rows.Add("KPI.Company", "cond", "CompanyDescription", _ent.CompanyDescription);

            DT1.Rows.Add("KPI.Company", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Company", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Company", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Company", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Company", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Company", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Company", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Company", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Company", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Company", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("KPI.Company", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1);

            Functions.AuditTrail("PMCOM", CompanyCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(PMCompany _ent)
        {
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Company", "cond", "CompanyCode", _ent.CompanyCode);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("PMCOM", CompanyCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
