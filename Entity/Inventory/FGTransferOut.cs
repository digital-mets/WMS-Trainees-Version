using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class FGTransferOut
    {
        private static string Conn;//ADD CONN
        private static string Docnum;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string ReferenceMONumber { get; set; }
        public virtual string PicklistNumber { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        

        


        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from Inventory.FGTransferOut where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ReferenceMONumber = dtRow["ReferenceMONumber"].ToString();
                PicklistNumber = dtRow["PicklistNumber"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
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
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
            }

            return a;
        }
        public class FGTransferOutDetail
        {

            public virtual FGTransferOut Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string SKUCode { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemDesc { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual decimal RemainingQty { get; set; }
            public virtual decimal TransferredQty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }


            // Added by JCB 10/21/2021
            public virtual DateTime ProductionDate { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual string PalletNumber { get; set; }
            public virtual bool ProductName { get; set; }
            public virtual bool SKUCodeBit { get; set; }
            public virtual bool ProductionDateBit { get; set; }
            public virtual bool BestBeforeDate { get; set; }
            public virtual bool BatchNum { get; set; }
            public virtual bool PackNum { get; set; }
            public virtual string BoxUsed { get; set; }

                /*// Added By JCB 10/21/2021
                ProductionDate = dtRow["ProductionDate"].ToString();
                BatchNumber = dtRow["BatchNumber"].ToString();
                PalletNumber = dtRow["PalletNumber"].ToString();
                ProductName = Convert.ToBoolean(dtRow["ProductName"]);
                SKUCodeBit = Convert.ToBoolean(dtRow["SKUCodeBit"]);
                ProductionDateBit = Convert.ToBoolean(dtRow["ProductionDateBit"]);
                BestBeforeDate = Convert.ToBoolean(dtRow["BestBeforeDate"]);
                PackNum = Convert.ToBoolean(dtRow["PackNum"]);
                BoxUsed = dtRow["BoxUsed"].ToString();*/

            public DataTable getmodetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select A.*, B.Workweek,B.Year from Production.MaterialOrderDetail A left join Production.MaterialOrder B on A.DocNumber = B.DocNumber where A.DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select B.ProductName AS ItemDesc,A.* from Inventory.FGTransferOutDetail A LEFT JOIN Masterfile.FGSKU B on A.ItemCode = B.SKUCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddFGTransferOutDetail(FGTransferOutDetail FGTransferOutDetail)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Inventory.FGTransferOutDetail where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "LineNumber", strLine);
                //DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "SKUCode", FGTransferOutDetail.SKUCode);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "ItemCode", FGTransferOutDetail.ItemCode);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "ItemDesc", FGTransferOutDetail.ItemDesc);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "OrderQty", FGTransferOutDetail.OrderQty);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "RemainingQty", FGTransferOutDetail.RemainingQty);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "TransferredQty", FGTransferOutDetail.TransferredQty);
                
                // Added by JCB 10/21/2021
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "ProductionDate", FGTransferOutDetail.ProductionDate);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "BatchNumber", FGTransferOutDetail.BatchNumber);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "PalletNumber", FGTransferOutDetail.PalletNumber);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "ProductName", FGTransferOutDetail.ProductName);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "SKUCodeBit", FGTransferOutDetail.SKUCodeBit);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "ProductionDateBit", FGTransferOutDetail.ProductionDateBit);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "BestBeforeDate", FGTransferOutDetail.BestBeforeDate);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "BatchNum", FGTransferOutDetail.BatchNum);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "PackNum", FGTransferOutDetail.PackNum);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "BoxUsed", FGTransferOutDetail.BoxUsed);


                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field1", FGTransferOutDetail.Field1);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field2", FGTransferOutDetail.Field2);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field3", FGTransferOutDetail.Field3);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field4", FGTransferOutDetail.Field4);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field5", FGTransferOutDetail.Field5);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field6", FGTransferOutDetail.Field6);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field7", FGTransferOutDetail.Field7);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field8", FGTransferOutDetail.Field8);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "0", "Field9", FGTransferOutDetail.Field9);



                //DT2.Rows.Add("Inventory.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Inventory.CuttingWorksheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateFGTransferOutDetail(FGTransferOutDetail FGTransferOutDetail)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Inventory.FGTransferOutDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "cond", "LineNumber", FGTransferOutDetail.LineNumber);
                //DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "SKUCode", FGTransferOutDetail.SKUCode);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "ItemCode", FGTransferOutDetail.ItemCode);
                //DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "ItemDescription", FGTransferOutDetail.ItemDesc);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "ItemDesc", FGTransferOutDetail.ItemDesc);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "OrderQty", FGTransferOutDetail.OrderQty);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "RemainingQty", FGTransferOutDetail.RemainingQty);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "TransferredQty", FGTransferOutDetail.TransferredQty);

                // Added By JCB 10/21/2021
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "ProductionDate", FGTransferOutDetail.ProductionDate);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "BatchNumber", FGTransferOutDetail.BatchNumber);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "PalletNumber", FGTransferOutDetail.PalletNumber);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "ProductName", FGTransferOutDetail.ProductName);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "SKUCodeBit", FGTransferOutDetail.SKUCodeBit);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "ProductionDateBit", FGTransferOutDetail.ProductionDateBit);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "BestBeforeDate", FGTransferOutDetail.BestBeforeDate);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "BatchNum", FGTransferOutDetail.BatchNum);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "PackNum", FGTransferOutDetail.PackNum);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "BoxUsed", FGTransferOutDetail.BoxUsed);


                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field1", FGTransferOutDetail.Field1);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field2", FGTransferOutDetail.Field2);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field3", FGTransferOutDetail.Field3);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field4", FGTransferOutDetail.Field4);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field5", FGTransferOutDetail.Field5);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field6", FGTransferOutDetail.Field6);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field7", FGTransferOutDetail.Field7);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field8", FGTransferOutDetail.Field8);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "set", "Field9", FGTransferOutDetail.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteFGTransferOutDetail(FGTransferOutDetail FGTransferOutDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "cond", "DocNumber", FGTransferOutDetail.DocNumber);
                DT1.Rows.Add("Inventory.FGTransferOutDetail", "cond", "LineNumber", FGTransferOutDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);




            }





        }
        public class RefTransaction
        {
            public virtual FGTransferOut Parent { get; set; }
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
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDFGTO' OR  A.TransType='PRDFGTO') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(FGTransferOut _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



            DT1.Rows.Add("Inventory.FGTransferOut", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "ReferenceMONumber", _ent.ReferenceMONumber);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "PicklistNumber", _ent.PicklistNumber);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.FGTransferOut", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(FGTransferOut _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.FGTransferOut", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Inventory.FGTransferOut", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "ReferenceMONumber", _ent.ReferenceMONumber);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "PicklistNumber", _ent.PicklistNumber);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.FGTransferOut", "set", "LastEditedDate", _ent.LastEditedDate);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDFGTO", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(FGTransferOut _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.FGTransferOut", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDFGTO", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        /*public class JournalEntry
        {
            public virtual FGTransferOut Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit, A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDFGTO') ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }*/
    }
}
