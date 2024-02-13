using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;


namespace Entity
{
    public class SKU
    {
        private static string SKUCo;
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN
        public virtual string SKUCode { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string ProductCategory { get; set; }
        public virtual string ItemCategoryCode { get; set; }
        public virtual string SKUType { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual string Unit { get; set; }
        public virtual int Pieces { get; set; }
        public virtual decimal BatchWeight { get; set; }
        public virtual decimal YieldPercentage { get; set; }
        public virtual decimal YieldedBatchWeight { get; set; }
        public virtual bool IsSmallSKU { get; set; }
        public virtual bool WithCheese { get; set; }
        public virtual string BackColor { get; set; }
        public virtual string PackagingType { get; set; }
        public virtual string SAPCode { get; set; }
        public virtual string SAPDescription { get; set; }


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
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }
        public virtual IList<ItemMasterClient> Detail3 { get; set; }

        
        public class ItemMasterClient
        {
            public virtual SKU Parent { get; set; }
            public virtual int RecordID { get; set; }
            public virtual string SKUCode { get; set; }
            public virtual string CookingStage { get; set; }
            public virtual decimal StdCookingTime { get; set; }
            public virtual decimal StoveTempStd { get; set; }
            public virtual decimal PercentHumidityStd { get; set; }

            public DataTable getdetail(string SKUCode, string Conn)
            {

                DataTable a;
                try
                {

                    a = Gears.RetriveData2("select distinct * from Masterfile.FGSKUDetailCooking where SKUCode = '" + SKUCo + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddItemDetail(ItemMasterClient ITMDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,RecordID)) as RecordID from Masterfile.FGSKUDetailCooking where SKUCode = '" + SKUCo + "'", Conn);

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
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "0", "SKUCode", SKUCo);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "0", "CookingStage", ITMDetail.CookingStage);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "0", "StdCookingTime", ITMDetail.StdCookingTime);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "0", "StoveTempStd", ITMDetail.StoveTempStd);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "0", "PercentHumidityStd", ITMDetail.PercentHumidityStd);

                //DT2.Rows.Add("MasterFile.Item", "cond", "ItemCode", Item);
                //DT2.Rows.Add("MasterFile.Item", "set", "IsWithDetail", "True");


                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2, Conn);
            }
            public void UpdateItemDetail(ItemMasterClient ITMDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "cond", "SKUCode", SKUCo);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "cond", "RecordID", ITMDetail.RecordID);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "set", "CookingStage", ITMDetail.CookingStage);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "set", "StdCookingTime", ITMDetail.StdCookingTime);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "set", "StoveTempStd", ITMDetail.StoveTempStd);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "set", "PercentHumidityStd", ITMDetail.PercentHumidityStd);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteItemDetail(ItemMasterClient ITMDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "cond", "SKUCode", SKUCo);
                DT1.Rows.Add("MasterFile.FGSKUDetailCooking", "cond", "RecordID", ITMDetail.RecordID);
             

                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from MasterFile.FGSKUDetailCooking where SKUCode = '" + SKUCode + "'", Conn);

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("MasterFile.Item", "cond", "SKUCode", SKUCode);
                //    DT2.Rows.Add("MasterFile.Item", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2, Conn);
                //}

            }
        }
        public DataTable getdata(string Colcode, string Conn)
        {
            DataTable a;

            if (Colcode != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.FGSKU where SKUCode = '" + Colcode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    SKUCode = dtRow["SKUCode"].ToString();
                    ProductName = dtRow["ProductName"].ToString();
                    ProductCategory = dtRow["ProductCategory"].ToString();
                    SKUType = dtRow["SKUType"].ToString();
                    Weight = Convert.ToDecimal(dtRow["Weight"]);
                    Unit = dtRow["Unit"].ToString();
                    Pieces = Convert.ToInt32(dtRow["Pieces"]);
                    Quantity = Convert.ToDecimal(dtRow["Quantity"]);
                    BatchWeight = Convert.ToDecimal(dtRow["BatchWeight"]);
                    YieldPercentage = Convert.ToDecimal(dtRow["YieldPercentage"]);
                    YieldedBatchWeight = Convert.ToDecimal(dtRow["YieldedBatchWeight"]);
                    IsSmallSKU = Convert.ToBoolean(dtRow["IsSmallSKU"]);
                    WithCheese = Convert.ToBoolean(dtRow["WithCheese"]);
                    BackColor = dtRow["BackColor"].ToString();
                    PackagingType = dtRow["PackagingType"].ToString();
                    SAPCode = dtRow["SAPCode"].ToString();
                    SAPDescription = dtRow["SAPDescription"].ToString();

                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();


                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(SKU _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            SKUCo = _ent.SKUCode;
            DT1.Rows.Add("Masterfile.FGSKU", "0", "SKUCode", _ent.SKUCode);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "ProductName", _ent.ProductName);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "SKUType", _ent.SKUType);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Weight", _ent.Weight);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Unit", _ent.Unit);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Pieces", _ent.Pieces);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Quantity", _ent.Quantity);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "BatchWeight", _ent.BatchWeight);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "YieldPercentage", _ent.YieldPercentage);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "YieldedBatchWeight", _ent.YieldedBatchWeight);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "IsSmallSKU", _ent.IsSmallSKU);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "WithCheese", _ent.WithCheese);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "BackColor", _ent.BackColor);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "PackagingType", _ent.PackagingType);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "SAPCode", _ent.SAPCode);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "SAPDescription", _ent.SAPDescription);


            DT1.Rows.Add("Masterfile.FGSKU", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.FGSKU", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFSKUM", _ent.SKUCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void InsertItemData(SKU _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            _ent.ItemCategoryCode = "003"; //Default to 'Sample Products'
            SKUCo = _ent.SKUCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            
            DT1.Rows.Add("Masterfile.Item", "0", "ItemCode", _ent.SKUCode);
            DT1.Rows.Add("Masterfile.Item", "0", "FullDesc", _ent.ProductName);
            DT1.Rows.Add("Masterfile.Item", "0", "ShortDesc", _ent.ProductName);
            DT1.Rows.Add("Masterfile.Item", "0", "ItemCategoryCode", _ent.ItemCategoryCode);
            //DT1.Rows.Add("Masterfile.Item", "0", "ProductCategoryCode", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.Item", "0", "ItemType", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.Item", "0", "UnitBase", _ent.Unit);
            DT1.Rows.Add("Masterfile.Item", "0", "Weight", _ent.Weight);

            DT1.Rows.Add("Masterfile.Item", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Item", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Item", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "0", "Field9", _ent.Field9);

            
            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFITM", _ent.SKUCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateItemData(SKU _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            _ent.ItemCategoryCode = "003"; //Default to 'Sample Products'
            SKUCo = _ent.SKUCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.SKUCode);
            DT1.Rows.Add("Masterfile.Item", "set", "FullDesc", _ent.ProductName);
            DT1.Rows.Add("Masterfile.Item", "set", "ShortDesc", _ent.ProductName);
            DT1.Rows.Add("Masterfile.Item", "set", "ItemCategoryCode", _ent.ItemCategoryCode);
            //DT1.Rows.Add("Masterfile.Item", "set", "ProductCategoryCode", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.Item", "set", "ItemType", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.Item", "set", "Weight", _ent.Weight);
            DT1.Rows.Add("Masterfile.Item", "set", "UnitBase", _ent.Unit);

            DT1.Rows.Add("Masterfile.Item", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Item", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Item", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "set", "Field9", _ent.Field9);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFITM", _ent.SKUCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void UpdateData(SKU _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            SKUCo = _ent.SKUCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.FGSKU", "cond", "SKUCode", _ent.SKUCode);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "ProductName", _ent.ProductName);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "SKUType", _ent.SKUType);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Weight", _ent.Weight);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Unit", _ent.Unit);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Pieces", _ent.Pieces);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Quantity", _ent.Quantity);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "BatchWeight", _ent.BatchWeight);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "YieldPercentage", _ent.YieldPercentage);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "YieldedBatchWeight", _ent.YieldedBatchWeight);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "IsSmallSKU", _ent.IsSmallSKU);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "WithCheese", _ent.WithCheese);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "BackColor", _ent.BackColor);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "PackagingType", _ent.PackagingType);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "SAPCode", _ent.SAPCode);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "SAPDescription", _ent.SAPDescription);

            DT1.Rows.Add("Masterfile.FGSKU", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.FGSKU", "set", "Field9", _ent.Field9);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFSKUM", _ent.SKUCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(SKU _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            SKUCode = _ent.SKUCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.FGSKU", "cond", "SKUCode", _ent.SKUCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFSKUM", _ent.SKUCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
