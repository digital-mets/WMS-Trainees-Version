using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DISin
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Step { get; set; }
        public virtual string DIS { get; set; }
        public virtual string WorkCenter { get; set; }
        public virtual string DRDocNo { get; set; }
        public virtual string Remarks { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }

        //public virtual string CancelledBy { get; set; }
        //public virtual string CancelledDate { get; set; }
        //public virtual string PostedBy { get; set; }
        //public virtual string PostedDate { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public class RefTransaction
        {
            public virtual DISin Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='" + TransType + "' OR  A.TransType='" + TransType + "') ", Conn);
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
                a = Gears.RetriveData2("select * from Production.DISin where DocNumber = '" + DocNumber + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    DocDate = dtRow["DocDate"].ToString();
                    Step = dtRow["Step"].ToString();
                    DIS = dtRow["DIS"].ToString();
                    WorkCenter = dtRow["WorkCenter"].ToString();
                    DRDocNo = dtRow["DRDocNo"].ToString();
                    Remarks = dtRow["Remarks"].ToString();
                    
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    SubmittedBy = dtRow["SubmittedBy"].ToString();
                    SubmittedDate = dtRow["SubmittedDate"].ToString();
                    //CancelledBy = dtRow["CancelledBy"].ToString();
                    //CancelledDate = dtRow["CancelledDate"].ToString();

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
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public class JournalEntry
        {
            public virtual DISin Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit, A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='" + TransType + "' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public void InsertData(DISin _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DISin", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.DISin", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.DISin", "0", "Step", _ent.Step);
            DT1.Rows.Add("Production.DISin", "0", "DIS", _ent.DIS);
            DT1.Rows.Add("Production.DISin", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.DISin", "0", "DRDocNo", _ent.DRDocNo);
            DT1.Rows.Add("Production.DISin", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Production.DISin", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.DISin", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.DISin", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.DISin", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.DISin", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.DISin", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.DISin", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.DISin", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.DISin", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.DISin", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.DISin", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Production.DISin", "0", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.DISin", "0", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
            //Functions.AuditTrail("ACTCOC", _ent.DocNumber, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }
        public void UpdateData(DISin _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DISin", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.DISin", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.DISin", "set", "Step", _ent.Step);
            DT1.Rows.Add("Production.DISin", "set", "DIS", _ent.DIS);
            DT1.Rows.Add("Production.DISin", "set", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.DISin", "set", "DRDocNo", _ent.DRDocNo);
            DT1.Rows.Add("Production.DISin", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Production.DISin", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.DISin", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.DISin", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.DISin", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.DISin", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.DISin", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.DISin", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.DISin", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.DISin", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.DISin", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.DISin", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            //Functions.AuditTrail("ACTCOC", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(DISin _ent)
        {
            Conn = _ent.Connection;
            DocNumber = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DISin", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDIN", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
