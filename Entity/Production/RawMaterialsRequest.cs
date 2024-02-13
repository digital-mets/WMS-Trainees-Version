using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class RawMaterialsRequest
    {
        private static string Conn;//ADD CONN
        private static string Docnum;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string DocDate { get; set; }
        public virtual string RMRNumber { get; set; }
        public virtual string RMRRDetails { get; set; }
        public virtual string RequestingDeptCompany { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Consignee { get; set; }
        public virtual string ConsigneeAddress { get; set; }
        public virtual string RequiredLoadingTime { get; set; }
        public virtual string ShipmentType { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from Production.RawMaterialsRequest where RMRNumber = '" + RMRNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                //DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                RMRNumber = dtRow["RMRNumber"].ToString();
                RMRRDetails = dtRow["RMRRDetails"].ToString();
                RequestingDeptCompany = dtRow["RequestingDeptCompany"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Consignee = dtRow["Consignee"].ToString();
                ConsigneeAddress = dtRow["ConsigneeAddress"].ToString();
                RequiredLoadingTime = dtRow["RequiredLoadingTime"].ToString();
                ShipmentType = dtRow["ShipmentType"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                LastEditedBy = dtRow["CancelledBy"].ToString();
                LastEditedDate = dtRow["CancelledDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
            }

            return a;
        }
        public class RMRDetail
        {

            public virtual RawMaterialsRequest Parent { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual string ProcessSteps { get; set; }
            public virtual string OCNNumber { get; set; }
            public virtual string Qty { get; set; }
            public virtual string UOM { get; set; }
            public virtual string SHInstruction { get; set; }
            public virtual string TotalQty { get; set; }
            public virtual string Remarks { get; set; }

            public DataTable getdetail(string ItemCode, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.RMRDetail where ItemCode='" + ItemCode + "' order by BatchNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddRMRDetail(RMRDetail RMRDetail)
            {

                int batchnumber = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,BatchNumber)) as BatchNumber from Production.RMRDetail where ItemCode = '" + ItemCode + "'", Conn);

                try
                {
                    batchnumber = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    batchnumber = 1;
                }
                string strLine = batchnumber.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.RMRDetail", "0", "ItemCode", ItemCode);
                DT1.Rows.Add("Production.RMRDetail", "0", "BatchNumber", strLine);
                DT1.Rows.Add("Production.RMRDetail", "0", "ItemDescription", RMRDetail.ItemDescription);
                DT1.Rows.Add("Production.RMRDetail", "0", "ProcessSteps", RMRDetail.ProcessSteps);
                DT1.Rows.Add("Production.RMRDetail", "0", "OCNNumber", RMRDetail.OCNNumber);
                DT1.Rows.Add("Production.RMRDetail", "0", "Qty", RMRDetail.Qty);
                DT1.Rows.Add("Production.RMRDetail", "0", "UOM", RMRDetail.UOM);
                DT1.Rows.Add("Production.RMRDetail", "0", "SHInstruction", RMRDetail.SHInstruction);
                DT1.Rows.Add("Production.RMRDetail", "0", "TotalQty", RMRDetail.TotalQty);
                DT1.Rows.Add("Production.RMRDetail", "0", "Remarks", RMRDetail.Remarks);




                //DT2.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.CuttingWorksheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateRMRDetail(RMRDetail RMRDetail)
            {
                int batchnumber = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.RMRDetail", "cond", "ItemCode", ItemCode);
                DT1.Rows.Add("Production.RMRDetail", "cond", "BatchNumber", RMRDetail.BatchNumber);
                DT1.Rows.Add("Production.RMRDetail", "set", "ItemDescription", RMRDetail.ItemDescription);
                DT1.Rows.Add("Production.RMRDetail", "set", "ProcessSteps", RMRDetail.ProcessSteps);
                DT1.Rows.Add("Production.RMRDetail", "set", "OCNNumber", RMRDetail.OCNNumber);
                DT1.Rows.Add("Production.RMRDetail", "set", "Qty", RMRDetail.Qty);
                DT1.Rows.Add("Production.RMRDetail", "set", "UOM", RMRDetail.UOM);
                DT1.Rows.Add("Production.RMRDetail", "set", "SHInstruction", RMRDetail.SHInstruction);
                DT1.Rows.Add("Production.RMRDetail", "set", "TotalQty", RMRDetail.TotalQty);
                DT1.Rows.Add("Production.RMRDetail", "set", "Remarks", RMRDetail.Remarks);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteRMRDetail(RMRDetail RMRDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.RMRDetail", "cond", "ItemCode", RMRDetail.ItemCode);
                DT1.Rows.Add("Production.RMRDetail", "cond", "BatchNumber", RMRDetail.BatchNumber);


                Gears.DeleteData(DT1, Conn);




            }





        }
        public class RefTransaction
        {
            public virtual RawMaterialsRequest Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDSRQT' OR  A.TransType='PRDSRQT') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(RawMaterialsRequest _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "RMRNumber", _ent.RMRNumber);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "RMRRDetails", _ent.RMRRDetails);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "RequestingDeptCompany", _ent.RequestingDeptCompany);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "Consignee", _ent.Consignee);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "ConsigneeAddress", _ent.ConsigneeAddress);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.RawMaterialsRequest", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(RawMaterialsRequest _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            RMRNumber = _ent.RMRNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "RMRNumber", _ent.RMRNumber);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "RMRRDetails", _ent.RMRRDetails);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "RequestingDeptCompany", _ent.RequestingDeptCompany);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "Consignee", _ent.Consignee);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "ConsigneeAddress", _ent.ConsigneeAddress);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.RawMaterialsRequest", "set", "LastEditedDate", _ent.LastEditedDate);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDSRQT", RMRNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(RawMaterialsRequest _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.RawMaterialsRequest", "cond", "RMRNumber", _ent.RMRNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDRMR", RMRNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        /*
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
        }*/
    }
}
