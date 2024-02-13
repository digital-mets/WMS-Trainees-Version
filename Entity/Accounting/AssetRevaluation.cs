using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class AssetRevaluation
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }


        public virtual string PropertyNumber { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string AssetCode { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual string ClassCode { get; set; }
        public virtual string SizeCode { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal UnitCost { get; set; }
        public virtual decimal OtherCost { get; set; }
        public virtual decimal TotalAcquisitionCost { get; set; }
        public virtual decimal SalvageValue { get; set; }
        public virtual decimal AccumulatedDepreciation { get; set; }
        public virtual decimal NetBookValue { get; set; }
        public virtual decimal NewBookValue { get; set; }
        public virtual string NewDepreciationMethod { get; set; }
        public virtual decimal NewRemainingLifeAsset { get; set; }
        public virtual decimal NewMonthlyDepreciationAmount { get; set; }
        public virtual decimal UpdatedLife { get; set; }





        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }


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
        

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }


        public class JournalEntry
        {
            public virtual AssetRevaluation Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTRVL' ", Conn);

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
            public virtual AssetRevaluation Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTRVL' OR  A.TransType='ACTRVL') ", Conn);
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
            a = Gears.RetriveData2("select * from Accounting.AssetRevaluation where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {  
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();


                PropertyNumber = dtRow["PropertyNumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                OtherCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["OtherCost"]) ? 0 : dtRow["OtherCost"]);
                TotalAcquisitionCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAcquisitionCost"]) ? 0 : dtRow["TotalAcquisitionCost"]);
                SalvageValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["SalvageValue"]) ? 0 : dtRow["SalvageValue"]);
                AccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(dtRow["AccumulatedDepreciation"]) ? 0 : dtRow["AccumulatedDepreciation"]);
                NetBookValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetBookValue"]) ? 0 : dtRow["NetBookValue"]);
                NewBookValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewBookValue"]) ? 0 : dtRow["NewBookValue"]);
                NewDepreciationMethod = dtRow["NewDepreciationMethod"].ToString();
                NewRemainingLifeAsset = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewRemainingLifeAsset"]) ? 0 : dtRow["NewRemainingLifeAsset"]);
                NewMonthlyDepreciationAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewMonthlyDepreciationAmount"]) ? 0 : dtRow["NewMonthlyDepreciationAmount"]);

                
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
        public void InsertData(AssetRevaluation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "DocDate", _ent.DocDate);


            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "PropertyNumber", _ent.PropertyNumber);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Qty", _ent.Qty);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "UnitCost", _ent.UnitCost);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "OtherCost", _ent.OtherCost);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "TotalAcquisitionCost", _ent.TotalAcquisitionCost);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "SalvageValue", _ent.SalvageValue);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "AccumulatedDepreciation", _ent.AccumulatedDepreciation);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "NetBookValue", _ent.NetBookValue);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "NewBookValue", _ent.NewBookValue);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "NewDepreciationMethod", _ent.NewDepreciationMethod);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "NewRemainingLifeAsset", _ent.NewRemainingLifeAsset);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "NewMonthlyDepreciationAmount", _ent.NewMonthlyDepreciationAmount);


            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "IsValidated", _ent.IsValidated);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "IsWithDetail", _ent.IsWithDetail);

            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AssetRevaluation", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
            
        }

        public void UpdateData(AssetRevaluation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AssetRevaluation", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "DocDate", _ent.DocDate);


            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "PropertyNumber", _ent.PropertyNumber);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Qty", _ent.Qty);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "UnitCost", _ent.UnitCost);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "OtherCost", _ent.OtherCost);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "TotalAcquisitionCost", _ent.TotalAcquisitionCost);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "SalvageValue", _ent.SalvageValue);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "AccumulatedDepreciation", _ent.AccumulatedDepreciation);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "NewBookValue", _ent.NewBookValue);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "NetBookValue", _ent.NetBookValue);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "NewDepreciationMethod", _ent.NewDepreciationMethod);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "NewRemainingLifeAsset", _ent.NewRemainingLifeAsset);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "NewMonthlyDepreciationAmount", _ent.NewMonthlyDepreciationAmount);


            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "IsValidated", _ent.IsValidated);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AssetRevaluation", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTRVL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(AssetRevaluation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.AssetRevaluation", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTRVL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
