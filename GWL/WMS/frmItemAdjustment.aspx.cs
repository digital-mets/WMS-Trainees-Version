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
    public partial class frmItemAdjustment : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.ItemAdjustment _Entity = new ItemAdjustment();//Calls entity ICN
        Entity.ItemAdjustment.ItemAdjustmentDetail _EntityDetail = new ItemAdjustment.ItemAdjustmentDetail();//Call entity POdetails

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
            //gv1.DataSource = null;
            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Session["wmsitemadjtab"] = null;
                Session["FilterExpression"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                   case "N":
                        updateBtn.Text = "Update";
                        txtDocnumber.ReadOnly = true;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocnumber.ReadOnly = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;

                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }


                txtDocnumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //    //gv1.DataSourceID = "sdsDetail";
                //    popup.ShowOnPageLoad = false;
                //    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}
                //else
                //{
                    //gv1.DataSourceID = "odsDetail";
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());
                    dtpdocdate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    txtwarehousecode.Value = _Entity.WarehouseCode;
                    cmbStorerKey.Value = _Entity.CustomerCode;
                    txttotaladjustment.Text = _Entity.TotalAdjustment.ToString();
                    txtAdjustmentType.Value = _Entity.AdjustmentType;
                    txtremarks.Text = _Entity.Remarks;
                
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

                //}
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}

                    DataTable dtbldetail = Gears.RetriveData2("select DocNumber from wms.ItemAdjustmentdetail where docnumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());
                    gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
            }

            if (Session["wmsitemadjtab"] == null)
            {
                string sb = "select b.TransDoc DocNumber,ROW_NUMBER() OVER (ORDER BY b.TransDoc,b.TransLine,b.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode,b.PalletID) as LineNumber,b.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode," +
                            " b.PalletID,b.PalletID ToPalletID," +
                            " SUM(ISNULL(b.RemainingBulkQty,0)-ISNULL(b.PickedBulkQty,0)-ISNULL(ReservedBulkQty,0)) BulkQty," +
                            " SUM(ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0)) Qty," +
                            " b.Location as FromLoc,'' ToLoc,'' StatusCode, Customer,'' LotID,d.WarehouseCode,UnitBase,UnitBulk," +
                            " MfgDate,ExpirationDate,BatchNumber from wms.CountSheetSetup b" +
                            " with (nolock)" +
                            " left join masterfile.Item c" +
                            " on b.ItemCode = c.ItemCode" +
                            " left join masterfile.Location d" +
                            " on b.Location = d.LocationCode" +
                            " where isnull(b.PutAwayDate,'')!=''" +
                            " and isnull(b.submitteddate,'')!=''" +
                            " and (ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0) > 0" +
                            " OR ISNULL(b.RemainingBulkQty,0)-ISNULL(b.PickedBulkQty,0)-ISNULL(ReservedBulkQty,0) > 0 )" +
                            " group by BatchNumber,MfgDate,ExpirationDate,b.Transdoc,b.TransLine,Customer,b.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode,b.Location,b.PalletID,d.WarehouseCode,UnitBase,UnitBulk" +
                            " order by b.Transdoc,b.TransLine,b.palletid";
                DataTable countsheet = Gears.RetriveData2(sb, Session["ConnString"].ToString());
                Session["wmsitemadjtab"] = countsheet;
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
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
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
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gvProductOrder") && !Request.Params["__CALLBACKID"].Contains("glItemCode") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                if (Session["wmsIAFilterExpression"] != null)
                {
                    if (Session["wmsIAchecker"] == "Color")
                    {
                        gridLookup.GridView.DataSourceID = "sdsColor";
                        sdsColor.SelectCommand = Session["wmsIAFilterExpression"].ToString();
                    }
                    else if (Session["wmsIAchecker"] == "Class")
                    {
                        gridLookup.GridView.DataSourceID = "sdsClass";
                        sdsClass.SelectCommand = Session["wmsIAFilterExpression"].ToString();
                    }
                    else if (Session["wmsIAchecker"] == "Size")
                    {
                        gridLookup.GridView.DataSourceID = "sdsSize";
                        sdsSize.SelectCommand = Session["wmsIAFilterExpression"].ToString();
                    }
                    else
                    {
                        gridLookup.GridView.DataSourceID = "sdsItemDetail";
                        sdsItemDetail.SelectCommand = "SELECT B.ItemCode, ColorCode, ClassCode,SizeCode,UnitBase AS Unit,FullDesc, UnitBulk AS BulkUnit FROM Masterfile.[Item] A INNER JOIN Masterfile.[ItemDetail] B ON A.ItemCode = B.ItemCode where isnull(A.IsInactive,0)=0";
                        sdsItemDetail.FilterExpression = Session["wmsIAFilterExpression"].ToString();
                    }
                }
            }
        }

        protected void lookup_Init2(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression2"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsLocation";
                sdsLocation.FilterExpression = Session["FilterExpression2"].ToString();
                //Session["FilterExpression"] = null;
            }
            else
            {
                gridLookup.GridView.DataSourceID = "sdsLocation";
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
            else if (e.Parameters.Contains("LocationCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { itemcode });
                sdsLocation.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression2"] = sdsLocation.FilterExpression;
                grid.DataSourceID = "sdsLocation";
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
            else if (e.Parameters.Contains("ColorCode"))
            {
                sdsColor.SelectCommand = string.Format("SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0)=0 and ItemCode = '{0}'", itemcode);
                Session["wmsIAFilterExpression"] = sdsColor.SelectCommand;
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;
                //CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                //sdsColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["wmsIAchecker"] = "Color";
                //Session["wmsIAFilterExpression"] = sdsColor.FilterExpression;
                grid.DataSourceID = "sdsColor";
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
            else if (e.Parameters.Contains("ClassCode"))
            {
                sdsClass.SelectCommand = string.Format("SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0 and ItemCode = '{0}'", itemcode);
                Session["wmsIAFilterExpression"] = sdsClass.SelectCommand;
                ASPxGridView grid1 = sender as ASPxGridView;
                var selectedValues1 = itemcode;
                //CriteriaOperator selectionCriteria1 = new InOperator("ItemCode", new string[] { itemcode });
                //sdsClass.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1)).ToString();
                Session["wmsIAchecker"] = "Class";
                //Session["wmsIAFilterExpression"] = sdsClass.FilterExpression;
                grid1.DataSourceID = "sdsClass";
                grid1.DataBind();

                for (int i = 0; i < grid1.VisibleRowCount; i++)
                {
                    if (grid1.GetRowValues(i, column) != null)
                        if (grid1.GetRowValues(i, column).ToString() == val)
                        {
                            grid1.Selection.SelectRow(i);
                            string key = grid1.GetRowValues(i, column).ToString();
                            grid1.MakeRowVisible(key);
                            break;
                        }
                }
            }
            else if (e.Parameters.Contains("SizeCode"))
            {
                sdsSize.SelectCommand = string.Format("SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0) = 0 and ItemCode = '{0}'", itemcode);
                Session["wmsIAFilterExpression"] = sdsSize.SelectCommand;
                ASPxGridView grid2 = sender as ASPxGridView;
                var selectedValues2 = itemcode;
                //CriteriaOperator selectionCriteria2 = new InOperator("ItemCode", new string[] { itemcode });
                //sdsSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria2)).ToString();
                Session["wmsIAchecker"] = "Size";
                //Session["wmsIAFilterExpression"] = sdsSize.FilterExpression;
                grid2.DataSourceID = "sdsSize";
                grid2.DataBind();

                for (int i = 0; i < grid2.VisibleRowCount; i++)
                {
                    if (grid2.GetRowValues(i, column) != null)
                        if (grid2.GetRowValues(i, column).ToString() == val)
                        {
                            grid2.Selection.SelectRow(i);
                            string key = grid2.GetRowValues(i, column).ToString();
                            grid2.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode"))
            {
                string test = Request.Params["__CALLBACKID"];
                ASPxGridLookup gridLookup = sender as ASPxGridLookup;
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                FilterItem();
                gridLookup.DataBind();
            }
        }
        public void FilterItem()
        {
            Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] A INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsAsset,0) = 0 ORDER BY ItemCode ASC";
        }
        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterItem();
            grid.DataSourceID = "Masterfileitem";
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
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpdocdate.Text;
          
            _Entity.WarehouseCode = txtwarehousecode.Text;
            _Entity.TransType = Request.QueryString["transtype"].ToString();
            _Entity.CustomerCode = cmbStorerKey.Text;
            _Entity.TotalAdjustment = Convert.ToDecimal(txttotaladjustment.Text);
            _Entity.AdjustmentType = txtAdjustmentType.Text;
            _Entity.Remarks = txtremarks.Text;
                 
            //_Entity.DocNumber = txtDocnumber.Text;
            //_Entity.DocDate = deDocDate.Value.ToString();
            //_Entity.ExpectedDeliveryDate = deExpDelDate.Value.ToString();
            //_Entity.StorerKey = glStorerKey.Text;
            //_Entity.WarehouseCode = glWarehouseCode.Text;
            _Entity.Connection = Session["ConnString"].ToString();
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
            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();//2nd initiation to insert grid

                        Validate();
                        cp.JSProperties["cp_message1"] = "Successfully Added!";//Message variable to client side
                     
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                //case "Update":

                
                //    gv1.UpdateEdit();


                //    if (error == false)
                //    {
                //        check = true;
                //        _Entity.UpdateData(_Entity);//Method of inserting for header

                //        if (Session["Datatable"] == "1")
                //        {
                //            gv1.UpdateEdit();
                //        }
                //        else
                //        {
                //            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                //            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                //            gv1.UpdateEdit();//2nd initiation to insert grid
                //        }
                //        Validate();
                //        cp.JSProperties["cp_message"] = "Successfully Updated!";
                //        cp.JSProperties["cp_success"] = true;
                //        cp.JSProperties["cp_close"] = true;
                //        Session["Refresh"] = "1";
                //    }

                //    else
                //    {
                //        cp.JSProperties["cp_message"] = "Please check all the fields!";
                //        cp.JSProperties["cp_success"] = true;
                //    }
                //    break;

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

                case "refgrid":
                    gv1.DataBind();
                    break;
            }
        }

        protected void gv1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                ASPxGridView grid = sender as ASPxGridView;
                double total = 0;
                for (int i = 0; i < grid.VisibleRowCount; i++)
                    if (grid.IsGroupRow(i))
                        total += Convert.ToDouble(grid.GetGroupSummaryValue(i, grid.GroupSummary["Qty"]));
                e.TotalValue = total;
                e.TotalValueReady = true;
                txttotaladjustment.Text = total.ToString();
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
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "Reason" && dataColumn.FieldName != "StorageType"
            //    && dataColumn.FieldName != "StorageSection" && dataColumn.FieldName != "StorageBin"  && dataColumn.FieldName != "StatusCode"
            //    && dataColumn.FieldName != "BarcodeNo" && dataColumn.FieldName != "PalletNumber" && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3"
            //    && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "BaseQty" && dataColumn.FieldName != "BulkQty" && dataColumn.FieldName != "BulkUnit"
            //    && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //    //Checking for non existing Codes..
            //    ItemCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ItemCode"])) ? "" : Convert.ToString(e.NewValues["ItemCode"]);
            //    ColorCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ColorCode"])) ? "" :  Convert.ToString(e.NewValues["ColorCode"]);
            //    ClassCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ClassCode"])) ? "" : Convert.ToString( e.NewValues["ClassCode"]);
            //    SizeCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["SizeCode"])) ? "" : Convert.ToString( e.NewValues["SizeCode"]);

            //    DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'");
            //    if (item.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //    }
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
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;   
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
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
       gparam._TransType = "WMSIA";

       string strresult = GWarehouseManagement.ItemAdjustment_Validate(gparam);

       cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

   }
   #endregion
        protected void Connection_Init(object sender, EventArgs e)
        {
            //Session["userid"] = "1828";
            //Session["ConnString"] = "Data Source=192.168.201.115;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;connection timeout=1800;";
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
           // sdsDetail.ConnectionString = Session["ConnString"].ToString();
           // Masterfileitem.ConnectionString = Session["ConnString"].ToString();
           // Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
           //Warehouse.ConnectionString = Session["ConnString"].ToString();
           //    ItemAdjustment.ConnectionString = Session["ConnString"].ToString();
           // Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
           // Unit.ConnectionString = Session["ConnString"].ToString();
           //     UnitBase.ConnectionString = Session["ConnString"].ToString();
           //     StoragesType.ConnectionString = Session["ConnString"].ToString();
           //     sdsLocation.ConnectionString = Session["ConnString"].ToString();
           //     StorerKey.ConnectionString = Session["ConnString"].ToString();
        }

        protected void gvExtract_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
                case "Pal":
                    if (string.IsNullOrEmpty(txtCustomer.Text) && string.IsNullOrEmpty(txtItem.Text) &&
                        string.IsNullOrEmpty(txtLocation.Text) && string.IsNullOrEmpty(txtPalletID.Text))
                    {
                        gvExtract.JSProperties["cp_error"] = "No input to search for!";
                    }
                    else
                    {
                        GetSelectedVal();
                        gvExtract.Selection.SelectAll();
                    }
                    break;
            }
        }

        private void GetSelectedVal()
        {
            gvExtract.DataSource = null;
            string f_expression = GetFilterExpression();
            DataTable dt = Session["wmsitemadjtab"] as DataTable;
            DataView dv = new DataView(dt);
            dv.RowFilter = f_expression.ToUpper().Trim();
            //dv.RowFilter = "[CUSTOMER] LIKE '%C-M011%'";
            gvExtract.DataSource = dv;
            gvExtract.DataBind();
        }

        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(txtCustomer.Text))
                lst_operator.Add(new BinaryOperator("Customer", string.Format("%{0}%", txtCustomer.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtItem.Text))
                lst_operator.Add(new BinaryOperator("ItemCode", string.Format("%{0}%", txtItem.Text.Trim()), BinaryOperatorType.Like));

            //if (!string.IsNullOrEmpty(txtLot.Text))
            //    lst_operator.Add(new BinaryOperator("LotId", string.Format("%{0}%", txtLot.Text), BinaryOperatorType.Like));
            if (!string.IsNullOrEmpty(txtwarehousecode.Text))
                lst_operator.Add(new BinaryOperator("WarehouseCode", string.Format("%{0}%", txtwarehousecode.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtLocation.Text))
                lst_operator.Add(new BinaryOperator("FromLoc", string.Format("%{0}%", txtLocation.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtPalletID.Text))
                lst_operator.Add(new BinaryOperator("PalletID", string.Format("%{0}%", txtPalletID.Text.Trim()), BinaryOperatorType.Like));

            if (lst_operator.Count > 0)
            {
                CriteriaOperator[] op = new CriteriaOperator[lst_operator.Count];
                for (int i = 0; i < lst_operator.Count; i++)
                    op[i] = lst_operator[i];
                CriteriaOperator res_operator = new GroupOperator(GroupOperatorType.And, op);
                res_str = res_operator.ToString();
            }

            return res_str;
        } 
      
    }
}