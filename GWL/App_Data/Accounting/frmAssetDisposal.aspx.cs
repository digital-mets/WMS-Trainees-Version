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



            if (!IsPostBack)
            {
              
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                
                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //gv1.DataSourceID = "sdsDetail";
                    popup.ShowOnPageLoad = false;
                    dtDocDate.Text = DateTime.Now.ToShortDateString();
                    //deExpDelDate.Text = DateTime.Now.ToShortDateString();
                }
                //else
                //{
                    // gv1.DataSourceID = "odsDetail";
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN


                    // Moved inside !IsPostBack   LGE 03 - 02 - 2016
                    // V=View, E=Edit, N=New
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
                            setDisposalType();
                            break;
                        case "E":
                            updateBtn.Text = "Update";
                            edit = true;
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
                    //--------------------------------------------------
                    //--------------------------------------------------

                    txtInvoiceDocNumber.Value = _Entity.InvoiceDocNumber;
                    dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    txtTotalAmountSold.Value = _Entity.TotalAmountSold;
                    txtTotalGrossVatableAmount.Value = _Entity.GrossVATAmount;
                    txtTotalNonGrossVatableAmount.Value = _Entity.GrossNonVATAmount;

                    cbDisposalType.Value = _Entity.DisposalType;
                    glSoldTo.Value = _Entity.SoldTo.ToString();
                    txtRemarks.Value = _Entity.Remarks;

                    txtTotalCostAsset.Value = _Entity.TotalAssetCost;
                    txtTotalAccumulatedDepreciationRecord.Value = _Entity.TotalAccumulatedDepreciation;
                    txtNetBookValue.Value = _Entity.NetBookValue;
                    txtTotalGainLoss.Value = _Entity.TotalGainLoss;



                    //txtDocnumber.Value = _Entity.DocNumber;
                    //dtDocDate.Value = _Entity.DocDate;
                    //glCustomerCode.Value = _Entity.CustomerCode;
                    //glCostCenter.Value = _Entity.CostCenter;
                    //txtRemarks.Value = _Entity.Remarks;
                    //txtStatus.Value = _Entity.Status;
                    //chkIsPrinted.Value = _Entity.IsPrinted;
                    //txtPODocNumber.Value = _Entity.PODocNumber;


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
                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;
                    txtCancelledBy.Text = _Entity.CancelledBy;
                    txtCancelledDate.Text = _Entity.CancelledDate;
                    txtPostedBy.Text = _Entity.PostedBy;
                    txtPostedDate.Text = _Entity.PostedDate;


                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    sdsDetail.SelectCommand = "SELECT * FROM Accounting.AssetDisposaldetail WHERE DocNumber is null";
                //    sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}

                    DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM Accounting.AssetDisposalDetail WHERE DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                    gvJournal.DataSourceID = "odsJournalEntry";

                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;

            }



            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (cbDisposalType.Value == "Retirement")
                {
                    glSoldTo.ClientEnabled = false;

                    gv1.Columns["UnitPrice"].Width = 0;
                    gv1.Columns["SoldAmount"].Width = 0;
                    gv1.Columns["IsVat"].Width = 0;
                    gv1.Columns["VATCode"].Width = 0;
                }


                if (cbDisposalType.Value == "Sales")
                {
                    glSoldTo.ClientEnabled = true;


                    gv1.Columns["UnitPrice"].Width = 180;
                    gv1.Columns["SoldAmount"].Width = 230;
                    gv1.Columns["IsVat"].Width = 100;
                    gv1.Columns["VATCode"].Width = 150;
                }

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

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
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
                        if(cbDisposalType.Text == "Sales")
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
                                    e.Visible = false;
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
                    else
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = false;
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

            _Entity.DocNumber     = txtDocnumber.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate       = dtDocDate.Text;

            _Entity.TotalAmountSold = Convert.ToDecimal(Convert.IsDBNull(txtTotalAmountSold.Value) ? 0 : txtTotalAmountSold.Value);
            _Entity.GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalGrossVatableAmount.Value) ? 0 : txtTotalGrossVatableAmount.Value);
            _Entity.GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalNonGrossVatableAmount.Value) ? 0 : txtTotalNonGrossVatableAmount.Value);


            _Entity.DisposalType = cbDisposalType.Text;
            _Entity.SoldTo = glSoldTo.Text;
            _Entity.Remarks       = txtRemarks.Text;

            _Entity.TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(txtTotalCostAsset.Value) ? 0 : txtTotalCostAsset.Value);
            _Entity.TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(txtTotalAccumulatedDepreciationRecord.Value) ? 0 : txtTotalAccumulatedDepreciationRecord.Value);
            _Entity.NetBookValue = Convert.ToDecimal(Convert.IsDBNull(txtNetBookValue.Value) ? 0 : txtNetBookValue.Value);
            _Entity.TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(txtTotalGainLoss.Value) ? 0 : txtTotalGainLoss.Value);
            //_Entity.Status        = txtStatus.Text;

            //_Entity.IsPrinted     = Convert.ToBoolean(chkIsPrinted.Value);

            //_Entity.TargetDate = dtTargetDate.Text;

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
                    strError = Functions.Submitted(_Entity.DocNumber,"Accounting.AssetDisposal",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
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
                case "Update":

                    gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber,"Accounting.AssetDisposal",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
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
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;

                //case "getvat":
                //    GetVat();
                //    break;

                case "selectsales":
                    gv1.Columns["UnitPrice"].Width = 180;
                    gv1.Columns["SoldAmount"].Width = 230;
                    gv1.Columns["IsVat"].Width = 100;
                    gv1.Columns["VATCode"].Width = 150;


                    cp.JSProperties["cp_disposaltype"] = "saletype";
                    break;

                case "selectretirement":
                    gv1.Columns["UnitPrice"].Width = 0;
                    gv1.Columns["SoldAmount"].Width = 0;
                    gv1.Columns["IsVat"].Width = 0;
                    gv1.Columns["VATCode"].Width = 0;


                    cp.JSProperties["cp_disposaltype"] = "retirementtype";
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

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
            SoldToLookup.ConnectionString = Session["ConnString"].ToString();
            PropertyNumberLookup.ConnectionString = Session["ConnString"].ToString();
            AssetAcquisitionLookup.ConnectionString = Session["ConnString"].ToString();
            VatCodeLookup.ConnectionString = Session["ConnString"].ToString();

        }

    }
}