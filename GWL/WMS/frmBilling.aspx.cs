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
using System.Data.SqlClient;
using System.Data.Sql;

namespace GWL
{
    public partial class frmBilling : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.Billing _Entity = new Billing();//Calls entity odsHeader
        Entity.Billing.BillingDetail _EntityDetail = new Billing.BillingDetail();//Call entity sdsDetail

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

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            gv1.KeyFieldName = "DocNumber;Date";

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;
            }
            else
            {
                view = false;
            }


            if (!IsPostBack)
            {
                Session["WMSBILServType"] = null;
                Session["WMSBILBizPartner"] = null;

                Connection = (Session["ConnString"].ToString());

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglBizPartnerCode.Value = _Entity.BizPartnerCode;
                aglWarehouse.Value = _Entity.WarehouseCode;
                //txtContract.Value = _Entity.ContractNumber;
                txtBillPeriod.Value = _Entity.BillingPeriodType;
                dtpDateFrom.Text = String.IsNullOrEmpty(_Entity.DateFrom.ToString()) ? null : Convert.ToDateTime(_Entity.DateFrom.ToString()).ToShortDateString();
                dtpDateTo.Text = String.IsNullOrEmpty(_Entity.DateTo.ToString()) ? null : Convert.ToDateTime(_Entity.DateTo.ToString()).ToShortDateString();
                aglServiceType.Value = _Entity.ServiceType;
                aglProfitCenter.Value = _Entity.ProfitCenterCode;
                txtBillingStatement.Value = _Entity.BillingStatement;
                speBegInv.Value = _Entity.BeginningInv;
                speTotalAmount.Value = _Entity.TotalAmount;
                speTotalVat.Value = _Entity.TotalVat;
                speTotalGross.Value = _Entity.TotalGross;
                aglCustomerCode.Value = _Entity.CustomerCode; 
                aglStorageCode.Value = _Entity.StorageCode;

                Session["WMSBILServType"] = _Entity.ServiceType;
                Session["WMSBILBizPartner"] = _Entity.BizPartnerCode + "|";
                //txtUnit.Value = _Entity.UnitOfMeasure;
                //speStorageRate.Value = _Entity.StorageRate;
                //speMinHandlingIn.Value = _Entity.MinHandlingIn;
                //speMinHandlingOut.Value = _Entity.MinHandlingOut;
                //speHandlingInRate.Value = _Entity.HandlingInRate;
                //speHandlingOutRate.Value = _Entity.HandlingOutRate;
                //speMinStorage.Value = _Entity.MinStorage;
                //txtBillingType.Value = _Entity.BillingType;

                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtHPostedBy.Text = _Entity.PostedBy;
                txtHPostedDate.Text = _Entity.PostedDate;

                DataTable OpenPeriod = Gears.RetriveData2("DECLARE @Month int DECLARE @Year int " +
                    " SELECT @Month = Value FROM IT.SystemSettings WHERE Code = 'CMONTH' SELECT @Year = Value FROM IT.SystemSettings WHERE Code = 'CYEAR' " +
                    " SELECT CONVERT(DATE,CONVERT(varchar(4),@Year)+'-'+CONVERT(varchar(2),@Month)+'-01') AS OpenPeriod", Session["ConnString"].ToString());

                dtpOpenPeriod.Text = Convert.ToDateTime(OpenPeriod.Rows[0]["OpenPeriod"].ToString()).ToShortDateString();

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

                        if (String.IsNullOrEmpty(_Entity.WarehouseCode))
                        {
                            aglWarehouse.Value = "MLICAV";
                        }

                        if (String.IsNullOrEmpty(_Entity.ProfitCenterCode))
                        {
                            aglProfitCenter.Value = "MET001";
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;

                        if (String.IsNullOrEmpty(_Entity.WarehouseCode))
                        {
                            aglWarehouse.Value = "MLICAV";
                        }

                        if (String.IsNullOrEmpty(_Entity.ProfitCenterCode))
                        {
                            aglProfitCenter.Value = "MET001";
                        }
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                }

                DataTable checkCount = new DataTable();
                checkCount = Gears.RetriveData2("SELECT DocNumber FROM WMS.BillingDetail WHERE DocNumber ='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                gv1.DataSourceID = (checkCount.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

                gvJournal.DataSourceID = "odsJournalEntry";

                DisableFields();
            }
        }
        
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSBIL";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GWarehouseManagement.StoreBilling_Validate(gparam); 

            cp.JSProperties["cp_valmsg"] = strresult;

        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSBIL";
            gparam._Table = "WMS.Billing";
            gparam._Connection = Session["ConnString"].ToString();
            gparam._Factor = -1;
            string strresult = GearsWarehouseManagement.GWarehouseManagement.Billing_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;
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
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D"
                || Request.QueryString["entry"].ToString() == "E" || Request.QueryString["entry"].ToString() == "N")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DropDownButton.Enabled = !view;
            combo.ReadOnly = view;
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
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void dtpDateFrom_Load(object sender, EventArgs e)
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = false;
            date.ReadOnly = true;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void ButtonLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (Request.QueryString["entry"] == "E")
            {
                var look = sender as ASPxButton;
                if (look != null)
                {
                    look.Enabled = true;
                }
            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }

            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New)
                e.Visible = false;
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
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //if (Session["FilterExpression"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = "sdsItemDetail";
            //    sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
            //}
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = String.IsNullOrEmpty(dtpDocDate.Text) ? null : Convert.ToDateTime(dtpDocDate.Text).ToShortDateString();
            _Entity.BizPartnerCode = String.IsNullOrEmpty(aglBizPartnerCode.Text) ? null : aglBizPartnerCode.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouse.Text) ? null : aglWarehouse.Value.ToString();
            //_Entity.ContractNumber = txtContract.Text;
            _Entity.BillingPeriodType = txtBillPeriod.Text;
            _Entity.DateFrom = String.IsNullOrEmpty(dtpDateFrom.Text) ? null : Convert.ToDateTime(dtpDateFrom.Text).ToShortDateString();
            _Entity.DateTo = String.IsNullOrEmpty(dtpDateTo.Text) ? null : Convert.ToDateTime(dtpDateTo.Text).ToShortDateString();
            _Entity.ServiceType = String.IsNullOrEmpty(aglServiceType.Text) ? null : aglServiceType.Value.ToString();
            _Entity.ProfitCenterCode = String.IsNullOrEmpty(aglProfitCenter.Text) ? null : aglProfitCenter.Value.ToString();
            _Entity.BillingStatement = txtBillingStatement.Text;
            _Entity.BeginningInv = String.IsNullOrEmpty(speBegInv.Text) ? 0 : Convert.ToDecimal(speBegInv.Text);
            _Entity.TotalAmount = String.IsNullOrEmpty(speTotalAmount.Text) ? 0 : Convert.ToDecimal(speTotalAmount.Text);
            _Entity.TotalVat = String.IsNullOrEmpty(speTotalVat.Text) ? 0 : Convert.ToDecimal(speTotalVat.Text);
            _Entity.TotalGross = String.IsNullOrEmpty(speTotalGross.Text) ? 0 : Convert.ToDecimal(speTotalGross.Text);
            _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Value.ToString();
            _Entity.StorageCode = String.IsNullOrEmpty(aglStorageCode.Text) ? null : aglStorageCode.Value.ToString();

            //_Entity.UnitOfMeasure = txtUnit.Text;
            //_Entity.StorageRate = String.IsNullOrEmpty(speStorageRate.Text) ? 0 : Convert.ToDecimal(speStorageRate.Text);
            //_Entity.MinHandlingIn = String.IsNullOrEmpty(speMinHandlingIn.Text) ? 0 : Convert.ToDecimal(speMinHandlingIn.Text);
            //_Entity.MinHandlingOut = String.IsNullOrEmpty(speMinHandlingOut.Text) ? 0 : Convert.ToDecimal(speMinHandlingOut.Text);
            //_Entity.HandlingInRate = String.IsNullOrEmpty(speHandlingInRate.Text) ? 0 : Convert.ToDecimal(speHandlingInRate.Text);
            //_Entity.HandlingOutRate = String.IsNullOrEmpty(speHandlingOutRate.Text) ? 0 : Convert.ToDecimal(speHandlingOutRate.Text);
            //_Entity.MinStorage = String.IsNullOrEmpty(speMinStorage.Text) ? 0 : Convert.ToDecimal(speMinStorage.Text);
            //_Entity.BillingType = txtBillingType.Text;
                                    
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

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM WMS.Billing WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());

            string param = e.Parameter.Split('=')[0];

            switch (param)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber, "WMS.Billing", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        Gears.RetriveData2 ("DELETE FROM WMS.BillingDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                        DataTable cnt = Gears.RetriveData2("SELECT COUNT(*) AS Row FROM WMS.BillingDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                        DataRow _cnt = cnt.Rows[0];
                        if (_cnt["Row"].ToString() == "0")
                        {

                            DataTable source = GenerateBilling();

                            foreach (DataRow dtRow in source.Rows)
                            {
                                _EntityDetail.Date = Convert.ToDateTime(dtRow["Date"].ToString());
                                _EntityDetail.DocIn = dtRow["DocIn"].ToString();
                                _EntityDetail.DocOut = dtRow["DocOut"].ToString();
                                _EntityDetail.QtyIn = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyIn"]) ? 0 : dtRow["QtyIn"]);
                                _EntityDetail.QtyOut = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyOut"]) ? 0 : dtRow["QtyOut"]);
                                _EntityDetail.EndingBal = Convert.ToDecimal(Convert.IsDBNull(dtRow["EndingBal"]) ? 0 : dtRow["EndingBal"]);
                                _EntityDetail.ChargeableEndBal = Convert.ToDecimal(Convert.IsDBNull(dtRow["ChargeableEndBal"]) ? 0 : dtRow["ChargeableEndBal"]);
                                _EntityDetail.StorageCharge = Convert.ToDecimal(Convert.IsDBNull(dtRow["StorageCharge"]) ? 0 : dtRow["StorageCharge"]);
                                _EntityDetail.Holding = Convert.ToDecimal(Convert.IsDBNull(dtRow["Holding"]) ? 0 : dtRow["Holding"]);
                                _EntityDetail.HandlingIn = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingIn"]) ? 0 : dtRow["HandlingIn"]);
                                _EntityDetail.HandlingOut = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingOut"]) ? 0 : dtRow["HandlingOut"]);
                                _EntityDetail.Amount = Convert.ToDecimal(Convert.IsDBNull(dtRow["Amount"]) ? 0 : dtRow["Amount"]);
                                _EntityDetail.Vat = Convert.ToDecimal(Convert.IsDBNull(dtRow["Vat"]) ? 0 : dtRow["Vat"]);
                                _EntityDetail.GrossAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossAmount"]) ? 0 : dtRow["GrossAmount"]);
                                _EntityDetail.CQtyIn = Convert.ToDecimal(Convert.IsDBNull(dtRow["CQtyIn"]) ? 0 : dtRow["CQtyIn"]);
                                _EntityDetail.CQtyOut = Convert.ToDecimal(Convert.IsDBNull(dtRow["CQtyOut"]) ? 0 : dtRow["CQtyOut"]);
                                _EntityDetail.UOM = dtRow["UOM"].ToString();
                                _EntityDetail.BillingType = dtRow["BillingType"].ToString();
                                _EntityDetail.StorageRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["StorageRate"]) ? 0 : dtRow["StorageRate"]);
                                _EntityDetail.HandlingInRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingInRate"]) ? 0 : dtRow["HandlingInRate"]);
                                _EntityDetail.HandlingOutRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingOutRate"]) ? 0 : dtRow["HandlingOutRate"]);
                                _EntityDetail.MinimumQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinimumQty"]) ? 0 : dtRow["MinimumQty"]);
                                _EntityDetail.MinHandlingIn = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingIn"]) ? 0 : dtRow["MinHandlingIn"]);
                                _EntityDetail.MinHandlingOut = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingOut"]) ? 0 : dtRow["MinHandlingOut"]);
                                if (!Convert.IsDBNull(dtRow["RRDate"]) && !String.IsNullOrEmpty(dtRow["RRDate"].ToString()))
                                {
                                    _EntityDetail.RRDate = Convert.ToDateTime(dtRow["RRDate"].ToString());
                                }
                                _EntityDetail.ProdNum = dtRow["ProdNum"].ToString();
                                if (!Convert.IsDBNull(dtRow["ProdDate"]) && !String.IsNullOrEmpty(dtRow["ProdDate"].ToString()))
                                {
                                    _EntityDetail.ProdDate = Convert.ToDateTime(dtRow["ProdDate"].ToString());
                                }
                                if (!Convert.IsDBNull(dtRow["AllocationDate"]) && !String.IsNullOrEmpty(dtRow["AllocationDate"].ToString()))
                                {
                                    _EntityDetail.AllocationDate = Convert.ToDateTime(dtRow["AllocationDate"].ToString());
                                }
                                _EntityDetail.NoStorageCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["NoStorageCharge"]) ? false : dtRow["NoStorageCharge"]);
                                _EntityDetail.NoHandlingCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["NoHandlingCharge"]) ? false : dtRow["NoHandlingCharge"]);
                                _EntityDetail.AllocChargeable = Convert.ToInt32(Convert.IsDBNull(dtRow["AllocChargeable"]) ? 0 : dtRow["AllocChargeable"]);
                                _EntityDetail.Staging = Convert.ToInt32(Convert.IsDBNull(dtRow["Staging"]) ? 0 : dtRow["Staging"]);
                                _EntityDetail.RefContract = dtRow["RefContract"].ToString();
                                _EntityDetail.ExcludeInBilling = Convert.ToBoolean(Convert.IsDBNull(dtRow["ExcludeInBilling"]) ? false : dtRow["ExcludeInBilling"]);
                                _EntityDetail.Remarks = dtRow["Remarks"].ToString();
                                _EntityDetail.RefRecordID = dtRow["RefRecordID"].ToString();
                                _EntityDetail.MulStorageCode = dtRow["MulStorageCode"].ToString();
                                _EntityDetail.MulCustomerCode = dtRow["MulCustomerCode"].ToString();
                                _EntityDetail.AddBillingDetail(_EntityDetail);
                            }
                        }

                        Post();
                        Validate();

                        if (e.Parameter == "Add")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else
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

                case "Generate":
                    //if (String.IsNullOrEmpty(txtContract.Text) ||
                    //String.IsNullOrEmpty(aglBizPartnerCode.Text) ||
                    //String.IsNullOrEmpty(dtpDateFrom.Text) ||
                    //String.IsNullOrEmpty(dtpDateTo.Text) ||
                    //String.IsNullOrEmpty(aglServiceType.Text) ||
                    ////String.IsNullOrEmpty(txtBillingType.Text) ||
                    //String.IsNullOrEmpty(txtDocNumber.Text))
                    //{
                    //    cp.JSProperties["cp_noparameter"] = true;
                    //    cp.JSProperties["cp_noparametermsg"] = "Please check if the transaction has the following details:" + Environment.NewLine +
                    //        (String.IsNullOrEmpty(aglServiceType.Text) ? "    Service Type" + Environment.NewLine : "") +
                    //        (String.IsNullOrEmpty(aglBizPartnerCode.Text) ? "    Business Partner" + Environment.NewLine : "") +
                    //        //(String.IsNullOrEmpty(txtBillingType.Text) ? "    Billing Type" + Environment.NewLine : "") +
                    //        (String.IsNullOrEmpty(txtContract.Text) ? "    Contract Number" + Environment.NewLine : "") +
                    //        (String.IsNullOrEmpty(dtpDateFrom.Text) ? "    Date From" + Environment.NewLine : "") +
                    //        (String.IsNullOrEmpty(dtpDateTo.Text) ? "    Date To" : "");
                    //}
                    DisableFields();
                    if (String.IsNullOrEmpty(aglBizPartnerCode.Text) ||
                    String.IsNullOrEmpty(dtpDateFrom.Text) ||
                    String.IsNullOrEmpty(dtpDateTo.Text) ||
                    String.IsNullOrEmpty(aglServiceType.Text) ||
                    String.IsNullOrEmpty(txtDocNumber.Text))
                    {
                        cp.JSProperties["cp_noparameter"] = true;
                        cp.JSProperties["cp_noparametermsg"] = "Please check if the transaction has the following details:" + Environment.NewLine +
                            (String.IsNullOrEmpty(aglBizPartnerCode.Text) ? "    Business Partner" + Environment.NewLine : "") +
                            (String.IsNullOrEmpty(aglServiceType.Text) ? "    Service Type" + Environment.NewLine : "") +
                            (String.IsNullOrEmpty(dtpDateFrom.Text) ? "    Date From" + Environment.NewLine : "") +
                            (String.IsNullOrEmpty(dtpDateTo.Text) ? "    Date To" : "");
                    }
                    else
                    {
                        GenerateBillingInfo();
                        GenerateBilling();
                        cp.JSProperties["cp_generated"] = true;
                    }
                    break;

                case "CallbackServType":
                    Session["WMSBILServType"] = null;
                    Session["WMSBILServType"] = aglServiceType.Text;
                    DisableFields();
                    if (!String.IsNullOrEmpty(aglBizPartnerCode.Text))
                    {
                        DefaultValueForFields();
                    }
                    
                    cp.JSProperties["cp_defaultServ"] = true;
                    break;

                case "CallbackBizPartner":
                    string filters = e.Parameter.Split('=')[1];
                    DisableFields();
                    DefaultValueForFields();

                    Session["WMSBILBizPartner"] = filters;
                    FilterSDS_Customer(filters);
                    FilterSDS_Storage(filters);

                    cp.JSProperties["cp_defaultBiz"] = true;
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

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

        private DataTable GenerateBilling()
        {
            //DataTable billing = Gears.RetriveData2("EXEC sp_GenerateWMSTransaction '" + txtContract.Text + "','" + aglBizPartnerCode.Text + "','" + dtpDateFrom.Text + "','" + dtpDateTo.Text + "','" + aglServiceType.Text + "','" + txtBillingType.Text + "','" + txtDocNumber.Text + "'", Session["ConnString"].ToString());


            DataTable dtbldetail = Gears.RetriveData2("select top 1 * from wms.Contract A INNER JOIN WMS.ContractDetail B ON A.DocNumber = B.DocNumber  where DiffCustomerCode  LIKE '%" + aglBizPartnerCode.Text + "%'   and Status IN ('ACTIVE','REVISED','RENEWED') AND EffectivityDate <= GETDATE() AND GETDATE()  BETWEEN DateFrom  AND DateTo AND ISNULL(SubmittedBy,'')!='' AND B.BeginDay!=0  AND WarehouseCode='"+ aglWarehouse.Text+"' AND ServiceType='"+aglServiceType.Text +"'", Session["ConnString"].ToString());

            DataTable billing;  
            if (dtbldetail.Rows.Count >0)
            {
                billing = Gears.RetriveData2("EXEC sp_generate_BillingNewScenario '" + aglBizPartnerCode.Text + "','" + dtpDateFrom.Text + "','" + dtpDateTo.Text + "','" + aglServiceType.Text + "','" + txtDocNumber.Text + "','" + aglWarehouse.Text + "'", Session["ConnString"].ToString());

            }
            else
            {
                billing = Gears.RetriveData2("EXEC sp_generate_Billing '" + aglBizPartnerCode.Text + "','" + dtpDateFrom.Text + "','" + dtpDateTo.Text + "','" + aglServiceType.Text + "','" + txtDocNumber.Text + "','" + aglWarehouse.Text + "'", Session["ConnString"].ToString());

            }
            
           
            gv1.DataSource = billing;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();

            return billing;            
        }
        protected void aglServiceType_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void dtpDateFrom_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDateFrom.Date = DateTime.Now;
            }
        }

        protected void dtpDateTo_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDateTo.Date = DateTime.Now;
            }
        }
        //protected void Connection_Init(object sender, EventArgs e)
        //{
        //    ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        //}

        protected void Generatebtn_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                Generatebtn.ClientVisible = false;
            }
            else
            {
                Generatebtn.ClientVisible = true;
            }            
        }

        protected void DisableFields()
        {
            if (Session["WMSBILServType"] == null || Session["WMSBILServType"] == "")
            {
                aglBizPartnerCode.ClientEnabled = false;
            }
            else
            {
                aglBizPartnerCode.ClientEnabled = true;
            }
        }
        protected void DefaultValueForFields()
        {
            SqlDataSource ds = sdsForQuery;
            //ds.SelectCommand = string.Format(" SELECT TOP 1 A.BizPartnerCode,A.DocNumber,A.BillingPeriodType,B.ServiceType,A.ProfitCenterCode,B.ServiceRate," +
            //" B.UnitOfMeasure,B.HandlingInRate,B.HandlingOutRate,B.MinHandlingIn,B.MinHandlingOut,B.MinStorage,D.EndingBalance, DateAdd(d,1,D.AsOfDate) as DateFrom, " +
            //" DATEADD(d,-DAY(DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) " +
            //" ,DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) as EndDate, " +
            //" DateAdd(d,DaysCount,DateAdd(d,1,D.AsOfDate)) as DateTo, DateAdd(d,2,D.AsOfDate) AS DailyDate, " +
            //" B.BillingType, A.WarehouseCode FROM WMS.Contract A LEFT JOIN WMS.ContractDetail B ON A.Docnumber = B.Docnumber LEFT JOIN Masterfile.WMSInventory D on A.BizPartnerCode = D.BizPartnerCode and B.ServiceType = D.ServiceType LEFT JOIN Masterfile.WMSBillingPeriod E on A.BillingPeriodType = E.BillingPeriodCode where A.BizPartnerCode = '" + aglBizPartnerCode.Text + "' AND B.ServiceType = '" + aglServiceType.Text + "' And ISNULL(A.SubmittedBy,'')!='' order by A.Docnumber desc,D.AsOfDate desc");
            ds.SelectCommand = string.Format(" SELECT TOP 1 A.BizPartnerCode,A.DocNumber,B.Period,B.ServiceType,A.ProfitCenterCode,B.ServiceRate," +
            " B.UnitOfMeasure,B.HandlingInRate,B.HandlingOutRate,B.MinHandlingIn,B.MinHandlingOut,B.MinStorage,D.EndingBalance, DateAdd(d,1,D.AsOfDate) as DateFrom, " +
            " DATEADD(d,-DAY(DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) " +
            " ,DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) as EndDate, " +
            " DateAdd(d,DaysCount,DateAdd(d,1,D.AsOfDate)) as DateTo, DateAdd(d,2,D.AsOfDate) AS DailyDate, " +
            " B.BillingType, A.WarehouseCode,D.AsOfDate FROM WMS.Contract A LEFT JOIN WMS.ContractDetail B ON A.Docnumber = B.Docnumber LEFT JOIN Masterfile.WMSInventory D on A.BizPartnerCode = D.BizPartnerCode and B.ServiceType = D.ServiceType AND A.WarehouseCode = D.WarehouseCode LEFT JOIN Masterfile.WMSBillingPeriod E on B.Period = E.BillingPeriodCode where A.BizPartnerCode = '" + aglBizPartnerCode.Text + "' AND B.ServiceType = '" + aglServiceType.Text + "' AND A.WarehouseCode = '" + aglWarehouse.Text + "'  And ISNULL(A.SubmittedBy,'')!=''   order by A.Docnumber desc,D.AsOfDate desc");
            DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            if (tran.Count > 0)
            {
                //txtContract.Value = tran[0][1].ToString();
                txtBillPeriod.Value = tran[0][2].ToString();
                _Entity.BillingPeriodType = tran[0][2].ToString();
                aglProfitCenter.Value = tran[0][4].ToString();
                speBegInv.Value = tran[0][12].ToString();
                dtpDateFrom.Text = Convert.ToDateTime(tran[0][13].ToString()).ToShortDateString();
                dtpDateTo.MinDate = Convert.ToDateTime(tran[0][13].ToString());
                //aglWarehouse.Text = tran[0][17].ToString();

                if (_Entity.BillingPeriodType == "MONTHLY") //FOR MONTHLY
                {
                    dtpDateTo.Text = Convert.ToDateTime(tran[0]["EndDate"].ToString()).ToShortDateString();
                }
                else if(_Entity.BillingPeriodType == "DAILY")
                {
                    dtpDateTo.Text = Convert.ToDateTime(tran[0]["DailyDate"].ToString()).ToShortDateString();
                }
                else //FOR SEMIMONTHLY
                {
                    if (Convert.ToDateTime(tran[0]["DateTo"].ToString()).Day == 15)
                    {
                        dtpDateTo.Text = Convert.ToDateTime(tran[0]["DateTo"].ToString()).ToShortDateString();
                    }
                    else
                    {
                        dtpDateTo.Text = Convert.ToDateTime(tran[0]["EndDate"].ToString()).ToShortDateString();
                    }
                }


                DataTable dt = Gears.RetriveData2("SELECT ISNULL(SUM(ISNULL(EndingBalance,0)),0) as EndingBalance FROM MasterFile.WMSInventory  " +
									" WHERE BizPartnerCode = '" + aglBizPartnerCode.Text + "' " +
									" AND ServiceType = '" + aglServiceType.Text + "' " +
									" and WarehouseCode='" + aglWarehouse.Text + "'" +
                                    " and AsOfDate='" + Convert.ToDateTime(tran[0]["AsofDate"].ToString()).ToShortDateString() + "'", Session["ConnString"].ToString());

                if(dt.Rows.Count >0)
                {
                    
                 
                    speBegInv.Value = dt.Rows[0]["EndingBalance"].ToString();
                }
            }
        }

        protected void GenerateBillingInfo()
        {
            DataTable getRecords = new DataTable();
            //getRecords = Gears.RetriveData2("SELECT DISTINCT RecordID FROM WMS.TransactionStorage WHERE BizPartnerCode = '" + aglBizPartnerCode.Text + "' AND ServiceType = '" + aglServiceType.Text + "' AND ISNULL(SubmittedBy,'') != '' AND ISNULL(BillNumber,'') = ''", Session["ConnString"].ToString());
            getRecords = Gears.RetriveData2("SELECT DISTINCT RecordID FROM WMS.TransactionStorage WHERE CustomerCode = '" + aglBizPartnerCode.Text + "' AND StorageCode = '" + aglServiceType.Text + "' AND ISNULL(SubmittedBy,'') != '' AND ISNULL(BillNumber,'') = '' AND WarehouseCode='"+ aglWarehouse.Text +"'", Session["ConnString"].ToString());

            if (getRecords.Rows.Count > 0)
            {
                foreach (DataRow _get in getRecords.Rows)
                {
                    DataTable generatebillinfo = new DataTable();
                    generatebillinfo = Gears.RetriveData2("EXEC sp_generate_BillingInfo '" + _get["RecordID"].ToString() + "','" + Session["Userid"].ToString() + "'", Session["ConnString"].ToString());
                }                
            }
        }

        private void FilterSDS_Customer(string filters)
        {
            string customer = filters.Split('|')[0];
            string storage = filters.Split('|')[1];

            sdsCustomer.SelectCommand = "SELECT DISTINCT A.DiffCustomerCode AS Customer, UPPER(B.Name) AS Name, C.Address, C.ContactPerson "
                + " FROM WMS.ContractDetail A INNER JOIN Masterfile.BPCustomerInfo B ON A.DiffCustomerCode = B.BizPartnerCode "
                + " INNER JOIN Masterfile.BizPartner C ON B.BizPartnerCode = C.BizPartnerCode "
                + " INNER JOIN WMS.Contract D ON A.DocNumber = D.DocNumber WHERE D.BizPartnerCode = '" + customer + "'";

            aglCustomerCode.DataBind();
        }

        private void FilterSDS_Storage(string filters)
        {
            string customer = filters.Split('|')[0];
            string storage = filters.Split('|')[1];

            sdsStorage.SelectCommand = "SELECT DISTINCT A.StorageCode AS StorageCode, UPPER(B.Description) AS StorageDesc "
                + " FROM WMS.ContractDetail A INNER JOIN Masterfile.WMSServiceType B ON A.StorageCode = B.ServiceType "
                + " INNER JOIN WMS.Contract C ON A.DocNumber = C.DocNumber WHERE C.BizPartnerCode = '" + customer + "'";

            aglStorageCode.DataBind();
        }

        protected void Customer_Init(object sender, EventArgs e)
        {
            string initfilter = (String.IsNullOrEmpty(aglBizPartnerCode.Text) ?
                (Session["WMSBILBizPartner"] == null || Session["WMSBILBizPartner"] == "" ? "" : Session["WMSBILBizPartner"].ToString())
                : aglBizPartnerCode.Text) + "|";
            FilterSDS_Customer(initfilter);
        }

        protected void Storage_Init(object sender, EventArgs e)
        {
            string initfilter = (String.IsNullOrEmpty(aglBizPartnerCode.Text) ?
                (Session["WMSBILBizPartner"] == null || Session["WMSBILBizPartner"] == "" ? "" : Session["WMSBILBizPartner"].ToString())
                : aglBizPartnerCode.Text) + "|";
            FilterSDS_Storage(initfilter);
        }
    }
}