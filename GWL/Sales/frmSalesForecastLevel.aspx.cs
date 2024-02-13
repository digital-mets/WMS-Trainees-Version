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
using GearsWarehouseManagement;

namespace GWL
{
    public partial class frmSalesForecastLevel : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        private static string Connection;

        Entity.SalesForecastLevel _Entity = new SalesForecastLevel();//Calls entity ICN

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection = Session["ConnString"].ToString();

            string referer;
            try //Validation to restrict user to browse/type directly to browser's address
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

            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
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
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }



                //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    // gv1.DataSourceID = "sdsDetail";

                    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                }
                else
                {
                    _Entity.getdata(Request.QueryString["docnumber"].ToString(),Session["ConnString"].ToString());
                    year.Text = _Entity.Year.ToString();
                    field.Text = _Entity.Field;
                    level.Text = _Entity.ForecastLevel.ToString();
                    forfig.Text = _Entity.ForecastFigure;
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
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
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
                if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        #endregion

        #region Lookup Settings


        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

            _Entity.Year = Convert.ToInt16(year.Text);
            _Entity.ForecastLevel = Convert.ToInt16(level.Text);
            _Entity.ForecastFigure = forfig.Text;
            _Entity.Field = field.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.LastEditedBy = Session["userid"].ToString();
            if (Request.QueryString["docnumber"].ToString()!="undefined")
            _Entity.RecordID = Convert.ToInt16(Request.QueryString["docnumber"].ToString());

            DataTable checkdup = Gears.RetriveData2("select * from sales.forecastlevel where Year='" + year.Text + "' and "
                                        + "Field = '" + field.Value.ToString() + "'", Connection);

            DataTable checkdup2 = Gears.RetriveData2("select * from sales.forecastlevel where ForecastLevel='" + level.Text + "'" +
                                  " and Year='" + year.Text + "'", Connection);

            switch (e.Parameter)
            {
                case "Add":

                    check = true;
                    if (level.Text == "1" && field.Value.ToString() != "None")
                    {
                        cp.JSProperties["cp_message"] = "Cannot set level to 1!";
                        cp.JSProperties["cp_success"] = true;
                        return;
                    }
                    
                    if (checkdup.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_message"] = "Cannot set duplicate forecast level!";
                        cp.JSProperties["cp_success"] = true;
                    }    
                    else if (checkdup2.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_message"] = "Cannot set duplicate forecast level!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    else
                    {
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side

                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }


                    break;

                case "Update":
                    if (level.Text == "1" && field.Value.ToString() != "None")
                    {
                        cp.JSProperties["cp_message"] = "Cannot set level to 1!";
                        cp.JSProperties["cp_success"] = true;
                        return;
                    }

                    if (checkdup.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_message"] = "Cannot set duplicate forecast level!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    else if (checkdup2.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_message"] = "Cannot set duplicate forecast level!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    else{
                        Gears.RetriveData2("delete from sales.forecast where Year='" + year.Text + "'", Connection);
                        _Entity.UpdateData(_Entity);//Method of inserting for header



                        // odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error

                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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



        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        #endregion

        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "WMSIA";

            //string strresult = GWarehouseManagement.PickList_Validate(gparam);

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion


        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}