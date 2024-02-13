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
using GearsWarehouseManagement;
using GearsSales;
using System.Web.Routing;
using GearsAccounting;

namespace GWL
{
    public partial class frmCashReplenishment : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode

        private static string Connection;

        Entity.CashReplenishment _Entity = new CashReplenishment();//Calls entity odsHeader
        Entity.CashReplenishment.CashReplenishmentDetail _EntityDetail = new CashReplenishment.CashReplenishmentDetail();//Call entity sdsDetail

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

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            gv1.KeyFieldName = "LineNumber";

 

            txtDocNumber.ReadOnly = true;
            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

            if (!IsPostBack)
            {
                Session["CashReplenishmentDetail"] = null;
                Session["CashReplenishmentDatatable"] = null;

                Connection = Session["ConnString"].ToString();

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN

                dtpDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                txtTotalAmountReplenished.Text = _Entity.TotalAmountReplenished.ToString();
                glFundSource.Value = _Entity.FundSource.ToString();

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
                        edit = true;
                        break;
                    case "V":
                        view = true;
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "D":
                        view = true;
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.CashReplenishmentDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                gvJournal.DataSourceID = "odsJournalEntry";

                           sdsCashRepDetail.SelectCommand = " SELECT '2' AS Version, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY TransDocNumber) AS VARCHAR(5)),5) AS LineNumber, * FROM( "
                                            + " SELECT 'True' As Checkbox, DocNumber AS TransDocNumber, DocDate AS TransDate, 'ACTCRB' AS TransType, ExpendAmount AS ExpenseAmount,FundSource FROM Accounting.CashReimbursement WHERE ISNULL(SubmittedBy,'') != '' AND ISNULL(ReplenishNumber,'')='' AND DocDate <= '" + dtpDocDate.Text + "' "
                                            + " UNION ALL SELECT 'True' As Checkbox, A.DocNumber AS TransDocNumber, A.DocDate AS TransDate, 'ACTLCA' AS TransType, A.ExpendAmount AS ExpenseAmount,FundSource FROM Accounting.LiquidationOfCashAdvance A INNER JOIN  Accounting.CashAdvance B ON A.CashAdvanceNUmber = B.Docnumber WHERE ISNULL(A.SubmittedBy,'') != '' AND ISNULL(A.ReplenishNumber,'')='' AND A.DocDate <= '" + dtpDocDate.Text + "'  AND FundSource ='" + glFundSource.Text + "'  ) AS TEMP "
                                            + " WHERE ISNULL(ExpenseAmount,0) != 0";
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
            gparam._TransType = "ACTCRP";
            string strresult = GearsAccounting.GAccounting.CashReplenishment_Validate(gparam);
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
            gparam._TransType = "ACTCRP";
            gparam._Table = "Accounting.CashReplenishment";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.CashReplenishment_Post(gparam);
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
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.DropDownButton.Enabled = !view;
                look.ReadOnly = view;
            }
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
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.TotalAmountReplenished = Convert.ToDecimal(txtTotalAmountReplenished.Text);
            _Entity.FundSource = glFundSource.Text;
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
                case "Update":
                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.CashReplenishment", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }

                        if (Session["CashReplenishmentDatatable"] == "1")
                        {
                            _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                            gv1.DataSourceID = sdsCashRepDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }

                        //  _Entity.SubsiEntry(txtDocNumber.Text);

                        Post();
                        Validate();

                        cp.JSProperties["cp_message"] = (e.Parameter == "Add" ? "Successfully Added!" : "Successfully Updated!"); 
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["CashReplenishmentDatatable"] = null;
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
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;

                case "Generate":
                    GetSelectedVal();
                    gv1.KeyFieldName = "LineNumber";
                    cp.JSProperties["cp_generated"] = true;
                    break;
            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
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

            if (Session["CashReplenishmentDatatable"] == "1" && check == true)
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

                    row["Checkbox"] = values.NewValues["Checkbox"];
                    row["TransDocNumber"] = values.NewValues["TransDocNumber"];
                    row["TransDate"] = values.NewValues["TransDate"];
                    row["TransType"] = values.NewValues["TransType"];
                    row["ExpenseAmount"] = values.NewValues["ExpenseAmount"];

                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                }

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.TransDocNumber = dtRow["TransDocNumber"].ToString();
                    _EntityDetail.TransDate = Convert.ToDateTime(dtRow["TransDate"].ToString());
                    _EntityDetail.Checkbox = Convert.ToBoolean(dtRow["CheckBox"].ToString());
                    _EntityDetail.TransType = dtRow["TransType"].ToString();
                    _EntityDetail.ExpenseAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExpenseAmount"]) ? 0 : dtRow["ExpenseAmount"]);

                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.AddCashReplenishmentDetail(_EntityDetail);
                }
            }
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CashReplenishmentDetail"] = null;
            }

            if (Session["CashReplenishmentDetail"] != null)
            {
                gv1.DataSourceID = sdsCashRepDetail.ID;
                sdsCashRepDetail.FilterExpression = Session["CashReplenishmentDetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        //private DataTable GetSelectedVal()
        //{
        //    DataTable dt = new DataTable();
        //    //string[] selectedValues = glBankAccountCode.Text.Split(';');
        //    //CriteriaOperator selectionCriteria = new InOperator(glBankAccountCode.KeyFieldName, selectedValues);

        //    //sdsCashRepDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    //Session["CashReplenishmentDetail"] = sdsCashRepDetail.FilterExpression;
        //    sdsCashRepDetail.SelectCommand = " SELECT '2' AS Version, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY TransDocNumber) AS VARCHAR(5)),5) AS LineNumber, * FROM( "
        //                                    + " SELECT 'True' As Checkbox, DocNumber AS TransDocNumber, DocDate AS TransDate, 'ACTCRB' AS TransType, ExpendAmount AS ExpenseAmount FROM Accounting.CashReimbursement WHERE ISNULL(SubmittedBy,'') != '' AND DocDate <= '" + dtpDocDate.Text + "' "
        //                                    + " UNION ALL SELECT 'True' As Checkbox, DocNumber AS TransDocNumber, DocDate AS TransDate, 'ACTLCA' AS TransType, ExpendAmount AS ExpenseAmount FROM Accounting.LiquidationOfCashAdvance WHERE ISNULL(SubmittedBy,'') != '' AND DocDate <= '" + dtpDocDate.Text + "' ) AS TEMP "
        //                                    + " WHERE ISNULL(ExpenseAmount,0) != 0";
        //    gv1.DataSourceID = sdsCashRepDetail.ID;
        //    gv1.DataBind();
        //    Session["CashReplenishmentDatatable"] = "1";

        //    foreach (GridViewColumn col in gv1.VisibleColumns)
        //    {
        //        GridViewDataColumn dataColumn = col as GridViewDataColumn;
        //        if (dataColumn == null) continue;
        //        dt.Columns.Add(dataColumn.FieldName);
        //    }
        //    for (int i = 0; i < gv1.VisibleRowCount; i++)
        //    {
        //        DataRow row = dt.Rows.Add();
        //        foreach (DataColumn col in dt.Columns)
        //            row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
        //    }

        //    dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
        //    dt.Columns["LineNumber"]};

        //    return dt;
        //}

        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            //string[] selectedValues = glBankAccountCode.Text.Split(';');
            //CriteriaOperator selectionCriteria = new InOperator(glBankAccountCode.KeyFieldName, selectedValues);

            //sdsCashRepDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["CashReplenishmentDetail"] = sdsCashRepDetail.FilterExpression;
           
            gv1.DataSourceID = sdsCashRepDetail.ID;
            gv1.DataBind();
            Session["CashReplenishmentDatatable"] = "1";

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

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}