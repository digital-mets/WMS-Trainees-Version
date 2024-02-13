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
//using GearsAccounting;


using System.Drawing;
using System.IO;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

namespace GWL
{
    public partial class frmProductInfoSheet : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state

        private static string strError;

        Entity.ProductInfoSheet _Entity = new ProductInfoSheet();//Calls entity ICN
        Entity.ProductInfoSheet.PISStyleChart _EntityDetail = new ProductInfoSheet.PISStyleChart();//Call entity POdetails
        Entity.ProductInfoSheet.PISStepTemplate _EntityStepTemplate = new ProductInfoSheet.PISStepTemplate();
        Entity.ProductInfoSheet.PISStyleTemplate _EntityStyleTemplate = new ProductInfoSheet.PISStyleTemplate();
        Entity.ProductInfoSheet.PISThreadDetail _EntityThreadDetail = new ProductInfoSheet.PISThreadDetail();
        Entity.ProductInfoSheet.PISEmbroideryDetail _EntityEmbroideryDetail = new ProductInfoSheet.PISEmbroideryDetail();
        Entity.ProductInfoSheet.PISPrintDetail _EntityPrintDetail = new ProductInfoSheet.PISPrintDetail();
        Entity.ProductInfoSheet.PISOtherPictureDetail _EntityOtherPictureDetail = new ProductInfoSheet.PISOtherPictureDetail();
        Entity.ProductInfoSheet.PISGradeBracket _EntityDetail2 = new ProductInfoSheet.PISGradeBracket();
        #region page load/entry

        protected void Page_PreInit(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            foreach (Control c in  form2.Controls)
            {
                if (c is SqlDataSource)
                {
                    ((SqlDataSource)c).ConnectionString = Session["ConnString"].ToString();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["AddColumns"] == "1")
                AddColumns();
            if (Session["GetMeasurementChartTemplate"] == "1")
            {
                GetMeasurementChartTemplate();
                //DataTable getBaseSize = Gears.RetriveData2("SELECT LTRIM(RTRIM(StandardSize)) AS StandardSize FROM Masterfile.Fit where FitCode = '" + glFitCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                //foreach (DataRow dt in getBaseSize.Rows)
                //{
                //    txtBaseSize.Text = dt[0].ToString();
                //}
                //if (!String.IsNullOrEmpty(txtBaseSize.Text))
                //{
                //    SetColor();
                //}
            }
            if (Request.QueryString["entry"].ToString() != "N" & Session["AddColumns"] == null)
            {
                if (Session["ColDT"] != null)
                {
                    gvSizeDetail1.DataSourceID = null;
                    gvSizeDetail1.DataSource = Session["ColDT"];
                }
                //else{
                //    AddColumns();
                //}
            }


            string referer;
            try //Validation to restrict user to browse/type directly to browser's address
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

            gvComposition.KeyFieldName = "LineNumber";
            gvSizeDetail2.KeyFieldName = "LineNumber";
            gvStepDetail.KeyFieldName = "LineNumber";
            gvStyleDetail.KeyFieldName = "LineNumber";
            gvSizeDetail1.KeyFieldName = "LineNumberMeasurementChart";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.



            if (!IsPostBack)
            {

                Session["DatatableGB"] = "0";
                DeleteSession();

                for (int i = 0; i < 3; i++)
                {
                    cbYear.Items.Add(Convert.ToString(DateTime.Now.Year + i));
                }
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session

                //Experiment...
                //string x = DateTime.Now.Year.ToString();
                //x.Substring(x.Length-1, 1); -- > Output 6.
                // DocNumber (PISNumber) = Last 2 Digits of Current Year.


                if (Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                {

                    _Entity.getdata(Request.QueryString["parameters"].ToString().Split('|')[1], Session["ConnString"].ToString());//ADD CONN
                    //UpdatePISNumber();
                    //txtPISNumber.Value = DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2, 2) + "*************";

                }
                else
                {
                    if (Request.QueryString["entry"].ToString() == "N")
                    {
                        popup.ShowOnPageLoad = false;
                        //UpdatePISNumber();
                        //txtPISNumber.Value = DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2, 2) + "*************";
                    }
                    else
                    {
                        txtPISNumber.Value = Request.QueryString["docnumber"].ToString();
                    }


                    _Entity.getdata(txtPISNumber.Text, Session["ConnString"].ToString());//ADD CONN
                }


                // Moved inside !IsPostBack   LGE 03 - 02 - 2016
                // V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (string.IsNullOrEmpty(_Entity.LastEditedBy) || Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        else
                        {
                            updateBtn.Text = "Update";
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        edit = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        DuringView();
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }
                //--------------------------------------------------
                //--------------------------------------------------

              

                txtPISDescription.Text = _Entity.PISDescription;

                glCustomerCode.Text = _Entity.CustomerCode;
                DataTable getcustomercode = Gears.RetriveData2("SELECT Name FROM Masterfile.BPCustomerInfo where BizPartnerCode = '" + glCustomerCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in getcustomercode.Rows)
                {
                    txtCustomerName.Text = dt[0].ToString();
                }
                cbBrand.Value = _Entity.Brand;
                cbGender.Value = _Entity.Gender;
                cbProductCategory.Value = _Entity.ProductCategory;
                cbProductGroup.Value = _Entity.ProductGroup;
                cbFOBSupplier.Value = _Entity.FOBSupplier;
                ProductSubCategoryLookup.SelectCommand = "SELECT Mnemonics, Description FROM Masterfile.ProductCategorySub WHERE ISNULL(IsInactive,0)=0 AND ISNULL(ProductSubCatCode,'')!='' AND ISNULL(Mnemonics,'')!=''  AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value + "%' AND ISNULL(Gender,'') like '%" + cbGender.Value + "%'";
                cbProductSubCategory.Value = _Entity.ProductSubCategory;
                DesignCategoryLookup.SelectCommand = "SELECT CategoryCode, Description FROM Masterfile.DesignCategory WHERE ISNULL(IsInactive,0)=0 AND ISNULL(CategoryCode,'')!='' AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value + "%'";
                cbDesignCategory.Value = _Entity.DesignCategory;
                DesignSubCategoryLookup.SelectCommand = "SELECT SubCategoryCode, Description FROM Masterfile.DesignSubCategory WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SubCategoryCode,'')!='' AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value + "%' AND ISNULL(ProductGroup,'') like '%" + cbProductGroup.Value + "%'";
                cbDesignSubCategory.Value = _Entity.DesignSubCategory;
                cbProductClass.Value = _Entity.ProductClass;
                cbProductSubClass.Value = _Entity.ProductSubClass;
                cbYear.Value = _Entity.DeliveryYear;
                cbMonth.Value = _Entity.DeliveryMonth;
                glCollection.Value = _Entity.Theme;
                glDesigner.Value = _Entity.Designer;
                glDISNo.Value = _Entity.DISNo;
                txtInspiration.Value = _Entity.Inspiration;

                ProductGroupLookup.SelectCommand = "SELECT ProductGroupCode, Description FROM Masterfile.ProductGroup WHERE ISNULL(IsInactive,0)=0 AND ISNULL(ProductGroupCode,'')!='' AND REPLACE (ProductCategoryCode, ' ', '' ) like '%," + cbProductCategory.Value + ",%'"; 

                // *** DATASOURCE *** //
                SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";

                // --- FABRIC DATA --- //
                glFabricSupplier.Value = _Entity.FabricSupplier;
                FabricCodeLookup.SelectCommand = "SELECT FabricCode, B.FullDesc AS FabricDescription FROM Masterfile.Fabric A INNER JOIN Masterfile.Item B ON A.FabricCode = B.ItemCode WHERE FabricGroup = '" + cbProductGroup.Value + "' AND FabricSupplier = '" + glFabricSupplier.Text + "' ";
                glFabricCode.Value = _Entity.FabricCode;
                FabricColorLookup.SelectCommand = "SELECT DISTINCT ColorCode, ItemCode AS FabricCode FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0)=0 AND ItemCode = '" + glFabricCode.Text + "'";
                glFabricColor.Value = _Entity.FabricColor;
                // --- --- //
                txtFabricGroup.Text = _Entity.FabricGroup;
                txtFabricDesignCategory.Text = _Entity.FabricDesignCategory;
                txtDyeingMethod.Text = _Entity.Dyeing;
                txtWeaveType.Text = _Entity.WeaveType;
                txtCuttableWidth.Text = _Entity.CuttableWidth;
                txtGrossWidth.Text = _Entity.GrossWidth;
                txtForKnitsOnly.Text = _Entity.ForKnitsOnly;
                txtCuttableWeightBW.Text = _Entity.CuttableWeightBW;
                txtGrossWeightBW.Text = _Entity.GrossWeightBW;
                txtYield.Text = _Entity.Yield;
                txtFabricStretch.Text = _Entity.FabricStretch;
                txtWarpConstruction.Text = _Entity.WarpConstruction;
                txtWeftConstruction.Text = _Entity.WeftConstruction;
                txtWarpWeave.Text = _Entity.WarpDensity;
                txtWeftWeave.Text = _Entity.WeftDensity;
                txtShrinkageWarp.Text = _Entity.WarpShrinkage;
                txtShrinkageWeft.Text = _Entity.WeftShrinkage;
                SetCompositionDetail();
                // --- --- //
                // --- END OF FABRIC DATA --- //


                // --- FIT DATA --- //
                glReferenceBizPartner.Value = _Entity.ReferenceBizPartner;
                FitCodeLookup.SelectCommand = "SELECT FitCode, FitName FROM Masterfile.Fit WHERE ISNULL(IsInactive,0)=0 AND GenderCode = '" + cbGender.Value + "' AND ProductCategory = '" + cbProductCategory.Value + "' ";
                glFitCode.Value = _Entity.FitCode;
                txtBasePattern.Text = _Entity.BasePattern;
                spinActualShrinkageWarp.Value = _Entity.ActualWarpShrinkage.ToString();
                spinActualShrinkageWeft.Value = _Entity.ActualWeftShrinkage.ToString();
                spinCombineShrinkage.Value = _Entity.CombinedShrinkage.ToString();
                // --- --- //
                txtWaistNeckLine.Text = _Entity.Waist;
                txtFit.Text = _Entity.Fit;
                txtSilhouette.Text = _Entity.Silhouette;
                txtMasterPattern.Text = _Entity.MasterPattern;
                // --- --- //
                // --- END OF FIT DATA --- //



                // --- WASH DATA --- //


                glWashSupplier.DataSourceID = "SupplierCodeLookup";
                //glWashSupplier.DataBind();
                glWashSupplier.Text = _Entity.WashSupplier;
                DataTable getwashsupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glWashSupplier.Text + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in getwashsupplier.Rows)
                {
                    txtWashSupplierName.Text = dt[0].ToString();
                }
                glWashCode.Text = _Entity.WashCode;
                glTintColor.Text = _Entity.TintColor;
                memoWashDescription.Text = _Entity.WashDescription;
                // --- END OF WASH DATA --- //

                // --- EMBROIDER DATA --- //
                glEmbroiderySupplier.Value = _Entity.EmbroiderySupplier;
                DataTable getembroidersupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glEmbroiderySupplier.Value + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in getembroidersupplier.Rows)
                {
                    txtEmbroiderSupplierDescription.Text = dt[0].ToString();
                }
                // --- END OF EMBROIDER DATA --- //

                // --- PRINT DATA --- //
                glPrintSupplier.Value = _Entity.PrintSupplier;
                DataTable getprintsupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glPrintSupplier.Value + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in getprintsupplier.Rows)
                {
                    txtPrintSupplierDescription.Text = dt[0].ToString();
                }
                // --- END OF PRINT DATA --- //

                // --- MEASUREMENT CHART DATA --- //
                txtBaseSize.Text = _Entity.BaseSize;
                txtRemarks.Text = _Entity.Remarks;
                txtBaseSizeBOM.Text = _Entity.BaseSize;
                // --- END OF MEASUREMENT CHART DATA --- //

                // --- PRINT DATA --- //
                glStyleCode.Value = _Entity.StyleTemplateCode;
                glStepCode.Value = _Entity.StepTemplateCode;
                spinTotalItemCost.Value = _Entity.TotalItemCost.ToString();
                spinMarkUp.Value = _Entity.Markup.ToString();
                spinSellingPrice.Value = _Entity.SellingPrice.ToString();
                spinAdditionalOverhead.Value = _Entity.AdditionalOverhead.ToString();
                spinSRP.Value = _Entity.SRP.ToString();
                spinProfitFactor.Value = _Entity.ProfitFactor.ToString();
                // --- END OF PRINT DATA --- //


                // --- IMAGES --- ///
                txtFrontImage64string.Text = _Entity.FrontImage;
                txtBackImage64string.Text = _Entity.BackImage;
                txt2DFrontImage64string.Text = _Entity.FrontImage2D;
                txt2DBackImage64string.Text = _Entity.BackImage2D;
                // --- END OF IMAGES --- ///


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
                txtApprovedBy.Text = _Entity.ApprovedBy;
                txtApprovedDate.Text = _Entity.ApprovedDate;
                txtUnapprovedBy.Text = _Entity.UnapprovedBy;
                txtUnapprovedDate.Text = _Entity.UnapprovedDate;
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;
                txtSyncedBy.Text = _Entity.SyncedBy;
                txtSyncedDate.Text = _Entity.SyncedDate;
                //txtHSubmittedBy.Text = _Entity.SubmittedBy;
                //txtHSubmittedDate.Text = _Entity.SubmittedDate;
                //txtCancelledBy.Text = _Entity.CancelledBy;
                //txtCancelledDate.Text = _Entity.CancelledDate;
                //txtPostedBy.Text = _Entity.PostedBy;
                //txtPostedDate.Text = _Entity.PostedDate;



                SetSizeDetail2();
                // --- SizeDetail DATA --- //
                AddColumns();
                if (!String.IsNullOrEmpty(txtBaseSize.Text))
                {
                    SetColor();
                }
                if (Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                {

                    GetSelectedValGB(); 
                    Session["DatatableGB"] = "1";

                    //DataTable checkSizeDetail1 = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStyleChart WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                    //gvSizeDetail1.DataSourceID = (checkSizeDetail1.Rows.Count > 0 ? "odsDetail" : "sdsSizeDetail");
                    //DataTable checkSizeDetail1 = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStyleChart WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());
                    //gvSizeDetail1.DataSourceID = (checkSizeDetail1.Rows.Count > 0 ? "odsDetail" : "sdsSizeDetail");
                    //gvSizeDetail1.DataBind();
                    DataTable checkSizeDetail1 = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStyleChart WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());
                    if (checkSizeDetail1.Rows.Count > 0)
                    {
                        sdsSizeDetail.SelectCommand = "DECLARE @Sizes VARCHAR(MAX)"
                                    + " DECLARE @query VARCHAR(MAX)"
                                    + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1) FROM ("
                                    + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),RTRIM(LTRIM(SizeCode))) + '],'  AS VARCHAR(MAX))"
                                    + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'"
                                    + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1"
                                    + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + Request.QueryString["parameters"].ToString().Split('|')[1] + "' ) AS Col"
                                    + " SELECT @query = 'SELECT RIGHT(''00000''+ CAST(ROW_NUMBER() OVER (ORDER BY [Order]) AS VARCHAR(5)),5) AS LineNumberMeasurementChart,* FROM ( SELECT PISNumber, ISNULL(A.IsMajor,0) AS IsMajor, A.POMCode AS [Code],B.Description AS PointofMeasurement,B.Instruction,Tolerance,Bracket, Grade, Sorting AS [Order],Value, RTRIM(LTRIM(SizeCode)) AS SizeCode"
                                    + " FROM Masterfile.PISStyleChart A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE PISNumber =''" + Request.QueryString["parameters"].ToString().Split('|')[1] + "''"
                                    + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query) ";
                        gvSizeDetail1.DataSource = sdsSizeDetail;
                        gvSizeDetail1.DataBind();
                    }
                    else
                        gvSizeDetail1.DataSourceID = "sdsSizeDetail";
                    // --- END OF SizeDetail DATA --- //



                    // --- THREAD DETAIL DATA --- //
                    DataTable checkThreadDetail = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISThreadDetail WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());
                    gvThreadDetail.DataSourceID = (checkThreadDetail.Rows.Count > 0 ? "odsThreadDetail" : "ThreadDetailDS");
                    // --- END OF THREAD DETAIL DATA --- //

                    // --- EMBROIDER DATA --- //
                    DataTable checkEmbroidery = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISEmbroideryDetail WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());
                    //gvEmbroiderDetail.DataSourceID = "EmbroiderDetailDS";
                    gvEmbroiderDetail.DataSourceID = (checkEmbroidery.Rows.Count > 0 ? "odsEmbroideryDetail" : "EmbroiderDetailDS");
                    // --- END OF EMBROIDER DATA --- //

                    // --- PRINT DATA --- //
                    DataTable checkPrint = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISPrintDetail WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());
                    gvPrintDetail.DataSourceID = (checkPrint.Rows.Count > 0 ? "odsPrintDetail" : "PrintDetailDS");
                    // --- END OF PRINT DATA --- //

                    // --- STEP DATA --- //
                    DataTable checkStep = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStepTemplate WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());//ADD CONN
                    gvStepDetail.DataSourceID = (checkStep.Rows.Count > 0 ? "odsStepDetail" : "StepDetailDS");
                    // --- END OF STEP DATA --- //

                    // --- STYLE DATA --- //
                    DataTable checkStyle = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStyleTemplate WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());//ADD CONN
                    gvStyleDetail.DataSourceID = (checkStyle.Rows.Count > 0 ? "odsStyleDetail" : "StyleDetailDS");
                    // --- END OF STYLE DATA --- //

                    // -- OTHER PICTURES DETAIL -- //
                    DataTable checkImage = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISImage WHERE PISNumber = '" + Request.QueryString["parameters"].ToString().Split('|')[1] + "'", Session["ConnString"].ToString());//ADD CONN
                    gvOtherPictures.DataSourceID = (checkStyle.Rows.Count > 0 ? "odsOtherPictureDetail" : "OtherPictureDetailDS");
                    // -- END OF OTHER PICTURES DETAIL -- //
                    txtHAddedBy.Text = "";
                    txtHAddedDate.Text = "";
                    txtHLastEditedBy.Text = "";
                    txtHLastEditedDate.Text = "";
                    txtApprovedBy.Text = "";
                    txtApprovedDate.Text = "";
                    Session["ThreadFit"] = "1";
                    Session["Embroider"] = "1";
                    Session["PrintDetail"] = "1";
                }
                else
                {
                    DataTable checkCount2 = Gears.RetriveData2("Select DocNumber from Production.PISGradeBracket  where DocNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                    gvGradeBracket.DataSourceID = (checkCount2.Rows.Count > 0 ? "odsDetail3" : "sdsDetail3");

                    DataTable checkSizeDetail1 = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStyleChart WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                    //gvSizeDetail1.DataSourceID = (checkSizeDetail1.Rows.Count > 0 ? "odsDetail" : "sdsSizeDetail");
                    if (checkSizeDetail1.Rows.Count > 0)
                    {
                        //sdsSizeDetail.SelectCommand = "DECLARE @Sizes VARCHAR(MAX)"
                        //            + " DECLARE @query VARCHAR(MAX)"
                        //            + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1) FROM ("
                        //            + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),RTRIM(LTRIM(SizeCode))) + '],'  AS VARCHAR(MAX))"
                        //            + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + txtPISNumber.Text + "'"
                        //            + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1"
                        //            + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + txtPISNumber.Text + "' ) AS Col"
                        //            + " SELECT @query = 'SELECT * FROM ( SELECT PISNumber, ISNULL(A.IsMajor,0) AS IsMajor, A.POMCode AS [Code],B.Description AS PointofMeasurement, Tolerance,Bracket, Grade, Sorting AS [Order],Value, RTRIM(LTRIM(SizeCode)) AS SizeCode"
                        //            + " FROM Masterfile.PISStyleChart A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE PISNumber =''" + txtPISNumber.Text + "''"
                        //            + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query) ";
                        //gvSizeDetail1.DataSource = sdsSizeDetail;
                        //gvSizeDetail1.DataBind();
                        GetMeasurementChart();
                    }
                    else
                    {

                        //gvSizeDetail1.DataSourceID = "sdsSizeDetail";
                        gvSizeDetail1.DataSource = null;
                        gvSizeDetail1.DataSourceID = "sdsSizeDetail";
                    }



                    //// --- END OF SizeDetail DATA --- //

                    // --- THREAD DETAIL DATA --- //
                    DataTable checkThreadDetail = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISThreadDetail WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                    gvThreadDetail.DataSourceID = (checkThreadDetail.Rows.Count > 0 ? "odsThreadDetail" : "ThreadDetailDS");
                    // --- END OF THREAD DETAIL DATA --- //

                    // --- EMBROIDER DATA --- //
                    DataTable checkEmbroidery = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISEmbroideryDetail WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                    //gvEmbroiderDetail.DataSourceID = "EmbroiderDetailDS";
                    gvEmbroiderDetail.DataSourceID = (checkEmbroidery.Rows.Count > 0 ? "odsEmbroideryDetail" : "EmbroiderDetailDS");
                    // --- END OF EMBROIDER DATA --- //

                    // --- PRINT DATA --- //
                    DataTable checkPrint = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISPrintDetail WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                    gvPrintDetail.DataSourceID = (checkPrint.Rows.Count > 0 ? "odsPrintDetail" : "PrintDetailDS");
                    // --- END OF PRINT DATA --- //

                    // --- STEP DATA --- //
                    DataTable checkStep = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStepTemplate WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gvStepDetail.DataSourceID = (checkStep.Rows.Count > 0 ? "odsStepDetail" : "StepDetailDS");
                    // --- END OF STEP DATA --- //

                    // --- STYLE DATA --- //
                    DataTable checkStyle = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISStyleTemplate WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gvStyleDetail.DataSourceID = (checkStyle.Rows.Count > 0 ? "odsStyleDetail" : "StyleDetailDS");
                    // --- END OF STYLE DATA --- //

                    // -- OTHER PICTURES DETAIL -- //
                    DataTable checkImage = Gears.RetriveData2("Select PISNumber FROM Masterfile.PISImage WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gvOtherPictures.DataSourceID = (checkImage.Rows.Count > 0 ? "odsOtherPictureDetail" : "OtherPictureDetailDS");
                    // -- END OF OTHER PICTURES DETAIL -- //



                }

                //SupplierCodeLookup.SelectCommand = null;

            }

            Freeze();
            ViewPISImages();
            SetLookupFilter();
        }
        #endregion



        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.PISNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDPIS";

            string strresult = GearsProduction.GProduction.ProductInfoSheet_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Post
        private void Post()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._Connection = Session["ConnString"].ToString();
            //gparam._DocNo = _Entity.PISNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "ACTADI";
            //gparam._Table = "Accounting.AssetDisposal";
            //gparam._Factor = -1;
            //string strresult = GearsAccounting.GAccounting.AssetDisposal_Post(gparam);
            //if (strresult != " ")
            //{
            //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            //}
        }
        #endregion

        #region Set controls' state/behavior/etc...

        protected void DuringView()
        {
            txtPISDescription.ReadOnly = true;
            glCustomerCode.ReadOnly = true;
            glCustomerCode.DropDownButton.Enabled = false;
            cbBrand.ReadOnly = true;
            cbBrand.DropDownButton.Enabled = false;
            cbDesignCategory.ReadOnly = true;
            cbDesignCategory.DropDownButton.Enabled = false;
            cbDesignSubCategory.ReadOnly = true;
            cbDesignSubCategory.DropDownButton.Enabled = false;
            cbProductClass.ReadOnly = true;
            cbProductClass.DropDownButton.Enabled = false;
            cbProductSubClass.ReadOnly = true;
            cbProductSubClass.DropDownButton.Enabled = false;
            txtInspiration.ReadOnly = true;
            cbYear.ReadOnly = true;
            cbYear.DropDownButton.Enabled = false;
            cbMonth.ReadOnly = true;
            cbMonth.DropDownButton.Enabled = false;
            glCollection.ReadOnly = true;
            glCollection.DropDownButton.Enabled = false;
            glDesigner.ReadOnly = true;
            glDesigner.DropDownButton.Enabled = false;
            glDISNo.ReadOnly = true;
            glDISNo.DropDownButton.Enabled = false;
            
            glFabricSupplier.ReadOnly = true;
            glFabricSupplier.DropDownButton.Enabled = false;
            glFabricCode.ReadOnly = true;
            glFabricCode.DropDownButton.Enabled = false;
            glFabricColor.ReadOnly = true;
            glFabricColor.DropDownButton.Enabled = false;

            glReferenceBizPartner.ReadOnly = true;
            glReferenceBizPartner.DropDownButton.Enabled = false;
            glFitCode.ReadOnly = true;
            glFitCode.DropDownButton.Enabled = false;
            txtBasePattern.ReadOnly = true;


            glWashSupplier.ReadOnly = true;
            glWashSupplier.DropDownButton.Enabled = false;
            glWashCode.ReadOnly = true;
            glWashCode.DropDownButton.Enabled = false;
            memoWashDescription.ReadOnly = true;
            glTintColor.ReadOnly = true;
            glTintColor.DropDownButton.Enabled = false;

            glEmbroiderySupplier.ReadOnly = true;
            glEmbroiderySupplier.DropDownButton.Enabled = false;

            glPrintSupplier.ReadOnly = true;
            glPrintSupplier.DropDownButton.Enabled = false;

            txtRemarks.ReadOnly = true;

            txtBaseSize.ReadOnly = true;
            btnSet.Enabled = false;
            btnRefreshGrid.Enabled = false;

            glStyleCode.ReadOnly = true;
            glStyleCode.DropDownButton.Enabled = false;
            btnGetStyleDetail.Enabled = false;

            glStepCode.ReadOnly = true;
            glStepCode.DropDownButton.Enabled = false;
            btnGetStepDetail.Enabled = false;

            txtHField1.ReadOnly = true;
            txtHField2.ReadOnly = true;
            txtHField3.ReadOnly = true;
            txtHField4.ReadOnly = true;
            txtHField5.ReadOnly = true;
            txtHField6.ReadOnly = true;
            txtHField7.ReadOnly = true;
            txtHField8.ReadOnly = true;
            txtHField9.ReadOnly = true;
        }
        protected void Freeze()
        {
            int consfreeze = 0;
            foreach (GridViewColumn column in gvSizeDetail1.VisibleColumns)
            {
                if (column is GridViewColumn)
                {
                    GridViewColumn dataColumn = (GridViewColumn)column;
                    if (dataColumn.Visible)
                        dataColumn.FixedStyle = GridViewColumnFixedStyle.Left;
                    consfreeze++;
                }
                if (consfreeze == 7)
                    break;
            }
        }

        private DataTable GetMeasurementChart()
        {
            gvSizeDetail1.DataSource = null;
            gvSizeDetail1.DataSourceID = null;

            DataTable dt = new DataTable();
            sdsSizeDetail.SelectCommand = "DECLARE @Sizes VARCHAR(MAX)"
                                    + " DECLARE @query VARCHAR(MAX)"
                                    + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1) FROM ("
                                    + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),RTRIM(LTRIM(SizeCode))) + '],'  AS VARCHAR(MAX))"
                                    + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + txtPISNumber.Text + "'"
                                    + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1"
                                    + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + txtPISNumber.Text + "' ) AS Col"
                                    + " SELECT @query = 'SELECT RIGHT(''00000''+ CAST(ROW_NUMBER() OVER (ORDER BY [Order]) AS VARCHAR(5)),5) AS LineNumberMeasurementChart,* FROM ( SELECT PISNumber, ISNULL(A.IsMajor,0) AS IsMajor, A.POMCode AS [Code],B.Description AS PointofMeasurement,B.Instruction,Tolerance,Bracket, Grade, Sorting AS [Order],Value, RTRIM(LTRIM(SizeCode)) AS SizeCode"
                                    + " FROM Masterfile.PISStyleChart A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE PISNumber =''" + txtPISNumber.Text + "''"
                                    + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query) ";
            gvSizeDetail1.DataSource = sdsSizeDetail;
            gvSizeDetail1.DataBind();

            Session["Datatable"] = "1";
            Session["GetMeasurementChartTemplate"] = null;

            foreach (GridViewColumn col in gvSizeDetail1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvSizeDetail1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvSizeDetail1.GetRowValues(i, col.ColumnName);
            }


            Session["ColDT"] = dt;
            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumberMeasurementChart"]};
            return dt;
        }

        private DataTable GetMeasurementChartTemplate()
        {
            gvSizeDetail1.DataSource = null;
            gvSizeDetail1.DataSourceID = null;

            DataTable dt = new DataTable();
            sdsSizeDetail.SelectCommand = "DECLARE @Sizes VARCHAR(MAX)"
                                    + " DECLARE @query VARCHAR(MAX)"
                                    + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1) FROM ("
                                    + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),RTRIM(LTRIM(SizeCode))) + '],'  AS VARCHAR(MAX))"
                                    + " FROM Masterfile.MeasurementChartTemplate WHERE FitCode ='" + glFitCode.Text + "'"
                                    + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1"
                                    + " FROM Masterfile.MeasurementChartTemplate WHERE FitCode ='" + glFitCode.Text + "' ) AS Col"
                                    + " SELECT @query = 'SELECT RIGHT(''00000''+ CAST(ROW_NUMBER() OVER (ORDER BY [Order]) AS VARCHAR(5)),5) AS LineNumberMeasurementChart,* FROM ( SELECT FitCode, ISNULL(A.IsMajor,0) AS IsMajor, A.POMCode AS [Code],B.Description AS PointofMeasurement,B.Instruction,Tolerance,Bracket, Grade, Sorting AS [Order],Value, RTRIM(LTRIM(SizeCode)) AS SizeCode"
                                    + " FROM Masterfile.MeasurementChartTemplate A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE FitCode =''" + glFitCode.Text + "''"
                                    + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query) ";
            gvSizeDetail1.DataSource = sdsSizeDetail;
            
            if (Request.QueryString["parameters"].ToString().Split('|')[0] != "CopyTrans")
                gvSizeDetail1.DataBind();

            Session["GetMeasurementChartTemplate"] = "1";
            Session["Datatable"] = null;

            foreach (GridViewColumn col in gvSizeDetail1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                int alreadythere = 0;
                foreach (DataColumn cols in dt.Columns)
                {
                    if (dataColumn.FieldName == cols.ToString())
                        alreadythere++;
                }
                if (alreadythere == 0)
                    dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvSizeDetail1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvSizeDetail1.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumberMeasurementChart"]};
            return dt;
        }
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;

            if (Request.QueryString["entry"].ToString() == "E")
            {
                if (text.ID == "txtBasePattern")
                {
                    text.ReadOnly = true;
                }
            }
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {

            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
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
        protected void btn_init(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            btn.ClientEnabled = !view;
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
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.DropDownButton.Enabled = !view;
            combobox.Enabled = !view;

            if (Request.QueryString["entry"].ToString() == "E")
            {
                if (combobox.ID == "cbGender" || combobox.ID == "cbProductCategory"
                    || combobox.ID == "cbProductGroup" || combobox.ID == "cbFOBSupplier" || combobox.ID == "cbProductSubCategory")
                {
                    combobox.DropDownButton.Enabled = false;
                    combobox.ClientEnabled = false;
                }
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.HorizontalAlign = HorizontalAlign.Right;
            spinedit.MinValue = 0;
            spinedit.MaxValue = 2147483647;
            spinedit.Increment = 0;

            if (Request.QueryString["entry"].ToString() == "E")
            {
                if (spinedit.ID == "spinActualShrinkageWarp"
                    || spinedit.ID == "spinActualShrinkageWeft"
                    || spinedit.ID == "spinTotalItemCost"
                    || spinedit.ID == "spinSellingPrice")
                {
                    spinedit.ReadOnly = true;
                }
            }
            else if (Request.QueryString["entry"].ToString() == "N")
            {
                if (spinedit.ID == "spinTotalItemCost"
                    || spinedit.ID == "spinSellingPrice")
                {
                    spinedit.ReadOnly = true;
                }
                else
                {
                    spinedit.ReadOnly = false;
                }
            }
            else
            {
                spinedit.ReadOnly = view;
            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            ASPxGridView grid = sender as ASPxGridView;
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
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                //if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
                //{
                //    if (!String.IsNullOrEmpty(cbDisposalType.Text))
                //    {
                //        if(cbDisposalType.Text == "Sales")
                //        {
                //            if (!String.IsNullOrEmpty(glSoldTo.Text))
                //            {
                //                if (e.ButtonType == ColumnCommandButtonType.New)
                //                {
                //                    e.Visible = true;
                //                }
                //            }
                //            else
                //            {
                //                if (e.ButtonType == ColumnCommandButtonType.New)
                //                {
                //                    e.Visible = false;
                //                }
                //            }
                //        }

                //        else if (cbDisposalType.Text == "Retirement")
                //        {
                //            if (e.ButtonType == ColumnCommandButtonType.New)
                //            {
                //                e.Visible = true;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (e.ButtonType == ColumnCommandButtonType.New)
                //        {
                //            e.Visible = false;
                //        }
                //    }


                //if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
                //{
                //    if (a.ID == "gvSizeDetail1")
                //    {
                //        if (string.IsNullOrEmpty(txtBaseSize.Text))
                //        {
                //            if (e.ButtonType == ColumnCommandButtonType.New)
                //                e.Visible = false;
                //        }
                //        else
                //        {
                //            if (e.ButtonType == ColumnCommandButtonType.New)
                //                e.Visible = true;
                //        }
                //    }
                //    else
                //    {
                //        if (e.ButtonType == ColumnCommandButtonType.New)
                //            e.Visible = true;
                //    }

                //}
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                        e.ButtonType == ColumnCommandButtonType.Update)
                        e.Visible = false;
                }
                if (e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
                if (e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
        }
        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "N")
                {
                    if (e.ButtonID == "Details")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }

                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    if (e.ButtonID == "ThreadDelete" || e.ButtonID == "EmbroiderDelete" || e.ButtonID == "PrintDelete"
                        || e.ButtonID == "MeasurementChartDelete"
                        || e.ButtonID == "StyleDelete"
                        || e.ButtonID == "StepsDelete"
                        || e.ButtonID == "OtherPictureDelete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
        }
        #endregion

        #region Lookup Settings
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string propertynumber = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var propertynumberlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("PropertyNumber"))
            {
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,ItemCode,Qty,UnitCost,AccumulatedDepreciation,Status from Accounting.AssetInv where propertynumber = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["ItemCode"].ToString() + ";";
                    codes += dt["Qty"].ToString() + ";";
                    codes += dt["UnitCost"].ToString() + ";";
                    codes += dt["AccumulatedDepreciation"].ToString() + ";";
                    codes += dt["Status"].ToString() + ";";

                }

                propertynumberlookup.JSProperties["cp_identifier"] = "ItemCode";
                propertynumberlookup.JSProperties["cp_codes"] = codes;
            }

            else if (e.Parameters.Contains("POMCode"))
            {
                DataTable getDetail = Gears.RetriveData2("SELECT Description,Instruction FROM Masterfile.POM WHERE POMCode = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";
                    codes += dt["Instruction"].ToString() + ";";

                }

                propertynumberlookup.JSProperties["cp_identifier"] = "POMCode";
                propertynumberlookup.JSProperties["cp_codes"] = codes;
            }

            else if (e.Parameters.Contains("ItemCategoryCodeStyle"))
            {
                DataTable getDetail = Gears.RetriveData2("SELECT Description FROM Masterfile.Itemcategory WHERE ItemCategoryCode = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";

                }

                propertynumberlookup.JSProperties["cp_identifier"] = "ItemCategoryCodeStyle";
                propertynumberlookup.JSProperties["cp_codes"] = codes;
            }


            else if (e.Parameters.Contains("ProductCategoryCodeStyle"))
            {
                DataTable getDetail = Gears.RetriveData2("SELECT Description FROM Masterfile.ProductCategory WHERE ProductCategoryCode = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";

                }

                propertynumberlookup.JSProperties["cp_identifier"] = "ProductCategoryCodeStyle";
                propertynumberlookup.JSProperties["cp_codes"] = codes;
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN

            _Entity.PISNumber = txtPISNumber.Text;
            _Entity.PISDescription = txtPISDescription.Text;

            _Entity.CustomerCode = glCustomerCode.Text;
            if (cbBrand.Value != null)
                _Entity.Brand = cbBrand.Value.ToString();
            if (cbGender.Value != null)
                _Entity.Gender = cbGender.Value.ToString();
            if (cbProductCategory.Value != null)
                _Entity.ProductCategory = cbProductCategory.Value.ToString();
            if (cbProductGroup.Value != null)
                _Entity.ProductGroup = cbProductGroup.Value.ToString();
            if (cbFOBSupplier.Value != null)
                _Entity.FOBSupplier = cbFOBSupplier.Value.ToString();
            if (cbProductSubCategory.Value != null)
                _Entity.ProductSubCategory = cbProductSubCategory.Value.ToString();
            if (cbDesignCategory.Value != null)
                _Entity.DesignCategory = cbDesignCategory.Value.ToString();
            if (cbDesignSubCategory.Value != null)
                _Entity.DesignSubCategory = cbDesignSubCategory.Value.ToString();
            if (cbProductClass.Value != null)
                _Entity.ProductClass = cbProductClass.Value.ToString();
            if (cbProductSubClass.Value != null)
                _Entity.ProductSubClass = cbProductSubClass.Value.ToString();
            _Entity.Inspiration = txtInspiration.Text;

            if (cbYear.Value != null)
                _Entity.DeliveryYear = cbYear.Value.ToString();
            if (cbMonth.Value != null)
                _Entity.DeliveryMonth = cbMonth.Value.ToString();
            _Entity.Theme = glCollection.Text;
            _Entity.Designer = glDesigner.Text;
            _Entity.DISNo = glDISNo.Text;

            //_Entity.FrontImage = (byte[])Session["FrontImage"];
            _Entity.FrontImage = txtFrontImage64string.Text;
            //_Entity.BackImage = (byte[])Session["BackImage"];
            _Entity.BackImage = txtBackImage64string.Text;
            //_Entity.FrontImage2D = (byte[])Session["FrontImage2D"];
            _Entity.FrontImage2D = txt2DFrontImage64string.Text;
            //_Entity.BackImage2D = (byte[])Session["BackImage2D"];
            _Entity.BackImage2D = txt2DBackImage64string.Text;


            // --- FABRIC DATA --- //
            _Entity.FabricCode = glFabricCode.Text;
            _Entity.FabricSupplier = glFabricSupplier.Text;
            _Entity.FabricColor = glFabricColor.Text;
            // --- END OF FABRIC DATA --- //

            // --- FIT DATA --- //
            _Entity.ReferenceBizPartner = glReferenceBizPartner.Text;
            _Entity.FitCode = glFitCode.Text;
            _Entity.BasePattern = txtBasePattern.Text;
            _Entity.ActualWarpShrinkage = Convert.ToDecimal(spinActualShrinkageWarp.Value);
            _Entity.ActualWeftShrinkage = Convert.ToDecimal(spinActualShrinkageWeft.Value);
            _Entity.CombinedShrinkage = Convert.ToDecimal(spinCombineShrinkage.Value);
            // --- END OF FIT DATA --- //


            // --- WASH DATA --- //
            _Entity.WashSupplier = glWashSupplier.Text;
            _Entity.WashCode = glWashCode.Text;
            _Entity.TintColor = glTintColor.Text;
            _Entity.WashDescription = memoWashDescription.Text;
            // --- END OF WASH DATA --- //

            // --- EMBROIDER DATA --- //
            _Entity.EmbroiderySupplier = glEmbroiderySupplier.Text;
            // --- END OF EMBROIDER DATA --- //

            // --- PRINT DATA --- //
            _Entity.PrintSupplier = glPrintSupplier.Text;
            // --- END OF PRINT DATA --- //

            // --- MEASUREMENT CHART DATA --- //
            _Entity.BaseSize = txtBaseSize.Text;
            _Entity.Remarks = txtRemarks.Text;
            // --- END OF MEASUREMENT CHART DATA --- //


            // --- BOM&COSTING DATA --- //
            _Entity.StyleTemplateCode = glStyleCode.Text;
            _Entity.StepTemplateCode = glStepCode.Text;
            _Entity.TotalItemCost = Convert.ToDecimal(spinTotalItemCost.Value);
            _Entity.Markup = Convert.ToDecimal(spinMarkUp.Value);
            _Entity.SellingPrice = Convert.ToDecimal(spinSellingPrice.Value);
            _Entity.AdditionalOverhead = Convert.ToDecimal(spinAdditionalOverhead.Value);
            _Entity.SRP = Convert.ToDecimal(spinSRP.Value);
            _Entity.ProfitFactor = Convert.ToDecimal(spinProfitFactor.Value);
            // --- END OF BOM&COSTING DATA --- //


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
            //string param = e.Parameter.Split('|')[0];
            switch (e.Parameter)
            {
                case "Add":

                    if (error == false)
                    {
                        check = true;


                        _Entity.AddedBy = Session["userid"].ToString();
                        Gears.RetriveData2("DELETE FROM Production.ProductInfoSheet WHERE  PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());

                        _Entity.InsertData(_Entity);//Method of inserting for header

                        gvStepDetail.UpdateEdit();
                        if (Session["GetMeasurementChartTemplate"] == "1" || Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {
                            gvSizeDetail1.DataSource = GetMeasurementChartTemplate();
                            gvSizeDetail1.DataBind();
                            gvSizeDetail1.UpdateEdit();
                        }
                        else
                        {
                            gvSizeDetail1.UpdateEdit();
                        }
                        // -- BOM STYLE DETAIL -- //
                        if (Session["StyleDetail"] == "1" || Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {
                            gvStyleDetail.DataSource = GetStyleDetail();
                            gvStyleDetail.UpdateEdit();
                        }
                        else
                        {

                            gvStyleDetail.DataSourceID = "odsStyleDetail";
                            odsStyleDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvStyleDetail.UpdateEdit();

                        }
                        // -- END OF BOM STYLE DETAIL -- //

                        // -- BOM STEPS DETAIL -- //
                        if (Session["StepDetail"] == "1" || Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {
                            gvStepDetail.DataSource = GetStepDetail();
                            gvStepDetail.UpdateEdit();
                        }
                        else
                        {

                            gvStepDetail.DataSourceID = "odsStepDetail";
                            odsStepDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvStepDetail.UpdateEdit();

                        }
                        // -- END OF BOM STEPS DETAIL -- //


                        // -- THREAD DETAIL -- //
                        if (Session["ThreadFit"] == "1" || Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {

                            gvThreadDetail.DataSource = GETFit();
                            gvThreadDetail.UpdateEdit();
                        }
                        else
                        {
                            gvThreadDetail.DataSourceID = "odsThreadDetail";
                            odsThreadDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvThreadDetail.UpdateEdit();

                        }
                        // -- END OF THREAD DETAIL -- //


                        // -- EMBROIDER DETAIL -- //
                        if (Session["Embroider"] == "1" || Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {

                            gvEmbroiderDetail.DataSource = GETEmbrioder();
                            gvEmbroiderDetail.UpdateEdit();
                        }
                        else
                        {
                            gvEmbroiderDetail.DataSourceID = "odsEmbroideryDetail";
                            odsEmbroideryDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvEmbroiderDetail.UpdateEdit();//2nd initiation to insert grid  

                        }
                     
                        // -- END OF EMBROIDER DETAIL -- //


                        // -- PRINT DETAIL -- //
                        if (Session["PrintDetail"] == "1" || Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {

                            gvPrintDetail.DataSource = GETPrint();
                            gvPrintDetail.UpdateEdit();
                        }
                        else
                        {
                            gvPrintDetail.DataSourceID = "odsPrintDetail";
                            odsPrintDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvPrintDetail.UpdateEdit();//2nd initiation to insert grid 

                        }
            
                        // -- END OF PRINT DETAIL -- //

                        if (Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                        {
                            gvOtherPictures.DataSource = GETOtherPictures();
                            gvOtherPictures.UpdateEdit();
                        }
                        else
                        {
                            // -- OTHER PICTURES DETAIL -- //
                            gvOtherPictures.DataSourceID = "odsOtherPictureDetail";
                            odsPrintDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvOtherPictures.UpdateEdit();//2nd initiation to insert grid  
                            // -- END OF OTHER PICTURES DETAIL -- //
                        }

                        // -- PRINT DETAIL -- //
                        //gvPrintDetail.DataSourceID = "odsEmbroideryDetail";
                        //odsPrintDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                        //gvPrintDetail.UpdateEdit();//2nd initiation to insert grid  
                        // -- END OF PRINT DETAIL -- //


                        //gvSizeDetail1.DataSourceID = "odsDetail";
                        //odsDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;
                        //gvSizeDetail1.UpdateEdit();//2nd Initiation to update grid
                        //Post();



                        gvGradeBracket.DataSourceID = "odsDetail3";
                        odsDetail3.SelectParameters["docnumber"].DefaultValue = txtPISNumber.Text;
                        gvGradeBracket.UpdateEdit();

                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                        DeleteSession();
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;
                case "Update":


                    gvStyleDetail.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header

                        // -- MEASUREMENT CHART DETAIL -- //
                        if (Session["Datatable"] == "1")
                        {
                            //sdsSizeDetail.SelectCommand = "DECLARE @Sizes VARCHAR(MAX)"
                            //        + " DECLARE @query VARCHAR(MAX)"
                            //        + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1) FROM ("
                            //        + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),RTRIM(LTRIM(SizeCode))) + '],'  AS VARCHAR(MAX))"
                            //        + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + txtPISNumber.Text + "'"
                            //        + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1"
                            //        + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + txtPISNumber.Text + "' ) AS Col"
                            //        + " SELECT @query = 'SELECT RIGHT(''00000''+ CAST(ROW_NUMBER() OVER (ORDER BY PISNumber) AS VARCHAR(5)),5) AS LineNumberMeasurementChart,* FROM ( SELECT PISNumber, ISNULL(A.IsMajor,0) AS IsMajor, A.POMCode AS [Code],B.Description AS PointofMeasurement,B.Instruction,Tolerance,Bracket, Grade, Sorting AS [Order],Value, RTRIM(LTRIM(SizeCode)) AS SizeCode"
                            //        + " FROM Masterfile.PISStyleChart A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE PISNumber =''" + txtPISNumber.Text + "''"
                            //        + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query) ";
                            //gvSizeDetail1.DataSource = sdsSizeDetail;
                            gvSizeDetail1.DataSource = GetMeasurementChart();
                            gvSizeDetail1.DataBind();
                            gvSizeDetail1.UpdateEdit();
                        }
                        else if (Session["GetMeasurementChartTemplate"] == "1")
                        {
                            gvSizeDetail1.DataSource = GetMeasurementChartTemplate();
                            gvSizeDetail1.DataBind();
                            gvSizeDetail1.UpdateEdit();
                        }
                        else
                        {
                            gvSizeDetail1.UpdateEdit();
                        }
                        // -- END MEASUREMENT CHART DETAIL -- //


                        // -- BOM STYLE DETAIL -- //
                        if (Session["StyleDetail"] == "1")
                        {
                            gvStyleDetail.DataSource = GetStyleDetail();
                            gvStyleDetail.UpdateEdit();
                        }
                        else
                        {
                            gvStyleDetail.DataSourceID = "odsStyleDetail";
                            odsStyleDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvStyleDetail.UpdateEdit();
                        }
                        // -- END OF BOM STYLE DETAIL -- //

                        // -- BOM STEPS DETAIL -- //
                        if (Session["StepDetail"] == "1")
                        {
                            gvStepDetail.DataSource = GetStepDetail();
                            gvStepDetail.UpdateEdit();
                        }
                        else
                        {

                            gvStepDetail.DataSourceID = "odsStepDetail";
                            odsStepDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvStepDetail.UpdateEdit();

                        }
                        // -- END OF BOM STEPS DETAIL -- //


                        // -- THREAD DETAIL -- //

                 
                            gvThreadDetail.DataSourceID = "odsThreadDetail";
                            odsThreadDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                            gvThreadDetail.UpdateEdit();

                      
                      
                        // -- END OF THREAD DETAIL -- //


                        // -- EMBROIDER DETAIL -- //
                        gvEmbroiderDetail.DataSourceID = "odsEmbroideryDetail";
                        odsEmbroideryDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                        gvEmbroiderDetail.UpdateEdit();//2nd initiation to insert grid  
                        // -- END OF EMBROIDER DETAIL -- //


                        // -- PRINT DETAIL -- //
                        gvPrintDetail.DataSourceID = "odsPrintDetail";
                        odsPrintDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                        gvPrintDetail.UpdateEdit();//2nd initiation to insert grid  
                        // -- END OF PRINT DETAIL -- //


                        // -- OTHER PICTURES DETAIL -- //
                        gvOtherPictures.DataSourceID = "odsOtherPictureDetail";
                        odsPrintDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text;//Set select parameter to prevent error
                        gvOtherPictures.UpdateEdit();//2nd initiation to insert grid  
                        // -- END OF OTHER PICTURES DETAIL -- //
                        //gv1.DataSourceID = "odsDetail";
                        //odsDetail.SelectParameters["DocNumber"].DefaultValue = txtPISNumber.Text; 
                        //gv1.UpdateEdit();//2nd Initiation to update grid
                        //Post();



                        gvGradeBracket.DataSourceID = "odsDetail3";
                        odsDetail3.SelectParameters["docnumber"].DefaultValue = txtPISNumber.Text;
                        gvGradeBracket.UpdateEdit();

                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                        DeleteSession();
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

                case "productgroupfiltercase":
                    glFabricCode.Text = "";         //Empty Fabric Code Field After Callback.
                    cbDesignSubCategory.Text = "";  //Empty Design Sub Category Field After Callback.

                    cp.JSProperties["cp_UpdatePISNumber"] = true;
                    break;
                case "fabricsupplierfiltercase":
                    glFabricCode.Text = "";     //Empty Fabric Code Field After Callback.
                    break;
                case "fabriccodefiltercase":
                    glFabricColor.Text = "";    //Empty Fabric Color Field After Callback.

                    SetFabricDetails();
                    SetCompositionDetail();
                    break;
                case "gendercodefiltercase":
                    glFitCode.Text = "";    //Empty Fit Code Field After Callback.
                    cbProductSubCategory.Text = ""; //Empty Product Sub Category Field After Callback.
                    cp.JSProperties["cp_UpdatePISNumber"] = true;
                    break;
                case "productcatergoryfiltercase":
                    glFitCode.Text = "";            //Empty Fit Code Field After Callback.
                    cbDesignCategory.Text = "";     //Empty Design Category Field After Callback.
                    cbDesignSubCategory.Text = "";  //Empty Design Sub Category Field After Callback.
                    cbProductSubCategory.Text = ""; //Empty Product Sub Category Field After Callback.
                    cp.JSProperties["cp_UpdatePISNumber"] = true;
                    break;
                case "fitcodecase":

                    SetFitDetail();
                    SetSizeDetail2();

                    AddColumns();
                    GetMeasurementChartTemplate();
                    GetSelectedValGB(); 
                    Session["DatatableGB"] = "1";

                    DataTable getBaseSize = Gears.RetriveData2("SELECT LTRIM(RTRIM(StandardSize)) AS StandardSize FROM Masterfile.Fit where FitCode = '" + glFitCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    foreach (DataRow dt in getBaseSize.Rows)
                    {
                        txtBaseSize.Text = dt[0].ToString();
                    }
                    if (!String.IsNullOrEmpty(txtBaseSize.Text))
                    {
                        SetColor();
                    }

                    Session["AddColumns"] = "1";
                    break;
                case "setcase":
                    if (!String.IsNullOrEmpty(txtBaseSize.Text))
                    {
                        SetColor();
                    }
                    break;

                case "washandtintcodecase":
                    Session["WashCode"] = null;
                    Session["TintCode"] = null;
                    DataTable getWashCode = Gears.RetriveData2("SELECT Description FROM Masterfile.Wash WHERE ISNULL(IsInactive,0)=0 AND WashCode = '" + glWashCode.Text + "'", Session["ConnString"].ToString());
                    DataTable getTintColor = Gears.RetriveData2("SELECT Description FROM Masterfile.Tint WHERE ISNULL(IsInactive,0)=0 AND TintCode = '" + glTintColor.Text + "'", Session["ConnString"].ToString());
                    foreach (DataRow dt in getWashCode.Rows)
                    {
                        //if (getWashCode.Rows.Count > 0)
                        Session["WashCode"] = dt[0].ToString();
                        //else
                        //    Session["WashCode"] = null;
                    }
                    foreach (DataRow dt in getTintColor.Rows)
                    {
                        //if (getTintColor.Rows.Count > 0)
                        Session["TintCode"] = dt[0].ToString();
                        //else
                        //    Session["TintCode"] = null;
                    }

                    if (!String.IsNullOrEmpty(Session["WashCode"] as string) && String.IsNullOrEmpty(Session["TintCode"] as string))
                        memoWashDescription.Text = Session["WashCode"].ToString();
                    else if (!String.IsNullOrEmpty(Session["TintCode"] as string) && String.IsNullOrEmpty(Session["WashCode"] as string))
                        memoWashDescription.Text = Session["TintCode"].ToString();
                    else
                        memoWashDescription.Text = Session["WashCode"].ToString() + " + " + Session["TintCode"].ToString();


                    Session["WashCode"] = null;
                    Session["TintCode"] = null;
                    break;
                case "GetStepDetail":
                    GetStepDetail();
                    break;

                case "GetStyleDetail":
                    GetStyleDetail();
                    break;

                case "customercodecase":

                    DataTable getcustomercode = Gears.RetriveData2("SELECT Name FROM Masterfile.BPCustomerInfo where BizPartnerCode = '" + glCustomerCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    foreach (DataRow dt in getcustomercode.Rows)
                    {
                        txtCustomerName.Value = dt[0].ToString();
                    }
                    break;
                case "washsuppliercase":
                    DataTable getwashsupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glWashSupplier.Value + "'", Session["ConnString"].ToString());//ADD CONN
                    foreach (DataRow dt in getwashsupplier.Rows)
                    {
                        txtWashSupplierName.Text = dt[0].ToString();
                    }
                    break;

                case "embroidersuppliercase":
                    DataTable getembroidersupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glEmbroiderySupplier.Value + "'", Session["ConnString"].ToString());//ADD CONN
                    foreach (DataRow dt in getembroidersupplier.Rows)
                    {
                        txtEmbroiderSupplierDescription.Text = dt[0].ToString();
                    }
                    break;

                case "printsuppliercase":
                    DataTable getprintsupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glPrintSupplier.Value + "'", Session["ConnString"].ToString());//ADD CONN
                    foreach (DataRow dt in getprintsupplier.Rows)
                    {
                        txtPrintSupplierDescription.Text = dt[0].ToString();
                    }
                    break;

            } 
        }


        //protected void UpdatePISNumber()
        //{

        //    string yearNow = DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2, 2);
        //    string genderCode = string.IsNullOrEmpty(cbGender.Text.ToString()) ? "*" : cbGender.Value.ToString();
        //    string productCatCode = string.IsNullOrEmpty(cbProductCategory.Text.ToString()) ? "***" : cbProductCategory.Value.ToString();
        //    string productGroup = string.IsNullOrEmpty(cbProductGroup.Text.ToString()) ? "*" : cbProductGroup.Value.ToString();
        //    string productSubCat = string.IsNullOrEmpty(cbProductSubCategory.Text.ToString()) ? "*" : cbProductSubCategory.Value.ToString();
        //    string fobSupplier = string.IsNullOrEmpty(cbFOBSupplier.Text.ToString()) ? "*" : cbFOBSupplier.Value.ToString();
        //    txtPISNumber.Value = yearNow + "" + genderCode + "" + productCatCode + "" + productGroup + "" + productSubCat  + "*****" + fobSupplier;
        //}















        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            //CostCenterlookup.ConnectionString = Session["ConnString"].ToString();
            //PropertyNumberLookup.ConnectionString = Session["ConnString"].ToString();
            //AssetAcquisitionLookup.ConnectionString = Session["ConnString"].ToString();
            //VatCodeLookup.ConnectionString = Session["ConnString"].ToString();


            //CustomerCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //ProductCategoryLookup.ConnectionString = Session["ConnString"].ToString();
            //ProductGroupLookup.ConnectionString = Session["ConnString"].ToString();
            //FOBSupplierLookup.ConnectionString = Session["ConnString"].ToString();
            //ProductSubCategoryLookup.ConnectionString = Session["ConnString"].ToString();
            //DesignCategoryLookup.ConnectionString = Session["ConnString"].ToString();
            //DesignSubCategoryLookup.ConnectionString = Session["ConnString"].ToString();
            //ProductClassLookup.ConnectionString = Session["ConnString"].ToString();
            //ProductSubClassLookup.ConnectionString = Session["ConnString"].ToString();
            //GenderCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //BrandLookup.ConnectionString = Session["ConnString"].ToString();
            //SupplierCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //FabricCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //FabricColorLookup.ConnectionString = Session["ConnString"].ToString();
            //FitCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //WashCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //TintCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //EmbroideryCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //PrintCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //InkCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //StepCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //StyleCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //StepLookup.ConnectionString = Session["ConnString"].ToString();
            //ItemCategoryLookup.ConnectionString = Session["ConnString"].ToString();
            //ItemCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //ClassCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //ColorCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //SizeCodeLookup.ConnectionString = Session["ConnString"].ToString();
            //UnitLookup.ConnectionString = Session["ConnString"].ToString();

            //CompositionDataSource.ConnectionString = Session["ConnString"].ToString();


          

        }



        protected void gvSizeDetail1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.UpdateValues.Clear();
                //e.InsertValues.Clear();

            }

            if (check == true)
            {
                e.Handled = true;
                DataTable source = new DataTable();
                DataTable withdetailsource = new DataTable();
                if (Session["Datatable"] == "1")
                {
                    withdetailsource = GetMeasurementChart();
                    Gears.RetriveData2("DELETE FROM Masterfile.PISStyleChart WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN        
                }
                else if (Session["GetMeasurementChartTemplate"] == "1")
                {
                    withdetailsource = GetMeasurementChartTemplate();
                    Gears.RetriveData2("DELETE FROM Masterfile.PISStyleChart WHERE PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN        
                }
                //withdetailsource.primaryKey = new DataColumn[] { withdetailsource.Columns["LineNumber"] };

                source.Columns.Add("LineNumber", typeof(string));
                source.Columns.Add("PISNumber", typeof(string));
                source.Columns.Add("Sorting", typeof(int));
                source.Columns.Add("IsMajor", typeof(bool));
                source.Columns.Add("POMCode", typeof(string));
                source.Columns.Add("SizeCode", typeof(string));
                source.Columns.Add("Value", typeof(string));
                //source.Columns.Add("Tolerance", typeof(string));
                //source.Columns.Add("Bracket", typeof(int));
                //source.Columns.Add("Grade", typeof(string));
                //source.Columns.Add("Column2", typeof(string));
                DataTable getSizeCode = Gears.RetriveData2("SELECT RTRIM(LTRIM(SizeCode)) AS SizeCode FROM masterfile.FitSizeDetail WHERE FitCode = '" + glFitCode.Text + "' ORDER BY SizeCode", Session["ConnString"].ToString());//ADD CONN

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    foreach (DataRow dt in getSizeCode.Rows)
                    {
                        var LineNumber = i;
                        var PISNumber = "Temp";
                        var Sorting = values.NewValues["Order"];
                        var IsMajor = values.NewValues["IsMajor"];
                        var POMCode = values.NewValues["Code"];
                        var SizeCode = dt[0].ToString();
                        var Value = values.NewValues[dt[0].ToString()];
                        //var Tolerance = values.NewValues["Tolerance"];
                        //var Bracket = values.NewValues["Bracket"];
                        //var Grade = values.NewValues["Grade"];

                        //source.Rows.Add(LineNumber, PISNumber, Sorting, IsMajor, POMCode, SizeCode, Value, Tolerance, Bracket, Grade);
                        source.Rows.Add(LineNumber, PISNumber, Sorting, IsMajor, POMCode, SizeCode, Value);
                        i++;
                    }
                }

                //Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = 
                    { 
                        values.NewValues["LineNumberMeasurementChart"]
                    };
                    DataRow row = withdetailsource.Rows.Find(keys);
                    row["Order"] = values.NewValues["Order"];
                    row["IsMajor"] = values.NewValues["IsMajor"];
                    row["Code"] = values.NewValues["Code"];
                    //row["Tolerance"] = values.NewValues["Tolerance"];
                    //row["Bracket"] = values.NewValues["Bracket"];
                    //row["Grade"] = values.NewValues["Grade"];
                    foreach (DataRow dt in getSizeCode.Rows)
                    {
                        row[dt[0].ToString()] = values.NewValues[dt[0].ToString()];
                    }
                }

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["LineNumberMeasurementChart"] };
                        withdetailsource.Rows.Remove(withdetailsource.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (DataRow dtRow in withdetailsource.Rows)
                {
                    foreach (DataRow dt in getSizeCode.Rows)
                    {
                        var LineNumber = i;
                        var PISNumber = "Temp";
                        var Sorting = dtRow["Order"].ToString();
                        var IsMajor = dtRow["IsMajor"].ToString();
                        var POMCode = dtRow["Code"].ToString();
                        var SizeCode = dt[0].ToString();
                        var Value = dtRow[dt[0].ToString()].ToString();
                        //var Tolerance = dtRow["Tolerance"].ToString();
                        //var Bracket = dtRow["Bracket"].ToString();
                        //var Grade = dtRow["Grade"].ToString();

                        //source.Rows.Add(LineNumber, PISNumber, Sorting, IsMajor, POMCode, SizeCode, Value, Tolerance, Bracket, Grade);
                        source.Rows.Add(LineNumber, PISNumber, Sorting, IsMajor, POMCode, SizeCode, Value);

                        i++;
                    }
                }


                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    //_EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                    _EntityDetail.LineNumber = dtRow["LineNumber"].ToString();
                    _EntityDetail.PISNumber = dtRow["PISNumber"].ToString();

                    _EntityDetail.IsMajor = Convert.ToBoolean(dtRow["IsMajor"].ToString());
                    _EntityDetail.POMCode = dtRow["POMCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Value = dtRow["Value"].ToString();
                    //_EntityDetail.Tolerance = dtRow["Tolerance"].ToString();
                    //_EntityDetail.Bracket = dtRow["Bracket"].ToString();
                    //_EntityDetail.Grade = dtRow["Grade"].ToString();
                    _EntityDetail.Sorting = dtRow["Sorting"].ToString(); 

                    _EntityDetail.AddPISStyleChart(_EntityDetail);
                }
            }
        }




        private DataTable GetStepDetail()
        {

            gvStepDetail.DataSourceID = null;
            //gv1.DataBind();

            DataTable dt = new DataTable();

            StepDetailDS.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY Sequence) AS VARCHAR(5)),5) AS LineNumber, Sequence,StepCode AS StepCodeSteps, '' AS SupplierSteps, '' AS WorkCenterName, 0 AS EstimatedPrice from Masterfile.StepTemplateDetail WHERE StepTemplateCode = '" + glStepCode.Text + "' Order By Sequence ASC";


            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["purchaserequestdetail"] = sdsPicklistDetail.FilterExpression;
            gvStepDetail.DataSource = StepDetailDS;
            if (Request.QueryString["parameters"].ToString().Split('|')[0] != "CopyTrans")
                gvStepDetail.DataBind();
            Session["StepDetail"] = "1";

            foreach (GridViewColumn col in gvStepDetail.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvStepDetail.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvStepDetail.GetRowValues(i, col.ColumnName);
            }
            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};
            dt.Columns["LineNumber"]};
            return dt;
        }

        protected void gvStepDetail_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;

            }

            if (check == true)
            {
                e.Handled = true;
                DataTable source = GetStepDetail();

                int i = 1;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = source.Rows.Count + i;
                    var Sequence = values.NewValues["Sequence"];
                    var StepCodeSteps = values.NewValues["StepCodeSteps"];
                    var SupplierSteps = values.NewValues["SupplierSteps"];
                    var WorkCenterName = values.NewValues["WorkCenterName"];
                    var EstimatedPrice = values.NewValues["EstimatedPrice"];

                    source.Rows.Add(LineNumber, Sequence, StepCodeSteps, SupplierSteps, WorkCenterName, EstimatedPrice);

                    i++;

                }

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = 
                    { 
                        values.NewValues["LineNumber"]
                    };
                    DataRow row = source.Rows.Find(keys);
                    row["Sequence"] = values.NewValues["Sequence"];
                    row["StepCodeSteps"] = values.NewValues["StepCodeSteps"];
                    row["SupplierSteps"] = values.NewValues["SupplierSteps"];
                    row["WorkCenterName"] = values.NewValues["WorkCenterName"];
                    row["EstimatedPrice"] = values.NewValues["EstimatedPrice"];
                }

                //Removing all deleted rows from the data source(Excel file)
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

                //Gears.RetriveData2("DELETE FROM Masterfile.PISStepTemplate WHERE  PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());



                int count = 1;
                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    //_EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                    //source.Rows.Add(LineNumber, Sequence, StepCode, Supplier, Name, EstimatedPrice);
                    _EntityStepTemplate.LineNumber = count.ToString();
                    _EntityStepTemplate.Sequence = dtRow["Sequence"].ToString();
                    _EntityStepTemplate.StepCodeSteps = dtRow["StepCodeSteps"].ToString();
                    _EntityStepTemplate.SupplierSteps = dtRow["SupplierSteps"].ToString();
                    _EntityStepTemplate.EstimatedPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstimatedPrice"]) ? 0 : dtRow["EstimatedPrice"]);
                    //_EntityDetail.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                    //_EntityDetail.ReceivedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyCount"]) ? 0 : dtRow["QtyCount"]);

                    _EntityStepTemplate.AddPISStepTemplate(_EntityStepTemplate);
                    count++;
                }
            }
        }



        private DataTable GetStyleDetail()
        {

            gvStyleDetail.DataSourceID = null;
            //gv1.DataBind();

            DataTable dt = new DataTable();

            StyleDetailDS.SelectCommand = "SELECT DISTINCT StyleCode"
                                                + " ,Step AS StepCodeStyle"
                                                + " ,O.ItemCategoryCode AS ItemCategoryCodeStyle"
                                                + " ,IC.Description  AS ItemCategoryDescription"
                                                + " ,O.ProductCategoryCode AS ProductCategoryCodeStyle"
                                                + " ,PC.Description AS ProductCategoryDescription"
                                                + " ,O.KeySupplier AS SupplierCodeStyle"
                                                + " ,STD.Component AS ComponentStyle"
                                                + " ,STD.ItemCode AS ItemCodeStyle"
                                                + " ,O.ShortDesc AS ItemDescription"
                                                + " ,'' AS StockSize"
                                                + " ,0 AS BySize"
                                                + " ,STD.ColorCode AS ColorCodeStyle"
                                                + " ,STD.ClassCode AS ClassCodeStyle"
                                                + " ,STD.SizeCode AS SizeCodeStyle"
                                                + " ,0.00 AS PerPieceConsumption"
                                                + " ,O.UnitBase AS UnitStyle"
                                                + " ,0.00 AS EstimatedUnitCost"
                                                + " ,0.00 AS EstimatedCost"
                                                + " ,ISNULL(ID.ItemImage,'') AS PictureBOM   "
                                                + " INTO #STD"
                                                + " FROM Masterfile.StyleTemplateDetail STD "
                                                + " LEFT JOIN Masterfile.Item O "
                                                + " ON STD.ItemCode = O.ItemCode"
                                                + " LEFT JOIN Masterfile.ItemCategory IC"
                                                + " ON O.ItemCategoryCode = IC.ItemCategoryCode"
                                                + " LEFT JOIN Masterfile.ProductCategory PC"
                                               + " ON O.ProductCategoryCode = PC.ProductCategoryCode"
                                                + " LEFT JOIN Masterfile.ItemDetail ID"
                                                + " ON STD.ItemCode = ID.ItemCode"
                                                + " AND STD.ColorCode = ID.ColorCode"
                                                + " AND STD.ClassCode = ID.ClassCode"
                                                + " AND STD.SizeCode = ID.SizeCode"
                                                + " WHERE StyleCode = '" + glStyleCode.Text + "' "
                                                + " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY StyleCode) AS VARCHAR(5)),5) AS LineNumber, *"
                                                + " FROM #STD";




            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["purchaserequestdetail"] = sdsPicklistDetail.FilterExpression;
            gvStyleDetail.DataSource = StyleDetailDS;

            if (Request.QueryString["parameters"].ToString().Split('|')[0] != "CopyTrans")
                gvStyleDetail.DataBind();
            Session["StyleDetail"] = "1";

            foreach (GridViewColumn col in gvStyleDetail.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvStyleDetail.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvStyleDetail.GetRowValues(i, col.ColumnName);
            }
            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};
            dt.Columns["LineNumber"]};
            cp.JSProperties["cp_bitbycode"] = "true";
            return dt;
        }

        protected void gvStyleDetail_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;

            }

            if (Session["StyleDetail"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetStyleDetail();

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = i;
                    var StepCode = values.NewValues["StepCodeStyle"];
                    var ItemCategoryCode = values.NewValues["ItemCategoryCodeStyle"];
                    var ItemCategoryDescription = values.NewValues["ItemCategoryDescription"];
                    var ProductCategoryCode = values.NewValues["ItemCategoryCodeStyle"];
                    var ProductCategoryDescription = values.NewValues["ProductCategoryDescription"];
                    var SupplierCode = values.NewValues["SupplierCodeStyle"];
                    var ItemCode = values.NewValues["ItemCodeStyle"];
                    var ItemDescription = values.NewValues["ItemDescription"];
                    var Component = values.NewValues["ComponentStyle"];
                    var StockSize = values.NewValues["StockSize"];
                    var BySize = values.NewValues["BySize"];
                    var ColorCode = values.NewValues["ColorCodeStyle"];
                    var ClassCode = values.NewValues["ColorCodeStyle"];
                    var SizeCode = values.NewValues["SizeCodeStyle"];
                    var PerPieceConsumption = values.NewValues["PerPieceConsumption"];
                    var Unit = values.NewValues["UnitStyle"];
                    var EstimatedUnitCost = values.NewValues["EstimatedUnitCost"];
                    var EstimatedCost = values.NewValues["EstimatedCost"];

                    source.Rows.Add(LineNumber, StepCode, ItemCategoryCode, ItemCategoryDescription, ProductCategoryCode, ProductCategoryDescription, SupplierCode, Component
                        , ItemCode, ItemDescription, StockSize, BySize, ColorCode, ClassCode, SizeCode, PerPieceConsumption, Unit, EstimatedUnitCost, EstimatedCost);

                    i++;

                }

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = 
                    { 
                        values.NewValues["LineNumber"]
                    };
                    DataRow row = source.Rows.Find(keys);
                    row["StepCodeStyle"] = values.NewValues["StepCodeStyle"];
                    row["ItemCategoryCodeStyle"] = values.NewValues["ItemCategoryCodeStyle"];
                    row["ItemCategoryDescription"] = values.NewValues["ItemCategoryDescription"];
                    row["ProductCategoryCodeStyle"] = values.NewValues["ProductCategoryCodeStyle"];
                    row["ProductCategoryDescription"] = values.NewValues["ProductCategoryDescription"];
                    row["SupplierCodeStyle"] = values.NewValues["SupplierCodeStyle"];
                    row["ItemCodeStyle"] = values.NewValues["ItemCodeStyle"];
                    row["ItemDescription"] = values.NewValues["ItemDescription"];
                    row["ComponentStyle"] = values.NewValues["ComponentStyle"];
                    row["StockSize"] = values.NewValues["StockSize"];
                    row["BySize"] = values.NewValues["BySize"];
                    row["ColorCodeStyle"] = values.NewValues["ColorCodeStyle"];
                    row["ClassCodeStyle"] = values.NewValues["ClassCodeStyle"];
                    row["SizeCodeStyle"] = values.NewValues["SizeCodeStyle"];
                    row["PerPieceConsumption"] = values.NewValues["PerPieceConsumption"];
                    row["UnitStyle"] = values.NewValues["UnitStyle"];
                    row["EstimatedUnitCost"] = values.NewValues["EstimatedUnitCost"];
                    row["EstimatedCost"] = values.NewValues["EstimatedCost"];
                }

                //Removing all deleted rows from the data source(Excel file)
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
                    //_EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                    //source.Rows.Add(LineNumber, Sequence, StepCode, Supplier, Name, EstimatedPrice);
                    _EntityStyleTemplate.LineNumber = dtRow["LineNumber"].ToString();
                    _EntityStyleTemplate.StepCodeStyle = dtRow["StepCodeStyle"].ToString();
                    _EntityStyleTemplate.ItemCategoryCodeStyle = dtRow["ItemCategoryCodeStyle"].ToString();
                    //_EntityStyleTemplate.ItemCategoryDescription = dtRow["ItemCategoryDescription"].ToString();
                    _EntityStyleTemplate.ProductCategoryCodeStyle = dtRow["ProductCategoryCodeStyle"].ToString();
                    //_EntityStyleTemplate.ProductCategoryDescription = dtRow["ProductCategoryDescription"].ToString();
                    _EntityStyleTemplate.SupplierCodeStyle = dtRow["SupplierCodeStyle"].ToString();
                    _EntityStyleTemplate.ItemCodeStyle = dtRow["ItemCodeStyle"].ToString();
                    _EntityStyleTemplate.ComponentStyle = dtRow["ComponentStyle"].ToString();
                    //_EntityStyleTemplate.ItemDescription = dtRow["ItemDescription"].ToString();
                    _EntityStyleTemplate.StockSize = dtRow["StockSize"].ToString();
                    //_EntityStyleTemplate.BySize = Convert.ToBoolean(Convert.ToInt16(dtRow["BySize"].ToString()));
                    _EntityStyleTemplate.ColorCodeStyle = dtRow["ColorCodeStyle"].ToString();
                    _EntityStyleTemplate.ClassCodeStyle = dtRow["ClassCodeStyle"].ToString();
                    _EntityStyleTemplate.SizeCodeStyle = dtRow["SizeCodeStyle"].ToString();
                    _EntityStyleTemplate.PerPieceConsumption = Convert.ToDecimal(Convert.IsDBNull(dtRow["PerPieceConsumption"]) ? 0 : dtRow["PerPieceConsumption"]);
                    _EntityStyleTemplate.UnitStyle = dtRow["UnitStyle"].ToString();
                    _EntityStyleTemplate.EstimatedUnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstimatedUnitCost"]) ? 0 : dtRow["EstimatedUnitCost"]);
                    _EntityStyleTemplate.EstimatedCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstimatedCost"]) ? 0 : dtRow["EstimatedCost"]);


                    _EntityStyleTemplate.AddPISStyleTemplate(_EntityStyleTemplate);
                }
            }
        }

        #region Object Init
        protected void glColorCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvThreadDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                ColorCodeLookup.SelectCommand = "SELECT ColorCode, Description FROM Masterfile.Color WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "ColorCodeLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glThreadColor")
                        && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                        && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string colorcode = param.Split(';')[1];
                DataTable getDetail = Gears.RetriveData2("SELECT ISNULL(R,'') AS R,ISNULL(G,'') AS G,ISNULL(B,'') AS B FROM Masterfile.Color WHERE ColorCode = '" + colorcode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["R"].ToString() + ";";
                    codes += dt["G"].ToString() + ";";
                    codes += dt["B"].ToString() + ";";

                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "FitColorCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }
        }
        protected void lookup_Init(object sender, EventArgs e)
        {

            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvEmbroiderDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                EmbroideryCodeLookup.SelectCommand = "SELECT EmbroideryCode AS EmbroCode, Description AS EmbroDescription FROM Masterfile.Embroidery WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "EmbroideryCodeLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glEmbroCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string embrocode = param.Split(';')[1];
                DataTable getDetail = Gears.RetriveData2("SELECT Description, Height, Width from masterfile.Embroidery WHERE EmbroideryCode = '" + embrocode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";
                    codes += dt["Height"].ToString() + ";";
                    codes += dt["Width"].ToString() + ";";

                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "EmbroCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }
        }
        protected void glPrintCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvPrintDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                PrintCodeLookup.SelectCommand = "SELECT ProcessCode, Description FROM Masterfile.PrintProcess WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "PrintCodeLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glPrintCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string printcode = param.Split(';')[1];
                DataTable getDetail = Gears.RetriveData2("SELECT Description from masterfile.PrintProcess WHERE ProcessCode = '" + printcode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";

                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "PrintCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }

        }
        protected void glInkCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvPrintDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                InkCodeLookup.SelectCommand = "SELECT InkCode, Description FROM Masterfile.PrintInk WHERE IsNull(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "InkCodeLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glInkCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string inkcode = param.Split(';')[1];
                DataTable getDetail = Gears.RetriveData2("SELECT Description from masterfile.PrintInk WHERE InkCode = '" + inkcode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";

                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "InkCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;

            }
        }
        protected void glPOMCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvSizeDetail1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                //if (Session["FilterExpression"] != null)
                //{
                //    gridLookup.GridView.DataSourceID = "POMLookup";
                //    AssetAcquisitionLookup.FilterExpression = Session["FilterExpression"].ToString();
                //    //Session["FilterExpression"] = null;
                //}
                //else
                //{
                //    gridLookup.GridView.DataSourceID = "POMLookup";
                //}
            }
        }
        protected void glStepCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                StepLookup.SelectCommand = "SELECT StepCode, Description FROM masterfile.Step WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "StepLookup";

                //if(Request.Params["__CALLBACKPARAM"].Contains("SELFIELDVALUES"))
                //    gridLookup.GridView.JSProperties["cp_RowClick"] = true;
                //if(Request.Params["__CALLBACKPARAM"].Contains("SELECTROWS"))
                //    gridLookup.GridView.JSProperties["cp_SetNull"] = true;
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glStepCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                gridLookup.GridView.JSProperties["cp_identifier"] = "StepCodeStyle";
            }
        }

        protected void glStepCode_Init1(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStepDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                StepLookup.SelectCommand = "SELECT StepCode, Description FROM masterfile.Step WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "StepLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glStepCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                gridLookup.GridView.JSProperties["cp_identifier"] = "StepCodeSteps";
            }
        }
        protected void glItemCategoryCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            //{
            //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //    ItemCategoryLookup.SelectCommand = "SELECT ItemCategoryCode, Description FROM Masterfile.ItemCategory WHERE ISNULL(IsInactive,0)=0";
            //    gridLookup.GridView.DataSourceID = "ItemCategoryLookup";

            //    if (Request.Params["__CALLBACKPARAM"].Contains("SELFIELDVALUES"))
            //        gridLookup.GridView.JSProperties["cp_RowClick"] = true;
            //    if (Request.Params["__CALLBACKPARAM"].Contains("SELECTROWS"))
            //        gridLookup.GridView.JSProperties["cp_SetNull"] = true;
            //}
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCategoryCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                DataTable dataTable = HttpContext.Current.Cache["ItemCategoryDS"] as DataTable;
                if (dataTable == null)
                {
                    dataTable = Gears.RetriveData2("SELECT ItemCategoryCode, Description FROM Masterfile.ItemCategory WHERE ISNULL(IsInactive,0)=0", Session["ConnString"].ToString());
                    HttpContext.Current.Cache["ItemCategoryDS"] = dataTable;
                }
                gridLookup.DataSource = dataTable;
                gridLookup.DataBind();
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCategoryCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string itemcategory = param.Split(';')[1];
                DataTable getDetail = Gears.RetriveData2("SELECT Description FROM Masterfile.Itemcategory WHERE ItemCategoryCode = '" + itemcategory + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";

                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "ItemCategoryCodeStyle";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }
        }
        protected void glProductCategoryCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ProductCategory_CustomCallback);
                if (Session["FilterProductCategoryCodeStyle"] != null)
                {
                    gridLookup.GridView.DataSource = Session["FilterProductCategoryCodeStyle"];
                }
                else
                {
                    gridLookup.GridView.DataSource = ProductCategoryLookup;
                }
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glProductCategoryCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string productcategorycode = param.Split(';')[1];
                DataTable getDetail = Gears.RetriveData2("SELECT ProductCategoryCode, Description FROM Masterfile.ProductCategory WHERE ISNULL(IsInactive,0)=0 AND ISNULL(ProductCategoryCode,'')!='' AND ProductCategoryCode = '" + productcategorycode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";

                }
                gridLookup.GridView.JSProperties["cp_identifier"] = "ProductCategoryCodeStyle";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }

        }
        protected void glSupplierCode_Init1(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                gridLookup.GridView.DataSourceID = "SupplierCodeLookup";

            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glSupplierCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                gridLookup.GridView.JSProperties["cp_identifier"] = "SupplierCodeStyle";
            }
        }
        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                if (Session["FilterItemCodeStyle"] != null)
                {
                    gridLookup.GridView.DataSource = Session["FilterItemCodeStyle"];
                }
                else
                {
                    gridLookup.GridView.DataSource = ItemCodeLookup;
                }
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string itemcode = param.Split(';')[1];

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

                DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                                                               "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

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


                DataTable getUnitDescLine = Gears.RetriveData2("SELECT TOP 1 FullDesc,UnitBase, EstimatedCost, ID.ItemImage  FROM Masterfile.Item I LEFT JOIN Masterfile.ItemDetail ID ON I.ItemCode = ID.ItemCode WHERE I.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in getUnitDescLine.Rows)
                {
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["EstimatedCost"].ToString() + ";";
                    codes += dt["ItemImage"].ToString() + ";";
                }


                gridLookup.GridView.JSProperties["cp_identifier"] = "ItemCodeStyle";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }
        }
        protected void glComponent_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                ComponentCodeLookup.SelectCommand = "SELECT ComponentCode, Description FROM Masterfile.Component WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "ComponentCodeLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glComponent")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                gridLookup.GridView.JSProperties["cp_identifier"] = "ComponentStyle";
            }
        }
        protected void glStockSize_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                SizeCodeLookup.SelectCommand = "SELECT SizeCode, Description from Masterfile.Size WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "SizeCodeLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glStockSize")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                gridLookup.GridView.JSProperties["cp_identifier"] = "StockSizeStyle";
            }
        }
        protected void glColorCode_Init1(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallbackItemStyle);
                if (Session["FilterExpressionPISItemCodeStyle"] != null)
                {
                    gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                    Masterfileitemdetail.FilterExpression = Session["FilterExpressionPISItemCodeStyle"].ToString();
                    //Session["FilterExpression"] = null;
                }
                else
                {
                    gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                }

                if (IsCallback && Request.Params["__CALLBACKID"].Contains("glColorCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
                {
                    gridLookup.GridView.JSProperties["cp_identifier"] = "ColorCodeStyle";
                }

                if (IsCallback && Request.Params["__CALLBACKID"].Contains("glClassCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
                {
                    gridLookup.GridView.JSProperties["cp_identifier"] = "ClassCodeStyle";
                }

                if (IsCallback && Request.Params["__CALLBACKID"].Contains("glSizeCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
                {
                    gridLookup.GridView.JSProperties["cp_identifier"] = "SizeCodeStyle";
                }

                if (IsCallback && Request.Params["__CALLBACKID"].Contains("glUnitBase")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
                {
                    gridLookup.GridView.JSProperties["cp_identifier"] = "UnitCodeStyle";
                }
            }
        }
        protected void glSupplierCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStepDetail") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                gridLookup.GridView.DataSourceID = "SupplierCodeLookup";
            }

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glSupplierCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string suppliercode = param.Split(';')[1];
                DataTable getDetail = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo WHERE SupplierCode = '" + suppliercode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Name"].ToString() + ";";

                }
                gridLookup.GridView.JSProperties["cp_identifier"] = "SupplierCodeSteps";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
            }
        }
        #endregion

        protected void glWashSupplier_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glWashSupplier") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(Header_CustomCallback);
                SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                gridLookup.GridView.DataSourceID = "SupplierCodeLookup";

            }



            //if (IsCallback && Request.Params["__CALLBACKID"].Contains("gvStyleDetail")
            //      && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            //{
            //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(Header_CustomCallback);
            //    //SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
            //    //gridLookup.GridView.DataSourceID = "SupplierCodeLookup";
            //    //gridLookup.DataBind();
            //}
        }
        protected void glWashCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glWashCode"))
            {
                WashCodeLookup.SelectCommand = "SELECT WashCode, Description FROM Masterfile.Wash WHERE ISNULL(IsInactive,0)=0";
                gridLookup.GridView.DataSourceID = "WashCodeLookup";
                gridLookup.DataBind();
            }
        }
        protected void glTintColor_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glTintColor"))
            {
                TintCodeLookup.SelectCommand = "SELECT TintCode, Description FROM Masterfile.Tint WHERE ISNULL(IsInactive,0)=0 AND ISNULL(TintCode,'')!=''";
                gridLookup.GridView.DataSourceID = "TintCodeLookup";
                gridLookup.DataBind();
            }
        }
        protected void glEmbroiderySupplier_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glEmbroiderySupplier") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(Header_CustomCallback);
                SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                gridLookup.GridView.DataSourceID = "SupplierCodeLookup";

            }


        }
        protected void glPrintSupplier_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glPrintSupplier") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(Header_CustomCallback);
                SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                gridLookup.GridView.DataSourceID = "SupplierCodeLookup";

            }

        }
        protected void glFabricSupplier_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glFabricSupplier"))
            {
                SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                gridLookup.GridView.DataSourceID = "SupplierCodeLookup";
                gridLookup.DataBind();
            }
        }
        protected void glFabricCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //if (IsCallback && Request.Params["__CALLBACKID"].Contains("glFabricSupplier")
            //    && Request.Params["__CALLBACKPARAM"].Contains("GLP_AIC")
            //    && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            //{
            //    FabricCodeLookup.SelectCommand = "SELECT FabricCode, FabricDescription FROM Masterfile.Fabric WHERE FabricGroup = '" + cbProductGroup.Value + "' AND FabricSupplier = '" + glFabricSupplier.Text + "' ";
            //    //FabricCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
            //    //gridLookup.GridView.DataSourceID = "FabricCodeLookup";
            //    //gridLookup.DataBind();
            //}

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glFabricCode"))
            {
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 2);
                string suppliercode = param.Split(';')[1];
                FabricCodeLookup.SelectCommand = "SELECT FabricCode, B.FullDesc AS FabricDescription FROM Masterfile.Fabric A INNER JOIN Masterfile.Item B ON A.FabricCode = B.ItemCode WHERE FabricGroup = '" + cbProductGroup.Value + "' AND FabricSupplier = '" + glFabricSupplier.Text + "' ";
                //SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                //gridLookup.GridView.DataSourceID = "SupplierCodeLookup";
                //gridLookup.DataBind();
            }
        }
        protected void glFabricColor_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //if (IsCallback && Request.Params["__CALLBACKID"].Contains("glFabricSupplier")
            //    && Request.Params["__CALLBACKPARAM"].Contains("GLP_AIC")
            //    && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            //{
            //    FabricCodeLookup.SelectCommand = "SELECT FabricCode, FabricDescription FROM Masterfile.Fabric WHERE FabricGroup = '" + cbProductGroup.Value + "' AND FabricSupplier = '" + glFabricSupplier.Text + "' ";
            //    //FabricCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
            //    //gridLookup.GridView.DataSourceID = "FabricCodeLookup";
            //    //gridLookup.DataBind();
            //}

            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glFabricColor"))
            {
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                string suppliercode = param.Split(';')[0];

                FabricColorLookup.SelectCommand = "SELECT DISTINCT ColorCode, ItemCode AS FabricCode FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0)=0 AND ItemCode = '" + suppliercode + "' ";
                //SupplierCodeLookup.SelectCommand = "SELECT SupplierCode,Name FROM Masterfile.BPSupplierInfo WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SupplierCode,'')!=''";
                //gridLookup.GridView.DataSourceID = "SupplierCodeLookup";
                //gridLookup.DataBind();
            }
        }


        public void Header_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F") || e.Parameters == "") return;
            ASPxGridView grid = sender as ASPxGridView;
            string codes = "";
            string supplier = e.Parameters.Split('|')[1];//Set column name
            //string productcategoru = e.Parameters.Split('|')[2];//Set Item Code

            if (e.Parameters.Contains("embroidersuppliercase"))
            {
                DataTable getembroidersupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + supplier + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in getembroidersupplier.Rows)
                {
                    codes = dt[0].ToString();
                    grid.JSProperties["cp_identifierheader"] = "EmbroiderSupplier";
                    grid.JSProperties["cp_codes"] = codes;
                }
            }

            else if (e.Parameters.Contains("printsuppliercase"))
            {
                DataTable getembroidersupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + supplier + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in getembroidersupplier.Rows)
                {
                    codes = dt[0].ToString();
                    grid.JSProperties["cp_identifierheader"] = "PrintSupplier";
                    grid.JSProperties["cp_codes"] = codes;
                }
            }

            else if (e.Parameters.Contains("washsuppliercase"))
            {
                DataTable getembroidersupplier = Gears.RetriveData2("SELECT Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + supplier + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in getembroidersupplier.Rows)
                {
                    codes = dt[0].ToString();
                    grid.JSProperties["cp_identifierheader"] = "WashSupplier";
                    grid.JSProperties["cp_codes"] = codes;
                }
            }

            //Session["FilterItemCodeStyle"] = ItemCodeLookup;
            //grid.DataSource = ItemCodeLookup;
            //grid.DataBind();

        }

        #region Images
        protected void btnFrontUpload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            //DateTime date = DateTime.Now;
            //byte[] bytes = e.UploadedFile.FileBytes;
            //Gears.RetriveData2("UPDATE Production.ProductInfoSheet SET FrontImage = '" + bytes +"' WHERE PISNumber='16BBD200001JA'", Session["ConnString"].ToString());//ADD CONN

            //string fileName = e.UploadedFile.FileName;
            //string extension = e.UploadedFile.ContentType;

            //int length = (int)e.UploadedFile.ContentLength;
            //byte[] file = new byte[length];
            //e.UploadedFile.FileContent.Read(file, 0, length);
            //byte[] bytes = e.UploadedFile.FileBytes;

            //Stream stream = e.UploadedFile.FileContent;
            //BinaryReader binaryReader = new BinaryReader(stream);
            //byte[] file = new byte[e.UploadedFile.ContentLength];
            //e.UploadedFile.FileContent.Read(file, 0, (int)e.UploadedFile.ContentLength);

            //Gears.RetriveData2("DECLARE @File varbinary(MAX) = CONVERT(VARBINARY(MAX),'" + bytes + "',1)"
            //            + " exec sp_UploadPhotoToDatebase '" + fileName + "','" + DateTime.Now + "','" + extension + "',@File", Session["ConnString"].ToString());


            //Gears.RetriveData2("exec sp_UploadPhotoToDatebase '" + fileName + "','" + DateTime.Now + "','" + extension + "','" + bytes.Length + "'", Session["ConnString"].ToString());
            //_EntityUploadPicture.Connection = Session["ConnString"].ToString();    
            ////_EntityStepTemplate.LineNumber = count.ToString();
            //_EntityUploadPicture.FileName = e.UploadedFile.FileName;
            //_EntityUploadPicture.DateUploaded = date.ToString();
            //_EntityUploadPicture.MIME = e.UploadedFile.ContentType;
            //_EntityUploadPicture.Image = e.UploadedFile.FileBytes;
            ////_EntityStepTemplate.StepCode = dtRow["StepCode"].ToString();
            ////_EntityStepTemplate.Supplier = dtRow["Supplier"].ToString();
            ////_EntityStepTemplate.EstimatedPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstimatedPrice"]) ? 0 : dtRow["EstimatedPrice"]);
            ////_EntityDetail.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
            ////_EntityDetail.ReceivedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyCount"]) ? 0 : dtRow["QtyCount"]);

            //_EntityUploadPicture.AddPISEmbroideryDetail(_EntityUploadPicture);


            //SqlConnection sqlConnection1 = new SqlConnection(Session["ConnString"].ToString());
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;
            //sqlConnection1.Open();

            //Image1.ImageUrl = "ImageHandler.ashx?roll_no=1";  
            //using (SqlCommand cmd = new SqlCommand("SELECT ImageData FROM Masterfile.PISETC WHERE FileName = '" + e.UploadedFile.FileName + "' AND DateUploaded = '" + date + "'", sqlConnection1))
            //{
            //    // Replace 8000, below, with the correct size of the field

            //byte[] img = (byte[])cmd.ExecuteScalar();
            //sqlConnection1.Close();
            //string strBase64 = Convert.ToBase64String(img);
            //Image1.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
            //Image.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
            //Session["FrontImage"] = "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
            //cp.JSProperties["cp_frontimage"] = "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
            //Session["FrontImageFileName"] = e.UploadedFile.FileName;
            //Session["FrontImageDateUploaded"] = date;
            //e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
            //Session["LuisTry"] = e.UploadedFile.FileBytes;
            //Session["LuisTry2"] = "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
            //Image2.ContentBytes = (byte[])img;

            //Image1 = byteArrayToImage(img);
            //Image = Image.FromStream(ms);
            //Image = Image.
            //imgLogoCompany.Src = String.Format("data:image/Bmp;base64,{0}\"", imgString);

            //Session["FrontImage"] = e.UploadedFile.FileBytes;
            txtFrontImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);


            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
            //}
        }
        protected void btnBackUpload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            txtBackImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }
        protected void UC2DFront_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            txt2DFrontImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }
        protected void UC2DBack_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            txt2DBackImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }
        protected void gvuploadimageembroider_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }
        protected void gvuploadimageprint_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }
        protected void gvgvuploadimageotherimage_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }
        #endregion

        #region MyFunctions
        protected void AddColumns()
        {
            Session["AddColumns"] = "0";
            //ASPxGridView1.KeyFieldName = "ProductID";
            //gvSizeDetail1.DataSourceID = "sdsSizeDetail";
            //gvSizeDetail1.DataSourceID = null;
            gvSizeDetail1.AutoGenerateColumns = false;

            DataTable getSizeCode = Gears.RetriveData2("SELECT RTRIM(LTRIM(SizeCode)) AS SizeCode from masterfile.FitSizeDetail WHERE FitCode = '" + glFitCode.Text + "' ORDER BY SizeCode", Session["ConnString"].ToString());//ADD CONN

            int x = 8;
             
            foreach (DataRow dt in getSizeCode.Rows)
            {
                //gvSizeDetail1.Columns.Add(new GridViewDataTextColumn() { FieldName = dt[0].ToString(), VisibleIndex = x, Width = 50, ReadOnly=false, UnboundType=DevExpress.Data.UnboundColumnType.Integer } 
                //});

                //GridViewDataSpinEditColumn spin = new GridViewDataSpinEditColumn();
                //gvSizeDetail1.Columns.Add(new GridViewDataSpinEditColumn() { FieldName = dt[0].ToString(), VisibleIndex = x, Width = 50, ReadOnly = false, UnboundType = DevExpress.Data.UnboundColumnType.Integer, PropertiesSpinEdit.ClientSideEvents.ValueChanged="autotry"});
                //gvSizeDetail1.ClientSideEvents.Init = "try_Init";
                //spin.PropertiesSpinEdit.ClientSideEvents.ValueChanged = "autotry";
                //GridViewDataSpinEditColumn spin = new GridViewDataSpinEditColumn();
                //spin.FieldName = dt[0].ToString();
                //spin.VisibleIndex = x;
                //spin.Width = 50;
                //spin.ReadOnly = false;
                //spin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                //spin.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                //spin.PropertiesSpinEdit.ClientSideEvents.ValueChanged = "computesizesvalue";

                //gvGradeBracket.DataBind();
                //DataTable dtgb = new DataTable();
                //int alreadythere = 0;  
                //foreach (GridViewColumn col in gvGradeBracket.VisibleColumns)
                //{
                //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
                //    if (dataColumn == null) continue;

                //    if (dt[0].ToString() == dataColumn.FieldName)
                //        alreadythere++; 
                //}
                //if (alreadythere == 0)
                //{
                    GridViewDataTextColumn text = new GridViewDataTextColumn();
                    text.FieldName = dt[0].ToString();
                    text.VisibleIndex = x;
                    text.Width = 50;
                    text.ReadOnly = false;
                    //spin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                    text.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    text.PropertiesTextEdit.ClientSideEvents.ValueChanged = "autoTolerance";

                    gvSizeDetail1.Columns.Add(text);
                    //gvSizeDetail1.Columns(x).OptionColumn.AllowEdit = true;
                    x++;
                //}
            }


            //gvSizeDetail1.DataSource = getSizeCode;
            //ASPxGridView1.DataSource = ds;
            gvSizeDetail1.DataBind();

            //"SELECT *,"  FROM Masterfile.PISStyleChart WHERE PISNumber is null
            //gvSizeDetail2.Columns["SizeCode"].ExportCellStyle.BackColor = System.Drawing.Color.Red;
            //gvSizeDetail2.Columns["SizeName"].CellStyle.BackColor = System.Drawing.Color.LimeGreen;

        }
        protected void DeleteSession()
        {
            Session["AddColumns"] = null;
            Session["StyleDetail"] = null;
            Session["StepDetail"] = null;
            Session["Datatable"] = null;
            Session["GetMeasurementChartTemplate"] = null;
            Session["ColDT"] = null;
            Session["ThreadFit"] = null;
            Session["PrintDetail"] = null;
            Session["Embroider"] = null;
            Session["OtherPictures"] = null;
        }
        protected void SetColor()
        {
            try
            {

                foreach (GridViewColumn column in gvSizeDetail1.VisibleColumns)
                {
                    if (column is GridViewColumn)
                    {
                        GridViewColumn dataColumn = (GridViewColumn)column;
                        if (dataColumn.Visible)
                            dataColumn.CellStyle.BackColor = ColorTranslator.FromHtml("#ffffff");
                            dataColumn.HeaderStyle.BackColor = ColorTranslator.FromHtml("#ffffff");
                        
                    }
                  
                }


         
                gvSizeDetail1.Columns[txtBaseSize.Text.TrimEnd()].HeaderStyle.BackColor = ColorTranslator.FromHtml("#66ff33");
                gvSizeDetail1.Columns[txtBaseSize.Text.TrimEnd()].CellStyle.BackColor = ColorTranslator.FromHtml("#66ff33");
              
                txtBaseSizeBOM.Text = txtBaseSize.Text;
            }
            catch
            {
                cp.JSProperties["cp_colorerror"] = "Base size " + txtBaseSize.Text + " not found in size list.";
                txtBaseSize.Text = "";
            }

        }
        protected void SetCompositionDetail()
        {
            CompositionDataSource.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY FCD.FabricCode) AS VARCHAR(5)),5) AS LineNumber "
                                                            + ",FCD.FabricCode, FCD.Percentage, GL.Description AS Composition"
                                                            + " FROM Masterfile.FabricCompositionDetail FCD"
                                                            + " INNER JOIN IT.GenericLookup GL"
                                                            + " ON FCD.Type = GL.Code"
                                                            + " WHERE LookUpKey = 'COMTP' AND FCD.FabricCode = '" + glFabricCode.Text + "' ";
            gvComposition.DataSource = CompositionDataSource;
            gvComposition.DataBind();
        }
        protected void SetFabricDetails()
        {

            DataTable getDetails = Gears.RetriveData2("SELECT FabricGroup, FabricDesignCategory, Dyeing"
                                              + ",WeaveType,CuttableWidth, GrossWidth, ForKnitsOnly"
                                              + ",CuttableWeightBW, GrossWeightBW, Yield, FabricStretch"
                                              + ",WarpConstruction, WeftConstruction"
                                              + ",WarpDensity, WeftDensity"
                                              + ",WarpShrinkage, WeftShrinkage FROM Masterfile.Fabric where FabricCode = '" + glFabricCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getDetails.Rows)
            {
                txtFabricGroup.Value = dt[0].ToString();
                txtFabricDesignCategory.Value = dt[1].ToString();
                txtDyeingMethod.Value = dt[2].ToString();
                txtWeaveType.Value = dt[3].ToString();
                txtCuttableWidth.Value = dt[4].ToString();
                txtGrossWidth.Value = dt[5].ToString();
                txtForKnitsOnly.Value = dt[6].ToString();
                txtCuttableWeightBW.Value = dt[7].ToString();
                txtGrossWeightBW.Value = dt[8].ToString();
                txtYield.Value = dt[9].ToString();
                txtFabricStretch.Value = dt[10].ToString();
                txtWarpConstruction.Value = dt[11].ToString();
                txtWeftConstruction.Value = dt[12].ToString();
                txtWarpWeave.Value = dt[13].ToString();
                txtWeftWeave.Value = dt[14].ToString();
                txtShrinkageWarp.Value = dt[15].ToString();
                txtShrinkageWeft.Value = dt[16].ToString();
            }

        }
        protected void SetFitDetail()
        {
            DataTable getDetails = Gears.RetriveData2("SELECT Waist, FitType, Silhouette, MasterPattern FROM Masterfile.Fit WHERE ISNULL(IsInactive,0)=0 AND FitCode = '" + glFitCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in getDetails.Rows)
            {
                txtWaistNeckLine.Value = dt[0].ToString();
                txtFit.Value = dt[1].ToString();
                txtSilhouette.Value = dt[2].ToString();
                txtMasterPattern.Value = dt[3].ToString();
            }
        }
        protected void SetLookupFilter()
        {

            // ** Product Sub Category Lookup Filter ** //
            ProductSubCategoryLookup.SelectCommand = "SELECT Mnemonics, Description FROM Masterfile.ProductCategorySub WHERE ISNULL(IsInactive,0)=0 AND ISNULL(ProductSubCatCode,'')!='' AND ISNULL(Mnemonics,'')!=''  AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value + "%' AND ISNULL(Gender,'') like '%" + cbGender.Value + "%'";
            cbProductSubCategory.DataBind();
            // ** ** ** ... //

            // ** Design Category Code Lookup Filter ** //
            DesignCategoryLookup.SelectCommand = "SELECT CategoryCode, Description FROM Masterfile.DesignCategory WHERE ISNULL(IsInactive,0)=0 AND ISNULL(CategoryCode,'')!='' AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value + "%'";
            cbDesignCategory.DataBind();
            // ** ** ** ... //

            // ** Design Sub Category Code Lookup Filter ** //
            DesignSubCategoryLookup.SelectCommand = "SELECT SubCategoryCode, Description FROM Masterfile.DesignSubCategory WHERE ISNULL(IsInactive,0)=0 AND ISNULL(SubCategoryCode,'')!='' AND ISNULL(ProductCategoryCode,'') like '%" + cbProductCategory.Value + "%' AND ISNULL(ProductGroup,'')  like '%" + cbProductGroup.Value + "%'";
            cbDesignSubCategory.DataBind();
            // ** ** ** ... //

            // ** Filter Fit Code Lookup Filter** //
            FitCodeLookup.SelectCommand = "SELECT FitCode, FitName FROM Masterfile.Fit WHERE ISNULL(IsInactive,0)=0 AND GenderCode = '" + cbGender.Value + "' AND ProductCategory = '" + cbProductCategory.Value + "' ";
            glFitCode.DataBind();
            // ** ** ** ... //

            // ** Fabric Code Lookup Filter ** //
            FabricCodeLookup.SelectCommand = "SELECT FabricCode, B.FullDesc AS FabricDescription FROM Masterfile.Fabric A INNER JOIN Masterfile.Item B ON A.FabricCode = B.ItemCode WHERE FabricGroup = '" + cbProductGroup.Value + "' AND FabricSupplier = '" + glFabricSupplier.Text + "' ";
            glFabricCode.DataBind();
            // ** ** ** ... //

            // ** Fabric Color Lookup Filter ** //
            FabricColorLookup.SelectCommand = "SELECT DISTINCT ColorCode, ItemCode AS FabricCode FROM Masterfile.ItemDetail WHERE ISNULL(IsInactive,0)=0 AND ItemCode = '" + glFabricCode.Text + "'";
            glFabricColor.DataBind();
            // ** ** ** ... //
             
            //// ** Product Group Lookup Filter ** //
            //FabricColorLookup.SelectCommand = "SELECT ProductGroupCode, Description FROM Masterfile.ProductGroup WHERE ISNULL(IsInactive,0)=0 AND ISNULL(ProductGroupCode,'')!='' AND ProductCategoryCode = '" + cbProductCategory.Value + "'";
            //glFabricColor.DataBind();
            //// ** ** ** ... //
             
            ProductGroupLookup.SelectCommand = "SELECT ProductGroupCode, Description FROM Masterfile.ProductGroup WHERE ISNULL(IsInactive,0)=0 AND ISNULL(ProductGroupCode,'')!='' AND REPLACE (ProductCategoryCode, ' ', '' ) like '%," + cbProductCategory.Value + ",%'";
            cbProductGroup.DataBind();
        }
        protected void SetSizeDetail2()
        {
            SizeDetail2DataSource.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY SizeCode) AS VARCHAR(5)),5) AS LineNumber "
                                                            + ",SizeCode, SizeName, Length AS InseamLength, SortNumber FROM Masterfile.FitSizeDetail"
                                                            + " WHERE FitCode = '" + glFitCode.Text + "' ";
            gvSizeDetail2.DataSource = SizeDetail2DataSource;
            gvSizeDetail2.DataBind();

        }
        protected void ViewPISImages()
        {
            if (!string.IsNullOrEmpty(txtFrontImage64string.Text))
                dxFrontImage.ImageUrl = "data:image/jpg;base64," + txtFrontImage64string.Text;
            dxFrontImage.LargeImageUrl = "data:image/jpg;base64," + txtFrontImage64string.Text;

            if (!string.IsNullOrEmpty(txtBackImage64string.Text))
                dxBackImage.ImageUrl = "data:image/jpg;base64," + txtBackImage64string.Text;
            dxBackImage.LargeImageUrl = "data:image/jpg;base64," + txtBackImage64string.Text;

            if (!string.IsNullOrEmpty(txt2DFrontImage64string.Text))
                dxFrontImage2D.ImageUrl = "data:image/jpg;base64," + txt2DFrontImage64string.Text;
            dxFrontImage2D.LargeImageUrl = "data:image/jpg;base64," + txt2DFrontImage64string.Text;


            if (!string.IsNullOrEmpty(txt2DBackImage64string.Text))
                dxBackImage2D.ImageUrl = "data:image/jpg;base64," + txt2DBackImage64string.Text;
            dxBackImage2D.LargeImageUrl = "data:image/jpg;base64," + txt2DBackImage64string.Text;


        }
        #endregion





        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            string supplier = e.Parameters.Split('|')[1];//Set column name
            string productcategoru = e.Parameters.Split('|')[2];//Set Item Code

            if (e.Parameters.Contains("ItemCodeStyle"))
            {
                //ItemCodeLookup.SelectCommand = "SELECT DISTINCT I.[ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] I LEFT JOIN Masterfile.ItemSupplierPrice ISP ON I.ItemCode = ISP.ItemCode WHERE I.ProductCategoryCode = '" + productcategoru + "' AND ISNULL(ISP.Supplier,'')='" + supplier + "'";
                ItemCodeLookup.SelectCommand = "SELECT DISTINCT I.[ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] I WHERE I.ProductCategoryCode = '" + productcategoru + "' AND ISNULL(I.KeySupplier,'')='" + supplier + "'";
            }

            Session["FilterItemCodeStyle"] = ItemCodeLookup;
            grid.DataSource = ItemCodeLookup;
            grid.DataBind();

        }
        public void gridView_CustomCallbackItemStyle(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";

            }
            else if (e.Parameters.Contains("ClassCode"))
            {
                Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS ColorCode, ClassCode, '' AS SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";

            }
            else if (e.Parameters.Contains("SizeCode"))
            {
                Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";

            }
            else if (e.Parameters.Contains("UnitBase"))
            {
                Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, UnitBase FROM Masterfile.Item  WHERE ItemCode = '" + itemcode + "'";

            }

            ASPxGridView grid = sender as ASPxGridView;
            ASPxGridLookup lookup = (ASPxGridLookup)gvStyleDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvStyleDetail.Columns[8], "glItemCode");
            var selectedValues = itemcode;
            CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
            Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["FilterExpressionPISItemCodeStyle"] = Masterfileitemdetail.FilterExpression;
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
        public void ProductCategory_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;//Traps the callback
            ASPxGridView productcategory = sender as ASPxGridView;
            string productcategorycode = e.Parameters.Split('|')[1];//Set Item Code

            if (e.Parameters.Contains("ProductCategoryCodeDropDown"))
            {//RA
                ProductCategoryLookup.SelectCommand = "SELECT LTRIM(RTRIM(ProductCategoryCode)) AS ProductCategoryCode, Description FROM Masterfile.ProductCategory WHERE ISNULL(IsInactive,0)=0 AND ISNULL(ProductCategoryCode,'')!='' AND ItemCategoryCode = '" + productcategorycode + "' ";

                Session["FilterProductCategoryCodeStyle"] = ProductCategoryLookup;
                productcategory.DataSource = ProductCategoryLookup;
                productcategory.DataBind();
            }
        }

        private DataTable GETFit()
        {

            gvThreadDetail.DataSourceID = null;
            //gv1.DataBind();

            DataTable dt = new DataTable();


            ThreadDetailDS.SelectCommand = "SELECT PISTD.*, ISNULL(C.R,'') AS R,ISNULL(C.G,'') AS G,ISNULL(C.B,'') AS B FROM MasterFile.PISThreadDetail PISTD LEFT JOIN Masterfile.Color C ON PISTD.ThreadColor = C.ColorCode WHERE PISNumber='" + Request.QueryString["parameters"].ToString().Split('|')[1] + "' order by LineNumber";
            gvThreadDetail.DataSource = ThreadDetailDS;
            if (Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                gvThreadDetail.DataBind();


            Session["ThreadFit"] = "1";

            foreach (GridViewColumn col in gvThreadDetail.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvThreadDetail.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvThreadDetail.GetRowValues(i, col.ColumnName);
            }
            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["Stitch"],dt.Columns["ThreadColor"],dt.Columns["Ticket"]}; 
          
            return dt;
        }

        protected void gvThreadDetail_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;

            }

            if (Session["ThreadFit"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GETFit();

                int Line = source.Rows.Count + 1;

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys["Stitch"], values.Keys["ThreadColor"], values.Keys["Ticket"] };
                    DataRow row = source.Rows.Find(keys);

                    //row["Stitch"] = values.NewValues["Stitch"];
                    //row["ThreadColor"] = values.NewValues["ThreadColor"];
                    //row["Ticket"] = values.NewValues["Ticket"];
                    row["R"] = values.NewValues["R"];
                    row["G"] = values.NewValues["G"];
                    row["B"] = values.NewValues["B"];
                    row["Location"] = values.NewValues["Location"];

                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["Stitch"], values.Keys["ThreadColor"], values.Keys["Ticket"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                //Gears.RetriveData2("DELETE FROM Masterfile.PISThreadDetail WHERE  PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());


                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    //var PISNumber = values.NewValues["PISNumber"];
                    //var LineNumber = values.NewValues["LineNumber"] == null ? Convert.ToString(Line).PadLeft(Convert.ToString(Line).Length + (5 - Convert.ToString(Line).Length), '0') : values.NewValues["LineNumber"];
                    //var LineNumber = values.NewValues["LineNumber"];

                    var Stitch = values.NewValues["Stitch"];

                    var ThreadColor = values.NewValues["ThreadColor"];
                    var Ticket = values.NewValues["Ticket"];
                    var R = values.NewValues["R"];
                    var G = values.NewValues["G"];
                    var B = values.NewValues["B"];
                    var Location = values.NewValues["Location"];


                    source.Rows.Add(Stitch, ThreadColor, Ticket, R, G,B, Location);

                    Line = Line + 1;
                }

                foreach (DataRow dtRow in source.Rows)
                {
                 
                    _EntityThreadDetail.Stitch = dtRow["Stitch"].ToString();
                    _EntityThreadDetail.ThreadColor = dtRow["ThreadColor"].ToString();
                    _EntityThreadDetail.Ticket = dtRow["Ticket"].ToString();
                    _EntityThreadDetail.R = dtRow["R"].ToString();
                    _EntityThreadDetail.G = dtRow["G"].ToString();
                    _EntityThreadDetail.B = dtRow["B"].ToString();
                    _EntityThreadDetail.Location = dtRow["Location"].ToString();
                    _EntityThreadDetail.AddPISThreadDetail(_EntityThreadDetail);
                }
            }
        }
        private DataTable GETEmbrioder()
        {

            gvEmbroiderDetail.DataSourceID = null;
            //gv1.DataBind();

            DataTable dt = new DataTable();


            EmbroiderDetailDS.SelectCommand = "SELECT A.*,'' AS UploadEmbroider, B.Description AS EmbroDescription FROM Masterfile.PISEmbroideryDetail A LEFT JOIN Masterfile.Embroidery B ON A.EmbroCode = B.EmbroideryCode WHERE PISNumber='" + Request.QueryString["parameters"].ToString().Split('|')[1] + "' order by LineNumber";
            gvEmbroiderDetail.DataSource = EmbroiderDetailDS;
            if (Request.QueryString["parameters"].ToString().Split('|')[0] != "CopyTrans")
                gvEmbroiderDetail.DataBind();


            Session["Embroider"] = "1";

            foreach (GridViewColumn col in gvEmbroiderDetail.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvEmbroiderDetail.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvEmbroiderDetail.GetRowValues(i, col.ColumnName);
            }
            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["PISNumber"],dt.Columns["LineNumber"]};

            return dt;
        }

        protected void gvEmbroiderDetail_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;

            }

            if (Session["Embroider"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GETEmbrioder();

                int Line = source.Rows.Count + 1;

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys["PISNumber"], values.Keys["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);

                    row["EmbroPart"] = values.NewValues["EmbroPart"];
                    row["EmbroCode"] = values.NewValues["EmbroCode"];
                    row["EmbroDescription"] = values.NewValues["EmbroDescription"];
                    row["Height"] = values.NewValues["Height"];
                    row["Width"] = values.NewValues["Width"];
                    row["PictureEmbroider"] = values.NewValues["PictureEmbroider"];

                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["EmbroPart"], values.Keys["EmbroCode"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                //Gears.RetriveData2("DELETE FROM Masterfile.PISEmbroideryDetail WHERE  PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());

                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var PISNumber = values.NewValues["PISNumber"];
                    var LineNumber = values.NewValues["LineNumber"] == null ? Convert.ToString(Line).PadLeft(Convert.ToString(Line).Length + (5 - Convert.ToString(Line).Length), '0') : values.NewValues["LineNumber"];
                    //var LineNumber = values.NewValues["LineNumber"];


                    var EmbroPart = values.NewValues["EmbroPart"];
                    var EmbroCode= values.NewValues["EmbroCode"];
                    var EmbroDescription  = values.NewValues["EmbroDescription"];
                    var Height  = values.NewValues["Height"];
                    var Width  = values.NewValues["Width"];
                    var PictureEmbroider  = values.NewValues["PictureEmbroider"];

                    source.Rows.Add(EmbroPart, EmbroCode, EmbroDescription, Height, Width, PictureEmbroider);

                    Line = Line + 1;
                }

                foreach (DataRow dtRow in source.Rows)
                {
                   
                    _EntityEmbroideryDetail.EmbroPart = dtRow["EmbroPart"].ToString();
                    _EntityEmbroideryDetail.EmbroCode = dtRow["EmbroCode"].ToString();
                    _EntityEmbroideryDetail.EmbroDescription = dtRow["EmbroDescription"].ToString();
                    _EntityEmbroideryDetail.Height = Convert.ToDecimal(Convert.IsDBNull(dtRow["Height"]) ? 0 : dtRow["Height"]); 
                    _EntityEmbroideryDetail.Width = Convert.ToDecimal(Convert.IsDBNull(dtRow["Width"]) ? 0 : dtRow["Width"]); 
                    _EntityEmbroideryDetail.PictureEmbroider = dtRow["PictureEmbroider"].ToString();
                    _EntityEmbroideryDetail.AddPISEmbroideryDetail(_EntityEmbroideryDetail);
                }
            }
        }

        private DataTable GetSelectedValGB()
        {
            gvGradeBracket.DataSourceID = null;
            DataTable dt = new DataTable();

            if (Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
            {
                sdsDetail3.SelectCommand = "SELECT * FROM Production.PISGradeBracket where DocNumber ='" + (Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans" ? Request.QueryString["parameters"].ToString().Split('|')[1] : txtPISNumber.Text) + "'";
            }
            else
            {
                sdsDetail3.SelectCommand = "SELECT * FROM Masterfile.FitGradeBracket where FitCode ='" + glFitCode.Text + "'";
            } 
            sdsDetail3.DataBind();

            gvGradeBracket.DataSourceID = "sdsDetail3";
            gvGradeBracket.DataBind();

            foreach (GridViewColumn col in gvGradeBracket.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                int alreadythere = 0;
                foreach (DataColumn cols in dt.Columns)
                {
                    if (dataColumn.FieldName == cols.ToString())
                        alreadythere++;
                }
                if (alreadythere == 0)
                    dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvGradeBracket.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvGradeBracket.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { dt.Columns["POMCode"], dt.Columns["Size"]  };
            return dt;
        }
        private DataTable GETPrint()
        {

            gvPrintDetail.DataSourceID = null;
          

            DataTable dt = new DataTable();


            PrintDetailDS.SelectCommand = "SELECT A.*,'' AS UploadPrint, B.Description AS PrintDescription,C.Description AS InkDescription FROM Masterfile.PISPrintDetail A LEFT JOIN Masterfile.PrintProcess B ON A.PrintCode = B.ProcessCode LEFT JOIN Masterfile.PrintInk C ON A.PrintInk = C.InkCode WHERE PISNumber='" + Request.QueryString["parameters"].ToString().Split('|')[1] + "' order by LineNumber";
            gvPrintDetail.DataSource = PrintDetailDS;

            if (Request.QueryString["parameters"].ToString().Split('|')[0] != "CopyTrans")
                gvPrintDetail.DataBind();


            Session["PrintDetail"] = "1";

            foreach (GridViewColumn col in gvPrintDetail.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvPrintDetail.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvPrintDetail.GetRowValues(i, col.ColumnName);
            }
            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["PISNumber"],dt.Columns["LineNumber"]};

            return dt;
        }

        protected void gvPrintDetail_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;

            }

            if (Session["PrintDetail"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GETPrint();

                int Line = source.Rows.Count + 1;

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys["PISNumber"], values.Keys["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);

                    row["PrintPart"] = values.NewValues["PrintPart"];
                    row["PrintCode"] = values.NewValues["PrintCode"];
                    row["PrintDescription"] = values.NewValues["PrintDescription"];
                    row["InkDescription"] = values.NewValues["InkDescription"];
                    row["PicturePrint"] = values.NewValues["PicturePrint"];
                 
                }

                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var PISNumber = values.NewValues["PISNumber"];
                    var LineNumber = values.NewValues["LineNumber"] == null ? Convert.ToString(Line).PadLeft(Convert.ToString(Line).Length + (5 - Convert.ToString(Line).Length), '0') : values.NewValues["LineNumber"];
                    //var LineNumber = values.NewValues["LineNumber"];

                    var PrintPart = values.NewValues["PrintPart"];
                    var PrintCode = values.NewValues["PrintCode"];
                    var PrintDescription = values.NewValues["PrintDescription"];
                    var InkDescription = values.NewValues["InkDescription"];
                    var PicturePrint = values.NewValues["PicturePrint"];




                    source.Rows.Add(PrintPart, PrintCode, PrintDescription, InkDescription, PicturePrint);

                    Line = Line + 1;
                }

                //test dito
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["PrintPart"], values.Keys["PrintCode"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (DataRow dtRow in source.Rows)
                {

                 
                    _EntityPrintDetail.PrintPart = dtRow["PrintPart"].ToString();
                    _EntityPrintDetail.PrintCode = dtRow["PrintCode"].ToString();
                    _EntityPrintDetail.PrintDescription = dtRow["PrintDescription"].ToString();
                    _EntityPrintDetail.InkDescription = dtRow["InkDescription"].ToString();
                    _EntityPrintDetail.PicturePrint = dtRow["PicturePrint"].ToString();
                    _EntityPrintDetail.AddPISPrintDetail(_EntityPrintDetail);
                }
            }
        }


        private DataTable GETOtherPictures()
        {

            gvOtherPictures.DataSourceID = null;


            DataTable dt = new DataTable();


            OtherPictureDetailDS.SelectCommand = "SELECT *,'' AS OtherPictureUpload FROM Masterfile.PISImage WHERE PISNumber='" + Request.QueryString["parameters"].ToString().Split('|')[1] + "' order by LineNumber";
            gvOtherPictures.DataSource = OtherPictureDetailDS;
            gvOtherPictures.DataBind();


            Session["OtherPictures"] = "1";

            foreach (GridViewColumn col in gvOtherPictures.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvOtherPictures.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvOtherPictures.GetRowValues(i, col.ColumnName);
            }
            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["PISNumber"],dt.Columns["LineNumber"]};

            return dt;
        }


        protected void gv1GB_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.UpdateValues.Clear();
            }

            if (Session["DatatableGB"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedValGB();

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

                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    var LineNumber = values.NewValues["LineNumber"];
                    var POMCode = values.NewValues["POMCode"];
                    var Size = values.NewValues["Size"];
                    var Grade = values.NewValues["Grade"];
                    var Bracket = values.NewValues["Bracket"];
                    var Tolerance = values.NewValues["Tolerance"];

                    source.Rows.Add(LineNumber, POMCode, Size, Grade, Bracket, Tolerance);
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["POMCode"], values.NewValues["Size"] };
                    DataRow row = source.Rows.Find(keys);
                    //row["POMCode"] = values.NewValues["POMCode"];
                    //row["Size"] = values.NewValues["Size"];
                    row["Grade"] = values.NewValues["Grade"];
                    row["Bracket"] = values.NewValues["Bracket"];
                    row["Tolerance"] = values.NewValues["Tolerance"];
                }
                Gears.RetriveData2("DELETE FROM Production.PISGradeBracket where DocNumber='" + txtPISNumber.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dtRow in source.Rows)
                {
                    _EntityDetail2.POMCode = dtRow["POMCode"].ToString();
                    _EntityDetail2.Size = dtRow["Size"].ToString();
                    _EntityDetail2.Grade = dtRow["Grade"].ToString();
                    _EntityDetail2.Bracket = Convert.ToInt32(Convert.IsDBNull(dtRow["Bracket"]) ? 0 : dtRow["Bracket"]);
                    _EntityDetail2.Tolerance = dtRow["Tolerance"].ToString();
                    _EntityDetail2.AddFitGradeBracket(_EntityDetail2);
                }
            }
            Session["DatatableGB"] = "0";
        } 

        protected void gvOtherPictures_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;

            }

            if (Session["OtherPictures"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GETOtherPictures();

                int Line = source.Rows.Count + 1;

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys["PISNumber"], values.Keys["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);

                    row["ImageFileName"] = values.NewValues["ImageFileName"];
                    row["OtherPicture"] = values.NewValues["OtherPicture"];

                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["PISNumber"], values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var PISNumber = values.NewValues["PISNumber"];
                    var LineNumber = values.NewValues["LineNumber"] == null ? Convert.ToString(Line).PadLeft(Convert.ToString(Line).Length + (5 - Convert.ToString(Line).Length), '0') : values.NewValues["LineNumber"];
                    //var LineNumber = values.NewValues["LineNumber"];

                    var ImageFileName = values.NewValues["ImageFileName"];
                    var OtherPicture = values.NewValues["OtherPicture"];




                    source.Rows.Add(PISNumber, LineNumber, ImageFileName, OtherPicture);

                    Line = Line + 1;
                }

                foreach (DataRow dtRow in source.Rows)
                {


                    _EntityOtherPictureDetail.ImageFileName = dtRow["ImageFileName"].ToString();
                    _EntityOtherPictureDetail.OtherPicture = dtRow["OtherPicture"].ToString();

                    _EntityOtherPictureDetail.AddPISOtherPictureDetail(_EntityOtherPictureDetail);
                }
            }
        }
    }
}