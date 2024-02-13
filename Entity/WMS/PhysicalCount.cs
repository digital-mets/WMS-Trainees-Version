using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PhysicalCount
    {

        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string trans;
		
        private static string ddate;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
   	    public virtual string Type{ get; set; }
        public virtual string StorageType { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
    	public virtual string PlantCode { get; set; }
    	public virtual string RoomCode { get; set; }
        public virtual string CountTag { get; set; }
        public virtual string CountDate { get; set; }
        public virtual string Status { get; set; }
        public virtual string Remarks { get; set; }
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
        public virtual string Transtype { get; set; }
        public virtual IList<PhysicalCountDetail> Detail { get; set; }


        public class PhysicalCountDetail
        {
            public virtual PhysicalCount Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual decimal ActualQty { get; set; }
            public virtual decimal SystemQty { get; set; }
            public virtual decimal SystemBulkQty { get; set; }
            public virtual decimal VarianceQty { get; set; }
            public virtual decimal VarianceBulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual string LocationCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual string PalletID { get; set; }
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
                    a = Gears.RetriveData2("select * from WMS.PhysicalCountDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPhysicalCountDetail(PhysicalCountDetail PhysicalCountDetail)
            {
                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.PhysicalCountDetail where docnumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "BulkQty", PhysicalCountDetail.BulkQty);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "BulkUnit", PhysicalCountDetail.BulkUnit);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "PalletID", PhysicalCountDetail.PalletID);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "LocationCode", PhysicalCountDetail.LocationCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Unit", PhysicalCountDetail.Unit);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "ActualQty", PhysicalCountDetail.ActualQty);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "StatusCode", PhysicalCountDetail.StatusCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "BarcodeNo", PhysicalCountDetail.BarcodeNo);

                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field1", PhysicalCountDetail.Field1);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field2", PhysicalCountDetail.Field2);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field3", PhysicalCountDetail.Field3);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field4", PhysicalCountDetail.Field4);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field5", PhysicalCountDetail.Field5);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field6", PhysicalCountDetail.Field6);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field7", PhysicalCountDetail.Field7);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field8", PhysicalCountDetail.Field8);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "0", "Field9", PhysicalCountDetail.Field9);

                DT2.Rows.Add("WMS.PhysicalCount", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.PhysicalCount", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);

                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                DataTable CS = Gears.RetriveData2("Select ISNULL(IsByBulk,0) as IsByBulk  from masterfile.item where itemcode = '" + PhysicalCountDetail.ItemCode + "'",Conn);
                foreach (DataRow dt in CS.Rows)
                {
                    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                }
                if (isbybulk == true)
                {
                    
                    
                    for (int i = 1; i <= PhysicalCountDetail.BulkQty; i++)
                    {
                        string strLine2 = i.ToString().PadLeft(5, '0');

                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", PhysicalCountDetail.PalletID);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", PhysicalCountDetail.LocationCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "RRDate", ddate);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                        Gears.CreateData(DT4,Conn);
                        DT4.Rows.Clear();
                    }
                }
                else
                {
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", PhysicalCountDetail.PalletID);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", PhysicalCountDetail.LocationCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "RRDate", ddate);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", PhysicalCountDetail.BulkQty);
                  
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", PhysicalCountDetail.ActualQty);
                    Gears.CreateData(DT4,Conn);
                } 

            }
            public void UpdatePhysicalCountDetail(PhysicalCountDetail PhysicalCountDetail)
            {
                bool isbybulk = false;

                DataTable dtable = Gears.RetriveData2("Select BulkQty from wms.PhysicalCountdetail where docnumber = '" + PhysicalCountDetail.DocNumber + "' " +
                "and LineNumber = '" + PhysicalCountDetail.LineNumber + "'",Conn);
                foreach (DataRow dtrow in dtable.Rows)
                {
                    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != PhysicalCountDetail.BulkQty)
                    {
                        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", PhysicalCountDetail.DocNumber);
                        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PhysicalCountDetail.LineNumber);
                        Gears.DeleteData(DT3,Conn);

                        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                        DataTable CS = Gears.RetriveData2("Select ISNULL(IsByBulk,0) as IsByBulk  from masterfile.item where itemcode = '" + PhysicalCountDetail.ItemCode + "'",Conn);
                        foreach (DataRow dt in CS.Rows)
                        {
                            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                        }
                        if (isbybulk == true)
                        {

                            for (int i = 1; i <= PhysicalCountDetail.BulkQty; i++)
                            {
                                string strLine2 = i.ToString().PadLeft(5, '0');

                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PhysicalCountDetail.LineNumber);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", PhysicalCountDetail.PalletID);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", PhysicalCountDetail.LocationCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "RRDate", ddate);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                                Gears.CreateData(DT4,Conn);
                                DT4.Rows.Clear();
                            }
                        }
                        else
                        {
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PhysicalCountDetail.LineNumber);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", PhysicalCountDetail.PalletID);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", PhysicalCountDetail.LocationCode);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "RRDate", ddate);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", PhysicalCountDetail.BulkQty);
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", PhysicalCountDetail.ActualQty);
                 
                            Gears.CreateData(DT4,Conn);
                        }
                    }
                }


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.PhysicalCountDetail", "cond", "DocNumber", PhysicalCountDetail.DocNumber);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "cond", "LineNumber", PhysicalCountDetail.LineNumber);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "ItemCode", PhysicalCountDetail.ItemCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "ColorCode", PhysicalCountDetail.ColorCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "ClassCode", PhysicalCountDetail.ClassCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "SizeCode", PhysicalCountDetail.SizeCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "BulkQty", PhysicalCountDetail.BulkQty);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "BulkUnit", PhysicalCountDetail.BulkUnit);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "LocationCode", PhysicalCountDetail.LocationCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "PalletID", PhysicalCountDetail.PalletID);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Unit", PhysicalCountDetail.Unit);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "ActualQty", PhysicalCountDetail.ActualQty);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "StatusCode", PhysicalCountDetail.StatusCode);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "BarcodeNo", PhysicalCountDetail.BarcodeNo);

                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field1", PhysicalCountDetail.Field1);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field2", PhysicalCountDetail.Field2);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field3", PhysicalCountDetail.Field3);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field4", PhysicalCountDetail.Field4);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field5", PhysicalCountDetail.Field5);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field6", PhysicalCountDetail.Field6);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field7", PhysicalCountDetail.Field7);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field8", PhysicalCountDetail.Field8);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "set", "Field9", PhysicalCountDetail.Field9);

                Gears.UpdateData(DT1,Conn);

                DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", PhysicalCountDetail.DocNumber);
                DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PhysicalCountDetail.LineNumber);
                DT2.Rows.Add("WMS.CountSheetSubsi", "set", "ToLoc", PhysicalCountDetail.LocationCode);

                Gears.UpdateData(DT2,Conn);
            }
            public void DeletePhysicalCountDetail(PhysicalCountDetail PhysicalCountDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.PhysicalCountDetail", "cond", "DocNumber", PhysicalCountDetail.DocNumber);
                DT1.Rows.Add("WMS.PhysicalCountDetail", "cond", "LineNumber", PhysicalCountDetail.LineNumber);
                
                Gears.DeleteData(DT1,Conn);
                
                DataTable count = Gears.RetriveData2("select * from WMS.PhysicalCountDetail where docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.PhysicalCountDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.PhysicalCountDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", PhysicalCountDetail.DocNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PhysicalCountDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);
                Gears.DeleteData(DT3,Conn);

            }
        }

        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;
            
            a = Gears.RetriveData2("select * from WMS.PhysicalCount where DocNumber = '" + DocNumber + "'",Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
		Type= dtRow["Type"].ToString();
                StorageType = dtRow["StorageType"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
  		PlantCode= dtRow["PlantCode"].ToString();
  		RoomCode = dtRow["RoomCode"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                CountTag = dtRow["CountTag"].ToString();
                CountDate = dtRow["CountDate"].ToString();
                Status = dtRow["Status"].ToString();
                Remarks = dtRow["Remarks"].ToString();

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
            }

            return a;
        }
        public void InsertData(PhysicalCount _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.PhysicalCount", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "DocDate", _ent.DocDate);
  	    DT1.Rows.Add("WMS.PhysicalCount", "0", "Type", _ent.Type);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "StorageType", _ent.StorageType);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "WarehouseCode", _ent.WarehouseCode);
 	    DT1.Rows.Add("WMS.PhysicalCount", "0", "PlantCode", _ent.PlantCode);
 	    DT1.Rows.Add("WMS.PhysicalCount", "0", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "CountTag", _ent.CountTag);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "CountDate", _ent.CountDate);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Status", _ent.Status);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.PhysicalCount", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1,_ent.Connection);
        }

        public void UpdateData(PhysicalCount _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transtype;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.PhysicalCount", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "DocDate", _ent.DocDate);
  	       DT1.Rows.Add("WMS.PhysicalCount", "set", "Type", _ent.Type);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "StorageType", _ent.StorageType);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "WarehouseCode", _ent.WarehouseCode);
 	        DT1.Rows.Add("WMS.PhysicalCount", "set", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "CountTag", _ent.CountTag);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "CountDate", _ent.CountDate);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Status", _ent.Status);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "LastEditedDate", _ent.LastEditedDate);

            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.PhysicalCount", "set", "Field9", _ent.Field9);


            string strErr = Gears.UpdateData(DT1,_ent.Connection);

            Functions.AuditTrail("WMSPHC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }

        public void Deletedata(PhysicalCount _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.PhysicalCount", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSPHC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
    }
}
