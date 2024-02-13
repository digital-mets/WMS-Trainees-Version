using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class RPApplication
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string BizPartner { get; set; }
        public virtual string Name { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal Adjustment { get; set; }
        public virtual decimal Variance { get; set; }
        public virtual string RefTransDoc { get; set; }
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
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual IList<RPApplicationTag> Application { get; set; }
        public virtual IList<RPApplicationAdj> RPAdjustment { get; set; }

        #region Journal Entry
        public class JournalEntry
        {
            public virtual RPApplication Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTRPA' ", Conn);

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

        #region Reference Transactions
        public class RefTransaction
        {
            public virtual RPApplication Parent { get; set; }
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
                                            + " inner join IT.MainMenu B on A.RMenuID = B.ModuleID inner join IT.MainMenu C on A.MenuID = C.ModuleID "
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='ACTRPA' OR  A.TransType='ACTRPA') ", Conn);
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

        #region Header
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Accounting.RPApplication WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                BizPartner = dtRow["BizPartner"].ToString();
                Name = dtRow["Name"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Adjustment = Convert.ToDecimal(Convert.IsDBNull(dtRow["Adjustment"]) ? 0 : dtRow["Adjustment"]);
                Variance = Convert.ToDecimal(Convert.IsDBNull(dtRow["Variance"]) ? 0 : dtRow["Variance"]);                
                RefTransDoc = dtRow["RefTransDoc"].ToString();              

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
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
            }

            return a;
        }
        public void InsertData(RPApplication _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.RPApplication", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.RPApplication", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.RPApplication", "0", "BizPartner", _ent.BizPartner);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Adjustment", _ent.Adjustment);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Variance", _ent.Variance);
            DT1.Rows.Add("Accounting.RPApplication", "0", "RefTransDoc", _ent.RefTransDoc);

            DT1.Rows.Add("Accounting.RPApplication", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.RPApplication", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.RPApplication", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.RPApplication", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(RPApplication _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.RPApplication", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Accounting.RPApplication", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.RPApplication", "set", "BizPartner", _ent.BizPartner);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Name", _ent.Name);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Adjustment", _ent.Adjustment);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Variance", _ent.Variance);
            DT1.Rows.Add("Accounting.RPApplication", "set", "RefTransDoc", _ent.RefTransDoc);

            DT1.Rows.Add("Accounting.RPApplication", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.RPApplication", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.RPApplication", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.RPApplication", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTRPA", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(RPApplication _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.RPApplication", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.RPApplicationTag", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DT3.Rows.Add("Accounting.RPApplicationAdj", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT3, _ent.Connection);

            Functions.AuditTrail("ACTRPA", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    #endregion

        #region Application
        public class RPApplicationTag
        {
            public virtual RPApplication Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string TransType { get; set; }
            public virtual string TransDoc { get; set; }
            public virtual DateTime TransDate { get; set; }
            public virtual string TransAccountCode { get; set; }
            public virtual string TransSubsiCode { get; set; }
            public virtual string TransProfitCenter { get; set; }
            public virtual string TransCostCenter { get; set; }
            public virtual string TransBizPartnerCode { get; set; }
            public virtual decimal TransAmountDue { get; set; }
            public virtual decimal TransAmountApplied { get; set; }
            public virtual string Version { get; set; }
            public virtual string RecordID { get; set; }


            public DataTable getApplication(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddRPApplicationTag(RPApplicationTag RPApplicationTag)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.RPApplicationTag WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransType", RPApplicationTag.TransType);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransDoc", RPApplicationTag.TransDoc);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransDate", RPApplicationTag.TransDate);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransAccountCode", RPApplicationTag.TransAccountCode);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransSubsiCode", RPApplicationTag.TransSubsiCode);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransProfitCenter", RPApplicationTag.TransProfitCenter);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransCostCenter", RPApplicationTag.TransCostCenter);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransBizPartnerCode", RPApplicationTag.TransBizPartnerCode);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransAmountDue", RPApplicationTag.TransAmountDue);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "TransAmountApplied", RPApplicationTag.TransAmountApplied);
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "Version", "1");
                DT1.Rows.Add("Accounting.RPApplicationTag", "0", "RecordID", RPApplicationTag.RecordID);

                DT2.Rows.Add("Accounting.RPApplication", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.RPApplication", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateRPApplicationTag(RPApplicationTag RPApplicationTag)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.RPApplicationTag", "cond", "DocNumber", Docnum );
                DT1.Rows.Add("Accounting.RPApplicationTag", "cond", "LineNumber", RPApplicationTag.LineNumber);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransType", RPApplicationTag.TransType);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransDoc", RPApplicationTag.TransDoc);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransDate", RPApplicationTag.TransDate);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransAccountCode", RPApplicationTag.TransAccountCode);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransSubsiCode", RPApplicationTag.TransSubsiCode);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransProfitCenter", RPApplicationTag.TransProfitCenter);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransCostCenter", RPApplicationTag.TransCostCenter);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransBizPartnerCode", RPApplicationTag.TransBizPartnerCode);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransAmountDue", RPApplicationTag.TransAmountDue);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "TransAmountApplied", RPApplicationTag.TransAmountApplied);
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "Version", "1");
                DT1.Rows.Add("Accounting.RPApplicationTag", "set", "RecordID", RPApplicationTag.RecordID);

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteRPApplicationTag(RPApplicationTag RPApplicationTag)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.RPApplicationTag", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RPApplicationTag", "cond", "LineNumber", RPApplicationTag.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Application = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber = '" + Docnum + "'", Conn);
                
                if ((Application.Rows.Count < 1) && (Adjustment.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.RPApplication", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.RPApplication", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        #endregion

        #region Adjustment
        public class RPApplicationAdj
        {
            public virtual RPApplication Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string BizPartnerCode { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual string Reason { get; set; }
            public DataTable getAdjustment(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddRPApplicationAdj(RPApplicationAdj RPApplicationAdj)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.RPApplicationAdj WHERE DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "AccountCode", RPApplicationAdj.AccountCode);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "SubsidiaryCode", RPApplicationAdj.SubsidiaryCode);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "BizPartnerCode", RPApplicationAdj.BizPartnerCode);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "ProfitCenter", RPApplicationAdj.ProfitCenter);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "CostCenter", RPApplicationAdj.CostCenter);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "Amount", RPApplicationAdj.Amount);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "0", "Reason", RPApplicationAdj.Reason);

                DT2.Rows.Add("Accounting.RPApplication", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.RPApplication", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateRPApplicationAdj(RPApplicationAdj RPApplicationAdj)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.RPApplicationAdj", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "cond", "LineNumber", RPApplicationAdj.LineNumber);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "set", "AccountCode", RPApplicationAdj.AccountCode);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "set", "SubsidiaryCode", RPApplicationAdj.SubsidiaryCode);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "set", "BizPartnerCode", RPApplicationAdj.BizPartnerCode);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "set", "ProfitCenter", RPApplicationAdj.ProfitCenter);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "set", "CostCenter", RPApplicationAdj.CostCenter);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "set", "Amount", RPApplicationAdj.Amount);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "set", "Reason", RPApplicationAdj.Reason);

                Gears.UpdateData(DT1, Conn);   
            }
            public void DeleteRPApplicationAdj(RPApplicationAdj RPApplicationAdj)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.RPApplicationAdj", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.RPApplicationAdj", "cond", "LineNumber", RPApplicationAdj.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable Application = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber = '" + Docnum + "'", Conn);
                DataTable Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber = '" + Docnum + "'", Conn);
                
                if ((Application.Rows.Count < 1) && (Adjustment.Rows.Count < 1))
                {
                    DT2.Rows.Add("Accounting.RPApplication", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.RPApplication", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        #endregion
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.RPApplicationTag WHERE DocNumber = '" + DocNumber + "' AND Version = '1'", Conn);
        }
    }
}
