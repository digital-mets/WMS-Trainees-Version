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
    public partial class frmBillingNonStorage : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        
        private static string Connection;


        Entity.BillingNonStorage _Entity = new BillingNonStorage();//Calls entity ICN
        Entity.BillingNonStorage.BillingNonStorageDetail _EntityDetail = new BillingNonStorage.BillingNonStorageDetail();//Call entity POdetails

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
	    Gears.UseConnectionString(Session["ConnString"].ToString());

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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }
            
            deDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());
            
            if (!IsPostBack)
            {
                Connection = (Session["ConnString"].ToString());
                Session["RRses"] = null;
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        //glcheck.ClientVisible = false;
                        //Generatebtn.ClientVisible = true;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //txtDocnumber.ReadOnly = true;
                        //Generatebtn.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        Generatebtn.ClientVisible = false;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                       Generatebtn.ClientVisible = false;
                        break;
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                   //gv1.DataSourceID = "odsDetail";
                    //_Entity.getdata(txtDocnumber.Text);
                    _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN
                    deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    gvBizPartnerCode.Value = _Entity.BizPartnerCode.ToString();
                    gvWarehouse.Value = _Entity.WarehouseCode.ToString();
                    gvProfit.Value = _Entity.ProfitCenterCode;
                    txtContractNumber.Text = _Entity.ContractNumber;
                    txtBillingPeriodType.Text = _Entity.BillingPeriodType;
                    dtpDateFrom.Text = String.IsNullOrEmpty(_Entity.DateFrom) ? Convert.ToDateTime(DateTime.Now).ToShortDateString() : Convert.ToDateTime(_Entity.DateFrom).ToShortDateString();

                    dtpDateTo.Text = String.IsNullOrEmpty(_Entity.DateTo) ? Convert.ToDateTime(DateTime.Now).ToShortDateString() : Convert.ToDateTime(_Entity.DateTo).ToShortDateString();
                    
                    gvServiceType.Value = _Entity.ServiceType.ToString();
                    txtBillingStatement.Text = _Entity.BillingStatement;
                    txtTotalAmount.Value = _Entity.TotalAmount;
                    txtTotalVat.Value = _Entity.TotalVat;
                   
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

                    //var formLayout = frmlayout1 as ASPxFormLayout;
                    //var layoutItem = formLayout.FindItemOrGroupByName("Field1");
                    //layoutItem.Caption = "Testssss";

                DataTable checkCount = Gears.RetriveData2("Select DocNumber from wms.billingotherservicedetail where docnumber = '" + txtDocnumber.Text + "'",
    Connection);//ADD CONN
                if (checkCount.Rows.Count > 0)
                {
                    gv1.DataSourceID = "odsDetail";
                    string sql = "Select Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9 from Masterfile.WMSServiceType where ServiceType = '" + gvServiceType.Text + "' ";
                    DataTable dtWMSServiceType = Gears.RetriveData2(sql, Session["ConnString"].ToString());
                    if (dtWMSServiceType.Rows.Count > 0)
                    {
                        for (int i = 1; i <= 9; i++)
                        {
                            string r1 = dtWMSServiceType.Rows[0][i - 1].ToString();
                            gv1.Columns["Field" + i.ToString()].Caption = r1;
                            gv1.Columns["Field" + i.ToString()].Width = String.IsNullOrEmpty(r1) ? 0 : 170;
                        }
                    }

                    //gv1.DataBind();
                }
                else
                {
                    gv1.DataSourceID = "sdsDetail";
                }

                gvJournal.DataSourceID = "odsJournalEntry";


                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = null;
                //}
                //else if (Request.QueryString["iswithdetail"].ToString() == "true" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "odsDetail";
                //    string sql = "Select Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9 from Masterfile.WMSServiceType where ServiceType = '" + gvServiceType.Text + "' ";
                //    DataTable dtWMSServiceType = Gears.RetriveData2(sql, Session["ConnString"].ToString());
                //    if (dtWMSServiceType.Rows.Count > 0)
                //    {
                //        for (int i = 1; i <= 9; i++)
                //        {
                //            string r1 = dtWMSServiceType.Rows[0][i - 1].ToString();
                //            gv1.Columns["Field" + i.ToString()].Caption = r1;
                //            gv1.Columns["Field" + i.ToString()].Width = String.IsNullOrEmpty(r1) ? 0 : 170;
                //        }
                //    }

                //    gv1.DataBind();
                //}
                //if (Request.QueryString["entry"].ToString() == "V")
                //{
                //    gv1.DataSourceID = "odsDetail";

                //}
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSBOS";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GWarehouseManagement.BillingNonStorage_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSBOS";
            gparam._Table = "WMS.BillingOtherService";
            gparam._Connection = Session["ConnString"].ToString();
            gparam._Factor = -1;
            string strresult = GearsWarehouseManagement.GWarehouseManagement.BOS_Post(gparam);
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
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string column = e.Parameters.Split('|')[0];//Set column name
            //if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            //string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            //string val = e.Parameters.Split('|')[2];//Set column value
            //if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            //ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
            //Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
            //grid.DataSourceID = "Masterfileitemdetail";
            //grid.DataBind();

            //for (int i = 0; i < grid.VisibleRowCount; i++)
            //{
            //    if (grid.GetRowValues(i, column) != null)
            //        if (grid.GetRowValues(i, column).ToString() == val)
            //        {
            //            grid.Selection.SelectRow(i);
            //            string key = grid.GetRowValues(i, column).ToString();
            //            grid.MakeRowVisible(key);
            //            break;
            //        }
            //}
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.BizPartnerCode = String.IsNullOrEmpty(gvBizPartnerCode.Text) ? null : gvBizPartnerCode.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(gvWarehouse.Text) ? null : gvWarehouse.Value.ToString();
            _Entity.ProfitCenterCode = String.IsNullOrEmpty(gvProfit.Text) ? null : gvProfit.Value.ToString();
            _Entity.ContractNumber = txtContractNumber.Text;
            _Entity.BillingPeriodType = txtBillingPeriodType.Text;
            _Entity.DocDate = String.IsNullOrEmpty(deDocDate.Text) ? null : Convert.ToDateTime(deDocDate.Text).ToShortDateString();
            _Entity.DateFrom = String.IsNullOrEmpty(dtpDateFrom.Text) ? null : Convert.ToDateTime(dtpDateFrom.Text).ToShortDateString();
            _Entity.DateTo = String.IsNullOrEmpty(dtpDateTo.Text) ? null : Convert.ToDateTime(dtpDateTo.Text).ToShortDateString();
            _Entity.ServiceType = String.IsNullOrEmpty(gvServiceType.Text) ? null : gvServiceType.Value.ToString();
            _Entity.BillingStatement = txtBillingStatement.Text;
            _Entity.TotalAmount = String.IsNullOrEmpty(txtTotalAmount.Text) ? 0 : Convert.ToDecimal(txtTotalAmount.Text);
            _Entity.TotalVat = String.IsNullOrEmpty(txtTotalVat.Text) ? 0 : Convert.ToDecimal(txtTotalVat.Text);
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
                  //string strError = Functions.Submitted(_Entity.DocNumber,"WMS.BillingOtherService",1,Connection);//NEWADD factor 1 if submit, 2 if approve

                  //  gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                  //  if (error == false)
                  //  {
                  //      check = true;
                  //      //_Entity.InsertData(_Entity);//Method of inserting for header
                  //      _Entity.UpdateData(_Entity);
                  //      gv1.DataSourceID = "odsDetail";
                  //      odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                  //      gv1.UpdateEdit();//2nd initiation to insert grid
                  //      Validate();
                  //      cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                  //      cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                  //      cp.JSProperties["cp_close"] = true;//Close window variable to client side
                  //      Session["Refresh"] = "1";
                  //  }
                  //  else
                  //  {
                  //      cp.JSProperties["cp_message"] = "Please check all the fields!";
                  //      cp.JSProperties["cp_success"] = true;
                  //  }

                    //break;

                case "Update":
                  string strError1 = Functions.Submitted(_Entity.DocNumber,"WMS.BillingOtherService",1,Connection);//NEWADD factor 1 if submit, 2 if approve

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();//2nd Initiation to update grid

                        Gears.RetriveData2("Delete from WMS.BillingOtherServiceDetail where DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());

                        DataTable cnt = Gears.RetriveData2("SELECT COUNT(*) as Row FROM WMS.BillingOtherServiceDetail WHERE DocNumber ='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());

                        DataRow _cnt = cnt.Rows[0];
                        if (_cnt["Row"].ToString() == "0")
                        {

                            DataTable source = GenerateBillingOtherService();

                

                            foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                            {

                                _EntityDetail.Field1 = dtRow["Field1"].ToString();
                                _EntityDetail.Field2 = dtRow["Field2"].ToString();
                                _EntityDetail.Field3 = dtRow["Field3"].ToString();
                                _EntityDetail.Field4 = String.IsNullOrEmpty(dtRow["Field4"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Field4"].ToString());
                                _EntityDetail.Field5 = String.IsNullOrEmpty(dtRow["Field5"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Field5"].ToString());
                                _EntityDetail.Field6 = String.IsNullOrEmpty(dtRow["Field6"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Field6"].ToString());
                                _EntityDetail.Field7 = String.IsNullOrEmpty(dtRow["Field7"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Field7"].ToString());
                                _EntityDetail.Field8 = dtRow["Field8"].ToString();
                                _EntityDetail.Field9 = dtRow["Field9"].ToString();
                                _EntityDetail.AddBillingNonStorageDetail(_EntityDetail);
                            }
                        }
                        Post();
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

                case "Generate":
                    GenerateBillingOtherService();
                    cp.JSProperties["cp_generated"] = true;
                    break;

                case "RR":
                    SqlDataSource ds = Masterfileitem;
                    ////ds.SelectCommand = string.Format(" SELECT top 1 A.BizPartnerCode, A.DocNumber,A.BillingPeriodType,B.ServiceType,C.ProfitCenterCode,B.Vatable,B.ServiceRate,A.WarehouseCode FROM WMS.Contract A INNER JOIN WMS.ContractDetail B ON A.Docnumber = B.Docnumber RIGHT JOIN Masterfile.BPCustomerInfo C ON A.BizPartnerCode = C.BizPartnerCode LEFT JOIN Masterfile.WMSBillingPeriod D on A.BillingPeriodType = D.BillingPeriodCode where A.BizPartnerCode = '" + gvBizPartnerCode.Text + "' AND B.ServiceType = '" + gvServiceType.Text + "' AND ISNull(SubmittedBy,'') != '' order by A.Docnumber desc");
                    //ds.SelectCommand = string.Format(" SELECT top 1 A.BizPartnerCode, A.DocNumber,COALESCE(A.BillingPeriodType,B.Period) AS BillingPeriodType,B.ServiceType,C.ProfitCenterCode,B.Vatable,B.ServiceRate,A.WarehouseCode FROM WMS.Contract A INNER JOIN WMS.ContractDetail B ON A.Docnumber = B.Docnumber RIGHT JOIN Masterfile.BPCustomerInfo C ON A.BizPartnerCode = C.BizPartnerCode LEFT JOIN Masterfile.WMSBillingPeriod D on A.BillingPeriodType = D.BillingPeriodCode where A.BizPartnerCode = '" + gvBizPartnerCode.Text + "' AND B.ServiceType = '" + gvServiceType.Text + "' AND ISNull(SubmittedBy,'') != '' AND A.Status = 'ACTIVE' ORDER BY A.Docnumber, A.EffectivityDate DESC");
                    ds.SelectCommand = string.Format(" EXEC [sp_generate_BillingNonStorage] '" + gvBizPartnerCode.Text + "','" + gvServiceType.Text + "','" + deDocDate.Text + "','" + gvWarehouse.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtContractNumber.Value = tran[0][1].ToString();
                        txtBillingPeriodType.Value = tran[0][2].ToString();
                        gvProfit.Value = tran[0][4].ToString();
                       

                    }
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            //foreach (GridViewColumn column in gv1.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    //if (dataColumn == null) continue;
            //    //if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "OrderQty")
            //    //{
            //    //    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    //}
            //    //Checking for non existing Codes..
            //    ItemCode = e.NewValues["ItemCode"].ToString();
            //    ColorCode = e.NewValues["ColorCode"].ToString();
            //    ClassCode = e.NewValues["ClassCode"].ToString();
            //    SizeCode = e.NewValues["SizeCode"].ToString();
            //    DataTable item = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'");
            //    if (item.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //    }
            //    DataTable color = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
            //    if (color.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //    }
            //    DataTable clss = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
            //    if (clss.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //    }
            //    DataTable size = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
            //    if (size.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
            //    }
            //}

            //if (e.Errors.Count > 0)
            //{
            //    error = true; //bool to cancel adding/updating if true
            //}
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
            if (Session["Datatable"] == "1" && error == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0] };
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
                    object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);

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


                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = Convert.ToDecimal(dtRow["Field4"].ToString());
                    _EntityDetail.Field5 = Convert.ToDecimal(dtRow["Field5"].ToString());
                    _EntityDetail.Field6 = Convert.ToDecimal(dtRow["Field6"].ToString());
                    _EntityDetail.Field7 = Convert.ToDecimal(dtRow["Field7"].ToString());
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();

                    _EntityDetail.AddBillingNonStorageDetail(_EntityDetail);
                }
            }
        }
        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            string[] selectedValues = gvBizPartnerCode.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(gvBizPartnerCode.KeyFieldName, selectedValues);
            sdsBOSDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["RRses"] = sdsBOSDetail.FilterExpression;
            gv1.DataSourceID = sdsBOSDetail.ID;
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
            dt.Columns["DocNumber"]};


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
                gv1.DataSource = sdsBOSDetail;
                sdsBOSDetail.FilterExpression = Session["RRses"].ToString();
                //gridview.DataSourceID = null;
            }
        }
        private DataTable GenerateBillingOtherService()
        {
            DataTable billing = Gears.RetriveData2("EXEC sp_GenerateWMSTransactionBOS '" + gvServiceType.Text + "','" + gvBizPartnerCode.Text + "','" + dtpDateFrom.Text + "','" + dtpDateTo.Text + "','" + gvWarehouse.Text + "'", Session["ConnString"].ToString());
            //DataTable gen = new DataTable();
            gv1.DataSource = billing;
          //  Session["Datatable"] = "1";
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }

            string sql = "Select Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9 from Masterfile.WMSServiceType where ServiceType = '" + gvServiceType.Text + "' ";
            DataTable dtWMSServiceType = Gears.RetriveData2(sql, Session["ConnString"].ToString());
            if (dtWMSServiceType.Rows.Count > 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    gv1.Columns["Field" + (i + 1).ToString()].Width = 170;
                    string r1 = dtWMSServiceType.Rows[0][i].ToString();
                    if(String.IsNullOrEmpty(r1))
                    {
                        gv1.Columns["Field" + (i + 1).ToString()].Width = 0;
                    }
                    gv1.Columns["Field" + (i+1).ToString()].Caption = r1;
                }
            }
            gv1.DataBind();
            return billing;
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
            //// (the e.Parameter is the callback item that we sent to the server).
            //     ds.SelectCommand = string.Format("SELECT OutgoingDocType FROM WMS.[ICN] WHERE DocNumber = '" + e.Parameter + "'");
            // //This is where we now initiate the command and get the data from it using dataview	
            //     DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //     if (biz.Count > 0)
            //     {
            // //Now, this is the part where we assign the following data to the textbox
            //         //txttype.Text = biz[0][0].ToString();
            //     }
        }



        protected void gvServiceType_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtBillingPeriodType_TextChanged(object sender, EventArgs e)
        {

        }

        protected void deDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                //deDocDate.Date = DateTime.Now;
            }
        }

        protected void dtpDateFrom_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
               // dtpDateFrom.Date = DateTime.Now;
            }
        }

        protected void dtpDateTo_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDateTo.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            sdsWarehouse.ConnectionString = Session["ConnString"].ToString();

            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            ProfitCenterCode.ConnectionString = Session["ConnString"].ToString();
            ServiceType.ConnectionString = Session["ConnString"].ToString();
            ContractNumber.ConnectionString = Session["ConnString"].ToString();
            BillingPeriodType.ConnectionString = Session["ConnString"].ToString();
            sdsBOSDetail.ConnectionString = Session["ConnString"].ToString();


        }

    }
}