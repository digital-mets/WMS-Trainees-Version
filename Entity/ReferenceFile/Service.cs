using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Service
    {

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string ServiceCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual string AccountCode { get; set; }
        public virtual string SubsiCode { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string VATCode { get; set; }
        public virtual string ATCCode { get; set; }
        public virtual bool IsVatable { get; set; }
        public virtual bool IsCore { get; set; }
        public virtual bool IsAllowBilling { get; set; }
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
        public DataTable getdata(string Code, string Conn)
        {
            DataTable a;
            a = Gears.RetriveData2("SELECT * FROM Masterfile.Service WHERE ServiceCode = '" + Code + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                ServiceCode = dtRow["ServiceCode"].ToString();
                Description = dtRow["Description"].ToString();
                Type = dtRow["Type"].ToString();
                AccountCode = dtRow["AccountCode"].ToString();
                SubsiCode = dtRow["SubsiCode"].ToString();
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                VATCode = dtRow["VATCode"].ToString();
                ATCCode = dtRow["ATCCode"].ToString();
                IsVatable = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVatable"]) ? false : dtRow["IsVatable"]);
                IsCore = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsCore"]) ? false : dtRow["IsCore"]);
                IsAllowBilling = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsAllowBilling"]) ? false : dtRow["IsAllowBilling"]);
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeActivatedBy = dtRow["DeactivatedBy"].ToString();
                DeActivatedDate = dtRow["DeactivatedDate"].ToString();
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
        public void InsertData(Service _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Service", "0", "ServiceCode", _ent.ServiceCode);
            DT1.Rows.Add("Masterfile.Service", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Service", "0", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.Service", "0", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Masterfile.Service", "0", "SubsiCode", _ent.SubsiCode);
            DT1.Rows.Add("Masterfile.Service", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Service", "0", "VATCode", _ent.VATCode);
            DT1.Rows.Add("Masterfile.Service", "0", "ATCCode", _ent.ATCCode);
            DT1.Rows.Add("Masterfile.Service", "0", "IsVatable", _ent.IsVatable);
            DT1.Rows.Add("Masterfile.Service", "0", "IsCore", _ent.IsCore);
            DT1.Rows.Add("Masterfile.Service", "0", "IsAllowBilling", _ent.IsAllowBilling);
            DT1.Rows.Add("Masterfile.Service", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Service", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Service", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Service", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Service", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Service", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Service", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Service", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Service", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Service", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Service", "0", "Field9", _ent.Field9);
            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(Service _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Service", "cond", "ServiceCode", _ent.ServiceCode);
            DT1.Rows.Add("Masterfile.Service", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Service", "set", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.Service", "set", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Masterfile.Service", "set", "SubsiCode", _ent.SubsiCode);
            DT1.Rows.Add("Masterfile.Service", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Service", "set", "VATCode", _ent.VATCode);
            DT1.Rows.Add("Masterfile.Service", "set", "ATCCode", _ent.ATCCode);
            DT1.Rows.Add("Masterfile.Service", "set", "IsVatable", _ent.IsVatable);
            DT1.Rows.Add("Masterfile.Service", "set", "IsCore", _ent.IsCore);
            DT1.Rows.Add("Masterfile.Service", "set", "IsAllowBilling", _ent.IsAllowBilling);
            DT1.Rows.Add("Masterfile.Service", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Service", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Service", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Service", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Service", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Service", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Service", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Service", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Service", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Service", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Service", "set", "Field9", _ent.Field9);
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFSERV", _ent.ServiceCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(Service _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Service", "cond", "ServiceCode", _ent.ServiceCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFSERV", _ent.ServiceCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
