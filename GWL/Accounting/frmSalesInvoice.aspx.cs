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
    public partial class frmSalesInvoice : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string val = "";

        Entity.SalesInvoice _Entity = new SalesInvoice();//Calls entity odsHeader
        Entity.SalesInvoice.SalesInvoiceDetail _EntityDetail = new SalesInvoice.SalesInvoiceDetail();//Call entity sdsDetail

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

            txtDocNumber.ReadOnly = true;
            GetDiscount();

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["aglTransNo_Init"] = null;
                Session["FilterExpression"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    gv1.KeyFieldName = "LineNumber;SONumber;TransType;TransDoc";
                    //gv1.KeyFieldName = "LineNumber";
                    aglCurrency.Value = "PHP";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                //else
                //{
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                txtName.Value = _Entity.Name.ToString();
                txtAddress.Value = _Entity.Address.ToString();
                txtTIN.Value = _Entity.TIN.ToString();
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                //aglCurrency.Value = _Entity.Currency.ToString();
                if (String.IsNullOrEmpty(_Entity.Currency.ToString()))
                {
                    aglCurrency.Value = "PHP";
                }
                else
                {
                    aglCurrency.Value = _Entity.Currency.ToString();
                }
                //aglSONumber.Text = _Entity.RefSO.ToString();
                val = _Entity.RefTrans.ToString();
                chkVatable.Value = _Entity.Vatable;
                chkIsWithSO.Value = _Entity.IsWithSO;
                txtReference.Value = _Entity.Reference;
                txtVATBefore.Value = _Entity.VatBeforeDisc.ToString();
                txtNonVATBefore.Value = _Entity.NonVatBeforeDisc.ToString();
                txtVATAfter.Value = _Entity.VatAfterDisc.ToString();
                txtNonVATAfter.Value = _Entity.NonVatAfterDisc.ToString();
                txtDiscount.Value = _Entity.TotalDiscount.ToString();
                txtSCDisc.Value = _Entity.SCDiscount.ToString();
                txtPWDDisc.Value = _Entity.PWDDiscount.ToString();
                txtDiplomatDisc.Value = _Entity.DiplomatDiscount.ToString();
                txtVATAmount.Value = _Entity.VATAmount.ToString();
                txtAmountDue.Value = _Entity.TotalAmountDue.ToString();

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

                gv1.KeyFieldName = "LineNumber;SONumber;TransType;TransDoc";

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                            Initialize();
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                            chkIsWithSO.Checked = true;
                        }
                        Check();
                        GetDiscount();
                        Session["Datatable"] = "1";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        Initialize();
                        GetDiscount();
                        Check();
                        if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                        {
                            GetVat();
                        }
                        aglTransNo.Text = val;
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        Initialize();
                        glcheck.ClientVisible = false;
                        aglTransNo.Text = val;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        Initialize();
                        aglTransNo.Text = val;
                        break;
                }


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    //gv1.DataSourceID = null;
                    gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                
                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    //gv1.DataSourceID = "odsDetail";
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                    gvJournal.DataSourceID = "odsJournalEntry";
                }

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = null;
                //    popup.ShowOnPageLoad = false;
                //}
                //else
                //{
                //    gv1.DataSourceID = "odsDetail";
                //    popup.ShowOnPageLoad = true;
                //}

            }

            DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.SalesInvoiceDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

            gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

            //if (gv1.DataSource != null)
            //{
            //    gv1.DataSourceID = null;
            //}
            gvJournal.DataSourceID = "odsJournalEntry";

        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTSIN";
            gparam._Connection =Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.SalesInvoice_Validate(gparam);
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
            gparam._TransType = "ACTSIN";
            gparam._Table = "Accounting.SalesInvoice";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.SalesInvoice_Post(gparam);
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
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
            //else
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = false;
            //}
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
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";           

            if (e.Parameters.Contains("DiscountType"))
            {
                DataTable DiscountRate = Gears.RetriveData2("SELECT DISTINCT CONVERT(decimal(15,2), Value) AS Rate FROM IT.SystemSettings WHERE Description = '" + itemcode.Trim() + "' AND Code LIKE 'DISC_%'", Session["ConnString"].ToString());

                foreach (DataRow dt in DiscountRate.Rows)
                {
                    codes = dt["Rate"].ToString();
                }

                itemlookup.JSProperties["cp_identifier"] = "discount";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("VATCode"))
            {
                DataTable VAT = Gears.RetriveData2("SELECT DISTINCT Rate FROM Masterfile.Tax WHERE TCode = '" + itemcode.Trim() + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in VAT.Rows)
                {
                    codes = dt["Rate"].ToString(); //+ ";";
                }

                itemlookup.JSProperties["cp_identifier"] = "vat";
                itemlookup.JSProperties["cp_codes"] = codes;
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
                _Entity.Address = String.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Text;
                _Entity.TIN = String.IsNullOrEmpty(txtTIN.Text) ? null : txtTIN.Text;
                _Entity.Reference = String.IsNullOrEmpty(txtReference.Text) ? null : txtReference.Text;
                _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Value.ToString();
                //_Entity.RefSO = String.IsNullOrEmpty(aglSONumber.Text) ? null : aglSONumber.Text;
                _Entity.RefTrans = String.IsNullOrEmpty(aglTransNo.Text) ? null : aglTransNo.Text;
                _Entity.Currency = String.IsNullOrEmpty(aglCurrency.Text) ? null : aglCurrency.Value.ToString();
                _Entity.Vatable = Convert.ToBoolean(chkVatable.Value.ToString());
                _Entity.IsWithSO = Convert.ToBoolean(chkIsWithSO.Value.ToString());
                _Entity.VatBeforeDisc = String.IsNullOrEmpty(txtVATBefore.Text) ? 0 : Convert.ToDecimal(txtVATBefore.Value.ToString());
                _Entity.NonVatBeforeDisc = String.IsNullOrEmpty(txtNonVATBefore.Text) ? 0 : Convert.ToDecimal(txtNonVATBefore.Value.ToString());
                _Entity.VatAfterDisc = String.IsNullOrEmpty(txtVATAfter.Text) ? 0 : Convert.ToDecimal(txtVATAfter.Value.ToString());
                _Entity.NonVatAfterDisc = String.IsNullOrEmpty(txtNonVATAfter.Text) ? 0 : Convert.ToDecimal(txtNonVATAfter.Value.ToString());
                _Entity.TotalDiscount = String.IsNullOrEmpty(txtDiscount.Text) ? 0 :  Convert.ToDecimal(txtDiscount.Value.ToString());
                _Entity.SCDiscount = String.IsNullOrEmpty(txtSCDisc.Text) ? 0 : Convert.ToDecimal(txtSCDisc.Value.ToString());
                _Entity.PWDDiscount = String.IsNullOrEmpty(txtPWDDisc.Text) ? 0 : Convert.ToDecimal(txtPWDDisc.Value.ToString());
                _Entity.DiplomatDiscount = String.IsNullOrEmpty(txtDiplomatDisc.Text) ? 0 : Convert.ToDecimal(txtDiplomatDisc.Value.ToString());
                _Entity.VATAmount = String.IsNullOrEmpty(txtVATAmount.Text) ? 0 : Convert.ToDecimal(txtVATAmount.Value.ToString());
                _Entity.TotalAmountDue = String.IsNullOrEmpty(txtAmountDue.Text) ? 0 : Convert.ToDecimal(txtAmountDue.Value.ToString());
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
              

            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();
                    
                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.SalesInvoice", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv1.DataSourceID = sdsTransDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        //Session["Datatable"] = null;
                        Post();
                        Validate();
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

                    gv1.UpdateEdit();             
                    
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Accounting.SalesInvoice", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv1.DataSourceID = sdsTransDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        //Session["Datatable"] = null;
                        Post();
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

                case "AddZeroDetail":

                    gv1.UpdateEdit();
                    
                    string strError2 = Functions.Submitted(_Entity.DocNumber, "Accounting.SalesInvoice", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.SalesInvoiceDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        Post();
                        Validate();
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

                    gv1.UpdateEdit();       
                    
                    string strError3 = Functions.Submitted(_Entity.DocNumber, "Accounting.SalesInvoice", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.SalesInvoiceDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        Post();
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
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    gv1.DataSource = null;
                    break;

                case "WithSO":

                    gv1.Columns["SONumber"].Width = 80;
                    Session["Datatable"] = "0";
                    gv1.DataSource = null;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    Check();
                    //aglSONumber.Text = "";
                    aglTransNo.Text = "";
                    cp.JSProperties["cp_generated"] = true;

                    break;

                case "WithoutSO":

                    gv1.Columns["SONumber"].Width = 0;                    
                    Session["Datatable"] = "0";
                    gv1.DataSource = null;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    //aglSONumber.Text = "";
                    aglTransNo.Text = "";
                    Check();
                    cp.JSProperties["cp_generated"] = true;

                    break;

                case "FilterSO":

                    Session["Datatable"] = "0";
                    gv1.DataSource = null;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();

                    if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                    {
                        DataTable info = Gears.RetriveData2("SELECT A.Name AS Name, DeliveryAddress, TIN, CASE WHEN ISNULL(C.Rate,0) != 0 THEN 1 ELSE 0 END AS VAT FROM Masterfile.BPCustomerInfo A LEFT JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode INNER JOIN Masterfile.Tax C ON A.TaxCode = C.TCode WHERE A.BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());
                        if (info.Rows.Count != 0)
                        {
                            txtName.Text = info.Rows[0]["Name"].ToString();
                            txtAddress.Text = info.Rows[0]["DeliveryAddress"].ToString();
                            txtTIN.Text = info.Rows[0]["TIN"].ToString();
                            chkVatable.Value = Convert.ToBoolean(info.Rows[0]["VAT"]);
                        }
                    }

                    Check();
                    cp.JSProperties["cp_generated"] = true;    

                    break;

                case "Filter":
                    
                    //aglSONumber.ClientEnabled = true;
                    //aglTransNo.ClientEnabled = true;
                    //string[] selectedValues = aglSONumber.Text.Split(';');
                    //CriteriaOperator selectionCriteria = new InOperator("RefSONum", selectedValues);
                    //string filter = selectionCriteria.ToString();

                    //sdsRefTrans.SelectCommand = "SELECT DocNumber FROM Sales.DeliveryReceipt WHERE ISNULL(SubmittedBy,'') != '' AND ISNULL(InvoiceDocNum,'') = '' AND " + filter + " UNION ALL "
                    //                            + "SELECT A.DocNumber FROM Sales.SalesReturn A INNER JOIN Sales.DeliveryReceipt B ON A.ReferenceDRNo = B.DocNumber WHERE ISNULL(A.SubmittedBy,'') != '' AND " + filter + "";
                    //aglTransNo.DataBind();

                    //GetVat();
                    //cp.JSProperties["cp_unitcost"] = true;

                    break;

                case "Details":
                    
                    GetSelectedVal();
                    Check();
                    //Initialize();
                    GetVat();
                    cp.JSProperties["cp_generated"] = true;                    

                    break;

                case "Rate":
                    GetRate();
                    Check();
                    break;

                case "unitcost":
                    GetVat();
                    cp.JSProperties["cp_unitcost"] = true;
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
            //    DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "' AND ItemCode = '" + ItemCode + "'");
            //    if (color.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //    }
            //    DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "' AND ItemCode = '" + ItemCode + "'");
            //    if (clss.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //    }
            //    DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "' AND ItemCode = '" + ItemCode + "'");
            //    if (size.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
            //    }

            //    if (e.Errors.Count > 0)
            //    {
            //        error = true; //bool to cancel adding/updating if true
            //    }
            
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

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        //object[] keys = { values.Keys["LineNumber"] };
                        object[] keys = { values.Keys["LineNumber"], values.Keys["SONumber"], values.Keys["TransDoc"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"], values.NewValues["SONumber"], values.NewValues["TransType"], values.NewValues["TransDoc"] };
                    DataRow row = source.Rows.Find(keys);
                    row["SONumber"] = values.NewValues["SONumber"];
                    row["TransType"] = values.NewValues["TransType"];
                    row["TransDoc"] = values.NewValues["TransDoc"];
                    row["TransDate"] = values.NewValues["TransDate"];
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Qty"] = values.NewValues["Qty"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["Price"] = values.NewValues["Price"];
                    row["AmountBeforeDisc"] = values.NewValues["AmountBeforeDisc"];
                    row["DiscountAmount"] = values.NewValues["DiscountAmount"];
                    row["VTaxCode"] = values.NewValues["VTaxCode"];
                    row["DiscountType"] = values.NewValues["DiscountType"];
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
                    row["Rate"] = values.NewValues["Rate"];
                    row["SDRate"] = values.NewValues["SDRate"];
                    row["SDComputedAmt"] = values.NewValues["SDComputedAmt"];
                    row["AverageCost"] = values.NewValues["AverageCost"];
                    row["UnitCost"] = values.NewValues["UnitCost"];
                }
                    
                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {

                    _EntityDetail.SONumber = dtRow["SONumber"].ToString();
                    _EntityDetail.TransType = dtRow["TransType"].ToString();
                    _EntityDetail.TransDoc = dtRow["TransDoc"].ToString();
                    _EntityDetail.TransDate = Convert.ToDateTime(dtRow["TransDate"].ToString()); 
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.Price = Convert.ToDecimal(Convert.IsDBNull(dtRow["Price"]) ? 0 : dtRow["Price"]);
                    _EntityDetail.AmountBeforeDisc = Convert.ToDecimal(Convert.IsDBNull(dtRow["AmountBeforeDisc"]) ? 0 : dtRow["AmountBeforeDisc"]);
                    _EntityDetail.DiscountAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["DiscountAmount"]) ? 0 : dtRow["DiscountAmount"]);
                    _EntityDetail.VTaxCode = dtRow["VTaxCode"].ToString();
                    _EntityDetail.DiscountType = dtRow["DiscountType"].ToString();
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
                    _EntityDetail.Rate = Convert.ToDecimal(Convert.IsDBNull(dtRow["Rate"]) ? 0 : dtRow["Rate"]);
                    _EntityDetail.SDRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["SDRate"]) ? 0 : dtRow["SDRate"]);
                    _EntityDetail.SDComputedAmt = Convert.ToDecimal(Convert.IsDBNull(dtRow["SDComputedAmt"]) ? 0 : dtRow["SDComputedAmt"]);
                    _EntityDetail.AverageCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["AverageCost"]) ? 0 : dtRow["AverageCost"]);
                    _EntityDetail.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                    _EntityDetail.AddSalesInvoiceDetail(_EntityDetail);
                }
            }            
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["detail"] = null;
            }

            if (Session["detail"] != null)
            {
                gv1.DataSource = sdsTransDetail;
            }

            if (Request.QueryString["entry"] == "E")
            {
                GetDiscount();
            }
        }

        private DataTable GetSelectedVal()
        {
            Session["Datatable"] = "0";
            gv1.DataSource = null;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            DataTable dt = new DataTable();
            string[] selectedtransValues = aglTransNo.Text.Split(';');
            CriteriaOperator selectionCriteria2 = new InOperator("DocNumber", selectedtransValues);
            string Transquery = selectionCriteria2.ToString();
            Selection(Transquery);
            gv1.DataSource = sdsTransDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session["Datatable"] = "1";

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

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumber"],dt.Columns["SONumber"],dt.Columns["TransType"],dt.Columns["TransDoc"]};
            //dt.Columns["LineNumber"]};

            //aglTransNo.Text = aglTransNoVal;
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
            //sdsBizPartnerCus.ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsSONumber.ConnectionString = Session["ConnString"].ToString();
            //sdsRefTrans.ConnectionString = Session["ConnString"].ToString();
            //sdsTransDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsDiscount.ConnectionString = Session["ConnString"].ToString();
            //sdsUnit.ConnectionString = Session["ConnString"].ToString();
            //sdsCurrency.ConnectionString = Session["ConnString"].ToString();
            //sdsTaxCode.ConnectionString = Session["ConnString"].ToString();
        }

        protected void Selection(string Trans)
        {
            //string VatCode = "";
            //gv1.DataSourceID = null;
            //gv1.DataBind();

            //if (chkVatable.Checked == true)
            //{
            //    DataTable vcode = new DataTable();
            //    vcode = Gears.RetriveData2("SELECT DISTINCT TaxCode FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + aglCustomerCode.Text + "'");

            //    if (vcode.Rows.Count > 0)
            //    {
            //        VatCode = vcode.Rows[0]["TaxCode"].ToString();
            //    }
            //    else
            //    {
            //        VatCode = "NONV";
            //    }
            //}
            //else
            //{
            //    VatCode = "NONV";
            //}

            if (chkIsWithSO.Checked == true)
            {
                sdsTransDetail.SelectCommand = ";WITH A AS (SELECT B.SONumber AS SONumber, 'SLSDRC' AS TransType, A.DocNumber AS TransDoc, A.DocDate AS TransDate,B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, SUM(B.DeliveredQty) AS Qty, SUM(B.BulkQty) AS BulkQty, MAX(B.Unit) AS Unit, "
                + " MAX(C.UnitPrice) AS Price, SUM(B.DeliveredQty)* MAX(C.UnitPrice) AS AmountBeforeDisc,0.00 AS DiscountAmount, 'NONV' AS VTaxCode,'NoDiscount' AS DiscountType, "
                + " SUM(ISNULL(B.BaseQty,0)) BaseQty,MAX(B.StatusCode) StatusCode,MAX(B.BarcodeNo) BarcodeNo,MAX(ISNULL(B.UnitFactor,0)) UnitFactor,MAX(B.Field1) AS Field1,MAX(B.Field2) AS Field2, "
                + " MAX(B.Field3) AS Field3,MAX(B.Field4) AS Field4,MAX(B.Field5) AS Field5,MAX(B.Field6) AS Field6,MAX(B.Field7) AS Field7,MAX(B.Field8) AS Field8,MAX(B.Field9) AS Field9, 'A' AS Ord, ISNULL(B.AverageCost,0) AS AverageCost, ISNULL(B.UnitCost,0) AS UnitCost FROM Sales.DeliveryReceipt A "
                + " INNER JOIN Sales.DeliveryReceiptDetail B ON A.DocNumber = B.DocNumber INNER JOIN Sales.SalesOrderDetail C ON B.SONumber = C.DocNumber AND B.ItemCode = C.ItemCode AND B.ColorCode = C.ColorCode AND B.ClassCode = C.ClassCode AND B.SizeCode = C.SizeCode "
                + " WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.IsWithDetail,0) != 0 AND (A." + Trans + ") GROUP BY B.SONumber, A.DocNumber, A.DocDate,B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, B.AverageCost, B.UnitCost "
                + " UNION ALL "
                + " SELECT D.DocNumber AS SONumber, 'SLSSRN' AS TransType, A.DocNumber AS TransDoc, A.DocDate AS TransDate,B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, -SUM(B.ReturnedQty) AS Qty, -SUM(B.ReturnedBulkQty) AS BulkQty, MAX(B.Unit) AS Unit,  "
                + " MAX(D.UnitPrice) AS Price, -(SUM(B.ReturnedQty)* MAX(D.UnitPrice)) AS AmountBeforeDisc,0.00 AS DiscountAmount, 'NONV' AS VTaxCode,'NoDiscount' AS DiscountType, "
                + " SUM(ISNULL(B.BaseQty,0)) BaseQty,MAX(B.StatusCode) StatusCode,MAX(B.BarcodeNo) BarcodeNo,MAX(ISNULL(B.UnitFactor,0)) UnitFactor,MAX(B.Field1) AS Field1,MAX(B.Field2) AS Field2, "
                + " MAX(B.Field3) AS Field3,MAX(B.Field4) AS Field4,MAX(B.Field5) AS Field5,MAX(B.Field6) AS Field6,MAX(B.Field7) AS Field7,MAX(B.Field8) AS Field8,MAX(B.Field9) AS Field9, 'B' AS Ord, ISNULL(B.AverageCost,0) AS AverageCost, ISNULL(B.UnitCost,0) AS UnitCost  FROM Sales.SalesReturn A INNER JOIN Sales.SalesReturnDetail B "
                + " ON A.DocNumber = B.DocNumber INNER JOIN Sales.DeliveryReceipt C ON A.ReferenceDRNo = C.DocNumber INNER JOIN Sales.SalesOrderDetail D ON C.RefSONum = D.DocNumber AND B.ItemCode = D.ItemCode AND B.ColorCode = D.ColorCode  "
                + " AND B.ClassCode = D.ClassCode AND B.SizeCode = D.SizeCode WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.IsWithDetail,0) != 0  AND (A." + Trans + ") GROUP BY D.DocNumber, A.DocNumber, A.DocDate,B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, B.AverageCost, B.UnitCost)  "
                + " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY Ord, TransDoc) AS VARCHAR(5)),5) AS LineNumber, SONumber, TransType, TransDoc, TransDate, ItemCode, ColorCode, ClassCode, SizeCode, Qty, BulkQty, Unit, Price, AmountBeforeDisc,  "
                + " DiscountAmount, VTaxCode, DiscountType,BaseQty, StatusCode, BarcodeNo, UnitFactor, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, AverageCost, UnitCost, 0.00 AS Rate, '2' AS Version, 0.00 AS Rate, 0.00 AS SDRate, 0.00 AS SDComputedAmt FROM A";
            }
            else //if hindi nakaWithSO
            {
                sdsTransDetail.SelectCommand = ";WITH A AS (SELECT '-' AS SONumber, 'SLSDRC' AS TransType, A.DocNumber AS TransDoc, A.DocDate AS TransDate, B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, SUM(B.DeliveredQty) AS Qty, SUM(B.BulkQty) AS BulkQty, MAX(B.Unit) AS Unit, "
                + " 0.00 AS Price, 0.00 AS AmountBeforeDisc, 0.00 AS DiscountAmount,'NONV' AS VTaxCode,'NoDiscount' AS DiscountType, "
                + " SUM(ISNULL(B.BaseQty,0)) BaseQty,MAX(B.StatusCode) StatusCode,MAX(B.BarcodeNo) BarcodeNo,MAX(ISNULL(B.UnitFactor,0)) UnitFactor,MAX(B.Field1) AS Field1,MAX(B.Field2) AS Field2, "
                + " MAX(B.Field3) AS Field3,MAX(B.Field4) AS Field4,MAX(B.Field5) AS Field5,MAX(B.Field6) AS Field6,MAX(B.Field7) AS Field7,MAX(B.Field8) AS Field8,MAX(B.Field9) AS Field9, 'A' AS Ord, ISNULL(B.AverageCost,0) AS AverageCost, ISNULL(B.UnitCost,0) AS UnitCost "
                + " FROM Sales.DeliveryReceipt A INNER JOIN Sales.DeliveryReceiptDetail B ON A.DocNumber =  B.DocNumber "
                + " WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.IsWithDetail,0) = 1 AND ISNULL(WithoutRef,0) = 1 AND ISNULL(A.RefSONum,'') = '' "
                + " AND ISNULL(InvoiceDocNum,'') = '' AND (A." + Trans + ") GROUP BY A.DocNumber, A.DocDate,B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, B.AverageCost, B.UnitCost "
                + " UNION ALL"
                + " SELECT '-' AS SONumber, 'SLSSRN' AS TransType, A.DocNumber AS TransDoc, A.DocDate AS TransDate,B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, -SUM(B.ReturnedQty) AS Qty, -SUM(B.ReturnedBulkQty) AS BulkQty, MAX(B.Unit) AS Unit, "
                + " 0.00 AS Price, 0.00 AS AmountBeforeDisc, 0.00 AS DiscountAmount, 'NONV' AS VTaxCode, 'NoDiscount' AS DiscountType, "
                + " SUM(ISNULL(B.BaseQty,0)) BaseQty,MAX(B.StatusCode) StatusCode,MAX(B.BarcodeNo) BarcodeNo,MAX(ISNULL(B.UnitFactor,0)) UnitFactor,MAX(B.Field1) AS Field1,MAX(B.Field2) AS Field2,"
                + " MAX(B.Field3) AS Field3,MAX(B.Field4) AS Field4,MAX(B.Field5) AS Field5,MAX(B.Field6) AS Field6,MAX(B.Field7) AS Field7,MAX(B.Field8) AS Field8,MAX(B.Field9) AS Field9, 'B' AS Ord, ISNULL(B.AverageCost,0) AS AverageCost, ISNULL(B.UnitCost,0) AS UnitCost FROM Sales.SalesReturn A "
                + " INNER JOIN Sales.SalesReturnDetail B ON A.DocNumber = B.DocNumber "
                + " WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.IsWithDetail,0) = 1 AND ISNULL(A.ReferenceDRNo,'') = '' AND ISNULL(IsWithDR,0) = 0 "
                + " AND ISNULL(InvoiceDocNum,'') = '' AND (A." + Trans + ") GROUP BY A.DocNumber, A.DocDate,B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, B.AverageCost, B.UnitCost "
                + " UNION ALL "
                + " SELECT '-' AS SONumber, 'ACTADI' AS TransType, A.DocNumber AS TransDoc, A.DocDate AS TransDate, C.ItemCode, C.ColorCode, C.ClassCode, C.SizeCode, SUM(B.Qty) AS Qty, 0 AS BulkQty, MAX(D.UnitBase) AS Unit, "
                + " MAX(B.UnitPrice) AS Price, (SUM(B.Qty)* MAX(B.UnitPrice)) AS AmountBeforeDisc, 0.00 AS DiscountAmount,'NONV' AS VTaxCode,'NoDiscount' AS DiscountType, "
                + " SUM(ISNULL(B.BaseQty,0)) BaseQty,MAX(B.StatusCode) StatusCode,MAX(B.BarcodeNo) BarcodeNo,MAX(ISNULL(B.UnitFactor,0)) UnitFactor,MAX(B.Field1) AS Field1,MAX(B.Field2) AS Field2, "
                + " MAX(B.Field3) AS Field3,MAX(B.Field4) AS Field4,MAX(B.Field5) AS Field5,MAX(B.Field6) AS Field6,MAX(B.Field7) AS Field7,MAX(B.Field8) AS Field8,MAX(B.Field9) AS Field9, 'C' AS Ord, ISNULL(B.AverageCost,0) AS AverageCost, ISNULL(B.UnitCost,0) AS UnitCost FROM Accounting.AssetDisposal A "
                + " INNER JOIN Accounting.AssetDisposalDetail B ON A.DocNumber = B.DocNumber INNER JOIN Accounting.AssetInv C ON B.PropertyNumber = C.PropertyNumber LEFT JOIN Masterfile.Item D ON C.ItemCode = D.ItemCode "
                + " WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.IsWithDetail,0) = 1 AND ISNULL(A.InvoiceDocNum,'') = '' AND (A." + Trans + ") GROUP BY A.DocNumber, A.DocDate,C.ItemCode, C.ColorCode, C.ClassCode, C.SizeCode, B.AverageCost, B.UnitCost) "
                + " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY Ord, TransDoc) AS VARCHAR(5)),5) AS LineNumber, SONumber, TransType, TransDoc, TransDate, ItemCode, ColorCode, ClassCode, SizeCode, Qty, BulkQty, Unit, Price, AmountBeforeDisc, "
                + " DiscountAmount, VTaxCode, DiscountType,BaseQty, StatusCode, BarcodeNo, UnitFactor, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, AverageCost, UnitCost, '2' AS Version, 0.00 AS Rate, 0.00 AS SDRate, 0.00 AS SDComputedAmt FROM A ";
            }
        }

        protected void Initialize()
        {
            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (chkIsWithSO.Checked == false)
                {
                    Session["aglTransNo_Init"] = "SELECT DISTINCT A.DocNumber, A.DocDate, A.CustomerCode, A.Remarks FROM Sales.DeliveryReceipt A INNER JOIN Sales.DeliveryReceiptDetail B ON A.DocNumber = B.DocNumber "
                                             + " WHERE ISNULL(SubmittedBy,'') != '' AND ISNULL(A.CancelledBy,'') = '' AND ISNULL(InvoiceDocNum,'') = '' "
                                             + " AND (ISNULL(RefSONum,'') = '' OR ISNULL(WithoutRef,0) = 1) AND ISNULL(IsWithDetail,0) = 1 AND CustomerCode = '" + aglCustomerCode.Text + "'"
                                             + " UNION ALL SELECT DISTINCT A.DocNumber, A.DocDate, A.CustomerCode, A.Remarks FROM Sales.SalesReturn A INNER JOIN Sales.SalesReturnDetail B ON A.DocNumber = B.DocNumber "
                                             + " WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.CancelledBy,'') = '' AND (ISNULL(ReferenceDRNo,'') = '' OR ISNULL(IsWithDR,0) = 0)"
                                             + " AND ISNULL(A.InvoiceDocNum,'') = '' AND A.CustomerCode = '" + aglCustomerCode.Text + "' UNION ALL "
                                             + " SELECT DISTINCT DocNumber, DocDate, SoldTo AS CustomerCode, Remarks FROM Accounting.AssetDisposal WHERE ISNULL(SubmittedBy,'') != '' AND ISNULL(CancelledBy,'') = '' AND ISNULL(InvoiceDocNum,'') = '' AND SoldTo = '" + aglCustomerCode.Text + "'";

                    sdsRefTrans.SelectCommand = Session["aglTransNo_Init"].ToString();
                    aglTransNo.DataBind();
                }
                else
                {
                    Session["aglTransNo_Init"] = "SELECT DISTINCT A.DocNumber, A.DocDate, A.CustomerCode, A.Remarks FROM Sales.DeliveryReceipt A INNER JOIN Sales.DeliveryReceiptDetail B ON A.DocNumber = B.DocNumber "
                                             + " WHERE ISNULL(SubmittedBy,'') != '' AND ISNULL(A.CancelledBy,'') = '' AND ISNULL(InvoiceDocNum,'') = '' "
                                             + " AND ISNULL(WithoutRef,0) = 0 AND CustomerCode = '" + aglCustomerCode.Text + "' UNION ALL "
                                             + " SELECT DISTINCT A.DocNumber, A.DocDate, A.CustomerCode, A.Remarks FROM Sales.SalesReturn A INNER JOIN Sales.SalesReturnDetail B "
                                             + " ON A.DocNumber = B.DocNumber INNER JOIN Sales.DeliveryReceipt C ON C.DocNumber = A.ReferenceDRNo "
                                             + " WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.CancelledBy,'') = '' AND (ISNULL(ReferenceDRNo,'') != '' OR ISNULL(IsWithDR,0) != 0) "
                                             + " AND ISNULL(A.InvoiceDocNum,'') = '' AND A.CustomerCode = '" + aglCustomerCode.Text + "' "
                                             + " AND (ISNULL(RefSONum,'') != '' OR ISNULL(WithoutRef,0) = 0)";

                    sdsRefTrans.SelectCommand = Session["aglTransNo_Init"].ToString();
                    aglTransNo.DataBind();
                }
            }
            else
            {
                sdsRefTrans.SelectCommand = "SELECT DISTINCT DocNumber, DocDate, CustomerCode, Remarks FROM Sales.DeliveryReceipt WHERE CustomerCode = '" + aglCustomerCode.Text + "'"
                                             + " UNION ALL SELECT DISTINCT DocNumber, DocDate, CustomerCode, Remarks FROM Sales.SalesReturn WHERE CustomerCode = '" + aglCustomerCode.Text + "' UNION ALL "
                                             + " SELECT DocNumber, DocDate, SoldTo AS CustomerCode, Remarks FROM Accounting.AssetDisposal WHERE SoldTo = '" + aglCustomerCode.Text + "'";
                aglTransNo.DataBind();
            }
            
        }

        protected void Check()
        {

            Initialize();

            if (chkIsWithSO.Checked == true)
            {
                gv1.Columns["SONumber"].Width = 80;

                if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                {
                    aglTransNo.ClientEnabled = true;
                }
                else
                {
                    aglTransNo.ClientEnabled = false;
                }
            }
            else
            {
                gv1.Columns["SONumber"].Width = 0;

                if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                {
                    aglTransNo.ClientEnabled = true;
                }
                else
                {
                    aglTransNo.ClientEnabled = false;
                }
        }

        }

        protected void GetRate()
        {
            decimal Rate = 0;
            decimal Answer = 0;

            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                DataTable code = new DataTable();

                code = Gears.RetriveData2("SELECT DISTINCT TaxCode, Rate FROM Masterfile.BPCustomerInfo A INNER JOIN Masterfile.Tax B ON A.TaxCode = B.TCode WHERE A.BizPartnerCode ='" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());

                if (code.Rows.Count != 0)
                {
                    Rate = Convert.ToDecimal(code.Rows[0]["Rate"].ToString());
                }

                if (Rate != 0)
                {
                    Answer = Convert.ToDecimal(txtVATAfter.Text) * Rate;

                    txtVATAmount.Text = Answer.ToString("0.00");
                }
                else
                {
                    txtVATAmount.Text = "0.00";
                }
            }
        }

        public void GetDiscount()
        {
            DataTable Diplomat = new DataTable();
            DataTable PWD = new DataTable();
            DataTable Senior = new DataTable();

            Diplomat = Gears.RetriveData2("SELECT ISNULL(Value,'0') AS Diplomat FROM IT.SystemSettings WHERE Code = 'DISC_DIP' AND SequenceNumber = 6 ", Session["ConnString"].ToString());
            PWD = Gears.RetriveData2("SELECT ISNULL(Value,'0')  AS PWD FROM IT.SystemSettings WHERE Code = 'DISC_PWD' AND SequenceNumber = 6 ", Session["ConnString"].ToString());
            Senior = Gears.RetriveData2("SELECT ISNULL(Value,'0') AS Senior FROM IT.SystemSettings WHERE Code = 'DISC_SC' AND SequenceNumber = 6", Session["ConnString"].ToString());

            if (Diplomat.Rows.Count > 0)
            {
                cp.JSProperties["cp_DIP"] = Diplomat.Rows[0]["Diplomat"].ToString();
            }
            else
            {
                cp.JSProperties["cp_DIP"] = "0";
            }

            if (PWD.Rows.Count > 0)
            {
                cp.JSProperties["cp_PWD"] = PWD.Rows[0]["PWD"].ToString();
            }
            else
            {
                cp.JSProperties["cp_PWD"] = "0";
            }

            if (Senior.Rows.Count > 0)
            {
                cp.JSProperties["cp_SC"] = Senior.Rows[0]["Senior"].ToString();
            }
            else
            {
                cp.JSProperties["cp_SC"] = "0";
            }
        }

        protected void aglTransNo_Init(object sender, EventArgs e)
        {
            if (Session["aglTransNo_Init"] != null)
            {
                sdsRefTrans.ConnectionString = Session["ConnString"].ToString();
                sdsRefTrans.SelectCommand = Session["aglTransNo_Init"].ToString();
                aglTransNo.DataBind();
            }
        }
        public void CheckSubmit()
        {
            string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.SalesInvoice", 1, Session["ConnString"].ToString());
            if (!string.IsNullOrEmpty(strError))
            {
                cp.JSProperties["cp_message"] = strError;
                cp.JSProperties["cp_success"] = true;
                cp.JSProperties["cp_forceclose"] = true;
                return;
            }
        }
        
    }
}