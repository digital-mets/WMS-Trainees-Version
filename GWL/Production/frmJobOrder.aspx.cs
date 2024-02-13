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
using DevExpress.Data.Filtering;
using GearsProduction;

namespace GWL
{
    public partial class frmJobOrder : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string Cascade = "";
        string TransNo = "";

        Entity.JobOrder _Entity = new Entity.JobOrder();//Calls entity odsHeader
        Entity.JobOrder.JOProductOrder _JOProductOrder = new Entity.JobOrder.JOProductOrder();
        Entity.JobOrder.JOBillOfMaterial _JOBillOfMaterial = new Entity.JobOrder.JOBillOfMaterial();
        Entity.JobOrder.JOStep _JOStep = new Entity.JobOrder.JOStep();
        Entity.JobOrder.JOStepPlanning _JOStepPlanning = new Entity.JobOrder.JOStepPlanning();
        Entity.JobOrder.JOClassBreakdown _JOClassBreakdown = new Entity.JobOrder.JOClassBreakdown();
        Entity.JobOrder.JOSizeBreakdown _JOSizeBreakdown = new Entity.JobOrder.JOSizeBreakdown();
        Entity.JobOrder.JOMaterialMovement _JOMaterialMovement = new Entity.JobOrder.JOMaterialMovement();

        #region page load/entry

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Random rand = new Random();
                hidcon["key"] = rand.Next().ToString();
            }
            
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            Session["callbackJO"] = Request.Params["__CALLBACKID"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            string referer;
            //try
            //{
            //    referer = Request.ServerVariables["http_referer"];
            //}
            //catch
            //{
            //    referer = "";
            //}

            //if (referer == null)
            //{
            //    Response.Redirect("~/error.aspx");
            //}

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            txtDocNumber.ReadOnly = true;
            gvRef.Columns["CommandString"].Width = 0;
            gvRef.Columns["RCommandString"].Width = 0;

            if (!IsPostBack)
            {
                Session["JODatatable"] = null;
                Session["JOFilterExpression"] = null;
                Session["JOchecker"] = null;
                Session["BOMFilterExpression"] = null;
                Session["BOMchecker"] = null;
                Session["JOSPDatatable"] = null; 
                Session["JOSPDetail"] = null;
                Session["BOMDatatable"] = null;
                Session["BOMDetail"] = null;
                Session["JOPISNumber"] = null;
                Session["JOStyle"] = null;
                Session["JOProductOrderFilter"] = null;
                Session["JOAlloc"] = null;
                Session["JOStepCascade"] = null;
                Session["JOStyle"] = null;
                Session["itemJOsql" + hidcon["key"]] = null;
                Session["JOProd"] = null;

                Session["SOitem"] = null;
                Session["SOclr"] = null;
                Session["SOcls"] = null;
                Session["SOqty"] = null;
                Session["SOprice"] = null;
                Session["SOunit"] = null;
                Session["SOdesc"] = null;
                Session["SObulk"] = null;
                Session["SOisbulk"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                dtpDueDate.Text = String.IsNullOrEmpty(_Entity.DueDate) ? null : Convert.ToDateTime(_Entity.DueDate.ToString()).ToShortDateString();
                DataTable leadtime = Gears.RetriveData2("SELECT ISNULL(Value,0) AS Value FROM IT.SystemSettings WHERE Code = 'JOCUTOFF'", Session["ConnString"].ToString());
                if (_Entity.Leadtime.ToString() == "-100")
                {
                    speLeadtime.Value = Convert.ToInt32(leadtime.Rows[0]["Value"].ToString());
                }
                else
                {
                    speLeadtime.Value = _Entity.Leadtime.ToString();
                }
                dtpProdDate.Text = String.IsNullOrEmpty(_Entity.ProdDate) ? null : Convert.ToDateTime(_Entity.ProdDate.ToString()).ToShortDateString();
                aglCustomerCode.Value = _Entity.CustomerCode;
                switch (_Entity.Status.ToString().Trim())
                {
                    case "N":
                        txtStatus.Text = "NEW";
                        break;
                    case "W":
                        txtStatus.Text = "WORK IN PROGRESS";
                        break;
                    case "C":
                        txtStatus.Text = "CLOSED";
                        break;
                    case "X":
                        txtStatus.Text = "MANUAL CLOSED";
                        break;
                    case "L":
                        txtStatus.Text = "CANCELLED";
                        break;
                    case null:
                    case "":
                        txtStatus.Text = "NEW";
                        break;
                }
                txtDateCompleted.Value = _Entity.DateCompleted.ToString();
                aglDIS.Value = _Entity.DISNumber.ToString();
                aglDesigner.Value = _Entity.Designer.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                aglParentStep.Value = _Entity.ParentStepcode.ToString();
                txtOriginalJO.Value = _Entity.OriginalJO.ToString();
                txtCustBrand.Value = _Entity.CustomerBrand.ToString();
                chkIsMultiIn.Value = _Entity.IsMultiIn;
                chkIsAutoJO.Value = _Entity.IsAutoJO;
                dtpSODueDate.Text = String.IsNullOrEmpty(_Entity.SODueDate.ToString()) ? null : Convert.ToDateTime(_Entity.SODueDate.ToString()).ToShortDateString();

                //Quantity
                speTotalJO.Value = _Entity.TotalJOQty.ToString();
                speTotalSO.Value = _Entity.TotalSOQty.ToString();
                speTotalIN.Value = _Entity.TotalINQty.ToString();
                speTotalFinal.Value = _Entity.TotalFinalQty.ToString();

                //Costing
                speSRP.Value = _Entity.SRP.ToString();
                aglCurrency.Value = _Entity.Currency.ToString();
                speTDirectLabor.Value = _Entity.TotalDirectLabor.ToString();
                speTDirectMat.Value = _Entity.TotalDirecMat.ToString();
                speTotalOH.Value = _Entity.TotalOverhead.ToString();
                speOHAdj.Value = _Entity.OverheadAdj.ToString();
                speUnitCost.Value = _Entity.UnitCost.ToString();
                speEstAccCost.Text = _Entity.EstAccCost.ToString();
                speEstUnitCost.Value = _Entity.EstUnitCost.ToString();
                speStdOHCost.Text = _Entity.StdOHCost.ToString();

                //BOM
                aglPISNumber.Value = _Entity.PISNumber.ToString();
                Session["JOPISNumber"] = _Entity.PISNumber.ToString();

                //Step Planning
                aglStepTemplate.Value = _Entity.StepTemplateNo.ToString();
                Session["JOStyle"] = _Entity.StepTemplateNo.ToString();

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
                txtHAllocSubmittedBy.Text = _Entity.AllocSubmittedBy;
                txtHAllocSubmittedDate.Text = _Entity.AllocSubmittedDate;
                txtHProdSubmittedBy.Text = _Entity.ProdSubmittedBy;
                txtHProdSubmittedDate.Text = _Entity.ProdSubmittedDate;
                txtHApprovedBy.Text = _Entity.ApprovedLeadBy;
                txtHApprovedDate.Text = _Entity.ApprovedLeadDate;
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtManualClosedBy.Text = _Entity.ManualClosedBy;
                txtManualClosedDate.Text = _Entity.ManualClosedDate;

                if (!String.IsNullOrEmpty(_Entity.LastEditedBy.ToString())){
                    popup.ShowOnPageLoad = true;
                }

                DataTable bookdate = new DataTable();
                bookdate = Gears.RetriveData2("DECLARE @CYear int = (SELECT Value FROM IT.SystemSettings WHERE Code = 'CYEAR') "
                    + " DECLARE @CMonth int = (SELECT Value FROM IT.SystemSettings WHERE Code = 'CMONTH') "
                    + " DECLARE @BookDate Date = CONVERT(DATE,CONVERT(varchar(4),@CYear)+'-'+CONVERT(varchar(2),@CMonth)+'-01')"
                    + " SELECT @BookDate AS BookDate", Session["ConnString"].ToString());

                dtpBookdate.Text = String.IsNullOrEmpty(bookdate.Rows[0]["BookDate"].ToString()) ? null : Convert.ToDateTime(bookdate.Rows[0]["BookDate"].ToString()).ToShortDateString();

                if (!String.IsNullOrEmpty(txtHAllocSubmittedBy.Text))
                {
                    Session["JOAlloc"] = "1";
                }

                if (!String.IsNullOrEmpty(txtHProdSubmittedBy.Text))
                {
                    Session["JOProd"] = "1";
                }

                gvProductOrder.KeyFieldName = "LineNumber";
                gvBOM.KeyFieldName = "LineNumber";
                gvSteps.KeyFieldName = "LineNumber";
                gvStepPlanning.KeyFieldName = "LineNumber";
                gvClass.KeyFieldName = "LineNumber";
                gvSize.KeyFieldName = "LineNumber";
                gvMaterial.KeyFieldName = "LineNumber";


                //Size Horizonhal
                gvSizeHorizontal.DataSourceID = "sdsSizeHorizontal";
                gvSizes.DataSourceID = "sdsSizes";
                gvSizeHorizontal.DataBind();
                gvSizes.DataBind();

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                        }
                        dtpDocDate.Date = DateTime.Now;
                        dtpDueDate.Date = DateTime.Now;
                        dtpProdDate.Date = DateTime.Now;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                InitControls();

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gvProductOrder.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvBOM.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvSteps.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvStepPlanning.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvClass.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvSize.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvMaterial.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }

                DataTable ProductOrder = new DataTable();
                DataTable BOM = new DataTable();
                DataTable Steps = new DataTable();
                DataTable StepPlanning = new DataTable();
                DataTable Class = new DataTable();
                DataTable Size = new DataTable();
                DataTable Material = new DataTable();

                ProductOrder = Gears.RetriveData2("SELECT top 1 DocNumber FROM Production.JOProductOrder WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gvProductOrder.DataSourceID = (ProductOrder.Rows.Count > 0 ? "odsProductOrder" : "sdsProductOrder");

                BOM = Gears.RetriveData2("SELECT top 1 DocNumber FROM Production.JOBillOfMaterial WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gvBOM.DataSourceID = (BOM.Rows.Count > 0 ? "odsBOM" : "sdsBOM");
                if (gvBOM.DataSource != null)
                {
                    gvBOM.DataSource = null;
                }

                Steps = Gears.RetriveData2("SELECT top 1 DocNumber FROM Production.JOStep WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gvSteps.DataSourceID = (Steps.Rows.Count > 0 ? "odsSteps" : "sdsJOSteps");

                StepPlanning = Gears.RetriveData2("SELECT top 1 DocNumber FROM Production.JOStepPlanning WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gvStepPlanning.DataSourceID = (StepPlanning.Rows.Count > 0 ? "odsStepPlanning" : "sdsStepPlanning");
                if (gvStepPlanning.DataSource != null)
                {
                    gvStepPlanning.DataSource = null;
                }

                Class = Gears.RetriveData2("SELECT top 1 DocNumber FROM Production.JOClassBreakdown WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gvClass.DataSourceID = (Class.Rows.Count > 0 ? "odsClass" : "sdsJOClass");

                Size = Gears.RetriveData2("SELECT top 1 DocNumber FROM Production.JOSizeBreakdown WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gvSize.DataSourceID = (Size.Rows.Count > 0 ? "odsSize" : "sdsJOSize");

                Material = Gears.RetriveData2("SELECT top 1 DocNumber FROM Production.JOMaterialMovement WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gvMaterial.DataSourceID = (Material.Rows.Count > 0 ? "odsMaterial" : "sdsMaterial");

                gvBOM.EnableCallbackAnimation = false;
                gvSteps.EnableCallbackAnimation = false;
                gvStepPlanning.EnableCallbackAnimation = false;
                gvClass.EnableCallbackAnimation = false;
                gvSize.EnableCallbackAnimation = false;
                gvMaterial.EnableCallbackAnimation = false;
                gvRef.EnableCallbackAnimation = false;
                gvProductOrder.EnableCallbackAnimation = false;
            }

            
            //gvJournal.DataSourceID = "odsJournalEntry";

            //GenerateBtn();
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDJOB";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProduction.GProduction.JobOrder_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] += strresult;//Message variable to client side
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "ACTCOL";
            //gparam._Table = "Accounting.Collection";
            //gparam._Factor = -1;
            //gparam._Connection = Session["ConnString"].ToString();
            //string strresult = GearsAccounting.GAccounting.Collection_Post(gparam);
            //if (strresult != " ")
            //{
            //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            //}
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(ASPxEdit sender)
        {
            ASPxTextBox text = sender as ASPxTextBox;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    text.ReadOnly = view;
                }
                else
                {
                    text.ReadOnly = !view;
                }
            }
            else
            {
                text.ReadOnly = true;
            }
        }
        protected void SRP_Load(ASPxEdit sender)
        {
            ASPxSpinEdit spe = sender as ASPxSpinEdit;
            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if ((!String.IsNullOrEmpty(txtOriginalJO.Text) && !String.IsNullOrEmpty(txtCustBrand.Text))
                    || (Session["JOAlloc"] != null) || chkIsAutoJO.Checked)
                {
                    spe.ReadOnly = true;
                }
                else
                {
                    spe.ReadOnly = view;
                }
            }
            else
            {
                spe.ReadOnly = true;
            }
        }


        protected void LookupLoad3(ASPxEdit sender)
        {
            ASPxGridLookup look = sender as ASPxGridLookup;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOProd"] != null)
                {
                    look.DropDownButton.Enabled = false;
                    look.ReadOnly = true;
                }
                else
                {
                    look.DropDownButton.Enabled = true;
                    look.ReadOnly = false;
                }
            }
            else
            {
                look.DropDownButton.Enabled = false;
                look.ReadOnly = true;
            }
        }
        protected void LookupLoad(ASPxEdit sender)
        {
            ASPxGridLookup look = sender as ASPxGridLookup;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    look.DropDownButton.Enabled = false;
                    look.ReadOnly = true;
                }
                else
                {
                    look.DropDownButton.Enabled = true;
                    look.ReadOnly = false;
                }
            }
            else
            {
                look.DropDownButton.Enabled = false;
                look.ReadOnly = true;
            }
        }
        protected void LookupLoad2(ASPxEdit sender)
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void Customer_LookupLoad(ASPxEdit sender)
        {
            ASPxGridLookup look = sender as ASPxGridLookup;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    look.DropDownButton.Enabled = false;
                    look.ReadOnly = true;
                }
                else
                {
                    if (chkIsAutoJO.Checked == false)
                    {
                        look.DropDownButton.Enabled = true;
                        look.ReadOnly = false;
                    }
                    else
                    {
                        look.DropDownButton.Enabled = false;
                        look.ReadOnly = true;
                    }
                }
            }
            else
            {
                look.DropDownButton.Enabled = false;
                look.ReadOnly = true;
            }
        }
        protected void CheckBoxLoad(ASPxEdit sender)
        {
            var check = sender as ASPxCheckBox;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    check.ReadOnly = true;
                }
                else
                {
                    check.ReadOnly = false;
                }
            }
            else
            {
                check.ReadOnly = true;
            }
        }
        protected void ComboBoxLoad(ASPxEdit sender)
        {
            var combo = sender as ASPxComboBox;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    combo.ReadOnly = true;
                }
                else
                {
                    combo.ReadOnly = false;
                }
            }
            else
            {
                combo.ReadOnly = true;
            }
        }
        protected void Date_Load(ASPxEdit sender)
        {
            ASPxDateEdit date = sender as ASPxDateEdit;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    date.DropDownButton.Enabled = false;
                    date.ReadOnly = true;
                }
                else
                {
                    date.DropDownButton.Enabled = true;
                    date.ReadOnly = false;
                }
            }
            else
            {
                date.DropDownButton.Enabled = false;
                date.ReadOnly = true;
            }
        }
        protected void SODueDate_Load(ASPxEdit sender)
        {
            ASPxDateEdit date = sender as ASPxDateEdit;

            // GC Removed code based on Ate Nes concern to set So Due Date as not editable regardless if manual/auto jo
            // 7/14/2016

            //if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            //{
            //if (Session["JOAlloc"] != null)
            //{
            date.DropDownButton.Enabled = false;
            date.ReadOnly = true;
            //}
            //else
            //{
            //    if (chkIsAutoJO.Checked == false)
            //    {
            //        date.DropDownButton.Enabled = true;
            //        date.ReadOnly = false;
            //    }
            //    else
            //    {
            //        date.DropDownButton.Enabled = false;
            //        date.ReadOnly = true;
            //    }
            //}
            //}
            //else
            //{
            //    date.DropDownButton.Enabled = false;
            //    date.ReadOnly = true;
            //}
        }
        protected void SpinEdit_Load(ASPxEdit sender)
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    spinedit.ReadOnly = true;
                }
                else
                {
                    spinedit.ReadOnly = false;
                }
            }
            else
            {
                spinedit.ReadOnly = true;
            }
        }
        protected void ButtonLoad_3(ASPxButton sender)
        {
            ASPxButton button = sender as ASPxButton;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOProd"] != null)
                {
                    button.ClientEnabled = false;
                }
                else
                {
                    button.ClientEnabled = true;
                }
            }
            else
            {
                button.ClientEnabled = false;
            }
        }
        protected void ButtonLoad(ASPxButton sender)
        {
            ASPxButton button = sender as ASPxButton;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    button.ClientEnabled = false;
                }
                else
                {
                    button.ClientEnabled = true;
                }
            }
            else
            {
                button.ClientEnabled = false;
            }
        }
        protected void ButtonLoad_2(ASPxButton sender)
        {
            ASPxButton button = sender as ASPxButton;
            button.ClientEnabled = !view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            //if (e.ButtonType == ColumnCommandButtonType.Update)
            //    e.Visible = false;
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit ||
                        e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                }

                if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        if (Session["JOAlloc"] != null)
                        {
                            e.Visible = false;
                        }
                        else
                        {
                            e.Visible = true;
                        }
                    }
                }

                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }
                }
            }
        }
        protected void gv_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "N")
                {
                    if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }

                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonID == "Delete" || e.ButtonID == "ProductDelete" || e.ButtonID == "BOMDelete" || e.ButtonID == "StepsDelete"
                         || e.ButtonID == "PlanDelete" || e.ButtonID == "ClassDelete" || e.ButtonID == "SizeDelete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
                else
                {
                    if (e.ButtonID == "Delete" || e.ButtonID == "ProductDelete" || e.ButtonID == "BOMDelete" || e.ButtonID == "StepsDelete"
                            || e.ButtonID == "ClassDelete" || e.ButtonID == "SizeDelete")
                    {
                        if (Session["JOAlloc"] != null)
                        {
                            e.Visible = DevExpress.Utils.DefaultBoolean.False;
                        }
                        else
                        {
                            e.Visible = DevExpress.Utils.DefaultBoolean.True;
                        }
                    }
                    else if (e.ButtonID == "PlanDelete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
            }
        }
        protected void gv_CommandButtonInitialize_2(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            //if (e.ButtonType == ColumnCommandButtonType.Delete)
            //{
            //    e.Image.IconID = "actions_cancel_16x16";
            //}
            //if (e.ButtonType == ColumnCommandButtonType.New)
            //{
            //    e.Image.IconID = "actions_addfile_16x16";

            //}
            //if (e.ButtonType == ColumnCommandButtonType.Edit)
            //{
            //    e.Image.IconID = "actions_addfile_16x16";
            //}
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;

                if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit ||
                        e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                }

                if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }
                }

                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }
                }
            }
        }
        protected void gv_CustomButtonInitialize_2(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "N")
                {
                    if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }

                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonID == "Delete" || e.ButtonID == "ProductDelete" || e.ButtonID == "BOMDelete" || e.ButtonID == "StepsDelete"
                         || e.ButtonID == "PlanDelete" || e.ButtonID == "ClassDelete" || e.ButtonID == "SizeDelete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
        }
        protected void MemoLoad(ASPxEdit sender)//Control for all textbox
        {
            ASPxMemo memo = sender as ASPxMemo;

            if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            {
                if (Session["JOAlloc"] != null)
                {
                    memo.ReadOnly = true;
                }
                else
                {
                    memo.ReadOnly = false;
                }
            }
            else
            {
                memo.ReadOnly = true;
            }
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Request.Params["__CALLBACKID"] == null) return;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gvProductOrder") && !Request.Params["__CALLBACKID"].Contains("glItemCode") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                if (Session["JOFilterExpression"] != null)
                {
                    if (Session["JOchecker"] == "Color")
                    {
                        gridLookup.GridView.DataSourceID = "sdsColor";
                        sdsColor.SelectCommand = Session["JOFilterExpression"].ToString();
                    }
                    else if (Session["JOchecker"] == "Class")
                    {
                        gridLookup.GridView.DataSourceID = "sdsClass";
                        sdsClass.SelectCommand = Session["JOFilterExpression"].ToString();
                    }
                    else if (Session["JOchecker"] == "Size")
                    {
                        gridLookup.GridView.DataSourceID = "sdsSize";
                        sdsSize.SelectCommand = Session["JOFilterExpression"].ToString();
                    }
                    else
                    {
                        gridLookup.GridView.DataSourceID = "sdsItemDetail";
                        sdsItemDetail.SelectCommand = "SELECT B.ItemCode, ColorCode, ClassCode,SizeCode,UnitBase AS Unit,FullDesc, UnitBulk AS BulkUnit FROM Masterfile.[Item] A INNER JOIN Masterfile.[ItemDetail] B ON A.ItemCode = B.ItemCode "
                            + " INNER JOIN Masterfile.ItemCategory C ON A.ItemCategoryCode = C.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(C.IsAsset,0) = 0";
                        sdsItemDetail.FilterExpression = Session["JOFilterExpression"].ToString();
                    }
                }
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;
            var itemlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("ItemCode"))
            {
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable countitem = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode, A.FullDesc FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          " WHERE A.ItemCode = '" + itemcode +
                                                          "' AND (ISNULL(A.IsInActive,0) = 0 OR ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                DataTable itemdesc = Gears.RetriveData2("SELECT DISTINCT FullDesc FROM Masterfile.Item WHERE ItemCode = '" + itemcode +
                                                          "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntsze = countsize.Rows.Count;

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntclr > 1 && cntcls > 1 && cntsze == 1)
                    {
                        codes = ";;";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";

                    }
                    else if (cntclr > 1 && cntcls == 1 && cntsze > 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntclr == 1 && cntcls > 1 && cntsze > 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";

                    }
                    else
                    {
                        codes = ";;;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                }

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("ColorCode"))//genrev
            {
                sdsColor.SelectCommand = string.Format("SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0)=0 and ItemCode = '{0}'", itemcode);
                Session["JOFilterExpression"] = sdsColor.SelectCommand;
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;
                //CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                //sdsColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["JOchecker"] = "Color";
                //Session["JOFilterExpression"] = sdsColor.FilterExpression;
                grid.DataSourceID = "sdsColor";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, column) != null)
                        if (grid.GetRowValues(i, column).ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, column).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("ClassCode"))
            {
                sdsClass.SelectCommand = string.Format("SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0 and ItemCode = '{0}'", itemcode);
                Session["JOFilterExpression"] = sdsClass.SelectCommand;
                ASPxGridView grid1 = sender as ASPxGridView;
                var selectedValues1 = itemcode;
                //CriteriaOperator selectionCriteria1 = new InOperator("ItemCode", new string[] { itemcode });
                //sdsClass.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1)).ToString();
                Session["JOchecker"] = "Class";
                //Session["JOFilterExpression"] = sdsClass.FilterExpression;
                grid1.DataSourceID = "sdsClass";
                grid1.DataBind();

                for (int i = 0; i < grid1.VisibleRowCount; i++)
                {
                    if (grid1.GetRowValues(i, column) != null)
                        if (grid1.GetRowValues(i, column).ToString() == val)
                        {
                            grid1.Selection.SelectRow(i);
                            string key = grid1.GetRowValues(i, column).ToString();
                            grid1.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("SizeCode"))
            {
                sdsSize.SelectCommand = string.Format("SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0 and ItemCode = '{0}'", itemcode);
                Session["JOFilterExpression"] = sdsSize.SelectCommand;
                ASPxGridView grid2 = sender as ASPxGridView;
                var selectedValues2 = itemcode;
                ASPxGridLookup lookup = (ASPxGridLookup)gvProductOrder.FindEditRowCellTemplateControl((GridViewDataTextColumn)gvProductOrder.Columns[8], "glSizeCode");
                //CriteriaOperator selectionCriteria2 = new InOperator("ItemCode", new string[] { itemcode });
                //sdsSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria2)).ToString();
                Session["JOchecker"] = "Size";
                //Session["JOFilterExpression"] = sdsSize.FilterExpression;
                grid2.DataSourceID = "sdsSize";
                grid2.DataBind();

                if (val != "")
                for (int i = 0; i < grid2.VisibleRowCount; i++)
                {
                    if (grid2.GetRowValues(i, column) != null)
                        if (grid2.GetRowValues(i, column).ToString() == val)
                        {
                            grid2.Selection.SelectRow(i);
                            string key = grid2.GetRowValues(i, column).ToString();
                            grid2.MakeRowVisible(key);
                            break;
                        }
                }

                if (val == "")
                {
                    lookup.Value = null;
                }
            }
            if (e.Parameters.Contains("BOMItemCode"))
            {
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable countitem = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode, A.FullDesc, A.UnitBase FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode +
                                                          "' AND (ISNULL(A.IsInActive,0) = 0 OR ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                DataTable itemdesc = Gears.RetriveData2("SELECT DISTINCT FullDesc, UnitBase FROM Masterfile.Item WHERE ItemCode = '" + itemcode +
                                                          "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntsze = countsize.Rows.Count;

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                    }
                    else
                    {
                        codes = ";;;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                    }
                }

                Session["JOchecker"] = "BOMItem";
                itemlookup.JSProperties["cp_BOMcodes"] = codes;
            }
            else if (e.Parameters.Contains("BOMColorCode"))
            {
                sdsColor.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0)=0";
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                sdsColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["JOchecker"] = "BOMColor";
                Session["JOFilterExpression"] = sdsColor.FilterExpression;
                grid.DataSourceID = "sdsColor";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, column) != null)
                        if (grid.GetRowValues(i, column).ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, column).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("BOMClassCode"))
            {
                sdsClass.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0";
                ASPxGridView grid1 = sender as ASPxGridView;
                var selectedValues1 = itemcode;
                CriteriaOperator selectionCriteria1 = new InOperator("ItemCode", new string[] { itemcode });
                sdsClass.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1)).ToString();
                Session["JOchecker"] = "BOMClass";
                Session["JOFilterExpression"] = sdsClass.FilterExpression;
                grid1.DataSourceID = "sdsClass";
                grid1.DataBind();

                for (int i = 0; i < grid1.VisibleRowCount; i++)
                {
                    if (grid1.GetRowValues(i, column) != null)
                        if (grid1.GetRowValues(i, column).ToString() == val)
                        {
                            grid1.Selection.SelectRow(i);
                            string key = grid1.GetRowValues(i, column).ToString();
                            grid1.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("BOMSizeCode"))
            {
                sdsSize.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0";
                ASPxGridView grid2 = sender as ASPxGridView;
                var selectedValues2 = itemcode;
                CriteriaOperator selectionCriteria2 = new InOperator("ItemCode", new string[] { itemcode });
                sdsSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria2)).ToString();
                Session["JOchecker"] = "BOMSize";
                Session["JOFilterExpression"] = sdsSize.FilterExpression;
                grid2.DataSourceID = "sdsSize";
                grid2.DataBind();

                for (int i = 0; i < grid2.VisibleRowCount; i++)
                {
                    if (grid2.GetRowValues(i, column) != null)
                        if (grid2.GetRowValues(i, column).ToString() == val)
                        {
                            grid2.Selection.SelectRow(i);
                            string key = grid2.GetRowValues(i, column).ToString();
                            grid2.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }
        protected void BOM_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(BOM_CustomCallback);
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvBOM") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                if (Session["BOMFilterExpression"] != null)
                {
                    if (Session["BOMchecker"] == "BOMColor" && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "sdsBOMColor";
                        sdsBOMColor.SelectCommand = Session["BOMFilterExpression"].ToString();
                        //sdsBOMColor.FilterExpression = Session["BOMFilterExpression"].ToString();
                    }
                    else if (Session["BOMchecker"] == "BOMClass" && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "sdsBOMClass";
                        sdsBOMClass.SelectCommand = Session["BOMFilterExpression"].ToString();
                    }
                    else if (Session["BOMchecker"] == "BOMSize" && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "sdsBOMSize";
                        sdsBOMSize.SelectCommand = Session["BOMFilterExpression"].ToString();
                    }
                    else
                    {
                        gridLookup.GridView.DataSourceID = "sdsBOMItemDetail";
                        sdsBOMItemDetail.SelectCommand = "SELECT B.ItemCode, ColorCode, ClassCode, SizeCode,UnitBase AS Unit, FullDesc, UnitBulk AS BulkUnit FROM Masterfile.[Item] A INNER JOIN Masterfile.[ItemDetail] B ON A.ItemCode = B.ItemCode "
                                    + " INNER JOIN Masterfile.ItemCategory C ON A.ItemCategoryCode = C.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(C.IsAsset,0) = 0";
                        sdsBOMItemDetail.FilterExpression = Session["BOMFilterExpression"].ToString();
                    }
                }
                if (Session["BOMchecker"] == "BOMItem" && Request.Params["__CALLBACKID"].Contains(gridLookup.ID) && Request.Params["__CALLBACKID"].Contains("ItemCode"))
                {
                    gridLookup.GridView.DataSourceID = "sdsBOMItem";
                    sdsBOMItem.SelectCommand = Session["sqljo"].ToString();
                    //gridLookup.DataBind();
                }
            }
        }
        public void BOM_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;
            var itemlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("ItemCode"))
            {
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable countitem = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode, A.FullDesc, A.UnitBase, CONVERT(decimal(15,2),ISNULL(C.AllowancePerc,0)) AS Allowance, ISNULL(A.EstimatedCost,0) AS EstimatedCost FROM Masterfile.Item A " +
                "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode LEFT JOIN Masterfile.ProductCategorySub C ON A.ProductSubCatCode = C.ProductSubCatCode WHERE A.ItemCode = '" + itemcode +
                "' AND (ISNULL(A.IsInActive,0) = 0 OR ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                DataTable itemdesc = Gears.RetriveData2("SELECT DISTINCT FullDesc, UnitBase, CONVERT(decimal(15,2),ISNULL(C.AllowancePerc,0)) AS Allowance, ISNULL(A.EstimatedCost,0) AS EstimatedCost FROM Masterfile.Item A LEFT JOIN Masterfile.ProductCategorySub C ON A.ProductSubCatCode = C.ProductSubCatCode  WHERE ItemCode = '" + itemcode +
                                                          "' AND ISNULL(A.IsInActive,0) = 0", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntsze = countsize.Rows.Count;

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["Allowance"].ToString() + ";";
                        codes += dt["EstimatedCost"].ToString() + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntclr > 1 && cntcls > 1 && cntsze == 1)
                    {
                        codes = ";;";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";

                    }
                    else if (cntclr > 1 && cntcls == 1 && cntsze > 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntclr == 1 && cntcls > 1 && cntsze > 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";

                    }
                    else
                    {
                        codes = ";;;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                }

                Session["JOchecker"] = "BOMItem";
                itemlookup.JSProperties["cp_BOMcodes"] = codes;
            }
            else if (e.Parameters.Contains("glbomItemLook"))
            {
                Session["BOMchecker"] = "BOMItem";
                sdsBOMItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] A INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsAsset,0) = 0";
                Session["sqljo"] = sdsBOMItem.SelectCommand;
                ASPxGridView grid = sender as ASPxGridView;
                grid.DataSourceID = "sdsBOMItem";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "ItemCode") != null)
                        if (grid.GetRowValues(i, "ItemCode").ToString() == itemcode)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "ItemCode").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("ColorCode"))
            {
                sdsBOMColor.SelectCommand = string.Format("SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0)=0 and ItemCode = '{0}'", itemcode);
                Session["BOMFilterExpression"] = sdsBOMColor.SelectCommand;
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;
                //CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                //sdsBOMColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["BOMchecker"] = "BOMColor";
                //Session["BOMFilterExpression"] = sdsBOMColor.FilterExpression;
                grid.DataSourceID = "sdsBOMColor";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, column) != null)
                        if (grid.GetRowValues(i, column).ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, column).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("ClassCode"))
            {
                sdsBOMClass.SelectCommand = string.Format("SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0 and ItemCode = '{0}'", itemcode);
                Session["BOMFilterExpression"] = sdsBOMClass.SelectCommand;
                ASPxGridView grid1 = sender as ASPxGridView;
                var selectedValues1 = itemcode;
                //CriteriaOperator selectionCriteria1 = new InOperator("ItemCode", new string[] { itemcode });
                //sdsBOMClass.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1)).ToString();
                Session["BOMchecker"] = "BOMClass";
                //Session["BOMFilterExpression"] = sdsBOMClass.FilterExpression;
                grid1.DataSourceID = "sdsBOMClass";
                grid1.DataBind();

                for (int i = 0; i < grid1.VisibleRowCount; i++)
                {
                    if (grid1.GetRowValues(i, column) != null)
                        if (grid1.GetRowValues(i, column).ToString() == val)
                        {
                            grid1.Selection.SelectRow(i);
                            string key = grid1.GetRowValues(i, column).ToString();
                            grid1.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("SizeCode"))
            {
                sdsBOMSize.SelectCommand = string.Format("SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0 and ItemCode = '{0}'", itemcode);
                Session["BOMFilterExpression"] = sdsBOMSize.SelectCommand;
                ASPxGridView grid2 = sender as ASPxGridView;
                var selectedValues2 = itemcode;
                //CriteriaOperator selectionCriteria2 = new InOperator("ItemCode", new string[] { itemcode });
                //sdsBOMSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria2)).ToString();
                Session["BOMchecker"] = "BOMSize";
                //Session["BOMFilterExpression"] = sdsBOMSize.FilterExpression;
                grid2.DataSourceID = "sdsBOMSize";
                grid2.DataBind();

                for (int i = 0; i < grid2.VisibleRowCount; i++)
                {
                    if (grid2.GetRowValues(i, column) != null)
                        if (grid2.GetRowValues(i, column).ToString() == val)
                        {
                            grid2.Selection.SelectRow(i);
                            string key = grid2.GetRowValues(i, column).ToString();
                            grid2.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.DueDate = dtpDueDate.Text;
            _Entity.Leadtime = String.IsNullOrEmpty(speLeadtime.Text) ? 0 : Convert.ToInt32(speLeadtime.Value.ToString());
            _Entity.ProdDate = dtpProdDate.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Text;
            if (txtStatus.Text.Trim() == "NEW" || String.IsNullOrEmpty(txtStatus.Text))
            {
                _Entity.Status = "N";
            }
            else if (txtStatus.Text.Trim() == "WORK IN PROGRESS")
            {
                _Entity.Status = "W";
            }
            else if (txtStatus.Text.Trim() == "CLOSED")
            {
                _Entity.Status = "C";
            }
            else if (txtStatus.Text.Trim() == "MANUAL CLOSED")
            {
                _Entity.Status = "X";
            }
            _Entity.DISNumber = String.IsNullOrEmpty(aglDIS.Text) ? null : aglDIS.Value.ToString();
            _Entity.Designer = String.IsNullOrEmpty(aglDesigner.Text) ? null : aglDesigner.Value.ToString();
            _Entity.Remarks = memRemarks.Text;
            _Entity.ParentStepcode = aglParentStep.Text;
            _Entity.OriginalJO = txtOriginalJO.Text;
            _Entity.CustomerBrand = txtCustBrand.Text;
            _Entity.IsMultiIn = Convert.ToBoolean(chkIsMultiIn.Value.ToString());
            _Entity.IsAutoJO = Convert.ToBoolean(chkIsAutoJO.Value.ToString());
            _Entity.SODueDate = String.IsNullOrEmpty(dtpSODueDate.Text) ? null : dtpSODueDate.Text;

            //Quantity
            _Entity.TotalJOQty = String.IsNullOrEmpty(speTotalJO.Text) ? 0 : Convert.ToDecimal(speTotalJO.Value.ToString());
            _Entity.TotalSOQty = String.IsNullOrEmpty(speTotalSO.Text) ? 0 : Convert.ToDecimal(speTotalSO.Value.ToString());
            _Entity.TotalINQty = String.IsNullOrEmpty(speTotalIN.Text) ? 0 : Convert.ToDecimal(speTotalIN.Value.ToString());
            _Entity.TotalFinalQty = String.IsNullOrEmpty(speTotalFinal.Text) ? 0 : Convert.ToDecimal(speTotalFinal.Value.ToString());

            //Costing
            _Entity.SRP = String.IsNullOrEmpty(speSRP.Text) ? 0 : Convert.ToDecimal(speSRP.Value.ToString());
            _Entity.Currency = String.IsNullOrEmpty(aglCurrency.Text) ? null : aglCurrency.Value.ToString();
            _Entity.TotalDirectLabor = String.IsNullOrEmpty(speTDirectLabor.Text) ? 0 : Convert.ToDecimal(speTDirectLabor.Value.ToString());
            _Entity.TotalDirecMat = String.IsNullOrEmpty(speTDirectMat.Text) ? 0 : Convert.ToDecimal(speTDirectMat.Value.ToString());
            _Entity.TotalOverhead = String.IsNullOrEmpty(speTotalOH.Text) ? 0 : Convert.ToDecimal(speTotalOH.Value.ToString());
            _Entity.OverheadAdj = String.IsNullOrEmpty(speOHAdj.Text) ? 0 : Convert.ToDecimal(speOHAdj.Value.ToString());
            _Entity.UnitCost = String.IsNullOrEmpty(speUnitCost.Text) ? 0 : Convert.ToDecimal(speUnitCost.Value.ToString());
            _Entity.EstAccCost = String.IsNullOrEmpty(speEstAccCost.Text) ? 0 : Convert.ToDecimal(speEstAccCost.Value.ToString());
            _Entity.EstUnitCost = String.IsNullOrEmpty(speEstUnitCost.Text) ? 0 : Convert.ToDecimal(speEstUnitCost.Value.ToString());
            _Entity.StdOHCost = String.IsNullOrEmpty(speStdOHCost.Text) ? 0 : Convert.ToDecimal(speStdOHCost.Value.ToString());

            //BOM
            _Entity.PISNumber = String.IsNullOrEmpty(aglPISNumber.Text) ? null : aglPISNumber.Value.ToString();

            //Step Planning
            _Entity.StepTemplateNo = String.IsNullOrEmpty(aglStepTemplate.Text) ? null : aglStepTemplate.Value.ToString();

            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
            //Session["userid"] = "1828";
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();


            cp.JSProperties["cp_valmsg"] = "";
            //if (!chkIsAutoJO.Checked)
            //{
            //    string parent = "";
            //    DataTable parentstepcheck = Gears.RetriveData2(" select 'Note: No parent step code indicated.' AS Note from Production.JobOrder where DocNumber = '" + txtDocNumber.Text + "' and ISNULL(ParentStepcode,'') = ''", Session["ConnString"].ToString());
            //    foreach (DataRow dt in parentstepcheck.Rows)
            //    {
            //        parent += dt["Note"].ToString();
            //    }
            //    if (!String.IsNullOrEmpty(parent))
            //        cp.JSProperties["cp_valmsg"] += parent; 
            //}

            DataTable StepPlanning = new DataTable();

            foreach (GridViewColumn col in gvStepPlanning.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                StepPlanning.Columns.Add(dataColumn.FieldName);
            }

            for (int i = 0; i < gvStepPlanning.VisibleRowCount; i++)
            {
                DataRow row = StepPlanning.Rows.Add();
                foreach (DataColumn col in StepPlanning.Columns)
                    row[col.ColumnName] = gvStepPlanning.GetRowValues(i, col.ColumnName);
            }

            DataTable PISDetail = new DataTable();

            foreach (GridViewColumn col in gvBOM.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                PISDetail.Columns.Add(dataColumn.FieldName);
            }

            for (int b = 0; b < gvBOM.VisibleRowCount; b++)
            {
                DataRow row = PISDetail.Rows.Add();
                foreach (DataColumn col in PISDetail.Columns)
                    row[col.ColumnName] = gvBOM.GetRowValues(b, col.ColumnName);
            }

            string param = e.Parameter.Split('|')[0];
            switch (param)
            {
                //Size Horizontal
                case "CallbackSize":

                    cp.JSProperties["cp_closeSH"] = true;

                    Session["SOitem"] = null;
                    Session["SOclr"] = null;
                    Session["SOcls"] = null;
                    Session["SOqty"] = null;
                    Session["SOprice"] = null;
                    Session["SOunit"] = null;
                    Session["SOdesc"] = null;
                    Session["SObulk"] = null;
                    Session["SOisbulk"] = null;
                    break;

                case "CallbackSizeHorizontal":

                    Session["SOitem"] = e.Parameter.Split('|')[1];
                    Session["SOclr"] = e.Parameter.Split('|')[2];
                    Session["SOcls"] = e.Parameter.Split('|')[3];
                    Session["SOqty"] = e.Parameter.Split('|')[4];
                    Session["SOprice"] = e.Parameter.Split('|')[5];
                    Session["SOunit"] = e.Parameter.Split('|')[6];
                    Session["SOdesc"] = e.Parameter.Split('|')[7];
                    Session["SObulk"] = e.Parameter.Split('|')[8];
                    Session["SOisbulk"] = e.Parameter.Split('|')[9];

                    sdsSizeHorizontal.ConnectionString = Session["ConnString"].ToString();
                    sdsSizeHorizontal.SelectCommand = "SELECT CONVERT(varchar(MAX),'" + txtDocNumber.Text + "') AS DocNumberX, '" + Session["SOitem"].ToString() + "' AS ItemCodeX, '" + Session["SOclr"].ToString() + "' AS ColorCodeX, '" + Session["SOcls"].ToString() + "' AS ClassCodeX, "
                        + " CONVERT(varchar(MAX),'" + Session["SOunit"].ToString() + "') AS UnitX, CONVERT(decimal(15,2), " + Session["SOqty"].ToString() + ") AS OrderQtyX, CONVERT(decimal(15,2)," + Session["SOprice"].ToString() + ") AS UnitPriceX, CONVERT(varchar(MAX),'" + Session["SOdesc"] + "') AS FullDescX, "
                        + " CONVERT(varchar(MAX),'" + Session["SObulk"].ToString() + "') AS BulkUnitX, CONVERT(bit,'" + Session["SOisbulk"].ToString() + "') AS IsByBulkX";

                    gvSizeHorizontal.DataSourceID = "sdsSizeHorizontal";
                    if (gvSizeHorizontal.DataSource != null)
                    {
                        gvSizeHorizontal.DataSource = null;
                    }
                    gvSizeHorizontal.DataBind();


                    sdsSizes.ConnectionString = Session["ConnString"].ToString();
                    gvSizes.DataSourceID = "sdsSizes";
                    if (gvSizes.DataSource != null)
                    {
                        gvSizes.DataSource = null;
                    }
                    gvSizes.DataBind();

                    gvSizes.AutoGenerateColumns = false;

                    DataTable getSizeCode = Gears.RetriveData2("SELECT A.SizeCode FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE ISNULL(A.IsInactive,0) = 0 AND ItemCode = '" + Session["SOitem"].ToString() +
                        "' AND ColorCode = '" + Session["SOclr"].ToString() + "' AND ClassCode = '" + Session["SOcls"].ToString() + "' ORDER BY SizeType, SortOrder", Session["ConnString"].ToString());


                    string query = "";

                    int x = 1;

                    foreach (DataRow dtsize in getSizeCode.Rows)
                    {
                        GridViewDataSpinEditColumn spin = new GridViewDataSpinEditColumn();
                        spin.FieldName = dtsize[0].ToString();
                        spin.VisibleIndex = x;
                        spin.Width = 50;
                        spin.ReadOnly = false;
                        spin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                        spin.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                        spin.PropertiesSpinEdit.DisplayFormatString = "{0:N}";
                        spin.PropertiesSpinEdit.ClientSideEvents.ValueChanged = "CalculateSize";
                        query += ", CONVERT(decimal(15,2),0) AS [" + dtsize[0].ToString() + "]";
                        gvSizes.Columns.Add(spin);
                        x++;
                    }
                    gvSizes.CancelEdit();

                    sdsSizes.SelectCommand = "SELECT CONVERT(varchar(MAX),'" + txtDocNumber.Text + "') AS DocNumberX" + query;
                    sdsSizes.DataBind();
                    gvSizes.DataSourceID = "sdsSizes";
                    if (gvSizes.DataSource != null)
                    {
                        gvSizes.DataSource = null;
                    }
                    gvSizes.DataBind();

                    cp.JSProperties["cp_RefreshSizeGrid"] = true;

                    break;
            }

            switch (e.Parameter)
            {
                case "Add":

                    //if (Session["BOMDatatable"] != "1")
                    //{
                    //    gvBOM.UpdateEdit();
                    //}

                    //if (Session["JOSPDatatable"] != "1")
                    //{
                    //    gvStepPlanning.UpdateEdit();
                    //}

                    gvProductOrder.UpdateEdit();
                    //gvBOM.UpdateEdit();
                    gvSteps.UpdateEdit();
                    //gvStepPlanning.UpdateEdit();
                    gvClass.UpdateEdit();
                    //gvSize.UpdateEdit();
                    gvMaterial.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber, "Production.JobOrder", 2, Session["ConnString"].ToString());
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);

                        gvProductOrder.DataSourceID = "odsProductOrder";
                        odsProductOrder.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvProductOrder.UpdateEdit();

                        if (Session["BOMDatatable"] == "1")
                        {
                            _Entity.DeleteBOM(txtDocNumber.Text, Session["ConnString"].ToString());
                            gvBOM.DataSource = PISDetail;
                            if (gvBOM.DataSourceID != null)
                            {
                                gvBOM.DataSourceID = null;
                            }
                            gvBOM.DataBind();
                            gvBOM.UpdateEdit();
                        }
                        else
                        {
                            gvBOM.DataSourceID = "odsBOM";
                            odsBOM.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gvBOM.UpdateEdit();
                        }

                        if (Session["JOSPDatatable"] == "1")
                        {
                            _Entity.DeleteStepPlanning(txtDocNumber.Text, Session["ConnString"].ToString());
                            gvStepPlanning.DataSource = StepPlanning;
                            if (gvStepPlanning.DataSourceID != null)
                            {
                                gvStepPlanning.DataSourceID = null;
                            }
                            gvStepPlanning.DataBind();
                            gvStepPlanning.UpdateEdit();
                        }
                        else
                        {
                            gvStepPlanning.DataSourceID = "odsStepPlanning";
                            odsStepPlanning.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gvStepPlanning.UpdateEdit();
                        } 

                        gvSteps.DataSourceID = "odsSteps";
                        odsSteps.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvSteps.UpdateEdit();

                        gvClass.DataSourceID = "odsClass";
                        odsClass.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvClass.UpdateEdit();

                        gvSize.DataSourceID = "odsSize";
                        odsSize.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        //gvSize.UpdateEdit();

                        _Entity.JODetails(txtDocNumber.Text, Session["ConnString"].ToString());
                        Gears.RetriveData2("exec [sp_Recalculate] '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        Validate();
                        _Entity.ArrangeLine(txtDocNumber.Text, Session["ConnString"].ToString());

                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Update":

                    //if (Session["BOMDatatable"] != "1")
                    //{
                    //    gvBOM.UpdateEdit();
                    //}

                    //if (Session["JOSPDatatable"] != "1")
                    //{
                    //    gvStepPlanning.UpdateEdit();
                    //}

                    //gvProductOrder.UpdateEdit();
                    ////gvBOM.UpdateEdit();
                    //gvSteps.UpdateEdit();
                    ////gvStepPlanning.UpdateEdit();
                    //gvClass.UpdateEdit();
                    //gvSize.UpdateEdit();
                    //gvMaterial.UpdateEdit();

                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Production.JobOrder", 2, Session["ConnString"].ToString());
                    if (!string.IsNullOrEmpty(strError1))
                    {
                        cp.JSProperties["cp_message"] = strError1;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    //if (error == false)
                    //{
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);

                        gvProductOrder.DataSourceID = "odsProductOrder";
                        odsProductOrder.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvProductOrder.UpdateEdit();

                        if (Session["BOMDatatable"] == "1")
                        {
                            _Entity.DeleteBOM(txtDocNumber.Text, Session["ConnString"].ToString());
                            gvBOM.DataSource = PISDetail;
                            if (gvBOM.DataSourceID != null)
                            {
                                gvBOM.DataSourceID = null;
                            }
                            gvBOM.DataBind();
                            gvBOM.UpdateEdit();
                        }
                        else
                        {
                            gvBOM.DataSourceID = "odsBOM";
                            odsBOM.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gvBOM.UpdateEdit();
                        }

                        gvSteps.DataSourceID = "odsSteps";
                        odsSteps.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvSteps.UpdateEdit();

                        if (Session["JOSPDatatable"] == "1")
                        {
                            _Entity.DeleteStepPlanning(txtDocNumber.Text, Session["ConnString"].ToString());
                            gvStepPlanning.DataSource = StepPlanning;
                            if (gvStepPlanning.DataSourceID != null)
                            {
                                gvStepPlanning.DataSourceID = null;
                            }
                            gvStepPlanning.DataBind();
                            gvStepPlanning.UpdateEdit();
                        }
                        else
                        {
                            gvStepPlanning.DataSourceID = "odsStepPlanning";
                            odsStepPlanning.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gvStepPlanning.UpdateEdit();
                        }

                        gvClass.DataSourceID = "odsClass";
                        odsClass.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvClass.UpdateEdit();

                        gvSize.DataSourceID = "odsSize";
                        odsSize.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        //gvSize.UpdateEdit();

                        _Entity.JODetails(txtDocNumber.Text, Session["ConnString"].ToString());
                        Gears.RetriveData2("exec [sp_Recalculate] '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        Validate();
                        _Entity.ArrangeLine(txtDocNumber.Text, Session["ConnString"].ToString());
                        

                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";

                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

                    break;

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Close":

                    cp.JSProperties["cp_close"] = true;
                    gvProductOrder.DataSource = null;
                    gvBOM.DataSource = null;
                    gvSteps.DataSource = null;
                    gvStepPlanning.DataSource = null;
                    gvClass.DataSource = null;
                    gvSize.DataSource = null;
                    gvMaterial.DataSource = null;


                    break;

                case "CallbackCustomer":

                    DataTable getCustomerdata = Gears.RetriveData2("SELECT DISTINCT ISNULL(Currency,'PHP') AS Currency FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());
                    aglCurrency.Value = getCustomerdata.Rows[0]["Currency"].ToString();

                    cp.JSProperties["cp_generated"] = true;

                    break; 
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gvProductOrder_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        protected void gvBOM_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["BOMDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = CascadePIS();

                int Line = source.Rows.Count + 1;

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["DocNumber"] = values.NewValues["DocNumber"];
                    row["Components"] = values.NewValues["Components"];
                    row["StepCode"] = values.NewValues["StepCode"];
                    row["ProductCode"] = values.NewValues["ProductCode"];
                    row["ProductColor"] = values.NewValues["ProductColor"];
                    row["ProductSize"] = values.NewValues["ProductSize"];
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["PerPieceConsumption"] = values.NewValues["PerPieceConsumption"];
                    row["Consumption"] = values.NewValues["Consumption"];
                    row["AllowancePerc"] = values.NewValues["AllowancePerc"];
                    row["AllowanceQty"] = values.NewValues["AllowanceQty"];
                    row["RequiredQty"] = values.NewValues["RequiredQty"];
                    row["UnitCost"] = values.NewValues["UnitCost"];
                    row["IsMajorMaterial"] = values.NewValues["IsMajorMaterial"];
                    row["IsBulk"] = values.NewValues["IsBulk"];
                    row["IsRounded"] = values.NewValues["IsRounded"];
                    row["IsExcluded"] = values.NewValues["IsExcluded"];
                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                    row["Version"] = values.NewValues["Version"];
                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var DocNumber = txtDocNumber.Text;
                    var LineNumber = values.NewValues["LineNumber"] == null ? Convert.ToString(Line).PadLeft(Convert.ToString(Line).Length + (5 - Convert.ToString(Line).Length), '0') : values.NewValues["LineNumber"];
                    //var LineNumber = values.NewValues["LineNumber"];
                    var Components = values.NewValues["Components"];
                    var StepCode = values.NewValues["StepCode"];
                    var ProductCode = values.NewValues["ProductCode"];
                    var ProductColor = values.NewValues["ProductColor"];
                    var ProductSize = values.NewValues["ProductSize"];
                    var ItemCode = values.NewValues["ItemCode"];
                    var FullDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var Unit = values.NewValues["Unit"];
                    var PerPieceConsumption = values.NewValues["PerPieceConsumption"];
                    var Consumption = values.NewValues["Consumption"];
                    var AllowancePerc = values.NewValues["AllowancePerc"];
                    var AllowanceQty = values.NewValues["AllowanceQty"];
                    var RequiredQty = values.NewValues["RequiredQty"];
                    var UnitCost = values.NewValues["UnitCost"];
                    var IsMajorMaterial = values.NewValues["IsMajorMaterial"];
                    var IsBulk = values.NewValues["IsBulk"];
                    var IsRounded = values.NewValues["IsRounded"];
                    var IsExcluded = values.NewValues["IsExcluded"];
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    var Version = values.NewValues["Version"];

                    source.Rows.Add(DocNumber, LineNumber, Components, StepCode, ProductCode, ProductColor, ProductSize, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, Unit,
                    PerPieceConsumption, Consumption, AllowancePerc, AllowanceQty, RequiredQty, UnitCost, IsMajorMaterial, IsBulk, IsRounded, IsExcluded, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Version);

                    Line = Line + 1;
                }

                foreach (DataRow dtRow in source.Rows)
                {
                    _JOBillOfMaterial.DocNumber = dtRow["DocNumber"].ToString();
                    _JOBillOfMaterial.Components = dtRow["Components"].ToString();
                    _JOBillOfMaterial.StepCode = dtRow["StepCode"].ToString();
                    _JOBillOfMaterial.ProductCode = dtRow["ProductCode"].ToString();
                    _JOBillOfMaterial.ProductColor = dtRow["ProductColor"].ToString();
                    _JOBillOfMaterial.ProductSize = dtRow["ProductSize"].ToString();
                    _JOBillOfMaterial.ItemCode = dtRow["ItemCode"].ToString();
                    _JOBillOfMaterial.FullDesc = dtRow["FullDesc"].ToString();
                    _JOBillOfMaterial.ColorCode = dtRow["ColorCode"].ToString();
                    _JOBillOfMaterial.ClassCode = dtRow["ClassCode"].ToString();
                    _JOBillOfMaterial.SizeCode = dtRow["SizeCode"].ToString();
                    _JOBillOfMaterial.Unit = dtRow["Unit"].ToString();
                    _JOBillOfMaterial.PerPieceConsumption = Convert.ToDecimal(Convert.IsDBNull(dtRow["PerPieceConsumption"]) ? 0 : dtRow["PerPieceConsumption"]);
                    _JOBillOfMaterial.Consumption = Convert.ToDecimal(Convert.IsDBNull(dtRow["Consumption"]) ? 0 : dtRow["Consumption"]);
                    _JOBillOfMaterial.AllowancePerc = Convert.ToDecimal(Convert.IsDBNull(dtRow["AllowancePerc"]) ? 0 : dtRow["AllowancePerc"]);
                    _JOBillOfMaterial.AllowanceQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["AllowanceQty"]) ? 0 : dtRow["AllowanceQty"]);
                    _JOBillOfMaterial.RequiredQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["RequiredQty"]) ? 0 : dtRow["RequiredQty"]);
                    _JOBillOfMaterial.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                    _JOBillOfMaterial.IsMajorMaterial = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsMajorMaterial"].ToString()) ? false : dtRow["IsMajorMaterial"]);
                    _JOBillOfMaterial.IsBulk = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsBulk"].ToString()) ? false : dtRow["IsBulk"]);
                    _JOBillOfMaterial.IsRounded = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsRounded"].ToString()) ? false : dtRow["IsRounded"]);
                    _JOBillOfMaterial.IsExcluded = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsExcluded"].ToString()) ? false : dtRow["IsExcluded"]);
                    _JOBillOfMaterial.Field1 = dtRow["Field1"].ToString();
                    _JOBillOfMaterial.Field2 = dtRow["Field2"].ToString();
                    _JOBillOfMaterial.Field3 = dtRow["Field3"].ToString();
                    _JOBillOfMaterial.Field4 = dtRow["Field4"].ToString();
                    _JOBillOfMaterial.Field5 = dtRow["Field5"].ToString();
                    _JOBillOfMaterial.Field6 = dtRow["Field6"].ToString();
                    _JOBillOfMaterial.Field7 = dtRow["Field7"].ToString();
                    _JOBillOfMaterial.Field8 = dtRow["Field8"].ToString();
                    _JOBillOfMaterial.Field9 = dtRow["Field9"].ToString();
                    _JOBillOfMaterial.Version = dtRow["Version"].ToString();
                    _JOBillOfMaterial.AddJOBillOfMaterial(_JOBillOfMaterial);
                }
            }
        }
        protected void gvSteps_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        protected void gvStepPlanning_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["JOSPDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = CascadeStepTemplate();

                int Line1 = source.Rows.Count + 1;


                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["DocNumber"] = values.NewValues["DocNumber"];
                    row["Sequence"] = values.NewValues["Sequence"];
                    row["PreProd"] = values.NewValues["PreProd"];
                    row["StepCode"] = values.NewValues["StepCode"];
                    row["WorkCenter"] = values.NewValues["WorkCenter"];
                    row["Overhead"] = values.NewValues["Overhead"];
                    row["IsInhouse"] = values.NewValues["IsInhouse"];
                    row["InQty"] = values.NewValues["InQty"];
                    row["OutQty"] = values.NewValues["OutQty"];
                    row["AdjQty"] = values.NewValues["AdjQty"];
                    row["Allowance"] = values.NewValues["Allowance"];
                    row["Yield"] = values.NewValues["Yield"];
                    row["ActualLoss"] = values.NewValues["ActualLoss"];
                    row["ActualYield"] = values.NewValues["ActualYield"];
                    row["Instruction"] = values.NewValues["Instruction"];
                    row["EstWorkOrderPrice"] = values.NewValues["EstWorkOrderPrice"];
                    row["WorkOrderPrice"] = values.NewValues["WorkOrderPrice"];
                    row["WorkOrderDate"] = values.NewValues["WorkOrderDate"];
                    row["WorkOrderQty"] = values.NewValues["WorkOrderQty"];
                    row["DateCommitted"] = values.NewValues["DateCommitted"];
                    row["VAT"] = values.NewValues["VAT"];
                    row["VATRate"] = values.NewValues["VATRate"];
                    row["TargetDateIn"] = values.NewValues["TargetDateIn"];
                    row["TargetDateOut"] = values.NewValues["TargetDateOut"];
                    row["ActualDateIn"] = values.NewValues["ActualDateIn"];
                    row["ActualDateOut"] = values.NewValues["ActualDateOut"];
                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                    row["Version"] = values.NewValues["Version"];

                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var DocNumber = txtDocNumber.Text;
                    var LineNumber = values.NewValues["LineNumber"] == null ? Convert.ToString(Line1).PadLeft(Convert.ToString(Line1).Length + (5 - Convert.ToString(Line1).Length), '0') : values.NewValues["LineNumber"];
                    //var LineNumber = values.NewValues["LineNumber"];
                    var Sequence = values.NewValues["Sequence"];
                    var StepCode = values.NewValues["StepCode"];
                    var WorkCenter = values.NewValues["WorkCenter"];
                    var Overhead = values.NewValues["Overhead"];
                    var PreProd = values.NewValues["PreProd"];
                    var IsInhouse = values.NewValues["IsInhouse"];
                    var InQty = values.NewValues["InQty"];
                    var OutQty = values.NewValues["OutQty"];
                    var AdjQty = values.NewValues["AdjQty"];
                    var Allowance = values.NewValues["Allowance"];
                    var Yield = values.NewValues["Yield"];
                    var ActualLoss = values.NewValues["ActualLoss"];
                    var ActualYield = values.NewValues["ActualYield"];
                    var Instruction = values.NewValues["Instruction"];
                    var EstWorkOrderPrice = values.NewValues["EstWorkOrderPrice"];
                    var WorkOrderPrice = values.NewValues["WorkOrderPrice"];
                    var WorkOrderDate = values.NewValues["WorkOrderDate"];
                    var WorkOrderQty = values.NewValues["WorkOrderQty"];
                    var DateCommitted = values.NewValues["DateCommitted"];
                    var VAT = values.NewValues["VAT"];
                    var VATRate = values.NewValues["VATRate"];
                    var TargetDateIn = values.NewValues["TargetDateIn"];
                    var TargetDateOut = values.NewValues["TargetDateOut"];
                    var ActualDateIn = values.NewValues["ActualDateIn"];
                    var ActualDateOut = values.NewValues["ActualDateOut"];
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    var Version = values.NewValues["Version"];

                    source.Rows.Add(DocNumber, LineNumber, Sequence, StepCode, WorkCenter, Overhead, PreProd, IsInhouse, InQty, OutQty, AdjQty, Instruction, EstWorkOrderPrice,
                        WorkOrderPrice, WorkOrderDate, WorkOrderQty, DateCommitted, VAT, VATRate, TargetDateIn, TargetDateOut, ActualDateIn, ActualDateOut, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Version);

                    Line1 = Line1 + 1;
                }

                foreach (DataRow dtRow in source.Rows)
                {
                    _JOStepPlanning.DocNumber = txtDocNumber.Text;
                    _JOStepPlanning.Sequence = Convert.ToDecimal(Convert.IsDBNull(dtRow["Sequence"]) ? 0 : dtRow["Sequence"]);
                    _JOStepPlanning.PreProd = Convert.ToBoolean(Convert.IsDBNull(dtRow["PreProd"].ToString()) ? false : dtRow["PreProd"]);
                    _JOStepPlanning.StepCode = dtRow["StepCode"].ToString();
                    _JOStepPlanning.WorkCenter = dtRow["WorkCenter"].ToString();
                    _JOStepPlanning.Overhead = dtRow["Overhead"].ToString();
                    _JOStepPlanning.IsInhouse = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInhouse"].ToString()) ? false : dtRow["IsInhouse"]);
                    _JOStepPlanning.InQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["InQty"]) ? 0 : dtRow["InQty"]);
                    _JOStepPlanning.OutQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OutQty"]) ? 0 : dtRow["OutQty"]);
                    _JOStepPlanning.AdjQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["AdjQty"]) ? 0 : dtRow["AdjQty"]);
                    _JOStepPlanning.Allowance = Convert.ToDecimal(Convert.IsDBNull(dtRow["Allowance"]) ? 0 : dtRow["Allowance"]);
                    _JOStepPlanning.Yield = Convert.ToDecimal(Convert.IsDBNull(dtRow["Yield"]) ? 0 : dtRow["Yield"]);
                    _JOStepPlanning.ActualLoss = Convert.ToDecimal(Convert.IsDBNull(dtRow["ActualLoss"]) ? 0 : dtRow["ActualLoss"]);
                    _JOStepPlanning.ActualYield = Convert.ToDecimal(Convert.IsDBNull(dtRow["ActualYield"]) ? 0 : dtRow["ActualYield"]);
                    _JOStepPlanning.Instruction = dtRow["Instruction"].ToString();
                    _JOStepPlanning.EstWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstWorkOrderPrice"]) ? 0 : dtRow["EstWorkOrderPrice"]);
                    _JOStepPlanning.WorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["WorkOrderPrice"]) ? 0 : dtRow["WorkOrderPrice"]);
                    _JOStepPlanning.WorkOrderDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["WorkOrderDate"]) ? null : dtRow["WorkOrderDate"]);
                    _JOStepPlanning.WorkOrderQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["WorkOrderQty"]) ? 0 : dtRow["WorkOrderQty"]);
                    _JOStepPlanning.DateCommitted = Convert.ToDateTime(Convert.IsDBNull(dtRow["DateCommitted"]) ? null : dtRow["DateCommitted"]);
                    _JOStepPlanning.VAT = dtRow["VAT"].ToString();
                    _JOStepPlanning.VATRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATRate"]) ? 0 : dtRow["VATRate"]);
                    _JOStepPlanning.TargetDateIn = Convert.ToDateTime(Convert.IsDBNull(dtRow["TargetDateIn"]) ? null : dtRow["TargetDateIn"]);
                    _JOStepPlanning.TargetDateOut = Convert.ToDateTime(Convert.IsDBNull(dtRow["TargetDateOut"]) ? null : dtRow["TargetDateOut"]);
                    _JOStepPlanning.ActualDateIn = Convert.ToDateTime(Convert.IsDBNull(dtRow["ActualDateIn"]) ? null : dtRow["ActualDateIn"]);
                    _JOStepPlanning.ActualDateOut = Convert.ToDateTime(Convert.IsDBNull(dtRow["ActualDateOut"]) ? null : dtRow["ActualDateOut"]);
                    _JOStepPlanning.Field1 = dtRow["Field1"].ToString();
                    _JOStepPlanning.Field2 = dtRow["Field2"].ToString();
                    _JOStepPlanning.Field3 = dtRow["Field3"].ToString();
                    _JOStepPlanning.Field4 = dtRow["Field4"].ToString();
                    _JOStepPlanning.Field5 = dtRow["Field5"].ToString();
                    _JOStepPlanning.Field6 = dtRow["Field6"].ToString();
                    _JOStepPlanning.Field7 = dtRow["Field7"].ToString();
                    _JOStepPlanning.Field8 = dtRow["Field8"].ToString();
                    _JOStepPlanning.Field9 = dtRow["Field9"].ToString();
                    _JOStepPlanning.AddJOStepPlanning(_JOStepPlanning);
                }
            }
        }
        protected void gvClass_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        protected void gvSize_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion
        private DataTable CascadeStepTemplate()
        {
            Session["JOSPDatatable"] = "0";
            gvStepPlanning.DataSource = null;
            if (gvStepPlanning.DataSourceID != "")
            {
                gvStepPlanning.DataSourceID = null;
            }
            gvStepPlanning.DataBind();
            DataTable dt = new DataTable();
            string selectedValues1 = Session["JOStyle"].ToString();

            if (Session["JOStepCascade"] != null)
            {
                //Session["JOSPDetail"] = "SELECT CONVERT(VARCHAR(MAX),'" + txtDocNumber.Text + "') AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY PISNumber, B.IsPreProductionStep DESC, Sequence ASC, A.StepCode ASC) AS VARCHAR(5)),5) AS LineNumber, A.Sequence, A.StepCode, ISNULL(A.Supplier,'') AS WorkCenter, "
                //+ " ISNULL(OverheadCode,'') AS Overhead, ISNULL(B.IsPreProductionStep,0) AS PreProd, ISNULL(B.IsInhouse,0) AS IsInhouse, 0.00 AS InQty, 0.00 AS OutQty, 0.00 AS AdjQty, '' As Instruction, ISNULL(A.EstimatedPrice,0) AS EstWorkOrderPrice, "
                //+ " 0 AS WorkOrderPrice, CONVERT(datetime,NULL) AS WorkOrderDate, 0.00 AS WorkOrderQty, CONVERT(datetime,NULL) AS DateCommitted, CONVERT(datetime,NULL) AS TargetDateIn, CONVERT(datetime,NULL) AS TargetDateOut, CONVERT(datetime,NULL) AS ActualDateIn, "
                //+ " CONVERT(datetime,NULL) AS ActualDateOut, '' AS WOPrint, '' AS WOPrint1, CONVERT(VARCHAR(MAX),'') AS Field1, CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6, "
                //+ " CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9, '2' AS Version, '' AS StepTemplate, 0.00 AS OHRate, '' AS OHType, '' AS ForCallback, ISNULL(B.MinimumWOPrice,0) AS MinPrice, ISNULL(B.MaximumWOPrice,0) AS MaxPrice, '' as VAT,0 as VATRate "
                //+ " FROM Masterfile.PISStepTemplate A INNER JOIN Masterfile.Step B ON A.StepCode = B.StepCode "
                ////+ " left join masterfile.BPSupplierInfo C on A.Supplier = C.SupplierCode left join masterfile.Tax D on C.TaxCode = D.TCode "
                //+ " WHERE PISNumber = '" + aglPISNumber.Text.Trim() + "' ORDER BY PISNumber, B.IsPreProductionStep DESC, A.Sequence ASC, A.StepCode ASC ";

                Session["JOSPDetail"] = "SELECT CONVERT(VARCHAR(MAX),'" + txtDocNumber.Text + "') AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY PISNumber, B.IsPreProductionStep DESC, Sequence ASC, A.StepCode ASC) AS VARCHAR(5)),5) AS LineNumber, A.Sequence, A.StepCode, ISNULL(A.Supplier,'') AS WorkCenter, "
                + " ISNULL(OverheadCode,'') AS Overhead, ISNULL(B.IsPreProductionStep,0) AS PreProd, ISNULL(B.IsInhouse,0) AS IsInhouse, 0.00 AS InQty, 0.00 AS OutQty, 0.00 AS AdjQty, B.Allowance, B.Yield, 0.00 AS ActualLoss, 0.00 AS ActualYield, '' As Instruction, ISNULL(A.EstimatedPrice,0) AS EstWorkOrderPrice, "
                + " 0 AS WorkOrderPrice, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS WorkOrderDate, " + (String.IsNullOrEmpty(speTotalJO.Text) ? 0.00 : Convert.ToDouble(speTotalJO.Text)).ToString() + " AS WorkOrderQty, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS DateCommitted, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS TargetDateIn, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS TargetDateOut, CONVERT(datetime,NULL) AS ActualDateIn, "
                + " CONVERT(datetime,NULL) AS ActualDateOut, '' AS WOPrint, '' AS WOPrint1, CONVERT(VARCHAR(MAX),'') AS Field1, CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6, "
                + " CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9, '2' AS Version, '' AS StepTemplate, 0.00 AS OHRate, '' AS OHType, '' AS ForCallback, ISNULL(B.MinimumWOPrice,0) AS MinPrice, ISNULL(B.MaximumWOPrice,0) AS MaxPrice, '' as VAT,0 as VATRate "
                + " FROM Masterfile.PISStepTemplate A INNER JOIN Masterfile.Step B ON A.StepCode = B.StepCode "
                + " WHERE PISNumber = '" + aglPISNumber.Text.Trim() + "' UNION ALL "
                + " SELECT CONVERT(VARCHAR(MAX),'" + txtDocNumber.Text + "') AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StyleCode, B.IsPreProductionStep DESC, Sequence ASC, A.Step ASC) AS VARCHAR(5)),5) AS LineNumber, A.Sequence, A.Step AS StepCode, ISNULL(A.WorkCenter,'') AS WorkCenter,   "
                + " ISNULL(OverheadCode,'') AS Overhead, ISNULL(B.IsPreProductionStep,0) AS PreProd, ISNULL(B.IsInhouse,0) AS IsInhouse, 0.00 AS InQty, 0.00 AS OutQty, 0.00 AS AdjQty, B.Allowance, B.Yield, 0.00 AS ActualLoss, 0.00 AS ActualYield, '' As Instruction, ISNULL(A.LaborCost,0) AS EstWorkOrderPrice, ISNULL(A.LaborCost,0) AS WorkOrderPrice, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS WorkOrderDate,  "
                + " " + (String.IsNullOrEmpty(speTotalJO.Text) ? 0.00 : Convert.ToDouble(speTotalJO.Text)).ToString() + " AS WorkOrderQty, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS DateCommitted, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS TargetDateIn, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS TargetDateOut, CONVERT(datetime,NULL) AS ActualDateIn, CONVERT(datetime,NULL) AS ActualDateOut, '' AS WOPrint, '' AS WOPrint1, CONVERT(VARCHAR(MAX),'') AS Field1,  "
                + " CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6,  CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9, '2' AS Version, '' AS StepTemplate,  "
                + " 0.00 AS OHRate, '' AS OHType, '' AS ForCallback, ISNULL(B.MinimumWOPrice,0) AS MinPrice, ISNULL(B.MaximumWOPrice,0) AS MaxPrice, '' as VAT,0 as VATRate FROM Masterfile.StyleTemplateDetailSequence A INNER JOIN Masterfile.Step B ON A.Step = B.StepCode WHERE StyleCode = '" + aglPISNumber.Text.Trim() + "'";

            }
            else
            {
                Session["JOSPDetail"] = "SELECT CONVERT(VARCHAR(MAX),'" + txtDocNumber.Text + "') AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY B.IsPreProductionStep DESC, Sequence ASC, A.StepCode ASC) AS VARCHAR(5)),5) AS LineNumber, Sequence,A.StepCode, '' AS WorkCenter, "
                + " ISNULL(OverheadCode,'') AS Overhead, ISNULL(B.IsPreProductionStep,0) AS PreProd, ISNULL(B.IsInhouse,0) AS IsInhouse, 0.00 AS InQty, 0.00 AS OutQty, 0.00 AS AdjQty, B.Allowance, B.Yield, 0.00 AS ActualLoss, 0.00 AS ActualYield, '' As Instruction, ISNULL(B.EstimatedWOPrice,0.00) AS EstWorkOrderPrice, "
                + " ISNULL(B.EstimatedWOPrice,0) AS WorkOrderPrice, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS WorkOrderDate, " + (String.IsNullOrEmpty(speTotalJO.Text) ? 0.00 : Convert.ToDouble(speTotalJO.Text)).ToString() + " AS WorkOrderQty, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS DateCommitted, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS TargetDateIn, CONVERT(datetime,'" + (DateTime.Now.ToShortDateString()).ToString() + "') AS TargetDateOut, CONVERT(datetime,NULL) AS ActualDateIn, "
                + " CONVERT(datetime,NULL) AS ActualDateOut, '' AS WOPrint, '' AS WOPrint1, CONVERT(VARCHAR(MAX),'') AS Field1, CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6, "
                + " CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9, '2' AS Version, StepTemplateCode AS StepTemplate, 0.00 AS OHRate, '' AS OHType, '' AS ForCallback, ISNULL(B.MinimumWOPrice,0) AS MinPrice, ISNULL(B.MaximumWOPrice,0) AS MaxPrice, '' as VAT,0 as VATRate "
                + " FROM Masterfile.StepTemplateDetail A INNER JOIN Masterfile.Step B ON A.StepCode = B.StepCode "
                //+ " left join masterfile.BPSupplierInfo C on A.Supplier = C.SupplierCode left join masterfile.Tax D on C.TaxCode = D.TCode "
                + " WHERE StepTemplateCode = '" + selectedValues1 + "' ORDER BY B.IsPreProductionStep DESC, Sequence ASC, A.StepCode ASC ";
            }

            sdsStepTemplateDetail.SelectCommand = Session["JOSPDetail"].ToString();
            gvStepPlanning.DataSource = sdsStepTemplateDetail;
            if (gvStepPlanning.DataSourceID != "")
            {
                gvStepPlanning.DataSourceID = null;
            }
            gvStepPlanning.DataBind();
            Session["JOSPDatatable"] = "1";

            foreach (GridViewColumn col in gvStepPlanning.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvStepPlanning.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvStepPlanning.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] {
            dt.Columns["LineNumber"]};

            return dt;
        }
        private DataTable CascadePIS()
        {
            Session["BOMDatatable"] = "0";
            gvBOM.DataSource = null;
            if (gvBOM.DataSourceID != "")
            {
                gvBOM.DataSourceID = null;
            }
            //gvBOM.DataBind();
            DataTable dt = new DataTable();
            //string selectedValues = aglPISNumber.Text;
            string selectedValues = Session["JOPISNumber"].ToString();
            //Session["BOMDetail"] = "SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StepCode, Components, StockSize, A.ItemCode, ColorCode, ClassCode, SizeCode) AS VARCHAR(5)),5) AS LineNumber, ISNULL(Components,'') AS Components, "
            //    + " StepCode, CONVERT(VARCHAR(MAX),'') AS ProductCode, CONVERT(VARCHAR(MAX),'') AS ProductColor, StockSize AS ProductSize, A.ItemCode, B.FullDesc, ColorCode, ClassCode, SizeCode, A.Unit, CONVERT(DECIMAL(16,6),ISNULL(PerPieceConsumption,0)) AS PerPieceConsumption, 0.000000 AS Consumption, ISNULL(C.AllowancePerc,0) AS AllowancePerc, "
            //    + " 0.00 AllowanceQty, CONVERT(DECIMAL(15,4),ISNULL(A.EstimatedUnitCost,0)) AS UnitCost, CONVERT(bit,0) AS IsMajorMaterial, CONVERT(bit,0) AS IsBulk, CONVERT(bit,0) AS IsRounded, CONVERT(bit,0) AS IsExcluded, CONVERT(VARCHAR(MAX),'') AS Field1, CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, "
            //    + " CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6, CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9,'2' AS Version  FROM Masterfile.PISStyleTemplate A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode "
            //    + " INNER JOIN Masterfile.ProductCategorySub C ON B.ProductSubCatCode = C.ProductSubCatCode WHERE A.PISNumber = '" + selectedValues + "'";
            
            //old code 07/27/2017
            //Session["BOMDetail"] = "SELECT * FROM (SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StepCode ASC, Components ASC) AS VARCHAR(5)),5) AS LineNumber, ISNULL(Components,'') AS Components, "
            //    + " StepCode, CONVERT(VARCHAR(MAX),'') AS ProductCode, CONVERT(VARCHAR(MAX),'') AS ProductColor, StockSize AS ProductSize, A.ItemCode, B.FullDesc, ColorCode, ClassCode, SizeCode, A.Unit, CONVERT(DECIMAL(16,6),ISNULL(PerPieceConsumption,0)) AS PerPieceConsumption, 0.000000 AS Consumption, ISNULL(C.AllowancePerc,0) AS AllowancePerc, "
            //    + " 0.000000 AS AllowanceQty, 0.000000 AS RequiredQty, CONVERT(DECIMAL(15,4),ISNULL(A.EstimatedUnitCost,0)) AS UnitCost, CONVERT(bit,0) AS IsMajorMaterial, CONVERT(bit,0) AS IsBulk, CONVERT(bit,0) AS IsRounded, CONVERT(bit,0) AS IsExcluded, CONVERT(VARCHAR(MAX),'') AS Field1, CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, "
            //    + " CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6, CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9,'2' AS Version  FROM Masterfile.PISStyleTemplate A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode "
            //    + " LEFT JOIN Masterfile.ProductCategorySub C ON B.ProductSubCatCode = C.ProductSubCatCode WHERE A.PISNumber = '" + selectedValues + "') AS A ORDER BY LineNumber";

            Session["BOMDetail"] = "SELECT * FROM (SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StepCode ASC, Components ASC) AS VARCHAR(5)),5) AS LineNumber, ISNULL(Components,'') AS Components, " 
                + " StepCode, CONVERT(VARCHAR(MAX),'') AS ProductCode, CONVERT(VARCHAR(MAX),'') AS ProductColor, StockSize AS ProductSize, A.ItemCode, B.FullDesc, ColorCode, ClassCode, SizeCode, A.Unit, CONVERT(DECIMAL(16,6),ISNULL(PerPieceConsumption,0)) AS PerPieceConsumption, 0.000000 AS Consumption, ISNULL(C.AllowancePerc,0) AS AllowancePerc, " 
                + " 0.000000 AS AllowanceQty, 0.000000 AS RequiredQty, CONVERT(DECIMAL(15,4),ISNULL(A.EstimatedUnitCost,0)) AS UnitCost, CONVERT(bit,0) AS IsMajorMaterial, CONVERT(bit,0) AS IsBulk, CONVERT(bit,0) AS IsRounded, CONVERT(bit,0) AS IsExcluded, CONVERT(VARCHAR(MAX),'') AS Field1, CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, " 
                + " CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6, CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9,'2' AS Version  FROM Masterfile.PISStyleTemplate A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode " 
                + " LEFT JOIN Masterfile.ProductCategorySub C ON B.ProductSubCatCode = C.ProductSubCatCode WHERE A.PISNumber = '" + selectedValues + "' ) AS A  "
                + " UNION ALL "
                + " SELECT * FROM (SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StyleCode ASC) AS VARCHAR(5)),5) AS LineNumber, A.Component AS Components, "
                + " A.Step AS StepCode, CONVERT(VARCHAR(MAX),'') AS ProductCode, CONVERT(VARCHAR(MAX),'') AS ProductColor, CONVERT(VARCHAR(MAX),'N/A') AS ProductSize, A.ItemCode, B.FullDesc, ColorCode, ClassCode, SizeCode, UnitBase AS Unit, CONVERT(DECIMAL(16,6),ISNULL(PerPieceConsumption,0)) AS PerPieceConsumption, 0.000000 AS Consumption, ISNULL(C.AllowancePerc,0) AS AllowancePerc, " 
                + " 0.000000 AS AllowanceQty, 0.000000 AS RequiredQty, CONVERT(DECIMAL(15,4),ISNULL(A.UnitCost,0)) AS UnitCost, CONVERT(bit,0) AS IsMajorMaterial, CONVERT(bit,0) AS IsBulk, CONVERT(bit,0) AS IsRounded, CONVERT(bit,0) AS IsExcluded, CONVERT(VARCHAR(MAX),'') AS Field1, CONVERT(VARCHAR(MAX),'') AS Field2, CONVERT(VARCHAR(MAX),'') AS Field3, CONVERT(VARCHAR(MAX),'') AS Field4, "
                + " CONVERT(VARCHAR(MAX),'') AS Field5, CONVERT(VARCHAR(MAX),'') AS Field6, CONVERT(VARCHAR(MAX),'') AS Field7, CONVERT(VARCHAR(MAX),'') AS Field8, CONVERT(VARCHAR(MAX),'') AS Field9,'2' AS Version FROM (SELECT A.*, EstimatedTotalCost FROM Masterfile.StyleTemplateDetail A INNER JOIN Masterfile.StyleTemplate B ON A.StyleCode = B.StyleCode) A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode " 
                + " LEFT JOIN Masterfile.ProductCategorySub C ON B.ProductSubCatCode = C.ProductSubCatCode WHERE A.StyleCode = '" + selectedValues + "' ) AS A ";

            sdsPISDetail.SelectCommand = Session["BOMDetail"].ToString();
            gvBOM.DataSource = sdsPISDetail;
            if (gvBOM.DataSourceID != "")
            {
                gvBOM.DataSourceID = null;
            }
            gvBOM.DataBind();
            Session["BOMDatatable"] = "1";

            foreach (GridViewColumn col in gvBOM.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvBOM.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvBOM.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] {
            dt.Columns["LineNumber"]};

            return dt;
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            //Session["user"] = "1828";
            //Session["ConnString"] = "Data Source=192.168.201.115;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;connection timeout=1800;";
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        protected void gvAdjustment_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            if (!String.IsNullOrEmpty(aglCustomerCode.Text))
            {
                e.NewValues["ProfitCenter"] = Session["Profit"].ToString();
                e.NewValues["CostCenter"] = Session["Cost"].ToString();
            }
        }
        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                string fck = txtDocNumber.Text;
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                //sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInActive,0) = 0";
                //if (Context.Items["JOsdsItem" + hidcon["key"]] == null)
                //{
                //    string proditemsql = "select distinct a.ItemCode, b.FullDesc from Production.JOProductOrder a " +
                //                                    "left join masterfile.Item b on a.ItemCode = b.ItemCode " +
                //                                    "where DocNumber = '" + txtDocNumber.Text + "' and ISNULL(ReferenceSO,'')!=''";
                //    DataTable checkProditem = Gears.RetriveData2(proditemsql, Session["ConnString"].ToString());

                //    if (checkProditem.Rows.Count > 0)
                //    {
                //        Session["itemJOsql" + hidcon["key"]] = proditemsql;
                //    }
                //}
                gridLookup.DataSource = GetDataTableFromCacheOrDatabase();
                gridLookup.DataBind();
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                //string changes = param.Split(';')[0];
                string itemcode = param.Split(';')[1];
                if (string.IsNullOrEmpty(itemcode)) return;

                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable countitem = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode, A.FullDesc FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          " WHERE A.ItemCode = '" + itemcode +
                                                          "' AND (ISNULL(A.IsInActive,0) = 0 OR ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                DataTable itemdesc = Gears.RetriveData2("SELECT DISTINCT FullDesc FROM Masterfile.Item WHERE ItemCode = '" + itemcode +
                                                          "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntsze = countsize.Rows.Count;

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntclr > 1 && cntcls > 1 && cntsze == 1)
                    {
                        codes = ";;";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";

                    }
                    else if (cntclr > 1 && cntcls == 1 && cntsze > 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                    else if (cntclr == 1 && cntcls > 1 && cntsze > 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";

                    }
                    else
                    {
                        codes = ";;;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemcode + ";";
                    }
                }
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }
        }
        public void bProductOrders_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if ((IsCallback && Request.Params["__CALLBACKID"].Contains("gvBOM")) && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(bProductOrders_CustomCallback);
                if (Session["JOProductOrderFilter"] != null)
                {
                    if (Session["PCChecking"] == "Item" && Session["PCChecking"] == glIDFinder(gridLookup.ID))
                    {
                        string test = Request.Params["__CALLBACKPARAM"].ToString();
                        gridLookup.GridView.DataSourceID = "sdsProdItem";
                        sdsProdItem.SelectCommand = Session["JOProductOrderFilter"].ToString();
                        if (Request.Params["__CALLBACKPARAM"].Contains("SELECTROWS"))
                            gridLookup.GridView.JSProperties["cp_unsel"] = true;
                    }
                    else if (Session["PCChecking"] == "Color" && Session["PCChecking"] == glIDFinder(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "sdsProdColor";
                        sdsProdColor.SelectCommand = Session["JOProductOrderFilter"].ToString();
                    }
                    else if (Session["PCChecking"] == "Size" && Session["PCChecking"] == glIDFinder(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "sdsProdSize";
                        sdsProdSize.SelectCommand = Session["JOProductOrderFilter"].ToString();
                    }
                    //gridLookup.DataBind();
                }
            }
        }

        public string glIDFinder(string glID)
        {
            if (glID.Contains("ColorCode"))
                return "Color";
            else if (glID.Contains("Item"))
                return "Item";
            else
                return "Size";
        }
        public void bProductOrders_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;
            string existitem = e.Parameters.Split('|')[3];
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;

            if (e.Parameters.Contains("ItemCode"))
            {
                sdsProdItem.SelectCommand = string.Format("SELECT DISTINCT DocNumber, ItemCode, '' AS ColorCode, '' AS SizeCode FROM Production.JOProductOrder where DocNumber = '{0}'", txtDocNumber.Text);
                
                Session["JOProductOrderFilter"] = sdsProdItem.SelectCommand;
                ASPxGridView grid = sender as ASPxGridView;
                //CriteriaOperator selectionCriteria = new InOperator("DocNumber", new string[] { txtDocNumber.Text });
                //sdsProdItem.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["PCChecking"] = "Item";
                //Session["JOProductOrderFilter"] = sdsProdItem.FilterExpression;
                grid.DataSourceID = "sdsProdItem";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, column) != null)
                    {
                        if (grid.GetRowValues(i, column).ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, column).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                    }

                }
                //grid.JSProperties["cp_changed"] = true;
            }
            else if (e.Parameters.Contains("ColorCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;

                sdsProdColor.SelectCommand = "SELECT DISTINCT DocNumber, '' AS ItemCode, ColorCode, '' AS SizeCode FROM Production.JOProductOrder where DocNumber = '" + txtDocNumber.Text + "'";
                Session["JOProductOrderFilter"] = sdsProdColor.SelectCommand;
                //if (existitem != "noitem")
                //{
                //    CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                //    CriteriaOperator selectionCriteria1 = new InOperator("DocNumber", new string[] { txtDocNumber.Text });
                //    sdsProdColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, selectionCriteria1)).ToString();
                //}
                //else
                //{
                //CriteriaOperator selectionCriteria1 = new InOperator("DocNumber", new string[] { txtDocNumber.Text });
                //sdsProdColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1)).ToString();
                //} 

                Session["PCChecking"] = "Color";
                //Session["JOProductOrderFilter"] = sdsProdColor.FilterExpression;
                grid.DataSourceID = "sdsProdColor";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, column) != null)
                        if (grid.GetRowValues(i, column).ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, column).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("SizeCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;
                sdsProdSize.SelectCommand = "SELECT null as DocNumber, '' AS ItemCode, '' AS ColorCode, 'N/A' AS SizeCode UNION ALL "
                    +"SELECT DISTINCT DocNumber, '' AS ItemCode, '' AS ColorCode, SizeCode FROM Production.JOProductOrder where DocNumber = '" + txtDocNumber.Text + "'";
                Session["JOProductOrderFilter"] = sdsProdSize.SelectCommand;
                //if (existitem != "noitem")
                //{
                //    CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                //    CriteriaOperator selectionCriteria1 = new InOperator("DocNumber", new string[] { txtDocNumber.Text });
                //    sdsProdSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, selectionCriteria1)).ToString();
                //}
                //else
                //{
                //CriteriaOperator selectionCriteria1 = new InOperator("DocNumber", new string[] { txtDocNumber.Text });
                //sdsProdSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1)).ToString();
                //}

                Session["PCChecking"] = "Size";
                //Session["JOProductOrderFilter"] = sdsProdSize.FilterExpression;
                grid.DataSourceID = "sdsProdSize";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, column) != null)
                        if (grid.GetRowValues(i, column).ToString().Trim() == val.Trim())
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, column).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }
        protected void dStepCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStepPlanning") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(dStepCode_CustomCallback);
                if (Session["SPFilterExpression"] != null)
                {
                    gridLookup.GridView.DataSourceID = "sdsPlanSteps";
                    sdsPlanSteps.FilterExpression = Session["SPFilterExpression"].ToString();
                }
            }
        }
        public void dStepCode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string stepcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;
            var itemlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("StepCode"))
            {
                DataTable countitem = Gears.RetriveData2("SELECT ISNULL(IsInhouse,0) AS Inhouse, ISNULL(IsPreProductionStep,0) AS PreProd, ISNULL(OverheadCode,'') AS Overhead, "
                    + " ISNULL(EstimatedWOPrice,0) AS WorkOrderPrice, ISNULL(OverheadCost,0) AS OHRate, "
                    + " CASE WHEN ISNULL(OverheadType,'')!= '' THEN LEFT(OverheadType,LEN(OverheadType)- (LEN(OverheadType)-1)) ELSE '' END AS OHType, "
                    + " ISNULL(A.MinimumWOPrice,0) AS MinPrice, ISNULL(A.MaximumWOPrice,0) AS MaxPrice"
                    + " FROM Masterfile.Step A LEFT JOIN Masterfile.Overhead B ON A.OverheadCode = B.OHCode WHERE A.StepCode = '" + stepcode + "'", Session["ConnString"].ToString());

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["Inhouse"].ToString() + ";";
                        codes += dt["Overhead"].ToString() + ";";
                        codes += dt["WorkOrderPrice"].ToString() + ";";
                        codes += dt["PreProd"].ToString() + ";";
                        codes += dt["OHRate"].ToString() + ";";
                        codes += dt["OHType"].ToString() + ";";
                        codes += dt["MinPrice"].ToString() + ";";
                        codes += dt["MaxPrice"].ToString() + ";";
                    }
                }

                itemlookup.JSProperties["cp_SPcodes"] = codes;
                itemlookup.JSProperties["cp_SPidentifier"] = "stepcode";
            }
            else if (e.Parameters.Contains("Overhead"))
            {
                DataTable countitem = Gears.RetriveData2("SELECT ISNULL(OverheadCost,0) AS OHRate, "
                    + " CASE WHEN ISNULL(OverheadType,'')!= '' THEN LEFT(OverheadType,LEN(OverheadType)- (LEN(OverheadType)-1)) ELSE '' END AS OHType "
                    + " FROM Masterfile.Overhead WHERE OHCode = '" + stepcode + "'", Session["ConnString"].ToString());

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes += dt["OHRate"].ToString() + ";";
                        codes += dt["OHType"].ToString() + ";";
                    }
                }

                itemlookup.JSProperties["cp_SPcodes"] = codes;
                itemlookup.JSProperties["cp_SPidentifier"] = "overhead";
            }

        }
        protected void gvBOM_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["BOMDetail"] = null;
            }

            //if (Session["BOMDetail"] != null)
            //{
            //   sdsPISDetail.SelectCommand = Session["BOMDetail"].ToString();
            //   gvBOM.DataSource = sdsPISDetail;
            //}
        }
        protected void gvStepPlanning_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["JOSPDetail"] = null;
            }

            //if (Session["JOSPDetail"] != null)
            //{
            //    sdsStepTemplateDetail.SelectCommand = Session["JOSPDetail"].ToString();
            //    gvStepPlanning.DataSource = sdsStepTemplateDetail;
            //    if (gvStepPlanning.DataSourceID != "")
            //    {
            //        gvStepPlanning.DataSourceID = null;
            //    }
            //    //gvStepPlanning.DataBind();
            //}
        }
        protected void gvStepPlanning_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string param = e.Parameters.Split('|')[0];

            switch (param)
            {
                case "GenerateStepTemplate":
                    Session["JOStepCascade"] = null;
                    Session["JOStyle"] = e.Parameters.Split('|')[1].Trim();
                    CascadeStepTemplate();
                    //GenerateBtn();
                    break;

                case "GenerateStepTemplatePIS":
                    Session["JOStepCascade"] = "1";
                    Session["JOStyle"] = aglStepTemplate.Text;
                    CascadeStepTemplate();
                    //GenerateBtn();
                    break;

                case "CallbackTemplate":
                    Session["JOStyle"] = aglStepTemplate.Text;
                    //GenerateBtn();
                    break;

                case "WOPrintClick":
                    string a = e.Parameters.Split('|')[1].Trim();
                    string lineA = e.Parameters.Split('|')[2].Trim();
                    DataTable get = Gears.RetriveData2("SELECT ISNULL(IsPrintedSingle,0) AS Printed FROM Production.JOStepPlanning WHERE DocNumber ='" + txtDocNumber.Text + "' AND LineNumber = '" + lineA + "'", Session["ConnString"].ToString());

                    gvStepPlanning.JSProperties["cp_JO"] = true;
                    gvStepPlanning.JSProperties["cp_JOStep"] = a;
                    gvStepPlanning.JSProperties["cp_JOLineA"] = lineA;
                    gvStepPlanning.JSProperties["cp_JOisprinted"] = get.Rows[0]["Printed"].ToString();
                    break;

                case "WOPrintMultiple":
                    string b = e.Parameters.Split('|')[1].Trim();
                    string lineB = e.Parameters.Split('|')[2].Trim();
                    DataTable getb = Gears.RetriveData2("SELECT ISNULL(IsPrintedMultiple,0) AS Printed FROM Production.JOStepPlanning WHERE DocNumber ='" + txtDocNumber.Text + "' AND LineNumber = '" + lineB + "'", Session["ConnString"].ToString());

                    gvStepPlanning.JSProperties["cp_JOMul"] = true;
                    gvStepPlanning.JSProperties["cp_JOWorkCntr"] = b;
                    gvStepPlanning.JSProperties["cp_JOLineB"] = lineB;
                    gvStepPlanning.JSProperties["cp_JOisprinted1"] = getb.Rows[0]["Printed"].ToString();
                    break;

                case "GenerateMultiPrint":
                    string[] keycode = new string[] { "LineNumber" };
                    List<object> fieldValues = gvStepPlanning.GetSelectedFieldValues(keycode);

                    string LineHolder = "'";
                    foreach (string x in fieldValues)
                    {
                        LineHolder += x + "','";
                    }
                    LineHolder = LineHolder.Substring(0, LineHolder.Length - 2);
                    DataTable getc = Gears.RetriveData2(" DECLARE @Steps varchar(MAX), @LineNums varchar(MAX); " +
                                                        " SELECT @Steps =  COALESCE(@Steps +',' ,'') + LTRIM(RTRIM(ISNULL(A.StepCode,''))), " +
                                                        " @LineNums = COALESCE(@LineNums +',' ,'') + LTRIM(RTRIM(ISNULL(A.LineNumber,'')))  " +
                                                        " FROM Production.JOStepPlanning A " +
                                                        " WHERE DocNumber ='" + txtDocNumber.Text + "' " +
                                                        " AND LineNumber IN (" + LineHolder + ") " +
                                                        " SELECT @Steps AS Steps, @LineNums AS LineNums", Session["ConnString"].ToString());

                    gvStepPlanning.JSProperties["cp_JOMulSing"] = true;
                    gvStepPlanning.JSProperties["cp_JOSPStep2"] = getc.Rows[0]["Steps"].ToString();
                    gvStepPlanning.JSProperties["cp_JOLineNumber2"] = getc.Rows[0]["LineNums"].ToString();
                    gvStepPlanning.JSProperties["cp_JOisprinted2"] = "0";

                    break;

            }
        }
        protected void gvBOM_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string param = e.Parameters.Split('|')[0];

            switch (param)
            {
                case "GeneratePIS":

                    Session["JOPISNumber"] = null;
                    Session["JOPISNumber"] = e.Parameters.Split('|')[1];

                    //DataTable PIS = Gears.RetriveData2("SELECT Designer , CONVERT(int,ISNULL(B.LeadTime,0)) AS Leadtime FROM Production.ProductInfoSheet A LEFT JOIN Masterfile.ProductClass B "
                    //        + " ON A.ProductClass = B.ProductClassCode WHERE PISNumber = '" + aglPISNumber.Text.Trim() + "'", Session["ConnString"].ToString());

                    //DataTable Check = Gears.RetriveData2("SELECT PISNumber FROM Masterfile.PISStepTemplate WHERE PISNumber = '" + aglPISNumber.Text.Trim() + "'", Session["ConnString"].ToString());

                    DataTable PIS = Gears.RetriveData2("SELECT Designer , CONVERT(int,ISNULL(B.LeadTime,0)) AS Leadtime FROM Production.ProductInfoSheet A LEFT JOIN Masterfile.ProductClass B "
                            + " ON A.ProductClass = B.ProductClassCode WHERE PISNumber = '" + Session["JOPISNumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable Check = Gears.RetriveData2("SELECT PISNumber FROM Masterfile.PISStepTemplate WHERE PISNumber = '" + Session["JOPISNumber"].ToString() + "' UNION ALL " 
                                                       + "SELECT StyleCode AS PISNumber FROM Masterfile.StyleTemplateDetail WHERE StyleCode = '" + Session["JOPISNumber"].ToString() + "'", Session["ConnString"].ToString());

                    if (PIS.Rows.Count > 0)
                    {
                        int Lead = Convert.ToInt32(speLeadtime.Text);

                        gvBOM.JSProperties["cp_JOSign"] = true;
                        gvBOM.JSProperties["cp_JODesigner"] = PIS.Rows[0]["Designer"].ToString();
                        gvBOM.JSProperties["cp_JOLeadVal"] = Convert.ToInt32(PIS.Rows[0]["Leadtime"].ToString()) == 0 ? Lead : Convert.ToInt32(PIS.Rows[0]["Leadtime"].ToString());
                    }

                    CascadePIS();

                    if (Check.Rows.Count > 0)
                    {
                        gvBOM.JSProperties["cp_StepCascade"] = true;
                    }
                    gvBOM.JSProperties["cp_generated"] = true;

                    break;

                case "CallbackPISNumber":
                    Session["JOPISNumber"] = aglPISNumber.Text;
                    break;
            }
        }
        protected void GenerateBtn()
        {
            if (String.IsNullOrEmpty(aglPISNumber.Text))
            {
                btnPISDetail.ClientEnabled = false;
            }
            else
            {
                btnPISDetail.ClientEnabled = true;
            }

            if (String.IsNullOrEmpty(aglStepTemplate.Text))
            {
                btnStepTempDetail.ClientEnabled = false;
            }
            else
            {
                btnStepTempDetail.ClientEnabled = true;
            }
        }
        protected void aglParentStep_Init(object sender, EventArgs e)
        {

        }
        protected void gvBOM_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IsMajorMaterial"] = false;
            e.NewValues["IsBulk"] = false;
            e.NewValues["IsRounded"] = false;
            e.NewValues["IsExcluded"] = false;
            e.NewValues["DocNumber"] = txtDocNumber.Text;
        }
        protected void gvStepPlanning_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["PreProd"] = false;
            e.NewValues["IsInhouse"] = false;
            e.NewValues["DocNumber"] = txtDocNumber.Text;
        }
        protected void InitControls()
        {
            foreach (var item in frmlayout1.Items)
            {
                if (item is LayoutGroupBase)
                    (item as LayoutGroupBase).ForEach(GetNestedControls);
            }
        }
        protected void GetNestedControls(LayoutItemBase item)
        {
            if (item is LayoutItem)
                SetViewState(item as LayoutItem);

        }
        protected void SetViewState(LayoutItem c)
        {
            foreach (Control control in c.Controls)
            {
                ASPxEdit editor = null;
                ASPxButton editor2 = null;
                if (control.GetType().ToString() == "DevExpress.Web.ASPxButton")
                {
                    editor2 = control as ASPxButton;
                    if (editor2.ID == "btnPISDetail")
                    {
                        ButtonLoad(editor2);
                    }
                    else if (editor2.ID == "btnStepTempDetail")
                    {
                        ButtonLoad_3(editor2);
                    }
                    else if (editor2.ID == "btnMultiPrintStep")
                    {
                        btnMultiPrintStep.ClientEnabled = true;
                    }
                    else
                    {
                        ButtonLoad_2(editor2);
                    }
                }
                else
                {
                    editor = control as ASPxEdit;
                }

                if (editor != null)
                {
                    if (editor.ID == "txtHField1" || editor.ID == "txtHField2" || editor.ID == "txtHField3"
                        || editor.ID == "txtHField4" || editor.ID == "txtHField5" || editor.ID == "txtHField6" || editor.ID == "txtHField7"
                        || editor.ID == "txtHField8" || editor.ID == "txtHField9")
                    {
                        TextboxLoad(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup" && editor.ID == "aglCustomerCode")
                    {
                        Customer_LookupLoad(editor);
                    }
                    //else if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup" && editor.ID != "aglStepTemplate")
                    //{
                    //    LookupLoad2(editor);
                    //}
                    else if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup" && editor.ID != "aglStepTemplate")
                    {
                        LookupLoad(editor);
                    }
                       else if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup" && editor.ID == "aglStepTemplate")
                    {
                        LookupLoad3(editor);
                    } 
                    //if (editor.ID == "speSRP")
                    //{
                    //    SRP_Load(editor);
                    //}
                    if (editor.ID == "chkIsMultiIn")
                    {
                        CheckBoxLoad(editor);
                    }
                    if (editor.ID == "dtpDocDate" || editor.ID == "dtpDueDate" || editor.ID == "dtpProdDate")
                    {
                        Date_Load(editor);
                    }
                    if (editor.ID == "dtpSODueDate")
                    {
                        SODueDate_Load(editor);
                    }
                    if (editor.ID == "speLeadtime")
                    {
                        SpinEdit_Load(editor);
                    }
                    if (editor.ID == "memRemarks")
                    {
                        MemoLoad(editor);
                    }
                }
            }
        }
        protected void bItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("bItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                //sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInActive,0) = 0";
                gridLookup.DataSource = GetDataTableFromCacheOrDatabase();
                gridLookup.DataBind();
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("bItemCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                //string changes = param.Split(';')[0];
                string itemcode = param.Split(';')[1];
                if (string.IsNullOrEmpty(itemcode)) return;

                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable countitem = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode, A.FullDesc, A.UnitBase, CONVERT(decimal(15,2),ISNULL(C.AllowancePerc,0)) AS Allowance, ISNULL(A.EstimatedCost,0) AS EstimatedCost FROM Masterfile.Item A " +
                "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode LEFT JOIN Masterfile.ProductCategorySub C ON A.ProductSubCatCode = C.ProductSubCatCode WHERE A.ItemCode = '" + itemcode +
                "' AND (ISNULL(A.IsInActive,0) = 0 OR ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                DataTable itemdesc = Gears.RetriveData2("SELECT DISTINCT FullDesc, UnitBase, CONVERT(decimal(15,2),ISNULL(C.AllowancePerc,0)) AS Allowance, ISNULL(A.EstimatedCost,0) AS EstimatedCost FROM Masterfile.Item A LEFT JOIN Masterfile.ProductCategorySub C ON A.ProductSubCatCode = C.ProductSubCatCode  WHERE ItemCode = '" + itemcode +
                                                          "' AND ISNULL(A.IsInActive,0) = 0", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntsze = countsize.Rows.Count;

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["Allowance"].ToString() + ";";
                        codes += dt["EstimatedCost"].ToString() + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntclr > 1 && cntcls > 1 && cntsze == 1)
                    {
                        codes = ";;";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";

                    }
                    else if (cntclr > 1 && cntcls == 1 && cntsze > 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                    else if (cntclr == 1 && cntcls > 1 && cntsze > 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";

                    }
                    else
                    {
                        codes = ";;;";
                        codes += itemdesc.Rows[0]["FullDesc"].ToString() + ";";
                        codes += itemdesc.Rows[0]["UnitBase"].ToString() + ";";
                        codes += itemdesc.Rows[0]["Allowance"].ToString() + ";";
                        codes += itemdesc.Rows[0]["EstimatedCost"].ToString() + ";";
                    }
                }

                gridLookup.GridView.JSProperties["cp_BOMcodes"] = codes;
            }
        }
        public DataTable GetDataTableFromCacheOrDatabase()
        {
            DataTable dataTable = Context.Items["JOsdsItem" + hidcon["key"]] as DataTable;
            if (dataTable == null)
            {
                //if (Session["itemJOsql" + hidcon["key"]] != null && !Request.Params["__CALLBACKID"].Contains("bItemCode"))
                //{
                //    dataTable = Gears.RetriveData2(Session["itemJOsql" + hidcon["key"]].ToString(), Session["ConnString"].ToString());
                //}
                //else
                {
                    dataTable = Gears.RetriveData2("SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] A INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsAsset,0) = 0", Session["ConnString"].ToString());
                }
                Context.Items["JOsdsItem" + hidcon["key"]] = dataTable;
            }
            return dataTable;
        }

        protected void gvStepPlanning_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Name = "MultiPrint";
            col.ShowSelectCheckbox = true;
            col.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;
            col.VisibleIndex = 26;
            col.Width = 50;
            //col.FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns.Add(col);
        }

        protected void callbacker_Callback(object source, CallbackEventArgs e)
        {
            if (e.Parameter.Split('|')[0] == "clear")
            {
                Session["itemJOsql" + hidcon["key"]] = null;
                Context.Items["JOsdsItem" + hidcon["key"]] = null;
            }
            else
            {
                Session["itemJOsql" + hidcon["key"]] = "select distinct ItemCode, FullDesc from masterfile.Item " +
                                                "where ItemCode = '" + e.Parameter.Split('|')[1] + "'";
            }
            


            
            string param = e.Parameter.Split('|')[0];
            switch (param)
            {
                //Size Horizontal
                case "CallbackSize":

                    cp.JSProperties["cp_closeSH"] = true;

                    Session["SOitem"] = null;
                    Session["SOclr"] = null;
                    Session["SOcls"] = null;
                    Session["SOqty"] = null;
                    Session["SOprice"] = null;
                    Session["SOunit"] = null;
                    Session["SOdesc"] = null;
                    Session["SObulk"] = null;
                    Session["SOisbulk"] = null;
                    break;

                case "CallbackSizeHorizontal":

                    Session["SOitem"] = e.Parameter.Split('|')[1];
                    Session["SOclr"] = e.Parameter.Split('|')[2];
                    Session["SOcls"] = e.Parameter.Split('|')[3];
                    Session["SOqty"] = e.Parameter.Split('|')[4];
                    Session["SOprice"] = e.Parameter.Split('|')[5];
                    Session["SOunit"] = e.Parameter.Split('|')[6];
                    Session["SOdesc"] = e.Parameter.Split('|')[7];
                    Session["SObulk"] = e.Parameter.Split('|')[8];
                    Session["SOisbulk"] = e.Parameter.Split('|')[9];

                    sdsSizeHorizontal.ConnectionString = Session["ConnString"].ToString();
                    sdsSizeHorizontal.SelectCommand = "SELECT CONVERT(varchar(MAX),'" + txtDocNumber.Text + "') AS DocNumberX, '" + Session["SOitem"].ToString() + "' AS ItemCodeX, '" + Session["SOclr"].ToString() + "' AS ColorCodeX, '" + Session["SOcls"].ToString() + "' AS ClassCodeX, "
                        + " CONVERT(varchar(MAX),'" + Session["SOunit"].ToString() + "') AS UnitX, CONVERT(decimal(15,2), " + Session["SOqty"].ToString() + ") AS OrderQtyX, CONVERT(decimal(15,2)," + Session["SOprice"].ToString() + ") AS UnitPriceX, CONVERT(varchar(MAX),'" + Session["SOdesc"] + "') AS FullDescX, "
                        + " CONVERT(varchar(MAX),'" + Session["SObulk"].ToString() + "') AS BulkUnitX, CONVERT(bit,'" + Session["SOisbulk"].ToString() + "') AS IsByBulkX";

                    gvSizeHorizontal.DataSourceID = "sdsSizeHorizontal";
                    if (gvSizeHorizontal.DataSource != null)
                    {
                        gvSizeHorizontal.DataSource = null;
                    }
                    gvSizeHorizontal.DataBind();


                    sdsSizes.ConnectionString = Session["ConnString"].ToString();
                    gvSizes.DataSourceID = "sdsSizes";
                    if (gvSizes.DataSource != null)
                    {
                        gvSizes.DataSource = null;
                    }
                    gvSizes.DataBind();

                    gvSizes.AutoGenerateColumns = false;

                    DataTable getSizeCode = Gears.RetriveData2("SELECT A.SizeCode FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE ISNULL(A.IsInactive,0) = 0 AND ItemCode = '" + Session["SOitem"].ToString() +
                        "' AND ColorCode = '" + Session["SOclr"].ToString() + "' AND ClassCode = '" + Session["SOcls"].ToString() + "' ORDER BY SizeType, SortOrder", Session["ConnString"].ToString());


                    string query = "";

                    int x = 1;

                    foreach (DataRow dtsize in getSizeCode.Rows)
                    {
                        GridViewDataSpinEditColumn spin = new GridViewDataSpinEditColumn();
                        spin.FieldName = dtsize[0].ToString();
                        spin.VisibleIndex = x;
                        spin.Width = 50;
                        spin.ReadOnly = false;
                        spin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                        spin.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                        spin.PropertiesSpinEdit.DisplayFormatString = "{0:N}";
                        spin.PropertiesSpinEdit.ClientSideEvents.ValueChanged = "CalculateSize";
                        query += ", CONVERT(decimal(15,2),0) AS [" + dtsize[0].ToString() + "]";
                        gvSizes.Columns.Add(spin);
                        x++;
                    }
                    gvSizes.CancelEdit();

                    sdsSizes.SelectCommand = "SELECT CONVERT(varchar(MAX),'" + txtDocNumber.Text + "') AS DocNumberX" + query;
                    sdsSizes.DataBind();
                    gvSizes.DataSourceID = "sdsSizes";
                    if (gvSizes.DataSource != null)
                    {
                        gvSizes.DataSource = null;
                    }
                    gvSizes.DataBind();

                    cp.JSProperties["cp_RefreshSizeGrid"] = true;

                    break;
            }
        }
        protected void ButtonLoad(object sender, EventArgs e)
        {
            var button = sender as ASPxButton;

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                button.Enabled = false;
            }
            else
            {
                button.Enabled = true;
            }
        }
    }
}