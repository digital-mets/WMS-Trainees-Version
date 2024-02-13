using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Amortization
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string Docnum;
        public virtual string RecordId { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string TranType { get; set; }
        public virtual string Description { get; set; }
        public virtual string DocDate { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual string AccountCode { get; set; }
        public virtual string SubsiCode { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string DateStart { get; set; }
        public virtual string DateEnd { get; set; }
        public virtual decimal MonthlyAmortization { get; set; }
        public virtual string NumPosting { get; set; }
        public virtual string ReversalGLCode { get; set; }
        public virtual string ReversalSubsiCode { get; set; }
        public virtual string ReversalProfitCode { get; set; }
        public virtual string ReversalCostCode { get; set; }
        public virtual string ReversalBizPartnerCode { get; set; }
        public virtual string ActualNumberOfPosting { get; set; }
        public virtual decimal PostingAmount { get; set; }
        public virtual string LastJVPosted { get; set; }
        public virtual string LastJVDate { get; set; }
        public virtual decimal ThisMonth { get; set; }
        public virtual string Status { get; set; }
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


        public DataTable getdata(string Id, string Conn)
        {
            DataTable a;

            //if (Id != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.Amortization where RecordId = '" + Id + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                RecordId = dtRow["RecordId"].ToString();
                DocNumber = dtRow["DocNumber"].ToString();
                TranType = dtRow["TranType"].ToString();
                Description = dtRow["Description"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? false : dtRow["TotalAmount"]);
                AccountCode = dtRow["AccountCode"].ToString();
                SubsiCode = dtRow["SubsiCode"].ToString();
                ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                CostCenterCode = dtRow["CostCenterCode"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                DateStart = dtRow["DateStart"].ToString();
                DateEnd = dtRow["DateEnd"].ToString();
                MonthlyAmortization = Convert.ToDecimal(Convert.IsDBNull(dtRow["MonthlyAmortization"]) ? false : dtRow["MonthlyAmortization"]);
                NumPosting = dtRow["NumPosting"].ToString();
                ReversalGLCode = dtRow["ReversalGLCode"].ToString();
                ReversalSubsiCode = dtRow["ReversalSubsiCode"].ToString();
                ReversalProfitCode = dtRow["ReversalProfitCode"].ToString();
                ReversalCostCode = dtRow["ReversalCostCode"].ToString();
                ReversalBizPartnerCode = dtRow["ReversalBizPartnerCode"].ToString();
                ActualNumberOfPosting = dtRow["ActualNumberOfPosting"].ToString();
                PostingAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PostingAmount"]) ? false : dtRow["PostingAmount"]);
                LastJVPosted = dtRow["LastJVPosted"].ToString();
                //LastJVDate = String.IsNullOrEmpty("LastJVDate") ? null : Convert.ToDateTime("LastJVDate").ToShortDateString();
                if (!String.IsNullOrEmpty(dtRow["LastJVPosted"].ToString()))
                    LastJVPosted = dtRow["LastJVPosted"].ToString();
                LastJVPosted = dtRow["LastJVPosted"].ToString();
                //if (!String.IsNullOrEmpty(dtRow["LastJVDate"].ToString()))
                //    LastJVDate = dtRow["LastJVDate"].ToString();
                ThisMonth = Convert.ToDecimal(Convert.IsDBNull(dtRow["ThisMonth"]) ? false : dtRow["ThisMonth"]);
                Status = dtRow["Status"].ToString();
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



                //LastEditedBy = dtRow["LastEditedBy"].ToString();
                //LastEditedDate = dtRow["LastEditedDate"].ToString();

            }

            return a;
        }
        public void InsertData(Amortization _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.Amortization", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.Amortization", "0", "TranType", _ent.TranType);
            DT1.Rows.Add("Accounting.Amortization", "0", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.Amortization", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.Amortization", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Accounting.Amortization", "0", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "SubsiCode", _ent.SubsiCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "DateStart", _ent.DateStart);
            DT1.Rows.Add("Accounting.Amortization", "0", "DateEnd", _ent.DateEnd);
            DT1.Rows.Add("Accounting.Amortization", "0", "MonthlyAmortization", _ent.MonthlyAmortization);
            DT1.Rows.Add("Accounting.Amortization", "0", "NumPosting", _ent.NumPosting);
            DT1.Rows.Add("Accounting.Amortization", "0", "ReversalGLCode", _ent.ReversalGLCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "ReversalSubsiCode", _ent.ReversalSubsiCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "ReversalProfitCode", _ent.ReversalProfitCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "ReversalCostCode", _ent.ReversalCostCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "ReversalBizPartnerCode", _ent.ReversalBizPartnerCode);
            DT1.Rows.Add("Accounting.Amortization", "0", "ActualNumberOfPosting", _ent.ActualNumberOfPosting);
            DT1.Rows.Add("Accounting.Amortization", "0", "PostingAmount", _ent.PostingAmount);
            //DT1.Rows.Add("Accounting.Amortization", "0", "LastJVPosted", _ent.LastJVPosted);
            //DT1.Rows.Add("Accounting.Amortization", "0", "LastJVDate", _ent.LastJVDate);
            DT1.Rows.Add("Accounting.Amortization", "0", "ThisMonth", _ent.ThisMonth);
            DT1.Rows.Add("Accounting.Amortization", "0", "Status", _ent.Status);
            DT1.Rows.Add("Accounting.Amortization", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.Amortization", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.Amortization", "0", "Field9", _ent.Field9);


            DT1.Rows.Add("Accounting.Amortization", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.Amortization", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(Amortization _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.Amortization", "cond", "RecordId", _ent.RecordId);
            DT1.Rows.Add("Accounting.Amortization", "set", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.Amortization", "set", "TranType", _ent.TranType);
            DT1.Rows.Add("Accounting.Amortization", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.Amortization", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.Amortization", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Accounting.Amortization", "set", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "SubsiCode", _ent.SubsiCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "DateStart", _ent.DateStart);
            DT1.Rows.Add("Accounting.Amortization", "set", "DateEnd", _ent.DateEnd);
            DT1.Rows.Add("Accounting.Amortization", "set", "MonthlyAmortization", _ent.MonthlyAmortization);
            DT1.Rows.Add("Accounting.Amortization", "set", "NumPosting", _ent.NumPosting);
            DT1.Rows.Add("Accounting.Amortization", "set", "ReversalGLCode", _ent.ReversalGLCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "ReversalSubsiCode", _ent.ReversalSubsiCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "ReversalProfitCode", _ent.ReversalProfitCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "ReversalCostCode", _ent.ReversalCostCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "ReversalBizPartnerCode", _ent.ReversalBizPartnerCode);
            DT1.Rows.Add("Accounting.Amortization", "set", "ActualNumberOfPosting", _ent.ActualNumberOfPosting);
            DT1.Rows.Add("Accounting.Amortization", "set", "PostingAmount", _ent.PostingAmount);
           // DT1.Rows.Add("Accounting.Amortization", "set", "LastJVPosted", _ent.LastJVPosted);
            //DT1.Rows.Add("Accounting.Amortization", "set", "LastJVDate", _ent.LastJVDate);
            DT1.Rows.Add("Accounting.Amortization", "set", "ThisMonth", _ent.ThisMonth);
            DT1.Rows.Add("Accounting.Amortization", "set", "Status", _ent.Status);
            DT1.Rows.Add("Accounting.Amortization", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.Amortization", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.Amortization", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.Amortization", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.Amortization", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTAMZ", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Amortization _ent)
        {
           // Docnum = _ent.DocNumber;
            RecordId = _ent.RecordId;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.Amortization", "cond", "RecordId", _ent.RecordId);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTAMZ", RecordId, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void UpdateMonthlyAmortization(string DocNumber, string Conn)
        {
            DataTable updatedata = new DataTable();

            updatedata = Gears.RetriveData2("UPDATE Accounting.Amortization "
                + " SET MonthlyAmortization = CONVERT(decimal(15,2),ISNULL(TotalAmount,0) / ISNULL(NumPosting,1)) "
                + " WHERE RecordID = '" + DocNumber + "'", Conn);
        }
    }
}
