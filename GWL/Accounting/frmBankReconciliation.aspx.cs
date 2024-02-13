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
using System.Web.Routing;
using GearsAccounting;

namespace GWL
{
    public partial class frmBankReconciliation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode

        private static string Connection;

        Entity.BankReconcilliation _Entity = new BankReconcilliation();//Calls entity odsHeader
        Entity.BankReconcilliation.BankReconcilliationDetail _EntityDetail = new BankReconcilliation.BankReconcilliationDetail();//Call entity sdsDetail

        //void Application_Start(object sender, EventArgs e)
        //{
        //    RegisterRoutes(RouteTable.Routes);
        //}
        //void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapPageRoute("",
        //"/Sales/frmSalesReturn.aspx", "~/frmSalesReturn.aspx");
        //}

        #region page load/entry
        protected void Page_PreInit(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            foreach (Control form in form2.Controls)
            {
                if (form is SqlDataSource)
                {
                    ((SqlDataSource)form).ConnectionString = Session["ConnString"].ToString();
                }
            }
        }
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

            dtpCutOffDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            gv1.KeyFieldName = "LineNumber";

            sdsGenerateBankRecon.SelectCommand = "EXEC [sp_Generate_BankReconciliation] '" + dtpCutOffDate.Value + "','" + glBankAccountCode.Value + "','" + cbxSelectAll.Value + "'";

            txtDocNumber.ReadOnly = true;
            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();

                dtpCutOffDate.Text = DateTime.Now.ToShortDateString();
                dtpLastBankReconDate.Text = DateTime.Now.ToShortDateString();

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                glBankAccountCode.Value = Convert.ToString(_Entity.BankAccountCode);
                dtpCutOffDate.Value = String.IsNullOrEmpty(_Entity.CutOffDate) ? DateTime.Now : Convert.ToDateTime(_Entity.CutOffDate);
                dtpLastBankReconDate.Value = String.IsNullOrEmpty(_Entity.LastBankReconDate) ? DateTime.Now : Convert.ToDateTime(_Entity.LastBankReconDate);
                txtLastBankReconBal.Text = _Entity.LastBankReconBal.ToString();
                txtEndingBalance.Text = _Entity.EndingBalance.ToString();
                txtTotalDebitAmount.Text = _Entity.TotalDebitAmount.ToString();
                txtTotalCreditAmount.Text = _Entity.TotalCreditAmount.ToString();
                cbxSelectAll.Value = Convert.ToBoolean(Convert.IsDBNull(_Entity.SelectAll)) ? false : Convert.ToBoolean(_Entity.SelectAll);

                txtHAddedBy.Value = _Entity.AddedBy;
                txtHAddedDate.Value = _Entity.AddedDate;
                txtHLastEditedBy.Value = _Entity.LastEditedBy;
                txtHLastEditedDate.Value = _Entity.LastEditedDate;
                txtHSubmittedBy.Value = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = _Entity.SubmittedDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        //Generatebtn.Enabled = false;
                        edit = true;
                        break;
                    case "V":
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        view = true;
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        view = true;
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.BankReconciliationDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
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
            gparam._TransType = "ACTBRC";
            string strresult = GearsAccounting.GAccounting.BankReconciliation_Validate(gparam);
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
            gparam._TransType = "ACTBRC";
            gparam._Table = "Accounting.BankReconciliation";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.BankReconciliation_Post(gparam);
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

        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientVisible = !view;
        }

        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            //ASPxGridLookup look = sender as ASPxGridLookup;
            //look.DropDownButton.Enabled = !view;
            //look.ReadOnly = view;
            var look = sender as ASPxGridLookup;
            if (look != null) { look.ReadOnly = view; }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D" || Request.QueryString["entry"].ToString() == "E")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = true;
            }
        }

        protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = true;
            }
            else
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;

        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {

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
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";

            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (Request.QueryString["entry"] == "V")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }
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
            if (e.ButtonID == "Countsheet" || e.ButtonID == "Countsheet")
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }

            if (e.ButtonID == "Delete" || e.ButtonID == "Delete")
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
        #endregion

        #region Lookup Settings

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.BankAccountCode = glBankAccountCode.Text;
            _Entity.CutOffDate = String.IsNullOrEmpty(dtpCutOffDate.Text) ? null : dtpCutOffDate.Text;
            _Entity.LastBankReconBal = String.IsNullOrEmpty(txtLastBankReconBal.Text) ? 0 : Convert.ToDecimal(txtLastBankReconBal.Value.ToString()); //Convert.ToDecimal(txtLastBankReconBal.Text);
            _Entity.LastBankReconDate = String.IsNullOrEmpty(dtpLastBankReconDate.Text) ? null : dtpLastBankReconDate.Text;
            _Entity.EndingBalance = String.IsNullOrEmpty(txtEndingBalance.Text) ? 0 : Convert.ToDecimal(txtEndingBalance.Value.ToString());
            _Entity.TotalDebitAmount = String.IsNullOrEmpty(txtTotalDebitAmount.Text) ? 0 : Convert.ToDecimal(txtTotalDebitAmount.Value.ToString());
            _Entity.TotalCreditAmount = String.IsNullOrEmpty(txtTotalCreditAmount.Text) ? 0 : Convert.ToDecimal(txtTotalCreditAmount.Value.ToString());
            _Entity.SelectAll = Convert.IsDBNull(cbxSelectAll.Value) ? false : Convert.ToBoolean(cbxSelectAll.Value);

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = txtHLastEditedDate.Text;
            _Entity.AddedBy = txtHAddedBy.Text;
            _Entity.AddedDate = txtHAddedDate.Text;
            _Entity.SubmittedBy = txtHSubmittedBy.Text;
            _Entity.SubmittedDate = txtHSubmittedDate.Text;
            _Entity.PostedBy = txtPostedBy.Text;
            _Entity.PostedDate = txtPostedDate.Text;
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();
                    string strError = Functions.Submitted(_Entity.DocNumber,"Accounting.BankReconciliation",1,Connection);//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity); //Method of inserting for header
                        //_Entity.SubsiEntry(txtDocNumber.Text);
                        if (Session["Datatable"] == "1")
                        {
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Post();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Update":

                    gv1.UpdateEdit();
                    string strError1 = Functions.Submitted(_Entity.DocNumber,"Accounting.BankReconciliation",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError1))
                        {
                            cp.JSProperties["cp_message"] = strError1;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                            gv1.DataSourceID = sdsGenerateBankRecon.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Post();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        //cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;


                case "Delete":
                    //GetSelectedVal(); 
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

                case "Generate":
                    GetSelectedVal();
                    gv1.KeyFieldName = "LineNumber";
                    //SetText();
                    cp.JSProperties["cp_generated"] = true;
                    //gridupcheck();
                    break;

                case "callbackBankAccount":
                    SqlDataSource ds = sdsBankAccount;

                    ds.SelectCommand = string.Format("SELECT BankAccountCode, LastBalance, LastReconDate FROM Masterfile.BankAccount where BankAccountCode = '" + glBankAccountCode.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtLastBankReconBal.Text = Convert.ToDecimal(inb[0][1]).ToString();
                        dtpLastBankReconDate.Text = Convert.ToDateTime(inb[0][2]).ToShortDateString();
                    }

                    ds.SelectCommand = string.Format("SELECT BankAccountCode FROM Masterfile.BankAccount WHERE ISNULL([IsInactive],0) = 0");

                    gv1.DataSourceID = null;
                    gv1.DataSource = null;
                    gv1.DataBind();

                    cp.JSProperties["cp_generated"] = true;
                    break;

                case "callbackCutOff":
                    gv1.DataSourceID = null;
                    gv1.DataBind();

                    cp.JSProperties["cp_generated"] = true;
                    break;

                case "recheck":
                    string temp = Session["ConnString"].ToString();


                    break;
            }
        }

        protected void gridupcheck()
        {
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = gv1.GetDataRow(i);
                string name = row["TransDocNumber"].ToString();
                
            }            
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";
            if (error == false && check == false)
            {
                foreach (GridViewColumn column in gv1.Columns)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber"
                        && dataColumn.FieldName != "PRNumber" && dataColumn.FieldName != "ReturnedQty" && dataColumn.FieldName != "UnitPrice"
                        && dataColumn.FieldName != "StatusCode" && dataColumn.FieldName != "DiscountRate" && dataColumn.FieldName != "AverageCost"
                        && dataColumn.FieldName != "VATCode" && dataColumn.FieldName != "IsVat" && dataColumn.FieldName != "IsAllowPartial"
                        && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3"
                        && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6"
                        && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9")
                    {
                        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                    }
                }

                //Checking for non existing Codes..
                ItemCode = e.NewValues["ItemCode"].ToString();
                ColorCode = e.NewValues["ColorCode"].ToString();
                ClassCode = e.NewValues["ClassCode"].ToString();
                SizeCode = e.NewValues["SizeCode"].ToString();
                DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (item.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
                }
                DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (color.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
                }
                DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (clss.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
                }
                DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (size.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
                }


                if (e.Errors.Count > 0)
                {
                    error = true; //bool to cancel adding/updating if true
                }
            }
        }
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
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    //object[] keys = { values.Keys[0] };
                    //DataRow row = source.Rows.Find(keys);
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);

                    row["TransDocNumber"] = values.NewValues["TransDocNumber"];
                    row["TransLineNumber"] = values.NewValues["TransLineNumber"];
                    row["TransType"] = values.NewValues["TransType"];
                    row["IsSelected"] = values.NewValues["IsSelected"];
                    row["DocDate"] = values.NewValues["DocDate"];
                    row["CheckNumber"] = values.NewValues["CheckNumber"];
                    row["PayeeName"] = values.NewValues["PayeeName"];
                    row["CheckDate"] = values.NewValues["CheckDate"];
                    row["DebitAmount"] = values.NewValues["DebitAmount"];
                    row["CreditAmount"] = values.NewValues["CreditAmount"];
                    row["BankRunningBalance"] = values.NewValues["BankRunningBalance"];

                    //row["Field1"] = values.NewValues["Field1"];
                    //row["Field2"] = values.NewValues["Field2"];
                    //row["Field3"] = values.NewValues["Field3"];
                    //row["Field4"] = values.NewValues["Field4"];
                    //row["Field5"] = values.NewValues["Field5"];
                    //row["Field6"] = values.NewValues["Field6"];
                    //row["Field7"] = values.NewValues["Field7"];
                    //row["Field8"] = values.NewValues["Field8"];
                    //row["Field9"] = values.NewValues["Field9"];
                }

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.TransType = dtRow["TransType"].ToString();
                    _EntityDetail.TransDocNumber = dtRow["TransDocNumber"].ToString();
                    _EntityDetail.TransLineNumber = Convert.ToInt32(Convert.IsDBNull(dtRow["TransLineNumber"]) ? 0 : dtRow["TransLineNumber"]);//dtRow["TransLineNumber"].ToString();
                    //_EntityDetail.DocNumber = dtRow["DocNumber"].ToString();
                    _EntityDetail.DocDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["DocDate"].ToString()) ? null : dtRow["DocDate"]);// dtRow["DocDate"].ToString();
                    _EntityDetail.IsSelected = Convert.ToBoolean(dtRow["IsSelected"].ToString());
                    _EntityDetail.CheckNumber = dtRow["CheckNumber"].ToString();
                    _EntityDetail.PayeeName = dtRow["PayeeName"].ToString();
                    _EntityDetail.CheckDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["CheckDate"]) ? null : dtRow["CheckDate"]);//Convert.IsDBNull(dtRow["CheckDate"].ToString()) ? null : dtRow["CheckDate"].ToString();  //dtRow["CheckDate"].ToString();
                    _EntityDetail.DebitAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["DebitAmount"]) ? 0 : dtRow["DebitAmount"]);
                    _EntityDetail.CreditAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CreditAmount"]) ? 0 : dtRow["CreditAmount"]);
                    _EntityDetail.BankRunningBalance = Convert.ToDecimal(Convert.IsDBNull(dtRow["BankRunningBalance"]) ? 0 : dtRow["BankRunningBalance"]);

                    //_EntityDetail.Field3 = dtRow["Field1"].ToString();
                    //_EntityDetail.Field3 = dtRow["Field2"].ToString();
                    //_EntityDetail.Field3 = dtRow["Field3"].ToString();
                    //_EntityDetail.Field4 = dtRow["Field4"].ToString();
                    //_EntityDetail.Field5 = dtRow["Field5"].ToString();
                    //_EntityDetail.Field6 = dtRow["Field6"].ToString();
                    //_EntityDetail.Field7 = dtRow["Field7"].ToString();
                    //_EntityDetail.Field8 = dtRow["Field8"].ToString();
                    //_EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.AddBankReconcilliationDetail(_EntityDetail);
                }
            }
        }
        #endregion

        //protected void aglcustomerinit(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
        //    if (Session["customoutbound"] != null)
        //    {
        //        gridLookup.GridView.DataSource = sdsPicklist;
        //        sdsPicklist.FilterExpression = Session["customoutbound"].ToString();
        //    }
        //}

        //public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string whcode = e.Parameters;//Set column name
        //    if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

        //    ASPxGridView grid = sender as ASPxGridView;
        //    CriteriaOperator selectionCriteria = new InOperator(glContactPerson.KeyFieldName, new string[] { whcode });
        //    sdsPicklist.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["customoutbound"] = sdsPicklist.FilterExpression;
        //    grid.DataSource = sdsPicklist;
        //    grid.DataBind();
        //}
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["bankreconciliationdetail"] = null;
            }

            if (Session["bankreconciliationdetail"] != null)
            {
                //sdsGenerateBankRecon.SelectCommand = "EXEC [sp_Generate_BankReconciliation] '" + dtpCutOffDate.Value + "','" + glBankAccountCode.Value + "','" + cbxSelectAll.Value + "'";

                gv1.DataSourceID = sdsGenerateBankRecon.ID;
                gv1.DataSource = sdsGenerateBankRecon;
                if (gv1.DataSourceID != null)
                {
                    gv1.DataSourceID = null;
                }
                gv1.DataBind();
                gv1.JSProperties["cp_change"] = true;
                //sdsGenerateBankRecon.FilterExpression = Session["bankreconciliationdetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            string[] selectedValues = glBankAccountCode.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(glBankAccountCode.KeyFieldName, selectedValues);

            //sdsGenerateBankRecon.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["bankreconciliationdetail"] = sdsGenerateBankRecon.FilterExpression;
            gv1.DataSourceID = sdsGenerateBankRecon.ID;
            if (gv1.DataSource != null)
            {
                gv1.DataSource = null;
            }
            gv1.DataBind();
            Session["Datatable"] = "1";

            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumber"]};

            return dt;
        }


        protected void aglCustomer_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void aglCustomer_TextChanged(object sender, EventArgs e)
        {
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpCutOffDate.Date = DateTime.Now;
            }
        }

        //protected void Connection_Init(object sender, EventArgs e)
        //{
        //    Gears.UseConnectionString(Session["ConnString"].ToString());
        //    //sdsDetail.ConnectionString = Session["ConnString"].ToString();
        //    //sdsGenerateBankRecon.ConnectionString = Session["ConnString"].ToString();
        //    //sdsBankAccount.ConnectionString = Session["ConnString"].ToString();
        //}


    }
}