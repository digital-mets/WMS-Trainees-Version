using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ItemAdjustment
    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string trans;

        private static string ddate;
        public virtual string DocNumber { get; set; }
        public virtual string TransType { get; set; }
        public virtual string DocDate{ get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual decimal TotalAdjustment { get; set; }
        public virtual string  AdjustmentType { get; set; }
        public virtual string Remarks { get; set; }
	    public virtual string AddedBy{ get; set; }
	    public virtual string AddedDate{ get; set; }
	    public virtual string LastEditedBy{ get; set; }
	    public virtual string LastEditedDate{ get; set; }

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
        public virtual IList<ItemAdjustmentDetail> Detail { get; set; }


        public class ItemAdjustmentDetail 
        {
            public virtual ItemAdjustment Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal Qty{ get; set; }
            public virtual string Unit{ get; set; }
            public virtual string Location { get; set; }
            public virtual DateTime ExpiryDate { get; set; }
            public virtual DateTime Mkfgdate { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string Reason { get; set; }
            public virtual string StorageType { get; set; }
            public virtual string StorageSection { get; set; }
            public virtual string StorageBin { get; set; }
            public virtual string PalletNumber { get; set; }
	        public virtual decimal BaseQty{ get; set; }
	        public virtual string StatusCode{ get; set; }
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
                try {
                    a = Gears.RetriveData2("select * from WMS.ItemAdjustmentDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                return a;
                }
                catch (Exception e)
                {
                a = null;
                return a;
                }
            }

            public void AddItemAdjustmentDetail(ItemAdjustmentDetail ItemAdjustmentDetail)
            {
                int linenum = 0;
                bool isbybulk = false;
                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.ItemAdjustmentDetail where docnumber = '" + Docnum + "'"
                    ,Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch {
                    linenum = 1;  
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "ItemCode", ItemAdjustmentDetail.ItemCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "ColorCode", ItemAdjustmentDetail.ColorCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "ClassCode", ItemAdjustmentDetail.ClassCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "SizeCode", ItemAdjustmentDetail.SizeCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "BulkQty", ItemAdjustmentDetail.BulkQty);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "BulkUnit ", ItemAdjustmentDetail.BulkUnit);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Qty", ItemAdjustmentDetail.Qty);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Unit ", ItemAdjustmentDetail.Unit);
 		        DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Location", ItemAdjustmentDetail.Location);
		        DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Reason", ItemAdjustmentDetail.Reason);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "StorageType", ItemAdjustmentDetail.StorageType);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "ExpiryDate", ItemAdjustmentDetail.ExpiryDate);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Mkfgdate", ItemAdjustmentDetail.Mkfgdate);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "BatchNo", ItemAdjustmentDetail.BatchNo);
		        DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "PalletNumber", ItemAdjustmentDetail.PalletNumber);
		        DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "BaseQty", ItemAdjustmentDetail.BaseQty);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "StatusCode ", ItemAdjustmentDetail.StatusCode);
		        DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "BarcodeNo", ItemAdjustmentDetail.BarcodeNo);

                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field1", ItemAdjustmentDetail.Field1);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field2", ItemAdjustmentDetail.Field2);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field3", ItemAdjustmentDetail.Field3);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field4", ItemAdjustmentDetail.Field4);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field5", ItemAdjustmentDetail.Field5);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field6", ItemAdjustmentDetail.Field6);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field7", ItemAdjustmentDetail.Field7);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field8", ItemAdjustmentDetail.Field8);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "0", "Field9", ItemAdjustmentDetail.Field9);
                
                
                
                DT2.Rows.Add("WMS.ItemAdjustment", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.ItemAdjustment", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);


                DataTable CS = Gears.RetriveData2("Select Isnull(IsByBulk,0)  IsByBulk from masterfile.item where itemcode = '" + ItemAdjustmentDetail.ItemCode + "'",Conn);
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();



             
                foreach (DataRow dt in CS.Rows)
                {
                    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                }
                if (isbybulk == true)
                {

                    for (int i = 1; i <= ItemAdjustmentDetail.BulkQty; i++)
                    {
                        string strLine2 = i.ToString().PadLeft(5, '0');
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemAdjustmentDetail.ItemCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemAdjustmentDetail.ColorCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemAdjustmentDetail.ClassCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemAdjustmentDetail.SizeCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", ItemAdjustmentDetail.Location);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", ItemAdjustmentDetail.PalletNumber);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "RRdate", ddate);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ExpirationDate", ItemAdjustmentDetail.ExpiryDate);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "MfgDate", ItemAdjustmentDetail.Mkfgdate);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Field1", ItemAdjustmentDetail.BatchNo);
                        if (ItemAdjustmentDetail.Qty > 0)
                        {
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                        }
                        else
                        {
                            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", -1);
                        }
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
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemAdjustmentDetail.ItemCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemAdjustmentDetail.ColorCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemAdjustmentDetail.ClassCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemAdjustmentDetail.SizeCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", ItemAdjustmentDetail.Location);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", ItemAdjustmentDetail.PalletNumber);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "RRdate", ddate);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ExpirationDate", ItemAdjustmentDetail.ExpiryDate);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "MfgDate", ItemAdjustmentDetail.Mkfgdate);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Field1", ItemAdjustmentDetail.BatchNo);
                    if (ItemAdjustmentDetail.Qty > 0)
                    {
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", ItemAdjustmentDetail.BulkQty);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", ItemAdjustmentDetail.Qty);
                    }
                    else
                    {
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", ItemAdjustmentDetail.BulkQty * -1);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", ItemAdjustmentDetail.Qty );
                    }
                    Gears.CreateData(DT4,Conn);
                }
                //Gears.RetriveData2("Update wms.ItemAdjustment set TotalAdjustment=(Select sum(Qty) from wms.ItemAdjustmentDetail where Docnumber='" + Docnum + "' ) where Docnumber = '" + Docnum + "'");

            }
            public void UpdateItemAdjustmentDetail(ItemAdjustmentDetail ItemAdjustmentDetail)
            {
                bool isbybulk = false;

                DataTable dtable = Gears.RetriveData2("Select BulkQty,Location from wms.ItemAdjustmentDetail where docnumber = '" + ItemAdjustmentDetail.DocNumber + "' " +
                "and LineNumber = '" + ItemAdjustmentDetail.LineNumber + "'",Conn);
                foreach (DataRow dtrow in dtable.Rows)
                {

                    DataTable CS = Gears.RetriveData2("Select  Isnull(IsByBulk,0)  IsByBulk from masterfile.item where itemcode = '" + ItemAdjustmentDetail.ItemCode + "'"
                        ,Conn);
                    foreach (DataRow dt in CS.Rows)
                    {
                        isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                    }
                    if (isbybulk == true)
                    {

                    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != ItemAdjustmentDetail.BulkQty || (dtrow["Location"].ToString() != ItemAdjustmentDetail.Location))
                    {
                        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", ItemAdjustmentDetail.DocNumber);
                        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemAdjustmentDetail.LineNumber);
                        Gears.DeleteData(DT3,Conn);

                        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                       

                            for (int i = 1; i <= ItemAdjustmentDetail.BulkQty; i++)
                            {
                                string strLine2 = i.ToString().PadLeft(5, '0');
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", ItemAdjustmentDetail.LineNumber);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemAdjustmentDetail.ItemCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemAdjustmentDetail.ColorCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemAdjustmentDetail.ClassCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemAdjustmentDetail.SizeCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", ItemAdjustmentDetail.Location);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", ItemAdjustmentDetail.PalletNumber);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "RRdate", ddate);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ExpirationDate", ItemAdjustmentDetail.ExpiryDate);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "MfgDate", ItemAdjustmentDetail.Mkfgdate);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Field1", ItemAdjustmentDetail.BatchNo);
                               if (ItemAdjustmentDetail.Qty > 0 )
                               {
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                               }
                               else
                               {
                                   DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", -1);
                               }
                                Gears.CreateData(DT4,Conn);
                                DT4.Rows.Clear();
                            }
                        }
                  
                    }
                    else
                    {
                        Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                        DT5.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemAdjustmentDetail.LineNumber);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "cond", "LineNumber", "00001");
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ItemCode", ItemAdjustmentDetail.ItemCode);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ColorCode", ItemAdjustmentDetail.ColorCode);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ClassCode", ItemAdjustmentDetail.ClassCode);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "SizeCode", ItemAdjustmentDetail.SizeCode);

                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "DocDate", ddate);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "Location", ItemAdjustmentDetail.Location);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "PalletID", ItemAdjustmentDetail.PalletNumber);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ExpirationDate", ItemAdjustmentDetail.ExpiryDate);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "MfgDate", ItemAdjustmentDetail.Mkfgdate);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "Field1", ItemAdjustmentDetail.BatchNo);
     
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "RRdate", ddate);
                        if (ItemAdjustmentDetail.Qty > 0)
                        {
                            DT5.Rows.Add("WMS.CountSheetSubsi", "set", "DocBulkQty", ItemAdjustmentDetail.BulkQty);
                            DT5.Rows.Add("WMS.CountSheetSubsi", "set", "UsedQty", ItemAdjustmentDetail.Qty);
                        }
                        else
                        {
                            DT5.Rows.Add("WMS.CountSheetSubsi", "set", "DocBulkQty", ItemAdjustmentDetail.BulkQty * -1);
                            DT5.Rows.Add("WMS.CountSheetSubsi", "set", "UsedQty", ItemAdjustmentDetail.Qty);
                        }
                        Gears.UpdateData(DT5,Conn);
                        DT5.Rows.Clear();


                    }
                }


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "cond", "DocNumber", ItemAdjustmentDetail.DocNumber);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "cond", "LineNumber", ItemAdjustmentDetail.LineNumber);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "ItemCode", ItemAdjustmentDetail.ItemCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "ColorCode", ItemAdjustmentDetail.ColorCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "ClassCode", ItemAdjustmentDetail.ClassCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "SizeCode", ItemAdjustmentDetail.SizeCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "BulkQty", ItemAdjustmentDetail.BulkQty);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "BulkUnit ", ItemAdjustmentDetail.BulkUnit);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Qty", ItemAdjustmentDetail.Qty);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Unit ", ItemAdjustmentDetail.Unit);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Location", ItemAdjustmentDetail.Location);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Reason", ItemAdjustmentDetail.Reason);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "StorageType", ItemAdjustmentDetail.StorageType);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "PalletNumber", ItemAdjustmentDetail.PalletNumber);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "BaseQty", ItemAdjustmentDetail.BaseQty);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "StatusCode ", ItemAdjustmentDetail.StatusCode);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "BarcodeNo", ItemAdjustmentDetail.BarcodeNo);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "ExpiryDate", ItemAdjustmentDetail.ExpiryDate);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Mkfgdate", ItemAdjustmentDetail.Mkfgdate);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "BatchNo", ItemAdjustmentDetail.BatchNo);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field1", ItemAdjustmentDetail.Field1);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field2", ItemAdjustmentDetail.Field2);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field3", ItemAdjustmentDetail.Field3);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field4", ItemAdjustmentDetail.Field4);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field5", ItemAdjustmentDetail.Field5);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field6", ItemAdjustmentDetail.Field6);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field7", ItemAdjustmentDetail.Field7);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field8", ItemAdjustmentDetail.Field8);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "set", "Field9", ItemAdjustmentDetail.Field9);

                Gears.UpdateData(DT1,Conn);
            //    Gears.RetriveData2("Update wms.ItemAdjustment set TotalAdjustment=(Select sum(Qty) from wms.ItemAdjustmentDetail where Docnumber='" + Docnum + "' ) where Docnumber = '" + Docnum + "'");

            }
            public void DeleteItemAdjustmentDetail(ItemAdjustmentDetail ItemAdjustmentDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "cond", "DocNumber", ItemAdjustmentDetail.DocNumber);
                DT1.Rows.Add("WMS.ItemAdjustmentDetail", "cond", "LineNumber", ItemAdjustmentDetail.LineNumber);

                Gears.DeleteData(DT1,Conn);

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", ItemAdjustmentDetail.DocNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemAdjustmentDetail.LineNumber);
                Gears.DeleteData(DT3,Conn);


                DataTable count = Gears.RetriveData2("select * from WMS.ItemAdjustmentDetail where docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                DT2.Rows.Add("WMS.ItemAdjustment", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.ItemAdjustment", "set", "IsWithDetail", "False");
                Gears.UpdateData(DT2,Conn);
                }
                //Gears.RetriveData2("Update wms.ItemAdjustment set TotalAdjustment=(Select sum(ISNULL(Qty,0)) from wms.ItemAdjustmentDetail where Docnumber='" + Docnum + "' ) where Docnumber = '" + Docnum + "'");

            }  
        }

        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
                a = Gears.RetriveData2("select * from WMS.ItemAdjustment where DocNumber = '" + DocNumber + "'",Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
	                DocDate = dtRow["DocDate"].ToString();
		            WarehouseCode = dtRow["WarehouseCode"].ToString();
                    CustomerCode = dtRow["CustomerCode"].ToString(); 
                    TotalAdjustment = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAdjustment"]) ? 0 : dtRow["TotalAdjustment"]); 
                    AdjustmentType = dtRow["AdjustmentType"].ToString();
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
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as DocDate ,'' as WarehouseCode"+
            //    ",'' as TotalAdjustment,'' as AdjustmentType,'' as Remarks,'' as Field1,'' as Field2"+
            //    ",'' as Field3,'' as Field4,'' as Field5"+
            //    ",'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}
            
            return a;
        }
        public void InsertData(ItemAdjustment _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.ItemAdjustment", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "AdjustmentType", _ent.AdjustmentType);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "TotalAdjustment", _ent.TotalAdjustment);

            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "IsValidated", "False");
            DT1.Rows.Add("WMS.ItemAdjustment", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1,_ent.Connection);
        }

        public void UpdateData(ItemAdjustment _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.TransType;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.ItemAdjustment", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "AdjustmentType", _ent.AdjustmentType);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "TotalAdjustment", _ent.TotalAdjustment);
            
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.ItemAdjustment", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSIA", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }
        public void DeleteData(ItemAdjustment _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.ItemAdjustment", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSIA", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
    }
}
