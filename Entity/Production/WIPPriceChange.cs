using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class WIPPriceChange
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string JONumber { get; set; }
        public virtual string WIPNo { get; set; }

        public virtual string TransType { get; set; }
        public virtual string WorkCenter { get; set; }
        public virtual string Step { get; set; }
        public virtual decimal OldWorkOrderPrice { get; set; }
        public virtual decimal NewWorkOrderPrice { get; set; }  
        public virtual decimal Qty { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from Production.WIPPriceChange where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();

                DocDate = dtRow["DocDate"].ToString();
                WIPNo = dtRow["WIPNo"].ToString();
                JONumber = dtRow["JONumber"].ToString();
                Step = dtRow["Step"].ToString();

                WorkCenter = dtRow["WorkCenter"].ToString();

                OldWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["OldWorkOrderPrice"]) ? 0 : dtRow["OldWorkOrderPrice"]);
                NewWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewWorkOrderPrice"]) ? 0 : dtRow["NewWorkOrderPrice"]);
                Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                Remarks = dtRow["Remarks"].ToString();
                TransType = dtRow["Transtype"].ToString();
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
            
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
            }

            return a;
        }

        public class RefTransaction
        {
            public virtual WIPPriceChange Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDWPC' OR  A.TransType='PRDWPC') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(WIPPriceChange _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



            DT1.Rows.Add("Production.WIPPriceChange", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "WIPNo", _ent.WIPNo);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Step", _ent.Step);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "OldWorkOrderPrice", _ent.OldWorkOrderPrice);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "NewWorkOrderPrice", _ent.NewWorkOrderPrice);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Qty", _ent.Qty);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.WIPPriceChange", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(WIPPriceChange _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.WIPPriceChange", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "WIPNo", _ent.WIPNo);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Step", _ent.Step);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "OldWorkOrderPrice", _ent.OldWorkOrderPrice);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "NewWorkOrderPrice", _ent.NewWorkOrderPrice);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Qty", _ent.Qty);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "Transtype", _ent.TransType);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.WIPPriceChange", "set", "LastEditedDate",_ent.LastEditedDate);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDWPC", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(WIPPriceChange _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.WIPPriceChange", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDWPC", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public class JournalEntry
        {
            public virtual WIPPriceChange Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit,A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDWPC') ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
    }
}
