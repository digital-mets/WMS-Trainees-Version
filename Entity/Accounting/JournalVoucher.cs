using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{   
    public class JournalVoucher
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Memo { get; set; }
        public virtual string RefTemCode { get; set; }
        public virtual string TemplateType { get; set; }
        public virtual decimal TotalDebit { get; set; }
        public virtual decimal TotalCredit { get; set; }
        

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
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }


        public virtual IList<JournalVoucherDetail> Detail { get; set; }


        public class JournalVoucherDetail
        {
            public virtual JournalVoucherDetail Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string Description { get; set; }
            public virtual string SubsiCode { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual string BizPartnerCode { get; set; }
            public virtual decimal Debit { get; set; }
            public virtual decimal Credit { get; set; }


            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    //a = Gears.RetriveData2("select * from Accounting.JournalVoucherDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);//KMM add Conn


                    a = Gears.RetriveData2("select A.*, B.Description from Accounting.JournalVoucherDetail AS A " +
                                            "INNER JOIN Accounting.ChartOfAccount AS B " +
                                            "ON A.AccountCode = B.AccountCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);//KMM add Conn
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddJournalVoucherDetail(JournalVoucherDetail JournalVoucherDetail)
            {
                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.JournalVoucherDetail where docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "AccountCode", JournalVoucherDetail.AccountCode);
                //DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Description", JournalVoucherDetail.Description);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "SubsiCode", JournalVoucherDetail.SubsiCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "ProfitCenterCode", JournalVoucherDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "CostCenterCode", JournalVoucherDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "BizPartnerCode", JournalVoucherDetail.BizPartnerCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Debit ", JournalVoucherDetail.Debit);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Credit", JournalVoucherDetail.Credit);


                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field1", JournalVoucherDetail.Field1);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field2", JournalVoucherDetail.Field2);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field3", JournalVoucherDetail.Field3);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field4", JournalVoucherDetail.Field4);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field5", JournalVoucherDetail.Field5);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field6", JournalVoucherDetail.Field6);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field7", JournalVoucherDetail.Field7);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field8", JournalVoucherDetail.Field8);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "0", "Field9", JournalVoucherDetail.Field9);

                DT2.Rows.Add("Accounting.JournalVoucher", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.JournalVoucher", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);



            }
            public void UpdateJournalVoucherDetail(JournalVoucherDetail JournalVoucherDetail)
            {



                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "cond", "DocNumber", JournalVoucherDetail.DocNumber);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "cond", "LineNumber", JournalVoucherDetail.LineNumber);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "AccountCode", JournalVoucherDetail.AccountCode);
                //DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Description", JournalVoucherDetail.Description);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "SubsiCode", JournalVoucherDetail.SubsiCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "ProfitCenterCode", JournalVoucherDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "CostCenterCode", JournalVoucherDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "BizPartnerCode", JournalVoucherDetail.BizPartnerCode);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Debit ", JournalVoucherDetail.Debit);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Credit", JournalVoucherDetail.Credit);


                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field1", JournalVoucherDetail.Field1);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field2", JournalVoucherDetail.Field2);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field3", JournalVoucherDetail.Field3);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field4", JournalVoucherDetail.Field4);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field5", JournalVoucherDetail.Field5);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field6", JournalVoucherDetail.Field6);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field7", JournalVoucherDetail.Field7);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field8", JournalVoucherDetail.Field8);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "set", "Field9", JournalVoucherDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteJournalVoucherDetail(JournalVoucherDetail JournalVoucherDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.JournalVoucherDetail", "cond", "DocNumber", JournalVoucherDetail.DocNumber);
                DT1.Rows.Add("Accounting.JournalVoucherDetail", "cond", "LineNumber", JournalVoucherDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Accounting.JournalVoucherDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.JournalVoucherDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.JournalVoucherDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }

        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.JournalVoucher where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                Memo = dtRow["Memo"].ToString();
                RefTemCode = dtRow["RefTemCode"].ToString();
                TemplateType = dtRow["TemplateType"].ToString();
                TotalDebit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalDebit"]) ? false : dtRow["TotalDebit"]);
                TotalCredit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCredit"]) ? false : dtRow["TotalCredit"]);
                

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

            }

            return a;
        }
        public void InsertData(JournalVoucher _ent)
        {

            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.JournalVoucher", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Memo", _ent.Memo);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "RefTemCode", _ent.RefTemCode);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "TemplateType", _ent.TemplateType);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "TotalDebit", _ent.TotalDebit);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "TotalCredit", _ent.TotalCredit);
            

            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.JournalVoucher", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }

        public void UpdateData(JournalVoucher _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.JournalVoucher", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Memo", _ent.Memo);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "RefTemCode", _ent.RefTemCode);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "TemplateType", _ent.TemplateType);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "TotalDebit", _ent.TotalDebit);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "TotalCredit", _ent.TotalCredit);

            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.JournalVoucher", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTJOV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        public void DeleteData(JournalVoucher _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.JournalVoucher", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("ACTJOV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }


        public class JournalEntry
        {
            private static string Conn { get; set; }
            public virtual string Connection { get; set; }
            public virtual JournalVoucher Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTJOV' ", Conn);

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