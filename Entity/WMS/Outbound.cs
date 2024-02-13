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
    //-- Description:	SetBox, NetWeight, NetVolume, SMDeptSub, ModeofPayment, ModeofShipment, Brand, TotalAmount, DeclaredValue, TotalQty, ForwarderTR (HEADER)
    //-- =============================================
    #endregion

    public class Outbound
    {

        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string trans;

        private static string ddate;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Customer { get; set; }
        public virtual string TargetDate { get; set; }
        public virtual string Startload { get; set; }
        public virtual string Completeload { get; set; }
        public virtual string ContainerNumber { get; set; }
        public virtual string SealNumber { get; set; }
        public virtual string OtherReference { get; set; }
        public virtual bool IsNoCharge { get; set; }
        public virtual string DeliverTo { get; set; }
        public virtual string DeliveryAddress { get; set; }
        public virtual string TruckingCo { get; set; }
        public virtual string PlateNumber { get; set; }
        public virtual string Driver { get; set; }
        public virtual string WarehouseChecker { get; set; }
        public virtual string DocumentStaff { get; set; }

        public virtual decimal SetBox { get; set; }
        public virtual decimal NetWeight { get; set; }
        public virtual decimal NetVolume { get; set; }
        public virtual string SMDeptSub { get; set; }
        public virtual string ModeofPayment { get; set; }
        public virtual string ModeofShipment { get; set; }
        public virtual string Brand { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal DeclaredValue { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual string ForwarderTR { get; set; }
        public virtual string AllocationDate { get; set; }
        public virtual string StorageType { get; set; }


        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string Transtype { get; set; }


        public virtual string Consignee { get; set; }
        //public virtual string ConsigneeAddr { get; set; }
        public virtual string Overtime { get; set; }
        public virtual string SuppliedBy { get; set; }
        public virtual string TruckingPro { get; set; }
        public virtual string NOmanpower { get; set; }
        public virtual string AddtionalManpower { get; set; }
        public virtual string CompanyDept { get; set; }
        public virtual string ShipmentType { get; set; }
        public virtual string RefDoc { get; set; }
        public virtual string TruckType { get; set; }
        //public virtual string TrackingNO { get; set; }  
        //Truck Transaction
        public virtual string ArrivalTime { get; set; }
        public virtual string Startloading { get; set; }
        public virtual string Completeloading { get; set; }
        public virtual string DepartureTime { get; set; }
        public virtual string HoldReason { get; set; }
        public virtual string HoldRemarks { get; set; }
        public virtual string HoldDate { get; set; }
        public virtual string UnHoldDate { get; set; }
        public virtual string HoldDuration { get; set; }
        public virtual string DwellTime { get; set; }
        public virtual string CheckingEnd { get; set; }
        public virtual string EndProcessing { get; set; }
        public virtual string StartProcessing { get; set; }
        public virtual string CheckingStart { get; set; }
        public virtual string DockingTime { get; set; }
        public virtual string Departure { get; set; }
        public virtual string Status { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string HoldStatus { get; set; }
        public virtual string DockingDoor { get; set; }

        public virtual IList<OutboundDetail> Detail { get; set; }


        public class OutboundDetail
        {
            public virtual Outbound Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PicklistNo { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal PicklistQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BarcodeNo { get; set; }
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
                    a = Gears.RetriveData2("select a.*,b.FullDesc from WMS.OutboundDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddOutboundDetail(OutboundDetail OutboundDetail)
            {
                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.OutboundDetail where docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("WMS.OutboundDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "PicklistNo", OutboundDetail.PicklistNo);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "ItemCode", OutboundDetail.ItemCode);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "ColorCode", OutboundDetail.ColorCode);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "ClassCode", OutboundDetail.ClassCode);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "SizeCode", OutboundDetail.SizeCode);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "BulkQty", OutboundDetail.BulkQty);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "BulkUnit", OutboundDetail.BulkUnit);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "PicklistQty", OutboundDetail.PicklistQty);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Unit", OutboundDetail.Unit);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "BaseQty", OutboundDetail.BaseQty);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "StatusCode", OutboundDetail.StatusCode);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "BarcodeNo", OutboundDetail.BarcodeNo);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field1", OutboundDetail.Field1);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field2", OutboundDetail.Field2);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field3", OutboundDetail.Field3);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field4", OutboundDetail.Field4);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field5", OutboundDetail.Field5);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field6", OutboundDetail.Field6);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field7", OutboundDetail.Field7);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field8", OutboundDetail.Field8);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "Field9", OutboundDetail.Field9);
                DT1.Rows.Add("WMS.OutboundDetail", "0", "PickLineNumber", OutboundDetail.LineNumber);
                DT2.Rows.Add("WMS.Outbound", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.Outbound", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);


                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //DataTable CS = Gears.RetriveData2("Select IsByBulk from masterfile.item where itemcode = '" + OutboundDetail.ItemCode + "'");
                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if(isbybulk == true){

                //    for (int i = 1; i <= OutboundDetail.BulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');

                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", OutboundDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", OutboundDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", OutboundDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", OutboundDetail.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //        Gears.CreateData(DT4);
                //        DT4.Rows.Clear();
                //    }
                //}
                //else
                //{
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", OutboundDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", OutboundDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", OutboundDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", OutboundDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", "GETDATE()");

                //    Gears.CreateData(DT4);
                //}



            }
            public void UpdateOutboundDetail(OutboundDetail OutboundDetail)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("Select BulkQty from wms.outbounddetail where docnumber = '" + OutboundDetail.DocNumber + "' " +
                //"and LineNumber = '" + OutboundDetail.LineNumber + "'");
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != OutboundDetail.BulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "DocNumber", OutboundDetail.DocNumber);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", OutboundDetail.LineNumber);
                //        Gears.DeleteData(DT3);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("Select IsByBulk from masterfile.item where itemcode = '" + OutboundDetail.ItemCode + "'");
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= OutboundDetail.BulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');

                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", OutboundDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", OutboundDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", OutboundDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", OutboundDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", OutboundDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //                Gears.CreateData(DT4);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", OutboundDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", OutboundDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", OutboundDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", OutboundDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", OutboundDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", OutboundDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", "GETDATE()");

                //            Gears.CreateData(DT4);
                //        }
                //    }
                //}

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.OutboundDetail", "cond", "DocNumber", OutboundDetail.DocNumber);
                DT1.Rows.Add("WMS.OutboundDetail", "cond", "LineNumber", OutboundDetail.LineNumber);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "PicklistNo", OutboundDetail.PicklistNo);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "ItemCode", OutboundDetail.ItemCode);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "ColorCode", OutboundDetail.ColorCode);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "ClassCode", OutboundDetail.ClassCode);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "SizeCode", OutboundDetail.SizeCode);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "BulkQty", OutboundDetail.BulkQty);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "BulkUnit", OutboundDetail.BulkUnit);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "PicklistQty", OutboundDetail.PicklistQty);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Unit", OutboundDetail.Unit);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "BaseQty", OutboundDetail.BaseQty);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "StatusCode", OutboundDetail.StatusCode);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "BarcodeNo", OutboundDetail.BarcodeNo);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field1", OutboundDetail.Field1);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field2", OutboundDetail.Field2);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field3", OutboundDetail.Field3);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field4", OutboundDetail.Field4);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field5", OutboundDetail.Field5);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field6", OutboundDetail.Field6);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field7", OutboundDetail.Field7);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field8", OutboundDetail.Field8);
                DT1.Rows.Add("WMS.OutboundDetail", "set", "Field9", OutboundDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteOutboundDetail(OutboundDetail OutboundDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.OutboundDetail", "cond", "DocNumber", OutboundDetail.DocNumber);
                DT1.Rows.Add("WMS.OutboundDetail", "cond", "LineNumber", OutboundDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "DocNumber", OutboundDetail.DocNumber);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", OutboundDetail.LineNumber);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);

                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("select * from WMS.OutboundDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.Outbound", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.Outbound", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select A.*  ,EU.FullName as  LastEditedBy1, SB.FullName as  SubmittedBy1,AU.FullName as AddedBy1 from WMS.Outbound A LEFT JOIN IT.Users AU    ON A.AddedBy = AU.UserID    LEFT JOIN IT.Users EU    ON A.LastEditedBy = EU.UserID    LEFT JOIN IT.Users SB    ON A.SubmittedBy = SB.UserID  where DocNumber = '" + DocNumber + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                Customer = dtRow["Customer"].ToString();
                TargetDate = dtRow["TargetDate"].ToString();
                Startload = dtRow["StartLoading"].ToString();
                Completeload = dtRow["CompleteLoading"].ToString();
                ContainerNumber = dtRow["ContainerNumber"].ToString();
                SealNumber = dtRow["SealNumber"].ToString();
                OtherReference = dtRow["OtherReference"].ToString();
                WarehouseChecker = dtRow["WarehouseChecker"].ToString();
                DocumentStaff = dtRow["DocumentStaff"].ToString();

                SetBox = Convert.ToDecimal(Convert.IsDBNull(dtRow["SetBox"]) ? 0 : dtRow["SetBox"]);
                NetWeight = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetWeight"]) ? 0 : dtRow["NetWeight"]);
                NetVolume = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetVolume"]) ? 0 : dtRow["NetVolume"]);
                SMDeptSub = dtRow["SMDeptSub"].ToString();
                ModeofPayment = dtRow["ModeofPayment"].ToString();
                ModeofShipment = dtRow["ModeofShipment"].ToString();
                Brand = dtRow["Brand"].ToString();
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                DeclaredValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["DeclaredValue"]) ? 0 : dtRow["DeclaredValue"]);
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                ForwarderTR = dtRow["ForwarderTR"].ToString();

                IsNoCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsNoCharge"]) ? false : dtRow["IsNoCharge"]);
                DeliverTo = dtRow["DeliverTo"].ToString();
                DeliveryAddress = dtRow["DeliveryAddress"].ToString();
                TruckingCo = dtRow["TrackingNO"].ToString();
                PlateNumber = dtRow["PlateNumber"].ToString();
                AllocationDate = dtRow["AllocationDate"].ToString();
                StorageType = dtRow["StorageType"].ToString();
                Driver = dtRow["Driver"].ToString();
                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();

                CompanyDept = dtRow["CompanyDept"].ToString();
                Consignee = dtRow["Consignee"].ToString();
                //ConsigneeAddr = dtRow["ConsigneeAddress"].ToString();
                Overtime = dtRow["Overtime"].ToString();
                AddtionalManpower = dtRow["AddtionalManpower"].ToString();
                SuppliedBy = dtRow["SuppliedBy"].ToString();
                NOmanpower = dtRow["NOManpower"].ToString();
                TruckingPro = dtRow["TruckProviderByMets"].ToString();
                TruckType = dtRow["TruckType"].ToString();
                RefDoc = dtRow["RefDoc"].ToString();
                ShipmentType = dtRow["ShipmentType"].ToString();
                //TrackingNO = dtRow["TrackingNO"].ToString();

                AddedBy = dtRow["AddedBy1"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy1"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy1"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                DockingDoor = dtRow["LoadingBay"].ToString();

                ArrivalTime = dtRow["ArrivalTime"].ToString();
                Startloading = dtRow["Startloading"].ToString();
                Completeloading = dtRow["Completeloading"].ToString();
                DepartureTime = dtRow["DepartureTime"].ToString();
                HoldReason = dtRow["HoldReason"].ToString();
                HoldRemarks = dtRow["HoldRemarks"].ToString();
                HoldDate = dtRow["HoldDate"].ToString();
                UnHoldDate = dtRow["UnHoldDate"].ToString();
                HoldDuration = dtRow["HoldDuration"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                Status = dtRow["Status"].ToString();
                CheckingEnd = dtRow["CheckingEnd"].ToString();
                EndProcessing = dtRow["EndProcessing"].ToString();
                StartProcessing = dtRow["StartProcessing"].ToString();
                CheckingStart = dtRow["CheckingStart"].ToString();
                DockingTime = dtRow["DockingTime"].ToString();
                HoldStatus = dtRow["HoldStatus"].ToString();
                DwellTime = dtRow["DwellTime"].ToString();

                //DataTable TruckMonetor = Gears.RetriveData2("select * from [PORTAL].IT.TruckMonitored where RefNO = '" + DocNumber + "'", Conn);
                //foreach (DataRow dtRow3 in TruckMonetor.Rows)
                //{
                //    DwellTime = dtRow3["DwellTime"].ToString();
                //    CancelledBy = dtRow3["CancelledBy"].ToString();
                //    CancelledDate = dtRow3["CancelledDate"].ToString();
                //}
            }
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as WarehouseCode,'' as Customer" +
            //   ",'' as TargetDate,0 as IsNoCharge,'' as DeliverTo,'' as DeliveryAddress,'' as TruckingCo,"+
            //   "'' as PlateNumber,'' as Driver,'' as Field1" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(Outbound _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.Outbound", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Outbound", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Outbound", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Outbound", "0", "Customer", _ent.Customer);
            DT1.Rows.Add("WMS.Outbound", "0", "StartLoading", _ent.Startload);
            DT1.Rows.Add("WMS.Outbound", "0", "CompleteLoading", _ent.Completeload);
            DT1.Rows.Add("WMS.Outbound", "0", "ContainerNumber", _ent.ContainerNumber);
            DT1.Rows.Add("WMS.Outbound", "0", "SealNumber", _ent.SealNumber);
            DT1.Rows.Add("WMS.Outbound", "0", "OtherReference", _ent.OtherReference);
            DT1.Rows.Add("WMS.Outbound", "0", "WarehouseChecker", _ent.WarehouseChecker);
            DT1.Rows.Add("WMS.Outbound", "0", "DocumentStaff", _ent.DocumentStaff);
            DT1.Rows.Add("WMS.Outbound", "0", "TargetDate", _ent.TargetDate);
            DT1.Rows.Add("WMS.Outbound", "0", "IsNoCharge", _ent.IsNoCharge);
            DT1.Rows.Add("WMS.Outbound", "0", "DeliverTo", _ent.DeliverTo);
            DT1.Rows.Add("WMS.Outbound", "0", "DeliveryAddress", _ent.DeliveryAddress);
            DT1.Rows.Add("WMS.Outbound", "0", "TruckingCo", _ent.TruckingCo);
            DT1.Rows.Add("WMS.Outbound", "0", "PlateNumber", _ent.PlateNumber);
            DT1.Rows.Add("WMS.Outbound", "0", "Driver", _ent.Driver);

            DT1.Rows.Add("WMS.Outbound", "0", "SetBox", _ent.SetBox);
            DT1.Rows.Add("WMS.Outbound", "0", "NetWeight", _ent.NetWeight);
            DT1.Rows.Add("WMS.Outbound", "0", "NetVolume", _ent.NetVolume);
            DT1.Rows.Add("WMS.Outbound", "0", "SMDeptSub", _ent.SMDeptSub);
            DT1.Rows.Add("WMS.Outbound", "0", "ModeofPayment", _ent.ModeofPayment);
            DT1.Rows.Add("WMS.Outbound", "0", "ModeofShipment", _ent.ModeofShipment);
            DT1.Rows.Add("WMS.Outbound", "0", "Brand", _ent.Brand);
            DT1.Rows.Add("WMS.Outbound", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.Outbound", "0", "DeclaredValue", _ent.DeclaredValue);
            DT1.Rows.Add("WMS.Outbound", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("WMS.Outbound", "0", "ForwarderTR", _ent.ForwarderTR);

            DT1.Rows.Add("WMS.Outbound", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Outbound", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Outbound", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Outbound", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Outbound", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Outbound", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Outbound", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Outbound", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Outbound", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.Outbound", "0", "IsWithDetail", "False");

            DT1.Rows.Add("WMS.Outbound", "0", "CompanyDept", _ent.CompanyDept);
            DT1.Rows.Add("WMS.Outbound", "0", "Consignee", _ent.Consignee);
            //DT1.Rows.Add("WMS.Outbound", "0", "ConsigneeAddress", _ent.ConsigneeAddr);
            DT1.Rows.Add("WMS.Outbound", "0", "Overtime", _ent.Overtime);
            DT1.Rows.Add("WMS.Outbound", "0", "AddtionalManpower", _ent.AddtionalManpower);
            DT1.Rows.Add("WMS.Outbound", "0", "SuppliedBy", _ent.SuppliedBy);
            DT1.Rows.Add("WMS.Outbound", "0", "NOManpower", _ent.NOmanpower);
            DT1.Rows.Add("WMS.Outbound", "0", "TruckProviderByMets", _ent.TruckingPro);
            DT1.Rows.Add("WMS.Outbound", "0", "TruckType", _ent.TruckType);
            DT1.Rows.Add("WMS.Outbound", "0", "RefDoc", _ent.RefDoc);
            DT1.Rows.Add("WMS.Outbound", "0", "ShipmentType", _ent.ShipmentType);
            //DT1.Rows.Add("WMS.Outbound", "0", "TrackingNO", _ent.TrackingNO);

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(Outbound _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transtype;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Outbound", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Outbound", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Outbound", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Outbound", "set", "Customer", _ent.Customer);
            DT1.Rows.Add("WMS.Outbound", "set", "StartLoading", _ent.Startload);
            DT1.Rows.Add("WMS.Outbound", "set", "CompleteLoading", _ent.Completeload);
            DT1.Rows.Add("WMS.Outbound", "set", "ContainerNumber", _ent.ContainerNumber);
            DT1.Rows.Add("WMS.Outbound", "set", "SealNumber", _ent.SealNumber);
            DT1.Rows.Add("WMS.Outbound", "set", "OtherReference", _ent.OtherReference);
            DT1.Rows.Add("WMS.Outbound", "set", "WarehouseChecker", _ent.WarehouseChecker);
            DT1.Rows.Add("WMS.Outbound", "set", "DocumentStaff", _ent.DocumentStaff);
            DT1.Rows.Add("WMS.Outbound", "set", "TargetDate", _ent.TargetDate);
            DT1.Rows.Add("WMS.Outbound", "set", "IsNoCharge", _ent.IsNoCharge);
            DT1.Rows.Add("WMS.Outbound", "set", "DeliverTo", _ent.DeliverTo);
            DT1.Rows.Add("WMS.Outbound", "set", "DeliveryAddress", _ent.DeliveryAddress);
            DT1.Rows.Add("WMS.Outbound", "set", "TruckingCo", _ent.TruckingCo);
            DT1.Rows.Add("WMS.Outbound", "set", "PlateNumber", _ent.PlateNumber);
            DT1.Rows.Add("WMS.Outbound", "set", "Driver", _ent.Driver);

            DT1.Rows.Add("WMS.Outbound", "set", "SetBox", _ent.SetBox);
            DT1.Rows.Add("WMS.Outbound", "set", "NetWeight", _ent.NetWeight);
            DT1.Rows.Add("WMS.Outbound", "set", "NetVolume", _ent.NetVolume);
            DT1.Rows.Add("WMS.Outbound", "set", "SMDeptSub", _ent.SMDeptSub);
            DT1.Rows.Add("WMS.Outbound", "set", "ModeofPayment", _ent.ModeofPayment);
            DT1.Rows.Add("WMS.Outbound", "set", "ModeofShipment", _ent.ModeofShipment);
            DT1.Rows.Add("WMS.Outbound", "set", "Brand", _ent.Brand);
            DT1.Rows.Add("WMS.Outbound", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.Outbound", "set", "DeclaredValue", _ent.DeclaredValue);
            DT1.Rows.Add("WMS.Outbound", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("WMS.Outbound", "set", "ForwarderTR", _ent.ForwarderTR);
            DT1.Rows.Add("WMS.Outbound", "set", "AllocationDate", _ent.AllocationDate);
            DT1.Rows.Add("WMS.Outbound", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Outbound", "set", "StorageType", _ent.StorageType);

            DT1.Rows.Add("WMS.Outbound", "set", "CompanyDept", _ent.CompanyDept);
            DT1.Rows.Add("WMS.Outbound", "set", "Consignee", _ent.Consignee);
            DT1.Rows.Add("WMS.Outbound", "set", "Overtime", _ent.Overtime);
            DT1.Rows.Add("WMS.Outbound", "set", "AddtionalManpower", _ent.AddtionalManpower);
            //DT1.Rows.Add("WMS.Outbound", "set", "ConsigneeAddress", _ent.ConsigneeAddr);
            DT1.Rows.Add("WMS.Outbound", "set", "SuppliedBy", _ent.SuppliedBy);
            DT1.Rows.Add("WMS.Outbound", "set", "NOManpower", _ent.NOmanpower);
            DT1.Rows.Add("WMS.Outbound", "set", "TruckType", _ent.TruckType);
            DT1.Rows.Add("WMS.Outbound", "set", "TruckProviderByMets", _ent.TruckingPro);
            DT1.Rows.Add("WMS.Outbound", "set", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("WMS.Outbound", "set", "RefDoc", _ent.RefDoc);
            //DT1.Rows.Add("WMS.Outbound", "set", "TrackingNO", _ent.TrackingNO);


            DT1.Rows.Add("WMS.Outbound", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Outbound", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Outbound", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Outbound", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Outbound", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Outbound", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Outbound", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Outbound", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.Outbound", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.Outbound", "set", "LastEditedDate", _ent.LastEditedDate);

            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSOUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void Deletedata(Outbound _ent)
        {

            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Outbound", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            //Gears.RetriveData2("UPDATE WMS.OCN set DRNumber='' where DRNumber= " + _ent.DocNumber + "'", _ent.Connection);
            Functions.AuditTrail("WMSOUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);

        }

        public void SubsiEntry(string RefNumber)
        {
            //DataTable subsi = Gears.RetriveData2("SELECT * FROM WMS.CountSheetSubsi WHERE TransDoc IN ('"+ RefNumber +"')");

            //if (subsi.Rows.Count == 0)
            //{
            Gears.RetriveData2("EXEC sp_OutboundSubsi '" + RefNumber + "'", Conn);
            //}

        }
    }
}
