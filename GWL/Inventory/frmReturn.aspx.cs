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
    public partial class frmReturn : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string filter = "";
        string strresult = "";

        private static string Connection;

        Entity.Return _Entity = new Return();//Calls entity odsHeader
        Entity.Return.ReturnDetail _EntityDetail = new Return.ReturnDetail();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            switch (Request.QueryString["transtype"].ToString())
            {
                case "INVOSR":
                    filter = "ITMCAT_OS";
                    break;

                case "INVSAR":
                case "INVCOR":
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
                    Session["FilterItems"] = dt["Value"].ToString();
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
            gv1.DataSource = null;
            ModuleUsed();

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["FilterItems"] = null;
                Session["referencedetail"] = null; 
                Session["Datatable"] = null;
                Session["IssFilterExpression"] = null;
                Session["SKUFilter"] = null;
                Session["ReturnItemCode"] = null;

                //_Entity.ReturnedBy = Session["Userid"].ToString();

                popup.ShowOnPageLoad = true;
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Date = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                //glType.Value = _Entity.SamplesType;
                glIssuanceNumber.Value = _Entity.IssuanceNumber;
                txtReturnedBy.Text = Session["userid"].ToString();
                txtCostCenter.Value = _Entity.CostCenter;
                glWarehouseCode.Value = _Entity.Warehouse;
                txtTotalQty.Value = _Entity.TotalQty;
                txtTotalBulkQty.Value = _Entity.TotalBulkQty;
                memRemarks.Value = _Entity.Remarks;
                cbxIsWithReference.Value = _Entity.IsWithReference;
                cbxIsPrinted.Value = _Entity.IsPrinted;

                //txtJONumber.Value = _Entity.JONumber;
                //txtJOStep.Value = _Entity.JOStep;
                //txtWorkCenter.Text = _Entity.WorkCenter;
                //memReason.Value = _Entity.Reason;
                //glCurrency.Text = String.IsNullOrEmpty(_Entity.Currency) ? "PHP" : _Entity.Currency;
                //txtExchangeRate.Text = _Entity.ExchangeRate.ToString() == "0" ? "1.00" : _Entity.ExchangeRate.ToString();
                //txtPesoAmount.Text = _Entity.PesoAmount.ToString();
                //txtForeignAmount.Text = _Entity.ForeignAmount.ToString();
                //cbxNoAlloc.Value = _Entity.NoAlloc;
                //txtTotalCost.Value = _Entity.TotalCost;

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
                Samplestayp();

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
                        //updateBtn.Text = "Add"; 
                        if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            cbxIsWithReference.Value = true;
                            glIssuanceNumber.ClientEnabled = true;
                            txtReturnedBy.Text = Session["userid"].ToString();
                            txtReturnedBy.ReadOnly = true;
                           
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        
                        if (cbxIsWithReference.Checked == false) glIssuanceNumber.ClientEnabled = false;
                        else glIssuanceNumber.ClientEnabled = true;
                        
                        txtReturnedBy.ReadOnly = true;
                        
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        cbxIsWithReference.ReadOnly = true;
                        cbxNoAlloc.ReadOnly = true;

                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        txtExchangeRate.ReadOnly = true;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "D":
                        view = true;
                        //cbxIsWithReference.Value = true;
                        
                        cbxIsWithReference.ReadOnly = true;
                        cbxNoAlloc.ReadOnly = true;
                        updateBtn.Text = "Delete";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.ReturnDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                gvJournal.DataSourceID = "odsJournalEntry";
            }         
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            strresult += GearsInventory.GInventory.Return_Validate(gparam);
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
            gparam._Table = "Inventory.ReturnHeader";
            gparam._Factor = -1;
            strresult += GearsInventory.GInventory.Return_Post(gparam);
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                //ASPxGridLookup look = sender as ASPxGridLookup;
                //look.Enabled = false;
            }
            else
            {
                //ASPxGridLookup look = sender as ASPxGridLookup;
                //look.Enabled = true;
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

        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientVisible = !view;
        }
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {}

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

            if (cbxIsWithReference.Checked)
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

            if (Request.QueryString["entry"] == "D")
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

            if (Request.QueryString["entry"] != "V" || Request.QueryString["entry"] != "D")
            {
                if (cbxIsWithReference.Checked)
                {
                    if (e.ButtonID == "Delete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
                else
                {
                    if (e.ButtonID == "Delete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
            }
            else
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
                if (Session["IssFilterExpression"] != null)
                {
                    if (Session["SKUFilter"] != null)
                    {
                        if (Session["SKUFilter"].ToString().Equals("ColorCode"))
                        {
                            gridLookup.GridView.DataSourceID = "sdsItemDetail";
                            sdsItemDetail.FilterExpression = Session["IssFilterExpression"].ToString();
                            sdsItemDetail.SelectCommand = "Select DISTINCT A.ItemCode ,ColorCode, '' AS ClassCode, '' AS SizeCode FROM masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + Session["ReturnItemCode"] + "'";//ADD CONN
                            sdsItemDetail.DataBind();
                            gv1.DataSourceID = "sdsItemDetail";
                            gridLookup.DataBind();
                        }
                        else if (Session["SKUFilter"].ToString().Equals("ClassCode"))
                        {
                            gridLookup.GridView.DataSourceID = "sdsItemDetail";
                            sdsItemDetail.FilterExpression = Session["IssFilterExpression"].ToString();
                            sdsItemDetail.SelectCommand = "Select DISTINCT A.ItemCode ,ClassCode, '' AS ColorCode, '' AS SizeCode FROM masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + Session["ReturnItemCode"] + "'";//ADD CONN
                            sdsItemDetail.DataBind();
                            gv1.DataSourceID = "sdsItemDetail";
                            gridLookup.DataBind();
                        }
                        else if (Session["SKUFilter"].ToString().Equals("SizeCode"))
                        {
                            gridLookup.GridView.DataSourceID = "sdsItemDetail";
                            sdsItemDetail.FilterExpression = Session["IssFilterExpression"].ToString();
                            sdsItemDetail.SelectCommand = "Select DISTINCT A.ItemCode ,SizeCode, '' AS ColorCode, '' AS ClassCode FROM masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + Session["ReturnItemCode"] + "'";//ADD CONN
                            sdsItemDetail.DataBind();
                            gv1.DataSourceID = "sdsItemDetail";
                            gridLookup.DataBind();
                        }
                        //Session["SKUFilter"] = null;
                    }
                    gridLookup.GridView.DataSourceID = "sdsItemDetail";
                    sdsItemDetail.FilterExpression = Session["IssFilterExpression"].ToString();
                    
                }

                

            }
            
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";
            Session["ReturnItemCode"] = itemcode;
            if (e.Parameters.Contains("ItemCode"))
            {
                //COLOR CODE LOOKUP
                DataTable getColor = Gears.RetriveData2("Select DISTINCT LTRIM(RTRIM(ColorCode)) AS ColorCode FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getColor.Rows.Count > 1)
                {
                    codes = "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getColor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                    }
                }

                //CLASS CODE LOOKUP
                DataTable getClass = Gears.RetriveData2("Select DISTINCT LTRIM(RTRIM(ClassCode)) AS ClassCode FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getClass.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getClass.Rows)
                    {
                        codes += dt["ClassCode"].ToString() + ";";
                    }
                }

                //SIZE CODE LOOKUP
                DataTable getSize = Gears.RetriveData2("Select DISTINCT LTRIM(RTRIM(C.SizeCode)) AS SizeCode, SortOrder FROM masterfile.item a "
                                    + " left join masterfile.itemdetail b on a.itemcode = b.itemcode "
                                    + " INNER JOIN Masterfile.Size C ON b.sizeCode = C.SizeCode "
                                    + " where a.itemcode = '" + itemcode + "'  ORDER BY SortOrder ASC", Session["ConnString"].ToString());//ADD CONN
                if (getSize.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getSize.Rows)
                    {
                        codes += dt["SizeCode"].ToString() + ";";
                    }
                }

                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,UnitBase,FullDesc, UnitBulk, ISNULL(A.IsByBulk,0) AS IsByBulk FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in countcolor.Rows)
                {
                    //codes = dt["ColorCode"].ToString() + ";";
                    //codes += dt["ClassCode"].ToString() + ";";
                    //codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["IsByBulk"].ToString() + ";";
                }

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                
                if (e.Parameters.Contains("ColorCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, LTRIM(RTRIM([ColorCode])) AS ColorCode, '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail";
                    Session["SKUFilter"] = "ColorCode";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], LTRIM(RTRIM([ClassCode])) AS ClassCode, '' AS [SizeCode] FROM Masterfile.ItemDetail";
                    Session["SKUFilter"] = "ClassCode";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], LTRIM(RTRIM(A.[SizeCode])) AS SizeCode, SortOrder FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode ORDER BY SortOrder ASC";
                    Session["SKUFilter"] = "SizeCode";
                }

                //COLOR CODE LOOKUP
                DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getColor.Rows.Count > 1)
                {
                    codes = "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getColor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                    }
                }

                //CLASS CODE LOOKUP
                DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getClass.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getClass.Rows)
                    {
                        codes += dt["ClassCode"].ToString() + ";";
                    }
                }

                //SIZE CODE LOOKUP
                DataTable getSize = Gears.RetriveData2("Select DISTINCT C.SizeCode, SortOrder FROM masterfile.item a "
                                    + " left join masterfile.itemdetail b on a.itemcode = b.itemcode "
                                    + " INNER JOIN Masterfile.Size C ON b.sizeCode = C.SizeCode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getSize.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getSize.Rows)
                    {
                        codes += dt["SizeCode"].ToString() + ";";
                    }
                }

                //UNIT BASE LOOKUP
                DataTable getUnit = Gears.RetriveData2("Select DISTINCT UnitBase FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getUnit.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnit.Rows)
                    {
                        codes += dt["UnitBase"].ToString() + ";";
                    }
                }

                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["IssFilterExpression"] = sdsItemDetail.FilterExpression;
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
            _Entity.ReturnType = String.IsNullOrEmpty(txtReturnType.Text) ? null : txtReturnType.Text;
            _Entity.SamplesType = String.IsNullOrEmpty(glType.Text) ? null : glType.Text;
            _Entity.IssuanceNumber = String.IsNullOrEmpty(glIssuanceNumber.Text) ? null : glIssuanceNumber.Text;
            _Entity.ReturnedBy = String.IsNullOrEmpty(txtReturnedBy.Text) ? null : txtReturnedBy.Text;
            _Entity.CostCenter = String.IsNullOrEmpty(txtCostCenter.Text) ? null : txtCostCenter.Text;
            _Entity.Warehouse = String.IsNullOrEmpty(glWarehouseCode.Text) ? null : glWarehouseCode.Text;
            _Entity.TotalQty = String.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToDecimal(txtTotalQty.Value.ToString());
            _Entity.TotalBulkQty = String.IsNullOrEmpty(txtTotalBulkQty.Text) ? 0 : Convert.ToDecimal(txtTotalBulkQty.Value.ToString());
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.IsWithReference = Convert.ToBoolean(cbxIsWithReference.Value.ToString());
            _Entity.IsPrinted = Convert.ToBoolean(cbxIsPrinted.Value.ToString());
            _Entity.JONumber = String.IsNullOrEmpty(txtJONumber.Text) ? null : txtJONumber.Text;
            _Entity.JOStep = String.IsNullOrEmpty(txtJOStep.Text) ? null : txtJOStep.Text;
            _Entity.WorkCenter = String.IsNullOrEmpty(txtWorkCenter.Text) ? null : txtWorkCenter.Text;
            _Entity.Reason = String.IsNullOrEmpty(memReason.Text) ? null : memReason.Text;
            _Entity.Currency = String.IsNullOrEmpty(glCurrency.Text) ? null : glCurrency.Text;
            //_Entity.NoAlloc = Convert.ToBoolean(cbxNoAlloc.Value.ToString());
            _Entity.TotalCost = String.IsNullOrEmpty(txtTotalCost.Text) ? 0 : Convert.ToDecimal(txtTotalCost.Value.ToString());
            
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
            //_Entity.LastEditedDate = DateTime.Now.ToString();

            _Entity.Transaction = Request.QueryString["transtype"].ToString();

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM Inventory.ReturnHeader WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());

            DataTable poolSource = new DataTable();
            if (Session["Datatable"] == "1")
            {
                foreach (GridViewColumn col in gv1.VisibleColumns)
                {
                    GridViewDataColumn dataColumn = col as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    poolSource.Columns.Add(dataColumn.FieldName);
                }
                for (int i = 0; i < gv1.VisibleRowCount; i++)
                {
                    DataRow row = poolSource.Rows.Add();
                    foreach (DataColumn col in poolSource.Columns)
                        row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
                }
            }

            switch (e.Parameter)
            {            
                case "Add":
                case "Update": 

                    gv1.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber,"Inventory.ReturnHeader",1,Connection);//NEWADD factor 1 if submit, 2 if approve

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
                        if (Session["Datatable"] == "1")
                        {
                            Gears.RetriveData2("DELETE FROM Inventory.ReturnDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            //gv1.KeyFieldName = "LineNumber";
                            //gv1.DataSourceID = sdsRefIssuanceDetail.ID;
                            gv1.DataSource = poolSource;
                            if (gv1.DataSourceID != null)
                            {
                                gv1.DataSourceID = null;
                            }
                            gv1.DataBind();
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        _Entity.UpdateUnitFactor(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        Post();
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
                case "IssuedNumber":
                    GetSelectedVal();
                    gv1.KeyFieldName = "LineNumber";
                    SqlDataSource ds = sdsIssuanceNumber;
                    string ISNum = glIssuanceNumber.Text;
                    ds.SelectCommand = string.Format("select DocNumber, WorkCenter, WarehouseCode, ISNULL(ReqJONumber,'N/A') AS ReqJONumber, ISNULL(ReqJOStep,'N/A') AS ReqJONumber, CostCenter, Currency, ExchangeRate, SamplesType from Inventory.Issuance WHERE ISNULL(SubmittedBy,'') != '' AND DocNumber = '" + glIssuanceNumber.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        //glReturnedBy.Text = inb[0][1].ToString();
                        txtWorkCenter.Text = inb[0][1].ToString();
                        glWarehouseCode.Text = inb[0][2].ToString();
                        txtJONumber.Text = inb[0][3].ToString();
                        txtJOStep.Text = inb[0][4].ToString();
                        txtCostCenter.Text = inb[0][5].ToString();
                        glCurrency.Text = inb[0][6].ToString();
                        txtExchangeRate.Text = inb[0][7].ToString();
                        glType.Text = inb[0][8].ToString();
                    }

                    if (Request.QueryString["transtype"].ToString() == "INVJON")
                    {
                        sdsIssuanceNumber.SelectCommand = "SELECT DISTINCT A.DocNumber, TransType, Type FROM Inventory.Issuance a inner join Inventory.IssuanceDetail b ON A.DocNumber = b.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND Type = 'INVJOI' AND (ISNULL(IssuedQty,0) > ISNULL(ReturnedQty,0) OR ISNULL(IssuedBulkQty,0) > ISNULL(ReturnedBulkQty,0))";
                        glIssuanceNumber.DataSourceID = sdsIssuanceNumber.ID;
                        glIssuanceNumber.DataBind();
                        
                    }
                    else
                    {
                        sdsIssuanceNumber.SelectCommand = "SELECT DISTINCT A.DocNumber, TransType, Type FROM Inventory.Issuance a inner join Inventory.IssuanceDetail b ON A.DocNumber = b.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND Type = 'INVSAI' AND (ISNULL(IssuedQty,0) > ISNULL(ReturnedQty,0) OR ISNULL(IssuedBulkQty,0) > ISNULL(ReturnedBulkQty,0))";
                        glIssuanceNumber.DataSourceID = sdsIssuanceNumber.ID;
                        glIssuanceNumber.DataBind();

                        sdsIssuanceNumber.SelectCommand = "SELECT DISTINCT A.DocNumber, TransType, Type FROM Inventory.Issuance a inner join Inventory.IssuanceDetail b ON A.DocNumber = b.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND (ISNULL(IssuedQty,0) > ISNULL(ReturnedQty,0) OR ISNULL(IssuedBulkQty,0) != ISNULL(ReturnedBulkQty,0))";
                        glIssuanceNumber.Text = ISNum;
                    }
                        cp.JSProperties["cp_generated"] = true;
                    break;
                case "SamplesType":
                    Samplestayp();
                    GetSelectedVal();
                    break;
                case "WithDetail":
                    if (cbxIsWithReference.Checked == true)
                    {
                        glIssuanceNumber.ClientEnabled = true;
                        txtReturnedBy.ReadOnly = true;
                        txtReturnedBy.Text = Session["userid"].ToString();
                        Session["Datatable"] = "1";
                    }
                    else
                    {
                        glIssuanceNumber.ClientEnabled = false;
                        txtReturnedBy.ReadOnly = true;
                        txtReturnedBy.Text = Session["userid"].ToString();
                        Session["Datatable"] = "0";
                    }
                    //glIssuanceNumber.Text = "";
                    
                    txtCostCenter.Text = "";
                    cp.JSProperties["cp_withref"] = true;
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    gv1.DataSourceID = "sdsDetailNew";
                    break;
                case "ReturnedBy":
                    SqlDataSource rb = sdsReturnedBy;
                    rb.SelectCommand = string.Format("select BizPartnerCode, Name, CostCenterCode from masterfile.BPCustomerInfo where ISNULL(IsInactive,0) = 0 AND BizPartnerCode = '" + txtReturnedBy.Text + "'");
                    DataView vl = (DataView)rb.Select(DataSourceSelectArguments.Empty);
                    if (vl.Count > 0)
                    {
                        txtCostCenter.Text = vl[0][2].ToString();
                    }
                    break;
                
                case "skufilter":

                    Session["SKUFilter"] = null;

                    break;
            }
        }        
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {}

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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                int countline = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = countline++;    
                    var ItemCode = values.NewValues["ItemCode"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var Unit = values.NewValues["Unit"];
                    var ItemPrice = values.NewValues["ItemPrice"];  
                    var IssuedQty = values.NewValues["IssuedQty"];
                    var IssuedBulkQty = values.NewValues["IssuedBulkQty"];
                    var ReturnedQty = values.NewValues["ReturnedQty"]; 
                    var ReturnedBulkQty = values.NewValues["ReturnedBulkQty"];
                    var Cost = values.NewValues["Cost"];
                    var StatusCode = values.NewValues["StatusCode"];
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"]; 
                    var Version = values.NewValues["Version"];
                    var ExpDate = values.NewValues["ExpDate"];
                    var MfgDate = values.NewValues["MfgDate"];
                    var BatchNo = values.NewValues["BatchNo"];
                    var LotNo = values.NewValues["LotNo"];
                    source.Rows.Add(txtDocNumber.Text, LineNumber, ItemCode, ColorCode, ClassCode, SizeCode, 
                                    ItemPrice, Unit, ReturnedQty, ReturnedBulkQty, IssuedQty, IssuedBulkQty,
                                    Cost, StatusCode, ExpDate, MfgDate, BatchNo, LotNo, Field1, Field2, Field3, Field4,
                                    Field5, Field6, Field7, Field8, Field9, Version);
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["ItemPrice"] = values.NewValues["ItemPrice"];
                    row["IssuedQty"] = values.NewValues["IssuedQty"];
                    row["IssuedBulkQty"] = values.NewValues["IssuedBulkQty"];
                    row["ReturnedQty"] = values.NewValues["ReturnedQty"];
                    row["ReturnedBulkQty"] = values.NewValues["ReturnedBulkQty"];
                    row["Cost"] = values.NewValues["Cost"];
                    //row["UnitCost"] = values.NewValues["UnitCost"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
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
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.ItemPrice = Convert.ToDecimal(dtRow["ItemPrice"].ToString());
                    _EntityDetail.IssuedQty = Convert.ToDecimal(dtRow["IssuedQty"].ToString());
                    //_EntityDetail.IssuedBulkQty = Convert.ToDecimal(dtRow["IssuedBulkQty"].ToString());
                    _EntityDetail.ReturnedQty = Convert.ToDecimal(dtRow["ReturnedQty"].ToString());
                    _EntityDetail.ReturnedBulkQty = Convert.ToDecimal(dtRow["ReturnedBulkQty"].ToString());
                    _EntityDetail.Cost = Convert.ToDecimal(dtRow["Cost"].ToString());
                    //_EntityDetail.UnitCost = Convert.ToDecimal(dtRow["UnitCost"].ToString());
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
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
                    _EntityDetail.AddReturnDetail(_EntityDetail);
                }
            }            
        }
        #endregion
        
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["referencedetail"] = null;
            }

            if (Session["referencedetail"] != null && Session["referencedetail"] != "")
            {
                sdsRefIssuanceDetail.FilterExpression = Session["referencedetail"].ToString();
                gv1.DataSource = sdsRefIssuanceDetail;
                if (gv1.DataSourceID != "")
                {
                    gv1.DataSourceID = null;
                }
                //gv1.DataBind();
            }
        }

        private DataTable GetSelectedVal()
        {
            Session["Datatable"] = "0";
            gv1.DataSourceID = null;
            gv1.DataBind();

            DataTable dt = new DataTable();
            Session["referencedetail"] = sdsRefIssuanceDetail.FilterExpression;
            sdsRefIssuanceDetail.SelectCommand = "SELECT '"+ txtDocNumber.Text +"' AS DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, "
                                                    + "ItemCode, ColorCode, ClassCode, SizeCode, IssuedQty, IssuedBulkQty, ISNULL(ItemPrice,0.00) AS ItemPrice, Unit, ISNULL(IssuedQty,0) - ISNULL(ReturnedQty,0) AS ReturnedQty, ISNULL(IssuedBulkQty,0) - ISNULL(ReturnedBulkQty,0) AS ReturnedBulkQty, ISNULL(Cost,0.0000) AS Cost, "
                                                    + "StatusCode,A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, Version, B.ExpDate, B.MfgDate, B.BatchNo, B.LotNo "
                                                    + "FROM Inventory.Issuance A INNER JOIN Inventory.IssuanceDetail B ON A.DocNumber = B.DocNumber "
                                                    + "WHERE A.DocNumber = '" + glIssuanceNumber.Text + "'";
                                                    //+"WHERE A.DocNumber like '%" + glIssuanceNumber.Text + "%'";

            //gv1.DataSourceID = "sdsRefIssuanceDetail";
            sdsRefIssuanceDetail.DataBind();
            gv1.DataSource = sdsRefIssuanceDetail;
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
            dt.Columns["LineNumber"]};

            return dt;
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
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode"))
            {
                ASPxGridLookup gridLookup = sender as ASPxGridLookup;
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                FilterItem();
                gridLookup.DataBind();
            }
        }
        protected void ColorCode_Init(object sender, EventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glColorCode"))
            {
                ASPxGridLookup gridLookup = sender as ASPxGridLookup;
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                FilterItem();
                gridLookup.DataBind();
            }
        }
        public void FilterItem()
        {
            if (Session["FilterItems"] == null)
            {
                Session["FilterItems"] = "";
            }
            if (!String.IsNullOrEmpty(Session["FilterItems"].ToString()))
            {
                if (Session["FilterItems"].ToString() != "ITMCAT_ASI")
                {
                    sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] " +
                    "WHERE ISNULL(IsInactive,0) = 0 AND ISNULL(ItemCategoryCode,0) IN ('" + Session["FilterItems"].ToString() + "') ORDER BY ItemCode ASC";
                }
                else if (Session["FilterItems"].ToString() == "ITMCAT_ASI")
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

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterItem();
            
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

        public void SKU_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";


            if (e.Parameters.Contains("ColorCode"))
            {
                sdsItemDetail.SelectCommand = "Select DISTINCT A.ItemCode ,ColorCode FROM masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'";//ADD CONN
            }
            else if (e.Parameters.Contains("ClassCode"))
            {
                sdsItemDetail.SelectCommand = "Select DISTINCT A.ItemCode ,ClassCode FROM masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'";//ADD CONN

            }
            else if (e.Parameters.Contains("SizeCode"))
            {
                sdsItemDetail.SelectCommand = "Select DISTINCT A.ItemCode ,SizeCode FROM masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'";//ADD CONN
            }

        }

        public void Samplestayp()
        {
            if (glType.Text == "LIQUIDATION OF RETURNABLE ITEM")
            {
                gv1.Columns["ItemPrice"].Width = 100;
                gv1.Columns["Cost"].Width = 0;
            }
            else
            {
                gv1.Columns["Cost"].Width = 100;
                gv1.Columns["ItemPrice"].Width = 0;
            }
        }

        protected void OnlyForYou()
        {
            sdsIssuanceNumber.SelectCommand = "SELECT DISTINCT A.DocNumber, TransType, Type FROM Inventory.Issuance a inner join Inventory.IssuanceDetail b ON A.DocNumber = b.DocNumber WHERE ISNULL(SubmittedBy,'') != ''";
            glIssuanceNumber.DataSourceID = "sdsIssuanceNumber";
            glIssuanceNumber.DataBind();
        }

        protected void ModuleUsed()
        {
            DataTable mod = new DataTable();
            mod = Gears.RetriveData2("SELECT DISTINCT Description FROM IT.GenericLookup WHERE LookUpKey ='RETTYPE' AND Code ='" + Request.QueryString["transtype"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

            foreach (DataRow md in mod.Rows)
            {
                txtReturnType.Text = md["Description"].ToString().Trim();
                HeaderText.Text = md["Description"].ToString().Trim() + " Return";
            }

            switch (Request.QueryString["transtype"].ToString())
            {
                case "INVJON":
                    frmlayout1.FindItemOrGroupByName("JOMaterialGroup").ClientVisible = true;
                    frmlayout1.FindItemOrGroupByName("SamplesGroup").ClientVisible = false;
                    sdsIssuanceNumber.SelectCommand = "SELECT DISTINCT A.DocNumber, TransType, Type FROM Inventory.Issuance a inner join Inventory.IssuanceDetail b ON A.DocNumber = b.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND Type = 'INVJOI' AND (ISNULL(IssuedQty,0) > ISNULL(ReturnedQty,0) OR ISNULL(IssuedBulkQty,0) > ISNULL(ReturnedBulkQty,0))";
                    gv1.Columns["Cost"].Width = 100;
                    glIssuanceNumber.DataBind();

                    var formLayout3 = frmlayout1 as ASPxFormLayout;
                    var ReturnedBy = formLayout3.FindItemOrGroupByName("ReturnedBy");
                    ReturnedBy.ClientVisible = false;
                    ReturnedBy.VisibleIndex = 11;
                    if (cbxIsWithReference.Checked == true)
                    {
                        glIssuanceNumber.ClientEnabled = true;
                        txtReturnedBy.ReadOnly = true;
                        //Session["Datatable"] = "1";
                    }
                    else
                    {
                        glIssuanceNumber.ClientEnabled = false;
                        txtReturnedBy.ReadOnly = true;
                        //Session["Datatable"] = "0";
                    }
                    break;
                case "INVSAN":
                    frmlayout1.FindItemOrGroupByName("JOMaterialGroup").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("SamplesGroup").ClientVisible = true;
                    sdsIssuanceNumber.SelectCommand = "SELECT DISTINCT A.DocNumber, TransType, Type FROM Inventory.Issuance a inner join Inventory.IssuanceDetail b ON A.DocNumber = b.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND Type = 'INVSAI' AND (ISNULL(IssuedQty,0) > ISNULL(ReturnedQty,0) OR ISNULL(IssuedBulkQty,0) > ISNULL(ReturnedBulkQty,0))";
                    //gv1.Columns["Cost"].Width = 0;
                    glIssuanceNumber.DataBind();
                    glType.ClientVisible = true;
                    
                    if (cbxIsWithReference.Checked == true)
                    {
                        glIssuanceNumber.ClientEnabled = true;
                        txtReturnedBy.ReadOnly = true;
                        //Session["Datatable"] = "1";
                    }
                    else
                    {
                        glIssuanceNumber.ClientEnabled = false;
                        txtReturnedBy.ReadOnly = true;
                        //Session["Datatable"] = "0";
                    }
                    break;
            }

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                sdsIssuanceNumber.SelectCommand = "SELECT DISTINCT A.DocNumber, TransType, Type FROM Inventory.Issuance a inner join Inventory.IssuanceDetail b ON A.DocNumber = b.DocNumber WHERE ISNULL(SubmittedBy,'') != ''";
                glIssuanceNumber.DataSourceID = "sdsIssuanceNumber";
                glIssuanceNumber.DataBind();
            }
        }
    }
}