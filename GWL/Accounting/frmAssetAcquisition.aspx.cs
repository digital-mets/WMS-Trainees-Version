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
    public partial class frmAssetAcquisition : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.AssetAcquisition _Entity = new AssetAcquisition();//Calls entity odsHeader
        Entity.AssetAcquisition.AssetAcquisitionDetail _EntityDetail = new AssetAcquisition.AssetAcquisitionDetail();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            string referer;
            try
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

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {

                Session["atccode"] = null;
                Session["picklistdetail"] = null;
                Session["customoutbound"] = null;

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Update";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        gv1.KeyFieldName = "DocNumber;LineNumber";
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    //popup.ShowOnPageLoad = false;
                    Generatebtn.ClientVisible = true;



                    //gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv1.Settings.VerticalScrollableHeight = 200;
                    //gv1.KeyFieldName = "LineNumber;PODocNumber";
                }
                else
                {

                    gv1.DataSourceID = null;
                    _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                    dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                    //txtTotalQuantity.Value = _Entity.TotalQty;
                    speTotalQuantity.Value = _Entity.TotalQty;
                    txtWarehouseCode.Text = _Entity.WarehouseCode;


                    txtHField1.Value = _Entity.Field1.ToString();
                    txtHField2.Value = _Entity.Field2.ToString();
                    txtHField3.Value = _Entity.Field3.ToString();
                    txtHField4.Value = _Entity.Field4.ToString();
                    txtHField5.Value = _Entity.Field5.ToString();
                    txtHField6.Value = _Entity.Field6.ToString();
                    txtHField7.Value = _Entity.Field7.ToString();
                    txtHField8.Value = _Entity.Field8.ToString();
                    txtHField9.Value = _Entity.Field9.ToString();

                    txtHAddedBy.Text = _Entity.AddedBy;
                    txtHAddedDate.Text = _Entity.AddedDate;
                    txtHLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHLastEditedDate.Text = _Entity.LastEditedDate;
                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;


                }


                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                if (Request.QueryString["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
                {
                    gv1.DataSourceID = null;
                }
            }

            if (gv1.DataSource != null)
            {
                gv1.DataSourceID = null;
            }


        }
        #endregion

        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._TransType = "PRCRCR";
            //string strresult = GearsProcurement.GProcurement.ReceivingReport_Validate(gparam);
            //if (strresult != " ")
            //{
            //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            //}
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
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate = dtpDocDate.Text;
            //_Entity.TotalQty = Convert.ToDecimal(txtTotalQuantity.Value);
            _Entity.TotalQty = String.IsNullOrEmpty(speTotalQuantity.Text) ? 0 : Convert.ToDecimal(speTotalQuantity.Value.ToString());
            _Entity.WarehouseCode = txtWarehouseCode.Text;


            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            //  _Entity.Transtype = Request.QueryString["transtype"].ToString();

            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        //  _Entity.SubsiEntry(txtDocNumber.Text);
                        if (Session["Datatable"] == "1")
                        {
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity

                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;

                case "Update":

                    //gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.InsertData(_Entity);//Method of inserting for header


                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = AcquisitionDetailSource.ID;
                            gv1.UpdateEdit();
                        }

                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
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
                    cp.JSProperties["cp_generated"] = true;
                    GetSelectedVal();
                    //glSupplierCode.Enabled = false;
                    //Generatebtn.Enabled = false;
                    break;
                case "clone":
                    Session["Clone"] = "1";
                    break;

            }
        }

        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            string[] selectedValues = txtDocNumber.Text.Split(' ');
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
            AcquisitionDetailSource.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["acquisitiondetail"] = AcquisitionDetailSource.FilterExpression;



            AcquisitionDetailSource.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY RRDPO.DocNumber) AS VARCHAR(5)),5) AS LineNumber, "
                                                    + "RRDPO.DocNumber,RRDPO.ItemCode,FullDesc,ColorCode,ClassCode,SizeCode,Unit,ReceivedQty,(ISNULL(RRDPO.UnitCost,0) + ISNULL(RRDPO.UnitFreight,0)) * ISNULL(B.ExchangeRate,0) as UnitCost, ReceivedQty AS QtyCount "

                                                    + " FROM Procurement.ReceivingReportDetailPO RRDPO "
                                                    + "INNER JOIN Procurement.ReceivingReport B "
                                                    + "ON RRDPO.DocNumber = B.DocNumber LEFT JOIN Masterfile.Item I "
                                                    + "ON RRDPO.ItemCode = I.ItemCode "
                                                    + "WHERE ISNULL(SubmittedBy,'')!='' "
                                                    + "AND RRDPO.DocNumber = '" + txtDocNumber.Text + "' "
                                                    + "AND RRDPO.ItemCode NOT IN (SELECT ItemCode FROM Accounting.AssetInv WHERE DocNumber = '" + txtDocNumber.Text + "')";


            gv1.DataSourceID = AcquisitionDetailSource.ID;
            gv1.DataBind();
            Session["Datatable"] = "1";

            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["DocNumber"],dt.Columns["LineNumber"]};

            return dt;
        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.UpdateValues.Clear();
                e.InsertValues.Clear();

            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = i;
                    var DocNumber = "Temp";
                    var ItemCode = values.NewValues["ItemCode"];
                    var FullDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var Unit = values.NewValues["Unit"];
                    var UnitCost = values.NewValues["UnitCost"];
                    var ReceivedQty = values.NewValues["ReceivedQty"];
                    var QtyCount = values.NewValues["QtyCount"];

                    //source.Rows.Add(LineNumber, DocNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, UnitCost, ReceivedQty, QtyCount, field1, field2, field3, field4, field5, field6, field7, field8, field9);
                    source.Rows.Add(LineNumber, DocNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, Unit, UnitCost, ReceivedQty, QtyCount);
                    i++;

                }

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = 
                    { 
                        values.NewValues["DocNumber"], 
                        values.NewValues["LineNumber"] 
                    };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["UnitCost"] = values.NewValues["UnitCost"];
                    row["ReceivedQty"] = values.NewValues["ReceivedQty"];
                    row["QtyCount"] = values.NewValues["QtyCount"];
                }

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["DocNumber"], values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }


                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    //_EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                    _EntityDetail.ReceivedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyCount"]) ? 0 : dtRow["QtyCount"]);

                    _EntityDetail.AddAssetAcquisitionDetail(_EntityDetail);
                }
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
                gv1.DataSource = AcquisitionDetailSource;
                AcquisitionDetailSource.FilterExpression = Session["picklistdetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}