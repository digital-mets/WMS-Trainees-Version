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
    public partial class frmItemRelocationModuleOpen : System.Web.UI.Page
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
            
                Session["FilterWH"] = null;
                Session["FilterCus"] = null;


                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Submit";
                        break;
            
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
               

           

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
                    


              //  }
                //2015-12-23    JDSD    Comment DataSource
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                //    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}
      
                    //gv1.DataSourceID =  "sdsDetail";
                    //var data = this.odsDetail.Select();
                    //DataView dtv = data as DataView;
                    //dtloc = dtv.ToTable();

                    //gv1.DataBind();
                    
                     //else
                     //{
                     //    gv1.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode;PalletID;Qty;FromLoc;ToLoc";
                     //}
            }

            //if (Session["itemreloctab"] == null)
            //{
            //    string sb = "select b.TransDoc DocNumber,b.RecordId as LineNumber   ,b.ItemCode,c.FullDesc ,b.ColorCode,b.ClassCode,b.SizeCode," +
            //                " b.PalletID,b.PalletID ToPalletID," +
            //                " SUM(ISNULL(b.RemainingBulkQty,0)-ISNULL(b.PickedBulkQty,0)-ISNULL(ReservedBulkQty,0)) BulkQty," +
            //                " SUM(ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0)) Qty," +
            //                " b.Location as FromLoc,'' ToLoc,'' StatusCode, Customer as CustomerCode,'' LotID,d.WarehouseCode,UnitBase,UnitBulk," +
            //                " MfgDate,ExpirationDate,BatchNumber from Wms.ItemLocation  b" +
            //                " with (nolock)" +
            //                " left join masterfile.Item c" +
            //                " on b.ItemCode = c.ItemCode" +

            //                " where ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0) > 0" +
            //                " and ISNULL(b.RemainingBulkQty,0)-ISNULL(b.PickedBulkQty,0)-ISNULL(ReservedBulkQty,0) > 0" +
            //                " group by BatchNumber,MfgDate,ExpirationDate,b.Transdoc,b.TransLine,Customer,b.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode,b.Location,b.PalletID,d.WarehouseCode,UnitBase,UnitBulk" +
            //                " order by b.Transdoc,b.TransLine,b.palletid";
            //    DataTable countsheet = Gears.RetriveData2(sb, Session["ConnString"].ToString());
            //    Session["itemreloctab"] = countsheet;
            //}
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

                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
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
        protected void gl6_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback3);
            gridLookup.GridView.DataSourceID = "ToLocation";
        }
        public void gridView_CustomCallback3(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = "ToLocation";
            grid.DataBind();

            string column = "LocationCode";
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, column) != null)
                    if (grid.GetRowValues(i, column).ToString() == "LocationCode")
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

            switch (e.Parameter)
            {
                case "Submit":
                  
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        cp.JSProperties["cp_message"] = "Successfully Submitted!";//Message variable to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                        break;
                
               
                case "refgrid":
                    gv1.DataBind();
                    break;
                case "wh":
                    ToLocation.DataBind();
                
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
            DataTable updtable = new DataTable();
            e.Handled = true;
            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                updtable.Columns.Add(dataColumn.FieldName);
            }

            foreach (ASPxDataUpdateValues values in e.UpdateValues)
            {
                updtable.Rows.Add(new Object[]{
                       "WMSREL",
                      values.NewValues["DocDate"],
                      values.NewValues["WHCode"],
                       values.NewValues["Storage"],
                        Session["userid"].ToString(),
                       

                            values.NewValues["RecordId"],
                             values.NewValues["ItemCode"],
                             "",
                            values.NewValues["PalletID"],
                            values.NewValues["ToPalletID"],
                            values.NewValues["FromLoc"],
                            values.NewValues["ToLoc"],
                            values.NewValues["Qty"],
                            values.NewValues["BulkQty"],
                            values.NewValues["BatchNumber"],
                            values.NewValues["Field1"],
                            values.NewValues["Field2"],
                            values.NewValues["Field3"],
                            values.NewValues["Field4"],
                            values.NewValues["Field5"],
                            values.NewValues["Field6"],
                            values.NewValues["Field7"],
                            values.NewValues["Field8"],
                            values.NewValues["Field9"],
                           
                            });
            }
            GWarehouseManagement._connectionString = Session["ConnString"].ToString();
            GearsWarehouseManagement.GWarehouseManagement.OpenFunction(updtable);


        }

        private void GetSelectedVal()
        {
            gv1.DataSource = null;
            string f_expression = GetFilterExpression();
           
            //glWarehouseCode.Text,
            //txtStorageType.Text,

            string sql = "select 'WMSREL' TransType,  '" + deDocDate.Text + "' as DocDate, '" + glWarehouseCode.Text + "' as WHCode, " +
                         " '" + txtStorageType.Text + "' as Storage,'' DocNumber,RecordId as LineNumber,b.ItemCode,c.FullDesc, " +
                        "b.PalletID,b.PalletID as ToPalletID,b.PalletID as ToPalletID,b.Location as  FromLoc " +
                        ",b.Location as  ToLoc,ISNULL(RemainingBaseQty,0) - ISNULL(PickedBaseQty,0)- ISNULL(ReservedBaseQty,0) as Qty " +
                        ",ISNULL(RemainingBulkQty,0) - ISNULL(PickedBulkQty,0)- ISNULL(ReservedBulkQty,0) as BulkQty ,BatchNumber " +
                        ",'' Field1,'' Field2,'' Field3,'' Field4,'' Field5,'' Field6,'' Field7,'' Field8,'' Field9 from WMS.ItemLocation b " +
                        "with (nolock) " +
                        "left join masterfile.Item c " +
                        "ON b.ItemCode = c.ItemCode " +
                        "LEFT JOIN Masterfile.Location A " +
                        "ON B.Location = A.LocationCode  where " +
                          f_expression.Replace("[", "").Replace("]", "") +
                        "order by b.RecordID DESC";


            DataTable dt = Gears.RetriveData2(sql, Session["ConnString"].ToString());
            gv1.DataSource = dt;
            gv1.DataBind();
        }


        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(cmbStorerKey.Text))
                lst_operator.Add(new BinaryOperator("Customer", string.Format("%{0}%", cmbStorerKey.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtItem.Text))
                lst_operator.Add(new BinaryOperator("b.ItemCode", string.Format("%{0}%", txtItem.Text.Trim()), BinaryOperatorType.Like));

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

                deDocDate.Date = DateTime.Now;
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
  

    
 
        protected void rpFilter_Load(object sender, EventArgs e)
        {
            ASPxRoundPanel pnl = sender as ASPxRoundPanel;
            pnl.Visible = !view;
        }

        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GetSelectedVal();
        } 
    }
        
    }
