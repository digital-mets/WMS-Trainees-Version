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
    public partial class frmOrganizationChart : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.OrganizationChart _Entity = new OrganizationChart();//Calls entity ICN
  
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
                       // gv2.Visible = false;
                        
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtorganizationchartentityid.ReadOnly = true;
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
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
                     txtorganizationchartentityid.ReadOnly = true;
                     txtorganizationchartentityid.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtorganizationchartentityid.Text, Session["ConnString"].ToString());



                     txtorganizationchartentityid.Value =        _Entity.OrganizationChartEntityID;
                     txtorganizationchartdescription.Value = _Entity.OrganizationChartDescription;
                     glParentEntityID.Value = _Entity.OrganizationChartParentEntityID;
                     chkorganizationchartisinactive.Value = Convert.ToBoolean(_Entity.OrganizationChartIsInactive.ToString());


                     txtAddedBy.Text = _Entity.AddedBy;
                     txtAddedDate.Text = _Entity.AddedDate;
                     txtLastEditedBy.Text = _Entity.LastEditedBy;
                     txtLastEditedDate.Text = _Entity.LastEditedDate;
                     txtActivatedBy.Text = _Entity.ActivatedBy;
                     txtActivatedDate.Text = _Entity.ActivatedDate;
                     txtDeactivatedBy.Text = _Entity.DeActivatedBy;
                     txtDeactivatedDate.Text = _Entity.DeActivatedDate;


                     txtHField1.Value = _Entity.Field1;
                     txtHField2.Value = _Entity.Field2;
                     txtHField3.Value = _Entity.Field3;
                     txtHField4.Value = _Entity.Field4;
                     txtHField5.Value = _Entity.Field5;
                     txtHField6.Value = _Entity.Field6;
                     txtHField7.Value = _Entity.Field7;
                     txtHField8.Value = _Entity.Field8;
                     txtHField9.Value = _Entity.Field9;


                }
                //if (Session["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
              //  {
              //      gv1.DataSourceID = "sdsDetail";

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

        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
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
       
     
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.OrganizationChartEntityID = txtorganizationchartentityid.Text;
            _Entity.OrganizationChartDescription = txtorganizationchartdescription.Text;
            _Entity.OrganizationChartParentEntityID = glParentEntityID.Text;
            _Entity.OrganizationChartIsInactive = chkorganizationchartisinactive.Value.ToString();

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

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            Warehouse.ConnectionString = Session["ConnString"].ToString();
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            ItemAdjustment.ConnectionString = Session["ConnString"].ToString();
            ParentEntityIDLookUp.ConnectionString = Session["ConnString"].ToString();
        }
      

       
       
       
      
    }
}