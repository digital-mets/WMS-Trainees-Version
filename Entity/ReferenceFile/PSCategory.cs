using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PSCategory
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        public virtual string ProductSubCatCode { get; set; }
        public virtual string ProductCategoryCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string IsRequired { get; set; }
        public virtual string Mnemonics { get; set; }
        public virtual decimal AllowancePerc { get; set; }
        
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

        public virtual bool IsRequiredSize { get; set; }
        public virtual string Gender { get; set; }
        public virtual string FabricGroup { get; set; }


        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public DataTable getdata(string ProdSubCat, string Conn) //Ter
        {
            DataTable a;

            if (ProdSubCat != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.ProductCategorySub where ProductSubCatCode = '" + ProdSubCat + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    ProductSubCatCode = dtRow["ProductSubCatCode"].ToString();
                    ProductCategoryCode = dtRow["ProductCategoryCode"].ToString();
                    AllowancePerc = Convert.ToDecimal(Convert.IsDBNull(dtRow["AllowancePerc"]) ? 0 : dtRow["AllowancePerc"]);
                    Description = dtRow["Description"].ToString();
                    Mnemonics = dtRow["Mnemonics"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();

                    IsRequiredSize = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsRequiredSize"]) ? false : dtRow["IsRequiredSize"]);
                    Gender = dtRow["Gender"].ToString();
                    FabricGroup = dtRow["FabricGroup"].ToString();

                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn); //Ter
            }
            return a;
        }
        public void InsertData(PSCategory _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "ProductSubCatCode", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "ProductCategoryCode", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "AllowancePerc", _ent.AllowancePerc);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Mnemonics", _ent.Mnemonics);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
      
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "IsRequiredSize", _ent.IsRequiredSize);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Gender", _ent.Gender);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "FabricGroup", _ent.FabricGroup);

            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER

            Functions.AuditTrail("REFPSCATEGORY", _ent.ProductSubCatCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // Ter
        }

        public void UpdateData(PSCategory _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.ProductCategorySub", "cond", "ProductSubCatCode", _ent.ProductSubCatCode);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "ProductCategoryCode", _ent.ProductCategoryCode);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "AllowancePerc", _ent.AllowancePerc);
            
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Mnemonics", _ent.Mnemonics);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    
            
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "IsRequiredSize", _ent.IsRequiredSize);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Gender", _ent.Gender);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "FabricGroup", _ent.FabricGroup);

            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.ProductCategorySub", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter

            Functions.AuditTrail("REFPSCATEGORY", _ent.ProductSubCatCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter
        }
        public void DeleteData(PSCategory _ent)
        {
            Conn = _ent.Connection; //Ter
            ProductSubCatCode = _ent.ProductSubCatCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.ProductCategorySub", "cond", "ProductSubCatCode", _ent.ProductSubCatCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFPSCATEGORY", _ent.ProductSubCatCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
