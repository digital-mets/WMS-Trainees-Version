using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{   
    public class PurchaseOrderRevision
    {
        //03-10-2016 KMM    add connection
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string PODocNumber { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal TotalQty { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TargetDelDate { get; set; }
        public virtual string CommDate { get; set; }
        public virtual string CancellationDate { get; set; }
        public virtual string Broker { get; set; }
        public virtual string OldSupplier { get; set; }
        public virtual string Supplier { get; set; }
        public virtual string PQReference { get; set; }

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
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }

        public virtual IList<PurchaseOrderRevisionDetail> Detail { get; set; }


        public class PurchaseOrderRevisionDetail
        {
            public virtual PurchaseOrderRevision Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PRNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal OldUnitCost { get; set; }
            public virtual decimal NewUnitCost { get; set; }
            public virtual decimal OldOrderQty { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual decimal AverageCost { get; set; }
            public virtual bool IsVAT { get; set; }
            public virtual string VATCode { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)//KMM add Conn
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select A.*, FullDesc AS ItemDescription from Procurement.PurchaseOrderRevisionDetail A left join Masterfile.Item B ON A.ItemCode = B.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);//KMM add Conn
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchaseOrderRevisionDetail(PurchaseOrderRevisionDetail PurchaseOrderRevisionDetail)
            {
                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseOrderRevisionDetail where docnumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "PRNumber", PurchaseOrderRevisionDetail.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "ItemCode", PurchaseOrderRevisionDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "ColorCode", PurchaseOrderRevisionDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "ClassCode", PurchaseOrderRevisionDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "SizeCode", PurchaseOrderRevisionDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "OldUnitCost", PurchaseOrderRevisionDetail.OldUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "NewUnitCost ", PurchaseOrderRevisionDetail.NewUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "OldOrderQty", PurchaseOrderRevisionDetail.OldOrderQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "OrderQty", PurchaseOrderRevisionDetail.OrderQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Unit ", PurchaseOrderRevisionDetail.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "StatusCode", PurchaseOrderRevisionDetail.StatusCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "UnitCost", PurchaseOrderRevisionDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "BaseQty", PurchaseOrderRevisionDetail.BaseQty);
                //DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "AverageCost", PurchaseOrderRevisionDetail.AverageCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "IsVAT", PurchaseOrderRevisionDetail.IsVAT);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "VATCode", PurchaseOrderRevisionDetail.VATCode);

                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field1", PurchaseOrderRevisionDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field2", PurchaseOrderRevisionDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field3", PurchaseOrderRevisionDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field4", PurchaseOrderRevisionDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field5", PurchaseOrderRevisionDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field6", PurchaseOrderRevisionDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field7", PurchaseOrderRevisionDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field8", PurchaseOrderRevisionDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "0", "Field9", PurchaseOrderRevisionDetail.Field9);

                DT2.Rows.Add("Procurement.PurchaseOrderRevision", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseOrderRevision", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

               

            }
            public void UpdatePurchaseOrderRevisionDetail(PurchaseOrderRevisionDetail PurchaseOrderRevisionDetail)
            {

                

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "cond", "DocNumber", PurchaseOrderRevisionDetail.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "cond", "LineNumber", PurchaseOrderRevisionDetail.LineNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "cond", "PRNumber", PurchaseOrderRevisionDetail.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "ItemCode", PurchaseOrderRevisionDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "ColorCode", PurchaseOrderRevisionDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "ClassCode", PurchaseOrderRevisionDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "SizeCode", PurchaseOrderRevisionDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "OldUnitCost", PurchaseOrderRevisionDetail.OldUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "NewUnitCost ", PurchaseOrderRevisionDetail.NewUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "OldOrderQty", PurchaseOrderRevisionDetail.OldOrderQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "OrderQty", PurchaseOrderRevisionDetail.OrderQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Unit", PurchaseOrderRevisionDetail.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "StatusCode", PurchaseOrderRevisionDetail.StatusCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "UnitCost", PurchaseOrderRevisionDetail.UnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "BaseQty", PurchaseOrderRevisionDetail.BaseQty);
                //DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "AverageCost", PurchaseOrderRevisionDetail.AverageCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "IsVAT", PurchaseOrderRevisionDetail.IsVAT);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "VATCode", PurchaseOrderRevisionDetail.VATCode);

                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field1", PurchaseOrderRevisionDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field2", PurchaseOrderRevisionDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field3", PurchaseOrderRevisionDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field4", PurchaseOrderRevisionDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field5", PurchaseOrderRevisionDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field6", PurchaseOrderRevisionDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field7", PurchaseOrderRevisionDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field8", PurchaseOrderRevisionDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "Field9", PurchaseOrderRevisionDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePurchaseOrderRevisionDetail(PurchaseOrderRevisionDetail PurchaseOrderRevisionDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "cond", "DocNumber", PurchaseOrderRevisionDetail.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "cond", "LineNumber", PurchaseOrderRevisionDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseOrderRevisionDetail where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public class PurchaseOrderRevisionService
        {
            public virtual PurchaseOrderRevision Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PRNumber { get; set; }
            public virtual string Service { get; set; }
            public virtual string Description { get; set; }
            public virtual decimal OldServiceQty { get; set; }
            public virtual decimal NewServiceQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal OldUnitCost { get; set; }
            public virtual decimal NewUnitCost { get; set; }
            public virtual decimal OldTotalCost { get; set; }
            public virtual decimal NewTotalCost { get; set; }
            public virtual bool AllowProgressBilling { get; set; }
            public virtual bool VatLiable { get; set; }
            public virtual string VatCode { get; set; }
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
                    a = Gears.RetriveData2("select * from Procurement.PurchaseOrderRevisionService where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchasedOrderService(PurchaseOrderRevisionService _ent)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseOrderRevisionService where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "PRNumber", _ent.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Service", _ent.Service);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "OldServiceQty", _ent.OldServiceQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "NewServiceQty", _ent.NewServiceQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "OldUnitCost", _ent.OldUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "NewUnitCost", _ent.NewUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "OldTotalCost", _ent.OldTotalCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "NewTotalCost", _ent.NewTotalCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "AllowProgressBilling", _ent.AllowProgressBilling);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "VatLiable", _ent.VatLiable);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "VatCode", _ent.VatCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "0", "Field9", _ent.Field9);

                DT2.Rows.Add("Procurement.PurchaseOrderRevision", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseOrderRevision", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePurchasedOrderService(PurchaseOrderRevisionService _ent)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "cond", "DocNumber", _ent.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "cond", "LineNumber", _ent.LineNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "PRNumber", _ent.PRNumber);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Service", _ent.Service);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "OldServiceQty", _ent.OldServiceQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "NewServiceQty", _ent.NewServiceQty);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "OldUnitCost", _ent.OldUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "NewUnitCost", _ent.NewUnitCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "OldTotalCost", _ent.OldTotalCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "NewTotalCost", _ent.NewTotalCost);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "AllowProgressBilling", _ent.AllowProgressBilling);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "VatLiable", _ent.VatLiable);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "VatCode", _ent.VatCode);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "set", "Field9", _ent.Field9); 


                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePurchasedOrderService(PurchaseOrderRevisionService PurchasedOrderService)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseOrderRevisionService", "cond", "LineNumber", PurchasedOrderService.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseOrderRevisionService where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseOrderRevision", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseOrderRevision", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        public class RefTransaction
        {
            public virtual PurchaseOrderRevision Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='PRPOR' OR  A.TransType='PRPOR') ", Conn);
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
            a = Gears.RetriveData2("select * from Procurement.PurchaseOrderRevision where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                PODocNumber = dtRow["PODocNumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQty"]) ? false : dtRow["TotalQty"]);
                DocDate = dtRow["Docdate"].ToString();
                TargetDelDate = dtRow["TargetDelDate"].ToString();
                CommDate = dtRow["CommDate"].ToString();
                CancellationDate = dtRow["CancellationDate"].ToString();
                Broker = dtRow["Broker"].ToString();
                OldSupplier = dtRow["OldSupplier"].ToString();
                Supplier = dtRow["Supplier"].ToString();
                PQReference = dtRow["PQReference"].ToString();

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
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
            }

            return a;
        }
        public void InsertData(PurchaseOrderRevision _ent)
        {

            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "PODocNumber", _ent.PODocNumber);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "TargetDelDate", _ent.TargetDelDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "CommDate", _ent.CommDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "CancellationDate", _ent.CancellationDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Broker", _ent.Broker);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "OldSupplier", _ent.OldSupplier);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Supplier", _ent.Supplier);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "PQReference", _ent.PQReference);

            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }

        public void UpdateData(PurchaseOrderRevision _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "PODocNumber", _ent.PODocNumber);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "TotalQty", _ent.TotalQty);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "TargetDelDate", _ent.TargetDelDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "CommDate", _ent.CommDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "CancellationDate", _ent.CancellationDate);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Broker", _ent.Broker);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "OldSupplier", _ent.OldSupplier);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Supplier", _ent.Supplier);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "PQReference", _ent.PQReference);

            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRPOR", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); //KMM add Conn
        }
        public void DeleteData(PurchaseOrderRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.PurchaseOrderRevision", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection); //KMM add Conn
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Procurement.PurchaseOrderRevisionDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);
            Functions.AuditTrail("PRPOR", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); //KMM add Conn
        }

    }
}