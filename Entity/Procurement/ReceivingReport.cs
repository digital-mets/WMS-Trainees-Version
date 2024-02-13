using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    #region Changes
    //-- =============================================
    //-- Edit By:		Terence Leonard A. Vergara
    //-- Edit Date:   05/13/2016
    //-- Description:	Updated for CountsheetSubsi
    //-- =============================================
    #endregion

    public class ReceivingReport
    {
        private static string Docnum;
        private static string Warehouse;

        private static string Conn;//ADD CONN
        private static string Docdatee;//ADD CONN
        private static string By;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string DocNumber { get; set; }
        public virtual string APVNumber { get; set; }
        public virtual string BrokerAPVNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string ReferenceNumber { get; set; }
        public virtual string AuxilaryReference { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Broker { get; set; }
        public virtual string Terms { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual decimal TotalFreight { get; set; }
        public virtual decimal TotalQuantity { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual bool ComplimentaryItem { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal GrossVatableAmount { get; set; }

        public virtual decimal NonVatableAmount { get; set; }
        public virtual decimal VatAmount { get; set; }
        public virtual decimal WithholdingTax { get; set; }

        public virtual decimal FreightVariance { get; set; }
        public virtual string InvoiceNumber { get; set; }
        public virtual string DRNumber { get; set; }
        public virtual string SINumber { get; set; }
        public virtual bool IsMarkerReceived { get; set; }
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
        public virtual string CostingSubmittedBy { get; set; }
        public virtual string CostingSubmittedDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual IList<ReceivingReportDetail> Detail { get; set; }


        public class ReceivingReportDetail
        {
            public virtual ReceivingReport Parent { get; set; }
            public virtual string DocNumber { get; set; }

            public virtual string LineNumber { get; set; }

            public virtual string PODocNumber { get; set; }
            public virtual string PRNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal ReceivedQty { get; set; }
            public virtual decimal POQty { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual decimal PesoAmount { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal RoundingAmt { get; set; }
            public virtual decimal UnitFreight { get; set; }
            public virtual decimal ReturnedQty { get; set; }
            public virtual decimal ReturnedBulkQty { get; set; }
            public virtual bool IsVat { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal VATRate { get; set; }
            public virtual string ATCCode { get; set; }
            public virtual decimal ATCRate { get; set; }
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

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,FullDesc from Procurement.ReceivingReportDetailPO a left join masterfile.item b on a.ItemCode = b.ItemCode " +
                                            " where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddReceivingReportDetail(ReceivingReportDetail A)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.ReceivingReportDetailPO where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "PODocNumber", A.PODocNumber);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "PRNumber", A.PRNumber);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ItemCode", A.ItemCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ColorCode", A.ColorCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ClassCode", A.ClassCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "SizeCode", A.SizeCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ReceivedQty ", A.ReceivedQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "POQty ", A.POQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Unit", A.Unit);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "UnitCost", A.UnitCost);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "PesoAmount", A.PesoAmount);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "BulkQty", A.BulkQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "RoundingAmt", A.RoundingAmt);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "UnitFreight", A.UnitFreight);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ReturnedQty", A.ReturnedQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ReturnedBulkQty", A.ReturnedBulkQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "IsVat", A.IsVat);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "VATCode", A.VATCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ATCCode", A.ATCCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "VATRate", A.VATRate);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ATCRate", A.ATCRate);

                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field1", A.Field1);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field2", A.Field2);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field3", A.Field3);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field4", A.Field4);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field5", A.Field5);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field6", A.Field6);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field7", A.Field7);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field8", A.Field8);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "Field9", A.Field9);
                if (A.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ExpDate", A.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "ExpDate", null);
                }
                if (A.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "MfgDate", A.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "MfgDate", Docdatee);
                }
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "BatchNo", A.BatchNo);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "0", "LotNo", A.LotNo);

                DT2.Rows.Add("Procurement.ReceivingReport", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.ReceivingReport", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                #region Comments of Kuya Renato
                //Gears.RetriveData2("Update Procurement.ReceivingReport"
                //+ " set TotalFreight=(Select sum(UnitFreight) * sum (ReceivedQty) "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "
                //+ " , TotalQuantity=(Select sum(ReceivedQty) "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "
                //+ " , TotalAmount=( "
                //+ " Select sum(UnitCost) * sum(ReceivedQty) * ExchangeRate "
                //+ " from Procurement.ReceivingReportDetailPO A "
                //+ " inner join Procurement.ReceivingReport B "
                //+ "  on A.DocNumber = B.DocNumber  where A.Docnumber='" + Docnum + "' group by ExchangeRate )  "
                //+ " , ForeignAmount=( "
                //+ " Select sum(UnitCost) * sum(ReceivedQty)  "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "
                //+ " , GrossVatableAmount=( "
                //+ " select  ISNULL(Sum(ReceivedQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.ReceivingReport A "
                //+ " inner join Procurement.ReceivingReportDetailPO B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and VatCode='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "

                //+ " , NonVatableAmount=( "
                //+ " select  ISNULL(Sum(ReceivedQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.ReceivingReport A "
                //+ " inner join Procurement.ReceivingReportDetailPO B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='NONV' "
                //+ " and VatCode='NONV' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where Docnumber = '" + Docnum + "'");

                //Gears.RetriveData2("Update Procurement.ReceivingReport set VatAmount =( "
                //+ " select (ISNULL(Sum(GrossVatableAmount),0)/(1+sum(Rate)))*sum(Rate) "
                //+ " from Procurement.ReceivingReport A "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where docnumber = '" + Docnum + "'");

                //Gears.RetriveData2("Update Procurement.ReceivingReport set  WTaxAmount =( "
                // + " Select (ISNULL(Sum(GrossVatableAmount),0) -ISNULL(Sum(VatAmount),0)) * sum(Rate) "
                // + "  from Procurement.ReceivingReport A "
                // + " inner join Masterfile.BPSupplierInfo B "
                // + " on A.SupplierCode = B.SupplierCode "
                // + "  inner join Masterfile.ATC C "
                // + "  on B.ATCCode = C.ATCCode "
                // + "  where IsWithholdingTaxAgent ='1'"
                // + " and A.DocNumber='" + Docnum + "' ) "
                // + " where docnumber = '" + Docnum + "'");
                #endregion


                #region Countsheet TLAV

                bool isbybulk = false;
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                DataTable CS = Gears.RetriveData2("SELECT IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + A.ItemCode + "'", Conn);
                foreach (DataRow dt in CS.Rows)
                {
                    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                }
                if (isbybulk == true)
                {
                    for (int i = 1; i <= A.BulkQty; i++)
                    {
                        string strLine2 = i.ToString().PadLeft(5, '0');
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", "PRDRRT");
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine2); // RollId
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", A.ItemCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", A.ColorCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", A.ClassCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", A.SizeCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehouse);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", A.ExpDate);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", A.MfgDate == Convert.ToDateTime("1/1/0001 12:00:00 AM") ? Convert.ToDateTime(Docdatee) : A.MfgDate);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", A.BatchNo == "" ? "N/A" : A.BatchNo);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", A.LotNo == "" ? "N/A" : A.LotNo);
                        Gears.CreateData(DT4, Conn);
                        DT4.Rows.Clear();
                    }
                }
                //else
                //{
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", "PRDRRT");
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", A.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", A.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", A.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", A.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                //    //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehouse);
                //    //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", A.ExpDate);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", A.MfgDate);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", A.BatchNo);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", A.LotNo);
                //    Gears.CreateData(DT4, Conn);
                //}
                #endregion

            }
            public void UpdateReceivingReportDetail(ReceivingReportDetail B)
            {
                #region Countsheet
                bool isbybulk = false;

                DataTable dtable = Gears.RetriveData2("SELECT BulkQty, BaseQty FROM Procurement.ReceivingReportDetailPO WHERE DocNumber = '" + Docnum + "' AND LineNumber = '" + B.LineNumber + "'", Conn);
                foreach (DataRow dtrow in dtable.Rows)
                {
                    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != B.BulkQty)
                    {
                        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", B.LineNumber);
                        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", "PRDRRT");
                        Gears.DeleteData(DT3, Conn);

                        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                        DataTable CS = Gears.RetriveData2("SELECT IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + B.ItemCode + "'", Conn);
                        foreach (DataRow dt in CS.Rows)
                        {
                            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                        }
                        if (isbybulk == true)
                        {
                            for (int i = 1; i <= B.BulkQty; i++)
                            {
                                string strLine2 = i.ToString().PadLeft(5, '0');

                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", "PRDRRT");
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", Docnum);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", B.LineNumber);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", strLine2);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", B.ItemCode);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", B.ColorCode);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", B.ClassCode);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", B.SizeCode);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehouse);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", B.ExpDate);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", B.MfgDate == Convert.ToDateTime("1/1/0001 12:00:00 AM") ? Convert.ToDateTime(Docdatee) : B.MfgDate);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", B.BatchNo == "" ? "N/A" : B.BatchNo);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", B.LotNo == "" ? "N/A" : B.LotNo);
                                Gears.CreateData(DT4, Conn);
                                DT4.Rows.Clear();
                            }
                        }
                        //else
                        //{
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", "PRDRRT");
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", Docnum);
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", B.LineNumber);
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", B.LineNumber);
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", B.ItemCode);
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", B.ColorCode);
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", B.ClassCode);
                        //    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", B.SizeCode);
                        //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                        //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                        //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                        //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", B.ExpDate);
                        //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", B.MfgDate);
                        //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", B.BatchNo);
                        //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", B.LotNo);
                        //    Gears.CreateData(DT4, Conn);
                        //}
                    }
                }
                #endregion

                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "cond", "LineNumber", B.LineNumber);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "PODocNumber", B.PODocNumber);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "PRNumber", B.PRNumber);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ItemCode", B.ItemCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ColorCode", B.ColorCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ClassCode", B.ClassCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "SizeCode", B.SizeCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ReceivedQty ", B.ReceivedQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "POQty ", B.POQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Unit", B.Unit);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "UnitCost", B.UnitCost);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "PesoAmount", B.PesoAmount);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "BulkQty", B.BulkQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "RoundingAmt", B.RoundingAmt);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "UnitFreight", B.UnitFreight);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ReturnedQty", B.ReturnedQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ReturnedBulkQty", B.ReturnedBulkQty);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "IsVat", B.IsVat);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "VATCode", B.VATCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ATCCode", B.ATCCode);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "VATRate", B.VATRate);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ATCRate", B.ATCRate);

                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field1", B.Field1);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field2", B.Field2);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field3", B.Field3);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field4", B.Field4);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field5", B.Field5);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field6", B.Field6);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field7", B.Field7);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field8", B.Field8);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "Field9", B.Field9);
                if (B.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ExpDate", B.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "ExpDate", null);
                }
                if (B.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "MfgDate", B.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "MfgDate", Docdatee);
                }
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "BatchNo", B.BatchNo);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "set", "LotNo", B.LotNo);

                Gears.UpdateData(DT1, Conn);

                #region Comments of Kuya Renato
                //Gears.RetriveData2("Update Procurement.ReceivingReport"
                //+ " set TotalFreight=(Select sum(UnitFreight) * sum (ReceivedQty) "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "
                //+ " , TotalQuantity=(Select sum(ReceivedQty) "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "
                //+ " , TotalAmount=( "
                //+ " Select sum(UnitCost) * sum(ReceivedQty) * SUM(ExchangeRate) "
                //+ " from Procurement.ReceivingReportDetailPO A "
                //+ " inner join Procurement.ReceivingReport B "
                //+ "  on A.DocNumber = B.DocNumber  where A.Docnumber='" + Docnum + "' )  "
                //+ " , ForeignAmount=( "
                //+ " Select sum(UnitCost) * sum(ReceivedQty)  "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "
                //+ " , GrossVatableAmount=( "
                //+ " select  ISNULL(Sum(ReceivedQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.ReceivingReport A "
                //+ " inner join Procurement.ReceivingReportDetailPO B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and VatCode='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " , NonVatableAmount=( "
                //+ " select  ISNULL(Sum(ReceivedQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.ReceivingReport A "
                //+ " inner join Procurement.ReceivingReportDetailPO B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='NONV' "
                //+ " and VatCode='NONV' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where Docnumber = '" + Docnum + "'", Conn);

                //Gears.RetriveData2("Update Procurement.ReceivingReport set VatAmount =( "
                //+ " select (ISNULL(Sum(GrossVatableAmount),0)/(1+sum(Rate)))*sum(Rate) "
                //+ " from Procurement.ReceivingReport A "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where docnumber = '" + Docnum + "'", Conn);

                //Gears.RetriveData2("Update Procurement.ReceivingReport set  WTaxAmount =( "
                // + " Select (ISNULL(Sum(GrossVatableAmount),0) -ISNULL(Sum(VatAmount),0)) * sum(Rate) "
                // + "  from Procurement.ReceivingReport A "
                // + " inner join Masterfile.BPSupplierInfo B "
                // + " on A.SupplierCode = B.SupplierCode "
                // + "  inner join Masterfile.ATC C "
                // + "  on B.ATCCode = C.ATCCode "
                // + "  where IsWithholdingTaxAgent ='1'"
                // + " and A.DocNumber='" + Docnum + "' ) "
                // + " where docnumber = '" + Docnum + "'", Conn);
                #endregion
            }
            public void DeleteReceivingReportDetaill(ReceivingReportDetail ReceivingReportDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.ReceivingReportDetailPO", "cond", "LineNumber", ReceivingReportDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", ReceivingReportDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", "PRDRRT");
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.ReceivingReportDetailPO where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.ReceivingReport", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.ReceivingReport", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }



                //Gears.RetriveData2("Update Procurement.ReceivingReport"
                //+ " set TotalFreight=(Select sum(UnitFreight) * sum (ReceivedQty) "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "

                //+ " , TotalQuantity=(Select sum(ReceivedQty) "

                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "

                //+ " , TotalAmount=( "

                //+ " Select sum(UnitCost) * sum(ReceivedQty) * SUM(ExchangeRate) "
                //+ " from Procurement.ReceivingReportDetailPO A "
                //+ " inner join Procurement.ReceivingReport B "
                //+ "  on A.DocNumber = B.DocNumber  where A.Docnumber='" + Docnum + "' )  "

                //+ " , ForeignAmount=( "

                //+ " Select sum(UnitCost) * sum(ReceivedQty)  "
                //+ " from Procurement.ReceivingReportDetailPO where Docnumber='" + Docnum + "' ) "

                //+ " , GrossVatableAmount=( "
                //+ " select  ISNULL(Sum(ReceivedQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.ReceivingReport A "
                //+ " inner join Procurement.ReceivingReportDetailPO B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and VatCode='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "

                //+ " , NonVatableAmount=( "
                //+ " select  ISNULL(Sum(ReceivedQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.ReceivingReport A "
                //+ " inner join Procurement.ReceivingReportDetailPO B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='NONV' "
                //+ " and VatCode='NONV' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where Docnumber = '" + Docnum + "'", Conn);

                //Gears.RetriveData2("Update Procurement.ReceivingReport set VatAmount =( "
                //+ " select (ISNULL(Sum(GrossVatableAmount),0)/(1+sum(Rate)))*sum(Rate) "
                //+ " from Procurement.ReceivingReport A "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where docnumber = '" + Docnum + "'", Conn);

                //Gears.RetriveData2("Update Procurement.ReceivingReport set  WTaxAmount =( "
                // + " Select (ISNULL(Sum(GrossVatableAmount),0) -ISNULL(Sum(VatAmount),0)) * sum(Rate) "
                // + "  from Procurement.ReceivingReport A "
                // + " inner join Masterfile.BPSupplierInfo B "
                // + " on A.SupplierCode = B.SupplierCode "
                // + "  inner join Masterfile.ATC C "
                // + "  on B.ATCCode = C.ATCCode "
                // + "  where IsWithholdingTaxAgent ='1'"
                // + " and A.DocNumber='" + Docnum + "' ) "
                // + " where docnumber = '" + Docnum + "'", Conn);

            }

            public DataTable SubmitWH(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select Docnumber from Procurement.ReceivingReport A where DocNumber='" + DocNumber + "' and ISNULL(SubmittedBy,'')!=''", Conn);
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
            public virtual ReceivingReport Parent { get; set; }
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
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDRRT' OR  A.TransType='PRDRRT') ", Conn);
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
            a = Gears.RetriveData2("select * from Procurement.ReceivingReport where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                APVNumber = dtRow["APVNumber"].ToString();
                BrokerAPVNumber = dtRow["BrokerAPVNumber"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                AuxilaryReference = dtRow["AuxilaryReference"].ToString();
                DRNumber = dtRow["DRNumber"].ToString();
                SINumber = dtRow["InvoiceNumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Broker = dtRow["Broker"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                ReferenceNumber = dtRow["ReferenceNumber"].ToString();

                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                TotalFreight = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalFreight"]) ? 0 : dtRow["TotalFreight"]);
                TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                ComplimentaryItem = Convert.ToBoolean(Convert.IsDBNull(dtRow["ComplimentaryItem"]) ? false : dtRow["ComplimentaryItem"]);
                Currency = dtRow["Currency"].ToString();
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatableAmount"]) ? 0 : dtRow["GrossVatableAmount"]);
                NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatableAmount"]) ? 0 : dtRow["NonVatableAmount"]);
                VatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatAmount"]) ? 0 : dtRow["VatAmount"]);
                WithholdingTax = Convert.ToDecimal(Convert.IsDBNull(dtRow["WTaxAmount"]) ? 0 : dtRow["WTaxAmount"]);

                Terms = dtRow["Terms"].ToString();
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
                CostingSubmittedBy = dtRow["CostingSubmittedBy"].ToString();
                CostingSubmittedDate = dtRow["CostingSubmittedDate"].ToString();


            }
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as CustomerCode,'' as DeliveryDate ,'' as PicklistType ,'' as Type " +
            //         ",'' as StorerKey ,'' as WarehouseCode ,'' as PlantCode ,'' as ReferenceNo ,'' as Remarks ,'' as OutboundNo  ,0 as IsAutoPick  ,'' as Field1" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(ReceivingReport _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Docdatee = _ent.DocDate;
            By = _ent.LastEditedBy;
            Warehouse = _ent.WarehouseCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.ReceivingReport", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "APVNumber ", _ent.APVNumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "ReferenceNumber ", _ent.ReferenceNumber);

            DT1.Rows.Add("Procurement.ReceivingReport", "0", "SupplierCode ", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "AuxilaryReference ", _ent.AuxilaryReference);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "DRNumber ", _ent.DRNumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "InvoiceNumber ", _ent.SINumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Remarks ", _ent.Remarks);

            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Broker ", _ent.Broker);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "WarehouseCode ", _ent.WarehouseCode);

            DT1.Rows.Add("Procurement.ReceivingReport", "0", "ExchangeRate ", _ent.ExchangeRate);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "TotalFreight ", _ent.TotalFreight);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "ComplimentaryItem ", _ent.ComplimentaryItem);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "TotalQuantity ", _ent.TotalQuantity);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "TotalBulkQty ", _ent.TotalBulkQty);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Currency ", _ent.Currency);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "TotalAmount ", _ent.TotalAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "ForeignAmount ", _ent.ForeignAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "GrossVatableAmount ", _ent.GrossVatableAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "NonVatableAmount ", _ent.NonVatableAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "VatAmount ", _ent.VatAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "WTaxAmount  ", _ent.WithholdingTax);

            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "Field9", _ent.Field9);
            DataTable dtcheck = Gears.RetriveData2("select Docnumber from Procurement.ReceivingReport where DocNumber = '" + DocNumber + "' and ISNULL(SubmittedBy,'')=''", _ent.Connection);
            if (dtcheck.Rows.Count > 0)
            {
                DT1.Rows.Add("Procurement.ReceivingReport", "0", "SubmittedBy", "");
                DT1.Rows.Add("Procurement.ReceivingReport", "0", "CostingSubmittedBy", "");
            }
            DT1.Rows.Add("Procurement.ReceivingReport", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(ReceivingReport _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Docdatee = _ent.DocDate;
            By = _ent.LastEditedBy;
            Warehouse = _ent.WarehouseCode;

            if (_ent.ExchangeRate == 0)
            {
                _ent.ExchangeRate = 1;
            }
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.ReceivingReport", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "DocDate ", _ent.DocDate);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "APVNumber ", _ent.APVNumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "ReferenceNumber ", _ent.ReferenceNumber);

            DT1.Rows.Add("Procurement.ReceivingReport", "set", "SupplierCode ", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "AuxilaryReference ", _ent.AuxilaryReference);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "DRNumber ", _ent.DRNumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "InvoiceNumber ", _ent.SINumber);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Remarks ", _ent.Remarks);

            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Broker ", _ent.Broker);

            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Terms ", _ent.Terms);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "WarehouseCode ", _ent.WarehouseCode);

            DT1.Rows.Add("Procurement.ReceivingReport", "set", "ExchangeRate ", _ent.ExchangeRate);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "TotalFreight ", _ent.TotalFreight);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "TotalQuantity ", _ent.TotalQuantity);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "ComplimentaryItem ", _ent.ComplimentaryItem);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Currency ", _ent.Currency);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "TotalAmount ", _ent.TotalAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "ForeignAmount ", _ent.ForeignAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "GrossVatableAmount ", _ent.GrossVatableAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "NonVatableAmount ", _ent.NonVatableAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "VatAmount ", _ent.VatAmount);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "WTaxAmount  ", _ent.WithholdingTax);

            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "LastEditedBy", _ent.LastEditedBy);

            DT1.Rows.Add("Procurement.ReceivingReport", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Procurement.ReceivingReport", "set", "CostingSubmittedBy", "");

            DataTable dtcheck = Gears.RetriveData2("select Docnumber from Procurement.ReceivingReport where DocNumber = '" + DocNumber + "' and ISNULL(SubmittedBy,'')=''", _ent.Connection);
            if (dtcheck.Rows.Count > 0)
            {
                DT1.Rows.Add("Procurement.ReceivingReport", "set", "SubmittedBy", "");

            }

            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDRRT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(ReceivingReport _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Docdatee = _ent.DocDate;
            By = _ent.LastEditedBy;
            Warehouse = _ent.WarehouseCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.ReceivingReport", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDRRT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public class JournalEntry
        {
            public virtual ReceivingReport Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDRRT' or TransType = 'ACTAQT') ", Conn);

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
