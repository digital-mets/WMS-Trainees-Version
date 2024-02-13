using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Loan
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string LoanType { get; set; }
        public virtual string LoanCategory { get; set; }
        public virtual string Status { get; set; }
        public virtual string LoanReference { get; set; }
        public virtual string LoanClass { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string Name { get; set; }
        public virtual Nullable<DateTime> MaturityDate { get; set; }
        public virtual string ReferenceLN { get; set; }

        public virtual decimal LoanAmount { get; set; }
        public virtual string InterestRate { get; set; }
        public virtual string TermType { get; set; }
        public virtual int Terms { get; set; }
        public virtual Nullable<int> IntFrequency { get; set; }
        public virtual Nullable<int> PrinFrequency { get; set; }

        public virtual bool IsGenerated { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }


        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string ForceClosedBy { get; set; }
        public virtual string ForceClosedDate { get; set; }
        
        //public virtual IList<PurchaseRequestDetail> Detail { get; set; }

        public class LoanDetail
        {
            public virtual Loan Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual int Period { get; set; }
            public virtual decimal BegBalofPrincipal { get; set; }
            public virtual DateTime PeriodFrom { get; set; }
            public virtual DateTime PeriodTo { get; set; }
            public virtual int NumOfDays { get; set; }
            public virtual decimal PaymentAmount { get; set; }
            public virtual decimal Interest { get; set; }
            public virtual decimal Principal { get; set; }
            public virtual decimal Penalty { get; set; }
            public virtual decimal EndBalofPrincipal { get; set; }
            public virtual string CVNumber { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT *, DATEDIFF(d,PeriodFrom,PeriodTo) AS NumOfDays FROM Accounting.LoanDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddLoanDetail(LoanDetail LoanDetail)
            {
                int maxlinecount = 0;

                DataTable maxline = Gears.RetriveData2("SELECT MIN(CONVERT(INT,LineNumber)) AS LineNumber from Accounting.LoanDetail WHERE DocNumber = '" + Docnum + "'", Conn);
               
                try
                {
                    maxlinecount = Convert.ToInt32(maxline.Rows[0][0].ToString()) - 1;
                }
                catch
                {
                    maxlinecount = Convert.ToInt32(LoanDetail.Period);
                }
                string strLine = maxlinecount.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.LoanDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("Accounting.LoanDetail", "0", "Period", LoanDetail.Period);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "BegBalofPrincipal", LoanDetail.BegBalofPrincipal);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "PeriodFrom", LoanDetail.PeriodFrom);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "PeriodTo", LoanDetail.PeriodTo);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "PaymentAmount", LoanDetail.PaymentAmount);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "Interest", LoanDetail.Interest);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "Principal", LoanDetail.Principal);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "Penalty", LoanDetail.Penalty);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "EndBalofPrincipal", LoanDetail.EndBalofPrincipal);
                DT1.Rows.Add("Accounting.LoanDetail", "0", "CVNumber", LoanDetail.CVNumber);

                DT2.Rows.Add("Accounting.Loan", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.Loan", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateLoanDetail(LoanDetail LoanDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.LoanDetail", "cond", "DocNumber", LoanDetail.DocNumber);
                DT1.Rows.Add("Accounting.LoanDetail", "cond", "LineNumber", LoanDetail.LineNumber);

                DT1.Rows.Add("Accounting.LoanDetail", "set", "Period", LoanDetail.Period);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "BegBalofPrincipal", LoanDetail.BegBalofPrincipal);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "PeriodFrom", LoanDetail.PeriodFrom);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "PeriodTo", LoanDetail.PeriodTo);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "PaymentAmount", LoanDetail.PaymentAmount);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "Interest", LoanDetail.Interest);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "Principal", LoanDetail.Principal);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "Penalty", LoanDetail.Penalty);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "EndBalofPrincipal", LoanDetail.EndBalofPrincipal);
                DT1.Rows.Add("Accounting.LoanDetail", "set", "CVNumber", LoanDetail.CVNumber);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteLoanDetail(LoanDetail LoanDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.LoanDetail", "cond", "DocNumber", LoanDetail.DocNumber);
                DT1.Rows.Add("Accounting.LoanDetail", "cond", "LineNumber", LoanDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.LoanDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.Loan", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.Loan", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select DocNumber, DocDate, LoanType, LoanCategory"
                                        + " , Status, LoanReference, LoanClass, BizPartnerCode, Name"
                                        + " , LoanAmount, InterestRate, TermType, Terms"
                                        + " , MaturityDate, ReferenceLN, IntFrequency, PrinFrequency"
                                        + " , Field1, Field2, Field3, Field4, Field5"
	                                    + " , Field6, Field7, Field8, Field9"
	                                    + " , AddedBy, AddedDate"
                                        + " , LastEditedBy, LastEditedDate"
                                        + " , SubmittedBy, SubmittedDate"
                                        + " , CancelledBy, CancelledDate"
                                        + " FROM Accounting.Loan where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocDate = dtRow["DocDate"].ToString();
                LoanType = dtRow["LoanType"].ToString();
                LoanCategory = dtRow["LoanCategory"].ToString();
                Status = dtRow["Status"].ToString();
                LoanReference = dtRow["LoanReference"].ToString();
                LoanClass = dtRow["LoanClass"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                Name = dtRow["Name"].ToString();
                LoanAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["LoanAmount"]) ? 0 : dtRow["LoanAmount"]);
                //InterestRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["InterestRate"]) ? 0 : dtRow["InterestRate"]);
                InterestRate = dtRow["InterestRate"].ToString();
                TermType = dtRow["TermType"].ToString();
                Terms = Convert.ToInt32(Convert.IsDBNull(dtRow["Terms"]) ? 0 : dtRow["Terms"]);
                if (dtRow["MaturityDate"] == DBNull.Value) { MaturityDate = null; }
                    else { MaturityDate = Convert.ToDateTime(dtRow["MaturityDate"]); }
                ReferenceLN = dtRow["ReferenceLN"].ToString();
                if (dtRow["IntFrequency"] == DBNull.Value) { IntFrequency = null; }
                    else { IntFrequency = Convert.ToInt32(dtRow["IntFrequency"]); }
                if (dtRow["PrinFrequency"] == DBNull.Value) { PrinFrequency = null; }
                    else { PrinFrequency = Convert.ToInt32(dtRow["PrinFrequency"]); }

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();

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

            return a;
        }

        public void UpdateData(Loan _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.Loan", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.Loan", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.Loan", "set", "LoanType", _ent.LoanType);
            DT1.Rows.Add("Accounting.Loan", "set", "LoanCategory", _ent.LoanCategory);
            DT1.Rows.Add("Accounting.Loan", "set", "Status", _ent.Status);
            DT1.Rows.Add("Accounting.Loan", "set", "LoanReference", _ent.LoanReference);
            DT1.Rows.Add("Accounting.Loan", "set", "LoanClass", _ent.LoanClass);
            DT1.Rows.Add("Accounting.Loan", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("Accounting.Loan", "set", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.Loan", "set", "LoanAmount", _ent.LoanAmount);
            DT1.Rows.Add("Accounting.Loan", "set", "InterestRate", _ent.InterestRate);
            DT1.Rows.Add("Accounting.Loan", "set", "TermType", _ent.TermType);
            DT1.Rows.Add("Accounting.Loan", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Accounting.Loan", "set", "MaturityDate", _ent.MaturityDate);
            DT1.Rows.Add("Accounting.Loan", "set", "ReferenceLN", _ent.ReferenceLN);
            DT1.Rows.Add("Accounting.Loan", "set", "IntFrequency", _ent.IntFrequency);
            DT1.Rows.Add("Accounting.Loan", "set", "PrinFrequency", _ent.PrinFrequency);

            DT1.Rows.Add("Accounting.Loan", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.Loan", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.Loan", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.Loan", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.Loan", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.Loan", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.Loan", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.Loan", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.Loan", "set", "Field9", _ent.Field9);

            DataTable dtTemp = Gears.RetriveData2(
                "SELECT ISNULL(MaturityDate,'') AS MaturityDate, CASE WHEN ISNULL(InterestRate,'') = '' THEN '0' ELSE InterestRate END AS InterestRate, ISNULL(IntFrequency,0) AS IntFrequency, ISNULL(PrinFrequency,0) AS PrinFrequency" +
                "  FROM Accounting.Loan WHERE DocNumber = '" + DocNumber + "'", Conn);

            if (dtTemp.Rows.Count > 0 &&
                (dtTemp.Rows[0]["InterestRate"] == DBNull.Value ||
                 Convert.ToDecimal(dtTemp.Rows[0]["InterestRate"]) != Convert.ToDecimal(String.IsNullOrEmpty(_ent.InterestRate) ? "0" : _ent.InterestRate)
                 || dtTemp.Rows[0]["MaturityDate"] == DBNull.Value ||
                 Convert.ToDateTime(dtTemp.Rows[0]["MaturityDate"]) != _ent.MaturityDate
                 || dtTemp.Rows[0]["IntFrequency"] == DBNull.Value ||
                 Convert.ToInt32(dtTemp.Rows[0]["IntFrequency"]) != _ent.IntFrequency
                 || dtTemp.Rows[0]["PrinFrequency"] == DBNull.Value ||
                 Convert.ToInt32(dtTemp.Rows[0]["PrinFrequency"]) != _ent.PrinFrequency
                 )
                )
            {
                DT1.Rows.Add("Accounting.Loan", "set", "IsGenerated", null);
            }

            dtTemp.Dispose();

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("RTLOAN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Loan _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.Loan", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Functions.AuditTrail("RTLOAN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", Conn);
        }
    }
}
