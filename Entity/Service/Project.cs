using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Project
    {
        public readonly string _TableName = "Service.Project";

        public virtual string ProjectCode { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual decimal ManHours { get; set; }
        public virtual decimal ProjectCost { get; set; }
        public virtual decimal BillableAmount { get; set; }
        public virtual Nullable<DateTime> DateClosed { get; set; }
        public virtual Nullable<DateTime> DateCancelled { get; set; }
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

        public int GetObject(string _ProjectCode)
        {
            DataTable dtResult;

            dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName+" WHERE ProjectCode = '" + _ProjectCode + "'");

            if (dtResult.Rows.Count == 0)
                return -1;      // No record found
            else if (dtResult.Rows.Count > 1)
                return 1;       // duplicate record found
            else
            {
                DataRow _Row = dtResult.Rows[0];

                ProjectCode = _Row["ProjectCode"].ToString();
                ProjectName = _Row["ProjectName"].ToString();
                CustomerCode = _Row["CustomerCode"].ToString();
                ManHours = Convert.ToDecimal(_Row["ManHours"]);
                ProjectCost = Convert.ToDecimal(_Row["ProjectCost"]);
                BillableAmount = Convert.ToDecimal(_Row["BillableAmount"]);
                DateClosed = Convert.ToDateTime((_Row["DateClosed"] == DBNull.Value) ? null : _Row["DateClosed"]);
                DateCancelled = Convert.ToDateTime((_Row["DateCancelled"] == DBNull.Value) ? null : _Row["DateCancelled"]);
                
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

            dtObject.Rows.Add(_TableName, "0", "ProjectCode", ProjectCode);
            dtObject.Rows.Add(_TableName, "0", "ProjectName", ProjectName);
            dtObject.Rows.Add(_TableName, "0", "CustomerCode", CustomerCode);
            dtObject.Rows.Add(_TableName, "0", "ManHours", ManHours);
            dtObject.Rows.Add(_TableName, "0", "ProjectCost", ProjectCost);
            dtObject.Rows.Add(_TableName, "0", "BillableAmount", BillableAmount);
            dtObject.Rows.Add(_TableName, "0", "DateClosed", DateClosed);
            dtObject.Rows.Add(_TableName, "0", "DateCancelled", DateCancelled);

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

            return Gears.CreateData(dtObject);
        }
        
        public string UpdateData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "ProjectCode", ProjectCode);

            dtObject.Rows.Add(_TableName, "set", "ProjectName", ProjectName);
            dtObject.Rows.Add(_TableName, "set", "CustomerCode", CustomerCode); 
            dtObject.Rows.Add(_TableName, "set", "ManHours", ManHours);
            dtObject.Rows.Add(_TableName, "set", "ProjectCost", ProjectCost);
            dtObject.Rows.Add(_TableName, "set", "BillableAmount", BillableAmount);
            dtObject.Rows.Add(_TableName, "set", "DateClosed", DateClosed);
            dtObject.Rows.Add(_TableName, "set", "DateCancelled", DateCancelled);

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
                Functions.AuditTrail("Project", ProjectCode,
                                     LastEditedBy, ((DateTime)LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }
        
        public string DeleteData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "ProjectCode", ProjectCode);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Company", ProjectCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }

    public class ProjectMilestone
    {
        public readonly string _TableName = "Service.ProjectMilestone";
        public virtual Int64 RecordID { get; set; }
        public virtual string ProjectCode { get; set; }
        public virtual string Milestone { get; set; }
        public virtual Nullable<DateTime> TargetDate { get; set; }
        public virtual Nullable<DateTime> ActualDate { get; set; }
        public virtual string KPICode { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual Boolean IsBillable { get; set; }
        public virtual decimal PercentBillable { get; set; }
        public virtual decimal AmountBillable { get; set; }
        public virtual Boolean OKForBilling { get; set; }
        public virtual Nullable<DateTime> DateOKForBilling { get; set; }
        public virtual string SOANumber { get; set; }
        public virtual string DateBilled { get; set; }
        public virtual string DatePaid { get; set; }

        public DataTable GetRecords(string _ProjectCode)
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName +" WHERE ProjectCode ='" + _ProjectCode + "'");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public string InsertData(ProjectMilestone _Record)    //, string _ProjectCode)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "ProjectCode", _Record.ProjectCode);
            dtObject.Rows.Add(_TableName, "0", "Milestone", _Record.Milestone);
            dtObject.Rows.Add(_TableName, "0", "TargetDate", _Record.TargetDate);
            dtObject.Rows.Add(_TableName, "0", "ActualDate", _Record.ActualDate);
            dtObject.Rows.Add(_TableName, "0", "KPICode", _Record.KPICode);
            dtObject.Rows.Add(_TableName, "0", "Weight", _Record.Weight);
            dtObject.Rows.Add(_TableName, "0", "IsBillable", _Record.IsBillable);
            dtObject.Rows.Add(_TableName, "0", "PercentBillable", _Record.PercentBillable);
            dtObject.Rows.Add(_TableName, "0", "AmountBillable", _Record.AmountBillable);

            return Gears.CreateData(dtObject);
        }

        public string UpdateData(ProjectMilestone _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);
            dtObject.Rows.Add(_TableName, "cond", "OKForBilling", 0);

            dtObject.Rows.Add(_TableName, "set", "Milestone", _Record.Milestone.ToUpper());
            dtObject.Rows.Add(_TableName, "set", "TargetDate", _Record.TargetDate);
            dtObject.Rows.Add(_TableName, "set", "ActualDate", _Record.ActualDate);
            dtObject.Rows.Add(_TableName, "set", "KPICode", _Record.KPICode);
            dtObject.Rows.Add(_TableName, "set", "Weight", _Record.Weight);
            dtObject.Rows.Add(_TableName, "set", "IsBillable", _Record.IsBillable);
            dtObject.Rows.Add(_TableName, "set", "PercentBillable", _Record.PercentBillable);
            dtObject.Rows.Add(_TableName, "set", "AmountBillable", _Record.AmountBillable);

            return Gears.UpdateData(dtObject);
        }

        public string DeleteData(ProjectMilestone _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);
            dtObject.Rows.Add(_TableName, "cond", "OKForBilling", 0);

            return Gears.DeleteData(dtObject);
        }

        public string DeleteRecords(string _ProjectCode)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "ProjectCode", _ProjectCode);
            dtObject.Rows.Add(_TableName, "cond", "OKForBilling", 0);

            return Gears.DeleteData(dtObject);
        }
    }

    public class ProjectBillingInfo
    {
        public readonly string _TableName = "Service.ProjectMilestone";
        public readonly string _ParentTable = "Service.Project";
        public virtual Int64 RecordID { get; set; }
        public virtual string ProjectCode { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual Nullable<DateTime> TargetDate { get; set; }
        public virtual Nullable<DateTime> ActualDate { get; set; }
        public virtual string Particulars { get; set; }
        public virtual decimal AmountBillable { get; set; }
        public virtual Boolean OKForBilling { get; set; }
        public virtual Nullable<DateTime> DateOKForBilling { get; set; }
        public virtual string SOANumber { get; set; }
        public virtual Nullable<DateTime> DateBilled { get; set; }
        public virtual Nullable<DateTime> DatePaid { get; set; }

        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }


        public DataTable GetApprovalInfo()
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT RecordID, MS.ProjectCode, ProjectName, TargetDate, ActualDate "+
                                              ", CONVERT(varchar,CONVERT(decimal(5,0),PercentBillable))+'% '+Milestone AS Particulars "+
                                              ", AmountBillable, OKForBilling, DateOKForBilling "+
                                              "FROM "+_TableName+" MS "+
                                              "INNER JOIN "+_ParentTable+" PJ "+
                                              "ON MS.ProjectCode = PJ.ProjectCode "+
                                              "WHERE ISNULL(AmountBillable,0) != 0 AND ActualDate IS NOT NULL "+
	                                          "AND DateBilled IS NULL");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public string UpdateApproval(ProjectBillingInfo _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);
            dtObject.Rows.Add(_TableName, "cond", "ISNULL(SOANumber,'')", "");

            dtObject.Rows.Add(_TableName, "set", "OKForBilling", _Record.OKForBilling);
            dtObject.Rows.Add(_TableName, "set", "LastEditedBy", _Record.LastEditedBy);
            _Record.LastEditedDate = DateTime.Now;
            dtObject.Rows.Add(_TableName, "set", "LastEditedDate", ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"));
            if (_Record.OKForBilling)
            {
                if (_Record.DateOKForBilling == null)
                    dtObject.Rows.Add(_TableName, "set", "DateOKForBilling", ((DateTime)_Record.LastEditedDate).Date);
            }
            else
                dtObject.Rows.Add(_TableName, "set", "DateOKForBilling", null);

            string strErr = Gears.UpdateData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("Billing Approval", _Record.ProjectCode + ";" + _Record.Particulars,
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }

        public DataTable GetBillingInfo()
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT RecordID, MS.ProjectCode, ProjectName, TargetDate, ActualDate "+
                                              ", CONVERT(varchar,CONVERT(decimal(5,0),PercentBillable))+'% '+Milestone AS Particulars "+
                                              ", AmountBillable, DateOKForBilling, SOANumber, DateBilled, DatePaid "+
                                              "FROM "+_TableName+" MS "+
                                              "INNER JOIN "+_ParentTable+" PJ "+
                                              "ON MS.ProjectCode = PJ.ProjectCode "+
                                              "WHERE DateOKForBilling IS NOT NULL");
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
                dtResult = Gears.RetriveData2("SELECT RecordID, MS.ProjectCode, ProjectName, TargetDate, ActualDate " +
                                              ", CONVERT(varchar,CONVERT(decimal(5,0),PercentBillable))+'% '+Milestone AS Particulars " +
                                              ", AmountBillable, DateOKForBilling, SOANumber, DateBilled, DatePaid " +
                                              "FROM " + _TableName + " MS " +
                                              "INNER JOIN " + _ParentTable + " PJ " +
                                              "ON MS.ProjectCode = PJ.ProjectCode " +
                                              "WHERE DateOKForBilling IS NOT NULL AND DateBilled IS NULL");
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
                dtResult = Gears.RetriveData2("SELECT RecordID, MS.ProjectCode, ProjectName, TargetDate, ActualDate " +
                                              ", CONVERT(varchar,CONVERT(decimal(5,0),PercentBillable))+'% '+Milestone AS Particulars " +
                                              ", AmountBillable, DateOKForBilling, SOANumber, DateBilled, DatePaid " +
                                              "FROM " + _TableName + " MS " +
                                              "INNER JOIN " + _ParentTable + " PJ " +
                                              "ON MS.ProjectCode = PJ.ProjectCode " +
                                              "WHERE (DateOKForBilling IS NOT NULL OR DateBilled IS NOT NULL) AND DatePaid IS NULL");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public string UpdateBilling(ProjectBillingInfo _Record)
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
                Functions.AuditTrail("Project Billing", _Record.ProjectCode + ";" + _Record.Particulars,
                                     _Record.LastEditedBy, ((DateTime)_Record.LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }
    }
}
