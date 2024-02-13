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
using GearsAccounting;

namespace GWL
{
    public partial class frmCashPaymentVoucher : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.CashPaymentVoucher _Entity = new CashPaymentVoucher();//Calls entity odsHeader
        Entity.CashPaymentVoucher.CashPaymentVoucherApplication _EntityDetailApplication = new CashPaymentVoucher.CashPaymentVoucherApplication();//Call entity sdsDetail
        Entity.CashPaymentVoucher.CashPaymentVoucherAdjustment _EntityDetailAdjustment = new CashPaymentVoucher.CashPaymentVoucherAdjustment();//Call entity sdsDetail
        string val = "";

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

            gv1.KeyFieldName = "DocNumber;LineNumber";
            gv2.KeyFieldName = "LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry and delete
            }

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["ACTCPVDocDate"] = "";
                Session["ACTCPVSupplier"] = "";

                txtDocNumber.ReadOnly = true;
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
                
                dtpDocDate.Text = DateTime.Now.ToShortDateString();
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                Session["ACTCPVDocDate"] = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                Session["ACTCPVSupplier"] = _Entity.SupplierCode.ToString();
                QueryRefresh();
                glReferenceChecks.Text = _Entity.ReferenceChecks;
                glSupplierCode.Value = _Entity.SupplierCode.ToString();
                txtSupplierName.Value = _Entity.SupplierName.ToString();
                txtCashAmount.Value = _Entity.CashAmount.ToString();
                txtTotalAppliedAmount.Value = _Entity.TotalAppliedAmount.ToString();
                txtTotalAdjustment.Value = _Entity.TotalAdjustment.ToString();
                txtVariance.Text = _Entity.Variance.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                txtHAddedBy.Value = _Entity.AddedBy;
                txtHAddedDate.Value = _Entity.AddedDate;
                txtHLastEditedBy.Value = _Entity.LastEditedBy;
                txtHLastEditedDate.Value = _Entity.LastEditedDate;
                txtHSubmittedBy.Value = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = _Entity.SubmittedDate;
                txtHCancelledBy.Value = _Entity.CancelledBy;
                txtHCancelledDate.Value = _Entity.CancelledDate;
                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                if (!String.IsNullOrEmpty(_Entity.SupplierCode.ToString()))
                {
                    sdsApplicationDetail.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocDate) AS VARCHAR(5)),5) AS LineNumber, "
                                                   + " DocNumber, DocNumber AS TransDocNumber, TransType, CAST(DocDate AS DATE) AS TransDate, "
                                                   + " CAST(DueDate AS DATE) AS DueDate, AccountCode, SubsiCode AS SubsidiaryCode, "
                                                   + " ProfitCenter AS ProfitCenterCode, CostCenter AS CostCenterCode, ISNULL(Amount,0) AS TransAmount, "
                                                   + " ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAppliedAmount, '' AS Field1, '' AS Field2, '' AS Field3, "
                                                   + " '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
                                                   + " FROM Accounting.SubsiLedgerNonInv where (ISNULL(Amount,0) - ISNULL(Applied,0)) !=0 "
                                                   + " AND DocDate <= '" + dtpDocDate.Text + "' AND BizPartnerCode = '" + glSupplierCode.Text + "'";
                    //glReferenceChecks.DataSourceID = "sdsApplicationDetail";
                    //glReferenceChecks.DataBind();
                    sdsApplicationDetail.DataBind();
                }
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
                        txtCashAmount.ReadOnly = true;
                        txtSupplierName.ReadOnly = true;
                        //var fl2 = frmlayout1 as ASPxFormLayout;
                        //var referencedocs2 = fl2.FindItemOrGroupByName("RC");
                        //referencedocs2.ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        txtCashAmount.ReadOnly = true;
                        txtSupplierName.ReadOnly = true;
                        //var fl1 = frmlayout1 as ASPxFormLayout;
                        //var referencedocs1 = fl1.FindItemOrGroupByName("RC");
                        //referencedocs1.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.CashPaymentVoucherApplication WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                DataTable dtbldetail1 = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.CashPaymentVoucherAdjustment WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN

                gv1.DataSourceID = (dtbldetail1.Rows.Count > 0 ? "odsDetail" : "sdsDetail2");
                gv2.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail2" : "sdsDetail");

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
            gparam._TransType = "ACTCPV";
            string strresult = GearsAccounting.GAccounting.CashPaymentVoucher_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCPV";
            gparam._Table = "Accounting.CashPaymentVoucher";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.CashPaymentVoucher_Post(gparam);
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

        protected void gvLookupLoad_2(object sender, EventArgs e)//Control for all lookup in details/grid
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

        protected void gv2_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            //ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            //spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            //if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
            //    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
            //    e.ButtonType == ColumnCommandButtonType.Update)
            //    e.Visible = false;
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "D" || Request.QueryString["entry"] == "V")
            {
                if (e.ButtonID == "Delete1" || e.ButtonID == "Delete")
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
            if (Session["FilterExpression3"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsApplicationDetail";
                sdsApplicationDetail.FilterExpression = Session["FilterExpression3"].ToString();
                //Session["FilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string TransDoc = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("TransDocNumber"))
            {
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT TransType, CONVERT(varchar(20),DocDate,101) AS TransDate, CONVERT(varchar(20),DueDate,101) AS DueDate, AccountCode, SubsiCode AS SubsidiaryCode, ProfitCenter AS ProfitCenterCode, CostCenter AS CostCenterCode, ISNULL(Amount,0) AS TransAmount, ISNULL(Amount,0)-ISNULL(Applied,0) AS TransAppliedAmount FROM Accounting.SubsiLedgerNonInv where DocNumber= '" + TransDoc + "' AND (Amount - Applied) !=0", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["TransType"].ToString() + ";";
                    codes += dt["TransDate"].ToString() + ";";
                    codes += dt["DueDate"].ToString() + ";";
                    codes += dt["AccountCode"].ToString() + ";";
                    codes += dt["SubsidiaryCode"].ToString() + ";";
                    codes += dt["ProfitCenterCode"].ToString() + ";";
                    codes += dt["CostCenterCode"].ToString() + ";";
                    codes += dt["TransAmount"].ToString() + ";";
                    codes += dt["TransAppliedAmount"].ToString() + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("checkdoc"))
            {
                //ASPxGridView grid = sender as ASPxGridView;
                //string[] transdoc = TransDoc.Split(';');
                //var selectedValues = transdoc;
                //CriteriaOperator selectionCriteria = new InOperator("DocNumber", transdoc);
                //CriteriaOperator not = new NotOperator(selectionCriteria);
                //sdsApplicationDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not, CriteriaOperator.Parse("TransDate <= '" + dtpDocDate.Text + "'"))).ToString();
                //Session["FilterExpression3"] = sdsApplicationDetail.FilterExpression;
                //grid.DataSourceID = "sdsApplicationDetail";
                //grid.DataBind();

                //for (int i = 0; i < grid.VisibleRowCount; i++)
                //{
                //    if (grid.GetRowValues(i, "TransDocNumber") != null)
                //        if (grid.GetRowValues(i, "TransDocNumber").ToString() == val)
                //        {
                //            grid.Selection.SelectRow(i);
                //            string key = grid.GetRowValues(i, "TransDocNumber").ToString();
                //            grid.MakeRowVisible(key);
                //            break;
                //        }
                //}
            }
        }

        protected void lookup_Init1(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup1 = sender as ASPxGridLookup;
            gridLookup1.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback1);
            if (Session["FilterExpression2"] != null)
            {
                gridLookup1.GridView.DataSourceID = "sdsSubsi";
                sdsAdjustmentDetail.FilterExpression = Session["FilterExpression2"].ToString();
                //Session["FilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column1 = e.Parameters.Split('|')[0];//Set column name
            if (column1.Contains("GLP_AIC") || column1.Contains("GLP_AC") || column1.Contains("GLP_F")) return;//Traps the callback
            string AccCode = e.Parameters.Split('|')[1];
            string Code = e.Parameters.Split('|')[2];
            if (Code.Contains("GLP_AIC") || Code.Contains("GLP_AC") || Code.Contains("GLP_F")) return;//Traps the callback
            var itemlookup1 = sender as ASPxGridView;
            string codes1 = "";

            if (e.Parameters.Contains("accountcode"))
            {
                DataTable getdt = Gears.RetriveData2("select AccountCode,Description AS AccountDescription from Accounting.ChartOfAccount where ISNULL(AllowJV,0) = 1 and AccountCode ='" + AccCode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getdt.Rows.Count == 1)
                {
                    foreach (DataRow dt in getdt.Rows)
                    {
                        codes1 = dt[1].ToString() + ";";
                    }
                }
                itemlookup1.JSProperties["cp_codes1"] = codes1;
            }
            else if (e.Parameters.Contains("subsicode"))
            {
                DataTable getdt = Gears.RetriveData2("select SubsiCode AS SubsidiaryCode,AccountCode,Description AS SubsidiaryDescription from Accounting.GLSubsiCode where SubsiCode ='" + AccCode + "' AND AccountCode = '" + Code + "'", Session["ConnString"].ToString());//ADD CONN
                if (getdt.Rows.Count == 1)
                {
                    foreach (DataRow dt in getdt.Rows)
                    {
                        codes1 = dt[2].ToString() + ";";
                    }
                }
                itemlookup1.JSProperties["cp_codes1"] = codes1;
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[5], "glSubsiCode");
                var selectedValues = AccCode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { AccCode });
                sdsSubsi.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression2"] = sdsSubsi.FilterExpression;
                grid.DataSourceID = "sdsSubsi";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    var col = "SubsidiaryCode";
                    if (grid.GetRowValues(i, col) != null)
                        if (grid.GetRowValues(i, col).ToString() == AccCode)
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
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.ReferenceChecks = String.IsNullOrEmpty(glReferenceChecks.Text) ? null : glReferenceChecks.Text;
            _Entity.SupplierCode = glSupplierCode.Text;
            _Entity.SupplierName = txtSupplierName.Text;
            _Entity.CashAmount = Convert.ToDecimal(Convert.IsDBNull(txtCashAmount.Value) ? 0 : Convert.ToDecimal(txtCashAmount.Value));
            _Entity.TotalAppliedAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalAppliedAmount.Value) ? 0 : Convert.ToDecimal(txtTotalAppliedAmount.Value));
            _Entity.TotalAdjustment = Convert.ToDecimal(Convert.IsDBNull(txtTotalAdjustment.Value) ? 0 : Convert.ToDecimal(txtTotalAdjustment.Value));
            _Entity.Variance = Convert.ToDecimal(Convert.IsDBNull(txtVariance.Value) ? 0 : Convert.ToDecimal(txtVariance.Value));
            _Entity.Remarks = memRemarks.Text;
            //_Entity.HAccountCode = txtHAccountCode.Text;
            //_Entity.HAccountDescription = txtHAccountDescription.Text;
            //_Entity.HSubsidiaryCode = txtHSubsidiaryCode.Text;
            //_Entity.HSubsidiaryDescription = txtHSubsidiaryDescription.Text;
            //_Entity.HProfitCenterCode = txtHProfitCenterCode.Text;
            //_Entity.HCostCenterCode = txtHCostCenterCode.Text;
            //_Entity.HDebitAmount = Convert.ToDecimal(Convert.IsDBNull(txtHDebitAmount.Value) ? 0 : Convert.ToDecimal(txtHDebitAmount.Value));
            //_Entity.HCreditAmount = Convert.ToDecimal(Convert.IsDBNull(txtHCreditAmount.Value) ? 0 : Convert.ToDecimal(txtHCreditAmount.Value));
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
            switch (e.Parameter)
            {
                case "Add":

                    //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method
                    //gv2.UpdateEdit();
                    //string strError = Functions.Submitted(_Entity.DocNumber,"Accounting.CashPaymentVoucher",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                    //    if (!string.IsNullOrEmpty(strError))
                    //    {
                    //        cp.JSProperties["cp_message"] = strError;
                    //        cp.JSProperties["cp_success"] = true;
                    //        cp.JSProperties["cp_forceclose"] = true;
                    //        return;
                    //    }
                    //if (error == false)
                    //{
                    //    check = true;
                    //    _Entity.InsertData(_Entity);//Method of inserting for header
                    //    _Entity.AddedBy = Session["userid"].ToString();
                    //    _Entity.UpdateData(_Entity);

                    //    Gears.RetriveData2("DELETE FROM Accounting.CashPaymentVoucherApplication WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN

                    //    gv1.DataSourceID = "odsDetail";
                    //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    //    gv1.UpdateEdit();//2nd initiation to insert grid

                    //    gv2.DataSourceID = "odsDetail2";
                    //    odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    //    gv2.UpdateEdit();//2nd initiation to insert grid
                    //    Post();
                    //    Validate();

                    //    cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                    //    cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                    //    cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    //    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}
                    //break;

                case "Update":
                    gv1.UpdateEdit();
                    gv2.UpdateEdit();
                    string strError1 = Functions.Submitted(_Entity.DocNumber,"Accounting.CashPaymentVoucher",1,Connection);//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header

                        Gears.RetriveData2("DELETE FROM Accounting.CashPaymentVoucherApplication WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();//2nd Initiation to update grid

                        gv2.DataSourceID = "odsDetail2";
                        odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv2.UpdateEdit();//2nd Initiation to update grid
                        Post();
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
                case "SupplierCode":
                    SqlDataSource ds = sdsSupplierCode;
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name from Masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        glSupplierCode.Text = tran[0][0].ToString();
                        txtSupplierName.Text = tran[0][1].ToString();
                    }
                    glReferenceChecks.Text = "";
                    //sdsApplicationDetail.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocDate) AS VARCHAR(5)),5) AS LineNumber, "
                    //                            + " DocNumber, DocNumber AS TransDocNumber, TransType, CAST(DocDate AS DATE) AS TransDate, "
                    //                            + " CAST(DueDate AS DATE) AS DueDate, AccountCode, SubsiCode AS SubsidiaryCode, "
                    //                            + " ProfitCenter AS ProfitCenterCode, CostCenter AS CostCenterCode, ISNULL(Amount,0) AS TransAmount, "
                    //                            + " ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAppliedAmount, '' AS Field1, '' AS Field2, '' AS Field3, "
                    //                            + " '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
                    //                            + " FROM Accounting.SubsiLedgerNonInv where (ISNULL(Amount,0) - ISNULL(Applied,0)) !=0 "
                    //                            + " AND DocDate <= '" + dtpDocDate.Text + "' AND BizPartnerCode = '" + glSupplierCode.Text + "'";
                    ////glReferenceChecks.DataSourceID = "sdsApplicationDetail";
                    ////glReferenceChecks.DataBind();
                    //sdsApplicationDetail.DataBind();
                    Session["ACTCPVSupplier"] = glSupplierCode.Text;
                    Session["ACTCPVDocDate"] = dtpDocDate.Value.ToString();
                    QueryRefresh();

                    gv2.DataSourceID = null;
                    gv2.DataBind();
                    gv2.DataSource = sdsDetail;
                    break;
                case "Details":
                    cp.JSProperties["cp_refdel"] = true;
                    gv2.DataSourceID = null;
                    gv2.DataBind();
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    break;
                case "RefCheck":
                    glReferenceChecks.Text = "";
                    Session["ACTCPVDocDate"] = dtpDocDate.Value.ToString();
                    Session["ACTCPVSupplier"] = glSupplierCode.Text;
                    GetSelectedVal();
                    QueryRefresh();
                    break;
            }

        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        protected void gv2_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        //object[] keys = { values.Keys["LineNumber"] };
                        object[] keys = { values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["TransDocNumber"] = values.NewValues["TransDocNumber"];
                    row["TransType"] = values.NewValues["TransType"];
                    row["TransDate"] = values.NewValues["TransDate"];
                    row["DueDate"] = values.NewValues["DueDate"];
                    row["AccountCode"] = values.NewValues["AccountCode"];
                    row["SubsidiaryCode"] = values.NewValues["SubsidiaryCode"];
                    row["ProfitCenterCode"] = values.NewValues["ProfitCenterCode"];
                    row["CostCenterCode"] = values.NewValues["CostCenterCode"];
                    row["TransAmount"] = values.NewValues["TransAmount"];
                    row["TransAppliedAmount"] = values.NewValues["TransAppliedAmount"];
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

                    _EntityDetailApplication.TransDocNumber = dtRow["TransDocNumber"].ToString();
                    _EntityDetailApplication.TransType = dtRow["TransType"].ToString();
                    _EntityDetailApplication.TransDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["TransDate"].ToString()) ? null : dtRow["TransDate"]);
                    _EntityDetailApplication.DueDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["DueDate"].ToString()) ? null : dtRow["DueDate"]);
                    _EntityDetailApplication.AccountCode = dtRow["AccountCode"].ToString();
                    _EntityDetailApplication.SubsidiaryCode = dtRow["SubsidiaryCode"].ToString();
                    _EntityDetailApplication.ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                    _EntityDetailApplication.CostCenterCode = dtRow["CostCenterCode"].ToString();
                    _EntityDetailApplication.TransAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TransAmount"]) ? 0 : dtRow["TransAmount"]);
                    _EntityDetailApplication.TransAppliedAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["TransAppliedAmount"]) ? 0 : dtRow["TransAppliedAmount"]);
                    _EntityDetailApplication.Field1 = dtRow["Field1"].ToString();
                    _EntityDetailApplication.Field2 = dtRow["Field2"].ToString();
                    _EntityDetailApplication.Field3 = dtRow["Field3"].ToString();
                    _EntityDetailApplication.Field4 = dtRow["Field4"].ToString();
                    _EntityDetailApplication.Field5 = dtRow["Field5"].ToString();
                    _EntityDetailApplication.Field6 = dtRow["Field6"].ToString();
                    _EntityDetailApplication.Field7 = dtRow["Field7"].ToString();
                    _EntityDetailApplication.Field8 = dtRow["Field8"].ToString();
                    _EntityDetailApplication.Field9 = dtRow["Field9"].ToString();
                    _EntityDetailApplication.AddCashPaymentVoucherApplication(_EntityDetailApplication);
                }
            }
        }

        private DataTable GetSelectedVal()
        {
            string linenummmm = "";
            Session["Datatable"] = "0";
            gv2.DataSource = null;
            if (gv2.DataSourceID != "")
            {
                gv2.DataSourceID = null;
            }
            gv2.DataBind();
            DataTable dt = new DataTable();
            string[] selectedtransValues = glReferenceChecks.Text.Split(';');
            foreach (string x in selectedtransValues)
            {
                linenummmm += "'" + x + "',";
            }
            sdsApplicationDetail.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocDate) AS VARCHAR(5)),5) AS LineNumber, "
                                                +" DocNumber, DocNumber AS TransDocNumber, TransType, CAST(DocDate AS DATE) AS TransDate, "
                                                +" CAST(DueDate AS DATE) AS DueDate, AccountCode, SubsiCode AS SubsidiaryCode, "
                                                +" ProfitCenter AS ProfitCenterCode, CostCenter AS CostCenterCode, ISNULL(Amount,0) AS TransAmount, "
                                                +" ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAppliedAmount, '' AS Field1, '' AS Field2, '' AS Field3, "
                                                +" '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 INTO #TEMP"
                                                +" FROM Accounting.SubsiLedgerNonInv where (ISNULL(Amount,0) - ISNULL(Applied,0)) !=0 "
                                                + " AND DocDate <= '" + dtpDocDate.Text + "' AND BizPartnerCode = '" + glSupplierCode.Text + "' SELECT * FROM #TEMP WHERE LineNumber IN (" + linenummmm + "'') DROP TABLE #TEMP";
            gv2.DataSource = sdsApplicationDetail;
            gv2.DataBind();
            Session["Datatable"] = "1";

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

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumber"]};
            return dt;
        }

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["detail"] = null;
            }

            if (Session["detail"] != null)
            {
                gv1.DataSource = sdsApplicationDetail;
            }
        }
        #endregion

        public void QueryRefresh()
        {
            //sdsApplicationDetail.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocDate) AS VARCHAR(5)),5) AS LineNumber, "
            //                                    +" DocNumber, DocNumber AS TransDocNumber, TransType, CAST(DocDate AS DATE) AS TransDate, "
            //                                    +" CAST(DueDate AS DATE) AS DueDate, AccountCode, SubsiCode AS SubsidiaryCode, "
            //                                    +" ProfitCenter AS ProfitCenterCode, CostCenter AS CostCenterCode, ISNULL(Amount,0) AS TransAmount, "
            //                                    +" ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAppliedAmount, '' AS Field1, '' AS Field2, '' AS Field3, "
            //                                    +" '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9  "
            //                                    +" FROM Accounting.SubsiLedgerNonInv where (ISNULL(Amount,0) - ISNULL(Applied,0)) !=0 "
            //                                    +" AND DocDate <= '"+dtpDocDate.Text+"'";
            //glReferenceChecks.DataSourceID = sdsApplicationDetail.ID;
            //glReferenceChecks.DataBind();

            string dateeeee = "";
            string supplierrrr = "";
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                sdsApplicationDetail.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocDate) AS VARCHAR(5)),5) AS LineNumber, DocNumber, DocNumber AS TransDocNumber, TransType, CAST(DocDate AS DATE) AS TransDate, CAST(DueDate AS DATE) AS DueDate, AccountCode, SubsiCode AS SubsidiaryCode, ProfitCenter AS ProfitCenterCode, CostCenter AS CostCenterCode, ISNULL(Amount,0) AS TransAmount, ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAppliedAmount, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 FROM Accounting.SubsiLedgerNonInv where (ISNULL(Amount,0) - ISNULL(Applied,0)) !=0 AND ISNULL(CounterDocNumber,'') = ''";
            }
            else
            {
                if (Session["ACTCPVDocDate"] != null && Session["ACTCPVSupplier"] != null)
                {
                    dateeeee = Session["ACTCPVDocDate"].ToString();
                    supplierrrr = Session["ACTCPVSupplier"].ToString();
                }

                sdsApplicationDetail.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocDate) AS VARCHAR(5)),5) AS LineNumber, "
                                                    + " DocNumber, DocNumber AS TransDocNumber, TransType, CAST(DocDate AS DATE) AS TransDate, "
                                                    + " CAST(DueDate AS DATE) AS DueDate, AccountCode, SubsiCode AS SubsidiaryCode, "
                                                    + " ProfitCenter AS ProfitCenterCode, CostCenter AS CostCenterCode, ISNULL(Amount,0) AS TransAmount, "
                                                    + " ISNULL(Amount,0) - ISNULL(Applied,0) AS TransAppliedAmount, '' AS Field1, '' AS Field2, '' AS Field3, "
                                                    + " '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
                                                    + " FROM Accounting.SubsiLedgerNonInv where (ISNULL(Amount,0) - ISNULL(Applied,0)) !=0 "
                                                    + " AND DocDate <= '" + dateeeee + "' AND BizPartnerCode = '" + supplierrrr +"'";
            }
            sdsApplicationDetail.DataBind();
        }

        protected void ConnectionInit_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString(); 
            sdsDetail2.ConnectionString = Session["ConnString"].ToString();
            sdsAdjustmentDetail.ConnectionString = Session["ConnString"].ToString();
            sdsApplicationDetail.ConnectionString = Session["ConnString"].ToString();
            sdsCOA.ConnectionString = Session["ConnString"].ToString();
            sdsSubsi.ConnectionString = Session["ConnString"].ToString();
            sdsProfitCenterCode.ConnectionString = Session["ConnString"].ToString();
            sdsCostCenterCode.ConnectionString = Session["ConnString"].ToString();
            sdsSupplierCode.ConnectionString = Session["ConnString"].ToString();
        }

        protected void glReferenceChecks_Init(object sender, EventArgs e)
        {
            QueryRefresh();
        }
    }
}