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
    public partial class frmCheckUnRelease : System.Web.UI.Page
    {
        Boolean view = false;
        CheckVoucher _Entity = new CheckVoucher();

        #region PAGE LOAD
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

            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;

                DataTable dtTemp = Gears.RetriveData2(" SELECT CV.DocNumber, CV.DocDate, CV.SupplierCode, BP.Name AS SupplierName, "
                                                    + " CV.ORNo AS ORNumber, CV.ORDate, CV.TransType AS PaymentType, CV.ReleasingRemarks "
                                                    + " FROM Accounting.CheckVoucher CV LEFT JOIN Masterfile.BizPartner BP "
                                                    + " ON CV.SupplierCode = BP.BizPartnerCode WHERE CV.DocNumber = '"
                                                    + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());

                if (dtTemp != null)
                {
                    sdsCheckDetail.SelectParameters["DocNumber"].DefaultValue = Request.QueryString["docnumber"].ToString();
                    txtDocNumber.Text = dtTemp.Rows[0]["DocNumber"].ToString();
                    txtDocDate.Text = dtTemp.Rows[0]["DocDate"].ToString();
                    txtORNumber.Text = dtTemp.Rows[0]["ORNumber"].ToString();
                    txtORDate.Text = dtTemp.Rows[0]["ORDate"].ToString();
                    txtPaymentType.Text = dtTemp.Rows[0]["PaymentType"].ToString();
                    txtMemo.Text = dtTemp.Rows[0]["ReleasingRemarks"].ToString();
                    txtSupplierCode.Text = dtTemp.Rows[0]["SupplierCode"].ToString();
                    txtSupplierName.Text = dtTemp.Rows[0]["SupplierName"].ToString();
                }
                else
                {
                    Response.Redirect("~/error.aspx");
                }
            }
        }
        #endregion

        #region GRID CONTROL
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsBehavior.AllowSort = false;
        }
        protected void gv1_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Cancel || e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }
        #endregion

        #region CALLBACK
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            string[] keycode = new string[] {"RecordID"};
            List<object> fieldValues = gv1.GetSelectedFieldValues(keycode);
        
            string strErrMssg = "";
            try
            {
                using (SqlConnection sqlconn = new SqlConnection(Session["ConnString"].ToString()))
                {
                    foreach (int item in fieldValues)
                    {
                        Gears.RetriveData2("EXEC sp_Unrelease_Check '" + item.ToString() + "'", Session["ConnString"].ToString());
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

        protected void gv1_DataBound(object sender, EventArgs e)//Creation of checkbox column on runtime
        {
            ASPxGridView grid = sender as ASPxGridView;
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Name = "SelectCB";
            col.ShowSelectCheckbox = true;
            col.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;
            col.VisibleIndex = 0;
            col.Width = 50;
            col.FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns.Add(col);
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        #endregion
    }
}