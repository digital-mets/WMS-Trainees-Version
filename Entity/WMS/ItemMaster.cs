using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ItemMaster
    {
        private static string Item;
        public virtual string ItemCode { get; set; }
        public virtual string FullDesc { get; set; }
        public virtual string ItemcategoryCode { get; set; }
        public virtual string ProductCategoryCode { get; set; }
        public virtual string ProductSubCatCode { get; set; }
        public virtual string Customer { get; set; }
        public virtual string KeySupplier { get; set; }
        public virtual string ReorderLevel { get; set; }

        public virtual decimal MaxLevel { get; set; }

       public virtual decimal MinQTY { get; set; }

        public virtual string UnitBase { get; set; }
        public virtual string UnitBulk { get; set; }

       public virtual bool IsCore { get; set; }
       public virtual bool IsInactive { get; set; }
       // public virtual string Status { get; set; }
       // public virtual string Barcode { get; set; }
    
        public virtual string StorageType { get; set; }
        public virtual string PickingStrategy { get; set; }

        public virtual decimal StandardQty { get; set; }

        //public virtual string minTemp { get; set; }
       // public virtual string maxtemp { get; set; }
       // public virtual bool UseCountSheet { get; set; }
    
    
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string Field10 { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

   


        public DataTable getdata(string ItemCode)
        {
            DataTable a;

            // if (DocNumber != null)
            // {
            a = Gears.RetriveData2("select * from Masterfile.Item where ItemCode = '" + ItemCode + "'");
            foreach (DataRow dtRow in a.Rows)
            {
                ItemCode = dtRow["ItemCode"].ToString();
                FullDesc = dtRow["FullDesc"].ToString();
                ItemcategoryCode = dtRow["ItemCategoryCode"].ToString();
                ProductCategoryCode = dtRow["ProductCategoryCode"].ToString();
                ProductSubCatCode = dtRow["ProductSubCatCode"].ToString();
                Customer = dtRow["Customer"].ToString();
                KeySupplier = dtRow["KeySupplier"].ToString();
                ReorderLevel = dtRow["ReorderLevel"].ToString();
                MaxLevel = String.IsNullOrEmpty(dtRow["MaxLevel"].ToString()) ? 0 : Convert.ToDecimal(dtRow["MaxLevel"].ToString());
                MinQTY = String.IsNullOrEmpty(dtRow["MinQTY"].ToString()) ? 0 : Convert.ToDecimal(dtRow["MinQTY"].ToString());
                StandardQty = String.IsNullOrEmpty(dtRow["StandardQty"].ToString()) ? 0 : Convert.ToDecimal(dtRow["StandardQty"].ToString());
                UnitBase = dtRow["UnitBase"].ToString();
                UnitBulk = dtRow["UnitBulk"].ToString();
                IsCore = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsCore"]) ? false : dtRow["IsCore"]);
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                //Status = dtRow["Status"].ToString();
              //  Barcode = dtRow["Barcode"].ToString();
                StorageType = dtRow["StorageType"].ToString();
                //minTemp = dtRow["Mintemp"].ToString();
               // maxtemp = dtRow["Maxtemp"].ToString();
                PickingStrategy = dtRow["PickingStrategy"].ToString();
              //  UseCountSheet = Convert.ToBoolean(Convert.IsDBNull(dtRow["UseCountSheet"]) ? false : dtRow["UseCountSheet"]);
                Field1 = dtRow["Field01"].ToString();
                Field2 = dtRow["Field02"].ToString();
                Field3 = dtRow["Field03"].ToString();
                Field4 = dtRow["Field04"].ToString();
                Field5 = dtRow["Field05"].ToString();
                Field6 = dtRow["Field06"].ToString();
                Field7 = dtRow["Field07"].ToString();
                Field8 = dtRow["Field08"].ToString();
                Field9 = dtRow["Field09"].ToString();
                Field10 = dtRow["Field10"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
            }
            //  }
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as OutgoingDocNumber,'' as OutgoingDocType,'' as WarehouseCode,'' as StorerKey" +
            //        ",'' as TargetDate,''  as TargetDate,'' as  DeliveryDate" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(ItemMaster _ent)
        {
            Item = _ent.ItemCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Item", "0", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("Masterfile.Item", "0", "FullDesc", _ent.FullDesc);
          
            DT1.Rows.Add("Masterfile.Item", "0", "ItemCategoryCode", _ent.ItemcategoryCode);
            DT1.Rows.Add("Masterfile.Item", "0", "UnitBase", _ent.UnitBase);
            DT1.Rows.Add("Masterfile.Item", "0", "UnitBulk", _ent.UnitBulk);
       
            DT1.Rows.Add("Masterfile.Item", "0", "ProductCategoryCode", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "0", "ProductSubCatCode", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "0", "ReorderLevel", _ent.ReorderLevel);
            DT1.Rows.Add("Masterfile.Item", "0", "StandardQty", _ent.StandardQty);
           
            DT1.Rows.Add("Masterfile.Item", "0", "MaxLevel", _ent.MaxLevel);
            DT1.Rows.Add("Masterfile.Item", "0", "MinQTY", _ent.MinQTY);
            
            DT1.Rows.Add("Masterfile.Item", "0", "KeySupplier", _ent.KeySupplier);
            DT1.Rows.Add("Masterfile.Item", "0", "Customer", _ent.Customer);
            DT1.Rows.Add("Masterfile.Item", "0", "IsInactive", Convert.IsDBNull(_ent.IsInactive) ? false : _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Item", "0", "IsCore", Convert.IsDBNull(_ent.IsCore) ? false : _ent.IsCore);   
            DT1.Rows.Add("Masterfile.Item", "0", "AddedBy", _ent.AddedBy);

            DT1.Rows.Add("Masterfile.Item", "0", "StorageType", _ent.StorageType);
      
            DT1.Rows.Add("Masterfile.Item", "0", "AddedAccessories", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "0", "Composition", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "0", "PickingStrategy", _ent.PickingStrategy);
           

            DT1.Rows.Add("Masterfile.Item", "0", "Field01", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "0", "Field02", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "0", "Field03", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "0", "Field04", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "0", "Field05", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "0", "Field06", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "0", "Field07", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "0", "Field08", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "0", "Field09", _ent.Field9);
            DT1.Rows.Add("Masterfile.Item", "0", "Field10", _ent.Field10);


            Gears.CreateData(DT1);
        }

        public void UpdateData(ItemMaster _ent)
        {
            Item = _ent.ItemCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("Masterfile.Item", "set", "FullDesc", _ent.FullDesc);
           
            DT1.Rows.Add("Masterfile.Item", "set", "ItemCategoryCode", _ent.ItemcategoryCode);
            DT1.Rows.Add("Masterfile.Item", "set", "UnitBase", _ent.UnitBase);
            DT1.Rows.Add("Masterfile.Item", "set", "UnitBulk", _ent.UnitBulk);
            DT1.Rows.Add("Masterfile.Item", "set", "ProductCategoryCode", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "set", "ProductSubCatCode", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "set", "ReorderLevel", _ent.ReorderLevel);
            DT1.Rows.Add("Masterfile.Item", "set", "MaxLevel", _ent.MaxLevel);
            DT1.Rows.Add("Masterfile.Item", "set", "MinQTY", _ent.MinQTY);
            DT1.Rows.Add("Masterfile.Item", "set", "IsInactive", Convert.IsDBNull(_ent.IsInactive) ? false : _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Item", "set", "IsCore", Convert.IsDBNull(_ent.IsCore) ? false : _ent.IsCore);
            DT1.Rows.Add("Masterfile.Item", "set", "StandardQty", _ent.StandardQty);
            DT1.Rows.Add("Masterfile.Item", "set", "KeySupplier", _ent.KeySupplier);
            DT1.Rows.Add("Masterfile.Item", "set", "Customer", _ent.Customer);
            DT1.Rows.Add("Masterfile.Item", "set", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Item", "set", "StorageType", _ent.StorageType);
            DT1.Rows.Add("Masterfile.Item", "set", "AddedAccessories", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "set", "Composition", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "set", "PickingStrategy", _ent.PickingStrategy);
            DT1.Rows.Add("Masterfile.Item", "set", "Field01", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "set", "Field02", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "set", "Field03", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "set", "Field04", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "set", "Field05", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "set", "Field06", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "set", "Field07", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "set", "Field08", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "set", "Field09", _ent.Field9);
            DT1.Rows.Add("Masterfile.Item", "set", "Field10", _ent.Field10);
        


            string strErr = Gears.UpdateData(DT1);
            Functions.AuditTrail("REFITEM", Item, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");

        }
        public void DeleteData(ItemMaster _ent)
        {
            Item = _ent.ItemCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.ItemCode);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("REFITEM", Item, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }


        public class ItemDetail
        {
            public virtual ItemMaster Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Barcode { get; set; }
            public virtual decimal OnHand { get; set; }
            public virtual decimal OnOrder { get; set; }
            public virtual decimal OnAlloc { get; set; }
            public virtual decimal OnBulkQty { get; set; }
            public virtual decimal InTransit { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal BaseUnit { get; set; }
            public virtual bool IsInactive { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from MasterFile.ItemDetail where ItemCode='" + ItemCode + "' order by LineNumber");
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddItemDetail(ItemDetail ITMDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from MasterFile.ItemDetail where ItemCode = '" + Item + "'");

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
                DT1.Rows.Add("MasterFile.Item", "0", "ItemCode", Item);
                DT1.Rows.Add("MasterFile.Item", "0", "LineNumber", strLine);
                DT1.Rows.Add("MasterFile.Item", "0", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.Item", "0", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.Item", "0", "SizeCode", ITMDetail.SizeCode);
                DT1.Rows.Add("MasterFile.Item", "0", "BarCode", ITMDetail.Barcode);
                DT1.Rows.Add("MasterFile.Item", "0", "Onhand", 0);
                DT1.Rows.Add("MasterFile.Item", "0", "OnOrder", 0);
                DT1.Rows.Add("MasterFile.Item", "0", "OnAlloc", 0);
                DT1.Rows.Add("MasterFile.Item", "0", "OnBulkQty", 0);
                DT1.Rows.Add("MasterFile.Item", "0", "InTransit ", 0);
                DT1.Rows.Add("MasterFile.Item", "0", "StatusCode", ITMDetail.StatusCode);
                DT1.Rows.Add("MasterFile.Item", "0", "BaseUnit", ITMDetail.BaseUnit);
                DT1.Rows.Add("MasterFile.Item", "0", "Field1", ITMDetail.Field1);
                DT1.Rows.Add("MasterFile.Item", "0", "Field2", ITMDetail.Field2);
                DT1.Rows.Add("MasterFile.Item", "0", "Field3", ITMDetail.Field3);
                DT1.Rows.Add("MasterFile.Item", "0", "Field4", ITMDetail.Field4);
                DT1.Rows.Add("MasterFile.Item", "0", "Field5", ITMDetail.Field5);
                DT1.Rows.Add("MasterFile.Item", "0", "Field6", ITMDetail.Field6);
                DT1.Rows.Add("MasterFile.Item", "0", "Field7", ITMDetail.Field7);
                DT1.Rows.Add("MasterFile.Item", "0", "Field8", ITMDetail.Field8);
                DT1.Rows.Add("MasterFile.Item", "0", "Field9", ITMDetail.Field9);

                DT2.Rows.Add("MasterFile.Item", "cond", "ItemCode", Item);
                DT2.Rows.Add("MasterFile.Item", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1);
                Gears.UpdateData(DT2);
            }
            public void UpdateICNDetail(ItemDetail ITMDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.Item", "cond", "ItemCode", ITMDetail.ItemCode);
                DT1.Rows.Add("MasterFile.Item", "cond", "LineNumber", ITMDetail.LineNumber);
                DT1.Rows.Add("MasterFile.Item", "cond", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.Item", "cond", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.Item", "cond", "SizeCode", ITMDetail.SizeCode);
                DT1.Rows.Add("MasterFile.Item", "set", "BarCode", ITMDetail.Barcode);
                DT1.Rows.Add("MasterFile.Item", "set", "StatusCode", ITMDetail.StatusCode);
                DT1.Rows.Add("MasterFile.Item", "set", "BaseUnit", ITMDetail.BaseUnit);
                DT1.Rows.Add("MasterFile.Item", "set", "Field1", ITMDetail.Field1);
                DT1.Rows.Add("MasterFile.Item", "set", "Field2", ITMDetail.Field2);
                DT1.Rows.Add("MasterFile.Item", "set", "Field3", ITMDetail.Field3);
                DT1.Rows.Add("MasterFile.Item", "set", "Field4", ITMDetail.Field4);
                DT1.Rows.Add("MasterFile.Item", "set", "Field5", ITMDetail.Field5);
                DT1.Rows.Add("MasterFile.Item", "set", "Field6", ITMDetail.Field6);
                DT1.Rows.Add("MasterFile.Item", "set", "Field7", ITMDetail.Field7);
                DT1.Rows.Add("MasterFile.Item", "set", "Field8", ITMDetail.Field8);
                DT1.Rows.Add("MasterFile.Item", "set", "Field9", ITMDetail.Field9);

                Gears.UpdateData(DT1);
            }
            public void DeleteICNDetail(ItemDetail ITMDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.Item", "cond", "ItemCode", ITMDetail.ItemCode);
                DT1.Rows.Add("MasterFile.Item", "cond", "LineNumber", ITMDetail.LineNumber);


                Gears.DeleteData(DT1);

                DataTable count = Gears.RetriveData2("select * from MasterFile.Item where ItemCode = '" + Item + "'");

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("MasterFile.Item", "cond", "ItemCode", Item);
                    DT2.Rows.Add("MasterFile.Item", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2);
                }

            }
        }
    }
}
