using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ICN
    {

        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Transtype { get; set; }
        public virtual string ExpectedDeliveryDate { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Plantcode { get; set; }
        public virtual string StorerKey { get; set; }
        public virtual string ArrivalDate { get; set; }

        public virtual string DRNumber { get; set; }
        public virtual string ContainerNo { get; set; }
        public virtual string ContactingDept { get; set; }
        public virtual string InvoiceNo { get; set; }
        public virtual string SealNo { get; set; }
        public virtual string DocumentationStaff { get; set; }
        public virtual string WarehouseChecker { get; set; }
        public virtual string StartUnload { get; set; }
        public virtual string CompleteUnload { get; set; }
        public virtual string ShipmentType { get; set; }
        public virtual string TrackingNO { get; set; }
        public virtual string LoadingTime { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }  
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string Status { get; set; }
        public virtual string CompanyDept { get; set; }
        public virtual string Consignee { get; set; }
        public virtual string ConsigneeAddr { get; set; }
        public virtual string Overtime { get; set; }
        public virtual string AddtionalManpower { get; set; }
        public virtual string RefDoc { get; set; }
        public virtual string SuppliedBy { get; set; }
        public virtual string NOmanpower { get; set; }

        public virtual string TruckingPro { get; set; }
        public virtual string TruckingCo { get; set; }
        public virtual string TruckType { get; set; }
        public virtual string PlateNumber { get; set; }
        public virtual string DriverName { get; set; }
        public virtual string SupplierCode { get; set; }
        //public virtual string SpecialInstruction { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual IList<ICNDetail> Detail { get; set; }


        public class ICNDetail 
        {
            public virtual ICN Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string IncomingDocType { get; set; }
            public virtual string IncomingDocNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual decimal InputBaseQty { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string LotNo { get; set; }
            public virtual string SpecialHandlingInstruc { get; set; }
            //public virtual string UDF01 { get; set; }
            //public virtual string UDF02 { get; set; }
            public virtual string UDF03 { get; set; }
            public virtual string MLIRemarks01 { get; set; }
            public virtual string MLIRemarks02 { get; set; }

            public virtual string Remarks { get; set; }
            public virtual decimal Price { get; set; }

            public virtual string BulkUnit { get; set; }
            //public virtual string Unit { get; set; }
            //public virtual decimal Price { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual DateTime ExpiryDateICN { get; set; }
            public virtual string BatchNumberICN { get; set; }
            public virtual DateTime ManufacturingDateICN { get; set; }
            public virtual decimal DocBaseQty { get; set; }
            public virtual decimal DocBulkQty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)//ADD CONN
            {
                
                DataTable a;
                try { 
                //a = Gears.RetriveData2("select [DocNumber],[LineNumber],[IncomingDocType],[IncomingDocNumber],a.[ItemCode],[ColorCode]"+
                //                       ",[ClassCode],[SizeCode],[InputBaseQty],[BulkQty],[BulkUnit],[Remarks],[Unit],[Price],[BaseQty],[StatusCode]"+
                //                       ",[BarcodeNo],a.[Field1],a.[Field2],a.[Field3],a.[Field4],a.[Field5],a.[Field6],a.[Field7],a.[Field8],a.[Field9],FullDesc"+
                //                       " from WMS.ICNDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber"
                //                       , Conn);//ADD CONN
                    a = Gears.RetriveData2("select a.*,b.FullDesc from WMS.ICNDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber"
                                        , Conn);//ADD CONN
                return a;
                }
                catch (Exception e)
                {
                a = null;
                return a;
                }
            }

            public void AddICNDetail(ICNDetail ICNDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.ICNDetail where docnumber = '" + Docnum + "'"
                    , Conn);//ADD CONN

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch {

                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ICNDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ICNDetail", "0", "LineNumber", strLine);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "IncomingDocType", ICNDetail.IncomingDocType);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "IncomingDocNumber", ICNDetail.IncomingDocNumber);
                DT1.Rows.Add("WMS.ICNDetail", "0", "ItemCode", ICNDetail.ItemCode);
                DT1.Rows.Add("WMS.ICNDetail", "0", "ColorCode", ICNDetail.ColorCode);
                DT1.Rows.Add("WMS.ICNDetail", "0", "ClassCode", ICNDetail.ClassCode);
                DT1.Rows.Add("WMS.ICNDetail", "0", "SizeCode", ICNDetail.SizeCode);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Remarks", ICNDetail.Remarks);
                DT1.Rows.Add("WMS.ICNDetail", "0", "BulkQty", ICNDetail.BulkQty);
                DT1.Rows.Add("WMS.ICNDetail", "0", "BulkUnit", ICNDetail.BulkUnit);
                DT1.Rows.Add("WMS.ICNDetail", "0", "InputBaseQty", ICNDetail.InputBaseQty);
                DT1.Rows.Add("WMS.ICNDetail", "0", "DocBaseQty", ICNDetail.DocBaseQty);
                DT1.Rows.Add("WMS.ICNDetail", "0", "DocBulkQty", ICNDetail.DocBulkQty);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Unit", ICNDetail.Unit);
                DT1.Rows.Add("WMS.ICNDetail", "0", "Price", ICNDetail.Price);

                DT1.Rows.Add("WMS.ICNDetail", "0", "StatusCode", ICNDetail.StatusCode);
                DT1.Rows.Add("WMS.ICNDetail", "0", "BaseQty", ICNDetail.BaseQty);
                DT1.Rows.Add("WMS.ICNDetail", "0", "ExpiryDateICN", ICNDetail.ExpiryDateICN);
                DT1.Rows.Add("WMS.ICNDetail", "0", "BatchNumberICN", ICNDetail.BatchNumberICN);
                DT1.Rows.Add("WMS.ICNDetail", "0", "ManufacturingDateICN", ICNDetail.ManufacturingDateICN);

                DT1.Rows.Add("WMS.ICNDetail", "0", "BarcodeNo", ICNDetail.BarcodeNo);


                //DT1.Rows.Add("WMS.ICNDetail", "0", "LotNo", ICNDetail.LotNo);
                DT1.Rows.Add("WMS.ICNDetail", "0", "SpecialHandlingInstruc", ICNDetail.SpecialHandlingInstruc);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "UDF01", ICNDetail.UDF01);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "UDF02", ICNDetail.UDF02);
                DT1.Rows.Add("WMS.ICNDetail", "0", "UDF03", ICNDetail.UDF03);
                DT1.Rows.Add("WMS.ICNDetail", "0", "MLIRemarks01", ICNDetail.MLIRemarks01);
                DT1.Rows.Add("WMS.ICNDetail", "0", "MLIRemarks02", ICNDetail.MLIRemarks02);

                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field1", ICNDetail.Field1);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field2", ICNDetail.Field2);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field3", ICNDetail.Field3);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field4", ICNDetail.Field4);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field5", ICNDetail.Field5);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field6", ICNDetail.Field6);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field7", ICNDetail.Field7);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field8", ICNDetail.Field8);
                //DT1.Rows.Add("WMS.ICNDetail", "0", "Field9", ICNDetail.Field9);

                DT2.Rows.Add("WMS.ICN", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.ICN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);//ADD CONN
                Gears.UpdateData(DT2, Conn);//ADD CONN
            }
            public void UpdateICNDetail(ICNDetail ICNDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ICNDetail", "cond", "DocNumber", ICNDetail.DocNumber);
                DT1.Rows.Add("WMS.ICNDetail", "cond", "LineNumber", ICNDetail.LineNumber);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "IncomingDocType", ICNDetail.IncomingDocType);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "IncomingDocNumber", ICNDetail.IncomingDocNumber);
                DT1.Rows.Add("WMS.ICNDetail", "set", "ItemCode", ICNDetail.ItemCode);
                DT1.Rows.Add("WMS.ICNDetail", "set", "ColorCode", ICNDetail.ColorCode);
                DT1.Rows.Add("WMS.ICNDetail", "set", "ClassCode", ICNDetail.ClassCode);
                DT1.Rows.Add("WMS.ICNDetail", "set", "SizeCode", ICNDetail.SizeCode);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Remarks", ICNDetail.Remarks);
                DT1.Rows.Add("WMS.ICNDetail", "set", "BulkQty", ICNDetail.BulkQty);
                DT1.Rows.Add("WMS.ICNDetail", "set", "BulkUnit", ICNDetail.BulkUnit);
                DT1.Rows.Add("WMS.ICNDetail", "set", "InputBaseQty ", ICNDetail.InputBaseQty);
                DT1.Rows.Add("WMS.ICNDetail", "set", "DocBaseQty", ICNDetail.DocBaseQty);
                DT1.Rows.Add("WMS.ICNDetail", "set", "DocBulkQty", ICNDetail.DocBulkQty);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Unit ", ICNDetail.Unit);
                DT1.Rows.Add("WMS.ICNDetail", "set", "Price ", ICNDetail.Price);
                DT1.Rows.Add("WMS.ICNDetail", "set", "StatusCode ", ICNDetail.StatusCode);
                DT1.Rows.Add("WMS.ICNDetail", "set", "BaseQty ", ICNDetail.BaseQty);
                DT1.Rows.Add("WMS.ICNDetail", "set", "ExpiryDateICN", ICNDetail.ExpiryDateICN);
                DT1.Rows.Add("WMS.ICNDetail", "set", "BatchNumberICN", ICNDetail.BatchNumberICN);
                DT1.Rows.Add("WMS.ICNDetail", "set", "ManufacturingDateICN", ICNDetail.ManufacturingDateICN);

                //DT1.Rows.Add("WMS.ICNDetail", "set", "LotNo", ICNDetail.LotNo);
                DT1.Rows.Add("WMS.ICNDetail", "set", "SpecialHandlingInstruc", ICNDetail.SpecialHandlingInstruc);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "UDF01", ICNDetail.UDF01);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "UDF02", ICNDetail.UDF02);
                DT1.Rows.Add("WMS.ICNDetail", "set", "UDF03", ICNDetail.UDF03);
                DT1.Rows.Add("WMS.ICNDetail", "set", "MLIRemarks01", ICNDetail.MLIRemarks01);
                DT1.Rows.Add("WMS.ICNDetail", "set", "MLIRemarks02", ICNDetail.MLIRemarks02);


                DT1.Rows.Add("WMS.ICNDetail", "set", "BarcodeNo", ICNDetail.BarcodeNo);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field1", ICNDetail.Field1);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field2", ICNDetail.Field2);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field3", ICNDetail.Field3);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field4", ICNDetail.Field4);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field5", ICNDetail.Field5);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field6", ICNDetail.Field6);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field7", ICNDetail.Field7);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field8", ICNDetail.Field8);
                //DT1.Rows.Add("WMS.ICNDetail", "set", "Field9", ICNDetail.Field9);

                Gears.UpdateData(DT1, Conn);//ADD CONN
            }
            public void DeleteICNDetail(ICNDetail ICNDetail)
            {
                

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ICNDetail", "cond", "DocNumber", ICNDetail.DocNumber);
                DT1.Rows.Add("WMS.ICNDetail", "cond", "LineNumber", ICNDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);//ADD CONN

                DataTable count = Gears.RetriveData2("select * from WMS.ICNDetail where docnumber = '" + Docnum + "'", Conn);//ADD CONN

                if (count.Rows.Count < 1){
                DT2.Rows.Add("WMS.ICNDetail", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.ICNDetail", "set", "IsWithDetail", "False");
                Gears.UpdateData(DT2, Conn);//ADD CONN
                }   
                
            }  
        }

        public DataTable getdata(string DocNumber, string Conn)//ADD CONN
        {
            
            DataTable specialData;
            DataTable a;
     
            //if (DocNumber != null)
            //{




            a = Gears.RetriveData2("select * from wms.ICN where DocNumber = '" + DocNumber + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    DocDate = dtRow["Docdate"].ToString();
                    ExpectedDeliveryDate = dtRow["ExpectedDeliveryDate"].ToString();
                    ArrivalDate = dtRow["ArrivalDate"].ToString();
                    WarehouseCode = dtRow["WarehouseCode"].ToString();
                    Plantcode = dtRow["Plantcode"].ToString(); 
                    
                    CompanyDept = dtRow["CompanyDept"].ToString();
                    Consignee = dtRow["Consignee"].ToString();
                    ConsigneeAddr = dtRow["ConsigneeAddress"].ToString();
                    Overtime = dtRow["Overtime"].ToString();
                    AddtionalManpower = dtRow["AddtionalManpower"].ToString();
                    SuppliedBy = dtRow["SuppliedBy"].ToString();
                    NOmanpower = dtRow["NOManpower"].ToString();
                    TruckingPro = dtRow["TruckProviderByMets"].ToString();
                    TruckType = dtRow["TruckType"].ToString();
                    StorerKey = dtRow["CustomerCode"].ToString();

                    Status = dtRow["Status"].ToString();
                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();

                    Transtype = dtRow["Type"].ToString();
                    RefDoc = dtRow["RefDoc"].ToString();
                    ShipmentType = dtRow["ShipmentType"].ToString();
                    TrackingNO = dtRow["TrackingNO"].ToString();
                    LoadingTime = dtRow["LoadingTime"].ToString();
                    TruckingCo = dtRow["TruckingCo"].ToString();
                    PlateNumber = dtRow["PlateNumber"].ToString();
                    DriverName = dtRow["DriverName"].ToString();
                    SupplierCode = dtRow["SupplierCode"].ToString();
                    //SpecialInstruction = dtRow["SpecialInstruction"].ToString();
                    DRNumber = dtRow["DRNumberICN"].ToString();
                    ContainerNo = dtRow["ContainerNoICN"].ToString();
                    ContactingDept = dtRow["ContactingDeptICN"].ToString();
                    InvoiceNo = dtRow["InvoiceNoICN"].ToString();
                    SealNo = dtRow["SealNoICN"].ToString();
                    DocumentationStaff = dtRow["DocumentationStaffICN"].ToString();
                    WarehouseChecker = dtRow["WarehouseCheckerICN"].ToString();
                    StartUnload = dtRow["StartUnloadICN"].ToString();
                    CompleteUnload = dtRow["CompleteUnloadICN"].ToString();

                    AddedBy = dtRow["AddedBy"].ToString();
                    SubmittedBy = dtRow["SubmittedBy"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    SubmittedDate = dtRow["SubmittedDate"].ToString();
                }
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as ExpectedDeliveryDate,'' as WarehouseCode,'' as StorerKey,'' as Field1"+
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}
            
            return a;
        }
        public void InsertData(ICN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.ICN", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.ICN", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ICN", "0", "ExpectedDeliveryDate", _ent.ExpectedDeliveryDate);
            DT1.Rows.Add("WMS.ICN", "0", "ArrivalDate", _ent.ArrivalDate);
            DT1.Rows.Add("WMS.ICN", "0", "Type", _ent.Transtype);
            DT1.Rows.Add("WMS.ICN", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.ICN", "0", "Plantcode", _ent.Plantcode);
            DT1.Rows.Add("WMS.ICN", "0", "CustomerCode", _ent.StorerKey);
            DT1.Rows.Add("WMS.ICN", "0", "DRNumberICN", _ent.DRNumber);

            DT1.Rows.Add("WMS.ICN", "0", "CompanyDept", _ent.CompanyDept);
            DT1.Rows.Add("WMS.ICN", "0", "Consignee", _ent.Consignee);
            DT1.Rows.Add("WMS.ICN", "0", "Overtime", _ent.Overtime);
            DT1.Rows.Add("WMS.ICN", "0", "AddtionalManpower", _ent.AddtionalManpower);
            DT1.Rows.Add("WMS.ICN", "0", "ConsigneeAddress", _ent.ConsigneeAddr);
            DT1.Rows.Add("WMS.ICN", "0", "SuppliedBy", _ent.SuppliedBy);
            DT1.Rows.Add("WMS.ICN", "0", "NOManpower", _ent.NOmanpower);
            DT1.Rows.Add("WMS.ICN", "0", "TruckType", _ent.TruckType);
            DT1.Rows.Add("WMS.ICN", "0", "TruckProviderByMets", _ent.TruckingPro);
            DT1.Rows.Add("WMS.ICN", "0", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("WMS.ICN", "0", "RefDoc", _ent.RefDoc);
            DT1.Rows.Add("WMS.ICN", "0", "TrackingNO", _ent.TrackingNO);
            DT1.Rows.Add("WMS.ICN", "0", "LoadingTime", _ent.LoadingTime);
            //DT1.Rows.Add("WMS.ICN", "0", "ContainerNoICN", _ent.ContainerNo);
            //DT1.Rows.Add("WMS.ICN", "0", "ContactingDeptICN", _ent.ContactingDept);
            //DT1.Rows.Add("WMS.ICN", "0", "InvoiceNoICN", _ent.InvoiceNo);
            //DT1.Rows.Add("WMS.ICN", "0", "SealNoICN", _ent.SealNo);
            //DT1.Rows.Add("WMS.ICN", "0", "DocumentationStaffICN", _ent.DocumentationStaff);
            //DT1.Rows.Add("WMS.ICN", "0", "WarehouseCheckerICN", _ent.WarehouseChecker);
            //DT1.Rows.Add("WMS.ICN", "0", "StartUnloadICN", _ent.StartUnload);
            //DT1.Rows.Add("WMS.ICN", "0", "CompleteUnloadICN", _ent.CompleteUnload);
            DT1.Rows.Add("WMS.ICN", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.ICN", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.ICN", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.ICN", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.ICN", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.ICN", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.ICN", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.ICN", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.ICN", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.ICN", "0", "TruckingCo", _ent.TruckingCo);
            DT1.Rows.Add("WMS.ICN", "0", "DriverName", _ent.DriverName);
            DT1.Rows.Add("WMS.ICN", "0", "PlateNumber", _ent.PlateNumber);
            DT1.Rows.Add("WMS.ICN", "0", "SupplierCode", _ent.SupplierCode);
            //DT1.Rows.Add("WMS.ICN", "0", "SpecialInstruction", _ent.SpecialInstruction);
            DT1.Rows.Add("WMS.ICN", "0", "IsWithDetail", "False");
            DT1.Rows.Add("WMS.ICN", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);//ADD CONN
        }
        public void UpdateData(ICN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.ICN", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.ICN", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ICN", "set", "ExpectedDeliveryDate", _ent.ExpectedDeliveryDate);
            DT1.Rows.Add("WMS.ICN", "set", "ArrivalDate", _ent.ArrivalDate);
            DT1.Rows.Add("WMS.ICN", "set", "Type", _ent.Transtype);
            DT1.Rows.Add("WMS.ICN", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.ICN", "set", "Plantcode", _ent.Plantcode);
            DT1.Rows.Add("WMS.ICN", "set", "CustomerCode", _ent.StorerKey);
            DT1.Rows.Add("WMS.ICN", "set", "Status", "N");
            DT1.Rows.Add("WMS.ICN", "set", "DRNumberICN", _ent.DRNumber);

            //DT1.Rows.Add("WMS.ICN", "set", "ContainerNoICN", _ent.ContainerNo);
            //DT1.Rows.Add("WMS.ICN", "set", "ContactingDeptICN", _ent.ContactingDept);
            //DT1.Rows.Add("WMS.ICN", "set", "InvoiceNoICN", _ent.InvoiceNo);
            //DT1.Rows.Add("WMS.ICN", "set", "SealNoICN", _ent.SealNo);
            //DT1.Rows.Add("WMS.ICN", "set", "DocumentationStaffICN", _ent.DocumentationStaff);
            //DT1.Rows.Add("WMS.ICN", "set", "WarehouseCheckerICN", _ent.WarehouseChecker);
            //DT1.Rows.Add("WMS.ICN", "set", "StartUnloadICN", _ent.StartUnload);
            //DT1.Rows.Add("WMS.ICN", "set", "CompleteUnloadICN", _ent.CompleteUnload);
            DT1.Rows.Add("WMS.ICN", "set", "CompanyDept", _ent.CompanyDept);
            DT1.Rows.Add("WMS.ICN", "set", "Consignee", _ent.Consignee);
            DT1.Rows.Add("WMS.ICN", "set", "Overtime", _ent.Overtime);
            DT1.Rows.Add("WMS.ICN", "set", "AddtionalManpower", _ent.AddtionalManpower);
            DT1.Rows.Add("WMS.ICN", "set", "ConsigneeAddress", _ent.ConsigneeAddr);
            DT1.Rows.Add("WMS.ICN", "set", "SuppliedBy", _ent.SuppliedBy);
            DT1.Rows.Add("WMS.ICN", "set", "NOManpower", _ent.NOmanpower);
            DT1.Rows.Add("WMS.ICN", "set", "TruckType", _ent.TruckType);
            DT1.Rows.Add("WMS.ICN", "set", "TruckProviderByMets", _ent.TruckingPro);
            DT1.Rows.Add("WMS.ICN", "set", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("WMS.ICN", "set", "RefDoc", _ent.RefDoc);
            DT1.Rows.Add("WMS.ICN", "set", "TrackingNO", _ent.TrackingNO);
            DT1.Rows.Add("WMS.ICN", "set", "LoadingTime", _ent.LoadingTime);

            DT1.Rows.Add("WMS.ICN", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.ICN", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.ICN", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.ICN", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.ICN", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.ICN", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.ICN", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.ICN", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.ICN", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.ICN", "set", "TruckingCo", _ent.TruckingCo);
            DT1.Rows.Add("WMS.ICN", "set", "DriverName", _ent.DriverName);
            DT1.Rows.Add("WMS.ICN", "set", "PlateNumber", _ent.PlateNumber);
            //DT1.Rows.Add("WMS.ICN", "set", "SpecialInstruction", _ent.SpecialInstruction);
            DT1.Rows.Add("WMS.ICN", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("WMS.ICN", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.ICN", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


           Gears.UpdateData(DT1, _ent.Connection);//ADD CONN

            Functions.AuditTrail("WMSICN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ICN _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.ICN", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);//ADD CONN
            Functions.AuditTrail("WMSICN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//ADD CONN
        }

    }
}
