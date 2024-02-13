using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Issuance
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string Transaction { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TransType { get; set; }
        public virtual string Type { get; set; }
        public virtual string RefNumber { get; set; }
        public virtual string IssuedTo { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string CostCenter { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual bool IsWithReference { get; set; }
        public virtual bool IsCharge { get; set; }
        public virtual bool IsIssued { get; set; }
        public virtual bool IsReplacement { get; set; }
        public virtual string MaterialType { get; set; }
        public virtual string SpecificType { get; set; }
        public virtual string WorkCenter { get; set; }
        public virtual string ReqJONumber { get; set; }
        public virtual string ReqJOStep { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual string ComplimentaryType { get; set; }
        public virtual string ReferenceDIS { get; set; }
        public virtual string SamplesTo { get; set; }
        public virtual string SamplesType { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
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
        public virtual bool IsWithDetail { get; set; }
        public virtual IList<IssuanceDetail> Detail { get; set; }
                
        public class IssuanceDetail
        {
            public virtual Issuance Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal RequestedQty { get; set; }
            public virtual decimal IssuedQty { get; set; }
      
            public virtual string MtlType { get; set; }
            public virtual string ShiftSched { get; set; }
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

            public virtual string BatchNo { get; set; }
            
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Inventory.IssuanceDetail WHERE DocNumber = '" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddIssuanceDetail(IssuanceDetail IssuanceDetail)
            {

                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Inventory.IssuanceDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "ItemCode", IssuanceDetail.ItemCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "FullDesc", IssuanceDetail.FullDesc);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "ColorCode", IssuanceDetail.ColorCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "ClassCode", IssuanceDetail.ClassCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "SizeCode", IssuanceDetail.SizeCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Unit", IssuanceDetail.Unit);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "RequestedQty", IssuanceDetail.RequestedQty);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "IssuedQty", IssuanceDetail.IssuedQty);



                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "MtlType", IssuanceDetail.MtlType);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "BatchNo", IssuanceDetail.BatchNo);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "ShiftSched", IssuanceDetail.ShiftSched);

                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field1", IssuanceDetail.Field1);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field2", IssuanceDetail.Field2);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field3", IssuanceDetail.Field3);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field4", IssuanceDetail.Field4);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field5", IssuanceDetail.Field5);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field6", IssuanceDetail.Field6);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field7", IssuanceDetail.Field7);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field8", IssuanceDetail.Field8);
                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Field9", IssuanceDetail.Field9);

                DT1.Rows.Add("Inventory.IssuanceDetail", "0", "Version", "1");
         

                DT2.Rows.Add("Inventory.Issuance", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Inventory.Issuance", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

                //DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode = '" + IssuanceDetail.ItemCode + "'", Conn);
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                //foreach (DataRow dt in CS.Rows)
                //{
                //    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //}
                //if (isbybulk == true)
                //{

                //    for (int i = 1; i <= IssuanceDetail.IssuedBulkQty; i++)
                //    {
                //        string strLine2 = i.ToString().PadLeft(5, '0');
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", IssuanceDetail.ItemCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", IssuanceDetail.ColorCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", IssuanceDetail.ClassCode);
                //        DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", IssuanceDetail.SizeCode);
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
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", IssuanceDetail.ItemCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", IssuanceDetail.ColorCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", IssuanceDetail.ClassCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", IssuanceDetail.SizeCode);
                //    DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);
                //    Gears.CreateData(DT4, Conn);
                //}
            }
            public void UpdateIssuanceDetail(IssuanceDetail IssuanceDetail)
            {
                //bool isbybulk = false;

                //DataTable dtable = Gears.RetriveData2("SELECT IssuedBulkQty FROM Inventory.IssuanceDetail WHERE DocNumber = '" + Docnum + "' " +
                //"AND LineNumber = '" + IssuanceDetail.LineNumber + "'", Conn);
                //foreach (DataRow dtrow in dtable.Rows)
                //{
                //    if (Convert.ToDecimal(dtrow["IssuedBulkQty"].ToString()) != IssuanceDetail.IssuedBulkQty)
                //    {
                //        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", Docnum);
                //        DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", IssuanceDetail.LineNumber);
                //        Gears.DeleteData(DT3, Conn);

                //        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                //        DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk FROM Masterfile.Item WHERE ItemCode  = '" + IssuanceDetail.ItemCode + "'", Conn);
                //        foreach (DataRow dt in CS.Rows)
                //        {
                //            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                //        }
                //        if (isbybulk == true)
                //        {

                //            for (int i = 1; i <= IssuanceDetail.IssuedBulkQty; i++)
                //            {
                //                string strLine2 = i.ToString().PadLeft(5, '0');
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", trans);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", IssuanceDetail.LineNumber);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLine2);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", IssuanceDetail.ItemCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", IssuanceDetail.ColorCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", IssuanceDetail.ClassCode);
                //                DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", IssuanceDetail.SizeCode);
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
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", IssuanceDetail.LineNumber);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", IssuanceDetail.ItemCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", IssuanceDetail.ColorCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", IssuanceDetail.ClassCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", IssuanceDetail.SizeCode);
                //            DT4.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", ddate);

                //            Gears.CreateData(DT4, Conn);
                //        }
                //    }
                //}

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.IssuanceDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.IssuanceDetail", "cond", "LineNumber", IssuanceDetail.LineNumber);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "ItemCode", IssuanceDetail.ItemCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "FullDesc", IssuanceDetail.FullDesc);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "ColorCode", IssuanceDetail.ColorCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "ClassCode", IssuanceDetail.ClassCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "SizeCode", IssuanceDetail.SizeCode);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Unit", IssuanceDetail.Unit);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "RequestedQty", IssuanceDetail.RequestedQty);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "IssuedQty", IssuanceDetail.IssuedQty);



                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "MtlType", IssuanceDetail.MtlType);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "BatchNo", IssuanceDetail.BatchNo);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "ShiftSched", IssuanceDetail.ShiftSched);

                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field1", IssuanceDetail.Field1);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field2", IssuanceDetail.Field2);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field3", IssuanceDetail.Field3);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field4", IssuanceDetail.Field4);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field5", IssuanceDetail.Field5);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field6", IssuanceDetail.Field6);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field7", IssuanceDetail.Field7);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field8", IssuanceDetail.Field8);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Field9", IssuanceDetail.Field9);
                DT1.Rows.Add("Inventory.IssuanceDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteIssuanceDetail(IssuanceDetail IssuanceDetail)
            {                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Inventory.IssuanceDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Inventory.IssuanceDetail", "cond", "LineNumber", IssuanceDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("SELECT * FROM Inventory.IssuanceDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Inventory.Issuance", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Inventory.Issuance", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Inventory.Issuance WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                TransType = dtRow["TransType"].ToString();
                Type = dtRow["Type"].ToString();
                RefNumber = dtRow["RefNumber"].ToString();
                IssuedTo = dtRow["IssuedTo"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                CostCenter = dtRow["CostCenter"].ToString();
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);
                IsCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsCharge"]) ? false : dtRow["IsCharge"]);
                IsIssued = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsIssued"]) ? false : dtRow["IsIssued"]);
                IsReplacement = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsReplacement"]) ? false : dtRow["IsReplacement"]);
                IsWithReference = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithReference"]) ? false : dtRow["IsWithReference"]);
                MaterialType = dtRow["MaterialType"].ToString();
                SpecificType = dtRow["SpecificType"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                ReqJONumber = dtRow["ReqJONumber"].ToString();
                ReqJOStep = dtRow["ReqJOStep"].ToString();
                Currency = dtRow["Currency"].ToString();
                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                ComplimentaryType = dtRow["ComplimentaryType"].ToString();
                ReferenceDIS = dtRow["ReferenceDIS"].ToString();
                SamplesTo = dtRow["SamplesTo"].ToString();
                SamplesType = dtRow["SamplesType"].ToString();
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
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
            }

            return a;
        }
        public void InsertData(Issuance _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            trans = _ent.Transaction;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.Issuance", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.Issuance", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.Issuance", "0", "TransType", _ent.TransType);
            DT1.Rows.Add("Inventory.Issuance", "0", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Inventory.Issuance", "0", "IssuedTo", _ent.IssuedTo);
            DT1.Rows.Add("Inventory.Issuance", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Inventory.Issuance", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.Issuance", "0", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Inventory.Issuance", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.Issuance", "0", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.Issuance", "0", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Inventory.Issuance", "0", "IsCharge", _ent.IsCharge);
            DT1.Rows.Add("Inventory.Issuance", "0", "IsIssued", _ent.IsIssued);
            DT1.Rows.Add("Inventory.Issuance", "0", "IsReplacement", _ent.IsReplacement);
            DT1.Rows.Add("Inventory.Issuance", "0", "IsWithReference", _ent.IsWithReference);
            DT1.Rows.Add("Inventory.Issuance", "0", "MaterialType", _ent.MaterialType);
            DT1.Rows.Add("Inventory.Issuance", "0", "SpecificType", _ent.SpecificType);
            DT1.Rows.Add("Inventory.Issuance", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Inventory.Issuance", "0", "ReqJONumber", _ent.ReqJONumber);
            DT1.Rows.Add("Inventory.Issuance", "0", "ReqJOStep", _ent.ReqJOStep);
            DT1.Rows.Add("Inventory.Issuance", "0", "ComplimentaryType", _ent.ComplimentaryType);
            DT1.Rows.Add("Inventory.Issuance", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Inventory.Issuance", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Inventory.Issuance", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Inventory.Issuance", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Inventory.Issuance", "0", "ReferenceDIS", _ent.ReferenceDIS);
            DT1.Rows.Add("Inventory.Issuance", "0", "SamplesTo", _ent.SamplesTo);
            DT1.Rows.Add("Inventory.Issuance", "0", "SamplesType", _ent.SamplesType);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.Issuance", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.Issuance", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.Issuance", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(Issuance _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.Issuance", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Inventory.Issuance", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Inventory.Issuance", "set", "TransType", _ent.TransType);
            DT1.Rows.Add("Inventory.Issuance", "set", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Inventory.Issuance", "set", "IssuedTo", _ent.IssuedTo);
            DT1.Rows.Add("Inventory.Issuance", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Inventory.Issuance", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Inventory.Issuance", "set", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Inventory.Issuance", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Inventory.Issuance", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Inventory.Issuance", "set", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Inventory.Issuance", "set", "IsWithReference", _ent.IsWithReference);
            DT1.Rows.Add("Inventory.Issuance", "set", "IsCharge", _ent.IsCharge);
            DT1.Rows.Add("Inventory.Issuance", "set", "IsIssued", _ent.IsIssued);
            DT1.Rows.Add("Inventory.Issuance", "set", "IsReplacement", _ent.IsReplacement);
            DT1.Rows.Add("Inventory.Issuance", "set", "MaterialType", _ent.MaterialType);
            DT1.Rows.Add("Inventory.Issuance", "set", "SpecificType", _ent.SpecificType);
            DT1.Rows.Add("Inventory.Issuance", "set", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Inventory.Issuance", "set", "ReqJONumber", _ent.ReqJONumber);
            DT1.Rows.Add("Inventory.Issuance", "set", "ReqJOStep", _ent.ReqJOStep);
            DT1.Rows.Add("Inventory.Issuance", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Inventory.Issuance", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Inventory.Issuance", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Inventory.Issuance", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Inventory.Issuance", "set", "ComplimentaryType", _ent.ComplimentaryType);
            DT1.Rows.Add("Inventory.Issuance", "set", "ReferenceDIS", _ent.ReferenceDIS);
            DT1.Rows.Add("Inventory.Issuance", "set", "SamplesTo", _ent.SamplesTo);
            DT1.Rows.Add("Inventory.Issuance", "set", "SamplesType", _ent.SamplesType);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.Issuance", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.Issuance", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.Issuance", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Issuance _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Inventory.Issuance", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Inventory.IssuanceDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }        
        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Inventory.IssuanceDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }

  
    }
}
