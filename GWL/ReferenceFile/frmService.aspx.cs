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
using DevExpress.Data.Filtering;


namespace GWL
{
    public partial class frmService : System.Web.UI.Page
    {
        Boolean view = false;//Boolean for view state

        Entity.Service _Entity = new Service();

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
                        txtServiceCode.ReadOnly = true;
                        glcheck.ClientVisible = true;
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        txtServiceCode.ReadOnly = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        txtServiceCode.ReadOnly = true;
                        break;
                }

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    txtServiceCode.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtServiceCode.Text, Session["ConnString"].ToString());
                    txtDescription.Value = _Entity.Description;
                    aglType.Value = _Entity.Type.ToString();
                    aglAccountCode.Value = _Entity.AccountCode.ToString();
                    aglSubsiCode.Value = _Entity.SubsiCode.ToString();
                    aglVAT.Value = _Entity.VATCode.ToString();
                    aglATC.Value = _Entity.ATCCode.ToString();
                    chkIsVatable.Value = _Entity.IsVatable;
                    chkIsCore.Value = _Entity.IsCore;
                    chkAllowProg.Value = _Entity.IsAllowBilling;
                    chkIsInactive.Value = _Entity.IsInactive;

                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeActivatedBy;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;

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
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void Check_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.ServiceCode = txtServiceCode.Text;
            _Entity.Description = txtDescription.Text;
            _Entity.Type = String.IsNullOrEmpty(aglType.Text) ? null : aglType.Value.ToString();
            _Entity.AccountCode = String.IsNullOrEmpty(aglAccountCode.Text) ? null : aglAccountCode.Value.ToString();
            _Entity.SubsiCode = String.IsNullOrEmpty(aglSubsiCode.Text) ? null : aglSubsiCode.Value.ToString();
            _Entity.VATCode = String.IsNullOrEmpty(aglVAT.Text) ? null : aglVAT.Value.ToString();
            _Entity.ATCCode = String.IsNullOrEmpty(aglATC.Text) ? null : aglATC.Value.ToString();
            _Entity.IsVatable = Convert.ToBoolean(chkIsVatable.Value.ToString());
            _Entity.IsCore = Convert.ToBoolean(chkIsCore.Value.ToString());
            _Entity.IsAllowBilling = Convert.ToBoolean(chkAllowProg.Value.ToString());

            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.Connection = Session["ConnString"].ToString();

            switch (e.Parameter)
            {
                case "Add":
                        _Entity.InsertData(_Entity);
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    break;

                case "Update":
                        _Entity.UpdateData(_Entity);
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    break;

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
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
        }


    }
}