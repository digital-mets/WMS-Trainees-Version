using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class RecurringJV
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DateStart { get; set; }
        public virtual string Cycle { get; set; }
        public virtual string Interval { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsInactive { get; set; }
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
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual IList<RecurringJVDetail> Detail { get; set; }

        public class RecurringJVDetail
        {
            public virtual RecurringJVDetail Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
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
                    a = Gears.RetriveData2("SELECT * FROM Accounting.RecurringJVDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);//KMM add Conn
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRecurringJVDetail(RecurringJVDetail RecurringJVDetail)
            {
                int linenum = 0;


                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Accounting.RecurringJVDetail WHERE docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "AccountCode", RecurringJVDetail.AccountCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "SubsiCode", RecurringJVDetail.SubsiCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "ProfitCenterCode", RecurringJVDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "CostCenterCode", RecurringJVDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "BizPartnerCode", RecurringJVDetail.BizPartnerCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Debit ", RecurringJVDetail.Debit);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Credit", RecurringJVDetail.Credit);


                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field1", RecurringJVDetail.Field1);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field2", RecurringJVDetail.Field2);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field3", RecurringJVDetail.Field3);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field4", RecurringJVDetail.Field4);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field5", RecurringJVDetail.Field5);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field6", RecurringJVDetail.Field6);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field7", RecurringJVDetail.Field7);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field8", RecurringJVDetail.Field8);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "0", "Field9", RecurringJVDetail.Field9);

                DT2.Rows.Add("Accounting.RecurringJV", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.RecurringJV", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);



            }
            public void UpdateRecurringJVDetail(RecurringJVDetail RecurringJVDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.RecurringJVDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "cond", "LineNumber", RecurringJVDetail.LineNumber);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "AccountCode", RecurringJVDetail.AccountCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "SubsiCode", RecurringJVDetail.SubsiCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "ProfitCenterCode", RecurringJVDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "CostCenterCode", RecurringJVDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "BizPartnerCode", RecurringJVDetail.BizPartnerCode);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Debit ", RecurringJVDetail.Debit);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Credit", RecurringJVDetail.Credit);

                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field1", RecurringJVDetail.Field1);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field2", RecurringJVDetail.Field2);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field3", RecurringJVDetail.Field3);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field4", RecurringJVDetail.Field4);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field5", RecurringJVDetail.Field5);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field6", RecurringJVDetail.Field6);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field7", RecurringJVDetail.Field7);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field8", RecurringJVDetail.Field8);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "set", "Field9", RecurringJVDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteRecurringJVDetail(RecurringJVDetail RecurringJVDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.RecurringJVDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RecurringJVDetail", "cond", "LineNumber", RecurringJVDetail.LineNumber);

                Gears.DeleteData(DT1, Conn);//KMM add Conn

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.RecurringJVDetail WHERE docnumber = '" + Docnum + "'", Conn);//KMM add Conn

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.RecurringJVDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.RecurringJVDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//KMM add Conn
                }
            }
        }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Accounting.RecurringJV WHERE DocNumber = '" + DocNumber + "'", Conn); //KMM add Conn
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DateStart = dtRow["DateStart"].ToString();
                Cycle = dtRow["Cycle"].ToString();
                Interval = dtRow["Interval"].ToString();
                Description = dtRow["Description"].ToString();
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                TotalDebit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalDebit"]) ? 0 : dtRow["TotalDebit"]);
                TotalCredit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCredit"]) ? 0 : dtRow["TotalCredit"]);

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
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                DeactivatedDate = dtRow["DeactivatedDate"].ToString();

            }
            return a;
        }
        public void InsertData(RecurringJV _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.RecurringJV", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "DateStart", _ent.DateStart);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Cycle", _ent.Cycle);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Interval", _ent.Interval);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "TotalDebit", _ent.TotalDebit);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "TotalCredit", _ent.TotalCredit);

            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.RecurringJV", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.RecurringJV", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Accounting.RecurringJV", "0", "DocDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }

        public void UpdateData(RecurringJV _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.RecurringJV", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "DateStart", _ent.DateStart);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Cycle", _ent.Cycle);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Interval", _ent.Interval);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "TotalDebit", _ent.TotalDebit);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "TotalCredit", _ent.TotalCredit);

            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.RecurringJV", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTRJS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        public void DeleteData(RecurringJV _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.RecurringJV", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("ACTRJS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }
    }
}
