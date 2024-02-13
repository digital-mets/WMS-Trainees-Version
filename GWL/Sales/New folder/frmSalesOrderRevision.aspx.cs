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
using GearsSales;

namespace GWL
{
    public partial class frmSalesOrderRevision : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string strError;

        Entity.SalesOrderRevision _Entity = new SalesOrderRevision();//Calls entity odsHeader
        Entity.SalesOrderRevision.SalesOrderRevisionDetail _EntityDetail = new SalesOrderRevision.SalesOrderRevisionDetail();//Call entity sdsDetail

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
            gv1.KeyFieldName = "DocNumber;LineNumber";

            if (!IsPostBack)
            {

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                //glPRNumber.Value = _Entity.PRNumber.ToString();

                // Moved inside !IsPostBack   LGE 03 - 02 - 2016
                //V=View, E=Edit, N=New
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
                        glSODocNumber.Visible = true;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }



                glSODocNumber.Value = _Entity.SODocNumber.ToString();
                txtRemarks.Value = _Entity.Remarks.ToString();
                txtOldCustomerPONumber.Value = _Entity.OldCustomerPONumber.ToString();
                txtNewCustomerPONumber.Value = _Entity.NewCustomerPONumber.ToString();

                dtDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();


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

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    //gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    glSODocNumber.ClientEnabled = true;
                    //Generatebtn.ClientVisible = true;
                    //gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv1.Settings.VerticalScrollableHeight = 200;
                    //gv1.KeyFieldName = "LineNumber";
                }
                //else
                //{
                //    gv1.DataSourceID = "odsDetail";
                //}
                

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                //if (Request.QueryString["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = null;
                //}
                //DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM Sales.SalesOrderRevisionDetail WHERE DocNumber = '" + txtDocNumber.Text + "'");
                //gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

                DataTable dtbldetail = Gears.RetriveData2("SELECT DocNumber FROM Sales.SalesOrderRevisionDetail Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                if (dtbldetail.Rows.Count > 0)
                {
                    gv1.DataSourceID = odsDetail.ID;
                }
                else
                {
                    gv1.DataSourceID = null;
                }


                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

            }

            //if (gv1.DataSource != null)
            //{
            //    gv1.DataSourceID = null;
            //}


        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "SLSSOR";
            string strresult = GearsSales.GSales.SORevision_Validate(gparam);
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
        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientVisible = !view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;

            if (Request.QueryString["entry"].ToString() == "E")
            {
                //if (!String.IsNullOrEmpty(aglCustomer.Text))
                //{
                //    aglCustomer.DropDownButton.Enabled = false;
                //    aglCustomer.ReadOnly = true;
                //}
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D"
                || Request.QueryString["entry"].ToString() == "E" || Request.QueryString["entry"].ToString() == "N")
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
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }

            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New)
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
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
           
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[3], "glItemCode");
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
         

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN

            _Entity.DocDate = dtDocDate.Text;
            _Entity.SODocNumber = glSODocNumber.Text;
            _Entity.Remarks = txtRemarks.Text;
            _Entity.OldCustomerPONumber = txtOldCustomerPONumber.Text;
            _Entity.NewCustomerPONumber = txtNewCustomerPONumber.Text;
            
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

               // _Entity.Transtype = Request.QueryString["transtype"].ToString();

            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber,"Sales.SalesOrderRevision",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsSalesOrderRevisionDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else 
                        { 
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                      //  _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSourceID = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;



                case "Update":

                    gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber,"Sales.SalesOrderRevision",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                
                        if (Session["Datatable"] == "1")
                        {

                            Gears.RetriveData2("DELETE FROM Sales.SalesOrderRevisionDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            gv1.DataSourceID = sdsSalesOrderRevisionDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else 
                        { 
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                      //  _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSourceID = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;


                case "Delete":
                    //GetSelectedVal(); 
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Close":
                        cp.JSProperties["cp_close"] = true;
                        gv1.DataSourceID = null;
                    break;

                case "Generate":
                    GetSelectedVal();
                    SetText();
                    Generatebtn.ClientEnabled = false;
                    //if (!String.IsNullOrEmpty(glSODocNumber.Text))
                    //{
                    //    Generatebtn.ClientEnabled = false;
                    //}
                        break;

                //case "SODocNumberCase":
                //        gv1.DataSourceID = null;
                //        gv1.DataBind();
                //        break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            //if (error == false && check == false)
            //{
            //    foreach (GridViewColumn column in gv1.Columns)
            //    {
            //        GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //        if (dataColumn == null) continue;
            //        if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "LineNumber"
            //            && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3"
            //            && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6"
            //            && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9"
            //            && dataColumn.FieldName != "ItemCode" && dataColumn.FieldName != "ColorCode" && dataColumn.FieldName != "SizeCode"
            //            && dataColumn.FieldName != "ClassCode" && dataColumn.FieldName != "OldUnitPrice")
            //        {
            //            e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //        }
            //    }

                //Checking for non existing Codes..
                //ItemCode = e.NewValues["ItemCode"].ToString();
                //ColorCode = e.NewValues["ColorCode"].ToString();
                //ClassCode = e.NewValues["ClassCode"].ToString();
                //SizeCode = e.NewValues["SizeCode"].ToString();
                //DataTable item = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());//ADD CONN
                //if (item.Rows.Count < 1)
                //{
                //    AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
                //}
                //DataTable color = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'", Session["ConnString"].ToString());//ADD CONN
                //if (color.Rows.Count < 1)
                //{
                //    AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
                //}
                //DataTable clss = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'", Session["ConnString"].ToString());//ADD CONN
                //if (clss.Rows.Count < 1)
                //{
                //    AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
                //}
                //DataTable size = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'", Session["ConnString"].ToString());//ADD CONN
                //if (size.Rows.Count < 1)
                //{
                //    AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
                //}

                if (e.Errors.Count > 0)
                {
                    error = true; //bool to cancel adding/updating if true
                }
           // }
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
                //e.UpdateValues.Clear();
            }

             if (Session["Datatable"] == "1" && check == true)
             {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0], values.Keys[1]};
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = 
                    { 
                        values.NewValues["DocNumber"], 
                        values.NewValues["LineNumber"] 
                    };
                    DataRow row = source.Rows.Find(keys);
                    //row["SODocNumber"] = values.NewValues["SODocNumber"];
                   // row["ItemCode"] = values.NewValues["ItemCode"];
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["OldQty"] = values.NewValues["OldQty"];
                    row["NewQty"] = values.NewValues["NewQty"];
                    row["OldUnitPrice"] = values.NewValues["OldUnitPrice"];
                    row["NewUnitPrice"] = values.NewValues["NewUnitPrice"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["IsVAT"] = values.NewValues["IsVAT"];
                    row["VATCode"] = values.NewValues["VATCode"];
                    row["Rate"] = values.NewValues["Rate"];
                    row["DiscountRate"] = values.NewValues["DiscountRate"];
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
                //_EntityDetail.SODocNumber = dtRow["PRNumber"].ToString();
                _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();

                _EntityDetail.OldQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OldQty"]) ? 0 : dtRow["OldQty"]);
                _EntityDetail.NewQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewQty"]) ? 0 : dtRow["NewQty"]);

                _EntityDetail.OldUnitPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["OldUnitPrice"]) ? 0 : dtRow["OldUnitPrice"]);
                _EntityDetail.NewUnitPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewUnitPrice"]) ? 0 : dtRow["NewUnitPrice"]);

                _EntityDetail.Unit = dtRow["Unit"].ToString();
                _EntityDetail.IsVAT = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVAT"]) ? 0 : dtRow["IsVAT"]);
                _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                _EntityDetail.Rate = Convert.ToDecimal(Convert.IsDBNull(dtRow["Rate"]) ? 0 : dtRow["Rate"]);
                _EntityDetail.DiscountRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["DiscountRate"]) ? 0 : dtRow["DiscountRate"]);
               
                _EntityDetail.Field1 = dtRow["Field1"].ToString();
                _EntityDetail.Field2 = dtRow["Field2"].ToString();
                _EntityDetail.Field3 = dtRow["Field3"].ToString();
                _EntityDetail.Field4 = dtRow["Field4"].ToString();
                _EntityDetail.Field5 = dtRow["Field5"].ToString();
                _EntityDetail.Field6 = dtRow["Field6"].ToString();
                _EntityDetail.Field7 = dtRow["Field7"].ToString();
                _EntityDetail.Field8 = dtRow["Field8"].ToString();
                _EntityDetail.Field9 = dtRow["Field9"].ToString();
                _EntityDetail.AddSalesOrderRevisionDetail(_EntityDetail);
                }
            }
        }
        #endregion

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {
        }

        //protected void aglcustomerinit(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
        //    if (Session["customoutbound"] != null)
        //    {
        //        gridLookup.GridView.DataSource = sdsPicklist;
        //        sdsPicklist.FilterExpression = Session["customoutbound"].ToString();
        //    }
        //}

        //public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string whcode = e.Parameters;//Set column name
        //    if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

        //    ASPxGridView grid = sender as ASPxGridView;
        //    CriteriaOperator selectionCriteria = new InOperator(glContactPerson.KeyFieldName, new string[] { whcode });
        //    sdsPicklist.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["customoutbound"] = sdsPicklist.FilterExpression;
        //    grid.DataSource = sdsPicklist;
        //    grid.DataBind();
        //}
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["salesorderrevisiondetail"] = null;
            }

            if (Session["salesorderrevisiondetail"] != null)
            {
                //gv1.DataSourceID = null;
                gv1.DataSourceID = sdsSalesOrderRevisionDetail.ID;
                sdsSalesOrderRevisionDetail.FilterExpression = Session["salesorderrevisiondetail"].ToString();
            }
        }

        private DataTable GetSelectedVal()
        {
            //gv1.DataSource = null;
            gv1.DataSourceID = null;
            DataTable dt = new DataTable();
            string[] selectedValues = glSODocNumber.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(glSODocNumber.KeyFieldName, selectedValues);
            sdsSalesOrderRevisionDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["salesorderrevisiondetail"] = sdsSalesOrderRevisionDetail.FilterExpression;
            gv1.DataSource = sdsSalesOrderRevisionDetail;
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
            dt.Columns["DocNumber"],dt.Columns["LineNumber"]};

            //if (!String.IsNullOrEmpty(glSODocNumber.Text))
            //{
            //    glSODocNumber.ClientEnabled = false;
            //}

            glSODocNumber.ClientEnabled = false;


            return dt;
        }

        protected void SetText()
        {
            //SqlDataSource ds = SODocNumberlookup;
            //ds.SelectCommand = string.Format("Select DocNumber from Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'");
            //DataView sodocnum = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (sodocnum.Count > 0)
            //{
            //    glSODocNumber.Text = sodocnum[0][0].ToString();
            //}

            //ds.SelectCommand = string.Format("Select CustomerPONo,DocNumber from Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'");
            //DataView oldcustomerponumber = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (oldcustomerponumber.Count > 0)
            //{
            //    txtOldCustomerPONumber.Value = oldcustomerponumber[0][0].ToString();
            //}


            DataTable oldcustomerponumber = Gears.RetriveData2("Select DocNumber from Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in oldcustomerponumber.Rows)
            {
                glSODocNumber.Text = dt[0].ToString();
            }

            DataTable getSODocNum = Gears.RetriveData2("Select CustomerPONo from Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getSODocNum.Rows)
            {
                txtOldCustomerPONumber.Text = dt[0].ToString();
            }

        }
       

        protected void aglCustomer_ValueChanged(object sender, EventArgs e)
        {
            
        }

        protected void aglCustomer_TextChanged(object sender, EventArgs e)
        {
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtDocDate.Date = DateTime.Now;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            sdsItem.ConnectionString = Session["ConnString"].ToString();
            sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsPicklistDetail.ConnectionString = Session["ConnString"].ToString();
            SODocNumberlookup.ConnectionString = Session["ConnString"].ToString();
            OldCustomerPONumberlookup.ConnectionString = Session["ConnString"].ToString();
            sdsSalesOrderRevisionDetail.ConnectionString = Session["ConnString"].ToString();
   
        }
        
        
        
    }
}