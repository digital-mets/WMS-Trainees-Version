using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;
using Entity;

namespace Entity
{
    public class InternalTransfer
    {
        private static string Conn;
        private static string Docnum;
        private static string Date;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string TransType { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

        public DataTable getdata(string Code, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select * from WMS.InternalTransfer where DocNumber = '" + Code + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                TransType = dtRow["TransType"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
            }

            return a;
        }
        public void InsertData(InternalTransfer _ent)
        {
            Docnum = _ent.DocNumber;//<--- You can change this part
            Conn = _ent.Connection;
            Date = _ent.DocDate;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.InternalTransfer", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.InternalTransfer", "0", "TransType", _ent.TransType);
            DT1.Rows.Add("WMS.InternalTransfer", "0", "DocDate", _ent.DocDate);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("WMSIT", _ent.DocNumber, AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }
        public void UpdateData(InternalTransfer _ent)
        {
            Docnum = _ent.DocNumber;//<--- You can change this part
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.InternalTransfer", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.InternalTransfer", "set", "TransType", "WMSIT");
            DT1.Rows.Add("WMS.InternalTransfer", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.InternalTransfer", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.InternalTransfer", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.InternalTransfer", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.InternalTransfer", "set", "LastEditedDate", DateTime.Now.ToShortDateString());

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public class InternalTransferDetail
        {
            public virtual string DocNumber { get; set; }
            public virtual string TransType { get; set; }
            public virtual int RecordId { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual DateTime ExpiryDate { get; set; }
            public virtual DateTime ManufacturingDate { get; set; }
            public virtual string PalletID { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string ClientName { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }


            public DataTable getdata(string Code, string Conn)
            {
                DataTable a;

                a = Gears.RetriveData2("select * from WMS.InternalTransferDetail where DocNumber = '" + Code + "' order by LineNumber desc", Conn);  
                return a;
            }
            public void InsertDataDetail(InternalTransferDetail _ent)
            {
                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.InternalTransferDetail where docnumber = '" + Docnum + "'"
                    , Conn);

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

                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "TransType", "WMSIT");
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "RecordId", _ent.RecordId);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "ItemCode", _ent.ItemCode);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "BatchNumber", _ent.BatchNumber);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "ExpiryDate", _ent.ExpiryDate);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "ManufacturingDate", _ent.ManufacturingDate);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "PalletID", _ent.PalletID);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Qty", _ent.Qty);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "BulkQty", _ent.BulkQty);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field1", _ent.Field1);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field2", _ent.Field2);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field3", _ent.Field3);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field4", _ent.Field4);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field5", _ent.Field5);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field6", _ent.Field6);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field7", _ent.Field7);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field8", _ent.Field8);
                DT1.Rows.Add("WMS.InternalTransferDetail", "0", "Field9", _ent.Field9);
                Gears.CreateData(DT1, Conn);

                DataTable getItemLoc = Gears.RetriveData2("Select * from wms.ItemLocation where RecordId = '" + _ent.RecordId + "'", Conn);
                foreach (DataRow dtrow in getItemLoc.Rows)
                {
                    DT1.Rows.Clear();
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", "WMSIT");
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", Docnum);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", strLine);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", "00001");
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", _ent.ItemCode);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", dtrow["RemainingBaseQty"].ToString());
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Location", dtrow["Location"].ToString());
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "PalletID", _ent.PalletID);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "ExpirationDate", _ent.ExpiryDate);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "MfgDate", _ent.ManufacturingDate);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "RRdate", dtrow["RRdate"].ToString());
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", _ent.Qty);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", _ent.BulkQty);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field1", _ent.Field1);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field2", _ent.Field2);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field3", _ent.Field3);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field4", _ent.Field4);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field5", _ent.Field5);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field6", _ent.Field6);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field7", _ent.Field7);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field8", _ent.Field8);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "Field9", _ent.Field9);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "DocDate", Date);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "DocBaseQty", dtrow["OriginalBaseQty"].ToString());
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "DocBulkQty", dtrow["OriginalBulkQty"].ToString());
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "UnitCost", string.IsNullOrEmpty(dtrow["UnitCost"].ToString()) ? "0" : dtrow["UnitCost"].ToString());
                    DT1.Rows.Add("WMS.CountSheetSubsi", "0", "SubmiteddDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Gears.CreateData(DT1, Conn);

                    //Change detail of transaction
                    DataTable getTrans = Gears.RetriveData2(string.Format("Select distinct TransType from wms.countsheetsetup where ItemCode = '{0}' and PalletID = '{1}'"+
                    " and BatchNumber = '{2}' and ExpirationDate = '{3}' and MfgDate = '{4}'", dtrow["ItemCode"], dtrow["PalletID"], dtrow["BatchNumber"], dtrow["ExpirationDate"], dtrow["MfgDate"]), Conn);
                    foreach (DataRow dtrow2 in getTrans.Rows)
                    {
                        string TableName = "";
                        string BatchCol = "";
                        string MfgCol = "";
                        if (dtrow2[0].ToString() == "WMSIA")
                        {
                            TableName = "WMS.ItemAdjustmentDetail";
                            BatchCol = "BatchNo";
                            MfgCol = "MkfgDate";
                        }
                        else if (dtrow2[0].ToString() == "WMSINB")
                        {
                            TableName = "WMS.InboundDetail";
                            BatchCol = "BatchNumber";
                            MfgCol = "ManufacturingDate";
                        }
                        else if (dtrow2[0].ToString() == "WMSPICK")
                        {
                            TableName = "WMS.PicklistDetail";
                            BatchCol = "BatchNo";
                            MfgCol = "MkfgDate";
                        }
                        else if (dtrow2[0].ToString() == "WMSREL")
                        {
                            TableName = "WMS.ItemRelocationDetail";
                            BatchCol = "BatchNumber";
                        }

                        DT1.Rows.Clear();
                        DT1.Rows.Add(TableName, "cond", "ItemCode", dtrow["ItemCode"].ToString());
                        DT1.Rows.Add(TableName, "cond", BatchCol, dtrow["BatchNumber"].ToString());
                        DT1.Rows.Add(TableName, "cond", "PalletID", dtrow["PalletID"].ToString());
                        if (dtrow2[0].ToString() != "WMSREL")
                        {
                            DT1.Rows.Add(TableName, "cond", "ExpiryDate", dtrow["ExpirationDate"].ToString());
                            DT1.Rows.Add(TableName, "cond", MfgCol, dtrow["MfgDate"].ToString());
                            DT1.Rows.Add(TableName, "set", "ExpiryDate", _ent.ExpiryDate);
                            DT1.Rows.Add(TableName, "set", MfgCol, _ent.ManufacturingDate);
                        }
                        DT1.Rows.Add(TableName, "set", "ItemCode", _ent.ItemCode);
                        DT1.Rows.Add(TableName, "set", BatchCol, _ent.BatchNumber);
                        DT1.Rows.Add(TableName, "cond", "PalletID", dtrow["PalletID"].ToString());
                        Gears.UpdateData(DT1, Conn);
                    }

                    //Change Setup of transaction
                    //DT1.Rows.Clear();
                    //DT1.Rows.Add("WMS.CountSheetSetup", "cond", "ItemCode", dtrow["ItemCode"].ToString());
                    //DT1.Rows.Add("WMS.CountSheetSetup", "cond", "BatchNumber", dtrow["BatchNumber"].ToString());
                    //DT1.Rows.Add("WMS.CountSheetSetup", "cond", "ExpirationDate", dtrow["ExpirationDate"].ToString());
                    //DT1.Rows.Add("WMS.CountSheetSetup", "cond", "MfgDate", dtrow["MfgDate"].ToString());
                    //DT1.Rows.Add("WMS.CountSheetSetup", "cond", "PalletID", dtrow["PalletID"].ToString());
                    //DT1.Rows.Add("WMS.CountSheetSetup", "set", "ItemCode", _ent.ItemCode);
                    //DT1.Rows.Add("WMS.CountSheetSetup", "set", "BatchNumber", _ent.BatchNumber);
                    //DT1.Rows.Add("WMS.CountSheetSetup", "set", "ExpirationDate", _ent.ExpiryDate);
                    //DT1.Rows.Add("WMS.CountSheetSetup", "set", "MfgDate", _ent.ManufacturingDate);           
                    //Gears.UpdateData(DT1, Conn);

                    
                }

                DT1.Rows.Clear();
                DT1.Rows.Add("WMS.ItemLocation", "cond", "RecordId", _ent.RecordId);
                DT1.Rows.Add("WMS.ItemLocation", "set", "ItemCode", _ent.ItemCode);
                DT1.Rows.Add("WMS.ItemLocation", "set", "BatchNumber", _ent.BatchNumber);
                DT1.Rows.Add("WMS.ItemLocation", "set", "ExpirationDate", _ent.ExpiryDate);
                DT1.Rows.Add("WMS.ItemLocation", "set", "MfgDate", _ent.ManufacturingDate);
                DT1.Rows.Add("WMS.ItemLocation", "set", "PalletID", _ent.PalletID);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field1", _ent.Field1);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field2", _ent.Field2);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field3", _ent.Field3);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field4", _ent.Field4);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field5", _ent.Field5);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field6", _ent.Field6);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field7", _ent.Field7);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field8", _ent.Field8);
                DT1.Rows.Add("WMS.ItemLocation", "set", "Field9", _ent.Field9);               
                Gears.UpdateData(DT1, Conn);

            }
        }
        
    }
}
