using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CollectionReceipt
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string ReceiptType { get; set; }     // Acknowledgement Receipt, Collection Receipt, Collection Receipt (Maturing Checks)
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string BizAccount { get; set; }
        public virtual string Collector { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal TotalCashAmount { get; set; }
        public virtual decimal TotalBankCredit { get; set; }
        public virtual decimal TotalCheckAmount { get; set; }
        public virtual decimal CashAmount { get; set; }
        public virtual string RefARChecks { get; set; }
        public virtual string CollectionType { get; set; }
        public virtual bool IsForDeposit { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual IList<CollectionCredit> Credit { get; set; }
        public virtual IList<CollectionChecks> Checks { get; set; }
        public virtual IList<CollectionChecks> ARChecks { get; set; }

        #region Journal Entry
        public class JournalEntry
        {
            //public virtual CollectionReceipt Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType LIKE 'ACT_RT' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        #endregion

        #region Header
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Accounting.Collection WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Name = dtRow["Name"].ToString();
                BizAccount = dtRow["BizAccount"].ToString();
                Collector = dtRow["Collector"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Currency = Convert.IsDBNull(dtRow["Currency"]) || String.IsNullOrEmpty(dtRow["Currency"].ToString()) ? "PHP" : dtRow["Currency"].ToString();
                TotalCashAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCashAmount"]) ? 0 : dtRow["TotalCashAmount"]);
                TotalBankCredit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBankCredit"]) ? 0 : dtRow["TotalBankCredit"]);
                TotalCheckAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCheckAmount"]) ? 0 : dtRow["TotalCheckAmount"]);
                CashAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CashAmount"]) ? 0 : dtRow["CashAmount"]);
                RefARChecks = dtRow["RefARChecks"].ToString();
                CollectionType = dtRow["CollectionType"].ToString();
                ReceiptType = dtRow["ReceiptType"].ToString();
                IsForDeposit = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsForDeposit"]) ? false : dtRow["IsForDeposit"]);

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
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
            }

            return a;
        }
        public void InsertData(CollectionReceipt _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.Collection", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.Collection", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.Collection", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.Collection", "0", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.Collection", "0", "Collector", _ent.Collector);
            DT1.Rows.Add("Accounting.Collection", "0", "BizAccount", _ent.BizAccount);
            DT1.Rows.Add("Accounting.Collection", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.Collection", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Accounting.Collection", "0", "TotalCashAmount", _ent.TotalCashAmount);
            DT1.Rows.Add("Accounting.Collection", "0", "TotalBankCredit", _ent.TotalBankCredit);
            DT1.Rows.Add("Accounting.Collection", "0", "TotalCheckAmount", _ent.TotalCheckAmount);
            DT1.Rows.Add("Accounting.Collection", "0", "CashAmount", _ent.CashAmount);
            DT1.Rows.Add("Accounting.Collection", "0", "RefARChecks", _ent.RefARChecks);
            DT1.Rows.Add("Accounting.Collection", "0", "CollectionType", _ent.CollectionType);
            DT1.Rows.Add("Accounting.Collection", "0", "ReceiptType", _ent.ReceiptType);

            DT1.Rows.Add("Accounting.Collection", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.Collection", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.Collection", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.Collection", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.Collection", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.Collection", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.Collection", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.Collection", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.Collection", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.Collection", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.Collection", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(CollectionReceipt _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Accounting.Collection", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.Collection", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.Collection", "set", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.Collection", "set", "Collector", _ent.Collector);
            DT1.Rows.Add("Accounting.Collection", "set", "BizAccount", _ent.BizAccount);
            DT1.Rows.Add("Accounting.Collection", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.Collection", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Accounting.Collection", "set", "TotalCashAmount", _ent.TotalCashAmount);
            DT1.Rows.Add("Accounting.Collection", "set", "TotalBankCredit", _ent.TotalBankCredit);
            DT1.Rows.Add("Accounting.Collection", "set", "TotalCheckAmount", _ent.TotalCheckAmount);
            DT1.Rows.Add("Accounting.Collection", "set", "CashAmount", _ent.CashAmount);
            DT1.Rows.Add("Accounting.Collection", "set", "RefARChecks", _ent.RefARChecks);
            DT1.Rows.Add("Accounting.Collection", "set", "CollectionType", _ent.CollectionType);
            DT1.Rows.Add("Accounting.Collection", "set", "ReceiptType", _ent.ReceiptType);

            DT1.Rows.Add("Accounting.Collection", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.Collection", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.Collection", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.Collection", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.Collection", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.Collection", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.Collection", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.Collection", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.Collection", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.Collection", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.Collection", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail(_ent.ReceiptType == "A" ? "ACTART" : "ACTCRT", 
                                 Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CollectionReceipt _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.CollectionCredit", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DT3.Rows.Add("Accounting.CollectionChecks", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT3, _ent.Connection);

            Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
            DT4.Rows.Add("Accounting.CollectionARChecks", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT4, _ent.Connection);

            Functions.AuditTrail(_ent.ReceiptType == "A" ? "ACTART" : "ACTCRT", 
                                 Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        #endregion

        #region Credit
        public class CollectionCredit
        {
            //public virtual Collection Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ReferenceNo { get; set; }
            public virtual string BankAccountCode { get; set; }
            public virtual DateTime DateDeposited { get; set; }
            public virtual decimal AmountDeposited { get; set; }

            public DataTable getCredit(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CollectionCredit WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCollectionCredit(CollectionCredit CollectionCredit)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CollectionCredit WHERE DocNumber = '" + Docnum + "'", Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionCredit", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionCredit", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CollectionCredit", "0", "ReferenceNo", CollectionCredit.ReferenceNo);
                DT1.Rows.Add("Accounting.CollectionCredit", "0", "BankAccountCode", CollectionCredit.BankAccountCode);
                DT1.Rows.Add("Accounting.CollectionCredit", "0", "DateDeposited", CollectionCredit.DateDeposited);
                DT1.Rows.Add("Accounting.CollectionCredit", "0", "AmountDeposited", CollectionCredit.AmountDeposited);

                DT2.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.Collection", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCollectionCredit(CollectionCredit CollectionCredit)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.CollectionCredit", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionCredit", "cond", "LineNumber", CollectionCredit.LineNumber);
                DT1.Rows.Add("Accounting.CollectionCredit", "set", "ReferenceNo", CollectionCredit.ReferenceNo);
                DT1.Rows.Add("Accounting.CollectionCredit", "set", "BankAccountCode", CollectionCredit.BankAccountCode);
                DT1.Rows.Add("Accounting.CollectionCredit", "set", "DateDeposited", CollectionCredit.DateDeposited);
                DT1.Rows.Add("Accounting.CollectionCredit", "set", "AmountDeposited", CollectionCredit.AmountDeposited);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteCollectionCredit(CollectionCredit CollectionCredit)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionCredit", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionCredit", "cond", "LineNumber", CollectionCredit.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Application = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Credit = Gears.RetriveData2("SELECT * FROM Accounting.CollectionCredit WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Checks = Gears.RetriveData2("SELECT * FROM Accounting.CollectionChecks WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Loan = Gears.RetriveData2("SELECT * FROM Accounting.CollectionLoan WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((Application.Rows.Count < 1) && (Adjustment.Rows.Count < 1) && (Credit.Rows.Count < 1) && (Checks.Rows.Count < 1) && (Loan.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.Collection", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        #endregion

        #region Checks
        public class CollectionChecks
        {
            //public virtual Collection Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Bank { get; set; }
            public virtual string Branch { get; set; }
            public virtual string CheckNumber { get; set; }
            public virtual Nullable<DateTime> CheckDate { get; set; }
            public virtual decimal CheckAmount { get; set; }

            public DataTable getChecks(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CollectionChecks WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCollectionChecks(CollectionChecks CollectionChecks)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CollectionChecks WHERE DocNumber = '" + Docnum + "'", Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionChecks", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionChecks", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CollectionChecks", "0", "Bank", CollectionChecks.Bank);
                DT1.Rows.Add("Accounting.CollectionChecks", "0", "Branch", CollectionChecks.Branch);
                DT1.Rows.Add("Accounting.CollectionChecks", "0", "CheckNumber", CollectionChecks.CheckNumber);
                DT1.Rows.Add("Accounting.CollectionChecks", "0", "CheckDate", CollectionChecks.CheckDate);
                DT1.Rows.Add("Accounting.CollectionChecks", "0", "CheckAmount", CollectionChecks.CheckAmount);

                DT2.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.Collection", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCollectionChecks(CollectionChecks CollectionChecks)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.CollectionChecks", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionChecks", "cond", "LineNumber", CollectionChecks.LineNumber);
                DT1.Rows.Add("Accounting.CollectionChecks", "set", "Bank", CollectionChecks.Bank);
                DT1.Rows.Add("Accounting.CollectionChecks", "set", "Branch", CollectionChecks.Branch);
                DT1.Rows.Add("Accounting.CollectionChecks", "set", "CheckNumber", CollectionChecks.CheckNumber);
                DT1.Rows.Add("Accounting.CollectionChecks", "set", "CheckDate", CollectionChecks.CheckDate);
                DT1.Rows.Add("Accounting.CollectionChecks", "set", "CheckAmount", CollectionChecks.CheckAmount);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteCollectionChecks(CollectionChecks CollectionChecks)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionChecks", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionChecks", "cond", "LineNumber", CollectionChecks.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Application = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Credit = Gears.RetriveData2("SELECT * FROM Accounting.CollectionCredit WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Checks = Gears.RetriveData2("SELECT * FROM Accounting.CollectionChecks WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Loan = Gears.RetriveData2("SELECT * FROM Accounting.CollectionLoan WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((Application.Rows.Count < 1) && (Adjustment.Rows.Count < 1) && (Credit.Rows.Count < 1) && (Checks.Rows.Count < 1) && (Loan.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.Collection", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        #endregion

        #region ARChecks
        public class CollectionARChecks
        {
            //public virtual Collection Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string RecordID { get; set; }
            public virtual string RefARNumber { get; set; }
            public virtual string RefARLineNum { get; set; }
            public virtual string Bank { get; set; }
            public virtual string Branch { get; set; }
            public virtual string CheckNumber { get; set; }
            public virtual Nullable<DateTime> CheckDate { get; set; }
            public virtual decimal CheckAmount { get; set; }

            public DataTable getARChecks(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT RefARNumber+'/'+RefARLineNum AS RecordID, CHK.* "+
                                           "  FROM Accounting.CollectionARChecks CHK " +
                                           "       LEFT JOIN Accounting.CollectionChecks REF " +
                                           "       ON CHK.RefARNumber = REF.DocNumber AND CHK.RefARLineNum = REF.LineNumber" +
                                           " WHERE CHK.DocNumber='" + DocNumber + "' ORDER BY CHK.LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCollectionARChecks(CollectionARChecks _CollectionARChecks)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CollectionARChecks WHERE DocNumber = '" + Docnum + "'", Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "LineNumber", strLine);
                int intIndex = _CollectionARChecks.RecordID.IndexOf('/');
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "RefARNumber", _CollectionARChecks.RecordID.Substring(0,intIndex));
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "RefARLineNum", _CollectionARChecks.RecordID.Substring(intIndex+1));
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "Bank", _CollectionARChecks.Bank);
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "Branch", _CollectionARChecks.Branch);
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "CheckNumber", _CollectionARChecks.CheckNumber);
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "CheckDate", _CollectionARChecks.CheckDate);
                DT1.Rows.Add("Accounting.CollectionARChecks", "0", "CheckAmount", _CollectionARChecks.CheckAmount);

                DT2.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.Collection", "set", "IsWithDetail", "True");

                string strMessage;
                strMessage = Gears.CreateData(DT1, Conn);
                strMessage = Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCollectionChecks(CollectionARChecks _CollectionARChecks)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.CollectionARChecks", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionARChecks", "cond", "LineNumber", _CollectionARChecks.LineNumber);
                int intIndex = _CollectionARChecks.RecordID.IndexOf('/');
                DT1.Rows.Add("Accounting.CollectionARChecks", "set", "RefARNumber", _CollectionARChecks.RecordID.Substring(0, intIndex));
                DT1.Rows.Add("Accounting.CollectionARChecks", "set", "RefARLineNum", _CollectionARChecks.RecordID.Substring(intIndex+1));
                DT1.Rows.Add("Accounting.CollectionARChecks", "set", "Bank", _CollectionARChecks.Bank);
                DT1.Rows.Add("Accounting.CollectionARChecks", "set", "Branch", _CollectionARChecks.Branch);
                DT1.Rows.Add("Accounting.CollectionARChecks", "set", "CheckNumber", _CollectionARChecks.CheckNumber);
                DT1.Rows.Add("Accounting.CollectionARChecks", "set", "CheckDate", _CollectionARChecks.CheckDate);
                DT1.Rows.Add("Accounting.CollectionARChecks", "set", "CheckAmount", _CollectionARChecks.CheckAmount);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteCollectionChecks(CollectionARChecks _CollectionARChecks)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionARChecks", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionARChecks", "cond", "LineNumber", _CollectionARChecks.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Credit = Gears.RetriveData2("SELECT * FROM Accounting.CollectionCredit WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Checks = Gears.RetriveData2("SELECT * FROM Accounting.CollectionChecks WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable ARChecks = Gears.RetriveData2("SELECT * FROM Accounting.CollectionARChecks WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((Credit.Rows.Count < 1) && (Checks.Rows.Count < 1) && (ARChecks.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.Collection", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.Collection", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        #endregion

        public void DeleteOldDetail(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.CollectionCredit WHERE DocNumber = '" + DocNumber + "';" +
                                   "DELETE FROM Accounting.CollectionChecks WHERE DocNumber = '" + DocNumber + "';" +
                                   "DELETE FROM Accounting.CollectionARChecks WHERE DocNumber = '" + DocNumber + "';" 
                                  , Conn);
        }
    }
}
