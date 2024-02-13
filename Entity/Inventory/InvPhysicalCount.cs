using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class InvPhysicalCount
    {
        private static string Docnum;
        private static string ddate;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string CutOffDate { get; set; }
        public virtual bool IsFinal { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string WarehouseType { get; set; }
        public virtual string Status { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string GeneratedBy { get; set; }
        public virtual string GeneratedDate { get; set; }
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
        public virtual IList<InvPhysicalCountDetail> Detail { get; set; }

        public class InvPhysicalCountDetail
        {
            public virtual InvPhysicalCount Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal ActualQty { get; set; }
            public virtual decimal ActualBulkQty { get; set; }
            public virtual decimal IntransitQty { get; set; }
            public virtual decimal SystemQty { get; set; }
            public virtual decimal SystemBulkQty { get; set; }
            public virtual decimal VarianceQty { get; set; }
            public virtual decimal VarianceBulkQty { get; set; }
            public virtual bool IsByBulk { get; set; }
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
                    a = Gears.RetriveData2("SELECT A.*,B.FullDesc FROM Inventory.PhysicalCountDetail A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.Itemcode WHERE a.DocNumber = '" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddInvPhysicalCountDetail(InvPhysicalCountDetail PhysicalCountDetail)
            {

                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Inventory.PhysicalCountDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Unit", PhysicalCountDetail.Unit);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "ActualQty", PhysicalCountDetail.ActualQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "ActualBulkQty", PhysicalCountDetail.ActualBulkQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "IntransitQty", PhysicalCountDetail.IntransitQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "SystemQty", PhysicalCountDetail.SystemQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "SystemBulkQty", PhysicalCountDetail.SystemBulkQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "VarianceQty", PhysicalCountDetail.VarianceQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "VarianceBulkQty", PhysicalCountDetail.VarianceBulkQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "IsByBulk", PhysicalCountDetail.IsByBulk);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "BaseQty", PhysicalCountDetail.BaseQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "UnitFactor", PhysicalCountDetail.UnitFactor);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "BarcodeNo", PhysicalCountDetail.BarcodeNo);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "StatusCode", PhysicalCountDetail.StatusCode);

                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field1", PhysicalCountDetail.Field1);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field2", PhysicalCountDetail.Field2);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field3", PhysicalCountDetail.Field3);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field4", PhysicalCountDetail.Field4);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field5", PhysicalCountDetail.Field5);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field6", PhysicalCountDetail.Field6);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field7", PhysicalCountDetail.Field7);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field8", PhysicalCountDetail.Field8);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Field9", PhysicalCountDetail.Field9);
                if (PhysicalCountDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "ExpDate", PhysicalCountDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "ExpDate", null);
                }
                if (PhysicalCountDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "MfgDate", PhysicalCountDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "BatchNo", PhysicalCountDetail.BatchNo);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "LotNo", PhysicalCountDetail.LotNo);

                DT1.Rows.Add("Inventory.PhysicalCountDetail", "0", "Version", "1");

                DT2.Rows.Add("Inventory.PhysicalCount", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Inventory.PhysicalCount", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                //DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode = '" + PhysicalCountDetail.ItemCode + "'", Conn);
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= PhysicalCountDetail.ActualBulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVCNT");
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);
                //        Gears.CreateData(DT4, Conn);
                //        DT4.Rows.Clear();
                //    }
                //}
                //else
                //{
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVCNT");
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    Gears.CreateData(DT4, Conn);
                //}
            }
            public void UpdateInvPhysicalCountDetail(InvPhysicalCountDetail PhysicalCountDetail)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("SELECT ISNULL(ActualBulkQty,0) ActualBulkQty FROM Inventory.PhysicalCountDetail WHERE DocNumber = '" + Docnum + "' " +
                //"AND LineNumber = '" + PhysicalCountDetail.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["ActualBulkQty"].ToString()) != PhysicalCountDetail.ActualBulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PhysicalCountDetail.LineNumber);
                //        Gears.DeleteData(DT3, Conn);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode  = '" + PhysicalCountDetail.ItemCode + "'", Conn);
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= PhysicalCountDetail.ActualBulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVCNT");
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PhysicalCountDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", 1);

                //                Gears.CreateData(DT4, Conn);
                //                DT4.Rows.Clear();
                //            }
                //        }
                //        else
                //        {
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "INVCNT");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", PhysicalCountDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", PhysicalCountDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", PhysicalCountDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", PhysicalCountDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", PhysicalCountDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //            Gears.CreateData(DT4, Conn);
                //        }
                //    }
                //}

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "cond", "LineNumber", PhysicalCountDetail.LineNumber);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "ItemCode", PhysicalCountDetail.ItemCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "FullDesc", PhysicalCountDetail.FullDesc);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "ColorCode", PhysicalCountDetail.ColorCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "ClassCode", PhysicalCountDetail.ClassCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "SizeCode", PhysicalCountDetail.SizeCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Unit", PhysicalCountDetail.Unit);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "ActualQty", PhysicalCountDetail.ActualQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "ActualBulkQty", PhysicalCountDetail.ActualBulkQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "IntransitQty", PhysicalCountDetail.IntransitQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "SystemQty", PhysicalCountDetail.SystemQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "SystemBulkQty", PhysicalCountDetail.SystemBulkQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "VarianceQty", PhysicalCountDetail.VarianceQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "VarianceBulkQty", PhysicalCountDetail.VarianceBulkQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "IsByBulk", PhysicalCountDetail.IsByBulk);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "BaseQty", PhysicalCountDetail.BaseQty);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "UnitFactor", PhysicalCountDetail.UnitFactor);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "BarcodeNo", PhysicalCountDetail.BarcodeNo);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "StatusCode", PhysicalCountDetail.StatusCode);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field1", PhysicalCountDetail.Field1);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field2", PhysicalCountDetail.Field2);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field3", PhysicalCountDetail.Field3);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field4", PhysicalCountDetail.Field4);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field5", PhysicalCountDetail.Field5);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field6", PhysicalCountDetail.Field6);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field7", PhysicalCountDetail.Field7);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field8", PhysicalCountDetail.Field8);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Field9", PhysicalCountDetail.Field9);
                if (PhysicalCountDetail.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "ExpDate", PhysicalCountDetail.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "ExpDate", null);
                }
                if (PhysicalCountDetail.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "MfgDate", PhysicalCountDetail.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "BatchNo", PhysicalCountDetail.BatchNo);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "LotNo", PhysicalCountDetail.LotNo);

                DT1.Rows.Add("Inventory.PhysicalCountDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);            
                 
            }
            public void DeleteInvPhysicalCountDetail(InvPhysicalCountDetail PhysicalCountDetail)
            {                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.PhysicalCountDetail", "cond", "LineNumber", PhysicalCountDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PhysicalCountDetail.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "INVCNT");
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Inventory.PhysicalCountDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Inventory.PhysicalCount", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Inventory.PhysicalCount", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }

        public class JournalEntry
        {
            public virtual InvPhysicalCount Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='INVCNT' ", Conn);

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

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Inventory.PhysicalCount WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                Type = dtRow["Type"].ToString();
                CutOffDate = dtRow["CutOffDate"].ToString();
                IsFinal = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsFinal"]) ? false : dtRow["IsFinal"]);
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                WarehouseType = dtRow["WarehouseType"].ToString();
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
                GeneratedBy = dtRow["GeneratedBy"].ToString();
                GeneratedDate = dtRow["GeneratedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
            }

            return a;
        }
        public void InsertData(InvPhysicalCount _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.PhysicalCount", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Type", _ent.Type);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "CutOffDate", _ent.CutOffDate);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "IsFinal", _ent.IsFinal);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "WarehouseType", _ent.WarehouseType);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Status", _ent.Status);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.PhysicalCount", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.PhysicalCount", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(InvPhysicalCount _ent)
        {
            Docnum = _ent.DocNumber;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.PhysicalCount", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Type", _ent.Type);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "CutOffDate", _ent.CutOffDate);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "IsFinal", _ent.IsFinal);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "WarehouseType", _ent.WarehouseType);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Status", _ent.Status);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.PhysicalCount", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.PhysicalCount", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("INVCNT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(InvPhysicalCount _ent)
        {
            Docnum = _ent.DocNumber;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.PhysicalCount", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Inventory.PhysicalCountDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("INVCNT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }        
        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Inventory.PhysicalCountDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }

        public void UpdateIsGenerated(string DocNum, string Conn)
        {
            DataTable b = Gears.RetriveData2("UPDATE Inventory.PhysicalCount SET IsGenerated = 0 WHERE DocNumber = '" + DocNum + "'", Conn);
        }
    }
}
