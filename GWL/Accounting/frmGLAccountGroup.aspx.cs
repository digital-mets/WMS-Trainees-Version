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
    public partial class frmGLAccountGroup : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.GLAccountGroup _Entity = new GLAccountGroup();//Calls entity odsHeader

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
                        glcheck.ClientVisible = false;
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


                txtGroupCode.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtGroupCode.Text, Session["ConnString"].ToString());

                txtGroupCode.Value = _Entity.GroupCode;
                txtLevel.Value = _Entity.GroupLevel;
                txtDesc.Value = _Entity.Description;
                txtAccFr.Value = _Entity.AccountFr;
                txtAccTo.Value = _Entity.AccountTo;

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
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); 
            _Entity.GroupCode = txtGroupCode.Text;
            _Entity.GroupLevel = txtLevel.Text;
            _Entity.Description = txtDesc.Text;
            _Entity.AccountFr = txtAccFr.Text;
            _Entity.AccountTo = txtAccTo.Text;

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

                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.AddedDate = DateTime.Now.ToString();

                        _Entity.InsertData(_Entity);

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

                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.LastEditedDate = DateTime.Now.ToString();

                        _Entity.UpdateData(_Entity);

                        //Validate();
                        
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
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {

        }
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}