using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Storage
    {
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN
        public virtual string StorageStorageType { get; set; }
        public virtual string StorageStorageDescription { get; set; }

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

        public DataTable getdata(string Stotype, string Conn)
        {
            DataTable a;

            if (Stotype != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.StorageType where StorageType = '" + Stotype + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    StorageStorageType = dtRow["StorageType"].ToString();
                    StorageStorageDescription = dtRow["StorageDescription"].ToString();


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
        public void InsertData(Storage _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.StorageType", "0", "StorageType", _ent.StorageStorageType);
            DT1.Rows.Add("Masterfile.StorageType", "0", "StorageDescription", _ent.StorageStorageDescription);

            DT1.Rows.Add("Masterfile.StorageType", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.StorageType", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Type", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Type", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Type", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Type", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Type", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Type", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Type", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Type", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Type", "0", "Field9", _ent.Field9);


            Gears.CreateData(DT1, _ent.Connection);

            Functions.AuditTrail("REFSTORAGE", _ent.StorageStorageType, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateData(Storage _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.StorageType", "cond", "StorageType", _ent.StorageStorageType);
            DT1.Rows.Add("Masterfile.StorageType", "set", "StorageDescription", _ent.StorageStorageDescription);

            DT1.Rows.Add("Masterfile.StorageType", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.StorageType", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Type", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Type", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Type", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Type", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Type", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Type", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Type", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Type", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Type", "set", "Field9", _ent.Field9);



            string strErr = Gears.UpdateData(DT1, _ent.Connection);

           Functions.AuditTrail("REFSTORAGE", _ent.StorageStorageType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(Storage _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            StorageStorageType = _ent.StorageStorageType;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.StorageType", "cond", "StorageType", _ent.StorageStorageType);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFSTORAGE", _ent.StorageStorageType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
