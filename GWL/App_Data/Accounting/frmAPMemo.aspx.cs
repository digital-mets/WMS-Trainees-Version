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


namespace GWL
{
    public partial class frmAPMemo : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.APMemo _Entity = new APMemo();//Calls entity PO
        Entity.APMemo.APMemoDetail _EntityDetail = new APMemo.APMemoDetail();//Call entity POdetails

        private static string Connection;

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Type.Value != null)
            {
                //if (Type.Value.ToString() == "PR")
                //{
                //    ReferenceDoc.SelectCommand = "select DocNumber,DocDate from accounting.APVoucher where ReferenceNumber is not null " +
                //                                 "and isnull(submittedby,'')!=''";
                //    RefDoc.DataBind();
                //}
                //else if (Type.Value.ToString() == "OA")
                //{
                //    ReferenceDoc.SelectCommand = "select DocNumber,DocDate from Procurement.PurchaseReturn where docnumber is null";
                //    RefDoc.DataBind();
                //    Session["Datatable"] = "0";
                //    gv1.DataSourceID = null;
                //    gv1.DataBind();
                //}
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

            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                Session["icndetail"] = null;
                Connection = Session["ConnString"].ToString();

                switch (Request.QueryString["entry"].ToString())
                {

                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                }

                    _Entity.getdata(txtDocNumber.Text,Connection); //Method for retrieving data from entity
                    DocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    Supplier.Text = _Entity.SupplierCode.ToString();
                    SupplierName.Text = _Entity.SupplierName;
                    Type.Value = _Entity.Type;
                    String type = (Type.Value ?? String.Empty).ToString();
                    _Entity.Type = type;
                    if (type == "PCC" || type == "PR")
                    {
                        Type.Enabled = false;
                    }
                    if ((type == "" || type == "OA") && Request.QueryString["entry"].ToString() != "V")
                    {
                        Type.Value = "OA";
                        TotalGross.ReadOnly = false;
                        TotalVat.ReadOnly = false;
                    }
                    RefDoc.Text = _Entity.ReferenceDocnumber;
                    RefDate.Text = String.IsNullOrEmpty(_Entity.ReferenceDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.ReferenceDate).ToShortDateString();
                    Remarks.Text = _Entity.Remarks;
                    TotalGross.Text = _Entity.TotalGrossAmount.ToString();
                    TotalVat.Text = _Entity.TotalVatAmount.ToString();
                    TotalAmt.Text = _Entity.TotalAmount.ToString();

                    txtHField1.Value = _Entity.Field1.ToString();
                    txtHField2.Value = _Entity.Field2.ToString();
                    txtHField3.Value = _Entity.Field3.ToString();
                    txtHField4.Value = _Entity.Field4.ToString();
                    txtHField5.Value = _Entity.Field5.ToString();
                    txtHField6.Value = _Entity.Field6.ToString();
                    txtHField7.Value = _Entity.Field7.ToString();
                    txtHField8.Value = _Entity.Field8.ToString();
                    txtHField9.Value = _Entity.Field9.ToString();

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




                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                DataTable checkCount = Gears.RetriveData2("Select DocNumber from accounting.apmemodetail where docnumber = '" + txtDocNumber.Text + "'", Connection);
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "TransDocNumber;LineNumber";
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.KeyFieldName = "TransDocNumber;LineNumber";
                    gv1.DataSourceID = "sdsDetail";
                }

                gvJournal.DataSourceID = "odsJournalEntry";

                if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Update";
                }
            }

            
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTAPM";
            gparam._Connection = Connection;
            string strresult = GearsAccounting.GAccounting.APMemo_Validate(gparam);
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
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTAPM";
            gparam._Table = "Accounting.APMemo";
            gparam._Factor = -1;
            gparam._Connection = Connection;
            string strresult = GearsAccounting.GAccounting.APMemo_Post(gparam);
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

        protected void ComboboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxComboBox cb = sender as ASPxComboBox;
            cb.ReadOnly = view;
        }

        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
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
                e.Image.IconID = "tasks_newtask_16x16";
            }
            //if (e.ButtonType == ColumnCommandButtonType.New)
            //{
            //    e.Image.IconID = "tasks_newtask_16x16";
            //}
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update||e.ButtonType == ColumnCommandButtonType.New)
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
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

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

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            //int i;
            //bool result = int.TryParse(e.Parameter, out i);
            //if (result == true)
            //{
            //    rowcount = i;
            //}
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = DocDate.Text;
            _Entity.Connection = Connection;
            _Entity.SupplierCode = string.IsNullOrEmpty(Supplier.Text) ? "" : Supplier.Text;
            _Entity.SupplierName = SupplierName.Text;
            String type = (Type.Value ?? String.Empty).ToString();
            _Entity.Type = type;
            _Entity.ReferenceDocnumber = RefDoc.Text;
            _Entity.ReferenceDate = RefDate.Text;
            _Entity.Remarks = Remarks.Text;
            _Entity.TotalGrossAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalGross.Text) ? "0" : TotalGross.Text);
            _Entity.TotalVatAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalVat.Text) ? "0" : TotalVat.Text);
            _Entity.TotalAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalAmt.Text) ? "0" : TotalAmt.Text);
            _Entity.Remarks = Remarks.Text;
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

            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);
                        if (Session["Datatable"] == "1")
                        {
                            //gv1.DataSource = GetSelectedVal();
                            gv1.UpdateEdit();//gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        else
                        {
                            //Change datasource for gridview to enable inserting
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Post();
                        Validate();
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

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                break;

                case "Supp":
                    SqlDataSource ds = Masterfilebiz;

                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo] where SupplierCode = '" + Supplier.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        SupplierName.Text = inb[0][1].ToString();
                    }
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo]");
                break;

                //case "Generate":
                //    GetSelectedVal();
                //break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "" ;
            string SizeCode = "";
            //foreach (GridViewColumn column in gv1.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "OrderQty")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //        //Checking for non existing Codes..
            //        ItemCode = e.NewValues["ItemCode"].ToString();
            //        ColorCode = e.NewValues["ColorCode"].ToString();
            //        ClassCode = e.NewValues["ClassCode"].ToString();
            //        SizeCode = e.NewValues["SizeCode"].ToString();
            //        DataTable item = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode +"'");
            //        if (item.Rows.Count < 1)
            //        {
            //            AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //        }
            //        DataTable color = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
            //        if (color.Rows.Count < 1)
            //        {
            //            AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //        }
            //        DataTable clss = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
            //        if (clss.Rows.Count < 1)
            //        {
            //            AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //        }
            //        DataTable size = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
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

            //if (Session["Datatable"] == "1" && check == true)
            //{
            //    e.Handled = true;
            //    DataTable source = GetSelectedVal();

            //    // Removing all deleted rows from the data source(Excel file)
            //    foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //    {
            //        try
            //        {
            //            object[] keys = { values.Keys[0], values.Keys[1] };
            //            source.Rows.Remove(source.Rows.Find(keys));
            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }
            //    }

            //    // Adding new rows
            //    foreach (ASPxDataInsertValues values in e.InsertValues)
            //    {
            //        var LineNumber = values.NewValues["LineNumber"];
            //        var TransDocNumber = values.NewValues["TransDocNumber"];
            //        var ItemCode = values.NewValues["ItemCode"];
            //        var ColorCode = values.NewValues["ColorCode"];
            //        var ClassCode = values.NewValues["ClassCode"];
            //        var SizeCode = values.NewValues["SizeCode"];
            //        var Quantity = values.NewValues["Quantity"];
            //        var Price = values.NewValues["Price"];
            //        var Amount = values.NewValues["Amount"];
            //        var Field1 = values.NewValues["Field1"];
            //        var Field2 = values.NewValues["Field2"];
            //        var Field3 = values.NewValues["Field3"];
            //        var Field4 = values.NewValues["Field4"];
            //        var Field5 = values.NewValues["Field5"];
            //        var Field6 = values.NewValues["Field6"];
            //        var Field7 = values.NewValues["Field7"];
            //        var Field8 = values.NewValues["Field8"];
            //        var Field9 = values.NewValues["Field9"];

            //        source.Rows.Add(LineNumber, TransDocNumber, ItemCode, ColorCode, ClassCode, SizeCode, Quantity, Price, Amount,
            //            Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
            //    }

            //    // Updating required rows
            //    foreach (ASPxDataUpdateValues values in e.UpdateValues)
            //    {
            //        object[] keys = { values.NewValues["TransDocNumber"], values.NewValues["LineNumber"] };
            //        DataRow row = source.Rows.Find(keys);
            //        row["TransDocNumber"] = values.NewValues["TransDocNumber"];
            //        row["ItemCode"] = values.NewValues["ItemCode"];
            //        row["ColorCode"] = values.NewValues["ColorCode"];
            //        row["ClassCode"] = values.NewValues["ClassCode"];
            //        row["SizeCode"] = values.NewValues["SizeCode"];
            //        row["Quantity"] = values.NewValues["Quantity"];
            //        row["Price"] = values.NewValues["Price"];
            //        row["Amount"] = values.NewValues["Amount"];
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

            //    Gears.RetriveData2("Delete from accounting.apmemodetail where docnumber = '" + txtDocNumber.Text + "'");

            //    foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
            //    {
            //        _EntityDetail.TransDocNumber = dtRow["TransDocNumber"].ToString();
            //        _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
            //        _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
            //        _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
            //        _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
            //        _EntityDetail.Quantity = Convert.ToDecimal(dtRow["Quantity"].ToString());
            //        _EntityDetail.Price = Convert.ToDecimal(dtRow["Price"].ToString());
            //        _EntityDetail.Amount = Convert.ToDecimal(dtRow["Amount"].ToString());
            //        _EntityDetail.Field1 = dtRow["Field1"].ToString();
            //        _EntityDetail.Field2 = dtRow["Field2"].ToString();
            //        _EntityDetail.Field3 = dtRow["Field3"].ToString();
            //        _EntityDetail.Field4 = dtRow["Field4"].ToString();
            //        _EntityDetail.Field5 = dtRow["Field5"].ToString();
            //        _EntityDetail.Field6 = dtRow["Field6"].ToString();
            //        _EntityDetail.Field7 = dtRow["Field7"].ToString();
            //        _EntityDetail.Field8 = dtRow["Field8"].ToString();
            //        _EntityDetail.Field9 = dtRow["Field9"].ToString();

            //        _EntityDetail.AddAPMemoDetail(_EntityDetail);
            //    }
            //}
        }

        //private DataTable GetSelectedVal()
        //{
        //    string val = "";
        //    DataTable dtble = Gears.RetriveData2("Select ReferenceNumber from accounting.apvoucher where docnumber = '"+RefDoc.Text+"'");
        //    foreach (DataRow dtrow in dtble.Rows)
        //    {
        //        val = dtrow[0].ToString();
        //    }
        //    gv1.DataSource = null;
        //    gv1.DataBind();

        //    gv1.DataSourceID = null;
            
        //    DataTable dt = new DataTable();
        //    string[] selectedValues = val.Split(';');
        //    CriteriaOperator selectionCriteria = new InOperator(RefDoc.KeyFieldName, selectedValues);
        //    RR.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["RRdet"] = RR.FilterExpression;
        //    gv1.DataSourceID = RR.ID;
        //    //if (gv1.DataSourceID != "")
        //    //{
        //    //    gv1.DataSourceID = null;
        //    //}
        //    gv1.DataBind();
        //    Session["Datatable"] = "1";

        //    foreach (GridViewColumn col in gv1.VisibleColumns)
        //    {
        //        GridViewDataColumn dataColumn = col as GridViewDataColumn;
        //        if (dataColumn == null) continue;
        //        dt.Columns.Add(dataColumn.FieldName);
        //    }
        //    for (int i = 0; i < gv1.VisibleRowCount; i++)
        //    {
        //        DataRow row = dt.Rows.Add();
        //        foreach (DataColumn col in dt.Columns)
        //            row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
        //    }

        //    dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
        //    dt.Columns["TransDocNumber"], dt.Columns["LineNumber"]};

        //    return dt;
        //}
        #endregion
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RRdet"] = null;
            }

            if (Session["RRdet"] != null)
            {
                gv1.DataSourceID = RR.ID;
                RR.FilterExpression = Session["RRdet"].ToString();
                //gridview.DataSourceID = null;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            ReferenceDoc.ConnectionString = Session["ConnString"].ToString();
            RR.ConnectionString = Session["ConnString"].ToString();
        }

    }
}