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
using GearsTrading;
using DevExpress.Data.Filtering;

namespace GWL
{
    public partial class frmMasterManPower : System.Web.UI.Page
    {
        Boolean error = false;      //Boolean for grid validation
        Boolean view = false;       //Boolean for view state

        MasterManPower _Entity = new MasterManPower();

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
                        txtYear.ClientEnabled = false;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gvDetail.DataSourceID = "sdsDetail";
                }
                else
                {
                    txtYear.Value = Convert.ToInt32(Request.QueryString["docnumber"]);

                    odsDetail.SelectParameters["_Year"].DefaultValue = txtYear.Text;
                    gvDetail.DataSourceID = "odsDetail";
                }
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        protected void Textbox_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Lookup_Load(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.ReadOnly = view;
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

        #region Grid Related Events
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "tasks_newtask_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "tasks_newtask_16x16";
            }
            if (Request.QueryString["entry"].ToString() == "V"
                || Request.QueryString["entry"].ToString() == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
            {
                e.Visible = false;
            }
        }
        protected void gv_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" 
                || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }
        #endregion

        #region Grid with columns having lookup
        protected void gv_LookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup lookup = sender as ASPxGridLookup;
                lookup.Enabled = false;
            }
        }
        #endregion
        
        #region ODS related events
        protected void odsDetail_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            Entity.MasterManPower _entity = e.InputParameters["_Record"] as Entity.MasterManPower;
            _entity.Year = Convert.ToInt16(txtYear.Text);
            _entity.AddedBy = Session["userid"].ToString();
        }
        protected void odsDetail_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            Entity.MasterManPower _entity = e.InputParameters["_Record"] as Entity.MasterManPower;
            _entity.LastEditedBy = Session["userid"].ToString();
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    if (error == false)
                    {
                        // Re-bind to ods in case sds has been used
                        gvDetail.DataSourceID = "odsDetail";
                        // Refresh SelectParameters[] which is used for refresh after UpdateEdit()
                        odsDetail.SelectParameters["_Year"].DefaultValue = txtYear.Text;
                        gvDetail.UpdateEdit();

                        if (e.Parameter == "Add")
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        else
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
                    _Entity.DeleteRecords(txtYear.Value.ToString(), Session["userid"].ToString());
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    cp.JSProperties["cp_success"] = true;
                    Session["Refresh"] = "1";
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;
            }
        }
        #endregion
    }
}