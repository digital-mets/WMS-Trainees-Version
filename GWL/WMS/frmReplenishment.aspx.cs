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
    public partial class frmReplenishment : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Replenishment _Entity = new Replenishment();//Calls entity Replenishment
        Entity.Replenishment.ReplenishmentDetail _EntityDetail = new Replenishment.ReplenishmentDetail();
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
            //gv1.DataSource = null;
            gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            dtpdocdate.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));
       

            if (!IsPostBack)
            {
                Session["repdetail"] = null;
                Session["FilterExpression"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";

                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }

                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //gv1.DataSourceID = "sdsDetail";

                    //popup.ShowOnPageLoad = false;
                }
               
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());

                //txtHField1.Text = _Entity.Field1;
                //txtHField2.Text = _Entity.Field2;
                //txtHField3.Text = _Entity.Field3;
                //txtHField4.Text = _Entity.Field4;
                //txtHField5.Text = _Entity.Field5;
                //txtHField6.Text = _Entity.Field6;
                //txtHField7.Text = _Entity.Field7;
                //txtHField8.Text = _Entity.Field8;
                //txtHField9.Text = _Entity.Field9;
                dtpdocdate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? " ": Convert.ToDateTime(_Entity.DocDate).ToShortDateString();

                txtMinWeight.Value =String.IsNullOrEmpty(_Entity.MinWeight.ToString()) ? 0 : _Entity.MinWeight;
                txtMaxWeight.Value = String.IsNullOrEmpty(_Entity.MaxWeight.ToString()) ? 0 : _Entity.MaxWeight;
                txtCurWeight.Value = String.IsNullOrEmpty(_Entity.CurrentWeight.ToString()) ? 0 : _Entity.CurrentWeight;
                txtRemWeight.Value = String.IsNullOrEmpty(_Entity.RemainingWeight.ToString()) ? 0 : _Entity.RemainingWeight;
                glStorerKey.Value =String.IsNullOrEmpty(_Entity.CustomerCode) ? "" : _Entity.CustomerCode;
                glWarehousecode.Value = String.IsNullOrEmpty(_Entity.WarehouseCode) ? "" : _Entity.WarehouseCode;
                gLoc.Value = String.IsNullOrEmpty(_Entity.Location) ? "" : _Entity.Location;
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? " ": Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text =String.IsNullOrEmpty(_Entity.LastEditedDate) ? " ": Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = String.IsNullOrEmpty(_Entity.SubmittedDate) ? " ": Convert.ToDateTime(_Entity.SubmittedDate).ToShortDateString();



                Masterfileitem.SelectCommand = "select distinct ISNULL(a.ItemCode,' ') as ItemCode,ISNULL(b.FullDesc,' ') as FullDesc from wms.CountSheetSetup a left join Masterfile.Item b on a.ItemCode = b.ItemCode where isnull(b.IsInactive,'')=0 and a.WarehouseCode = '" + glWarehousecode.Text + "' and a.Location = '" + gLoc.Text + "'";

                DataTable checkCount = Gears.RetriveData2("SELECT DocNumber FROM WMS.Replenishmentdetail WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

                   if (checkCount.Rows.Count > 0)
                {


                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    //sdsDetail.SelectParameters["DocNumber"].DefaultValue =
                    //          gv1.DataBind();
                    gv1.DataSourceID = "sdsDetail";
                  

                }
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
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
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
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V")
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

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            ASPxGridView LocList = sender as ASPxGridView;
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
           

          if (e.Parameters.Contains("LocationCodeDropDown"))
            { 
                string itemcode = e.Parameters.Split('|')[1];//Set Item Code
                Masterfileloc.SelectCommand = "select distinct Location from wms.CountSheetSetup where ItemCode = '"+itemcode+"' and ISNULL(Location,'')!='' ";
                Session["locsql"] = Masterfileloc.SelectCommand;
                //LocList.DataSource = Masterfileloc;
                LocList.DataBind();

            }
          else if (e.Parameters.Contains("ItemCodeDropDown")) {
              string location = e.Parameters.Split('|')[2];//Set loc Code
               string ware = e.Parameters.Split('|')[1];//Set warehouse Code
               Masterfileitem.SelectCommand = "select distinct ISNULL(a.ItemCode,' ') as ItemCode,ISNULL(b.FullDesc,' ') as FullDesc from wms.CountSheetSetup a left join Masterfile.Item b on a.ItemCode = b.ItemCode where isnull(b.IsInactive,'') = 0 and a.WarehouseCode = '" + ware + "' and a.Location = '" + location + "'";
               Session["itemsql"] = Masterfileitem.SelectCommand;
               Masterfileitem.DataBind();
          
          }
            
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gv1")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                if (Session["locsql"] != null)
                {
                    Masterfileloc.SelectCommand = Session["locsql"].ToString();
                    Masterfileloc.DataBind();
                }
                if (Session["itemsql"] != null) {
                    Masterfileitem.SelectCommand = Session["itemsql"].ToString();
                    Masterfileitem.DataBind();
                
                }
                //gridLookup.GridView.DataSourceID = Masterfileitem.ID; 
                //gridLookup.GridView.DataSource = Masterfileitem;
            }
          
        }
      
    
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            if(e.Parameter != "Loc"){
            Gears.UseConnectionString(Session["ConnString"].ToString());

            _Entity.Location = String.IsNullOrEmpty(gLoc.Text) ? "" : gLoc.Text;
            _Entity.WarehouseCode = String.IsNullOrEmpty(glWarehousecode.Text) ? "" : glWarehousecode.Text;
            _Entity.CustomerCode = String.IsNullOrEmpty(glStorerKey.Text) ? "" : glStorerKey.Text;
            _Entity.MinWeight = Convert.ToDecimal(txtMinWeight.Value);
            _Entity.CurrentWeight = Convert.ToDecimal(txtCurWeight.Value);
            decimal rem = Convert.ToDecimal(txtMinWeight.Value) > Convert.ToDecimal(txtCurWeight.Value) ? Convert.ToDecimal(Convert.ToDecimal(txtMinWeight.Value) - Convert.ToDecimal(txtCurWeight.Value)) : Convert.ToDecimal(Convert.ToDecimal(txtCurWeight.Value) - Convert.ToDecimal(txtMinWeight.Value));
            _Entity.RemainingWeight = rem;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = txtHAddedDate.ToString();
            _Entity.DocDate = dtpdocdate.Text;
            _Entity.LastEditedBy = txtHLastEditedBy.Text;
            _Entity.LastEditedDate = txtHLastEditedDate.Text;
           }
            switch (e.Parameter)
            {
                case "Add":

                    if (error == false)
                    {
                        check = true;
                        //_Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.InsertData(_Entity);
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

                    if (error == false)
                    {
                        check = true;
                  
                        _Entity.UpdateData(_Entity);//Method of Updating header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = Request.QueryString["docnumber"].ToString();//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                        Session["Datatable"] = null;
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

                case "Loc":
                    if((glStorerKey.Text != null || glStorerKey.Text != "") && (glWarehousecode.Text != null || glWarehousecode.Text != "") ){
                        loc.SelectCommand = "select * from Masterfile.location where WarehouseCode = '" + glWarehousecode.Text + "' and CustomerCode = '" + glStorerKey.Text + "' and ISNULL(Replenish,0) != 0";
                        //LocList.DataSource = Masterfileloc;
                        loc.DataBind();
                      }
                    cp.JSProperties["cp_delete"] = "Loc";
                    break;
              
            }
        }



        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            foreach (GridViewColumn column in gv1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if ((e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "") && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "LocationCode" && dataColumn.FieldName != "ItemCode")
                {
                    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                }
            }

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
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["locdetail"] = null;
            }

        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._Connection = Session["ConnString"].ToString();
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSREPL";

            string strresult = Gears.RetriveData2("EXEC [sp_validate_Replenishment] '" + gparam._DocNo + "','" + gparam._TransType+ "'", gparam._Connection).ToString();//ADD CONN


            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion
        protected void Connection_Init(object sender, EventArgs e)
        {
            //Session["userid"] = "1828";
            //Session["ConnString"] = "Data Source=192.168.201.115;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;connection timeout=1800;";
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            // sdsDetail.ConnectionString = Session["ConnString"].ToString();
            // Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            // Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //Warehouse.ConnectionString = Session["ConnString"].ToString();
            //    ItemAdjustment.ConnectionString = Session["ConnString"].ToString();
            // Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            // Unit.ConnectionString = Session["ConnString"].ToString();
            //     UnitBase.ConnectionString = Session["ConnString"].ToString();
            //     StoragesType.ConnectionString = Session["ConnString"].ToString();
            //     sdsLocation.ConnectionString = Session["ConnString"].ToString();
            //     StorerKey.ConnectionString = Session["ConnString"].ToString();
        }

        protected void gvExtract_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //switch (e.Parameters)
            //{
            //    case "Pal":
            //        if (string.IsNullOrEmpty(txtCustomer.Text)
            //            //&& string.IsNullOrEmpty(txtItem.Text) &&
            //            //string.IsNullOrEmpty(txtLocation.Text) && 
            //            &&string.IsNullOrEmpty(txtPalletID.Text))
            //        {
            //            gvExtract.JSProperties["cp_error"] = "No input to search for!";
            //        }
            //        else
            //        {
            //            GetSelectedVal();
            //            gvExtract.Selection.SelectAll();
            //        }
            //        break;
            //}
        }

       

        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(glStorerKey.Text))
                lst_operator.Add(new BinaryOperator("Customer", string.Format("{0}", glStorerKey.Text.Trim()), BinaryOperatorType.Equal));

            //if (!string.IsNullOrEmpty(txtItem.Text))
            //    lst_operator.Add(new BinaryOperator("A.ItemCode", string.Format("%{0}%", txtItem.Text.Trim()), BinaryOperatorType.Like));


            ////if (!string.IsNullOrEmpty(txtRR.Text))
            ////    lst_operator.Add(new BinaryOperator("inb.Docnumber", string.Format("%{0}%", txtRR.Text.Trim()), BinaryOperatorType.Like));


            //if (!string.IsNullOrEmpty(txtRR.Text))
            //    lst_operator.Add(new BinaryOperator("TransDoc", string.Format("%{0}%", txtRR.Text.Trim()), BinaryOperatorType.Like));
            ////if (!string.IsNullOrEmpty(txtLot.Text))
            //////    lst_operator.Add(new BinaryOperator("LotId", string.Format("%{0}%", txtLot.Text), BinaryOperatorType.Like));
            //if (!string.IsNullOrEmpty(glWarehouseCode.Text))
            //    lst_operator.Add(new BinaryOperator("A.WarehouseCode", string.Format("%{0}%", glWarehouseCode.Text.Trim()), BinaryOperatorType.Like));

            //if (!string.IsNullOrEmpty(txtLocation.Text))
            //    lst_operator.Add(new BinaryOperator("Location", string.Format("%{0}%", txtLocation.Text.Trim()), BinaryOperatorType.Like));

            //if (!string.IsNullOrEmpty(txtPalletID.Text))
            //    lst_operator.Add(new BinaryOperator("PalletID", string.Format("%{0}%", txtPalletID.Text.Trim()), BinaryOperatorType.Like));

            if (lst_operator.Count > 0)
            {
                CriteriaOperator[] op = new CriteriaOperator[lst_operator.Count];
                for (int i = 0; i < lst_operator.Count; i++)
                    op[i] = lst_operator[i];
                CriteriaOperator res_operator = new GroupOperator(GroupOperatorType.And, op);
                res_str = res_operator.ToString();
            }

            return res_str;
        }


        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
             
            }
            
        }
        protected void dtpdocdate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {

                dtpdocdate.Date = DateTime.Now;
            }
        }


    }
}