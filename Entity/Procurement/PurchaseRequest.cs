using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PurchaseRequest
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TargetDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string StockNumber { get; set; }
        public virtual string Status { get; set; }
        public virtual string Remarks { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual string PODocNumber { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string ForceClosedBy { get; set; }
        public virtual string ForceClosedDate { get; set; }
        public virtual string CostCenter { get; set; }


        //2021-06-21    EMC add fields
        public virtual Int32 Year { get; set; }
        public virtual Int32 WorkWeek { get; set; }
        public virtual Int32 DayNo { get; set; }


        //2021-06-16    EMC add fields
        public virtual string RequestType { get; set; }
        public virtual string RequestingDeptCompany { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual bool IsAllowPartialHeader { get; set; }

        public virtual string Consignee { get; set; }
        public virtual string ConsigneeAddress { get; set; }
        public virtual string RequiredLoadingTime { get; set; }
        public virtual string ShipmentType { get; set; }
        public virtual string OCNRequestNumber { get; set; }
        public virtual string TransDate { get; set; }
        public virtual string RequestRef { get; set; }
        public virtual string TargetDeliveryDate { get; set; }
        public virtual string Employee { get; set; }
        public virtual string Department { get; set; }

        public virtual string ScrapRef { get; set; }


        public virtual IList<PurchaseRequestDetail> Detail { get; set; }

        public virtual IList<PurchaseRequestService> Detail2 { get; set; }
        public class PurchaseRequestDetail
        {
            public virtual PurchaseRequest Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal RequestQty { get; set; }
            public virtual string UnitBase { get; set; }
            public virtual decimal OrderQty { get; set; }
            public virtual Boolean IsAllowPartial { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }



            //2021-06-16    EMC add fields
            public virtual string ItemDescription { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual string ProcessSteps { get; set; }
            public virtual string OCNNumber { get; set; }
            public virtual decimal Qty { get; set; }

            public virtual string UOM { get; set; }
            public virtual string SpecialHandlingInstruction { get; set; }
            public virtual string Remarks { get; set; }
            public virtual decimal TotalQty { get; set; }
            public virtual string ScrapCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual bool IsPrinted { get; set; }

            public virtual DateTime ManuDate { get; set; }
            
            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,FullDesc from Procurement.PurchaseRequestDetail a left join masterfile.item b on a.ItemCode = b.ItemCode " +
                                            " where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchaseRequestDetail(PurchaseRequestDetail PurchaseRequestDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseRequestDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "LineNumber", strLine);


                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "ItemCode", PurchaseRequestDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "ColorCode", PurchaseRequestDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "ClassCode", PurchaseRequestDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "SizeCode", PurchaseRequestDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "RequestQty", PurchaseRequestDetail.RequestQty);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "UnitBase", PurchaseRequestDetail.UnitBase);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "OrderQty", PurchaseRequestDetail.OrderQty);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "IsAllowPartial", PurchaseRequestDetail.IsAllowPartial);



                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field1", PurchaseRequestDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field2", PurchaseRequestDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field3", PurchaseRequestDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field4", PurchaseRequestDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field5", PurchaseRequestDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field6", PurchaseRequestDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field7", PurchaseRequestDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field8", PurchaseRequestDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "Field9", PurchaseRequestDetail.Field9);

                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "0", "ManuDate", PurchaseRequestDetail.ManuDate);


                DT2.Rows.Add("Procurement.PurchaseRequest", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseRequest", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePurchaseRequestDetail(PurchaseRequestDetail PurchaseRequestDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "cond", "DocNumber", PurchaseRequestDetail.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "cond", "LineNumber", PurchaseRequestDetail.LineNumber);

                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "ItemCode", PurchaseRequestDetail.ItemCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "ColorCode", PurchaseRequestDetail.ColorCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "ClassCode", PurchaseRequestDetail.ClassCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "SizeCode", PurchaseRequestDetail.SizeCode);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "RequestQty", PurchaseRequestDetail.RequestQty);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "UnitBase", PurchaseRequestDetail.UnitBase);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "OrderQty", PurchaseRequestDetail.OrderQty);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "IsAllowPartial", PurchaseRequestDetail.IsAllowPartial);


                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field1", PurchaseRequestDetail.Field1);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field2", PurchaseRequestDetail.Field2);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field3", PurchaseRequestDetail.Field3);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field4", PurchaseRequestDetail.Field4);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field5", PurchaseRequestDetail.Field5);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field6", PurchaseRequestDetail.Field6);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field7", PurchaseRequestDetail.Field7);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field8", PurchaseRequestDetail.Field8);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "Field9", PurchaseRequestDetail.Field9);


                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "set", "ManuDate", PurchaseRequestDetail.ManuDate);


                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePurchaseRequestDetail(PurchaseRequestDetail PurchaseRequestDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "cond", "DocNumber", PurchaseRequestDetail.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseRequestDetail", "cond", "LineNumber", PurchaseRequestDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseRequestDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseRequest", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseRequest", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        public class PurchaseRequestService
        {
            public virtual PurchaseRequest Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string ServiceType { get; set; }
            public virtual string Description { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Unit { get; set; }
            public virtual bool IsAllowProgressBilling { get; set; }
            public virtual decimal ServicePOQty { get; set; }
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
                    a = Gears.RetriveData2("select * from Procurement.PurchaseRequestService where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPurchaseRequestDetail(PurchaseRequestService _ent)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseRequestService where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "ServiceType", _ent.ServiceType);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Qty", _ent.Qty);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "IsAllowProgressBilling", _ent.IsAllowProgressBilling);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "ServicePOQty", _ent.ServicePOQty);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "0", "Field9", _ent.Field9); 

                DT2.Rows.Add("Procurement.PurchaseRequest", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Procurement.PurchaseRequest", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePurchaseRequestDetail(PurchaseRequestService _ent)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseRequestService", "cond", "LineNumber", _ent.LineNumber);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "cond", "DocNumber", _ent.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "ServiceType", _ent.ServiceType);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Description", _ent.Description);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Qty", _ent.Qty);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Unit", _ent.Unit);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "IsAllowProgressBilling", _ent.IsAllowProgressBilling);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "ServicePOQty", _ent.ServicePOQty);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field1", _ent.Field1);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field2", _ent.Field2);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field3", _ent.Field3);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field4", _ent.Field4);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field5", _ent.Field5);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field6", _ent.Field6);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field7", _ent.Field7);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field8", _ent.Field8);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "set", "Field9", _ent.Field9); 

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePurchaseRequestDetail(PurchaseRequestService PurchaseRequestDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.PurchaseRequestService", "cond", "DocNumber", PurchaseRequestDetail.DocNumber);
                DT1.Rows.Add("Procurement.PurchaseRequestService", "cond", "LineNumber", PurchaseRequestDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Procurement.PurchaseRequestService where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.PurchaseRequest", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.PurchaseRequest", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        public class RefTransaction
        {
            public virtual PurchaseRequest Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='PRDPRQT' OR  A.TransType='PRDPRQT') ", Conn);
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
            a = Gears.RetriveData2("select * from Procurement.PurchaseRequest where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                TargetDate = dtRow["TargetDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                StockNumber = dtRow["StockNumber"].ToString();
                Status = dtRow["Status"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                CostCenter = dtRow["CostCenter"].ToString();
                PODocNumber = dtRow["PODocNumber"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString(); 
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                ForceClosedBy = dtRow["ForceClosedBy"].ToString();
                ForceClosedDate = dtRow["ForceClosedDate"].ToString();
                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);
                //IsSample = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSample"]) ? false : dtRow["IsSample"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);

                //2021-06-21    EMC add code for Toll Module
                Year = Convert.ToInt32(Convert.IsDBNull(dtRow["Year"]) ? "0" : dtRow["Year"].ToString());
                WorkWeek = Convert.ToInt32(Convert.IsDBNull(dtRow["WorkWeek"]) ? "0" : dtRow["WorkWeek"].ToString());
                DayNo = Convert.ToInt32(Convert.IsDBNull(dtRow["DayNo"]) ? "0" : dtRow["DayNo"].ToString());
                RequestType = dtRow["RequestType"].ToString();
                RequestingDeptCompany = dtRow["RequestingDeptCompany"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                IsAllowPartialHeader = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsAllowPartial"]) ? false : dtRow["IsAllowPartial"]);
                Consignee = dtRow["Consignee"].ToString();
                ConsigneeAddress = dtRow["ConsigneeAddress"].ToString();
                RequiredLoadingTime = dtRow["RequiredLoadingTime"].ToString();
                ShipmentType = dtRow["ShipmentType"].ToString();
                OCNRequestNumber = dtRow["OCNRequestNumber"].ToString();

                ScrapRef = dtRow["ScrapRef"].ToString();

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
        public string InsertData(PurchaseRequest _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "TargetDate", _ent.TargetDate);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "StockNumber", _ent.StockNumber);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Status", _ent.Status);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "CostCenter", _ent.CostCenter);
            
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "PODocNumber", _ent.PODocNumber);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "SubmittedBy", _ent.SubmittedBy);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "SubmittedDate", _ent.SubmittedDate);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "LastEditedBy", _ent.LastEditedBy);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "LastEditedDate", _ent.LastEditedDate);


            //2021-06-22    EMC add code for Toll Module
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Year", _ent.Year);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "RequestType", _ent.RequestType);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "RequestingDeptCompany", _ent.RequestingDeptCompany);

            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "IsAllowPartial", _ent.IsAllowPartialHeader);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Consignee", _ent.Consignee);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "ConsigneeAddress", _ent.ConsigneeAddress);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "OCNRequestNumber", _ent.OCNRequestNumber);

            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "ScrapRef", _ent.ScrapRef);


            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "IsPrinted", "False");
            
            
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "IsWithDetail", "False");
            

            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseRequest", "0", "Field9", _ent.Field9);
            string strErr = Gears.CreateData(DT1, _ent.Connection);
            return strErr;
        }

        public string UpdateData(PurchaseRequest _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.PurchaseRequest", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "TargetDate", _ent.TargetDate);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "StockNumber", _ent.StockNumber);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Status", "N");
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "CostCenter", _ent.CostCenter);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "PODocNumber", _ent.PODocNumber);

            //2021-06-21    EMC add code for Toll Module
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Year", _ent.Year);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "RequestType", _ent.RequestType);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "RequestingDeptCompany", _ent.RequestingDeptCompany);

            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "IsAllowPartial", _ent.IsAllowPartialHeader);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Consignee", _ent.Consignee);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "ConsigneeAddress", _ent.ConsigneeAddress);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "OCNRequestNumber", _ent.OCNRequestNumber);


            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "ScrapRef", _ent.ScrapRef);


            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "ApprovedBy", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "ApprovedDate", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "AddedBy", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "AddedDate", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "LastEditedBy", _ent.CustomerCode);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "LastEditedDate", _ent.CustomerCode);

            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "IsPrinted", _ent.IsPrinted);

            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "IsSample", _ent.IsSample);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "set", "IsValidated", _ent.IsValidated);
           // DT1.Rows.Add("Procurement.PurchaseRequest", "set", "IsWithDetail", _ent.IsWithDetail);

            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.PurchaseRequest", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDPRQT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
            return strErr;
            
        }
        public void DeleteData(PurchaseRequest _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.PurchaseRequest", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Procurement.PurchaseRequestDetail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT2, _ent.Connection);


            Functions.AuditTrail("PRDPRQT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", Conn);
        }
    }
}
