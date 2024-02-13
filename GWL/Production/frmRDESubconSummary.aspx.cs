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
using GearsProcurement;
using System.Data.SqlClient;

using System.IO;
using System.Windows.Forms;

namespace GWL
{
    public partial class frmRDESubconSummary : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state
        static DataTable dt;  

        private static string strError; 
        #region page load/entry

        protected void Page_PreInit(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            foreach (System.Web.UI.Control c in form2.Controls)
            {
                if (c is SqlDataSource)
                {
                    ((SqlDataSource)c).ConnectionString = Session["ConnString"].ToString();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString()); 

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (!IsPostBack)
            {
                dt = null;
                Session["RDEtable2"] = null;
                Session["RDEtable"] = null;
            }

            if (Session["RDEtable2"] != null)
            {
                gv2.DataSource = (DataTable)Session["RDEtable2"];
                gv2.DataBind();
            }

            if (Session["RDEtable"] != null)
            {
                gv.DataSource = (DataTable)Session["RDEtable"];
                gv.DataBind();
            }

        }
        #endregion

        #region Set controls' state/behavior/etc... 
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

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break; 
            } 

            dt = new DataTable();
            dt = Gears.RetriveData2("exec sp_rde_SubconSummary '" + ASPxDateEdit1.Text + "','" + ASPxDateEdit2.Text + "','" + glWorkCenter.Text + "','" + glJobOrder.Text + "','" + glStepCode.Text + "'", Session["ConnString"].ToString()); 

            gv.Columns.Clear();
            gv2.Columns.Clear(); 
            Session["RDEtable2"] = null;
            Session["RDEtable"] = dt;
            gv.DataSource = dt;
            gv.DataBind();
            gv2.Visible = false;
            gv.Visible = true;
            return;  
        }
        #endregion


        protected void IDExportToCSV_Click(object sender, EventArgs e)
        {
            #region Exporting
            if (Session["RDEtable2"] != null) return;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=\"Subcon Summary (Raw Data Extraction)\".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            if (dt.Rows.Count > 0)
            { 
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Columns[k].ColumnName + ',');
                }
                //append new line
                sb.Append("\r\n");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }

                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                cp.JSProperties["cp_message"] = "No data extracted!";
            }

            #endregion
        }  

        #region Inits 
        protected void Connection_Init(object sender, EventArgs e)
        { 
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        #endregion 
    }
}