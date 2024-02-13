using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using GearsLibrary;
using DevExpress.Web;
using System.IO;

namespace GWL.WebControl
{
    public partial class wc_UploadDocsPopup : System.Web.UI.UserControl
    {
        public string HeaderText
        {
            get { return DocRmkPopup.HeaderText; }
            set { DocRmkPopup.HeaderText = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initialize popup parameter
            if (!IsPostBack && Visible)
            {
            }
            if (Visible)
            {
                // gvDetail.DataBind();
            }
            #endregion
        }

        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (txtRmkType.Text == "INCORRECT" && String.IsNullOrEmpty(memRemarks.Text))
            {
                cp.JSProperties["cp_errormssg"] = "Please provide all required parameters";
            }
            else
            {
                string strErrmssg;
                if (txtRmkType.Text == "INCORRECT")
                {
                    strErrmssg = ExecuteSQL(
                        "UPDATE IT.Documents SET MissingDocs = '" + memRemarks.Text + "', Rejected = 1, DateRejected = GETDATE() " +
                        "WHERE TransType = '" + txtTransType.Text + "' AND DocNumber = '" + txtDocNumber.Text + "' AND ISNULL(Verified,0) = 0");
                }
                else
                {
                    strErrmssg = ExecuteSQL(
                        "UPDATE IT.Documents SET MissingDocs = '" + txtMissingDocs.Text + "' " +
                        "WHERE TransType = '" + txtTransType.Text + "' AND DocNumber = '" + txtDocNumber.Text + "' AND ISNULL(Complete,0) = 0");
                }
                if (strErrmssg != "")
                {
                    cp.JSProperties["cp_errormssg"] = strErrmssg;
                }
                else
                {
                    cp.JSProperties["cp_success"] = true;
                }
            }
        }

        protected String ExecuteSQL(String strSQLCmd)
        {
            String strErrMssg = "";
            using (SqlConnection sqlconn = new SqlConnection(Session["ConnString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(strSQLCmd))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 600;
                        sqlconn.Open();
                        cmd.Connection = sqlconn;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        strErrMssg = ex.ToString();
                    }
                    sqlconn.Close();
                }
            }

            return strErrMssg;
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}