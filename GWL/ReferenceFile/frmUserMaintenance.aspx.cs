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
    public partial class frmUserMaintenance : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        private static int lastGeneratedNumber = 999999;

        Entity.UserMaintenance _Entity = new UserMaintenance();//Calls entity PO
        Entity.UserMaintenance.UserMaintenanceDetail _EntityDetail = new UserMaintenance.UserMaintenanceDetail();//Call entity POdetails

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
                string inputValue = "123";

                txtuserid.Text = inputValue.PadLeft(6, '0');
                
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                Session["icndetail"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        int newNumber = lastGeneratedNumber + 1;
                        
                        lastGeneratedNumber = newNumber;

                        DataTable CreateNewID = Gears.RetriveData2("SELECT Prefix + REPLICATE('0', Serieswidth - len(SeriesNumber+1)) + cast(SeriesNumber+1 as varchar)  FROM IT.DocNumberSettings WHERE Module = 'ITUSERS' AND Prefix = '0'", Session["ConnString"].ToString());

                        if (CreateNewID.Rows.Count == 1)
                        {
                            foreach (DataRow dtrow in CreateNewID.Rows)
                            {
                                
                                txtuserid.Text = dtrow[0].ToString();
                            }
                        }
                      
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtuserid.ReadOnly = true;
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        txtuserid.ReadOnly = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        txtuserid.ReadOnly = true;
                        break;
                }

               
                if (Request.QueryString["entry"].ToString() == "N")
                {
                   
                    gv1.DataSourceID = "sdsDetail";

                   
                }
                else
                {
                    _Entity.getdata(Request.QueryString["docnumber"].ToString(), Session["ConnString"].ToString()); //Method for retrieving data from entity
                    txtuserid.Text = _Entity.UserID;
                    txtUserName.Text = _Entity.UserName;
                    txtFullname.Text = _Entity.FullName;
                    dtpbirthdate.Value = _Entity.BirthDate;
                    txtemailaddress.Text = _Entity.EmailAddress;
                    txtpassword.Text = _Entity.Password;
                    txtSavedPW.Text = _Entity.Password;
                    aglsq.Value = _Entity.SecurityQuestion;
                    txtsecanswer.Text    = _Entity.SecurityAnswer;
                    txtorgchartentity.Text = _Entity.OrgChartEntity;
                    aglBizPartnerCode.Value = _Entity.BizPartnerCode;
                    cmbStorerKey.Text = _Entity.ClientCodes;
                    cbxusertype.Text = _Entity.UserType;

                    //customerCode.Text = _Entity.CustomerCode;

                    algCompany.Text = _Entity.CompanyCode;
                    chkisuser.Value = Convert.ToBoolean(_Entity.IsUser);
                    //_Entity.SecurityQuestion 
                    //_Entity.SecurityAnswer 
                    //_Entity.OrgChartEntity 
                    //_Entity.BizPartnerCode 
                    //_Entity.IsUser 
               
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;
               
                
                    //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                    odsDetail.SelectParameters["UserID"].DefaultValue = txtuserid.Text;
                    gv1.DataSourceID = "odsDetail";
                 

                }

         
                // if (Request.QueryString["iswithdetail"].ToString() == "true" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.KeyFieldName = "EntityCode;KPICode";
                //    gv1.DataSourceID = "odsDetail";
                //}
                if (Request.QueryString["entry"].ToString() == "V")
                {
                  
                    gv1.DataSourceID = "odsDetail";
                
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
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
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
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.SpinButtons.Enabled = !view;
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

        protected void CheckBox_Load(object sender, EventArgs e)
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }


        #endregion

        #region Lookup Settings
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
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
            _Entity.UserID = txtuserid.Text;
            _Entity.UserName = txtUserName.Text;
            _Entity.FullName = txtFullname.Text;
            _Entity.BirthDate = dtpbirthdate.Text;
            _Entity.EmailAddress = txtemailaddress.Text;
            _Entity.Password = txtpassword.Text;
            _Entity.SavedPW = txtSavedPW.Text;
            _Entity.SecurityQuestion = aglsq.Text;
            _Entity.SecurityAnswer = txtsecanswer.Text;
            _Entity.OrgChartEntity = txtorgchartentity.Text;
            _Entity.BizPartnerCode = aglBizPartnerCode.Text;
            _Entity.CompanyCode = algCompany.Text;
            _Entity.ClientCodes = cmbStorerKey.Text;
            _Entity.UserType = cbxusertype.Text;
            _Entity.IsUser = Convert.ToBoolean(chkisuser.Value);
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

                        DataTable checkusers = Gears.RetriveData2("exec sp_CheckUsersCap", Session["ConnString"].ToString());
                        if (checkusers.Rows.Count == 1)
                        {
                            foreach (DataRow dtrow in checkusers.Rows)
                            {
                                cp.JSProperties["cp_message"] = dtrow[0].ToString();
                                cp.JSProperties["cp_success"] = true;
                                return;
                            }
                        }

                        _Entity.InsertData(_Entity);

                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["UserID"].DefaultValue = txtuserid.Text; //Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid


                        Gears.RetriveData2("exec sp_ConsoUsers '" + txtuserid.Text + "','ADD'", Session["ConnString"].ToString());

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
                        odsDetail.SelectParameters["UserID"].DefaultValue = txtuserid.Text; //Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        Gears.RetriveData2("exec sp_ConsoUsers '" + txtuserid.Text + "','UPDATE'", Session["ConnString"].ToString());

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

                case "RR":
                    SqlDataSource ds = sdsBizPartner;

                    ds.SelectCommand = string.Format("SELECT DateOfBirth,EntityID,EmployeeCode,LastName + ',' + FirstName + ' ' + MiddleName as FullName  from Masterfile.BPEmployeeInfo where ISNULL(IsInactive,0)=0 and  EmployeeCode = '" + aglBizPartnerCode.Value + "'");

                    DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (biz.Count > 0)
                    {

                        dtpbirthdate.Date = String.IsNullOrEmpty(biz[0]["DateOfBirth"].ToString()) ? DateTime.Now : Convert.ToDateTime(biz[0]["DateOfBirth"].ToString());

                        txtorgchartentity.Value = biz[0]["EntityID"].ToString();
                        //ASPxTextBox1.Text = biz[0]["SupplierCode"].To String();

                    }
                    ds.SelectCommand = string.Format("select EmployeeCode,LastName + ',' + FirstName + ' ' + MiddleName as FullName  from Masterfile.BPEmployeeInfo where ISNULL(IsInactive,0)=0");

                    break;
            }
        }
        //protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        //{ //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
        //    string ItemCode = "";
        //    string ColorCode = "";
        //    string ClassCode = "";
        //    string SizeCode = "";

        //    if (e.Errors.Count > 0)
        //    {
        //        error = true; //bool to cancel adding/updating if true
        //    }
        //}
        ////dictionary method to hold error 
        //void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        //{
        //    if (errors.ContainsKey(column)) return;
        //    errors[column] = errorText;
        //}


  

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            sdsSecurity.ConnectionString = Session["ConnString"].ToString();
            sdsUserRole.ConnectionString = Session["ConnString"].ToString();
            sdsBizPartner.ConnectionString = Session["ConnString"].ToString();
            sdsCompany.ConnectionString = Session["ConnString"].ToString();


        }

        #endregion

        
    }
}