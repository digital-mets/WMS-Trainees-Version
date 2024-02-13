using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class APVoucher
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string SupplierName { get; set; }
        public virtual string BrokerCode { get; set; }
        public virtual string BrokerName { get; set; }
        public virtual string ReferenceNumber { get; set; }
        public virtual string InvoiceNumber { get; set; }
        public virtual string ReferenceDate { get; set; }
        public virtual string DueDate { get; set; }
        public virtual decimal TotalAPAmount { get; set; }
        public virtual decimal TotalVATAmount { get; set; }
        public virtual decimal TotalEWTAmount { get; set; }
        public virtual string Remarks { get; set; }
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
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public virtual IList<APVoucherDetail> Detail { get; set; }

        public class APVoucherDetail
        {
            public virtual APVoucher Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Transtype { get; set; }
            public virtual string TransDocNumber { get; set; }
            public virtual DateTime TransDate { get; set; }
            public virtual decimal TransAPAmount { get; set; }
            public virtual decimal TransVatAmount { get; set; }
            public virtual decimal TransEWTAmount { get; set; }
            public virtual string Currency { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from accounting.apvoucherdetail where DocNumber='" + DocNumber + "' order by LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddAPVoucherDetail(APVoucherDetail APVoucherDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.APVoucherDetail where docnumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Transtype", APVoucherDetail.Transtype);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "TransDocNumber", APVoucherDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "TransDate", APVoucherDetail.TransDate);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "TransAPAmount", APVoucherDetail.TransAPAmount);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "TransVatAmount", APVoucherDetail.TransVatAmount);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "TransEWTAmount", APVoucherDetail.TransEWTAmount);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Currency", APVoucherDetail.Currency);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field1", APVoucherDetail.Field1);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field2", APVoucherDetail.Field2);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field3", APVoucherDetail.Field3);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field4", APVoucherDetail.Field4);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field5", APVoucherDetail.Field5);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field6", APVoucherDetail.Field6);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field7", APVoucherDetail.Field7);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field8", APVoucherDetail.Field8);
                DT1.Rows.Add("Accounting.APVoucherDetail", "0", "Field9", APVoucherDetail.Field9);

                DT2.Rows.Add("Accounting.APVoucher", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.APVoucher", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);
            }
            public void UpdateAPVoucherDetail(APVoucherDetail APVoucherDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.APVoucherDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.APVoucherDetail", "cond", "LineNumber", APVoucherDetail.LineNumber);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Transtype", APVoucherDetail.Transtype);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "TransDocNumber", APVoucherDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "TransDate", APVoucherDetail.TransDate);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "TransAPAmount", APVoucherDetail.TransAPAmount);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "TransVatAmount", APVoucherDetail.TransVatAmount);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "TransEWTAmount", APVoucherDetail.TransEWTAmount);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Currency", APVoucherDetail.Currency);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field1", APVoucherDetail.Field1);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field2", APVoucherDetail.Field2);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field3", APVoucherDetail.Field3);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field4", APVoucherDetail.Field4);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field5", APVoucherDetail.Field5);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field6", APVoucherDetail.Field6);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field7", APVoucherDetail.Field7);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field8", APVoucherDetail.Field8);
                DT1.Rows.Add("Accounting.APVoucherDetail", "set", "Field9", APVoucherDetail.Field9);

                Gears.UpdateData(DT1,Conn);
            }
            public void DeleteAPVoucherDetail(APVoucherDetail APVoucherDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.APVoucherDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.APVoucherDetail", "cond", "LineNumber", APVoucherDetail.LineNumber);


                Gears.DeleteData(DT1,Conn);

                DataTable count = Gears.RetriveData2("select * from Accounting.APVoucherDetail where docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.APVoucherDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.APVoucherDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }
            }
        }

        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.APVoucher where DocNumber = '" + DocNumber + "'",Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                SupplierName = dtRow["SupplierName"].ToString();
                BrokerCode = dtRow["BrokerCode"].ToString();
                BrokerName = dtRow["BrokerName"].ToString();
                ReferenceNumber = dtRow["ReferenceNumber"].ToString();
                InvoiceNumber = dtRow["InvoiceNumber"].ToString();
                ReferenceDate = dtRow["ReferenceDate"].ToString();
                DueDate = dtRow["DueDate"].ToString();
                TotalAPAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalAPAmount"].ToString()) ? 0 : dtRow["TotalAPAmount"]);
                TotalVATAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalVATAmount"].ToString()) ? 0 : dtRow["TotalVATAmount"]);
                TotalEWTAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalEWTAmount"].ToString()) ? 0 : dtRow["TotalEWTAmount"]);
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
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
            }

            return a;
        }
        public void InsertData(APVoucher _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.APVoucher", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.APVoucher", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.APVoucher", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.APVoucher", "0", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.APVoucher", "0", "BrokerCode", _ent.BrokerCode);
            DT1.Rows.Add("Accounting.APVoucher", "0", "BrokerName", _ent.BrokerName);
            DT1.Rows.Add("Accounting.APVoucher", "0", "ReferenceNumber", _ent.ReferenceNumber);
            DT1.Rows.Add("Accounting.APVoucher", "0", "InvoiceNumber", _ent.InvoiceNumber);
            DT1.Rows.Add("Accounting.APVoucher", "0", "ReferenceDate", _ent.ReferenceDate);
            DT1.Rows.Add("Accounting.APVoucher", "0", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Accounting.APVoucher", "0", "TotalAPAmount", _ent.TotalAPAmount);
            DT1.Rows.Add("Accounting.APVoucher", "0", "TotalVATAmount", _ent.TotalVATAmount);
            DT1.Rows.Add("Accounting.APVoucher", "0", "TotalEWTAmount", _ent.TotalEWTAmount);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.APVoucher", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.APVoucher", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.APVoucher", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1,_ent.Connection);
        }
        public void UpdateData(APVoucher _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.APVoucher", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.APVoucher", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.APVoucher", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.APVoucher", "set", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.APVoucher", "set", "BrokerCode", _ent.BrokerCode);
            DT1.Rows.Add("Accounting.APVoucher", "set", "BrokerName", _ent.BrokerName);
            DT1.Rows.Add("Accounting.APVoucher", "set", "ReferenceNumber", _ent.ReferenceNumber);
            DT1.Rows.Add("Accounting.APVoucher", "set", "InvoiceNumber", _ent.InvoiceNumber);
            DT1.Rows.Add("Accounting.APVoucher", "set", "ReferenceDate", _ent.ReferenceDate);
            DT1.Rows.Add("Accounting.APVoucher", "set", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Accounting.APVoucher", "set", "TotalAPAmount", _ent.TotalAPAmount);
            DT1.Rows.Add("Accounting.APVoucher", "set", "TotalVATAmount", _ent.TotalVATAmount);
            DT1.Rows.Add("Accounting.APVoucher", "set", "TotalEWTAmount", _ent.TotalEWTAmount);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.APVoucher", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.APVoucher", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.APVoucher", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1,_ent.Connection);

            Functions.AuditTrail("ACTAPV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }
        public void DeleteData(APVoucher _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.APVoucher", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("ACTAPV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }

        public class JournalEntry
        {
            public virtual APVoucher Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode,(case when isnull(B.Description,'') = '' then A.Description else B.Description end) AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " LEFT JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTAPV' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }            
        }
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.APVoucherDetail WHERE DocNumber = '" + DocNumber + "'", Conn);
        }
    }
}

