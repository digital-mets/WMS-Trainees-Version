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
    public partial class frmARMemo : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        

        Entity.ARMemo _Entity = new ARMemo();//Calls entity odsHeader
        Entity.ARMemo.ARMemoDetail _EntityDetail = new ARMemo.ARMemoDetail();//Call entity sdsDetail

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
            //gv1.DataSource = null;

            if (!IsPostBack)
            {
                Session["ARDatatable"] = null;
                Session["ARtry"] = null;
                Session["ARFilterExpression"] = null;
                Session["TransNoARMemo"] = null;
                Session["ARQuery"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    //gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv1.KeyFieldName = "LineNumber";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                //else
                //{
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                aglWarehouseCode.Value = _Entity.WarehouseCode.ToString();
                aglType.Value = _Entity.Type.ToString();
                //aglSINumber.Text = _Entity.RefInvoice.ToString();
                Session["TransNoARMemo"] = _Entity.RefInvoice.ToString();
                aglTaxCode.Text = _Entity.TaxCode.ToString();
                speRate.Text = _Entity.TaxRate.ToString();
                txtName.Value = _Entity.Name.ToString();
                txtAddress.Value = _Entity.Address.ToString();
                aglCurrency.Value = _Entity.Currency.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                speGross.Value = _Entity.GrossVatAmount.ToString();
                speGrossNon.Value = _Entity.GrossNonVatAmount.ToString();
                txtVAT.Value = _Entity.VATAmount.ToString();
                txtAmount.Value = _Entity.TotalAmount.ToString();

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
                //GrossEffect();
                //}

                gv1.KeyFieldName = "LineNumber;InvoiceNo";

                //string val = "";
                //DataTable rate = new DataTable();
                //rate = Gears.RetriveData2("SELECT DISTINCT Value FROM IT.SystemSettings WHERE Code = 'TAXRATE'");
                //if (rate.Rows.Count > 0)
                //{
                //    val = rate.Rows[0]["Value"].ToString();
                //}
                //else
                //{
                //    val = "0.12";
                //}
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
                        //aglTaxRate.Text = val;
                        if (String.IsNullOrEmpty(aglCurrency.Text))
                        {
                            aglCurrency.Text = "PHP";
                        }

                        aglSINumber.Text = Session["TransNoARMemo"].ToString();

                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //if (String.IsNullOrEmpty(aglTaxRate.Text))
                        //{
                        //    aglTaxRate.Text = val;
                        //}
                        if (String.IsNullOrEmpty(aglCurrency.Text))
                        {
                            aglCurrency.Text = "PHP";
                        }
                        //Initialize();
                        ////TypeEffect();
                        //GrossEffect();

                        aglSINumber.Text = Session["TransNoARMemo"].ToString();

                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        if (String.IsNullOrEmpty(aglCurrency.Text))
                        {
                            aglCurrency.Text = "PHP";
                        }

                        aglSINumber.Text = Session["TransNoARMemo"].ToString();

                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        if (String.IsNullOrEmpty(aglCurrency.Text))
                        {
                            aglCurrency.Text = "PHP";
                        }

                        aglSINumber.Text = Session["TransNoARMemo"].ToString();

                        break;
                }


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    //gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    //gv1.DataSourceID = "odsDetail";
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }

                Initialize();
                //TypeEffect();
                ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = null;
                //}
                //else
                //{
                //    gv1.DataSourceID = "odsDetail";
                //}
                
            }

            DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.ARMemoDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

            gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

            gvJournal.DataSourceID = "odsJournalEntry";


        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTARM";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ARMemo_Validate(gparam);
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
            gparam._TransType = "ACTARM";
            gparam._Table = "Accounting.ARMemo";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ARMemo_Post(gparam);
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
            if (Session["ARFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsItemDetail.FilterExpression = Session["ARFilterExpression"].ToString();
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
            if (e.Parameters.Contains("ItemCode"))
            {
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,UnitBase,FullDesc, UnitBulk from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                }

                DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode='V' THEN 'True' ELSE 'False' END AS IsVat from " + 
                                                " masterfile.BPCustomerInfo where BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in tax.Rows)
                {
                    codes += dt["TaxCode"].ToString() + ";";
                    codes += dt["IsVat"].ToString() + ";";
                }

                itemlookup.JSProperties["cp_codes"] = codes;

            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["ARFilterExpression"] = sdsItemDetail.FilterExpression;
                grid.DataSourceID = "sdsItemDetail";
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
                _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouseCode.Text) ? null : aglWarehouseCode.Value.ToString();
                _Entity.Name = String.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
                _Entity.Address = String.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Text;
                _Entity.RefInvoice = String.IsNullOrEmpty(aglSINumber.Text) ? null : aglSINumber.Text;
                _Entity.Type = String.IsNullOrEmpty(aglType.Text) ? null : aglType.Value.ToString();
                _Entity.Currency = String.IsNullOrEmpty(aglCurrency.Text) ? "PHP" : aglCurrency.Value.ToString();
                _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
                _Entity.TaxCode = String.IsNullOrEmpty(aglTaxCode.Text) ? "PHP" : aglTaxCode.Value.ToString(); ;
                _Entity.TaxRate = String.IsNullOrEmpty(speRate.Text) ? 0 : Convert.ToDecimal(speRate.Text);
                _Entity.GrossVatAmount = String.IsNullOrEmpty(speGross.Text) ? 0 : Convert.ToDecimal(speGross.Value.ToString());
                _Entity.GrossNonVatAmount = String.IsNullOrEmpty(speGrossNon.Text) ? 0 : Convert.ToDecimal(speGrossNon.Value.ToString());
                _Entity.VATAmount = String.IsNullOrEmpty(txtVAT.Text) ? 0 : Convert.ToDecimal(txtVAT.Value.ToString());
                _Entity.TotalAmount = String.IsNullOrEmpty(txtAmount.Text) ? 0 : Convert.ToDecimal(txtAmount.Value.ToString());
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
                    
                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.ARMemo", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        if (Session["ARDatatable"] == "1")
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
                        Post();
                        Validate();
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        cp.JSProperties["cp_message"] = "Successfully Added!";
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
                    
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Accounting.ARMemo", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        if (Session["ARDatatable"] == "1")
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
                        Post();
                        Validate();
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
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
                    
                    string strError2 = Functions.Submitted(_Entity.DocNumber, "Accounting.ARMemo", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.ARMemoDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        Post();
                        Validate();
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        cp.JSProperties["cp_message"] = "Successfully Added!";
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
                    
                    string strError3 = Functions.Submitted(_Entity.DocNumber, "Accounting.ARMemo", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.ARMemoDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        Post();
                        Validate();
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
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

                case "CallbackCustomer":

                    if (Request.QueryString["entry"] == "E")
                    {
                        aglSINumber.Text = "";
                    }

                    Session["ARDatatable"] = "0";
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();

                    if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                    {
                        //DataTable info = Gears.RetriveData2("SELECT DISTINCT A.Name, Address, TaxCode  FROM Masterfile.BizPartner A INNER JOIN "
                        //                            + " Masterfile.BPCustomerInfo  B ON A.BizPartnerCode = B.BizPartnerCode WHERE A.BizPartnerCode = '" 
                        //                            + aglCustomerCode.Text + "'");
                        DataTable info = Gears.RetriveData2("SELECT DISTINCT A.Name, Address, C.Rate, C.TCode AS TaxCode  FROM Masterfile.BizPartner A INNER JOIN "
                                                    + " Masterfile.BPCustomerInfo B ON A.BizPartnerCode = B.BizPartnerCode "
                                                    + " INNER JOIN Masterfile.Tax C ON B.TaxCode = C.TCode WHERE A.BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());

                        if (info.Rows.Count != 0)
                        {
                            txtName.Text = info.Rows[0]["Name"].ToString();
                            txtAddress.Text = info.Rows[0]["Address"].ToString();
                            aglTaxCode.Text = info.Rows[0]["TaxCode"].ToString();
                            speRate.Text = info.Rows[0]["Rate"].ToString();
                        }
                    }

                    //TypeEffect();
                    Initialize();
                    //GrossEffect();
                    cp.JSProperties["cp_generated"] = true;
                    cp.JSProperties["cp_typeeffect"] = true;
                    ResetFields();

                    break;

                case "Details":
                    
                    //Session["ARDatatable"] = "0";
                    //gv1.DataSource = sdsDetail;
                    //if (gv1.DataSourceID != "")
                    //{
                    //    gv1.DataSourceID = null;
                    //}
                    //gv1.DataBind();
                    Session["TransNoARMemo"] = aglSINumber.Text;
                    GetSelectedVal();
                    Initialize();
                    //GrossEffect();
                    cp.JSProperties["cp_generated"] = true;
                    cp.JSProperties["cp_typeeffect"] = true;
                    aglSINumber.Text = Session["TransNoARMemo"].ToString();

                    break;

                case "CallbackType":

                    Session["ARDatatable"] = "0";
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    //TypeEffect();
                    Initialize();
                    //GrossEffect();
                    aglSINumber.Text = "";
                    cp.JSProperties["cp_generated"] = true;
                    cp.JSProperties["cp_typeeffect"] = true;
                    ResetFields();

                    break;

                case "CallbackTaxCode":

                    //TypeEffect();
                    cp.JSProperties["cp_generated"] = true;
                    cp.JSProperties["cp_typeeffect"] = true;

                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";
            if (error == false && check == false)
            {
                foreach (GridViewColumn column in gv1.Columns)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber"
                        && dataColumn.FieldName != "DeliveredQty" && dataColumn.FieldName != "DiscountRate" && dataColumn.FieldName != "SubstituteItem"
                        && dataColumn.FieldName != "StatusCode" && dataColumn.FieldName != "FullDesc" && dataColumn.FieldName != "SubstituteColor"
                        && dataColumn.FieldName != "VATCode" && dataColumn.FieldName != "IsVAT" && dataColumn.FieldName != "BaseQty"
                        && dataColumn.FieldName != "BarcodeNo" && dataColumn.FieldName != "UnitFreight" && dataColumn.FieldName != "BulkUnit" && dataColumn.FieldName != "BulkQty"
                        && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3"
                        && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6"
                        && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9")
                    {
                        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message                        
                    }
                }

                //Checking for non existing Codes..
                ItemCode = e.NewValues["ItemCode"].ToString();
                ColorCode = e.NewValues["ColorCode"].ToString();
                ClassCode = e.NewValues["ClassCode"].ToString();
                SizeCode = e.NewValues["SizeCode"].ToString();
                DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());
                if (item.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
                }
                DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "' AND ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());
                if (color.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
                }
                DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "' AND ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());
                if (clss.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
                }
                DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "' AND ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());
                if (size.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
                }

                if (e.Errors.Count > 0)
                {
                    error = true; //bool to cancel adding/updating if true
                }
            }
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

            if (Session["ARDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"], values.Keys["InvoiceNo"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"], values.NewValues["InvoiceNo"] };
                    DataRow row = source.Rows.Find(keys);
                    row["InvoiceNo"] = values.NewValues["InvoiceNo"];
                    row["TransDoc"] = values.NewValues["TransDoc"];
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["Qty"] = values.NewValues["Qty"];
                    row["Price"] = values.NewValues["Price"];
                    row["BulkQty"] = values.NewValues["BulkQty"];
                    row["Vatable"] = values.NewValues["Vatable"];
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

                    _EntityDetail.InvoiceNo = dtRow["InvoiceNo"].ToString();
                    _EntityDetail.TransDoc = dtRow["TransDoc"].ToString();
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                    _EntityDetail.Price = Convert.ToDecimal(Convert.IsDBNull(dtRow["Price"]) ? 0 : dtRow["Price"]);
                    _EntityDetail.BulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BulkQty"]) ? 0 : dtRow["BulkQty"]);
                    _EntityDetail.Vatable = Convert.ToBoolean(Convert.IsDBNull(dtRow["Vatable"].ToString()) ? false : dtRow["Vatable"]);
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
                    _EntityDetail.AddARMemoDetail(_EntityDetail);
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
                //sdsTransDetail.SelectCommand = Session["ARtry"].ToString();
                gv1.DataSource = sdsTransDetail;
                //if (gv1.DataSourceID != "")
                //{
                //    gv1.DataSourceID = null;
                //}
                //gv1.DataBind();
            }
        }

        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            Filter();           
            gv1.DataSource = sdsTransDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session["ARDatatable"] = "1";
            //Session["detail"] = "1";
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
            dt.Columns["LineNumber"], dt.Columns["InvoiceNo"]};

            //Check();
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
            //sdsSINumber.ConnectionString = Session["ConnString"].ToString();
            //sdsTransDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsType.ConnectionString = Session["ConnString"].ToString();
            //sdsTaxCode.ConnectionString = Session["ConnString"].ToString();
            //sdsCurrency.ConnectionString = Session["ConnString"].ToString();
            //sdsWarehouse.ConnectionString = Session["ConnString"].ToString();
        }


        protected void Initialize()
        {
            if (!String.IsNullOrEmpty(aglCustomerCode.Text))
            {
                if (aglType.Text.Trim() == "Sales Return")
                {
                    aglSINumber.ClientEnabled = true;
                }
                else
                {
                    aglSINumber.ClientEnabled = false;
                }
                Session["ARQuery"] = " SELECT B.LineNumber, A.DocNumber AS InvoiceNo, CONVERT(varchar(20), A.DocDate,101) AS InvoiceDate, B.TransDoc, B.TransDate, B.ItemCode, B.ColorCode, B.ClassCode, B.SizeCode, B.Unit, Qty, Price, B.BulkQty, Remarks "
                                          + " from Accounting.SalesInvoice A INNER JOIN Accounting.SalesInvoiceDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND A.CustomerCode = '" + aglCustomerCode.Text + "'"
                                          + " AND DocDate <= '" + dtpDocDate.Text + "'";
                sdsSINumber.SelectCommand = Session["ARQuery"].ToString();
                sdsSINumber.DataBind();                
            }
            else
            {
                aglSINumber.ClientEnabled = false;
            }         

        }

        protected void Filter()
        {
            string Value = "";
            int cnt = 0;
            string and = "";
            int count = Session["TransNoARMemo"].ToString().Split(';').Length;
            var pieces = Session["TransNoARMemo"].ToString().Split(new[] { ';' }, count);

            foreach (string c in pieces)
            {
                var a = c.Split(new[] { '-' }, 2);

                if (cnt != 0)
                {
                    and = " OR ";
                }
                Value += and + "(B.LineNumber = '" + a[1].ToString() + "' AND A.DocNumber ='" + a[0].ToString() + "')";

                cnt = cnt + 1;
            }
            //Session["TransNoARMemo"]
            
            //List<object> fieldValues = aglSINumber.GridView.GetSelectedFieldValues(new string[] { "LineNumber", "InvoiceNo" });
            //foreach (object[] item in fieldValues)
            //{
            //    if (cnt != 0)
            //    {
            //        and = " OR ";
            //    }
            //    Value += and + "(B.LineNumber = '"+ item[0].ToString() + "' AND A.DocNumber ='" + item[1].ToString() + "')";

            //    cnt = cnt + 1;
            //}

            string test = Value;

            sdsTransDetail.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, "
                            + " A.DocNumber AS InvoiceNo, B.TransDoc, B.ItemCode, C.FullDesc ,B.ColorCode, B.ClassCode, B.SizeCode, B.Unit, Qty, Price, ISNULL(BulkQty,0) AS BulkQty, "
                            + " ISNULL(Vatable,0) AS Vatable, B.BaseQty, B.StatusCode, B.BarcodeNo, B.UnitFactor, B.Field1, B.Field2, B.Field3, B.Field4, B.Field5, B.Field6, "
                            + " B.Field7, B.Field8, B.Field9, '2' AS Version, B.TransType AS RefTransType from Accounting.SalesInvoice A INNER JOIN Accounting.SalesInvoiceDetail B ON A.DocNumber = B.DocNumber "
                            + " INNER JOIN Masterfile.Item C ON B.ItemCode = C.ItemCode WHERE ISNULL(A.SubmittedBy,'') != '' AND " + Value + "";

            //sdsTransDetail.SelectCommand = Session["ARtry"].ToString();
        }

        protected void TypeEffect()
        {
            if (!String.IsNullOrEmpty(aglType.Text))
            {
                if (aglType.Text.Trim() == "Sales Return")
                {
                    speGross.ReadOnly = true;
                    speGrossNon.ReadOnly = true;
                    aglWarehouseCode.ClientEnabled = true;

                }

                if (aglType.Text.Trim() != "Sales Return")
                {

                    aglSINumber.Text = "";

                    if (aglTaxCode.Text.Trim() == "NONV")
                    {
                        speGross.Text = "0.00";
                        speGross.ReadOnly = true;
                        speGrossNon.ReadOnly = false;
                    }
                    else
                    {
                        speGross.ReadOnly = false;
                        speGrossNon.ReadOnly = false;
                    }

                    //speGross.ReadOnly = false;
                    //speGrossNon.ReadOnly = false;
                    aglWarehouseCode.Text = "";
                    aglWarehouseCode.ClientEnabled = false;
                }
            }            
        }

        protected void ResetFields()
        {
            speGross.Text = "0.00";
            speGrossNon.Text = "0.00";
            txtVAT.Text = "0.00";
            txtAmount.Text = "0.00";

        }

        protected void aglWarehouseCode_Load(object sender, EventArgs e)
        {
            if (aglType.Text.Trim() == "Sales Return")
            {
                aglWarehouseCode.ClientEnabled = true;
            }
            else
            {
                aglWarehouseCode.Text = "";
                aglWarehouseCode.ClientEnabled = false;
            }

        }

        protected void aglSINumber_Init(object sender, EventArgs e)
        {
            if (Session["ARQuery"] != null)
            {
                sdsSINumber.SelectCommand = Session["ARQuery"].ToString();
                sdsSINumber.DataBind();
            }
        }

        //protected void GrossEffect()
        //{
        //    if (aglType.Text.Trim() != "Sales Return")
        //    {
                
        //    }
        //}
    }
}