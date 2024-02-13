using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BillingNonStorage
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string ContractNumber { get; set; }
        public virtual string BillingPeriodType { get; set; }
        public virtual string DateFrom { get; set; }
        public virtual string DateTo { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual string BillingStatement { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal TotalVat { get; set; }
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
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual IList<BillingNonStorageDetail> Detail { get; set; }


        public class BillingNonStorageDetail
        {
            public virtual BillingNonStorage Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }


            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual decimal Field4 { get; set; }
            public virtual decimal Field5 { get; set; }
            public virtual decimal Field6 { get; set; }
            public virtual decimal Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from WMS.BillingOtherServiceDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddBillingNonStorageDetail(BillingNonStorageDetail BillingNonStorageDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.BillingOtherServiceDetail where docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field1", BillingNonStorageDetail.Field1);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field2", BillingNonStorageDetail.Field2);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field3", BillingNonStorageDetail.Field3);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field4", BillingNonStorageDetail.Field4);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field5", BillingNonStorageDetail.Field5);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field6", BillingNonStorageDetail.Field6);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field7", BillingNonStorageDetail.Field7);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field8", BillingNonStorageDetail.Field8);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "0", "Field9", BillingNonStorageDetail.Field9);

                DT2.Rows.Add("WMS.BillingOtherService", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.BillingOtherService", "set", "IsWithDetail", "True");
                //DT2.Rows.Add("WMS.BillingOtherService", "set", "IsValidated", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateBillingNonStorageDetail(BillingNonStorageDetail BillingNonStorageDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "cond", "DocNumber", BillingNonStorageDetail.DocNumber);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "cond", "LineNumber", BillingNonStorageDetail.LineNumber);


                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field1", BillingNonStorageDetail.Field1);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field2", BillingNonStorageDetail.Field2);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field3", BillingNonStorageDetail.Field3);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field4", BillingNonStorageDetail.Field4);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field5", BillingNonStorageDetail.Field5);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field6", BillingNonStorageDetail.Field6);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field7", BillingNonStorageDetail.Field7);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field8", BillingNonStorageDetail.Field8);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "set", "Field9", BillingNonStorageDetail.Field9);

                Gears.UpdateData(DT1, Conn);//KMM add Conn
            }
            public void DeleteBillingNonStorageDetail(BillingNonStorageDetail BillingNonStorageDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "cond", "DocNumber", BillingNonStorageDetail.DocNumber);
                DT1.Rows.Add("WMS.BillingOtherServiceDetail", "cond", "LineNumber", BillingNonStorageDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);//KMM add Conn

                DataTable count = Gears.RetriveData2("select * from WMS.BillingOtherServiceDetail where docnumber = '" + Docnum + "'", Conn);//KMM add Conn

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.BillingOtherServiceDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.BillingOtherServiceDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//KMM add Conn
                }

            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from WMS.BillingOtherService where DocNumber = '" + DocNumber + "'", Conn); //KMM add Conn
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                ContractNumber = dtRow["ContractNumber"].ToString();
                BillingPeriodType = dtRow["BillingPeriodType"].ToString();
                DateFrom = dtRow["DateFrom"].ToString();
                DateTo = dtRow["DateTo"].ToString();
                ServiceType = dtRow["ServiceType"].ToString();
                BillingStatement = dtRow["BillingStatement"].ToString();
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                TotalVat = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalVat"]) ? 0 : dtRow["TotalVat"]);


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
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as ExpectedDeliveryDate,'' as WarehouseCode,'' as StorerKey,'' as Field1"+
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(BillingNonStorage _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.BillingOtherService", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "ContractNumber", _ent.ContractNumber);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "BillingStatement", _ent.BillingStatement);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "TotalVat", _ent.TotalVat);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "IsWithDetail", "False");
            //DT1.Rows.Add("WMS.BillingOtherService", "0", "IsValidated", "False");
            DT1.Rows.Add("WMS.BillingOtherService", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.BillingOtherService", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }

        public void UpdateData(BillingNonStorage _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.BillingOtherService", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "ContractNumber", _ent.ContractNumber);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "BillingStatement", _ent.BillingStatement);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "TotalVat", _ent.TotalVat);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.BillingOtherService", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSBOS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }

        public void DeleteData(BillingNonStorage _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.BillingOtherService", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSBOS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }

        public class JournalEntry
        {
            private static string Conn { get; set; }
            public virtual string Connection { get; set; }
            public virtual BillingNonStorage Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='WMSBOS' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
    }
}
