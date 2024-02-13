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


namespace GWL
{
    public partial class frmTransactionStorage : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        private static string Connection;

        Entity.TransactionStorage _Entity = new TransactionStorage();//Calls entity odsHeader

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

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
            }

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());


            if (!IsPostBack)
            {
                Connection = (Session["ConnString"].ToString());

                txtId.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtId.Text, Session["ConnString"].ToString());

                txtId.Value = _Entity.RecordId;
                txtDocNumber.Value = _Entity.DocNumber;
                //aglBizPartnerCode.Value = _Entity.BizPartnerCode;
                //aglServiceType.Value = _Entity.ServiceType;
                //txtCustomer.Value = _Entity.BizPartnerCode;
                //txtStorage.Value = _Entity.ServiceType;
                //aglBizPartner.Value = _Entity.CustomerCode;
                //aglService.Value = _Entity.StorageCode;
                aglBizPartner.Value = _Entity.BizPartnerCode;
                aglService.Value = _Entity.ServiceType;
                txtCustomer.Value = _Entity.CustomerCode;
                txtStorage.Value = _Entity.StorageCode;
                aglWarehouse.Value = _Entity.WarehouseCode;
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                cboType.Value = _Entity.Type;
                speQty.Value = _Entity.Qty;
                speChargeableQty.Value = _Entity.ChargeableQty;
                aglUnitOfMeasure.Value = _Entity.UnitOfMeasure;
                spePalletCnt.Text = _Entity.PalletCount;
                txtBill.Text = _Entity.BillNumber;
                chkNoCharge.Value = _Entity.IsNoCharge;
                txtBillingType.Text = _Entity.BillingType;
                speStorageRate.Value = _Entity.StorageRate;
                speHandlingIn.Value = _Entity.HandlingInRate;
                speHandlingOut.Value = _Entity.HandlingOutRate;
                speMinQty.Value = _Entity.MinimumQty;
                speMinHandlingIn.Value = _Entity.MinHandlingIn;
                speMinHandlingOut.Value = _Entity.MinHandlingOut;
                dtpRRDate.Value = String.IsNullOrEmpty(_Entity.RRDate) ? null : Convert.ToDateTime(_Entity.RRDate).ToShortDateString();
                txtProdNum.Text = _Entity.ProdNum;
                dtpProdDate.Text = String.IsNullOrEmpty(_Entity.ProdDate) ? null : Convert.ToDateTime(_Entity.ProdDate).ToShortDateString();
                txtSplitBillRecID.Text = _Entity.SplitBillRecordID;
                chkNoStorage.Value = _Entity.NoStorageCharge;
                txtRefContract.Text = _Entity.RefContract;
                chkExcluded.Value = _Entity.ExcludeInBilling;
                memRemarks.Text = _Entity.Remarks;
                speInventoryDays.Value = _Entity.InventoryDays;
                speNoChargeDays.Value = _Entity.NoChargeDays;
                dtpAllocationDate.Text = String.IsNullOrEmpty(_Entity.AllocationDate) ? null : Convert.ToDateTime(_Entity.AllocationDate).ToShortDateString();
                speMinStorage.Value = _Entity.MinStorage;
                speAllocChargeable.Value = _Entity.AllocChargeable;
                speStaging.Value = _Entity.Staging;
                chkOverride.Value = _Entity.IsOverridden;
                speOverride.Value = _Entity.OverrideQty;

                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtDateSynced.Text = _Entity.DateSynced;
                txtCompleteUnload.Text = _Entity.CompleteUnload;

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
                        glcheck.ClientVisible = false;
                        break;

                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ClientEnabled = false;
                        glcheck.ClientVisible = true;
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

                    case "X":
                        view = true;
                        updateBtn.Text = "Override";
                        txtDocNumber.ClientEnabled = false;
                        glcheck.ClientVisible = false;
                        break;
                }
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = Request.QueryString["docnumber"].ToString(); 
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSTRN";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GWarehouseManagement.TransactionStorage_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
            combobox.DropDownButton.Enabled = !view;
        }
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox text = sender as ASPxCheckBox;
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D" || Request.QueryString["entry"].ToString() == "X")
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D" || Request.QueryString["entry"].ToString() == "X")
            {
                ASPxGridView grid = sender as ASPxGridView;
                e.Editor.ReadOnly = view;
            }
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
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "X")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
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
        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.RecordId = Request.QueryString["docnumber"].ToString();
            _Entity.DocNumber = txtDocNumber.Text;
            //_Entity.BizPartnerCode = String.IsNullOrEmpty(aglBizPartnerCode.Text) ? null : aglBizPartnerCode.Value.ToString();
            //_Entity.ServiceType = String.IsNullOrEmpty(aglServiceType.Text) ? null : aglServiceType.Value.ToString();
            //_Entity.BizPartnerCode = txtCustomer.Text;
            //_Entity.CustomerCode = String.IsNullOrEmpty(aglBizPartner.Text) ? null : aglBizPartner.Value.ToString();
            //_Entity.ServiceType = txtStorage.Text;
            //_Entity.StorageCode = String.IsNullOrEmpty(aglService.Text) ? null : aglService.Value.ToString();
            _Entity.CustomerCode = txtCustomer.Text;
            _Entity.BizPartnerCode = String.IsNullOrEmpty(aglBizPartner.Text) ? null : aglBizPartner.Value.ToString();
            _Entity.StorageCode = txtStorage.Text;
            _Entity.ServiceType = String.IsNullOrEmpty(aglService.Text) ? null : aglService.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouse.Text) ? null : aglWarehouse.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Type = String.IsNullOrEmpty(cboType.Text) ? null : cboType.Value.ToString();
            _Entity.Qty = String.IsNullOrEmpty(speQty.Text) ? 0 : Convert.ToDecimal(speQty.Value.ToString());
            _Entity.ChargeableQty = String.IsNullOrEmpty(speChargeableQty.Text) ? 0 : Convert.ToDecimal(speChargeableQty.Value.ToString());
            _Entity.UnitOfMeasure = String.IsNullOrEmpty(aglUnitOfMeasure.Text) ? null : aglUnitOfMeasure.Value.ToString();
            _Entity.PalletCount = spePalletCnt.Text;
            _Entity.BillNumber = txtBill.Text;
            _Entity.IsNoCharge = Convert.ToBoolean(chkNoCharge.Value.ToString());
            _Entity.BillingType = txtBillingType.Text;
            _Entity.StorageRate = String.IsNullOrEmpty(speStorageRate.Text) ? 0 : Convert.ToDecimal(speStorageRate.Value.ToString());
            _Entity.HandlingInRate = String.IsNullOrEmpty(speHandlingIn.Text) ? 0 : Convert.ToDecimal(speHandlingIn.Value.ToString());
            _Entity.HandlingOutRate = String.IsNullOrEmpty(speHandlingOut.Text) ? 0 : Convert.ToDecimal(speHandlingOut.Value.ToString());
            _Entity.MinimumQty = String.IsNullOrEmpty(speMinQty.Text) ? 0 : Convert.ToDecimal(speMinQty.Value.ToString());
            _Entity.MinHandlingIn = String.IsNullOrEmpty(speMinHandlingIn.Text) ? 0 : Convert.ToDecimal(speMinHandlingIn.Value.ToString());
            _Entity.MinHandlingOut = String.IsNullOrEmpty(speMinHandlingOut.Text) ? 0 : Convert.ToDecimal(speMinHandlingOut.Value.ToString());
            _Entity.RRDate = dtpRRDate.Text;
            _Entity.ProdNum = txtProdNum.Text;
            _Entity.ProdDate = dtpProdDate.Text;
            _Entity.SplitBillRecordID = txtSplitBillRecID.Text;
            _Entity.NoStorageCharge = Convert.ToBoolean(chkNoStorage.Value.ToString());
            _Entity.RefContract = txtRefContract.Text;
            _Entity.Remarks = memRemarks.Text;
            _Entity.InventoryDays = String.IsNullOrEmpty(speInventoryDays.Text) ? 0 : Convert.ToInt32(speInventoryDays.Value.ToString());
            _Entity.NoChargeDays = String.IsNullOrEmpty(speNoChargeDays.Text) ? 0 : Convert.ToInt32(speNoChargeDays.Value.ToString());
            _Entity.AllocationDate = dtpAllocationDate.Text;
            _Entity.MinStorage = String.IsNullOrEmpty(speMinStorage.Text) ? 0 : Convert.ToDecimal(speMinStorage.Value.ToString());
            _Entity.AllocChargeable = String.IsNullOrEmpty(speAllocChargeable.Text) ? 0 : Convert.ToInt32(speAllocChargeable.Value.ToString());
            _Entity.Staging = String.IsNullOrEmpty(speStaging.Text) ? 0 : Convert.ToInt32(speStaging.Value.ToString());
            _Entity.IsOverridden = Convert.ToBoolean(chkOverride.Value.ToString());
            _Entity.OverrideQty = String.IsNullOrEmpty(speOverride.Text) ? 0 : Convert.ToDecimal(speOverride.Value.ToString());

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
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited, ISNULL(SubmittedBy,'') AS SubmittedBy FROM WMS.TransactionStorage WHERE RecordID = '" + _Entity.RecordId + "'", Session["ConnString"].ToString());

            switch (e.Parameter)
            {
                case "Add":
                case "Update":                    

                    if (_Entity.RecordId != null && _Entity.RecordId != "" && _Entity.RecordId != "undefined")
                    {
                        //string strError = Functions.Submitted(_Entity.RecordId, "WMS.TransactionStorage", 1, Connection);
                        //if (!string.IsNullOrEmpty(strError))
                        if (!String.IsNullOrEmpty(LastEditedCheck.Rows[0]["SubmittedBy"].ToString()))
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
                    }

                    if (error == false)
                    {
                        check = true;

                        if (e.Parameter == "Add")
                        {
                            _Entity.InsertData(_Entity);
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else
                        {
                            _Entity.UpdateData(_Entity);
                            Validate();
                            cp.JSProperties["cp_message"] = "Successfully Updated!";
                            cp.JSProperties["cp_notadd"] = true;
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

                case "AddNew":
                    if (_Entity.RecordId != null && _Entity.RecordId != "" && _Entity.RecordId != "undefined")
                    {
                        if (!String.IsNullOrEmpty(LastEditedCheck.Rows[0]["SubmittedBy"].ToString()))
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
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.InsertData(_Entity);
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        cp.JSProperties["cp_addnew"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Override":
                    //if (_Entity.RecordId != null && _Entity.RecordId != "" && _Entity.RecordId != "undefined")
                    //{
                    //    if (txtHLastEditedDate.Text != (LastEditedCheck.Rows[0]["LastEdited"].ToString() == "1/1/1900 12:00:00 AM" || String.IsNullOrEmpty(LastEditedCheck.Rows[0]["LastEdited"].ToString()) ? "" : LastEditedCheck.Rows[0]["LastEdited"].ToString()))
                    //    {
                    //        cp.JSProperties["cp_message"] = "The transaction has been modified by other user(s)." + Environment.NewLine + "Changes you made will not be saved!";
                    //        cp.JSProperties["cp_success"] = true;
                    //        cp.JSProperties["cp_forceclose"] = true;
                    //        return;
                    //    }
                    //}

                    //if (error == false)
                    //{
                        check = true;
                        _Entity.OverrideData(_Entity);
                        cp.JSProperties["cp_message"] = "Successfully Overriden!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}
                    break;

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;

                case "CallbackServiceType":

                    //SqlDataSource ds = sdsDetail;

                    //ds.SelectCommand = string.Format("SELECT DISTINCT B.ServiceType, B.UnitOfMeasure FROM WMS.Contract A INNER JOIN WMS.ContractDetail B "
                    //    + " ON A.Docnumber = B.Docnumber LEFT JOIN WMS.TransactionStorage C ON A.BizPartnerCode = C.BizPartnerCode INNER JOIN Masterfile.WMSServiceType D "
                    //    + " ON B.ServiceType = D.ServiceType WHERE A.BizPartnerCode = '" + aglBizPartnerCode.Text.Trim() + "' AND B.ServiceType = '" + aglServiceType.Text.Trim()
                    //    + "' AND A.WarehouseCode = '" + aglWarehouse.Text.Trim() + "' AND A.Status != 'Closed' AND D.Type = 'STORAGE' AND ISNULL(B.BillingType,'') != '' AND ISNULL(A.SubmittedBy,'') != '' AND '" + _Entity.DocDate
                    //    + "' BETWEEN A.DateFrom AND A.DateTo");

                    //DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);

                    //if (tran.Count > 0)
                    //{
                    //    aglUnitOfMeasure.Value = tran[0][1].ToString();
                    //}

                    //sdsDetail.SelectCommand = "SELECT * FROM WMS.TransactionStorage WHERE DocNumber IS NULL";
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {

        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {

        }
        #endregion

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }
        protected void Connection_init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void addnewbtn_Load(object sender, EventArgs e)
        {
            ASPxButton addnew = sender as ASPxButton;
            if (Request.QueryString["entry"] == "N")
            {
                addnew.ClientVisible = true;
            }
            else
            {
                addnew.ClientVisible = false;
            }
        }

        protected void chkOverride_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() != "X")
            {
                //chkOverride.ReadOnly = true;
                chkOverride.ClientEnabled = false;
            }
            else
            {
                //chkOverride.ReadOnly = false;
                chkOverride.ClientEnabled = true;
            }
        }

        protected void speOverride_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() != "X")
            {
                //speOverride.ReadOnly = true;
                speOverride.ClientEnabled = false;
            }
            else
            {
                if (chkOverride.Checked == true || _Entity.IsOverridden == true)
                {
                    //speOverride.ReadOnly = false;
                    speOverride.ClientEnabled = true;
                }
                else
                {
                    //speOverride.ReadOnly = true;
                    speOverride.ClientEnabled = false;
                }
            }
        }
    }
}