using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BizAccount
    {

        private static string ID;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string BizAccountCode { get; set; }
        public virtual string BizAccountName { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string TIN { get; set; }
        public virtual string Address { get; set; }
        public virtual decimal TotalCreditLimit { get; set; }
        public virtual decimal TotalARBalance { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public DataTable getdata(string BizAccount, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from Masterfile.BizAccount where BizAccountCode = '" + BizAccount + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                BizAccountCode = dtRow["BizAccountCode"].ToString();
                BizAccountName = dtRow["BizAccountName"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                CustomerName = dtRow["CustomerName"].ToString();
                TIN = dtRow["TIN"].ToString();
                Address = dtRow["Address"].ToString();
                TotalCreditLimit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCreditLimit"]) ? 0 : dtRow["TotalCreditLimit"]);
                TotalARBalance = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalARBalance"]) ? 0 : dtRow["TotalARBalance"]);
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                DeactivatedDate = dtRow["DeactivatedDate"].ToString();

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
        public void InsertData(BizAccount _ent)
        {
            ID = _ent.BizAccountCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.BizAccount", "0", "BizAccountCode", _ent.BizAccountCode);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "BizAccountName", _ent.BizAccountName);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "CustomerName", _ent.CustomerName);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "TIN", _ent.TIN);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "TotalCreditLimit", _ent.TotalCreditLimit);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "TotalARBalance", _ent.TotalARBalance);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.BizAccount", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.BizAccount", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
          


            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(BizAccount _ent)
        {
            ID = _ent.BizAccountCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.BizAccount", "cond", "BizAccountCode", ID);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "BizAccountName", _ent.BizAccountName);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "CustomerName", _ent.CustomerName);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "TIN", _ent.TIN);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "TotalCreditLimit", _ent.TotalCreditLimit);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "TotalARBalance", _ent.TotalARBalance);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "Field9", _ent.Field9);
            
            DT1.Rows.Add("Masterfile.BizAccount", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.BizAccount", "set", "LastEditedDate", _ent.LastEditedDate);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFBIZACC", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

        }
        public void DeleteData(BizAccount _ent)
        {
            ID = _ent.BizAccountCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BizAccount", "cond", "BizAccountCode", ID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFBIZACC", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
