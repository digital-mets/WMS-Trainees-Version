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
using GearsAccounting;

namespace GWL
{
    public partial class frmAmortization : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Amortization _Entity = new Amortization();//Calls entity 

        #region page load/entry

            protected void Page_Load(object sender, EventArgs e)
            {
                Gears.UseConnectionString(Session["ConnString"].ToString());
           //Random rand = new Random();
           //GearsLibrary.Gears.GearsParameter gp = new Gears.GearsParameter();
           //string Meth = "GearsWarehouseManagement.GWarehouseManagement.WMSTransactionNonStorage._Submit(gp)"
           //System.Reflection.MethodInfo info = rand.GetType().GetMethod(Meth);
           //string sstrmessage = (string)info.Invoke(rand,null);

                
                //select A.AccountCode,B.SubsiCode,B.Description from Accounting.ChartOfAccount A inner join Accounting.GLSubsiCode B on A.AccountCode = B.AccountCode where " +
                                            
 
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

            //if (!string.IsNullOrEmpty(glServiceType.Text))
            //{
            //    ChangeFieldName();
            //}

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }
            popup.ShowOnPageLoad = false;
            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                Session["amor"] = null;
                Session["amor2"] = null;
                Session["shiela"] = null;
                Session["shielpat"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        //glcheck.ClientVisible = false;
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
                txtDocnumber.ReadOnly = true;
                txtId.Text = Request.QueryString["docnumber"].ToString();
                
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    txtStatus.Value = "N";
                    popup.ShowOnPageLoad = false;
                }
                else
                {
                    _Entity.getdata(txtId.Text, Session["ConnString"].ToString());//ADD CONN

                    //txtId.Value = _Entity.RecordId;
                    txtDocnumber.Value = _Entity.DocNumber;
                    txtTran.Value = _Entity.TranType;
                    txtDesc.Value = _Entity.Description;
                    deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    txtTotal.Value = _Entity.TotalAmount;
                    glAccount.Value = _Entity.AccountCode;
                    glSubsi.Value = _Entity.SubsiCode;
                    glProfit.Value = _Entity.ProfitCenterCode;
                    glCost.Value = _Entity.CostCenterCode;
                    txtBiz.Value = _Entity.BizPartnerCode;
                    dtpStart.Text = String.IsNullOrEmpty(_Entity.DateStart) ? "" : Convert.ToDateTime(_Entity.DateStart).ToShortDateString();
                    dtpEnd.Text = String.IsNullOrEmpty(_Entity.DateEnd) ? "" : Convert.ToDateTime(_Entity.DateEnd).ToShortDateString();
                    txtAmortization.Value = _Entity.MonthlyAmortization;
                    txtNumPost.Value = _Entity.NumPosting;
                    glRevAcc.Value = _Entity.ReversalGLCode;
                    
                    glRevSubsi.Value = _Entity.ReversalSubsiCode;
                    glRevProfit.Value = _Entity.ReversalProfitCode;
                    glRevCost.Value = _Entity.ReversalCostCode;
                    glRevBiz.Value = _Entity.ReversalBizPartnerCode;
                    txtActNum.Value = _Entity.ActualNumberOfPosting;
                    txtPostAmt.Value = _Entity.PostingAmount;
                    txtLastJVPost.Value = _Entity.LastJVPosted;
                    //if (!String.IsNullOrEmpty(_Entity.LastJVPosted))
                    //    txtLastJVPost.Value = Convert.ToDateTime(_Entity.LastJVPosted).ToShortDateString();
                    if (!String.IsNullOrEmpty(_Entity.LastJVDate))
                        dtpLastJVDate.Value = Convert.ToDateTime(_Entity.LastJVDate).ToShortDateString();
                    txtThisMonth.Value = _Entity.ThisMonth;
                    txtRemarks.Value = _Entity.Remarks;
                    txtStatus.Value = _Entity.Status;
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

                    //txtAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? "" : Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                    //txtLastEditedDate.Text = String.IsNullOrEmpty(_Entity.LastEditedDate) ? "" : Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                    
                    //txtAddedBy.Text = _Entity.AddedBy;
                    //txtLastEditedBy.Text = _Entity.LastEditedBy;
                    

                    if ((txtStatus.Text.ToString() == "X" || txtStatus.Text.ToString() == "C" || txtStatus.Text.ToString() == "A"))
                    {
                        ErrorPop.ShowOnPageLoad = true;
                        return;
                    }
                }
            }
            //ReversalAccount.SelectCommand = "SELECT AccountCode,Description from Accounting.ChartOfAccount where Isnull(AllowJV,0)=1 and Isnull(AmortizationAccount,0)=0 and isnull(IsInactive,0)=0 AND AccountCode != '" + glAccount.Text + "'";
            //RevSubsiCode.SelectCommand = "SELECT SubsiCode,Description from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode = '" + glRevAcc.Text + "'";
            //ReversalAccount.DataBind();
            //RevSubsiCode.DataBind();
                //glRevAcc.Value = _Entity.ReversalGLCode;
            //glRevSubsi.Value = _Entity.ReversalSubsiCode;
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.RecordId;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GAccounting.Amortization_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        }
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
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
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

        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
           
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.RecordId = Request.QueryString["docnumber"].ToString();
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = deDocDate.Text;
            _Entity.TranType = txtTran.Text;
            _Entity.Description = txtDesc.Text;
            _Entity.TotalAmount = String.IsNullOrEmpty(txtTotal.Text) ? 0 : Convert.ToDecimal(txtTotal.Text);
            //2016-05-21    DAREN   Comment na to
            _Entity.AccountCode = String.IsNullOrEmpty(glAccount.Text) ? null : glAccount.Value.ToString();
            _Entity.SubsiCode = String.IsNullOrEmpty(glSubsi.Text) ? null : glSubsi.Value.ToString();
            _Entity.ProfitCenterCode = String.IsNullOrEmpty(glProfit.Text) ? null : glProfit.Value.ToString();
            _Entity.CostCenterCode = String.IsNullOrEmpty(glCost.Text) ? null : glCost.Value.ToString();
            _Entity.BizPartnerCode = txtBiz.Text;
            _Entity.DateStart = dtpStart.Text;
            _Entity.DateEnd = dtpEnd.Text;
            _Entity.MonthlyAmortization = String.IsNullOrEmpty(txtAmortization.Text) ? 0 : Convert.ToDecimal(txtAmortization.Text);
            _Entity.NumPosting = txtNumPost.Text;
            _Entity.ReversalGLCode = String.IsNullOrEmpty(glRevAcc.Text) ? null : glRevAcc.Value.ToString();
            _Entity.ReversalSubsiCode = String.IsNullOrEmpty(glRevSubsi.Text) ? null : glRevSubsi.Value.ToString();
            _Entity.ReversalProfitCode = String.IsNullOrEmpty(glProfit.Text) ? null : glProfit.Value.ToString();
            _Entity.ReversalCostCode = String.IsNullOrEmpty(glRevCost.Text) ? null : glRevCost.Value.ToString();
            _Entity.ReversalBizPartnerCode = String.IsNullOrEmpty(glRevBiz.Text) ? null : glRevBiz.Value.ToString();
            _Entity.ActualNumberOfPosting = txtActNum.Text;
            _Entity.PostingAmount = String.IsNullOrEmpty(txtPostAmt.Text) ? 0 : Convert.ToDecimal(txtPostAmt.Text);
            _Entity.LastJVPosted = txtLastJVPost.Text;
            _Entity.LastJVDate = dtpLastJVDate.Text;
            _Entity.ThisMonth = String.IsNullOrEmpty(txtThisMonth.Text) ? 0 : Convert.ToDecimal(txtThisMonth.Text);
            _Entity.Status = String.IsNullOrEmpty(txtStatus.Text) ? null : txtStatus.Value.ToString();
            _Entity.Remarks = txtRemarks.Text;
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            //_Entity.AddedBy = txtAddedBy.Text;
            //_Entity.AddedDate = txtAddedDate.Text;

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
                        _Entity.UpdateData(_Entity);
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
                case "Account":
                    SqlDataSource pl = SubsiCode;
                    pl.SelectCommand = string.Format("SELECT SubsiCode,Description,AccountCode from Accounting.GLSubsiCode where isnull(IsInactive,0)=0 and AccountCode = '" + _Entity.ReversalGLCode + "'");
                    glRevSubsi.DataSourceID = "RevSubsiCode";
                    glRevSubsi.DataBind();
                    //DataView tranpl = (DataView)pl.Select(DataSourceSelectArguments.Empty);
                    //if (tranpl.Count > 0)
                    //{
                    //    glRevSubsi.Text = tranpl[0][1].ToString();
                    //}
                    break;
                case "Rev":
                    SqlDataSource Rv = ReversalAccount;
                    Rv.SelectCommand = string.Format("SELECT AccountCode,Description from Accounting.ChartOfAccount where Isnull(AllowJV,0)=1 and Isnull(AmortizationAccount,0)=0 and isnull(IsInactive,0)=0 and Isnull(ControlAccount,0)=0 and AccountCode != '" + glAccount.Text + "'");
                    glRevAcc.DataSourceID = "AccountCode";
                    glRevAcc.DataBind();
                    //DataView tranpl = (DataView)pl.Select(DataSourceSelectArguments.Empty);
                    //if (tranpl.Count > 0)
                    //{
                    //    glRevSubsi.Text = tranpl[0][1].ToString();
                    //}
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

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {

        }

        protected void deDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                deDocDate.Date = DateTime.Now;
            }
        }
        //protected void dtpLastJVDate_Init(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["entry"] == "N")
        //    {
        //        dtpLastJVDate.Date = DateTime.Now;
        //    }
        //}
        protected void dtpStart_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpStart.Date = DateTime.Now;
            }
        }
        protected void dtpEnd_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpEnd.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();

            AccountCode.ConnectionString = Session["ConnString"].ToString();
            ReversalAccount.ConnectionString = Session["ConnString"].ToString();
            CostCenterCode.ConnectionString = Session["ConnString"].ToString();
            ProfitCenterCode.ConnectionString = Session["ConnString"].ToString();
            SubsiCode.ConnectionString = Session["ConnString"].ToString();
            RevSubsiCode.ConnectionString = Session["ConnString"].ToString();
            BizPartner.ConnectionString = Session["ConnString"].ToString();

        }

        protected void glRevSubsi_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
            if (Session["shiela"] != null)
            {
                //gridLookup.GridView.DataSourceID = null;
                //gridLookup.GridView.DataSource = RevSubsiCode;
                RevSubsiCode.FilterExpression = Session["shiela"].ToString();
            }
        }

        public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {



            string whcode = e.Parameters;//Set column name
            if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { glRevAcc.Text });
            RevSubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["shiela"] = RevSubsiCode.FilterExpression;
            grid.DataSourceID = "RevSubsiCode";
            grid.DataBind();
        }
        protected void glRevAcc_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList2_CustomCallback);
            if (Session["shielapat"] != null)
            {
                //gridLookup.GridView.DataSourceID = null;
                //gridLookup.GridView.DataSource = ReversalAccount;
                ReversalAccount.FilterExpression = Session["shielapat"].ToString();
            }
        }

        public void glPickList2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string whcode2 = e.Parameters;//Set column name
            if (whcode2.Contains("GLP_AIC") || whcode2.Contains("GLP_AC") || whcode2.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            //grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { glAccount.Text });
            CriteriaOperator not = new NotOperator(selectionCriteria);
            ReversalAccount.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not)).ToString();
            Session["shielapat"] = ReversalAccount.FilterExpression;
            grid.DataSourceID = "ReversalAccount";
            grid.DataBind();
        }

    }
}