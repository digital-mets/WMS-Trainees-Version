using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PurchaseCostChange
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber {get; set;}
        public virtual string DocDate {get; set;}
        public virtual string SupplierCode {get; set;}
        public virtual string Broker {get; set;}
        public virtual string ReferenceRRNo {get; set;}
        public virtual string APMemo {get; set;}
        public virtual string Remarks {get; set;}
        public virtual decimal NewRate {get; set;}
        public virtual decimal OldTotalAmount {get; set;}
        public virtual decimal NewTotalAmount {get; set;}
        public virtual decimal OldRate {get; set;}
        public virtual decimal OldFreight {get; set;}
        public virtual decimal NewFreight {get; set;}
        public virtual string AddedBy {get; set;}
        public virtual string AddedDate {get; set;}
        public virtual string LastEditedBy {get; set;}
        public virtual string LastEditedDate {get; set;}
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string Field1 {get; set;}
        public virtual string Field2 {get; set;}
        public virtual string Field3 {get; set;}
        public virtual string Field4 {get; set;}
        public virtual string Field5 {get; set;}
        public virtual string Field6 {get; set;}
        public virtual string Field7 {get; set;}
        public virtual string Field8 {get; set;}
        public virtual string Field9 {get; set;}
        public virtual IList<PurchaseCostChangeDetail> Detail { get; set; }

        public class PurchaseCostChangeDetail
        {
            public virtual PurchaseCostChange Parent { get; set; }
            public virtual string DocNumber {get; set;}
            public virtual string LineNumber { get; set; }
            public virtual string PODocNumber {get; set;}
            public virtual string ItemCode {get; set;}
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode {get; set;}
            public virtual string ClassCode {get; set;}
            public virtual string SizeCode {get; set;}
            public virtual decimal Qty { get; set; }
            public virtual decimal UnitCost {get; set;}
            public virtual decimal UnitFreight {get; set;}
            public virtual decimal NewUnitCost { get; set; }
            public virtual decimal NewUnitFreight { get; set; }
            public virtual string Field1 {get; set;}
            public virtual string Field2 {get; set;}
            public virtual string Field3 {get; set;}
            public virtual string Field4 {get; set;}
            public virtual string Field5 {get; set;}
            public virtual string Field6 {get; set;}
            public virtual string Field7 {get; set;}
            public virtual string Field8 { get; set; }
            public virtual string Field9 {get; set;}
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,b.FullDesc from Procurement.PurchaseCostChangeDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchaseCostChangeDetail(PurchaseCostChangeDetail PurchaseCostChangeDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseCostChangeDetail where docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "PODocNumber", PurchaseCostChangeDetail.PODocNumber);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "ItemCode", PurchaseCostChangeDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "ColorCode", PurchaseCostChangeDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "ClassCode", PurchaseCostChangeDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "SizeCode", PurchaseCostChangeDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Qty", PurchaseCostChangeDetail.Qty);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "UnitCost", PurchaseCostChangeDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "UnitFreight", PurchaseCostChangeDetail.UnitFreight);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "NewUnitCost", PurchaseCostChangeDetail.NewUnitCost);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "NewUnitFreight", PurchaseCostChangeDetail.NewUnitFreight);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field1", PurchaseCostChangeDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field2", PurchaseCostChangeDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field3", PurchaseCostChangeDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field4", PurchaseCostChangeDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field5", PurchaseCostChangeDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field6", PurchaseCostChangeDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field7", PurchaseCostChangeDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field8", PurchaseCostChangeDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "0", "Field9", PurchaseCostChangeDetail.Field9);

                DT2.Rows.Add("Procurement.PurchaseCostChange", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseCostChange", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdatePurchaseCostChangeDetail(PurchaseCostChangeDetail PurchaseCostChangeDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "cond", "LineNumber", PurchaseCostChangeDetail.LineNumber);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "cond", "PODocNumber", PurchaseCostChangeDetail.PODocNumber);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "ItemCode", PurchaseCostChangeDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "ColorCode", PurchaseCostChangeDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "ClassCode", PurchaseCostChangeDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "SizeCode", PurchaseCostChangeDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Qty", PurchaseCostChangeDetail.Qty);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "UnitCost", PurchaseCostChangeDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "UnitFreight", PurchaseCostChangeDetail.UnitFreight);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "NewUnitCost", PurchaseCostChangeDetail.NewUnitCost);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "NewUnitFreight", PurchaseCostChangeDetail.NewUnitFreight);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field1", PurchaseCostChangeDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field2", PurchaseCostChangeDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field3", PurchaseCostChangeDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field4", PurchaseCostChangeDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field5", PurchaseCostChangeDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field6", PurchaseCostChangeDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field7", PurchaseCostChangeDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field8", PurchaseCostChangeDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "set", "Field9", PurchaseCostChangeDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }

            public void DeletePurchaseCostChangeDetail(PurchaseCostChangeDetail PurchaseCostChangeDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "cond", "DocNumber", PurchaseCostChangeDetail.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "cond", "LineNumber", PurchaseCostChangeDetail.LineNumber);

                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseCostChangeDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseCostChange", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseCostChange", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }

            public void DeleteAllPurchaseCostChangeDetail(PurchaseCostChangeDetail PurchaseCostChangeDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseCostChangeDetail", "cond", "DocNumber", PurchaseCostChangeDetail.DocNumber);

                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseCostChangeDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseCostChange", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseCostChange", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }  

        }

        public class RefTransaction
        {
            public virtual PurchaseCostChange Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='PRCPCC' OR  A.TransType='PRCPCC') ", Conn);
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
            a = Gears.RetriveData2("select * from Procurement.PurchaseCostChange where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                Broker = dtRow["Broker"].ToString();
                ReferenceRRNo = dtRow["ReferenceRRNo"].ToString();
                APMemo = dtRow["APMemo"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                NewRate = string.IsNullOrEmpty(dtRow["NewRate"].ToString()) ? 0 : Convert.ToDecimal(dtRow["NewRate"].ToString());
                OldTotalAmount = string.IsNullOrEmpty(dtRow["OldTotalAmount"].ToString()) ? 0 : Convert.ToDecimal(dtRow["OldTotalAmount"].ToString());
                NewTotalAmount = string.IsNullOrEmpty(dtRow["NewTotalAmount"].ToString()) ? 0 : Convert.ToDecimal(dtRow["NewTotalAmount"].ToString());
                OldRate = string.IsNullOrEmpty(dtRow["OldRate"].ToString()) ? 0 : Convert.ToDecimal(dtRow["OldRate"].ToString());
                OldFreight = string.IsNullOrEmpty(dtRow["OldFreight"].ToString()) ? 0 : Convert.ToDecimal(dtRow["OldFreight"].ToString());
                NewFreight = string.IsNullOrEmpty(dtRow["NewFreight"].ToString()) ? 0 : Convert.ToDecimal(dtRow["NewFreight"].ToString());
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
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
            }
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as ExpectedDeliveryDate,'' as WarehouseCode,'' as StorerKey,'' as Field1"+
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(PurchaseCostChange _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Broker", _ent.Broker);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "ReferenceRRNo", _ent.ReferenceRRNo);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "APMemo", _ent.APMemo);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "NewRate", _ent.NewRate);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "OldTotalAmount", _ent.OldTotalAmount);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "NewTotalAmount", _ent.NewTotalAmount);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "OldRate", _ent.OldRate);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "OldFreight", _ent.OldFreight);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "NewFreight", _ent.NewFreight);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Procurement.PurchaseCostChange", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(PurchaseCostChange _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.PurchaseCostChange", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Broker", _ent.Broker);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "ReferenceRRNo", _ent.ReferenceRRNo);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "APMemo", _ent.APMemo);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "NewRate", _ent.NewRate);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "OldTotalAmount", _ent.OldTotalAmount);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "NewTotalAmount", _ent.NewTotalAmount);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "OldRate", _ent.OldRate);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "OldFreight", _ent.OldFreight);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "NewFreight", _ent.NewFreight);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.PurchaseCostChange", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PROCPCC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(PurchaseCostChange _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.PurchaseCostChange", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PROCPCC", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public class JournalEntry
        {
            public virtual PurchaseCostChange Parent { get; set; }
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
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit , A.BizpartnerCode FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='PRCPCC' ", Conn);

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
