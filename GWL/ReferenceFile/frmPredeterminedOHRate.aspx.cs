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
using GearsWarehouseManagement;


namespace GWL
{
    public partial class frmPredeterminedOHRate : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.PredeterminedOHRate _Entity = new PredeterminedOHRate();//Calls entity odsHeader

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

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }

            

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }
            if (!IsPostBack)
            {
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }

            

                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //popup.ShowOnPageLoad = false;
                }
                else
                {
                    txtRate.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    _Entity.getdata(txtRate.Text, Session["ConnString"].ToString());//ADD CONN //Method for retrieving data from entity

                    txtRate.Text = _Entity.RateCode;
                    txtDesc.Text = _Entity.Description;
                    cboType.Value = _Entity.Type;
                    spOHRate.Value = _Entity.OHRate;
                    spBudgetOH.Value = _Entity.BudgetOverhead;
                    txtBudgetOHDesc.Text = _Entity.BudgetOHDesc;
                    spBudgetQtyAlloc.Value = _Entity.BudgetQtyAlloc;
                    txtBudgetQtyAllocDesc.Text = _Entity.BudgetQtyAllocDesc;
                    dtpEffectivity.Text = String.IsNullOrEmpty(_Entity.EffectivityDate) ? "" : Convert.ToDateTime(_Entity.EffectivityDate).ToShortDateString();
                    glAppliedGL.Value = _Entity.OHAppliedGLCode;
                    glAppliedSubsi.Value = _Entity.OHAppliedSubsiCode;
                    glAppliedCost.Value = _Entity.OHAppliedCostCenter;
                    glActualGL.Value = _Entity.OHActualGLCode;
                    glActualSubsi.Text = _Entity.OHActualSubsiCode;
                    glActualCost.Text = _Entity.OHActualCostCenter;
                    chkIsInactive.Value = _Entity.IsInactive;
                    
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeActivatedBy;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtApprovedDate.Text = _Entity.ApprovedDate;
                    txtApprovedBy.Text = _Entity.ApprovedBy;

                    SubsiCode.SelectCommand = "SELECT SubsiCode,Description from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode = '" + glAppliedGL.Text + "'";
                    glAppliedSubsi.Value = _Entity.OHAppliedSubsiCode;
                    ActualSubsiCode.SelectCommand = "SELECT SubsiCode,Description from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode = '" + glActualGL.Text + "'";
                    glActualSubsi.Value = _Entity.OHActualSubsiCode;

                    if(Request.QueryString["parameters"].ToString()=="OHEdit")
                    {
                        txtRate.ClientEnabled = false;
                        txtDesc.ClientEnabled = false;
                        glActualGL.ClientEnabled = false;
                        glActualSubsi.ClientEnabled = false;
                        glActualCost.ClientEnabled = false;
                        glAppliedGL.ClientEnabled = false;
                        glAppliedCost.ClientEnabled = false;
                        glAppliedSubsi.ClientEnabled = false;
                        chkIsInactive.ClientEnabled = false;

                    }

                    gvRef.DataSourceID = "odsKate";
                }

            }

            
        }
        #endregion

        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "REFLOC";

            //string strresult =GWarehouseManagement.PredeterminedOHRate_Validate(gparam);
            

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox text = sender as ASPxCheckBox;
            text.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            //Control look = (Control)sender;
            //((ASPxGridLookup)look).ReadOnly = view;
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {

                ASPxGridLookup look = sender as ASPxGridLookup;
                if (look != null)
                {
                    look.Enabled = false;
                }
            }
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
            if (Request.QueryString["entry"].ToString() == "V")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "Details")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.RateCode = txtRate.Text;
            _Entity.Description = txtDesc.Text;
            _Entity.Type = String.IsNullOrEmpty(cboType.Text) ? null : cboType.Value.ToString();
            _Entity.OHRate = String.IsNullOrEmpty(spOHRate.Text) ? 0 : Convert.ToDecimal(spOHRate.Text);
            _Entity.BudgetOverhead = String.IsNullOrEmpty(spBudgetOH.Text) ? 0 : Convert.ToDecimal(spBudgetOH.Text);
            _Entity.BudgetOHDesc = txtDesc.Text;
            _Entity.BudgetQtyAlloc = String.IsNullOrEmpty(spBudgetQtyAlloc.Text) ? 0 : Convert.ToDecimal(spBudgetQtyAlloc.Text);
            _Entity.BudgetQtyAllocDesc = txtBudgetQtyAllocDesc.Text;
            _Entity.EffectivityDate = dtpEffectivity.Text;
            _Entity.OHAppliedGLCode = String.IsNullOrEmpty(glAppliedGL.Text) ? null : glAppliedGL.Value.ToString();
            _Entity.OHAppliedSubsiCode = String.IsNullOrEmpty(glAppliedSubsi.Text) ? null : glAppliedSubsi.Value.ToString();
            _Entity.OHAppliedCostCenter = String.IsNullOrEmpty(glAppliedCost.Text) ? null : glAppliedCost.Value.ToString();
            _Entity.OHActualGLCode = String.IsNullOrEmpty(glActualGL.Text) ? null : glActualGL.Value.ToString();
            _Entity.OHActualSubsiCode = String.IsNullOrEmpty(glActualSubsi.Text) ? null : glActualSubsi.Value.ToString();
            _Entity.OHActualCostCenter = String.IsNullOrEmpty(glActualCost.Text) ? null : glActualCost.Value.ToString();
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Checked);
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();
            _Entity.ApprovedBy = txtApprovedBy.Text;

            //_Entity.AddedBy = txtAddedBy.Text;
            //_Entity.AddedDate = txtAddedDate.Text;
            //_Entity.ActivatedBy = txtActivatedBy.Text;
            //_Entity.ActivatedDate = txtActivatedDate.Text;
            //_Entity.DeActivatedBy = txtDeActivatedBy.Text;
            //_Entity.DeActivatedDate = txtDeActivatedDate.Text;



            switch (e.Parameter)
            {
                case "Add":

                    if (error == false)
                    {
                        check = true;
                        //_Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.InsertData(_Entity);
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
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
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of Updating header
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
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
                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;
                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
                case "Account":
                    SqlDataSource pl = SubsiCode;
                    pl.SelectCommand = string.Format("SELECT SubsiCode,Description from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode = '" + _Entity.OHAppliedGLCode + "'");
                    glAppliedSubsi.DataSourceID = "OHAppliedSubsiCode";
                    glAppliedGL.DataBind();
                    break;
                case "Rev":
                    SqlDataSource Rv = ActualSubsiCode;
                    Rv.SelectCommand = string.Format("SELECT SubsiCode,Description from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode= '" + _Entity.OHActualGLCode + "'");
                    glActualSubsi.DataSourceID = "OHActualSubsiCode";
                    glActualGL.DataBind();
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {

        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {

        }
        protected void dtpEffectivity_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpEffectivity.Date = DateTime.Now;
            }
        }
        #endregion
        protected void glRevSubsi_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
            if (Session["preOH"] != null)
            {
                gridLookup.GridView.DataSourceID = null;
                gridLookup.GridView.DataSource = SubsiCode;
                SubsiCode.FilterExpression = Session["preOH"].ToString();
            }
        }
        public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {



            string whcode = e.Parameters;//Set column name
            if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { whcode });
            SubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["preOH"] = SubsiCode.FilterExpression;
            grid.DataSource = SubsiCode;
            grid.DataBind();
        }
        protected void glRevAcc_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList2_CustomCallback);
            if (Session["preOH2"] != null)
            {
                gridLookup.GridView.DataSourceID = null;
                gridLookup.GridView.DataSource = ActualSubsiCode;
                ActualSubsiCode.FilterExpression = Session["preOH2"].ToString();
            }
        }

        public void glPickList2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string whcode2 = e.Parameters;//Set column name
            if (whcode2.Contains("GLP_AIC") || whcode2.Contains("GLP_AC") || whcode2.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { whcode2 });
            ActualSubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["preOH2"] = SubsiCode.FilterExpression;
            grid.DataSource = ActualSubsiCode;
            grid.DataBind();
        }

        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();

            AccountCode.ConnectionString = Session["ConnString"].ToString();
            Sub.ConnectionString = Session["ConnString"].ToString();
            CostCenterCode.ConnectionString = Session["ConnString"].ToString();
            PredeterminedOHRateType.ConnectionString = Session["ConnString"].ToString();
            SubsiCode.ConnectionString = Session["ConnString"].ToString();
            ActualSubsiCode.ConnectionString = Session["ConnString"].ToString();
            Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            ActualAccount.ConnectionString = Session["ConnString"].ToString();


        }
    }
}