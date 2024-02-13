using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class SLAContract
    {
        public readonly string _TableName = "Service.Contract";

        public virtual string ContractNumber { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual DateTime DateSigned { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal ContractAmount { get; set; }
        public virtual decimal BillableAmount { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public int GetObject(string _ContractNumber)
        {
            DataTable dtResult;

            dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName+" WHERE ContractNumber = '" + _ContractNumber + "'");

            if (dtResult.Rows.Count == 0)
                return -1;      // No record found
            else if (dtResult.Rows.Count > 1)
                return 1;       // duplicate record found
            else
            {
                DataRow _Row = dtResult.Rows[0];

                ContractNumber = _Row["ContractNumber"].ToString();
                CustomerCode = _Row["CustomerCode"].ToString();
                ServiceType = _Row["ServiceType"].ToString();
                DateSigned = Convert.ToDateTime(_Row["DateSigned"]);
                Remarks = _Row["Remarks"].ToString();
                ContractAmount = Convert.ToDecimal(_Row["ContractAmount"]);
                BillableAmount = Convert.ToDecimal(_Row["BillableAmount"]);
                
                Field1 = _Row["Field1"].ToString();
                Field2 = _Row["Field2"].ToString();
                Field3 = _Row["Field3"].ToString();
                Field4 = _Row["Field4"].ToString();
                Field5 = _Row["Field5"].ToString();
                Field6 = _Row["Field6"].ToString();
                Field7 = _Row["Field7"].ToString();
                Field8 = _Row["Field8"].ToString();
                Field9 = _Row["Field9"].ToString();

                AddedBy = _Row["AddedBy"].ToString();
                AddedDate = Convert.ToDateTime((_Row["AddedDate"] == DBNull.Value) ? null : _Row["AddedDate"]);
                LastEditedBy = (_Row["LastEditedDate"] == DBNull.Value) ? null : _Row["LastEditedBy"].ToString();
                LastEditedDate = Convert.ToDateTime((_Row["LastEditedDate"] == DBNull.Value) ? null : _Row["LastEditedDate"]);

                return 0;
            }
        }
  
        public string InsertData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "ContractNumber", ContractNumber);
            dtObject.Rows.Add(_TableName, "0", "CustomerCode", CustomerCode);
            dtObject.Rows.Add(_TableName, "0", "ServiceType", ServiceType);
            dtObject.Rows.Add(_TableName, "0", "DateSigned", DateSigned);
            dtObject.Rows.Add(_TableName, "0", "Remarks", Remarks);
            dtObject.Rows.Add(_TableName, "0", "ContractAmount", ContractAmount);
            dtObject.Rows.Add(_TableName, "0", "BillableAmount", BillableAmount);

            dtObject.Rows.Add(_TableName, "0", "Field1", Field1);
            dtObject.Rows.Add(_TableName, "0", "Field2", Field2);
            dtObject.Rows.Add(_TableName, "0", "Field3", Field3);
            dtObject.Rows.Add(_TableName, "0", "Field4", Field4);
            dtObject.Rows.Add(_TableName, "0", "Field5", Field5);
            dtObject.Rows.Add(_TableName, "0", "Field6", Field6);
            dtObject.Rows.Add(_TableName, "0", "Field7", Field7);
            dtObject.Rows.Add(_TableName, "0", "Field8", Field8);
            dtObject.Rows.Add(_TableName, "0", "Field9", Field9);

            dtObject.Rows.Add(_TableName, "0", "AddedBy", AddedBy);
            dtObject.Rows.Add(_TableName, "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErrMssg = Gears.CreateData(dtObject);
            return strErrMssg;
        }
        
        public string UpdateData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "ContractNumber", ContractNumber);

            dtObject.Rows.Add(_TableName, "set", "CustomerCode", CustomerCode);
            dtObject.Rows.Add(_TableName, "set", "ServiceType", ServiceType);
            dtObject.Rows.Add(_TableName, "set", "DateSigned", DateSigned);
            dtObject.Rows.Add(_TableName, "set", "Remarks", Remarks);
            dtObject.Rows.Add(_TableName, "set", "ContractAmount", ContractAmount);
            dtObject.Rows.Add(_TableName, "set", "BillableAmount", BillableAmount);

            dtObject.Rows.Add(_TableName, "set", "Field1", Field1);
            dtObject.Rows.Add(_TableName, "set", "Field2", Field2);
            dtObject.Rows.Add(_TableName, "set", "Field3", Field3);
            dtObject.Rows.Add(_TableName, "set", "Field4", Field4);
            dtObject.Rows.Add(_TableName, "set", "Field5", Field5);
            dtObject.Rows.Add(_TableName, "set", "Field6", Field6);
            dtObject.Rows.Add(_TableName, "set", "Field7", Field7);
            dtObject.Rows.Add(_TableName, "set", "Field8", Field8);
            dtObject.Rows.Add(_TableName, "set", "Field9", Field9);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", LastEditedBy);
            LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("SLA Contract", ContractNumber,
                                     LastEditedBy, ((DateTime)LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }
        
        public string DeleteData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "ContractNumber", ContractNumber);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("SLA Contract", ContractNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }

    public class SLABilling
    {
        public readonly string _TableName = "Service.ContractBilling";
        public virtual Int64 RecordID { get; set; }
        public virtual string ContractNumber { get; set; }
        public virtual string Period { get; set; }
        public virtual DateTime ReferenceDate { get; set; }
        public virtual decimal AmountBillable { get; set; }
        public virtual string SOANumber { get; set; }
        public virtual string DateBilled { get; set; }
        public virtual string DatePaid { get; set; }

        public DataTable GetRecords(string _ContractNumber)
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName +" WHERE ContractNumber ='" + _ContractNumber + "'");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public string InsertData(SLABilling _Record)    //, string _ProjectCode)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "ContractNumber", _Record.ContractNumber);
            dtObject.Rows.Add(_TableName, "0", "Period", _Record.Period);
            dtObject.Rows.Add(_TableName, "0", "ReferenceDate", _Record.ReferenceDate);
            dtObject.Rows.Add(_TableName, "0", "AmountBillable", _Record.AmountBillable);

            return Gears.CreateData(dtObject);
        }

        public string UpdateData(SLABilling _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);
            dtObject.Rows.Add(_TableName, "cond", "ISNULL(SOANumber,'')", "");

            dtObject.Rows.Add(_TableName, "set", "Period", _Record.Period);
            dtObject.Rows.Add(_TableName, "set", "ReferenceDate", _Record.ReferenceDate);
            dtObject.Rows.Add(_TableName, "set", "AmountBillable", _Record.AmountBillable);

            return Gears.UpdateData(dtObject);
        }

        public string DeleteData(SLABilling _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);
            dtObject.Rows.Add(_TableName, "cond", "ISNULL(SOANumber,'')", "");

            return Gears.DeleteData(dtObject);
        }

        public string DeleteRecords(string _ContractNumber)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "ContractNumber", _ContractNumber);
            dtObject.Rows.Add(_TableName, "cond", "ISNULL(SOANumber,'')", "");

            return Gears.DeleteData(dtObject);
        }
    }

    public class SLABillingInfo
    {
        public readonly string _TableName = "Service.ContractBilling";
        public readonly string _ParentTable = "Service.Contract";
        public virtual Int64 RecordID { get; set; }
        public virtual string ContractNumber { get; set; }
        public virtual string Period { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual DateTime DateSigned { get; set; }
        public virtual string Particulars { get; set; }
        public virtual decimal AmountBillable { get; set; }
        public virtual string SOANumber { get; set; }
        public virtual Nullable<DateTime> DateBilled { get; set; }
        public virtual Nullable<DateTime> DatePaid { get; set; }

        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }

        public DataTable GetBillingInfo()
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT RecordID, DTL.ContractNumber, Period " +
                                              ", CustomerCode, DateSigned, STYPE.Description AS Particulars " +
                                              ", AmountBillable, SOANumber, DateBilled, DatePaid "+
                                              "FROM "+_TableName+" DTL "+
                                              "INNER JOIN "+_ParentTable+" SLA "+
                                              "ON DTL.ContractNumber = SLA.ContractNumber " +
                                              "INNER JOIN IT.GenericLookup STYPE " +
                                              "ON SLA.ServiceType = STYPE.Code");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public DataTable GetUnbilled()
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT RecordID, DTL.ContractNumber, Period " +
                                              ", CustomerCode, DateSigned, STYPE.Description AS Particulars " +
                                              ", AmountBillable, SOANumber, DateBilled, DatePaid " +
                                              "FROM " + _TableName + " DTL " +
                                              "INNER JOIN " + _ParentTable + " SLA " +
                                              "ON DTL.ContractNumber = SLA.ContractNumber " +
                                              "INNER JOIN IT.GenericLookup STYPE " +
                                              "ON SLA.ServiceType = STYPE.Code " +
                                              "WHERE ReferenceDate <= '"+DateTime.Today.ToString("yyyy-MM-dd")+
                                              "' AND DateBilled IS NULL");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public DataTable GetUnpaid()
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT RecordID, DTL.ContractNumber, Period " +
                                              ", CustomerCode, DateSigned, STYPE.Description AS Particulars " +
                                              ", AmountBillable, SOANumber, DateBilled, DatePaid " +
                                              "FROM " + _TableName + " DTL " +
                                              "INNER JOIN " + _ParentTable + " SLA " +
                                              "ON DTL.ContractNumber = SLA.ContractNumber " +
                                              "INNER JOIN IT.GenericLookup STYPE " +
                                              "ON SLA.ServiceType = STYPE.Code " +
                                              "WHERE ReferenceDate <= '" + DateTime.Today.ToString("yyyy-MM-dd") +
                                              "' AND DatePaid IS NULL");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public string UpdateBilling(SLABillingInfo _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "SOANumber", _Record.SOANumber);
            dtObject.Rows.Add(_TableName, "set", "DateBilled", _Record.DateBilled);
            dtObject.Rows.Add(_TableName, "set", "DatePaid", _Record.DatePaid);

            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", _Record.LastEditedBy);
            _Record.LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("SLA Billing", _Record.ContractNumber + ";" + _Record.Period,
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }
    }
}
