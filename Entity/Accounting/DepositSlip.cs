using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DepositSlip
    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string BankAccountCode { get; set; }
        public virtual string ReferenceChecks { get; set; }
        public virtual decimal TotalCashAmount { get; set; }
        public virtual decimal TotalCheckAmount { get; set; }
        public virtual decimal TotalAmount { get; set; }
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
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual IList<DepositSlipCash> Application { get; set; }
        public virtual IList<DepositSlipCheck> Adjustment { get; set; }

        #region Header

        public class RefTransaction
        {
            public virtual DepositSlip Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTDES' OR  A.TransType='ACTDES') ", Conn);
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

            a = Gears.RetriveData2("SELECT * FROM Accounting.DepositSlip WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                BankAccountCode = dtRow["BankAccountCode"].ToString();
                ReferenceChecks = dtRow["ReferenceChecks"].ToString();
                TotalCheckAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCheckAmount"]) ? 0 : dtRow["TotalCheckAmount"]);
                TotalCashAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCashAmount"]) ? 0 : dtRow["TotalCashAmount"]);
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
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
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString(); 
            }
            return a;
        }
        public void InsertData(DepositSlip _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.DepositSlip", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "BankAccountCode", _ent.BankAccountCode);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "ReferenceChecks", _ent.ReferenceChecks);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "TotalCheckAmount", _ent.TotalCheckAmount);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "TotalCashAmount", _ent.TotalCashAmount);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.DepositSlip", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Accounting.DepositSlip", "0", "IsWithDetail", "False");
            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(DepositSlip _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.DepositSlip", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "BankAccountCode", _ent.BankAccountCode);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "ReferenceChecks", _ent.ReferenceChecks);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "TotalCheckAmount", _ent.TotalCheckAmount);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "TotalCashAmount", _ent.TotalCashAmount);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.DepositSlip", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTDES", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(DepositSlip _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.DepositSlip", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.DepositSlipCash", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);
            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DT3.Rows.Add("Accounting.DepositSlipCheck", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT3, _ent.Connection);
            Functions.AuditTrail("ACTDES", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        #endregion

        public class JournalEntry
        {
            public virtual DepositSlip Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTDES' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        #region Details
        public class DepositSlipCash
        {
            public virtual DepositSlip Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string RefDocNum { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual string CustomerCode { get; set; }
            public virtual string CustomerName { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getCashDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.*, CASE WHEN ISNULL(B.CustomerCode,'') != '' THEN B.CustomerCode ELSE B.BizAccount END AS CustomerCode, RTRIM(LTRIM(C.Name)) AS CustomerName FROM Accounting.DepositSlipCash A LEFT JOIN Accounting.Collection B ON A.RefDocNum = B.DocNumber "
                        + "  LEFT JOIN Masterfile.BPCustomerInfo C ON CASE WHEN ISNULL(B.CustomerCode,'') != '' THEN B.CustomerCode ELSE B.BizAccount END = C.BizPartnerCode  WHERE A.DocNumber='" + DocNumber + "' ORDER BY A.LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddDepositSlipCash(DepositSlipCash DepositSlipCash)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.DepositSlipCash WHERE DocNumber = '" + Docnum + "'", Conn);
                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }
                
                string strLine = linenum.ToString().PadLeft(5, '0');
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "RefDocNum", DepositSlipCash.RefDocNum);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Amount", DepositSlipCash.Amount);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field1", DepositSlipCash.Field1);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field2", DepositSlipCash.Field2);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field3", DepositSlipCash.Field3);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field4", DepositSlipCash.Field4);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field5", DepositSlipCash.Field5);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field6", DepositSlipCash.Field6);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field7", DepositSlipCash.Field7);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field8", DepositSlipCash.Field8);
                DT1.Rows.Add("Accounting.DepositSlipCash", "0", "Field9", DepositSlipCash.Field9);
                DT2.Rows.Add("Accounting.DepositSlip", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.DepositSlip", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateDepositSlipCash(DepositSlipCash DepositSlipCash)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.DepositSlipCash", "cond", "DocNumber", DepositSlipCash.DocNumber);
                DT1.Rows.Add("Accounting.DepositSlipCash", "cond", "LineNumber", DepositSlipCash.LineNumber);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "RefDocNum", DepositSlipCash.RefDocNum);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Amount", DepositSlipCash.Amount);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field1", DepositSlipCash.Field1);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field2", DepositSlipCash.Field2);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field3", DepositSlipCash.Field3);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field4", DepositSlipCash.Field4);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field5", DepositSlipCash.Field5);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field6", DepositSlipCash.Field6);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field7", DepositSlipCash.Field7);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field8", DepositSlipCash.Field8);
                DT1.Rows.Add("Accounting.DepositSlipCash", "set", "Field9", DepositSlipCash.Field9);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteDepositSlipCash(DepositSlipCash DepositSlipCash)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.DepositSlipCash", "cond", "DocNumber", DepositSlipCash.DocNumber);
                DT1.Rows.Add("Accounting.DepositSlipCash", "cond", "LineNumber", DepositSlipCash.LineNumber);
                Gears.DeleteData(DT1, Conn);
                DataTable count = Gears.RetriveData2("select * from Accounting.DepositSlipCheck where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DataTable count2 = Gears.RetriveData2("select * from Accounting.DepositSlipCash where DocNumber = '" + Docnum + "'", Conn);
                    if (count2.Rows.Count < 1)
                    {
                        DT2.Rows.Add("Accounting.DepositSlip", "cond", "DocNumber", Docnum);
                        DT2.Rows.Add("Accounting.DepositSlip", "set", "IsWithDetail", "False");
                        Gears.UpdateData(DT2, Conn);
                    }
                }
            }
        }
        public class DepositSlipCheck
        {
            public virtual DepositSlip Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string RefDocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string CheckNumber { get; set; }
            public virtual string BankCode { get; set; }
            public virtual string BankBranch { get; set; }
            public virtual DateTime CheckDate { get; set; }
            public virtual decimal CheckAmount { get; set; }
            public virtual string BPCode { get; set; }
            public virtual string BPName { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getCheckDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.DepositSlipCheck WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddDepositSlipCheck(DepositSlipCheck DepositSlipCheck)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.DepositSlipCheck WHERE DocNumber = '" + Docnum + "'", Conn);

                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }

                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "DocNumber ", Docnum);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "RefDocNumber", DepositSlipCheck.RefDocNumber);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "CheckNumber", DepositSlipCheck.CheckNumber);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "BankCode", DepositSlipCheck.BankCode);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "BankBranch", DepositSlipCheck.BankBranch);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "CheckDate", DepositSlipCheck.CheckDate);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "CheckAmount", DepositSlipCheck.CheckAmount);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "BPCode", DepositSlipCheck.BPCode);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "BPName", DepositSlipCheck.BPName);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field1", DepositSlipCheck.Field1);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field2", DepositSlipCheck.Field2);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field3", DepositSlipCheck.Field3);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field4", DepositSlipCheck.Field4);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field5", DepositSlipCheck.Field5);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field6", DepositSlipCheck.Field6);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field7", DepositSlipCheck.Field7);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field8", DepositSlipCheck.Field8);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "0", "Field9", DepositSlipCheck.Field9);
                DT2.Rows.Add("Accounting.DepositSlip", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.DepositSlip", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateDepositSlipCheck(DepositSlipCheck DepositSlipCheck)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.DepositSlipCheck", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "cond", "LineNumber", DepositSlipCheck.LineNumber);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "RefDocNumber", DepositSlipCheck.RefDocNumber);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "CheckNumber", DepositSlipCheck.CheckNumber);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "BankCode", DepositSlipCheck.BankCode);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "BankBranch", DepositSlipCheck.BankBranch);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "CheckDate", DepositSlipCheck.CheckDate);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "CheckAmount", DepositSlipCheck.CheckAmount);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "BPCode", DepositSlipCheck.BPCode);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "BPName", DepositSlipCheck.BPName);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field1", DepositSlipCheck.Field1);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field2", DepositSlipCheck.Field2);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field3", DepositSlipCheck.Field3);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field4", DepositSlipCheck.Field4);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field5", DepositSlipCheck.Field5);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field6", DepositSlipCheck.Field6);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field7", DepositSlipCheck.Field7);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field8", DepositSlipCheck.Field8);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "set", "Field9", DepositSlipCheck.Field9);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteDepositSlipCheck(DepositSlipCheck DepositSlipCheck)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.DepositSlipCheck", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.DepositSlipCheck", "cond", "LineNumber", DepositSlipCheck.LineNumber);
                Gears.DeleteData(DT1, Conn);
                DataTable count = Gears.RetriveData2("select * from Accounting.DepositSlipCheck where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DataTable count2 = Gears.RetriveData2("select * from Accounting.DepositSlipCash where DocNumber = '" + Docnum + "'", Conn);
                     if (count2.Rows.Count < 1)
                     {
                         DT2.Rows.Add("Accounting.DepositSlip", "cond", "DocNumber", Docnum);
                         DT2.Rows.Add("Accounting.DepositSlip", "set", "IsWithDetail", "False");
                         Gears.UpdateData(DT2, Conn);
                     }
                }
            }
        }
        #endregion
    }
}
