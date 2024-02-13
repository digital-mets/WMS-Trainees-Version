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
    public partial class frmWorkCenter : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string ItemCat = "";
        string ProdCat = "";
        string SubCat = "";
        private static string Connection;

        Entity.WorkCenter _Entity = new WorkCenter();//Calls entity ICN
  
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

            Connection = Session["ConnString"].ToString();

            if (!IsPostBack)
            {
               //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                   // gv1.DataSourceID = "sdsDetail";
                  
                    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                }
                else
                {
                    _Entity.getdata(Request.QueryString["docnumber"].ToString(), Session["ConnString"].ToString());

                    txtWorkCenter.Value = _Entity.WorkCenterCode;
                    txtDescription.Text = _Entity.Description;
                    txtStep.Text = _Entity.Step;
                    txtDays.Text = _Entity.Days.ToString();
                    txtShift.Text = _Entity.Shift.ToString();
                    txtOPHours.Text = _Entity.OperatingShift.ToString();
                    txtSAM.Text = _Entity.SAM.ToString();
                    txtUtil.Text = _Entity.Utilization.ToString();
                    txtMachine.Text = _Entity.MachineQty.ToString();
                    txtDaily.Text = _Entity.OutputDays.ToString();
                    txtMonthly.Text = _Entity.OutputWeekly.ToString();
                    txtHField1.Value = _Entity.Field1;
                    txtHField2.Value = _Entity.Field2;
                    txtHField3.Value = _Entity.Field3;
                    txtHField4.Value = _Entity.Field4;
                    txtHField5.Value = _Entity.Field5;
                    txtHField6.Value = _Entity.Field6;
                    txtHField7.Value = _Entity.Field7;
                    txtHField8.Value = _Entity.Field8;
                    txtHField9.Value = _Entity.Field9;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtDeactivatedBy.Text = _Entity.DeActivatedBy;
                    txtDeactivatedDate.Text = _Entity.DeActivatedDate;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        //    gv2.Visible = false;

                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtWorkCenter.ReadOnly = true;
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
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

        protected void CheckBoxLoad(object sender, EventArgs e)//Control for all textbox
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
            if (Request.QueryString["docnumber"] != null && Request.QueryString["docnumber"]!="undefined")
            {
                _Entity.RecordID = Convert.ToInt16(Request.QueryString["docnumber"].ToString());
            }
            _Entity.WorkCenterCode = txtWorkCenter.Text;
            _Entity.Description = txtDescription.Text;
            _Entity.Step = txtStep.Text;
            _Entity.Days = Convert.ToInt16(txtDays.Value);
            _Entity.Shift = Convert.ToInt16(txtShift.Value);
            _Entity.OperatingShift = Convert.ToInt16(txtOPHours.Value);
            _Entity.SAM = Convert.ToInt16(txtSAM.Value);
            _Entity.Utilization = Convert.ToDecimal(txtUtil.Value);
            _Entity.MachineQty = Convert.ToInt16(txtMachine.Value);
            _Entity.OutputDays = Convert.ToDecimal(txtDaily.Value);
            _Entity.OutputWeekly = Convert.ToInt16(txtMonthly.Value);
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeActivatedBy = txtDeactivatedBy.Text;
            _Entity.DeActivatedDate = txtDeactivatedDate.Text;
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.LastEditedBy = txtLastEditedBy.Text;
            _Entity.LastEditedDate = txtLastEditedDate.Text;

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

            string exist = "";
            string recordid = "";
            DataTable checkdb = Gears.RetriveData2("Select RecordID,WorkCenter from masterfile.WorkCenter where workcenter='"
                                + txtWorkCenter.Text + "' and Step = '" + txtStep.Text + "'",Connection);
            foreach (DataRow dt in checkdb.Rows)
            {
                recordid = dt[0].ToString();
                exist = dt[1].ToString();
            }

            switch (e.Parameter)
            {
                case "Add":
                    //if (error == false)
                    //{
                    if (!string.IsNullOrEmpty(exist))
                    {
                        cp.JSProperties["cp_message"] = "Work Center "+txtWorkCenter.Text+" with the Step "+txtStep.Text.Trim()+" already exist!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    else
                    {
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    }
                    
                    break;
                case "Update":
                    if (!string.IsNullOrEmpty(exist) && (Request.QueryString["docnumber"] != "undefined" && Request.QueryString["docnumber"]!=recordid))
                    {
                        cp.JSProperties["cp_message"] = "Work Center with the Step " + txtStep.Text + " already exist!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    else
                    {
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            MasterfileStep.ConnectionString = Session["ConnString"].ToString();
        }

      

      
        //private string RemoveSpaces(string str)
        //{
        //    string result = "";
        //    char[] remover = str.ToCharArray();

        //    for (int i = 0; i < remover.Length; i++)
        //    {
        //        if (remover[i] != ' ')
        //        {
        //            result = string.Concat(result, remover[i]);
        //        }

        //    }

        //    return result;
        //}
       
       
       
      
    }
}