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
    //-- Description:	Added Price(Detail)
    //-- =============================================
    #endregion

    public class PICKLIST
    {

        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string trans;

        private static string ddate;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Status { get; set; }
        public virtual string TransType { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string DeliveryDate { get; set; }
        public virtual string PicklistType { get; set; }
        public virtual string PicklistStatus { get; set; }

        public virtual string WarehouseCode { get; set; }
        public virtual string PlantCode { get; set; }
        public virtual string RoomCode { get; set; }
        public virtual string DeliverTo { get; set; }
        //public virtual string DeliverAddress { get; set; }
        public virtual string TruckingCo { get; set; }
        public virtual string PlateNo { get; set; }
        public virtual string DriverName { get; set; }

        public virtual string ReferenceNo { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string OutboundNo { get; set; }
        public virtual string StorageType { get; set; }
        public virtual bool IsAutoPick { get; set; }
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

        public virtual string Consignee { get; set; }
        public virtual string ConsigneeAddress { get; set; }
        public virtual string Overtime { get; set; }
        public virtual string AddtionalManpower { get; set; }
        public virtual string SuppliedBy { get; set; }

        public virtual string CompanyDept { get; set; }
        public virtual string ShipmentType { get; set; }
        public virtual string RefDoc { get; set; }
        public virtual string TruckType { get; set; }  

        public virtual string NOManpower { get; set; }
        public virtual string TruckProviderByMets { get; set; }
        public virtual string SealNo { get; set; }

        public virtual IList<PICKLISTDetail> Detail { get; set; }


        public class PICKLISTDetail
        {
            public virtual PICKLIST Parent { get; set; }
           
            public virtual string DocNumber { get; set; }
      
            public virtual string LineNumber { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string Customer { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Unit { get; set; }
         
            public virtual string Location { get; set; }
            public virtual string ToLocation { get; set; }
            public virtual string PalletID { get; set; }
            public virtual DateTime ExpiryDate { get; set; }
            public virtual DateTime Mkfgdate { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string LotID { get; set; }
            public virtual DateTime RRDocdate { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BarcodeNo { get; set; }

            public virtual decimal Price { get; set; }
            public virtual string SpecialHandlingInstruc { get; set; }
            public virtual string MLIRemarks01 { get; set; }
            public virtual string MLIRemarks02 { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual decimal PickedQty { get; set; }
            public virtual string Outlet { get; set; }
            public virtual string DropNo { get; set; }
            public virtual string Accountcode { get; set; }
            public virtual string DeliveryReport { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select a.*,b.FullDesc from WMS.PICKLISTDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPICKLISTDetail(PICKLISTDetail PICKLISTDetail)
            {
                int linenum = 0;
   

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.PICKLISTDetail where docnumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Customer", PICKLISTDetail.Customer);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "ItemCode", PICKLISTDetail.ItemCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "ColorCode", PICKLISTDetail.ColorCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "ClassCode", PICKLISTDetail.ClassCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "SizeCode", PICKLISTDetail.SizeCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "BulkQty", PICKLISTDetail.BulkQty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "BulkUnit", PICKLISTDetail.BulkUnit);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Qty", PICKLISTDetail.Qty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Unit", PICKLISTDetail.Unit);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Location", PICKLISTDetail.Location);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "ToLocation", PICKLISTDetail.ToLocation);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "PalletID", PICKLISTDetail.PalletID);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "ExpiryDate", PICKLISTDetail.ExpiryDate);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Mkfgdate", PICKLISTDetail.Mkfgdate);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "BatchNo", PICKLISTDetail.BatchNo);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "RRDocdate", PICKLISTDetail.RRDocdate);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "BaseQty", PICKLISTDetail.BaseQty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "StatusCode", PICKLISTDetail.StatusCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "BarcodeNo", PICKLISTDetail.BarcodeNo);

                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "SpecialHandlingInstruc", PICKLISTDetail.SpecialHandlingInstruc);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "MLIRemarks01", PICKLISTDetail.MLIRemarks01);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "MLIRemarks02", PICKLISTDetail.MLIRemarks02);

                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Price", PICKLISTDetail.Price);

                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field1", PICKLISTDetail.Field1);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field2", PICKLISTDetail.Field2);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field3", PICKLISTDetail.Field3);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field4", PICKLISTDetail.Field4);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field5", PICKLISTDetail.Field5);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field6", PICKLISTDetail.Field6);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field7", PICKLISTDetail.Field7);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field8", PICKLISTDetail.Field8);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Field9", PICKLISTDetail.Field9);

                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "PickedQty", PICKLISTDetail.PickedQty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Outlet", PICKLISTDetail.Outlet);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "DropNo", PICKLISTDetail.DropNo);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "Accountcode", PICKLISTDetail.Accountcode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "0", "DeliveryReport", PICKLISTDetail.DeliveryReport);
               
                DT2.Rows.Add("WMS.PICKLIST", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.PICKLIST", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);

                #region csheet
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                //DataTable CS = Gears.RetriveData2("Select IsByBulk,isnull(StandardQty,0) as StandardQty from masterfile.item where itemcode = '" + PICKLISTDetail.ItemCode + "'");
                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= PICKLISTDetail.BulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", trans);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", PICKLISTDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", PICKLISTDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", PICKLISTDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", PICKLISTDetail.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", PICKLISTDetail.ManufacturingDate);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", PICKLISTDetail.ExpiryDate);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "PalletID", PICKLISTDetail.PalletID);
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", "1");
                //        DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", ddate);
                //        Gears.CreateData(DT4);
                //        DT4.Rows.Clear();

                //        decimal dcStandard =Convert.ToDecimal(CS.Rows[0]["StandardQty"]) * Convert.ToDecimal(PICKLISTDetail.BulkQty);
                //        if (dcStandard != Convert.ToDecimal(PICKLISTDetail.ReceivedQty) && dcStandard !=0 )
                //        {
                //            if (i < PICKLISTDetail.BulkQty)
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
                //                decimal remainingstandard = PICKLISTDetail.ReceivedQty % Convert.ToDecimal(CS.Rows[0]["StandardQty"]);
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
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", PICKLISTDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", PICKLISTDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", PICKLISTDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", PICKLISTDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBaseQty", PICKLISTDetail.ReceivedQty);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", PICKLISTDetail.BulkQty);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", PICKLISTDetail.ManufacturingDate);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", PICKLISTDetail.ExpiryDate);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "PalletID", PICKLISTDetail.PalletID);
                //    DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", ddate);

                //    Gears.CreateData(DT4);
                //}
                #endregion
            }
            public void UpdatePICKLISTDetail(PICKLISTDetail PICKLISTDetail)
            {
                //bool isbybulk = false;

                #region Csheet
                //DataTable dtable = Gears.RetriveData2("Select BulkQty from wms.PICKLISTDetail where docnumber = '" + PICKLISTDetail.DocNumber + "' " +
                //"and LineNumber = '" + PICKLISTDetail.LineNumber + "'");
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != PICKLISTDetail.BulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", PICKLISTDetail.DocNumber);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PICKLISTDetail.LineNumber);
                //        Gears.DeleteData(DT3);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("Select IsByBulk from masterfile.item where itemcode = '" + PICKLISTDetail.ItemCode + "'");
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= PICKLISTDetail.BulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');

                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PICKLISTDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PICKLISTDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PICKLISTDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PICKLISTDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PICKLISTDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //                Gears.CreateData(DT4);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PICKLISTDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", PICKLISTDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PICKLISTDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PICKLISTDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PICKLISTDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PICKLISTDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", "GETDATE()");

                //            Gears.CreateData(DT4);
                //        }
                //    }
                //}


                //int linenum = 0;
                #endregion

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.PICKLISTDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.PICKLISTDetail", "cond", "LineNumber", PICKLISTDetail.LineNumber);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Customer", PICKLISTDetail.Customer);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "ItemCode", PICKLISTDetail.ItemCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "ColorCode", PICKLISTDetail.ColorCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "ClassCode", PICKLISTDetail.ClassCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "SizeCode", PICKLISTDetail.SizeCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "BulkQty", PICKLISTDetail.BulkQty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "BulkUnit", PICKLISTDetail.BulkUnit);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Qty", PICKLISTDetail.Qty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Unit", PICKLISTDetail.Unit);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Location", PICKLISTDetail.Location);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "ToLocation", PICKLISTDetail.ToLocation);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "PalletID", PICKLISTDetail.PalletID);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "ExpiryDate", PICKLISTDetail.ExpiryDate);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Mkfgdate", PICKLISTDetail.Mkfgdate);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "BatchNo", PICKLISTDetail.BatchNo);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "RRDocdate", PICKLISTDetail.RRDocdate);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "BaseQty", PICKLISTDetail.BaseQty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "StatusCode", PICKLISTDetail.StatusCode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "BarcodeNo", PICKLISTDetail.BarcodeNo);

                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Price", PICKLISTDetail.Price);

                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "SpecialHandlingInstruc", PICKLISTDetail.SpecialHandlingInstruc);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "MLIRemarks01", PICKLISTDetail.MLIRemarks01);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "MLIRemarks02", PICKLISTDetail.MLIRemarks02);

                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field1", PICKLISTDetail.Field1);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field2", PICKLISTDetail.Field2);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field3", PICKLISTDetail.Field3);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field4", PICKLISTDetail.Field4);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field5", PICKLISTDetail.Field5);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field6", PICKLISTDetail.Field6);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field7", PICKLISTDetail.Field7);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field8", PICKLISTDetail.Field8);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Field9", PICKLISTDetail.Field9);

                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "PickedQty", PICKLISTDetail.PickedQty);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Outlet", PICKLISTDetail.Outlet);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "DropNo", PICKLISTDetail.DropNo);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "Accountcode", PICKLISTDetail.Accountcode);
                DT1.Rows.Add("WMS.PICKLISTDetail", "set", "DeliveryReport", PICKLISTDetail.DeliveryReport);

                Gears.UpdateData(DT1,Conn);

    
            }
            public void DeletePICKLISTDetail(PICKLISTDetail PICKLISTDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.PICKLISTDetail", "cond", "DocNumber", PICKLISTDetail.DocNumber);
                DT1.Rows.Add("WMS.PICKLISTDetail", "cond", "LineNumber", PICKLISTDetail.LineNumber);


                Gears.DeleteData(DT1,Conn);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "DocNumber", PICKLISTDetail.DocNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PICKLISTDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);
                Gears.DeleteData(DT3,Conn);

                DataTable count = Gears.RetriveData2("select * from WMS.PICKLISTDetail where docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.PICKLISTDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.PICKLISTDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }

            }
        }
        public class OCNandPICKLISTDetail
        {
            public virtual PICKLIST Parent { get; set; }
            public virtual string OCNNumber { get; set; }

            public virtual string LineNumber { get; set; }
            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from WMS.OCNandPICKLISTDetail where DocNumber='" + DocNumber + "'",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddOCNandPICKLISTDetail(OCNandPICKLISTDetail OCNandPICKLISTDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("WMS.OCNandPICKLISTDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.OCNandPICKLISTDetail", "0", "OCNNumber", OCNandPICKLISTDetail.OCNNumber);

                Gears.CreateData(DT1,Conn);



            }
            public void UpdateOCNandPICKLISTDetail(OCNandPICKLISTDetail OCNandPICKLISTDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("WMS.OCNandPICKLISTDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.OCNandPICKLISTDetail", "set", "OCNNumber", OCNandPICKLISTDetail.OCNNumber);

                Gears.CreateData(DT1, Conn);



            }

            public void DeleteOCNandPICKLISTDetail(OCNandPICKLISTDetail OCNandPICKLISTDetail)
            {
             
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.OCNandPICKLISTDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.OCNandPICKLISTDetail", "cond", "OCNNumber", OCNandPICKLISTDetail.OCNNumber);
                Gears.DeleteData(DT1,Conn);
                
            }
        }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select A.*  ,EU.FullName as  LastEditedBy1, SB.FullName as  SubmittedBy1,AU.FullName as AddedBy1 from WMS.PICKLIST A LEFT JOIN IT.Users AU    ON A.AddedBy = AU.UserID    LEFT JOIN IT.Users EU    ON A.LastEditedBy = EU.UserID    LEFT JOIN IT.Users SB    ON A.SubmittedBy = SB.UserID  where DocNumber = '" + DocNumber + "'", Conn);


                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    DocDate = dtRow["Docdate"].ToString();
                    CustomerCode = dtRow["CustomerCode"].ToString();
                    DeliveryDate = dtRow["DeliveryDate"].ToString();
                    PicklistType = dtRow["PicklistType"].ToString();
                    PicklistStatus = dtRow["Status"].ToString();
                    StorageType = dtRow["StorageType"].ToString();
                 
                    WarehouseCode = dtRow["WarehouseCode"].ToString();
                    PlantCode = dtRow["PlantCode"].ToString();
                    RoomCode = dtRow["RoomCode"].ToString();
                    ReferenceNo = dtRow["ReferenceNo"].ToString();
                    Remarks = dtRow["Remarks"].ToString();
                    OutboundNo = dtRow["OutboundNo"].ToString();
                    DeliverTo = dtRow["DeliverTo"].ToString();
                    //DeliverAddress = dtRow["DeliverAddress"].ToString();
                    TruckingCo = dtRow["TruckingCo"].ToString();
                    PlateNo = dtRow["PlateNumber"].ToString();
                    DriverName = dtRow["Driver"].ToString();
                    IsAutoPick = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsAutoPick"]) ? false : dtRow["IsAutoPick"]);
                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();
                    AddedBy = dtRow["AddedBy1"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy1"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    SubmittedBy = dtRow["SubmittedBy1"].ToString();
                    SubmittedDate = dtRow["SubmittedDate"].ToString();


                    Consignee = dtRow["Consignee"].ToString();
                    ConsigneeAddress = dtRow["ConsigneeAddress"].ToString();
                    Overtime = dtRow["Overtime"].ToString();
                    AddtionalManpower = dtRow["AddtionalManpower"].ToString();
                    SuppliedBy = dtRow["SuppliedBy"].ToString();
                    NOManpower = dtRow["NOManpower"].ToString();
                    TruckProviderByMets = dtRow["TruckProviderByMets"].ToString();
                    SealNo = dtRow["SealNo"].ToString();
                    TruckType = dtRow["TruckType"].ToString();
                    RefDoc = dtRow["RefDoc"].ToString();
                    ShipmentType = dtRow["ShipmentType"].ToString();
                    CompanyDept = dtRow["CompanyDept"].ToString();






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
        public void InsertData(PICKLIST _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.PICKLIST", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.PICKLIST", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.PICKLIST", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.PICKLIST", "0", "DeliveryDate", _ent.DeliveryDate);
            DT1.Rows.Add("WMS.PICKLIST", "0", "PicklistType", _ent.PicklistType);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Status", _ent.PicklistStatus);
            DT1.Rows.Add("WMS.PICKLIST", "0", "StorageType", _ent.StorageType);
 
            DT1.Rows.Add("WMS.PICKLIST", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.PICKLIST", "0", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("WMS.PICKLIST", "0", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("WMS.PICKLIST", "0", "ReferenceNo", _ent.ReferenceNo);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.PICKLIST", "0", "DeliverTo", _ent.DeliverTo);
            //DT1.Rows.Add("WMS.PICKLIST", "0", "DeliverAddress", _ent.DeliverAddress);
            DT1.Rows.Add("WMS.PICKLIST", "0", "PlateNumber", _ent.PlateNo);
            DT1.Rows.Add("WMS.PICKLIST", "0", "TruckingCo", _ent.TruckingCo);
            DT1.Rows.Add("WMS.PICKLIST", "0", "PlateNumber", _ent.PlateNo);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Driver", _ent.DriverName);
            DT1.Rows.Add("WMS.PICKLIST", "0", "IsAutoPick", _ent.IsAutoPick);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.PICKLIST", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.PICKLIST", "0", "IsWithDetail", "False");
           
             DT1.Rows.Add("WMS.PICKLIST", "0", "Consignee", _ent.Consignee);
             DT1.Rows.Add("WMS.PICKLIST", "0", "ConsigneeAddress", _ent.ConsigneeAddress);
             DT1.Rows.Add("WMS.PICKLIST", "0", "Overtime", _ent.Overtime);
             DT1.Rows.Add("WMS.PICKLIST", "0", "AddtionalManpower", _ent.AddtionalManpower);
             DT1.Rows.Add("WMS.PICKLIST", "0", "SuppliedBy", _ent.SuppliedBy);
             DT1.Rows.Add("WMS.PICKLIST", "0", "NOManpower", _ent.NOManpower);
             DT1.Rows.Add("WMS.PICKLIST", "0", "TruckProviderByMets", _ent.TruckProviderByMets);
             DT1.Rows.Add("WMS.PICKLIST", "0", "SealNo", _ent.SealNo);
             DT1.Rows.Add("WMS.PICKLIST", "0", "CompanyDept", _ent.CompanyDept);
             DT1.Rows.Add("WMS.PICKLIST", "0", "TruckType", _ent.TruckType);
             DT1.Rows.Add("WMS.PICKLIST", "0", "RefDoc", _ent.RefDoc);
             DT1.Rows.Add("WMS.PICKLIST", "0", "ShipmentType", _ent.ShipmentType);

            
           
            Gears.CreateData(DT1,_ent.Connection);
           
        }

        public void UpdateData(PICKLIST _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            trans = _ent.TransType;
            ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.PICKLIST", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.PICKLIST", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.PICKLIST", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.PICKLIST", "set", "DeliveryDate", _ent.DeliveryDate);
            DT1.Rows.Add("WMS.PICKLIST", "set", "PicklistType", _ent.PicklistType);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Status", _ent.PicklistStatus);
            DT1.Rows.Add("WMS.PICKLIST", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.PICKLIST", "set", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("WMS.PICKLIST", "set", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("WMS.PICKLIST", "set", "ReferenceNo", _ent.ReferenceNo);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.PICKLIST", "set", "OutboundNo", _ent.OutboundNo);
            DT1.Rows.Add("WMS.PICKLIST", "set", "DeliverTo", _ent.DeliverTo);
            //DT1.Rows.Add("WMS.PICKLIST", "set", "DeliverAddress", _ent.DeliverAddress);
            DT1.Rows.Add("WMS.PICKLIST", "set", "TruckingCo", _ent.TruckingCo);
            DT1.Rows.Add("WMS.PICKLIST", "set", "PlateNumber", _ent.PlateNo);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Driver", _ent.DriverName);
            DT1.Rows.Add("WMS.PICKLIST", "set", "IsAutoPick", _ent.IsAutoPick);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.PICKLIST", "set", "StorageType", _ent.StorageType);

            DT1.Rows.Add("WMS.PICKLIST", "set", "Consignee", _ent.Consignee);
            DT1.Rows.Add("WMS.PICKLIST", "set", "ConsigneeAddress", _ent.ConsigneeAddress);
            DT1.Rows.Add("WMS.PICKLIST", "set", "Overtime", _ent.Overtime);
            DT1.Rows.Add("WMS.PICKLIST", "set", "AddtionalManpower", _ent.AddtionalManpower);
            DT1.Rows.Add("WMS.PICKLIST", "set", "SuppliedBy", _ent.SuppliedBy);
            DT1.Rows.Add("WMS.PICKLIST", "set", "NOManpower", _ent.NOManpower);
            DT1.Rows.Add("WMS.PICKLIST", "set", "TruckProviderByMets", _ent.TruckProviderByMets);
            DT1.Rows.Add("WMS.PICKLIST", "set", "SealNo", _ent.SealNo);
            DT1.Rows.Add("WMS.PICKLIST", "set", "CompanyDept", _ent.CompanyDept);
            DT1.Rows.Add("WMS.PICKLIST", "set", "TruckType", _ent.TruckType);
            DT1.Rows.Add("WMS.PICKLIST", "set", "RefDoc", _ent.RefDoc);
            DT1.Rows.Add("WMS.PICKLIST", "set", "ShipmentType", _ent.ShipmentType);
            



            DT1.Rows.Add("WMS.PICKLIST", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.PICKLIST", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
             Gears.UpdateData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSPICK", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }

        public void DeleteData(PICKLIST _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.PICKLIST", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSPICK", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
        public static DataTable dtGenerate(string Docnumber, string Conn)
        {

            DataTable dtgenerate = Gears.RetriveData2("select * from wms.ocndetail where Docnumber ='" + Docnumber + "'",Conn);
            return dtgenerate;
        }

    }
}
