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
    public partial class frmSalesOrder : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string val = "";   //Boolean for Edit Mode

        Entity.SalesOrder _Entity = new SalesOrder();//Calls entity odsHeader
        Entity.SalesOrder.SalesOrderDetail _EntityDetail = new SalesOrder.SalesOrderDetail();//Call entity sdsDetail

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

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            txtDocNumber.ReadOnly = true;

            if (!IsPostBack)
            {
                //Session["Datatable"] = null;
                //Session["QuoteFilter"] = null;
                //Session["referencequote"] = null;
                //Session["FilterExpression"] = null; 
                Cache.Remove("SOsdsItem");
                Session["SODatatable"] = null;
                Session["SOQuoteFilter"] = null;
                Session["SOreferencequote"] = null;
                Session["SOFilterExpression"] = null;
                Session["SOitem"] = null;
                Session["SOclr"] = null;
                Session["SOcls"] = null;
                Session["SOqty"] = null;
                Session["SOprice"] = null;
                Session["SOunit"] = null;
                Session["SOdesc"] = null;
                Session["SObulk"] = null;
                Session["SOisbulk"] = null;


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                } 

                //_Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); 
                //dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                //aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                //dtpTargetDate.Text = String.IsNullOrEmpty(_Entity.TargetDeliveryDate.ToString()) ? null : Convert.ToDateTime(_Entity.TargetDeliveryDate.ToString()).ToShortDateString();
                sdsBizPartnerCus.SelectCommand = "SELECT DISTINCT BizPartnerCode, Name FROM Masterfile.BPCustomerInfo WHERE ISNULL(IsInActive,0)=0";
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglCustomerCode.Value = _Entity.CustomerCode.ToString();
                dtpTargetDate.Text = String.IsNullOrEmpty(_Entity.TargetDeliveryDate.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.TargetDeliveryDate.ToString()).ToShortDateString();
                Session["TargetDate"] = dtpTargetDate.Text;
                Session["DocDate"] = dtpDocDate.Text;
                Session["CustomerCode"] = aglCustomerCode.Text;

                if (_Entity.Status.ToString().Trim() == "N")
                {
                    txtStatus.Text = "NEW";
                }
                else if (_Entity.Status.ToString().Trim() == "P")
                {
                    txtStatus.Text = "PARTIALLY SERVED";
                }
                else if (_Entity.Status.ToString().Trim() == "C")
                {
                    txtStatus.Text = "CLOSED";
                }
                else if (_Entity.Status.ToString().Trim() == "X")
                {
                    txtStatus.Text = "FORCED CLOSED";
                }
                else if (_Entity.Status.ToString().Trim() == "L")
                {
                    txtStatus.Text = "CANCELLED";
                }
                else if (_Entity.Status.ToString().Trim() == "A")
                {
                    txtStatus.Text = "PARTIAL CLOSED";
                }
                else
                {
                    txtStatus.Text = "NEW";
                }
                txtDateCompleted.Value = _Entity.DateCompleted.ToString();
                txtCustomerPONo.Value = _Entity.CustomerPONo.ToString();
                aglQuote.Value = _Entity.QuotationNo.ToString();
                val = _Entity.QuotationNo.ToString();


                //aglProcurementDoc.Value = _Entity.ProcurementDoc.ToString();
                //DataTable proc = Gears.RetriveData2("SELECT Code, Description FROM IT.GenericLookup WHERE LookUpKey ='SOPRDOC' AND Code = '" + _Entity.ProcurementDoc.ToString() + "'", Session["ConnString"].ToString());
                //if (proc.Rows.Count == 0)
                //{
                //    aglProcurementDoc.Text = "No Procurement";
                //}
                //else
                //{
                //    aglProcurementDoc.Text = proc.Rows[0]["Description"].ToString();
                //}
                //aglProcurementDoc.Value = _Entity.ProcurementDoc.ToString();  
                DataTable proc = new DataTable(); 
                if (String.IsNullOrEmpty(_Entity.LastEditedBy.ToString()) && Request.QueryString["entry"].ToString() == "N")
                {
                    proc = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code = 'PROC_DOC'", Session["ConnString"].ToString());
                    if (proc.Rows.Count == 0)
                    {
                        aglProcurementDoc.Text = "No Procurement";
                        aglProcurementDoc.Value = "A";
                    }
                    else
                    {
                        aglProcurementDoc.Value = proc.Rows[0]["Value"].ToString();
                    }
                }
                else
                { 
                    aglProcurementDoc.Value = _Entity.ProcurementDoc.ToString();
                }

                chkVatable.Value = _Entity.Vatable;
                chkIsWithQuote.Value = _Entity.IsWithQuote;
                speExchangeRate.Value = _Entity.ExchangeRate.ToString();
                txtPesoAmount.Value = _Entity.PesoAmount.ToString();
                txtForeignAmount.Value = _Entity.ForeignAmount.ToString();
                aglCurrency.Value = _Entity.Currency.ToString(); 
                speTerms.Value = _Entity.Terms.ToString();
                txtGross.Value = _Entity.GrossVatableAmount.ToString();
                memTotalQty.Value = _Entity.TotalQty.ToString();
                txtTotalBulkQty.Value = _Entity.TotalBulkQty;
                txtNonVatable.Value = _Entity.NonVatableAmount.ToString();
                txtVatAmount.Value = _Entity.VATAmount.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();

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
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;
                txtForcedClosedBy.Text = _Entity.ForcedClosedBy;
                txtForcedClosedDate.Text = _Entity.ForcedClosedDate;


                gv1.KeyFieldName = "DocNumber;LineNumber";

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithdr"] == "1" || chkIsWithQuote.Value.ToString() == "True"))
                    { 
                        aglQuote.ClientEnabled = true;
                    }
                    else if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithdr"] == "1" || chkIsWithQuote.Value.ToString() == "True"))
                    { 
                        aglQuote.ClientEnabled = true;
                    } 
                }

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
                        txtStatus.Text = "NEW";
                        aglQuote.ClientEnabled = false;
                        if (chkIsWithQuote.Value.ToString() == "True")
                        {
                            aglQuote.ClientEnabled = true;

                            if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                            {
                                //sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                                //Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                //    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                                //    + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                                //sdsQuotation.SelectCommand = Session["QuoteFilter"].ToString();
                                //aglQuote.DataBind();
                                sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                                Session["SOQuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                                    + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                            }
                        }
                        else if (chkIsWithQuote.Value.ToString() == "False")
                        {
                            aglQuote.ClientEnabled = false;
                        }
                        aglQuote.Text = val;
                        speExchangeRate.Text = "1.00";
                        break;
                    case "E":
                        updateBtn.Text = "Update"; 
                        if (chkIsWithQuote.Value.ToString() == "True")
                        {
                            aglQuote.ClientEnabled = true;

                            if (!String.IsNullOrEmpty(aglCustomerCode.Text))
                            {
                                //sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                                //Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                //    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                                //    + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                                //sdsQuotation.SelectCommand = Session["QuoteFilter"].ToString();
                                //aglQuote.DataBind();
                                sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                                Session["SOQuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                                    + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                            }
                        }
                        else if (chkIsWithQuote.Value.ToString() == "False")
                        {
                            aglQuote.ClientEnabled = false;
                        }
                        if (String.IsNullOrEmpty(speExchangeRate.Text) || speExchangeRate.Text == "0")
                        {
                            speExchangeRate.Text = "1.00";
                        }
                        aglQuote.Text = val;
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
            //} 
            //SessionRef(); 
            //DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.SalesOrderDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
            //gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

                InitControls(); 
                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.SalesOrderDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                gvSizeHorizontal.DataSourceID = "sdsSizeHorizontal";
                gvSizes.DataSourceID = "sdsSizes";
                gvSizeHorizontal.DataBind();
                gvSizes.DataBind();
            } 
            SessionRef(); 


        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "SLSORD";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsSales.GSales.SalesOrder_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        //protected void TextboxLoad(object sender, EventArgs e)
        protected void TextboxLoad(ASPxEdit sender)
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        //protected void LookupLoad(object sender, EventArgs e)
        protected void LookupLoad(ASPxEdit sender)
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;                       
        }
        protected void CheckBoxLoad(ASPxEdit sender)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void ComboBoxLoad(ASPxEdit sender)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        } 
        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        //protected void Date_Load(object sender, EventArgs e)
        protected void Date_Load(ASPxEdit sender)
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        //protected void SpinEdit_Load(object sender, EventArgs e)
        protected void SpinEdit_Load(ASPxEdit sender)
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        } 

        //ADDED START
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
        //ADDED END

        protected void gvLookupLoad(object sender, EventArgs e)
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D") 
            { 
                look.Enabled = false; 
            }
            else
            {
                look.Enabled = true; 
            } 
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsBehavior.AllowSort = false;
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            { 
                e.Editor.ReadOnly = true;
            }
            else
            {
                e.Editor.ReadOnly = false;
            }

        }
        protected void gvTextBoxLoad(object sender, EventArgs e)
        {
            GridViewDataTextColumn text = sender as GridViewDataTextColumn;
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                text.ReadOnly = true;
            }
            else
            { 
                text.ReadOnly = false;
            }
        }


        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   
            //if (e.ButtonType == ColumnCommandButtonType.Delete)
            //{
            //    e.Image.IconID = "actions_cancel_16x16";
            //}
            //if (e.ButtonType == ColumnCommandButtonType.New)
            //{
            //    e.Image.IconID = "actions_addfile_16x16"; 
            //}
            //if (e.ButtonType == ColumnCommandButtonType.Edit)
            //{
            //    e.Image.IconID = "actions_addfile_16x16";
            //}
            //if (e.ButtonType == ColumnCommandButtonType.Update)
            //{
            //    e.Visible = false;
            //}
            //if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            //{
            //    if (e.ButtonType == ColumnCommandButtonType.Edit ||
            //        e.ButtonType == ColumnCommandButtonType.Cancel)
            //        e.Visible = false;
            //}
            //if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            //{ 
            //    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
            //    {
            //        e.Visible = true;
            //    } 
            //}
            //if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            //{
            //    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
            //    {
            //        e.Visible = false;
            //    }
            //}
             
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                } 
                if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
                { 
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
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

            //if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            //{
            //    if (e.ButtonID == "Delete")
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}

            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
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
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            //{
            //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //    if (Session["SOitemID"] != null)
            //    {
            //        if (Session["SOitemID"].ToString() == glIDFinder(gridLookup.ID))
            //        {
            //            gridLookup.GridView.DataSourceID = "sdsItemDetail";
            //            sdsItemDetail.SelectCommand = Session["SOsql"].ToString(); 
            //            gridLookup.DataBind();
            //        }
            //    } 
            //}

            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                if (Session["SOFilterExpression"] != null)
                {
                    sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
                    gridLookup.GridView.DataSourceID = "sdsItemDetail";
                    sdsItemDetail.SelectCommand = Session["SOFilterExpression"].ToString();
                }
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var itemlookup = sender as ASPxGridView;

            if (Request.Params["__CALLBACKPARAM"].ToString().Contains("GLP_AC"))
            {
                itemlookup.JSProperties["cp_endedit"] = true;
                return;
            }
            if (Request.Params["__CALLBACKPARAM"].ToString().Contains("GLP_F") || Request.Params["__CALLBACKPARAM"].ToString().Contains("CUSTOMCALLBACK0")) return;
            
            string column = e.Parameters.Split('|')[0]; 
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC")) return; 
            string itemcode = e.Parameters.Split('|')[1]; 
            string val = e.Parameters.Split('|')[2]; 
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC")) return; 
            
            string codes = "";

            //START ADDED NEW
            string colorcode = e.Parameters.Split('|')[3];
            string classcode = e.Parameters.Split('|')[4];
            string sizecode = e.Parameters.Split('|')[5];
            //END ADDED NEW


            if (Request.Params["__CALLBACKPARAM"].ToString().Contains("GLP_F"))
            {
               val = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1).Split(';')[1];
            }

            if (e.Parameters.Contains("ItemCode"))
            {

                // kulang pa to ng set up para sa StatusCode
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;
                 
                DataTable countitem = Gears.RetriveData2("SELECT DISTINCT B.ColorCode,B.ClassCode,B.SizeCode, ISNULL(UnitBase,'') AS UnitBase, FullDesc, ISNULL(UnitBulk,'') AS UnitBulk, ISNULL(A.IsByBulk,0) IsByBulk, ISNULL(StatusCode,'') AS StatusCode, "
                     + " ISNULL(A.UpdatedPrice,0) AS UnitPrice, 'NONV' AS VATCode, 0 AS IsVAT, ISNULL(SubstitutedItem,'') SubstitutedItem, ISNULL(SubstitutedClass,'') SubstitutedClass, ISNULL(SubstitutedColor,'') SubstitutedColor, ISNULL(SubstitutedSize,'') SubstitutedSize FROM Masterfile.Item A LEFT JOIN Masterfile.ItemDetail B on A.ItemCode = B.ItemCode "
                     + " LEFT JOIN Masterfile.ItemCustomerPrice C ON B.ItemCode = C.ItemCode AND B.ColorCode = C.ColorCode AND  B.ClassCode = C.ClassCode AND B.SizeCode = C.SizeCode AND C.Customer = '" + Session["CustomerCode"].ToString() + "' "
                     + " WHERE A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());


                DataTable desc = Gears.RetriveData2("SELECT DISTINCT FullDesc, ISNULL(IsByBulk,0) AS IsByBulk, UnitBase, UnitBulk, ISNULL(UpdatedPrice,0) AS UnitPrice, 'NONV' AS VATCode, 0 AS IsVAT "
                                                  + " FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;


                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntsze = countsize.Rows.Count;
                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += dt["UnitBulk"].ToString() + ";";
                        codes += dt["IsByBulk"].ToString() + ";";
                        codes += dt["StatusCode"].ToString() + ";";
                        codes += dt["UnitPrice"].ToString() + ";";
                        codes += dt["VATCode"].ToString() + ";";
                        codes += dt["IsVAT"].ToString() + ";";
                        codes += dt["SubstitutedItem"].ToString() + ";";
                        codes += dt["SubstitutedColor"].ToString() + ";";
                        codes += dt["SubstitutedClass"].ToString() + ";";
                        codes += dt["SubstitutedSize"].ToString() + ";";
                    }
                }
                else
                {
                    //(col) cla  siz
                    // col (cla) siz
                    // col  cla (siz)
                    //(col)(cla) siz
                    // col (cla)(siz)
                    //(col) cla (siz)

                    if (cntclr == 1)
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                    else
                        codes = ";";
                    if (cntcls == 1)
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                    else
                        codes += ";";
                    if (cntsze == 1)
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                    else
                        codes += ";";
                    codes += desc.Rows[0]["UnitBase"].ToString() + ";";
                    codes += desc.Rows[0]["FullDesc"].ToString() + ";";
                    codes += desc.Rows[0]["UnitBulk"].ToString() + ";";
                    codes += desc.Rows[0]["IsByBulk"].ToString() + ";"; 
                    codes += ";";
                    codes += desc.Rows[0]["UnitPrice"].ToString() + ";";
                    codes += desc.Rows[0]["VATCode"].ToString() + ";";
                    codes += desc.Rows[0]["IsVAT"].ToString() + ";;;;;"; 
                }

                itemlookup.JSProperties["cp_identifier"] = "ItemCode";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("VATCode"))
            {
                DataTable vat = Gears.RetriveData2("SELECT DISTINCT ISNULL(Rate,1) AS Rate FROM Masterfile.Tax WHERE TCode = '" + itemcode + "'", Session["ConnString"].ToString());

                if (vat.Rows.Count > 0)
                {
                    foreach (DataRow dt in vat.Rows)
                    {
                        codes = dt["Rate"].ToString();
                    }
                }

                itemlookup.JSProperties["cp_identifier"] = "VAT";
                itemlookup.JSProperties["cp_codes"] = Convert.ToDecimal(codes) + ";";
            }
            //START ADDED NEW
            else if (e.Parameters.Contains("Subs"))
            {

                DataTable dtItem = Gears.RetriveData2("SELECT  BrandCode,GenderCode,ProductCategoryCode,ProductSubCatCode from Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                 
                DataTable getStatus = Gears.RetriveData2("select StatusCode,Price,row_number() over (partition BY A.StockNumber,StatusCode " +
                                        " ORDER BY EffectivityDate DESC) rn ,EffectivityDate " +
                                        " into #SMPH from Retail.StockMasterPriceHistory A " +
                                        " where ISNULL(EffectivityDate,'1990-01-01')<= '" + Session["TargetDAte"].ToString() + "' " +
                                        " and StockNumber= '" + itemcode + "' " +
                                        " SELECT top 1 StatusCode,Price  FROM #SMPH " +
                                        " where rn='1' " +
                                         " ORDER BY EffectivityDate DESC " +
                                        " DROP TABLE #SMPH ", Session["ConnString"].ToString());//ADD CONN

                if (dtItem.Rows.Count > 0 && getStatus.Rows.Count > 0)
                {
                    DataTable getPriceDiscount1st = Gears.RetriveData2("SELECT UnitPrice,DiscountRate FROM Masterfile.BPCustomerCommission where CustomerCode = '" + Session["CustomerCode"].ToString() + "' "
                                                            + " and (Brand = '" + dtItem.Rows[0]["BrandCode"].ToString() + "' OR ISNULL(Brand,'')='') "
                                                            + " and (GenderCode = '" + dtItem.Rows[0]["GenderCode"].ToString() + "' OR ISNULL(GenderCode,'')='') "
                                                            + " and (ProductCategoryCode = '" + dtItem.Rows[0]["ProductCategoryCode"].ToString() + "' OR ISNULL(ProductCategoryCode,'')='') "
                                                            + " and (ProductSubCategoryCode = '" + dtItem.Rows[0]["ProductSubCatCode"].ToString() + "' OR ISNULL(ProductSubCategoryCode ,'')='') "
                                                            + " and (ProductStatus = '" + getStatus.Rows[0]["StatusCode"].ToString() + "' OR ISNULL(ProductStatus,'')='') ", Session["ConnString"].ToString());//ADD CONN

                    DataTable subs1 = Gears.RetriveData2("SELECT case when '" + Session["DocDate"].ToString() + "' BETWEEN DateFrom AND DateTo THEN 'YES' ELSE 'NO' END as KeyField,ISNULL(Price,0)- case when '" + Session["DocDate"].ToString() + "' BETWEEN DateFrom AND DateTo THEN   ( (CASE WHEN ISNULL(IsApplicable,0)=1 THEN DiscountRate ELSE 0 END /100 )  * ISNULL(Price,0) )  ELSE 0 END as Price,CASE WHEN ISNULL(IsApplicable,0) =1 THEN  DiscountRate ELSE 0  END as DiscountRate"
                          + " FROM Masterfile.ItemCustomerPrice WHERE ItemCode = '" + itemcode + "' AND ColorCode = '" + colorcode + "' AND ClassCode = '" + classcode + "' AND SizeCode = '" + sizecode + "' AND Customer = '" + Session["CustomerCode"].ToString() + "'", Session["ConnString"].ToString());
                    if (subs1.Rows.Count > 0)
                    {
                        if (subs1.Rows[0]["KeyField"].ToString() == "YES")
                        {
                            codes = subs1.Rows[0]["Price"].ToString() + ";";
                            codes += subs1.Rows[0]["DiscountRate"].ToString() + ";";
                        }
                        else
                        {
                            codes = subs1.Rows[0]["Price"].ToString() + ";";
                            codes += "" + ";";
                        }
                    }
                    else
                    {
                        if (getPriceDiscount1st.Rows.Count > 0)
                        {
                            codes = getPriceDiscount1st.Rows[0]["UnitPrice"].ToString() + ";";
                            codes += getPriceDiscount1st.Rows[0]["DiscountRate"].ToString() + ";";
                        }
                        else
                        {
                            if (getStatus.Rows.Count > 0)
                            {
                                codes = getStatus.Rows[0]["Price"].ToString() + ";";
                                codes += "" + ";";
                            }
                            else
                            {
                                codes = "" + ";";
                                codes += "" + ";";
                            }
                        }
                    }
                }
                DataTable subs = Gears.RetriveData2("SELECT DISTINCT ISNULL(SubstitutedItem,'') AS SubstitutedItem, ISNULL(SubstitutedColor,'') AS SubstitutedColor, ISNULL(SubstitutedClass,'') AS SubstitutedClass, ISNULL(SubstitutedSize,'') AS SubstitutedSize "
                        + " FROM Masterfile.ItemCustomerPrice WHERE ItemCode = '" + itemcode + "' AND ColorCode = '" + colorcode + "' AND ClassCode = '" + classcode + "' AND SizeCode = '" + sizecode + "' AND Customer = '" + Session["CustomerCode"].ToString() + "'", Session["ConnString"].ToString());
                //RA
                if (subs.Rows.Count > 0)
                {
                    foreach (DataRow dt in subs.Rows)
                    {
                        codes += dt["SubstitutedItem"].ToString() + ";";
                        codes += dt["SubstitutedColor"].ToString() + ";";
                        codes += dt["SubstitutedClass"].ToString() + ";";
                        codes += dt["SubstitutedSize"].ToString() + ";";
                    }
                }

                itemlookup.JSProperties["cp_identifier"] = "Subs";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            //END ADDED NEW
            else
            {

                int colind = 0;
                string colname = "";
                if (Request.Params["__CALLBACKID"].Contains("glColorCode"))
                {
                    colname = "glColorCode";
                    colind = 5;
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["SOitemID"] = "ColorCode";
                }
                else if (Request.Params["__CALLBACKID"].Contains("glClassCode"))
                {
                    colname = "glClassCode";
                    colind = 6;
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["SOitemID"] = "ClassCode";
                }
                else if (Request.Params["__CALLBACKID"].Contains("glSizeCode"))
                {
                    colname = "glSizeCode";
                    colind = 7;
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["SOitemID"] = "SizeCode";
                }
                else if (Request.Params["__CALLBACKID"].Contains("glUnitBase"))
                {
                    colname = "glUnitBase";
                    colind = 8;
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, UnitBase FROM Masterfile.Item where ItemCode = '" + itemcode + "'";
                    Session["SOitemID"] = "UnitBase";
                }

                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[colind], colname);
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                Session["SOsql"] = sdsItemDetail.SelectCommand; 
                grid.DataSourceID = "sdsItemDetail";
                grid.DataBind();

                if (Request.Params["__CALLBACKPARAM"].ToString().Contains("GLP_F")) return;
                if (val != "")
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

                if (val == "")
                {
                    lookup.Value = null;
                }
                itemlookup.JSProperties["cp_finload"] = true;
                itemlookup.JSProperties["cp_identifier"] = "Sku";
            }  
        }

        public string glIDFinder(string glID)
        {
            if (glID.Contains("ColorCode"))
                return "ColorCode";
            else if (glID.Contains("ClassCode"))
                return "ClassCode";
            else
                return "SizeCode";
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

                _Entity.DocNumber = txtDocNumber.Value.ToString();
                _Entity.DocDate = dtpDocDate.Text;
                _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomerCode.Text) ? null : aglCustomerCode.Value.ToString();
                _Entity.TargetDeliveryDate = dtpTargetDate.Text;
                if (txtStatus.Text == "NEW")
                {
                    _Entity.Status = "N";
                }
                else if (txtStatus.Text == "PARTIALLY SERVED")
                {
                    _Entity.Status = "P";
                }
                else if (txtStatus.Text == "CLOSED")
                {
                    _Entity.Status = "C";
                }
                else if (txtStatus.Text == "FORCED CLOSED")
                {
                    _Entity.Status = "X";
                }
                else if (txtStatus.Text == "CANCELLED")
                {
                    _Entity.Status = "L";
                }
                else if (txtStatus.Text == "PARTIAL CLOSED")
                {
                    _Entity.Status = "A";
                }
                else
                {
                    _Entity.Status = "N";
                }
                _Entity.CustomerPONo = String.IsNullOrEmpty(txtCustomerPONo.Text) ? null : txtCustomerPONo.Text;
                _Entity.QuotationNo = String.IsNullOrEmpty(aglQuote.Text) ? null : aglQuote.Value.ToString();
                if (aglProcurementDoc.Text == "No Procurement")
                {
                    _Entity.ProcurementDoc = "A";
                }
                else if (aglProcurementDoc.Text == "Purchase Request")
                {
                    _Entity.ProcurementDoc = "PR";
                }
                else if (aglProcurementDoc.Text == "Job Order")
                {
                    _Entity.ProcurementDoc = "JO";
                }
                else
                {
                    _Entity.ProcurementDoc = "A";
                }
                _Entity.Vatable = Convert.ToBoolean(chkVatable.Value.ToString());
                _Entity.IsWithQuote = Convert.ToBoolean(chkIsWithQuote.Value.ToString());
                _Entity.ExchangeRate = String.IsNullOrEmpty(speExchangeRate.Text) ? 1 : Convert.ToDecimal(speExchangeRate.Value.ToString());
                _Entity.PesoAmount = String.IsNullOrEmpty(txtPesoAmount.Text) ? 0 : Convert.ToDecimal(txtPesoAmount.Value.ToString());
                _Entity.Currency = String.IsNullOrEmpty(aglCurrency.Text) ? null : aglCurrency.Value.ToString();
                _Entity.ForeignAmount = String.IsNullOrEmpty(txtForeignAmount.Text) ? 0 : Convert.ToDecimal(txtForeignAmount.Value.ToString());
                _Entity.Terms = String.IsNullOrEmpty(speTerms.Text) ? 0 : Convert.ToInt32(speTerms.Text);
                _Entity.GrossVatableAmount = String.IsNullOrEmpty(txtGross.Text) ? 0 : Convert.ToDecimal(txtGross.Value.ToString());
                _Entity.TotalQty = String.IsNullOrEmpty(memTotalQty.Text) ? null : memTotalQty.Text;
                _Entity.NonVatableAmount = String.IsNullOrEmpty(txtNonVatable.Text) ? 0 : Convert.ToDecimal(txtNonVatable.Value.ToString());
                //_Entity.TotalFreight = String.IsNullOrEmpty(txtTotalFreight.Text) ? 0 : Convert.ToDecimal(txtTotalFreight.Value.ToString());
                _Entity.VATAmount = String.IsNullOrEmpty(txtVatAmount.Text) ? 0 : Convert.ToDecimal(txtVatAmount.Value.ToString());
                _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
                _Entity.TotalBulkQty = String.IsNullOrEmpty(txtTotalBulkQty.Text) ? 0 : Convert.ToDecimal(txtTotalBulkQty.Value.ToString());
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
                _Entity.Connection = Session["ConnString"].ToString();

                DataTable dt = new DataTable(); 
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

                //START ADDED NEW
                string array = "";
                if (e.Parameter.Contains('|'))
                {
                    array = e.Parameter.Split('|')[0];
                }
                else
                {
                    array = e.Parameter.ToString();
                }
                //END ADDED NEW

            //switch (e.Parameter)
            switch (array)
            {
                case "Add": 
                    //if (Session["Datatable"] != "1")
                    if (Session["SODatatable"] != "1")
                    {
                        gv1.UpdateEdit();
                    }

                    string strError = Functions.Submitted(_Entity.DocNumber,"Sales.SalesOrder",1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity); 
                        //if (Session["Datatable"] == "1")
                        if (Session["SODatatable"] == "1")
                        {
                            gv1.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode";
                            _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString()); 
                            gv1.DataSource = dt;
                            if (gv1.DataSourceID != null)
                            {
                                gv1.DataSourceID = null;
                            }
                            gv1.DataBind(); 
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv1.UpdateEdit();
                        }
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.SalesOrderDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        _Entity.SubstituteInfo(txtDocNumber.Text, aglCustomerCode.Text, Session["ConnString"].ToString());
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
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

                case "Update": 
                    //if (Session["Datatable"] != "1")
                    if (Session["SODatatable"] != "1")
                    {
                        gv1.UpdateEdit();
                    }

                    string strError1 = Functions.Submitted(_Entity.DocNumber,"Sales.SalesOrder",1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);
                        //if (Session["Datatable"] == "1")
                        if (Session["SODatatable"] == "1")
                        { 
                            gv1.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode";
                            _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                            gv1.DataSource = dt;
                            if (gv1.DataSourceID != null)
                            {
                                gv1.DataSourceID = null;
                            }
                            gv1.DataBind(); 
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            //gv1.UpdateEdit();

                            //START ADDED NEW
                            gv1.DataBind();
                            gv1.UpdateEdit();
                            //END ADDED NEW
                        }
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.SalesOrderDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail"); 
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        _Entity.SubstituteInfo(txtDocNumber.Text, aglCustomerCode.Text, Session["ConnString"].ToString());
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
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

                case "AddZeroDetail":
                    gv1.UpdateEdit();

                    string strError2 = Functions.Submitted(_Entity.DocNumber,"Sales.SalesOrder",1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.SalesOrderDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
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

                case "UpdateZeroDetail":

                    gv1.UpdateEdit();

                    string strError3 = Functions.Submitted(_Entity.DocNumber,"Sales.SalesOrder",1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Sales.SalesOrderDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
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

                case "iswithquotetrue":

                    sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                    //if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
                    //{
                    //    Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                    //                + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                    //                + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                    //    sdsQuotation.SelectCommand = Session["QuoteFilter"].ToString();
                    //    aglQuote.DataBind();
                    //}
                    //else
                    //{
                    //    Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                    //                + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                    //    sdsQuotation.SelectCommand = Session["QuoteFilter"].ToString();
                    //    aglQuote.DataBind();
                    //}
                    //aglQuote.Text = "";
                    //Session["referencequote"] = "1";
                    //Session["Datatable"] = "0";

                    //START ADDED CODE
                    if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
                    { 
                        Session["SOQuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                                    + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                        sdsQuotation.SelectCommand = Session["SOQuoteFilter"].ToString();
                        aglQuote.DataBind();
                    }
                    else
                    { 
                        Session["SOQuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                        sdsQuotation.SelectCommand = Session["SOQuoteFilter"].ToString();
                        aglQuote.DataBind();
                    }
                    aglQuote.Text = "";
                    Session["SOreferencequote"] = "1";
                    Session["SODatatable"] = "0";
                    //END ADDED CODE


                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    cp.JSProperties["cp_iswithdr"] = "1";

                    break;

                case "iswithquotefalse":

                    //Session["referencequote"] = "0";                   
                    //Session["Datatable"] = "0";
                    Session["referencequote"] = "0";                   
                    Session["Datatable"] = "0";


                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    cp.JSProperties["cp_iswithdr"] = "0";
                    aglQuote.Text = "";
                    break;

                case "FilterQuote":

                    //if (Session["referencequote"] == "1")
                    if (Session["SOreferencequote"] == "1")
                    {
                        //Session["Datatable"] = "0";
                        Session["SODatatable"] = "0";

                        gv1.DataSource = sdsDetail;
                        if (gv1.DataSourceID != "")
                        {
                            gv1.DataSourceID = null;
                        }
                        gv1.DataBind();
                        cp.JSProperties["cp_generated"] = true;
                    }
                    else
                    {
                        cp.JSProperties["cp_calculateonly"] = true;
                    }

                    //Session["QuoteFilter"] = Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                    Session["SOQuoteFilter"] = Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                                    + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                    
                    sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                    
                    //sdsQuotation.SelectCommand = Session["QuoteFilter"].ToString();
                    sdsQuotation.SelectCommand = Session["SOQuoteFilter"].ToString();
                    
                    aglQuote.DataBind();

                    if (!String.IsNullOrEmpty(aglCustomerCode.Value.ToString()))
                    {
                        DataTable info = Gears.RetriveData2("SELECT DISTINCT Currency, ISNULL(ARTerm,0) AS ARTerm FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + aglCustomerCode.Value.ToString() + "'", Session["ConnString"].ToString());
                        if (info.Rows.Count != 0)
                        {
                            aglCurrency.Text = info.Rows[0]["Currency"].ToString();
                            speTerms.Text = info.Rows[0]["ARTerm"].ToString();
                        }
                    }

                    //START ADDED NEW
                    Session["CustomerCode"] = aglCustomerCode.Text;
                    //END ADDED NEW

                    GetVat();
                    aglQuote.Text = "";
                    dtpTargetDate.Focus();
                    break;

                case "QuoteDetails":
                    GetSelectedVal();
                    if (!String.IsNullOrEmpty(aglCustomerCode.Value.ToString()))
                    {
                        DataTable info = Gears.RetriveData2("SELECT Currency, ARTerm FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + aglCustomerCode.Value.ToString() + "'", Session["ConnString"].ToString());
                        if (info.Rows.Count != 0)
                        {
                            aglCurrency.Text = info.Rows[0]["Currency"].ToString();
                            speTerms.Text = info.Rows[0]["ARTerm"].ToString();
                        }
                    }

                    if (!String.IsNullOrEmpty(aglQuote.Value.ToString()))
                    {
                        DataTable target = Gears.RetriveData2("SELECT (CASE WHEN ISNULL(TargetDeliveryDate,'')='' THEN GETDATE() ELSE TargetDeliveryDate END) AS TargetDeliveryDate, ISNULL(Freight,0) AS Freight FROM Sales.Quotation WHERE DocNumber ='" + aglQuote.Value.ToString() + "'", Session["ConnString"].ToString());
                        if (target.Rows.Count != 0)
                        {
                            dtpTargetDate.Text = Convert.ToDateTime(target.Rows[0]["TargetDeliveryDate"].ToString()).ToShortDateString();
                        }
                    }

                    GetVat();

                    //Session["QuoteFilter"] = Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                    Session["SOQuoteFilter"] = Session["QuoteFilter"] = "SELECT A.DocNumber, DocDate, CustomerCode, Status, SUM(ISNULL(B.Qty,0)) AS TotalQty, Validity, TargetDeliveryDate, Remarks FROM Sales.Quotation A "
                                    + " LEFT JOIN Sales.QuotationDetail B ON A.DocNumber = B.DocNumber WHERE ISNULL([SubmittedBy], '') != '' AND CustomerCode = '" + aglCustomerCode.Text + "' AND Status NOT IN ('X','C','L')"
                                    + " GROUP BY A.DocNumber, DocDate, CustomerCode, Status, Validity, TargetDeliveryDate, Remarks";
                    
                    sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                    
                    //sdsQuotation.SelectCommand = Session["QuoteFilter"].ToString();
                    sdsQuotation.SelectCommand = Session["SOQuoteFilter"].ToString();
                    
                    aglQuote.DataBind();


                    cp.JSProperties["cp_generated"] = true;

                    break;

                case "unitcost":
                    GetVat();
                    cp.JSProperties["cp_unitcost"] = true;
                    break;

                case "CallbackDataSourceCheck":
                    DataTable dtbldetail1 = Gears.RetriveData2("SELECT DocNumber FROM Sales.SalesOrderDetail Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                    if (dtbldetail1.Rows.Count > 0)
                    {
                        gv1.DataSourceID = odsDetail.ID; 
                        gv1.DataBind();
                    }
                    else
                    {
                        gv1.DataSourceID = sdsDetail.ID; 
                        gv1.DataBind();
                    }
                    break;

                case "CallbackSize":

                    cp.JSProperties["cp_closeSH"] = true;

                    Session["SOitem"] = null;
                    Session["SOclr"] = null;
                    Session["SOcls"] = null;
                    Session["SOqty"] = null;
                    Session["SOprice"] = null;
                    Session["SOunit"] = null;
                    Session["SOdesc"] = null;
                    Session["SObulk"] = null;
                    Session["SOisbulk"] = null;
                    break;

                case "CallbackSizeHorizontal":

                    Session["SOitem"] = e.Parameter.Split('|')[1];
                    Session["SOclr"] = e.Parameter.Split('|')[2];
                    Session["SOcls"] = e.Parameter.Split('|')[3];
                    Session["SOqty"] = e.Parameter.Split('|')[4];
                    Session["SOprice"] = e.Parameter.Split('|')[5];
                    Session["SOunit"] = e.Parameter.Split('|')[6];
                    Session["SOdesc"] = e.Parameter.Split('|')[7];
                    Session["SObulk"] = e.Parameter.Split('|')[8];
                    Session["SOisbulk"] = e.Parameter.Split('|')[9];

                    sdsSizeHorizontal.ConnectionString = Session["ConnString"].ToString();
                    sdsSizeHorizontal.SelectCommand = "SELECT CONVERT(varchar(MAX),'" + txtDocNumber.Text + "') AS DocNumberX, '" + Session["SOitem"].ToString() + "' AS ItemCodeX, '" + Session["SOclr"].ToString() + "' AS ColorCodeX, '" + Session["SOcls"].ToString() + "' AS ClassCodeX, "
                        + " CONVERT(varchar(MAX),'" + Session["SOunit"].ToString() + "') AS UnitX, CONVERT(decimal(15,2), " + Session["SOqty"].ToString() + ") AS OrderQtyX, CONVERT(decimal(15,2)," + Session["SOprice"].ToString() + ") AS UnitPriceX, CONVERT(varchar(MAX),'" + Session["SOdesc"] + "') AS FullDescX, "
                        + " CONVERT(varchar(MAX),'" + Session["SObulk"].ToString() + "') AS BulkUnitX, CONVERT(bit,'" + Session["SOisbulk"].ToString() + "') AS IsByBulkX";

                    gvSizeHorizontal.DataSourceID = "sdsSizeHorizontal";
                    if (gvSizeHorizontal.DataSource != null)
                    {
                        gvSizeHorizontal.DataSource = null;
                    }
                    gvSizeHorizontal.DataBind();


                    sdsSizes.ConnectionString = Session["ConnString"].ToString();
                    gvSizes.DataSourceID = "sdsSizes";
                    if (gvSizes.DataSource != null)
                    {
                        gvSizes.DataSource = null;
                    }
                    gvSizes.DataBind();

                    gvSizes.AutoGenerateColumns = false;

                    //DataTable getSizeCode = Gears.RetriveData2("SELECT A.SizeCode FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE ISNULL(A.IsInactive,0) = 0 AND ItemCode = '" + item +
                    //    "' AND ColorCode = '" + clr + "' AND ClassCode = '" + cls + "' ORDER BY SizeType, SortOrder", Session["ConnString"].ToString());
                    DataTable getSizeCode = Gears.RetriveData2("SELECT A.SizeCode FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE ISNULL(A.IsInactive,0) = 0 AND ItemCode = '" + Session["SOitem"].ToString() +
                        "' AND ColorCode = '" + Session["SOclr"].ToString() + "' AND ClassCode = '" + Session["SOcls"].ToString() + "' ORDER BY SizeType, SortOrder", Session["ConnString"].ToString());


                    string query = "";

                    int x = 1;

                    foreach (DataRow dtsize in getSizeCode.Rows)
                    {
                        GridViewDataSpinEditColumn spin = new GridViewDataSpinEditColumn();
                        spin.FieldName = dtsize[0].ToString();
                        spin.VisibleIndex = x;
                        spin.Width = 50;
                        spin.ReadOnly = false;
                        spin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                        spin.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                        spin.PropertiesSpinEdit.DisplayFormatString = "{0:N}";
                        spin.PropertiesSpinEdit.ClientSideEvents.ValueChanged = "CalculateSize";
                        query += ", CONVERT(decimal(15,2),0) AS [" + dtsize[0].ToString() + "]";
                        gvSizes.Columns.Add(spin);
                        x++; 
                    }
                    gvSizes.CancelEdit();

                    sdsSizes.SelectCommand = "SELECT CONVERT(varchar(MAX),'" + txtDocNumber.Text + "') AS DocNumberX" + query;
                    sdsSizes.DataBind();
                    gvSizes.DataSourceID = "sdsSizes";
                    if (gvSizes.DataSource != null)
                    {
                        gvSizes.DataSource = null;
                    }
                    gvSizes.DataBind();

                    cp.JSProperties["cp_RefreshSizeGrid"] = true;

                    break;
                case "TargetDelivery":
                    Session["TargetDate"] = dtpTargetDate.Text;
                    break;
                case "DocDate":
                    Session["DocDate"] = dtpDocDate.Text;
                    break;
            }
        }
        protected void GetVat()
        {

            DataTable getvat = Gears.RetriveData2("select Rate from Masterfile.BPCustomerInfo A inner join Masterfile.Tax B on A.TaxCode = B.TCode where BizPartnerCode = '" + aglCustomerCode.Text + "'", Session["ConnString"].ToString());

            if (getvat.Rows.Count > 0)
            {
                cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
            }
            else
            {
                cp.JSProperties["cp_vatrate"] = "0";
            }
        
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
           
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


            if (Session["SOHorizontal"] == "1")
            {
                DataTable newsource = new DataTable();

                foreach (GridViewColumn col in gv1.VisibleColumns)
                {
                    GridViewDataColumn dataColumn = col as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    newsource.Columns.Add(dataColumn.FieldName);
                }

                for (int i = 0; i < gv1.VisibleRowCount; i++)
                {
                    DataRow row = newsource.Rows.Add();
                    foreach (DataColumn col in newsource.Columns)
                        row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
                }

                foreach (DataRow dtRow in newsource.Rows)
                {
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.OrderQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OrderQty"]) ? 0 : dtRow["OrderQty"]);
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.BulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BulkQty"]) ? 0 : dtRow["BulkQty"]);
                    _EntityDetail.BulkUnit = dtRow["BulkUnit"].ToString();
                    _EntityDetail.UnitPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitPrice"]) ? 0 : dtRow["UnitPrice"]);
                    _EntityDetail.IsVAT = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVAT"].ToString()) ? false : dtRow["IsVAT"]);
                    _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                    _EntityDetail.DiscountRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["DiscountRate"]) ? 0 : dtRow["DiscountRate"]);
                    _EntityDetail.DeliveredQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["DeliveredQty"]) ? 0 : dtRow["DeliveredQty"]);
                    _EntityDetail.SubstituteItem = dtRow["SubstituteItem"].ToString();
                    _EntityDetail.SubstituteColor = dtRow["SubstituteColor"].ToString();
                    _EntityDetail.SubstituteClass = dtRow["SubstituteClass"].ToString();
                    _EntityDetail.BaseQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BaseQty"]) ? 0 : dtRow["BaseQty"]);
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
                    _EntityDetail.UnitFactor = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFactor"]) ? 0 : dtRow["UnitFactor"]);
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.Version = "1";
                    _EntityDetail.Rate = Convert.ToDecimal(Convert.IsDBNull(dtRow["Rate"]) ? 0 : dtRow["Rate"]);
                    _EntityDetail.IsByBulk = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsByBulk"].ToString()) ? false : dtRow["IsByBulk"]);
                    _EntityDetail.AddSalesOrderDetail(_EntityDetail);
                }
            }

            //if (Session["Datatable"] == "1" && check == true)
            if (Session["SODatatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumber"], values.Keys["ItemCode"], values.Keys["ColorCode"], values.Keys["ClassCode"], values.Keys["SizeCode"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    var DocNumber = txtDocNumber.Text;
                    var LineNumber = values.NewValues["LineNumber"] == null ? "00001" : values.NewValues["LineNumber"];
                    var ItemCode = values.NewValues["ItemCode"];
                    var FullDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var OrderQty = values.NewValues["OrderQty"];
                    var Unit = values.NewValues["Unit"];
                    var BulkQty = values.NewValues["BulkQty"];
                    var BulkUnit = values.NewValues["BulkUnit"];
                    var UnitPrice = values.NewValues["UnitPrice"];
                    //var UnitFreight = values.NewValues["UnitFreight"];
                    var IsVAT = values.NewValues["IsVAT"];
                    var VATCode = values.NewValues["VATCode"];
                    var DiscountRate = values.NewValues["DiscountRate"];
                    var DeliveredQty = values.NewValues["DeliveredQty"];
                    var SubstituteItem = values.NewValues["SubstituteItem"];
                    var SubstituteColor = values.NewValues["SubstituteColor"];
                    var SubstituteClass = values.NewValues["SubstituteClass"];
                    var BaseQty = values.NewValues["BaseQty"];
                    var StatusCode = values.NewValues["StatusCode"];
                    var BarcodeNo = values.NewValues["BarcodeNo"];
                    var UnitFactor = values.NewValues["UnitFactor"];
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
                    var Rate = values.NewValues["Rate"];
                    var IsByBulk = values.NewValues["IsByBulk"];
                    //source.Rows.Add(LineNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, OrderQty, Unit, BulkQty, BulkUnit, UnitPrice, UnitFreight, IsVAT, VATCode, DiscountRate, DeliveredQty, SubstituteItem, SubstituteColor, SubstituteClass, BaseQty, StatusCode, BarcodeNo, UnitFactor, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Version, Rate, IsByBulk);
                    source.Rows.Add(DocNumber, LineNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, OrderQty, Unit, BulkQty, BulkUnit, UnitPrice, IsVAT, VATCode, DiscountRate, DeliveredQty, SubstituteItem, SubstituteColor, SubstituteClass, BaseQty, StatusCode, BarcodeNo, UnitFactor, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Version, Rate, IsByBulk);
               
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"], values.NewValues["ItemCode"], values.NewValues["ColorCode"], values.NewValues["ClassCode"], values.NewValues["SizeCode"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["OrderQty"] = values.NewValues["OrderQty"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["BulkQty"] = values.NewValues["BulkQty"];
                    row["BulkUnit"] = values.NewValues["BulkUnit"];
                    row["UnitPrice"] = values.NewValues["UnitPrice"];
                    //row["UnitFreight"] = values.NewValues["UnitFreight"];
                    row["IsVAT"] = values.NewValues["IsVAT"];
                    row["VATCode"] = values.NewValues["VATCode"];
                    row["DiscountRate"] = values.NewValues["DiscountRate"];
                    row["DeliveredQty"] = values.NewValues["DeliveredQty"];
                    row["SubstituteItem"] = values.NewValues["SubstituteItem"];
                    row["SubstituteColor"] = values.NewValues["SubstituteColor"];
                    row["SubstituteClass"] = values.NewValues["SubstituteClass"];
                    row["BaseQty"] = values.NewValues["BaseQty"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
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
                    row["Version"] = values.NewValues["Version"];
                    row["Rate"] = values.NewValues["Rate"];
                    row["IsByBulk"] = values.NewValues["IsByBulk"];
                }
                    
                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.OrderQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OrderQty"]) ? 0 : dtRow["OrderQty"]);
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.BulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BulkQty"]) ? 0 : dtRow["BulkQty"]);
                    _EntityDetail.BulkUnit = dtRow["BulkUnit"].ToString();
                    _EntityDetail.UnitPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitPrice"]) ? 0 : dtRow["UnitPrice"]);
                    //_EntityDetail.UnitFreight = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFreight"]) ? 0 : dtRow["UnitFreight"]);
                    _EntityDetail.IsVAT = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVAT"].ToString()) ? false : dtRow["IsVAT"]);
                    _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                    _EntityDetail.DiscountRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["DiscountRate"]) ? 0 : dtRow["DiscountRate"]);
                    _EntityDetail.DeliveredQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["DeliveredQty"]) ? 0 : dtRow["DeliveredQty"]);
                    _EntityDetail.SubstituteItem = dtRow["SubstituteItem"].ToString();
                    _EntityDetail.SubstituteColor = dtRow["SubstituteColor"].ToString();
                    _EntityDetail.SubstituteClass = dtRow["SubstituteClass"].ToString();
                    _EntityDetail.BaseQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BaseQty"]) ? 0 : dtRow["BaseQty"]);
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
                    _EntityDetail.UnitFactor = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFactor"]) ? 0 : dtRow["UnitFactor"]);
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.Version = "1";
                    _EntityDetail.Rate = Convert.ToDecimal(Convert.IsDBNull(dtRow["Rate"]) ? 0 : dtRow["Rate"]);
                    _EntityDetail.IsByBulk = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsByBulk"].ToString()) ? false : dtRow["IsByBulk"]);
                    _EntityDetail.AddSalesOrderDetail(_EntityDetail);
                }
            }            
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["sodetail"] = null;
            //} 
            //if (Session["sodetail"] != null)
            //{
            //    gv1.DataSourceID = sdsQuotationDetail.ID;
            //    sdsQuotationDetail.FilterExpression = Session["sodetail"].ToString();
            //}

            ASPxGridView grid = sender as ASPxGridView;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains(grid.ID)))
            {
                if (Session["sodetail"] != null)
                {
                    gv1.DataSourceID = sdsQuotationDetail.ID;
                    sdsQuotationDetail.FilterExpression = Session["sodetail"].ToString();
                }
            }
        }

        private DataTable GetSelectedVal()
        {
            //Session["Datatable"] = "0";
            Session["SODatatable"] = "0";

            gv1.DataSource = sdsDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            DataTable dt = new DataTable();
            string[] selectedValues = aglQuote.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(aglQuote.KeyFieldName, selectedValues);
            sdsQuotationDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["sodetail"] = sdsQuotationDetail.FilterExpression;
            gv1.DataSource = sdsQuotationDetail;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();

            //Session["Datatable"] = "1";
            Session["SODatatable"] = "1";

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
            dt.Columns["LineNumber"], dt.Columns["ItemCode"], dt.Columns["ColorCode"], dt.Columns["ClassCode"], dt.Columns["SizeCode"]};

            return dt;
        }    
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }
        protected void dtpTargetDelivery_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpTargetDate.Date = DateTime.Now;
            }
            else if (Request.QueryString["entry"] == "E")
            {
                if (String.IsNullOrEmpty(dtpTargetDate.Text))
                {
                    dtpTargetDate.Date = DateTime.Now;
                }
                else
                {
                    dtpTargetDate.Text = Convert.ToDateTime(_Entity.TargetDeliveryDate.ToString()).ToShortDateString(); ;
                }
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString(); 
        }

        protected void aglQuote_Init(object sender, EventArgs e)
        {
            //if (Session["QuoteFilter"] != null)
            //{
            //    sdsQuotation.ConnectionString = Session["ConnString"].ToString();
            //    sdsQuotation.SelectCommand = Session["QuoteFilter"].ToString();
            //    aglQuote.DataBind();
            //}
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
            {
                if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                {
                    if (Session["SOQuoteFilter"] != null)
                    {
                        sdsQuotation.ConnectionString = Session["ConnString"].ToString();
                        sdsQuotation.SelectCommand = Session["SOQuoteFilter"].ToString();
                        aglQuote.DataBind();
                    }
                }
            }
        }
        
        protected void SessionRef()
        {
            //if (Session["referencequote"] == null)
            //{
            //    if (chkIsWithQuote.Checked == true)
            //    {
            //        Session["referencequote"] = "1";
            //    }
            //    else
            //    {
            //        Session["referencequote"] = "0";
            //    }
            //}
            if (Session["SOreferencequote"] == null)
            {
                if (chkIsWithQuote.Checked == true)
                {
                    Session["SOreferencequote"] = "1";
                }
                else
                {
                    Session["SOreferencequote"] = "0";
                }
            }
        }

        //START ADDED NEW
        protected void InitControls()
        {
            foreach (var item in frmlayout1.Items)
            {
                if (item is LayoutGroupBase)
                    (item as LayoutGroupBase).ForEach(GetNestedControls);
            }
        }
        protected void GetNestedControls(LayoutItemBase item)
        {
            if (item is LayoutItem)
                SetViewState(item as LayoutItem);

        }
        protected void SetViewState(LayoutItem c)
        {
            foreach (Control control in c.Controls)
            {
                ASPxEdit editor = control as ASPxEdit;
                if (editor != null)
                {
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxTextBox")
                    {
                        TextboxLoad(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup")
                    {
                        LookupLoad(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxDateEdit")
                    {
                        Date_Load(editor);
                    } 
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxCheckBox")
                    {
                        CheckBoxLoad(editor);
                    }
                }
            }
        }
        //END ADDED NEW

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                 
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
                string itemcode1 = param.Split(';')[0];
                if (string.IsNullOrEmpty(itemcode)) return;
                //if(changes == "1")
                //{
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable countitem = Gears.RetriveData2("SELECT DISTINCT B.ColorCode,B.ClassCode,B.SizeCode, ISNULL(UnitBase,'') AS UnitBase, FullDesc, ISNULL(UnitBulk,'') AS UnitBulk, ISNULL(A.IsByBulk,0) IsByBulk, ISNULL(StatusCode,'') AS StatusCode, "
                     + " ISNULL(A.UpdatedPrice,0) AS UnitPrice, 'NONV' AS VATCode, 0 AS IsVAT, ISNULL(SubstitutedItem,'') SubstitutedItem, ISNULL(SubstitutedClass,'') SubstitutedClass, ISNULL(SubstitutedColor,'') SubstitutedColor, ISNULL(SubstitutedSize,'') SubstitutedSize "
                     + " ,case when '" + Session["DocDate"].ToString() + "' BETWEEN DateFrom AND DateTo THEN 'YES' ELSE 'NO' END as KeyField,ISNULL(C.Price,0)- case when '" + Session["DocDate"].ToString() + "' BETWEEN DateFrom AND DateTo THEN   ( (CASE WHEN ISNULL(IsApplicable,0)=1 THEN DiscountRate ELSE 0 END /100 )  * ISNULL(C.Price,0) )  ELSE 0 END as Price, CASE WHEN ISNULL(IsApplicable,0)=1 THEN DiscountRate ELSE 0 END as DiscountRate "
                     + " FROM Masterfile.Item A LEFT JOIN Masterfile.ItemDetail B on A.ItemCode = B.ItemCode "
                     + " LEFT JOIN Masterfile.ItemCustomerPrice C ON B.ItemCode = C.ItemCode AND B.ColorCode = C.ColorCode AND  B.ClassCode = C.ClassCode AND B.SizeCode = C.SizeCode AND C.Customer = '" + Session["CustomerCode"].ToString() + "' "
                     + " WHERE A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());


                DataTable desc = Gears.RetriveData2("SELECT DISTINCT FullDesc, ISNULL(IsByBulk,0) AS IsByBulk, UnitBase, UnitBulk, ISNULL(UpdatedPrice,0) AS UnitPrice, 'NONV' AS VATCode, 0 AS IsVAT "
                                                  + " FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;


                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                DataTable getStatus = Gears.RetriveData2("select StatusCode,Price,row_number() over (partition BY A.StockNumber,StatusCode " +
                                                       " ORDER BY EffectivityDate DESC) rn ,EffectivityDate " +
                                                       " into #SMPH from Retail.StockMasterPriceHistory A " +
                                                       " where ISNULL(EffectivityDate,'1990-01-01')<= '" + Session["TargetDAte"].ToString() + "' " +
                                                       " and StockNumber= '" + itemcode + "' " +
                                                       " SELECT top 1 StatusCode,Price  FROM #SMPH " +
                                                       " where rn='1' " +
                                                        " ORDER BY EffectivityDate DESC " +
                                                       " DROP TABLE #SMPH ", Session["ConnString"].ToString());//ADD CONN


                DataTable dtItem = Gears.RetriveData2("SELECT BrandCode,GenderCode,ProductCategoryCode,ProductSubCatCode from Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                DataTable getPriceDiscount1st = new DataTable();
                if (dtItem.Rows.Count > 0 && getStatus.Rows.Count > 0)
                {
                    getPriceDiscount1st = Gears.RetriveData2("SELECT UnitPrice,DiscountRate FROM Masterfile.BPCustomerCommission where CustomerCode = '" + Session["CustomerCode"].ToString() + "' "
                                                              + " and (Brand = '" + dtItem.Rows[0]["BrandCode"].ToString() + "' OR ISNULL(Brand,'')='') "
                                                              + " and (GenderCode = '" + dtItem.Rows[0]["GenderCode"].ToString() + "' OR ISNULL(GenderCode,'')='') "
                                                              + " and (ProductCategoryCode = '" + dtItem.Rows[0]["ProductCategoryCode"].ToString() + "' OR ISNULL(ProductCategoryCode,'')='') "
                                                              + " and (ProductSubCategoryCode = '" + dtItem.Rows[0]["ProductSubCatCode"].ToString() + "' OR ISNULL(ProductSubCategoryCode,'')='') "
                                                              + " and (ProductStatus = '" + getStatus.Rows[0]["StatusCode"].ToString() + "' OR ISNULL(ProductStatus,'')='') ", Session["ConnString"].ToString());//ADD CONN
                }

                cntsze = countsize.Rows.Count;
                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += dt["UnitBulk"].ToString() + ";";
                        codes += dt["IsByBulk"].ToString() + ";";

                        if (getStatus.Rows.Count == 0)
                        {
                            codes += "" + ";";
                        }
                        else
                        {
                            if (getStatus.Rows.Count > 0)
                            {


                                codes += getStatus.Rows[0]["StatusCode"].ToString() + ";";
                            }
                        }

                        if (countitem.Rows.Count > 0)
                        {
                            if (countitem.Rows[0]["KeyField"].ToString() == "YES")
                            {
                                codes += dt["Price"].ToString() + ";";
                                codes += dt["DiscountRate"].ToString() + ";";
                            }
                            else
                            {
                                codes += dt["Price"].ToString() + ";";
                                codes += "0" + ";";
                            }
                        }
                        else
                        {

                            if (getPriceDiscount1st.Rows.Count > 0)
                            {
                                codes += getPriceDiscount1st.Rows[0]["UnitPrice"].ToString() + ";";
                                codes += getPriceDiscount1st.Rows[0]["DiscountRate"].ToString() + ";";
                            }
                            else
                            {

                                if (getStatus.Rows.Count > 0)
                                {
                                    codes += getStatus.Rows[0]["Price"].ToString() + ";";
                                    codes += "0" + ";";
                                }
                                else
                                {
                                    codes += dt["UnitPrice"].ToString() + ";";
                                    codes += "0" + ";";
                                }

                            }
                        }



                        codes += dt["VATCode"].ToString() + ";";
                        codes += dt["IsVAT"].ToString() + ";";
                        codes += dt["SubstitutedItem"].ToString() + ";";
                        codes += dt["SubstitutedColor"].ToString() + ";";
                        codes += dt["SubstitutedClass"].ToString() + ";";
                        codes += dt["SubstitutedSize"].ToString() + ";";
                    }
                }
                else
                {
                    if (cntclr == 1)
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                    else
                        codes = ";";
                    if (cntcls == 1)
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                    else
                        codes += ";";
                    if (cntsze == 1)
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                    else
                        codes += ";";
                    codes += desc.Rows[0]["UnitBase"].ToString() + ";";
                    codes += desc.Rows[0]["FullDesc"].ToString() + ";";
                    codes += desc.Rows[0]["UnitBulk"].ToString() + ";";
                    codes += desc.Rows[0]["IsByBulk"].ToString() + ";";
                    if (getStatus.Rows.Count == 0)
                    {
                        codes += "" + ";";
                    }
                    else
                    {
                        if (getStatus.Rows.Count > 0)
                        {
                            codes += getStatus.Rows[0]["StatusCode"].ToString() + ";";
                        }
                    }


                    if (getPriceDiscount1st.Rows.Count > 0)
                    {
                        codes += getPriceDiscount1st.Rows[0]["UnitPrice"].ToString() + ";";
                        codes += getPriceDiscount1st.Rows[0]["DiscountRate"].ToString() + ";";
                    }
                    else
                    {
                        if (getStatus.Rows.Count > 0)
                        {
                            codes += getStatus.Rows[0]["Price"].ToString() + ";";
                            codes += "0" + ";";
                        }
                        else
                        {
                            codes += desc.Rows[0]["UnitPrice"].ToString() + ";";
                            codes += "0" + ";";
                        }

                    }
                    codes += desc.Rows[0]["VATCode"].ToString() + ";";
                    codes += desc.Rows[0]["IsVAT"].ToString() + ";;;;;";

                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "ItemCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true; 
            }

        }
         
        public bool LookupValRemover(object sender, ASPxGridViewCustomCallbackEventArgs e, bool IsCallback, string CallbackID, string CallbackParam)
        {
            var itemlookup = sender as ASPxGridView;

            string param = CallbackParam.ToString().Substring(CallbackParam.ToString().LastIndexOf('|') + 1);
            //string changes = param.Split(';')[0];

            string itemcode = param.Split(';')[1];
            if (string.IsNullOrEmpty(itemcode))
            {
                itemlookup.JSProperties["cp_identifier"] = "ItemCode";
                itemlookup.JSProperties["cp_codes"] = ";;;;;;;0;;false;";
                itemlookup.JSProperties["cp_valch"] = true;
                return false;
            }
            else
            {
                for (int i = 0; i < itemlookup.VisibleRowCount; i++)
                {
                    if (itemlookup.GetRowValues(i, "ItemCode") != null)
                    {
                        if (itemlookup.GetRowValues(i, "ItemCode").ToString() == itemcode)
                        {
                            return true;
                        }
                    }
                }
                return false;
            } 
        }
         
        protected void aglCustomerCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
                if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                {
                    sdsBizPartnerCus.ConnectionString = Session["ConnString"].ToString();
                    gridLookup.GridView.DataSourceID = "sdsBizPartnerCus";
                    sdsBizPartnerCus.SelectCommand = "SELECT DISTINCT BizPartnerCode, Name FROM Masterfile.BPCustomerInfo WHERE ISNULL(IsInActive,0)=0";
                    gridLookup.DataBind();
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