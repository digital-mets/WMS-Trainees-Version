using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{   
    public class PurchaseReturn
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Docnum;

        private static string trans;

        private static string ddate;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TransType { get; set; }
        public virtual string RRNumber { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual string PLDocNumber { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Currency { get; set; }
        public virtual bool ReturnOnOrder { get; set; }
        public virtual string Reason { get; set; }
        public virtual string DRDocNumber { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual decimal GrossVatAmount { get; set; }
        public virtual decimal NonVatAmount { get; set; }
        public virtual decimal VatAmount { get; set; }
        public virtual decimal WithholdingTax { get; set; }
        public virtual string Terms { get; set; }
        public virtual string APMemo { get; set; }

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

        public virtual IList<PurchaseReturnDetail> Detail { get; set; }


        public class PurchaseReturnDetail
        {
            public virtual PurchaseReturn Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string PODocNumber { get; set; }
            public virtual string Unit { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal ReturnedQty { get; set; }
            public virtual decimal ReturnedBulkQty { get; set; }
            public virtual decimal AverageCost { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual bool IsVat { get; set; }
            public virtual string VatCode { get; set; }
            //public virtual string CountSheet { get; set; }
            //public virtual string PropertyNumber { get; set; }


            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual DateTime ExpDate { get; set; }
            public virtual DateTime MfgDate { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string LotNo { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,FullDesc from Procurement.PurchaseReturnDetail a left join masterfile.item b on a.ItemCode = b.ItemCode " +
                                            " where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchaseReturnDetail(PurchaseReturnDetail PurchaseReturnDetail)
            {
                int linenum = 0;
                bool isbybulk = false;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseReturnDetail where docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "ItemCode", PurchaseReturnDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "ColorCode", PurchaseReturnDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "ClassCode", PurchaseReturnDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "SizeCode", PurchaseReturnDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "PODocNumber", PurchaseReturnDetail.PODocNumber);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Unit", PurchaseReturnDetail.Unit);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "StatusCode ", PurchaseReturnDetail.StatusCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "ReturnedQty", PurchaseReturnDetail.ReturnedQty);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "ReturnedBulkQty ", PurchaseReturnDetail.ReturnedBulkQty);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "AverageCost", PurchaseReturnDetail.AverageCost);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "UnitCost", PurchaseReturnDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "BaseQty", PurchaseReturnDetail.BaseQty);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "IsVat", PurchaseReturnDetail.IsVat);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "VatCode", PurchaseReturnDetail.VatCode);
                //DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "CountSheet", PurchaseReturnDetail.CountSheet);
                //DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "PropertyNumber", PurchaseReturnDetail.PropertyNumber);

                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field1", PurchaseReturnDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field2", PurchaseReturnDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field3", PurchaseReturnDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field4", PurchaseReturnDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field5", PurchaseReturnDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field6", PurchaseReturnDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field7", PurchaseReturnDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field8", PurchaseReturnDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "Field9", PurchaseReturnDetail.Field9);
                if (PurchaseReturnDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "ExpDate", PurchaseReturnDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "ExpDate", null);
                }
                if (PurchaseReturnDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "MfgDate", PurchaseReturnDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "BatchNo", PurchaseReturnDetail.BatchNo);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "0", "LotNo", PurchaseReturnDetail.LotNo);

                DT2.Rows.Add("Procurement.PurchaseReturn", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseReturn", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            //    DataTable CS = Gears.RetriveData2("Select IsByBulk from masterfile.item where itemcode = '" + PurchaseReturnDetail.ItemCode + "'", Conn);
            //    Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            //    Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();



             
            //    foreach (DataRow dt in CS.Rows)
            //    {
            //        isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
            //    }
            //    if (isbybulk == true)
            //    {

            //        for (int i = 1; i <= PurchaseReturnDetail.ReturnedBulkQty; i++)
            //        {
            //            string strLine2 = i.ToString().PadLeft(5, '0');
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PurchaseReturnDetail.ItemCode);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PurchaseReturnDetail.ColorCode);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PurchaseReturnDetail.ClassCode);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PurchaseReturnDetail.SizeCode);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
            //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
            //            Gears.CreateData(DT4, Conn);
            //            DT4.Rows.Clear();
            //        }
            //    }
            //    else
            //    {
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", linenum);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", linenum);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PurchaseReturnDetail.ItemCode);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PurchaseReturnDetail.ColorCode);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PurchaseReturnDetail.ClassCode);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PurchaseReturnDetail.SizeCode);
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", "GETDATE()");
            //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", PurchaseReturnDetail.ReturnedBulkQty);
            //        Gears.CreateData(DT4, Conn);
            //    }
            //    Gears.RetriveData2("Update wms.ItemAdjustment set TotalAdjustment=(Select sum(Qty) from wms.ItemAdjustmentDetail where Docnumber='" + Docnum + "' ) where Docnumber = '" + Docnum + "'", Conn);

            }
            //public void UpdatePurchaseReturnDetail(PurchaseReturnDetail PurchaseReturnDetail)
            //{
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("Select ReturnedBulkQty from Procurement.PurchaseReturnDetail where docnumber = '" + PurchaseReturnDetail.DocNumber + "' " +
                //"and LineNumber = '" + PurchaseReturnDetail.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["ReturnedBulkQty"].ToString()) != PurchaseReturnDetail.ReturnedBulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", PurchaseReturnDetail.DocNumber);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PurchaseReturnDetail.LineNumber);
                //        Gears.DeleteData(DT3, Conn);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("Select IsByBulk from masterfile.item where itemcode = '" + PurchaseReturnDetail.ItemCode + "'", Conn);
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= PurchaseReturnDetail.ReturnedBulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PurchaseReturnDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PurchaseReturnDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PurchaseReturnDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PurchaseReturnDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PurchaseReturnDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);

                //                Gears.CreateData(DT4, Conn);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PurchaseReturnDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", PurchaseReturnDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PurchaseReturnDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PurchaseReturnDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PurchaseReturnDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PurchaseReturnDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", "GETDATE()");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", PurchaseReturnDetail.ReturnedBulkQty);
                //            Gears.CreateData(DT4, Conn);
                //        }
                //    }
                //}
            //}
            public void UpdatePurchaseReturnDetail(PurchaseReturnDetail PurchaseReturnDetail)
            { 
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "cond", "LineNumber", PurchaseReturnDetail.LineNumber);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "ItemCode", PurchaseReturnDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "ColorCode", PurchaseReturnDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "ClassCode", PurchaseReturnDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "SizeCode", PurchaseReturnDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "PODocNumber", PurchaseReturnDetail.PODocNumber);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Unit", PurchaseReturnDetail.Unit);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "StatusCode ", PurchaseReturnDetail.StatusCode);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "ReturnedQty", PurchaseReturnDetail.ReturnedQty);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "ReturnedBulkQty ", PurchaseReturnDetail.ReturnedBulkQty);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "AverageCost", PurchaseReturnDetail.AverageCost);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "UnitCost", PurchaseReturnDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "BaseQty", PurchaseReturnDetail.BaseQty);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "IsVat", PurchaseReturnDetail.IsVat);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "VatCode", PurchaseReturnDetail.VatCode);
                //DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "CountSheet", PurchaseReturnDetail.CountSheet);
                //DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "PropertyNumber", PurchaseReturnDetail.PropertyNumber);

                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field1", PurchaseReturnDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field2", PurchaseReturnDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field3", PurchaseReturnDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field4", PurchaseReturnDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field5", PurchaseReturnDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field6", PurchaseReturnDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field7", PurchaseReturnDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field8", PurchaseReturnDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "Field9", PurchaseReturnDetail.Field9);

                if (PurchaseReturnDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "ExpDate", PurchaseReturnDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "ExpDate", null);
                }
                if (PurchaseReturnDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "MfgDate", PurchaseReturnDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "BatchNo", PurchaseReturnDetail.BatchNo);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "set", "LotNo", PurchaseReturnDetail.LotNo);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePurchaseReturnDetail(PurchaseReturnDetail PurchaseReturnDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseReturnDetail", "cond", "LineNumber", PurchaseReturnDetail.LineNumber); 
                Gears.DeleteData(DT1, Conn);


                DT1.Rows.Add("WMS.CountsheetSubsi", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.CountsheetSubsi", "cond", "TransLine", PurchaseReturnDetail.LineNumber);
                DT1.Rows.Add("WMS.CountsheetSubsi", "cond", "TransType", "PRPRT");
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseReturnDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseReturnDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseReturnDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        public class RefTransaction
        {
            public virtual PurchaseReturn Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='PRPRT' OR  A.TransType='PRPRT') ", Conn);
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

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Procurement.PurchaseReturn where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                RRNumber = dtRow["RRNumber"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                PLDocNumber = dtRow["PLDocNumber"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Currency = dtRow["Currency"].ToString();
                ReturnOnOrder = Convert.ToBoolean(Convert.IsDBNull(dtRow["ReturnOnOrder"]) ? false : dtRow["ReturnOnOrder"]);
                //AdjustmentCode = dtRow["AdjustmentCode"].ToString();
                Reason = dtRow["Reason"].ToString();
                DRDocNumber = dtRow["DRDocNumber"].ToString();
                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatAmount"]) ? 0 : dtRow["GrossVatAmount"]);
                NonVatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatAmount"]) ? 0 : dtRow["NonVatAmount"]);
                VatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatAmount"]) ? 0 : dtRow["VatAmount"]);
                WithholdingTax = Convert.ToDecimal(Convert.IsDBNull(dtRow["WithholdingTax"]) ? 0 : dtRow["WithholdingTax"]);
                Terms = dtRow["Terms"].ToString();
                APMemo = dtRow["APMemo"].ToString();
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
            }

            return a;
        }
        public void InsertData(PurchaseReturn _ent)
        {

            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "RRNumber", _ent.RRNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "TotalAmount", _ent.PesoAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "PLDocNumber", _ent.PLDocNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "ReturnOnOrder", _ent.ReturnOnOrder);
            //DT1.Rows.Add("Procurement.PurchaseReturn", "0", "AdjustmentCode", _ent.AdjustmentCode);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Reason", _ent.Reason);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "DRDocNumber", _ent.DRDocNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "GrossVatAmount", _ent.GrossVatAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "NonVatAmount", _ent.NonVatAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "WithholdingTax", _ent.WithholdingTax);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Terms", _ent.Terms);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "APMemo", _ent.APMemo);

            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Procurement.PurchaseReturn", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("PRPRT", Docnum, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); //KMM add Conn

        }

        public void UpdateData(PurchaseReturn _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            trans = _ent.TransType;
            ddate = _ent.DocDate;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseReturn", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "RRNumber", _ent.RRNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "TotalAmount", _ent.PesoAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "PLDocNumber", _ent.PLDocNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "ReturnOnOrder", _ent.ReturnOnOrder);
            //DT1.Rows.Add("Procurement.PurchaseReturn", "set", "AdjustmentCode", _ent.AdjustmentCode);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Reason", _ent.Reason);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "DRDocNumber", _ent.DRDocNumber);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "GrossVatAmount", _ent.GrossVatAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "NonVatAmount", _ent.NonVatAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "WithholdingTax", _ent.WithholdingTax);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "APMemo", _ent.APMemo);

            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.PurchaseReturn", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRPRT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        public void DeleteData(PurchaseReturn _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.PurchaseReturn", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("PRPRT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }
        public class JournalEntry
        {

            public virtual PurchaseReturn Parent { get; set; }
            private static string Conn { get; set; }
            public virtual string Connection { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " LEFT JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " LEFT JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='PRPRT' ", Conn);

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