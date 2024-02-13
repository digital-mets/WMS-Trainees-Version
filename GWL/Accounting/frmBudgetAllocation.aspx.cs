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
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using System.Globalization;

namespace GWL
{
    public partial class frmBudgetAllocation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.BudgetAllocation _Entity = new BudgetAllocation();//Calls entity Customer
        Entity.BudgetAllocation.BudgetAllocationDetail _EntityDetail = new BudgetAllocation.BudgetAllocationDetail();//Call entity POdetails
 
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());


            //sql.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;
            //sql.SelectParameters["Year"].DefaultValue = glYear.Value == null ? "" : glYear.Text;

            ////sql2.SelectParameters["version"].DefaultValue = txtver.Value == null ? "" : (txtver.Text == "" ? txtver.Value.ToString() : txtver.Text);

            //sql2.SelectParameters["year"].DefaultValue = year.Text;

            //forecastsumm.SelectParameters[0].DefaultValue = year.Text;
            //forecastsumm2.SelectParameters[0].DefaultValue = year.Text;
            //string referer;
            //try //Validation to restrict user to browse/type directly to browser's address
            //{
            //    referer = Request.ServerVariables["http_referer"];
            //}
            //catch
            //{
            //    referer = "";
            //}

            //if (referer == null)
            //{
            //    Response.Redirect("~/error.aspx");
            //}
            
            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        Generatebtn.ClientVisible = true;
                       // glcheck.ClientVisible = false;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        glYear.ClientEnabled = false;
                        txtBudget.ClientEnabled = false;
                        Generatebtn.ClientVisible = false;

                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        Generatebtn.ClientVisible = false;
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        Generatebtn.ClientVisible = false;
                        glcheck.ClientVisible = false;
                        break;
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session

                txtDoc.Value = Request.QueryString["docnumber"].ToString();
                if (Request.QueryString["entry"].ToString() == "N")
                {


                }
                else
                {
                    //gvBiz.ReadOnly = true;


                    _Entity.getdata(txtDoc.Text, Session["ConnString"].ToString());//ADD CONN
                    //_Entity.getdata(txtDoc.Text);
                    glYear.Value = _Entity.Year;
                    txtBudget.Value = _Entity.ReferenceBudget;
                    txtStatus.Value = _Entity.BudgetStatus;
                    chkIsInactive.Value = _Entity.IsInactive;
                   
                    txtAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? "" : Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                    txtLastEditedDate.Text = String.IsNullOrEmpty(_Entity.LastEditedDate) ? "" : Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                    txtApprovedDate.Text = String.IsNullOrEmpty(_Entity.ApprovedDate) ? "" : Convert.ToDateTime(_Entity.ApprovedDate).ToShortDateString();
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtApprovedBy.Text = _Entity.ApprovedBy;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;


                }
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    ASPxPivotGrid1.DataSourceID = "sdsDetail";
                //}
                //else if (Request.QueryString["iswithdetail"].ToString() == "true" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    ASPxPivotGrid1.DataSourceID = "odsDetail";
                //}
                //if (Request.QueryString["entry"].ToString() == "V")
                //{
                //    ASPxPivotGrid1.DataSourceID = "odsDetail";
                //}
            }
            string doc = Request.QueryString["docnumber"].ToString();
            //if (!string.IsNullOrEmpty(txtBudget.Text))
            //{
            //    doc = txtBudget.Text;
            //}
            //else
            //{
            //    doc = Request.QueryString["docnumber"].ToString();
            //}
            sdsBudDetails.SelectCommand = "SELECT * from accounting.BudgetAllocationDetail where docnumber = '" + doc + "'";
            sdsBudDetails.DataBind();
            ASPxPivotGrid1.DataBind();
        }
        #endregion
        protected void pivotGrid_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
        {
            ASPxPivotGrid1.DataSourceID = sdsBudDetails.ID;
            //ASPxPivotGrid1.DataBind();
            ASPxPivotGrid pivot = (ASPxPivotGrid)sender;
            string[] args = e.Parameters.Split('|');
            int colIndex = int.Parse(args[1]);
            int rowIndex = int.Parse(args[2]);
            ChangeCellValue(
                    pivot.CreateDrillDownDataSource(colIndex, rowIndex),
                    (decimal)pivot.GetCellValue(colIndex, rowIndex),
                    decimal.Parse(args[3], NumberStyles.Currency)
                );
        }

        private void ChangeCellValue(PivotDrillDownDataSource source, decimal oldValue, decimal newValue)
        {
            
            decimal diff = newValue - oldValue;
            decimal fact = diff == newValue ? diff / source.RowCount : diff / oldValue;
            decimal statnewVal = newValue;
            for (int i = 0; i < source.RowCount; i++)
            {
                sql.UpdateParameters.Clear();
                decimal value = (decimal)source.GetValue(i, "Amount");
                newValue = (value == 0 ? 1 : value) * (1m + fact);
                if (value == 0)
                {
                    newValue = statnewVal;
                }

                sql.UpdateParameters.Add("Amount", DbType.Decimal,
                    newValue.ToString());
                sql.UpdateParameters.Add("AccountCode", DbType.String,
                    source.GetValue(i, "AccountCode").ToString());
                sql.UpdateParameters.Add("SubsiCode", DbType.String,
                    source.GetValue(i, "SubsiCode").ToString());
                sql.UpdateParameters.Add("CostCenterCode", DbType.String,
                    source.GetValue(i, "CostCenterCode").ToString());
                sql.UpdateParameters.Add("Month", DbType.Int32,
                    source.GetValue(i, "Month").ToString());
                sql.UpdateParameters.Add("DocNumber", DbType.String,
                    txtDoc.Value.ToString());
                //if (Session["sql"].ToString() == "sql2")
                //{
                //    sql.UpdateCommand = "UPDATE sales.Forecast_Temp_Save SET forecast = @Forecast WHERE Year = @Year and Month = @Month and Customer = @Customer and Col1 = @Col1 and Version = @Version";
                //}
                //else
                //{
                sql.UpdateCommand = "UPDATE Accounting.BudgetAllocationDetail SET Amount = @Amount WHERE DocNumber = @DocNumber and AccountCode = @AccountCode and SubsiCode = @SubsiCode and CostCenterCode = @CostCenterCode and Month = @Month";

                //}
                sql.Update();
                source.SetValue(
                        i, "Amount",
                        newValue
                    );
            }
            ASPxPivotGrid1.DataBind();
        }
        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            //gparam._Factor = 1;
            // gparam._Action = "Validate";
            //here
            string strresult = GAccounting.BudgetAllocation_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

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
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
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
            //    //grid.SettingsBehavior.AllowGroup = false;
            //    //grid.SettingsBehavior.AllowSort = false;
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
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "SubsiCode";
                SubsiCode.FilterExpression = Session["FilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string code = e.Parameters.Split('|')[1];//Set Item Code
            var gridlookup = sender as ASPxGridView;
            string codes = "";

            //if (e.Parameters.Contains("SubsiGetCode"))
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    ASPxGridLookup lookup = (ASPxGridLookup)ASPxPivotGrid1.FindEditRowCellTemplateControl((GridViewDataColumn)ASPxPivotGrid1.Columns[4], "glSubsiCode");
            //    var selectedValues = code;
            //    CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { code });
            //    SubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //    Session["FilterExpression"] = SubsiCode.FilterExpression;
            //    grid.DataSourceID = "SubsiCode";
            //    grid.DataBind();

            //    for (int i = 0; i < grid.VisibleRowCount; i++)
            //    {
            //        var col = "SubsiCode";
            //        if (grid.GetRowValues(i, col) != null)
            //            if (grid.GetRowValues(i, col).ToString() == code)
            //            {
            //                grid.Selection.SelectRow(i);
            //                string key = grid.GetRowValues(i, col).ToString();
            //                grid.MakeRowVisible(key);
            //                break;
            //            }
            //    }
            //}
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDoc.Text;
            _Entity.Year = String.IsNullOrEmpty(glYear.Text) ? null : glYear.Value.ToString();
            _Entity.ReferenceBudget = String.IsNullOrEmpty(txtBudget.Text) ? null : txtBudget.Value.ToString();
            _Entity.BudgetStatus = txtStatus.Text;
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Checked);

           
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.ApprovedBy = txtApprovedBy.Text;
            _Entity.ApprovedDate = txtApprovedDate.Text;

            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();


            switch (e.Parameter)
            {
                case "Add":
                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();
                       
                        _Entity.UpdateData(_Entity);
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

                   

                   // gv1.UpdateEdit();

                    

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of inserting for header

                           // gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                           // gv1.UpdateEdit();//2nd initiation to insert grid
                    
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
                    //Generate();
                    cp.JSProperties["cp_generated"] = true;
                    Gears.RetriveData2("exec sp_GenerateBudAlloc '" + txtDoc.Text + "','" + glYear.Text + "','" + txtBudget.Text + "'", Session["ConnString"].ToString());
                    ASPxPivotGrid1.DataBind();
                    break;
                //case "Alloc":
                //    GetSelectedVal();
                //    cp.JSProperties["cp_generated"] = true;
                //    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ServiceType = "";
            //string UnitOfMeasure = "";


            //foreach (GridViewColumn column in gv1.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber"
            //        && dataColumn.FieldName != "Vatable" && dataColumn.FieldName != "MinHandlingIn" && dataColumn.FieldName != "MinHandlingOut"
            //        && dataColumn.FieldName != "MinStorage" && dataColumn.FieldName != "HandlingInRate" && dataColumn.FieldName != "HandlingOutRate"
            //        && dataColumn.FieldName != "Remarks" && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2"
            //        && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5"
            //        && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
            //        && dataColumn.FieldName != "Field9")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //    //Checking for non existing Codes..
            //    ServiceType = String.IsNullOrEmpty(e.NewValues["ServiceType"].ToString()) ? "" : e.NewValues["ServiceType"].ToString();
            //    UnitOfMeasure = String.IsNullOrEmpty(e.NewValues["UnitOfMeasure"].ToString()) ? "" : e.NewValues["UnitOfMeasure"].ToString();

            //    DataTable ST = Gears.RetriveData2("SELECT ServiceType FROM Masterfile.WMSServiceType WHERE ServiceType = '" + ServiceType + "'");
            //    if (ST.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ServiceType"], "ServiceType doesn't exist!");
            //    }

            //    DataTable UOM = Gears.RetriveData2("SELECT UnitOfMeasure FROM Masterfile.WMSUnitOfMeasure WHERE UnitOfMeasure = '" + UnitOfMeasure + "'");
            //    if (UOM.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["UnitOfMeasure"], "Unit of Measure doesn't exist!");
            //    }
            //}

            //if (e.Errors.Count > 0)
            //{
            //    error = true; //bool to cancel adding/updating if true
            //}
        }
            void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
            protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
            {
                //if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
                //{
                //    e.Handled = true;
                //    //e.DeleteValues.Clear();
                //    //e.InsertValues.Clear();
                //    //e.UpdateValues.Clear();
                //}
                //if (Session["Datatable"] == "1" && check == true)
                //{
                //    e.Handled = true;
                //    DataTable source = GetSelectedVal();

                //    // Removing all deleted rows from the data source(Excel file)
                //    foreach (ASPxDataDeleteValues values in e.DeleteValues)
                //    {
                //        try
                //        {
                //            object[] keys = { values.Keys[0] };
                //            source.Rows.Remove(source.Rows.Find(keys));
                //        }
                //        catch (Exception)
                //        {
                //            continue;
                //        }
                //    }

                //    // Updating required rows
                //    foreach (ASPxDataUpdateValues values in e.UpdateValues)
                //    {
                //        object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"] };
                //        DataRow row = source.Rows.Find(keys);
                //        row["OldUnitCost"] = values.NewValues["OldUnitCost"];
                //        row["NewUnitCost"] = values.NewValues["NewUnitCost"];
                //        row["IsVAT"] = values.NewValues["IsVAT"];
                //        row["VATCode"] = values.NewValues["VATCode"];
                //        row["OrderQty"] = values.NewValues["OrderQty"];
                //        row["Unit"] = values.NewValues["Unit"];
                //        row["UnitCost"] = values.NewValues["UnitCost"];
                //        row["BaseQty"] = values.NewValues["BaseQty"];
                //        row["AverageCost"] = values.NewValues["AverageCost"];
                //        row["StatusCode"] = values.NewValues["StatusCode"];
                //        row["Field1"] = values.NewValues["Field1"];
                //        row["Field2"] = values.NewValues["Field2"];
                //        row["Field3"] = values.NewValues["Field3"];
                //        row["Field4"] = values.NewValues["Field4"];
                //        row["Field5"] = values.NewValues["Field5"];
                //        row["Field6"] = values.NewValues["Field6"];
                //        row["Field7"] = values.NewValues["Field7"];
                //        row["Field8"] = values.NewValues["Field8"];
                //        row["Field9"] = values.NewValues["Field9"];
                //    }

                //    foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                //    {
                //        _EntityDetail.AccountCode = dtRow["AccountCode"].ToString();
                //        _EntityDetail.SubsiCode = dtRow["SubsiCode"].ToString();
                //        _EntityDetail.CostCenterCode = dtRow["CostCenterCode"].ToString();
                //        _EntityDetail.Month = dtRow["Month"].ToString();
                //        _EntityDetail.Amount = Convert.ToDecimal(dtRow["Amount"].ToString());


                //        _EntityDetail.AddBudgetAllocationDetail(_EntityDetail);

                //        //_EntityDetail.DeleteOutboundDetail(_EntityDetail);
                //    }
                //}
            }
        #endregion
        //private DataTable GetSelectedVal()
        //{
            //DataTable dt = new DataTable();
            //string[] selectedValues = txtBudget.Text.Split(';');
            //CriteriaOperator selectionCriteria = new InOperator(txtBudget.KeyFieldName, selectedValues);
            //sdsBudDetails.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["RRses"] = sdsBudDetails.FilterExpression;
            //gv1.DataSourceID = sdsBudDetails.ID;
            //gv1.DataBind();
            //Session["Datatable"] = "1";

            //foreach (GridViewColumn col in gv1.VisibleColumns)
            //{
            //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    dt.Columns.Add(dataColumn.FieldName);
            //}
            //for (int i = 0; i < gv1.VisibleRowCount; i++)
            //{
            //    DataRow row = dt.Rows.Add();
            //    foreach (DataColumn col in dt.Columns)
            //        row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            //}

            //dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};


            //return dt;
        //}

        protected void gv1_Init(object sender, EventArgs e)
        {

            //if (!IsPostBack)
            //{
            //    Session["RRses"] = null;
            //}

            //if (Session["RRses"] != null)
            //{
            //    gv1.DataSource = sdsBudDetails;
            //    sdsBudDetails.FilterExpression = Session["RRses"].ToString();
            //    //gridview.DataSourceID = null;
            //}
        }
        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }
        //private DataTable Generate()
        //{
        //    DataTable alloc = Gears.RetriveData2("EXEC sp_GenerateBudAlloc '" + txtDoc.Text + "','" + glYear.Text + "'");

        //    //DataTable gen = new DataTable();
        //    gv1.DataSource = billing;
        //    if (gv1.DataSourceID != "")
        //    {
        //        gv1.DataSourceID = null;
        //    }
        //    gv1.DataBind();

        //    return billing;
        //}
        protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glBizPartnerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtPlant_TextChanged(object sender, EventArgs e)
        {

        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            CostCenterCode.ConnectionString = Session["ConnString"].ToString();
            AccountCode.ConnectionString = Session["ConnString"].ToString();
            SubsiCode.ConnectionString = Session["ConnString"].ToString();
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            BizPartner.ConnectionString = Session["ConnString"].ToString();
            BizAccount.ConnectionString = Session["ConnString"].ToString();
            Currency.ConnectionString = Session["ConnString"].ToString();
            ItemCategory.ConnectionString = Session["ConnString"].ToString();
            Budget.ConnectionString = Session["ConnString"].ToString();
            sdsyear.ConnectionString = Session["ConnString"].ToString();




        }
    }
}