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
using GearsProcurement;

namespace GWL
{
    public partial class frmPurchaseReturn : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.PurchaseReturn _Entity = new PurchaseReturn();//Calls entity PurchaseReturn
        Entity.PurchaseReturn.PurchaseReturnDetail _EntityDetail = new PurchaseReturn.PurchaseReturnDetail();//Call entity POdetails

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

            gv1.KeyFieldName = "DocNumber;LineNumber";
            txtDoc.ReadOnly = true;
            txtDoc.Value = Request.QueryString["docnumber"].ToString();
            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Connection = (Session["ConnString"].ToString());
                Session["PRTFilterExpression"] = null;
                Session["PRTDatatable"] = null;


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                // gv1.KeyFieldName = "DocNumber;LineNumber";
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session


                // if (Request.QueryString["entry"].ToString() == "N")
                // {

                //   popup.ShowOnPageLoad = false;
                // }

                //gv1.DataSourceID = "odsDetail";
                //_Entity.getdata(txtDoc.Text);
                _Entity.getdata(txtDoc.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                gvRRDoc.Text = _Entity.RRNumber;
                txtPLDoc.Text = _Entity.PLDocNumber;
                txtAPMemo.Text = _Entity.APMemo;
                txtSupplier.Value = _Entity.SupplierCode;
                gvWarehouse.Value = _Entity.WarehouseCode;
                txtRemarks.Value = _Entity.Remarks.ToString();
                gvReason.Value = _Entity.Reason;
                txtDRDoc.Value = _Entity.DRDocNumber;
                chkReturn.Value = _Entity.ReturnOnOrder;
                txtCurrency.Value = _Entity.Currency;
                txtPeso.Value = _Entity.PesoAmount;
                txtForeign.Value = _Entity.ForeignAmount;
                txtWithTax.Value = _Entity.WithholdingTax;
                txtExchange.Value = _Entity.ExchangeRate;
                txtVat.Value = _Entity.VatAmount;
                txtNonVat.Value = _Entity.NonVatAmount;
                txtTotalQty.Value = _Entity.TotalQty;
                txtTotalBulkQty.Value = _Entity.TotalBulkQty;
                txtTotalAmt.Value = _Entity.GrossVatAmount;
                txtTerms.Value = _Entity.Terms;
                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtHPostedBy.Text = _Entity.PostedBy;
                txtHPostedDate.Text = _Entity.PostedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;
                //txtHAddedBy.Text = _Entity.AddedBy;
                //txtHAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? "" : Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                //txtHLastEditedBy.Text = _Entity.LastEditedBy;
                //txtHLastEditedDate.Text = String.IsNullOrEmpty(_Entity.LastEditedDate) ? "" : Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                //txtHSubmittedBy.Text = _Entity.SubmittedBy;
                //txtHSubmittedDate.Text = String.IsNullOrEmpty(_Entity.SubmittedDate) ? "" : Convert.ToDateTime(_Entity.SubmittedDate).ToShortDateString();
                //txtHPostedBy.Text = _Entity.PostedBy;
                //txtHPostedDate.Text = String.IsNullOrEmpty(_Entity.PostedDate) ? "" : Convert.ToDateTime(_Entity.PostedDate).ToShortDateString();

                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = null;
                //}
                //else if (Request.QueryString["IsWithDetail"].ToString() == "True" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "odsDetail";
                //}
                //if (Request.QueryString["entry"].ToString() == "V")
                //{
                //    gv1.DataSourceID = "odsDetail";

                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

                updateBtn.Text = "Add";
                if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Update";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                }
                switch (Request.QueryString["entry"].ToString())

                   // Session["RRses"] = null;
                //Session["PRTDatatable"] = null;
                //Session["PRTFilterExpression"] = null;

                   // switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        //glcheck.ClientVisible = false;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //gvRRDoc.ClientEnabled = false;
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                DataTable checkCount = Gears.RetriveData2("Select DocNumber from procurement.purchasereturndetail where docnumber = '" + txtDoc.Text + "'", Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "sdsDetail";
                }
                if (Request.QueryString["entry"].ToString() != "N")
                {
                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;

                   
                }
                gvJournal.DataSourceID = "odsJournalEntry";
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
            string strresult = GProcurement.PurchaseReturn_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRPRT";
            gparam._Table = "Procurement.PurchaseReturn";
            gparam._Connection = Session["ConnString"].ToString();
            gparam._Factor = -1;
            string strresult = GearsProcurement.GProcurement.PurchaseReturn_Post(gparam);
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
        protected void memoremarks_Load(object sender, EventArgs e)
        {
            ((ASPxMemo)sender).ReadOnly = view;

        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            //ASPxGridLookup look = sender as ASPxGridLookup;
            //look.DropDownButton.Enabled = !view;
            //look.ReadOnly = view;

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
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (!IsPostBack)
            {
                ASPxCheckBox text = sender as ASPxCheckBox;
                text.ReadOnly = view;
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

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["PRTFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["PRTFilterExpression"].ToString();
                //Session["PRTFilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            var selectedValues = itemcode;
            CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
            Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["PRTFilterExpression"] = Masterfileitemdetail.FilterExpression;
            grid.DataSourceID = "Masterfileitemdetail";
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

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDoc.Text;
            _Entity.DocDate = String.IsNullOrEmpty(dtpDate.Text) ? null : Convert.ToDateTime(dtpDate.Text).ToShortDateString();
            _Entity.RRNumber = String.IsNullOrEmpty(gvRRDoc.Text) ? null : gvRRDoc.Value.ToString();
            _Entity.PLDocNumber = txtPLDoc.Text;
            _Entity.SupplierCode = String.IsNullOrEmpty(txtSupplier.Text) ? null : txtSupplier.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(gvWarehouse.Text) ? null : gvWarehouse.Value.ToString();
            _Entity.Remarks = txtRemarks.Text;
            _Entity.Reason = String.IsNullOrEmpty(gvReason.Text) ? null : gvReason.Value.ToString();
            _Entity.DRDocNumber = txtDRDoc.Text;
            _Entity.ReturnOnOrder = Convert.ToBoolean(chkReturn.Value.ToString());
            _Entity.Currency = txtCurrency.Text;
            _Entity.PesoAmount = String.IsNullOrEmpty(txtPeso.Text) ? 0 : Convert.ToDecimal(txtPeso.Text);
            _Entity.ForeignAmount = String.IsNullOrEmpty(txtForeign.Text) ? 0 : Convert.ToDecimal(txtForeign.Text);
            _Entity.TotalQty = String.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToDecimal(txtTotalQty.Text);
            _Entity.TotalBulkQty = String.IsNullOrEmpty(txtTotalBulkQty.Text) ? 0 : Convert.ToDecimal(txtTotalBulkQty.Text);
            _Entity.GrossVatAmount = String.IsNullOrEmpty(txtTotalAmt.Text) ? 0 : Convert.ToDecimal(txtTotalAmt.Text);
            _Entity.VatAmount = String.IsNullOrEmpty(txtVat.Text) ? 0 : Convert.ToDecimal(txtVat.Text);
            _Entity.NonVatAmount = String.IsNullOrEmpty(txtNonVat.Text) ? 0 : Convert.ToDecimal(txtNonVat.Text);
            _Entity.ExchangeRate = String.IsNullOrEmpty(txtExchange.Text) ? 0 : Convert.ToDecimal(txtExchange.Text);
            _Entity.WithholdingTax = String.IsNullOrEmpty(txtWithTax.Text) ? 0 : Convert.ToDecimal(txtWithTax.Text);
            _Entity.Terms = txtTerms.Text;
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            _Entity.AddedBy = txtHAddedBy.Text;
            _Entity.AddedDate = txtHAddedDate.Text;
            _Entity.SubmittedBy = txtHSubmittedBy.Text;
            _Entity.SubmittedDate = txtHSubmittedDate.Text;
            _Entity.PostedBy = txtHPostedBy.Text;
            _Entity.PostedDate = txtHPostedDate.Text;
            _Entity.CancelledBy = txtHCancelledBy.Text;
            _Entity.CancelledDate = txtHCancelledDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
           

            switch (e.Parameter)
            {
                case "Add":
                  //string strError = Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseReturn",1,Connection);//NEWADD factor 1 if submit, 2 if approve

                  // gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                  //  if (error == false)
                  //  {
                  //      //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                  //      check = true;
                  //      //_Entity.InsertData(_Entity);//Method of inserting for header
                  //      _Entity.AddedBy = Session["userid"].ToString();
                        
                  //      _Entity.UpdateData(_Entity);
                  //      gv1.DataSourceID = "odsDetail";
                  //      odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;
                  //      gv1.UpdateEdit();//2nd initiation to insert grid
                  //      Validate();
                  //      Post();
                  //      cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                  //      cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                  //      cp.JSProperties["cp_close"] = true;//Close window variable to client side
                  //      Session["Refresh"] = "1";
                  //  }
                  //  else
                  //  {
                  //      cp.JSProperties["cp_message"] = "Please check all the fields!";
                  //      cp.JSProperties["cp_success"] = true;
                  //  }

                  //  break;

                case "Update":
                    string strError1 = Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseReturn",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                           gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        //gv1.UpdateEdit();
                        _Entity.TransType = Request.QueryString["transtype"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header
                        if (Session["PRTDatatable"] == "1")
                        {
                            gv1.DataSource = GetSelectedVal();
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = odsDetail.ID;//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Post();
                        Validate();
                        if (cp.JSProperties["cp_valmsg"].ToString() != "")
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["PRTDatatable"] = null;
                        gv1.DataSource = null;
                        
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                   

                    break;

                case "Delete":
                    //GetSelectedVal(); 
                    //cp.JSProperties["cp_delete"] = true;
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

                case "Generate":
                    
                    break;

                case "RR":
                    SqlDataSource ds = Masterfilebizcustomer;
                    ds.SelectCommand = string.Format("SELECT A.DocNumber,A.SupplierCode as BizPartnerCode, B.Name from Procurement.ReceivingReport A left join Masterfile.BizPartner B on A.SupplierCode = B. BizPartnerCode where docnumber = '" + gvRRDoc.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtSupplier.Value = tran[0][1].ToString();
                    }

                    ds.SelectCommand = string.Format("select DRNumber,WarehouseCode,ExchangeRate,Currency from Procurement.ReceivingReport where docnumber = '" + gvRRDoc.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtDRDoc.Value = inb[0][0].ToString();
                        gvWarehouse.Value = inb[0][1].ToString();
                        txtExchange.Value = inb[0][2].ToString();
                        txtCurrency.Value = inb[0][3].ToString();
                    }

                    ds.SelectCommand = string.Format("select APTerms from Procurement.ReceivingReport A inner join Masterfile.BPSupplierInfo B on A.SupplierCode = B.SupplierCode where docnumber = '" + gvRRDoc.Text + "'");
                    DataView term = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (term.Count > 0)
                    {
                        txtTerms.Value = term[0][0].ToString();
                      
                    }
                    
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;

                    DataTable unit = Gears.RetriveData2("Select SUM(UnitCost) from Procurement.ReceivingReportDetailPO where DocNumber = '" + gvRRDoc.Text + "'", Session["ConnString"].ToString());
                     
                    foreach (DataRow dtrow in unit.Rows)
                    {
                        cp.JSProperties["cp_unitcost"] = dtrow[0].ToString();
                    }
                      DataTable getvat = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                        "on A.TaxCode = B.TCode "+
                                                        "where SupplierCode = '" + txtSupplier.Text + "'", Session["ConnString"].ToString());
                 
                        if (getvat.Rows.Count > 0)
                        {
                            cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
                        }
                    
                        else
                        {
                            cp.JSProperties["cp_vatrate"] = "0";
                        }
                          DataTable getatc = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.ATC B " +
                                                        "on A.ATCCode = B.ATCCode " +
                                                        "where SupplierCode = '" + txtSupplier.Text + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());

                          if (getatc.Rows.Count > 0)
                        {
                            cp.JSProperties["cp_atc"] = getatc.Rows[0]["Rate"].ToString();
                        }
                    
                        else
                        {
                            cp.JSProperties["cp_atc"] = "0";
                        }
                    //
                    break;

            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
           

            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
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
                e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
            if (Session["PRTDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0], values.Keys[1] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ReturnedBulkQty"] = values.NewValues["ReturnedBulkQty"];
                   // row["CountSheet"] = values.NewValues["CountSheet"];
                    row["IsVat"] = values.NewValues["IsVat"];
                    row["VatCode"] = values.NewValues["VatCode"];
                    row["ReturnedQty"] = values.NewValues["ReturnedQty"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["UnitCost"] = values.NewValues["UnitCost"];
                    row["BaseQty"] = values.NewValues["BaseQty"];
                    row["AverageCost"] = values.NewValues["AverageCost"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
                    //row["PropertyNumber"] = values.NewValues["PropertyNumber"];
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

                Gears.RetriveData2("Delete from procurement.PurchaseReturnDetail where DocNumber = '" + txtDoc.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.PODocNumber = dtRow["PODocNumber"].ToString();
                    _EntityDetail.ReturnedQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["ReturnedQty"].ToString()) ? "0" : dtRow["ReturnedQty"].ToString());
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.UnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["UnitCost"].ToString()) ? "0" : dtRow["UnitCost"].ToString());
                    _EntityDetail.BaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["BaseQty"].ToString()) ? "0" : dtRow["BaseQty"].ToString());
                    _EntityDetail.AverageCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["AverageCost"].ToString()) ? "0" : dtRow["AverageCost"].ToString());
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.ReturnedBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["ReturnedBulkQty"].ToString()) ? "0" : dtRow["ReturnedBulkQty"].ToString());
                    //_EntityDetail.CountSheet = dtRow["CountSheet"].ToString();
                    //_EntityDetail.PropertyNumber = dtRow["PropertyNumber"].ToString();
                    _EntityDetail.IsVat = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVat"]) ? false : dtRow["IsVat"]);
                    _EntityDetail.VatCode = dtRow["VatCode"].ToString();
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();

                    _EntityDetail.AddPurchaseReturnDetail(_EntityDetail);

                    //_EntityDetail.DeleteOutboundDetail(_EntityDetail);
                }
            }
        }
        #endregion

        protected void gvSupplier_TextChanged(object sender, EventArgs e)
        {

        }


        private DataTable GetSelectedVal()
        {
            gv1.DataSourceID = null;

            DataTable dt = new DataTable();
            string[] selectedValues = gvRRDoc.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(gvRRDoc.KeyFieldName, selectedValues);
            sdsRRDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["RRses"] = sdsRRDetail.FilterExpression;
            gv1.DataSource = sdsRRDetail;
            gv1.DataBind();
            Session["PRTDatatable"] = "1";

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
            dt.Columns["DocNumber"],dt.Columns["LineNumber"]};


            return dt;
        }

        protected void gv1_Init(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["RRses"] = null;
            }

            if (Session["RRses"] != null)
            {
                gv1.DataSource = sdsRRDetail;
                sdsRRDetail.FilterExpression = Session["RRses"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        //#endregion

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {
            //       //This is the datasource where we will get the connection string
            //     SqlDataSource ds = OCN;
            //     //This is the sql command that we will initiate to find the data that we want to set to the textbox.
            //// (the e.Parameter is the callback item that we sent to the server)
            //     ds.SelectCommand = string.Format("SELECT OutgoingDocType FROM WMS.[PurchaseOrderRevision] WHERE DocNumber = '" + e.Parameter + "'");
            // //This is where we now initiate the command and get the data from it using dataview	
            //     DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //     if (biz.Count > 0)
            //     {
            // //Now, this is the part where we assign the following data to the textbox
            //         //txttype.Text = biz[0][0].ToString();
            //     }
        }

        protected void dtpDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDate.Date = DateTime.Now;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            RRDoc.ConnectionString = Session["ConnString"].ToString();

            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            Warehouse.ConnectionString = Session["ConnString"].ToString();
            sdsRRDetail.ConnectionString = Session["ConnString"].ToString();
            Reason.ConnectionString = Session["ConnString"].ToString();


        }

        //private DataTable GeneratePO()
        //{
        //    DataTable billing = Gears.RetriveData2("EXEC sp_GeneratePORevision '" + gvRRDoc.Text + "'");

        //    //DataTable gen = new DataTable();
        //    gv1.DataSource = billing;
        //    if (gv1.DataSourceID != "")
        //    {
        //        gv1.DataSourceID = null;
        //    }
        //    gv1.DataBind();

        //    return billing;
        //}
    }
}