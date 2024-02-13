using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class RoleAndAccessControl
    {

        private static string ID;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string RoleID { get; set; }
        public virtual string Description { get; set; }
        public virtual string PanelName { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual IList<RolesDetail> Detail { get; set; }


        public class RolesDetail
        {
            public virtual RoleAndAccessControl Parent { get; set; }
            public virtual string RoleID { get; set; }
            public virtual string ModuleID { get; set; }
            public virtual string prevModuleID { get; set; }
            public virtual string Access { get; set; }
            public virtual string FactBox { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }



            public DataTable getdetail(string RoleID, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,ModuleID as prevModuleID from IT.UserRolesDetail where RoleID='" + RoleID + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRolesDetail(RolesDetail RolesDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("IT.UserRolesDetail", "0", "RoleID", ID);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "ModuleID", RolesDetail.ModuleID);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Access", RolesDetail.Access);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "FactBox", RolesDetail.FactBox); 
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field1", RolesDetail.Field1);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field2", RolesDetail.Field2);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field3", RolesDetail.Field3);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field4", RolesDetail.Field4);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field5", RolesDetail.Field5);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field6", RolesDetail.Field6);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field7", RolesDetail.Field7);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field8", RolesDetail.Field8);
                DT1.Rows.Add("IT.UserRolesDetail", "0", "Field9", RolesDetail.Field9);

                DT2.Rows.Add("IT.UserRoles", "cond", "RoleID", ID);
                DT2.Rows.Add("IT.UserRoles", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateRolesDetail(RolesDetail RolesDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("IT.UserRolesDetail", "cond", "RoleID", ID);
                DT1.Rows.Add("IT.UserRolesDetail", "cond", "ModuleID", RolesDetail.prevModuleID);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "ModuleID", RolesDetail.ModuleID);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Access", RolesDetail.Access);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "FactBox", RolesDetail.FactBox);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field1", RolesDetail.Field1);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field2", RolesDetail.Field2);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field3", RolesDetail.Field3);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field4", RolesDetail.Field4);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field5", RolesDetail.Field5);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field6", RolesDetail.Field6);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field7", RolesDetail.Field7);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field8", RolesDetail.Field8);
                DT1.Rows.Add("IT.UserRolesDetail", "set", "Field9", RolesDetail.Field9);

                DataTable count = Gears.RetriveData2("SELECT * FROM IT.UserRolesDetail WHERE RoleID = '" + ID + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("IT.UserRoles", "cond", "RoleID", ID);
                    DT2.Rows.Add("IT.UserRoles", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                } 


                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteRolesDetail(RolesDetail RolesDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("IT.UserRolesDetail", "cond", "RoleID", RolesDetail.RoleID);
                DT1.Rows.Add("IT.UserRolesDetail", "cond", "ModuleID", RolesDetail.ModuleID);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM IT.UserRolesDetail WHERE RoleID = '" + ID + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("IT.UserRoles", "cond", "RoleID", ID);
                    DT2.Rows.Add("IT.UserRoles", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }                
            }
        }

        public DataTable getdata(string RoleID, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from IT.UserRoles where RoleID = '" + RoleID + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                RoleID = dtRow["RoleID"].ToString();
                Description = dtRow["Description"].ToString();
                PanelName = dtRow["PanelName"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                DeactivatedDate = dtRow["DeactivatedDate"].ToString();

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

            return a;
        }
        public void InsertData(RoleAndAccessControl _ent)
        {
            ID = _ent.RoleID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.UserRoles", "0", "RoleID", _ent.RoleID);
            DT1.Rows.Add("IT.UserRoles", "0", "Description", _ent.Description);
            DT1.Rows.Add("IT.UserRoles", "0", "PanelName", _ent.PanelName);
            DT1.Rows.Add("IT.UserRoles", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("IT.UserRoles", "0", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("IT.UserRoles", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.UserRoles", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.UserRoles", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.UserRoles", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.UserRoles", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.UserRoles", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.UserRoles", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.UserRoles", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.UserRoles", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("IT.UserRoles", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.UserRoles", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(RoleAndAccessControl _ent)
        {
            ID = _ent.RoleID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("IT.UserRoles", "cond", "RoleID", ID);
            DT1.Rows.Add("IT.UserRoles", "set", "Description", _ent.Description);
            DT1.Rows.Add("IT.UserRoles", "set", "PanelName", _ent.PanelName);
            DT1.Rows.Add("IT.UserRoles", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("IT.UserRoles", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("IT.UserRoles", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("IT.UserRoles", "set", "LastEditedDate", _ent.LastEditedDate);

            DT1.Rows.Add("IT.UserRoles", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("IT.UserRoles", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("IT.UserRoles", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("IT.UserRoles", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("IT.UserRoles", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("IT.UserRoles", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("IT.UserRoles", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("IT.UserRoles", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("IT.UserRoles", "set", "Field9", _ent.Field9);



            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFROLE", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

        }
        public void DeleteData(RoleAndAccessControl _ent)
        {
            ID = _ent.RoleID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.UserRoles", "cond", "RoleID", _ent.RoleID);
            Gears.DeleteData(DT1, _ent.Connection);

            DataTable count = Gears.RetriveData2("SELECT * FROM IT.UserRolesDetail WHERE RoleID = '" + ID + "'", _ent.Connection);

            if (count.Rows.Count > 1)
            {
                DT1.Rows.Add("IT.UserRolesDetail", "cond", "RoleID", _ent.RoleID);
                Gears.DeleteData(DT1, _ent.Connection);
            }

            Functions.AuditTrail("REFROLE", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
