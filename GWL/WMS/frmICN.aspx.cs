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
    public partial class frmICN : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.ICN _Entity = new ICN();//Calls entity ICN
        Entity.ICN.ICNDetail _EntityDetail = new ICN.ICNDetail();//Call entity POdetails

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
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            deDocDate.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));
       
            
            if (!IsPostBack)
            {
               
                Session["itemsql"] = null;
                Connection = Session["ConnString"].ToString();

                switch (Request.QueryString["entry"].ToString())
                {
                     case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";

                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
 
                }

                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                     Warehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";
                  
                _Entity.getdata(txtDocnumber.Text, Connection);//ADD CONN

                    deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    deExpDelDate.Text = String.IsNullOrEmpty(_Entity.ExpectedDeliveryDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.ExpectedDeliveryDate).ToShortDateString();
                    dtpArrival.Text = String.IsNullOrEmpty(_Entity.ArrivalDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.ArrivalDate).ToShortDateString();
                    glWarehouseCode.Value = String.IsNullOrEmpty(_Entity.WarehouseCode) ? "" : _Entity.WarehouseCode;
                    gvPlant.Value = String.IsNullOrEmpty(_Entity.Plantcode) ? "" : _Entity.Plantcode;
                    glStorerKey.Value =String.IsNullOrEmpty(_Entity.StorerKey) ? "" : _Entity.StorerKey;
                    txtHTranstype.Text = _Entity.Transtype;
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
                    txtStatus.Text = _Entity.Status;
                    txtReqCoDept.Text = _Entity.CompanyDept;
                    txtConsignee.Text = _Entity.Consignee;
                    txtConAddr.Text = _Entity.ConsigneeAddr;
                    txtOvertime.Text = _Entity.Overtime;
                    txtTBSB.Text = _Entity.SuppliedBy;
                    //txtSpIns.Value = _Entity.SpecialInstruction;
                txtManpower.Value =String.IsNullOrEmpty(_Entity.NOmanpower)? 0:Convert.ToDecimal(_Entity.NOmanpower);
                    txtLoad.Value = String.IsNullOrEmpty(_Entity.LoadingTime) ? " " : Convert.ToDateTime(_Entity.LoadingTime).ToString();
                    txtShipType.Text = _Entity.ShipmentType;
                    txtRefDoc.Text = _Entity.RefDoc;
                    txtAddpower.Text = _Entity.AddtionalManpower;
                    txtTProvided.Text = _Entity.TruckingPro;
                    txtHTruckingCo.Text = _Entity.TrackingNO;
                    txtTruckType.Text = _Entity.TruckType;
                    //txtHSupplier.Text = _Entity.SupplierCode;
                    //txtHSpecialIns.Text = _Entity.SpecialInstruction;
                    txtHPlate.Text = _Entity.PlateNumber;
                    txtHDriverName.Text = _Entity.DriverName;

                    txtDRNo.Value = _Entity.DRNumber;
                    //txtContainerNo.Value = _Entity.ContainerNo;
                    //txtInvoice.Value = _Entity.InvoiceNo;
                    //txtSeal.Value = _Entity.SealNo;
        
                    DataTable dtfullname = Gears.RetriveData2("Select UserName from it.users where UserID='" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
                    DataRow _ret = dtfullname.Rows[0];
                    //txtStaff.Text = string.IsNullOrEmpty(_Entity.DocumentationStaff) ? _ret["UserName"].ToString().Replace(".", " ") : _Entity.DocumentationStaff;
                    //txtChecker.Value = _Entity.WarehouseChecker;
                    //dtpStart.Date = String.IsNullOrEmpty(_Entity.StartUnload) ? DateTime.Now : Convert.ToDateTime(_Entity.StartUnload);
                    //dtpComplete.Date = String.IsNullOrEmpty(_Entity.CompleteUnload) ? DateTime.Now : Convert.ToDateTime(_Entity.CompleteUnload);
                    txtdocstaf.Text = string.IsNullOrEmpty(_Entity.DocumentationStaff) ? _ret["UserName"].ToString().Replace(".", " ") : _Entity.DocumentationStaff;


                    DataTable checkCount = Gears.RetriveData2("Select DocNumber from wms.icndetail where docnumber = '" + txtDocnumber.Text + "'",
                        Connection);//ADD CONN
                    if (checkCount.Rows.Count > 0)		
                    {		
                        gv1.DataSourceID = "odsDetail";		
                    }		
                    else		
                    {
                        gv1.DataSourceID = "sdsDetail";
                    }

                //if (!string.IsNullOrEmpty(_Entity.LastEditedBy))//NEWADD
                //{
                //    updateBtn.Text = "Update";
                //}
                glcheck.Checked = true;
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Connection;
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();

            string strresult = GWarehouseManagement.ICN_Validate(gparam);

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
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (!IsPostBack && !IsCallback)
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
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (!IsPostBack && !IsCallback)
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    ASPxGridView grid = sender as ASPxGridView;
                    //grid.SettingsBehavior.AllowGroup = false;
                    //grid.SettingsBehavior.AllowSort = false;
                    e.Editor.ReadOnly = view;
                }
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
            if (!IsPostBack && !IsCallback)
            {
                if (Request.QueryString["entry"].ToString() == "V")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Update ||
                        e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                }
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
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
        public void cleardata() {



            gv1.DataSource = null;

            gv1.Columns.Clear();
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
        protected void OnComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)sender;
            string hexColor;
            // Check if the selected item is "Other"
            //if (comboBox.SelectedItem != null && comboBox.SelectedItem.Value.ToString() == "Other")
            //{
            //    // Enable text editing
            //    hexColor = "#FFFFFF"; // Replace with your desired hex color
            //    System.Drawing.Color convertedColor = System.Drawing.ColorTranslator.FromHtml(hexColor);
            //    txtSpecify.ReadOnly = false;
            //    txtSpecify.DisabledStyle.BackColor = convertedColor;
            //    comboBox.ClientEnabled = true;
            //}
            //else
            //{
            //    // Disable text editing
            //    hexColor = "#F9F9F9"; // Replace with your desired hex color
            //    System.Drawing.Color convertedColor = System.Drawing.ColorTranslator.FromHtml(hexColor);
            //    txtSpecify.ReadOnly = true;
            //    txtSpecify.DisabledStyle.BackColor = convertedColor;

            //    comboBox.ClientEnabled = false;
            //}
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

            if (val2 == "")
            {
                val2 = "0";
            }
    
             if (e.Parameters.Contains("ItemCode"))
            {
                DataTable getall = Gears.RetriveData2("Select top 1 ColorCode,ClassCode,SizeCode,FullDesc,isnull(StandardQty,0) as StandardQty,UnitBulk from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "' " +
                                                          "and isnull(a.IsInactive,0)=0", Session["ConnString"].ToString()); //ADD CONN
                foreach (DataRow dt in getall.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
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
            //else
            //{
                _Entity.DocNumber = txtDocnumber.Text;
                _Entity.Connection = Connection; //ADD CONN
                _Entity.DocDate = deDocDate.Text;
                _Entity.ExpectedDeliveryDate = deExpDelDate.Text;
                _Entity.ArrivalDate = dtpArrival.Text;
                _Entity.WarehouseCode = String.IsNullOrEmpty(glWarehouseCode.Text) ? null : glWarehouseCode.Value.ToString();
                _Entity.Plantcode = String.IsNullOrEmpty(gvPlant.Text) ? null : gvPlant.Value.ToString();
                _Entity.StorerKey = String.IsNullOrEmpty(glStorerKey.Text) ? null : glStorerKey.Value.ToString();
                _Entity.Transtype = txtHTranstype.Text;
                _Entity.Field1 = txtHField1.Text;
                _Entity.Field2 = txtHField2.Text;
                _Entity.Field3 = txtHField3.Text;
                _Entity.Field4 = txtHField4.Text;
                _Entity.Field5 = txtHField5.Text;
                _Entity.Field6 = txtHField6.Text;
                _Entity.Field7 = txtHField7.Text;
                _Entity.Field8 = txtHField8.Text;
                _Entity.Field9 = txtHField9.Text;
                //_Entity.SpecialInstruction = txtSpIns.Text;
                _Entity.DRNumber = txtDRNo.Text;
                _Entity.LoadingTime = txtLoad.Text;
                _Entity.Status=txtStatus.Text;
                _Entity.CompanyDept = txtReqCoDept.Text ;
                _Entity.Consignee = txtConsignee.Text ;
                _Entity.ConsigneeAddr = txtConAddr.Text ;
                _Entity.Overtime =txtOvertime.Text ;
                 _Entity.SuppliedBy=txtTBSB.Text;

                 _Entity.NOmanpower = String.IsNullOrEmpty(txtManpower.Value.ToString()) ? Convert.ToString(0): txtManpower.Value.ToString();
                _Entity.ShipmentType = txtShipType.Text;
                _Entity.RefDoc=txtRefDoc.Text ;
                 _Entity.AddtionalManpower=txtAddpower.Text;
                _Entity.TruckingPro=txtTProvided.Text ;
                _Entity.TrackingNO=txtHTruckingCo.Text ;
                _Entity.TruckType = String.IsNullOrEmpty(txtTruckType.Text) ? null : txtTruckType.Value.ToString();
                //_Entity.ContainerNo = txtContainerNo.Text;
                //_Entity.InvoiceNo = txtInvoice.Text;
                //_Entity.SealNo = txtSeal.Text;
                //_Entity.DocumentationStaff = txtStaff.Text;
                //_Entity.WarehouseChecker = txtChecker.Text;
                //_Entity.StartUnload = dtpStart.Text;
                //_Entity.CompleteUnload = dtpComplete.Text;
            
             
                //_Entity.SpecialInstruction = txtHSpecialIns.Text;
                _Entity.PlateNumber = txtHPlate.Text;
                _Entity.DriverName = txtHDriverName.Text;
              string strError="";
                switch (e.Parameter)
                {
                    case "Add":

                        if (error == false)
                        {
                            check = true;
                            //_Entity.InsertData(_Entity);//Method of inserting for header
                            _Entity.UpdateData(_Entity);
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                            gv1.UpdateEdit();//2nd Initiation to update grid
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
                      strError = Functions.Submitted(_Entity.DocNumber,"WMS.ICN",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                        
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
                            _Entity.LastEditedBy = Session["userid"].ToString();
                            _Entity.UpdateData(_Entity);//Method of Updating header
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                            gv1.UpdateEdit();//2nd Initiation to update grid
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
                         strError = Functions.Submitted(_Entity.DocNumber,"WMS.ICN",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                        
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }

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
                    case "WH":
                        CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { glWarehouseCode.Text });
                        Plant.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                        gvPlant.DataSourceID = "Plant";
                        gvPlant.DataBind();
                        break;
                 
                }
            //}
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
                if ((e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "") && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "Type"
                    && dataColumn.FieldName != "IncomingDocNumber" && dataColumn.FieldName != "IncomingDocType" && dataColumn.FieldName != "Remarks" && dataColumn.FieldName != "Price"
                    && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "BulkQty" && dataColumn.FieldName != "DocBaseQty" && dataColumn.FieldName != "DocBulkQty"
                    && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
                    && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "BarcodeNo" && dataColumn.FieldName != "ManufacturingDateICN" && dataColumn.FieldName != "ExpiryDateICN" && dataColumn.FieldName != "BatchNumberICN")
                {
                    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //Warehouse.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            //OCN.ConnectionString = Session["ConnString"].ToString();
            //Unit.ConnectionString = Session["ConnString"].ToString();
            //Plant.ConnectionString = Session["ConnString"].ToString();
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            //10/5/2016 emc add filter by customer
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gv1")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ItemCodeLookup_CustomCallback);
                if (Session["itemsql"] != null)
                {
                    Masterfileitem.SelectCommand = Session["itemsql"].ToString();
                    Masterfileitem.DataBind();
                }
                
                //gridLookup.GridView.DataSourceID = Masterfileitem.ID; 
                //gridLookup.GridView.DataSource = Masterfileitem;
                
            }
        }
     
        public void ItemCodeLookup_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //10/5/2016 emc add filter by customer
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;//Traps the callback
            ASPxGridView ItemcodeList = sender as ASPxGridView;
            string CustomerCode = e.Parameters.Split('|')[1];//Set Customer

            if (e.Parameters.Contains("ItemCodeDropDown"))
            {
        
                 DataTable getCustomer = Gears.RetriveData2("SELECT ISNULL(Field1,'') as  Field1 FROM Masterfile.BizPartner WHERE ISNULL(IsInactive,0)=0 AND BizPartnerCode = '"+CustomerCode+"' AND ISNULL(Field1,'')!='' ", Session["ConnString"].ToString()); //ADD CONN

                 if (getCustomer.Rows.Count>0)
                 {
                     CustomerCode = getCustomer.Rows[0][0].ToString();
                 }
                 

                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc],Customer FROM Masterfile.[Item] where isnull(IsInactive,'')=0 AND Customer IN ('"+CustomerCode+"') ";
                Session["itemsql"] = Masterfileitem.SelectCommand;
                //ItemcodeList.DataSource = Masterfileitem;
                ItemcodeList.DataBind();
            }
            
        }

     
    }
}