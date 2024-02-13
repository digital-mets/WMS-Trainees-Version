using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Adjustment
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        public virtual string AdjustmentCode { get; set; }
        public virtual string TransType { get; set; }
        public virtual string Description { get; set; }
        public virtual string AccountCode { get; set; }
        public virtual string SubsiCode { get; set; }
        public virtual bool IsInactive { get; set; }
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
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public DataTable getdata(string MFAdjustment, string Conn) //Ter
        {
            DataTable a;

            if (MFAdjustment != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.AdjustmentType where AdjustmentCode = '" + MFAdjustment + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    AdjustmentCode = dtRow["AdjustmentCode"].ToString();
                    TransType = dtRow["TransType"].ToString();
                    Description = dtRow["Description"].ToString();
                    AccountCode = dtRow["AccountCode"].ToString();
                    SubsiCode = dtRow["SubsiCode"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();

                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn); //Ter
            }
            return a;
        }
        public void InsertData(Adjustment _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "AdjustmentCode", _ent.AdjustmentCode);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "TransType", _ent.TransType);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "SubsiCode", _ent.SubsiCode);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
          
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.AdjustmentType", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER

            Functions.AuditTrail("REFADJUSTMENT", _ent.AdjustmentCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // Ter
        }

        public void UpdateData(Adjustment _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.AdjustmentType", "cond", "AdjustmentCode", _ent.AdjustmentCode);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "TransType", _ent.TransType);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "SubsiCode", _ent.SubsiCode);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.AdjustmentType", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1,_ent.Connection); // Ter

            Functions.AuditTrail("REFADJUSTMENT", _ent.AdjustmentCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter
        }
        public void DeleteData(Adjustment _ent)
        {
            Conn = _ent.Connection; //Ter
            AdjustmentCode = _ent.AdjustmentCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.AdjustmentType", "cond", "AdjustmentCode", _ent.AdjustmentCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFADJUSTMENT", _ent.AdjustmentCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
