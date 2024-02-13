using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class GenericLookup
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string LookupKey { get; set; }
        public virtual string Description { get; set; }
        public virtual string Code { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public DataTable getdata(string lookup,string Conn)
        {
            DataTable a;

            if (lookup != null)
            {
                a = Gears.RetriveData2("select * from IT.GenericLookup where LookupKey = '" + lookup + "'",Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    LookupKey = dtRow["LookupKey"].ToString();
                    Description = dtRow["Description"].ToString();
                    Code = dtRow["Code"].ToString();
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
                a = Gears.RetriveData2("select '' as DocNumber,'' as Column,'' as OldValue,'' as Name,'' as EditedDate",Conn);
            }
            return a;
        }
        public void InsertData(GenericLookup _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.GenericLookup", "0", "LookupKey", _ent.LookupKey);
            DT1.Rows.Add("IT.GenericLookup", "0", "Description", _ent.Description);
            DT1.Rows.Add("IT.GenericLookup", "0", "Code", _ent.Code);
            DT1.Rows.Add("IT.GenericLookup", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.GenericLookup", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("IT.GenericLookup", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.GenericLookup", "0", "Field9", _ent.Field9);
            Gears.CreateData(DT1,_ent.Connection);
        }
        public void UpdateData(GenericLookup _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.GenericLookup", "cond", "LookupKey", _ent.LookupKey);
            DT1.Rows.Add("IT.GenericLookup", "cond", "Code", _ent.Code);
            DT1.Rows.Add("IT.GenericLookup", "set", "Description", _ent.Description);
            DT1.Rows.Add("IT.GenericLookup", "set", "Code", _ent.Code);
            DT1.Rows.Add("IT.GenericLookup", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.GenericLookup", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("IT.GenericLookup", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.GenericLookup", "set", "Field9", _ent.Field9);
            string strErr = Gears.UpdateData(DT1,_ent.Connection);
        }
        public void DeleteData(GenericLookup _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.GenericLookup", "cond", "LookupKey", _ent.LookupKey);
            DT1.Rows.Add("IT.GenericLookup", "cond", "Code", _ent.Code);
            Gears.DeleteData(DT1,_ent.Connection);
        }
    }
}
