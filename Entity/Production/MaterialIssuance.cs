using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class MaterialIssuance
    {
        private static string Docnum;
        private static string ddate;
        private static string trans;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string Transaction { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TransType { get; set; }
        public virtual string Type { get; set; }
        public virtual string RefNumber { get; set; }
        public virtual string IssuedTo { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual bool IsWithReference { get; set; }
       
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual IList<MaterialIssuanceDetail> Detail { get; set; }
                
        public class MaterialIssuanceDetail
        {
            public virtual MaterialIssuance Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string MtlType { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual decimal ReqQty { get; set; }
            public virtual decimal IssuedQty { get; set; }
            public virtual string UOM { get; set; }
            public virtual string Shift { get; set; }
           
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.MaterialIssuanceDetail WHERE DocNumber = '" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddIssuanceDetail(MaterialIssuanceDetail IssuanceDetail)
            {

                int linenum = 0;
                bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.MaterialIssuanceDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "ItemCode", IssuanceDetail.ItemCode);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "MtlType", IssuanceDetail.MtlType);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "BatchNo", IssuanceDetail.BatchNo);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "ReqQty", IssuanceDetail.ReqQty);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "IssuedQty", IssuanceDetail.IssuedQty);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "UOM", IssuanceDetail.UOM);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Shift", IssuanceDetail.Shift);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "IssuedQty", IssuanceDetail.IssuedQty);
              

                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field1", IssuanceDetail.Field1);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field2", IssuanceDetail.Field2);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field3", IssuanceDetail.Field3);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field4", IssuanceDetail.Field4);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field5", IssuanceDetail.Field5);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field6", IssuanceDetail.Field6);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field7", IssuanceDetail.Field7);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field8", IssuanceDetail.Field8);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field9", IssuanceDetail.Field9);

            

                DT2.Rows.Add("Production.MaterialIssuanceDetail", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.MaterialIssuanceDetail", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

              
            }
            public void UpdateIssuanceDetail(MaterialIssuanceDetail IssuanceDetail)
            {
             

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "cond", "LineNumber", IssuanceDetail.LineNumber);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "ItemCode", IssuanceDetail.ItemCode);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "MtlType", IssuanceDetail.MtlType);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "BatchNo", IssuanceDetail.BatchNo);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "ReqQty", IssuanceDetail.ReqQty);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "IssuedQty", IssuanceDetail.IssuedQty);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "UOM", IssuanceDetail.UOM);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Shift", IssuanceDetail.Shift);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "IssuedQty", IssuanceDetail.IssuedQty);


                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field1", IssuanceDetail.Field1);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field2", IssuanceDetail.Field2);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field3", IssuanceDetail.Field3);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field4", IssuanceDetail.Field4);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field5", IssuanceDetail.Field5);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field6", IssuanceDetail.Field6);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field7", IssuanceDetail.Field7);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field8", IssuanceDetail.Field8);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "0", "Field9", IssuanceDetail.Field9);

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteIssuanceDetail(MaterialIssuanceDetail IssuanceDetail)
            {                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.MaterialIssuanceDetail", "cond", "LineNumber", IssuanceDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Production.MaterialIssuanceDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.MaterialIssuance", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.MaterialIssuance", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }

    
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT DISTINCT * FROM Production.MaterialIssuance WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                TransType = dtRow["TransType"].ToString();
                IssuedTo = dtRow["IssuedTo"].ToString();
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                Type = dtRow["MaterialType"].ToString();
                RefNumber = dtRow["ReferenceNumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                IsWithReference = Convert.ToBoolean(Convert.IsDBNull(dtRow["WithRef"]) ? false : dtRow["WithRef"]);
           
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
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
        
            }

            return a;
        }
        public void InsertData(MaterialIssuance _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            trans = _ent.Transaction;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.MaterialIssuance", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "TransType", _ent.TransType);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "IssuedTo", _ent.IssuedTo);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "MaterialType", _ent.Type);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "WithRef", _ent.IsWithReference);
     


            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.MaterialIssuance", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.MaterialIssuance", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(MaterialIssuance _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.MaterialIssuance", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "TransType", _ent.TransType);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "IssuedTo", _ent.IssuedTo);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "MaterialType", _ent.Type);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "WithRef", _ent.IsWithReference);
     
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.MaterialIssuance", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.MaterialIssuance", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(MaterialIssuance _ent)
        {
            Docnum = _ent.DocNumber;
            trans = _ent.Transaction;
            ddate = _ent.DocDate;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.MaterialIssuance", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Functions.AuditTrail(trans, Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }        
     
    }
}
