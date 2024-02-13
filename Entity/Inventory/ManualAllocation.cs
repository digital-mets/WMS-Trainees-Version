using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ManualAllocation
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string Transaction { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        //public virtual string DeliveryDate { get; set; }
        //public virtual string CancellationDate { get; set; }
        public virtual string Customer { get; set; }
        public virtual string Remarks { get; set; }
        //public virtual string DRNumber { get; set; }
        public virtual decimal TotalQty { get; set; }
        //public virtual decimal TotalAmount { get; set; }
        //public virtual string Warehouse { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }

        //Audit Trail
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
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

        public virtual IList<ManualAllocationDetail> Detail { get; set; }

        public class ManualAllocationDetail
        {
            public virtual ManualAllocation Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Price { get; set; }
            public virtual decimal Qty { get; set; }
            //public virtual bool Allocate { get; set; }
            public virtual string Unit { get; set; }
            
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
                    a = Gears.RetriveData2("select * from Inventory.ManualAllocationDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddManualAllocationDetail(ManualAllocationDetail A)
            {
                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Inventory.ManualAllocationDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "ItemCode", A.ItemCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "ColorCode", A.ColorCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "ClassCode", A.ClassCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "SizeCode", A.SizeCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Price", A.Price);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Qty", A.Qty);
                //DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Allocate", A.Allocate);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Unit", A.Unit);

                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field1", A.Field1);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field2", A.Field2);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field3", A.Field3);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field4", A.Field4);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field5", A.Field5);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field6", A.Field6);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field7", A.Field7);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field8", A.Field8);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Field9", A.Field9);

                DT1.Rows.Add("Inventory.ManualAllocationDetail", "0", "Version", "1");

                DT2.Rows.Add("Inventory.ManualAllocation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Inventory.ManualAllocation", "set", "IsWithDetail", "True");

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

                //    for (int i = 1; i <= A.Qty; i++)
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
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", A.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", A.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", A.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", A.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    Gears.CreateData(DT4, Conn);
                //}
            }
            public void UpdateManualAllocationDetail(ManualAllocationDetail B)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("SELECT IssuedBulkQty FROM Inventory.ManualAllocationDetail WHERE DocNumber = '" + Docnum + "' " +
                //"AND LineNumber = '" + B.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["Qty"].ToString()) != B.Qty)
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

                //            for (int i = 1; i <= B.Qty; i++)
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
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);

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
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "cond", "LineNumber", B.LineNumber);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "ItemCode", B.ItemCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "ColorCode", B.ColorCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "ClassCode", B.ClassCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "SizeCode", B.SizeCode);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Price", B.Price);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Qty", B.Qty);
                //DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Allocate", B.Allocate);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Unit", B.Unit);

                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field1", B.Field1);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field2", B.Field2);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field3", B.Field3);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field4", B.Field4);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field5", B.Field5);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field6", B.Field6);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field7", B.Field7);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field8", B.Field8);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Field9", B.Field9);

                DT1.Rows.Add("Inventory.ManualAllocationDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);            
                 
            }
            public void DeleteManualAllocationDetail(ManualAllocationDetail C)
            {               
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ManualAllocationDetail", "cond", "LineNumber", C.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", C.LineNumber);
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Inventory.ManualAllocationDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Inventory.ManualAllocation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Inventory.ManualAllocation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }
        public class RefTransaction
        {
            public virtual ManualAllocation Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='INVMAN' OR  A.TransType='INVMAN') ", Conn);
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

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Inventory.ManualAllocation WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                //header
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                //DeliveryDate = dtRow["DeliveryDate"].ToString();
                //CancellationDate = dtRow["CancellationDate"].ToString();
                Customer = dtRow["Customer"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                //DRNumber = dtRow["DRNumber"].ToString();
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                //TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                //Warehouse = dtRow["Warehouse"].ToString();
                
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);
                
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
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
            }

            return a;
        }
        public void InsertData(ManualAllocation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "DocDate", _ent.DocDate);
            //DT1.Rows.Add("Inventory.ManualAllocation", "0", "DeliveryDate", _ent.DeliveryDate);
            //DT1.Rows.Add("Inventory.ManualAllocation", "0", "CancellationDate", _ent.CancellationDate);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Customer", _ent.Customer);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Remarks", _ent.Remarks);
            //DT1.Rows.Add("Inventory.ManualAllocation", "0", "DRNumber", _ent.DRNumber);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "TotalQty", _ent.TotalQty);
            //DT1.Rows.Add("Inventory.ManualAllocation", "0", "TotalAmount", _ent.TotalAmount);
            //DT1.Rows.Add("Inventory.ManualAllocation", "0", "Warehouse", _ent.Warehouse);

            //user defined
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ManualAllocation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.ManualAllocation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ManualAllocation _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.ManualAllocation", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "DocDate", _ent.DocDate);
            //DT1.Rows.Add("Inventory.ManualAllocation", "set", "DeliveryDate", _ent.DeliveryDate);
            //DT1.Rows.Add("Inventory.ManualAllocation", "set", "CancellationDate", _ent.CancellationDate);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Customer", _ent.Customer);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Remarks", _ent.Remarks);
            //DT1.Rows.Add("Inventory.ManualAllocation", "set", "DRNumber", _ent.DRNumber);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "TotalQty", _ent.TotalQty);
            //DT1.Rows.Add("Inventory.ManualAllocation", "set", "TotalAmount", _ent.TotalAmount);
            //DT1.Rows.Add("Inventory.ManualAllocation", "set", "Warehouse", _ent.Warehouse);

            //user defined
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ManualAllocation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.ManualAllocation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ManualAllocation _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.ManualAllocation", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Inventory.ManualAllocationDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }        
        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Inventory.ManualAllocationDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }
    }
}
