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
    public partial class frmItemCategory : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.ItemCategory _Entity = new ItemCategory();//Calls entity odsHeader

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            GLSubsiCode.SelectCommand = "Select SubsiCode, Description from Accounting.GLSubsiCode where AccountCode = '" + gvInvGL.Text + "'";

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
                    txtItemCat.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                   // _Entity.getdata(txtItemCat.Text); //Method for retrieving data from entity
                    _Entity.getdata(txtItemCat.Text, Session["ConnString"].ToString());//ADD CONN
                    txtItemCat.Text = _Entity.ItemCategoryCode;
                    txtDesc.Text = _Entity.Description;
                    txtItemForm.Text = _Entity.ItemCodeFormat;
                    gvInvGL.Value = _Entity.InventoryGLCode;
                    gvAdjGL.Value = _Entity.AdjustmentGLCode;
                    GLSubsiCode.SelectCommand = "Select SubsiCode, Description from Accounting.GLSubsiCode where AccountCode = '" + gvInvGL.Text + "'";
                    gvGLSubsi.DataBind();
                    gvGLSubsi.Value = _Entity.GLSubsiCode;
                    txtThres.Text = _Entity.Threshold.ToString();
                    cboCost.Value = _Entity.CostingMethod;
                    gvBase.Value = _Entity.BaseUnit;
                    gvBulk.Value = _Entity.BulkUnit;
                    chkAllowNega.Value = _Entity.IsAllowNega;
                    chkAllowZero.Value = _Entity.AllowZeroCost;
                    chkForecasted.Value = _Entity.IsForecasted;
                    gvSalesGL.Value = _Entity.SalesGLCode;
                    gvSalesReturn.Value = _Entity.SalesReturnGLCode;
                    txtARGL.Value = _Entity.ARGLCode;
                    gvCostGL.Value = _Entity.CostOfGoodsGLCode;
                    gvAccGL.Value = _Entity.AccumulatedGLCode;
                    gvDepGL.Value = _Entity.DepreciationGLCode;
                    chkAsset.Value = _Entity.IsAsset;
                    txtAsset.Text = _Entity.AssetLife.ToString();
                    txtLookCap.Text = _Entity.LookUpCaption;
                    txtLookKey.Text = _Entity.LookUpKey;
                    chkReq.Value = _Entity.Required;
                    chkIsInactive.Value = _Entity.IsInactive;
                    chkIsStock.Value = _Entity.IsStock;
                    cboAllocation.Value = _Entity.Allocation;
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

            //string strresult =GWarehouseManagement.ItemCategory_Validate(gparam);
            

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
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
            _Entity.ItemCategoryCode = txtItemCat.Text;
            _Entity.Description = txtDesc.Text;
            _Entity.ItemCodeFormat = txtItemForm.Text;
            _Entity.InventoryGLCode = String.IsNullOrEmpty(gvInvGL.Text) ? null : gvInvGL.Value.ToString();
            _Entity.AdjustmentGLCode = String.IsNullOrEmpty(gvAdjGL.Text) ? null : gvAdjGL.Value.ToString();
            _Entity.GLSubsiCode = String.IsNullOrEmpty(gvGLSubsi.Text) ? null : gvGLSubsi.Value.ToString();
            _Entity.Threshold = String.IsNullOrEmpty(txtThres.Text) ? 0 : Convert.ToInt32(txtThres.Text);
            _Entity.CostingMethod = String.IsNullOrEmpty(cboCost.Text) ? null : cboCost.Value.ToString();
            _Entity.BaseUnit = String.IsNullOrEmpty(gvBase.Text) ? null : gvBase.Value.ToString();
            _Entity.BulkUnit = String.IsNullOrEmpty(gvBulk.Text) ? null : gvBulk.Value.ToString();
            _Entity.IsAllowNega = Convert.ToBoolean(chkAllowNega.Value.ToString());
            _Entity.AllowZeroCost = Convert.ToBoolean(chkAllowZero.Value.ToString());
            _Entity.IsForecasted = Convert.ToBoolean(chkForecasted.Value.ToString());
            _Entity.SalesGLCode = String.IsNullOrEmpty(gvSalesGL.Text) ? null : gvSalesGL.Value.ToString();
            _Entity.SalesReturnGLCode = String.IsNullOrEmpty(gvSalesReturn.Text) ? null : gvSalesReturn.Value.ToString();
            _Entity.ARGLCode = String.IsNullOrEmpty(txtARGL.Text) ? null : txtARGL.Value.ToString();
            _Entity.CostOfGoodsGLCode = String.IsNullOrEmpty(gvCostGL.Text) ? null : gvCostGL.Value.ToString();
            _Entity.AccumulatedGLCode = String.IsNullOrEmpty(gvAccGL.Text) ? null : gvAccGL.Value.ToString();
            _Entity.DepreciationGLCode = String.IsNullOrEmpty(gvDepGL.Text) ? null : gvDepGL.Value.ToString();
            _Entity.IsAsset = Convert.ToBoolean(chkAsset.Checked);
            _Entity.IsStock = Convert.ToBoolean(chkIsStock.Checked);
            _Entity.AssetLife = String.IsNullOrEmpty(txtAsset.Text) ? 0 : Convert.ToInt32(txtAsset.Text);
            _Entity.LookUpCaption = txtLookCap.Text;
            _Entity.LookUpKey = txtLookKey.Text;
            _Entity.Required = Convert.ToBoolean(chkReq.Value.ToString());
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Value.ToString());
            _Entity.Allocation = String.IsNullOrEmpty(cboAllocation.Text) ? null : cboAllocation.Value.ToString();
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
                case "glcode":
                    gvGLSubsi.DataBind();
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

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            GLSubsiCode.ConnectionString = Session["ConnString"].ToString();
            ChartOfAccount.ConnectionString = Session["ConnString"].ToString();
            Unit.ConnectionString = Session["ConnString"].ToString();
        }
        #endregion
        //protected void gvGLSubsi_Init(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
        //    if (Session["customoutbound"] != null)
        //    {
        //        gridLookup.GridView.DataSource = GLSubsiCode;
        //        GLSubsiCode.FilterExpression = Session["customoutbound"].ToString();
        //    }
        //}

        //public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{



        //    string whcode = e.Parameters;//Set column name
        //    if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

        //    ASPxGridView grid = sender as ASPxGridView;
        //    grid.DataSourceID = null;
        //    CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { whcode });
        //    GLSubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["customoutbound"] = GLSubsiCode.FilterExpression;
        //    grid.DataSource = GLSubsiCode;
        //    grid.DataBind();
        //}
    }
}