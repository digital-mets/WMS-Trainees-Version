using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ProductTransform
    {
        // 11/28/2023

        private static string Doc;
        private static string Conn { get; set; }
        private static string CustomerCode { get; set; }
        private static string ocn { get; set; }
        private static string docdate { get; set; }   
        public virtual string Connection { get; set; }

        public virtual string StorerKey { get; set; }
        public virtual string Docnumber { get; set; }
        public virtual string OCNdocnumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string RefDocDate { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }


        public virtual IList<ProductTransform> Detail { get; set; }


        public class ProductTransformDetail
        {
            public virtual ProductTransform Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string RefDoc { get; set; }
            public virtual string Customercode { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string PalletID { get; set; }
            public virtual string Itemcode { get; set; }
            public virtual string Batchno { get; set; }
            public virtual string UOM { get; set; }
            public virtual decimal Kilos { get; set; }
            public virtual decimal Quantity { get; set; }
            public virtual DateTime ExpiryDate { get; set; }
            public virtual DateTime MfgDate { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)//ADD CONN
            {
                // the pallet here is the declared variable from the top and updated the value below
                
                DataTable a;
                try {
                    a = Gears.RetriveData2("Select * from wms.[ProductTransformDetail] where ReferenceDocNumber ='" + Doc + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddProductDetail(ProductTransformDetail ProductTransformDetail)
            {
                int linenum = 0;

                //check if there is already existing value base from the query so that it wont contain any duplicates inside the sql
                DataTable checkData = Gears.RetriveData2("Select * from wms.[ProductTransformDetail] where ReferenceDocNumber ='" + Doc + "' and DocNumber ='" + ocn + "' and Customercode = '" + CustomerCode + "' and PalletID = '" + ProductTransformDetail.PalletID + "'", Conn);
                if (checkData.Rows.Count > 0)
                {
                    DeleteProductDetail(ProductTransformDetail);
                }
                // the product detail here is the declared variable from the top and updated the value below
                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from [WMS].[ProductTransformDetail]"
                    , Conn);//ADD CONN
               
                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                  
                }
                catch {

                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

               
                if (ProductTransformDetail.RefDoc == null && ProductTransformDetail.DocNumber == null && ProductTransformDetail.Customercode == null)
                {
                    DT1.Rows.Add("WMS.ProductTransformDetail", "0", "ReferenceDocNumber", Doc);
                    DT1.Rows.Add("WMS.ProductTransformDetail", "0", "DocNumber", ocn);
                    DT1.Rows.Add("WMS.ProductTransformDetail", "0", "Customercode", CustomerCode);
                }
                else
                {
                    DT1.Rows.Add("WMS.ProductTransformDetail", "0", "ReferenceDocNumber", ProductTransformDetail.RefDoc);
                    DT1.Rows.Add("WMS.ProductTransformDetail", "0", "DocNumber", ProductTransformDetail.DocNumber);
                    DT1.Rows.Add("WMS.ProductTransformDetail", "0", "Customercode", ProductTransformDetail.Customercode);

                }

                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "Batchno", ProductTransformDetail.Batchno);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "Itemcode", ProductTransformDetail.Itemcode);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "UOM", ProductTransformDetail.UOM);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "ExpiryDate", ProductTransformDetail.ExpiryDate);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "Quantity", ProductTransformDetail.Quantity);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "Kilos", ProductTransformDetail.Kilos);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "PalletID", ProductTransformDetail.PalletID);
                DT1.Rows.Add("WMS.ProductTransformDetail", "0", "MfgDate", ProductTransformDetail.MfgDate);

                DT2.Rows.Add("WMS.ProductTransform", "cond", "Docnumber", Doc);
                DT2.Rows.Add("WMS.ProductTransform", "set", "IsWithDetail", "True");


                Gears.CreateData(DT1, Conn);//ADD CONN
                Gears.UpdateData(DT2, Conn);//ADD CONN
            }
            public void UpdateProductDetail(ProductTransformDetail ProductTransformDetail)
            {       
                
                // the product detail here is the declared variable from the top and updated the value below
                //the referencedocnumber here is the docnumber of the current transaction
                //the docnumber here is the ocn docnumber from the header

                ////check if there's already existing value base from the query so that it wont contain any duplicates inside the sql
                //DataTable checkData = Gears.RetriveData2("Select * from wms.[ProductTransformDetail] where ReferenceDocNumber ='" + Doc + "' and DocNumber ='" + ocn + "' and Customercode = '" + CustomerCode + "'", Conn);
                //if (checkData.Rows.Count > 0)
                //{
                //    DeleteProductDetail(ProductTransformDetail);
                //}
              
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

             
                DT1.Rows.Add("WMS.ProductTransformDetail", "cond", "ReferenceDocNumber", Doc);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "DocNumber", ProductTransformDetail.DocNumber);
                DT1.Rows.Add("WMS.ProductTransformDetail", "cond", "LineNumber", ProductTransformDetail.LineNumber);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "Customercode", ProductTransformDetail.Customercode);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "Batchno", ProductTransformDetail.Batchno);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "Itemcode", ProductTransformDetail.Itemcode);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "UOM", ProductTransformDetail.UOM);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "ExpiryDate", ProductTransformDetail.ExpiryDate);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "MfgDate", ProductTransformDetail.MfgDate);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "Quantity", ProductTransformDetail.Quantity);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "Kilos", ProductTransformDetail.Kilos);
                DT1.Rows.Add("WMS.ProductTransformDetail", "set", "PalletID", ProductTransformDetail.PalletID);

               
               
               

                Gears.UpdateData(DT1, Conn);//ADD CONN
            }
            public void DeleteProductDetail(ProductTransformDetail ProductTransformDetail)
            {
                

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.ProductTransformDetail", "cond", "ReferenceDocNumber", Doc);
                DT1.Rows.Add("WMS.ProductTransformDetail", "cond", "LineNumber", ProductTransformDetail.LineNumber);





                DataTable count = Gears.RetriveData2("select * from [WMS].[ProductTransformDetail] where ReferenceDocNumber ='" + Doc + "' and LineNumber <> '" + ProductTransformDetail.LineNumber + "'", Conn);//ADD CONN

                Gears.DeleteData(DT1, Conn);//ADD CONN
                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.ProductTransform", "cond", "DocNumber", Doc);
                    DT2.Rows.Add("WMS.ProductTransform", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);//ADD CONN
                }   
                //if (count.Rows.Count < 1){
                //    foreach (DataRow dtRow in count.Rows)
                //    {
                //        DT2.Rows.Add("WMS.ProductTransformDetail", "cond", "ReferenceDocNumber", Doc);
                //        DT2.Rows.Add("WMS.ProductTransformDetail", "cond", "LineNumber", dtRow["LineNumber"].ToString());
                //        Gears.UpdateData(DT2, Conn);
                //    }
                //}   
                
            }  
        }

        public DataTable getdata(string DocNumber, string Conn)//ADD CONN
        {


            DataTable a;
          
            if (DocNumber != null)
            {
                Doc = DocNumber;
                a = Gears.RetriveData2("select * from WMS.[ProductTransform] where Docnumber = '" + Doc + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows){
                    DocNumber = dtRow["DocNumber"].ToString();
                    DocDate = dtRow["DocDate"].ToString();
                    RefDocDate = dtRow["ReferenceDocDate"].ToString();
                    OCNdocnumber = dtRow["OCNDocNumber"].ToString();
                    ocn = OCNdocnumber;
                    StorerKey = dtRow["CustomerCode"].ToString();
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                }
                
            }
            else {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
           
            
            return a;
        }
        public void InsertData(ProductTransform _ent)
        {
            Doc = _ent.Docnumber;//get the value from the txtPallet and ser the first value that is inserted in the header panel
            Conn = _ent.Connection;//ADD CONN
            CustomerCode = _ent.StorerKey;
            ocn = _ent.OCNdocnumber;
            docdate = _ent.RefDocDate;
            DateTime now = DateTime.Now;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            

            DT1.Rows.Add("WMS.ProductTransform", "0", "Docnumber", _ent.Docnumber);
            DT1.Rows.Add("WMS.ProductTransform", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ProductTransform", "0", "ReferenceDocDate", _ent.RefDocDate);
            DT1.Rows.Add("WMS.ProductTransform", "0", "OCNDocNumber", _ent.OCNdocnumber);
            DT1.Rows.Add("WMS.ProductTransform", "0", "CustomerCode", _ent.StorerKey);
            DT1.Rows.Add("WMS.ProductTransform", "0", "IsWithDetail", "False");
            DT1.Rows.Add("WMS.ProductTransform", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.ProductTransform", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

           
         

            Gears.CreateData(DT1, _ent.Connection);//ADD CONN
            Functions.AuditTrail("WMSPTRANS", _ent.Docnumber, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);//KMM add Conn
        }
        public void UpdateData(ProductTransform _ent)
        {
            Doc = _ent.Docnumber;//get the value from the txtDocnumber and ser the first value that is inserted in the header panel
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            Conn = _ent.Connection;
            CustomerCode = _ent.StorerKey;
            ocn = _ent.OCNdocnumber;
            docdate = _ent.RefDocDate;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.ProductTransform", "cond", "Docnumber", _ent.Docnumber);
            DT1.Rows.Add("WMS.ProductTransform", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.ProductTransform", "set", "ReferenceDocDate", _ent.RefDocDate);
            DT1.Rows.Add("WMS.ProductTransform", "set", "OCNDocNumber", _ent.OCNdocnumber);
            DT1.Rows.Add("WMS.ProductTransform", "set", "CustomerCode", _ent.StorerKey);
            DT1.Rows.Add("WMS.ProductTransform", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.ProductTransform", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

       
         

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//ADD CONN

            Functions.AuditTrail("WMSPTRANS", Docnumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn
        }
        public void DeleteData(ProductTransform _ent)
        {
            Conn = _ent.Connection;//ADD CONN
         
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
      

            DT1.Rows.Add("WMS.ProductTransform", "cond", "Docnumber", _ent.Docnumber);

            //Delete each row from product transform detail when deleting the current Docnumber of product transform
            DataTable count = Gears.RetriveData2("select * from [WMS].[ProductTransformDetail] where ReferenceDocNumber ='" + _ent.Docnumber + "'", Conn);//ADD CONN
            foreach (DataRow dtRow in count.Rows) {
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT2.Rows.Add("WMS.ProductTransformDetail", "cond", "ReferenceDocNumber", dtRow["ReferenceDocNumber"].ToString());
                DT2.Rows.Add("WMS.ProductTransformDetail", "cond", "LineNumber", dtRow["LineNumber"].ToString());
                Gears.DeleteData(DT2, Conn);//ADD CONN
            }

            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
           
            Functions.AuditTrail("WMSPTRANS", Docnumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn
        }

    }
}
