using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CashRecon
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string FundCode { get; set; }
        public virtual string Custodian { get; set; }

        public virtual decimal CashFundAmount { get; set; }
        public virtual decimal TotalShortOverCash { get; set; }
        public virtual decimal CheckAmountOnHand { get; set; }
        public virtual decimal TotalCashOnHand { get; set; }
        public virtual decimal CashAdvance { get; set; }
        public virtual decimal UnreplenishedExpenditures { get; set; }
        public virtual decimal PettyCashReimbursement { get; set; }
        public virtual decimal LiquidatedCashAdvance { get; set; }

        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }


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
        
        
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }


        public class CashReconDetail
        {
            public virtual CashRecon Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual decimal Denomination { get; set; }
            public virtual int Qty { get; set; }
            public virtual decimal Amount { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * from Accounting.PettyCashReconDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCashReconDetail(CashReconDetail CashReconDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("SELECT MAX(convert(int,LineNumber)) as LineNumber from Accounting.PettyCashReconDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "0", "LineNumber", strLine);


                DT1.Rows.Add("Accounting.PettyCashReconDetail", "0", "Denomination", CashReconDetail.Denomination);
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "0", "Qty", CashReconDetail.Qty);
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "0", "Amount", CashReconDetail.Amount);


                DT2.Rows.Add("Accounting.PettyCashRecon", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.PettyCashRecon", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateCashReconDetail(CashReconDetail CashReconDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "cond", "DocNumber", CashReconDetail.DocNumber);
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "cond", "LineNumber", CashReconDetail.LineNumber);


                DT1.Rows.Add("Accounting.PettyCashReconDetail", "set", "Denomination", CashReconDetail.Denomination);
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "set", "Qty", CashReconDetail.Qty);
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "set", "Amount", CashReconDetail.Amount);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteCashReconDetail(CashReconDetail CashReconDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "cond", "DocNumber", CashReconDetail.DocNumber);
                DT1.Rows.Add("Accounting.PettyCashReconDetail", "cond", "LineNumber", CashReconDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Accounting.PettyCashReconDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.PettyCashRecon", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.PettyCashRecon", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        //public class JournalEntry
        //{
        //    public virtual AssetDisposal Parent { get; set; }
        //    public virtual string AccountCode { get; set; }
        //    public virtual string AccountDescription { get; set; }
        //    public virtual string SubsidiaryCode { get; set; }
        //    public virtual string SubsidiaryDescription { get; set; }
        //    public virtual string ProfitCenter { get; set; }
        //    public virtual string CostCenter { get; set; }
        //    public virtual string Debit { get; set; }
        //    public virtual string Credit { get; set; }
        //    public DataTable getJournalEntry(string DocNumber, string Conn)
        //    {

        //        DataTable a;
        //        try
        //        {
        //            a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
        //            + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
        //            + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
        //            + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
        //            + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTADI' ", Conn);

        //            return a;
        //        }
        //        catch (Exception e)
        //        {
        //            a = null;
        //            return a;
        //        }
        //    }
        //}


        //public class RefTransaction
        //{
        //    public virtual AssetDisposal Parent { get; set; }
        //    public virtual string RTransType { get; set; }
        //    public virtual string REFDocNumber { get; set; }
        //    public virtual string RMenuID { get; set; }
        //    public virtual string TransType { get; set; }
        //    public virtual string DocNumber { get; set; }
        //    public virtual string MenuID { get; set; }
        //    public virtual string CommandString { get; set; }
        //    public virtual string RCommandString { get; set; }
        //    public DataTable getreftransaction(string DocNumber, string Conn)
        //    {

        //        DataTable a;
        //        try
        //        {
        //            a = Gears.RetriveData2("SELECT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
        //                                    + " INNER JOIN IT.MainMenu B"
        //                                    + " ON A.RMenuID =B.ModuleID "
        //                                    + " INNER JOIN IT.MainMenu C "
        //                                    + " ON A.MenuID = C.ModuleID "
        //                                    + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTADI' OR  A.TransType='ACTADI') ", Conn);
        //            return a;
        //        }
        //        catch (Exception e)
        //        {
        //            a = null;
        //            return a;
        //        }
        //    }
        //}

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.PettyCashRecon where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                FundCode = dtRow["FundCode"].ToString();
                CashFundAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CashFundAmount"]) ? 0 : dtRow["CashFundAmount"]);
                TotalShortOverCash = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalShortOverCash"]) ? 0 : dtRow["TotalShortOverCash"]);
                CheckAmountOnHand = Convert.ToDecimal(Convert.IsDBNull(dtRow["CheckAmountOnHand"]) ? 0 : dtRow["CheckAmountOnHand"]);
                TotalCashOnHand = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCashOnHand"]) ? 0 : dtRow["TotalCashOnHand"]);
                CashAdvance = Convert.ToDecimal(Convert.IsDBNull(dtRow["CashAdvance"]) ? 0 : dtRow["CashAdvance"]);
                UnreplenishedExpenditures = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnreplenishedExpenditures"]) ? 0 : dtRow["UnreplenishedExpenditures"]);
                PettyCashReimbursement = Convert.ToDecimal(Convert.IsDBNull(dtRow["PettyCashReimbursement"]) ? 0 : dtRow["PettyCashReimbursement"]);
                LiquidatedCashAdvance = Convert.ToDecimal(Convert.IsDBNull(dtRow["LiquidatedCashAdvance"]) ? 0 : dtRow["LiquidatedCashAdvance"]);

                Custodian = dtRow["Custodian"].ToString();

                
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                //CancelledBy = dtRow["CancelledBy"].ToString();
                //CancelledDate = dtRow["CancelledDate"].ToString();
                //PostedBy = dtRow["PostedBy"].ToString();
                //PostedDate = dtRow["PostedDate"].ToString();


                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);


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

            return a;
        }
        public void InsertData(AssetDisposal _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "DocNumber", _ent.DocNumber);
            ////DT1.Rows.Add("Accounting.AssetDisposal", "0", "InvoiceDocNum", _ent.InvoiceDocNumber);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "DocDate", _ent.DocDate);


            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "DisposalType", _ent.DisposalType);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "SoldTo", _ent.SoldTo);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAmountSold", _ent.TotalAmountSold);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossVATAmount", _ent.GrossVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossNonVATAmount", _ent.GrossNonVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Remarks", _ent.Remarks);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAssetCost", _ent.TotalAssetCost);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAccumulatedDepreciation", _ent.TotalAccumulatedDepreciation);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "NetBookValue", _ent.NetBookValue);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalGainLoss", _ent.TotalGainLoss);


            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            ////DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsValidated", _ent.IsValidated);
            ////DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsWithDetail", "False");

            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field1", _ent.Field1);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field2", _ent.Field2);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field3", _ent.Field3);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field4", _ent.Field4);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field5", _ent.Field5);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field6", _ent.Field6);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field7", _ent.Field7);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field8", _ent.Field8);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field9", _ent.Field9);



           // Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(CashRecon _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                
            DT1.Rows.Add("Accounting.PettyCashRecon", "cond", "DocNumber", _ent.DocNumber);
            //DT1.Rows.Add("Accounting.PettyCashRecon", "set", "InvoiceDocNum", _ent.InvoiceDocNumber);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "FundCode", _ent.FundCode);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Custodian", _ent.Custodian);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "CashFundAmount", _ent.CashFundAmount);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "TotalShortOverCash", _ent.TotalShortOverCash);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "CheckAmountOnHand", _ent.CheckAmountOnHand);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "TotalCashOnHand", _ent.TotalCashOnHand);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "CashAdvance", _ent.CashAdvance);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "UnreplenishedExpenditures", _ent.UnreplenishedExpenditures);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "PettyCashReimbursement", _ent.PettyCashReimbursement);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "LiquidatedCashAdvance", _ent.LiquidatedCashAdvance);


            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            //DT1.Rows.Add("Accounting.PettyCashRecon", "set", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.PettyCashRecon", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.PettyCashRecon", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
         Functions.AuditTrail("RTLPCR", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(CashRecon _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.PettyCashRecon", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);


            Functions.AuditTrail("RTLPCR    ", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
