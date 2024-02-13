using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Return
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string Transaction { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string ReturnType { get; set; }
        public virtual string SamplesType { get; set; }
        public virtual string IssuanceNumber { get; set; }
        public virtual string ReturnedBy { get; set; }
        public virtual string CostCenter { get; set; }
        public virtual string Warehouse { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual string Remarks { get; set; }
        public virtual bool IsWithReference { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }

        //JO Tab
        public virtual string JONumber { get; set; }
        public virtual string JOStep { get; set; }
        public virtual string WorkCenter { get; set; }
        public virtual string Reason { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual Boolean NoAlloc { get; set; }
        public virtual decimal TotalCost { get; set; }

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

        public virtual IList<ReturnDetail> Detail { get; set; }
        public class JournalEntry
        {
            public virtual Return Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='"+TransType+"' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public class ReturnDetail
        {
            public virtual Return Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal ItemPrice { get; set; }
            public virtual decimal IssuedQty { get; set; }
            public virtual decimal IssuedBulkQty { get; set; }
            public virtual decimal ReturnedQty { get; set; }
            public virtual decimal ReturnedBulkQty { get; set; }
            public virtual decimal Cost { get; set; }
            public virtual decimal UnitCost { get; set; }
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
                    a = Gears.RetriveData2("select * from Inventory.ReturnDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddReturnDetail(ReturnDetail A)
            {
                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Inventory.ReturnDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "ItemCode", A.ItemCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "ColorCode", A.ColorCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "ClassCode", A.ClassCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "SizeCode", A.SizeCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Unit", A.Unit);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "ItemPrice", A.ItemPrice);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "IssuedQty", A.IssuedQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "IssuedBulkQty", A.IssuedBulkQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "ReturnedQty", A.ReturnedQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "ReturnedBulkQty", A.ReturnedBulkQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Cost", A.Cost);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "UnitCost", A.UnitCost);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "StatusCode", A.StatusCode);

                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field1", A.Field1);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field2", A.Field2);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field3", A.Field3);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field4", A.Field4);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field5", A.Field5);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field6", A.Field6);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field7", A.Field7);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field8", A.Field8);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Field9", A.Field9);
                if (A.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "0", "ExpDate", A.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "0", "ExpDate", null);
                }
                if (A.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "0", "MfgDate", A.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "BatchNo", A.BatchNo);
                DT1.Rows.Add("Inventory.ReturnDetail", "0", "LotNo", A.LotNo);

                DT1.Rows.Add("Inventory.ReturnDetail", "0", "Version", "1");

                DT2.Rows.Add("Inventory.ReturnHeader", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Inventory.ReturnHeader", "set", "IsWithDetail", "True");

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
                //    DataTable RT = Gears.RetriveData2("SELECT * FROM WMS.CountsheetSetup WHERE ItemCode = '" + A.ItemCode + "' AND ColorCode = '" + A.ColorCode + "' AND ClassCode = '" + A.ClassCode + "' AND SizeCode = '" + A.SizeCode + "'", Conn);
                //    foreach (DataRow i in RT.Rows)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", i["TransDoc"].ToString());
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", A.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", A.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", A.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", A.SizeCode);
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
                //    //DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    Gears.CreateData(DT4, Conn);
                //}
            }
            public void UpdateReturnDetail(ReturnDetail B)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("SELECT ReturnedBulkQty FROM Inventory.ReturnDetail WHERE DocNumber = '" + Docnum + "' " +
                //"AND LineNumber = '" + B.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["ReturnedBulkQty"].ToString()) != B.ReturnedBulkQty)
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
                //            DataTable RT = Gears.RetriveData2("SELECT * FROM WMS.CountsheetSetup WHERE ItemCode = '" + B.ItemCode + "' AND ColorCode = '" + B.ColorCode + "' AND ClassCode = '" + B.ClassCode + "' AND SizeCode = '" + B.SizeCode + "'", Conn);
                //            foreach (DataRow i in RT.Rows)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", i["TransDoc"].ToString());
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", B.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", B.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", B.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", B.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", B.SizeCode);
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
                //            Gears.CreateData(DT4, Conn);
                //        }
                //    }
                //}

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.ReturnDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ReturnDetail", "cond", "LineNumber", B.LineNumber);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "ItemCode", B.ItemCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "ColorCode", B.ColorCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "ClassCode", B.ClassCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "SizeCode", B.SizeCode);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Unit", B.Unit);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "ItemPrice", B.ItemPrice);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "IssuedQty", B.IssuedQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "IssuedBulkQty", B.IssuedBulkQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "ReturnedQty", B.ReturnedQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "ReturnedBulkQty", B.ReturnedBulkQty);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Cost", B.Cost);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "UnitCost", B.UnitCost);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "StatusCode", B.StatusCode);

                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field1", B.Field1);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field2", B.Field2);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field3", B.Field3);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field4", B.Field4);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field5", B.Field5);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field6", B.Field6);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field7", B.Field7);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field8", B.Field8);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Field9", B.Field9);
                if (B.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "set", "ExpDate", B.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "set", "ExpDate", null);
                }
                if (B.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "set", "MfgDate", B.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Inventory.ReturnDetail", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "BatchNo", B.BatchNo);
                DT1.Rows.Add("Inventory.ReturnDetail", "set", "LotNo", B.LotNo);

                DT1.Rows.Add("Inventory.ReturnDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteReturnDetail(ReturnDetail C)
            {               
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.ReturnDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.ReturnDetail", "cond", "LineNumber", C.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", C.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", trans);
                Gears.DeleteData(DT3, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Inventory.ReturnDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Inventory.ReturnHeader", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Inventory.ReturnHeader", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }
        public class RefTransaction
        {
            public virtual Return Parent { get; set; }
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

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Inventory.ReturnHeader WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                //header
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ReturnType = dtRow["ReturnType"].ToString();
                SamplesType = dtRow["SamplesType"].ToString();
                IssuanceNumber = dtRow["IssuanceNumber"].ToString();
                ReturnedBy = dtRow["ReturnedBy"].ToString();
                CostCenter = dtRow["CostCenter"].ToString();
                Warehouse = dtRow["Warehouse"].ToString();
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                Remarks = dtRow["Remarks"].ToString();
                IsWithReference = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithReference"]) ? false : dtRow["IsWithReference"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);
                
                //JO
                JONumber = dtRow["JONumber"].ToString();
                JOStep = dtRow["JOStep"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                Reason = dtRow["Reason"].ToString();
                Currency = dtRow["Currency"].ToString();
                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                NoAlloc = Convert.ToBoolean(Convert.IsDBNull(dtRow["NoAlloc"]) ? false : dtRow["NoAlloc"]);
                TotalCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCost"]) ? 0 : dtRow["TotalCost"]);
                
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
        public void InsertData(Return _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            trans = _ent.Transaction;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "ReturnType", _ent.ReturnType);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "SamplesType", _ent.SamplesType);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "IssuanceNumber", _ent.IssuanceNumber);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "ReturnedBy", _ent.ReturnedBy);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Warehouse", _ent.Warehouse);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "IsWithReference", _ent.IsWithReference);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "IsPrinted", _ent.IsPrinted);

            //JO
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "JOStep", _ent.JOStep);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Reason", _ent.Reason);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "NoAlloc", _ent.NoAlloc);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "TotalCost", _ent.TotalCost);

            //user defined
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ReturnHeader", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.ReturnHeader", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(Return _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //header
            DT1.Rows.Add("Inventory.ReturnHeader", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "ReturnType", _ent.ReturnType);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "SamplesType", _ent.SamplesType);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "IssuanceNumber", _ent.IssuanceNumber);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "ReturnedBy", _ent.ReturnedBy);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Warehouse", _ent.Warehouse);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "IsWithReference", _ent.IsWithReference);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "IsPrinted", _ent.IsPrinted);

            //JO
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "JOStep", _ent.JOStep);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Reason", _ent.Reason);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "NoAlloc", _ent.NoAlloc);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "TotalCost", _ent.TotalCost);

            //user defined
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ReturnHeader", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.ReturnHeader", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Return _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.ReturnHeader", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Inventory.ReturnDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }        
        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Inventory.ReturnDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }

        public void UpdateUnitFactor(string DocNum, string Conn)
        {
            DataTable b = Gears.RetriveData2(" UPDATE A "
                + " SET A.UnitFactor = ISNULL(C.ConversionFactor ,1) "
                + " FROM Inventory.ReturnDetail  A "
                + " INNER JOIN Masterfile.Item B "
                + " ON A.ItemCode = B.ItemCode "
                + " LEFT JOIN Masterfile.UnitConversion C "
                + " ON A.Unit = C.UnitCodeFrom "
                + " AND B.UnitBase = C.UnitCodeTo "
                + " WHERE DocNumber ='" + DocNum + "'", Conn);
        }
    }
}