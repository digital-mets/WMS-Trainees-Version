using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class OCN
    {

        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
  
        public virtual string WarehouseCode { get; set; }
        public virtual string PlantCode { get; set; }

        public virtual string StorerKey { get; set; }
        public virtual string TargetDate { get; set; }
        public virtual string DeliveryDate { get; set; }
        public virtual string StatusCode { get; set; }

        public virtual string Consignee { get; set; }
        public virtual string ConAddr { get; set; }
        public virtual string ReqDept { get; set; }
        public virtual string RefDoc { get; set; }
        public virtual string Addmanpower { get; set; }
        public virtual string NOmanpower { get; set; }
        public virtual string Overtime { get; set; }
        public virtual string TBSB { get; set; }
        public virtual string TProvided { get; set; }
        public virtual string ShipmentType { get; set; }

        public virtual string PickType { get; set; }
        public virtual string TruckingCompany { get; set; }
        public virtual string PlateNumber { get; set; }
        public virtual string DriverName { get; set; }
        public virtual string DeliverToName { get; set; }
        public virtual string DeliverToAddress { get; set; }
        public virtual string Instruction { get; set; }

        public virtual string LoadingTime { get; set; }

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
        public virtual IList<OCNDetail> Detail { get; set; }


        public class OCNDetail
        {
            public virtual OCN Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string OutgoingDocNumber { get; set; }
            public virtual string OutgoingDocType { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal Price { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal Weight { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual string LotNo { get; set; }
            public virtual string SpecialHandlingInstruc { get; set; }
            public virtual string MLIRemarks01 { get; set; }
            public virtual string MLIRemarks02 { get; set; }
            public virtual string UDF01 { get; set; }
            public virtual string UDF02 { get; set; }
            public virtual string UDF03 { get; set; }
            public virtual DateTime MfgDate { get; set; }
            public virtual DateTime ExpDate { get; set; }
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

            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select a.*,b.FullDesc from WMS.OCNDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber"
                        ,Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddOCNDetail(OCNDetail OCNDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.OCNDetail where docnumber = '" + Docnum + "'"
                    ,Conn);

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
                DT1.Rows.Add("WMS.OCNDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.OCNDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("WMS.OCNDetail", "0", "OutgoingDocNumber", OCNDetail.OutgoingDocNumber);
                DT1.Rows.Add("WMS.OCNDetail", "0", "OutgoingDocType", OCNDetail.OutgoingDocType);
                DT1.Rows.Add("WMS.OCNDetail", "0", "ItemCode", OCNDetail.ItemCode);
                DT1.Rows.Add("WMS.OCNDetail", "0", "ColorCode", OCNDetail.ColorCode);
                DT1.Rows.Add("WMS.OCNDetail", "0", "ClassCode", OCNDetail.ClassCode);
                DT1.Rows.Add("WMS.OCNDetail", "0", "SizeCode", OCNDetail.SizeCode);
                DT1.Rows.Add("WMS.OCNDetail", "0", "BulkQty", OCNDetail.BulkQty);
                DT1.Rows.Add("WMS.OCNDetail", "0", "BulkUnit", OCNDetail.BulkUnit);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Qty", OCNDetail.Qty);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Unit ", OCNDetail.Unit);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Price ", OCNDetail.Price);

                DT1.Rows.Add("WMS.OCNDetail", "0", "BatchNumber ", OCNDetail.BatchNumber);
                DT1.Rows.Add("WMS.OCNDetail", "0", "LotNo ", OCNDetail.LotNo);
                DT1.Rows.Add("WMS.OCNDetail", "0", "MfgDate ", OCNDetail.MfgDate);
                DT1.Rows.Add("WMS.OCNDetail", "0", "ExpDate ", OCNDetail.ExpDate);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Weight ", OCNDetail.Weight);
                DT1.Rows.Add("WMS.OCNDetail", "0", "SpecialHandlingInstruc ", OCNDetail.SpecialHandlingInstruc);
                DT1.Rows.Add("WMS.OCNDetail", "0", "MLIRemarks01 ", OCNDetail.MLIRemarks01);
                DT1.Rows.Add("WMS.OCNDetail", "0", "MLIRemarks02 ", OCNDetail.MLIRemarks02);
                DT1.Rows.Add("WMS.OCNDetail", "0", "UDF01 ", OCNDetail.UDF01);
                DT1.Rows.Add("WMS.OCNDetail", "0", "UDF02 ", OCNDetail.UDF02);
                DT1.Rows.Add("WMS.OCNDetail", "0", "UDF03 ", OCNDetail.UDF03);

                DT1.Rows.Add("WMS.OCNDetail", "0", "BaseQty ", OCNDetail.BaseQty);
                DT1.Rows.Add("WMS.OCNDetail", "0", "BarcodeNo", OCNDetail.BarcodeNo);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field1", OCNDetail.Field1);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field2", OCNDetail.Field2);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field3", OCNDetail.Field3);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field4", OCNDetail.Field4);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field5", OCNDetail.Field5);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field6", OCNDetail.Field6);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field7", OCNDetail.Field7);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field8", OCNDetail.Field8);
                DT1.Rows.Add("WMS.OCNDetail", "0", "Field9", OCNDetail.Field9);
                DT2.Rows.Add("WMS.OCN", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.OCN", "0", "IsWithDetail", "True");
               
          
                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);
            }
            public void UpdateOCNDetail(OCNDetail OCNDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.OCNDetail", "cond", "DocNumber", OCNDetail.DocNumber);
                DT1.Rows.Add("WMS.OCNDetail", "cond", "LineNumber", OCNDetail.LineNumber);
                DT1.Rows.Add("WMS.OCNDetail", "set", "OutgoingDocNumber", OCNDetail.OutgoingDocNumber);
                DT1.Rows.Add("WMS.OCNDetail", "set", "OutgoingDocType", OCNDetail.OutgoingDocType);
                DT1.Rows.Add("WMS.OCNDetail", "set", "ItemCode", OCNDetail.ItemCode);
                DT1.Rows.Add("WMS.OCNDetail", "set", "ColorCode", OCNDetail.ColorCode);
                DT1.Rows.Add("WMS.OCNDetail", "set", "ClassCode", OCNDetail.ClassCode);
                DT1.Rows.Add("WMS.OCNDetail", "set", "SizeCode", OCNDetail.SizeCode);
                DT1.Rows.Add("WMS.OCNDetail", "set", "BulkQty", OCNDetail.BulkQty);
                DT1.Rows.Add("WMS.OCNDetail", "set", "BulkUnit", OCNDetail.BulkUnit);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Qty", OCNDetail.Qty);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Unit ", OCNDetail.Unit);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Price ", OCNDetail.Price);

                DT1.Rows.Add("WMS.OCNDetail", "set", "BaseQty ", OCNDetail.BaseQty);
                DT1.Rows.Add("WMS.OCNDetail", "set", "BarcodeNo ", OCNDetail.BarcodeNo);

                DT1.Rows.Add("WMS.OCNDetail", "set", "BatchNumber ", OCNDetail.BatchNumber);
                DT1.Rows.Add("WMS.OCNDetail", "set", "LotNo ", OCNDetail.LotNo);
                DT1.Rows.Add("WMS.OCNDetail", "set", "MfgDate ", OCNDetail.MfgDate);
                DT1.Rows.Add("WMS.OCNDetail", "set", "ExpDate ", OCNDetail.ExpDate);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Weight ", OCNDetail.Weight);
                DT1.Rows.Add("WMS.OCNDetail", "set", "SpecialHandlingInstruc ", OCNDetail.SpecialHandlingInstruc);
                DT1.Rows.Add("WMS.OCNDetail", "set", "MLIRemarks01 ", OCNDetail.MLIRemarks01);
                DT1.Rows.Add("WMS.OCNDetail", "set", "MLIRemarks02 ", OCNDetail.MLIRemarks02);
                DT1.Rows.Add("WMS.OCNDetail", "set", "UDF01 ", OCNDetail.UDF01);
                DT1.Rows.Add("WMS.OCNDetail", "set", "UDF02 ", OCNDetail.UDF02);
                DT1.Rows.Add("WMS.OCNDetail", "set", "UDF03 ", OCNDetail.UDF03);

                DT1.Rows.Add("WMS.OCNDetail", "set", "Field1", OCNDetail.Field1);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field2", OCNDetail.Field2);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field3", OCNDetail.Field3);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field4", OCNDetail.Field4);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field5", OCNDetail.Field5);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field6", OCNDetail.Field6);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field7", OCNDetail.Field7);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field8", OCNDetail.Field8);
                DT1.Rows.Add("WMS.OCNDetail", "set", "Field9", OCNDetail.Field9);

                Gears.UpdateData(DT1,Conn);
            }
            public void DeleteOCNDetail(OCNDetail OCNDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.OCNDetail", "cond", "DocNumber", OCNDetail.DocNumber);
                DT1.Rows.Add("WMS.OCNDetail", "cond", "LineNumber", OCNDetail.LineNumber);


                Gears.DeleteData(DT1,Conn);

                DataTable count = Gears.RetriveData2("select * from WMS.OCNDetail where docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.OCNDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.OCNDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }

            }
        }

        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;

            // if (DocNumber != null)
            // {
            a = Gears.RetriveData2("select *  from wms.ocn where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                PlantCode = dtRow["PlantCode"].ToString();
                StorerKey = dtRow["StorerKey"].ToString();
                TargetDate = dtRow["TargetDate"].ToString();
                DeliveryDate = dtRow["DeliveryDate"].ToString();
                StatusCode = dtRow["StatusCode"].ToString();
                PickType = dtRow["PickType"].ToString();
                TruckingCompany = dtRow["TruckingCompany"].ToString();
                PlateNumber = dtRow["PlateNo"].ToString();
                DriverName = dtRow["DriverName"].ToString();
                DeliverToAddress = dtRow["DeliverToAddress"].ToString();
                DeliverToName = dtRow["DeliverToName"].ToString();
                Instruction = dtRow["Instruction"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();

                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                //ActivatedBy = dtRow["ActivatedBy"].ToString();
                //ActivatedDate = dtRow["ActivatedDate"].ToString();
                //DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                //DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                Consignee = dtRow["Consignee"].ToString();
                ConAddr = dtRow["ConsigneeAddress"].ToString();
                ReqDept = dtRow["CompanyDept"].ToString();
                RefDoc = dtRow["RefDoc"].ToString();
                LoadingTime = dtRow["LoadingTime"].ToString();
                Overtime = dtRow["Overtime"].ToString();
                Addmanpower = dtRow["AddtionalManpower"].ToString();
                TBSB = dtRow["SuppliedBy"].ToString();
                NOmanpower = dtRow["NOManpower"].ToString();
                TProvided = dtRow["TruckProviderByMets"].ToString();
                ShipmentType = dtRow["ShipmentType"].ToString();

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
            //  }
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as OutgoingDocNumber,'' as OutgoingDocType,'' as WarehouseCode,'' as StorerKey" +
            //        ",'' as TargetDate,''  as TargetDate,'' as  DeliveryDate" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(OCN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.OCN", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.OCN", "0", "DocDate", _ent.DocDate);
            
            DT1.Rows.Add("WMS.OCN", "0", "WarehouseCode", _ent.WarehouseCode);

            DT1.Rows.Add("WMS.OCN", "0", "PlantCode", _ent.PlantCode);

            DT1.Rows.Add("WMS.OCN", "0", "StorerKey", _ent.StorerKey);
            DT1.Rows.Add("WMS.OCN", "0", "TargetDate", _ent.TargetDate);
          //  DT1.Rows.Add("WMS.OCN", "0", "DeliveryDate", _ent.DeliveryDate);
            DT1.Rows.Add("WMS.OCN", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.OCN", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("WMS.OCN", "0", "PickType", _ent.PickType);
            DT1.Rows.Add("WMS.OCN", "0", "TruckingCompany", _ent.TruckingCompany);
            DT1.Rows.Add("WMS.OCN", "0", "PlateNo", _ent.PlateNumber);
            DT1.Rows.Add("WMS.OCN", "0", "DriverName", _ent.DriverName);
            DT1.Rows.Add("WMS.OCN", "0", "DeliverToName", _ent.DeliverToName);
            DT1.Rows.Add("WMS.OCN", "0", "DeliverToAddress", _ent.DeliverToAddress);
            DT1.Rows.Add("WMS.OCN", "0", "Instruction", _ent.Instruction);
            DT1.Rows.Add("WMS.OCN", "0", "Consignee", _ent.Consignee);
            DT1.Rows.Add("WMS.OCN", "0", "ConsigneeAddress", _ent.ConAddr);
            DT1.Rows.Add("WMS.OCN", "0", "RefDoc", _ent.RefDoc);
            DT1.Rows.Add("WMS.OCN", "0", "Overtime", _ent.Overtime);
            DT1.Rows.Add("WMS.OCN", "0", "AddtionalManpower", _ent.Addmanpower);
            DT1.Rows.Add("WMS.OCN", "0", "SuppliedBy", _ent.TBSB);
            DT1.Rows.Add("WMS.OCN", "0", "NOManpower", Convert.ToInt32(_ent.NOmanpower));
            DT1.Rows.Add("WMS.OCN", "0", "CompanyDept",_ent.ReqDept);
            DT1.Rows.Add("WMS.OCN", "0", "TruckProviderByMets", _ent.TProvided);

            DT1.Rows.Add("WMS.OCN", "0", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("WMS.OCN", "0", "LoadingTime", _ent.LoadingTime);
            
          
            DT1.Rows.Add("WMS.OCN", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.OCN", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.OCN", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.OCN", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.OCN", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.OCN", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.OCN", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.OCN", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.OCN", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.OCN", "0", "IsWithDetail", "False");
            DT1.Rows.Add("WMS.OCN", "0", "IsValidated", "False");


            Gears.CreateData(DT1,_ent.Connection);
        }

        public void UpdateData(OCN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("WMS.OCN", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.OCN", "set", "DocDate", _ent.DocDate);
        
            DT1.Rows.Add("WMS.OCN", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.OCN", "set", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("WMS.OCN", "set", "StatusCode", _ent.StatusCode);
            DT1.Rows.Add("WMS.OCN", "set", "StorerKey", _ent.StorerKey);
            DT1.Rows.Add("WMS.OCN", "set", "TargetDate", _ent.TargetDate);
            //DT1.Rows.Add("WMS.OCN", "set", "DeliveryDate", _ent.DeliveryDate);

            DT1.Rows.Add("WMS.OCN", "set", "PickType", _ent.PickType);
            DT1.Rows.Add("WMS.OCN", "set", "TruckingCompany", _ent.TruckingCompany);
            DT1.Rows.Add("WMS.OCN", "set", "PlateNo", _ent.PlateNumber);
            DT1.Rows.Add("WMS.OCN", "set", "DriverName", _ent.DriverName);
            DT1.Rows.Add("WMS.OCN", "set", "DeliverToName", _ent.DeliverToName);
            DT1.Rows.Add("WMS.OCN", "set", "DeliverToAddress", _ent.DeliverToAddress);
            DT1.Rows.Add("WMS.OCN", "set", "Instruction", _ent.Instruction);

            DT1.Rows.Add("WMS.OCN", "set", "Consignee", _ent.Consignee);
            DT1.Rows.Add("WMS.OCN", "set", "ConsigneeAddress", _ent.ConAddr);
            DT1.Rows.Add("WMS.OCN", "set", "RefDoc", _ent.RefDoc);
            DT1.Rows.Add("WMS.OCN", "set", "Overtime", _ent.Overtime);
            DT1.Rows.Add("WMS.OCN", "set", "AddtionalManpower", _ent.Addmanpower);
            DT1.Rows.Add("WMS.OCN", "set", "SuppliedBy", _ent.TBSB);
            DT1.Rows.Add("WMS.OCN", "set", "NOManpower", Convert.ToInt32(_ent.NOmanpower));
            DT1.Rows.Add("WMS.OCN", "set", "CompanyDept", _ent.ReqDept);
            DT1.Rows.Add("WMS.OCN", "set", "TruckProviderByMets", _ent.TProvided);

            DT1.Rows.Add("WMS.OCN", "set", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("WMS.OCN", "set", "LoadingTime", _ent.LoadingTime);

            DT1.Rows.Add("WMS.OCN", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.OCN", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.OCN", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.OCN", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.OCN", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.OCN", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.OCN", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.OCN", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.OCN", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.OCN", "set", "IsWithDetail", "True");
            DT1.Rows.Add("WMS.OCN", "set", "IsValidated", "True");
          
            DT1.Rows.Add("WMS.OCN", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.OCN", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSOCN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);

        }
        public void DeleteData(OCN _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.OCN", "cond", "DocNumber", _ent.DocNumber);

            //Delete each row from ocn detail when deleting the current Docnumber of ocn
            DataTable count = Gears.RetriveData2("select * from WMS.[OCNDetail] where DocNumber ='" + _ent.DocNumber + "'", _ent.Connection);//ADD CONN
            foreach (DataRow dtRow in count.Rows)
            {
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT2.Rows.Add("WMS.OCNDetail", "cond", "DocNumber", dtRow["DocNumber"].ToString());
                DT2.Rows.Add("WMS.OCNDetail", "cond", "LineNumber", dtRow["LineNumber"].ToString());
                Gears.DeleteData(DT2, _ent.Connection);//ADD CONN
            }

            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSOCN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
    }
}
