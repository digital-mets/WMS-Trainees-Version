using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ItemCategory
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string ItemCategoryCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string ItemCodeFormat { get; set; }
        public virtual string InventoryGLCode { get; set; }
        public virtual string AdjustmentGLCode { get; set; }
        public virtual string GLSubsiCode { get; set; }
        public virtual int Threshold { get; set; }
        public virtual string CostingMethod { get; set; }
        public virtual string BaseUnit { get; set; }
        public virtual string BulkUnit { get; set; }
        public virtual bool IsAllowNega { get; set; }
        public virtual bool AllowZeroCost { get; set; }
        public virtual bool IsForecasted { get; set; }
        public virtual string SalesGLCode { get; set; }
        public virtual string SalesReturnGLCode { get; set; }
        public virtual string ARGLCode { get; set; }
        public virtual string CostOfGoodsGLCode { get; set; }
        public virtual string AccumulatedGLCode { get; set; }
        public virtual string DepreciationGLCode { get; set; }
        public virtual bool IsAsset { get; set; }
        public virtual int AssetLife { get; set; }
        public virtual string LookUpCaption { get; set; }
        public virtual string LookUpKey { get; set; }
        public virtual bool Required { get; set; }
        public virtual string Allocation { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual bool IsStock { get; set; }
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



        public DataTable getdata(string ItemCat, string Conn)//KMM add Conn
        {
            DataTable a;

            if (ItemCat != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.ItemCategory where ItemCategoryCode = '" + ItemCat + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {
                   
                    ItemCategoryCode = dtRow["ItemCategoryCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    ItemCodeFormat = dtRow["ItemCodeFormat"].ToString();
                    InventoryGLCode = dtRow["InventoryGLCode"].ToString();
                    AdjustmentGLCode = dtRow["AdjustmentGLCode"].ToString();
                    GLSubsiCode = dtRow["GLSubsiCode"].ToString();
                    Threshold = String.IsNullOrEmpty(dtRow["Threshold"].ToString()) ? 0 : Convert.ToInt32(dtRow["Threshold"].ToString());
                    CostingMethod = dtRow["CostingMethod"].ToString();
                    BaseUnit = dtRow["BaseUnit"].ToString();
                    BulkUnit = dtRow["BulkUnit"].ToString();
                    IsAllowNega = Convert.ToBoolean(dtRow["IsAllowNega"].ToString());
                    AllowZeroCost = Convert.ToBoolean(dtRow["AllowZeroCost"].ToString());
                    IsForecasted = Convert.ToBoolean(dtRow["IsForecasted"].ToString());
                    SalesGLCode = dtRow["SalesGLCode"].ToString();
                    SalesReturnGLCode = dtRow["SalesReturnGLCode"].ToString();
                    ARGLCode = dtRow["ARGLCode"].ToString();
                    CostOfGoodsGLCode = dtRow["CostOfGoodsGLCode"].ToString();
                    AccumulatedGLCode = dtRow["AccumulatedGLCode"].ToString();
                    DepreciationGLCode = dtRow["DepreciationGLCode"].ToString();
                    IsAsset = Convert.ToBoolean(dtRow["IsAsset"].ToString());
                    AssetLife = String.IsNullOrEmpty(dtRow["AssetLife"].ToString()) ? 0 : Convert.ToInt32(dtRow["AssetLife"].ToString());
                    LookUpCaption = dtRow["LookUpCaption"].ToString();
                    LookUpKey = dtRow["LookUpKey"].ToString();
                    Required = Convert.ToBoolean(dtRow["Required"].ToString());
                    IsInactive = Convert.ToBoolean(dtRow["IsInactive"].ToString());
                    IsStock = Convert.ToBoolean(dtRow["IsStock"].ToString());
                    Allocation = dtRow["Allocation"].ToString();
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

            }

            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
            return a;
        }
        public void InsertData(ItemCategory _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.ItemCategory", "0", "ItemCategoryCode", _ent.ItemCategoryCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "ItemCodeFormat", _ent.ItemCodeFormat);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "InventoryGLCode", _ent.InventoryGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "AdjustmentGLCode", _ent.AdjustmentGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "GLSubsiCode", _ent.GLSubsiCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Threshold", _ent.Threshold);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "CostingMethod", _ent.CostingMethod);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "BaseUnit", _ent.BaseUnit);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "BulkUnit", _ent.BulkUnit);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "IsAllowNega", _ent.IsAllowNega);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "AllowZeroCost", _ent.AllowZeroCost);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "IsForecasted", _ent.IsForecasted);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "SalesGLCode", _ent.SalesGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "SalesReturnGLCode", _ent.SalesReturnGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "ARGLCode", _ent.ARGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "CostOfGoodsGLCode", _ent.CostOfGoodsGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "AccumulatedGLCode", _ent.AccumulatedGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "DepreciationGLCode", _ent.DepreciationGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "IsAsset", _ent.IsAsset);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "AssetLife", _ent.AssetLife);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "LookUpCaption", _ent.LookUpCaption);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "LookUpKey", _ent.LookUpKey);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Required", _ent.Required);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "IsStock", _ent.IsStock);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Allocation", _ent.Allocation);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.ItemCategory", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);//KMM add Conn

           // Functions.AuditTrail("REFITMCAT", _ent.ItemCategoryCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }

        public void UpdateData(ItemCategory _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.ItemCategory", "cond", "ItemCategoryCode", _ent.ItemCategoryCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "ItemCodeFormat", _ent.ItemCodeFormat);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "InventoryGLCode", _ent.InventoryGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "AdjustmentGLCode", _ent.AdjustmentGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "GLSubsiCode", _ent.GLSubsiCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Threshold", _ent.Threshold);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "CostingMethod", _ent.CostingMethod);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "BaseUnit", _ent.BaseUnit);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "BulkUnit", _ent.BulkUnit);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "IsAllowNega", _ent.IsAllowNega);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "AllowZeroCost", _ent.AllowZeroCost);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "IsForecasted", _ent.IsForecasted);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "SalesGLCode", _ent.SalesGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "SalesReturnGLCode", _ent.SalesReturnGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "ARGLCode", _ent.ARGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "CostOfGoodsGLCode", _ent.CostOfGoodsGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "AccumulatedGLCode", _ent.AccumulatedGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "DepreciationGLCode", _ent.DepreciationGLCode);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "IsAsset", _ent.IsAsset);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "AssetLife", _ent.AssetLife);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "LookUpCaption", _ent.LookUpCaption);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "LookUpKey", _ent.LookUpKey);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Required", _ent.Required);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "IsStock", _ent.IsStock);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Allocation", _ent.Allocation);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.ItemCategory", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFITMCAT", ItemCategoryCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn
        }
        
        public void DeleteData(ItemCategory _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.ItemCategory", "cond", "ItemCategoryCode", _ent.ItemCategoryCode);
            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFITMCAT", ItemCategoryCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn



        }
    }
}
