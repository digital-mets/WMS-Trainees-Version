using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PurchasedOrder
    {
        private static string Docnum;
        private static string Conn;     //ADD CONN
        public virtual string Connection { get; set; }      //ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string TargetDeliveryDate { get; set; }
        public virtual string DateCompleted { get; set; }
        public virtual decimal TotalFreight { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual string Status { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string ReceivingWarehouse { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string Broker { get; set; }
        public virtual string PRNumberH { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancellationDate { get; set; }
        public virtual bool IsWithPR { get; set; }
        public virtual bool IsWithInvoice { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsReleased { get; set; }
        public virtual bool IsPrinted { get; set; }
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
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string ForceClosedBy { get; set; }
        public virtual string ForceClosedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string ReleasedBy { get; set; }
        public virtual string ReleasedDate { get; set; }
        
        

        public virtual string CommitmentDate { get; set; }
        public virtual string QuotationNo { get; set; }
        public virtual string Currency { get; set; }
        public virtual int Terms { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual decimal GrossVATableAmount { get; set; }
        public virtual decimal NonVATableAmount { get; set; }
        public virtual decimal VATAmount { get; set; }
        public virtual decimal WithholdingTax { get; set; }

        public virtual IList<PurchasedOrderDetail> Detail { get; set; }

        public virtual IList<PurchasedOrderService> DetailService { get; set; }

        public class PurchasedOrderDetail
        {
            public virtual PurchasedOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PRNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual decimal ReceivedQty { get; set; }
            public virtual decimal UnitFreight { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual bool IsClosed { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal AverageCost { get; set; }
            public virtual Boolean IsVat { get; set; }
            public virtual Boolean IsAllowPartial { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal Rate { get; set; }
            public virtual decimal ATCRate { get; set; }
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
                    a = Gears.RetriveData2("select *,b.FullDesc from Procurement.PurchaseOrderDetail a left join masterfile.item b on a.ItemCode = b.ItemCode "+
                                            " where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchasedOrderDetail(PurchasedOrderDetail PurchasedOrderDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseOrderDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "LineNumber", strLine);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "PRNumber", PRNumber);

                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "ItemCode", PurchasedOrderDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "ColorCode", PurchasedOrderDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "ClassCode", PurchasedOrderDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "SizeCode", PurchasedOrderDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "OrderQty", PurchasedOrderDetail.OrderQty);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Unit", PurchasedOrderDetail.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "UnitCost", PurchasedOrderDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "ReceivedQty", PurchasedOrderDetail.ReceivedQty);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "UnitFreight", PurchasedOrderDetail.UnitFreight);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "StatusCode", PurchasedOrderDetail.StatusCode);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "IsClosed", PurchasedOrderDetail.IsClosed);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "BaseQty", PurchasedOrderDetail.BaseQty);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "AverageCost", PurchasedOrderDetail.AverageCost);


                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "PRNumber", PurchasedOrderDetail.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "IsVat", PurchasedOrderDetail.IsVat);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "IsAllowPartial", PurchasedOrderDetail.IsAllowPartial);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "VATCode", PurchasedOrderDetail.VATCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Rate", PurchasedOrderDetail.Rate);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "ATCRate", PurchasedOrderDetail.ATCRate);

                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field1", PurchasedOrderDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field2", PurchasedOrderDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field3", PurchasedOrderDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field4", PurchasedOrderDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field5", PurchasedOrderDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field6", PurchasedOrderDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field7", PurchasedOrderDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field8", PurchasedOrderDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "0", "Field9", PurchasedOrderDetail.Field9);

                DT2.Rows.Add("Procurement.PurchaseOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePurchasedOrderDetail(PurchasedOrderDetail PurchasedOrderDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "cond", "LineNumber", PurchasedOrderDetail.LineNumber);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "PRNumber", PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "ItemCode", PurchasedOrderDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "ColorCode", PurchasedOrderDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "ClassCode", PurchasedOrderDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "SizeCode", PurchasedOrderDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "OrderQty", PurchasedOrderDetail.OrderQty);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Unit", PurchasedOrderDetail.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "UnitCost", PurchasedOrderDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "ReceivedQty", PurchasedOrderDetail.ReceivedQty);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "UnitFreight", PurchasedOrderDetail.UnitFreight);

                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "StatusCode", PurchasedOrderDetail.StatusCode);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "IsClosed", PurchasedOrderDetail.IsClosed);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "BaseQty", PurchasedOrderDetail.BaseQty);
                //DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "AverageCost", PurchasedOrderDetail.AverageCost);

                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "IsVat", PurchasedOrderDetail.IsVat);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "IsAllowPartial", PurchasedOrderDetail.IsAllowPartial);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "VATCode", PurchasedOrderDetail.VATCode);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Rate", PurchasedOrderDetail.Rate);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "ATCRate", PurchasedOrderDetail.ATCRate);

                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "PRNumber", PurchasedOrderDetail.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field1", PurchasedOrderDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field2", PurchasedOrderDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field3", PurchasedOrderDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field4", PurchasedOrderDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field5", PurchasedOrderDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field6", PurchasedOrderDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field7", PurchasedOrderDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field8", PurchasedOrderDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "set", "Field9", PurchasedOrderDetail.Field9);

                Gears.UpdateData(DT1, Conn);

                //Gears.RetriveData2("Update Procurement.PurchaseOrder"
                //+ " set TotalFreight=(Select sum(UnitFreight) * sum (OrderQty) "
                //+ " from Procurement.PurchaseOrderDetail where Docnumber='" + Docnum + "' ) "
                //+ " , TotalQty=(Select sum(OrderQty) "
                //+ " from Procurement.PurchaseOrderDetail where Docnumber='" + Docnum + "' ) "
                //+ " , PesoAmount=( "
                //+ " Select sum(UnitCost) * sum(OrderQty) * SUM(ExchangeRate) "
                //+ " from Procurement.PurchaseOrderDetail A "
                //+ " inner join Procurement.PurchaseOrder B "
                //+ "  on A.DocNumber = B.DocNumber  where A.Docnumber='" + Docnum + "' )  "
                //+ " , ForeignAmount=( "
                //+ " Select sum(UnitCost) * sum(OrderQty)  "
                //+ " from Procurement.PurchaseOrderDetail where Docnumber='" + Docnum + "' ) "
                //+ " , GrossVATableAmount=( "
                //+ " select  ISNULL(Sum(OrderQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.PurchaseOrder A "
                //+ " inner join Procurement.PurchaseOrderDetail B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and VatCode='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " , NonVATableAmount=( "
                //+ " select  ISNULL(Sum(OrderQty),0) *  ISNULL(Sum(UnitCost),0) *  ISNULL(Sum(ExchangeRate),0) "
                //+ "  from Procurement.PurchaseOrder A "
                //+ " inner join Procurement.PurchaseOrderDetail B "
                //+ " on A.DocNumber = B.DocNumber "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='NONV' "
                //+ " and VatCode='NONV' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where Docnumber = '" + Docnum + "'");

                //Gears.RetriveData2("Update Procurement.PurchaseOrder set VATAmount =( "
                //+ " select (ISNULL(Sum(GrossVATableAmount),0)/(1+sum(Rate)))*sum(Rate) "
                //+ " from Procurement.PurchaseOrder A "
                //+ " inner join Masterfile.BPSupplierInfo C "
                //+ " on A.SupplierCode = C.SupplierCode "
                //+ " inner join Masterfile.Tax D "
                //+ " on C.TaxCode = D.TCode "
                //+ " where C.TaxCode ='V' "
                //+ " and A.DocNumber='" + Docnum + "' ) "
                //+ " where docnumber = '" + Docnum + "'");

                //Gears.RetriveData2("Update Procurement.PurchaseOrder set  WithholdingTax =( "
                // + " Select (ISNULL(Sum(GrossVATableAmount),0) -ISNULL(Sum(VATAmount),0)) * sum(Rate) "
                // + "  from Procurement.PurchaseOrder A "
                // + " inner join Masterfile.BPSupplierInfo B "
                // + " on A.SupplierCode = B.SupplierCode "
                // + "  inner join Masterfile.ATC C "
                // + "  on B.ATCCode = C.ATCCode "
                // + "  where IsWithholdingTaxAgent ='1'"
                // + " and A.DocNumber='" + Docnum + "' ) "
                // + " where docnumber = '" + Docnum + "'");
                 
                 
            }
            public void DeletePurchasedOrderDetail(PurchasedOrderDetail PurchasedOrderDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderDetail", "cond", "LineNumber", PurchasedOrderDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseOrderDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public class PurchasedOrderService
        {
            public virtual PurchasedOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PRNumber { get; set; }
            public virtual string ServiceType { get; set; }
            public virtual string Description { get; set; }
            public virtual decimal ServiceQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual decimal TotalCost { get; set; }
            public virtual bool IsAllowProgressBilling { get; set; }
            public virtual bool IsVat { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal VATRate { get; set; }
            public virtual decimal CostApplied { get; set; }
            public virtual string EPNumber { get; set; }
            public virtual bool IsClosed { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual int RecordID { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Procurement.PurchaseOrderService where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchasedOrderService(PurchasedOrderService _ent)
            {
                int linenum = 0;
                //bool isbybulk = false;
                
                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseOrderService where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "PRNumber", _ent.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "ServiceType", _ent.ServiceType);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "ServiceQty", _ent.ServiceQty);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "UnitCost", _ent.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "TotalCost", _ent.TotalCost);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "IsAllowProgressBilling", _ent.IsAllowProgressBilling);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "IsVat", _ent.IsVat);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "VATCode", _ent.VATCode);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "VATRate", _ent.VATRate);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "CostApplied", _ent.CostApplied);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "EPNumber", _ent.EPNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "IsClosed", _ent.IsClosed);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "0", "Field9", _ent.Field9);

                DT2.Rows.Add("Procurement.PurchaseOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePurchasedOrderService(PurchasedOrderService _ent)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseOrderService", "cond", "DocNumber", _ent.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "cond", "LineNumber", _ent.LineNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "PRNumber", _ent.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "ServiceType", _ent.ServiceType);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "ServiceQty", _ent.ServiceQty);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "UnitCost", _ent.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "TotalCost", _ent.TotalCost);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "IsAllowProgressBilling", _ent.IsAllowProgressBilling);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "IsVat", _ent.IsVat);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "VATCode", _ent.VATCode);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "VATRate", _ent.VATRate);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "CostApplied", _ent.CostApplied);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "EPNumber", _ent.EPNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "IsClosed", _ent.IsClosed);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "set", "Field9", _ent.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePurchasedOrderService(PurchasedOrderService PurchasedOrderService)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseOrderService", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderService", "cond", "LineNumber", PurchasedOrderService.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseOrderService where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public class RefTransaction
        {
            public virtual PurchasedOrder Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='PRCPOM' OR  A.TransType='PRCPOM') ", Conn);
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
            //a = Gears.RetriveData2("select * from Procurement.PurchaseOrder where DocNumber = '" + DocNumber + "'");

            a = Gears.RetriveData2("SELECT *,(SELECT  TOP 1 STUFF((SELECT ';' + CAST(PRNumber AS VARCHAR(MAX)) "
                                    + "FROM (SELECT DISTINCT DocNumber, PRNumber FROM Procurement.PurchaseOrderDetail WHERE DocNumber ='" + DocNumber + "') "
                                    + "AS X ORDER BY PRNumber  ASC FOR XML PATH(''), TYPE).value('.','VARCHAR(MAX)'),1,1,'') "
                                    + "FROM Procurement.PurchaseOrderDetail WHERE DocNumber ='" + DocNumber + "') AS PRNumber FROM Procurement.PurchaseOrder "
                                    + "WHERE DocNumber='" + DocNumber + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                TargetDeliveryDate = dtRow["TargetDeliveryDate"].ToString();
                Status = dtRow["Status"].ToString();
                DateCompleted = dtRow["DateCompleted"].ToString();
                ReceivingWarehouse = dtRow["ReceivingWarehouse"].ToString();
                CancellationDate = dtRow["CancellationDate"].ToString();
                ContactPerson = dtRow["ContactPerson"].ToString();
                CommitmentDate = dtRow["CommitmentDate"].ToString();
                QuotationNo = dtRow["QuotationNo"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                Broker = dtRow["Broker"].ToString();
                PRNumberH = dtRow["PRNumber"].ToString();

                
                Currency = dtRow["Currency"].ToString();
                Terms = Convert.ToInt32(Convert.IsDBNull(dtRow["Terms"]) ? 0 : dtRow["Terms"]);
                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? 0 : dtRow["TotalQty"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVATableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVATableAmount"]) ? 0 : dtRow["GrossVATableAmount"]);
                NonVATableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVATableAmount"]) ? 0 : dtRow["NonVATableAmount"]);
                VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);
                TotalFreight = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalFreight"]) ? 0 : dtRow["TotalFreight"]);
                WithholdingTax = Convert.ToDecimal(Convert.IsDBNull(dtRow["WithholdingTax"]) ? 0 : dtRow["WithholdingTax"]);



                //ExchangeRate = dtRow["ExchangeRate"].ToString();
                
                //DownloadDate = dtRow["DownloadDate"].ToString();
                //PrintCount = dtRow["PrintCount"].ToString();
                //VersionNumber = dtRow["VersionNumber"].ToString();

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                ForceClosedBy = dtRow["ForceClosedBy"].ToString();
                ForceClosedDate = dtRow["ForceClosedDate"].ToString();
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();
                ReleasedBy = dtRow["ReleasedBy"].ToString();
                ReleasedDate = dtRow["ReleasedDate"].ToString();
                IsWithPR = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithPR"]) ? false : dtRow["IsWithPR"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);
                IsWithInvoice = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithInvoice"]) ? false : dtRow["IsWithInvoice"]);
                //IsSubmitted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSubmitted"]) ? false : dtRow["IsSubmitted"]);
                //IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);
                IsReleased = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsReleased"]) ? false : dtRow["IsReleased"]);

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
        public void InsertData(PurchasedOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Status", _ent.Status);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "DateCompleted", _ent.DateCompleted);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "ReceivingWarehouse", _ent.ReceivingWarehouse);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "CancellationDate", _ent.CancellationDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "CommitmentDate", _ent.CommitmentDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "QuotationNo", _ent.QuotationNo);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Remarks", _ent.Remarks);

            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "ExchangeRate", _ent.ExchangeRate);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "SubmittedBy", _ent.SubmittedBy);

            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "SubmittedDate", _ent.SubmittedDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "LastEditedBy", _ent.LastEditedBy);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "LastEditedDate", _ent.LastEditedDate);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "DownloadDate", _ent.DownloadDate);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "PrintCount", _ent.PrintCount);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "VersionNumber", _ent.VersionNumber);

            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "IsWithPR", _ent.IsWithPR);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "IsWithInvoice", _ent.IsWithInvoice);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "IsSubmitted", _ent.IsSubmitted);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "0", "IsWithDetail", "False");

            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Currency", _ent.Currency);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Terms", _ent.Terms);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "GrossVATableAmount", _ent.GrossVATableAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "NonVATableAmount", _ent.NonVATableAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "TotalFreight", _ent.TotalFreight);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "WithholdingTax", _ent.WithholdingTax);



            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseOrder", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(PurchasedOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseOrder", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Status", "N");
            //DT1.Rows.Add("Procurement.PurchaseOrder", "set", "DateCompleted", _ent.DateCompleted);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "ReceivingWarehouse", _ent.ReceivingWarehouse);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "CancellationDate", _ent.CancellationDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "CommitmentDate", _ent.CommitmentDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "QuotationNo", _ent.QuotationNo);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Broker", _ent.Broker);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "ExchangeRate", _ent.ExchangeRate);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "SubmittedBy", _ent.SubmittedBy);

            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "SubmittedDate", _ent.SubmittedDate);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "AddedDate", _ent.AddedDate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "DownloadDate", _ent.DownloadDate);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "PrintCount", _ent.PrintCount);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "VersionNumber", _ent.VersionNumber);

            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "IsWithPR", _ent.IsWithPR);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "IsWithInvoice", _ent.IsWithInvoice);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "IsSubmitted", _ent.IsSubmitted);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Procurement.PurchasedOrder", "set", "IsWithDetail", "0");
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "PRNumber", _ent.PRNumberH);

            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Currency", _ent.Currency);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "GrossVATableAmount", _ent.GrossVATableAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "NonVATableAmount", _ent.NonVATableAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "VATAmount", _ent.VATAmount);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "TotalFreight", _ent.TotalFreight);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "WithholdingTax", _ent.WithholdingTax);



            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseOrder", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
         Functions.AuditTrail("PRCPOM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(PurchasedOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.PurchaseOrder", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRCPOM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
