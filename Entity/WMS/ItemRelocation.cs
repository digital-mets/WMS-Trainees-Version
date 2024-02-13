using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ItemRelocation
    {
       
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string trans;

        private static string ddate;
        public virtual string TransType { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string StorageType { get; set; }
        public virtual string RelocationType { get; set; }
        
        public virtual string CustomerCode { get; set; }
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
        public virtual IList<ItemRelocationDetail> Detail { get; set; }


        public class ItemRelocationDetail
        {
            public virtual ItemRelocation Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string PalletID { get; set; }
            public virtual string ToPalletID { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Unit { get; set; }
            public virtual string FromLoc { get; set; }
            public virtual string ToLoc { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual string BatchNumber { get; set; }
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
                    a = Gears.RetriveData2("select * from WMS.ItemRelocationDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddItemRelocationDetail(ItemRelocationDetail ItemRelocationDetail)
            {
                int linenum = 0;
                bool isbybulk=false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.ItemRelocationDetail where docnumber = '" + Docnum + "'"
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
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "ItemCode", ItemRelocationDetail.ItemCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "ColorCode", ItemRelocationDetail.ColorCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "ClassCode", ItemRelocationDetail.ClassCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "SizeCode", ItemRelocationDetail.SizeCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "PalletID", ItemRelocationDetail.PalletID);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "ToPalletID", ItemRelocationDetail.ToPalletID);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "BulkQty", ItemRelocationDetail.BulkQty);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "BulkUnit", ItemRelocationDetail.BulkUnit);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Qty", ItemRelocationDetail.Qty);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Unit", ItemRelocationDetail.Unit);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "FromLoc", ItemRelocationDetail.FromLoc);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "ToLoc", ItemRelocationDetail.ToLoc);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "StatusCode", ItemRelocationDetail.StatusCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "BaseQty", ItemRelocationDetail.BaseQty);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "BarcodeNo", ItemRelocationDetail.BarcodeNo);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "BatchNumber", ItemRelocationDetail.BatchNumber);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field1", ItemRelocationDetail.Field1);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field2", ItemRelocationDetail.Field2);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field3", ItemRelocationDetail.Field3);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field4", ItemRelocationDetail.Field4);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field5", ItemRelocationDetail.Field5);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field6", ItemRelocationDetail.Field6);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field7", ItemRelocationDetail.Field7);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field8", ItemRelocationDetail.Field8);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "0", "Field9", ItemRelocationDetail.Field9);

                DT2.Rows.Add("WMS.ItemRelocation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.ItemRelocation", "set", "IsWithDetail", "True");
                DT2.Rows.Add("WMS.ItemRelocation", "set", "IsValidated", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);
                //from ERA
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                DataTable CS = Gears.RetriveData2("Select ISNULL(IsByBulk,0) as IsByBulk from masterfile.item where itemcode = '" + ItemRelocationDetail.ItemCode + "'",Conn);
                foreach (DataRow dt in CS.Rows)
                {
                    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                }
                if (isbybulk == true)
                {
                    string strline2;
                    for (int i = 1; i <= ItemRelocationDetail.BulkQty; i++)
                    {
                        strline2 = i.ToString().PadLeft(5,'0');

                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strline2);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemRelocationDetail.ItemCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemRelocationDetail.ColorCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemRelocationDetail.ClassCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemRelocationDetail.SizeCode);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", ItemRelocationDetail.PalletID);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ToPalletID", ItemRelocationDetail.ToPalletID);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", ItemRelocationDetail.FromLoc);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ToLoc", ItemRelocationDetail.ToLoc);
                        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", "1");


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
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemRelocationDetail.ItemCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemRelocationDetail.ColorCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemRelocationDetail.ClassCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemRelocationDetail.SizeCode);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", ItemRelocationDetail.PalletID);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ToPalletID", ItemRelocationDetail.ToPalletID);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", ItemRelocationDetail.FromLoc);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ToLoc", ItemRelocationDetail.ToLoc);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", ItemRelocationDetail.BulkQty);
                    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", ItemRelocationDetail.Qty);
                    Gears.CreateData(DT4,Conn);
                }
            }
            public void UpdateItemRelocationDetail(ItemRelocationDetail ItemRelocationDetail)
            {


                ////////
                bool isbybulk = false;

                DataTable dtable = Gears.RetriveData2("Select SUM(isnull(BulkQty,0)) as BulkQty,FromLoc,ToLoc from wms.ItemrelocationDetail where docnumber = '" + ItemRelocationDetail.DocNumber + "' " +
                "and LineNumber = '" + ItemRelocationDetail.LineNumber + "' group by FromLoc,ToLoc",Conn);
                foreach (DataRow dtrow in dtable.Rows)
                {
                    DataTable CS = Gears.RetriveData2("Select isnull(IsByBulk,0) as IsByBulk from masterfile.item where itemcode = '" + ItemRelocationDetail.ItemCode + "'"
                        ,Conn);
                    foreach (DataRow dt in CS.Rows)
                    {
                        isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                    }
                    if (isbybulk == true)
                    {


                        if ((Convert.ToDecimal(dtrow["BulkQty"].ToString()) != ItemRelocationDetail.BulkQty) || (dtrow["FromLoc"].ToString() != ItemRelocationDetail.FromLoc) || (dtrow["ToLoc"].ToString() != ItemRelocationDetail.ToLoc))
                        {
                            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                            DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", ItemRelocationDetail.DocNumber);
                            DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemRelocationDetail.LineNumber);
                            Gears.DeleteData(DT3,Conn);

                            Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                            for (int i = 1; i <= ItemRelocationDetail.BulkQty; i++)
                            {
                                string strLine2 = i.ToString().PadLeft(5, '0');

                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", ItemRelocationDetail.LineNumber);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemRelocationDetail.ItemCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemRelocationDetail.ColorCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemRelocationDetail.ClassCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemRelocationDetail.SizeCode);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", ItemRelocationDetail.PalletID);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ToPalletID", ItemRelocationDetail.ToPalletID);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", ItemRelocationDetail.FromLoc);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ToLoc", ItemRelocationDetail.ToLoc);
                                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", "1");


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
                        DT5.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemRelocationDetail.LineNumber);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "cond", "LineNumber", "00001");
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ItemCode", ItemRelocationDetail.ItemCode);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ColorCode", ItemRelocationDetail.ColorCode);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ClassCode", ItemRelocationDetail.ClassCode);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "SizeCode", ItemRelocationDetail.SizeCode);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "PalletID", ItemRelocationDetail.PalletID);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ToPalletID", ItemRelocationDetail.ToPalletID);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "DocDate", ddate);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "Location", ItemRelocationDetail.FromLoc);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "ToLoc", ItemRelocationDetail.ToLoc);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "DocBulkQty", ItemRelocationDetail.BulkQty);
                        DT5.Rows.Add("WMS.CountSheetSubsi", "set", "UsedQty", ItemRelocationDetail.Qty);
                        Gears.UpdateData(DT5,Conn);
                        DT5.Rows.Clear();
                    }
                }

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ItemRelocationDetail", "cond", "DocNumber", ItemRelocationDetail.DocNumber);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "cond", "LineNumber", ItemRelocationDetail.LineNumber);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "ItemCode", ItemRelocationDetail.ItemCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "ColorCode", ItemRelocationDetail.ColorCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "ClassCode", ItemRelocationDetail.ClassCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "SizeCode", ItemRelocationDetail.SizeCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "PalletID", ItemRelocationDetail.PalletID);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "ToPalletID", ItemRelocationDetail.ToPalletID);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "BulkQty", ItemRelocationDetail.BulkQty);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "BulkUnit", ItemRelocationDetail.BulkUnit);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Qty", ItemRelocationDetail.Qty);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Unit", ItemRelocationDetail.Unit);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "FromLoc", ItemRelocationDetail.FromLoc);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "ToLoc", ItemRelocationDetail.ToLoc);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "StatusCode", ItemRelocationDetail.StatusCode);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "BaseQty", ItemRelocationDetail.BaseQty);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "BarcodeNo", ItemRelocationDetail.BarcodeNo);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "BatchNumber", ItemRelocationDetail.BatchNumber);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field1", ItemRelocationDetail.Field1);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field2", ItemRelocationDetail.Field2);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field3", ItemRelocationDetail.Field3);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field4", ItemRelocationDetail.Field4);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field5", ItemRelocationDetail.Field5);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field6", ItemRelocationDetail.Field6);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field7", ItemRelocationDetail.Field7);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field8", ItemRelocationDetail.Field8);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "set", "Field9", ItemRelocationDetail.Field9);

                Gears.UpdateData(DT1,Conn);
                ///////



            }

            public void DeleteItemRelocationDetail(ItemRelocationDetail ItemRelocationDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ItemRelocationDetail", "cond", "DocNumber", ItemRelocationDetail.DocNumber);
                DT1.Rows.Add("WMS.ItemRelocationDetail", "cond", "LineNumber", ItemRelocationDetail.LineNumber);


                Gears.DeleteData(DT1,Conn);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", ItemRelocationDetail.DocNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemRelocationDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);
                Gears.DeleteData(DT3,Conn);




                DataTable count = Gears.RetriveData2("select * from WMS.ItemRelocationDetail where docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.ItemRelocationDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.ItemRelocationDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }

            }
        }

        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select A.*  ,EU.FullName as  LastEditedBy1, SB.FullName as  SubmittedBy1,AU.FullName as AddedBy1 from WMS.ItemRelocation A LEFT JOIN IT.Users AU    ON A.AddedBy = AU.UserID    LEFT JOIN IT.Users EU    ON A.LastEditedBy = EU.UserID    LEFT JOIN IT.Users SB    ON A.SubmittedBy = SB.UserID  where DocNumber = '" + DocNumber + "'", Conn);


            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                StorageType = dtRow["StorageType"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                RelocationType = dtRow["RelocationType"].ToString();

                SubmittedBy = dtRow["SubmittedBy1"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                AddedBy = dtRow["AddedBy1"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy1"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();

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
            }
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as ExpectedDeliveryDate,'' as WarehouseCode,'' as StorerKey,'' as Field1"+
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(ItemRelocation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.ItemRelocation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "StorageType", _ent.StorageType);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "RelocationType", _ent.RelocationType);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("WMS.ItemRelocation", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field5", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field6", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field7", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field8", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "Field9", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "0", "IsWithDetail", "False");
            DT1.Rows.Add("WMS.ItemRelocation", "0", "IsValidated", "False");

            Gears.CreateData(DT1,_ent.Connection);
        }

        public void UpdateData(ItemRelocation _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.TransType;
            ddate=_ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.ItemRelocation", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "StorageType", _ent.StorageType);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "RelocationType", _ent.RelocationType);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field5", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field6", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field7", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field8", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "Field9", _ent.Field4);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.ItemRelocation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1,_ent.Connection);

            Functions.AuditTrail("WMSREL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }

        public void DeleteData(ItemRelocation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.ItemRelocation", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSREL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }

    }
}
