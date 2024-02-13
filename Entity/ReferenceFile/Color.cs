using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Color
    {
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN
        public virtual string ColorColorCode { get; set; }
        public virtual string ColorDescription { get; set; }
        public virtual string ColorColorGroup { get; set; }
        public virtual string ColorSource { get; set; }
        public virtual string ColorMnemonics { get; set; }
        public virtual bool ColorIsInactive { get; set; }
        public virtual string ColorRecordID { get; set; }
        public virtual string ColorItemCategory { get; set; }
        public virtual string ColorProductCategory { get; set; }
        public virtual string ColorProductSubCategory { get; set; }
        public virtual string ColorR { get; set; }
        public virtual string ColorG { get; set; }
        public virtual string ColorB { get; set; }
        public virtual string ColorReferenceCode { get; set; }
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
        public DataTable getdata(string Colcode, string Conn)
        {
            DataTable a;

            if (Colcode != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.Color where ColorCode = '" + Colcode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    ColorColorCode = dtRow["ColorCode"].ToString();
                    ColorDescription = dtRow["Description"].ToString();
                    ColorColorGroup = dtRow["ColorGroup"].ToString();
                    ColorSource = dtRow["Source"].ToString();
                    ColorMnemonics = dtRow["Mnemonics"].ToString();
                    ColorIsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]); 
                    ColorItemCategory = dtRow["ItemCategory"].ToString();
                    ColorProductCategory = dtRow["ProductCategory"].ToString();
                    ColorProductSubCategory = dtRow["ProductSubCategory"].ToString();
                    ColorR = dtRow["R"].ToString();
                    ColorG = dtRow["G"].ToString();
                    ColorB = dtRow["B"].ToString();
                    ColorReferenceCode = dtRow["ReferenceCode"].ToString();


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
        public void InsertData(Color _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Color", "0", "ColorCode", _ent.ColorColorCode);
            DT1.Rows.Add("Masterfile.Color", "0", "Description", _ent.ColorDescription);
            DT1.Rows.Add("Masterfile.Color", "0", "ColorGroup", _ent.ColorColorGroup);
            DT1.Rows.Add("Masterfile.Color", "0", "Source", _ent.ColorSource);
            DT1.Rows.Add("Masterfile.Color", "0", "Mnemonics", _ent.ColorMnemonics);
            DT1.Rows.Add("Masterfile.Color", "0", "IsInactive", _ent.ColorIsInactive);
            DT1.Rows.Add("Masterfile.Color", "0", "ItemCategory", _ent.ColorItemCategory);
            DT1.Rows.Add("Masterfile.Color", "0", "ProductCategory", _ent.ColorProductCategory);
            DT1.Rows.Add("Masterfile.Color", "0", "ProductSubCategory", _ent.ColorProductSubCategory);
            DT1.Rows.Add("Masterfile.Color", "0", "R", _ent.ColorR);
            DT1.Rows.Add("Masterfile.Color", "0", "G", _ent.ColorG);
            DT1.Rows.Add("Masterfile.Color", "0", "B", _ent.ColorB);
            DT1.Rows.Add("Masterfile.Color", "0", "ReferenceCode", _ent.ColorReferenceCode);

            DT1.Rows.Add("Masterfile.Color", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Color", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Class", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Class", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Class", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Class", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Class", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Class", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Class", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Class", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Class", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFCOLOR", _ent.ColorColorCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateData(Color _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Color", "cond", "ColorCode", _ent.ColorColorCode);
            DT1.Rows.Add("Masterfile.Color", "set", "Description", _ent.ColorDescription);
            DT1.Rows.Add("Masterfile.Color", "set", "ColorGroup", _ent.ColorColorGroup);
            DT1.Rows.Add("Masterfile.Color", "set", "Source", _ent.ColorSource);
            DT1.Rows.Add("Masterfile.Color", "set", "Mnemonics", _ent.ColorMnemonics);
            DT1.Rows.Add("Masterfile.Color", "set", "IsInactive", _ent.ColorIsInactive);
            DT1.Rows.Add("Masterfile.Color", "set", "ItemCategory", _ent.ColorItemCategory);
            DT1.Rows.Add("Masterfile.Color", "set", "ProductCategory", _ent.ColorProductCategory);
            DT1.Rows.Add("Masterfile.Color", "set", "ProductSubCategory", _ent.ColorProductSubCategory);
            DT1.Rows.Add("Masterfile.Color", "set", "R", _ent.ColorR);
            DT1.Rows.Add("Masterfile.Color", "set", "G", _ent.ColorG);
            DT1.Rows.Add("Masterfile.Color", "set", "B", _ent.ColorB);
            DT1.Rows.Add("Masterfile.Color", "set", "ReferenceCode", _ent.ColorReferenceCode);

            DT1.Rows.Add("Masterfile.Color", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Color", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Class", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Class", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Class", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Class", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Class", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Class", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Class", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Class", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Class", "set", "Field9", _ent.Field9);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFCOLOR", _ent.ColorColorCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(Color _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            ColorColorCode = _ent.ColorColorCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Color", "cond", "ColorCode", _ent.ColorColorCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFCOLOR", _ent.ColorColorCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
