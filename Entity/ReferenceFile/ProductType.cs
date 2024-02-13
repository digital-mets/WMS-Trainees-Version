using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ProductType
    {
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN
        public virtual string SKUTypeCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string FontColor { get; set; }
        public virtual string BackColor { get; set; }


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
                a = Gears.RetriveData2("select * from Masterfile.ProductType where SKUTypeCode = '" + Colcode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    SKUTypeCode = dtRow["SKUTypeCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    FontColor = dtRow["FontColor"].ToString();
                    BackColor = dtRow["BackColor"].ToString();
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
        public void InsertData(ProductType _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.ProductType", "0", "SKUTypeCode", _ent.SKUTypeCode);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.ProductType", "0", "FontColor", _ent.FontColor);
            DT1.Rows.Add("Masterfile.ProductType", "0", "BackColor", _ent.BackColor);


            DT1.Rows.Add("Masterfile.ProductType", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.ProductType", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.ProductType", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.ProductType", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFPTM", _ent.SKUTypeCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateData(ProductType _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.ProductType", "cond", "SKUTypeCode", _ent.SKUTypeCode);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.ProductType", "set", "FontColor", _ent.FontColor);
            DT1.Rows.Add("Masterfile.ProductType", "set", "BackColor", _ent.BackColor);

            DT1.Rows.Add("Masterfile.ProductType", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.ProductType", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.ProductType", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.ProductType", "set", "Field9", _ent.Field9);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFPTM", _ent.SKUTypeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(ProductType _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            SKUTypeCode = _ent.SKUTypeCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.ProductType", "cond", "SKUTypeCode", _ent.SKUTypeCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFPTM", _ent.SKUTypeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
