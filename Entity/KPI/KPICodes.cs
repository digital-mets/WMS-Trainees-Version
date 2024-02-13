using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class KPICodes
    {
        public readonly string _TableName = "KPI.KPICode";

        public virtual string KPICode { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual Boolean YTD { get; set; }
        public virtual string Narrative { get; set; }
        public virtual string SQLScript { get; set; }
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

        public int GetObject(string _KPICode)
        {
            DataTable dtResult;

            dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName+" WHERE KPICode = '" + _KPICode + "'");

            if (dtResult.Rows.Count == 0)
                return -1;      // No record found
            else if (dtResult.Rows.Count > 1)
                return 1;       // duplicate record found
            else
            {
                DataRow _Row = dtResult.Rows[0];

                KPICode = _Row["KPICode"].ToString();
                YTD = Convert.ToBoolean(_Row["YTD"]);
                Description = _Row["Description"].ToString();
                Type = _Row["Type"].ToString();
                Narrative = _Row["Narrative"].ToString();
                SQLScript = _Row["SQLScript"].ToString();
                
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

            dtObject.Rows.Add(_TableName, "0", "KPICode", KPICode);
            dtObject.Rows.Add(_TableName, "0", "Type", Type);
            dtObject.Rows.Add(_TableName, "0", "YTD", YTD);
            dtObject.Rows.Add(_TableName, "0", "Description", Description);
            dtObject.Rows.Add(_TableName, "0", "Narrative", Narrative);
            dtObject.Rows.Add(_TableName, "0", "SQLScript", SQLScript);

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

            dtObject.Rows.Add(_TableName, "cond", "KPICode", KPICode);

            dtObject.Rows.Add(_TableName, "set", "Type", Type);
            dtObject.Rows.Add(_TableName, "set", "YTD", YTD);
            dtObject.Rows.Add(_TableName, "set", "Description", Description);
            dtObject.Rows.Add(_TableName, "set", "Narrative", Narrative); 
            dtObject.Rows.Add(_TableName, "set", "SQLScript", SQLScript);

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
                Functions.AuditTrail("KPI Code", KPICode,
                                     LastEditedBy, ((DateTime)LastEditedDate).ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
            }
            return strErr;
        }
        
        public string DeleteData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "KPICode", KPICode);

            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("KPI Code", KPICode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }

    public class KPIRating
    {
        public readonly string _TableName = "KPI.KPIRating";
        public virtual Int64 RecordID { get; set; }
        public virtual string KPICode { get; set; }
        public virtual Decimal Rating { get; set; }
        public virtual Nullable<Decimal> Lower { get; set; }
        public virtual Nullable<Decimal> Upper { get; set; }

        public DataTable GetRecords(string _KPICode)
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName +" WHERE KPICode ='" + _KPICode + "'");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public string InsertData(KPIRating _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "KPICode", _Record.KPICode);
            dtObject.Rows.Add(_TableName, "0", "Rating", _Record.Rating);
            dtObject.Rows.Add(_TableName, "0", "Lower", _Record.Lower);
            dtObject.Rows.Add(_TableName, "0", "Upper", _Record.Upper);

            return Gears.CreateData(dtObject);
        }

        public string UpdateData(KPIRating _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "Rating", _Record.Rating);
            dtObject.Rows.Add(_TableName, "set", "Lower", _Record.Lower);
            dtObject.Rows.Add(_TableName, "set", "Upper", _Record.Upper);

            return Gears.UpdateData(dtObject);
        }

        public string DeleteData(KPIRating _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            return Gears.DeleteData(dtObject);
        }

        public string DeleteRecords(string _KPICode)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "KPICode", _KPICode);

            return Gears.DeleteData(dtObject);
        }
    }

    public class ChildKPI
    {
        public readonly string _TableName = "KPI.ChildKPI";
        public virtual Int64 RecordID { get; set; }
        public virtual string KPICode { get; set; }
        public virtual string ChildKPICode { get; set; }
        public virtual decimal Weight { get; set; }

        public DataTable GetRecords(string _KPICode)
        {
            DataTable dtResult;
            try
            {
                dtResult = Gears.RetriveData2("SELECT * FROM " + _TableName + " WHERE KPICode ='" + _KPICode + "'");
            }
            catch (Exception e)
            {
                dtResult = null;
            }
            return dtResult;
        }

        public string InsertData(ChildKPI _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "KPICode", _Record.KPICode);
            dtObject.Rows.Add(_TableName, "0", "Weight", _Record.Weight);
            dtObject.Rows.Add(_TableName, "0", "CHildKPICode", _Record.ChildKPICode);

            return Gears.CreateData(dtObject);
        }

        public string UpdateData(ChildKPI _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            dtObject.Rows.Add(_TableName, "set", "Weight", _Record.Weight);
            dtObject.Rows.Add(_TableName, "set", "ChildKPICode", _Record.ChildKPICode);

            return Gears.UpdateData(dtObject);
        }

        public string DeleteData(ChildKPI _Record)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "RecordID", _Record.RecordID);

            return Gears.DeleteData(dtObject);
        }

        public string DeleteRecords(string _KPICode)
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "KPICode", _KPICode);

            return Gears.DeleteData(dtObject);
        }
    }
}
