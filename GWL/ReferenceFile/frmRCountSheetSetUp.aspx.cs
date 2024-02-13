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
    public partial class frmRCountSheetSetUp : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.RCountSheetSetUp _Entity = new RCountSheetSetUp();//Calls entity odsHeader

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
                        updateBtn.Text = "Update";
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
                    txtRecord.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    _Entity.getdata(txtRecord.Text, Session["ConnString"].ToString());//ADD CONN //Method for retrieving data from entity

                    txtRecord.Text = _Entity.RecordId;
                    txtTranType.Text = _Entity.TransType;
                    txtTranDoc.Text = _Entity.TransDoc;
                    txtTranLine.Text = _Entity.TransLine;
                    txtLineNum.Text = _Entity.LineNumber;
                    txtItem.Value = _Entity.ItemCode;
                    txtColor.Value = _Entity.ColorCode;
                    txtClass.Text = _Entity.ClassCode;
                    txtSize.Text = _Entity.SizeCode;
                    txtPallet.Text = _Entity.PalletID;
                    txtLoc.Text = _Entity.Location;
                    dtExp.Text = String.IsNullOrEmpty(_Entity.ExpirationDate) ? "" : Convert.ToDateTime(_Entity.ExpirationDate).ToShortDateString();
                    dtMfg.Text = String.IsNullOrEmpty(_Entity.MfgDate) ? "" : Convert.ToDateTime(_Entity.MfgDate).ToShortDateString();
                    txtOrigBulk.Value = _Entity.OriginalBulkQty;
                    txtOrigBase.Value = _Entity.OriginalBaseQty;
                    txtOrigLoc.Value = _Entity.OriginalLocation;
                    txtRemBulk.Value = _Entity.RemainingBulkQty;
                    txtRemBase.Value = _Entity.RemainingBaseQty;
                    txtPickBulk.Value = _Entity.PickedBulkQty;
                    txtPickBase.Value = _Entity.PickedBaseQty;
                    txtResBulk.Value = _Entity.ReservedBulkQty;
                    txtResBase.Value = _Entity.ReservedBaseQty;
                    txtOrigCost.Value = _Entity.OriginalCost;
                    txtUnitCost.Value = _Entity.UnitCost;
                    dtSub.Text = String.IsNullOrEmpty(_Entity.SubmittedDate) ? "" : Convert.ToDateTime(_Entity.SubmittedDate).ToShortDateString();
                    dtPut.Text = String.IsNullOrEmpty(_Entity.PutawayDate) ? "" : Convert.ToDateTime(_Entity.PutawayDate).ToShortDateString();
                    
                    
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

                    if((Convert.ToDecimal(txtOrigBulk.Value) != Convert.ToDecimal(txtRemBulk.Value) || 
                        Convert.ToDecimal(txtOrigBase.Value) != Convert.ToDecimal(txtRemBase.Value)) ||
                        (txtPickBase.Value.ToString() != "0" || txtPickBulk.Value.ToString() != "0") ||
                        (txtResBase.Value.ToString() != "0" || txtResBulk.Value.ToString() != "0"))
                    {
                        //ErrorPop.ShowOnPageLoad = true;
                        return;
                    }
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

            //string strresult =GWarehouseManagement.CountSheetSetUp_Validate(gparam);
            

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            if (text.ReadOnly != true)
            {
                text.ReadOnly = view;
            }
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
            if(date.ReadOnly != true){
            date.ReadOnly = view;
            }
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
            _Entity.RecordId = txtRecord.Text;
            _Entity.TransType = txtTranType.Text;
            _Entity.TransDoc = txtTranDoc.Text;
            _Entity.TransLine = txtTranLine.Text;
            _Entity.LineNumber = txtLineNum.Text;
            _Entity.ItemCode = txtItem.Text;
            _Entity.ColorCode = txtColor.Text;
            _Entity.ClassCode = txtClass.Text;
            _Entity.SizeCode = txtSize.Text;
            _Entity.PalletID = txtPallet.Text;
            _Entity.Location = txtLoc.Text;
            _Entity.ExpirationDate = dtExp.Text;
            _Entity.MfgDate = dtMfg.Text;
            _Entity.RRdate = dtRR.Text;
            _Entity.OriginalBulkQty = String.IsNullOrEmpty(txtOrigBulk.Text) ? 0 : Convert.ToDecimal(txtOrigBulk.Text);
            _Entity.OriginalBaseQty = String.IsNullOrEmpty(txtOrigBase.Text) ? 0 : Convert.ToDecimal(txtOrigBase.Text);
            _Entity.OriginalLocation = txtOrigLoc.Text;
            _Entity.RemainingBulkQty = String.IsNullOrEmpty(txtRemBulk.Text) ? 0 : Convert.ToDecimal(txtRemBulk.Text);
            _Entity.RemainingBaseQty = String.IsNullOrEmpty(txtRemBase.Text) ? 0 : Convert.ToDecimal(txtRemBase.Text);
            _Entity.PickedBulkQty = String.IsNullOrEmpty(txtPickBulk.Text) ? 0 : Convert.ToDecimal(txtPickBulk.Text);
            _Entity.PickedBaseQty = String.IsNullOrEmpty(txtPickBase.Text) ? 0 : Convert.ToDecimal(txtPickBase.Text);
            _Entity.ReservedBulkQty = String.IsNullOrEmpty(txtResBulk.Text) ? 0 : Convert.ToDecimal(txtResBulk.Text);
            _Entity.ReservedBaseQty = String.IsNullOrEmpty(txtResBase.Text) ? 0 : Convert.ToDecimal(txtResBase.Text);
            _Entity.OriginalCost = String.IsNullOrEmpty(txtOrigCost.Text) ? 0 : Convert.ToDecimal(txtOrigCost.Text);
            _Entity.SubmittedDate = dtSub.Text;
            _Entity.PutawayDate = dtPut.Text;
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

        protected void dtExp_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtExp.Date = DateTime.Now;
            }
        }

        protected void dtMfg_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtMfg.Date = DateTime.Now;
            }
        }

        protected void dtSub_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtSub.Date = DateTime.Now;
            }
        }

        protected void dtRR_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtRR.Date = DateTime.Now;
            }
        }

        protected void dtPut_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtPut.Date = DateTime.Now;
            }
        }

        protected void txtSize_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Connection_init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            sdsItem.ConnectionString = Session["ConnString"].ToString();
            sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            Warehouse.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            PlantCode.ConnectionString = Session["ConnString"].ToString();
            RoomCode.ConnectionString = Session["ConnString"].ToString();
            Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            CountSheetSetUpType.ConnectionString = Session["ConnString"].ToString();
        }
    }
}