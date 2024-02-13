using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ProdWIPOUT
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
     
        public virtual string Type { get; set; }
        public virtual string AdjustmentClass { get; set; }
        public virtual string JobOrder { get; set; }
        public virtual string Step { get; set; }
   
        public virtual string WorkCenter { get; set; }

        public virtual string txtPD { get; set; }
        public virtual string Shift { get; set; }
        public virtual string txtProductName { get; set; }
        public virtual string txtMonitoredBy { get; set; }
        public virtual string MemoRemarks { get; set; }

        public virtual string ReceivedWorkCenter { get; set; }

        public virtual string DRDocnumber { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string OverheadCode { get; set; }
        public virtual decimal TotalQuantity { get; set; }
        public virtual decimal WorkOrderPrice { get; set; }
        public virtual decimal OriginalWorkOrderPrice { get; set; }
        public virtual string VatCode { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual decimal GrossVatableAmount { get; set; }
        public virtual decimal NonVatableAmount { get; set; }
        public virtual decimal VatAmount { get; set; }
        public virtual decimal VatRate { get; set; }
        public virtual decimal AtcRate { get; set; }
        public virtual decimal WTaxAmount { get; set; }
        public virtual bool ClassA { get; set; }
        public virtual bool AutoCharge { get; set; }
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

        //2021-06-03    emc add field for Toll Project
        public virtual int DayNo { get; set; }
        public virtual int Year { get; set; }
        public virtual int WorkWeek { get; set; }
        public virtual string SKUcode { get; set; }
        public virtual string BatchNo { get; set; }
        public virtual string Machine { get; set; }
        public virtual decimal PlanQty { get; set; }
        public virtual decimal ActualQty { get; set; }
        public virtual bool IsBackFlash { get; set; }

        //2021-07-20    emc add field for Toll Project
        public virtual string ProdSite { get; set; }
        public virtual string StepSeq { get; set; }
        public virtual string ScrapCode { get; set; }
        public virtual string ScrapDate { get; set; }
        public virtual decimal ScrapWeight { get; set; }
        public virtual string ScrapUOM { get; set; }
        public virtual string glSmokehouse { get; set; }

        public virtual decimal BatchNum { get; set; }
        public virtual string TimeStandard { get; set; }

        public virtual string TimeStart { get; set; }
      

        public virtual string TimeEnd { get; set; }
        
        public virtual string txtStoveTemp { get; set; }
        public virtual string txtStoveTempAct { get; set; }
        public virtual string txtHumiditySTD { get; set; }
        public virtual string txtHumidityAct { get; set; }
        public virtual string txtSteam { get; set; }
        public virtual string txtInternal { get; set; }
        public virtual string TxtWeighingAC { get; set; }
        public virtual string txtValidated { get; set; }
        public virtual IList<ProdWIPOUTDetail> Detail1 { get; set; }
        public virtual IList<ProdWIPOUTDetailCooking> Detail2 { get; set; }


        public class ProdWIPOUTDetail
        {

            public virtual ProdWIPOUT Parent { get; set; }
            public virtual string DocNumber { get; set; }

            public virtual string LineNumber { get; set; }

            public virtual string SmokeHouseNo { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual decimal ITAcooking { get; set; }
          
    


            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.WIPOutDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddProdWIPOUTDetail(ProdWIPOUTDetail ProdWIPOUTDetail)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.WIPOutDetail where docnumber = '" + Docnum + "'", Conn);

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




                DT1.Rows.Add("Production.WIPOutDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WIPOutDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.WIPOutDetail", "0", "SmokeHouseNo", ProdWIPOUTDetail.SmokeHouseNo);
                DT1.Rows.Add("Production.WIPOutDetail", "0", "BatchNo", ProdWIPOUTDetail.BatchNo);
                DT1.Rows.Add("Production.WIPOutDetail", "0", "ITAcooking", ProdWIPOUTDetail.ITAcooking);
 

               




                //DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateProdWIPOUTDetail(ProdWIPOUTDetail ProdWIPOUTDetail)
            {
                int linenum = 0;
                

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



                DT1.Rows.Add("Production.WIPOUTDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WIPOUTDetail", "cond", "LineNumber", ProdWIPOUTDetail.LineNumber);
                DT1.Rows.Add("Production.WIPOUTDetail", "set", "SmokeHouseNo", ProdWIPOUTDetail.SmokeHouseNo);
                DT1.Rows.Add("Production.WIPOUTDetail", "set", "BatchNo", ProdWIPOUTDetail.BatchNo);
                DT1.Rows.Add("Production.WIPOUTDetail", "set", "ITAcooking", ProdWIPOUTDetail.ITAcooking);
                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteProdWIPOUTDetail(ProdWIPOUTDetail ProdWIPOUTDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.WIPOUTDetail", "cond", "DocNumber", ProdWIPOUTDetail.DocNumber);
                DT1.Rows.Add("Production.WIPOUTDetail", "cond", "LineNumber", ProdWIPOUTDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Production.WIPOUTDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.ProdWIPOUT", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.ProdWIPOUT", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }





        }

        public class ProdWIPOUTDetailCooking
        {

            public virtual ProdWIPOUT Parent { get; set; }
            public virtual string DocNumber { get; set; }

            public virtual string LineNumber { get; set; }

            public virtual string CookingStage { get; set; }
            public virtual decimal STDcooking { get; set; }
            public virtual decimal TimeStart { get; set; }
            public virtual decimal TimeEnd { get; set; }
            public virtual decimal StdST { get; set; }
            public virtual decimal StdH { get; set; }
            public virtual decimal ActualST { get; set; }
            public virtual decimal ActualH { get; set; }




            public DataTable getdetailCooking(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.WIPOUTDetailCooking where DocNumber='" + DocNumber + "' order by LineNumber", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddProdWIPOUTDetailCooking(ProdWIPOUTDetailCooking ProdWIPOUTDetailCooking)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.WIPOUTDetailCooking where docnumber = '" + Docnum + "'", Conn);

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




                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "LineNumber", strLine);

                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "CookingStage", ProdWIPOUTDetailCooking.CookingStage);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "STDcooking", ProdWIPOUTDetailCooking.STDcooking);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "TimeStart", ProdWIPOUTDetailCooking.TimeStart);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "TimeEnd", ProdWIPOUTDetailCooking.TimeEnd);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "StdST", ProdWIPOUTDetailCooking.StdST);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "ActualST", ProdWIPOUTDetailCooking.ActualST);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "StdH", ProdWIPOUTDetailCooking.StdH);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "0", "ActualH", ProdWIPOUTDetailCooking.ActualH);







                //DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateProdWIPOUTDetailCooking(ProdWIPOUTDetailCooking ProdWIPOUTDetailCooking)
            {
                int linenum = 0;


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



                DT1.Rows.Add("Production.WIPOUTDetailCooking", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "cond", "LineNumber", ProdWIPOUTDetailCooking.LineNumber);
                                      
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "CookingStage", ProdWIPOUTDetailCooking.CookingStage);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "STDcooking", ProdWIPOUTDetailCooking.STDcooking);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "TimeStart", ProdWIPOUTDetailCooking.TimeStart);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "TimeEnd", ProdWIPOUTDetailCooking.TimeEnd);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "StdST", ProdWIPOUTDetailCooking.StdST);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "ActualST", ProdWIPOUTDetailCooking.ActualST);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "StdH", ProdWIPOUTDetailCooking.StdH);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "set", "ActualH", ProdWIPOUTDetailCooking.ActualH);
                Gears.UpdateData(DT1, Conn);



            }
            public void DeleteProdWIPOUTDetailCooking(ProdWIPOUTDetailCooking ProdWIPOUTDetailCooking)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "cond", "DocNumber", ProdWIPOUTDetailCooking.DocNumber);
                DT1.Rows.Add("Production.WIPOUTDetailCooking", "cond", "LineNumber", ProdWIPOUTDetailCooking.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Production.ProdWIPOUTDetailCooking where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.ProdWIPOUT", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.ProdWIPOUT", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }





        }
        public virtual IList<WOClassBreakDown> Detail { get; set; }


        public class WOClassBreakDown
        {

            public virtual ProdWIPOUT Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string ClassCode { get; set; }
            public virtual string SizeCodes { get; set; }
            public virtual decimal Quantity { get; set; }


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
                    a = Gears.RetriveData2("select * from Procurement.WOClassBreakDown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddWOClassBreakDown(WOClassBreakDown WOClassBreakDown)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.WOClassBreakDown where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.WOClassBreakDown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "ClassCode", WOClassBreakDown.ClassCode);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "SizeCodes", WOClassBreakDown.SizeCodes);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Quantity", WOClassBreakDown.Quantity);


                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field1", WOClassBreakDown.Field1);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field2", WOClassBreakDown.Field2);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field3", WOClassBreakDown.Field3);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field4", WOClassBreakDown.Field4);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field5", WOClassBreakDown.Field5);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field6", WOClassBreakDown.Field6);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field7", WOClassBreakDown.Field7);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field8", WOClassBreakDown.Field8);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "0", "Field9", WOClassBreakDown.Field9);



                //DT2.Rows.Add("Procurement.WIPOUT", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Procurement.WIPOUT", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateWOClassBreakDown(WOClassBreakDown WOClassBreakDown)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.WOClassBreakDown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "cond", "LineNumber", WOClassBreakDown.LineNumber);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "ClassCode", WOClassBreakDown.ClassCode);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "SizeCodes", WOClassBreakDown.SizeCodes);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Quantity", WOClassBreakDown.Quantity);



                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field1", WOClassBreakDown.Field1);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field2", WOClassBreakDown.Field2);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field3", WOClassBreakDown.Field3);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field4", WOClassBreakDown.Field4);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field5", WOClassBreakDown.Field5);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field6", WOClassBreakDown.Field6);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field7", WOClassBreakDown.Field7);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field8", WOClassBreakDown.Field8);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "set", "Field9", WOClassBreakDown.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteWOClassBreakDown(WOClassBreakDown WOClassBreakDown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.WOClassBreakDown", "cond", "DocNumber", WOClassBreakDown.DocNumber);
                DT1.Rows.Add("Procurement.WOClassBreakDown", "cond", "LineNumber", WOClassBreakDown.LineNumber);


                Gears.DeleteData(DT1, Conn);


                //DataTable count = Gears.RetriveData2("select * from Procurement.WOClassBreakDown where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Procurement.WIPOUT", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Procurement.WIPOUT", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }


            public class RefTransaction
            {
                public virtual ProdWIPOUT Parent { get; set; }
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
                                                + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDWOT' OR  A.TransType='PRDWOT') ", Conn);
                        return a;
                    }
                    catch (Exception e)
                    {
                        a = null;
                        return a;
                    }
                }
            }


        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Production.WIPOUT where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                Type = dtRow["Type"].ToString();
                JobOrder = dtRow["JobOrder"].ToString();
                Step = dtRow["Step"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                ReceivedWorkCenter = dtRow["ReceivedWorkCenter"].ToString();
                AdjustmentClass = dtRow["AdjustmentClass"].ToString();
                TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);
                txtPD = dtRow["txtPD"].ToString();
                Shift = dtRow["Shift"].ToString();
                txtProductName = dtRow["txtProductName"].ToString();
                txtMonitoredBy = dtRow["txtMonitoredBy"].ToString();
                MemoRemarks = dtRow["MemoRemarks"].ToString();

                DRDocnumber = dtRow["DRDocnumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                OverheadCode = dtRow["OverheadCode"].ToString();
                WorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["WorkOrderPrice"]) ? 0 : dtRow["WorkOrderPrice"]);
                OriginalWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["OriginalWorkOrderPrice"]) ? 0 : dtRow["OriginalWorkOrderPrice"]);
                VatCode = dtRow["VatCode"].ToString();
                VatRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatRate"]) ? 0 : dtRow["VatRate"]);
                AtcRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["AtcRate"]) ? 0 : dtRow["AtcRate"]);
                ClassA = Convert.ToBoolean(Convert.IsDBNull(dtRow["ClassA"]) ? false : dtRow["ClassA"]);
                AutoCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["AutoCharge"]) ? false : dtRow["AutoCharge"]);
                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatableAmount"]) ? 0 : dtRow["GrossVatableAmount"]);
                NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatableAmount"]) ? 0 : dtRow["NonVatableAmount"]);
                VatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatAmount"]) ? 0 : dtRow["VatAmount"]);
                WTaxAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["WTaxAmount"]) ? 0 : dtRow["WTaxAmount"]);
                Currency = dtRow["Currency"].ToString();
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

                //2021-06-03   emc ADD code
                DayNo = Convert.ToInt32(Convert.IsDBNull(dtRow["DayNo"]) ? 0 : dtRow["DayNo"]);
                Year = Convert.ToInt32(Convert.IsDBNull(dtRow["Year"]) ? 0 : dtRow["Year"]);
                WorkWeek = Convert.ToInt32(Convert.IsDBNull(dtRow["WorkWeek"]) ? 0 : dtRow["WorkWeek"]);
                SKUcode = dtRow["SKUcode"].ToString();
                BatchNo = dtRow["BatchNo"].ToString();
                Machine = dtRow["Machine"].ToString();
                PlanQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["PlanQty"]) ? 0 : dtRow["PlanQty"]);
                ActualQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ActualQty"]) ? 0 : dtRow["ActualQty"]);
                IsBackFlash = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsBackFlash"]) ? false : dtRow["IsBackFlash"]);

                ProdSite = dtRow["ProdSite"].ToString();
                StepSeq = dtRow["StepSeq"].ToString();
                ScrapCode = dtRow["ScrapCode"].ToString();
                ScrapDate = dtRow["ScrapDate"].ToString();
                ScrapWeight = Convert.ToDecimal(Convert.IsDBNull(dtRow["ScrapWeight"]) ? 0 : dtRow["ScrapWeight"]);
                ScrapUOM = dtRow["ScrapUOM"].ToString();



                 glSmokehouse = dtRow["glSmokehouse"].ToString();
                 BatchNum = Convert.ToDecimal(Convert.IsDBNull(dtRow["BatchNum"]) ? 0 : dtRow["BatchNum"]);
                 TimeStandard = dtRow["TimeStandard"].ToString();
                 TimeStart = dtRow["TimeStart"].ToString();
                 TimeEnd = dtRow["TimeEnd"].ToString();
                 txtStoveTemp = dtRow["txtStoveTemp"].ToString();
                 txtStoveTempAct = dtRow["txtStoveTempAct"].ToString();
                 txtHumiditySTD = dtRow["txtHumiditySTD"].ToString();
                 txtHumidityAct = dtRow["txtHumidityAct"].ToString();
                 txtSteam = dtRow["txtSteam"].ToString();
                 txtInternal = dtRow["txtInternal"].ToString();
                 TxtWeighingAC = dtRow["TxtWeighingAC"].ToString();
                 txtValidated = dtRow["txtValidated"].ToString();






            }
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as CustomerCode,'' as DeliveryDate ,'' as PicklistType ,'' as Type " +
            //         ",'' as StorerKey ,'' as WarehouseCode ,'' as PlantCode ,'' as ReferenceNo ,'' as Remarks ,'' as OutboundNo  ,0 as IsAutoPick  ,'' as Field1" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(ProdWIPOUT _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

    
            DT1.Rows.Add("Production.WIPOUT", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.WIPOUT", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.WIPOUT", "0", "Type", _ent.Type);
            DT1.Rows.Add("Production.WIPOUT", "0", "AdjustmentClass", _ent.AdjustmentClass);
            DT1.Rows.Add("Production.WIPOUT", "0", "JobOrder", _ent.JobOrder);
            DT1.Rows.Add("Production.WIPOUT", "0", "Step", _ent.Step);
            DT1.Rows.Add("Production.WIPOUT", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.WIPOUT", "0", "ReceivedWorkCenter", _ent.ReceivedWorkCenter);

            DT1.Rows.Add("Production.WIPOUT", "0", "TotalQuantity", _ent.TotalQuantity);
            DT1.Rows.Add("Production.WIPOUT", "0", "DRDocnumber", _ent.DRDocnumber);
            DT1.Rows.Add("Production.WIPOUT", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.WIPOUT", "0", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.WIPOUT", "0", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.WIPOUT", "0", "VatCode ", _ent.VatCode);
            DT1.Rows.Add("Production.WIPOUT", "0", "Currency ", _ent.Currency);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtPD ", _ent.txtPD);
            DT1.Rows.Add("Production.WIPOUT", "0", "Shift ", _ent.Shift);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtProductName ", _ent.txtProductName);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtMonitoredBy ", _ent.txtMonitoredBy);
            DT1.Rows.Add("Production.WIPOUT", "0", "MemoRemarks ", _ent.MemoRemarks);
            
            DT1.Rows.Add("Production.WIPOUT", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.WIPOUT", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.WIPOUT", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.WIPOUT", "0", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.WIPOUT", "0", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.WIPOUT", "0", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.WIPOUT", "0", "WTaxAmount", _ent.WTaxAmount);
            DT1.Rows.Add("Production.WIPOUT", "0", "VatRate", _ent.VatRate);
            DT1.Rows.Add("Production.WIPOUT", "0", "AtcRate", _ent.AtcRate);
            DT1.Rows.Add("Production.WIPOUT", "0", "ClassA", _ent.ClassA);
            DT1.Rows.Add("Production.WIPOUT", "0", "AutoCharge", _ent.AutoCharge);



            DT1.Rows.Add("Production.WIPOUT", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.WIPOUT", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.WIPOUT", "0", "SubmittedBy", "");

            //2021-06-03   emc ADD code
            DT1.Rows.Add("Production.WIPOUT", "0", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Production.WIPOUT", "0", "Year", _ent.Year);
            DT1.Rows.Add("Production.WIPOUT", "0", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Production.WIPOUT", "0", "SKUcode", _ent.SKUcode);
            DT1.Rows.Add("Production.WIPOUT", "0", "BatchNo", _ent.BatchNo);
            DT1.Rows.Add("Production.WIPOUT", "0", "Machine", _ent.Machine);
            DT1.Rows.Add("Production.WIPOUT", "0", "PlanQty", _ent.PlanQty);
            DT1.Rows.Add("Production.WIPOUT", "0", "ActualQty", _ent.ActualQty);
            DT1.Rows.Add("Production.WIPOUT", "0", "IsBackFlash", _ent.IsBackFlash);


            DT1.Rows.Add("Production.WIPOUT", "0", "ProdSite", _ent.ProdSite);
            DT1.Rows.Add("Production.WIPOUT", "0", "StepSeq", _ent.StepSeq);
            DT1.Rows.Add("Production.WIPOUT", "0", "ScrapCode", _ent.ScrapCode);
            DT1.Rows.Add("Production.WIPOUT", "0", "ScrapDate", _ent.ScrapDate);
            DT1.Rows.Add("Production.WIPOUT", "0", "ScrapWeight", _ent.ScrapWeight);
            DT1.Rows.Add("Production.WIPOUT", "0", "ScrapUOM", _ent.ScrapUOM);

            DT1.Rows.Add("Production.WIPOUT", "0", "glSmokehouse", _ent.glSmokehouse);
            DT1.Rows.Add("Production.WIPOUT", "0", "BatchNum", _ent.BatchNum);

            DT1.Rows.Add("Production.WIPOUT", "0", "TimeStandard", _ent.TimeStandard);
            DT1.Rows.Add("Production.WIPOUT", "0", "TimeStart", _ent.TimeStart);
            DT1.Rows.Add("Production.WIPOUT", "0", "TimeEnd", _ent.TimeEnd);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtStoveTemp", _ent.txtStoveTemp);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtStoveTempAct", _ent.txtStoveTempAct);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtHumiditySTD", _ent.txtHumiditySTD);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtHumidityAct", _ent.txtHumidityAct);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtSteam", _ent.txtSteam);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtInternal", _ent.txtInternal);
            DT1.Rows.Add("Production.WIPOUT", "0", "TxtWeighingAC", _ent.TxtWeighingAC);
            DT1.Rows.Add("Production.WIPOUT", "0", "txtValidated", _ent.txtValidated);
            



            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(ProdWIPOUT _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.WIPOUT", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.WIPOUT", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.WIPOUT", "set", "Type  ", _ent.Type);
            DT1.Rows.Add("Production.WIPOUT", "set", "AdjustmentClass", _ent.AdjustmentClass);
            DT1.Rows.Add("Production.WIPOUT", "set", "JobOrder  ", _ent.JobOrder);
            DT1.Rows.Add("Production.WIPOUT", "set", "Step  ", _ent.Step);
            DT1.Rows.Add("Production.WIPOUT", "set", "WorkCenter  ", _ent.WorkCenter);
            DT1.Rows.Add("Production.WIPOUT", "set", "ReceivedWorkCenter  ", _ent.ReceivedWorkCenter);
            DT1.Rows.Add("Production.WIPOUT", "set", "AutoCharge  ", _ent.AutoCharge);
            DT1.Rows.Add("Production.WIPOUT", "set", "ClassA  ", _ent.ClassA);
            DT1.Rows.Add("Production.WIPOUT", "set", "OverheadCode", _ent.OverheadCode);
            DT1.Rows.Add("Production.WIPOUT", "set", "TotalQuantity  ", _ent.TotalQuantity);
            DT1.Rows.Add("Production.WIPOUT", "set", "DRDocnumber ", _ent.DRDocnumber);
            DT1.Rows.Add("Production.WIPOUT", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.WIPOUT", "set", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.WIPOUT", "set", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.WIPOUT", "set", "VatCode ", _ent.VatCode);

            DT1.Rows.Add("Production.WIPOUT", "set", "txtPD ", _ent.txtPD);
            DT1.Rows.Add("Production.WIPOUT", "set", "Shift ", _ent.Shift);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtProductName ", _ent.txtProductName);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtMonitoredBy ", _ent.txtMonitoredBy);
            DT1.Rows.Add("Production.WIPOUT", "set", "MemoRemarks ", _ent.MemoRemarks);
            
            DT1.Rows.Add("Production.WIPOUT", "set", "Currency ", _ent.Currency);
            DT1.Rows.Add("Production.WIPOUT", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.WIPOUT", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.WIPOUT", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.WIPOUT", "set", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.WIPOUT", "set", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.WIPOUT", "set", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.WIPOUT", "set", "WTaxAmount", _ent.WTaxAmount);
            DT1.Rows.Add("Production.WIPOUT", "set", "VatRate", _ent.VatRate);
            DT1.Rows.Add("Production.WIPOUT", "set", "AtcRate", _ent.AtcRate);
            
            DT1.Rows.Add("Production.WIPOUT", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.WIPOUT", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.WIPOUT", "set", "SubmittedBy", "");
            DT1.Rows.Add("Production.WIPOUT", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.WIPOUT", "set", "LastEditedDate", _ent.LastEditedDate);

            //2021-06-03   emc ADD code
            DT1.Rows.Add("Production.WIPOUT", "set", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Production.WIPOUT", "set", "Year", _ent.Year);
            DT1.Rows.Add("Production.WIPOUT", "set", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Production.WIPOUT", "set", "SKUcode", _ent.SKUcode);
            DT1.Rows.Add("Production.WIPOUT", "set", "BatchNo", _ent.BatchNo);
            DT1.Rows.Add("Production.WIPOUT", "set", "Machine", _ent.Machine);
            DT1.Rows.Add("Production.WIPOUT", "set", "PlanQty", _ent.PlanQty);
            DT1.Rows.Add("Production.WIPOUT", "set", "ActualQty", _ent.ActualQty);
            DT1.Rows.Add("Production.WIPOUT", "set", "IsBackFlash", _ent.IsBackFlash);

            DT1.Rows.Add("Production.WIPOUT", "set", "ProdSite", _ent.ProdSite);
            DT1.Rows.Add("Production.WIPOUT", "set", "StepSeq", _ent.StepSeq);
            DT1.Rows.Add("Production.WIPOUT", "set", "ScrapCode", _ent.ScrapCode);
            DT1.Rows.Add("Production.WIPOUT", "set", "ScrapDate", _ent.ScrapDate);
            DT1.Rows.Add("Production.WIPOUT", "set", "ScrapWeight", _ent.ScrapWeight);
            DT1.Rows.Add("Production.WIPOUT", "set", "ScrapUOM", _ent.ScrapUOM);

            DT1.Rows.Add("Production.WIPOUT", "set", "glSmokehouse", _ent.glSmokehouse);
            DT1.Rows.Add("Production.WIPOUT", "set", "BatchNum", _ent.BatchNum);
            DT1.Rows.Add("Production.WIPOUT", "set", "TimeStandard", _ent.TimeStandard);
            DT1.Rows.Add("Production.WIPOUT", "set", "TimeStart", _ent.TimeStart);
            DT1.Rows.Add("Production.WIPOUT", "set", "TimeEnd", _ent.TimeEnd);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtStoveTemp", _ent.txtStoveTemp);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtStoveTempAct", _ent.txtStoveTempAct);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtSteam", _ent.txtSteam);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtHumiditySTD", _ent.txtHumiditySTD);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtHumidityAct", _ent.txtHumidityAct);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtInternal", _ent.txtInternal);
            DT1.Rows.Add("Production.WIPOUT", "set", "TxtWeighingAC", _ent.TxtWeighingAC);
            DT1.Rows.Add("Production.WIPOUT", "set", "txtValidated", _ent.txtValidated);















            Gears.UpdateData(DT1, _ent.Connection);


            Functions.AuditTrail("PRDWOUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(ProdWIPOUT _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.WIPOUT", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDWOUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }


        #region Journal Entry
        public class JournalEntry
        {
            public virtual ProdWIPOUT Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit, A.BizPartnerCode FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='PRDWOT' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        #endregion

        #region Reference Transactions
        public class RefTransaction
        {
            public virtual ProdWIPOUT Parent { get; set; }
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
                                            + " inner join IT.MainMenu B on A.RMenuID = B.ModuleID inner join IT.MainMenu C on A.MenuID = C.ModuleID "
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDWOT' OR  A.TransType='PRDWOT') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        #endregion


    }
}
