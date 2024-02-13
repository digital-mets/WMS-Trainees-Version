using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ISProjection
    {
        public string _TableName = "ABP.ISProjection";

        public virtual int Year { get; set; }
        public virtual String Version { get; set; }
        public virtual String RefCode { get; set; }
        public virtual String AccountCode { get; set; }
        public virtual String Description { get; set; }
        public virtual Decimal Jan { get; set; }
        public virtual Decimal Feb { get; set; }
        public virtual Decimal Mar { get; set; }
        public virtual Decimal Apr { get; set; }
        public virtual Decimal May { get; set; }
        public virtual Decimal Jun { get; set; }
        public virtual Decimal Jul { get; set; }
        public virtual Decimal Aug { get; set; }
        public virtual Decimal Sep { get; set; }
        public virtual Decimal Oct { get; set; }
        public virtual Decimal Nov { get; set; }
        public virtual Decimal Dec { get; set; }

        public DataTable GetRecords(int _Year, String _Version)
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM " + _TableName 
                                            + " WHERE Year=" + _Year.ToString() +" Version='"+_Version+"'"
                                            + " ORDER BY RecordID");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }
  
        public string InsertData(ISProjection _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "Year", _Record.Year);
            dtObject.Rows.Add(_TableName, "0", "Version", _Record.Version);
            dtObject.Rows.Add(_TableName, "0", "RefCode", _Record.RefCode);
            dtObject.Rows.Add(_TableName, "0", "AccountCode", _Record.AccountCode);
            dtObject.Rows.Add(_TableName, "0", "Description", Description);
            dtObject.Rows.Add(_TableName, "0", "Jan", _Record.Jan);
            dtObject.Rows.Add(_TableName, "0", "Feb", _Record.Feb);
            dtObject.Rows.Add(_TableName, "0", "Mar", _Record.Mar);
            dtObject.Rows.Add(_TableName, "0", "Apr", _Record.Apr);
            dtObject.Rows.Add(_TableName, "0", "May", _Record.May);
            dtObject.Rows.Add(_TableName, "0", "Jun", _Record.Jun);
            dtObject.Rows.Add(_TableName, "0", "Jul", _Record.Jul);
            dtObject.Rows.Add(_TableName, "0", "Aug", _Record.Aug);
            dtObject.Rows.Add(_TableName, "0", "Sep", _Record.Sep);
            dtObject.Rows.Add(_TableName, "0", "Oct", _Record.Oct);
            dtObject.Rows.Add(_TableName, "0", "Nov", _Record.Nov);
            dtObject.Rows.Add(_TableName, "0", "Dec", _Record.Dec);

            string strErr = Gears.CreateData(dtObject);

            return strErr;
        }

        public string UpdateData(ISProjection _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "Year", _Record.Year);
            dtObject.Rows.Add(_TableName, "cond", "Version", _Record.Version);
            dtObject.Rows.Add(_TableName, "cond", "RefCode", _Record.Version);
            dtObject.Rows.Add(_TableName, "cond", "AccountCode", _Record.Version);

            dtObject.Rows.Add(_TableName, "set", "Jan", _Record.Jan);
            dtObject.Rows.Add(_TableName, "set", "Feb", _Record.Feb);
            dtObject.Rows.Add(_TableName, "set", "Mar", _Record.Mar);
            dtObject.Rows.Add(_TableName, "set", "Apr", _Record.Apr);
            dtObject.Rows.Add(_TableName, "set", "May", _Record.May);
            dtObject.Rows.Add(_TableName, "set", "Jun", _Record.Jun);
            dtObject.Rows.Add(_TableName, "set", "Jul", _Record.Jul);
            dtObject.Rows.Add(_TableName, "set", "Aug", _Record.Aug);
            dtObject.Rows.Add(_TableName, "set", "Sep", _Record.Sep);
            dtObject.Rows.Add(_TableName, "set", "Oct", _Record.Oct);
            dtObject.Rows.Add(_TableName, "set", "Nov", _Record.Nov);
            dtObject.Rows.Add(_TableName, "set", "Dec", _Record.Dec);

            string strErr = Gears.UpdateData(dtObject);

            return strErr;
        }

        public string DeleteData(ISProjection _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "Year", _Record.Year);
            dtObject.Rows.Add(_TableName, "cond", "Version", _Record.Version);
            dtObject.Rows.Add(_TableName, "cond", "RefCode", _Record.Version);
            dtObject.Rows.Add(_TableName, "cond", "AccountCode", _Record.Version);

            string strErr = Gears.DeleteData(dtObject);

            return strErr;
        }

        public string DeleteRecords(Int32 _Year, string _Version, string _UserID)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();
            dtObject.Rows.Add(_TableName, "cond", "Year", _Year);
            dtObject.Rows.Add(_TableName, "cond", "Version", _Version);

            string strErr = Gears.DeleteData(dtObject);

            return strErr;
        }
    }
}
