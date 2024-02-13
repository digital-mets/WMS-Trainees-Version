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
using System.Windows.Forms;

namespace GWL
{
    //-- =============================================
    //-- Author:		Renato Anciado
    //-- CREATION  DATE: 03/02/2015
    //-- Description:	Item Relocation Module
    //-- =============================================
    //--2015-12-11  KMM Filter Location
    //--2016-01-04  ES  Add function for auto calculate qty
    public partial class frmItemRelocation : System.Web.UI.Page
    {

        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.ItemRelocation _Entity = new ItemRelocation();//Calls entity ICN
        Entity.ItemRelocation.ItemRelocationDetail _EntityDetail = new ItemRelocation.ItemRelocationDetail();//Call entity POdetails

        private static DataTable dtloc;

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            Location.SelectCommand = "SELECT LocationCode,LocationDescription FROM Masterfile.Location where ISNULL(IsInactive,0)=0 and ISNULL(OnHandBaseQty,0) > 0 and WarehouseCode = '" + glWarehouseCode.Text + "'";
            ToLocation.SelectCommand = "SELECT LocationCode,LocationDescription FROM Masterfile.Location where ISNULL(IsInactive,0)=0 and WarehouseCode = '" + glWarehouseCode.Text + "'";

            if (!string.IsNullOrEmpty(cmbStorerKey.Text))
            {
                Session["FilterCus"] = cmbStorerKey.Text;
            }

            if (!string.IsNullOrEmpty(glWarehouseCode.Text))
            {
                Session["FilterWH"] = glWarehouseCode.Text;
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

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Session["itemreloctab"] = null;
                Session["FilterWH"] = null;
                Session["FilterCus"] = null;
                Session["FilterExpressionitemrel"] = null;
                Session["Datatable"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Update";
                        break;
                    case "E":
                        updateBtn.Text = "Update";

                        break;
                    case "V":
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                if (Request.QueryString["parameters"].ToString() == "N")
                {
                    txtRelocationType.Items.Add("Normal Relocation");
                    txtRelocationType.SelectedIndex = 0;
                }
                else
                {
                    ASPxLabel.Text = "Item Reservation";
                    txtRelocationType.Items.Add("Reserved By Scatter");
                    txtRelocationType.Items.Add("Reserved By Weight");
                    txtRelocationType.Items.Add("Unreserved");

                }

                //2015-12-23    JDSD    Comment DataSource
                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //    
                //    //gv1.DataSourceID = "sdsDetail";
                //    popup.ShowOnPageLoad = false;
                //}
                //else
                //{
                    
                    //gv1.DataSourceID = "odsDetail";
                    _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());

                    deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    glWarehouseCode.Value = _Entity.WarehouseCode;
                    Session["FilterWH"] = glWarehouseCode.Text;
                    txtStorageType.Text = _Entity.StorageType;
                    txtRelocationType.Text = _Entity.RelocationType;
                    cmbStorerKey.Value = _Entity.CustomerCode;
                    Session["FilterCus"] = cmbStorerKey.Text;
                    //txtStorageSection.Text = _Entity.StorageSection;
                    txtRemarks.Text = _Entity.Remarks;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;


              //  }
                //2015-12-23    JDSD    Comment DataSource
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}
                    DataTable dtblDetail = Gears.RetriveData2("SELECT Docnumber from WMS.ItemRelocationDetail where Docnumber ='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());
               
                    gv1.DataSourceID =  (dtblDetail.Rows.Count>0 ? "odsDetail":"sdsDetail");
                    //var data = this.odsDetail.Select();
                    //DataView dtv = data as DataView;
                    //dtloc = dtv.ToTable();

                    //gv1.DataBind();
                    
                     //else
                     //{
                     //    gv1.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode;PalletID;Qty;FromLoc;ToLoc";
                     //}
            }

            if (Session["itemreloctab"] == null)
            {
                string sb = "select b.TransDoc DocNumber,ROW_NUMBER() OVER (ORDER BY b.TransDoc,b.TransLine,b.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode,b.PalletID) as LineNumber,b.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode," +
                            " b.PalletID,b.PalletID ToPalletID," +
                            " SUM(ISNULL(b.RemainingBulkQty,0)-ISNULL(b.PickedBulkQty,0)-ISNULL(ReservedBulkQty,0)) BulkQty," +
                            " SUM(ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0)) Qty," +
                            " b.Location as FromLoc,'' ToLoc,'' StatusCode, Customer,'' LotID,d.WarehouseCode,UnitBase,UnitBulk," +
                            " MfgDate,ExpirationDate,BatchNumber from wms.CountSheetSetup b" +
                            " with (nolock)" +
                            " left join masterfile.Item c" +
                            " on b.ItemCode = c.ItemCode" +
                            " left join masterfile.Location d" +
                            " on b.Location = d.LocationCode" +
                            " where isnull(b.PutAwayDate,'')!=''" +
                            " and isnull(b.submitteddate,'')!=''" +
                            " and ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0) > 0" +
                            " and ISNULL(b.RemainingBulkQty,0)-ISNULL(b.PickedBulkQty,0)-ISNULL(ReservedBulkQty,0) > 0" +
                            " group by BatchNumber,MfgDate,ExpirationDate,b.Transdoc,b.TransLine,Customer,b.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode,b.Location,b.PalletID,d.WarehouseCode,UnitBase,UnitBulk" +
                            " order by b.Transdoc,b.TransLine,b.palletid";
                DataTable countsheet = Gears.RetriveData2(sb, Session["ConnString"].ToString());
                Session["itemreloctab"] = countsheet;
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();

            //here
            string strresult = GWarehouseManagement.ItemRelocation_Validate(gparam);

            gv1.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion


        #region Set controls' state/behavior/etc...
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
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
            if (Request.QueryString["entry"].ToString() == "V")
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
            //if (Request.QueryString["entry"] == "N")
            //{
            //    if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}
        }
        #endregion

        #region Lookup Settings
      
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpressionitemrel"] != null)
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpressionitemrel"].ToString();
                //Session["FilterExpression"] = null;
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            string val2 = "";
            try
            {
                val2 = e.Parameters.Split('|')[3];
            }
            catch (Exception)
            {
                val2 = "";
            }
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string qty = "";
            decimal totalqty = 0;
            string codes = "";

            if (val2 == "null")
            {
                val2 = "0";
            }

            if (e.Parameters.Contains("ItemCode"))
            {
                DataTable getall = Gears.RetriveData2("Select top 1 ColorCode,ClassCode,SizeCode,FullDesc,isnull(StandardQty,0) as StandardQty from masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "' " +
                                                          "and isnull(a.IsInactive,0)=0", Session["ConnString"].ToString());
                foreach (DataRow dt in getall.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    qty = dt["StandardQty"].ToString();
                    qty = string.IsNullOrEmpty(qty) ? "0" : qty;
                }
                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val2));
                    codes += totalqty + ";";
                }

                if (codes == "")
                {
                    codes = "N/A;N/A;N/A;N/A;"+totalqty;
                }

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("BulkQty"))
            {
                DataTable getqty = Gears.RetriveData2("Select isnull(StandardQty,0) as StandardQty from masterfile.item where itemcode = '" + itemcode + "'", Session["ConnString"].ToString());
                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["StandardQty"].ToString();
                    }
                }
                qty = String.IsNullOrEmpty(qty) ? "0" : qty;
                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val));
                }

                itemlookup.JSProperties["cp_codes"] = totalqty + ";";
            }
            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpressionitemrel"] = Masterfileitemdetail.FilterExpression;
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
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

            switch (e.Parameter)
            {
                case "Add":
                    //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    //if (error == false)
                    //{
                    //    check = true;
                    //    //_Entity.InsertData(_Entity);//Method of inserting for header
                    //    _Entity.UpdateData(_Entity);
                    //    _Entity.AddedBy = Session["userid"].ToString();
                    //    gv1.DataSourceID = "odsDetail";
                    //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    //    gv1.UpdateEdit();//2nd initiation to insert grid
                    //    Validate();
                    //    cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                    //    cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                    //    cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    //    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

                    //break;
                
                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;
                
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
                case "refgrid":
                    gv1.DataBind();
                    break;
                case "wh":
                    ToLocation.DataBind();
                    Location.DataBind();
                    //gv1.DataBind();
                    //BindingSource bs = (BindingSource)gv1.DataSource; // Se convierte el DataSource 
                    //DataTable tCxC = (DataTable)bs.DataSource;
                    //Session["testdb"] = tCxC;
                    //gv1.UpdateEdit();
                    break;

                
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
                e.DeleteValues.Clear();
                //e.InsertValues.Clear();
                //e.UpdateValues.Clear();
                
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = new DataTable();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        //, values.Keys[2], values.Keys[3],
                        //                values.Keys[4],values.Keys[5],values.Keys[6],values.Keys[7],
                        //                values.Keys[8],
                        object[] keys = { values.Keys[0], values.Keys[1], values.Keys[2] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = i;
                    var DocNumber = values.NewValues["DocNumber"] == null ? "" : values.NewValues["DocNumber"];
                    var ItemCode = values.NewValues["ItemCode"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var PalletID = values.NewValues["PalletID"] == null ? "" : values.NewValues["PalletID"];
                    var ToPalletID = values.NewValues["ToPalletID"] == null ? "" : values.NewValues["ToPalletID"];
                    var Qty = values.NewValues["Qty"] == null ? "0" : values.NewValues["Qty"];
                    var BulkQty = values.NewValues["BulkQty"] == null ? "0" : values.NewValues["BulkQty"];
                    var FromLoc = values.NewValues["FromLoc"] == null ? "" : values.NewValues["FromLoc"];
                    var ToLoc = values.NewValues["ToLoc"] == null ? "" : values.NewValues["ToLoc"];
                    var StatusCode = values.NewValues["StatusCode"] == null ? "" : values.NewValues["StatusCode"];
                    var BatchNumber = values.NewValues["BatchNumber"] == null ? "" : values.NewValues["BatchNumber"];
                    var Field1 = values.NewValues["Field1"] == null ? "" : values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"] == null ? "" : values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"] == null ? "" : values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"] == null ? "" : values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"] == null ? "" : values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"] == null ? "" : values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"] == null ? "" : values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"] == null ? "" : values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"] == null ? "" : values.NewValues["Field9"];

                    source.Rows.Add(DocNumber, LineNumber, ItemCode, ColorCode, ClassCode, SizeCode, PalletID,
                        ToPalletID, BulkQty, Qty, FromLoc, ToLoc, StatusCode, BatchNumber, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);

                    i++;
                    //if (string.IsNullOrEmpty(OrderQty.ToString()))
                    //{
                    //    OrderQty = 0;
                    //}
                }
                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"], values.NewValues["PalletID"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["PalletID"] = values.NewValues["PalletID"];
                    row["ToPalletID"] = values.NewValues["ToPalletID"];
                    row["BulkQty"] = values.NewValues["BulkQty"];
                    row["Qty"] = values.NewValues["Qty"];
                    row["FromLoc"] = values.NewValues["FromLoc"];
                    row["ToLoc"] = values.NewValues["ToLoc"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
                    row["BatchNumber"] = values.NewValues["BatchNumber"];
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

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.LineNumber = dtRow["LineNumber"].ToString();
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.PalletID = dtRow["PalletID"].ToString();
                    _EntityDetail.ToPalletID = dtRow["ToPalletID"].ToString();
                    _EntityDetail.BulkQty = Convert.ToDecimal(dtRow["BulkQty"].ToString());
                    _EntityDetail.Qty = Convert.ToDecimal(dtRow["Qty"].ToString());
                    _EntityDetail.FromLoc = dtRow["FromLoc"].ToString();
                    _EntityDetail.ToLoc = dtRow["ToLoc"].ToString();
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.BatchNumber = dtRow["BatchNumber"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();

                    _EntityDetail.AddItemRelocationDetail(_EntityDetail);

                    //_EntityDetail.DeleteOutboundDetail(_EntityDetail);
                }
            }
        }

        private void GetSelectedVal()
        {
            gvExtract.DataSource = null;
            string f_expression = GetFilterExpression();
            DataTable dt = Session["itemreloctab"] as DataTable;
            DataView dv = new DataView(dt);
            dv.RowFilter = f_expression.ToUpper().Trim();
            //dv.RowFilter = "[CUSTOMER] LIKE '%C-M011%'";
            gvExtract.DataSource = dv;
            gvExtract.DataBind();
            
        }

        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(txtCustomer.Text))
                lst_operator.Add(new BinaryOperator("Customer", string.Format("%{0}%", txtCustomer.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtItem.Text))
                lst_operator.Add(new BinaryOperator("ItemCode", string.Format("%{0}%", txtItem.Text.Trim()), BinaryOperatorType.Like));

            //if (!string.IsNullOrEmpty(txtLot.Text))
            //    lst_operator.Add(new BinaryOperator("LotId", string.Format("%{0}%", txtLot.Text), BinaryOperatorType.Like));
            if (!string.IsNullOrEmpty(glWarehouseCode.Text))
                lst_operator.Add(new BinaryOperator("WarehouseCode", string.Format("%{0}%", glWarehouseCode.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtLocation.Text))
                lst_operator.Add(new BinaryOperator("FromLoc", string.Format("%{0}%", txtLocation.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtPalletID.Text))
                lst_operator.Add(new BinaryOperator("PalletID", string.Format("%{0}%", txtPalletID.Text.Trim()), BinaryOperatorType.Like));

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
        #endregion

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {
            //       //This is the datasource where we will get the connection string
            //     SqlDataSource ds = OCN;
            //     //This is the sql command that we will initiate to find the data that we want to set to the textbox.
            //// (the e.Parameter is the callback item that we sent to the server)
            //     ds.SelectCommand = string.Format("SELECT OutgoingDocType FROM WMS.[ICN] WHERE DocNumber = '" + e.Parameter + "'");
            // //This is where we now initiate the command and get the data from it using dataview	
            //     DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //     if (biz.Count > 0)
            //     {
            // //Now, this is the part where we assign the following data to the textbox
            //         //txttype.Text = biz[0][0].ToString();
            //     }
        }
        protected void dtpdocdate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {

                //deDocDate.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            //Session["userid"] = "1828";
            //Session["ConnString"] = "Data Source=192.168.201.115;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;connection timeout=1800;";
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //Warehouse.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            //StorageType.ConnectionString = Session["ConnString"].ToString();
            //OCN.ConnectionString = Session["ConnString"].ToString();
            //Location.ConnectionString = Session["ConnString"].ToString();
            //ToLocation.ConnectionString = Session["ConnString"].ToString();
            //Unit.ConnectionString = Session["ConnString"].ToString();
            //StorerKey.ConnectionString = Session["ConnString"].ToString();
            //Palletsql.ConnectionString = Session["ConnString"].ToString();

        }

        //Genrev 12/11/2015 Added codes to filter itemcode by customer
        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
            FilterItem();
            gridLookup.DataBind();
        }
        public void FilterItem()
        {
            if (Session["FilterCus"] == null)
            {
                Session["FilterCus"] = "";
            }
            if (!String.IsNullOrEmpty(Session["FilterCus"].ToString()))
            {
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 AND (ISNULL(Customer,'') = '' OR Customer = '" + Session["FilterCus"].ToString() + "') ORDER BY ItemCode ASC";
            }
            else
            {
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0";
            }
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterItem();
            grid.DataSourceID = "Masterfileitem";
            grid.DataBind();
        }
        protected void LocationCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gllocation_CustomCallback);
            FilterLoc();
            gridLookup.DataBind();
        }

        public void FilterLoc()
        {
            if (Session["FilterWH"] == null)
            {
                Session["FilterWH"] = "";
            }
            if (!String.IsNullOrEmpty(Session["FilterWH"].ToString()))
            {
                Location.SelectCommand = "SELECT LocationCode,WarehouseCode,RoomCode from Masterfile.Location where WarehouseCode = '" + Session["FilterWH"].ToString() + "' ORDER BY LocationCode ASC";
            }
            else
            {
                Location.SelectCommand = "SELECT LocationCode,WarehouseCode,RoomCode from Masterfile.Location ORDER BY LocationCode ASC";
            }
        }
        public void gllocation_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterLoc();
            grid.DataSourceID = "Location";
            grid.DataBind();
        }
        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            _Entity.TransType = Request.QueryString["transtype"].ToString();
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = deDocDate.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(glWarehouseCode.Text) ? null : glWarehouseCode.Value.ToString();
            _Entity.StorageType = String.IsNullOrEmpty(txtStorageType.Text) ? null : txtStorageType.Value.ToString();
            _Entity.CustomerCode = String.IsNullOrEmpty(cmbStorerKey.Text) ? null : cmbStorerKey.Value.ToString();
            _Entity.RelocationType = String.IsNullOrEmpty(txtRelocationType.Text) ? null : txtRelocationType.Value.ToString();
            _Entity.Remarks = txtRemarks.Text;
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();

            switch (e.Parameters)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of inserting for header

                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            gv1.KeyFieldName = "DocNumber;LineNumber";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        Validate();
                        //if (cp.JSProperties["cp_valmsg"].ToString() != "")
                        //{
                        //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                        //    cp.JSProperties["cp_success"] = true;
                        //}
                        //else
                        //{
                        gv1.JSProperties["cp_message"] = "Successfully Updated!";
                        gv1.JSProperties["cp_success"] = true;
                        gv1.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                        //}
                    }
                    else
                    {
                        gv1.JSProperties["cp_message"] = "Please check all the fields!";
                        gv1.JSProperties["cp_success"] = true;
                    }
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    gv1.JSProperties["cp_close"] = true;
                    gv1.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                    break;

                case "Pal":
                    if (string.IsNullOrEmpty(txtCustomer.Text) && string.IsNullOrEmpty(txtItem.Text) &&
                        string.IsNullOrEmpty(txtLocation.Text) && string.IsNullOrEmpty(txtPalletID.Text))
                    {
                        gv1.JSProperties["cp_error"] = "No input to search for!";
                    }
                    else
                    {
                        GetSelectedVal();
                        gvExtract.Selection.SelectAll();
                    }
                    break;


            }
        }
    }
        
    }
