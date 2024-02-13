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
    public partial class frmCustomer : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Customer _Entity = new Customer();//Calls entity Customer
        Entity.Customer.BPBankInfo _EntityDetail = new Customer.BPBankInfo();//Calls entity Customer
        Entity.Customer.BPCustomerTerm _EntityDetail1 = new Customer.BPCustomerTerm();//Calls entity Customer
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
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session


                //if (Request.QueryString["entry"].ToString() == "N")
                //{


                //}
                //else
                //{
                    //gvBiz.ReadOnly = true;
                    gvBiz.Value = Request.QueryString["docnumber"].ToString();

                    _Entity.getdata(gvBiz.Text, Session["ConnString"].ToString());//ADD CONN
                    //_Entity.getdata(gvBiz.Text);

                    gvBiz.Text = _Entity.BizPartnerCode;
                    txtName.Text = _Entity.Name;
                    gvProfit.Value = _Entity.ProfitCenterCode;
                    gvCost.Value = _Entity.CostCenterCode;
                    txtTotalAR.Text = _Entity.TotalAR;
                    txtAddress.Text = _Entity.DeliveryAddress;
                    gvSalesman.Value = _Entity.SalesmanCode;
                    gvCurrency.Value = _Entity.Currency;
                    cboStatus.Value = _Entity.Status;
                    chkCounter.Value = _Entity.IsForCounter;
                    //gvBankCode.Value = _Entity.BankAccountCode;
                    //gvAccount.Text = _Entity.AccountNumber;
                    //txtBranch.Text = _Entity.Branch;
                    //txtSpec.Text = _Entity.SpecimenSignature;
                    //gvItemCat.Value = _Entity.ItemCategoryCode;
                    //txtARTerm.Text = _Entity.ARTerm;
                    txtTaxCode.Value = _Entity.TaxCode;
                    txtATCCode.Value = _Entity.ATCCode;
                    txtWithReferenceNumber.Value = _Entity.WithInvoice;
                    cbWithholdingTaxAgent.Value = _Entity.WithholdingTaxAgent;
                    chkIsInactive.Value = _Entity.IsInactive;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeActivatedBy;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
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

                //}
                //if (Request.QueryString["entry"].ToString() != "N")
                //{
                //    if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //    {
                //        gv1.KeyFieldName = "BizPartnerCode";
                //        gv1.DataSourceID = null;
                //        gv2.DataSourceID = "odsDetail2";
                //    }

                //    DataTable dtrow = Gears.RetriveData2
                //       ("Select BizPartnerCode from Masterfile.BPCustomerTerm where BizPartnerCode= '" + gvBiz.Text + "'",
                //        Session["ConnString"].ToString());//ADD CONN
                //    if (dtrow.Rows.Count > 0){
                //        gv1.DataSourceID = "odsDetail";
                //        gv2.DataSourceID = "odsDetail2";
                //    }

                    DataTable checkCount = Gears.RetriveData2("Select BizPartnerCode from masterfile.BPBankInfo where BizPartnerCode = '" + gvBiz.Text + "'", Session["ConnString"].ToString());
                    if (checkCount.Rows.Count > 0)
                    {
                        
                        odsDetail2.SelectParameters["docnumber"].DefaultValue = gvBiz.Text;
                     
                        gv2.DataSourceID = "odsDetail2";
                    }
                    else
                    {
                     
                        gv2.DataSourceID = "sdsDetail2";

                    }

                    DataTable checkCount1 = Gears.RetriveData2("Select BizPartnerCode from masterfile.BPCustomerTerm where BizPartnerCode = '" + gvBiz.Text + "'", Session["ConnString"].ToString());
                    if (checkCount1.Rows.Count > 0)
                    {
                        odsDetail.SelectParameters["docnumber"].DefaultValue = gvBiz.Text;
                        gv1.DataSourceID = "odsDetail";
                    }
                    else
                    {

                        gv1.DataSourceID = "sdsDetail";

                    }

                    //else if (Request.QueryString["iswithdetail"].ToString() == "true" && Request.QueryString["entry"].ToString() != "V")
                    //{
                    //    gv1.KeyFieldName = "BizPartnerCode;ItemCategory";
                    //    gv1.DataSourceID = "odsDetail";
                    //}
                    //if (Request.QueryString["entry"].ToString() == "V")
                    //{
                    //    gv1.KeyFieldName = "BizPartnerCode;ItemCategory";
                    //    gv1.DataSourceID = "odsDetail";
                    //}
                
            }
        }
        #endregion

        #region Validation
        //private void Validate()
        //{
        //    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        //    gparam._DocNo = _Entity.BizPartnerCode;
        //    gparam._UserId = Session["Userid"].ToString();
        //    gparam._TransType = Request.QueryString["transtype"].ToString();
        //    //gparam._Factor = 1;
        //    // gparam._Action = "Validate";
        //    //here
        //    string strresult = GWarehouseManagement.OCN_Validate(gparam);
        //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        //}
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
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
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
                if (e.ButtonID == "Details")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "Delete")
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
            _Entity.BizPartnerCode = String.IsNullOrEmpty(gvBiz.Text) ? null : gvBiz.Value.ToString();
            _Entity.Name = txtName.Text;
            _Entity.ProfitCenterCode = String.IsNullOrEmpty(gvProfit.Text) ? null : gvProfit.Value.ToString();
            _Entity.CostCenterCode = String.IsNullOrEmpty(gvCost.Text) ? null : gvCost.Value.ToString();
            _Entity.TotalAR = txtTotalAR.Text;
            _Entity.DeliveryAddress = txtAddress.Text;
            _Entity.SalesmanCode = String.IsNullOrEmpty(gvSalesman.Text) ? null : gvSalesman.Value.ToString();
            _Entity.Currency = String.IsNullOrEmpty(gvCurrency.Text) ? null : gvCurrency.Value.ToString();
            _Entity.Status = String.IsNullOrEmpty(cboStatus.Text) ? null : cboStatus.Value.ToString();
            _Entity.IsForCounter = Convert.ToBoolean(chkCounter.Checked);
           // _Entity.BankAccountCode = String.IsNullOrEmpty(gvBankCode.Text) ? null : gvBankCode.Value.ToString();
           // _Entity.AccountNumber = String.IsNullOrEmpty(gvAccount.Text) ? null : gvAccount.Value.ToString();
           // _Entity.Branch = txtBranch.Text;
           //// _Entity.AccountNumber = gvAccount.Text;
           // _Entity.SpecimenSignature = txtSpec.Text;
            //_Entity.ItemCategoryCode = gvItemCat.Text;
            //_Entity.ARTerm = txtARTerm.Text;
            _Entity.TaxCode = String.IsNullOrEmpty(txtTaxCode.Text) ? null : txtTaxCode.Value.ToString();
            _Entity.ATCCode = String.IsNullOrEmpty(txtATCCode.Text) ? null : txtATCCode.Value.ToString();
            _Entity.WithInvoice = Convert.ToBoolean(txtWithReferenceNumber.Checked);
            _Entity.WithholdingTaxAgent = Convert.ToBoolean(cbWithholdingTaxAgent.Checked);
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Checked);
            //_Entity.AddedBy = txtAddedBy.Text;
            //_Entity.AddedDate = txtAddedDate.Text;
            //_Entity.ActivatedBy = txtActivatedBy.Text;
            //_Entity.ActivatedDate = txtActivatedDate.Text;
            //_Entity.DeActivatedBy = txtDeActivatedBy.Text;
            //_Entity.DeActivatedDate = txtDeActivatedDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
           
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            


            switch (e.Parameter)
            {
                case "Add":
                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();

                        _Entity.InsertData(_Entity);
                        gv1.DataSourceID = "odsDetail";

                        gv1.UpdateEdit();
                        gv2.DataSourceID = "odsDetail2";
                        gv2.UpdateEdit();
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
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();

                        _Entity.UpdateData(_Entity);
                        gv1.DataSourceID = "odsDetail";
                        gv1.UpdateEdit();
                        gv2.DataSourceID = "odsDetail2";
                        gv2.UpdateEdit();
                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";//Message variable to client side
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

                case "SUP":
                    SqlDataSource ds = BizAccount;
                    ds.SelectCommand = string.Format("select BizPartnerCode,Name,Address from Masterfile.BizPartner where BizPartnerCode = '" + gvBiz.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtName.Value = tran[0][1].ToString();
                        txtAddress.Value = tran[0][2].ToString();
                        
                    }
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

        protected void glBizPartnerCode_TextChanged(object sender, EventArgs e)
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

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

        }
    }
}