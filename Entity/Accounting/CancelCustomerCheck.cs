using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CancelCustomerCheck
    {
        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string CheckCancelType { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CheckNumber { get; set; }
        public virtual string CheckDate { get; set; }
        public virtual decimal CheckAmount { get; set; }
        public virtual int CheckRecID { get; set; }
        public virtual string BouncedReason { get; set; }
        public virtual string CheckRefNumber { get; set; }
        public virtual bool IsValidated { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }

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
            public virtual CancelCustomerCheck Parent { get; set; }
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
                                            + "  WHERE (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTCOC' OR  A.TransType='ACTCOC') ", Conn);
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

            a = Gears.RetriveData2("SELECT *, CheckNumber + '|' + CheckRefNumber AS Ref FROM Accounting.CancellationOfCustomerCheck WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                CheckCancelType = dtRow["CheckCancelType"].ToString();
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CheckNumber = dtRow["Ref"].ToString();
                CheckDate = dtRow["CheckDate"].ToString();
                CheckAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CheckAmount"]) ? 0.00 : dtRow["CheckAmount"]);
                CheckRecID = Convert.ToInt32(Convert.IsDBNull(dtRow["CheckRecID"]) ? 0 : dtRow["CheckRecID"]);
                BouncedReason = dtRow["BouncedReason"].ToString();
                CheckRefNumber = dtRow["CheckRefNumber"].ToString();

                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();

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
        public class JournalEntry
        {
            public virtual CancelCustomerCheck Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCOC' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(CancelCustomerCheck _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "CheckCancelType", _ent.CheckCancelType);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "CheckNumber", _ent.CheckNumber);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "CheckDate", _ent.CheckDate);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "CheckAmount", _ent.CheckAmount);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "CheckRecID", _ent.CheckRecID);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "BouncedReason", _ent.BouncedReason);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "CheckRefNumber", _ent.CheckRefNumber);

            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(CancelCustomerCheck _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "CheckCancelType", _ent.CheckCancelType);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "CheckNumber", _ent.CheckNumber);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "CheckDate", _ent.CheckDate);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "CheckAmount", _ent.CheckAmount);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "CheckRecID", _ent.CheckRecID);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "BouncedReason", _ent.BouncedReason);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "CheckRefNumber", _ent.CheckRefNumber);

            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCOC", _ent.DocNumber, _ent.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CancelCustomerCheck _ent)
        {
            Conn = _ent.Connection;
            DocNumber = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CancellationOfCustomerCheck", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCOC", _ent.DocNumber, _ent.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
