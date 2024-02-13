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
using GearsAccounting;

namespace GWL
{
    public partial class frmRecurringJV : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.RecurringJV _Entity = new RecurringJV();//Calls entity 

        #region page load/entry

        private static string Connection;

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

            //Details checking. changes datasourceid of gridview if there's no details for a certain DocNumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            popup.ShowOnPageLoad = false;

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["RJVFilterExpression"] = null;

                txtDoc.Value = Request.QueryString["DocNumber"].ToString();

                _Entity.getdata(txtDoc.Text, Session["ConnString"].ToString());
                dtpStart.Text = String.IsNullOrEmpty(_Entity.DateStart) ? "" : Convert.ToDateTime(_Entity.DateStart).ToShortDateString();
                txtCycle.Value = _Entity.Cycle;
                txtDesc.Value = _Entity.Description;
                cboInterval.Value = _Entity.Interval;
                chkIsInactive.Value = _Entity.IsInactive;
                speTotalDebit.Value = _Entity.TotalDebit;
                speTotalCredit.Value = _Entity.TotalCredit;

                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;

                txtAddedDate.Text = _Entity.AddedDate;
                txtLastEditedDate.Text = _Entity.LastEditedDate;
                txtAddedBy.Text = _Entity.AddedBy;
                txtLastEditedBy.Text = _Entity.LastEditedBy;
                txtSubmittedDate.Text = _Entity.SubmittedDate;
                txtSubmittedBy.Text = _Entity.SubmittedBy;
                txtActivatedDate.Text = _Entity.ActivatedDate;
                txtActivatedBy.Text = _Entity.ActivatedBy;
                txtDeactivatedDate.Text = _Entity.DeactivatedDate;
                txtDeactivatedBy.Text = _Entity.DeactivatedBy;

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

                DataTable checkCount = Gears.RetriveData2("SELECT DocNumber FROM Accounting.RecurringJVDetail WHERE DocNumber = '" + txtDoc.Text + "'", Connection);
                gv1.DataSourceID = (checkCount.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GAccounting.RecurringJV_Validate(gparam);
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
        protected void memoremarks_Load(object sender, EventArgs e)
        {
            ((ASPxMemo)sender).ReadOnly = view;
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
            combobox.DropDownButton.Enabled = !view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.DropDownButton.Enabled = !view;
                look.ReadOnly = view;
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
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
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
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

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["RJVFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsSubsiCode";
                sdsSubsiCode.FilterExpression = Session["RJVFilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string code = e.Parameters.Split('|')[1];//Set Item Code
            var gridlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("SubsiGetCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[4], "glSubsiCode");
                var selectedValues = code;
                CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { code });
                sdsSubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["RJVFilterExpression"] = sdsSubsiCode.FilterExpression;
                grid.DataSourceID = "sdsSubsiCode";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    var col = "SubsiCode";
                    if (grid.GetRowValues(i, col) != null)
                        if (grid.GetRowValues(i, col).ToString() == code)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, col).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDoc.Text;
            _Entity.DateStart = dtpStart.Text;
            _Entity.Cycle = txtCycle.Text;
            _Entity.Description = txtDesc.Text;
            _Entity.Interval = String.IsNullOrEmpty(cboInterval.Text) ? null : cboInterval.Value.ToString();
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Value.ToString());
            _Entity.TotalDebit = String.IsNullOrEmpty(speTotalDebit.Text) ? 0 : Convert.ToDecimal(speTotalDebit.Value.ToString());
            _Entity.TotalCredit = String.IsNullOrEmpty(speTotalCredit.Text) ? 0 : Convert.ToDecimal(speTotalCredit.Value.ToString());

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
                case "Update":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set SELECT parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid

                        Validate();

                        cp.JSProperties["cp_message"] = (e.Parameter == "Add" ? "Successfully Added!" : "Successfully Updated!");
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
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;

            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        //dictionary method to hold error 
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

        protected void dtpStart_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpStart.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        public void glAccountCode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView lookup = sender as ASPxGridView;
            string[] trans = e.Parameters.Split('|');
            string column = e.Parameters.Split('|')[0];//Set column name
            if (e.Parameters.Contains("GLP_AC"))
            {
                lookup.JSProperties["cp_ac"] = "true";
            }
        }

        protected void glAccountCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glAccountCode_CustomCallback);
        }
    }
}