using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class UnitConversion
    {
        private static string unit;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string ConversionCode { get; set; }
        public virtual string UnitCodeFrom { get; set; }
        public virtual string UnitCodeTo { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal ConversionFactor { get; set; }
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
         public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }



        public DataTable getdata(string unitcode, string Conn)
        {
            DataTable a;

            // if (DocNumber != null)
            // {
            a = Gears.RetriveData2("select * from Masterfile.UnitConversion where ConversionCode = '" + unitcode + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                ConversionCode = dtRow["ConversionCode"].ToString();
                UnitCodeFrom = dtRow["UnitCodeFrom"].ToString();
                UnitCodeTo = dtRow["UnitCodeTo"].ToString();
                Description = dtRow["Description"].ToString();
                ConversionFactor = String.IsNullOrEmpty(dtRow["ConversionFactor"].ToString()) ? 0 : Convert.ToDecimal(dtRow["ConversionFactor"].ToString());
              
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
            
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
            //  }
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as OutgoingDocNumber,'' as OutgoingDocType,'' as WarehouseCode,'' as StorerKey" +
            //        ",'' as TargetDate,''  as TargetDate,'' as  DeliveryDate" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(UnitConversion _ent)
        {
            unit = _ent.ConversionCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.UnitConversion", "0", "ConversionCode", _ent.ConversionCode);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "UnitCodeFrom", _ent.UnitCodeFrom);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "UnitCodeTo", _ent.UnitCodeTo);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "ConversionFactor", _ent.ConversionFactor);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "IsInactive", Convert.IsDBNull(_ent.IsInactive) ? false : _ent.IsInactive);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.UnitConversion", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.UnitConversion", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(UnitConversion _ent)
        {
            unit = _ent.ConversionCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.UnitConversion", "cond", "ConversionCode", _ent.ConversionCode);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "UnitCodeFrom", _ent.UnitCodeFrom);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "UnitCodeTo", _ent.UnitCodeTo);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "ConversionFactor", _ent.ConversionFactor);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "IsInactive", Convert.IsDBNull(_ent.IsInactive) ? false : _ent.IsInactive);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.UnitConversion", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.UnitConversion", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFUNITCON", unit, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

        }
        public void DeleteData(UnitConversion _ent)
        {
            unit = _ent.ConversionCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.UnitConversion", "cond", "ConversionCode", _ent.ConversionCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFUNITCON", unit, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
