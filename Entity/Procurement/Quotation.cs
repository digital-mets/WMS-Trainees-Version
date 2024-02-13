using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Quotation
    {
        private static string Docnum;
        private static string Supp;
        private static string Conn;     //ADD CONN
        public virtual string Connection { get; set; }      //ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string SupplierTargetDate { get; set; }
        public virtual int LeadTime { get; set; }
        public virtual string PRDocNumber { get; set; }
        public virtual int Terms { get; set; }
        public virtual string FileNameOfQuotation { get; set; }
        public virtual string Supplier { get; set; }
        public virtual string Name { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string ValidFrom { get; set; }
        public virtual string ValidTo { get; set; }
        //public virtual string Status { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string UnApprovedBy { get; set; }
        public virtual string UnApprovedDate { get; set; }
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
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

        public virtual IList<QuotationDetail> Detail { get; set; }


        public class QuotationDetail
        {
            public virtual Quotation Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PRNo { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual bool IsVat { get; set; }
            public virtual decimal QuotedQty { get; set; }
            public virtual decimal QuotedCost { get; set; }

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
                    a = Gears.RetriveData2("select *, B.FullDesc AS ItemDescription from Procurement.QuotationDetail A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode where A.DocNumber='" + DocNumber + "' order by LineNumber", Conn);
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

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.QuotationDetail where DocNumber = '" + Docnum + "'", Conn);

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
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "PRNo", QuotationDetail.PRNo);

                DT1.Rows.Add("Procurement.QuotationDetail", "0", "ItemCode", QuotationDetail.ItemCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "ColorCode", QuotationDetail.ColorCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "ClassCode", QuotationDetail.ClassCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "SizeCode", QuotationDetail.SizeCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "IsVat", QuotationDetail.IsVat);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "QuotedQty", QuotationDetail.QuotedQty);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "QuotedCost", QuotationDetail.QuotedCost);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field1", QuotationDetail.Field1);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field2", QuotationDetail.Field2);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field3", QuotationDetail.Field3);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field4", QuotationDetail.Field4);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field5", QuotationDetail.Field5);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field6", QuotationDetail.Field6);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field7", QuotationDetail.Field7);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field8", QuotationDetail.Field8);
                DT1.Rows.Add("Procurement.QuotationDetail", "0", "Field9", QuotationDetail.Field9);

                //masterfile
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "ItemCode", QuotationDetail.ItemCode);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "ColorCode", QuotationDetail.ColorCode);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "ClassCode", QuotationDetail.ClassCode);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "SizeCode", QuotationDetail.SizeCode);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "QuotePrice", QuotationDetail.QuotedCost);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "Unit", 0);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "Price", 0);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "PriceCurrency", 0);
                DT3.Rows.Add("Masterfile.ItemSupplierPrice", "0", "Supplier", Supp); 
                
                DT2.Rows.Add("Procurement.Quotation", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.Quotation", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.CreateData(DT3, Conn); //masterfile
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateQuotationDetail(QuotationDetail QuotationDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.QuotationDetail", "cond", "DocNumber", QuotationDetail.DocNumber);
                DT1.Rows.Add("Procurement.QuotationDetail", "cond", "LineNumber", QuotationDetail.LineNumber);

                DT1.Rows.Add("Procurement.QuotationDetail", "set", "ItemCode", QuotationDetail.ItemCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "ColorCode", QuotationDetail.ColorCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "ClassCode", QuotationDetail.ClassCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "SizeCode", QuotationDetail.SizeCode);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "IsVat", QuotationDetail.IsVat);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "QuotedQty", QuotationDetail.QuotedQty);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "QuotedCost", QuotationDetail.QuotedCost);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field1", QuotationDetail.Field1);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field2", QuotationDetail.Field2);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field3", QuotationDetail.Field3);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field4", QuotationDetail.Field4);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field5", QuotationDetail.Field5);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field6", QuotationDetail.Field6);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field7", QuotationDetail.Field7);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field8", QuotationDetail.Field8);
                DT1.Rows.Add("Procurement.QuotationDetail", "set", "Field9", QuotationDetail.Field9);

                //masterfile
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "cond", "ItemCode", QuotationDetail.ItemCode);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "cond", "ColorCode", QuotationDetail.ColorCode);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "cond", "ClassCode", QuotationDetail.ClassCode);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "cond", "SizeCode", QuotationDetail.SizeCode);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "set", "QuotePrice", QuotationDetail.QuotedCost);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "set", "Unit", 0);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "set", "Price", 0);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "set", "PriceCurrency", 0);
                DT2.Rows.Add("Masterfile.ItemSupplierPrice", "cond", "Supplier", Supp);

                Gears.UpdateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void DeleteQuotationDetail(QuotationDetail QuotationDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.QuotationDetail", "cond", "DocNumber", QuotationDetail.DocNumber);
                DT1.Rows.Add("Procurement.QuotationDetail", "cond", "LineNumber", QuotationDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.QuotationDetail where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.Quotation", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.Quotation", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        public class RefTransaction
        {
            public virtual Quotation Parent { get; set; }
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
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRCQUM' OR  A.TransType='PRCQUM') ", Conn);
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
            a = Gears.RetriveData2("select * from Procurement.Quotation where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                SupplierTargetDate = dtRow["SupplierTargetDate"].ToString();
                LeadTime = Convert.ToInt32(Convert.IsDBNull(dtRow["LeadTime"]) ? 0 : dtRow["LeadTime"]);
                PRDocNumber = dtRow["PRDocNumber"].ToString();
                Terms = Convert.ToInt32(Convert.IsDBNull(dtRow["Terms"]) ? 0 : dtRow["Terms"]);
                FileNameOfQuotation = dtRow["FileNameOfQuotation"].ToString();
                ContactPerson = dtRow["ContactPerson"].ToString();
                Supplier = dtRow["Supplier"].ToString();
                Name = dtRow["Name"].ToString();
                ValidFrom = dtRow["ValidFrom"].ToString();
                ValidTo = dtRow["ValidTo"].ToString();
                
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                UnApprovedBy = dtRow["UnApprovedBy"].ToString();
                UnApprovedDate = dtRow["UnApprovedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();

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
        public void InsertData(Quotation _ent)
        {
            Docnum = _ent.DocNumber;
            Supp = _ent.Supplier;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.Quotation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.Quotation", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.Quotation", "0", "SupplierTargetDate", _ent.SupplierTargetDate);
            DT1.Rows.Add("Procurement.Quotation", "0", "LeadTime", _ent.LeadTime);
            DT1.Rows.Add("Procurement.Quotation", "0", "PRDocNumber", _ent.PRDocNumber);
            DT1.Rows.Add("Procurement.Quotation", "0", "Terms", _ent.Terms);
            DT1.Rows.Add("Procurement.Quotation", "0", "FileNameOfQuotation", _ent.FileNameOfQuotation);
            DT1.Rows.Add("Procurement.Quotation", "0", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Procurement.Quotation", "0", "Supplier", _ent.Supplier);
            DT1.Rows.Add("Procurement.Quotation", "0", "Name", _ent.Name);
            DT1.Rows.Add("Procurement.Quotation", "0", "ValidFrom", _ent.ValidFrom);
            DT1.Rows.Add("Procurement.Quotation", "0", "ValidTo", _ent.ValidTo);
            DT1.Rows.Add("Procurement.Quotation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Procurement.Quotation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Procurement.Quotation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.Quotation", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Procurement.Quotation", "0", "IsWithDetail", "False");

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(Quotation _ent)
        {
            Docnum = _ent.DocNumber;
            Supp = _ent.Supplier;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.Quotation", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.Quotation", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.Quotation", "set", "SupplierTargetDate", _ent.SupplierTargetDate);
            DT1.Rows.Add("Procurement.Quotation", "set", "LeadTime", _ent.LeadTime);
            DT1.Rows.Add("Procurement.Quotation", "set", "PRDocNumber", _ent.PRDocNumber);
            DT1.Rows.Add("Procurement.Quotation", "set", "Terms", _ent.Terms);
            DT1.Rows.Add("Procurement.Quotation", "set", "FileNameOfQuotation", _ent.FileNameOfQuotation);
            DT1.Rows.Add("Procurement.Quotation", "set", "ContactPerson", _ent.ContactPerson);
            DT1.Rows.Add("Procurement.Quotation", "set", "Supplier", _ent.Supplier);
            DT1.Rows.Add("Procurement.Quotation", "set", "Name", _ent.Name);
            DT1.Rows.Add("Procurement.Quotation", "set", "ValidFrom", _ent.ValidFrom);
            DT1.Rows.Add("Procurement.Quotation", "set", "ValidTo", _ent.ValidTo);
            DT1.Rows.Add("Procurement.Quotation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.Quotation", "set", "LastEditedDate", _ent.LastEditedDate);

            DT1.Rows.Add("Procurement.Quotation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.Quotation", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            //Gears.UpdateData(DT2);
            Functions.AuditTrail("PRCQUM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Quotation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.Quotation", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Procurement.QuotationDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);
            Functions.AuditTrail("PRCQUM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
