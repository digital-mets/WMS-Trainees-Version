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
using DevExpress.Data.Filtering;
using GearsSales;

namespace GWL
{
    public partial class frmServiceOrder : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string val = "";   //Boolean for Edit Mode

        Entity.ServiceOrder _Entity = new ServiceOrder();//Calls entity odsHeader
        Entity.ServiceOrder.SOBillOfMaterial _EntityDetail = new ServiceOrder.SOBillOfMaterial();//Call entity sdsDetail
        Entity.ServiceOrder.SOMaterialMovement _EntityMM = new ServiceOrder.SOMaterialMovement();
        Entity.ServiceOrder.SoSizeBreakDown _EntitySizeBreakdown = new ServiceOrder.SoSizeBreakDown();
        Entity.ServiceOrder.SoClassBreakDown _EntityClassBreakdown = new ServiceOrder.SoClassBreakDown();
        Entity.ServiceOrder.SOWorkOrder _EntitySOWork = new ServiceOrder.SOWorkOrder();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            foreach (Control c in form2.Controls)
            {
                if (c is SqlDataSource)
                {
                    ((SqlDataSource)c).ConnectionString = Session["ConnString"].ToString();
                }
            }
        }

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

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }

            dtpdocdate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());
            
            txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
            txtDocnumber.ReadOnly = true;

            if (!IsPostBack)
            {
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        txtStatus.Text = "NEW";

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

                Session["Datatable"] = null;
                Session["SizeGrid"] = null;
                Session["FilterColorCode"] = null;
                Session["FilterItemCode"] = null;
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    gvbom.DataSourceID = null;
                    gvwoi.DataSourceID = null;
                    gvclass.DataSourceID = null;
                    gvsize.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
          
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                  
                }
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpdocdate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                dtpduedate.Text = String.IsNullOrEmpty(_Entity.DueDate.ToString()) ? null : Convert.ToDateTime(_Entity.DueDate.ToString()).ToShortDateString();

                txtStatus.Value = _Entity.Status.ToString();
                if (_Entity.Status.ToString() == "N")
                {
                    txtStatus.Text = "NEW";
                }
                else if (_Entity.Status.ToString() == "W")
                {
                    txtStatus.Text = "WORK IN PROGRESS";
                }
                else if (_Entity.Status.ToString() == "C")
                {
                    txtStatus.Text = "CLOSED";
                    dtpdatecompleted.Text = Convert.ToDateTime(_Entity.DateCompleted.ToString()).ToShortDateString();

                }
                else if (_Entity.Status.ToString() == "X")
                {
                    txtStatus.Text = "MANUAL CLOSED";
                    dtpdatecompleted.Text = Convert.ToDateTime(_Entity.DateCompleted.ToString()).ToShortDateString();

                }
                else if (_Entity.Status.ToString() == "C")
                {
                    txtStatus.Text = "CANCELLED";
                }
                else if (_Entity.Status.ToString() == "A")
                {
                    txtStatus.Text = "PARTIAL MANUAL CLOSED";
                    dtpdatecompleted.Text = Convert.ToDateTime(_Entity.DateCompleted.ToString()).ToShortDateString();

                }
                else
                {
                    txtStatus.Text = "NEW";
                }

                memReason.Text = _Entity.Remarks.ToString();
                aglCustomer.Value = _Entity.CustomerCode.ToString();
                glItemCode.Value = _Entity.ItemCode.ToString();
                MasterfileColor.SelectCommand = "SELECT DISTINCT [ColorCode],[ItemCode] FROM Masterfile.[ItemDetail] WHERE isnull(IsInactive,0)=0 AND ItemCode='" + glItemCode.Text + "'";

                glColorCode.Value = _Entity.ColorCode.ToString();
                txtReferenceNo.Value = _Entity.ReferenceNo.ToString();
                speTotalLabor.Text = _Entity.TotalLabor.ToString();
                speTotalRaw.Text = _Entity.TotalRawMaterials.ToString();
                speUnitCost.Text = _Entity.UnitCost.ToString();
                speEstAcc.Text = _Entity.EstAccCost.ToString();
                speEstUnitCost.Text = _Entity.EstUnitCost.ToString();
                speTotalSVOQty.Text = _Entity.TotalSVOQty.ToString();
                speTotalInQty.Text = _Entity.TotalINQty.ToString();
                speTotalfinalQty.Text = _Entity.TotalFinalQty.ToString();
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
                txtmanualclosed.Text = _Entity.ManualClosedBy;
                txtmanualcloseddate.Text = _Entity.ManualClosedDate;

                Session["FilterColorCode"] = _Entity.ColorCode.ToString();
                Session["FilterItemCode"] = _Entity.ItemCode.ToString();
              

                //V=View, E=Edit, N=New 
                if (Request.QueryString["entry"].ToString() == "N")
                { 
                    popup.ShowOnPageLoad = false;
                    gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                else
                {
                    gv1.DataSourceID = "odsDetail";
                    gvRef.DataSourceID = "odsReference";
                    gvclass.DataSourceID = "odsClass";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }


                DataTable dtbom = _EntityDetail.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
                if (dtbom.Rows.Count > 0)
                {
                    //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

                    odsBOM.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    gvbom.DataSourceID = "odsBOM";
                }
                else
                {
                    gvbom.DataSourceID = "sdsBOM";
                }
                DataTable dtwoi = _EntitySOWork.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
                if (dtwoi.Rows.Count > 0)
                {

                    odsWOI.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    gvwoi.DataSourceID = "odsWOI";
                }
                else
                {
                    gvwoi.DataSourceID = "sdsWOI";
                }

                DataTable dtsize = _EntitySizeBreakdown.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
                if (dtsize.Rows.Count > 0)
                {

                    odsSize.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    gvsize.DataSourceID = "odsSize";
                }
                else
                {
                    gvsize.DataSourceID = "sdsSize";
                }
                DataTable dtmm = _EntityMM.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
                if (dtmm.Rows.Count > 0)
                {
                    
                    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    gv1.DataSourceID = "odsDetail";
                }

                DataTable dtclass = _EntityClassBreakdown.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
                if (dtclass.Rows.Count > 0)
                {
            
                    odsClass.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    gvclass.DataSourceID = "odsClass";
                }

                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Procurement.ServiceOrder where docnumber = '" + txtDocnumber.Text + "' and ISNULL(CustomerCode,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }
                ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                //if (Request.QueryString["iswithdetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                   
                //    gvbom.DataSourceID = "sdsBOM";
                //}
                //else
                //{
                //    gv1.DataSourceID = "odsDetail";
                //    gvbom.DataSourceID = "odsBOM";
                //}
                
            }

            MasterfileColor.SelectCommand = "SELECT  DISTINCT [ColorCode],[ItemCode] FROM Masterfile.[ItemDetail] WHERE isnull(IsInactive,0)=0 AND ItemCode='" + glItemCode.Text + "'";

            if (gv1.DataSource != null)
            {
                gv1.DataSourceID = null;
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
            gparam._TransType = "PRCSCO";
  

            string strresult = GearsProcurement.GProcurement.ServiceOrder_Validate(gparam);
        
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
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
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = true;

            }
        }

        protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = true;
            }
            else
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
            else
            { 
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false; 
            }
            grid.SettingsBehavior.AllowDragDrop = false;
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
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";

            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;

            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }

            if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            {
                //if (Session["iswithdr"] == "1" || chkIsWithQuote.Value.ToString() == "True")
                //{
                //    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                //    {
                //        e.Visible = false;
                //    }
                //}
                //if (Session["iswithdr"] == "0" || chkIsWithQuote.Value.ToString() == "False")
                //{
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = true;
                }
                //}
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }

            
            }
            if (e.ButtonType == ColumnCommandButtonType.Cancel || e.ButtonType == ColumnCommandButtonType.Update)
            {
                e.Visible = false;
            }


        
        }

        protected void gv_CommandButtonInitialize1(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";

            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;

            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }

            if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            {
                //if (Session["iswithdr"] == "1" || chkIsWithQuote.Value.ToString() == "True")
                //{
                //    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                //    {
                //        e.Visible = false;
                //    }
                //}
                //if (Session["iswithdr"] == "0" || chkIsWithQuote.Value.ToString() == "False")
                //{
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = true;
                }
                //}
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }

            }

            ASPxGridView grid = sender as ASPxGridView;

            if (grid.VisibleStartIndex  < 2  )
            {
                (grid.Columns["cmd"] as GridViewCommandColumn).ShowNewButtonInHeader = true;
            }
            else
            {
                (grid.Columns["cmd"] as GridViewCommandColumn).ShowNewButtonInHeader = false;
            }
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

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "Delete")
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
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gvbom") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                if (Session["PRitemID"] != null)
                {
                    if (Session["PRitemID"].ToString() == glIDFinder(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                        Masterfileitemdetail.SelectCommand = Session["PRsql"].ToString();
                        gridLookup.DataBind();
                    }
                }
            }
           
        }
        public string glIDFinder(string glID)
        {
            if (glID.Contains("ColorCode"))
                return "ColorCode";
            else if (glID.Contains("ClassCode"))
                return "ClassCode";
            else if (glID.Contains("SizeCode"))
                return "SizeCode";
            else
                return "Unit";
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("ItemCode"))
            {
                //Get Distinct Color Code
                DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                                                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getColor.Rows.Count > 1)
                {
                    codes = "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getColor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                    }
                }

                DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                                                      "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getClass.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getClass.Rows)
                    {
                        codes += dt["ClassCode"].ToString() + ";";
                    }
                }

                DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                                                                 "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getSize.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getSize.Rows)
                    {
                        codes += dt["SizeCode"].ToString() + ";";
                    }
                }

                DataTable getUnit = Gears.RetriveData2("SELECT DISTINCT UnitBase FROM Masterfile.Item  WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getUnit.Rows.Count == 0)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnit.Rows)
                    {
                        codes += dt["UnitBase"].ToString() + ";";
                    }
                }

                itemlookup.JSProperties["cp_identifier"] = "ItemCode";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                if (e.Parameters.Contains("ColorCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "ColorCode";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "ClassCode";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS ColorCode, ClassCode, '' AS SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "SizeCode";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("UnitBase"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, UnitBase FROM Masterfile.Item where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "UnitBase";
                }

                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[1], "glItemCode");
                //var selectedValues = itemcode;
                //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                //Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["PRsql"] = Masterfileitemdetail.SelectCommand;
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
                _Entity.Connection = Session["ConnString"].ToString(); 
                _Entity.DocNumber = txtDocnumber.Value.ToString();
                _Entity.DocDate = dtpdocdate.Text;
                _Entity.CustomerCode = String.IsNullOrEmpty(aglCustomer.Text) ? null : aglCustomer.Value.ToString();

                _Entity.ReferenceNo = String.IsNullOrEmpty(txtReferenceNo.Text) ? null : txtReferenceNo.Value.ToString();
                _Entity.DueDate = dtpduedate.Text;
                _Entity.DateCompleted = dtpdatecompleted.Text;
                _Entity.ItemCode = glItemCode.Text;
                _Entity.ColorCode = glColorCode.Text;
                _Entity.Status = txtStatus.Text;
                _Entity.Remarks = memReason.Text;
                _Entity.EstAccCost = String.IsNullOrEmpty(speEstAcc.Text) ? 0 : Convert.ToDecimal(speEstAcc.Value.ToString());  
                _Entity.EstUnitCost = String.IsNullOrEmpty(speEstUnitCost.Text) ? 0 : Convert.ToDecimal(speEstUnitCost.Value.ToString());   
                _Entity.TotalSVOQty =   String.IsNullOrEmpty(speTotalSVOQty.Text) ? 0 : Convert.ToDecimal(speTotalSVOQty.Value.ToString());  
                 if (txtStatus.Text == "NEW")
                {
                    _Entity.Status = "N";
                }
                else if (txtStatus.Text == "WORK IN PROGRESS")
                {
                    _Entity.Status = "W";
                }
                else if (txtStatus.Text == "CLOSED")
                {
                    _Entity.Status = "C";
                }
                else if (txtStatus.Text == "MANUAL CLOSED")
                {
                    _Entity.Status = "X";
                }
                else if (txtStatus.Text == "CANCELLED")
                {
                    _Entity.Status = "C";
                }
                else if (txtStatus.Text == "PARTIAL MANUAL CLOSED")
                {
                    _Entity.Status = "A";
                }
                else
                {
                    _Entity.Status = "N";
                }
   
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
              

            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid  
                            
                            gvbom.DataSourceID = "odsBOM";    
                            odsBOM.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                            gvbom.UpdateEdit();//2nd initiation to insert grid  

                             gvwoi.DataSourceID = "odsWOI";
                             odsWOI.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                             gvwoi.UpdateEdit();//2nd initiation to insert grid  
                        
                            gvsize.DataSourceID = "odsSize";
                             odsSize.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                             gvsize.UpdateEdit();//2nd initiation to insert grid  




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
                    gv1.DataSource = null;
                    break;

                case "Item":

                    Session["FilterItemCode"] = glItemCode.Text;
                    glColorCode.Text = "";
                    Session["FilterColorCode"] = glColorCode.Text;
                    MasterfileColor.SelectCommand = "SELECT  DISTINCT [ColorCode],[ItemCode] FROM Masterfile.[ItemDetail] WHERE isnull(IsInactive,0)=0 AND ItemCode='"+glItemCode.Text+"'";
                   glColorCode.DataBind();
                   
                   gvsize.DataSourceID = sdsSize.ID;
                   gvsize.DataBind();
                    break;
                case "Color":
                    Session["FilterItemCode"] = glItemCode.Text;
                    Session["FilterColorCode"] = glColorCode.Text;

                    break;
            }
        }
       


        #endregion

   

     
        protected void dtpdocdate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpdocdate.Date = DateTime.Now;
            }
        }
      
        protected void Size_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glsizecodes")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                sdsSizecode.SelectCommand = "SELECT DISTINCT [SizeCode] , [ItemCode], [ColorCode] FROM Masterfile.[ItemDetail] where isnull(IsInactive,0)=0 AND ItemCode='" + Session["FilterItemCode"] + "' AND ColorCode='" + Session["FilterColorCode"] + "' ";
                gridLookup.DataBind();
            }
        }

        protected void Memo_Load(object sender, EventArgs e)
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
   
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
           
        }
      
        protected void gvbom_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["ByBulk"] = false;
            e.NewValues["MajorMaterial"] = false;
            
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.Item I  WHERE ISNULL(I.IsInactive,0)=0 ";
                gridLookup.DataBind();
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
               && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
               && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                //string changes = param.Split(';')[0];
                string itemcode = param.Split(';')[1];
                if (string.IsNullOrEmpty(itemcode)) return;
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable dtItem = Gears.RetriveData2("SELECT DISTINCT A.ItemCode, ColorCode,ClassCode,SizeCode, FullDesc, UnitBase, ISNULL(EstimatedCost,0) AS Cost FROM Masterfile.Item A INNER JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                DataTable desc = Gears.RetriveData2("SELECT DISTINCT FullDesc, UnitBase, ISNULL(EstimatedCost,0) AS Cost "
                                               + " FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());


                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                 "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                 "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                 "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;


                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());
                cntsze = countsize.Rows.Count;
                if (dtItem.Rows.Count == 1)
                {

                    foreach (DataRow dt in dtItem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["Cost"].ToString() + ";";
                    }

                }
                else
                {
                    if (cntclr == 1)
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                    else
                        codes = ";";
                    if (cntcls == 1)
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                    else
                        codes += ";";
                    if (cntsze == 1)
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                    else
                        codes += ";";
                    codes += desc.Rows[0]["FullDesc"].ToString() + ";";
                    codes += desc.Rows[0]["UnitBase"].ToString() + ";";
                    codes += desc.Rows[0]["Cost"].ToString() + ";";
                  
                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "ItemCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }

        protected void glStockSize_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glStockSize")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                sdsSizecode.SelectCommand = "SELECT DISTINCT [SizeCode] , [ItemCode], [ColorCode] FROM Masterfile.[ItemDetail] where isnull(IsInactive,0)=0 AND ItemCode='" + Session["FilterItemCode"] + "' AND ColorCode='" + Session["FilterColorCode"] + "' ";
                gridLookup.DataBind();
            }
        }

        protected void glWorkCenter_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("WorkCenter")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                sdsWorkCenter.SelectCommand = "SELECT SupplierCode, Name FROM Masterfile.BPSupplierInfo where isnull(IsInactive,0)=0";
                gridLookup.DataBind();
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("WorkCenter")
               && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
               && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                //string changes = param.Split(';')[0];
                string workcenter = param.Split(';')[1];
                if (string.IsNullOrEmpty(workcenter)) return;

                DataTable getdt = Gears.RetriveData2("select TaxCode as VatCode from Masterfile.BPSupplierInfo where  SupplierCode ='" + workcenter + "'", Session["ConnString"].ToString());
                if (getdt.Rows.Count == 1)
                {
                    foreach (DataRow dt in getdt.Rows)
                    {
                        codes = dt[0].ToString() + ";";
                    }
                }



                gridLookup.GridView.JSProperties["cp_identifier"] = "WorkCenter";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }


    }
}