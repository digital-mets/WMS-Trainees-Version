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
using DevExpress.Data.Filtering;
using GearsWarehouseManagement;

namespace GWL
{
    public partial class frmOutbound : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Outbound _Entity = new Outbound();//Calls entity odsHeader
        Entity.Outbound.OutboundDetail _EntityDetail = new Outbound.OutboundDetail();//Call entity sdsDetail
        private DataTable getDetail;
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

            gv1.DataSource = null;


            dtpComplete.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));


            if (!IsPostBack)
            {

                sdsWarehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";



                Session["customoutbound"] = null;
                Session["picklistdetail1"] = null;
                Session["FilterExpression"] = null;

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglWarehouseCode.Value = _Entity.WarehouseCode.ToString();
                aglCustomer.Value = _Entity.Customer.ToString();
                dtpStart.Date = String.IsNullOrEmpty(_Entity.Startload) ? DateTime.MinValue : Convert.ToDateTime(_Entity.Startload);
                dtpComplete.Date = String.IsNullOrEmpty(_Entity.Completeload) ? DateTime.MinValue : Convert.ToDateTime(_Entity.Completeload);
                txtContainer.Value = _Entity.ContainerNumber.ToString();
                txtSealNumber.Value = _Entity.SealNumber.ToString();
                txtOtherReference.Value = _Entity.OtherReference.ToString();

                chkIsNoCharge.Value = Convert.ToBoolean(_Entity.IsNoCharge.ToString());
                //txtDeliverTo.Value = _Entity.DeliverTo.ToString();
                txtDeliveryAddress.Value = _Entity.DeliveryAddress.ToString();
                txtTruckingCo.Value = _Entity.TruckingCo.ToString();
                txtPlateNumber.Value = _Entity.PlateNumber.ToString();
                DataTable dtfullname = Gears.RetriveData2("Select FullName from it.users where UserID='" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
                DataRow _ret = dtfullname.Rows[0];
                txtDocumentstaff.Text = string.IsNullOrEmpty(_Entity.DocumentStaff) ? _ret["FullName"].ToString().Replace(".", " ") : _Entity.DocumentStaff;
                txtwarehousechck.Value = _Entity.WarehouseChecker.ToString();

                dtpAllocationDate.Date = String.IsNullOrEmpty(_Entity.AllocationDate) ? DateTime.Now : Convert.ToDateTime(_Entity.AllocationDate);
                // Added By Luis Genel T. Edpao 12/14/2015
                //spinSetBox.Value = _Entity.SetBox.ToString();
                //spinNetWeight.Value = _Entity.NetWeight.ToString();
                //spinNetVolume.Value = _Entity.NetVolume.ToString();
                //txtSMDeptSub.Value = _Entity.SMDeptSub.ToString();
                //txtModeofPayment.Value = _Entity.ModeofPayment.ToString();
                //txtModeofShipment.Value = _Entity.ModeofShipment.ToString();
                //txtBrand.Value = _Entity.Brand.ToString();
                //spinTotalAmount.Value = _Entity.TotalAmount.ToString();
                //spinDeclaredValue.Value = _Entity.DeclaredValue.ToString();
                //spinTotalQty.Value = _Entity.TotalQty.ToString();
                //txtForwarderTR.Value = _Entity.ForwarderTR.ToString();
                //txtConAddr.Text = _Entity.ConsigneeAddr;
                txtConsignee.Text = _Entity.Consignee;
                txtOvertime.Text = _Entity.Overtime;
                txtTBSB.Text = _Entity.SuppliedBy;
                txtTProvided.Text = _Entity.TruckingPro;
                txtManpower.Value = String.IsNullOrEmpty(_Entity.NOmanpower) ? 0 : Convert.ToDecimal(_Entity.NOmanpower);
                txtAddpower.Text = _Entity.AddtionalManpower;
                txtReqCoDept.Text = _Entity.CompanyDept;
                txtShipType.Text = _Entity.ShipmentType;
                txtRefDoc.Text = _Entity.RefDoc;
                txtTruckType.Text = _Entity.TruckType;
                //txtHTruckingCo.Text = _Entity.TrackingNO;
 



                txtStorageType.Text = _Entity.StorageType;

                txtDriver.Value = _Entity.Driver.ToString();
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

                //Truck Transaction
                ArrivalTime.Text = _Entity.ArrivalTime;
                StartUnloading.Text = _Entity.Startloading;
                CompleteUnloading.Text = _Entity.Completeloading;
                DepartureTime.Text = _Entity.DepartureTime;
                HoldReason.Text = _Entity.HoldReason;
                HoldRemarks.Text = _Entity.HoldRemarks;
                HoldDate.Text = _Entity.HoldDate;
                UnHoldDate.Text = _Entity.UnHoldDate;
                HoldDuration.Text = _Entity.HoldDuration;
                Status.Text = _Entity.Status;
                DwellTime.Text = _Entity.DwellTime;
                CheckingEnd.Text = _Entity.CheckingEnd;
                EndProcessing.Text = _Entity.EndProcessing;
                StartProcessing.Text = _Entity.StartProcessing;
                CheckingStart.Text = _Entity.CheckingStart;
                DockingDoor.Text = _Entity.DockingDoor;
                DockingTime.Text = _Entity.DockingTime;
                HoldStatus.Text = _Entity.HoldStatus;
                CancelledBy.Text = _Entity.CancelledBy;
                CancelledDate.Text = _Entity.CancelledDate;

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Update";
                        glPicklist.Visible = true;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                gv1.Settings.VerticalScrollableHeight = 200;

                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //gv1.DataSourceID = null; TLV
                // popup.ShowOnPageLoad = false;

                //}

                //if (Request.QueryString["entry"].ToString() == "E")
                //{

                //if (String.IsNullOrEmpty(aglCustomer.Text))
                //{
                //    glPicklist.ClientEnabled = true;
                //   
                //}
                //else
                //{
                //    glPicklist.ClientVisible = false;
                //    Generatebtn.ClientVisible = false;
                //}

                //gv1.DataSourceID = "odsDetail"; TLV this line only
                //}

                //if (Request.QueryString["entry"].ToString() == "V")
                //{
                //gv1.DataSourceID = "odsDetail"; TLV
                //}


                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = null;
                //}

                DataTable dtbldetail = Gears.RetriveData2("SELECT DocNumber FROM WMS.OutboundDetail Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                if (dtbldetail.Rows.Count > 0)
                {
                    gv1.DataSourceID = "odsDetail";
                    gv3.DataSourceID = null;
                    getDetail = Gears.RetriveData2("EXEC [dbo].[sp_WMS_Variance] 'WMSOUT','" + txtDocNumber.Text + "' ", Session["ConnString"].ToString());
                    gv3.DataSource = getDetail;
                    gv3.DataBind();
                }
                else
                {
                    gv1.DataSourceID = null;
                    gv3.DataSourceID = null;
                    getDetail = Gears.RetriveData2("EXEC [dbo].[sp_WMS_Variance] 'WMSOUT','" + txtDocNumber.Text + "' ", Session["ConnString"].ToString()); 
                    gv3.DataSource = getDetail;
                    gv3.DataBind();
                    glPicklist.ClientEnabled = true;
                    Generatebtn.ClientVisible = true;
                    gv1.KeyFieldName = "LineNumber;PicklistNo";
                }
                //gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail":"sdsDetail");
            }

            //if (gv1.DataSource != null)
            //{
            //gv1.DataSourceID = null; 
            //} TLV
            glcheck.Checked = true;

        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._Connection = Session["ConnString"].ToString();
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSOUT";
            string strresult = GWarehouseManagement.Outbound_Validate(gparam);
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
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;

            //if (Request.QueryString["entry"].ToString() == "E")
            //{
            //    if (!String.IsNullOrEmpty(aglCustomer.Text))
            //    {
            //        aglCustomer.DropDownButton.Enabled = false;
            //        aglCustomer.ReadOnly = true;
            //    }
            //}
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
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            //    if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //    {
            //        ASPxGridView grid = sender as ASPxGridView;
            //        grid.SettingsBehavior.AllowGroup = false;
            //        grid.SettingsBehavior.AllowSort = false;
            //        e.Editor.ReadOnly = view;
            //    }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
            dtpDocDate.DropDownButton.Enabled = false;
            dtpDocDate.ReadOnly = true;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
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
            //if (Request.QueryString["entry"] == "N")
            //{
            //    if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            var selectedValues = itemcode;
            CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
            sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["FilterExpression"] = sdsItemDetail.FilterExpression;
            grid.DataSourceID = "sdsItemDetail";
            grid.DataBind();

            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, column) != null)
                    if (grid.GetRowValues(i, column).ToString() == val)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, column).ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouseCode.Text) ? null : aglWarehouseCode.Value.ToString();
            _Entity.Customer = String.IsNullOrEmpty(aglCustomer.Text) ? null : aglCustomer.Value.ToString();
            _Entity.Startload = dtpStart.Text;
            _Entity.Completeload = dtpComplete.Text;
            _Entity.ContainerNumber = txtContainer.Text;
            _Entity.SealNumber = txtSealNumber.Text;
            _Entity.OtherReference = txtOtherReference.Text;
            //_Entity.DeliverTo = String.IsNullOrEmpty(txtDeliverTo.Text) ? null : txtDeliverTo.Text;
            _Entity.DeliveryAddress = String.IsNullOrEmpty(txtDeliveryAddress.Text) ? null : txtDeliveryAddress.Value.ToString();
            _Entity.TruckingCo = String.IsNullOrEmpty(txtTruckingCo.Text) ? null : txtTruckingCo.Text;
            _Entity.PlateNumber = String.IsNullOrEmpty(txtPlateNumber.Text) ? null : txtPlateNumber.Text;
            _Entity.Driver = String.IsNullOrEmpty(txtDriver.Text) ? null : txtDriver.Text;
            _Entity.WarehouseChecker = String.IsNullOrEmpty(txtwarehousechck.Text) ? null : txtwarehousechck.Text;
            _Entity.DocumentStaff = String.IsNullOrEmpty(txtDocumentstaff.Text) ? null : txtDocumentstaff.Text;

            _Entity.IsNoCharge = Convert.ToBoolean(chkIsNoCharge.Value.ToString());

            _Entity.AllocationDate = dtpAllocationDate.Text;
            _Entity.StorageType = txtStorageType.Text;
            // Added By Luis Genel T. Edpao     12/14/2015
            //_Entity.SetBox = Convert.ToDecimal(spinSetBox.Value);
            //_Entity.NetWeight = Convert.ToDecimal(spinNetWeight.Value);
            //_Entity.NetVolume = Convert.ToDecimal(spinNetVolume.Value);
            //_Entity.SMDeptSub = txtSMDeptSub.Text;
            //_Entity.ModeofPayment = txtModeofPayment.Text;
            //_Entity.ModeofShipment = txtModeofShipment.Text;
            //_Entity.Brand = txtBrand.Text;
            //_Entity.TotalAmount = Convert.ToDecimal(spinTotalAmount.Value);
            //_Entity.DeclaredValue = Convert.ToDecimal(spinDeclaredValue.Value);
            //_Entity.TotalQty = Convert.ToDecimal(spinTotalQty.Value);
            //_Entity.ForwarderTR = txtForwarderTR.Text;
            _Entity.Consignee = txtConsignee.Text;
            //_Entity.ConsigneeAddr = txtConAddr.Text;
            _Entity.Overtime = txtOvertime.Text;
            _Entity.SuppliedBy = txtTBSB.Text;
            _Entity.TruckingPro = txtTProvided.Text;
            _Entity.NOmanpower = String.IsNullOrEmpty(txtManpower.Value.ToString()) ? Convert.ToString(0) : txtManpower.Value.ToString();
            _Entity.AddtionalManpower = txtAddpower.Text;
            _Entity.CompanyDept = txtReqCoDept.Text;
            _Entity.ShipmentType = txtShipType.Text;
            _Entity.RefDoc = txtRefDoc.Text;
            _Entity.TruckType = String.IsNullOrEmpty(txtTruckType.Text) ? null : txtTruckType.Value.ToString();
            //_Entity.TrackingNO=txtHTruckingCo.Text ; 


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

            //Truck Transaction
            //_Entity.ArrivalTime = ArrivalTime.Text;
            //_Entity.StartUnloading = StartUnloading.Text;
            //_Entity.CompleteUnloading = CompleteUnloading.Text;
            //_Entity.DepartureTime = DepartureTime.Text;
            //_Entity.HoldReason = HoldReason.Text;
            //_Entity.HoldRemarks = HoldRemarks.Text;
            //_Entity.HoldDate = HoldDate.Text;
            //_Entity.UnHoldDate = UnHoldDate.Text;
            //_Entity.HoldDuration = HoldDuration.Text;
            //_Entity.Status = Status.Text;
            //_Entity.DwellTime = DwellTime.Text;
            //_Entity.CheckingEnd = CheckingEnd.Text;
            //_Entity.EndProcessing = EndProcessing.Text;
            //_Entity.StartProcessing = StartProcessing.Text;
            //_Entity.CheckingStart = CheckingStart.Text;
            //_Entity.DockingTime = DockingTime.Text;
            //_Entity.HoldStatus = HoldStatus.Text;
            //_Entity.CancelledBy = CancelledBy.Text;
            //_Entity.CancelledDate = CancelledDate.Text;

            _Entity.Transtype = Request.QueryString["transtype"].ToString();
            string strError;
            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();

                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.OutBound", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        _Entity.SubsiEntry(txtDocNumber.Text);
                        if (Session["Datatable"] == "1")
                        {
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Update":

                    gv1.UpdateEdit();

                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.OutBound", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        //cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Delete":
                    //GetSelectedVal(); 
                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.OutBound", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;


                    }

                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.Deletedata(_Entity);
                    Gears.RetriveData2("DELETE FROM WMS.CountSheetsubsi Where TransDoc = '" + txtDocNumber.Text + "' and TransType='WMSOUT'", Session["ConnString"].ToString());
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    gv1.DataSource = null;
                    break;

                case "Generate":
                    GetSelectedVal();
                    TruckingDetails();
                    break;

                case "customercode":
                    sdsPicklist.SelectParameters["customercode"].DefaultValue = aglCustomer.Text;
                    break;


            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            //if (error == false && check == false)
            //{
            //    foreach (GridViewColumn column in gv1.Columns)
            //    {
            //        GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //        if (dataColumn == null) continue;
            //        if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber")
            //        {
            //            e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //        }
            //    }

            //    //Checking for non existing Codes..
            //    ItemCode = e.NewValues["ItemCode"].ToString();
            //    ColorCode = e.NewValues["ColorCode"].ToString();
            //    ClassCode = e.NewValues["ClassCode"].ToString();
            //    SizeCode = e.NewValues["SizeCode"].ToString();
            //    DataTable item = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'");
            //    if (item.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //    }
            //    DataTable color = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
            //    if (color.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //    }
            //    DataTable clss = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
            //    if (clss.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //    }
            //    DataTable size = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
            //    if (size.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
            //    }

            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
            }
            //}
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

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0], values.Keys[1] };
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
                    object[] keys = { values.Keys[0], values.Keys[1] };
                    DataRow row = source.Rows.Find(keys);
                    row["PicklistNo"] = values.NewValues["PicklistNo"];
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["BulkQty"] = values.NewValues["BulkQty"];
                    row["BaseQty"] = values.NewValues["BaseQty"];
                    row["PicklistQty"] = values.NewValues["PicklistQty"];
                    row["BulkUnit"] = values.NewValues["BulkUnit"];
                    row["Unit"] = values.NewValues["Unit"];
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

                Gears.RetriveData2("DELETE FROM WMS.OutboundDetail Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.PicklistNo = dtRow["PicklistNo"].ToString();
                    _EntityDetail.LineNumber = dtRow["LineNumber"].ToString();
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.BulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BulkQty"]) ? 0 : dtRow["BulkQty"]);
                    _EntityDetail.BaseQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BaseQty"]) ? 0 : dtRow["BaseQty"]);
                    _EntityDetail.PicklistQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["PicklistQty"]) ? 0 : dtRow["PicklistQty"]);
                    _EntityDetail.BulkUnit = dtRow["BulkUnit"].ToString();
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.AddOutboundDetail(_EntityDetail);
                }
            }
        }
        #endregion

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {
            SqlDataSource ds = sdsBizPartnerCus;

            ds.SelectCommand = string.Format("SELECT DeliveryAddress FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + e.Parameter + "'");

            DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            if (biz.Count > 0)
            {
                txtDeliveryAddress.Text = biz[0][0].ToString();
            }
        }

        protected void aglcustomerinit(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
            if (Session["customoutbound"] != null)
            {
                gridLookup.GridView.DataSource = sdsPicklist;
                sdsPicklist.FilterExpression = Session["customoutbound"].ToString();
            }
        }

        public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string whcode = e.Parameters.Split('|')[0]; //SetPalletID column name

            if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback
            string customer = e.Parameters.Split('|')[1]; //SetPalletID column name

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = null;
            CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { whcode });
            CriteriaOperator selectionCriteria1 = new InOperator("CustomerCode", new string[] { customer });
            sdsPicklist.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, selectionCriteria1)).ToString();
            Session["customoutbound"] = sdsPicklist.FilterExpression;
            grid.DataSource = sdsPicklist;
            grid.DataBind();
        }
        protected void gv1_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["picklistdetail1"] = null;
            //}

            //if (Session["picklistdetail1"] != null)
            //{
            //    gv1.DataSource = sdsPicklistDetail;
            //    sdsPicklistDetail.FilterExpression = Session["picklistdetail"].ToString();
            //    //gridview.DataSourceID = null;
            //}
        }

        private DataTable GetSelectedVal()
        {
            gv1.DataSource = null;
            DataTable dt = new DataTable();


            sdsPicklistDetail.SelectCommand = "SELECT A.DocNumber,A.DocNumber AS PicklistNo,LineNumber,A.ItemCode,C.FullDesc,ColorCode,ClassCode,SizeCode,BulkQty, BaseQty, Qty AS PicklistQty, BulkUnit,Unit,StatusCode,BarcodeNo,A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7,A.Field8,A.Field9, CustomerCode FROM WMS.PicklistDetail A INNER JOIN WMS.Picklist B ON A.DocNumber = B.DocNumber left join masterfile.item C on a.ItemCode = c.ItemCode WHERE ISNULL(OutBoundNo,'')='' AND ISNULL(SubmittedBy,'')!='' AND A.Docnumber ='" + glPicklist.Text + "'";

            //string[] selectedValues = glPicklist.Text.Split(';');
            //CriteriaOperator selectionCriteria = new InOperator(glPicklist.KeyFieldName, selectedValues);
            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            // Session["picklistdetail1"] = sdsPicklistDetail;
            gv1.DataSource = sdsPicklistDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session["Datatable"] = "1";

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
            dt.Columns["LineNumber"],dt.Columns["PicklistNo"]};

            glPicklist.ClientEnabled = false;

            return dt;
        }
        private void TruckingDetails()
        {
            string pick = glPicklist.Text;
            string refpick = "";

            int cnt = pick.LastIndexOf(";");

            if (cnt > 0)
            {
                refpick = pick.Substring(cnt + 1, pick.Length - (cnt + 1));
            }
            else
            {
                refpick = pick;
            }

            DataTable ret = Gears.RetriveData2("SELECT StorageType FROM WMS.Picklist WHERE DocNumber = '" + refpick.Trim() + "'", Session["ConnString"].ToString());

            if (ret.Rows.Count > 0)
            {
                //txtTruckingCo.Text = ret.Rows[0]["TruckingCo"].ToString();
                //txtPlateNumber.Text = ret.Rows[0]["PlateNumber"].ToString(); 
                //txtDriver.Text = ret.Rows[0]["Driver"].ToString();
                txtStorageType.Text = ret.Rows[0]["StorageType"].ToString();
            }

        }

        protected void aglCustomer_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void aglCustomer_TextChanged(object sender, EventArgs e)
        {
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
                dtpAllocationDate.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();



        }


    }
}