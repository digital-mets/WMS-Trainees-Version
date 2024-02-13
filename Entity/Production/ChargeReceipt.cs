using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ChargeReceipt
    {
        private static string Conn;//ADD CONN
        private static string Docnum;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
    
        public virtual string WorkCenter { get; set; }
        public virtual string PayTo { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal Amount { get; set; }
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


            a = Gears.RetriveData2("select * from Production.ChargeReceipt where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                PayTo = dtRow["PayTo"].ToString();
                Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                Amount = Convert.ToDecimal(Convert.IsDBNull(dtRow["Amount"]) ? 0 : dtRow["Amount"]);
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
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
            }

            return a;
        }
        public class CRDetail
        {

            public virtual ChargeReceipt Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string JobOrder { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string Description { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual decimal Price { get; set; }
            public virtual decimal Amount { get; set; }
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
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.CRDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCRDetail(CRDetail CRDetail)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.CRDetail where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.CRDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.CRDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.CRDetail", "0", "JobOrder", CRDetail.JobOrder);
                DT1.Rows.Add("Production.CRDetail", "0", "StepCode", CRDetail.StepCode);
                DT1.Rows.Add("Production.CRDetail", "0", "Description", CRDetail.Description);
                DT1.Rows.Add("Production.CRDetail", "0", "ItemCode", CRDetail.ItemCode);
                DT1.Rows.Add("Production.CRDetail", "0", "ColorCode", CRDetail.ColorCode);
                DT1.Rows.Add("Production.CRDetail", "0", "ClassCode", CRDetail.ClassCode);
                DT1.Rows.Add("Production.CRDetail", "0", "SizeCode", CRDetail.SizeCode);
                DT1.Rows.Add("Production.CRDetail", "0", "Qty", CRDetail.Qty);
                DT1.Rows.Add("Production.CRDetail", "0", "Price", CRDetail.Price);
                DT1.Rows.Add("Production.CRDetail", "0", "Amount", CRDetail.Amount);
                DT1.Rows.Add("Production.CRDetail", "0", "Remarks", CRDetail.Remarks);

                DT1.Rows.Add("Production.CRDetail", "0", "Field1", CRDetail.Field1);
                DT1.Rows.Add("Production.CRDetail", "0", "Field2", CRDetail.Field2);
                DT1.Rows.Add("Production.CRDetail", "0", "Field3", CRDetail.Field3);
                DT1.Rows.Add("Production.CRDetail", "0", "Field4", CRDetail.Field4);
                DT1.Rows.Add("Production.CRDetail", "0", "Field5", CRDetail.Field5);
                DT1.Rows.Add("Production.CRDetail", "0", "Field6", CRDetail.Field6);
                DT1.Rows.Add("Production.CRDetail", "0", "Field7", CRDetail.Field7);
                DT1.Rows.Add("Production.CRDetail", "0", "Field8", CRDetail.Field8);
                DT1.Rows.Add("Production.CRDetail", "0", "Field9", CRDetail.Field9);



                //DT2.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.CuttingWorksheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateCRDetail(CRDetail CRDetail)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.CRDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.CRDetail", "cond", "LineNumber", CRDetail.LineNumber);
                DT1.Rows.Add("Production.CRDetail", "set", "JobOrder", CRDetail.JobOrder);
                DT1.Rows.Add("Production.CRDetail", "set", "StepCode", CRDetail.StepCode);
                DT1.Rows.Add("Production.CRDetail", "set", "Description", CRDetail.Description);
                DT1.Rows.Add("Production.CRDetail", "set", "ItemCode", CRDetail.ItemCode);
                DT1.Rows.Add("Production.CRDetail", "set", "ColorCode", CRDetail.ColorCode);
                DT1.Rows.Add("Production.CRDetail", "set", "ClassCode", CRDetail.ClassCode);
                DT1.Rows.Add("Production.CRDetail", "set", "SizeCode", CRDetail.SizeCode);
                DT1.Rows.Add("Production.CRDetail", "set", "Qty", CRDetail.Qty);
                DT1.Rows.Add("Production.CRDetail", "set", "Price", CRDetail.Price);
                DT1.Rows.Add("Production.CRDetail", "set", "Amount", CRDetail.Amount);
                DT1.Rows.Add("Production.CRDetail", "set", "Remarks", CRDetail.Remarks);

                DT1.Rows.Add("Production.CRDetail", "set", "Field1", CRDetail.Field1);
                DT1.Rows.Add("Production.CRDetail", "set", "Field2", CRDetail.Field2);
                DT1.Rows.Add("Production.CRDetail", "set", "Field3", CRDetail.Field3);
                DT1.Rows.Add("Production.CRDetail", "set", "Field4", CRDetail.Field4);
                DT1.Rows.Add("Production.CRDetail", "set", "Field5", CRDetail.Field5);
                DT1.Rows.Add("Production.CRDetail", "set", "Field6", CRDetail.Field6);
                DT1.Rows.Add("Production.CRDetail", "set", "Field7", CRDetail.Field7);
                DT1.Rows.Add("Production.CRDetail", "set", "Field8", CRDetail.Field8);
                DT1.Rows.Add("Production.CRDetail", "set", "Field9", CRDetail.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteCRDetail(CRDetail CRDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.CRDetail", "cond", "DocNumber", CRDetail.DocNumber);
                DT1.Rows.Add("Production.CRDetail", "cond", "LineNumber", CRDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);




            }





        }
        public class RefTransaction
        {
            public virtual ChargeReceipt Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDCRT' OR  A.TransType='PRDCRT') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(ChargeReceipt _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



            DT1.Rows.Add("Production.ChargeReceipt", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Qty", _ent.Qty);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Amount", _ent.Amount);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.ChargeReceipt", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ChargeReceipt _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.ChargeReceipt", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.ChargeReceipt", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "PayTo", _ent.PayTo);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Qty", _ent.Qty);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Amount", _ent.Amount);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.ChargeReceipt", "set", "LastEditedDate", _ent.LastEditedDate);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDCRT", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ChargeReceipt _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.ChargeReceipt", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDCRT", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public class JournalEntry
        {
            public virtual ChargeReceipt Parent { get; set; }
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
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit, A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDCRT') ", Conn);

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
