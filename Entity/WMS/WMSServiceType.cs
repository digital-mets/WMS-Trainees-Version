using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class WMSServiceType
    {
    private static string Conn { get; set; }
    public virtual string Connection { get; set; }
    //public static string DocNumber;
    public virtual string ServiceType { get; set; }
    public virtual string Type { get; set; }
    public virtual string Description { get; set; }
    public virtual string SequenceNumber { get; set; }
    public virtual decimal ServiceRate { get; set; }
    public virtual string ServiceTypeCatCode { get; set; }
    public virtual string SalesGLCode { get; set; }
    public virtual string SalesGLSubsiCode { get; set; }
    public virtual string ARGLCode { get; set; }
    public virtual string ARGLSubsiCode { get; set; }
    public virtual bool IsStandard { get; set; }
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
	public virtual string AddedBy{ get; set; }
	public virtual string AddedDate{ get; set; }
	public virtual string LastEditedBy{ get; set; }
	public virtual string LastEditedDate{ get; set; }
    public virtual string ActivatedBy { get; set; }
    public virtual string ActivatedDate { get; set; }
    public virtual string DeactivatedBy { get; set; }
    public virtual string DeactivatedDate { get; set; }


    public DataTable getdata(string SType, string Conn)
        {
            DataTable a;
            if (SType != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.WMSServiceType where ServiceType = '" + SType + "'", Conn); //KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {
                    ServiceType = dtRow["ServiceType"].ToString();
                    Type = dtRow["Type"].ToString();
                    Description = dtRow["Description"].ToString();
                    SequenceNumber = dtRow["SequenceNumber"].ToString();
                    ServiceRate = Convert.ToDecimal(dtRow["ServiceRate"].ToString());
                    ServiceTypeCatCode = dtRow["ServiceTypeCatCode"].ToString();
                    SalesGLCode = dtRow["SalesGLCode"].ToString();
                    SalesGLSubsiCode = dtRow["SalesGLSubsiCode"].ToString();
                    ARGLCode = dtRow["ARGLCode"].ToString();
                    ARGLSubsiCode = dtRow["ARGLSubsiCode"].ToString();
                    IsStandard = Convert.ToBoolean(dtRow["IsStandard"].ToString());
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
                    DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                    DeactivatedDate = dtRow["DeactivatedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(WMSServiceType _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            //Docnum = _ent.DocNumber;
            //string "Masterfile.WMSServiceType" = "Masterfile.WMSServiceType";
            //string "0"1 = "0";
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "SequenceNumber", _ent.SequenceNumber);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ServiceRate", _ent.ServiceRate);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ServiceTypeCatCode", _ent.ServiceTypeCatCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "SalesGLCode", _ent.SalesGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "SalesGLSubsiCode", _ent.SalesGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ARGLCode", _ent.ARGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "ARGLSubsiCode", _ent.ARGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "IsStandard", _ent.IsStandard);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.WMSServiceType", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Masterfile.WMSServiceType", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSTYPE", _ent.ServiceType, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // KMM add Conn
        }

        public void UpdateData(WMSServiceType _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            //Docnum = _ent.DocNumber;
            //string "Masterfile.WMSServiceType" = "Masterfile.WMSServiceType";
            //string "0" = "set";

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.WMSServiceType", "cond", "ServiceType", _ent.ServiceType);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "SequenceNumber", _ent.SequenceNumber);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ServiceRate", _ent.ServiceRate);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ServiceTypeCatCode", _ent.ServiceTypeCatCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "SalesGLCode", _ent.SalesGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "SalesGLSubsiCode", _ent.SalesGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ARGLCode", _ent.ARGLCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "ARGLSubsiCode", _ent.ARGLSubsiCode);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "IsStandard", _ent.IsStandard);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.WMSServiceType", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("WMSTYPE", ServiceType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }

        public void DeleteData(WMSServiceType _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.WMSServiceType", "cond", "ServiceType", _ent.ServiceType);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSTYPE", ServiceType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn



        }
    }
}
