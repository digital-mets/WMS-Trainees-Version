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
using GearsAccounting;

namespace GWL
{
    public partial class frmJVTemplate : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.JVTemplate _Entity = new JVTemplate();//Calls entity Customer

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
            
            deDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                Connection = Session["ConnString"].ToString();
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        //glcheck.ClientVisible = false;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //glPrefix.ClientEnabled = false;
                        //deDocDate.ClientEnabled = false;
                        //cboType.ClientEnabled = false;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session

                txtDoc.Value = Request.QueryString["docnumber"].ToString();
                //if (Request.QueryString["entry"].ToString() == "N")
                //{


                //}
                //else
                //{
                //gvBiz.ReadOnly = true;



                //_Entity.getdata(txtDoc.Text);

                _Entity.getdata(txtDoc.Text, Session["ConnString"].ToString());//ADD CONN

                //glPrefix.Value = _Entity.Prefix;
                deDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                txtDesc.Value = _Entity.Description;
                spTotalDebit.Value = _Entity.TotalDebit;
                spTotalCredit.Value = _Entity.TotalCredit;
                chkIsInactive.Value = _Entity.IsInactive;


                txtAddedDate.Text = _Entity.AddedDate;
                txtLastEditedDate.Text = _Entity.LastEditedDate;
                txtAddedBy.Text = _Entity.AddedBy;
                txtLastEditedBy.Text = _Entity.LastEditedBy;
                txtActivatedBy.Text = _Entity.ActivatedBy;
                txtDeActivatedBy.Text = _Entity.DeactivatedBy;
                txtActivatedDate.Text = _Entity.ActivatedDate;
                txtDeActivatedDate.Text = _Entity.DeactivatedDate;
                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;


                //}

                DataTable checkCount = Gears.RetriveData2("Select DocNumber from accounting.jvtemplatedetail where docnumber = '" + txtDoc.Text + "'",
                    Connection);//ADD CONN
                gv1.DataSourceID = (checkCount.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Connection = Session["ConnString"].ToString();
            //gparam._Factor = 1;
            // gparam._Action = "Validate";
            //here
            string strresult = GAccounting.JVTemplate_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void memoremarks_Load(object sender, EventArgs e)
        {
            ((ASPxMemo)sender).ReadOnly = view;

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
                if (e.ButtonID == "Details")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "Delete")
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
                gridLookup.GridView.DataSourceID = "SubsiCode";
                SubsiCode.FilterExpression = Session["FilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string code = e.Parameters.Split('|')[1];//Set Item Code
            var gridlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("SubsiGetCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[4], "glSubsiCode");
                var selectedValues = code;
                CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { code });
                SubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = SubsiCode.FilterExpression;
                grid.DataSourceID = "SubsiCode";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    var col = "SubsiCode";
                    if (grid.GetRowValues(i, col) != null)
                        if (grid.GetRowValues(i, col).ToString() == code)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, col).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDoc.Text;
            //_Entity.Prefix = String.IsNullOrEmpty(glPrefix.Text) ? null : glPrefix.Value.ToString();
            _Entity.DocDate = deDocDate.Text;
            _Entity.Description = txtDesc.Text;
            _Entity.TotalDebit = String.IsNullOrEmpty(spTotalDebit.Text) ? 0 : Convert.ToDecimal(spTotalDebit.Text);
            _Entity.TotalCredit = String.IsNullOrEmpty(spTotalCredit.Text) ? 0 : Convert.ToDecimal(spTotalCredit.Text);
            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Checked);

            //_Entity.AddedBy = txtAddedBy.Text;
            //_Entity.AddedDate = txtAddedDate.Text;
            //_Entity.ActivatedBy = txtActivatedBy.Text;
            //_Entity.ActivatedDate = txtActivatedDate.Text;
            //_Entity.DeactivatedBy = txtDeActivatedBy.Text;
            //_Entity.DeactivatedDate = txtDeActivatedDate.Text;

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
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();


            switch (e.Parameter)
            {
                case "Add":
                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        Validate();
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid

                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    gv1.UpdateEdit();



                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid

                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;
                case "Delete":
                    check = true;
                    cp.JSProperties["cp_delete"] = true;
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                    cp.JSProperties["cp_success"] = true;
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
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
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
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

        protected void glBizPartnerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtPlant_TextChanged(object sender, EventArgs e)
        {

        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            AccountCode.ConnectionString = Session["ConnString"].ToString();
            SubsiCode.ConnectionString = Session["ConnString"].ToString();
            CostCenterCode.ConnectionString = Session["ConnString"].ToString();
            ProfitCenterCode.ConnectionString = Session["ConnString"].ToString();
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            BizPartnerCode.ConnectionString = Session["ConnString"].ToString();
            BizAccount.ConnectionString = Session["ConnString"].ToString();
            Currency.ConnectionString = Session["ConnString"].ToString();
            ItemCategory.ConnectionString = Session["ConnString"].ToString();
            Prefix.ConnectionString = Session["ConnString"].ToString();

        }


        protected void deDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                deDocDate.Date = DateTime.Now;
            }
        }


        public void glAccountCode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView lookup = sender as ASPxGridView;
            string[] trans = e.Parameters.Split('|');
            string column = e.Parameters.Split('|')[0];//Set column name
            if (e.Parameters.Contains("GLP_AC"))
            {
                lookup.JSProperties["cp_ac"] = "true";
            }
        }

        protected void glAccountCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glAccountCode_CustomCallback);
        }
    }
}