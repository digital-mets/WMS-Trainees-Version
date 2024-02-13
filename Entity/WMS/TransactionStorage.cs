using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class TransactionStorage
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string RecordId { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal ChargeableQty { get; set; }
        public virtual string UnitOfMeasure { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual string PalletCount { get; set; }
        public virtual string BillingType { get; set; }
        public virtual decimal StorageRate { get; set; }
        public virtual decimal HandlingInRate { get; set; }
        public virtual decimal HandlingOutRate { get; set; }
        public virtual decimal MinimumQty { get; set; }
        public virtual decimal MinHandlingIn { get; set; }
        public virtual decimal MinHandlingOut { get; set; }
        public virtual string RRDate { get; set; }
        public virtual string ProdNum { get; set; }
        public virtual string ProdDate { get; set; }
        public virtual string BillNumber { get; set; }
        public virtual string SplitBillRecordID { get; set; }
        public virtual bool NoStorageCharge { get; set; }
        public virtual bool IsNoCharge { get; set; }
        public virtual string RefContract { get; set; }
        public virtual bool ExcludeInBilling { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal TransQty { get; set; }
        public virtual string TransUnit { get; set; }
        public virtual string HandlingBillNumber { get; set; }
        public virtual string DateSynced { get; set; }
        public virtual string CompleteUnload { get; set; }
        public virtual string StorageCode { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual int InventoryDays { get; set; }
        public virtual int NoChargeDays { get; set; }
        public virtual string AllocationDate { get; set; }
        public virtual decimal MinStorage { get; set; }
        public virtual int Staging { get; set; }
        public virtual int AllocChargeable { get; set; }
        public virtual bool IsOverridden { get; set; }
        public virtual decimal OverrideQty { get; set; }
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
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }

        public DataTable getdata(string Id, string Conn)
        {
            DataTable a;
            a = Gears.RetriveData2("SELECT A.*, B.FullName AS Added, C.FullName AS LastEdited, D.FullName AS Submitted FROM WMS.TransactionStorage A "
                + " LEFT JOIN IT.Users B ON A.AddedBy = B.UserID LEFT JOIN IT.Users C ON A.LastEditedBy = C.UserID  LEFT JOIN IT.Users D ON A.SubmittedBy = D.UserID "
                + " WHERE RecordId = '" + Id + "'", Conn); //KMM add Conn
            foreach (DataRow dtRow in a.Rows)
            {
                RecordId = dtRow["RecordId"].ToString();
                DocNumber = dtRow["DocNumber"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                Type = dtRow["Type"].ToString();
                Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                ChargeableQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ChargeableQty"]) ? 0 : dtRow["ChargeableQty"]);
                UnitOfMeasure = dtRow["UnitOfMeasure"].ToString();
                ServiceType = dtRow["ServiceType"].ToString();
                PalletCount = dtRow["PalletCount"].ToString();
                BillingType = dtRow["BillingType"].ToString();
                StorageRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["StorageRate"]) ? 0 : dtRow["StorageRate"]);
                HandlingInRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingInRate"]) ? 0 : dtRow["HandlingInRate"]);
                HandlingOutRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingOutRate"]) ? 0 : dtRow["HandlingOutRate"]);
                MinHandlingIn = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingIn"]) ? 0 : dtRow["MinHandlingIn"]);
                MinimumQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinimumQty"]) ? 0 : dtRow["MinimumQty"]);
                MinHandlingOut = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingOut"]) ? 0 : dtRow["MinHandlingOut"]);
                RRDate = dtRow["RRDate"].ToString();
                ProdNum = dtRow["ProdNum"].ToString();
                ProdDate = dtRow["ProdDate"].ToString();
                BillNumber = dtRow["BillNumber"].ToString();
                SplitBillRecordID = dtRow["SplitBillRecordID"].ToString();
                NoStorageCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["NoStorageCharge"]) ? false : dtRow["NoStorageCharge"]);
                IsNoCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsNoCharge"]) ? false : dtRow["IsNoCharge"]);
                RefContract = dtRow["RefContract"].ToString();
                ExcludeInBilling = Convert.ToBoolean(Convert.IsDBNull(dtRow["ExcludeInBilling"]) ? false : dtRow["ExcludeInBilling"]);
                Remarks = dtRow["Remarks"].ToString();
                DateSynced = dtRow["DateSynced"].ToString();
                CompleteUnload = dtRow["CompleteUnload"].ToString();
                StorageCode = dtRow["StorageCode"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                InventoryDays = Convert.ToInt32(Convert.IsDBNull(dtRow["InventoryDays"]) ? 0 : dtRow["InventoryDays"]);
                NoChargeDays = Convert.ToInt32(Convert.IsDBNull(dtRow["NoChargeDays"]) ? 0 : dtRow["NoChargeDays"]);
                AllocationDate = dtRow["AllocationDate"].ToString();
                MinStorage = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinStorage"]) ? 0 : dtRow["MinStorage"]);
                Staging = Convert.ToInt32(Convert.IsDBNull(dtRow["Staging"]) ? 0 : dtRow["Staging"]);
                AllocChargeable = Convert.ToInt32(Convert.IsDBNull(dtRow["AllocChargeable"]) ? 0 : dtRow["AllocChargeable"]);
                IsOverridden = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsOverridden"]) ? false : dtRow["IsOverridden"]);
                OverrideQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OverrideQty"]) ? 0 : dtRow["OverrideQty"]);

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
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
            }
            return a;
        }
        public void InsertData(TransactionStorage _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //DT1.Rows.Add("WMS.TransactionStorage", "0", "RecordId", _ent.RecordId);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Type", _ent.Type);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Qty", _ent.Qty);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "ChargeableQty", _ent.ChargeableQty);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "UnitOfMeasure", _ent.UnitOfMeasure);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "PalletCount", _ent.PalletCount);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "BillingType", _ent.BillingType);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "StorageRate", _ent.StorageRate);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "HandlingInRate", _ent.HandlingInRate);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "HandlingOutRate", _ent.HandlingOutRate);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "MinimumQty", _ent.MinimumQty);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "MinHandlingIn", _ent.MinHandlingIn);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "MinHandlingOut", _ent.MinHandlingOut);
            if (Convert.ToString(_ent.RRDate) != "1/1/0001 12:00:00 AM" && _ent.RRDate != null && _ent.RRDate != "")
            {
                DT1.Rows.Add("WMS.TransactionStorage", "0", "RRDate", _ent.RRDate);
            }
            DT1.Rows.Add("WMS.TransactionStorage", "0", "ProdNum", _ent.ProdNum);
            if (Convert.ToString(_ent.ProdDate) != "1/1/0001 12:00:00 AM" && _ent.ProdDate != null && _ent.ProdDate != "")
            {
                DT1.Rows.Add("WMS.TransactionStorage", "0", "ProdDate", _ent.ProdDate);
            }
            DT1.Rows.Add("WMS.TransactionStorage", "0", "BillNumber", _ent.BillNumber);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "SplitBillRecordID", _ent.SplitBillRecordID);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "NoStorageCharge", _ent.NoStorageCharge);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "IsNoCharge", _ent.IsNoCharge);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "RefContract", _ent.RefContract);
            //DT1.Rows.Add("WMS.TransactionStorage", "0", "ExcludeInBilling", _ent.ExcludeInBilling);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "StorageCode", _ent.StorageCode);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "InventoryDays", _ent.InventoryDays);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "NoChargeDays", _ent.NoChargeDays);
            if (Convert.ToString(_ent.AllocationDate) != "1/1/0001 12:00:00 AM" && _ent.AllocationDate != null && _ent.AllocationDate != "")
            {
                DT1.Rows.Add("WMS.TransactionStorage", "0", "AllocationDate", _ent.AllocationDate);
            }
            DT1.Rows.Add("WMS.TransactionStorage", "0", "MinStorage", _ent.MinStorage);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Staging", _ent.Staging);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "AllocChargeable", _ent.AllocChargeable);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "IsOverridden", _ent.IsOverridden);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "OverrideQty", _ent.OverrideQty);

            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.TransactionStorage", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);

            //Functions.AuditTrail("WMSTRN", _ent.RecordId, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // KMM add Conn
        }

        public void UpdateData(TransactionStorage _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.TransactionStorage", "cond", "RecordId", _ent.RecordId);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Type", _ent.Type);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Qty", _ent.Qty);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "ChargeableQty", _ent.ChargeableQty);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "UnitOfMeasure", _ent.UnitOfMeasure);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "PalletCount", _ent.PalletCount);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "BillingType", _ent.BillingType);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "StorageRate", _ent.StorageRate);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "HandlingInRate", _ent.HandlingInRate);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "HandlingOutRate", _ent.HandlingOutRate);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "MinimumQty", _ent.MinimumQty);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "MinHandlingIn", _ent.MinHandlingIn);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "MinHandlingOut", _ent.MinHandlingOut);
            if (Convert.ToString(_ent.RRDate) != "1/1/0001 12:00:00 AM" && _ent.RRDate != null && _ent.RRDate != "")
            {
                DT1.Rows.Add("WMS.TransactionStorage", "set", "RRDate", _ent.RRDate);
            }
            DT1.Rows.Add("WMS.TransactionStorage", "set", "ProdNum", _ent.ProdNum);
            if (Convert.ToString(_ent.ProdDate) != "1/1/0001 12:00:00 AM" && _ent.ProdDate != null && _ent.ProdDate != "")
            {
                DT1.Rows.Add("WMS.TransactionStorage", "set", "ProdDate", _ent.ProdDate);
            }
            DT1.Rows.Add("WMS.TransactionStorage", "set", "BillNumber", _ent.BillNumber);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "SplitBillRecordID", _ent.SplitBillRecordID);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "NoStorageCharge", _ent.NoStorageCharge);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "IsNoCharge", _ent.IsNoCharge);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "RefContract", _ent.RefContract);
            //DT1.Rows.Add("WMS.TransactionStorage", "set", "ExcludeInBilling", _ent.ExcludeInBilling);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "StorageCode", _ent.StorageCode);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "InventoryDays", _ent.InventoryDays);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "NoChargeDays", _ent.NoChargeDays);
            if (Convert.ToString(_ent.AllocationDate) != "1/1/0001 12:00:00 AM" && _ent.AllocationDate != null && _ent.AllocationDate != "")
            {
                DT1.Rows.Add("WMS.TransactionStorage", "set", "AllocationDate", _ent.AllocationDate);
            }
            DT1.Rows.Add("WMS.TransactionStorage", "set", "MinStorage", _ent.MinStorage);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Staging", _ent.Staging);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "AllocChargeable", _ent.AllocChargeable);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "IsOverridden", _ent.IsOverridden);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "OverrideQty", _ent.OverrideQty);

            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("WMS.TransactionStorage", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSTRN", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(TransactionStorage _ent)
        {
            Conn = _ent.Connection;
            RecordId = _ent.RecordId;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.TransactionStorage", "cond", "RecordId", _ent.RecordId);

            Gears.DeleteData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSTRN", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void OverrideData(TransactionStorage _ent)
        {
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.TransactionStorage", "cond", "RecordId", _ent.RecordId);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "IsOverridden", _ent.IsOverridden);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "OverrideQty", _ent.OverrideQty);

            DT1.Rows.Add("WMS.TransactionStorage", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.TransactionStorage", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSTRN", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "OVERRIDE", _ent.Connection);
        }
    }
}
