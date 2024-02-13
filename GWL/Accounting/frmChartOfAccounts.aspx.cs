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
using GearsAccounting;

namespace GWL
{
    public partial class frmChartOfAccounts : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string ErrorMsg = "";
        private static string Connection;

        Entity.ChartOfAccounts _Entity = new ChartOfAccounts();//Calls entity Customer
 
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            string referer;
            try //Validation to restrict user to browse/type directly to browser's address
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

            Connection = Session["ConnString"].ToString();

            if (!IsPostBack)
            {
                Session["COAAccountFr"] = "0";
                Session["COAAccountTo"] = "0";
                Session["COAFilterExpression"] = null;
                Session["COABalAmount"] = null;
                
                txtAccount.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtAccount.Text, Session["ConnString"].ToString());

                txtAccount.Value = _Entity.AccountCode;
                txtDesc.Value = _Entity.Description;
                gvGroupCode.Value = _Entity.GroupCode;
                txtExternal.Value = _Entity.ExternalCode;
                chkConfi.Value = _Entity.Confidential;
                speBalAmt.Value = _Entity.GLBalanceAmount;
                Session["COABalAmount"] = _Entity.GLBalanceAmount;
                chkControl.Value = _Entity.ControlAccount;
                cboApproach.Value = _Entity.BudgetApproach;
                cboCoverage.Value = _Entity.BudgetCoverage;
                cboLevel.Value = _Entity.BudgetLevel;
                chkAmortization.Value = _Entity.AmortizationAccount;
                chkCash.Value = _Entity.CashAccount;
                chkDebit.Value = _Entity.IsDebit;
                chkAllowJV.Value = _Entity.AllowJV;
                chkIsInactive.Value = _Entity.IsInactive;
                cboOpex.Value = _Entity.TypeOpex;
                glCost.Value = _Entity.FixedCostCenter;
                chkBudget.Value = _Entity.IsBudget;

                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;

                txtAddedBy.Text = _Entity.AddedBy;
                txtAddedDate.Text = _Entity.AddedDate;
                txtLastEditedBy.Text = _Entity.LastEditedBy;
                txtLastEditedDate.Text = _Entity.LastEditedDate;
                txtActivatedBy.Text = _Entity.ActivatedBy;
                txtActivatedDate.Text = _Entity.ActivatedDate;
                txtDeActivatedBy.Text = _Entity.DeactivatedBy;
                txtDeActivatedDate.Text = _Entity.DeactivatedDate;

                if (Convert.ToBoolean(chkConfi.Value) == true)
                {
                    speBalAmt.ClientVisible = false;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        cboApproach.ClientEnabled = false;
                        cboCoverage.ClientEnabled = false;
                        cboLevel.ClientEnabled = false;
                        //glcheck.ClientVisible = false;
                        glcheck.ClientVisible = false;
                        DisableFields();
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        gvGroupCode.ClientEnabled = false;
                        chkDebit.ClientEnabled = false;


                        //ADDED FOR TICKET #27953
                        chkControl.ClientEnabled = false;
                        chkAmortization.ClientEnabled = false;
                        chkIsInactive.ClientEnabled = false;
                        chkAllowJV.ClientEnabled = false;
                        chkDebit.ClientEnabled = false;
                        //readonly true
                        chkControl.ReadOnly = true;
                        chkAmortization.ReadOnly = true;
                        chkIsInactive.ReadOnly = true;
                        chkAllowJV.ReadOnly = true;
                        chkDebit.ReadOnly = true;
                        //chkAmortization.ClientEnabled = false;
                        DisableFields();
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
                    //for storing special acces to string Acess
                    string Access = Common.Common.GetAccess(Session["userid"].ToString(), Request.QueryString["transtype"].ToString(), Session["ConnString"].ToString());
                    //checking if the current sesion have the special permission
                    if (Access.Contains("S") && (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E"))
                    {
                        //client enable true
                        chkControl.ClientEnabled = true;
                        chkAmortization.ClientEnabled = true;
                        chkIsInactive.ClientEnabled = true;
                        chkAllowJV.ClientEnabled = true;
                        chkDebit.ClientEnabled = true;
                        //readonly false
                        chkControl.ReadOnly = false;
                        chkAmortization.ReadOnly = false;
                        chkIsInactive.ReadOnly = false;
                        chkAllowJV.ReadOnly = false;
                        chkDebit.ReadOnly = false;
                    }
                gv1.KeyFieldName = "AccountCode;SubsiCode";

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    DataTable checkCount = Gears.RetriveData2("SELECT AccountCode FROM Accounting.GLSubsicode WHERE AccountCode = '" + txtAccount.Text + "'", Connection);
                    gv1.DataSourceID = (checkCount.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                }
                else
                {
                    gv1.DataSourceID = "sdsDetail";
                }

            }

        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.AccountCode;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GAccounting.ChartOfAccounts_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void OtherCheckBox(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            string Access = Common.Common.GetAccess(Session["userid"].ToString(), Request.QueryString["transtype"].ToString(), Session["ConnString"].ToString());
            if (Access.Contains("S") && (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E"))
            {
                if (Convert.ToDecimal(Session["COABalAmount"].ToString()) != 0)
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
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (!IsPostBack)
            {
                ASPxCheckBox text = sender as ASPxCheckBox;
                text.ReadOnly = view;
            }
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
            combobox.DropDownButton.ClientVisible = !view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
                look.DropDownButton.ClientVisible = !view;
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
                    look.DropDownButton.ClientVisible = false;
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
            if (spinedit.Text == "")
            {
                spinedit.ReadOnly = false;
            }
            else
            {
                spinedit.ReadOnly = true;
            }
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
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

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "Delete")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); 
            _Entity.AccountCode = txtAccount.Text;
            _Entity.Description = txtDesc.Text;
            _Entity.GroupCode = String.IsNullOrEmpty(gvGroupCode.Text) ? "" : gvGroupCode.Value.ToString();
            _Entity.ExternalCode = txtExternal.Text;
            _Entity.Confidential = Convert.ToBoolean(chkConfi.Value.ToString());
            _Entity.GLBalanceAmount = String.IsNullOrEmpty(speBalAmt.Text) ? 0 : Convert.ToDecimal(speBalAmt.Text);
            _Entity.ControlAccount = Convert.ToBoolean(chkControl.Value.ToString());
            _Entity.IsBudget = Convert.ToBoolean(chkBudget.Value.ToString());
            _Entity.BudgetApproach = String.IsNullOrEmpty(cboApproach.Text) ? "" : cboApproach.Value.ToString();
            _Entity.BudgetCoverage = String.IsNullOrEmpty(cboCoverage.Text) ? "" : cboCoverage.Value.ToString();
            _Entity.BudgetLevel = String.IsNullOrEmpty(cboLevel.Text) ? "" : cboLevel.Value.ToString();
            _Entity.AmortizationAccount = Convert.ToBoolean(chkAmortization.Value.ToString());
            _Entity.CashAccount = Convert.ToBoolean(chkCash.Value.ToString());
            _Entity.IsDebit = Convert.ToBoolean(chkDebit.Value.ToString());
            _Entity.AllowJV = Convert.ToBoolean(chkAllowJV.Value.ToString());
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Value.ToString());
            _Entity.TypeOpex = String.IsNullOrEmpty(cboOpex.Text) ? "" : cboOpex.Value.ToString();
            _Entity.FixedCostCenter = String.IsNullOrEmpty(glCost.Text) ? "" : glCost.Value.ToString();

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
            
            switch (e.Parameter)
            {
                case "Add":

                    CheckAccountCode();

                    if (error == false)
                    {
                        gv1.UpdateEdit();
                        check = true;
                        _Entity.AddedBy = Session["userid"].ToString();
                       
                        _Entity.InsertData(_Entity);

                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["AccountCode"].DefaultValue = txtAccount.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        Validate();
                        
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = ErrorMsg;
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    gv1.UpdateEdit();

                    CheckAccountCode();

                    if (error == false)
                    {
                        check = true;
			            _Entity.UpdateData(_Entity);

                        gv1.KeyFieldName = "AccountCode;SubsiCode";
                        gv1.DataSourceID = "odsDetail";
                        gv1.UpdateEdit();

                        Validate();

                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = ErrorMsg;
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Delete":
                    check = true;
                    cp.JSProperties["cp_delete"] = true;
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                    cp.JSProperties["cp_success"] = true;
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
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
                case "Group":

                    DataTable group = Gears.RetriveData2("SELECT AccountFr,AccountTo from Accounting.GLAccountGroup where GroupCode = '" + gvGroupCode.Text + "' ", Session["ConnString"].ToString());
                    foreach (DataRow dtrow in group.Rows)
                    {
                        //cp.JSProperties["cp_AccountFr"] = dtrow[0].ToString();
                        //cp.JSProperties["cp_AccountTo"] = dtrow[1].ToString();
                        Session["COAAccountFr"] = group.Rows[0]["AccountFr"].ToString();
                        Session["COAAccountTo"] = group.Rows[0]["AccountTo"].ToString();
                    }
                    
                    //cp.JSProperties["cp_group"] = true;
                    //cp.JSProperties["cp_message"] = "Account Code not in Group Code range!";
                    //Session["Refresh"] = "1";
                    //DataView tranpl = (DataView)pl.Select(DataSourceSelectArguments.Empty);
                    //if (tranpl.Count > 0)
                    //{
                    //    glRevSubsi.Text = tranpl[0][1].ToString();
                    //}
                    break;
            }

        }
        public void CheckAccountCode()
        {
            DataTable group = Gears.RetriveData2("SELECT AccountFr,AccountTo from Accounting.GLAccountGroup where GroupCode = '" + gvGroupCode.Text + "' ", Session["ConnString"].ToString());
            foreach (DataRow dtrow in group.Rows)
            {
                //cp.JSProperties["cp_AccountFr"] = dtrow[0].ToString();
                //cp.JSProperties["cp_AccountTo"] = dtrow[1].ToString();
                Session["COAAccountFr"] = group.Rows[0]["AccountFr"].ToString();
                Session["COAAccountTo"] = group.Rows[0]["AccountTo"].ToString();

                if ((Convert.ToInt32(txtAccount.Text) < Convert.ToInt32(Session["COAAccountFr"].ToString())) ||
                    (Convert.ToInt32(txtAccount.Text) > Convert.ToInt32(Session["COAAccountTo"].ToString())))
                {
                    error = true;
                    ErrorMsg = "Account code must be in the range " + Session["COAAccountFr"].ToString() + "-" + Session["COAAccountTo"].ToString() + ".";
                }
                else
                {
                    ErrorMsg = "Please check all fields!";
                }
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
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        private void DisableFields()
        {
            if (chkBudget.Checked == false)
            {
                cboApproach.ClientEnabled = false;
                cboCoverage.ClientEnabled = false;
                cboLevel.ClientEnabled = false;

                cboApproach.Text = "";
                cboCoverage.Text = "";
                cboLevel.Text = "";
            }
            else
            {
                cboApproach.ClientEnabled = true;

                var Approach = cboApproach.Text;

                if (Approach == "ByPass")
                {
                    cboCoverage.ClientEnabled = false;
                    cboLevel.ClientEnabled = false;
                    cboCoverage.Text = "";
                    cboLevel.Text = "";
                }
                else
                {
                    cboCoverage.ClientEnabled = true;
                    cboLevel.ClientEnabled = true;
                }
            }

            if (chkAmortization.Checked == true)
            {
                chkControl.ClientEnabled = false;
                chkControl.Checked = false;
            }
            else
            {
                chkControl.ClientEnabled = true;
            }    
        }
        //protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        //{

        //}

        //protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        //{

        //}

        //protected void glBizPartnerCode_TextChanged(object sender, EventArgs e)
        //{

        //}

        //protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void txtPlant_TextChanged(object sender, EventArgs e)
        //{

        //}
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IsInactive"] = false;
        }

        //protected void chkControl_Load(object sender, EventArgs e)
        //{

        //}

        //protected void txtAccount_Init(object sender, EventArgs e)
        //{
            
        //}
    }
}