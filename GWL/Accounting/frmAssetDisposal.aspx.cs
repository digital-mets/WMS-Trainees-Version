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
    public partial class frmAssetDisposal : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state

        private static string strError;

        Entity.AssetDisposal _Entity = new AssetDisposal();//Calls entity ICN
        Entity.AssetDisposal.AssetDisposalDetail _EntityDetail = new AssetDisposal.AssetDisposalDetail();//Call entity POdetails

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

            dtDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            gv1.KeyFieldName = "DocNumber;LineNumber";

            if (!IsPostBack)
            {
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                    dtDocDate.Text = DateTime.Now.ToShortDateString();
                }

                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());
                
                txtInvoiceDocNumber.Value = _Entity.InvoiceDocNumber;
                dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                speTotalAmountSold.Value = _Entity.TotalAmountSold;
                speTotalGrossVatableAmount.Value = _Entity.GrossVATAmount;
                speTotalNonGrossVatableAmount.Value = _Entity.GrossNonVATAmount;

                cbDisposalType.Value = _Entity.DisposalType;
                glSoldTo.Value = _Entity.SoldTo.ToString();
                memRemarks.Value = _Entity.Remarks;

                speTotalCostAsset.Value = _Entity.TotalAssetCost;
                speTotalAccumulatedDepreciationRecord.Value = _Entity.TotalAccumulatedDepreciation;
                speNetBookValue.Value = _Entity.NetBookValue;
                speTotalGainLoss.Value = _Entity.TotalGainLoss;
                
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
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        else
                        {
                            updateBtn.Text = "Update";
                        }

                        if (string.IsNullOrEmpty(_Entity.DisposalType))
                        {
                            setDisposalType();
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        edit = true;

                        if (string.IsNullOrEmpty(_Entity.DisposalType))
                        {
                            setDisposalType();
                        }
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        txtDocnumber.Enabled = true;
                        txtDocnumber.ReadOnly = false;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }
            }

            DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM Accounting.AssetDisposalDetail WHERE DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
            gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
            
            gvJournal.DataSourceID = "odsJournalEntry";

            if (cbDisposalType.Text == "Retirement")
            {
                glSoldTo.ClientEnabled = false;
                gv1.Columns["UnitPrice"].Width = 0;
                gv1.Columns["SoldAmount"].Width = 0;
                gv1.Columns["IsVat"].Width = 0;
                gv1.Columns["VATCode"].Width = 0;
            }


            if (cbDisposalType.Text == "Sales")
            {
                glSoldTo.ClientEnabled = true;
                gv1.Columns["UnitPrice"].Width = 180;
                gv1.Columns["SoldAmount"].Width = 230;
                gv1.Columns["IsVat"].Width = 100;
                gv1.Columns["VATCode"].Width = 150;
            }
        }
        #endregion

        protected void setDisposalType()
        {
            if(String.IsNullOrEmpty(cbDisposalType.Text))
                cbDisposalType.SelectedIndex = 1;

        }

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTADI";
            string strresult = GearsAccounting.GAccounting.AssetDisposal_Validate(gparam);
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
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTADI";
            gparam._Table = "Accounting.AssetDisposal";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.AssetDisposal_Post(gparam);
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
                dtDocDate.Date = DateTime.Now;
            }
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
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (!String.IsNullOrEmpty(cbDisposalType.Text))
                {
                    if (cbDisposalType.Text == "Sales")
                    {
                        if (!String.IsNullOrEmpty(glSoldTo.Text))
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New)
                            {
                                e.Visible = true;
                            }
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New)
                            {
                                e.Visible = true;
                            }
                        }
                    }

                    else if (cbDisposalType.Text == "Retirement")
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = true;
                        }
                    }
                }

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

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
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

                    DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode!='NONV' THEN 'True' ELSE 'False' END AS IsVat, Rate from masterfile.BPCustomerInfo BPCI LEFT JOIN Masterfile.Tax T ON BPCI.TaxCode = T.TCode where BizPartnerCode =  '" + glSoldTo.Text + "'", Session["ConnString"].ToString());//ADD CONN

                    foreach (DataRow dt in tax.Rows)
                    {
                        codes += dt["IsVat"].ToString() + ";";
                        codes += dt["TaxCode"].ToString() + ";";
                        codes += dt["Rate"].ToString() + ";";
                    }


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
            _Entity.Connection = Session["ConnString"].ToString(); 
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtDocDate.Text;

            _Entity.TotalAmountSold = Convert.ToDecimal(Convert.IsDBNull(speTotalAmountSold.Value) ? 0 : speTotalAmountSold.Value);
            _Entity.GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(speTotalGrossVatableAmount.Value) ? 0 : speTotalGrossVatableAmount.Value);
            _Entity.GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(speTotalNonGrossVatableAmount.Value) ? 0 : speTotalNonGrossVatableAmount.Value);


            _Entity.DisposalType = cbDisposalType.Text;
            _Entity.SoldTo = glSoldTo.Text;
            _Entity.Remarks = memRemarks.Text;

            _Entity.TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(speTotalCostAsset.Value) ? 0 : speTotalCostAsset.Value);
            _Entity.TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(speTotalAccumulatedDepreciationRecord.Value) ? 0 : speTotalAccumulatedDepreciationRecord.Value);
            _Entity.NetBookValue = Convert.ToDecimal(Convert.IsDBNull(speNetBookValue.Value) ? 0 : speNetBookValue.Value);
            _Entity.TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(speTotalGainLoss.Value) ? 0 : speTotalGainLoss.Value);

            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text; 
            
            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM Accounting.AssetDisposal WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());
            
            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();

                    strError = "";
                    strError = Functions.Submitted(_Entity.DocNumber,"Accounting.AssetDisposal",1,Session["ConnString"].ToString());
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

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        gv1.UpdateEdit();

                        Post();
                        Validate();

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

                case "selectsales":
                    gv1.Columns["UnitPrice"].Width = 180;
                    gv1.Columns["SoldAmount"].Width = 230;
                    gv1.Columns["IsVat"].Width = 100;
                    gv1.Columns["VATCode"].Width = 150;

                    //cp.JSProperties["cp_disposaltype"] = "saletype";
                    cp.JSProperties["cp_disposaltype"] = true;
                    break;

                case "selectretirement":
                    gv1.Columns["UnitPrice"].Width = 0;
                    gv1.Columns["SoldAmount"].Width = 0;
                    gv1.Columns["IsVat"].Width = 0;
                    gv1.Columns["VATCode"].Width = 0;

                    //cp.JSProperties["cp_disposaltype"] = "retirementtype";
                    cp.JSProperties["cp_disposaltype"] = true;
                    break;
           //         if (CINDisposalType.GetValue() == "Sales") {
           //    cp.PerformCallback('selectsales');
           //    e.processOnServer = false;
           //    CINSoldTo.SetEnabled(true);

           //}

           //else {
           //    CINSoldTo.SetEnabled(false);
           //    CINSoldTo.SetText("");
           //    cp.PerformCallback('selectretirement');
           //    e.processOnServer = false;
           //}
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

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

    }
}