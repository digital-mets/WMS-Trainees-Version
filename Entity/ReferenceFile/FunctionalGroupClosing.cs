using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class FunctionalGroupClosing
    {

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string FunctionalGroupID { get; set; }
        public virtual string Description { get; set; }
        public virtual string AssignHead { get; set; }
        public virtual string DateClosed { get; set; }
        public virtual int Days { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public DataTable getdata(string FuncGroupID, string Conn)
        {
            DataTable a;

            if (FuncGroupID != null)
            {
                a = Gears.RetriveData2("select * from IT.FuncGroup where FuncGroupID = '" + FuncGroupID + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    FunctionalGroupID = dtRow["FuncGroupID"].ToString();
                    Description = dtRow["Description"].ToString();
                    AssignHead = dtRow["Head"].ToString();
                    DateClosed = dtRow["DateClosed"].ToString();
                    Days = Convert.ToInt32(dtRow["Days"].ToString());
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
        public void InsertData(FunctionalGroupClosing _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.FuncGroup", "0", "FuncGroupID", _ent.FunctionalGroupID);
            DT1.Rows.Add("IT.FuncGroup", "0", "Description", _ent.Description);
            DT1.Rows.Add("IT.FuncGroup", "0", "Head", _ent.AssignHead);
            DT1.Rows.Add("IT.FuncGroup", "0", "DateClosed", _ent.DateClosed);
            DT1.Rows.Add("IT.FuncGroup", "0", "Days", _ent.Days);
            DT1.Rows.Add("IT.FuncGroup", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.FuncGroup", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("IT.FuncGroup", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.FuncGroup", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(FunctionalGroupClosing _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.FuncGroup", "cond", "FuncGroupID", _ent.FunctionalGroupID);
            DT1.Rows.Add("IT.FuncGroup", "set", "Description", _ent.Description);
            DT1.Rows.Add("IT.FuncGroup", "set", "Head", _ent.AssignHead);
            DT1.Rows.Add("IT.FuncGroup", "set", "DateClosed", _ent.DateClosed);
            DT1.Rows.Add("IT.FuncGroup", "set", "Days", _ent.Days);
            DT1.Rows.Add("IT.FuncGroup", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.FuncGroup", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("IT.FuncGroup", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.FuncGroup", "set", "Field9", _ent.Field9);
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("FUNCGRP", _ent.FunctionalGroupID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

            strErr = Gears.UpdateData(DT1, _ent.Connection);
        }

        public void DeleteData(FunctionalGroupClosing _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.FuncGroup", "cond", "FuncGroupID", _ent.FunctionalGroupID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("FUNCGRP", _ent.FunctionalGroupID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
