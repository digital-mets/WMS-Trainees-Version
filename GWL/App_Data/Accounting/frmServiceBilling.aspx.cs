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
    public partial class frmServiceBilling : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string ErrorMsg = "";

        Entity.ServiceBilling _Entity = new ServiceBilling();//Calls entity odsHeader

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

            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                aglCurrency.Value = _Entity.Currency.ToString();
                dtpDateFrom.Text = String.IsNullOrEmpty(_Entity.DateFrom.ToString()) ? null : Convert.ToDateTime(_Entity.DateFrom.ToString()).ToShortDateString();
                dtpDateTo.Text = String.IsNullOrEmpty(_Entity.DateTo.ToString()) ? null : Convert.ToDateTime(_Entity.DateTo.ToString()).ToShortDateString();
                aglServiceCode.Value = _Entity.ServiceCode.ToString();
                txtDescription.Value = _Entity.Description.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                aglVATCode.Value = _Entity.VTaxCode.ToString();
                aglATCCode.Value = _Entity.CreditWithholdingTax.ToString();
                speGross.Value = _Entity.GrossVatAmount.ToString();
                speNonVat.Value = _Entity.GrossNonVatAmount.ToString();
                txtVATAmount.Value = _Entity.VATAmount.ToString();
                txtTaxAmount.Value = _Entity.WithholdingTax.ToString();
                speNetAmount.Value = _Entity.NetAmount.ToString();

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
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;

                gvJournal.DataSourceID = "odsJournalEntry";
                gvJournal.DataBind();
                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                            if (String.IsNullOrEmpty(aglCurrency.Text))
                            {
                                aglCurrency.Text = "PHP";
                            }
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                            aglCurrency.Text = "PHP";
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        if (String.IsNullOrEmpty(aglCurrency.Text))
                        {
                            aglCurrency.Text = "PHP";
                        }
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
            }      
            
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTSEB";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ServiceBilling_Validate(gparam);
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
            gparam._TransType = "ACTSEB";
            gparam._Table = "Accounting.ServiceBilling";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ServiceBilling_Post(gparam);
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = false;
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
        protected void Memo_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxMemo memo = sender as ASPxMemo;
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

        protected void GrossVat(object sender, EventArgs e)//Control for all date editor
        {
            //DataTable vatrate = new DataTable();
            //DataTable atcrate = new DataTable();
            //decimal vat = 0;
            //decimal tax = 0;

            //if (String.IsNullOrEmpty(aglVATCode.Text) || String.IsNullOrEmpty(aglATCCode.Text))
            //{
            //    cp.JSProperties["cp_message"] = "No Tax Code or ATC Code is selected!";
            //}
            //else if (!String.IsNullOrEmpty(aglVATCode.Text) && String.IsNullOrEmpty(aglATCCode.Text))
            //{
            //    vatrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.Tax WHERE TCode ='" + aglVATCode.Text.Trim() + "'");

            //    vat = Convert.ToDecimal(speGross.Text) * Convert.ToDecimal(vatrate.Rows[0]["Rate"].ToString());

            //}
            //else if (String.IsNullOrEmpty(aglVATCode.Text) && !String.IsNullOrEmpty(aglATCCode.Text))
            //{
            //    atcrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.ATC WHERE ATCCode ='" + aglATCCode.Text.Trim() + "'");

            //    tax = Convert.ToDecimal(speGross.Text) * Convert.ToDecimal(atcrate.Rows[0]["Rate"].ToString());
            //}
            //else if (!String.IsNullOrEmpty(aglVATCode.Text) || !String.IsNullOrEmpty(aglATCCode.Text))
            //{
            //    vatrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.Tax WHERE TCode ='" + aglVATCode.Text.Trim() + "'");
            //    atcrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.ATC WHERE ATCCode ='" + aglATCCode.Text.Trim() + "'");

            //    vat = Convert.ToDecimal(speGross.Text) * Convert.ToDecimal(vatrate.Rows[0]["Rate"].ToString());
            //    tax = Convert.ToDecimal(speGross.Text) * Convert.ToDecimal(atcrate.Rows[0]["Rate"].ToString());
            //}

            //txtVATAmount.Text = vat.ToString();
            //txtTaxAmount.Text = tax.ToString();

        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
                _Entity.DocNumber = txtDocNumber.Value.ToString();
                _Entity.DocDate = dtpDocDate.Text;
                _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Value.ToString();
                _Entity.Currency = String.IsNullOrEmpty(aglCurrency.Text) ? "PHP" : aglCurrency.Value.ToString();
                _Entity.DateFrom = dtpDateFrom.Text;
                _Entity.DateTo = dtpDateTo.Text;
                _Entity.ServiceCode = String.IsNullOrEmpty(aglServiceCode.Text) ? null : aglServiceCode.Value.ToString();
                _Entity.Description = txtDescription.Text;
                _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
                _Entity.Connection = Session["ConnString"].ToString();
                _Entity.VTaxCode = String.IsNullOrEmpty(aglVATCode.Text) ? null : aglVATCode.Value.ToString();
                _Entity.CreditWithholdingTax = String.IsNullOrEmpty(aglATCCode.Text) ? null : aglATCCode.Value.ToString();
                _Entity.GrossVatAmount = String.IsNullOrEmpty(speGross.Text) ? 0 : Convert.ToDecimal(speGross.Value.ToString());
                _Entity.GrossNonVatAmount = String.IsNullOrEmpty(speNonVat.Text) ? 0 : Convert.ToDecimal(speNonVat.Value.ToString());
                _Entity.VATAmount = String.IsNullOrEmpty(txtVATAmount.Text) ? 0 : Convert.ToDecimal(txtVATAmount.Value.ToString());
                _Entity.WithholdingTax = String.IsNullOrEmpty(txtTaxAmount.Text) ? 0 : Convert.ToDecimal(txtTaxAmount.Value.ToString());
                _Entity.NetAmount = String.IsNullOrEmpty(speNetAmount.Text) ? 0 : Convert.ToDecimal(speNetAmount.Value.ToString());

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
                    
                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.ServiceBilling", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        Post();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
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

                case "Update":
                    
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Accounting.ServiceBilling", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError1))
                    {
                        cp.JSProperties["cp_message"] = strError1;
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
                        Post();
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

                case "ServiceDetail":

                    DataTable a = new DataTable();

                    a = Gears.RetriveData2("SELECT ISNULL(Description,'-') Description FROM Masterfile.Service WHERE ServiceCode ='" + aglServiceCode.Text.Trim() + "'", Session["ConnString"].ToString());
                    
                    if (a.Rows.Count != 0)
                    {
                        txtDescription.Text = a.Rows[0]["Description"].ToString();
                    }
                    break;

                case "Customer":

                    DataTable b = new DataTable();

                    b = Gears.RetriveData2("SELECT DISTINCT ISNULL(TaxCode,'') AS Tax, ISNULL(ATCCode,'') AS WHT FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode='" + aglCustomerCode.Text.Trim() + "'", Session["ConnString"].ToString());
                    
                    if (b.Rows.Count != 0)
                    {
                        aglVATCode.Text = b.Rows[0]["Tax"].ToString();
                        aglATCCode.Text = b.Rows[0]["WHT"].ToString();
                    }

                    txtVATAmount.Text = "0.00";
                    txtTaxAmount.Text = "0.00";
                    speNetAmount.Text = "0.00";

                    DataTable get = Gears.RetriveData2("DECLARE @MaxDate datetime "
                    + " SELECT @MaxDate = MAX(SubmittedDate) FROM Accounting.ServiceBilling "
                    + " WHERE CustomerCode = '" + aglCustomerCode.Text + "' AND ISNULL(CancelledBy,'') != '' "
                    + " SELECT DATEADD(DAY,1,DateTo) AS DateTo FROM Accounting.ServiceBilling "
                    + " WHERE CustomerCode = '" + aglCustomerCode.Text + "' AND ISNULL(CancelledBy,'') != '' "
                    + " AND SubmittedDate = @MaxDate", Session["ConnString"].ToString());

                    if (get.Rows.Count > 0)
                    {
                        string date = "";
                        date = Convert.ToDateTime(get.Rows[0]["DateTo"].ToString()).ToShortDateString();
                        dtpDateFrom.Text = date;
                    }
                    else
                    {
                        dtpDateFrom.Text = DateTime.Now.ToShortDateString();
                    }

                    break;

                case "Gross":

                    DataTable vatrate = new DataTable();
                    DataTable atcrate = new DataTable();
                    decimal vat = 0;
                    decimal tax = 0;
                    decimal gross = String.IsNullOrEmpty(speGross.Text) ? 0 : Convert.ToDecimal(speGross.Text);
                    decimal nonvat = String.IsNullOrEmpty(speNonVat.Text) ? 0 : Convert.ToDecimal(speNonVat.Text);
                    decimal net = 0;


                    if (String.IsNullOrEmpty(aglVATCode.Text) && String.IsNullOrEmpty(aglATCCode.Text))
                    {
                        cp.JSProperties["cp_message"] = "No Tax Code or ATC Code is selected!";
                    }
                    else if (!String.IsNullOrEmpty(aglVATCode.Text) && String.IsNullOrEmpty(aglATCCode.Text))
                    {
                        //vatrate = Gears.RetriveData2("SELECT (CASE WHEN Rate = 0 THEN 1 ELSE ISNULL(Rate,1) END) AS Rate FROM Masterfile.Tax WHERE TCode ='" + aglVATCode.Text.Trim() + "'");
                        vatrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.Tax WHERE TCode ='" + aglVATCode.Text.Trim() + "'", Session["ConnString"].ToString());
                        
                        vat = gross * Convert.ToDecimal(vatrate.Rows[0]["Rate"].ToString());
                    }
                    else if (String.IsNullOrEmpty(aglVATCode.Text) && !String.IsNullOrEmpty(aglATCCode.Text))
                    {
                        //atcrate = Gears.RetriveData2("SELECT (CASE WHEN Rate = 0 THEN 1 ELSE ISNULL(Rate,1) END) AS Rate FROM Masterfile.ATC WHERE ATCCode ='" + aglATCCode.Text.Trim() + "'");
                        atcrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.ATC WHERE ATCCode ='" + aglATCCode.Text.Trim() + "'", Session["ConnString"].ToString());

                        tax = gross * Convert.ToDecimal(atcrate.Rows[0]["Rate"].ToString());
                    }
                    else if (!String.IsNullOrEmpty(aglVATCode.Text) || !String.IsNullOrEmpty(aglATCCode.Text))
                    {
                        //vatrate = Gears.RetriveData2("SELECT (CASE WHEN Rate = 0 THEN 1 ELSE ISNULL(Rate,1) END) AS Rate FROM Masterfile.Tax WHERE TCode ='" + aglVATCode.Text.Trim() + "'");
                        //atcrate = Gears.RetriveData2("SELECT (CASE WHEN Rate = 0 THEN 1 ELSE ISNULL(Rate,1) END) AS Rate FROM Masterfile.ATC WHERE ATCCode ='" + aglATCCode.Text.Trim() + "'");
                        vatrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.Tax WHERE TCode ='" + aglVATCode.Text.Trim() + "'", Session["ConnString"].ToString());
                        atcrate = Gears.RetriveData2("SELECT ISNULL(Rate,0) AS Rate FROM Masterfile.ATC WHERE ATCCode ='" + aglATCCode.Text.Trim() + "'", Session["ConnString"].ToString());

                        vat = gross * Convert.ToDecimal(vatrate.Rows[0]["Rate"].ToString());
                        tax = gross * Convert.ToDecimal(atcrate.Rows[0]["Rate"].ToString());
                    }

                    net = (gross + nonvat + vat) - tax;
                    
                    txtVATAmount.Text = vat.ToString("0.00");
                    txtTaxAmount.Text = tax.ToString("0.00");
                    speNetAmount.Text = net.ToString("0.00");


                    break;

                case "FreezeObjects":
                    txtDocNumber.ReadOnly = true;
                    dtpDocDate.ReadOnly = true;
                    dtpDocDate.DropDownButton.Enabled = false;
                    aglCustomerCode.ReadOnly = true;
                    aglCustomerCode.DropDownButton.Enabled = false;
                    dtpDateFrom.ReadOnly = true;
                    dtpDateFrom.DropDownButton.Enabled = false;
                    aglServiceCode.ReadOnly = true;
                    aglServiceCode.DropDownButton.Enabled = false;
                    dtpDateTo.ReadOnly = true;
                    dtpDateTo.DropDownButton.Enabled = false;
                    txtDescription.ReadOnly = true;
                    aglCurrency.ReadOnly = true;
                    aglCurrency.DropDownButton.Enabled = false;
                    memRemarks.ReadOnly = true;
                    aglVATCode.ReadOnly = true;
                    aglVATCode.DropDownButton.Enabled = false;
                    aglATCCode.ReadOnly = true;
                    aglATCCode.DropDownButton.Enabled = false;
                    speGross.ReadOnly = true;
                    speNonVat.ReadOnly = true;
                    txtHField1.ReadOnly = true;
                    txtHField2.ReadOnly = true;
                    txtHField3.ReadOnly = true;
                    txtHField4.ReadOnly = true;
                    txtHField5.ReadOnly = true;
                    txtHField6.ReadOnly = true;
                    txtHField7.ReadOnly = true;
                    txtHField8.ReadOnly = true;
                    txtHField9.ReadOnly = true;
                    break;
            }
        }
        protected void GetVat()
        {

            DataTable getvat = Gears.RetriveData2("select Rate from Masterfile.BPCustomerInfo A inner join Masterfile.Tax B on A.TaxCode = B.TCode where BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());

            if (getvat.Rows.Count > 0)
            {
                cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
            }
            else
            {
                cp.JSProperties["cp_vatrate"] = "0";
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
            //        GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //        if (dataColumn == null) continue;
            //        if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber"
            //            && dataColumn.FieldName != "DeliveredQty" && dataColumn.FieldName != "DiscountRate" && dataColumn.FieldName != "SubstituteItem"
            //            && dataColumn.FieldName != "StatusCode" && dataColumn.FieldName != "FullDesc" && dataColumn.FieldName != "SubstituteColor"
            //            && dataColumn.FieldName != "VATCode" && dataColumn.FieldName != "IsVAT" && dataColumn.FieldName != "BaseQty"
            //            && dataColumn.FieldName != "BarcodeNo" && dataColumn.FieldName != "UnitFreight" && dataColumn.FieldName != "BulkUnit" && dataColumn.FieldName != "BulkQty"
            //            && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3"
            //            && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6"
            //            && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9")
            //        {
            //            e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //        }
            //    }

            //    //Checking for non existing Codes..
            //    ItemCode = e.NewValues["ItemCode"].ToString();
            //    ColorCode = e.NewValues["ColorCode"].ToString();
            //    ClassCode = e.NewValues["ClassCode"].ToString();
            //    SizeCode = e.NewValues["SizeCode"].ToString();
            //    DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'");
            //    if (item.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //    }
            //    DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
            //    if (color.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //    }
            //    DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
            //    if (clss.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //    }
            //    DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
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

            //if (Session["Datatable"] == "1" && check == true)
            //{
            //    e.Handled = true;
            //    DataTable source = GetSelectedVal();

            //    foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //    {
            //        try
            //        {
            //            object[] keys = { values.Keys[0] };
            //            source.Rows.Remove(source.Rows.Find(keys));
            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }
            //    }

            //    foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
            //    {
            //        //var LineNumber = values.NewValues["LineNumber"];
            //        var ItemCode = values.NewValues["ItemCode"];
            //        var FullDesc = values.NewValues["FullDesc"];
            //        var ColorCode = values.NewValues["ColorCode"];
            //        var ClassCode = values.NewValues["ClassCode"];
            //        var SizeCode = values.NewValues["SizeCode"];
            //        var OrderQty = values.NewValues["OrderQty"];
            //        var Unit = values.NewValues["Unit"];
            //        var BulkQty = values.NewValues["BulkQty"];
            //        var BulkUnit = values.NewValues["BulkUnit"];
            //        var UnitPrice = values.NewValues["UnitPrice"];
            //        var IsVAT = values.NewValues["IsVAT"];
            //        var UnitFreight = values.NewValues["UnitFreight"];
            //        var VATCode = values.NewValues["VATCode"];
            //        var DiscountRate = values.NewValues["DiscountRate"];
            //        var DeliveredQty = values.NewValues["DeliveredQty"];
            //        var SubstituteItem = values.NewValues["SubstituteItem"];
            //        var SubstituteColor = values.NewValues["SubstituteColor"];
            //        var BaseQty = values.NewValues["BaseQty"];
            //        var StatusCode = values.NewValues["StatusCode"];
            //        var BarcodeNo = values.NewValues["BarcodeNo"];
            //        var Field1 = values.NewValues["Field1"];
            //        var Field2 = values.NewValues["Field2"];
            //        var Field3 = values.NewValues["Field3"];
            //        var Field4 = values.NewValues["Field4"];
            //        var Field5 = values.NewValues["Field5"];
            //        var Field6 = values.NewValues["Field6"];
            //        var Field7 = values.NewValues["Field7"];
            //        var Field8 = values.NewValues["Field8"];
            //        var Field9 = values.NewValues["Field9"];
            //        //source.Rows.Add(LineNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, OrderQty, Unit, BulkQty, BulkUnit, UnitPrice, IsVAT, UnitFreight, VATCode, DiscountRate, DeliveredQty, SubstituteItem, SubstituteColor, BaseQty, StatusCode, BarcodeNo, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
            //        source.Rows.Add(ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, OrderQty, Unit, BulkQty, BulkUnit, UnitPrice, IsVAT, UnitFreight, VATCode, DiscountRate, DeliveredQty, SubstituteItem, SubstituteColor, BaseQty, StatusCode, BarcodeNo, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
               
            //    }

                //foreach (ASPxDataUpdateValues values in e.UpdateValues)
                //{
                //    object[] keys = { values.Keys[0] };
                //    DataRow row = source.Rows.Find(keys);
                //    row["ItemCode"] = values.NewValues["ItemCode"];
                //    row["FullDesc"] = values.NewValues["FullDesc"];
                //    row["ColorCode"] = values.NewValues["ColorCode"];
                //    row["ClassCode"] = values.NewValues["ClassCode"];
                //    row["SizeCode"] = values.NewValues["SizeCode"];
                //    row["OrderQty"] = values.NewValues["OrderQty"];
                //    row["Unit"] = values.NewValues["Unit"];
                //    row["BulkQty"] = values.NewValues["BulkQty"];
                //    row["BulkUnit"] = values.NewValues["BulkUnit"];
                //    row["UnitPrice"] = values.NewValues["UnitPrice"];
                //    row["IsVAT"] = values.NewValues["IsVAT"];
                //    row["UnitFreight"] = values.NewValues["UnitFreight"];
                //    row["VATCode"] = values.NewValues["VATCode"];
                //    row["DiscountRate"] = values.NewValues["DiscountRate"];
                //    row["DeliveredQty"] = values.NewValues["DeliveredQty"];
                //    row["SubstituteItem"] = values.NewValues["SubstituteItem"];
                //    row["SubstituteColor"] = values.NewValues["SubstituteColor"];
                //    row["BaseQty"] = values.NewValues["BaseQty"];
                //    row["StatusCode"] = values.NewValues["StatusCode"];
                //    row["BarcodeNo"] = values.NewValues["BarcodeNo"];
                //    row["Field1"] = values.NewValues["Field1"];
                //    row["Field2"] = values.NewValues["Field2"];
                //    row["Field3"] = values.NewValues["Field3"];
                //    row["Field4"] = values.NewValues["Field4"];
                //    row["Field5"] = values.NewValues["Field5"];
                //    row["Field6"] = values.NewValues["Field6"];
                //    row["Field7"] = values.NewValues["Field7"];
                //    row["Field8"] = values.NewValues["Field8"];
                //    row["Field9"] = values.NewValues["Field9"];
                //}
                    
                //foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                //{
                //    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                //    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                //    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                //    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                //    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                //    _EntityDetail.OrderQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OrderQty"]) ? 0 : dtRow["OrderQty"]);
                //    _EntityDetail.Unit = dtRow["Unit"].ToString();
                //    _EntityDetail.BulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BulkQty"]) ? 0 : dtRow["BulkQty"]);
                //    _EntityDetail.BulkUnit = dtRow["BulkUnit"].ToString();
                //    _EntityDetail.UnitPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitPrice"]) ? 0 : dtRow["UnitPrice"]);
                //    _EntityDetail.IsVAT = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVAT"].ToString()) ? false : dtRow["IsVAT"]);
                //    //_EntityDetail.IsVAT = Convert.ToBoolean(Convert.ToInt32(Convert.IsDBNull(dtRow["IsVAT"].ToString()) ? 0 : dtRow["IsVAT"]));
                //    _EntityDetail.UnitFreight = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFreight"]) ? 0 : dtRow["UnitFreight"]);
                //    _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                //    _EntityDetail.DiscountRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["DiscountRate"]) ? 0 : dtRow["DiscountRate"]);
                //    _EntityDetail.DeliveredQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["DeliveredQty"]) ? 0 : dtRow["DeliveredQty"]);
                //    _EntityDetail.SubstituteItem = dtRow["SubstituteItem"].ToString();
                //    _EntityDetail.SubstituteColor = dtRow["SubstituteColor"].ToString();
                //    _EntityDetail.BaseQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BaseQty"]) ? 0 : dtRow["BaseQty"]);
                //    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                //    _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
                //    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                //    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                //    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                //    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                //    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                //    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                //    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                //    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                //    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                //    _EntityDetail.AddSalesOrderDetail(_EntityDetail);
                //}
            //}            
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["sodetail"] = null;
            //}

            //if (Session["sodetail"] != null)
            //{
            //    gv1.DataSourceID = sdsQuotationDetail.ID;
            //    sdsQuotationDetail.FilterExpression = Session["sodetail"].ToString();
            //}
        }

        //private DataTable GetSelectedVal()
        //{
        //    DataTable dt = new DataTable();
        //    string[] selectedValues = aglQuote.Text.Split(';');
        //    CriteriaOperator selectionCriteria = new InOperator(aglQuote.KeyFieldName, selectedValues);
        //    sdsQuotationDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["sodetail"] = sdsQuotationDetail.FilterExpression;
        //    gv1.DataSourceID = sdsQuotationDetail.ID;
        //    gv1.DataBind();
        //    Session["Datatable"] = "1";

        //    foreach (GridViewColumn col in gv1.VisibleColumns)
        //    {
        //        GridViewDataColumn dataColumn = col as GridViewDataColumn;
        //        if (dataColumn == null) continue;
        //        dt.Columns.Add(dataColumn.FieldName);
        //    }
        //    for (int i = 0; i < gv1.VisibleRowCount; i++)
        //    {
        //        DataRow row = dt.Rows.Add();
        //        foreach (DataColumn col in dt.Columns)
        //            row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
        //    }

        //    dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
        //    dt.Columns["LineNumber"]};

        //    return dt;
        //}    
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }
        //protected void dtpTargetDelivery_Init(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["entry"] == "N")
        //    {
        //        dtpTargetDate.Date = DateTime.Now;
        //    }
        //    else if (Request.QueryString["entry"] == "E")
        //    {
        //        if (String.IsNullOrEmpty(dtpTargetDate.Text))
        //        {
        //            dtpTargetDate.Date = DateTime.Now;
        //        }
        //        else
        //        {
        //            dtpTargetDate.Text = Convert.ToDateTime(_Entity.TargetDeliveryDate.ToString()).ToShortDateString(); ;
        //        }
        //    }
        //}
        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsBizPartnerCus.ConnectionString = Session["ConnString"].ToString();
            sdsTaxCode.ConnectionString = Session["ConnString"].ToString();
            sdsATC.ConnectionString = Session["ConnString"].ToString();
            sdsService.ConnectionString = Session["ConnString"].ToString();
            sdsCurrency.ConnectionString = Session["ConnString"].ToString();
        }
        public void CheckDates()
        {
            if (dtpDateTo.Date < dtpDateFrom.Date)
            {
                error = true;
                ErrorMsg = "Date From is greater than Date To!";
            }
            else
            {
                ErrorMsg = "Please check all fields!";
            }
        }

    }
}