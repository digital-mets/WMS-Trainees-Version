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
    public partial class frmItemMasterfile : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string imagepath = "";

        string itemCodeTemp;

        Entity.ItemMasterfile _Entity = new ItemMasterfile();
        Entity.ItemMasterfile.ItemStock _Stock = new ItemMasterfile.ItemStock();
        Entity.ItemMasterfile.ItemCustomerPriceDetail _ItemCustomerPrice = new ItemMasterfile.ItemCustomerPriceDetail();

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            // 04 - 06 - 2016   LGE Add Filter when form is being use as Asset Masterfile
            if (Request.QueryString["transtype"].ToString() == "REFASS")
            {
                FormLabel.Text = "Asset Masterfile";
                ItemCategory.SelectCommand = "select ItemCategoryCode,Description from Masterfile.ItemCategory WHERE ISNULL(IsInactive,0) =0 and isnull(IsAsset,0)=1 ORDER BY CONVERT(int, ItemCategoryCode) ASC";
            }

            ProdCat.SelectCommand = "select ProductCategoryCode,Description from  Masterfile.ProductCategory where " +
                                    "ISNULL(IsInactive,0)=0 and ItemCategoryCode = '" + txtitemcat.Text + "'";
            ProdSubCat.SelectCommand = "select ProductSubCatCode,Description from  Masterfile.ProductCategorySub where " +
                                       "ISNULL(IsInactive,0)=0 and ProductCategoryCode like '%" + txtprodcategory.Text.Trim() + "%'";

            Request.QueryString["transtype"].ToString();


            string referer;
            try //Validation to restrict user to browse/type directly to browser's address
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

            //gv1.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode";
            if (Request.QueryString["parameters"].ToString() == "2  " || Request.QueryString["parameters"].ToString() == "2")
            {
                txtItemCode.ReadOnly = true;
                frmlayout1.FindItemOrGroupByName("FabricInfo").ClientVisible = true;
                TabbedLayoutGroup group = (TabbedLayoutGroup)frmlayout1.Items[0];
                group.PageControl.ActiveTabIndex = 1;
                FormLabel.Text = "Fabric";
            }
            else if (Request.QueryString["parameters"].ToString() == "1" || Request.QueryString["parameters"].ToString() == "1  ")
            {
                txtItemCode.ReadOnly = true;
                frmlayout1.FindItemOrGroupByName("Stock Master Info").ClientVisible = true;
                frmlayout1.FindItemOrGroupByName("WMSInfo").ClientVisible = true;
                TabbedLayoutGroup group = (TabbedLayoutGroup)frmlayout1.Items[0];
                group.PageControl.ActiveTabIndex = 1;
                FormLabel.Text = "Stock Masterfile";

                txtitemcat.Text = "1  ";//seph
                aglTaxCode.Text = "VAT12";
            }


            if (!IsPostBack)
            {
                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                if (Request.QueryString["entry"].ToString() == "V")
                {
                    view = true;//sets view mode for entry
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        txtItemCode.ReadOnly = true;
                        updateBtn.Text = "Update";
                        glcheck.ClientVisible = true;
                        txtitemcat.ReadOnly = true;
                        txtprodcategory.ReadOnly = true;
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                   



                    gv1.DataSourceID = "sdsDetail";
                    agvItemSupplier.DataSourceID = "sdsItemSupp";
                    popup.ShowOnPageLoad = false;
                    if (Request.QueryString["docnumber"].ToString()!="undefined")
                    {
                        txtItemCode.ReadOnly = true;
                        txtItemCode.Value = Request.QueryString["docnumber"].ToString();
                    }
                    if (Request.QueryString["itemcat"] != null)
                    {
                        // seph txtitemcat.Value = Request.QueryString["itemcat"].ToString();
                        if (Request.QueryString["parameters"].ToString() == "2" || Request.QueryString["parameters"].ToString() == "2  ")
                        {

                            txtitemcat.Value = Request.QueryString["itemcat"].ToString();

                        }
                    }
                    if (Request.QueryString["pis"] != null)
                    {
                        DataTable GetPIS = Gears.RetriveData2("select pisdescription,productsubclass from production.productinfosheet where pisnumber='" + Request.QueryString["pis"].ToString() + "'", Session["ConnString"].ToString());
                       string subclass="";
                        foreach (DataRow dtrow in GetPIS.Rows)
                        {
                            
                            txtitemdesc.Text = dtrow[0].ToString();
                            txtshortdesc.Text = dtrow[0].ToString();

                            if (dtrow[1].ToString() == "I") { subclass = "IMAGE"; }
                            if (dtrow[1].ToString() == "C") { subclass = "CORE"; }
                            if (dtrow[1].ToString() == "S") { subclass = "SEASON"; }
                            if (dtrow[1].ToString() == "R") { subclass = "REPEAT"; }
                            cbProdSubClass.Text = subclass;
                        }

                        
                    }


                    //txtitemcat.Value = "2  ";
                    if (Request.QueryString["prodcat"] != null)
                    {
                        ProdCat.SelectCommand = "select ProductCategoryCode,Description from  Masterfile.ProductCategory where " +
                                    "ISNULL(IsInactive,0)=0 and ItemCategoryCode like '%" + txtitemcat.Text.Trim() + "%'";
                        txtprodcategory.DataBind();
                        txtprodcategory.Value = Request.QueryString["prodcat"].ToString();
                    }
                    if (Request.QueryString["prodsub"] != null)
                    {
                        txtprodsubcat.Value = Request.QueryString["prodsub"].ToString();
                    }
                    if (Request.QueryString["sup"] != null)
                    {
                        txtitemsupplier.Value = Request.QueryString["sup"].ToString();
                    }

                    //------- STOCK MASTER
                    if (Request.QueryString["conf"] != null && Request.QueryString["conf"] == "1"
                        && (Request.QueryString["parameters"].ToString() == "1" || Request.QueryString["parameters"].ToString() == "1  "))
                    {
                        DataTable GetBrand = Gears.RetriveData2("select BrandName From masterfile.Brand where BrandCode = '" + Request.QueryString["brand"].ToString() + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in GetBrand.Rows)
                        {
                            txtBrand.Text = dtrow[0].ToString();
                        }
                        DataTable GetProdGroup = Gears.RetriveData2("select Description From masterfile.ProductGroup where ProductGroupCode = '" + Request.QueryString["prdgrp"].ToString() + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in GetProdGroup.Rows)
                        {
                            txtProductGroup.Text = dtrow[0].ToString();
                        }
                        
                        txtDeliveryYear.Text = Request.QueryString["deldate"].ToString() == "x" ? null : Request.QueryString["deldate"].ToString().Split('-')[0];
                        cboDeliveryMonth.Value = Request.QueryString["deldate"].ToString() == "x" ? null : Request.QueryString["deldate"].ToString().Split('-')[1];
                        //txtCollAbbreviation.Text = Request.QueryString["coll"].ToString();
                        txtPISNumber.Text = Request.QueryString["pis"].ToString();
                        DataTable getPisimage = Gears.RetriveData2("Select FrontImage,BackImage from production.ProductInfoSheet where PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in getPisimage.Rows)
                        {
                            if (!string.IsNullOrEmpty(dtrow[0].ToString()))
                                FrontImage.ImageUrl = "data:image/jpg;base64," + dtrow[0].ToString();
                                ItemImage.ImageUrl = "data:image/jpg;base64," + dtrow[0].ToString();
                            if (!string.IsNullOrEmpty(dtrow[1].ToString()))
                                BackImage.ImageUrl = "data:image/jpg;base64," + dtrow[1].ToString();
                        }

                        DataTable getFitName = Gears.RetriveData2("Select isnull(FitName,'') FitName from masterfile.Fit where FitCode = '" + Request.QueryString["fit"].ToString() + "'", Session["ConnString"].ToString());
                        foreach(DataRow dtrow in getFitName.Rows){
                            txtFitCode.Text = Request.QueryString["fit"].ToString() + " [" + dtrow[0].ToString() + "]";
                        }
                        //txtColor.Text = Request.QueryString["color"].ToString();
                        DataTable getDesName = Gears.RetriveData2("Select isnull(Description,'') Description from masterfile.DesignCategory where CategoryCode = '" + Request.QueryString["descat"].ToString() + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in getDesName.Rows)
                        {
                            txtProdDesignCat.Text = Request.QueryString["descat"].ToString() + " [" + dtrow[0].ToString() + "]";
                        }
                        //txtProdDesignCat.Text = Request.QueryString["descat"].ToString();
                        //txtColorName.Text = Request.QueryString["colname"].ToString();
                        //txtRetailFabric.Text = Request.QueryString["retfabric"].ToString();
                        txtWash.Text = Request.QueryString["wash"].ToString();
                        txtTint.Text = Request.QueryString["tint"].ToString();
                        txtGender.Text = Request.QueryString["gender"].ToString();

                        //string sql = "select FabricColor ColorCode,SizeCode from Masterfile.FitSizeDetail a " +
                        //            "cross join Production.ProductInfoSheet b " +
                        //            "where a.FitCode = '" + txtFitCode.Text + "' and PISNUmber = '" + txtPISNumber.Text + "'";
                        string sb = "select FabricColor ColorCode,SizeCode, b.frontimage ItemImage  from Masterfile.FitSizeDetail a " +
                                    "cross join Production.ProductInfoSheet b " +
                                    "where a.FitCode = '" + Request.QueryString["fit"].ToString() + "' and PISNUmber = '" + txtPISNumber.Text + "'";

                        DataTable SizeTable = Gears.RetriveData2(sb,Session["ConnString"].ToString());
                        if (SizeTable.Rows.Count > 0)
                        {
                            gvExtract.DataSource = SizeTable;
                            gvExtract.DataBind();
                            gvExtract.Selection.SelectAll();
                        }
                        txtImportedItem.Text = Request.QueryString["impitem"].ToString();

                        gv1.DataSourceID = "odsDetail";
                    }
                }
                else
                {
                    txtItemCode.ReadOnly = true;
                    txtItemCode.Value = Request.QueryString["docnumber"].ToString();
                    agvRunningInv.DataSourceID = "odsWHDetail";
                    agvItemCustomer.DataSourceID = "odsCusDetail";
                    agvItemSupplier.DataSourceID = "odsSuppDetail";
                    gv1.DataSourceID = "odsDetail";


                    _Entity.getdata(txtItemCode.Text, Session["ConnString"].ToString());
                    txtitemdesc.Text = _Entity.FullDesc;
                    txtshortdesc.Text = _Entity.ShortDesc;
                    txtitemcat.Value = _Entity.ItemcategoryCode;
                    Session["txtitemcat"] = _Entity.ItemcategoryCode;//joseph -- for retaining of txtitemcate after update
                    // 04 - 06 - 2016   LGE Add Filter when form is being use as Asset Masterfile
                    if (Request.QueryString["transtype"].ToString() == "REFASS")
                    {
                        ItemCategory.SelectCommand = "select ItemCategoryCode,Description from Masterfile.ItemCategory WHERE ISNULL(IsInactive,0) =0 and isnull(IsAsset,0)=1 ORDER BY CONVERT(int, ItemCategoryCode) ASC";
                    }
                    ProdCat.SelectCommand = "select ProductCategoryCode,Description from  Masterfile.ProductCategory where " +
                                    "ISNULL(IsInactive,0)=0 and ItemCategoryCode like '%" + txtitemcat.Text.Trim() + "%'";
                    txtprodcategory.DataBind();
                    txtprodcategory.Value = _Entity.ProductCategoryCode;
                    ProdSubCat.SelectCommand = "select ProductSubCatCode,Description from  Masterfile.ProductCategorySub where " +
                                               "ISNULL(IsInactive,0)=0 and ProductCategoryCode like '%" + txtprodcategory.Text.Trim() + "%'";
                    ProdSubCat.DataBind();
                    txtprodsubcat.DataBind();
                    txtprodsubcat.Value = _Entity.ProductSubCatCode;
                    txtitemcustomer.Value = _Entity.Customer;
                    txtitemsupplier.Value = _Entity.KeySupplier;
                    txtreorderlevel.Text = _Entity.ReorderLevel.ToString();
                    txtmaxlevel.Text = _Entity.MaxLevel.ToString();
                    txtstandardqty.Text = _Entity.StandardQTY.ToString();
                    txtbaseunit.Value = _Entity.UnitBase;
                    txtunitbulk.Text = _Entity.UnitBulk;//here
                    txtstoragetype.Value = _Entity.StorageType;
                    txtstrategy.Text = _Entity.PickingStrategy;
                    txtminorder.Text = _Entity.MinQTY.ToString();
                    txtestcost.Text = _Entity.EstimatedCost.ToString();
                    chkIsBulk.Value = _Entity.IsByBulk;
                    chkIsCore.Value = _Entity.IsCore;
                    chkIsInactive.Value = _Entity.IsInactive;
                    chkIsPWD.Value = _Entity.IsPWD;
                    chkIsBNPC.Value = _Entity.IsBNPC;
                    chkIsSenior.Value = _Entity.IsSenior;
                    cboxItemType.Value = _Entity.ItemType;
                    aglTaxCode.Text = _Entity.TaxCode;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;
                    txth10.Text = _Entity.Field10;


                    txtActivatedDate.Text = String.IsNullOrEmpty(_Entity.ActivatedDate) ? "" : Convert.ToDateTime(_Entity.ActivatedDate).ToShortDateString();
                    txtAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? "" : Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                    txtLastEditedDate.Text = String.IsNullOrEmpty(_Entity.LastEditedDate) ? "" : Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                    txtDeactivatedDate.Text = String.IsNullOrEmpty(_Entity.DeActivatedDate) ? "" : Convert.ToDateTime(_Entity.DeActivatedDate).ToShortDateString();
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtDeactivatedBy.Text = _Entity.DeActivatedBy;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;

                    txtFabGroup.Value = _Entity.FabricGroup;
                    if (txtFabGroup.Value != null)
                    {
                        if (!string.IsNullOrEmpty(txtFabGroup.Value.ToString()) && txtFabGroup.Value.ToString() == "D")
                        {
                            Dye.SelectCommand = "SELECT DyeingCode,Description FROM MasterFile.Dyeing WHERE ProductGroup like '%D%'";
                            Weave.SelectCommand = "SELECT Code,Description FROM IT.GenericLookup WHERE LookUpKey = 'WEAVE'";
                        }
                        else
                        {

                            Dye.SelectCommand = "SELECT DyeingCode,Description FROM MasterFile.Dyeing WHERE ProductGroup like '%K%'";
                            Weave.SelectCommand = "SELECT Code,Description FROM IT.GenericLookup WHERE LookUpKey = 'KNTYP'";
                        }
                        if (!string.IsNullOrEmpty(txtFabGroup.Value.ToString()) && txtFabGroup.Value.ToString() == "K")
                        {
                            frmlayout1.FindItemOrGroupByName("WeaveType").Caption = "Knit Type";
                        }
                    }
                    if (txtFabGroup.Text == "K")
                    {
                        cbforknits.ClientEnabled = true;
                        txtYield.ClientEnabled = true;
                    }
                    else
                    {
                        cbforknits.ClientEnabled = false;
                        txtYield.ClientEnabled = false;
                    }
                    
                    //------- FABRIC
                    Dye.DataBind();
                    Weave.DataBind();
                    txtRetailFabCode.Text = _Entity.SupplierFabricCode;
                    txtFabDesCat.Value = _Entity.FabricDesignCategory;
                    txtDye.Value = _Entity.Dyeing;
                    txtWeave.Value = _Entity.WeaveType;
                    txtFinishing.Value = _Entity.Finishing;
                    txtCuttableWidth.Text = _Entity.CuttableWidth;
                    txtGrossWidth.Text = _Entity.GrossWidth;
                    txtCuttableWeightBW.Text = _Entity.CuttableWeightBW;
                    txtGrossWeightBW.Text = _Entity.GrossWeightBW;
                    txtFabricStretch.Text = _Entity.FabricStretch;
                    txtWarpConstruction.Text = _Entity.WarpConstruction;
                    txtWeftConstruction.Text = _Entity.WeftConstruction;
                    txtWarpDensity.Text = _Entity.WarpDensity;
                    txtWeftDensity.Text = _Entity.WeftDensity;
                    txtWarpShrinkage.Text = _Entity.WarpShrinkage;
                    txtWeftShrinkage.Text = _Entity.WeftShrinkage;
                    cbforknits.Value = _Entity.ForKnitsOnly;
                    txtYield.Text = _Entity.Yield;

                    //--- STOCK MASTER
                    DataTable checkItemCategoryCode = Gears.RetriveData2("SELECT * FROM Masterfile.Item Where ItemCategoryCode = '1' and Itemcode='" + txtItemCode.Text + "'", Session["ConnString"].ToString());//ADD CONN

                    foreach (DataRow dt in checkItemCategoryCode.Rows)
                    {
                        itemCodeTemp = dt["ItemCategoryCode"].ToString();
                    }

                    if (itemCodeTemp != null)
                    {
                        _Stock.getdata(txtItemCode.Text, Session["ConnString"].ToString());
                        DataTable GetBrand = Gears.RetriveData2("select BrandName From masterfile.Brand where BrandCode = '" + _Stock.BrandCode + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in GetBrand.Rows)
                        {
                            txtBrand.Text = dtrow[0].ToString();
                        }
                        DataTable GetProdGroup = Gears.RetriveData2("select Description From masterfile.ProductGroup where ProductGroupCode = '" + _Stock.ProductGroup + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in GetProdGroup.Rows)
                        {
                            txtProductGroup.Text = dtrow[0].ToString();
                        }
                        txtDeliveryYear.Text = _Stock.DeliveryYear;
                        cboDeliveryMonth.Value = _Stock.DeliveryMonth;
                        //txtCollAbbreviation.Text = _Stock.CollectionAbbreviation;
                        txtPISNumber.Text = _Stock.PISNumber;
                        DataTable getPisimage = Gears.RetriveData2("Select FrontImage,BackImage from production.ProductInfoSheet where PISNumber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in getPisimage.Rows)
                        {
                            if (!string.IsNullOrEmpty(dtrow[0].ToString()))
                                FrontImage.ImageUrl = "data:image/jpg;base64," + dtrow[0].ToString();
                            if (!string.IsNullOrEmpty(dtrow[1].ToString()))
                                BackImage.ImageUrl = "data:image/jpg;base64," + dtrow[1].ToString();
                        }
                        DataTable getFitName = Gears.RetriveData2("Select isnull(FitName,'') FitName from masterfile.Fit where FitCode = '" + _Stock.FitCode + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in getFitName.Rows)
                        {
                            txtFitCode.Text = _Stock.FitCode + " [" + dtrow[0].ToString() + "]";
                        }
                        //txtColor.Text = _Stock.ColorCode;
                        txtProdDesignCat.Text = _Stock.ProductDesignCategory;
                        //txtColorName.Text = _Stock.ColorName;
                        //txtRetailFabric.Text = _Stock.RetailFabricCode;
                        txtWash.Text = _Stock.WashCode;
                        txtTint.Text = _Stock.TintCode;
                        cbProdClass.Value = _Stock.ProductClass;
                        txtImportedItem.Text = _Stock.ImportedItem;

                        if (_Stock.ProductSubClass == null)
                        { 
                        
                        }
                        else
                        { //qwe
                        if (_Stock.ProductSubClass.ToString() == "I") { cbProdSubClass.Text = "IMAGE"; }
                        if (_Stock.ProductSubClass.ToString() == "C") { cbProdSubClass.Text = "CORE"; }
                        if (_Stock.ProductSubClass.ToString() == "S") { cbProdSubClass.Text = "SEASON"; }
                        if (_Stock.ProductSubClass.ToString() == "R") { cbProdSubClass.Text = "REPEAT"; }
                        // cbProdSubClass.Text = "IMAGE";
                        // cbProdSubClass.Value = _Stock.ProductSubClass;
                        cbSeason.Value = _Stock.Season;
                        cbProdAlign.Value = _Stock.ProductAlignment;
                        cbRecon.Value = _Stock.RecoAlloc;
                        txtSRP.Text = _Stock.SRP.ToString();
                        txtGender.Text = _Stock.GenderCode;
                        }
                    }
                    ViewReportSummary();
                }
                DataTable checkCount = Gears.RetriveData2("Select fabriccode from MasterFile.FabricCompositionDetail where fabriccode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    gvFab.DataSourceID = "odsFab";
                }
                else
                {
                    gvFab.DataSourceID = "sdsFabricComp";
                }

                checkCount = Gears.RetriveData2("select ItemCode from Masterfile.ItemCustomerPrice where ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    agvItemCustomer.DataSourceID = "odsCusDetail";
                }
                else
                {
                    agvItemCustomer.DataSourceID = "sdsItemSupp";
                }

                checkCount = Gears.RetriveData2("Select StockNumber from Retail.StockMasterSellThru where StockNumber = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                gvSellthru.DataSourceID = checkCount.Rows.Count > 0 ? "odsstockPrice" : "sdsstockPrice";

                DataTable stockPrice = Gears.RetriveData2("Select * from Retail.StockMasterPriceHistory where StockNumber = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                gvStockPriceHistory.DataSource = stockPrice;
                gvStockPriceHistory.DataBind();
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
           

            //DataTable dt = new DataTable();
            //foreach (GridViewColumn col in gv1.VisibleColumns)
            //{
            //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    dt.Columns.Add(dataColumn.FieldName);
            //}
            //if (dt.Rows.Count<1) {
            
            //};
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
           // gparam._DocNo = _Entity.ItemCode;
           // gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = Request.QueryString["transtype"].ToString();
           // //gparam._Factor = 1;
           // // gparam._Action = "Validate";
           // //here
           // string strresult = GWarehouseManagement.OCN_Validate(gparam);
          //  cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = view;
        }
        protected void Check_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox chk = sender as ASPxCheckBox;
            chk.ReadOnly = view;
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
                ASPxComboBox combobox = sender as ASPxComboBox;
                combobox.DropDownButton.Enabled = !view;
                combobox.Enabled = !view;
                combobox.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
                var look = sender as ASPxGridLookup;
                if (look != null)
                {
                    look.DropDownButton.Enabled = !view;
                    look.ReadOnly = view;
                }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
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
                date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
                ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
                spinedit.SpinButtons.Enabled = !view;
                spinedit.ReadOnly = view;
        }
        protected void gvitem_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            ASPxGridView gv = sender as ASPxGridView;

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
            if (Request.QueryString["entry"].ToString() == "N")
            {
                if (e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New)
                    e.Visible = true;
            }
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            //if (Request.QueryString["entry"].ToString() == "E")
            //{
            //    if (e.ButtonType == ColumnCommandButtonType.Delete)
            //        e.Visible = false;
            //}

            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
                ASPxGridView gv = sender as ASPxGridView;        

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
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New)
                        e.Visible = true;
                }
                if (Request.QueryString["entry"].ToString() == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Delete)
                        e.Visible = false;
                }
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                        e.ButtonType == ColumnCommandButtonType.Update)
                        e.Visible = false;
                }
                if (Request.QueryString["entry"].ToString() == "E" )
                {
                    if ( e.ButtonType == ColumnCommandButtonType.Delete)
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
                    gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                    //     Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                }
            
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            var selectedValues = itemcode;
            CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
          //  Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
           // Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
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

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.ItemCode = txtItemCode.Text;
            _Entity.FullDesc = txtitemdesc.Text;
            _Entity.ShortDesc = txtshortdesc.Text;
            if (txtitemcat.Text == "" || txtitemcat.Text == null) { _Entity.ItemcategoryCode = Session["txtitemcat"].ToString(); } else { _Entity.ItemcategoryCode = txtitemcat.Text; }//txtitemcat.Text;
            _Entity.ProductCategoryCode = txtprodcategory.Text;
            _Entity.ProductSubCatCode = txtprodsubcat.Text;
            _Entity.Customer = txtitemcustomer.Text;
            _Entity.KeySupplier = txtitemsupplier.Text;
            _Entity.ReorderLevel = String.IsNullOrEmpty(txtreorderlevel.Text) ? 0 : Convert.ToDecimal(txtreorderlevel.Text);
            _Entity.MaxLevel = String.IsNullOrEmpty(txtmaxlevel.Text) ? 0 : Convert.ToDecimal(txtmaxlevel.Text);
            _Entity.MinQTY = String.IsNullOrEmpty(txtminorder.Text) ? 0 : Convert.ToDecimal(txtminorder.Text);
            _Entity.UnitBase = txtbaseunit.Text;
            _Entity.StandardQTY = String.IsNullOrEmpty(txtstandardqty.Text) ? 0 : Convert.ToDecimal(txtstandardqty.Text);
            _Entity.EstimatedCost = String.IsNullOrEmpty(txtestcost.Text) ? 0 : Convert.ToDecimal(txtestcost.Text);
            _Entity.UnitBulk = txtunitbulk.Text;
            _Entity.StorageType = txtstoragetype.Text;
            _Entity.PickingStrategy = txtstrategy.Text;
            _Entity.IsByBulk = Convert.ToBoolean(chkIsBulk.Value.ToString());
            _Entity.IsCore = Convert.ToBoolean(chkIsCore.Value.ToString());
            _Entity.IsPWD = Convert.ToBoolean(chkIsPWD.Value.ToString());
            _Entity.IsBNPC = Convert.ToBoolean(chkIsBNPC.Value.ToString());
            _Entity.IsSenior = Convert.ToBoolean(chkIsSenior.Value.ToString());
            _Entity.TaxCode = String.IsNullOrEmpty(aglTaxCode.Text) ? null : aglTaxCode.Value.ToString();
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            _Entity.Field10 = txth10.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeActivatedBy = txtDeactivatedBy.Text;
            _Entity.DeActivatedDate = txtDeactivatedDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            //---- FABRIC
            _Entity.FabricGroup = string.IsNullOrEmpty(txtFabGroup.Text)? "" : txtFabGroup.Value.ToString();
            _Entity.SupplierFabricCode = txtRetailFabCode.Text;
            _Entity.FabricDesignCategory = string.IsNullOrEmpty(txtFabDesCat.Text)? "" : txtFabDesCat.Value.ToString();
            _Entity.Dyeing = string.IsNullOrEmpty(txtDye.Text)? "" : txtDye.Value.ToString();
            _Entity.WeaveType = string.IsNullOrEmpty(txtWeave.Text) ? "" : txtWeave.Value.ToString();
            _Entity.Finishing = string.IsNullOrEmpty(txtFinishing.Text) ? "" : txtFinishing.Value.ToString();
            _Entity.CuttableWidth = string.IsNullOrEmpty(txtCuttableWidth.Text) ? "0" : txtCuttableWidth.Text;
            _Entity.GrossWidth = string.IsNullOrEmpty(txtGrossWidth.Text) ? "0" : txtGrossWidth.Text;
            _Entity.CuttableWeightBW = string.IsNullOrEmpty(txtCuttableWeightBW.Text) ? "0" : txtCuttableWeightBW.Text;
            _Entity.GrossWeightBW = string.IsNullOrEmpty(txtGrossWeightBW.Text) ? "0" : txtGrossWeightBW.Text;
            _Entity.FabricStretch = string.IsNullOrEmpty(txtFabricStretch.Text) ? "0" : txtFabricStretch.Text;
            _Entity.WarpConstruction = string.IsNullOrEmpty(txtWarpConstruction.Text) ? "0" : txtWarpConstruction.Text;
            _Entity.WeftConstruction = string.IsNullOrEmpty(txtWeftConstruction.Text) ? "0" : txtWeftConstruction.Text;
            _Entity.WarpDensity = string.IsNullOrEmpty(txtWarpDensity.Text) ? "0" : txtWarpDensity.Text;
            _Entity.WeftDensity = string.IsNullOrEmpty(txtWeftDensity.Text) ? "0" : txtWeftDensity.Text;
            _Entity.WarpShrinkage = string.IsNullOrEmpty(txtWarpShrinkage.Text) ? "0" : txtWarpShrinkage.Text;
            _Entity.WeftShrinkage = string.IsNullOrEmpty(txtWeftShrinkage.Text) ? "0" : txtWeftShrinkage.Text;
            _Entity.ForKnitsOnly = string.IsNullOrEmpty(cbforknits.Text) ? "" : cbforknits.Value.ToString();
            _Entity.Yield = txtYield.Text;
            _Entity.ItemType = string.IsNullOrEmpty(cboxItemType.Text) ? "" : cboxItemType.Value.ToString();
            //---- STOCK MASTER
            if (txtitemcat.Text.Trim() == "1")
            {
                _Stock.StockNumber = txtItemCode.Text;
                DataTable GetBrand = Gears.RetriveData2("select BrandCode From masterfile.Brand where BrandName = '" + txtBrand.Text + "'", Session["ConnString"].ToString());
                foreach (DataRow dtrow in GetBrand.Rows)
                {
                    _Stock.BrandCode = dtrow[0].ToString();
                    _Entity.BrandCode = dtrow[0].ToString();
                }
                DataTable GetProdGroup = Gears.RetriveData2("select ProductGroupCode From masterfile.ProductGroup where Description = '" + txtProductGroup.Text + "'", Session["ConnString"].ToString());
                foreach (DataRow dtrow in GetProdGroup.Rows)
                {
                    _Stock.ProductGroup = dtrow[0].ToString();
                    _Entity.ProductGroup = dtrow[0].ToString();
                }
                _Stock.DeliveryYear = txtDeliveryYear.Text;
                _Stock.DeliveryMonth = string.IsNullOrEmpty(cboDeliveryMonth.Text) ? "" : cboDeliveryMonth.Value.ToString();
                //_Stock.CollectionAbbreviation = txtCollAbbreviation.Text  ;
                _Stock.PISNumber = txtPISNumber.Text;
                _Stock.FitCode = txtFitCode.Text.Split('[')[0].Trim();
                //_Stock.ColorCode = txtColor.Text  ;
                _Stock.ProductDesignCategory = txtProdDesignCat.Text;
                //_Stock.ColorName = txtColorName.Text  ;
                //_Stock.RetailFabricCode = txtRetailFabric.Text  ;
                _Stock.WashCode = txtWash.Text;
                _Stock.TintCode = txtTint.Text;
                _Stock.ProductClass = string.IsNullOrEmpty(cbProdClass.Text) ? "" : cbProdClass.Value.ToString();
                _Stock.ImportedItem = txtImportedItem.Text;
                //  _Stock.ProductSubClass = string.IsNullOrEmpty(cbProdSubClass.Text) ? "" : cbProdSubClass.Value.ToString();
                if (cbProdSubClass.Text == "CORE") { _Stock.ProductSubClass = "C"; }
                if (cbProdSubClass.Text == "IMAGE") { _Stock.ProductSubClass = "I"; }
                if (cbProdSubClass.Text == "SEASON") { _Stock.ProductSubClass = "S"; }
                if (cbProdSubClass.Text == "REPEAT") { _Stock.ProductSubClass = "R"; }
                _Stock.ProductAlignment = string.IsNullOrEmpty(cbProdAlign.Text) ? "" : cbProdAlign.Value.ToString();
                _Stock.Season = string.IsNullOrEmpty(cbSeason.Text) ? "" : cbSeason.Value.ToString();
                _Stock.RecoAlloc = string.IsNullOrEmpty(cbRecon.Text) ? "" : cbRecon.Value.ToString();
                _Stock.SRP = string.IsNullOrEmpty(txtSRP.Text) ? 0 : Convert.ToDecimal(txtSRP.Text);
                _Stock.GenderCode = txtGender.Text; 
            }


            _Entity.GenderCode = txtGender.Text;
            if (txtFabGroup.Text == "KNITS")
            {
                cbforknits.ClientEnabled = true;
                txtYield.ClientEnabled = true;
            }
            else
            {
                cbforknits.ClientEnabled = false;
                cbforknits.Value = null;
                txtYield.ClientEnabled = false;
                txtYield.Text = null;
            }

            switch (e.Parameter)
            {
                case "Add":

                    //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method
                    
                    //if (error == false)
                    //{
                        DataTable getData = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.Item WHERE ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                        if (getData.Rows.Count > 0)
                        {
                            cp.JSProperties["cp_message"] = "Item Code is already in the record. Please use other code.";
                            cp.JSProperties["cp_success"] = true;
                        }
                        else
                        {
                            check = true;
                            //     _Entity.InsertData(_Entity);//Method of inserting for header
                            _Entity.AddedBy = Session["userid"].ToString();
                            if (Request.QueryString["useitem"] != null)
                                _Entity.UseThisItem = Request.QueryString["useitem"] == "1" ? true : false;

                            _Entity.InsertData(_Entity);
                            if (txtitemcat.Text.Trim() == "1") { _Stock.InsertData(_Stock); }
                            gv1.DataSourceID = "odsDetail";
                            gvFab.DataSourceID = "odsFab";
                            agvItemCustomer.DataSourceID = "odsCusDetail";
                            odsFab.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                            odsDetail.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                            gv1.UpdateEdit();//2nd initiation to insert grid
                            gvFab.UpdateEdit();
                            gvSellthru.DataSourceID = "odsstockPrice";
                            gvSellthru.UpdateEdit();
                            agvItemCustomer.UpdateEdit();
                            Validate();

                            Gears.RetriveData2("exec [sp_UponAddGenBarcode] '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                            _Entity.UpdateBasedonReference();
                            cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                            cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                            cp.JSProperties["cp_close"] = true;//Close window variable to client side

                            try
                            {
                                DataTable itemimagepath = Gears.RetriveData2("select distinct b.frontimage as ItemImage  from Masterfile.FitSizeDetail a " +
                                            "cross join Production.ProductInfoSheet b " +
                                            "where a.FitCode = '" + Request.QueryString["fit"].ToString() + "' and PISNUmber = '" + txtPISNumber.Text + "'", Session["ConnString"].ToString());
                                foreach (DataRow dtrow in itemimagepath.Rows)
                                {
                                    if (!string.IsNullOrEmpty(dtrow[0].ToString()))
                                        imagepath = dtrow[0].ToString();

                                }
                                Gears.RetriveData2("Update masterfile.itemdetail set BaseUnit = '" + txtbaseunit.Text + "', ItemImage='" + imagepath + "' where ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());

                            }
                            catch
                            {
                                Gears.RetriveData2("Update masterfile.itemdetail set BaseUnit = '" + txtbaseunit.Text + "', ItemImage='" + imagepath + "' where ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                            }
                        }

                    break;

                case "Update":

                    //gv1.UpdateEdit();
                     //DataTable a = gv1.DataBound();
                      
                    //if (error == false)
                    //{
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of Updating header
                        if (txtitemcat.Text.Trim() == "1") { _Stock.InsertData(_Stock); }
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        gv1.DataSourceID = "odsDetail";
                        gvFab.DataSourceID = "odsFab";
                        agvItemCustomer.DataSourceID = "odsCusDetail";
                        odsFab.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                        odsDetail.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                     gv1.UpdateEdit();//2nd Initiation to update grid

                        agvItemCustomer.UpdateEdit();

                        gvFab.UpdateEdit();
                        gvSellthru.DataSourceID = "odsstockPrice";
                        gvSellthru.UpdateEdit();
                        Validate();

                         string OnHand;
                         string OnOrder;
                         string OnAlloc;
                        string ActiveState;
                        string no = "Yes";
                        int b = gv1.VisibleRowCount;
                        //for (int i = 0; i < b; i++) {
                        //    OnHand = gv1.GetRowValues(i, "OnHand").ToString();
                        //    OnOrder = gv1.GetRowValues(i, "OnOrder").ToString();
                        //    OnAlloc = gv1.GetRowValues(i, "OnAlloc").ToString();
                        //    ActiveState = gv1.GetRowValues(i, "IsInactive").ToString();

                        //    if ((OnHand != "0" || OnOrder != "0" || OnAlloc != "0") && ActiveState == "True") { no = "No"; } 
                        //}
                    //if(no=="Yes"){
                        _Entity.UpdateBasedonReference();
                       
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";

                        Gears.RetriveData2("Update masterfile.itemdetail set BaseUnit = '" + txtbaseunit.Text + "' where ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                    //}
                    //else {
                    //    no = "Yes";
                    //    cp.JSProperties["cp_message"] = "Please Check Detail! Cannot Set as Inactive!";
                    //    cp.JSProperties["cp_success"] = true;
                    //    cp.JSProperties["cp_close"] = false;
                    //}
                        // }
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

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

                case "itemcat":
                    txtprodcategory.DataBind();
                    DataTable getunit = Gears.RetriveData2("Select BaseUnit,BulkUnit from masterfile.ItemCategory where ItemCategoryCode = '" + txtitemcat.Text + "'", Session["ConnString"].ToString());
                    foreach(DataRow dt in getunit.Rows){
                        txtbaseunit.Value = dt[0].ToString().Trim();
                        txtunitbulk.Value = dt[1].ToString().Trim();
                    }
                    break;

                case "prodcat":
                    txtprodsubcat.DataBind();
                    DataTable gethavsub = Gears.RetriveData2("Select HaveSubCategory from masterfile.ProductCategory where ProductCategoryCode = '" + txtprodcategory.Text + "'",Session["ConnString"].ToString());
                    foreach(DataRow dt in gethavsub.Rows){
                        txtHavProdSub.Text = dt[0].ToString();
                    }
                    break;

                case "fabgroup":
                    if (!string.IsNullOrEmpty(txtFabGroup.Value.ToString()) && txtFabGroup.Value.ToString() == "D")
                    {
                        Dye.SelectCommand = "SELECT DyeingCode,Description FROM MasterFile.Dyeing WHERE ProductGroup like '%D%'";
                        Weave.SelectCommand = "SELECT Code,Description FROM IT.GenericLookup WHERE LookUpKey = 'WEAVE'";
                    }
                    else
                    {
                        
                        Dye.SelectCommand = "SELECT DyeingCode,Description FROM MasterFile.Dyeing WHERE ProductGroup like '%K%'";
                        Weave.SelectCommand = "SELECT Code,Description FROM IT.GenericLookup WHERE LookUpKey = 'KNTYP'";
                    }

                    if (!string.IsNullOrEmpty(txtFabGroup.Value.ToString()) && txtFabGroup.Value.ToString() == "K")
                    {
                        frmlayout1.FindItemOrGroupByName("WeaveType").Caption = "Knit Type";
                    }
                    Dye.DataBind();
                    Weave.DataBind();
                    txtDye.Value = null;
                    txtWeave.Value = null;
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";

            //if (e.Errors.Count > 0)
            //{
            //    error = true; //bool to cancel adding/updating if true
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
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IsInactive"] = false;
            e.NewValues["Price"] = 0.00;
        }
        protected void gvSellthru_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["DateEncoded"] = Convert.ToString(DateTime.Now.ToShortDateString());
            e.NewValues["UserName"] = Session["userid"].ToString();
        }

        protected void gvuploadimage_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        }

        private void ViewReportSummary()
        {
            DataTable dtStockInfo = Gears.RetriveData2("exec sp_GetStockInfo '" + txtItemCode.Text + "'",Session["ConnString"].ToString());
            if (dtStockInfo.Rows.Count > 0)
            {
                decimal decQtySold = Convert.ToDecimal(dtStockInfo.Rows[0]["QtySold"].ToString());
                txtQTYSold.Text = string.Format("{0:#,0.00}", decQtySold);

                decimal decAveDailySales = Convert.ToDecimal(dtStockInfo.Rows[0]["AverageDailySales"].ToString());
                txtAverageDailySales.Text = string.Format("{0:#,0.00}", decAveDailySales);

                decimal decAveWeeklySales = decAveDailySales * 7;
                txtAverageWeeklySales.Text = string.Format("{0:#,0.00}", decAveWeeklySales);

                decimal decInitialSRP = Convert.ToDecimal(dtStockInfo.Rows[0]["InitialSRP"].ToString());
                txtInitialSRP.Text = string.Format("{0:#,0.00}", decInitialSRP);

                decimal decLatestSRP = Convert.ToDecimal(dtStockInfo.Rows[0]["LatestSRP"].ToString());
                txtUpdatedSRP.Text = string.Format("{0:#,0.00}", decLatestSRP);

                Decimal decWareHouseInv = Convert.ToDecimal(dtStockInfo.Rows[0]["WarehouseInventory"].ToString());
                txtWarehouseInventory.Text = string.Format("{0:#,0.00}", decWareHouseInv);

                Decimal decOutletInv = Convert.ToDecimal(dtStockInfo.Rows[0]["OutletInventory"].ToString());
                txtOutletInventory.Text = string.Format("{0:#,0.00}", decOutletInv);

                Decimal decAverageBuyingCost = Convert.ToDecimal(dtStockInfo.Rows[0]["AverageBuyingCost"].ToString());
                txtAverageBuyingCost.Text = string.Format("{0:#,0.00}", decAverageBuyingCost);

                Decimal decPlannedBuyingCost = Convert.ToDecimal(dtStockInfo.Rows[0]["PlannedBuyingCost"].ToString());
                txtPlannedBuyingCost.Text = string.Format("{0:#,0.00}", decPlannedBuyingCost);

                Decimal decIndexPrice = Convert.ToDecimal(dtStockInfo.Rows[0]["IndexPrice"].ToString());
                txtIndexPrice.Text = string.Format("{0:#,0.00}", decIndexPrice);

                Decimal decTotalInventory = decOutletInv + decWareHouseInv;
                txtTotalInventory.Text = string.Format("{0:#,0.00}", decTotalInventory);

                if (decAverageBuyingCost > 0)
                {
                    Decimal decAverageSellingMarkup = decIndexPrice / decAverageBuyingCost;
                    txtAverageSellingMarkup.Text = string.Format("{0:#,0.00}", decAverageSellingMarkup);
                }
                else
                {
                    txtAverageSellingMarkup.Text = "0.00";
                }
                if ((decQtySold + decTotalInventory) * 100 > 0)
                {
                    Decimal decSellThru = decQtySold / (decQtySold + decTotalInventory) * 100;
                    txtTotalSellThru.Text = string.Format("{0:#,0.00}", decSellThru);
                }
                else
                {
                    txtTotalSellThru.Text = "0.00";
                }


                Decimal decMonthCount = Convert.ToDecimal(dtStockInfo.Rows[0]["MonthCount"].ToString());

                Decimal decAverageMonthlySales = 0;
                if (decMonthCount > 0)
                {
                    decAverageMonthlySales = decQtySold / decMonthCount;
                }
                txtAverageMonthlySales.Text = string.Format("{0:#,0.00}", decAverageMonthlySales);

                Decimal decUpdatedBuyingMarkup = 0;
                if (decPlannedBuyingCost > 0)
                {
                    decUpdatedBuyingMarkup = decLatestSRP / decPlannedBuyingCost;
                }
                txtUpdatedBuyingMarkup.Text = string.Format("{0:#,0.00}", decUpdatedBuyingMarkup);

                Decimal decSSRDay = 0;
                if (decAveDailySales > 0)
                {
                    decSSRDay = decTotalInventory / decAveDailySales;
                }
                txtSSRDays.Text = string.Format("{0:#,0.00}", decSSRDay);

                Decimal decSSRWeek = 0;
                if (decAveWeeklySales > 0)
                {
                    decSSRWeek = decTotalInventory / decAveWeeklySales;
                }
                txtSSRWeeks.Text = string.Format("{0:#,0.00}", decSSRWeek);

                Decimal decSSRMonths = 0;
                if (decAverageMonthlySales > 0)
                {
                    decSSRMonths = decTotalInventory / decAverageMonthlySales;
                }
                txtSSRMonths.Text = string.Format("{0:#,0.00}", decSSRMonths);
            }
            //,ISNULL(@TotalAmount,0) AS TotalAmount


            DataTable dtStockInfoPerSize = Gears.RetriveData2("exec sp_GetStockInfoPerSize '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
            if (dtStockInfoPerSize.Rows.Count > 0)
            {
                gvPerSizeReport.DataSource = dtStockInfoPerSize;
                gvPerSizeReport.DataBind();
                gvPerSizeReport.KeyFieldName = "Size";
            }

            DataTable dtOutletSales = Gears.RetriveData2("exec sp_GetOutletSales '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
            if (dtOutletSales.Rows.Count > 0)
            {
                gvOutletSales.DataSourceID = null; 
                gvOutletSales.DataSource = dtOutletSales;
            }
            DataTable dtDateOfTrans = Gears.RetriveData2("SELECT MIN(DocDate) FROM Accounting.SubsiLedgerInv WHERE ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
            if (dtDateOfTrans.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dtDateOfTrans.Rows[0][0].ToString()))
                {
                    DateTime Dtrans = Convert.ToDateTime((dtDateOfTrans.Rows[0][0].ToString()));
                    int diffDays = DateTime.Today.Subtract(Dtrans).Days;
                    txtWeeksRunning.Text = (diffDays / 7).ToString();
                }
            }
            DataTable dtPOHistory = Gears.RetriveData2("exec sp_POHistory '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
            if (dtPOHistory.Rows.Count > 0)
            {
                gvPOHistory.DataSource = dtPOHistory;
            }
            DataTable dtPriceHistory2 = Gears.RetriveData2("exec sp_GetPriceHistory '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
            if (dtPriceHistory2.Rows.Count > 0)
            {
                gvPriceHistory2.DataSource = dtPriceHistory2;
            }
        }

        
    }
}