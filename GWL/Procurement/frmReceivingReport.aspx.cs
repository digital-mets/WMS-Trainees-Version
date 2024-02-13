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
using GearsProcurement;
using GearsCommon;
namespace GWL
{
    public partial class frmReceivingReport : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.ReceivingReport _Entity = new ReceivingReport();//Calls entity odsHeader
        Entity.ReceivingReport.ReceivingReportDetail _EntityDetail = new ReceivingReport.ReceivingReportDetail();//Call entity sdsDetail
        string access1 = " ";
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

            if (gv1.DataSource != null)
            {
                gv1.DataSourceID = null;
            }
            if (!IsPostBack)
            {
                //V=View, E=Edit, N=New
                //Session["glene"] = null;
                //Session["tina"] = null;
                //Session["melyn"] = null;
                //Session["euny"] = null;
                Session[Request.QueryString["docnumber"].ToString() + "Datatable"] = null;
                Session[Request.QueryString["docnumber"].ToString() + "glene"] = null;
                Session[Request.QueryString["docnumber"].ToString() + "tina"] = null;
                Session[Request.QueryString["docnumber"].ToString() + "melyn"] = null;
                Session[Request.QueryString["docnumber"].ToString() + "euny"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        GetVat();
                        updateBtn.Text = "Add";
                        glPicklist.Visible = true;
                        frmlayout1.FindItemOrGroupByName("JEntries").ClientVisible = false;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        break;
                    case "E":
                        GetVat();
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        string access = GCommon.SystemSetting("ACC_VIEWCOST", Session["ConnString"].ToString());
                        access1 = GCommon.ACCESS(Session["userid"].ToString(), "PRCRCR", access, Session["ConnString"].ToString());
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;

                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                //dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                //txtApVNumber.Value = _Entity.APVNumber.ToString();
                //glSupplierCode.Value = _Entity.SupplierCode.ToString();
                //memRemarks.Value = _Entity.Remarks.ToString();
                //glPicklist.Value = _Entity.ReferenceNumber.ToString();
                //txtAuxiliaryReference.Text = _Entity.AuxilaryReference;
                //txtDrNumber.Text = _Entity.DRNumber;
                //txtSINumber.Text = _Entity.SINumber;
                //txtBroker.Value = _Entity.Broker;
                //txtTerms.Text = _Entity.Terms;
                //aglWarehouseCode.Value = _Entity.WarehouseCode.ToString();
               // speExchangeRate.Value = _Entity.ExchangeRate.ToString();
                //speTotalFreight.Value = _Entity.TotalFreight.ToString();
                //speTotalQuantity.Value = _Entity.TotalQuantity.ToString();
                //speTotalBulk.Value = _Entity.TotalBulkQty.ToString();
                //chckComplimentary.Value = _Entity.ComplimentaryItem;

                //txtCurrency.Value = _Entity.Currency;
                //spePesoAmount.Value = _Entity.TotalAmount.ToString();
                //speForeigntAmount.Value = _Entity.ForeignAmount.ToString();
                //speGrossVatableAmount.Value = _Entity.GrossVatableAmount.ToString();
                //speNonVatableAmount.Value = _Entity.NonVatableAmount.ToString();
                //speVatAmount.Value = _Entity.VatAmount.ToString();
                //speWithHoldingTax.Value = _Entity.WithholdingTax.ToString();

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
                txtCostingSubmittedBy.Text = _Entity.CostingSubmittedBy;
                txtCostingSubmittedDate.Text = _Entity.CostingSubmittedDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;

                // 2016-01-07  TL  Customized titles
                if (Request.QueryString["transtype"].ToString() == "ACTAQT")
                {
                    FormTitle.Text = "Asset Acquisition";
                    frmlayout1.FindItemOrGroupByName("RRDetail").Caption = "Asset Acquisition Detail";
                }

                // 2016-01-07  TL  (End)

                if (Request.QueryString["editcosting"] == null && access1 == " ")
                {

                    this.gv1.Columns["PesoAmount"].Width = 0;
                    this.gv1.Columns["UnitCost"].Width = 0;
                    this.gv1.Columns["RoundingAmount"].Width = 0;
                    this.gv1.Columns["UnitFreight"].Width = 0;
                    this.gv1.Columns["IsVat"].Width = 0;
                    this.gv1.Columns["ATCCode"].Width = 0;
                    this.gv1.Columns["VATCode"].Width = 0;
                    frmlayout1.FindItemOrGroupByName("Costing").ClientVisible = false;
                    //  frmlayout1.FindItemOrGroupByName("Costing").Width = 0;



                }
                else
                {
                    //if (String.IsNullOrEmpty(glSupplierCode.Text))
                    //{

                    //    glPicklist.ClientEnabled = true;
                    //    Generatebtn.ClientVisible = true;
                    //}
                    //else
                    //{
                    //    glPicklist.ClientVisible = false;
                    //    Generatebtn.ClientVisible = false;
                    //    glSupplierCode.ClientEnabled = false;
                    //}
                    spePesoAmount.ReadOnly = true;
                    glPicklist.ClientEnabled = true;
                    Generatebtn.ClientVisible = true;
                }




                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    glPicklist.ClientEnabled = true;
                    Generatebtn.ClientVisible = true;
                    txtApVNumber.ReadOnly = true;
                    spePesoAmount.ReadOnly = false;
                    speExchangeRate.Text = "1";



                    //gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv1.Settings.VerticalScrollableHeight = 200;
                    //gv1.KeyFieldName = "LineNumber;PODocNumber";
                }
                else
                {

                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;
                }

                DataTable dtrr = _EntityDetail.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());
                if (dtrr.Rows.Count > 0)
                {
                    //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

                    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    gv1.DataSourceID = "odsDetail";

                    //Session["Datatable"] = "1";
                }

                DataTable dtcs = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());

                if (dtcs.Rows.Count > 0)
                {
                    aglWarehouseCode.ClientEnabled = false;
                    txtAuxiliaryReference.ClientEnabled = false;
                    glPicklist.ClientEnabled = false;
                    Generatebtn.ClientEnabled = false;

                    dtpDocDate.ClientEnabled = false;
                    glSupplierCode.ClientEnabled = false;
                    txtDrNumber.ClientEnabled = false;
                    chckComplimentary.ClientEnabled = false;
                    Session[Request.QueryString["docnumber"].ToString() + "Datatable"] = "1";

                }
                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Procurement.ReceivingReport where docnumber = '" + txtDocNumber.Text + "' and ISNULL(SupplierCode,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }
                //else
                //{
                //    gv1.DataSourceID = null;
                //}
                sdsPicklist.SelectParameters["IsAsset"].DefaultValue =
                (Request.QueryString["transtype"].ToString() == "ACTAQT") ? "1" : "0";
            }
            gvJournal.DataSourceID = "odsJournalEntry";



        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._Connection = Session["ConnString"].ToString();
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            string strresult = GearsProcurement.GProcurement.ReceivingReport_Validate(gparam);
            string strresult1 = GearsProcurement.GProcurement.ReceivingReport_CheckPOVSRECEIVED(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n" + strresult1;//Message variable to client side
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Table = "Procurement.ReceivingReport";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsAccounting.GAccounting.ReceivingReportEntry_Post(gparam);
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

            if (Request.QueryString["entry"].ToString() == "E" || Request.QueryString["editcosting"] != null)
            {

                //if (!String.IsNullOrEmpty(glSupplierCode.Text))
                //{
                //    glSupplierCode.DropDownButton.Enabled = false;
                //    glSupplierCode.ReadOnly = true;

                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "true")
                //{
                //Generatebtn.ClientEnabled = false;
                //glPicklist.ClientEnabled = false;
                //glPicklist.DropDownButton.Enabled = false;
                ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

                //}
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                //grid.SettingsBehavior.AllowGroup = false;
                //grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
            DataTable dtcs = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());


            if (dtcs.Rows.Count > 0)
            {
                if (e.Column.FieldName == "ReceivedQty" || e.Column.FieldName == "BulkQty" || e.Column.FieldName == "Unit")
                    e.Editor.ReadOnly = true;

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
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New)
                e.Visible = false;

            DataTable dtce = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());

            if (dtce.Rows.Count > 0)
            {
                //if (e.ButtonType == ColumnCommandButtonType.Delete || e.ButtonType == ColumnCommandButtonType.New)
                //    e.Visible = false;

                e.Visible = gv1.IsNewRowEditing;
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

            DataTable dtcs = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());


            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D" || dtcs.Rows.Count > 0)
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
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //if (Session["euny"] != null)
            if (Session[Request.QueryString["docnumber"].ToString() + "euny"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                //sdsItemDetail.FilterExpression = Session["euny"].ToString();
                sdsItemDetail.FilterExpression = Session[Request.QueryString["docnumber"].ToString() + "euny"].ToString();
                //Session["euny"] = null;
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

            if (e.Parameters.Contains("VATCode"))
            {
                DataTable vat = Gears.RetriveData2("SELECT DISTINCT ISNULL(Rate,0) AS Rate FROM Masterfile.Tax WHERE TCode = '" + itemcode + "'", Session["ConnString"].ToString());

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
            else if (e.Parameters.Contains("ATCCode"))
            {
                DataTable vat = Gears.RetriveData2("SELECT DISTINCT ISNULL(Rate,0) AS Rate FROM Masterfile.Atc WHERE ATCCode = '" + itemcode + "'", Session["ConnString"].ToString());

                if (vat.Rows.Count > 0)
                {
                    foreach (DataRow dt in vat.Rows)
                    {
                        codes = dt["Rate"].ToString();
                    }
                }

                itemlookup.JSProperties["cp_identifier"] = "ATC";
                itemlookup.JSProperties["cp_codes"] = Convert.ToDecimal(codes) + ";";
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate = dtpDocDate.Text;


            _Entity.APVNumber = txtApVNumber.Text;

            _Entity.SupplierCode = String.IsNullOrEmpty(glSupplierCode.Text) ? null : glSupplierCode.Value.ToString();
            _Entity.Remarks = memRemarks.Text;
            _Entity.ReferenceNumber = glPicklist.Text;
            _Entity.AuxilaryReference = txtAuxiliaryReference.Text;
            _Entity.DRNumber = txtDrNumber.Text;
            _Entity.SINumber = txtSINumber.Text;
            _Entity.Broker = txtBroker.Text;
            _Entity.Terms = txtTerms.Text;
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouseCode.Text) ? null : aglWarehouseCode.Value.ToString();
            _Entity.Currency = txtCurrency.Text;

            _Entity.ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(speExchangeRate.Value) ? "0" : speExchangeRate.Value);


            _Entity.TotalFreight = Convert.ToDecimal(Convert.IsDBNull(speTotalFreight.Value) ? "0" : speTotalFreight.Value);
            _Entity.ComplimentaryItem = Convert.ToBoolean(chckComplimentary.Value);


            _Entity.TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(speTotalQuantity.Value) ? "0" : speTotalQuantity.Value);
            _Entity.TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(speTotalBulk.Value) ? "0" : speTotalBulk.Value);

            _Entity.TotalAmount = Convert.ToDecimal(Convert.IsDBNull(spePesoAmount.Value) ? "0" : spePesoAmount.Value);
            _Entity.ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(speForeigntAmount.Value) ? "0" : speForeigntAmount.Value);
            _Entity.GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(speGrossVatableAmount.Value) ? "0" : speGrossVatableAmount.Value);
            _Entity.NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(speNonVatableAmount.Value) ? "0" : speNonVatableAmount.Value);
            _Entity.VatAmount = Convert.ToDecimal(Convert.IsDBNull(speVatAmount.Value) ? "0" : speVatAmount.Value);
            _Entity.WithholdingTax = Convert.ToDecimal(Convert.IsDBNull(speWithHoldingTax.Value) ? "0" : speWithHoldingTax.Value);

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

            //  _Entity.Transtype = Request.QueryString["transtype"].ToString();


            DataTable RRDetail = new DataTable();

            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                RRDetail.Columns.Add(dataColumn.FieldName);
            }

            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = RRDetail.Rows.Add();
                foreach (DataColumn col in RRDetail.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }



            switch (e.Parameter)
            {
                case "Add":
                    //gv1.UpdateEdit();


                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        //  _Entity.SubsiEntry(txtDocNumber.Text);

                        if (Session[Request.QueryString["docnumber"].ToString() + "Datatable"] == "1")
                        {
                            gv1.DataSource = RRDetail;


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
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session[Request.QueryString["docnumber"].ToString() + "Datatable"] = null;
                        gv1.DataSource = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Update":

                    //gv1.UpdateEdit();

                    _Entity.UpdateData(_Entity);//Method of inserting for header


                    if (error == false)
                    {
                        check = true;

                        if (Session[Request.QueryString["docnumber"].ToString() + "Datatable"] == "1")
                        {
                            DataTable dtce = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());

                            if (dtce.Rows.Count == 0)
                            {

                                Gears.RetriveData2("DELETE FROM Procurement.ReceivingReportdetailPO where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                                gv1.DataSource = RRDetail;


                                if (gv1.DataSourceID != null)
                                {
                                    gv1.DataSourceID = null;
                                }
                                gv1.DataBind();
                                gv1.UpdateEdit();
                            }
                            else
                            {
                                gv1.UpdateEdit();
                            }
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        // _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
                        DataTable dtpost = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());

                        if (dtpost.Rows.Count > 0)
                        {
                            Post();
                        }
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session[Request.QueryString["docnumber"].ToString() + "Datatable"] = null;

                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Delete":
                    //GetSelectedVal(); 
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

                case "Supplier":
                    DataTable getsupinfo = Gears.RetriveData2("SELECT SupplierCode,Currency,APTerms,Name FROM Masterfile.BPSupplierInfo WHERE SupplierCode = '" + glSupplierCode.Value + "'", Session["ConnString"].ToString());
                    if (getsupinfo.Rows.Count > 0)
                    {
                        foreach (DataRow dtrow in getsupinfo.Rows)
                        {
                            txtCurrency.Text = dtrow[1].ToString();

                            txtTerms.Text = dtrow[2].ToString();
                        }

                        //ASPxTextBox1.Text = biz[0]["SupplierCode"].To String();

                    }
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    glPicklist.Value = "";
                    txtAuxiliaryReference.Focus();

                    break;

                case "RR":

                    break;
                case "Generate":
                    GetSelectedVal();
                    // GetVat();
                    OtherDetail();
                    cp.JSProperties["cp_generated"] = true;

                    //glSupplierCode.Enabled = false;
                    //Generatebtn.Enabled = false;
                    break;

                case "customercode":
                    //    sdsPicklist.SelectParameters["customercode"].DefaultValue = aglCustomer.Text;
                    break;


                case "Cloned":
                    Session[Request.QueryString["docnumber"].ToString() + "Datatable"] = "1";
                    break;

            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";
            if (error == false && check == false)
            {
                foreach (GridViewColumn column in gv1.Columns)
                {
                    //GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    //if (dataColumn == null) continue;
                    //if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber")
                    //{
                    //    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                    //}
                }

                //Checking for non existing Codes..
                ItemCode = e.NewValues["ItemCode"].ToString();
                ColorCode = e.NewValues["ColorCode"].ToString();
                ClassCode = e.NewValues["ClassCode"].ToString();
                SizeCode = e.NewValues["SizeCode"].ToString();
                DataTable item = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());
                if (item.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
                }
                DataTable color = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'", Session["ConnString"].ToString());
                if (color.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
                }
                DataTable clss = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'", Session["ConnString"].ToString());
                if (clss.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
                }
                DataTable size = Gears.RetriveData2("SELECT * FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'", Session["ConnString"].ToString());
                if (size.Rows.Count < 1)
                {
                    AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
                }

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
            DataTable dtce = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());

            if (dtce.Rows.Count == 0)
            {

                if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
                {
                    e.Handled = true;
                    e.UpdateValues.Clear();
                }

                if (Session[Request.QueryString["docnumber"].ToString() + "Datatable"] == "1")
                {
                    e.Handled = true;
                    DataTable source = GetSelectedVal();
                    foreach (ASPxDataUpdateValues values in e.UpdateValues)
                    {
                        object[] keys = { values.NewValues["PODocNumber"], values.NewValues["LineNumber"] };
                        DataRow row = source.Rows.Find(keys);

                        row["ItemCode"] = values.NewValues["ItemCode"];
                        row["ColorCode"] = values.NewValues["ColorCode"];
                        row["ClassCode"] = values.NewValues["ClassCode"];
                        row["SizeCode"] = values.NewValues["SizeCode"];
                        row["POQty"] = values.NewValues["POQty"];
                        row["ReceivedQty"] = values.NewValues["ReceivedQty"];
                        row["Unit"] = values.NewValues["Unit"];
                        row["PesoAmount"] = values.NewValues["PesoAmount"];
                        row["UnitCost"] = values.NewValues["UnitCost"];
                        row["BulkQty"] = values.NewValues["BulkQty"];
                        row["RoundingAmt"] = values.NewValues["RoundingAmt"];
                        row["UnitFreight"] = values.NewValues["UnitFreight"];
                        row["ReturnedQty"] = values.NewValues["ReturnedQty"];
                        row["ReturnedBulkQty"] = values.NewValues["ReturnedBulkQty"];



                        row["IsVat"] = values.NewValues["IsVat"];
                        row["ATCCode"] = values.NewValues["ATCCode"];
                        row["VATCode"] = values.NewValues["VATCode"];
                        row["VATRate"] = values.NewValues["VATRate"];
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
                        row["ExpDate"] = values.NewValues["ExpDate"];
                        row["MfgDate"] = values.NewValues["MfgDate"];
                        row["BatchNo"] = values.NewValues["BatchNo"];
                        row["LotNo"] = values.NewValues["LotNo"];

                    }

                    // Removing all deleted rows from the data source(Excel file)
                    foreach (ASPxDataDeleteValues values in e.DeleteValues)
                    {
                        try
                        {
                            object[] keys = { values.Keys["PODocNumber"], values.Keys["LineNumber"] };
                            source.Rows.Remove(source.Rows.Find(keys));
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }


                    int ln = 1;
                    foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                    {
                        //var LineNumber = values.NewValues["LineNumber"];
                        var LineNumber = ln;
                        var PODocNumber = values.NewValues["PODocNumber"];
                        var PRNumber = values.NewValues["PRNumber"];
                        var ItemCode = values.NewValues["ItemCode"];
                        var FullDesc = values.NewValues["FullDesc"];
                        var ColorCode = values.NewValues["ColorCode"];
                        var ClassCode = values.NewValues["ClassCode"];
                        var SizeCode = values.NewValues["SizeCode"];
                        var POQty = values.NewValues["POQty"];
                        var ReceivedQty = values.NewValues["ReceivedQty"];
                        var Unit = values.NewValues["Unit"];
                        var PesoAmount = values.NewValues["PesoAmount"];
                        var UnitCost = values.NewValues["UnitCost"];
                        var BulkQty = values.NewValues["BulkQty"];
                        var RoundingAmt = values.NewValues["RoundingAmt"];
                        var UnitFreight = values.NewValues["UnitFreight"];
                        var ReturnedQty = values.NewValues["ReturnedQty"];
                        var ReturnedBulkQty = values.NewValues["ReturnedBulkQty"];
                        var ATCCode = values.NewValues["ATCCode"];
                        var IsVat = values.NewValues["IsVat"];

                        var VATCode = values.NewValues["VATCode"];
                        var VATRate = values.NewValues["VATRate"];
                        var ATCRate = values.NewValues["ATCRate"];



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
                        source.Rows.Add(txtDocNumber.Text, LineNumber, PODocNumber, PRNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, POQty, ReceivedQty, Unit, UnitCost, PesoAmount, BulkQty, RoundingAmt, UnitFreight, ReturnedQty, IsVat, VATCode, ReturnedBulkQty, ExpDate, MfgDate, BatchNo, LotNo, ATCCode, Field1, VATRate, Field2, ATCRate, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
                        ln++;
                    }

                    //Updating required rows


                    foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                    {
                        _EntityDetail.PODocNumber = dtRow["PODocNumber"].ToString();
                        _EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                        _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                        _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                        _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                        _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                        _EntityDetail.POQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["POQty"]) ? 0 : dtRow["POQty"]);
                        _EntityDetail.ReceivedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReceivedQty"]) ? 0 : dtRow["ReceivedQty"]);
                        _EntityDetail.Unit = dtRow["Unit"].ToString();
                        _EntityDetail.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                        _EntityDetail.PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                        _EntityDetail.BulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BulkQty"]) ? 0 : dtRow["BulkQty"]);
                        _EntityDetail.RoundingAmt = Convert.ToDecimal(Convert.IsDBNull(dtRow["RoundingAmt"]) ? 0 : dtRow["RoundingAmt"]);
                        _EntityDetail.UnitFreight = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFreight"]) ? 0 : dtRow["UnitFreight"]);
                        _EntityDetail.ReturnedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedQty"]) ? 0 : dtRow["ReturnedQty"]);
                        _EntityDetail.ReturnedBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedBulkQty"]) ? 0 : dtRow["ReturnedBulkQty"]);
                        _EntityDetail.IsVat = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVat"]) ? false : dtRow["IsVat"]);
                        _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                        _EntityDetail.ATCCode = dtRow["ATCCode"].ToString();
                        _EntityDetail.VATRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["VATRate"]) ? 0 : dtRow["VATRate"]);
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
                        _EntityDetail.ExpDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["ExpDate"]) ? null : dtRow["ExpDate"]);
                        _EntityDetail.MfgDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["MfgDate"]) ? null : dtRow["MfgDate"]);
                        _EntityDetail.BatchNo = dtRow["BatchNo"].ToString();
                        _EntityDetail.LotNo = dtRow["LotNo"].ToString();
                        _EntityDetail.AddReceivingReportDetail(_EntityDetail);
                    }
                }
            }
        }
        #endregion



        protected void aglcustomerinit(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
            //if (Session["melyn"] != null)
            if (Session[Request.QueryString["docnumber"].ToString() + "melyn"] != null)
            {
                gridLookup.GridView.DataSourceID = sdsPicklist.ID;
                //sdsPicklist.FilterExpression = Session["melyn"].ToString();
                sdsPicklist.FilterExpression = Session[Request.QueryString["docnumber"].ToString() + "melyn"].ToString();
            }
        }

        public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string whcode = e.Parameters;//Set column name
            //if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback


            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator(glSupplierCode.KeyFieldName, new string[] { glSupplierCode.Text });
            sdsPicklist.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["melyn"] = sdsPicklist.FilterExpression;
            Session[Request.QueryString["docnumber"].ToString() + "melyn"] = sdsPicklist.FilterExpression;
            grid.DataSourceID = sdsPicklist.ID;
            grid.DataBind();


        }
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["tina"] = null;
                Session[Request.QueryString["docnumber"].ToString() + "tina"] = null;
            }

            //if (Session["tina"] != null)
            if (Session[Request.QueryString["docnumber"].ToString() + "tina"] != null)
            {
                DataTable dtce = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());

                if (dtce.Rows.Count > 0)
                {

                    gv1.DataSource = sdsCostingDetail;
                    //sdsCostingDetail.FilterExpression = Session["tina"].ToString();
                    sdsCostingDetail.FilterExpression = Session[Request.QueryString["docnumber"].ToString() + "tina"].ToString();


                }
                else
                {
                    if (Request.QueryString["TransType"].ToString() == "PRCRCR")
                    {

                        gv1.DataSource = sdsPicklistDetail;
                        //sdsPicklistDetail.FilterExpression = Session["tina"].ToString();
                        sdsPicklistDetail.FilterExpression = Session[Request.QueryString["docnumber"].ToString() + "tina"].ToString();
                    }
                    else
                    {
                        gv1.DataSource = sdsPicklistDetail1;
                        //sdsPicklistDetail1.FilterExpression = Session["tina"].ToString();
                        sdsPicklistDetail1.FilterExpression = Session[Request.QueryString["docnumber"].ToString() + "tina"].ToString();
                    }
                    //gridview.DataSourceID = null;
                }
            }
        }

        private DataTable GetSelectedVal()
        {
            gv1.DataSource = null;
            DataTable dt = new DataTable();
            string[] selectedValues = glPicklist.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(glPicklist.KeyFieldName, selectedValues);
            DataTable dtce = _EntityDetail.SubmitWH(txtDocNumber.Text, Session["ConnString"].ToString());

            if (dtce.Rows.Count > 0)
            {

                sdsCostingDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                //Session["tina"] = sdsCostingDetail.FilterExpression;
                Session[Request.QueryString["docnumber"].ToString() + "tina"] = sdsCostingDetail.FilterExpression;
                gv1.DataSource = sdsCostingDetail;
            }
            else
            {
                if (Request.QueryString["TransType"].ToString() == "PRCRCR")
                {

                    sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                    //Session["tina"] = sdsPicklistDetail.FilterExpression;
                    Session[Request.QueryString["docnumber"].ToString() + "tina"] = sdsPicklistDetail.FilterExpression;
                    gv1.DataSource = sdsPicklistDetail;
                }
                else
                {
                    sdsPicklistDetail1.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                    //Session["tina"] = sdsPicklistDetail1.FilterExpression;
                    Session[Request.QueryString["docnumber"].ToString() + "tina"] = sdsPicklistDetail1.FilterExpression;
                    gv1.DataSource = sdsPicklistDetail1;

                }

            }



            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session[Request.QueryString["docnumber"].ToString() + "Datatable"] = "1";

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
            dt.Columns["PODocNumber"],dt.Columns["LineNumber"]};



            return dt;
        }
        private void GetVat()
        {
            DataTable getvat = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                       "on A.TaxCode = B.TCode " +
                                                       "where SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());

            if (getvat.Rows.Count > 0)
            {
                cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
            }

            else
            {
                cp.JSProperties["cp_vatrate"] = "0";
            }
            DataTable getatc = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.ATC B " +
                                          "on A.ATCCode = B.ATCCode " +
                                          "where SupplierCode = '" + glSupplierCode.Text + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());

            if (getatc.Rows.Count > 0)
            {
                cp.JSProperties["cp_atc"] = getatc.Rows[0]["Rate"].ToString();
            }

            else
            {
                cp.JSProperties["cp_atc"] = "0";
            }


        }
        private void OtherDetail()
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

            DataTable ret = Gears.RetriveData2("SELECT Broker, ReceivingWarehouse,Terms,ExchangeRate FROM Procurement.PurchaseOrder WHERE DocNumber = '" + refpick.Trim() + "'", Session["ConnString"].ToString());

            DataRow _ret = ret.Rows[0];
            if (String.IsNullOrEmpty(txtBroker.Text))
            {
                _Entity.Broker = _ret["Broker"].ToString();
                txtBroker.Text = _ret["Broker"].ToString();
            }
            if (String.IsNullOrEmpty(txtTerms.Text))
            {
                _Entity.Terms = _ret["Terms"].ToString();
                txtTerms.Text = _ret["Terms"].ToString();
            }

            if (String.IsNullOrEmpty(aglWarehouseCode.Text))
            {
                _Entity.WarehouseCode = _ret["ReceivingWarehouse"].ToString();
                aglWarehouseCode.Value = _ret["ReceivingWarehouse"].ToString();
            }

            if (_ret["ExchangeRate"] != null)
            {
                _Entity.ExchangeRate = Convert.ToDecimal(_ret["ExchangeRate"]);
                speExchangeRate.Text = _ret["ExchangeRate"].ToString();
            }

            string[] selectedValues = glPicklist.Text.Split(';');
            string joinval = "('" + string.Join("','", selectedValues) + "')";
            DataTable getCur = Gears.RetriveData2("Select Currency from Procurement.PurchaseOrder where DocNumber in " + joinval + "", Session["ConnString"].ToString());
            if (getCur.Rows.Count > 1)
            {
                txtCurrency.Text = "PHP";
            }
            else
            {
                DataRow _getCur = getCur.Rows[0];
                txtCurrency.Text = _getCur["Currency"].ToString();
            }
        }
        public void glAtcCode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string bizpartner = e.Parameters;
            if (bizpartner.Contains("GLP_AIC") || bizpartner.Contains("GLP_AC") || bizpartner.Contains("GLP_F")) return;//Traps the callback


            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("SupplierCode", new string[] { bizpartner });

            sdsATC.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["glene"] = sdsATC.FilterExpression;
            Session[Request.QueryString["docnumber"].ToString() + "glene"] = sdsATC.FilterExpression;
            grid.DataSourceID = sdsATC.ID;
            grid.DataBind();


        }
        protected void glAtcCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glAtcCode_CustomCallback);
            if (Session["atccode"] != null)
            {
                gridLookup.GridView.DataSourceID = sdsATC.ID;
                //sdsATC.FilterExpression = Session["glene"].ToString();
                sdsATC.FilterExpression = Session[Request.QueryString["docnumber"].ToString() + "glene"].ToString();
            }
        }

        protected void aglCustomer_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void aglCustomer_TextChanged(object sender, EventArgs e)
        {
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

        protected void sdsWarehouse_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsPicklistDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsCostingDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsSupplier.ConnectionString = Session["ConnString"].ToString();
            //sdsWarehouse.ConnectionString = Session["ConnString"].ToString();
            //sdsPicklist.ConnectionString = Session["ConnString"].ToString();
            //sdsPicklistDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsATC.ConnectionString = Session["ConnString"].ToString();
            //VatCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //sdsPicklistDetail1.ConnectionString = Session["ConnString"].ToString();
            //Unitlookup.ConnectionString = Session["ConnString"].ToString();
        }
    }
}