using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BizPartnerEnt
    {

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string BusinessPartnerCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string TIN { get; set; }
        public virtual string ContactNumber { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string BusinessAccountCode { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual bool IsSupplier { get; set; }
        public virtual bool IsCustomer { get; set; }
        public virtual bool IsEmployee { get; set; }
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
        public DataTable getdata(string BizPartnerCode, string Conn)
        {
            DataTable a;

            if (BizPartnerCode != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.BizPartner where BizPartnerCode = '" + BizPartnerCode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    BusinessPartnerCode = dtRow["BizPartnerCode"].ToString();
                    Name = dtRow["Name"].ToString();
                    Address = dtRow["Address"].ToString();
                    ContactPerson = dtRow["ContactPerson"].ToString();
                    TIN = dtRow["TIN"].ToString();
                    ContactNumber = dtRow["ContactNumber"].ToString();
                    EmailAddress = dtRow["EmailAddress"].ToString();
                    BusinessAccountCode = dtRow["BusinessAccountCode"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    IsSupplier = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSupplier"]) ? false : dtRow["IsSupplier"]);
                    IsCustomer = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsCustomer"]) ? false : dtRow["IsCustomer"]);
                    IsEmployee = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsEmployee"]) ? false : dtRow["IsEmployee"]); 
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
        public void InsertData(BizPartnerEnt _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.BizPartner", "0", "BizPartnerCode", _ent.BusinessPartnerCode);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "Name", _ent.Name);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "TIN", _ent.TIN);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "ContactNumber", _ent.ContactNumber);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "EmailAddress", _ent.EmailAddress);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "BusinessAccountCode", _ent.BusinessAccountCode);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "IsSupplier", _ent.IsSupplier);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "IsCustomer", _ent.IsCustomer);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "IsEmployee", _ent.IsEmployee);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.BizPartner", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
   
            DT1.Rows.Add("Masterfile.BizPartner", "0", "PalletLength", "12");


            Gears.CreateData(DT1, _ent.Connection);

            DataTable dtuser = Gears.RetriveData2("SELECT MAX(UserID) as UserID  FROM it.usersdetail WHERE RoleID='DOCS HEAD' ", Conn);
            

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

            DT2.Rows.Add("IT.FuncGroup", "0", "FuncGroupID", _ent.BusinessPartnerCode);
            DT2.Rows.Add("IT.FuncGroup", "0", "Description", _ent.Name);
            DT2.Rows.Add("IT.FuncGroup", "0", "Head", dtuser.Rows[0][0].ToString());



            Gears.CreateData(DT2, _ent.Connection);
        }

        public void UpdateData(BizPartnerEnt _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BizPartner", "cond", "BizPartnerCode", _ent.BusinessPartnerCode);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "Name", _ent.Name);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "TIN", _ent.TIN);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "ContactNumber", _ent.ContactNumber);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "EmailAddress", _ent.EmailAddress);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "BusinessAccountCode", _ent.BusinessAccountCode);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "IsSupplier", _ent.IsSupplier);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "IsCustomer", _ent.IsCustomer);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "IsEmployee", _ent.IsEmployee);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.BizPartner", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.BizPartner", "set", "PalletLength", "12");


            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("BIZPART", _ent.BusinessPartnerCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(BizPartnerEnt _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BizPartner", "cond", "BizPartnerCode", _ent.BusinessPartnerCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("BIZPART", _ent.BusinessPartnerCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
