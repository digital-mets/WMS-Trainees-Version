using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class MasterManPower
    {
        public string _TableName = "Enterprise.ManPower";
        
        public virtual Int64 RecordID { get; set; }
        public virtual int Year { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual int ExeCom { get; set; }
        public virtual int ManCom { get; set; }
        public virtual int RegularStaff { get; set; }
        public virtual int TemporaryStaff { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }

        public DataTable GetRecords(string _Year)
        {

            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM " + _TableName + " WHERE Year=" + _Year);
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }
  
        public string InsertData(MasterManPower _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "Year", _Record.Year);
            dtObject.Rows.Add(_TableName, "0", "CompanyCode", _Record.CompanyCode);
            dtObject.Rows.Add(_TableName, "0", "Execom", _Record.ExeCom);
            dtObject.Rows.Add(_TableName, "0", "Mancom", _Record.ManCom);
            dtObject.Rows.Add(_TableName, "0", "RegularStaff", _Record.RegularStaff);
            dtObject.Rows.Add(_TableName, "0", "TemporaryStaff", _Record.TemporaryStaff);

            dtObject.Rows.Add(_TableName, "0", "AddedBy", _Record.AddedBy);
            dtObject.Rows.Add(_TableName, "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.CreateData(dtObject);
            return strErr;
        }

        public string UpdateData(MasterManPower _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "CompanyCode", _Record.CompanyCode);
            dtObject.Rows.Add(_TableName, "set", "Execom", _Record.ExeCom);
            dtObject.Rows.Add(_TableName, "set", "Mancom", _Record.ManCom);
            dtObject.Rows.Add(_TableName, "set", "RegularStaff", _Record.RegularStaff);
            dtObject.Rows.Add(_TableName, "set", "TemporaryStaff", _Record.TemporaryStaff);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", _Record.LastEditedBy);
            _Record.LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Master Manpower", _Record.Year.ToString() + ";" + _Record.CompanyCode,
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }

        public string DeleteData(MasterManPower _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Master Manpower", _Record.Year.ToString() + ";" + _Record.CompanyCode,
                                     _Record.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }

        public string DeleteRecords(string _Year, string _UserID)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();
            dtObject.Rows.Add(_TableName, "cond", "Year", _Year);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Master Manpower", CompanyCode + ";" + Year.ToString(),
                                     _UserID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }

    }
}
