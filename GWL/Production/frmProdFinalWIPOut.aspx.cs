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

namespace GWL
{
    public partial class frmProdFinalWIPOut : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        private static string strError;
        Entity.FinalWIPOUT _Entity = new FinalWIPOUT();//Calls entity odsHeader
        Entity.FinalWIPOUT.WOSizeBreakDown _EntityDetail = new FinalWIPOUT.WOSizeBreakDown();//Call entity sdsDetail
        Entity.FinalWIPOUT.WOClassBreakDown _EntityClassBreakdown = new FinalWIPOUT.WOClassBreakDown();
     
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());


            if (!string.IsNullOrEmpty(glserviceorder.Text))
            {
                Session["FilterItemCode"] = glserviceorder.Text;
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

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
            }

            if (!IsPostBack)
            {

       
                   Session["stuffen"] = null;
    

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                    
                        updateBtn.Text = "Add";
                        glserviceorder.Visible = true;
                        break;

                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;

                    case "V":
                        sdsServiceOrder.SelectCommand = "SELECT A.DocNumber,StepCode,WorkCenter FROM (SELECT DISTINCT A.DocNumber,MAX(Sequence) as Sequence FROM Production.JobOrder A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber group by A.DocNumber) as A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber and A.Sequence = B.Sequence where   ISNULL(PreProd,0)=0";
                        sdsServiceOrder.DataBind();
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false; 
                        break;

                    case "D":
                        sdsServiceOrder.SelectCommand = "SELECT A.DocNumber,StepCode,WorkCenter FROM (SELECT DISTINCT A.DocNumber,MAX(Sequence) as Sequence FROM Production.JobOrder A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber group by A.DocNumber) as A INNER JOIN Production.JOStepPlanning B ON A.DocNumber = B.DocNumber and A.Sequence = B.Sequence where   ISNULL(PreProd,0)=0";
                        sdsServiceOrder.DataBind();
                        view = true;
                        updateBtn.Text = "Delete";
                        break;

                    case "O":
                        txtDrNumber.ClientEnabled = true;
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Override";
                        glcheck.ClientVisible = false; 
                        break;
                }




                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                cmbType.Text = _Entity.Type.ToString();
                glAdjustment.Text = _Entity.AdjustmentClass.ToString();
                aglwarehousecode.Value = _Entity.Warehouse.ToString();
                txtDrNumber.Text = _Entity.DRDocnumber.ToString();
                glserviceorder.Text = _Entity.JobOrder.ToString();
                txtStep.Text = _Entity.Step.ToString();
                txtWorkCenter.Text = _Entity.WorkCenter.ToString();
                glProductCode.Value = _Entity.ProductCode.ToString();
                glProductColor.Value = _Entity.ProductColor.ToString();
                chkIsWithDR.Value = _Entity.ClassA;
                chkIsAutoCharge.Value = _Entity.AutoCharge;
                speTotalQuantity.Value = _Entity.TotalQuantity.ToString();
                txtRRNumber.Text = _Entity.RRDocnumber;
                memRemarks.Value = _Entity.Remarks;
                txtOverhead.Text = _Entity.OverheadCode;

                txtVatCode.Text = _Entity.VatCode.ToString();
                txtCurrency.Text = _Entity.Currency;
                speTotalQuantity.Value = _Entity.TotalQuantity;
                speWOP.Value = _Entity.WorkOrderPrice;
                speOrigWOP.Value = _Entity.OriginalWorkOrderPrice;
                speExchangeRate.Value = _Entity.ExchangeRate;
                speVatAmount.Value = _Entity.VatAmount;
                spePesoAmount.Value = _Entity.PesoAmount;
                speForeignAmount.Value = _Entity.ForeignAmount;
                speGrossVatableAmount.Value = _Entity.GrossVatableAmount;
                speNonVatableAmount.Value = _Entity.NonVatableAmount;
                speWithHoldingTax.Value = _Entity.WTaxAmount;
                speVatRate.Value = _Entity.VatRate;
                speAtc.Value = _Entity.AtcRate;

                glStatus.Text = _Entity.Status;
                glDisposition.Text = _Entity.Disposition;

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

                if (String.IsNullOrEmpty(cmbType.Text)) { 
                    cmbType.Text = "Normal Out";
                }

                if (updateBtn.Text == "Add")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    glserviceorder.ClientEnabled = true;

                    gvclass.DataSourceID = "sdsClass";
                    speExchangeRate.Value = "1.00";
                    //cmbType.Text = "Normal Out";
                    chkIsWithDR.CheckState = CheckState.Checked;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
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
                DataTable dtsize = _EntityDetail.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());

                DataTable dtclass = _EntityClassBreakdown.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());
                if (dtclass.Rows.Count > 0)
                {
                    //odsDetail1.SelectParameters["EntityCode"].DefaultValue = txtDocNumber.Text;
                    gvclass.DataSourceID = "odsClass";
                }
                else
                {
                    gvclass.DataSourceID = "sdsClass";
                }
                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Production.FinalWIPOUT where docnumber = '" + txtDocNumber.Text + "' and ISNULL(JobOrder,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }

                if (dtsize.Rows.Count > 0)
                {
                    //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                }

                gvJournal.DataSourceID = "odsJournalEntry";
                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
 
            }

            if (cmbType.Text == "Normal Out")
            {
                glAdjustment.ClientEnabled = false; 
                speTotalQuantity.ClientEnabled = false; 
                speWOP.ClientEnabled = false;
            }
            else
            {
                glAdjustment.ClientEnabled = true;
                speTotalQuantity.ClientEnabled = true; 
                chkIsWithDR.CheckState = CheckState.Unchecked; 
                speWOP.ClientEnabled = true; 
            }

        }
        #endregion

        #region Validation
		string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRDFWT";
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.FinalWIPOut_Validate(gparam);
        
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n";//Message variable to client side
            }
        }
        #endregion
        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString().Trim();
            gparam._Table = "Production.FinalWipOut";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.ProdFinalWIPOut_Post(gparam);
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
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
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
                //glserviceorder.ClientEnabled = false;
                //glserviceorder.DropDownButton.Enabled = false;
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
        protected void lookup_Init(object sender, EventArgs e)
        {
            //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //if (Session["FilterExpression"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = "sdsItemDetail";
            //    sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
            //    //Session["FilterExpression"] = null;
            //}
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string column = e.Parameters.Split('|')[0];//Set column name
            //if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            //string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            //string val = e.Parameters.Split('|')[2];//Set column value
            //if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            //ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
            //sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["FilterExpression"] = sdsItemDetail.FilterExpression;
            //grid.DataSourceID = "sdsItemDetail";
            //grid.DataBind();

            //for (int i = 0; i < grid.VisibleRowCount; i++)
            //{
            //    if (grid.GetRowValues(i, column) != null)
            //        if (grid.GetRowValues(i, column).ToString() == val)
            //        {
            //            grid.Selection.SelectRow(i);
            //            string key = grid.GetRowValues(i, column).ToString();
            //            grid.MakeRowVisible(key);
            //            break;
            //        }
            //}
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
                _Entity.Connection = Session["ConnString"].ToString(); 
                _Entity.DocNumber = txtDocNumber.Value.ToString();
                _Entity.DocDate = dtpDocDate.Text;
                _Entity.Type = cmbType.Text;
                _Entity.AdjustmentClass = glAdjustment.Text;
                _Entity.Step = txtStep.Text;
                _Entity.JobOrder = glserviceorder.Text;
                _Entity.ProductCode = glProductCode.Text;
                _Entity.ProductColor = glProductColor.Text;
                _Entity.Warehouse = aglwarehousecode.Text;
                _Entity.ClassA = Convert.ToBoolean(chkIsWithDR.Value);
                _Entity.AutoCharge = Convert.ToBoolean(chkIsAutoCharge.Value);
                _Entity.WorkCenter = txtWorkCenter.Text;
                _Entity.DRDocnumber = txtDrNumber.Text;
                _Entity.RRDocnumber = txtRRNumber.Text;
                _Entity.Remarks = memRemarks.Text;
                _Entity.OverheadCode = txtOverhead.Text;
                _Entity.VatCode = txtVatCode.Text;
                _Entity.Currency = txtCurrency.Text;
                _Entity.TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(speTotalQuantity.Value) ? 0 : Convert.ToDecimal(speTotalQuantity.Value));
                _Entity.WorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(speWOP.Value) ? 0 : Convert.ToDecimal(speWOP.Value));
                _Entity.ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(speExchangeRate.Value) ? 0 : Convert.ToDecimal(speExchangeRate.Value));
                _Entity.VatAmount = Convert.ToDecimal(Convert.IsDBNull(speVatAmount.Value) ? 0 : Convert.ToDecimal(speVatAmount.Value));
                _Entity.PesoAmount = Convert.ToDecimal(Convert.IsDBNull(spePesoAmount.Value) ? 0 : Convert.ToDecimal(spePesoAmount.Value));
                _Entity.ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(speForeignAmount.Value) ? 0 : Convert.ToDecimal(speForeignAmount.Value));
                _Entity.GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(speGrossVatableAmount.Value) ? 0 : Convert.ToDecimal(speGrossVatableAmount.Value));
                _Entity.NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(speNonVatableAmount.Value) ? 0 : Convert.ToDecimal(speNonVatableAmount.Value));
                _Entity.WTaxAmount = Convert.ToDecimal(Convert.IsDBNull(speWithHoldingTax.Value) ? 0 : Convert.ToDecimal(speWithHoldingTax.Value));
                _Entity.VatRate = Convert.ToDecimal(Convert.IsDBNull(speVatRate.Value) ? 0 : Convert.ToDecimal(speVatRate.Value));
                _Entity.AtcRate = Convert.ToDecimal(Convert.IsDBNull(speAtc.Value) ? 0 : Convert.ToDecimal(speAtc.Value));
                _Entity.OriginalWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(speOrigWOP.Value) ? 0 : Convert.ToDecimal(speOrigWOP.Value));
                _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
                _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
                _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
                _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
                _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
                _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
                _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
                _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
                _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

                _Entity.Disposition = glDisposition.Text;
                _Entity.Status = glStatus.Text;
     
              //  _Entity.Transtype = Request.QueryString["transtype"].ToString();

                string param = e.Parameter.Split('|')[0]; //Renats
                switch (param)
            {

                case "Override":
                    _Entity.OverrideData(_Entity);
                    cp.JSProperties["cp_message"] = "Override DR Successfull!";
                    cp.JSProperties["cp_success"] = true;
                    cp.JSProperties["cp_close"] = true;
                    break;

                case "Add":
                        
                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsServiceOrderdetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        if (Convert.ToBoolean(chkIsWithDR.Value) == false)
                        {
                            //Gears.RetriveData2("DELETE FROM Production.FinalWOClassBreakdown where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                            Gears.RetriveData2("DELETE FROM Production.FinalWOSizeBreakDown where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                            gvclass.DataSourceID = "odsClass";//Renew datasourceID to entity
                            odsClass.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gvclass.UpdateEdit();
                        }
                        // _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate(); Post();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
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
             _Entity.LastEditedBy = Session["userid"].ToString();
           
            _Entity.LastEditedDate = DateTime.Now.ToString();
                          strError = Functions.Submitted(_Entity.DocNumber, "Production.FinalWIPOut", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                            gv1.DataSourceID = sdsServiceOrderdetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        if (Convert.ToBoolean(chkIsWithDR.Value) == false)
                        {

                            

                            //Gears.RetriveData2("DELETE FROM Production.FinalWOClassBreakdown where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                            Gears.RetriveData2("DELETE FROM Production.FinalWOSizeBreakDown where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                            gvclass.DataSourceID = "odsClass";//Renew datasourceID to entity
                            odsClass.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gvclass.UpdateEdit();
                        }
                       // _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate(); Post();
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


                case "isclassatrue":
                    Session["isclassa"] = "1";
                    Session["Datatable"] = "0";
                   
                    cp.JSProperties["cp_isclassa"] = "1";

                          gv1.DataSourceID = null;
                          gv1.DataBind();


                          gvclass.DataSourceID = sdsClass.ID;
                    gvclass.DataBind();
                    GetSelectedVal();
                    GetVat();
                    cp.JSProperties["cp_generated"] = true;
 
                    break;

    

                case "isclassafalse":
                    Session["isclassa"] = "0";
                    Session["Datatable"] = "0";
                    //gv1.DataSourceID = null;
                    //gvclass.DataSourceID = null;
                     GetVat();
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    gvclass.DataSourceID = sdsClass.ID;
                    gvclass.DataBind();
             
                    cp.JSProperties["cp_isclassa"] = "0";
               
                     cp.JSProperties["cp_generated"] = true;

                    break;


                case "JO":

                    txtWorkCenter.Text = e.Parameter.Split('|')[3];
                    txtStep.Text = e.Parameter.Split('|')[2];


                   
                 DataTable dttable = Gears.RetriveData2("SELECT DISTINCT A.DocNumber,B.StepCode,WorkCenter,Itemcode AS ProductCode "
                     + "   , Colorcode as ProductColor, DocDate, DueDate, LeadTime, CustomerCode, TotalJOQty,B.Overhead "
                     + " , TotalINQty, TotalFinalQty,WorkOrderPrice,VAT,ISNULL(VATRate,0) as VATRate,D.ATCCode,ISNULL(Rate,0) as Rate,D.Currency ,Remarks FROM Production.JobOrder A  "
                     + " inner join Production.JOStepPlanning B "
                     + " on A.DocNumber = B.DocNumber "
                     + " and A.DocNumber = '" + glserviceorder.Text + "' "
                     + " inner join Production.JOProductOrder C "
                     + " on A.DocNumber = C.DocNumber "
         
                     + " inner join Masterfile.BPSupplierInfo D "
                     + " on B.WorkCenter = D.SupplierCode "
                     + " left join Masterfile.ATC E "
                     + " on D.ATCCode = E.ATCCode "
                     + "  WHERE Status IN ('N','W') "
                     + " and ISNULL(PreProd,0)='0'  and A.DocNumber = '" + glserviceorder.Text + "' AND StepCode = '" + txtStep.Text + "'", Session["ConnString"].ToString());


                   
                    if (dttable.Rows.Count > 0)
                    {
                        txtStep.Text = dttable.Rows[0][1].ToString();
                        txtWorkCenter.Text = dttable.Rows[0][2].ToString();
                        speWOP.Value = cmbType.Text.ToUpper() == "ADJUSTMENT" ? "0" : dttable.Rows[0]["WorkOrderPrice"].ToString();
                         speOrigWOP.Value = dttable.Rows[0]["WorkOrderPrice"].ToString();
                        txtVatCode.Text = dttable.Rows[0]["VAT"].ToString();
                        speVatRate.Text = dttable.Rows[0]["VATRate"].ToString();
                        txtCurrency.Text = dttable.Rows[0]["Currency"].ToString();
                        txtAtcCode.Text = dttable.Rows[0]["ATCCode"].ToString();
                        txtOverhead.Text = dttable.Rows[0]["Overhead"].ToString();
                        speAtc.Text = dttable.Rows[0]["Rate"].ToString();
                     
                        DataTable dtproductcode = Gears.RetriveData2("SELECT Itemcode,Count(Itemcode) "
                               + " FROM Production.JobOrder A  "
                               + " inner join Production.JOStepPlanning B "
                               + " on A.DocNumber = B.DocNumber "
                               + " inner join Production.JOProductOrder C "
                               + " on A.DocNumber = C.DocNumber "
                               + "  WHERE Status IN ('N','W') "
                               + " and ISNULL(PreProd,0)='0'  and A.DocNumber = '" +  glserviceorder.Text + "'   GROUP BY Itemcode HAVING COUNT(Itemcode)>1", Session["ConnString"].ToString());


                        if (dtproductcode.Rows.Count == 1)
                        {
                            glProductCode.Text = dttable.Rows[0]["ProductCode"].ToString();
                        }

                        DataTable dtproductcolor = Gears.RetriveData2("SELECT Colorcode,Count(Colorcode) "
                               + "  FROM Production.JobOrder A  "
                               + " inner join Production.JOStepPlanning B "
                               + " on A.DocNumber = B.DocNumber "
                               + " inner join Production.JOProductOrder C "
                               + " on A.DocNumber = C.DocNumber "
                               + "  WHERE Status IN ('N','W')  "
                               + " and ISNULL(PreProd,0)='0'  and A.DocNumber = '" +  glserviceorder.Text + "'  GROUP BY Colorcode HAVING COUNT(Colorcode)>1", Session["ConnString"].ToString());

                        if (dtproductcolor.Rows.Count == 1)
                        {
                            glProductColor.Text = dttable.Rows[0]["ProductColor"].ToString();
                        }
                    
                    }
                  //  GetVat();
                    OtherDetail();
                    GetSelectedVal();
                   //glSupplierCode.Enabled = false;
                    //Generatebtn.Enabled = false;
                    break;

                case "Type": 
                    if (cmbType.Text == "Normal Out")
                    {
                        glAdjustment.ClientEnabled = false;
                        glAdjustment.Value = null;
                        speTotalQuantity.ClientEnabled = false; 
                        speWOP.ClientEnabled = false;
                    }
                    else
                    {
                        glAdjustment.ClientEnabled = true;
                        speTotalQuantity.ClientEnabled = true;
                        speTotalQuantity.Value = 0.00;
                        chkIsWithDR.CheckState = CheckState.Unchecked; 
                        speWOP.ClientEnabled = true;
                        speWOP.Value = 0.00;
                    }
                    break;

            }
        }
        private void GetVat()
        {

            DataTable getvat = Gears.RetriveData2("select ISNULL(Rate,0) as Rate  from Masterfile.BPSupplierInfo A inner join Masterfile.Tax B " +
                                                       "on A.TaxCode = B.TCode " +
                                                       "where SupplierCode = '" + txtWorkCenter.Text + "'", Session["ConnString"].ToString());

            if (getvat.Rows.Count > 0)
            {
                speVatRate.Text = getvat.Rows[0]["Rate"].ToString();
            }

            else
            {
                speVatRate.Text = "0";
            }
            DataTable getatc = Gears.RetriveData2("select Rate from Masterfile.BPSupplierInfo A inner join Masterfile.ATC B " +
                                          "on A.ATCCode = B.ATCCode " +
                                          "where SupplierCode = '" + txtWorkCenter.Text + "' and IsWithholdingTaxAgent ='1'", Session["ConnString"].ToString());
            
            if (getatc.Rows.Count > 0)
            {
                speAtc.Text = getatc.Rows[0]["Rate"].ToString();
            }

            else
            {
                speAtc.Text = "0";
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
            //        //GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //        //if (dataColumn == null) continue;
            //        //if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber")
            //        //{
            //        //    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //        //}
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

            //    if (e.Errors.Count > 0)
            //    {
            //        error = true; //bool to cancel adding/updating if true
            //    }
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
                        object[] keys = { values.Keys[0]};
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                //foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                //{
                //    var SizeCode = values.NewValues["SizeCode"];
                //    var Qty = values.NewValues["Qty"];
                //    var SVOBreakdown = values.NewValues["SVOBreakdown"];
               

                    
                //    var Field1 = values.NewValues["Field1"];
                //    var Field2 = values.NewValues["Field2"];
                //    var Field3 = values.NewValues["Field3"];
                //    var Field4 = values.NewValues["Field4"];
                //    var Field5 = values.NewValues["Field5"];
                //    var Field6 = values.NewValues["Field6"];
                //    var Field7 = values.NewValues["Field7"];
                //    var Field8 = values.NewValues["Field8"];
                //    var Field9 = values.NewValues["Field9"];
                //    source.Rows.Add(SizeCode, Qty, SVOBreakdown, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
                //}

                 //Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {

                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);

                        row["SizeCode"] = values.NewValues["SizeCode"];
                        row["ClassCodes"] = values.NewValues["ClassCodes"];
                        row["Qty"] = values.NewValues["Qty"];
                        row["BulkQty"] = values.NewValues["BulkQty"];
                        row["JOBreakdown"] = values.NewValues["JOBreakdown"];
                    
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
                Gears.RetriveData2("DELETE FROM Production.FinalWOSizeBreakdown where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.ClassCodes = dtRow["ClassCodes"].ToString(); 
                    _EntityDetail.Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                    _EntityDetail.BulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BulkQty"]) ? 0 : dtRow["BulkQty"]);
                    _EntityDetail.JOBreakdown = Convert.ToDecimal(Convert.IsDBNull(dtRow["JOBreakdown"]) ? 0 : dtRow["JOBreakdown"]);

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
                    _EntityDetail.AddWOSizeBreakDown(_EntityDetail);
                }
            }
        }
        #endregion

     



      
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                   Session["stuffen"] = null;
            }

            if (   Session["stuffen"] != null)
            {
                gv1.DataSourceID = sdsServiceOrderdetail.ID;
                sdsServiceOrderdetail.FilterExpression =    Session["stuffen"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();

            if (chkIsWithDR.Value.ToString() == "True")
            {
                gv1.DataSourceID = null;
                gv1.DataBind();
                gv1.CancelEdit();
                string[] selectedValues = glserviceorder.Text.Split(';');
               // CriteriaOperator selectionCriteria = new InOperator(glserviceorder.KeyFieldName, selectedValues);
                CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
                sdsServiceOrderdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                   Session["stuffen"] = sdsServiceOrderdetail.FilterExpression;
                gv1.DataSourceID = sdsServiceOrderdetail.ID;

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

                //glserviceorder.ClientEnabled = false;
            }
            else
            {
                gv1.DataSourceID = null;
                gv1.DataBind();
                gvclass.DataSourceID = sdsClass.ID;
                gvclass.DataBind();
             
            }
                return dt;
          
        }

        private void OtherDetail()
        {
            string pick = glserviceorder.Text;


            DataTable ret = Gears.RetriveData2("SELECT  WorkCenter,A.WorkOrderPrice,A.VAT,Currency FROM Production.JOStepPlanning A inner join Masterfile.BPSupplierInfo  B on A.WorkCenter = B.SupplierCode WHERE DocNumber = '" + pick + "' AND A.WorkCenter ='"+txtWorkCenter.Text+"' and StepCode = '" + txtStep.Text + "'", Session["ConnString"].ToString());



    
            if (ret.Rows.Count > 0)
            {
               // txtWorkCenter.Text = ret.Rows[0]["WorkCenter"].ToString();
                speWOP.Text = cmbType.Text.ToUpper() == "ADJUSTMENT" ? "0" : Convert.ToDecimal(ret.Rows[0]["WorkOrderPrice"]).ToString();

                txtVatCode.Text = ret.Rows[0]["VAT"].ToString();
                txtCurrency.Text = ret.Rows[0]["Currency"].ToString(); 

              
            }
            else
            {
                txtCurrency.Text = null;
             //   txtWorkCenter.Text = null;
                txtVatCode.Text = null;
                speWOP.Text = "0.00";
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
            foreach (Control a in this.Controls)
            {
                if (a is SqlDataSource)
                {

                    ((SqlDataSource)a).ConnectionString = Session["ConnString"].ToString();
                }


            }
              
        }
        protected void glclass_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback2);
            FilterSize();
            gridLookup.DataBind();
        }

        public void FilterSize()
        {
            if (Session["FilterItemCode"] == null)
            {
                Session["FilterItemCode"] = "";
            }
            DataTable dtItemCode = Gears.RetriveData2("select ItemCode from Production.JOProductOrder where DocNumber ='" + Session["FilterItemCode"] + "'", Session["ConnString"].ToString());

            if (dtItemCode.Rows.Count > 0)
            {
                MasterfileClass.SelectCommand = "SELECT  [ClassCode], [ItemCode],  [SizeCode] , [ColorCode] FROM Masterfile.[ItemDetail] where isnull(IsInactive,0)=0 and ItemCode = '" + dtItemCode.Rows[0]["ItemCode"].ToString() + "' ORDER BY ItemCode ASC";
            }

        }
        public void gridView_CustomCallback2(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterSize();
            grid.DataSourceID = "MasterfileClass";
            grid.DataBind();
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView1_CustomCallback);

        }

        public void gridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];
            string itemcode1 = e.Parameters.Split('|')[2];
            string val = e.Parameters.Split('|')[2];
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;

            var itemlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("ClassCode"))
            {

                itemlookup.JSProperties["cp_codes"] = itemcode1;
            }
        }

        protected void gvclass_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
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


            if (Request.QueryString["entry"] == "N")
            {

                if (Session["isclassa"] == "1")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New )
                    {
                        e.Visible = false;
                    }
                }
                if (Session["isclassa"] == "0")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }
                }
            }


            if (Request.QueryString["entry"] == "E")
            {
                if (Request.QueryString["iswithdetail"].ToString() == "true" && Session["isclassa"] == "0" || chkIsWithDR.Value.ToString() == "False")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }
                }

                if (Request.QueryString["iswithdetail"].ToString() == "false" && Session["isclassa"] == "0" || chkIsWithDR.Value.ToString() == "False")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }

                }

                if (Request.QueryString["iswithdetail"].ToString() == "false" && Session["isclassa"] == "1" || chkIsWithDR.Value.ToString() == "True")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }

                }

                if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["isclassa"] == "1" || chkIsWithDR.Value.ToString() == "True"))
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;

                    }
                }
            }


            if (Request.QueryString["entry"] == "V")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }
            
      
        }

        protected void glProductCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glProductCode_CustomCallback);
            if (Session["ProductCode"] != null)
            {
                gridLookup.GridView.DataSourceID = sdsProductCode.ID;
                sdsProductCode.FilterExpression = Session["ProductCode"].ToString();
            }
        }

        public void glProductCode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string whcode = e.Parameters;//Set column name
            //if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback


            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", new string[] { glserviceorder.Text });
            sdsProductCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["ProductCode"] = sdsProductCode.FilterExpression;
            grid.DataSourceID = sdsProductCode.ID;
            grid.DataBind();


        }

        protected void glProductColor_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glProductColor_CustomCallback);
            if (Session["ProductColor"] != null)
            {
                gridLookup.GridView.DataSourceID = sdsProductColor.ID;
                sdsProductColor.FilterExpression = Session["ProductColor"].ToString();
            }
        }

        public void glProductColor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string whcode = e.Parameters;//Set column name
            //if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback


            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", new string[] { glserviceorder.Text });
            CriteriaOperator selectionCriteria1 = new InOperator("ProductCode", new string[] { glProductCode.Text });
            sdsProductColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, selectionCriteria1)).ToString();
            Session["ProductColor"] = sdsProductColor.FilterExpression;
            grid.DataSourceID = sdsProductColor.ID;
            grid.DataBind();


        }
        protected void SizeCodes_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glSizeCodes_CustomCallback);
            if (Session["SizeCodes"] != null)
            {

                gridLookup.GridView.DataSourceID = SizeJO.ID;
                SizeJO.FilterExpression = Session["SizeCodes"].ToString();
            }
        }

        public void glSizeCodes_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {


            //   if (ocn.Contains("GLP_AIC") || ocn.Contains("GLP_AC") || ocn.Contains("GLP_F")) return;//Traps the callback





            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", new string[] { glserviceorder.Text });
            SizeJO.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();

            Session["SizeCodes"] = SizeJO.FilterExpression;
            grid.DataSourceID = SizeJO.ID;
            grid.DataBind();
        }

        protected void glClass1_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glClass_CustomCallback);
            if (Session["glClass"] != null)
            {

                gridLookup.GridView.DataSourceID = "MasterfileClass";
              
                gridLookup.DataBind();
            }
        }

        public void glClass_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;

            if (!String.IsNullOrEmpty(e.Parameters))
            {
                string val = e.Parameters.Split('|')[1];//Set column value
                if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback


                ASPxGridLookup lookup = (ASPxGridLookup)gvclass.FindEditRowCellTemplateControl((GridViewDataTextColumn)gvclass.Columns[3], "glClassCode");


                ASPxGridView grid = sender as ASPxGridView;



                MasterfileClass.SelectCommand = "SELECT  ClassCode,Description,StepCode  FROM Masterfile.[Class] where isnull(IsInactive,0)=0  AND StepCode  like '%" + txtStep.Text.TrimEnd() + "%' ";

                Session["glClass"] = MasterfileClass;
                grid.DataSourceID = "MasterfileClass";
                grid.DataBind();

                if (val != "")
                {
                    for (int i = 0; i < grid.VisibleRowCount; i++)
                    {
                        if (grid.GetRowValues(i, "ClassCode") != null)
                            if (grid.GetRowValues(i, "ClassCode").ToString() == e.Parameters)
                            {
                                grid.Selection.SelectRow(i);
                                string key = grid.GetRowValues(i, "ClassCode").ToString();
                                grid.MakeRowVisible(key);
                                break;
                            }
                    }
                }
                //if (val == "")
                //{
                //    lookup.Value = null;
                //}
            }
        }

        
        
    }
}