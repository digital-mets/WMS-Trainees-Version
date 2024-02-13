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


namespace GWL
{

    #region Changes
    //-- =============================================
    //-- Edit By:		Luis Genel T. Edpao
    //-- Edit Date:     12/14/2015
    //-- Description:	Added ICNTotalQty (Header), ICNQty(Detail), Status(Detail)
    //-- =============================================
    #endregion

    public partial class frmInbound : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Inbound _Entity = new Inbound();//Calls entity odsHeader
        Entity.Inbound.InboundDetail _EntityDetail = new Inbound.InboundDetail();//Call entity sdsDetail
        private DataTable getDetail;
        private DataTable getDetailCOMI;

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            if (!string.IsNullOrEmpty(gvCustomer.Text))
            {
                Session["FilterCus"] = gvCustomer.Text;
            }
            if (!string.IsNullOrEmpty(gvWarehouse.Text))
            {
                Session["FilterWH"] = gvWarehouse.Text;
            }

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

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            //2016-10-05 emc add filter for ICN document
            ICNNumber.SelectCommand = "SELECT DocNumber FROM WMS.ICN WHERE ISNULL(SubmittedBy,'') != '' AND Status = 'N' AND CustomerCode = '" + gvCustomer.Text + "' and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0 ";

            dtpComplete.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));

            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                Session["icndetail"] = null;
                Session["FilterCus"] = null;
                Session["FilterWH"] = null;

                switch (Request.QueryString["entry"].ToString())
                {

                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                sdsWarehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";


                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                gvPlant.Text = _Entity.Plant;
                gvCustomer.Value = _Entity.CustomerCode;
                Session["FilterCus"] = gvCustomer.Text;
                gvWarehouse.Value = _Entity.WarehouseCode;
                Session["FilterWH"] = gvWarehouse.Text;
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                txtDelivery.Text = String.IsNullOrEmpty(_Entity.Delivery) ? "" : Convert.ToDateTime(_Entity.Delivery).ToShortDateString();
                if (String.IsNullOrEmpty(_Entity.ICNNumber))
                {
                    gvICN.ClientEnabled = true;
                    Generatebtn.ClientVisible = true;
                }
                DataTable dticncheck = Gears.RetriveData2("Select top 1 Status from WMS.InboundDetail where Docnumber='" + txtDocNumber.Text + "' and Status='S'", Session["ConnString"].ToString());

                //2016-10-05 emc add filter for ICN document
                if (!string.IsNullOrEmpty(_Entity.SubmittedBy) || dticncheck.Rows.Count > 0)
                {
                    ICNNumber.SelectCommand = "SELECT DocNumber FROM WMS.ICN WHERE ISNULL(SubmittedBy,'') != '' AND CustomerCode = '" + gvCustomer.Text + "' and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";

                }
                else
                {
                    ICNNumber.SelectCommand = "SELECT DocNumber FROM WMS.ICN WHERE ISNULL(SubmittedBy,'') != '' AND Status = 'N' AND CustomerCode = '" + gvCustomer.Text + "' and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";

                }

                gvICN.Value = _Entity.ICNNumber;
                txtTranType.Value = _Entity.TranType;
                txtDRNo.Value = _Entity.DRNumber;
                //txtTemp.Value = _Entity.ContainerTemp;
                //txtTrucker.Value = _Entity.Trucker;
                txtDriver.Value = _Entity.Driver;
                txtPlate.Value = _Entity.PlateNo;
                txtConsignee.Value = _Entity.Consignee;
                txtConAddress.Value = _Entity.ConAddress;
                txtTruckName.Value=_Entity.TrackingNo;
                txtContainerNo.Value=_Entity.ContainNumber;
                txtTruckType.Value = _Entity.TruckT;
                txtSealNo.Value = _Entity.SealNo;
                txtProv.Value = _Entity.TruckProv;
                txtReqDept.Value = _Entity.ReqDept;
                txtShipmentType.Value = _Entity.ShipmentType;
                txtRefDoc.Value = _Entity.RefDoc;
                txtStatus.Value = _Entity.Stat;
                txtOvertime.Value = _Entity.Overtime;
                //txtDelivery.Text = _Entity.Delivery;
                txtAddManpower.Value = _Entity.AddManpower;
                txtManpowerNo.Value = _Entity.NOmanpower;
                txtTBSB.Value = _Entity.TBSB;
                //txtContainerNo.Value = _Entity.ContainerNo;
                //txtConDept.Value = _Entity.ContactingDept;
                txtInvoice.Value = _Entity.InvoiceNo;
                txtBatch.Value = _Entity.Batch;
                txtCon.Value = _Entity.Consigne;
                CheckIntExt.Value = _Entity.InternalExternal;
                //gvSupplier.Value = _Entity.Supplier;
                glStorageType.Value = _Entity.StorageType;
                txtProdNo.Text = _Entity.ProdNumber;
                //txtAWB.Value = _Entity.AWB;

                DataTable dtfullname = Gears.RetriveData2("Select UserName from it.users where UserID='" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
                DataRow _ret = dtfullname.Rows[0];
                txtStaff.Text = string.IsNullOrEmpty(_Entity.DocumentationStaff) ? _ret["UserName"].ToString().Replace(".", " ") : _Entity.DocumentationStaff;
                txtChecker.Value = _Entity.WarehouseChecker;
                //txtGuard.Value = _Entity.GuardOnDuty;
                //txtRep.Value = _Entity.CustomerRepresentative;
                //txtOfficer.Value = _Entity.ApprovingOfficer;

                spinICNTotalQty.Value = _Entity.ICNTotalQty;

                chkIsNoCharge.Value = _Entity.IsNoCharge;
                dtpStart.Date = string.IsNullOrEmpty(_Entity.StartUnload) ? DateTime.MinValue : Convert.ToDateTime(_Entity.StartUnload);
                dtpComplete.Date = string.IsNullOrEmpty(_Entity.CompleteUnload) ? DateTime.MinValue : Convert.ToDateTime(_Entity.CompleteUnload);
                //dtpStart.Text = _Entity.StartUnload;
                //dtpComplete.Text = _Entity.CompleteUnload;
                //txtPacking.Value = _Entity.Packing;
                glAssignLoc.Value = _Entity.AssignLoc;
                //txtOfficer.Value = _Entity.ApprovingOfficer;
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
                txtWeekNo.Text = _Entity.WeekNo;
                //txtTruckNo.Text = _Entity.TruckNo;

                //Truck Transaction
                CancelledBy.Text = _Entity.CancelledBy;
                CancelledDate.Text = _Entity.CancelledDate;
                ArrivalTime.Text = _Entity.ArrivalTime;
                StartUnloading.Text = _Entity.StartUnloading;
                CompleteUnloading.Text = _Entity.CompleteUnloading;
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
               
                DockingTime.Text = _Entity.DockingTime;
                DockingDoor.Text = _Entity.DockingDoor;
               
              
                HoldStatus.Text = _Entity.HoldStatus;
                
  
              

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                DataTable checkCount = Gears.RetriveData2("Select DocNumber from wms.inbounddetail where docnumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {

                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.KeyFieldName = "LineNumber";
                    gv1.DataSourceID = null;
                    gvICN.ClientEnabled = true;
                    Generatebtn.ClientVisible = true;
                }
                // 2023-09-18 For Variance Tab
                DataTable checkCount1 = Gears.RetriveData2("Select DocNumber from wms.inbounddetail where docnumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                if (checkCount1.Rows.Count > 0)
                {

                    gv3.DataSourceID = null;
                    gv4.DataSourceID = null;
                    getDetail = Gears.RetriveData2("EXEC [dbo].[sp_WMS_Variance] 'WMSINB','" + txtDocNumber.Text + "' ", Session["ConnString"].ToString());
                     
                    gv3.DataSource = getDetail;
                    gv3.DataBind();
                    gv3.KeyFieldName = "ItemCode";
                    getDetailCOMI = Gears.RetriveData2("select a.ItemCode,b.FullDesc,c.Brand,c.Origin,c.MfgName as Manufacturers_Name,c.EstablishNo,c.VQMLICNo from WMS.InboundDetail a left join masterfile.item b on a.ItemCode = b.ItemCode left join wms.[COMIDetail] c on a.DocNumber = c.DocNumber and a.LineNumber = c.LineNumber where a.DocNumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                    gv4.DataSource = getDetailCOMI;
                    gv4.DataBind();
                
                }
                // 2023-09-18 For Variance Tab
                if (!string.IsNullOrEmpty(_Entity.LastEditedBy) && string.IsNullOrEmpty(_Entity.SubmittedBy) && Request.QueryString["entry"] == "E")
                {
                    updateBtn.Text = "Update";
                }
                if (Request.QueryString["entry"] == "D")
                {
                    glcheck.ClientVisible = false;
                }

                glcheck.Checked = true;
  

            }          
            if (Request.Params["__CALLBACKID"] != null)
                if (Request.Params["__CALLBACKID"].Contains("glAssignLoc"))
                {
                    if (gvPlant.Text != "")
                    {
                        Location.SelectCommand = "SELECT PlantCode,LocationCode from Masterfile.Location where LocationType != 'N' and PlantCode = '" + gvPlant.Text + "' and WarehouseCode = '" + gvWarehouse.Text + "' AND ISNULL(IsInactive,0)=0";
                    }
                    glAssignLoc.DataBind();
                }
           

        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSINB";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GWarehouseManagement.Inbound_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            Functions.AuditTrail("WMSINB", gparam._DocNo, gparam._UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "VALIDATE", Session["ConnString"].ToString());

        }
        #endregion
        //added 1/2/2024
        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (!IsPostBack)
            {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = view;
            }
        }
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (!IsPostBack)
            {
                ASPxCheckBox chk = sender as ASPxCheckBox;
                if (chk.ReadOnly) return;
                chk.ReadOnly = view;
            }
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            //Control look = (Control)sender;
            //((ASPxGridLookup)look).ReadOnly = view;
            if (!IsPostBack)
            {
                var look = sender as ASPxGridLookup;
                if (look != null)
                {
                    look.ReadOnly = view;
                }
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["entry"].ToString() == "V")
                {
                    ASPxGridLookup look = sender as ASPxGridLookup;
                    look.DropDownButton.Visible = false;
                    look.ClientEnabled = false;
                }
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            //if (Request.QueryString["entry"].ToString() == "V")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            if (!IsPostBack)
            {
                ASPxDateEdit date = sender as ASPxDateEdit;
                date.ReadOnly = view;
                dtpDocDate.DropDownButton.Enabled = false;
                dtpDocDate.ReadOnly = true;

                txtDelivery.DropDownButton.Enabled = false;
                txtDelivery.ReadOnly = true;

              
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            if (!IsPostBack)
            {
                ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
                spinedit.ReadOnly = view;
            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            DataTable checkCount = Gears.RetriveData2("Select DocNumber from wms.inbounddetail where docnumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
            if (checkCount.Rows.Count == 0)
            {
                if (e.ButtonType == ColumnCommandButtonType.New)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
            {
                e.Visible = false;
            }
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                //if (Request.QueryString["entry"] == "N")
                //{
                //    if (e.ButtonID == "Details" || e.ButtonID == "CountSheet" || e.ButtonID == "CopyButton")
                //    {
                //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                //    }
                //}
                if (Request.QueryString["entry"] == "V")
                {
                    if (e.ButtonID == "CopyButton" || e.ButtonID == "Delete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
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
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            string val2 = "";
            try
            {
                val2 = e.Parameters.Split('|')[3];
            }
            catch (Exception)
            {
                val2 = "";
            }
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string qty = "";
            decimal totalqty = 0;
            string codes = "";

            if (val2 == "")
            {
                val2 = "0";
            }

            if (e.Parameters.Contains("ItemCode") && itemcode != null)
            {
                DataTable getall = Gears.RetriveData2("Select top 1 ColorCode,ClassCode,SizeCode,FullDesc,isnull(StandardQty,0) as StandardQty,UnitBulk from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "' " +
                                                          "and isnull(a.IsInactive,0)=0", Session["ConnString"].ToString());
                foreach (DataRow dt in getall.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    qty = dt["StandardQty"].ToString();
                    qty = string.IsNullOrEmpty(qty) ? "0" : qty;
                }

                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val2));
                    codes += totalqty + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("BulkQty"))
            {
                DataTable getqty = Gears.RetriveData2("Select isnull(StandardQty,0) as StandardQty from masterfile.item where itemcode = '" + itemcode + "'", Session["ConnString"].ToString());
                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["StandardQty"].ToString();
                    }
                }
                qty = String.IsNullOrEmpty(qty) ? "0" : qty;
                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val));
                }

                itemlookup.JSProperties["cp_codes"] = totalqty + ";";
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
                grid.DataSourceID = "Masterfileitemdetail";
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
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.Plant = gvPlant.Text;
            _Entity.TransType = Request.QueryString["transtype"].ToString();
            _Entity.CustomerCode = gvCustomer.Text;
            _Entity.WarehouseCode = gvWarehouse.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.ICNNumber = String.IsNullOrEmpty(gvICN.Text) ? gvICN.NullText : gvICN.Text;
            _Entity.Delivery = txtDelivery.Text;
            _Entity.TranType = txtTranType.Text;
            _Entity.DRNumber = txtDRNo.Text;
      
            _Entity.ReqDept = txtReqDept.Text;
            _Entity.ShipmentType = txtShipmentType.Text;
            _Entity.RefDoc = txtRefDoc.Text;
            _Entity.Overtime = txtOvertime.Text;
            _Entity.AddManpower = txtAddManpower.Text;
            _Entity.NOmanpower = txtManpowerNo.Text;
            _Entity.TBSB = txtTBSB.Text;
            _Entity.Stat = txtStatus.Text;
            //_Entity.Trucker = txtTrucker.Text;
            //_Entity.ContainerTemp = String.IsNullOrEmpty(txtTemp.Text) ? "0" : txtTemp.Text;
            _Entity.Driver = txtDriver.Text;
            _Entity.PlateNo = txtPlate.Text;
           
            _Entity.Consignee = txtConsignee.Text;
            _Entity.ConAddress=txtConAddress.Text ;
            _Entity.TrackingNo= txtTruckName.Text;
            _Entity.ContainNumber=txtContainerNo.Text;
            _Entity.TruckT = txtTruckType.Text;
            _Entity.SealNo= txtSealNo.Text ;
            _Entity.TruckProv=txtProv.Text;
            
            //_Entity.ContainerNo = txtContainerNo.Text;
            //_Entity.ContactingDept = txtConDept.Text;
            _Entity.InvoiceNo = txtInvoice.Text;
            _Entity.Batch=txtBatch.Text;
            _Entity.Consigne = txtCon.Text;
 
       
            //_Entity.Supplier = gvSupplier.Text;
            _Entity.StorageType = glStorageType.Text;
            _Entity.ProdNumber = txtProdNo.Text;
            //_Entity.AWB = txtAWB.Text;
            
            _Entity.DocumentationStaff = txtStaff.Text;
            _Entity.WarehouseChecker = txtChecker.Text;
            //_Entity.GuardOnDuty = txtGuard.Text;
            //_Entity.CustomerRepresentative = txtRep.Text;
            //_Entity.ApprovingOfficer = txtOfficer.Text;

            _Entity.StartUnload = dtpStart.Text;
            _Entity.CompleteUnload = dtpComplete.Text;
            _Entity.InternalExternal = Convert.ToBoolean(CheckIntExt.Value.ToString());
            _Entity.IsNoCharge = Convert.ToBoolean(chkIsNoCharge.Value.ToString());
            _Entity.WeekNo = txtWeekNo.Text;
            //_Entity.TruckNo = txtTruckNo.Text;
            //_Entity.Packing = txtPacking.Text;
            
            _Entity.ICNTotalQty = Convert.ToDecimal(spinICNTotalQty.Value);

            _Entity.AssignLoc = glAssignLoc.Text;
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

            ////Truck Transaction
            //_Entity.CancelledDate = CancelledDate.Text;
            //_Entity.CancelledBy = CancelledBy.Text;
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

            _Entity.AddedBy = Session["userid"].ToString();


            _Entity.LastEditedBy = Session["userid"].ToString();
            string strError = "";
            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.Inbound", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

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
                            gv1.DataSourceID = sdsICNDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Validate();
                        //if (cp.JSProperties["cp_valmsg"].ToString() != "")
                        //{
                        //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                        //    cp.JSProperties["cp_success"] = true;
                        //}
                        //else
                        //{
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                        //}
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;


                case "Delete":
                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.Inbound", 3, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

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
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    break;

                case "ICN":
                    SqlDataSource ds = TranType;
                    ds.SelectCommand = string.Format("SELECT DocNumber,TransType from wms.icn where docnumber = '" + gvICN.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtTranType.Text = tran[0][1].ToString();
                    }

                    ds.SelectCommand = string.Format("select WarehouseCode,CustomerCode,PlantCode from wms.icn where docnumber = '" + gvICN.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        gvWarehouse.Value = inb[0][0].ToString();
                        gvCustomer.Value = inb[0][1].ToString();
                        //gvSupplier.Value = inb[0][2].ToString();
                        gvPlant.Value = inb[0][2].ToString();
                    }
                    cp.JSProperties["cp_icnclose"] = true;
                    break;

                case "Plant":
                    SqlDataSource pl = TranType;
                    pl.SelectCommand = string.Format("SELECT PlantCode,ReceivingLocationCode from Masterfile.Plant where PlantCode = '" + gvPlant.Text + "'");
                    CriteriaOperator plantCriteria = new InOperator("PlantCode", new string[] { gvPlant.Text });
                    Location.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, plantCriteria)).ToString();
                    glAssignLoc.DataSourceID = "Location";
                    glAssignLoc.DataBind();
                    DataView tranpl = (DataView)pl.Select(DataSourceSelectArguments.Empty);
                    if (tranpl.Count > 0)
                    {
                        glAssignLoc.Text = tranpl[0][1].ToString();
                    }
                    break;

                case "WH":
                    CriteriaOperator selectionCriteria = new InOperator("WarehouseCode", new string[] { gvWarehouse.Text });
                    Plant.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                    gvPlant.DataSourceID = "Plant";
                    gvPlant.DataBind();
                    break;

                case "refgrid":
                    gv1.DataBind();
                    break;

                case "CustomerICN":
                    gvICN.DataBind();
                    gvICN.Value = null;
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    break;

            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";
            //foreach (GridViewColumn column in gv1.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if ((e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName] == "") && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber"
            //        && dataColumn.FieldName != "BulkUnit" && dataColumn.FieldName != "Unit" && dataColumn.FieldName != "RRDocDate"
            //        && dataColumn.FieldName != "BatchNumber" && dataColumn.FieldName != "ToLocation" && dataColumn.FieldName != "LotID"
            //        && dataColumn.FieldName != "PickedQty" && dataColumn.FieldName != "Remarks" && dataColumn.FieldName != "StatusCode"
            //        && dataColumn.FieldName != "BarcodeNo" && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2"
            //        && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5"
            //        && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
            //        && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "ICNQty")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //}

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
                //e.InsertValues.Clear();
                //e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys[0] };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["BulkQty"] = values.NewValues["BulkQty"];
                    row["ReceivedQty"] = values.NewValues["ReceivedQty"];
                    row["BaseQty"] = values.NewValues["BaseQty"];
                    row["ExpiryDate"] = values.NewValues["ExpiryDate"];
                    row["ManufacturingDate"] = values.NewValues["ManufacturingDate"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["Remarks"] = values.NewValues["Remarks"];
                    row["Status"] = values.NewValues["Status"];
                    row["RRDocDate"] = values.NewValues["RRDocDate"];
                    row["BulkUnit"] = values.NewValues["BulkUnit"];
                    row["BatchNumber"] = values.NewValues["BatchNumber"];
                    row["PalletID"] = values.NewValues["PalletID"];
                    row["LotID"] = values.NewValues["LotID"];
                    row["ICNQty"] = values.NewValues["ICNQty"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
                    row["BarcodeNo"] = values.NewValues["BarcodeNo"];
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

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = i;
                    var ItemCode = values.NewValues["ItemCode"];
                    var ItemDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var BulkQty = values.NewValues["BulkQty"];
                    var ReceivedQty = values.NewValues["ReceivedQty"];
                    var BaseQty = values.NewValues["BaseQty"];
                    var ExpiryDate = values.NewValues["ExpiryDate"];
                    var ManufacturingDate = values.NewValues["ManufacturingDate"];
                    var ToLocation = values.NewValues["ToLocation"];
                    var Remarks = values.NewValues["Remarks"];
                    var Status = values.NewValues["Status"];
                    var RRDocDate = values.NewValues["RRDocDate"];
                    var BulkUnit = values.NewValues["BulkUnit"];
                    var BatchNumber = values.NewValues["BatchNumber"];
                    var PalletID = values.NewValues["PalletID"];
                    var PickedQty = values.NewValues["PickedQty"];
                    var LotID = values.NewValues["LotID"];
                    var ICNQty = values.NewValues["ICNQty"];
                    var StatusCode = values.NewValues["StatusCode"];
                    var BarcodeNo = values.NewValues["BarcodeNo"];
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    var Unit = values.NewValues["Unit"];

                    source.Rows.Add(LineNumber, ItemCode, ItemDesc, ColorCode, ClassCode, SizeCode, BulkQty, BulkUnit, ReceivedQty, Unit,
                       PalletID, BatchNumber, ManufacturingDate, ExpiryDate, Field9, ToLocation, LotID, RRDocDate, PickedQty, Remarks, Status, BaseQty, ICNQty,
                       StatusCode, BarcodeNo, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8);

                    i++;
                    //if (string.IsNullOrEmpty(OrderQty.ToString()))
                    //{
                    //    OrderQty = 0;
                    //}
                }
                // Updating required rows

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.LineNumber = dtRow["LineNumber"].ToString();
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.BulkQty = Convert.ToDecimal(dtRow["BulkQty"].ToString());
                    _EntityDetail.BulkUnit = dtRow["BulkUnit"].ToString();
                    _EntityDetail.ReceivedQty = Convert.ToDecimal(dtRow["ReceivedQty"].ToString());
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.ExpiryDate = Convert.ToDateTime(dtRow["ExpiryDate"].ToString());
                    _EntityDetail.BatchNumber = dtRow["BatchNumber"].ToString();
                    _EntityDetail.ManufacturingDate = Convert.ToDateTime(dtRow["ManufacturingDate"].ToString());
                    _EntityDetail.ToLocation = dtRow["ToLocation"].ToString();
                    _EntityDetail.PalletID = dtRow["PalletID"].ToString();
                    _EntityDetail.LotID = dtRow["LotID"].ToString();
                    _EntityDetail.RRDocDate = Convert.ToDateTime(dtRow["RRDocDate"].ToString());
                    _EntityDetail.PickedQty = Convert.ToDecimal(dtRow["PickedQty"].ToString() == "" ? "0" : dtRow["PickedQty"].ToString());
                    _EntityDetail.Remarks = dtRow["Remarks"].ToString();
                    _EntityDetail.Status = dtRow["Status"].ToString();
                    _EntityDetail.BaseQty = Convert.ToDecimal(dtRow["BaseQty"].ToString() == "" ? "0" : dtRow["PickedQty"].ToString());
                    _EntityDetail.ICNQty = Convert.ToDecimal(dtRow["ICNQty"].ToString() == "" ? "0" : dtRow["ICNQty"].ToString());
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();

                    _EntityDetail.AddInboundDetail(_EntityDetail);

                    //_EntityDetail.DeleteOutboundDetail(_EntityDetail);
                }
            }
        }
        #endregion

        protected void gvSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvICN_TextChanged(object sender, EventArgs e)
        {

        }

        private DataTable GetSelectedVal()
        {
            gv1.DataSourceID = null;
            gv1.DataBind();
            DataTable dt = new DataTable();
            string[] selectedValues = gvICN.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(gvICN.KeyFieldName, selectedValues);
            sdsICNDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["icndetail"] = sdsICNDetail.FilterExpression;
            gv1.DataSourceID = sdsICNDetail.ID;
            //if (gv1.DataSourceID != "")
            //{
            //    gv1.DataSourceID = null;
            //}
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
            dt.Columns["LineNumber"]};

            gvICN.ClientEnabled = false;
            return dt;
        }

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["icndetail"] = null;
            }

            if (Session["icndetail"] != null)
            {
                gv1.DataSourceID = sdsICNDetail.ID;
                sdsICNDetail.FilterExpression = Session["icndetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["entry"] == "N")
                {
                    dtpDocDate.Date = DateTime.Now;
                    txtDelivery.Date = DateTime.Now;
                }
            }
        }


        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        //Genrev 12/11/2015 Added codes to filter itemcode by customer
        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
            FilterItem();
            gridLookup.DataBind();
        }
        public void FilterItem()
        {
            if (Session["FilterCus"] == null)
            {
                Session["FilterCus"] = "";
            }
            if (!String.IsNullOrEmpty(Session["FilterCus"].ToString()))
            {
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 AND (ISNULL(Customer,'') = '' OR Customer = '" + Session["FilterCus"].ToString() + "') ORDER BY ItemCode ASC";
                // sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 AND (ISNULL(Customer,'') = '' OR Customer = '" + Session["FilterCus"].ToString() + "') ORDER BY ItemCode ASC";
            }
            else
            {
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0";
                //sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0";
            }
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterItem();
            grid.DataSourceID = "Masterfileitem";
            grid.DataBind();

            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, "ItemCode") != null)
                    if (grid.GetRowValues(i, "ItemCode").ToString() == e.Parameters)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, "ItemCode").ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }

        protected void LocationCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gllocation_CustomCallback);
            FilterLoc();
            gridLookup.DataBind();
        }

        public void FilterLoc()
        {
            if (Session["FilterWH"] == null)
            {
                Session["FilterWH"] = "";
            }
            //if (!String.IsNullOrEmpty(Session["FilterWH"].ToString()))
            //{
            //    ToLocation.SelectCommand = "SELECT LocationCode,WarehouseCode,RoomCode from Masterfile.Location where WarehouseCode = '" + Session["FilterWH"].ToString() + "' ORDER BY LocationCode ASC";
            //}
            //else
            //{
            //    ToLocation.SelectCommand = "SELECT LocationCode,WarehouseCode,RoomCode from Masterfile.Location ORDER BY LocationCode ASC";
            //}
        }

        public void gllocation_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterLoc();
            grid.DataSourceID = "ToLocation";
            grid.DataBind();
        }

        protected void gvCustomer_ValueChanged(object sender, EventArgs e)
        {
            Session["FilterCus"] = gvCustomer.Value;
        }

        protected void gvWarehouse_ValueChanged(object sender, EventArgs e)
        {
            Session["FilterWH"] = gvWarehouse.Value;
        }

        protected void callonload_Callback(object source, CallbackEventArgs e)
        {
            string PalletCharCount = "";
            string PaltwoCustomer = "";
            DataTable getPalCharCount = Gears.RetriveData2("Select isnull(PalletLength,12) as PalletCharCount,ISNULL(Field16,'') as Field16 from masterfile.BizPartner where BizPartnerCode = '" + gvCustomer.Text + "'", Session["ConnString"].ToString());
            if (getPalCharCount.Rows.Count > 0)
            {
                PalletCharCount = getPalCharCount.Rows[0]["PalletCharCount"].ToString();
                PaltwoCustomer = getPalCharCount.Rows[0]["Field16"].ToString();
            }
            callonload.JSProperties["cp_palcharcount"] = PalletCharCount;
            callonload.JSProperties["cp_paltwocustomer"] = PaltwoCustomer;
        }
        //GC end

        #region commented
        //protected void gv1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        //{
        //    if (!object.Equals(e.RowType, GridViewRowType.Data)) return;

        //    bool nopallet = string.IsNullOrEmpty(e.GetValue("PalletID").ToString());
        //    if (nopallet)
        //    {
        //        e.Row.ForeColor = System.Drawing.Color.Red;
        //        e.Row.ToolTip = "Check for missing values";
        //    }
        //}

        //protected void gvPlant_Init(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(plant_CustomCallback);
        //    if (Session["plantfilter"] != null)
        //    {
        //        gridLookup.GridView.DataSourceID = "Plant";
        //        Plant.FilterExpression = Session["plantfilter"].ToString();
        //    }
        //}

        //public void plant_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string column = e.Parameters;//Set column name
        //    if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback



        //}
        #endregion

    }
}