using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ServiceBilling
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string RefNumber { get; set; }
        public virtual string DueDate { get; set; }
        public virtual decimal Term { get; set; }
        public virtual decimal GrossVatAmount { get; set; }
        public virtual decimal GrossNonVatAmount { get; set; }
        public virtual decimal VATAmount { get; set; }
        public virtual decimal WithholdingTax { get; set; }
        public virtual decimal NetAmount { get; set; }
        public virtual string Remarks { get; set; }
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
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual IList<ServiceBillingDetail> Detail { get; set; }
        
        #region Detail
        public class ServiceBillingDetail
        {
            public virtual ServiceBilling Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ServiceCode { get; set; }
            public virtual string ServiceDescription { get; set; }
            public virtual string GLAccountCode { get; set; }
            public virtual string GLSubsiCode { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual bool IsVatable { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal VATRate { get; set; }
            public virtual decimal VATAmount { get; set; }
            public virtual bool IsEWT { get; set; }
            public virtual string ATCCode { get; set; }
            public virtual decimal ATCRate { get; set; }
            public virtual decimal ATCAmount { get; set; }
            public virtual decimal Net { get; set; }
            public virtual string Remarks { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.ServiceBillingDetail WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddServiceBillingDetail(ServiceBillingDetail ServiceBillingDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Accounting.ServiceBillingDetail WHERE docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "ServiceCode", ServiceBillingDetail.ServiceCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "ServiceDescription", ServiceBillingDetail.ServiceDescription);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "GLAccountCode", ServiceBillingDetail.GLAccountCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "GLSubsiCode", ServiceBillingDetail.GLSubsiCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "ProfitCenterCode", ServiceBillingDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "CostCenterCode", ServiceBillingDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Amount", ServiceBillingDetail.Amount);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "IsVatable", ServiceBillingDetail.IsVatable);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "VATCode", ServiceBillingDetail.VATCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "VATRate", ServiceBillingDetail.VATRate);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "VATAmount", ServiceBillingDetail.VATAmount);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "IsEWT", ServiceBillingDetail.IsEWT);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "ATCCode", ServiceBillingDetail.ATCCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "ATCRate", ServiceBillingDetail.ATCRate);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "ATCAmount", ServiceBillingDetail.ATCAmount);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Net", ServiceBillingDetail.Net);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Remarks", ServiceBillingDetail.Remarks);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field1", ServiceBillingDetail.Field1);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field2", ServiceBillingDetail.Field2);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field3", ServiceBillingDetail.Field3);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field4", ServiceBillingDetail.Field4);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field5", ServiceBillingDetail.Field5);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field6", ServiceBillingDetail.Field6);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field7", ServiceBillingDetail.Field7);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field8", ServiceBillingDetail.Field8);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Field9", ServiceBillingDetail.Field9);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "0", "Version", "1");

                DT2.Rows.Add("Accounting.ServiceBilling", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.ServiceBilling", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateServiceBillingDetail(ServiceBillingDetail ServiceBillingDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "cond", "LineNumber", ServiceBillingDetail.LineNumber); 
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "ServiceCode", ServiceBillingDetail.ServiceCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "ServiceDescription", ServiceBillingDetail.ServiceDescription);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "GLAccountCode", ServiceBillingDetail.GLAccountCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "GLSubsiCode", ServiceBillingDetail.GLSubsiCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "ProfitCenterCode", ServiceBillingDetail.ProfitCenterCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "CostCenterCode", ServiceBillingDetail.CostCenterCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Amount", ServiceBillingDetail.Amount);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "IsVatable", ServiceBillingDetail.IsVatable);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "VATCode", ServiceBillingDetail.VATCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "VATRate", ServiceBillingDetail.VATRate);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "VATAmount", ServiceBillingDetail.VATAmount);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "IsEWT", ServiceBillingDetail.IsEWT);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "ATCCode", ServiceBillingDetail.ATCCode);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "ATCRate", ServiceBillingDetail.ATCRate);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "ATCAmount", ServiceBillingDetail.ATCAmount);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Net", ServiceBillingDetail.Net);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Remarks", ServiceBillingDetail.Remarks);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field1", ServiceBillingDetail.Field1);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field2", ServiceBillingDetail.Field2);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field3", ServiceBillingDetail.Field3);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field4", ServiceBillingDetail.Field4);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field5", ServiceBillingDetail.Field5);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field6", ServiceBillingDetail.Field6);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field7", ServiceBillingDetail.Field7);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field8", ServiceBillingDetail.Field8);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Field9", ServiceBillingDetail.Field9);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteServiceBillingDetail(ServiceBillingDetail ServiceBillingDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.ServiceBillingDetail", "cond", "LineNumber", ServiceBillingDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);
            }
        }
        #endregion

        #region Header
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT A.*, B.FullName AS Added, C.FullName AS LastEdited, D.FullName AS Submitted, F.FullName AS Posted, G.FullName AS Cancelled "
                + " FROM Accounting.ServiceBilling A LEFT JOIN IT.Users B ON A.AddedBy = B.UserID "
                + " LEFT JOIN IT.Users C ON A.LastEditedBy = C.UserID  LEFT JOIN IT.Users D ON A.SubmittedBy = D.UserID "
                + " LEFT JOIN Masterfile.BizPartner E ON A.CustomerCode = E.BizPartnerCode LEFT JOIN IT.Users F ON A.PostedBy = F.UserID "
                + " LEFT JOIN IT.Users G ON A.CancelledBy = G.UserID WHERE A.DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Name = dtRow["Name"].ToString();
                RefNumber = dtRow["RefNumber"].ToString();
                DueDate = Convert.IsDBNull(dtRow["DueDate"]) || String.IsNullOrEmpty(dtRow["DueDate"].ToString()) ? null : dtRow["DueDate"].ToString();
                Term = Convert.ToDecimal(Convert.IsDBNull(dtRow["Term"]) ? 0 : dtRow["Term"]);
                GrossVatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatAmount"]) ? 0 : dtRow["GrossVatAmount"]);
                GrossNonVatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossNonVatAmount"]) ? 0 : dtRow["GrossNonVatAmount"]);
                VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);
                WithholdingTax = Convert.ToDecimal(Convert.IsDBNull(dtRow["WithholdingTax"]) ? 0 : dtRow["WithholdingTax"]);
                NetAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetAmount"]) ? 0 : dtRow["NetAmount"]);
                Remarks = dtRow["Remarks"].ToString();
                
                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();

                AddedBy = dtRow["Added"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEdited"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["Submitted"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["Cancelled"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                PostedBy = dtRow["Posted"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();             
            }

            return a;
        }
        public void InsertData(ServiceBilling _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.ServiceBilling", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Term", _ent.Term);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "GrossVatAmount", _ent.GrossVatAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "GrossNonVatAmount", _ent.GrossNonVatAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "WithholdingTax", _ent.WithholdingTax);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "NetAmount", _ent.NetAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.ServiceBilling", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.ServiceBilling", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ServiceBilling _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.ServiceBilling", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "RefNumber", _ent.RefNumber);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Term", _ent.Term);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "GrossVatAmount", _ent.GrossVatAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "GrossNonVatAmount", _ent.GrossNonVatAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "WithholdingTax", _ent.WithholdingTax);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "NetAmount", _ent.NetAmount);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.ServiceBilling", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.ServiceBilling", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTSEB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ServiceBilling _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.ServiceBilling", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTSEB", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        #endregion

        #region Journal Entry
        public class JournalEntry
        {
            public virtual ServiceBilling Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTSEB' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        #endregion
    }
}
