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
using GearsAccounting;

namespace GWL
{
    public partial class frmLoan : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        // ^^^private static string strError;

        Entity.Loan _Entity = new Loan();

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

            gv1.KeyFieldName = "DocNumber;LineNumber";
            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            dtDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                //Common.Common.HideUDF(frmlayout1, "udf", Session["ConnString"].ToString());
                //Common.Common.preventAutoCloseCheck(glcheck, Session["ConnString"].ToString());
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Add";
                        }
                        else
                        {
                            updateBtn.Text = "Update";
                        }
                        //popup2.ShowOnPageLoad = false;
                        dtDocDate.Text = DateTime.Now.ToShortDateString();
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "M":
                        updateBtn.Text = "Update";
                        if (Request.QueryString["parameters"].ToString() == "loan")
                        {
                            dtDocDate.ClientEnabled = false;
                            cbLoanType.ClientEnabled = false;
                            cbLoanCategory.ClientEnabled = false;
                            aglNewLoans.ClientEnabled = false;
                            aglLoanRenewal.ClientEnabled = false;
                        }
                        else
                        {
                            view = true;
                        }
                        break;
                    case "V":
                        view = true;    //sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);

                cbLoanType.Value = _Entity.LoanType;
                cbLoanCategory.Value = _Entity.LoanCategory;
                cbLoanClass.Text = _Entity.LoanClass;
                spinLoanAmount.Value = _Entity.LoanAmount;
                spinInterestRate.Text = _Entity.InterestRate;
                cbTermType.Value = _Entity.TermType;
                spinTerms.Value = _Entity.Terms;
                dtMaturityDate.Value = _Entity.MaturityDate;
                cbFrequencyInterest.Value = _Entity.IntFrequency.ToString();
                cbFrequencyPrincipal.Value = _Entity.PrinFrequency.ToString();

                if (!String.IsNullOrEmpty(_Entity.LoanReference))
                {
                    sdsNewLoans.SelectParameters["LoanReference"].DefaultValue = _Entity.LoanReference;
                    aglNewLoans.DataBind();
                    sdsLoanRenewal.SelectParameters["LoanReference"].DefaultValue = _Entity.LoanReference;
                    aglLoanRenewal.DataBind();
                }

                if (_Entity.LoanCategory.ToString() == "NewAvailment")
                {
                    aglNewLoans.Value = _Entity.LoanReference;
                    aglNewLoans.ClientVisible = true;
                    aglLoanRenewal.ClientVisible = false;
                    txtLoanReference.ClientEnabled = true;
                }
                else
                {
                    aglLoanRenewal.Value = _Entity.LoanReference;
                    aglLoanRenewal.ClientVisible = true;
                    aglNewLoans.ClientVisible = false;
                    txtLoanReference.ClientEnabled = false;
                }
                txtLoanReference.Text = _Entity.ReferenceLN;
                txtBizPartner.Text = _Entity.BizPartnerCode;
                txtName.Text = _Entity.Name;
      
                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;

                SetStatus();
                DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM Accounting.LoanDetail WHERE DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "RTLOAN";

            string strResult = GearsAccounting.GAccounting.Loan_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strResult;//Message variable to client side
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            //text.ReadOnly = view;
            text.ClientEnabled = text.ClientEnabled && !view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.ClientEnabled = look.ClientEnabled && !view;
            //look.DropDownButton.Enabled = !view;
            //look.ReadOnly = view;
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
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            //combobox.ReadOnly = view;
            //combobox.DropDownButton.Enabled = !view;
            combobox.ClientEnabled = combobox.ClientEnabled && !view;
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            //^^^date.DropDownButton.Enabled = !view;
            //^^^date.ReadOnly = view;
            date.ClientEnabled = date.ClientEnabled && !view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            //spinedit.ReadOnly = view;
            spinedit.ClientEnabled = spinedit.ClientEnabled && !view;
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
            if (e.ButtonType == ColumnCommandButtonType.Update) { e.Visible = false; }
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
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == "SetMaturity") 
            {
                if (!(dtDocDate.Value == null || String.IsNullOrEmpty(cbLoanClass.Text) ||
                      String.IsNullOrEmpty(cbTermType.Text)))
                {
                    if (cbTermType.Text == "Days")
                    {
                        dtMaturityDate.Value = ((DateTime)dtDocDate.Value).AddDays(Convert.ToInt32(spinTerms.Value));
                    }
                    else
                    {
                        if (cbLoanClass.Text == "Regular")
                        {
                            dtMaturityDate.Value = ((DateTime)dtDocDate.Value).AddDays(Convert.ToInt32(spinTerms.Value)*30);
                        }
                        else
                        {
                            dtMaturityDate.Value = ((DateTime)dtDocDate.Value).AddMonths(Convert.ToInt32(spinTerms.Value));
                        }
                    }
                }
                // 2017-03-22   TL  Re-setup display of loan reference
                if (cbLoanCategory.Text == "New Availment")
                {
                    aglNewLoans.ClientVisible = true;
                    aglLoanRenewal.ClientVisible = false;
                    txtLoanReference.ClientEnabled = true;
                }
                else
                {
                    aglNewLoans.ClientVisible = false;
                    aglLoanRenewal.ClientVisible = true;
                    txtLoanReference.ClientEnabled = false;
                }
            }
            else 
            {
                _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
                _Entity.DocNumber = txtDocnumber.Text;
                _Entity.DocDate = dtDocDate.Text;

                _Entity.LoanType = cbLoanType.Value.ToString();
                _Entity.LoanCategory = cbLoanCategory.Value.ToString();
                _Entity.LoanClass = cbLoanClass.Text;

                if (cbLoanCategory.Value.ToString() == "NewAvailment")
                    _Entity.LoanReference = aglNewLoans.Text;
                else
                    _Entity.LoanReference = aglLoanRenewal.Text;
                _Entity.ReferenceLN = txtLoanReference.Text;
                _Entity.BizPartnerCode = txtBizPartner.Text;
                _Entity.Name = txtName.Text;

                _Entity.LoanAmount = Convert.ToDecimal(spinLoanAmount.Value);
                _Entity.InterestRate = spinInterestRate.Text;
                _Entity.TermType = cbTermType.Value.ToString();
                _Entity.Terms = Convert.ToInt32(spinTerms.Value);
                if (dtMaturityDate.Value == null) { _Entity.MaturityDate = null; }
                else { _Entity.MaturityDate = Convert.ToDateTime(dtMaturityDate.Value); }
                _Entity.IntFrequency = Convert.ToInt32(cbFrequencyInterest.Value);
                _Entity.PrinFrequency = Convert.ToInt32(cbFrequencyPrincipal.Value);

                SaveStatus();

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

                            _Entity.LastEditedBy = Session["userid"].ToString();
                            _Entity.UpdateData(_Entity);//Method of Updating header


                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                            gv1.UpdateEdit();//2nd Initiation to update grid
                            Validate();
                            cp.JSProperties["cp_message"] = "Successfully Updated!";
                            cp.JSProperties["cp_close"] = true;
                            Session["Refresh"] = "1";
                        }
                        else
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }

                        break;

                    case "Update":

                        //gv1.UpdateEdit();
                        //strError = Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseRequest",2,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        //    if (!string.IsNullOrEmpty(strError))
                        //    {
                        //        cp.JSProperties["cp_message"] = strError;
                        //        cp.JSProperties["cp_success"] = true;
                        //        cp.JSProperties["cp_forceclose"] = true;
                        //        return;
                        //    }
                        if (error == false)
                        {
                            check = true;

                            _Entity.LastEditedBy = Session["userid"].ToString();
                            _Entity.UpdateData(_Entity);//Method of Updating header


                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                            gv1.UpdateEdit();//2nd Initiation to update grid
                            Validate();
                            cp.JSProperties["cp_message"] = "Successfully Updated!";
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
        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = false;
                //e.DeleteValues.Clear();
                //e.InsertValues.Clear();
                //e.UpdateValues.Clear();
            }
        }
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        #region Added By LGE
        protected void SetStatus()
        {
            if (_Entity.Status == "C")
                txtStatus.Value = "Closed";
            else if (_Entity.Status == "A")
                txtStatus.Value = "Active";
            else
            {
                txtStatus.Value = "New";
            }
        }
        protected void SaveStatus()
        {
            if (txtStatus.Text == "Closed")
                _Entity.Status = "C";
            else if (txtStatus.Text == "Active")
                _Entity.Status = "A";
            else
                _Entity.Status = "N";
        }
        #endregion
    }
}