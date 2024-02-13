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
    public partial class frmPallet : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Pallet _Entity = new Pallet();//Calls entity odsHeader

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
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        CustomerCode.ClientEnabled = false;
                       
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
                    txtPallet.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    _Entity.getdata(txtPallet.Text, Session["ConnString"].ToString());//ADD CONN //Method for retrieving data from entity

                    txtPallet.Text = _Entity.PalletID;
                    CustomerCode.Value = _Entity.AreaCode;
                    txtPackaging.Value = _Entity.Packaging;
                    txtCaseTier.Text = _Entity.CaseTier.ToString();
                    txtTierPallet.Text = _Entity.TierPallet.ToString();
                    txtWidth.Text = _Entity.Width.ToString();
                    txtLength.Text = _Entity.Length.ToString();
                    txtHeight.Text = _Entity.Height.ToString();
                    txtUnitWeight.Text = _Entity.UnitWeight.ToString();
                    txtPalletType.Text = _Entity.PalletType.ToString();
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

            //string strresult =GWarehouseManagement.Pallet_Validate(gparam);


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
            //CustomerCode.ReadOnly = view;
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
            _Entity.PalletID = txtPallet.Text;
            _Entity.AreaCode = String.IsNullOrEmpty(CustomerCode.Text) ? null : CustomerCode.Value.ToString();
            _Entity.Packaging = String.IsNullOrEmpty(txtPackaging.Text) ? null : txtPackaging.Value.ToString();
            _Entity.CaseTier = String.IsNullOrEmpty(txtCaseTier.Text) ? 0 : Convert.ToDecimal(txtCaseTier.Text);
            _Entity.TierPallet = String.IsNullOrEmpty(txtTierPallet.Text) ? 0 : Convert.ToDecimal(txtTierPallet.Text);
            _Entity.Width = String.IsNullOrEmpty(txtWidth.Text) ? 0 : Convert.ToDecimal(txtWidth.Text);
            _Entity.Length = String.IsNullOrEmpty(txtLength.Text) ? 0 : Convert.ToDecimal(txtLength.Text);
            _Entity.Height = String.IsNullOrEmpty(txtHeight.Text) ? 0 : Convert.ToDecimal(txtHeight.Text);
            _Entity.UnitWeight = String.IsNullOrEmpty(txtUnitWeight.Text) ? 0 : Convert.ToDecimal(txtUnitWeight.Text);
            _Entity.PalletType = String.IsNullOrEmpty(txtPalletType.Text) ? null : txtPalletType.Value.ToString();
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

                        SqlDataSource ss = Customer;
                        ss.SelectCommand = string.Format("UPDATE PORTAL.IT.PalletInfo SET SeriesNumber = SeriesNumber + 1,PalletId= ( SELECT Prefix + LEFT(Storagetype, 1) + CASE WHEN Prefix = 'PI' OR Prefix = 'LI' THEN RIGHT(REPLICATE('0', 12) + SeriesNumber, 12) ELSE RIGHT(REPLICATE('0', 6) + SeriesNumber, 6)" +
                                                         " END FROM PORTAL.IT.PalletInfo WHERE CustomerCode = '" + CustomerCode.Text + "' ) where CustomerCode = '" + CustomerCode.Text + "'");
                        DataView trans = (DataView)ss.Select(DataSourceSelectArguments.Empty);
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
                case "SUP":
                    SqlDataSource ds = Customer;
                    ds.SelectCommand = string.Format("DECLARE @NewPalletId varchar(20)" +
                                                     "SELECT @NewPalletId = Prefix +LEFT(Storagetype, 1)+ CASE WHEN Prefix = 'PI' OR Prefix = 'LI' THEN RIGHT(REPLICATE('0',12)+SeriesNumber,12) ELSE RIGHT(REPLICATE('0',6)+SeriesNumber,6) END " +
                                                     "FROM PORTAL.IT.PalletInfo Where CustomerCode = '" + CustomerCode.Text + "' SELECT @NewPalletId");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtPallet.Value = tran[0][0].ToString();

                    }
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

        protected void Connection_Init(object sender, EventArgs e)
        {
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Warehouse.ConnectionString = Session["ConnString"].ToString();
            //PlantCode.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            //RoomCode.ConnectionString = Session["ConnString"].ToString();
            //PalletType.ConnectionString = Session["ConnString"].ToString();
            //Storage.ConnectionString = Session["ConnString"].ToString();
            //PalletID.ConnectionString = Session["ConnString"].ToString();
        }


        #endregion
    }
}