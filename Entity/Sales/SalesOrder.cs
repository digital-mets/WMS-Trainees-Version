using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class SalesOrder
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Status { get; set; }
        public virtual string CustomerPONo { get; set; }
        public virtual string DateCompleted { get; set; }
        public virtual string TargetDeliveryDate { get; set; }
        public virtual string Remarks { get; set; }
        //public virtual decimal TotalQty { get; set; }
        public virtual string TotalQty { get; set; }
        public virtual decimal TotalBulkQty { get; set; }
        public virtual decimal TotalFreight { get; set; }
        public virtual bool Vatable { get; set; }
        public virtual string QuotationNo { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual string Currency { get; set; }
        public virtual int Terms { get; set; }
        public virtual string ProcurementDoc { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual decimal GrossVatableAmount { get; set; }
        public virtual decimal NonVatableAmount { get; set; }
        public virtual decimal VATAmount { get; set; }
        public virtual bool IsWithQuote { get; set; }
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
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string ForcedClosedBy { get; set; }
        public virtual string ForcedClosedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }

        public virtual IList<SalesOrderDetail> Detail { get; set; }


        public class SalesOrderDetail
        {
            public virtual SalesOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal UnitPrice { get; set; }
            public virtual decimal UnitFreight { get; set; }
            public virtual bool IsVAT { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal DiscountRate { get; set; }
            public virtual decimal DeliveredQty { get; set; }
            public virtual string SubstituteItem { get; set; }
            public virtual string SubstituteColor { get; set; }
            public virtual string SubstituteClass { get; set; }
            public virtual string SubstituteSize { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal UnitFactor { get; set; }
            public virtual string Version { get; set; }
            public virtual decimal Rate { get; set; }
            public virtual bool IsByBulk { get; set; }
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
                    a = Gears.RetriveData2("select * from Sales.SalesOrderDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddSalesOrderDetail(SalesOrderDetail SalesOrderDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Sales.SalesOrderDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "ItemCode", SalesOrderDetail.ItemCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "ColorCode", SalesOrderDetail.ColorCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "ClassCode", SalesOrderDetail.ClassCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "SizeCode", SalesOrderDetail.SizeCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "OrderQty", SalesOrderDetail.OrderQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Unit", SalesOrderDetail.Unit);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "BulkQty", SalesOrderDetail.BulkQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "BulkUnit", SalesOrderDetail.BulkUnit);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "UnitPrice", SalesOrderDetail.UnitPrice);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "UnitFreight", SalesOrderDetail.UnitFreight);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "IsVAT", SalesOrderDetail.IsVAT);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "VATCode", SalesOrderDetail.VATCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "DiscountRate", SalesOrderDetail.DiscountRate);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "DeliveredQty", SalesOrderDetail.DeliveredQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "SubstituteItem", SalesOrderDetail.SubstituteItem);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "SubstituteColor", SalesOrderDetail.SubstituteColor);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "SubstituteClass", SalesOrderDetail.SubstituteClass);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "SubstituteSize", SalesOrderDetail.SubstituteSize);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "FullDesc", SalesOrderDetail.FullDesc);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "StatusCode", SalesOrderDetail.StatusCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "BaseQty", SalesOrderDetail.BaseQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "BarcodeNo", SalesOrderDetail.BarcodeNo);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "UnitFactor", SalesOrderDetail.UnitFactor);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Rate", SalesOrderDetail.Rate);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Version", "1");
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "IsByBulk", SalesOrderDetail.IsByBulk);

                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field1", SalesOrderDetail.Field1);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field2", SalesOrderDetail.Field2);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field3", SalesOrderDetail.Field3);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field4", SalesOrderDetail.Field4);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field5", SalesOrderDetail.Field5);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field6", SalesOrderDetail.Field6);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field7", SalesOrderDetail.Field7);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field8", SalesOrderDetail.Field8);
                DT1.Rows.Add("Sales.SalesOrderDetail", "0", "Field9", SalesOrderDetail.Field9);

                DT2.Rows.Add("Sales.SalesOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Sales.SalesOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateSalesOrderDetail(SalesOrderDetail SalesOrderDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.SalesOrderDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.SalesOrderDetail", "cond", "LineNumber", SalesOrderDetail.LineNumber); 
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "ItemCode", SalesOrderDetail.ItemCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "ColorCode", SalesOrderDetail.ColorCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "ClassCode", SalesOrderDetail.ClassCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "SizeCode", SalesOrderDetail.SizeCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "OrderQty", SalesOrderDetail.OrderQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Unit", SalesOrderDetail.Unit);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "BulkQty", SalesOrderDetail.BulkQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "BulkUnit", SalesOrderDetail.BulkUnit);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "UnitPrice", SalesOrderDetail.UnitPrice);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "UnitFreight", SalesOrderDetail.UnitFreight);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "IsVAT", SalesOrderDetail.IsVAT);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "VATCode", SalesOrderDetail.VATCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "DiscountRate", SalesOrderDetail.DiscountRate);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "DeliveredQty", SalesOrderDetail.DeliveredQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "SubstituteItem", SalesOrderDetail.SubstituteItem);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "SubstituteColor", SalesOrderDetail.SubstituteColor);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "SubstituteClass", SalesOrderDetail.SubstituteClass);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "SubstituteSize", SalesOrderDetail.SubstituteSize);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "FullDesc", SalesOrderDetail.FullDesc);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "StatusCode", SalesOrderDetail.StatusCode);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "BaseQty", SalesOrderDetail.BaseQty);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "BarcodeNo", SalesOrderDetail.BarcodeNo);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "UnitFactor", SalesOrderDetail.UnitFactor);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Rate", SalesOrderDetail.Rate);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Version", "1");
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "IsByBulk", SalesOrderDetail.IsByBulk);

                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field1", SalesOrderDetail.Field1);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field2", SalesOrderDetail.Field2);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field3", SalesOrderDetail.Field3);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field4", SalesOrderDetail.Field4);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field5", SalesOrderDetail.Field5);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field6", SalesOrderDetail.Field6);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field7", SalesOrderDetail.Field7);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field8", SalesOrderDetail.Field8);
                DT1.Rows.Add("Sales.SalesOrderDetail", "set", "Field9", SalesOrderDetail.Field9);

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteSalesOrderDetail(SalesOrderDetail SalesOrderDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.SalesOrderDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.SalesOrderDetail", "cond", "LineNumber", SalesOrderDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Sales.SalesOrderDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Sales.SalesOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Sales.SalesOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public class RefTransaction
        {
            public virtual SalesOrder Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='SLSORD' OR  A.TransType='SLSORD') ", Conn);
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

            a = Gears.RetriveData2("select * from Sales.SalesOrder where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Status = dtRow["Status"].ToString();
                CustomerPONo = dtRow["CustomerPONo"].ToString();
                DateCompleted = dtRow["DateCompleted"].ToString();
                TargetDeliveryDate = dtRow["TargetDeliveryDate"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                //TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                TotalQty = dtRow["TotalQty"].ToString();
                TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalBulkQty"]) ? 0 : dtRow["TotalBulkQty"]);
                TotalFreight = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalFreight"]) ? 0 : dtRow["TotalFreight"]);
                Vatable = Convert.ToBoolean(Convert.IsDBNull(dtRow["Vatable"]) ? false : dtRow["Vatable"]);
                IsWithQuote = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithQuote"]) ? false : dtRow["IsWithQuote"]);
                QuotationNo = dtRow["QuotationNo"].ToString();
                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                Currency = dtRow["Currency"].ToString();
                Terms = Convert.ToInt32(Convert.IsDBNull(dtRow["Terms"]) ? 0 : dtRow["Terms"]);
                ProcurementDoc = dtRow["ProcurementDoc"].ToString();
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatableAmount"]) ? 0 : dtRow["GrossVatableAmount"]);
                NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatableAmount"]) ? 0 : dtRow["NonVatableAmount"]);
                VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);

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
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                ForcedClosedBy = dtRow["ForcedClosedBy"].ToString();
                ForcedClosedDate = dtRow["ForcedClosedDate"].ToString();               
            }

            return a;
        }
        public void InsertData(SalesOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.SalesOrder", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SalesOrder", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.SalesOrder", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Status", _ent.Status);
            DT1.Rows.Add("Sales.SalesOrder", "0", "CustomerPONo", _ent.CustomerPONo);
            DT1.Rows.Add("Sales.SalesOrder", "0", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.SalesOrder", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Sales.SalesOrder", "0", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Sales.SalesOrder", "0", "TotalFreight", _ent.TotalFreight);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Vatable", _ent.Vatable);
            DT1.Rows.Add("Sales.SalesOrder", "0", "IsWithQuote", _ent.IsWithQuote);
            DT1.Rows.Add("Sales.SalesOrder", "0", "QuotationNo", _ent.QuotationNo);
            DT1.Rows.Add("Sales.SalesOrder", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Terms", _ent.Terms);
            DT1.Rows.Add("Sales.SalesOrder", "0", "ProcurementDoc", _ent.ProcurementDoc);
            DT1.Rows.Add("Sales.SalesOrder", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Sales.SalesOrder", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Sales.SalesOrder", "0", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Sales.SalesOrder", "0", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Sales.SalesOrder", "0", "VATAmount", _ent.VATAmount);

            DT1.Rows.Add("Sales.SalesOrder", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SalesOrder", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Sales.SalesOrder", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Sales.SalesOrder", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(SalesOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.SalesOrder", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SalesOrder", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.SalesOrder", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Status", _ent.Status);
            DT1.Rows.Add("Sales.SalesOrder", "set", "CustomerPONo", _ent.CustomerPONo);
            DT1.Rows.Add("Sales.SalesOrder", "set", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.SalesOrder", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Sales.SalesOrder", "set", "TotalBulkQty", _ent.TotalBulkQty);
            DT1.Rows.Add("Sales.SalesOrder", "set", "TotalFreight", _ent.TotalFreight);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Vatable", _ent.Vatable);
            DT1.Rows.Add("Sales.SalesOrder", "set", "IsWithQuote", _ent.IsWithQuote);
            DT1.Rows.Add("Sales.SalesOrder", "set", "QuotationNo", _ent.QuotationNo);
            DT1.Rows.Add("Sales.SalesOrder", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Sales.SalesOrder", "set", "ProcurementDoc", _ent.ProcurementDoc);
            DT1.Rows.Add("Sales.SalesOrder", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Sales.SalesOrder", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Sales.SalesOrder", "set", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Sales.SalesOrder", "set", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Sales.SalesOrder", "set", "VATAmount", _ent.VATAmount);

            DT1.Rows.Add("Sales.SalesOrder", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SalesOrder", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Sales.SalesOrder", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Sales.SalesOrder", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSORD", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(SalesOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.SalesOrder", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Sales.SalesOrderDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("SLSORD", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void SubstituteInfo(string DocNumber, string Customer, string Conn)
        {
            DataTable subs = Gears.RetriveData2("select * from Sales.SalesOrderDetail where Docnumber ='" + DocNumber + "'", Conn);
            if (subs.Rows.Count != 0)
            {
                DataTable update = Gears.RetriveData2("UPDATE A SET A.SubstituteItem = B.SubstitutedItem, A.SubstituteColor = B.SubstitutedColor, A.SubstituteClass = B.SubstitutedClass " +
                                                    " FROM Sales.SalesOrderDetail A "+
                                                    " INNER JOIN Masterfile.ItemCustomerPrice B "+
                                                    " ON A.ItemCode = B.ItemCode "+
                                                    " AND A.ColorCode = B.ColorCode "+
                                                    " AND A.SizeCode = B.SizeCode "+
                                                    " AND A.ClassCode = B.ClassCode " +
                                                    " WHERE A.DocNumber ='" + DocNumber + "' AND B.Customer = '" + Customer + "'", Conn);
            }
        }

        public void InitialDelete(string DocNum, string Conn)
        {
            DataTable a = Gears.RetriveData2("DELETE FROM Sales.SalesOrderDetail WHERE DocNumber = '" + DocNum + "'  AND Version = '1'", Conn);
        }
    }
}
