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
using GearsAccounting;
using GearsProduction;

namespace GWL
{
    public partial class frmDIS : System.Web.UI.Page
    {
        Boolean error = false; 
        Boolean view = false; 
        Boolean check = false; 

        private static string Connection;

        Entity.DIS _Entity = new DIS(); 
        Entity.DIS.DISBillOfMaterial _EntityDetailApplication = new DIS.DISBillOfMaterial();
        Entity.DIS.DISSizeBreakdown _EntityDetailAdjustment = new DIS.DISSizeBreakdown();
        Entity.DIS.DISStep _EntityDetailStep = new DIS.DISStep(); 
        string val = "";

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

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
            }

            gv1.KeyFieldName = "LineNumber";
            gv2.KeyFieldName = "LineNumber";

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry and delete
            }

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["STStepFilterExpression"] = null;
                Session["BOMStepFilterExpression"] = null;
                Session["StepFilterExpression"] = null;
                Session["DISDatatable"] = null; 

                WordStatus();
                //General Tab
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //ADD CONN
                txtDISNumber.Value = _Entity.DISNumber;
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                dtpDueDate.Text = String.IsNullOrEmpty(_Entity.DueDate.ToString()) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DueDate.ToString()).ToShortDateString();
                glType.Value = _Entity.Type;
                glPIS.Value = _Entity.PIS;
                PISNumber();
                glCustomer.Value = _Entity.Customer;
                //txtStyleNo.Text = _Entity.StyleNo;
                glColor.Value = _Entity.Color;
                glOriginalDIS.Value = _Entity.OriginalDIS;
                txtJONumber.Text = _Entity.JONumber;
                glBrand.Value = _Entity.Brand;
                glCategory.Value = _Entity.Category;
                glGender.Value = _Entity.Gender;
                glFitting.Value = _Entity.Fitting;
                glDesigner.Value = _Entity.Designer;
                txtDISQty.Text = _Entity.DISQty.ToString();
                txtTotalDISDays.Text = _Entity.TotalDISDays.ToString();
                txtWashingInstruction.Text = _Entity.WashingInstruction.ToString();
                txtSpecs.Text = _Entity.Specs.ToString();
                txtShrinkage.Text = _Entity.Shrinkage.ToString();
                glFabric.Value = _Entity.Fabric.ToString();
                memRemarks.Text = _Entity.Remarks.ToString();
                glStepTemplateCode.Value = _Entity.StepTemplateCode;

                //Costing Tab
                txtCSTLaborCost.Text = _Entity.CSTLaborCost.ToString();
                txtCSTMaterialCost.Text = _Entity.CSTMaterialCost.ToString();
                txtCSTTotalDISCost.Text = _Entity.CSTTotalDISCost.ToString();

                //Customer Issuance and Return Tab
                txtDateSent.Text = _Entity.DateSent;
                txtReturnedDate.Text = _Entity.ReturnedDate;
                txtLastDateSent.Text = _Entity.LastDateSent;
                txtLastReturnedDate.Text = _Entity.LastReturnedDate;

                //Audit Trail Tab
                txtHAddedBy.Value = _Entity.AddedBy;
                txtHAddedDate.Value = _Entity.AddedDate;
                txtHLastEditedBy.Value = _Entity.LastEditedBy;
                txtHLastEditedDate.Value = _Entity.LastEditedDate;
                txtHSubmittedBy.Value = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = _Entity.SubmittedDate;
                txtHStartDISBy.Value = _Entity.StartDISBy;
                txtHStartDISDate.Value = _Entity.StartDISDate;
                txtHCancelledBy.Value = _Entity.CancelledBy;
                txtHCancelledDate.Value = _Entity.CancelledDate;

                //User Defined Tab
                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                OriginalDIS();

                updateBtn.Text = "Add";
                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                            updateBtn.Text = "Update";
                        else
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail1 = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.DISBillOfMaterial WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                DataTable dtbldetail2 = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.DISStep WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                DataTable dtbldetail3 = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.DISSizeBreakdown WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                
                gv1.DataSourceID = (dtbldetail1.Rows.Count > 0 ? "odsDetail1" : "sdsDetail1");
                gv2.DataSourceID = (dtbldetail2.Rows.Count > 0 ? "odsDetail2" : "sdsDetail2");
                gv3.DataSourceID = (dtbldetail3.Rows.Count > 0 ? "odsDetail3" : "sdsDetail3");
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
            gparam._TransType = "PRDDIS";
            string strresult = GearsProduction.GProduction.DesignInforSheet_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                if (look != null)
                {
                    look.Enabled = false;
                }
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsBehavior.AllowSort = false;
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || 
                e.ButtonType == ColumnCommandButtonType.Cancel ||
                e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;

            if (Request.QueryString["entry"] == "D" || 
                Request.QueryString["entry"] == "V")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || 
                        e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }
                }
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "D" || 
                Request.QueryString["entry"] == "V")
                {
                    if (e.ButtonID == "Delete1" || 
                        e.ButtonID == "Delete" || 
                        e.ButtonID == "Delete2")
                            e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
        }
        #endregion

        #region Lookup Settings
        protected void lookupItem_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["StepFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsItemDetail.FilterExpression = Session["StepFilterExpression"].ToString();
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
                DataTable countcolor = Gears.RetriveData2("Select c.ColorCode,c.ClassCode,c.SizeCode, ISNULL(EstimatedCost,0) AS Cost "
                                     + " from masterfile.item a left join masterfile.ItemDetail c on a.ItemCode = c.ItemCode left join masterfile.ItemCustomerPrice b "
                                     + " on A.ItemCode = B.ItemCode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["Cost"].ToString() + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                if (e.Parameters.Contains("ColorCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, LTRIM(RTRIM([ColorCode])) AS ColorCode, '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], LTRIM(RTRIM([ClassCode])) AS ClassCode, '' AS [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], LTRIM(RTRIM(A.[SizeCode])) AS SizeCode, SortOrder FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode where A.ItemCode = '" + itemcode + "' ORDER BY SortOrder ASC";
                }
                ASPxGridView grid = sender as ASPxGridView;
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


        //protected void lookupStep_Init(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridViewStep_CustomCallback);
        //    if (Session["STStepFilterExpression"] != null)
        //    {
        //        gridLookup.GridView.DataSourceID = "sdsSTStep";
        //        sdsSTStep.FilterExpression = Session["STStepFilterExpression"].ToString();
        //    }
        //}

        //public void gridViewStep_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string column = e.Parameters.Split('|')[0];//Set column name
        //    if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
        //    string Steppp = e.Parameters.Split('|')[1];//Set Item Code
        //    string val = e.Parameters.Split('|')[2];//Set column value
        //    if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
        //    var itemlookup = sender as ASPxGridView;
        //    string codes = "";

        //    if (e.Parameters.Contains("checkdoc"))
        //    {
        //        ASPxGridView grid = sender as ASPxGridView;
        //        string[] Step = Steppp.Split(';');
        //        CriteriaOperator selectionCriteria = new InOperator("Step", Step);
        //        CriteriaOperator not = new NotOperator(selectionCriteria);
        //        sdsSTStep.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not)).ToString();
        //        Session["STStepFilterExpression"] = sdsSTStep.FilterExpression;
        //        grid.DataSourceID = "sdsSTStep";
        //        grid.DataBind();

        //        for (int i = 0; i < grid.VisibleRowCount; i++)
        //        {
        //            if (grid.GetRowValues(i, "Step") != null)
        //                if (grid.GetRowValues(i, "Step").ToString() == val)
        //                {
        //                    grid.Selection.SelectRow(i);
        //                    string key = grid.GetRowValues(i, "Step").ToString();
        //                    grid.MakeRowVisible(key);
        //                    break;
        //                }
        //        }
        //    }
        //}

        protected void lookupBOM_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridViewBOM_CustomCallback);
            if (Session["BOMStepFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsBOMStep";
                sdsBOMStep.FilterExpression = Session["BOMStepFilterExpression"].ToString();
            }
        }

        public void gridViewBOM_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string Steppp = e.Parameters.Split('|')[1];//Set Item Code
            string Steppp1 = e.Parameters.Split('|')[2];//Set Item Code
            string val = e.Parameters.Split('|')[3];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("stepcode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                string[] Step = Steppp.Split(';');
                string[] Step1 = Steppp1.Split(';');
                CriteriaOperator selectionCriteria = new InOperator("Step", Step);
                CriteriaOperator selectionCriteria1 = new InOperator("Step", Step1);
                CriteriaOperator not = new NotOperator(selectionCriteria);
                sdsBOMStep.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1)).ToString();
                //sdsBOMStep.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria1, not)).ToString();
                Session["BOMStepFilterExpression"] = sdsBOMStep.FilterExpression;
                grid.DataSourceID = "sdsBOMStep";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "Step") != null)
                        if (grid.GetRowValues(i, "Step").ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "Step").ToString();
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
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DISNumber = txtDISNumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.DueDate = dtpDueDate.Text;
            _Entity.Type = glType.Text;
            _Entity.PIS = glPIS.Text;
            _Entity.Customer = glCustomer.Text;
            //_Entity.StyleNo = txtStyleNo.Text;
            _Entity.Color = glColor.Text;
            _Entity.OriginalDIS = glOriginalDIS.Text;
            _Entity.JONumber = txtJONumber.Text;
            _Entity.Brand = glBrand.Text;
            _Entity.Category = glCategory.Text;
            _Entity.Gender = glGender.Text;
            _Entity.Fitting = glFitting.Text;
            _Entity.Designer = glDesigner.Text;
            _Entity.DISQty = Convert.ToDecimal(Convert.IsDBNull(txtDISQty.Value) ? 0 : Convert.ToDecimal(txtDISQty.Value));
            _Entity.TotalDISDays = Convert.ToDecimal(Convert.IsDBNull(txtTotalDISDays.Value) ? 0 : Convert.ToDecimal(txtTotalDISDays.Value));
            _Entity.WashingInstruction = txtWashingInstruction.Text;
            _Entity.Specs = txtSpecs.Text;
            _Entity.Shrinkage = txtShrinkage.Text;
            _Entity.Fabric = glFabric.Text;
            _Entity.Remarks = memRemarks.Text;

            _Entity.StepTemplateCode = glStepTemplateCode.Text;
            _Entity.CSTLaborCost = Convert.ToDecimal(Convert.IsDBNull(txtCSTLaborCost.Value) ? 0 : Convert.ToDecimal(txtCSTLaborCost.Value));
            _Entity.CSTMaterialCost = Convert.ToDecimal(Convert.IsDBNull(txtCSTMaterialCost.Value) ? 0 : Convert.ToDecimal(txtCSTMaterialCost.Value));
            _Entity.CSTTotalDISCost = Convert.ToDecimal(Convert.IsDBNull(txtCSTTotalDISCost.Value) ? 0 : Convert.ToDecimal(txtCSTTotalDISCost.Value));

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    //gv1.UpdateEdit();
                    gv2.UpdateEdit();
                    //gv3.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber,"Production.DIS",1,Connection);//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of Updating header
                        
                        gv1.DataSourceID = "odsDetail1";
                        odsDetail1.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

                        if (Session["DISDatatable"] == "1")
                        {
                            gv2.KeyFieldName = "LineNumber";
                            gv2.UpdateEdit();
                        }
                        else
                        {
                            gv2.DataSourceID = "odsDetail2";
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv2.UpdateEdit();
                        }
                         
                        gv3.DataSourceID = "odsDetail3";
                        odsDetail3.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv3.UpdateEdit();
                        
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
                    Session["Refresh"] = "1";
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
                case "OriginalDIS":
                    OriginalDIS();
                    break;
                case "Generate":
                    GetSelectedVal();
                    break;
                case "PISNumber":
                    if (!String.IsNullOrEmpty(glPIS.Text.Trim())) 
                    {
                        DataTable getDIScount = Gears.RetriveData2("SELECT '-' + RIGHT('000' + CAST(COUNT(DISNumber) + 1 AS VARCHAR(3)),3)  AS Cnt from Production.DIS where RTRIM(LTRIM(ISNULL(DISNumber,''))) != '' and RTRIM(LTRIM(ISNULL(DISNumber,''))) like '" + glPIS.Text + "%'", Session["ConnString"].ToString());//ADD CONN
                        txtDISNumber.Text = glPIS.Text + getDIScount.Rows[0][0].ToString();
                        DataTable getPISvals = Gears.RetriveData2("Select A.PISNumber, A.PISDescription, ISNULL(CustomerCode,''), ISNULL(Brand,''), ISNULL(Gender,''), ISNULL(ProductCategory,''), ISNULL(FitCode,''), ISNULL(FabricCode,''), ISNULL(Designer,''), ISNULL(WashDescription,'') from Production.ProductInfoSheet A where ISNULL(CancelledBy,'') = '' AND A.PISNumber = '" + glPIS.Text + "'", Session["ConnString"].ToString());//ADD CONN
                        glCustomer.Value = getPISvals.Rows[0][2].ToString().Trim();
                        glBrand.Value = getPISvals.Rows[0][3].ToString();
                        glGender.Value = getPISvals.Rows[0][4].ToString();
                        glCategory.Value = getPISvals.Rows[0][5].ToString().Trim();
                        glFitting.Value = getPISvals.Rows[0][6].ToString();
                        glFabric.Value = getPISvals.Rows[0][7].ToString();
                        glDesigner.Value = getPISvals.Rows[0][8].ToString();
                        txtWashingInstruction.Text = getPISvals.Rows[0][9].ToString();
                    }
                    else
                    {
                        txtDISNumber.Text = "";
                        glCustomer.Text = "";
                        glBrand.Text = "";
                        glGender.Text = "";
                        glCategory.Text = "";
                        glFitting.Text = "";
                        glFabric.Text = "";
                        glDesigner.Text = "";
                        txtWashingInstruction.Text = ""; 
                    }
                    break; 
            }

        }
         
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false) 
            {
                e.Handled = true; 
            }

            if (Session["DISDatatable"] == "1" && check == true)
            {
                e.Handled = true; 
                DataTable source = GetSelectedVal(); 
                Gears.RetriveData2("DELETE FROM Production.DISStep WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    i++;
                    var LineNumber = i;
                    var Step = values.NewValues["Step"];
                    var WorkCenter = values.NewValues["WorkCenter"];
                    var LaborCost = values.NewValues["LaborCost"];
                    var TargetDateIN = values.NewValues["TargetDateIN"];
                    var TargetDateOUT = values.NewValues["TargetDateOUT"];
                    var SpecialInst = values.NewValues["SpecialInst"];
                    var DateIN = values.NewValues["DateIN"];
                    var DateOUT = values.NewValues["DateOUT"];
                    var Days = values.NewValues["Days"];
                    var Remarks = values.NewValues["Remarks"];  
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];

                    source.Rows.Add(LineNumber, Step, WorkCenter, LaborCost, TargetDateIN,  
                        TargetDateOUT, SpecialInst, DateIN, DateOUT, Days, Remarks,
                        Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9); 
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["Step"] = values.NewValues["Step"];
                    row["WorkCenter"] = values.NewValues["WorkCenter"];
                    row["LaborCost"] = values.NewValues["LaborCost"];
                    row["TargetDateIN"] = values.NewValues["TargetDateIN"];
                    row["TargetDateOUT"] = values.NewValues["TargetDateOUT"];
                    row["DateIN"] = values.NewValues["DateIN"];
                    row["DateOUT"] = values.NewValues["DateOUT"];
                    row["Days"] = values.NewValues["Days"];
                    row["Remarks"] = values.NewValues["Remarks"]; 
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

                foreach (DataRow dtRow in source.Rows)
                {
                    _EntityDetailStep.LineNumber = dtRow["LineNumber"].ToString();
                    _EntityDetailStep.Step = dtRow["Step"].ToString();
                    _EntityDetailStep.WorkCenter = dtRow["WorkCenter"].ToString();
                    _EntityDetailStep.LaborCost = Convert.ToDecimal(String.IsNullOrEmpty(dtRow["LaborCost"].ToString()) ? "0" : dtRow["LaborCost"].ToString());
                    _EntityDetailStep.TargetDateIN = Convert.ToDateTime(dtRow["TargetDateIN"].ToString());
                    _EntityDetailStep.TargetDateOUT = Convert.ToDateTime(dtRow["TargetDateOUT"].ToString());
                    _EntityDetailStep.SpecialInst = dtRow["SpecialInst"].ToString();
                    if (!Convert.IsDBNull(dtRow["DateIN"]))
                        _EntityDetailStep.DateIN = Convert.ToDateTime(dtRow["DateIN"].ToString());
                    if (!Convert.IsDBNull(dtRow["DateOUT"]))
                        _EntityDetailStep.DateOUT = Convert.ToDateTime(dtRow["DateOUT"].ToString());
                    _EntityDetailStep.Days = Convert.ToInt32(String.IsNullOrEmpty(dtRow["Days"].ToString()) ? "0" : dtRow["Days"].ToString()); 
                    _EntityDetailStep.Remarks = dtRow["Remarks"].ToString(); 
                    _EntityDetailStep.Field1 = dtRow["Field1"].ToString();
                    _EntityDetailStep.Field2 = dtRow["Field2"].ToString();
                    _EntityDetailStep.Field3 = dtRow["Field3"].ToString();
                    _EntityDetailStep.Field4 = dtRow["Field4"].ToString();
                    _EntityDetailStep.Field5 = dtRow["Field5"].ToString();
                    _EntityDetailStep.Field6 = dtRow["Field6"].ToString();
                    _EntityDetailStep.Field7 = dtRow["Field7"].ToString();
                    _EntityDetailStep.Field8 = dtRow["Field8"].ToString();
                    _EntityDetailStep.Field9 = dtRow["Field9"].ToString();
                    _EntityDetailStep.AddDISStep(_EntityDetailStep); 
                }
            }
        }

        private DataTable GetSelectedVal()
        {
            Session["DISDatatable"] = "0";
            gv2.DataSource = null;
            gv2.DataSourceID = null;

            DataTable dt = new DataTable();
            sdsDetail2.SelectCommand = " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StepCode) AS VARCHAR(5)),5) AS LineNumber, StepCode AS Step, "
	                                 + " '' AS WorkCenter, 0.0000 AS LaborCost, GETDATE() AS TargetDateIN, GETDATE() As TargetDateOUT, '' AS SpecialInst, NULL AS DateIN, NULL AS DateOUT, "
	                                 + " 0 AS Days, '' AS Remarks, '' AS Field1, '' AS Field2, '' AS Field3, '' AS Field4, '' AS Field5, '' AS Field6, '' AS Field7, "
                                     + " '' AS Field8, '' AS Field9 FROM Masterfile.StepTemplateDetail WHERE StepTemplateCode = '" + glStepTemplateCode.Text + "'";
            gv2.DataSource = sdsDetail2;
            gv2.DataBind();
            Session["DISDatatable"] = "1";

            foreach (GridViewColumn col in gv2.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv2.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns) {
                    row[col.ColumnName] = gv2.GetRowValues(i, col.ColumnName); 
                }
            }

            dt.PrimaryKey = new DataColumn[] { dt.Columns["LineNumber"] };
            return dt;
        } 
        #endregion

        protected void WordStatus()
        {
            DataTable mod = new DataTable();
            mod = Gears.RetriveData2("SELECT Status FROM Production.DIS WHERE DocNumber = '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

            foreach (DataRow md in mod.Rows)
            {
                switch (md["Status"].ToString())
                {
                    case "N":
                        txtStatus.Text = "NEW";
                        break;
                    case "C":
                        txtStatus.Text = "CLOSED";
                        break;
                    case "W":
                        txtStatus.Text = "WIP";
                        break;
                }
            }
        }

        public void OriginalDIS()
        {
            if (glType.Text == "Development")
            {
                glOriginalDIS.ClientEnabled = false;
                glOriginalDIS.Text = "";
            }
            else
            {
                glOriginalDIS.ClientEnabled = true;
            }
        }

        public void PISNumber()
        {
            bool enbdis = glPIS.Text == "" ? true : false; 
            txtDISNumber.ClientEnabled = enbdis;
            glCustomer.ClientEnabled = enbdis;
            glBrand.ClientEnabled = enbdis;
            glGender.ClientEnabled = enbdis;
            glCategory.ClientEnabled = enbdis;
            glFitting.ClientEnabled = enbdis;
            glFabric.ClientEnabled = enbdis;
            txtWashingInstruction.ClientEnabled = enbdis; 
        }


        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}