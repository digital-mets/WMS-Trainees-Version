using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class EmployeeMaterfile
    {
        private static string Conn;     // ADD CONN
        public virtual string Connection { get; set; }      // ADD CONN
        private static string Employee;
        public virtual string EmployeeCode { get; set; }
        public virtual string EmployeeID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address { get; set; }
        public virtual string DateOfBirth { get; set; }
        public virtual string SSSNo { get; set; }

        public virtual string TIN { get; set; }

        public virtual string HDMF { get; set; }

        public virtual string PhilHealth { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual string EntityID { get; set; }

         public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }


        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
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

   


        public DataTable getdata(string EmployeeCode, string Conn)
        {
            DataTable a;

            // if (DocNumber != null)
            // {
            a = Gears.RetriveData2("select * from Masterfile.BPEmployeeInfo where EmployeeCode = '" + EmployeeCode + "' ", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                EmployeeCode = dtRow["EmployeeCode"].ToString();
                EmployeeID = dtRow["EmployeeID"].ToString();
                FirstName = dtRow["FirstName"].ToString();
                MiddleName = dtRow["MiddleName"].ToString();
                LastName = dtRow["LastName"].ToString();
                Address = dtRow["Address"].ToString();
                DateOfBirth = dtRow["DateOfBirth"].ToString();
                SSSNo = dtRow["SSSNo"].ToString();
                TIN = dtRow["TIN"].ToString();
                HDMF = dtRow["HDMF"].ToString();
                PhilHealth = dtRow["PhilHealth"].ToString();
                ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                CostCenterCode = dtRow["CostCenterCode"].ToString();
                EntityID = dtRow["EntityID"].ToString();
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
               
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
                DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                DeactivatedDate = dtRow["DeactivatedDate"].ToString();
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
        public void InsertData(EmployeeMaterfile _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Employee = _ent.EmployeeID;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "EmployeeCode", _ent.EmployeeCode);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "EmployeeID", _ent.EmployeeID);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "FirstName", _ent.FirstName);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "MiddleName", _ent.MiddleName);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "LastName", _ent.LastName);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "DateOfBirth", _ent.DateOfBirth);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "SSSNo", _ent.SSSNo);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "TIN", _ent.TIN);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "HDMF", _ent.HDMF);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "PhilHealth", _ent.PhilHealth);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "CostCenterCode", _ent.CostCenterCode);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "EntityID", _ent.EntityID);
            
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "Field9", _ent.Field9);
         
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(EmployeeMaterfile _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Employee = _ent.EmployeeID;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "cond", "EmployeeID", _ent.EmployeeID);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "EmployeeCode", _ent.EmployeeCode);


            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "FirstName", _ent.FirstName);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "MiddleName", _ent.MiddleName);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "LastName", _ent.LastName);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "DateOfBirth", _ent.DateOfBirth);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "SSSNo", _ent.SSSNo);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "TIN", _ent.TIN);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "HDMF", _ent.HDMF);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "PhilHealth", _ent.PhilHealth);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "EntityID", _ent.EntityID);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
        


            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFEMPLOYEE", Employee, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

        }
        public void DeleteData(EmployeeMaterfile _ent)
        {
            Conn = _ent.Connection; //ADD CONN
            Employee = _ent.EmployeeID;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BPEmployeeInfo", "cond", "EmployeeID", _ent.EmployeeID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFEMPLOYEE", Employee, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
