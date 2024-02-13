using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Customer
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Bizpartner;
        public virtual string BizPartnerCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string CostCenterCode { get; set; }
        public virtual string DeliveryAddress { get; set; }
        public virtual string SalesmanCode { get; set; }
       
        public virtual string Currency { get; set; }
        //public virtual string ARTerm { get; set; }
        public virtual bool IsForCounter { get; set; }
        public virtual string TotalAR { get; set; }
       // public virtual string ItemCategoryCodeCode { get; set; }
        public virtual string Status { get; set; }
        public virtual string TaxCode { get; set; }
        public virtual string ATCCode { get; set; }
        public virtual bool WithInvoice { get; set; }
        public virtual bool WithholdingTaxAgent { get; set; }
        public virtual bool IsInactive { get; set; }
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
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public virtual IList<BPBankInfo> Detail1 { get; set; }
        public virtual IList<BPCustomerTerm> Detail { get; set; }

        public class BPBankInfo
        {
            public virtual Customer Parent { get; set; }

            public virtual string BizPartnerCode { get; set; }
            public virtual string BankCode { get; set; }
            public virtual string AccountNo { get; set; }
            public virtual string Branch { get; set; }
            public virtual string ImageFileName { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getdetail(string docnumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Masterfile.BPBankInfo where BizPartnerCode='" + docnumber + "' order by RecordId", Conn);//KMM add Conn
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddBPBankInfo(BPBankInfo BPBankInfo)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "BizPartnerCode", Bizpartner);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "BankCode", BPBankInfo.BankCode);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "AccountNo", BPBankInfo.AccountNo);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Branch", BPBankInfo.Branch);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "ImageFileName", BPBankInfo.ImageFileName);


                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field1", BPBankInfo.Field1);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field2", BPBankInfo.Field2);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field3", BPBankInfo.Field3);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field4", BPBankInfo.Field4);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field5", BPBankInfo.Field5);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field6", BPBankInfo.Field6);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field7", BPBankInfo.Field7);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field8", BPBankInfo.Field8);
                DT1.Rows.Add("Masterfile.BPBankInfo", "0", "Field9", BPBankInfo.Field9);

                DT2.Rows.Add("Masterfile.BPCustomerInfo", "cond", "BizPartnerCode", BizPartnerCode);
                DT2.Rows.Add("Masterfile.BPCustomerInfo", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);//KMM add Conn
                Gears.UpdateData(DT2, Conn);//KMM add Conn



            }
            public void UpdateBPBankInfo(BPBankInfo BPBankInfo)
            {



                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.BPBankInfo", "cond", "BizPartnerCode", Bizpartner);
                DT1.Rows.Add("Masterfile.BPBankInfo", "cond", "BankCode", BPBankInfo.BankCode);
                DT1.Rows.Add("Masterfile.BPBankInfo", "cond", "AccountNo", BPBankInfo.AccountNo);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Branch", BPBankInfo.Branch);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "ImageFileName", BPBankInfo.ImageFileName);


                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field1", BPBankInfo.Field1);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field2", BPBankInfo.Field2);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field3", BPBankInfo.Field3);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field4", BPBankInfo.Field4);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field5", BPBankInfo.Field5);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field6", BPBankInfo.Field6);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field7", BPBankInfo.Field7);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field8", BPBankInfo.Field8);
                DT1.Rows.Add("Masterfile.BPBankInfo", "set", "Field9", BPBankInfo.Field9);

                Gears.UpdateData(DT1, Conn);//KMM add Conn
            }
            public void DeleteBPBankInfo(BPBankInfo BPBankInfo)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.BPBankInfo", "cond", "BizPartnerCode", BPBankInfo.BizPartnerCode);
                DT1.Rows.Add("Masterfile.BPBankInfo", "cond", "BankCode", BPBankInfo.BankCode);
                DT1.Rows.Add("Masterfile.BPBankInfo", "cond", "AccountNo", BPBankInfo.AccountNo);

                Gears.DeleteData(DT1, Conn);//KMM add Conn


                DataTable count = Gears.RetriveData2("select * from Masterfile.BPBankInfo where BizPartnerCode = '" + Bizpartner + "'", Conn);//KMM add Conn

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Masterfile.BPCustomerInfo", "cond", "BizPartnerCode", Bizpartner);
                    DT2.Rows.Add("Masterfile.BPCustomerInfo", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//KMM add Conn
                }

            }
        }

        


        public class BPCustomerTerm
        {
            public virtual Customer Parent { get; set; }
            
            public virtual string BizPartnerCode { get; set; }
            public virtual string ItemCategoryCode { get; set; }
            public virtual int ARTerms { get; set; }
            public virtual int DeclaredTerms { get; set; }
            public virtual decimal Markup { get; set; }
            public virtual decimal ARBalance { get; set; }
            public virtual decimal ARCreditLimit { get; set; }
            public virtual string Notes { get; set; }

             public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getdetail(string docnumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Masterfile.BPCustomerTerm where BizPartnerCode='" + docnumber + "' order by RecordId", Conn);//KMM add Conn
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddBPCustomerTerm(BPCustomerTerm BPCustomerTerm)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "BizPartnerCode", Bizpartner);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "ItemCategoryCode", BPCustomerTerm.ItemCategoryCode);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "ARTerms", BPCustomerTerm.ARTerms);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "DeclaredTerms", BPCustomerTerm.DeclaredTerms);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Markup", BPCustomerTerm.Markup);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "ARBalance", BPCustomerTerm.ARBalance);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "ARCreditLimit", BPCustomerTerm.ARCreditLimit);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Notes", BPCustomerTerm.Notes);

                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field1", BPCustomerTerm.Field1);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field2", BPCustomerTerm.Field2);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field3", BPCustomerTerm.Field3);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field4", BPCustomerTerm.Field4);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field5", BPCustomerTerm.Field5);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field6", BPCustomerTerm.Field6);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field7", BPCustomerTerm.Field7);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field8", BPCustomerTerm.Field8);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "0", "Field9", BPCustomerTerm.Field9);

                DT2.Rows.Add("Masterfile.BPCustomerInfo", "cond", "BizPartnerCode", BizPartnerCode);
                DT2.Rows.Add("Masterfile.BPCustomerInfo", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);//KMM add Conn
                Gears.UpdateData(DT2, Conn);//KMM add Conn



            }
            public void UpdateBPCustomerTerm(BPCustomerTerm BPCustomerTerm)
            {



                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "cond", "BizPartnerCode", BPCustomerTerm.BizPartnerCode);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "ItemCategoryCode", BPCustomerTerm.ItemCategoryCode);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "ARTerms", BPCustomerTerm.ARTerms);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "DeclaredTerms", BPCustomerTerm.DeclaredTerms);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Markup", BPCustomerTerm.Markup);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "ARBalance", BPCustomerTerm.ARBalance);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "ARCreditLimit", BPCustomerTerm.ARCreditLimit);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Notes", BPCustomerTerm.Notes);

                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field1", BPCustomerTerm.Field1);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field2", BPCustomerTerm.Field2);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field3", BPCustomerTerm.Field3);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field4", BPCustomerTerm.Field4);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field5", BPCustomerTerm.Field5);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field6", BPCustomerTerm.Field6);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field7", BPCustomerTerm.Field7);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field8", BPCustomerTerm.Field8);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "set", "Field9", BPCustomerTerm.Field9);

                Gears.UpdateData(DT1, Conn);//KMM add Conn
            }
            public void DeleteBPCustomerTerm(BPCustomerTerm BPCustomerTerm)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.BPCustomerTerm", "cond", "BizPartnerCode", BPCustomerTerm.BizPartnerCode);
                DT1.Rows.Add("Masterfile.BPCustomerTerm", "cond", "SubsiCode", BPCustomerTerm.ItemCategoryCode);


                Gears.DeleteData(DT1, Conn);//KMM add Conn


                DataTable count = Gears.RetriveData2("select * from Masterfile.BPCustomerTerm where BizPartnerCode = '" + Bizpartner + "'", Conn);//KMM add Conn

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Masterfile.BPCustomerInfo", "cond", "BizPartnerCode", Bizpartner);
                    DT2.Rows.Add("Masterfile.BPCustomerInfo", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//KMM add Conn
                }

            }
        }


        public DataTable getdata(string Cus, string Conn)
        {
            DataTable a;

            if (Cus != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.BPCustomerInfo where BizPartnerCode = '" + Cus + "'", Conn); //KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {

                    BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                    Name = dtRow["Name"].ToString();
                    ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                    CostCenterCode = dtRow["CostCenterCode"].ToString();
                    DeliveryAddress = dtRow["DeliveryAddress"].ToString();
                    SalesmanCode = dtRow["SalesmanCode"].ToString();
                    //BankAccountCode = dtRow["BankAccountCode"].ToString();
                    //AccountNumber = dtRow["AccountNumber"].ToString();
                    //Branch = dtRow["Branch"].ToString();
                    //SpecimenSignature = dtRow["SpecimenSignature"].ToString();
                    Currency = dtRow["Currency"].ToString();
                    //ARTerm = dtRow["ARTerm"].ToString();
                    IsForCounter = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsForCounter"]) ? false : dtRow["IsForCounter"]);
                    TotalAR = dtRow["TotalAR"].ToString();
                    //ItemCategoryCode = dtRow["ItemCategoryCode"].ToString();
                    WithholdingTaxAgent = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithholdingTaxAgent"]) ? false : dtRow["IsWithholdingTaxAgent"]);
                    Status = dtRow["Status"].ToString();
                    TaxCode = dtRow["TaxCode"].ToString();
                    ATCCode = dtRow["ATCCOde"].ToString();
                    WithInvoice = Convert.ToBoolean(Convert.IsDBNull(dtRow["WithInvoice"]) ? false : dtRow["WithInvoice"]);
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
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }

            }

            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(Customer _ent)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);

            Conn = _ent.Connection;//ADD CONN
            Bizpartner = _ent.BizPartnerCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Name", _ent.Name);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "DeliveryAddress", _ent.DeliveryAddress);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "SalesmanCode", _ent.SalesmanCode);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "BankAccountCode", _ent.BankAccountCode);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "AccountNumber", _ent.AccountNumber);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Branch", _ent.Branch);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "SpecimenSignature", _ent.SpecimenSignature);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Currency", _ent.Currency);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "ARTerm", _ent.ARTerm);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "IsForCounter", _ent.IsForCounter);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "TotalAR", _ent.TotalAR);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "IsWithholdingTaxAgent", _ent.WithholdingTaxAgent);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "ItemCategoryCode", _ent.ItemCategoryCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Status", _ent.Status);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "TaxCode", _ent.TaxCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "ATCCode", _ent.ATCCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "WithInvoice", _ent.WithInvoice);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "0", "BookDate", startDate.ToString("yyyy-MM-dd"));
           


            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn

            Functions.AuditTrail("REFCUS", _ent.BizPartnerCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection); // KMM add Conn
        }

        public void UpdateData(Customer _ent)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);

            
            
            Bizpartner = _ent.BizPartnerCode;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "cond", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Name", _ent.Name);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "CostCenterCode", _ent.CostCenterCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "DeliveryAddress", _ent.DeliveryAddress);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "SalesmanCode", _ent.SalesmanCode);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "BankAccountCode", _ent.BankAccountCode);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "AccountNumber", _ent.AccountNumber);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Branch", _ent.Branch);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "SpecimenSignature", _ent.SpecimenSignature);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Currency", _ent.Currency);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "ARTerm", _ent.ARTerm);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "IsForCounter", _ent.IsForCounter);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "TotalAR", _ent.TotalAR);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "ItemCategoryCode", _ent.ItemCategoryCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "IsWithholdingTaxAgent", _ent.WithholdingTaxAgent);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Status", _ent.Status);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "TaxCode", _ent.TaxCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "ATCCode", _ent.ATCCode);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "WithInvoice", _ent.WithInvoice);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "IsInactive", _ent.IsInactive);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "set", "BookDate", startDate.ToString("yyyy-MM-dd"));
           
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFCUS", BizPartnerCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        
        public void DeleteData(Customer _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BPCustomerInfo", "cond", "BizPartnerCode", _ent.BizPartnerCode);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("REFCUS", BizPartnerCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn



        }
    }
}
