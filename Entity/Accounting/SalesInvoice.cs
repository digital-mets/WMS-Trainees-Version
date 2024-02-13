using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class SalesInvoice
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string TIN { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Currency { get; set; }
        public virtual string RefSO { get; set; }
        public virtual string RefTrans { get; set; }
        public virtual bool Vatable { get; set; }
        public virtual bool IsWithSO { get; set; }
        public virtual decimal VatBeforeDisc { get; set; }
        public virtual decimal NonVatBeforeDisc { get; set; }
        public virtual decimal TotalDiscount { get; set; }
        public virtual decimal SCDiscount { get; set; }
        public virtual decimal PWDDiscount { get; set; }
        public virtual decimal DiplomatDiscount { get; set; }
        public virtual decimal VatAfterDisc { get; set; }
        public virtual decimal NonVatAfterDisc { get; set; }
        public virtual decimal VATAmount { get; set; }
        public virtual decimal TotalAmountDue { get; set; }
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
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }

        public virtual IList<SalesInvoiceDetail> Detail { get; set; }


        public class SalesInvoiceDetail
        {
            public virtual SalesInvoice Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string SONumber { get; set; }
            public virtual string TransType { get; set; }
            public virtual string TransDoc { get; set; }
            public virtual DateTime TransDate { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal Price { get; set; }
            public virtual decimal AmountBeforeDisc { get; set; }
            public virtual decimal DiscountAmount { get; set; }
            public virtual string VTaxCode { get; set; }
            public virtual string DiscountType { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual decimal UnitFactor { get; set; }
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
            public virtual decimal Rate { get; set; }
            public virtual decimal SDRate { get; set; }
            public virtual decimal SDComputedAmt { get; set; }
            public virtual decimal AverageCost { get; set; }
            public virtual decimal UnitCost { get; set; }

            
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.SalesInvoiceDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddSalesInvoiceDetail(SalesInvoiceDetail SalesInvoiceDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Accounting.SalesInvoiceDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "SONumber", SalesInvoiceDetail.SONumber);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "TransType", SalesInvoiceDetail.TransType);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "TransDoc", SalesInvoiceDetail.TransDoc);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "TransDate", SalesInvoiceDetail.TransDate);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "ItemCode", SalesInvoiceDetail.ItemCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "ColorCode", SalesInvoiceDetail.ColorCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "ClassCode", SalesInvoiceDetail.ClassCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "SizeCode", SalesInvoiceDetail.SizeCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Qty", SalesInvoiceDetail.Qty);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "BulkQty", SalesInvoiceDetail.BulkQty);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Unit", SalesInvoiceDetail.Unit);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Price", SalesInvoiceDetail.Price);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "AmountBeforeDisc", SalesInvoiceDetail.AmountBeforeDisc);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "DiscountAmount", SalesInvoiceDetail.DiscountAmount);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "VTaxCode", SalesInvoiceDetail.VTaxCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "DiscountType", SalesInvoiceDetail.DiscountType);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "StatusCode", SalesInvoiceDetail.StatusCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "BaseQty", SalesInvoiceDetail.BaseQty);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "BarcodeNo", SalesInvoiceDetail.BarcodeNo);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "UnitFactor", SalesInvoiceDetail.UnitFactor);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Version", "1");
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Rate", SalesInvoiceDetail.Rate);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "SDRate", SalesInvoiceDetail.SDRate);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "SDComputedAmt", SalesInvoiceDetail.SDComputedAmt);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "AverageCost", SalesInvoiceDetail.AverageCost);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "UnitCost", SalesInvoiceDetail.UnitCost);

                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field1", SalesInvoiceDetail.Field1);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field2", SalesInvoiceDetail.Field2);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field3", SalesInvoiceDetail.Field3);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field4", SalesInvoiceDetail.Field4);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field5", SalesInvoiceDetail.Field5);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field6", SalesInvoiceDetail.Field6);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field7", SalesInvoiceDetail.Field7);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field8", SalesInvoiceDetail.Field8);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "0", "Field9", SalesInvoiceDetail.Field9);

                DT2.Rows.Add("Accounting.SalesInvoice", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.SalesInvoice", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateSalesInvoiceDetail(SalesInvoiceDetail SalesInvoiceDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "cond", "LineNumber", SalesInvoiceDetail.LineNumber);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "SONumber", SalesInvoiceDetail.SONumber);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "TransType", SalesInvoiceDetail.TransType);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "TransDoc", SalesInvoiceDetail.TransDoc);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "TransDate", SalesInvoiceDetail.TransDate);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "ItemCode", SalesInvoiceDetail.ItemCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "ColorCode", SalesInvoiceDetail.ColorCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "ClassCode", SalesInvoiceDetail.ClassCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "SizeCode", SalesInvoiceDetail.SizeCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Qty", SalesInvoiceDetail.Qty);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "BulkQty", SalesInvoiceDetail.BulkQty);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Unit", SalesInvoiceDetail.Unit);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Price", SalesInvoiceDetail.Price);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "AmountBeforeDisc", SalesInvoiceDetail.AmountBeforeDisc);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "DiscountAmount", SalesInvoiceDetail.DiscountAmount);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "VTaxCode", SalesInvoiceDetail.VTaxCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "DiscountType", SalesInvoiceDetail.DiscountType);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "StatusCode", SalesInvoiceDetail.StatusCode);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "BaseQty", SalesInvoiceDetail.BaseQty);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "BarcodeNo", SalesInvoiceDetail.BarcodeNo);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "UnitFactor", SalesInvoiceDetail.UnitFactor);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Version", "1");
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Rate", SalesInvoiceDetail.Rate);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "SDRate", SalesInvoiceDetail.SDRate);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "SDComputedAmt", SalesInvoiceDetail.SDComputedAmt);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "AverageCost", SalesInvoiceDetail.AverageCost);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "UnitCost", SalesInvoiceDetail.UnitCost);

                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field1", SalesInvoiceDetail.Field1);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field2", SalesInvoiceDetail.Field2);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field3", SalesInvoiceDetail.Field3);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field4", SalesInvoiceDetail.Field4);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field5", SalesInvoiceDetail.Field5);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field6", SalesInvoiceDetail.Field6);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field7", SalesInvoiceDetail.Field7);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field8", SalesInvoiceDetail.Field8);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "set", "Field9", SalesInvoiceDetail.Field9);

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteSalesInvoiceDetail(SalesInvoiceDetail SalesInvoiceDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "cond", "DocNumber", SalesInvoiceDetail.DocNumber);
                DT1.Rows.Add("Accounting.SalesInvoiceDetail", "cond", "LineNumber", SalesInvoiceDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.SalesInvoiceDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.SalesInvoice", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.SalesInvoice", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        public class JournalEntry
        {
            public virtual SalesInvoice Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTSIN' ", Conn);

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
            public virtual SalesInvoice Parent { get; set; }
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
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='ACTSIN' OR  A.TransType='ACTSIN') ", Conn);
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

            a = Gears.RetriveData2("SELECT * FROM Accounting.SalesInvoice WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Name = dtRow["Name"].ToString();
                Address = dtRow["Address"].ToString();
                TIN = dtRow["TIN"].ToString();
                Reference = dtRow["Reference"].ToString();
                Currency = Convert.IsDBNull(dtRow["Currency"]) || String.IsNullOrEmpty(dtRow["Currency"].ToString()) ? "PHP" : dtRow["Currency"].ToString();
                RefSO = dtRow["RefSO"].ToString();
                RefTrans = dtRow["RefTrans"].ToString();
                Vatable = Convert.ToBoolean(Convert.IsDBNull(dtRow["Vatable"]) ? false : dtRow["Vatable"]);
                IsWithSO = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithSO"]) ? false : dtRow["IsWithSO"]);
                VatBeforeDisc = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatBeforeDisc"]) ? 0 : dtRow["VatBeforeDisc"]);
                NonVatBeforeDisc = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatBeforeDisc"]) ? 0 : dtRow["NonVatBeforeDisc"]);
                TotalDiscount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalDiscount"]) ? 0 : dtRow["TotalDiscount"]);
                SCDiscount = Convert.ToDecimal(Convert.IsDBNull(dtRow["SCDiscount"]) ? 0 : dtRow["SCDiscount"]);
                PWDDiscount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PWDDiscount"]) ? 0 : dtRow["PWDDiscount"]);
                DiplomatDiscount = Convert.ToDecimal(Convert.IsDBNull(dtRow["DiplomatDiscount"]) ? 0 : dtRow["DiplomatDiscount"]);
                VatAfterDisc = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatAfterDisc"]) ? 0 : dtRow["VatAfterDisc"]);
                NonVatAfterDisc = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatAfterDisc"]) ? 0 : dtRow["NonVatAfterDisc"]);
                VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);
                TotalAmountDue = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountDue"]) ? 0 : dtRow["TotalAmountDue"]);
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
        public void InsertData(SalesInvoice _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.SalesInvoice", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Address", _ent.Address);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "TIN", _ent.TIN);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Reference", _ent.Reference);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "RefSO", _ent.RefSO);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Vatable", _ent.Vatable);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "IsWithSO", _ent.IsWithSO);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "VatBeforeDisc", _ent.VatBeforeDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "NonVatBeforeDisc", _ent.NonVatBeforeDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "TotalDiscount", _ent.TotalDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "SCDiscount", _ent.SCDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "PWDDiscount", _ent.PWDDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "DiplomatDiscount", _ent.DiplomatDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "VatAfterDisc", _ent.VatAfterDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "NonVatAfterDisc", _ent.NonVatAfterDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.SalesInvoice", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.SalesInvoice", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(SalesInvoice _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.SalesInvoice", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Address", _ent.Address);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "TIN", _ent.TIN);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Reference", _ent.Reference);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "RefSO", _ent.RefSO);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Vatable", _ent.Vatable);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "IsWithSO", _ent.IsWithSO);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "VatBeforeDisc", _ent.VatBeforeDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "NonVatBeforeDisc", _ent.NonVatBeforeDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "TotalDiscount", _ent.TotalDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "SCDiscount", _ent.SCDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "PWDDiscount", _ent.PWDDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "DiplomatDiscount", _ent.DiplomatDiscount);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "VatAfterDisc", _ent.VatAfterDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "NonVatAfterDisc", _ent.NonVatAfterDisc);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.SalesInvoice", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.SalesInvoice", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTSIN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(SalesInvoice _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.SalesInvoice", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.SalesInvoiceDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("ACTSIN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.SalesInvoiceDetail WHERE DocNumber = '" + DocNumber + "' AND Version = '1'", Conn);
        }
    }
}
