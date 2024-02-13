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
    public partial class frmInventoryForecastLevel : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.InventoryForecastLevel _Entity = new InventoryForecastLevel();//Calls entity ICN

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            sdsYear.SelectCommand = ";WITH YearList AS ("
                    + " SELECT 2000 AS Year"
                    + " UNION ALL"
                    + " SELECT yl.Year + 1 AS Year"
                    + " FROM yearlist yl"
                    + "  WHERE yl.Year + 1 <= YEAR(GetDate()))"
                    + " SELECT Year FROM YearList ORDER BY Year DESC";
            aglYear.DataBind();

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

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                }
                else
                {
                    _Entity.getdata(Request.QueryString["docnumber"].ToString());
                    aglYear.Value = _Entity.Year.ToString();
                    cmbInventoryLevel.Text = _Entity.InventoryLevel.ToString();
                    speValue.Text = _Entity.InventoryLevelVal.ToString();
                    speLevel.Text = _Entity.ForecastLevel.ToString();
                    aglField.Text = _Entity.Field.ToString();
                    cmbFigure.Text = _Entity.ForecastFigure.ToString();

                    txtHField1.Value = _Entity.Field1.ToString();
                    txtHField2.Value = _Entity.Field2.ToString();
                    txtHField3.Value = _Entity.Field3.ToString();
                    txtHField4.Value = _Entity.Field4.ToString();
                    txtHField5.Value = _Entity.Field5.ToString();
                    txtHField6.Value = _Entity.Field6.ToString();
                    txtHField7.Value = _Entity.Field7.ToString();
                    txtHField8.Value = _Entity.Field8.ToString();
                    txtHField9.Value = _Entity.Field9.ToString();

                    txtHAddedBy.Text = _Entity.AddedBy;
                    txtHAddedDate.Text = _Entity.AddedDate;
                    txtHLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHLastEditedDate.Text = _Entity.LastEditedDate;
                    txtHActivatedBy.Text = _Entity.ActivatedBy;
                    txtHActivatedDate.Text = _Entity.ActivatedDate;
                    txtHDeActivatedBy.Text = _Entity.DeactivatedBy;
                    txtHDeActivatedDate.Text = _Entity.DeactivatedDate;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        aglYear.ReadOnly = true;
                        aglYear.DropDownButton.Enabled = false;
                        break;
                    case "V":
                        view = true;
                        aglYear.ReadOnly = true;
                        aglYear.DropDownButton.Enabled = false;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        aglYear.ReadOnly = true;
                        aglYear.DropDownButton.Enabled = false;
                        updateBtn.Text = "Delete";
                        break;
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
            look.ReadOnly = view;
            look.DropDownButton.Enabled = !view;
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
        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
            combo.DropDownButton.Enabled = !view;
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

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.RecordID = Request.QueryString["docnumber"].ToString();
            _Entity.Year = Convert.ToInt16(aglYear.Text);
            _Entity.InventoryLevel = cmbInventoryLevel.Text;
            _Entity.InventoryLevelVal = String.IsNullOrEmpty(speValue.Text) ? 0 : Convert.ToDecimal(speValue.Text);
            _Entity.ForecastLevel = Convert.ToInt16(speLevel.Text);
            _Entity.Field = aglField.Text;
            _Entity.ForecastFigure = cmbFigure.Text;

            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString(); 
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            switch (e.Parameter)
            {
                case "Add":

                    if (error == false)
                    {
                        check = true;

                        DataTable addforecast = Gears.RetriveData2("SELECT * FROM Inventory.ForecastLevel WHERE Year = '" + aglYear.Text + "' AND "
                                            + " Field = '" + aglField.Value.ToString() + "' AND ForecastLevel = '" + speLevel.Text + "'");

                        //DataTable addforecast1 = Gears.RetriveData2("SELECT * FROM Inventory.ForecastLevel WHERE Year = '" + aglYear.Text + "' AND "
                        //                    + " ForecastLevel = '" + speLevel.Text + "'");

                        if ((addforecast.Rows.Count > 0))
                        {
                            cp.JSProperties["cp_message"] = "Cannot set duplicate forecast level!";
                            cp.JSProperties["cp_success"] = true;
                        }
                        else
                        {
                            DataTable others = Gears.RetriveData2("SELECT ColumnName FROM IT.ForecastField WHERE Field = '" + aglField.Text + "'");

                            _Entity.ColumnName = others.Rows[0]["ColumnName"].ToString();
                            _Entity.Connector = others.Rows[0]["ColumnName"].ToString();                            

                            _Entity.InsertData(_Entity);//Method of inserting for header
                            cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side

                            cp.JSProperties["cp_close"] = true;//Close window variable to client side
                            Session["Refresh"] = "1";
                        }
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    if (error == false)
                    {
                        check = true;

                        DataTable updateforecast = Gears.RetriveData2("SELECT * FROM Inventory.ForecastLevel WHERE Year = '" + aglYear.Text + "' AND "
                                            + " Field = '" + aglField.Value.ToString() + "' AND ForecastLevel = '" + speLevel.Text + "'");

                        //DataTable updateforecast1 = Gears.RetriveData2("SELECT * FROM Inventory.ForecastLevel WHERE Year = '" + aglYear.Text + "' AND "
                        //                    + " ForecastLevel = '" + speLevel.Text + "'");

                        if ((updateforecast.Rows.Count > 0))
                        {
                            cp.JSProperties["cp_message"] = "Cannot set duplicate forecast level!";
                            cp.JSProperties["cp_success"] = true;
                        }
                        else
                        {
                            DataTable others = Gears.RetriveData2("SELECT ColumnName FROM IT.ForecastField WHERE Field = '" + aglField.Text + "'");

                            _Entity.ColumnName = others.Rows[0]["ColumnName"].ToString();
                            _Entity.Connector = others.Rows[0]["ColumnName"].ToString();  

                            _Entity.UpdateData(_Entity);//Method of inserting for header
                            cp.JSProperties["cp_message"] = "Successfully Updated!";//Message variable to client side

                            cp.JSProperties["cp_close"] = true;//Close window variable to client side
                            Session["Refresh"] = "1";
                        }
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
    }
}