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
using GearsAccounting;
using DevExpress.Data.Filtering;


namespace GWL
{
    public partial class frmServiceBilling : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.ServiceBilling _Entity = new ServiceBilling();
        Entity.ServiceBilling.ServiceBillingDetail _EntityDetail = new ServiceBilling.ServiceBillingDetail();

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

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            txtDocNumber.ReadOnly = true;
            //this.gvRef.Columns["CommandString"].Width = 0;
            //this.gvRef.Columns["RCommandString"].Width = 0;

            gv1.KeyFieldName = "DocNumber;LineNumber";

            if (!IsPostBack)
            {
                Session["ServBilDatatable"] = null;
                Session["ServBilExpression"] = null;
                Session["ServBilTransNo"] = null;
                Session["ServBilPartner"] = null;
                Session["ServBilName"] = null;
                Session["ServBilWithPO"] = null;
                Session["ServBilProfit"] = null;
                Session["ServBilCost"] = null;

                Connection = Session["ConnString"].ToString();

                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                //}                

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                    dtpDocDate.Date = DateTime.Now;
                    dtpDueDate.Date = DateTime.Now;
                }

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                aglReceivable.Value = _Entity.CustomerCode.ToString();
                dtpDueDate.Text = String.IsNullOrEmpty(_Entity.DueDate) ? "" : Convert.ToDateTime(_Entity.DueDate).ToShortDateString();
                speTerms.Value = _Entity.Term;
                txtName.Text = _Entity.Name.ToString();
                txtRefNumber.Value = _Entity.RefNumber;
                speTotalGross.Value = _Entity.GrossVatAmount;
                speTotalGrossNon.Value = _Entity.GrossNonVatAmount;
                speTotalVat.Value = _Entity.VATAmount;
                speTotalWithholding.Text = _Entity.WithholdingTax.ToString();
                speTotalAmount.Text = _Entity.NetAmount.ToString();
                memRemarks.Value = _Entity.Remarks;
                    
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
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;

                Session["ServBilPartner"] = _Entity.CustomerCode.ToString();
                Session["ServBilName"] = _Entity.Name.ToString();

                DataTable getdetail = new DataTable();
                getdetail = Gears.RetriveData2("SELECT TOP 1 ProfitCenterCode, CostCenterCode FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + _Entity.CustomerCode.ToString() + "'", Session["ConnString"].ToString());

                if (getdetail.Rows.Count > 0)
                {
                    Session["ServBilProfit"] = getdetail.Rows[0]["ProfitCenterCode"].ToString();
                    Session["ServBilCost"] = getdetail.Rows[0]["CostCenterCode"].ToString();
                }                

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
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }

                //if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                //{
                //    gvRef.DataSourceID = "odsReference";
                //}

                DataTable checkCount = new DataTable();
                
                checkCount = Gears.RetriveData2("SELECT TOP 1 DocNumber FROM Accounting.ServiceBillingDetail WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gv1.DataSourceID = (checkCount.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

                gvJournal.DataSourceID = "odsJournalEntry";

                //Filter();
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

            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                look.ReadOnly = false;
            }
            else
            {
                look.ReadOnly = true;
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
                button.Enabled = true;
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
        protected void MemoLoad(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxMemo memo = sender as ASPxMemo;
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
                    e.Visible = true;
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
            gparam._TransType = "ACTSEB";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ServiceBilling_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;
            }
        }
        #endregion                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTSEB";
            gparam._Table = "Accounting.ServiceBilling";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ServiceBilling_Post(gparam);
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
            else
            {
                DataTable getqty = Gears.RetriveData2("SELECT DISTINCT UPPER(A.ServiceCode) AS ServiceCode, UPPER(A.Description) AS Description, A.AccountCode, A.SubsiCode, ISNULL(A.IsVatable,0) AS IsVatable, ISNULL(A.VATCode,'NONV') AS VATCode, "
                + " ISNULL(B.Rate,0) AS Rate, '" + Session["ServBilProfit"] + "' AS ProfitCenter, '" + Session["ServBilCost"] + "' AS CostCenter FROM Masterfile.Service A LEFT JOIN Masterfile.Tax B ON A.VATCode = B.TCode WHERE A.ServiceCode = '" + code + "'", Session["ConnString"].ToString());
                
                if (getqty.Rows.Count == 1)
                {
                    desc = getqty.Rows[0]["Description"].ToString() + ";";
                    desc += getqty.Rows[0]["AccountCode"].ToString() + ";";
                    desc += getqty.Rows[0]["SubsiCode"].ToString() + ";";
                    desc += getqty.Rows[0]["IsVatable"].ToString() + ";";
                    desc += getqty.Rows[0]["VATCode"].ToString() + ";";
                    desc += getqty.Rows[0]["Rate"].ToString() + ";";
                    desc += getqty.Rows[0]["ProfitCenter"].ToString() + ";";
                    desc += getqty.Rows[0]["CostCenter"].ToString() + ";";
                }
                else
                {
                    desc = ";;;;;;;;";
                }

                itemlookup.JSProperties["cp_identifier"] = "Expense";
                itemlookup.JSProperties["cp_codes"] = desc;
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(aglReceivable.Text) ? "" : aglReceivable.Value.ToString();
            _Entity.Name = txtName.Text;
            _Entity.RefNumber = txtRefNumber.Text;
            _Entity.DueDate = dtpDueDate.Text;
            _Entity.Term = Convert.ToDecimal(string.IsNullOrEmpty(speTerms.Text) ? "0" : speTerms.Text);
            _Entity.GrossVatAmount = Convert.ToDecimal(string.IsNullOrEmpty(speTotalGross.Text) ? "0" : speTotalGross.Text);
            _Entity.GrossNonVatAmount = Convert.ToDecimal(string.IsNullOrEmpty(speTotalGrossNon.Text) ? "0" : speTotalGrossNon.Text);
            _Entity.VATAmount = Convert.ToDecimal(string.IsNullOrEmpty(speTotalVat.Text) ? "0" : speTotalVat.Text);
            _Entity.WithholdingTax = Convert.ToDecimal(string.IsNullOrEmpty(speTotalWithholding.Text) ? "0" : speTotalWithholding.Text);
            _Entity.NetAmount = Convert.ToDecimal(string.IsNullOrEmpty(speTotalAmount.Text) ? "0" : speTotalAmount.Text);
            _Entity.Remarks = memRemarks.Text;

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

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM Accounting.ServiceBilling WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());

            string strError = "";
            string param = e.Parameter.Split('|')[0];

            switch (param)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();

                    strError = CheckSubmit();
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = "Cannot update! Document is either Approved, Submitted or Cancelled already!";
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
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                break;

                case "CallbackCustomer":
                    string cust = (e.Parameter.Split('|')[1] == null || e.Parameter.Split('|')[1] == "") ? "" : e.Parameter.Split('|')[1].Trim();
                    DataTable getdata = new DataTable();
                    getdata = Gears.RetriveData2("SELECT UPPER(ISNULL(Name,'')) AS Name, DATEADD(dd,ISNULL(ARTerm,0),'" + dtpDocDate.Text + "') AS DueDate, ISNULL(ARTerm,0) AS Terms FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + cust + "'", Session["ConnString"].ToString());
                                        
                    if (getdata.Rows.Count > 0)
                    {
                        txtName.Text = getdata.Rows[0]["Name"].ToString();
                        dtpDueDate.Text = Convert.ToDateTime(getdata.Rows[0]["DueDate"].ToString()).ToShortDateString();
                        speTerms.Value = getdata.Rows[0]["Terms"].ToString();
                        Session["ServBilName"] = getdata.Rows[0]["Name"].ToString();
                    }
                    
                    DataTable getdetail = new DataTable();
                    getdetail = Gears.RetriveData2("SELECT TOP 1 ProfitCenterCode, CostCenterCode FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + cust + "'", Session["ConnString"].ToString());

                    if (getdetail.Rows.Count > 0)
                    {
                        Session["ServBilProfit"] = getdetail.Rows[0]["ProfitCenterCode"].ToString();
                        Session["ServBilCost"] = getdetail.Rows[0]["CostCenterCode"].ToString();
                    }

                    Session["ServBilPartner"] = cust;
                break;

                case "CallbackTerms":
                    DateTime dttime = Convert.ToDateTime(dtpDocDate.Value);
                    speTerms.Text = string.IsNullOrEmpty(speTerms.Text) ? "0" : speTerms.Text;
                    dtpDueDate.Text = dttime.AddDays(Convert.ToDouble(speTerms.Text)).ToShortDateString();
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
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion
        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["ProfitCenterCode"] = (Session["ServBilProfit"] == null || Session["ServBilProfit"] == "") ? "" : Session["ServBilProfit"].ToString();
            e.NewValues["CostCenterCode"] = (Session["ServBilCost"] == null || Session["ServBilCost"] == "") ? "" : Session["ServBilCost"].ToString();
            e.NewValues["IsVatable"] = false;
            e.NewValues["IsEWT"] = false;
            e.NewValues["VATCode"] = "NONV";
            e.NewValues["VATRate"] = 0;
            e.NewValues["ATCRate"] = 0;
        }
        public string CheckSubmit()
        {
            string result = "";

            result = Functions.Submitted(_Entity.DocNumber, "Accounting.ServiceBilling", 1, Session["ConnString"].ToString());
            result += (String.IsNullOrEmpty(result) ? "" : Environment.NewLine) + Functions.Submitted(_Entity.DocNumber, "Accounting.ServiceBilling", 3, Session["ConnString"].ToString());

            return result;
        }
    }
}