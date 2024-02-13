using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ChangeJODueDate
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string ReferenceJO { get; set; }
        public virtual string Reason { get; set; }
        public virtual string OldDueDate { get; set; }
        public virtual string NewDueDate { get; set; }
        public virtual string Customer { get; set; }
        public virtual string SODueDate { get; set; }
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
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual bool IsWithDetail { get; set; }


        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;
            
            a = Gears.RetriveData2("SELECT * FROM Production.ChangeJODueDate WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ReferenceJO = dtRow["ReferenceJO"].ToString();
                Reason = dtRow["Reason"].ToString();
                OldDueDate = dtRow["OldDueDate"].ToString();
                NewDueDate = dtRow["NewDueDate"].ToString();
                Customer = dtRow["Customer"].ToString();
                SODueDate = dtRow["SODueDate"].ToString();
                Remarks = dtRow["Remarks"].ToString();

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
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
             
            }

            return a;
        }
        public void InsertData(ChangeJODueDate _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ChangeJODueDate", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "ReferenceJO", _ent.ReferenceJO);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Reason", _ent.Reason);
            if (Convert.ToString(_ent.OldDueDate) != "1/1/0001 12:00:00 AM")
            {
                DT1.Rows.Add("Production.ChangeJODueDate", "0", "OldDueDate", _ent.OldDueDate);
            }
            if (Convert.ToString(_ent.NewDueDate) != "1/1/0001 12:00:00 AM")
            {
                DT1.Rows.Add("Production.ChangeJODueDate", "0", "NewDueDate", _ent.NewDueDate);
            }
            if (Convert.ToString(_ent.SODueDate) != "" && Convert.ToString(_ent.SODueDate) != " " && Convert.ToString(_ent.SODueDate) != "1/1/0001 12:00:00 AM")
            {
                DT1.Rows.Add("Production.ChangeJODueDate", "0", "SODueDate", _ent.SODueDate);
            }
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Customer", _ent.Customer);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.ChangeJODueDate", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.ChangeJODueDate", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(ChangeJODueDate _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ChangeJODueDate", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "ReferenceJO", _ent.ReferenceJO);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Reason", _ent.Reason);
            if (Convert.ToString(_ent.OldDueDate) != "1/1/0001 12:00:00 AM")
            {
                DT1.Rows.Add("Production.ChangeJODueDate", "set", "OldDueDate", _ent.OldDueDate);
            }
            if (Convert.ToString(_ent.NewDueDate) != "1/1/0001 12:00:00 AM")
            {
                DT1.Rows.Add("Production.ChangeJODueDate", "set", "NewDueDate", _ent.NewDueDate);
            }
            if (Convert.ToString(_ent.SODueDate) != "" && Convert.ToString(_ent.SODueDate) != " " && Convert.ToString(_ent.SODueDate) != "1/1/0001 12:00:00 AM")
            {
                DT1.Rows.Add("Production.ChangeJODueDate", "set", "SODueDate", _ent.SODueDate);
            }
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Customer", _ent.Customer);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.ChangeJODueDate", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.ChangeJODueDate", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDCHG", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ChangeJODueDate _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.ChangeJODueDate", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDCHG", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public class JournalEntry
        {
            public virtual ChangeJODueDate Parent { get; set; }
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
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit , A.BizPartnerCode FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='PRDCHG' ", Conn);

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
            public virtual ChangeJODueDate Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
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
    }
}
