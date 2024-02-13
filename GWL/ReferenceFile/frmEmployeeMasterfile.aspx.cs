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

namespace GWL
{
    public partial class frmEmployeeMasterfile : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.EmployeeMaterfile _Entity = new EmployeeMaterfile();//Calls entity ICN
      //  Entity.ItemMaster.ItemDetail _EntityDetail = new ItemMaster.ItemDetail();//Call entity POdetails

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

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

          //  gv1.KeyFieldName = "DocNumber;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
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
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }

               
                

                if (Request.QueryString["entry"].ToString() == "N")
                {

                  //  gv1.DataSourceID = "sdsDetail";
                                }
                else
                {
                 //  gv1.DataSourceID = "odsDetail";

                    aglEmployeeCode.ReadOnly = true;
                    


                    aglEmployeeCode.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(aglEmployeeCode.Text,  Session["ConnString"].ToString());
                    txtEmployeeID.Text = _Entity.EmployeeID;
                    txtfirstname.Text = _Entity.FirstName;
                    txtmiddlename.Text = _Entity.MiddleName;
                    txtlastname.Text = _Entity.LastName;
                    txtaddress.Text = _Entity.Address;
                    dtpbirthdate.Text = String.IsNullOrEmpty(_Entity.DateOfBirth) ? "" : Convert.ToDateTime(_Entity.DateOfBirth).ToShortDateString();
                    txtsssno.Text = _Entity.SSSNo;
                    txttin.Text = _Entity.TIN;
                    txthdmf.Text = _Entity.HDMF;
                    txtphilhealth.Text = _Entity.PhilHealth;
                    txtprofitcentercode.Text = _Entity.ProfitCenterCode;
                    txtcostcentercode.Text = _Entity.CostCenterCode;
                    txtentity.Text = _Entity.EntityID;



                    chkisinactive.Value = _Entity.IsInactive;

                 //   txtHField1.Text = _Entity.Field1;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;
                

                   
                    txtActivatedDate.Text = String.IsNullOrEmpty(_Entity.ActivatedDate) ? "" : Convert.ToDateTime(_Entity.ActivatedDate).ToShortDateString();
                    txtAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? "" : Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                    txtLastEditedDate.Text = String.IsNullOrEmpty(_Entity.LastEditedDate) ? "" : Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                    txtDeactivatedDate.Text = String.IsNullOrEmpty(_Entity.DeactivatedDate) ? "" : Convert.ToDateTime(_Entity.DeactivatedDate).ToShortDateString();
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;

                }
                dtpbirthdate.MaxDate = DateTime.Now;
                
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        //    gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            //gparam._Factor = 1;
            // gparam._Action = "Validate";
            //here
            string strresult = GWarehouseManagement.OCN_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        //protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        //{
        //    var look = sender as ASPxGridLookup;
        //    if (look != null)
        //    {
        //        look.ReadOnly = view;
        //    }
        //}

        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.DropDownButton.Enabled = !view;
            lookup.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                if (look != null)
                {
                    look.Enabled = false;
                }
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                //grid.SettingsBehavior.AllowGroup = false;
                //grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }


        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
            date.DropDownButton.Enabled = !view;
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
            if (Request.QueryString["entry"].ToString() == "V")
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

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
              //  Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
          //  ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            var selectedValues = itemcode;
           // CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
           // Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
         // Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
            grid.DataSourceID = "Masterfileitemdetail";
            grid.DataBind();

            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, column) != null)
                    if (grid.GetRowValues(i, column).ToString() == val)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, column).ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN

            _Entity.EmployeeCode = aglEmployeeCode.Text;
            _Entity.EmployeeID = txtEmployeeID.Text;

            _Entity.FirstName= txtfirstname.Text ;
            _Entity.MiddleName=txtmiddlename.Text ;
            _Entity.LastName=txtlastname.Text ;
            _Entity.Address= txtaddress.Text ;
            _Entity.DateOfBirth=dtpbirthdate.Text;
            _Entity.SSSNo=txtsssno.Text ;
            _Entity.TIN= txttin.Text ;
            _Entity.HDMF= txthdmf.Text;
            _Entity.PhilHealth= txtphilhealth.Text;
            _Entity.ProfitCenterCode= txtprofitcentercode.Text ;
            _Entity.CostCenterCode= txtcostcentercode.Text ;
            _Entity.EntityID=txtentity.Text ;
         

         
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2=txtHField2.Text ;
             _Entity.Field3=txtHField3.Text ;
             _Entity.Field4=txtHField4.Text ;
             _Entity.Field5=txtHField5.Text ;
              _Entity.Field6=txtHField6.Text;
            _Entity.Field7=txtHField7.Text ;
          _Entity.Field8= txtHField8.Text ;
            _Entity.Field9=txtHField9.Text ;
            _Entity.IsInactive = Convert.ToBoolean(chkisinactive.Value); 
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeactivatedBy = txtDeactivatedBy.Text;
            _Entity.DeactivatedDate = txtDeactivatedDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();



            switch (e.Parameter)
            {
                case "Add":
                    _Entity.AddedBy = Session["userid"].ToString();
                    if (error == false)
                    {
                        check = true;

                        _Entity.InsertData(_Entity);

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
                    _Entity.LastEditedBy = Session["userid"].ToString();
                    
           
                        if (error == false)
                        {
                            check = true;

                            _Entity.UpdateData(_Entity);//Method of Updating header

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
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;
                case "SUP":
                    SqlDataSource ds = BizAccount;
                    ds.SelectCommand = string.Format("select BizPartnerCode,Address,TIN from Masterfile.BizPartner where BizPartnerCode = '" + aglEmployeeCode.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        //txtfirstname.Value = tran[0][1].ToString();
                        txtaddress.Value = tran[0][1].ToString();
                        txttin.Value = tran[0][2].ToString();
                    }
                    break;
            }
        }
        //protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        //{ //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
        //    string ItemCode = "";
        //    string ColorCode = "";
        //    string ClassCode = "";
        //    string SizeCode = "";
        //    //foreach (GridViewColumn column in gv1.Columns)
        //    //{
        //    //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
        //    //    //if (dataColumn == null) continue;
        //    //    //if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "OrderQty")
        //    //    //{
        //    //    //    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
        //    //    //}
        //    //    //Checking for non existing Codes..
        //    //    ItemCode = e.NewValues["ItemCode"].ToString();
        //    //    ColorCode = e.NewValues["ColorCode"].ToString();
        //    //    ClassCode = e.NewValues["ClassCode"].ToString();
        //    //    SizeCode = e.NewValues["SizeCode"].ToString();
        //    //    DataTable item = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'");
        //    //    if (item.Rows.Count < 1)
        //    //    {
        //    //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
        //    //    }
        //    //    DataTable color = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
        //    //    if (color.Rows.Count < 1)
        //    //    {
        //    //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
        //    //    }
        //    //    DataTable clss = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
        //    //    if (clss.Rows.Count < 1)
        //    //    {
        //    //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
        //    //    }
        //    //    DataTable size = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
        //    //    if (size.Rows.Count < 1)
        //    //    {
        //    //        AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
        //    //    }
        //    //}

        //    if (e.Errors.Count > 0)
        //    {
        //        error = true; //bool to cancel adding/updating if true
        //    }
        //}
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glCustomerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
          protected void Connection_Init(object sender, EventArgs e)
        {
            costcenter.ConnectionString = Session["ConnString"].ToString();
            sdsEmployee.ConnectionString = Session["ConnString"].ToString();
            profitcenter.ConnectionString = Session["ConnString"].ToString();
            sdsOrgchart.ConnectionString = Session["ConnString"].ToString();
            BizAccount.ConnectionString = Session["ConnString"].ToString();
        }

     
    }
}