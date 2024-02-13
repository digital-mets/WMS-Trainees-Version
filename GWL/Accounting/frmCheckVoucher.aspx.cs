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
        Entity.CheckVoucher.CheckVoucherTagging _CVTagging = new CheckVoucher.CheckVoucherTagging();

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
            
            DocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                Session["ChkVSup"] = null;
                Session["ChkVType"] = null;
                Session["chkVClearData"] = null;
                Session["ChkVDatatable"] = null;
                Session["ChkVTransNo"] = null;
                Session["ChkVSupChange"] = null;
                Session["DtpDocDate"] = null;

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
                Session["ChkVSup"] = _Entity.SupplierCode.ToString().Trim();
                Session["DtpDocDate"] = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                SupplierName.Text = _Entity.SupplierName;
                TransType.Value = _Entity.TransType;
                Session["ChkVType"] = _Entity.TransType;
                if (TransType.Value != null && TransType.Value.ToString() == "Fund Transfer")
                {
                    gvSupplier.ClientEnabled = false;
                }
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

                aglTransNo.Text = _Entity.RefTrans.ToString();

                gv1.KeyFieldName = "DocNumber;LineNumber";

                DataTable chkdetail = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherDetail WHERE DocNumber = '" + txtDocNumber.Text + "'",Connection);
                if (chkdetail.Rows.Count == 0)
                {
                    gv1.DataSourceID = "sdsDetail";
                }
                else
                {
                    gv1.DataSourceID = "odsDetail";
                }

                DataTable chktag = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherTagging WHERE DocNumber = '" + txtDocNumber.Text + "'",Connection);
                if (chktag.Rows.Count == 0)
                {
                    gv2.DataSourceID = "sdsDetail2";
                }
                else
                {
                    gv2.DataSourceID = "odsDetail2";
                }

                DataTable chkadj = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherAdjEntry WHERE DocNumber = '" + txtDocNumber.Text + "'",Connection);
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

                Filter();
                HideOthers();
                DisableObjects();
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
            if (strresult != "")
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
            if (strresult != "")
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
        protected void Button_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton button = sender as ASPxButton;
            button.ClientVisible = !view;
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

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "CVDelete" || e.ButtonID == "ChkDelete" || e.ButtonID == "AdjDelete")
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
                                                     " and RecordId='"+trans[8]+"'", Connection);
                
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
                DataTable getdt = Gears.RetriveData2("select AccountCode,Description from Accounting.ChartOfAccount WHERE ISNULL(AllowJV,0) = 1 and AccountCode ='" + code + "' AND ISNULL(IsInActive,0) = 0",Connection);
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
                DataTable getdt = Gears.RetriveData2("select SubsiCode,AccountCode,Description from Accounting.GLSubsiCode WHERE SubsiCode ='" + code + "' and AccountCode ='" + val2 + "'",Connection);
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
                DataTable getdt = Gears.RetriveData2("select BankCode,Branch,NextCheckNumber from masterfile.BankAccount WHERE BankAccountCode ='" + code + "'",Connection);
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
                ds.SelectCommand = string.Format("SELECT SupplierCode, Name,ProfitCenterCode,CostCenterCode,PayeeName FROM Masterfile.[BPSupplierInfo] WHERE SupplierCode = '" + gvSupplier.Text + "'");
                DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                if (inb.Count > 0)
                {
                    SupplierName.Text = inb[0][1].ToString();
                    profit = inb[0][2].ToString();
                    cost = inb[0][3].ToString();
                    payee = inb[0][4].ToString();
                }
                payee = string.IsNullOrEmpty(payee) ? SupplierName.Text : payee;
                ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo] WHERE ISNULL(IsInactive,0) = 0");
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
            _Entity.TransType = TransType.Value.ToString();
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
            _Entity.RefTrans = String.IsNullOrEmpty(aglTransNo.Text) ? null : aglTransNo.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            //_Entity.LastEditedDate = DateTime.Now.ToString();

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited, ISNULL(SupplierCode,'') AS SupplierCode FROM Accounting.CheckVoucher WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());

            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    gv1.UpdateEdit();
                    //gv2.UpdateEdit();
                    gv3.UpdateEdit();                    

                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.CheckVoucher", 1, Session["ConnString"].ToString());
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (txtHLastEditedDate.Text != (LastEditedCheck.Rows[0]["LastEdited"].ToString() == "1/1/1900 12:00:00 AM" || String.IsNullOrEmpty(LastEditedCheck.Rows[0]["LastEdited"].ToString()) ? "" : LastEditedCheck.Rows[0]["LastEdited"].ToString()))
                    {
                        cp.JSProperties["cp_message"] = "The transaction has been modified by other user(s)." + Environment.NewLine + "Changes you made will not be saved!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

                        if (Session["ChkVClearData"] == "1")
                        {
                            _Entity.DeleteTagsAndAdj(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv2.DataSourceID = "odsDetail2";
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv2.UpdateEdit();
                        }
                        else if (Session["ChkVDatatable"] == "1")
                        {
                            _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv2.DataSourceID = sdsTransDetail.ID;
                            gv2.UpdateEdit();
                        }
                        else
                        {
                            if (gvSupplier.Text != LastEditedCheck.Rows[0]["SupplierCode"].ToString())
                            {
                                _Entity.DeleteTagsAndAdj(txtDocNumber.Text, Session["ConnString"].ToString());
                            }
                            gv2.DataSourceID = "odsDetail2";
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv2.UpdateEdit();
                        }

                        gv3.DataSourceID = "odsDetail3";
                        odsDetail3.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv3.UpdateEdit();

                        Post();
                        Validate();

                        if (e.Parameter == "Add")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else if (e.Parameter == "Update")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Updated!";
                        }

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
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name,ProfitCenterCode,CostCenterCode,PayeeName FROM Masterfile.[BPSupplierInfo] WHERE SupplierCode = '" + gvSupplier.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        SupplierName.Text = inb[0][1].ToString();
                        profit = inb[0][2].ToString();
                        cost = inb[0][3].ToString();
                        payee = inb[0][4].ToString();
                    }
                    payee = string.IsNullOrEmpty(payee) ? SupplierName.Text : payee;
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo] WHERE ISNULL(IsInactive,0) = 0");
                    gvSupplier.DataBind();
                    cp.JSProperties["cp_prof"] = profit;
                    cp.JSProperties["cp_cost"] = cost;
                    cp.JSProperties["cp_payee"] = payee;
                    cp.JSProperties["cp_supp"] = true;
                    Session["ChkVSup"] = _Entity.SupplierCode;
                    Session["ChkVType"] = _Entity.TransType;
                    Session["DtpDocDate"] = _Entity.DocDate;
                    //jay


                    Session["ChkVSupChange"] = "1";
                    Filter();
                    HideOthers();
                    DisableObjects();
                    txtRemarks.Focus();
                break;


                case "DocDateChange": // jay
                Session["DtpDocDate"] = _Entity.DocDate;
                Session["DtpDocDate"] = _Entity.DocDate;
                gv2.DataSourceID = null;
                gv2.DataSource = null;
                gv2.DataBind();
                DisableObjects();
                Gears.RetriveData2(" delete FROM Accounting.CheckVoucherTagging where docnumber='" + txtDocNumber .Text+ "'", Connection);
                   // txtDocNumber
                break;


                case "SI":
                    GetSelectedVal();
                    DisableObjects();
                break;

                case "Trans":
                    ds = Masterfilebiz;
                    if (TransType.Value.ToString() == "Fund Transfer" || TransType.Value.ToString() == "Payment-LN" ||
                        TransType.Value.ToString() == "Advances" || TransType.Value.ToString() == "Advances-NT")
                    {
                        gv2.ClientVisible = false;
                        gv3.ClientVisible = false;
                        aglTransNo.ClientVisible = false;
                        btnGenerate.ClientVisible = false;
                        lblSpace.ClientVisible = false;

                        //^^^Session["ChkVDatatable"] = "0";
                        gv2.DataSourceID = null;
                        gv2.DataBind();
                        frmlayout1.FindItemOrGroupByName("TransactionNo").ClientVisible = false;

                        //gv3.DataSourceID = null;
                        //gv3.DataBind();

                        Session["ChkVClearData"] = "1";

                        if (TransType.Value.ToString() == "Fund Transfer")
                        {
                            gvSupplier.ClientEnabled = false;
                            
                            profit = "";
                            cost = "";
                            payee = "";
                            string supp = "";
                            DataTable getsupp = Gears.RetriveData2(" Select Value from it.SystemSettings WHERE Code='COMPBPCODE'",Connection);
                            foreach (DataRow dt in getsupp.Rows)
                            {
                                supp = dt[0].ToString();
                                gvSupplier.Value = supp;
                            }

                            ds.SelectCommand = string.Format("SELECT SupplierCode, Name,ProfitCenterCode,CostCenterCode,PayeeName FROM Masterfile.[BPSupplierInfo] WHERE SupplierCode = '" + supp + "'");
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
                        Session["ChkVClearData"] = "0";
                        Session["ChkVDatatable"] = "1";     //^^^
                        gv2.DataSourceID = null;
                        gv2.DataBind();
                        gv2.ClientVisible = true;
                        //gv3.DataSourceID = "sdsDetail3";
                        //gv3.DataBind();
                        gv3.ClientVisible = true;
                        aglTransNo.ClientVisible = true;
                        btnGenerate.ClientVisible = true;
                        lblSpace.ClientVisible = true;
                        frmlayout1.FindItemOrGroupByName("TransactionNo").ClientVisible = true;
                        gvSupplier.ClientEnabled = true;
                        Session["ChkVSup"] = _Entity.SupplierCode;
                        Session["ChkVType"] = _Entity.TransType;
                        Filter();
                        DisableObjects();
                    }

                    cp.JSProperties["cp_generated"] = true;
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo] WHERE ISNULL(IsInactive,0) = 0");
                    gvSupplier.DataBind();

                    DocDate.Focus();

                break;

                case "CallbackTransNo":

                    Session["ChkVTransNo"] = null;
                    Session["ChkVTransNo"] = aglTransNo.Text;
                    HideOthers();
                    GetSelectedVal();
                    DisableObjects();
                    cp.JSProperties["cp_generated"] = true;

                break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "" ;
            //string SizeCode = "";
            
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
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        private DataTable GetSelectedVal()
        {
            //^^^Session["ChkVDatatable"] = "0";
            gv2.DataSourceID = null;
            gv2.DataBind();
            DataTable dt = new DataTable();

            sdsTransDetail.SelectCommand = Detail();
            gv2.DataSource = sdsTransDetail;
            if (gv2.DataSourceID != "")
            {
                gv2.DataSourceID = null;
            }
            gv2.DataBind();
            Session["ChkVDatatable"] = "1";

            foreach (GridViewColumn col in gv2.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv2.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv2.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] {
            dt.Columns["RecordId"],dt.Columns["TransDocNumber"]};

            return dt;
        }

        public string Detail()
        {

            string query = "";
            string Value = "";
            int cnt = 0;
            string bridge = "";

            if (aglTransNo.Text == "") 
            {
                Value = "1 = 0";
            }
            else
            {
                int count = aglTransNo.Text.Split(';').Length;
                var pieces = aglTransNo.Text.Split(new[] { ';' }, count);

                foreach (string c in pieces)
                {
                    var a = c.Split(new[] { '-' }, 2);

                    if (cnt != 0)
                    {
                        bridge = " OR ";
                    }
                    Value += bridge + "(RecordId = '" + a[0].ToString() + "' AND DocNumber ='" + a[1].ToString() + "')";

                    cnt = cnt + 1;
                }
            }

            query = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, TransType ,DocNumber AS TransDocNumber, DocDate AS TransDate, "
                + " AccountCode AS TransAccountCode, SubsiCode AS TransSubsiCode, ProfitCenter AS TransProfitCenter, CostCenter AS TransCostCenter, BizPartnerCode AS TransBizPartnerCode, "
                + " ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAmount, ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAppliedAmount, DueDate AS TransDueDate, '2' AS Version, RecordId, "
                + " '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
                + " FROM Accounting.SubsiLedgerNonInv WHERE ISNULL(Applied,0) <> ISNULL(Amount,0) AND ( " + Value + " )";

            return query;
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

        private void Filter()
        {
            String _SupplierCode = (Session["ChkVSup"] != null) ? Session["ChkVSup"].ToString() : null;
            String _TransType = (Session["ChkVType"] != null) ? Session["ChkVType"].ToString() : null;
            if (!String.IsNullOrEmpty(_SupplierCode) && !String.IsNullOrEmpty(_TransType))
            {
                string strSQLCmd = "SELECT MAX(CASE WHEN Code = 'GLADVSUPTR' THEN Value ELSE NULL END) AS GLAdvSuppTr, " +
                                   "       MAX(CASE WHEN Code = 'GLADVSUPNT' THEN Value ELSE NULL END) AS GLAdvSuppNT, " +
                                   "       MAX(CASE WHEN Code = 'GLAPTRADE' THEN Value ELSE NULL END) AS GLAPTrade, " +
                                   "       MAX(CASE WHEN Code = 'GLAPMASK' THEN Value ELSE NULL END) AS GLAPMask, " +
                                   // 03-23-2017    GC  Added GLOTHADV on Code
                                   "       MAX(CASE WHEN Code = 'GLOTHADV' THEN Value ELSE NULL END) AS GLOthAdv " +
                                   "  FROM IT.SystemSettings WHERE Code IN ('GLADVSUPTR','GLADVSUPNT','GLAPTRADE','GLAPMASK','GLOTHADV')";
                                   // END
                DataTable dtTemp = Gears.RetriveData2(strSQLCmd, Session["ConnString"].ToString());

                string strGLAdvSuppTr = (dtTemp.Rows[0]["GLAdvSuppTr"] == DBNull.Value) ? "" : dtTemp.Rows[0]["GLAdvSuppTr"].ToString();
                string strGLAdvSuppNT = (dtTemp.Rows[0]["GLAdvSuppNT"] == DBNull.Value) ? "" : dtTemp.Rows[0]["GLAdvSuppNT"].ToString();
                string strGLAPTrade = (dtTemp.Rows[0]["GLAPTrade"] == DBNull.Value) ? "" : dtTemp.Rows[0]["GLAPTrade"].ToString();
                string strGLAPMask = (dtTemp.Rows[0]["GLAPMask"] == DBNull.Value) ? "" : dtTemp.Rows[0]["GLAPMask"].ToString();
                string strGLOthAdv = (dtTemp.Rows[0]["GLOthAdv"] == DBNull.Value) ? "" : dtTemp.Rows[0]["GLOthAdv"].ToString();
                dtTemp.Dispose();

                sdsRefTrans.ConnectionString = Session["ConnString"].ToString();
                sdsRefTrans.SelectCommand = 
                    "SELECT DISTINCT RecordId, TransType, DocNumber, DocDate, AccountCode, SubsiCode, " +
                    "       ProfitCenter, CostCenter, BizPartnerCode, ISNULL(Amount,0) - ISNULL(Applied,0) AS Amount, " +
                    "       CounterDocNumber AS CounterReceiptNo " +
                    // 03-01-2017   GC  Added code on SDS
                    "       , ISNULL(Reference,'') AS Reference " +
                    // END
                    "  FROM Accounting.SubsiLedgerNonInv " +
                    " WHERE ISNULL(Applied,0) <> ISNULL(Amount,0) AND BizPartnerCode = '" + Session["ChkVSup"].ToString() + "' and DocDate<='" + Session["DtpDocDate"].ToString() + "'";
                if (_TransType == "Payment")
                {
                    sdsRefTrans.SelectCommand = sdsRefTrans.SelectCommand +
                    "       AND AccountCode IN ('"+strGLAPTrade+"', '"+strGLAdvSuppTr+"')";
                }
                else if (_TransType == "Payment-NT")
                {
                    sdsRefTrans.SelectCommand = sdsRefTrans.SelectCommand +
                    "       AND (AccountCode LIKE '" + strGLAPMask + "' OR AccountCode = '" + strGLAdvSuppNT + "'" +
                    // 03-23-2017    GC  Added GLOTHADV on Code
                    "       OR AccountCode IN (" + strGLOthAdv + "))" +
                    //END
                    "       AND AccountCode != '" + strGLAPTrade + "'";

                }
            }
            sdsRefTrans.DataBind();
            aglTransNo.DataBind();
        }

        protected void aglTransNo_Init(object sender, EventArgs e)
        {
            Filter();
        }

        protected void gv2_Init(object sender, EventArgs e)
        {

        }

        protected void gv2_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["ChkVDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["RecordId"], values.NewValues["TransDocNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    if (row != null)
                    {
                        row["LineNumber"] = values.NewValues["LineNumber"];
                        //row["RecordId"] = values.NewValues["RecordId"];
                        row["TransType"] = values.NewValues["TransType"];
                        row["TransDate"] = values.NewValues["TransDate"];
                        row["TransDueDate"] = values.NewValues["TransDueDate"];
                        row["TransAmount"] = values.NewValues["TransAmount"];
                        row["TransAppliedAmount"] = values.NewValues["TransAppliedAmount"];
                        row["TransAccountCode"] = values.NewValues["TransAccountCode"];
                        row["TransSubsiCode"] = values.NewValues["TransSubsiCode"];
                        row["TransProfitCenter"] = values.NewValues["TransProfitCenter"];
                        row["TransCostCenter"] = values.NewValues["TransCostCenter"];
                        row["TransBizPartnerCode"] = values.NewValues["TransBizPartnerCode"];
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
                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["RecordId"], values.Keys["TransDocNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (DataRow dtRow in source.Rows)
                {
                    _CVTagging.RecordId = dtRow["RecordId"].ToString();
                    _CVTagging.TransDocNumber = dtRow["TransDocNumber"].ToString();
                    _CVTagging.TransType = dtRow["TransType"].ToString();
                    _CVTagging.TransDate = Convert.ToDateTime(dtRow["TransDate"].ToString());
                    _CVTagging.TransDueDate = Convert.ToDateTime(dtRow["TransDueDate"].ToString());
                    _CVTagging.TransAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TransAmount"]) ? 0 : dtRow["TransAmount"]);
                    _CVTagging.TransAppliedAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TransAppliedAmount"]) ? 0 : dtRow["TransAppliedAmount"]);
                    _CVTagging.TransAccountCode = dtRow["TransAccountCode"].ToString();
                    _CVTagging.TransSubsiCode = dtRow["TransSubsiCode"].ToString();
                    _CVTagging.TransProfitCenter = dtRow["TransProfitCenter"].ToString();
                    _CVTagging.TransCostCenter = dtRow["TransCostCenter"].ToString();
                    _CVTagging.TransBizPartnerCode = dtRow["TransBizPartnerCode"].ToString();
                    _CVTagging.Field1 = dtRow["Field1"].ToString();
                    _CVTagging.Field2 = dtRow["Field2"].ToString();
                    _CVTagging.Field3 = dtRow["Field3"].ToString();
                    _CVTagging.Field4 = dtRow["Field4"].ToString();
                    _CVTagging.Field5 = dtRow["Field5"].ToString();
                    _CVTagging.Field6 = dtRow["Field6"].ToString();
                    _CVTagging.Field7 = dtRow["Field7"].ToString();
                    _CVTagging.Field8 = dtRow["Field8"].ToString();
                    _CVTagging.Field9 = dtRow["Field9"].ToString();
                    _CVTagging.AddCheckVoucherTagging(_CVTagging);
                }
            }
        }

        protected void HideOthers()
        {
            if (String.IsNullOrEmpty(TransType.Text))
            {
                TransType.Value = "Payment";
            }

            if (TransType.Value.ToString() == "Fund Transfer" || TransType.Value.ToString() == "Payment-LN" ||
                TransType.Value.ToString() == "Advances" || TransType.Value.ToString() == "Advances-NT")
            {
                gv2.ClientVisible = false;
                gv3.ClientVisible = false;
                aglTransNo.ClientVisible = false;
                btnGenerate.ClientVisible = false;
                lblSpace.ClientVisible = false;

                //^^^Session["ChkVDatatable"] = "0";
                gv2.DataSourceID = null;
                gv2.DataBind();
                frmlayout1.FindItemOrGroupByName("TransactionNo").ClientVisible = false;

                //gv3.DataSourceID = null;
                //gv3.DataBind();

                Session["ChkVClearData"] = "1";

                if (TransType.Value.ToString() == "Fund Transfer")
                {
                    gvSupplier.ClientEnabled = false;
                }
                else
                {
                    gvSupplier.ClientEnabled = true;
                }
            }
            else
            {
                Session["ChkVClearData"] = "0";
                gv2.ClientVisible = true;
                gv3.ClientVisible = true;
                aglTransNo.ClientVisible = true;
                btnGenerate.ClientVisible = true;
                lblSpace.ClientVisible = true;
                frmlayout1.FindItemOrGroupByName("TransactionNo").ClientVisible = true;
                gvSupplier.ClientEnabled = true;  

                DataTable chktag = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherTagging WHERE DocNumber = '" + txtDocNumber.Text + "'", Connection);
                if (chktag.Rows.Count == 0)
                {
                    gv2.DataSourceID = "sdsDetail2";
                }
                else
                {
                    if (Session["ChkVSupChange"] != null)
                    {
                        gv2.DataSourceID = "sdsDetail2";
                    }
                    else
                    {
                        gv2.DataSourceID = "odsDetail2";
                    }
                }

                DataTable chkadj = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherAdjEntry WHERE DocNumber = '" + txtDocNumber.Text + "'", Connection);
                if (chkadj.Rows.Count == 0)
                {
                    gv3.DataSourceID = "sdsDetail3";
                }
                else
                {
                    gv3.DataSourceID = "odsDetail3";
                }
            }
        }

        protected void DisableObjects()
        {
            if (Session["ChkVSup"] != null && Session["ChkVSup"] != "")
            {
                btnGenerate.ClientEnabled = true;
                aglTransNo.ClientEnabled = true;
            }
            else
            {
                btnGenerate.ClientEnabled = false;
                aglTransNo.ClientEnabled = false;
            }
        }

    }
}