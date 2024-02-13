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

    #region Changes
    //-- =============================================
    //-- Edit By:		Luis Genel T. Edpao
    //-- Edit Date:     12/14/2015
    //-- Description:	Added Price(Detail)
    //-- =============================================
    #endregion

    public partial class frmPickList : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation


        Entity.PICKLIST _Entity = new PICKLIST();//Calls entity ICN
        Entity.PICKLIST.PICKLISTDetail _EntityDetail = new PICKLIST.PICKLISTDetail();//Call entity POdetails
        private DataTable getDetail;
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            //Location.SelectCommand = "SELECT LocationCode,LocationDescription FROM Masterfile.Location where ISNULL(IsInactive,0)=0 and ISNULL(OnHandBaseQty,0) > 0 and WarehouseCode = '" + txtwarehousecode.Text + "'";
            // Location.SelectCommand = "SELECT LocationCode,LocationDescription FROM Masterfile.Location where ISNULL(IsInactive,0)=0 and WarehouseCode = '" + txtwarehousecode.Text + "'";



            if (!string.IsNullOrEmpty(txtCustomercode.Text))
            {
                Session["FilterCus"] = txtCustomercode.Text;
            }
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
            gv1.DataSource = null;
            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }
            dtpdocdate.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));


            if (!IsPostBack)
            {
                Session["LocationExp"] = null;
                Session["OCNGrid"] = null;
                Session["palletid"] = null;
                Session["FilterCus"] = null;
                Session["itemsql"] = null;


                Warehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";


                DataTable dtgrid1 = Gears.RetriveData2("SELECT DocNumber FROM  wms.picklistdetail where DocNumber= '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                if (dtgrid1.Rows.Count > 0)
                {
                    gv1.ClientVisible = true;
                }
                else
                {
                    gv1.ClientVisible = false;
                }

                switch (Request.QueryString["entry"].ToString())
                {

                    case "N":
                        updateBtn.Text = "Update";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocnumber.ReadOnly = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;

                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }


                txtDocnumber.Value = Request.QueryString["docnumber"].ToString(); ; //sets docnumber from session
                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //gv1.DataSourceID = "sdsDetail"; GC 12/23/2015
                //popup.ShowOnPageLoad = false;
                gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                gv1.Settings.VerticalScrollableHeight = 200;
                //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}
                //else
                //{
                //gv1.DataSourceID = "odsDetail"; GC 12/23/2015

                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());

                DataTable dtgrid = Gears.RetriveData2("SELECT DocNumber FROM  wms.OCNandPicklistDetail where DocNumber= '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
                if (dtgrid.Rows.Count > 0)
                {
                    gv3.DataSourceID = null;
                    getDetail = Gears.RetriveData2("EXEC [dbo].[sp_WMS_Variance] 'WMSPICK','" + Request.QueryString["docnumber"].ToString() + "' ", Session["ConnString"].ToString());
                    gv3.DataSource = getDetail;
                    gv3.DataBind();
                    dgvocn.DataSourceID = "OCNInsert";
                }
                else
                {
                    gv3.DataSourceID = null;
                    getDetail = Gears.RetriveData2("Select Distinct A.ItemCode as 'ItemCode',MAX(C.FullDesc) as ItemDesc,MAX(ISNULL(A.BulkQty,0)) AS 'Documented_Quantity',MAX(ISNULL(A.Qty,0)) as 'Documented Weight', " +
                        "Sum(ISNULL(B.BulkQty, 0)) as 'Received Quantity', Sum(ISNULL(B.Qty, 0)) as 'Received Weight', ABS(MAX(ISNULL(A.BulkQty, 0)) - SUM(ISNULL(B.BulkQty, 0))) as 'Deviation Quantity', ABS(MAX(ISNULL(A.Qty, 0)) - SUM(ISNULL(B.Qty, 0))) as 'Deviation Weight', " +
                        "MAX(FullName) as 'Received By', MAX(FORMAT(D.DeliveryDate, 'MM/dd/yyyy')) as 'Received Date', Case when ISNULL(MAX(D.AcceptBy), '') != '' and ISNULL(MAX(D.RFPickBy), '') = '' then 'In Progress' " +
                        "when ISNULL(MAX(D.AcceptBy), '') != '' and ISNULL(MAX(D.RFPickBy), '') != '' then 'Completed' else 'Pending' End as 'Status'  from WMS.OCNDetail A LEFT JOIN WMS.PicklistDetail B ON A.DocNumber = B.DocNumber AND A.ItemCode = B.ItemCode and A.LineNumber = B.OCNLineNumber left join Masterfile.Item C on A.ItemCode = C.ItemCode " +
                        "join WMS.Picklist D on A.DocNumber = D.Docnumber left join IT.Users E on  E.UserId = LEFT(D.AcceptBy, CHARINDEX(',', D.AcceptBy + ',') - 1)  where A.DocNumber = '" + Request.QueryString["docnumber"].ToString() + "' Group by A.ItemCode, A.LineNumber", Session["ConnString"].ToString());
                    gv3.DataSource = getDetail;
                    gv3.DataBind();
                    dgvocn.DataSourceID = "sdsDetail1";
                }
                dtpdocdate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                dtpdeliverydate.Text = String.IsNullOrEmpty(_Entity.DeliveryDate) ? "" : Convert.ToDateTime(_Entity.DeliveryDate).ToShortDateString();
                txtCustomercode.Value = _Entity.CustomerCode;

                Session["FilterCus"] = txtCustomercode.Text;
                if (_Entity.PicklistType == "FR")
                {
                    cbxPickListType.Text = "Pick From Reserved";
                }
                else
                {
                    cbxPickListType.Text = "Pick From Normal";
                }

                if (_Entity.PicklistStatus == "N" || _Entity.PicklistStatus == "")
                {
                    txtstatuscode.Text = "NEW";
                }
                else
                {
                    txtstatuscode.Text = "OUT";
                }

                txtwarehousecode.Value = _Entity.WarehouseCode;





                txtremarks.Text = _Entity.Remarks;
                txtOutboundNo.Text = _Entity.OutboundNo;
                chckAutoPick.Value = _Entity.IsAutoPick;
                txtTruckingCompany.Text = _Entity.TruckingCo;
                txtPlateNumber.Text = _Entity.PlateNo;
                txtDriverName.Text = _Entity.DriverName;
                glStorageType.Value = _Entity.StorageType;
                //txtAddress.Text = _Entity.DeliverAddress;

                //_Entity.DocNumber = txtDocnumber.Text;
                //_Entity.DocDate = deDocDate.Value.ToString();
                //_Entity.ExpectedDeliveryDate = deExpDelDate.Value.ToString();
                //_Entity.StorerKey = glStorerKey.Text;
                //_Entity.WarehouseCode = glWarehouseCode.Text;
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

                txtConsignee.Text = _Entity.Consignee;
                txtConsigneeAddress.Text = _Entity.ConsigneeAddress;
                txtOvertime.Text = _Entity.Overtime;
                txtAddtionalManpower.Text = _Entity.AddtionalManpower;
                txtSuppliedBy.Text = _Entity.SuppliedBy;
                txtNOManpower.Text = _Entity.NOManpower;
                txtTruckProviderByMets.Text = _Entity.TruckProviderByMets;
                txtSealNo.Text = _Entity.SealNo;

                txtRefDoc.Text = _Entity.RefDoc;
                txtShipType.Text = _Entity.ShipmentType;
                txtTruckType.Text = _Entity.TruckType;
                txtReqCoDept.Text = _Entity.CompanyDept;







                //}
                // GC 12/23/2015
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";
                //    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}
                //end

                //if (Session["IsWithDetail"].ToString() == "True" || Request.QueryString["entry"].ToString() != "N" )
                //{
                //    dgvocn.DataSourceID = "OCNInsert";
                //    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}

                // GC 12/23/2015
                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM WMS.PICKLISTDETAIL WHERE DocNumber ='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());

                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                //end

            }
            glcheck.Checked = true;
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
            DataTable count = Gears.RetriveData2("select Docnumber from wms.OCNandPicklistDetail where DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());
            if (Request.QueryString["entry"].ToString() == "V" && Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
                //e.Editor.ReadOnly = dgocn;
            }
        }

        protected void dgvocn_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V")
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
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            //string value = "";
            //DataTable count = Gears.RetriveData2("select * from wms.OCNandPicklistDetail where DocNumber = '" + txtDocnumber.Text + "'");

            //if(count.Rows.Count > 0)
            //{

            //    value = "E";
            //}
            //else
            //{
            //    value = "";
            //}
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }

            //DataTable checkCount = Gears.RetriveData2("Select DocNumber from wms.picklistdetail where docnumber = '" + txtDocnumber.Text + "'");
            //if (checkCount.Rows.Count == 0)
            //{
            //    if (e.ButtonType == ColumnCommandButtonType.New)
            //        e.Visible = false;
            //}
            //if (e.ButtonType == ColumnCommandButtonType.Update)
            //{
            //    e.Visible = false;
            //}
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

        protected void gl6_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback3);
            gridLookup.GridView.DataSourceID = "Location";
        }
        public void gridView_CustomCallback3(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = "Location";
            grid.DataBind();

            string column = "LocationCode";
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, column) != null)
                    if (grid.GetRowValues(i, column).ToString() == "LocationCode")
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, column).ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }
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
                DataTable getall = Gears.RetriveData2("Select top 1 ColorCode,ClassCode,SizeCode,FullDesc,isnull(StandardQty,0) as StandardQty,UnitBulk,UnitBase from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "' " +
                                                          "and isnull(a.IsInactive,0)=0", Session["ConnString"].ToString());
                foreach (DataRow dt in getall.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";

                    qty = dt["StandardQty"].ToString();
                    qty = string.IsNullOrEmpty(qty) ? "0" : qty;
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
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
            else if (e.Parameters.Contains("PalletID"))
            {
                DataTable getRemainingQty = Gears.RetriveData2("Select SUM(ISNULL(RemainingBaseQty,0))-SUM(ISNULL(PickedBaseQty,0))-SUM(ISNULL(ReservedBaseQty,0)) as Qty,SUM(ISNULL(RemainingBulkQty,0))-SUM(ISNULL(PickedBulkQty,0))-SUM(ISNULL(ReservedBulkQty,0)) as BulkQty from WMS.CountsheetSetup where PAlletID = '" + val + "' and ItemCode= '" + itemcode + "'", Session["ConnString"].ToString());
                foreach (DataRow dt in getRemainingQty.Rows)
                {
                    codes = dt["Qty"].ToString() + ";";
                    codes += dt["BulkQty"].ToString() + ";";
                    codes += val + ";";

                }


                itemlookup.JSProperties["cp_codes"] = codes;

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

            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpdocdate.Text;
            _Entity.DeliveryDate = dtpdeliverydate.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(txtCustomercode.Text) ? null : txtCustomercode.Value.ToString();
            _Entity.TransType = Request.QueryString["transtype"].ToString();
            if (cbxPickListType.Text == "Pick From Reserved")
            {
                _Entity.PicklistType = "FR";
            }
            else
            {
                _Entity.PicklistType = "N";
            }

            if (txtstatuscode.Text == "NEW")
            {
                _Entity.PicklistStatus = "N";
            }

            _Entity.WarehouseCode = String.IsNullOrEmpty(txtwarehousecode.Text) ? null : txtwarehousecode.Value.ToString();

            _Entity.Remarks = txtremarks.Text;
            _Entity.TruckingCo = txtTruckingCompany.Text;
            _Entity.PlateNo = txtPlateNumber.Text;
            _Entity.DriverName = txtDriverName.Text;
            _Entity.OutboundNo = txtOutboundNo.Text;
            _Entity.IsAutoPick = Convert.ToBoolean(chckAutoPick.Value);
            _Entity.StorageType = glStorageType.Text;
            //_Entity.DeliverAddress = txtAddress.Text;
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            _Entity.Consignee = txtConsignee.Text;
            _Entity.ConsigneeAddress = txtConsigneeAddress.Text;
            _Entity.Overtime = txtOvertime.Text;
            _Entity.AddtionalManpower = txtAddtionalManpower.Text;
            _Entity.SuppliedBy = txtSuppliedBy.Text;
            _Entity.NOManpower = txtNOManpower.Text;
            _Entity.TruckProviderByMets = txtTruckProviderByMets.Text;
            _Entity.SealNo = txtSealNo.Text;

            _Entity.CompanyDept = txtReqCoDept.Text;
            _Entity.ShipmentType = txtShipType.Text;
            _Entity.RefDoc = txtRefDoc.Text;
            _Entity.TruckType = String.IsNullOrEmpty(txtTruckType.Text) ? null : txtTruckType.Value.ToString();








            _Entity.LastEditedBy = Session["userid"].ToString();
            string strError;
            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.Picklist", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

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
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();//2nd initiation to insert grid



                        Validate();
                        cp.JSProperties["cp_message1"] = "Successfully Added!";//Message variable to client side

                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.Picklist", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;


                    }


                    if (Session["Datatable"] == "1")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.UpdateEdit();


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
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        dgvocn.DataSourceID = "OCNInsert";
                        OCNInsert.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                        dgvocn.UpdateEdit();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Delete":
                    strError = Functions.Submitted(_Entity.DocNumber, "WMS.Picklist", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve

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
                    Gears.RetriveData2("DELETE FROM WMS.CountSheetsubsi Where TransDoc = '" + txtDocnumber.Text + "' and TransType='WMSPICK'", Session["ConnString"].ToString());
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";

                    break;



                case "refgrid":
                    gv1.DataBind();
                    break;
                case "wh":
                    Location.DataBind();
                    //Location.DataBind();
                    break;

            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            // GC 12/23/2015
            //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string PalletID = "";
            //string ClassCode = "";
            //string SizeCode = "";
            if (error == false && check == false)
            {


                //Checking for non existing Codes..
                ItemCode = e.NewValues["ItemCode"].ToString();
                if (e.NewValues["PalletID"] != null)
                {
                    PalletID = e.NewValues["PalletID"].ToString();

                    DataTable item = Gears.RetriveData2("SELECT RecordID FROM WMS.ItemLocation with (nolock) WHERE ItemCode = '" + ItemCode + "' AND  PalletID = '" + PalletID + "' AND WarehouseCode='" + txtwarehousecode.Text + "'", Session["ConnString"].ToString());
                    if (item.Rows.Count < 1)
                    {
                        AddError(e.Errors, gv1.Columns["PalletID"], "Pallet ID is not associated with Item Code '" + ItemCode + "'");
                    }
                }

            }

            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
            }





            //end
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
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._Connection = Session["ConnString"].ToString();
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSPICK";

            string strresult = GWarehouseManagement.PickList_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion


        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //foreach (GridViewColumn col in gv1.VisibleColumns)
            //{
            //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    dt.Columns.Add(dataColumn.FieldName);
            //}
            //for (int i = 0; i < gv1.VisibleRowCount; i++)
            //{
            //    DataRow row = dt.Rows.Add();
            //    foreach (DataColumn col in dt.Columns)
            //        row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            //}
            //dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["LineNumber"]};
            //return dt;

            //_Entity.DocNumber = txtDocnumber.Text;
            //DataTable dtgenerate = PICKLIST.dtGenerate(txtocnnumber.Text);

            //foreach (DataRow dtRow in dtgenerate.Rows)
            //{

            //    gv1.DataSource = dtRow;
            //    gv1.KeyFieldName = "Docnumber,Linenumber";
            //    gv1.DataBind();

            //}
        }

        protected void gv1_Init(object sender, EventArgs e)
        {

            //ASPxGridView gridview = sender as ASPxGridView;
            //gridview.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPicklist_CustomCallback);
            //if (Session["ocndetail"] != null)
            //{
            //    gridview.DataSource = OCNDETAIL;
            //    OCNDETAIL.FilterExpression = Session["ocndetail"].ToString();
            //}
        }
        //public void glPicklist_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    if (!e.Parameters.Contains("ValueChanged")) return;
        //    string value = e.Parameters.Split('|')[1];

        //    ASPxGridView grid = sender as ASPxGridView;
        //    grid.DataSourceID = null;

        //    //var selectedValues = txtocnnumber.GridView.GetSelectedFieldValues(txtocnnumber.KeyFieldName);
        //    //if (selectedValues.Count == 0)
        //    //    selectedValues.Add(-1);
        //    CriteriaOperator selectionCriteria = new InOperator(txtocnnumber.KeyFieldName, new string[] { value });
        //    OCNDETAIL.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["ocndetail"] = OCNDETAIL.FilterExpression;
        //    grid.DataSource = OCNDETAIL;
        //    grid.DataBind();
        //    Session["Datatable"] = "1";
        //}

        //private DataTable GetSelectedVal()
        //{
        //    DataTable dt = new DataTable();
        //    string[] selectedValues = txtocnnumber.Text.Split(';');
        //    CriteriaOperator selectionCriteria = new InOperator(txtocnnumber.KeyFieldName, selectedValues);
        //    OCNDETAIL.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["ocndetail"] = OCNDETAIL.FilterExpression;
        //    gv1.DataSource = OCNDETAIL;
        //    gv1.DataBind();


        //    foreach (GridViewColumn col in gv1.VisibleColumns)
        //    {
        //        GridViewDataColumn dataColumn = col as GridViewDataColumn;
        //        if (dataColumn == null) continue;
        //        dt.Columns.Add(dataColumn.FieldName);
        //    }
        //    for (int i = 0; i < gv1.VisibleRowCount; i++)
        //    {
        //        DataRow row = dt.Rows.Add();
        //        foreach (DataColumn col in dt.Columns)
        //            row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
        //    }
        //    dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
        //    dt.Columns["LineNumber"]};
        //    return dt;
        //}
        //protected void gv1_DataBinding(object sender, EventArgs e)
        //{
        //    if (IsCallback && Session["Datatable"] != null)
        //        if (gv1.DataSource == null)
        //        {
        //            DataTable source = GetSelectedVal();
        //            gv1.DataSource = source;
        //        }
        //}

        protected void txtplantcode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtplantcode_Init(object sender, EventArgs e)
        {

        }




        protected void glpallet_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gv1")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glpallet_CustomCallback);
                if (Session["itemsql"] != null)
                {
                    sdsPallet.SelectCommand = Session["itemsql"].ToString();
                    sdsPallet.DataBind();
                }

                //gridLookup.GridView.DataSourceID = Masterfileitem.ID; 
                //gridLookup.GridView.DataSource = Masterfileitem;

            }
        }

        public void glpallet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string itemcode = e.Parameters.Split('|')[0];//Set column name
            if (itemcode.Contains("GLP_AIC") || itemcode.Contains("GLP_AC") || itemcode.Contains("GLP_F")) return;//Traps the callback

            //string location = e.Parameters.Split('|')[4];//Set column value

            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            if (e.Parameters.Contains("ItemCodeDropDown"))
            {
                sdsPallet.SelectCommand = "SELECT DISTINCT PalletID,CONVERT(VARCHAR(24),MfgDate,101) as MfgDate, CONVERT(VARCHAR(24),ExpirationDate,101) as ExpirationDate,BatchNumber "
                                + " ,ItemCode , ColorCode , ClassCode , SizeCode , Location "
                                + " FROM WMS.Countsheetsetup"
                                + " WHERE ItemCode = '" + itemcode + "' "
                                + " AND WarehouseCode = '" + txtwarehousecode.Text + "' "
                                + " AND ISNULL(PutawayDate,'')!='' AND (ISNULL(RemainingBaseQty,0)-ISNULL(PickedBaseQty,0)-ISNULL(ReservedBaseQty,0) > 0 OR ISNULL(RemainingBulkQty,0)-ISNULL(PickedBulkQty,0)-ISNULL(ReservedBulkQty,0) >0)";
                Session["itemsql"] = sdsPallet.SelectCommand;

                grid.DataBind();
            }
        }
        protected void glOCNumber_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glOCN_CustomCallback);
            if (Session["OCNGrid"] != null)
            {
                gridLookup.GridView.DataSourceID = OCN.ID;
                OCN.FilterExpression = Session["OCNGrid"].ToString();
            }
        }
        public void glOCN_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string ocn = cbxPickListType.Text; //SetPalletID column name
            //   if (ocn.Contains("GLP_AIC") || ocn.Contains("GLP_AC") || ocn.Contains("GLP_F")) return;//Traps the callback

            if (ocn == "Pick From Reserved")
            {
                ocn = "FR";
            }
            else
            {
                ocn = "N";
            }
            string customer = txtCustomercode.Text;
            string warehouse = txtwarehousecode.Text;




            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("PickType", new string[] { ocn });
            CriteriaOperator selectionCriteria2 = new InOperator("StorerKey", new string[] { customer });
            CriteriaOperator selectionCriteria3 = new InOperator("WarehouseCode", new string[] { warehouse });


            OCN.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, selectionCriteria2, selectionCriteria3)).ToString();

            Session["OCNGrid"] = OCN.FilterExpression;
            grid.DataSourceID = OCN.ID;
            grid.DataBind();
        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }



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

        protected void txtCustomercode_ValueChanged(object sender, EventArgs e)
        {
            Session["FilterCus"] = txtCustomercode.Value;
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["entry"] == "N")
                {
                    dtpdocdate.Date = DateTime.Now;
                }
            }
        }
    }
}