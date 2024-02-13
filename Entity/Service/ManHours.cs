using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ManHours
    {
        public string _TableName = "Service.ManHours";
        
        public virtual Int64 RecordID { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string UserID { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal Duration { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }

        public DataTable GetRecords(string _Date, string _UserID)
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT RecordID, [Date], UserID, [Type]+':'+Reference AS Reference, "+
                                              "Remarks, Duration, AddedBy, AddedDate, LastEditedBy, LastEditedDate " +
                                              "FROM " + _TableName + " WHERE Date='" + _Date + "' AND UserID='" + _UserID + "'");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }
  
        public string InsertData(ManHours _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "Date", _Record.Date.ToString("yyyy-MM-dd"));
            dtObject.Rows.Add(_TableName, "0", "UserID", _Record.UserID);
            dtObject.Rows.Add(_TableName, "0", "Type", _Record.Reference.Substring(0,3));
            dtObject.Rows.Add(_TableName, "0", "Reference", _Record.Reference.Substring(4));
            dtObject.Rows.Add(_TableName, "0", "Remarks", _Record.Remarks);
            dtObject.Rows.Add(_TableName, "0", "Duration", _Record.Duration);

            dtObject.Rows.Add(_TableName, "0", "AddedBy", _Record.AddedBy);
            dtObject.Rows.Add(_TableName, "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.CreateData(dtObject);
            return strErr;
        }

        public string UpdateData(ManHours _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "Type", _Record.Reference.Substring(0,3));
            dtObject.Rows.Add(_TableName, "set", "Reference", _Record.Reference.Substring(4));
            dtObject.Rows.Add(_TableName, "set", "Remarks", _Record.Remarks);
            dtObject.Rows.Add(_TableName, "set", "Duration", _Record.Duration);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", _Record.LastEditedBy);
            _Record.LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Man-Hours", _Record.UserID + ";" + _Record.Date.ToString("yyyy-MM-dd") + ";" + _Record.RecordID.ToString(),
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }

        public string DeleteData(ManHours _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Man-Hours", _Record.UserID + ";" + _Record.Date.ToString("yyyy-MM-dd") 
                                     + ";" + _Record.Reference + ";" + _Record.Duration.ToString(),
                                     _Record.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }

        public string DeleteRecords(string _Date, string _UserID)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();
            dtObject.Rows.Add(_TableName, "cond", "Date", _Date);
            dtObject.Rows.Add(_TableName, "cond", "UserID", _UserID);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Man-Hours", _UserID + ";" + _Date,
                                     _UserID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }
}
