using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Currency
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        public virtual string CurrencyCurrency { get; set; }
        public virtual string CurrencyName { get; set; }
        public virtual string Symbol { get; set; }
        public virtual bool IsInactive { get; set; }
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
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public DataTable getdata(string MFCurrency, string Conn) //Ter
        {
            DataTable a;

            if (MFCurrency != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.Currency where Currency = '" + MFCurrency + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    CurrencyCurrency = dtRow["Currency"].ToString();
                    CurrencyName = dtRow["CurrencyName"].ToString();
                    Symbol = dtRow["Symbol"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();

                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn); //Ter
            }
            return a;
        }
        public void InsertData(Currency _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Currency", "0", "Currency", _ent.CurrencyCurrency);
            DT1.Rows.Add("Masterfile.Currency", "0", "CurrencyName", _ent.CurrencyName);
            DT1.Rows.Add("Masterfile.Currency", "0", "Symbol", _ent.Symbol);
            DT1.Rows.Add("Masterfile.Currency", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Currency", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Currency", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Currency", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Currency", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER

            Functions.AuditTrail("REFCURRENCY", _ent.CurrencyCurrency, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // Ter
        }

        public void UpdateData(Currency _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Currency", "cond", "Currency", _ent.CurrencyCurrency);
            DT1.Rows.Add("Masterfile.Currency", "set", "CurrencyName", _ent.CurrencyName);
            DT1.Rows.Add("Masterfile.Currency", "set", "Symbol", _ent.Symbol);
            DT1.Rows.Add("Masterfile.Currency", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Currency", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Currency", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Currency", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Currency", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter

            Functions.AuditTrail("REFCURRENCY", _ent.CurrencyCurrency, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter
        }
        public void DeleteData(Currency _ent)
        {
            Conn = _ent.Connection; //Ter
            CurrencyCurrency = _ent.CurrencyCurrency;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Currency", "cond", "Currency", _ent.CurrencyCurrency);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFCURRENCY", _ent.CurrencyCurrency, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
