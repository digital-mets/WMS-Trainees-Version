using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CounterSlip
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string BizAccountCode { get; set; }
        public virtual string DateFrom { get; set; }
        public virtual string DateTo { get; set; }
        public virtual decimal TotalGrossVat { get; set; }
        public virtual decimal TotalGrossNonVat { get; set; }
        public virtual decimal TotalVAT { get; set; }
        public virtual decimal TotalAmountDue { get; set; }
        public virtual string RefTrans { get; set; }      
        public virtual string Remarks { get; set; }       
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
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual bool IsWithDetail { get; set; }

        public virtual IList<CounterSlipDetail> Detail { get; set; }


        public class CounterSlipDetail
        {
            public virtual CounterSlip Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string DRNumber { get; set; }
            public virtual DateTime DRDate { get; set; }
            public virtual string SalesInvoiceNo { get; set; }
            public virtual DateTime SIDate { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual decimal GrossVAT { get; set; }
            public virtual decimal GrossNonVAT { get; set; }
            public virtual decimal VATAmount { get; set; }
            public virtual decimal AmountDue { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual decimal UnitFactor { get; set; }
            public virtual string Version { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CounterSlipDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCounterSlipDetail(CounterSlipDetail CounterSlipDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Accounting.CounterSlipDetail WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "DRNumber", CounterSlipDetail.DRNumber);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "DRDate", CounterSlipDetail.DRDate);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "SalesInvoiceNo", CounterSlipDetail.SalesInvoiceNo);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "SIDate", CounterSlipDetail.SIDate);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Qty", CounterSlipDetail.Qty);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "GrossVAT", CounterSlipDetail.GrossVAT);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "GrossNonVAT", CounterSlipDetail.GrossNonVAT);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "VATAmount", CounterSlipDetail.VATAmount);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "AmountDue", CounterSlipDetail.AmountDue);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "StatusCode", CounterSlipDetail.StatusCode);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "BaseQty", CounterSlipDetail.BaseQty);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "BarcodeNo", CounterSlipDetail.BarcodeNo);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "UnitFactor", CounterSlipDetail.UnitFactor);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Version", "1");

                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field1", CounterSlipDetail.Field1);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field2", CounterSlipDetail.Field2);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field3", CounterSlipDetail.Field3);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field4", CounterSlipDetail.Field4);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field5", CounterSlipDetail.Field5);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field6", CounterSlipDetail.Field6);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field7", CounterSlipDetail.Field7);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field8", CounterSlipDetail.Field8);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "0", "Field9", CounterSlipDetail.Field9);

                DT2.Rows.Add("Accounting.CounterSlip", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CounterSlip", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCounterSlipDetail(CounterSlipDetail CounterSlipDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CounterSlipDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "cond", "LineNumber", CounterSlipDetail.LineNumber);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "DRNumber", CounterSlipDetail.DRNumber);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "DRDate", CounterSlipDetail.DRDate);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "SalesInvoiceNo", CounterSlipDetail.SalesInvoiceNo);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "SIDate", CounterSlipDetail.SIDate);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Qty", CounterSlipDetail.Qty);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "GrossVAT", CounterSlipDetail.GrossVAT);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "GrossNonVAT", CounterSlipDetail.GrossNonVAT);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "VATAmount", CounterSlipDetail.VATAmount);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "AmountDue", CounterSlipDetail.AmountDue);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "StatusCode", CounterSlipDetail.StatusCode);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "BaseQty", CounterSlipDetail.BaseQty);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "BarcodeNo", CounterSlipDetail.BarcodeNo);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "UnitFactor", CounterSlipDetail.UnitFactor);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Version", "1");

                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field1", CounterSlipDetail.Field1);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field2", CounterSlipDetail.Field2);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field3", CounterSlipDetail.Field3);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field4", CounterSlipDetail.Field4);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field5", CounterSlipDetail.Field5);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field6", CounterSlipDetail.Field6);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field7", CounterSlipDetail.Field7);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field8", CounterSlipDetail.Field8);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "set", "Field9", CounterSlipDetail.Field9);

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteCounterSlipDetail(CounterSlipDetail CounterSlipDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CounterSlipDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CounterSlipDetail", "cond", "LineNumber", CounterSlipDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.CounterSlipDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.CounterSlip", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.CounterSlip", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        public class RefTransaction
        {
            public virtual CounterSlip Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B on A.RMenuID = B.ModuleID inner join IT.MainMenu C on A.MenuID = C.ModuleID "
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='ACTCOS' OR  A.TransType='ACTCOS') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Accounting.CounterSlip WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                BizAccountCode = dtRow["BizAccountCode"].ToString();
                DateFrom = dtRow["DateFrom"].ToString();
                DateTo = dtRow["DateTo"].ToString();
                TotalGrossVat = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGrossVat"]) ? 0 : dtRow["TotalGrossVat"]);
                TotalGrossNonVat = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGrossNonVat"]) ? 0 : dtRow["TotalGrossNonVat"]);
                TotalVAT = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalVAT"]) ? 0 : dtRow["TotalVAT"]);
                TotalAmountDue = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountDue"]) ? 0 : dtRow["TotalAmountDue"]);
                RefTrans = dtRow["RefTrans"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                
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
        public void InsertData(CounterSlip _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CounterSlip", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "BizAccountCode", _ent.BizAccountCode);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "DateTo", _ent.DateTo);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "TotalGrossVat", _ent.TotalGrossVat);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "TotalGrossNonVat", _ent.TotalGrossNonVat);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "TotalVAT", _ent.TotalVAT);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CounterSlip", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CounterSlip", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(CounterSlip _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CounterSlip", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "BizAccountCode", _ent.BizAccountCode);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "DateTo", _ent.DateTo);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "TotalGrossVat", _ent.TotalGrossVat);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "TotalGrossNonVat", _ent.TotalGrossNonVat);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "TotalVAT", _ent.TotalVAT);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "TotalAmountDue", _ent.TotalAmountDue);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.CounterSlip", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CounterSlip", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCOS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CounterSlip _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CounterSlip", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.CounterSlipDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("ACTCOS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.CounterSlipDetail WHERE DocNumber = '" + DocNumber + "' AND Version = '1'", Conn);
        }

        public void UpdateOther(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("UPDATE Accounting.CounterSlipDetail SET DRNumber = SalesInvoiceNo , DRDate = SIDate  WHERE DocNumber = '" + DocNumber + "'", Conn);
        }
    }
}
