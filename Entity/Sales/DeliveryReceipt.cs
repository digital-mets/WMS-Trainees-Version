using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DeliveryReceipt
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Type { get; set; }
        public virtual string RefSONum { get; set; }
        public virtual string PLDocNum { get; set; }
        public virtual string TIN { get; set; }
        public virtual int Terms { get; set; }
        public virtual string InvoiceDocNum { get; set; }
        public virtual string CounterDocNum { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string TotalQty { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual string Outlet { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string DeliveryAddress { get; set; }
        public virtual string CustomerPO { get; set; }
        public virtual string ForwardedDate { get; set; }
        public virtual string DispatchDate { get; set; }
        public virtual bool WithoutRef { get; set; }
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
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual bool IsWithDetail { get; set; }

        public virtual IList<DeliveryReceiptDetail> Detail { get; set; }
                
        public class DeliveryReceiptDetail
        {
            public virtual DeliveryReceipt Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string SONumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal DeliveredQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal ReturnedQty { get; set; }
            public virtual decimal ReturnedBulkQty { get; set; }
            public virtual string SubstituteItem { get; set; }
            public virtual string SubstituteColor { get; set; }
            public virtual string SubstituteClass { get; set; }
            public virtual string FullDesc { get; set; }
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
            public virtual bool IsByBulk { get; set; }
            public virtual string TransPONo { get; set; }
            public virtual DateTime ExpDate { get; set; }
            public virtual DateTime MfgDate { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string LotNo { get; set; }


            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Sales.DeliveryReceiptDetail WHERE DocNumber = '" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddDeliveryReceiptDetail(DeliveryReceiptDetail DeliveryReceiptDetail)
            {

                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Sales.DeliveryReceiptDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "SONumber", DeliveryReceiptDetail.SONumber);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "ItemCode", DeliveryReceiptDetail.ItemCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "ColorCode", DeliveryReceiptDetail.ColorCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "ClassCode", DeliveryReceiptDetail.ClassCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "SizeCode", DeliveryReceiptDetail.SizeCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "DeliveredQty", DeliveryReceiptDetail.DeliveredQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Unit", DeliveryReceiptDetail.Unit);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "BulkQty", DeliveryReceiptDetail.BulkQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "BulkUnit", DeliveryReceiptDetail.BulkUnit);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "StatusCode", DeliveryReceiptDetail.StatusCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "ReturnedQty", DeliveryReceiptDetail.ReturnedQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "ReturnedBulkQty", DeliveryReceiptDetail.ReturnedBulkQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "SubstituteItem", DeliveryReceiptDetail.SubstituteItem);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "SubstituteColor", DeliveryReceiptDetail.SubstituteColor);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "SubstituteClass", DeliveryReceiptDetail.SubstituteClass);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "FullDesc", DeliveryReceiptDetail.FullDesc);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "BaseQty", DeliveryReceiptDetail.BaseQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "BarcodeNo", DeliveryReceiptDetail.BarcodeNo);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "UnitFactor", DeliveryReceiptDetail.UnitFactor);

                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field1", DeliveryReceiptDetail.Field1);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field2", DeliveryReceiptDetail.Field2);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field3", DeliveryReceiptDetail.Field3);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field4", DeliveryReceiptDetail.Field4);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field5", DeliveryReceiptDetail.Field5);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field6", DeliveryReceiptDetail.Field6);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field7", DeliveryReceiptDetail.Field7);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field8", DeliveryReceiptDetail.Field8);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Field9", DeliveryReceiptDetail.Field9);

                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "Version", "1");
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "IsByBulk", DeliveryReceiptDetail.IsByBulk);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "TransPONo", DeliveryReceiptDetail.TransPONo);
                
                if (DeliveryReceiptDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "ExpDate", DeliveryReceiptDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "ExpDate", null);
                }
                if (DeliveryReceiptDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "MfgDate", DeliveryReceiptDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "BatchNo", DeliveryReceiptDetail.BatchNo);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "0", "LotNo", DeliveryReceiptDetail.LotNo);

                DT2.Rows.Add("Sales.DeliveryReceipt", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Sales.DeliveryReceipt", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                //DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode = '" + DeliveryReceiptDetail.ItemCode + "'", Conn);
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= DeliveryReceiptDetail.BulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "SLSDRC");
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", DeliveryReceiptDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", DeliveryReceiptDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", DeliveryReceiptDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", DeliveryReceiptDetail.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                //        Gears.CreateData(DT4, Conn);
                //        DT4.Rows.Clear();
                //    }
                //}
                //else
                //{
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "SLSDRC");
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", DeliveryReceiptDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", DeliveryReceiptDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", DeliveryReceiptDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", DeliveryReceiptDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    Gears.CreateData(DT4, Conn);
                //}
            }
            public void UpdateDeliveryReceiptDetail(DeliveryReceiptDetail DeliveryReceiptDetail)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("SELECT BulkQty FROM Sales.DeliveryReceiptDetail WHERE DocNumber = '" + Docnum + "' " +
                //"and LineNumber = '" + DeliveryReceiptDetail.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != DeliveryReceiptDetail.BulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", DeliveryReceiptDetail.LineNumber);
                //        Gears.DeleteData(DT3, Conn);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode  = '" + DeliveryReceiptDetail.ItemCode + "'", Conn);
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= DeliveryReceiptDetail.BulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "SLSDRC");
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", DeliveryReceiptDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", DeliveryReceiptDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", DeliveryReceiptDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", DeliveryReceiptDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", DeliveryReceiptDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);

                //                Gears.CreateData(DT4, Conn);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "SLSDRC");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", DeliveryReceiptDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", DeliveryReceiptDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", DeliveryReceiptDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", DeliveryReceiptDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", DeliveryReceiptDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //            Gears.CreateData(DT4, Conn);
                //        }
                //    }
                //}

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "cond", "LineNumber", DeliveryReceiptDetail.LineNumber);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "SONumber", DeliveryReceiptDetail.SONumber);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "ItemCode", DeliveryReceiptDetail.ItemCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "ColorCode", DeliveryReceiptDetail.ColorCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "ClassCode", DeliveryReceiptDetail.ClassCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "SizeCode", DeliveryReceiptDetail.SizeCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "DeliveredQty", DeliveryReceiptDetail.DeliveredQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Unit", DeliveryReceiptDetail.Unit);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "BulkQty", DeliveryReceiptDetail.BulkQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "BulkUnit", DeliveryReceiptDetail.BulkUnit);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "StatusCode", DeliveryReceiptDetail.StatusCode);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "ReturnedQty", DeliveryReceiptDetail.ReturnedQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "ReturnedBulkQty", DeliveryReceiptDetail.ReturnedBulkQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "SubstituteItem", DeliveryReceiptDetail.SubstituteItem);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "SubstituteColor", DeliveryReceiptDetail.SubstituteColor);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "SubstituteClass", DeliveryReceiptDetail.SubstituteClass);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "FullDesc", DeliveryReceiptDetail.FullDesc);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "BaseQty", DeliveryReceiptDetail.BaseQty);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "BarcodeNo", DeliveryReceiptDetail.BarcodeNo);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "UnitFactor", DeliveryReceiptDetail.UnitFactor);

                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field1", DeliveryReceiptDetail.Field1);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field2", DeliveryReceiptDetail.Field2);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field3", DeliveryReceiptDetail.Field3);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field4", DeliveryReceiptDetail.Field4);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field5", DeliveryReceiptDetail.Field5);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field6", DeliveryReceiptDetail.Field6);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field7", DeliveryReceiptDetail.Field7);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field8", DeliveryReceiptDetail.Field8);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Field9", DeliveryReceiptDetail.Field9);
                
                if (DeliveryReceiptDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "ExpDate", DeliveryReceiptDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "ExpDate", null);
                }
                if (DeliveryReceiptDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "MfgDate", DeliveryReceiptDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "BatchNo", DeliveryReceiptDetail.BatchNo);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "LotNo", DeliveryReceiptDetail.LotNo);

                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "Version", "1");
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "IsByBulk", DeliveryReceiptDetail.IsByBulk);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "set", "TransPONo", DeliveryReceiptDetail.TransPONo);

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteDeliveryReceiptDetail(DeliveryReceiptDetail DeliveryReceiptDetail)
            {                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.DeliveryReceiptDetail", "cond", "LineNumber", DeliveryReceiptDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", DeliveryReceiptDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "SLSDRC");
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Sales.DeliveryReceiptDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Sales.DeliveryReceipt", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Sales.DeliveryReceipt", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }
        public class JournalEntry
        {
            public virtual DeliveryReceipt Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='SLSDRC' ", Conn);

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
            public virtual DeliveryReceipt Parent { get; set; }
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
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID = B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='SLSDRC' OR  A.TransType='SLSDRC') ", Conn);
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

            a = Gears.RetriveData2("SELECT * FROM Sales.DeliveryReceipt WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Type = String.IsNullOrEmpty(dtRow["Type"].ToString()) ? "X" : dtRow["Type"].ToString();
                RefSONum = dtRow["RefSONum"].ToString();
                PLDocNum = dtRow["PLDocNum"].ToString();
                TIN = dtRow["TIN"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                TotalQty = dtRow["TotalQty"].ToString();
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                Outlet = dtRow["Outlet"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                InvoiceDocNum = dtRow["InvoiceDocNum"].ToString();
                DeliveryAddress = dtRow["DeliveryAddress"].ToString();
                CounterDocNum = dtRow["CounterDocNum"].ToString();
                Terms = Convert.ToInt32(Convert.IsDBNull(dtRow["Terms"]) ? 0 : dtRow["Terms"]);
                CustomerPO = dtRow["CustomerPO"].ToString();
                WithoutRef = Convert.ToBoolean(Convert.IsDBNull(dtRow["WithoutRef"]) ? false : dtRow["WithoutRef"]);

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
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                ForwardedDate = dtRow["ForwardedDate"].ToString();
                DispatchDate = dtRow["DispatchDate"].ToString();
            }

            return a;
        }
        public void InsertData(DeliveryReceipt _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Type", _ent.Type);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "RefSONum", _ent.RefSONum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "PLDocNum", _ent.PLDocNum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "TIN", _ent.TIN);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Outlet", _ent.Outlet);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "InvoiceDocNum", _ent.InvoiceDocNum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "DeliveryAddress", _ent.DeliveryAddress);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "CounterDocNum", _ent.CounterDocNum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Terms", _ent.Terms);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "CustomerPO", _ent.CustomerPO);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "WithoutRef", _ent.WithoutRef);

            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Sales.DeliveryReceipt", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(DeliveryReceipt _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.DeliveryReceipt", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Type", _ent.Type);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "RefSONum", _ent.RefSONum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "PLDocNum", _ent.PLDocNum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "TIN", _ent.TIN);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Outlet", _ent.Outlet);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "InvoiceDocNum", _ent.InvoiceDocNum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "DeliveryAddress", _ent.DeliveryAddress);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "CounterDocNum", _ent.CounterDocNum);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "CustomerPO", _ent.CustomerPO);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "WithoutRef", _ent.WithoutRef);

            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Sales.DeliveryReceipt", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSDRC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(DeliveryReceipt _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.DeliveryReceipt", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Sales.DeliveryReceiptDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("SLSDRC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Sales.DeliveryReceiptDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }

        public void SubstituteInfo(string DocNumber, string Customer, string Conn)
        {
            DataTable subs = Gears.RetriveData2("SELECT * FROM Sales.DeliveryReceiptDetail WHERE Docnumber ='" + DocNumber + "'", Conn);
            if (subs.Rows.Count != 0)
            {
                DataTable update = Gears.RetriveData2("UPDATE A SET A.SubstituteItem = B.SubstitutedItem, A.SubstituteColor = B.SubstitutedColor, A.SubstituteClass = B.SubstitutedClass " +
                                                    " FROM Sales.DeliveryReceiptDetail A " +
                                                    " INNER JOIN Masterfile.ItemCustomerPrice B " +
                                                    " ON A.ItemCode = B.ItemCode " +
                                                    " AND A.ColorCode = B.ColorCode " +
                                                    " AND A.SizeCode = B.SizeCode " +
                                                    " AND A.ClassCode = B.ClassCode " +
                                                    " WHERE A.DocNumber ='" + DocNumber + "' AND B.Customer = '" + Customer + "'", Conn);
            }
        }

        public void UpdateRefSO(string DocNumber, string Conn)
        {
            DataTable RefSo = Gears.RetriveData2(";WITH A AS ( "
	                                           + " SELECT DISTINCT DocNumber, SONumber FROM Sales.DeliveryReceiptDetail "
	                                           + " WHERE DocNumber = '" + DocNumber + "' "
	                                           + " ) UPDATE X SET RefSONum = UpdatedSO FROM Sales.DeliveryReceipt X "
	                                           + " INNER JOIN ( SELECT  DocNumber, STUFF((SELECT ';' + CAST(SONumber AS VARCHAR(MAX)) "
	                                           + " FROM A ORDER BY SONumber ASC FOR XML PATH(''), TYPE).value('.','VARCHAR(MAX)'),1,1,'') AS UpdatedSO "
                                               + " FROM A) AS Y ON X.DocNumber = Y.DocNumber ", Conn);
        }
    }
}
