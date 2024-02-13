using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Billing
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }

        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string ContractNumber { get; set; }
        public virtual string BillingPeriodType { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string DateFrom { get; set; }
        public virtual string DateTo { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string BillingStatement { get; set; }
        public virtual string UnitOfMeasure { get; set; }
        public virtual decimal BeginningInv { get; set; }
        public virtual decimal StorageRate { get; set; }
        public virtual decimal MinHandlingIn { get; set; }
        public virtual decimal MinHandlingOut { get; set; }
        public virtual decimal HandlingInRate { get; set; }
        public virtual decimal HandlingOutRate { get; set; }
        public virtual decimal MinStorage { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal TotalVat { get; set; }
        public virtual decimal TotalGross { get; set; }
        public virtual string BillingType { get; set; }
        public virtual string StorageCode { get; set; }
        public virtual string CustomerCode { get; set; }
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
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                BillingPeriodType = dtRow["BillingPeriodType"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                DateFrom = dtRow["DateFrom"].ToString();
                DateTo = dtRow["DateTo"].ToString();
                ServiceType = dtRow["ServiceType"].ToString();
                ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                BillingStatement = dtRow["BillingStatement"].ToString();
                BeginningInv = Convert.ToDecimal(Convert.IsDBNull(dtRow["BeginningInv"]) ? 0 : dtRow["BeginningInv"]);
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                TotalVat = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalVat"]) ? 0 : dtRow["TotalVat"]);
                TotalGross = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGross"]) ? 0 : dtRow["TotalGross"]);
                CustomerCode = dtRow["CustomerCode"].ToString();
                StorageCode = dtRow["StorageCode"].ToString();
                //ContractNumber = dtRow["ContractNumber"].ToString();
                //UnitOfMeasure = dtRow["UnitOfMeasure"].ToString();
                //StorageRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["StorageRate"]) ? 0 : dtRow["StorageRate"]);
                //MinHandlingIn = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingIn"]) ? 0 : dtRow["MinHandlingIn"]);
                //MinHandlingOut = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingOut"]) ? 0 : dtRow["MinHandlingOut"]);
                //HandlingInRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingInRate"]) ? 0 : dtRow["HandlingInRate"]);
                //HandlingOutRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingOutRate"]) ? 0 : dtRow["HandlingOutRate"]);
                //MinStorage = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinStorage"]) ? 0 : dtRow["MinStorage"]);
                //BillingType = dtRow["BillingType"].ToString();

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
        public void InsertData(Billing _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.Billing", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Billing", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Billing", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.Billing", "0", "WarehouseCode", _ent.WarehouseCode);
            //DT1.Rows.Add("WMS.Billing", "0", "ContractNumber", _ent.ContractNumber);
            DT1.Rows.Add("WMS.Billing", "0", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.Billing", "0", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.Billing", "0", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.Billing", "0", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.Billing", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.Billing", "0", "BillingStatement", _ent.BillingStatement);
            DT1.Rows.Add("WMS.Billing", "0", "BeginningInv", _ent.BeginningInv);
            DT1.Rows.Add("WMS.Billing", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.Billing", "0", "TotalVat", _ent.TotalVat);
            DT1.Rows.Add("WMS.Billing", "0", "TotalGross", _ent.TotalGross);
            DT1.Rows.Add("WMS.Billing", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.Billing", "0", "StorageCode", _ent.StorageCode);

            //DT1.Rows.Add("WMS.Billing", "0", "UnitOfMeasure", _ent.UnitOfMeasure);
            //DT1.Rows.Add("WMS.Billing", "0", "StorageRate", _ent.StorageRate);
            //DT1.Rows.Add("WMS.Billing", "0", "MinHandlingIn", _ent.MinHandlingIn);
            //DT1.Rows.Add("WMS.Billing", "0", "MinHandlingOut", _ent.MinHandlingOut);
            //DT1.Rows.Add("WMS.Billing", "0", "HandlingInRate", _ent.HandlingInRate);
            //DT1.Rows.Add("WMS.Billing", "0", "HandlingOutRate", _ent.HandlingOutRate);
            //DT1.Rows.Add("WMS.Billing", "0", "MinStorage", _ent.MinStorage);
            //DT1.Rows.Add("WMS.Billing", "0", "BillingType", _ent.BillingType);

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

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }
        public void UpdateData(Billing _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Billing", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Billing", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Billing", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.Billing", "set", "WarehouseCode", _ent.WarehouseCode);
            //DT1.Rows.Add("WMS.Billing", "set", "ContractNumber", _ent.ContractNumber);
            DT1.Rows.Add("WMS.Billing", "set", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.Billing", "set", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.Billing", "set", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.Billing", "set", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.Billing", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.Billing", "set", "BillingStatement", _ent.BillingStatement);
            DT1.Rows.Add("WMS.Billing", "set", "BeginningInv", _ent.BeginningInv);
            DT1.Rows.Add("WMS.Billing", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.Billing", "set", "TotalVat", _ent.TotalVat);
            DT1.Rows.Add("WMS.Billing", "set", "TotalGross", _ent.TotalGross);
            DT1.Rows.Add("WMS.Billing", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.Billing", "set", "StorageCode", _ent.StorageCode);

            //DT1.Rows.Add("WMS.Billing", "set", "UnitOfMeasure", _ent.UnitOfMeasure);
            //DT1.Rows.Add("WMS.Billing", "set", "StorageRate", _ent.StorageRate);
            //DT1.Rows.Add("WMS.Billing", "set", "MinHandlingIn", _ent.MinHandlingIn);
            //DT1.Rows.Add("WMS.Billing", "set", "MinHandlingOut", _ent.MinHandlingOut);
            //DT1.Rows.Add("WMS.Billing", "set", "HandlingInRate", _ent.HandlingInRate);
            //DT1.Rows.Add("WMS.Billing", "set", "HandlingOutRate", _ent.HandlingOutRate);
            //DT1.Rows.Add("WMS.Billing", "set", "MinStorage", _ent.MinStorage);
            //DT1.Rows.Add("WMS.Billing", "set", "BillingType", _ent.BillingType);

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

            Functions.AuditTrail("WMSBIL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        public void DeleteData(Billing _ent)
        {
            Conn = _ent.Connection;
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Billing", "cond", "DocNumber", _ent.DocNumber);

            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSBIL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }
        #endregion


        #region Billing Detail
        public class BillingDetail
        {
            public virtual Billing Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual DateTime Date { get; set; }
            public virtual string DocIn { get; set; }
            public virtual string DocOut { get; set; }
            public virtual decimal QtyIn { get; set; }
            public virtual decimal QtyOut { get; set; }
            public virtual decimal EndingBal { get; set; }
            public virtual decimal ChargeableEndBal { get; set; }
            public virtual decimal StorageCharge { get; set; }
            public virtual decimal Holding { get; set; }
            public virtual decimal HandlingIn { get; set; }
            public virtual decimal HandlingOut { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual decimal Vat { get; set; }
            public virtual decimal GrossAmount { get; set; }
            public virtual decimal CQtyIn { get; set; }
            public virtual decimal CQtyOut { get; set; }
            public virtual string UOM { get; set; }
            public virtual string BillingType { get; set; }
            public virtual decimal StorageRate { get; set; }
            public virtual decimal HandlingInRate { get; set; }
            public virtual decimal HandlingOutRate { get; set; }
            public virtual decimal MinimumQty { get; set; }
            public virtual decimal MinHandlingIn { get; set; }
            public virtual decimal MinHandlingOut { get; set; }
            public virtual DateTime RRDate { get; set; }
            public virtual string ProdNum { get; set; }
            public virtual DateTime ProdDate { get; set; }
            public virtual bool NoStorageCharge { get; set; }
            public virtual bool NoHandlingCharge { get; set; }
            public virtual string RefContract { get; set; }
            public virtual bool ExcludeInBilling { get; set; }
            public virtual string Remarks { get; set; }
            public virtual string RefRecordID { get; set; }
            public virtual string MulStorageCode { get; set; }
            public virtual string MulCustomerCode { get; set; }
            public virtual DateTime AllocationDate { get; set; }
            public virtual int AllocChargeable { get; set; }
            public virtual int Staging { get; set; }
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
                DT1.Rows.Add("WMS.BillingDetail", "0", "Holding", BillingDetail.Holding);
                DT1.Rows.Add("WMS.BillingDetail", "0", "HandlingIn", BillingDetail.HandlingIn);
                DT1.Rows.Add("WMS.BillingDetail", "0", "HandlingOut", BillingDetail.HandlingOut);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Amount", BillingDetail.Amount);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Vat", BillingDetail.Vat);
                DT1.Rows.Add("WMS.BillingDetail", "0", "GrossAmount", BillingDetail.GrossAmount);
                DT1.Rows.Add("WMS.BillingDetail", "0", "CQtyIn", BillingDetail.CQtyIn);
                DT1.Rows.Add("WMS.BillingDetail", "0", "CQtyOut", BillingDetail.CQtyOut);
                DT1.Rows.Add("WMS.BillingDetail", "0", "UOM", BillingDetail.UOM);
                DT1.Rows.Add("WMS.BillingDetail", "0", "BillingType", BillingDetail.BillingType);
                DT1.Rows.Add("WMS.BillingDetail", "0", "StorageRate", BillingDetail.StorageRate);
                DT1.Rows.Add("WMS.BillingDetail", "0", "HandlingInRate", BillingDetail.HandlingInRate);
                DT1.Rows.Add("WMS.BillingDetail", "0", "HandlingOutRate", BillingDetail.HandlingOutRate);
                DT1.Rows.Add("WMS.BillingDetail", "0", "MinimumQty", BillingDetail.MinimumQty);
                DT1.Rows.Add("WMS.BillingDetail", "0", "MinHandlingIn", BillingDetail.MinHandlingIn);
                DT1.Rows.Add("WMS.BillingDetail", "0", "MinHandlingOut", BillingDetail.MinHandlingOut);

                if (BillingDetail.RRDate != DateTime.MinValue && BillingDetail.RRDate != null)
                {
                    DT1.Rows.Add("WMS.BillingDetail", "0", "RRDate", BillingDetail.RRDate);
                }

                DT1.Rows.Add("WMS.BillingDetail", "0", "ProdNum", BillingDetail.ProdNum);
                if (BillingDetail.ProdDate != DateTime.MinValue && BillingDetail.ProdDate != null)
                {
                    DT1.Rows.Add("WMS.BillingDetail", "0", "ProdDate", BillingDetail.ProdDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "0", "NoStorageCharge", BillingDetail.NoStorageCharge);
                DT1.Rows.Add("WMS.BillingDetail", "0", "NoHandlingCharge", BillingDetail.NoHandlingCharge);
                DT1.Rows.Add("WMS.BillingDetail", "0", "RefContract", BillingDetail.RefContract);
                DT1.Rows.Add("WMS.BillingDetail", "0", "ExcludeInBilling", BillingDetail.ExcludeInBilling);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Remarks", BillingDetail.Remarks);
                DT1.Rows.Add("WMS.BillingDetail", "0", "RefRecordID", BillingDetail.RefRecordID);
                DT1.Rows.Add("WMS.BillingDetail", "0", "MulStorageCode", BillingDetail.MulStorageCode);
                DT1.Rows.Add("WMS.BillingDetail", "0", "MulCustomerCode", BillingDetail.MulCustomerCode);
                if (BillingDetail.AllocationDate != DateTime.MinValue && BillingDetail.AllocationDate != null)
                {
                    DT1.Rows.Add("WMS.BillingDetail", "0", "AllocationDate", BillingDetail.AllocationDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "0", "AllocChargeable", BillingDetail.AllocChargeable);
                DT1.Rows.Add("WMS.BillingDetail", "0", "Staging", BillingDetail.Staging);

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
                //DT1.Rows.Add("WMS.BillingDetail", "cond", "LineNumber", BillingDetail.LineNumber);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Date", BillingDetail.Date);
                DT1.Rows.Add("WMS.BillingDetail", "set", "DocIn", BillingDetail.DocIn);
                DT1.Rows.Add("WMS.BillingDetail", "set", "DocOut", BillingDetail.DocOut);
                DT1.Rows.Add("WMS.BillingDetail", "set", "QtyIn", BillingDetail.QtyIn);
                DT1.Rows.Add("WMS.BillingDetail", "set", "QtyOut", BillingDetail.QtyOut);
                DT1.Rows.Add("WMS.BillingDetail", "set", "EndingBal", BillingDetail.EndingBal);
                DT1.Rows.Add("WMS.BillingDetail", "set", "ChargeableEndBal", BillingDetail.ChargeableEndBal);
                DT1.Rows.Add("WMS.BillingDetail", "set", "StorageCharge", BillingDetail.StorageCharge);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Holding", BillingDetail.Holding);
                DT1.Rows.Add("WMS.BillingDetail", "set", "HandlingIn", BillingDetail.HandlingIn);
                DT1.Rows.Add("WMS.BillingDetail", "set", "HandlingOut", BillingDetail.HandlingOut);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Amount", BillingDetail.Amount);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Vat", BillingDetail.Vat);
                DT1.Rows.Add("WMS.BillingDetail", "set", "GrossAmount", BillingDetail.GrossAmount);
                DT1.Rows.Add("WMS.BillingDetail", "set", "CQtyIn", BillingDetail.CQtyIn);
                DT1.Rows.Add("WMS.BillingDetail", "set", "CQtyOut", BillingDetail.CQtyOut);
                DT1.Rows.Add("WMS.BillingDetail", "set", "UOM", BillingDetail.UOM);
                DT1.Rows.Add("WMS.BillingDetail", "set", "BillingType", BillingDetail.BillingType);
                DT1.Rows.Add("WMS.BillingDetail", "set", "StorageRate", BillingDetail.StorageRate);
                DT1.Rows.Add("WMS.BillingDetail", "set", "HandlingInRate", BillingDetail.HandlingInRate);
                DT1.Rows.Add("WMS.BillingDetail", "set", "HandlingOutRate", BillingDetail.HandlingOutRate);
                DT1.Rows.Add("WMS.BillingDetail", "set", "MinimumQty", BillingDetail.MinimumQty);
                DT1.Rows.Add("WMS.BillingDetail", "set", "MinHandlingIn", BillingDetail.MinHandlingIn);
                DT1.Rows.Add("WMS.BillingDetail", "set", "MinHandlingOut", BillingDetail.MinHandlingOut);
                if (Convert.ToString(BillingDetail.RRDate) != "1/1/0001 12:00:00 AM" && BillingDetail.RRDate != null && Convert.ToString(BillingDetail.RRDate) != "")
                {
                    DT1.Rows.Add("WMS.BillingDetail", "set", "RRDate", BillingDetail.RRDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "set", "ProdNum", BillingDetail.ProdNum);
                if (Convert.ToString(BillingDetail.ProdDate) != "1/1/0001 12:00:00 AM" && BillingDetail.ProdDate != null && Convert.ToString(BillingDetail.ProdDate) != "")
                {
                    DT1.Rows.Add("WMS.BillingDetail", "set", "ProdDate", BillingDetail.ProdDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "set", "NoStorageCharge", BillingDetail.NoStorageCharge);
                DT1.Rows.Add("WMS.BillingDetail", "set", "NoHandlingCharge", BillingDetail.NoHandlingCharge);
                DT1.Rows.Add("WMS.BillingDetail", "set", "RefContract", BillingDetail.RefContract);
                DT1.Rows.Add("WMS.BillingDetail", "set", "ExcludeInBilling", BillingDetail.ExcludeInBilling);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Remarks", BillingDetail.Remarks);
                DT1.Rows.Add("WMS.BillingDetail", "set", "RefRecordID", BillingDetail.RefRecordID);
                DT1.Rows.Add("WMS.BillingDetail", "set", "MulStorageCode", BillingDetail.MulStorageCode);
                DT1.Rows.Add("WMS.BillingDetail", "set", "MulCustomerCode", BillingDetail.MulCustomerCode);
                if (Convert.ToString(BillingDetail.AllocationDate) != "1/1/0001 12:00:00 AM" && BillingDetail.AllocationDate != null && Convert.ToString(BillingDetail.AllocationDate) != "")
                {
                    DT1.Rows.Add("WMS.BillingDetail", "set", "AllocationDate", BillingDetail.AllocationDate);
                }
                DT1.Rows.Add("WMS.BillingDetail", "set", "AllocChargeable", BillingDetail.AllocChargeable);
                DT1.Rows.Add("WMS.BillingDetail", "set", "Staging", BillingDetail.Staging);

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
                //DT1.Rows.Add("WMS.BillingDetail", "cond", "LineNumber", BillingDetail.LineNumber);


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
            public virtual Billing Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='WMSBIL' ", Conn);

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
