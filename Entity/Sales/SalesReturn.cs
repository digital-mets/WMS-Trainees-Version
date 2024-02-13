using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;
namespace Entity
{
    public class SalesReturn
    {
        private static string Docnum;
        private static string trans;
        private static string ddate;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string TransType { get; set; }
        public virtual string Reason { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string ReferenceDRNo { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Currency { get; set; }
        public virtual string TotalQuantity { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual string CounterDocNo { get; set; }
        public virtual string RRDocNumber { get; set; }
        public virtual bool IsWithDR { get; set; }
        public virtual bool IsReclass { get; set; }
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
        public virtual IList<SalesReturnDetail> Detail { get; set; }
        public virtual IList<RefTransaction> Reference { get; set; }
      
        public class SalesReturnDetail
        {
            public virtual SalesReturn Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string SONumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal ReturnedQty { get; set; }
            public virtual decimal ReturnedBulkQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal UnitPrice { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal DiscountRate { get; set; }
            public virtual decimal AverageCost { get; set; }
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
                    a = Gears.RetriveData2("select * from Sales.SalesReturnDetail A inner join Masterfile.Item B on A.ItemCode = B.ItemCode where DocNumber='" + DocNumber + "'  order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSalesReturnDetail(SalesReturnDetail SalesReturnDetail)
            {

                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Sales.SalesReturnDetail where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "SONumber", SalesReturnDetail.SONumber);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "ItemCode", SalesReturnDetail.ItemCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "ColorCode", SalesReturnDetail.ColorCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "ClassCode", SalesReturnDetail.ClassCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "SizeCode", SalesReturnDetail.SizeCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "ReturnedQty ", SalesReturnDetail.ReturnedQty);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "ReturnedBulkQty ", SalesReturnDetail.ReturnedBulkQty);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Unit", SalesReturnDetail.Unit);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "UnitPrice", SalesReturnDetail.UnitPrice);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "StatusCode", SalesReturnDetail.StatusCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "DiscountRate", SalesReturnDetail.DiscountRate);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "AverageCost", SalesReturnDetail.AverageCost);


                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field1", SalesReturnDetail.Field1);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field2", SalesReturnDetail.Field2);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field3", SalesReturnDetail.Field3);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field4", SalesReturnDetail.Field4);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field5", SalesReturnDetail.Field5);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field6", SalesReturnDetail.Field6);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field7", SalesReturnDetail.Field7);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field8", SalesReturnDetail.Field8);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "Field9", SalesReturnDetail.Field9);

                if (SalesReturnDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "0", "ExpDate", SalesReturnDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "0", "ExpDate", null);
                }
                if (SalesReturnDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "0", "MfgDate", SalesReturnDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "BatchNo", SalesReturnDetail.BatchNo);
                DT1.Rows.Add("Sales.SalesReturnDetail", "0", "LotNo", SalesReturnDetail.LotNo);

                DT2.Rows.Add("Sales.SalesReturn", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Sales.SalesReturn", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                //DataTable CS = Gears.RetriveData2("Select IsByBulk from masterfile.item where itemcode = '" + SalesReturnDetail.ItemCode + "'", Conn);
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();




                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= SalesReturnDetail.ReturnedBulkQty; i++)
                //    {

                //        string strLine2 = i.ToString().PadLeft(5, '0');

                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", SalesReturnDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", SalesReturnDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", SalesReturnDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", SalesReturnDetail.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //        //DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", SalesReturnDetail.Location);
                //        //DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", SalesReturnDetail.PalletNumber);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                //        Gears.CreateData(DT4, Conn);
                //        DT4.Rows.Clear();
                //    }
                //}
                //else
                //{
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", SalesReturnDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", SalesReturnDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", SalesReturnDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", SalesReturnDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    //DT4.Rows.Add("WMS.CountSheetSubsi", "0", "Location", SalesReturnDetail.Location);
                //    //DT4.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", SalesReturnDetail.PalletNumber);
                //    //DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", SalesReturnDetail.BulkQty);
                //    Gears.CreateData(DT4, Conn);
                //}
           
            }
            public void UpdateSalesReturnDetail(SalesReturnDetail SalesReturnDetail)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("Select BulkQty from wms.SalesReturnDetail where docnumber = '" + SalesReturnDetail.DocNumber + "' " +
                //"and LineNumber = '" + SalesReturnDetail.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != SalesReturnDetail.ReturnedBulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", SalesReturnDetail.DocNumber);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", SalesReturnDetail.LineNumber);
                //        Gears.DeleteData(DT3, Conn);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("Select IsByBulk from masterfile.item where itemcode = '" + SalesReturnDetail.ItemCode + "'", Conn);
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= SalesReturnDetail.ReturnedBulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", SalesReturnDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", SalesReturnDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", SalesReturnDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", SalesReturnDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", SalesReturnDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                               
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);

                //                Gears.CreateData(DT4, Conn);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", SalesReturnDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", SalesReturnDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", SalesReturnDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", SalesReturnDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", SalesReturnDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //            Gears.CreateData(DT4, Conn);
                //        }
                //    }
                //}


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Sales.SalesReturnDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.SalesReturnDetail", "cond", "LineNumber", SalesReturnDetail.LineNumber);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "SONumber", SalesReturnDetail.SONumber);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "ItemCode", SalesReturnDetail.ItemCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "ColorCode", SalesReturnDetail.ColorCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "ClassCode", SalesReturnDetail.ClassCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "SizeCode", SalesReturnDetail.SizeCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "ReturnedQty ", SalesReturnDetail.ReturnedQty);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "ReturnedBulkQty ", SalesReturnDetail.ReturnedBulkQty);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Unit", SalesReturnDetail.Unit);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "UnitPrice", SalesReturnDetail.UnitPrice);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "StatusCode", SalesReturnDetail.StatusCode);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "DiscountRate", SalesReturnDetail.DiscountRate);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "AverageCost", SalesReturnDetail.AverageCost);

                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field1", SalesReturnDetail.Field1);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field2", SalesReturnDetail.Field2);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field3", SalesReturnDetail.Field3);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field4", SalesReturnDetail.Field4);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field5", SalesReturnDetail.Field5);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field6", SalesReturnDetail.Field6);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field7", SalesReturnDetail.Field7);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field8", SalesReturnDetail.Field8);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "Field9", SalesReturnDetail.Field9);

                if (SalesReturnDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "set", "ExpDate", SalesReturnDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "set", "ExpDate", null);
                }
                if (SalesReturnDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "set", "MfgDate", SalesReturnDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Sales.SalesReturnDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "BatchNo", SalesReturnDetail.BatchNo);
                DT1.Rows.Add("Sales.SalesReturnDetail", "set", "LotNo", SalesReturnDetail.LotNo);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteSalesReturnDetaill(SalesReturnDetail SalesReturnDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Sales.SalesReturnDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.SalesReturnDetail", "cond", "LineNumber", SalesReturnDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);



                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", SalesReturnDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "SLSSRN");
                Gears.DeleteData(DT3, Conn);


                DataTable count = Gears.RetriveData2("select * from Sales.SalesReturnDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Sales.SalesReturn", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Sales.SalesReturn", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

          

            }


        }
        public class RefTransaction
        {
            public virtual SalesReturn Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='SLSSRN' OR  A.TransType='SLSSRN') ", Conn);
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
            a = Gears.RetriveData2("select * from Sales.SalesReturn where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                Reason = dtRow["Reason"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                ReferenceDRNo = dtRow["ReferenceDRNo"].ToString();
                RRDocNumber = dtRow["RRDocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Currency = dtRow["Currency"].ToString();
                TotalQuantity = dtRow["TotalQuantity"].ToString(); ;

                TotalAmount =  Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                CounterDocNo = dtRow["CounterDocNo"].ToString();
        
   
                IsWithDR = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDR"]) ? false : dtRow["IsWithDR"]);
                IsReclass = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsReclass"]) ? false : dtRow["IsReclass"]);
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
       

            }
           

            return a;
        }
        public void InsertData(SalesReturn _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.SalesReturn", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Reason", _ent.Reason);
            DT1.Rows.Add("Sales.SalesReturn", "0", "CustomerCode ", _ent.CustomerCode);
            DT1.Rows.Add("Sales.SalesReturn", "0", "ReferenceDRNo ", _ent.ReferenceDRNo);
            DT1.Rows.Add("Sales.SalesReturn", "0", "RRDocNumber ", _ent.RRDocNumber);
            DT1.Rows.Add("Sales.SalesReturn", "0", "DocDate ", _ent.DocDate);
            DT1.Rows.Add("Sales.SalesReturn", "0", "WarehouseCode ", _ent.WarehouseCode);

            DT1.Rows.Add("Sales.SalesReturn", "0", "Remarks ", _ent.Remarks);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Currency ", _ent.Currency);
            DT1.Rows.Add("Sales.SalesReturn", "0", "TotalQuantity ", _ent.TotalQuantity);
            DT1.Rows.Add("Sales.SalesReturn", "0", "TotalAmount ", _ent.TotalAmount);
            DT1.Rows.Add("Sales.SalesReturn", "0", "TotalBulkQty ", _ent.TotalBulkQty);
            DT1.Rows.Add("Sales.SalesReturn", "0", "CounterDocNo ", _ent.CounterDocNo);
            DT1.Rows.Add("Sales.SalesReturn", "0", "IsWithDR ", _ent.IsWithDR);
            DT1.Rows.Add("Sales.SalesReturn", "0", "IsReclass ", _ent.IsReclass);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SalesReturn", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Sales.SalesReturn", "0", "SubmittedBy","");
            DT1.Rows.Add("Sales.SalesReturn", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(SalesReturn _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.TransType;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.SalesReturn", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Reason", _ent.Reason);
            DT1.Rows.Add("Sales.SalesReturn", "set", "CustomerCode ", _ent.CustomerCode);
            DT1.Rows.Add("Sales.SalesReturn", "set", "ReferenceDRNo ", _ent.ReferenceDRNo);
            DT1.Rows.Add("Sales.SalesReturn", "set", "RRDocNumber ", _ent.RRDocNumber);
            DT1.Rows.Add("Sales.SalesReturn", "set", "DocDate ", _ent.DocDate);
            DT1.Rows.Add("Sales.SalesReturn", "set", "WarehouseCode ", _ent.WarehouseCode);
            
            DT1.Rows.Add("Sales.SalesReturn", "set", "Remarks ", _ent.Remarks);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Currency ", _ent.Currency);
            DT1.Rows.Add("Sales.SalesReturn", "set", "TotalQuantity ", _ent.TotalQuantity);
            DT1.Rows.Add("Sales.SalesReturn", "set", "TotalAmount ", _ent.TotalAmount);
            DT1.Rows.Add("Sales.SalesReturn", "set", "TotalBulkQty ", _ent.TotalBulkQty);
            DT1.Rows.Add("Sales.SalesReturn", "set", "CounterDocNo ", _ent.CounterDocNo);
            DT1.Rows.Add("Sales.SalesReturn", "set", "IsWithDR ", _ent.IsWithDR );
            DT1.Rows.Add("Sales.SalesReturn", "set", "IsReclass ", _ent.IsReclass);

            
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SalesReturn", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Sales.SalesReturn", "set", "SubmittedBy", "");
            DT1.Rows.Add("Sales.SalesReturn", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Sales.SalesReturn", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("SLSSRN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(SalesReturn _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.SalesReturn", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSSRN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }


    
}
