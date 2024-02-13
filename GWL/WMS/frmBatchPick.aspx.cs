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
    public partial class frmBatchPick : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.WipIN _Entity = new WipIN();//Calls entity odsHeader
        Entity.WipIN.WISizeBreakDown _EntityDetail = new WipIN.WISizeBreakDown();//Call entity sdsDetail

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


            if (!IsPostBack)
            {

                //Session["atccode"] = null;
                //Session["picklistdetail"] = null;
                //Session["customoutbound"] = null;
                
                //Session["pickfromdate"] = null;
                //Session["picktodate"] = null;

                if (Session["pickfromdate"] != null)
                {
                    dtpdatefrom.Text = Session["pickfromdate"].ToString();
                    
                }

                if (Session["picktodate"] != null)
                {
                    dtpdateto.Text  =  Session["picktodate"].ToString();
                }

                
                if (Session["pickwh"] != null)
                {
                    txtwarehouse.Text = Session["pickwh"].ToString();
                }

                if (Session["pickcustomer"] != null)
                {
                    txtcustomer.Text = Session["pickcustomer"].ToString();
                }

                

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                       
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
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }



                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    gv1.DataBind();
                    popup.ShowOnPageLoad = false;
                    //frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

             
                   
                    //gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv1.Settings.VerticalScrollableHeight = 200;
                    //gv1.KeyFieldName = "LineNumber;PODocNumber";
                }
  
                

                

            }
            else
            {
                Session["pickwh"] = null;
                Session["pickcustomer"] = null;
                Session["picktodate"] = null;
                Session["pickfromdate"] = null; 

            }

            



        }

        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRCWIN";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProcurement.GProcurement.WIPIN_Validate(gparam);
    
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n" ;//Message variable to client side
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
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New)
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
                _Entity.LastEditedBy = Session["userid"].ToString();
                _Entity.LastEditedDate = DateTime.Now.ToString();

              //  _Entity.Transtype = Request.QueryString["transtype"].ToString();

            switch (e.Parameter)
            {
         

                case "Update":
                case "Add":
                //emc*
                gv1.UpdateEdit();
                 GetSelectedVal();

                 

                 Session["pickcustomer"] = txtcustomer.Text;
                 Session["pickwh"] = txtwarehouse.Text;
                 Session["picktodate"] = dtpdateto.Text;
                 Session["pickfromdate"] = dtpdatefrom.Text;

                        cp.JSProperties["cp_message"] = "Successfully Save!";
                      cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                    
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


               
                case "Generate":
                    GetSelectedVal();
                   // GetVat();
                  //  OtherDetail();
                    cp.JSProperties["cp_generated"] = true;
                   
                    //glSupplierCode.Enabled = false;
                    //Generatebtn.Enabled = false;
                    break;

                case "customercode":
                //    sdsPicklist.SelectParameters["customercode"].DefaultValue = aglCustomer.Text;
                    break;

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
            e.Handled = true;
            DataTable PicklistTrans = new DataTable();

            string sql = "SELECT TOP 1 SPACE(100) as Picklistno,   SPACE(8000) as reason, SPACE(8000) as Remarks";

            PicklistTrans = Gears.RetriveData2(sql, Session["ConnString"].ToString());
            PicklistTrans.Rows.Clear();


            //PicklistTrans = Gears.RetriveData2("SELECT TOP(1) DocNumber  as Picklistno,DocDate as Pickdate,WarehouseCode as whcode ,CustomerCode as customer,Creason as reason,cremarks as Remarks "
            //+ " FROM wms.Picklist "
            //+ " WHERE DocDate Between '" + dtpdateto.Text + "' and '" + dtpdateto.Text + "' "
            //+ " AND CustomerCode = '" + txtcustomer.Text + "' "
            //+ " AND WarehouseCode = '" + txtwarehouse.Text + "' ", Session["ConnString"].ToString());

            PicklistTrans.PrimaryKey = new DataColumn[] { PicklistTrans.Columns["Picklistno"] };

            foreach (ASPxDataUpdateValues values in e.UpdateValues)
            {
                PicklistTrans.Rows.Add(new Object[]{
                      values.NewValues["Picklistno"],
                      values.NewValues["reason"],
                      values.NewValues["Remarks"]
      });

                //object[] keys = { values.Keys["Picklistno"] };
                //DataRow row = PicklistTrans.Rows.Find(keys);
                //row["reason"] = values.NewValues["reason"];
                //row["Remarks"] = values.NewValues["Remarks"];

            }
            foreach (DataRow dtRow in PicklistTrans.Rows)
            {
                DataTable dtMM = Gears.RetriveData2("UPDATE wms.Picklist set cRemarks='" + dtRow["Remarks"] + "',Creason='" + dtRow["reason"] + "'"
                                                   + " where Docnumber='" + dtRow["Picklistno"] + "' ", Session["ConnString"].ToString());


                //update wms.Picklist set field1 = 'x',Remarks = 'y' where DocNumber = 'aaa'
            }

                

            
        }
        #endregion

     



      
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["picklistdetail"] = null;
            }

            if (Session["picklistdetail"] != null)
            {
                gv1.DataSourceID = sdsServiceOrderdetail.ID;
                sdsServiceOrderdetail.FilterExpression = Session["picklistdetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        private DataTable GetSelectedVal()
        {
            //emc*
            gv1.DataSourceID  = null;

            //DataTable PicklistTrans = Gears.RetriveData2("SELECT DocNumber  as Picklistno,DocDate as Pickdate,WarehouseCode as whcode ,CustomerCode as customer,Creason as reason,cremarks as Remarks "
            //+ " FROM wms.Picklist "
            //+ " WHERE DocDate Between '" + dtpdateto.Text + "' and '" + dtpdateto.Text + "' "
            //+ " AND CustomerCode = '" + txtcustomer.Text + "' "
            //+ " AND WarehouseCode = '" + txtwarehouse.Text + "' ", Session["ConnString"].ToString());

           // EXEC [dbo].[sp_picklist_batchencode] '12/1/2016','12/1/2016','MJI','MGDCAV'

            DataTable PicklistTrans = Gears.RetriveData2("EXEC [dbo].[sp_picklist_batchencode] "
                        + " '" + dtpdatefrom.Text + "', "
                        + " '" + dtpdateto.Text + "', "
                        + " '" + txtcustomer.Text + "', "
                        + " '" + txtwarehouse.Text + "' ", Session["ConnString"].ToString());

            gv1.DataSource = PicklistTrans;
            gv1.DataBind();

            txttotalpicklist.Text = PicklistTrans.Rows.Count.ToString();

            return PicklistTrans;
        }



        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpdateto.Date = DateTime.Now;
                dtpdatefrom.Date = DateTime.Now;
            }
        }


        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
               
        }

        
        
    }
}