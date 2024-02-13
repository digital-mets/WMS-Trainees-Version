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
    public partial class frmPurchaseCostChange : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.PurchaseCostChange _Entity = new PurchaseCostChange();//Calls entity ICN
        Entity.PurchaseCostChange.PurchaseCostChangeDetail _EntityDetail = new PurchaseCostChange.PurchaseCostChangeDetail();//Call entity POdetails

        private static string Connection;

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
            
            deDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Session["RRses"] = null;
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;

                Connection = Session["ConnString"].ToString();
                switch (Request.QueryString["entry"].ToString())
                {
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }

                gv1.KeyFieldName = "DocNumber;LineNumber";
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                }
                    _Entity.getdata(txtDocnumber.Text, Connection);

                    deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    txtRRNo.Value = _Entity.ReferenceRRNo;
                    txtSupplier.Text = _Entity.SupplierCode;
                    txtBroker.Text = _Entity.Broker;
                    txtAPMemo.Text = _Entity.APMemo;
                    APRemarks.Text = _Entity.Remarks;
                    txtOldTotal.Text = _Entity.OldTotalAmount.ToString();
                    txtNewTotal.Text = _Entity.NewTotalAmount.ToString();
                    txtOldRate.Text = _Entity.OldRate.ToString();
                    txtNewRate.Text = _Entity.NewRate.ToString();
                    txtOldFreight.Text = _Entity.OldFreight.ToString();
                    txtNewFreight.Text = _Entity.NewFreight.ToString();
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;
                    txtHAddedBy.Text = _Entity.AddedBy;
                    txtHAddedDate.Text = _Entity.AddedDate;
                    txtHLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHLastEditedDate.Text = _Entity.LastEditedDate;
                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;
                    txtHPostedBy.Text = _Entity.PostedBy;
                    txtHPostedDate.Text = _Entity.PostedDate;

                DataTable checkCount = Gears.RetriveData2("Select DocNumber from Procurement.PurchaseCostChangeDetail where docnumber = '" + txtDocnumber.Text + "'", Connection);
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = null;
                }

                if (!string.IsNullOrEmpty(_Entity.LastEditedBy) && (Request.QueryString["entry"].ToString() == "E" || Request.QueryString["entry"].ToString() == "N"))
                {
                    updateBtn.Text = "Update";
                }

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    gvRef.DataSourceID = "odsReference";

                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;



                }
            }
            gvJournal.DataSourceID = "odsJournalEntry";
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProcurement.GProcurement.PurchaseCostChange_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Table = "Procurement.PurchaseCostChange";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.PurchaseCostChange_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }


        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo text = sender as ASPxMemo;
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

            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Visible = false;
            }

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
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
            else
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
            }
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
                DataTable countcolor = Gears.RetriveData2("Select distinct ColorCode from masterfile.itemdetail where itemcode = '" + itemcode + "'",Connection);
                if (countcolor.Rows.Count == 1)
                {
                    foreach (DataRow dt in countcolor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                    }
                }

                DataTable countclass = Gears.RetriveData2("Select distinct ClassCode from masterfile.itemdetail where itemcode = '" + itemcode + "'", Connection);
                if (countclass.Rows.Count == 1)
                {
                    foreach (DataRow dt in countclass.Rows)
                    {
                        codes += dt["ClassCode"].ToString() + ";";
                    }
                }

                DataTable sizeclass = Gears.RetriveData2("Select distinct SizeCode from masterfile.itemdetail where itemcode = '" + itemcode + "'", Connection);
                if (sizeclass.Rows.Count == 1)
                {
                    foreach (DataRow dt in sizeclass.Rows)
                    {
                        codes += dt["SizeCode"].ToString() + ";";
                    }
                }

                DataTable itemdesc = Gears.RetriveData2("Select FullDesc from masterfile.item where itemcode = '" + itemcode + "'", Connection);
                if (itemdesc.Rows.Count == 1)
                {
                    foreach (DataRow dt in itemdesc.Rows)
                    {
                        codes += dt["FullDesc"].ToString() + ";";
                    }
                }

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
                grid.DataSourceID = "Masterfileitemdetail";
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
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = deDocDate.Text;
            _Entity.Connection = Connection;
            _Entity.ReferenceRRNo = txtRRNo.Text;
            _Entity.SupplierCode = txtSupplier.Text;
            _Entity.Broker = txtBroker.Text;
            _Entity.APMemo = txtAPMemo.Text;
            _Entity.Remarks = APRemarks.Text;
            _Entity.OldTotalAmount = String.IsNullOrEmpty(txtOldTotal.Text) ? 0 : Convert.ToDecimal(txtOldTotal.Text);
            _Entity.NewTotalAmount = String.IsNullOrEmpty(txtNewTotal.Text) ? 0 : Convert.ToDecimal(txtNewTotal.Text);
            _Entity.OldRate = String.IsNullOrEmpty(txtOldRate.Text) ? 0 : Convert.ToDecimal(txtOldRate.Text);
            _Entity.NewRate = String.IsNullOrEmpty(txtNewRate.Text) ? 0 : Convert.ToDecimal(txtNewRate.Text);
            _Entity.OldFreight = String.IsNullOrEmpty(txtOldFreight.Text) ? 0 : Convert.ToDecimal(txtOldFreight.Text);
            _Entity.NewFreight = String.IsNullOrEmpty(txtNewFreight.Text) ? 0 : Convert.ToDecimal(txtNewFreight.Text);
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
                    //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    //if (error == false)
                    //{
                    //    check = true;
                    //    //_Entity.InsertData(_Entity);//Method of inserting for header
                    //    _Entity.AddedBy = Session["userid"].ToString();
                    //    _Entity.UpdateData(_Entity);
                    //    gv1.DataSourceID = "odsDetail";
                    //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    //    gv1.UpdateEdit();//2nd initiation to insert grid
                    //    Validate();
                    //    cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                    //    cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                    //    cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    //    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

                    //break;

                case "Update":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsRRDetail.ID;
                            gv1.UpdateEdit();
                            gv1.EndUpdate();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }

                        Validate();
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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

                case "RRDoc":
                    GetSelectedVal();
                    SqlDataSource ds = Masterfileitem;
                    ds.SelectCommand = string.Format("SELECT SupplierCode,Broker,ExchangeRate,TotalAmount,TotalFreight from procurement.receivingreport where docnumber = '" + txtRRNo.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtSupplier.Value = tran[0][0].ToString();
                        txtBroker.Value = tran[0][1].ToString();
                        txtOldTotal.Value = tran[0][3].ToString();
                        txtNewTotal.Value = tran[0][3].ToString();
                        txtOldRate.Value = tran[0][2].ToString();
                        txtNewRate.Value = tran[0][2].ToString();
                        txtOldFreight.Value = tran[0][4].ToString();
                        txtNewFreight.Value = tran[0][4].ToString();
                    }
                    ds.SelectCommand = string.Format("SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] where isnull(IsInactive,'')=0");
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";
            foreach (GridViewColumn column in gv1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "Type"
                    && dataColumn.FieldName != "IncomingDocNumber" && dataColumn.FieldName != "IncomingDocType" && dataColumn.FieldName != "Remarks" && dataColumn.FieldName != "Price"
                    && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4"
                    && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
                    && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "BarcodeNo")
                {
                    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                }
                //Checking for non existing Codes..

                ItemCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ItemCode"])) ? "" : e.NewValues["ItemCode"].ToString();
                ColorCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ColorCode"])) ? "" : e.NewValues["ColorCode"].ToString();
                ClassCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ClassCode"])) ? "" : e.NewValues["ClassCode"].ToString();
                SizeCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["SizeCode"])) ? "" : e.NewValues["SizeCode"].ToString();

                DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'", Connection);
                if (item.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
                }
                DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'", Connection);
                if (color.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
                }
                DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'", Connection);
                if (clss.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
                }
                DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'", Connection);
                if (size.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
                }
            }

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

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                _EntityDetail.DocNumber = txtDocnumber.Text;
                _EntityDetail.DeleteAllPurchaseCostChangeDetail(_EntityDetail);

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["NewUnitCost"] = values.NewValues["NewUnitCost"];
                    row["NewUnitFreight"] = values.NewValues["NewUnitFreight"];
                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                }

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.PODocNumber = dtRow["PODocnumber"].ToString();
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Qty = Convert.ToDecimal(dtRow["Qty"].ToString());
                    _EntityDetail.UnitCost = Convert.ToDecimal(dtRow["UnitCost"].ToString());
                    _EntityDetail.NewUnitCost = Convert.ToDecimal(dtRow["NewUnitCost"].ToString());
                    _EntityDetail.UnitFreight = Convert.ToDecimal(dtRow["UnitFreight"].ToString());
                    _EntityDetail.NewUnitFreight = Convert.ToDecimal(dtRow["NewUnitFreight"].ToString());
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();

                    _EntityDetail.AddPurchaseCostChangeDetail(_EntityDetail);
                }
            }
        }
        #endregion

        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            string[] selectedValues = txtRRNo.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(txtRRNo.KeyFieldName, selectedValues);
            sdsRRDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["RRses"] = sdsRRDetail.FilterExpression;
            gv1.DataSourceID = sdsRRDetail.ID;
            gv1.DataBind();
            Session["Datatable"] = "1";

            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
                if (dataColumn.FieldName == "NewUnitCost")
                {
                    dt.Columns[9].DataType = typeof(decimal);
                }
            }
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["DocNumber"],dt.Columns["LineNumber"]};

            return dt;
        }

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RRses"] = null;
            }

            if (Session["RRses"] != null)
            {
                sdsRRDetail.FilterExpression = Session["RRses"].ToString();
                gv1.DataSourceID = sdsRRDetail.ID;
                //gridview.DataSourceID = null;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //RRref.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            //Unit.ConnectionString = Session["ConnString"].ToString();
            //sdsRRDetail.ConnectionString = Session["ConnString"].ToString();
        }


    }
}