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
    public partial class frmBillingMD : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.BillingMD _Entity = new BillingMD();//Calls entity odsHeader
        Entity.BillingMD.BillingDetail _EntityDetail = new BillingMD.BillingDetail();//Call entity sdsDetail

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

            gv1.KeyFieldName = "DocNumber;Date";

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;
            }
            else
            {
                view = false;
            }

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["WMSBMDServType"] = null;
                Session["WMSBMDBizPartner"] = null;

                Connection = (Session["ConnString"].ToString());

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglServiceType.Value = _Entity.ServiceType;
                aglBizPartnerCode.Value = _Entity.BizPartnerCode;
                dtpDateFrom.Text = String.IsNullOrEmpty(_Entity.DateFrom.ToString()) ? null : Convert.ToDateTime(_Entity.DateFrom.ToString()).ToShortDateString();
                dtpDateTo.Text = String.IsNullOrEmpty(_Entity.DateTo.ToString()) ? null : Convert.ToDateTime(_Entity.DateTo.ToString()).ToShortDateString();
                txtBillPeriod.Text = _Entity.BillingPeriodType;
                aglWarehouse.Value = _Entity.WarehouseCode;
                aglProfitCenter.Value = _Entity.ProfitCenterCode;
                txtBillingStatement.Value = _Entity.BillingStatement;
                aglProdNum.Value = _Entity.ProdNumber;
                speTotalAmount.Value = _Entity.TotalAmount;
                speTotalVat.Value = _Entity.TotalVat;
                speTotalGross.Value = _Entity.TotalGross;
                speBegInv.Value = _Entity.BeginningInv;

                Session["WMSBMDServType"] = _Entity.ServiceType;
                Session["WMSBMDBizPartner"] = _Entity.BizPartnerCode + "|";

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
            gparam._TransType = "WMSBMD";
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
            gparam._TransType = "WMSBMD";
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
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = String.IsNullOrEmpty(dtpDocDate.Text) ? null : Convert.ToDateTime(dtpDocDate.Text).ToShortDateString();
            _Entity.ServiceType = String.IsNullOrEmpty(aglServiceType.Text) ? null : aglServiceType.Value.ToString();
            _Entity.BizPartnerCode = String.IsNullOrEmpty(aglBizPartnerCode.Text) ? null : aglBizPartnerCode.Value.ToString();
            _Entity.DateFrom = String.IsNullOrEmpty(dtpDateFrom.Text) ? null : Convert.ToDateTime(dtpDateFrom.Text).ToShortDateString();
            _Entity.DateTo = String.IsNullOrEmpty(dtpDateTo.Text) ? null : Convert.ToDateTime(dtpDateTo.Text).ToShortDateString();
            _Entity.BillingPeriodType = txtBillPeriod.Text;
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouse.Text) ? null : aglWarehouse.Value.ToString();
            _Entity.ProfitCenterCode = String.IsNullOrEmpty(aglProfitCenter.Text) ? null : aglProfitCenter.Value.ToString();
            _Entity.BillingStatement = txtBillingStatement.Text;
            _Entity.ProdNumber = aglProdNum.Text;
            _Entity.TotalAmount = String.IsNullOrEmpty(speTotalAmount.Text) ? 0 : Convert.ToDecimal(speTotalAmount.Text);
            _Entity.TotalVat = String.IsNullOrEmpty(speTotalVat.Text) ? 0 : Convert.ToDecimal(speTotalVat.Text);
            _Entity.TotalGross = String.IsNullOrEmpty(speTotalGross.Text) ? 0 : Convert.ToDecimal(speTotalGross.Text);
            _Entity.BeginningInv = String.IsNullOrEmpty(speBegInv.Text) ? 0 : Convert.ToDecimal(speBegInv.Text);

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

                    string strError = Functions.Submitted(_Entity.DocNumber, "WMS.Billing", 1, Session["ConnString"].ToString());
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
                                _EntityDetail.Amount = Convert.ToDecimal(Convert.IsDBNull(dtRow["Amount"]) ? 0 : dtRow["Amount"]);
                                _EntityDetail.Vat = Convert.ToDecimal(Convert.IsDBNull(dtRow["Vat"]) ? 0 : dtRow["Vat"]);
                                _EntityDetail.GrossAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossAmount"]) ? 0 : dtRow["GrossAmount"]);
                                _EntityDetail.ProdNum = dtRow["ProdNum"].ToString();
                                if (!Convert.IsDBNull(dtRow["ProdDate"]) && !String.IsNullOrEmpty(dtRow["ProdDate"].ToString()))
                                {
                                    _EntityDetail.ProdDate = Convert.ToDateTime(dtRow["ProdDate"].ToString());
                                }
                                if (!Convert.IsDBNull(dtRow["RRDate"]) && !String.IsNullOrEmpty(dtRow["RRDate"].ToString()))
                                {
                                    _EntityDetail.RRDate = Convert.ToDateTime(dtRow["RRDate"].ToString());
                                }
                                _EntityDetail.RRNum = dtRow["RRNum"].ToString();
                                _EntityDetail.StorageRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["StorageRate"]) ? 0 : dtRow["StorageRate"]);
                                _EntityDetail.MinimumQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinimumQty"]) ? 0 : dtRow["MinimumQty"]);
                                _EntityDetail.NoStorageCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["NoStorageCharge"]) ? false : dtRow["NoStorageCharge"]);
                                _EntityDetail.InventoryDays = Convert.ToInt32(Convert.IsDBNull(dtRow["InventoryDays"]) ? 0 : dtRow["InventoryDays"]);
                                _EntityDetail.NoChargeDays = Convert.ToInt32(Convert.IsDBNull(dtRow["NoChargeDays"]) ? 0 : dtRow["NoChargeDays"]);
                                _EntityDetail.RefContract = dtRow["RefContract"].ToString();
                                _EntityDetail.ExcludeInBilling = Convert.ToBoolean(Convert.IsDBNull(dtRow["ExcludeInBilling"]) ? false : dtRow["ExcludeInBilling"]);
                                _EntityDetail.Remarks = dtRow["Remarks"].ToString();
                                _EntityDetail.RefRecordID = dtRow["RefRecordID"].ToString();
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
                    DisableFields();
                    if (String.IsNullOrEmpty(aglBizPartnerCode.Text) ||
                    String.IsNullOrEmpty(dtpDateFrom.Text) ||
                    String.IsNullOrEmpty(dtpDateTo.Text) ||
                    String.IsNullOrEmpty(aglServiceType.Text) ||
                    String.IsNullOrEmpty(txtDocNumber.Text) ||
                    String.IsNullOrEmpty(aglProdNum.Text))
                    {
                        cp.JSProperties["cp_noparameter"] = true;
                        cp.JSProperties["cp_noparametermsg"] = "Please check if the transaction has the following details:" + Environment.NewLine +
                            (String.IsNullOrEmpty(aglBizPartnerCode.Text) ? "    Business Partner" + Environment.NewLine : "") +
                            (String.IsNullOrEmpty(aglProdNum.Text) ? "    Prod. Num" + Environment.NewLine : "") +
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
                    Session["WMSBMDServType"] = null;
                    Session["WMSBMDServType"] = aglServiceType.Text;
                    DisableFields();
                    if (!String.IsNullOrEmpty(aglBizPartnerCode.Text) && !String.IsNullOrEmpty(aglProdNum.Text))
                    {
                        DefaultValueForFields();
                    }
                    
                    cp.JSProperties["cp_defaultServ"] = true;
                    break;

                case "CallbackBizPartner":
                    string filters = e.Parameter.Split('=')[1];
                    DisableFields();
                    DefaultValueForFields();

                    Session["WMSBMDBizPartner"] = filters;
                    break;

                case "CallbackProdNum":
                    DisableFields();
                    if (!String.IsNullOrEmpty(aglBizPartnerCode.Text) && !String.IsNullOrEmpty(aglServiceType.Text))
                    {
                        DefaultValueForFields();
                    }
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
            DataTable billing = Gears.RetriveData2("EXEC sp_generate_BillingMD '" + aglBizPartnerCode.Text + "','" + dtpDateFrom.Text + "','" + dtpDateTo.Text + "','" + aglServiceType.Text + "','" + aglProdNum.Text.Trim() + "','" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

            gv1.DataSource = billing;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();

            return billing;            
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
            if (Session["WMSBMDServType"] == null || Session["WMSBMDServType"] == "")
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
            ds.SelectCommand = string.Format(" SELECT TOP 1 A.BizPartnerCode,A.DocNumber,B.Period,B.ServiceType,A.ProfitCenterCode,B.ServiceRate," +
            " B.UnitOfMeasure,B.HandlingInRate,B.HandlingOutRate,B.MinHandlingIn,B.MinHandlingOut,B.MinStorage,D.EndingBalance, DateAdd(d,1,D.AsOfDate) as DateFrom, " +
            " DATEADD(d,-DAY(DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) " +
            " ,DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) as EndDate, " +
            " DateAdd(d,DaysCount,DateAdd(d,1,D.AsOfDate)) as DateTo, DateAdd(d,2,D.AsOfDate) AS DailyDate, B.BillingType, A.WarehouseCode FROM WMS.Contract A INNER JOIN " +
            " WMS.ContractDetail B ON A.Docnumber = B.Docnumber LEFT JOIN Masterfile.WMSInventory D on A.BizPartnerCode = D.BizPartnerCode and B.ServiceType = D.ServiceType  " +
            " LEFT JOIN Masterfile.WMSBillingPeriod E ON B.Period = E.BillingPeriodCode where A.BizPartnerCode = '" + aglBizPartnerCode.Text + "' AND B.ServiceType = '" + aglServiceType.Text +
            "' And ISNULL(A.SubmittedBy,'')!='' AND REPLACE(REPLACE(REPLACE(D.ProdNum,'PROD',''),'#',''),' ','') = '" + aglProdNum.Text.Replace("PROD", "").Replace("#", "").Replace(" ", "") + "' AND Year(A.DocDate)='" + Convert.ToDateTime(dtpDocDate.Text).Year + "'  AND Year(A.DocDate)<='" + Convert.ToDateTime(dtpDocDate.Text).ToShortDateString() + "' ORDER BY A.Docnumber DESC, D.AsOfDate DESC");
            DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            if (tran.Count > 0)
            {
                txtBillPeriod.Value = tran[0][2].ToString();
                _Entity.BillingPeriodType = tran[0][2].ToString();
                aglProfitCenter.Value = tran[0][4].ToString();
                speBegInv.Value = tran[0][12].ToString();
                dtpDateFrom.Text = Convert.ToDateTime(tran[0][13].ToString()).ToShortDateString();
                dtpDateTo.MinDate = Convert.ToDateTime(tran[0][13].ToString());
                aglWarehouse.Value = tran[0][17].ToString();

                if (_Entity.BillingPeriodType == "MONTHLY")
                {
                    dtpDateTo.Text = Convert.ToDateTime(tran[0]["EndDate"].ToString()).ToShortDateString();
                }
                else if (_Entity.BillingPeriodType == "DAILY")
                {
                    dtpDateTo.Text = Convert.ToDateTime(tran[0]["DailyDate"].ToString()).ToShortDateString();
                }
                else
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
            }
            else
            {
                ds.SelectCommand = string.Format(" SELECT TOP 1 A.BizPartnerCode,A.DocNumber,B.Period,B.ServiceType,A.ProfitCenterCode,B.ServiceRate," +
                " B.UnitOfMeasure,B.HandlingInRate,B.HandlingOutRate,B.MinHandlingIn,B.MinHandlingOut,B.MinStorage,D.EndingBalance, DateAdd(d,1,D.AsOfDate) as DateFrom, " +
                " DATEADD(d,-DAY(DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) " +
                " ,DATEADD(d,32,CONVERT(date,CONVERT(varchar(max),MONTH(DateAdd(d,1,D.AsOfDate)))+'/1/'+CONVERT(varchar(max),YEAR(DateAdd(d,1,D.AsOfDate)))))) as EndDate, " +
                " DateAdd(d,DaysCount,DateAdd(d,1,D.AsOfDate)) as DateTo, DateAdd(d,2,D.AsOfDate) AS DailyDate, B.BillingType, A.WarehouseCode FROM WMS.Contract A INNER JOIN " +
                " WMS.ContractDetail B ON A.Docnumber = B.Docnumber LEFT JOIN Masterfile.WMSInventory D on A.BizPartnerCode = D.BizPartnerCode and B.ServiceType = D.ServiceType  " +
                " LEFT JOIN Masterfile.WMSBillingPeriod E ON B.Period = E.BillingPeriodCode where A.BizPartnerCode = '" + aglBizPartnerCode.Text + "' AND B.ServiceType = '" + aglServiceType.Text +
                "' And ISNULL(A.SubmittedBy,'')!='' ORDER BY A.Docnumber DESC, D.AsOfDate DESC");
                DataView tran1 = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                if (tran1.Count > 0)
                {
                    txtBillPeriod.Value = tran1[0][2].ToString();
                    _Entity.BillingPeriodType = tran1[0][2].ToString();
                }            
            }
        }

        protected void GenerateBillingInfo()
        {
            DataTable getRecords = new DataTable();
            getRecords = Gears.RetriveData2("SELECT DISTINCT RecordID FROM WMS.TransactionStorage WHERE CustomerCode = '" + aglBizPartnerCode.Text + "' AND StorageCode = '" + aglServiceType.Text + "' AND ISNULL(SubmittedBy,'') != '' AND ISNULL(BillNumber,'') = ''", Session["ConnString"].ToString());

            if (getRecords.Rows.Count > 0)
            {
                foreach (DataRow _get in getRecords.Rows)
                {
                    DataTable generatebillinfo = new DataTable();
                    generatebillinfo = Gears.RetriveData2("EXEC sp_generate_BillingInfo '" + _get["RecordID"].ToString() + "','" + Session["Userid"].ToString() + "'", Session["ConnString"].ToString());
                }                
            }
        }
    }
}