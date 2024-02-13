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
    public partial class frmContract : System.Web.UI.Page
    {
        Boolean error = false;
        Boolean view = false;
        Boolean check = false;

        private static string Connection;

        Entity.Contract _Entity = new Contract();
        Entity.Contract.ContractDetail _EntityDetail = new Contract.ContractDetail();
        Entity.Contract.ContractDetailNon _EntityDetailNon = new Contract.ContractDetailNon();

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

            if (Request.QueryString["entry"].ToString() == "N")
            {
                gv1.KeyFieldName = "ServiceType";
                gv2.KeyFieldName = "ServiceType";
            }
            else
            {
                gv1.KeyFieldName = "DocNumber;LineNumber;ServiceType";
                gv2.KeyFieldName = "DocNumber;LineNumber;ServiceType";
            }

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;
            }
            else
            {
                view = false;
            }


       
            if (!IsPostBack)
            {
                Session["ContractDatatable"] = null;
                Session["ContractDetail"] = null;
                Session["ContractDatatable2"] = null;
                Session["ContractDetail2"] = null;
                Session["ContractCascaded"] = null;
                Session["ContractCascaded2"] = null;
                Session["ContractRefNumber"] = null;
                Session["ContractBizPartner"] = null;
                Session["ContractProfit"] = null;
                Session["ContractWH"] = null;

                Connection = (Session["ConnString"].ToString());

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                txtStatus.Text = _Entity.Status;
                aglContractNumber.Value = _Entity.ContractNumber;
                dtpDateFrom.Text = String.IsNullOrEmpty(_Entity.DateFrom) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DateFrom).ToShortDateString();
                dtpDateTo.Text = String.IsNullOrEmpty(_Entity.DateTo) ? DateTime.Now.AddYears(1).ToShortDateString() : Convert.ToDateTime(_Entity.DateTo).ToShortDateString();
                //aglBillingPeriod.Value = _Entity.BillingPeriodType;
                aglBizPartnerCode.Value = _Entity.BizPartnerCode;
                aglWarehouse.Value = _Entity.WarehouseCode;
                aglProfitCenter.Value = _Entity.ProfitCenterCode;
                txtType.Text = _Entity.ContractType;
                dtpEffectivityDate.Text = String.IsNullOrEmpty(_Entity.EffectivityDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.EffectivityDate).ToShortDateString();
                
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
                
                if (!String.IsNullOrEmpty(_Entity.ContractNumber))
                {
                    Session["ContractRefNumber"] = _Entity.ContractNumber;
                }
                Session["ContractBizPartner"] = _Entity.BizPartnerCode;
                Session["ContractProfit"] = _Entity.ProfitCenterCode;
                Session["ContractWH"] = _Entity.WarehouseCode;

                DataTable OpenPeriod = Gears.RetriveData2("DECLARE @Month int DECLARE @Year int " +
                    " SELECT @Month = Value FROM IT.SystemSettings WHERE Code = 'CMONTH' SELECT @Year = Value FROM IT.SystemSettings WHERE Code = 'CYEAR' " +
                    " SELECT CONVERT(DATE,CONVERT(varchar(4),@Year)+'-'+CONVERT(varchar(2),@Month)+'-01') AS OpenPeriod", Session["ConnString"].ToString());

                dtpOpenPeriod.Text = Convert.ToDateTime(OpenPeriod.Rows[0]["OpenPeriod"].ToString()).ToShortDateString();

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }

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

                        switch (Request.QueryString["transtype"].ToString().Trim())
                        {
                            case "WMSCON":
                                if (String.IsNullOrEmpty(_Entity.ContractType))
                                {
                                    txtType.Text = "NEW CONTRACT";
                                }

                                txtHeader.Text = "Contract";
                                this.Title = "Contract";
                                view = false;
                                break;

                            case "WMSCREV":

                                txtType.Text = "REVISION";
                                txtHeader.Text = "Contract Revision";
                                this.Title = "Contract Revision";
                                view = false;

                                break;

                            case "WMSCREN":
                                txtType.Text = "RENEWAL";
                                txtHeader.Text = "Contract Renewal";
                                this.Title = "Contract Renewal";
                                view = false;
                                break;

                            case "WMSCTER":
                                txtType.Text = "TERMINATION";
                                txtHeader.Text = "Contract Termination";
                                this.Title = "Contract Termination";
                                view = true;
                                break;

                            case "WMSCALL":
                                txtHeader.Text = "All Contracts";
                                this.Title = "All Contracts";
                                view = true;
                                break;
                        }

                        if(String.IsNullOrEmpty(_Entity.Status))
                        {
                            txtStatus.Text = "NEW";
                        }

                        //if(String.IsNullOrEmpty(_Entity.BillingPeriodType))
                        //{
                        //    aglBillingPeriod.Value = "SEMIMONTHLY";
                        //}

                        if (Request.QueryString["transtype"].ToString().Trim() == "WMSCON")
                        {
                            if (String.IsNullOrEmpty(_Entity.WarehouseCode))
                            {
                                aglWarehouse.Value = "MLICAV";
                            }
                        }

                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;

                        switch (Request.QueryString["transtype"].ToString().Trim())
                        {
                            case "WMSCON":
                                if (String.IsNullOrEmpty(_Entity.ContractType))
                                {
                                    txtType.Text = "NEW CONTRACT";
                                }

                                txtHeader.Text = "Contract";
                                this.Title = "Contract";
                                view = false;
                                break;

                            case "WMSCREV":

                                txtType.Text = "REVISION";
                                txtHeader.Text = "Contract Revision";
                                this.Title = "Contract Revision";
                                view = false;

                                break;

                            case "WMSCREN":
                                txtType.Text = "RENEWAL";
                                txtHeader.Text = "Contract Renewal";
                                this.Title = "Contract Renewal";
                                view = false;
                                break;

                            case "WMSCTER":
                                txtType.Text = "TERMINATION";
                                txtHeader.Text = "Contract Termination";
                                this.Title = "Contract Termination";
                                view = true;
                                break;

                            case "WMSCALL":
                                txtHeader.Text = "All Contracts";
                                this.Title = "All Contracts";
                                view = true;
                                break;
                        }

                        if(String.IsNullOrEmpty(_Entity.Status))
                        {
                            txtStatus.Text = "NEW";
                        }

                        //if(String.IsNullOrEmpty(_Entity.BillingPeriodType))
                        //{
                        //    aglBillingPeriod.Value = "SEMIMONTHLY";
                        //}

                        if (Request.QueryString["transtype"].ToString().Trim() == "WMSCON")
                        {
                            if (String.IsNullOrEmpty(_Entity.WarehouseCode))
                            {
                                aglWarehouse.Value = "MLICAV";
                            }
                        }

                        break;

                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        switch (Request.QueryString["transtype"].ToString().Trim())
                        {
                            case "WMSCON":
                                txtHeader.Text = "Contract";
                                this.Title = "Contract";
                                break;

                            case "WMSCREV":
                                txtHeader.Text = "Contract Revision";
                                this.Title = "Contract Revision";
                                break;

                            case "WMSCREN":
                                txtHeader.Text = "Contract Renewal";
                                this.Title = "Contract Renewal";
                                break;

                            case "WMSCTER":
                                txtHeader.Text = "Contract Termination";
                                this.Title = "Contract Termination";
                                break;

                            case "WMSCALL":
                                txtHeader.Text = "All Contracts";
                                this.Title = "All Contracts";
                                break;
                        }
                        break;

                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        switch (Request.QueryString["transtype"].ToString().Trim())
                        {
                            case "WMSCON":
                                txtHeader.Text = "Contract";
                                this.Title = "Contract";
                                break;

                            case "WMSCREV":
                                txtHeader.Text = "Contract Revision";
                                this.Title = "Contract Revision";
                                break;

                            case "WMSCREN":
                                txtHeader.Text = "Contract Renewal";
                                this.Title = "Contract Renewal";
                                break;

                            case "WMSCTER":
                                txtHeader.Text = "Contract Termination";
                                this.Title = "Contract Termination";
                                break;

                            case "WMSCALL":
                                txtHeader.Text = "All Contracts";
                                this.Title = "All Contracts";
                                break;
                        }
                        break;
                }

                DataTable checkCount = Gears.RetriveData2("SELECT DocNumber FROM WMS.ContractDetail WHERE Type = 'STORAGE' AND DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    gv1.DataSourceID = "odsDetail";

                }
                else
                {
                    //if (Request.QueryString["entry"].ToString() == "E" || (Request.QueryString["entry"].ToString() == "N"))
                    //{
                    //    if (Request.QueryString["transtype"].ToString().Trim() == "WMSCON")
                    //    {
                    //        if ((Request.QueryString["entry"].ToString() == "E" && !String.IsNullOrEmpty(_Entity.LastEditedBy)))
                    //        {
                    //            gv1.DataSourceID = "sdsDetail";
                    //        }
                            //else
                            //{
                            //    GetSelectedVal();
                            //}
                    //    }
                    //    else
                    //    {
                    //        gv1.DataSourceID = "sdsDetail";
                    //    }
                    //}
                    //else
                    //{
                        gv1.DataSourceID = "sdsDetail";
                    //}
                }


                DataTable checkCount1 = Gears.RetriveData2("SELECT DocNumber FROM WMS.ContractDetail WHERE Type != 'STORAGE' AND DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                if (checkCount1.Rows.Count > 0)
                {
                    gv2.DataSourceID = "odsDetailNon";

                }
                else
                {
                    //if (Request.QueryString["entry"].ToString() == "E" || (Request.QueryString["entry"].ToString() == "N"))
                    //{
                    //    if (Request.QueryString["transtype"].ToString().Trim() == "WMSCON")
                    //    {
                    //        if ((Request.QueryString["entry"].ToString() == "E" && !String.IsNullOrEmpty(_Entity.LastEditedBy)))
                    //        {
                    //            gv2.DataSourceID = "sdsDetail";
                    //        }
                    //        //else
                    //        //{
                    //        //    GetSelectedVal2();
                    //        //}
                    //    }
                    //    else
                    //    {
                            gv2.DataSourceID = "sdsDetail";
                    //    }
                    //}
                    //else
                    //{
                    //    gv2.DataSourceID = "sdsDetail";
                    //}
                }


                if (!String.IsNullOrEmpty(txtHLastEditedBy.Text))
                {
                    gvRef.DataSourceID = "odsReference";
                }

                //Session["ContractRefNumber"] = aglContractNumber.Text;
            }

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                DefaultValues();
            }              
            
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSCON";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GWarehouseManagement.Contract_Validate(gparam);

            gv1.JSProperties["cp_valmsg"] = strresult;

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            var look = sender as ASPxTextBox;
            if (look != null)
            {
                if (Request.QueryString["transtype"].ToString().Trim() == "WMSCTER")
                {
                    look.ReadOnly = true;
                }
                else
                {
                    look.ReadOnly = view;
                }
            }
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                if (Request.QueryString["transtype"].ToString().Trim() == "WMSCTER")
                {
                    look.ReadOnly = true;
                    look.DropDownButton.Enabled = false;
                }
                else
                {
                    look.ReadOnly = view;
                    look.DropDownButton.Enabled = !view;
                }
            }
        }
        protected void LookupLoad_2(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    look.ReadOnly = true;
                    look.DropDownButton.Enabled = false;
                }
                else
                {
                    if (txtType.Text == "NEW CONTRACT")
                    {
                        look.ReadOnly = true;
                        look.DropDownButton.Enabled = false;
                    }
                    else
                    {
                        look.ReadOnly = false;
                        look.DropDownButton.Enabled = true;
                    }
                }
            }
        }
        protected void ButtonLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var button = sender as ASPxButton;
            if (button != null)
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    button.Enabled = false;
                }
                else
                {
                    if (txtType.Text == "NEW CONTRACT")
                    {
                        button.Enabled = false;
                    }
                    else
                    {
                        button.Enabled = true;
                    }
                }
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            var look = sender as ASPxGridLookup;
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                if (look != null)
                {
                    look.Enabled = false;
                }
            }
        }
        protected void CheckBoxLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxCheckBox;
            if (look != null)
            {
                if (Request.QueryString["transtype"].ToString().Trim() == "WMSCTER")
                {
                    look.ReadOnly = true;
                }
                else
                {
                    look.ReadOnly = view;
                }
            }
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all textbox
        {
            var look = sender as ASPxComboBox;
            if (look != null)
            {
                if (Request.QueryString["transtype"].ToString().Trim() == "WMSCTER")
                {
                    look.ReadOnly = true;
                    look.DropDownButton.Enabled = false;
                }
                else
                {
                    look.ReadOnly = view;
                    look.DropDownButton.Enabled = !view;
                }
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            var look = sender as ASPxDateEdit;
            if (look != null)
            {
                if (Request.QueryString["transtype"].ToString().Trim() == "WMSCTER")
                {
                    look.ReadOnly = true;
                    look.DropDownButton.Enabled = false;
                }
                else
                {
                    look.ReadOnly = view;
                    look.DropDownButton.Enabled = !view;
                }
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            var look = sender as ASPxSpinEdit;
            if (look != null)
            {
                if (Request.QueryString["transtype"].ToString().Trim() == "WMSCTER")
                {
                    look.ReadOnly = true;
                }
                else
                {
                    look.ReadOnly = view;
                }
            }
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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            if (Request.QueryString["transtype"].ToString().Trim() == "WMSCTER")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "Details")
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
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsServiceType";
                sdsServiceType.FilterExpression = Session["FilterExpression"].ToString();
            }
        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string stype = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
 
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;
            var itemlookup = sender as ASPxGridView;
            string codes = "";


            if (e.Parameters.Contains("ServiceType"))
            {
                DataTable getall = Gears.RetriveData2("SELECT ServiceType, Description, ServiceRate, ISNULL(Type,'STORAGE') AS Type FROM Masterfile.WMSServiceType WHERE ServiceType = '" + stype + "'", Session["ConnString"].ToString());
                foreach (DataRow dt in getall.Rows)
                {
                    codes = dt["Description"].ToString() + ";";
                    codes += dt["ServiceRate"].ToString() + ";";
                    codes += dt["Type"].ToString() + ";";
                    codes += dt["ServiceType"].ToString() + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            if (e.Parameters.Contains("ServiceHandling"))
            {
                DataTable getall = Gears.RetriveData2("SELECT Description, ServiceRate, Type FROM Masterfile.WMSServiceType WHERE ServiceType = '" + stype + "'", Session["ConnString"].ToString());
                foreach (DataRow dt in getall.Rows)
                {
                    codes += dt["ServiceRate"].ToString() + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Status = txtStatus.Text;
            //_Entity.ContractNumber = String.IsNullOrEmpty(aglContractNumber.Text) ? null : aglContractNumber.Value.ToString();
            _Entity.DateFrom = dtpDateFrom.Text;
            _Entity.DateTo = dtpDateTo.Text;
            //_Entity.BillingPeriodType = String.IsNullOrEmpty(aglBillingPeriod.Text) ? null : aglBillingPeriod.Value.ToString();
            _Entity.BizPartnerCode = String.IsNullOrEmpty(aglBizPartnerCode.Text) ? null : aglBizPartnerCode.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouse.Text) ? null : aglWarehouse.Value.ToString();
            _Entity.ProfitCenterCode = String.IsNullOrEmpty(aglProfitCenter.Text) ? null : aglProfitCenter.Value.ToString();
            _Entity.ContractType = String.IsNullOrEmpty(txtType.Text) ? null : txtType.Text;
            _Entity.EffectivityDate = dtpEffectivityDate.Text;
            
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
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM WMS.Contract WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());



            switch (e.Parameter)
            {

                case "Add":
                case "Update":

                    //gv1.UpdateEdit();
                    //gv2.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber, "WMS.Contract", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        gv1.JSProperties["cp_message"] = strError;
                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_forceclose"] = true;
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
                        _Entity.UpdateData(_Entity);

                        //if (Session["ContractDatatable"] == "1")
                        //{
                        //    //gv1.DataSource = GetSelectedVal();
                        //    _Entity.DeleteFirstDataStorage(txtDocNumber.Text, Session["ConnString"].ToString());
                        //    gv1.DataSourceID = sdsContractDetail.ID;
                        //    gv1.UpdateEdit();
                        //}
                        //else
                        //{
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();
                        //}

                        //if (Session["ContractDatatable2"] == "1")
                        //{
                        //    //gv2.DataSource = GetSelectedVal2();
                        //    _Entity.DeleteFirstDataNonStorage(txtDocNumber.Text, Session["ConnString"].ToString());
                        //    gv2.DataSourceID = sdsContractDetailNon.ID;
                        //    gv2.UpdateEdit();
                        //}
                        //else
                        //{
                        gv2.DataSourceID = "odsDetailNon";
                        odsDetailNon.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv2.UpdateEdit();
                        //}

                        _Entity.UpdateOtherInfo(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        DataTable CheckRate = new DataTable();
                        CheckRate = Gears.RetriveData2("SELECT 'Line ['+ LineNumber +'] Service rate is less than the original/standard rate set for the service type.' AS PromptMsg "
                            + " FROM WMS.ContractDetail A INNER JOIN Masterfile.WMSServiceType B ON A.ServiceType = B.ServiceType AND A.ServiceRate < B.ServiceRate WHERE A.DocNumber = '" + _Entity.DocNumber + "' AND A.BillingType != 'EXCESS QTYND'", Session["ConnString"].ToString());

                        string CheckRateMsg = "";

                        if (CheckRate.Rows.Count > 0)
                        {
                            foreach (DataRow dt in CheckRate.Rows)
                            {
                                CheckRateMsg += Environment.NewLine + dt["PromptMsg"].ToString();
                            }

                            gv1.JSProperties["cp_valmsg"] = (String.IsNullOrEmpty(gv1.JSProperties["cp_valmsg"].ToString()) ? "" : gv1.JSProperties["cp_valmsg"].ToString().Trim()) + CheckRateMsg;
                        }

                        if (e.Parameter == "Add")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else
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
                    Session["Refresh"] = "1";
                break;

                case "ConfDelete":
                    _Entity.Deletedata(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                break;

                case "Generate":

                break;

                case "CallbackContractNum":
                    Session["ContractCascaded"] = null;
                    Session["ContractCascaded2"] = null;

                    SqlDataSource ds = sdsForDetail;
                    ds.SelectCommand = string.Format("SELECT DocNumber, BizPartnerCode, ProfitCenterCode, BillingPeriodType, WarehouseCode, DateFrom, DateTo FROM WMS.Contract WHERE DocNumber =  '" + aglContractNumber.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        aglBizPartnerCode.Text = tran[0][1].ToString();
                        aglProfitCenter.Text = tran[0][2].ToString();
                        //aglBillingPeriod.Text = tran[0][3].ToString();
                        aglWarehouse.Text = tran[0][4].ToString();
                        dtpDateFrom.Date = Convert.ToDateTime(tran[0][5].ToString());
                        dtpDateTo.Date = Convert.ToDateTime(tran[0][6].ToString());
                    }

                    Session["ContractCascaded"] = "1";
                    Session["ContractCascaded2"] = "1";
                    
                    Session["ContractRefNumber"] = null;
                    Session["ContractRefNumber"] = aglContractNumber.Text;
                break;

                case "CallbackRefContract":
                    Session["ContractRefNumber"] = null;
                    Session["ContractRefNumber"] = aglContractNumber.Text;
                break;

                case "CallbackBizPartner":
                    SqlDataSource pc = sdsBizPartner;
                    pc.SelectCommand = string.Format("select BizPartnerCode,Name, ProfitCenterCode from Masterfile.BPCustomerInfo where BizPartnerCode =  '" + aglBizPartnerCode.Text + "'");
                    DataView prof = (DataView)pc.Select(DataSourceSelectArguments.Empty);
                    if (prof.Count > 0)
                    {

                        aglProfitCenter.Text = prof[0][2].ToString();
                    
                    }
                    pc.SelectCommand = "SELECT A.BizPartnerCode, A.Name, ISNULL(B.BusinessAccountCode,'') AS BusinessAccountCode, ISNULL(C.BizAccountName,'') AS BizAccountName, B.Address, B.ContactPerson FROM Masterfile.BPCustomerInfo A " +
                    " INNER JOIN Masterfile.BizPartner B ON A.BizPartnerCode = B.BizPartnerCode LEFT JOIN Masterfile.BizAccount C ON B.BusinessAccountCode = C.BizAccountCode WHERE (ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsInactive,0) = 0)";

                    pc.DataBind();
                    cp.JSProperties["cp_bizpartner"] = true;
                break;
                case "CallbackContractCascade2":
                GetSelectedVal2();
                cp.JSProperties["cp_generated"] = true;
                break;
                case "CallbackContractCascade":
                GetSelectedVal();
                cp.JSProperties["cp_generatedstorage"] = true;
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

            //if (Session["ContractDatatable"].ToString() == "1" && check == true)
            //{
            //    e.Handled = true;
            //    DataTable source = GetSelectedVal();

            //    foreach (ASPxDataUpdateValues values in e.UpdateValues)
            //    {
            //        object[] keys = { values.NewValues["ServiceType"] };
            //        DataRow row = source.Rows.Find(keys);
            //        row["ServiceType"] = values.NewValues["ServiceType"];
            //        row["Description"] = values.NewValues["Description"];
            //        row["BillingType"] = values.NewValues["BillingType"];
            //        row["ServiceRate"] = values.NewValues["ServiceRate"];
            //        row["ExcessRate"] = values.NewValues["ExcessRate"];
            //        row["UnitOfMeasure"] = values.NewValues["UnitOfMeasure"];
            //        row["UnitOfMeasureBulk"] = values.NewValues["UnitOfMeasureBulk"];
            //        row["Vatable"] = values.NewValues["Vatable"];
            //        row["VATCode"] = values.NewValues["VATCode"];
            //        row["Period"] = values.NewValues["Period"];
            //        row["IsMulStorage"] = values.NewValues["IsMulStorage"];
            //        row["StorageCode"] = values.NewValues["StorageCode"];
            //        row["IsDiffCustomer"] = values.NewValues["IsDiffCustomer"];
            //        row["DiffCustomerCode"] = values.NewValues["DiffCustomerCode"];
            //        row["HandlingInRate"] = values.NewValues["HandlingInRate"];
            //        row["HandlingOutRate"] = values.NewValues["HandlingOutRate"];
            //        row["MinHandlingIn"] = values.NewValues["MinHandlingIn"];
            //        row["MinHandlingOut"] = values.NewValues["MinHandlingOut"];
            //        row["MinStorage"] = values.NewValues["MinStorage"];
            //        row["BeginDay"] = values.NewValues["BeginDay"];
            //        row["Staging"] = values.NewValues["Staging"];
            //        row["AllocChargeable"] = values.NewValues["AllocChargeable"];
            //        row["SplitBill"] = values.NewValues["SplitBill"];
            //        row["ServiceHandling"] = values.NewValues["ServiceHandling"];
            //        row["SplitBillRate"] = values.NewValues["SplitBillRate"];
            //        row["HandlingUOMQty"] = values.NewValues["HandlingUOMQty"];
            //        row["HandlingUOMBulk"] = values.NewValues["HandlingUOMBulk"];
            //        row["BillingPrintOutStr"] = values.NewValues["BillingPrintOutStr"];
            //        row["BillingPrintOutHan"] = values.NewValues["BillingPrintOutHan"];
            //        row["ConvFactorStr"] = values.NewValues["ConvFactorStr"];
            //        row["ConvFactorHan"] = values.NewValues["ConvFactorHan"];
            //        row["Remarks"] = values.NewValues["Remarks"];
            //        row["Field1"] = values.NewValues["Field1"];
            //        row["Field2"] = values.NewValues["Field2"];
            //        row["Field3"] = values.NewValues["Field3"];
            //        row["Field4"] = values.NewValues["Field4"];
            //        row["Field5"] = values.NewValues["Field5"];
            //        row["Field6"] = values.NewValues["Field6"];
            //        row["Field7"] = values.NewValues["Field7"];
            //        row["Field8"] = values.NewValues["Field8"];
            //        row["Field9"] = values.NewValues["Field9"];
            //        row["Type"] = values.NewValues["Type"];
            //    }

            //    foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //    {
            //        try
            //        {
            //            object[] keys = { values.Keys["ServiceType"] };
            //            source.Rows.Remove(source.Rows.Find(keys));
            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }
            //    }

            //    foreach (ASPxDataInsertValues values in e.InsertValues)
            //    {
            //        var LineNumber = values.NewValues["LineNumber"];
            //        var ServiceType = values.NewValues["ServiceType"];
            //        var Description = values.NewValues["Description"];
            //        var BillingType = values.NewValues["BillingType"];
            //        var ServiceRate = values.NewValues["ServiceRate"];
            //        var ExcessRate = values.NewValues["ExcessRate"];
            //        var UnitOfMeasure = values.NewValues["UnitOfMeasure"];
            //        var UnitOfMeasureBulk = values.NewValues["UnitOfMeasureBulk"];
            //        var Vatable = values.NewValues["Vatable"];
            //        var VATCode = values.NewValues["VATCode"];
            //        var Period = values.NewValues["Period"];
            //        var IsMulStorage = values.NewValues["IsMulStorage"];
            //        var StorageCode = values.NewValues["StorageCode"];
            //        var IsDiffCustomer = values.NewValues["IsDiffCustomer"];
            //        var DiffCustomerCode = values.NewValues["DiffCustomerCode"];
            //        var HandlingInRate = values.NewValues["HandlingInRate"];
            //        var HandlingOutRate = values.NewValues["HandlingOutRate"];
            //        var MinHandlingIn = values.NewValues["MinHandlingIn"];
            //        var MinHandlingOut = values.NewValues["MinHandlingOut"];
            //        var MinStorage = values.NewValues["MinStorage"];
            //        var BeginDay = values.NewValues["BeginDay"];
            //        var Staging = values.NewValues["Staging"];
            //        var AllocChargeable = values.NewValues["AllocChargeable"];
            //        var SplitBill = values.NewValues["SplitBill"];
            //        var ServiceHandling = values.NewValues["ServiceHandling"];
            //        var SplitBillRate = values.NewValues["SplitBillRate"];
            //        var HandlingUOMQty = values.NewValues["HandlingUOMQty"];
            //        var HandlingUOMBulk = values.NewValues["HandlingUOMBulk"];
            //        var BillingPrintOutStr = values.NewValues["BillingPrintOutStr"];
            //        var BillingPrintOutHan = values.NewValues["BillingPrintOutHan"];
            //        var ConvFactorStr = values.NewValues["ConvFactorStr"];
            //        var ConvFactorHan = values.NewValues["ConvFactorHan"];
            //        var Remarks = values.NewValues["Remarks"];
            //        var Field1 = values.NewValues["Field1"];
            //        var Field2 = values.NewValues["Field2"];
            //        var Field3 = values.NewValues["Field3"];
            //        var Field4 = values.NewValues["Field4"];
            //        var Field5 = values.NewValues["Field5"];
            //        var Field6 = values.NewValues["Field6"];
            //        var Field7 = values.NewValues["Field7"];
            //        var Field8 = values.NewValues["Field8"];
            //        var Field9 = values.NewValues["Field9"];
            //        var Type = values.NewValues["Type"];

            //        source.Rows.Add(LineNumber, ServiceType, Description, BillingType, ServiceRate, ExcessRate, UnitOfMeasure, UnitOfMeasureBulk, Vatable, VATCode,
            //            Period, IsMulStorage, StorageCode, IsDiffCustomer, DiffCustomerCode, HandlingInRate, HandlingOutRate, MinHandlingIn, MinHandlingOut, MinStorage, BeginDay,
            //            Staging, AllocChargeable, SplitBill, ServiceHandling, SplitBillRate, HandlingUOMQty, HandlingUOMBulk, BillingPrintOutStr, BillingPrintOutHan, ConvFactorStr,
            //            ConvFactorHan, Remarks, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Type);
            //    }

            //    foreach (DataRow dtRow in source.Rows)
            //    {
            //        _EntityDetail.ServiceType = dtRow["ServiceType"].ToString();
            //        _EntityDetail.Description = dtRow["Description"].ToString();
            //        _EntityDetail.BillingType = dtRow["BillingType"].ToString();
            //        _EntityDetail.ServiceRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ServiceRate"]) ? 0 : dtRow["ServiceRate"]);
            //        _EntityDetail.ExcessRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExcessRate"]) ? 0 : dtRow["ExcessRate"]);
            //        _EntityDetail.UnitOfMeasure = dtRow["UnitOfMeasure"].ToString();
            //        _EntityDetail.UnitOfMeasureBulk = dtRow["UnitOfMeasureBulk"].ToString();
            //        _EntityDetail.Vatable = Convert.ToBoolean(Convert.IsDBNull(dtRow["Vatable"]) ? false : dtRow["Vatable"]);
            //        _EntityDetail.VATCode = dtRow["VATCode"].ToString();
            //        _EntityDetail.Period = dtRow["Period"].ToString();
            //        _EntityDetail.IsMulStorage = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsMulStorage"]) ? false : dtRow["IsMulStorage"]);
            //        _EntityDetail.StorageCode = dtRow["StorageCode"].ToString();
            //        _EntityDetail.IsDiffCustomer = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsDiffCustomer"]) ? false : dtRow["IsDiffCustomer"]);
            //        _EntityDetail.DiffCustomerCode = dtRow["DiffCustomerCode"].ToString();
            //        _EntityDetail.HandlingInRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingInRate"]) ? 0 : dtRow["HandlingInRate"]);
            //        _EntityDetail.HandlingOutRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["HandlingOutRate"]) ? 0 : dtRow["HandlingOutRate"]);
            //        _EntityDetail.MinHandlingIn = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingIn"]) ? 0 : dtRow["MinHandlingIn"]);
            //        _EntityDetail.MinHandlingOut = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinHandlingOut"]) ? 0 : dtRow["MinHandlingOut"]);
            //        _EntityDetail.MinStorage = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinStorage"]) ? 0 : dtRow["MinStorage"]);
            //        _EntityDetail.BeginDay = Convert.ToInt32(Convert.IsDBNull(dtRow["BeginDay"]) ? 0 : dtRow["BeginDay"]);
            //        _EntityDetail.Staging = Convert.ToInt32(Convert.IsDBNull(dtRow["Staging"]) ? 0 : dtRow["Staging"]);
            //        _EntityDetail.AllocChargeable = Convert.ToInt32(Convert.IsDBNull(dtRow["AllocChargeable"]) ? 0 : dtRow["AllocChargeable"]);
            //        _EntityDetail.SplitBill = Convert.ToBoolean(Convert.IsDBNull(dtRow["SplitBill"]) ? false : dtRow["SplitBill"]);
            //        _EntityDetail.ServiceHandling = dtRow["ServiceHandling"].ToString();
            //        _EntityDetail.SplitBillRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["SplitBillRate"]) ? 0 : dtRow["SplitBillRate"]);
            //        _EntityDetail.HandlingUOMQty = dtRow["HandlingUOMQty"].ToString();
            //        _EntityDetail.HandlingUOMBulk = dtRow["HandlingUOMBulk"].ToString();
            //        _EntityDetail.BillingPrintOutStr = dtRow["BillingPrintOutStr"].ToString();
            //        _EntityDetail.BillingPrintOutHan = dtRow["BillingPrintOutHan"].ToString();
            //        _EntityDetail.ConvFactorStr = Convert.ToDecimal(Convert.IsDBNull(dtRow["ConvFactorStr"]) ? 0 : dtRow["ConvFactorStr"]);
            //        _EntityDetail.ConvFactorHan = Convert.ToDecimal(Convert.IsDBNull(dtRow["ConvFactorHan"]) ? 0 : dtRow["ConvFactorHan"]);
            //        _EntityDetail.Remarks = dtRow["Remarks"].ToString();
            //        _EntityDetail.Field1 = dtRow["Field1"].ToString();
            //        _EntityDetail.Field2 = dtRow["Field2"].ToString();
            //        _EntityDetail.Field3 = dtRow["Field3"].ToString();
            //        _EntityDetail.Field4 = dtRow["Field4"].ToString();
            //        _EntityDetail.Field5 = dtRow["Field5"].ToString();
            //        _EntityDetail.Field6 = dtRow["Field6"].ToString();
            //        _EntityDetail.Field7 = dtRow["Field7"].ToString();
            //        _EntityDetail.Field8 = dtRow["Field8"].ToString();
            //        _EntityDetail.Field9 = dtRow["Field9"].ToString();
            //        _EntityDetail.Type = dtRow["Type"].ToString();

            //        _EntityDetail.AddContractDetail(_EntityDetail);
            //    }
            //}
        }
        #endregion
        private DataTable GetSelectedVal()
        {

            DataTable dt = new DataTable();

            // Storage
            gvRefC.DataSourceID = null;

            DataTable getDetail = Gears.RetriveData2(
            "SELECT '' AS DocNumber, RIGHT('00000'+CONVERT(varchar(max),ROW_NUMBER() OVER (ORDER BY ServiceType)),5)  as LineNumber, ServiceType, Description, "
                    + " ServiceRate, UnitOfMeasure, UnitOfMeasureBulk, Vatable, "
                    + " CASE WHEN BillingType = 'Chargeable' OR BillingType = 'CHR' OR BillingType = 'NEXT DAY' THEN 'NEXT DAY' WHEN BillingType = 'Actual' OR BillingType = 'ACT' OR BillingType = 'SAME DAY' THEN 'SAME DAY' ELSE BillingType END AS BillingType, ISNULL(Period,'SEMIMONTHLY') AS Period, "
                    + " IsMulStorage, StorageCode, IsDiffCustomer, DiffCustomerCode, "
                    + " ISNULL(HandlingInRate,0.0000) AS HandlingInRate, ISNULL(HandlingOutRate,0.0000) AS HandlingOutRate, ISNULL(MinHandlingIn,0) AS MinHandlingIn, ISNULL(MinHandlingOut,0) AS MinHandlingOut, MinStorage, Remarks, "
                    + " Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Type,  VATCode, ExcessRate, BeginDay, ISNULL(Staging,0) AS Staging, ISNULL(AllocChargeable,0) AS AllocChargeable, SplitBill,  ServiceHandling,  HandlingUOMQty,  HandlingUOMBulk,  "
                    + " CASE WHEN ISNULL(BillingPrintOutStr,'') = '' THEN 'REGULAR' ELSE BillingPrintOutStr END AS BillingPrintOutStr,  BillingPrintOutHan, ISNULL(ConvFactorStr,0) AS ConvFactorStr, ConvFactorHan, SplitBillRate "
                    + " FROM WMS.ContractDetail WHERE Type = 'STORAGE' AND DocNumber = '" + Session["ContractRefNumber"].ToString() + "'", Session["ConnString"].ToString());

            gvRefC.DataSource = getDetail;
            gvRefC.DataBind();
            gvRefC.KeyFieldName = "ServiceType";

            // Non Storage
            gvRefCN.DataSourceID = null;


            DataTable getDetail2 = Gears.RetriveData2("SELECT '' AS DocNumber, RIGHT('00000'+CONVERT(varchar(max),ROW_NUMBER() OVER (ORDER BY ServiceType)),5)  as LineNumber, ServiceType, Description, "
                    + " ServiceRate, UnitOfMeasure, UnitOfMeasureBulk, Vatable, CASE WHEN BillingType = 'Chargeable' THEN 'CHR' ELSE 'ACT' END AS BillingType, ISNULL(Period,'SEMIMONTHLY') AS Period, "
                    + " ISNULL(HandlingInRate,0.0000) AS HandlingInRate, ISNULL(HandlingOutRate,0.0000) AS HandlingOutRate, ISNULL(MinHandlingIn,0) AS MinHandlingIn, ISNULL(MinHandlingOut,0) AS MinHandlingOut, MinStorage, Remarks, "
                    + " Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Type,  VATCode, ExcessRate, BeginDay, ISNULL(Staging,0) AS Staging, ISNULL(AllocChargeable,0) AS AllocChargeable, SplitBill,  ServiceHandling,  HandlingUOMQty,  HandlingUOMBulk,  "
                    + " CASE WHEN ISNULL(BillingPrintOutStr,'') = '' THEN 'REGULAR' ELSE BillingPrintOutStr END AS BillingPrintOutStr,  BillingPrintOutHan, ISNULL(ConvFactorStr,0) AS ConvFactorStr, ConvFactorHan, SplitBillRate "
                    + " FROM WMS.ContractDetail WHERE Type != 'STORAGE' AND DocNumber = '" + Session["ContractRefNumber"].ToString() + "'", Session["ConnString"].ToString());

            gvRefCN.DataSource = getDetail2;
            gvRefCN.DataBind();
            gvRefCN.KeyFieldName = "ServiceType";
            //DataTable dt = new DataTable();
            //gv1.DataSourceID = null;
            //gv1.DataBind();

            //if (Session["ContractCascaded"] == "1")
            //{
            //    sdsContractDetail.SelectCommand = "SELECT '' AS DocNumber, RIGHT('00000'+CONVERT(varchar(max),ROW_NUMBER() OVER (ORDER BY ServiceType)),5)  as LineNumber, ServiceType, Description, "
            //        + " ServiceRate, UnitOfMeasure, UnitOfMeasureBulk, Vatable, "
            //        + " CASE WHEN BillingType = 'Chargeable' OR BillingType = 'CHR' OR BillingType = 'NEXT DAY' THEN 'NEXT DAY' WHEN BillingType = 'Actual' OR BillingType = 'ACT' OR BillingType = 'SAME DAY' THEN 'SAME DAY' ELSE BillingType END AS BillingType, ISNULL(Period,'SEMIMONTHLY') AS Period, "
            //        + " IsMulStorage, StorageCode, IsDiffCustomer, DiffCustomerCode, "
            //        + " ISNULL(HandlingInRate,0.0000) AS HandlingInRate, ISNULL(HandlingOutRate,0.0000) AS HandlingOutRate, ISNULL(MinHandlingIn,0) AS MinHandlingIn, ISNULL(MinHandlingOut,0) AS MinHandlingOut, MinStorage, Remarks, "
            //        + " Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Type,  VATCode, ExcessRate, BeginDay, ISNULL(Staging,0) AS Staging, ISNULL(AllocChargeable,0) AS AllocChargeable, SplitBill,  ServiceHandling,  HandlingUOMQty,  HandlingUOMBulk,  "
            //        + " CASE WHEN ISNULL(BillingPrintOutStr,'') = '' THEN 'REGULAR' ELSE BillingPrintOutStr END AS BillingPrintOutStr,  BillingPrintOutHan, ISNULL(ConvFactorStr,0) AS ConvFactorStr, ConvFactorHan, SplitBillRate "
            //        + " FROM WMS.ContractDetail WHERE Type = 'STORAGE' AND DocNumber = '" + Session["ContractRefNumber"].ToString() + "'";
            //}
            //else
            //{
            //    sdsContractDetail.SelectCommand = "SELECT '' AS DocNumber, RIGHT('00000'+CONVERT(varchar(max),ROW_NUMBER() OVER (ORDER BY ServiceType)),5)  AS LineNumber, ServiceType, Description, "
            //        + " ServiceRate, '' AS UnitOfMeasure,'' AS UnitOfMeasureBulk, CONVERT(bit,0) AS Vatable, 'NEXT DAY' AS BillingType, 'SEMIMONTHLY' AS Period, CONVERT(bit,0) AS IsMulStorage, ServiceType AS StorageCode, CONVERT(bit,0) AS IsDiffCustomer, '' AS DiffCustomerCode, 0.0000 AS HandlingInRate, 0.0000 AS HandlingOutRate, "
            //        + " 0.00 AS MinHandlingIn, 0.00 AS MinHandlingOut, 0.00 AS MinStorage,'' AS Remarks, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, "
            //        + " '' as Field9,Type, 'NONV' AS VATCode, 0.0000 AS	ExcessRate, 0 AS BeginDay, 0 AS Staging, 0 AS AllocChargeable, CONVERT(bit,0) AS SplitBill, '' AS ServiceHandling, '' AS HandlingUOMQty, '' AS HandlingUOMBulk, 'REGULAR' AS BillingPrintOutStr, '' AS BillingPrintOutHan, 0.000000 AS ConvFactorStr, 0.000000 AS ConvFactorHan, 0.0000 AS SplitBillRate "
            //        + " FROM Masterfile.WMSServiceType WHERE ISNULL(IsStandard,0) = 1 and ISNULL(IsInactive,0) = 0 AND Type = 'STORAGE'";
            //}

            //gv1.DataSource = sdsContractDetail;
            //if (gv1.DataSourceID != "")
            //{
            //    gv1.DataSourceID = null;
            //}
            //gv1.DataBind();
            //Session["ContractDetail"] = sdsContractDetail;
            //Session["ContractDatatable"] = "1";

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

            //dt.PrimaryKey = new DataColumn[] {
            //dt.Columns["ServiceType"]};

            return dt;
        }
        private DataTable GetSelectedVal2()
        {
            DataTable dt = new DataTable();
            gvRefCN.DataSourceID = null;

          
            DataTable getDetail = Gears.RetriveData2("SELECT '' AS DocNumber, RIGHT('00000'+CONVERT(varchar(max),ROW_NUMBER() OVER (ORDER BY ServiceType)),5)  as LineNumber, ServiceType, Description, "
                    + " ServiceRate, UnitOfMeasure, UnitOfMeasureBulk, Vatable, CASE WHEN BillingType = 'Chargeable' THEN 'CHR' ELSE 'ACT' END AS BillingType, ISNULL(Period,'SEMIMONTHLY') AS Period, "
                    + " ISNULL(HandlingInRate,0.0000) AS HandlingInRate, ISNULL(HandlingOutRate,0.0000) AS HandlingOutRate, ISNULL(MinHandlingIn,0) AS MinHandlingIn, ISNULL(MinHandlingOut,0) AS MinHandlingOut, MinStorage, Remarks, "
                    + " Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Type,  VATCode, ExcessRate, BeginDay, ISNULL(Staging,0) AS Staging, ISNULL(AllocChargeable,0) AS AllocChargeable, SplitBill,  ServiceHandling,  HandlingUOMQty,  HandlingUOMBulk,  "
                    + " CASE WHEN ISNULL(BillingPrintOutStr,'') = '' THEN 'REGULAR' ELSE BillingPrintOutStr END AS BillingPrintOutStr,  BillingPrintOutHan, ISNULL(ConvFactorStr,0) AS ConvFactorStr, ConvFactorHan, SplitBillRate "
                    + " FROM WMS.ContractDetail WHERE Type != 'STORAGE' AND DocNumber = '" + Session["ContractRefNumber"].ToString() + "'", Session["ConnString"].ToString());

            gvRefCN.DataSource = getDetail;
            gvRefCN.DataBind();
            gvRefCN.KeyFieldName = "ServiceType";

            //if (Session["ContractCascaded2"] == "1")
            //{
            //    sdsContractDetailNon.SelectCommand = "SELECT '' AS DocNumber, RIGHT('00000'+CONVERT(varchar(max),ROW_NUMBER() OVER (ORDER BY ServiceType)),5)  as LineNumber, ServiceType, Description, "
            //        + " ServiceRate, UnitOfMeasure, UnitOfMeasureBulk, Vatable, CASE WHEN BillingType = 'Chargeable' THEN 'CHR' ELSE 'ACT' END AS BillingType, ISNULL(Period,'SEMIMONTHLY') AS Period, "
            //        + " ISNULL(HandlingInRate,0.0000) AS HandlingInRate, ISNULL(HandlingOutRate,0.0000) AS HandlingOutRate, ISNULL(MinHandlingIn,0) AS MinHandlingIn, ISNULL(MinHandlingOut,0) AS MinHandlingOut, MinStorage, Remarks, "
            //        + " Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Type,  VATCode, ExcessRate, BeginDay, ISNULL(Staging,0) AS Staging, ISNULL(AllocChargeable,0) AS AllocChargeable, SplitBill,  ServiceHandling,  HandlingUOMQty,  HandlingUOMBulk,  "
            //        + " CASE WHEN ISNULL(BillingPrintOutStr,'') = '' THEN 'REGULAR' ELSE BillingPrintOutStr END AS BillingPrintOutStr,  BillingPrintOutHan, ISNULL(ConvFactorStr,0) AS ConvFactorStr, ConvFactorHan, SplitBillRate "
            //        + " FROM WMS.ContractDetail WHERE Type != 'STORAGE' AND DocNumber = '" + Session["ContractRefNumber"].ToString() + "'";
            //}
            //else
            //{
            //    sdsContractDetailNon.SelectCommand = "SELECT '' AS DocNumber, RIGHT('00000'+CONVERT(varchar(max),ROW_NUMBER() OVER (ORDER BY ServiceType)),5)  AS LineNumber, ServiceType, Description, "
            //        + " ServiceRate, '' AS UnitOfMeasure,'' AS UnitOfMeasureBulk, CONVERT(bit,0) AS Vatable, 'ACT' AS BillingType, 'SEMIMONTHLY' AS Period, 0.0000 AS HandlingInRate, 0.0000 AS HandlingOutRate, 0.00 AS MinHandlingIn, 0.00 AS MinHandlingOut, 0.00 AS MinStorage,'' AS Remarks, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, '' AS Field8, "
            //        + " '' AS Field9,Type, 'NONV' AS VATCode, 0.0000 AS	ExcessRate, 0 AS BeginDay, 0 AS Staging, 0 AS AllocChargeable, CONVERT(bit,0) AS SplitBill, '' AS ServiceHandling, '' AS HandlingUOMQty, '' AS HandlingUOMBulk, 'REGULAR' AS BillingPrintOutStr, '' AS BillingPrintOutHan, 0.000000 AS ConvFactorStr, 0.000000 AS ConvFactorHan, 0.0000 AS SplitBillRate "
            //        + " FROM Masterfile.WMSServiceType WHERE ISNULL(IsStandard,0) = 1 and ISNULL(IsInactive,0) = 0 AND Type != 'STORAGE'";
            //}

            //gv2.DataSource = sdsContractDetailNon;
            //if (gv2.DataSourceID != "")
            //{
            //    gv2.DataSourceID = null;
            //}
            //gv2.DataBind();
            //Session["ContractDetail2"] = sdsContractDetailNon;
            //Session["ContractDatatable2"] = "1";

            //foreach (GridViewColumn col in gv2.VisibleColumns)
            //{
            //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    dt.Columns.Add(dataColumn.FieldName);
            //}
            //for (int i = 0; i < gv2.VisibleRowCount; i++)
            //{
            //    DataRow row = dt.Rows.Add();
            //    foreach (DataColumn col in dt.Columns)
            //        row[col.ColumnName] = gv2.GetRowValues(i, col.ColumnName);
            //}

            //dt.PrimaryKey = new DataColumn[] {
            //dt.Columns["ServiceType"]};

            return dt;
        }
        protected void dtpDateFrom_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDateFrom.Date = DateTime.Now;
            }
        }

        protected void dtpDateTo_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDateTo.Date = DateTime.Now;
            }
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

        protected void cboStatus_ValueChanged(object sender, EventArgs e)
        {
            //if (cboStatus.Value == "NEW")
            //{
            //    aglContractNumber.Enabled = false;
            //}
            //else
            //{
            //    aglContractNumber.Enabled = true;
            //}
        }

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ContractDetail"] = null;
            }

            if (Session["ContractDetail"] != null)
            {
                gv1.DataSourceID = sdsContractDetail.ID;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Status = txtStatus.Text;
            _Entity.ContractNumber = String.IsNullOrEmpty(aglContractNumber.Text) ? null : aglContractNumber.Value.ToString();
            _Entity.DateFrom = dtpDateFrom.Text;
            _Entity.DateTo = dtpDateTo.Text;
            //_Entity.BillingPeriodType = String.IsNullOrEmpty(aglBillingPeriod.Text) ? null : aglBillingPeriod.Value.ToString();
            _Entity.BizPartnerCode = String.IsNullOrEmpty(aglBizPartnerCode.Text) ? null : aglBizPartnerCode.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouse.Text) ? null : aglWarehouse.Value.ToString();
            _Entity.ProfitCenterCode = String.IsNullOrEmpty(aglProfitCenter.Text) ? null : aglProfitCenter.Value.ToString();
            _Entity.ContractType = String.IsNullOrEmpty(txtType.Text) ? null : txtType.Text;
            _Entity.EffectivityDate = dtpEffectivityDate.Text;

            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            _Entity.AddedBy = txtHAddedBy.Text;
            _Entity.AddedDate = txtHAddedDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM WMS.Contract WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());

            switch (e.Parameters)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();
                    gv2.UpdateEdit();
                  
                    string strError = Functions.Submitted(_Entity.DocNumber, "WMS.Contract", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        gv1.JSProperties["cp_message"] = strError;
                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_forceclose"] = true;
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
                        _Entity.UpdateData(_Entity);

                        //if (Session["ContractDatatable"] == "1")
                        //{
                        //    //gv1.DataSource = GetSelectedVal();
                        //    _Entity.DeleteFirstDataStorage(txtDocNumber.Text, Session["ConnString"].ToString());
                        //    gv1.DataSourceID = sdsContractDetail.ID;
                        //    gv1.UpdateEdit();
                        //}
                        //else
                        //{
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv1.UpdateEdit();
                        //}

                        //if (Session["ContractDatatable2"] == "1")
                        //{
                        //    //gv2.DataSource = GetSelectedVal2();
                        //    _Entity.DeleteFirstDataNonStorage(txtDocNumber.Text, Session["ConnString"].ToString());
                        //    gv2.DataSourceID = sdsContractDetailNon.ID;
                        //    gv2.UpdateEdit();
                        //}
                        //else
                        //{
                            gv2.DataSourceID = "odsDetailNon";
                            odsDetailNon.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv2.UpdateEdit();
                        //}

                        _Entity.UpdateOtherInfo(txtDocNumber.Text, Session["ConnString"].ToString());
                        Validate();
                        DataTable CheckRate = new DataTable();
                        CheckRate = Gears.RetriveData2("SELECT 'Line ['+ LineNumber +'] Service rate is less than the original/standard rate set for the service type.' AS PromptMsg "
                            + " FROM WMS.ContractDetail A INNER JOIN Masterfile.WMSServiceType B ON A.ServiceType = B.ServiceType AND A.ServiceRate < B.ServiceRate WHERE A.DocNumber = '" + _Entity.DocNumber + "' AND A.BillingType != 'EXCESS QTYND'", Session["ConnString"].ToString());
                        
                        string CheckRateMsg = "";
                        
                        if (CheckRate.Rows.Count > 0)
                        {
                            foreach (DataRow dt in CheckRate.Rows)
                            {
                                CheckRateMsg += Environment.NewLine + dt["PromptMsg"].ToString();
                            }

                            gv1.JSProperties["cp_valmsg"] = (String.IsNullOrEmpty(gv1.JSProperties["cp_valmsg"].ToString()) ? "" : gv1.JSProperties["cp_valmsg"].ToString().Trim()) + CheckRateMsg;
                        }

                        if (e.Parameters == "Add")
                        {
                            gv1.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else
                        {
                            gv1.JSProperties["cp_message"] = "Successfully Updated!";
                        }

                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        gv1.JSProperties["cp_message"] = "Please check all the fields!";
                        gv1.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Delete":
                    gv1.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.Deletedata(_Entity);
                    gv1.JSProperties["cp_close"] = true;
                    gv1.JSProperties["cp_message"] = "Successfully deleted!";
                    Session["Refresh"] = "1";
                    break;

                case "CallbackContractCascade":
                    
                    Session["ContractCascaded"] = "1";
                    Session["ContractCascaded2"] = "1";
                    
                    Session["ContractRefNumber"] = null;
                    Session["ContractRefNumber"] = aglContractNumber.Text;

                    
                    break;
            }
        }
        protected void gv2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
             
            }
        }
        protected void BillingPrintOut_Init(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(aglBizPartnerCode.Text))
            //{
            //    sdsBillingPrintOut.SelectCommand = "SELECT [Text] AS PrintCode, Customer FROM Masterfile.FormLayout WHERE FormName IN ('WMSBIL') AND Customer = '" + aglBizPartnerCode.Text + "'";
            //    sdsBillingPrintOut.DataBind();
            //}
        }

        protected void gv2_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            //if (Session["ContractDatatable2"].ToString() == "1" && check == true)
            //{
            //    e.Handled = true;
            //    DataTable source = GetSelectedVal2();

            //    foreach (ASPxDataUpdateValues values in e.UpdateValues)
            //    {
            //        object[] keys = { values.NewValues["ServiceType"] };
            //        DataRow row = source.Rows.Find(keys);
            //        row["ServiceType"] = values.NewValues["ServiceType"];
            //        row["Description"] = values.NewValues["Description"];
            //        row["ServiceRate"] = values.NewValues["ServiceRate"];
            //        row["UnitOfMeasure"] = values.NewValues["UnitOfMeasure"];
            //        row["UnitOfMeasureBulk"] = values.NewValues["UnitOfMeasureBulk"];
            //        row["Vatable"] = values.NewValues["Vatable"];
            //        row["VATCode"] = values.NewValues["VATCode"];
            //        row["BillingType"] = values.NewValues["BillingType"];
            //        row["Period"] = values.NewValues["Period"];
            //        row["Remarks"] = values.NewValues["Remarks"];
            //        row["Type"] = values.NewValues["Type"];
            //    }

            //    foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //    {
            //        try
            //        {
            //            object[] keys = { values.Keys["ServiceType"] };
            //            source.Rows.Remove(source.Rows.Find(keys));
            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }
            //    }

            //    foreach (ASPxDataInsertValues values in e.InsertValues)
            //    {
            //        var LineNumber = values.NewValues["LineNumber"];
            //        var ServiceType = values.NewValues["ServiceType"];
            //        var Description = values.NewValues["Description"];
            //        var ServiceRate = values.NewValues["ServiceRate"];
            //        var UnitOfMeasure = values.NewValues["UnitOfMeasure"];
            //        var UnitOfMeasureBulk = values.NewValues["UnitOfMeasureBulk"];
            //        var Vatable = values.NewValues["Vatable"];
            //        var VATCode = values.NewValues["VATCode"];
            //        var BillingType = values.NewValues["BillingType"];
            //        var Period = values.NewValues["Period"];
            //        var Remarks = values.NewValues["Remarks"];
            //        var Type = values.NewValues["Type"];

            //        source.Rows.Add(LineNumber, ServiceType, Description, ServiceRate, UnitOfMeasure, UnitOfMeasureBulk,
            //            Vatable, VATCode, BillingType, Period, Remarks, Type);
            //    }

            //    foreach (DataRow dtrow in source.Rows)
            //    {
            //        _EntityDetailNon.ServiceType = dtrow["ServiceType"].ToString();
            //        _EntityDetailNon.Description = dtrow["Description"].ToString();
            //        _EntityDetailNon.ServiceRate = Convert.ToDecimal(dtrow["ServiceRate"].ToString());
            //        _EntityDetailNon.UnitOfMeasure = dtrow["UnitOfMeasure"].ToString();
            //        _EntityDetailNon.UnitOfMeasureBulk = dtrow["UnitOfMeasureBulk"].ToString();
            //        _EntityDetailNon.Vatable = Convert.ToBoolean(Convert.IsDBNull(dtrow["Vatable"]) ? false : dtrow["Vatable"]);
            //        _EntityDetailNon.VATCode = dtrow["VATCode"].ToString();
            //        _EntityDetailNon.BillingType = dtrow["BillingType"].ToString();
            //        _EntityDetailNon.Period = dtrow["Period"].ToString();
            //        _EntityDetailNon.Remarks = dtrow["Remarks"].ToString();
            //        _EntityDetailNon.Type = dtrow["Type"].ToString();

            //        _EntityDetailNon.AddContractDetailNon(_EntityDetailNon);
            //    }
            //}
        }

        protected void gv2_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ContractDetail2"] = null;
            }

            if (Session["ContractDetail2"] != null)
            {
                gv2.DataSourceID = sdsContractDetailNon.ID;
            }
        }

        protected void aglContractNumber_Init(object sender, EventArgs e)
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    look.ReadOnly = true;
                    look.DropDownButton.Enabled = false;

                    sdsContract.SelectCommand = "SELECT DocNumber, DocDate, ContractType, BizPartnerCode, DateFrom AS PeriodFrom, DateTo AS PeriodTo, EffectivityDate, BillingPeriodType, ProfitCenterCode, Status, WarehouseCode FROM WMS.Contract WHERE ISNULL(SubmittedBy,'') != ''";
                    sdsContract.DataBind();
                }
                else
                {

                    if (Request.QueryString["transtype"].ToString() == "WMSCTER")
                    {
                        sdsContract.SelectCommand = "SELECT DocNumber, DocDate, ContractType, BizPartnerCode, DateFrom AS PeriodFrom, DateTo AS PeriodTo, EffectivityDate, BillingPeriodType, ProfitCenterCode, Status, WarehouseCode FROM WMS.Contract WHERE ISNULL(SubmittedBy,'') != ''  AND Status IN ('ACTIVE','REVISED','RENEWED')";
                        sdsContract.DataBind();
                    }
                    else
                    {
                        sdsContract.SelectCommand = "SELECT DocNumber, DocDate, ContractType, BizPartnerCode, DateFrom AS PeriodFrom, DateTo AS PeriodTo, EffectivityDate, BillingPeriodType, ProfitCenterCode, Status, WarehouseCode FROM WMS.Contract WHERE ISNULL(SubmittedBy,'') != '' AND Status = 'ACTIVE'";
                        sdsContract.DataBind();
                    }
                 

                    if (txtType.Text == "NEW CONTRACT")
                    {
                        look.ReadOnly = true;
                        look.DropDownButton.Enabled = false;
                    }
                    else
                    {
                        look.ReadOnly = false;
                        look.DropDownButton.Enabled = true;
                    }
                }
            }
        }

        protected void aglBizPartnerCode_Load(object sender, EventArgs e)
        {
            var look = sender as ASPxGridLookup;
            //WMSCREN
            if (look != null)
            {
                if ((Request.QueryString["transtype"].ToString().Trim() != "WMSCREN") && (Request.QueryString["transtype"].ToString().Trim() != "WMSCTER"))
                {
                    look.ReadOnly = view;
                    look.DropDownButton.Enabled = !view;
                }
                else
                {
                    look.ReadOnly = true;
                    look.DropDownButton.Enabled = false;
                }
            }
        }

        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["VATCode"] = "NONV";
            e.NewValues["BillingType"] = "NEXT DAY";
            e.NewValues["Period"] = "SEMIMONTHLY";
            e.NewValues["BillingPrintOutStr"] = "REGULAR";
        }

        protected void gv2_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["VATCode"] = "NONV";
            e.NewValues["BillingType"] = "ACT";
            e.NewValues["Period"] = "SEMIMONTHLY";
        }
        protected void DefaultValues()
        {
            if (Session["ContractRefNumber"] != null)
            {
                aglContractNumber.Text = Session["ContractRefNumber"].ToString();
            }

            if (Session["ContractBizPartner"] != null)
            {
                aglBizPartnerCode.Text = Session["ContractBizPartner"].ToString();
            }
            
            if (Session["ContractProfit"] != null)
            {
                aglProfitCenter.Text = Session["ContractProfit"].ToString();
            }
            
            if (Session["ContractWH"] != null)
            {
                aglWarehouse.Text = Session["ContractWH"].ToString();
            }
        }
    }

}