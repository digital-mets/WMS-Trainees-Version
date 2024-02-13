using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CashAdvance
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string FundSource { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Requestor { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual string Receiver { get; set; }
        public virtual decimal CashAdvanceAmount { get; set; }

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
            public virtual CashAdvance Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString FROM  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + " WHERE (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTCAD' OR  A.TransType='ACTCAD') ", Conn);
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

            a = Gears.RetriveData2("SELECT * FROM Accounting.CashAdvance WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                FundSource = dtRow["FundSource"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Requestor = dtRow["Requestor"].ToString();
                CostCenterCode = dtRow["CostCenter"].ToString();
                Receiver = dtRow["Receiver"].ToString();
                CashAdvanceAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CashAdvanceAmount"]) ? 0 : dtRow["CashAdvanceAmount"]);

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

            return a;
        }
        public class JournalEntry
        {
            public virtual CashAdvance Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, String Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCAD' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(CashAdvance _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CashAdvance", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Requestor", _ent.Requestor);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "CostCenter", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Receiver", _ent.Receiver);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "CashAdvanceAmount", _ent.CashAdvanceAmount);

            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashAdvance", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCAD", _ent.DocNumber, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }
        public void UpdateData(CashAdvance _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CashAdvance", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Requestor", _ent.Requestor);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "CostCenter", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Receiver", _ent.Receiver);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "CashAdvanceAmount", _ent.CashAdvanceAmount);

            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CashAdvance", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CashAdvance", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCAD", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CashAdvance _ent)
        {
            Conn = _ent.Connection;
            DocNumber = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CashAdvance", "cond", "DocNumber", _ent.DocNumber);

            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCAD", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
