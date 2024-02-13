using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class StockTransfer
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;
        private static string By;//ADD CONN
        private static string Warehouse;
        private static string typee; 
        public virtual bool changed {get; set;}
        private static bool change;
        public virtual string entryy { get; set; }
        private static string entryyy;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string Transaction { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string ReceivedDate { get; set; }
        public virtual string DispatchNumber { get; set; }
        public virtual string AccountabilityType { get; set; }
        public virtual string FromWarehouse { get; set; }
        public virtual string ToWarehouse { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsAutoReceive { get; set; }

        // 11/16/2021 Added by JCB
        public virtual string ReferenceRR { get; set; }
        public virtual bool Backload { get; set; }

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

        public virtual IList<StockTransferDetail> Detail { get; set; }

        public class JournalEntry
        {
            public virtual StockTransfer Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='INVSTR' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public class StockTransferDetail
        {
            public virtual StockTransfer Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string OISNo { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal DispatchQty { get; set; }
            public virtual decimal ReceivedQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal DispatchBulkQty { get; set; }
            public virtual decimal ReceivedBulkQty { get; set; }

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
                    a = Gears.RetriveData2("SELECT A.*,B.FullDesc FROM Inventory.StockTransferDetail A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.Itemcode WHERE a.DocNumber = '" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStockTransferDetail(StockTransferDetail A)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Inventory.StockTransferDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "ItemCode", A.ItemCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "ColorCode", A.ColorCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "ClassCode", A.ClassCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "SizeCode", A.SizeCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Unit", A.Unit);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "DispatchQty", A.DispatchQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "ReceivedQty", A.ReceivedQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "DispatchBulkQty", A.DispatchBulkQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "ReceivedBulkQty", A.ReceivedBulkQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field1", A.Field1);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field2", A.Field2);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field3", A.Field3);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field4", A.Field4);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field5", A.Field5);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field6", A.Field6);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field7", A.Field7);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field8", A.Field8);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Field9", A.Field9);
                if (A.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "0", "ExpDate", A.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "0", "ExpDate", null);
                }
                if (A.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "0", "MfgDate", A.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "BatchNo", A.BatchNo);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "LotNo", A.LotNo);
                DT1.Rows.Add("Inventory.StockTransferDetail", "0", "Version", "1");
                DT2.Rows.Add("Inventory.StockTransfer", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Inventory.StockTransfer", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);


                #region Countsheet TLAV
                if (typee == "R")
                {
                    bool isbybulk = false;
                    Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                    Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                    DataTable CS = Gears.RetriveData2("SELECT IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + A.ItemCode + "'", Conn);
                    foreach (DataRow dt in CS.Rows)
                    {
                        isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                    }
                    if (isbybulk == true)
                    {
                        for (int i = 1; i <= A.ReceivedBulkQty; i++)
                        {
                            string strLine2 = i.ToString().PadLeft(5, '0');
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", "INVSTR");
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine2); // RollId
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", A.ItemCode);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", A.ColorCode);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", A.ClassCode);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", A.SizeCode);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", ddate);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehouse);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", A.ExpDate);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", A.MfgDate == Convert.ToDateTime("1/1/0001 12:00:00 AM") ? Convert.ToDateTime(ddate) : A.MfgDate);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", A.BatchNo == "" ? "N/A" : A.BatchNo);
                            DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", A.LotNo == "" ? "N/A" : A.LotNo);
                            Gears.CreateData(DT4, Conn);
                            DT4.Rows.Clear();
                        }
                    }
                }
                #endregion

            }
            public void UpdateStockTransferDetail(StockTransferDetail B)
            {
                #region Countsheet 
                if (typee == "R")
                {
                    bool isbybulk = false;

                    DataTable dtable = Gears.RetriveData2("SELECT ReceivedBulkQty AS BulkQty FROM Inventory.StockTransferDetail WHERE DocNumber = '" + Docnum + "' AND LineNumber = '" + B.LineNumber + "'", Conn);
                    foreach (DataRow dtrow in dtable.Rows)
                    {
                        if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != B.ReceivedBulkQty)
                        {
                            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                            DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                            DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", B.LineNumber);
                            Gears.DeleteData(DT3, Conn);

                            Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                            DataTable CS = Gears.RetriveData2("SELECT IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + B.ItemCode + "'", Conn);
                            foreach (DataRow dt in CS.Rows)
                            {
                                isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                            }
                            if (isbybulk == true)
                            {
                                for (int i = 1; i <= B.ReceivedBulkQty; i++)
                                {
                                    string strLine2 = i.ToString().PadLeft(5, '0');

                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", "INVSTR");
                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", Docnum);
                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", B.LineNumber);
                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", strLine2);
                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", B.ItemCode);
                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", B.ColorCode);
                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", B.ClassCode);
                                    DT4.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", B.SizeCode);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", ddate);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehouse);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", B.ExpDate);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", B.MfgDate == Convert.ToDateTime("1/1/0001 12:00:00 AM") ? Convert.ToDateTime(ddate) : B.MfgDate);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", B.BatchNo == "" ? "N/A" : B.BatchNo);
                                    DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", B.LotNo == "" ? "N/A" : B.LotNo);
                                    Gears.CreateData(DT4, Conn);
                                    DT4.Rows.Clear();
                                }
                            }
                        }
                    }
                }
                #endregion
                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.StockTransferDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.StockTransferDetail", "cond", "LineNumber", B.LineNumber);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "OISNo", B.OISNo);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "ItemCode", B.ItemCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "ColorCode", B.ColorCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "ClassCode", B.ClassCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "SizeCode", B.SizeCode);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "DispatchQty", B.DispatchQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "ReceivedQty", B.ReceivedQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Unit", B.Unit);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "DispatchBulkQty", B.DispatchBulkQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "ReceivedBulkQty", B.ReceivedBulkQty);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field1", B.Field1);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field2", B.Field2);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field3", B.Field3);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field4", B.Field4);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field5", B.Field5);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field6", B.Field6);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field7", B.Field7);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field8", B.Field8);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Field9", B.Field9);
                if (B.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "set", "ExpDate", B.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "set", "ExpDate", null);
                }
                if (B.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "set", "MfgDate", B.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.StockTransferDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "BatchNo", B.BatchNo);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "LotNo", B.LotNo);
                DT1.Rows.Add("Inventory.StockTransferDetail", "set", "Version", "1");
                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteStockTransferDetail(StockTransferDetail C)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();   
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.StockTransferDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.StockTransferDetail", "cond", "LineNumber", C.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", C.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "INVSTR");
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Inventory.StockTransferDetail WHERE DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Inventory.StockTransfer", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Inventory.StockTransfer", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }
        public class RefTransaction
        {
            public virtual StockTransfer Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType, REFDocNumber, RMenuID, RIGHT(B.CommandString, LEN(B.CommandString) - 1) AS RCommandString, "
                                            + " A.TransType, DocNumber, A.MenuID, RIGHT(C.CommandString, LEN(C.CommandString) - 1) AS CommandString FROM IT.ReferenceTrans A "
                                            + " INNER JOIN IT.MainMenu B ON A.RMenuID =B.ModuleID INNER JOIN IT.MainMenu C ON A.MenuID = C.ModuleID "
                                            + " WHERE (DocNumber = '" + DocNumber + "' OR REFDocNumber = '" + DocNumber + "') AND (RTransType = 'INVSTR' OR A.TransType = 'INVSTR')", Conn);
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

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Inventory.StockTransfer WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                //header
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                Type = dtRow["Type"].ToString();
                ReceivedDate = dtRow["ReceivedDate"].ToString();
                DispatchNumber = dtRow["DispatchNumber"].ToString();
                AccountabilityType = dtRow["AccountabilityType"].ToString();
                FromWarehouse = dtRow["FromWarehouse"].ToString();
                ToWarehouse = dtRow["ToWarehouse"].ToString();
                // 11/16/2021 Added by JCB
                ReferenceRR = dtRow["ReferenceRR"].ToString();
                Backload = Convert.ToBoolean(Convert.IsDBNull(dtRow["Backload"]) ? false : dtRow["Backload"]);

                Remarks = dtRow["Remarks"].ToString();
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);
                IsAutoReceive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsAutoReceive"]) ? false : dtRow["IsAutoReceive"]);

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
        public void InsertData(StockTransfer _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            change = _ent.changed;
            entryyy = _ent.entryy;
            By = _ent.LastEditedBy;
            Warehouse = _ent.ToWarehouse;
            typee = _ent.Type;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.StockTransfer", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Type", _ent.Type);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "ReceivedDate", _ent.ReceivedDate);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "DispatchNumber", _ent.DispatchNumber);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "AccountabilityType", _ent.AccountabilityType);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "FromWarehouse", _ent.FromWarehouse);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "ToWarehouse", _ent.ToWarehouse);
            // 11/16/2021 Added By JCB
            DT1.Rows.Add("Inventory.StockTransfer", "0", "ReferenceRR", _ent.ReferenceRR);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Backload", _ent.Backload);

            DT1.Rows.Add("Inventory.StockTransfer", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "IsAutoReceive", _ent.IsAutoReceive);

            //user defined
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.StockTransfer", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.StockTransfer", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(StockTransfer _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;
            change = _ent.changed;
            entryyy = _ent.entryy;
            By = _ent.LastEditedBy;
            Warehouse = _ent.ToWarehouse;
            typee = _ent.Type;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.StockTransfer", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Type", _ent.Type);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "ReceivedDate", _ent.ReceivedDate);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "DispatchNumber", _ent.DispatchNumber);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "AccountabilityType", _ent.AccountabilityType);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "FromWarehouse", _ent.FromWarehouse);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "ToWarehouse", _ent.ToWarehouse);
            // 11/16/2021 Added by JCB
            DT1.Rows.Add("Inventory.StockTransfer", "set", "ReferenceRR", _ent.ReferenceRR);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Backload", _ent.Backload);

            DT1.Rows.Add("Inventory.StockTransfer", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "IsAutoReceive", _ent.IsAutoReceive);

            //user defined
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.StockTransfer", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.StockTransfer", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(StockTransfer _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;
            By = _ent.LastEditedBy;
            Warehouse = _ent.ToWarehouse;
            typee = _ent.Type;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.StockTransfer", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Inventory.StockTransferDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void UpdateUnitFactor(string DocNum, string Conn)
        {
            DataTable b = Gears.RetriveData2(" UPDATE A "
                + " SET A.UnitFactor = ISNULL(C.ConversionFactor ,1) "
                + " FROM Inventory.StockTransferDetail  A "
                + " INNER JOIN Masterfile.Item B "
                + " ON A.ItemCode = B.ItemCode "
                + " LEFT JOIN Masterfile.UnitConversion C "
                + " ON A.Unit = C.UnitCodeFrom "
                + " AND B.UnitBase = C.UnitCodeTo "
                + " WHERE DocNumber ='" + DocNum + "'", Conn);
        }
    }
}
