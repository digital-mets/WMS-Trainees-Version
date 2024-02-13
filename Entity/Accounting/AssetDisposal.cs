using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class AssetDisposal
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string InvoiceDocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string DisposalType { get; set; }
        public virtual string SoldTo { get; set; }
        public virtual decimal TotalAmountSold { get; set; }
        public virtual decimal GrossVATAmount { get; set; }
        public virtual decimal GrossNonVATAmount { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal TotalAssetCost { get; set; }
        public virtual decimal TotalAccumulatedDepreciation { get; set; }
        public virtual decimal NetBookValue { get; set; }
        public virtual decimal TotalGainLoss { get; set; }

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

        



        public virtual IList<AssetDisposalDetail> Detail { get; set; }
        public class AssetDisposalDetail
        {
            public virtual AssetDisposal Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PropertyNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual decimal OrigQty { get; set; }
            public virtual decimal UnitPrice { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual decimal AccumulatedDepreciation { get; set; }
            public virtual decimal SoldAmount { get; set; }
            public virtual bool IsVat { get; set; }
            public virtual decimal Rate { get; set; }
            public virtual string VATCode { get; set; }
            public virtual string PropertyStatus { get; set; }
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
                    a = Gears.RetriveData2("select *, ItemCode AS ClassCode, ItemCode AS SizeCode from Accounting.AssetDisposalDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddAssetDisposalDetail(AssetDisposalDetail AssetDisposalDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.AssetDisposalDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "LineNumber", strLine);


                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "PropertyNumber", AssetDisposalDetail.PropertyNumber);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "ItemCode", AssetDisposalDetail.ItemCode);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "FullDesc", AssetDisposalDetail.FullDesc);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "ColorCode", AssetDisposalDetail.ColorCode);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Qty", AssetDisposalDetail.Qty);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "OrigQty", AssetDisposalDetail.OrigQty);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "UnitPrice", AssetDisposalDetail.UnitPrice);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "UnitCost", AssetDisposalDetail.UnitCost);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "AccumulatedDepreciation", AssetDisposalDetail.AccumulatedDepreciation);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "SoldAmount", AssetDisposalDetail.SoldAmount);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "IsVat", AssetDisposalDetail.IsVat);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Rate", AssetDisposalDetail.Rate);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "VATCode", AssetDisposalDetail.VATCode);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "PropertyStatus", AssetDisposalDetail.PropertyStatus);


                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field1", AssetDisposalDetail.Field1);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field2", AssetDisposalDetail.Field2);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field3", AssetDisposalDetail.Field3);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field4", AssetDisposalDetail.Field4);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field5", AssetDisposalDetail.Field5);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field6", AssetDisposalDetail.Field6);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field7", AssetDisposalDetail.Field7);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field8", AssetDisposalDetail.Field8);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Field9", AssetDisposalDetail.Field9);


                DT2.Rows.Add("Accounting.AssetDisposal", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.AssetDisposal", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateAssetDisposalDetail(AssetDisposalDetail AssetDisposalDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "cond", "LineNumber", AssetDisposalDetail.LineNumber);


                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "PropertyNumber", AssetDisposalDetail.PropertyNumber);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "ItemCode", AssetDisposalDetail.ItemCode);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "FullDesc", AssetDisposalDetail.FullDesc);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "ColorCode", AssetDisposalDetail.ColorCode);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Qty", AssetDisposalDetail.Qty);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "OrigQty", AssetDisposalDetail.OrigQty);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "UnitPrice", AssetDisposalDetail.UnitPrice);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "UnitCost", AssetDisposalDetail.UnitCost);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "AccumulatedDepreciation", AssetDisposalDetail.AccumulatedDepreciation);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "SoldAmount", AssetDisposalDetail.SoldAmount);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "IsVat", AssetDisposalDetail.IsVat);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Rate", AssetDisposalDetail.Rate);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "VATCode", AssetDisposalDetail.VATCode);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "PropertyStatus", AssetDisposalDetail.PropertyStatus);


                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field1", AssetDisposalDetail.Field1);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field2", AssetDisposalDetail.Field2);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field3", AssetDisposalDetail.Field3);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field4", AssetDisposalDetail.Field4);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field5", AssetDisposalDetail.Field5);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field6", AssetDisposalDetail.Field6);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field7", AssetDisposalDetail.Field7);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field8", AssetDisposalDetail.Field8);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "set", "Field9", AssetDisposalDetail.Field9);

                Gears.UpdateData(DT1, Conn);                               
            }
            public void DeleteAssetDisposalDetail(AssetDisposalDetail AssetDisposalDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.AssetDisposalDetail", "cond", "LineNumber", AssetDisposalDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Accounting.AssetDisposalDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.AssetDisposal", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.AssetDisposal", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public class JournalEntry
        {
            public virtual AssetDisposal Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTADI' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }


        public class RefTransaction
        {
            public virtual AssetDisposal Parent { get; set; }
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
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTADI' OR  A.TransType='ACTADI') ", Conn);
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

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.AssetDisposal where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                InvoiceDocNumber = dtRow["InvoiceDocNum"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                DisposalType = dtRow["DisposalType"].ToString();
                SoldTo = dtRow["SoldTo"].ToString();
                TotalAmountSold = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountSold"]) ? 0 : dtRow["TotalAmountSold"]);
                GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVATAmount"]) ? 0 : dtRow["GrossVATAmount"]);
                GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossNonVATAmount"]) ? 0 : dtRow["GrossNonVATAmount"]);
                Remarks = dtRow["Remarks"].ToString();
                TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAssetCost"]) ? 0 : dtRow["TotalAssetCost"]);
                TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAccumulatedDepreciation"]) ? 0 : dtRow["TotalAccumulatedDepreciation"]);
                NetBookValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetBookValue"]) ? 0 : dtRow["NetBookValue"]);
                TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGainLoss"]) ? 0 : dtRow["TotalGainLoss"]);
                
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                PostedBy = dtRow["PostedBy"].ToString();
                PostedDate = dtRow["PostedDate"].ToString();


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

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AssetDisposal", "0", "DocNumber", _ent.DocNumber);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "InvoiceDocNum", _ent.InvoiceDocNumber);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "DocDate", _ent.DocDate);


            DT1.Rows.Add("Accounting.AssetDisposal", "0", "DisposalType", _ent.DisposalType);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "SoldTo", _ent.SoldTo);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAmountSold", _ent.TotalAmountSold);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossVATAmount", _ent.GrossVATAmount);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossNonVATAmount", _ent.GrossNonVATAmount);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAssetCost", _ent.TotalAssetCost);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAccumulatedDepreciation", _ent.TotalAccumulatedDepreciation);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "NetBookValue", _ent.NetBookValue);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalGainLoss", _ent.TotalGainLoss);


            DT1.Rows.Add("Accounting.AssetDisposal", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsWithDetail", "False");

            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AssetDisposal", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(AssetDisposal _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AssetDisposal", "cond", "DocNumber", _ent.DocNumber);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "InvoiceDocNum", _ent.InvoiceDocNumber);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "DisposalType", _ent.DisposalType);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "SoldTo", _ent.SoldTo);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalAmountSold", _ent.TotalAmountSold);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "GrossVATAmount", _ent.GrossVATAmount);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "GrossNonVATAmount", _ent.GrossNonVATAmount);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalAssetCost", _ent.TotalAssetCost);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalAccumulatedDepreciation", _ent.TotalAccumulatedDepreciation);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "NetBookValue", _ent.NetBookValue);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalGainLoss", _ent.TotalGainLoss);


            DT1.Rows.Add("Accounting.AssetDisposal", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTADI", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(AssetDisposal _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.AssetDisposal", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.AssetDisposalDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("ACTADI", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
