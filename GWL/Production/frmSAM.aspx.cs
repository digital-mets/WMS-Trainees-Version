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
using GearsWarehouseManagement;

namespace GWL
{
    public partial class frmSAM : System.Web.UI.Page
    {
        Boolean error = false; 
        Boolean view = false; 
        Boolean check = false; 

        Entity.SAM _Entity = new SAM(); 
  
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

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true; 
            }
            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            if (!IsPostBack)
            { 
                Common.Common.HideUDF(frmlayout1, "udf", Session["ConnString"].ToString());
                Common.Common.preventAutoCloseCheck(glcheck, Session["ConnString"].ToString());

                if (Request.QueryString["entry"].ToString() == "N")
                {

                    speEfficiencyC.Value = 100;
                    speAllowanceC.Value = 30;
                    speMarkupC.Value = 200;

                    speEfficiencyS.Value = 100;
                    speAllowanceS.Value = 30;
                    speMarkupS.Value = 200;

                    speEfficiencyF.Value = 100;
                    speAllowanceF.Value = 30;
                    speMarkupF.Value = 200;

                    speEfficiencyW.Value = 100;
                    speAllowanceW.Value = 30;
                    speMarkupW.Value = 200;

                    speEfficiencyE.Value = 100;
                    speAllowanceE.Value = 30;
                    speMarkupE.Value = 200;

                    speEfficiencyP.Value = 100;
                    speAllowanceP.Value = 30;
                    speMarkupP.Value = 200;

                }
                else
                {

                    txtDocNumber.ReadOnly = true;
                    txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                    txtDocNumber.Text = _Entity.DocNumber;
                    dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                    glSupplierCodeH.Text = _Entity.SupplierCode;
                    txtSName.Text = _Entity.Name ;
                    txtDISNumber.Text = _Entity.DISNumber;
                    cbBrand.Text = _Entity.Brand;
                    cbGender.Text = _Entity.Gender;
                    cbProductCategory.Text = _Entity.ProductCategory;
                    ProductSubCategoryLookup.SelectCommand = "SELECT Description as ProductSubCatCode, ProductSubCatCode as Description FROM Masterfile.ProductCategorySub WHERE ISNULL(IsInactive,0)=0   AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value.ToString().Replace(" ", "") + "%' AND ISNULL(Gender,'') like '%" + cbGender.Value.ToString().Replace(" ", "") + "%'";
                    glProductSubCategory.Value = _Entity.ProductSubCategory.ToString();
                    ProductGroupLookup.SelectCommand = "SELECT  Description as ProductGroupCode,Description FROM Masterfile.ProductGroup WHERE ISNULL(IsInactive,0)=0   AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value.ToString().Replace(" ", "") + "%'";
                    glProductGroup.Value = _Entity.ProductGroup.ToString();
                    cbFitCode.Text = _Entity.FitCode;
                    cbProductClass.Text = _Entity.ProductClass;
                    txtDesignDesc.Text = _Entity.DesignDesc;
                    txtRemarks.Text = _Entity.Remarks;

                    speCMPCost.Value = _Entity.CMPCost.ToString();
                    speCuttingCost.Value = _Entity.CuttingCost.ToString();
                    speSewingCost.Value = _Entity.SewingCost.ToString();
                    speFinishingCost.Value = _Entity.FinishingCost.ToString();
                    speWashing.Value = _Entity.WashingCost.ToString();
                    speEmbro.Value = _Entity.EmbroideryCost.ToString();
                    spePrintingCost.Value = _Entity.PrintingCost.ToString();
                    speTotalCost.Value = _Entity.TotalCost.ToString();


                    speEfficiencyC.Value = _Entity.CEfficiency.ToString();
                    speAllowanceC.Value = _Entity.CAllowance.ToString();
                    speTotalObservedTimeC.Value = _Entity.CTotalObserved.ToString();
                    speBasicMinutesC.Value = _Entity.CBasicMinutes.ToString();
                    speSAMC.Value = _Entity.CSAM.ToString();
                    speMinimumWageC.Value = _Entity.CMinimumWage.ToString();
                    speLaborCostC.Value = _Entity.CLaborCost.ToString();
                    speMarkupC.Value = _Entity.CMarkup.ToString();
                    speCostC.Value = _Entity.CCost.ToString();


                    speEfficiencyS.Value = _Entity.SEfficiency.ToString();
                    speAllowanceS.Value = _Entity.SAllowance.ToString();
                    speTotalObservedTimeS.Value = _Entity.STotalObserved.ToString();
                    speBasicMinutesS.Value = _Entity.SBasicMinutes.ToString();
                    speSAMS.Value = _Entity.SSAM.ToString();
                    speMinimumWageS.Value = _Entity.SMinimumWage.ToString();
                    speLaborCostS.Value = _Entity.SLaborCost.ToString();
                    speMarkupS.Value = _Entity.SMarkup.ToString();
                    speCostS.Value = _Entity.SCost.ToString();

                    speEfficiencyF.Value = _Entity.FEfficiency.ToString();
                    speAllowanceF.Value = _Entity.FAllowance.ToString();
                    speTotalObservedTimeF.Value = _Entity.FTotalObserved.ToString();
                    speBasicMinutesF.Value = _Entity.FBasicMinutes.ToString();
                    speSAMF.Value = _Entity.FSAM.ToString();
                    speMinimumWageF.Value = _Entity.FMinimumWage.ToString();
                    speLaborCostF.Value = _Entity.FLaborCost.ToString();
                    speMarkupF.Value = _Entity.FMarkup.ToString();
                    speCostF.Value = _Entity.FCost.ToString();


                    speEfficiencyW.Value = _Entity.WEfficiency.ToString();
                    speAllowanceW.Value = _Entity.WAllowance.ToString();
                    speTotalObservedTimeW.Value = _Entity.WTotalObserved.ToString();
                    speBasicMinutesW.Value = _Entity.WBasicMinutes.ToString();
                    speSAMW.Value = _Entity.WSAM.ToString();
                    speMinimumWageW.Value = _Entity.WMinimumWage.ToString();
                    speLaborCostW.Value = _Entity.WLaborCost.ToString();
                    speMarkupW.Value = _Entity.WMarkup.ToString();
                    speCostW.Value = _Entity.WCost.ToString();


                    speEfficiencyE.Value = _Entity.EEfficiency.ToString();
                    speAllowanceE.Value = _Entity.EAllowance.ToString();
                    speTotalObservedTimeE.Value = _Entity.ETotalObserved.ToString();
                    speBasicMinutesE.Value = _Entity.EBasicMinutes.ToString();
                    speSAME.Value = _Entity.ESAM.ToString();
                    speMinimumWageE.Value = _Entity.EMinimumWage.ToString();
                    speLaborCostE.Value = _Entity.ELaborCost.ToString();
                    speMarkupE.Value = _Entity.EMarkup.ToString();
                    speCostE.Value = _Entity.ECost.ToString();



                    speEfficiencyP.Value = _Entity.PEfficiency.ToString();
                    speAllowanceP.Value = _Entity.PAllowance.ToString();
                    speTotalObservedTimeP.Value = _Entity.PTotalObserved.ToString();
                    speBasicMinutesP.Value = _Entity.PBasicMinutes.ToString();
                    speSAMP.Value = _Entity.PSAM.ToString();
                    speMinimumWageP.Value = _Entity.PMinimumWage.ToString();
                    speLaborCostP.Value = _Entity.PLaborCost.ToString();
                    speMarkupP.Value = _Entity.PMarkup.ToString();
                    speCostP.Value = _Entity.PCost.ToString();

                    txtFrontImage64string.Text = _Entity.FrontImage;
                    txtBackImage64string.Text = _Entity.BackImage;

                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;

                    txtForceClosedBy.Text = _Entity.ForceClosedBy;
                    txtForceClosedDate.Text = _Entity.ForceClosedDate;
                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;
                    cbRegionC.Text = _Entity.CRegion;
                    cbRegionS.Text = _Entity.SRegion;
                    cbRegionF.Text = _Entity.FRegion;
                    cbRegionW.Text = _Entity.WRegion;
                    cbRegionE.Text = _Entity.ERegion;
                    cbRegionP.Text = _Entity.PRegion;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N": 
                        updateBtn.Text = "Add";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        btnBackUpload.Enabled = false;
                        btnFrontUpload.Enabled = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }
                
                DataTable dtcutting = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.SAMDetail WHERE DocNumber ='" + txtDocNumber.Text + "' AND Type='C'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtcutting.Rows.Count > 0 ? "odsCutting" : "sdsCutting");

                DataTable dtSewing = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.SAMDetail WHERE DocNumber ='" + txtDocNumber.Text + "' AND Type='S'", Session["ConnString"].ToString());//ADD CONN
                gvS.DataSourceID = (dtSewing.Rows.Count > 0 ? "odsSewing" : "sdsSewing");


                DataTable dtFinishing = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.SAMDetail WHERE DocNumber ='" + txtDocNumber.Text + "' AND Type='F'", Session["ConnString"].ToString());//ADD CONN
                gvF.DataSourceID = (dtFinishing.Rows.Count > 0 ? "odsFinishing" : "sdsFinishing");


                DataTable dtWashing = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.SAMDetail WHERE DocNumber ='" + txtDocNumber.Text + "' AND Type='W'", Session["ConnString"].ToString());//ADD CONN
                gvW.DataSourceID = (dtWashing.Rows.Count > 0 ? "odsWashing" : "sdsWashing");


                DataTable dtEmbroidery = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.SAMDetail WHERE DocNumber ='" + txtDocNumber.Text + "' AND Type='E'", Session["ConnString"].ToString());//ADD CONN
                gvE.DataSourceID = (dtEmbroidery.Rows.Count > 0 ? "odsEmbroidery" : "sdsEmbroidery");


                DataTable dtPrinting = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.SAMDetail WHERE DocNumber ='" + txtDocNumber.Text + "' AND Type='P'", Session["ConnString"].ToString());//ADD CONN
                gvP.DataSourceID = (dtPrinting.Rows.Count > 0 ? "odsPrinting" : "sdsPrinting");
               
             

             
            }
            SetLookupFilter();
            if (!string.IsNullOrEmpty(txtFrontImage64string.Text))
                dxFrontImage.ImageUrl = "data:image/jpg;base64," + txtFrontImage64string.Text;
            dxFrontImage.LargeImageUrl = "data:image/jpg;base64," + txtFrontImage64string.Text;

            if (!string.IsNullOrEmpty(txtBackImage64string.Text))
                dxBackImage.ImageUrl = "data:image/jpg;base64," + txtBackImage64string.Text;
            dxBackImage.LargeImageUrl = "data:image/jpg;base64," + txtBackImage64string.Text;
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
        }
        protected void UploadControlLoad(object sender, EventArgs e)
        {
            ASPxUploadControl uploadcontrol = sender as ASPxUploadControl;
            uploadcontrol.Enabled = !view;
        }
        protected void ImageLoad(object sender, EventArgs e)
        {
            ASPxImage image = sender as ASPxImage;
            image.ReadOnly = view;
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { }

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
        }

        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        { }

  
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
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
            {
                e.Visible = false;
            }
            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
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
         
        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN

            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.SupplierCode = glSupplierCodeH.Text;
    
            _Entity.DISNumber = txtDISNumber.Text;
            _Entity.Brand = cbBrand.Text;
            _Entity.Gender = cbGender.Text;
            _Entity.ProductCategory = cbProductCategory.Text;
            _Entity.ProductSubCategory = glProductSubCategory.Text;
            _Entity.ProductGroup = glProductGroup.Text;
            _Entity.FitCode = cbFitCode.Text;
            _Entity.ProductClass = cbProductClass.Text;
            _Entity.DesignDesc = txtDesignDesc.Text;
            _Entity.Remarks = txtRemarks.Text;
            _Entity.CMPCost = Convert.ToDecimal(Convert.IsDBNull(speCMPCost.Value) ? "0" : speCMPCost.Value);
            _Entity.CuttingCost = Convert.ToDecimal(Convert.IsDBNull(speCuttingCost.Value) ? "0" : speCuttingCost.Value);
            _Entity.SewingCost = Convert.ToDecimal(Convert.IsDBNull(speSewingCost.Value) ? "0" : speSewingCost.Value);
            _Entity.FinishingCost = Convert.ToDecimal(Convert.IsDBNull(speFinishingCost.Value) ? "0" : speFinishingCost.Value);
            _Entity.WashingCost = Convert.ToDecimal(Convert.IsDBNull(speWashing.Value) ? "0" : speWashing.Value);
            _Entity.EmbroideryCost = Convert.ToDecimal(Convert.IsDBNull(speEmbro.Value) ? "0" : speEmbro.Value);
            _Entity.PrintingCost = Convert.ToDecimal(Convert.IsDBNull(spePrintingCost.Value) ? "0" : spePrintingCost.Value);
            _Entity.TotalCost = Convert.ToDecimal(Convert.IsDBNull(speTotalCost.Value) ? "0" : speTotalCost.Value);


            _Entity.CEfficiency = Convert.ToDecimal(Convert.IsDBNull(speEfficiencyC.Value) ? "0" : speEfficiencyC.Value);
            _Entity.CAllowance = Convert.ToDecimal(Convert.IsDBNull(speAllowanceC.Value) ? "0" : speAllowanceC.Value);
            _Entity.CTotalObserved = Convert.ToDecimal(Convert.IsDBNull(speTotalObservedTimeC.Value) ? "0" : speTotalObservedTimeC.Value);
            _Entity.CBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(speBasicMinutesC.Value) ? "0" : speBasicMinutesC.Value);
            _Entity.CSAM = Convert.ToDecimal(Convert.IsDBNull(speSAMC.Value) ? "0" : speSAMC.Value);
            _Entity.CMinimumWage = Convert.ToDecimal(Convert.IsDBNull(speMinimumWageC.Value) ? "0" : speMinimumWageC.Value);
            _Entity.CLaborCost = Convert.ToDecimal(Convert.IsDBNull(speLaborCostC.Value) ? "0" : speLaborCostC.Value);
            _Entity.CMarkup = Convert.ToDecimal(Convert.IsDBNull(speMarkupC.Value) ? "0" : speMarkupC.Value);
            _Entity.CCost = Convert.ToDecimal(Convert.IsDBNull(speCostC.Value) ? "0" : speCostC.Value);


            _Entity.SEfficiency = Convert.ToDecimal(Convert.IsDBNull(speEfficiencyS.Value) ? "0" : speEfficiencyS.Value);
            _Entity.SAllowance = Convert.ToDecimal(Convert.IsDBNull(speAllowanceS.Value) ? "0" : speAllowanceS.Value);
            _Entity.STotalObserved = Convert.ToDecimal(Convert.IsDBNull(speTotalObservedTimeS.Value) ? "0" : speTotalObservedTimeS.Value);
            _Entity.SBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(speBasicMinutesS.Value) ? "0" : speBasicMinutesS.Value);
            _Entity.SSAM = Convert.ToDecimal(Convert.IsDBNull(speSAMS.Value) ? "0" : speSAMS.Value);
            _Entity.SMinimumWage = Convert.ToDecimal(Convert.IsDBNull(speMinimumWageS.Value) ? "0" : speMinimumWageS.Value);
            _Entity.SLaborCost = Convert.ToDecimal(Convert.IsDBNull(speLaborCostS.Value) ? "0" : speLaborCostS.Value);
            _Entity.SMarkup = Convert.ToDecimal(Convert.IsDBNull(speMarkupS.Value) ? "0" : speMarkupS.Value);
            _Entity.SCost = Convert.ToDecimal(Convert.IsDBNull(speCostS.Value) ? "0" : speCostS.Value);

            _Entity.FEfficiency = Convert.ToDecimal(Convert.IsDBNull(speEfficiencyF.Value) ? "0" : speEfficiencyF.Value);
            _Entity.FAllowance = Convert.ToDecimal(Convert.IsDBNull(speAllowanceF.Value) ? "0" : speAllowanceF.Value);
            _Entity.FTotalObserved = Convert.ToDecimal(Convert.IsDBNull(speTotalObservedTimeF.Value) ? "0" : speTotalObservedTimeF.Value);
            _Entity.FBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(speBasicMinutesF.Value) ? "0" : speBasicMinutesF.Value);
            _Entity.FSAM = Convert.ToDecimal(Convert.IsDBNull(speSAMF.Value) ? "0" : speSAMF.Value);
            _Entity.FMinimumWage = Convert.ToDecimal(Convert.IsDBNull(speMinimumWageF.Value) ? "0" : speMinimumWageF.Value);
            _Entity.FLaborCost = Convert.ToDecimal(Convert.IsDBNull(speLaborCostF.Value) ? "0" : speLaborCostF.Value);
            _Entity.FMarkup = Convert.ToDecimal(Convert.IsDBNull(speMarkupF.Value) ? "0" : speMarkupF.Value);
            _Entity.FCost = Convert.ToDecimal(Convert.IsDBNull(speCostF.Value) ? "0" : speCostF.Value);


            _Entity.WEfficiency = Convert.ToDecimal(Convert.IsDBNull(speEfficiencyW.Value) ? "0" : speEfficiencyW.Value);
            _Entity.WAllowance = Convert.ToDecimal(Convert.IsDBNull(speAllowanceW.Value) ? "0" : speAllowanceW.Value);
            _Entity.WTotalObserved = Convert.ToDecimal(Convert.IsDBNull(speTotalObservedTimeW.Value) ? "0" : speTotalObservedTimeW.Value);
            _Entity.WBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(speBasicMinutesW.Value) ? "0" : speBasicMinutesW.Value);
            _Entity.WSAM = Convert.ToDecimal(Convert.IsDBNull(speSAMW.Value) ? "0" : speSAMW.Value);
            _Entity.WMinimumWage = Convert.ToDecimal(Convert.IsDBNull(speMinimumWageW.Value) ? "0" : speMinimumWageW.Value);
            _Entity.WLaborCost = Convert.ToDecimal(Convert.IsDBNull(speLaborCostW.Value) ? "0" : speLaborCostW.Value);
            _Entity.WMarkup = Convert.ToDecimal(Convert.IsDBNull(speMarkupW.Value) ? "0" : speMarkupW.Value);
            _Entity.WCost = Convert.ToDecimal(Convert.IsDBNull(speCostW.Value) ? "0" : speCostW.Value);


            _Entity.EEfficiency = Convert.ToDecimal(Convert.IsDBNull(speEfficiencyE.Value) ? "0" : speEfficiencyE.Value);
            _Entity.EAllowance = Convert.ToDecimal(Convert.IsDBNull(speAllowanceE.Value) ? "0" : speAllowanceE.Value);
            _Entity.ETotalObserved = Convert.ToDecimal(Convert.IsDBNull(speTotalObservedTimeE.Value) ? "0" : speTotalObservedTimeE.Value);
            _Entity.EBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(speBasicMinutesE.Value) ? "0" : speBasicMinutesE.Value);
            _Entity.ESAM = Convert.ToDecimal(Convert.IsDBNull(speSAME.Value) ? "0" : speSAME.Value);
            _Entity.EMinimumWage = Convert.ToDecimal(Convert.IsDBNull(speMinimumWageE.Value) ? "0" : speMinimumWageE.Value);
            _Entity.ELaborCost = Convert.ToDecimal(Convert.IsDBNull(speLaborCostE.Value) ? "0" : speLaborCostE.Value);
            _Entity.EMarkup = Convert.ToDecimal(Convert.IsDBNull(speMarkupE.Value) ? "0" : speMarkupE.Value);
            _Entity.ECost = Convert.ToDecimal(Convert.IsDBNull(speCostE.Value) ? "0" : speCostE.Value);


            _Entity.PEfficiency = Convert.ToDecimal(Convert.IsDBNull(speEfficiencyP.Value) ? "0" : speEfficiencyP.Value);
            _Entity.PAllowance = Convert.ToDecimal(Convert.IsDBNull(speAllowanceP.Value) ? "0" : speAllowanceP.Value);
            _Entity.PTotalObserved = Convert.ToDecimal(Convert.IsDBNull(speTotalObservedTimeP.Value) ? "0" : speTotalObservedTimeP.Value);
            _Entity.PBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(speBasicMinutesP.Value) ? "0" : speBasicMinutesP.Value);
            _Entity.PSAM = Convert.ToDecimal(Convert.IsDBNull(speSAMP.Value) ? "0" : speSAMP.Value);
            _Entity.PMinimumWage = Convert.ToDecimal(Convert.IsDBNull(speMinimumWageP.Value) ? "0" : speMinimumWageP.Value);
            _Entity.PLaborCost = Convert.ToDecimal(Convert.IsDBNull(speLaborCostP.Value) ? "0" : speLaborCostP.Value);
            _Entity.PMarkup = Convert.ToDecimal(Convert.IsDBNull(speMarkupP.Value) ? "0" : speMarkupP.Value);
            _Entity.PCost = Convert.ToDecimal(Convert.IsDBNull(speCostP.Value) ? "0" : speCostP.Value);

      
            _Entity.FrontImage = txtFrontImage64string.Text;
            _Entity.BackImage = txtBackImage64string.Text; 
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();

           _Entity.CRegion = cbRegionC.Text;
           _Entity.SRegion = cbRegionS.Text;
           _Entity.FRegion = cbRegionF.Text;
           _Entity.WRegion = cbRegionW.Text;
           _Entity.ERegion = cbRegionE.Text;
           _Entity.PRegion = cbRegionP.Text;

            switch (e.Parameter)
            {
                case "Add":

                      gv1.UpdateEdit();
                       gvS.UpdateEdit();
                       gvF.UpdateEdit();
                       gvW.UpdateEdit();
                       gvE.UpdateEdit();
                       gvP.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber,"Production.SAM",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }

                    string Result = "";
                    
                    Result= _Entity.UpdateData(_Entity); 

                    if (!String.IsNullOrEmpty(Result))
                    {
                        error = true;
                    }


                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsCutting";//Renew datasourceID to entity
                        odsCutting.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid

                        gvS.DataSourceID = "odsSewing";//Renew datasourceID to entity
                        odsSewing.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvS.UpdateEdit();//2nd initiation to insert grid

                        gvF.DataSourceID = "odsFinishing";//Renew datasourceID to entity
                        odsFinishing.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvF.UpdateEdit();//2nd initiation to insert grid

                        gvW.DataSourceID = "odsWashing";//Renew datasourceID to entity
                        odsWashing.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvW.UpdateEdit();//2nd initiation to insert grid

                        gvE.DataSourceID = "odsEmbroidery";//Renew datasourceID to entity
                        odsEmbroidery.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvE.UpdateEdit();//2nd initiation to insert grid


                        gvP.DataSourceID = "odsPrinting";//Renew datasourceID to entity
                        odsPrinting.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvP.UpdateEdit();//2nd initiation to insert grid


                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Result)) //Database Validation
                        {
                            cp.JSProperties["cp_message"] = Result;
                            cp.JSProperties["cp_success"] = true;
                        }
                        else //Client Size Validation
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
                    }

                    break;

                case "Update":
                       gv1.UpdateEdit();
                       gvS.UpdateEdit();
                       gvF.UpdateEdit();
                       gvW.UpdateEdit();
                       gvE.UpdateEdit();
                       gvP.UpdateEdit();


                    string strError1 = Functions.Submitted(_Entity.DocNumber,"Production.SAM",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError1))
                        {
                            cp.JSProperties["cp_message"] = strError1;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }

                    string ResultU = "";

                    ResultU = _Entity.UpdateData(_Entity);

                    if (!String.IsNullOrEmpty(ResultU))
                    {
                        error = true;
                    }


                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);

                        gv1.DataSourceID = "odsCutting";//Renew datasourceID to entity
                        odsCutting.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid

                        gvS.DataSourceID = "odsSewing";//Renew datasourceID to entity
                        odsSewing.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvS.UpdateEdit();//2nd initiation to insert grid

                        gvF.DataSourceID = "odsFinishing";//Renew datasourceID to entity
                        odsFinishing.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvF.UpdateEdit();//2nd initiation to insert grid

                        gvW.DataSourceID = "odsWashing";//Renew datasourceID to entity
                        odsWashing.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvW.UpdateEdit();//2nd initiation to insert grid

                        gvE.DataSourceID = "odsEmbroidery";//Renew datasourceID to entity
                        odsEmbroidery.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvE.UpdateEdit();//2nd initiation to insert grid


                        gvP.DataSourceID = "odsPrinting";//Renew datasourceID to entity
                        odsPrinting.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gvP.UpdateEdit();//2nd initiation to insert grid

                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(ResultU)) //Database Validation
                        {
                            cp.JSProperties["cp_message"] = ResultU;
                            cp.JSProperties["cp_success"] = true;
                        }
                        else //Client Size Validation
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
                    }

                    break;

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;


                case "Generate":
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    break;

                case "ConfDelete":
                   _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully Deleted"; 
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true; 
                    break;
                case "gendercodefiltercase":
                    glProductSubCategory.Text = "";
                  
                    break;
                case "productcatergoryfiltercase":
                    glProductSubCategory.Text = "";
                    glProductGroup.Text = "";
                    break;
            }
        } 
      #endregion

        protected void btnFrontUpload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {

            txtFrontImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }
        protected void btnBackUpload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            txtBackImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }


        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }



        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

    
 

        protected void Errorchecker_Callback(object source, CallbackEventArgs e)
        {
            string[] parameters = e.Parameter.Split('|');
            DataTable checkCode = null;
            //Errorchecker.JSProperties["s_lookupGrid"] = 
            switch (parameters[0])
            {
                case "OBCode":
                    checkCode = Gears.RetriveData2("Select OBCode from Masterfile.OpsBreakdown where OBCode = '" + parameters[1] + "' AND ISNULL(IsInactive,0)=0", Session["ConnString"].ToString());
                    if (checkCode.Rows.Count == 0)
                        e.Result = string.Format("'{0}' is invalid value", parameters[1]);
                    break;
               
            }
        }

        protected void CallbackFunc_Callback(object source, CallbackEventArgs e)
        {
            string column = e.Parameter.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameter.Split('|')[1];//Set Item Code
            string val = e.Parameter.Split('|')[2];//Set column value
            string val2 = "";
            try
            {
                val2 = e.Parameter.Split('|')[3];
            }
            catch (Exception)
            {
                val2 = "";
            }
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            //var itemlookup = sender as ASPxGridView;
           
            string codes = "";

            if (val2 == "")
            {
                val2 = "0";
            }

            if (e.Parameter.Contains("OBCode"))
            {
                DataTable getdatail = Gears.RetriveData2("Select Step,Parts,OpsDescription,MachineType,ObservedTime,Video FROM masterfile.OpsBreakdown Where OBCode = '" + itemcode + "' AND ISNULL(IsInactive,0)=0", Session["ConnString"].ToString());//ADD CONN



                    codes += getdatail.Rows[0]["Step"].ToString() + ";";
                    codes += getdatail.Rows[0]["Parts"].ToString() + ";";
                    codes += getdatail.Rows[0]["OpsDescription"].ToString() + ";";
                    codes += getdatail.Rows[0]["MachineType"].ToString() + ";";
                    codes += getdatail.Rows[0]["ObservedTime"].ToString() + ";";
                    codes += getdatail.Rows[0]["Video"].ToString() + ";";
                

                CallbackFunc.JSProperties["cp_codes"] = codes;
            }

        }

        protected void gvData_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "OperationCode")
            {
                e.Editor.ClientInstanceName = e.Column.FieldName;
                GridPop.JSProperties["cp_Field"] = e.Editor;
            }
        }

        protected void GridPop_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            string Field = e.Parameter.Split(';')[0];
            string Search = e.Parameter.Split(';')[1];
            string Code = e.Parameter.Split(';')[2];
            gvData.Columns.Clear();
            gvData.FilterExpression = "";

            switch (Field)
            {
              
                case "OBCode":
                    DataTable OBDT = Gears.RetriveData2("select OBCode as OperationCode ,OpsDescription as Description,OperationType from masterfile.OpsBreakdown Where ProductCategory = '" + cbProductCategory.Text + "' AND Brand  = '" + cbBrand.Text + "' AND ProductClass ='MODIFIED' AND OperationType= '" + Code + "' AND Gender ='" + cbGender.Text +"'   ", Session["ConnString"].ToString());
                    Cache["lastDT"] = OBDT;
                    Cache["lastkey"] = "OperationCode";
                     gvData.DataBind();
                     (gvData.Columns["OperationCode"] as GridViewDataColumn).AutoFilterBy(Search);
                    break;
            
            }
        }

        protected void gvData_DataBinding(object sender, EventArgs e)
        {
            if (Cache["lastDT"] != null)
            {
                gvData.KeyFieldName = Cache["lastkey"].ToString();
                (sender as ASPxGridView).DataSource = Cache["lastDT"];
            }
        }

        protected void gvData_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewDataColumn _col in gvData.DataColumns)
            {
                _col.Settings.AutoFilterCondition = AutoFilterCondition.Contains; //Auto filter condition to contains
            }
        }
        protected void ASPxTextBox_Init(object sender, EventArgs e)
        {
            (sender as ASPxTextBox).Attributes.Add("ondblclick", "MyScript()");
        }
        private DataTable GetSelectedVal()
        {

            gvRefC.DataSourceID = null;
            

            DataTable dt = new DataTable();

            DataTable getDetail = Gears.RetriveData2("select OBCode,Step,Parts,OpsDescription,MachineType,ObservedTime,Video,OperationType from masterfile.OpsBreakdown  Where ProductCategory = '" + cbProductCategory.Text + "' AND Brand  = '" + cbBrand.Text + "' AND Gender = '" + cbGender.Text +"' AND ProductClass ='BASIC' ORDER BY OperationType ASC   ", Session["ConnString"].ToString());

            gvRefC.DataSource = getDetail;
            gvRefC.DataBind();

         
            gvRefC.KeyFieldName = "OBCode";
       


            return dt;
        }
        protected void SetLookupFilter()
        {

            if (!String.IsNullOrEmpty(cbProductCategory.Text) && !String.IsNullOrEmpty(cbGender.Text))
            {
                ProductSubCategoryLookup.SelectCommand = "SELECT ProductSubCatCode, Description FROM Masterfile.ProductCategorySub WHERE ISNULL(IsInactive,0)=0   AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value.ToString().Replace(" ", "") + "%' AND ISNULL(Gender,'') like '%" + cbGender.Value.ToString().Replace(" ", "") + "%'";
                glProductSubCategory.DataBind();
            }
            if (!String.IsNullOrEmpty(cbProductCategory.Text))
            {
                ProductGroupLookup.SelectCommand = "SELECT ProductGroupCode,Description FROM Masterfile.ProductGroup WHERE ISNULL(IsInactive,0)=0   AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value.ToString().Replace(" ", "") + "%'";
                glProductGroup.DataBind();
            }
        }

     

    }
}