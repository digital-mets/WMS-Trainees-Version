﻿using DevExpress.Spreadsheet;
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
    public partial class frmCompany : System.Web.UI.Page
    {
        Boolean view = false;       //Boolean for view state

        Company _Entity = new Company();

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
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtCompanyCode.Enabled = false;
                        break;
                    case "V":
                        view = true;//sets view only mode
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        view = true;//sets view only mode
                        updateBtn.Text = "Delete";
                        break;
                }

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    int intResult = _Entity.GetObject(Request.QueryString["docnumber"].ToString()); //Method for retrieving data from entity

                    if (intResult == 0)
                    {
                        txtCompanyCode.Text = _Entity.CompanyCode;
                        txtCompanyName.Text = _Entity.CompanyName;
                        txtShortName.Text = _Entity.ShortName;
                        txtPercentShare.Value = _Entity.PercentShare;
                        aglCompanyGroup.Value = _Entity.CompanyGroup;
                        cbxBusinessType.Value = _Entity.BusinessType;
                        txtSortOrder.Value = _Entity.SortOrder;
                        txtDatabaseName.Text = _Entity.DatabaseName;
                        chkInactive.Value = _Entity.IsInactive;

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
                    else
                    {
                        Response.Redirect("~/error.aspx");
                    }
                }
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void Textbox_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Lookup_Load(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.ReadOnly = view;
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
            if (view) { date.ClearButton.Visibility = AutoBoolean.False; }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        #endregion

        protected void Checkbox_Load(object sender, EventArgs e)
        {
            ASPxCheckBox checkbox = sender as ASPxCheckBox;
            checkbox.ReadOnly = view;
        }

        protected void Combobox_Load(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
            if (view) { combobox.ClearButton.Visibility = AutoBoolean.False; }
        }

        #region Lookup Settings

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            string strErr = "";

            _Entity.CompanyCode = txtCompanyCode.Text.ToUpper();
            _Entity.CompanyName = txtCompanyName.Text.ToUpper();
            _Entity.ShortName = txtShortName.Text.ToUpper();
            _Entity.PercentShare = Convert.ToDecimal(txtPercentShare.Text);
            _Entity.CompanyGroup = (aglCompanyGroup.Value == null) ? null : aglCompanyGroup.Value.ToString();
            _Entity.BusinessType = (cbxBusinessType.Value == null) ? null : cbxBusinessType.Value.ToString();
            _Entity.SortOrder = Convert.ToInt16(txtSortOrder.Text);
            _Entity.DatabaseName = txtDatabaseName.Text.ToUpper();
            _Entity.IsInactive = Convert.ToBoolean(chkInactive.Value);

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
                    _Entity.AddedBy = Session["userid"].ToString();
                    strErr = _Entity.InsertData();

                    if (strErr == "")
                    {
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = strErr;
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;
                case "Update":
                    _Entity.LastEditedBy = Session["userid"].ToString();
                    strErr = _Entity.UpdateData();

                    if (strErr == "")
                    {
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = strErr;
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;
                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;
                case "ConfDelete":
                    strErr = _Entity.DeleteData();

                    if (strErr == "")
                    {
                        cp.JSProperties["cp_close"] = true;
                        cp.JSProperties["cp_message"] = "Successfully deleted";
                        cp.JSProperties["cp_success"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = strErr;
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        #endregion
    }
}