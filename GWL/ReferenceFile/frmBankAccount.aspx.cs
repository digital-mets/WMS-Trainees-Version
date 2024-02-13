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
    public partial class frmBankAccount : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.BankAccount _Entity = new BankAccount();//Calls entity odsHeader

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
                        break;
                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    txtbankaccountcode.Text = "";
                    txtbankaccountcode.ReadOnly = false;
                }
                else
                {
                    txtbankaccountcode.Value = Request.QueryString["docnumber"].ToString(); 

                    _Entity.getdata(txtbankaccountcode.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                    txtaccountnumber.Text = _Entity.AccountNumber;
                    glBankCode.Text = _Entity.BankCode;
                    txtaccountname.Text = _Entity.AccountName;
                    glBranch.Text = _Entity.Branch;
                    gltype.Text = _Entity.Type;
                    txtdescription.Text = _Entity.Description;
                    glglcode.Text = _Entity.GLCode;
                    txtSignatory1.Text = _Entity.Signatory1;
                    txtSignatory2.Text = _Entity.Signatory2;
                    txtSignatory3.Text = _Entity.Signatory3;
                    txtSignatory4.Text = _Entity.Signatory4;
                    dtpdateopen.Text = String.IsNullOrEmpty(_Entity.DateOpen) ? "" : Convert.ToDateTime(_Entity.DateOpen).ToShortDateString();
                    dtprecondate.Text = String.IsNullOrEmpty(_Entity.LastReconDate) ? "" : Convert.ToDateTime(_Entity.LastReconDate).ToShortDateString();
                    txtMbalance.Text = _Entity.MaintainingBalance.ToString();
                    txtbalance.Text = _Entity.LastBalance.ToString();
                    startseries.Value = _Entity.MinCheckNumber.ToString();
                    endseries.Value = _Entity.MaxCheckNumber.ToString();
                    nextcheck.Text = _Entity.NextCheckNumber.ToString();

                    sptxtDateX.Text = _Entity.DateX.ToString();
                    sptxtDateY.Text = _Entity.DateY.ToString();
                    sptxtAmtX.Text = _Entity.AmountWX.ToString();
                    sptxtAmtY.Text = _Entity.AmountWY.ToString();
                    sptxtPayeeX.Text = _Entity.PayeeX.ToString();
                    sptxtPayeeY.Text = _Entity.PayeeY.ToString();
                    sptxtRemarksX.Text = _Entity.RemarksX.ToString();
                    sptxtRemarksY.Text = _Entity.RemarksY.ToString();
                    sptxtAmtnoX.Text = _Entity.AmountNX.ToString();
                    sptxtAmtnoY.Text = _Entity.AmountNY.ToString();
                    sptxtHeight.Text = _Entity.CheckWidth.ToString();
                    sptxtCheckWidth.Text = _Entity.CheckHeight.ToString();

                    chkIsInactive.Value = _Entity.IsInactive;
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
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtAddedDate.Text = _Entity.ActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeactivatedBy;
                    txtDeActivatedDate.Text = _Entity.DeactivatedDate;

                    DataTable dtrr1 = Gears.RetriveData2("select BankAccountCode from masterfile.BankAccount BankAccountCode= '" + txtbankaccountcode.Text + "' and ISNULL(LastEditedBy,'')!='' ", Session["ConnString"].ToString());
                    if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                    {
                        updateBtn.Text = "Update";
                    }
                }

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

            }

            
        }
        #endregion

        #region Validation
       
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
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
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
           
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.BankAccountCode = txtbankaccountcode.Text;

            _Entity.AccountNumber= txtaccountnumber.Text;
            _Entity.BankCode=  String.IsNullOrEmpty(glBankCode.Text) ? null : glBankCode.Value.ToString();  
            _Entity.AccountName=txtaccountname.Text;
            _Entity.Branch =String.IsNullOrEmpty(glBranch.Text) ? null : glBranch.Value.ToString();
            _Entity.Type = String.IsNullOrEmpty(gltype.Text) ? null : gltype.Value.ToString(); 
            _Entity.Description=txtdescription.Text;
            _Entity.GLCode = String.IsNullOrEmpty(glglcode.Text) ? null : glglcode.Value.ToString();
            _Entity.Signatory1 = txtSignatory1.Text; 
            _Entity.Signatory2=txtSignatory2.Text ;
            _Entity.Signatory3=txtSignatory3.Text;
            _Entity.Signatory4=txtSignatory4.Text;
            _Entity.DateOpen=dtpdateopen.Text ;
            _Entity.LastReconDate=dtprecondate.Text;
            _Entity.MaintainingBalance = String.IsNullOrEmpty(txtMbalance.Text) ? 0 : Convert.ToDecimal(txtMbalance.Text);
            _Entity.LastBalance = String.IsNullOrEmpty(txtbalance.Text) ? 0 : Convert.ToDecimal(txtbalance.Text);
            _Entity.MinCheckNumber =  String.IsNullOrEmpty(startseries.Text) ? 0 : Convert.ToDecimal(startseries.Text);
            _Entity.MaxCheckNumber =  String.IsNullOrEmpty(endseries.Text) ? 0 : Convert.ToDecimal(endseries.Text);
            _Entity.NextCheckNumber = nextcheck.Text;

            _Entity.DateX = String.IsNullOrEmpty(sptxtDateX.Text) ? 0 : Convert.ToInt32(sptxtDateX.Text);
            _Entity.DateY = String.IsNullOrEmpty(sptxtDateY.Text) ? 0 : Convert.ToInt32(sptxtDateY.Text);
            _Entity.AmountWX = String.IsNullOrEmpty(sptxtAmtX.Text) ? 0 : Convert.ToInt32(sptxtAmtX.Text);
            _Entity.AmountWY = String.IsNullOrEmpty(sptxtAmtY.Text) ? 0 : Convert.ToInt32(sptxtAmtY.Text);
            _Entity.PayeeX = String.IsNullOrEmpty(sptxtPayeeX.Text) ? 0 : Convert.ToInt32(sptxtPayeeX.Text);
            _Entity.PayeeY = String.IsNullOrEmpty(sptxtPayeeY.Text) ? 0 : Convert.ToInt32(sptxtPayeeY.Text);
            _Entity.RemarksX = String.IsNullOrEmpty(sptxtRemarksX.Text) ? 0 : Convert.ToInt32(sptxtRemarksX.Text);
            _Entity.RemarksY = String.IsNullOrEmpty(sptxtRemarksY.Text) ? 0 : Convert.ToInt32(sptxtRemarksY.Text);
            _Entity.AmountNX = String.IsNullOrEmpty(sptxtAmtnoX.Text) ? 0 : Convert.ToInt32(sptxtAmtnoX.Text);
            _Entity.AmountNY = String.IsNullOrEmpty(sptxtAmtnoY.Text) ? 0 : Convert.ToInt32(sptxtAmtnoY.Text);
            _Entity.CheckWidth = String.IsNullOrEmpty(sptxtHeight.Text) ? 0 : Convert.ToInt32(sptxtHeight.Text);
            _Entity.CheckHeight = String.IsNullOrEmpty(sptxtCheckWidth.Text) ? 0 : Convert.ToInt32(sptxtCheckWidth.Text);
   
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Value);
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeactivatedBy = txtDeActivatedBy.Text;
            _Entity.DeactivatedDate = txtDeActivatedDate.Text;


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

                        _Entity.UpdateData(_Entity);


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
                    break;
            }

           
        }
       
        //dictionary method to hold error 
       
        //protected void dtpEffectivity_Init(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["entry"] == "N")
        //    {
        //        //dtpEffectivity.Date = DateTime.Now;
        //    }
        //}
        #endregion
        //protected void glRevSubsi_Init(object sender, EventArgs e)
        //{
        //    //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
        //    //if (Session["preOH"] != null)
        //    //{
        //    //    gridLookup.GridView.DataSourceID = null;
        //    //    gridLookup.GridView.DataSource = SubsiCode;
        //    //    SubsiCode.FilterExpression = Session["preOH"].ToString();
        //    //}
        //}
        //public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{



        //    //string whcode = e.Parameters;//Set column name
        //    //if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

        //    //ASPxGridView grid = sender as ASPxGridView;
        //    //grid.DataSourceID = null;
        //    //CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { whcode });
        //    //SubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    //Session["preOH"] = SubsiCode.FilterExpression;
        //    //grid.DataSource = SubsiCode;
        //    //grid.DataBind();
        //}
        //protected void glRevAcc_Init(object sender, EventArgs e)
        //{
        //    //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList2_CustomCallback);
        //    //if (Session["preOH2"] != null)
        //    //{
        //    //    gridLookup.GridView.DataSourceID = null;
        //    //    gridLookup.GridView.DataSource = ActualSubsiCode;
        //    //    ActualSubsiCode.FilterExpression = Session["preOH2"].ToString();
        //    //}
        //}

        //public void glPickList2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{

        //    //string whcode2 = e.Parameters;//Set column name
        //    //if (whcode2.Contains("GLP_AIC") || whcode2.Contains("GLP_AC") || whcode2.Contains("GLP_F")) return;//Traps the callback

        //    //ASPxGridView grid = sender as ASPxGridView;
        //    //grid.DataSourceID = null;
        //    //CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { whcode2 });
        //    //ActualSubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    //Session["preOH2"] = SubsiCode.FilterExpression;
        //    //grid.DataSource = ActualSubsiCode;
        //    //grid.DataBind();
        //}

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //dsBankCode.ConnectionString = Session["ConnString"].ToString();
            //dsBranch.ConnectionString = Session["ConnString"].ToString();
            //dsType.ConnectionString = Session["ConnString"].ToString();
            //dsglcode.ConnectionString = Session["ConnString"].ToString();
        }
    }
}