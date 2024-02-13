using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{   
    public class JVTemplate
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        //public virtual string Prefix { get; set; }
        public virtual string DocDate { get; set; }
        //public virtual string TemplateType { get; set; }
        public virtual decimal TotalDebit { get; set; }
        public virtual decimal TotalCredit { get; set; }
        public virtual string Description { get; set; }
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
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }

        public virtual IList<JVTemplateDetail> Detail { get; set; }


        public class JVTemplateDetail
        {
            public virtual JVTemplateDetail Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string SubsiCode { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual string BizPartnerCode { get; set; }
            public virtual decimal Debit { get; set; }
            public virtual decimal Credit { get; set; }


            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)//KMM
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Accounting.JVTemplateDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);//KMM add Conn
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddJVTemplateDetail(JVTemplateDetail JVTemplateDetail)
            {
                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.JVTemplateDetail where docnumber = '" + Docnum + "'", Conn);//KMM add Conn

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "AccountCode", JVTemplateDetail.AccountCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "SubsiCode", JVTemplateDetail.SubsiCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "ProfitCenterCode", JVTemplateDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "CostCenterCode", JVTemplateDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "BizPartnerCode", JVTemplateDetail.BizPartnerCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Debit ", JVTemplateDetail.Debit);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Credit", JVTemplateDetail.Credit);
   

                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field1", JVTemplateDetail.Field1);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field2", JVTemplateDetail.Field2);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field3", JVTemplateDetail.Field3);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field4", JVTemplateDetail.Field4);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field5", JVTemplateDetail.Field5);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field6", JVTemplateDetail.Field6);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field7", JVTemplateDetail.Field7);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field8", JVTemplateDetail.Field8);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "0", "Field9", JVTemplateDetail.Field9);

                DT2.Rows.Add("Accounting.JVTemplate", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.JVTemplate", "set", "IsWithDetail", "True");


                Gears.CreateData(DT1, Conn);//KMM add Conn
                Gears.UpdateData(DT2, Conn);//KMM add Conn

               

            }
            public void UpdateJVTemplateDetail(JVTemplateDetail JVTemplateDetail)
            {

                

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.JVTemplateDetail", "cond", "DocNumber", JVTemplateDetail.DocNumber);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "cond", "LineNumber", JVTemplateDetail.LineNumber);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "AccountCode", JVTemplateDetail.AccountCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "SubsiCode", JVTemplateDetail.SubsiCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "ProfitCenterCode", JVTemplateDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "CostCenterCode", JVTemplateDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "BizPartnerCode", JVTemplateDetail.BizPartnerCode);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Debit ", JVTemplateDetail.Debit);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Credit", JVTemplateDetail.Credit);
   

                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field1", JVTemplateDetail.Field1);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field2", JVTemplateDetail.Field2);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field3", JVTemplateDetail.Field3);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field4", JVTemplateDetail.Field4);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field5", JVTemplateDetail.Field5);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field6", JVTemplateDetail.Field6);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field7", JVTemplateDetail.Field7);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field8", JVTemplateDetail.Field8);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "set", "Field9", JVTemplateDetail.Field9);

                Gears.UpdateData(DT1, Conn);//KMM add Conn
            }
            public void DeleteJVTemplateDetail(JVTemplateDetail JVTemplateDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                
                DT1.Rows.Add("Accounting.JVTemplateDetail", "cond", "DocNumber", JVTemplateDetail.DocNumber);
                DT1.Rows.Add("Accounting.JVTemplateDetail", "cond", "LineNumber", JVTemplateDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);//KMM add Conn


                DataTable count = Gears.RetriveData2("select * from Accounting.JVTemplateDetail where docnumber = '" + Docnum + "'", Conn);//KMM add Conn

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.JVTemplateDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.JVTemplateDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//KMM add Conn
                }

            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.JVTemplate where DocNumber = '" + DocNumber + "'", Conn); //KMM add Conn
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                //Prefix = dtRow["Prefix"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                Description = dtRow["Description"].ToString();
                TotalDebit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalDebit"]) ? false : dtRow["TotalDebit"]);
                TotalCredit = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCredit"]) ? false : dtRow["TotalCredit"]);
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

            return a;
        }
        public void InsertData(JVTemplate _ent)
        {

            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.JVTemplate", "0", "DocNumber", _ent.DocNumber);
            //DT1.Rows.Add("Accounting.JVTemplate", "0", "Prefix", _ent.Prefix);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "TotalDebit", _ent.TotalDebit);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "TotalCredit", _ent.TotalCredit);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "IsInactive", _ent.IsInactive);
            

            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.JVTemplate", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.JVTemplate", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }

        public void UpdateData(JVTemplate _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.JVTemplate", "cond", "DocNumber", _ent.DocNumber);
            //DT1.Rows.Add("Accounting.JVTemplate", "set", "Prefix", _ent.Prefix);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "TotalDebit", _ent.TotalDebit);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "TotalCredit", _ent.TotalCredit);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.JVTemplate", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTJVT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        public void DeleteData(JVTemplate _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.JVTemplate", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("ACTJVT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }
    }
}
