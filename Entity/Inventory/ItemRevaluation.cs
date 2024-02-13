using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ItemRevaluation
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string RefNumber { get; set; }
        public virtual string Remarks { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual IList<ItemRevaluationDetail> Detail { get; set; }
                
        public class ItemRevaluationDetail
        {
            public virtual ItemRevaluation Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal OnHandQty { get; set; }
            public virtual decimal OldCost { get; set; }
            public virtual decimal NewCost { get; set; }
            public virtual bool IsByBulk { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal UnitFactor { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }
            
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT  A.DocNumber,A.LineNumber,A.ItemCode,A.ColorCode,A.ClassCode,A.SizeCode,A.Unit,A.OnHandQty,OldCost,NewCost,BaseQty,UnitFactor,BarcodeNo,StatusCode,A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7,A.Field8,A.Field9,Version,AverageCost,b.FullDesc FROM Inventory.ItemRevaluationDetail  a left join masterfile.item b on a.ItemCode = b.ItemCode  WHERE DocNumber = '" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddItemRevaluationDetail(ItemRevaluationDetail ItemRevaluationDetail)
            {

                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Inventory.ItemRevaluationDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "ItemCode", ItemRevaluationDetail.ItemCode);

                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "ColorCode", ItemRevaluationDetail.ColorCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "ClassCode", ItemRevaluationDetail.ClassCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "SizeCode", ItemRevaluationDetail.SizeCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Unit", ItemRevaluationDetail.Unit);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "OnHandQty", ItemRevaluationDetail.OnHandQty);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "NewCost", ItemRevaluationDetail.NewCost);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "OldCost", ItemRevaluationDetail.OldCost);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "BaseQty", ItemRevaluationDetail.BaseQty);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "UnitFactor", ItemRevaluationDetail.UnitFactor);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "BarcodeNo", ItemRevaluationDetail.BarcodeNo);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "StatusCode", ItemRevaluationDetail.StatusCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field1", ItemRevaluationDetail.Field1);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field2", ItemRevaluationDetail.Field2);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field3", ItemRevaluationDetail.Field3);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field4", ItemRevaluationDetail.Field4);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field5", ItemRevaluationDetail.Field5);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field6", ItemRevaluationDetail.Field6);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field7", ItemRevaluationDetail.Field7);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field8", ItemRevaluationDetail.Field8);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Field9", ItemRevaluationDetail.Field9);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "0", "Version", "1");

                DT2.Rows.Add("Inventory.ItemRevaluation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Inventory.ItemRevaluation", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                //DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode = '" + ItemRevaluationDetail.ItemCode + "'");
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= ItemRevaluationDetail.BulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVREV");
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemRevaluationDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemRevaluationDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemRevaluationDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemRevaluationDetail.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                //        Gears.CreateData(DT4);
                //        DT4.Rows.Clear();
                //    }
                //}
                //else
                //{
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVREV");
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemRevaluationDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemRevaluationDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemRevaluationDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemRevaluationDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    Gears.CreateData(DT4);
                //}
            }
            public void UpdateItemRevaluationDetail(ItemRevaluationDetail ItemRevaluationDetail)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("SELECT BulkQty FROM Inventory.ItemRevaluationDetail WHERE DocNumber = '" + Docnum + "' " +
                //"AND LineNumber = '" + ItemRevaluationDetail.LineNumber + "'");
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != ItemRevaluationDetail.BulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemRevaluationDetail.LineNumber);
                //        Gears.DeleteData(DT3);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode  = '" + ItemRevaluationDetail.ItemCode + "'");
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= ItemRevaluationDetail.BulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVREV");
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", ItemRevaluationDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemRevaluationDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemRevaluationDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemRevaluationDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemRevaluationDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);

                //                Gears.CreateData(DT4);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVREV");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", ItemRevaluationDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", ItemRevaluationDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", ItemRevaluationDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", ItemRevaluationDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", ItemRevaluationDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //            Gears.CreateData(DT4);
                //        }
                //    }
                //}

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "cond", "LineNumber", ItemRevaluationDetail.LineNumber);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "ItemCode", ItemRevaluationDetail.ItemCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "FullDesc", ItemRevaluationDetail.FullDesc);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "ColorCode", ItemRevaluationDetail.ColorCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "ClassCode", ItemRevaluationDetail.ClassCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "SizeCode", ItemRevaluationDetail.SizeCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Unit", ItemRevaluationDetail.Unit);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "OnHandQty", ItemRevaluationDetail.OnHandQty);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "OldCost", ItemRevaluationDetail.OldCost);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "NewCost", ItemRevaluationDetail.NewCost);
                //DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "IsByBulk", ItemRevaluationDetail.IsByBulk);
                //DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "BulkQty", ItemRevaluationDetail.BulkQty);
                //DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "BulkUnit", ItemRevaluationDetail.BulkUnit);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "BaseQty", ItemRevaluationDetail.BaseQty);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "UnitFactor", ItemRevaluationDetail.UnitFactor);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "BarcodeNo", ItemRevaluationDetail.BarcodeNo);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "StatusCode", ItemRevaluationDetail.StatusCode);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field1", ItemRevaluationDetail.Field1);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field2", ItemRevaluationDetail.Field2);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field3", ItemRevaluationDetail.Field3);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field4", ItemRevaluationDetail.Field4);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field5", ItemRevaluationDetail.Field5);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field6", ItemRevaluationDetail.Field6);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field7", ItemRevaluationDetail.Field7);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field8", ItemRevaluationDetail.Field8);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Field9", ItemRevaluationDetail.Field9);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteItemRevaluationDetail(ItemRevaluationDetail ItemRevaluationDetail)
            {                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ItemRevaluationDetail", "cond", "LineNumber", ItemRevaluationDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                //DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                //DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", ItemRevaluationDetail.LineNumber);
                //Gears.DeleteData(DT3);

                DataTable count = Gears.RetriveData2("SELECT * FROM Inventory.ItemRevaluationDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Inventory.ItemRevaluation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Inventory.ItemRevaluation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }

 

        public class RefTransaction
        {
            public virtual ReceivingReport Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='INVREV' OR  A.TransType='INVREV') ", Conn);
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

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Inventory.ItemRevaluation WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                RefNumber = dtRow["RefNumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);
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
        public void InsertData(ItemRevaluation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "DocNumber", Docnum);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.ItemRevaluation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ItemRevaluation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.ItemRevaluation", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.ItemRevaluation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("INVREV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ItemRevaluation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.ItemRevaluation", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Inventory.ItemRevaluationDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("INVREV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }        
        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Inventory.ItemRevaluationDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }
        public void UpdateUnitFactor(string DocNum, string Conn)
        {
            DataTable b = Gears.RetriveData2("UPDATE A "
                + " SET A.UnitFactor = ISNULL(C.ConversionFactor ,1) "
                + " FROM Inventory.ItemRevaluationDetail  A "
                + " INNER JOIN Masterfile.Item B "
                + " ON A.ItemCode = B.ItemCode "
                + " LEFT JOIN Masterfile.UnitConversion C "
                + " ON A.Unit = C.UnitCodeFrom "
                + " AND B.UnitBase = C.UnitCodeTo "
                + " WHERE A.DocNumber = '" + DocNum + "'", Conn);
        }

        public class JournalEntry
        {
            public virtual ItemRevaluation Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='INVREV' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
    }
}
