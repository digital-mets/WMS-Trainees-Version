using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DocNumberSettings
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string RecordId { get; set; }
        public virtual string Module { get; set; }
        public virtual string Prefix { get; set; }
        public virtual string Counter { get; set; }
        public virtual string Description { get; set; }
        public virtual string SeriesWidth { get; set; }
        public virtual string SeriesNumber { get; set; }
        public virtual bool IsDefault { get; set; }
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



        public DataTable getdata(string Doc, string Conn)//KMM add Conn
        {
            DataTable a;

            if (Doc != null)
            {
                a = Gears.RetriveData2("select * from IT.DocNumberSettings where RecordId = '" + Doc + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {
                    RecordId = dtRow["RecordId"].ToString();
                    Module = dtRow["Module"].ToString();
                    Prefix = dtRow["Prefix"].ToString();
                    Counter = dtRow["Counter"].ToString();
                    Description = dtRow["Description"].ToString();
                    SeriesWidth = dtRow["SeriesWidth"].ToString();
                    SeriesNumber = dtRow["SeriesNumber"].ToString();
                    IsDefault = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsDefault"]) ? false : dtRow["IsDefault"]);
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

            }

            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
            return a;
        }
        public void InsertData(DocNumberSettings _ent)
        {

            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //DT1.Rows.Add("IT.DocNumberSettings", "0", "RecordId", _ent.RecordId);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Module", _ent.Module);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Prefix", _ent.Prefix);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Counter", _ent.Counter);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Description", _ent.Description);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "SeriesWidth", _ent.SeriesWidth);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "SeriesNumber", _ent.SeriesNumber);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "IsDefault", _ent.IsDefault);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.DocNumberSettings", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);//KMM add Conn

            Functions.AuditTrail("REFDOC", _ent.RecordId, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);//KMM add Conn
        }

        public void UpdateData(DocNumberSettings _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.DocNumberSettings", "cond", "RecordId", _ent.RecordId);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Module", _ent.Module);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Prefix", _ent.Prefix);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Counter", _ent.Counter);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Description", _ent.Description);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "SeriesWidth", _ent.SeriesWidth);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "SeriesNumber", _ent.SeriesNumber);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "IsDefault", _ent.IsDefault);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("IT.DocNumberSettings", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFDOC", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn
        }

        public void DeleteData(DocNumberSettings _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.DocNumberSettings", "cond", "RecordId", _ent.RecordId);
            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFDOC", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn



        }
    }
}
