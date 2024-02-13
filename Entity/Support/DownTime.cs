using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DownTime
    {
        public string _TableName = "Support.DownTime";
        
        public virtual Int64 RecordID { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual String StartDown { get; set; }
        public virtual String EndDown { get; set; }
        public virtual int DownTimeInMin { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }

        public DataTable GetRecords(string _Date)
        {

            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM " + _TableName + " WHERE Date='" + _Date + "'");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }
  
        public string InsertData(DownTime _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "Date", _Record.Date.ToString("yyyy-MM-dd"));
            dtObject.Rows.Add(_TableName, "0", "CustomerCode", _Record.CustomerCode);
            dtObject.Rows.Add(_TableName, "0", "StartDown", _Record.StartDown);
            dtObject.Rows.Add(_TableName, "0", "EndDown", _Record.EndDown);
            dtObject.Rows.Add(_TableName, "0", "DownTimeInMin", _Record.DownTimeInMin);

            dtObject.Rows.Add(_TableName, "0", "AddedBy", _Record.AddedBy);
            dtObject.Rows.Add(_TableName, "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.CreateData(dtObject);
            return strErr;
        }

        public string UpdateData(DownTime _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "CustomerCode", _Record.CustomerCode);
            dtObject.Rows.Add(_TableName, "set", "StartDown", _Record.StartDown);
            dtObject.Rows.Add(_TableName, "set", "EndDown", _Record.EndDown);
            dtObject.Rows.Add(_TableName, "set", "DownTimeInMin", _Record.DownTimeInMin);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", _Record.LastEditedBy);
            _Record.LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Down Time", _Record.Date.ToString("yyyy-MM-dd") + ";" + _Record.CustomerCode,
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }

        public string DeleteData(DownTime _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Down Time", _Record.Date.ToString("yyyy-MM-dd") + ";" + _Record.CustomerCode,
                                     _Record.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }

        public string DeleteRecords(string _Date, string _UserID)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();
            dtObject.Rows.Add(_TableName, "cond", "Date", _Date);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Down Time", _Date,
                                     _UserID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }
}
