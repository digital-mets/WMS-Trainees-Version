using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
using Entity;
using System.Data;
using GearsTrading;
using DevExpress.Data.Filtering;
using GearsWarehouseManagement;
using GearsProcurement;

using System.Globalization;

namespace GWL
{
    public partial class frmProdWIPOut : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        private static string strError;
        Entity.ProdWIPOUT _Entity = new ProdWIPOUT();//Calls entity odsHeader/
        Entity.ProdWIPOUT.ProdWIPOUTDetail _EntityDetail1 = new ProdWIPOUT.ProdWIPOUTDetail();//Call entity sdsDetail
        Entity.ProdWIPOUT.ProdWIPOUTDetailCooking _EntityDetail2 = new ProdWIPOUT.ProdWIPOUTDetailCooking();//Call entity sdsDetail
      
     
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());


            //if (!string.IsNullOrEmpty(gljoborder.Text))
            //{
            //    Session["FilterItemCode"] = gljoborder.Text;
            //}


            string referer;
            try
            {
                referer = Request.ServerVariables["http_referer"];
            }
            catch
            {
                referer = "";
            }

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
            }



            if (!IsPostBack)
            {

                if (Request.QueryString["parameters"].ToString() == "OUT")
                {
                    FormTitle.Text = "WIP OUT";
                }
                else if (Request.QueryString["parameters"].ToString() == "FINAL")
                {
                    FormTitle.Text = "FINAL WIP OUT";

                }
                else if (Request.QueryString["parameters"].ToString() == "TIPIN")
                {
                    FormTitle.Text = "Unfinished WIP OUT (TIP IN)";
                    frmlayout1.FindItemOrGroupByName("backflush").ClientVisible = false;

                    frmlayout1.FindItemOrGroupByName("scrapcode").ClientVisible = true;
                    frmlayout1.FindItemOrGroupByName("scrapweight").ClientVisible = true;
                    frmlayout1.FindItemOrGroupByName("scrapdate").ClientVisible = true;
                    frmlayout1.FindItemOrGroupByName("scrapuom").ClientVisible = true;

                    frmlayout1.FindItemOrGroupByName("prodsite").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("stepseq").ClientVisible = false;


                }

                Session["atccode"] = null;
                Session["picklistdetail"] = null;
                Session["customoutbound"] = null;

                Session["batchdate"] = null;

                Session["sdsbatch"] = null;




                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                cmbType.Text = _Entity.Type.ToString();
                //txtDrNumber.Text = _Entity.DRDocnumber.ToString();
                //glAdjustment.Value = Convert.IsDBNull(_Entity.AdjustmentClass) ? "" : _Entity.AdjustmentClass.ToString();

                //Gridlookup_InitValue(sdsStep, txtStepCode, _Entity.Step.ToString());                
                //txtStepCode.Value = _Entity.Step.ToString();
               
                //cmbRecWorkCenter.Text = _Entity.ReceivedWorkCenter.ToString();
                
                txtRecWorkCenter.Text = _Entity.ReceivedWorkCenter.ToString();
                
                //cmbRecWorkCenter.Items.Add(_Entity.ReceivedWorkCenter.ToString());
                
                //gljoborder.Text = _Entity.JobOrder.ToString();

                //txtWorkCenter.Text = _Entity.WorkCenter.ToString();
                //chkIsWithDR.Value = _Entity.ClassA;

                
                txtDayNo.Text = _Entity.DayNo.ToString();
                txtYear.Text = _Entity.Year.ToString();
                txtWorkWeek.Text = _Entity.WorkWeek.ToString();
                //txtMachine.Text = _Entity.Machine;                
                txtPlanQty.Text = _Entity.PlanQty.ToString();
                txtActualQty.Text = _Entity.ActualQty.ToString();
                chkBackFlash.Value = _Entity.IsBackFlash;

                //2021-07-20    EMC add field

                hprodsite.Text = "MLI";

                string DatePD = _Entity.txtPD.ToString();
                if (DatePD == "" || DatePD == null)
                {
                    txtPD.Text = _Entity.txtPD.ToString();

                }
                else
                {
                    txtPD.Text = Convert.ToDateTime(_Entity.txtPD.ToString()).ToShortDateString();
                }

                Shift.Value = _Entity.Shift.ToString();
                txtProductName.Text = _Entity.txtProductName.ToString();
                txtMonitoredBy.Text = _Entity.txtMonitoredBy.ToString();
                MemoRemarks.Text = _Entity.MemoRemarks;


                hstepseq.Text = _Entity.StepSeq.ToString();
                hscrapcode.Text = _Entity.ScrapCode.ToString();
                hscrapdate.Text = String.IsNullOrEmpty(_Entity.ScrapDate.ToString()) ? null : Convert.ToDateTime(_Entity.ScrapDate.ToString()).ToShortDateString();

                hscrapweight.Text = _Entity.ScrapWeight.ToString();
                hscrapuom.Text = _Entity.ScrapUOM.ToString();


                Gridlookup_InitValue(sdsBatch, txtBatchNo, _Entity.BatchNo.ToString());
                //txtBatchNo.Text = _Entity.BatchNo.ToString();

                txtRemarks.Text = _Entity.Remarks;
                
                
                txtVatCode.Text = _Entity.VatCode.ToString();
                txtCurrency.Text = _Entity.Currency;
                speTotalQuantity.Value = _Entity.TotalQuantity;
                speWOP.Value = _Entity.WorkOrderPrice;
                speOrigWOP.Value = _Entity.OriginalWorkOrderPrice;
                
                speExchangeRate.Value = _Entity.ExchangeRate;
                speVatAmount.Value = _Entity.VatAmount;
                spePesoAmount.Value = _Entity.PesoAmount;
                speForeignAmount.Value = _Entity.ForeignAmount;
                speGrossVatableAmount.Value = _Entity.GrossVatableAmount;
                speNonVatableAmount.Value = _Entity.NonVatableAmount;
                speWithHoldingTax.Value = _Entity.WTaxAmount;
                speVatRate.Value = _Entity.VatRate;
                speAtc.Value = _Entity.AtcRate;
                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString();
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;


                //glSmokehouse.Value = _Entity.glSmokehouse;
                BatchNum.Value = _Entity.BatchNum;
            
                ////TimeStandard.Text = Convert.ToDateTime(_Entity.TimeStandard.ToString()).ToShortTimeString();
                //string TimeStd = _Entity.TimeStandard.ToString();
                //if (TimeStd == "" || TimeStd == null)
                //{
                //    TimeStandard.Text = _Entity.TimeStandard.ToString();

                //}
                //else
                //{
                //    TimeStandard.Text = Convert.ToDateTime(_Entity.TimeStandard.ToString()).ToShortDateString();
                //}
                ////TimeStart.Text = Convert.ToDateTime(_Entity.TimeStart.ToString()).ToShortTimeString();
                //string TimeStr = _Entity.TimeStart.ToString();
                //if (TimeStr == "" || TimeStr == null)
                //{
                //    TimeStart.Text = _Entity.TimeStart.ToString();

                //}
                //else
                //{
                //    TimeStart.Text = Convert.ToDateTime(_Entity.TimeStart.ToString()).ToShortTimeString();
                //}
                ////TimeEnd.Text = Convert.ToDateTime(_Entity.TimeEnd.ToString()).ToShortTimeString();
                //string TimeE = _Entity.TimeEnd.ToString();
                //if (TimeE == "" || TimeE == null)
                //{
                //    TimeEnd.Text = _Entity.TimeEnd.ToString();

                //}
                //else
                //{
                //    TimeEnd.Text = Convert.ToDateTime(_Entity.TimeEnd.ToString()).ToShortTimeString();
                //}


                //txtStoveTemp.Text = _Entity.txtStoveTemp;
                //txtStoveTempAct.Text = _Entity.txtStoveTempAct;
                //txtHumiditySTD.Text = _Entity.txtHumiditySTD;
                //txtHumidityAct.Text = _Entity.txtHumidityAct;
                //txtSteam.Text = _Entity.txtSteam;
                //txtInternal.Text = _Entity.txtInternal;
                //TxtWeighingAC.Text = _Entity.TxtWeighingAC;
                txtValidated.Text = _Entity.txtValidated;

                DataTable checkCount = _EntityDetail1.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "odsDetail";
               
                }
                else
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "sdsDetail";
                
                }

                DataTable checkCnt = _EntityDetail2.getdetailCooking(txtDocNumber.Text, Session["ConnString"].ToString());
                if (checkCnt.Rows.Count > 0)
                {

                    gv2.KeyFieldName = "DocNumber;LineNumber";
                    gv2.DataSourceID = "odsDetailCooking";
                }
                else
                {

                    gv2.KeyFieldName = "DocNumber;LineNumber";
                    gv2.DataSourceID = "sdsDetailCooking";
                }


                string SKU = _Entity.SKUcode.ToString();
                if (SKU == "" || SKU == null)
                {
                    sdsSKUCode.SelectCommand = "select A.ItemCode AS SKUCode, B.ProductName	from production.CounterPlanDetail A left join Masterfile.FGSKU B ON A.ItemCode = B.SKUCode";
                    sdsSKUCode.DataBind();



                }
                else
                {
                    string PD = _Entity.txtPD.ToString();
                    //sdsSKUCode.SelectCommand = "  Declare @PDWorkWeek varchar(50) SELECT @PDWorkWeek = DATEPART(WW,'" + PD + "') 	Declare @PDYear varchar(50) SELECT @PDYear = DATEPART(YEAR,'" + PD + "') select A.ItemCode AS SKUCode, B.ProductName from production.CounterPlanDetail A left join Masterfile.FGSKU B ON A.ItemCode = B.SKUCode where A.Docnumber = 'CP-MLI'+ @PDYear + @PDWorkWeek";
                    //sdsSKUCode.DataBind();

                    string strSKU = "";

                    strSKU = " Declare @PDWorkWeek varchar(50) SELECT @PDWorkWeek = DATEPART(WW,'" + PD + "') ";
                    strSKU = strSKU + " Declare @PDYear varchar(50) SELECT @PDYear = DATEPART(YEAR,'" + PD + "') ";
                    strSKU = strSKU + " select A.ItemCode AS SKUCode, B.ProductName from production.CounterPlanDetail A left join Masterfile.FGSKU B ON A.ItemCode = B.SKUCode ";
                    strSKU = strSKU + " where A.Docnumber = 'CP-MLI'+ @PDYear + @PDWorkWeek ";


                    sdsSKUCode.SelectCommand = strSKU;

                }

                txtskucode.Value = _Entity.SKUcode;
                txtskucode.Text = _Entity.SKUcode;






                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":

                        updateBtn.Text = "Add";
                        //gljoborder.Visible = true;

                        txtYear.Text = DateTime.Now.Year.ToString();
                        txtWorkWeek.Text = GetWorkWeek(DateTime.Now).ToString();
                        txtDayNo.Text = GetDayofWeek(DateTime.Now).ToString();
                        cmbType.Text = "OUT";

                        break;
                    case "E":

                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        sdsJobOrder.SelectCommand = "SELECT A.DocNumber,StepCode,WorkCenter,WorkOrderPrice FROM (SELECT DISTINCT A.DocNumber,MAX(Sequence) as Sequence FROM Production.JobOrder A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber group by A.DocNumber) as A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber and A.Sequence != B.Sequence where ISNULL(PreProd,0)=0 ";
                        sdsJobOrder.DataBind();
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;

                        break;
                    case "D":
                        sdsJobOrder.SelectCommand = "SELECT A.DocNumber,StepCode,WorkCenter,WorkOrderPrice FROM (SELECT DISTINCT A.DocNumber,MAX(Sequence) as Sequence FROM Production.JobOrder A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber group by A.DocNumber) as A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber and A.Sequence != B.Sequence where ISNULL(PreProd,0)=0 ";
                        sdsJobOrder.DataBind();
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }


                if (String.IsNullOrEmpty(cmbType.Text))
                {
                    //cmbType.Text = "Normal Out";                    
                    cmbType.Text = Request.QueryString["parameters"].ToString(); 
                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                    //gljoborder.ClientEnabled = true;

                  //  gvclass.DataSourceID = "odsDetail";
                    speExchangeRate.Value = 1.00;
                    //cmbType.Text = "Normal Out";


                    speWOP.ClientEnabled = false;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                    //gvclass.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gvclass.Settings.VerticalScrollableHeight = 200;
                    //gvclass.KeyFieldName = "LineNumber;PODocNumber";
                }
                else
                {
                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;
                   
                }


                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Production.WIPOUT where docnumber = '" + txtDocNumber.Text + "' and ISNULL(JobOrder,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }

                gvJournal.DataSourceID = "odsJournalEntry";
 
            }

            
                switch (Request.QueryString["parameters"].ToString())
                {

                    case "Cooking":
                        FormTitle.Text = "WIP OUT (Cooking)";
                        txtStepCode.ClientEnabled = false;
                        Session["StepC"] = "Cooking";

                        break;

                    default :
                            frmlayout1.FindItemOrGroupByName("glSmokehouse").Visible = false;
                            frmlayout1.FindItemOrGroupByName("BatchNum").Visible = false;
                            frmlayout1.FindItemOrGroupByName("TimeStandard").Visible = false;
                            //frmlayout1.FindItemOrGroupByName("TimeStart").Visible = false;
                            //frmlayout1.FindItemOrGroupByName("TimeEnd").Visible = false;
                            frmlayout1.FindItemOrGroupByName("txtStoveTemp").Visible = false;
                            frmlayout1.FindItemOrGroupByName("txtStoveTempAct").Visible = false;
                            frmlayout1.FindItemOrGroupByName("txtHumiditySTD").Visible = false;
                            frmlayout1.FindItemOrGroupByName("txtHumidityAct").Visible = false;
                            frmlayout1.FindItemOrGroupByName("txtSteam").Visible = false;
                            frmlayout1.FindItemOrGroupByName("txtInternal").Visible = false;
                            frmlayout1.FindItemOrGroupByName("TxtWeighingAC").Visible = false;
                            frmlayout1.FindItemOrGroupByName("txtValidated").Visible = false;
                        break;

                }





            //sdsBatch.SelectCommand = "SELECT BatchNo FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
            

            if (cmbType.Text == "Normal Out")
            {
                //glAdjustment.ClientEnabled = false; 
                speWOP.ClientEnabled = false;
                //chkIsWithDR.ClientEnabled = true; 
            }
            else
            {
                //glAdjustment.ClientEnabled = true;
                speWOP.ClientEnabled = true; 
                //chkIsWithDR.ClientEnabled = false;
            }

            gvJournal.DataSourceID = "odsJournalEntry";

            //if (Session["sdsbatch"] != null)
            //{
            //    sdsBatch.SelectCommand = Session["sdsbatch"].ToString();
            //}

        }


        #endregion


        private int GetDayofWeek(DateTime dtUse)
        {
            int DofW = 0;
            string strDayofWeek = dtUse.DayOfWeek.ToString();
            switch (strDayofWeek.Substring(0, 2).ToUpper())
            {
                case "SU":
                    DofW = 1;
                    break;
                case "MO":
                    DofW = 2;
                    break;
                case "TU":
                    DofW = 3;
                    break;
                case "WE":
                    DofW = 4;
                    break;
                case "TH":
                    DofW = 5;
                    break;
                case "FR":
                    DofW = 6;
                    break;
                case "SA":
                    DofW = 7;
                    break;

            }

            return DofW;
        }
        private int GetWorkWeek(DateTime dtUse)
        {
            // Gets the Calendar instance associated with a CultureInfo.
            CultureInfo myCI = new CultureInfo("en-US");
            //Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            int WorkWeek = myCI.Calendar.GetWeekOfYear(dtUse, myCWR, myFirstDOW);

            return WorkWeek;
        }
       


        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRDWOUT";//"PRDWOT";
            gparam._Connection = Session["ConnString"].ToString();
            strresult = GearsProduction.GProduction.WIPOUT_Validate(gparam);
        
            if (strresult.Length > 8 )
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n";//Message variable to client side
            }
        }
        #endregion
        #region Post
        private void Post()
        {

            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDWOT";
            gparam._Table = "Production.WipOut";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.ProdWIPOut_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }

        }
        #endregion


        private DataTable GetSelectedVal()
        {
            //2020-10-30 Convert approach to odsdetail and sdsdetail
            //Batch Detail
            gv3.DataSourceID = null;

            DataTable dt = new DataTable();
            DataTable getDetail = new DataTable();

            //string selectedValues = glRefMONum.Text.Replace(';', ',');

            //getDetail = Gears.RetriveData2("select ROW_NUMBER() OVER(ORDER BY StepSequence ASC) AS LineNumber, '" + txtDocNumber.Text + "' AS DocNumber, '1' AS SmokeHouseNo,StepSequence AS BatchNo, StepCode as CookingStage from Production.ProdRoutingStepPack  " +

            //                        "  WHERE SKUCode = '" + txtskucode.Text + "' order by LineNumber desc  ", Session["ConnString"].ToString());


            getDetail = Gears.RetriveData2(" EXEC dbo.sp_WipoutBatchTable '" + txtPD.Value + "','" + txtskucode.Text + "','" + txtDocNumber.Text + "'  ", Session["ConnString"].ToString());
                                                                                                
                            

         


            gv3.DataSource = getDetail;
            gv3.DataBind();

            gv3.KeyFieldName = "DocNumber;LineNumber";
            //END 2020-10-30 Convert approach to odsdetail and sdsdetail

            //Cooking Detail

            gv4.DataSourceID = null;

            DataTable dt2 = new DataTable();
            DataTable getDetail2 = new DataTable();


            getDetail2 = Gears.RetriveData2("select ROW_NUMBER() OVER(ORDER BY StepSequence ASC) AS LineNumber, '" + txtDocNumber.Text + "' AS DocNumber, '1' AS SmokeHouseNo,StepSequence AS BatchNo, StepCode as CookingStage from Production.ProdRoutingStepPack   " +

                                    " WHERE SKUCode = '" + txtskucode.Text + "' order by LineNumber desc  ", Session["ConnString"].ToString());


            //getDetail2 = Gears.RetriveData2("select * from production.Wipoutdetailcooking  " +

            //                        " WHERE LineNumber = '00001y'  ", Session["ConnString"].ToString());


            gv4.DataSource = getDetail2;
            gv4.DataBind();

            gv4.KeyFieldName = "DocNumber;LineNumber";
            //END 2020-10-30 Convert approach to odsdetail and sdsdetail

            return dt;
        }



        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;

     
        }

        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientVisible = !view;
        }

        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;

            if (Request.QueryString["entry"].ToString() == "E" || Request.QueryString["editcosting"] != null)
            {

                //if (!String.IsNullOrEmpty(glSupplierCode.Text))
                //{
                //    glSupplierCode.DropDownButton.Enabled = false;
                //    glSupplierCode.ReadOnly = true;
                  
                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "true")
                //{
                //Generatebtn.ClientEnabled = false;
                //glserviceorder.ClientEnabled = false;
                //glserviceorder.DropDownButton.Enabled = false;
                ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            
                //}
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void gvclass_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                //grid.SettingsBehavior.AllowGroup = false;
                //grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }

            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
          
        }

        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                //grid.SettingsBehavior.AllowGroup = false;
                //grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;

            }
        }

        protected void gvclass_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        #endregion

        #region Lookup Settings
        protected string Gridlookup_SQL(ASPxGridLookup gridLookup, string sqlCommand, string ActionType)
        {
            string strRetSql = "";
            
            string sdsID = gridLookup.DataSourceID;

            if (ActionType == "SET")
            {

                foreach (Control ca in this.Controls)
                {
                    if (ca is SqlDataSource)
                    {
                        if (((SqlDataSource)ca).ID == sdsID)
                        {
                            ((SqlDataSource)ca).SelectCommand = sqlCommand;
                            Session["sds" + sdsID] = sqlCommand;
                        }
                    }
                }


            }

            if (ActionType == "GET")
            {
                if (Session["sds" + sdsID] != null)
                {
                    strRetSql = Session["sds" + sdsID].ToString();
                }
            }

            return strRetSql;
        }

        protected void Gridlookup_InitValue(SqlDataSource sdsUsed, ASPxGridLookup gridLookup, string lookupValue)
        {
            //emclookup
            //string [] UseCol = lookupColumn.Split(';');
            string strSqlCmd = "SELECT ";
            for (int i = 0; i < gridLookup.Columns.Count; i++)
            {
                if (i != 0)
                {
                    strSqlCmd = strSqlCmd + " , ";
                }
                strSqlCmd = strSqlCmd + " '" + lookupValue + "' AS " + gridLookup.Columns[i].ToString().Replace(" ", "");
            }
            sdsUsed.SelectCommand = strSqlCmd;



     
        }

        protected void lookup_Init(object sender, EventArgs e)
        {

            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
//            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("txtBatchNo") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            //emclookup(2021)
            if (IsCallback && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))          
            {

                string strUseSqlCmd = Gridlookup_SQL(gridLookup, " ", "GET");
                if(strUseSqlCmd != "")
                {
                    Gridlookup_SQL(gridLookup, strUseSqlCmd, "SET");
                }


                //string strSdsID = gridLookup.DataSourceID;
                //if (Session["sds" + strSdsID] != null)
                //{
                //    string strUseSqlCmd = Session["sds" + strSdsID].ToString();

                //    foreach (Control ca in this.Controls)
                //    {
                //        if (ca is SqlDataSource)
                //        {
                //            if (((SqlDataSource)ca).ID == strSdsID)
                //            {
                //                ((SqlDataSource)ca).SelectCommand = strUseSqlCmd;
                //            }
                //        }


                //    }

                //}

                //if(strIDuse == "txtBatchNo")
                //{
                //    sdsBatch.SelectCommand = "SELECT BatchNo,MAX(SKUcode) AS SKUcode FROM Production.BatchQueue GROUP BY BatchNo";
                //}

                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                //if (Session["sdsbatch"] != null)
                //{

                //    sdsBatch.SelectCommand  = Session["sdsbatch"].ToString();

                //        //Session["batchdate"]
                //        //gridLookup.GridView.DataSourceID = "Masterfileitemdetail";

                //        //sdsBatch.SelectCommand = "SELECT BatchNo FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + Session["batchdate"] + "')";
            
                //        //Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                //        //Session["FilterExpression"] = null;
                //        gridLookup.DataBind();
                    
                //}

                

            }

            //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //if (Session["FilterExpression"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = "sdsItemDetail";
            //    sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
            //    //Session["FilterExpression"] = null;
            //}
        }
        
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string column = e.Parameters.Split('|')[0];//Set column name
            //if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            //string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            //string val = e.Parameters.Split('|')[2];//Set column value
            //if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            //ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gvclass.FindEditRowCellTemplateControl((GridViewDataColumn)gvclass.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
            //sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["FilterExpression"] = sdsItemDetail.FilterExpression;
            //grid.DataSourceID = "sdsItemDetail";
            //grid.DataBind();

            //for (int i = 0; i < grid.VisibleRowCount; i++)
            //{
            //    if (grid.GetRowValues(i, column) != null)
            //        if (grid.GetRowValues(i, column).ToString() == val)
            //        {
            //            grid.Selection.SelectRow(i);
            //            string key = grid.GetRowValues(i, column).ToString();
            //            grid.MakeRowVisible(key);
            //            break;
            //        }
            //}
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
       
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); 
                _Entity.DocNumber = txtDocNumber.Value.ToString();
                _Entity.DocDate = dtpDocDate.Text;
                _Entity.Type = cmbType.Text;
                //_Entity.AdjustmentClass = glAdjustment.Text;
                
                 //_Entity.JobOrder = gljoborder.Text;

                _Entity.SKUcode = txtskucode.Text;
                //_Entity.DayNo = Convert.ToInt32(txtDayNo.Text);
                //_Entity.Year = Convert.ToInt32(txtYear.Text);
                _Entity.WorkWeek = Convert.ToInt32(txtWorkWeek.Text);
                //_Entity.BatchNo = txtBatchNo.Text;
                
                //_Entity.Machine = txtMachine.Text;

                //2021-07-20    EMC add field
                _Entity.ProdSite = hprodsite.Text;
                _Entity.txtPD = txtPD.Text;
                _Entity.Shift = Shift.Text;
                _Entity.txtProductName = txtProductName.Text;
                _Entity.txtMonitoredBy = txtMonitoredBy.Text;
                _Entity.MemoRemarks = MemoRemarks.Text;
                _Entity.StepSeq = hstepseq.Text;
                _Entity.ScrapCode = hscrapcode.Text;
                _Entity.ScrapDate = hscrapdate.Text;
                _Entity.ScrapWeight = Convert.ToDecimal(Convert.IsDBNull(hscrapweight.Text) ? 0 : Convert.ToDecimal(hscrapweight.Text));
                _Entity.ScrapUOM = hscrapuom.Text;
                _Entity.Step = Session["StepC"].ToString();
                //_Entity.PlanQty = Convert.ToDecimal(Convert.IsDBNull(txtPlanQty.Text) ? 0 : Convert.ToDecimal(txtPlanQty.Text));
                //_Entity.ActualQty = Convert.ToDecimal(Convert.IsDBNull(txtActualQty.Text) ? 0 : Convert.ToDecimal(txtActualQty.Text));
                _Entity.IsBackFlash = Convert.ToBoolean(chkBackFlash.Value); ;

                //_Entity.ClassA = Convert.ToBoolean(chkIsWithDR.Value);
                //_Entity.AutoCharge = Convert.ToBoolean(chkAutoCharge.Value);
                //_Entity.WorkCenter = txtWorkCenter.Text;
                //_Entity.DRDocnumber = txtDrNumber.Text;

                //_Entity.Step = txtStepCode.Text;
                _Entity.ReceivedWorkCenter = txtRecWorkCenter.Text;
                _Entity.Remarks = txtRemarks.Text;
                
                _Entity.VatCode = txtVatCode.Text;
                _Entity.Currency = txtCurrency.Text;
     
                _Entity.TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(speTotalQuantity.Value) ? 0 : Convert.ToDecimal(speTotalQuantity.Value));
                _Entity.OriginalWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(speOrigWOP.Value) ? 0 : Convert.ToDecimal(speOrigWOP.Value));
                _Entity.WorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(speWOP.Value) ? 0 : Convert.ToDecimal(speWOP.Value));
                _Entity.ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(speExchangeRate.Value) ? 0 : Convert.ToDecimal(speExchangeRate.Value));
                _Entity.VatAmount = Convert.ToDecimal(Convert.IsDBNull(speVatAmount.Value) ? 0 : Convert.ToDecimal(speVatAmount.Value));
                _Entity.PesoAmount = Convert.ToDecimal(Convert.IsDBNull(spePesoAmount.Value) ? 0 : Convert.ToDecimal(spePesoAmount.Value));
                _Entity.ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(speForeignAmount.Value) ? 0 : Convert.ToDecimal(speForeignAmount.Value));
                _Entity.GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(speGrossVatableAmount.Value) ? 0 : Convert.ToDecimal(speGrossVatableAmount.Value));
                _Entity.NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(speNonVatableAmount.Value) ? 0 : Convert.ToDecimal(speNonVatableAmount.Value));
                _Entity.WTaxAmount = Convert.ToDecimal(Convert.IsDBNull(speWithHoldingTax.Value) ? 0 : Convert.ToDecimal(speWithHoldingTax.Value));
                _Entity.VatRate = Convert.ToDecimal(Convert.IsDBNull(speVatRate.Value) ? 0 : Convert.ToDecimal(speVatRate.Value));
                _Entity.AtcRate = Convert.ToDecimal(Convert.IsDBNull(speAtc.Value) ? 0 : Convert.ToDecimal(speAtc.Value));          
              
            
                _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
                _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
                _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
                _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
                _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
                _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
                _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
                _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
                _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
         


            _Entity.glSmokehouse = glSmokehouse.Text;
            _Entity.BatchNum = Convert.ToDecimal(Convert.IsDBNull(BatchNum.Value) ? 0 : Convert.ToDecimal(BatchNum.Value));
            _Entity.TimeStandard = TimeStandard.Text;
            _Entity.TimeStart = TimeStart.Text;
            _Entity.TimeEnd = TimeEnd.Text;
            _Entity.txtStoveTemp = txtStoveTemp.Text;
            _Entity.txtStoveTempAct = txtStoveTempAct.Text;

            _Entity.txtHumiditySTD = txtHumiditySTD.Text;
            _Entity.txtHumidityAct = txtHumidityAct.Text;
            _Entity.txtSteam = txtSteam.Text;
            _Entity.txtInternal = txtInternal.Text;
            _Entity.TxtWeighingAC = TxtWeighingAC.Text;
            _Entity.txtValidated = txtValidated.Text;
           


   



              //  _Entity.Transtype = Request.QueryString["transtype"].ToString();

                string param = e.Parameter.Split('|')[0]; //Renats
                switch (param)
            {

                case "Add":

                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        gv1.DataSourceID = "odsDetail";
                        gv2.DataSourceID = "odsDetailCooking";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        odsDetailCooking.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();
                        gv2.UpdateEdit();
                      
                        // _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate(); 
                        //Post();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        Session["StepC"] = null;

                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;
                case "Update":
            
                        _Entity.LastEditedBy = Session["userid"].ToString();
           
                        _Entity.LastEditedDate = DateTime.Now.ToString();
                    if (error == false)
                    {

                        strError = Functions.Submitted(_Entity.DocNumber, "Production.WIPOUT", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        gv1.DataSourceID = "odsDetail";                    
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;                      
                        gv1.UpdateEdit();

                        gv2.DataSourceID = "odsDetailCooking";
                        odsDetailCooking.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv2.UpdateEdit();

                       // _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate(); 
                        //Post();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        Session["StepC"] = null;
                        
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Delete":
                    //GetSelectedVal(); 
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Close":
                        cp.JSProperties["cp_close"] = true;
                     
                    break;
                case "Generates":
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    break;
               
                case "Generate":
                   string  a = e.Parameter.Split('|')[1]; //JONUmber
                   string  b = e.Parameter.Split('|')[2]; //Step
                   string  c = e.Parameter.Split('|')[3]; //Work Center


                   DataTable dttable = Gears.RetriveData2("SELECT DISTINCT A.DocNumber,B.StepCode,WorkCenter,WorkOrderPrice, DocDate, DueDate, LeadTime, CustomerCode, TotalJOQty "
                           + " , TotalINQty, TotalFinalQty, Remarks,Overhead FROM Production.JobOrder A  "
                           + " inner join Production.JOStepPlanning B "
                           + " on A.DocNumber = B.DocNumber "
                           + " and B.DocNumber = '" + a + "'"
                           + "  WHERE Status IN ('N','W') AND  ISNULL(ProdSubmittedBy,'')!='' "
                           + " and ISNULL(PreProd,0)='0'  and A.DocNumber = '" + a + "' AND B.StepCode = '" + b + "'", Session["ConnString"].ToString());



                    if (dttable.Rows.Count > 0)
                    {
                        txtStepCode.Text = dttable.Rows[0][1].ToString();
                        //txtWorkCenter.Text = dttable.Rows[0][2].ToString();
                        //txtOverhead.Text = dttable.Rows[0]["OverHead"].ToString();
                        if (cmbType.Text == "Normal Out")
                        {

                            speWOP.Value = Convert.ToDecimal(dttable.Rows[0][3]);
                            speOrigWOP.Value = Convert.ToDecimal(dttable.Rows[0][3]);
                        }
                        else
                        {
                            speWOP.Value = 0.00;
                            speOrigWOP.Value = Convert.ToDecimal(dttable.Rows[0][3]);
                        }
                        
                    }

                    if (cmbType.Text == "Normal Out")
                    {

                        speWOP.ClientEnabled = false;
                    }
                    else
                    {
                        speWOP.ClientEnabled = true;
                        speWOP.Value = 0.00;

                    }

                 
                    GetVat();
             


                    //DataTable dtreceived = Gears.RetriveData2("SELECT A.WorkCenter from Production.JOStepPlanning A " +
                    //    " INNER JOIN Production.JobOrder B "+
                    //    " ON A.DocNumber = B.DocNumber "+
                    //    " where A.DocNumber ='" + gljoborder.Value.ToString().Split('|')[0] + "' and ISNULL(PreProd,0)=0  and Sequence =  " +
                    //    " (CASE WHEN ISNULL(B.IsMultiIn,0)=1 THEN Sequence ELSE  "+
                    //    " (select MAX(Sequence)+1 from Production.JOStepPlanning " +
                    //    " where DocNumber ='" + gljoborder.Value.ToString().Split('|')[0] + "' and StepCode ='" + b + "') END) ", Session["ConnString"].ToString());
                
                    //if (dtreceived.Rows.Count > 0)
                    //  {
                    //      cmbRecWorkCenter.Text = dtreceived.Rows[0][0].ToString();
                    //      txtRecWorkCenter.Text = dtreceived.Rows[0][0].ToString();
                    //       cmbRecWorkCenter.Items.Add(dtreceived.Rows[0][0].ToString());
                    //  }
                          
                       
                     
                   //glSupplierCode.Enabled = false;
                    //Generatebtn.Enabled = false;
                    break;

                case "Type":
                    if (cmbType.Text == "Normal Out")
                    {
                        //glAdjustment.ClientEnabled = false;
                        //glAdjustment.Value = null;
                        speWOP.ClientEnabled = false;
                       // chkIsWithDR.ClientEnabled = true;
                        speWOP.ClientEnabled = false;
                    }
                    else
                    {
                        //glAdjustment.ClientEnabled = true; 
                        speWOP.ClientEnabled = true;
                        speWOP.Value = 0.00;
                        //chkIsWithDR.ClientEnabled = false; 
                    } 
                    break;

                case "docdate":

                    DateTime dt2 = Convert.ToDateTime(dtpDocDate.Text);
                    txtWorkWeek.Text = GetWorkWeek(dt2).ToString();
                    txtDayNo.Text = GetDayofWeek(dt2).ToString();
                    txtYear.Text = dt2.Year.ToString();

                    //Session["batchdate"] = dtpDocDate.Text;
                    //emc2021
                    //sdsBatch.SelectCommand = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                    string strSql3 = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                    Gridlookup_SQL(txtBatchNo, strSql3, "SET");

                    txtBatchNo.Text = "";
                    break;
                case "sds":
                    string  sdsID = e.Parameter.Split('|')[1]; //SDS object ID
                    string  sdsSqlCommand = e.Parameter.Split('|')[2]; //SQLcommand
                    string  glID = e.Parameter.Split('|')[3]; //GridLookup Obj



                    foreach (Control ca in this.Controls)
                    {
                        if (ca is SqlDataSource)
                        {

                            if (((SqlDataSource)ca).ID == sdsID)
                            {
                                //2021-07-22    EMC
                                //Special Case if yun SQLcommand may parameter na need i pasok base sa object ng FrontEnd
                                // i overwrite and variable na sdsSqlCommand

                                if (sdsID == "sdsBatch")
                                {
                                    sdsSqlCommand = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                                }


                                ((SqlDataSource)ca).SelectCommand = sdsSqlCommand;
                                Session["sds" + sdsID] = sdsSqlCommand;
                                //sdsCookingStage.SelectCommand = sdsSqlCommand;
                            }
                        }


                    }

                

                    //SqlDataSource sdsUsed = frmlayout1.Controls[]

                    //string strSqlCmd = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                    //sdsBatch.SelectCommand = strSqlCmd;


                    //if(hgridlookup.Text.Trim() == "")
                    //{
                    //    string strSqlCmd = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                    //    sdsBatch.SelectCommand = strSqlCmd;
                    //    //hgridlookup.Text = "GETDATA-DONE";
                    //}

                    //Session["sdsbatch"] = strSqlCmd;

                    //if (sdsBatch.SelectCommand != strSqlCmd)
                    //{
                    //    sdsBatch.SelectCommand = strSqlCmd;
                    //    //Session["sdsbatch"] = strSqlCmd;
                    //}


                    //if (Session["sdsbatch"] != strSqlCmd)
                    //{
                    //    sdsBatch.SelectCommand = strSqlCmd;
                    //    Session["sdsbatch"] = strSqlCmd;
                    //}

                    break;


                case "CookingStage":
                      
                    string CSSqlCmd = "SELECT DISTINCT StepCode FROM Production.ProdRoutingStepPack WHERE SKUCode ='" + txtskucode.Text + "'";
                    sdsCookingStage.SelectCommand = CSSqlCmd;
                    sdsCookingStage.DataBind();
                    break;
            }
        }


        private void GetVat()
        {

            DataTable getvat = Gears.RetriveData2("select ISNULL(Rate,0) as Rate,TaxCode,Currency from Masterfile.BPSupplierInfo A LEFT join Masterfile.Tax B " +
                                                       "on A.TaxCode = B.TCode " +
                                                       "where SupplierCode = '" + " " + "'", Session["ConnString"].ToString());

            if (getvat.Rows.Count > 0)
            {
                speVatRate.Value =Convert.ToDecimal(getvat.Rows[0]["Rate"]);
                txtVatCode.Text = getvat.Rows[0]["TaxCode"].ToString();
                txtCurrency.Text = getvat.Rows[0]["Currency"].ToString(); 
            }

            else
            {
                speVatRate.Value = 0.00;
                txtVatCode.Text = null;
                txtCurrency.Text = null;

            }
            DataTable getatc = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.ATC B " +
                                          "on A.ATCCode = B.ATCCode " +
                                          "where SupplierCode = '" + " " + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());
            
            if (getatc.Rows.Count > 0)
            {
                speAtc.Value = Convert.ToDecimal(getatc.Rows[0]["Rate"]);
            }

            else
            {
                speAtc.Value = 0.00;
            }


        }
   
        #endregion

     



    
   

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }


        protected void Connection_Init(object sender, EventArgs e)
        {

            foreach (Control a in this.Controls)
            {
                if(a is SqlDataSource)
                {

                    ((SqlDataSource)a).ConnectionString = Session["ConnString"].ToString();
                }
                

            }
            //sdsClass.ConnectionString = Session["ConnString"].ToString();
            //sdsAdjustmentClassification.ConnectionString = Session["ConnString"].ToString();
            //sdsJobOrder.ConnectionString = Session["ConnString"].ToString();
            //sdsRecWorkCenter.ConnectionString = Session["ConnString"].ToString();
            //sdsWarehouse.ConnectionString = Session["ConnString"].ToString();

        }

        protected void cmbRecWorkCenter_Callback(object sender, CallbackEventArgsBase e)
        {
            //if (e.Parameter == "clear")
            //{
            //      if (!string.IsNullOrEmpty(gljoborder.Text))
            
            //      {
            //    DataTable dtreceived = Gears.RetriveData2("SELECT A.WorkCenter from Production.JOStepPlanning A " +
            //         " INNER JOIN Production.JobOrder B " +
            //         " ON A.DocNumber = B.DocNumber " +
            //         " where A.DocNumber ='" + gljoborder.Value.ToString().Split('|')[0] + "' and ISNULL(PreProd,0)=0  and Sequence =  " +
            //         " (CASE WHEN ISNULL(B.IsMultiIn,0)=1 THEN Sequence ELSE  " +
            //         " (select MAX(Sequence)+1 from Production.JOStepPlanning " +
            //         " where DocNumber ='" + gljoborder.Value.ToString().Split('|')[0] + "' and StepCode ='" + txtStepCode.Text + "') END) ", Session["ConnString"].ToString());

            //    if (dtreceived.Rows.Count > 0)
            //    {
            //        //cmbRecWorkCenter.Text = dtreceived.Rows[0][0].ToString();
            //        //txtRecWorkCenter.Text = dtreceived.Rows[0][0].ToString();
            //        cmbRecWorkCenter.Items.Add(dtreceived.Rows[0][0].ToString());
            //    }
            //      }
            //}
        }
  




     


        
        
    }
}