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
    public partial class frmAddPartsToProperty : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state
        int x = 0;
        int parentLife = 0;

        Entity.AddPartsToProperty _Entity = new AddPartsToProperty();//Calls entity ICN
        Entity.AddPartsToProperty.AddPartsToPropertyDetail _EntityDetail = new AddPartsToProperty.AddPartsToPropertyDetail();//Call entity POdetails

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

            switch (Request.QueryString["entry"].ToString())
            {
                case "N":
                    updateBtn.Text = "Update";
                    break;
                case "E":
                    updateBtn.Text = "Assign";
                    gv1.KeyFieldName = "PropertyNumber";
                    edit = true;
                    break;
                case "V":
                    view = true;//sets view mode for entry
                    txtPropertyNumber.Enabled = true;
                    txtPropertyNumber.ReadOnly = false;
                    updateBtn.Text = "Close";
                    glcheck.ClientVisible = false;
                    break;
                case "D":
                    view = true;
                    updateBtn.Text = "Delete";
                    break;
            }

            if (!IsPostBack)
            {
              
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtPropertyNumber.ReadOnly = true;
                txtPropertyNumber.Value = Request.QueryString["docnumber"].ToString();
                
                if (Request.QueryString["entry"].ToString() == "N")
                {

                    gv1.DataSourceID = "sdsDetail";
                    //popup.ShowOnPageLoad = false;
                    //deExpDelDate.Text = DateTime.Now.ToShortDateString();
                }
                else
                {

                    // 
                    if(gv1.VisibleRowCount > 0)
                        gv1.DataSourceID = "odsDetail";
                    else
                        gv1.DataSourceID = "sdsDetail";


                    _Entity.getdata(txtPropertyNumber.Text, Session["ConnString"].ToString());//ADD CONN

                    txtItemCode.Value = _Entity.ItemCode;
                    memodescription.Value = _Entity.FullDesc;
                    txtColorCode.Value = _Entity.ColorCode;
                    txtClassCode.Value =  _Entity.ClassCode;
                    txtSizeCode.Value = _Entity.SizeCode;
                    txtLife.Value = _Entity.Life;
                    //dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    //txtTotalAmountSold.Value = _Entity.TotalAmountSold;
                    //txtTotalGrossVatableAmount.Value = _Entity.GrossVATAmount;
                    //txtTotalNonGrossVatableAmount.Value = _Entity.GrossNonVATAmount;

                    //cbDisposalType.Value = _Entity.DisposalType;
                    //glSoldTo.Value = _Entity.SoldTo;
                    //txtRemarks.Value = _Entity.Remarks;

                    //txtTotalCostAsset.Value = _Entity.TotalAssetCost;
                    //txtTotalAccumulatedDepreciationRecord.Value = _Entity.TotalAccumulatedDepreciation;
                    //txtNetBookValue.Value = _Entity.NetBookValue;
                    //txtTotalGainLoss.Value = _Entity.TotalGainLoss;



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


             



                }
                if (Request.QueryString["IsWithDetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                {
                    gv1.DataSourceID = "sdsDetail";

                    sdsDetail.SelectCommand = "SELECT * FROM Accounting.AssetDisposaldetail WHERE DocNumber is null";
                    sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                }
            }
        }
        #endregion


        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "ACTADI";

            //string strresult = GearsAccounting.GAccounting.AssetDisposal_Validate(gparam);

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
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
            //}
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
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New)
                    {
                        e.Visible = true;
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
                AssetAcquisitionLookup.SelectCommand = "SELECT PropertyNumber,ItemCode,Description,ColorCode,ClassCode,SizeCode,ISNULL(Life,0) AS Life FROM Accounting.AssetInv WHERE Status='F' AND (ISNULL(ParentProperty,'')='') AND PropertyNumber NOT IN (Select ISNULL(ParentProperty,'') AS ParentProperty FROM Accounting.AssetInv GROUP BY ParentProperty) AND PropertyNumber != '" + txtPropertyNumber.Text +"'"; 
                
                gridLookup.GridView.DataSourceID = "AssetAcquisitionLookup";
                AssetAcquisitionLookup.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
            else
            {
                //AssetAcquisitionLookup.SelectCommand = "SELECT PropertyNumber,ItemCode,Description,ColorCode,ClassCode,SizeCode FROM Accounting.[AssetInv] WHERE Status='F' AND ParentProperty IS NULL"; 
                AssetAcquisitionLookup.SelectCommand = "SELECT PropertyNumber,ItemCode,Description,ColorCode,ClassCode,SizeCode,ISNULL(Life,0) AS Life FROM Accounting.AssetInv WHERE Status='F' AND (ISNULL(ParentProperty,'')='') AND PropertyNumber NOT IN (Select ISNULL(ParentProperty,'') AS ParentProperty FROM Accounting.AssetInv GROUP BY ParentProperty) AND PropertyNumber != '" + txtPropertyNumber.Text + "'"; 
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
                DataTable countcolor = Gears.RetriveData2("Select ItemCode,Description,ColorCode,ClassCode,SizeCode,ISNULL(Life,0) AS Life from Accounting.AssetInv where propertynumber = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN
                
                    foreach (DataRow dt in countcolor.Rows)
                    {
                        codes = dt["ItemCode"].ToString() + ";";
                        codes += dt["Description"].ToString() + ";";
                        codes += dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["Life"].ToString() + ";";
                    }



                    
                    propertynumberlookup.JSProperties["cp_codes"] = codes;
            }


            else if (e.Parameters.Contains("checkdoc"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                string[] transdoc = propertynumber.Split(';');
                var selectedValues = transdoc;
                CriteriaOperator selectionCriteria = new InOperator("PropertyNumber", transdoc);
                CriteriaOperator not = new NotOperator(selectionCriteria);
                AssetAcquisitionLookup.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not)).ToString();
                Session["FilterExpression"] = AssetAcquisitionLookup.FilterExpression;
                grid.DataSourceID = "AssetAcquisitionLookup";
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

            _Entity.PropertyNumber     = txtPropertyNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            //_Entity.DocDate       = dtDocDate.Text;

            //_Entity.TotalAmountSold = Convert.ToDecimal(Convert.IsDBNull(txtTotalAmountSold.Value) ? 0 : txtTotalAmountSold.Value);
            //_Entity.GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalGrossVatableAmount.Value) ? 0 : txtTotalGrossVatableAmount.Value);
            //_Entity.GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalNonGrossVatableAmount.Value) ? 0 : txtTotalNonGrossVatableAmount.Value);


            //_Entity.DisposalType = cbDisposalType.Text;
            //_Entity.SoldTo = glSoldTo.Text;
            //_Entity.Remarks       = txtRemarks.Text;

            //_Entity.TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(txtTotalCostAsset.Value) ? 0 : txtTotalCostAsset.Value);
            //_Entity.TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(txtTotalAccumulatedDepreciationRecord.Value) ? 0 : txtTotalAccumulatedDepreciationRecord.Value);
            //_Entity.NetBookValue = Convert.ToDecimal(Convert.IsDBNull(txtNetBookValue.Value) ? 0 : txtNetBookValue.Value);
            //_Entity.TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(txtTotalGainLoss.Value) ? 0 : txtTotalGainLoss.Value);
            ////_Entity.Status        = txtStatus.Text;

            //_Entity.IsPrinted     = Convert.ToBoolean(chkIsPrinted.Value);

            //_Entity.TargetDate = dtTargetDate.Text;




            switch (e.Parameter)
            {
                case "Assign":
                    _Entity.getdata(txtPropertyNumber.Text, Session["ConnString"].ToString());//ADD CONN
                     parentLife = Convert.ToInt32(_Entity.Life);
                     gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                       // _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["PropertyNumber"].DefaultValue = txtPropertyNumber.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Assigned!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Child life is greater than Parent life!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                       // _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtPropertyNumber.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "";
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



            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string PropertyNumber = "";
            int childLife = 0;
            
            //Getting Life of Parent Property
            //_Entity.getdata(txtPropertyNumber.Text);
            // Convert.ToInt32(_Entity.Life);
            // End
            
            

            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            foreach (GridViewColumn column in gv1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
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

                PropertyNumber = String.IsNullOrEmpty(Convert.ToString(e.NewValues["PropertyNumber"])) ? "" : e.NewValues["PropertyNumber"].ToString();
                childLife = Convert.ToInt32(e.NewValues["Life"].ToString());
                //    ColorCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ColorCode"])) ? "" : e.NewValues["ColorCode"].ToString(); 
            //    ClassCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ClassCode"])) ? "" : e.NewValues["ClassCode"].ToString(); 
            //    SizeCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["SizeCode"])) ? "" : e.NewValues["SizeCode"].ToString();
                //if (dataColumn.FieldName == "PropertyNumber")
                //{
                    //DataTable Life = Gears.RetriveData2("SELECT Life FROM Accounting.AssetInv WHERE PropertyNumber = '" + PropertyNumber + "'");
                    //foreach (DataRow dt in Life.Rows)
                    //{
                    //    childLife = Convert.ToInt32(dt[0].ToString());
                    //}
                    if (childLife > parentLife)
                    {
                        AddError(e.Errors, gv1.Columns["PropertyNumber"], "life is greater than Parent life!");
                        //e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                    }
                //}
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
            }
        //
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
        }

    }
}