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
    public partial class frmRequest : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string filter = "";
        string specific = "";
        string param = "";

        private static string Connection;

        Entity.Request _Entity = new Request();//Calls entity odsHeader
        Entity.Request.RequestDetail _EntityDetail = new Request.RequestDetail();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {

            Gears.UseConnectionString(Session["ConnString"].ToString());

            //ADDED NEW
            ChangeSource(String.IsNullOrEmpty(glSpecificMaterialType.Text) ? "JO Bill Of Material" : glSpecificMaterialType.Text);

            switch (Request.QueryString["transtype"].ToString())
            {
                case "INVJOR":
                    this.Title = "Material Request";
                    break;

                case "INVOSR":
                    this.Title = "Supplies Request";
                    filter = "ITMCAT_OS";
                    break;

                case "INVSAR":
                    this.Title = "Samples Request";
                    filter = "ITMCAT_FOB";
                    break;

                case "INVCOR":
                    this.Title = "Complimentary Request";
                    filter = "ITMCAT_FOB";
                    break;

                case "INVASR":
                    filter = "ITMCAT_ASI";
                    break;

            }

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!String.IsNullOrEmpty(filter))
            {
                DataTable filteritem = new DataTable();
                filteritem = Gears.RetriveData2("SELECT REPLACE(Value,',',''',''') AS Value FROM IT.SystemSettings WHERE Code ='" + filter + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in filteritem.Rows)
                {
                    Session["ReqFilterItems"] = dt["Value"].ToString();
                }
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

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            //gvRef.Columns["CommandString"].Width = 0;
            //gvRef.Columns["RCommandString"].Width = 0;

            if (!IsPostBack)
            {
                param = "";
                Connection = Session["ConnString"].ToString();
                Session["ReqFilterItems"] = null;
                Session["ReqStepCode"] = "";
                Session["ReqStepCodeUpdate"] = "";
                Session["ReqDatatable"] = null;
                Session["ReqItemCategoryJO"] = "";
                Session["ReqRefType"] = "";
                WordStatus();

                gv1.DataSourceID = null;
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = true;
                }
                gv1.KeyFieldName = "LineNumber";
                dtpDeliveryDate.Value = DateTime.Now;
                txtTransType.Text = "REQUEST";

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Date = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                dtpDeliveryDate.Date = String.IsNullOrEmpty(_Entity.DeliveryDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DeliveryDate);

                //ADDED NEW
                sdsXDeal.SelectCommand = "  SELECT A.DocNumber, DocDate, ContractPeriodFrom, ContractPeriodTo, Recipient "
                                       + "  FROM Retail.XDeal A WHERE (A.ContractQty != A.IssuedQty OR A.ContractAmount != A.IssuedAmount) "
                                       + "  AND ('" + dtpDocDate.Text + "' BETWEEN CAST(ContractPeriodFrom AS date) AND CAST(ContractPeriodTo AS date )) "
                                       + "  AND ISNULL(A.ForceClosedBy,'') = '' AND ISNULL(ApprovedBy,'') != ''";
                sdsXDeal.DataBind();

                txtTransType.Value = _Entity.TransType;
                txtCostCenter.Value = _Entity.CostCenter;
                txtRequestedBy.Value = _Entity.RequestedBy;
                txtTotalQty.Text = _Entity.TotalQty.ToString();
                txtTotalBulkQty.Text = _Entity.TotalBulkQty.ToString();
                memRemarks.Text = _Entity.Remarks;
                cbxIsPrinted.Value = _Entity.IsPrinted;
                txtRefIssuance.Text = _Entity.RefIssuance;

                //samples
                cbxWithDIS.Value = _Entity.WithDIS;
                if (cbxWithDIS.Checked == false)
                {
                    txtDISNumber.Text = "";
                    txtDISNumber.ClientEnabled = false;
                }
                else
                {
                    txtDISNumber.Text = _Entity.DISNumber;
                    txtDISNumber.ClientEnabled = false;
                }

              
                FilterByMaterialType();
                //JO Materials Tab
                cbxCharge.Value = _Entity.IsCharge;
                cbxReplacement.Value = _Entity.IsReplacement;
                cbxIssued.Value = _Entity.IsIssued;
                glIssuingWarehouse.Text = _Entity.IssuingWarehouse;
                txtRefJOStep.Text = _Entity.RefJOStep;
                txtWorkCenter.Text = _Entity.WorkCenter;

                //ADDED NEW
                //glMaterialType.Text = _Entity.MaterialType;
                //specific = _Entity.SpecificMaterialType.ToString();
                //glSpecificMaterialType.Text = _Entity.SpecificMaterialType.ToString();
                //ChangeSource(_Entity.SpecificMaterialType.ToString());
                glMaterialType.Text = String.IsNullOrEmpty(_Entity.MaterialType) ? "DIRECT" : _Entity.MaterialType;
                specific = _Entity.SpecificMaterialType;
                glSpecificMaterialType.Value = String.IsNullOrEmpty(_Entity.SpecificMaterialType) ? "JO Bill Of Material" : _Entity.SpecificMaterialType.ToString();
                ChangeSource(String.IsNullOrEmpty(_Entity.SpecificMaterialType) ? "JO Bill Of Material" : _Entity.SpecificMaterialType.ToString());


                glRefJONumber.Text = _Entity.RefJONumber;

                //Complimentary Tab
                cmbComplimentaryType.Value = _Entity.ComplimentaryType;
                if (_Entity.ComplimentaryType != null && cmbComplimentaryType.Value != null)
                {
                    if (_Entity.ComplimentaryType.ToString() == "Others")
                        //ADDED NEW
                        //txtRefDoc.ClientEnabled = false;
                        glRefDoc.ClientEnabled = false;
                    else
                        //ADDED NEW
                        //txtRefDoc.ClientEnabled = true;
                        glRefDoc.ClientEnabled = true;
                }
                else
                    //ADDED NEW
                    //txtRefDoc.ClientEnabled = false;
                    glRefDoc.ClientEnabled = false;
                glRefDoc.Text = _Entity.RefDoc;
                txtAuthorizedBy.Text = _Entity.AuthorizedBy;
                txtAuthorizedDate.Text = _Entity.AuthorizedDate;

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
                txtHApprovedBy.Text = _Entity.ApprovedBy;
                txtHApprovedDate.Text = _Entity.ApprovedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;
                txtclosedby.Text = _Entity.ForceClosedBy;
                txtcloseddate.Text = _Entity.ForceClosedDate;

                ModuleUsed();

                //ADDED NEW 
                FilterByMaterialType();

                gv1.KeyFieldName = "LineNumber";

                //ADDED NEW        
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
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        break;
                    case "E": 
                        //ADDED NEW
                        if(String.IsNullOrEmpty(txtRequestedBy.Text.ToString()))
                        {
                            DefaultVal();
                        } 
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true; 
                        break;
                    case "V":
                        memRemarks.ReadOnly = true;
                        view = true;
                        updateBtn.Text = "Close";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.RequestDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

            }
            DefaultVal();

            //ADDED NEW
            Session["DocDate"] = dtpDocDate.Text;
            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());
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
            gparam._TransType = Request.QueryString["transtype"].ToString();
            strresult += GearsInventory.GInventory.Request_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Posting
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Table = "Inventory.Request";
            gparam._Factor = -1;
            strresult += GearsInventory.GInventory.Request_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion


        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = true;
            }
            else {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = false;
            }
        }
        protected void MemoLoad(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                var memo = sender as ASPxMemo;
                memo.ReadOnly = true;
            }
            else
            {
                var memo = sender as ASPxMemo;
                memo.ReadOnly = false;
            }
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
                look.ReadOnly = true;
            }
            

            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = true;
                look.ReadOnly = false;
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {

                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = false;
                look.ReadOnly = true;
            }
            else
            {
                ASPxGridLookup looka = sender as ASPxGridLookup;
                looka.DropDownButton.Enabled = true;
                looka.ReadOnly = false;
            }
        }

        protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = true;
            }
            else
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {


            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                var check = sender as ASPxCheckBox;
                check.Enabled = false;
            }
            else
            {
                var check = sender as ASPxCheckBox;
                check.Enabled = true;
            }
        }

        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                var combo = sender as ASPxComboBox;
                combo.DropDownButton.Enabled = false;
                combo.ReadOnly = true;

                
            }
            else
            {
                var combo = sender as ASPxComboBox;
                combo.DropDownButton.Enabled = true;
                combo.ReadOnly = false;

            }



        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        { }

        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxDateEdit date = sender as ASPxDateEdit;
                date.Enabled = false;
                date.ReadOnly = true;
            }
            else {
                ASPxDateEdit date = sender as ASPxDateEdit;
                date.Enabled = true;
                date.ReadOnly = false;
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
                spinedit.ReadOnly = true;
            }
            else {
                ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
                spinedit.ReadOnly = false;
            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
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
                if (glSpecificMaterialType.Text == "Design Info Sheet" || glSpecificMaterialType.Text == "JO Bill Of Material" || glSpecificMaterialType.Text == "Service Order")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }
                }
                else if (glSpecificMaterialType.Text == "Chemical" || glSpecificMaterialType.Text == "Factory Supplies")
                {
                    if (!String.IsNullOrEmpty(glRefJONumber.Text))
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                        {
                            e.Visible = false;
                        }
                    }
                    else
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                        {
                            e.Visible = true;
                        }
                    }
                }
                else
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
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
                if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
           if(glSpecificMaterialType.Text == "Design Info Sheet" || glSpecificMaterialType.Text == "JO Bill Of Material" || glSpecificMaterialType.Text == "Service Order")
            {
                if (e.ButtonID == "Details" || e.ButtonID == "Delete")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
            }
            else if (glSpecificMaterialType.Text == "Chemical" || glSpecificMaterialType.Text == "Factory Supplies")
            {
                if (!String.IsNullOrEmpty(glRefJONumber.Text))
                {
                    if (e.ButtonID == "Delete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
            if (Request.QueryString["transtype"].ToString() == "INVOSR")
            {
                if (e.ButtonID == "CountSheet")
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
                        //ADDED NEW
                        //if (Session["STRsql"] != null)
                        //{
                        //    gridLookup.GridView.DataSourceID = "sdsItemDetail";
                        //}
                        //else
                        //{
                        //    gridLookup.GridView.DataSourceID = "sdsItemDetail";
                        //}
                        gridLookup.GridView.DataSourceID = "sdsItemDetail";
                        sdsItemDetail.SelectCommand = Session["STRsql"].ToString(); 
                        gridLookup.DataBind();
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


            if (e.Parameters.Contains("ItemCode"))
            { 
                DataTable CountAll = Gears.RetriveData2("SELECT C.ColorCode, C.ClassCode, C.SizeCode, "
                                     + " A.UnitBase, A.FullDesc, A.UnitBulk, A.IsByBulk "
                                     + " FROM Masterfile.Item A LEFT JOIN Masterfile.ItemDetail C "
                                     + " ON A.ItemCode = C.ItemCode WHERE A.itemcode = '" + itemcode + "'"
                                     , Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in CountAll.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["IsByBulk"].ToString() + ";";
                }

                DataTable CountPrice = Gears.RetriveData2("SELECT TOP 1 Price, StatusCode FROM Retail.StockMasterPriceHistory WHERE StockNumber = '" + itemcode + "' AND (EffectivityDate <= '" + Session["DocDate"].ToString() + "'OR EffectivityDate IS NULL) ORDER BY EffectivityDate DESC", Session["ConnString"].ToString());
                foreach (DataRow dt in CountPrice.Rows)
                {
                    codes += dt["Price"].ToString() + ";";
                    codes += dt["StatusCode"].ToString() + ";";
                } 

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {

                if (e.Parameters.Contains("ColorCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "ColorCode";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "ClassCode";
                }
                if (e.Parameters.Contains("SizeCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "SizeCode";
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
        }
        


        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Transaction = Request.QueryString["transtype"].ToString();
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.DeliveryDate = String.IsNullOrEmpty(dtpDeliveryDate.Text) ? null : dtpDeliveryDate.Text;
            _Entity.TransType = String.IsNullOrEmpty(txtTransType.Text) ? null : txtTransType.Text;
            _Entity.CostCenter = String.IsNullOrEmpty(txtCostCenter.Text) ? null : txtCostCenter.Text;
            _Entity.TotalQty = String.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToDecimal(txtTotalQty.Value.ToString());
            _Entity.TotalBulkQty = String.IsNullOrEmpty(txtTotalBulkQty.Text) ? 0 : Convert.ToDecimal(txtTotalBulkQty.Value.ToString());
            _Entity.RequestedBy = String.IsNullOrEmpty(txtRequestedBy.Text) ? null : txtRequestedBy.Text;
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.RefIssuance = String.IsNullOrEmpty(txtRefIssuance.Text) ? null : txtRefIssuance.Text;
            _Entity.IsPrinted = Convert.ToBoolean(cbxIsPrinted.Value.ToString());
            _Entity.WithDIS = Convert.ToBoolean(cbxWithDIS.Value.ToString());
            _Entity.DISNumber = String.IsNullOrEmpty(txtDISNumber.Text) ? null : txtDISNumber.Text;
            _Entity.ComplimentaryType = String.IsNullOrEmpty(cmbComplimentaryType.Text) ? null : cmbComplimentaryType.Value.ToString();
            
            //ADDED NEW
            //_Entity.RefDoc = String.IsNullOrEmpty(txtRefDoc.Text) ? null : txtRefDoc.Text;
            _Entity.RefDoc = String.IsNullOrEmpty(glRefDoc.Text) ? null : glRefDoc.Text;

            _Entity.RefJONumber = String.IsNullOrEmpty(glRefJONumber.Text) ? null : glRefJONumber.Text;
            _Entity.IsCharge = Convert.ToBoolean(cbxCharge.Value.ToString());
            _Entity.IsReplacement = Convert.ToBoolean(cbxReplacement.Value.ToString());
            _Entity.IsIssued = Convert.ToBoolean(cbxIssued.Value.ToString());
            _Entity.IssuingWarehouse = String.IsNullOrEmpty(glIssuingWarehouse.Text) ? null : glIssuingWarehouse.Text;
            _Entity.RefJOStep = String.IsNullOrEmpty(txtRefJOStep.Text) ? null : txtRefJOStep.Text;
            _Entity.MaterialType = String.IsNullOrEmpty(glMaterialType.Text) ? null : glMaterialType.Text;
            _Entity.SpecificMaterialType = String.IsNullOrEmpty(glSpecificMaterialType.Text) ? null : glSpecificMaterialType.Text;
            _Entity.WorkCenter = String.IsNullOrEmpty(txtWorkCenter.Text) ? null : txtWorkCenter.Text;


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

            _Entity.Transaction = Request.QueryString["transtype"].ToString();

            param = e.Parameter.Split('|')[0];



            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM Inventory.Request WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());




            DataTable dt = new DataTable();
            if (Session["ReqDatatable"] == "1")
            {

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
            }


            switch (param)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();
                    string strError = Functions.Submitted(_Entity.DocNumber, "Inventory.Request", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
                    
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["ReqDatatable"] == "1")
                        {
                           
                            _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());

                            Session["ReqStepCode"] = Session["ReqStepCodeUpdate"].ToString();
                            gv1.DataSource = dt;
                            if (gv1.DataSourceID != null)
                            {
                                gv1.DataSourceID = null;
                            }
                            gv1.DataBind();
                            //gv1.DataSourceID = sdsSODetail.ID;
                            gv1.UpdateEdit();
                          
                            //gv1.DataSourceID = "odsDetail";
                            //Session["ReqStepCode"] = Session["ReqStepCodeUpdate"].ToString();
                            //gv1.DataBind();
                            //gv1.UpdateEdit();
                            Session["ReqStepCode"] = "";
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid


                        }
                        //_Entity.SubstituteInfo(txtDocNumber.Text, aglCustomerCode.Text);

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

                case "MaterialType":
                    
                    glSpecificMaterialType.Text = "";
                    cp.JSProperties["cp_refdel"] = true;
                    FilterByMaterialType();
                    txtRefJOStep.Text = "";
                    glRefJONumber.Text = "";
                    txtWorkCenter.Text = "";
                    cp.JSProperties["cp_generated"] = true;
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    gv1.DataSourceID = "sdsDetail";
                    break;

                case "DISChecked":
                    if (cbxWithDIS.Checked == false)
                    {
                        txtDISNumber.ClientEnabled = false;
                        txtDISNumber.Text = "";
                    }
                    else txtDISNumber.ClientEnabled = true;
                    break;

                case "CompType":
                    if (cmbComplimentaryType.Text == "Pre-Approved")
                    {
                        //ADDED NEW
                        //txtRefDoc.ClientEnabled = true;
                        glRefDoc.ClientEnabled = true;
                    }
                    else
                    {
                        //ADDED NEW
                        //txtRefDoc.ClientEnabled = false;
                        //txtRefDoc.Text = "";
                        glRefDoc.ClientEnabled = false;
                        glRefDoc.Text = "";
                    }
                    break;

                case "FilterRefDoc":
                   
                    cp.JSProperties["cp_refdel"] = true;
                    ChangeSource(glSpecificMaterialType.Text);
                    txtRefJOStep.Text = "";
                    glRefJONumber.Text = "";
                    txtWorkCenter.Text = "";
                    cp.JSProperties["cp_generated"] = true;
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    gv1.DataSourceID = "sdsDetail"; 
                    break;

                case "JOStep":
                    Session["ReqStepCode"] = e.Parameter;
                    Session["ReqStepCodeUpdate"] = e.Parameter;
                    cp.JSProperties["cp_refdel"] = true;
                    GetSelectedVal();
                    Session["ReqStepCode"] = "";
                    DefaultVal();
                    cp.JSProperties["cp_generated"] = true; 
                    break; 

                case "XDeal":
                    sdsXDeal.SelectCommand = "  SELECT A.DocNumber, DocDate, ContractPeriodFrom, ContractPeriodTo, Recipient "
                                       + "  FROM Retail.XDeal A WHERE (A.ContractQty != A.IssuedQty OR A.ContractAmount != A.IssuedAmount) "
                                       + "  AND ('" + dtpDocDate.Text + "' BETWEEN CAST(ContractPeriodFrom AS date) AND CAST(ContractPeriodTo AS date )) "
                                       + "  AND ISNULL(A.ForceClosedBy,'') = '' AND ISNULL(ApprovedBy,'') != ''";
                    sdsXDeal.DataBind();
                    glRefDoc.Text = "";
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { }

        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["sodetail"] = null;
            }

            if (Session["sodetail"] != null)
            {
                gv1.DataSourceID = sdsSODetail.ID;
            }
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
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
            switch (Request.QueryString["transtype"].ToString())
            {
                case "INVOSR":
                    filter = "ITMCAT_OS";
                    break;

                case "INVSAR":
                    filter = "ITMCAT_FOB";
                    break;

                case "INVCOR":
                    filter = "ITMCAT_FOB";
                    break;

                case "INVASR":
                    filter = "ITMCAT_ASI";
                    break;

            }

            if (!String.IsNullOrEmpty(filter))
            {
                DataTable filteritem = new DataTable();
                filteritem = Gears.RetriveData2("SELECT REPLACE(Value,',',''',''') AS Value FROM IT.SystemSettings WHERE Code ='" + filter + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in filteritem.Rows)
                {
                    Session["ReqFilterItems"] = dt["Value"].ToString();
                }
            }

            if (Session["ReqFilterItems"] == null)
            {
                Session["ReqFilterItems"] = "";
            }
            if (!String.IsNullOrEmpty(Session["ReqFilterItems"].ToString()))
            {
                if (Session["ReqFilterItems"].ToString() != "ITMCAT_ASI")
                {
                    sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] " +
                    "WHERE ISNULL(IsInactive,0) = 0 AND ISNULL(ItemCategoryCode,0) IN ('" + Session["ReqFilterItems"].ToString() + "') ORDER BY ItemCode ASC";
                }
                else if (Session["ReqFilterItems"].ToString() == "ITMCAT_ASI")
                {
                    sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] A " +
                    "INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode " +
                    "WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(IsAsset,0) = 1 ORDER BY ItemCode ASC";
                }
            }
            else
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 ORDER BY ItemCode ASC";
            }
        }

        public void FilterItemJO()
        {
            string itemcatJO = "";
            string finalcatJO = "";

            if (glSpecificMaterialType.Text == "Chemical")
            {
                itemcatJO = "ITMCAT_CHM";
            }
            else
            {
                itemcatJO = "ITMCAT_FS";
            }

            DataTable one = new DataTable();
            one = Gears.RetriveData2("SELECT REPLACE(Value,',',''',''') AS Value FROM IT.SystemSettings WHERE Code ='" + itemcatJO + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in one.Rows)
            {
                finalcatJO = dt["Value"].ToString();
            }

            Session["ReqItemCategoryJO"] = finalcatJO;

            if (glSpecificMaterialType.Text == "Chemical")
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] " +
                    "WHERE ISNULL(IsInactive,0) = 0 AND ItemCategoryCode = '" + finalcatJO + "' ORDER BY ItemCode ASC";
            }
            else if (glSpecificMaterialType.Text == "Factory Supplies")
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] " +
                    "WHERE ISNULL(IsInactive,0) = 0 AND ItemCategoryCode = '" + finalcatJO + "' ORDER BY ItemCode ASC";
            }
            else
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 ORDER BY ItemCode ASC";
            }
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;

            if (Request.QueryString["transtype"].ToString() == "INVJOR")
            {
                FilterItemJO();
            }
            else
            {
                FilterItem();
            }
            grid.DataSourceID = "sdsItem";
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

        protected void FilterByMaterialType()
        {
            if (!String.IsNullOrEmpty(glMaterialType.Text))
            {
                switch (glMaterialType.Text)
                {
                    case "DIRECT":
                        sdsSpecificMaterial.SelectCommand = "SELECT DISTINCT Code, Description FROM IT.GenericLookup WHERE LookUpKey = 'JODIRECT'";
                        glSpecificMaterialType.DataBind();
                        break;
                    case "INDIRECT":
                        sdsSpecificMaterial.SelectCommand = "SELECT DISTINCT Code, Description FROM IT.GenericLookup WHERE LookUpKey = 'JOINDIRECT'";
                        glSpecificMaterialType.DataBind();
                        break;
                }
            }
        }

        protected void ModuleUsed()
        {
            DataTable mod = new DataTable();
            mod = Gears.RetriveData2("SELECT DISTINCT Description FROM IT.GenericLookup WHERE LookUpKey ='REQTYPE' AND Code ='" + Request.QueryString["transtype"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

            foreach (DataRow md in mod.Rows)
            {
                txtType.Text = md["Description"].ToString().Trim();
                HeaderText.Text = md["Description"].ToString().Trim() + " Request";
            }

            if (String.IsNullOrEmpty(txtTransType.Text))
            {
                txtTransType.Text = "REQUEST";
            }

            switch (Request.QueryString["transtype"].ToString())
            {
                case "INVOSR":
                case "INVASR":
                    frmlayout1.FindItemOrGroupByName("JOMaterialGroup").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("ComplimentaryGroup").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("SamplesGroup").ClientVisible = false;
                    txtAuthorizedBy.ClientVisible = false;
                    txtAuthorizedDate.ClientVisible = false;
                    var formLayout0 = frmlayout1 as ASPxFormLayout;
                    var AuthorizedBy0 = formLayout0.FindItemOrGroupByName("authby");
                    var AuthorizedDate0 = formLayout0.FindItemOrGroupByName("authdate");
                    AuthorizedBy0.ClientVisible = false;
                    AuthorizedDate0.ClientVisible = false;
                    gv1.Columns["ItemPrice"].Width = 0;
                    gv1.Columns["ReturnedQty"].Width = 0;
                    gv1.Columns["ReturnedBulkQty"].Width = 0;
                    gv1.Columns["Cost"].Width = 0;
                    break;
                case "INVSAR":
                    frmlayout1.FindItemOrGroupByName("JOMaterialGroup").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("ComplimentaryGroup").ClientVisible = false;
                    txtAuthorizedBy.ClientVisible = false;
                    txtAuthorizedDate.ClientVisible = false;
                    var formLayout = frmlayout1 as ASPxFormLayout;
                    var AuthorizedBy = formLayout.FindItemOrGroupByName("authby");
                    var AuthorizedDate = formLayout.FindItemOrGroupByName("authdate");
                    AuthorizedBy.ClientVisible = false;
                    AuthorizedDate.ClientVisible = false;
                    gv1.Columns["ItemPrice"].Width = 0;
                    gv1.Columns["ReturnedQty"].Width = 0;
                    gv1.Columns["ReturnedBulkQty"].Width = 0;
                    gv1.Columns["Cost"].Width = 0;
                    break;
                case "INVJOR":
                    frmlayout1.FindItemOrGroupByName("JOMaterialGroup").ClientVisible = true;
                    frmlayout1.FindItemOrGroupByName("ComplimentaryGroup").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("SamplesGroup").ClientVisible = false;
                    txtAuthorizedBy.ClientVisible = false;
                    txtAuthorizedDate.ClientVisible = false;
                    var formLayout1 = frmlayout1 as ASPxFormLayout;
                    var AuthorizedBy1 = formLayout1.FindItemOrGroupByName("authby");
                    var AuthorizedDate1 = formLayout1.FindItemOrGroupByName("authdate");
                    var RequestedBy1 = formLayout1.FindItemOrGroupByName("requestedby");
                    var Printed = formLayout1.FindItemOrGroupByName("Print");
                    AuthorizedBy1.ClientVisible = false;
                    AuthorizedDate1.ClientVisible = false;
                    RequestedBy1.ClientVisible = false;
                    RequestedBy1.VisibleIndex = 11;
                    Printed.VisibleIndex = 12;
                    gv1.Columns["ItemPrice"].Width = 0;
                    gv1.Columns["ReturnedQty"].Width = 100;
                    gv1.Columns["ReturnedBulkQty"].Width = 100;
                    gv1.Columns["Cost"].Width = 0;
                    break;
                case "INVCOR":
                    frmlayout1.FindItemOrGroupByName("JOMaterialGroup").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("ComplimentaryGroup").ClientVisible = true;
                    frmlayout1.FindItemOrGroupByName("SamplesGroup").ClientVisible = false;
                    txtAuthorizedBy.ClientVisible = true;
                    txtAuthorizedDate.ClientVisible = true;
                    txtTotalBulkQty.ClientVisible = false;
                    var formLayout2 = frmlayout1 as ASPxFormLayout;
                    var TotalBulk = formLayout2.FindItemOrGroupByName("TotalBulkQty");
                    TotalBulk.ClientVisible = false;
                    TotalBulk.VisibleIndex = 13;
                    gv1.Columns["ItemPrice"].Width = 100;
                    gv1.Columns["ReturnedQty"].Width = 0;
                    gv1.Columns["ReturnedBulkQty"].Width = 0;
                    gv1.Columns["Cost"].Width = 0;
                    break;
            }

        }
        protected void WordStatus()
        {
            DataTable mod = new DataTable();
            mod = Gears.RetriveData2("SELECT Status FROM Inventory.Request WHERE DocNumber = '" + Request.QueryString["docnumber"].ToString() + "' AND Type = '" + Request.QueryString["transtype"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

            foreach (DataRow md in mod.Rows)
            {
                switch (md["Status"].ToString())
                {
                    case "N":
                        txtStatus.Text = "New";
                        break;
                    case "C":
                        txtStatus.Text = "Closed";
                        break;
                    case "X":
                        txtStatus.Text = "ManualClosed";
                        break;
                    case "L":
                        txtStatus.Text = "Cancelled";
                        break;
                    case "P":
                        txtStatus.Text = "Partial";
                        break;
                    case "A":
                        txtStatus.Text = "Partial Closed";
                        break;
                }
            }
        }
        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                //sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0)=0";
                FilterItem();
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



                //ADDED NEW
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT UPPER(ColorCode) AS ColorCode, UPPER(ClassCode) AS ClassCode,UPPER(SizeCode) AS SizeCode,UPPER(UnitBase) AS UnitBase, " +
                                                       " FullDesc, UPPER(UnitBulk) AS UnitBulk, A.IsByBulk, ISNULL(StatusCode,'') AS StatusCode " +
                                                       " from masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());
                //jay
                foreach (DataRow dt in countcolor.Rows)
                { 
                    codes = "";

                    int ColorCount = countcolor.AsEnumerable().Select(row => row.Field<string>("ColorCode")).Distinct().Count();
                    codes += ColorCount > 1 ? ";" : dt["ColorCode"].ToString() + ";"; 

                    int ClassCount = countcolor.AsEnumerable().Select(row => row.Field<string>("ClassCode")).Distinct().Count();
                    codes += ClassCount > 1 ? ";" : dt["ClassCode"].ToString() + ";";

                    int SizeCount = countcolor.AsEnumerable().Select(row => row.Field<string>("SizeCode")).Distinct().Count();
                    codes += SizeCount > 1 ? ";" : dt["SizeCode"].ToString() + ";";

                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";



                }
                DataTable CountPrice = Gears.RetriveData2("SELECT TOP 1 Price, StatusCode FROM Retail.StockMasterPriceHistory WHERE StockNumber = '" + itemcode + "' AND (EffectivityDate <= '" + Session["DocDate"].ToString() + "'OR EffectivityDate IS NULL) ORDER BY EffectivityDate DESC", Session["ConnString"].ToString());
                foreach (DataRow dt in CountPrice.Rows)
                {
                    codes += dt["Price"].ToString() + ";";
                    codes += dt["StatusCode"].ToString() + ";";
                }

              



                //DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                //                                                     "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                //if (getColor.Rows.Count > 1)
                //{
                //    codes = "" + ";";
                //    Session["ColorCode"] = "";
                //}
                //else
                //{
                //    foreach (DataRow dt in getColor.Rows)
                //    {
                //        codes = dt["ColorCode"].ToString() + ";";
                //        Session["ColorCode"] = dt["ColorCode"].ToString();
                //    }
                //}

                //DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                //                                                         "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                //if (getClass.Rows.Count > 1)
                //{
                //    codes += "" + ";";
                //    Session["ClassCode"] = "";
                //}
                //else
                //{
                //    foreach (DataRow dt in getClass.Rows)
                //    {
                //        codes += dt["ClassCode"].ToString() + ";";
                //        Session["ClassCode"] = dt["ClassCode"].ToString();
                //    }
                //}

                //DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                //                                                             "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                //if (getSize.Rows.Count > 1)
                //{
                //    codes += "" + ";";
                //    Session["SizeCode"] = "";
                //}
                //else
                //{
                //    foreach (DataRow dt in getSize.Rows)
                //    {
                //        codes += dt["SizeCode"].ToString() + ";";
                //        Session["SizeCode"] = dt["SizeCode"].ToString();
                //    }
                //}


                //DataTable getUnitBase = Gears.RetriveData2("SELECT DISTINCT UnitBase FROM Masterfile.item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                //if (getUnitBase.Rows.Count == 0)
                //{
                //    codes += "" + ";";
                //}
                //else
                //{
                //    foreach (DataRow dt in getUnitBase.Rows)
                //    {
                //        codes += dt["UnitBase"].ToString() + ";";
                //    }
                //}

                //DataTable getRequestedBulkUnit = Gears.RetriveData2("select distinct UnitBulk from Masterfile.Unit a inner join masterfile.Item b ON a.UnitCode = b.UnitBulk where ItemCode =  '" + itemcode + "'", Session["ConnString"].ToString());

                //if (getRequestedBulkUnit.Rows.Count == 0)
                //{
                //    codes += "" + ";";
                //}
                //else {
                //    foreach (DataRow dt in getRequestedBulkUnit.Rows)
                //    {
                //        codes += dt["UnitBulk"].ToString() + ";";
                //        Session["UnitBulk"] = dt["UnitBulk"].ToString();
                //    }
                
                //}

                //DataTable getFullDesc = Gears.RetriveData2("select * from masterfile.Item where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                //if (getFullDesc.Rows.Count == 0)
                //{
                //    codes += "" + ";";
                //}
                //else
                //{
                //    foreach (DataRow dt in getFullDesc.Rows)
                //    {
                //        codes += dt["FullDesc"].ToString() + ";";
                //        Session["FullDesc"] = dt["FullDesc"].ToString();
                //    }

                //}


                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
        public void DefaultVal()
        {
            DataTable def = new DataTable();
            if (String.IsNullOrEmpty(txtWorkCenter.Text))
            {
                def = Gears.RetriveData2("select CostCenterCode from Masterfile.BPEmployeeInfo WHERE EmployeeCode ='" + Session["Userid"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN
            }
            else
            {
                def = Gears.RetriveData2("select CostCenterCode from Masterfile.BPSupplierInfo where SupplierCode ='" + txtWorkCenter.Text + "'", Session["ConnString"].ToString());//ADD CONN
            }
            foreach (DataRow df in def.Rows)
            {
                txtCostCenter.Text = df["CostCenterCode"].ToString();
            }
            if (Request.QueryString["transtype"].ToString() != "INVJOR")
            {
                txtRequestedBy.Text = Session["Userid"].ToString();
            }
        }

        public void ChangeSource(string Type)
        {
            switch (Type)
            {
                case "JO Bill Of Material":
                    sdsRefDoc.SelectCommand = " SELECT DISTINCT A.DocNumber, ISNULL(B.StepCode,'') AS Code, ISNULL(WorkCenter,'') AS WorkCenter FROM Production.JobOrder A INNER JOIN Production.JOStep B "
                                            + " ON A.DocNumber = B.DocNumber INNER JOIN Production.JOStepPlanning C ON B.StepCode = C.StepCode AND A.DocNumber = C.DocNumber WHERE "
                                            + " (ISNULL(Status,'N') IN ('W','C')) OR (ISNULL(Status,'N') = 'N' AND ISNULL(A.ProdSubmittedBy,'') != '') ";
                    break;
                case "Chemical":
                case "Factory Supplies":
                    sdsRefDoc.SelectCommand = " SELECT DISTINCT A.DocNumber, ISNULL(B.StepCode,'') AS Code, ISNULL(WorkCenter,'') AS WorkCenter "
                                            + " FROM Production.JobOrder A INNER JOIN Production.JOStep B ON A.DocNumber = B.DocNumber "
                                            + " INNER JOIN Production.JOStepPlanning C ON B.StepCode = C.StepCode AND B.DocNumber = C.DocNumber "
                                            + " INNER JOIN Production.JOBillOfMaterial D ON B.DocNumber = D.DocNumber AND B.StepCode = D.StepCode "
                                            + " LEFT JOIN Masterfile.Item E ON E.ItemCode = D.ItemCode WHERE (ISNULL(Status,'N') IN ('W','C')) OR "
                                            + " (ISNULL(Status,'N') = 'N' AND ISNULL(A.ProdSubmittedBy,'') != '') AND E.ItemCategoryCode = '" + Session["ReqItemCategoryJO"] + "'";
                    break;
                case "Design Info Sheet":
                    sdsRefDoc.SelectCommand = " SELECT DISTINCT A.DocNumber, ISNULL(Step,'') AS Code, ISNULL(WorkCenter,'') AS WorkCenter FROM Production.DIS A INNER JOIN Production.DISStep B ON A.DocNumber = B.DocNumber "
                                            + " WHERE (ISNULL(Status,'N') IN ('W')) OR (ISNULL(Status,'N') = 'N' AND ISNULL(A.SubmittedBy,'') != '') ";
                    break;
                case "Service Order":
                    sdsRefDoc.SelectCommand = " SELECT DISTINCT A.DocNumber, ISNULL(RecordID,'') AS Code, ISNULL(WorkCenter,'') AS WorkCenter from Procurement.SOWorkOrder A INNER JOIN Procurement.ServiceOrder B "
                                            + " ON A.DocNumber = B.DocNumber WHERE ISNULL(B.Status,'N') = 'W' OR (ISNULL(Status,'') = 'N' AND ISNULL(B.SubmittedBy,'') != '')";
                    break;
            }
         
            glRefJONumber.DataSourceID = "sdsRefDoc";
            glRefJONumber.DataBind();
        }

        private DataTable GetSelectedVal()
        {
            Session["ReqDatatable"] = "0";
            gv1.DataSourceID = null;
            gv1.DataBind();

            DataTable dt = new DataTable();

            sdsTransDetail.SelectCommand = CascadeDetailJO(glSpecificMaterialType.Text, Session["ReqStepCode"].ToString());
            sdsTransDetail.DataBind();
            gv1.DataSource = sdsTransDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session["ReqDatatable"] = "1";

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

            return dt;

        }

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["ReqDatatable"] == "1" && check == true)
            { 
                e.Handled = true;
                DataTable source = GetSelectedVal();


                //ADDED NEW
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["RequestedQty"] = values.NewValues["RequestedQty"];
                    row["RequestedBulkUnit"] = values.NewValues["RequestedBulkUnit"];
                    row["ExpDate"] = values.NewValues["ExpDate"];
                    row["MfgDate"] = values.NewValues["MfgDate"];
                    row["BatchNo"] = values.NewValues["BatchNo"];
                    row["LotNo"] = values.NewValues["LotNo"];
                }


                int ln = 1;
                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    //var LineNumber = values.NewValues["LineNumber"];
                    var LineNumber = ln;
                    var ItemCode = values.NewValues["ItemCode"];
                    var FullDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var Unit = values.NewValues["Unit"];
                    var RequestedBulkUnit = values.NewValues["RequestedBulkUnit"];
                    var RequestedQty = values.NewValues["RequestedQty"];
                    var RequestedBulkQty = values.NewValues["RequestedBulkQty"];
                    var IssuedQty = values.NewValues["IssuedQty"];
                    var ReturnedQty = values.NewValues["ReturnedQty"];
                    var ItemPrice = values.NewValues["ItemPrice"];
                    var ReturnedBulkQty = values.NewValues["ReturnedBulkQty"];
                    var Cost = values.NewValues["Cost"];
                    var Version = values.NewValues["Version"];
                     
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
                    source.Rows.Add(txtDocNumber.Text, LineNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode,
                                    Unit, ItemPrice, RequestedQty, RequestedBulkQty, RequestedBulkUnit, IssuedQty, ReturnedQty,
                                    ReturnedBulkQty, Cost, ExpDate, MfgDate, BatchNo, LotNo, Field1, Field2, Field3, Field4,
                                    Field5, Field6, Field7, Field8, Field9, Version);
                    ln++;
                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.RequestedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["RequestedQty"]) ? 0 : dtRow["RequestedQty"]);
                    _EntityDetail.RequestedBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["RequestedBulkQty"]) ? 0 : dtRow["RequestedBulkQty"]);
                    _EntityDetail.RequestedBulkUnit = dtRow["RequestedBulkUnit"].ToString();
                    _EntityDetail.ExpDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["ExpDate"]) ? null : dtRow["ExpDate"]);
                    _EntityDetail.MfgDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["MfgDate"]) ? null : dtRow["MfgDate"]);
                    _EntityDetail.BatchNo = dtRow["BatchNo"].ToString();
                    _EntityDetail.LotNo = dtRow["LotNo"].ToString();
                    //_EntityDetail.IssuedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["IssuedQty"]) ? 0 : dtRow["IssuedQty"]); ;
                    //_EntityDetail.ReturnedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedQty"]) ? 0 : dtRow["ReturnedQty"]);
                    //_EntityDetail.ReturnedBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedBulkQty"]) ? 0 : dtRow["ReturnedBulkQty"]);
                    _EntityDetail.AddRequestDetail(_EntityDetail);
                }
            }
        }

        public string CascadeDetailJO(string Type, string param)
        {
            string query = "";
            if (!String.IsNullOrEmpty(param))
            {
                string a = param.Split('|')[1];
                string b = param.Split('|')[2];
                string c = param.Split('|')[3];
                //var a = glRefJONumber.GridView.GetRowValues(glRefJONumber.GridView.FocusedRowIndex, "DocNumber");
                //var b = glRefJONumber.GridView.GetRowValues(glRefJONumber.GridView.FocusedRowIndex, "Code");

                switch (Type)
                {
                    case "JO Bill Of Material":
                        txtRefJOStep.Text = b.ToString();
                        txtWorkCenter.Text = c.ToString();
                        //ADDED NEW
                        //query = " SELECT DISTINCT '"+ txtDocNumber.Text +"' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc ,"
                        //      + " A.ColorCode, A.ClassCode, A.SizeCode, A.Unit, ISNULL(A.Consumption,0) + ISNULL(AllowanceQty,0) AS RequestedQty, "
                        //      + " 0 AS RequestedBulkQty, B.UnitBulk AS RequestedBulkUnit, 0.0000 AS IssuedQty, 0.00 AS IssuedBulkQty, 0.0000 AS ReturnedQty, 0.00 AS ReturnedBulkQty, A.Field1, "
                        //      + " A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Production.JOBillOfMaterial A "
                        //      + " LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode "
                        //      + " WHERE A.DocNumber = '" + a.ToString() + "' AND A.StepCode = '" + b.ToString() + "'";
                        query = " SELECT DISTINCT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc ,"
                              + " A.ColorCode, A.ClassCode, A.SizeCode, A.Unit, ISNULL(A.Consumption,0) + ISNULL(A.AllowanceQty,0) AS RequestedQty, "
                              + " 0.00 AS RequestedBulkQty, B.UnitBulk AS RequestedBulkUnit, 0.0000 AS IssuedQty, 0.00 AS IssuedBulkQty, 0.0000 AS ReturnedQty, 0.00 AS ReturnedBulkQty, A.Field1, "
                              + " A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Production.JOMaterialMovement A "
                              + " INNER JOIN Production.JOBillOfMaterial C ON A.DocNumber = C.DocNumber AND A.StepCode = C.StepCode AND A.ItemCode = C.ItemCode AND A.ColorCode = C.ColorCode AND A.SizeCode = C.SizeCode LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode "
                              + " WHERE A.DocNumber = '" + a.ToString() + "' AND A.StepCode = '" + b.ToString() + "' AND ISNULL(C.IsExcluded,0) != 1";
                        break;
                    case "Chemical":
                    case "Factory Supplies":
                        txtRefJOStep.Text = b.ToString();
                        txtWorkCenter.Text = c.ToString();
                        //ADDED NEW
                        //query = " SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc ,A.ColorCode, "
                        //      + " A.ClassCode, A.SizeCode, A.Unit, 0.0000 AS RequestedQty, 0 AS RequestedBulkQty, "
                        //      + " B.UnitBulk AS RequestedBulkUnit, 0.0000 AS IssuedQty, 0.0000 AS ReturnedQty, 0 AS ReturnedBulkQty, A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, "
                        //      + " A.Field6, A.Field7, A.Field8, A.Field9, '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode "
                        //      + " WHERE A.DocNumber = '" + a.ToString() + "' AND A.StepCode = '" + b.ToString()
                        //      + "' AND B.ItemCategoryCode = '" + Session["ReqItemCategoryJO"] + "'";
                        query = " SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc ,A.ColorCode, "
                              + " A.ClassCode, A.SizeCode, A.Unit, 0.0000 AS RequestedQty, 0 AS RequestedBulkQty, "
                              + " B.UnitBulk AS RequestedBulkUnit, 0.0000 AS IssuedQty, 0.0000 AS ReturnedQty, 0 AS ReturnedBulkQty, A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, "
                              + " A.Field6, A.Field7, A.Field8, A.Field9, '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode "
                              + " WHERE A.DocNumber = '" + a.ToString() + "' AND A.StepCode = '" + b.ToString()
                              + "' AND B.ItemCategoryCode = '" + Session["ReqItemCategoryJO"] + "'";
                        break;
                    case "Design Info Sheet":
                        txtRefJOStep.Text = b.ToString();
                        txtWorkCenter.Text = c.ToString();
                        //ADDED NEW
                        //query = " SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc, A.ColorCode, "
                        //      + " A.ClassCode, A.SizeCode, B.UnitBase AS Unit, A.Consumption AS RequestedQty, 0 AS RequestedBulkQty, "
                        //      + " B.UnitBulk AS RequestedBulkUnit, 0.0000 AS IssuedQty, 0.0000 AS ReturnedQty, 0 AS ReturnedBulkQty, A.Field1, A.Field2, A.Field3, "
                        //      + " A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Production.DISBillOfMaterial A "
                        //      + " LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode INNER JOIN Production.DISStep C ON A.DocNumber = C.DocNumber AND A.Step = C.Step "
                        //      + " WHERE A.DocNumber = '" + a.ToString() + "' AND A.Step = '" + b.ToString() + "'";
                        query = " SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc, A.ColorCode, "
                              + " A.ClassCode, A.SizeCode, B.UnitBase AS Unit, A.Consumption AS RequestedQty, 0 AS RequestedBulkQty, "
                              + " B.UnitBulk AS RequestedBulkUnit, 0.0000 AS IssuedQty, 0.0000 AS ReturnedQty, 0 AS ReturnedBulkQty, A.Field1, A.Field2, A.Field3, "
                              + " A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Production.DISBillOfMaterial A "
                              + " LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode INNER JOIN Production.DISStep C ON A.DocNumber = C.DocNumber AND A.Step = C.Step "
                              + " WHERE A.DocNumber = '" + a.ToString() + "' AND A.Step = '" + b.ToString() + "'";
                        break;
                    case "Service Order":
                        txtWorkCenter.Text = c.ToString();
                        //ADDED NEW
                        //query = "SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc,A.ColorCode, A.ClassCode, "
                        //      + " A.SizeCode, Unit, ISNULL(A.Consumption,0) - ISNULL(A.IssuedQty,0) AS RequestedQty, 0 AS RequestedBulkQty, B.UnitBulk AS RequestedBulkUnit, "
                        //      + " 0.0000 AS IssuedQty, 0.0000 AS ReturnedQty, 0 AS ReturnedBulkQty, A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, "
                        //      + " '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Procurement.SOMaterialMovement A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.DocNumber = '" + a.ToString() + "'";
                        query = "SELECT '" + txtDocNumber.Text + "' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode,B.FullDesc,A.ColorCode, A.ClassCode, "
                              + " A.SizeCode, Unit, ISNULL(A.Consumption,0) - ISNULL(A.IssuedQty,0) AS RequestedQty, 0 AS RequestedBulkQty, B.UnitBulk AS RequestedBulkUnit, "
                              + " 0 AS IssuedQty, 0.0000 AS ReturnedQty, 0.0000 AS ReturnedBulkQty, A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, "
                              + " '' AS Version, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM Procurement.SOMaterialMovement A LEFT JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.DocNumber = '" + a.ToString() + "'";
                        break;
                }
            }
            return query;
        }

        protected void glRefJONumber_Init(object sender, EventArgs e)
        {
            if (Session["ReqRefType"] != null && Session["ReqRefType"] != "")
            {
                ChangeSource(Session["ReqRefType"].ToString());
            }
        }
    }
}