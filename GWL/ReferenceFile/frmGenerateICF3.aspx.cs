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
    public partial class frmGenerateICF : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        public string udpItemCategory
        {
            get { return (string)cboItemCategory.Value; }
            set { cboItemCategory.Value = value; }

        }
        public string udpBrand
        {
            get { return txtBrand.Text; }
            set { txtBrand.Text = value; }
        }
        public string udpBrand2
        {
            get { return txtBrand2.Text; }
            set { txtBrand2.Text = value; }
        }
        public string udpGender
        {
            get { return txtGender.Text; }
            set { txtGender.Text = value; }
        }
        public string udpProductGroup
        {
            get { return txtProductGroup.Text; }
            set { txtProductGroup.Text = value; }
        }
        public string udpFit
        {
            get { return txtFit.Text; }
            set { txtFit.Text = value; }
        }
        public string udpColorDesc
        {
            get { return txtColorDesc.Text; }
            set { txtColorDesc.Text = value; }
        }
        public string udpItemCode
        {
            get { return txtItemCode.Text; }
            set { txtItemCode.Text = value; }
        }
        public string udpProductCategory
        {
            get { return txtProductCategory.Text; }
            set { txtProductCategory.Text = value; }
        }
        public string udpSupplierItemCode
        {
            get { return txtShortDescription.Text; }
            set { txtShortDescription.Text = value; }
        }
        public string udpSupplierItemCode2
        {
            get { return txtSupplierItemCode.Text; }
            set { txtSupplierItemCode.Text = value; }
        }
        public string udpColorName
        {
            get { return txtColorName.Text; }
            set { txtColorName.Text = value; }
        }
        public string udpProductSubCategory
        {
            get { return txtProductSubCategory.Text; }
            set { txtProductSubCategory.Text = value; }
        }
        public string udpShortDesc
        {
            get { return txtShortDescription.Text; }
            set { txtShortDescription.Text = value; }
        }
        public string udpCustomer
        {
            get { return txtCustomer.Text; }
            set { txtCustomer.Text = value; }
        }
        public string udpPIS
        {
            get { return txtPIS.Text; }
            set { txtPIS.Text = value; }
        }
        public string udpColor
        {
            get { return txtColor.Text; }
            set { txtColor.Text = value; }
        }
        public string udpRetailFabricCode
        {
            get { return txtRetailFabricCode.Text; }
            set { txtRetailFabricCode.Text = value; }
        }
        public string udpSupplier
        {
            get { return txtKeySupplier.Text; }
            set { txtKeySupplier.Text = value; }
        }
        public string udpDeliveryYear
        {
            get { return txtDeliveryYear.Text; }
            set { txtDeliveryYear.Text = value; }
        }
        public string udpDeliveryMonth
        {
            get { return cboDeliveryMonth.Value.ToString(); }
            set { cboDeliveryMonth.Value = value; }
        }
        bool blnoformat = false;
        public bool udpNoFormat
        {
            get { return blnoformat; }
            set { blnoformat = value; }
        }
        //string strIsRetail = Common.Common.SystemSetting("ISRETAIL", HttpContext.Current.Session["ConnString"].ToString());
        string strSubCatMnemonics = "*";
        string strColor = "";
        string strDeliveryYear = "**";
        string strDeliveryMonth = "*";
        string str2nd = "";

        Entity.Color _Entity = new Color();//Calls entity ICN
  
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            //string referer;
            //try //Validation to restrict user to browse/type directly to browser's address
            //{
            //    referer = Request.ServerVariables["http_referer"];
            //}
            //catch
            //{
            //    referer = "";
            //}

            //if (referer == null)
            //{
            //    Response.Redirect("~/error.aspx");
            //}
         
            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                //cboItemCategory.Text = "FABRIC";
                cboItemCategory.ReadOnly = true;
                cboItemCategory.Value = "2  ";
                Loaditems();
            }
            
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }

        protected void CheckBoxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
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
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
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
       
     
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

           
            switch (e.Parameter)
            {

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";

                    break;

                case "prodcat":
                    DataTable getprodcat = Gears.RetriveData2(string.Format("Select Description from masterfile.Productcategory where ProductCategoryCode = '{0}'", txtProductCategory.Text), Session["ConnString"].ToString());
                    foreach(DataRow dtr in getprodcat.Rows){
                        txtProductCategoryDesc.Text = dtr[0].ToString();
                    }
                    txtItemCode.Text = ItemGenerator(txtItemCode, false);
                    break;

                case "prodsub":
                    DataTable getprodsub = Gears.RetriveData2(string.Format("Select Description from masterfile.ProductCategorySub where ProductSubCatCode = '{0}'", txtProductSubCategory.Text), Session["ConnString"].ToString());
                    foreach (DataRow dtr in getprodsub.Rows)
                    {
                        txtProductSubCategoryDesc.Text = dtr[0].ToString();
                    }
                    txtItemCode.Text = ItemGenerator(txtItemCode, false);
                    break;
                case "keysupp":
                    DataTable getsupp = Gears.RetriveData2(string.Format("SELECT SupMnemonics FROM MasterFile.BPSupplierInfo WHERE SupplierCode = '{0}' ", txtKeySupplier.Text), Session["ConnString"].ToString());
                    foreach (DataRow dtr in getsupp.Rows)
                    {
                        txtSuppName.Text = dtr[0].ToString();
                    }
                    txtItemCode.Text = ItemGenerator(txtItemCode, false);
                    break;

                case "textchanged":
                    txtItemCode.Text = ItemGenerator(txtItemCode, false);
                    break;

                case "pis":
                    DataTable dtPIS = Gears.RetriveData2(string.Format("SELECT * FROM MasterFile.ProductInfoSheet WHERE PISNumber = '{0}'", txtPIS.Value.ToString().Trim()), Session["ConnString"].ToString());
                    txtBrand.Text = dtPIS.Rows[0]["BrandCode"].ToString();
                    txtProductGroup.Text = dtPIS.Rows[0]["ProductGroup"].ToString();
                    txtDeliveryYear.Text = dtPIS.Rows[0]["DeliveryYear"].ToString();
                    cboDeliveryMonth.Value = dtPIS.Rows[0]["DeliveryMonth"].ToString();
                    txtDesignCategory.Text = dtPIS.Rows[0]["DesignCategory"].ToString();
                    txtColor.Text = dtPIS.Rows[0]["FabricColor"].ToString();
                    txtGender.Text = dtPIS.Rows[0]["Gender"].ToString();
                    txtWashCode.Text = dtPIS.Rows[0]["WashCode"].ToString();
                    txtTintCode.Text = dtPIS.Rows[0]["Tint"].ToString();
                    if (txtProductCategory.Text.Trim() == "B")
                    {
                        txtFit.Text = dtPIS.Rows[0]["FitCode"].ToString();
                    }
                    else if(txtProductCategory.Text.Trim() == "T"){
                        txtFit.Text = dtPIS.Rows[0]["FitCode"].ToString();
                    }
                    if (txtProductCategory.Text.Trim() == "T")
                    {
                        txtWashCode.Enabled = false;
                        txtTintCode.Enabled = false;
                        txtWashCode.Text = "";
                        txtTintCode.Text = "";
                        txtWashDesc.Text = "";
                        txtTintDesc.Text = "";
                        txtColor.Enabled = true;
                    }
                    else if ((txtProductCategory.Text.Trim() == "B") && (txtProductGroup.Text.Trim() == "D"))
                    {
                        txtWashCode.Enabled = true;
                        txtTintCode.Enabled = true;
                        txtColor.Enabled = false;
                        txtColor.Text = "";
                        txtColorDesc.Text = "";
                    }
                    break;
            }
        }
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
      #endregion

        #region Validation
   private void Validate()
   {
       //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
       //gparam._DocNo = _Entity.;
       //gparam._UserId = Session["Userid"].ToString();
       //gparam._TransType = "WMSIA";

       //string strresult = GWarehouseManagement.PickList_Validate(gparam);

       //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

   }
   #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        public string ItemGenerator(ASPxTextBox txtobj, bool blFinal)
        {
            String ProdCat = "";
            ProdCat = "XX";
            String ProdCatSub = "XX";
            //old emc 2/6/2014 9:11am gawin 12 char na String OthderDesc = "XXXXXXXX";
            String OthderDesc = "XXXXXXXXXXXX";
            String SupMnemo = "XX";
            String CusMnemo = "XX";
            String CusSeqNum = "001";
            String Brand = "XX";
            String StrGender = "X";
            String Cyear = Common.Common.SystemSetting("CYEAR", Session["ConnString"].ToString());
            Cyear = Cyear.Substring(2, 2);
            string Itemcat = cboItemCategory.SelectedItem == null ? cboItemCategory.Value.ToString() : cboItemCategory.SelectedItem.Value.ToString();
            string strQuery = string.Format("SELECT * From MasterFile.ItemCategory where ItemCategoryCode='{0}'", Itemcat);

            DataTable dtCat;
            dtCat = Gears.RetriveData2(strQuery, Session["ConnString"].ToString());

            if (dtCat.Rows.Count <= 0)
            {
                return "";
            }

            string strFormat = dtCat.Rows[0]["ItemCodeFormat"].ToString();

            if (strFormat == "NONE")
            {
                return "";
            }

            bool blIsAsset = Convert.ToBoolean(dtCat.Rows[0]["IsAsset"]);

            if (!string.IsNullOrEmpty(txtProductCategory.Text))
            {
                ProdCat = txtProductCategory.Text.Trim();
            }

            if (!string.IsNullOrEmpty(txtProductSubCategory.Text))
            {
                ProdCatSub = txtProductSubCategory.Text.Trim();
            }

            if (!string.IsNullOrEmpty(txtShortDescription.Text))
            {
                OthderDesc = txtShortDescription.Text.Trim();

                // old code  emc 2/6/2013 9:13am 
                //if (OthderDesc.Length >= 8)
                //{
                //    OthderDesc = OthderDesc.Substring(0, 8);
                //}

                //new code 12 char na
                if (OthderDesc.Length >= 12)
                {
                    OthderDesc = OthderDesc.Substring(0, 12);
                }

            }

            if (!string.IsNullOrEmpty(txtSupplierItemCode.Text))
            {
                OthderDesc = txtSupplierItemCode.Text.Trim();
                if (OthderDesc.Length >= 6)
                {
                    OthderDesc = OthderDesc.Substring(0, 6);
                }
            }

            if (!string.IsNullOrEmpty(txtKeySupplier.Text))
            {
                DataTable dtDetail = Gears.RetriveData2(string.Format("SELECT SupMnemonics FROM MasterFile.BPSupplierInfo WHERE SupplierCode = '{0}' ", txtKeySupplier.Text), Session["ConnString"].ToString());
                if (dtDetail.Rows.Count != 0)
                {
                    DataRow _Row = dtDetail.Rows[0];
                    if (!Convert.IsDBNull(_Row["SupMnemonics"]))
                    {
                        SupMnemo = _Row["SupMnemonics"].ToString();
                        SupMnemo = SupMnemo.Trim();
                    }
                    else
                    {
                        SupMnemo = "XX";
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtBrand2.Text))
            {
                string strBrand2 = string.Format("SELECT * FROM MasterFile.Brand WHERE ISNULL(IsInactive,0)=0 AND BrandCode ='{0}'", txtBrand2.Text);
                DataTable dtBrand2 = Gears.RetriveData2(strBrand2,Session["ConnString"].ToString());
                if (dtBrand2.Rows.Count >= 1)
                {
                    Brand = dtBrand2.Rows[0]["AccessoriesMnemonics"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {

                //CusSeqNum =  Common.Common.SystemSetting("MN" + txtCustomer.Text);

                if (blFinal)
                {
                    string SQLSecNum = string.Format("Select SeqNum from Masterfile.BPCustomerInfo where  BizPartnerCode = '{0}'", txtCustomer.Text);
                    DataTable CusSeq = Gears.RetriveData2(SQLSecNum, Session["ConnString"].ToString());

                    if (CusSeq.Rows.Count != 0)
                    {
                        CusSeqNum = (CusSeq.Rows[0][0] == null || String.IsNullOrEmpty(CusSeq.Rows[0][0].ToString())) ? "0" : CusSeq.Rows[0][0].ToString();
                        CusSeqNum = Convert.ToString(Convert.ToInt32(CusSeqNum) + 1);
                        CusSeqNum = CusSeqNum.PadLeft(3, '0');
                    }

                }
                else
                {
                    CusSeqNum = "***";
                }

                DataTable dtDetail = Gears.RetriveData2("SELECT CusMnemonics FROM MasterFile.BPCustomerInfo WHERE BizPartnerCode = '" + txtCustomer.Text + "' ", Session["ConnString"].ToString());
                if (dtDetail.Rows.Count != 0)
                {
                    DataRow _Row = dtDetail.Rows[0];
                    if (!Convert.IsDBNull(_Row["CusMnemonics"]))
                    {
                        CusMnemo = _Row["CusMnemonics"].ToString();
                        CusMnemo = CusMnemo.Trim();

                    }
                }

            }
            //system Setting
            //MNEMO

            String TxtItem = "";

            if (cboItemCategory.Value.ToString().Trim() == "2") //fabric
            {
                TxtItem = ProdCatSub + SupMnemo + "-" + OthderDesc;
            }
            else if (cboItemCategory.Value.ToString().Trim() == "3") //Accs
            {
                TxtItem = ProdCat + ProdCatSub + Brand + "-" + OthderDesc;
            }
            else if (cboItemCategory.Value.ToString().Trim() == "1") //FG
            {
                //B-Brand Deliveryyear fitcode - retailfabcode(2charforDenim 3charfor Nondenim) (washcodeforDenim and BottomColor4NonDenim)
                //T-brand deliveryyear deliverymonth gender prodsubcat proddesigncat
                if (txtProductCategory.Text.Trim() == "B")
                {
                    if (txtProductGroup.Text.Trim() == "D")
                    {
                        TxtItem = str2nd + txtBrand.Text.Trim() + strDeliveryYear +
                                  txtFit.Text.Trim() + "-";
                        TxtItem = TxtItem + txtRetailFabricCode.Text.Trim() +
                              txtWashCode.Text.Trim() + txtTintCode.Text.Trim();
                    }

                    else
                    {

                        TxtItem = str2nd + txtBrand.Text.Trim() + strDeliveryYear +
                                      txtFit.Text.Trim() + "-";

                        TxtItem = TxtItem + txtRetailFabricCode.Text.Trim();
                        if (txtRetailFabricCode.Text.Trim().Length == 3)
                        {
                            TxtItem = TxtItem.Trim() + strColor.Trim();
                        }

                    }
                }
                else if (txtProductCategory.Text.Trim() == "T")
                {
                    string strFit = "*";
                    DataTable dtFit = Gears.RetriveData2("SELECT Fitting FROM MasterFile.Fit WHERE FitCode = '" + txtFit.Text.Trim() + "'", Session["ConnString"].ToString());
                    if (dtFit.Rows.Count > 0)
                    {
                        strFit = dtFit.Rows[0][0].ToString().Trim();
                    }

                    TxtItem = str2nd + txtBrand.Text.Trim() + strDeliveryYear +
                            strDeliveryMonth + "-" + txtGender.Text.Trim() +
                            strSubCatMnemonics.Trim() + strFit + txtDesignCategory.Text.Trim() + txtColor.Text.Trim();
                }
                else
                {

                    TxtItem = txtBrand.Text.Trim() + txtImportedItem.Text.Trim() + strDeliveryYear + strDeliveryMonth + txtGender.Text.Trim()
                        + strSubCatMnemonics.Trim() + "-" + txtColor.Text.Trim();
                }
            }
            else
            {
                TxtItem = str2nd + CusMnemo + StrGender.Substring(0, 1) + "-" + ProdCat.Substring(0, 1) + Cyear + "-" + CusSeqNum;
            }


            if (blIsAsset)//ASSET
            {
                TxtItem = ProdCat.Substring(0, 2) + ProdCatSub.Substring(0, 2) + OthderDesc;
            }

            txtItemCode.Text = TxtItem;

            return TxtItem;

        }

        private string RemoveSpaces(string str)
        {
            string result = "";
            char[] remover = str.ToCharArray();

            for (int i = 0; i < remover.Length; i++)
            {
                if (remover[i] != ' ')
                {
                    result = string.Concat(result, remover[i]);
                }

            }

            return result;
        }

        protected void txtKeySupplier_TextChanged(object sender, EventArgs e)
        {
            txtItemCode.Text = ItemGenerator(txtItemCode, false);
        }

        protected void txtProductCategory_TextChanged(object sender, EventArgs e)
        {
            if (txtProductCategory.Text.Trim() == "B")
            {
                txtDesignCategory.Text = "";
                txtDesignCategoryDesc.Text = "";
                txtDesignCategory.Enabled = false;
                txtRetailFabricCode.Enabled = true;
                txtImportedItem.Enabled = false;
                txtImportedItem.Text = null;
            }
            else if (txtProductCategory.Text.Trim() == "A")
            {
                txtImportedItem.Enabled = true;
                txtRetailFabricCode.Enabled = false;
                txtRetailFabricCode.Text = "";
                txtDesignCategory.Enabled = true;
            }
            else
            {
                txtRetailFabricCode.Enabled = false;
                txtRetailFabricCode.Text = "";
                txtDesignCategory.Enabled = true;
                txtImportedItem.Enabled = false;
                txtImportedItem.Text = null;
            }
            if ((txtProductCategory.Text.Trim() == "T") || (txtProductGroup.Text.Trim() == "N"))
            {
                txtWashCode.Enabled = false;
                txtTintCode.Enabled = false;
                txtWashCode.Text = "";
                txtTintCode.Text = "";
                txtWashDesc.Text = "";
                txtTintDesc.Text = "";
                txtColor.Enabled = true;
            }
            else if ((txtProductCategory.Text.Trim() == "B") && (txtProductGroup.Text.Trim() == "D"))
            {
                txtWashCode.Enabled = true;
                txtTintCode.Enabled = true;
                txtColor.Enabled = false;
                txtColor.Text = "";
                txtColorDesc.Text = "";
            }
            txtItemCode.Text = ItemGenerator(txtItemCode, false);
        }

        protected void txtProductCategory_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback2);
            if (Session["SelComProdcat"] != null)
            {
                ProductCategoryLookUp.SelectCommand = Session["SelComProdcat"].ToString();
                ProductCategoryLookUp.DataBind();
            }

            if (Session["SelComProdsubcat"] != null)
            {
                ProductSubCategoryLookUp.SelectCommand = Session["SelComProdsubcat"].ToString();
                ProductSubCategoryLookUp.DataBind();
            }
        }

        public void gridView_CustomCallback2(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters;
            if (column.Contains("GLP_AIC") || column.Contains("GLP_F") || column.Contains("GLP_AC")) return;//Traps the callback  

            if (column == "prodcat")
            {
                ASPxGridView grid = sender as ASPxGridView;
                ProductCategoryLookUp.SelectCommand = string.Format("SELECT * FROM MasterFile.ProductCategory WHERE ItemCategoryCode = '{0}' and ISNULL(IsInactive,0) = 0", cboItemCategory.Value);
                Session["SelComProdcat"] = ProductCategoryLookUp.SelectCommand;
                grid.DataSourceID = "ProductCategoryLookUp";
                grid.DataBind();
            }
            else if (column == "prodsubcat")
            {
                Boolean blnHaveSubCategory = false;

                DataTable dtDetail = Gears.RetriveData2(string.Format("SELECT * FROM MasterFile.ProductCategory WHERE ProductCategoryCode = '{0}' and ISNULL(IsInactive,0) = 0 ", txtProductCategory.Text), Session["ConnString"].ToString());
                if (dtDetail != null)
                {

                    foreach (DataRow _SOdetail in dtDetail.Rows)
                    {
                        if (!Convert.IsDBNull(_SOdetail["HaveSubCategory"]))
                        {
                            blnHaveSubCategory = Convert.ToBoolean(_SOdetail["HaveSubCategory"]);
                        }
                    }
                }

                ProductSubCategoryLookUp.SelectCommand = " SELECT ProductSubCatCode,Description FROM MasterFile.ProductCategorySub WHERE CHARINDEX(',',ProductCategoryCode) =  0 and ISNULL(IsInactive,0) = 0";

                if (blnHaveSubCategory)
                {
                    ProductSubCategoryLookUp.SelectCommand = string.Format(" SELECT ProductSubCatCode,Description FROM MasterFile.ProductCategorySub WHERE CHARINDEX(',{0},',ProductCategoryCode) != 0 and ISNULL(IsInactive,0) = 0 ", txtProductCategory.Text.Trim());
                }

                ASPxGridView grid = sender as ASPxGridView;
                ProductSubCategoryLookUp.SelectCommand = " SELECT ProductSubCatCode,Description FROM MasterFile.ProductCategorySub WHERE CHARINDEX(',',ProductCategoryCode) =  0 and ISNULL(IsInactive,0) = 0";
                if (blnHaveSubCategory)
                {
                    ProductSubCategoryLookUp.SelectCommand = string.Format(" SELECT ProductSubCatCode,Description FROM MasterFile.ProductCategorySub WHERE CHARINDEX(',{0},',ProductCategoryCode) != 0 and ISNULL(IsInactive,0) = 0 ", txtProductCategory.Text.Trim());
                }
                Session["SelComProdsubcat"] = ProductSubCategoryLookUp.SelectCommand;
                grid.DataSourceID = "ProductSubCategoryLookUp";
                grid.DataBind();
            }
        }

        protected void cboItemCategory_DataBound(object sender, EventArgs e)
        {
            
        }
       
        protected void Loaditems()
        {
            if (cboItemCategory.Value.ToString().Trim() == "1")
                {
                    frmlayout1.FindItemOrGroupByName("expAccessories").ClientVisible = false;
                    txtCustomer.Visible = false;
                    txtMnemonics.Visible = false;
                    frmlayout1.FindItemOrGroupByName("lblCustomer").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("expFabric").ClientVisible = false;
                    txtImportedItem.Enabled = false;

                }
                else
                {
                    txtCustomer.Visible = false;
                    txtMnemonics.Visible = false;
                    frmlayout1.FindItemOrGroupByName("lblCustomer").ClientVisible = false;
                }

                if (cboItemCategory.Value.ToString().Trim() == "2")
                {
                    frmlayout1.FindItemOrGroupByName("expAccessories").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("expStockMaster").ClientVisible = false;
                }
                if (cboItemCategory.Value.ToString().Trim() == "3")
                {
                    frmlayout1.FindItemOrGroupByName("expFabric").ClientVisible = false;
                    frmlayout1.FindItemOrGroupByName("expStockMaster").ClientVisible = false;
                }

                txtProductCategory.Text = "";
                txtProductSubCategory.Text = "";
                txtShortDescription.Text = "";
                txtCustomer.Text = "";
                txtKeySupplier.Text = "";
                string strQuery = string.Format("SELECT * From MasterFile.ItemCategory where ItemCategoryCode='{0}'  and ISNULL(IsInactive,0) = 0", cboItemCategory.Value.ToString().Trim());

                DataTable dtCat = Gears.RetriveData2(strQuery,Session["ConnString"].ToString());
                if (dtCat.Rows.Count <= 0)
                {
                    return;
                }
                string strFormat = dtCat.Rows[0]["ItemCodeFormat"].ToString();

                if (strFormat == "NONE")
                {
                    blnoformat = true;
                }
                else
                {
                    blnoformat = false;
                }

                if (cboItemCategory.Value.ToString().Trim() == "1")
                {
                    frmlayout1.FindItemOrGroupByName("lblKeySupplier").ClientVisible = txtKeySupplier.Visible = false;
                    txtCustomer.Visible = false;
                    txtMnemonics.Visible = false;
                    txtSuppName.Visible = false;
                }
                else
                {
                    txtSuppName.Visible = true;
                    frmlayout1.FindItemOrGroupByName("lblKeySupplier").ClientVisible = txtKeySupplier.Visible = true;
                    txtCustomer.Visible = false;
                    txtMnemonics.Visible = false;
                }

                if (cboItemCategory.Value.ToString().Trim() == "2")
                {
                    frmlayout1.FindItemOrGroupByName("lblDescription").Caption = "Supplier Item Code:";
                    txtCustomer.Visible = false;
                    frmlayout1.FindItemOrGroupByName("lblCustomer").ClientVisible = false;
                    txtMnemonics.Visible = false;
                }
                else
                {
                    frmlayout1.FindItemOrGroupByName("lblDescription").Caption = "Short Description:";
                    txtCustomer.Visible = false;
                    frmlayout1.FindItemOrGroupByName("lblCustomer").ClientVisible = false;
                    txtMnemonics.Visible = false;
                }

                if (cboItemCategory.Value.ToString().Trim() == "3")
                {
                    txtShortDescription.MaxLength = 6;
                }
                else if (Convert.ToBoolean(dtCat.Rows[0]["IsAsset"]))
                {
                    txtShortDescription.MaxLength = 4;
                }
                else
                {
                    txtShortDescription.MaxLength = 30;
                }
                ProductCategoryLookUp.SelectCommand = string.Format("SELECT * FROM MasterFile.ProductCategory WHERE ItemCategoryCode = '{0}' and ISNULL(IsInactive,0) = 0", cboItemCategory.Value);
                Boolean blnHaveSubCategory = false;

                DataTable dtDetail = Gears.RetriveData2(string.Format("SELECT * FROM MasterFile.ProductCategory WHERE ProductCategoryCode = '{0}' and ISNULL(IsInactive,0) = 0 ", txtProductCategory.Text), Session["ConnString"].ToString());
                if (dtDetail != null)
                {

                    foreach (DataRow _SOdetail in dtDetail.Rows)
                    {
                        if (!Convert.IsDBNull(_SOdetail["HaveSubCategory"]))
                        {
                            blnHaveSubCategory = Convert.ToBoolean(_SOdetail["HaveSubCategory"]);
                        }
                    }
                }

                ProductSubCategoryLookUp.SelectCommand = " SELECT ProductSubCatCode,Description FROM MasterFile.ProductCategorySub WHERE CHARINDEX(',',ProductCategoryCode) =  0 and ISNULL(IsInactive,0) = 0";

                if (blnHaveSubCategory)
                {
                    ProductSubCategoryLookUp.SelectCommand = string.Format(" SELECT ProductSubCatCode,Description FROM MasterFile.ProductCategorySub WHERE CHARINDEX(',{0},',ProductCategoryCode) != 0 and ISNULL(IsInactive,0) = 0 ", txtProductCategory.Text.Trim());
                }
                ProductCategoryLookUp.DataBind();
                ProductSubCategoryLookUp.DataBind();

                txtItemCode.Text = ItemGenerator(txtItemCode, false);

            }
       
      
    }
}