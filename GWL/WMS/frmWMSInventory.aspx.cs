﻿using DevExpress.Web;
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
    public partial class frmWMSInventory : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.WMSInventory _Entity = new WMSInventory();//Calls entity odsHeader

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
                        updateBtn.Text = "Add";
                        glcheck.ClientVisible = false;
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
                    //popup.ShowOnPageLoad = false;
                }
                else
                {
                    txtId.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    //_Entity.getdata(txtId.Text); //Method for retrieving data from entity
                    _Entity.getdata(txtId.Text, Session["ConnString"].ToString());//ADD CONN
                    
                    txtId.Value = _Entity.RecordId;
                    gvBizPartner.Value = _Entity.BizPartnerCode;
                    gvServiceType.Value = _Entity.ServiceType;
                    gvWarehouse.Value = _Entity.WarehouseCode;
                    spEndBal.Value = _Entity.EndingBalance;
                    dtpAsOfDate.Text = Convert.ToDateTime(_Entity.AsOfDate).ToShortDateString();
                    gvUnitOfMeasure.Value = _Entity.UnitOfMeasure;
                    spPallet.Value = _Entity.PalletEndingBal;
                    txtProdNum.Value = _Entity.ProdNum;
                    speRRDate.Value = String.IsNullOrEmpty(_Entity.RRDate) ? null : Convert.ToDateTime(_Entity.RRDate).ToShortDateString();  
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;                    
                }

            }

            
        }
        #endregion

        #region Validation
        private void Validate()
        {
        //    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        //    gparam._DocNo = _Entity.DocNumber;
        //    gparam._UserId = Session["Userid"].ToString();
        //    gparam._TransType = "WMSTRN";

        //    string strresult =GWarehouseManagement.WMSInventory_Validate(gparam);
            

        //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

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
                look.DropDownButton.Enabled = !view;
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
            date.DropDownButton.Enabled = !view;
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
            _Entity.RecordId = Request.QueryString["docnumber"].ToString();
            _Entity.BizPartnerCode = String.IsNullOrEmpty(gvBizPartner.Text) ? null : gvBizPartner.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(gvWarehouse.Text) ? null : gvWarehouse.Value.ToString();
            _Entity.ServiceType = String.IsNullOrEmpty(gvServiceType.Text) ? null : gvServiceType.Value.ToString();
            _Entity.EndingBalance = String.IsNullOrEmpty(spEndBal.Text) ? 0 : Convert.ToDecimal(spEndBal.Text);
            _Entity.AsOfDate = dtpAsOfDate.Text;
            _Entity.UnitOfMeasure = String.IsNullOrEmpty(gvUnitOfMeasure.Text) ? null : gvUnitOfMeasure.Value.ToString();
            _Entity.PalletEndingBal = String.IsNullOrEmpty(spPallet.Text) ? 0 : Convert.ToDecimal(spPallet.Text);
            _Entity.ProdNum = txtProdNum.Text;
            _Entity.RRDate = (speRRDate.Value)==null ? null : speRRDate.Value.ToString();
                
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

                    if (error == false)
                    {
                        check = true;
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
                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header
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
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {

        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {

        }
        #endregion

        protected void dtpAsOfDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpAsOfDate.Date = DateTime.Now;
            }
        }
        protected void Connection_init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}