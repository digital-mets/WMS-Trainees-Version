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
    public partial class frmItemReservation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            ToLocation.SelectCommand = "SELECT LocationCode,LocationDescription FROM Masterfile.Location where ISNULL(IsInactive,0)=0 and WarehouseCode = '" + txtwarehousecode.Text + "'";

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


            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            dtpdocdate.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));
            Warehouse.SelectCommand = "SELECT WareHouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0";


            if (!IsPostBack)
            {
                Session["wmsitemadjtab"] = null;
                Session["FilterExpression"] = null;
                updateBtn.Text = "Submit";
                view = false;


                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        txtDocnumber.ReadOnly = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }

                txtRelocationType.Items.Add("Reserved By Scatter");
                txtRelocationType.Items.Add("Reserved By Weight");
                txtRelocationType.Items.Add("Unreserved");




                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                DataTable getHeader = Gears.RetriveData2("SELECT A.*,AU.FullName AddB,EU.FullName EditB,SB.FullName SubB FROM WMS.ItemRelocation A " +
                "  LEFT JOIN IT.Users AU "+
                "  ON A.AddedBy = AU.UserID "+
                "  LEFT JOIN IT.Users EU "+
                "  ON A.LastEditedBy = EU.UserID "+
                "  LEFT JOIN IT.Users SB " +
                "  ON A.SubmittedBy = SB.UserID "+
                " WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    txtRelocationType.Text = "Reserved By Scatter";
                    txtwarehousecode.Value = Common.Common.SystemSetting("MAINWH", Session["ConnString"].ToString());//ADD CONN

                }
                else
                {

                    dtpdocdate.Text = String.IsNullOrEmpty(getHeader.Rows[0]["DocDate"].ToString()) ? "" : Convert.ToDateTime(getHeader.Rows[0]["DocDate"].ToString()).ToShortDateString();
                    cmbStorerKey.Text = getHeader.Rows[0]["CustomerCode"].ToString();
                    txtwarehousecode.Value = getHeader.Rows[0]["WarehouseCode"].ToString();

                }
                DataTable getDet = Gears.RetriveData2("SELECT A.*,B.FullDesc FROM WMS.ItemRelocationDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode  WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN


                txtAddedBy.Text = getHeader.Rows[0]["AddB"].ToString();
                txtLastEditedBy.Text = getHeader.Rows[0]["EditB"].ToString();
                txtHSubmittedBy.Text = getHeader.Rows[0]["SubB"].ToString();




                txtAddedDate.Text = getHeader.Rows[0]["AddedDate"].ToString();
                txtLastEditedDate.Text = getHeader.Rows[0]["LastEditedDate"].ToString();
                txtHSubmittedDate.Text = getHeader.Rows[0]["SubmittedDate"].ToString();

                gvd1.DataSource = getDet;
                gvd1.KeyFieldName = "DocNumber;LineNumber";
                gvd1.DataBind();



                Freeze();
                
            }

            CriteriaOperator selectionCriteria = new InOperator("CustomerCode", new string[] { cmbStorerKey.Text });
            Inbound.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            txtRRDocno.DataSourceID = "Inbound";
            txtRRDocno.DataBind();


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

                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
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
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
        }

        protected void lookup_Init2(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
          
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode"))
            {
                string test = Request.Params["__CALLBACKID"];
                ASPxGridLookup gridLookup = sender as ASPxGridLookup;
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                FilterItem();
                gridLookup.DataBind();
            }
        }
        public void FilterItem()
        {
         }
        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            switch (e.Parameter)
            {
                case "Submit":

                    gv1.UpdateEdit();//2nd initiation to insert grid

                    break;

                case "refgrid":
                    gv1.DataBind();
                    break;
                case "customer":

                    CriteriaOperator selectionCriteria = new InOperator("CustomerCode", new string[] { cmbStorerKey.Text });
                    Inbound.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                    txtRRDocno.DataSourceID = "Inbound";
                    txtRRDocno.DataBind();
                    break;


                case "Delete":

                    DataTable dttableDetail = Gears.RetriveData2("SELECT Docnumber FROM WMS.ItemRelocationDetail WHERE Docnumber='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());


                    if (dttableDetail.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_message"] = "Unable to delete Item Reservation with Transaction Detail records!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;


                    }

                    cp.JSProperties["cp_delete"] = true;
                    break;
                case "ConfDelete":
                    Gears.RetriveData2("DELETE FROM WMS.ItemRelocation WHERE Docnumber='" + txtDocnumber.Text + "' ", Session["ConnString"].ToString());

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

        protected void gv1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                ASPxGridView grid = sender as ASPxGridView;
                double total = 0;
                for (int i = 0; i < grid.VisibleRowCount; i++)
                    if (grid.IsGroupRow(i))
                        total += Convert.ToDouble(grid.GetGroupSummaryValue(i, grid.GroupSummary["Qty"]));
                e.TotalValue = total;
                e.TotalValueReady = true;
             
            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { 
            

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
            DataTable updtable = new DataTable();
            e.Handled = true;
            string sql = "SELECT TOP 1 SPACE(100) as TransType,  0 as RecordID," +
                " SPACE(100) as ItemCode,GETDATE() as DocDate,SPACE(100) as WarehouseCode,SPACE(100) as UserID, 0.00 as AdjustedQty, 0.00 as BulkQty,  SPACE(8000) as Field1," +
                " SPACE(8000) as Field2, SPACE(8000) as Field3, SPACE(8000) as Field4, SPACE(8000) as Field4, SPACE(8000) as Field5," +
                " SPACE(8000) as Field6, SPACE(8000) as Field7, SPACE(8000) as Field8, SPACE(8000) as Field9 FROM WMS.ItemAdjustmentDetail ";
            updtable = Gears.RetriveData2(sql, Session["ConnString"].ToString());

            updtable.Rows.Clear();

            foreach (ASPxDataUpdateValues values in e.UpdateValues)
            {
                updtable.Rows.Add(new Object[]{
                      Request.QueryString["transtype"].ToString(),
                      values.NewValues["RecordID"],
                      values.NewValues["ItemCode"],
                      dtpdocdate.Value.ToString(),
                      txtwarehousecode.Text.ToString(),
                      Session["userid"].ToString(),
                      values.NewValues["TargetQty"],
                      values.NewValues["TargetBulkQty"],
                      values.NewValues["ToPalletID"],
                      values.NewValues["FromLoc"],
                      txtRelocationType.Text.ToString(),
                      values.NewValues["Field4"],
                      values.NewValues["Field5"],
                      values.NewValues["Field6"],
                      values.NewValues["Field7"],
                      values.NewValues["Field8"],
                      values.NewValues["Field9"]
                            });
            }
            GWarehouseManagement._connectionString = Session["ConnString"].ToString();
            string message = GWarehouseManagement.OpenFunction(txtDocnumber.Text,updtable);
            if (!string.IsNullOrEmpty(message))
            {
                cp.JSProperties["cp_success"] = false;
                cp.JSProperties["cp_message"] = message;
                cp.JSProperties["cp_fail"] = true;
                Gears.RetriveData2("DELETE FROM WMS.ItemRelocationDetail WHERE Docnumber='" + txtDocnumber.Text + "' AND ISNULL(Field9,'')=''", Session["ConnString"].ToString());
                Gears.RetriveData2("DELETE FROM WMS.CountsheetSubsi WHERE TransDoc='" + txtDocnumber.Text + "' AND TransType='WMSRSV' AND ISNULL(SubmiteddDate,'')=''", Session["ConnString"].ToString());
            }
            else
            {
                cp.JSProperties["cp_success"] = true;
                cp.JSProperties["cp_message"] = "Successfully Submitted!";
                cp.JSProperties["cp_close"] = true;
                Session["Refresh"] = "1";
            }
        }
        #endregion

        #region Validation
   private void Validate()
   {
    

   }
   #endregion
        protected void Connection_Init(object sender, EventArgs e)
        {
           
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
          
        }

        protected void gvExtract_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        private void GetSelectedVal()
        {
            gv1.DataSource = null;

            gv1.DataSourceID = null;
            gv1.DataBind();
            string f_expression = GetFilterExpression();
            string sql = "";

            if (txtRelocationType.Text == "Unreserved")
            {
                sql = "select '' as Docnumber ,b.RecordID,b.ItemCode,MAX(C.Fulldesc) as ItemDescription, b.PalletID ,b.PalletID ToPalletID, " +
                                  " B.ReservedBulkQty CurrentBulkQty, " +
                                  " B.ReservedBulkQty TargetBulkQty, 0.00 as BulkQty ," +
                                   " B.ReservedBaseQty CurrentQty, " +
                                  " B.ReservedBaseQty TargetQty," +
                                  " 0.00 as AdjustedQty, b.Location as FromLoc ,'' ToLoc,'' StatusCode, Customer,'' LotID,d.WarehouseCode,UnitBase as Unit,UnitBulk BulkUnit, Convert(varchar(max),MfgDate) Mkfgdate," +
                                  " Convert(varchar(max),ExpirationDate) ExpiryDate,b.BatchNumber as BatchNo,Convert(varchar(max),b.RRDate) as RRDate,MAX(d.StorageType) as StorageType, PickedBaseQty,PickedBulkQty,ReservedBaseQty,ReservedBulkQty,b.Field1 Field1,b.Field1  Field2, " +
                                  " b.Field1 as Field3,b.Field1 as Field4,b.Field1 as Field5,b.Field1 as Field6,b.Field1 as Field7,b.Field1 as Field8,b.Field1 as Field9 " +
                                  " from wms.Itemlocation b with (nolock) " +
                                  " left join masterfile.Item c with (nolock) " +
                                  " on b.ItemCode = c.ItemCode" +
                                  " left join masterfile.Location d with (nolock) " +
                                  " on b.Location = d.LocationCode" +
                                  " left join (SELECT DISTINCT Docnumber,PalletID FROM wms.inboundDetail with (nolock)) inb  " +
                                  " on b.PalletID = inb.PalletID" +
                    //  " inner join ( SELECT RecordID,ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0) as CurrentQty,'' as Field FROM WMS.ItemLocation b) as INC ON INC.RecordID = b.RecordID" +
                                  " where " + f_expression.Replace("[", "").Replace("]", "") +
                                  " AND  ISNULL(b.ReservedBulkQty,0) > 0 AND ISNULL(ReservedBaseQty,0) > 0" +

                                  " group by b.RecordID,b.BatchNumber,MfgDate,ExpirationDate,RRDate,Customer,b.ItemCode,b.Location,b.PalletID,d.WarehouseCode,UnitBase,UnitBulk,B.RemainingBaseQty,b.Field1,B.RemainingBulkQty, PickedBaseQty,PickedBulkQty,ReservedBaseQty,ReservedBulkQty order by b.palletid ";

            }
            else
            {
                sql = "select '' as Docnumber ,b.RecordID,b.ItemCode,MAX(C.Fulldesc) as ItemDescription, b.PalletID ,b.PalletID ToPalletID, " +
                                " B.RemainingBulkQty CurrentBulkQty, " +
                                " B.RemainingBulkQty TargetBulkQty, 0.00 as BulkQty ," +
                                 " B.RemainingBaseQty CurrentQty, " +
                                " B.RemainingBaseQty TargetQty," +
                                " 0.00 as AdjustedQty, b.Location as FromLoc ,'' ToLoc,'' StatusCode, Customer,'' LotID,d.WarehouseCode,UnitBase as Unit,UnitBulk BulkUnit, Convert(varchar(max),MfgDate) Mkfgdate," +
                                " Convert(varchar(max),ExpirationDate) ExpiryDate,b.BatchNumber as BatchNo,Convert(varchar(max),b.RRDate) as RRDate,MAX(d.StorageType) as StorageType, PickedBaseQty,PickedBulkQty,ReservedBaseQty,ReservedBulkQty,b.Field1 Field1,b.Field1  Field2, " +
                                " b.Field1 as Field3,b.Field1 as Field4,b.Field1 as Field5,b.Field1 as Field6,b.Field1 as Field7,b.Field1 as Field8,b.Field1 as Field9 " +
                                " from wms.Itemlocation b with (nolock) " +
                                " left join masterfile.Item c with (nolock) " +
                                " on b.ItemCode = c.ItemCode" +
                                " left join masterfile.Location d with (nolock) " +
                                " on b.Location = d.LocationCode" +
                                " left join (SELECT DISTINCT Docnumber,PalletID FROM wms.inboundDetail with (nolock)) inb  " +
                                " on b.PalletID = inb.PalletID" +
                    //  " inner join ( SELECT RecordID,ISNULL(b.RemainingBaseQty,0)-ISNULL(b.PickedBaseQty,0)-ISNULL(ReservedBaseQty,0) as CurrentQty,'' as Field FROM WMS.ItemLocation b) as INC ON INC.RecordID = b.RecordID" +
                                " where " + f_expression.Replace("[", "").Replace("]", "") +
                                " AND  ISNULL(b.PickedBaseQty,0) = 0 AND ISNULL(ReservedBaseQty,0) = 0" +
                                " and ISNULL(b.PickedBulkQty,0) = 0 AND ISNULL(ReservedBulkQty,0) = 0" +
                                " and ISNULL(b.RemainingBaseQty,0) > 0 AND ISNULL(RemainingBulkQty,0) > 0" +
                                " group by b.RecordID,b.BatchNumber,MfgDate,ExpirationDate,RRDate,Customer,b.ItemCode,b.Location,b.PalletID,d.WarehouseCode,UnitBase,UnitBulk,B.RemainingBaseQty,b.Field1,B.RemainingBulkQty, PickedBaseQty,PickedBulkQty,ReservedBaseQty,ReservedBulkQty order by b.palletid ";

            }

            DataTable dt = Gears.RetriveData2(sql, Session["ConnString"].ToString());
            foreach (DataRow _row in dt.Rows)
            {
                _row["CurrentQty"] = Convert.ToDecimal(_row["CurrentQty"].ToString()) - (Convert.ToDecimal(_row["PickedBaseQty"].ToString()) + Convert.ToDecimal(_row["ReservedBaseQty"].ToString()));
                _row["TargetQty"] = Convert.ToDecimal(_row["CurrentQty"].ToString());

                _row["CurrentBulkQty"] = Convert.ToDecimal(_row["CurrentBulkQty"].ToString()) - (Convert.ToDecimal(_row["PickedBulkQty"].ToString()) + Convert.ToDecimal(_row["ReservedBulkQty"].ToString()));
                _row["TargetBulkQty"] = Convert.ToDecimal(_row["CurrentBulkQty"].ToString());


            }
            gv1.KeyFieldName = "RecordID";
            gv1.DataSource = dt;
            gv1.DataBind();
            Freeze();
        }

        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(cmbStorerKey.Text.Trim()))
                lst_operator.Add(new BinaryOperator("Customer", string.Format("%{0}%", cmbStorerKey.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtItem.Text))
                lst_operator.Add(new BinaryOperator("b.ItemCode", string.Format("%{0}%", txtItem.Text.Trim()), BinaryOperatorType.Like));

            //if (!string.IsNullOrEmpty(txtLot.Text))
            //    lst_operator.Add(new BinaryOperator("LotId", string.Format("%{0}%", txtLot.Text), BinaryOperatorType.Like));
            if (!string.IsNullOrEmpty(txtwarehousecode.Text))
                lst_operator.Add(new BinaryOperator("d.WarehouseCode", string.Format("%{0}%", txtwarehousecode.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtRRDocno.Text))
                lst_operator.Add(new BinaryOperator("inb.Docnumber", string.Format("%{0}%", txtRRDocno.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtLocation.Text))
                lst_operator.Add(new BinaryOperator("Location", string.Format("%{0}%", txtLocation.Text.Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtPalletID.Text))
                lst_operator.Add(new BinaryOperator("b.PalletID", string.Format("%{0}%", txtPalletID.Text.Trim()), BinaryOperatorType.Like));

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
        protected void rpFilter_Load(object sender, EventArgs e)
        {
            ASPxRoundPanel pnl = sender as ASPxRoundPanel;
            pnl.Visible = !view;
        }

        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GetSelectedVal();
        }

        protected void Freeze()
        {

            //if (gv1.Columns["JorOrder"].Width.ToString() == "0")
            //{
            //    int consfreeze = 0;
            //    foreach (GridViewColumn column in gv1.VisibleColumns)
            //    {
            //        if (column is GridViewColumn)
            //        {
            //            GridViewColumn dataColumn = (GridViewColumn)column;
            //            if (dataColumn.Visible)
            //                dataColumn.FixedStyle = GridViewColumnFixedStyle.Left;
            //            consfreeze++;
            //        }
            //        if (consfreeze == 8)
            //            break;
            //    }
            //}
            //else
            //{
                int consfreeze = 0;
                foreach (GridViewColumn column in gv1.VisibleColumns)
                {
                    if (column is GridViewColumn)
                    {
                        GridViewColumn dataColumn = (GridViewColumn)column;
                        if (dataColumn.Visible)
                            dataColumn.FixedStyle = GridViewColumnFixedStyle.Left;
                        consfreeze++;
     
                    }

                    if (consfreeze == 5)
                    {
                        break;
                    }
                }
            //}




        }

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
      
    }
}