using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CancelCheckIssued
    {

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TransType { get; set; }
        public virtual string CheckNumber { get; set; }
        public virtual string BankAccountCode { get; set; }
        public virtual string CheckVoucherDocNum { get; set; }
        public virtual string Remarks { get; set; }

        public virtual string AccountCode { get; set; }
        public virtual string AccountDescription { get; set; }
        public virtual string SubsidiaryCode { get; set; }
        public virtual string SubsidiaryDescription { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual decimal DebitAmount { get; set; }
        public virtual decimal CreditAmount { get; set; }

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

        public class RefTransaction
        {
            public virtual CancelCheckIssued Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTCCI' OR  A.TransType='ACTCCI') ", Conn);
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

            if (DocNumber != null)
            {
                a = Gears.RetriveData2("select * from Accounting.CancellationOfCheckIssued where DocNumber = '" + DocNumber + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    DocDate = dtRow["DocDate"].ToString();
                    TransType = dtRow["TransType"].ToString();
                    CheckNumber = dtRow["CheckNumber"].ToString();
                    BankAccountCode = dtRow["BankAccountCode"].ToString();
                    CheckVoucherDocNum = dtRow["CheckVoucherDocNum"].ToString();
                    Remarks = dtRow["Remarks"].ToString();

                    AccountCode = dtRow["AccountCode"].ToString();
                    AccountDescription = dtRow["AccountDescription"].ToString();
                    SubsidiaryCode = dtRow["SubsidiaryCode"].ToString();
                    SubsidiaryDescription = dtRow["SubsidiaryDescription"].ToString();
                    ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                    CostCenterCode = dtRow["CostCenterCode"].ToString();
                    DebitAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["DebitAmount"]) ? 0 : dtRow["DebitAmount"]);
                    CreditAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CreditAmount"]) ? 0 : dtRow["CreditAmount"]);

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
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public class JournalEntry
        {
            public virtual CancelCheckIssued Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCCI' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(CancelCheckIssued _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Conn = _ent.Connection;

            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "TransType", _ent.TransType);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "CheckNumber", _ent.CheckNumber);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "BankAccountCode", _ent.BankAccountCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "CheckVoucherDocNum", _ent.CheckVoucherDocNum);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "AccountDescription", _ent.AccountDescription);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "SubsidiaryCode", _ent.SubsidiaryCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "SubsidiaryDescription", _ent.SubsidiaryDescription);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "DebitAmount", _ent.DebitAmount);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "CreditAmount", _ent.CreditAmount);

            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "0", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
            //Functions.AuditTrail("ACTCCI", _ent.DocNumber, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }
        public void UpdateData(CancelCheckIssued _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "TransType", _ent.TransType);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "CheckNumber", _ent.CheckNumber);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "BankAccountCode", _ent.BankAccountCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "CheckVoucherDocNum", _ent.CheckVoucherDocNum);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "AccountDescription", _ent.AccountDescription);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "SubsidiaryCode", _ent.SubsidiaryCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "SubsidiaryDescription", _ent.SubsidiaryDescription);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "DebitAmount", _ent.DebitAmount);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "CreditAmount", _ent.CreditAmount);

            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            //Functions.AuditTrail("ACTCCI", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(CancelCheckIssued _ent)
        {
            Conn = _ent.Connection;
            DocNumber = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CancellationOfCheckIssued", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCCI", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
