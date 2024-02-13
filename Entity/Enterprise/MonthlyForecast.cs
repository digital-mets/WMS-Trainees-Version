using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class MonthlyForecast
    {
        public string _TableName = "Enterprise.MonthlyForecast";
        
        public virtual Int64 RecordID { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual int Year { get; set; }
        public virtual int Month { get; set; }
        public virtual decimal Forecast { get; set; }
        public virtual decimal TargetMarkup { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }

        public DataTable GetRecords(string _Year, string _CompanyCode)
        {

            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM " + _TableName +
                                              " WHERE Year=" + _Year + " AND CompanyCode='" + _CompanyCode + "'");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }
  
        public string InsertData(MonthlyForecast _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "Year", _Record.Year);
            dtObject.Rows.Add(_TableName, "0", "CompanyCode", _Record.CompanyCode);
            dtObject.Rows.Add(_TableName, "0", "Month", _Record.Month);
            dtObject.Rows.Add(_TableName, "0", "Forecast", _Record.Forecast);
            dtObject.Rows.Add(_TableName, "0", "TargetMarkup", _Record.TargetMarkup);

            dtObject.Rows.Add(_TableName, "0", "AddedBy", _Record.AddedBy);
            dtObject.Rows.Add(_TableName, "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return Gears.CreateData(dtObject);
        }

        public string UpdateData(MonthlyForecast _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "Month", _Record.Month);
            dtObject.Rows.Add(_TableName, "set", "Forecast", _Record.Forecast);
            dtObject.Rows.Add(_TableName, "set", "TargetMarkup", _Record.TargetMarkup);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", _Record.LastEditedBy);
            _Record.LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Monthly Forecast", _Record.CompanyCode + ";" + _Record.Year.ToString() + ";" + _Record.Month.ToString(),
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }

        public string DeleteData(MonthlyForecast _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Monthly Forecast", _Record.CompanyCode + ";" + _Record.Year.ToString() + ";" + _Record.Month.ToString(),
                                     _Record.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }

        public string DeleteRecords(string _Year, string _CompanyCode, string _UserID)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();
            dtObject.Rows.Add(_TableName, "cond", "Year", _Year);
            dtObject.Rows.Add(_TableName, "cond", "CompanyCode", _CompanyCode);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("MonthlyForecast", CompanyCode + ";" + Year.ToString(),
                                     _UserID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }

        public DataTable NewSheet(String _Dummy)
        {
            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("RecordID");
            dtResult.Columns.Add("CompanyCode");
            dtResult.Columns.Add("Year");
            dtResult.Columns.Add("Month");
            dtResult.Columns.Add("Forecast");
            dtResult.Columns.Add("TargetMarkup");
            dtResult.Columns.Add("AddedBy");
            dtResult.Columns.Add("AddedDate");
            dtResult.Columns.Add("LastEditedBy");
            dtResult.Columns.Add("LastEditedDate");

            dtResult.Rows.Add(1, "", 0, 1, 0, 0, "", null, null, null);
            dtResult.Rows.Add(2, "", 0, 2, 0, 0, "", null, null, null);
            dtResult.Rows.Add(3, "", 0, 3, 0, 0, "", null, null, null);
            dtResult.Rows.Add(4, "", 0, 4, 0, 0, "", null, null, null);
            dtResult.Rows.Add(5, "", 0, 5, 0, 0, "", null, null, null);
            dtResult.Rows.Add(6, "", 0, 6, 0, 0, "", null, null, null);
            dtResult.Rows.Add(7, "", 0, 7, 0, 0, "", null, null, null);
            dtResult.Rows.Add(8, "", 0, 8, 0, 0, "", null, null, null);
            dtResult.Rows.Add(9, "", 0, 9, 0, 0, "", null, null, null);
            dtResult.Rows.Add(10, "", 0, 10, 0, 0, "", null, null, null);
            dtResult.Rows.Add(11, "", 0, 11, 0, 0, "", null, null, null);
            dtResult.Rows.Add(12, "", 0, 12, 0, 0, "", null, null, null);

            return dtResult;
        }
    }
    public class Months
    {
        public virtual string MonthName { get; set; }
        public virtual int MonthValue { get; set; }

        public List<Months> GetMonths()
        {
            List<Months> MonthList = new List<Months>();

            MonthList.Add(new Months { MonthName = "JANUARY", MonthValue = 1 });
            MonthList.Add(new Months { MonthName = "FEBRUARY", MonthValue = 2 });
            MonthList.Add(new Months { MonthName = "MARCH", MonthValue = 3 });
            MonthList.Add(new Months { MonthName = "APRIL", MonthValue = 4 });
            MonthList.Add(new Months { MonthName = "MAY", MonthValue = 5 });
            MonthList.Add(new Months { MonthName = "JUNE", MonthValue = 6 });
            MonthList.Add(new Months { MonthName = "JULY", MonthValue = 7 });
            MonthList.Add(new Months { MonthName = "AUGUST", MonthValue = 8 });
            MonthList.Add(new Months { MonthName = "SEPTEMBER", MonthValue = 9 });
            MonthList.Add(new Months { MonthName = "OCTOBER", MonthValue = 10 });
            MonthList.Add(new Months { MonthName = "NOVEMBER", MonthValue = 11 });
            MonthList.Add(new Months { MonthName = "DECEMBER", MonthValue = 12 });

            return MonthList;
        }
    }
}
