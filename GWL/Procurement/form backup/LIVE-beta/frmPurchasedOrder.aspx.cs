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
    public partial class frmPurchasedOrder : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        Boolean checkdel = false;

        private static string strError;

        Entity.PurchasedOrder _Entity = new PurchasedOrder();//Calls entity odsHeader
        Entity.PurchasedOrder.PurchasedOrderDetail _EntityDetail = new PurchasedOrder.PurchasedOrderDetail();//Call entity sdsDetail

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

            // Moved inside !IsPostBack   LGE 03 - 02 - 2016
            // V=View, E=Edit, N=New
            switch (Request.QueryString["entry"].ToString())
            {
                case "N":
                    //gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.KeyFieldName = "LineNumber";
                    break;
                case "E":
                    updateBtn.Text = "Update";
                    txtDocNumber.ReadOnly = true;
                    gv1.KeyFieldName = "LineNumber";
                    edit = true;
                    break;
                case "V":
                    view = true;//sets view mode for entry
                    updateBtn.Text = "Close";
                    glcheck.ClientVisible = false;
                    break;
                case "D":
                    view = true;
                    updateBtn.Text = "Delete";
                    break;
            }
            //--------------------------------------------------
            //--------------------------------------------------

            

            //sdsItemDetail.SelectCommand = "SELECT B.ItemCode, ColorCode, ClassCode,SizeCode,UnitBase AS Unit,FullDesc,LineNumber,TaxCode as VATCode, CASE WHEN TaxCode='V' THEN 1 ELSE 0 END IsVat FROM Masterfile.[Item] A IR JOIN Masterfile.[ItemDetail] B ON A.ItemCode = B.ItemCode inner JOIN Masterfile.BPSupplierInfo C on c.suppliercode = '" + glSupplierCode.Text + "' where isnull(A.IsInactive,0)=0";
            
            if (!IsPostBack)
            {
                // To Destroy Session Every Page Load
                Session["Datatable"] = null;
                Session["newsource"] = null;
                Session["iswithpr"] = null;
                Session["iswithprcheckstate"] = null;
                Session["Message"] = null;
                Session["DELETEDETAIL"] = null;
                Session["ColorCode"] = null;
                Session["ClassCode"] = null;
                Session["SizeCode"] = null;


                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN




                //Session["iswithprcheckstate"] = chkIsWithPR.Checked.ToString();
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    //gv1.DataSourceID = "sdsDetail";
                    popup.ShowOnPageLoad = false;
                    Generatebtn.ClientVisible = true;

                    if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                    {
                        updateBtn.Text = "Add";
                        HeaderForm.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        chkIsWithPR.ClientEnabled = true;
                        glPRNumber.ClientEnabled = false;
                        Generatebtn.ClientEnabled = false;
                    }
                    else
                    {
                        updateBtn.Text = "Update";

                        //--------------------------------------------------

                        //gv1.KeyFieldName = "DocNumber;LineNumber";
                        gv1.KeyFieldName = "LineNumber";
                        chkIsWithPR.ClientEnabled = false;
                        glPRNumber.ClientEnabled = false;
                        Generatebtn.ClientEnabled = false;

                        //--------------------------------------------------
                    
                    }
                }



                SupplierCodelookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.[BPSupplierInfo] WHERE ISNULL([IsInactive],0) = 0";
                glSupplierCode.Value = _Entity.SupplierCode.ToString();
                glReceivingWarehouse.Value = _Entity.ReceivingWarehouse.ToString();
                txtContactPerson.Value = _Entity.ContactPerson.ToString();
                glQuotationNumber.Value = _Entity.QuotationNo.ToString();
                PRNumberlookup.SelectCommand = "SELECT DocNumber FROM Procurement.[PurchaseRequest] WHERE ISNULL (ApprovedBy,'')!='' AND Status NOT IN ('C','X')";
                glPRNumber.Text = _Entity.PRNumberH.ToString();


                txtStatus.Value = _Entity.Status.ToString();
                glBroker.Value = _Entity.Broker.ToString();
                memoremarks.Value = _Entity.Remarks.ToString();

                //chkIsWithPR.Value = Convert.ToBoolean(_Entity.IsWithPR.ToString());
                chkIsWithPR.Value = Convert.ToBoolean(_Entity.IsWithPR);
                chkIsWithInvoice.Value = Convert.ToBoolean(_Entity.IsWithInvoice.ToString());
                chkIsReleased.Value = _Entity.IsReleased;
                chkIsPrinted.Value = _Entity.IsPrinted;
                
                dtDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                dtTargetDate.Text = String.IsNullOrEmpty(_Entity.TargetDeliveryDate.ToString()) ? null : Convert.ToDateTime(_Entity.TargetDeliveryDate.ToString()).ToShortDateString();
                dtDateCompleted.Text = String.IsNullOrEmpty(_Entity.DateCompleted.ToString()) ? null : Convert.ToDateTime(_Entity.DateCompleted.ToString()).ToShortDateString();
                dtCancellationDate.Text = String.IsNullOrEmpty(_Entity.CancellationDate.ToString()) ? null : Convert.ToDateTime(_Entity.CancellationDate.ToString()).ToShortDateString();
                dtCommitmentDate.Text = String.IsNullOrEmpty(_Entity.CommitmentDate.ToString()) ? null : Convert.ToDateTime(_Entity.CommitmentDate.ToString()).ToShortDateString();

                txtCurrency.Value = _Entity.Currency.ToString();
                spinTerms.Value = _Entity.Terms.ToString();
                spinTotalFreight.Value = _Entity.TotalFreight.ToString();
                spinExchangeRate.Value = _Entity.ExchangeRate.ToString();
                spinWithholdingTax.Value = _Entity.WithholdingTax.ToString();
                spinTotalQty.Value = _Entity.TotalQty.ToString();
                spinPesoAmount.Value = _Entity.PesoAmount.ToString();
                spinForeignAmount.Value = _Entity.ForeignAmount.ToString();
                spinVATAmount.Value = _Entity.VATAmount.ToString();
                spinGrossVATableAmount.Value = _Entity.GrossVATableAmount.ToString();
                spinNonVATableAmount.Value = _Entity.NonVATableAmount.ToString();


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
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtForceClosedBy.Text = _Entity.ForceClosedBy;
                txtForceClosedDate.Text = _Entity.ForceClosedDate;
                txtReleasedBy.Text = _Entity.ReleasedBy;
                txtReleasedDate.Text = _Entity.ReleasedDate;

                glPRNumber.Text = _Entity.PRNumberH.ToString();

                if (spinExchangeRate.Text == "0" ||
                        spinExchangeRate.Text == "0.00" ||
                        spinExchangeRate.Text == "0.0000" )
                {
                    spinExchangeRate.Text = "1.00";
                }

                if(chkIsWithPR.Checked.ToString() == "True")
                {

                    Session["iswithpr"] = "1";
                }
                else
                {
                    Session["iswithpr"] = "0";
                }

                SetStatus();
                // Generatebtn, chkIsWithPR and glPRNumber behavior when in VIEW MODE and DELETE MODE
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithpr"] == "1" || chkIsWithPR.Value.ToString() == "True"))
                    {
                        //gv1.KeyFieldName = "DocNumber;LineNumber";
                        gv1.KeyFieldName = "LineNumber";
                        cp.JSProperties["cp_iswithpr"] = "0";
                        chkIsWithPR.Enabled = false;
                        gv1.Columns["PRNumber"].Width = 100;
                        gvService.Columns["PRNumber"].Width = 100;
                    }
                    else if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithpr"] == "1" || chkIsWithPR.Value.ToString() == "True"))
                    {
                        //gv1.KeyFieldName = "LineNumber;PRNumber";
                        gv1.KeyFieldName = "LineNumber";
                        cp.JSProperties["cp_iswithpr"] = "0";
                        chkIsWithPR.ReadOnly = false;
                        gv1.Columns["PRNumber"].Width = 100;
                        gvService.Columns["PRNumber"].Width = 100;
                    }
                    else if (Request.QueryString["iswithdetail"].ToString() == "false" || Request.QueryString["iswithdetail"].ToString() == "0" && (Session["iswithpr"] == "0" || chkIsWithPR.Value.ToString() == "False"))
                    {
                        //gv1.KeyFieldName = "LineNumber;PRNumber";
                        gv1.KeyFieldName = "LineNumber";
                        cp.JSProperties["cp_iswithpr"] = "0";
                        chkIsWithPR.ReadOnly = false;
                    }
                    else if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithpr"] == "0" || chkIsWithPR.Value.ToString() == "False"))
                    {
                        //gv1.KeyFieldName = "DocNumber;LineNumber";
                        gv1.KeyFieldName = "LineNumber";
                        cp.JSProperties["cp_iswithpr"] = "0";
                        chkIsWithPR.Enabled = false;

                    }
                }

                //--------------------------------------------------
                // Generatebtn, chkIsWithPR and glPRNumber behavior: Prevent Auto Close
                //--------------------------------------------------
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    if (chkIsWithPR.Checked.ToString() == "True")
                    {

                        gv1.Columns["PRNumber"].Width = 100;
                        gvService.Columns["PRNumber"].Width = 100;
                    }
                    else
                    {
                        gv1.Columns["PRNumber"].Width = 0;
                        gvService.Columns["PRNumber"].Width = 0;
                    }
                }
                //--------------------------------------------------
                //--------------------------------------------------
                // Generatebtn, chkIsWithPR and glPRNumber behavior when in EDIT MODE
                //--------------------------------------------------
                if (Request.QueryString["entry"].ToString() == "E")
                {
                    // [1] With PR || [1] With Detail
                    if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithpr"] == "1" || chkIsWithPR.Value.ToString() == "True"))
                    {
                        //gv1.KeyFieldName = "DocNumber;LineNumber";
                        gv1.KeyFieldName = "LineNumber";

                        chkIsWithPR.ClientEnabled = false;
                        glPRNumber.ClientEnabled = false;
                        Generatebtn.ClientEnabled = false;

                        gv1.Columns["PRNumber"].Width = 100;
                        gvService.Columns["PRNumber"].Width = 100;
                    }

                    // [1] With PR || [0] With Detail
                    else if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithpr"] == "1" || chkIsWithPR.Value.ToString() == "True"))
                    {
                        //gv1.KeyFieldName = "DocNumber;LineNumber";
                        gv1.KeyFieldName = "LineNumber";

                        chkIsWithPR.ClientEnabled = true;
                        glPRNumber.ClientEnabled = true;
                        Generatebtn.ClientEnabled = true;

                        gv1.Columns["PRNumber"].Width = 100;
                        gvService.Columns["PRNumber"].Width = 100;
                    }
                    // [0] With PR || [1] With Detail
                    else if (Request.QueryString["iswithdetail"].ToString() == "true"  && (Session["iswithpr"] == "0" || chkIsWithPR.Value.ToString() == "False"))
                    {
                        //gv1.KeyFieldName = "DocNumber;LineNumber";
                        gv1.KeyFieldName = "LineNumber";
                        chkIsWithPR.ClientEnabled = true;
                        glPRNumber.ClientEnabled = false;
                        Generatebtn.ClientEnabled = false;
                    }

                    // [0] With PR || [0] With Detail
                    else if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithpr"] == "0" || chkIsWithPR.Value.ToString() == "False"))
                    {
                        gv1.KeyFieldName = "LineNumber";
                        //gv1.KeyFieldName = "DocNumber;LineNumber";
                        chkIsWithPR.ClientEnabled = true;
                        glPRNumber.ClientEnabled = false;
                        Generatebtn.ClientEnabled = false;
                    }
                }


                DataTable dtbldetail = Gears.RetriveData2("SELECT DocNumber FROM Procurement.PurchaseOrderDetail Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = dtbldetail.Rows.Count > 0 ? odsDetail.ID : sdsDetail.ID;

                dtbldetail = Gears.RetriveData2("SELECT DocNumber FROM Procurement.PurchaseOrderService Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gvService.DataSourceID = dtbldetail.Rows.Count > 0 ? odsDetail2.ID : sdsDetail2.ID;
                //DataSourceChecker();
                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                InitControls();
            }

        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["userid"].ToString();
            gparam._TransType = "PRCPOM";
            string strresult = GearsProcurement.GProcurement.PurchaseOrder_Validate(gparam);
            if (strresult != " ")
            {
                gv1.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(ASPxEdit sender)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientVisible = !view;
        }
        protected void LookupLoad(ASPxEdit sender)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;

            if (Request.QueryString["entry"].ToString() == "E")
            {
                if (!String.IsNullOrEmpty(glQuotationNumber.Text))
                {
                    glQuotationNumber.DropDownButton.Enabled = !edit;
                    glQuotationNumber.ReadOnly = edit;
                }
            }
        }
        protected void CheckBoxLoad(ASPxEdit sender)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;

            if (Request.QueryString["entry"].ToString() == "E")
            {
                if (!String.IsNullOrEmpty(chkIsWithInvoice.ToString()))
                    chkIsWithInvoice.ReadOnly = edit;  //Set chkIsWithInvoice To Readonly when in Edit Mode
            }
        }
        protected void Date_Load(ASPxEdit sender)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
            if (Request.QueryString["entry"].ToString() == "E")
            {
                //if (!String.IsNullOrEmpty(dtDateCompleted.Text) || !String.IsNullOrEmpty(dtCommitmentDate.Text))
                if (!String.IsNullOrEmpty(dtDateCompleted.Text))
                {
                    dtDateCompleted.DropDownButton.Enabled = !edit;
                    dtDateCompleted.ReadOnly = edit;

                    //dtCommitmentDate.DropDownButton.Enabled = !edit;
                    //dtCommitmentDate.ReadOnly = edit;
                }
            }
        }
        protected void SpinEdit_Load(ASPxEdit sender)//Control for all numeric entries in header
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
                spin.HorizontalAlign = HorizontalAlign.Right;
                spin.MinValue = 0;
                spin.MaxValue = 2147483647;
                spin.Increment = 0;
            

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                spin.ReadOnly = false;
            }
            else
            {
                if(spin.ID == "spinExchangeRate" || spin.ID == "spinTerms")
                {
                    spin.ReadOnly = false;
                }
                else
                {
                    spin.ReadOnly = true;
                }

            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
                if (view == true)
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit ||
                        e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                }
                if (Request.QueryString["entry"] == "N")
                {
                    if (Session["iswithpr"] == "1" || Session["iswithprcheckstate"] == "True")
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = false;
                        }
                    }
                    if (Session["iswithpr"] == "0" || Session["iswithprcheckstate"] == "False")
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = true;
                        }
                    }
                }


                //--------------------------------------------------
                // New Button During EDIT MODE
                //--------------------------------------------------
                if (Request.QueryString["entry"] == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }

                    // [1] With Detail && [1] With PR
                    if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithpr"] == "1" || chkIsWithPR.Value.ToString() == "True"))
                    {
                        //if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = false;
                        }
                    }

                    // [0] With Detail && [1] With PR
                    if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithpr"] == "1" || chkIsWithPR.Value.ToString() == "True"))
                    {
                        //if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = false;
                        }

                    }

                    // [1] With Detail && [0] With PR
                    if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithpr"] == "0" || chkIsWithPR.Value.ToString() == "False"))
                    {
                        //if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = true;
                        }

                    }

                    // [0] With Detail && [0] With PR
                    if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithpr"] == "0" || chkIsWithPR.Value.ToString() == "False"))
                    {
                        //if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                        if (e.ButtonType == ColumnCommandButtonType.New)
                        {
                            e.Visible = true;
                        }
                    }
                }
                //----------------------------------------------------------------------------------------------------

                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    //if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }
                }
            }
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
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

                 //if (Request.QueryString["entry"] == "E")
                 //{
                 //    if (e.ButtonID == "Delete")
                 //    {
                 //        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                 //    }
                 //}

                 if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                 {
                     if (e.ButtonID == "Delete")
                     {
                         e.Visible = DevExpress.Utils.DefaultBoolean.False;
                     }
                     if (e.ButtonID == "Delete2")
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
                if (Session["POitemID"] != null)
                {
                    if (Session["POitemID"].ToString() == glIDFinder(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                        Masterfileitemdetail.SelectCommand = Session["POsql"].ToString();
                        //Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                        //Session["FilterExpression"] = null;
                        gridLookup.DataBind();
                    }
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
            if (e.Parameters.Contains("ItemCode"))
            {

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


                    DataTable getUnitDescLine = Gears.RetriveData2("Select DISTINCT UnitBase,FullDesc from Masterfile.Item  Where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                    foreach (DataRow dt in getUnitDescLine.Rows)
                    {
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                    }


                    //DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode!='NONV' THEN 'True' ELSE 'False' END AS IsVat from masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'");
                    DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode!='NONV' THEN 'True' ELSE 'False' END AS IsVat from masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            if (tax.Rows.Count == 0)
                            {
                                codes += ";;";
                            }
                            else
                            {
                                foreach (DataRow dt in tax.Rows)
                                {
                                    codes += dt["TaxCode"].ToString() + ";";
                                    codes += dt["IsVat"].ToString() + ";";
                                }
                            }
                    


                    DataTable getVATRate = Gears.RetriveData2("select DISTINCT Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                               "on A.TaxCode = B.TCode " +
                                                               "where SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            if (getVATRate.Rows.Count == 0)
                            {
                                codes += "0" + ";";
                            }
                            else
                            {
                                foreach (DataRow dt in getVATRate.Rows)
                                {
                                    codes += dt["Rate"].ToString() + ";";
                                }
                            }
                    


                    DataTable getATCRate = Gears.RetriveData2("select DISTINCT ISNULL(Rate,0) AS ATCRate from Masterfile.BPSupplierInfo A left join Masterfile.ATC B " +
                                                  "on A.ATCCode = B.ATCCode " +
                                                  "where SupplierCode = '" + glSupplierCode.Text + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());//ADD CONN
                            if (getATCRate.Rows.Count == 0)
                            {
                                codes += "0" + ";";
                            }
                            else
                            {
                                foreach (DataRow dt in getATCRate.Rows)
                                {
                                    codes += dt["ATCRate"].ToString() + ";";
                                }
                            }

                    DataTable getUnitCost = Gears.RetriveData2("SELECT LastPrice FROM Masterfile.ItemSupplierPrice where ItemCode = '" + itemcode + "' AND ColorCode = '" + Session["ColorCode"].ToString() + "' AND ClassCode = '" + Session["ClassCode"].ToString() + "' AND SizeCode = '" + Session["SizeCode"].ToString() + "' AND Supplier = '" + glSupplierCode.Text +"'", Session["ConnString"].ToString());//ADD CONN
                            if (getUnitCost.Rows.Count == 0)
                            {
                                codes += "0" + ";";
                            }
                            else
                            {
                                foreach (DataRow dt in getUnitCost.Rows)
                                {
                                    codes += dt["LastPrice"].ToString() + ";";
                                }
                            }
                    
                itemlookup.JSProperties["cp_identifier"] = "ItemCode";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("VATCode"))
            {
                DataTable vat = Gears.RetriveData2("SELECT DISTINCT ISNULL(Rate,1) AS Rate FROM Masterfile.Tax WHERE TCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

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
            else
            {

                if (e.Parameters.Contains("ColorCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["POitemID"] = "ColorCode";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["POitemID"] = "ClassCode";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS ColorCode, ClassCode, '' AS SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["POitemID"] = "SizeCode";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("UnitBase"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, UnitBase FROM Masterfile.Item where ItemCode = '" + itemcode + "'";
                    Session["POitemID"] = "UnitBase";
                }

                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[3], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                Session["POsql"] = Masterfileitemdetail.SelectCommand;
                //Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                //Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
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
            string param = e.Parameter.Split('|')[0];
            switch (param)
            {
                case "SupplierCodeCase":
                    //if (Session["DataTable"] == "1")
                    //{
                    //    checkdel = true;
                    //    gv1.UpdateEdit();
                    //}
                    Session["iswithprcheckstate"] = chkIsWithPR.Checked.ToString();
                    DataTable dtbldetail = Gears.RetriveData2("SELECT DocNumber FROM Procurement.PurchaseOrderDetail Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    if (dtbldetail.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_iswithdetail"] = "True";
                    }
                    try
                    {
                        if (!String.IsNullOrEmpty(glSupplierCode.Value.ToString()))
                        {
                            SetText(e.Parameter.Split('|')[1]);
                            checkDetail(e.Parameter.Split('|')[1]);
                            //chkIsWithPR.ClientEnabled = true;
                        }
                    }
                    catch (Exception er)
                    {
                        spinTerms.Text = "0";
                        //cp.JSProperties["cp_vatrate"] = "0";
                        //cp.JSProperties["cp_atc"] = "0";
                        //cp.JSProperties["cp_VATCode"] = "";
                        //cp.JSProperties["cp_VATAX"] = "True";
                    }
                    break;
                
                case "Delete":
                    //GetSelectedVal(); 

                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "Close":
                        cp.JSProperties["cp_close"] = true;
                    break;

                

                //case "CancellationDate":

                //    if (Session["DataTable"] == "1")
                //    {
                //        checkdel = true;
                //        //tempdelete = true;
                //        gv1.UpdateEdit();
                //    }

                //    DateTime commitment = Convert.ToDateTime(dtCommitmentDate.Value);
                //    DateTime defaultcancel = commitment.AddDays(7);
                //    dtCancellationDate.Text = defaultcancel.ToShortDateString();
                //    checkDetail();
                //    break;
                //case "TargetDeliveryDate":
                //    if (Session["DataTable"] == "1")
                //    {
                //        checkdel = true;
                //        //tempdelete = true;
                //        gv1.UpdateEdit();
                //    }

                //    if(!String.IsNullOrEmpty(glSupplierCode.Text))
                //    {
                //        DateTime docdate = Convert.ToDateTime(dtDocDate.Value);
                //        DateTime defaulttarget = docdate.AddDays(Convert.ToInt32(spinTerms.Value));
                //        dtTargetDate.Text = defaulttarget.ToShortDateString();
                //    }
                //    checkDetail();
                //    break;
            }
        }
        protected void checkDetail(string supptext)
        {
                DataTable getvat = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                            "on A.TaxCode = B.TCode " +
                                                            "where SupplierCode = '" + supptext + "'", Session["ConnString"].ToString());//ADD CONN

                if (getvat.Rows.Count > 0)
                {
                    cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
                }

                else
                {
                    cp.JSProperties["cp_vatrate"] = "0";
                }
                DataTable getatc = Gears.RetriveData2("select ISNULL(Rate,0) AS Rate from Masterfile.BPSupplierInfo A left join Masterfile.ATC B " +
                                              "on A.ATCCode = B.ATCCode " +
                                              "where SupplierCode = '" + supptext + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());//ADD CONN

                if (getatc.Rows.Count > 0)
                {
                    cp.JSProperties["cp_atc"] = getatc.Rows[0]["Rate"].ToString();
                }

                else
                {
                    cp.JSProperties["cp_atc"] = "0";
                }

                DataTable getVATCode = Gears.RetriveData2("SELECT TaxCode FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND SupplierCode = '" + supptext + "'", Session["ConnString"].ToString());//ADD CONN
                if (getVATCode.Rows.Count > 0)
                {
                    cp.JSProperties["cp_VATCode"] = getVATCode.Rows[0]["TaxCode"].ToString();
                }

                else
                {
                    cp.JSProperties["cp_VATCode"] = "";
                }

                cp.JSProperties["cp_VATAX"] = "True";

        }

        //protected void checkMOQ()
        //{


        //    DataTable getvat = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
        //                                                "on A.TaxCode = B.TCode " +
        //                                                "where SupplierCode = '" + glSupplierCode.Text + "'");

        //    if (getvat.Rows.Count > 0)
        //    {
        //        cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
        //    }

        //    else
        //    {
        //        cp.JSProperties["cp_vatrate"] = "0";
        //    }

        //    cp.JSProperties["cp_VATAX"] = "True";

        //}


        //protected void GetVat()
        //{
            //DataTable getvat = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
            //                                            "on A.TaxCode = B.TCode " +
            //                                            "where SupplierCode = '" + glSupplierCode.Text + "'");

            //if (getvat.Rows.Count > 0)
            //{
            //    cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
            //}

            //else
            //{
            //    cp.JSProperties["cp_vatrate"] = "0";
            //}
            //DataTable getatc = Gears.RetriveData2("select ISNULL(Rate,0) AS Rate from Masterfile.BPSupplierInfo A left join Masterfile.ATC B " +
            //                              "on A.ATCCode = B.ATCCode " +
            //                              "where SupplierCode = '" + glSupplierCode.Text + "' and IsWithholdingTaxAgent ='1'");

            //if (getatc.Rows.Count > 0)
            //{
            //    cp.JSProperties["cp_atc"] = getatc.Rows[0]["Rate"].ToString();
            //}

            //else
            //{
            //    cp.JSProperties["cp_atc"] = "0";
            //}
        //}
        protected void SetText(string supptext)
        {

            DataTable getContactPerson = Gears.RetriveData2("SELECT ContactPerson FROM Masterfile.BPSupplierInfo where SupplierCode = '" + supptext + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getContactPerson.Rows)
            {
                txtContactPerson.Value = dt[0].ToString();
            }

            //ds.SelectCommand = string.Format("SELECT Currency,SupplierCode FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'");
            //DataView cur = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (cur.Count > 0)
            //{
            //    txtCurrency.Value = cur[0][0].ToString();
            //}

            DataTable getCurrency = Gears.RetriveData2("SELECT Currency FROM Masterfile.BPSupplierInfo where SupplierCode = '" + supptext + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getCurrency.Rows)
            {
                txtCurrency.Value = dt[0].ToString();
            }


            DataTable getExchangeRate = Gears.RetriveData2("SELECT Currency FROM Masterfile.BPSupplierInfo where SupplierCode = '" + supptext + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getCurrency.Rows)
            {
                txtCurrency.Value = dt[0].ToString();
            }
            //ds.SelectCommand = string.Format("SELECT ISNULL(APTerms,0) AS APTerms,SupplierCode FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'");
            //DataView terms = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (terms.Count > 0)
            //{
            //    txtTerms.Value = terms[0][0].ToString();

            //}

            DataTable getTerms = Gears.RetriveData2("SELECT ISNULL(APTerms,0) AS APTerms FROM Masterfile.BPSupplierInfo where SupplierCode = '" + supptext + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getTerms.Rows)
            {
                spinTerms.Value = dt[0].ToString();
            }


            if (!String.IsNullOrEmpty(supptext))
            {
                DateTime docdate = Convert.ToDateTime(dtDocDate.Value);
                DateTime defaulttarget = docdate.AddDays(Convert.ToInt32(spinTerms.Value));
                dtTargetDate.Text = defaulttarget.ToShortDateString();
                dtCommitmentDate.Text = defaulttarget.ToShortDateString();
                DateTime defaultcancellationdate = defaulttarget.AddDays(7);
                dtCancellationDate.Text = defaultcancellationdate.ToShortDateString();
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";
            double Qty = 0.00;
            double MOQ = 0.00;
            if (error == false && check == false)
            {
                //foreach (GridViewColumn column in gv1.Columns)
                //{
                    //GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    //if (dataColumn == null) continue;
                    //if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber"
                    //    && dataColumn.FieldName != "PRNumber" && dataColumn.FieldName != "OrderQty" && dataColumn.FieldName != "UnitCost"
                    //    && dataColumn.FieldName != "FullDesc" && dataColumn.FieldName != "Unit" && dataColumn.FieldName != "UnitFreight"
                    //    && dataColumn.FieldName != "VATCode" && dataColumn.FieldName != "IsVat" && dataColumn.FieldName != "IsAllowPartial"
                    //    && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3"
                    //    && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6"
                    //    && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9")
                    //{
                    //    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                    //}



                //ItemCode = e.NewValues["ItemCode"].ToString();
                //ColorCode = e.NewValues["ColorCode"].ToString();
                //ClassCode = e.NewValues["ClassCode"].ToString();
                //SizeCode = e.NewValues["SizeCode"].ToString();
                //Qty = Convert.ToDouble(e.NewValues["OrderQty"].ToString());

                //DataTable MOQ1 = Gears.RetriveData2("SELECT ISNULL(MOQ,0) AS MOQ FROM Masterfile.ItemDetail"
                //                                                      + " WHERE ISNULL(IsInactive,0)=0"
                //                                                      + " AND ItemCode = '" + ItemCode+ "'"
                //                                                      + " AND ColorCode = '" + ColorCode + "'"
                //                                                      + " AND ClassCode = '" + ClassCode + "'"
                //                                                      + " AND SizeCode = '" + SizeCode + "'", Session["ConnString"].ToString());//ADD CONN
                //foreach (DataRow dt in MOQ1.Rows)
                //{
                //    MOQ = Convert.ToDouble(dt[0].ToString());
                //}
                //    if(MOQ > 0)
                //    {
                //        if(Qty < MOQ)
                //        {
                //            Session["Message"] += ItemCode + ", ";
                //        }
                //    }
                //    else
                //    {
                //        DataTable MOQ2 = Gears.RetriveData2("SELECT ISNULL(MinQTY,0) AS MinQTY FROM Masterfile.Item WHERE ISNULL(IsInactive,0)=0"
                //                                                      + " AND ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());//ADD CONN

                //        foreach (DataRow dt in MOQ2.Rows)
                //        {
                //            MOQ = Convert.ToDouble(dt[0].ToString());
                //        }

                //        if (Qty < MOQ)
                //        {
                //            Session["Message"] += ItemCode + ", ";
                //        }
                //    }
                //}
                if (e.Errors.Count > 0)
                {
                    error = true; //bool to cancel adding/updating if true
                }
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
            ASPxGridView Grid = sender as ASPxGridView;
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.InsertValues.Clear();
                //e.DeleteValues.Clear();
                //e.UpdateValues.Clear();
            }
            //-----
            //-----
            //if (tempdelete == true)
            //{
            //    if(e.DeleteValues.Count > 0)
            //    {
            //        e.Handled = true;
            //        e.UpdateValues.Clear();
            //        e.InsertValues.Clear();

            //        DataTable source = new DataTable();
            //        if (e.DeleteValues.Count > 0)
            //            source = GetSelectedVal();

            //        // Removing all deleted rows from the data source(Excel file)
            //        foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //        {
            //            try
            //            {
            //                object[] keys = { values.Keys[0], values.Keys[1] };
            //                source.Rows.Remove(source.Rows.Find(keys));
            //            }
            //            catch (Exception)
            //            {
            //                continue;
            //            }
            //        }


            //        tempdelete = false;
            //        gv1.DataBind();

            //    }
                
            //}

            //-----
            //-----

            if (Session["Datatable"] == "1" && check == true && Grid.ID == "gv1")
             {
                //e.Handled = true;
                
                ////DataTable source = GetSelectedVal();
                //e.DeleteValues.Clear();
                ////e.UpdateValues.Clear();

                //DataTable source = Session["newsource"] as DataTable;
                //gv1.DataSourceID = null;
                //gv1.DataSource = source;
                //gv1.DataBind();

                 e.Handled = true;
                 DataTable source = GetSelectedVal();


                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = 
                    { 
                        //values.NewValues["DocNumber"], 
                        values.NewValues["LineNumber"] 
                    };
                    DataRow row = source.Rows.Find(keys);
                    //row["PRNumber"] = values.NewValues["PRNumber"];
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["OrderQty"] = values.NewValues["OrderQty"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["UnitCost"] = values.NewValues["UnitCost"];
                    row["ReceivedQty"] = values.NewValues["ReceivedQty"];
                    row["UnitFreight"] = values.NewValues["UnitFreight"];
                    row["VATCode"] = values.NewValues["VATCode"];
                    row["IsVat"] = values.NewValues["IsVat"];
                    row["IsAllowPartial"] = values.NewValues["IsAllowPartial"];
                    row["Rate"] = values.NewValues["Rate"];
                    row["ATCRate"] = values.NewValues["ATCRate"];
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

                // Removing all deleted rows from the data source(Excel file)
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



                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                //_EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                _EntityDetail.ItemCode  = dtRow["ItemCode"].ToString();
                _EntityDetail.FullDesc  = dtRow["FullDesc"].ToString();
                _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                _EntityDetail.SizeCode  = dtRow["SizeCode"].ToString();
                _EntityDetail.PRNumber  = dtRow["PRNumber"].ToString();

                _EntityDetail.OrderQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OrderQty"]) ? 0 : dtRow["OrderQty"]);
                _EntityDetail.Unit = dtRow["Unit"].ToString();
                _EntityDetail.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                _EntityDetail.ReceivedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedQty"]) ? 0 : dtRow["ReceivedQty"]);
                _EntityDetail.UnitFreight = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFreight"]) ? 0 : dtRow["UnitFreight"]);
                _EntityDetail.IsVat = Convert.ToBoolean(Convert.ToInt16(dtRow["IsVat"].ToString()));
                _EntityDetail.IsAllowPartial = Convert.ToBoolean(dtRow["IsAllowPartial"].ToString());
                _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                _EntityDetail.Rate = Convert.ToDecimal(Convert.IsDBNull(dtRow["Rate"]) ? 0 : dtRow["Rate"]);
                _EntityDetail.ATCRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ATCRate"]) ? 0 : dtRow["ATCRate"]);
                _EntityDetail.Field1 = dtRow["Field1"].ToString();
                _EntityDetail.Field2 = dtRow["Field2"].ToString();
                _EntityDetail.Field3 = dtRow["Field3"].ToString();
                _EntityDetail.Field4 = dtRow["Field4"].ToString();
                _EntityDetail.Field5 = dtRow["Field5"].ToString();
                _EntityDetail.Field6 = dtRow["Field6"].ToString();
                _EntityDetail.Field7 = dtRow["Field7"].ToString();
                _EntityDetail.Field8 = dtRow["Field8"].ToString();
                _EntityDetail.Field9 = dtRow["Field9"].ToString();
                 _EntityDetail.AddPurchasedOrderDetail(_EntityDetail);
                }
            }
        }
        #endregion
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["purchaserequestdetail"] = null;
            }
            ASPxGridView grid = sender as ASPxGridView;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(grid.ID)))
            if (Session["purchaserequestdetail"] != null)
            {
                gv1.DataSourceID = sdsPicklistDetail.ID;
                sdsPicklistDetail.FilterExpression = Session["purchaserequestdetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        private DataTable GetSelectedVal()
        {
            gv1.DataSourceID = null;
            //gv1.DataBind();
            string vatcode = "";
            double VATRate = 0.00;
            double ATCRate = 0.00;

            DataTable dt = new DataTable();
            string[] selectedValues = glPRNumber.Text.Split(';');
            string subok = "('" + string.Join("','", selectedValues) + "')";
            //CriteriaOperator suppcrit = new BinaryOperator("SupplierCode","%"+glSupplierCode.Text+"%",BinaryOperatorType.Like);
            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, suppcrit)).ToString();

            DataTable getVATCode = Gears.RetriveData2("SELECT TaxCode FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
            if (getVATCode.Rows.Count > 0)
            {
                vatcode = getVATCode.Rows[0]["TaxCode"].ToString();
            }

            else
            {
                vatcode = "";
            }



            DataTable getVATRate = Gears.RetriveData2("select ISNULL(Rate,0) AS Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                       "on A.TaxCode = B.TCode " +
                                                       "where SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN

            if (getVATRate.Rows.Count > 0)
            {
                VATRate = Convert.ToDouble(getVATRate.Rows[0]["Rate"].ToString());
            }

            else
            {
                VATRate = 0.00;
            }


            DataTable getATCRate = Gears.RetriveData2("select ISNULL(Rate,0) AS Rate from Masterfile.BPSupplierInfo A left join Masterfile.ATC B " +
                                          "on A.ATCCode = B.ATCCode " +
                                          "where SupplierCode = '" + glSupplierCode.Text + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());//ADD CONN

            if (getATCRate.Rows.Count > 0)
            {
                ATCRate = Convert.ToDouble(getATCRate.Rows[0]["Rate"].ToString());
            }

            else
            {
                ATCRate = 0.00;
            }


            sdsPicklistDetail.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber"
	                                               + ",A.DocNumber"
	                                               + ",A.DocNumber AS PRNumber"
	                                               + ",'"+glSupplierCode.Text+"' AS SupplierCode"
	                                               + ",A.ItemCode"	
	                                               + ",FullDesc"
                                                   + ",A.ColorCode"
                                                   + ",A.ClassCode"
                                                   + ",A.SizeCode"	
	                                               + ",(ISNULL(RequestQty,0) - ISNULL(OrderQty,0)) AS OrderQty"		
	                                               + ",A.UnitBase AS Unit"
                                                   + ",ISNULL(ISP.LastPrice,0) AS UnitCost"
                                                   + ",0 AS ReceivedQty"
                                                   + ",0.00 as UnitFreight"
                                                   + ",'" + vatcode + "' AS VATCode"
                                                   + ",CASE WHEN '" + vatcode + "'!='NONV' THEN 1 ELSE 0 END AS IsVat"
	                                               + ",ISNULL(A.IsAllowPartial,0) AS IsAllowPartial"
                                                   + ",'" + VATRate + "' AS Rate"
                                                   + ",'" + ATCRate + "' AS ATCRate"
	                                               + ",A.Field1,A.Field2,A.Field3"
	                                               + ",A.Field4,A.Field5,A.Field6"
	                                               + ",A.Field7,A.Field8,A.Field9" 
	                                               + " FROM Procurement.PurchaseRequestDetail A"
	                                               + " INNER JOIN Procurement.PurchaseRequest B"
	                                               + " ON A.DocNumber = B.DocNumber" 
	                                               + " INNER JOIN Masterfile.Item D" 
	                                               + " ON A.ItemCode = D.ItemCode"
                                                   + " LEFT JOIN Masterfile.ItemSupplierPrice ISP"
		                                           + " ON A.ItemCode = ISP.ItemCode"
		                                           + " AND A.ColorCode = ISP.ColorCode"
		                                           + " AND A.ClassCode = ISP.ClassCode"
		                                           + " AND A.SizeCode = ISP.SizeCode"
                                                   + " AND '" + glSupplierCode.Text + "' = ISP.Supplier WHERE A.DocNumber IN "+ subok +"";


            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["purchaserequestdetail"] = sdsPicklistDetail.FilterExpression;
            //sdsPicklistDetail.DataBind();
            gv1.DataSource = sdsPicklistDetail;
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
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};
            dt.Columns["LineNumber"]};

            return dt;
        }
        protected void SetStatus()
        {
            if (_Entity.Status == "P")
                txtStatus.Value = "Partial";
            else if (_Entity.Status == "C")
                txtStatus.Value = "Closed";
            else if (_Entity.Status == "L")
                txtStatus.Value = "Cancelled";

            else if (_Entity.Status == "X")
                txtStatus.Value = "Manual Closed";
            else if (_Entity.Status == "A")
                txtStatus.Value = "Partial Closed";
            else if (_Entity.Status == "O")
                txtStatus.Value = "Re-Open";
            else
            {
                txtStatus.Value = "New";
            }
        }
        protected void SaveStatus()
        {
            if (txtStatus.Text == "Partial")
                _Entity.Status = "P";
            else if (txtStatus.Text == "Closed")
                _Entity.Status = "C";
            else if (txtStatus.Text == "Cancelled")
                _Entity.Status = "L";
            else if (txtStatus.Text == "Manual Closed")
                _Entity.Status = "X";
            else if (txtStatus.Text == "Partial Closed")
                _Entity.Status = "A";
            else if (txtStatus.Text == "Re-Open")
                _Entity.Status = "O";
            else
                _Entity.Status = "N";

        }
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtDocDate.Date = DateTime.Now;
            }
        }

        protected void GrossNonVATCheck()
        {
            double GrossVAT = 0.00;
            double NonVAT = 0.00;
            double PesoAmount = 0.00;
            double Total = 0.00;

            GrossVAT = Convert.ToDouble(spinGrossVATableAmount.Value.ToString());
            NonVAT = Convert.ToDouble(spinNonVATableAmount.Value.ToString());
            PesoAmount = Convert.ToDouble(spinPesoAmount.Value.ToString());
            Total = GrossVAT + NonVAT;

            if ((GrossVAT + NonVAT) != PesoAmount)
            {
                Session["GrossNonMessage"] = "Gross VATable Amount and Non VATable Amount total is not equal to Peso Amount!";
                error = true;
            }
        }
        protected void DeleteorNot()
        {
            if (Session["DELETEDETAIL"] == "1")
            {
                Gears.RetriveData2("DELETE FROM Procurement.PurchaseOrderDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                Gears.RetriveData2("DELETE FROM Procurement.PurchaseOrderService WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            //Session["userid"] = "1828";
            //Session["ConnString"] = "Data Source=192.168.201.115;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;connection timeout=1800;";
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsPicklistDetail.ConnectionString = Session["ConnString"].ToString();
            //SupplierCodelookup.ConnectionString = Session["ConnString"].ToString();
            ////ContactPersonlookup.ConnectionString = Session["ConnString"].ToString();
            //Currencylookup.ConnectionString = Session["ConnString"].ToString();
            //Termslookup.ConnectionString = Session["ConnString"].ToString();
            //ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            //PRNumberlookup.ConnectionString = Session["ConnString"].ToString();
            //QuotationNumberlookup.ConnectionString = Session["ConnString"].ToString();
            //Unitlookup.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //Temporary.ConnectionString = Session["ConnString"].ToString();
            //VatCodeLookup.ConnectionString = Session["ConnString"].ToString();
        }

        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate = dtDocDate.Text;
            _Entity.SupplierCode = glSupplierCode.Text;
            _Entity.TargetDeliveryDate = dtTargetDate.Text;
            _Entity.Status = txtStatus.Text;
            _Entity.DateCompleted = dtDateCompleted.Text;
            _Entity.ReceivingWarehouse = glReceivingWarehouse.Text;
            _Entity.CancellationDate = dtCancellationDate.Text;
            _Entity.ContactPerson = txtContactPerson.Text;
            _Entity.CommitmentDate = dtCommitmentDate.Text;
            _Entity.QuotationNo = glQuotationNumber.Text;
            _Entity.Remarks = memoremarks.Text;
            _Entity.Broker = glBroker.Text;
            _Entity.IsWithPR = Convert.ToBoolean(chkIsWithPR.Value);
            _Entity.IsWithInvoice = Convert.ToBoolean(chkIsWithInvoice.Value);


            _Entity.PRNumberH = glPRNumber.Text;

            _Entity.Currency = txtCurrency.Text;
            _Entity.Terms = Convert.ToInt32(Convert.IsDBNull(spinTerms.Value) ? 0 : Convert.ToInt32(spinTerms.Value));


            _Entity.ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(spinExchangeRate.Value) ? 0 : Convert.ToDecimal(spinExchangeRate.Value));

            _Entity.TotalFreight = Convert.ToDecimal(Convert.IsDBNull(spinTotalFreight.Value) ? 0 : Convert.ToDecimal(spinTotalFreight.Value));
            _Entity.WithholdingTax = Convert.ToDecimal(Convert.IsDBNull(spinWithholdingTax.Value) ? 0 : Convert.ToDecimal(spinWithholdingTax.Value));
            _Entity.TotalQty = Convert.ToDecimal(Convert.IsDBNull(spinTotalQty.Value) ? 0 : Convert.ToDecimal(spinTotalQty.Value));
            _Entity.VATAmount = Convert.ToDecimal(Convert.IsDBNull(spinVATAmount.Value) ? 0 : Convert.ToDecimal(spinVATAmount.Value));
            _Entity.PesoAmount = Convert.ToDecimal(Convert.IsDBNull(spinPesoAmount.Value) ? 0 : Convert.ToDecimal(spinPesoAmount.Value));
            _Entity.ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(spinForeignAmount.Value) ? 0 : Convert.ToDecimal(spinForeignAmount.Value));
            _Entity.GrossVATableAmount = Convert.ToDecimal(Convert.IsDBNull(spinGrossVATableAmount.Value) ? 0 : Convert.ToDecimal(spinGrossVATableAmount.Value));
            _Entity.NonVATableAmount = Convert.ToDecimal(Convert.IsDBNull(spinNonVATableAmount.Value) ? 0 : Convert.ToDecimal(spinNonVATableAmount.Value));

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

            SaveStatus();

            switch(e.Parameters)
            {
                case "Add":

                    //GrossNonVATCheck();     // Check if Gross VATable Amount and Non VAT Amoun Total is equal to Peso Amount    LGE 02 - 24 - 2016
                    //if (Session["DataTable"] == "1")
                    //    checkdel = true;
                    //gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber, "Procurement.PurchaseOrder", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        gv1.JSProperties["cp_message"] = strError;
                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_forceclose"] = true;
                        return;
                    }


                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSource = GetSelectedVal();
                            gv1.UpdateEdit();
                            gvService.DataSourceID = odsDetail2.ID;//Renew datasourceID to entity
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gvService.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = odsDetail.ID;//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                            gvService.DataSourceID = odsDetail2.ID;//Renew datasourceID to entity
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gvService.UpdateEdit();
                        }
                        //  _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
                        gv1.JSProperties["cp_message2"] = Session["Message"];
                        gv1.JSProperties["cp_message"] = "Successfully Added!";
                        //cp.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        Session["Message"] = null;
                    }
                    else
                    {
                        //if (Session["GrossNonMessage"] != null)
                        //{
                        //    cp.JSProperties["cp_message"] = Session["GrossNonMessage"];
                        //    cp.JSProperties["cp_success"] = true;
                        //    Session["GrossNonMessage"] = null;
                        //}
                        //else
                        //{
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                        //}

                    }
                    break;

                case "Update":

                    // GrossNonVATCheck();     // Check if Gross VATable Amount and Non VAT Amoun Total is equal to Peso Amount    LGE 02 - 24 - 2016
                    //gv1.DataSource = null;
                    //if (Session["DataTable"] == "1")
                    //    checkdel = true;
                    //gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber, "Procurement.PurchaseOrder", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        gv1.JSProperties["cp_message"] = strError;
                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {

                            //gv1.DataSource = sdsPicklistDetail;
                            Gears.RetriveData2("DELETE FROM Procurement.PurchaseOrderDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            gv1.DataSourceID = sdsPicklistDetail.ID;
                            gv1.UpdateEdit();
                            gvService.DataSourceID = odsDetail2.ID;//Renew datasourceID to entity
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gvService.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = odsDetail.ID;//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            DeleteorNot();
                            gv1.UpdateEdit();//2nd initiation to insert grid
                            gvService.DataSourceID = odsDetail2.ID;//Renew datasourceID to entity
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gvService.UpdateEdit();
                        }
                        //  _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
                        gv1.JSProperties["cp_message2"] = Session["Message"];
                        gv1.JSProperties["cp_message"] = "Successfully Updated!";
                        //cp.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        Session["Message"] = null;
                    }
                    else
                    {
                        //if (Session["GrossNonMessage"] != null)
                        //{
                        //    cp.JSProperties["cp_message"] = Session["GrossNonMessage"];
                        //    cp.JSProperties["cp_success"] = true;
                        //    Session["GrossNonMessage"] = null;
                        //}
                        //else
                        //{
                        gv1.JSProperties["cp_message"] = "Please check all the fields!";
                        gv1.JSProperties["cp_success"] = true;
                        //}
                    }
                    break;


                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    gv1.JSProperties["cp_close"] = true;
                    gv1.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Generate":
                    Session["newsource"] = null;
                    //SetText();
                    gv1.JSProperties["cp_generated"] = true;
                    GetSelectedVal();
                    gv1.Columns["PRNumber"].Width = 100;
                    break;

                case "iswithprtrue":
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    Session["iswithpr"] = "1";
                    gv1.JSProperties["cp_iswithpr"] = "1";
                    //SetText();
                    gv1.Columns["PRNumber"].Width = 100;
                    break;
                case "iswithprfalse":
                    Session["iswithpr"] = "0";
                    Session["Datatable"] = "0";
                    Session["DELETEDETAIL"] = "1";
                    //gv1.DataSource = null;
                    //gv1.DataBind();
                    gv1.DataSource = null;
                    gv1.DataSourceID = sdsDetail.ID;
                    gv1.DataBind();
                    gv1.JSProperties["cp_iswithpr"] = "0";
                    //SetText();

                    chkIsWithPR.ClientEnabled = true;
                    gv1.Columns["PRNumber"].Width = 0;
                    break;
            }
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                //sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInActive,0) = 0";
                SetItemCodeDS();
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


                DataTable getUnitDescLine = Gears.RetriveData2("Select DISTINCT UnitBase,FullDesc from Masterfile.Item  Where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in getUnitDescLine.Rows)
                {
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                }


                //DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode!='NONV' THEN 'True' ELSE 'False' END AS IsVat from masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'");
                DataTable tax = Gears.RetriveData2("Select DISTINCT TaxCode, CASE WHEN TaxCode!='NONV' THEN 'True' ELSE 'False' END AS IsVat from masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                if (tax.Rows.Count == 0)
                {
                    codes += ";;";
                }
                else
                {
                    foreach (DataRow dt in tax.Rows)
                    {
                        codes += dt["TaxCode"].ToString() + ";";
                        codes += dt["IsVat"].ToString() + ";";
                    }
                }

                DataTable getVATRate = Gears.RetriveData2("select DISTINCT Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                           "on A.TaxCode = B.TCode " +
                                                           "where SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                if (getVATRate.Rows.Count == 0)
                {
                    codes += "0" + ";";
                }
                else
                {
                    foreach (DataRow dt in getVATRate.Rows)
                    {
                        codes += dt["Rate"].ToString() + ";";
                    }
                }

                DataTable getATCRate = Gears.RetriveData2("select DISTINCT ISNULL(Rate,0) AS ATCRate from Masterfile.BPSupplierInfo A left join Masterfile.ATC B " +
                                              "on A.ATCCode = B.ATCCode " +
                                              "where SupplierCode = '" + glSupplierCode.Text + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());//ADD CONN
                if (getATCRate.Rows.Count == 0)
                {
                    codes += "0" + ";";
                }
                else
                {
                    foreach (DataRow dt in getATCRate.Rows)
                    {
                        codes += dt["ATCRate"].ToString() + ";";
                    }
                }

                DataTable getUnitCost = Gears.RetriveData2("SELECT LastPrice FROM Masterfile.ItemSupplierPrice where ItemCode = '" + itemcode + "' AND ColorCode = '" + Session["ColorCode"].ToString() + "' AND ClassCode = '" + Session["ClassCode"].ToString() + "' AND SizeCode = '" + Session["SizeCode"].ToString() + "' AND Supplier = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                if (getUnitCost.Rows.Count == 0)
                {
                    codes += "0" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnitCost.Rows)
                    {
                        codes += dt["LastPrice"].ToString() + ";";
                    }
                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "ItemCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
        protected void SetItemCodeDS()
        {
            DataTable getNOPR = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='NOPR'", Session["ConnString"].ToString());//ADD CONN
            string NOPR = getNOPR.Rows[0]["Value"].ToString();


            if (NOPR.ToUpper() == "YES")
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.Item I INNER JOIN Masterfile.ItemCategory IC ON I.ItemCategoryCode = IC.ItemCategoryCode WHERE ISNULL(I.IsInactive,0)=0 AND (ISNULL(IsService,0) = 1 OR ISNULL(IsStock,0) = 1 OR ISNULL(IsAsset,0)=1)";
                chkIsWithPR.ClientEnabled = false;
            }
            else
            {
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.Item I INNER JOIN Masterfile.ItemCategory IC ON I.ItemCategoryCode = IC.ItemCategoryCode WHERE ISNULL(I.IsInactive,0)=0 AND ISNULL(IsCore,0)=1 AND (ISNULL(IsService,0) = 1 OR ISNULL(IsStock,0) = 1 OR ISNULL(IsAsset,0)=1)";
            }
            sdsItem.DataBind();
        }
        protected void MemoLoad(ASPxEdit sender)
        {
            ((ASPxMemo)sender).ReadOnly = view;
        }

        protected void InitControls()
        {
            foreach (var item in MainForm.Items)
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
                ASPxEdit editor = null;

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
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxMemo")
                    {
                        MemoLoad(editor);
                    }
                }
            }
        }

        protected void glSupplierCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
            if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.DataSourceID = "SupplierCodelookup";
                SupplierCodelookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.[BPSupplierInfo] WHERE ISNULL([IsInactive],0) = 0";
                gridLookup.DataBind();
            }
        }

        protected void glPRNumber_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
                if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                {
                    gridLookup.GridView.DataSourceID = "PRNumberlookup";
                    PRNumberlookup.SelectCommand = "SELECT DocNumber FROM Procurement.[PurchaseRequest] WHERE ISNULL (ApprovedBy,'')!='' AND Status NOT IN ('C','X')";
                    gridLookup.DataBind();
                }
        }

        protected void glBroker_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
                if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                {
                    gridLookup.GridView.DataSourceID = "SupplierCodelookup";
                    SupplierCodelookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.[BPSupplierInfo] WHERE ISNULL([IsInactive],0) = 0";
                    gridLookup.DataBind();
                }
        }

        protected void gvPRDetails_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string vatcode = "";
            double VATRate = 0.00;
            double ATCRate = 0.00;
            string[] selectedValues = glPRNumber.Text.Split(';');
            string subok = "('" + string.Join("','", selectedValues) + "')";
            //CriteriaOperator suppcrit = new BinaryOperator("SupplierCode","%"+glSupplierCode.Text+"%",BinaryOperatorType.Like);
            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, suppcrit)).ToString();

            DataTable getVATCode = Gears.RetriveData2("SELECT TaxCode FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
            if (getVATCode.Rows.Count > 0)
            {
                vatcode = getVATCode.Rows[0]["TaxCode"].ToString();
            }

            else
            {
                vatcode = "";
            }

            DataTable getVATRate = Gears.RetriveData2("select ISNULL(Rate,0) AS Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                       "on A.TaxCode = B.TCode " +
                                                       "where SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN

            if (getVATRate.Rows.Count > 0)
            {
                VATRate = Convert.ToDouble(getVATRate.Rows[0]["Rate"].ToString());
            }

            else
            {
                VATRate = 0.00;
            }


            string PRSelect = "SELECT A.DocNumber,RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber," +
                            "A.DocNumber AS PRNumber,A.ServiceType,A.Description," +
                            "(ISNULL(Qty,0) - ISNULL(ServicePOQty,0)) AS ServiceQty,A.Unit,0 AS UnitCost," +
                            "0 AS TotalCost,IsAllowProgressBilling,CASE WHEN '" + vatcode + "' !='NONV' " +
                            "THEN convert(bit,1) ELSE convert(bit,0) END AS IsVat,'" + vatcode + "' AS VATCode," + VATRate + " AS VATRate," +
                            "0 AS CostApplied,'' as EPNumber,convert(bit,0) as IsClosed,A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7," +
                            "A.Field8,A.Field9 FROM Procurement.PurchaseRequestService A " +
                            "INNER JOIN Procurement.PurchaseRequest B ON A.DocNumber = B.DocNumber " +
                            "WHERE A.DocNumber IN " + subok + "";
            DataTable gvPR = Gears.RetriveData2(PRSelect, Session["ConnString"].ToString());
            if (gvPR.Rows.Count > 0)
            {
                gvPRDetails.DataSource = gvPR;
                gvPRDetails.DataBind();
            }
        }

        protected void gvService_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
                case "iswithprtrue":
                    //gvService.DataSourceID = null;
                    gvService.DataBind();
                    Session["iswithpr"] = "1";
                    gvService.JSProperties["cp_iswithpr"] = "1";
                    //SetText();
                    gvService.Columns["PRNumber"].Width = 100;
                    break;
                case "iswithprfalse":
                    Session["iswithpr"] = "0";
                    Session["Datatable"] = "0";
                    Session["DELETEDETAIL"] = "1";
                    //gv1.DataSource = null;
                    //gv1.DataBind();
                    gvService.DataSource = null;
                    gvService.DataSourceID = sdsDetail2.ID;
                    gvService.DataBind();
                    gvService.JSProperties["cp_iswithpr"] = "0";
                    //SetText();

                    chkIsWithPR.ClientEnabled = true;
                    gv1.Columns["PRNumber"].Width = 0;
                    break;
            }
        }



    }
}