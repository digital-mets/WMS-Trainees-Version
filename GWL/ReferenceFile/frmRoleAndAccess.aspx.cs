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
    public partial class frmRoleAndAccess : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.RoleAndAccessControl _Entity = new RoleAndAccessControl();//Calls entity PO
        Entity.RoleAndAccessControl.RolesDetail _EntityDetail = new RoleAndAccessControl.RolesDetail();//Call entity POdetails

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

            gv1.DataSource = null;

            if (!IsPostBack)
            {
				Gears.UseConnectionString(Session["ConnString"].ToString());
				
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                Session["icndetail"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";                  
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtRoleID.ReadOnly = true;
                        break;
                    case "V":                      
                        updateBtn.Text = "Close";
                        txtRoleID.ReadOnly = true;
                        view = true;
                        break;
                    case "D":                    
                        updateBtn.Text = "Delete";
                        txtRoleID.ReadOnly = true;
                        view = true;
                        break;
                }                               
                

                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                   
                //    gv1.DataSourceID = "sdsDetail";
                   
                //}
                //else
                if (Request.QueryString["entry"].ToString() != "N")
                {
                    txtRoleID.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtRoleID.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                    txtDescription.Text = _Entity.Description.ToString();
                    txtPanel.Text = _Entity.PanelName.ToString();
                    chkIsInactive.Value = _Entity.IsInactive;

                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    txtHAddedBy.Text = _Entity.AddedBy;
                    txtHAddedDate.Text = _Entity.AddedDate;
                    txtHLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHLastEditedDate.Text = _Entity.LastEditedDate;
                    txtHActivatedBy.Text = _Entity.ActivatedBy;
                    txtHActivatedDate.Text = _Entity.ActivatedDate;
                    txtHDeactivatedBy.Text = _Entity.DeactivatedBy;
                    txtHDeactivatedDate.Text = _Entity.DeactivatedDate;               
                
                    //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                    odsDetail.SelectParameters["RoleID"].DefaultValue = txtRoleID.Text;
                //    gv1.DataSourceID = "odsDetail";


                }

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT RoleID FROM IT.UserRolesDetail WHERE RoleID ='" + txtRoleID.Text + "'", Session["ConnString"].ToString());

                    gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
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
            look.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsBehavior.AllowSort = false;
            e.Editor.ReadOnly = view;
            
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
                e.Image.IconID = "tasks_newtask_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "tasks_newtask_16x16";
            }
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
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

        #region Lookup Settings
        //protected void lookup_Init(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
        //    if (Session["FilterExpression"] != null)
        //    {
        //        gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
        //        Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
        //        //Session["FilterExpression"] = null;
        //    }
        //}
        //public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string column = e.Parameters.Split('|')[0];//Set column name
        //    if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
        //    string itemcode = e.Parameters.Split('|')[1];//Set Item Code
        //    string val = e.Parameters.Split('|')[2];//Set column value
        //    if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

        //    ASPxGridView grid = sender as ASPxGridView;
        //    ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
        //    var selectedValues = itemcode;
        //    CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
        //    Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
        //    grid.DataSourceID = "Masterfileitemdetail";
        //    grid.DataBind();

        //    for (int i = 0; i < grid.VisibleRowCount; i++)
        //    {
        //        if (grid.GetRowValues(i, column) != null)
        //            if (grid.GetRowValues(i, column).ToString() == val)
        //            {
        //                grid.Selection.SelectRow(i);
        //                string key = grid.GetRowValues(i, column).ToString();
        //                grid.MakeRowVisible(key);
        //                break;
        //            }
        //    }
        //}

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.RoleID = txtRoleID.Text.ToUpper();
            _Entity.Description = txtDescription.Text;
            _Entity.PanelName = txtPanel.Text;
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Value.ToString());
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.Connection = Session["ConnString"].ToString();

            switch (e.Parameter)
            {
                case "Add":

               
                    if (error == false)
                    {
                        check = true;

                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.InsertData(_Entity);

                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["RoleID"].DefaultValue = txtRoleID.Text; //Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                       

                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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

                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["RoleID"].DefaultValue = txtRoleID.Text; //Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                       

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
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;

           
            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            if (error == false && check == false)
            {
                foreach (GridViewColumn column in gv1.Columns)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName == "ModuleID" && dataColumn.FieldName == "Access")
                    {
                        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                    }
                }

                if (e.Errors.Count > 0)
                {
                    error = true; //bool to cancel adding/updating if true
                }
            }
        }

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        ////dictionary method to hold error 
        //void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        //{
        //    if (errors.ContainsKey(column)) return;
        //    errors[column] = errorText;
        //}


  

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            sdsModuleID.ConnectionString = Session["ConnString"].ToString();
            sdsAcess.ConnectionString = Session["ConnString"].ToString();
            sdsBizPartner.ConnectionString = Session["ConnString"].ToString();
        }

        #endregion
    }
}