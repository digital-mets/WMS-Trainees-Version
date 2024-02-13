using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ItemMasterfile
    {

        private static string Item;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string FullDesc { get; set; }
        public virtual string ShortDesc { get; set; }
        public virtual string ItemcategoryCode { get; set; }
        public virtual string ProductCategoryCode { get; set; }
        public virtual string ProductSubCatCode { get; set; }
        public virtual string Customer { get; set; }
        public virtual string KeySupplier { get; set; }
        public virtual decimal ReorderLevel { get; set; }
        public virtual decimal MaxLevel { get; set; }
        public virtual decimal MinQTY { get; set; }
        public virtual decimal StandardQTY { get; set; }
        public virtual decimal EstimatedCost { get; set; }
        public virtual string UnitBase { get; set; }
        public virtual string UnitBulk { get; set; }
        //public virtual bool IsService { get; set; }
        public virtual string ItemType { get; set; }
        public virtual bool UseThisItem { get; set; }
        public virtual bool IsCore { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual bool IsSenior { get; set; }
        public virtual bool IsPWD { get; set; }
        public virtual bool IsBNPC { get; set; }
        // public virtual string Status { get; set; }
        // public virtual string Barcode { get; set; }
        public virtual string StorageType { get; set; }
        public virtual string PickingStrategy { get; set; }
        public virtual string ProductGroup { get; set; }
        public virtual string GenderCode { get; set; }
        public virtual string BrandCode { get; set; }
        //public virtual string minTemp { get; set; }
        // public virtual string maxtemp { get; set; }
        public virtual bool IsByBulk { get; set; }
        public virtual string TaxCode { get; set; }
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
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }
///===================================FABRIC======================================///
        public virtual string RetailFabricCode { get; set; }
        public virtual string FabricGroup { get; set; }
        public virtual string SupplierFabricCode { get; set; }
        public virtual string FabricDesignCategory { get; set; }
        public virtual string Dyeing { get; set; }
        public virtual string WeaveType { get; set; }
        public virtual string Finishing { get; set; }
        public virtual string CuttableWidth { get; set; }
        public virtual string GrossWidth { get; set; }
        public virtual string CuttableWeightBW { get; set; }
        public virtual string GrossWeightBW { get; set; }
        public virtual string FabricStretch { get; set; }
        public virtual string WarpConstruction { get; set; }
        public virtual string WeftConstruction { get; set; }
        public virtual string WarpDensity { get; set; }
        public virtual string WeftDensity { get; set; }
        public virtual string WarpShrinkage { get; set; }
        public virtual string WeftShrinkage { get; set; }
        public virtual string ForKnitsOnly { get; set; }
        public virtual string Yield { get; set; }

        public virtual IList<ItemMasterDetail> Detail { get; set; }
        public virtual IList<ItemCustomerPriceDetail> Detail2 { get; set; }
        public class ItemMasterDetail
        {
            public virtual ItemMasterfile Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string prevColorCode { get; set; }
            public virtual string prevClassCode { get; set; }
            public virtual string prevSizeCode { get; set; }
            public virtual string Barcode { get; set; }
            public virtual decimal OnHand { get; set; }
            public virtual decimal OnOrder { get; set; }
            public virtual decimal OnAlloc { get; set; }
            public virtual decimal OnBulkQty { get; set; }
            public virtual decimal InTransit { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BaseUnit { get; set; }
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
            public virtual string Field10{ get; set; }
            public virtual string AddedDetail { get; set; }
            public virtual string FabricCode { get; set; }
            public virtual decimal Percentage { get; set; }
            public virtual string Type { get; set; }
            public virtual string Description { get; set; }
            public virtual string RecordID { get; set; }
            public virtual string ItemImage { get; set; }
            public virtual string UploadPhoto { get; set; }

            public DataTable getdetail(string ItemCode, string Conn)
            {

                DataTable a;
                try
                {

                    a = Gears.RetriveData2(string.Format("select a.*,b.Description,a.ColorCode as prevColorCode,a.ClassCode as prevClassCode,a.SizeCode as prevSizeCode, '' AS UploadPhoto from MasterFile.ItemDetail a left join masterfile.color b on a.colorcode = b.colorcode where ItemCode='{0}' order by LineNumber", ItemCode), Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddItemDetail(ItemMasterDetail ITMDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from MasterFile.ItemDetail where ItemCode = '" + Item + "'", Conn);

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
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "ItemCode", Item);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "SizeCode", ITMDetail.SizeCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "BarCode", ITMDetail.Barcode);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Onhand", 0);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "OnOrder", 0);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "OnAlloc", 0);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "OnBulkQty", 0);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "InTransit ", 0);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "StatusCode", ITMDetail.StatusCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "BaseUnit", ITMDetail.BaseUnit);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field1", ITMDetail.Field1);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field2", ITMDetail.Field2);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field3", ITMDetail.Field3);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field4", ITMDetail.Field4);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field5", ITMDetail.Field5);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field6", ITMDetail.Field6);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field7", ITMDetail.Field7);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field8", ITMDetail.Field8);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "Field9", ITMDetail.Field9);
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "AddedDetail", "1");
                DT1.Rows.Add("MasterFile.ItemDetail", "0", "ItemImage", ITMDetail.ItemImage);

                DT2.Rows.Add("MasterFile.Item", "cond", "ItemCode", Item);
                DT2.Rows.Add("MasterFile.Item", "set", "IsWithDetail", "True");


                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateItemDetail(ItemMasterDetail ITMDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "ItemCode", ITMDetail.ItemCode);
                //DT1.Rows.Add("MasterFile.ItemDetail", "cond", "LineNumber", ITMDetail.LineNumber);
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "ColorCode", ITMDetail.prevColorCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "ClassCode", ITMDetail.prevClassCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "SizeCode", ITMDetail.prevSizeCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "SizeCode", ITMDetail.SizeCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "BarCode", ITMDetail.Barcode);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "StatusCode", ITMDetail.StatusCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "BaseUnit", ITMDetail.BaseUnit);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "IsInactive", ITMDetail.IsInactive);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field1", ITMDetail.Field1);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field2", ITMDetail.Field2);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field3", ITMDetail.Field3);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field4", ITMDetail.Field4);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field5", ITMDetail.Field5);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field6", ITMDetail.Field6);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field7", ITMDetail.Field7);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field8", ITMDetail.Field8);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "Field9", ITMDetail.Field9);
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "AddedDetail", "1");
                DT1.Rows.Add("MasterFile.ItemDetail", "set", "ItemImage", ITMDetail.ItemImage);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteItemDetail(ItemMasterDetail ITMDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "ItemCode", ITMDetail.ItemCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.ItemDetail", "cond", "SizeCode", ITMDetail.SizeCode);
                //DT1.Rows.Add("MasterFile.ItemDetail", "cond", "LineNumber", ITMDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from MasterFile.ItemDetail where ItemCode = '" + Item + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("MasterFile.Item", "cond", "ItemCode", Item);
                    DT2.Rows.Add("MasterFile.Item", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }


           


     
            public DataTable getfabric(string ItemCode, string Conn)
            {

                DataTable a;
                try
                {

                    a = Gears.RetriveData2("select FabricCode,Type,Percentage,Description from MasterFile.FabricCompositionDetail where FabricCode='" + ItemCode + "' order by RecordID", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddFabricComp(ItemMasterDetail ITMDetail)
            {
                string Type = "";
                DataTable getType = Gears.RetriveData2("Select Code from it.GenericLookup where LookUpKey = 'COMTP' and Description = '" + ITMDetail.Description+"'", Conn);
                foreach (DataRow dtRow in getType.Rows)
                {
                    Type = dtRow[0].ToString();
                }
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "0", "FabricCode", Item);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "0", "Description", ITMDetail.Description);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "0", "Type", Type);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "0", "Percentage", ITMDetail.Percentage);

                Gears.CreateData(DT1, Conn);
            }
            public void UpdateFabricComp(ItemMasterDetail ITMDetail)
            {
                string Type = "";
                DataTable getType = Gears.RetriveData2("Select Code from it.GenericLookup where LookUpKey = 'COMTP' and Description = '" + ITMDetail.Description + "'", Conn);
                foreach (DataRow dtRow in getType.Rows)
                {
                    Type = dtRow[0].ToString();
                }
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "cond", "FabricCode", ITMDetail.FabricCode);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "cond", "Description", ITMDetail.Description);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "set", "Description", ITMDetail.Description);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "set", "Type", Type);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "set", "Percentage", ITMDetail.Percentage);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteFabricComp(ItemMasterDetail ITMDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "cond", "FabricCode", ITMDetail.FabricCode);
                DT1.Rows.Add("MasterFile.FabricCompositionDetail", "cond", "Type", ITMDetail.Type);

                Gears.DeleteData(DT1, Conn);
            }
            public DataTable getItemWHDetail(string ItemCode, string Conn)
            {

                DataTable a;
                try
                {

                    a = Gears.RetriveData2("select * from Masterfile.ItemWHDetail where ItemCode='" + ItemCode + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public DataTable getItemSupplierDetail(string ItemCode, string Conn)
            {

                DataTable a;
                try
                {

                    a = Gears.RetriveData2("select * from Masterfile.ItemSupplierPrice where ItemCode='" + ItemCode + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public class ItemCustomerPriceDetail
        {
            public virtual ItemMasterfile Parent { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string PrevColorCode { get; set; }
            public virtual string PrevClassCode { get; set; }
            public virtual string PrevSizeCode { get; set; }
            public virtual string Customer { get; set; }
            public virtual decimal Price { get; set; }
            public virtual string SubstitutedItem { get; set; }
            public virtual string SubstitutedColor { get; set; }
            public virtual string SubstitutedClass { get; set; }
            public virtual string SubstitutedSize { get; set; }
            public DataTable getdetail(string ItemCode, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,ColorCode as PrevColorCode,ClassCode as PrevClassCode,SizeCode as PrevSizeCode from Masterfile.ItemCustomerPrice where ItemCode='" + ItemCode + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddItemPriceDetail(ItemCustomerPriceDetail ITMDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "ItemCode", Item);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "SizeCode", ITMDetail.SizeCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "Customer", ITMDetail.Customer);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "Price", ITMDetail.Price);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "SubstitutedItem", ITMDetail.SubstitutedItem);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "SubstitutedColor", ITMDetail.SubstitutedColor);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "SubstitutedClass", ITMDetail.SubstitutedClass);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "0", "SubstitutedSize ", ITMDetail.SubstitutedSize);

                Gears.CreateData(DT1, Conn);
            }
            public void UpdateItemPriceDetail(ItemCustomerPriceDetail ITMDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "ItemCode", Item);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "ColorCode", ITMDetail.PrevColorCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "ClassCode", ITMDetail.PrevClassCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "SizeCode", ITMDetail.PrevSizeCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "SizeCode", ITMDetail.SizeCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "Customer", ITMDetail.Customer);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "Price", ITMDetail.Price);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "SubstitutedItem", ITMDetail.SubstitutedItem);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "SubstitutedColor", ITMDetail.SubstitutedColor);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "SubstitutedClass", ITMDetail.SubstitutedClass);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "set", "SubstitutedSize ", ITMDetail.SubstitutedSize);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteItemPriceDetail(ItemCustomerPriceDetail ITMDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "ItemCode", ITMDetail.ItemCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "ColorCode", ITMDetail.ColorCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "ClassCode", ITMDetail.ClassCode);
                DT1.Rows.Add("MasterFile.ItemCustomerPrice", "cond", "SizeCode", ITMDetail.SizeCode);
                //DT1.Rows.Add("MasterFile.ItemDetail", "cond", "LineNumber", ITMDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);
            }
        }
        public DataTable getdata(string ItemCode, string Conn)
        {
            DataTable a;

            // if (DocNumber != null)
            // {
            a = Gears.RetriveData2("select * from Masterfile.Item where ItemCode = '" + ItemCode + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                ItemCode = dtRow["ItemCode"].ToString();
                FullDesc = dtRow["FullDesc"].ToString();
                ShortDesc = dtRow["ShortDesc"].ToString();
                ItemcategoryCode = dtRow["ItemCategoryCode"].ToString();
                ProductCategoryCode = dtRow["ProductCategoryCode"].ToString();
                ProductSubCatCode = dtRow["ProductSubCatCode"].ToString();
                Customer = dtRow["Customer"].ToString();
                KeySupplier = dtRow["KeySupplier"].ToString();
                ReorderLevel = String.IsNullOrEmpty(dtRow["ReorderLevel"].ToString()) ? 0 : Convert.ToDecimal(dtRow["ReorderLevel"].ToString());
                MaxLevel = String.IsNullOrEmpty(dtRow["MaxLevel"].ToString()) ? 0 : Convert.ToDecimal(dtRow["MaxLevel"].ToString());
                MinQTY = String.IsNullOrEmpty(dtRow["MinQTY"].ToString()) ? 0 : Convert.ToDecimal(dtRow["MinQTY"].ToString());
                StandardQTY = String.IsNullOrEmpty(dtRow["StandardQty"].ToString()) ? 0 : Convert.ToDecimal(dtRow["StandardQty"].ToString());
                EstimatedCost = String.IsNullOrEmpty(dtRow["EstimatedCost"].ToString()) ? 0 : Convert.ToDecimal(dtRow["EstimatedCost"].ToString());
                UnitBase = dtRow["UnitBase"].ToString();
                UnitBulk = dtRow["UnitBulk"].ToString();
                //IsService = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsService"]) ? false : dtRow["IsService"]);
                ItemType = dtRow["ItemType"].ToString();
                IsCore = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsCore"]) ? false : dtRow["IsCore"]);
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                IsByBulk = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsByBulk"]) ? false : dtRow["IsByBulk"]);
                TaxCode = dtRow["TaxCode"].ToString();
                IsSenior = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSenior"]) ? 0 : dtRow["IsSenior"]);
                IsPWD = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPWD"]) ? 0 : dtRow["IsPWD"]);
                IsBNPC = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsBNPC"]) ? 0 : dtRow["IsBNPC"]);
                StorageType = dtRow["StorageType"].ToString();
                PickingStrategy = dtRow["PickingStrategy"].ToString();
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
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                DeActivatedDate = dtRow["DeActivatedDate"].ToString();
               
            }
            a = Gears.RetriveData2("select * from masterfile.fabric where fabriccode = '"+ ItemCode +"'",Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                //FABRIC
                FabricGroup = dtRow["FabricGroup"].ToString();
                SupplierFabricCode = dtRow["SupplierFabricCode"].ToString();
                FabricDesignCategory = dtRow["FabricDesignCategory"].ToString();
                Dyeing = dtRow["Dyeing"].ToString();
                WeaveType = dtRow["WeaveType"].ToString();
                Finishing = dtRow["Finish"].ToString();
                CuttableWidth = dtRow["CuttableWidth"].ToString();
                GrossWidth = dtRow["GrossWidth"].ToString();
                CuttableWeightBW = dtRow["CuttableWeightBW"].ToString();
                GrossWeightBW = dtRow["GrossWeightBW"].ToString();
                FabricStretch = dtRow["FabricStretch"].ToString();
                WarpConstruction = dtRow["WarpConstruction"].ToString();
                WeftConstruction = dtRow["WeftConstruction"].ToString();
                WarpDensity = dtRow["WarpDensity"].ToString();
                WeftDensity = dtRow["WeftDensity"].ToString();
                WarpShrinkage = dtRow["WarpShrinkage"].ToString();
                WeftShrinkage = dtRow["WeftShrinkage"].ToString();
                ForKnitsOnly = dtRow["ForKnitsOnly"].ToString();
                Yield = dtRow["Yield"].ToString();
            }

            return a;
        }
        public void InsertData(ItemMasterfile _ent)
        {
            Item = _ent.ItemCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Item", "0", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("Masterfile.Item", "0", "FullDesc", _ent.FullDesc);
            DT1.Rows.Add("Masterfile.Item", "0", "ShortDesc", _ent.ShortDesc);
            DT1.Rows.Add("Masterfile.Item", "0", "ItemCategoryCode", _ent.ItemcategoryCode);
            DT1.Rows.Add("Masterfile.Item", "0", "UnitBase", _ent.UnitBase);
            DT1.Rows.Add("Masterfile.Item", "0", "UnitBulk", _ent.UnitBulk);
            DT1.Rows.Add("Masterfile.Item", "0", "ProductCategoryCode", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "0", "ProductSubCatCode", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "0", "ReorderLevel", _ent.ReorderLevel);
            DT1.Rows.Add("Masterfile.Item", "0", "MaxLevel", _ent.MaxLevel);
            DT1.Rows.Add("Masterfile.Item", "0", "MinQTY", _ent.MinQTY);
            DT1.Rows.Add("Masterfile.Item", "0", "EstimatedCost", _ent.EstimatedCost);
            DT1.Rows.Add("Masterfile.Item", "0", "StandardQty", _ent.StandardQTY);
            DT1.Rows.Add("Masterfile.Item", "0", "KeySupplier", _ent.KeySupplier);
            DT1.Rows.Add("Masterfile.Item", "0", "Customer", _ent.Customer);
            DT1.Rows.Add("Masterfile.Item", "0", "ItemType", _ent.ItemType);
            DT1.Rows.Add("Masterfile.Item", "0", "UseThisItem", Convert.IsDBNull(_ent.UseThisItem) ? false : _ent.UseThisItem);
            DT1.Rows.Add("Masterfile.Item", "0", "IsInactive", Convert.IsDBNull(_ent.IsInactive) ? false : _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Item", "0", "IsSenior", _ent.IsSenior);
            DT1.Rows.Add("Masterfile.Item", "0", "IsPWD", _ent.IsPWD);
            DT1.Rows.Add("Masterfile.Item", "0", "IsBNPC", _ent.IsBNPC);
            DT1.Rows.Add("Masterfile.Item", "0", "ProductGroup", _ent.ProductGroup);
            DT1.Rows.Add("Masterfile.Item", "0", "GenderCode", _ent.GenderCode);
            DT1.Rows.Add("Masterfile.Item", "0", "BrandCode", _ent.BrandCode);
            //DT1.Rows.Add("Masterfile.Item", "0", "IsService", Convert.IsDBNull(_ent.IsService) ? false : _ent.IsService);
           	if (_ent.ItemType == "C")
            {
                DT1.Rows.Add("Masterfile.Item", "0", "IsCore", true);
            }
            else
            {
                DT1.Rows.Add("Masterfile.Item", "0", "IsCore", Convert.IsDBNull(_ent.IsCore) ? false : _ent.IsCore);
            }
            DT1.Rows.Add("Masterfile.Item", "0", "IsByBulk", Convert.IsDBNull(_ent.IsByBulk) ? false : _ent.IsByBulk);
            DT1.Rows.Add("Masterfile.Item", "0", "StorageType", _ent.StorageType);
            DT1.Rows.Add("Masterfile.Item", "0", "TaxCode", _ent.TaxCode);
            DT1.Rows.Add("Masterfile.Item", "0", "AddedAccessories", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "0", "Composition", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "0", "PickingStrategy", _ent.PickingStrategy);
            DT1.Rows.Add("Masterfile.Item", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.Item", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Item", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
            //FABRIC
            if (_ent.ItemcategoryCode.Trim() == "2")
            {
                DT2.Rows.Add("Masterfile.Fabric", "0", "FabricCode", _ent.ItemCode);
                DT2.Rows.Add("Masterfile.Fabric", "0", "FabricGroup", _ent.FabricGroup);
                DT2.Rows.Add("Masterfile.Fabric", "0", "FabricSupplier", _ent.KeySupplier);
                DT2.Rows.Add("Masterfile.Fabric", "0", "SupplierFabricCode", _ent.SupplierFabricCode);
                DT2.Rows.Add("Masterfile.Fabric", "0", "FabricDesignCategory", _ent.FabricDesignCategory);
                DT2.Rows.Add("Masterfile.Fabric", "0", "Dyeing", _ent.Dyeing);
                DT2.Rows.Add("Masterfile.Fabric", "0", "Finish", _ent.Finishing);
                DT2.Rows.Add("Masterfile.Fabric", "0", "WeaveType", _ent.WeaveType);
                DT2.Rows.Add("Masterfile.Fabric", "0", "CuttableWidth", _ent.CuttableWidth);
                DT2.Rows.Add("Masterfile.Fabric", "0", "GrossWidth", _ent.GrossWidth);
                DT2.Rows.Add("Masterfile.Fabric", "0", "CuttableWeightBW", _ent.CuttableWeightBW);
                DT2.Rows.Add("Masterfile.Fabric", "0", "GrossWeightBW", _ent.GrossWeightBW);
                DT2.Rows.Add("Masterfile.Fabric", "0", "FabricStretch", _ent.FabricStretch);
                DT2.Rows.Add("Masterfile.Fabric", "0", "WarpConstruction", _ent.WarpConstruction);
                DT2.Rows.Add("Masterfile.Fabric", "0", "WeftConstruction", _ent.WeftConstruction);
                DT2.Rows.Add("Masterfile.Fabric", "0", "WarpDensity", _ent.WarpDensity);
                DT2.Rows.Add("Masterfile.Fabric", "0", "WeftDensity", _ent.WeftDensity);
                DT2.Rows.Add("Masterfile.Fabric", "0", "WarpShrinkage", _ent.WarpShrinkage);
                DT2.Rows.Add("Masterfile.Fabric", "0", "WeftShrinkage", _ent.WeftShrinkage);
                DT2.Rows.Add("Masterfile.Fabric", "0", "ForKnitsOnly", _ent.ForKnitsOnly);
                DT2.Rows.Add("Masterfile.Fabric", "0", "Yield", _ent.Yield);
                Gears.CreateData(DT2, _ent.Connection);
            }

        }
        public void UpdateData(ItemMasterfile _ent)
        {
            Item = _ent.ItemCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("Masterfile.Item", "set", "FullDesc", _ent.FullDesc);
            DT1.Rows.Add("Masterfile.Item", "set", "ShortDesc", _ent.ShortDesc);
            DT1.Rows.Add("Masterfile.Item", "set", "ItemCategoryCode", _ent.ItemcategoryCode);
            DT1.Rows.Add("Masterfile.Item", "set", "UnitBase", _ent.UnitBase);
            DT1.Rows.Add("Masterfile.Item", "set", "UnitBulk", _ent.UnitBulk);
            DT1.Rows.Add("Masterfile.Item", "set", "ProductCategoryCode", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "set", "ProductSubCatCode", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "set", "ReorderLevel", _ent.ReorderLevel);
            DT1.Rows.Add("Masterfile.Item", "set", "MaxLevel", _ent.MaxLevel);
            DT1.Rows.Add("Masterfile.Item", "set", "MinQTY", _ent.MinQTY);
            DT1.Rows.Add("Masterfile.Item", "set", "StandardQty", _ent.StandardQTY);
            DT1.Rows.Add("Masterfile.Item", "set", "EstimatedCost", _ent.EstimatedCost);
            DT1.Rows.Add("Masterfile.Item", "set", "ItemType", _ent.ItemType);
            DT1.Rows.Add("Masterfile.Item", "set", "IsInactive", Convert.IsDBNull(_ent.IsInactive) ? false : _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Item", "set", "IsSenior", _ent.IsSenior);
            DT1.Rows.Add("Masterfile.Item", "set", "IsPWD", _ent.IsPWD);
            DT1.Rows.Add("Masterfile.Item", "set", "IsBNPC", _ent.IsBNPC); 
            //DT1.Rows.Add("Masterfile.Item", "set", "IsService", Convert.IsDBNull(_ent.) ? false : _ent.IsService);

            //foreach (var fields in _ent.GetType().GetProperties())
            //{
            //    if (fields.Name != "Connection" && fields.PropertyType.Name != "Ilist`1")
            //    {
            //        if (fields.Name == "IsService" || fields.Name == "IsInactive" || fields.Name == "IsByBulk")
            //            DT1.Rows.Add("Masterfile.Item", "set", fields.Name, Convert.IsDBNull(GetPropertyValue(_ent, fields.Name)) ? false : GetPropertyValue(_ent, fields.Name));
            //        else if (fields.Name == "LastEditedDate")
            //            DT1.Rows.Add("Masterfile.Item", "set", fields.Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //        else if (fields.Name == "IsCore" && _ent.ItemType == "C")
            //            DT1.Rows.Add("Masterfile.Item", "set", fields.Name, true);
            //        else if (fields.Name == "IsCore" && _ent.ItemType != "C")
            //            DT1.Rows.Add("Masterfile.Item", "set", fields.Name, Convert.IsDBNull(_ent.IsCore) ? false : _ent.IsCore);
            //        else
            //            DT1.Rows.Add("Masterfile.Item", "set", fields.Name, GetPropertyValue(_ent, fields.Name));
            //    }
            //}

            if (_ent.ItemType == "C")
            {
                DT1.Rows.Add("Masterfile.Item", "set", "IsCore", true);
            }
            else
            {
                DT1.Rows.Add("Masterfile.Item", "set", "IsCore", Convert.IsDBNull(_ent.IsCore) ? false : _ent.IsCore);
            }
            DT1.Rows.Add("Masterfile.Item", "set", "IsByBulk", Convert.IsDBNull(_ent.IsByBulk) ? false : _ent.IsByBulk);
            DT1.Rows.Add("Masterfile.Item", "set", "KeySupplier", _ent.KeySupplier);
            DT1.Rows.Add("Masterfile.Item", "set", "Customer", _ent.Customer);
            DT1.Rows.Add("Masterfile.Item", "set", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Item", "set", "StorageType", _ent.StorageType);
            DT1.Rows.Add("Masterfile.Item", "set", "TaxCode", _ent.TaxCode);
            DT1.Rows.Add("Masterfile.Item", "set", "AddedAccessories", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.Item", "set", "Composition", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.Item", "set", "PickingStrategy", _ent.PickingStrategy);
            DT1.Rows.Add("Masterfile.Item", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Item", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Item", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Item", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Item", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Item", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Item", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Item", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Item", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.Item", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Item", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            //FABRIC
            DataTable checkFab = Gears.RetriveData2("Select FabricCode from masterfile.fabric where fabriccode = '" + _ent.ItemCode + "'",Connection);
            if (checkFab.Rows.Count > 0)
            {
                DT2.Rows.Add("Masterfile.Fabric", "cond", "FabricCode", _ent.ItemCode);
                DT2.Rows.Add("Masterfile.Fabric", "set", "FabricGroup", _ent.FabricGroup);
                DT2.Rows.Add("Masterfile.Fabric", "set", "SupplierFabricCode", _ent.SupplierFabricCode);
                DT2.Rows.Add("Masterfile.Fabric", "set", "FabricDesignCategory", _ent.FabricDesignCategory);
                DT2.Rows.Add("Masterfile.Fabric", "set", "FabricSupplier", _ent.KeySupplier);
                DT2.Rows.Add("Masterfile.Fabric", "set", "Dyeing", _ent.Dyeing);
                DT2.Rows.Add("Masterfile.Fabric", "set", "Finish", _ent.Finishing);
                DT2.Rows.Add("Masterfile.Fabric", "set", "WeaveType", _ent.WeaveType);
                DT2.Rows.Add("Masterfile.Fabric", "set", "CuttableWidth", _ent.CuttableWidth);
                DT2.Rows.Add("Masterfile.Fabric", "set", "GrossWidth", _ent.GrossWidth);
                DT2.Rows.Add("Masterfile.Fabric", "set", "CuttableWeightBW", _ent.CuttableWeightBW);
                DT2.Rows.Add("Masterfile.Fabric", "set", "GrossWeightBW", _ent.GrossWeightBW);
                DT2.Rows.Add("Masterfile.Fabric", "set", "FabricStretch", _ent.FabricStretch);
                DT2.Rows.Add("Masterfile.Fabric", "set", "WarpConstruction", _ent.WarpConstruction);
                DT2.Rows.Add("Masterfile.Fabric", "set", "WeftConstruction", _ent.WeftConstruction);
                DT2.Rows.Add("Masterfile.Fabric", "set", "WarpDensity", _ent.WarpDensity);
                DT2.Rows.Add("Masterfile.Fabric", "set", "WeftDensity", _ent.WeftDensity);
                DT2.Rows.Add("Masterfile.Fabric", "set", "WarpShrinkage", _ent.WarpShrinkage);
                DT2.Rows.Add("Masterfile.Fabric", "set", "WeftShrinkage", _ent.WeftShrinkage);
                DT2.Rows.Add("Masterfile.Fabric", "set", "ForKnitsOnly", _ent.ForKnitsOnly);
                DT2.Rows.Add("Masterfile.Fabric", "set", "Yield", _ent.Yield);
                Gears.UpdateData(DT2, _ent.Connection);
            }
            else
            {
                if (_ent.ItemcategoryCode.Trim() == "2")
                {
                    DT2.Rows.Add("Masterfile.Fabric", "0", "FabricCode", _ent.ItemCode);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "FabricGroup", _ent.FabricGroup);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "SupplierFabricCode", _ent.SupplierFabricCode);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "FabricSupplier", _ent.KeySupplier);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "FabricDesignCategory", _ent.FabricDesignCategory);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "Dyeing", _ent.Dyeing);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "Finish", _ent.Finishing);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "WeaveType", _ent.WeaveType);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "CuttableWidth", _ent.CuttableWidth);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "GrossWidth", _ent.GrossWidth);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "CuttableWeightBW", _ent.CuttableWeightBW);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "GrossWeightBW", _ent.GrossWeightBW);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "FabricStretch", _ent.FabricStretch);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "WarpConstruction", _ent.WarpConstruction);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "WeftConstruction", _ent.WeftConstruction);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "WarpDensity", _ent.WarpDensity);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "WeftDensity", _ent.WeftDensity);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "WarpShrinkage", _ent.WarpShrinkage);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "WeftShrinkage", _ent.WeftShrinkage);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "ForKnitsOnly", _ent.ForKnitsOnly);
                    DT2.Rows.Add("Masterfile.Fabric", "0", "Yield", _ent.Yield);
                    Gears.CreateData(DT2, _ent.Connection);
                }
            }

            

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ITMMAST", Item, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

        }
        public void DeleteData(ItemMasterfile _ent)
        {
            Item = _ent.ItemCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.ItemCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ITMMAST", Item, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public void UpdateBasedonReference()
        {

            Gears.RetriveData2("UPDATE A SET A.ColorCode=B.ColorCode,A.ClassCode=C.ClassCode,A.SizeCode=D.SizeCode FROM Masterfile.ItemDetail A " +
                            " INNER JOIN Masterfile.Color B" +
                            " ON A.ColorCode = B.ColorCode" +
                            " INNER JOIN Masterfile.Class C" +
                            " ON A.ClassCode = C.ClassCode" +
                            " INNER JOIN Masterfile.Size D" +
                            " ON A.SizeCode	= D.SizeCode" +
                            " WHERE ItemCode = '" + Item + "'", Conn);



        }
        public class ItemStock
        {
            ///===================================STOCK MASTER======================================///
            public virtual string StockNumber { get; set; }
            public virtual string ProductGroup { get; set; }
            public virtual string DeliveryYear { get; set; }
            public virtual string DeliveryMonth { get; set; }
            public virtual string CollectionAbbreviation { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string FitCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ColorName { get; set; }
            public virtual string ProductDesignCategory { get; set; }
            public virtual string RetailFabricCode { get; set; }
            public virtual string WashCode { get; set; }
            public virtual string TintCode { get; set; }
            public virtual string ImportedItem { get; set; }
            public virtual string ProductClass { get; set; }
            public virtual string ProductSubClass { get; set; }
            public virtual string ProductAlignment { get; set; }
            public virtual string Season { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string RecoAlloc { get; set; }
            public virtual decimal SRP { get; set; }
            public virtual int RecordID { get; set; }
            public virtual string ProductCategory { get; set; }
            public virtual string GenderCode { get; set; }
            public virtual string BrandCode { get; set; }
            public virtual decimal UpdatedPrice { get; set; }
            public virtual string Notes { get; set; }
            public virtual string DeliveryTheme { get; set; }


            public DataTable getdata(string ItemCode, string Conn)
            {
                DataTable a;

                // if (DocNumber != null)
                // {
                a = Gears.RetriveData2("select * from Masterfile.StockMaster where StockNumber = '" + ItemCode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    StockNumber = dtRow["StockNumber"].ToString();
                    ProductGroup = dtRow["ProductGroup"].ToString();
                    DeliveryYear = dtRow["DeliveryYear"].ToString();
                    DeliveryMonth = dtRow["DeliveryMonth"].ToString();
                    CollectionAbbreviation = dtRow["CollectionAbbreviation"].ToString();
                    PISNumber = dtRow["PISNumber"].ToString();
                    FitCode = dtRow["FitCode"].ToString();
                    ColorCode = dtRow["ColorCode"].ToString();
                    ColorName = dtRow["ColorName"].ToString();
                    ProductDesignCategory = dtRow["ProductDesignCategory"].ToString();
                    RetailFabricCode = dtRow["RetailFabricCode"].ToString();
                    WashCode = dtRow["WashCode"].ToString();
                    TintCode = dtRow["TintCode"].ToString();
                    ImportedItem = dtRow["ImportedItem"].ToString();
                    ProductClass = dtRow["ProductClass"].ToString();
                    ProductSubClass = dtRow["ProductSubClass"].ToString();
                    ProductAlignment = dtRow["ProductAlignment"].ToString();
                    Season = dtRow["Season"].ToString();
                    ClassCode = dtRow["ClassCode"].ToString();
                    RecoAlloc = dtRow["RecoAlloc"].ToString();
                    SRP = Convert.ToDecimal(Convert.IsDBNull(dtRow["SRP"]) ? 0 : dtRow["SRP"]);
                    //RecordID = Convert.ToInt16(Convert.IsDBNull(dtRow["RecordID"]) ? 0 : dtRow["RecordID"]);
                    ProductCategory = dtRow["ProductCategory"].ToString();
                    GenderCode = dtRow["GenderCode"].ToString();
                    BrandCode = dtRow["BrandCode"].ToString();
                    Notes = dtRow["Notes"].ToString();
                    DeliveryTheme = dtRow["DeliveryTheme"].ToString();
                }
                return a;
            }
            public void InsertData(ItemStock _ent)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.StockMaster", "0", "StockNumber", _ent.StockNumber);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ProductGroup", _ent.ProductGroup);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "DeliveryYear", _ent.DeliveryYear);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "DeliveryMonth", _ent.DeliveryMonth);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "CollectionAbbreviation", _ent.CollectionAbbreviation);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "PISNumber", _ent.PISNumber);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "FitCode", _ent.FitCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ColorCode", _ent.ColorCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ColorName", _ent.ColorName);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ProductDesignCategory", _ent.ProductDesignCategory);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "RetailFabricCode", _ent.RetailFabricCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "WashCode", _ent.WashCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "TintCode", _ent.TintCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ImportedItem", _ent.ImportedItem);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ProductClass", _ent.ProductClass);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ProductSubClass", _ent.ProductSubClass);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ProductAlignment", _ent.ProductAlignment);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "Season", _ent.Season);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ClassCode", _ent.ClassCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "RecoAlloc", _ent.RecoAlloc);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "SRP", _ent.SRP);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "ProductCategory", _ent.ProductCategory);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "GenderCode", _ent.GenderCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "BrandCode", _ent.BrandCode);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "Notes", _ent.Notes);
                DT1.Rows.Add("Masterfile.StockMaster", "0", "DeliveryTheme", _ent.DeliveryTheme);



                DataTable dtdata = Gears.RetriveData2("SELECT StockNumber FROM  Retail.StockMasterPriceHistory where StockNumber = '" + _ent.StockNumber + "'", Conn);
           
                if (dtdata.Rows.Count ==0)
                {
                    DT2.Rows.Add("Retail.StockMasterPriceHistory", "0", "StockNumber", _ent.StockNumber);
                    DT2.Rows.Add("Retail.StockMasterPriceHistory", "0", "StatusCode", "RG");
                    DT2.Rows.Add("Retail.StockMasterPriceHistory", "0", "Price", _ent.SRP);
                    Gears.CreateData(DT2, Conn);
                }
              

                Gears.CreateData(DT1, Conn);
              

                Gears.RetriveData2("Update masterfile.item set SRP = '" + _ent.SRP + "', updatedPrice = '" + _ent.SRP + "' where ItemCode = '" + _ent.StockNumber + "'", Conn);
            }
            public void UpdateData(ItemStock _ent)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.StockMaster", "cond", "StockNumber", _ent.StockNumber);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ProductGroup", _ent.ProductGroup);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "DeliveryYear", _ent.DeliveryYear);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "DeliveryMonth", _ent.DeliveryMonth);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "CollectionAbbreviation", _ent.CollectionAbbreviation);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "PISNumber", _ent.PISNumber);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "FitCode", _ent.FitCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ColorCode", _ent.ColorCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ColorName", _ent.ColorName);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ProductDesignCategory", _ent.ProductDesignCategory);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "RetailFabricCode", _ent.RetailFabricCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "WashCode", _ent.WashCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "TintCode", _ent.TintCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ImportedItem", _ent.ImportedItem);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ProductClass", _ent.ProductClass);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ProductSubClass", _ent.ProductSubClass);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ProductAlignment", _ent.ProductAlignment);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "Season", _ent.Season);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ClassCode", _ent.ClassCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "RecoAlloc", _ent.RecoAlloc);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "SRP", _ent.SRP);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "ProductCategory", _ent.ProductCategory);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "GenderCode", _ent.GenderCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "BrandCode", _ent.BrandCode);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "Notes", _ent.Notes);
                DT1.Rows.Add("Masterfile.StockMaster", "set", "DeliveryTheme", _ent.DeliveryTheme); 

                Gears.UpdateData(DT1, Conn);

                Gears.RetriveData2("Update masterfile.item set SRP = '" + _ent.SRP + "', updatedPrice = '" + _ent.SRP + "' where ItemCode = '" + _ent.StockNumber + "'", Conn);
                //Functions.AuditTrail("ITMMAST", Item, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

            }
            public void DeleteData(ItemStock _ent)
            {
                //Item = _ent.ItemCode;
                //Conn = _ent.Connection;

                //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //DT1.Rows.Add("Masterfile.Item", "cond", "ItemCode", _ent.ItemCode);
                //Gears.DeleteData(DT1, _ent.Connection);
                //Functions.AuditTrail("ITMMAST", Item, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
            }

            public class StockMasterSellThru
            {
                public virtual string StockNumber { get; set; }
                public virtual string DateEncoded { get; set; }
                public virtual string LineNumber { get; set; }
                public virtual string UserName { get; set; }
                public virtual string Analysis { get; set; }
                public virtual string ActionPlan { get; set; }
                public virtual int RecordID { get; set; }
                public virtual string ProductAlignment { get; set; }
                public virtual string Remarks { get; set; }

                public DataTable getdetail(string ItemCode, string Conn)
                {

                    DataTable a;
                    try
                    {

                        a = Gears.RetriveData2(string.Format("select StockNumber,LineNumber,CONVERT(varchar(20),DateEncoded) DateEncoded,UserName,Analysis,ActionPlan,ProductAlignment,Remarks from Retail.StockMasterSellThru where StockNumber = '{0}' order by LineNumber", ItemCode), Conn);
                        return a;
                    }
                    catch (Exception e)
                    {
                        a = null;
                        return a;
                    }
                }
                public void AddItemDetail(StockMasterSellThru _ent)
                {
                    int linenum = 0;

                    DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Retail.StockMasterSellThru where StockNumber = '" + Item + "'", Conn);

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
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "LineNumber", strLine);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "StockNumber", Item);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "DateEncoded", _ent.DateEncoded);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "UserName", _ent.UserName);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "Analysis", _ent.Analysis);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "ActionPlan", _ent.ActionPlan);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "ProductAlignment", _ent.ProductAlignment);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "0", "Remarks", _ent.Remarks); 

                    Gears.CreateData(DT1, Conn);
                }

                public void UpdateStock(StockMasterSellThru _ent)
                {
                    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                    DT1.Rows.Add("Retail.StockMasterSellThru", "cond", "StockNumber", _ent.StockNumber);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "cond", "LineNumber", _ent.LineNumber);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "set", "DateEncoded", _ent.DateEncoded);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "set", "UserName", _ent.UserName);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "set", "Analysis", _ent.Analysis);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "set", "ActionPlan", _ent.ActionPlan);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "set", "ProductAlignment", _ent.ProductAlignment);
                    DT1.Rows.Add("Retail.StockMasterSellThru", "set", "Remarks", _ent.Remarks);

                    Gears.UpdateData(DT1, Conn);
                }
            }
        }
        public class ItemPrice
        {
            public virtual ItemMasterfile Parent { get; set; }
            public virtual string ItemColor { get; set; }
            public virtual string ItemClass { get; set; }
            public virtual string ItemSize { get; set; }
            public virtual Nullable<DateTime> EffectivityDate { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal Price { get; set; }
        }

        public static object GetPropertyValue(object SourceData, string propName)
        {
            return SourceData.GetType().GetProperty(propName).GetValue(SourceData, null);
        }
    }
}
