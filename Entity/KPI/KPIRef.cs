using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class KPIRef
    {

       public virtual string KPICode { get; set; }
        public virtual string Description { get; set; }
        public virtual string Formula { get; set; }
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
        public DataTable getdata(string KPICode)
        {
            DataTable a;

  
            a = Gears.RetriveData2("select * from KPI.Reference where KPICode = '" + KPICode + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                KPICode = dtRow["KPICode"].ToString();
                Description = dtRow["Description"].ToString();
                Formula = dtRow["Formula"].ToString();
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
        public void InsertData(KPIRef _ent)
        {
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("KPI.Reference", "0", "KPICode", _ent.KPICode);
            DT1.Rows.Add("KPI.Reference", "0", "Description", _ent.Description);
            DT1.Rows.Add("KPI.Reference", "0", "Formula", _ent.Formula);
            DT1.Rows.Add("KPI.Reference", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Reference", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Reference", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Reference", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Reference", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Reference", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Reference", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Reference", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Reference", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Reference", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("KPI.Reference", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1);
        }
        public void UpdateData(KPIRef _ent)
        {
         

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Reference", "cond", "KPICode", _ent.KPICode);
            DT1.Rows.Add("KPI.Reference", "set", "Description", _ent.Description);
            DT1.Rows.Add("KPI.Reference", "set", "Formula", _ent.Formula);
            DT1.Rows.Add("KPI.Reference", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("KPI.Reference", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("KPI.Reference", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("KPI.Reference", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("KPI.Reference", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("KPI.Reference", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("KPI.Reference", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("KPI.Reference", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("KPI.Reference", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("KPI.Reference", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("KPI.Reference", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1);

            Functions.AuditTrail("KPIREF", KPICode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(KPIRef _ent)
        {
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("KPI.Reference", "cond", "KPICode", _ent.KPICode);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("KPIREF", KPICode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
