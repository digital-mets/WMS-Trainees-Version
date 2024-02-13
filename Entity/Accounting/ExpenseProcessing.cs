using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ExpenseProcessing
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string PayableTo { get; set; }
        public virtual string PayableName { get; set; }
        public virtual decimal TotalGrossVatable { get; set; }
        public virtual decimal TotalGrossNonVatable { get; set; }
        public virtual decimal TotalVatAmount { get; set; }
        public virtual decimal TotalWitholdingTax { get; set; }
        public virtual decimal TotalAmountDue { get; set; }
        public virtual decimal Terms { get; set; }
        public virtual string DueDate { get; set; }
        public virtual string APVNumber { get; set; }
        public virtual bool IsWithPO { get; set; }
        public virtual string RefTrans { get; set; }
        public virtual string HRemarks { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string IsWithDetail { get; set; }
        public virtual string IsValidated { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual IList<ExpenseProcessingDetail> Detail { get; set; }
        public class ExpenseProcessingDetail
        {
            public virtual ExpenseProcessing Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ExpenseCode { get; set; }
            public virtual string ExpenseDescription { get; set; }
            public virtual string GLAccountCode { get; set; }
            public virtual string SubsiCode { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual string SupplierCode { get; set; }
            public virtual string SupplierName { get; set; }
            public virtual bool IsVatable { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal VATRate { get; set; }
            public virtual decimal VATAmount { get; set; }
            public virtual bool IsEWT { get; set; }
            public virtual string ATCCode { get; set; }
            public virtual decimal ATCRate { get; set; }
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
            public virtual string Version { get; set; }
            public virtual string RecordID { get; set; }
            public virtual string PONumber { get; set; }

            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.ExpenseProcessingDetail WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddExpenseProcessingDetail(ExpenseProcessingDetail ExpenseProcessingDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Accounting.ExpenseProcessingDetail WHERE docnumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "ExpenseCode", ExpenseProcessingDetail.ExpenseCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "ExpenseDescription", ExpenseProcessingDetail.ExpenseDescription);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "GLAccountCode", ExpenseProcessingDetail.GLAccountCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "SubsiCode", ExpenseProcessingDetail.SubsiCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "ProfitCenterCode", ExpenseProcessingDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "CostCenterCode", ExpenseProcessingDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "SupplierCode", ExpenseProcessingDetail.SupplierCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "SupplierName", ExpenseProcessingDetail.SupplierName);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "IsVatable", ExpenseProcessingDetail.IsVatable);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "VATCode", ExpenseProcessingDetail.VATCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "VATRate", ExpenseProcessingDetail.VATRate);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "IsEWT", ExpenseProcessingDetail.IsEWT);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "ATCCode", ExpenseProcessingDetail.ATCCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "ATCRate", ExpenseProcessingDetail.ATCRate);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Amount", ExpenseProcessingDetail.Amount);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "VATAmount", ExpenseProcessingDetail.VATAmount);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Remarks", ExpenseProcessingDetail.Remarks);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field1", ExpenseProcessingDetail.Field1);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field2", ExpenseProcessingDetail.Field2);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field3", ExpenseProcessingDetail.Field3);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field4", ExpenseProcessingDetail.Field4);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field5", ExpenseProcessingDetail.Field5);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field6", ExpenseProcessingDetail.Field6);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field7", ExpenseProcessingDetail.Field7);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field8", ExpenseProcessingDetail.Field8);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Field9", ExpenseProcessingDetail.Field9);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "RecordID", ExpenseProcessingDetail.RecordID);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "PONumber", ExpenseProcessingDetail.PONumber);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "0", "Version", "1");

                DT2.Rows.Add("Accounting.ExpenseProcessing", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.ExpenseProcessing", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);
            }
            public void UpdateExpenseProcessingDetail(ExpenseProcessingDetail ExpenseProcessingDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "LineNumber", ExpenseProcessingDetail.LineNumber);
                //DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "RecordID", ExpenseProcessingDetail.RecordID);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "ExpenseCode", ExpenseProcessingDetail.ExpenseCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "ExpenseDescription", ExpenseProcessingDetail.ExpenseDescription);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "GLAccountCode", ExpenseProcessingDetail.GLAccountCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "SubsiCode", ExpenseProcessingDetail.SubsiCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "ProfitCenterCode", ExpenseProcessingDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "CostCenterCode", ExpenseProcessingDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "SupplierCode", ExpenseProcessingDetail.SupplierCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "SupplierName", ExpenseProcessingDetail.SupplierName);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "IsVatable", ExpenseProcessingDetail.IsVatable);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "VATCode", ExpenseProcessingDetail.VATCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "VATRate", ExpenseProcessingDetail.VATRate);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "IsEWT", ExpenseProcessingDetail.IsEWT);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "ATCCode", ExpenseProcessingDetail.ATCCode);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "ATCRate", ExpenseProcessingDetail.ATCRate);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Amount", ExpenseProcessingDetail.Amount);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "VATAmount", ExpenseProcessingDetail.VATAmount);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Remarks", ExpenseProcessingDetail.Remarks);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field1", ExpenseProcessingDetail.Field1);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field2", ExpenseProcessingDetail.Field2);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field3", ExpenseProcessingDetail.Field3);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field4", ExpenseProcessingDetail.Field4);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field5", ExpenseProcessingDetail.Field5);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field6", ExpenseProcessingDetail.Field6);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field7", ExpenseProcessingDetail.Field7);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field8", ExpenseProcessingDetail.Field8);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Field9", ExpenseProcessingDetail.Field9);
                //DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "RecordID", ExpenseProcessingDetail.RecordID);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "PONumber", ExpenseProcessingDetail.PONumber);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "Version", "1");

                Gears.UpdateData(DT1,Conn);
            }
            public void DeleteExpenseProcessingDetail(ExpenseProcessingDetail ExpenseProcessingDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "LineNumber", ExpenseProcessingDetail.LineNumber);
                //DT1.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "RecordID", ExpenseProcessingDetail.RecordID);


                Gears.DeleteData(DT1,Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.ExpenseProcessingDetail WHERE docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.ExpenseProcessingDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }
            }
        }

        #region Journal Entry
        public class JournalEntry
        {
            public virtual ExpenseProcessing Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode,(case when isnull(B.Description,'') = '' then A.Description else B.Description end) AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " LEFT JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTEXP' ",Conn);

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
            public virtual ExpenseProcessing Parent { get; set; }
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
                    a = Gears.RetriveData2("select DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B on A.RMenuID = B.ModuleID inner join IT.MainMenu C on A.MenuID = C.ModuleID "
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='ACTEXP' OR  A.TransType='ACTEXP') ", Conn);
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
        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Accounting.ExpenseProcessing WHERE DocNumber = '" + DocNumber + "'",Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                DueDate = dtRow["DueDate"].ToString();
                APVNumber = dtRow["APVNumber"].ToString();
                PayableTo = string.IsNullOrEmpty(dtRow["PayableTo"].ToString()) ? "" : dtRow["PayableTo"].ToString();
                PayableName = dtRow["PayableName"].ToString();
                TotalGrossVatable = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalGrossVatable"].ToString()) ? "0" : dtRow["TotalGrossVatable"].ToString());
                TotalGrossNonVatable = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalGrossNonVatable"].ToString()) ? "0" : dtRow["TotalGrossNonVatable"].ToString());
                TotalVatAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalVatAmount"].ToString()) ? "0" : dtRow["TotalVatAmount"].ToString());
                TotalWitholdingTax = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalWitholdingTax"].ToString()) ? "0" : dtRow["TotalWitholdingTax"].ToString());
                TotalAmountDue = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalAmountDue"].ToString()) ? "0" : dtRow["TotalAmountDue"].ToString());
                Terms = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["Terms"].ToString()) ? "0" : dtRow["Terms"].ToString());
                RefTrans = dtRow["RefTrans"].ToString();
                HRemarks = dtRow["HRemarks"].ToString(); 
                IsWithPO = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithPO"]) ? false : dtRow["IsWithPO"]);
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
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
            }

            return a;
        }
        public void InsertData(ExpenseProcessing _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "PayableTo", _ent.PayableTo);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "PayableName", _ent.PayableName);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "TotalGrossVatable", _ent.TotalGrossVatable);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "TotalGrossNonVatable", _ent.TotalGrossNonVatable);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "TotalVatAmount", _ent.TotalVatAmount);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "TotalWitholdingTax", _ent.TotalWitholdingTax);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Terms", _ent.Terms);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "APVNumber", _ent.APVNumber);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "HRemarks", _ent.HRemarks);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "IsWithPO", _ent.IsWithPO);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.ExpenseProcessing", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1,_ent.Connection);
        }
        public void UpdateData(ExpenseProcessing _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.ExpenseProcessing", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "PayableTo", _ent.PayableTo);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "PayableName", _ent.PayableName);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "TotalGrossVatable", _ent.TotalGrossVatable);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "TotalGrossNonVatable", _ent.TotalGrossNonVatable);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "TotalVatAmount", _ent.TotalVatAmount);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "TotalWitholdingTax", _ent.TotalWitholdingTax);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "APVNumber", _ent.APVNumber);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "HRemarks", _ent.HRemarks);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "IsWithPO", _ent.IsWithPO);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.ExpenseProcessing", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1,_ent.Connection);

            Functions.AuditTrail("ACTEXP", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }
        public void DeleteData(ExpenseProcessing _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.ExpenseProcessing", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.ExpenseProcessingDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("ACTEXP", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.ExpenseProcessingDetail WHERE DocNumber = '" + DocNumber + "' AND Version = '1'", Conn);
        }
    }
}

