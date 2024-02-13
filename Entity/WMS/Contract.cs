using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Contract
    {
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }

        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Status { get; set; }
        public virtual string ContractNumber { get; set; }
        public virtual string DateFrom { get; set; }
        public virtual string DateTo { get; set; }
        public virtual string BillingPeriodType { get; set; }
        public virtual string BizPartnerCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string ProfitCenterCode { get; set; }
        public virtual string ContractType { get; set; }
        public virtual string EffectivityDate { get; set; }
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
        public virtual IList<ContractDetail> Detail { get; set; }
        public virtual IList<ContractDetailNon> NonDetail { get; set; }


        #region Reference Transactions
        public class RefTransaction
        {
            public virtual Contract Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType, REFDocNumber, RMenuID, RIGHT(B.CommandString, LEN(B.CommandString) - 1) AS RCommandString, "
                                            + " A.TransType, DocNumber, A.MenuID, RIGHT(C.CommandString, LEN(C.CommandString) - 1) AS CommandString FROM IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B ON A.RMenuID = B.ModuleID INNER JOIN IT.MainMenu C ON A.MenuID = C.ModuleID "
                                            + " WHERE (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') AND ((RTransType IN ('WMSCON','WMSCREV','WMSCREN','WMSCTER')) OR  (A.TransType IN ('WMSCON','WMSCREV','WMSCREN','WMSCTER')))", Conn);
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

        #region Storage Details
        public class ContractDetail
        {
            public virtual Contract Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ServiceType { get; set; }
            public virtual string Description { get; set; }
            public virtual decimal ServiceRate { get; set; }
            public virtual bool Vatable { get; set; }
            public virtual string UnitOfMeasure { get; set; }
            public virtual string UnitOfMeasureBulk { get; set; }
            public virtual string BillingType { get; set; }
            public virtual string Period { get; set; }
            public virtual decimal MinHandlingIn { get; set; }
            public virtual decimal MinHandlingOut { get; set; }
            public virtual decimal HandlingInRate { get; set; }
            public virtual decimal HandlingOutRate { get; set; }
            public virtual decimal MinStorage { get; set; }
            public virtual string Remarks { get; set; }
            public virtual string Type { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal ExcessRate { get; set; }
            public virtual int BeginDay { get; set; }
            public virtual int Staging { get; set; }
            public virtual int AllocChargeable { get; set; }
            public virtual bool SplitBill { get; set; }
            public virtual decimal SplitBillRate { get; set; }
            public virtual string ServiceHandling { get; set; }
            public virtual string HandlingUOMQty { get; set; }
            public virtual string HandlingUOMBulk { get; set; }
            public virtual string BillingPrintOutStr { get; set; }
            public virtual string BillingPrintOutHan { get; set; }
            public virtual decimal ConvFactorStr { get; set; }
            public virtual decimal ConvFactorHan { get; set; }
            public virtual bool IsMulStorage { get; set; }
            public virtual string StorageCode { get; set; }
            public virtual bool IsDiffCustomer { get; set; }
            public virtual string DiffCustomerCode { get; set; }
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
                    a = Gears.RetriveData2("SELECT * FROM WMS.ContractDetail WHERE Type = 'STORAGE' AND DocNumber = '" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddContractDetail(ContractDetail ContractDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM WMS.ContractDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }

                string strLine = linenum.ToString().PadLeft(5, '0');

                ContractDetail.Type = "STORAGE";

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ContractDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ContractDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ServiceType", ContractDetail.ServiceType);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Description", ContractDetail.Description);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ServiceRate", ContractDetail.ServiceRate);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Vatable", ContractDetail.Vatable);
                DT1.Rows.Add("WMS.ContractDetail", "0", "UnitOfMeasure", ContractDetail.UnitOfMeasure);
                DT1.Rows.Add("WMS.ContractDetail", "0", "UnitOfMeasureBulk", ContractDetail.UnitOfMeasureBulk);
                DT1.Rows.Add("WMS.ContractDetail", "0", "BillingType", ContractDetail.BillingType);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Period", ContractDetail.Period);
                DT1.Rows.Add("WMS.ContractDetail", "0", "MinHandlingIn", ContractDetail.MinHandlingIn);
                DT1.Rows.Add("WMS.ContractDetail", "0", "MinHandlingOut", ContractDetail.MinHandlingOut);
                DT1.Rows.Add("WMS.ContractDetail", "0", "HandlingInRate", ContractDetail.HandlingInRate);
                DT1.Rows.Add("WMS.ContractDetail", "0", "HandlingOutRate", ContractDetail.HandlingOutRate);
                DT1.Rows.Add("WMS.ContractDetail", "0", "MinStorage", ContractDetail.MinStorage);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Remarks", ContractDetail.Remarks);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Type", ContractDetail.Type);
                DT1.Rows.Add("WMS.ContractDetail", "0", "VATCode", ContractDetail.VATCode);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ExcessRate", ContractDetail.ExcessRate);
                DT1.Rows.Add("WMS.ContractDetail", "0", "BeginDay", ContractDetail.BeginDay);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Staging", ContractDetail.Staging);
                DT1.Rows.Add("WMS.ContractDetail", "0", "AllocChargeable", ContractDetail.AllocChargeable);
                DT1.Rows.Add("WMS.ContractDetail", "0", "SplitBill", ContractDetail.SplitBill);
                DT1.Rows.Add("WMS.ContractDetail", "0", "SplitBillRate", ContractDetail.SplitBillRate);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ServiceHandling", ContractDetail.ServiceHandling);
                DT1.Rows.Add("WMS.ContractDetail", "0", "HandlingUOMQty", ContractDetail.HandlingUOMQty);
                DT1.Rows.Add("WMS.ContractDetail", "0", "HandlingUOMBulk", ContractDetail.HandlingUOMBulk);
                DT1.Rows.Add("WMS.ContractDetail", "0", "BillingPrintOutStr", ContractDetail.BillingPrintOutStr);
                DT1.Rows.Add("WMS.ContractDetail", "0", "BillingPrintOutHan", ContractDetail.BillingPrintOutHan);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ConvFactorStr", ContractDetail.ConvFactorStr);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ConvFactorHan", ContractDetail.ConvFactorHan);
                DT1.Rows.Add("WMS.ContractDetail", "0", "IsMulStorage", ContractDetail.IsMulStorage);
                DT1.Rows.Add("WMS.ContractDetail", "0", "StorageCode", ContractDetail.StorageCode);
                DT1.Rows.Add("WMS.ContractDetail", "0", "IsDiffCustomer", ContractDetail.IsDiffCustomer);
                DT1.Rows.Add("WMS.ContractDetail", "0", "DiffCustomerCode", ContractDetail.DiffCustomerCode);

                DT1.Rows.Add("WMS.ContractDetail", "0", "Field1", ContractDetail.Field1);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field2", ContractDetail.Field2);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field3", ContractDetail.Field3);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field4", ContractDetail.Field4);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field5", ContractDetail.Field5);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field6", ContractDetail.Field6);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field7", ContractDetail.Field7);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field8", ContractDetail.Field8);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Field9", ContractDetail.Field9);

                DT2.Rows.Add("WMS.Contract", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.Contract", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateContractDetail(ContractDetail ContractDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ContractDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "LineNumber", ContractDetail.LineNumber);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "Type", ContractDetail.Type);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ServiceType", ContractDetail.ServiceType);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Description", ContractDetail.Description);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ServiceRate", ContractDetail.ServiceRate);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Vatable", ContractDetail.Vatable);
                DT1.Rows.Add("WMS.ContractDetail", "set", "UnitOfMeasure", ContractDetail.UnitOfMeasure);
                DT1.Rows.Add("WMS.ContractDetail", "set", "UnitOfMeasureBulk", ContractDetail.UnitOfMeasureBulk);
                DT1.Rows.Add("WMS.ContractDetail", "set", "BillingType", ContractDetail.BillingType);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Period", ContractDetail.Period);
                DT1.Rows.Add("WMS.ContractDetail", "set", "MinHandlingIn", ContractDetail.MinHandlingIn);
                DT1.Rows.Add("WMS.ContractDetail", "set", "MinHandlingOut", ContractDetail.MinHandlingOut);
                DT1.Rows.Add("WMS.ContractDetail", "set", "HandlingInRate", ContractDetail.HandlingInRate);
                DT1.Rows.Add("WMS.ContractDetail", "set", "HandlingOutRate", ContractDetail.HandlingOutRate);
                DT1.Rows.Add("WMS.ContractDetail", "set", "MinStorage", ContractDetail.MinStorage);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Remarks", ContractDetail.Remarks);
                DT1.Rows.Add("WMS.ContractDetail", "set", "VATCode", ContractDetail.VATCode);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ExcessRate", ContractDetail.ExcessRate);
                DT1.Rows.Add("WMS.ContractDetail", "set", "BeginDay", ContractDetail.BeginDay);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Staging", ContractDetail.Staging);
                DT1.Rows.Add("WMS.ContractDetail", "set", "AllocChargeable", ContractDetail.AllocChargeable);
                DT1.Rows.Add("WMS.ContractDetail", "set", "SplitBill", ContractDetail.SplitBill);
                DT1.Rows.Add("WMS.ContractDetail", "set", "SplitBillRate", ContractDetail.SplitBillRate);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ServiceHandling", ContractDetail.ServiceHandling);
                DT1.Rows.Add("WMS.ContractDetail", "set", "HandlingUOMQty", ContractDetail.HandlingUOMQty);
                DT1.Rows.Add("WMS.ContractDetail", "set", "HandlingUOMBulk", ContractDetail.HandlingUOMBulk);
                DT1.Rows.Add("WMS.ContractDetail", "set", "BillingPrintOutStr", ContractDetail.BillingPrintOutStr);
                DT1.Rows.Add("WMS.ContractDetail", "set", "BillingPrintOutHan", ContractDetail.BillingPrintOutHan);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ConvFactorStr", ContractDetail.ConvFactorStr);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ConvFactorHan", ContractDetail.ConvFactorHan);
                DT1.Rows.Add("WMS.ContractDetail", "set", "IsMulStorage", ContractDetail.IsMulStorage);
                DT1.Rows.Add("WMS.ContractDetail", "set", "StorageCode", ContractDetail.StorageCode);
                DT1.Rows.Add("WMS.ContractDetail", "set", "IsDiffCustomer", ContractDetail.IsDiffCustomer);
                DT1.Rows.Add("WMS.ContractDetail", "set", "DiffCustomerCode", ContractDetail.DiffCustomerCode);

                DT1.Rows.Add("WMS.ContractDetail", "set", "Field1", ContractDetail.Field1);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field2", ContractDetail.Field2);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field3", ContractDetail.Field3);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field4", ContractDetail.Field4);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field5", ContractDetail.Field5);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field6", ContractDetail.Field6);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field7", ContractDetail.Field7);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field8", ContractDetail.Field8);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Field9", ContractDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteContractDetail(ContractDetail ContractDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ContractDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "LineNumber", ContractDetail.LineNumber);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "ServiceType", ContractDetail.ServiceType);

                Gears.DeleteData(DT1, Conn);//KMM add Conn

                DataTable count = Gears.RetriveData2("SELECT * FROM WMS.ContractDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.ContractDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.ContractDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//KMM add Conn
                }

            }
        }
        #endregion

        #region NonStorage Details
        public class ContractDetailNon
        {
            public virtual Contract Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ServiceType { get; set; }
            public virtual string Description { get; set; }
            public virtual decimal ServiceRate { get; set; }
            public virtual string UnitOfMeasure { get; set; }
            public virtual string UnitOfMeasureBulk { get; set; }
            public virtual bool Vatable { get; set; }
            public virtual string VATCode { get; set; }
            public virtual string BillingType { get; set; }
            public virtual string Period { get; set; }
            public virtual string Remarks { get; set; }
            public virtual string Type { get; set; }
            public virtual bool AllocC { get; set; }
            public virtual string TransT { get; set; }
            public virtual string TruckT { get; set; }


            public DataTable getdetailNon(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    if(DocNumber == null){
                        DocNumber = Docnum;
                    }
                    a = Gears.RetriveData2("SELECT * FROM WMS.ContractDetail WHERE Type IN ('NONSTORAGE','NON-STORAGE','NON STORAGE') AND DocNumber = '" + DocNumber + "' ORDER BY LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddContractDetailNon(ContractDetailNon ContractDetailNon)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM WMS.ContractDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }

                string strLine = linenum.ToString().PadLeft(5, '0');

                ContractDetailNon.Type = "NONSTORAGE";
                
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ContractDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ContractDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ServiceType", ContractDetailNon.ServiceType);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Description", ContractDetailNon.Description);
                DT1.Rows.Add("WMS.ContractDetail", "0", "ServiceRate", ContractDetailNon.ServiceRate);
                DT1.Rows.Add("WMS.ContractDetail", "0", "UnitOfMeasure", ContractDetailNon.UnitOfMeasure);
                DT1.Rows.Add("WMS.ContractDetail", "0", "UnitOfMeasureBulk", ContractDetailNon.UnitOfMeasureBulk);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Vatable", ContractDetailNon.Vatable);
                DT1.Rows.Add("WMS.ContractDetail", "0", "VATCode", ContractDetailNon.VATCode);
                DT1.Rows.Add("WMS.ContractDetail", "0", "BillingType", ContractDetailNon.BillingType);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Period", ContractDetailNon.Period);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Remarks", ContractDetailNon.Remarks);
                DT1.Rows.Add("WMS.ContractDetail", "0", "Type", ContractDetailNon.Type);
                DT1.Rows.Add("WMS.ContractDetail", "0", "AllocC", ContractDetailNon.AllocC);
                DT1.Rows.Add("WMS.ContractDetail", "0", "TransT", ContractDetailNon.TransT);
                DT1.Rows.Add("WMS.ContractDetail", "0", "TruckT", ContractDetailNon.TruckT);


                DT2.Rows.Add("WMS.Contract", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("WMS.Contract", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }

            public void UpdateContractDetailNon(ContractDetailNon ContractDetailNon)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ContractDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "LineNumber", ContractDetailNon.LineNumber);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "Type", ContractDetailNon.Type);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ServiceType", ContractDetailNon.ServiceType);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Description", ContractDetailNon.Description);
                DT1.Rows.Add("WMS.ContractDetail", "set", "ServiceRate", ContractDetailNon.ServiceRate);
                DT1.Rows.Add("WMS.ContractDetail", "set", "UnitOfMeasure", ContractDetailNon.UnitOfMeasure);
                DT1.Rows.Add("WMS.ContractDetail", "set", "UnitOfMeasureBulk", ContractDetailNon.UnitOfMeasureBulk);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Vatable", ContractDetailNon.Vatable);
                DT1.Rows.Add("WMS.ContractDetail", "set", "VATCode", ContractDetailNon.VATCode);
                DT1.Rows.Add("WMS.ContractDetail", "set", "BillingType", ContractDetailNon.BillingType);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Period", ContractDetailNon.Period);
                DT1.Rows.Add("WMS.ContractDetail", "set", "Remarks", ContractDetailNon.Remarks);
                DT1.Rows.Add("WMS.ContractDetail", "set", "AllocC", ContractDetailNon.AllocC);
                DT1.Rows.Add("WMS.ContractDetail", "set", "TransT", ContractDetailNon.TransT);
                DT1.Rows.Add("WMS.ContractDetail", "set", "TruckT", ContractDetailNon.TruckT);


                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteContractDetailNon(ContractDetailNon ContractDetailNon)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ContractDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "LineNumber", ContractDetailNon.LineNumber);
                DT1.Rows.Add("WMS.ContractDetail", "cond", "ServiceType", ContractDetailNon.ServiceType);


                Gears.DeleteData(DT1, Conn);//KMM add Conn

                DataTable count = Gears.RetriveData2("SELECT * FROM WMS.ContractDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.ContractDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("WMS.ContractDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//KMM add Conn
                }

            }
        }
        #endregion

        #region Contract Header
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT A.*, B.FullName AS Added, C.FullName AS LastEdited, D.FullName AS Submitted FROM WMS.Contract A "
                + " LEFT JOIN IT.Users B ON A.AddedBy = B.UserID LEFT JOIN IT.Users C ON A.LastEditedBy = C.UserID  LEFT JOIN IT.Users D ON A.SubmittedBy = D.UserID "
                + " WHERE DocNumber = '" + DocNumber + "' ", Conn); //KMM add Conn
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                ContractNumber = dtRow["ContractNumber"].ToString();
                BillingPeriodType = dtRow["BillingPeriodType"].ToString();
                DateFrom = dtRow["DateFrom"].ToString();
                DateTo = dtRow["DateTo"].ToString();
                Status = dtRow["Status"].ToString();
                ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                ContractType = dtRow["ContractType"].ToString();
                EffectivityDate = dtRow["EffectivityDate"].ToString();

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
            }

            return a;
        }
        public void InsertData(Contract _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.Contract", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Contract", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Contract", "0", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.Contract", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Contract", "0", "ContractNumber", _ent.ContractNumber);
            DT1.Rows.Add("WMS.Contract", "0", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.Contract", "0", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.Contract", "0", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.Contract", "0", "Status", _ent.Status);
            DT1.Rows.Add("WMS.Contract", "0", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.Contract", "0", "ContractType", _ent.ContractType);
            DT1.Rows.Add("WMS.Contract", "0", "EffectivityDate", _ent.EffectivityDate);

            DT1.Rows.Add("WMS.Contract", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Contract", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Contract", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Contract", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Contract", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Contract", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Contract", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Contract", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Contract", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("WMS.Contract", "0", "IsWithDetail", "False");

            DT1.Rows.Add("WMS.Contract", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.Contract", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }
        public void UpdateData(Contract _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Contract", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Contract", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Contract", "set", "BizPartnerCode", _ent.BizPartnerCode);
            DT1.Rows.Add("WMS.Contract", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Contract", "set", "ContractNumber", _ent.ContractNumber);
            DT1.Rows.Add("WMS.Contract", "set", "BillingPeriodType", _ent.BillingPeriodType);
            DT1.Rows.Add("WMS.Contract", "set", "DateFrom", _ent.DateFrom);
            DT1.Rows.Add("WMS.Contract", "set", "DateTo", _ent.DateTo);
            DT1.Rows.Add("WMS.Contract", "set", "Status", _ent.Status);
            DT1.Rows.Add("WMS.Contract", "set", "ProfitCenterCode", _ent.ProfitCenterCode);
            DT1.Rows.Add("WMS.Contract", "set", "ContractType", _ent.ContractType);
            DT1.Rows.Add("WMS.Contract", "set", "EffectivityDate", _ent.EffectivityDate);

            DT1.Rows.Add("WMS.Contract", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("WMS.Contract", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("WMS.Contract", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("WMS.Contract", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("WMS.Contract", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("WMS.Contract", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("WMS.Contract", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("WMS.Contract", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("WMS.Contract", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("WMS.Contract", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.Contract", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSCON", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        public void Deletedata(Contract _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Contract", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Functions.AuditTrail("WMSCON", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }

        #endregion

        #region OtherCodes
        public void DeleteFirstDataStorage(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM WMS.ContractDetail WHERE DocNumber = '" + DocNumber + "' AND Type = 'STORAGE'", Conn);
        }
        public void DeleteFirstDataNonStorage(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM WMS.ContractDetail WHERE DocNumber = '" + DocNumber + "' AND Type != 'STORAGE'", Conn);
        }

        public void UpdateOtherInfo(string DocNumber, string Conn)
        {
            DataTable info = new DataTable();
            info = Gears.RetriveData2("UPDATE WMS.ContractDetail SET StorageCode = ServiceType WHERE DocNumber = '" + DocNumber + "' AND ISNULL(StorageCode,'') = ''"
                    + " UPDATE A SET A.DiffCustomerCode = B.BizPartnerCode FROM WMS.ContractDetail A INNER JOIN WMS.Contract B "
                    + " ON A.DocNumber = B.DocNumber WHERE A.DocNumber = '" + DocNumber + "' AND ISNULL(A.DiffCustomerCode,'') = ''", Conn);
        }

        #endregion
    }
}
