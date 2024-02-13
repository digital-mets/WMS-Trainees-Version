using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class UserMaintenance
    {

        private static string ID;
        public virtual string UserID { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FullName { get; set; }

        private static string Conn;
        public virtual string Connection { get; set; }

        public virtual string Password { get; set; }
        public virtual string SavedPW { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string BirthDate { get; set; }
        public virtual string SecurityQuestion { get; set; }
        public virtual string SecurityAnswer { get; set; }
        public virtual string OrgChartEntity { get; set; }
        public virtual bool IsUser { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string UserType { get; set; }
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

        public virtual string ClientCodes { get; set; }

        //public virtual string ActivatedBy { get; set; }
        //public virtual string ActivatedDate { get; set; }
        //public virtual string DeactivatedBy { get; set; }
        //public virtual string DeactivatedDate { get; set; }


        public virtual IList<UserMaintenanceDetail> Detail { get; set; }


        public class UserMaintenanceDetail
        {
            public virtual UserMaintenance Parent { get; set; }
            public virtual string UserID { get; set; }
            public virtual string RoleID { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }


            public DataTable getdetail(string UserID, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from IT.UsersDetail where UserID='" + UserID + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddUserMaintenanceDetail(UserMaintenanceDetail UserMaintenancedcode)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("IT.UsersDetail", "0", "UserID", ID);

                DT1.Rows.Add("IT.UsersDetail", "0", "RoleID", UserMaintenancedcode.RoleID);

                DT1.Rows.Add("IT.UsersDetail", "0", "Field1", UserMaintenancedcode.Field1);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field2", UserMaintenancedcode.Field2);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field3", UserMaintenancedcode.Field3);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field4", UserMaintenancedcode.Field4);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field5", UserMaintenancedcode.Field5);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field6", UserMaintenancedcode.Field6);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field7", UserMaintenancedcode.Field7);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field8", UserMaintenancedcode.Field8);
                DT1.Rows.Add("IT.UsersDetail", "0", "Field9", UserMaintenancedcode.Field9);

                //Gears.RetriveData2();
               
                Gears.CreateData(DT1, Conn);
                Gears.RetriveData2("UPDATE IT.DocNumberSettings  SET SeriesNumber = SeriesNumber + 1  WHERE Module = 'ITUSERS' AND Prefix = '0' ", Conn);
            }
            public void UpdateUserMaintenanceDetail(UserMaintenanceDetail UserMaintenancedcode)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("IT.UsersDetail", "cond", "UserID", ID);
                DT1.Rows.Add("IT.UsersDetail", "set", "RoleID", UserMaintenancedcode.RoleID);

                DT1.Rows.Add("IT.UsersDetail", "set", "Field1", UserMaintenancedcode.Field1);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field2", UserMaintenancedcode.Field2);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field3", UserMaintenancedcode.Field3);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field4", UserMaintenancedcode.Field4);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field5", UserMaintenancedcode.Field5);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field6", UserMaintenancedcode.Field6);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field7", UserMaintenancedcode.Field7);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field8", UserMaintenancedcode.Field8);
                DT1.Rows.Add("IT.UsersDetail", "set", "Field9", UserMaintenancedcode.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteUserMaintenanceDetail(UserMaintenanceDetail UserMaintenancedcode)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("IT.UsersDetail", "cond", "UserID", ID);
                DT1.Rows.Add("IT.UsersDetail", "cond", "RoleID", UserMaintenancedcode.RoleID);
                Gears.DeleteData(DT1, Conn);

                //DataTable count = Gears.RetriveData2("select * from IT.UsersDetail where UserID = '" + ID + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("IT.UsersDetail", "cond", "UserID", ID);
                //   Gears.UpdateData(DT2);
                //}

            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            // if (DocNumber != null)
            // {
            a = Gears.RetriveData2("select * from IT.Users where UserID = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                UserID = dtRow["UserID"].ToString();
                UserName = dtRow["UserName"].ToString();
                FullName = dtRow["FullName"].ToString();

                BirthDate = dtRow["BirthDate"].ToString();
                EmailAddress = dtRow["EmailAddress"].ToString();
                UserType = dtRow["UserType"].ToString();
                Password = dtRow["Password"].ToString();
                SavedPW = Password;
                SecurityQuestion = dtRow["SecurityQuestion"].ToString();
                SecurityAnswer = dtRow["SecurityAnswer"].ToString();
                OrgChartEntity = dtRow["OrgChartEntity"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                CompanyCode = dtRow["CompanyCode"].ToString();
                ClientCodes = dtRow["ClientCodes"].ToString();
                UserType = dtRow["UserType"].ToString();
                IsUser = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsUser"]) ? false : dtRow["IsUser"]);


                AddedBy = dtRow["AddedBy"].ToString();


            }
            //  }
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as OutgoingDocNumber,'' as OutgoingDocType,'' as WarehouseCode,'' as StorerKey" +
            //        ",'' as TargetDate,''  as TargetDate,'' as  DeliveryDate" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(UserMaintenance _ent)
        {
            ID = _ent.UserID;
            Conn = _ent.Connection;

            _ent.Password = Gears.PasswordEncrypt(_ent.UserName, _ent.Password);

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.Users", "0", "UserID", _ent.UserID);
            DT1.Rows.Add("IT.Users", "0", "UserName", _ent.UserName);
            DT1.Rows.Add("IT.Users", "0", "FullName", _ent.FullName);
            DT1.Rows.Add("IT.Users", "0", "UserType", _ent.UserType);
            DT1.Rows.Add("IT.Users", "0", "Password", _ent.Password);
            DT1.Rows.Add("IT.Users", "0", "EmailAddress", _ent.EmailAddress);
            DT1.Rows.Add("IT.Users", "0", "BirthDate", _ent.BirthDate);
            DT1.Rows.Add("IT.Users", "0", "SecurityQuestion", _ent.SecurityQuestion);
            DT1.Rows.Add("IT.Users", "0", "SecurityAnswer", _ent.SecurityAnswer);
            DT1.Rows.Add("IT.Users", "0", "IsUser", _ent.IsUser);
            DT1.Rows.Add("IT.Users", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("IT.Users", "0", "CompanyCode", _ent.CompanyCode);
            DT1.Rows.Add("IT.Users", "0", "OrgChartEntity", _ent.OrgChartEntity);
            DT1.Rows.Add("IT.Users", "0", "UserType", _ent.UserType);
            DT1.Rows.Add("IT.Users", "0", "ClientCodes", _ent.ClientCodes);
            //DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field5", _ent.Field5);
            //DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field6", _ent.Field6);
            //DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field7", _ent.Field7);
            //DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field8", _ent.Field8);
            //DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("IT.Users", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("IT.Users", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(UserMaintenance _ent)
        {
            ID = _ent.UserID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.Users", "cond", "UserID", _ent.UserID);
            DT1.Rows.Add("IT.Users", "set", "UserName", _ent.UserName);
            DT1.Rows.Add("IT.Users", "set", "FullName", _ent.FullName);

            if (_ent.Password != _ent.SavedPW)
            {
                _ent.Password = Gears.PasswordEncrypt(_ent.UserName, _ent.Password);
                DT1.Rows.Add("IT.Users", "set", "Password", _ent.Password);
            }
            DT1.Rows.Add("IT.Users", "set", "EmailAddress", _ent.EmailAddress);
            DT1.Rows.Add("IT.Users", "set", "BirthDate", _ent.BirthDate);
            DT1.Rows.Add("IT.Users", "set", "UserType", _ent.UserType);
            DT1.Rows.Add("IT.Users", "set", "SecurityQuestion", _ent.SecurityQuestion);
            DT1.Rows.Add("IT.Users", "set", "SecurityAnswer", _ent.SecurityAnswer);
            DT1.Rows.Add("IT.Users", "set", "IsUser", _ent.IsUser);
            DT1.Rows.Add("IT.Users", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("IT.Users", "set", "CompanyCode", _ent.CompanyCode);
            DT1.Rows.Add("IT.Users", "set", "ClientCodes", _ent.ClientCodes);
            DT1.Rows.Add("IT.Users", "set", "UserType", _ent.UserType);
            
            DT1.Rows.Add("IT.Users", "set", "OrgChartEntity", _ent.OrgChartEntity);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("USERS", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

        }
        public void DeleteData(UserMaintenance _ent)
        {
            ID = _ent.UserID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.Users", "cond", "UserID", _ent.UserID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("USERS", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
