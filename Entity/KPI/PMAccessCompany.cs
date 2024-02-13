using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data; 

namespace Entity
{
   public class PMAccessCompany
    {

       public virtual string CompanyID { get; set; }
        public virtual string CompanyDescription     { get; set; }      
        public virtual string CompanyGroupings { get; set; }
        public virtual string IndustryID { get; set; }
        public virtual string IndustryDescription { get; set; }
        public virtual decimal PercentageofOwnership { get; set; }
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
        public DataTable getdata(string RecordID)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from KPI.AccessCompany where RecordID = '" + RecordID + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                CompanyID = dtRow["CompanyID"].ToString();
                CompanyDescription = dtRow["CompanyDescription"].ToString();
                CompanyGroupings = dtRow["CompanyGroupings"].ToString();
                CompanyDescription = dtRow["CompanyDescription"].ToString();
                IndustryID = dtRow["IndustryID"].ToString();
                IndustryDescription = dtRow["IndustryDescription"].ToString();
                PercentageofOwnership = Convert.ToInt32(dtRow["PercentageofOwnership"]);
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
        public void InsertData(PMAccessCompany _ent)
        {
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("KPI.AccessCompany", "0", "CompanyID", _ent.CompanyID);
            DT1.Rows.Add("KPI.AccessCompany", "0", "CompanyDescription", _ent.CompanyDescription);
            DT1.Rows.Add("KPI.AccessCompany", "0", "CompanyGroupings", _ent.CompanyGroupings);
            DT1.Rows.Add("KPI.AccessCompany", "0", "IndustryID", _ent.IndustryID);
            DT1.Rows.Add("KPI.AccessCompany", "0", "IndustryDescription", _ent.IndustryDescription);
            DT1.Rows.Add("KPI.AccessCompany", "0", "PercentageofOwnership", _ent.PercentageofOwnership);

            DT1.Rows.Add("KPI.AccessCompany", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.AccessCompany", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.AccessCompany", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("KPI.AccessCompany", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1);
        }
        public void UpdateData(PMAccessCompany _ent)
        {
         

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.AccessCompany", "cond", "CompanyID", _ent.CompanyID);
            DT1.Rows.Add("KPI.AccessCompany", "cond", "IndustryID", _ent.IndustryID);


            DT1.Rows.Add("KPI.AccessCompany", "setf", "CompanyDescription", _ent.CompanyDescription);
            DT1.Rows.Add("KPI.AccessCompany", "set", "CompanyGroupings", _ent.CompanyGroupings);
            DT1.Rows.Add("KPI.AccessCompany", "set", "IndustryDescription", _ent.IndustryDescription);
            DT1.Rows.Add("KPI.AccessCompany", "set", "PercentageofOwnership", _ent.PercentageofOwnership);


            DT1.Rows.Add("KPI.AccessCompany", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.AccessCompany", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.AccessCompany", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("KPI.AccessCompany", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1);
            Functions.AuditTrail("PMAOC", CompanyID + " " + IndustryID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(PMAccessCompany _ent)
        {
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.AccessCompany", "cond", "CompanyID", _ent.CompanyID);
            DT1.Rows.Add("KPI.AccessCompany", "cond", "IndustryID", _ent.IndustryID);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("PMAOC", CompanyID + " " + IndustryID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
