using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Supplier
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual string Address { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string APAccountCode { get; set; }
        public virtual string Mnemonics { get; set; }
        public virtual bool ShowForPayee { get; set; }
        public virtual string PayeeName { get; set; }
        public virtual string Currency { get; set; }
        public virtual string APTerms { get; set; }
        public virtual string TaxCode { get; set; }
        //public virtual bool WithholdingTaxAgent { get; set; }
        public virtual string ATCCode { get; set; }
        public virtual string WithReferenceNumber { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual bool IsWorkCenter { get; set; }
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
        public DataTable getdata(string SupCode, string Conn)//KMM add Conn
        {
            DataTable a;

            if (SupCode != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.BPSupplierInfo where SupplierCode = '" + SupCode + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {
                    SupplierCode = dtRow["SupplierCode"].ToString();
                    Name = dtRow["Name"].ToString();
                    ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                    CostCenterCode = dtRow["CostCenterCode"].ToString();
                    Address = dtRow["Address"].ToString();
                    ContactPerson = dtRow["ContactPerson"].ToString();
                    APAccountCode = dtRow["APAccountCode"].ToString();
                    Mnemonics = dtRow["Mnemonics"].ToString();
                    ShowForPayee = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsShowForPayee"]) ? false : dtRow["IsShowForPayee"]);
                    PayeeName = dtRow["PayeeName"].ToString();
                    Currency = dtRow["Currency"].ToString();
                    APTerms = dtRow["APTerms"].ToString();
                    TaxCode = dtRow["TaxCode"].ToString();
                    //WithholdingTaxAgent = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithholdingTaxAgent"]) ? false : dtRow["IsWithholdingTaxAgent"]);
                    ATCCode = dtRow["ATCCode"].ToString();
                    WithReferenceNumber = dtRow["WithReferenceNumber"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    IsWorkCenter = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWorkCenter"]) ? false : dtRow["IsWorkCenter"]);
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
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
            return a;
        }
        public void InsertData(Supplier _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Name", _ent.Name);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "APAccountCode", _ent.APAccountCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Mnemonics", _ent.Mnemonics);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "IsShowForPayee", _ent.ShowForPayee);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "PayeeName", _ent.PayeeName);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "APTerms", _ent.APTerms);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "TaxCode", _ent.TaxCode);
            //DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "IsWithholdingTaxAgent", _ent.WithholdingTaxAgent);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "ATCCode", _ent.ATCCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "WithReferenceNumber", _ent.WithReferenceNumber);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "IsWorkCenter", _ent.IsWorkCenter);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);//KMM add Conn
        }

        public void UpdateData(Supplier _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "cond", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Name", _ent.Name);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "APAccountCode", _ent.APAccountCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Mnemonics", _ent.Mnemonics);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "IsShowForPayee", _ent.ShowForPayee);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "PayeeName", _ent.PayeeName);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "APTerms", _ent.APTerms);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "TaxCode", _ent.TaxCode);
            //DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "IsWithholdingTaxAgent", _ent.WithholdingTaxAgent);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "ATCCode", _ent.ATCCode);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "WithReferenceNumber", _ent.WithReferenceNumber);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "IsWorkCenter", _ent.IsWorkCenter);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn

            Functions.AuditTrail("REFSUPPC", _ent.SupplierCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn

            strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn
        }

        public void DeleteData(Supplier _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BPSupplierInfo", "cond", "SupplierCode", _ent.SupplierCode);
            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFSUPPC", _ent.SupplierCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn
        }
    }
}
