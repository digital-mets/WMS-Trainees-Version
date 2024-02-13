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
    public partial class frmCheckVoucher : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.CheckVoucher _Entity = new CheckVoucher();//Calls entity PO
        Entity.CheckVoucher.CheckVoucherDetail _EntityDetail = new CheckVoucher.CheckVoucherDetail();//Call entity POdetails

        private static string Connection;

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
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
                Session["icndetail"] = null;

                Connection = Session["ConnString"].ToString();

                switch (Request.QueryString["entry"].ToString())
                {

                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    _Entity.getdata(txtDocNumber.Text,Connection); //Method for retrieving data from entity
                    DocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    gvSupplier.Value = _Entity.SupplierCode;
                    SupplierName.Text = _Entity.SupplierName;
                    TransType.Text = _Entity.TransType;
                    TotalCheckAmount.Text = _Entity.TotalCheckAmount.ToString();
                    TotalAppliedAmount.Text = _Entity.TotalAppliedAmount.ToString();

                    txtHField1.Value = _Entity.Field1.ToString();
                    txtHField2.Value = _Entity.Field2.ToString();
                    txtHField3.Value = _Entity.Field3.ToString();
                    txtHField4.Value = _Entity.Field4.ToString();
                    txtHField5.Value = _Entity.Field5.ToString();
                    txtHField6.Value = _Entity.Field6.ToString();
                    txtHField7.Value = _Entity.Field7.ToString();
                    txtHField8.Value = _Entity.Field8.ToString();
                    txtHField9.Value = _Entity.Field9.ToString();
                    txtRemarks.Text = _Entity.Remarks.ToString();
                    txtHAddedBy.Text = _Entity.AddedBy;
                    txtHAddedDate.Text = _Entity.AddedDate;
                    txtHLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHLastEditedDate.Text = _Entity.LastEditedDate;
                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;
                    txtCancelledBy.Text = _Entity.CancelledBy;
                    txtCancelledDate.Text = _Entity.CancelledDate;
                    txtPostedBy.Text = _Entity.PostedBy;
                    txtPostedDate.Text = _Entity.PostedDate;

                gv1.KeyFieldName = "DocNumber;LineNumber";

                DataTable chkdetail = Gears.RetriveData2("SELECT * FROM  accounting.CheckVoucherDetail where DocNumber = '" + txtDocNumber.Text + "'",Connection);
                if (chkdetail.Rows.Count == 0)
                {
                    gv1.DataSourceID = "sdsDetail";
                }
                else
                {
                    gv1.DataSourceID = "odsDetail";
                }

                DataTable chktag = Gears.RetriveData2("SELECT * FROM  accounting.CheckVoucherTagging where DocNumber = '" + txtDocNumber.Text + "'",Connection);
                if (chktag.Rows.Count == 0)
                {
                    gv2.DataSourceID = "sdsDetail2";
                }
                else
                {
                    gv2.DataSourceID = "odsDetail2";
                }

                DataTable chkadj = Gears.RetriveData2("SELECT * FROM  accounting.CheckVoucherAdjEntry where DocNumber = '" + txtDocNumber.Text + "'",Connection);
                if (chkadj.Rows.Count == 0)
                {
                    gv3.DataSourceID = "sdsDetail3";
                }
                else
                {
                    gv3.DataSourceID = "odsDetail3";
                }
 
                if (Request.QueryString["entry"].ToString() == "V")
                {
                    gv1.DataSourceID = "odsDetail";
                }

                gvJournal.DataSourceID = "odsJournalEntry";

                if (!string.IsNullOrEmpty(_Entity.LastEditedBy) && (Request.QueryString["entry"].ToString() == "E"||Request.QueryString["entry"].ToString() == "N"))
                {
                    updateBtn.Text = "Update";
                }
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCHV";
            gparam._Connection = Connection;
            string strresult = GearsAccounting.GAccounting.CheckVoucher_Validate(gparam);
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
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCHV";
            gparam._Table = "Accounting.CheckVoucher";
            gparam._Factor = -1;
            gparam._Connection = Connection;
            string strresult = GearsAccounting.GAccounting.CheckVoucher_Post(gparam);
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
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.ReadOnly = view;
            look.DropDownButton.Visible = !view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
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
            date.ReadOnly = view;
            date.DropDownButton.Visible = !view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void ComboBox_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
            combo.DropDownButton.Visible = !view;
            
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
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel)
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
                gridLookup.GridView.DataSourceID = "Subsi";
                Subsi.FilterExpression = Session["FilterExpression"].ToString();
            }
        }

        protected void lookup_Init2(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression2"] != null)
            {
                gridLookup.GridView.DataSourceID = "Profitsql";
                Costsql.FilterExpression = Session["FilterExpression2"].ToString();
            }
        }

        protected void lookup_Init3(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
        }
        protected void lookup_Init4(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression3"] != null)
            {
                gridLookup.GridView.DataSourceID = "TransDocs";
                TransDocs.FilterExpression = Session["FilterExpression3"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] trans = e.Parameters.Split('|');
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_F")) return;//Traps the callback  || column.Contains("GLP_AC")
            string code = e.Parameters.Split('|')[1];//Set Item Code
            if (code.Contains(";"))
            {
                code = code.Split(';')[1];
            }
            var gridlookup = sender as ASPxGridView;
            string codes = "";
            string val2 = "";
            try
            {
                val2 = e.Parameters.Split('|')[2];
            }
            catch (Exception)
            {
                val2 = "";
            }

            if (column.Contains("GLP_AC")&&trans.Length>3)
            {
                //string a = e.Parameters.Split('|')[1];
                
                if (trans[1].Contains(";"))
                {
                    trans[1] = trans[1].Split(';')[1];
                }
                if (trans[7].Contains(";"))
                {
                    trans[7] = trans[7].Split(';')[0];
                }
                if (trans[8].Contains(";"))
                {
                    trans[8] = trans[8].Split(';')[0];
                }
                DataTable getdt = Gears.RetriveData2("SELECT DISTINCT DocNumber, TransType, DocDate,DueDate, AccountCode, SubsiCode, ProfitCenter, CostCenter, BizPartnerCode, ISNULL(Amount,0)-ISNULL(Applied,0) as Amount, ISNULL(Amount,0)-ISNULL(Applied,0) as Applied,RecordId,Reference " +
                                                     " FROM Accounting.SubsiLedgerNonInv"+
                                                     " WHERE Applied != Amount" +
                                                     " and RecordID='"+trans[8]+"'", Connection);
                
                if (getdt.Rows.Count == 1)
                {
                    foreach (DataRow dt in getdt.Rows)
                    {
                        codes = dt[1].ToString()+";";
                        codes += (string.IsNullOrEmpty(dt[2].ToString()) ? "" : Convert.ToDateTime(dt[2].ToString()).ToShortDateString()) + ";";
                        codes += (string.IsNullOrEmpty(dt[3].ToString()) ? "" : Convert.ToDateTime(dt[3].ToString()).ToShortDateString()) + ";";
                        codes += dt[4].ToString()+";";
                        codes += dt[5].ToString()+";";
                        codes += dt[6].ToString() + ";";
                        codes += dt[7].ToString() + ";";
                        codes += dt[8].ToString() + ";";
                        codes += dt[9].ToString() + ";";
                        codes += dt[10].ToString() + ";";
                        codes += dt[11].ToString() + ";";
                    }
                }
                gridlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("trans2doc"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv3.FindEditRowCellTemplateControl((GridViewDataColumn)gv3.Columns[5], "glSubsidiaryCode");
                var selectedValues = code;
                CriteriaOperator selectionCriteria = new InOperator("SupplierCode", new string[] { code });
                TransDocs.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression3"] = TransDocs.FilterExpression;
                grid.DataSourceID = "TransDocs";
                grid.DataBind();
                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    var col = "RecordId";
                    if (grid.GetRowValues(i, col) != null && trans.Length > 3 && trans[3] != null)
                        if (grid.GetRowValues(i, col).ToString() == trans[3])
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, col).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
                gridlookup.JSProperties["cp_change"] = true;
            }
            else if (e.Parameters.Contains("accountcode"))
            {
                DataTable getdt = Gears.RetriveData2("select AccountCode,Description from Accounting.ChartOfAccount where ISNULL(AllowJV,0) = 1 and AccountCode ='" + code + "'",Connection);
                if (getdt.Rows.Count == 1)
                {
                    foreach (DataRow dt in getdt.Rows)
                    {
                        codes = dt[1].ToString() + ";";
                    }
                }
                gridlookup.JSProperties["cp_codes"] = codes;
                gridlookup.JSProperties["cp_identifier"] = "change";
            }
            else if (e.Parameters.Contains("subsicode"))
            {
                DataTable getdt = Gears.RetriveData2("select SubsiCode,AccountCode,Description from Accounting.GLSubsiCode where SubsiCode ='" + code + "' and AccountCode ='" + val2 + "'",Connection);
                if (getdt.Rows.Count == 1)
                {
                    foreach (DataRow dt in getdt.Rows)
                    {
                        codes = dt[2].ToString() + ";";
                    }
                }
                gridlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("bankacc"))
            {
                DataTable getdt = Gears.RetriveData2("select BankCode,Branch,NextCheckNumber from masterfile.BankAccount where BankAccountCode ='" + code + "'",Connection);
                if (getdt.Rows.Count == 1)
                {
                    foreach (DataRow dt in getdt.Rows)
                    {
                        codes = dt[0].ToString() + ";";
                        codes += dt[1].ToString() + ";";
                        codes += dt[2].ToString() + ";";
                    }
                }

                SqlDataSource ds = Masterfilebiz;
                string profit = "";
                string cost = "";
                string payee = "";
                ds.SelectCommand = string.Format("SELECT SupplierCode, Name,ProfitCenterCode,CostCenterCode,PayeeName FROM Masterfile.[BPSupplierInfo] where SupplierCode = '" + gvSupplier.Text + "'");
                DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                if (inb.Count > 0)
                {
                    SupplierName.Text = inb[0][1].ToString();
                    profit = inb[0][2].ToString();
                    cost = inb[0][3].ToString();
                    payee = inb[0][4].ToString();
                }
                payee = string.IsNullOrEmpty(payee) ? SupplierName.Text : payee;
                ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo]");
                gvSupplier.DataBind();
                gridlookup.JSProperties["cp_payee"] = payee;
                gridlookup.JSProperties["cp_supp"] = true;  

                gridlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("SubsiGetCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv3.FindEditRowCellTemplateControl((GridViewDataColumn)gv3.Columns[5], "glSubsidiaryCode");
                var selectedValues = code;
                CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { code });
                Subsi.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = Subsi.FilterExpression;
                grid.DataSourceID = "Subsi";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    var col = "SubsiCode";
                    if (grid.GetRowValues(i, col) != null)
                        if (grid.GetRowValues(i, col).ToString() == val2)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, col).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
                gridlookup.JSProperties["cp_identifier"] = "change";
            }
            else if (e.Parameters.Contains("CostGetCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv3.FindEditRowCellTemplateControl((GridViewDataColumn)gv3.Columns[8], "glCostcode");
                var selectedValues = code;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { code });
                Costsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression2"] = Costsql.FilterExpression;
                grid.DataSourceID = "Costsql";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    var col = "CostCenterCode";
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
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = DocDate.Text;
            _Entity.Connection = Connection;
            _Entity.SupplierCode = gvSupplier.Text;
            _Entity.SupplierName = SupplierName.Text;
            _Entity.TransType = TransType.Text;
            _Entity.TotalCheckAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalCheckAmount.Text) && TotalCheckAmount.Text!="NaN" ? "0" : TotalCheckAmount.Text);
            _Entity.TotalAppliedAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalAppliedAmount.Text) ? "0" : TotalAppliedAmount.Text);
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
            _Entity.Remarks = txtRemarks.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    
                    gv1.UpdateEdit();
                    gv2.UpdateEdit();
                    gv3.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

                        gv2.DataSourceID = "odsDetail2";
                        odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv2.UpdateEdit();

                        gv3.DataSourceID = "odsDetail3";
                        odsDetail3.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv3.UpdateEdit();

                        Post();
                        Validate();
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

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                break;

                case "Supp":
                    SqlDataSource ds = Masterfilebiz;
                    string profit = "";
                    string cost = "";
                    string payee = "";
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name,ProfitCenterCode,CostCenterCode,PayeeName FROM Masterfile.[BPSupplierInfo] where SupplierCode = '" + gvSupplier.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        SupplierName.Text = inb[0][1].ToString();
                        profit = inb[0][2].ToString();
                        cost = inb[0][3].ToString();
                        payee = inb[0][4].ToString();
                    }
                    payee = string.IsNullOrEmpty(payee) ? SupplierName.Text : payee;
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo]");
                    gvSupplier.DataBind();
                    cp.JSProperties["cp_prof"] = profit;
                    cp.JSProperties["cp_cost"] = cost;
                    cp.JSProperties["cp_payee"] = payee;
                    cp.JSProperties["cp_supp"] = true;  
                break;

                case "SI":
                    GetSelectedVal();
                break;

                case "Trans":
                ds = Masterfilebiz;
                if (TransType.Value.ToString() == "F" || TransType.Value.ToString() == "A")
                {
                        gv2.ClientVisible = false;
                        gv3.ClientVisible = false;
                        if (TransType.Value.ToString() == "F")
                        {
                            gvSupplier.ClientEnabled = false;
                            
                            profit = "";
                            cost = "";
                            payee = "";
                            string supp = "";
                            DataTable getsupp = Gears.RetriveData2(" Select Value from it.SystemSettings where Code='COMPBPCODE'",Connection);
                            foreach (DataRow dt in getsupp.Rows)
                            {
                                supp = dt[0].ToString();
                                gvSupplier.Value = supp;
                            }

                            ds.SelectCommand = string.Format("SELECT SupplierCode, Name,ProfitCenterCode,CostCenterCode,PayeeName FROM Masterfile.[BPSupplierInfo] where SupplierCode = '" + supp + "'");
                            inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                            if (inb.Count > 0)
                            {
                                SupplierName.Text = inb[0][1].ToString();
                                profit = inb[0][2].ToString();
                                cost = inb[0][3].ToString();
                                payee = inb[0][4].ToString();
                            }
                            cp.JSProperties["cp_payee"] = payee;
                        }
                        else
                        {
                            gvSupplier.ClientEnabled = true;
                        }
                        cp.JSProperties["cp_trans"] = true;  
                    }
                    else
                    {
                        gv2.ClientVisible = true;
                        gv3.ClientVisible = true;
                    }
                ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo]");
                gvSupplier.DataBind();
                break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "" ;
            string SizeCode = "";
            
            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
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
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            //string[] selectedValues = glInvoice.Text.Split(';');
            //CriteriaOperator selectionCriteria = new InOperator(glInvoice.KeyFieldName, selectedValues);
            //SalesInvoicedet.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["SI"] = SalesInvoicedet.FilterExpression;
            //gv1.DataSource = SalesInvoicedet;
            //if (gv1.DataSourceID != "")
            //{
            //    gv1.DataSourceID = null;
            //}
            //gv1.DataBind();
            //Session["Datatable"] = "1";

            ////DataTable dt2 = new DataTable();
            //////dt2 = Gears.RetriveData2("SELECT DocNumber, SONumber, LineNumber, DetailVersion, ItemCode,ColorCode,ClassCode,SizeCode,FullDesc, CONVERT(varchar(MAX),DeliveredQty) DeliveredQty,Unit,CONVERT(varchar(MAX),BulkQty) BulkQty,BulkUnit,CONVERT(varchar(MAX),ReturnedQty) ReturnedQty,SubstituteItem,SubstituteColor,StatusCode, CONVERT(varchar(MAX),BaseQty) BaseQty,BarcodeNo,Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9 FROM Sales.DeliveryReceiptDetail WHERE DocNumber = '" + txtDocNumber.Text + "'");
            ////dt2 = Gears.RetriveData2("select DocNumber,LineNumber,Transtype,TransDocNumber,TransDate,TransAPAmount,TransVatAmount,EWT,ATCcode,Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9 from Accounting.apvoucherdetail WHERE DocNumber = '" + txtDocNumber.Text + "'");
            ////dt2.PrimaryKey = new DataColumn[] { 
            ////dt2.Columns["TransDocNumber"],dt2.Columns["LineNumber"]};

            //foreach (GridViewColumn col in gv1.VisibleColumns)
            //{
            //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    dt.Columns.Add(dataColumn.FieldName);
            //}
            //for (int i = 0; i < gv1.VisibleRowCount; i++)
            //{
            //    DataRow row = dt.Rows.Add();
            //    foreach (DataColumn col in dt.Columns)
            //        row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            //}

            //dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["TransDocNumber"],dt.Columns["LineNumber"]};

            ////for (int i = 0; i < dt2.Rows.Count; i++)
            ////{
            ////    foreach (DataRow _row in dt2.Rows)
            ////    {
            ////        DataRow row = dt.NewRow();
            ////        row["LineNumber"] = _row["LineNumber"].ToString();
            ////        row["Transtype"] = _row["Transtype"].ToString();
            ////        row["TransDocNumber"] = _row["TransDocNumber"].ToString();
            ////        row["TransDate"] = _row["TransDate"].ToString();
            ////        row["TransAPAmount"] = Convert.ToDecimal(_row["TransAPAmount"].ToString());
            ////        row["TransVatAmount"] = Convert.ToDecimal(_row["TransVatAmount"].ToString());
            ////        row["EWT"] = _row["EWT"].ToString();
            ////        row["ATCcode"] = _row["ATCcode"].ToString();
            ////        row["Field1"] = _row["Field1"].ToString();
            ////        row["Field2"] = _row["Field2"].ToString();
            ////        row["Field3"] = _row["Field3"].ToString();
            ////        row["Field4"] = _row["Field4"].ToString();
            ////        row["Field5"] = _row["Field5"].ToString();
            ////        row["Field6"] = _row["Field6"].ToString();
            ////        row["Field7"] = _row["Field7"].ToString();
            ////        row["Field8"] = _row["Field8"].ToString();
            ////        row["Field9"] = _row["Field9"].ToString();

            ////        dt.Rows.Add(row);
            ////    }
            ////}

            //gv1.DataSource = dt;
            //if (gv1.DataSourceID != "")
            //{
            //    gv1.DataSourceID = null;
            //}
            //gv1.DataBind();

            return dt;
        }

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SI"] = null;
            }

            if (Session["SI"] != null)
            {
                gv1.DataSourceID = SalesInvoicedet.ID;
                SalesInvoicedet.FilterExpression = Session["SI"].ToString();
                //gridview.DataSourceID = null;
            }

            if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            {
                gv3.Columns["NewButtonCol"].Width = 40;
            }
            else
            {
                gv3.Columns["NewButtonCol"].Width = 0;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IsCross"] = true;
            e.NewValues["CheckAmount"] = 0;
        }

        protected void gv3_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["DebitAmount"] = 0;
            e.NewValues["CreditAmount"] = 0;
        }



    }
}