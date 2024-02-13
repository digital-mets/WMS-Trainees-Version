using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GearsLibrary;
using System.IO;
using System.Data.OleDb;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Entity;
using System.Configuration;


namespace GWL.IT
{
    public partial class frmWIPOUTV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string[] Parameter() // Get parameters value
        {
            int maxItem = 2;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            string Conn = HttpContext.Current.Session["ConnString"].ToString();

            dtbl = Gears.RetriveData2("SELECT DISTINCT ItemCode AS Code, ProductName AS Name FROM Production.CounterPlan A INNER JOIN Production.CounterPlanDetail B ON A.Docnumber = B.Docnumber INNER JOIN Masterfile.FGSKU C ON B.ItemCode = C.SKUCode WHERE ISNULL(SubmittedBy,'')!='' AND ISNULL(C.IsInactive,0)=0", Conn);
            output[0] = JsonConvert.SerializeObject(dtbl);

            DataTable dt = Gears.RetriveData2("SELECT Access FROM IT.UsersDetail A INNER JOIN IT.UserRolesDetail B ON A.RoleID = B.RoleID WHERE ModuleID = 'PRDWIPV2' AND UserID = '" + HttpContext.Current.Session["userid"].ToString() + "'", Conn);

            dtbl = Gears.RetriveData2("SELECT REPLACE(REPLACE(AccessName, ']', ''), '[', '') AS Code, REPLACE(REPLACE(AccessName, ']', ''), '[', '') AS Name FROM IT.Access WHERE CHARINDEX(AccessCode,'" + dt.Rows[0][0].ToString() + "')>0 AND ISNUMERIC(AccessCode)=1", Conn);

            output[1] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] FilteredParameter(WIPOUTV2 _data) // Get parameters value
        {
            int maxItem = 1;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT DISTINCT ItemCode AS Code, ProductName AS Name " +
                                        "FROM Production.CounterPlan A " +
                                        "INNER JOIN Production.CounterPlanDetail B " +
                                        "ON A.Docnumber = B.Docnumber " +
                                        "INNER JOIN Masterfile.FGSKU C " +
                                        "ON B.ItemCode = C.SKUCode " +
                                        "WHERE ISNULL(SubmittedBy,'')!='' " +
                                        "AND Year='" + _data.Year + "' " +
                                        "AND WorkWeek='" + _data.WorkWeek + "' " +
                                        "AND ISNULL(C.IsInactive,0)=0", HttpContext.Current.Session["ConnString"].ToString());

            output[0] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] GetShift() // Get Shift Data
        {
            int maxItem = 1;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT ShiftCode AS Code, ShiftName AS Name FROM Masterfile.Shift WHERE ISNULL(IsInactive,0)=0", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] GetCookingSelects(WIPOUTV2 _data) // Get Cooking Selects Data
        {
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT ShiftCode AS Code, ShiftName AS Name FROM Masterfile.Shift WHERE ISNULL(IsInactive,0)=0", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT DISTINCT StepCode AS Code, StepCode AS Name FROM Production.ProdRoutingStepPack WHERE SKUCode = '" + _data.SKUCode + "'", HttpContext.Current.Session["ConnString"].ToString());
            output[1] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT CookingStage AS Code, CookingStage AS Name FROM Masterfile.FGSKUDetailCooking WHERE SKUCode='" + _data.SKUCode + "'", HttpContext.Current.Session["ConnString"].ToString());
            output[2] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] GetFGDispatch(WIPOUTV2 _data) // Get FG Dispatch Data
        {
            int maxItem = 5;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT BizPartnerCode AS Code, Name FROM Masterfile.BizPartner WHERE ISNULL([IsInactive],0)=0 and IsCustomer='1'", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT WarehouseCode AS Code, Description AS Name FROM Masterfile.Warehouse WHERE ISNULL([IsInactive],0)=0", HttpContext.Current.Session["ConnString"].ToString());
            output[1] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT ItemCode AS Code, FullDesc AS Name FROM Masterfile.item WHERE ISNULL(IsInactive,0)=0", HttpContext.Current.Session["ConnString"].ToString());
            output[2] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT ShiftCode AS Code, ShiftName AS Name FROM Masterfile.Shift WHERE ISNULL(IsInactive,0)=0", HttpContext.Current.Session["ConnString"].ToString());
            output[3] = JsonConvert.SerializeObject(dtbl);

            DataTable dt = Gears.RetriveData2("SELECT A.SAPNo FROM Production.WIPV2 A INNER JOIN Production.WIPV2Detail B ON A.DocNumber = B.DocNumber WHERE ProductionDate='" + _data.ProductionDate + "' AND SKUCode='" + _data.SKUCode + "' AND ISNULL(PortalSent,0)=0 AND StepCode = 'FG Dispatch'", HttpContext.Current.Session["ConnString"].ToString());
            string cnd = dt.Rows.Count == 0 ? "AND ISNULL(PortalSent, 0) = 0" : "";

            dtbl = Gears.RetriveData2("SELECT B.SAPNo FROM Production.WIPV2 A INNER JOIN Production.WIPV2Detail B ON A.DocNumber = B.DocNumber WHERE ProductionDate='" + _data.ProductionDate + "' AND SKUCode='" + _data.SKUCode + "' " + cnd + " AND ISNULL(B.SAPNo,'')!=''  AND StepCode = 'FG Dispatch' ", HttpContext.Current.Session["ConnString"].ToString());
            output[4] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] GetSGSelects() // Get SG Selects Data
        {
            int maxItem = 2;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT ShiftCode AS Code, ShiftName AS Name FROM Masterfile.Shift WHERE ISNULL(IsInactive,0)=0", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT ItemCode AS Code, FullDesc AS Name FROM Masterfile.Item WHERE ItemCategoryCode='007' AND ISNULL(IsInactive,0)=0", HttpContext.Current.Session["ConnString"].ToString());
            output[1] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] SendToPortalLimit() // Get Send To Portal Limit
        {
            int maxItem = 1;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='FGDPLIMIT'", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string SP_Call(WIPOUTV2 _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();
            string Year = _data.Year ?? "";
            string WorkWeek = _data.WorkWeek ?? "";
            string DayNo = _data.DayNo ?? "";
            string ProductionDate = _data.ProductionDate ?? "";
            string SKUCode = _data.SKUCode ?? "";
            string StepCode = _data.StepCode ?? "";
            string Action = _data.Action ?? "";
            string Action2 = _data.Action2 ?? "";
            string doc = _data.DocNumber ?? "";
            string BatchNo = _data.BatchNo ?? "";
            string SmokeHouseNo = _data.SmokeHouseNo ?? "";
            string ITAfterCooking = _data.ITAfterCooking ?? "";
            string ITValidationQA = _data.ITValidationQA ?? "";
            string Rerun = _data.Rerun ?? "";

            DataTable dt = Gears.RetriveData2("EXEC sp_WIPOUTV2 '" + Year + "', '" + WorkWeek + "', '" + DayNo + "', '" + ProductionDate + "', '" + SKUCode + "', '" + StepCode + "', '" + Action + "', '" + HttpContext.Current.Session["userid"].ToString() + "', '" + Action2 + "', '" + doc + "', '" + BatchNo + "', '" + SmokeHouseNo + "', '" + ITAfterCooking + "', '" + ITValidationQA + "', '" + Rerun + "'", Conn);

            return JsonConvert.SerializeObject(dt);
        }


        [WebMethod]
        public static string ViewICN(WIPOUTV2 _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();
            string ICN = _data.ICNNumber ?? "";
            DataTable dtTOLLPORTAL = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='PORTALCONN'", Conn);

            string Conn2 = dtTOLLPORTAL.Rows[0][0].ToString();

            DataTable dt = Gears.RetriveData2("SELECT * FROM IT.ICNDetail WHERE DocNumber='" + ICN + "'", Conn2);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static void Save(WIPOUTV2 _d, List<WIPOUTV2> _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();
            string SAPNo = (_d.SAPNo == null || _d.SAPNo == "") ? "SAPNo=NULL" : "SAPNo='" + _d.SAPNo + "'";
            string StepC = "";
            string PDate = "";
            string SKU = "";
            DataTable Step = Gears.RetriveData2("SELECT StepCode,ProductionDate,SKUCode FROM Production.WIPV2 WHERE DocNumber='" + _data[0].DocNumber + "'", Conn);
            StepC = Step.Rows[0][0].ToString();
            PDate = Step.Rows[0][1].ToString();
            SKU = Step.Rows[0][2].ToString();
            // Header Update
            if (StepC == "FG Dispatch")
            {
                Gears.RetriveData2("UPDATE Production.WIPV2 SET " + SAPNo + ",LastEditedBy='" + HttpContext.Current.Session["userid"].ToString() + "', LastEditedDate=GETDATE() WHERE DocNumber='" + _data[0].DocNumber + "'", Conn);
            }
            // Details Update
            foreach (var obj in _data)
            {
                string BatchNo = (obj.BatchNo == null || obj.BatchNo == "") ? "BatchNo=NULL" : "BatchNo='" + obj.BatchNo + "'";
                string StuffingMachineUsed = (obj.StuffingMachineUsed == null || obj.StuffingMachineUsed == "") ? "StuffingMachineUsed=NULL" : "StuffingMachineUsed='" + obj.StuffingMachineUsed + "'";
                string NoStrands = (obj.NoStrands == null || obj.NoStrands == "") ? "NoStrands=NULL" : "NoStrands='" + obj.NoStrands + "'";
                string WeightSmokecart = (obj.WeightSmokecart == null || obj.WeightSmokecart == "") ? "WeightSmokecart=NULL" : "WeightSmokecart='" + obj.WeightSmokecart + "'";
                string WeightBeforeCooking = (obj.WeightBeforeCooking == null || obj.WeightBeforeCooking == "") ? "WeightBeforeCooking=NULL" : "WeightBeforeCooking='" + obj.WeightBeforeCooking + "'";
                string CheckedBy = "CheckedBy='" + HttpContext.Current.Session["userid"].ToString() + "'";
                string StickProperlyArranged = (obj.StickProperlyArranged == null || obj.StickProperlyArranged == "") ? "StickProperlyArranged=NULL" : "StickProperlyArranged=" + obj.NoStrands;
                string FreeFromUnlinkUntwist = (obj.FreeFromUnlinkUntwist == null || obj.FreeFromUnlinkUntwist == "") ? "FreeFromUnlinkUntwist=NULL" : "FreeFromUnlinkUntwist=" + obj.FreeFromUnlinkUntwist;
                string HotdogArrangedProperly = (obj.HotdogArrangedProperly == null || obj.HotdogArrangedProperly == null) ? "HotdogArrangedProperly=NULL" : "HotdogArrangedProperly=" + obj.HotdogArrangedProperly;
                string SpiralMachineUsed = obj.SpiralMachineUsed == null ? "SpiralMachineUsed=NULL" : "SpiralMachineUsed='" + obj.SpiralMachineUsed + "'";
                string MachineSpeed = obj.MachineSpeed == null ? "MachineSpeed=NULL" : "MachineSpeed='" + obj.MachineSpeed + "'";
                string NoPacks = obj.NoPacks == null ? "NoPacks=NULL" : "NoPacks='" + obj.NoPacks + "'";
                string ITPriorLoading = obj.ITPriorLoading == null ? "ITPriorLoading=NULL" : "ITPriorLoading='" + obj.ITPriorLoading + "'";
                string TimeStarted = (obj.TimeStarted == null || obj.TimeStarted == "") ? "TimeStarted=NULL" : "TimeStarted='" + obj.TimeStarted + "'";
                string TimeFinished = (obj.TimeFinished == null || obj.TimeFinished == "") ? "TimeFinished=NULL" : "TimeFinished='" + obj.TimeFinished + "'";
                string NoLoosePacks = (obj.NoLoosePacks == null || obj.NoLoosePacks == "") ? "NoLoosePacks=NULL" : "NoLoosePacks='" + obj.NoLoosePacks + "'";
                string RoomTemp = (obj.RoomTemp == null || obj.RoomTemp == "") ? "RoomTemp=NULL" : "RoomTemp='" + obj.RoomTemp + "'";
                string SpiralTempQA = (obj.SpiralTempQA == null || obj.SpiralTempQA == "") ? "SpiralTempQA=NULL" : "SpiralTempQA='" + obj.SpiralTempQA + "'";
                string ITPriorLoadingQA = (obj.ITPriorLoadingQA == null || obj.ITPriorLoadingQA == "") ? "ITPriorLoadingQA=NULL" : "ITPriorLoadingQA='" + obj.ITPriorLoadingQA + "'";
                string BlastMachineUsed = (obj.BlastMachineUsed == null || obj.BlastMachineUsed == "") ? "BlastMachineUsed=NULL" : "BlastMachineUsed='" + obj.BlastMachineUsed + "'";
                string BoxedBy = (obj.BoxedBy == null || obj.BoxedBy == "") ? "BoxedBy=NULL" : "BoxedBy='" + obj.BoxedBy + "'";
                string LoadedBy = (obj.LoadedBy == null || obj.LoadedBy == "") ? "LoadedBy=NULL" : "LoadedBy='" + obj.LoadedBy + "'";
                string TimeSwitchedON = (obj.TimeSwitchedON == null || obj.TimeSwitchedON == "") ? "TimeSwitchedON=NULL" : "TimeSwitchedON='" + obj.TimeSwitchedON + "'";
                string TimeSwitchedOFF = (obj.TimeSwitchedOFF == null || obj.TimeSwitchedOFF == "") ? "TimeSwitchedOFF=NULL" : "TimeSwitchedOFF='" + obj.TimeSwitchedOFF + "'";
                string BlastTemp = obj.BlastTemp == null ? "BlastTemp=NULL" : "BlastTemp='" + obj.BlastTemp + "'";

                string SmokeHouseNo = (obj.SmokeHouseNo == null || obj.SmokeHouseNo == "") ? "SmokeHouseNo=NULL" : "SmokeHouseNo='" + obj.SmokeHouseNo + "'";
                string ITAfterCooking = (obj.ITAfterCooking == null || obj.ITAfterCooking == "") ? "ITAfterCooking=NULL" : "ITAfterCooking='" + obj.ITAfterCooking + "'";
                string WeightAfterCooking = (obj.WeightAfterCooking == null || obj.WeightAfterCooking == "") ? "WeightAfterCooking=NULL" : "WeightAfterCooking='" + obj.WeightAfterCooking + "'";
                string ITValidationQA = (obj.ITValidationQA == null || obj.ITValidationQA == "") ? "ITValidationQA=NULL" : "ITValidationQA='" + obj.ITValidationQA + "'";
                string MonitoredBy = "MonitoredBy='" + HttpContext.Current.Session["userid"].ToString() + "'";

                string PicklistNo = obj.PicklistNo == null ? "PicklistNo=NULL" : "PicklistNo='" + obj.PicklistNo + "'";
                string CustomerCode = obj.CustomerCode == null ? "CustomerCode=NULL" : "CustomerCode='" + obj.CustomerCode + "'";
                string WarehouseCode = obj.WarehouseCode == null ? "WarehouseCode=NULL" : "WarehouseCode='" + obj.WarehouseCode + "'";
                string ItemCode = obj.ItemCode == null ? "ItemCode=NULL" : "ItemCode='" + obj.ItemCode + "'";
                string OrderQty = (obj.OrderQty == null || obj.OrderQty == "") ? "OrderQty=NULL" : "OrderQty='" + obj.OrderQty + "'";
                string TransferedQty = (obj.TransferedQty == null || obj.TransferedQty == "") ? "TransferedQty=NULL" : "TransferedQty='" + obj.TransferedQty + "'";
                string RemainingQty = (obj.RemainingQty == null || obj.RemainingQty == "") ? "RemainingQty=NULL" : "RemainingQty='" + obj.RemainingQty + "'";
                string PalletNumber = obj.PalletNumber == null ? "PalletNumber=NULL" : "PalletNumber='" + obj.PalletNumber + "'";
                string BestBeforeDate = obj.BestBeforeDate == null ? "BestBeforeDate=NULL" : "BestBeforeDate='" + obj.BestBeforeDate + "'";
                string BoxUsed = obj.BoxUsed == null ? "BoxUsed=NULL" : "BoxUsed='" + obj.BoxUsed + "'";

                string NoBoxPerBatch = (obj.NoBoxPerBatch == null || obj.NoBoxPerBatch == "") ? "NoBoxPerBatch=NULL" : "NoBoxPerBatch='" + obj.NoBoxPerBatch + "'";
                string TotalPacks = (obj.TotalPacks == null || obj.TotalPacks == "") ? "TotalPacks=NULL" : "TotalPacks='" + obj.TotalPacks + "'";
                string ProductNameCk = (obj.ProductNameCk == null || obj.ProductNameCk == "") ? "ProductNameCk=NULL" : "ProductNameCk='" + obj.ProductNameCk + "'";
                string SKUCodeCk = (obj.SKUCodeCk == null || obj.SKUCodeCk == "") ? "SKUCodeCk=NULL" : "SKUCodeCk='" + obj.SKUCodeCk + "'";
                string PDCk = (obj.PDCk == null || obj.PDCk == "") ? "PDCk=NULL" : "PDCk='" + obj.PDCk + "'";
                string EDCk = (obj.EDCk == null || obj.EDCk == "") ? "EDCk=NULL" : "EDCk='" + obj.EDCk + "'";
                string BatchNoCk = (obj.BatchNoCk == null || obj.BatchNoCk == "") ? "BatchNoCk=NULL" : "BatchNoCk='" + obj.BatchNoCk + "'";
                string NoPacksCk = (obj.NoPacksCk == null || obj.NoPacksCk == "") ? "NoPacksCk=NULL" : "NoPacksCk='" + obj.NoPacksCk + "'";
                string TimeOfInspection = (obj.TimeOfInspection == null || obj.TimeOfInspection == "") ? "TimeOfInspection=NULL" : "TimeOfInspection='" + obj.TimeOfInspection + "'";

                string ScrapCode = obj.ScrapCode == null ? "ScrapCode=NULL" : "ScrapCode='" + obj.ScrapCode + "'";
                string UnlinkUntwist = (obj.UnlinkUntwist == null || obj.UnlinkUntwist == "") ? "UnlinkUntwist=NULL" : "UnlinkUntwist='" + obj.UnlinkUntwist + "'";
                string Miscut = (obj.Miscut == null || obj.Miscut == "") ? "Miscut=NULL" : "Miscut='" + obj.Miscut + "'";
                string Deform = (obj.Deform == null || obj.Deform == "") ? "Deform=NULL" : "Deform='" + obj.Deform + "'";
                string Others = (obj.Others == null || obj.Others == "") ? "Others=NULL" : "Others='" + obj.Others + "'";
                string TotalRejectScrap = (obj.TotalRejectScrap == null || obj.TotalRejectScrap == "") ? "TotalRejectScrap=NULL" : "TotalRejectScrap='" + obj.TotalRejectScrap + "'";
                string TotalFallen = (obj.TotalFallen == null || obj.TotalFallen == "") ? "TotalFallen=NULL" : "TotalFallen='" + obj.TotalFallen + "'";

                if (obj.CheckedBy != null)
                {
                    DataTable isCheckedBy = Gears.RetriveData2("SELECT CheckedBy FROM Production.WIPV2Detail WHERE RecordID='" + obj.RecordID + "'", Conn);
                    if (isCheckedBy.Rows[0][0].ToString() != null && isCheckedBy.Rows[0][0].ToString() != "") CheckedBy = "CheckedBy='" + isCheckedBy.Rows[0][0].ToString() + "'";
                }
                if (obj.MonitoredBy != null)
                {
                    DataTable isMonitoredBy = Gears.RetriveData2("SELECT MonitoredBy FROM Production.WIPV2Detail WHERE RecordID='" + obj.RecordID + "'", Conn);
                    if (isMonitoredBy.Rows[0][0].ToString() != null && isMonitoredBy.Rows[0][0].ToString() != "") MonitoredBy = "MonitoredBy='" + isMonitoredBy.Rows[0][0].ToString() + "'";
                }

                if (StepC == "FG Dispatch")
                {
                    {
                        DataTable isSubmitted = Gears.RetriveData2("SELECT IsSubmitted,CONVERT(VARCHAR(20), FLOOR(NoBoxPerBatch)) as NoBoxPerBatch FROM Production.WIPV2Detail WHERE RecordID='" + obj.RecordID + "' AND ISNULL(IsSubmitted,0) !=0", Conn);
                        if (isSubmitted.Rows.Count > 0)
                        {
                            object NoBoxPerBatchOld = isSubmitted.Rows[0].ItemArray[1];
                            if (obj.NoBoxPerBatch.ToString() != NoBoxPerBatchOld.ToString())
                            {
                                DataTable isMonitoredBy = Gears.RetriveData2("INSERT INTO IT.FGAuditTrail (DocNumber,FGRecordID,ProductionDate,SKUCode,PalletID,NoofBoxPerBatch,OldValue,EditedBy,EditedDate) SELECT '" + obj.DocNumber + "','" + obj.RecordID + "','" + obj.ProductionDate + "','" + obj.SKUCode + "','" + obj.PalletNumber + "','" + obj.NoBoxPerBatch + "','" + isSubmitted.Rows[0][1].ToString() + "','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", Conn);

                            }
                        }
                    }
                }
                else if (StepC == "SG Cooking" || StepC == "SG Brine Chilling" || StepC == "SG Packaging") {
                    DataTable isSubmitted2 = Gears.RetriveData2("SELECT B.SubmittedDate,CONVERT(VARCHAR(20), FLOOR(UnlinkUntwist)) as UnlinkUntwist FROM Production.WIPV2Detail A LEFT JOIN  Production.WIPV2 B on A.DocNumber = B.DocNumber WHERE A.RecordID='" + obj.RecordID + "' AND ISNULL(B.SubmittedDate,'') !=''", Conn);
                    if (isSubmitted2.Rows.Count > 0)
                    {
                        object UnlinkUntwistOld = isSubmitted2.Rows[0].ItemArray[1];
                        if (obj.UnlinkUntwist.ToString() != UnlinkUntwistOld.ToString())
                        {
                            DataTable isMonitoredBy = Gears.RetriveData2("INSERT INTO IT.ScrapAuditTrail (DocNumber,ScrapRecordID,ProductionDate,SKUCode,StepCode,UnlinkUntwist,OldValue,EditedBy,EditedDate) SELECT '" + obj.DocNumber + "','" + obj.RecordID + "','" + PDate + "','" + SKU + "','" + StepC  + "','" + obj.UnlinkUntwist + "','" + isSubmitted2.Rows[0][1].ToString() + "','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", Conn);

                        }
                    }
                }
                if (StepC == "FG Dispatch")
                {
                    Gears.RetriveData2("UPDATE Production.WIPV2Detail SET " + BatchNo + "," + StuffingMachineUsed + ", " + NoStrands + " " +
                        ", " + WeightSmokecart + ", " + WeightBeforeCooking + ", Shift='" + obj.Shift + "'" +
                        ", " + CheckedBy + ", " + StickProperlyArranged + ", " + FreeFromUnlinkUntwist + " " +
                        ", " + HotdogArrangedProperly + ", Remarks='" + obj.Remarks + "'" +
                        ", " + SpiralMachineUsed + ", " + MachineSpeed + ", " + NoPacks + " " +
                        ", " + ITPriorLoading + ", " + TimeStarted + ", " + TimeFinished + " " +
                        ", " + NoLoosePacks + ", " + RoomTemp + ", " + SpiralTempQA + " " +
                        ",  " + ITPriorLoadingQA + ", " + BlastMachineUsed + ", " + BoxedBy + " " +
                        ", " + LoadedBy + ", " + TimeSwitchedON + ", " + TimeSwitchedOFF + " , " + BlastTemp + " " +
                        ", " + SmokeHouseNo + ", " + ITAfterCooking + ", " + ITValidationQA + " , " + MonitoredBy + " " +
                        ", " + ScrapCode + ", " + UnlinkUntwist + ", " + Miscut + " , " + Deform + " " +
                        ", " + Others + ", " + TotalRejectScrap + ", " + TotalFallen + " " +
                        ", " + PicklistNo + ", " + CustomerCode + ", " + WarehouseCode + " " +
                        ", " + ItemCode + ", " + OrderQty + ", " + TransferedQty + " " +
                        ", " + RemainingQty + ", " + BestBeforeDate + ", " + PalletNumber + ", " + BoxUsed + " " +
                        ", " + NoBoxPerBatch + ", " + TotalPacks + ", " + ProductNameCk + " " +
                        ", " + SKUCodeCk + ", " + PDCk + ", " + EDCk + " " +
                        ", " + BatchNoCk + ", " + NoPacksCk + ", " + TimeOfInspection + ", " + WeightAfterCooking + " " +
                        ", " + SAPNo + " " +
                        " WHERE RecordID='" + obj.RecordID + "'", Conn);
                }
                else
                {
                    Gears.RetriveData2("UPDATE Production.WIPV2Detail SET " + BatchNo + "," + StuffingMachineUsed + ", " + NoStrands + " " +
                       ", " + WeightSmokecart + ", " + WeightBeforeCooking + ", Shift='" + obj.Shift + "'" +
                       ", " + CheckedBy + ", " + StickProperlyArranged + ", " + FreeFromUnlinkUntwist + " " +
                       ", " + HotdogArrangedProperly + ", Remarks='" + obj.Remarks + "'" +
                       ", " + SpiralMachineUsed + ", " + MachineSpeed + ", " + NoPacks + " " +
                       ", " + ITPriorLoading + ", " + TimeStarted + ", " + TimeFinished + " " +
                       ", " + NoLoosePacks + ", " + RoomTemp + ", " + SpiralTempQA + " " +
                       ",  " + ITPriorLoadingQA + ", " + BlastMachineUsed + ", " + BoxedBy + " " +
                       ", " + LoadedBy + ", " + TimeSwitchedON + ", " + TimeSwitchedOFF + " , " + BlastTemp + " " +
                       ", " + SmokeHouseNo + ", " + ITAfterCooking + ", " + ITValidationQA + " , " + MonitoredBy + " " +
                       ", " + ScrapCode + ", " + UnlinkUntwist + ", " + Miscut + " , " + Deform + " " +
                       ", " + Others + ", " + TotalRejectScrap + ", " + TotalFallen + " " +
                       ", " + PicklistNo + ", " + CustomerCode + ", " + WarehouseCode + " " +
                       ", " + ItemCode + ", " + OrderQty + ", " + TransferedQty + " " +
                       ", " + RemainingQty + ", " + BestBeforeDate + ", " + PalletNumber + ", " + BoxUsed + " " +
                       ", " + NoBoxPerBatch + ", " + TotalPacks + ", " + ProductNameCk + " " +
                       ", " + SKUCodeCk + ", " + PDCk + ", " + EDCk + " " +
                       ", " + BatchNoCk + ", " + NoPacksCk + ", " + TimeOfInspection + ", " + WeightAfterCooking + " " +
                       " WHERE RecordID='" + obj.RecordID + "'", Conn);
                }

            }
        }

        [WebMethod]
        public static void SaveCooking(List<WIPOUTV2> _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();

            foreach (var obj in _data)
            {
                string BatchNo = obj.BatchNo == "" ? "BatchNo=NULL" : "BatchNo='" + obj.BatchNo + "'";
                string CookingStage = obj.CookingStage == "" ? "CookingStage=NULL" : "CookingStage='" + obj.CookingStage + "'";
                string SmokeHouseNo = obj.SmokeHouseNo == "" ? "SmokeHouseNo=NULL" : "SmokeHouseNo='" + obj.SmokeHouseNo + "'";
                string ITAfterCooking = obj.ITAfterCooking == "" ? "ITAfterCooking=NULL" : "ITAfterCooking='" + obj.ITAfterCooking + "'";
                string ITValidationQA = obj.ITValidationQA == "" ? "ITValidationQA=NULL" : "ITValidationQA='" + obj.ITValidationQA + "'";
                string StdCookingTime = obj.StdCookingTime == "" ? "StdCookingTime=NULL" : "StdCookingTime='" + obj.StdCookingTime + "'";
                string TimeStart = obj.TimeStart == "" ? "TimeStart=NULL" : "TimeStart='" + obj.TimeStart + "'";
                string TimeEnd = obj.TimeEnd == "" ? "TimeEnd=NULL" : "TimeEnd='" + obj.TimeEnd + "'";
                string StoveTempStd = obj.StoveTempStd == "" ? "StoveTempStd=NULL" : "StoveTempStd='" + obj.StoveTempStd + "'";
                string StoveTempActual = obj.StoveTempActual == "" ? "StoveTempActual=NULL" : "StoveTempActual='" + obj.StoveTempActual + "'";
                string PercentHumidityStd = obj.PercentHumidityStd == "" ? "PercentHumidityStd=NULL" : "PercentHumidityStd='" + obj.PercentHumidityStd + "'";
                string PercentHumidityActual = obj.PercentHumidityActual == "" ? "PercentHumidityActual=NULL" : "PercentHumidityActual='" + obj.PercentHumidityActual + "'";

                Gears.RetriveData2("UPDATE Production.WIPV2DetailCooking SET " +
                    " " + BatchNo + ", " + CookingStage + ", " + StdCookingTime +
                    ", " + TimeStart + ", " + TimeEnd + ", " + StoveTempStd +
                    ", " + StoveTempActual + ", " + PercentHumidityStd + ", " + PercentHumidityActual +
                    ", " + SmokeHouseNo + ", " + ITAfterCooking + ", " + ITValidationQA + "  " +
                    "WHERE RecordID='" + obj.RecordID + "'", Conn);
            }

            DataTable dtDocNumber = Gears.RetriveData2("SELECT DocNumber FROM Production.WIPV2DetailCooking WHERE RecordID='" + _data[0].RecordID + "'", Conn);

            string[] batches = _data[0].BatchNo.Split(',');

            foreach (var batch in batches)
            {
                DataTable isMonitored = Gears.RetriveData2("SELECT MonitoredBy FROM Production.WIPV2Detail WHERE DocNumber = '" + dtDocNumber.Rows[0][0].ToString() + "' AND BatchNo='" + batch + "'", Conn);
                string isMonitoredBy = isMonitored.Rows[0][0].ToString() ?? HttpContext.Current.Session["userid"].ToString();

                Gears.RetriveData2("UPDATE Production.WIPV2Detail " +
                    "SET SmokeHouseNo='" + _data[0].SmokeHouseNo + "', " +
                    "ITAfterCooking='" + _data[0].ITAfterCooking + "', " +
                    "ITValidationQA='" + _data[0].ITValidationQA + "', " +
                    "Rerun='" + _data[0].Rerun + "', " +
                    "MonitoredBy='" + isMonitored + "' " +
                    "WHERE DocNumber='" + dtDocNumber.Rows[0][0].ToString() + "' " +
                    "AND BatchNo='" + batch + "'", Conn);

                Gears.RetriveData2("UPDATE Production.WIPV2DetailCooking " +
                    "WHERE DocNumber='" + dtDocNumber.Rows[0][0].ToString() + "' " +
                    "AND BatchNo='" + _data[0].BatchNo + "'", Conn);
            }
        }

        [WebMethod]
        public static void CloseCookingStage(List<WIPOUTV2> _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();



            DataTable dtDocNumber = Gears.RetriveData2("SELECT DocNumber FROM Production.WIPV2DetailCooking WHERE RecordID='" + _data[0].RecordID + "'", Conn);


            Gears.RetriveData2("DELETE FROM Production.WIPV2DetailCooking " +
                   "WHERE DocNumber='" + dtDocNumber.Rows[0][0].ToString() + "' " +
                   "AND BatchNo is null ", Conn);


        }

        [WebMethod]
        public static void SaveNewCookingStage(WIPOUTV2 _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();

            DataTable dtCooking = Gears.RetriveData2("SELECT CookingStage, StdCookingTime, StoveTempStd, PercentHumidityStd FROM Masterfile.FGSKUDetailCooking WHERE SKUCode='" + _data.SKUCode + "' AND CookingStage='" + _data.CookingStage + "'", Conn);

            foreach (DataRow dr in dtCooking.Rows)
            {
                Gears.RetriveData2("INSERT INTO Production.WIPV2DetailCooking (DocNumber, CookingStage, StdCookingTime, StoveTempStd, PercentHumidityStd)" +
                                    "SELECT '" + _data.DocNumber + "', '" + dr[0].ToString() + "', '" + dr[1].ToString() + "', '" + dr[2].ToString() + "', '" + dr[3].ToString() + "' FROM Masterfile.FGSKUDetailCooking WHERE SKUCode='" + _data.SKUCode + "' AND CookingStage='" + _data.CookingStage + "'", Conn);
            }
        }

        [WebMethod]
        public static void SendToPortal(WIPOUTV2 _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();

            DataTable dtTOLLCC = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='FGDCC'", Conn);
            DataTable dtTOLLWHC = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='FGDWHC'", Conn);
            DataTable dtTOLLPORTAL = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='PORTALCONN'", Conn);
            DataTable dtTOLLExpDate = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='FGDEXPDATE'", Conn);
            DataTable dtTOLLUser = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='FGDUSER'", Conn);

            string Conn2 = dtTOLLPORTAL.Rows[0][0].ToString();

            DataTable dtDoc = Gears.RetriveData2("SELECT SeriesWidth, SeriesNumber FROM IT.BizPartner WHERE BizPartnerCode='" + dtTOLLCC.Rows[0][0].ToString() + "'", Conn2);

            string test = dtTOLLCC.Rows[0][0].ToString();
            string test1 = dtDoc.Rows[0][0].ToString();
            string test2 = dtDoc.Rows[0][1].ToString();

            DataTable dtTOLLH = Gears.RetriveData2("SELECT 'ICN' + '" + dtTOLLCC.Rows[0][0].ToString() + "' + " +
                                                    "REPLICATE('0', " + dtDoc.Rows[0][0].ToString() + " - " +
                                                    "LEN((" + dtDoc.Rows[0][1].ToString() + " + " +
                                                    "(" + dtDoc.Rows[0][1].ToString() + " + 1))))+ CAST(" + dtDoc.Rows[0][1].ToString() + " +1 AS VARCHAR) AS DocNumber, " +
                                                    "'MLI PROD OPS',GETDATE(),'FRABELLE CORP','Normal','1','Local Transfer', " +
                                                    "GETDATE(), 'METS', 'No', 'No', 'Mets 5', 0, 'NA', " +
                                                    "'NA', 'NA', 'NA', '" + dtTOLLCC.Rows[0][0].ToString() + "', " +
                                                    "'" + dtTOLLWHC.Rows[0][0].ToString() + "', GETDATE(), '" + dtTOLLUser.Rows[0][0].ToString() + "', GETDATE(), '" + dtTOLLUser.Rows[0][0].ToString() + "' ", Conn);

            DataTable dtTOLLD = Gears.RetriveData2("SELECT 'ICN' + '" + dtTOLLCC.Rows[0][0].ToString() + "' + " +
                                                   "REPLICATE('0', " + dtDoc.Rows[0][0].ToString() + " - " +
                                                   "LEN((" + dtDoc.Rows[0][1].ToString() + " + " +
                                                   "(" + dtDoc.Rows[0][1].ToString() + " + 1))))+ CAST(" + dtDoc.Rows[0][1].ToString() + " +1 AS VARCHAR) AS DocNumber, " +
                                                    "A.SKUCode, C.ProductName, B.SAPNo,BatchNo, " +
                                                    "ProductionDate, DATEADD(day, " + dtTOLLExpDate.Rows[0][0].ToString() + ", ProductionDate), " +
                                                    "TotalPacks, '', PalletNumber, '" + dtTOLLCC.Rows[0][0].ToString() + "', " +
                                                    "'" + dtTOLLWHC.Rows[0][0].ToString() + "', 0 " +
                                                    "FROM Production.WIPV2 A " +
                                                    "INNER JOIN Production.WIPV2Detail B " +
                                                    "ON A.DocNumber = B.DocNumber " +
                                                    "INNER JOIN Masterfile.FGSKU  C " +
                                                    "ON A.SKUCode = C.SKUCode " +
                                                    //"WHERE A.ProductionDate = '" + _data.ProductionDate + "' " +
                                                    //"AND A.SKUCode = '" + _data.SKUCode + "' " +
                                                    //"AND A.StepCode = '" + _data.StepCode + "'" +
                                                    //"AND ISNULL(PortalSent,0)=0 AND ISNULL(IsSubmitted,0)!=0", Conn);    
                                                    "WHERE B.RecordID in (" + _data.RecordID + ") ", Conn);


            foreach (DataRow dr in dtTOLLH.Rows)
            {
                Gears.RetriveData2("INSERT INTO IT.ICN (DocNumber, CompanyDept, TransactionDate, Consignee, Type, RefDoc, ShipmentType, " +
                                    "LoadingTime, ConsigneeAddress, Overtime, AddtionalManpower, SuppliedBy, NOManpower, TruckProviderByMets, " +
                                    "TrackingNO, PlateNO, DriverName, CustomerCode, WarehouseCode, AddedDate, AddedBy, SubmittedDate, SubmittedBy) " +
                                    "SELECT '" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + dr[5].ToString() + "','" + dr[6].ToString() + "', " +
                                    "'" + dr[7].ToString() + "','" + dr[8].ToString() + "','" + dr[9].ToString() + "','" + dr[10].ToString() + "','" + dr[11].ToString() + "','" + dr[12].ToString() + "','" + dr[13].ToString() + "'," +
                                    "'" + dr[14].ToString() + "','" + dr[15].ToString() + "','" + dr[16].ToString() + "','" + dr[17].ToString() + "','" + dr[18].ToString() + "','" + dr[19].ToString() + "','" + dr[20].ToString() + "','" + dr[21].ToString() + "','" + dr[22].ToString() + "'", Conn2);
            }

            foreach (DataRow dr in dtTOLLD.Rows)
            {
                Gears.RetriveData2("INSERT INTO IT.ICNDetail (DocNumber,ItemCode,ItemDescription,BatchNumber,LotNo,MfgDate,ExpDate, " +
                                    "Qty, Packing, SpecialHandlingInstruc, CustomerCode, WarehouseCode, Weight) " +
                                    "SELECT '" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + dr[5].ToString() + "','" + dr[6].ToString() + "'," +
                                    "'" + dr[7].ToString() + "','" + dr[8].ToString() + "','" + dr[9].ToString() + "','" + dr[10].ToString() + "','" + dr[11].ToString() + "','" + dr[12].ToString() + "'", Conn2);
            }

            Gears.RetriveData2("UPDATE Production.WIPV2Detail " +
                                "SET PortalSent = 1, " +
                                "ICNNumber = 'ICN' + '" + dtTOLLCC.Rows[0][0].ToString() + "' + " +
                                                   "REPLICATE('0', " + dtDoc.Rows[0][0].ToString() + " - " +
                                                   "LEN((" + dtDoc.Rows[0][1].ToString() + " + " +
                                                   "(" + dtDoc.Rows[0][1].ToString() + " + 1))))+ CAST(" + dtDoc.Rows[0][1].ToString() + " +1 AS VARCHAR) " +
                                "FROM Production.WIpv2 A " +
                                "INNER JOIN Production.WIPV2Detail B " +
                                "ON A.DocNumber = B.DocNumber " +
                                //"WHERE A.ProductionDate = '" + _data.ProductionDate + "' " +
                                //"AND A.SKUCode = '" + _data.SKUCode + "' " +
                                //"AND A.StepCode = '" + _data.StepCode + "' " +
                                //"AND ISNULL(PortalSent,0)=0 " +
                                //"AND ISNULL(PalletNumber,'')!=''", Conn);
                                "WHERE B.RecordID in (" + _data.RecordID + ") ", Conn);

            Gears.RetriveData2("UPDATE IT.BizPartner SET SeriesNumber=SeriesNumber+1 WHERE BizPartnerCode='" + dtTOLLCC.Rows[0][0].ToString() + "'", Conn2);
        }

        [WebMethod]
        public static bool CheckIfCookingStageExist(WIPOUTV2 _data)
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();
            bool isExisting = false;

            DataTable dt = Gears.RetriveData2("SELECT * FROM Masterfile.FGSKUDetailCooking WHERE SKUCode='" + _data.DocNumber + "'", Conn);
            if (dt.Rows.Count > 0)
            {
                isExisting = true;
            }

            return isExisting;
        }
    }
}