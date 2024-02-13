using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class TransactionNonStorage
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string BillNumber { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }  
        public virtual string Field3 { get; set; }
        public virtual decimal Field4 { get; set; }
        public virtual decimal Field5 { get; set; }
        public virtual decimal Field6 { get; set; }
        public virtual decimal Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }


        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select * from WMS.TransactionNonStorage where DocNumber = '" + DocNumber + "'", Conn); //KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    DocDate = dtRow["Docdate"].ToString();
                    ServiceType = dtRow["ServiceType"].ToString();
                    BillNumber = dtRow["BillNumber"].ToString();
                    BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                    WarehouseCode = dtRow["WarehouseCode"].ToString();
                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = Convert.ToDecimal(Convert.IsDBNull(dtRow["Field4"]) ? 0 : dtRow["Field4"]);
                    Field5 = Convert.ToDecimal(Convert.IsDBNull(dtRow["Field5"]) ? 0 : dtRow["Field5"]);
                    Field6 = Convert.ToDecimal(Convert.IsDBNull(dtRow["Field6"]) ? 0 : dtRow["Field6"]);
                    Field7 = Convert.ToDecimal(Convert.IsDBNull(dtRow["Field7"]) ? 0 : dtRow["Field7"]);
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();

                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    SubmittedBy = dtRow["SubmittedBy"].ToString();
                    SubmittedDate = dtRow["SubmittedDate"].ToString();
                }
            
            return a;
        }
        public void InsertData(TransactionNonStorage _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;
            string strTableName = "WMS.TransactionNonStorage";
            string strParam = "0";
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add(strTableName, strParam, "DocNumber", _ent.DocNumber);
            DT1.Rows.Add(strTableName, strParam, "DocDate", _ent.DocDate);
            DT1.Rows.Add(strTableName, strParam, "ServiceType", _ent.ServiceType);
            DT1.Rows.Add(strTableName, strParam, "BillNumber", _ent.BillNumber);
            DT1.Rows.Add(strTableName, strParam, "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add(strTableName, strParam, "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add(strTableName, strParam, "Field1", _ent.Field1);
            DT1.Rows.Add(strTableName, strParam, "Field2", _ent.Field2);
            DT1.Rows.Add(strTableName, strParam, "Field3", _ent.Field3);
            DT1.Rows.Add(strTableName, strParam, "Field4", _ent.Field4);
            DT1.Rows.Add(strTableName, strParam, "Field5", _ent.Field5);
            DT1.Rows.Add(strTableName, strParam, "Field6", _ent.Field6);
            DT1.Rows.Add(strTableName, strParam, "Field7", _ent.Field7);
            DT1.Rows.Add(strTableName, strParam, "Field8", _ent.Field8);
            DT1.Rows.Add(strTableName, strParam, "Field9", _ent.Field9);
            DT1.Rows.Add(strTableName, strParam, "IsWithDetail", "False");


            DT1.Rows.Add(strTableName, strParam, "AddedBy", _ent.AddedBy);
            DT1.Rows.Add(strTableName, strParam, "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }

        public void UpdateData(TransactionNonStorage _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;
            string strTableName = "WMS.TransactionNonStorage";
            string strParam = "set";

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add(strTableName, "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add(strTableName, strParam, "DocDate", _ent.DocDate);
            DT1.Rows.Add(strTableName, strParam, "ServiceType", _ent.ServiceType);
            DT1.Rows.Add(strTableName, strParam, "BillNumber", _ent.BillNumber);
            DT1.Rows.Add(strTableName, strParam, "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add(strTableName, strParam, "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add(strTableName, strParam, "Field1", _ent.Field1);
            DT1.Rows.Add(strTableName, strParam, "Field2", _ent.Field2);
            DT1.Rows.Add(strTableName, strParam, "Field3", _ent.Field3);
            DT1.Rows.Add(strTableName, strParam, "Field4", _ent.Field4);
            DT1.Rows.Add(strTableName, strParam, "Field5", _ent.Field5);
            DT1.Rows.Add(strTableName, strParam, "Field6", _ent.Field6);
            DT1.Rows.Add(strTableName, strParam, "Field7", _ent.Field7);
            DT1.Rows.Add(strTableName, strParam, "Field8", _ent.Field8);
            DT1.Rows.Add(strTableName, strParam, "Field9", _ent.Field9);
        

            DT1.Rows.Add(strTableName, strParam, "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add(strTableName, strParam, "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSNON", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }

        public void DeleteData(TransactionNonStorage _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;
            string strTableName = "WMS.TransactionNonStorage";

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add(strTableName, "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSNON", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }

    }
}
