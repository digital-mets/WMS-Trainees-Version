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
using GearsTrading;
using DevExpress.Data.Filtering;
using GearsWarehouseManagement;

namespace GWL
{
    public partial class frmPhysicalCountWMS : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.PhysicalCount _Entity = new PhysicalCount();//Calls entity odsHeader
        Entity.PhysicalCount.PhysicalCountDetail _EntityDetail = new PhysicalCount.PhysicalCountDetail();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
	    Gears.UseConnectionString(Session["ConnString"].ToString());

            if (!string.IsNullOrEmpty(aglWarehouseCode.Text))
            {
                Session["FilterWH"] = aglWarehouseCode.Text;
            }
            if (!string.IsNullOrEmpty(txtplantcode.Text))
            {
                Session["FilterPlant"] = txtplantcode.Text;
            }
            if (!string.IsNullOrEmpty(txtRoomCode.Text))
            {
                Session["FilterRoom"] = txtRoomCode.Text;
            }

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

            gv1.KeyFieldName = "DocNumber;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }


            if (!IsPostBack)
            {

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Update";
                        txtStatus.Text = "New";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "D":
                        //view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        //view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        break;
                }

                //txtDocNumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
                sdsWarehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";
          
                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //    //gv1.DataSourceID = "sdsDetail";
                //    popup.ShowOnPageLoad = false;
                //}
                //else
                //{
                    //gv1.DataSourceID = "odsDetail";
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                    dtpDocDate.Date = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    cbxtype.Text = _Entity.Type;
                    aglStorageType.Value = _Entity.StorageType;
                    aglWarehouseCode.Value = _Entity.WarehouseCode;
                    Session["FilterWH"] = aglWarehouseCode.Text;
                    txtplantcode.Value = _Entity.PlantCode;
                    Session["FilterPlant"] = txtplantcode.Text;
                    txtRoomCode.Value = _Entity.RoomCode;
                    Session["FilterRoom"] = txtRoomCode.Text;
                    cmbStorerKey.Value = _Entity.CustomerCode.Trim();
                    txtCountTag.Value = _Entity.CountTag;
                    dtpCountDate.Date = String.IsNullOrEmpty(_Entity.CountDate) ? DateTime.Now : Convert.ToDateTime(_Entity.CountDate);
                    txtStatus.Text = _Entity.Status;
                    txtRemarks.Value = _Entity.Remarks;
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
                //}

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    //gv1.DataSourceID = "sdsDetail";
                //}

                    DataTable dtbldetail = Gears.RetriveData2("select Docnumber from WMS.PhysicalCountDetail where docnumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                gv1.DataSourceID = (dtbldetail.Rows.Count>0 ? "odsDetail":"sdsDetail");
            }

            
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._Connection = Session["ConnString"].ToString();
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSPHC";

            string strresult = GWarehouseManagement.PhysicalCountWMS_Validate(gparam);

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
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
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
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DropDownButton.Enabled = !view;
            combo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void ButtonLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (Request.QueryString["entry"] == "E")
            {
                var look = sender as ASPxButton;
                if (look != null)
                {
                    look.Enabled = true;
                }
            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }

            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }

            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        #endregion

        #region Lookup Settings
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
            else
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            string val2 = "";
            try
            {
                val2 = e.Parameters.Split('|')[3];
            }
            catch (Exception)
            {
                val2 = "";
            }
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string qty = "";
            decimal totalqty = 0;
            string codes = "";

            if (val2 == "null")
            {
                val2 = "0";
            }

            if (e.Parameters.Contains("ItemCode"))
            {
                DataTable getall = Gears.RetriveData2("Select top 1 ColorCode,ClassCode,SizeCode,FullDesc,isnull(StandardQty,0) as StandardQty,UnitBulk,UnitBase from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "' " +
                                                          "and isnull(a.IsInactive,0)=0", Session["ConnString"].ToString());
                foreach (DataRow dt in getall.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    qty = dt["StandardQty"].ToString();
                    qty = string.IsNullOrEmpty(qty) ? "0" : qty;
                }
                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val2));
                    codes += totalqty + ";";
                }

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("BulkQty"))
            {
                DataTable getqty = Gears.RetriveData2("Select isnull(StandardQty,0) as StandardQty from masterfile.item where itemcode = '" + itemcode + "'", Session["ConnString"].ToString());
                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["StandardQty"].ToString();
                    }
                }
                qty = String.IsNullOrEmpty(qty) ? "0" : qty;
                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val));
                }

                itemlookup.JSProperties["cp_codes"] = totalqty + ";";
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = sdsItemDetail.FilterExpression;
                grid.DataSourceID = "sdsItemDetail";
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
            Gears.UseConnectionString(Session["ConnString"].ToString());

            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Type = cbxtype.Text;
            _Entity.StorageType = aglStorageType.Text;
            _Entity.WarehouseCode = aglWarehouseCode.Text;
            _Entity.PlantCode = txtplantcode.Text;
            _Entity.RoomCode = txtRoomCode.Text;
            _Entity.CustomerCode = cmbStorerKey.Text;
            _Entity.CountTag = txtCountTag.Text;
            _Entity.CountDate = dtpCountDate.Text;
            _Entity.Remarks = txtRemarks.Text;
            _Entity.Status = txtStatus.Text;
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

            _Entity.Transtype = Request.QueryString["transtype"].ToString();

            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
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

                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
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

                case "Plant":

                CriteriaOperator plantCriteria = new InOperator("PlantCode", new string[] { txtplantcode.Text });
                CriteriaOperator warehouseCriteria = new InOperator("WarehouseCode", new string[] { aglWarehouseCode.Text });
                CriteriaOperator storageCriteria = new InOperator("StorageType", new string[] { aglStorageType.Text });
                if (!string.IsNullOrEmpty(txtplantcode.Text))
                {
                    Roomsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, plantCriteria, warehouseCriteria, storageCriteria)).ToString();
                }
                else
                {
                    Roomsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And,  warehouseCriteria, storageCriteria)).ToString();
                }
                txtRoomCode.DataSourceID = "Roomsql";
                txtRoomCode.DataBind();
      
                break;
                    
                case "WH":
                CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { aglWarehouseCode.Text });
                Plantsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                txtplantcode.DataSourceID = "Plantsql";
                txtplantcode.DataBind();
                break;

               

                case "refgrid":
                gv1.DataBind();
                break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "" ;
            //string SizeCode = "";

            //foreach (GridViewColumn column in gv1.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" 
            //        && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4"
            //        && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
            //        && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "BarcodeNo" && dataColumn.FieldName != "StatusCode" && dataColumn.FieldName != "BulkUnit"
            //        && dataColumn.FieldName != "BulkQty" && dataColumn.FieldName != "VarianceQty" && dataColumn.FieldName != "VarianceBulkQty" && dataColumn.FieldName != "SystemQty" && dataColumn.FieldName != "SystemBulkQty"
            //        && dataColumn.FieldName != "BaseQty" && dataColumn.FieldName != "PalletID")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //        //Checking for non existing Codes..
            //        ItemCode = String.IsNullOrEmpty(e.NewValues["ItemCode"].ToString()) ? "" : e.NewValues["ItemCode"].ToString();
            //        ColorCode = String.IsNullOrEmpty(e.NewValues["ColorCode"].ToString()) ? "" : e.NewValues["ColorCode"].ToString();
            //        ClassCode = String.IsNullOrEmpty(e.NewValues["ClassCode"].ToString()) ? "" : e.NewValues["ClassCode"].ToString();
            //        SizeCode = String.IsNullOrEmpty(e.NewValues["SizeCode"].ToString()) ? "" : e.NewValues["SizeCode"].ToString();

            //        DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode +"'");
            //        if (item.Rows.Count < 1)
            //        {
            //            AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //        }
            //        DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
            //        if (color.Rows.Count < 1)
            //        {
            //            AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //        }
            //        DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
            //        if (clss.Rows.Count < 1)
            //        {
            //            AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //        }
            //        DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
            //        if (size.Rows.Count < 1)
            //        {
            //            AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
            //        }
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

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            sdsItem.ConnectionString = Session["ConnString"].ToString();
            sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            sdsBizPartner.ConnectionString = Session["ConnString"].ToString();
            sdsWarehouse.ConnectionString = Session["ConnString"].ToString();
            sdsStorageType.ConnectionString = Session["ConnString"].ToString();
            sdsLocation.ConnectionString = Session["ConnString"].ToString();
            Unit.ConnectionString = Session["ConnString"].ToString();
            StorerKey.ConnectionString = Session["ConnString"].ToString();
            Plantsql.ConnectionString = Session["ConnString"].ToString();
            Roomsql.ConnectionString = Session["ConnString"].ToString();
        }

        protected void glwarehouseinit(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glplant_CustomCallback);
            if (Session["LocationExp"] != null)
            {
                gridLookup.GridView.DataSourceID = Plantsql.ID;
                Plantsql.FilterExpression = Session["LocationExp"].ToString();
            }
        }
        public void glplant_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string whcode = e.Parameters;//Set column name
            if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { whcode });
            Plantsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["LocationExp"] = Plantsql.FilterExpression;
            grid.DataSourceID = Plantsql.ID;
            grid.DataBind();

        }
        protected void txtRoomCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glroomcode_CustomCallback);
        }
        public void glroomcode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string whcode = e.Parameters.Split('|')[0]; ;//Set column name

            if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback
            string plantcode = e.Parameters.Split('|')[1]; //SetPalletID column name

            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { whcode });
            CriteriaOperator selectionCriteria1 = new InOperator("PlantCode", new string[] { plantcode });
            Roomsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, selectionCriteria1)).ToString();
            Session["RoomExp"] = Roomsql.FilterExpression;
            grid.DataSourceID = Roomsql.ID;
            grid.DataBind();

        }

        //public void gllocation_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{

        //    string whcode = e.Parameters.Split('|')[0]; ;//Set column name

        //    if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback
        //    string plantcode = e.Parameters.Split('|')[1]; //SetPalletID column name
        //    string roomcode = e.Parameters.Split('|')[2]; //SetPalletID column name

        //    ASPxGridView grid = sender as ASPxGridView;
        //    //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
        //    //var selectedValues = itemcode;
        //    grid.DataSourceID = null;
         
        //    CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { whcode });
        //    CriteriaOperator selectionCriteria1 = new InOperator("PlantCode", new string[] { plantcode });
        //    CriteriaOperator selectionCriteria2 = new InOperator("RoomCode", new string[] { roomcode });
            
        //    Session["LocationExp1"] = sdsLocation.FilterExpression;
        //    grid.DataSourceID = sdsLocation.ID;
        //    grid.DataBind();

        //}

        protected void LocationCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gllocation_CustomCallback);
            FilterLoc();
            gridLookup.DataBind();
        }

        public void FilterLoc()
        {
            if (Session["FilterWH"] == null)
            {
                Session["FilterWH"] = "";
            }


            if (!String.IsNullOrEmpty(Session["FilterPlant"].ToString()))
            {
                if (!String.IsNullOrEmpty(Session["FilterRoom"].ToString()))
                {
                    sdsLocation.SelectCommand = "SELECT [LocationCode],[WarehouseCode],[PlantCode],[RoomCode] FROM Masterfile.[Location] WHERE ISNULL(IsInActive,0)=0 and WarehouseCode = '" + Session["FilterWH"].ToString() + "'" +
                    " and PlantCode = '" + Session["FilterPlant"].ToString() + "' and RoomCode = '" + Session["FilterRoom"].ToString() + "' ORDER BY LocationCode ASC";
                }
                else
                {
                    sdsLocation.SelectCommand = "SELECT [LocationCode],[WarehouseCode],[PlantCode],[RoomCode] FROM Masterfile.[Location] WHERE ISNULL(IsInActive,0)=0 and WarehouseCode = '" + Session["FilterWH"].ToString() + "'" +
                    " and PlantCode = '" + Session["FilterPlant"].ToString() + "' ORDER BY LocationCode ASC";

                }
            }
            else
            {

                sdsLocation.SelectCommand = "SELECT [LocationCode],[WarehouseCode],[PlantCode],[RoomCode] FROM Masterfile.[Location] WHERE ISNULL(IsInActive,0)=0 and WarehouseCode = '" + Session["FilterWH"].ToString() + "'  ORDER BY LocationCode ASC";
                
            }
        }

        public void gllocation_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterLoc();
            grid.DataSourceID = "sdsLocation";
            grid.DataBind();
        }

        //Genrev 12/11/2015 Added codes to filter itemcode by customer
        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
        }
        public void FilterItem()
        {
            if (!String.IsNullOrEmpty(cmbStorerKey.Text))
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 AND (ISNULL(Customer,'') = '' OR Customer = '" + cmbStorerKey.Text + "') ORDER BY ItemCode ASC";
            }
            else
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0";
            }
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            FilterItem();
            grid.DataSource = sdsItem;
            if (grid.DataSourceID != null)
            {
                grid.DataSourceID = null;
            }
            grid.DataBind();

        }
        //GC End

        #region future ref
        //if (Request.QueryString["entry"].ToString() == "V")
        //{
        //    gv1.DataSourceID = "odsDetail";
        //    view = true; //sets view mode for entry
        //    updateBtn.Text = "Close"; //sets button name
        //    txtDocNumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
        //    _Entity.getdata(txtDocNumber.Text); //Method for retrieving data from entity
        //    Supplier.Value = _Entity.SupplierCode.ToString(); //sets textbox value from retrieved data
        //    DocDate.Value = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
        //    TotalFreight.Value = _Entity.TotalFreight.ToString();
        //}
        //else if (Request.QueryString["entry"].ToString() == "E")
        //{
        //    gv1.DataSourceID = "odsDetail";
        //    updateBtn.Text = "Update";
        //    txtDocNumber.ReadOnly = true;
        //    txtDocNumber.Value = Session["DocNumber"].ToString();
        //    _Entity.getdata(txtDocNumber.Text);
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
        //        var DocNumber = values.NewValues["DocNumber"];
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
        //        source.Rows.Add(DocNumber, LineNumber, ItemCode, ColorCode, ClassCode, SizeCode, OrderQty);
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