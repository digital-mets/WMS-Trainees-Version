using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class MaterialPlanning
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string JONumber { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string DueDateFrom { get; set; }
        public virtual string DueDateTo { get; set; }
        //public virtual string Remarks { get; set; }
        //public virtual decimal TotalAssetCost { get; set; }
        //public virtual decimal TotalAccumulatedDepreciation { get; set; }
        //public virtual decimal NetBookValue { get; set; }
        //public virtual decimal TotalGainLoss { get; set; }

        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }


        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        //public virtual string SubmittedBy { get; set; }
        //public virtual string SubmittedDate { get; set; }
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





        public virtual IList<MaterialPlanningDetail> Detail { get; set; }
        public class MaterialPlanningDetail
        {
            public virtual MaterialPlanning Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string JONumber { get; set; }
            public virtual string StockNumber { get; set; }
            public virtual string JODueDate { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal RequiredQty { get; set; }
            public virtual decimal UnallocatedQty { get; set; }
            public virtual decimal NeededQty { get; set; }
            public virtual decimal OnhandQty { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual decimal AllocatedQty { get; set; }

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
                    a = Gears.RetriveData2("select * from Production.MaterialPlanningDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddMaterialPlanningDetail(MaterialPlanningDetail MaterialPlanningDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.MaterialPlanningDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "LineNumber", strLine);


                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "JONumber", MaterialPlanningDetail.JONumber);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "StockNumber", MaterialPlanningDetail.StockNumber);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "JODueDate", MaterialPlanningDetail.JODueDate);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "StepCode", MaterialPlanningDetail.StepCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "ItemCode", MaterialPlanningDetail.ItemCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "ColorCode", MaterialPlanningDetail.ColorCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "ClassCode", MaterialPlanningDetail.ClassCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "SizeCode", MaterialPlanningDetail.SizeCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "RequiredQty", MaterialPlanningDetail.RequiredQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "UnallocatedQty", MaterialPlanningDetail.UnallocatedQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "NeededQty", MaterialPlanningDetail.NeededQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "OnhandQty", MaterialPlanningDetail.OnhandQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "OrderQty", MaterialPlanningDetail.OrderQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "AllocatedQty", MaterialPlanningDetail.AllocatedQty);


                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field1", MaterialPlanningDetail.Field1);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field2", MaterialPlanningDetail.Field2);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field3", MaterialPlanningDetail.Field3);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field4", MaterialPlanningDetail.Field4);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field5", MaterialPlanningDetail.Field5);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field6", MaterialPlanningDetail.Field6);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field7", MaterialPlanningDetail.Field7);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field8", MaterialPlanningDetail.Field8);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "0", "Field9", MaterialPlanningDetail.Field9);


                DT2.Rows.Add("Production.MaterialPlanningDetail", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.MaterialPlanningDetail", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateMaterialPlanningDetail(MaterialPlanningDetail MaterialPlanningDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.MaterialPlanningDetail", "cond", "DocNumber", MaterialPlanningDetail.DocNumber);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "cond", "LineNumber", MaterialPlanningDetail.LineNumber);


                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "JONumber", MaterialPlanningDetail.JONumber);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "StockNumber", MaterialPlanningDetail.StockNumber);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "JODueDate", MaterialPlanningDetail.JODueDate);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "StepCode", MaterialPlanningDetail.StepCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "ItemCode", MaterialPlanningDetail.ItemCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "ColorCode", MaterialPlanningDetail.ColorCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "ClassCode", MaterialPlanningDetail.ClassCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "SizeCode", MaterialPlanningDetail.SizeCode);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "RequiredQty", MaterialPlanningDetail.RequiredQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "UnallocatedQty", MaterialPlanningDetail.UnallocatedQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "NeededQty", MaterialPlanningDetail.NeededQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "OnhandQty", MaterialPlanningDetail.OnhandQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "OrderQty", MaterialPlanningDetail.OrderQty);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "AllocatedQty", MaterialPlanningDetail.AllocatedQty);


                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field1", MaterialPlanningDetail.Field1);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field2", MaterialPlanningDetail.Field2);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field3", MaterialPlanningDetail.Field3);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field4", MaterialPlanningDetail.Field4);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field5", MaterialPlanningDetail.Field5);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field6", MaterialPlanningDetail.Field6);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field7", MaterialPlanningDetail.Field7);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field8", MaterialPlanningDetail.Field8);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "set", "Field9", MaterialPlanningDetail.Field9);

                Gears.UpdateData(DT1, Conn);                               
            }
            public void DeleteMaterialPlanningDetail(MaterialPlanningDetail MaterialPlanningDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.MaterialPlanningDetail", "cond", "DocNumber", MaterialPlanningDetail.DocNumber);
                DT1.Rows.Add("Production.MaterialPlanningDetail", "cond", "LineNumber", MaterialPlanningDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Production.MaterialPlanningDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.MaterialPlanningDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.MaterialPlanningDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        //public class JournalEntry
        //{
        //    public virtual MaterialPlanning Parent { get; set; }
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
        //            + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit  FROM Accounting.GeneralLedger A "
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
            a = Gears.RetriveData2("select * from Production.MaterialPlanning where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                JONumber = dtRow["JONumber"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                DueDateFrom = dtRow["DueDateFrom"].ToString();
                DueDateTo = dtRow["DueDateTo"].ToString();
                //DueDateTo = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountSold"]) ? 0 : dtRow["TotalAmountSold"]);
                //GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVATAmount"]) ? 0 : dtRow["GrossVATAmount"]);
                //GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossNonVATAmount"]) ? 0 : dtRow["GrossNonVATAmount"]);
                //Remarks = dtRow["Remarks"].ToString();
                //TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAssetCost"]) ? 0 : dtRow["TotalAssetCost"]);
                //TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAccumulatedDepreciation"]) ? 0 : dtRow["TotalAccumulatedDepreciation"]);
                //NetBookValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetBookValue"]) ? 0 : dtRow["NetBookValue"]);
                //TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGainLoss"]) ? 0 : dtRow["TotalGainLoss"]);
                
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                //SubmittedBy = dtRow["SubmittedBy"].ToString();
                //SubmittedDate = dtRow["SubmittedDate"].ToString();
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
        public void InsertData(MaterialPlanning _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.MaterialPlanning", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "DocDate", _ent.DocDate);


            DT1.Rows.Add("Production.MaterialPlanning", "0", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "DueDateFrom", _ent.DueDateFrom);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "DueDateTo", _ent.DueDateTo);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossNonVATAmount", _ent.GrossNonVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Remarks", _ent.Remarks);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAssetCost", _ent.TotalAssetCost);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAccumulatedDepreciation", _ent.TotalAccumulatedDepreciation);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "NetBookValue", _ent.NetBookValue);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalGainLoss", _ent.TotalGainLoss);


            DT1.Rows.Add("Production.MaterialPlanning", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsWithDetail", "False");

            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.MaterialPlanning", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(MaterialPlanning _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.MaterialPlanning", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "DocDate", _ent.DocDate);


            DT1.Rows.Add("Production.MaterialPlanning", "set", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "DueDateFrom", _ent.DueDateFrom);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "DueDateTo", _ent.DueDateTo);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossNonVATAmount", _ent.GrossNonVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Remarks", _ent.Remarks);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAssetCost", _ent.TotalAssetCost);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAccumulatedDepreciation", _ent.TotalAccumulatedDepreciation);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "NetBookValue", _ent.NetBookValue);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalGainLoss", _ent.TotalGainLoss);


            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "LastEditedBy", _ent.LastEditedBy);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.MaterialPlanning", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRODMP", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(MaterialPlanning _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.MaterialPlanning", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Production.MaterialPlanningDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("PRODMP", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
