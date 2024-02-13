using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CashReimbursement
    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string ReplenishNumber { get; set; }
        public virtual string FundSource { get; set; }
        public virtual string Requestor { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual string Receiver { get; set; }
        public virtual decimal ExpendAmount { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }
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

        public virtual IList<CashReimbursementDetail> Detail { get; set; }


        public class CashReimbursementDetail
        {
            public virtual CashReimbursement Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ExpenseCode { get; set; }
            public virtual string ExpenseDescription { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual decimal ExpenseAmount { get; set; }
            public virtual string Remarks { get; set; }
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
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CashReimbursementDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCashReimbursementDetail(CashReimbursementDetail CashReimbursementDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) AS LineNumber FROM Accounting.CashReimbursementDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "ExpenseCode", CashReimbursementDetail.ExpenseCode);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "ExpenseDescription", CashReimbursementDetail.ExpenseDescription);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "ProfitCenterCode", CashReimbursementDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "CostCenterCode", CashReimbursementDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "ExpenseAmount", CashReimbursementDetail.ExpenseAmount);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Remarks", CashReimbursementDetail.Remarks);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field1", CashReimbursementDetail.Field1);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field2", CashReimbursementDetail.Field2);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field3", CashReimbursementDetail.Field3);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field4", CashReimbursementDetail.Field4);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field5", CashReimbursementDetail.Field5);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field6", CashReimbursementDetail.Field6);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field7", CashReimbursementDetail.Field7);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field8", CashReimbursementDetail.Field8);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "0", "Field9", CashReimbursementDetail.Field9);

                DT2.Rows.Add("Accounting.CashReimbursement", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CashReimbursement", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCashReimbursementDetail(CashReimbursementDetail CashReimbursementDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.CashReimbursementDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "cond", "LineNumber", CashReimbursementDetail.LineNumber);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "ExpenseCode", CashReimbursementDetail.ExpenseCode);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "ExpenseDescription", CashReimbursementDetail.ExpenseDescription);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "ProfitCenterCode", CashReimbursementDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "CostCenterCode", CashReimbursementDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "ExpenseAmount", CashReimbursementDetail.ExpenseAmount);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Remarks", CashReimbursementDetail.Remarks);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field1", CashReimbursementDetail.Field1);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field2", CashReimbursementDetail.Field2);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field3", CashReimbursementDetail.Field3);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field4", CashReimbursementDetail.Field4);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field5", CashReimbursementDetail.Field5);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field6", CashReimbursementDetail.Field6);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field7", CashReimbursementDetail.Field7);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field8", CashReimbursementDetail.Field8);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "set", "Field9", CashReimbursementDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteCashReimbursementDetail(CashReimbursementDetail CashReimbursementDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashReimbursementDetail", "cond", "LineNumber", CashReimbursementDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.CashReimbursementDetail WHERE DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.CashReimbursement", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.CashReimbursement", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        public class JournalEntry
        {
            public virtual CashReimbursement Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCRB' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public class RefTransaction
        {
            public virtual CashReimbursement Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) AS RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) AS CommandString FROM  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  WHERE (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTCRB' OR  A.TransType='ACTCRB') ", Conn);
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

            a = Gears.RetriveData2("SELECT * FROM Accounting.CashReimbursement WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ReplenishNumber = dtRow["ReplenishNumber"].ToString();
                FundSource = dtRow["FundSource"].ToString();
                Requestor = dtRow["Requestor"].ToString();
                CostCenterCode = dtRow["CostCenterCode"].ToString();
                Receiver = dtRow["Receiver"].ToString();
                ExpendAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExpendAmount"]) ? 0 : dtRow["ExpendAmount"]);
                
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
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
        public void InsertData(CashReimbursement _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CashReimbursement", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "ReplenishNumber", _ent.ReplenishNumber);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Requestor", _ent.Requestor);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Receiver", _ent.Receiver);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "ExpendAmount", _ent.ExpendAmount);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.CashReimbursement", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTLCA", Docnum, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }
        public void UpdateData(CashReimbursement _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CashReimbursement", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "ReplenishNumber", _ent.ReplenishNumber);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Requestor", _ent.Requestor);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Receiver", _ent.Receiver);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "ExpendAmount", _ent.ExpendAmount);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashReimbursement", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCRB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CashReimbursement _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CashReimbursement", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.CashReimbursementDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);
            Functions.AuditTrail("ACTCRB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
