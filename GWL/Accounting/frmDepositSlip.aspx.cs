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
    public partial class frmDepositSlip : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.DepositSlip _Entity = new DepositSlip();//Calls entity header
        Entity.DepositSlip.DepositSlipCash _EntityCashDetail = new DepositSlip.DepositSlipCash();//Call entity detail cash
        Entity.DepositSlip.DepositSlipCheck _EntityCheckDetail = new DepositSlip.DepositSlipCheck();//Call entity detail check
        string val = "";
        private static string Connection;

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
            gv2.KeyFieldName = "DocNumber;LineNumber";
			
            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;
            }

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()] = "";
                Session["ACTDESFilterExpression" + Request.QueryString["docnumber"].ToString()] = null;
                Session["ACTDESFilterExpression2" + Request.QueryString["docnumber"].ToString()] = null;
                Session["ACTDESDatatable" + Request.QueryString["docnumber"].ToString()] = null;
                Session["ACTDESCashDate"] = null;

                txtDocNumber.ReadOnly = true;
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

                dtpDocDate.Text = DateTime.Now.ToShortDateString();
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                Session["ACTDESCashDate"] = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now.ToShortDateString() : _Entity.DocDate;
                sdsCashDetail.DataBind();
                glBankAccountCode.Value = _Entity.BankAccountCode.ToString();
                txtTotalCashAmount.Value = _Entity.TotalCashAmount;
                txtTotalCheckAmount.Value = _Entity.TotalCheckAmount;
                Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()] = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                QueryRefresh();
                glReferenceChecks.Text = _Entity.ReferenceChecks;
                txtTotalAmount.Value = _Entity.TotalAmount;
                txtHAddedBy.Value = _Entity.AddedBy;
                txtHAddedDate.Value = _Entity.AddedDate;
                txtHLastEditedBy.Value = _Entity.LastEditedBy;
                txtHLastEditedDate.Value = _Entity.LastEditedDate;
                txtHSubmittedBy.Value = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = _Entity.SubmittedDate;
                txtHCancelledBy.Value = _Entity.CancelledBy;
                txtHCancelledDate.Value = _Entity.CancelledDate;
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
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.DepositSlipCash WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                DataTable dtbldetail1 = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Accounting.DepositSlipCheck WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN

                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                gv2.DataSourceID = (dtbldetail1.Rows.Count > 0 ? "odsDetail2" : "sdsDetail2");

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
            gparam._TransType = "ACTDES";
            string strresult = GearsAccounting.GAccounting.DepositSlip_Validate(gparam);
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
            gparam._TransType = "ACTDES";
            gparam._Table = "Accounting.DepositSlip";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.DepositSlip_Post(gparam);
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
                look.DropDownButton.Enabled = !view;
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
            date.DropDownButton.Enabled = !view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                e.ButtonType == ColumnCommandButtonType.Update)
            {
                if (Request.QueryString["entry"] == "D" || Request.QueryString["entry"] == "V")
                {
                    e.Visible = false;
                }
                else
                {
                    e.Visible = true;
                }
            }
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "D" || Request.QueryString["entry"] == "V")
            {
                if (e.ButtonID == "Delete" || e.ButtonID == "Delete1")
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
            if (Session["ACTDESFilterExpression" + Request.QueryString["docnumber"].ToString()] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsCheckDetail";
                sdsCheckDetail.FilterExpression = Session["ACTDESFilterExpression" + Request.QueryString["docnumber"].ToString()].ToString();
                //Session["ACTDESFilterExpression" + Request.QueryString["docnumber"].ToString()] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string RefDocNumber = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";
            string doc = "";
            string check = "";
            int count = 0;

            if (e.Parameters.Contains("RefDocNumber"))
            {
                string docc = RefDocNumber.Split(',')[0];
                string checkk = RefDocNumber.Split(',')[1];
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT Bank AS BankCode, Branch AS BankBranch, CONVERT(varchar(20),CheckDate,101) AS CheckDate, CheckAmount, CheckNumber FROM Accounting.CollectionChecks where DocNumber= '" + docc + "' AND CheckNumber = '" + checkk + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["BankCode"].ToString() + ";";
                    codes += dt["BankBranch"].ToString() + ";";
                    codes += dt["CheckDate"].ToString() + ";";
                    codes += dt["CheckAmount"].ToString() + ";";
                    codes += dt["CheckNumber"].ToString() + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
                itemlookup.JSProperties["cp_refdoc"] = true;
            }
            else if (e.Parameters.Contains("checkdoc"))
            {
                //string start = " (A.DocNumber != '";
                //string middle = "' OR CheckNumber != '";
                //string concatquery = "";
                //string initquery = "";
                //int counterr = 0;
                //ASPxGridView grid = sender as ASPxGridView;
                //string[] refdocnumber = RefDocNumber.Split(';');
                //foreach (string x in refdocnumber)
                //{
                //    if (counterr != 0 && x != "")
                //    {
                //        concatquery += start + x.Split(',')[0] + middle;
                //        concatquery += x.Split(',')[1] + "')";
                //        if (counterr != refdocnumber.Length - 2)
                //            concatquery += " AND ";
                //    }
                //    counterr++;
                //}
                //if (concatquery != "")
                //    initquery = " AND " + concatquery;
                //string finalquery = "SELECT A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 FROM Accounting.CollectionChecks A INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber WHERE ISNULL(IsForDeposit,0) = 1 AND A.DepositedDate IS NULL " + initquery + " AND CheckDate <= '" + dtpDocDate.Text + "';";
                ////string[] docc = refdocnumber.Where((str, ix) => ix % 2 == 0).ToArray();
                ////string[] bank = refdocnumber.Where((str, ix) => ix % 2 == 1).ToArray();
                ////string[] checkk = refdocnumber.Where((str, ix) => ix % 2 == 2).ToArray();

                ////CriteriaOperator selectionCriteria = new InOperator("DocNumber", docc);
                ////CriteriaOperator not = new NotOperator(selectionCriteria);
                ////CriteriaOperator selectionCriteria1 = new InOperator("CheckNumber", checkk);
                ////CriteriaOperator not1 = new NotOperator(selectionCriteria1);
                ////sdsCheckDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not, CriteriaOperator.Parse("CheckDate <= '" + dtpDocDate.Text + "'"))).ToString();


                ////Session["ACTDESFilterExpression" + Request.QueryString["docnumber"].ToString()] = sdsCheckDetail.FilterExpression;
                //sdsCheckDetail.SelectCommand = finalquery;
                //grid.DataSourceID = "sdsCheckDetail";
                //grid.DataBind();

                //for (int i = 0; i < grid.VisibleRowCount; i++)
                //{
                //    if (grid.GetRowValues(i, "RefDocNumber") != null)
                //        if (grid.GetRowValues(i, "RefDocNumber").ToString() == val)
                //        {
                //            grid.Selection.SelectRow(i);
                //            string key = grid.GetRowValues(i, "RefDocNumber").ToString();
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
            if (Session["ACTDESFilterExpression2" + Request.QueryString["docnumber"].ToString()] != null)
            {
                gridLookup1.GridView.DataSourceID = "sdsCashDetail";
                sdsCashDetail.FilterExpression = Session["ACTDESFilterExpression2" + Request.QueryString["docnumber"].ToString()].ToString();
                //Session["ACTDESFilterExpression" + Request.QueryString["docnumber"].ToString()] = null;
            }
        }
        public void gridView_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column1 = e.Parameters.Split('|')[0];//Set column name
            if (column1.Contains("GLP_AIC") || column1.Contains("GLP_AC") || column1.Contains("GLP_F")) return;//Traps the callback
            string RefDocNum = e.Parameters.Split('|')[1];//Set Item Code
            string val1 = e.Parameters.Split('|')[2];//Set column value
            if (val1.Contains("GLP_AIC") || val1.Contains("GLP_AC") || val1.Contains("GLP_F")) return;//Traps the callback
            var itemlookup1 = sender as ASPxGridView;
            string codes1 = "";

            if (e.Parameters.Contains("RefDocNum"))
            {
                DataTable countcolor1 = Gears.RetriveData2("SELECT DISTINCT A.CashAmount AS Amount, CASE WHEN ISNULL(A.CustomerCode,'') != '' THEN A.CustomerCode ELSE A.BizAccount END AS CustomerCode, RTRIM(LTRIM(B.Name)) AS CustomerName FROM Accounting.Collection  A "
                    + " LEFT JOIN Masterfile.BPCustomerInfo B ON CASE WHEN ISNULL(A.CustomerCode,'') != '' THEN A.CustomerCode ELSE A.BizAccount END = B.BizPartnerCode WHERE A.DocNumber = '" + RefDocNum + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor1.Rows)
                {
                    codes1 = dt["Amount"].ToString() + ";";
                    codes1 += dt["CustomerCode"].ToString() + ";";
                    codes1 += dt["CustomerName"].ToString() + ";";
                    //codes += dt["CheckDate"].ToString() + ";";
                    //codes += dt["CheckAmount"].ToString() + ";";
                }
                itemlookup1.JSProperties["cp_codes1"] = codes1;
            }
            else if (e.Parameters.Contains("cashdoc"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                //string ddate = dtpDocDate.Text + ";";
                //string[] docdate = ddate.Split(';');
                string[] refdocnum = RefDocNum.Split(';');
                var selectedValues = refdocnum;
                CriteriaOperator selectionCriteria = new InOperator("DocNumber", refdocnum);
                CriteriaOperator not = new NotOperator(selectionCriteria);
                //sdsCashDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not, CriteriaOperator.Parse("CheckDate <= "+docdate))).ToString();
                sdsCashDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not)).ToString();
                Session["ACTDESFilterExpression2" + Request.QueryString["docnumber"].ToString()] = sdsCashDetail.FilterExpression;
                grid.DataSourceID = "sdsCashDetail";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "RefDocNum") != null)
                        if (grid.GetRowValues(i, "RefDocNum").ToString() == val1)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "RefDocNum").ToString();
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
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.BankAccountCode = glBankAccountCode.Text;
            _Entity.ReferenceChecks = String.IsNullOrEmpty(glReferenceChecks.Text) ? null : glReferenceChecks.Text;
            _Entity.TotalCashAmount = String.IsNullOrEmpty(txtTotalCashAmount.Text) ? 0 : Convert.ToDecimal(txtTotalCashAmount.Text);
            _Entity.TotalCheckAmount = String.IsNullOrEmpty(txtTotalCheckAmount.Text) ? 0 : Convert.ToDecimal(txtTotalCheckAmount.Text);
            _Entity.TotalAmount = String.IsNullOrEmpty(txtTotalAmount.Text) ? 0 : Convert.ToDecimal(txtTotalAmount.Text);
            _Entity.LastEditedBy = Session["userid"].ToString();
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
                    gv2.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.DepositSlip", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        } 
                        
                        if (Session["ACTDESDatatable" + Request.QueryString["docnumber"].ToString()] == "1")
                        {
                            Gears.RetriveData2("DELETE FROM Accounting.DepositSlipCheck WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            gv2.UpdateEdit();
                        }
                        else
                        {
                            gv2.DataSourceID = "odsDetail2";
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv2.UpdateEdit();
                        }

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

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
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                
                    //gv2.UpdateEdit();
                    //if (error == false)
                    //{
                    //    check = true;
                    //    _Entity.LastEditedBy = Session["userid"].ToString();
                    //    _Entity.UpdateData(_Entity);//Method of Updating header

                    //    string strError1 = Functions.Submitted(_Entity.DocNumber, "Accounting.DepositSlip", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
                    //    if (!string.IsNullOrEmpty(strError1))
                    //    {
                    //        cp.JSProperties["cp_message"] = strError1;
                    //        cp.JSProperties["cp_success"] = true;
                    //        cp.JSProperties["cp_forceclose"] = true;
                    //        return;
                    //    }
                    //    if (Session["ACTDESDatatable" + Request.QueryString["docnumber"].ToString()] == "1")
                    //    {
                    //        Gears.RetriveData2("DELETE FROM Accounting.DepositSlipCheck WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    //        gv2.UpdateEdit();
                    //    }
                    //    else
                    //    {
                    //        gv2.DataSourceID = "odsDetail2";
                    //        odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    //        gv2.UpdateEdit();//2nd Initiation to update grid
                    //    }
                    //    gv1.DataSourceID = "odsDetail";
                    //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    //    gv1.UpdateEdit();//2nd Initiation to update grid
                    //    Post();
                    //    Validate();

                    //    cp.JSProperties["cp_message"] = "Successfully Updated!";
                    //    cp.JSProperties["cp_success"] = true;
                    //    cp.JSProperties["cp_close"] = true;
                    //    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}
                    //break;
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
                case "Details":
                    cp.JSProperties["cp_refdel"] = true;
                    gv2.DataSourceID = null;
                    gv2.DataBind();
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    break;
                case "RefCheck":
                    glReferenceChecks.Text = "";
                    Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()] = dtpDocDate.Value.ToString();
                    Session["ACTDESCashDate"] = dtpDocDate.Value.ToString();
                    sdsCashDetail.DataBind();
                    GetSelectedVal();
                    QueryRefresh();
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
                //e.DeleteValues.Clear();
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

            if (Session["ACTDESDatatable" + Request.QueryString["docnumber"].ToString()] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        //object[] keys = { values.Keys["LineNumber"] };
                        object[] keys = { values.Keys["LineNumber"]};
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"]};
                    DataRow row = source.Rows.Find(keys);
                    row["RefDocNumber"] = values.NewValues["RefDocNumber"];
                    row["BankCode"] = values.NewValues["BankCode"];
                    row["CheckNumber"] = values.NewValues["CheckNumber"];
                    row["BankBranch"] = values.NewValues["BankBranch"];
                    row["CheckDate"] = values.NewValues["CheckDate"];
                    row["CheckAmount"] = values.NewValues["CheckAmount"];
                    row["BPCode"] = values.NewValues["BPCode"];
                    row["BPName"] = values.NewValues["BPName"];
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

                    _EntityCheckDetail.RefDocNumber = dtRow["RefDocNumber"].ToString();
                    _EntityCheckDetail.BankCode = dtRow["BankCode"].ToString();
                    _EntityCheckDetail.CheckNumber = dtRow["CheckNumber"].ToString();
                    _EntityCheckDetail.BankBranch = dtRow["BankBranch"].ToString();
                    _EntityCheckDetail.CheckDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["CheckDate"].ToString()) ? null : dtRow["CheckDate"]);
                    _EntityCheckDetail.CheckAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CheckAmount"]) ? 0 : dtRow["CheckAmount"]);
                    _EntityCheckDetail.BPCode = dtRow["BPCode"].ToString();
                    _EntityCheckDetail.BPName = dtRow["BPName"].ToString();
                    _EntityCheckDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityCheckDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityCheckDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityCheckDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityCheckDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityCheckDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityCheckDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityCheckDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityCheckDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityCheckDetail.AddDepositSlipCheck(_EntityCheckDetail);
                }
            }
        }

        private DataTable GetSelectedVal()
        {
            string linenummmm = "";
            Session["ACTDESDatatable" + Request.QueryString["docnumber"].ToString()] = "0";
            gv2.DataSource = null;
            if (gv2.DataSourceID != "")
            {
                gv2.DataSourceID = null;
            }
            gv2.DataBind();
            DataTable dt = new DataTable();
            string[] selectedtransValues = glReferenceChecks.Text.Split(';');
            foreach(string x in selectedtransValues)
            {
                linenummmm += "'" + x + "',";
            }
            //sdsCheckDetail.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY CheckDate) AS VARCHAR(5)),5) AS LineNumber, "
            //                             + " A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, "
            //                             + " '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, "
            //                             + " '' AS Field8, '' AS Field9 INTO #TEMP FROM Accounting.CollectionChecks A INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber "
            //                             + " WHERE (ISNULL(IsForDeposit,0) = 1 AND A.DepositedDate IS NULL) AND A.CheckDate <= '" +dtpDocDate.Text+ "' SELECT * FROM #TEMP WHERE CheckNumber IN (" + linenummmm + "'@@@@@@') DROP TABLE #TEMP";

            //sdsCheckDetail.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY CheckDate) AS VARCHAR(5)),5) AS LineNumber, *, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 INTO #TEMP"
            //                             + " FROM (SELECT A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecID AS RecordID  FROM Accounting.CollectionChecks A INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber WHERE (ISNULL(IsForDeposit,0) = 1 AND A.DepositedDate IS NULL AND ISNULL(CancelledCheckDocNum,'') = '') AND A.CheckDate <= '" + dtpDocDate.Text + "' "
            //                             + " UNION ALL SELECT A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecordID  FROM Accounting.CheckVoucherDetail A WHERE A.DocNumber IN (SELECT DocNumber FROM Accounting.CheckVoucher WHERE TransType = 'Fund Transfer' AND ISNULL(SubmittedBy,'') != '') AND ClearedDate IS NULL AND ReleasedDate IS NULL AND ISNULL(CancelledCheckDocNum,'') = '') A "
            //                             + " SELECT * FROM #TEMP WHERE CheckNumber IN (" + linenummmm + "'@@@@@@') DROP TABLE #TEMP";

            sdsCheckDetail.SelectCommand = 
                "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY CheckDate) AS VARCHAR(5)),5) AS LineNumber,"
              + "       *, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
              + "  INTO #TEMP"
              + "  FROM ("
              + "       SELECT A.DocNumber, A.DocNumber AS RefDocNumber, A.Bank AS BankCode, A.CheckNumber,"
              //+ "              Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecID AS RecordID"
              + "              A.Branch AS BankBranch, CAST(A.CheckDate AS DATE) AS CheckDate, A.CheckAmount, CONVERT(varchar(MAX),A.RecID) + 'C' AS RecordID , CASE WHEN ISNULL(B.BizAccount,'') = '' THEN B.CustomerCode ELSE B.BizAccount END AS BPCode, RTRIM(LTRIM(LEFT(C.Name,20))) AS BPName "
              + "         FROM Accounting.CollectionChecks A "
              + "              INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber "
              + "              LEFT JOIN Masterfile.BPCustomerInfo C ON (CASE WHEN ISNULL(B.BizAccount,'') = '' THEN B.CustomerCode ELSE B.BizAccount END) = C.BizPartnerCode "
              + "        WHERE (B.ReceiptType = 'C' OR ISNULL(A.CollReceiptNum,'') != '') AND "
              + "              ISNULL(B.SubmittedBy,'') != '' AND "
              + "              A.DepositedDate IS NULL AND ISNULL(A.CancelledCheckDocNum,'') = '' AND A.CheckDate <= '" + dtpDocDate.Text + "' "
              + "       UNION ALL "
              + "       SELECT A.DocNumber, A.DocNumber AS RefDocNumber, A.Bank AS BankCode, A.CheckNumber, "
                //+ "              Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecordID"
              + "              A.Branch AS BankBranch, CAST(A.CheckDate AS DATE) AS CheckDate, A.CheckAmount, CONVERT(varchar(MAX),A.RecordID) + 'F', ISNULL(B.SupplierCode,'') AS BPCode, RTRIM(LTRIM(LEFT(C.Name,20))) AS BPName "
              + "         FROM Accounting.CheckVoucherDetail A "
              + "              INNER JOIN Accounting.CheckVoucher B ON A.DocNumber = B.DocNumber"
              + "              LEFT JOIN Masterfile.BizPartner C ON B.SupplierCode = C.BizPartnerCode"
                //    02-23-2017  TL  Bug Fix 
                //+ "        WHERE TransType = 'Fund Transfer' AND ISNULL(SubmittedBy,'') != '') AND "
              + "        WHERE B.TransType = 'Fund Transfer' AND ISNULL(B.SubmittedBy,'') != '' AND "
              + "              A.ReleasedDate IS NULL AND ISNULL(A.CancelledCheckDocNum,'') = '' "
              + "       ) A "
              + " SELECT * FROM #TEMP WHERE CheckNumber IN (" + linenummmm + "'@@@@@@') DROP TABLE #TEMP";
            gv2.DataSource = sdsCheckDetail;
            gv2.DataBind();
            Session["ACTDESDatatable" + Request.QueryString["docnumber"].ToString()] = "1";

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
                gv1.DataSource = sdsCheckDetail;
            }
        }
        #endregion

        protected void ConnectionInit_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        public void QueryRefresh()
        {
            string dateeeee = "";
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                sdsCheckDetail.SelectCommand =
                    "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY CheckDate) AS VARCHAR(5)),5) AS LineNumber,"
                  + "       *, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
                  + "  FROM ("
                  + "       SELECT A.DocNumber, A.DocNumber AS RefDocNumber, A.Bank AS BankCode, A.CheckNumber, "
                  //+ "              Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecID AS RecordID "
                  + "              A.Branch AS BankBranch, CAST(A.CheckDate AS DATE) AS CheckDate, A.CheckAmount, CONVERT(varchar(MAX),A.RecID) + 'C' AS RecordID , CASE WHEN ISNULL(B.BizAccount,'') = '' THEN B.CustomerCode ELSE B.BizAccount END AS BPCode, RTRIM(LTRIM(LEFT(C.Name,20))) AS BPName "
                  + "         FROM Accounting.CollectionChecks A "
                  + "              INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber "
                  + "              LEFT JOIN Masterfile.BPCustomerInfo C ON (CASE WHEN ISNULL(B.BizAccount,'') = '' THEN B.CustomerCode ELSE B.BizAccount END) = C.BizPartnerCode "
                  + "        WHERE (B.ReceiptType = 'C' OR ISNULL(A.CollReceiptNum,'') != '') AND "
                  + "              ISNULL(B.SubmittedBy,'') != '' AND "
                  + "              A.DepositedDate IS NULL AND ISNULL(A.CancelledCheckDocNum,'') = ''"
                  + "       UNION ALL "
                  + "       SELECT A.DocNumber, A.DocNumber AS RefDocNumber, A.Bank AS BankCode, A.CheckNumber, "
                    //+ "              Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecordID "
                    + "              A.Branch AS BankBranch, CAST(A.CheckDate AS DATE) AS CheckDate, A.CheckAmount, CONVERT(varchar(MAX),A.RecordID) + 'F', ISNULL(B.SupplierCode,'') AS BPCode, RTRIM(LTRIM(LEFT(C.Name,20))) AS BPName "
                  + "         FROM Accounting.CheckVoucherDetail A "
                  + "              INNER JOIN Accounting.CheckVoucher B ON A.DocNumber = B.DocNumber"
                  + "              LEFT JOIN Masterfile.BizPartner C ON B.SupplierCode = C.BizPartnerCode"
                  + "        WHERE B.TransType = 'Fund Transfer' AND ISNULL(B.SubmittedBy,'') != '' AND "
                  + "              A.ReleasedDate IS NULL AND ISNULL(A.CancelledCheckDocNum,'') = '' "
                  + "       ) A ";
            }
            else
            {
                if (Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()] != null)
                {
                    dateeeee = Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()].ToString();
                }

                sdsCheckDetail.SelectCommand =
                    " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY CheckDate) AS VARCHAR(5)),5) AS LineNumber,"
                  + "        *, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
                  + "   FROM ("
                  + "        SELECT A.DocNumber, A.DocNumber AS RefDocNumber, A.Bank AS BankCode, A.CheckNumber, "
                  //+ "               Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecID AS RecordID "
                  + "              A.Branch AS BankBranch, CAST(A.CheckDate AS DATE) AS CheckDate, A.CheckAmount, CONVERT(varchar(MAX),A.RecID) + 'C' AS RecordID , CASE WHEN ISNULL(B.BizAccount,'') = '' THEN B.CustomerCode ELSE B.BizAccount END AS BPCode, RTRIM(LTRIM(LEFT(C.Name,20))) AS BPName "
                  + "          FROM Accounting.CollectionChecks A "
                  + "               INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber "
                  + "               LEFT JOIN Masterfile.BPCustomerInfo C ON (CASE WHEN ISNULL(B.BizAccount,'') = '' THEN B.CustomerCode ELSE B.BizAccount END) = C.BizPartnerCode "
                  + "         WHERE (B.ReceiptType = 'C' OR ISNULL(A.CollReceiptNum,'') != '') AND "
                  + "               ISNULL(B.SubmittedBy,'') != '' AND "
                  + "               A.DepositedDate IS NULL AND ISNULL(A.CancelledCheckDocNum,'') = '' AND "
                    //    02-23-2017  GC  Changed ClearedDate to A.DateCleared
                  //+ "               ClearedDate IS NULL AND A.CheckDate <= '" + dateeeee + "' "
                  + "               A.DateCleared IS NULL AND A.CheckDate <= '" + dateeeee + "' "
                  + "        UNION ALL "
                  + "        SELECT A.DocNumber, A.DocNumber AS RefDocNumber, A.Bank AS BankCode, A.CheckNumber, "
                    //+ "               Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount, RecordID "
                    + "              A.Branch AS BankBranch, CAST(A.CheckDate AS DATE) AS CheckDate, A.CheckAmount, CONVERT(varchar(MAX),RecordID) + 'F', ISNULL(B.SupplierCode,'') AS BPCode, RTRIM(LTRIM(LEFT(C.Name,20))) AS BPName "
                  + "          FROM Accounting.CheckVoucherDetail A "
                  + "               INNER JOIN Accounting.CheckVoucher B ON A.DocNumber = B.DocNumber"
                  + "               LEFT JOIN Masterfile.BizPartner C ON B.SupplierCode = C.BizPartnerCode"
                  + "         WHERE B.TransType = 'Fund Transfer' AND ISNULL(B.SubmittedBy,'') != '' AND "
                  + "               A.ReleasedDate IS NULL AND ISNULL(A.CancelledCheckDocNum,'') = '' AND "
                  //    02-23-2017  GC  Changed ClearedDate to A.ClearedDate
                  //+ "               ClearedDate IS NULL AND A.CheckDate <= '" + dateeeee + "' "
                  // 2017-07-07   TL  Should no longer check ClearedDate
                  // + "               A.ClearedDate IS NULL AND A.CheckDate <= '" + dateeeee + "' "
                  + "               A.CheckDate <= '" + dateeeee + "' "
                  // 2017-07-07   TL  (End)
                  + "        ) A";
            }
            sdsCheckDetail.DataBind();
            //glReferenceChecks.DataBind();
        }

        protected void glReferenceChecks_Init(object sender, EventArgs e)
        {
            QueryRefresh();
        }

        //protected void glReferenceChecks_Init(object sender, EventArgs e)
        //{
        //    if (Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()] == null)
        //    {
        //        sdsCheckDetail.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY CheckDate) AS VARCHAR(5)),5) AS LineNumber, *, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
        //              + " FROM (SELECT A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount FROM Accounting.CollectionChecks A INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber WHERE ISNULL(IsForDeposit,0) = 1 AND A.DepositedDate IS NULL AND ISNULL(CancelledCheckDocNum,'') = '' UNION ALL "
        //              + " SELECT A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount FROM Accounting.CheckVoucherDetail A WHERE A.DocNumber IN (SELECT DocNumber FROM Accounting.CheckVoucher WHERE TransType = 'Fund Transfer' AND ISNULL(SubmittedBy,'') != '') AND ClearedDate IS NULL AND ReleasedDate IS NULL AND ISNULL(CancelledCheckDocNum,'') = '') A";
        //        glReferenceChecks.DataBind();
        //    }
        //    else
        //    {
        //        sdsCheckDetail.SelectCommand =
        //        " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY CheckDate) AS VARCHAR(5)),5) AS LineNumber, *, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, '' AS Field9 "
        //      + " FROM (SELECT A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount FROM Accounting.CollectionChecks A INNER JOIN Accounting.Collection B ON A.DocNumber = B.DocNumber WHERE ISNULL(IsForDeposit,0) = 1 AND A.DepositedDate IS NULL AND ISNULL(CancelledCheckDocNum,'') = '' AND A.CheckDate <= '" + Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()].ToString() + "' UNION ALL "
        //      + " SELECT A.DocNumber, A.DocNumber AS RefDocNumber, Bank AS BankCode, CheckNumber, Branch AS BankBranch, CAST(CheckDate AS DATE) AS CheckDate, CheckAmount FROM Accounting.CheckVoucherDetail A WHERE A.DocNumber IN (SELECT DocNumber FROM Accounting.CheckVoucher WHERE TransType = 'Fund Transfer' AND ISNULL(SubmittedBy,'') != '') AND ClearedDate IS NULL AND ReleasedDate IS NULL AND ISNULL(CancelledCheckDocNum,'') = ''  AND A.CheckDate <= '" + Session["ACTDESCheckNumbers" + Request.QueryString["docnumber"].ToString()].ToString() + "') A ";
        //        glReferenceChecks.DataBind();
        //    }
        //}
    }
}