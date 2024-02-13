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
    public partial class frmRoom : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Room _Entity = new Room();//Calls entity Room
 
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


            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }
            Plant.SelectCommand = "SELECT PlantCode, WarehouseCode, PlantDescription FROM Masterfile.Plant where warehousecode = '" + cmbWarehouseCode.Text +"'";
                
            if (!IsPostBack)
            {
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";

                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        break;
                }
                
                if (Request.QueryString["entry"].ToString() == "N"){}
                else
                {
                    txtRoomCode.ReadOnly = true;
                    txtRoomCode.Value = Request.QueryString["docnumber"].ToString();

                    _Entity.getdata(txtRoomCode.Text, Session["ConnString"].ToString());//ADD CONN
                    txtRoomDescription.Text = _Entity.RoomDescription;
                    cmbStorageType.Value = _Entity.StorageType;
                    cmbWarehouseCode.Value = _Entity.WarehouseCode;
                    Plant.SelectCommand = "SELECT PlantCode, WarehouseCode, PlantDescription FROM Masterfile.Plant where warehousecode = '" + cmbWarehouseCode.Text + "'";
                    cmbPlant.DataBind();
                    cmbPlant.Text = _Entity.PlantCode;
                    cmbStorerKey.Value = _Entity.CustomerCode;
                    seSideCountPerRoom.Text = _Entity.SideCountperRoom;
                    seRowCountPerRoom.Text = _Entity.RowCountperRoom;
                    seLevelCountPerRoom.Text = _Entity.LevelCountperRoom;
                    seTotalCountLocation.Text = _Entity.TotalCountLocation;
                    seMinTemperature.Text = _Entity.MinTemperature;
                    seMaxTemperature.Text = _Entity.MaxTemperature;
                    seMaxPalletCount.Text = _Entity.MaxPalletCount;
                    seMaxBaseQty.Text = _Entity.MaxBaseQty;
                    seMaxBulkQty.Text = _Entity.MaxBulkQty;
                    chkIsInactive.Value = _Entity.IsInactive;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;
                }
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.RoomCode;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
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
            if (Session["LocationExp"] != null)
            {
                gridLookup.GridView.DataSource = Plant;
                Plant.FilterExpression = Session["LocationExp"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string PC = e.Parameters;//Set column name
            if (PC.Contains("GLP_AIC") || PC.Contains("GLP_AC") || PC.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns[0], "glSalesSubsiCode");
            CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { PC });
            Plant.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["LocationExp"] = Plant.FilterExpression;
            grid.DataSource = Plant;
            grid.DataBind();
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.RoomCode = txtRoomCode.Text;
            _Entity.WarehouseCode = String.IsNullOrEmpty(cmbWarehouseCode.Text) ? null : cmbWarehouseCode.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(cmbStorerKey.Text) ? null : cmbStorerKey.Text;
            _Entity.PlantCode = String.IsNullOrEmpty(cmbPlant.Text) ? null : cmbPlant.Text;
            _Entity.RoomDescription = txtRoomDescription.Text;
            _Entity.StorageType = String.IsNullOrEmpty(cmbStorageType.Text) ? null : cmbStorageType.Text;
            _Entity.SideCountperRoom = String.IsNullOrEmpty(seSideCountPerRoom.Text) ? "0" : seSideCountPerRoom.Text;
            _Entity.RowCountperRoom = String.IsNullOrEmpty(seRowCountPerRoom.Text) ? "0" : seRowCountPerRoom.Text;
            _Entity.LevelCountperRoom = String.IsNullOrEmpty(seLevelCountPerRoom.Text) ? "0" :seLevelCountPerRoom.Text ;
            //_Entity.TotalCountLocation = String.IsNullOrEmpty(txtTotalCountLocation.Text) ? "0" :txtTotalCountLocation.Text ;
            _Entity.TotalCountLocation = Convert.ToString(Convert.ToInt64(_Entity.SideCountperRoom) * Convert.ToInt64(_Entity.LevelCountperRoom) * Convert.ToInt64(_Entity.RowCountperRoom));
            _Entity.MinTemperature = String.IsNullOrEmpty(seMinTemperature.Text) ? "0" :seMinTemperature.Text ;
            _Entity.MaxTemperature = String.IsNullOrEmpty(seMaxTemperature.Text) ? "0" :seMaxTemperature.Text ;
            _Entity.MaxPalletCount = String.IsNullOrEmpty(seMaxPalletCount.Text) ? "0" : seMaxPalletCount.Text;
            _Entity.MaxBaseQty = String.IsNullOrEmpty(seMaxBaseQty.Text) ? "0" : seMaxBaseQty.Text;
            _Entity.MaxBulkQty = String.IsNullOrEmpty(seMaxBulkQty.Text) ? "0" : seMaxBulkQty.Text;
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeActivatedBy = txtDeActivatedBy.Text;
            _Entity.DeActivatedDate = txtDeActivatedDate.Text;
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
                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();
                       
                        _Entity.InsertData(_Entity);
                        //Validate();
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
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header
                        //Validate();
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
                    check = true;
                    cp.JSProperties["cp_delete"] = true;
                    _Entity.DeleteData(_Entity);
                        cp.JSProperties["cp_message"] = "Successfully Deleted!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
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
                    cmbPlant.DataBind();
                    break;
            }
        }
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

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glCustomerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtPlant_TextChanged(object sender, EventArgs e)
        {

        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            Warehouse.ConnectionString = Session["ConnString"].ToString();
            Plant1.ConnectionString = Session["ConnString"].ToString();
            Plant.ConnectionString = Session["ConnString"].ToString();
            StorerKey.ConnectionString = Session["ConnString"].ToString();
            StorageType.ConnectionString = Session["ConnString"].ToString();
        }
    }
}