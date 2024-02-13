using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;

using System.Data;
using DevExpress.Data.Filtering;
using GearsWarehouseManagement;
using System.Windows.Forms;

namespace GWL
{
    //-- =============================================
    //-- Author:		Alvin Joshua P. Alcon
    //-- CREATION  DATE: 12/19/2016
    //-- Description:	Service Processing Module
    //-- =============================================

    public partial class frmStorageTransfer : System.Web.UI.Page
    {

        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        string cb = "0";
        private static DataTable dtloc;

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            ToLocation.SelectCommand = "SELECT LocationCode,LocationDescription FROM Masterfile.Location where ISNULL(IsInactive,0)=0 and WarehouseCode = '" + txtwarehousecode.Text + "'";

            if (!string.IsNullOrEmpty(cmbStorerKey.Text))
            {
                Session["FilterCus"] = cmbStorerKey.Text;
            }

            if (!string.IsNullOrEmpty(txtwarehousecode.Text))
            {
                Session["FilterWH"] = txtwarehousecode.Text;
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

            dtpdocdate.MinDate = Convert.ToDateTime(Common.Common.SystemSetting("BOOKDATE", Session["ConnString"].ToString()));
            if (!IsPostBack)
            {
                Warehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";

                Session["FilterWH"] = null;
                Session["FilterCus"] = null;
		
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


                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();


                CriteriaOperator selectionCriteria = new InOperator("CustomerCode", new string[] { cmbStorerKey.Text });
                Inbound.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                txtRRDocno.DataSourceID = "Inbound";
                txtRRDocno.DataBind();

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    DataTable dtwh = Gears.RetriveData2("SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0", Session["ConnString"].ToString());
                    if (dtwh.Rows.Count == 1)
                    {

                        txtwarehousecode.Value = dtwh.Rows[0][0].ToString();

                    }
                }
                else if (Request.QueryString["entry"].ToString() == "V")
                {
                    txtwarehousecode.Value = Common.Common.SystemSetting("MAINWH", Session["ConnString"].ToString());
                    updateBtn.Text = "Close";


              

                
                    dtpdocdate.ReadOnly = true;
                
                }
                else
                {
                    DataTable getHeader = Gears.RetriveData2("SELECT * FROM WMS.StorageTransfer WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN
                    txtAddedBy.Text = getHeader.Rows[0]["AddedBy"].ToString();
                    txtAddedDate.Text = getHeader.Rows[0]["AddedDate"].ToString();
                    txtLastEditedBy.Text = getHeader.Rows[0]["LastEditedBy"].ToString();
                    txtLastEditedDate.Text = getHeader.Rows[0]["LastEditedDate"].ToString();
                    txtHSubmittedBy.Text = getHeader.Rows[0]["SubmittedBy"].ToString();
                    txtHSubmittedDate.Text = getHeader.Rows[0]["SubmittedDate"].ToString();
                    dtpdocdate.Text = String.IsNullOrEmpty(getHeader.Rows[0]["DocDate"].ToString()) ? "" : Convert.ToDateTime(getHeader.Rows[0]["DocDate"].ToString()).ToShortDateString();
                    cmbStorerKey.Text = getHeader.Rows[0]["CustomerCode"].ToString();
                    txtwarehousecode.Value = getHeader.Rows[0]["WarehouseCode"].ToString();

                }
                //DataTable getDet = Gears.RetriveData2("SELECT A.DocNumber, A.LineNumber, A.ColorCode, A.ClassCode, A.SizeCode, A.BulkQty, A.FromBulkUnit, A.Qty,"+
                //                                       " A.Unit, A.FromLoc, A.ToLoc, A.frombaseQty, A.StatusCode, A.BarcodeNo, A.BatchNumber, A.Field1, A.Field2,"+
                //                                       "A.Field3, A.PalletID, A.ToPalletID, A.ToBulkUnit, A.ToBaseUnit, A.ConvertedQty, A.ConvertedBulkQty, B.FullDesc"+
                //                                       "FROM WMS.RelocationStorageTypeDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN


                // DataTable getDet = Gears.RetriveData2("SELECT A.*, B.Fulldesc FROM WMS.RelocationStorageTypeDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());

                //DataTable getDet = Gears.RetriveData2("SELECT A.*, B.FullDesc FROM WMS.StorageTransferDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

                DataTable getDet = Gears.RetriveData2("SELECT A.DocNumber, A.ItemCode, A.NewBulkUnit, A.LineNumber,A.BulkQty,A.BulkUnit,A.BaseQty,A.BaseQty,A.BaseUnit,A.NewItemCode,A.NewBulkQty,A.NewBaseQty, " +
                "A.NewBaseUnit,A.PalletID,A.Location,A.BatchNumber,CONVERT(varchar(10),A.ExpiryDate,10) AS ExpiryDate,CONVERT(varchar(10),A.MfgDate,10) AS MfgDate,CONVERT(varchar(10),A.RRdate,10) AS RRdate, B.FullDesc " +
                "FROM WMS.StorageTransferDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

                //DataTable getDet = Gears.RetriveData2("SELECT * FROM WMS.StorageTransferDetail WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

                if (getDet.Rows.Count > 0)
                {
                    dtpdocdate.ClientEnabled = false;


                }

                gvd1.DataSource = getDet;
                gvd1.KeyFieldName = "DocNumber;LineNumber";
                gvd1.DataBind();



                //gvd1.Columns["MyDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                //gridView1.Columns["MyDate"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

            }

            if (cmbStorerKey.Text != "")
            {
                Inbound.SelectCommand = "select Docnumber,CustomerCode from wms.Inbound where CustomerCode = '" + cmbStorerKey.Text + "' and isnull(submittedby,'')!='' and isnull(PutawayDate,'')!='' AND   CHARINDEX(WarehouseCode,'" + txtwarehousecode.Text + "') >0  order by AddedDate Desc ";
            }
            txtRRDocno.DataBind();


            Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] A INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsAsset,0) = 0 AND Customer = '" + cmbStorerKey.Text + "' ORDER BY ItemCode ASC";

            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[12], "glItemCode");
            lookup.DataBind();
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
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            //date.ReadOnly = view;
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
                    cp.JSProperties["cp_message"] = "Successfully Submitted!";
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
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
                    break;
                case "customer":

                    CriteriaOperator selectionCriteria = new InOperator("CustomerCode", new string[] { cmbStorerKey.Text });
                    Inbound.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                    txtRRDocno.DataSourceID = "Inbound";
                    txtRRDocno.DataBind();
                    break;

            

           
                case "Delete":

                    DataTable dttableDetail = Gears.RetriveData2("SELECT Docnumber FROM WMS.StorageTransferDetail WHERE Docnumber='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());


                    if (dttableDetail.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_message"] = "Unable to delete Storage Transfer with Transaction Detail records!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;


                    }

                    cp.JSProperties["cp_delete"] = true;
                    break;
                case "ConfDelete":
                    Gears.RetriveData2("DELETE FROM WMS.StorageTransfer WHERE Docnumber='" + txtDocnumber.Text + "' ", Session["ConnString"].ToString());

                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
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
                               
                                "WMSSTOR",
                                dtpdocdate.Text.ToString(),
                                txtwarehousecode.Text.ToString(),
                                cmbStorerKey.Text.ToString(),
                                  "",
                                Session["userid"].ToString(),
                                values.NewValues["RecordId"],
                                values.NewValues["ItemCode"],
                                values.NewValues["BulkQty"],
                                values.NewValues["BulkUnit"],
                                values.NewValues["BaseQty"],
                                values.NewValues["BaseUnit"],
                                values.NewValues["NewItemCode"],
                                values.NewValues["NewBulkQty"],
                                values.NewValues["NewBulkUnit"],
                                values.NewValues["NewBaseQty"],
                                values.NewValues["NewBaseUnit"],
                                values.NewValues["PalletID"],
                                values.NewValues["Location"],
                                values.NewValues["BatchNumber"],
                                values.NewValues["ExpiryDate"],
                                values.NewValues["MfgDate"],
                                values.NewValues["RRdate"],
                                cb.ToString(),
                              ""
                  
                            });
            }
            GWarehouseManagement._connectionString = Session["ConnString"].ToString();
            string message = GWarehouseManagement.OpenFunctionVR(txtDocnumber.Text, updtable);

            if (!string.IsNullOrEmpty(message))
            {
                cp.JSProperties["cp_message"] = message;
                cp.JSProperties["cp_close"] = false;
            }
            else
                cp.JSProperties["cp_message"] = "Successfully Submitted!";
            cp.JSProperties["cp_close"] = true;
            Session["Refresh"] = "1";

        }

        private void GetSelectedVal()
        {
            gv1.DataSource = null;
            string f_expression = GetFilterExpression();

            string sql = "select '' TransType,  '' as DocDate, '' as WHCode,  '' as CustCode, '' as Rate " +
            ",'' AS Service, '' DocNumber, b.RecordId as LineNumber, b.ItemCode, c.FullDesc AS FullDesc, b.PalletID AS PalletID " +
            ",ISNULL(RemainingBulkQty,0)  as BulkQty " +
            ",C.UnitBulk AS BulkUnit " +
            ",ISNULL(RemainingBaseQty,0) as BaseQty" +
            ",C.UnitBase AS BaseUnit " +
            ",b.ItemCode as NewItemCode " +
            ",ISNULL(RemainingBulkQty,0)    as NewBulkQty" +
            ",C.UnitBase as NewBulkUnit " +
            ",ISNULL(RemainingBaseQty,0)  AS NewBaseQty " +
            ",B.BatchNumber, b.Location, CONVERT(varchar(10),B.ExpirationDate,10) AS ExpiryDate,CONVERT(varchar(10),B.MfgDate,10) AS MfgDate" +
            ",CONVERT(varchar(10),B.RRdate,10) AS RRdate, '' AS NoCharge " +
            ",C.UnitBase AS NewBaseUnit" +
            " from wms.Itemlocation b with (nolock) " +
            " left join masterfile.Item c with (nolock) " +
            " on b.ItemCode = c.ItemCode" +
            " left join masterfile.Location d with (nolock) " +
            " on b.Location = d.LocationCode" +
            " left join (SELECT DISTINCT Docnumber,PalletID FROM wms.inboundDetail with (nolock)) inb  " +
            " on b.PalletID = inb.PalletID" +
            " where " + f_expression.Replace("[", "").Replace("]", "") +
            " AND ISNULL(RemainingBaseQty,0) - ISNULL(PickedBaseQty,0)- ISNULL(ReservedBaseQty,0)  > 0" +
            " and ISNULL(RemainingBulkQty,0) - ISNULL(PickedBulkQty,0)- ISNULL(ReservedBulkQty,0)  > 0" +
            " group by B.RecordID, B.ItemCode, C.Fulldesc, B.PalletID,ISNULL(RemainingBulkQty,0)  , C.UnitBulk, ISNULL(RemainingBaseQty,0) " +
            ",C.UnitBase,B.BatchNumber, b.Location, B.ExpirationDate, B.MfgDate, B.RRdate   order by b.palletid ";


            DataTable dt = Gears.RetriveData2(sql, Session["ConnString"].ToString());
            gv1.DataSource = dt;
            gv1.DataBind();
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
                lst_operator.Add(new BinaryOperator("WarehouseCode", string.Format("%{0}%", txtwarehousecode.Text.Trim()), BinaryOperatorType.Like));

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
        #endregion


        protected void dtpdocdate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {

                dtpdocdate.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

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
    }

}
