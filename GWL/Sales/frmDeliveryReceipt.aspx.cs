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
using GearsSales;

namespace GWL
{
    public partial class frmDeliveryReceipt : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string save = "";

        Entity.DeliveryReceipt _Entity = new DeliveryReceipt();//Calls entity odsHeader
        Entity.DeliveryReceipt.DeliveryReceiptDetail _EntityDetail = new DeliveryReceipt.DeliveryReceiptDetail();//Call entity sdsDetail

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

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            gvRef.Columns["CommandString"].Width = 0;
            gvRef.Columns["RCommandString"].Width = 0;

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["FirstClick"] = null;
                Session["RefCheck"] = null;
                Session["FilterExpression"] = null;
                Session["DatatableSaving"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    gv1.KeyFieldName = "LineNumber";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("JournalEntry").ClientVisible = false;
                }
                //else
                //{
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglRefSO.Value = _Entity.RefSONum;
                //aglRefSO.Text = _Entity.RefSONum.ToString();
                aglType.Value = _Entity.Type.ToString();
                DataTable type = new DataTable();
                type = Gears.RetriveData2("SELECT DISTINCT Description FROM IT.GenericLookup WHERE LookUpKey ='DRTYPE' AND Code ='" + _Entity.Type.ToString() + "'", Session["ConnString"].ToString());
                if (type.Rows.Count != 0)
                {
                    aglType.Text = type.Rows[0]["Description"].ToString();
                }
                else
                {
                    aglType.Text = "SALES ORDER";
                }
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                txtPLNumber.Text = _Entity.PLDocNum.ToString();
                //txtCustomerPONo.Value = _Entity.CustomerPO.ToString();
                aglWarehouseCode.Value = _Entity.WarehouseCode.ToString();
                txtInvoiceNo.Value = _Entity.InvoiceDocNum.ToString();
                txtCounterNo.Value = _Entity.CounterDocNum.ToString();
                txtOutlet.Value = _Entity.Outlet.ToString();
                txtTIN.Value = _Entity.TIN.ToString();
                txtAddress.Value = _Entity.DeliveryAddress.ToString();
                speTerms.Value = _Entity.Terms.ToString();
                memTotalQty.Value = _Entity.TotalQty.ToString();
                speTotalBulkQty.Value = _Entity.TotalBulkQty;
                memRemarks.Value = _Entity.Remarks.ToString();
                txtForwardedDate.Value = _Entity.ForwardedDate.ToString();
                txtDispatchDate.Value = _Entity.DispatchDate.ToString();
                chkRefSO.Value = _Entity.WithoutRef;

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
                txtHPostedBy.Text = _Entity.PostedBy;
                txtHPostedDate.Text = _Entity.PostedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;


             
                //}
                
                gv1.KeyFieldName = "LineNumber";

                //V=View, E=Edit, N=New
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
                        RefCheck();
                        FilterSO();
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        RefCheck();
                        FilterSO();
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        RefCheck();
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        RefCheck();
                        updateBtn.Text = "Delete";
                        break;
                }


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                    gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                else
                {
                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.DeliveryReceiptDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail"); 
                gvJournal.DataSourceID = "odsJournalEntry";


                
            }

          

        }
        #endregion

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "SLSDRC";
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsSales.GSales.DeliveryReceipt_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "SLSDRC";
            gparam._Table = "Sales.DeliveryReceipt";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsSales.GSales.DeliveryReceipt_Post(gparam);
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
        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo memo = sender as ASPxMemo;
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
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = true;

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
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void ButtonLoad(object sender, EventArgs e)
        {
            var button = sender as ASPxButton;

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                button.Enabled = false;
            }
            else
            {
                button.Enabled = true;
            }
        }
        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
            //else
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = false;
            //}

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
                if (chkRefSO.Value.ToString() == "True")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }
                }
                if (chkRefSO.Value.ToString() == "False")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
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
            if (e.Parameters != null && e.Parameters != "")
            {
                string column = e.Parameters.Split('|')[0];//Set column name
                if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
                string itemcode = e.Parameters.Split('|')[1];//Set Item Code
                string val = e.Parameters.Split('|')[2];//Set column value
                if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
                var itemlookup = sender as ASPxGridView;
                string codes = "";
                //if (e.Parameters.Contains("ItemCode"))
                //{
                //    DataTable countcolor = Gears.RetriveData2("Select DISTINCT UPPER(ColorCode) AS ColorCode, UPPER(ClassCode) AS ClassCode,UPPER(SizeCode) AS SizeCode,UPPER(UnitBase) AS UnitBase, " +
                //                                            " FullDesc, UPPER(UnitBulk) AS UnitBulk, A.IsByBulk, ISNULL(StatusCode,'') AS StatusCode " +
                //                                            " from masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());

                //    foreach (DataRow dt in countcolor.Rows)
                //    {
                //        codes = dt["ColorCode"].ToString() + ";";
                //        codes += dt["ClassCode"].ToString() + ";";
                //        codes += dt["SizeCode"].ToString() + ";";
                //        codes += dt["UnitBase"].ToString() + ";";
                //        codes += dt["UnitBulk"].ToString() + ";";
                //        codes += dt["FullDesc"].ToString() + ";";
                //        codes += dt["IsByBulk"].ToString() + ";";
                //        codes += dt["StatusCode"].ToString() + ";";
                //        codes += "2;";
                //    }

                //    itemlookup.JSProperties["cp_codes"] = codes;
                //}
                //else
                //{
                if (e.Parameters.Contains("ColorCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "ColorCode";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "ClassCode";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["STRitemID"] = "SizeCode";
                }

                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
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
        //public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string column = e.Parameters.Split('|')[0];//Set column name
        //    if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
        //    string itemcode = e.Parameters.Split('|')[1];//Set Item Code
        //    string val = e.Parameters.Split('|')[2];//Set column value
        //    if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
        //    var itemlookup = sender as ASPxGridView;
        //    string codes = "";
        //    if (e.Parameters.Contains("ItemCode"))  
        //    {
        //        DataTable countcolor = Gears.RetriveData2("Select DISTINCT UPPER(RTRIM(ColorCode)) AS ColorCode, UPPER(RTRIM(ClassCode)) AS ClassCode,UPPER(RTRIM(SizeCode)) AS SizeCode,UPPER(UnitBase) AS UnitBase, " +
        //                                                " FullDesc, UPPER(UnitBulk) AS UnitBulk, A.IsByBulk, ISNULL(StatusCode,'') AS StatusCode " +
        //                                                " from masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());
                               
        //        foreach (DataRow dt in countcolor.Rows)
        //        {
        //            codes = "";

        //            int ColorCount = countcolor.AsEnumerable().Select(row => row.Field<string>("ColorCode")).Distinct().Count();
        //            codes += ColorCount > 1 ? ";" : dt["ColorCode"].ToString() + ";";
        //           // .Where(row => row.Field<string>("ItemCode") == itemcode)

        //            int ClassCount = countcolor.AsEnumerable().Select(row => row.Field<string>("ClassCode")).Distinct().Count();
        //            codes += ClassCount > 1 ? ";" : dt["ClassCode"].ToString() + ";";

        //            int SizeCount = countcolor.AsEnumerable().Select(row => row.Field<string>("SizeCode")).Distinct().Count();
        //            codes += SizeCount > 1 ? ";" : dt["SizeCode"].ToString() + ";";

        //            codes += dt["UnitBase"].ToString() + ";";
        //            codes += dt["UnitBulk"].ToString() + ";";
        //            codes += dt["FullDesc"].ToString() + ";";
        //            codes += dt["IsByBulk"].ToString() + ";";
        //            codes += dt["StatusCode"].ToString() + ";";
        //            codes += "2;";
        //        }              

        //        itemlookup.JSProperties["cp_codes"] = codes;
        //    }
        //    else
        //    {
                
        //        if (e.Parameters.Contains("ColorCode"))
        //        {
        //            sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail";
        //        }
        //        else if (e.Parameters.Contains("ClassCode"))
        //        {
        //            sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail";
        //        }
        //        else if (e.Parameters.Contains("SizeCode"))
        //        {
        //            sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail";
        //        }

        //        ASPxGridView grid = sender as ASPxGridView;
        //        ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
        //        var selectedValues = itemcode;
        //        CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
        //        sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //        Session["FilterExpression"] = sdsItemDetail.FilterExpression;
        //        grid.DataSourceID = "sdsItemDetail";
        //        grid.DataBind();

        //        for (int i = 0; i < grid.VisibleRowCount; i++)
        //        {
        //            if (grid.GetRowValues(i, column) != null)
        //                if (grid.GetRowValues(i, column).ToString() == val)
        //                {
        //                    grid.Selection.SelectRow(i);
        //                    string key = grid.GetRowValues(i, column).ToString();
        //                    grid.MakeRowVisible(key);
        //                    break;
        //                }
        //        }
        //    }
        //}

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.RefSONum = String.IsNullOrEmpty(aglRefSO.Text) ? null : aglRefSO.Text;
            _Entity.Type = String.IsNullOrEmpty(aglType.Text) ? null : aglType.Value.ToString();
            _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Value.ToString();
            _Entity.PLDocNum = String.IsNullOrEmpty(txtPLNumber.Text) ? null : txtPLNumber.Text;
            //_Entity.CustomerPO = String.IsNullOrEmpty(txtCustomerPONo.Text) ? null : txtCustomerPONo.Text;
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouseCode.Text) ? null : aglWarehouseCode.Value.ToString();
            _Entity.InvoiceDocNum = String.IsNullOrEmpty(txtInvoiceNo.Text) ? null : txtInvoiceNo.Text;
            _Entity.CounterDocNum = String.IsNullOrEmpty(txtCounterNo.Text) ? null : txtCounterNo.Text;
            _Entity.Outlet = String.IsNullOrEmpty(txtOutlet.Text) ? null : txtOutlet.Text;
            _Entity.TIN = String.IsNullOrEmpty(txtTIN.Text) ? null : txtTIN.Text;
            _Entity.DeliveryAddress = String.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Value.ToString();
            _Entity.Terms = String.IsNullOrEmpty(speTerms.Text) ? 0 : Convert.ToInt32(speTerms.Text);
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Value.ToString();
            _Entity.TotalQty = String.IsNullOrEmpty(memTotalQty.Text) ? null : memTotalQty.Value.ToString();
            _Entity.WithoutRef = Convert.ToBoolean(chkRefSO.Value.ToString());
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.TotalBulkQty = String.IsNullOrEmpty(speTotalBulkQty.Text) ? 0 : Convert.ToDecimal(speTotalBulkQty.Value.ToString());
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
             
            DataTable dt = new DataTable();
            if (Session["Datatable"] == "1")
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

            switch (e.Parameter)
            {
                case "Add": 
                    if (Session["Datatable"] != "1")
                    {
                        gv1.UpdateEdit();
                    }

                    Checking();
                    string strError = Functions.Submitted(_Entity.DocNumber, "Sales.DeliveryReceipt", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        if (Session["Datatable"] == "1")
                        {
                            _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv1.DataSource = dt;
                            if (gv1.DataSourceID != null)
                            {
                                gv1.DataSourceID = null;
                            }
                            gv1.DataBind();
                            //gv1.DataSourceID = sdsSODetail.ID;
                            gv1.UpdateEdit();                            
                        }
                        else
                        {
                            if (Session["RefCheck"] == "1")
                            {
                                _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                            }
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Session["Datatable"] = null;
                        _Entity.SubstituteInfo(txtDocNumber.Text, aglCustomerCode.Text, Session["ConnString"].ToString());
                        _Entity.UpdateRefSO(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if (edit == true)
                        {
                            save = "Tagged Sales Orders have different customers!";
                            aglRefSO.Text = "";
                            aglCustomerCode.Text = "";
                            txtAddress.Text = "";
                            txtTIN.Text = "N/A";
                            speTerms.Text = "";
                            memTotalQty.Text = "";
                            Session["FirstClick"] = null;
                        }
                        else
                        {
                            save = "Please check all the fields!";
                        }

                        cp.JSProperties["cp_message"] = save;
                        cp.JSProperties["cp_success"] = true;
                        edit = false;

                    }

                    break;

                case "Update": 
                    if (Session["Datatable"] != "1")
                    {
                        gv1.UpdateEdit();
                    }

                    Checking();
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Sales.DeliveryReceipt", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError1))
                    {
                        cp.JSProperties["cp_message"] = strError1;
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
                            _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv1.DataSourceID = sdsSODetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Session["Datatable"] = null;
                        _Entity.SubstituteInfo(txtDocNumber.Text, aglCustomerCode.Text, Session["ConnString"].ToString());
                        _Entity.UpdateRefSO(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if (edit == true)
                        {
                            save = "Tagged Sales Orders have different customers!";
                            aglRefSO.Text = "";
                            aglCustomerCode.Text = "";
                            txtAddress.Text = "";
                            txtTIN.Text = "N/A";
                            speTerms.Text = "";
                            memTotalQty.Text = "";
                            Session["FirstClick"] = null;
                        }
                        else
                        {
                            save = "Please check all the fields!";
                        }

                        cp.JSProperties["cp_message"] = save;
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
                    }

                    break;

                case "AddZeroDetail":

                    gv1.UpdateEdit();

                    Checking();
                    string strError2 = Functions.Submitted(_Entity.DocNumber, "Sales.DeliveryReceipt", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError2))
                    {
                        cp.JSProperties["cp_message"] = strError2;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.DeliveryReceiptDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        _Entity.SubstituteInfo(txtDocNumber.Text, aglCustomerCode.Text, Session["ConnString"].ToString());
                        _Entity.UpdateRefSO(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if (edit == true)
                        {
                            save = "Tagged Sales Orders have different customers!";
                            aglRefSO.Text = "";
                            aglCustomerCode.Text = "";
                            txtAddress.Text = "";
                            txtTIN.Text = "N/A";
                            speTerms.Text = "";
                            memTotalQty.Text = "";
                            Session["FirstClick"] = null;
                        }
                        else
                        {
                            save = "Please check all the fields!";
                        }

                        cp.JSProperties["cp_message"] = save;
                        cp.JSProperties["cp_success"] = true;
                        edit = false;

                    }

                    break;

                case "UpdateZeroDetail":

                    gv1.UpdateEdit();

                    Checking();

                    string strError3 = Functions.Submitted(_Entity.DocNumber, "Sales.DeliveryReceipt", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError3))
                    {
                        cp.JSProperties["cp_message"] = strError3;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.DeliveryReceiptDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        _Entity.SubstituteInfo(txtDocNumber.Text, aglCustomerCode.Text, Session["ConnString"].ToString());
                        _Entity.UpdateRefSO(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if (edit == true)
                        {
                            save = "Tagged Sales Orders have different customers!";
                            aglRefSO.Text = "";
                            aglCustomerCode.Text = "";
                            txtAddress.Text = "";
                            txtTIN.Text = "N/A";
                            speTerms.Text = "";
                            memTotalQty.Text = "";
                            Session["FirstClick"] = null;
                        }
                        else
                        {
                            save = "Please check all the fields!";
                        }

                        cp.JSProperties["cp_message"] = save;
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
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

                case "CallbackCustomer":
                   // popup2.ContentUrl("../FactBox/fbBizPartner.aspx?BizPartnerCode="+_Entity.CustomerCode);
                   // factbox2.SetContentUrl('../FactBox/fbBizPartner.aspx?BizPartnerCode=' + BizPartnerCode);
                    popup2.ContentUrl = "../FactBox/fbBizPartner.aspx?BizPartnerCode= '" + aglWarehouseCode.Text + "'";

                    if (Request.QueryString["entry"].ToString() == "N" || (aglRefSO.Text == ""))
                    {
                        if (Session["FirstClick"] == null)
                        {
                            Session["FirstClick"] = "Customer";
                        }
                    }
                    aglRefSO.Text = "";

                    if (chkRefSO.Checked == false)
                    {
                        Session["Datatable"] = "0";
                        gv1.DataSource = sdsDetail;
                        if (gv1.DataSourceID != "")
                        {
                            gv1.DataSourceID = null;
                        }
                        gv1.DataBind();
                        cp.JSProperties["cp_generated"] = true;
                    }
                    //else
                    //{
                    //    cp.JSProperties["cp_calculateonly"] = true;
                    //}
                    //    DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.DeliveryReceiptDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                    //    if (dtbldetail.Rows.Count > 0)
                    //    {
                    //        gv1.DataSource = odsDetail;
                    //    }
                    //    else
                    //    {
                    //        gv1.DataSource = sdsDetail;
                    //    }
                    //    if (gv1.DataSourceID != "")
                    //    {
                    //        gv1.DataSourceID = null;
                    //    }
                    //    gv1.DataBind();
                    //}
                    CascadeCustomerInfo();
                    FilterSO();
                    RefCheck();
                    //Session["RefCheck"] = "1";
                    cp.JSProperties["cp_generated"] = true;
                    aglWarehouseCode.Focus();
                    break;

                case "CallbackRefSO":

                    DataTable checkRefSO = new DataTable();
                    checkRefSO = Gears.RetriveData2(CheckCustomer(), Session["ConnString"].ToString());

                    if (checkRefSO.Rows.Count > 1)
                    {
                        cp.JSProperties["cp_message"] = "Tagged Sales Orders have different customers!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_generated"] = true;
                        aglRefSO.Text = "";
                        aglCustomerCode.Text = "";
                        txtAddress.Text = "";
                        txtTIN.Text = "N/A";
                        speTerms.Text = "";
                        memTotalQty.Text = "";
                        //return;
                    }
                    else
                    {
                        if (Request.QueryString["entry"].ToString() == "N" || (aglCustomerCode.Text == ""))
                        {
                            if (Session["FirstClick"] == null)
                            {
                                Session["FirstClick"] = "Reference";
                                ForRefSO();
                            }
                        }
                        else
                        {
                            ForRefSO();
                        }

                        if ((!String.IsNullOrEmpty(aglWarehouseCode.Text)) || (!String.IsNullOrEmpty(aglType.Text)) || (!String.IsNullOrEmpty(dtpDocDate.Text)))
                        {
                            cp.JSProperties["cp_counterror"] = true;
                        }
                        //CustomerPODetails();
                        GetSelectedVal();
                        RefCheck();
                        FilterSO();
                        Session["RefCheck"] = "1";
                        cp.JSProperties["cp_generated"] = true;
                    }

                    break;

                case "RefGrid":
                    gv1.DataBind();
                    cp.JSProperties["cp_generated"] = true;
                    FilterSO();
                    break;

                case "CallbackCheck":

                    Session["Datatable"] = "0";
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    aglRefSO.Text = "";
                    RefCheck();
                    FilterSO();
                    Session["RefCheck"] = "1";
                    cp.JSProperties["cp_generated"] = true;
                    cp.JSProperties["cp_reference"] = true;
                    txtPLNumber.Focus();
                    break;
            }
        }        
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            if (check == true)
            {
                e.Handled = true;
                DataTable source = new DataTable();
                //if (Session["Datatable"] == "1")
                {
                    source = GetSelectedVal();
                    //}
                    //else
                    //{
                    //source.Columns.Add("LineNumber", typeof(string));
                    //source.Columns.Add("DocNumber", typeof(string));
                    //source.Columns.Add("SONumber", typeof(string));
                    //source.Columns.Add("ItemCode", typeof(string));
                    //source.Columns.Add("ColorCode", typeof(string));
                    //source.Columns.Add("ClassCode", typeof(string));
                    //source.Columns.Add("SizeCode", typeof(string));
                    //source.Columns.Add("DeliveredQty", typeof(decimal), null);
                    //source.Columns.Add("Unit", typeof(string));
                    //source.Columns.Add("BulkQty", typeof(decimal), null);
                    //source.Columns.Add("BulkUnit", typeof(string));
                    //source.Columns.Add("StatusCode", typeof(string));
                    //source.Columns.Add("ReturnedQty", typeof(decimal), null);
                    //source.Columns.Add("ReturnedBulkQty", typeof(decimal), null);
                    //source.Columns.Add("SubstituteItem", typeof(string));
                    //source.Columns.Add("SubstituteColor", typeof(string));
                    //source.Columns.Add("SubstituteClass", typeof(string));
                    //source.Columns.Add("FullDesc", typeof(string));
                    //source.Columns.Add("BaseQty", typeof(decimal), null);
                    //source.Columns.Add("BarcodeNo", typeof(decimal), null);
                    //source.Columns.Add("UnitFactor", typeof(decimal), null);
                    //source.Columns.Add("UnitCost", typeof(decimal), null);
                    //source.Columns.Add("Version", typeof(string));
                    //source.Columns.Add("IsByBulk", typeof(bool), null);
                    //source.Columns.Add("TransPONo", typeof(string));
                    //source.Columns.Add("Field1", typeof(string));
                    //source.Columns.Add("Field2", typeof(string));
                    //source.Columns.Add("Field3", typeof(string));
                    //source.Columns.Add("Field4", typeof(string));
                    //source.Columns.Add("Field5", typeof(string));
                    //source.Columns.Add("Field6", typeof(string));
                    //source.Columns.Add("Field7", typeof(string));
                    //source.Columns.Add("Field8", typeof(string));
                    //source.Columns.Add("Field9", typeof(string));
                    //source.Columns.Add("AverageCost", typeof(decimal), null);
                    //source.Columns.Add("ExpDate", typeof(DateTime), null);
                    //source.Columns.Add("MfgDate", typeof(DateTime), null);
                    //source.Columns.Add("BatchNo", typeof(string));
                    //source.Columns.Add("LotNo", typeof(string)); 
                    //source = GetSelectedValDtl();
                    //DataTable dttbl = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.DeliveryReceiptDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                    //if (dttbl.Rows.Count > 0)
                    //    source = dttbl;
                    //else
                    //    source = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.DeliveryReceiptDetail WHERE DocNumber IS NULL", Session["ConnString"].ToString());

                    ////source = Gears.RetriveData2("SELECT * FROM Sales.DeliveryReceiptDetail A WHERE A.DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                    //source.PrimaryKey = new DataColumn[] { source.Columns["LineNumber"], source.Columns["SONumber"] };
                    //}

                    Gears.RetriveData2("DELETE FROM Sales.DeliveryReceiptDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                    foreach (ASPxDataDeleteValues values in e.DeleteValues)
                    {
                        try
                        {
                            object[] keys = { values.Keys["LineNumber"], values.Keys["DocNumber"] };
                            source.Rows.Remove(source.Rows.Find(keys));
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    int i = 1;
                    foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                    {
                        var SONumber = values.NewValues["SONumber"];
                        var ItemCode = values.NewValues["ItemCode"];
                        var FullDesc = values.NewValues["FullDesc"];
                        var ColorCode = values.NewValues["ColorCode"];
                        var ClassCode = values.NewValues["ClassCode"];
                        var SizeCode = values.NewValues["SizeCode"];
                        var DeliveredQty = values.NewValues["DeliveredQty"];
                        var Unit = values.NewValues["Unit"];
                        var BulkQty = values.NewValues["BulkQty"];
                        var BulkUnit = values.NewValues["BulkUnit"];
                        var StatusCode = values.NewValues["StatusCode"];
                        var SubstituteItem = values.NewValues["SubstituteItem"];
                        var SubstituteColor = values.NewValues["SubstituteColor"];
                        var SubstituteClass = values.NewValues["SubstituteClass"];
                        var ReturnedQty = values.NewValues["ReturnedQty"];
                        var ReturnedBulkQty = values.NewValues["ReturnedBulkQty"];
                        var BaseQty = values.NewValues["BaseQty"];
                        var BarcodeNo = values.NewValues["BarcodeNo"];
                        var UnitFactor = values.NewValues["UnitFactor"];
                        var UnitCost = values.NewValues["UnitCost"];
                        var AverageCost = values.NewValues["AverageCost"];
                        var Field1 = values.NewValues["Field1"];
                        var Field2 = values.NewValues["Field2"];
                        var Field3 = values.NewValues["Field3"];
                        var Field4 = values.NewValues["Field4"];
                        var Field5 = values.NewValues["Field5"];
                        var Field6 = values.NewValues["Field6"];
                        var Field7 = values.NewValues["Field7"];
                        var Field8 = values.NewValues["Field8"];
                        var Field9 = values.NewValues["Field9"];
                        var IsByBulk = values.NewValues["IsByBulk"];
                        var TransPONo = values.NewValues["TransPONo"];
                        var Version = values.NewValues["Version"];
                        var ExpDate = values.NewValues["ExpDate"];
                        var MfgDate = values.NewValues["MfgDate"];
                        var BatchNo = values.NewValues["BatchNo"];
                        var LotNo = values.NewValues["LotNo"];

                        //source.Rows.Add(txtDocNumber.Text, i, SONumber, TransPONo, ItemCode, FullDesc,
                        //            ColorCode, ClassCode, SizeCode, DeliveredQty, Unit, IsByBulk, BulkQty,
                        //            BulkUnit, StatusCode, SubstituteItem, SubstituteColor, SubstituteClass,
                        //            ReturnedQty, BaseQty, BarcodeNo, UnitFactor, ExpDate, MfgDate, BatchNo,
                        //            LotNo, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8,
                        //            Field9, Version);
                        
                        i++;
                    }

                    foreach (ASPxDataUpdateValues values in e.UpdateValues)
                    {
                        object[] keys = { values.NewValues["LineNumber"], values.NewValues["DocNumber"] };
                        DataRow row = source.Rows.Find(keys);
                        row["SONumber"] = values.NewValues["SONumber"];
                        row["ItemCode"] = values.NewValues["ItemCode"];
                        row["FullDesc"] = values.NewValues["FullDesc"];
                        row["ColorCode"] = values.NewValues["ColorCode"];
                        row["ClassCode"] = values.NewValues["ClassCode"];
                        row["SizeCode"] = values.NewValues["SizeCode"];
                        row["DeliveredQty"] = values.NewValues["DeliveredQty"];
                        row["Unit"] = values.NewValues["Unit"];
                        row["BulkQty"] = values.NewValues["BulkQty"];
                        row["BulkUnit"] = values.NewValues["BulkUnit"];
                        row["StatusCode"] = values.NewValues["StatusCode"];
                        row["SubstituteItem"] = values.NewValues["SubstituteItem"];
                        row["SubstituteColor"] = values.NewValues["SubstituteColor"];
                        row["SubstituteClass"] = values.NewValues["SubstituteClass"];
                        row["ReturnedQty"] = values.NewValues["ReturnedQty"];
                        row["BaseQty"] = values.NewValues["BaseQty"];
                        row["BarcodeNo"] = values.NewValues["BarcodeNo"];
                        row["UnitFactor"] = values.NewValues["UnitFactor"];
                        row["Field1"] = values.NewValues["Field1"];
                        row["Field2"] = values.NewValues["Field2"];
                        row["Field3"] = values.NewValues["Field3"];
                        row["Field4"] = values.NewValues["Field4"];
                        row["Field5"] = values.NewValues["Field5"];
                        row["Field6"] = values.NewValues["Field6"];
                        row["Field7"] = values.NewValues["Field7"];
                        row["Field8"] = values.NewValues["Field8"];
                        row["Field9"] = values.NewValues["Field9"];
                        row["IsByBulk"] = values.NewValues["IsByBulk"];
                        row["TransPONo"] = values.NewValues["TransPONo"];
                        row["Version"] = values.NewValues["Version"];
                        row["ExpDate"] = Convert.ToDateTime(Convert.IsDBNull(values.NewValues["ExpDate"]) ? null : values.NewValues["ExpDate"]);  //values.NewValues["ExpDate"];
                        row["MfgDate"] = Convert.ToDateTime(Convert.IsDBNull(values.NewValues["MfgDate"]) ? null : values.NewValues["MfgDate"]);  //values.NewValues["MfgDate"];
                        row["BatchNo"] = values.NewValues["BatchNo"];
                        row["LotNo"] = values.NewValues["LotNo"];
                    }

                    foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                    {
                        _EntityDetail.SONumber = dtRow["SONumber"].ToString();
                        _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                        _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                        _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                        _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                        _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                        _EntityDetail.DeliveredQty = Convert.ToDecimal(String.IsNullOrEmpty(dtRow["DeliveredQty"].ToString()) ? "0" : dtRow["DeliveredQty"].ToString());
                        _EntityDetail.Unit = dtRow["Unit"].ToString();
                        _EntityDetail.BulkQty = Convert.ToDecimal(String.IsNullOrEmpty(dtRow["BulkQty"].ToString()) ? "0" : dtRow["BulkQty"].ToString());
                        _EntityDetail.BulkUnit = dtRow["BulkUnit"].ToString();
                        _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                        _EntityDetail.SubstituteItem = dtRow["SubstituteItem"].ToString();
                        _EntityDetail.SubstituteColor = dtRow["SubstituteColor"].ToString();
                        _EntityDetail.SubstituteClass = dtRow["SubstituteClass"].ToString();
                        _EntityDetail.ReturnedQty = Convert.ToDecimal(String.IsNullOrEmpty(dtRow["ReturnedQty"].ToString()) ? "0" : dtRow["ReturnedQty"].ToString());
                        _EntityDetail.BaseQty = Convert.ToDecimal(String.IsNullOrEmpty(dtRow["BaseQty"].ToString()) ? "0" : dtRow["BaseQty"].ToString());
                        _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
                        _EntityDetail.UnitFactor = Convert.ToDecimal(String.IsNullOrEmpty(dtRow["UnitFactor"].ToString()) ? "0" : dtRow["UnitFactor"].ToString());

                        _EntityDetail.Field1 = dtRow["Field1"].ToString();
                        _EntityDetail.Field2 = dtRow["Field2"].ToString();
                        _EntityDetail.Field3 = dtRow["Field3"].ToString();
                        _EntityDetail.Field4 = dtRow["Field4"].ToString();
                        _EntityDetail.Field5 = dtRow["Field5"].ToString();
                        _EntityDetail.Field6 = dtRow["Field6"].ToString();
                        _EntityDetail.Field7 = dtRow["Field7"].ToString();
                        _EntityDetail.Field8 = dtRow["Field8"].ToString();
                        _EntityDetail.Field9 = dtRow["Field9"].ToString();
                        _EntityDetail.IsByBulk = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsByBulk"]) ? "0" : dtRow["IsByBulk"].ToString());
                        _EntityDetail.TransPONo = dtRow["TransPONo"].ToString();
                        _EntityDetail.Version = dtRow["Version"].ToString();
                        _EntityDetail.ExpDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["ExpDate"]) ? null : dtRow["ExpDate"].ToString());
                        _EntityDetail.MfgDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["MfgDate"]) ? null : dtRow["MfgDate"].ToString());
                        _EntityDetail.BatchNo = dtRow["BatchNo"].ToString();
                        _EntityDetail.LotNo = dtRow["LotNo"].ToString();

                        _EntityDetail.AddDeliveryReceiptDetail(_EntityDetail);
                    }
                }
            }            
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

        private DataTable GetSelectedVal()
        {
            Session["Datatable"] = "0";
            gv1.DataSource = sdsDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            DataTable dt = new DataTable();
            string[] selectedValues = aglRefSO.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
            string RefSO = selectionCriteria.ToString();
            
            sdsSODetail.SelectCommand = ";WITH A AS "
                    + " (SELECT A.DocNumber, A.DocNumber AS SONumber, ItemCode,FullDesc,ColorCode,ClassCode,SizeCode,ISNULL(OrderQty,0) - ISNULL(DeliveredQty,0) AS DeliveredQty,Unit,BulkQty,BulkUnit,StatusCode,0.00 AS ReturnedQty, "
                    + " SubstituteItem,SubstituteColor,SubstituteClass, 0.00 AS BaseQty,BarcodeNo,ISNULL(UnitFactor,0.0000) AS UnitFactor,A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7,A.Field8,A.Field9,'2' AS Version, ISNULL(CustomerPONo,'') AS TransPONo FROM Sales.SalesOrderDetail A "
                    + " INNER JOIN Sales.SalesOrder B ON A.DocNumber = B.DocNumber WHERE A."+ RefSO + ") "
                    + " SELECT DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY SONumber) AS VARCHAR(5)),5) AS LineNumber, SONumber, A.ItemCode, A.FullDesc, ColorCode, ClassCode, SizeCode, DeliveredQty, B.UnitBase AS Unit, BulkQty, BulkUnit, StatusCode, ReturnedQty, SubstituteItem, SubstituteColor, SubstituteClass, "
                    + " BaseQty, BarcodeNo, UnitFactor, A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, Version, ISNULL(B.IsByBulk,0) AS IsByBulk, TransPONo, CAST(NULL AS Date) AS ExpDate, CAST(NULL AS Date) AS MfgDate, '' AS BatchNo, '' AS LotNo FROM A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode ";

            gv1.DataSource = sdsSODetail;
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
            dt.Columns["LineNumber"],dt.Columns["DocNumber"]};
            
            return dt;
        }
         
        private DataTable GetSelectedValDtl()
        { 
            gv1.DataSource = sdsDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            DataTable dt = new DataTable(); 
            sdsSODetail.SelectCommand = "";


            DataTable dttbl = Gears.RetriveData2("SELECT DISTINCT * FROM Sales.DeliveryReceiptDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
            if (dttbl.Rows.Count > 0)
                dt = dttbl;
            else
                dt = Gears.RetriveData2("SELECT TOP 1 '" + txtDocNumber.Text + "' AS DocNumber,'10000' AS LineNumber, ' ' SONumber, '' TransPONo, '' ItemCode, '' FullDesc, "
                                       + "'' ColorCode, '' ClassCode, '' SizeCode, 0.0000 DeliveredQty, '' Unit, CAST(0 AS BIT) AS IsByBulk, 0.00 BulkQty, "
                                       + "'' BulkUnit,''  StatusCode, '' SubstituteItem, '' SubstituteColor, '' SubstituteClass, "
                                       + "0.0000 ReturnedQty, 0.0000 BaseQty, '' BarcodeNo, 0.0000 UnitFactor, CAST(NULL AS DATE) ExpDate, CAST(NULL AS DATE) MfgDate, '' BatchNo, "
                                       + "'' AS LotNo, '' Field1, '' Field2, '' Field3, '' Field4, '' Field5, '' Field6, '' Field7, '' Field8, "
                                       + "'' Field9, '' Version FROM Sales.DeliveryReceiptDetail", Session["ConnString"].ToString());
            
                                //SELECT '10000' AS LineNumber, " + txtDocNumber.Text + " AS DocNumber,[SONumber]"
                                //       +",[ItemCode],[ColorCode],[ClassCode],[SizeCode],[DeliveredQty],[Unit],[BulkQty],[BulkUnit]"
                                //       +",[StatusCode],[ReturnedQty],[ReturnedBulkQty],[SubstituteItem],[SubstituteColor]"
                                //       +",[SubstituteClass],[FullDesc],[BaseQty],[BarcodeNo],[UnitFactor],[UnitCost]"      
                                //       +",[Version],[IsByBulk],[TransPONo],[Field1],[Field2],[Field3],[Field4],[Field5]"
                                //       +",[Field6],[Field7],[Field8],[Field9],[AverageCost],[ExpDate],[MfgDate]"
                                //       +",[BatchNo],[LotNo] FROM [GWL-GLAUCUS].[Sales].[DeliveryReceiptDetail] where DocNumber IS NULL", Session["ConnString"].ToString());
            
            gv1.DataSource = sdsSODetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();

            
            //foreach (GridViewColumn col in gv1.VisibleColumns)
            //{
            //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    dt.Columns.Add(dataColumn.FieldName);
            //}
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumber"],dt.Columns["DocNumber"]};

            return dt;
        }    
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

        protected void aglType_Init(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(aglType.Text))
            {
                aglType.DataSourceID = "sdsType";
                aglType.DataBind();
                aglType.Text = "SALES ORDER";
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsBizPartnerCus.ConnectionString = Session["ConnString"].ToString();
            //sdsType.ConnectionString = Session["ConnString"].ToString();
            //sdsUnit.ConnectionString = Session["ConnString"].ToString();
            //sdsBulkUnit.ConnectionString = Session["ConnString"].ToString();
            //sdsRefSO.ConnectionString = Session["ConnString"].ToString();
            //sdsSODetail.ConnectionString = Session["ConnString"].ToString();
            //sdsWarehouse.ConnectionString = Session["ConnString"].ToString();
        }

        protected void FilterSO()
        {
            if (!String.IsNullOrEmpty(aglCustomerCode.Text))
            {
                Session["RefSOFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, CustomerPONo, Currency, SUM(B.OrderQty-B.DeliveredQty) AS Quantity, Remarks, Terms, TargetDeliveryDate FROM Sales.SalesOrder A "
                + "INNER JOIN Sales.SalesOrderDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND Status IN ('N','P') AND CustomerCode ='" + aglCustomerCode.Text + "' "
                + "GROUP BY A.DocNumber, DocDate, CustomerCode, CustomerPONo, Currency, Remarks, Terms, TargetDeliveryDate";
                sdsRefSO.SelectCommand = Session["RefSOFilter"].ToString();
                sdsRefSO.DataBind();
            }
            if (String.IsNullOrEmpty(aglCustomerCode.Text))
            {
                Session["RefSOFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, CustomerPONo, Currency, SUM(B.OrderQty-B.DeliveredQty) AS Quantity, Remarks, Terms, TargetDeliveryDate FROM Sales.SalesOrder A "
                + "INNER JOIN Sales.SalesOrderDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND Status IN ('N','P') "
                + "GROUP BY A.DocNumber, DocDate, CustomerCode, CustomerPONo, Currency, Remarks, Terms, TargetDeliveryDate";
                sdsRefSO.SelectCommand = Session["RefSOFilter"].ToString();
                sdsRefSO.DataBind();
            }
        }
        protected void RefCheck()
        {
            if (chkRefSO.Checked == false)
            {
                aglRefSO.ClientEnabled = true;
                btnGenDetail.ClientEnabled = true;
                gv1.Columns["SONumber"].Width = 120;
            }
            else
            {
                aglRefSO.ClientEnabled = false;
                btnGenDetail.ClientEnabled = false;
                gv1.Columns["SONumber"].Width = 0;
            }
        }

        public string CheckCustomer()
        {
            string Value = "";
            string[] selectedValues = aglRefSO.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
            string RefSO = selectionCriteria.ToString();

            Value = "SELECT DISTINCT CustomerCode FROM Sales.SalesOrder WHERE " + RefSO ;

            return Value;
        }


        public void Checking()
        {
            DataTable check = new DataTable();
            check = Gears.RetriveData2(CheckCustomer(), Session["ConnString"].ToString());

            if (check.Rows.Count > 1)
            {
                error = true;
                edit = true;
            }
        }
        public void CascadeCustomerInfo()
        {
            if (Session["FirstClick"] == "Customer")
            {
                if (!String.IsNullOrEmpty(aglCustomerCode.Value.ToString()))
                {
                    DataTable info = Gears.RetriveData2("SELECT DeliveryAddress, ISNULL(TIN,'N/A') AS TIN, ARTerm FROM Masterfile.BPCustomerInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode WHERE A.BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());
                    if (info.Rows.Count != 0)
                    {
                        speTerms.Text = info.Rows[0]["ARTerm"].ToString();
                        txtAddress.Text = info.Rows[0]["DeliveryAddress"].ToString();
                        txtTIN.Text = info.Rows[0]["TIN"].ToString();
                    }
                }

                //Session["FirstClick"] = null;
            }

            if (Session["FirstClick"] == "Reference")
            {
                ForRefSO();

                //Session["FirstClick"] = null;                
            }

            if (Session["FirstClick"] == null)
            {
                if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                {
                    DataTable info = Gears.RetriveData2("SELECT DeliveryAddress, ISNULL(TIN,'N/A') AS TIN, ARTerm FROM Masterfile.BPCustomerInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode WHERE A.BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());
                    if (info.Rows.Count != 0)
                    {
                        speTerms.Text = info.Rows[0]["ARTerm"].ToString();
                        txtAddress.Text = info.Rows[0]["DeliveryAddress"].ToString();
                        txtTIN.Text = info.Rows[0]["TIN"].ToString();
                    }
                }
                else
                {
                    speTerms.Text = "";
                    txtAddress.Text = "";
                    txtTIN.Text = "N/A";
                }
            }

        }

        //public void CustomerPODetails()
        //{
            //string[] selectedValues = aglRefSO.Text.Split(';');
            //CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
            //string Query = selectionCriteria.ToString();

            //DataTable PONumber = Gears.RetriveData2("SELECT DocNumber, CustomerPONo INTO #Customer "
            //    + " FROM Sales.SalesOrder WHERE " + Query + " AND ISNULL(CustomerPONo,'') != '' ORDER BY DocNumber"
            //    + " SELECT TOP 1 ISNULL(STUFF((SELECT ', ' + CAST(CustomerPONo AS VARCHAR(MAX)) FROM #Customer"
            //    + " FOR XML PATH(''), TYPE).value('.','VARCHAR(MAX)'),1,2,' '),'') AS CustomerPO FROM Sales.SalesOrder"
            //    + " DROP TABLE #Customer ");

            //txtCustomerPONo.Text = PONumber.Rows[0]["CustomerPO"].ToString();

        //}

        public void ForRefSO()
        {
            string pick = aglRefSO.Text;
            string refpick = "";
            string cust = "";
            string terms = "";

            int cnt = pick.LastIndexOf(";");

            if (cnt > 0)
            {
                refpick = pick.Substring(0, pick.IndexOf(";"));

                DataTable customer = Gears.RetriveData2("SELECT DISTINCT CustomerCode FROM Sales.SalesOrder WHERE DocNumber = '" + refpick + "'", Session["ConnString"].ToString());

                if (customer.Rows.Count != 0)
                {
                    cust = customer.Rows[0]["CustomerCode"].ToString();
                }

                DataTable name = Gears.RetriveData2("SELECT DeliveryAddress, ISNULL(TIN,'N/A') AS TIN, ISNULL(ARTerm,0) AS ARTerm FROM Masterfile.BPCustomerInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode WHERE A.BizPartnerCode = '" + cust.Trim() + "'", Session["ConnString"].ToString());
                if (name.Rows.Count != 0)
                {
                    speTerms.Text = name.Rows[0]["ARTerm"].ToString();
                    txtAddress.Text = name.Rows[0]["DeliveryAddress"].ToString();
                    txtTIN.Text = name.Rows[0]["TIN"].ToString();
                }

            }
            else
            {
                refpick = pick;
                DataTable get = Gears.RetriveData2("SELECT ISNULL(Terms,0) AS Terms, CustomerCode FROM Sales.SalesOrder WHERE DocNumber = '" + refpick + "'", Session["ConnString"].ToString());
                if (get.Rows.Count != 0)
                {
                    terms = get.Rows[0]["Terms"].ToString();
                    cust = get.Rows[0]["CustomerCode"].ToString();
                }

                DataTable cust1 = Gears.RetriveData2("SELECT ISNULL(ARTerm,0) AS CustomerTerm FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + cust.Trim() + "'", Session["ConnString"].ToString());
                if (cust1.Rows.Count != 0)
                {
                    speTerms.Text = terms == "0" || terms == "0.00" ? cust1.Rows[0]["CustomerTerm"].ToString() : terms;
                }

                DataTable name = Gears.RetriveData2("SELECT DeliveryAddress, ISNULL(TIN,'N/A') AS TIN, ISNULL(ARTerm,0) AS ARTerm FROM Masterfile.BPCustomerInfo A INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode WHERE A.BizPartnerCode = '" + cust.Trim() + "'", Session["ConnString"].ToString());
                if (name.Rows.Count != 0)
                {
                    txtAddress.Text = name.Rows[0]["DeliveryAddress"].ToString();
                    txtTIN.Text = name.Rows[0]["TIN"].ToString();
                }
            }
            aglCustomerCode.Text = cust;

        }

        protected void gv1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {

            cp.JSProperties["cp_test"] = true;
        }

        protected void aglRefSO_Init(object sender, EventArgs e)
        {
            if (Session["RefSOFilter"] != null)
            {
                sdsRefSO.SelectCommand = Session["RefSOFilter"].ToString();
                sdsRefSO.DataBind();
            }
        }



        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.DataSource = GetDataTableFromCacheOrDatabase();
                gridLookup.DataBind();
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



                DataTable countcolor = Gears.RetriveData2("Select Top 1 UPPER(ColorCode) AS ColorCode, UPPER(ClassCode) AS ClassCode,UPPER(SizeCode) AS SizeCode,UPPER(UnitBase) AS UnitBase, " +
                                                        " FullDesc, UPPER(UnitBulk) AS UnitBulk, A.IsByBulk, ISNULL(StatusCode,'') AS StatusCode " +
                                                        " from masterfile.item a left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["IsByBulk"].ToString() + ";";
                    codes += dt["StatusCode"].ToString() + ";";
                    codes += "2;";
                }

                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
        public DataTable GetDataTableFromCacheOrDatabase()
        {
            DataTable dataTable = HttpContext.Current.Cache["SOsdsItem"] as DataTable;
            if (dataTable == null)
            {
                dataTable = Gears.RetriveData2("SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());
                HttpContext.Current.Cache["SOsdsItem"] = dataTable;
            }
            return dataTable;
        }
    }
}