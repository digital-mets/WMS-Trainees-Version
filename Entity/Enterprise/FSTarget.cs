using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class FSTarget
    {
        private string _TableName = "Enterprise.FSTarget";
        public virtual string BusinessType { get; set; }
        public virtual string ReportGroup { get; set; }
        public virtual string AcidRatio { get; set; }
        public virtual string CurrentRatio { get; set; }
        public virtual string CollectionPeriod { get; set; }
        public virtual string InventoryPeriod { get; set; }
        public virtual string PaymentPeriod { get; set; }
        public virtual string ROE { get; set; }
        public virtual string LoanCapital { get; set; }
        public virtual string DebtEquity { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual Nullable<DateTime> LastEditedDate { get; set; }
        //public virtual Boolean IsInactive { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public int GetObject(string _BusinessType)
        {
            DataTable dtResult;

            dtResult = Gears.RetriveData2("SELECT * FROM "+_TableName+" WHERE BusinessType = '" + _BusinessType + "'");

            if (dtResult.Rows.Count == 0)
                return -1;      // No record found
            else if (dtResult.Rows.Count > 1)
                return 1;       // duplicate record found
            else
            {
                DataRow _Row = dtResult.Rows[0];

                BusinessType = _Row["BusinessType"].ToString();
                ReportGroup = _Row["ReportGroup"].ToString();
                AcidRatio = _Row["AcidRatio"].ToString();
                CurrentRatio = _Row["CurrentRatio"].ToString();
                CollectionPeriod = _Row["CollectionPeriod"].ToString();
                InventoryPeriod = _Row["InventoryPeriod"].ToString();
                PaymentPeriod = _Row["PaymentPeriod"].ToString();
                ROE = _Row["ROE"].ToString();
                LoanCapital = _Row["LoanCapital"].ToString();
                DebtEquity = _Row["DebtEquity"].ToString();

//                IsInactive = Convert.ToBoolean((_Row["IsInactive"] == DBNull.Value) ? null : _Row["IsInactive"]);

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
                LastEditedBy = (_Row["LastEditedDate"] == DBNull.Value) ? null :_Row["LastEditedBy"].ToString();
                LastEditedDate = Convert.ToDateTime((_Row["LastEditedDate"] == DBNull.Value) ? null : _Row["LastEditedDate"]);

                return 0;
            }
        }
  
        public string InsertData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "0", "BusinessType", BusinessType);
            dtObject.Rows.Add(_TableName, "0", "ReportGroup", ReportGroup);
            dtObject.Rows.Add(_TableName, "0", "AcidRatio", AcidRatio);
            dtObject.Rows.Add(_TableName, "0", "CurrentRatio", CurrentRatio);
            dtObject.Rows.Add(_TableName, "0", "CollectionPeriod", CollectionPeriod);
            dtObject.Rows.Add(_TableName, "0", "InventoryPeriod", InventoryPeriod);
            dtObject.Rows.Add(_TableName, "0", "PaymentPeriod", PaymentPeriod);
            dtObject.Rows.Add(_TableName, "0", "ROE", ROE);
            dtObject.Rows.Add(_TableName, "0", "LoanCapital", LoanCapital);
            dtObject.Rows.Add(_TableName, "0", "DebtEquity", DebtEquity);
            //dtObject.Rows.Add(_TableName, "0", "IsInactive", IsInactive);

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

            dtObject.Rows.Add(_TableName, "cond", "BusinessType", BusinessType);

            dtObject.Rows.Add(_TableName, "set", "ReportGroup", ReportGroup);
            dtObject.Rows.Add(_TableName, "set", "AcidRatio", AcidRatio);
            dtObject.Rows.Add(_TableName, "set", "CurrentRatio", CurrentRatio);
            dtObject.Rows.Add(_TableName, "set", "CollectionPeriod", CollectionPeriod);
            dtObject.Rows.Add(_TableName, "set", "InventoryPeriod", InventoryPeriod);
            dtObject.Rows.Add(_TableName, "set", "PaymentPeriod", PaymentPeriod);
            dtObject.Rows.Add(_TableName, "set", "ROE", ROE);
            dtObject.Rows.Add(_TableName, "set", "LoanCapital", LoanCapital);
            dtObject.Rows.Add(_TableName, "set", "DebtEquity", DebtEquity);
            //dtObject.Rows.Add(_TableName, "set", "IsInactive", IsInactive);

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
                Functions.AuditTrail("FSTargets", BusinessType,
                                     LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
        
        public string DeleteData()
        {
            Gears.CRUDdatatable dtObject = new Gears.CRUDdatatable();

            dtObject.Rows.Add(_TableName, "cond", "BusinessType", BusinessType);
        
            string strErr = Gears.DeleteData(dtObject);
            if (strErr == "")
            {
                Functions.AuditTrail("FSTargets", BusinessType, 
                                     LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
            }
            return strErr;
        }
    }
}
