using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Module
    {
        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string ModuleID { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string NotedBy { get; set; }
        public virtual string Field1Label { get; set; }
        public virtual string Field2Label { get; set; }
        public virtual string Field3Label { get; set; }
        public virtual string Field4Label { get; set; }
        public virtual string Field5Label { get; set; }
        public virtual string Field6Label { get; set; }
        public virtual string Field7Label { get; set; }
        public virtual string Field8Label { get; set; }
        public virtual string Field9Label { get; set; }
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

        public DataTable getdata(string ModuleID, string Conn)
        {
            DataTable a;

            if (ModuleID != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.Module where ModuleID = '" + ModuleID + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    ModuleID = dtRow["ModuleID"].ToString();
                    NotedBy = dtRow["NotedBy"].ToString();
                    ApprovedBy = dtRow["ApprovedBy"].ToString();

                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();

                    Field1Label = dtRow["Field1Label"].ToString();
                    Field2Label = dtRow["Field2Label"].ToString();
                    Field3Label = dtRow["Field3Label"].ToString();
                    Field4Label = dtRow["Field4Label"].ToString();
                    Field5Label = dtRow["Field5Label"].ToString();
                    Field6Label = dtRow["Field6Label"].ToString();
                    Field7Label = dtRow["Field7Label"].ToString();
                    Field8Label = dtRow["Field8Label"].ToString();
                    Field9Label = dtRow["Field9Label"].ToString();

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
        public void InsertData(Module _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Module", "0", "ModuleID", _ent.ModuleID);
            DT1.Rows.Add("Masterfile.Module", "0", "NotedBy", _ent.NotedBy);
            DT1.Rows.Add("Masterfile.Module", "0", "ApprovedBy", _ent.ApprovedBy);

            DT1.Rows.Add("Masterfile.Module", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Module", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Module", "0", "Field1Label", _ent.Field1Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field2Label", _ent.Field2Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field3Label", _ent.Field3Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field4Label", _ent.Field4Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field5Label", _ent.Field5Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field6Label", _ent.Field6Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field7Label", _ent.Field7Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field8Label", _ent.Field8Label);
            DT1.Rows.Add("Masterfile.Module", "0", "Field9Label", _ent.Field9Label);

            DT1.Rows.Add("Masterfile.Module", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Module", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Module", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Module", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Module", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Module", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Module", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Module", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Module", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFMOD", _ent.ModuleID, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateData(Module _ent)
        {
            Conn = _ent.Connection; //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Module", "cond", "ModuleID", _ent.ModuleID);
            DT1.Rows.Add("Masterfile.Module", "set", "NotedBy", _ent.NotedBy);
            DT1.Rows.Add("Masterfile.Module", "set", "ApprovedBy", _ent.ApprovedBy);

            DT1.Rows.Add("Masterfile.Module", "set", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Module", "set", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Module", "set", "Field1Label", _ent.Field1Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field2Label", _ent.Field2Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field3Label", _ent.Field3Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field4Label", _ent.Field4Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field5Label", _ent.Field5Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field6Label", _ent.Field6Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field7Label", _ent.Field7Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field8Label", _ent.Field8Label);
            DT1.Rows.Add("Masterfile.Module", "set", "Field9Label", _ent.Field9Label);

            DT1.Rows.Add("Masterfile.Module", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Module", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Module", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Module", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Module", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Module", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Module", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Module", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Module", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Module", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Module", "set", "Field9", _ent.Field9);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFMOD", _ent.ModuleID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(Module _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            ModuleID = _ent.ModuleID;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Module", "cond", "ModuleID", _ent.ModuleID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFMOD", _ent.ModuleID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
