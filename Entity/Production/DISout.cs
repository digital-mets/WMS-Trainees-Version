using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DISout
    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Step { get; set; }
        public virtual string DIS { get; set; }
        public virtual string WorkCenter { get; set; }
        public virtual string ReceivingStep { get; set; }
        public virtual string ReceivingWorkCenter { get; set; }
        public virtual string DRDocNo { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal LaborCost { get; set; }
        //public virtual string Size { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual bool FinalDIS { get; set; }
        public virtual string Warehouse { get; set; }
        public virtual string Remarks { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }

        //public virtual string CancelledBy { get; set; }
        //public virtual string CancelledDate { get; set; }
        //public virtual string PostedBy { get; set; }
        //public virtual string PostedDate { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public class RefTransaction
        {
            public virtual DISout Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='" + TransType + "' OR  A.TransType='" + TransType + "') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            if (DocNumber != null)
            {
                a = Gears.RetriveData2("select * from Production.DISout where DocNumber = '" + DocNumber + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    DocDate = dtRow["DocDate"].ToString();
                    Step = dtRow["Step"].ToString();
                    DIS = dtRow["DIS"].ToString();
                    WorkCenter = dtRow["WorkCenter"].ToString();
                    ReceivingStep = dtRow["ReceivingStep"].ToString();
                    ReceivingWorkCenter = dtRow["ReceivingWorkCenter"].ToString(); 
                    DRDocNo = dtRow["DRDocNo"].ToString();
                    TotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmount"]) ? 0 : dtRow["TotalAmount"]);
                    LaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["LaborCost"]) ? 0 : dtRow["LaborCost"]);
                    //Size = dtRow["Size"].ToString();
                    Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);

                    FinalDIS = Convert.ToBoolean(Convert.IsDBNull(dtRow["FinalDIS"]) ? false : dtRow["FinalDIS"]);
                    Warehouse = dtRow["Warehouse"].ToString();
                    Remarks = dtRow["Remarks"].ToString();
                    
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    SubmittedBy = dtRow["SubmittedBy"].ToString();
                    SubmittedDate = dtRow["SubmittedDate"].ToString();
                    //CancelledBy = dtRow["CancelledBy"].ToString();
                    //CancelledDate = dtRow["CancelledDate"].ToString();

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
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public class JournalEntry
        {
            public virtual DISout Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit, A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='" + TransType + "' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public class DISoutDetail
        {
            public virtual DISout Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.DISoutDetail WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddDISoutDetail(DISoutDetail A)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.DISoutDetail WHERE DocNumber = '" + Docnum + "'", Conn);
                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }

                string strLine = linenum.ToString().PadLeft(5, '0');
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISoutDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISoutDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.DISoutDetail", "0", "SizeCode", A.SizeCode);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Qty", A.Qty);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field1", A.Field1);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field2", A.Field2);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field3", A.Field3);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field4", A.Field4);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field5", A.Field5);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field6", A.Field6);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field7", A.Field7);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field8", A.Field8);
                DT1.Rows.Add("Production.DISoutDetail", "0", "Field9", A.Field9);
                DT2.Rows.Add("Production.DISout", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.DISout", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateDISoutDetail(DISoutDetail B)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISoutDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISoutDetail", "cond", "LineNumber", B.LineNumber);
                DT1.Rows.Add("Production.DISoutDetail", "set", "SizeCode", B.SizeCode);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Qty", B.Qty);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field1", B.Field1);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field2", B.Field2);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field3", B.Field3);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field4", B.Field4);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field5", B.Field5);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field6", B.Field6);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field7", B.Field7);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field8", B.Field8);
                DT1.Rows.Add("Production.DISoutDetail", "set", "Field9", B.Field9);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteDISoutDetail(DISoutDetail C)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISoutDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISoutDetail", "cond", "LineNumber", C.LineNumber);
                Gears.DeleteData(DT1, Conn);
                
                DataTable count3 = Gears.RetriveData2("select * from Production.DISoutDetail where DocNumber = '" + Docnum + "'", Conn);
                if (count3.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.DISout", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.DISout", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        public void InsertData(DISout _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DISout", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.DISout", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.DISout", "0", "Step", _ent.Step);
            DT1.Rows.Add("Production.DISout", "0", "DIS", _ent.DIS);
            DT1.Rows.Add("Production.DISout", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.DISout", "0", "ReceivingStep", _ent.ReceivingStep);
            DT1.Rows.Add("Production.DISout", "0", "ReceivingWorkCenter", _ent.ReceivingWorkCenter);
            DT1.Rows.Add("Production.DISout", "0", "DRDocNo", _ent.DRDocNo);
            DT1.Rows.Add("Production.DISout", "0", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Production.DISout", "0", "LaborCost", _ent.LaborCost);
            //DT1.Rows.Add("Production.DISout", "0", "Size", _ent.Size);
            DT1.Rows.Add("Production.DISout", "0", "Qty", _ent.Qty);
            DT1.Rows.Add("Production.DISout", "0", "FinalDIS", _ent.FinalDIS);
            DT1.Rows.Add("Production.DISout", "0", "Warehouse", _ent.Warehouse);
            DT1.Rows.Add("Production.DISout", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Production.DISout", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.DISout", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.DISout", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.DISout", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.DISout", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.DISout", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.DISout", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.DISout", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.DISout", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.DISout", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.DISout", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Production.DISout", "0", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.DISout", "0", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
            //Functions.AuditTrail("ACTCOC", _ent.DocNumber, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }
        public void UpdateData(DISout _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DISout", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.DISout", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.DISout", "set", "Step", _ent.Step);
            DT1.Rows.Add("Production.DISout", "set", "DIS", _ent.DIS);
            DT1.Rows.Add("Production.DISout", "set", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.DISout", "set", "ReceivingStep", _ent.ReceivingStep);
            DT1.Rows.Add("Production.DISout", "set", "ReceivingWorkCenter", _ent.ReceivingWorkCenter);
            DT1.Rows.Add("Production.DISout", "set", "DRDocNo", _ent.DRDocNo);
            DT1.Rows.Add("Production.DISout", "set", "TotalAmount", _ent.TotalAmount);
            DT1.Rows.Add("Production.DISout", "set", "LaborCost", _ent.LaborCost);
            //DT1.Rows.Add("Production.DISout", "set", "Size", _ent.Size);
            DT1.Rows.Add("Production.DISout", "set", "Qty", _ent.Qty);
            DT1.Rows.Add("Production.DISout", "set", "FinalDIS", _ent.FinalDIS);
            DT1.Rows.Add("Production.DISout", "set", "Warehouse", _ent.Warehouse);
            DT1.Rows.Add("Production.DISout", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Production.DISout", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.DISout", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.DISout", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.DISout", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.DISout", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.DISout", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.DISout", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.DISout", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.DISout", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.DISout", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.DISout", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            //Functions.AuditTrail("ACTCOC", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(DISout _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DISout", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDOUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
