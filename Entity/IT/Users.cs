using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GearsLibrary;

namespace Entity
{
    public class Users
    {
        public virtual string UserID { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Password { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual IList<UsersDetail> Detail { get; set; }


        public class UsersDetail 
        {
            public virtual Users Parent { get; set; }
            public virtual string RoleID { get; set; }

            public  DataTable getdetail(string strKey)
            {

                DataTable a = Gears.RetriveData("select * from IT.UsersDetail where UserID ='"+strKey+"'");
                //DataTable a = null;// Gears.RetriveData("select '' as UserID,'' as userName,'' as FullName,'' as Password,'' as EmailAddress "); 
                return a;
            }

            public void AddUsersDetail(UsersDetail usersdetail)
            {
                //Detail.Add(usersdetail);

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("it.Users", "0", "UserID", usersdetail.Parent.UserID);
                DT1.Rows.Add("it.Users", "0", "RoleID", usersdetail.RoleID);

                Gears.CreateData(DT1);

            }
        }

        public virtual void AddUsersDetail(UsersDetail usersdetail)
        {
            usersdetail.Parent = this;
            //Detail.Add(usersdetail);

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("it.Users", "0", "UserID", usersdetail.Parent.UserID);
            DT1.Rows.Add("it.Users", "0", "RoleID", usersdetail.RoleID);

            Gears.CreateData(DT1);

        }

        public static DataTable getdata()
        {

            DataTable a = Gears.RetriveData("select UserID,userName,FullName,Password,EmailAddress from IT.Users"); 
            //DataTable a = null;// Gears.RetriveData("select '' as UserID,'' as userName,'' as FullName,'' as Password,'' as EmailAddress "); 
            return a; 
        }

        


        public void InsertData(Users _ent)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("it.Users", "0", "UserID", _ent.UserID);
            DT1.Rows.Add("it.Users", "0", "UserName", _ent.UserName);
            DT1.Rows.Add("it.Users", "0", "FullName", _ent.FullName);
            DT1.Rows.Add("it.Users", "0", "Password", _ent.Password);
            DT1.Rows.Add("it.Users", "0", "EmailAddress", _ent.EmailAddress);
         

            Gears.CreateData(DT1);

        }

        public void UpdateData(Users _ent)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("it.Users", "cond", "UserID", _ent.UserID);
            DT1.Rows.Add("it.Users", "set", "UserName", _ent.UserName);
            DT1.Rows.Add("it.Users", "set", "FullName", _ent.FullName);
            DT1.Rows.Add("it.Users", "set", "Password", _ent.Password);
            DT1.Rows.Add("it.Users", "set", "EmailAddress", _ent.EmailAddress);
          
            


            string strErr = Gears.UpdateData(DT1);


        }

        
    }


}
