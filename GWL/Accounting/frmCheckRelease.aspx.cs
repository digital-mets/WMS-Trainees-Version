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
using System.Data.SqlClient;

namespace GWL
{
    public partial class frmCheckRelease : System.Web.UI.Page
    {
        Boolean view = false;       //Boolean for view state

        CheckVoucher _Entity = new CheckVoucher();

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
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

            dteDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());


            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                String strParam = Request.QueryString["parameters"].ToString().ToUpper();
                String strEntry = Request.QueryString["entry"].ToString();
                String strTitle = Request.QueryString["transtype"].ToString();

                switch (strEntry)
                {
                    //case "N":
                    //    updateBtn.Text = "Add";
                    //    break;
                    case "E":
                        updateBtn.Text = "Release";
                        break;
                    //case "V":
                    //    view = true;//sets view only mode
                    //    updateBtn.Text = "Close";
                    //    break;
                    //case "D":
                    //    view = true;//sets view only mode
                    //    updateBtn.Text = "Delete";
                    //    break;
                }

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    DataTable dtTemp = Gears.RetriveData2("SELECT CV.DocNumber, CV.DocDate, CV.SupplierCode, BP.Name AS SupplierName "
                                                         +"FROM Accounting.CheckVoucher CV LEFT JOIN Masterfile.BizPartner BP "
                                                         +"ON CV.SupplierCode = BP.BizPartnerCode "
                                                         +"WHERE CV.DocNumber = '"+Request.QueryString["docnumber"].ToString()+"'"
                                                         , Session["ConnString"].ToString());

                    if (dtTemp != null)
                    {
                        sdsCheckDetail.SelectParameters["DocNumber"].DefaultValue = Request.QueryString["docnumber"].ToString();
                        //sdsCheckDetail.DataBind();

                        txtDocNumber.Text = dtTemp.Rows[0]["DocNumber"].ToString();
                        dteDocDate.Value = Convert.ToDateTime(dtTemp.Rows[0]["DocDate"]);
                        dteReleaseDate.Value = DateTime.Today;
                        txtSupplierCode.Text = dtTemp.Rows[0]["SupplierCode"].ToString();
                        txtSupplierName.Value = dtTemp.Rows[0]["SupplierName"].ToString();
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

        protected void Memo_Load(object sender, EventArgs e)//Control for all memo
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }

        #region Lookup Settings

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == "Close")
            {
                cp.JSProperties["cp_close"] = true;
            }
            else
            {
                string strErrMssg = "";
                try
                {
                    using (SqlConnection sqlconn = new SqlConnection(Session["ConnString"].ToString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_ReleaseCheck"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@DocNumber", txtDocNumber.Text));
                            cmd.Parameters.Add(new SqlParameter("@ReleaseDate", Convert.ToDateTime(dteReleaseDate.Value)));
                            cmd.Parameters.Add(new SqlParameter("@ORNumber", txtORNumber.Text));
                            cmd.Parameters.Add(new SqlParameter("@ORDate", Convert.ToDateTime(dteORDate.Value)));
                            cmd.Parameters.Add(new SqlParameter("@Remarks", txtRemarks.Text));
                            sqlconn.Open();
                            cmd.Connection = sqlconn;
                            strErrMssg = cmd.ExecuteScalar().ToString();
                            sqlconn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    strErrMssg = ex.Message;
                }
                if (strErrMssg == "")
                {
                    cp.JSProperties["cp_message"] = "Check voucher has been updated!";
                    cp.JSProperties["cp_success"] = true;
                    cp.JSProperties["cp_close"] = true;
                }
                else
                {
                    cp.JSProperties["cp_message"] = strErrMssg;
                    cp.JSProperties["cp_success"] = true;
                }
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        #endregion
    }
}