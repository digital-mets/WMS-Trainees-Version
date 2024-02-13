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
    public partial class frmWarehouse : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Warehouse _Entity = new Warehouse();//Calls entity ICN
  
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
         
            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                switch (Request.QueryString["entry"].ToString())
                {
                   case "N":
                        gv1.Visible = false;
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtwarehouse.ReadOnly = true;
                        glcheck.ClientVisible = true;
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        txtwarehouse.ReadOnly = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        txtwarehouse.ReadOnly = true;
                        break;
                }



               //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                   // gv1.DataSourceID = "sdsDetail";
                  
                    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                }
                else
                {
                    txtwarehouse.Value = Request.QueryString["docnumber"].ToString();

                    _Entity.getdata(txtwarehouse.Text, Session["ConnString"].ToString());
                     txtwarehouse.Text =  _Entity.WarehouseCode;

                  txtMnemonics.Text  =  _Entity.Mnemonics ;
                     txtdesciption.Text = _Entity.Description;
                      txtsupervisor.Value = _Entity.Supervisor;
                    txtaddress.Text =   _Entity.Address;
                     txtContactNumber.Text =  _Entity.ContactNumber;
                    txtCustomerNumber.Text =  _Entity.CustomerNumber;
                    //dtplastcountdate.Text = String.IsNullOrEmpty(_Entity.LastCountDate) ? "" : Convert.ToDateTime(_Entity.LastCountDate).ToShortDateString();
                    txtLastCountDate.Text = String.IsNullOrEmpty(_Entity.LastCountDate) ? "" : Convert.ToDateTime(_Entity.LastCountDate).ToShortDateString();
                 txtLatitude.Text = _Entity.Latitude;

                 txtLongitude.Text = _Entity.Longitude;
                   chckBizPartner.Value =_Entity.IsBizPartner ;

                   chckInactive.Value =   _Entity.IsInactive;
         


                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;


                    //DataTable dtItemWHDetail = Gears.RetriveData2("select * from Masterfile.ItemWHDetail where WarehouseCode='" + txtwarehouse.Value + "'");

                    //gv2.DataSource = dtItemWHDetail;
                    //gv2.DataBind();
                    odsDetail.SelectParameters["WarehouseCode"].DefaultValue  =  txtwarehouse.Text;
                    gv1.DataSourceID = "odsDetail";
                }
                //if (Session["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
              //  {
              //     

                    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
             //   } 
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
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.DropDownButton.Enabled = !view;
            lookup.ReadOnly = view;
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
            spinedit.SpinButtons.Enabled = !view;
            spinedit.ReadOnly = view;
        }

        protected void Check_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
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
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel)
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
       
     
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.WarehouseCode = txtwarehouse.Text;

            _Entity.Mnemonics = txtMnemonics.Text;
            _Entity.Description = txtdesciption.Text;
            _Entity.Supervisor = string.IsNullOrEmpty(txtsupervisor.Value.ToString()) ? "" : txtsupervisor.Value.ToString();
            _Entity.Address = txtaddress.Text;
            _Entity.ContactNumber = txtContactNumber.Text;
            _Entity.CustomerNumber = txtCustomerNumber.Text;
            _Entity.LastCountDate = String.IsNullOrEmpty(txtLastCountDate.Text) ? null : txtLastCountDate.Text;
            
            _Entity.Latitude = txtLatitude.Text;
            _Entity.Longitude = txtLongitude.Text;

            _Entity.IsBizPartner = Convert.ToBoolean(chckBizPartner.Value);

            _Entity.IsInactive = Convert.ToBoolean(chckInactive.Value);
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
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.Connection = Session["ConnString"].ToString(); 


            switch (e.Parameter)
            {
                case "Add":


                   
                        check = true;
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        
                   
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                     
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                   

                    break;

                case "Update":

                
                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header


                   
                       // odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                      
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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

      
  
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
      #endregion

        #region Validation
   private void Validate()
   {
       //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
       //gparam._DocNo = _Entity.;
       //gparam._UserId = Session["Userid"].ToString();
       //gparam._TransType = "WMSIA";

       //string strresult = GWarehouseManagement.PickList_Validate(gparam);

       //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

   }
   #endregion


        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Connection_Init(object sender, EventArgs e)
        {
          
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            Warehouse.ConnectionString = Session["ConnString"].ToString();
            ItemAdjustment.ConnectionString = Session["ConnString"].ToString();
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            MasterfileEmp.ConnectionString = Session["ConnString"].ToString();
        }
      

      

       
       
       
      
    }
}