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

namespace GWL
{
    public partial class frmCashReimbursement : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.CashReimbursement _Entity = new CashReimbursement();//Calls entity Quotation
        Entity.CashReimbursement.CashReimbursementDetail _EntityDetail = new CashReimbursement.CashReimbursementDetail();//Call entity Quotationdetails

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

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());


            gv1.KeyFieldName = "DocNumber;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry and delete
            }

            if (!IsPostBack)
            {
                Session["ACTLCAFilterExpression"] = null;

                Connection = Session["ConnString"].ToString();

                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN

                dtpDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                txtReplenishNumber.Value = _Entity.ReplenishNumber.ToString();
                glFundSource.Value = _Entity.FundSource.ToString();
                if ((Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E") && (_Entity.Requestor.ToString() == "" || _Entity.Requestor == null))
                {
                    glRequestor.Value = Session["userid"].ToString();
                }
                else
                {
                    glRequestor.Value = _Entity.Requestor.ToString();
                }
                glCostCenter.Value = _Entity.CostCenterCode.ToString();
                glReceiver.Value = _Entity.Receiver.ToString();
                txtExpendAmount.Value = Convert.ToDecimal(_Entity.ExpendAmount);

                txtHAddedBy.Value = _Entity.AddedBy;
                txtHAddedDate.Value = _Entity.AddedDate;
                txtHLastEditedBy.Value = _Entity.LastEditedBy;
                txtHLastEditedDate.Value = _Entity.LastEditedDate;
                txtHSubmittedBy.Value = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = _Entity.SubmittedDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;

                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

                updateBtn.Text = "Add";

                if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Update";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        //updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.CashReimbursementDetail WHERE DocNumber ='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                gvJournal.DataSourceID = "odsJournalEntry";
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCRB";

            string strresult = GearsAccounting.GAccounting.CashReimbursement_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCRB";
            gparam._Table = "Accounting.CashReimbursement";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.CashReimbursement_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
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
                look.DropDownButton.Enabled = !view;
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
            ASPxGridView grid = sender as ASPxGridView;
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsBehavior.AllowSort = false;
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
            spinedit.ReadOnly = view;
        }
        protected void Memo_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (!IsPostBack)
            {
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
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "D" || Request.QueryString["entry"] == "V")
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
            if (Session["ACTLCAFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsExpense";
                sdsExpense.FilterExpression = Session["ACTLCAFilterExpression"].ToString();
            }
            else
            {
                gridLookup.GridView.DataSourceID = "sdsExpense";
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string Exp = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var ExpenseLookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("ExpenseCode"))
            {
                DataTable expdesc = Gears.RetriveData2("SELECT Description AS ExpenseDescription FROM Masterfile.Service WHERE ServiceCode = '" + Exp + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in expdesc.Rows)
                {
                    codes = dt["ExpenseDescription"].ToString() + ";";
                }

                DataTable suppliercode = Gears.RetriveData2("SELECT ISNULL(ProfitCenterCode,'N/A') AS ProfitCenterCode, '" + glCostCenter.Text + "' AS CostCenterCode FROM Masterfile.BPEmployeeInfo WHERE EmployeeCode = '" + glReceiver.Text + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in suppliercode.Rows)
                {
                    codes += dt["ProfitCenterCode"].ToString() + ";";
                    codes += dt["CostCenterCode"].ToString() + ";";
                }
                ExpenseLookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = Exp;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { Exp });
                sdsExpense.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["ACTLCAFilterExpression"] = sdsExpense.FilterExpression;
                grid.DataSourceID = "sdsExpense";
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

        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.ReplenishNumber = txtReplenishNumber.Text;
            _Entity.FundSource = glFundSource.Text;
            _Entity.Requestor = glRequestor.Text;
            _Entity.CostCenterCode = glCostCenter.Text;
            _Entity.Receiver = glReceiver.Text;
            _Entity.ExpendAmount = String.IsNullOrEmpty(txtExpendAmount.Text) ? 0 : Convert.ToDecimal(txtExpendAmount.Text);
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

            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.CashReimbursement", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();

                        Post();
                        Validate();

                        cp.JSProperties["cp_message"] = (e.Parameter == "Add" ? "Successfully Added!" : "Successfully Updated!");
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
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;

                case "Receiver":
                    SqlDataSource ds = sdsCashAdvance;

                    ds.SelectCommand = string.Format("SELECT CostCenterCode FROM Masterfile.BPEmployeeInfo WHERE EmployeeCode = '" + glReceiver.Text + "' AND ISNULL(IsInactive,0)=0");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        glCostCenter.Value = inb[0][0].ToString();
                    }

                    ds.SelectCommand = "SELECT DocNumber, DocDate FROM Accounting.CashAdvance WHERE ISNULL(SubmittedBy,'') = ''";
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
            }
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

        protected void ConnectionInit_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}