using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class S_Quotation
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Status { get; set; }
        public virtual string Validity { get; set; }
        public virtual string TargetDeliveryDate { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string TotalQty { get; set; }
        public virtual decimal InitialTotalAmount { get; set; }
        public virtual decimal AmountDiscounted { get; set; }
        //public virtual decimal Freight { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual decimal GrossVATableAmount { get; set; }
        public virtual decimal NonVATableAmount { get; set; }
        public virtual decimal VATAmount { get; set; }
        public virtual string Currency { get; set; }
        public virtual int Terms { get; set; }
        public virtual decimal ExchangeRate { get; set; }

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
        //public virtual string UsageTrail { get; set; }
        public virtual string IsWithDetail { get; set; }
        public virtual string IsValidated { get; set; }
        public virtual bool IsPrinted { get; set; }

        public virtual IList<QuotationDetail> Detail { get; set; }


        public class QuotationDetail
        {
            public virtual S_Quotation Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal UnitPrice { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal DiscountRate { get; set; }
            public virtual bool IsVat { get; set; }
            public virtual decimal vatrate { get; set; }
            public virtual string TaxCode { get; set; }
            public virtual decimal DeliveredQty { get; set; }
            public virtual decimal OrderedQty { get; set; }
            //public virtual decimal UnitFreight { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string BarcodeNo { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal UnitFactor { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Sales.QuotationDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            
            public void AddQuotationDetail(QuotationDetail QuotationDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Sales.QuotationDetail where DocNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Sales.QuotationDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("Sales.QuotationDetail", "0", "ItemCode", QuotationDetail.ItemCode);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "ColorCode", QuotationDetail.ColorCode);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "ClassCode", QuotationDetail.ClassCode);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "SizeCode", QuotationDetail.SizeCode);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Qty", QuotationDetail.Qty);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Unit", QuotationDetail.Unit);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "UnitPrice", QuotationDetail.UnitPrice);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "BulkQty", QuotationDetail.BulkQty);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "BulkUnit", QuotationDetail.BulkUnit);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "DiscountRate", QuotationDetail.DiscountRate);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "IsVat", QuotationDetail.IsVat);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "vatrate", QuotationDetail.vatrate);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "TaxCode", QuotationDetail.TaxCode);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "DeliveredQty", QuotationDetail.DeliveredQty);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "OrderedQty", QuotationDetail.OrderedQty);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "StatusCode", QuotationDetail.StatusCode);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "BaseQty", QuotationDetail.BaseQty);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "BarcodeNo", QuotationDetail.BarcodeNo);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "UnitFactor", QuotationDetail.UnitFactor);
                //DT1.Rows.Add("Sales.QuotationDetail", "0", "UnitFreight", QuotationDetail.UnitFreight);
                
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field1", QuotationDetail.Field1);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field2", QuotationDetail.Field2);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field3", QuotationDetail.Field3);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field4", QuotationDetail.Field4);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field5", QuotationDetail.Field5);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field6", QuotationDetail.Field6);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field7", QuotationDetail.Field7);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field8", QuotationDetail.Field8);
                DT1.Rows.Add("Sales.QuotationDetail", "0", "Field9", QuotationDetail.Field9);

                DT2.Rows.Add("Sales.Quotation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Sales.Quotation", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateQuotationDetail(QuotationDetail QuotationDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Sales.QuotationDetail", "cond", "DocNumber", QuotationDetail.DocNumber);
                DT1.Rows.Add("Sales.QuotationDetail", "cond", "LineNumber", QuotationDetail.LineNumber);

                DT1.Rows.Add("Sales.QuotationDetail", "set", "ItemCode", QuotationDetail.ItemCode);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "ColorCode", QuotationDetail.ColorCode);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "ClassCode", QuotationDetail.ClassCode);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "SizeCode", QuotationDetail.SizeCode);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Qty", QuotationDetail.Qty);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Unit", QuotationDetail.Unit);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "UnitPrice", QuotationDetail.UnitPrice);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "BulkQty", QuotationDetail.BulkQty);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "BulkUnit", QuotationDetail.BulkUnit);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "DiscountRate", QuotationDetail.DiscountRate);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "IsVat", QuotationDetail.IsVat);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "vatrate", QuotationDetail.vatrate);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "TaxCode", QuotationDetail.TaxCode);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "DeliveredQty", QuotationDetail.DeliveredQty);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "OrderedQty", QuotationDetail.OrderedQty);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "StatusCode", QuotationDetail.StatusCode);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "BaseQty", QuotationDetail.BaseQty);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "BarcodeNo", QuotationDetail.BarcodeNo);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "UnitFactor", QuotationDetail.UnitFactor);
                //DT1.Rows.Add("Sales.QuotationDetail", "set", "UnitFreight", QuotationDetail.UnitFreight);

                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field1", QuotationDetail.Field1);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field2", QuotationDetail.Field2);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field3", QuotationDetail.Field3);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field4", QuotationDetail.Field4);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field5", QuotationDetail.Field5);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field6", QuotationDetail.Field6);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field7", QuotationDetail.Field7);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field8", QuotationDetail.Field8);
                DT1.Rows.Add("Sales.QuotationDetail", "set", "Field9", QuotationDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteQuotationDetail(QuotationDetail QuotationDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.QuotationDetail", "cond", "DocNumber", QuotationDetail.DocNumber);
                DT1.Rows.Add("Sales.QuotationDetail", "cond", "LineNumber", QuotationDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Sales.QuotationDetail where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Sales.Quotation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Sales.Quotation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        public class RefTransaction
        {
            public virtual S_Quotation Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='SLSQUM' OR  A.TransType='SLSQUM') ", Conn);
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
            DataTable a = Gears.RetriveData2("select * from Sales.Quotation where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Status = dtRow["Status"].ToString();
                Validity = dtRow["Validity"].ToString();
                TargetDeliveryDate = dtRow["TargetDeliveryDate"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                TotalQty = dtRow["TotalQty"].ToString();
                InitialTotalAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["InitialTotalAmount"]) ? 0 : dtRow["InitialTotalAmount"]);
                AmountDiscounted = Convert.ToDecimal(Convert.IsDBNull(dtRow["AmountDiscounted"]) ? 0 : dtRow["AmountDiscounted"]);
                //Freight = Convert.ToDecimal(Convert.IsDBNull(dtRow["Freight"]) ? 0 : dtRow["Freight"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVATableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVATableAmount"]) ? 0 : dtRow["GrossVATableAmount"]);
                NonVATableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVATableAmount"]) ? 0 : dtRow["NonVATableAmount"]);
                VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);
                Currency = dtRow["Currency"].ToString();
                Terms = Convert.ToInt32(Convert.IsDBNull(dtRow["Terms"]) ? 0 : dtRow["Terms"]);
                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 1 : dtRow["ExchangeRate"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);
                
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                CancelledBy = dtRow["ForceClosedBy"].ToString();
                CancelledDate = dtRow["ForceClosedDate"].ToString();
                //UsageTrail = dtRow["UsageTrail"].ToString();

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
        public void InsertData(S_Quotation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.Quotation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.Quotation", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.Quotation", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Sales.Quotation", "0", "Status", "N");
            DT1.Rows.Add("Sales.Quotation", "0", "Validity", _ent.Validity);
            DT1.Rows.Add("Sales.Quotation", "0", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Sales.Quotation", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.Quotation", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Sales.Quotation", "0", "InitialTotalAmount", _ent.InitialTotalAmount);
            DT1.Rows.Add("Sales.Quotation", "0", "AmountDiscounted", _ent.AmountDiscounted);
            //DT1.Rows.Add("Sales.Quotation", "0", "Freight", _ent.Freight);
            DT1.Rows.Add("Sales.Quotation", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Sales.Quotation", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Sales.Quotation", "0", "GrossVATableAmount", _ent.GrossVATableAmount);
            DT1.Rows.Add("Sales.Quotation", "0", "NonVATableAmount", _ent.NonVATableAmount);
            DT1.Rows.Add("Sales.Quotation", "0", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Sales.Quotation", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Sales.Quotation", "0", "Terms", _ent.Terms);
            DT1.Rows.Add("Sales.Quotation", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Sales.Quotation", "0", "IsPrinted", _ent.IsPrinted);

            DT1.Rows.Add("Sales.Quotation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.Quotation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.Quotation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.Quotation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.Quotation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.Quotation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.Quotation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.Quotation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.Quotation", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Sales.Quotation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Sales.Quotation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Sales.Quotation", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(S_Quotation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.Quotation", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.Quotation", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.Quotation", "set", "CustomerCode", _ent.CustomerCode);
            //DT1.Rows.Add("Sales.Quotation", "set", "Status", _ent.Status);
            DT1.Rows.Add("Sales.Quotation", "set", "Validity", _ent.Validity);
            DT1.Rows.Add("Sales.Quotation", "set", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Sales.Quotation", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.Quotation", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Sales.Quotation", "set", "InitialTotalAmount", _ent.InitialTotalAmount);
            DT1.Rows.Add("Sales.Quotation", "set", "AmountDiscounted", _ent.AmountDiscounted);
            //DT1.Rows.Add("Sales.Quotation", "set", "Freight", _ent.Freight);
            DT1.Rows.Add("Sales.Quotation", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Sales.Quotation", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Sales.Quotation", "set", "GrossVATableAmount", _ent.GrossVATableAmount);
            DT1.Rows.Add("Sales.Quotation", "set", "NonVATableAmount", _ent.NonVATableAmount);
            DT1.Rows.Add("Sales.Quotation", "set", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Sales.Quotation", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Sales.Quotation", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Sales.Quotation", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Sales.Quotation", "set", "IsPrinted", _ent.IsPrinted);

            DT1.Rows.Add("Sales.Quotation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.Quotation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.Quotation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.Quotation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.Quotation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.Quotation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.Quotation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.Quotation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.Quotation", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Sales.Quotation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Sales.Quotation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            //Gears.UpdateData(DT2);
            Functions.AuditTrail("SLSQUM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(S_Quotation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.Quotation", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSQUM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
