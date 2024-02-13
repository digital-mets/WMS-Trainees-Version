using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class SalesOrderRevision
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string SODocNumber { get; set; }
        public virtual string Remarks { get; set; }
         
        public virtual decimal NewQtyTotal { get; set; }
        public virtual decimal NewUnitPriceTotal { get; set; }
        public virtual string Customer { get; set; }

        public virtual string OldCustomerPONumber { get; set; }
        public virtual string NewCustomerPONumber { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
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

        public virtual IList<SalesOrderRevisionDetail> Detail { get; set; }


        public class SalesOrderRevisionDetail
        {
            public virtual SalesOrderRevision Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal OldQty { get; set; }
            public virtual decimal NewQty { get; set; }
            public virtual decimal OldUnitPrice { get; set; }
            public virtual decimal NewUnitPrice { get; set; }
            public virtual string Unit { get; set; }
            public virtual Boolean IsVAT { get; set; }
            public virtual string VATCode { get; set; }
            public virtual decimal Rate { get; set; }
            public virtual decimal DiscountRate { get; set; }
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
                    a = Gears.RetriveData2("select A.*, B.FullDesc AS ItemDescription from Sales.SalesOrderRevisionDetail A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode where A.DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddSalesOrderRevisionDetail(SalesOrderRevisionDetail SalesOrderRevisionDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Sales.SalesOrderRevisionDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "ItemCode", SalesOrderRevisionDetail.ItemCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "ColorCode", SalesOrderRevisionDetail.ColorCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "ClassCode", SalesOrderRevisionDetail.ClassCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "SizeCode", SalesOrderRevisionDetail.SizeCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "OldQty", SalesOrderRevisionDetail.OldQty);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "NewQty", SalesOrderRevisionDetail.NewQty);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "OldUnitPrice", SalesOrderRevisionDetail.OldUnitPrice);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "NewUnitPrice", SalesOrderRevisionDetail.NewUnitPrice);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Unit", SalesOrderRevisionDetail.Unit);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "IsVAT", SalesOrderRevisionDetail.IsVAT);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "VATCode", SalesOrderRevisionDetail.VATCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Rate", SalesOrderRevisionDetail.Rate);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "DiscountRate", SalesOrderRevisionDetail.DiscountRate);

                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field1", SalesOrderRevisionDetail.Field1);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field2", SalesOrderRevisionDetail.Field2);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field3", SalesOrderRevisionDetail.Field3);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field4", SalesOrderRevisionDetail.Field4);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field5", SalesOrderRevisionDetail.Field5);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field6", SalesOrderRevisionDetail.Field6);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field7", SalesOrderRevisionDetail.Field7);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field8", SalesOrderRevisionDetail.Field8);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "0", "Field9", SalesOrderRevisionDetail.Field9);

                DT2.Rows.Add("Sales.SalesOrderRevision", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Sales.SalesOrderRevision", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateSalesOrderRevisionDetail(SalesOrderRevisionDetail SalesOrderRevisionDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "cond", "DocNumber", SalesOrderRevisionDetail.DocNumber);
                DT1.Rows.Add("Sa,es.SalesOrderRevisionDetail", "cond", "LineNumber", SalesOrderRevisionDetail.LineNumber);

                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "ItemCode", SalesOrderRevisionDetail.ItemCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "ColorCode", SalesOrderRevisionDetail.ColorCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "ClassCode", SalesOrderRevisionDetail.ClassCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "SizeCode", SalesOrderRevisionDetail.SizeCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "OldQty", SalesOrderRevisionDetail.OldQty);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "NewQty", SalesOrderRevisionDetail.NewQty);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "OldUnitPrice", SalesOrderRevisionDetail.OldUnitPrice);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "NewUnitPrice", SalesOrderRevisionDetail.NewUnitPrice);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Unit", SalesOrderRevisionDetail.Unit);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "IsVAT", SalesOrderRevisionDetail.IsVAT);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "VATCode", SalesOrderRevisionDetail.VATCode);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Rate", SalesOrderRevisionDetail.Rate);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "DiscountRate", SalesOrderRevisionDetail.DiscountRate);

                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field1", SalesOrderRevisionDetail.Field1);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field2", SalesOrderRevisionDetail.Field2);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field3", SalesOrderRevisionDetail.Field3);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field4", SalesOrderRevisionDetail.Field4);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field5", SalesOrderRevisionDetail.Field5);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field6", SalesOrderRevisionDetail.Field6);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field7", SalesOrderRevisionDetail.Field7);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field8", SalesOrderRevisionDetail.Field8);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "set", "Field9", SalesOrderRevisionDetail.Field9);
                
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteSalesOrderRevisionDetail(SalesOrderRevisionDetail SalesOrderRevisionDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "cond", "DocNumber", SalesOrderRevisionDetail.DocNumber);
                DT1.Rows.Add("Sales.SalesOrderRevisionDetail", "cond", "LineNumber", SalesOrderRevisionDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);
                //DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseRequestDetail where DocNumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Procurement.PurchaseRequest", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Procurement.PurchaseRequest", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}
            }
        }


        public class RefTransaction
        {
            public virtual SalesOrderRevision Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='SLSSOR' OR  A.TransType='SLSSOR') ", Conn);
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
            a = Gears.RetriveData2("select * from Sales.SalesOrderRevision where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                SODocNumber = dtRow["SODocNumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                NewQtyTotal = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewQtyTotal"]) ? 0 : dtRow["NewQtyTotal"]);
                NewUnitPriceTotal = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewUnitPriceTotal"]) ? 0 : dtRow["NewUnitPriceTotal"]);
                Customer = dtRow["Customer"].ToString();
                OldCustomerPONumber = dtRow["OldCustomerPONumber"].ToString();
                NewCustomerPONumber = dtRow["NewCustomerPONumber"].ToString();

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                //IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValited"]);
                //IsWithDetail =  Convert.ToBoolean(dtRow["IsWithDetail"].ToString());
                //IsSample = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSample"]) ? false : dtRow["IsSample"]);
                //IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);

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
        public void InsertData(SalesOrderRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "SODocNumber", _ent.SODocNumber);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "NewQtyTotal", _ent.NewQtyTotal);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "NewUnitPriceTotal", _ent.NewUnitPriceTotal);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Customer", _ent.Customer);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "OldCustomerPONumber", _ent.OldCustomerPONumber);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "NewCustomerPONumber", _ent.NewCustomerPONumber);

            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "SubmittedBy", "");
            DT1.Rows.Add("Sales.SalesOrderRevision", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(SalesOrderRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.SalesOrderRevision", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "SODocNumber", _ent.SODocNumber);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "NewQtyTotal", _ent.NewQtyTotal);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "NewUnitPriceTotal", _ent.NewUnitPriceTotal);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Customer", _ent.Customer);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "OldCustomerPONumber", _ent.OldCustomerPONumber);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "NewCustomerPONumber", _ent.NewCustomerPONumber);

            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Sales.SalesOrderRevision", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSSOR", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(SalesOrderRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.SalesOrderRevision", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSSOR", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
