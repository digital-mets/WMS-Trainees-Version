using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class LiquidationOfCashAdvance
    {
        private static string Docnum;
        private static string Conn;
        public virtual string Connection { get; set; }

        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string ReplenishNumber { get; set; }
        public virtual string CashAdvanceNumber { get; set; }
        public virtual string CashAdvanceDate { get; set; }
        public virtual string FundSource { get; set; }
        public virtual string Requestor { get; set; }
        public virtual string Receiver { get; set; }
        public virtual decimal AdvanceAmount { get; set; }
        public virtual decimal ExpendAmount { get; set; }
        public virtual decimal OverShort { get; set; }
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

        public virtual IList<LiquidationOfCashAdvanceDetail> Detail { get; set; }

        public class RefTransaction
        {
            public virtual LiquidationOfCashAdvance Parent { get; set; }
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
                                            + " WHERE (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTLCA' OR  A.TransType='ACTLCA') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public class LiquidationOfCashAdvanceDetail
        {
            public virtual LiquidationOfCashAdvance Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT * FROM Accounting.LiquidationOfCashAdvanceDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddLiquidationOfCashAdvanceDetail(LiquidationOfCashAdvanceDetail LiquidationOfCashAdvanceDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Accounting.LiquidationOfCashAdvanceDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "ExpenseCode", LiquidationOfCashAdvanceDetail.ExpenseCode);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "ExpenseDescription", LiquidationOfCashAdvanceDetail.ExpenseDescription);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "ProfitCenterCode", LiquidationOfCashAdvanceDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "CostCenterCode", LiquidationOfCashAdvanceDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "ExpenseAmount", LiquidationOfCashAdvanceDetail.ExpenseAmount);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Remarks", LiquidationOfCashAdvanceDetail.Remarks);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field1", LiquidationOfCashAdvanceDetail.Field1);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field2", LiquidationOfCashAdvanceDetail.Field2);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field3", LiquidationOfCashAdvanceDetail.Field3);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field4", LiquidationOfCashAdvanceDetail.Field4);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field5", LiquidationOfCashAdvanceDetail.Field5);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field6", LiquidationOfCashAdvanceDetail.Field6);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field7", LiquidationOfCashAdvanceDetail.Field7);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field8", LiquidationOfCashAdvanceDetail.Field8);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "0", "Field9", LiquidationOfCashAdvanceDetail.Field9);

                DT2.Rows.Add("Accounting.LiquidationOfCashAdvance", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateLiquidationOfCashAdvanceDetail(LiquidationOfCashAdvanceDetail LiquidationOfCashAdvanceDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "cond", "LineNumber", LiquidationOfCashAdvanceDetail.LineNumber);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "ExpenseCode", LiquidationOfCashAdvanceDetail.ExpenseCode);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "ExpenseDescription", LiquidationOfCashAdvanceDetail.ExpenseDescription);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "ProfitCenterCode", LiquidationOfCashAdvanceDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "CostCenterCode", LiquidationOfCashAdvanceDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "ExpenseAmount", LiquidationOfCashAdvanceDetail.ExpenseAmount);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Remarks", LiquidationOfCashAdvanceDetail.Remarks);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field1", LiquidationOfCashAdvanceDetail.Field1);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field2", LiquidationOfCashAdvanceDetail.Field2);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field3", LiquidationOfCashAdvanceDetail.Field3);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field4", LiquidationOfCashAdvanceDetail.Field4);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field5", LiquidationOfCashAdvanceDetail.Field5);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field6", LiquidationOfCashAdvanceDetail.Field6);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field7", LiquidationOfCashAdvanceDetail.Field7);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field8", LiquidationOfCashAdvanceDetail.Field8);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "set", "Field9", LiquidationOfCashAdvanceDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteLiquidationOfCashAdvanceDetail(LiquidationOfCashAdvanceDetail LiquidationOfCashAdvanceDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "cond", "LineNumber", LiquidationOfCashAdvanceDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Procurement.LiquidationOfCashAdvanceDetail WHERE DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.LiquidationOfCashAdvance", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        public class JournalEntry
        {
            public virtual LiquidationOfCashAdvance Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTLCA' ", Conn);

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

            a = Gears.RetriveData2("SELECT *, CONVERT(varchar(20),CashAdvanceDate,101) AS CADate FROM Accounting.LiquidationOfCashAdvance WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ReplenishNumber = dtRow["ReplenishNumber"].ToString();
                CashAdvanceNumber = dtRow["CashAdvanceNumber"].ToString();
                CashAdvanceDate = dtRow["CADate"].ToString();
                FundSource = dtRow["FundSource"].ToString();
                Requestor = dtRow["Requestor"].ToString();
                Receiver = dtRow["Receiver"].ToString();
                AdvanceAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["AdvanceAmount"]) ? 0 : dtRow["AdvanceAmount"]);
                ExpendAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExpendAmount"]) ? 0 : dtRow["ExpendAmount"]);
                OverShort = Convert.ToDecimal(Convert.IsDBNull(dtRow["OverShort"]) ? 0 : dtRow["OverShort"]);
                
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
        public void InsertData(LiquidationOfCashAdvance _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "ReplenishNumber", _ent.ReplenishNumber);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "CashAdvanceNumber", _ent.CashAdvanceNumber);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "CashAdvanceDate", _ent.CashAdvanceDate);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Requestor", _ent.Requestor);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Receiver", _ent.Receiver);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "AdvanceAmount", _ent.AdvanceAmount);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "ExpendAmount", _ent.ExpendAmount);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "OverShort", _ent.OverShort);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTLCA", Docnum, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }
        public void UpdateData(LiquidationOfCashAdvance _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "ReplenishNumber", _ent.ReplenishNumber);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "CashAdvanceNumber", _ent.CashAdvanceNumber);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "CashAdvanceDate", _ent.CashAdvanceDate);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Requestor", _ent.Requestor);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Receiver", _ent.Receiver);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "AdvanceAmount", _ent.AdvanceAmount);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "ExpendAmount", _ent.ExpendAmount);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "OverShort", _ent.OverShort);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            //Gears.UpdateData(DT2);
            Functions.AuditTrail("ACTLCA", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(LiquidationOfCashAdvance _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.LiquidationOfCashAdvance", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.LiquidationOfCashAdvanceDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);
            Functions.AuditTrail("ACTLCA", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
