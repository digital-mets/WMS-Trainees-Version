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
    public partial class frmAssetDepreciationSetup : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state
        Boolean posted = false;

        Entity.AssetDepreciationSetup _Entity = new AssetDepreciationSetup();//Calls entity ICN
        Entity.AssetDepreciationSetup.AssetDepreciationSetupDetail _EntityDetail = new AssetDepreciationSetup.AssetDepreciationSetupDetail();//Call entity POdetails

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

            gv1.KeyFieldName = "PropertyNumber;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

            AccumulatedGLSubsiCode.SelectCommand = "select B.SubsiCode, B.Description from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glAccumulatedAccountCode.Text + "'";
            DepreciatedGLSubsiCode.SelectCommand = "select B.SubsiCode, B.Description from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glDepreciationAccountCode.Text + "'";

            if (!IsPostBack)
            {

                txtPropertyNumber.Value = Request.QueryString["docnumber"].ToString();//sets docnumber from session
                //  txtDocNumber.ReadOnly = true;
                //  txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

                //if (Request.QueryString["entry"].ToString() == "N")
                //{

                //    gv1.DataSourceID = "sdsDetail";
                //    //popup.ShowOnPageLoad = false;
                //    //deExpDelDate.Text = DateTime.Now.ToShortDateString();
                //}
                //else
                //{
                gv1.DataSourceID = "odsDetail";
                _Entity.getdata(txtPropertyNumber.Text, Session["ConnString"].ToString());//ADD CONN

                dtDateAcquired.Value = String.IsNullOrEmpty(_Entity.DateAcquired) ? DateTime.Now : Convert.ToDateTime(_Entity.DateAcquired);
                dtStartOfDepreciation.Value = Convert.ToDateTime(_Entity.StartOfDepreciation);

                txtTransType.Value = _Entity.TransType;
                txtDocNumberRR.Value = _Entity.DocNumber;
                txtItemCode.Value = _Entity.ItemCode;
                txtColorCode.Value = _Entity.ColorCode;
                txtClassCode.Value = _Entity.ClassCode;
                txtSizeCode.Value = _Entity.SizeCode;
                txtDescription.Value = _Entity.FullDesc;

                txtParentProperty.Value = _Entity.ParentProperty;

                txtAssignedTo.Value = _Entity.AssignedTo;
                txtLocation.Value = _Entity.Location;
                txtDepartment.Value = _Entity.Department;
                txtWarehouse.Value = _Entity.WarehouseCode;
                spinLifeInMonths.Value = Convert.ToDecimal(_Entity.Life);

                txtSalvageValue.Value = _Entity.SalvageValue;
                cbNewDepreciationMethod.Value = _Entity.DepreciationMethod;

                spinQty.Value = _Entity.Qty;
                spinUnitCost.Value = _Entity.UnitCost;
                spinAmountSold.Value = _Entity.AmountSold;

                txtAcquisitionCost.Value = _Entity.AcquisitionCost;
                txtBookValue.Value = _Entity.BookValue;
                txtMonthlyDepreciation.Value = _Entity.MonthlyDepreciation;


                glDepreciationAccountCode.Value = _Entity.DepreciationAccountCode;
                glDepreciationProfitCenter.Value = _Entity.DepreciationProfitCenter;
                glDepreciationCostCenter.Value = _Entity.DepreciationCostCenter;

                glAccumulatedAccountCode.Value = _Entity.AccumulatedAccountCode;
                glAccumulatedProfitCenter.Value = _Entity.AccumulatedProfitCenter;
                glAccumulatedCostCenter.Value = _Entity.AccumulatedCostCenter;

                glGainLossAccount.Value = _Entity.GainLossAccount;

                spinAccumulatedDepreciation.Value = _Entity.AccumulatedDepreciation;

                txtUnit.Value = _Entity.Unit;

                //GC 09/02/2016 Added code
                chkParentSetup.Value = _Entity.IsParentSetup;
                //end


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


                AccumulatedGLSubsiCode.SelectCommand = "select B.SubsiCode, B.Description from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glAccumulatedAccountCode.Text + "'";
                glAccumulatedSubsiCode.DataBind();
                glAccumulatedSubsiCode.Value = _Entity.AccumulatedSubsiCode;
                DepreciatedGLSubsiCode.SelectCommand = "select B.SubsiCode, B.Description from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glDepreciationAccountCode.Text + "'";
                glDepreciationSubsiCode.DataBind();
                glDepreciationSubsiCode.Value = _Entity.DepreciationSubsiCode;

                SetStatus();



                try
                {
                    dtDateRetired.Value = Convert.ToDateTime(_Entity.DateRetired);
                }
                catch (Exception x)
                {
                    dtDateRetired.Value = "";
                }
                //}

                DataTable dtbldetail = Gears.RetriveData2("SELECT PropertyNumber FROM Accounting.AssetInvDetail Where PropertyNumber = '" + txtPropertyNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN

                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                //if (Request.QueryString["IsWithDetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;


                //}
            }

            DataTable getCount = Gears.RetriveData2("SELECT DISTINCT A.PropertyNumber, B.Status FROM Accounting.AssetInvDetail A INNER JOIN Accounting.AssetInv B "
                    + " ON A.PropertyNumber = B.PropertyNumber WHERE A.PropertyNumber = '" + txtPropertyNumber.Text + "' AND ISNULL(PostedDate,'') != '' "
                    + " AND ISNULL(B.Status,'F') != 'F'", Session["ConnString"].ToString());
            if (getCount.Rows.Count > 0)
            {
                posted = true;
            }

            switch (Request.QueryString["entry"].ToString())
            {
                case "N":
                    updateBtn.Text = "Update";
                    
                    if (posted == true)
                    {
                        view = true;
                    }
                    break;
                case "E":
                    updateBtn.Text = "Update";
                    GetLife();
                    edit = true;

                    if (posted == true)
                    {
                        view = true;
                    }
                    break;
                case "V":
                    view = true;//sets view mode for entry
                    glcheck.ClientVisible = false;
                    //  txtDocNumber.Enabled = true;
                    //  txtDocNumber.ReadOnly = false;
                    updateBtn.Text = "Close";
                    break;
                case "D":
                    view = true;
                    updateBtn.Text = "Delete";
                    break;
            }


        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.PropertyNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTADS";

            string strresult = GearsAccounting.GAccounting.AssetDepreciation_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }

        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientEnabled = !view;
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
            if (edit != false)
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
            combobox.ClientEnabled = !view;
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
            spinedit.HorizontalAlign = HorizontalAlign.Right;
            spinedit.AllowMouseWheel = false;
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
            //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);

            //GLSubsiCodeLookup.SelectCommand = "select A.AccountCode,B.SubsiCode,B.Description from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glAccumulatedAccountCode.Text + "'";
            //gridLookup.GridView.DataSourceID = "select A.AccountCode,B.SubsiCode,B.Description from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glAccumulatedAccountCode.Text + "'"; 
            //if (Session["FilterExpression"] != null)
            //{
            //gridLookup.GridView.DataSourceID = "AssetAcquisitionLookup";
            //AssetAcquisitionLookup.FilterExpression = Session["FilterExpression"].ToString();
            //Session["FilterExpression"] = null;
            //}
            //else
            //{
            //    gridLookup.GridView.DataSourceID = "AssetAcquisitionLookup";
            //}
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
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,Qty,UnitCost,AccumulatedDepreciation from Accounting.AssetAcquisition where propertynumber = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["Qty"].ToString() + ";";
                    codes += dt["UnitCost"].ToString() + ";";
                    codes += dt["AccumulatedDepreciation"].ToString() + ";";

                }



                propertynumberlookup.JSProperties["cp_codes"] = codes;
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
            _Entity.PropertyNumber = txtPropertyNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.TransType = txtTransType.Text;
            _Entity.DocNumber = txtDocNumberRR.Text;
            _Entity.ItemCode = txtItemCode.Text;
            _Entity.ColorCode = txtColorCode.Text;
            _Entity.ClassCode = txtClassCode.Text;
            _Entity.SizeCode = txtSizeCode.Text;
            _Entity.FullDesc = txtDescription.Text;
            _Entity.Qty = Convert.ToDecimal(Convert.IsDBNull(spinQty.Value) ? 0 : spinQty.Value);
            _Entity.UnitCost = Convert.ToDecimal(Convert.IsDBNull(spinUnitCost.Value) ? 0 : spinUnitCost.Value);
            _Entity.DepreciationMethod = cbNewDepreciationMethod.Text;


            _Entity.DateAcquired = dtDateAcquired.Text;
            //_Entity.DateRetired = dtDateRetired.Text;
            _Entity.StartOfDepreciation = dtStartOfDepreciation.Text;


            _Entity.AssignedTo = txtAssignedTo.Text;
            _Entity.Location = txtLocation.Text;
            _Entity.Department = txtDepartment.Text;
            _Entity.WarehouseCode = txtWarehouse.Text;
            _Entity.Life = Convert.ToDecimal(Convert.IsDBNull(spinLifeInMonths.Value) ? 0 : spinLifeInMonths.Value);

            _Entity.AcquisitionCost = Convert.ToDecimal(Convert.IsDBNull(txtAcquisitionCost.Value) ? 0 : txtAcquisitionCost.Value);
            _Entity.AmountSold = Convert.ToDecimal(Convert.IsDBNull(spinAmountSold.Value) ? 0 : spinAmountSold.Value);
            _Entity.BookValue = Convert.ToDecimal(Convert.IsDBNull(txtBookValue.Value) ? 0 : txtBookValue.Value);
            _Entity.SalvageValue = Convert.ToDecimal(Convert.IsDBNull(txtSalvageValue.Value) ? 0 : txtSalvageValue.Value);
            _Entity.MonthlyDepreciation = Convert.ToDecimal(Convert.IsDBNull(txtMonthlyDepreciation.Value) ? 0 : txtMonthlyDepreciation.Value);
            _Entity.AccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(spinAccumulatedDepreciation.Value) ? 0 : spinAccumulatedDepreciation.Value);
            _Entity.IsParentSetup = Convert.ToBoolean(chkParentSetup.Value.ToString());

            _Entity.DepreciationAccountCode = glDepreciationAccountCode.Text;
            _Entity.DepreciationSubsiCode = glDepreciationSubsiCode.Text;
            _Entity.DepreciationProfitCenter = glDepreciationProfitCenter.Text;
            _Entity.DepreciationCostCenter = glDepreciationCostCenter.Text;

            _Entity.AccumulatedAccountCode = glAccumulatedAccountCode.Text;
            _Entity.AccumulatedSubsiCode = glAccumulatedSubsiCode.Text;
            _Entity.AccumulatedProfitCenter = glAccumulatedProfitCenter.Text;
            _Entity.AccumulatedCostCenter = glAccumulatedCostCenter.Text;

            _Entity.GainLossAccount = glGainLossAccount.Text;


            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            SaveStatus();
            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    if (error == false)
                    {
                        check = true;
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);

                        //_Entity.DeleteFirstData(txtPropertyNumber.Text);

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["PropertyNumber"].DefaultValue = txtPropertyNumber.Text;
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
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

                        //_Entity.DeleteFirstData(txtPropertyNumber.Text);
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["PropertyNumber"].DefaultValue = txtPropertyNumber.Text;
                        gv1.UpdateEdit();//2nd Initiation to update grid
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

                case "AccumulatedSetSubsi":
                    //    GLSubsiCodeLookup.SelectCommand = "select B.SubsiCode from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glAccumulatedAccountCode.Text + "'";
                    glAccumulatedSubsiCode.DataBind();
                    break;

                case "DepreciatedSetSubsi":
                    //    GLSubsiCodeLookup.SelectCommand = "select B.SubsiCode from Accounting.ChartOfAccount A INNER JOIN Accounting.GLSubsiCode B ON A.AccountCode = B.AccountCode WHERE A.AccountCode = '" + glAccumulatedAccountCode.Text + "'";
                    glDepreciationSubsiCode.DataBind();
                    break;


            }
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

            // Removing all deleted rows from the data source(Excel file)
            //foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //{
            //    try
            //    {
            //        object[] keys = { values.Keys[0], values.Keys[1] };
            //        gv1.Rows.Remove(gv1.Rows.Find(keys));
            //        gv1.row
            //    }
            //    catch (Exception)
            //    {
            //        continue;
            //    }
            //}
        }
        #endregion

        protected void GetLife()
        {
            DataTable getlife = Gears.RetriveData2("select AssetLife, DepreciationGLCode, GLSubsiCode, AccumulatedGLCode, GainLossAccount, DepreciationMethod from Masterfile.Item I Left Join Masterfile.ItemCategory IC ON I.ItemCategoryCode = IC.ItemCategoryCode WHERE ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());//ADD CONN

            //if (String.IsNullOrEmpty(spinLifeInMonths.Text))
            //{
            //    spinLifeInMonths.Value = getlife.Rows[0]["AssetLife"].ToString();
            //}

            if (String.IsNullOrEmpty(glDepreciationAccountCode.Text))
            {
                glDepreciationAccountCode.Value = getlife.Rows[0]["DepreciationGLCode"].ToString();
            }

            if (String.IsNullOrEmpty(glDepreciationSubsiCode.Text))
            {
                glDepreciationSubsiCode.Value = getlife.Rows[0]["GLSubsiCode"].ToString();
            }

            if (String.IsNullOrEmpty(glAccumulatedAccountCode.Text))
            {
                glAccumulatedAccountCode.Value = getlife.Rows[0]["AccumulatedGLCode"].ToString();
            }

            if (String.IsNullOrEmpty(glAccumulatedSubsiCode.Text))
            {
                glAccumulatedSubsiCode.Value = getlife.Rows[0]["GLSubsiCode"].ToString();
            }

            if (String.IsNullOrEmpty(glGainLossAccount.Text))
            {

                glGainLossAccount.Value = getlife.Rows[0]["GainLossAccount"].ToString();
            }

            if (String.IsNullOrEmpty(cbNewDepreciationMethod.Text))
            {

                cbNewDepreciationMethod.Value = getlife.Rows[0]["DepreciationMethod"].ToString();
            }
        }
        protected void SetStatus()
        {
            if (_Entity.Status == "H")
            {
                txtStatus.Value = "Hold";
                view = true;
            }
            else if (_Entity.Status == "F")
                txtStatus.Value = "For Setup";
            else if (_Entity.Status == "R")
            {
                txtStatus.Value = "Retired";
                view = true;
            }
            else
            {
                txtStatus.Value = "Open";
                view = true;
            }

        }

        protected void SaveStatus()
        {
            if (txtStatus.Text == "Hold")
                _Entity.Status = "H";
            else if (txtStatus.Text == "For Setup")
                _Entity.Status = "F";
            else if (txtStatus.Text == "Retired")
                _Entity.Status = "R";
            else
                _Entity.Status = "O";

        }

        protected void GetDepreciationAccountCode()
        {
            DataTable getlife = Gears.RetriveData2("select DepreciationGLCode from Masterfile.Item I Left Join Masterfile.ItemCategory IC ON I.ItemCategoryCode = IC.ItemCategoryCode WHERE ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());//ADD CONN

            glDepreciationAccountCode.Value = getlife.Rows[0]["DepreciationGLCode"].ToString();

        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

    }
}