using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class GLAccountGroup
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
       
        
        public virtual string GroupCode { get; set; }
        public virtual string GroupLevel { get; set; }
        public virtual string Description { get; set; }
        public virtual string AccountFr { get; set; }
        public virtual string AccountTo { get; set; }



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


    public DataTable getdata(string Group, string Conn)
        {
            DataTable a;
            if (Group != null)
            {
            a = Gears.RetriveData2("select * from Accounting.GLAccountGroup where GroupCode = '" + Group + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                GroupCode = dtRow["GroupCode"].ToString();
                GroupLevel = dtRow["GroupLevel"].ToString();
                Description = dtRow["Description"].ToString();
                AccountFr = dtRow["AccountFr"].ToString();
                AccountTo = dtRow["AccountTo"].ToString();



                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();

            }
            }

                //LastEditedBy = dtRow["LastEditedBy"].ToString();
                //LastEditedDate = dtRow["LastEditedDate"].ToString();
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(GLAccountGroup _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "GroupCode", _ent.GroupCode);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "GroupLevel", _ent.GroupLevel);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "AccountFr", _ent.AccountFr);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "AccountTo", _ent.AccountTo);



            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "Field9", _ent.Field9);


            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.GLAccountGroup", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("ACTGLG", _ent.GroupCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // KMM add Conn
        }

        public void UpdateData(GLAccountGroup _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.GLAccountGroup", "cond", "GroupCode", _ent.GroupCode);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "GroupLevel", _ent.GroupLevel);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "AccountFr", _ent.AccountFr);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "AccountTo", _ent.AccountTo);
           
          

            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.GLAccountGroup", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTGLG", _ent.GroupCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(GLAccountGroup _ent)
        {
            
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.GLAccountGroup", "cond", "GroupCode", _ent.GroupCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTGLG", _ent.GroupCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
