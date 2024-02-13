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
    public partial class frmCounterSlip : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string ErrorMsg = "";

        Entity.CounterSlip _Entity = new CounterSlip();//Calls entity odsHeader
        Entity.CounterSlip.CounterSlipDetail _EntityDetail = new CounterSlip.CounterSlipDetail();//Call entity sdsDetail

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
            gvRef.Columns["CommandString"].Width = 0;
            gvRef.Columns["RCommandString"].Width = 0;
            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["CtrSlpDatatable"] = null;
                Session["CtrSlpchecker"] = null;
                Session["CtrSlpFilterExpression"] = null;
                Session["CtrSlpButton"] = null;
                Session["CtrSlpDocNum"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.KeyFieldName = "LineNumber";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                aglBizAccount.Value = _Entity.BizAccountCode.ToString();
                dtpDateFrom.Text = String.IsNullOrEmpty(_Entity.DateFrom.ToString()) ? null : Convert.ToDateTime(_Entity.DateFrom.ToString()).ToShortDateString();
                dtpDateTo.Text = String.IsNullOrEmpty(_Entity.DateTo.ToString()) ? null : Convert.ToDateTime(_Entity.DateTo.ToString()).ToShortDateString();
                txtGross.Value = _Entity.TotalGrossVat.ToString();
                //aglTransNo.Value = _Entity.RefTrans.ToString();
                aglTransNo.Text = _Entity.RefTrans.ToString();
                txtNonVatable.Value = _Entity.TotalGrossNonVat.ToString();
                txtVatAmount.Value = _Entity.TotalVAT.ToString();
                txtAmountDue.Value = _Entity.TotalAmountDue.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                    
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
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                gv1.KeyFieldName = "LineNumber";

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                            popup.ShowOnPageLoad = true;
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                        }                    
                        ButtonVisible();

                        break;
                    case "E":

                        updateBtn.Text = "Update";
                        popup.ShowOnPageLoad = true;                    
                        ButtonVisible();

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


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    //gv1.DataSourceID = null;
                    //popup.ShowOnPageLoad = false;
                    gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }

            }

            DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.CounterSlipDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

            gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCOS";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.CounterSlip_Validate(gparam);
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
        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
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
            combo.DropDownButton.Enabled = !view;
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
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }

        protected void Button_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton button = sender as ASPxButton;
            button.ClientVisible = !view;
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
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Value.ToString();
            _Entity.BizAccountCode = String.IsNullOrEmpty(aglBizAccount.Text) ? null : aglBizAccount.Value.ToString();
            _Entity.DateFrom = dtpDateFrom.Text;
            _Entity.DateTo = dtpDateTo.Text;
            _Entity.TotalGrossVat = String.IsNullOrEmpty(txtGross.Text) ? 0 : Convert.ToDecimal(txtGross.Value.ToString());
            _Entity.TotalGrossNonVat = String.IsNullOrEmpty(txtNonVatable.Text) ? 0 : Convert.ToDecimal(txtNonVatable.Value.ToString());
            _Entity.TotalVAT = String.IsNullOrEmpty(txtVatAmount.Text) ? 0 : Convert.ToDecimal(txtVatAmount.Value.ToString());
            _Entity.TotalAmountDue = String.IsNullOrEmpty(txtAmountDue.Text) ? 0 : Convert.ToDecimal(txtAmountDue.Value.ToString());
            _Entity.RefTrans = String.IsNullOrEmpty(aglTransNo.Text) ? null : aglTransNo.Text;
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
            _Entity.Connection = Session["ConnString"].ToString();

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            DataTable dt = new DataTable();

            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }

            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }

            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    if (Session["CtrSlpDatatable"] == null)
                    {
                        gv1.UpdateEdit();
                    }

                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.CounterSlip", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    CheckDates();

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        //if (Session["CtrSlpDatatable"] == "1")
                        //{
                        _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                        gv1.DataSource = dt;
                        if (gv1.DataSourceID != null)
                        {
                            gv1.DataSourceID = null;
                        }
                        gv1.DataBind();
                        gv1.UpdateEdit();
                        //gv1.DataSourceID = sdsSalesInvoice.ID;                            
                        //gv1.UpdateEdit();
                        //}
                        //else
                        //{
                        //    //gv1.DataSourceID = "odsDetail";
                        //    gv1.DataSource = odsDetail;
                        //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        //    if (gv1.DataSourceID != null)
                        //    {
                        //        gv1.DataSourceID = null;
                        //    }
                        //    gv1.DataBind();
                        //    gv1.UpdateEdit();
                        //}
                        Validate();
                        _Entity.UpdateOther(txtDocNumber.Text, Session["ConnString"].ToString());

                        cp.JSProperties["cp_message"] = e.Parameter == "Add" ? "Successfully Added!" : "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        ButtonVisible();
                        cp.JSProperties["cp_message"] = ErrorMsg;
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                //case "Update":

                //    if (Session["CtrSlpDatatable"] == null)
                //    {
                //        gv1.UpdateEdit();
                //    }

                //    string strError1 = Functions.Submitted(_Entity.DocNumber, "Accounting.CounterSlip", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                //    if (!string.IsNullOrEmpty(strError1))
                //    {
                //        cp.JSProperties["cp_message"] = strError1;
                //        cp.JSProperties["cp_success"] = true;
                //        cp.JSProperties["cp_forceclose"] = true;
                //        return;
                //    }

                //    CheckDates();

                //    if (error == false)
                //    {
                //        check = true;
                //        _Entity.LastEditedBy = Session["userid"].ToString();
                //        _Entity.UpdateData(_Entity);//Method of inserting for header
                //        //if (Session["CtrSlpDatatable"] == "1")
                //        //{
                //        _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                //        gv1.DataSource = dt;
                //        if (gv1.DataSourceID != null)
                //        {
                //            gv1.DataSourceID = null;
                //        }
                //        gv1.DataBind();
                //        gv1.UpdateEdit();
                //        //gv1.DataSourceID = sdsSalesInvoice.ID;                            
                //        //gv1.UpdateEdit();
                //        //}
                //        //else
                //        //{
                //        //    //gv1.DataSourceID = "odsDetail";
                //        //    gv1.DataSource = odsDetail;
                //        //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                //        //    if (gv1.DataSourceID != null)
                //        //    {
                //        //        gv1.DataSourceID = null;
                //        //    }
                //        //    gv1.DataBind();
                //        //    gv1.UpdateEdit();
                //        //}
                //        Validate();
                //        _Entity.UpdateOther(txtDocNumber.Text, Session["ConnString"].ToString());
                //        cp.JSProperties["cp_message"] = "Successfully Updated!";
                //        cp.JSProperties["cp_success"] = true;
                //        cp.JSProperties["cp_close"] = true;
                //        Session["Refresh"] = "1";
                //    }
                //    else
                //    {
                //        ButtonVisible();
                //        cp.JSProperties["cp_message"] = ErrorMsg;
                //        cp.JSProperties["cp_success"] = true;
                //    }
                //    break;

                case "AddZeroDetail":
                case "UpdateZeroDetail":

                    gv1.UpdateEdit();

                    string strError2 = Functions.Submitted(_Entity.DocNumber, "Accounting.CounterSlip", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError2))
                    {
                        cp.JSProperties["cp_message"] = strError2;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    CheckDates();

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.CounterSlipDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        Validate();
                        _Entity.UpdateOther(txtDocNumber.Text, Session["ConnString"].ToString());
                        cp.JSProperties["cp_message"] = e.Parameter == "AddZeroDetail" ? "Successfully Added!" : "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        ButtonVisible();
                        cp.JSProperties["cp_message"] = ErrorMsg;
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
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    gv1.DataSource = null;
                    break;

                case "Generate":

                    GetSelectedVal();

                    if (Session["CtrSlpchecker"] != null)
                    {
                        cp.JSProperties["cp_check"] = true;
                        cp.JSProperties["cp_message"] = "No data to be generated!";
                        Session["CtrSlpchecker"] = null;
                    }
                    cp.JSProperties["cp_generated"] = true;

                    ButtonVisible();

                    break;

                case "GridReset":

                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    Session["CtrSlpDatatable"] = 0;
                    aglTransNo.Text = "";
                    cp.JSProperties["cp_generated"] = true;

                    //txtGross.Text = "0.00";
                    //txtNonVatable.Text = "0.00";
                    //txtVatAmount.Text = "0.00";
                    //txtAmountDue.Text = "0.00";

                    ButtonVisible();

                    break;

                case "CallbackCustomer":
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    Session["CtrSlpDatatable"] = 0;
                    aglTransNo.Text = "";
                    aglBizAccount.Text = "";
                    cp.JSProperties["cp_generated"] = true;
                    ButtonVisible();
                    break;

                case "CallbackBizAccount":
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    Session["CtrSlpDatatable"] = 0;
                    aglTransNo.Text = "";
                    aglCustomerCode.Text = "";
                    cp.JSProperties["cp_generated"] = true;
                    ButtonVisible();
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
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            //if (Session["CtrSlpDatatable"] == "1" && check == true)
            if (check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"]};
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    //row["DRNumber"] = values.NewValues["DRNumber"];
                    //row["DRDate"] = values.NewValues["DRDate"];
                    row["SalesInvoiceNo"] = values.NewValues["SalesInvoiceNo"];
                    row["SIDate"] = values.NewValues["SIDate"];
                    row["Qty"] = values.NewValues["Qty"];
                    row["GrossVAT"] = values.NewValues["GrossVAT"];
                    row["GrossNonVAT"] = values.NewValues["GrossNonVAT"];
                    row["VATAmount"] = values.NewValues["VATAmount"];
                    row["AmountDue"] = values.NewValues["AmountDue"];                    
                    row["BaseQty"] = values.NewValues["BaseQty"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
                    row["BarcodeNo"] = values.NewValues["BarcodeNo"];
                    row["UnitFactor"] = values.NewValues["UnitFactor"];

                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                }
                    
                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    //_EntityDetail.DRNumber = dtRow["DRNumber"].ToString();
                    //_EntityDetail.DRDate = Convert.ToDateTime(dtRow["DRDate"].ToString());
                    _EntityDetail.SalesInvoiceNo = dtRow["SalesInvoiceNo"].ToString();
                    _EntityDetail.SIDate = Convert.ToDateTime(dtRow["SIDate"].ToString());
                    _EntityDetail.Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                    _EntityDetail.GrossVAT = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVAT"]) ? 0 : dtRow["GrossVAT"]);
                    _EntityDetail.GrossNonVAT = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossNonVAT"]) ? 0 : dtRow["GrossNonVAT"]);
                    _EntityDetail.VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);
                    _EntityDetail.AmountDue = Convert.ToDecimal(Convert.IsDBNull(dtRow["AmountDue"]) ? 0 : dtRow["AmountDue"]);                
                    _EntityDetail.BaseQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BaseQty"]) ? 0 : dtRow["BaseQty"]);
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
                    _EntityDetail.UnitFactor = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFactor"]) ? 0 : dtRow["UnitFactor"]);

                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.AddCounterSlipDetail(_EntityDetail);
                }
            }            
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CtrSlpDetail"] = null;
            }

            if (Session["CtrSlpDetail"] != null)
            {
                gv1.DataSourceID = sdsSalesInvoice.ID;
            }
        }

        private DataTable GetSelectedVal()
        {
            Session["CtrSlpDatatable"] = "0";
            gv1.DataSourceID = null;
            gv1.DataSource = null;
            gv1.DataBind();
            DataTable dt = new DataTable();
            string[] selectedValues = aglTransNo.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
            // 03/03/2017   GC  Changed ORDER BY clause
            //sdsSalesInvoice.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY SalesInvoiceNo) AS VARCHAR(5)),5) AS LineNumber, * FROM "
            sdsSalesInvoice.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY SIDate, SalesInvoiceNo ASC) AS VARCHAR(5)),5) AS LineNumber, * FROM "
            // END
                + " (SELECT * FROM (SELECT A.DocNumber AS SalesInvoiceNo, CONVERT(VARCHAR(10),A.DocDate,101) AS SIDate, A.CustomerCode, ABS(SUM(ISNULL(Qty,0))) AS Qty, ABS(ISNULL(VatAfterDisc,0)) AS GrossVAT, ABS(ISNULL(NonVatAfterDisc,0)) AS GrossNonVAT, ABS(ISNULL(VATAmount,0)) AS VATAmount,  "
            + " ABS(ISNULL(D.Amount,0) - ISNULL(D.Applied,0)) AS AmountDue,  '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9, '' AS StatusCode, 0.00 AS BaseQty, '' AS BarcodeNo, 0.00 AS UnitFactor, '2' AS Version "
	        + " FROM Accounting.SalesInvoice A INNER JOIN Accounting.SalesInvoiceDetail B ON A.DocNumber = B.DocNumber INNER JOIN Accounting.SubsiledgerNonInv D ON A.DocNumber = D.DocNumber AND ISNULL(CounterDocNumber,'') = ''  "
            + " WHERE A." + selectionCriteria.ToString() + " GROUP BY A.DocNumber, A.DocDate, A.CustomerCode, VatAfterDisc, NonVatAfterDisc, VATAmount, D.Amount, D.Applied) AS A  "
            + " UNION ALL SELECT * FROM (SELECT A.DocNumber AS SalesInvoiceNo, CONVERT(VARCHAR(10),A.DocDate,101) AS DocDate, A.CustomerCode, -ABS(SUM(ISNULL(Qty,0))) AS Qty, "
            + " -ABS(ISNULL(GrossVatAmount,0)) AS GrossVAT, -ABS(ISNULL(GrossNonVatAmount,0)) AS GrossNonVAT, -ABS(ISNULL(VATAmount,0)) AS VATAmount,  "
            + " -ABS(ISNULL(TotalAmount,0)) AS AmountDue,  '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9, '' AS StatusCode, 0.00 AS BaseQty, '' AS BarcodeNo, 0.00 AS UnitFactor, '2' AS Version "
	        + " FROM Accounting.ARMemo A INNER JOIN Accounting.ARMemoDetail B ON A.DocNumber = B.DocNumber INNER JOIN Accounting.SubsiledgerNonInv D ON A.DocNumber = D.DocNumber AND ISNULL(CounterDocNumber,'') = ''  "
            + " WHERE A."+ selectionCriteria.ToString() +"  and Type!='Other Adjustment' GROUP BY A.DocNumber, A.DocDate, A.CustomerCode, GrossVatAmount, GrossNonVatAmount, VATAmount, TotalAmount "
            
            + " UNION ALL SELECT A.DocNumber AS SalesInvoiceNo, CONVERT(VARCHAR(10),A.DocDate,101) AS DocDate, A.CustomerCode, 0 AS Qty, "
            + " (ISNULL(GrossVatAmount,0)) AS GrossVAT, (ISNULL(GrossNonVatAmount,0)) AS GrossNonVAT, (ISNULL(VATAmount,0)) AS VATAmount,  "
            + " (ISNULL(TotalAmount,0)) AS AmountDue,  '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9, '' AS StatusCode, 0.00 AS BaseQty, '' AS BarcodeNo, 0.00 AS UnitFactor, '2' AS Version "
	        + " FROM Accounting.ARMemo A INNER JOIN Accounting.SubsiledgerNonInv D ON A.DocNumber = D.DocNumber AND ISNULL(CounterDocNumber,'') = ''  "
            + " WHERE A."+ selectionCriteria.ToString() +"  and Type='Other Adjustment' GROUP BY A.DocNumber, A.DocDate, A.CustomerCode, GrossVatAmount, GrossNonVatAmount, VATAmount, TotalAmount "
            
            + " ) AS B "
            
            + " UNION ALL " 
            + " SELECT * FROM "
            + " 		( "
            + " 		SELECT A.DocNumber AS SalesInvoiceNo, CONVERT(VARCHAR(10),A.DocDate,101) AS DocDate, D.BizPartnerCode AS CustomerCode , 0 AS Qty, "
            + " 		0 AS GrossVAT, ABS(ISNULL(D.Amount,0)) AS GrossNonVAT, 0 AS VATAmount,  "
            + " 		ABS(ISNULL(D.Amount,0) - ISNULL(D.Applied,0)) AS AmountDue,  '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9, '' AS StatusCode, 0.00 AS BaseQty, '' AS BarcodeNo, 0.00 AS UnitFactor, '2' AS Version "
            + " 		FROM Accounting.JournalVoucher A INNER JOIN Accounting.JournalVoucherDetail B ON A.DocNumber = B.DocNumber INNER JOIN Accounting.SubsiledgerNonInv D ON A.DocNumber = D.DocNumber AND ISNULL(CounterDocNumber,'') = ''  "
            + " 		WHERE A." + selectionCriteria.ToString() + " AND D.BizPartnerCode = '" + (String.IsNullOrEmpty(aglCustomerCode.Text) ? "" : aglCustomerCode.Text) + "' GROUP BY A.DocNumber, A.DocDate, D.BizPartnerCode, D.Amount, D.Applied) AS D "
            + " ) C";

            //sdsSalesInvoice.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["CtrSlpDetail"] = sdsSalesInvoice.FilterExpression;
            gv1.DataSource = sdsSalesInvoice;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session["CtrSlpDatatable"] = "1";

            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }

            if (dt.Rows.Count == 0)
            {
                Session["CtrSlpchecker"] = "1";
            }

            dt.PrimaryKey = new DataColumn[] {
            dt.Columns["LineNumber"]};

            return dt;

            //sdsSalesInvoice.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, "
            //+ " A.DocNumber, CONVERT(VARCHAR(10),A.DocDate,101) AS SIDate, A.CustomerCode, B.TransDoc AS DRNumber, CONVERT(VARCHAR(10),B.TransDate,101) AS DRDate, "
            //+ " B.DocNumber AS SalesInvoiceNo, SUM(ISNULL(Qty,0)) AS Qty, "
            //+ " SUM(CASE WHEN VTaxCode != 'NONV' THEN ISNULL(Price,0) * ISNULL(Qty,0) ELSE 0 END) AS GrossVAT, "
            //+ " SUM(CASE WHEN VTaxCode = 'NONV' THEN ISNULL(Price,0) * ISNULL(Qty,0) ELSE 0 END) AS GrossNonVAT, "
            //+ " SUM( (ISNULL(Price,0) * ISNULL(Qty,0)) * C.Rate) AS VATAmount, "
            //+ " SUM(ISNULL(Price,0) * ISNULL(Qty,0)) + SUM( (ISNULL(Price,0) * ISNULL(Qty,0)) * C.Rate) AS AmountDue, "
            //+ " '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, "
            //+ " '' AS Field7, '' AS Field8, '' AS Field9,"
            //+ " '' AS StatusCode, 0.00 AS BaseQty, '' AS BarcodeNo, 0.00 AS UnitFactor, '2' AS Version"
            //+ " FROM Accounting.SalesInvoice A"
            //+ " INNER JOIN Accounting.SalesInvoiceDetail B"
            //+ " ON A.DocNumber = B.DocNumber"
            //+ " INNER JOIN Masterfile.Tax C "
            //+ " ON B.VTaxCode = C.TCode  "
            //+ " INNER JOIN Accounting.SubsiledgerNonInv D "
            //+ " ON A.DocNumber = D.DocNumber AND ISNULL(CounterDocNumber,'') = '' "
            //+ " WHERE A.CustomerCode = '" + aglCustomerCode.Text + "' AND (A.DocDate BETWEEN '" + dtpDateFrom.Text + "' AND '" + dtpDateTo.Text + "')"
            //+ " GROUP BY A.DocNumber, A.DocDate, A.CustomerCode,B.TransDoc, B.TransDate, B.DocNumber ORDER BY A.DocNumber, A.DocDate";
            
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

        private void Computation()
        {
            DataTable compute = new DataTable();

            compute = Gears.RetriveData2("SELECT CAST(SUM(CASE WHEN VTaxCode != 'NONV' THEN ISNULL(Price,0) * ISNULL(Qty,0) ELSE 0 END) AS decimal(15,2)) AS GrossVAT, "
            + " CAST(SUM(CASE WHEN VTaxCode = 'NONV' THEN ISNULL(Price,0) * ISNULL(Qty,0) ELSE 0 END) AS decimal(15,2)) AS GrossNonVAT, "
            + " CAST(SUM( (ISNULL(Price,0) * ISNULL(Qty,0)) * C.Rate) AS decimal(15,2)) AS VATAmount, "
            + " CAST(SUM(ISNULL(Price,0) * ISNULL(Qty,0)) + SUM( (ISNULL(Price,0) * ISNULL(Qty,0)) * C.Rate) AS decimal(15,2)) AS AmountDue "
            + " FROM Accounting.SalesInvoice A"
            + " INNER JOIN Accounting.SalesInvoiceDetail B"
            + " ON A.DocNumber = B.DocNumber"
            + " INNER JOIN Masterfile.Tax C "
            + " ON B.VTaxCode = C.TCode  "
            + " INNER JOIN Accounting.SubsiledgerNonInv D "
            + " ON A.DocNumber = D.DocNumber AND ISNULL(CounterDocNumber,'') = '' "
            + " WHERE A.CustomerCode = '" + aglCustomerCode.Text + "' AND (A.DocDate BETWEEN '" + dtpDateFrom.Text + "' AND '" + dtpDateTo.Text + "')"
            , Session["ConnString"].ToString());

            if (compute.Rows.Count != 0)
            {
                txtGross.Text = Convert.IsDBNull(compute.Rows[0]["GrossVAT"]) ? "0.00" : compute.Rows[0]["GrossVAT"].ToString();
                txtNonVatable.Text = Convert.IsDBNull(compute.Rows[0]["GrossNonVAT"]) ? "0.00" : compute.Rows[0]["GrossNonVAT"].ToString();
                txtVatAmount.Text = Convert.IsDBNull(compute.Rows[0]["VATAmount"]) ? "0.00" : compute.Rows[0]["VATAmount"].ToString();
                txtAmountDue.Text = Convert.IsDBNull(compute.Rows[0]["AmountDue"]) ? "0.00" : compute.Rows[0]["AmountDue"].ToString();

                if ((txtGross.Text == "0.00" || txtGross.Text == null) && (txtNonVatable.Text == "0.00" || txtNonVatable.Text == null) && 
                    (txtVatAmount.Text == "0.00" || txtVatAmount.Text == null) && (txtAmountDue.Text == "0.00" || txtAmountDue.Text == null))
                {
                    Session["CtrSlpchecker"] = "1";
                }
                else
                {
                    Session["CtrSlpchecker"] = null;
                }
                //txtGross.Text = compute.Rows[0]["GrossVAT"].ToString();
                //txtNonVatable.Text = compute.Rows[0]["GrossNonVAT"].ToString();
                //txtVatAmount.Text = compute.Rows[0]["VATAmount"].ToString();
                //txtAmountDue.Text = compute.Rows[0]["AmountDue"].ToString();
            }
            else
            {
                Session["CtrSlpchecker"] = "1";
                txtGross.Text = "0.00";
                txtNonVatable.Text = "0.00";
                txtVatAmount.Text = "0.00";
                txtAmountDue.Text = "0.00";
            }
        }
        public void CheckDates()
        {
            if (dtpDateTo.Date < dtpDateFrom.Date)
            {
                error = true;
                ErrorMsg = "Date From is greater than Date To!";
            }
            else if (dtpDateTo.Date > dtpDocDate.Date)
            {
                error = true;
                ErrorMsg = "Document Date is less than Date To!";
            }
            else
            {
                ErrorMsg = "Please check all fields!";
            }
        }

        protected void aglTransNo_Init(object sender, EventArgs e)
        {
            if (Session["CtrSlpDocNum"] != null)
            {
                sdsRefTrans.ConnectionString = Session["ConnString"].ToString();
                sdsRefTrans.SelectCommand = Session["CtrSlpDocNum"].ToString();
                aglTransNo.DataBind();
            }
        }

        protected void ButtonVisible()
        {
            if (!String.IsNullOrEmpty(dtpDateFrom.Text) && !String.IsNullOrEmpty(dtpDateTo.Text) && !String.IsNullOrEmpty(aglCustomerCode.Text) && String.IsNullOrEmpty(aglBizAccount.Text))
            {
                sdsRefTrans.ConnectionString = Session["ConnString"].ToString();
                Session["CtrSlpDocNum"] = "SELECT RecordID, TransType, DocNumber, DocDate, Reference, BizPartnerCode, AccountCode, SubsiCode, ProfitCenter, CostCenter, "
                    + " Amount, Applied FROM Accounting.SubsiLedgerNonInv WHERE TransType IN ('ACTSIN','ACTARM','ACTJOV','ACTJOV2','SLSDRC') AND ISNULL(CounterDocNumber,'') = '' AND ISNULL(Amount,0) <> ISNULL(Applied,0)"
                    // 03/03/2017   GC  Changed ORDER BY clause
                    //+ " AND BizPartnerCode = '" + aglCustomerCode.Text + "' AND (DocDate BETWEEN '" + dtpDateFrom.Text + "' AND '" + dtpDateTo.Text + "') ORDER BY TransType, DocNumber, DocDate";
                    + " AND BizPartnerCode = '" + aglCustomerCode.Text + "' AND (DocDate BETWEEN '" + dtpDateFrom.Text + "' AND '" + dtpDateTo.Text + "') ORDER BY DocDate, DocNumber, TransType";
                    // END
                sdsRefTrans.SelectCommand = Session["CtrSlpDocNum"].ToString();
                aglTransNo.DataBind();

                aglTransNo.ClientEnabled = true;
            }
            else if (!String.IsNullOrEmpty(dtpDateFrom.Text) && !String.IsNullOrEmpty(dtpDateTo.Text) && String.IsNullOrEmpty(aglCustomerCode.Text) && !String.IsNullOrEmpty(aglBizAccount.Text))
            {
                sdsRefTrans.ConnectionString = Session["ConnString"].ToString();
                Session["CtrSlpDocNum"] = "SELECT RecordID, TransType, DocNumber, DocDate, Reference, A.BizPartnerCode, AccountCode, SubsiCode, ProfitCenter, CostCenter, "
                    + " Amount, Applied FROM Accounting.SubsiLedgerNonInv A LEFT JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode "
                    + " WHERE TransType IN ('ACTSIN','ACTARM','ACTJOV','ACTJOV2','SLSDRC') AND ISNULL(CounterDocNumber,'') = '' AND ISNULL(Amount,0) <> ISNULL(Applied,0) "
                    + " AND (B.BusinessAccountCode = '" + aglBizAccount.Text + "' OR A.BizPartnerCode = '" + aglBizAccount.Text + "') AND (DocDate BETWEEN '" + dtpDateFrom.Text + "' AND '" + dtpDateTo.Text + "')"
                    // 03/03/2017   GC  Changed ORDER BY clause
                    //+ " ORDER BY TransType, A.BizPartnerCode, DocNumber, DocDate";
                    + " ORDER BY DocDate, DocNumber, TransType";
                    // END
                sdsRefTrans.SelectCommand = Session["CtrSlpDocNum"].ToString();
                aglTransNo.DataBind();

                aglTransNo.ClientEnabled = true;
            }
            else
            {
                aglTransNo.ClientEnabled = false;
                //Session["CtrSlpButton"] = null;
                //btnGenerate.ClientVisible = false;
            }
        }
    }
}