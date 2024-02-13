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
using GearsAccounting;

namespace GWL
{
    public partial class frmCollection : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string Cascade = "";
        string TransNo = "";

        Entity.Collection _Entity = new Entity.Collection();//Calls entity odsHeader
        Entity.Collection.CollectionAdjustment _Adjustment = new Entity.Collection.CollectionAdjustment();
        Entity.Collection.CollectionApplication _Application = new Entity.Collection.CollectionApplication();
        Entity.Collection.CollectionChecks _Checks = new Entity.Collection.CollectionChecks();
        Entity.Collection.CollectionCredit _Credit = new Entity.Collection.CollectionCredit();


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

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            txtDocNumber.ReadOnly = true;
            gvRef.Columns["CommandString"].Width = 0;
            gvRef.Columns["RCommandString"].Width = 0;

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["appdetail"] = null;
                Session["ColRepDatatable"] = null;
                Session["ColRepProfit"] = null;
                Session["ColRepCost"] = null;
                Session["ColRepFilterExpression"] = null;
                Session["ColRepTransNo"] = null;
                Session["ColRepBizAcc"] = null;
                Session["ColRepCus"] = null;
                Session["ColRepChecks"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                txtName.Value = _Entity.Name.ToString();
                aglCollector.Value = _Entity.Collector.ToString();
                aglBizAccount.Value = _Entity.BizAccount.ToString();
                aglCurrency.Value = _Entity.Currency.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                speHCash.Value = _Entity.TotalCashAmount.ToString();
                speHCredit.Value = _Entity.TotalBankCredit.ToString();
                speHCheck.Value = _Entity.TotalCheckAmount.ToString();
                speHDue.Value = _Entity.TotalAmountDue.ToString();
                speHApplied.Value = _Entity.TotalApplied.ToString();
                speHAdjustment.Value = _Entity.TotalAdjustment.ToString();
                speHVariance.Value = _Entity.Variance.ToString();
                txtDCash.Value = _Entity.CashAmount.ToString();

                Filter();

                aglTransNo.Text = _Entity.RefTransDoc.ToString();
                chkIsForDeposit.Value = _Entity.IsForDeposit;

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
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;
                //}

                gvApplication.KeyFieldName = "RecordID";
                gvAdjustment.KeyFieldName = "LineNumber";
                gvChecks.KeyFieldName = "LineNumber";
                gvCredit.KeyFieldName = "LineNumber";
                popup.ShowOnPageLoad = true;

                //V=View, E=Edit, N=New
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
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //Check();
                        if (chkIsForDeposit.Checked == true)
                        {
                            txtDCash.Enabled = false;
                        }
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

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gvApplication.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvAdjustment.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvChecks.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    gvCredit.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }

                //Filter();
            }

            DataTable Application = new DataTable();
            DataTable Adjustment = new DataTable();
            DataTable Checks = new DataTable();
            DataTable Credit = new DataTable();

            Application = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            gvApplication.DataSourceID = (Application.Rows.Count > 0 ? "odsApplication" : "sdsApplication");

            Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            gvAdjustment.DataSourceID = (Adjustment.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");

            Checks = Gears.RetriveData2("SELECT * FROM Accounting.CollectionChecks WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            gvChecks.DataSourceID = (Checks.Rows.Count > 0 ? "odsChecks" : "sdsChecks");

            Credit = Gears.RetriveData2("SELECT * FROM Accounting.CollectionCredit WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            gvCredit.DataSourceID = (Credit.Rows.Count > 0 ? "odsCredit" : "sdsCredit");

            gvJournal.DataSourceID = "odsJournalEntry";

            //Filter();
            Check();
            GetProfitAndCost();
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCOL";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.Collection_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCOL";
            gparam._Table = "Accounting.Collection";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.Collection_Post(gparam);
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
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = true;
            }
        }

        protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = true;
            }
            else
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
        }

        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void Button_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton button = sender as ASPxButton;
            button.ClientVisible = !view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";

            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
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

        protected void gvReceipts_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";

            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;

            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.New ||
                    e.ButtonType == ColumnCommandButtonType.Delete)
                    e.Visible = false;
            }

            if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    if (chkIsForDeposit.Checked == true)
                    {
                        e.Visible = false;
                    }
                    else
                    {
                        e.Visible = true;
                    }
                }
            }
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

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "Delete" || e.ButtonID == "AdjustmentDelete" || e.ButtonID == "ApplicationDelete")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }

        protected void gvReceipts_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
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
                if (e.ButtonID == "Delete" || e.ButtonID == "CreditDelete" || e.ButtonID == "ChecksDelete")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }

            if (Request.QueryString["entry"] == "E")
            {
                if (e.ButtonID == "Delete" || e.ButtonID == "CreditDelete" || e.ButtonID == "ChecksDelete")
                {
                    if (chkIsForDeposit.Checked == true)
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["ColRepFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsSubsi";
                sdsSubsi.FilterExpression = Session["ColRepFilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string value = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";


            if (e.Parameters.Contains("SubsiCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gvAdjustment.FindEditRowCellTemplateControl((GridViewDataColumn)gvAdjustment.Columns[2], "glAccountCode");
                var selectedValues = value;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { value });
                sdsSubsi.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["ColRepFilterExpression"] = sdsSubsi.FilterExpression;
                grid.DataSourceID = "sdsSubsi";
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
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Value.ToString();
            _Entity.Name = String.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
            _Entity.Collector = String.IsNullOrEmpty(aglCollector.Text) ? null : aglCollector.Text;
            _Entity.BizAccount = String.IsNullOrEmpty(aglBizAccount.Text) ? null : aglBizAccount.Value.ToString();
            _Entity.Currency = String.IsNullOrEmpty(aglCurrency.Text) ? "PHP" : aglCurrency.Value.ToString();
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.TotalCashAmount = String.IsNullOrEmpty(speHCash.Text) ? 0 : Convert.ToDecimal(speHCash.Value.ToString());
            _Entity.TotalCheckAmount = String.IsNullOrEmpty(speHCheck.Text) ? 0 : Convert.ToDecimal(speHCheck.Value.ToString());
            _Entity.TotalBankCredit = String.IsNullOrEmpty(speHCredit.Text) ? 0 : Convert.ToDecimal(speHCredit.Value.ToString());
            _Entity.TotalAmountDue = String.IsNullOrEmpty(speHDue.Text) ? 0 : Convert.ToDecimal(speHDue.Value.ToString());
            _Entity.TotalApplied = String.IsNullOrEmpty(speHApplied.Text) ? 0 : Convert.ToDecimal(speHApplied.Value.ToString());
            _Entity.TotalAdjustment = String.IsNullOrEmpty(speHAdjustment.Text) ? 0 : Convert.ToDecimal(speHAdjustment.Value.ToString());
            _Entity.Variance = String.IsNullOrEmpty(speHVariance.Text) ? 0 : Convert.ToDecimal(speHVariance.Value.ToString());
            _Entity.CashAmount = String.IsNullOrEmpty(txtDCash.Text) ? 0 : Convert.ToDecimal(txtDCash.Value.ToString());
            _Entity.RefTransDoc = String.IsNullOrEmpty(aglTransNo.Text) ? null : aglTransNo.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM Accounting.Collection WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());

            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    gvApplication.UpdateEdit();
                    gvAdjustment.UpdateEdit();
                    gvChecks.UpdateEdit();
                    gvCredit.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.Collection", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (txtHLastEditedDate.Text != (LastEditedCheck.Rows[0]["LastEdited"].ToString() == "1/1/1900 12:00:00 AM" || String.IsNullOrEmpty(LastEditedCheck.Rows[0]["LastEdited"].ToString()) ? "" : LastEditedCheck.Rows[0]["LastEdited"].ToString()))
                    {
                        cp.JSProperties["cp_message"] = "The transaction has been modified by other user(s)." + Environment.NewLine + "Changes you made will not be saved!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }
                    
                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        if (Session["ColRepDatatable"] == "1")
                        {
                            _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                            gvApplication.DataSourceID = sdsTransDetail.ID;
                            gvApplication.UpdateEdit();
                        }
                        else
                        {
                            gvApplication.DataSourceID = "odsApplication";
                            odsApplication.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gvApplication.UpdateEdit();
                        }

                        gvAdjustment.DataSourceID = "odsAdjustment";
                        odsAdjustment.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvAdjustment.UpdateEdit();

                        gvCredit.DataSourceID = "odsCredit";
                        odsCredit.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvCredit.UpdateEdit();

                        if (Session["ColRepChecks"] == "1")
                        {
                            _Entity.DeleteChecks(txtDocNumber.Text, Session["ConnString"].ToString());
                        }

                        gvChecks.DataSourceID = "odsChecks";
                        odsChecks.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvChecks.UpdateEdit();

                        _Entity.UpdateAmount(txtDocNumber.Text, Session["ConnString"].ToString());
                        Post();
                        Validate();

                        DataTable Applicationdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Applicationdtl.Rows.Count > 0 ? "odsApplication" : "sdsApplication");
                        if (gvApplication.DataSource != null)
                        {
                            gvApplication.DataSource = null;
                        }

                        DataTable Adjustmentdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvAdjustment.DataSourceID = (Adjustmentdtl.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");
                        if (gvAdjustment.DataSource != null)
                        {
                            gvAdjustment.DataSource = null;
                        }

                        DataTable Checksdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionChecks WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvChecks.DataSourceID = (Checksdtl.Rows.Count > 0 ? "odsChecks" : "sdsChecks");
                        if (gvChecks.DataSource != null)
                        {
                            gvChecks.DataSource = null;
                        }

                        DataTable Creditdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionCredit WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvCredit.DataSourceID = (Creditdtl.Rows.Count > 0 ? "odsCredit" : "sdsCredit");
                        if (gvCredit.DataSource != null)
                        {
                            gvCredit.DataSource = null;
                        }

                        if (e.Parameter == "Add")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else if (e.Parameter == "Update")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Updated!";
                        }
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


                case "AddZeroDetail":
                case "UpdateZeroDetail":

                    gvApplication.UpdateEdit();
                    gvAdjustment.UpdateEdit();
                    gvChecks.UpdateEdit();
                    gvCredit.UpdateEdit();

                    string strError2 = Functions.Submitted(_Entity.DocNumber, "Accounting.Collection", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError2))
                    {
                        cp.JSProperties["cp_message"] = strError2;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());

                        DataTable Application = new DataTable();

                        Application = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Application.Rows.Count > 0 ? "odsApplication" : "sdsApplication");

                        gvAdjustment.DataSourceID = "odsAdjustment";
                        odsAdjustment.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvAdjustment.UpdateEdit();

                        gvCredit.DataSourceID = "odsCredit";
                        odsCredit.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvCredit.UpdateEdit();

                        if (Session["ColRepChecks"] == "1")
                        {
                            _Entity.DeleteChecks(txtDocNumber.Text, Session["ConnString"].ToString());
                        }

                        gvChecks.DataSourceID = "odsChecks";
                        odsChecks.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvChecks.UpdateEdit();

                        _Entity.UpdateAmount(txtDocNumber.Text, Session["ConnString"].ToString());
                        Post();
                        Validate();

                        DataTable Applicationdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionApplication WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Applicationdtl.Rows.Count > 0 ? "odsApplication" : "sdsApplication");
                        if (gvApplication.DataSource != null)
                        {
                            gvApplication.DataSource = null;
                        }

                        DataTable Adjustmentdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionAdjustment WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvAdjustment.DataSourceID = (Adjustmentdtl.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");
                        if (gvAdjustment.DataSource != null)
                        {
                            gvAdjustment.DataSource = null;
                        }

                        DataTable Checksdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionChecks WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvChecks.DataSourceID = (Checksdtl.Rows.Count > 0 ? "odsChecks" : "sdsChecks");
                        if (gvChecks.DataSource != null)
                        {
                            gvChecks.DataSource = null;
                        }

                        DataTable Creditdtl = Gears.RetriveData2("SELECT * FROM Accounting.CollectionCredit WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvCredit.DataSourceID = (Creditdtl.Rows.Count > 0 ? "odsCredit" : "sdsCredit");
                        if (gvCredit.DataSource != null)
                        {
                            gvCredit.DataSource = null;
                        }

                        if (e.Parameter == "AddZeroDetail")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else if (e.Parameter == "UpdateZeroDetail")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Updated!";
                        }

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
                    break;

                case "Close":

                    cp.JSProperties["cp_close"] = true;
                    gvApplication.DataSource = null;
                    gvAdjustment.DataSource = null;
                    gvChecks.DataSource = null;
                    gvCredit.DataSource = null;

                    break;

                case "CallbackTransNo":

                    Session["ColRepTransNo"] = null;
                    Session["ColRepTransNo"] = aglTransNo.Text;
                    GetSelectedVal();
                    Check();
                    cp.JSProperties["cp_generated"] = true;

                    break;

                case "CallbackCustomer":

                    Session["ColRepDatatable"] = "0";
                    gvApplication.DataSourceID = null;
                    gvApplication.DataBind();

                    gvChecks.DataSourceID = "sdsChecks";
                    if (gvChecks.DataSource != null)
                    {
                        gvChecks.DataSource = null;
                    }
                    gvChecks.DataBind();
                    Session["ColRepChecks"] = "1";

                    Session["ColRepCus"] = aglCustomerCode.Text;
                    Session["ColRepBizAcc"] = null;
                    aglBizAccount.Text = "";
                    txtName.Text = "";
                    aglTransNo.Text = "";
                    aglCollector.Text = "";

                    DataTable customer = new DataTable();

                    customer = Gears.RetriveData2("SELECT Name, Collector FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());

                    if (customer.Rows.Count > 0)
                    {
                        txtName.Text = customer.Rows[0]["Name"].ToString();
                        aglCollector.Text = customer.Rows[0]["Collector"].ToString();
                    }

                    Filter();
                    Check();
                    GetProfitAndCost();
                    cp.JSProperties["cp_clear"] = true;
                    cp.JSProperties["cp_generated"] = true;

                    break;

                case "CallbackBizAccount":

                    Session["ColRepDatatable"] = "0";
                    Session["ColRepBizAcc"] = aglBizAccount.Text;
                    Session["ColRepCus"] = null;
                    gvApplication.DataSourceID = null;
                    gvApplication.DataBind();

                    gvChecks.DataSourceID = "sdsChecks";
                    if (gvChecks.DataSource != null)
                    {
                        gvChecks.DataSource = null;
                    }
                    gvChecks.DataBind();
                    Session["ColRepChecks"] = "1";

                    aglCustomerCode.Text = "";
                    txtName.Text = "";
                    aglTransNo.Text = "";
                    aglCollector.Text = "";

                    DataTable bizaccount = new DataTable();

                    bizaccount = Gears.RetriveData2("SELECT BizAccountName AS Name FROM Masterfile.BizAccount WHERE BizAccountCode = '" + aglBizAccount.Text + "'", Session["ConnString"].ToString());

                    if (bizaccount.Rows.Count > 0)
                    {
                        txtName.Text = bizaccount.Rows[0]["Name"].ToString();
                    }

                    Filter();
                    Check();
                    Session["ColRepProfit"] = null;
                    Session["ColRepCost"] = null;
                    cp.JSProperties["cp_clear"] = true;
                    cp.JSProperties["cp_generated"] = true;

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

        protected void gvApplication_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["ColRepDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        //object[] keys = { values.Keys["LineNumber"], values.Keys["TransDoc"] };
                        object[] keys = { values.Keys["RecordID"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    //object[] keys = { values.NewValues["LineNumber"],values.NewValues["TransDoc"] };
                    object[] keys = { values.NewValues["RecordID"] };
                    DataRow row = source.Rows.Find(keys);
                    row["LineNumber"] = values.NewValues["LineNumber"];
                    row["TransType"] = values.NewValues["TransType"];
                    row["TransDoc"] = values.NewValues["TransDoc"];
                    row["TransDate"] = values.NewValues["TransDate"];
                    row["TransAccountCode"] = values.NewValues["TransAccountCode"];
                    row["TransSubsiCode"] = values.NewValues["TransSubsiCode"];
                    row["TransProfitCenter"] = values.NewValues["TransProfitCenter"];
                    row["TransCostCenter"] = values.NewValues["TransCostCenter"];
                    row["TransBizPartnerCode"] = values.NewValues["TransBizPartnerCode"];
                    row["TransAmountDue"] = values.NewValues["TransAmountDue"];
                    row["TransAmountApplied"] = values.NewValues["TransAmountApplied"];
                    //row["RecordID"] = values.NewValues["RecordID"];
                }

                foreach (DataRow dtRow in source.Rows)
                {
                    _Application.TransType = dtRow["TransType"].ToString();
                    _Application.TransDoc = dtRow["TransDoc"].ToString();
                    _Application.TransDate = Convert.ToDateTime(dtRow["TransDate"].ToString());
                    _Application.TransAccountCode = dtRow["TransAccountCode"].ToString();
                    _Application.TransSubsiCode = dtRow["TransSubsiCode"].ToString();
                    _Application.TransProfitCenter = dtRow["TransProfitCenter"].ToString();
                    _Application.TransCostCenter = dtRow["TransCostCenter"].ToString();
                    _Application.TransBizPartnerCode = dtRow["TransBizPartnerCode"].ToString();
                    _Application.TransAmountDue = Convert.ToDecimal(Convert.IsDBNull(dtRow["TransAmountDue"]) ? 0 : dtRow["TransAmountDue"]);
                    _Application.TransAmountApplied = Convert.ToDecimal(Convert.IsDBNull(dtRow["TransAmountApplied"]) ? 0 : dtRow["TransAmountApplied"]);
                    _Application.RecordID = dtRow["RecordID"].ToString();
                    _Application.AddCollectionApplication(_Application);
                }
            }
        }

        protected void gvAdjustment_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }

        protected void gvChecks_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }

        protected void gvCredit_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }

        #endregion

        protected void gvApplication_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["appdetail"] = null;
            }

            if (Session["appdetail"] != null)
            {
                gvApplication.DataSource = sdsTransDetail;
            }
        }
        private DataTable GetSelectedVal()
        {
            Session["ColRepDatatable"] = "0";
            gvApplication.DataSourceID = null;
            gvApplication.DataBind();
            DataTable dt = new DataTable();
            string[] selectedtrans = aglTransNo.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedtrans);
            string selection = selectionCriteria.ToString();

            sdsTransDetail.SelectCommand = Detail();
            gvApplication.DataSource = sdsTransDetail;
            if (gvApplication.DataSourceID != "")
            {
                gvApplication.DataSourceID = null;
            }
            gvApplication.DataBind();
            Session["ColRepDatatable"] = "1";

            foreach (GridViewColumn col in gvApplication.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvApplication.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvApplication.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] {
            //dt.Columns["LineNumber"],dt.Columns["TransDoc"]};
            dt.Columns["RecordID"]};

            return dt;
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
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        private void Filter()
        {
            sdsRefTrans.ConnectionString = Session["ConnString"].ToString();
            sdsBank.ConnectionString = Session["ConnString"].ToString();
            sdsBranch.ConnectionString = Session["ConnString"].ToString();

            if (aglCustomerCode.Text != "" || Session["ColRepCus"] != null)
            {
                //Base filter on session 04-28 Era
                string cus = string.IsNullOrEmpty(aglCustomerCode.Text) ? Session["ColRepCus"].ToString() : aglCustomerCode.Text;
                Session["ColRepCus"] = cus;

                //11/29/2016	GC	Changed code and added PLDocNum
                //sdsRefTrans.SelectCommand = "SELECT DISTINCT RecordID, TransType, DocNumber, DocDate, AccountCode, SubsiCode, ProfitCenter, CostCenter, BizPartnerCode, ISNULL(Amount,0) - ISNULL(Applied,0) AS Amount, "
                //   + " CounterDocNumber AS CounterReceiptNo FROM Accounting.SubsiLedgerNonInv WHERE ISNULL(Applied,0) <> ISNULL(Amount,0) AND BizPartnerCode = '" + cus + "'";

                sdsRefTrans.SelectCommand = "SELECT DISTINCT A.RecordID, A.TransType, A.DocNumber, A.DocDate, A.AccountCode, A.SubsiCode, A.ProfitCenter, A.CostCenter, A.BizPartnerCode, ISNULL(A.Amount,0) - ISNULL(A.Applied,0) AS Amount, "
                    + " A.CounterDocNumber AS CounterReceiptNo, ISNULL(B.PLDocNum,'') AS PLDocNum FROM Accounting.SubsiLedgerNonInv A LEFT JOIN Sales.DeliveryReceipt B ON A.DocNumber = B.DocNumber AND A.TransType IN ('SLSDRC','ACTSIN') "
                    + " WHERE ISNULL(A.Applied,0) <> ISNULL(A.Amount,0) AND A.BizPartnerCode = '" + cus + "'";
                //END

                aglTransNo.DataBind();

                //2016-04-25 Remove filter of transtype as per kuya jabs

                sdsBank.SelectCommand = "SELECT DISTINCT A.BankCode AS Bank, C.Description, A.AccountNo, A.Branch FROM Masterfile.BPBankInfo A INNER JOIN "
                    + " Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode LEFT JOIN Masterfile.Bank C ON A.BankCode = C.BankCode "
                    + " WHERE A.BizPartnerCode = '" + cus + "'";
                gvChecks.DataBind();

                //sdsBranch.SelectCommand = "SELECT A.Branch FROM Masterfile.BPBankInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode "
                //    + " WHERE A.BizPartnerCode = '" + cus + "'";
                //gvChecks.DataBind();
            }

            if (aglBizAccount.Text != "" || Session["ColRepBizAcc"] != null)
            {
                //Base filter on session 04-28 Era
                string bizacc = string.IsNullOrEmpty(aglBizAccount.Text) ? Session["ColRepBizAcc"].ToString() : aglBizAccount.Text;
                Session["ColRepBizAcc"] = bizacc;

                //11/29/2016	GC	Changed code and added PLDocNum
                //sdsRefTrans.SelectCommand = "SELECT DISTINCT RecordID, TransType, DocNumber, DocDate, AccountCode, ProfitCenter, SubsiCode, CostCenter, "
                //    + " A.BizPartnerCode, ISNULL(Amount,0) - ISNULL(Applied,0) AS Amount, A.CounterDocNumber AS CounterReceiptNo FROM Accounting.SubsiLedgerNonInv A LEFT JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode "
                //    + " WHERE ISNULL(Applied,0) <> ISNULL(Amount,0) "
                //    + " AND (B.BusinessAccountCode = '" + bizacc + "' OR A.BizPartnerCode = '" + bizacc + "')";
                sdsRefTrans.SelectCommand = "SELECT DISTINCT A.RecordID, A.TransType, A.DocNumber, A.DocDate, A.AccountCode, A.ProfitCenter, A.SubsiCode, A.CostCenter, "
                    + " A.BizPartnerCode, ISNULL(A.Amount,0) - ISNULL(A.Applied,0) AS Amount, A.CounterDocNumber AS CounterReceiptNo, ISNULL(C.PLDocNum,'') AS PLDocNum FROM Accounting.SubsiLedgerNonInv A LEFT JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode "
                    + " LEFT JOIN Sales.DeliveryReceipt C ON A.DocNumber = C.DocNumber AND A.TransType IN ('SLSDRC','ACTSIN') WHERE ISNULL(A.Applied,0) <> ISNULL(A.Amount,0) "
                    + " AND (B.BusinessAccountCode = '" + bizacc + "' OR A.BizPartnerCode = '" + bizacc + "')";
                //END

                aglTransNo.DataBind();

                //2016-04-25 Remove filter of transtype as per kuya jabs

                sdsBank.SelectCommand = "SELECT DISTINCT A.BankCode AS Bank, C.Description, A.AccountNo, A.Branch FROM Masterfile.BPBankInfo A INNER JOIN "
                    + " Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode LEFT JOIN Masterfile.Bank C ON A.BankCode = C.BankCode "
                    + " WHERE B.BusinessAccountCode = '" + bizacc + "'";
                gvChecks.DataBind();

                //sdsBranch.SelectCommand = "SELECT A.Branch FROM Masterfile.BPBankInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode "
                //    + " WHERE B.BusinessAccountCode = '" + bizacc + "'";
                //gvChecks.DataBind();
            }
        }
        private void Check()
        {
            if (aglCustomerCode.Text == "" && aglBizAccount.Text == "")
            {
                aglTransNo.ClientEnabled = false;
                btnGenerate.ClientEnabled = false;
            }
            else
            {
                aglTransNo.ClientEnabled = true;
                btnGenerate.ClientEnabled = true;
            }
        }
        public string Detail()
        {

            string query = "";
            string Value = "";
            int cnt = 0;
            string bridge = "";

            int count = aglTransNo.Text.Split(';').Length;
            var pieces = aglTransNo.Text.Split(new[] { ';' }, count);

            foreach (string c in pieces)
            {
                var a = c.Split(new[] { '-' }, 2);

                if (cnt != 0)
                {
                    bridge = " OR ";
                }
                Value += bridge + "(RecordID = '" + a[0].ToString() + "' AND DocNumber ='" + a[1].ToString() + "')";

                cnt = cnt + 1;
            }

            query = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, TransType ,DocNumber AS TransDoc, DocDate AS TransDate, "
                     + " AccountCode AS TransAccountCode, SubsiCode AS TransSubsiCode, ProfitCenter AS TransProfitCenter, CostCenter AS TransCostCenter, BizPartnerCode AS TransBizPartnerCode, "
                     + " ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAmountDue, ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAmountApplied,  '2' AS Version, RecordID FROM Accounting.SubsiLedgerNonInv WHERE  ISNULL(Applied,0) <> ISNULL(Amount,0) "
                     + " AND ( " + Value + " )";

            return query;
        }
        protected void GetProfitAndCost()
        {
            if (!String.IsNullOrEmpty(aglCustomerCode.Text))
            {
                DataTable pc = Gears.RetriveData2("SELECT ProfitCenterCode, CostCenterCode FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());

                if (pc.Rows.Count != 0)
                {
                    Session["ColRepProfit"] = pc.Rows[0]["ProfitCenterCode"].ToString();
                    Session["ColRepCost"] = pc.Rows[0]["CostCenterCode"].ToString();
                }
            }
        }
        protected void gvAdjustment_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            if (!String.IsNullOrEmpty(aglCustomerCode.Text))
            {
                e.NewValues["AccountCode"] = "";
                e.NewValues["ProfitCenter"] = Session["ColRepProfit"].ToString();
                e.NewValues["CostCenter"] = Session["ColRepCost"].ToString();
            }
        }

        protected void aglTransNo_Init(object sender, EventArgs e)
        {
            Filter();
        }

        protected void glBank_Init(object sender, EventArgs e)
        {

        }

        protected void gvChecks_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Bank"] = "";
        }

        protected void glAccountCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
                if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                {
                    gridLookup.GridView.DataSourceID = "sdsAccountCode";
                    sdsAccountCode.SelectCommand = "SELECT AccountCode, Description, ISNULL(ControlAccount,0) AS ControlAccount FROM Accounting.ChartOfAccount WHERE ISNULL(IsInactive,0) = 0 AND ISNULL(AllowJV,0) = 1";
                    gridLookup.DataBind();
                }
        }
    }
}