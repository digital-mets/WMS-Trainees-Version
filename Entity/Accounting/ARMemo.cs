using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ARMemo
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Type { get; set; }
        public virtual string Reference { get; set; }
        public virtual string RefInvoice { get; set; }
        public virtual string TaxCode { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual string Currency { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal GrossVatAmount { get; set; }
        public virtual decimal GrossNonVatAmount { get; set; }
        public virtual decimal VATAmount { get; set; }
        public virtual decimal TotalAmount { get; set; }        
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
        public virtual IList<ARMemoDetail> Detail { get; set; }


        public class ARMemoDetail
        {
            public virtual ARMemo Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string InvoiceNo { get; set; }
            public virtual string TransDoc { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual decimal Price { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual bool Vatable { get; set; }
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
            public virtual string RefTransType { get; set; }
            public virtual string RecordNo { get; set; }
            public virtual DateTime ExpDate { get; set; }
            public virtual DateTime MfgDate { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string LotNo { get; set; }


            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.ARMemoDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddARMemoDetail(ARMemoDetail ARMemoDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Accounting.ARMemoDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "InvoiceNo", ARMemoDetail.InvoiceNo);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "TransDoc", ARMemoDetail.TransDoc);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "ItemCode", ARMemoDetail.ItemCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "FullDesc", ARMemoDetail.FullDesc);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "ColorCode", ARMemoDetail.ColorCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "ClassCode", ARMemoDetail.ClassCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "SizeCode", ARMemoDetail.SizeCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Unit", ARMemoDetail.Unit);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Qty", ARMemoDetail.Qty);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Price", ARMemoDetail.Price);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "BulkQty", ARMemoDetail.BulkQty);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Vatable", ARMemoDetail.Vatable);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "StatusCode", ARMemoDetail.StatusCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "BaseQty", ARMemoDetail.BaseQty);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "BarcodeNo", ARMemoDetail.BarcodeNo);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "UnitFactor", ARMemoDetail.UnitFactor);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Version", "1");
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "RefTransType", ARMemoDetail.RefTransType);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "RecordNo", ARMemoDetail.RecordNo);

                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field1", ARMemoDetail.Field1);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field2", ARMemoDetail.Field2);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field3", ARMemoDetail.Field3);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field4", ARMemoDetail.Field4);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field5", ARMemoDetail.Field5);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field6", ARMemoDetail.Field6);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field7", ARMemoDetail.Field7);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field8", ARMemoDetail.Field8);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "Field9", ARMemoDetail.Field9);
                
                if (ARMemoDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "0", "ExpDate", ARMemoDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "0", "ExpDate", null);
                }
                if (ARMemoDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "0", "MfgDate", ARMemoDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "BatchNo", ARMemoDetail.BatchNo);
                DT1.Rows.Add("Accounting.ARMemoDetail", "0", "LotNo", ARMemoDetail.LotNo);

                DT2.Rows.Add("Accounting.ARMemo", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.ARMemo", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateARMemoDetail(ARMemoDetail ARMemoDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.ARMemoDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ARMemoDetail", "cond", "LineNumber", ARMemoDetail.LineNumber);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "InvoiceNo", ARMemoDetail.InvoiceNo);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "TransDoc", ARMemoDetail.TransDoc);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "ItemCode", ARMemoDetail.ItemCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "FullDesc", ARMemoDetail.FullDesc);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "ColorCode", ARMemoDetail.ColorCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "ClassCode", ARMemoDetail.ClassCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "SizeCode", ARMemoDetail.SizeCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Unit", ARMemoDetail.Unit);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Qty", ARMemoDetail.Qty);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Price", ARMemoDetail.Price);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "BulkQty", ARMemoDetail.BulkQty);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Vatable", ARMemoDetail.Vatable);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "StatusCode", ARMemoDetail.StatusCode);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "BaseQty", ARMemoDetail.BaseQty);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "BarcodeNo", ARMemoDetail.BarcodeNo);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "UnitFactor", ARMemoDetail.UnitFactor);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Version", "1");
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "RefTransType", ARMemoDetail.RefTransType);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "RecordNo", ARMemoDetail.RecordNo);

                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field1", ARMemoDetail.Field1);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field2", ARMemoDetail.Field2);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field3", ARMemoDetail.Field3);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field4", ARMemoDetail.Field4);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field5", ARMemoDetail.Field5);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field6", ARMemoDetail.Field6);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field7", ARMemoDetail.Field7);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field8", ARMemoDetail.Field8);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "Field9", ARMemoDetail.Field9);
                
                if (ARMemoDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "set", "ExpDate", ARMemoDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "set", "ExpDate", null);
                }
                if (ARMemoDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "set", "MfgDate", ARMemoDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Accounting.ARMemoDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "BatchNo", ARMemoDetail.BatchNo);
                DT1.Rows.Add("Accounting.ARMemoDetail", "set", "LotNo", ARMemoDetail.LotNo);

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteARMemoDetail(ARMemoDetail ARMemoDetail)
            { 
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                 
                DT1.Rows.Add("Accounting.ARMemoDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ARMemoDetail", "cond", "LineNumber", ARMemoDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);
                 
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ARMemoDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "ACTARM");
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.ARMemoDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.ARMemo", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.ARMemo", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public class JournalEntry
        {
            public virtual ARMemo Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTARM' ", Conn);

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
            public virtual ARMemo Parent { get; set; }
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
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='ACTARM' OR  A.TransType='ACTARM') ", Conn);
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
            a = Gears.RetriveData2("SELECT * FROM Accounting.ARMemo WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                Name = dtRow["Name"].ToString();
                Address = dtRow["Address"].ToString();
                Type = dtRow["Type"].ToString();
                Reference = dtRow["Reference"].ToString();
                RefInvoice = dtRow["RefInvoice"].ToString();
                TaxCode = dtRow["TaxCode"].ToString();
                TaxRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["TaxRate"]) ? 0 : dtRow["TaxRate"]);
                Remarks = dtRow["Remarks"].ToString();
                Currency = Convert.IsDBNull(dtRow["Currency"]) || String.IsNullOrEmpty(dtRow["Currency"].ToString()) ? "PHP" : dtRow["Currency"].ToString();
                GrossVatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatAmount"]) ? 0 : dtRow["GrossVatAmount"]);
                GrossNonVatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossNonVatAmount"]) ? 0 : dtRow["GrossNonVatAmount"]);
                VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                
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
        public void InsertData(ARMemo _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.ARMemo", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.ARMemo", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.ARMemo", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.ARMemo", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Address", _ent.Address);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Type", _ent.Type);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Reference", _ent.Reference);
            DT1.Rows.Add("Accounting.ARMemo", "0", "RefInvoice", _ent.RefInvoice);
            DT1.Rows.Add("Accounting.ARMemo", "0", "TaxCode", _ent.TaxCode);
            DT1.Rows.Add("Accounting.ARMemo", "0", "TaxRate", _ent.TaxRate);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.ARMemo", "0", "GrossVatAmount", _ent.GrossVatAmount);
            DT1.Rows.Add("Accounting.ARMemo", "0", "GrossNonVatAmount", _ent.GrossNonVatAmount);
            DT1.Rows.Add("Accounting.ARMemo", "0", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Accounting.ARMemo", "0", "TotalAmount", _ent.TotalAmount);

            DT1.Rows.Add("Accounting.ARMemo", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ARMemo", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.ARMemo", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.ARMemo", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ARMemo _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.ARMemo", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Accounting.ARMemo", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.ARMemo", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.ARMemo", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Address", _ent.Address);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Type", _ent.Type);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Reference", _ent.Reference);
            DT1.Rows.Add("Accounting.ARMemo", "set", "RefInvoice", _ent.RefInvoice);
            DT1.Rows.Add("Accounting.ARMemo", "set", "TaxCode", _ent.TaxCode);
            DT1.Rows.Add("Accounting.ARMemo", "set", "TaxRate", _ent.TaxRate);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.ARMemo", "set", "GrossVatAmount", _ent.GrossVatAmount);
            DT1.Rows.Add("Accounting.ARMemo", "set", "GrossNonVatAmount", _ent.GrossNonVatAmount);
            DT1.Rows.Add("Accounting.ARMemo", "set", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Accounting.ARMemo", "set", "TotalAmount", _ent.TotalAmount);

            DT1.Rows.Add("Accounting.ARMemo", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ARMemo", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.ARMemo", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.ARMemo", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTARM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ARMemo _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.ARMemo", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.ARMemoDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("ACTARM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.ARMemoDetail WHERE DocNumber = '" + DocNumber + "' AND Version = '1'", Conn);
        }
    }
}
