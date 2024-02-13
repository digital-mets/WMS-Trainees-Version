using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BillingMD
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }

        private static string Docnum;
        public virtual string DocNumber { get; set; }
		public virtual string BizPartnerCode { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string DateFrom { get; set; }
        public virtual string DateTo { get; set; }
        public virtual string BillingPeriodType { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string BillingStatement { get; set; }
        public virtual string ProdNumber { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal TotalVat { get; set; }
        public virtual decimal TotalGross { get; set; }
        public virtual decimal BeginningInv { get; set; }
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
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual IList<BillingDetail> Detail { get; set; }

        #region Billing Header
        public DataTable getdata(string DocNumber, string Conn)//KMM
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT A.*, B.FullName AS Added, C.FullName AS LastEdited, D.FullName AS Submitted, E.FullName AS Posted FROM WMS.Billing A "
                + " LEFT JOIN IT.Users B ON A.AddedBy = B.UserID LEFT JOIN IT.Users C ON A.LastEditedBy = C.UserID  LEFT JOIN IT.Users D ON A.SubmittedBy = D.UserID "
                + " LEFT JOIN IT.Users E ON A.PostedBy = E.UserID WHERE DocNumber = '" + DocNumber + "' ", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                ServiceType = dtRow["ServiceType"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                DateFrom = dtRow["DateFrom"].ToString();
                DateTo = dtRow["DateTo"].ToString();
                BillingPeriodType = dtRow["BillingPeriodType"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                BillingStatement = dtRow["BillingStatement"].ToString();
                ProdNumber = dtRow["ProdNumber"].ToString();
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                TotalVat = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalVat"]) ? 0 : dtRow["TotalVat"]);
                TotalGross = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGross"]) ? 0 : dtRow["TotalGross"]);
                BeginningInv = Convert.ToDecimal(Convert.IsDBNull(dtRow["BeginningInv"]) ? 0 : dtRow["BeginningInv"]);

                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();

                AddedBy = dtRow["Added"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEdited"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["Submitted"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                PostedBy = dtRow["Posted"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
            }

            return a;
        }
        public void InsertData(BillingMD _ent)
        {
            Conn = _ent.Connection;
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.Billing", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Billing", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Billing", "0", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.Billing", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.Billing", "0", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.Billing", "0", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.Billing", "0", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.Billing", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Billing", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.Billing", "0", "BillingStatement", _ent.BillingStatement);
            DT1.Rows.Add("WMS.Billing", "0", "ProdNumber", _ent.ProdNumber);
            DT1.Rows.Add("WMS.Billing", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.Billing", "0", "TotalVat", _ent.TotalVat);
            DT1.Rows.Add("WMS.Billing", "0", "TotalGross", _ent.TotalGross);
            DT1.Rows.Add("WMS.Billing", "0", "BeginningInv", _ent.BeginningInv);

            DT1.Rows.Add("WMS.Billing", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Billing", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Billing", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Billing", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Billing", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Billing", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Billing", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Billing", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Billing", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("WMS.Billing", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.Billing", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); 
        }
        public void UpdateData(BillingMD _ent)
        {
            Conn = _ent.Connection;
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Billing", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Billing", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Billing", "set", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.Billing", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.Billing", "set", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.Billing", "set", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.Billing", "set", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.Billing", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Billing", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.Billing", "set", "BillingStatement", _ent.BillingStatement);
            DT1.Rows.Add("WMS.Billing", "set", "ProdNumber", _ent.ProdNumber);
            DT1.Rows.Add("WMS.Billing", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.Billing", "set", "TotalVat", _ent.TotalVat);
            DT1.Rows.Add("WMS.Billing", "set", "TotalGross", _ent.TotalGross);
            DT1.Rows.Add("WMS.Billing", "set", "BeginningInv", _ent.BeginningInv);

            DT1.Rows.Add("WMS.Billing", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.Billing", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("WMS.Billing", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Billing", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Billing", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Billing", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Billing", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Billing", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Billing", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Billing", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Billing", "set", "Field9", _ent.Field9);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSBMD", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); 
        }
        public void DeleteData(BillingMD _ent)
        {
            Conn = _ent.Connection;
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Billing", "cond", "DocNumber", _ent.DocNumber);

            Gears.DeleteData(DT1, _ent.Connection); 
            Functions.AuditTrail("WMSBMD", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); 
        }
        #endregion


        #region Billing Detail
        public class BillingDetail 
        {
            public virtual BillingMD Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual DateTime Date { get; set; }
            public virtual string DocIn { get; set; }
            public virtual string DocOut { get; set; }
            public virtual decimal QtyIn { get; set; }
            public virtual decimal QtyOut { get; set; }
            public virtual decimal EndingBal { get; set; }
            public virtual decimal ChargeableEndBal { get; set; }
            public virtual decimal StorageCharge { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual decimal Vat { get; set; }
            public virtual decimal GrossAmount { get; set; }
            public virtual string ProdNum { get; set; }
            public virtual DateTime ProdDate { get; set; }
            public virtual string RRNum { get; set; }
            public virtual DateTime RRDate { get; set; }
            public virtual decimal StorageRate { get; set; }
            public virtual decimal MinimumQty { get; set; }
            public virtual bool NoStorageCharge { get; set; }
            public virtual int InventoryDays { get; set; }
            public virtual int NoChargeDays { get; set; }
            public virtual string RefContract { get; set; }
            public virtual bool ExcludeInBilling { get; set; }
            public virtual string Remarks { get; set; }
            public virtual string RefRecordID { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }


            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM WMS.BillingDetail WHERE DocNumber='" + DocNumber + "' ORDER BY Date", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddBillingDetail(BillingDetail BillingDetail)
            {                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.BillingDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Date", BillingDetail.Date);
                DT1.Rows.Add("WMS.BillingDetail", "0", "DocIn", BillingDetail.DocIn);
                DT1.Rows.Add("WMS.BillingDetail", "0", "DocOut", BillingDetail.DocOut);
                DT1.Rows.Add("WMS.BillingDetail", "0", "QtyIn", BillingDetail.QtyIn);
                DT1.Rows.Add("WMS.BillingDetail", "0", "QtyOut", BillingDetail.QtyOut);
                DT1.Rows.Add("WMS.BillingDetail", "0", "EndingBal", BillingDetail.EndingBal);
                DT1.Rows.Add("WMS.BillingDetail", "0", "ChargeableEndBal", BillingDetail.ChargeableEndBal);
                DT1.Rows.Add("WMS.BillingDetail", "0", "StorageCharge", BillingDetail.StorageCharge);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Amount", BillingDetail.Amount);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Vat", BillingDetail.Vat);
                DT1.Rows.Add("WMS.BillingDetail", "0", "GrossAmount", BillingDetail.GrossAmount);
                DT1.Rows.Add("WMS.BillingDetail", "0", "ProdNum", BillingDetail.ProdNum);
                if (Convert.ToString(BillingDetail.ProdDate) != "1/1/0001 12:00:00 AM" && BillingDetail.ProdDate != null && Convert.ToString(BillingDetail.ProdDate) != "")
                {
                    DT1.Rows.Add("WMS.BillingDetail", "0", "ProdDate", BillingDetail.ProdDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "0", "RRNum", BillingDetail.RRNum);
                if (Convert.ToString(BillingDetail.RRDate) != "1/1/0001 12:00:00 AM" && BillingDetail.RRDate != null && Convert.ToString(BillingDetail.RRDate) != "")
                {
                    DT1.Rows.Add("WMS.BillingDetail", "0", "RRDate", BillingDetail.RRDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "0", "StorageRate", BillingDetail.StorageRate);
                DT1.Rows.Add("WMS.BillingDetail", "0", "MinimumQty", BillingDetail.MinimumQty);
                DT1.Rows.Add("WMS.BillingDetail", "0", "NoStorageCharge", BillingDetail.NoStorageCharge);
                DT1.Rows.Add("WMS.BillingDetail", "0", "InventoryDays", BillingDetail.InventoryDays);
                DT1.Rows.Add("WMS.BillingDetail", "0", "NoChargeDays", BillingDetail.NoChargeDays);
                DT1.Rows.Add("WMS.BillingDetail", "0", "RefContract", BillingDetail.RefContract);
                DT1.Rows.Add("WMS.BillingDetail", "0", "ExcludeInBilling", BillingDetail.ExcludeInBilling);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Remarks", BillingDetail.Remarks);
                DT1.Rows.Add("WMS.BillingDetail", "0", "RefRecordID", BillingDetail.RefRecordID);              

                DT1.Rows.Add("WMS.BillingDetail", "0", "Field1", BillingDetail.Field1);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field2", BillingDetail.Field2);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field3", BillingDetail.Field3);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field4", BillingDetail.Field4);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field5", BillingDetail.Field5);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field6", BillingDetail.Field6);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field7", BillingDetail.Field7);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field8", BillingDetail.Field8);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Field9", BillingDetail.Field9);

                DT2.Rows.Add("WMS.Billing", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.Billing", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
               
            }
            public void UpdateBillingDetail(BillingDetail BillingDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.BillingDetail", "cond", "DocNumber", Docnum); 
                DT1.Rows.Add("WMS.BillingDetail", "set", "Date", BillingDetail.Date);
                DT1.Rows.Add("WMS.BillingDetail", "set", "DocIn", BillingDetail.DocIn);
                DT1.Rows.Add("WMS.BillingDetail", "set", "DocOut", BillingDetail.DocOut);
                DT1.Rows.Add("WMS.BillingDetail", "set", "QtyIn", BillingDetail.QtyIn);
                DT1.Rows.Add("WMS.BillingDetail", "set", "QtyOut", BillingDetail.QtyOut);
                DT1.Rows.Add("WMS.BillingDetail", "set", "EndingBal", BillingDetail.EndingBal);
                DT1.Rows.Add("WMS.BillingDetail", "set", "ChargeableEndBal", BillingDetail.ChargeableEndBal);
                DT1.Rows.Add("WMS.BillingDetail", "set", "StorageCharge", BillingDetail.StorageCharge);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Amount", BillingDetail.Amount);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Vat", BillingDetail.Vat);
                DT1.Rows.Add("WMS.BillingDetail", "set", "GrossAmount", BillingDetail.GrossAmount);
                DT1.Rows.Add("WMS.BillingDetail", "set", "ProdNum", BillingDetail.ProdNum);
                if (Convert.ToString(BillingDetail.ProdDate) != "1/1/0001 12:00:00 AM" && BillingDetail.ProdDate != null && Convert.ToString(BillingDetail.ProdDate) != "")
                {
                    DT1.Rows.Add("WMS.BillingDetail", "set", "ProdDate", BillingDetail.ProdDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "set", "RRNum", BillingDetail.RRNum);
                if (Convert.ToString(BillingDetail.RRDate) != "1/1/0001 12:00:00 AM" && BillingDetail.RRDate != null && Convert.ToString(BillingDetail.RRDate) != "")
                {
                    DT1.Rows.Add("WMS.BillingDetail", "set", "RRDate", BillingDetail.RRDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "set", "StorageRate", BillingDetail.StorageRate);
                DT1.Rows.Add("WMS.BillingDetail", "set", "MinimumQty", BillingDetail.MinimumQty);
                DT1.Rows.Add("WMS.BillingDetail", "set", "NoStorageCharge", BillingDetail.NoStorageCharge);
                DT1.Rows.Add("WMS.BillingDetail", "set", "InventoryDays", BillingDetail.InventoryDays);
                DT1.Rows.Add("WMS.BillingDetail", "set", "NoChargeDays", BillingDetail.NoChargeDays);
                DT1.Rows.Add("WMS.BillingDetail", "set", "RefContract", BillingDetail.RefContract);
                DT1.Rows.Add("WMS.BillingDetail", "set", "ExcludeInBilling", BillingDetail.ExcludeInBilling);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Remarks", BillingDetail.Remarks);
                DT1.Rows.Add("WMS.BillingDetail", "set", "RefRecordID", BillingDetail.RefRecordID);

                DT1.Rows.Add("WMS.BillingDetail", "set", "Field1", BillingDetail.Field1);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field2", BillingDetail.Field2);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field3", BillingDetail.Field3);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field4", BillingDetail.Field4);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field5", BillingDetail.Field5);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field6", BillingDetail.Field6);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field7", BillingDetail.Field7);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field8", BillingDetail.Field8);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Field9", BillingDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteBillingDetail(BillingDetail BillingDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.BillingDetail", "cond", "DocNumber", Docnum);

                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM WMS.BillingDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.Billing", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.Billing", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }   
        }
        
        #endregion
        

        #region Journal Entries
        public class JournalEntry
        {
            private static string Conn { get; set; }
            public virtual string Connection { get; set; }
            public virtual BillingMD Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='WMSBMD' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        #endregion
    }
}
