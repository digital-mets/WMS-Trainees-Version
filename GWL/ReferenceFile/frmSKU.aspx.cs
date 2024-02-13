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


namespace GWL
{
    public partial class frmSKU : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string ItemCat = "";
        string ProdCat = "";
        string SubCat = "";

        Entity.SKU _Entity = new SKU();//Calls entity ICN

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

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

            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {


                //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    spWeight.Value = 0;
                    spBatchWeight.Value = 0;
                    spYieldPercent.Value = 0;
                    spYieldedBatchWeight.Value = 0;
                    // gv1.DataSourceID = "sdsDetail";
                    gvClient.DataSourceID = "sdsDetail4";
                    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                }
                else
                {
                    gvClient.DataSourceID = "odsDetail4";
                    txtSKUCode.ReadOnly = true;
                    txtSKUCode.Value = Request.QueryString["docnumber"].ToString();


                    _Entity.getdata(txtSKUCode.Text, Session["ConnString"].ToString());

                    txtSKUCode.Text = _Entity.SKUCode;
                    txtProductName.Text = _Entity.ProductName;
                    glProductCategory.Value = _Entity.ProductCategory;
                    glSKU.Value = _Entity.SKUType;
                    spWeight.Value = _Entity.Weight;
                    glUnit.Value = _Entity.Unit;
                    spPiece.Value = _Entity.Pieces;
                    Quantity.Value = _Entity.Quantity;
                    spBatchWeight.Value = _Entity.BatchWeight;
                    spYieldPercent.Value = _Entity.YieldPercentage;
                    spYieldedBatchWeight.Value = _Entity.YieldedBatchWeight;
                    cbIsSmallSKU.Checked = _Entity.IsSmallSKU;
                    cbWithCheese.Checked = _Entity.WithCheese;
                    txtBackColor.Text = _Entity.BackColor;
                    glPackagingType.Value = _Entity.PackagingType;
                    txtSAPCode.Text = _Entity.SAPCode;
                    txtSAPDescription.Text = _Entity.SAPDescription;

                    txtHField1.Value = _Entity.Field1;
                    txtHField2.Value = _Entity.Field2;
                    txtHField3.Value = _Entity.Field3;
                    txtHField4.Value = _Entity.Field4;
                    txtHField5.Value = _Entity.Field5;
                    txtHField6.Value = _Entity.Field6;
                    txtHField7.Value = _Entity.Field7;
                    txtHField8.Value = _Entity.Field8;
                    txtHField9.Value = _Entity.Field9;


                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtDeactivatedBy.Text = _Entity.DeActivatedBy;
                    txtDeactivatedDate.Text = _Entity.DeActivatedDate;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;

                    DataTable checkCount2 = Gears.RetriveData2(" select SKUCode from Masterfile.FGSKUDetailCooking where SKUCode = '" + txtSKUCode.Text + "'", Session["ConnString"].ToString());//ADD CONN

                    if (checkCount2.Rows.Count > 0)
                    {
                        odsDetail4.SelectParameters["SKUCode"].DefaultValue = txtSKUCode.Text;
                        gvClient.DataSourceID = "odsDetail4";
                    }
                    else
                    {
                        gvClient.DataSourceID = "sdsDetail4";
                    }
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        //    gv2.Visible = false;
                        
                        glProductCategory.Value = "FG";
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                      
                        updateBtn.Text = "Update";
                        txtSKUCode.ReadOnly = true;
                        break;
                    case "V":
                       
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        
                        spBatchWeight.ReadOnly = true;
                        spYieldPercent.ReadOnly = true;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }


                //if (Session["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
                //  {
                //      gv1.DataSourceID = "sdsDetail";

                //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //   }
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }

        protected void ColorEditLoad(object sender, EventArgs e)//Control for all ColorEdit
        {
            ASPxColorEdit text = sender as ASPxColorEdit;
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

        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
            spinedit.AllowMouseWheel = false;

            spinedit.SpinButtons.ShowIncrementButtons = false;
            
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
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN

            DataTable skucode = Gears.RetriveData2("SELECT SKUCode FROM MasterFile.FGSKU WHERE SKUCode = '" + txtSKUCode.Text + "'", Session["ConnString"].ToString());
            if (skucode.Rows.Count > 0 && updateBtn.Text == "Add")
            {
                cp.JSProperties["cp_message"] = "SKU Code:'" + txtSKUCode.Text + "' already Exist!";
                cp.JSProperties["cp_success"] = true;
                return;
            }

            DataTable itemcode = Gears.RetriveData2("SELECT ItemCode FROM MasterFile.Item WHERE ItemCode = '" + txtSKUCode.Text + "'", Session["ConnString"].ToString());
            //if (itemcode.Rows.Count > 0 && updateBtn.Text == "Add")
            //{
            //    cp.JSProperties["cp_message"] = "Item Code:'" + txtSKUCode.Text + "' already Exist!";
            //    cp.JSProperties["cp_success"] = true;
            //    return;
            //}

            _Entity.SKUCode = txtSKUCode.Text;
            _Entity.ProductName = txtProductName.Text;
            _Entity.ProductCategory = glProductCategory.Text;
            _Entity.SKUType = glSKU.Text;
            _Entity.Weight = Convert.ToDecimal(spWeight.Text);
            _Entity.Unit = glUnit.Text;
            _Entity.Pieces = Convert.ToInt32(spPiece.Text);
            _Entity.Quantity = Convert.ToDecimal(Quantity.Text);
            _Entity.BatchWeight = Convert.ToDecimal(spBatchWeight.Text);
            _Entity.YieldPercentage = Convert.ToDecimal(spYieldPercent.Text);
            _Entity.YieldedBatchWeight = Convert.ToDecimal(spYieldedBatchWeight.Text);
            _Entity.IsSmallSKU = cbIsSmallSKU.Checked;
            _Entity.WithCheese = cbWithCheese.Checked;
            _Entity.BackColor = txtBackColor.Text;
            _Entity.PackagingType = glPackagingType.Text;
            _Entity.SAPCode = txtSAPCode.Text;
            _Entity.SAPDescription = txtSAPDescription.Text;
            //_Entity.ColorReferenceCode = txtcolorreferencecode.Text;

            //_Entity.ColorIsInactive = Convert.ToBoolean(chkisinactive.Value);
            
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeActivatedBy = txtDeactivatedBy.Text;
            _Entity.DeActivatedDate = txtDeactivatedDate.Text;
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.LastEditedBy = txtLastEditedBy.Text;
            _Entity.LastEditedDate = txtLastEditedDate.Text;



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
            _Entity.AddedBy = Session["userid"].ToString();
            switch (e.Parameter)
            {
                case "Add":


                    gvClient.UpdateEdit();
                    check = true;
                    _Entity.InsertData(_Entity);//Method of inserting for header
                    _Entity.InsertItemData(_Entity);//Method to insert data to Item Masterfile
                    
                    gvClient.DataSourceID = "odsDetail4";
                    odsDetail4.SelectParameters["SKUCode"].DefaultValue = txtSKUCode.Text;
                        gvClient.UpdateEdit();

                    Validate();
                    cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side

                    cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    Session["Refresh"] = "1";


                    break;

                case "Update":
                    gvClient.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        _Entity.UpdateItemData(_Entity);//Method to update data to Item Masterfile

                        gvClient.DataSourceID = "odsDetail4";
                        odsDetail4.SelectParameters["SKUCode"].DefaultValue = txtSKUCode.Text;
                        gvClient.UpdateEdit();
                        // odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error

                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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

        //to convert string seperated using ; to ,
        private string convertToSPC(string value)
        {
            string temp = ",";
            string[] strarr = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < strarr.Length; i++)
            {
                temp += strarr[i].ToString() + ",";
            }

            return temp;
        }
        //to convert string seperated using , to ;
        private string convertToGVF(string value)
        {
            string temp = ";";
            string[] strarr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < strarr.Length; i++)
            {
                temp += strarr[i].ToString() + ";";
            }

            return temp;
        }
        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            /*sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            Warehouse.ConnectionString = Session["ConnString"].ToString();
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            ItemAdjustment.ConnectionString = Session["ConnString"].ToString();
            ColorGroupLookUp.ConnectionString = Session["ConnString"].ToString();
            ItemCategoryCodeLookup.ConnectionString = Session["ConnString"].ToString();
            ProductCategoryLookUp.ConnectionString = Session["ConnString"].ToString();
            ProductSubCategoryLookUp.ConnectionString = Session["ConnString"].ToString();*/
        }

        protected void gvClient_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            //e.NewValues["FromUnit"] = txtbaseunit.Value;
        }

        protected void spYieldedBatchWeight_NumberChanged(object sender, EventArgs e)
        {
            spYieldedBatchWeight.DisplayFormatString = "N4";
        }

      

        //private string RemoveSpaces(string str)
        //{
        //    string result = "";
        //    char[] remover = str.ToCharArray();

        //    for (int i = 0; i < remover.Length; i++)
        //    {
        //        if (remover[i] != ' ')
        //        {
        //            result = string.Concat(result, remover[i]);
        //        }

        //    }

        //    return result;
        //}
    }
}