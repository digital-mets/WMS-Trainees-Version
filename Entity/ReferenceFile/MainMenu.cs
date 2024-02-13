using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class MainMenu
    {
        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string RecordID { get; set; }
        public virtual string MenuID { get; set; }
        public virtual string MenuSequence { get; set; }
        public virtual string FuncGroupID { get; set; }
        public virtual string ModuleID { get; set; }
        public virtual string ModuleDescription { get; set; }
        public virtual string CommandString { get; set; }
        public virtual string IconFileName { get; set; }
        public virtual string HelpIndex { get; set; }
        public virtual string PrimarykeyColumn { get; set; }
        public virtual string TableName { get; set; }
        public virtual string StoredProcedure { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual string Parameters { get; set; }
        public virtual string FactBox { get; set; }
        public virtual string CancelStoredProcedure { get; set; }
        public virtual string ApproveStoredProcedure { get; set; }
        public virtual string Ribbon { get; set; }
        public virtual string GLPosting { get; set; }
        public virtual string ColDate { get; set; }
        public virtual string SQLCommand { get; set; }
        public virtual string Extract { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }


        public DataTable getdata(string RecordID, string Conn)
        {
            DataTable a;

            if (RecordID != null)
            {
                a = Gears.RetriveData2("select * from IT.MainMenu where RecordID = '" + RecordID + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    MenuID = dtRow["MenuID"].ToString();
                    MenuSequence = dtRow["MenuSequence"].ToString();
                    FuncGroupID = dtRow["FuncGroupID"].ToString();
                    ModuleID = dtRow["ModuleID"].ToString();
                    ModuleDescription = dtRow["ModuleDescription"].ToString();
                    CommandString = dtRow["CommandString"].ToString();
                    IconFileName = dtRow["IconFileName"].ToString();
                    HelpIndex = dtRow["HelpIndex"].ToString();
                    PrimarykeyColumn = dtRow["PKeyColumn"].ToString();
                    TableName = dtRow["TableName"].ToString();
                    StoredProcedure = dtRow["StoredProc"].ToString();
                    TransactionType = dtRow["TransType"].ToString();
                    Parameters = dtRow["Parameters"].ToString();
                    FactBox = dtRow["FactBox"].ToString();
                    CancelStoredProcedure = dtRow["CancelSP"].ToString();
                    ApproveStoredProcedure = dtRow["ApproveSP"].ToString();
                    Ribbon = dtRow["Ribbon"].ToString();
                    GLPosting = dtRow["GLPosting"].ToString();
                    ColDate = dtRow["ColumnDate"].ToString();
                    SQLCommand = dtRow["SQLCommand"].ToString();
                    Extract = dtRow["SQLExtract"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(MainMenu _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.MainMenu", "0", "MenuID ", _ent.MenuID);
            DT1.Rows.Add("IT.MainMenu", "0", "MenuSequence", _ent.MenuSequence);
            DT1.Rows.Add("IT.MainMenu", "0", "FuncGroupID", _ent.FuncGroupID);
            DT1.Rows.Add("IT.MainMenu", "0", "ModuleID", _ent.ModuleID);
            DT1.Rows.Add("IT.MainMenu", "0", "ModuleDescription", _ent.ModuleDescription);
            DT1.Rows.Add("IT.MainMenu", "0", "CommandString", _ent.CommandString);
            DT1.Rows.Add("IT.MainMenu", "0", "IconFileName", _ent.IconFileName);
            DT1.Rows.Add("IT.MainMenu", "0", "HelpIndex", _ent.HelpIndex);
            DT1.Rows.Add("IT.MainMenu", "0", "PKeyColumn", _ent.PrimarykeyColumn);
            DT1.Rows.Add("IT.MainMenu", "0", "TableName", _ent.TableName);
            DT1.Rows.Add("IT.MainMenu", "0", "StoredProc", _ent.StoredProcedure);
            DT1.Rows.Add("IT.MainMenu", "0", "TransType", _ent.TransactionType);
            DT1.Rows.Add("IT.MainMenu", "0", "Parameters", _ent.Parameters);
            DT1.Rows.Add("IT.MainMenu", "0", "FactBox", _ent.FactBox);
            DT1.Rows.Add("IT.MainMenu", "0", "CancelSP", _ent.CancelStoredProcedure);
            DT1.Rows.Add("IT.MainMenu", "0", "ApproveSP", _ent.ApproveStoredProcedure);
            DT1.Rows.Add("IT.MainMenu", "0", "Ribbon", _ent.Ribbon);
            DT1.Rows.Add("IT.MainMenu", "0", "GLPosting", _ent.GLPosting);
            DT1.Rows.Add("IT.MainMenu", "0", "ColumnDate", _ent.ColDate);
            DT1.Rows.Add("IT.MainMenu", "0", "SQLCommand", _ent.SQLCommand);
            DT1.Rows.Add("IT.MainMenu", "0", "SQLExtract", _ent.Extract);
            DT1.Rows.Add("IT.MainMenu", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.MainMenu", "0", "AddedDate", _ent.AddedDate);

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(MainMenu _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.MainMenu", "cond", "MenuID ", _ent.MenuID);
            DT1.Rows.Add("IT.MainMenu", "set", "MenuSequence", _ent.MenuSequence);
            DT1.Rows.Add("IT.MainMenu", "set", "FuncGroupID", _ent.FuncGroupID);
            DT1.Rows.Add("IT.MainMenu", "set", "ModuleID", _ent.ModuleID);
            DT1.Rows.Add("IT.MainMenu", "set", "ModuleDescription", _ent.ModuleDescription);
            DT1.Rows.Add("IT.MainMenu", "set", "CommandString", _ent.CommandString);
            DT1.Rows.Add("IT.MainMenu", "set", "IconFileName", _ent.IconFileName);
            DT1.Rows.Add("IT.MainMenu", "set", "HelpIndex", _ent.HelpIndex);
            DT1.Rows.Add("IT.MainMenu", "set", "PKeyColumn", _ent.PrimarykeyColumn);
            DT1.Rows.Add("IT.MainMenu", "set", "TableName", _ent.TableName);
            DT1.Rows.Add("IT.MainMenu", "set", "StoredProc", _ent.StoredProcedure);
            DT1.Rows.Add("IT.MainMenu", "set", "TransType", _ent.TransactionType);
            DT1.Rows.Add("IT.MainMenu", "set", "Parameters", _ent.Parameters);
            DT1.Rows.Add("IT.MainMenu", "set", "FactBox", _ent.FactBox);
            DT1.Rows.Add("IT.MainMenu", "set", "CancelSP", _ent.CancelStoredProcedure);
            DT1.Rows.Add("IT.MainMenu", "set", "ApproveSP", _ent.ApproveStoredProcedure);
            DT1.Rows.Add("IT.MainMenu", "set", "Ribbon", _ent.Ribbon);
            DT1.Rows.Add("IT.MainMenu", "set", "GLPosting", _ent.GLPosting);
            DT1.Rows.Add("IT.MainMenu", "set", "ColumnDate", _ent.ColDate);
            DT1.Rows.Add("IT.MainMenu", "set", "SQLCommand", _ent.SQLCommand);
            DT1.Rows.Add("IT.MainMenu", "set", "SQLExtract", _ent.Extract);
            DT1.Rows.Add("IT.MainMenu", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("IT.MainMenu", "set", "LastEditedDate", _ent.LastEditedDate);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("MAINMENU", _ent.MenuID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
                  
        }

        public void DeleteData(MainMenu _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.MainMenu", "cond", "RecordID", _ent.RecordID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("MAINMENU", _ent.MenuID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
           
        }
    }
}
