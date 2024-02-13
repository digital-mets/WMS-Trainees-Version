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
using DevExpress.XtraGrid;


namespace GWL
{
    public partial class frmExpenseProcessing : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.ExpenseProcessing _Entity = new ExpenseProcessing();//Calls entity PO
        Entity.ExpenseProcessing.ExpenseProcessingDetail _EntityDetail = new ExpenseProcessing.ExpenseProcessingDetail();//Call entity POdetails

        #region page load/entry
        protected void Page_PreInit(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            foreach (Control c in form2.Controls)
            {
                if (c is SqlDataSource)
                {
                    ((SqlDataSource)c).ConnectionString = Session["ConnString"].ToString();
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

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            txtDocNumber.ReadOnly = true;
            this.gvRef.Columns["CommandString"].Width = 0;
            this.gvRef.Columns["RCommandString"].Width = 0;
            
            DocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["ExpProDatatable"] = null;
                Session["ExpProExpression"] = null;
                Session["ExpProTransNo"] = null;
                Session["ExpProSupplier"] = null;
                Session["ExpProName"] = null;
                Session["ExpProWithPO"] = null;
                Session["ExpProDocDate"] = null;

                Connection = Session["ConnString"].ToString();

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                    DocDate.Date = DateTime.Now;
                    DueDate.Date = DateTime.Now;
                }

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                DocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                aglPayables.Value = _Entity.PayableTo.ToString();
                txtName.Text = _Entity.PayableName.ToString();
                TotalGross.Value = _Entity.TotalGrossVatable;
                TotalGrossNon.Text = _Entity.TotalGrossNonVatable.ToString();
                TotalVat.Value = _Entity.TotalVatAmount;
                TotalWithholding.Text = _Entity.TotalWitholdingTax.ToString();
                TotalAmount.Text = _Entity.TotalAmountDue.ToString();
                //DueDate.Text = String.IsNullOrEmpty(_Entity.DueDate) ? "" : Convert.ToDateTime(_Entity.DueDate).ToShortDateString();
				DueDate.Text = String.IsNullOrEmpty(_Entity.DueDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DueDate).ToShortDateString();
                Terms.Text = _Entity.Terms.ToString();
                txtAPVoucher.Text = _Entity.APVNumber.ToString();
                chkPORef.Value = _Entity.IsWithPO;
                aglPORef.Text = _Entity.RefTrans.ToString();
                memRemarks.Value = _Entity.HRemarks;

                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString();

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHApprovedBy.Text = _Entity.ApprovedBy;
                txtHApprovedDate.Text = _Entity.ApprovedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;

                Session["ExpProSupplier"] = _Entity.PayableTo.ToString();
                Session["ExpProName"] = _Entity.PayableName.ToString();
                Session["ExpProDocDate"] = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }

                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    gvRef.DataSourceID = "odsReference";
                }
            }

            DataTable checkCount = new DataTable();

            checkCount = Gears.RetriveData2("SELECT TOP 1 DocNumber FROM Accounting.ExpenseProcessingDetail WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            gv1.DataSourceID = (checkCount.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

            gvJournal.DataSourceID = "odsJournalEntry";

            Filter();
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

            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (chkPORef.Checked == true)
                {
                    aglPORef.ReadOnly = false;
                }
                else
                {
                    aglPORef.ReadOnly = true;
                }
            }
        }
        protected void LookupLoad_PORef(object sender, EventArgs e)//Control for all lookup in header
        {
            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (chkPORef.Checked == true)
                {
                    aglPORef.ReadOnly = false;
                    aglPORef.DropDownButton.Enabled = true;
                    aglPORef.DropDownButton.Visible = true;
                }
                else
                {
                    aglPORef.ReadOnly = true;
                    aglPORef.DropDownButton.Enabled = false;
                    aglPORef.DropDownButton.Visible = false;
                }
            }
            else
            {
                aglPORef.ReadOnly = true;
                aglPORef.DropDownButton.Enabled = false;
                aglPORef.DropDownButton.Visible = false;
            }
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
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
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
        protected void Button_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton button = sender as ASPxButton;

            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (chkPORef.Checked == true)
                {
                    button.Enabled = true;

                }
                else
                {
                    button.Enabled = false;
                }
            }
            else
            {
                button.Enabled = false;
            }
        }
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   
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

            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }

            if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    if (chkPORef.Checked == true)
                    {
                        e.Visible = false;
                    }
                    else
                    {
                        e.Visible = true;
                    }
                }
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
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
                if (e.ButtonID == "Details")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
            else if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                if (e.ButtonID == "Delete")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
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
            gparam._TransType = "ACTEXP";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ExpenseProcessing_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTEXP";
            gparam._Table = "Accounting.ExpenseProcessing";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ExpenseProcessing_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string code = e.Parameters.Split('|')[1];//Set Item Code
            var itemlookup = sender as ASPxGridView;
            string qty = "";
            string desc = "";

            DataTable getDefault = new DataTable();
            getDefault = Gears.RetriveData2("SELECT DISTINCT ISNULL(ProfitCenterCode,'') AS ProfitCenter, ISNULL(CostCenterCode,'') AS CostCenter FROM Masterfile.BPEmployeeInfo WHERE EmployeeCode = '" + Session["userid"].ToString().Trim() + "'", Session["ConnString"].ToString());


            if (e.Parameters.Contains("VATCode"))
            {
                DataTable getqty = Gears.RetriveData2("Select ISNULL(Rate,0) AS Rate from masterfile.Tax where Tcode = '" + code + "'", Session["ConnString"].ToString());
                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["Rate"].ToString();
                    }
                }
                if (qty == "")
                {
                    qty = "0";
                }
                itemlookup.JSProperties["cp_identifier"] = "VAT";
                itemlookup.JSProperties["cp_codes"] = Convert.ToDecimal(qty) + ";";
            }
            else if (e.Parameters.Contains("ATCCode"))
            {
                DataTable getqty = Gears.RetriveData2("Select ISNULL(Rate,0) AS Rate from masterfile.ATC where ATCCode = '" + code + "'", Session["ConnString"].ToString());
                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["Rate"].ToString();
                    }
                }

                itemlookup.JSProperties["cp_identifier"] = "ATC";
                itemlookup.JSProperties["cp_codes"] = Convert.ToDecimal(qty) + ";";
            }
            else if (e.Parameters.Contains("Supplier"))
            {
                DataTable getSupplierDet = Gears.RetriveData2("SELECT DISTINCT SupplierCode, Name FROM Masterfile.BPSupplierInfo WHERE SupplierCode = '" + code + "'", Session["ConnString"].ToString());

                if (getSupplierDet.Rows.Count == 1)
                {
                    desc = getSupplierDet.Rows[0]["Name"].ToString() + ";";
                }
                else
                {
                    desc = ";";
                }

                itemlookup.JSProperties["cp_identifier"] = "SupplierCN";
                itemlookup.JSProperties["cp_codes"] = desc;
            }
            else
            {
                DataTable getqty = Gears.RetriveData2("SELECT DISTINCT A.ServiceCode, A.Description, A.AccountCode, A.SubsiCode, ISNULL(A.IsVatable,0) AS IsVatable, ISNULL(A.VATCode,'') AS VATCode, "
                + " ISNULL(B.Rate,0) AS Rate FROM Masterfile.Service A LEFT JOIN Masterfile.Tax B ON A.VATCode = B.TCode WHERE A.ServiceCode = '" + code + "'", Session["ConnString"].ToString());
                
                if (getqty.Rows.Count == 1)
                {
                    desc = getqty.Rows[0]["Description"].ToString() + ";";
                    desc += getqty.Rows[0]["AccountCode"].ToString() + ";";
                    desc += getqty.Rows[0]["SubsiCode"].ToString() + ";";
                    desc += getqty.Rows[0]["IsVatable"].ToString() + ";";
                    desc += getqty.Rows[0]["VATCode"].ToString() + ";";
                    desc += getqty.Rows[0]["Rate"].ToString() + ";";
                    desc += (getDefault.Rows.Count > 0 ? getDefault.Rows[0]["ProfitCenter"].ToString() : "N/A") + ";";
                    desc += (getDefault.Rows.Count > 0 ? getDefault.Rows[0]["CostCenter"].ToString() : "N/A") + ";";
                    desc += (Session["ExpProSupplier"] != null || Session["ExpProSupplier"] != "" ? Session["ExpProSupplier"].ToString() : "") + ";";
                    desc += (Session["ExpProName"] != null || Session["ExpProName"] != "" ? Session["ExpProName"].ToString() : "") + ";";
                }
                else
                {
                    desc = ";;;;;;;;;;";
                }

                itemlookup.JSProperties["cp_identifier"] = "Expense";
                itemlookup.JSProperties["cp_codes"] = desc;
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = DocDate.Text;
            _Entity.PayableTo = String.IsNullOrEmpty(aglPayables.Text) ? "" : aglPayables.Value.ToString();
            _Entity.PayableName = txtName.Text;
            _Entity.TotalGrossVatable = Convert.ToDecimal(string.IsNullOrEmpty(TotalGross.Text) ? "0" : TotalGross.Text);
            _Entity.TotalGrossNonVatable = Convert.ToDecimal(string.IsNullOrEmpty(TotalGrossNon.Text) ? "0" : TotalGrossNon.Text);
            _Entity.TotalVatAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalVat.Text) ? "0" : TotalVat.Text);
            _Entity.TotalWitholdingTax = Convert.ToDecimal(string.IsNullOrEmpty(TotalWithholding.Text) ? "0" : TotalWithholding.Text);
            _Entity.TotalAmountDue = Convert.ToDecimal(string.IsNullOrEmpty(TotalAmount.Text) ? "0" : TotalAmount.Text);
            _Entity.DueDate = DueDate.Text;
            _Entity.Terms = Convert.ToDecimal(string.IsNullOrEmpty(Terms.Text) ? "0" : Terms.Text);
            _Entity.IsWithPO = Convert.ToBoolean(chkPORef.Value.ToString());
            _Entity.RefTrans = String.IsNullOrEmpty(aglPORef.Text) ? "" : aglPORef.Text;
            _Entity.HRemarks = String.IsNullOrEmpty(memRemarks.Text) ? "" : memRemarks.Text;
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            string strError = "";

            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();

                    strError = CheckSubmit();
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

                        if (Session["ExpProDatatable"] == "1")
                        {
                            _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv1.DataSourceID = sdsTransDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            if (Session["ExpProWithPO"] == "1")
                            {
                                _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                            }
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv1.UpdateEdit();
                        }

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

                case "AddZero":
                case "UpdateZero":

                //gv1.UpdateEdit();

                strError = CheckSubmit();
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

                    gv1.DataSourceID = "sdsDetail";
                    if (gv1.DataSource != "")
                    {
                        gv1.DataSource = null;
                    }
                    gv1.DataBind();

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

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                break;

                case "refgrid":
                    gv1.DataBind();
                break;

                case "CallbackPayables":
                    DataTable getdata = new DataTable();
                    getdata = Gears.RetriveData2("SELECT RTRIM(LTRIM(Name)) AS Name FROM Masterfile.BizPartner WHERE BizPartnerCode = '" + aglPayables.Value.ToString().Trim() + "'", Session["ConnString"].ToString());
                    if (getdata.Rows.Count > 0)
                    {
                        txtName.Text = getdata.Rows[0]["Name"].ToString();
                        Session["ExpProName"] = getdata.Rows[0]["Name"].ToString();
                    }
                    Session["ExpProSupplier"] = aglPayables.Value.ToString().Trim();
                    Session["ExpProDocDate"] = DocDate.Text;
                    Filter();
                break;

                case "Terms":
                    DateTime dttime = Convert.ToDateTime(DocDate.Value);
                    Terms.Text = string.IsNullOrEmpty(Terms.Text) ? "0" : Terms.Text;
                    DueDate.Text = dttime.AddDays(Convert.ToDouble(Terms.Text)).ToShortDateString();
                    Filter();
                break;

                case "CallbackCheck":
                    Session["ExpProDatatable"] = "0";
                    gv1.DataSourceID = "sdsDetail";
                    if (gv1.DataSource != null)
                    {
                        gv1.DataSource = null;
                    }
                    gv1.DataBind();

                    aglPORef.Text = "";
                    Filter();
                    Session["ExpProWithPO"] = "1";
                    cp.JSProperties["cp_calculate"] = true;
                    
                break;

                case "CallbackRefPO":
                    if (String.IsNullOrEmpty(aglPORef.Text))
                    {
                        cp.JSProperties["cp_nodetail"] = "No reference PO is selected!";
                    }
                    else
                    {
                        Session["ExpProTransNo"] = null;
                        Session["ExpProTransNo"] = aglPORef.Text;
                        GetSelectedVal();

                        DataTable getRemarks = new DataTable();
                        getRemarks = Gears.RetriveData2(HeaderRemarks(), Session["ConnString"].ToString());

                        string newRemarks = "";

                        foreach (DataRow dt in getRemarks.Rows)
                        {
                            newRemarks += dt["DocNumber"].ToString() + Environment.NewLine + dt["Remarks"].ToString() + Environment.NewLine;
                        }

                        if (!String.IsNullOrEmpty(newRemarks))
                        {
                            memRemarks.Text = newRemarks;
                        }
                    }
                    Filter();
                    cp.JSProperties["cp_calculate"] = true;
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["ExpProDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["RecordID"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ExpenseCode"] = values.NewValues["ExpenseCode"];
                    row["ExpenseDescription"] = values.NewValues["ExpenseDescription"];
                    row["GLAccountCode"] = values.NewValues["GLAccountCode"];
                    row["SubsiCode"] = values.NewValues["SubsiCode"];
                    row["ProfitCenterCode"] = values.NewValues["ProfitCenterCode"];
                    row["CostCenterCode"] = values.NewValues["CostCenterCode"];
                    row["Amount"] = values.NewValues["Amount"];
                    row["SupplierCode"] = values.NewValues["SupplierCode"];
                    row["SupplierName"] = values.NewValues["SupplierName"];
                    row["IsVatable"] = values.NewValues["IsVatable"];
                    row["VATCode"] = values.NewValues["VATCode"];
                    row["VATAmount"] = values.NewValues["VATAmount"];
                    row["VATRate"] = values.NewValues["VATRate"];
                    row["IsEWT"] = values.NewValues["IsEWT"];
                    row["ATCCode"] = values.NewValues["ATCCode"];
                    row["ATCRate"] = values.NewValues["ATCRate"];
                    //row["Remarks"] = values.NewValues["Remarks"];
                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                    //row["RecordID"] = values.NewValues["RecordID"];
                    row["PONumber"] = values.NewValues["PONumber"];
                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["RecordID"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (DataRow dtRow in source.Rows)
                {
                    _EntityDetail.ExpenseCode = dtRow["ExpenseCode"].ToString();
                    _EntityDetail.ExpenseDescription = dtRow["ExpenseDescription"].ToString();
                    _EntityDetail.GLAccountCode = dtRow["GLAccountCode"].ToString();
                    _EntityDetail.SubsiCode = dtRow["SubsiCode"].ToString();
                    _EntityDetail.ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                    _EntityDetail.CostCenterCode = dtRow["CostCenterCode"].ToString();
                    _EntityDetail.Amount = Convert.ToDecimal(Convert.IsDBNull(dtRow["Amount"]) ? 0 : dtRow["Amount"]);
                    _EntityDetail.SupplierCode = dtRow["SupplierCode"].ToString();
                    _EntityDetail.SupplierName = dtRow["SupplierName"].ToString();
                    _EntityDetail.IsVatable = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVatable"].ToString()) ? false : dtRow["IsVatable"]);
                    _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                    _EntityDetail.VATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATAmount"]) ? 0 : dtRow["VATAmount"]);
                    _EntityDetail.VATRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATRate"]) ? 0 : dtRow["VATRate"]);
                    _EntityDetail.IsEWT = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsEWT"]) ? false : dtRow["IsEWT"]);
                    _EntityDetail.ATCCode = dtRow["ATCCode"].ToString();
                    _EntityDetail.ATCRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ATCRate"]) ? 0 : dtRow["ATCRate"]);
                    //_EntityDetail.Remarks = dtRow["Remarks"].ToString();
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.RecordID = dtRow["RecordID"].ToString();
                    _EntityDetail.PONumber = dtRow["PONumber"].ToString();
                    _EntityDetail.AddExpenseProcessingDetail(_EntityDetail);

                }
            }
        }
        #endregion
        private DataTable GetSelectedVal()
        {
            Session["ExpProDatatable"] = "0";
            gv1.DataSourceID = null;
            gv1.DataBind();
            DataTable dt = new DataTable();
            sdsTransDetail.SelectCommand = Detail();
            gv1.DataSource = sdsTransDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session["ExpProDatatable"] = "1";

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

            dt.PrimaryKey = new DataColumn[] {
            dt.Columns["RecordID"]};

            return dt;
        }

        public string Detail()
        {

            string query = "";
            string Value = "";
            int cnt = 0;
            string bridge = "";

            int count = aglPORef.Text.Split(';').Length;
            var pieces = aglPORef.Text.Split(new[] { ';' }, count);

            foreach (string c in pieces)
            {
                var a = c.Split(new[] { '-' }, 2);

                if (cnt != 0)
                {
                    bridge = " OR ";
                }
                Value += bridge + "(A.RecordID = '" + a[0].ToString() + "' AND A.DocNumber ='" + a[1].ToString() + "')";

                cnt = cnt + 1;
            }

            query = "SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber, A.LineNumber) AS VARCHAR(5)),5) AS LineNumber, A.DocNumber AS PONumber,"
                     + " A.ServiceType AS ExpenseCode, A.Description AS ExpenseDescription, ISNULL(B.AccountCode,'') AS GLAccountCode, ISNULL(B.SubsiCode,'') AS SubsiCode, "
                     + " ISNULL(C.ProfitCenterCode,'') AS ProfitCenterCode, ISNULL(C.CostCenterCode,'') AS CostCenterCode, ISNULL(A.TotalCost,0) - ISNULL(A.CostApplied,0) AS Amount, "
                     + " '" + aglPayables.Value.ToString() + "' AS SupplierCode, '" + txtName.Text + "' AS SupplierName, "
                     + " ISNULL(A.IsVat,0) AS IsVatable, ISNULL(A.VATCode,'') AS VATCode, ISNULL(A.TotalCost,0) * ISNULL(A.VATRate,0) AS VATAmount, ISNULL(A.VATRate,0) AS VATRate, "
                     + " D.IsEWT, D.ATCCode, D.ATCRate, "
                     + " CONVERT(varchar(MAX),'') AS Remarks, A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, '2' AS Version, A.RecordID "
                     + " FROM Procurement.PurchaseOrderService A INNER JOIN Masterfile.Service B ON A.ServiceType = B.ServiceCode LEFT JOIN "
                     + " Masterfile.BPEmployeeInfo C ON C.EmployeeCode = '" + Session["userid"].ToString().Trim() + "'"
                     + " LEFT JOIN (SELECT SupplierCode, ISNULL(IsWithholdingTaxAgent,0) AS IsEWT, CASE WHEN ISNULL(IsWithholdingTaxAgent,0) = 0 THEN '' ELSE A.ATCCode END AS ATCCode, "
                     + " CASE WHEN ISNULL(IsWithholdingTaxAgent,0) = 0 THEN 0 ELSE ISNULL(Rate,0) END AS ATCRate FROM Masterfile.BPSupplierInfo A "
                     + " LEFT JOIN Masterfile.ATC B ON A.ATCCode = B.ATCCode) D ON D.SupplierCode = '" + aglPayables.Value.ToString() + "'"
                     + " WHERE ( " + Value + " )";

            return query;
        }

        public string HeaderRemarks()
        {

            string query = "";
            string Value = "";
            int cnt = 0;
            string bridge = "";

            int count = aglPORef.Text.Split(';').Length;
            var pieces = aglPORef.Text.Split(new[] { ';' }, count);

            foreach (string c in pieces)
            {
                var a = c.Split(new[] { '-' }, 2);

                if (cnt != 0)
                {
                    bridge = " OR ";
                }
                Value += bridge + "(DocNumber ='" + a[1].ToString() + "')";

                cnt = cnt + 1;
            }

            query = "SELECT DISTINCT '[DOCNUMBER:] ' + DocNumber AS DocNumber, '[REMARKS:] ' + RTRIM(LTRIM(Remarks)) AS Remarks"
                  + " FROM Procurement.PurchaseOrder WHERE ISNULL(Remarks,'') != '' AND (" + Value + ") ";

            return query;
        }

        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {            
            //DataTable getdetail = new DataTable();
            //getdetail = Gears.RetriveData2("SELECT DISTINCT ISNULL(ProfitCenterCode,'') AS ProfitCenter, ISNULL(CostCenterCode,'') AS CostCenter FROM Masterfile.BPEmployeeInfo WHERE EmployeeCode = '" + Session["userid"].ToString().Trim() + "'", Session["ConnString"].ToString());

            //if (getdetail.Rows.Count == 1)
            //{
            //    e.NewValues["ProfitCenterCode"] = getdetail.Rows[0]["ProfitCenter"].ToString();
            //    e.NewValues["CostCenterCode"] = getdetail.Rows[0]["CostCenter"].ToString();
            //}

            //if (Session["ExpProSupplier"] != null)
            //{
            //    e.NewValues["SupplierCode"] = Session["ExpProSupplier"].ToString();
            //    e.NewValues["SupplierName"] = Session["ExpProName"].ToString();
            //}

            e.NewValues["IsVatable"] = false;
            e.NewValues["IsEWT"] = false;
            e.NewValues["VATRate"] = 0;
        }

        protected void aglPORef_Init(object sender, EventArgs e)
        {
            Filter();
        }
        public string CheckSubmit()
        {
            string result = "";

            result = Functions.Submitted(_Entity.DocNumber, "Accounting.ExpenseProcessing", 1, Session["ConnString"].ToString());

            return result;
        }

        protected void Filter()
        {
            if ((Session["ExpProSupplier"] != null && Session["ExpProSupplier"].ToString() != "") && (Session["ExpProDocDate"] != null && Session["ExpProDocDate"].ToString() != ""))
            {
                sdsPOReference.SelectCommand = "SELECT A.DocNumber AS PONumber, B.DocDate AS PODate, B.SupplierCode, C.Name, B.TargetDeliveryDate, B.CommitmentDate, A.PRNumber, A.ServiceType AS ServiceCode, A.Description AS ServDesc, "
                    + " A.ServiceQty, A.Unit, A.UnitCost, A.TotalCost, ISNULL(A.IsAllowProgressBilling,0) AS AllowProgress, CASE WHEN ISNULL(A.IsVat,0) = 0 THEN A.ServiceQty * A.UnitCost * ISNULL(A.VATRate,0) ELSE 0 END AS GrossNonVat, "
                    + " CASE WHEN ISNULL(A.IsVat,0) = 1 THEN A.ServiceQty * A.UnitCost * ISNULL(A.VATRate,0) ELSE 0 END AS GrossVat, A.EPNumber, A.RecordID FROM Procurement.PurchaseOrderService A "
                    + " INNER JOIN Procurement.PurchaseOrder B ON A.DocNumber = B.DocNumber LEFT JOIN Masterfile.BPSupplierInfo C ON B.SupplierCode = C.SupplierCode WHERE "
                    + " ISNULL(B.SubmittedBy,'') != '' AND ISNULL(B.CancelledBy,'') = '' AND B.Status IN ('N','P') AND ISNULL(A.IsClosed,0) = 0 AND B.SupplierCode = '" + Session["ExpProSupplier"].ToString().Trim() + "'"
					// 03-23-2017   Added code to filter reference
                    // + " AND B.DocDate <= '" + DocDate.Text + "'";                    
                    + " AND B.DocDate <= '" + Session["ExpProDocDate"].ToString() + "'";
                    // END
                //sdsPOReference.DataBind();
                aglPORef.DataBind();
            }
        }
    }
}