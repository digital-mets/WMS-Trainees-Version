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
    public partial class frmLocation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

       
        Entity.Location _Entity = new Location();//Calls entity odsHeader
        Entity.Location.LocationDetail _EntityDetail = new Location.LocationDetail();
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            PlantCode.SelectCommand = "SELECT PlantCode, PlantDescription from Masterfile.Plant where WarehouseCode = '" + txtwarehousecode.Text +"'";
            RoomCode.SelectCommand = "SELECT RoomCode, RoomDescription from Masterfile.Room where PlantCode = '" + gvPlant.Text + "'";

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
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }
            if (!IsPostBack)
            {
                Session["locdetail"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
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
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }



                if (Request.QueryString["entry"].ToString() == "N")
                {

                    gv1.DataSourceID = "sdsDetail";

                    //popup.ShowOnPageLoad = false;
                }

               
                    txtLocation.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    _Entity.getdata(txtLocation.Text, Session["ConnString"].ToString());//ADD CONN //Method for retrieving data from entity

                    txtLocation.Text = _Entity.LocationCode;
                    txtDesc.Text = _Entity.LocationDescription;

                    txtwarehousecode.Value = _Entity.WarehouseCode;

                    PlantCode.SelectCommand = "SELECT PlantCode, PlantDescription from Masterfile.Plant where WarehouseCode = '" + txtwarehousecode.Text + "'";
                    gvPlant.DataBind();
                    gvPlant.Value = _Entity.PlantCode;
                    RoomCode.SelectCommand = "SELECT RoomCode, RoomDescription from Masterfile.Room where PlantCode = '" + gvPlant.Text + "'";
                    gvRoom.DataBind();
                    gvRoom.Value = _Entity.RoomCode;
                    gvLocType.Value = _Entity.LocationType;
                    txtOnHandBulk.Text = _Entity.OnHandBulkQty.ToString();
                    txtOnHandBase.Text = _Entity.OnHandBaseQty.ToString();
                    txtMinweight.Text = _Entity.MinWeight.ToString();
                    txtCurrentPal.Text = _Entity.CurrentPalletCount;
                    txtMaxBulk.Text = _Entity.MaxBulkQty.ToString();
                    txtMaxBase.Text = _Entity.MaxBaseQty.ToString();
                    txtBaseUnit.Text = _Entity.OnHandBaseUnit;
                    txtMaxPal.Text = _Entity.MaxPalletCount;
                    txtStorageType.Value = _Entity.StorageType;
                    gvCustomer.Value = _Entity.CustomerCode;
                    gvItemCode.Value = _Entity.ItemCode;
                    chkReplenish.Value = _Entity.Replenish;
                    chkIsInactive.Value = _Entity.IsInactive;
                    txtABC.Value = _Entity.ABC;
                   
                    txtPriority.Text = _Entity.Priority.ToString();
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeActivatedBy;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;

                    //set the gv1 datasource
                    //gv1.DataSourceID = "odsDetail";

                    //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                    // this can fix the issue of clonenode
                    DataTable checkCount = Gears.RetriveData2("Select DocNumber from Masterfile.LocationDetail where docnumber = '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                    if (checkCount.Rows.Count > 0)
                    {

                      
                        gv1.DataSourceID = "odsDetail";
                    }
                    else
                    {
                        //sdsDetail.SelectParameters["DocNumber"].DefaultValue =
                        //          gv1.DataBind();
                        gv1.DataSourceID = "sdsDetail";
                  
                    }
            }

            
        }
        #endregion

        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "REFLOC";

            //string strresult =GWarehouseManagement.Location_Validate(gparam);
            

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox text = sender as ASPxCheckBox;
            text.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            //Control look = (Control)sender;
            //((ASPxGridLookup)look).ReadOnly = view;
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

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.LocationCode = txtLocation.Text;
            _Entity.LocationDescription = txtDesc.Text;
            _Entity.WarehouseCode = txtwarehousecode.Text;
            _Entity.PlantCode = String.IsNullOrEmpty(gvPlant.Text) ? null : gvPlant.Value.ToString();
            _Entity.RoomCode = String.IsNullOrEmpty(gvRoom.Text) ? null : gvRoom.Value.ToString();
            _Entity.LocationType = String.IsNullOrEmpty(gvLocType.Text) ? null : gvLocType.Value.ToString();
            _Entity.StorageType = String.IsNullOrEmpty(txtStorageType.Text) ? null : txtStorageType.Value.ToString();
            _Entity.OnHandBulkQty = String.IsNullOrEmpty(txtOnHandBulk.Text) ? 0 : Convert.ToDecimal(txtOnHandBulk.Text);
            _Entity.OnHandBaseQty = String.IsNullOrEmpty(txtOnHandBase.Text) ? 0 : Convert.ToDecimal(txtOnHandBase.Text);
            _Entity.MinWeight = String.IsNullOrEmpty(txtMinweight.Text) ? 0 : Convert.ToDecimal(txtMinweight.Text);
            _Entity.OnHandBaseUnit = txtBaseUnit.Text;
            _Entity.CurrentPalletCount = txtCurrentPal.Text;
            _Entity.MaxBulkQty = String.IsNullOrEmpty(txtMaxBulk.Text) ? 0 : Convert.ToDecimal(txtMaxBulk.Text);
            _Entity.MaxBaseQty = String.IsNullOrEmpty(txtMaxBase.Text) ? 0 : Convert.ToDecimal(txtMaxBase.Text);
            _Entity.MaxPalletCount = txtMaxPal.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(gvCustomer.Text) ? null : gvCustomer.Value.ToString();
            _Entity.ItemCode = String.IsNullOrEmpty(gvItemCode.Text) ? null : gvItemCode.Value.ToString();
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Checked);
            _Entity.Replenish = Convert.ToBoolean(chkReplenish.Checked);
            _Entity.ABC = String.IsNullOrEmpty(txtABC.Text) ? null : txtABC.Value.ToString();
           
            _Entity.Priority = String.IsNullOrEmpty(txtPriority.Text) ? 0 : Convert.ToInt16(txtPriority.Text);
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
            //_Entity.ActivatedBy = txtActivatedBy.Text;
            //_Entity.ActivatedDate = txtActivatedDate.Text;
            //_Entity.DeActivatedBy = txtDeActivatedBy.Text;
            //_Entity.DeActivatedDate = txtDeActivatedDate.Text;


            switch (e.Parameter)
            {
                case "Add":

                    if (error == false)
                    {
                        check = true;
                        //_Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.InsertData(_Entity);
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

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of Updating header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = Request.QueryString["docnumber"].ToString();//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
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
                case "wh":
                    gvPlant.DataBind();
                    break;
                case "plant":
                    gvRoom.DataBind();
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            foreach (GridViewColumn column in gv1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if ((e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "") && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "LocationCode" && dataColumn.FieldName != "WareHouseCode" && dataColumn.FieldName != "CustomerCode")
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

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["locdetail"] = null;
            }

        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        
   
#endregion
    }
}