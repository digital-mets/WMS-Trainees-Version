using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CashPaymentVoucher
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string ReferenceChecks { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string SupplierName { get; set; }
        public virtual decimal CashAmount { get; set; }
        public virtual decimal TotalAppliedAmount { get; set; }
        public virtual decimal TotalAdjustment { get; set; }
        public virtual decimal Variance { get; set; }
        public virtual string Remarks { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual IList<CashPaymentVoucherApplication> Application { get; set; }
        public virtual IList<CashPaymentVoucherAdjustment> Adjustment { get; set; }

        #region Header
        public class RefTransaction
        {
            public virtual CashPaymentVoucher Parent { get; set; }
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
                                            + "  WHERE (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTCPV' OR  A.TransType='ACTCPV') ", Conn);
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

            a = Gears.RetriveData2("SELECT * FROM Accounting.CashPaymentVoucher WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ReferenceChecks = dtRow["ReferenceChecks"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                SupplierName = dtRow["SupplierName"].ToString();
                CashAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CashAmount"]) ? 0 : dtRow["CashAmount"]);
                TotalAppliedAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAppliedAmount"]) ? 0 : dtRow["TotalAppliedAmount"]);
                TotalAdjustment = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAdjustment"]) ? 0 : dtRow["TotalAdjustment"]);
                Variance = Convert.ToDecimal(Convert.IsDBNull(dtRow["Variance"]) ? 0 : dtRow["Variance"]);
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
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString(); 
            }
            return a;
        }
        public void InsertData(CashPaymentVoucher _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "DocNumber", Docnum);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "ReferenceChecks", _ent.ReferenceChecks);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "CashAmount", _ent.CashAmount);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "TotalAppliedAmount", _ent.TotalAppliedAmount);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "TotalAdjustment", _ent.TotalAdjustment);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Variance", _ent.Variance);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "0", "IsWithDetail", "False");
            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(CashPaymentVoucher _ent)
        {
            Conn = _ent.Connection;
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CashPaymentVoucher", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "ReferenceChecks", _ent.ReferenceChecks);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "CashAmount", _ent.CashAmount);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "TotalAppliedAmount", _ent.TotalAppliedAmount);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "TotalAdjustment", _ent.TotalAdjustment);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Variance", _ent.Variance);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCPV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CashPaymentVoucher _ent)
        {
            Conn = _ent.Connection;
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CashPaymentVoucher", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.CashPaymentVoucherApplication", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DT3.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT3, _ent.Connection);

            Functions.AuditTrail("ACTCPV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        #endregion

        public class JournalEntry
        {
            public virtual CashPaymentVoucher Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCPV' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        #region Details
        public class CashPaymentVoucherApplication
        {
            public virtual CashPaymentVoucher Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string TransType { get; set; }
            public virtual string TransDocNumber { get; set; }
            public virtual DateTime TransDate { get; set; }
            public virtual DateTime DueDate { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual decimal TransAmount { get; set; }
            public virtual decimal TransAppliedAmount { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string RecordID { get; set; }

            public DataTable getApplicationDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CashPaymentVoucherApplication WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCashPaymentVoucherApplication(CashPaymentVoucherApplication CashPaymentVoucherApplication)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CashPaymentVoucherApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }
                
                string strLine = linenum.ToString().PadLeft(5, '0');
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "TransType", CashPaymentVoucherApplication.TransType);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "TransDocNumber", CashPaymentVoucherApplication.TransDocNumber);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "TransDate", CashPaymentVoucherApplication.TransDate);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "DueDate", CashPaymentVoucherApplication.DueDate);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "AccountCode", CashPaymentVoucherApplication.AccountCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "SubsidiaryCode", CashPaymentVoucherApplication.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "ProfitCenterCode", CashPaymentVoucherApplication.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "CostCenterCode", CashPaymentVoucherApplication.CostCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "TransAmount", CashPaymentVoucherApplication.TransAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "TransAppliedAmount", CashPaymentVoucherApplication.TransAppliedAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field1", CashPaymentVoucherApplication.Field1);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field2", CashPaymentVoucherApplication.Field2);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field3", CashPaymentVoucherApplication.Field3);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field4", CashPaymentVoucherApplication.Field4);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field5", CashPaymentVoucherApplication.Field5);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field6", CashPaymentVoucherApplication.Field6);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field7", CashPaymentVoucherApplication.Field7);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field8", CashPaymentVoucherApplication.Field8);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "Field9", CashPaymentVoucherApplication.Field9);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "0", "RecordID", CashPaymentVoucherApplication.RecordID);
                DT2.Rows.Add("Accounting.CashPaymentVoucher", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CashPaymentVoucher", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateCashPaymentVoucherApplication(CashPaymentVoucherApplication CashPaymentVoucherApplication)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "cond", "LineNumber", CashPaymentVoucherApplication.LineNumber);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "TransType", CashPaymentVoucherApplication.TransType);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "TransDocNumber", CashPaymentVoucherApplication.TransDocNumber);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "TransDate", CashPaymentVoucherApplication.TransDate);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "DueDate", CashPaymentVoucherApplication.DueDate);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "AccountCode", CashPaymentVoucherApplication.AccountCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "SubsidiaryCode", CashPaymentVoucherApplication.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "ProfitCenterCode", CashPaymentVoucherApplication.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "CostCenterCode", CashPaymentVoucherApplication.CostCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "TransAmount", CashPaymentVoucherApplication.TransAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "TransAppliedAmount", CashPaymentVoucherApplication.TransAppliedAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field1", CashPaymentVoucherApplication.Field1);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field2", CashPaymentVoucherApplication.Field2);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field3", CashPaymentVoucherApplication.Field3);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field4", CashPaymentVoucherApplication.Field4);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field5", CashPaymentVoucherApplication.Field5);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field6", CashPaymentVoucherApplication.Field6);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field7", CashPaymentVoucherApplication.Field7);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field8", CashPaymentVoucherApplication.Field8);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "Field9", CashPaymentVoucherApplication.Field9);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "set", "RecordID", CashPaymentVoucherApplication.RecordID);
                Gears.UpdateData(DT1, Conn);  
            }
            public void DeleteCashPaymentVoucherApplication(CashPaymentVoucherApplication CashPaymentVoucherApplication)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CashPaymentVoucherApplication", "cond", "LineNumber", CashPaymentVoucherApplication.LineNumber);
                Gears.DeleteData(DT1, Conn);
                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.CashPaymentVoucherAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DataTable count2 = Gears.RetriveData2("SELECT * FROM Accounting.CashPaymentVoucherApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                    if (count2.Rows.Count < 1)
                    {
                        DT2.Rows.Add("Accounting.CashPaymentVoucher", "cond", "DocNumber", Docnum);
                        DT2.Rows.Add("Accounting.CashPaymentVoucher", "set", "IsWithDetail", "False");
                        Gears.UpdateData(DT2, Conn);
                    }
                }
            }
        }
        public class CashPaymentVoucherAdjustment
        {
            public virtual CashPaymentVoucher Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual decimal DebitAmount { get; set; }
            public virtual decimal CreditAmount { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getAdjustmentDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CashPaymentVoucherAdjustment WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCashPaymentVoucherAdjustment(CashPaymentVoucherAdjustment CashPaymentVoucherAdjustment)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CashPaymentVoucherAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);

                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }

                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "DocNumber ", Docnum);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "AccountCode", CashPaymentVoucherAdjustment.AccountCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "AccountDescription", CashPaymentVoucherAdjustment.AccountDescription);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "SubsidiaryCode", CashPaymentVoucherAdjustment.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "SubsidiaryDescription", CashPaymentVoucherAdjustment.SubsidiaryDescription);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "ProfitCenterCode", CashPaymentVoucherAdjustment.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "CostCenterCode", CashPaymentVoucherAdjustment.CostCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "DebitAmount", CashPaymentVoucherAdjustment.DebitAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "CreditAmount", CashPaymentVoucherAdjustment.CreditAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field1", CashPaymentVoucherAdjustment.Field1);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field2", CashPaymentVoucherAdjustment.Field2);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field3", CashPaymentVoucherAdjustment.Field3);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field4", CashPaymentVoucherAdjustment.Field4);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field5", CashPaymentVoucherAdjustment.Field5);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field6", CashPaymentVoucherAdjustment.Field6);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field7", CashPaymentVoucherAdjustment.Field7);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field8", CashPaymentVoucherAdjustment.Field8);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "0", "Field9", CashPaymentVoucherAdjustment.Field9);
                DT2.Rows.Add("Accounting.CashPaymentVoucher", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CashPaymentVoucher", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateCashPaymentVoucherAdjustment(CashPaymentVoucherAdjustment CashPaymentVoucherAdjustment)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "cond", "DocNumber", CashPaymentVoucherAdjustment.DocNumber);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "cond", "LineNumber", CashPaymentVoucherAdjustment.LineNumber);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "AccountCode", CashPaymentVoucherAdjustment.AccountCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "AccountDescription", CashPaymentVoucherAdjustment.AccountDescription);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "SubsidiaryCode", CashPaymentVoucherAdjustment.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "SubsidiaryDescription", CashPaymentVoucherAdjustment.SubsidiaryDescription);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "ProfitCenterCode", CashPaymentVoucherAdjustment.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "CostCenterCode", CashPaymentVoucherAdjustment.CostCenterCode);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "DebitAmount", CashPaymentVoucherAdjustment.DebitAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "CreditAmount", CashPaymentVoucherAdjustment.CreditAmount);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field1", CashPaymentVoucherAdjustment.Field1);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field2", CashPaymentVoucherAdjustment.Field2);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field3", CashPaymentVoucherAdjustment.Field3);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field4", CashPaymentVoucherAdjustment.Field4);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field5", CashPaymentVoucherAdjustment.Field5);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field6", CashPaymentVoucherAdjustment.Field6);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field7", CashPaymentVoucherAdjustment.Field7);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field8", CashPaymentVoucherAdjustment.Field8);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "set", "Field9", CashPaymentVoucherAdjustment.Field9);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteCashPaymentVoucherAdjustment(CashPaymentVoucherAdjustment CashPaymentVoucherAdjustment)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "cond", "DocNumber", CashPaymentVoucherAdjustment.DocNumber);
                DT1.Rows.Add("Accounting.CashPaymentVoucherAdjustment", "cond", "LineNumber", CashPaymentVoucherAdjustment.LineNumber);
                Gears.DeleteData(DT1, Conn);
                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.CashPaymentVoucherAdjustment WHERE DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DataTable count2 = Gears.RetriveData2("SELECT * FROM Accounting.CashPaymentVoucherApplication WHERE DocNumber = '" + Docnum + "'", Conn);
                    if (count2.Rows.Count < 1)
                    {
                        DT2.Rows.Add("Accounting.CashPaymentVoucher", "cond", "DocNumber", Docnum);
                        DT2.Rows.Add("Accounting.CashPaymentVoucher", "set", "IsWithDetail", "False");
                        Gears.UpdateData(DT2, Conn);
                    }
                }
            }
        }
        #endregion
    }
}
