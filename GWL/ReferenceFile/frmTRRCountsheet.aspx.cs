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
    public partial class frmTRRCountsheet : System.Web.UI.Page
    {
        Boolean view = false;//Boolean for view state

        Entity.TRRSetupCountsheet _Entity = new TRRSetupCountsheet();

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
            
            updateBtn.Text = "Update";
            if (!IsPostBack)
            {
                switch (Request.QueryString["entry"].ToString())
                {
                    case "E":
                        glcheck.ClientVisible = true;
                        break;
                    case "V":
                        view = true;
                        break;
                }

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    txtRecordID.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtRecordID.Text, Session["ConnString"].ToString());
                    txtDocNumber.Value = _Entity.DocNumber;
                    txtItemCode.Value = _Entity.ItemCode;
                    txtColorCode.Value = _Entity.ColorCode;
                    txtClassCode.Value = _Entity.ClassCode;
                    txtSizeCode.Value = _Entity.SizeCode;
                    txtRollID.Value = _Entity.RollID;
                    txtRollQty.Value = _Entity.RollQty;
                    txtRemainingQty.Value = _Entity.RemainingQty;
                    txtWidth.Value = _Entity.Width;
                    txtShade.Value = _Entity.Shade;
                    txtShrinkage.Value = _Entity.Shrinkage;
                    
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    
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
            _Entity.RecordID = txtRecordID.Text;
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.ItemCode = txtItemCode.Text;
            _Entity.ColorCode = txtColorCode.Text;
            _Entity.ClassCode = txtClassCode.Text;
            _Entity.RollID = txtRollID.Text;
            _Entity.RollQty = Convert.ToDecimal(Convert.IsDBNull(txtRollQty.Value) ? 0 : Convert.ToDecimal(txtRollQty.Value));
            _Entity.RemainingQty = Convert.ToDecimal(Convert.IsDBNull(txtRemainingQty.Value) ? 0 : Convert.ToDecimal(txtRemainingQty.Value)); 
            _Entity.Width = txtWidth.Text;
            _Entity.Shade = txtShade.Text;
            _Entity.Shrinkage = txtShrinkage.Text;

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
                case "Update":
                        _Entity.UpdateData(_Entity);
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;
            }
        }
        #endregion
    }
}