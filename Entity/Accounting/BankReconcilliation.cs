using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BankReconcilliation
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string BankAccountCode { get; set; }
        public virtual string CutOffDate { get; set; }
        public virtual decimal LastBankReconBal { get; set; }
        public virtual string LastBankReconDate { get; set; }
        public virtual decimal EndingBalance { get; set; }
        public virtual decimal TotalDebitAmount { get; set; }
        public virtual decimal TotalCreditAmount { get; set; }
        public virtual bool SelectAll { get; set; }

        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }

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

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public virtual IList<BankReconcilliationDetail> Detail { get; set; }

        public class JournalEntry
        {
            public virtual BankReconcilliation Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTBRC' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public class BankReconcilliationDetail
        {
            public virtual BankReconcilliation Parent { get; set; }
            public virtual string TransDocNumber { get; set; }
            public virtual Int32 TransLineNumber { get; set; }
            public virtual string TransType { get; set; }
            public virtual bool IsSelected { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual DateTime DocDate { get; set; }
            public virtual string CheckNumber { get; set; }
            public virtual string PayeeName { get; set; }
            public virtual DateTime CheckDate { get; set; }
            public virtual decimal DebitAmount { get; set; }
            public virtual decimal CreditAmount { get; set; }
            public virtual decimal BankRunningBalance { get; set; }
            public virtual string Version { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Accounting.BankReconciliationDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddBankReconcilliationDetail(BankReconcilliationDetail BankReconcilliationDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.BankReconciliationDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "TransDocNumber", BankReconcilliationDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "TransLineNumber", BankReconcilliationDetail.TransLineNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "IsSelected", BankReconcilliationDetail.IsSelected);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "TransType", BankReconcilliationDetail.TransType);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "DocDate", BankReconcilliationDetail.DocDate);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "CheckNumber", BankReconcilliationDetail.CheckNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "PayeeName", BankReconcilliationDetail.PayeeName);
                if (!Convert.IsDBNull(BankReconcilliationDetail.CheckDate))
                    DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "CheckDate", BankReconcilliationDetail.CheckDate);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "DebitAmount", BankReconcilliationDetail.DebitAmount);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "CreditAmount", BankReconcilliationDetail.CreditAmount);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "BankRunningBalance", BankReconcilliationDetail.BankRunningBalance);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Version", "1");

                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field1", BankReconcilliationDetail.Field1);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field2", BankReconcilliationDetail.Field2);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field3", BankReconcilliationDetail.Field3);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field4", BankReconcilliationDetail.Field4);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field5", BankReconcilliationDetail.Field5);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field6", BankReconcilliationDetail.Field6);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field7", BankReconcilliationDetail.Field7);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field8", BankReconcilliationDetail.Field8);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "0", "Field9", BankReconcilliationDetail.Field9);

                DT2.Rows.Add("Accounting.BankReconciliation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.BankReconciliation", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateBankReconcilliationDetail(BankReconcilliationDetail BankReconcilliationDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "cond", "LineNumber", BankReconcilliationDetail.LineNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "IsSelected", BankReconcilliationDetail.IsSelected);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "TransDocNumber", BankReconcilliationDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "TransLineNumber", BankReconcilliationDetail.TransLineNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "TransType", BankReconcilliationDetail.TransType);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "DocDate", BankReconcilliationDetail.DocDate);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "CheckNumber", BankReconcilliationDetail.CheckNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "PayeeName", BankReconcilliationDetail.PayeeName);
                if (!Convert.IsDBNull(BankReconcilliationDetail.CheckDate))
                    DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "CheckDate", BankReconcilliationDetail.CheckDate);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "DebitAmount", BankReconcilliationDetail.DebitAmount);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "CreditAmount", BankReconcilliationDetail.CreditAmount);
                if (!String.IsNullOrEmpty(BankReconcilliationDetail.BankRunningBalance.ToString()))
                    DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "BankRunningBalance", BankReconcilliationDetail.BankRunningBalance);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Version", "1");

                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field1", BankReconcilliationDetail.Field1);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field2", BankReconcilliationDetail.Field2);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field3", BankReconcilliationDetail.Field3);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field4", BankReconcilliationDetail.Field4);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field5", BankReconcilliationDetail.Field5);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field6", BankReconcilliationDetail.Field6);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field7", BankReconcilliationDetail.Field7);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field8", BankReconcilliationDetail.Field8);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "set", "Field9", BankReconcilliationDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteBankReconcilliationDetail(BankReconcilliationDetail BankReconcilliationDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "cond", "DocNumber", BankReconcilliationDetail.DocNumber);
                DT1.Rows.Add("Accounting.BankReconciliationDetail", "cond", "LineNumber", BankReconcilliationDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Accounting.BankReconciliationDetail where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.BankReconciliation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.BankReconciliation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        public class RefTransaction
        {
            public virtual BankReconcilliation Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTBRC' OR  A.TransType='ACTBRC') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select * from Accounting.BankReconciliation where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                BankAccountCode = dtRow["BankAccountCode"].ToString();
                CutOffDate = dtRow["DocDate"].ToString();
                LastBankReconBal = Convert.ToDecimal(Convert.IsDBNull(dtRow["LastBankReconBal"]) ? 0 : dtRow["LastBankReconBal"]);
                LastBankReconDate = dtRow["LastBankReconDate"].ToString();
                EndingBalance = Convert.ToDecimal(Convert.IsDBNull(dtRow["EndingBalance"]) ? 0 : dtRow["EndingBalance"]);
                TotalDebitAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalDebitAmount"]) ? 0 : dtRow["TotalDebitAmount"]);
                TotalCreditAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCreditAmount"]) ? 0 : dtRow["TotalCreditAmount"]);
                SelectAll = Convert.ToBoolean(Convert.IsDBNull(dtRow["SelectAll"]) ? false : dtRow["SelectAll"]);

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString(); 
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString(); 

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
        public void InsertData(BankReconcilliation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.BankReconciliation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "BankAccountCode", _ent.BankAccountCode);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "DocDate", _ent.CutOffDate);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "LastBankReconBal", _ent.LastBankReconBal);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "LastBankReconDate", _ent.LastBankReconDate);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "EndingBalance", _ent.EndingBalance);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "TotalDebitAmount", _ent.TotalDebitAmount);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "TotalCreditAmount", _ent.TotalCreditAmount);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "SelectAll", _ent.SelectAll);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.BankReconciliation", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.BankReconciliation", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(BankReconcilliation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.BankReconciliation", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "BankAccountCode", _ent.BankAccountCode);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "DocDate", _ent.CutOffDate);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "LastBankReconBal", _ent.LastBankReconBal);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "LastBankReconDate", _ent.LastBankReconDate);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "EndingBalance", _ent.EndingBalance);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "TotalDebitAmount", _ent.TotalDebitAmount);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "TotalCreditAmount", _ent.TotalCreditAmount);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "SelectAll", _ent.SelectAll);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.BankReconciliation", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTBRC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(BankReconcilliation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.BankReconciliation", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.BankReconciliationDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);
            Functions.AuditTrail("ACTBRC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable del = new DataTable();

            del = Gears.RetriveData2("DELETE FROM Accounting.BankReconciliationDetail WHERE DocNumber ='" + DocNumber + "' AND Version = '1'", Conn);
        }
    }
}
