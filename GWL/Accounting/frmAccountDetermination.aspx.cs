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
using GearsAccounting;

namespace GWL
{
    public partial class frmAccountDetermination : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state

        Entity.AccountDetermination _Entity = new AccountDetermination();//Calls entity ICN
        Entity.AccountDetermination.AccountDeterminationDetail _EntityDetail = new AccountDetermination.AccountDeterminationDetail();//Call entity POdetails

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
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

            gv1.KeyFieldName = "Transtype;TOACode";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.



            if (!IsPostBack)
            {
              
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //gv1.DataSourceID = "sdsDetail";
                    popup.ShowOnPageLoad = false;
                    
                    //deExpDelDate.Text = DateTime.Now.ToShortDateString();
                }
                //else
                //{
                    // gv1.DataSourceID = "odsDetail";
                //_Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN
                glTransType.Value = Request.QueryString["docnumber"].ToString();
                _Entity.getdata(glTransType.Text, Session["ConnString"].ToString());

                    // Moved inside !IsPostBack   LGE 03 - 02 - 2016
                    // V=View, E=Edit, N=New
                    switch (Request.QueryString["entry"].ToString())
                    {
                        case "N":
                            if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                            {
                                updateBtn.Text = "Add";
                            }
                            else
                            {
                                updateBtn.Text = "Update";
                            }
                            break;
                        case "E":
                            updateBtn.Text = "Update";
                            edit = true;
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
                    //--------------------------------------------------
                    //--------------------------------------------------

                    //txtInvoiceDocNumber.Value = _Entity.InvoiceDocNumber;
                    //dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    //txtTotalAmountSold.Value = _Entity.TransType;
                    //txtTotalGrossVatableAmount.Value = _Entity.TransType;
                    //txtTotalNonGrossVatableAmount.Value = _Entity.TransType;

                    
                    //glTransType.Value = _Entity.TransType;
                    txtDescription.Value = _Entity.Description;
                    glModuleID.Value = _Entity.Module;

                


                    //dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    //dtTargetDate.Value = String.IsNullOrEmpty(_Entity.TargetDate) ? DateTime.Now : Convert.ToDateTime(_Entity.TargetDate);


                    txtHField1.Value = _Entity.Field1;
                    txtHField2.Value = _Entity.Field2;
                    txtHField3.Value = _Entity.Field3;
                    txtHField4.Value = _Entity.Field4;
                    txtHField5.Value = _Entity.Field5;
                    txtHField6.Value = _Entity.Field6;
                    txtHField7.Value = _Entity.Field7;
                    txtHField8.Value = _Entity.Field8;
                    txtHField9.Value = _Entity.Field9;


                    txtHAddedBy.Text = _Entity.AddedBy;
                    txtHAddedDate.Text = _Entity.AddedDate;
                    txtHLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHLastEditedDate.Text = _Entity.LastEditedDate;

                    if (Request.QueryString["entry"].ToString() == "E")
                    {
                        glTransType.ClientEnabled = false;
                        txtDescription.ClientEnabled = false;
                        glModuleID.ClientEnabled = false;
                    }
                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    sdsDetail.SelectCommand = "SELECT * FROM Accounting.AssetDisposaldetail WHERE DocNumber is null";
                //    sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}

                    DataTable dtblDetail = Gears.RetriveData2("Select TransType FROM Accounting.AccountDeterminationDetail WHERE TransType = '" + glTransType.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                    //gvJournal.DataSourceID = "odsJournalEntry";

            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._Connection = Session["ConnString"].ToString();
            //gparam._DocNo = _Entity.TransType;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "ACTDET";

            //string strresult = GearsAccounting.GAccounting.AssetDisposal_Validate(gparam);

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        }
        #endregion

        #region Post
        private void Post()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "ACTADI";
            //gparam._Table = "Accounting.AssetDisposal";
            //gparam._Factor = -1;
            //string strresult = GearsAccounting.GAccounting.AssetDisposal_Post(gparam);
            //if (strresult != " ")
            //{
            //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            //}
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
            //var look = sender as ASPxGridLookup;
            //if (look != null)
            //{
            //    look.ReadOnly = view;
            //}
        }

        protected void CostCenterLookupLoad(object sender, EventArgs e)
        {
            if(edit != false)
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = !edit;
                look.ReadOnly = edit;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = !view;
                look.ReadOnly = view;
            }

        }

        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
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
            ////}
            //if (Request.QueryString["entry"].ToString() == "V")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = true;
            //}
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all lookup in header
        {

            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.DropDownButton.Enabled = !view;
            combobox.Enabled = !view;
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                //dtDocDate.Date = DateTime.Now;
            }
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

                //if (!string.IsNullOrEmpty(glTransType.Text))
                //{
                //    if (e.ButtonType == ColumnCommandButtonType.New)
                //        e.Visible = true;
                //}

                if (Request.QueryString["entry"].ToString() == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New)
                            e.Visible = false;
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

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D" || Request.QueryString["entry"].ToString() == "E")
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
                gridLookup.GridView.DataSourceID = "AssetAcquisitionLookup";
                AssetAcquisitionLookup.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
            else
            {
                gridLookup.GridView.DataSourceID = "AssetAcquisitionLookup";
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string propertynumber = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var propertynumberlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("PropertyNumber"))
            {
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,ItemCode,Qty,UnitCost,AccumulatedDepreciation,Status from Accounting.AssetInv where propertynumber = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN
                
                    foreach (DataRow dt in countcolor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["ItemCode"].ToString() + ";";
                        codes += dt["Qty"].ToString() + ";";
                        codes += dt["UnitCost"].ToString() + ";";
                        codes += dt["AccumulatedDepreciation"].ToString() + ";";
                        codes += dt["Status"].ToString() + ";";

                    }

                    //DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode!='NONV' THEN 'True' ELSE 'False' END AS IsVat, Rate from masterfile.BPCustomerInfo BPCI LEFT JOIN Masterfile.Tax T ON BPCI.TaxCode = T.TCode where BizPartnerCode =  '" + glSoldTo.Text + "'", Session["ConnString"].ToString());//ADD CONN

                    //foreach (DataRow dt in tax.Rows)
                    //{
                    //    codes += dt["IsVat"].ToString() + ";";
                    //    codes += dt["TaxCode"].ToString() + ";";
                    //    codes += dt["Rate"].ToString() + ";";
                    //}


                    propertynumberlookup.JSProperties["cp_identifier"] = "ItemCode";
                    propertynumberlookup.JSProperties["cp_codes"] = codes;
            }

            else if (e.Parameters.Contains("CheckPNumber"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                string[] transdoc = propertynumber.Split(';');
                var selectedValues = transdoc;
                CriteriaOperator selectionCriteria = new InOperator("PropertyNumber", transdoc);
                CriteriaOperator not = new NotOperator(selectionCriteria);
                PropertyNumberLookup.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not)).ToString();
                Session["FilterExpression"] = PropertyNumberLookup.FilterExpression;
                grid.DataSourceID = "PropertyNumberLookup";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "PropertyNumber") != null)
                        if (grid.GetRowValues(i, "PropertyNumber").ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "PropertyNumber").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }


            else if (e.Parameters.Contains("VATCode"))
            {
                DataTable vat = Gears.RetriveData2("SELECT DISTINCT ISNULL(Rate,1) AS Rate FROM Masterfile.Tax WHERE TCode = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                if (vat.Rows.Count > 0)
                {
                    foreach (DataRow dt in vat.Rows)
                    {
                        codes = dt["Rate"].ToString();
                    }
                }

                propertynumberlookup.JSProperties["cp_identifier"] = "VAT";
                propertynumberlookup.JSProperties["cp_codes"] = Convert.ToDecimal(codes) + ";";
            }

            


            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[1], "glPropertyNumber");
                var selectedValues = propertynumber;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { propertynumber });
                AssetAcquisitionLookup.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = AssetAcquisitionLookup.FilterExpression;
                grid.DataSourceID = "AssetAcquisitionLookup";
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

            _Entity.TransType = glTransType.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.Description = txtDescription.Text;
            _Entity.Module = glModuleID.Text;


            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;


            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.InsertData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["Trans"].DefaultValue = glTransType.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        //Post();
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
                case "Update":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["Trans"].DefaultValue = glTransType.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        //Post();
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
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;

                case "TransTypeCase":
                    SetText();
                    break;
            }
        }

        //protected void GetVat()
        //{
        //    DataTable getvat = Gears.RetriveData2("Select DISTINCT Rate from masterfile.BPCustomerInfo BPCI LEFT JOIN Masterfile.Tax T ON BPCI.TaxCode = T.TCode where BizPartnerCode =  '" + glSoldTo.Text + "'");

        //    if (getvat.Rows.Count > 0)
        //    {
        //        cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
        //    }

        //    else
        //    {
        //        cp.JSProperties["cp_vatrate"] = "0ite
        //    }

        //}

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            //foreach (GridViewColumn column in gv1.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "Type"
            //        && dataColumn.FieldName != "UnitPrice" && dataColumn.FieldName != "Remarks" && dataColumn.FieldName != "Qty"
            //        && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4"
            //        && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
            //        && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "VatRate" && dataColumn.FieldName != "IsVatable" && dataColumn.FieldName != "UnitCost"
            //        && dataColumn.FieldName != "ItemCode" && dataColumn.FieldName != "ColorCode" && dataColumn.FieldName != "SizeCode" && dataColumn.FieldName != "ClassCode"
            //        && dataColumn.FieldName != "AccumulatedDepreciation" && dataColumn.FieldName != "VatRate")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //    //Checking for non existing Codes..

            //    ItemCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ItemCode"])) ? "" : e.NewValues["ItemCode"].ToString();
            //    ColorCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ColorCode"])) ? "" : e.NewValues["ColorCode"].ToString(); 
            //    ClassCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ClassCode"])) ? "" : e.NewValues["ClassCode"].ToString(); 
            //    SizeCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["SizeCode"])) ? "" : e.NewValues["SizeCode"].ToString();
             
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
            //}

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
                //e.InsertValues.Clear();
                //e.UpdateValues.Clear();
            }
        }
        #endregion


        protected void SetText()
        {
            //SqlDataSource ds = SODocNumberlookup;
            //ds.SelectCommand = string.Format("Select DocNumber from Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'");
            //DataView sodocnum = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (sodocnum.Count > 0)
            //{
            //    glSODocNumber.Text = sodocnum[0][0].ToString();
            //}

            //ds.SelectCommand = string.Format("Select CustomerPONo,DocNumber from Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'");
            //DataView oldcustomerponumber = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (oldcustomerponumber.Count > 0)
            //{
            //    txtOldCustomerPONumber.Value = oldcustomerponumber[0][0].ToString();
            //}


            DataTable getDescription = Gears.RetriveData2("Select Prompt FROM IT.MainMenu WHERE TransType = '" + glTransType.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getDescription.Rows)
            {
                txtDescription.Text = dt[0].ToString();
            }

            DataTable getModuleID = Gears.RetriveData2("Select ModuleID FROM IT.MainMenu WHERE TransType = '" + glTransType.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getModuleID.Rows)
            {
                glModuleID.Text = dt[0].ToString();
            }

        }
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
       //     ds.SelectCommand = string.Format("SELECT OutgoingDocType FROM WMS.[ICN] WHERE DocNumber = '" + e.Parameter + "'");
       // //This is where we now initiate the command and get the data from it using dataview	
       //     DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
       //     if (biz.Count > 0)
       //     {
       // //Now, this is the part where we assign the following data to the textbox
       //         //txttype.Text = biz[0][0].ToString();
       //     }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            CustomerCodelookup.ConnectionString = Session["ConnString"].ToString();
            CostCenterlookup.ConnectionString = Session["ConnString"].ToString();
            TransTypeLookup.ConnectionString = Session["ConnString"].ToString();
            ModuleIDLookup.ConnectionString = Session["ConnString"].ToString();
            AssetAcquisitionLookup.ConnectionString = Session["ConnString"].ToString();
            AccountCodeLookup.ConnectionString = Session["ConnString"].ToString();
            PropertyNumberLookup.ConnectionString = Session["ConnString"].ToString();

        }

     

    }
}