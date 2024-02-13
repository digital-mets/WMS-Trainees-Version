using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ModifyJOStep
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string ReferenceJO { get; set; }
        public virtual string StepCode { get; set; }
        public virtual string SequenceNo { get; set; }
        public virtual decimal EstWOPrice { get; set; }
        public virtual decimal WorkOrderPrice { get; set; }
        public virtual decimal WorkOrderQty { get; set; }
        public virtual string WorkOrderDate { get; set; }
        public virtual string DateCommitted { get; set; }
        public virtual string WorkCenter { get; set; }
        public virtual string Overhead { get; set; }
        public virtual bool IsInHouse { get; set; }
        public virtual bool PreProd { get; set; }
        public virtual string Reason { get; set; }
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
        public virtual string RefRecordID { get; set; }
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
            
            a = Gears.RetriveData2("SELECT * FROM Production.ModifyJOStep WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                Type = dtRow["Type"].ToString();
                ReferenceJO = dtRow["ReferenceJO"].ToString();
                StepCode = dtRow["StepCode"].ToString();
                SequenceNo = dtRow["SequenceNo"].ToString();
                WorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["WorkOrderPrice"]) ? 0 : dtRow["WorkOrderPrice"]);
                EstWOPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstWOPrice"]) ? 0 : dtRow["EstWOPrice"]);
                WorkOrderQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["WorkOrderQty"]) ? 0 : dtRow["WorkOrderQty"]);
                DateCommitted = dtRow["DateCommitted"].ToString();
                WorkOrderDate = dtRow["WorkOrderDate"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                Overhead = dtRow["Overhead"].ToString();
                IsInHouse = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInHouse"]) ? false : dtRow["IsInHouse"]);
                PreProd = Convert.ToBoolean(Convert.IsDBNull(dtRow["PreProd"]) ? false : dtRow["PreProd"]);
                Reason = dtRow["Reason"].ToString();
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
                RefRecordID = dtRow["RefRecordID"].ToString();

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

        public void InsertData(ModifyJOStep _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ModifyJOStep", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Type", _ent.Type);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "ReferenceJO", _ent.ReferenceJO);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "StepCode", _ent.StepCode);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "SequenceNo", _ent.SequenceNo);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "WorkOrderPrice", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "EstWOPrice", _ent.EstWOPrice);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "WorkOrderQty", _ent.WorkOrderQty);
            if (!String.IsNullOrEmpty(_ent.DateCommitted.ToString()))
            {
                DT1.Rows.Add("Production.ModifyJOStep", "0", "DateCommitted", _ent.DateCommitted);
            }
            if (!String.IsNullOrEmpty(_ent.WorkOrderDate.ToString()))
            {
                DT1.Rows.Add("Production.ModifyJOStep", "0", "WorkOrderDate", _ent.WorkOrderDate);
            }
            DT1.Rows.Add("Production.ModifyJOStep", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Overhead", _ent.Overhead);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "IsInHouse", _ent.IsInHouse);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "PreProd", _ent.PreProd);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Reason", _ent.Reason);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Customer", _ent.Customer);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "SODueDate", _ent.SODueDate);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "RefRecordID", _ent.RefRecordID);

            DT1.Rows.Add("Production.ModifyJOStep", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.ModifyJOStep", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(ModifyJOStep _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ModifyJOStep", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Type", _ent.Type);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "ReferenceJO", _ent.ReferenceJO);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "StepCode", _ent.StepCode);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "SequenceNo", _ent.SequenceNo);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "WorkOrderPrice", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "EstWOPrice", _ent.EstWOPrice);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "WorkOrderQty", _ent.WorkOrderQty);
            if (!String.IsNullOrEmpty(_ent.DateCommitted.ToString()))
            {
                DT1.Rows.Add("Production.ModifyJOStep", "set", "DateCommitted", _ent.DateCommitted);
            }
            if (!String.IsNullOrEmpty(_ent.WorkOrderDate.ToString()))
            {
                DT1.Rows.Add("Production.ModifyJOStep", "set", "WorkOrderDate", _ent.WorkOrderDate);
            }
            DT1.Rows.Add("Production.ModifyJOStep", "set", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Overhead", _ent.Overhead);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "IsInHouse", _ent.IsInHouse);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "PreProd", _ent.PreProd);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Reason", _ent.Reason);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Customer", _ent.Customer);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "SODueDate", _ent.SODueDate);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "RefRecordID", _ent.RefRecordID);

            DT1.Rows.Add("Production.ModifyJOStep", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.ModifyJOStep", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDMOD", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ModifyJOStep _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.ModifyJOStep", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDMOD", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public class RefTransaction
        {
            public virtual ModifyJOStep Parent { get; set; }
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

        public class JournalEntry
        {
            public virtual ModifyJOStep Parent { get; set; }
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
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit,A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='PRDMOD' ", Conn);

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
