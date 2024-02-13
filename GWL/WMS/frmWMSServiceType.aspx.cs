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


namespace GWL
{
    public partial class frmWMSServiceType : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.WMSServiceType _Entity = new WMSServiceType();//Calls entity odsHeader

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
                Session["servt"] = null;
                Session["servt2"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        //glcheck.ClientVisible = false;
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
                    txtCode.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    //_Entity.getdata(txtCode.Text); //Method for retrieving data from entity
                    _Entity.getdata(txtCode.Text, Session["ConnString"].ToString());//ADD CONN

                    txtCode.Value = _Entity.ServiceType;
                    cboServType.Value = _Entity.Type;
                    txtDesc.Value = _Entity.Description;
                    txtSeq.Value = _Entity.SequenceNumber;
                    txtRate.Value = _Entity.ServiceRate;
                    cboCat.Value = _Entity.ServiceTypeCatCode;
                    glSalesGL.Value = _Entity.SalesGLCode;
                    glSalesSubsi.Value = _Entity.SalesGLSubsiCode;
                    glARGL.Value = _Entity.ARGLCode;
                    glARSubsi.Value = _Entity.ARGLSubsiCode;
                    chkStandard.Value = _Entity.IsStandard;
                    chkIsInactive.Value = _Entity.IsInactive;

                    txtField1.Text = _Entity.Field1;
                    txtField2.Text = _Entity.Field2;
                    txtField3.Text = _Entity.Field3;
                    txtField4.Text = _Entity.Field4;
                    txtField5.Text = _Entity.Field5;
                    txtField6.Text = _Entity.Field6;
                    txtField7.Text = _Entity.Field7;
                    txtField8.Text = _Entity.Field8;
                    txtField9.Text = _Entity.Field9;


                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeactivatedBy;
                    txtDeActivatedDate.Text = _Entity.DeactivatedDate;
                }

            }

            
        }
        #endregion

        #region Validation
        private void Validate()
        {
        //    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        //    gparam._DocNo = _Entity.DocNumber;
        //    gparam._UserId = Session["Userid"].ToString();
        //    gparam._TransType = "WMSTRN";

        //    string strresult =GWarehouseManagement.WMSServiceType_Validate(gparam);
            

        //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

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

            _Entity.ServiceType = String.IsNullOrEmpty(txtCode.Text) ? null : txtCode.Value.ToString();
            _Entity.Type = String.IsNullOrEmpty(cboServType.Text) ? null : cboServType.Value.ToString();
            _Entity.Description = txtDesc.Text;
            _Entity.SequenceNumber = txtSeq.Text;
            _Entity.ServiceRate = String.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
            _Entity.ServiceTypeCatCode = String.IsNullOrEmpty(cboCat.Text) ? null : cboCat.Value.ToString();
            _Entity.SalesGLCode = String.IsNullOrEmpty(glSalesGL.Text) ? null : glSalesGL.Value.ToString();
            _Entity.SalesGLSubsiCode = String.IsNullOrEmpty(glSalesSubsi.Text) ? null : glSalesSubsi.Value.ToString();
            _Entity.ARGLCode = String.IsNullOrEmpty(glARGL.Text) ? null : glARGL.Value.ToString(); 
            _Entity.ARGLSubsiCode = String.IsNullOrEmpty(glARSubsi.Text) ? null : glARSubsi.Value.ToString();
            _Entity.IsStandard = Convert.ToBoolean(chkStandard.Checked);
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Checked);

            _Entity.Field1 = txtField1.Text;
            _Entity.Field2 = txtField2.Text;
            _Entity.Field3 = txtField3.Text;
            _Entity.Field4 = txtField4.Text;
            _Entity.Field5 = txtField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtField6.Text) ? null : txtField6.Value.ToString();
            _Entity.Field7 = String.IsNullOrEmpty(txtField7.Text) ? null : txtField7.Value.ToString();
            _Entity.Field8 = txtField8.Text;
            _Entity.Field9 = txtField9.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();



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
                        _Entity.AddedBy = Session["userid"].ToString();
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
                    pl.SelectCommand = string.Format("SELECT SubsiCode,Description from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode = '" + glSalesGL.Text + "'");
                    glSalesSubsi.DataSourceID = "SubsiCode";
                    glSalesSubsi.DataBind();
                    //DataView tranpl = (DataView)pl.Select(DataSourceSelectArguments.Empty);
                    //if (tranpl.Count > 0)
                    //{
                    //    glRevSubsi.Text = tranpl[0][1].ToString();
                    //}
                    break;
                case "Account2":
                    SqlDataSource pl2 = SubsiCode;
                    pl2.SelectCommand = string.Format("SELECT SubsiCode,Description from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode = '" + glARGL.Text + "'");
                    glARSubsi.DataSourceID = "SubsiCode2";
                    glARSubsi.DataBind();
                    //DataView tranpl = (DataView)pl.Select(DataSourceSelectArguments.Empty);
                    //if (tranpl.Count > 0)
                    //{
                    //    glRevSubsi.Text = tranpl[0][1].ToString();
                    //}
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
        #endregion

        protected void glRevSubsi_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
            if (Session["servt"] != null)
            {
                gridLookup.GridView.DataSourceID = SubsiCode.ID;
                SubsiCode.FilterExpression = Session["servt"].ToString();
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
            Session["servt2"] = SubsiCode.FilterExpression;
            grid.DataSourceID = SubsiCode.ID;
            grid.DataBind();
        }

        protected void glRevSubsi2_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList2_CustomCallback);
            if (Session["servt2"] != null)
            {
                gridLookup.GridView.DataSourceID = SubsiCode2.ID;
                SubsiCode2.FilterExpression = Session["servt2"].ToString();
            }
        }

        public void glPickList2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {



            string whcode = e.Parameters;//Set column name
            if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { whcode });
            SubsiCode2.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["servt2"] = SubsiCode2.FilterExpression;
            grid.DataSourceID = SubsiCode2.ID;
            grid.DataBind();
        }

    
        protected void Connection_init(object sender, EventArgs e)
        {
         // sdsDetail.ConnectionString = Session["ConnString"].ToString();
           // sdsItem.ConnectionString = Session["ConnString"].ToString();
           // SubsiCode.ConnectionString = Session["ConnString"].ToString();
           // SubsiCode2.ConnectionString = Session["ConnString"].ToString();
           // ServiceType.ConnectionString = Session["ConnString"].ToString();
           // SalesGLCode.ConnectionString = Session["ConnString"].ToString();
           // ARGLCode.ConnectionString = Session["ConnString"].ToString();
           // UnitOfMeasure.ConnectionString = Session["ConnString"].ToString();
           // Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
           // Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
           // Bizpartner.ConnectionString = Session["ConnString"].ToString();
	((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}