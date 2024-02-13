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
    public partial class frmInternalTransfer : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.InternalTransfer _Entity = new InternalTransfer();//Calls entity ICN
        Entity.InternalTransfer.InternalTransferDetail _EntityDetail = new InternalTransfer.InternalTransferDetail();//Call entity POdetails

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

            dtpdocdate.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));
       

            if (!IsPostBack)
            {
                Random rand = new Random();
                ViewState["_pageID"] = rand.Next().ToString();

                Session["wmsitemadjtab"] = null;
                Session["FilterExpression"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
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

                Warehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";
                
                

                
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
                dtpdocdate.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                //txtwarehousecode.Value = _Entity.WarehouseCode;
                //cmbStorerKey.Value = _Entity.CustomerCode;
                //txttotaladjustment.Text = _Entity.TotalAdjustment.ToString();
                //txtAdjustmentType.Value = _Entity.AdjustmentType;
                //txtremarks.Text = _Entity.Remarks;

                //txtHField1.Text = _Entity.Field1;
                //txtHField2.Text = _Entity.Field2;
                //txtHField3.Text = _Entity.Field3;
                //txtHField4.Text = _Entity.Field4;
                //txtHField5.Text = _Entity.Field5;
                //txtHField6.Text = _Entity.Field6;
                //txtHField7.Text = _Entity.Field7;
                //txtHField8.Text = _Entity.Field8;
                //txtHField9.Text = _Entity.Field9;
                glWarehouseCode.Value = _Entity.WarehouseCode;
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtCustomer.Value = _Entity.CustomerCode;
                //}
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}

                //DataTable dtbldetail = Gears.RetriveData2("select DocNumber from wms.ItemAdjustmentdetail where docnumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());
                //gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");


                DataTable getDet = Gears.RetriveData2("SELECT A.DocNumber FROM WMS.InternalTransferDetail A  WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

                //DataTable getDet = Gears.RetriveData2("SELECT * FROM WMS.ServiceProcessingDetail WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN
                if (getDet.Rows.Count > 0)
                {
                    dtpdocdate.ClientEnabled = false;
                    txtCustomer.ClientEnabled = false;

                }
            }
            if (txtCustomer.Text != "")
            {
                RRNum.SelectCommand = "select DocNumber,CustomerCode from wms.Inbound where CustomerCode = '" + txtCustomer.Text + "' and isnull(submittedby,'')!='' and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] A INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsAsset,0) = 0 AND Customer = '" + txtCustomer.Text + "' ORDER BY ItemCode ASC";
            }
            txtRR.DataBind();
            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[5], "glItemCode");
            lookup.DataBind();


            
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

        }

        protected void lookup_Init2(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);

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

            //_Entity.DocNumber = txtDocnumber.Text;
            //_Entity.DocDate = dtpdocdate.Text;

            //_Entity.WarehouseCode = txtwarehousecode.Text;
            //_Entity.TransType = Request.QueryString["transtype"].ToString();
            // _Entity.CustomerCode = cmbStorerKey.Text;
            //_Entity.TotalAdjustment = Convert.ToDecimal(txttotaladjustment.Text);
            //_Entity.AdjustmentType = txtAdjustmentType.Text;
            //_Entity.Remarks = txtremarks.Text;

            //_Entity.DocNumber = txtDocnumber.Text;
            //_Entity.DocDate = deDocDate.Value.ToString();
            //_Entity.ExpectedDeliveryDate = deExpDelDate.Value.ToString();
            //_Entity.StorerKey = glStorerKey.Text;
            _Entity.WarehouseCode = glWarehouseCode.Text;
            _Entity.CustomerCode = txtCustomer.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.DocDate = dtpdocdate.Text;
            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    //if (error == false)
                    //{
                    //check = true;
                    //_Entity.InsertData(_Entity);//Method of inserting for header
                    gv1.UpdateEdit();//2nd initiation to insert grid
                    //cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                    cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

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

                    DataTable dttableDetail = Gears.RetriveData2("SELECT Docnumber FROM WMS.InternalTransferDetail WHERE Docnumber='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());


                    if (dttableDetail.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_message"] = "Unable to delete Internal Transfer with Transaction Detail record!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;


                    }

                    cp.JSProperties["cp_delete"] = true;
                    break;
                case "ConfDelete":
                    Gears.RetriveData2("DELETE FROM WMS.InternalTransfer WHERE Docnumber='" + txtDocnumber.Text + "' ", Session["ConnString"].ToString());

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
                //txttotaladjustment.Text = total.ToString();
            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
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
            DataTable updtable = new DataTable();
            e.Handled = true;
            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                updtable.Columns.Add(dataColumn.FieldName);
            }

            updtable.Columns.Add("AddedBy");

            foreach (ASPxDataUpdateValues values in e.UpdateValues)
            {
                updtable.Rows.Add(new Object[]{
                            "WMSIT",
                            "",
                            "",
                            values.NewValues["RecordId"],
                            values.NewValues["OldItemCode"],
                            values.NewValues["ItemCode"],
                            values.NewValues["OldBatchNumber"],
                            values.NewValues["BatchNumber"],
                            values.NewValues["OldManufacturingDate"],
                            values.NewValues["ManufacturingDate"],
                            values.NewValues["OldExpiryDate"],
                            values.NewValues["ExpiryDate"],
                            values.NewValues["OldPalletID"],
                            values.NewValues["PalletID"],
                            values.NewValues["Qty"],
                            values.NewValues["BulkQty"],
                            //values.NewValues["Field1"],
                            //values.NewValues["Field2"],
                            //values.NewValues["Field3"],
                            //values.NewValues["Field4"],
                            //values.NewValues["Field5"],
                            //values.NewValues["Field6"],
                            //values.NewValues["Field7"],
                            //values.NewValues["Field8"],
                            //values.NewValues["Field9"],
                            Session["userid"].ToString(),
                            values.NewValues["OldClientName"],
                            values.NewValues["ClientName"],
                            });
            }

            GWarehouseManagement._connectionString = Session["ConnString"].ToString();
            string message = GWarehouseManagement.OpenFunction(txtDocnumber.Text, updtable);
            if (!string.IsNullOrEmpty(message))
            {
                
                //Gears.RetriveData2("delete from wms.InternalTransferDetail where DocNumber = '" + txtDocnumber.Text + "' and isnull(Status,'')!='S'", Session["ConnString"].ToString());
                if (message.Contains("Consider making"))
                {
                    callbacker.JSProperties["cp_success"] = true;
                    callbacker.JSProperties["cp_message"] = message;
                    callbacker.JSProperties["cp_message2"] = "Successfully Submitted!";
                }
                else
                {
                    callbacker.JSProperties["cp_success"] = false;
                    callbacker.JSProperties["cp_message"] = message;
                }
            }
            else
            {
                callbacker.JSProperties["cp_success"] = true;
                callbacker.JSProperties["cp_message"] = "Successfully Submitted!";
            }


            //foreach (DataRow dtRow in updtable.Rows)//This is where the data will be inserted into db
            //{
            //    _EntityDetail.RecordId = Convert.ToInt16(dtRow["RecordId"].ToString());
            //    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
            //    _EntityDetail.ExpiryDate = Convert.ToDateTime(dtRow["ExpiryDate"].ToString());
            //    _EntityDetail.BatchNumber = dtRow["BatchNumber"].ToString();
            //    _EntityDetail.ManufacturingDate = Convert.ToDateTime(dtRow["ManufacturingDate"].ToString());
            //    _EntityDetail.PalletID = dtRow["PalletID"].ToString();
            //    _EntityDetail.Field1 = dtRow["Field1"].ToString();
            //    _EntityDetail.Field2 = dtRow["Field2"].ToString();
            //    _EntityDetail.Field3 = dtRow["Field3"].ToString();
            //    _EntityDetail.Field4 = dtRow["Field4"].ToString();
            //    _EntityDetail.Field5 = dtRow["Field5"].ToString();
            //    _EntityDetail.Field6 = dtRow["Field6"].ToString();
            //    _EntityDetail.Field7 = dtRow["Field7"].ToString();
            //    _EntityDetail.Field8 = dtRow["Field8"].ToString();
            //    _EntityDetail.Field9 = dtRow["Field9"].ToString();

            //    _EntityDetail.InsertDataDetail(_EntityDetail);
            //}
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._Connection = Session["ConnString"].ToString();
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSIT";

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
            //switch (e.Parameters)
            //{
            //    case "Pal":
            //        if (string.IsNullOrEmpty(txtCustomer.Text)
            //            //&& string.IsNullOrEmpty(txtItem.Text) &&
            //            //string.IsNullOrEmpty(txtLocation.Text) && 
            //            &&string.IsNullOrEmpty(txtPalletID.Text))
            //        {
            //            gvExtract.JSProperties["cp_error"] = "No input to search for!";
            //        }
            //        else
            //        {
            //            GetSelectedVal();
            //            gvExtract.Selection.SelectAll();
            //        }
            //        break;
            //}
        }

        private void GetSelectedVal()
        {
            gv1.DataSource = null;
            string f_expression = GetFilterExpression().Replace("[A.ItemCode]", "A.ItemCode");
             f_expression = GetFilterExpression().Replace("[A.WarehouseCode]", "A.WarehouseCode");
            string getRR = "";
            if (!string.IsNullOrEmpty(txtRR.Text.Trim()))
            {
                getRR = " where TransDoc = '" + txtRR.Text.Trim() + "' ";
            }
            string sql = "select '' DocNumber,'WMSIT' TransType,'' LineNumber,A.RecordId,A.ItemCode as OldItemCode,A.ItemCode, A.PalletID as OldPalletID,A.PalletID,A.BatchNumber as OldBatchNumber,A.BatchNumber as BatchNumber , " +
                 "A.MfgDate as OldManufacturingDate,A.MfgDate as ManufacturingDate, " +
                 "A.ExpirationDate as OldExpiryDate,A.ExpirationDate as ExpiryDate,ClientName as ClientName,ClientName as OldClientName, " +
                 "A.CustomerCode,A.WarehouseCode,A.Location,SUM(A.RemainingBaseQty) Qty,SUM(A.RemainingBulkQty) BulkQty " +
                 " from WMS.ItemLocation A " +
                 "with (nolock) left join masterfile.Item c on A.ItemCode = c.ItemCode " +
                 "inner join masterfile.Location B " +
                 "on A.Location = B.LocationCode " +
                 "and A.WarehouseCode = B.WarehouseCode " +
                 " left join (SELECT DISTINCT TransDoc,A.WarehouseCode as WarehouseCodeCSS,ISNULL(BatchNumber,'') as BatchNumber,A.ItemCode as ItemCodeCSS,A.Location as LocationCSS,MfgDate,ExpirationDate,PalletID as PalletCSS,RRDate,A.Field2 as ClientName  " +
                             "FROM wms.countsheetsetup A with (nolock) " +
                              "INNER JOIN Masterfile.Location B " +
                              "ON A.Location = B.LocationCode " +
                              "AND A.WarehouseCode = B.WarehouseCode " +
                              "INNER JOIN MAsterfile.Item C " +
                              "ON A.ITemCode = C.ItemCode " +
                             "WHERE ISNULL(PutawayDate,'')!='' and  (RemainingBaseQty > 0 OR RemainingBulkQty > 0) and  " + f_expression + " " +
                             ") as inb " +
                             " on A.PalletID = inb.PalletCSS " +
                             " and ISNULL(A.BatchNumber,'') = ISNULL(inb.BatchNumber,'') " +
                             " and A.ItemCode = inb.ItemCodeCSS " +
                             " and A.Location = inb.LocationCSS" +
                              " and A.WarehouseCode = inb.WarehouseCodeCSS" +
                             " and A.MfgDate = inb.MfgDate " +
                             " and A.ExpirationDate = inb.ExpirationDate " +
                             " AND A.RRDate = inb.RRDate " +
                 "where ISNULL(A.PickedBaseQty,0) = 0 and ISNULL(A.ReservedBaseQty,0) = 0 and (A.RemainingBaseQty > 0 OR A.RemainingBulkQty > 0) " +
                //"   " + getRR +
                 " and " + f_expression + " "+
                 "group by A.RecordId,A.BatchNumber,A.MfgDate,A.ExpirationDate,A.CustomerCode,A.ItemCode,A.PalletID,A.WarehouseCode,A.Location,ClientName " +
               "  order by RecordId desc,palletid ";

            DataTable dt = Gears.RetriveData2(sql, Session["ConnString"].ToString());
            //Session["WMSIT_temptable"] = dt;
            gv1.DataSource = dt;
            gv1.DataBind();
        }

        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(txtCustomer.Text))
                lst_operator.Add(new BinaryOperator("Customer", string.Format("{0}", txtCustomer.Text.Trim()), BinaryOperatorType.Equal));

            if (!string.IsNullOrEmpty(txtItem.Text))
                lst_operator.Add(new BinaryOperator("A.ItemCode", string.Format("%{0}%", txtItem.Text.Trim()), BinaryOperatorType.Like));


            //if (!string.IsNullOrEmpty(txtRR.Text))
            //    lst_operator.Add(new BinaryOperator("inb.Docnumber", string.Format("%{0}%", txtRR.Text.Trim()), BinaryOperatorType.Like));


            if (!string.IsNullOrEmpty(txtRR.Text))
                lst_operator.Add(new BinaryOperator("TransDoc", string.Format("%{0}%", txtRR.Text.Trim()), BinaryOperatorType.Like));
            //if (!string.IsNullOrEmpty(txtLot.Text))
            ////    lst_operator.Add(new BinaryOperator("LotId", string.Format("%{0}%", txtLot.Text), BinaryOperatorType.Like));
            if (!string.IsNullOrEmpty(glWarehouseCode.Text))
                lst_operator.Add(new BinaryOperator("A.WarehouseCode", string.Format("%{0}%", glWarehouseCode.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtLocation.Text))
                lst_operator.Add(new BinaryOperator("Location", string.Format("%{0}%", txtLocation.Text.Trim()), BinaryOperatorType.Like));

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

        protected void rpFilter_Load(object sender, EventArgs e)
        {
            ASPxRoundPanel pnl = sender as ASPxRoundPanel;
            pnl.Visible = !view;
        }

        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
                case "CustChanged":
                    gv1.DataSource = null;
                    gv1.DataBind();
                    break;
                
                case "bind":
                    GetSelectedVal();
                    break;

            }
            
        }
        protected void dtpdocdate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {

                dtpdocdate.Date = DateTime.Now;
            }
        }

        protected void callbacker_Callback(object source, CallbackEventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpdocdate.Text;
            _Entity.WarehouseCode = glWarehouseCode.Text;
            _Entity.CustomerCode = txtCustomer.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.DocDate = dtpdocdate.Text;
            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    //if (error == false)
                    //{
                    //check = true;
                    _Entity.UpdateData(_Entity);//Method of inserting for header
                    gv1.UpdateEdit();//2nd initiation to insert grid
                    //cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                    callbacker.JSProperties["cp_close"] = true;//Close window variable to client side
                    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
            }
        }

    }
}