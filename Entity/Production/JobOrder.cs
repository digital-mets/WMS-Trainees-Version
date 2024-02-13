using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class JobOrder
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string DueDate { get; set; }
        public virtual Int32 Leadtime { get; set; }
        public virtual string ProdDate { get; set; }
        public virtual string SODueDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Status { get; set; }
        public virtual string DateCompleted { get; set; }
        public virtual string DISNumber { get; set; }
        public virtual string PISNumber { get; set; }
        public virtual string StepTemplateNo { get; set; }
        public virtual string Designer { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string ParentStepcode { get; set; }
        public virtual string OriginalJO { get; set; }
        public virtual string CustomerBrand { get; set; }
        public virtual bool IsMultiIn { get; set; }
        public virtual bool IsAutoJO { get; set; }
        public virtual decimal TotalJOQty { get; set; }
        public virtual decimal TotalSOQty { get; set; }
        public virtual decimal TotalINQty { get; set; }
        public virtual decimal TotalFinalQty { get; set; }
        public virtual decimal SRP { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal TotalDirectLabor { get; set; }
        public virtual decimal TotalDirecMat { get; set; }
        public virtual decimal TotalOverhead { get; set; }
        public virtual decimal OverheadAdj { get; set; }
        public virtual decimal UnitCost { get; set; }
        public virtual decimal EstAccCost { get; set; }
        public virtual decimal EstUnitCost { get; set; }
        public virtual decimal StdOHCost { get; set; }
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
        public virtual string AllocSubmittedBy { get; set; }
        public virtual string AllocSubmittedDate { get; set; }
        public virtual string ProdSubmittedBy { get; set; }
        public virtual string ProdSubmittedDate { get; set; }
        public virtual string ApprovedLeadBy { get; set; }
        public virtual string ApprovedLeadDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string ManualClosedBy { get; set; }
        public virtual string ManualClosedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual IList<JOProductOrder> JOProductOrderDetail { get; set; }
        public virtual IList<JOBillOfMaterial> JOBillOfMaterialDetail { get; set; }
        public virtual IList<JOStep> JOStepDetail { get; set; }
        public virtual IList<JOStepPlanning> JOStepPlanningDetail { get; set; }
        public virtual IList<JOClassBreakdown> JOClassBreakdownDetail { get; set; }
        public virtual IList<JOSizeBreakdown> JOSizeBreakdownDetail { get; set; }
        public virtual IList<JOMaterialMovement> JOMaterialMovementDetail { get; set; }

        #region Journal Entry
        public class JournalEntry
        {
            public virtual JobOrder Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='PRDJOB' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        #endregion

        #region Reference Transactions
        public class RefTransaction
        {
            public virtual JobOrder Parent { get; set; }
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
                    a = Gears.RetriveData2("select RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B on A.RMenuID = B.ModuleID inner join IT.MainMenu C on A.MenuID = C.ModuleID "
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDJOB' OR  A.TransType='PRDJOB') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        #endregion

        #region JobOrder
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Production.JobOrder WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                DueDate = dtRow["DueDate"].ToString();
                Leadtime = Convert.ToInt32(Convert.IsDBNull(dtRow["Leadtime"]) ? -100 : dtRow["Leadtime"]);
                ProdDate = dtRow["ProdDate"].ToString();
                SODueDate = dtRow["SODueDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Status = dtRow["Status"].ToString();
                DateCompleted = dtRow["DateCompleted"].ToString();
                DISNumber = dtRow["DISNumber"].ToString();
                PISNumber = dtRow["PISNumber"].ToString();
                StepTemplateNo = dtRow["StepTemplateNo"].ToString();
                Designer = dtRow["Designer"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                ParentStepcode = dtRow["ParentStepcode"].ToString();
                OriginalJO = dtRow["OriginalJO"].ToString();
                CustomerBrand = dtRow["CustomerBrand"].ToString();
                IsMultiIn = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsMultiIn"]) ? false : dtRow["IsMultiIn"]);
                IsAutoJO = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsAutoJO"]) ? false : dtRow["IsAutoJO"]);
                TotalJOQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalJOQty"]) ? 0 : dtRow["TotalJOQty"]);
                TotalSOQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalSOQty"]) ? 0 : dtRow["TotalSOQty"]);
                TotalINQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalINQty"]) ? 0 : dtRow["TotalINQty"]);
                TotalFinalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalFinalQty"]) ? 0 : dtRow["TotalFinalQty"]);
                SRP = Convert.ToDecimal(Convert.IsDBNull(dtRow["SRP"]) ? 0 : dtRow["SRP"]);
                Currency = dtRow["Currency"].ToString();
                TotalDirectLabor = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalDirectLabor"]) ? 0 : dtRow["TotalDirectLabor"]);
                TotalDirecMat = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalDirecMat"]) ? 0 : dtRow["TotalDirecMat"]);
                TotalOverhead = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalOverhead"]) ? 0 : dtRow["TotalOverhead"]);
                OverheadAdj = Convert.ToDecimal(Convert.IsDBNull(dtRow["OverheadAdj"]) ? 0 : dtRow["OverheadAdj"]);
                UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                EstAccCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstAccCost"]) ? 0 : dtRow["EstAccCost"]);
                EstUnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstUnitCost"]) ? 0 : dtRow["EstUnitCost"]);
                StdOHCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["StdOHCost"]) ? 0 : dtRow["StdOHCost"]);

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
                AllocSubmittedBy = dtRow["AllocSubmittedBy"].ToString();
                AllocSubmittedDate = dtRow["AllocSubmittedDate"].ToString();
                ProdSubmittedBy = dtRow["ProdSubmittedBy"].ToString();
                ProdSubmittedDate = dtRow["ProdSubmittedDate"].ToString();
                ApprovedLeadBy = dtRow["ApprovedLeadBy"].ToString();
                ApprovedLeadDate = dtRow["ApprovedLeadDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                ManualClosedBy = dtRow["ManualClosedBy"].ToString();
                ManualClosedDate = dtRow["ManualClosedDate"].ToString();
            }

            return a;
        }
        public void InsertData(JobOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.JobOrder", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.JobOrder", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.JobOrder", "0", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Production.JobOrder", "0", "Leadtime", _ent.Leadtime);
            if (Convert.ToString(_ent.ProdDate) != "1/1/0001 12:00:00 AM" && !String.IsNullOrEmpty(_ent.ProdDate)
                && _ent.ProdDate != null && _ent.ProdDate != " " && _ent.ProdDate != "")
            {
                DT1.Rows.Add("Production.JobOrder", "0", "ProdDate", _ent.ProdDate);
            }
            if (Convert.ToString(_ent.SODueDate) != "1/1/0001 12:00:00 AM" && !String.IsNullOrEmpty(_ent.SODueDate)
                && _ent.SODueDate != null && _ent.SODueDate != " " && _ent.SODueDate != "")
            {
                DT1.Rows.Add("Production.JobOrder", "0", "SODueDate", _ent.SODueDate);
            }
            DT1.Rows.Add("Production.JobOrder", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.JobOrder", "0", "Status", _ent.Status);
            DT1.Rows.Add("Production.JobOrder", "0", "DISNumber", _ent.DISNumber);
            DT1.Rows.Add("Production.JobOrder", "0", "PISNumber", _ent.PISNumber);
            DT1.Rows.Add("Production.JobOrder", "0", "StepTemplateNo", _ent.StepTemplateNo);
            DT1.Rows.Add("Production.JobOrder", "0", "Designer", _ent.Designer);
            DT1.Rows.Add("Production.JobOrder", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.JobOrder", "0", "ParentStepcode", _ent.ParentStepcode);
            DT1.Rows.Add("Production.JobOrder", "0", "OriginalJO", _ent.OriginalJO);
            DT1.Rows.Add("Production.JobOrder", "0", "CustomerBrand", _ent.CustomerBrand);
            DT1.Rows.Add("Production.JobOrder", "0", "IsMultiIn", _ent.IsMultiIn);
            DT1.Rows.Add("Production.JobOrder", "0", "IsAutoJO", _ent.IsAutoJO);
            DT1.Rows.Add("Production.JobOrder", "0", "TotalJOQty", _ent.TotalJOQty);
            DT1.Rows.Add("Production.JobOrder", "0", "TotalSOQty", _ent.TotalSOQty);
            DT1.Rows.Add("Production.JobOrder", "0", "TotalINQty", _ent.TotalINQty);
            DT1.Rows.Add("Production.JobOrder", "0", "TotalFinalQty", _ent.TotalFinalQty);
            DT1.Rows.Add("Production.JobOrder", "0", "SRP", _ent.SRP);
            DT1.Rows.Add("Production.JobOrder", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Production.JobOrder", "0", "TotalDirectLabor", _ent.TotalDirectLabor);
            DT1.Rows.Add("Production.JobOrder", "0", "TotalDirecMat", _ent.TotalDirecMat);
            DT1.Rows.Add("Production.JobOrder", "0", "TotalOverhead", _ent.TotalOverhead);
            DT1.Rows.Add("Production.JobOrder", "0", "OverheadAdj", _ent.OverheadAdj);
            DT1.Rows.Add("Production.JobOrder", "0", "UnitCost", _ent.UnitCost);
            DT1.Rows.Add("Production.JobOrder", "0", "EstAccCost", _ent.EstAccCost);
            DT1.Rows.Add("Production.JobOrder", "0", "EstUnitCost", _ent.EstUnitCost);
            DT1.Rows.Add("Production.JobOrder", "0", "StdOHCost", _ent.StdOHCost);
            DT1.Rows.Add("Production.JobOrder", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.JobOrder", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.JobOrder", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.JobOrder", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.JobOrder", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.JobOrder", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.JobOrder", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.JobOrder", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.JobOrder", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.JobOrder", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.JobOrder", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(JobOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.JobOrder", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.JobOrder", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.JobOrder", "set", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Production.JobOrder", "set", "Leadtime", _ent.Leadtime);
            if (Convert.ToString(_ent.ProdDate) != "1/1/0001 12:00:00 AM" && !String.IsNullOrEmpty(_ent.ProdDate)
                && _ent.ProdDate != null && _ent.ProdDate != " " && _ent.ProdDate != "")
            {
                DT1.Rows.Add("Production.JobOrder", "set", "ProdDate", _ent.ProdDate);
            }
            if (Convert.ToString(_ent.SODueDate) != "1/1/0001 12:00:00 AM" && !String.IsNullOrEmpty(_ent.SODueDate)
                && _ent.SODueDate != null && _ent.SODueDate != " " && _ent.SODueDate != "")
            {
                DT1.Rows.Add("Production.JobOrder", "set", "SODueDate", _ent.SODueDate);
            }
            DT1.Rows.Add("Production.JobOrder", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.JobOrder", "set", "Status", _ent.Status);
            DT1.Rows.Add("Production.JobOrder", "set", "DISNumber", _ent.DISNumber);
            DT1.Rows.Add("Production.JobOrder", "set", "PISNumber", _ent.PISNumber);
            DT1.Rows.Add("Production.JobOrder", "set", "StepTemplateNo", _ent.StepTemplateNo);
            DT1.Rows.Add("Production.JobOrder", "set", "Designer", _ent.Designer);
            DT1.Rows.Add("Production.JobOrder", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.JobOrder", "set", "ParentStepcode", _ent.ParentStepcode);
            DT1.Rows.Add("Production.JobOrder", "set", "OriginalJO", _ent.OriginalJO);
            DT1.Rows.Add("Production.JobOrder", "set", "CustomerBrand", _ent.CustomerBrand);
            DT1.Rows.Add("Production.JobOrder", "set", "IsMultiIn", _ent.IsMultiIn);
            DT1.Rows.Add("Production.JobOrder", "set", "IsAutoJO", _ent.IsAutoJO);
            DT1.Rows.Add("Production.JobOrder", "set", "TotalJOQty", _ent.TotalJOQty);
            DT1.Rows.Add("Production.JobOrder", "set", "TotalSOQty", _ent.TotalSOQty);
            DT1.Rows.Add("Production.JobOrder", "set", "TotalINQty", _ent.TotalINQty);
            DT1.Rows.Add("Production.JobOrder", "set", "TotalFinalQty", _ent.TotalFinalQty);
            DT1.Rows.Add("Production.JobOrder", "set", "SRP", _ent.SRP);
            DT1.Rows.Add("Production.JobOrder", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Production.JobOrder", "set", "TotalDirectLabor", _ent.TotalDirectLabor);
            DT1.Rows.Add("Production.JobOrder", "set", "TotalDirecMat", _ent.TotalDirecMat);
            DT1.Rows.Add("Production.JobOrder", "set", "TotalOverhead", _ent.TotalOverhead);
            DT1.Rows.Add("Production.JobOrder", "set", "OverheadAdj", _ent.OverheadAdj);
            DT1.Rows.Add("Production.JobOrder", "set", "UnitCost", _ent.UnitCost);
            DT1.Rows.Add("Production.JobOrder", "set", "EstAccCost", _ent.EstAccCost);
            DT1.Rows.Add("Production.JobOrder", "set", "EstUnitCost", _ent.EstUnitCost);
            DT1.Rows.Add("Production.JobOrder", "set", "StdOHCost", _ent.StdOHCost);
            DT1.Rows.Add("Production.JobOrder", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.JobOrder", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.JobOrder", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.JobOrder", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.JobOrder", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.JobOrder", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.JobOrder", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.JobOrder", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.JobOrder", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.JobOrder", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.JobOrder", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDJOB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(JobOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Production.JOProductOrder", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DT3.Rows.Add("Production.JOBillOfMaterial", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT3, _ent.Connection);

            Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
            DT4.Rows.Add("Production.JOStep", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT4, _ent.Connection);

            Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
            DT5.Rows.Add("Production.JOStepPlanning", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT5, _ent.Connection);

            Gears.CRUDdatatable DT6 = new Gears.CRUDdatatable();
            DT6.Rows.Add("Production.JOClassBreakdown", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT6, _ent.Connection);

            Gears.CRUDdatatable DT7 = new Gears.CRUDdatatable();
            DT7.Rows.Add("Production.JOSizeBreakdown", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT7, _ent.Connection);

            Gears.CRUDdatatable DT8 = new Gears.CRUDdatatable();
            DT8.Rows.Add("Production.JOMaterialMovement", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT8, _ent.Connection);

            Functions.AuditTrail("PRDJOB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    #endregion

        #region JOProductOrder
        public class JOProductOrder
        {
            public virtual JobOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ReferenceSO { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal SOQty { get; set; }
            public virtual decimal JOQty { get; set; }
            public virtual decimal INQty { get; set; }
            public virtual decimal FinalQty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }

            public DataTable getJOProductOrder(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.JOProductOrder WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddJOProductOrder(JOProductOrder JOProductOrder)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.JOProductOrder WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Production.JOProductOrder", "0", "DocNumber", Docnum);
                //DT1.Rows.Add("Production.JOProductOrder", "0", "DocNumber", JOProductOrder.DocNumber);
                DT1.Rows.Add("Production.JOProductOrder", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.JOProductOrder", "0", "ReferenceSO", JOProductOrder.ReferenceSO);
                DT1.Rows.Add("Production.JOProductOrder", "0", "ItemCode", JOProductOrder.ItemCode);
                DT1.Rows.Add("Production.JOProductOrder", "0", "FullDesc", JOProductOrder.FullDesc);
                DT1.Rows.Add("Production.JOProductOrder", "0", "ColorCode", JOProductOrder.ColorCode);
                DT1.Rows.Add("Production.JOProductOrder", "0", "ClassCode", JOProductOrder.ClassCode);
                DT1.Rows.Add("Production.JOProductOrder", "0", "SizeCode", JOProductOrder.SizeCode);
                DT1.Rows.Add("Production.JOProductOrder", "0", "SOQty", JOProductOrder.SOQty);
                DT1.Rows.Add("Production.JOProductOrder", "0", "JOQty", JOProductOrder.JOQty);
                DT1.Rows.Add("Production.JOProductOrder", "0", "INQty", JOProductOrder.INQty);
                DT1.Rows.Add("Production.JOProductOrder", "0", "FinalQty", JOProductOrder.FinalQty);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field1", JOProductOrder.Field1);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field2", JOProductOrder.Field2);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field3", JOProductOrder.Field3);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field4", JOProductOrder.Field4);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field5", JOProductOrder.Field5);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field6", JOProductOrder.Field6);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field7", JOProductOrder.Field7);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field8", JOProductOrder.Field8);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Field9", JOProductOrder.Field9);
                DT1.Rows.Add("Production.JOProductOrder", "0", "Version", "1");

                DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateJOProductOrder(JOProductOrder JOProductOrder)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.JOProductOrder", "cond", "DocNumber", Docnum );
                DT1.Rows.Add("Production.JOProductOrder", "cond", "LineNumber", JOProductOrder.LineNumber);
                DT1.Rows.Add("Production.JOProductOrder", "set", "ReferenceSO", JOProductOrder.ReferenceSO);
                DT1.Rows.Add("Production.JOProductOrder", "set", "ItemCode", JOProductOrder.ItemCode);
                DT1.Rows.Add("Production.JOProductOrder", "set", "FullDesc", JOProductOrder.FullDesc);
                DT1.Rows.Add("Production.JOProductOrder", "set", "ColorCode", JOProductOrder.ColorCode);
                DT1.Rows.Add("Production.JOProductOrder", "set", "ClassCode", JOProductOrder.ClassCode);
                DT1.Rows.Add("Production.JOProductOrder", "set", "SizeCode", JOProductOrder.SizeCode);
                DT1.Rows.Add("Production.JOProductOrder", "set", "SOQty", JOProductOrder.SOQty);
                DT1.Rows.Add("Production.JOProductOrder", "set", "JOQty", JOProductOrder.JOQty);
                DT1.Rows.Add("Production.JOProductOrder", "set", "INQty", JOProductOrder.INQty);
                DT1.Rows.Add("Production.JOProductOrder", "set", "FinalQty", JOProductOrder.FinalQty);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field1", JOProductOrder.Field1);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field2", JOProductOrder.Field2);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field3", JOProductOrder.Field3);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field4", JOProductOrder.Field4);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field5", JOProductOrder.Field5);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field6", JOProductOrder.Field6);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field7", JOProductOrder.Field7);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field8", JOProductOrder.Field8);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Field9", JOProductOrder.Field9);
                DT1.Rows.Add("Production.JOProductOrder", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteJOProductOrder(JOProductOrder JOProductOrder)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.JOProductOrder", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOProductOrder", "cond", "LineNumber", JOProductOrder.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable JOProduct = Gears.RetriveData2("SELECT * FROM Production.JOProductOrder WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOBOM = Gears.RetriveData2("SELECT * FROM Production.JOBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSteps = Gears.RetriveData2("SELECT * FROM Production.JOStep WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOStepPlan = Gears.RetriveData2("SELECT * FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOClass = Gears.RetriveData2("SELECT * FROM Production.JOClassBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSize = Gears.RetriveData2("SELECT * FROM Production.JOSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOMaterial = Gears.RetriveData2("SELECT * FROM Production.JOMaterialMovement WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((JOProduct.Rows.Count < 1) && (JOBOM.Rows.Count < 1) && (JOSteps.Rows.Count < 1) && (JOStepPlan.Rows.Count < 1)
                    && (JOClass.Rows.Count < 1) && (JOSize.Rows.Count < 1) && (JOMaterial.Rows.Count < 1))
                {
                    DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        #endregion

        #region JOBillOfMaterial
        public class JOBillOfMaterial
        {
            public virtual JobOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Components { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string ProductCode { get; set; }
            public virtual string ProductColor { get; set; }
            public virtual string ProductSize { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal PerPieceConsumption { get; set; }
            public virtual decimal Consumption { get; set; }
            public virtual decimal AllowancePerc { get; set; }
            public virtual decimal AllowanceQty { get; set; }
            public virtual decimal RequiredQty { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual bool IsMajorMaterial { get; set; }
            public virtual bool IsBulk { get; set; }
            public virtual bool IsRounded { get; set; }
            public virtual bool IsExcluded { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }

            public DataTable getJOBillOfMaterial(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.JOBillOfMaterial WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddJOBillOfMaterial(JOBillOfMaterial JOBillOfMaterial)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.JOBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);

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
                //DT1.Rows.Add("Production.JOBillOfMaterial", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "DocNumber", JOBillOfMaterial.DocNumber);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Components", JOBillOfMaterial.Components);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "StepCode", JOBillOfMaterial.StepCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "ProductCode", JOBillOfMaterial.ProductCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "ProductColor", JOBillOfMaterial.ProductColor);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "ProductSize", JOBillOfMaterial.ProductSize);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "ItemCode", JOBillOfMaterial.ItemCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "FullDesc", JOBillOfMaterial.FullDesc);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "ColorCode", JOBillOfMaterial.ColorCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "ClassCode", JOBillOfMaterial.ClassCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "SizeCode", JOBillOfMaterial.SizeCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Unit", JOBillOfMaterial.Unit);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "PerPieceConsumption", JOBillOfMaterial.PerPieceConsumption);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Consumption", JOBillOfMaterial.Consumption);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "AllowancePerc", JOBillOfMaterial.AllowancePerc);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "AllowanceQty", JOBillOfMaterial.AllowanceQty);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "RequiredQty", JOBillOfMaterial.RequiredQty);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "UnitCost", JOBillOfMaterial.UnitCost);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "IsMajorMaterial", JOBillOfMaterial.IsMajorMaterial);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "IsBulk", JOBillOfMaterial.IsBulk);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "IsRounded", JOBillOfMaterial.IsRounded);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "IsExcluded", JOBillOfMaterial.IsExcluded);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field1", JOBillOfMaterial.Field1);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field2", JOBillOfMaterial.Field2);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field3", JOBillOfMaterial.Field3);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field4", JOBillOfMaterial.Field4);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field5", JOBillOfMaterial.Field5);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field6", JOBillOfMaterial.Field6);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field7", JOBillOfMaterial.Field7);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field8", JOBillOfMaterial.Field8);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Field9", JOBillOfMaterial.Field9);
                DT1.Rows.Add("Production.JOBillOfMaterial", "0", "Version", "1");

                DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateJOBillOfMaterial(JOBillOfMaterial JOBillOfMaterial)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.JOBillOfMaterial", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOBillOfMaterial", "cond", "LineNumber", JOBillOfMaterial.LineNumber);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Components", JOBillOfMaterial.Components);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "StepCode", JOBillOfMaterial.StepCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "ProductCode", JOBillOfMaterial.ProductCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "ProductColor", JOBillOfMaterial.ProductColor);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "ProductSize", JOBillOfMaterial.ProductSize);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "ItemCode", JOBillOfMaterial.ItemCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "FullDesc", JOBillOfMaterial.FullDesc);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "ColorCode", JOBillOfMaterial.ColorCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "ClassCode", JOBillOfMaterial.ClassCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "SizeCode", JOBillOfMaterial.SizeCode);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Unit", JOBillOfMaterial.Unit);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "PerPieceConsumption", JOBillOfMaterial.PerPieceConsumption);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Consumption", JOBillOfMaterial.Consumption);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "AllowancePerc", JOBillOfMaterial.AllowancePerc);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "AllowanceQty", JOBillOfMaterial.AllowanceQty);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "RequiredQty", JOBillOfMaterial.RequiredQty);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "UnitCost", JOBillOfMaterial.UnitCost);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "IsMajorMaterial", JOBillOfMaterial.IsMajorMaterial);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "IsBulk", JOBillOfMaterial.IsBulk);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "IsRounded", JOBillOfMaterial.IsRounded);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "IsExcluded", JOBillOfMaterial.IsExcluded);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field1", JOBillOfMaterial.Field1);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field2", JOBillOfMaterial.Field2);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field3", JOBillOfMaterial.Field3);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field4", JOBillOfMaterial.Field4);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field5", JOBillOfMaterial.Field5);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field6", JOBillOfMaterial.Field6);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field7", JOBillOfMaterial.Field7);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field8", JOBillOfMaterial.Field8);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Field9", JOBillOfMaterial.Field9);
                DT1.Rows.Add("Production.JOBillOfMaterial", "set", "Version", "1");


                Gears.UpdateData(DT1, Conn);   
            }
            public void DeleteJOBillOfMaterial(JOBillOfMaterial JOBillOfMaterial)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.JOBillOfMaterial", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOBillOfMaterial", "cond", "LineNumber", JOBillOfMaterial.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable JOProduct = Gears.RetriveData2("SELECT * FROM Production.JOProductOrder WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOBOM = Gears.RetriveData2("SELECT * FROM Production.JOBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSteps = Gears.RetriveData2("SELECT * FROM Production.JOStep WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOStepPlan = Gears.RetriveData2("SELECT * FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOClass = Gears.RetriveData2("SELECT * FROM Production.JOClassBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSize = Gears.RetriveData2("SELECT * FROM Production.JOSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOMaterial = Gears.RetriveData2("SELECT * FROM Production.JOMaterialMovement WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((JOProduct.Rows.Count < 1) && (JOBOM.Rows.Count < 1) && (JOSteps.Rows.Count < 1) && (JOStepPlan.Rows.Count < 1)
                    && (JOClass.Rows.Count < 1) && (JOSize.Rows.Count < 1) && (JOMaterial.Rows.Count < 1))
                {
                    DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        #endregion

        #region JOStep
        public class JOStep
        {
            public virtual JobOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string StepCode { get; set; }
            public virtual decimal InQty { get; set; }
            public virtual decimal OutQty { get; set; }
            public virtual decimal AdjQty { get; set; }
            public virtual decimal Allowance { get; set; }
            public virtual decimal Yield { get; set; }
            public virtual decimal ActualLoss { get; set; }
            public virtual decimal ActualYield { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }

            public DataTable getJOStep(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.JOStep WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddJOStep(JOStep JOStep)
            {
                int linenum = 0;

                //DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.JOStep WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "' and StepCode = '" + JOStep.StepCode + "'", Conn);

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
                //DT1.Rows.Add("Production.JOStep", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOStep", "0", "DocNumber", JOStep.DocNumber);
                DT1.Rows.Add("Production.JOStep", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.JOStep", "0", "StepCode", JOStep.StepCode);
                DT1.Rows.Add("Production.JOStep", "0", "InQty", JOStep.InQty);
                DT1.Rows.Add("Production.JOStep", "0", "OutQty", JOStep.OutQty);
                DT1.Rows.Add("Production.JOStep", "0", "AdjQty", JOStep.AdjQty);
                DT1.Rows.Add("Production.JOStep", "0", "Allowance", JOStep.Allowance);
                DT1.Rows.Add("Production.JOStep", "0", "Yield", JOStep.Yield);
                DT1.Rows.Add("Production.JOStep", "0", "ActualLoss", JOStep.ActualLoss);
                DT1.Rows.Add("Production.JOStep", "0", "ActualYield", JOStep.ActualYield);
                DT1.Rows.Add("Production.JOStep", "0", "Field1", JOStep.Field1);
                DT1.Rows.Add("Production.JOStep", "0", "Field2", JOStep.Field2);
                DT1.Rows.Add("Production.JOStep", "0", "Field3", JOStep.Field3);
                DT1.Rows.Add("Production.JOStep", "0", "Field4", JOStep.Field4);
                DT1.Rows.Add("Production.JOStep", "0", "Field5", JOStep.Field5);
                DT1.Rows.Add("Production.JOStep", "0", "Field6", JOStep.Field6);
                DT1.Rows.Add("Production.JOStep", "0", "Field7", JOStep.Field7);
                DT1.Rows.Add("Production.JOStep", "0", "Field8", JOStep.Field8);
                DT1.Rows.Add("Production.JOStep", "0", "Field9", JOStep.Field9);
                DT1.Rows.Add("Production.JOStep", "0", "Version", "1");
                
                DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateJOStep(JOStep JOStep)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.JOStep", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOStep", "cond", "LineNumber", JOStep.LineNumber);
                DT1.Rows.Add("Production.JOStep", "set", "StepCode", JOStep.StepCode);
                DT1.Rows.Add("Production.JOStep", "set", "InQty", JOStep.InQty);
                DT1.Rows.Add("Production.JOStep", "set", "OutQty", JOStep.OutQty);
                DT1.Rows.Add("Production.JOStep", "set", "AdjQty", JOStep.AdjQty);
                DT1.Rows.Add("Production.JOStep", "set", "Allowance", JOStep.Allowance);
                DT1.Rows.Add("Production.JOStep", "set", "Yield", JOStep.Yield);
                DT1.Rows.Add("Production.JOStep", "set", "ActualLoss", JOStep.ActualLoss);
                DT1.Rows.Add("Production.JOStep", "set", "ActualYield", JOStep.ActualYield);
                DT1.Rows.Add("Production.JOStep", "set", "Field1", JOStep.Field1);
                DT1.Rows.Add("Production.JOStep", "set", "Field2", JOStep.Field2);
                DT1.Rows.Add("Production.JOStep", "set", "Field3", JOStep.Field3);
                DT1.Rows.Add("Production.JOStep", "set", "Field4", JOStep.Field4);
                DT1.Rows.Add("Production.JOStep", "set", "Field5", JOStep.Field5);
                DT1.Rows.Add("Production.JOStep", "set", "Field6", JOStep.Field6);
                DT1.Rows.Add("Production.JOStep", "set", "Field7", JOStep.Field7);
                DT1.Rows.Add("Production.JOStep", "set", "Field8", JOStep.Field8);
                DT1.Rows.Add("Production.JOStep", "set", "Field9", JOStep.Field9);
                DT1.Rows.Add("Production.JOStep", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteJOStep(JOStep JOStep)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.JOStep", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOStep", "cond", "LineNumber", JOStep.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable JOProduct = Gears.RetriveData2("SELECT * FROM Production.JOProductOrder WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOBOM = Gears.RetriveData2("SELECT * FROM Production.JOBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSteps = Gears.RetriveData2("SELECT * FROM Production.JOStep WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOStepPlan = Gears.RetriveData2("SELECT * FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOClass = Gears.RetriveData2("SELECT * FROM Production.JOClassBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSize = Gears.RetriveData2("SELECT * FROM Production.JOSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOMaterial = Gears.RetriveData2("SELECT * FROM Production.JOMaterialMovement WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((JOProduct.Rows.Count < 1) && (JOBOM.Rows.Count < 1) && (JOSteps.Rows.Count < 1) && (JOStepPlan.Rows.Count < 1)
                    && (JOClass.Rows.Count < 1) && (JOSize.Rows.Count < 1) && (JOMaterial.Rows.Count < 1))
                {
                    DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        #endregion

        #region JOStepPlanning
        public class JOStepPlanning
        {
            public virtual JobOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual decimal Sequence { get; set; }
            public virtual bool PreProd { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string WorkCenter { get; set; }
            public virtual string Overhead { get; set; }
            public virtual bool IsInhouse { get; set; }
            public virtual decimal InQty { get; set; }
            public virtual decimal OutQty { get; set; }
            public virtual decimal AdjQty { get; set; }
            public virtual decimal Allowance { get; set; }
            public virtual decimal Yield { get; set; }
            public virtual decimal ActualLoss { get; set; }
            public virtual decimal ActualYield { get; set; }
            public virtual string Instruction { get; set; }
            public virtual decimal EstWorkOrderPrice { get; set; }
            public virtual decimal WorkOrderPrice { get; set; }
            public virtual DateTime WorkOrderDate { get; set; }
            public virtual decimal WorkOrderQty { get; set; }
            public virtual DateTime DateCommitted { get; set; }
            public virtual string VAT { get; set; }
            public virtual decimal VATRate { get; set; }
            public virtual DateTime TargetDateIn { get; set; }
            public virtual DateTime TargetDateOut { get; set; }
            public virtual DateTime ActualDateIn { get; set; }
            public virtual DateTime ActualDateOut { get; set; }
            public virtual string WOPrint { get; set; }
            public virtual string WOPrint1 { get; set; }
            public virtual string ForCallback { get; set; }
            public virtual string StepTemplate { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }
            public virtual decimal OHRate { get; set; }
            public virtual string OHType { get; set; }
            public virtual decimal MinPrice { get; set; }
            public virtual decimal MaxPrice { get; set; }


            public DataTable getJOStepPlanning(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT DISTINCT A.*, '' AS WOPrint, '' AS WOPrint1, '' AS ForCallback, ISNULL(B.MinimumWOPrice,0) AS MinPrice, ISNULL(B.MaximumWOPrice,0) AS MaxPrice FROM Production.JOStepPlanning A LEFT JOIN Masterfile.Step B "
                            + " ON A.StepCode = B.StepCode WHERE A.DocNumber='" + DocNumber + "' ORDER BY PreProd DESC, Sequence ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddJOStepPlanning(JOStepPlanning JOStepPlanning)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "'", Conn);

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
                //DT1.Rows.Add("Production.JOStepPlanning", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "DocNumber", JOStepPlanning.DocNumber);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Sequence", JOStepPlanning.Sequence);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "PreProd", JOStepPlanning.PreProd);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "StepCode", JOStepPlanning.StepCode);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "WorkCenter", JOStepPlanning.WorkCenter);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Overhead", JOStepPlanning.Overhead);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "IsInhouse", JOStepPlanning.IsInhouse);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "InQty", JOStepPlanning.InQty);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "OutQty", JOStepPlanning.OutQty);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "AdjQty", JOStepPlanning.AdjQty);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Allowance", JOStepPlanning.Allowance);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Yield", JOStepPlanning.Yield);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "ActualLoss", JOStepPlanning.ActualLoss);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "ActualYield", JOStepPlanning.ActualYield);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Instruction", JOStepPlanning.Instruction);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "EstWorkOrderPrice", JOStepPlanning.EstWorkOrderPrice);
                if (Convert.ToString(JOStepPlanning.WorkOrderDate) != "1/1/0001 12:00:00 AM" && JOStepPlanning.WorkOrderDate != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "0", "WorkOrderDate", JOStepPlanning.WorkOrderDate);
                }
                DT1.Rows.Add("Production.JOStepPlanning", "0", "WorkOrderPrice", JOStepPlanning.WorkOrderPrice);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "WorkOrderQty", JOStepPlanning.WorkOrderQty);
                if (Convert.ToString(JOStepPlanning.DateCommitted) != "1/1/0001 12:00:00 AM" && JOStepPlanning.DateCommitted != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "0", "DateCommitted", JOStepPlanning.DateCommitted);
                }
                DT1.Rows.Add("Production.JOStepPlanning", "0", "VAT", JOStepPlanning.VAT);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "VATRate", JOStepPlanning.VATRate);
                if (Convert.ToString(JOStepPlanning.TargetDateIn) != "1/1/0001 12:00:00 AM" && JOStepPlanning.TargetDateIn != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "0", "TargetDateIn", JOStepPlanning.TargetDateIn);
                }
                if (Convert.ToString(JOStepPlanning.TargetDateOut) != "1/1/0001 12:00:00 AM" && JOStepPlanning.TargetDateOut != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "0", "TargetDateOut", JOStepPlanning.TargetDateOut);
                }
                if (Convert.ToString(JOStepPlanning.ActualDateIn) != "1/1/0001 12:00:00 AM" && JOStepPlanning.ActualDateIn != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "0", "ActualDateIn", JOStepPlanning.ActualDateIn);
                }
                if (Convert.ToString(JOStepPlanning.ActualDateOut) != "1/1/0001 12:00:00 AM" && JOStepPlanning.ActualDateOut != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "0", "ActualDateOut", JOStepPlanning.ActualDateOut);
                }
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field1", JOStepPlanning.Field1);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field2", JOStepPlanning.Field2);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field3", JOStepPlanning.Field3);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field4", JOStepPlanning.Field4);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field5", JOStepPlanning.Field5);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field6", JOStepPlanning.Field6);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field7", JOStepPlanning.Field7);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field8", JOStepPlanning.Field8);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Field9", JOStepPlanning.Field9);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "OHRate", JOStepPlanning.OHRate);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "OHType", JOStepPlanning.OHType);
                DT1.Rows.Add("Production.JOStepPlanning", "0", "Version", "1");

                DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateJOStepPlanning(JOStepPlanning JOStepPlanning)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.JOStepPlanning", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOStepPlanning", "cond", "LineNumber", JOStepPlanning.LineNumber);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Sequence", JOStepPlanning.Sequence);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "PreProd", JOStepPlanning.PreProd);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "StepCode", JOStepPlanning.StepCode);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "WorkCenter", JOStepPlanning.WorkCenter);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Overhead", JOStepPlanning.Overhead);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "IsInhouse", JOStepPlanning.IsInhouse);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "InQty", JOStepPlanning.InQty);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "OutQty", JOStepPlanning.OutQty);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "AdjQty", JOStepPlanning.AdjQty);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Allowance", JOStepPlanning.Allowance);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Yield", JOStepPlanning.Yield);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "ActualLoss", JOStepPlanning.ActualLoss);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "ActualYield", JOStepPlanning.ActualYield);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Instruction", JOStepPlanning.Instruction);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "EstWorkOrderPrice", JOStepPlanning.EstWorkOrderPrice);
                if (Convert.ToString(JOStepPlanning.WorkOrderDate) != "1/1/0001 12:00:00 AM" && JOStepPlanning.WorkOrderDate != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "set", "WorkOrderDate", JOStepPlanning.WorkOrderDate);
                }
                DT1.Rows.Add("Production.JOStepPlanning", "set", "WorkOrderPrice", JOStepPlanning.WorkOrderPrice);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "WorkOrderQty", JOStepPlanning.WorkOrderQty);
                if (JOStepPlanning.DateCommitted != Convert.ToDateTime("1/1/0001 12:00:00 AM") && JOStepPlanning.DateCommitted != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "set", "DateCommitted", JOStepPlanning.DateCommitted);
                }
                DT1.Rows.Add("Production.JOStepPlanning", "set", "VAT", JOStepPlanning.VAT);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "VATRate", JOStepPlanning.VATRate);
                if (JOStepPlanning.TargetDateIn != Convert.ToDateTime("1/1/0001 12:00:00 AM") && JOStepPlanning.TargetDateIn != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "set", "TargetDateIn", JOStepPlanning.TargetDateIn);
                }
                if (JOStepPlanning.TargetDateOut != Convert.ToDateTime("1/1/0001 12:00:00 AM") && JOStepPlanning.TargetDateOut != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "set", "TargetDateOut", JOStepPlanning.TargetDateOut);
                }
                if (JOStepPlanning.ActualDateIn != Convert.ToDateTime("1/1/0001 12:00:00 AM") && JOStepPlanning.ActualDateIn != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "set", "ActualDateIn", JOStepPlanning.ActualDateIn);
                }
                if (JOStepPlanning.ActualDateOut != Convert.ToDateTime("1/1/0001 12:00:00 AM") && JOStepPlanning.ActualDateOut != null)
                {
                    DT1.Rows.Add("Production.JOStepPlanning", "set", "ActualDateOut", JOStepPlanning.ActualDateOut);
                }
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field1", JOStepPlanning.Field1);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field2", JOStepPlanning.Field2);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field3", JOStepPlanning.Field3);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field4", JOStepPlanning.Field4);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field5", JOStepPlanning.Field5);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field6", JOStepPlanning.Field6);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field7", JOStepPlanning.Field7);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field8", JOStepPlanning.Field8);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Field9", JOStepPlanning.Field9);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "OHRate", JOStepPlanning.OHRate);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "OHType", JOStepPlanning.OHType);
                DT1.Rows.Add("Production.JOStepPlanning", "set", "Version", "1");
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteJOStepPlanning(JOStepPlanning JOStepPlanning)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.JOStepPlanning", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOStepPlanning", "cond", "LineNumber", JOStepPlanning.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable JOProduct = Gears.RetriveData2("SELECT * FROM Production.JOProductOrder WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOBOM = Gears.RetriveData2("SELECT * FROM Production.JOBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSteps = Gears.RetriveData2("SELECT * FROM Production.JOStep WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOStepPlan = Gears.RetriveData2("SELECT * FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOClass = Gears.RetriveData2("SELECT * FROM Production.JOClassBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSize = Gears.RetriveData2("SELECT * FROM Production.JOSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOMaterial = Gears.RetriveData2("SELECT * FROM Production.JOMaterialMovement WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((JOProduct.Rows.Count < 1) && (JOBOM.Rows.Count < 1) && (JOSteps.Rows.Count < 1) && (JOStepPlan.Rows.Count < 1)
                    && (JOClass.Rows.Count < 1) && (JOSize.Rows.Count < 1) && (JOMaterial.Rows.Count < 1))
                {
                    DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        #endregion

        #region JOClassBreakdown
        public class JOClassBreakdown
        {
            public virtual JobOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }


            public DataTable getJOClassBreakdown(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.JOClassBreakdown WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddJOClassBreakdown(JOClassBreakdown JOClassBreakdown)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.JOClassBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "ClassCode", JOClassBreakdown.ClassCode);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Qty", JOClassBreakdown.Qty);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field1", JOClassBreakdown.Field1);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field2", JOClassBreakdown.Field2);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field3", JOClassBreakdown.Field3);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field4", JOClassBreakdown.Field4);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field5", JOClassBreakdown.Field5);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field6", JOClassBreakdown.Field6);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field7", JOClassBreakdown.Field7);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field8", JOClassBreakdown.Field8);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Field9", JOClassBreakdown.Field9);
                DT1.Rows.Add("Production.JOClassBreakdown", "0", "Version", "1");
                
                DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateJOClassBreakdown(JOClassBreakdown JOClassBreakdown)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.JOClassBreakdown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOClassBreakdown", "cond", "LineNumber", JOClassBreakdown.LineNumber);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "ClassCode", JOClassBreakdown.ClassCode);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Qty", JOClassBreakdown.Qty);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field1", JOClassBreakdown.Field1);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field2", JOClassBreakdown.Field2);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field3", JOClassBreakdown.Field3);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field4", JOClassBreakdown.Field4);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field5", JOClassBreakdown.Field5);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field6", JOClassBreakdown.Field6);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field7", JOClassBreakdown.Field7);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field8", JOClassBreakdown.Field8);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Field9", JOClassBreakdown.Field9);
                DT1.Rows.Add("Production.JOClassBreakdown", "set", "Version", "1");


                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteJOClassBreakdown(JOClassBreakdown JOClassBreakdown)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.JOClassBreakdown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.JOClassBreakdown", "cond", "LineNumber", JOClassBreakdown.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable JOProduct = Gears.RetriveData2("SELECT * FROM Production.JOProductOrder WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOBOM = Gears.RetriveData2("SELECT * FROM Production.JOBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSteps = Gears.RetriveData2("SELECT * FROM Production.JOStep WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOStepPlan = Gears.RetriveData2("SELECT * FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOClass = Gears.RetriveData2("SELECT * FROM Production.JOClassBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOSize = Gears.RetriveData2("SELECT * FROM Production.JOSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable JOMaterial = Gears.RetriveData2("SELECT * FROM Production.JOMaterialMovement WHERE DocNumber = '" + Docnum + "'", Conn);

                if ((JOProduct.Rows.Count < 1) && (JOBOM.Rows.Count < 1) && (JOSteps.Rows.Count < 1) && (JOStepPlan.Rows.Count < 1)
                    && (JOClass.Rows.Count < 1) && (JOSize.Rows.Count < 1) && (JOMaterial.Rows.Count < 1))
                {
                    DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        #endregion

        #region JOSizeBreakdown
        public class JOSizeBreakdown
        {
            public virtual JobOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string StockSize { get; set; }
            public virtual decimal SOQty { get; set; }
            public virtual decimal JOQty { get; set; }
            public virtual decimal INQty { get; set; }
            public virtual decimal FinalQty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }

            public DataTable getJOSizeBreakdown(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.JOSizeBreakdown WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddJOSizeBreakdown(JOSizeBreakdown JOSizeBreakdown)
            {
                //int linenum = 0;

                //DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.JOSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);

                //try
                //{
                //    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                //}
                //catch
                //{
                //    linenum = 1;
                //}
                //string strLine = linenum.ToString().PadLeft(5, '0');

                //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "DocNumber", Docnum);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "LineNumber", strLine);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "StockSize", JOSizeBreakdown.StockSize);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "SOQty", JOSizeBreakdown.SOQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "JOQty", JOSizeBreakdown.JOQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "INQty", JOSizeBreakdown.INQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "FinalQty", JOSizeBreakdown.FinalQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field1", JOSizeBreakdown.Field1);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field2", JOSizeBreakdown.Field2);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field3", JOSizeBreakdown.Field3);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field4", JOSizeBreakdown.Field4);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field5", JOSizeBreakdown.Field5);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field6", JOSizeBreakdown.Field6);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field7", JOSizeBreakdown.Field7);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field8", JOSizeBreakdown.Field8);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "0", "Field9", JOSizeBreakdown.Field9);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Version", "1");
                
                //DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "True");

                //Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2, Conn);

            }
            public void UpdateJOSizeBreakdown(JOSizeBreakdown JOSizeBreakdown)
            {
                //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                //DT1.Rows.Add("Production.JOSizeBreakdown", "cond", "DocNumber", Docnum);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "cond", "LineNumber", JOSizeBreakdown.LineNumber);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "StockSize", JOSizeBreakdown.StockSize);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "SOQty", JOSizeBreakdown.SOQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "JOQty", JOSizeBreakdown.JOQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "INQty", JOSizeBreakdown.INQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "FinalQty", JOSizeBreakdown.FinalQty);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field1", JOSizeBreakdown.Field1);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field2", JOSizeBreakdown.Field2);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field3", JOSizeBreakdown.Field3);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field4", JOSizeBreakdown.Field4);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field5", JOSizeBreakdown.Field5);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field6", JOSizeBreakdown.Field6);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field7", JOSizeBreakdown.Field7);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field8", JOSizeBreakdown.Field8);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Field9", JOSizeBreakdown.Field9);
                //DT1.Rows.Add("Production.JOSizeBreakdown", "set", "Version", "1");

                //Gears.UpdateData(DT1, Conn);
            }
            public void DeleteJOSizeBreakdown(JOSizeBreakdown JOSizeBreakdown)
            {
            //    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            //    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            //    DT1.Rows.Add("Production.JOSizeBreakdown", "cond", "DocNumber", Docnum);
            //    DT1.Rows.Add("Production.JOSizeBreakdown", "cond", "LineNumber", JOSizeBreakdown.LineNumber);
            //    Gears.DeleteData(DT1, Conn);

            //    DataTable JOProduct = Gears.RetriveData2("SELECT * FROM Production.JOProductOrder WHERE DocNumber = '" + Docnum + "'", Conn);
            //    DataTable JOBOM = Gears.RetriveData2("SELECT * FROM Production.JOBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);
            //    DataTable JOSteps = Gears.RetriveData2("SELECT * FROM Production.JOStep WHERE DocNumber = '" + Docnum + "'", Conn);
            //    DataTable JOStepPlan = Gears.RetriveData2("SELECT * FROM Production.JOStepPlanning WHERE DocNumber = '" + Docnum + "'", Conn);
            //    DataTable JOClass = Gears.RetriveData2("SELECT * FROM Production.JOClassBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
            //    DataTable JOSize = Gears.RetriveData2("SELECT * FROM Production.JOSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
            //    DataTable JOMaterial = Gears.RetriveData2("SELECT * FROM Production.JOMaterialMovement WHERE DocNumber = '" + Docnum + "'", Conn);

            //    if ((JOProduct.Rows.Count < 1) && (JOBOM.Rows.Count < 1) && (JOSteps.Rows.Count < 1) && (JOStepPlan.Rows.Count < 1)
            //        && (JOClass.Rows.Count < 1) && (JOSize.Rows.Count < 1) && (JOMaterial.Rows.Count < 1))
            //    {
            //        DT2.Rows.Add("Production.JobOrder", "cond", "DocNumber", Docnum);
            //        DT2.Rows.Add("Production.JobOrder", "set", "IsWithDetail", "False");
            //        Gears.UpdateData(DT2, Conn);
            //    }

            }
        }
        #endregion

        #region JOMaterialMovement
        public class JOMaterialMovement
        {
            public virtual JobOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal IssuedQty { get; set; }
            public virtual decimal ReturnQty { get; set; }
            public virtual decimal Consumption { get; set; }
            public virtual decimal AllowanceQty { get; set; }
            public virtual decimal INQty { get; set; }
            public virtual decimal Allocation { get; set; }
            public virtual decimal ReplacementQty { get; set; }
            public virtual decimal ChargeQty { get; set; }
            public virtual bool NoReturn { get; set; }
            public virtual string Remarks { get; set; }
            public virtual bool IsChargedAlready { get; set; }
            public DataTable getJOMaterialMovement(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.JOMaterialMovement WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        #endregion

        public void JODetails(string DocNumber, string Conn)
        {
            DataTable JODetails = new DataTable();
            JODetails = Gears.RetriveData2("EXEC sp_generate_JODetails '" + DocNumber + "'", Conn);
        }

        public void DeleteStepPlanning(string DocNumber, string Conn)
        {
            DataTable DeleteStepPlanning = new DataTable();
            DeleteStepPlanning = Gears.RetriveData2("DELETE FROM Production.JOStepPlanning WHERE Version = '1' AND DocNumber = '" + DocNumber + "'", Conn);
        }
        public void DeleteBOM(string DocNumber, string Conn)
        {
            DataTable DeleteBOM = new DataTable();
            DeleteBOM = Gears.RetriveData2("DELETE FROM Production.JOBillOfMaterial WHERE Version = '1' AND DocNumber = '" + DocNumber + "'", Conn);
        }

        public void ArrangeLine(string DocNumber, string Conn)
        {
            DataTable ArrangeLine = new DataTable();
            ArrangeLine = Gears.RetriveData2("UPDATE A SET LineNumber = Line FROM Production.JOBillOfMaterial A INNER JOIN "
            + " (SELECT DocNumber, LineNumber, StepCode, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StepCode ASC, Components ASC) AS VARCHAR(5)),5) AS Line FROM Production.JOBillOfMaterial "
            + " WHERE DocNumber = '" + DocNumber + "') B ON A.DocNumber = B.DocNumber AND A.LineNumber = B.LineNumber AND A.StepCode = B.StepCode AND A.DocNumber = '" + DocNumber + "' "
            + " UPDATE A SET LineNumber = Line FROM Production.JOStepPlanning A INNER JOIN "
            + " (SELECT DocNumber, LineNumber, Sequence, ISNULL(PreProd,0) PreProd, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY ISNULL(PreProd,0) DESC, Sequence ASC) AS VARCHAR(5)),5) AS Line FROM Production.JOStepPlanning "
            + " WHERE DocNumber = '" + DocNumber + "') B ON A.DocNumber = B.DocNumber AND A.LineNumber = B.LineNumber AND A.Sequence = B.Sequence AND ISNULL(A.PreProd,0) = ISNULL(B.PreProd,0) AND A.DocNumber = '" + DocNumber + "'", Conn);
        }
    }
}
