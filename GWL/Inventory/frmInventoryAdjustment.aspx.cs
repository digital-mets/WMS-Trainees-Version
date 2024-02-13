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
using GearsInventory;

namespace GWL
{
    public partial class frmInventoryAdjustment : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string filter = "";

        private static string Connection;

        Entity.InventoryAdjustment _Entity = new InventoryAdjustment();//Calls entity odsHeader
        Entity.InventoryAdjustment.InventoryAdjustmentDetail _EntityDetail = new InventoryAdjustment.InventoryAdjustmentDetail();//Call entity sdsDetail

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
            
            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());
            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["INVADJTFilterItems"] = null;
                Session["INVADJTfilterexpression"] = null;
                Session["INVADJTreferencedetail"] = null;
                Session["INVADJTfilterexpression1"] = null;
                Session["INVADJTDataTable"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    gv1.KeyFieldName = "LineNumber";
                }

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Date = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                glAdjustmentType.Value = _Entity.AdjustmentType;
                txtRefNumber.Value = _Entity.RefNumber;
                glWarehouseCode.Value = _Entity.WarehouseCode;
                txtTotalQty.Value = _Entity.TotalQty;
                txtTotalBulkQty.Value = _Entity.TotalBulkQty;
                memRemarks.Value = _Entity.Remarks;
                cbxIsPrinted.Value = _Entity.IsPrinted;

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
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                ItemConversion();
                gv1.KeyFieldName = "LineNumber";

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
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.ItemAdjustmentDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                gvJournal.DataSourceID = "odsJournalEntry";
            }         
        }
        #endregion

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            strresult += GearsInventory.GInventory.ItemAdjustment_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Posting
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Table = "Inventory.ItemAdjustment";
            gparam._Factor = -1;
            strresult += GearsInventory.GInventory.ItemAdjustment_Post(gparam);
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
        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
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
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = true;
            }
        }

        protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = true;
            }
            else
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {}

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
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   
            //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
            {
                e.Visible = false;
            }
            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }
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
            if (Session["INVADJTfilterexpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsItemDetail.FilterExpression = Session["INVADJTfilterexpression"].ToString();
            }
            //if (Session["INVADJTfilterexpression1"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = "sdsItemDetailNew";
            //    sdsItemDetailNew.SelectCommand = Session["INVADJTfilterexpression1"].ToString();
            //    //gridLookup.DataBind();
            //}
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
            if (e.Parameters.Contains("ItemCode"))
            {
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,UnitBase,FullDesc, UnitBulk, A.IsByBulk from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["IsByBulk"].ToString() + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[3], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["INVADJTfilterexpression"] = sdsItemDetail.FilterExpression;
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
            _Entity.Transaction = Request.QueryString["transtype"].ToString();
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.AdjustmentType = String.IsNullOrEmpty(glAdjustmentType.Text) ? null : glAdjustmentType.Text;
            _Entity.RefNumber = String.IsNullOrEmpty(txtRefNumber.Text) ? null : txtRefNumber.Text;
            _Entity.WarehouseCode = String.IsNullOrEmpty(glWarehouseCode.Text) ? null : glWarehouseCode.Text;
            _Entity.TotalQty = String.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToDecimal(txtTotalQty.Value.ToString());
            _Entity.TotalBulkQty = String.IsNullOrEmpty(txtTotalBulkQty.Text) ? 0 : Convert.ToDecimal(txtTotalBulkQty.Value.ToString());
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.IsPrinted = Convert.ToBoolean(cbxIsPrinted.Value.ToString());
            
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            _Entity.Transaction = Request.QueryString["transtype"].ToString();

            switch (e.Parameter)
            {
                case "Add": 
                    
                    gv1.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber,"Inventory.ItemAdjustment",1,Connection);//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity); 
                        
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                       
                        _Entity.UpdateUnitFactor(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Update": 
                    gv1.UpdateEdit();

                    string strError1 = Functions.Submitted(_Entity.DocNumber,"Inventory.ItemAdjustment",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError1))
                        {
                            cp.JSProperties["cp_message"] = strError1;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid

                        _Entity.UpdateUnitFactor(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        Post();
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
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    gv1.DataSource = null;
                    break;

                case "ItemConv":
                    ItemConversion();
                    break;

                case "Cloned":
                    Session["INVADJTDataTable"] = "1";
                    break;
            }
        }        
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {}

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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
            if (Session["INVADJTDataTable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = new DataTable();
                if (glAdjustmentType.Text == "ITMCONV")
                { 
                    source = Gears.RetriveData2("SELECT TOP 1 DocNumber, CAST('' AS Varchar(20)) LineNumber, CAST('' AS Varchar(20)) AS ItemCode, CAST('' AS Varchar(20)) AS ColorCode, CAST('' AS Varchar(20)) AS ClassCode, CAST('' AS Varchar(20)) AS SizeCode, "
                                    + "0.0000 AS AdjustedQty, CAST('' AS Varchar(20)) AS Unit, 0.00 AS AdjustedBulkQty, CAST('' AS Varchar(20)) AS NewItemCode, CAST('' AS Varchar(20)) AS NewColorCode, CAST('' AS Varchar(20)) AS NewClassCode, CAST('' AS Varchar(20)) AS NewSizeCode, "
                                    + "0.0000 AS NewAdjustedQty, CAST('' AS Varchar(20)) AS NewUnit, 0.00 NewAdjustedBulkQty, CAST('' AS Varchar(20)) Field1, CAST('' AS Varchar(20)) Field2, CAST('' AS Varchar(20)) Field3,  CAST('' AS Varchar(20)) Field4, CAST('' AS Varchar(20)) Field5, CAST('' AS Varchar(20)) Field6, CAST('' AS Varchar(20)) "
                                    + "Field7, CAST('' AS Varchar(20)) Field8, CAST('' AS Varchar(20)) Field9, CAST(NULL AS Date) ExpDate, CAST(NULL AS Date) MfgDate, CAST('' AS Varchar(20)) BatchNo, CAST('' AS Varchar(20)) LotNo FROM Inventory.ItemAdjustment where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                }
                else
                {
                    source = Gears.RetriveData2("SELECT TOP 1 DocNumber, CAST('' AS Varchar(20)) LineNumber, CAST('' AS Varchar(20)) AS ItemCode, CAST('' AS Varchar(20)) AS ColorCode, CAST('' AS Varchar(20)) AS ClassCode, CAST('' AS Varchar(20)) AS SizeCode, "
                                   + "0.0000 AS AdjustedQty, CAST('' AS Varchar(20)) AS Unit, 0.00 AS AdjustedBulkQty, CAST('' AS Varchar(20)) Field1, CAST('' AS Varchar(20)) Field2, CAST('' AS Varchar(20)) Field3,  CAST('' AS Varchar(20)) Field4, CAST('' AS Varchar(20)) Field5, "
                                   + "CAST('' AS Varchar(20)) Field6, CAST('' AS Varchar(20)) Field7, CAST('' AS Varchar(20)) Field8, CAST('' AS Varchar(20)) Field9, CAST(NULL AS Date) ExpDate, CAST(NULL AS Date) MfgDate, CAST('' AS Varchar(20)) BatchNo, CAST('' AS Varchar(20)) LotNo " 
                                   + "FROM Inventory.ItemAdjustment where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                }
                Gears.RetriveData2("DELETE FROM Inventory.ItemAdjustmentDetail WHERE DocNumber = '"+txtDocNumber.Text+"'", Session["ConnString"].ToString());

                int ln = 1;
                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    //var LineNumber = values.NewValues["LineNumber"];
                    var LineNumber = ln;
                    var ItemCode = values.NewValues["ItemCode"];
                    //var FullDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var AdjustedQty = values.NewValues["AdjustedQty"]; 
                    var Unit = values.NewValues["Unit"];
                    var AdjustedBulkQty = values.NewValues["AdjustedBulkQty"];
                     
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];

                    var ExpDate = values.NewValues["ExpDate"];
                    var MfgDate = values.NewValues["MfgDate"];
                    var BatchNo = values.NewValues["BatchNo"];
                    var LotNo = values.NewValues["LotNo"];

                    if (glAdjustmentType.Text == "ITMCONV")
                    {
                        var NewItemCode = values.NewValues["NewItemCode"];
                        var NewColorCode = values.NewValues["NewColorCode"];
                        var NewClassCode = values.NewValues["NewClassCode"];
                        var NewSizeCode = values.NewValues["NewSizeCode"];
                        var NewAdjustedQty = values.NewValues["NewAdjustedQty"];
                        var NewUnit = values.NewValues["NewUnit"];
                        var NewAdjustedBulkQty = values.NewValues["NewAdjustedBulkQty"];
                        source.Rows.Add(txtDocNumber.Text, LineNumber, ItemCode, ColorCode, ClassCode, SizeCode, AdjustedQty, Unit,
                                    AdjustedBulkQty, NewItemCode, NewColorCode, NewClassCode, NewSizeCode, Convert.IsDBNull(NewAdjustedQty) ? 0 : NewAdjustedQty, NewUnit,
                                    Convert.IsDBNull(NewAdjustedBulkQty) ? 0 : NewAdjustedQty, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9,
                                    ExpDate, MfgDate, BatchNo, LotNo);
                    }
                    else
                    {
                        source.Rows.Add(txtDocNumber.Text, LineNumber, ItemCode, ColorCode, ClassCode, SizeCode, AdjustedQty, Unit,
                                    AdjustedBulkQty, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9,
                                    ExpDate, MfgDate, BatchNo, LotNo);
                    }
                    ln++;
                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0], values.Keys[1] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    if (dtRow["LineNumber"].ToString() != "" && dtRow["LineNumber"].ToString() != null)
                    {
                        _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                        _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                        _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                        _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();

                        _EntityDetail.AdjustedQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["AdjustedQty"].ToString()) ? "0" : dtRow["AdjustedQty"].ToString());
                        _EntityDetail.Unit = dtRow["Unit"].ToString();
                        _EntityDetail.AdjustedBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["AdjustedBulkQty"].ToString()) ? "0" : dtRow["AdjustedBulkQty"].ToString());
                        if (glAdjustmentType.Text == "ITMCONV")
                        {
                            _EntityDetail.NewItemCode = dtRow["NewItemCode"].ToString();
                            _EntityDetail.NewColorCode = dtRow["NewColorCode"].ToString();
                            _EntityDetail.NewClassCode = dtRow["NewClassCode"].ToString();
                            _EntityDetail.NewSizeCode = dtRow["NewSizeCode"].ToString();
                            _EntityDetail.NewAdjustedQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["NewAdjustedQty"].ToString()) ? "0" : dtRow["NewAdjustedQty"].ToString());
                            _EntityDetail.NewUnit = dtRow["NewUnit"].ToString();
                            _EntityDetail.NewAdjustedBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["NewAdjustedBulkQty"].ToString()) ? "0" : dtRow["NewAdjustedBulkQty"].ToString());
                        }
                        _EntityDetail.Field1 = dtRow["Field1"].ToString();
                        _EntityDetail.Field2 = dtRow["Field2"].ToString();
                        _EntityDetail.Field3 = dtRow["Field3"].ToString();
                        _EntityDetail.Field4 = dtRow["Field4"].ToString();
                        _EntityDetail.Field5 = dtRow["Field5"].ToString();
                        _EntityDetail.Field6 = dtRow["Field6"].ToString();
                        _EntityDetail.Field7 = dtRow["Field7"].ToString();
                        _EntityDetail.Field8 = dtRow["Field8"].ToString();
                        _EntityDetail.Field9 = dtRow["Field9"].ToString();
                        _EntityDetail.ExpDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["ExpDate"]) ? null : dtRow["ExpDate"]);
                        _EntityDetail.MfgDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["MfgDate"]) ? null : dtRow["MfgDate"]);
                        _EntityDetail.BatchNo = dtRow["BatchNo"].ToString();
                        _EntityDetail.LotNo = dtRow["LotNo"].ToString();

                        _EntityDetail.AddItemAdjustmentDetail(_EntityDetail); 
                    }
                }
            }
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["INVADJTreferencedetail"] = null;
            }
            if (Session["INVADJTreferencedetail"] != null)
            {
                gv1.DataSourceID = sdsRefIssuanceDetail.ID;
                sdsRefIssuanceDetail.FilterExpression = Session["INVADJTreferencedetail"].ToString();
            }
        }

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
            sdsItemDetailNew.ConnectionString = Session["ConnString"].ToString();
            sdsBizPartnerCus.ConnectionString = Session["ConnString"].ToString();
            sdsType.ConnectionString = Session["ConnString"].ToString();
            sdsUnit.ConnectionString = Session["ConnString"].ToString();
            sdsBulkUnit.ConnectionString = Session["ConnString"].ToString();
            sdsRefSO.ConnectionString = Session["ConnString"].ToString();
            sdsRefIssuanceDetail.ConnectionString = Session["ConnString"].ToString();
            sdsWarehouse.ConnectionString = Session["ConnString"].ToString();

            sdsRequestedBy.ConnectionString = Session["ConnString"].ToString();
            sdsMaterialType.ConnectionString = Session["ConnString"].ToString();
            sdsSpecificMaterialType.ConnectionString = Session["ConnString"].ToString();
            sdsCostCenter.ConnectionString = Session["ConnString"].ToString();
            sdsSpecificMaterial.ConnectionString = Session["ConnString"].ToString();

            sdsSamplesType.ConnectionString = Session["ConnString"].ToString();
            sdsIssuanceNumber.ConnectionString = Session["ConnString"].ToString();
            sdsCurrency.ConnectionString = Session["ConnString"].ToString();
            sdsAdjustmentType.ConnectionString = Session["ConnString"].ToString();
        }
        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
            if (Session["INVADJTfilterexpression1"] != null)
            {
                sdsItemDetailNew.SelectCommand = Session["INVADJTfilterexpression1"].ToString();
                gridLookup.DataBind();
            }
            
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("NewItmCde"))
            {
                string itemcode = e.Parameters.Split('|')[1];//Set Item Code
                ASPxGridView grid = sender as ASPxGridView;
                string finalquery =
                    //  " SELECT DISTINCT A.ItemCode, ColorCode, ClassCode, SizeCode FROM Masterfile.[Item] A "
                    //+ " LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode "
                    //+ " WHERE A.ItemCategoryCode = (SELECT ItemCategoryCode FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "') AND "
                    //+ " ISNULL(B.IsInactive,0) != 1";
                                    " SELECT DISTINCT A.ItemCode, [FullDesc], [ShortDesc] FROM Masterfile.[Item] A "
                                  + " WHERE A.ItemCategoryCode = (SELECT ItemCategoryCode FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "') "
                                  + " AND ISNULL(A.IsInactive,0) = 0";
                sdsItemDetailNew.SelectCommand = finalquery;
                //Session["INVADJTfilterexpression1"] = sdsItemDetailNew.SelectCommand;
                Session["INVADJTfilterexpression1"] = sdsItemDetailNew.SelectCommand;
                grid.DataSourceID = "sdsItemDetailNew";
                grid.DataBind();
            }
            else
            {
                if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
                ASPxGridView grid = sender as ASPxGridView;
                grid.DataSourceID = "sdsItem";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "ItemCode") != null)
                        if (grid.GetRowValues(i, "ItemCode").ToString() == e.Parameters)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "ItemCode").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }

        public void ItemConversion()
        {
            if (glAdjustmentType.Text == "ITMCONV")
            {
                gv1.Columns["NewItemCode"].Width = 200;
                gv1.Columns["NewColorCode"].Width = 100;
                gv1.Columns["NewClassCode"].Width = 100;
                gv1.Columns["NewSizeCode"].Width = 100;
                gv1.Columns["NewAdjustedQty"].Width = 100;
                gv1.Columns["NewUnit"].Width = 100;
                gv1.Columns["NewAdjustedBulkQty"].Width = 100;
            }
            else
            {
                gv1.Columns["NewItemCode"].Width = 0;
                gv1.Columns["NewColorCode"].Width = 0;
                gv1.Columns["NewClassCode"].Width = 0;
                gv1.Columns["NewSizeCode"].Width = 0;
                gv1.Columns["NewAdjustedQty"].Width = 0;
                gv1.Columns["NewUnit"].Width = 0;
                gv1.Columns["NewAdjustedBulkQty"].Width = 0;
            }
        }
    }
}