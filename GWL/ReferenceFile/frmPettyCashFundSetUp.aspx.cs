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
    public partial class frmPettyCashFundSetUp : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.PettyCashFundSetUp _Entity = new PettyCashFundSetUp();//Calls entity ICN
  
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
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        txtFundCode.ClientEnabled = false;
                        glGLAccountCode.ClientEnabled = false;
                        glGLSubsiCode.ClientEnabled = false;
                        glRecordingMethod.ClientEnabled = false;
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        chckInactive.ReadOnly = true;
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        chckInactive.ReadOnly = true;
                        break;
                }

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    txtFundCode.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtFundCode.Text, Session["ConnString"].ToString());//ADD CONN
                    txtFundDescription.Text = _Entity.FundDescription;
                    glCustodian.Value = _Entity.Custodian;
                    seCashFundAmount.Text = Convert.ToDecimal(_Entity.CashFundAmount).ToString();
                    glGLAccountCode.Value = _Entity.GLAccountCode;
                    //glGLSubsiCode.DataBind();
                    glGLSubsiCode.Value = _Entity.GLSubsiCode;
                    glRecordingMethod.Value = _Entity.RecordingMethod;
                    chckInactive.Value = _Entity.IsInactive;

                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeActivatedBy;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;

                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;
                }
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
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
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
        #endregion

        #region Lookup Settings
       
     
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.FundCode = txtFundCode.Text;
            _Entity.FundDescription = txtFundDescription.Text;
            _Entity.Custodian = glCustodian.Text;
            _Entity.CashFundAmount = String.IsNullOrEmpty(seCashFundAmount.Text) ? 0 : Convert.ToDecimal(seCashFundAmount.Text);
            _Entity.GLAccountCode = glGLAccountCode.Text;
            _Entity.GLSubsiCode = glGLSubsiCode.Text;
            _Entity.RecordingMethod = glRecordingMethod.Text;
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

            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.LastEditedDate = txtLastEditedDate.Text;
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeActivatedBy = txtDeActivatedBy.Text;
            _Entity.DeActivatedDate = txtDeActivatedDate.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();

            switch (e.Parameter)
            {
                case "Add":
                        check = true;
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                        break;

                case "Update":
                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
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

                case "Accnt":
                    sdsSubsi.SelectCommand = "select SubsiCode, AccountCode, Description from Accounting.GLSubsiCode where ISNULL(IsInactive,0) = 0 AND AccountCode = '" + glGLAccountCode.Text + "'";
                    glGLSubsiCode.DataSourceID = "sdsSubsi";
                    glGLSubsiCode.DataBind();
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
        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsEmp.ConnectionString = Session["ConnString"].ToString();
            sdsCOA.ConnectionString = Session["ConnString"].ToString();
            sdsSubsi.ConnectionString = Session["ConnString"].ToString();
            sdsMethod.ConnectionString = Session["ConnString"].ToString();
        }
    }
}