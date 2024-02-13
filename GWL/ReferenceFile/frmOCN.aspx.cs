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
    public partial class frmOCN : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.OCN _Entity = new OCN();//Calls entity ICN
        Entity.OCN.OCNDetail _EntityDetail = new OCN.OCNDetail();//Call entity POdetails

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

            if (!IsPostBack)
            {
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Update";
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

                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //    //Edit By Luis Genel T. Edpao 12/23/2015
                //    //gv1.DataSourceID = "sdsDetail";
                //    popup.ShowOnPageLoad = false;
                //    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}
                //else
                //{
                    //Edit By Luis Genel T. Edpao 12/23/2015
                    //gv1.DataSourceID = "odsDetail";

                    _Entity.getdata(txtDocnumber.Text);

                    dtpdocdate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    txtwarehousecode.Value = _Entity.WarehouseCode;
                    txtplantcode.Value = _Entity.PlantCode;
                     if (_Entity.StatusCode.Contains("N")) {
                         txtstatuscode.Text = "NEW";
                     }
                     if (_Entity.StatusCode.Contains("O"))
                     {
                         txtstatuscode.Text = "OUT";
                     }
                     if (_Entity.StatusCode.Contains("P"))
                     {
                         txtstatuscode.Text = "PICKED";
                     }
                     if (_Entity.StatusCode.Contains("X"))
                     {
                         txtstatuscode.Text = "CANCELLED";
                     }
                    cmbStorerKey.Value = _Entity.StorerKey;
                    dtptargetDate.Text = String.IsNullOrEmpty(_Entity.TargetDate) ? "" : Convert.ToDateTime(_Entity.TargetDate).ToShortDateString();
                    dtpdeliveryDate.Text = _Entity.DeliveryDate ;

                 

                    if (_Entity.PickType=="FR")
                    {
                        txtpickType.Text = "Pick From Reserved";
                    }
                    if (_Entity.PickType=="N")
                    {
                        txtpickType.Text = "Pick From Normal";
                    }
                    txtAddress.Text = _Entity.DeliverToAddress;
                    txtName.Text = _Entity.DeliverToName;
                    txtTruckingCompany.Text = _Entity.TruckingCompany;
                    txtPlateNumber.Text = _Entity.PlateNumber;
                    txtDriverName.Text = _Entity.DriverName;
                    txtinstruction.Text = _Entity.Instruction;
                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;

                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
               // }

                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}

                    DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM WMS.OCNDetail WHERE DocNumber = '"+ txtDocnumber.Text +"'");
                    gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail":"sdsDetail");
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            //gparam._Factor = 1;
            // gparam._Action = "Validate";
            //here
            string strresult = GWarehouseManagement.OCN_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
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
            }
            else { gridLookup.GridView.DataSourceID = "Masterfileitemdetail"; }
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
            catch(Exception)
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
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'");
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
                DataTable getqty = Gears.RetriveData2("Select isnull(StandardQty,0) as StandardQty from masterfile.item where itemcode = '" + itemcode + "'");
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

                    itemlookup.JSProperties["cp_codes"] = totalqty+";";
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
            _Entity.DocDate = dtpdocdate.Text;
            //_Entity.OutgoingDocNumber = txtOutgoing.Text;
            //_Entity.OutgoingDocType = txtDocType.Text;
            
            _Entity.WarehouseCode = txtwarehousecode.Text;
            _Entity.PlantCode = txtplantcode.Text;
            if (txtstatuscode.Text=="NEW") { _Entity.StatusCode = "N"; }
            if (txtstatuscode.Text == "OUT") { _Entity.StatusCode = "O"; }
            if (txtstatuscode.Text == "PICKED") { _Entity.StatusCode = "P"; }
            if (txtstatuscode.Text == "CANCELLED") { _Entity.StatusCode = "X"; }
            _Entity.StorerKey = cmbStorerKey.Text;
            _Entity.TargetDate = dtptargetDate.Text;
            _Entity.DeliveryDate = dtpdeliveryDate.Text;


            if (txtpickType.Text == "Pick From Reserved")
            {
                _Entity.PickType = "FR";
            }
            else
            {
                _Entity.PickType = "N";
            }

       

                    

             _Entity.DeliverToAddress = txtAddress.Text; 
              _Entity.DeliverToName = txtName.Text;
            _Entity.TruckingCompany = txtTruckingCompany.Text; 
              _Entity.PlateNumber = txtPlateNumber.Text;
              _Entity.DriverName = txtDriverName.Text;
            _Entity.Instruction = txtinstruction.Text;

            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
           _Entity.Instruction = txtinstruction.Text;

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

                    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
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

                        _Entity.UpdateData(_Entity);//Method of Updating header
                        _Entity.LastEditedBy = Session["userid"].ToString();
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
                    CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { txtwarehousecode.Text });
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

        protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glCustomerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dtpdocdate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpdocdate.Date = DateTime.Now;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            Warehouse.ConnectionString = Session["ConnString"].ToString();
            Bizpartner.ConnectionString = Session["ConnString"].ToString();
            StorerKey.ConnectionString = Session["ConnString"].ToString();
            Unit.ConnectionString = Session["ConnString"].ToString();
            UnitBase.ConnectionString = Session["ConnString"].ToString();
            Plantsql.ConnectionString = Session["ConnString"].ToString();

        }
    }
}