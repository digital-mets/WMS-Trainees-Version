﻿using DevExpress.Web;
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


        private static DataTable dtloc;

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["transtype"].ToString() == "WMSREL")
            {
                txttile.Text = "Item Relocation";
                ASPxLabel.Text = "Item Relocation";
                this.gv1.Columns["ToPalletID"].Width = 0;
            }
            else
            {
                this.gv1.Columns["ToLoc"].Width = 0;
                ASPxLabel.Text = "Pallet Compress";
                txttile.Text = "Pallet Compress";
            }
            Gears.UseConnectionString(Session["ConnString"].ToString());
            //ToLocation.SelectCommand = "SELECT LocationCode,LocationDescription FROM Masterfile.Location where ISNULL(IsInactive,0)=0 and WarehouseCode = '" + txtwarehousecode.Text + "'";

            if (!string.IsNullOrEmpty(cmbStorerKey.Text))
            {
                Session["CustFilter"] = cmbStorerKey.Text;
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
            Warehouse.SelectCommand = "SELECT WarehouseCode,Description FROM Masterfile.[Warehouse] where isnull(IsInactive,'')=0 and CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0";
                
            if (!IsPostBack)
            {
                dtpdocdate.Date = DateTime.Now;
                Session["DocDate"] = null;
                Session["CustFilter"] = null;
             
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

                DataTable getHeader = Gears.RetriveData2("select A.*  ,EU.FullName as  LastEditedBy1, SB.FullName as  SubmittedBy1,AU.FullName as AddedBy1 from WMS.ItemRelocation A LEFT JOIN IT.Users AU    ON A.AddedBy = AU.UserID    LEFT JOIN IT.Users EU    ON A.LastEditedBy = EU.UserID    LEFT JOIN IT.Users SB    ON A.SubmittedBy = SB.UserID   WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN


                if (Request.QueryString["entry"].ToString() == "N")
                {
                    Session["DocDate"] = dtpdocdate.Text;
                    DataTable dtwh = Gears.RetriveData2("SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0", Session["ConnString"].ToString());
                    if (dtwh.Rows.Count == 1)
                    {

                        txtwarehousecode.Value = dtwh.Rows[0][0].ToString();

                    }

                }
                else if (Request.QueryString["entry"].ToString() != "D")
                {
                          
                    dtpdocdate.Text = String.IsNullOrEmpty( getHeader.Rows[0]["DocDate"].ToString()) ? "" : Convert.ToDateTime( getHeader.Rows[0]["DocDate"].ToString()).ToShortDateString();
                    Session["DocDate"] = dtpdocdate.Text;
                    Session["CustFilter"] = getHeader.Rows[0]["CustomerCode"].ToString();
                    cmbStorerKey.Text = getHeader.Rows[0]["CustomerCode"].ToString();
                    txtwarehousecode.Value = getHeader.Rows[0]["WarehouseCode"].ToString();
                   
                }
                DataTable getDet = Gears.RetriveData2("SELECT A.*,B.FullDesc FROM WMS.ItemRelocationDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE Docnumber='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

                if(getDet.Rows.Count > 0)
                {
                    dtpdocdate.ClientEnabled = false;
                    cmbStorerKey.ClientEnabled = false;
                }

                txtAddedBy.Text = getHeader.Rows[0]["AddedBy1"].ToString();
                txtAddedDate.Text = getHeader.Rows[0]["AddedDate"].ToString();
                txtLastEditedBy.Text = getHeader.Rows[0]["LastEditedBy1"].ToString();
                txtLastEditedDate.Text = getHeader.Rows[0]["LastEditedDate"].ToString();
                txtHSubmittedBy.Text = getHeader.Rows[0]["SubmittedBy1"].ToString();
                txtHSubmittedDate.Text = getHeader.Rows[0]["SubmittedDate"].ToString();

                gvd1.DataSource = getDet;
                gvd1.KeyFieldName = "DocNumber;LineNumber";
                gvd1.DataBind();
               
            }

            Session["CustFilter"] = cmbStorerKey.Text;
            if (cmbStorerKey.Text != "")
            {
                Inbound.SelectCommand = "select Docnumber,CustomerCode from wms.Inbound where CustomerCode = '" + cmbStorerKey.Text + "' and isnull(submittedby,'')!='' and isnull(PutawayDate,'')!='' AND   CHARINDEX(WarehouseCode,'" + txtwarehousecode.Text + "') >0  order by AddedDate Desc ";
            }
            txtRRDocno.DataBind();


         
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

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

            switch (e.Parameter)
            {
                case "Submit":
                  
                        gv1.UpdateEdit();//2nd initiation to insert grid
                     
                        break;
                
               
                case "refgrid":
                    gv1.DataBind();
                    break;
                
                case "DocDate":
                    Session["DocDate"] = dtpdocdate.Text;
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
			string errorMsg ="";

  			if (Request.QueryString["transtype"].ToString() == "WMSREL")
            		{
            			  errorMsg  =  "Unable to delete Item Relocation with Transaction Detail records!";

           		 }
          		else
           		 {
               			  errorMsg  =  "Unable to delete Pallet Compress with Transaction Detail records!";
           		 }


                        cp.JSProperties["cp_message"] = errorMsg;
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
                                Session["DocDate"].ToString(),
                                txtwarehousecode.Text.ToString(),
                                Session["CustFilter"].ToString(),
                               
                                "",
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
                                values.NewValues["Field3"]
                           
                            });
            }
            GWarehouseManagement._connectionString = Session["ConnString"].ToString();
            string message = GWarehouseManagement.OpenFunction(txtDocnumber.Text, updtable);

            if (!string.IsNullOrEmpty(message))
            {

                cp.JSProperties["cp_success"] = false;
                cp.JSProperties["cp_message"] = message;
                cp.JSProperties["cp_fail"] = true;
                Gears.RetriveData2("DELETE FROM WMS.ItemRelocationDetail WHERE Docnumber='" + txtDocnumber.Text + "' AND ISNULL(Field9,'')=''", Session["ConnString"].ToString());
                Gears.RetriveData2("DELETE FROM WMS.CountsheetSubsi WHERE TransDoc='" + txtDocnumber.Text + "' AND TransType='WMSREL' AND ISNULL(SubmiteddDate,'')=''", Session["ConnString"].ToString());
            }
            else
            {
                cp.JSProperties["cp_success"] = true;
                cp.JSProperties["cp_message"] = "Successfully Submitted!";
                cp.JSProperties["cp_close"] = true;
                Session["Refresh"] = "1";
            }

     

        }

        private void GetSelectedVal()
        {
            gv1.DataSource = null;
            string f_expression = GetFilterExpression().Replace("[b.ItemCode]", "b.ItemCode")
                .Replace("[b.PalletID]", "b.PalletID");
           
            //glWarehouseCode.Text,
            //txtStorageType.Text,


            string sql = "select '' TransType,  '' as DocDate, '' as WHCode,  '' as CustCode, " +
                        " '' as Storage,'' DocNumber,RecordId as LineNumber,b.ItemCode,c.FullDesc, " +
                        "b.PalletID,b.PalletID as ToPalletID,b.Location as  FromLoc " +
                        ",b.Location as  ToLoc,ISNULL(RemainingBaseQty,0) - ISNULL(PickedBaseQty,0)- ISNULL(ReservedBaseQty,0) as Qty " +
                        ",ISNULL(RemainingBulkQty,0) - ISNULL(PickedBulkQty,0)- ISNULL(ReservedBulkQty,0) as BulkQty ,BatchNumber " +
                        ", Convert(varchar(max),ExpirationDate) Field1, Convert(varchar(max),MfgDate) Field2,   Convert(varchar(max),b.RRDate) Field3 " +
                        " from wms.Itemlocation b with (nolock) " +
                        " left join masterfile.Item c with (nolock) " +
                        " on b.ItemCode = c.ItemCode" +
                        " left join masterfile.Location d with (nolock) " +
                        " on b.Location = d.LocationCode" +
                        " and b.WarehouseCode = d.WarehouseCode" +
                        " left join (SELECT DISTINCT Docnumber,PalletID FROM wms.inboundDetail with (nolock)) inb  " +
                        " on b.PalletID = inb.PalletID" +
                        " where " + f_expression.Replace("[", "").Replace("]", "") +
                        " AND  ISNULL(b.PickedBaseQty,0) = 0 AND ISNULL(ReservedBaseQty,0) = 0" +
                        " and ISNULL(b.PickedBulkQty,0) = 0 AND ISNULL(ReservedBulkQty,0) = 0" +
                        " and (ISNULL(b.RemainingBaseQty,0) > 0 OR ISNULL(RemainingBulkQty,0) > 0)" +
                        " group by RecordID,b.ItemCode,FullDesc,MfgDate,ExpirationDate,RRDate,b.PalletID,b.Location,Customer,b.ItemCode,b.Location,BatchNumber,ISNULL(RemainingBaseQty,0) - ISNULL(PickedBaseQty,0)- ISNULL(ReservedBaseQty,0),ISNULL(RemainingBulkQty,0) - ISNULL(PickedBulkQty,0)- ISNULL(ReservedBulkQty,0)  order by b.palletid ";



            DataTable dt = Gears.RetriveData2(sql, Session["ConnString"].ToString());
            gv1.DataSource = dt;
            gv1.DataBind();
        }


        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(cmbStorerKey.Text.Trim()))
                lst_operator.Add(new BinaryOperator("Customer", string.Format("%{0}%", Session["CustFilter"].ToString().Trim()), BinaryOperatorType.Like));

            if (!string.IsNullOrEmpty(txtItem.Text))
                lst_operator.Add(new BinaryOperator("b.ItemCode", string.Format("%{0}%", txtItem.Text.Trim()), BinaryOperatorType.Like));

            //if (!string.IsNullOrEmpty(txtLot.Text))
            //    lst_operator.Add(new BinaryOperator("LotId", string.Format("%{0}%", txtLot.Text), BinaryOperatorType.Like));
            if (!string.IsNullOrEmpty(txtwarehousecode.Text))
                lst_operator.Add(new BinaryOperator("b.WarehouseCode", string.Format("%{0}%", txtwarehousecode.Text.Trim()), BinaryOperatorType.Like));

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