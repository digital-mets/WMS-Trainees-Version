using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class OrganizationChart
    {
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN
        public virtual string OrganizationChartEntityID { get; set; }
        public virtual string OrganizationChartDescription { get; set; }
        public virtual string OrganizationChartParentEntityID { get; set; }
        public virtual string OrganizationChartIsInactive { get; set; }
        public virtual string OrganizationChartRecordID { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public DataTable getdata(string EntID, string Conn)
        {
            DataTable a;

            if (EntID != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.OrgChartEntity where EntityID = '" + EntID + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    OrganizationChartEntityID = dtRow["EntityID"].ToString();
                    OrganizationChartDescription = dtRow["Description"].ToString();
                    OrganizationChartParentEntityID = dtRow["ParentEntityID"].ToString();
                    OrganizationChartIsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]).ToString();
  
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();

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
        public void InsertData(OrganizationChart _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "EntityID", _ent.OrganizationChartEntityID);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Description", _ent.OrganizationChartDescription);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "ParentEntityID", _ent.OrganizationChartParentEntityID);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "IsInactive", _ent.OrganizationChartIsInactive);

            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFORGCHARTENTITY", _ent.OrganizationChartEntityID, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateData(OrganizationChart _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.OrgChartEntity", "cond", "EntityID", _ent.OrganizationChartEntityID);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Description", _ent.OrganizationChartDescription);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "ParentEntityID", _ent.OrganizationChartParentEntityID);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "IsInactive", _ent.OrganizationChartIsInactive);

            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.OrgChartEntity", "set", "Field9", _ent.Field9);
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFORGCHARTENTITY", _ent.OrganizationChartEntityID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(OrganizationChart _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            OrganizationChartEntityID = _ent.OrganizationChartEntityID;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.OrgChartEntity", "cond", "EntityID", _ent.OrganizationChartEntityID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFORGCHARTENTITY", _ent.OrganizationChartEntityID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
