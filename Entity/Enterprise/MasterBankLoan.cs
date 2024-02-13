using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class MasterBankLoan
    {
        public string _TableName = "Enterprise.BankLoan";
        
        public virtual Int64 RecordID { get; set; }
    	public virtual DateTime ReferenceDate { get; set; }
    	public virtual String BankCode { get; set; }
    	public virtual String CompanyCode { get; set; }
        public virtual String Name { get; set; }
        public virtual String LoanType { get; set; }
        public virtual String PNNumber { get; set; }
    	public virtual DateTime DateAcquired { get; set; }
    	public virtual DateTime MaturityDate { get; set; }
    	public virtual Nullable<DateTime> LastIntDate { get; set; }
    	public virtual Nullable<DateTime> NextIntDate { get; set; }
    	public virtual Decimal InterestRate { get; set; }
    	public virtual Decimal PrincipalAmount { get; set; }
        public virtual Decimal Balance { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }

        public DataTable GetRecords(string _ReferenceDate)
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM " + _TableName + " WHERE ReferenceDate='" + _ReferenceDate + "' ORDER BY RecordID");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }
  
        public string InsertData(MasterBankLoan _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "ReferenceDate", _Record.ReferenceDate.ToString("yyyy-MM-dd"));
            dtObject.Rows.Add(_TableName, "0", "BankCode", _Record.BankCode);
            dtObject.Rows.Add(_TableName, "0", "CompanyCode", _Record.CompanyCode);
            dtObject.Rows.Add(_TableName, "0", "LoanType", (_Record.LoanType == null) ? null : _Record.LoanType.ToUpper());
            dtObject.Rows.Add(_TableName, "0", "Name", (_Record.Name == null) ? null : _Record.Name.ToUpper());
            dtObject.Rows.Add(_TableName, "0", "PNNumber", (_Record.PNNumber == null) ? null : _Record.PNNumber.ToUpper());
            dtObject.Rows.Add(_TableName, "0", "DateAcquired", _Record.DateAcquired);
            dtObject.Rows.Add(_TableName, "0", "MaturityDate", _Record.MaturityDate);
            dtObject.Rows.Add(_TableName, "0", "LastIntDate", _Record.LastIntDate);
            dtObject.Rows.Add(_TableName, "0", "NextIntDate", _Record.NextIntDate);
            dtObject.Rows.Add(_TableName, "0", "InterestRate", _Record.InterestRate);
            dtObject.Rows.Add(_TableName, "0", "PrincipalAmount", _Record.PrincipalAmount);
            if (_Record.Balance == 0) { _Record.Balance = _Record.PrincipalAmount; }
            dtObject.Rows.Add(_TableName, "0", "Balance", _Record.Balance);

            dtObject.Rows.Add(_TableName, "0", "AddedBy", _Record.AddedBy);
            dtObject.Rows.Add(_TableName, "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.CreateData(dtObject);

            return strErr;
        }

        public string UpdateData(MasterBankLoan _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "BankCode", _Record.BankCode);
            dtObject.Rows.Add(_TableName, "set", "CompanyCode", _Record.CompanyCode);
            dtObject.Rows.Add(_TableName, "set", "LoanType", _Record.LoanType);
            dtObject.Rows.Add(_TableName, "set", "Name", _Record.Name);
            dtObject.Rows.Add(_TableName, "set", "PNNumber", _Record.PNNumber);
            dtObject.Rows.Add(_TableName, "set", "DateAcquired", _Record.DateAcquired);
            dtObject.Rows.Add(_TableName, "set", "MaturityDate", _Record.MaturityDate);
            dtObject.Rows.Add(_TableName, "set", "LastIntDate", _Record.LastIntDate);
            dtObject.Rows.Add(_TableName, "set", "NextIntDate", _Record.NextIntDate);
            dtObject.Rows.Add(_TableName, "set", "InterestRate", _Record.InterestRate);
            dtObject.Rows.Add(_TableName, "set", "PrincipalAmount", _Record.PrincipalAmount);
            dtObject.Rows.Add(_TableName, "set", "Balance", _Record.Balance);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", _Record.LastEditedBy);
            _Record.LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Master Bank Loan", _Record.ReferenceDate.ToString("yyyy-MM-dd")+";"+_Record.BankCode+";"+_Record.CompanyCode+";"+_Record.LoanType,
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }

        public string DeleteData(MasterBankLoan _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Master Bank Loan", _Record.ReferenceDate.ToString("yyyy-MM-dd")+";"+_Record.BankCode+";"+_Record.CompanyCode+";"+_Record.LoanType,
                                     _Record.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }

        public string DeleteRecords(string _ReferenceDate, string _UserID)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();
            dtObject.Rows.Add(_TableName, "cond", "ReferenceDate", _ReferenceDate);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Master Bank Loan", _ReferenceDate,
                                     _UserID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }
}
