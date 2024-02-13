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
    //-- Edit By:		Luis Genel T. Edpao
    //-- Edit Date:     12/11/2015
    //-- Description:	Added ICNTotalQty (Header), ICNQty(Detail), Status(Detail)
    //-- =============================================
    #endregion

    public class Inbound
    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string RecLoc;

        private static string trans;

        private static string ddate;
        public virtual string DocNumber { get; set; }
        public virtual string TransType { get; set; }
        public virtual string TranType { get; set; }
        public virtual string Plant { get; set; }
        public virtual string DRNumber { get; set; }
        public virtual string ContainerTemp { get; set; }
        public virtual string Driver { get; set; }
        public virtual string PlateNo { get; set; }
        public virtual string Consignee { get; set; }
        public virtual string Delivery { get; set; }
        public virtual string ConAddress { get; set; }
        public virtual string TrackingNo { get; set; }
        public virtual string ReqDept { get; set; }
        public virtual string ShipmentType { get; set; }
        public virtual string RefDoc { get; set; }
        public virtual string Overtime { get; set; }
        public virtual string AddManpower { get; set; }
        public virtual string NOmanpower { get; set; }
        public virtual string TBSB { get; set; }
        public virtual string TruckT { get; set; }
        public virtual string Stat { get; set; }
        public virtual string TruckProv { get; set; }
        public virtual string Batch { get; set; }
        public virtual string Consigne { get; set; }
        public virtual string ContainNumber { get; set; }
        public virtual string ContactingDept { get; set; }
        public virtual string InvoiceNo { get; set; }
        public virtual string SealNo { get; set; }
        public virtual string Supplier { get; set; }
        public virtual string AWB { get; set; }
        public virtual string GuardOnDuty { get; set; }
        public virtual string CustomerRepresentative { get; set; }
        public virtual string ApprovingOfficer { get; set; }
        public virtual bool IsNoCharge { get; set; }
        public virtual string Packing { get; set; }
        public virtual string AssignLoc { get; set; }
        public virtual decimal ICNTotalQty { get; set; }

  

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
        public virtual string ProdNumber { get; set; }
        public virtual string StorageType { get; set; }
        public virtual bool IsService { get; set; }
        public virtual string WeekNo { get; set; }

        //Truck Transaction



        public virtual string ArrivalTime { get; set; }
        public virtual string StartUnloading { get; set; }
        public virtual string CompleteUnloading { get; set; }
        public virtual string DepartureTime { get; set; }
        public virtual string HoldReason { get; set; }
        public virtual string HoldRemarks { get; set; }
        public virtual string HoldDate { get; set; }
        public virtual string UnHoldDate { get; set; }
        public virtual string HoldDuration { get; set; }
        //public virtual string Trucker { get; set; }
        public virtual string DwellTime { get; set; }
        public virtual string CheckingEnd { get; set; }
        public virtual string EndProcessing { get; set; }
        public virtual string StartProcessing { get; set; }
        public virtual string CheckingStart { get; set; }

        public virtual string DockingTime { get; set; }
        public virtual string Qty { get; set; }
        public virtual string Pallet { get; set; }



        //public virtual string TruckNo { get; set; }
        public virtual string Arrival { get; set; }
        public virtual string Departure { get; set; }
        public virtual string StartUnload { get; set; }
        public virtual string CompleteUnload { get; set; }

        public virtual string DocumentationStaff { get; set; }
        public virtual string WarehouseChecker { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Status { get; set; }


        public virtual string DocDate { get; set; }
        public virtual string ICNNumber { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }

        public virtual string ContainerNo { get; set; }
        public virtual string HoldStatus { get; set; }
        public virtual string TruckType { get; set; }
        public virtual string DocumentBy { get; set; }
        public virtual string CheckedBy { get; set; }
        public virtual bool InternalExternal { get; set; }
        public virtual string DockingDoor { get; set; }

        public virtual IList<InboundDetail> Detail { get; set; }

        public class InboundDetail
        {
            public virtual Inbound Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal ReceivedQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual DateTime ExpiryDate { get; set; }
         
            public virtual string BatchNumber { get; set; }
            public virtual DateTime ManufacturingDate { get; set; }
            public virtual string ToLocation { get; set; }
            public virtual string PalletID { get; set; }
            public virtual string LotID { get; set; }
            public virtual DateTime RRDocDate { get; set; }
            public virtual decimal PickedQty { get; set; }
            public virtual string Remarks { get; set; }

            public virtual string SpecialHandlingInstruc { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual string Status { get; set; }
            public virtual decimal ICNQty { get; set; }
            //     public virtual string ServiceTypeCode { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select a.*,b.FullDesc from WMS.InboundDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "'order by LineNumber"
                        , Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddInboundDetail(InboundDetail InboundDetail)
            {
                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.InboundDetail where docnumber = '" + Docnum + "'"
                    , Conn);

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
                DT1.Rows.Add("WMS.InboundDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.InboundDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ItemCode", InboundDetail.ItemCode);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ColorCode", InboundDetail.ColorCode);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ClassCode", InboundDetail.ClassCode);
                DT1.Rows.Add("WMS.InboundDetail", "0", "SizeCode", InboundDetail.SizeCode);
                DT1.Rows.Add("WMS.InboundDetail", "0", "BulkQty", InboundDetail.BulkQty);
                DT1.Rows.Add("WMS.InboundDetail", "0", "BulkUnit ", InboundDetail.BulkUnit);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ReceivedQty", InboundDetail.ReceivedQty);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Unit ", InboundDetail.Unit);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ExpiryDate", InboundDetail.ExpiryDate);
                DT1.Rows.Add("WMS.InboundDetail", "0", "BatchNumber", InboundDetail.BatchNumber);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ManufacturingDate", InboundDetail.ManufacturingDate);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ToLocation", RecLoc);
                DT1.Rows.Add("WMS.InboundDetail", "0", "PalletID", PalletID);
                DT1.Rows.Add("WMS.InboundDetail", "0", "LotID", InboundDetail.LotID);
                DT1.Rows.Add("WMS.InboundDetail", "0", "RRDocDate", ddate);
                DT1.Rows.Add("WMS.InboundDetail", "0", "PickedQty", InboundDetail.PickedQty);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Remarks", InboundDetail.Remarks);
                DT1.Rows.Add("WMS.InboundDetail", "0", "BaseQty ", InboundDetail.BaseQty);
                DT1.Rows.Add("WMS.InboundDetail", "0", "StatusCode ", InboundDetail.StatusCode);
                DT1.Rows.Add("WMS.InboundDetail", "0", "BarcodeNo", InboundDetail.BarcodeNo);

                DT1.Rows.Add("WMS.InboundDetail", "0", "Status", InboundDetail.Status);
                DT1.Rows.Add("WMS.InboundDetail", "0", "ICNQty", InboundDetail.ICNQty);
                DT1.Rows.Add("WMS.InboundDetail", "0", "SpecialHandlingInstruc", InboundDetail.SpecialHandlingInstruc);

                
                //   DT1.Rows.Add("WMS.InboundDetail", "0", "ServiceTypeCode", InboundDetail.ServiceTypeCode);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field1", InboundDetail.Field1);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field2", InboundDetail.Field2);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field3", InboundDetail.Field3);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field4", InboundDetail.Field4);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field5", InboundDetail.Field5);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field6", InboundDetail.Field6);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field7", InboundDetail.Field7);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field8", InboundDetail.Field8);
                DT1.Rows.Add("WMS.InboundDetail", "0", "Field9", InboundDetail.Field9);

                DT2.Rows.Add("WMS.Inbound", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.Inbound", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                #region Countsheet
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                //DataTable CS = Gears.RetriveData2("Select IsByBulk,isnull(StandardQty,0) as StandardQty from masterfile.item where itemcode = '" + InboundDetail.ItemCode + "'");
                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= InboundDetail.BulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", trans);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", InboundDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", InboundDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", InboundDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", InboundDetail.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", InboundDetail.ManufacturingDate);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", InboundDetail.ExpiryDate);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "PalletID", InboundDetail.PalletID);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", "1");
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", ddate);
                //        Gears.CreateData(DT4);
                //        DT4.Rows.Clear();

                //        decimal dcStandard =Convert.ToDecimal(CS.Rows[0]["StandardQty"]) * Convert.ToDecimal(InboundDetail.BulkQty);
                //        if (dcStandard != Convert.ToDecimal(InboundDetail.ReceivedQty) && dcStandard !=0 )
                //        {
                //            if (i < InboundDetail.BulkQty)
                //            {
                //                Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", strLine);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "LineNumber", strLine2);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBaseQty", CS.Rows[0]["StandardQty"]);

                //                Gears.UpdateData(DT5);

                //            }
                //            else
                //            {
                //                decimal remainingstandard = InboundDetail.ReceivedQty % Convert.ToDecimal(CS.Rows[0]["StandardQty"]);
                //                Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", strLine);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "LineNumber", strLine2);
                //                DT5.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBaseQty", remainingstandard);

                //                Gears.UpdateData(DT5);
                //            }

                //        }
                //        else
                //        {
                //            Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                //            DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                //            DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                //            DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", strLine);
                //            DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "LineNumber", strLine2);
                //            DT5.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBaseQty", CS.Rows[0]["StandardQty"]);

                //            Gears.UpdateData(DT5);
                //        }
                //    }
                //}
                //else
                //{
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", trans);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", InboundDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", InboundDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", InboundDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", InboundDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBaseQty", InboundDetail.ReceivedQty);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", InboundDetail.BulkQty);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", InboundDetail.ManufacturingDate);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", InboundDetail.ExpiryDate);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "PalletID", InboundDetail.PalletID);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", ddate);

                //    Gears.CreateData(DT4);
                //}
                #endregion
            }
            public void UpdateInboundDetail(InboundDetail InboundDetail)
            {
                #region Countsheet
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("Select BulkQty,BaseQty from wms.inbounddetail where docnumber = '" + InboundDetail.DocNumber + "' " +
                //"and LineNumber = '" + InboundDetail.LineNumber + "'");
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != InboundDetail.BulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", InboundDetail.DocNumber);
                //        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", InboundDetail.LineNumber);
                //        Gears.DeleteData(DT3);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("Select IsByBulk,isnull(StandardQty,0) as StandardQty from masterfile.item where itemcode = '" + InboundDetail.ItemCode + "'");
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= InboundDetail.BulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');

                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", InboundDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", InboundDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", InboundDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", InboundDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", InboundDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", InboundDetail.ManufacturingDate);
                //                DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", InboundDetail.ExpiryDate);
                //                DT4.Rows.Add("WMS.CountSheetSetup", "0", "PalletID", InboundDetail.PalletID);
                //                DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", "1");
                //                DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", ddate);

                //                Gears.CreateData(DT4);
                //                DT4.Rows.Clear();

                //     decimal dcStandard =Convert.ToDecimal(CS.Rows[0]["StandardQty"]) * Convert.ToDecimal(InboundDetail.BulkQty);
                //        if (dcStandard != Convert.ToDecimal(InboundDetail.ReceivedQty) && dcStandard !=0 )
                //        {
                //                    if (i < InboundDetail.BulkQty)
                //                    {
                //                        Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", InboundDetail.LineNumber);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "LineNumber", strLine2);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBaseQty", CS.Rows[0]["StandardQty"]);

                //                        Gears.UpdateData(DT5);

                //                    }
                //                    else
                //                    {
                //                        decimal remainingstandard = InboundDetail.ReceivedQty % Convert.ToDecimal(CS.Rows[0]["StandardQty"]);
                //                        Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", InboundDetail.LineNumber);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "LineNumber", strLine2);
                //                        DT5.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBaseQty", remainingstandard);

                //                        Gears.UpdateData(DT5);
                //                    }

                //                }
                //                else
                //                {
                //                    Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                //                    DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                //                    DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                //                    DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", InboundDetail.LineNumber);
                //                    DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "LineNumber", strLine2);
                //                    DT5.Rows.Add("WMS.CountSheetSetUp", "set", "OriginalBaseQty", CS.Rows[0]["StandardQty"]);

                //                    Gears.UpdateData(DT5);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", trans);
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", InboundDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", InboundDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", InboundDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", InboundDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", InboundDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", InboundDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", InboundDetail.ManufacturingDate);
                //            DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", InboundDetail.ExpiryDate);
                //            DT4.Rows.Add("WMS.CountSheetSetup", "0", "PalletID", InboundDetail.PalletID);
                //            DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBaseQty", InboundDetail.ReceivedQty);
                //            DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", InboundDetail.BulkQty);
                //            DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Parent.DocDate);


                //            Gears.CreateData(DT4);
                //        }
                //    }
                //}
                #endregion

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.InboundDetail", "cond", "DocNumber", InboundDetail.DocNumber);
                DT1.Rows.Add("WMS.InboundDetail", "cond", "LineNumber", InboundDetail.LineNumber);
                DT1.Rows.Add("WMS.InboundDetail", "set", "ItemCode", InboundDetail.ItemCode);
                DT1.Rows.Add("WMS.InboundDetail", "set", "ColorCode", InboundDetail.ColorCode);
                DT1.Rows.Add("WMS.InboundDetail", "set", "ClassCode", InboundDetail.ClassCode);
                DT1.Rows.Add("WMS.InboundDetail", "set", "SizeCode", InboundDetail.SizeCode);
                DT1.Rows.Add("WMS.InboundDetail", "set", "BulkQty", InboundDetail.BulkQty);
                DT1.Rows.Add("WMS.InboundDetail", "set", "BulkUnit ", InboundDetail.BulkUnit);
                DT1.Rows.Add("WMS.InboundDetail", "set", "ReceivedQty", InboundDetail.ReceivedQty);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Unit ", InboundDetail.Unit);
                DT1.Rows.Add("WMS.InboundDetail", "set", "ExpiryDate", InboundDetail.ExpiryDate);
                DT1.Rows.Add("WMS.InboundDetail", "set", "BatchNumber", InboundDetail.BatchNumber);
                DT1.Rows.Add("WMS.InboundDetail", "set", "ManufacturingDate", InboundDetail.ManufacturingDate);
                DT1.Rows.Add("WMS.InboundDetail", "set", "PalletID", InboundDetail.PalletID);
                if (!String.IsNullOrEmpty(RecLoc))
                {
                    DT1.Rows.Add("WMS.InboundDetail", "set", "ToLocation", RecLoc);
                }
                //DT1.Rows.Add("WMS.InboundDetail", "set", "PalletID", PalletID);
                //Pallet id of countsheet //2017-05-30  RA Remove na autoupdate nasa validate naman eh
                //DT2.Rows.Add("WMS.CountSheetSetup", "cond", "TransDoc", InboundDetail.DocNumber);
                //DT2.Rows.Add("WMS.CountSheetSetup", "cond", "TransLine", InboundDetail.LineNumber);
                //DT2.Rows.Add("WMS.CountSheetSetup", "set", "PalletID", InboundDetail.PalletID);
                //  DT2.Rows.Add("WMS.CountSheetSetup", "set", "Field1", InboundDetail.ServiceTypeCode);

                DT1.Rows.Add("WMS.InboundDetail", "set", "LotID", InboundDetail.LotID);
                DT1.Rows.Add("WMS.InboundDetail", "set", "RRDocDate", ddate);
                DT1.Rows.Add("WMS.InboundDetail", "set", "PickedQty", InboundDetail.PickedQty);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Remarks", InboundDetail.Remarks);
                DT1.Rows.Add("WMS.InboundDetail", "set", "BaseQty ", InboundDetail.BaseQty);
                DT1.Rows.Add("WMS.InboundDetail", "set", "StatusCode ", InboundDetail.StatusCode);
                DT1.Rows.Add("WMS.InboundDetail", "set", "BarcodeNo", InboundDetail.BarcodeNo);

                DT1.Rows.Add("WMS.InboundDetail", "set", "Status", InboundDetail.Status);
                DT1.Rows.Add("WMS.InboundDetail", "set", "ICNQty", InboundDetail.ICNQty);
                DT1.Rows.Add("WMS.InboundDetail", "set", "SpecialHandlingInstruc", InboundDetail.SpecialHandlingInstruc);

                
                //  DT1.Rows.Add("WMS.InboundDetail", "set", "ServiceTypeCode", InboundDetail.ServiceTypeCode);

                DT1.Rows.Add("WMS.InboundDetail", "set", "Field1", InboundDetail.Field1);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field2", InboundDetail.Field2);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field3", InboundDetail.Field3);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field4", InboundDetail.Field4);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field5", InboundDetail.Field5);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field6", InboundDetail.Field6);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field7", InboundDetail.Field7);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field8", InboundDetail.Field8);
                DT1.Rows.Add("WMS.InboundDetail", "set", "Field9", InboundDetail.Field9);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteInboundDetail(InboundDetail InboundDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.InboundDetail", "cond", "DocNumber", InboundDetail.DocNumber);
                DT1.Rows.Add("WMS.InboundDetail", "cond", "LineNumber", InboundDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", InboundDetail.DocNumber);
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", InboundDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("select * from WMS.InboundDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.Inbound", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.Inbound", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;
            DataTable b;
  
            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select a.*,CASE when A.SubmittedDate = GETDATE() then 'N' when ISNULL(A.SubmittedDate,'') != '' then 'C' ELSE 'X' end as Status" +
                                    ",b.[TruckType],b.[Consignee],b.[ConsigneeAddress],b.[TrackingNO],b.[TruckProviderByMets]" +
                                    ",b.[CompanyDept],b.[ShipmentType],b.[RefDoc],b.[Overtime],b.[AddtionalManpower],b.[NOManpower],b.[SuppliedBy]" +
                                    ",c.Batch as CBatch,c.Consignee as CConsignee,c.Origin as COrigin,ExpectedDeliveryDate " +
                                    "from WMS.Inbound a left join WMS.icn b on a.DocNumber = b.DocNumber left join wms.comi c on a.DocNumber = c.DocNumber where a.DocNumber = '" + DocNumber + "'", Conn);
   


            foreach (DataRow dtRow in a.Rows)
            {

                DocNumber = dtRow["DocNumber"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                ICNNumber = dtRow["ICNNumber"].ToString();
                TranType = dtRow["TranType"].ToString();
                Plant = dtRow["Plant"].ToString();
                DRNumber = dtRow["DRNumber"].ToString();
                ContainerTemp = dtRow["ContainerTemp"].ToString();
                Driver = dtRow["Driver"].ToString();
                Consignee = dtRow["Consignee"].ToString();
                ConAddress = dtRow["ConsigneeAddress"].ToString();
                TrackingNo = dtRow["TrackingNO"].ToString();
                Delivery = dtRow["ExpectedDeliveryDate"].ToString();
                TruckT = dtRow["TruckType"].ToString();
                TruckProv = dtRow["TruckProviderByMets"].ToString();
                ContainNumber = dtRow["ContainerNo"].ToString();
                ContactingDept = dtRow["ContactingDept"].ToString();
                ReqDept = dtRow["CompanyDept"].ToString();
                ShipmentType = dtRow["ShipmentType"].ToString();
                Stat = dtRow["Status"].ToString();
                RefDoc = dtRow["RefDoc"].ToString();
                Overtime = dtRow["Overtime"].ToString();
                AddManpower = dtRow["AddtionalManpower"].ToString();
                NOmanpower = dtRow["NOManpower"].ToString();
                TBSB = dtRow["SuppliedBy"].ToString();
                InvoiceNo = dtRow["InvoiceNo"].ToString();
                PlateNo = dtRow["PlateNo"].ToString();
                SealNo = dtRow["SealNo"].ToString();
                Supplier = dtRow["Supplier"].ToString();
                AWB = dtRow["AWB"].ToString();
                //Trucker = dtRow["Trucker"].ToString();
                DocumentationStaff = dtRow["DocumentationStaff"].ToString();
                WarehouseChecker = dtRow["WarehouseChecker"].ToString();
                GuardOnDuty = dtRow["GuardOnDuty"].ToString();
                CustomerRepresentative = dtRow["CustomerRepresentative"].ToString();
                ApprovingOfficer = dtRow["ApprovingOfficer"].ToString();
                Arrival = dtRow["Arrival"].ToString();
                Departure = dtRow["Departure"].ToString();
                StartUnload = dtRow["StartUnload"].ToString();
                CompleteUnload = dtRow["CompleteUnload"].ToString();
                InternalExternal = Convert.ToBoolean(Convert.IsDBNull(dtRow["InternalExternal"]) ? false : dtRow["InternalExternal"]);
                IsNoCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsNoCharge"]) ? false : dtRow["IsNoCharge"]);
                Packing = dtRow["Packing"].ToString();
                AssignLoc = dtRow["AssignLoc"].ToString();
                DockingDoor = dtRow["LoadingBay"].ToString();
                ICNTotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ICNTotalQty"]) ? 0 : dtRow["ICNTotalQty"]);
                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();

                ArrivalTime = dtRow["Arrival"].ToString();
                StartUnloading = dtRow["StartUnload"].ToString();
                CompleteUnloading = dtRow["CompleteUnload"].ToString();
                DepartureTime = dtRow["Departure"].ToString();
                HoldReason = dtRow["HoldReason"].ToString();
                HoldRemarks = dtRow["HoldRemarks"].ToString();
                HoldDate = dtRow["HoldDate"].ToString();
                UnHoldDate = dtRow["UnHoldDate"].ToString();
                HoldDuration = dtRow["HoldDuration"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                Status = dtRow["Status"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                CheckingEnd = dtRow["CheckingEnd"].ToString();
                EndProcessing = dtRow["EndProcessing"].ToString();
                StartProcessing = dtRow["StartProcessing"].ToString();
                CheckingStart = dtRow["CheckingStart"].ToString();
                DockingTime = dtRow["DockingTime"].ToString();
                //Qty = dtRow["Qty"].ToString();
                //Pallet = dtRow["Pallet"].ToString();
                ContainerNo = dtRow["ContainerNo"].ToString();
                HoldStatus = dtRow["HoldStatus"].ToString();
                TruckType = dtRow["TruckType"].ToString();
                DocumentBy = dtRow["DocumentBy"].ToString();
                CheckedBy = dtRow["CheckedBy"].ToString();
                DwellTime = dtRow["DwellTime"].ToString();

                //Batch generating value
                if (dtRow["CBatch"].ToString() == null || dtRow["CBatch"].ToString() == "")
                {
                    int linenum = 0;
                    try
                    {

                        b = Gears.RetriveData2("select top 1* from wms.[comi] order by AddedDate desc", Conn);

                        if (b.Rows.Count > 0) {
                            linenum = Convert.ToInt32(b.Rows[0]["Batch"].ToString().Split('-')[1]) + 1; 
                         
                        }
                        else {
                            linenum = Convert.ToInt32(b.Rows.Count.ToString()) + 1; 
                        }
                            

                    }
                    catch
                    {
                        linenum = 1;
                    }
                    string strLine = linenum.ToString().PadLeft(5, '0');


                    Batch = dtRow["CustomerCode"].ToString() + DateTime.Now.ToString("yyyy") + '-' + strLine;
                }
                else
                {
                    Batch = dtRow["CBatch"].ToString();
                }
                Consigne = dtRow["CConsignee"].ToString();


                DataTable getusername = Gears.RetriveData2("select b.UserName as AddedBy,c.UserName as SubmittedBy,d.UserName as LastEditedBy from " +
                                        "wms.Inbound a " +
                                        "left join it.Users b " +
                                        "on a.AddedBy = b.UserID " +
                                        "left join it.Users c " +
                                        "on a.SubmittedBy = c.UserID " +
                                        "left join it.Users d " +
                                        "on a.LastEditedBy = d.UserID where DocNumber = '" + DocNumber + "'", Conn);
                foreach (DataRow dtRow2 in getusername.Rows)
                {
                    AddedBy = dtRow2["AddedBy"].ToString();
                    SubmittedBy = dtRow2["SubmittedBy"].ToString();
                    LastEditedBy = dtRow2["LastEditedBy"].ToString();
                }
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                ProdNumber = dtRow["ProdNumber"].ToString();
                StorageType = dtRow["StorageType"].ToString();
                IsService = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsService"]) ? false : dtRow["IsService"]);
                WeekNo = dtRow["WeekNo"].ToString();
                //TruckNo = dtRow["TruckNo"].ToString();

                //DataTable TruckMonetor = Gears.RetriveData2("select * from [PORTAL].IT.TruckMonitored where RefNO = '" + DocNumber + "'", Conn);
                //foreach (DataRow dtRow3 in TruckMonetor.Rows)
                //{
                //    DwellTime = dtRow3["DwellTime"].ToString();
                //    //InternalExternal = dtRow3["InternalExternal"].ToString();
                //    CancelledBy = dtRow3["CancelledBy"].ToString();
                //    CancelledDate = dtRow3["CancelledDate"].ToString();
                //}
            }

            return a;
        }
        public void InsertData(Inbound _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.TransType;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DataTable checkComi;
            DT1.Rows.Add("WMS.Inbound", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Inbound", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.Inbound", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Inbound", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Inbound", "0", "ICNNumber", _ent.ICNNumber);
            DT1.Rows.Add("WMS.Inbound", "0", "TranType", _ent.TranType);
            DT1.Rows.Add("WMS.Inbound", "0", "Plant", _ent.Plant);
            DT1.Rows.Add("WMS.Inbound", "0", "DRNumber", _ent.DRNumber);
            DT1.Rows.Add("WMS.Inbound", "0", "ContainerTemp", _ent.ContainerTemp);
            DT1.Rows.Add("WMS.Inbound", "0", "Driver", _ent.Driver);
            DT1.Rows.Add("WMS.Inbound", "0", "ContainerNo", _ent.ContainNumber);
            DT1.Rows.Add("WMS.Inbound", "0", "DeliveryDate", _ent.Delivery);
            DT1.Rows.Add("WMS.Inbound", "0", "ContactingDept", _ent.ContactingDept);
            DT1.Rows.Add("WMS.Inbound", "0", "InvoiceNo", _ent.InvoiceNo);
            DT1.Rows.Add("WMS.Inbound", "0", "PlateNo", _ent.PlateNo);
            DT1.Rows.Add("WMS.Inbound", "0", "SealNo", _ent.SealNo);
            DT1.Rows.Add("WMS.Inbound", "0", "Supplier", _ent.Supplier);
            DT1.Rows.Add("WMS.Inbound", "0", "AWB", _ent.AWB);
            //DT1.Rows.Add("WMS.Inbound", "0", "Trucker", _ent.Trucker);
            DT1.Rows.Add("WMS.Inbound", "0", "DocumentationStaff", _ent.DocumentationStaff);
            //DT1.Rows.Add("WMS.Inbound", "0", "WarehouseChecker", _ent.WarehouseChecker);
            DT1.Rows.Add("WMS.Inbound", "0", "GuardOnDuty", _ent.GuardOnDuty);
            DT1.Rows.Add("WMS.Inbound", "0", "CustomerRepresentative", _ent.CustomerRepresentative);
            DT1.Rows.Add("WMS.Inbound", "0", "ApprovingOfficer", _ent.ApprovingOfficer);
            //DT1.Rows.Add("WMS.Inbound", "0", "Arrival", _ent.Arrival);
            //DT1.Rows.Add("WMS.Inbound", "0", "Departure", _ent.Departure);
            //DT1.Rows.Add("WMS.Inbound", "0", "StartUnload", _ent.StartUnload);
            //DT1.Rows.Add("WMS.Inbound", "0", "CompleteUnload", _ent.CompleteUnload);
            DT1.Rows.Add("WMS.Inbound", "0", "IsNoCharge", _ent.IsNoCharge);
            DT1.Rows.Add("WMS.Inbound", "0", "InternalExternal", _ent.InternalExternal);
            DT1.Rows.Add("WMS.Inbound", "0", "Packing", _ent.Packing);
            DT1.Rows.Add("WMS.Inbound", "0", "AssignLoc", _ent.AssignLoc);
            DT1.Rows.Add("WMS.Inbound", "0", "ICNTotalQty", _ent.ICNTotalQty);
            DT1.Rows.Add("WMS.Inbound", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Inbound", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Inbound", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Inbound", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Inbound", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Inbound", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Inbound", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Inbound", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Inbound", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.Inbound", "0", "IsWithDetail", "False");

            //add Comi if update clicked
            checkComi = Gears.RetriveData2("select * from wms.[comi] where DocNumber = '" + _ent.DocNumber + "'", Conn);
            if (checkComi.Rows.Count > 0)
            {
                DT3.Rows.Add("WMS.COMI", "cond", "DocNumber", _ent.DocNumber);
                DT3.Rows.Add("WMS.COMI", "set", "Batch", _ent.Batch);
                DT3.Rows.Add("WMS.COMI", "set", "Consignee", _ent.Consigne);
                DT3.Rows.Add("WMS.COMI", "set", "LastEditedBy", _ent.LastEditedBy);
                DT3.Rows.Add("WMS.COMI", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Gears.UpdateData(DT3, Conn);
            }
            else
            {
                DT3.Rows.Add("WMS.COMI", "0", "DocNumber", _ent.DocNumber);
                DT3.Rows.Add("WMS.COMI", "0", "Batch", _ent.Batch);
                DT3.Rows.Add("WMS.COMI", "0", "Consignee", _ent.Consigne);
                DT3.Rows.Add("WMS.COMI", "0", "AddedBy", _ent.AddedBy);
                DT3.Rows.Add("WMS.COMI", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Gears.CreateData(DT3, Conn);
            }

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(Inbound _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.TransType;
            ddate = _ent.DocDate;
            RecLoc = _ent.AssignLoc;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DataTable checkComi;

            DT1.Rows.Add("WMS.Inbound", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Inbound", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.Inbound", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Inbound", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Inbound", "set", "ICNNumber", _ent.ICNNumber);
            DT1.Rows.Add("WMS.Inbound", "set", "TranType", _ent.TranType);
            DT1.Rows.Add("WMS.Inbound", "set", "DRNumber", _ent.DRNumber);
            DT1.Rows.Add("WMS.Inbound", "set", "Plant", _ent.Plant);
            DT1.Rows.Add("WMS.Inbound", "set", "DeliveryDate", _ent.Delivery);
            DT1.Rows.Add("WMS.Inbound", "set", "ContainerTemp", _ent.ContainerTemp);
            DT1.Rows.Add("WMS.Inbound", "set", "Driver", _ent.Driver);
            DT1.Rows.Add("WMS.Inbound", "set", "ContainerNo", _ent.ContainNumber);
            DT1.Rows.Add("WMS.Inbound", "set", "ContactingDept", _ent.ContactingDept);
            DT1.Rows.Add("WMS.Inbound", "set", "InvoiceNo", _ent.InvoiceNo);
            DT1.Rows.Add("WMS.Inbound", "set", "PlateNo", _ent.PlateNo);
            DT1.Rows.Add("WMS.Inbound", "set", "SealNo", _ent.SealNo);
            DT1.Rows.Add("WMS.Inbound", "set", "Supplier", _ent.Supplier);
            DT1.Rows.Add("WMS.Inbound", "set", "AWB", _ent.AWB);
            //DT1.Rows.Add("WMS.Inbound", "set", "Trucker", _ent.Trucker);
            DT1.Rows.Add("WMS.Inbound", "set", "DocumentationStaff", _ent.DocumentationStaff);
            //DT1.Rows.Add("WMS.Inbound", "set", "WarehouseChecker", _ent.WarehouseChecker);
            DT1.Rows.Add("WMS.Inbound", "set", "GuardOnDuty", _ent.GuardOnDuty);
            DT1.Rows.Add("WMS.Inbound", "set", "CustomerRepresentative", _ent.CustomerRepresentative);
            DT1.Rows.Add("WMS.Inbound", "set", "ApprovingOfficer", _ent.ApprovingOfficer);
            //DT1.Rows.Add("WMS.Inbound", "set", "Arrival", _ent.Arrival);
            //DT1.Rows.Add("WMS.Inbound", "set", "Departure", _ent.Departure);
            DT1.Rows.Add("WMS.Inbound", "set", "StartUnload", _ent.StartUnload);
            DT1.Rows.Add("WMS.Inbound", "set", "CompleteUnload", _ent.CompleteUnload);
            DT1.Rows.Add("WMS.Inbound", "set", "IsNoCharge", _ent.IsNoCharge);
            DT1.Rows.Add("WMS.Inbound", "set", "InternalExternal", _ent.InternalExternal);
            DT1.Rows.Add("WMS.Inbound", "set", "Packing", _ent.Packing);
            DT1.Rows.Add("WMS.Inbound", "set", "AssignLoc", _ent.AssignLoc);
            DT1.Rows.Add("WMS.Inbound", "set", "ICNTotalQty", _ent.ICNTotalQty);
            DT1.Rows.Add("WMS.Inbound", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Inbound", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Inbound", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Inbound", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Inbound", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Inbound", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Inbound", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Inbound", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Inbound", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.Inbound", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.Inbound", "set", "LastEditedDate", _ent.LastEditedDate);
            DT1.Rows.Add("WMS.Inbound", "set", "ProdNumber", _ent.ProdNumber);
            DT1.Rows.Add("WMS.Inbound", "set", "StorageType", _ent.StorageType);
            DT1.Rows.Add("WMS.Inbound", "set", "IsService", _ent.IsService);
            DT1.Rows.Add("WMS.Inbound", "set", "WeekNo", _ent.WeekNo);
            //DT1.Rows.Add("WMS.Inbound", "set", "TruckNo", _ent.TruckNo);
            string strErr = Gears.UpdateData(DT1, _ent.Connection);


            DT2.Rows.Add("WMS.icn", "cond", "DocNumber", _ent.DocNumber);
            DT2.Rows.Add("WMS.icn", "set", "TruckType", _ent.TruckT);
            DT2.Rows.Add("WMS.icn", "set", "Consignee", _ent.Consignee);
            DT2.Rows.Add("WMS.icn", "set", "ConsigneeAddress", _ent.ConAddress);
            DT2.Rows.Add("WMS.icn", "set", "TrackingNO", _ent.TrackingNo);
            DT2.Rows.Add("WMS.icn", "set", "TruckProviderByMets", _ent.TruckProv);
            Gears.UpdateData(DT2, _ent.Connection);

            //add Comi if update clicked
            checkComi = Gears.RetriveData2("select * from wms.[comi] where DocNumber = '" + _ent.DocNumber + "'", Conn);
            if (checkComi.Rows.Count > 0)
            {
                DT3.Rows.Add("WMS.COMI", "cond", "DocNumber", _ent.DocNumber);
                DT3.Rows.Add("WMS.COMI", "set", "Batch", _ent.Batch);
                DT3.Rows.Add("WMS.COMI", "set", "Consignee", _ent.Consigne);
                DT3.Rows.Add("WMS.COMI", "set", "LastEditedBy", _ent.LastEditedBy);
                DT3.Rows.Add("WMS.COMI", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Gears.UpdateData(DT3, Conn);
            }
            else {
                DT3.Rows.Add("WMS.COMI", "0", "DocNumber", _ent.DocNumber);
                DT3.Rows.Add("WMS.COMI", "0", "Batch", _ent.Batch);
                DT3.Rows.Add("WMS.COMI", "0", "Consignee", _ent.Consigne);
                DT3.Rows.Add("WMS.COMI", "0", "AddedBy", _ent.AddedBy);
                DT3.Rows.Add("WMS.COMI", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Gears.CreateData(DT3, Conn);
            }
            //DataTable getinbounddet = Gears.RetriveData2("select DocNumber from wms.inbounddetail where docnumber = '" + _ent.DocNumber + "'"
            //    ,_ent.Connection);
            //if (getinbounddet.Rows.Count > 0)
            //{
            //    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            //    DT2.Rows.Add("WMS.inbounddetail", "cond", "DocNumber", _ent.DocNumber);
            //    if (!string.IsNullOrEmpty(_ent.AssignLoc))
            //    {
            //        DT2.Rows.Add("WMS.inbounddetail", "set", "ToLocation", _ent.AssignLoc);
            //    }

            //    Gears.UpdateData(DT2,_ent.Connection);
            //}

            //DataTable getcountsheet = Gears.RetriveData2("select TransDoc from wms.countsheetsetup where transdoc = '" + _ent.DocNumber + "'"
            //    ,_ent.Connection);
            //if (getcountsheet.Rows.Count > 0)
            //{
            //    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            //    DT2.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", _ent.DocNumber);
            //    if (!string.IsNullOrEmpty(_ent.AssignLoc))
            //    {
            //        DT2.Rows.Add("WMS.CountSheetSetUp", "set", "Location", _ent.AssignLoc);
            //    }
            //    Gears.UpdateData(DT2,_ent.Connection);
            //}

            Functions.AuditTrail("WMSINB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Inbound _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DataTable checkComi;
            DT1.Rows.Add("WMS.Inbound", "cond", "DocNumber", _ent.DocNumber);
     
            checkComi = Gears.RetriveData2("select * from wms.[comi] where DocNumber = '" + _ent.DocNumber + "'", Conn);
            if(checkComi.Rows.Count > 0){
                DT2.Rows.Add("WMS.COMI", "cond", "DocNumber", _ent.DocNumber);
                Gears.DeleteData(DT2, _ent.Connection);
            }

            Gears.DeleteData(DT1, _ent.Connection);
           
            Functions.AuditTrail("WMSINB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
