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
    public partial class frmCashRecon : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state

        private static string strError;

        Entity.CashRecon _Entity = new CashRecon();//Calls entity ICN
        Entity.CashRecon.CashReconDetail _EntityDetail = new CashRecon.CashReconDetail();//Call entity POdetails

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
            gvReconciling.KeyFieldName = "DocNumber;LineNumber";
            if (!IsPostBack)
            {
                //Common.Common.HideUDF(frmlayout1, "udf", Session["ConnString"].ToString());
                //Common.Common.preventAutoCloseCheck(glcheck, Session["ConnString"].ToString());
              
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                
                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //gv1.DataSourceID = "sdsDetail";
                    popup.ShowOnPageLoad = false;
                    //dtDocDate.Text = DateTime.Now.ToShortDateString();
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

                    dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);

                    spinCheckAmount.Value = _Entity.CheckAmountOnHand;
                    spinTotalCashOnHand.Value = _Entity.TotalCashOnHand;
                    spinCashAdvance.Value = _Entity.CashAdvance;

                    spinFundAmount.Value = _Entity.CashFundAmount;
                    spinTotalShortOverCash.Value = _Entity.TotalShortOverCash;
                    UnreplenishedExpenditures.Value = _Entity.UnreplenishedExpenditures;
                    PettyCashReimbursement.Value = _Entity.PettyCashReimbursement;
                    LiquidatedCashAdvance.Value = _Entity.LiquidatedCashAdvance;

                    glFundCode.Text = _Entity.FundCode;
                    txtCashCustodian.Text = _Entity.Custodian;


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

                    DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM Accounting.PettyCashReconDetail WHERE DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDenomination");
                    if (dtblDetail.Rows.Count > 0)
                    {
                        
                        gv1.DataSourceID = "odsDetail";
                        
                    }
                    else
                    {
                        GetDenomination();
                    }

                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;

            }
        }

        private DataTable GetDenomination()
        {
            gv1.DataSource = null;
            gv1.DataSourceID = null;

            DataTable dt = new DataTable();
            gv1.DataSourceID = "sdsDenomination";
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
            dt.Columns["LineNumber"]};
            return dt;
        }
        #endregion



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
                    //if (!String.IsNullOrEmpty(cbDisposalType.Text))
                    //{
                    //    if(cbDisposalType.Text == "Sales")
                    //    {
                    //        if (!String.IsNullOrEmpty(glSoldTo.Text))
                    //        {
                    //            if (e.ButtonType == ColumnCommandButtonType.New)
                    //            {
                    //                e.Visible = true;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (e.ButtonType == ColumnCommandButtonType.New)
                    //            {
                    //                e.Visible = false;
                    //            }
                    //        }
                    //    }

                    //    else if (cbDisposalType.Text == "Retirement")
                    //    {
                    //        if (e.ButtonType == ColumnCommandButtonType.New)
                    //        {
                    //            e.Visible = true;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (e.ButtonType == ColumnCommandButtonType.New)
                    //    {
                    //        e.Visible = false;
                    //    }
                    //}
                    
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

                    DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode!='NONV' THEN 'True' ELSE 'False' END AS IsVat, Rate from masterfile.BPCustomerInfo BPCI LEFT JOIN Masterfile.Tax T ON BPCI.TaxCode = T.TCode where BizPartnerCode =  '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

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
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber     = txtDocnumber.Text;
            _Entity.DocDate       = dtDocDate.Text;

            _Entity.CheckAmountOnHand = Convert.ToDecimal(Convert.IsDBNull(spinCheckAmount.Value) ? 0 : spinCheckAmount.Value);
            _Entity.TotalCashOnHand = Convert.ToDecimal(Convert.IsDBNull(spinTotalCashOnHand.Value) ? 0 : spinTotalCashOnHand.Value);
            _Entity.CashAdvance = Convert.ToDecimal(Convert.IsDBNull(spinCashAdvance.Value) ? 0 : spinCashAdvance.Value);
            _Entity.CashFundAmount = Convert.ToDecimal(Convert.IsDBNull(spinFundAmount.Value) ? 0 : spinFundAmount.Value);
            _Entity.TotalShortOverCash = Convert.ToDecimal(Convert.IsDBNull(spinTotalShortOverCash.Value) ? 0 : spinTotalShortOverCash.Value);
            _Entity.UnreplenishedExpenditures = Convert.ToDecimal(Convert.IsDBNull(UnreplenishedExpenditures.Value) ? 0 : UnreplenishedExpenditures.Value);
            _Entity.PettyCashReimbursement = Convert.ToDecimal(Convert.IsDBNull(PettyCashReimbursement.Value) ? 0 : PettyCashReimbursement.Value);
            _Entity.LiquidatedCashAdvance = Convert.ToDecimal(Convert.IsDBNull(LiquidatedCashAdvance.Value) ? 0 : LiquidatedCashAdvance.Value);

            _Entity.FundCode = glFundCode.Text;
            _Entity.Custodian = txtCashCustodian.Text;
            

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
                case "Update":

                    //gv1.UpdateEdit();
                    //strError = Functions.Submitted(_Entity.DocNumber,"Accounting.AssetDisposal",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    //    if (!string.IsNullOrEmpty(strError))
                    //    {
                    //        cp.JSProperties["cp_message"] = strError;
                    //        cp.JSProperties["cp_success"] = true;
                    //        cp.JSProperties["cp_forceclose"] = true;
                    //        return;
                    //    }

                    check = true;

                    _Entity.LastEditedBy = Session["userid"].ToString();
                    _Entity.UpdateData(_Entity);//Method of Updating header

                    if (Session["Datatable"] == "1")
                    {
                        gv1.DataSource = GetDenomination();
                        gv1.UpdateEdit();
                    }
                    else
                    {
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();//2nd Initiation to update grid
                    }
                    
                    //Post();
                    //Validate();
                    cp.JSProperties["cp_message"] = "Successfully Updated!";
                    cp.JSProperties["cp_success"] = true;
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";


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

                case "CashFundCode":
                    setText();
                    break;

                case "Generate":
                    //Session["newsource"] = null;
                    //SetText();
                   // gv1.JSProperties["cp_generated"] = true;
                    GetSelectedVal();
                    //gv1.Columns["PRNumber"].Width = 100;
                    break;

            }
        }

        protected void setText()
        {
            DataTable propertydata = Gears.RetriveData2("SELECT Custodian, CashFundAmount FROM Masterfile.PettyCashFundSetup where FundCode = '" + glFundCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in propertydata.Rows)
            {
                txtCashCustodian.Text = dt[0].ToString();
                spinFundAmount.Value = dt[1].ToString();
            }
        }
        private DataTable GetSelectedVal()
        {
            gvReconciling.DataSourceID = null;

            DataTable dt = new DataTable();




            sdsReconData.SelectCommand = "SELECT DocDate, DocNumber, Receiver, CashAdvanceAmount, 0 AS LiquidatedCA, 0 CashReimbursement "
                                           + " INTO #FINAL FROM Accounting.CashAdvance WHERE ISNULL(SubmittedBy,'')!='' AND ISNULL(FundSource,'')='" + glFundCode.Text + "' "
                                           + "  UNION ALL "
                                           + " SELECT DocDate, DocNumber, Receiver,0 AS CashAdvanceAmount, ExpendAmount AS LiquidatedCA, 0 CashReimbursement "
                                           + " FROM Accounting.LiquidationOfCashAdvance WHERE ISNULL(ReplenishNumber,'')='' AND ISNULL(FundSource,'')='" + glFundCode.Text + "' "
                                           + " AND ISNULL(SubmittedBy,'')!='' "
                                           + " UNION ALL "
                                           + "  SELECT DocDate, DocNumber, Receiver,0 AS CashAdvanceAmount, 0 AS LiquidatedCA, ExpendAmount CashReimbursement "
                                           + "  FROM Accounting.CashReimbursement WHERE ISNULL(ReplenishNumber,'')=''  "
                                           + " AND ISNULL(SubmittedBy,'')!='' AND ISNULL(FundSource,'')='" + glFundCode.Text + "' "
                                           + " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber,'' AS DocNumber, CONVERT(date,DocDate,101) AS TransDate, DocNumber AS TransDoc, Receiver"
                                           + " , CashAdvanceAmount, LiquidatedCA, CashReimbursement FROM #FINAL";


            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["purchaserequestdetail"] = sdsPicklistDetail.FilterExpression;
            //sdsPicklistDetail.DataBind();
            gvReconciling.DataSource = sdsReconData;
            gvReconciling.DataBind();
            Session["ReconData"] = "1";

            foreach (GridViewColumn col in gvReconciling.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvReconciling.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvReconciling.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};
            dt.Columns["LineNumber"]};
            return dt;
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

            if (Session["Datatable"] == "1" && check == true)
            {

                e.Handled = true;
                DataTable source = GetDenomination();


                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = 
                    { 
                        //values.NewValues["DocNumber"], 
                        values.NewValues["LineNumber"] 
                    };
                    DataRow row = source.Rows.Find(keys);
                    //row["PRNumber"] = values.NewValues["PRNumber"];
                    row["Denomination"] = values.NewValues["Denomination"];
                    row["Qty"] = values.NewValues["Qty"];
                    row["Amount"] = values.NewValues["Amount"];
                }

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    //_EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                    _EntityDetail.Denomination = Convert.ToDecimal(dtRow["Denomination"]);
                    _EntityDetail.Qty = Convert.ToInt32(dtRow["Qty"]);
                    _EntityDetail.Amount = Convert.ToDecimal(dtRow["Amount"]);

                    _EntityDetail.AddCashReconDetail(_EntityDetail);
                }
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
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            //CustomerCodelookup.ConnectionString = Session["ConnString"].ToString();
            //CostCenterlookup.ConnectionString = Session["ConnString"].ToString();
            //SoldToLookup.ConnectionString = Session["ConnString"].ToString();
            //PropertyNumberLookup.ConnectionString = Session["ConnString"].ToString();
            //AssetAcquisitionLookup.ConnectionString = Session["ConnString"].ToString();
        }

        protected void glReconciling_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {

        }

    }
}