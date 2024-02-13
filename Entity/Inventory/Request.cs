using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Request
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string Transaction { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string DeliveryDate { get; set; }
        public virtual string TransType { get; set; }
        //public virtual string Type { get; set; }
        public virtual string RequestedBy { get; set; }
        //public virtual string Status { get; set; }
        public virtual string CostCenter { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string RefIssuance { get; set; }
        public virtual Boolean IsPrinted { get; set; }
        public virtual Boolean IsValidated { get; set; }
        public virtual Boolean IsWithDetail { get; set; }

        //Samples Tab
        public virtual Boolean WithDIS { get; set; }
        public virtual string DISNumber { get; set; }

        //Complimentary Tab
        public virtual string ComplimentaryType { get; set; }
        public virtual string RefDoc { get; set; }
        public virtual string AuthorizedBy { get; set; }
        public virtual string AuthorizedDate { get; set; }

        //JO Tab
        public virtual Boolean IsCharge { get; set; }
        public virtual Boolean IsReplacement { get; set; }
        public virtual Boolean IsIssued { get; set; }
        public virtual string IssuingWarehouse { get; set; }
        public virtual string RefJONumber { get; set; }
        public virtual string RefJOStep { get; set; }
        public virtual string MaterialType { get; set; }
        public virtual string SpecificMaterialType { get; set; }
        public virtual string WorkCenter { get; set; }

        //Audit Trail
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string ForceClosedBy { get; set; }
        public virtual string ForceClosedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }

        //User defined fields
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public virtual IList<RequestDetail> Detail { get; set; }

        public class RequestDetail
        {
            public virtual Request Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal ItemPrice { get; set; }
            public virtual decimal RequestedQty { get; set; }
            public virtual decimal RequestedBulkQty { get; set; }
            public virtual decimal IssuedQty { get; set; }
            public virtual string RequestedBulkUnit { get; set; }
            public virtual decimal ReturnedQty { get; set; }
            public virtual decimal ReturnedBulkQty { get; set; }
            public virtual decimal Cost { get; set; }
            
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

            public virtual string Version { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Inventory.RequestDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRequestDetail(RequestDetail A)
            {
                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Inventory.RequestDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Inventory.RequestDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "ItemCode", A.ItemCode);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "FullDesc", A.FullDesc);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "ColorCode", A.ColorCode);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "ClassCode", A.ClassCode);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "SizeCode", A.SizeCode);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Unit", A.Unit);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "ItemPrice", A.ItemPrice);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "RequestedQty", A.RequestedQty);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "RequestedBulkQty", A.RequestedBulkQty);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "IssuedQty", A.IssuedQty);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "RequestedBulkUnit", A.RequestedBulkUnit);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "ReturnedQty", A.ReturnedQty);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "ReturnedBulkQty", A.ReturnedBulkQty);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Cost", A.Cost);

                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field1", A.Field1);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field2", A.Field2);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field3", A.Field3);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field4", A.Field4);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field5", A.Field5);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field6", A.Field6);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field7", A.Field7);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field8", A.Field8);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "Field9", A.Field9);
                if (A.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "0", "ExpDate", A.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "0", "ExpDate", null);
                }
                if (A.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "0", "MfgDate", A.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.RequestDetail", "0", "BatchNo", A.BatchNo);
                DT1.Rows.Add("Inventory.RequestDetail", "0", "LotNo", A.LotNo);

                DT1.Rows.Add("Inventory.RequestDetail", "0", "Version", "1");

                DT2.Rows.Add("Inventory.Request", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Inventory.Request", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                //DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode = '" + A.ItemCode + "'", Conn);
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= A.RequestedBulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", A.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", A.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", A.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", A.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", "1");
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
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", A.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", A.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", A.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", A.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    Gears.CreateData(DT4, Conn);
                //}
            }
            public void UpdateRequestDetail(RequestDetail B)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("SELECT RequestedBulkQty FROM Inventory.RequestDetail WHERE DocNumber = '" + Docnum + "' " +
                //"AND LineNumber = '" + B.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["RequestedBulkQty"].ToString()) != B.RequestedBulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", B.LineNumber);
                //        Gears.DeleteData(DT3, Conn);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode  = '" + B.ItemCode + "'", Conn);
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= B.RequestedBulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", B.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", B.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", B.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", B.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", B.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", "1");

                //                Gears.CreateData(DT4, Conn);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", B.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", B.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", B.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", B.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", B.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //            Gears.CreateData(DT4, Conn);
                //        }
                //    }
                //}

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.RequestDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.RequestDetail", "cond", "LineNumber", B.LineNumber);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "ItemCode", B.ItemCode);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "FullDesc", B.FullDesc);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "ColorCode", B.ColorCode);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "ClassCode", B.ClassCode);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "SizeCode", B.SizeCode);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Unit", B.Unit);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "ItemPrice", B.ItemPrice);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "RequestedQty", B.RequestedQty);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "RequestedBulkQty", B.RequestedBulkQty);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "IssuedQty", B.IssuedQty);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "RequestedBulkUnit", B.RequestedBulkUnit);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "ReturnedQty", B.ReturnedQty);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "ReturnedBulkQty", B.ReturnedBulkQty);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Cost", B.Cost);
                
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field1", B.Field1);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field2", B.Field2);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field3", B.Field3);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field4", B.Field4);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field5", B.Field5);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field6", B.Field6);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field7", B.Field7);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field8", B.Field8);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "Field9", B.Field9);
                if (B.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "set", "ExpDate", B.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "set", "ExpDate", null);
                }
                if (B.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "set", "MfgDate", B.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.RequestDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.RequestDetail", "set", "BatchNo", B.BatchNo);
                DT1.Rows.Add("Inventory.RequestDetail", "set", "LotNo", B.LotNo);

                DT1.Rows.Add("Inventory.RequestDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);           
                 
            }
            public void DeleteRequestDetail(RequestDetail C)
            {               
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.RequestDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.RequestDetail", "cond", "LineNumber", C.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", C.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Inventory.RequestDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Inventory.Request", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Inventory.Request", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }
        public class RefTransaction
        {
            public virtual Request Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='" + TransType + "' OR  A.TransType='" + TransType + "') ", Conn);
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

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Inventory.Request WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                //header
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                DeliveryDate = dtRow["DeliveryDate"].ToString();
                TransType = dtRow["TransType"].ToString();
                //Type = dtRow["Type"].ToString();
                RequestedBy = dtRow["RequestedBy"].ToString();
                //Status = dtRow["Status"].ToString();
                CostCenter = dtRow["CostCenter"].ToString();
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                Remarks = dtRow["Remarks"].ToString();
                RefIssuance = dtRow["RefIssuance"].ToString();
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);

                //samples 
                WithDIS = Convert.ToBoolean(Convert.IsDBNull(dtRow["WithDIS"]) ? false : dtRow["WithDIS"]);
                DISNumber = dtRow["DISNumber"].ToString();
                
                //complimentary
                ComplimentaryType = dtRow["ComplimentaryType"].ToString();
                RefDoc = dtRow["RefDoc"].ToString();
                AuthorizedBy = dtRow["AuthorizedBy"].ToString();
                AuthorizedDate = dtRow["AuthorizedDate"].ToString();

                //JO
                IsCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsCharge"]) ? false : dtRow["IsCharge"]);
                IsReplacement = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsReplacement"]) ? false : dtRow["IsReplacement"]);
                IsIssued = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsIssued"]) ? false : dtRow["IsIssued"]);
                IssuingWarehouse = dtRow["IssuingWarehouse"].ToString();
                RefJONumber = dtRow["RefJONumber"].ToString();
                RefJOStep = dtRow["RefJOStep"].ToString();
                MaterialType = dtRow["MaterialType"].ToString();
                SpecificMaterialType = dtRow["SpecificMaterialType"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();

                //user defined
                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();

                //audit trail
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                ForceClosedBy = dtRow["ForceClosedBy"].ToString();
                ForceClosedDate = dtRow["ForceClosedDate"].ToString();
            }

            return a;
        }
        public void InsertData(Request _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            trans = _ent.Transaction;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.Request", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.Request", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.Request", "0", "DeliveryDate", _ent.DeliveryDate);
            DT1.Rows.Add("Inventory.Request", "0", "TransType", _ent.TransType);
            //DT1.Rows.Add("Inventory.Request", "0", "Type", _ent.Type);
            DT1.Rows.Add("Inventory.Request", "0", "RequestedBy", _ent.RequestedBy);
            //DT1.Rows.Add("Inventory.Request", "0", "Status", "N");
            DT1.Rows.Add("Inventory.Request", "0", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Inventory.Request", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.Request", "0", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.Request", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.Request", "0", "RefIssuance", _ent.RefIssuance);
            DT1.Rows.Add("Inventory.Request", "0", "IsPrinted", _ent.IsPrinted);

            //samples
            DT1.Rows.Add("Inventory.Request", "0", "WithDis", _ent.WithDIS);
            DT1.Rows.Add("Inventory.Request", "0", "DISNumber", _ent.DISNumber);

            //complimentary
            DT1.Rows.Add("Inventory.Request", "0", "ComplimentaryType", _ent.ComplimentaryType);
            DT1.Rows.Add("Inventory.Request", "0", "RefDoc", _ent.RefDoc);
            //DT1.Rows.Add("Inventory.Request", "0", "AuthorizedBy", _ent.AuthorizedBy);
            //DT1.Rows.Add("Inventory.Request", "0", "AuthorizedDate", _ent.AuthorizedDate);

            //JO
            DT1.Rows.Add("Inventory.Request", "0", "IsCharge", _ent.IsCharge);
            DT1.Rows.Add("Inventory.Request", "0", "IsReplacement", _ent.IsReplacement);
            DT1.Rows.Add("Inventory.Request", "0", "IsIssued", _ent.IsIssued);
            DT1.Rows.Add("Inventory.Request", "0", "IssuingWarehouse", _ent.IssuingWarehouse);
            DT1.Rows.Add("Inventory.Request", "0", "RefJONumber", _ent.RefJONumber);
            DT1.Rows.Add("Inventory.Request", "0", "RefJOStep", _ent.RefJOStep);
            DT1.Rows.Add("Inventory.Request", "0", "MaterialType", _ent.MaterialType);
            DT1.Rows.Add("Inventory.Request", "0", "SpeciificMaterialType", _ent.SpecificMaterialType);
            DT1.Rows.Add("Inventory.Request", "0", "WorkCenter", _ent.WorkCenter);

            //user defined
            DT1.Rows.Add("Inventory.Request", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.Request", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.Request", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.Request", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.Request", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.Request", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.Request", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.Request", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.Request", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.Request", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.Request", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(Request _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.Request", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.Request", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.Request", "set", "DeliveryDate", _ent.DeliveryDate);
            DT1.Rows.Add("Inventory.Request", "set", "TransType", _ent.TransType);
            //DT1.Rows.Add("Inventory.Request", "set", "Type", _ent.Type);
            DT1.Rows.Add("Inventory.Request", "set", "RequestedBy", _ent.RequestedBy);
            //DT1.Rows.Add("Inventory.Request", "set", "Status", _ent.Status);
            DT1.Rows.Add("Inventory.Request", "set", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Inventory.Request", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.Request", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.Request", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.Request", "set", "RefIssuance", _ent.RefIssuance);
            DT1.Rows.Add("Inventory.Request", "set", "IsPrinted", _ent.IsPrinted);

            //samples
            DT1.Rows.Add("Inventory.Request", "set", "WithDIS", _ent.WithDIS);
            DT1.Rows.Add("Inventory.Request", "set", "DISNumber", _ent.DISNumber);

            //complimentary
            DT1.Rows.Add("Inventory.Request", "set", "ComplimentaryType", _ent.ComplimentaryType);
            DT1.Rows.Add("Inventory.Request", "set", "RefDoc", _ent.RefDoc);
           // DT1.Rows.Add("Inventory.Request", "set", "AuthorizedBy", _ent.AuthorizedBy);
            //DT1.Rows.Add("Inventory.Request", "set", "AuthorizedDate", _ent.AuthorizedDate);

            //JO
            DT1.Rows.Add("Inventory.Request", "set", "IsCharge", _ent.IsCharge);
            DT1.Rows.Add("Inventory.Request", "set", "IsReplacement", _ent.IsReplacement);
            DT1.Rows.Add("Inventory.Request", "set", "IsIssued", _ent.IsIssued);
            DT1.Rows.Add("Inventory.Request", "set", "IssuingWarehouse", _ent.IssuingWarehouse);
            DT1.Rows.Add("Inventory.Request", "set", "RefJONumber", _ent.RefJONumber);
            DT1.Rows.Add("Inventory.Request", "set", "RefJOStep", _ent.RefJOStep);
            DT1.Rows.Add("Inventory.Request", "set", "MaterialType", _ent.MaterialType);
            DT1.Rows.Add("Inventory.Request", "set", "SpecificMaterialType", _ent.SpecificMaterialType);
            DT1.Rows.Add("Inventory.Request", "set", "WorkCenter", _ent.WorkCenter);

            //user defined
            DT1.Rows.Add("Inventory.Request", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.Request", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.Request", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.Request", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.Request", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.Request", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.Request", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.Request", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.Request", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.Request", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.Request", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Request _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.Request", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Inventory.RequestDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }        
        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Inventory.RequestDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }
    }
}
