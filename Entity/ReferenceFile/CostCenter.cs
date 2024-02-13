using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CostCenter
    {

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string BusinessUnit { get; set; }
        public virtual bool IsHeadOffice { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public DataTable getdata(string CostCode, string Conn)
        {
            DataTable a;

            if (CostCode != null)
            {
                a = Gears.RetriveData2("select * from Accounting.CostCenter where CostCenterCode = '" + CostCode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    CostCenterCode = dtRow["CostCenterCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    BusinessUnit = dtRow["BusinessUnit"].ToString();
                    IsHeadOffice = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsHeadOffice"]) ? false : dtRow["IsHeadOffice"]);
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeactivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeactivatedDate"].ToString();
                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString(); 
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(CostCenter _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CostCenter", "0", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.CostCenter", "0", "BusinessUnit", _ent.BusinessUnit);
            DT1.Rows.Add("Accounting.CostCenter", "0", "IsHeadOffice", _ent.IsHeadOffice);
            DT1.Rows.Add("Accounting.CostCenter", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Accounting.CostCenter", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CostCenter", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CostCenter", "0", "Field9", _ent.Field9);
            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(CostCenter _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CostCenter", "cond", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.CostCenter", "set", "BusinessUnit", _ent.BusinessUnit);
            DT1.Rows.Add("Accounting.CostCenter", "set", "IsHeadOffice", _ent.IsHeadOffice);
            DT1.Rows.Add("Accounting.CostCenter", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Accounting.CostCenter", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.CostCenter", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CostCenter", "set", "Field9", _ent.Field9);
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFCOST", _ent.CostCenterCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(CostCenter _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CostCenter", "cond", "CostCenterCode", _ent.CostCenterCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFCOST", _ent.CostCenterCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
