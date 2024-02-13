using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;
using System.IO;
namespace Entity
{
    public class MaterialOrder

    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        private static string Param;
        private static string Doc;
        
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string OrderDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string Step { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string ProductionSite { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string WorkWeek { get; set; }
        public virtual string Year { get; set; }

    
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string RevisedBy { get; set; }
        public virtual string RevisedDate { get; set; }
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
        public virtual string Picture1 { get; set; }
        public virtual string RRemarks { get; set; }
        public virtual string Parameters { get; set; }
        public virtual string Docno { get; set; }
        public virtual string MaterialOrderNo { get; set; }
        public virtual IList<MaterialOrderDetail> Detail { get; set; }


        public class MaterialOrderDetail
        {

            public virtual MaterialOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string SKUCode { get; set; }

            public virtual string LineNumber { get; set; }

            public virtual string ItemDescription { get; set; }
            public virtual string PackagingType { get; set; }
            public virtual decimal BatchWeight { get; set; }
            public virtual decimal Day1 { get; set; }
            public virtual decimal Day2 { get; set; }
            public virtual decimal Day3 { get; set; }
            public virtual decimal Day4 { get; set; }
            public virtual decimal Day5 { get; set; }
            public virtual decimal Day6 { get; set; }
            public virtual decimal Day7 { get; set; }
           
            
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select SKUCode AS SAPCode ,* from Production.MaterialOrderDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddMaterialOrderDetail(MaterialOrderDetail MaterialOrderDetail)
            {

                int linenum = 0;
                string doc = MaterialOrder.Doc;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.MaterialOrderDetail where docnumber = '" + doc + "'", Conn);

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

             
                

                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "DocNumber", doc);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "SKUCode", MaterialOrderDetail.SKUCode);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "ItemDescription", MaterialOrderDetail.ItemDescription);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "PackagingType", MaterialOrderDetail.PackagingType);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "BatchWeight", MaterialOrderDetail.BatchWeight);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day1", MaterialOrderDetail.Day1);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day2", MaterialOrderDetail.Day2);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day3", MaterialOrderDetail.Day3);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day4", MaterialOrderDetail.Day4);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day5", MaterialOrderDetail.Day5);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day6", MaterialOrderDetail.Day6);
                DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day7", MaterialOrderDetail.Day7);
               



                //DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateMaterialOrderDetail(MaterialOrderDetail MaterialOrderDetail)
            {
                int linenum = 0;
                string par = MaterialOrder.Param;
                string doc = MaterialOrder.Doc;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                if (par == "ReviseTrans")
                {
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "DocNumber", doc);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "LineNumber", MaterialOrderDetail.LineNumber);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "SKUCode", MaterialOrderDetail.SKUCode);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "ItemDescription", MaterialOrderDetail.ItemDescription);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "PackagingType", MaterialOrderDetail.PackagingType);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "BatchWeight", MaterialOrderDetail.BatchWeight);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day1", MaterialOrderDetail.Day1);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day2", MaterialOrderDetail.Day2);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day3", MaterialOrderDetail.Day3);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day4", MaterialOrderDetail.Day4);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day5", MaterialOrderDetail.Day5);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day6", MaterialOrderDetail.Day6);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "0", "Day7", MaterialOrderDetail.Day7);
                    Gears.CreateData(DT1, Conn);
                }
                else
                {

                    DT1.Rows.Add("Production.MaterialOrderDetail", "cond", "DocNumber", MaterialOrder.Docnum);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "cond", "LineNumber", MaterialOrderDetail.LineNumber);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "SKUCode", MaterialOrderDetail.SKUCode);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "ItemDescription", MaterialOrderDetail.ItemDescription);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "PackagingType", MaterialOrderDetail.PackagingType);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "BatchWeight", MaterialOrderDetail.BatchWeight);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "Day1", MaterialOrderDetail.Day1);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "Day2", MaterialOrderDetail.Day2);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "Day3", MaterialOrderDetail.Day3);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "Day4", MaterialOrderDetail.Day4);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "Day5", MaterialOrderDetail.Day5);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "Day6", MaterialOrderDetail.Day6);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "set", "Day7", MaterialOrderDetail.Day7);
                    Gears.UpdateData(DT1, Conn);
                }
               

            }
            public void DeleteMaterialOrderDetail(MaterialOrderDetail MaterialOrderDetail)
            {
                string doc = MaterialOrder.Doc;
                if (doc == MaterialOrderDetail.DocNumber) { 
                    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                    Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                    DT1.Rows.Add("Production.MaterialOrderDetail", "cond", "DocNumber", MaterialOrderDetail.DocNumber);
                    DT1.Rows.Add("Production.MaterialOrderDetail", "cond", "LineNumber", MaterialOrderDetail.LineNumber);
                    Gears.DeleteData(DT1, Conn);
                }
                else { }
                


                //DataTable count = Gears.RetriveData2("select * from Production.MaterialOrderDetail where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }





        }
        public class RefTransaction
        {
            public virtual MaterialOrder Parent { get; set; }
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
                    a = Gears.RetriveData2("select RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDPPO' OR  A.TransType='PRDPPO') ", Conn);
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


            a = Gears.RetriveData2("select * from Production.materialOrder where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                OrderDate = dtRow["OrderDate"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                ProductionSite = dtRow["ProductionSite"].ToString();
                WorkWeek = dtRow["WorkWeek"].ToString();
                Year = dtRow["Year"].ToString();
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
                RevisedBy = dtRow["RevisedBy"].ToString();
                RevisedDate = dtRow["RevisedDate"].ToString();
                Picture1 = dtRow["Picture1"].ToString();
                RRemarks = dtRow["RRemarks"].ToString();
                //Picture2 = dtRow["Picture2"].ToString();

            }

            return a;
        }
        
        public void InsertData(MaterialOrder _ent)
        {
            string doc = _ent.DocNumber.Split('-')[0];
            string revise = _ent.DocNumber.Split('-')[1];
            Param = _ent.Parameters;

            Doc = _ent.DocNumber;
            string dc = _ent.Docno;
            string material = _ent.DocNumber.ToString().Substring(0, 2) + "-WW" + _ent.WorkWeek + "-" + _ent.DocNumber.ToString().Substring(2, 8);


            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
           
                DT1.Rows.Add("Production.MaterialOrder", "0", "DocNumber", _ent.DocNumber);
                DT1.Rows.Add("Production.MaterialOrder", "0", "CustomerCode", _ent.CustomerCode);
                DT1.Rows.Add("Production.MaterialOrder", "0", "DocDate", _ent.DocDate);
                DT1.Rows.Add("Production.MaterialOrder", "0", "OrderDate", _ent.OrderDate);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Remarks", _ent.Remarks);
                DT1.Rows.Add("Production.MaterialOrder", "0", "ProductionSite", _ent.ProductionSite);
                DT1.Rows.Add("Production.MaterialOrder", "0", "WorkWeek", _ent.WorkWeek);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Year", _ent.Year);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field1", _ent.Field1);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field2", _ent.Field2);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field3", _ent.Field3);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field4", _ent.Field4);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field5", _ent.Field5);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field6", _ent.Field6);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field7", _ent.Field7);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field8", _ent.Field8);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Field9", _ent.Field9);
                DT1.Rows.Add("Production.MaterialOrder", "0", "AddedBy", _ent.AddedBy);
                DT1.Rows.Add("Production.MaterialOrder", "0", "Picture1", _ent.Picture1);
                DT1.Rows.Add("Production.MaterialOrder", "0", "RRemarks", _ent.RRemarks);
                //DT1.Rows.Add("Production.MaterialOrder", "0", "Picture2", _ent.Picture2);
                DT1.Rows.Add("Production.MaterialOrder", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                if (Param == "ReviseTrans")
                {                                                                                                   
                   
                    if (dc.ToLower().Contains('w'))
                    {
                        DT1.Rows.Add("Production.MaterialOrder", "0", "MaterialOrderNo", _ent.Docno);
                    }
                    else
                    {
                        DT1.Rows.Add("Production.MaterialOrder", "0", "MaterialOrderNo", material);
                    }
                    DataTable ResetNew = Gears.RetriveData2("Update [Production].[MaterialOrder] set Status = 'Revised', RevisedDate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , RevisedBy = '" + _ent.RevisedBy + "' Where Docnumber = '" + _ent.Docno + "' ", _ent.Connection);
                 }else{
                }
                //if (docn.ToLower().Contains('w')) {
                //DT1.Rows.Add("Production.MaterialOrder", "0", "MaterialOrderNo", _ent.Docno);
                //}
                //else
                //{
                //    DT1.Rows.Add("Production.MaterialOrder", "0", "MaterialOrderNo", _ent.DocNumber.Split('-')[0] + "'WW'" + _ent.WorkWeek + _ent.DocNumber.Split('-')[1]);
                //}


            
            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(MaterialOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;        //ADD CONN
            Param = _ent.Parameters;
            Doc = _ent.DocNumber;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.MaterialOrder", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.MaterialOrder", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.MaterialOrder", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.MaterialOrder", "set", "OrderDate", _ent.OrderDate);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.MaterialOrder", "set", "ProductionSite", _ent.ProductionSite);
            DT1.Rows.Add("Production.MaterialOrder", "set", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Year", _ent.Year);
            string docn = _ent.Docno;
            
            
            if (docn.ToLower().Contains('w'))
            {
                DT1.Rows.Add("Production.MaterialOrder", "set", "MaterialOrderNo", _ent.Docno);
            }
            else if (docn.ToLower().Contains('-'))
            {
                string materialr = _ent.DocNumber.ToString().Substring(0, 2) + "-WW" + _ent.WorkWeek + "-" + _ent.DocNumber.ToString().Substring(2, 8);
                DT1.Rows.Add("Production.MaterialOrder", "set", "MaterialOrderNo", materialr);
            }
            else
            {
                string material = _ent.DocNumber.ToString().Substring(0, 2) + "-WW" + _ent.WorkWeek + "-" + _ent.DocNumber.ToString().Substring(2, 5);
                DT1.Rows.Add("Production.MaterialOrder", "set", "MaterialOrderNo", material);
            }
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.MaterialOrder", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.MaterialOrder", "set", "LastEditedDate",_ent.LastEditedDate);
            DT1.Rows.Add("Production.MaterialOrder", "set", "Picture1", _ent.Picture1);
            DT1.Rows.Add("Production.MaterialOrder", "set", "RRemarks", _ent.RRemarks);
            //DT1.Rows.Add("Production.MaterialOrder", "set", "Picture2", _ent.Picture2);

            
            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDMTO", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(MaterialOrder _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.MaterialOrder", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDMTO", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
