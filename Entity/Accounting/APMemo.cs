using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class APMemo
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string SupplierName { get; set; }
        public virtual string Type { get; set; }
        public virtual string ReferenceDocnumber { get; set; }
        public virtual string ReferenceDate { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal TotalGrossAmount { get; set; }
        public virtual decimal TotalVatAmount { get; set; }
        public virtual decimal TotalAmount { get; set; }
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

        public virtual IList<APMemoDetail> Detail { get; set; }

        public class APMemoDetail
        {
            public virtual APMemo Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string TransDocNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Quantity { get; set; }
            public virtual decimal Price { get; set; }
            public virtual decimal Amount { get; set; }
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
                    a = Gears.RetriveData2("select *,b.FullDesc as ItemDescription from Accounting.APMemoDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddAPMemoDetail(APMemoDetail APMemoDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.APMemoDetail where docnumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "ItemCode", APMemoDetail.ItemCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "TransDocNumber", APMemoDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "ColorCode", APMemoDetail.ColorCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "ClassCode", APMemoDetail.ClassCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "SizeCode", APMemoDetail.SizeCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Quantity", APMemoDetail.Quantity);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Price", APMemoDetail.Price);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Amount", APMemoDetail.Amount);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field1", APMemoDetail.Field1);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field2", APMemoDetail.Field2);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field3", APMemoDetail.Field3);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field4", APMemoDetail.Field4);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field5", APMemoDetail.Field5);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field6", APMemoDetail.Field6);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field7", APMemoDetail.Field7);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field8", APMemoDetail.Field8);
                DT1.Rows.Add("Accounting.APMemoDetail", "0", "Field9", APMemoDetail.Field9);

                DT2.Rows.Add("Accounting.APMemo", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.APMemo", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);
            }
            public void UpdateAPMemoDetail(APMemoDetail APMemoDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.APMemoDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.APMemoDetail", "cond", "LineNumber", APMemoDetail.LineNumber);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "TransDocNumber", APMemoDetail.TransDocNumber);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "ItemCode", APMemoDetail.ItemCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "ColorCode", APMemoDetail.ColorCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "ClassCode", APMemoDetail.ClassCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "SizeCode", APMemoDetail.SizeCode);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Quantity", APMemoDetail.Quantity);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Price", APMemoDetail.Price);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Amount", APMemoDetail.Amount);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field1", APMemoDetail.Field1);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field2", APMemoDetail.Field2);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field3", APMemoDetail.Field3);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field4", APMemoDetail.Field4);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field5", APMemoDetail.Field5);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field6", APMemoDetail.Field6);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field7", APMemoDetail.Field7);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field8", APMemoDetail.Field8);
                DT1.Rows.Add("Accounting.APMemoDetail", "set", "Field9", APMemoDetail.Field9);

                Gears.UpdateData(DT1,Conn);
            }
            public void DeleteAPMemoDetail(APMemoDetail APMemoDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.APMemoDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.APMemoDetail", "cond", "LineNumber", APMemoDetail.LineNumber);
                Gears.DeleteData(DT1,Conn);


                //DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "DocNumber", Docnum);
                //DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", APMemoDetail.LineNumber);
                //DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "ACTARM");
                //Gears.DeleteData(DT3, Conn);


                DataTable count = Gears.RetriveData2("select * from Accounting.APMemoDetail where docnumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.APMemoDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.APMemoDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }
            }
        }

        public class JournalEntry
        {
            public virtual APMemo Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTAPM' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select * from Accounting.APMemo where DocNumber = '" + DocNumber + "'",Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                SupplierName = dtRow["SupplierName"].ToString();
                Type = dtRow["Type"].ToString();
                ReferenceDocnumber = dtRow["ReferenceDocnumber"].ToString();
                ReferenceDate = dtRow["ReferenceDate"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                TotalGrossAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalGrossAmount"].ToString()) ? 0 : dtRow["TotalGrossAmount"]);
                TotalVatAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalVatAmount"].ToString()) ? 0 : dtRow["TotalVatAmount"]);
                TotalAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalAmount"].ToString()) ? 0 : dtRow["TotalAmount"]);
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
            }

            return a;
        }
        public void InsertDataTAX(APMemo _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.APMemo", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.APMemo", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.APMemo", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.APMemo", "0", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.APMemo", "0", "Type", _ent.Type);
            DT1.Rows.Add("Accounting.APMemo", "0", "ReferenceDocnumber", _ent.ReferenceDocnumber);
            DT1.Rows.Add("Accounting.APMemo", "0", "ReferenceDate", _ent.ReferenceDate);
            DT1.Rows.Add("Accounting.APMemo", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.APMemo", "0", "TotalGrossAmount", _ent.TotalGrossAmount);
            DT1.Rows.Add("Accounting.APMemo", "0", "TotalVatAmount", _ent.TotalVatAmount);
            DT1.Rows.Add("Accounting.APMemo", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Accounting.APMemo", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.APMemo", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.APMemo", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void InsertData(APMemo _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.APMemo", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.APMemo", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.APMemo", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.APMemo", "0", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.APMemo", "0", "Type", _ent.Type);
            DT1.Rows.Add("Accounting.APMemo", "0", "ReferenceDocnumber", _ent.ReferenceDocnumber);
            DT1.Rows.Add("Accounting.APMemo", "0", "ReferenceDate", _ent.ReferenceDate);
            DT1.Rows.Add("Accounting.APMemo", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.APMemo", "0", "TotalGrossAmount", _ent.TotalGrossAmount);
            DT1.Rows.Add("Accounting.APMemo", "0", "TotalVatAmount", _ent.TotalVatAmount);
            DT1.Rows.Add("Accounting.APMemo", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.APMemo", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.APMemo", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.APMemo", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1,_ent.Connection);
        }
        public void UpdateData(APMemo _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.APMemo", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.APMemo", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.APMemo", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.APMemo", "set", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.APMemo", "set", "Type", _ent.Type);
            DT1.Rows.Add("Accounting.APMemo", "set", "ReferenceDocnumber", _ent.ReferenceDocnumber);
            DT1.Rows.Add("Accounting.APMemo", "set", "ReferenceDate", _ent.ReferenceDate);
            DT1.Rows.Add("Accounting.APMemo", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.APMemo", "set", "TotalGrossAmount", _ent.TotalGrossAmount);
            DT1.Rows.Add("Accounting.APMemo", "set", "TotalVatAmount", _ent.TotalVatAmount);
            DT1.Rows.Add("Accounting.APMemo", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.APMemo", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.APMemo", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.APMemo", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1,_ent.Connection);

            Functions.AuditTrail("ACTAPM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }
        public void DeleteData(APMemo _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.APMemo", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("ACTAPM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
    }
}

