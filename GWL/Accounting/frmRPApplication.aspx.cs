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
    public partial class frmRPApplication : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        //string Cascade = "";
        //string TransNo = "";

        Entity.RPApplication _Entity = new Entity.RPApplication();//Calls entity odsHeader
        Entity.RPApplication.RPApplicationTag _Application = new Entity.RPApplication.RPApplicationTag();
        Entity.RPApplication.RPApplicationAdj _Adjustment = new Entity.RPApplication.RPApplicationAdj();


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

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            txtDocNumber.ReadOnly = true;
            gvRef.Columns["CommandString"].Width = 0;
            gvRef.Columns["RCommandString"].Width = 0;

            if (!IsPostBack)
            {
                Session["appdetail"] = null;
                Session["RPADatatable"] = null;
                Session["RPAProfit"] = null;
                Session["RPACost"] = null;
                Session["RPAFilterExpression"] = null;
                Session["RPATransNo"] = null;
                Session["RPABizAcc"] = null;
                Session["RPACus"] = null;
                Session["RPADatatable2"] = null;
                Session["RPATransLoan"] = null;
                Session["RPADocDate"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }                    
                
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglBizPartner.Value = _Entity.BizPartner.ToString();
                txtName.Value = _Entity.Name.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                speAdjustment.Value = _Entity.Adjustment.ToString();
                speVariance.Value = _Entity.Variance.ToString();

                Filter();

                aglTransNo.Text = _Entity.RefTransDoc.ToString();
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

                gvApplication.KeyFieldName = "LineNumber";
                gvAdjustment.KeyFieldName = "LineNumber";
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
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }
            }
            
            DataTable Application = new DataTable();
            DataTable Adjustment = new DataTable();

            Application = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            gvApplication.DataSourceID = (Application.Rows.Count > 0 ? "odsApplication" : "sdsApplication");

            Adjustment = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            gvAdjustment.DataSourceID = (Adjustment.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");

            gvJournal.DataSourceID = "odsJournalEntry";

            GetProfitAndCost();
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTRPA";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.RPApplication_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTRPA";
            gparam._Table = "Accounting.RPApplication";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.RPApplication_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;
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
                    e.Visible = true;
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
                if (e.ButtonID == "Delete" || e.ButtonID == "AdjustmentDelete" 
                    || e.ButtonID == "ApplicationDelete")
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
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["RPAFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsSubsi";
                sdsSubsi.FilterExpression = Session["RPAFilterExpression"].ToString();
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
            //string codes = "";
            

            if (e.Parameters.Contains("SubsiCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gvAdjustment.FindEditRowCellTemplateControl((GridViewDataColumn)gvAdjustment.Columns[2], "glAccountCode");
                var selectedValues = value;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { value });
                sdsSubsi.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["RPAFilterExpression"] = sdsSubsi.FilterExpression;
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
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.BizPartner = String.IsNullOrEmpty(aglBizPartner.Text) ? null : aglBizPartner.Value.ToString();
            _Entity.Name = String.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.Adjustment = String.IsNullOrEmpty(speAdjustment.Text) ? 0 : Convert.ToDecimal(speAdjustment.Value.ToString());
            _Entity.Variance = String.IsNullOrEmpty(speVariance.Text) ? 0 : Convert.ToDecimal(speVariance.Value.ToString());
            _Entity.RefTransDoc = String.IsNullOrEmpty(aglTransNo.Text) ? null : aglTransNo.Text;
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
              

            switch (e.Parameter)
            {
                case "Add":

                    gvApplication.UpdateEdit();
                    gvAdjustment.UpdateEdit();
                    
                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.RPApplication", 1, Session["ConnString"].ToString());
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

                        if (Session["RPADatatable"] == "1")
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

                        Post();
                        Validate();

                        DataTable Applicationdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Applicationdtl.Rows.Count > 0 ? "odsApplication" : "sdsApplication");
                        if (gvApplication.DataSource != null)
                        {
                            gvApplication.DataSource = null;
                        }

                        DataTable Adjustmentdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvAdjustment.DataSourceID = (Adjustmentdtl.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");
                        if (gvAdjustment.DataSource != null)
                        {
                            gvAdjustment.DataSource = null;
                        }

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

                    gvApplication.UpdateEdit();
                    gvAdjustment.UpdateEdit();
                    
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Accounting.RPApplication", 1, Session["ConnString"].ToString());
                    if (!string.IsNullOrEmpty(strError1))
                    {
                        cp.JSProperties["cp_message"] = strError1;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        if (Session["RPADatatable"] == "1")
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

                        Post();
                        Validate();

                        DataTable Applicationdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Applicationdtl.Rows.Count > 0 ? "odsApplication" : "sdsApplication");
                        if (gvApplication.DataSource != null)
                        {
                            gvApplication.DataSource = null;
                        }

                        DataTable Adjustmentdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvAdjustment.DataSourceID = (Adjustmentdtl.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");
                        if (gvAdjustment.DataSource != null)
                        {
                            gvAdjustment.DataSource = null;
                        }

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

                case "AddZeroDetail":

                    gvApplication.UpdateEdit();
                    gvAdjustment.UpdateEdit();
                    
                    string strError2 = Functions.Submitted(_Entity.DocNumber, "Accounting.RPApplication", 1, Session["ConnString"].ToString());
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

                        Application = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Application.Rows.Count > 0 ? "odsApplication" : "sdsApplication");

                        gvAdjustment.DataSourceID = "odsAdjustment";
                        odsAdjustment.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvAdjustment.UpdateEdit();

                        Post();
                        Validate();

                        DataTable Applicationdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Applicationdtl.Rows.Count > 0 ? "odsApplication" : "sdsApplication");
                        if (gvApplication.DataSource != null)
                        {
                            gvApplication.DataSource = null;
                        }

                        DataTable Adjustmentdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvAdjustment.DataSourceID = (Adjustmentdtl.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");
                        if (gvAdjustment.DataSource != null)
                        {
                            gvAdjustment.DataSource = null;
                        }

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

                case "UpdateZeroDetail":

                    gvApplication.UpdateEdit();
                    gvAdjustment.UpdateEdit();
                    
                    string strError3 = Functions.Submitted(_Entity.DocNumber, "Accounting.RPApplication", 1, Session["ConnString"].ToString());
                    if (!string.IsNullOrEmpty(strError3))
                    {
                        cp.JSProperties["cp_message"] = strError3;
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

                        Application = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Application.Rows.Count > 0 ? "odsApplication" : "sdsApplication");

                        gvAdjustment.DataSourceID = "odsAdjustment";
                        odsAdjustment.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gvAdjustment.UpdateEdit();

                        Post();
                        Validate();

                        DataTable Applicationdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationTag WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvApplication.DataSourceID = (Applicationdtl.Rows.Count > 0 ? "odsApplication" : "sdsApplication");
                        if (gvApplication.DataSource != null)
                        {
                            gvApplication.DataSource = null;
                        }

                        DataTable Adjustmentdtl = Gears.RetriveData2("SELECT * FROM Accounting.RPApplicationAdj WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                        gvAdjustment.DataSourceID = (Adjustmentdtl.Rows.Count > 0 ? "odsAdjustment" : "sdsAdjustment");
                        if (gvAdjustment.DataSource != null)
                        {
                            gvAdjustment.DataSource = null;
                        }

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
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    gvApplication.DataSource = null;
                    gvAdjustment.DataSource = null;

                    break;

                case "CallbackTransNo":

                    Session["RPATransNo"] = null;
                    Session["RPATransNo"] = aglTransNo.Text;
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    
                    break;

                case "CallbackDocDate":

                    Session["RPADatatable"] = "0";
                    Session["RPADatatable2"] = "0";
                    gvApplication.DataSourceID = null;
                    gvApplication.DataBind();

                    Session["RPADocDate"] = dtpDocDate.Text;
                    aglTransNo.Text = "";
                    Filter();
                    cp.JSProperties["cp_generated"] = true;

                    break;

                case "CallbackBizPartner":

                    Session["RPADatatable"] = "0";
                    Session["RPADatatable2"] = "0";
                    gvApplication.DataSourceID = null;
                    gvApplication.DataBind();

                    Session["RPACus"] = aglBizPartner.Text;
                    txtName.Text = "";
                    aglTransNo.Text = "";

                    DataTable customer = new DataTable();

                    customer = Gears.RetriveData2("SELECT Name FROM Masterfile.BizPartner WHERE BizPartnerCode = '" + aglBizPartner.Text + "'", Session["ConnString"].ToString());

                    if (customer.Rows.Count > 0)
                    {
                        txtName.Text = customer.Rows[0]["Name"].ToString();
                    }
                    
                    Filter();
                    GetProfitAndCost();
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
        
        protected void gvApplication_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["RPADatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"], values.Keys["TransDoc"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"],values.NewValues["TransDoc"] };
                    DataRow row = source.Rows.Find(keys);
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
                    row["RecordID"] = values.NewValues["RecordID"];
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
                    _Application.AddRPApplicationTag(_Application);
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
            Session["RPADatatable"] = "0";
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
            Session["RPADatatable"] = "1";

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
            dt.Columns["LineNumber"],dt.Columns["TransDoc"]};

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

            if (aglBizPartner.Text != "" || Session["RPACus"] != null)
            {
                string cus = string.IsNullOrEmpty(aglBizPartner.Text) ? Session["RPACus"].ToString() : aglBizPartner.Text;
                string date = string.IsNullOrEmpty(dtpDocDate.Text) ? Session["RPADocDate"].ToString() : dtpDocDate.Text;
                Session["RPACus"] = cus;
                Session["RPADocDate"] = date;
                sdsRefTrans.SelectCommand = "SELECT DISTINCT RecordID, TransType, DocNumber, DocDate, AccountCode, SubsiCode, ProfitCenter, CostCenter, BizPartnerCode, ISNULL(Amount,0) - ISNULL(Applied,0) AS Amount, "
                    + " CounterDocNumber AS CounterReceiptNo FROM Accounting.SubsiLedgerNonInv WHERE ISNULL(Applied,0) <> ISNULL(Amount,0) AND BizPartnerCode = '" + cus + "' AND DocDate <= '" + date + "'";
                aglTransNo.DataBind();
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
            if (!String.IsNullOrEmpty(aglBizPartner.Text))
            {
                DataTable pc = Gears.RetriveData2("SELECT ProfitCenterCode, CostCenterCode FROM Masterfile.BizPartner WHERE BizPartnerCode = '" + aglBizPartner.Text.Trim() + "'", Session["ConnString"].ToString());
                
                if (pc.Rows.Count != 0)
                {
                    Session["RPAProfit"] = pc.Rows[0]["ProfitCenterCode"].ToString();
                    Session["RPACost"] = pc.Rows[0]["CostCenterCode"].ToString();
                }
                else
                {
                    Session["RPAProfit"] = "";
                    Session["RPACost"] = "";
                }
            }
        }
        protected void gvAdjustment_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            if (!String.IsNullOrEmpty(aglBizPartner.Text))
            {
                e.NewValues["ProfitCenter"] = Session["RPAProfit"].ToString();
                e.NewValues["CostCenter"] = Session["RPACost"].ToString();
            }
        }
        protected void aglTransNo_Init(object sender, EventArgs e)
        {
            Filter();
        }
    }
}