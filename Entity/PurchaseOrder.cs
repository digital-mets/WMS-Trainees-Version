using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PurchaseOrder
    {

        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TargetDeliveryDate { get; set; }
        public virtual string DateCompleted { get; set; }
        public virtual double TotalFreight { get; set; }
        public virtual IList<PODetail> Detail { get; set; }


        public class PODetail 
        {
            public virtual PurchaseOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal OrderQty { get; set; }

            public DataTable getdetail(string DocNumber)
            {
                
                DataTable a;
                try { 
                a = Gears.RetriveData2("select * from Trading.PurchaseOrderDetail where DocNumber='" + DocNumber + "' order by LineNumber");
                return a;
                }
                catch (Exception e)
                {
                a = null;
                return a;
                }
            }

            public void AddPODetail(PODetail podetails)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from trading.purchaseorderdetail where docnumber = '" + Docnum + "'");

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch {
                    linenum = 1;  
                }

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "0", "LineNumber", linenum);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "0", "ItemCode", podetails.ItemCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "0", "ColorCode", podetails.ColorCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "0", "ClassCode", podetails.ClassCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "0", "SizeCode", podetails.SizeCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "0", "OrderQty", podetails.OrderQty);
                DT2.Rows.Add("Trading.PurchaseOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Trading.PurchaseOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1);
                Gears.UpdateData(DT2);
            }

            public void UpdatePODetail(PODetail podetails)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "cond", "DocNumber", podetails.DocNumber);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "cond", "LineNumber", podetails.LineNumber);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "set", "ItemCode", podetails.ItemCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "set", "ColorCode", podetails.ColorCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "set", "ClassCode", podetails.ClassCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "set", "SizeCode", podetails.SizeCode);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "set", "OrderQty", podetails.OrderQty);

                Gears.UpdateData(DT1);
            }
            public void DeletePODetail(PODetail podetails)
            {
                

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "cond", "DocNumber", podetails.DocNumber);
                DT1.Rows.Add("Trading.PurchaseOrderDetail", "cond", "LineNumber", podetails.LineNumber);
                
                
                Gears.DeleteData(DT1);

                DataTable count = Gears.RetriveData2("select * from trading.purchaseorderdetail where docnumber = '" + Docnum + "'");

                if (count.Rows.Count < 1){
                DT2.Rows.Add("Trading.PurchaseOrder", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Trading.PurchaseOrder", "set", "IsWithDetail", "False");
                Gears.UpdateData(DT2);
                }   
                
            }  
        }

        public DataTable getdata(string DocNumber)
        {
            DataTable a;

            if (DocNumber != null)
            {
                a = Gears.RetriveData2("select * from Trading.PurchaseOrder where DocNumber = '" + DocNumber + "'");
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    SupplierCode = dtRow["SupplierCode"].ToString();
                    TargetDeliveryDate = dtRow["TargetDeliveryDate"].ToString();
                    DocDate = dtRow["DocDate"].ToString();
                    TotalFreight = String.IsNullOrEmpty(dtRow["TotalFreight"].ToString()) ? 0 : Convert.ToDouble(dtRow["TotalFreight"].ToString());
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as DocNumber,'' as SupplierCode,'' as DocDate,'' as TargetDeliveryDate,'' as DateCompleted,'' as TotalFreight");
            }
            
            return a;
        }
        public void InsertData(PurchaseOrder _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Trading.PurchaseOrder", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Trading.PurchaseOrder", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Trading.PurchaseOrder", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Trading.PurchaseOrder", "0", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Trading.PurchaseOrder", "0", "DateCompleted", _ent.DateCompleted);
            DT1.Rows.Add("Trading.PurchaseOrder", "0", "TotalFreight", _ent.TotalFreight);
            DT1.Rows.Add("Trading.PurchaseOrder", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1);
        }

        public void UpdateData(PurchaseOrder _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Trading.PurchaseOrder", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Trading.PurchaseOrder", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Trading.PurchaseOrder", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Trading.PurchaseOrder", "set", "TargetDeliveryDate", _ent.TargetDeliveryDate);
            DT1.Rows.Add("Trading.PurchaseOrder", "set", "DateCompleted", _ent.DateCompleted);
            DT1.Rows.Add("Trading.PurchaseOrder", "set", "TotalFreight", _ent.TotalFreight);
          
            string strErr = Gears.UpdateData(DT1);
        }  
    }
}
