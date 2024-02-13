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
using GearsSales;


namespace GWL
{
    public partial class frmSalesQuotation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.S_Quotation _Entity = new S_Quotation();//Calls entity Quotation
        Entity.S_Quotation.QuotationDetail _EntityDetail = new S_Quotation.QuotationDetail();//Call entity Quotationdetails

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

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry and delete
            }

            Masterfileitemdetail.SelectCommand = "SELECT DISTINCT A.[ItemCode], A.[ColorCode], A.[ClassCode],A.[SizeCode] "
                                               + " ,BaseUnit AS Unit, OnHand AS DeliveredQty, Price AS UnitPrice "
                                               + " FROM Masterfile.[ItemDetail] A LEFT JOIN Masterfile.ItemCustomerPrice B "
                                               + " ON A.ItemCode = B.ItemCode WHERE isnull(IsInactive,'') = 0";
            
            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();

                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                
                if (Request.QueryString["entry"].ToString() == "N")
                {
                     dtpDocDate.Text = DateTime.Now.ToShortDateString();
                     dtpTargetDeliveryDate.Text = DateTime.Now.ToShortDateString();
                     DateTime docdate1 = Convert.ToDateTime(dtpDocDate.Value);
                     DateTime answer = docdate1.AddDays(30);
                     dtpValidity.Text = answer.ToShortDateString();
                     popup.ShowOnPageLoad = false;
                }

                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                glCustomerCode.Value = _Entity.CustomerCode;
                txtStatus.Text = _Entity.Status;
                dtpValidity.Value = String.IsNullOrEmpty(_Entity.Validity) ? DateTime.Now : Convert.ToDateTime(_Entity.Validity);
                if (String.IsNullOrEmpty(_Entity.TargetDeliveryDate))
                {
                    dtpTargetDeliveryDate.Text = DateTime.Now.ToShortDateString();
                    DateTime docdate1 = Convert.ToDateTime(dtpDocDate.Value);
                    DateTime answer = docdate1.AddDays(30);
                    dtpValidity.Text = answer.ToShortDateString();
                }
                else
                    dtpTargetDeliveryDate.Value = String.IsNullOrEmpty(_Entity.TargetDeliveryDate) ? DateTime.Now : Convert.ToDateTime(_Entity.TargetDeliveryDate);
                txtRemarks.Text = _Entity.Remarks;
                txtTotalQty.Text = _Entity.TotalQty.ToString();
                txtInitialTotalAmount.Text = _Entity.InitialTotalAmount.ToString();
                txtAmountDiscounted.Text = _Entity.AmountDiscounted.ToString();
                //txtFreight.Text = _Entity.Freight.ToString();
                txtPesoAmount.Text = _Entity.PesoAmount.ToString();
                txtForeignAmount.Text = _Entity.ForeignAmount.ToString();
                txtGrossVATableAmount.Text = _Entity.GrossVATableAmount.ToString();
                txtNonVATableAmount.Text = _Entity.NonVATableAmount.ToString();
                txtVATAmount.Text = _Entity.VATAmount.ToString();
                txtCurrency.Text = _Entity.Currency;
                txtTerms.Text = _Entity.Terms.ToString();
                txtExchangeRate.Text = _Entity.ExchangeRate.ToString();
                //cbxIsPrinted.Value = _Entity.IsPrinted;

                txtHAddedBy.Value = _Entity.AddedBy;
                txtHAddedDate.Value = _Entity.AddedDate;
                txtHLastEditedBy.Value = _Entity.LastEditedBy;
                txtHLastEditedDate.Value = _Entity.LastEditedDate;
                txtHSubmittedBy.Value = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = _Entity.SubmittedDate;
                txtHCancelledBy.Value = _Entity.CancelledBy;
                txtHCancelledDate.Value = _Entity.CancelledDate;
                //txtUsageTrail.Value = _Entity.UsageTrail;

                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

                updateBtn.Text = "Add";
                if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Update";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                }
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        //updateBtn.Text = "Add";
                        txtStatus.Value = "N";
                        txtExchangeRate.Text = "1";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.QuotationDetail WHERE DocNumber ='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "SLSQUM";

            string strresult = GearsSales.GSales.SalesQuotation_Validate(gparam);
            
            gv1.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

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
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
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

                if (Request.QueryString["entry"] == "V")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New)
                    {
                        e.Visible = false;
                    }
                }
            
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N" )
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
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
            }
            else
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
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
            string qty = "";

            if (e.Parameters.Contains("ItemCode"))
            {
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,UnitBase,StatusCode from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["StatusCode"].ToString() + ";";
                }

                DataTable uprice = Gears.RetriveData2("SELECT DISTINCT ISNULL(UpdatedPrice,0) AS UnitPrice, 0 AS vatrate from masterfile.Item where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                
                //select DISTINCT Price AS UnitPrice, 0 AS vatrate from masterfile.BPCustomerInfo A LEFT JOIN Masterfile.ItemCustomerPrice B ON A.BizPartnerCode = B.Customer where Customer = '" + glCustomerCode.Text + "' AND A.BizPartnerCode = '" + glCustomerCode.Text + "'");
                //select DISTINCT Price AS UnitPrice, A.TaxCode, CASE TaxCode WHEN 'V' THEN 'True' ELSE 'False' END AS IsVat from masterfile.BPCustomerInfo A LEFT JOIN Masterfile.ItemCustomerPrice B ON A.BizPartnerCode = B.Customer where Customer = '" + glCustomerCode.Text + "' AND A.BizPartnerCode = '" + glCustomerCode.Text + "'"
                 
                foreach (DataRow dt in uprice.Rows)
                {
                    codes += dt["UnitPrice"].ToString() + ";";
                    codes += dt["vatrate"].ToString() + ";";
                }
                itemlookup.JSProperties["cp_identifier"] = "ItmCde";
                itemlookup.JSProperties["cp_codes"] = codes + ";";
            }
            else if (e.Parameters.Contains("VatCode"))
            {
                if (String.IsNullOrEmpty(itemcode)) { itemcode = "NONV"; }
                DataTable getqty = Gears.RetriveData2("Select Rate from masterfile.Tax where Tcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["Rate"].ToString();
                    }
                }
                itemlookup.JSProperties["cp_identifier"] = "VAT";
                itemlookup.JSProperties["cp_codes"] = Convert.ToDecimal(qty) + ";";
                //itemlookup.JSProperties["cp_codes"] = qty + ";";
            }
            else
            {
                if (e.Parameters.Contains("ColorCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail";
                }

                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
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
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {


            switch (e.Parameter)
            {
                //case "Add":
                //    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method
                //    string strError = Functions.Submitted(_Entity.DocNumber,"Sales.Quotation",2,Connection);//NEWADD factor 1 if submit, 2 if approve
                //        if (!string.IsNullOrEmpty(strError))
                //        {
                //            cp.JSProperties["cp_message"] = strError;
                //            cp.JSProperties["cp_success"] = true;
                //            cp.JSProperties["cp_forceclose"] = true;
                //            return;
                //        }
                //    if (error == false)
                //    {
                //        check = true;
                //        _Entity.InsertData(_Entity);//Method of inserting for header
                //        _Entity.AddedBy = Session["userid"].ToString();
                //        _Entity.UpdateData(_Entity);

                //        gv1.DataSourceID = "odsDetail";
                //        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                //        gv1.UpdateEdit();//2nd initiation to insert grid
                //        Validate();
                //        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                //        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                //        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                //        //Session["Refresh"] = "1";
                //    }
                //    else
                //    {
                //        cp.JSProperties["cp_message"] = "Please check all the fields!";
                //        cp.JSProperties["cp_success"] = true;
                //    }
                //    break;
                //case "Update":
                //    gv1.UpdateEdit();
                //    string strError1 = Functions.Submitted(_Entity.DocNumber,"Sales.Quotation",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                //        if (!string.IsNullOrEmpty(strError1))
                //        {
                //            cp.JSProperties["cp_message"] = strError1;
                //            cp.JSProperties["cp_success"] = true;
                //            cp.JSProperties["cp_forceclose"] = true;
                //            return;
                //        }
                //    if (error == false)
                //    {
                //        check = true;
                //        _Entity.LastEditedBy = Session["userid"].ToString();
                //        _Entity.UpdateData(_Entity);//Method of Updating header

                //        gv1.DataSourceID = "odsDetail";
                //        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                //        gv1.UpdateEdit();//2nd Initiation to update grid
                //        Validate();
                //        cp.JSProperties["cp_message"] = "Successfully Updated!";
                //        cp.JSProperties["cp_success"] = true;
                //        cp.JSProperties["cp_close"] = true;
                //        //Session["Refresh"] = "1";
                //    }
                //    else
                //    {
                //        cp.JSProperties["cp_message"] = "Please check all the fields!";
                //        cp.JSProperties["cp_success"] = true;
                //    }
                //    break;
                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;
                //case "ConfDelete":
                //    _Entity.DeleteData(_Entity);
                //    cp.JSProperties["cp_close"] = true;
                //    cp.JSProperties["cp_message"] = "Successfully deleted";
                //    Session["Refresh"] = "1";
                //    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
                case "Validity":
                    DateTime docdate1 = Convert.ToDateTime(dtpDocDate.Value);
                    DateTime answer = docdate1.AddDays(30);
                    dtpValidity.Text = answer.ToShortDateString();
                    break;
                case "CustomerCodeCase":
                    SqlDataSource ds = sdsCustomer;

                    ds.SelectCommand = string.Format("SELECT BizPartnerCode, Name, ISNULL(Currency, 'N/A') AS Currency, ISNULL(ARTerm, '0') AS ARTerm FROM Masterfile.BPCustomerInfo where BizPartnerCode = '" + glCustomerCode.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtCurrency.Text = inb[0][2].ToString();
                        txtTerms.Text = inb[0][3].ToString();
                    }
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ITMCD = "";
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";
            
            foreach (GridViewColumn column in gv1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "TaxCode" 
                    && dataColumn.FieldName != "IsVat" && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3" 
                    && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7"
                    && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "Unit" && dataColumn.FieldName != "OrderedQty"
                    && dataColumn.FieldName != "DeliveredQty" && dataColumn.FieldName != "DiscountRate" && dataColumn.FieldName != "UnitFreight"
                    && dataColumn.FieldName != "BulkQty" && dataColumn.FieldName != "BulkUnit" && dataColumn.FieldName != "vatrate" && dataColumn.FieldName != "BaseQty"
                    && dataColumn.FieldName != "StatusCode" && dataColumn.FieldName != "UnitFactor" && dataColumn.FieldName != "BarcodeNo")
                    {
                        e.Errors[dataColumn] = "Value can't be null.";
                    }

                //Checking for non existing Codes..
                ItemCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ItemCode"])) ? "" : e.NewValues["ItemCode"].ToString();
                ColorCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ColorCode"])) ? "" : e.NewValues["ColorCode"].ToString(); 
                ClassCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ClassCode"])) ? "" : e.NewValues["ClassCode"].ToString(); 
                SizeCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["SizeCode"])) ? "" : e.NewValues["SizeCode"].ToString();

                DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (item.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
                }
                DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (color.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
                }
                DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (clss.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
                }
                DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (size.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
                }
            }

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
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion
        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IsVat"] = false;
            e.NewValues["TaxCode"] = "NONV";
        }

        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            var lookup = sender as ASPxGridView;
            lookup.JSProperties["cp_identifier"] = "sku";
        }
      
        protected void ConnectionInit_Init(object sender, EventArgs e)
        {

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //sdsBulkUnit.ConnectionString = Session["ConnString"].ToString();
            //sdsTaxCode.ConnectionString = Session["ConnString"].ToString();
            //sdsCustomer.ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //StorerKey.ConnectionString = Session["ConnString"].ToString();
            //Unitlookup.ConnectionString = Session["ConnString"].ToString();
        }

        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.CustomerCode = glCustomerCode.Text;
            _Entity.Status = txtStatus.Text;
            _Entity.Validity = dtpValidity.Text;
            _Entity.TargetDeliveryDate = dtpTargetDeliveryDate.Text;
            _Entity.Remarks = txtRemarks.Text;
            _Entity.TotalQty = txtTotalQty.Text;
            _Entity.InitialTotalAmount = String.IsNullOrEmpty(txtInitialTotalAmount.Text) ? 0 : Convert.ToDecimal(txtInitialTotalAmount.Text);
            _Entity.AmountDiscounted = String.IsNullOrEmpty(txtAmountDiscounted.Text) ? 0 : Convert.ToDecimal(txtAmountDiscounted.Text);
            //_Entity.Freight = String.IsNullOrEmpty(txtFreight.Text) ? 0 : Convert.ToDecimal(txtFreight.Text);
            _Entity.PesoAmount = String.IsNullOrEmpty(txtPesoAmount.Text) ? 0 : Convert.ToDecimal(txtPesoAmount.Text);
            _Entity.ForeignAmount = String.IsNullOrEmpty(txtForeignAmount.Text) ? 0 : Convert.ToDecimal(txtForeignAmount.Text);
            _Entity.GrossVATableAmount = String.IsNullOrEmpty(txtGrossVATableAmount.Text) ? 0 : Convert.ToDecimal(txtGrossVATableAmount.Text);
            _Entity.NonVATableAmount = String.IsNullOrEmpty(txtNonVATableAmount.Text) ? 0 : Convert.ToDecimal(txtNonVATableAmount.Text);
            _Entity.VATAmount = String.IsNullOrEmpty(txtVATAmount.Text) ? 0 : Convert.ToDecimal(txtVATAmount.Text);
            _Entity.Currency = txtCurrency.Text;
            _Entity.Terms = String.IsNullOrEmpty(txtTerms.Text) ? 0 : Convert.ToInt32(txtTerms.Text);
            _Entity.ExchangeRate = String.IsNullOrEmpty(txtExchangeRate.Text) ? 1 : Convert.ToDecimal(txtExchangeRate.Text);
            //_Entity.IsPrinted = Convert.ToBoolean(cbxIsPrinted.Value.ToString());
            _Entity.LastEditedBy = Session["userid"].ToString();

            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            switch (e.Parameters)
            {
                case "Add":
                case "Update":
                    gv1.UpdateEdit();
                    string strError = Functions.Submitted(_Entity.DocNumber, "Sales.Quotation", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        gv1.JSProperties["cp_message"] = strError;
                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_forceclose"] = true;
                        return;
                    }
                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        Validate();
                        gv1.JSProperties["cp_message"] = "Successfully Updated!";
                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_close"] = true;
                        //Session["Refresh"] = "1";
                    }
                    else
                    {
                        gv1.JSProperties["cp_message"] = "Please check all the fields!";
                        gv1.JSProperties["cp_success"] = true;
                    }
                    break;
                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    gv1.JSProperties["cp_close"] = true;
                    gv1.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                    break;
            }
        }
    }
}