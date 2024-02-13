using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CashReplenishment
    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual decimal TotalAmountReplenished { get; set; }
        public virtual string FundSource { get; set; }
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

        public virtual IList<CashReplenishmentDetail> Detail { get; set; }

        public class RefTransaction
        {
            public virtual CashReplenishment Parent { get; set; }
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
                                            + "  WHERE (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTCRP' OR  A.TransType='ACTCRP') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public class CashReplenishmentDetail
        {
            public virtual CashReplenishment Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual bool Checkbox { get; set; }
            public virtual string TransDocNumber { get; set; }
            public virtual DateTime TransDate { get; set; }
            public virtual string TransType { get; set; }
            public virtual decimal ExpenseAmount { get; set; }
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
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CashReplenishmentDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCashReplenishmentDetail(CashReplenishmentDetail CashReplenishmentDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) AS LineNumber FROM Accounting.CashReplenishmentDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Checkbox", CashReplenishmentDetail.Checkbox);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "TransDocNumber", CashReplenishmentDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "TransDate", CashReplenishmentDetail.TransDate);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "TransType", CashReplenishmentDetail.TransType);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "ExpenseAmount", CashReplenishmentDetail.ExpenseAmount);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field1", CashReplenishmentDetail.Field1);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field2", CashReplenishmentDetail.Field2);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field3", CashReplenishmentDetail.Field3);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field4", CashReplenishmentDetail.Field4);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field5", CashReplenishmentDetail.Field5);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field6", CashReplenishmentDetail.Field6);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field7", CashReplenishmentDetail.Field7);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field8", CashReplenishmentDetail.Field8);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Field9", CashReplenishmentDetail.Field9);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "0", "Version", "1");

                DT2.Rows.Add("Accounting.CashReplenishment", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CashReplenishment", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCashReplenishmentDetail(CashReplenishmentDetail CashReplenishmentDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "cond", "LineNumber", CashReplenishmentDetail.LineNumber);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Checkbox", CashReplenishmentDetail.Checkbox);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "TransDocNumber", CashReplenishmentDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "TransDate", CashReplenishmentDetail.TransDate);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "TransType", CashReplenishmentDetail.TransType);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "ExpenseAmount", CashReplenishmentDetail.ExpenseAmount);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field1", CashReplenishmentDetail.Field1);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field2", CashReplenishmentDetail.Field2);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field3", CashReplenishmentDetail.Field3);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field4", CashReplenishmentDetail.Field4);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field5", CashReplenishmentDetail.Field5);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field6", CashReplenishmentDetail.Field6);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field7", CashReplenishmentDetail.Field7);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field8", CashReplenishmentDetail.Field8);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Field9", CashReplenishmentDetail.Field9);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteCashReplenishmentDetail(CashReplenishmentDetail CashReplenishmentDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashReplenishmentDetail", "cond", "LineNumber", CashReplenishmentDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Procurement.CashReplenishmentDetail WHERE DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.CashReplenishment", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.CashReplenishment", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        public class JournalEntry
        {
            public virtual CashReplenishment Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCRP' ", Conn);

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

            a = Gears.RetriveData2("SELECT * FROM Accounting.CashReplenishment WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                TotalAmountReplenished = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountReplenished"]) ? 0 : dtRow["TotalAmountReplenished"]);
                FundSource = dtRow["FundSource"].ToString();
                
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
        public void InsertData(CashReplenishment _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CashReplenishment", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "TotalAmountReplenished", _ent.TotalAmountReplenished);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.CashReplenishment", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCRP", Docnum, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateData(CashReplenishment _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CashReplenishment", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "TotalAmountReplenished", _ent.TotalAmountReplenished);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "FundSource", _ent.FundSource);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashReplenishment", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCRP", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CashReplenishment _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CashReplenishment", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.CashReplenishmentDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);
            Functions.AuditTrail("ACTCRP", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable del = new DataTable();
            del = Gears.RetriveData2("DELETE FROM Accounting.CashReplenishmentDetail WHERE DocNumber ='" + DocNumber + "' AND Version = '1'", Conn);
        }
    }
}
