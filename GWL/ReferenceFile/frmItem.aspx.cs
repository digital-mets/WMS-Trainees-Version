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
    public partial class frmItem : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Item _Entity = new Item();//Calls entity ICN

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

            if (Request.QueryString["transtype"].ToString() == "REFRMM")
            {
                FormLabel.Text = "Raw Materials Master";
                ItemCategory.SelectCommand = "select ItemCategoryCode,Description from Masterfile.ItemCategory WHERE ISNULL(IsInactive,0) =0 and isnull(IsAsset,0)=1 ORDER BY CONVERT(int, ItemCategoryCode) ASC";
            }

            if (Request.QueryString["transtype"].ToString() == "REFSPM")
            {
                FormLabel.Text = "Spice Master";
                ItemCategory.SelectCommand = "select ItemCategoryCode,Description from Masterfile.ItemCategory WHERE ISNULL(IsInactive,0) =0 and isnull(IsAsset,0)=1 ORDER BY CONVERT(int, ItemCategoryCode) ASC";
            }

            if (Request.QueryString["transtype"].ToString() == "REFOIM")
            {
                FormLabel.Text = "Other ingredients Master";
                ItemCategory.SelectCommand = "select ItemCategoryCode,Description from Masterfile.ItemCategory WHERE ISNULL(IsInactive,0) =0 and isnull(IsAsset,0)=1 ORDER BY CONVERT(int, ItemCategoryCode) ASC";
            }

            /*ProdCat.SelectCommand = "select ProductCategoryCode,Description from  Masterfile.ProductCategory where " +
                                    "ISNULL(IsInactive,0)=0 and ItemCategoryCode = '" + txtitemcat.Text + "'";
            ProdSubCat.SelectCommand = "select ProductSubCatCode,Description from  Masterfile.ProductCategorySub where " +
                                       "ISNULL(IsInactive,0)=0 and ProductCategoryCode like '%" + txtprodcategory.Text.Trim() + "%'";
            */
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
                frmlayout1.FindItemOrGroupByName("FabricInfo").ClientVisible = true;
            }
            //else if (Request.QueryString["parameters"].ToString() == "1" || Request.QueryString["parameters"].ToString() == "1  ")
            //{
            //    frmlayout1.FindItemOrGroupByName("WMSInfo").ClientVisible = true;
            //}

            if (!IsPostBack)
            {


                switch (Request.QueryString["parameters"].ToString())
                {
                    case "RM":
                        FormLabel.Text = "Raw Materials";
                        break;
                }

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
                        updateBtn.Text = "Update";
                        glcheck.ClientVisible = true;
                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }


                //this.gv1.Columns["BaseUnit"].Width = 0;

                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //gv1.DataSourceID = "sdsDetail";
                    gvUnit.DataSourceID = "sdsDetail3";

                    agvItemSupplier.DataSourceID = "sdsItemSupp";
                    popup.ShowOnPageLoad = false;
                    if (Request.QueryString["docnumber"].ToString()!="undefined")
                    {
                        txtItemCode.ReadOnly = true;
                        txtItemCode.Value = Request.QueryString["docnumber"].ToString();
                    }
                    if (Request.QueryString["itemcat"] != null)
                    {
                        //txtitemcat.Value = Request.QueryString["itemcat"].ToString();
                    }
                    //txtitemcat.Value = "2  ";
                    if (Request.QueryString["prodcat"] != null)
                    {
                        /*ProdCat.SelectCommand = "select ProductCategoryCode,Description from  Masterfile.ProductCategory where " +
                                    "ISNULL(IsInactive,0)=0 and ItemCategoryCode like '%" + txtitemcat.Text.Trim() + "%'";
                        txtprodcategory.DataBind();
                        txtprodcategory.Value = Request.QueryString["prodcat"].ToString();*/
                    }
                    if (Request.QueryString["prodsub"] != null)
                    {
                        //txtprodsubcat.Value = Request.QueryString["prodsub"].ToString();
                    }
                    if (Request.QueryString["sup"] != null)
                    {
                        //txtitemsupplier.Value = Request.QueryString["sup"].ToString();
                    }
                    
                }
                else
                {
                    txtItemCode.ReadOnly = true;
                    txtItemCode.Value = Request.QueryString["docnumber"].ToString();
                    agvRunningInv.DataSourceID = "odsWHDetail";
                    agvItemCustomer.DataSourceID = "odsCusDetail";
                    agvItemSupplier.DataSourceID = "odsSuppDetail";
                    
                    //gv1.DataSourceID = "odsDetail";
                    gvUnit.DataSourceID = "odsDetail3";


                    //txtbaseunit.ClientEnabled = false;
                    
                    /*txtitemcat.ClientEnabled = false;
                    txtprodcategory.ClientEnabled = false;
                    txtprodsubcat.ClientEnabled = false;
                    */
                    _Entity.getdata(txtItemCode.Text, Session["ConnString"].ToString());
                    txtitemdesc.Text = _Entity.FullDesc;
                    //txtshortdesc.Text = _Entity.ShortDesc;
                    //txtitemcat.Value = _Entity.ItemcategoryCode;
                    //txtitemcat2.Text = _Entity.ItemcategoryCode;
                    // 04 - 06 - 2016   LGE Add Filter when form is being use as Asset Masterfile
                    if (Request.QueryString["transtype"].ToString() == "REFASS")
                    {
                        ItemCategory.SelectCommand = "select ItemCategoryCode,Description from Masterfile.ItemCategory WHERE ISNULL(IsInactive,0) =0 and isnull(IsAsset,0)=1 ORDER BY CONVERT(int, ItemCategoryCode) ASC";
                    }
                    /*ProdCat.SelectCommand = "select ProductCategoryCode,Description from  Masterfile.ProductCategory where " +
                                    "ISNULL(IsInactive,0)=0 and ItemCategoryCode like '%" + txtitemcat.Text.Trim() + "%'";*/
                    //txtprodcategory.DataBind();
                    //txtprodcategory.Value = _Entity.ProductCategoryCode;
                    /*ProdSubCat.SelectCommand = "select ProductSubCatCode,Description from  Masterfile.ProductCategorySub where " +
                                               "ISNULL(IsInactive,0)=0 and ProductCategoryCode like '%" + txtprodcategory.Text.Trim() + "%'";*/
                    ProdSubCat.DataBind();
                    //txtprodsubcat.DataBind();
                    //txtprodsubcat.Value = _Entity.ProductSubCatCode;
                    //txtitemcustomer.Value = _Entity.Customer;
                    //txtitemsupplier.Value = _Entity.KeySupplier;
                    txtreorderlevel.Text = _Entity.ReorderLevel.ToString();
                    txtmaxlevel.Text = _Entity.MaxLevel.ToString();
                    txtstandardqty.Text = _Entity.StandardQTY.ToString();
                    txtbaseunit.Value = _Entity.UnitBase;
                    //txtunitbulk.Value = _Entity.UnitBulk;
                    txtstoragetype.Value = _Entity.StorageType;
                    txtstrategy.Text = _Entity.PickingStrategy;
                    txtminorder.Text = _Entity.MinQTY.ToString();
                    //txtestcost.Text = _Entity.EstimatedCost.ToString();
                    //chkIsBulk.Value = _Entity.IsByBulk;
                    //chkIsCore.Value = _Entity.IsCore;
                    chkIsInactive.Value = _Entity.IsInactive;
                    cboxItemType.Value = _Entity.ItemType;
                    //aglTaxCode.Text = _Entity.TaxCode.ToString();

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
                    
                    /*Dye.DataBind();
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
                    txtYield.Text = _Entity.Yield;*/
                }
              //  DataTable checkCount = Gears.RetriveData2("Select fabriccode from MasterFile.FabricCompositionDetail where fabriccode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());
                /*if (checkCount.Rows.Count > 0)
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
                }*/

               DataTable checkCount = Gears.RetriveData2(" select ItemCode from Masterfile.ItemUnit where ItemCode = '" + txtItemCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gvUnit.DataSourceID = (checkCount.Rows.Count > 0 ? "odsDetail3" : "sdsDetail3");
            
            
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
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
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }



        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                        e.ButtonType == ColumnCommandButtonType.Update)
                        e.Visible = false;
                }
                if (e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
        }

        protected void gv_CommandButtonInitialize2(object sender, ASPxGridViewCommandButtonEventArgs e)
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
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            var selectedValues = itemcode;
            //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
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
            //_Entity.ShortDesc = txtshortdesc.Text;
            if (Request.QueryString["transtype"].ToString() == "REFRMM")
            {

                _Entity.ItemcategoryCode = "004";

            }
            else if (Request.QueryString["transtype"].ToString() == "REFSPM")
            {

                _Entity.ItemcategoryCode = "005";

            }
            else if (Request.QueryString["transtype"].ToString() == "REFOIM")
            {

                _Entity.ItemcategoryCode = "006";

            }
            //_Entity.ItemcategoryCode = txtitemcat2.Text;

            //_Entity.ProductCategoryCode = txtprodcategory.Text;
            //_Entity.ProductSubCatCode = txtprodsubcat.Text;
            //_Entity.Customer = txtitemcustomer.Text;
            //_Entity.KeySupplier = txtitemsupplier.Text;
            _Entity.ReorderLevel = String.IsNullOrEmpty(txtreorderlevel.Text) ? 0 : Convert.ToDecimal(txtreorderlevel.Text);
            _Entity.MaxLevel = String.IsNullOrEmpty(txtmaxlevel.Text) ? 0 : Convert.ToDecimal(txtmaxlevel.Text);
            _Entity.MinQTY = String.IsNullOrEmpty(txtminorder.Text) ? 0 : Convert.ToDecimal(txtminorder.Text);
            _Entity.UnitBase = txtbaseunit.Text;
            _Entity.StandardQTY = String.IsNullOrEmpty(txtstandardqty.Text) ? 0 : Convert.ToDecimal(txtstandardqty.Text);
            //_Entity.EstimatedCost = String.IsNullOrEmpty(txtestcost.Text) ? 0 : Convert.ToDecimal(txtestcost.Text);
            //_Entity.UnitBulk = txtunitbulk.Text;
            _Entity.StorageType = txtstoragetype.Text;
            _Entity.PickingStrategy = txtstrategy.Text;
            //_Entity.IsByBulk = Convert.ToBoolean(chkIsBulk.Value.ToString());
            //_Entity.IsCore = Convert.ToBoolean(chkIsCore.Value.ToString());
            //_Entity.TaxCode = String.IsNullOrEmpty(aglTaxCode.Text) ? null : aglTaxCode.Value.ToString();
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
                    gvUnit.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.InsertData (_Entity);
                        //gv1.DataSourceID = "odsDetail";
                        gvUnit.DataSourceID = "odsDetail3";
                        
                        gvFab.DataSourceID = "odsFab";
                        agvItemCustomer.DataSourceID = "odsCusDetail";
                        odsFab.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                        odsDetail.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                        odsDetail3.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;

                        //gv1.UpdateEdit();//2nd initiation to insert grid
                        gvUnit.UpdateEdit();

                        gvFab.UpdateEdit();
                        agvItemCustomer.UpdateEdit();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    //gv1.UpdateEdit();
                    gvUnit.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of Updating header
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        //gv1.DataSourceID = "odsDetail";
                        gvUnit.DataSourceID = "odsDetail3";

                        gvFab.DataSourceID = "odsFab";
                        agvItemCustomer.DataSourceID = "odsCusDetail";
                        odsFab.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                        odsDetail.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                        odsDetail3.SelectParameters["ItemCode"].DefaultValue = txtItemCode.Text;
                        
                        //gv1.UpdateEdit();//2nd Initiation to update grid
                        gvUnit.UpdateEdit();

                        agvItemCustomer.UpdateEdit();
                        gvFab.UpdateEdit();
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

                case "itemcat":
                    //txtprodcategory.DataBind();
                    //DataTable getunit = Gears.RetriveData2("Select BaseUnit,BulkUnit from masterfile.ItemCategory where ItemCategoryCode = '" + txtitemcat.Text + "'", Session["ConnString"].ToString());
                    //foreach(DataRow dt in getunit.Rows){
                    //    txtbaseunit.Value = dt[0].ToString().Trim();
                    //    txtunitbulk.Value = dt[1].ToString().Trim();
                    //}
                    break;

                case "prodcat":
                    //txtprodsubcat.DataBind();
                    //DataTable gethavsub = Gears.RetriveData2("Select HaveSubCategory from masterfile.ProductCategory where ProductCategoryCode = '" + txtprodcategory.Text + "'",Session["ConnString"].ToString());
                    //foreach(DataRow dt in gethavsub.Rows){
                    //    txtHavProdSub.Text = dt[0].ToString();
                    //}
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
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "";
            string SizeCode = "";

            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
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
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }


        #endregion

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glCustomerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void gvUnit_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["FromUnit"] = txtbaseunit.Value; 
        }

        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IsInactive"] = false;
            e.NewValues["Price"] = 0.00;
        }


        //protected void dtpdocdate_Init(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["entry"] == "N")
        //    {
        //        dtpdocdate.Date = DateTime.Now;
        //    }
        //}
    }
}