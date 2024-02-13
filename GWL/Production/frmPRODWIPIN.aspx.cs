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
    public partial class frmPRODWIPIN : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string a = ""; //Renats
        string b = ""; //Renats
        string c = ""; //Renats
        private static string strError;
        Entity.PRODWIPIN _Entity = new PRODWIPIN();//Calls entity odsHeader
        Entity.PRODWIPIN.WIClassBreakDown _EntityDetail = new PRODWIPIN.WIClassBreakDown();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

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

                Session["atccode"] = null;
                Session["picklistdetail"] = null;
                Session["customoutbound"] = null;

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                       
                        updateBtn.Text = "Add";

                        txtYear.Text = DateTime.Now.Year.ToString();
                        txtWorkWeek.Text = GetWorkWeek(DateTime.Now).ToString();
                        txtDayNo.Text = GetDayofWeek(DateTime.Now).ToString();


                        break;
                    case "E":

                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;

                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }
              
                switch (Request.QueryString["parameters"].ToString())
                {
                    case "Weighing":
                        Session["StepC"] = "Weighing";
                        FormTitle.Text = "WIP IN (Weighing)";

                        //txtStepCode.Text = "Weighing";
                        //txtStepCode.ClientEnabled = false;
                        
                        //Blasting
                        frmlayout1.FindItemOrGroupByName("TimeOn").Visible = false;
                        frmlayout1.FindItemOrGroupByName("TimeOff").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtBlastTemp").Visible = false;
                        //frmlayout1.FindItemOrGroupByName("txtProductName").Visible = true;
                        //frmlayout1.FindItemOrGroupByName("txtPD").Visible = true;
                        frmlayout1.FindItemOrGroupByName("NumPacks").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtLoadedBy").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtMonitoredBy").Visible = false;

                        frmlayout1.FindItemOrGroupByName("txtStepCode").Visible = false;
                        frmlayout1.FindItemOrGroupByName("stepseq").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtPlanQty").ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("txtActualQty").ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("txtYear").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtDayNo").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtBlastMachine").Visible = false;


                        //Spiral
                        frmlayout1.FindItemOrGroupByName("glSpiralMach").Visible = false;
                        frmlayout1.FindItemOrGroupByName("QtyPacks").Visible = false;
                        frmlayout1.FindItemOrGroupByName("QtyLoosePacks").Visible = false;
                        frmlayout1.FindItemOrGroupByName("TimeStarted").Visible = false;
                        frmlayout1.FindItemOrGroupByName("TimeFinished").Visible = false;
                        frmlayout1.FindItemOrGroupByName("IntTempPL").Visible = false;
                        frmlayout1.FindItemOrGroupByName("StdRoomTemp").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtQAVSpiral").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtQAVPLoad").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtQAVValBy").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtMachSpeed").Visible = false;
                        frmlayout1.FindItemOrGroupByName("QAVal").Visible = false;
                         


                    
                        break;


                    case "Spiral":
                        Session["StepC"] = "Spiral Freezing";
                        FormTitle.Text = "WIP IN (Spiral)";
                       
                        txtStepCode.ClientEnabled = false;
                       
                       

                        //Weighing
                        frmlayout1.FindItemOrGroupByName("NumStrands").Visible = false;
                        frmlayout1.FindItemOrGroupByName("glStuffingMach").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtWeightSmokecart").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtWeightbefore").Visible = false;
                        frmlayout1.FindItemOrGroupByName("cbQCStick").Visible = false;
                        frmlayout1.FindItemOrGroupByName("cbQCHotdog").Visible = false;
                        frmlayout1.FindItemOrGroupByName("cbQCfreefrom").Visible = false;
                        frmlayout1.FindItemOrGroupByName("HeadQuality").Visible = false;


                        //Blasting
                        frmlayout1.FindItemOrGroupByName("TimeOn").Visible = false;
                        frmlayout1.FindItemOrGroupByName("TimeOff").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtBlastTemp").Visible = false;
                        frmlayout1.FindItemOrGroupByName("NumPacks").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtLoadedBy").Visible = false;


                        frmlayout1.FindItemOrGroupByName("txtStepCode").Visible = false;
                        frmlayout1.FindItemOrGroupByName("stepseq").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtPlanQty").ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("txtActualQty").ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("txtYear").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtDayNo").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtBlastMachine").Visible = false;

                        frmlayout1.FindItemOrGroupByName("txtQAVValBy").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtMonitoredBy").Visible = false;
                       




                     
                 
                        break;


                       


                    case "Blast":
                        Session["StepC"] = "Blast Freezing";
                        FormTitle.Text = "WIP IN (Blast)";
                       
                        txtStepCode.ClientEnabled = false;

                        //Weighing
                        frmlayout1.FindItemOrGroupByName("NumStrands").Visible = false;
                        frmlayout1.FindItemOrGroupByName("glStuffingMach").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtWeightSmokecart").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtWeightbefore").Visible = false;
                        frmlayout1.FindItemOrGroupByName("cbQCStick").Visible = false;
                        frmlayout1.FindItemOrGroupByName("cbQCHotdog").Visible = false;
                        frmlayout1.FindItemOrGroupByName("cbQCfreefrom").Visible = false;


                        //Spiral
                        frmlayout1.FindItemOrGroupByName("glSpiralMach").Visible = false;
                        frmlayout1.FindItemOrGroupByName("QtyPacks").Visible = false;
                        frmlayout1.FindItemOrGroupByName("QtyLoosePacks").Visible = false;
                        frmlayout1.FindItemOrGroupByName("TimeStarted").Visible = false;
                        frmlayout1.FindItemOrGroupByName("TimeFinished").Visible = false;
                        frmlayout1.FindItemOrGroupByName("IntTempPL").Visible = false;
                        frmlayout1.FindItemOrGroupByName("StdRoomTemp").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtQAVSpiral").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtQAVPLoad").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtQAVValBy").Visible = false;
                       
                        frmlayout1.FindItemOrGroupByName("txtStepCode").Visible = false;
                        frmlayout1.FindItemOrGroupByName("stepseq").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtPlanQty").ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("txtActualQty").ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("txtYear").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtDayNo").Visible = false;


                        frmlayout1.FindItemOrGroupByName("txtCheckedBy").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtMachSpeed").Visible = false;
                        frmlayout1.FindItemOrGroupByName("HeadQuality").Visible = false;
                        frmlayout1.FindItemOrGroupByName("QAVal").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtDayNo").Visible = false;
                        frmlayout1.FindItemOrGroupByName("txtDayNo").Visible = false;
                        
                        break;


                }


                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
             
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                
                //txtWorkCenter.Text = _Entity.WorkCenter.ToString();

             
                
                txtDayNo.Text = _Entity.DayNo.ToString();
                txtYear.Text = _Entity.Year.ToString();
                txtWorkWeek.Text = _Entity.WorkWeek.ToString();
                txtPlanQty.Text = _Entity.PlanQty.ToString();
                txtActualQty.Text = _Entity.ActualQty.ToString();
                string site = "MLI";
                //Gridlookup_InitValue(sdsSite, hprodsite, _Entity.ProdSite.ToString());
                hprodsite.Text = site ;

                //Gridlookup_InitValue(sdsStep, txtStepCode, _Entity.Step.ToString());
                txtStepCode.Text = _Entity.Step.ToString();
                
                hstepseq.Text = _Entity.StepSeq.ToString();


                NumStrands.Text = _Entity.NumStrands.ToString();
                glStuffingMach.Text = _Entity.glStuffingMach.ToString();
                txtWeightSmokecart.Text = _Entity.txtWeightSmokecart.ToString();
                txtWeightbefore.Text = _Entity.txtWeightbefore.ToString();

                
                if (_Entity.cbQCStick == true)
                {
                    cbQCStick.Checked = _Entity.cbQCStick;

                }
                else
                {
                    cbQCStickNo.Checked = true;
                }
              

                if (_Entity.cbQCHotdog == true)
                {
                    cbQCHotdog.Checked = _Entity.cbQCHotdog;

                }
                else
                {
                    cbQCHotdogNo.Checked = true;
                }





          
                if (_Entity.cbQCfreefrom == true)
                {
                    cbQCfreefrom.Checked = _Entity.cbQCfreefrom;

                }
                else
                {
                    cbQCfreefromNo.Checked = true;
                }

                Shift.Value = _Entity.Shift.ToString();

                //TimeOn.Text = Convert.ToDateTime(_Entity.TimeOn.ToString()).ToShortTimeString();

                string TimeO = _Entity.TimeOn.ToString();
                if (TimeO == "" || TimeO == null)
                {
                    TimeOn.Text = _Entity.TimeOn.ToString();

                }
                else
                {
                    TimeOn.Text = Convert.ToDateTime(_Entity.TimeOn.ToString()).ToShortTimeString();
                }

                //TimeOff.Text = Convert.ToDateTime(_Entity.TimeOff.ToString()).ToShortTimeString();

                string TimeOf = _Entity.TimeOff.ToString();
                if (TimeOf == "" || TimeOf == null)
                {
                    TimeOff.Text = _Entity.TimeOff.ToString();

                }
                else
                {
                    TimeOff.Text = Convert.ToDateTime(_Entity.TimeOff.ToString()).ToShortTimeString();
                }

                txtBlastTemp.Text = _Entity.txtBlastTemp.ToString();
                txtProductName.Text = _Entity.txtProductName.ToString();
                //txtPD.Text = _Entity.txtPD.ToString();
                //txtPD.Text = Convert.ToDateTime(_Entity.txtPD.ToString()).ToShortDateString();

                string DatePD = _Entity.txtPD.ToString();
                if (DatePD == "" || DatePD == null)
                {
                    txtPD.Text = _Entity.txtPD.ToString();

                }
                else
                {
                    txtPD.Text = Convert.ToDateTime(_Entity.txtPD.ToString()).ToShortDateString();
                }

                NumPacks.Value = _Entity.NumPacks;
                txtLoadedBy.Text = _Entity.txtLoadedBy.ToString();
                txtMonitoredBy.Text = _Entity.txtMonitoredBy.ToString();
                txtCheckedBy.Text = _Entity.txtCheckedBy.ToString();
                txtMachSpeed.Text = _Entity.txtMachSpeed.ToString();




                glSpiralMach.Text = _Entity.glSpiralMach.ToString();
                txtBlastMachine.Text = _Entity.txtBlastMachine.ToString();
                QtyPacks.Value = _Entity.QtyPacks;
                QtyLoosePacks.Value = _Entity.QtyLoosePacks;

                //TimeStarted.Text = Convert.ToDateTime(_Entity.TimeStarted.ToString()).ToShortTimeString();

                string TimeS = _Entity.TimeStarted.ToString();
                if (TimeS == "" || TimeS == null)
                {
                    TimeStarted.Text = _Entity.TimeStarted.ToString();

                }
                else
                {

                    TimeStarted.Text = Convert.ToDateTime(_Entity.TimeStarted.ToString()).ToShortTimeString();
                }

                //TimeFinished.Text = Convert.ToDateTime(_Entity.TimeFinished.ToString()).ToShortTimeString();

                string TimeF = _Entity.TimeFinished.ToString();
                if (TimeF == "" || TimeF == null)
                {
                    TimeFinished.Text = _Entity.TimeFinished.ToString();

                }
                else
                {
                    TimeFinished.Text = Convert.ToDateTime(_Entity.TimeFinished.ToString()).ToShortTimeString();
                }

                IntTempPL.Value = _Entity.IntTempPL;
                StdRoomTemp.Value = _Entity.StdRoomTemp;
                txtQAVSpiral.Text = _Entity.txtQAVSpiral.ToString();
                txtQAVPLoad.Text = _Entity.txtQAVPLoad.ToString();
                txtQAVValBy.Text = _Entity.txtQAVValBy.ToString();





                //Gridlookup_InitValue(sdsBatch, txtBatchNo, _Entity.BatchNo.ToString());
                txtBatchNo.Text = _Entity.BatchNo.ToString();

                txtRemarks.Text = _Entity.Remarks;
                MemoRemarks.Text = _Entity.MemoRemarks;
                
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
                if (Request.QueryString["entry"].ToString() == "N")
                {
                
                    popup.ShowOnPageLoad = false;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

            
                
                }
                else
                {


                    //gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;
                }
  
                
                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Production.WIPIN where docnumber = '" + txtDocNumber.Text + "' and ISNULL(JobOrder,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }


              

            }



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
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            //gparam._TransType = "PRDWIN";
            gparam._TransType = HttpContext.Current.Request.QueryString["transtype"];
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProduction.GProduction.WIPIN_Validate(gparam);
 
            if (strresult.Length > 8)
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n" ;//Message variable to client side
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;

     
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
                //gljoborder.ClientEnabled = false;
                //gljoborder.DropDownButton.Enabled = false;
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
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New)
                e.Visible = false;
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
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
            //if (IsCallback && (Request.Params["__CALLBACKID"].Contains("txtBatchNo") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            //emclookup(2021)
            if (IsCallback && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {

                string strUseSqlCmd = Gridlookup_SQL(gridLookup, " ", "GET");
                if (strUseSqlCmd != "")
                {
                    Gridlookup_SQL(gridLookup, strUseSqlCmd, "SET");
                }


            }

        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string column = e.Parameters.Split('|')[0];//Set column name
            //if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            //string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            //string val = e.Parameters.Split('|')[2];//Set column value
            //if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            //ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
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

            //DataTable Batchcode = Gears.RetriveData2("select * from Production.WipIN where txtpd = '" + txtPD.Text + "'  and SKUcode  = '" + txtskucode.Text + "' and BatchNo = '" + txtBatchNo.Text + "'", Session["ConnString"].ToString());
            //if (Batchcode.Rows.Count > 0)
            //{
            //    cp.JSProperties["cp_message"] = "BatchCode:'" + txtBatchNo.Text + "' already Exist!";
            //    cp.JSProperties["cp_success"] = true;
            //    return;
            //}
                _Entity.DocNumber = txtDocNumber.Value.ToString();
                _Entity.DocDate = dtpDocDate.Text;
                _Entity.Type = "WIP IN";
                _Entity.Remarks = txtRemarks.Text;
                _Entity.MemoRemarks = MemoRemarks.Text;
            

                _Entity.SKUcode = txtskucode.Text;
                //_Entity.DayNo = Convert.ToInt32(txtDayNo.Text);
                //_Entity.Year = Convert.ToInt32(txtYear.Text);
                _Entity.WorkWeek = Convert.ToInt32(txtWorkWeek.Text);
                //_Entity.BatchNo = Convert.ToDecimal(Convert.IsDBNull(txtBatchNo.Text) ? 0 : Convert.ToDecimal(txtBatchNo.Text));]
                _Entity.BatchNo = txtBatchNo.Text;
                _Entity.Step = Session["StepC"].ToString();
                _Entity.ProdSite = hprodsite.Text;
                _Entity.StepSeq = hstepseq.Text;

                _Entity.PlanQty = Convert.ToDecimal(Convert.IsDBNull(txtPlanQty.Text) ? 0 : Convert.ToDecimal(txtPlanQty.Text));
                _Entity.ActualQty = Convert.ToDecimal(Convert.IsDBNull(txtActualQty.Text) ? 0 : Convert.ToDecimal(txtActualQty.Text));

                _Entity.NumStrands = Convert.ToDecimal(Convert.IsDBNull(NumStrands.Value) ? 0 : Convert.ToDecimal(NumStrands.Value));
                //_Entity.glStuffingMach = glStuffingMach.Text;
                _Entity.glStuffingMach = Convert.ToDecimal(Convert.IsDBNull(glStuffingMach.Value) ? 0 : Convert.ToDecimal(glStuffingMach.Value));
                _Entity.txtWeightSmokecart = txtWeightSmokecart.Text;
                _Entity.txtWeightbefore = txtWeightbefore.Text;
                _Entity.cbQCStick = cbQCStick.Checked;
                _Entity.cbQCHotdog = cbQCHotdog.Checked;
                _Entity.cbQCfreefrom = cbQCfreefrom.Checked;
                _Entity.Shift = Shift.Text;

                _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
                _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
                _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
                _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
                _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
                _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
                _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
                _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
                _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;


                _Entity.TimeOn = TimeOn.Text;
                _Entity.TimeOff = TimeOff.Text;
                _Entity.txtBlastTemp = txtBlastTemp.Text;
                _Entity.txtProductName = txtProductName.Text;
                _Entity.txtPD = txtPD.Text;
                _Entity.NumPacks = Convert.ToDecimal(Convert.IsDBNull(NumPacks.Value) ? 0 : Convert.ToDecimal(NumPacks.Value));
                _Entity.txtLoadedBy = txtLoadedBy.Text;
                _Entity.txtMonitoredBy = txtMonitoredBy.Text;
                _Entity.txtCheckedBy = txtCheckedBy.Text;
                _Entity.txtMachSpeed = txtMachSpeed.Text;
            
                //_Entity.glSpiralMach = glSpiralMach.Text;
                _Entity.glSpiralMach = Convert.ToDecimal(Convert.IsDBNull(glSpiralMach.Value) ? 0 : Convert.ToDecimal(glSpiralMach.Value));
                _Entity.txtBlastMachine = Convert.ToDecimal(Convert.IsDBNull(txtBlastMachine.Value) ? 0 : Convert.ToDecimal(txtBlastMachine.Value));
                _Entity.QtyPacks = Convert.ToDecimal(Convert.IsDBNull(QtyPacks.Value) ? 0 : Convert.ToDecimal(QtyPacks.Value));
                _Entity.QtyLoosePacks = Convert.ToDecimal(Convert.IsDBNull(QtyLoosePacks.Value) ? 0 : Convert.ToDecimal(QtyLoosePacks.Value));
                _Entity.TimeStarted = TimeStarted.Text;
                _Entity.TimeFinished = TimeFinished.Text;
                _Entity.IntTempPL = Convert.ToDecimal(Convert.IsDBNull(IntTempPL.Value) ? 0 : Convert.ToDecimal(IntTempPL.Value));
                _Entity.StdRoomTemp = Convert.ToDecimal(Convert.IsDBNull(StdRoomTemp.Value) ? 0 : Convert.ToDecimal(StdRoomTemp.Value));
                _Entity.txtQAVSpiral = txtQAVSpiral.Text;
                _Entity.txtQAVPLoad = txtQAVPLoad.Text;
                _Entity.txtQAVValBy = txtQAVValBy.Text;



           
              //  _Entity.Transtype = Request.QueryString["transtype"].ToString();

                string param = e.Parameter.Split('|')[0]; //Renats
                switch (param) //Renats
                {

                    case "Add":

                        if (error == false)
                        {
                            check = true;

                            _Entity.UpdateData(_Entity);//Method of inserting for header

                            // _Entity.SubsiEntry(txtDocNumber.Text);
                            Validate();
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
             
                 
                    if (error == false)
                    {
                        _Entity.LastEditedBy = Session["userid"].ToString();

                        _Entity.LastEditedDate = DateTime.Now.ToString();
                        check = true;
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.WIPIN", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                        _Entity.UpdateData(_Entity);//Method of inserting for header
               
                       // _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
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

                case "JO":
                    a = e.Parameter.Split('|')[1]; //Renats
                    b = e.Parameter.Split('|')[2]; //Renats
                    c = e.Parameter.Split('|')[3]; //Renats
                

                    break;

               
            

                case "customercode":
                //    sdsPicklist.SelectParameters["customercode"].DefaultValue = aglCustomer.Text;
                    break;
                
                case "docdate":

                    DateTime dt2 = Convert.ToDateTime(dtpDocDate.Text);
                    txtWorkWeek.Text = GetWorkWeek(dt2).ToString();
                    txtDayNo.Text = GetDayofWeek(dt2).ToString();
                    txtYear.Text = dt2.Year.ToString();

                    //Session["batchdate"] = dtpDocDate.Text;
                    //emc2021
                    //sdsBatch.SelectCommand = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                    //string strSql3 = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                    //Gridlookup_SQL(txtBatchNo, strSql3, "SET");

                    //txtBatchNo.Text = "";
                    break;

                case "sds":
                    string sdsID = e.Parameter.Split('|')[1]; //SDS object ID
                    string sdsSqlCommand = e.Parameter.Split('|')[2]; //SQLcommand
                    string glID = e.Parameter.Split('|')[3]; //GridLookup Obj



                    foreach (Control ca in this.Controls)
                    {
                        if (ca is SqlDataSource)
                        {

                            if (((SqlDataSource)ca).ID == sdsID)
                            {
                                //2021-07-22    EMC
                                //Special Case if yun SQLcommand may parameter na need i pasok base sa object ng FrontEnd
                                // i overwrite and variable na sdsSqlCommand

                                //if (sdsID == "sdsBatch")
                                //{
                                //    sdsSqlCommand = "SELECT BatchNo,SKUcode FROM Production.BatchQueue WHERE CONVERT(DATE,FIELD9) = CONVERT(DATE, '" + dtpDocDate.Text + "')";
                                //}


                                ((SqlDataSource)ca).SelectCommand = sdsSqlCommand;
                                Session["sds" + sdsID] = sdsSqlCommand;

                            }
                        }


                    }




                    break;


            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            //if (error == false && check == false)
            //{
            //    foreach (GridViewColumn column in gv1.Columns)
            //    {
            //        //GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //        //if (dataColumn == null) continue;
            //        //if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber")
            //        //{
            //        //    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //        //}
            //    }

            //    //Checking for non existing Codes..
            //    ItemCode = e.NewValues["ItemCode"].ToString();
            //    ColorCode = e.NewValues["ColorCode"].ToString();
            //    ClassCode = e.NewValues["ClassCode"].ToString();
            //    SizeCode = e.NewValues["SizeCode"].ToString();
            //    DataTable item = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'");
            //    if (item.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //    }
            //    DataTable color = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
            //    if (color.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //    }
            //    DataTable clss = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
            //    if (clss.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //    }
            //    DataTable size = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
            //    if (size.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
            //    }

            //    if (e.Errors.Count > 0)
            //    {
            //        error = true; //bool to cancel adding/updating if true
            //    }
            //}
        }
        //dictionary method to hold error 


     



      
    

      
        private void OtherDetail()
        {
            string pick = "";// gljoborder.Text;


            DataTable ret = Gears.RetriveData2("SELECT  WorkCenter FROM Procurement.SOWorkOrder WHERE DocNumber = '" + pick + "'", Session["ConnString"].ToString());

            DataRow _ret = ret.Rows[0];

            //if (String.IsNullOrEmpty(txtWorkCenter.Text))
            //{
            //    _Entity.WorkCenter = _ret["WorkCenter"].ToString();
            //   // txtWorkCenter.Text = _ret["WorkCenter"].ToString();
            //}
      

         
        }
   



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
                if (a is SqlDataSource)
                {

                    ((SqlDataSource)a).ConnectionString = Session["ConnString"].ToString();
                }


            }

   

        }



    }
        #endregion
}
