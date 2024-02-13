using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PrintingOfCheckIssued
    {
        public virtual string CheckNumber { get; set; }
        public virtual string CheckVoucher { get; set; }
        public virtual string CheckDate { get; set; }
        public virtual decimal CheckAmount { get; set; }
        public virtual string CheckStatus { get; set; }
        public virtual string DateCreated { get; set; }
        public virtual string DatePrinted { get; set; }
        public virtual string DateReleased { get; set; }
        public virtual string DateCleared { get; set; }

        public DataTable getdata(string POCI)
        {
            DataTable a;

            if (POCI != null)
            {
                a = Gears.RetriveData2("select * from Accounting.PrintingOfCheckIssued where CheckNumber = '" + POCI + "'");
                foreach (DataRow dtRow in a.Rows)
                {
                    CheckNumber = dtRow["CheckNumber"].ToString();
                    CheckVoucher = dtRow["CheckVoucher"].ToString();
                    CheckDate = dtRow["CheckDate"].ToString();
                    CheckAmount = Convert.ToDecimal(dtRow["CheckAmount"].ToString());
                    CheckStatus = dtRow["CheckStatus"].ToString();
                    DateCreated = dtRow["DateCreated"].ToString();
                    DatePrinted = dtRow["DatePrinted"].ToString();
                    DateReleased = dtRow["DateReleased"].ToString();
                    DateCleared = dtRow["DateCleared"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days");
            }
            return a;
        }
        public void InsertData(PrintingOfCheckIssued _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "CheckNumber", _ent.CheckNumber);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "CheckVoucher", _ent.CheckVoucher);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "CheckDate", _ent.CheckDate);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "CheckAmount", _ent.CheckAmount);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "CheckStatus", _ent.CheckStatus);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "DateCreated", _ent.DateCreated);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "DatePrinted", _ent.DatePrinted);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "DateReleased", _ent.DateReleased);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "0", "DateCleared", _ent.DateCleared);

            Gears.CreateData(DT1);
            //Functions.AuditTrail("REFATC", _ent.CheckNumber, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }

        public void UpdateData(PrintingOfCheckIssued _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "cond", "CheckNumber", _ent.CheckNumber);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "CheckVoucher", _ent.CheckVoucher);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "CheckDate", _ent.CheckDate);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "CheckAmount", _ent.CheckAmount);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "CheckStatus", _ent.CheckStatus);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "DateCreated", _ent.DateCreated);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "DatePrinted", _ent.DatePrinted);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "DateReleased", _ent.DateReleased);
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "set", "DateCleared", _ent.DateCleared);

            string strErr = Gears.UpdateData(DT1);
            //Functions.AuditTrail("REFATC", _ent.ATCCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(PrintingOfCheckIssued _ent)
        {
            CheckNumber = _ent.CheckNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.PrintingOfCheckIssued", "cond", "CheckNumber", _ent.CheckNumber);
            Gears.DeleteData(DT1);
            //Functions.AuditTrail("REFATC", _ent.ATCCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
