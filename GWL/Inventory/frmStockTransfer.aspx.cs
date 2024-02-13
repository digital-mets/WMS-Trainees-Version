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
using GearsInventory;

namespace GWL
{
    public partial class frmStockTransfer : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string filter = "";

        private static string Connection;

        Entity.StockTransfer _Entity = new StockTransfer();//Calls entity odsHeader
        Entity.StockTransfer.StockTransferDetail _EntityDetail = new StockTransfer.StockTransferDetail();//Call entity sdsDetail

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

            //if (glType.Text.toU == "DISPATCH")
            //{
            //    glDispatchNumber.ClientEnabled = false;
            //    dtpReceivedDate.ClientEnabled = true;
            //    if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
            //    {
            //        cbxIsAutoReceive.ClientEnabled = true;
            //        cbxIsAutoReceive.ReadOnly = false;
            //    }
            //    else
            //    {
            //        cbxIsAutoReceive.ClientEnabled = false;
            //        cbxIsAutoReceive.ReadOnly = true;
            //    }
            //    glFromWarehouse.ClientEnabled = true;
            //    glToWarehouse.ClientEnabled = true;
            //    gv1.Columns["ReceivedQty"].Width = 0;
            //    gv1.Columns["ReceivedBulkQty"].Width = 0;
            //}
            //else
            //{
            //    glDispatchNumber.ClientEnabled = true;
            //    dtpReceivedDate.ClientEnabled = false;
            //    cbxIsAutoReceive.ClientEnabled = false;
            //    cbxIsAutoReceive.ReadOnly = true;
            //    glFromWarehouse.ClientEnabled = false;
            //    glToWarehouse.ClientEnabled = false;
            //    gv1.Columns["ReceivedQty"].Width = 100;
            //    gv1.Columns["ReceivedBulkQty"].Width = 100;
            //}

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            if (!IsPostBack)
            {
                Session["STTFDeleteData"] = null;
                Session["STTFDatatable"] = null;
                Session["STTFType"] = "";
                Session["STRFilterExpression"] = null;
                Session["STRType"] = null;
                Session["STRreferencedetail"] = null;
                Session["STTFCloned"] = null;
                Connection = Session["ConnString"].ToString();

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Date = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                glType.Value = _Entity.Type;
                //dtpReceivedDate.Text = String.IsNullOrEmpty(_Entity.ReceivedDate) ? "" : _Entity.ReceivedDate.ToString();
                if (!String.IsNullOrEmpty(_Entity.ReceivedDate))
                {
                    dtpReceivedDate.Date = Convert.ToDateTime(_Entity.ReceivedDate);
                }
                
                if (_Entity.Type != null)
                {
                    if (_Entity.Type.ToString() == "D")
                    {
                        glDispatchNumber.ClientEnabled = false;
                        dtpReceivedDate.ClientEnabled = true;
                        if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
                        {
                            cbxIsAutoReceive.ClientEnabled = true;
                            cbxIsAutoReceive.ReadOnly = false;
                        }
                        else
                        {
                            cbxIsAutoReceive.ClientEnabled = false;
                            cbxIsAutoReceive.ReadOnly = true;
                        }
                        glFromWarehouse.ClientEnabled = true;
                        glToWarehouse.ClientEnabled = true;
                        gv1.Columns["ReceivedQty"].Width = 0;
                        gv1.Columns["ReceivedBulkQty"].Width = 0; 
                        //SpinClone.ClientEnabled = true;
                    }
                    else
                    {
                        glDispatchNumber.ClientEnabled = true;
                        dtpReceivedDate.ClientEnabled = false;
                        cbxIsAutoReceive.ClientEnabled = false;
                        cbxIsAutoReceive.ReadOnly = true;
                        glFromWarehouse.ClientEnabled = false;
                        glToWarehouse.ClientEnabled = false;
                        gv1.Columns["ReceivedQty"].Width = 100;
                        gv1.Columns["ReceivedBulkQty"].Width = 100;
                        //SpinClone.ClientEnabled = false; 
                    }
                }
                else
                {
                    glDispatchNumber.ClientEnabled = false;
                }

                glDispatchNumber.Value = _Entity.DispatchNumber;
                if (String.IsNullOrEmpty(_Entity.Type))
                {
                    SqlDataSource ds = sdsAccountType;
                    ds.SelectCommand = string.Format("SELECT Value FROM It.SystemSettings WHERE Code = 'ACCTTYPE'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtAccountabilityType.Text = inb[0][0].ToString();
                    }
                }
                else
                {
                    txtAccountabilityType.Value = _Entity.AccountabilityType;
                }
                glFromWarehouse.Value = _Entity.FromWarehouse;
                glToWarehouse.Value = _Entity.ToWarehouse;

                // 11/16/2021 Added by JCB
                txtRefRR.Text = _Entity.ReferenceRR;
                chkBackload.Value = _Entity.Backload;

                memRemarks.Value = _Entity.Remarks;
                txtTotalQty.Text = _Entity.TotalQty.ToString();
                txtTotalBulkQty.Text = _Entity.TotalBulkQty.ToString();
                cbxIsPrinted.Value = _Entity.IsPrinted;
                cbxIsAutoReceive.Value = _Entity.IsAutoReceive;

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
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                gv1.KeyFieldName = "LineNumber";
                gv1.Columns["OISNo"].Width = 0;
                
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        popup.ShowOnPageLoad = true;
                        if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                            updateBtn.Text = "Update";
                        else
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        break;
                    case "E":
                        popup.ShowOnPageLoad = true;
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

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;
                Session["STTFType"] = _Entity.Type;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.StockTransferDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
            }         
        }
        #endregion

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transType"].ToString();
            strresult += GearsInventory.GInventory.StockTransfer_Validate(gparam);
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
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all checkboxes
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;                       
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            if (Request.QueryString["entry"].ToString() == "V" || 
                Request.QueryString["entry"].ToString() == "D")
            {
                look.Enabled = false;
            }
            else
            {
                look.Enabled = true;
            }
        }

        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   
            if (e.ButtonType == ColumnCommandButtonType.Delete)
                e.Image.IconID = "actions_cancel_16x16";

            if (e.ButtonType == ColumnCommandButtonType.New)
                e.Image.IconID = "actions_addfile_16x16";

            if (e.ButtonType == ColumnCommandButtonType.Edit)
                e.Image.IconID = "actions_addfile_16x16";

            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;

            if (e.ButtonType == ColumnCommandButtonType.Edit ||
                e.ButtonType == ColumnCommandButtonType.Cancel)
                e.Visible = false;

            if (e.ButtonType == ColumnCommandButtonType.New ||
                e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    if (Request.QueryString["entry"] == "N" || 
                        Request.QueryString["entry"] == "E")
                    {
                        //if (glType.Text != "D")
                        //if (glType.Value.ToString() != "D")
                         if (Session["STTFType"].ToString() != "D")
                            e.Visible = false;
                        else
                            e.Visible = true;
                    }
                    else
                        e.Visible = false;
                }
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
            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "Delete")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
            else
            {
                //if (glType.Text != "D")
                //if (glType.Value.ToString() != "D")
                if (Session["STTFType"].ToString() != "D")
                {
                    if (e.ButtonID == "Delete")
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
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                if (Session["STRitemID"] != null)
                {
                    if (Session["STRitemID"].ToString() == glIDFinder(gridLookup.ID))
                    {
                        if (Session["STRsql"] != null)
                        {
                            gridLookup.GridView.DataSourceID = "sdsItemDetail";
                        }
                        else
                        {
                            gridLookup.GridView.DataSourceID = "sdsItemDetail";
                        }
                    }
                }
            }
        }

        public string glIDFinder(string glID)
        {
            if (glID.Contains("ColorCode"))
                return "ColorCode";
            else if (glID.Contains("ClassCode"))
                return "ClassCode";
            else if (glID.Contains("UnitBase"))
                return "UnitBase";
            else
                return "SizeCode";
        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;
            var itemlookup = sender as ASPxGridView;
            string codes = "";
            
                if (e.Parameters.Contains("ColorCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "ColorCode";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "ClassCode";
                    //sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS ColorCode, ClassCode, '' AS SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "SizeCode";
                    //sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("UnitBase"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, UnitBase FROM Masterfile.Item where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "UnitBase";
                }
                ASPxGridView grid = sender as ASPxGridView;
                Session["STRsql"] = sdsItemDetail.SelectCommand;
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
            _Entity.Transaction = Request.QueryString["transType"].ToString();
            _Entity.Connection = Session["ConnString"].ToString();

            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Type = String.IsNullOrEmpty(glType.Text) ? null : glType.Value.ToString();
            _Entity.ReceivedDate = dtpReceivedDate.Text;
            _Entity.DispatchNumber = String.IsNullOrEmpty(glDispatchNumber.Text) ? null : glDispatchNumber.Text;
            _Entity.AccountabilityType = String.IsNullOrEmpty(txtAccountabilityType.Text) ? null : txtAccountabilityType.Text;
            _Entity.FromWarehouse = String.IsNullOrEmpty(glFromWarehouse.Text) ? null : glFromWarehouse.Text;
            _Entity.ToWarehouse = String.IsNullOrEmpty(glToWarehouse.Text) ? null : glToWarehouse.Text;

            // 11/16/2021 Added by JCB
            _Entity.ReferenceRR = String.IsNullOrEmpty(txtRefRR.Text) ? null : txtRefRR.Text;
            _Entity.Backload = Convert.ToBoolean(chkBackload.Value.ToString());

            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.TotalQty = String.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToDecimal(txtTotalQty.Text);
            _Entity.TotalBulkQty = String.IsNullOrEmpty(txtTotalBulkQty.Text) ? 0 : Convert.ToDecimal(txtTotalBulkQty.Text);
            _Entity.IsPrinted = Convert.ToBoolean(cbxIsPrinted.Value.ToString());
            _Entity.IsAutoReceive = Convert.ToBoolean(cbxIsAutoReceive.Value.ToString());

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

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM Inventory.StockTransfer WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());

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
                        _Entity.entryy = updateBtn.Text;
                        //if (glType.Text == "D")
                        //if (glType.Value.ToString() != "D")
                        if (Session["STTFType"].ToString() != "D")
                        {
                            _Entity.changed = true;
                        }
                        _Entity.UpdateData(_Entity);
                        if (Session["STTFDatatable"] == "1")
                        {
                            if(Session["STTFCloned"] != "1")
                                Gears.RetriveData2("DELETE FROM Inventory.StockTransferDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            gv1.DataSourceID = sdsDispatchNumber.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            if (Session["STTFDeleteData"] == "1" && Session["STTFDatatable"] != "1")
                                Gears.RetriveData2("DELETE FROM Inventory.StockTransferDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv1.UpdateEdit();
                        }
                        _Entity.UpdateUnitFactor(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    gv1.DataSource = null;
                    break;

                case "Type":
                    //Session["STRType"] = glType.Text.ToString();
                    Session["STRType"] = glType.Value.ToString();
                    Session["STTFType"] = glType.Value.ToString();
                    glFromWarehouse.Text = "";
                    glToWarehouse.Text = "";
                    dtpReceivedDate.Text = "";
                    //if (glType.Text.ToString() == "D")
                    //if (glType.Value.ToString() == "D")
                    if (Session["STTFType"].ToString() == "D")
                    {
                        gv1.DataSourceID = "sdsDetail";
                        glDispatchNumber.ClientEnabled = false;
                        glFromWarehouse.ClientEnabled = true;
                        glToWarehouse.ClientEnabled = true;
                        glDispatchNumber.Text = null;
                        glDispatchNumber.DataSourceID = null;
                        glDispatchNumber.DataBind();
                        dtpReceivedDate.ClientEnabled = true;
                        if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
                        {
                            cbxIsAutoReceive.ClientEnabled = true;
                            cbxIsAutoReceive.ReadOnly = false;
                        }
                        else
                        {
                            cbxIsAutoReceive.ClientEnabled = false;
                            cbxIsAutoReceive.ReadOnly = true;
                        }
                        gv1.Columns["ReceivedQty"].Width = 0;
                        gv1.Columns["ReceivedBulkQty"].Width = 0;
                        //gv1.Columns["ReceivedQty"].VisibleIndex = 11;
                        //gv1.Columns["ReceivedBulkQty"].VisibleIndex = 12;
                        //gv1.Columns["DispatchQty"].VisibleIndex = 8;
                        //gv1.Columns["DispatchBulkQty"].VisibleIndex = 9;
                        Session["STTFDeleteData"] = "1";
                    }
                    else
                    {
                        gv1.DataSourceID = null;
                        glDispatchNumber.ClientEnabled = true;
                        glFromWarehouse.ClientEnabled = false;
                        glToWarehouse.ClientEnabled = false;
                        glDispatchNumber.DataSourceID = "sdsDispatchNumber";
                        glDispatchNumber.DataBind();
                        dtpReceivedDate.ClientEnabled = false;
                        cbxIsAutoReceive.ClientEnabled = false;
                        cbxIsAutoReceive.ReadOnly = true;
                        gv1.Columns["ReceivedQty"].Width = 100;
                        gv1.Columns["ReceivedBulkQty"].Width = 100;
                        //gv1.Columns["ReceivedQty"].VisibleIndex = 8;
                        //gv1.Columns["ReceivedBulkQty"].VisibleIndex = 9;
                        //gv1.Columns["DispatchQty"].VisibleIndex = 11;
                        //gv1.Columns["DispatchBulkQty"].VisibleIndex = 12;
                        Session["STTFDeleteData"] = "1";
                    }
                    cp.JSProperties["cp_refdel"] = true;
                    break;

                case "DispatchNumber":
                    glFromWarehouse.ClientEnabled = false;
                    glToWarehouse.ClientEnabled = false;
                    dtpReceivedDate.ClientEnabled = false;
                    GetSelectedVal();
                    SqlDataSource ds = sdsDispatchCallback;
                    ds.SelectCommand = string.Format("SELECT A.DocNumber, FromWarehouse, ToWarehouse, ReceivedDate"
                                            + " FROM Inventory.StockTransfer A INNER JOIN Inventory.StockTransferDetail B ON A.DocNumber = B.DocNumber "
                                            + " AND A.DocNumber = '" + glDispatchNumber.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        glFromWarehouse.Text = inb[0][1].ToString();
                        glToWarehouse.Text = inb[0][2].ToString();
                        dtpReceivedDate.Date = Convert.ToDateTime(inb[0][3].ToString());
                    }
                    glDispatchNumber.ClientEnabled = true;
                    cp.JSProperties["cp_generated"] = true;
                    break;

                case "Cloned":
                    Session["STTFDatatable"] = "1";
                    Session["STTFCloned"] = "1";
                    if (glType.Text.ToUpper() == "DISPATCH")
                    {
                        glDispatchNumber.ClientEnabled = false;
                        dtpReceivedDate.ClientEnabled = true;
                        if (Request.QueryString["entry"].ToString() != "V" && Request.QueryString["entry"].ToString() != "D")
                        {
                            cbxIsAutoReceive.ClientEnabled = true;
                            cbxIsAutoReceive.ReadOnly = false;
                        }
                        else
                        {
                            cbxIsAutoReceive.ClientEnabled = false;
                            cbxIsAutoReceive.ReadOnly = true;
                        }
                        glFromWarehouse.ClientEnabled = true;
                        glToWarehouse.ClientEnabled = true;
                        gv1.Columns["ReceivedQty"].Width = 0;
                        gv1.Columns["ReceivedBulkQty"].Width = 0;
                        //SpinClone.ClientEnabled = true;
                    }
                    else
                    {
                        glDispatchNumber.ClientEnabled = true;
                        dtpReceivedDate.ClientEnabled = false;
                        cbxIsAutoReceive.ClientEnabled = false;
                        cbxIsAutoReceive.ReadOnly = true;
                        glFromWarehouse.ClientEnabled = false;
                        glToWarehouse.ClientEnabled = false;
                        gv1.Columns["ReceivedQty"].Width = 100;
                        gv1.Columns["ReceivedBulkQty"].Width = 100;
                        //SpinClone.ClientEnabled = false;
                    }
                    break;
            }
        }        

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            int countline = 0;
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["STTFDatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

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

                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = countline++;
                    var OISNo = values.NewValues["OISNo"];
                    var ItemCode = values.NewValues["ItemCode"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var ReceivedQty = values.NewValues["ReceivedQty"];
                    var ReceivedBulkQty = values.NewValues["ReceivedBulkQty"];
                    var Unit = values.NewValues["Unit"];
                    var DispatchQty = values.NewValues["DispatchQty"];
                    var DispatchBulkQty = values.NewValues["DispatchBulkQty"];
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    var ExpDate = values.NewValues["ExpDate"];
                    var MfgDate = values.NewValues["MfgDate"];
                    var BatchNo = values.NewValues["BatchNo"];
                    var LotNo = values.NewValues["LotNo"];
                    source.Rows.Add(LineNumber, OISNo, ItemCode, ColorCode, ClassCode, SizeCode,
                                    ReceivedQty, ReceivedBulkQty, DispatchQty, DispatchBulkQty, Unit,  
                                    ExpDate, MfgDate, BatchNo, LotNo, Field1, Field2, Field3, Field4, 
                                    Field5, Field6, Field7, Field8, Field9);
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["ReceivedQty"] = values.NewValues["ReceivedQty"];
                    row["ReceivedBulkQty"] = values.NewValues["ReceivedBulkQty"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["DispatchQty"] = values.NewValues["DispatchQty"];
                    row["DispatchBulkQty"] = values.NewValues["DispatchBulkQty"];
                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                    row["ExpDate"] = values.NewValues["ExpDate"];
                    row["MfgDate"] = values.NewValues["MfgDate"];
                    row["BatchNo"] = values.NewValues["BatchNo"];
                    row["LotNo"] = values.NewValues["LotNo"];
                }

                foreach (DataRow dtRow in source.Rows)
                {
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.ReceivedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedQty"]) ? 0 : dtRow["ReceivedQty"]);
                    _EntityDetail.ReceivedBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedBulkQty"]) ? 0 : dtRow["ReceivedBulkQty"]);
                    _EntityDetail.DispatchQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchQty"]) ? 0 : dtRow["DispatchQty"]);
                    _EntityDetail.DispatchBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchBulkQty"]) ? 0 : dtRow["DispatchBulkQty"]);
                    //_EntityDetail.ReceivedQty = (glType.Text == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedQty"]) ? 0 : dtRow["ReceivedQty"])
                    //                                                : Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchQty"]) ? 0 : dtRow["DispatchQty"]));
                    //_EntityDetail.ReceivedBulkQty = (glType.Text == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedBulkQty"]) ? 0 : dtRow["ReceivedBulkQty"])
                    //                                                    : Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchBulkQty"]) ? 0 : dtRow["DispatchBulkQty"]));
                    //_EntityDetail.ReceivedQty = (Session["STTFType"].ToString() == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedQty"]) ? 0 : dtRow["ReceivedQty"])
                    //                                                : Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchQty"]) ? 0 : dtRow["DispatchQty"]));
                    //_EntityDetail.ReceivedBulkQty = (Session["STTFType"].ToString() == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedBulkQty"]) ? 0 : dtRow["ReceivedBulkQty"])
                    //                                                    : Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchBulkQty"]) ? 0 : dtRow["DispatchBulkQty"]));
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    //_EntityDetail.DispatchQty = (glType.Text == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchQty"]) ? 0 : dtRow["DispatchQty"])
                    //                                                : Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedQty"]) ? 0 : dtRow["ReceivedQty"]));
                    //_EntityDetail.DispatchBulkQty = (glType.Text == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchBulkQty"]) ? 0 : dtRow["DispatchBulkQty"])
                    //                                                    : Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedBulkQty"]) ? 0 : dtRow["ReceivedBulkQty"]));
                    //_EntityDetail.DispatchQty = (Session["STTFType"].ToString() == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchQty"]) ? 0 : dtRow["DispatchQty"])
                    //                                                : Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedQty"]) ? 0 : dtRow["ReceivedQty"]));
                    //_EntityDetail.DispatchBulkQty = (Session["STTFType"].ToString() == "R" ? Convert.ToDecimal(Convert.IsDBNull(dtRow["DispatchBulkQty"]) ? 0 : dtRow["DispatchBulkQty"])
                    //                                                    : Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedBulkQty"]) ? 0 : dtRow["ReceivedBulkQty"]));
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.ExpDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["ExpDate"]) ? null : dtRow["ExpDate"]);
                    _EntityDetail.MfgDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["MfgDate"]) ? null : dtRow["MfgDate"]);
                    _EntityDetail.BatchNo = dtRow["BatchNo"].ToString();
                    _EntityDetail.LotNo = dtRow["LotNo"].ToString();
                    _EntityDetail.AddStockTransferDetail(_EntityDetail);
                }
            }
        }
        #endregion

        private DataTable GetSelectedVal()
        {
            Session["STTFDatatable"] = "0";
            gv1.DataSourceID = null;
            gv1.DataBind();

            DataTable dt = new DataTable();
            Session["STRreferencedetail"] = sdsDispatchNumber.FilterExpression;
            if (Session["STTFCloned"] == "1")
            {
                sdsDispatchNumber.SelectCommand = "SELECT A.DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, OISNo, "
                                              + "B.ItemCode,C.FullDesc, B.ColorCode, B.ClassCode, B.SizeCode, ISNULL(DispatchQty,0.0000) AS DispatchQty, ISNULL(ReceivedQty,0.0000) AS ReceivedQty, Unit, ISNULL(DispatchBulkQty,0.0000) AS DispatchBulkQty, ISNULL(ReceivedBulkQty,0.0000) AS ReceivedBulkQty, "
                                               + "A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, Version, B.ExpDate, B.MfgDate, B.BatchNo, B.LotNo "
                                               + "FROM Inventory.StockTransfer A INNER JOIN Inventory.StockTransferDetail B  ON A.DocNumber = B.DocNumber  LEFT JOIN Masterfile.Item C ON B.ITemCode = C.ItemCode"
                                               + "AND A.DocNumber = '" + txtDocNumber.Text + "'";
            }
            else
            {
                sdsDispatchNumber.SelectCommand = "SELECT A.DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, OISNo, "
                                               + "B.ItemCode,C.FullDesc, B.ColorCode, B.ClassCode, B.SizeCode, ISNULL(DispatchQty,0.0000) AS DispatchQty, ISNULL(ReceivedQty,0.0000) AS ReceivedQty, Unit, ISNULL(DispatchBulkQty,0.0000) AS DispatchBulkQty, ISNULL(ReceivedBulkQty,0.0000) AS ReceivedBulkQty, "
                                               + "A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, Version, B.ExpDate, B.MfgDate, B.BatchNo, B.LotNo "
                                               + "FROM Inventory.StockTransfer A INNER JOIN Inventory.StockTransferDetail B ON A.DocNumber = B.DocNumber LEFT JOIN Masterfile.Item C ON B.ITemCode = C.ItemCode "
                                               + "AND A.DocNumber = '" + glDispatchNumber.Text + "'";
            }
            
            gv1.DataSourceID = "sdsDispatchNumber";
            gv1.DataBind();
            Session["STTFDatatable"] = "1";

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

            dt.PrimaryKey = new DataColumn[] {dt.Columns["LineNumber"]};
            return dt;
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0)=0";
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                //string changes = param.Split(';')[0];
                string itemcode = param.Split(';')[1];
                if (string.IsNullOrEmpty(itemcode)) return;


                DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                                                                     "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getColor.Rows.Count > 1)
                {
                    codes = "" + ";";
                    Session["ColorCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getColor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        Session["ColorCode"] = dt["ColorCode"].ToString();
                    }
                }

                DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                                                                         "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getClass.Rows.Count > 1)
                {
                    codes += "" + ";";
                    Session["ClassCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getClass.Rows)
                    {
                        codes += dt["ClassCode"].ToString() + ";";
                        Session["ClassCode"] = dt["ClassCode"].ToString();
                    }
                }

                DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                                                                             "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getSize.Rows.Count > 1)
                {
                    codes += "" + ";";
                    Session["SizeCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getSize.Rows)
                    {
                        codes += dt["SizeCode"].ToString() + ";";
                        Session["SizeCode"] = dt["SizeCode"].ToString();
                    }
                }


                DataTable getUnitBase = Gears.RetriveData2("SELECT DISTINCT UnitBase,FullDesc FROM Masterfile.item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getUnitBase.Rows.Count == 0)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnitBase.Rows)
                    {
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                    }
                }

                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
        public string CheckSubmit()
        {
            string result = "";

            result = Functions.Submitted(_Entity.DocNumber, "Inventory.StockTransfer", 1, Session["ConnString"].ToString());

            return result;
        }

        protected void dtpReceivedDate_Load(object sender, EventArgs e)
        {

        }
    }
}