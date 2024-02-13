using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CollectionLiquidation
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual DateTime DocDate { get; set; }
        public virtual string RefNumber { get; set; }
        // Lookup field
        public virtual string CollectionType { get; set; }
        public virtual string Name { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string BizAccount { get; set; }
        public virtual string Collector { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal CashAmount { get; set; }
        public virtual decimal TotalCashAmount { get; set; }
        public virtual decimal TotalBankCredit { get; set; }
        public virtual decimal TotalCheckAmount { get; set; }
        // End of lookup field
        public virtual decimal TotalAmountDue { get; set; }
        public virtual decimal TotalApplied { get; set; }
        public virtual decimal TotalAdjustment { get; set; }
        public virtual decimal Variance { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string RefTransDoc { get; set; }
        public virtual string RefLoans { get; set; }
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
        public virtual IList<CollectionApplication> Application { get; set; }
        public virtual IList<CollectionAdjustment> Adjustment { get; set; }
        public virtual IList<CollectionLoan> Loan { get; set; }

        #region Journal Entry
        public class JournalEntry
        {
            public virtual CollectionApplication Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit, A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCLQ' "
                    + " ORDER BY A.DebitAmount DESC, A.AccountCode ASC, A.SubsiCode ASC, A.ProfitCenterCode ASC, A.CostCenterCode ASC", Conn);

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

        #region Reference Transactions
        public class RefTransaction
        {
            public virtual CollectionLiquidation Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B on A.RMenuID = B.ModuleID inner join IT.MainMenu C on A.MenuID = C.ModuleID "
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='ACTCLQ' OR  A.TransType='ACTCLQ') ", Conn);
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
            a = Gears.RetriveData2(
                "WITH LST AS " +
                "( " +
                "SELECT Split.a.value('.', 'VARCHAR(20)') AS DocNumber " +
                "  FROM (SELECT CAST ('<M>' + REPLACE(RefNumber, ';', '</M><M>') + '</M>' AS XML) AS String " +
                "          FROM Accounting.CollectionLiquidation WHERE DocNumber = '" + DocNumber + "') AS A " +
                " CROSS APPLY String.nodes ('/M') AS Split(a) " +
                ") " +
                ", CR AS " +
                "( " +
                "SELECT '" + DocNumber + "' AS DocNumber, " +
                "       MAX(Collector) AS Collector, MAX(Currency) AS Currency, " +
                "       MAX(CollectionType) AS CollectionType, MAX(Name) AS Name, " +
                "       MAX(CustomerCode) AS CustomerCode, MAX(BizAccount) AS BizAccount, " +
                "       SUM(CashAmount) AS CashAmount, " +
                "       SUM(TotalCashAmount) AS TotalCashAmount, " +
                "       SUM(TotalBankCredit) AS TotalBankCredit, " +
                "       SUM(TotalCheckAmount) AS TotalCheckAmount " +
                "  FROM LST INNER JOIN Accounting.Collection CR ON LST.DocNumber = CR.DocNumber " +
                ") " +
                "SELECT LIQ.*, COALESCE(CR.CollectionType, LIQ.CollectionType) AS CollType, " +
                "       COALESCE(CR.Name, LIQ.Name) AS CustName, " +
                "       COALESCE(CR.CustomerCode, LIQ.CustomerCode) AS CustCode, " +
                "       COALESCE(CR.BizAccount, LIQ.BizAccount) AS BizAcct, Collector, " +
                "       Currency, CashAmount, TotalCashAmount, TotalBankCredit, TotalCheckAmount " +
                "  FROM Accounting.CollectionLiquidation LIQ " +
                "       LEFT JOIN CR ON LIQ.DocNumber = CR.DocNumber " +
                " WHERE LIQ.DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = Convert.ToDateTime(dtRow["DocDate"]);
                RefNumber = dtRow["RefNumber"].ToString();
                CustomerCode = dtRow["CustCode"].ToString();
                Name = dtRow["CustName"].ToString();
                BizAccount = dtRow["BizAcct"].ToString();
                Collector = dtRow["Collector"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Currency = Convert.IsDBNull(dtRow["Currency"]) || String.IsNullOrEmpty(dtRow["Currency"].ToString()) ? "PHP" : dtRow["Currency"].ToString();
                CashAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CashAmount"]) ? 0 : dtRow["CashAmount"]);
                TotalCashAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCashAmount"]) ? 0 : dtRow["TotalCashAmount"]);
                TotalBankCredit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBankCredit"]) ? 0 : dtRow["TotalBankCredit"]);
                TotalCheckAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCheckAmount"]) ? 0 : dtRow["TotalCheckAmount"]);
                TotalAmountDue = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountDue"]) ? 0 : dtRow["TotalAmountDue"]);
                TotalApplied = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalApplied"]) ? 0 : dtRow["TotalApplied"]);
                TotalAdjustment = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAdjustment"]) ? 0 : dtRow["TotalAdjustment"]);
                Variance = Convert.ToDecimal(Convert.IsDBNull(dtRow["Variance"]) ? 0 : dtRow["Variance"]);
                RefTransDoc = dtRow["RefTransDoc"].ToString();
                RefLoans = dtRow["RefLoans"].ToString();
                CollectionType = dtRow["CollType"].ToString();

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
        public void InsertData(CollectionLiquidation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "TotalApplied", _ent.TotalApplied);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "TotalAdjustment", _ent.TotalAdjustment);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Variance", _ent.Variance);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "RefTransDoc", _ent.RefTransDoc);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "RefLoans", _ent.RefLoans);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "CollectionType", _ent.CollectionType);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "BizAccount", _ent.BizAccount);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Name", _ent.Name);

            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(CollectionLiquidation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "TotalApplied", _ent.TotalApplied);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "TotalAdjustment", _ent.TotalAdjustment);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Variance", _ent.Variance);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "RefTransDoc", _ent.RefTransDoc);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "RefLoans", _ent.RefLoans);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "CollectionType", _ent.CollectionType);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "BizAccount", _ent.BizAccount);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Name", _ent.Name);

            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CollectionLiquidation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strMessage = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCLQ", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CollectionLiquidation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.CollectionApplication", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DT3.Rows.Add("Accounting.CollectionAdjustment", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT3, _ent.Connection);

            Functions.AuditTrail("ACTCLQ", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        #endregion

        #region Application
        public class CollectionApplication
        {
            public virtual CollectionLiquidation Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string TransType { get; set; }
            public virtual string TransDoc { get; set; }
            public virtual DateTime TransDate { get; set; }
            public virtual string TransAccountCode { get; set; }
            public virtual string TransSubsiCode { get; set; }
            public virtual string TransProfitCenter { get; set; }
            public virtual string TransCostCenter { get; set; }
            public virtual string TransBizPartnerCode { get; set; }
            public virtual decimal TransAmountDue { get; set; }
            public virtual decimal TransAmountApplied { get; set; }
            public virtual string Version { get; set; }
            public virtual string RecordID { get; set; }


            public DataTable getApplication(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCollectionApplication(CollectionApplication CollectionApplication)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CollectionApplication WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransType", CollectionApplication.TransType);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransDoc", CollectionApplication.TransDoc);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransDate", CollectionApplication.TransDate);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransAccountCode", CollectionApplication.TransAccountCode);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransSubsiCode", CollectionApplication.TransSubsiCode);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransProfitCenter", CollectionApplication.TransProfitCenter);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransCostCenter", CollectionApplication.TransCostCenter);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransBizPartnerCode", CollectionApplication.TransBizPartnerCode);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransAmountDue", CollectionApplication.TransAmountDue);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "TransAmountApplied", CollectionApplication.TransAmountApplied);
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "Version", "1");
                DT1.Rows.Add("Accounting.CollectionApplication", "0", "RecordID", CollectionApplication.RecordID);

                DT2.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CollectionLiquidation", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCollectionApplication(CollectionApplication CollectionApplication)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionApplication", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionApplication", "cond", "LineNumber", CollectionApplication.LineNumber);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransType", CollectionApplication.TransType);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransDoc", CollectionApplication.TransDoc);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransDate", CollectionApplication.TransDate);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransAccountCode", CollectionApplication.TransAccountCode);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransSubsiCode", CollectionApplication.TransSubsiCode);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransProfitCenter", CollectionApplication.TransProfitCenter);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransCostCenter", CollectionApplication.TransCostCenter);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransBizPartnerCode", CollectionApplication.TransBizPartnerCode);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransAmountDue", CollectionApplication.TransAmountDue);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "TransAmountApplied", CollectionApplication.TransAmountApplied);
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "Version", "1");
                DT1.Rows.Add("Accounting.CollectionApplication", "set", "RecordID", CollectionApplication.RecordID);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteCollectionApplication(CollectionApplication CollectionApplication)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionApplication", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionApplication", "cond", "LineNumber", CollectionApplication.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Application = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Loan = Gears.RetriveData2("SELECT * FROM Accounting.CollectionLoan WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((Application.Rows.Count < 1) && (Adjustment.Rows.Count < 1) && (Loan.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.CollectionLiquidation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        #endregion

        #region Adjustment
        public class CollectionAdjustment
        {
            public virtual CollectionLiquidation Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string BizPartnerCode { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual string Reason { get; set; }
            public DataTable getAdjustment(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCollectionAdjustment(CollectionAdjustment CollectionAdjustment)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CollectionAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "AccountCode", CollectionAdjustment.AccountCode);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "SubsidiaryCode", CollectionAdjustment.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "BizPartnerCode", CollectionAdjustment.BizPartnerCode);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "ProfitCenter", CollectionAdjustment.ProfitCenter);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "CostCenter", CollectionAdjustment.CostCenter);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "Amount", CollectionAdjustment.Amount);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "0", "Reason", CollectionAdjustment.Reason);

                DT2.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CollectionLiquidation", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateCollectionAdjustment(CollectionAdjustment CollectionAdjustment)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.CollectionAdjustment", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "cond", "LineNumber", CollectionAdjustment.LineNumber);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "set", "AccountCode", CollectionAdjustment.AccountCode);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "set", "SubsidiaryCode", CollectionAdjustment.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "set", "BizPartnerCode", CollectionAdjustment.BizPartnerCode);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "set", "ProfitCenter", CollectionAdjustment.ProfitCenter);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "set", "CostCenter", CollectionAdjustment.CostCenter);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "set", "Amount", CollectionAdjustment.Amount);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "set", "Reason", CollectionAdjustment.Reason);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteCollectionAdjustment(CollectionAdjustment CollectionAdjustment)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionAdjustment", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionAdjustment", "cond", "LineNumber", CollectionAdjustment.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Application = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Loan = Gears.RetriveData2("SELECT * FROM Accounting.CollectionLoan WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((Application.Rows.Count < 1) && (Adjustment.Rows.Count < 1) && (Loan.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.CollectionLiquidation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        #endregion

        #region Loan
        public class CollectionLoan
        {
            public virtual CollectionLiquidation Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string LoanNumber { get; set; }
            public virtual string LoanType { get; set; }
            public virtual string LoanClass { get; set; }
            public virtual decimal LoanAmount { get; set; }
            public virtual string LoanReference { get; set; }
            public virtual string Version { get; set; }


            public DataTable getLoan(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CollectionLoan WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCollectionLoan(CollectionLoan CollectionLoan)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CollectionLoan WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "LoanNumber", CollectionLoan.LoanNumber);
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "LoanType", CollectionLoan.LoanType);
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "LoanClass", CollectionLoan.LoanClass);
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "LoanAmount", CollectionLoan.LoanAmount);
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "LoanReference", CollectionLoan.LoanReference);
                DT1.Rows.Add("Accounting.CollectionLoan", "0", "Version", "1");

                DT2.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CollectionLiquidation", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCollectionLoan(CollectionLoan CollectionLoan)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionLoan", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionLoan", "cond", "LineNumber", CollectionLoan.LineNumber);
                DT1.Rows.Add("Accounting.CollectionLoan", "set", "LoanNumber", CollectionLoan.LoanNumber);
                DT1.Rows.Add("Accounting.CollectionLoan", "set", "LoanType", CollectionLoan.LoanType);
                DT1.Rows.Add("Accounting.CollectionLoan", "set", "LoanClass", CollectionLoan.LoanClass);
                DT1.Rows.Add("Accounting.CollectionLoan", "set", "LoanAmount", CollectionLoan.LoanAmount);
                DT1.Rows.Add("Accounting.CollectionLoan", "set", "LoanReference", CollectionLoan.LoanReference);
                DT1.Rows.Add("Accounting.CollectionLoan", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteCollectionLoan(CollectionLoan CollectionLoan)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CollectionLoan", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CollectionLoan", "cond", "LineNumber", CollectionLoan.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Application = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Loan = Gears.RetriveData2("SELECT * FROM Accounting.CollectionLoan WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((Application.Rows.Count < 1) && (Adjustment.Rows.Count < 1) && (Loan.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.CollectionLiquidation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.CollectionLiquidation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        #endregion
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.CollectionApplication WHERE DocNumber = '" + DocNumber + "'", Conn);
        }

        public void DeleteFirstDataLoan(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.CollectionLoan WHERE DocNumber = '" + DocNumber + "'", Conn);
        }
    }
}
