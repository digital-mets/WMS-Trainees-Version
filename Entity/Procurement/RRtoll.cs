using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class RRtoll
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TargetDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string StockNumber { get; set; }
        public virtual string Status { get; set; }
        public virtual string Remarks { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual string PODocNumber { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string ForceClosedBy { get; set; }
        public virtual string ForceClosedDate { get; set; }
        public virtual string CostCenter { get; set; }

   


        //2021-06-21    EMC add fields
        public virtual Int32 Year { get; set; }
        public virtual Int32 WorkWeek { get; set; }
        public virtual Int32 DayNo { get; set; }


        //2021-06-16    EMC add fields
        public virtual string RequestType { get; set; }
        public virtual string RequestingDeptCompany { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual bool IsAllowPartialHeader { get; set; }

        public virtual string Consignee { get; set; }
        public virtual string ConsigneeAddress { get; set; }
        public virtual string RequiredLoadingTime { get; set; }
        public virtual string ShipmentType { get; set; }
        public virtual string OCNRequestNumber { get; set; }
        public virtual string TransDate { get; set; }
        public virtual string RequestRef { get; set; }
        public virtual string TargetDeliveryDate { get; set; }
        public virtual string Employee { get; set; }
        public virtual string Department { get; set; }

        public virtual IList<RRtollDetail> Detail { get; set; }

        public virtual IList<RRtollService> Detail4 { get; set; }

        public virtual IList<RRtollScrap> Detail3 { get; set; }

        public virtual IList<RRtollSpices> Detail2 { get; set; }

        public class RRtollDetail
        {
            public virtual RRtoll Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal RequestQty { get; set; }
            public virtual string UnitBase { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual Boolean IsAllowPartial { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual DateTime Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public virtual decimal RRQty { get; set; }
            public virtual string RefRawMatCode { get; set; }
            public virtual string WarehouseCode { get; set; }
            public virtual string RawMatType { get; set; }
            public virtual string UOMReq { get; set; }

            public virtual DateTime ExpDate { get; set; }
                


            //2021-06-16    EMC add fields
            public virtual string ItemDescription { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual string ProcessSteps { get; set; }
            public virtual string OCNNumber { get; set; }
            public virtual decimal Qty { get; set; }

            public virtual string UOM { get; set; }
            public virtual string SpecialHandlingInstruction { get; set; }
            public virtual string Remarks { get; set; }
            public virtual decimal TotalQty { get; set; }
            public virtual string ScrapCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual bool IsPrinted { get; set; }

            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,FullDesc from Procurement.RRtollDetail a left join masterfile.item b on a.ItemCode = b.ItemCode" +
                                            " where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRRtollDetail(RRtollDetail RRtollDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.RRtollDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "LineNumber", strLine);


                DT1.Rows.Add("Procurement.RRtollDetail", "0", "ItemCode", RRtollDetail.ItemCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "ColorCode", RRtollDetail.ColorCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "ClassCode", RRtollDetail.ClassCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "SizeCode", RRtollDetail.SizeCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "RequestQty", RRtollDetail.RequestQty);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "UnitBase", RRtollDetail.UnitBase);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "OrderQty", RRtollDetail.OrderQty);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "IsAllowPartial", RRtollDetail.IsAllowPartial);



                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field1", RRtollDetail.Field1);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field2", RRtollDetail.Field2);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field3", RRtollDetail.Field3);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field4", RRtollDetail.Field4);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field5", RRtollDetail.Field5);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field6", RRtollDetail.Field6);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field7", RRtollDetail.Field7);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field8", RRtollDetail.Field8);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "Field9", RRtollDetail.Field9);

                DT1.Rows.Add("Procurement.RRtollDetail", "0", "RRQty", RRtollDetail.RRQty);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "RefRawMatCode", RRtollDetail.RefRawMatCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "WarehouseCode", RRtollDetail.WarehouseCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "RawMatType", RRtollDetail.RawMatType);
                DT1.Rows.Add("Procurement.RRtollDetail", "0", "UOMReq", RRtollDetail.UOMReq);

                DT1.Rows.Add("Procurement.RRtollDetail", "0", "ExpDate", RRtollDetail.ExpDate);



            DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateRRtollDetail(RRtollDetail RRtollDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollDetail", "cond", "DocNumber", RRtollDetail.DocNumber);
                DT1.Rows.Add("Procurement.RRtollDetail", "cond", "LineNumber", RRtollDetail.LineNumber);

                DT1.Rows.Add("Procurement.RRtollDetail", "set", "ItemCode", RRtollDetail.ItemCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "ColorCode", RRtollDetail.ColorCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "ClassCode", RRtollDetail.ClassCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "SizeCode", RRtollDetail.SizeCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "RequestQty", RRtollDetail.RequestQty);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "UnitBase", RRtollDetail.UnitBase);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "OrderQty", RRtollDetail.OrderQty);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "IsAllowPartial", RRtollDetail.IsAllowPartial);


                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field1", RRtollDetail.Field1);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field2", RRtollDetail.Field2);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field3", RRtollDetail.Field3);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field4", RRtollDetail.Field4);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field5", RRtollDetail.Field5);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field6", RRtollDetail.Field6);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field7", RRtollDetail.Field7);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field8", RRtollDetail.Field8);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "Field9", RRtollDetail.Field9);


                DT1.Rows.Add("Procurement.RRtollDetail", "set", "RRQty", RRtollDetail.RRQty);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "RefRawMatCode", RRtollDetail.RefRawMatCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "WarehouseCode", RRtollDetail.WarehouseCode);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "RawMatType", RRtollDetail.RawMatType);
                DT1.Rows.Add("Procurement.RRtollDetail", "set", "UOMReq", RRtollDetail.UOMReq);


                DT1.Rows.Add("Procurement.RRtollDetail", "set", "ExpDate", RRtollDetail.ExpDate);


                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteRRtollDetail(RRtollDetail RRtollDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollDetail", "cond", "DocNumber", RRtollDetail.DocNumber);
                DT1.Rows.Add("Procurement.RRtollDetail", "cond", "LineNumber", RRtollDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.RRtollDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public class RRtollSpices
        {
            public virtual RRtoll Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal RequestQty { get; set; }
            public virtual string UnitBase { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual Boolean IsAllowPartial { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public virtual decimal RRQty { get; set; }
            public virtual string RefRawMatCode { get; set; }
            public virtual string WarehouseCode { get; set; }
            public virtual string RawMatType { get; set; }
            public virtual string UOMReq { get; set; }

            //2021-06-16    EMC add fields
            public virtual string ItemDescription { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual string ProcessSteps { get; set; }
            public virtual string OCNNumber { get; set; }
            public virtual decimal Qty { get; set; }

            public virtual string UOM { get; set; }
            public virtual string SpecialHandlingInstruction { get; set; }
            public virtual string Remarks { get; set; }
            public virtual decimal TotalQty { get; set; }
            public virtual string ScrapCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual bool IsPrinted { get; set; }


            public virtual DateTime ExpDate { get; set; }


            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,FullDesc from Procurement.RRtollSpices a left join masterfile.item b on a.ItemCode = b.ItemCode " +
                                            " where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRRtollDetail(RRtollSpices RRtollSpices)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.RRtollSpices where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "LineNumber", strLine);


                DT1.Rows.Add("Procurement.RRtollSpices", "0", "ItemCode", RRtollSpices.ItemCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "ColorCode", RRtollSpices.ColorCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "ClassCode", RRtollSpices.ClassCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "SizeCode", RRtollSpices.SizeCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "RequestQty", RRtollSpices.RequestQty);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "UnitBase", RRtollSpices.UnitBase);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "OrderQty", RRtollSpices.OrderQty);
                               
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field1", RRtollSpices.Field1);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field2", RRtollSpices.Field2);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field3", RRtollSpices.Field3);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field4", RRtollSpices.Field4);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field5", RRtollSpices.Field5);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field6", RRtollSpices.Field6);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field7", RRtollSpices.Field7);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field8", RRtollSpices.Field8);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "Field9", RRtollSpices.Field9);


                DT1.Rows.Add("Procurement.RRtollSpices", "0", "RRQty", RRtollSpices.RRQty);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "RefRawMatCode", RRtollSpices.RefRawMatCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "WarehouseCode", RRtollSpices.WarehouseCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "RawMatType", RRtollSpices.RawMatType);
                DT1.Rows.Add("Procurement.RRtollSpices", "0", "UOMReq", RRtollSpices.UOMReq);




                DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateRRtollDetail(RRtollSpices RRtollSpices)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollSpices", "cond", "DocNumber", RRtollSpices.DocNumber);
                DT1.Rows.Add("Procurement.RRtollSpices", "cond", "LineNumber", RRtollSpices.LineNumber);

                DT1.Rows.Add("Procurement.RRtollSpices", "set", "ItemCode", RRtollSpices.ItemCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "ColorCode", RRtollSpices.ColorCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "ClassCode", RRtollSpices.ClassCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "SizeCode", RRtollSpices.SizeCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "RequestQty", RRtollSpices.RequestQty);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "UnitBase", RRtollSpices.UnitBase);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "OrderQty", RRtollSpices.OrderQty);

                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field1", RRtollSpices.Field1);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field2", RRtollSpices.Field2);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field3", RRtollSpices.Field3);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field4", RRtollSpices.Field4);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field5", RRtollSpices.Field5);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field6", RRtollSpices.Field6);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field7", RRtollSpices.Field7);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field8", RRtollSpices.Field8);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "Field9", RRtollSpices.Field9);

                DT1.Rows.Add("Procurement.RRtollSpices", "set", "RRQty", RRtollSpices.RRQty);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "RefRawMatCode", RRtollSpices.RefRawMatCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "WarehouseCode", RRtollSpices.WarehouseCode);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "RawMatType", RRtollSpices.RawMatType);
                DT1.Rows.Add("Procurement.RRtollSpices", "set", "UOMReq", RRtollSpices.UOMReq);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteRRtollDetail(RRtollSpices RRtollSpices)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollSpices", "cond", "DocNumber", RRtollSpices.DocNumber);
                DT1.Rows.Add("Procurement.RRtollSpices", "cond", "LineNumber", RRtollSpices.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.RRtollSpices where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }



        public class RRtollService
        {
            public virtual RRtoll Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string ServiceType { get; set; }
            public virtual string Description { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Unit { get; set; }
            public virtual bool IsAllowProgressBilling { get; set; }
            public virtual decimal ServicePOQty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            //emc888
            public virtual string RefSpiCode { get; set; }
            public virtual string CustomerCode { get; set; }
            public virtual string ItemCode2 { get; set; }
            public virtual string ItemDesc2 { get; set; }

            public virtual string RawMatType2 { get; set; }
            public virtual string UOMreq2 { get; set; }
            public virtual string UOMrr2 { get; set; }
            public virtual decimal ReqQty2 { get; set; }
            public virtual decimal RRQty2 { get; set; }

           

            

            public virtual DateTime ExpDate { get; set; }




            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select  B.FullDesc AS Description, A.* from Procurement.RRtollService a left join masterfile.item b on a.ItemCode2 = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRRtollDetail(RRtollService _ent)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.RRtollService where DocNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Procurement.RRtollService", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.RRtollService", "0", "DocNumber", Docnum);

                //DT1.Rows.Add("Procurement.RRtollService", "0", "ServiceType", _ent.ServiceType);
                DT1.Rows.Add("Procurement.RRtollService", "0", "ServiceType", " ");

                DT1.Rows.Add("Procurement.RRtollService", "0", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Qty", _ent.Qty);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.RRtollService", "0", "IsAllowProgressBilling", _ent.IsAllowProgressBilling);
                DT1.Rows.Add("Procurement.RRtollService", "0", "ServicePOQty", _ent.ServicePOQty);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.RRtollService", "0", "Field9", _ent.Field9);

                DT1.Rows.Add("Procurement.RRtollService", "0", "RefSpiCode", _ent.RefSpiCode);
                DT1.Rows.Add("Procurement.RRtollService", "0", "CustomerCode", _ent.CustomerCode);
                DT1.Rows.Add("Procurement.RRtollService", "0", "ItemCode2", _ent.ItemCode2);
                DT1.Rows.Add("Procurement.RRtollService", "0", "ItemDesc2", _ent.ItemDesc2);
                DT1.Rows.Add("Procurement.RRtollService", "0", "RawMatType2", _ent.RawMatType2);

                DT1.Rows.Add("Procurement.RRtollService", "0", "UOMreq2", _ent.UOMreq2);
                DT1.Rows.Add("Procurement.RRtollService", "0", "UOMrr2", _ent.UOMrr2);
                DT1.Rows.Add("Procurement.RRtollService", "0", "ReqQty2", _ent.ReqQty2);
                DT1.Rows.Add("Procurement.RRtollService", "0", "RRQty2", _ent.RRQty2);

                DT1.Rows.Add("Procurement.RRtollService", "0", "ExpDate", _ent.ExpDate);


                DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateRRtollDetail(RRtollService _ent)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollService", "cond", "LineNumber", _ent.LineNumber);
                DT1.Rows.Add("Procurement.RRtollService", "cond", "DocNumber", _ent.DocNumber);
                DT1.Rows.Add("Procurement.RRtollService", "set", "ServiceType", _ent.ServiceType);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Qty", _ent.Qty);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.RRtollService", "set", "IsAllowProgressBilling", _ent.IsAllowProgressBilling);
                DT1.Rows.Add("Procurement.RRtollService", "set", "ServicePOQty", _ent.ServicePOQty);

                DT1.Rows.Add("Procurement.RRtollService", "set", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.RRtollService", "set", "Field9", _ent.Field9);

                DT1.Rows.Add("Procurement.RRtollService", "set", "RefSpiCode", _ent.RefSpiCode);
                DT1.Rows.Add("Procurement.RRtollService", "set", "CustomerCode", _ent.CustomerCode);
                DT1.Rows.Add("Procurement.RRtollService", "set", "ItemCode2", _ent.ItemCode2);
                DT1.Rows.Add("Procurement.RRtollService", "set", "ItemDesc2", _ent.ItemDesc2);

                DT1.Rows.Add("Procurement.RRtollService", "set", "RawMatType2", _ent.RawMatType2);
                DT1.Rows.Add("Procurement.RRtollService", "set", "UOMreq2", _ent.UOMreq2);
                DT1.Rows.Add("Procurement.RRtollService", "set", "UOMrr2", _ent.UOMrr2);
                DT1.Rows.Add("Procurement.RRtollService", "set", "ReqQty2", _ent.ReqQty2);
                DT1.Rows.Add("Procurement.RRtollService", "set", "RRQty2", _ent.RRQty2);

                DT1.Rows.Add("Procurement.RRtollService", "set", "ExpDate", _ent.ExpDate);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteRRtollDetail(RRtollService RRtollDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollService", "cond", "DocNumber", RRtollDetail.DocNumber);
                DT1.Rows.Add("Procurement.RRtollService", "cond", "LineNumber", RRtollDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.RRtollService where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }


        public class RRtollScrap
        {


            public virtual RRtoll Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string RefScrapCode { get; set; }
            public virtual string SCustomer{ get; set; }
            public virtual string SItemCode { get; set; }
            public virtual string ItemDesc { get; set; }
            public virtual decimal RequestQty { get; set; }
            public virtual string UOM { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual string Step { get; set; }
            public virtual string Appearance { get; set; }
            public virtual string Disposition { get; set; }
            public virtual string HandInst { get; set; }
            public virtual string SRemarks { get; set; }

            public virtual string RawMatType3 { get; set; }
            public virtual string UOMreq3 { get; set; }
            public virtual string UOMrr3 { get; set; }
            public virtual decimal ReqQty3 { get; set; }
            public virtual decimal RRQty3 { get; set; }


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


            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select B.FullDesc AS ItemDesc ,A.* from Procurement.RRtollScrap a left join masterfile.item b on a.SItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRRtollDetail(RRtollScrap _ent)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.RRtollScrap where DocNumber = '" + Docnum + "'", Conn);

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


                //emc888
                //[RefScrapCode] [varchar] (50) NULL,
                //[CustomerCode] [varchar] (50) NULL,
                //[ItemCode] [varchar] (50) NULL,
                //[ItemDesc] [varchar] (200) NULL,
                //[MaterialType] [varchar] (50) NULL,
                //[RequestQty] [decimal](16, 4) NULL,
                //[UOM] [varchar] (50) NULL,
                //   [BatchNumber] [varchar] (50) NULL,
                //   [Step] [varchar] (50) NULL,
                //[ExpDate] [datetime] NULL,
                //   [Appearance] [varchar] (50) NULL,
                //   [Disposition] [varchar] (50) NULL,
                //   [HandInst] [varchar] (100) NULL,
                //   [SRemarks] [varchar] (100) NULL,


                DT1.Rows.Add("Procurement.RRtollScrap", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "DocNumber", Docnum);

                DT1.Rows.Add("Procurement.RRtollScrap", "0", "RefScrapCode", _ent.RefScrapCode);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "SCustomer", _ent.SCustomer);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "SItemCode", _ent.SItemCode);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "ItemDesc", _ent.ItemDesc);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "RequestQty", _ent.RequestQty);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "UOM", _ent.UOM);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "BatchNumber", _ent.BatchNumber);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Step", _ent.Step);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "ExpDate", _ent.ExpDate);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Appearance", _ent.Appearance);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Disposition", _ent.Disposition);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "HandInst", _ent.HandInst);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "SRemarks", _ent.SRemarks);

                DT1.Rows.Add("Procurement.RRtollScrap", "0", "RawMatType3", _ent.RawMatType3);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "UOMreq3", _ent.UOMreq3);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "UOMrr3", _ent.UOMrr3);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "ReqQty3", _ent.ReqQty3);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "RRQty3", _ent.RRQty3);

                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.RRtollScrap", "0", "Field9", _ent.Field9);


                DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateRRtollDetail(RRtollScrap _ent)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollScrap", "cond", "LineNumber", _ent.LineNumber);
                DT1.Rows.Add("Procurement.RRtollScrap", "cond", "DocNumber", _ent.DocNumber);

                DT1.Rows.Add("Procurement.RRtollScrap", "set", "RefScrapCode", _ent.RefScrapCode);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "SCustomer", _ent.SCustomer);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "SItemCode", _ent.SItemCode);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "ItemDesc", _ent.ItemDesc);

                DT1.Rows.Add("Procurement.RRtollScrap", "set", "RequestQty", _ent.RequestQty);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "UOM", _ent.UOM);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "BatchNumber", _ent.BatchNumber);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Step", _ent.Step);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "ExpDate", _ent.ExpDate);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Appearance", _ent.Appearance);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Disposition", _ent.Disposition);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "HandInst", _ent.HandInst);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "SRemarks", _ent.SRemarks);

                DT1.Rows.Add("Procurement.RRtollScrap", "set", "RawMatType3", _ent.RawMatType3);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "UOMreq3", _ent.UOMreq3);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "UOMrr3", _ent.UOMrr3);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "ReqQty3", _ent.ReqQty3);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "RRQty3", _ent.RRQty3);

                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.RRtollScrap", "set", "Field9", _ent.Field9);


                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteRRtollDetail(RRtollScrap RRtollDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.RRtollScrap", "cond", "DocNumber", RRtollDetail.DocNumber);
                DT1.Rows.Add("Procurement.RRtollScrap", "cond", "LineNumber", RRtollDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.RRtollScrap where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }


        public class RefTransaction
        {
            public virtual RRtoll Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='PRDPRQT' OR  A.TransType='PRDPRQT') ", Conn);
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
            a = Gears.RetriveData2("select * from Procurement.RRtoll where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                TargetDate = dtRow["TargetDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                StockNumber = dtRow["StockNumber"].ToString();
                Status = dtRow["Status"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                CostCenter = dtRow["CostCenter"].ToString();
                PODocNumber = dtRow["PODocNumber"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString(); 
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                ForceClosedBy = dtRow["ForceClosedBy"].ToString();
                ForceClosedDate = dtRow["ForceClosedDate"].ToString();
                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);
                //IsSample = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSample"]) ? false : dtRow["IsSample"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);

                //2021-06-21    EMC add code for Toll Module
                Year = Convert.ToInt32(Convert.IsDBNull(dtRow["Year"]) ? "0" : dtRow["Year"].ToString());
                WorkWeek = Convert.ToInt32(Convert.IsDBNull(dtRow["WorkWeek"]) ? "0" : dtRow["WorkWeek"].ToString());
                DayNo = Convert.ToInt32(Convert.IsDBNull(dtRow["DayNo"]) ? "0" : dtRow["DayNo"].ToString());
       
                RequestingDeptCompany = dtRow["RequestingDeptCompany"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                IsAllowPartialHeader = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsAllowPartial"]) ? false : dtRow["IsAllowPartial"]);
                Consignee = dtRow["Consignee"].ToString();
                ConsigneeAddress = dtRow["ConsigneeAddress"].ToString();
                RequiredLoadingTime = dtRow["RequiredLoadingTime"].ToString();
                ShipmentType = dtRow["ShipmentType"].ToString();
                OCNRequestNumber = dtRow["OCNRequestNumber"].ToString();
                RequestRef = dtRow["RequestRef"].ToString();
                RequestType = dtRow["RequestType"].ToString();

                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();
            }

            return a;
        }
        public void InsertData(RRtoll _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.RRtoll", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.RRtoll", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.RRtoll", "0", "TargetDate", _ent.TargetDate);
            DT1.Rows.Add("Procurement.RRtoll", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Procurement.RRtoll", "0", "StockNumber", _ent.StockNumber);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Status", _ent.Status);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.RRtoll", "0", "CostCenter", _ent.CostCenter);
            
            //DT1.Rows.Add("Procurement.RRtoll", "0", "PODocNumber", _ent.PODocNumber);
            //DT1.Rows.Add("Procurement.RRtoll", "0", "SubmittedBy", _ent.SubmittedBy);
            //DT1.Rows.Add("Procurement.RRtoll", "0", "SubmittedDate", _ent.SubmittedDate);
            //DT1.Rows.Add("Procurement.RRtoll", "0", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Procurement.RRtoll", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Procurement.RRtoll", "0", "LastEditedBy", _ent.LastEditedBy);
            //DT1.Rows.Add("Procurement.RRtoll", "0", "LastEditedDate", _ent.LastEditedDate);


            //2021-06-22    EMC add code for Toll Module
            DT1.Rows.Add("Procurement.RRtoll", "0", "Year", _ent.Year);
            DT1.Rows.Add("Procurement.RRtoll", "0", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Procurement.RRtoll", "0", "DayNo", _ent.DayNo);
          
            DT1.Rows.Add("Procurement.RRtoll", "0", "RequestingDeptCompany", _ent.RequestingDeptCompany);

            DT1.Rows.Add("Procurement.RRtoll", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Procurement.RRtoll", "0", "IsAllowPartial", _ent.IsAllowPartialHeader);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Consignee", _ent.Consignee);
            DT1.Rows.Add("Procurement.RRtoll", "0", "ConsigneeAddress", _ent.ConsigneeAddress);
            DT1.Rows.Add("Procurement.RRtoll", "0", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Procurement.RRtoll", "0", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("Procurement.RRtoll", "0", "OCNRequestNumber", _ent.OCNRequestNumber);

            DT1.Rows.Add("Procurement.RRtoll", "0", "RequestRef", _ent.RequestRef);
            DT1.Rows.Add("Procurement.RRtoll", "0", "RequestType", _ent.RequestType);
           
            //DT1.Rows.Add("Sales.SODueDateRevision", "0", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Sales.SODueDateRevision", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Procurement.RRtoll", "0", "IsPrinted", "False");
            
            
            DT1.Rows.Add("Procurement.RRtoll", "0", "IsWithDetail", "False");
            

            DT1.Rows.Add("Procurement.RRtoll", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.RRtoll", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(RRtoll _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.RRtoll", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.RRtoll", "set", "TargetDate", _ent.TargetDate);
            DT1.Rows.Add("Procurement.RRtoll", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Procurement.RRtoll", "set", "StockNumber", _ent.StockNumber);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Status", "N");
            DT1.Rows.Add("Procurement.RRtoll", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.RRtoll", "set", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Procurement.RRtoll", "set", "PODocNumber", _ent.PODocNumber);

            //2021-06-21    EMC add code for Toll Module
            DT1.Rows.Add("Procurement.RRtoll", "set", "Year", _ent.Year);
            DT1.Rows.Add("Procurement.RRtoll", "set", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Procurement.RRtoll", "set", "DayNo", _ent.DayNo);
           
            DT1.Rows.Add("Procurement.RRtoll", "set", "RequestingDeptCompany", _ent.RequestingDeptCompany);

            DT1.Rows.Add("Procurement.RRtoll", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Procurement.RRtoll", "set", "IsAllowPartial", _ent.IsAllowPartialHeader);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Consignee", _ent.Consignee);
            DT1.Rows.Add("Procurement.RRtoll", "set", "ConsigneeAddress", _ent.ConsigneeAddress);
            DT1.Rows.Add("Procurement.RRtoll", "set", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Procurement.RRtoll", "set", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("Procurement.RRtoll", "set", "OCNRequestNumber", _ent.OCNRequestNumber);

            DT1.Rows.Add("Procurement.RRtoll", "set", "RequestRef", _ent.RequestRef);
            DT1.Rows.Add("Procurement.RRtoll", "set", "RequestType", _ent.RequestType);
           
            //DT1.Rows.Add("Procurement.RRtoll", "set", "ApprovedBy", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.RRtoll", "set", "ApprovedDate", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.RRtoll", "set", "AddedBy", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.RRtoll", "set", "AddedDate", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.RRtoll", "set", "LastEditedBy", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.RRtoll", "set", "LastEditedDate", _ent.CustomerCode);

            DT1.Rows.Add("Procurement.RRtoll", "set", "IsPrinted", _ent.IsPrinted);

            //DT1.Rows.Add("Procurement.RRtoll", "set", "IsSample", _ent.IsSample);
            //DT1.Rows.Add("Procurement.RRtoll", "set", "IsValidated", _ent.IsValidated);
           // DT1.Rows.Add("Procurement.RRtoll", "set", "IsWithDetail", _ent.IsWithDetail);

            DT1.Rows.Add("Procurement.RRtoll", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.RRtoll", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            DT1.Rows.Add("Procurement.RRtoll", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.RRtoll", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDPRQT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(RRtoll _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.RRtoll", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Procurement.RRtollDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);


            Functions.AuditTrail("PRDRRT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", Conn);
        }
    }
}
