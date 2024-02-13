using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ProfitCenter
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string BusinessUnit { get; set; }
        public virtual bool IsHeadOffice { get; set; }
        public virtual bool IsInactive { get; set; }
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




        public DataTable getdata(string Profit, string Conn)//KMM add Conn
        {
            DataTable a;

            if (Profit != null)
            {
                a = Gears.RetriveData2("select * from Accounting.ProfitCenter where ProfitCenterCode = '" + Profit + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {

                    ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    BusinessUnit = dtRow["BusinessUnit"].ToString();
                    IsHeadOffice = Convert.ToBoolean(dtRow["IsHeadOffice"].ToString());
                    IsInactive = Convert.ToBoolean(dtRow["IsInactive"].ToString());

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
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }

            }

            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
            return a;
        }
        public void InsertData(ProfitCenter _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Accounting.ProfitCenter", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "BusinessUnit", _ent.BusinessUnit);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "IsHeadOffice", _ent.IsHeadOffice);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ProfitCenter", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);//KMM add Conn

            //Functions.AuditTrail("REFPFT", _ent.ProfitCenterCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }

        public void UpdateData(ProfitCenter _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Accounting.ProfitCenter", "cond", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "BusinessUnit", _ent.BusinessUnit);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "IsHeadOffice", _ent.IsHeadOffice);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.ProfitCenter", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFPFT", ProfitCenterCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn
        }
        
        public void DeleteData(ProfitCenter _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.ProfitCenter", "cond", "ProfitCenterCode", _ent.ProfitCenterCode);
            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFPFT", ProfitCenterCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn



        }
    }
}
