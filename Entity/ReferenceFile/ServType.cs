using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ServType
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        public virtual string ServiceType { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual string ServiceRate { get; set; }
        public virtual string ARGLCode { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

        public virtual string SequenceNumber { get; set; }
        public virtual string ServiceTypeCategoryCode { get; set; }
        public virtual string SalesGLCode { get; set; }
        public virtual string SalesGLSubsiCode { get; set; }
        public virtual string ARGLSubsiCode { get; set; }
        public virtual bool IsStandard { get; set; }

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

        public DataTable getdata(string MFServiceType, string Conn) //Ter
        {
            DataTable a;

            if (MFServiceType != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.WMSServiceType where ServiceType = '" + MFServiceType + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    ServiceType = dtRow["ServiceType"].ToString();
                    Description = dtRow["Description"].ToString();
                    Type = dtRow["Type"].ToString();
                    ServiceRate = dtRow["ServiceRate"].ToString();
                    ARGLCode = dtRow["ARGLCode"].ToString();
                    AddedBy= dtRow["AddedBy"].ToString();
                    AddedDate= dtRow["AddedDate"].ToString();
                    LastEditedBy= dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();

                    SequenceNumber = dtRow["SequenceNumber"].ToString();
                    ServiceTypeCategoryCode = dtRow["ServiceTypeCategoryCode"].ToString();
                    SalesGLCode = dtRow["SalesGLCode"].ToString();
                    SalesGLSubsiCode = dtRow["SalesGLSubsiCode"].ToString();
                    ARGLSubsiCode = dtRow["ARGLSubsiCode"].ToString();
                    IsStandard = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsStandard"]) ? false : dtRow["IsStandard"]);

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
        public void InsertData(ServType _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ServiceRate", _ent.ServiceRate);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ARGLCode", _ent.ARGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "SequenceNumber", _ent.SequenceNumber);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ServiceTypeCategoryCode", _ent.ServiceTypeCategoryCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "SalesGLCode", _ent.SalesGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "SalesGLSubsiCode", _ent.SalesGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ARGLSubsiCode", _ent.ARGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "IsStandard", _ent.IsStandard);

            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER

            Functions.AuditTrail("REFSERVTYPE", _ent.ServiceType, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // TER
        }

        public void UpdateData(ServType _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.WMSServiceType", "cond", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ServiceRate", _ent.ServiceRate);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ARGLCode", _ent.ARGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "SequenceNumber", _ent.SequenceNumber);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ServiceTypeCategoryCode", _ent.ServiceTypeCategoryCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "SalesGLCode", _ent.SalesGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "SalesGLSubsiCode", _ent.SalesGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ARGLSubsiCode", _ent.ARGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "IsStandard", _ent.IsStandard);

            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter

            Functions.AuditTrail("REFSERVTYPE", _ent.ServiceType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter
        }
        public void DeleteData(ServType _ent)
        {
            Conn = _ent.Connection; //Ter
            ServiceType = _ent.ServiceType;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.WMSServiceType", "cond", "ServiceType", _ent.ServiceType);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSERVTYPE", _ent.ServiceType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
