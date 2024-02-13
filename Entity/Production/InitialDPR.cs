using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class InitialDPR
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
        public virtual IList<InitialDPRDetail> Detail1 { get; set; }
        public virtual IList<InitialDPRDetail2> Detail2 { get; set; }


        public class InitialDPRDetail
        {

            public virtual InitialDPR Parent { get; set; }
            public virtual string DocNumber { get; set; }

            public virtual string LineNumber { get; set; }

            public virtual string SmokeHouseNo { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual decimal ITAcooking { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemC { get; set; }
            public virtual string itemDescription { get; set; }
            public virtual decimal ProductionPlanDay  { get; set; }
            public virtual decimal ProductionPlanNight { get; set; }
            public virtual decimal ProductionPlanTotal { get; set; }
            public virtual decimal ActualProducedDay { get; set; }
            public virtual decimal ActualProducedNight { get; set; }
            public virtual decimal ActualProducedTotal { get; set; }
            public virtual decimal PLTotal { get; set; }
            public virtual decimal TipinDay { get; set; }
            public virtual decimal TipinNight { get; set; }
            public virtual decimal TipinTotal { get; set; }
            public virtual decimal stdBatchWeight { get; set; }
            public virtual decimal InputDay { get; set; }
            public virtual decimal InputNight { get; set; }
            public virtual decimal InputTotal { get; set; }
            public virtual decimal BC { get; set; }
            public virtual decimal PROyield { get; set; }
            public virtual decimal AC { get; set; }
            public virtual decimal CookingLoss { get; set; }
            public virtual decimal SMYield { get; set; }
            public virtual decimal PackagingInitialOutDay { get; set; }
            public virtual decimal PackagingInitialOutNight { get; set; }
            public virtual decimal PackagingInitialOutTotal { get; set; }
            public virtual decimal PackagingInitialOutTotalPkcs { get; set; }
            public virtual decimal RejectionSummarypercent { get; set; }
            public virtual decimal RejectionSummarypercentTotal { get; set; }
            public virtual decimal SMKHULUT { get; set; }
            public virtual decimal BRINEULUT { get; set; }
            public virtual decimal CUTTINGULUT { get; set; }
            public virtual decimal PackagingULUT { get; set; }
            public virtual decimal PackagingOSDF { get; set; }
            public virtual decimal PackagingMISCUT { get; set; }
            public virtual decimal FGGiveawaySTD { get; set; }
            public virtual decimal FGGiveawayActual { get; set; }
            public virtual decimal FGGiveawayPercent { get; set; }
            public virtual decimal TotalYield { get; set; }
            public virtual decimal StandaredYield { get; set; }
            public virtual decimal StandardWTPStrand { get; set; }
            public virtual decimal StandardCaseCon { get; set; }
            public virtual decimal AVGKiloPPiece { get; set; }
            public virtual decimal InitialYield { get; set; }

            public virtual decimal Spillage1 { get; set; }
            public virtual decimal Spillage2 { get; set; }
            public virtual decimal Spillage3 { get; set; }
            public virtual decimal Spillage4 { get; set; }
            public virtual decimal Spillage5 { get; set; }
            public virtual decimal Spillage6 { get; set; }

            public virtual decimal SectionSAMPLE { get; set; }
            public virtual decimal SpillageTotal { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select B.SAPDescription AS ItemDescription,* from Production.InitialDPRDetail A left join masterfile.fgsku B on A.ItemC = B.SKUCode  where DocNumber='" + DocNumber + "' order by LineNumber", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddInitialDPRDetail(InitialDPRDetail InitialDPRDetail)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.InitialDPRDetail where docnumber = '" + Docnum + "'", Conn);

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




                DT1.Rows.Add("Production.InitialDPRDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "SmokeHouseNo", InitialDPRDetail.SmokeHouseNo);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "BatchNo", InitialDPRDetail.BatchNo);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ITAcooking", InitialDPRDetail.ITAcooking);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ItemC", InitialDPRDetail.ItemC);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ProductionPlanDay", InitialDPRDetail.ProductionPlanDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ProductionPlanNight", InitialDPRDetail.ProductionPlanNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ProductionPlanTotal", InitialDPRDetail.ProductionPlanTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ActualProducedDay", InitialDPRDetail.ActualProducedDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ActualProducedNight", InitialDPRDetail.ActualProducedNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "ActualProducedTotal", InitialDPRDetail.ActualProducedTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PLTotal", InitialDPRDetail.PLTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "TipinDay", InitialDPRDetail.TipinDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "TipinNight", InitialDPRDetail.TipinNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "TipinTotal", InitialDPRDetail.TipinTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "stdBatchWeight", InitialDPRDetail.stdBatchWeight);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "InputDay", InitialDPRDetail.InputDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "InputNight", InitialDPRDetail.InputNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "InputTotal", InitialDPRDetail.InputTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "BC", InitialDPRDetail.BC);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PROyield", InitialDPRDetail.PROyield);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "AC", InitialDPRDetail.AC);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "CookingLoss", InitialDPRDetail.CookingLoss);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "SMYield", InitialDPRDetail.SMYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PackagingInitialOutDay", InitialDPRDetail.PackagingInitialOutDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PackagingInitialOutNight", InitialDPRDetail.PackagingInitialOutNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PackagingInitialOutTotal", InitialDPRDetail.PackagingInitialOutTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PackagingInitialOutTotalPkcs", InitialDPRDetail.PackagingInitialOutTotalPkcs);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "RejectionSummarypercent", InitialDPRDetail.RejectionSummarypercent);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "RejectionSummarypercentTotal", InitialDPRDetail.RejectionSummarypercentTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "SMKHULUT", InitialDPRDetail.SMKHULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "BRINEULUT", InitialDPRDetail.BRINEULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "CUTTINGULUT", InitialDPRDetail.CUTTINGULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PackagingULUT", InitialDPRDetail.PackagingULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PackagingOSDF", InitialDPRDetail.PackagingOSDF);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "PackagingMISCUT", InitialDPRDetail.PackagingMISCUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "FGGiveawaySTD", InitialDPRDetail.FGGiveawaySTD);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "FGGiveawayActual", InitialDPRDetail.FGGiveawayActual);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "FGGiveawayPercent", InitialDPRDetail.FGGiveawayPercent);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "TotalYield", InitialDPRDetail.TotalYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "StandaredYield", InitialDPRDetail.StandaredYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "StandardWTPStrand", InitialDPRDetail.StandardWTPStrand);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "StandardCaseCon", InitialDPRDetail.StandardCaseCon);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "InitialYield", InitialDPRDetail.InitialYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "Spillage1", InitialDPRDetail.Spillage1);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "Spillage2", InitialDPRDetail.Spillage2);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "Spillage3", InitialDPRDetail.Spillage3);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "Spillage4", InitialDPRDetail.Spillage4);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "Spillage5", InitialDPRDetail.Spillage5);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "Spillage6", InitialDPRDetail.Spillage6);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "AVGKiloPPiece", InitialDPRDetail.AVGKiloPPiece);
                DT1.Rows.Add("Production.InitialDPRDetail", "0", "SectionSAMPLE", InitialDPRDetail.SectionSAMPLE);

                //DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateInitialDPRDetail(InitialDPRDetail InitialDPRDetail)
            {
                int linenum = 0;
                

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



                DT1.Rows.Add("Production.InitialDPRDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.InitialDPRDetail", "cond", "LineNumber", InitialDPRDetail.LineNumber);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "SmokeHouseNo", InitialDPRDetail.SmokeHouseNo);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "BatchNo", InitialDPRDetail.BatchNo);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ITAcooking", InitialDPRDetail.ITAcooking);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ItemC", InitialDPRDetail.ItemC);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ProductionPlanDay", InitialDPRDetail.ProductionPlanDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ProductionPlanNight", InitialDPRDetail.ProductionPlanNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ProductionPlanTotal", InitialDPRDetail.ProductionPlanTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ActualProducedDay", InitialDPRDetail.ActualProducedDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ActualProducedNight", InitialDPRDetail.ActualProducedNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "ActualProducedTotal", InitialDPRDetail.ActualProducedTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PLTotal", InitialDPRDetail.PLTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "TipinDay", InitialDPRDetail.TipinDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "TipinNight", InitialDPRDetail.TipinNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "TipinTotal", InitialDPRDetail.TipinTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "stdBatchWeight", InitialDPRDetail.stdBatchWeight);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "InputDay", InitialDPRDetail.InputDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "InputNight", InitialDPRDetail.InputNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "InputTotal", InitialDPRDetail.InputTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "BC", InitialDPRDetail.BC);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PROyield", InitialDPRDetail.PROyield);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "AC", InitialDPRDetail.AC);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "CookingLoss", InitialDPRDetail.CookingLoss);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "SMYield", InitialDPRDetail.SMYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PackagingInitialOutDay", InitialDPRDetail.PackagingInitialOutDay);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PackagingInitialOutNight", InitialDPRDetail.PackagingInitialOutNight);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PackagingInitialOutTotal", InitialDPRDetail.PackagingInitialOutTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PackagingInitialOutTotalPkcs", InitialDPRDetail.PackagingInitialOutTotalPkcs);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "RejectionSummarypercent", InitialDPRDetail.RejectionSummarypercent);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "RejectionSummarypercentTotal", InitialDPRDetail.RejectionSummarypercentTotal);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "SMKHULUT", InitialDPRDetail.SMKHULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "BRINEULUT", InitialDPRDetail.BRINEULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "CUTTINGULUT", InitialDPRDetail.CUTTINGULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PackagingULUT", InitialDPRDetail.PackagingULUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PackagingOSDF", InitialDPRDetail.PackagingOSDF);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "PackagingMISCUT", InitialDPRDetail.PackagingMISCUT);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "FGGiveawaySTD", InitialDPRDetail.FGGiveawaySTD);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "FGGiveawayActual", InitialDPRDetail.FGGiveawayActual);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "FGGiveawayPercent", InitialDPRDetail.FGGiveawayPercent);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "TotalYield", InitialDPRDetail.TotalYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "StandaredYield", InitialDPRDetail.StandaredYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "StandardWTPStrand", InitialDPRDetail.StandardWTPStrand);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "StandardCaseCon", InitialDPRDetail.StandardCaseCon);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "AVGKiloPPiece", InitialDPRDetail.AVGKiloPPiece);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "InitialYield", InitialDPRDetail.InitialYield);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "Spillage1", InitialDPRDetail.Spillage1);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "Spillage2", InitialDPRDetail.Spillage2);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "Spillage3", InitialDPRDetail.Spillage3);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "Spillage4", InitialDPRDetail.Spillage4);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "Spillage5", InitialDPRDetail.Spillage5);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "Spillage6", InitialDPRDetail.Spillage6);
                DT1.Rows.Add("Production.InitialDPRDetail", "set", "SectionSAMPLE", InitialDPRDetail.SectionSAMPLE);
                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteInitialDPRDetail(InitialDPRDetail InitialDPRDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.InitialDPRDetail", "cond", "DocNumber", InitialDPRDetail.DocNumber);
                DT1.Rows.Add("Production.InitialDPRDetail", "cond", "LineNumber", InitialDPRDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Production.InitialDPRDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.InitialDPR", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.InitialDPR", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }





        }

        public class InitialDPRDetail2
        {

            public virtual InitialDPR Parent { get; set; }
            public virtual string DocNumber { get; set; }

            public virtual string LineNumber { get; set; }
            public virtual string ItemRemarks { get; set; }

            public virtual string ItemCode { get; set; }
            public virtual string ItemC { get; set; }
            public virtual string itemDescription { get; set; }
            public virtual decimal MEATStdUseMDM { get; set; }
            public virtual decimal MEATStdUseTHEORETICAL { get; set; }
            public virtual decimal MEATStdUseCOPACOL { get; set; }
            public virtual decimal MEATStdUseFatA { get; set; }
            public virtual decimal MEATStdUseEMERGING { get; set; }
            public virtual decimal MEATStdUseBEEF { get; set; }
            public virtual decimal MEATStdUseGroundMeat { get; set; }
            public virtual decimal Cheese { get; set; }
            public virtual decimal MEATActUseMDM { get; set; }
            public virtual decimal MEATActUseTHEORETICAL { get; set; }
            public virtual decimal MEATActUseCOPACOL { get; set; }
            public virtual decimal MEATActUseFatA { get; set; }
            public virtual decimal MEATActUseEMERGING { get; set; }
            public virtual decimal MEATActUseBEEF { get; set; }
            public virtual decimal MEATActUseGroundMeat { get; set; }
            public virtual string CasingSKU { get; set; }
            public virtual decimal CasingSTD { get; set; }
            public virtual decimal CasingACT { get; set; }
            public virtual decimal CasingREJ { get; set; }
            public virtual decimal CasingTOTAL { get; set; }
            public virtual decimal CasingDIFF { get; set; }
            public virtual string PlasticPackagingSKU { get; set; }
            public virtual decimal PlasticPackagingSTD { get; set; }
            public virtual decimal PlasticPackagingACT { get; set; }
            public virtual decimal PlasticPackagingREJ { get; set; }
            public virtual decimal PlasticPackagingTOTAL { get; set; }
            public virtual decimal PlasticPackagingDIFF { get; set; }


            public virtual string CartonSKU { get; set; }
            public virtual decimal CartonSTD { get; set; }
            public virtual decimal CartonACT { get; set; }
            public virtual decimal CartonREJ { get; set; }

            public virtual string SKUCode1 { get; set; }
            public virtual string SKUCode2 { get; set; }
            public virtual string SKUCode3 { get; set; }


            public DataTable getDetail2(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select B.SAPDescription AS ItemDescription,* from Production.InitialDPRDetail2 A left join masterfile.fgsku B on A.ItemC = B.SKUCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddInitialDPRDetail2(InitialDPRDetail2 InitialDPRDetail2)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.InitialDPRDetail2 where docnumber = '" + Docnum + "'", Conn);

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




                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "ItemRemarks", InitialDPRDetail2.ItemRemarks);


                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "ItemC", InitialDPRDetail2.ItemC);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATStdUseMDM", InitialDPRDetail2.MEATStdUseMDM);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATStdUseTHEORETICAL", InitialDPRDetail2.MEATStdUseTHEORETICAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATStdUseCOPACOL", InitialDPRDetail2.MEATStdUseCOPACOL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATStdUseFatA", InitialDPRDetail2.MEATStdUseFatA);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATStdUseEMERGING", InitialDPRDetail2.MEATStdUseEMERGING);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATStdUseBEEF", InitialDPRDetail2.MEATStdUseBEEF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATStdUseGroundMeat", InitialDPRDetail2.MEATStdUseGroundMeat);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "Cheese", InitialDPRDetail2.Cheese);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATActUseMDM", InitialDPRDetail2.MEATActUseMDM);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATActUseTHEORETICAL", InitialDPRDetail2.MEATActUseTHEORETICAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATActUseCOPACOL", InitialDPRDetail2.MEATActUseCOPACOL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATActUseFatA", InitialDPRDetail2.MEATActUseFatA);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATActUseEMERGING", InitialDPRDetail2.MEATActUseEMERGING);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATActUseBEEF", InitialDPRDetail2.MEATActUseBEEF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "MEATActUseGroundMeat", InitialDPRDetail2.MEATActUseGroundMeat);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CasingSKU", InitialDPRDetail2.CasingSKU);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CasingSTD", InitialDPRDetail2.CasingSTD);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CasingACT", InitialDPRDetail2.CasingACT);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CasingREJ", InitialDPRDetail2.CasingREJ);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CasingTOTAL", InitialDPRDetail2.CasingTOTAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CasingDIFF", InitialDPRDetail2.CasingDIFF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "PlasticPackagingSKU", InitialDPRDetail2.PlasticPackagingSKU);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "PlasticPackagingSTD", InitialDPRDetail2.PlasticPackagingSTD);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "PlasticPackagingACT", InitialDPRDetail2.PlasticPackagingACT);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "PlasticPackagingREJ", InitialDPRDetail2.PlasticPackagingREJ);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "PlasticPackagingTOTAL", InitialDPRDetail2.PlasticPackagingTOTAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "PlasticPackagingDIFF", InitialDPRDetail2.PlasticPackagingDIFF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CartonSKU", InitialDPRDetail2.CartonSKU);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CartonSTD", InitialDPRDetail2.CartonSTD);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CartonACT", InitialDPRDetail2.CartonACT);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "CartonREJ", InitialDPRDetail2.CartonREJ);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "SKUCode1", InitialDPRDetail2.SKUCode1);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "SKUCode2", InitialDPRDetail2.SKUCode2);
                DT1.Rows.Add("Production.InitialDPRDetail2", "0", "SKUCode3", InitialDPRDetail2.SKUCode3);



                //DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateInitialDPRDetail2(InitialDPRDetail2 InitialDPRDetail2)
            {
                int linenum = 0;


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



                DT1.Rows.Add("Production.InitialDPRDetail2", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.InitialDPRDetail2", "cond", "LineNumber", InitialDPRDetail2.LineNumber);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "ItemRemarks", InitialDPRDetail2.ItemRemarks);

                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "ItemC", InitialDPRDetail2.ItemC);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATStdUseMDM", InitialDPRDetail2.MEATStdUseMDM);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATStdUseTHEORETICAL", InitialDPRDetail2.MEATStdUseTHEORETICAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATStdUseCOPACOL", InitialDPRDetail2.MEATStdUseCOPACOL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATStdUseFatA", InitialDPRDetail2.MEATStdUseFatA);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATStdUseEMERGING", InitialDPRDetail2.MEATStdUseEMERGING);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATStdUseBEEF", InitialDPRDetail2.MEATStdUseBEEF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATStdUseGroundMeat", InitialDPRDetail2.MEATStdUseGroundMeat);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "Cheese", InitialDPRDetail2.Cheese);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATActUseMDM", InitialDPRDetail2.MEATActUseMDM);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATActUseTHEORETICAL", InitialDPRDetail2.MEATActUseTHEORETICAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATActUseCOPACOL", InitialDPRDetail2.MEATActUseCOPACOL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATActUseFatA", InitialDPRDetail2.MEATActUseFatA);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATActUseEMERGING", InitialDPRDetail2.MEATActUseEMERGING);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATActUseBEEF", InitialDPRDetail2.MEATActUseBEEF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "MEATActUseGroundMeat", InitialDPRDetail2.MEATActUseGroundMeat);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CasingSKU", InitialDPRDetail2.CasingSKU);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CasingSTD", InitialDPRDetail2.CasingSTD);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CasingACT", InitialDPRDetail2.CasingACT);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CasingREJ", InitialDPRDetail2.CasingREJ);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CasingTOTAL", InitialDPRDetail2.CasingTOTAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CasingDIFF", InitialDPRDetail2.CasingDIFF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "PlasticPackagingSKU", InitialDPRDetail2.PlasticPackagingSKU);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "PlasticPackagingSTD", InitialDPRDetail2.PlasticPackagingSTD);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "PlasticPackagingACT", InitialDPRDetail2.PlasticPackagingACT);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "PlasticPackagingREJ", InitialDPRDetail2.PlasticPackagingREJ);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "PlasticPackagingTOTAL", InitialDPRDetail2.PlasticPackagingTOTAL);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "PlasticPackagingDIFF", InitialDPRDetail2.PlasticPackagingDIFF);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CartonSKU", InitialDPRDetail2.CartonSKU);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CartonSTD", InitialDPRDetail2.CartonSTD);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CartonACT", InitialDPRDetail2.CartonACT);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "CartonREJ", InitialDPRDetail2.CartonREJ);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "SKUCode1", InitialDPRDetail2.SKUCode1);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "SKUCode2", InitialDPRDetail2.SKUCode2);
                DT1.Rows.Add("Production.InitialDPRDetail2", "set", "SKUCode3", InitialDPRDetail2.SKUCode3);
                Gears.UpdateData(DT1, Conn);



            }
            public void DeleteInitialDPRDetail2(InitialDPRDetail2 InitialDPRDetail2)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.InitialDPRDetail2", "cond", "DocNumber", InitialDPRDetail2.DocNumber);
                DT1.Rows.Add("Production.InitialDPRDetail2", "cond", "LineNumber", InitialDPRDetail2.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Production.InitialDPRDetail2 where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.InitialDPR", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.InitialDPR", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }





        }
        public virtual IList<WOClassBreakDown> Detail { get; set; }


        public class WOClassBreakDown
        {

            public virtual InitialDPR Parent { get; set; }
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
                public virtual InitialDPR Parent { get; set; }
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
            a = Gears.RetriveData2("select * from Production.InitialDPR where DocNumber = '" + DocNumber + "'", Conn);
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
        public void InsertData(InitialDPR _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

    
            DT1.Rows.Add("Production.InitialDPR", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.InitialDPR", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.InitialDPR", "0", "Type", _ent.Type);
            DT1.Rows.Add("Production.InitialDPR", "0", "AdjustmentClass", _ent.AdjustmentClass);
            DT1.Rows.Add("Production.InitialDPR", "0", "JobOrder", _ent.JobOrder);
            DT1.Rows.Add("Production.InitialDPR", "0", "Step", _ent.Step);
            DT1.Rows.Add("Production.InitialDPR", "0", "WorkCenter", _ent.WorkCenter);
            DT1.Rows.Add("Production.InitialDPR", "0", "ReceivedWorkCenter", _ent.ReceivedWorkCenter);

            DT1.Rows.Add("Production.InitialDPR", "0", "TotalQuantity", _ent.TotalQuantity);
            DT1.Rows.Add("Production.InitialDPR", "0", "DRDocnumber", _ent.DRDocnumber);
            DT1.Rows.Add("Production.InitialDPR", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.InitialDPR", "0", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.InitialDPR", "0", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.InitialDPR", "0", "VatCode ", _ent.VatCode);
            DT1.Rows.Add("Production.InitialDPR", "0", "Currency ", _ent.Currency);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtPD ", _ent.txtPD);
            DT1.Rows.Add("Production.InitialDPR", "0", "Shift ", _ent.Shift);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtProductName ", _ent.txtProductName);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtMonitoredBy ", _ent.txtMonitoredBy);
            DT1.Rows.Add("Production.InitialDPR", "0", "MemoRemarks ", _ent.MemoRemarks);
            
            DT1.Rows.Add("Production.InitialDPR", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.InitialDPR", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.InitialDPR", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.InitialDPR", "0", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.InitialDPR", "0", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.InitialDPR", "0", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.InitialDPR", "0", "WTaxAmount", _ent.WTaxAmount);
            DT1.Rows.Add("Production.InitialDPR", "0", "VatRate", _ent.VatRate);
            DT1.Rows.Add("Production.InitialDPR", "0", "AtcRate", _ent.AtcRate);
            DT1.Rows.Add("Production.InitialDPR", "0", "ClassA", _ent.ClassA);
            DT1.Rows.Add("Production.InitialDPR", "0", "AutoCharge", _ent.AutoCharge);



            DT1.Rows.Add("Production.InitialDPR", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.InitialDPR", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.InitialDPR", "0", "SubmittedBy", "");

            //2021-06-03   emc ADD code
            DT1.Rows.Add("Production.InitialDPR", "0", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Production.InitialDPR", "0", "Year", _ent.Year);
            DT1.Rows.Add("Production.InitialDPR", "0", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Production.InitialDPR", "0", "SKUcode", _ent.SKUcode);
            DT1.Rows.Add("Production.InitialDPR", "0", "BatchNo", _ent.BatchNo);
            DT1.Rows.Add("Production.InitialDPR", "0", "Machine", _ent.Machine);
            DT1.Rows.Add("Production.InitialDPR", "0", "PlanQty", _ent.PlanQty);
            DT1.Rows.Add("Production.InitialDPR", "0", "ActualQty", _ent.ActualQty);
            DT1.Rows.Add("Production.InitialDPR", "0", "IsBackFlash", _ent.IsBackFlash);


            DT1.Rows.Add("Production.InitialDPR", "0", "ProdSite", _ent.ProdSite);
            DT1.Rows.Add("Production.InitialDPR", "0", "StepSeq", _ent.StepSeq);
            DT1.Rows.Add("Production.InitialDPR", "0", "ScrapCode", _ent.ScrapCode);
            DT1.Rows.Add("Production.InitialDPR", "0", "ScrapDate", _ent.ScrapDate);
            DT1.Rows.Add("Production.InitialDPR", "0", "ScrapWeight", _ent.ScrapWeight);
            DT1.Rows.Add("Production.InitialDPR", "0", "ScrapUOM", _ent.ScrapUOM);

            DT1.Rows.Add("Production.InitialDPR", "0", "glSmokehouse", _ent.glSmokehouse);
            DT1.Rows.Add("Production.InitialDPR", "0", "BatchNum", _ent.BatchNum);

            DT1.Rows.Add("Production.InitialDPR", "0", "TimeStandard", _ent.TimeStandard);
            DT1.Rows.Add("Production.InitialDPR", "0", "TimeStart", _ent.TimeStart);
            DT1.Rows.Add("Production.InitialDPR", "0", "TimeEnd", _ent.TimeEnd);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtStoveTemp", _ent.txtStoveTemp);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtStoveTempAct", _ent.txtStoveTempAct);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtHumiditySTD", _ent.txtHumiditySTD);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtHumidityAct", _ent.txtHumidityAct);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtSteam", _ent.txtSteam);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtInternal", _ent.txtInternal);
            DT1.Rows.Add("Production.InitialDPR", "0", "TxtWeighingAC", _ent.TxtWeighingAC);
            DT1.Rows.Add("Production.InitialDPR", "0", "txtValidated", _ent.txtValidated);
            



            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(InitialDPR _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.InitialDPR", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.InitialDPR", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.InitialDPR", "set", "Type  ", _ent.Type);
            DT1.Rows.Add("Production.InitialDPR", "set", "AdjustmentClass", _ent.AdjustmentClass);
            DT1.Rows.Add("Production.InitialDPR", "set", "JobOrder  ", _ent.JobOrder);
            DT1.Rows.Add("Production.InitialDPR", "set", "Step  ", _ent.Step);
            DT1.Rows.Add("Production.InitialDPR", "set", "WorkCenter  ", _ent.WorkCenter);
            DT1.Rows.Add("Production.InitialDPR", "set", "ReceivedWorkCenter  ", _ent.ReceivedWorkCenter);
            DT1.Rows.Add("Production.InitialDPR", "set", "AutoCharge  ", _ent.AutoCharge);
            DT1.Rows.Add("Production.InitialDPR", "set", "ClassA  ", _ent.ClassA);
            DT1.Rows.Add("Production.InitialDPR", "set", "OverheadCode", _ent.OverheadCode);
            DT1.Rows.Add("Production.InitialDPR", "set", "TotalQuantity  ", _ent.TotalQuantity);
            DT1.Rows.Add("Production.InitialDPR", "set", "DRDocnumber ", _ent.DRDocnumber);
            DT1.Rows.Add("Production.InitialDPR", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.InitialDPR", "set", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.InitialDPR", "set", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.InitialDPR", "set", "VatCode ", _ent.VatCode);

            DT1.Rows.Add("Production.InitialDPR", "set", "txtPD ", _ent.txtPD);
            DT1.Rows.Add("Production.InitialDPR", "set", "Shift ", _ent.Shift);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtProductName ", _ent.txtProductName);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtMonitoredBy ", _ent.txtMonitoredBy);
            DT1.Rows.Add("Production.InitialDPR", "set", "MemoRemarks ", _ent.MemoRemarks);
            
            DT1.Rows.Add("Production.InitialDPR", "set", "Currency ", _ent.Currency);
            DT1.Rows.Add("Production.InitialDPR", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.InitialDPR", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.InitialDPR", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.InitialDPR", "set", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.InitialDPR", "set", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.InitialDPR", "set", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.InitialDPR", "set", "WTaxAmount", _ent.WTaxAmount);
            DT1.Rows.Add("Production.InitialDPR", "set", "VatRate", _ent.VatRate);
            DT1.Rows.Add("Production.InitialDPR", "set", "AtcRate", _ent.AtcRate);
            
            DT1.Rows.Add("Production.InitialDPR", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.InitialDPR", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.InitialDPR", "set", "SubmittedBy", "");
            DT1.Rows.Add("Production.InitialDPR", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.InitialDPR", "set", "LastEditedDate", _ent.LastEditedDate);

            //2021-06-03   emc ADD code
            DT1.Rows.Add("Production.InitialDPR", "set", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Production.InitialDPR", "set", "Year", _ent.Year);
            DT1.Rows.Add("Production.InitialDPR", "set", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Production.InitialDPR", "set", "SKUcode", _ent.SKUcode);
            DT1.Rows.Add("Production.InitialDPR", "set", "BatchNo", _ent.BatchNo);
            DT1.Rows.Add("Production.InitialDPR", "set", "Machine", _ent.Machine);
            DT1.Rows.Add("Production.InitialDPR", "set", "PlanQty", _ent.PlanQty);
            DT1.Rows.Add("Production.InitialDPR", "set", "ActualQty", _ent.ActualQty);
            DT1.Rows.Add("Production.InitialDPR", "set", "IsBackFlash", _ent.IsBackFlash);

            DT1.Rows.Add("Production.InitialDPR", "set", "ProdSite", _ent.ProdSite);
            DT1.Rows.Add("Production.InitialDPR", "set", "StepSeq", _ent.StepSeq);
            DT1.Rows.Add("Production.InitialDPR", "set", "ScrapCode", _ent.ScrapCode);
            DT1.Rows.Add("Production.InitialDPR", "set", "ScrapDate", _ent.ScrapDate);
            DT1.Rows.Add("Production.InitialDPR", "set", "ScrapWeight", _ent.ScrapWeight);
            DT1.Rows.Add("Production.InitialDPR", "set", "ScrapUOM", _ent.ScrapUOM);

            DT1.Rows.Add("Production.InitialDPR", "set", "glSmokehouse", _ent.glSmokehouse);
            DT1.Rows.Add("Production.InitialDPR", "set", "BatchNum", _ent.BatchNum);
            DT1.Rows.Add("Production.InitialDPR", "set", "TimeStandard", _ent.TimeStandard);
            DT1.Rows.Add("Production.InitialDPR", "set", "TimeStart", _ent.TimeStart);
            DT1.Rows.Add("Production.InitialDPR", "set", "TimeEnd", _ent.TimeEnd);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtStoveTemp", _ent.txtStoveTemp);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtStoveTempAct", _ent.txtStoveTempAct);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtSteam", _ent.txtSteam);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtHumiditySTD", _ent.txtHumiditySTD);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtHumidityAct", _ent.txtHumidityAct);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtInternal", _ent.txtInternal);
            DT1.Rows.Add("Production.InitialDPR", "set", "TxtWeighingAC", _ent.TxtWeighingAC);
            DT1.Rows.Add("Production.InitialDPR", "set", "txtValidated", _ent.txtValidated);















            Gears.UpdateData(DT1, _ent.Connection);


            Functions.AuditTrail("PRDWOUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(InitialDPR _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.InitialDPR", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDWOUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }


        #region Journal Entry
        public class JournalEntry
        {
            public virtual InitialDPR Parent { get; set; }
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
            public virtual InitialDPR Parent { get; set; }
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
