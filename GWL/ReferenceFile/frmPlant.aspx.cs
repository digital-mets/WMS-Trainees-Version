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
using GearsWarehouseManagement;


namespace GWL
{
    public partial class frmPlant : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Plant _Entity = new Plant();//Calls entity odsHeader

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


            //Details checking. changes datasourceid of gridview if there's no details for a certain PlantCode; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }


            if (!IsPostBack)
            {

                sdsWarehouse.ConnectionString = Session["ConnString"].ToString();
                
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                }
                

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                }
                else
                {
                    popup.ShowOnPageLoad = false;

                    txtPlantCode.ReadOnly = true;
                    txtPlantCode.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtPlantCode.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                    txtPlantDesc.Text = _Entity.PlantDescription;
                    txtPlantPrefix.Text = _Entity.PlantPrefix;
                    txtRoomCount.Text = _Entity.RoomCount;
                    aglWarehouseCode.Value = _Entity.WarehouseCode;
                    txtDispatch.Text = _Entity.DispatchLocationCode;
                    txtReceiving.Text = _Entity.ReceivingLocationCode;
                    chkIsInActive.Value = Convert.ToBoolean(_Entity.IsInActive.ToString());
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = String.IsNullOrEmpty(_Entity.ActivatedDate.ToString()) ? null : Convert.ToDateTime(_Entity.ActivatedDate.ToString()).ToShortDateString();
                    txtDeactivatedBy.Text = _Entity.DeActivatedBy;
                    txtDeactivatedDate.Text = String.IsNullOrEmpty(_Entity.DeActivatedDate.ToString()) ? null : Convert.ToDateTime(_Entity.DeActivatedDate.ToString()).ToShortDateString();

                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtDeactivatedBy.Text = _Entity.DeActivatedBy;
                    txtDeactivatedDate.Text = _Entity.DeActivatedDate;

                    txtField1.Value = _Entity.Field1;
                    txtHField2.Value = _Entity.Field2;
                    txtHField3.Value = _Entity.Field3;
                    txtHField4.Value = _Entity.Field4;
                    txtHField5.Value = _Entity.Field5;
                    txtHField6.Value = _Entity.Field6;
                    txtHField7.Value = _Entity.Field7;
                    txtHField8.Value = _Entity.Field8;
                    txtHField9.Value = _Entity.Field9;


                }

            }

            
        }
        #endregion

        //#region Validation
        //private void Validate()
        //{
        //    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        //    gparam._DocNo = _Entity.PlantCode;
        //    gparam._UserId = Session["Userid"].ToString();
        //    gparam._TransType = "WMSCON";

        //    string strresult = GWarehouseManagement.Plant_Validate(gparam);

        //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        //}
        //#endregion

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
            var look = sender as ASPxGridLookup;
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                if (look != null)
                {
                    look.Enabled = false;
                }
            }
        }
        protected void CheckBoxLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxCheckBox;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all textbox
        {
            var look = sender as ASPxComboBox;
            if (look != null)
            {
                look.ReadOnly = view;
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
            //ASPxDateEdit date = sender as ASPxDateEdit;
            //date.ReadOnly = view;

            var look = sender as ASPxDateEdit;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            //ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            //spinedit.ReadOnly = view;

            var look = sender as ASPxSpinEdit;
            if (look != null)
            {
                look.ReadOnly = view;
            }
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

        //#region Lookup Settings
        //protected void lookup_Init(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
        //    if (Session["FilterExpression"] != null)
        //    {
        //        gridLookup.GridView.DataSourceID = "sdsServiceType";
        //        sdsServiceType.FilterExpression = Session["FilterExpression"].ToString();
        //        //Session["FilterExpression"] = null;
        //    }
        //}
        //public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string column = e.Parameters.Split('|')[0];//Set column name
        //    if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
        //    string ServiceType = e.Parameters.Split('|')[1];//Set Item Code
        //    string val = e.Parameters.Split('|')[2];//Set column value
        //    if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

        //    ASPxGridView grid = sender as ASPxGridView;
        //    ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "ServiceType");
        //    var selectedValues = ServiceType;
        //    CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { ServiceType });
        //    sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["FilterExpression"] = sdsItemDetail.FilterExpression;
        //    grid.DataSourceID = "sdsServiceType";
        //    grid.DataBind();

        //    for (int i = 0; i < grid.VisibleRowCount; i++)
        //    {
        //        if (grid.GetRowValues(i, column) != null)
        //            if (grid.GetRowValues(i, column).ToString() == val)
        //            {
        //                grid.Selection.SelectRow(i);
        //                string key = grid.GetRowValues(i, column).ToString();
        //                grid.MakeRowVisible(key);
        //                break;
        //            }
        //    }
        //}

        //#endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.PlantCode = txtPlantCode.Text;
            _Entity.PlantDescription = txtPlantDesc.Text;
            _Entity.PlantPrefix = txtPlantPrefix.Text;
            _Entity.RoomCount = txtRoomCount.Text;
            _Entity.WarehouseCode = aglWarehouseCode.Text;
            _Entity.DispatchLocationCode = txtDispatch.Text;
            _Entity.ReceivingLocationCode = txtReceiving.Text;
            _Entity.IsInActive = Convert.ToBoolean(chkIsInActive.Value.ToString());
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeActivatedBy = txtDeactivatedBy.Text;
            _Entity.DeActivatedDate = txtDeactivatedDate.Text;
            
            _Entity.Field1 = txtField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;


            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.LastEditedBy = Session["userid"].ToString();

            switch (e.Parameter)
            {
                case "Add":
                    
                    if (error == false)
                    {
                        check = true;

                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.InsertData(_Entity);//Method of inserting for header
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

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of inserting for header
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
                    Session["Refresh"] = "1";
                break;

                case "ConfDelete":
                    _Entity.Deletedata(_Entity);
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
        //protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        //{ 
        //    //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
        //    string ServiceType = "";
        //    string UnitOfMeasure = "";


        //    foreach (GridViewColumn column in gv1.Columns)
        //    {
        //        GridViewDataColumn dataColumn = column as GridViewDataColumn;
        //        //if (dataColumn == null) continue;
        //        //if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "PlantCode" && dataColumn.FieldName != "OrderQty")
        //        //{
        //        //    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
        //        //}
        //        //Checking for non existing Codes..
        //        ServiceType = e.NewValues["ServiceType"].ToString();
        //        UnitOfMeasure = e.NewValues["UnitOfMeasure"].ToString();

        //        DataTable ST = Gears.RetriveData2("SELECT ServiceType FROM Masterfile.WMSServiceType WHERE ServiceType = '" + ServiceType + "'");
        //        if (ST.Rows.Count < 1)
        //        {
        //            AddError(e.Errors, gv1.Columns["ServiceType"], "ServiceType doesn't exist!");
        //        }

        //        DataTable UOM = Gears.RetriveData2("SELECT UnitOfMeasure FROM Masterfile.WMSUnitOfMeasure WHERE UnitOfMeasure = '" + UnitOfMeasure + "'");
        //        if (UOM.Rows.Count < 1)
        //        {
        //            AddError(e.Errors, gv1.Columns["UnitOfMeasure"], "Unit of Measure doesn't exist!");
        //        }
        //    }

        //    if (e.Errors.Count > 0)
        //    {
        //        error = true; //bool to cancel adding/updating if true
        //    }
        //}
        //dictionary method to hold error 
        //void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        //{
        //    if (errors.ContainsKey(column)) return;
        //    errors[column] = errorText;
        //}
        //protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        //{
        //    if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
        //    {
        //        e.Handled = true;
        //        //e.DeleteValues.Clear();
        //        e.InsertValues.Clear();
        //        e.UpdateValues.Clear();
        //    }
        //}
        #endregion

        #region future ref
        //if (Request.QueryString["entry"].ToString() == "V")
        //{
        //    gv1.DataSourceID = "odsDetail";
        //    view = true; //sets view mode for entry
        //    updateBtn.Text = "Close"; //sets button name
        //    txtPlantCode.Value = Session["PlantCode"].ToString(); //sets PlantCode from session
        //    _Entity.getdata(txtPlantCode.Text); //Method for retrieving data from entity
        //    Supplier.Value = _Entity.SupplierCode.ToString(); //sets textbox value from retrieved data
        //    DocDate.Value = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
        //    TotalFreight.Value = _Entity.TotalFreight.ToString();
        //}
        //else if (Request.QueryString["entry"].ToString() == "E")
        //{
        //    gv1.DataSourceID = "odsDetail";
        //    updateBtn.Text = "Update";
        //    txtPlantCode.ReadOnly = true;
        //    txtPlantCode.Value = Session["PlantCode"].ToString();
        //    _Entity.getdata(txtPlantCode.Text);
        //    Supplier.Value = _Entity.SupplierCode.ToString();
        //}
        ////Changes datasourceID of header to enable adding of new records. //Add
        //else if (Request.QueryString["entry"].ToString() == "N")
        //{
        //    updateBtn.Text = "Add";
        //    gv1.DataSourceID = "sdsDetail";
        //    popup.ShowOnPageLoad = false;
        //}

        //if (!string.IsNullOrEmpty(FilePath) && check == true)
        //{
        //    e.Handled = true;
        //    DataTable source = gv1.DataSource as DataTable;

        //    // Removing all deleted rows from the data source(Excel file)
        //    foreach (ASPxDataDeleteValues values in e.DeleteValues)
        //    {
        //        try
        //        {
        //            object[] keys = { values.Keys[0], values.Keys[1], values.Keys[2], values.Keys[3],
        //                              values.Keys[4], values.Keys[5]};
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
        //            object[] keys = { values.Keys[0], values.Keys[1], values.Keys[2], values.Keys[3],
        //                              values.Keys[4], values.Keys[5]};
        //            DataRow row = source.Rows.Find(keys);
        //            row["ItemCode"] = values.NewValues["ItemCode"];
        //            row["ColorCode"] = values.NewValues["ColorCode"];
        //            row["ClassCode"] = values.NewValues["ClassCode"];
        //            row["SizeCode"] = values.NewValues["SizeCode"];
        //            row["OrderQty"] = values.NewValues["OrderQty"];
        //    }

        //    // Adding new rows
        //    foreach (ASPxDataInsertValues values in e.InsertValues)
        //    {
        //        var PlantCode = values.NewValues["PlantCode"];
        //        var LineNumber = values.NewValues["LineNumber"];
        //        var ItemCode = values.NewValues["ItemCode"];
        //        var ColorCode = values.NewValues["ColorCode"];
        //        var ClassCode = values.NewValues["ClassCode"];
        //        var SizeCode = values.NewValues["SizeCode"];
        //        var OrderQty = values.NewValues["OrderQty"];
        //        if (string.IsNullOrEmpty(OrderQty.ToString()))
        //        {
        //            OrderQty = 0;
        //        }
        //        source.Rows.Add(PlantCode, LineNumber, ItemCode, ColorCode, ClassCode, SizeCode, OrderQty);
        //    }

        //    foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
        //    {
        //        _EntityDetail.ItemCode = dtRow[2].ToString();
        //        _EntityDetail.ColorCode = dtRow[3].ToString();
        //        _EntityDetail.ClassCode = dtRow[4].ToString();
        //        _EntityDetail.SizeCode = dtRow[5].ToString();
        //        _EntityDetail.OrderQty = Convert.ToDecimal(dtRow[6].ToString());
        //        _EntityDetail.AddPODetail(_EntityDetail);
        //    }
        //}
        #endregion  




    }
}