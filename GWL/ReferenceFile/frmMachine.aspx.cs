using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
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


namespace GWL
{
    public partial class frmMachine : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Machine _Entity = new Machine();

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

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
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
                     
                        break;
                     case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        view = true;//sets view mode for entry
                        glcheck.ClientVisible = false;
                        break;
                }

                //chkIsInactive.Enabled = false;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    txtCode.Text = "";
                    txtCode.ReadOnly = false;
                }
                else
                {

                    txtCode.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                    _Entity.getdata(txtCode.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                    txtName.Text = _Entity.MachineName;
                    txtSpecifications.Text = _Entity.Specifications;
                    txtBrand.Text = _Entity.Brand;
                    txtModel.Text = _Entity.Model;
                    txtSerialNo.Text = _Entity.SerialNo;
                    txtAssetTag.Text = _Entity.AssetTag;
                    txtCapacityKW.Value = _Entity.CapacityKW;
                    txtSupplyVoltage.Text = _Entity.SupplyVoltage;
                    txtLocation.Text = _Entity.Location;
                    txtStatus.Text = _Entity.Status;
                    txtMachineCapacityPerBatch.Value = _Entity.MachineCapacityPerBatch;
                    txtMachineCapacityPerUnit.Value = _Entity.MachineCapacityPerUnit;
                    chkDaily.Value = _Entity.IsDaily;
                    txtDaily.Text = _Entity.Daily;
                    chkWeekly.Value = _Entity.IsWeekly;
                    txtWeekly.Text = _Entity.Weekly;
                    chkBiMonthly.Value = _Entity.IsBiMonthly;
                    txtBiMonthly.Text = _Entity.BiMonthly;
                    chkMonthly.Value = _Entity.IsMonthly;
                    txtMonthly.Text = _Entity.Monthly;
                    chkSemiAnnual.Value = _Entity.IsSemiAnnual;
                    txtSemiAnnual.Text = _Entity.SemiAnnual;
                    chkAnnually.Value = _Entity.IsAnnually;
                    txtAnnually.Text = _Entity.Annually;
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
                    txtHActivatedBy.Text = _Entity.ActivatedBy;
                    txtHActivatedDate.Text = _Entity.ActivatedDate;
                    txtHDeactivatedBy.Text = _Entity.DeActivatedBy;
                    txtHDeactivatedDate.Text = _Entity.DeActivatedDate;

                    DataTable dtrr1 = Gears.RetriveData2("Select StepCode from MasterFile.Step where StepCode = '" + txtCode.Text + "' and ISNULL(LastEditedBy,'')!='' ", Session["ConnString"].ToString());
                    if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                    {
                        updateBtn.Text = "Update";
                    }

                }
                
                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

            }

            //if (updateBtn.Text == "Add")
            //{
            //    spinEstWOPrice.Value = 0.00;
            //    spinMaxWOPrice.Value = 0.00;
            //    spinMinWOPrice.Value = 0.00;
            //    spinAllowance.Value = 0.00;
            //    spinYield.Value = 0.00;
            //}
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
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
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

        protected void CheckBoxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void TimeEdit_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTimeEdit time = sender as ASPxTimeEdit;
            time.ReadOnly = view;
        }

        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "tasks_newtask_16x16";
            }
            //if (e.ButtonType == ColumnCommandButtonType.New)
            //{
            //    e.Image.IconID = "tasks_newtask_16x16";
            //}
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update && e.ButtonType == ColumnCommandButtonType.New)
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
            _Entity.Connection = Session["ConnString"].ToString();
            DataTable machinecode = Gears.RetriveData2("SELECT MachineCode from MasterFile.Machine WHERE MachineCode = '" + txtCode.Text + "'", Session["ConnString"].ToString());
            if (machinecode.Rows.Count > 0 && updateBtn.Text == "Add")
            {
                cp.JSProperties["cp_message"] = "MachineCode:'" + txtCode.Text + "' already Exist!";
                cp.JSProperties["cp_success"] = true;
                return;
            }

            _Entity.MachineCode = txtCode.Text;
            _Entity.MachineName = txtName.Text;
            _Entity.Specifications = txtSpecifications.Text;
            _Entity.Brand = txtBrand.Text;
            _Entity.Model = txtModel.Text;
            _Entity.SerialNo = txtSerialNo.Text;
            _Entity.AssetTag = txtAssetTag.Text;
            _Entity.CapacityKW = String.IsNullOrEmpty(txtCapacityKW.Value.ToString()) ? "0.00" : txtCapacityKW.Value.ToString();
            _Entity.SupplyVoltage = String.IsNullOrEmpty(txtSupplyVoltage.Value.ToString()) ? "0.00" : txtSupplyVoltage.Value.ToString();
            txtLocation.Text = _Entity.Location;
            _Entity.Status = txtStatus.Text;
            _Entity.MachineCapacityPerBatch = String.IsNullOrEmpty(txtMachineCapacityPerBatch.Value.ToString()) ? "0.00" : txtMachineCapacityPerBatch.Value.ToString();
            _Entity.MachineCapacityPerUnit = String.IsNullOrEmpty(txtMachineCapacityPerUnit.Value.ToString()) ? "0.00" : txtMachineCapacityPerUnit.Value.ToString();
            _Entity.IsDaily = Convert.ToBoolean(chkDaily.Value);
            _Entity.Daily = txtDaily.Text;
            _Entity.IsWeekly = Convert.ToBoolean(chkWeekly.Value);
            _Entity.Weekly = txtWeekly.Text;
            _Entity.IsBiMonthly = Convert.ToBoolean(chkBiMonthly.Value);
            _Entity.BiMonthly = txtBiMonthly.Text;
            _Entity.IsMonthly = Convert.ToBoolean(chkMonthly.Value);
            _Entity.Monthly = txtMonthly.Text;
            _Entity.IsSemiAnnual = Convert.ToBoolean(chkSemiAnnual.Value);
            _Entity.SemiAnnual = txtSemiAnnual.Text;
            _Entity.IsAnnually = Convert.ToBoolean(chkAnnually.Value);
            _Entity.Annually = txtAnnually.Text;
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
            _Entity.LastEditedDate = DateTime.Now.ToString();

            switch (e.Parameter)
            {
                case "Add":
                        check = true;
                        string Result = "";

                       Result= _Entity.InsertData(_Entity); 

                    if (!String.IsNullOrEmpty(Result))
                    {
                        error = true;
                    }

                    if (error == false)
                    {
                        check = true;
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Result)) //Database Validation
                        {
                            cp.JSProperties["cp_message"] = "Step is already existing, cannot add the new step.";
                            cp.JSProperties["cp_success"] = true;
                        }
                        else //Client Size Validation
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
                    }
                        
                        Validate();
                     

                        break;
                case "Update":
                     if (error == false)
                   check = true;
                    string ResultU = "";

                    ResultU = _Entity.UpdateData(_Entity);

                    if (!String.IsNullOrEmpty(ResultU))
                    {
                        error = true;
                    }

                    if (error == false)
                    {
                        check = true;
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(ResultU)) //Database Validation
                        {
                            cp.JSProperties["cp_message"] = ResultU;
                            cp.JSProperties["cp_success"] = true;
                        }
                        else //Client Size Validation
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
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
                    break;
            }
        }


     
   
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //sdsOverhead.ConnectionString = Session["ConnString"].ToString();
            //sdsStep.ConnectionString = Session["ConnString"].ToString();
        }

    }
}