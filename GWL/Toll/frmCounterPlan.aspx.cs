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
using GearsProduction;

namespace GWL
{
    public partial class frmCounterPlan : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string Cascade = "";
        string TransNo = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void de_Init(object sender, EventArgs e)
        {
            MachineCPYear.Date = DateTime.Today;
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }

        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e) // Gridview CommandButton
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit ||
                        e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                }

                if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        if (Session["JOAlloc"] != null)
                        {
                            e.Visible = false;
                        }
                        else
                        {
                            e.Visible = true;
                        }
                    }
                }

                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }
                }
            }
        }

        protected void gv_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e) // Gridview CustomButton
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "N")
                {
                    if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }

                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonID == "Delete" || e.ButtonID == "ProductDelete" || e.ButtonID == "BOMDelete" || e.ButtonID == "StepsDelete"
                         || e.ButtonID == "PlanDelete" || e.ButtonID == "ClassDelete" || e.ButtonID == "SizeDelete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
                else
                {
                    if (e.ButtonID == "Delete" || e.ButtonID == "ProductDelete" || e.ButtonID == "BOMDelete" || e.ButtonID == "StepsDelete"
                            || e.ButtonID == "ClassDelete" || e.ButtonID == "SizeDelete")
                    {
                        if (Session["JOAlloc"] != null)
                        {
                            e.Visible = DevExpress.Utils.DefaultBoolean.False;
                        }
                        else
                        {
                            e.Visible = DevExpress.Utils.DefaultBoolean.True;
                        }
                    }
                    else if (e.ButtonID == "PlanDelete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }

                }
            }
        }

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        { 
        }
        #endregion
    }
}