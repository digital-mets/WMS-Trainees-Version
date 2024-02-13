using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class FSData
    {
        private string _TableName = "Enterprise.FSData";

        public virtual Int64 RecordID { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual decimal Asset { get; set; }
        public virtual decimal CurrentAsset { get; set; }
        public virtual decimal AR_Trade { get; set; }
        public virtual decimal Inventory { get; set; }
        public virtual decimal PrepaidExp { get; set; }
        public virtual decimal Liability { get; set; }
        public virtual decimal CurrentLiability { get; set; }
        public virtual decimal AP_Trade { get; set; }
        public virtual decimal BankLoan { get; set; }
        public virtual decimal PrivateLoan { get; set; }
        public virtual decimal Equity { get; set; }
        public virtual decimal CommonStock { get; set; }
        public virtual decimal PreferredStock { get; set; }
        public virtual decimal RE_Appropriated { get; set; }
        public virtual decimal RE_YTD { get; set; }
        public virtual decimal GrossSales { get; set; }
        public virtual decimal NetSales { get; set; }
        public virtual decimal COGS { get; set; }
        public virtual decimal Expenses { get; set; }
        public virtual decimal NetIncome { get; set; }
        public virtual decimal NetPurchase { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public int GetObject(string _RecordID)
        {
            DataTable dtResult;

            dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName+" WHERE RecordID = " + _RecordID);

            if (dtResult.Rows.Count == 0)
                return -1;      // No record found
            else if (dtResult.Rows.Count > 1)
                return 1;       // duplicate record found
            else
            {
                DataRow _Row = dtResult.Rows[0];

                RecordID = Convert.ToInt64(_Row["RecordID"]);
                CompanyCode = _Row["CompanyCode"].ToString();
                Year = Convert.ToInt16(_Row["Year"]);
                Month = Convert.ToInt16(_Row["Month"]);
                Asset = Convert.ToDecimal(_Row["Asset"]);
                CurrentAsset = Convert.ToDecimal(_Row["CurrentAsset"]);
                AR_Trade = Convert.ToDecimal(_Row["AR_Trade"]);
                Inventory = Convert.ToDecimal(_Row["Inventory"]);
                PrepaidExp = Convert.ToDecimal(_Row["PrepaidExp"]);
                Liability = Convert.ToDecimal(_Row["Liability"]);
                CurrentLiability = Convert.ToDecimal(_Row["CurrentLiability"]);
                AP_Trade = Convert.ToDecimal(_Row["AP_Trade"]);
                BankLoan = Convert.ToDecimal(_Row["BankLoan"]);
                PrivateLoan = Convert.ToDecimal(_Row["PrivateLoan"]);
                Equity = Convert.ToDecimal(_Row["Equity"]);
                CommonStock = Convert.ToDecimal(_Row["CommonStock"]);
                PreferredStock = Convert.ToDecimal(_Row["PreferredStock"]);
                RE_Appropriated = Convert.ToDecimal(_Row["RE_Appropriated"]);
                RE_YTD = Convert.ToDecimal(_Row["RE_YTD"]);
                GrossSales = Convert.ToDecimal(_Row["GrossSales"]);
                NetSales = Convert.ToDecimal(_Row["NetSales"]);
                COGS = Convert.ToDecimal(_Row["COGS"]);
                Expenses = Convert.ToDecimal(_Row["Expenses"]);
                NetIncome = Convert.ToDecimal(_Row["NetIncome"]);
                NetPurchase = Convert.ToDecimal(_Row["NetPurchase"]);

                Field1 = _Row["Field1"].ToString();
                Field2 = _Row["Field2"].ToString();
                Field3 = _Row["Field3"].ToString();
                Field4 = _Row["Field4"].ToString();
                Field5 = _Row["Field5"].ToString();
                Field6 = _Row["Field6"].ToString();
                Field7 = _Row["Field7"].ToString();
                Field8 = _Row["Field8"].ToString();
                Field9 = _Row["Field9"].ToString();

                AddedBy = _Row["AddedBy"].ToString();
                AddedDate = Convert.ToDateTime((_Row["AddedDate"] == DBNull.Value) ? null : _Row["AddedDate"]);
                LastEditedBy = (_Row["LastEditedDate"] == DBNull.Value) ? null :_Row["LastEditedBy"].ToString();
                LastEditedDate = Convert.ToDateTime((_Row["LastEditedDate"] == DBNull.Value) ? null : _Row["LastEditedDate"]);

                return 0;
            }
        }
  
        public string InsertData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "CompanyCode", CompanyCode);
            dtObject.Rows.Add(_TableName, "0", "Year", Year);
            dtObject.Rows.Add(_TableName, "0", "Month", Month);
            dtObject.Rows.Add(_TableName, "0", "Asset", Asset);
            dtObject.Rows.Add(_TableName, "0", "CurrentAsset", CurrentAsset);
            dtObject.Rows.Add(_TableName, "0", "AR_Trade", AR_Trade);
            dtObject.Rows.Add(_TableName, "0", "Inventory", Inventory);
            dtObject.Rows.Add(_TableName, "0", "PrepaidExp", PrepaidExp);
            dtObject.Rows.Add(_TableName, "0", "Liability", Liability);
            dtObject.Rows.Add(_TableName, "0", "CurrentLiability", CurrentLiability);
            dtObject.Rows.Add(_TableName, "0", "AP_Trade", AP_Trade);
            dtObject.Rows.Add(_TableName, "0", "BankLoan", BankLoan);
            dtObject.Rows.Add(_TableName, "0", "PrivateLoan", PrivateLoan);
            dtObject.Rows.Add(_TableName, "0", "Equity", Equity);
            dtObject.Rows.Add(_TableName, "0", "CommonStock", CommonStock);
            dtObject.Rows.Add(_TableName, "0", "PreferredStock", PreferredStock);
            dtObject.Rows.Add(_TableName, "0", "RE_Appropriated", RE_Appropriated);
            dtObject.Rows.Add(_TableName, "0", "RE_YTD", RE_YTD);
            dtObject.Rows.Add(_TableName, "0", "GrossSales", GrossSales);
            dtObject.Rows.Add(_TableName, "0", "NetSales", NetSales);
            dtObject.Rows.Add(_TableName, "0", "COGS", COGS);
            dtObject.Rows.Add(_TableName, "0", "Expenses", Expenses);
            dtObject.Rows.Add(_TableName, "0", "NetIncome", NetIncome);
            dtObject.Rows.Add(_TableName, "0", "NetPurchase", NetPurchase);

            dtObject.Rows.Add(_TableName, "0", "Field1", Field1);
            dtObject.Rows.Add(_TableName, "0", "Field2", Field2);
            dtObject.Rows.Add(_TableName, "0", "Field3", Field3);
            dtObject.Rows.Add(_TableName, "0", "Field4", Field4);
            dtObject.Rows.Add(_TableName, "0", "Field5", Field5);
            dtObject.Rows.Add(_TableName, "0", "Field6", Field6);
            dtObject.Rows.Add(_TableName, "0", "Field7", Field7);
            dtObject.Rows.Add(_TableName, "0", "Field8", Field8);
            dtObject.Rows.Add(_TableName, "0", "Field9", Field9);

            dtObject.Rows.Add(_TableName, "0", "AddedBy", AddedBy);
            dtObject.Rows.Add(_TableName, "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return Gears.CreateData(dtObject);
        }
        
        public string UpdateData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "CompanyCode", CompanyCode);
            dtObject.Rows.Add(_TableName, "cond", "Year", Year);
            dtObject.Rows.Add(_TableName, "cond", "Month", Month);

            dtObject.Rows.Add(_TableName, "set", "Asset", Asset);
            dtObject.Rows.Add(_TableName, "set", "CurrentAsset", CurrentAsset);
            dtObject.Rows.Add(_TableName, "set", "AR_Trade", AR_Trade);
            dtObject.Rows.Add(_TableName, "set", "Inventory", Inventory);
            dtObject.Rows.Add(_TableName, "set", "PrepaidExp", PrepaidExp);
            dtObject.Rows.Add(_TableName, "set", "Liability", Liability);
            dtObject.Rows.Add(_TableName, "set", "CurrentLiability", CurrentLiability);
            dtObject.Rows.Add(_TableName, "set", "AP_Trade", AP_Trade);
            dtObject.Rows.Add(_TableName, "set", "BankLoan", BankLoan);
            dtObject.Rows.Add(_TableName, "set", "PrivateLoan", PrivateLoan);
            dtObject.Rows.Add(_TableName, "set", "Equity", Equity);
            dtObject.Rows.Add(_TableName, "set", "CommonStock", CommonStock);
            dtObject.Rows.Add(_TableName, "set", "PreferredStock", PreferredStock);
            dtObject.Rows.Add(_TableName, "set", "RE_Appropriated", RE_Appropriated);
            dtObject.Rows.Add(_TableName, "set", "RE_YTD", RE_YTD);
            dtObject.Rows.Add(_TableName, "set", "GrossSales", GrossSales);
            dtObject.Rows.Add(_TableName, "set", "NetSales", NetSales);
            dtObject.Rows.Add(_TableName, "set", "COGS", COGS);
            dtObject.Rows.Add(_TableName, "set", "Expenses", Expenses);
            dtObject.Rows.Add(_TableName, "set", "NetIncome", NetIncome);
            dtObject.Rows.Add(_TableName, "set", "NetPurchase", NetPurchase);

            dtObject.Rows.Add(_TableName, "set", "Field1", Field1);
            dtObject.Rows.Add(_TableName, "set", "Field2", Field2);
            dtObject.Rows.Add(_TableName, "set", "Field3", Field3);
            dtObject.Rows.Add(_TableName, "set", "Field4", Field4);
            dtObject.Rows.Add(_TableName, "set", "Field5", Field5);
            dtObject.Rows.Add(_TableName, "set", "Field6", Field6);
            dtObject.Rows.Add(_TableName, "set", "Field7", Field7);
            dtObject.Rows.Add(_TableName, "set", "Field8", Field8);
            dtObject.Rows.Add(_TableName, "set", "Field9", Field9);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", LastEditedBy);
            LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("FSData", CompanyCode + ";" + Year.ToString() + ";" + Month.ToString(),
                                     LastEditedBy, ((DateTime)LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }
        
        public string DeleteData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "CompanyCode", CompanyCode);
            dtObject.Rows.Add(_TableName, "cond", "Year", Year);
            dtObject.Rows.Add(_TableName, "cond", "Month", Month);
        
            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("FSData", CompanyCode + ";" + Year.ToString() + ";" + Month.ToString(),
                                     LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }
}
