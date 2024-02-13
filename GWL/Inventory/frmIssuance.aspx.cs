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
using GearsInventory;

namespace GWL
{
    public partial class frmIssuance : System.Web.UI.Page
    {
        Boolean error = false; 
        Boolean view = false; 
        Boolean check = false; 
        Boolean edit = false; 

        string Reference;
        string Issued;
        string filter = "";
        string specific = "";
        string refff = "";
        string param = "";

        Entity.Issuance _Entity = new Issuance();//Calls entity odsHeader
        Entity.Issuance.IssuanceDetail _EntityDetail = new Issuance.IssuanceDetail();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

        
                    this.Title = "Material Issuance";

                    txtTransType.Text = "ISSUANCE";

            if (!String.IsNullOrEmpty(filter))
            {
                DataTable filteritem = new DataTable();
                filteritem = Gears.RetriveData2("SELECT REPLACE(Value,',',''',''') AS Value FROM IT.SystemSettings WHERE Code ='" + filter + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in filteritem.Rows)
                {
                    Session["IssFilterItems"] = dt["Value"].ToString();
                }
            }

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            { 
                chkIsWithRef.ReadOnly = true;
            
                chkIsWithRef.Enabled = false; 
            }
            else
            { 
                chkIsWithRef.ReadOnly = false;
             
                chkIsWithRef.Enabled = true; 
            }

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

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); 

            if (!IsPostBack)
            {
                Session["IssFilterItems"] = null;
                Session["IssWithReference"] = null;
                //Session["IssDatatable"] = null;
                Session["IssSamplesTo"] = null;
                Session["IssFilterExpression"] = null;
                Session["IssStepCodeIssuance"] = "";
                Session["IssStepCodeUpdateIssuance"] = "";
                //Session["IssDataTableJO"] = 0;
                Session["IssSpecificType"] = "";
                Session["IssItemCategoryJO"] = "";
                 
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = true;
                }
                //else
                //{
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); 
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                txtTransType.Value = _Entity.TransType.ToString(); 
                Reference = _Entity.RefNumber.ToString();
                Issued = _Entity.IssuedTo.ToString();
                specific = String.IsNullOrEmpty(_Entity.SpecificType) ? "JO Bill Of Material" : _Entity.SpecificType.ToString(); ;
                refff = _Entity.ReqJONumber.ToString();
                aglIssuedTo.Value = _Entity.IssuedTo.ToString();
                aglWarehouseCode.Value = _Entity.WarehouseCode.ToString();
                txtTotalQty.Value = _Entity.TotalQty.ToString();
               
                chkIsWithRef.Value = _Entity.IsWithReference;
                if (chkIsWithRef.Value.ToString() == "true" || chkIsWithRef.Value.ToString() == "True")
                {
                    Session["IssWithReference"] = "1";
                    Enabled();
                }
                else
                {
                    Session["IssWithReference"] = "1";
                    Enabled();
                }
                memRemarks.Value = _Entity.Remarks.ToString();
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
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;
                PostedBy.Text = _Entity.PostedBy;
                PostedDate.Text = _Entity.PostedDate;
                //}

                
                ForSpecificType_LookUp();
               
                SamplesToChanging();
                WithReference();
         
                gv1.KeyFieldName = "LineNumber";
                 
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        aglRefNum.Text = Reference;
                        aglIssuedTo.Text = Issued;
                    
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        aglRefNum.Text = Reference;
                        aglIssuedTo.Text = Issued;
                     
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        aglRefNum.Text = Reference;
                        aglIssuedTo.Text = Issued;
                     
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        aglRefNum.Text = Reference;
                        aglIssuedTo.Text = Issued;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        
                        break;
                }
                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.IssuanceDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
            
            }
        }
        #endregion

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString().Trim();
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsInventory.GInventory.Issuance_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString().Trim();
            gparam._Table = "Inventory.Issuance";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsInventory.GInventory.Issuance_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            //Raven Maglonzo disable  textbox on View and delete mode
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = true;
                text.Enabled = false;
             
            }
            else
            {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = false;
                text.Enabled = true;
               
            }
            //Raven Maglonzo disable  textbox on View and delete mode
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            //ASPxGridLookup look = sender as ASPxGridLookup;
            //look.DropDownButton.Enabled = !view;
            //look.ReadOnly = view;
            //Raven Maglonzo disable  textbox on View and delete mode
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = false;
                look.ReadOnly = true;
             
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = true;
                look.ReadOnly = false;
               

            }
            //Raven Maglonzo disable  textbox on View and delete mode
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                //ASPxGridLookup look = sender as ASPxGridLookup;
                //look.DropDownButton.Enabled = false;
            }
            else
            {
                //ASPxGridLookup look = sender as ASPxGridLookup;
                //look.DropDownButton.Enabled = true;

            }
        }

        protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                //GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                //text.ReadOnly = true;
            }
            else
            {
                //GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                //text.ReadOnly = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxCheckBox check = sender as ASPxCheckBox;
             
                
            }
            else {

                ASPxCheckBox check = sender as ASPxCheckBox;
              
               

            }

        }

        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {

                ASPxComboBox combo = sender as ASPxComboBox;
                combo.DropDownButton.Enabled = false;
                combo.Enabled = false;
                combo.ReadOnly = true;
            }

            else
            {
                ASPxComboBox combo = sender as ASPxComboBox;
                combo.DropDownButton.Enabled = true;
                combo.Enabled = true;
                combo.ReadOnly = false;
            }
        }

        protected void MemoLoad(object sender, EventArgs e)
        {
            // var memo = sender as ASPxMemo;
            // memo.ReadOnly = view;
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {

                ASPxMemo memo = sender as ASPxMemo;
                memo.ReadOnly = true;
            }

            else
            {
                ASPxMemo memo = sender as ASPxMemo;
                memo.ReadOnly = false;
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
            //else
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = false;
            //}
        }

        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxDateEdit date = sender as ASPxDateEdit;
                date.DropDownButton.Enabled = false;
                date.ReadOnly = true;
            }
            else
            {
                ASPxDateEdit date = sender as ASPxDateEdit;
                date.DropDownButton.Enabled = true;
                date.ReadOnly = false;
            }
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
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    if (Request.QueryString["transtype"].ToString() == "INVJOI")
                    {
                        if (Session["IssWithReference"].ToString() == "1")
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                            {
                                e.Visible = false;
                            }
                        }
                        else
                        {
                            //if (aglSpecific.Text == "Design Info Sheet" || aglSpecific.Text == "JO Bill Of Material" || aglSpecific.Text == "Service Order")
                            //{
                            //    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                            //    {
                            //        e.Visible = false;
                            //    }
                            //}
                            //else if (aglSpecific.Text == "Chemical" || aglSpecific.Text == "Factory Supplies")
                            //{
                                //if (!String.IsNullOrEmpty(glReqNum.Text))
                                //{
                                //    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                                //    {
                                //        e.Visible = false;
                                //    }
                                //}
                                //else
                                //{
                                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                                    {
                                        e.Visible = true;
                                    }
                                //}
                            //}
                            //else
                            //{
                            //    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                            //    {
                            //        e.Visible = true;
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        if (Session["IssWithReference"].ToString() == "1")
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                            {
                                e.Visible = false;
                            }
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                            {
                                e.Visible = true;
                            }
                        }
                    }
                }
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "CountSheet")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
            //if (Session["IssWithReference"].ToString() == "1")
            //{
            //    if (e.ButtonID == "Delete")
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}
            //else
            //{
            //if (aglSpecific.Text == "Design Info Sheet" || aglSpecific.Text == "JO Bill Of Material" || aglSpecific.Text == "Service Order")
            //{
            //    if (e.ButtonID == "Delete")
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}
            //else if (aglSpecific.Text == "Chemical" || aglSpecific.Text == "Factory Supplies")
            //{
            //    if (!String.IsNullOrEmpty(glReqNum.Text))
            //    {
            //        if (e.ButtonID == "Delete")
            //        {
            //            e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //        }
            //    }
            //}
            if (e.ButtonID == "Delete")
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            //}

            if (Request.QueryString["entry"] == "E")
            {
                if (e.ButtonID == "CountSheet")
                {
                    if (Request.QueryString["transtype"].ToString() == "INVOSI")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                    else
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }

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
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //if (Session["IssFilterExpression"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = "sdsItemDetail";
            //    sdsItemDetail.FilterExpression = Session["IssFilterExpression"].ToString();
            //}
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
                //COLOR CODE LOOKUP
                DataTable getColor = Gears.RetriveData2("Select DISTINCT ((ColorCode)) AS ColorCode FROM masterfile.item a " +
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

                //CLASS CODE LOOKUP
                DataTable getClass = Gears.RetriveData2("Select DISTINCT ((ClassCode)) AS ClassCode FROM masterfile.item a " +
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

                //SIZE CODE LOOKUP
                DataTable getSize = Gears.RetriveData2("Select DISTINCT ((C.SizeCode)) AS SizeCode, SortOrder FROM masterfile.item a "
                                    + " left join masterfile.itemdetail b on a.itemcode = b.itemcode "
                                    + " INNER JOIN Masterfile.Size C ON b.sizeCode = C.SizeCode "
                                    + " where a.itemcode = '" + itemcode + "'  ORDER BY SortOrder ASC", Session["ConnString"].ToString());//ADD CONN
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

                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,UnitBase,FullDesc, UnitBulk, ISNULL(A.IsByBulk,0) AS IsByBulk, MaterialType FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in countcolor.Rows)
                {
                    //codes = dt["ColorCode"].ToString() + ";";
                    //codes += dt["ClassCode"].ToString() + ";";
                    //codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["UnitBase"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["IsByBulk"].ToString() + ";";
                    codes += dt["MaterialType"].ToString() + ";";
                }

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                if (e.Parameters.Contains("ColorCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode AS [ColorCode], '' AS [ClassCode], '' AS SizeCode, SortOrder FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE A.ItemCode = '" + itemcode + "' ORDER BY SortOrder ASC";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], ClassCode AS [ClassCode], '' AS SizeCode, SortOrder FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE A.ItemCode = '" + itemcode + "' ORDER BY SortOrder ASC";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], ((A.[SizeCode])) AS SizeCode, SortOrder FROM Masterfile.ItemDetail A INNER JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE A.ItemCode = '" + itemcode + "' ORDER BY SortOrder ASC";
                }

                else if (e.Parameters.Contains("Unit"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, ToUnit AS Unit FROM Masterfile.ItemUnit where ItemCode = '" + itemcode + "' UNION ALL select distinct itemcode, UnitBase AS Unit from Masterfile.Item where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "UnitBase";
                }


                //COLOR CODE LOOKUP
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

                //CLASS CODE LOOKUP
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

                //SIZE CODE LOOKUP
                DataTable getSize = Gears.RetriveData2("Select DISTINCT C.SizeCode, SortOrder FROM masterfile.item a "
                                    + " left join masterfile.itemdetail b on a.itemcode = b.itemcode "
                                    + " INNER JOIN Masterfile.Size C ON b.sizeCode = C.SizeCode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
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

                //UNIT BASE LOOKUP
                DataTable getUnit = Gears.RetriveData2("Select DISTINCT UnitBase as Unit FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getUnit.Rows.Count > 1)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnit.Rows)
                    {
                        codes += dt["Unit"].ToString() + ";";
                    }
                }

                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["IssFilterExpression"] = sdsItemDetail.FilterExpression;
                grid.DataSourceID = "sdsItemDetail";
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
            _Entity.Transaction = Request.QueryString["transtype"].ToString();
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.TransType = String.IsNullOrEmpty(txtTransType.Text) ? null : txtTransType.Text;
            //_Entity.Type = String.IsNullOrEmpty(txtType.Text) ? null : txtType.Text;
            _Entity.RefNumber = String.IsNullOrEmpty(aglRefNum.Text) ? null : aglRefNum.Value.ToString();
            _Entity.IssuedTo = String.IsNullOrEmpty(aglIssuedTo.Text) ? null : aglIssuedTo.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouseCode.Text) ? null : aglWarehouseCode.Value.ToString();
            _Entity.TotalQty = String.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToDecimal(txtTotalQty.Text);
            //_Entity.IsPrinted = Convert.ToBoolean(chkIsPrinted.Value.ToString());
            _Entity.IsWithReference = Convert.ToBoolean(chkIsWithRef.Value.ToString());
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.Connection = Session["ConnString"].ToString();
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

            string param = e.Parameter.Split('|')[0];

            switch (param)
            {
                case "Add":

                    gv1.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber, "Inventory.Issuance", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        //if (Session["IssDatatable"] == "1")
                        //{
                        //    //_Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                        //    Gears.RetriveData2("DELETE FROM Inventory.IssuanceDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                        //    gv1.DataSourceID = sdsReqDetail.ID;
                        //    Session["IssStepCodeIssuance"] = Session["IssStepCodeUpdateIssuance"].ToString();
                        //    gv1.UpdateEdit();
                        //    Session["IssStepCodeIssuance"] = "";
                        //}
                        //else
                        //{
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        //}

                     //   _Entity.UpdateUnitFactor(txtDocNumber.Text.Trim(), Session["ConnString"].ToString());

                        Validate();
                        Post();
                        
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
                    }
                    WithReference();
                    break;

                case "Update":

                    gv1.UpdateEdit();

                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Inventory.Issuance", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError1))
                    {
                        cp.JSProperties["cp_message"] = strError1;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        //if (Session["IssDatatable"] == "1")
                        //{
                        //    //_Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                        //    Gears.RetriveData2("DELETE FROM Inventory.IssuanceDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                        //    gv1.DataSourceID = sdsReqDetail.ID;
                        //    Session["IssStepCodeIssuance"] = Session["IssStepCodeUpdateIssuance"].ToString();
                        //    gv1.UpdateEdit();
                        //    Session["IssStepCodeIssuance"] = "";
                        //}
                        //else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                    //    _Entity.UpdateUnitFactor(txtDocNumber.Text, Session["ConnString"].ToString());

                        Validate();
                        Post();
                        
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
                    }
                    WithReference();
                    break;

                case "AddZeroDetail":

                    gv1.UpdateEdit();

                    string strError2 = Functions.Submitted(_Entity.DocNumber, "Inventory.Issuance", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError2))
                    {
                        cp.JSProperties["cp_message"] = strError2;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        _Entity.InitialDelete(txtDocNumber.Text, Session["ConnString"].ToString());
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.IssuanceDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        Validate();
                     //   _Entity.DeleteGL(txtDocNumber.Text.Trim(), Request.QueryString["transtype"].ToString().Trim(), Session["ConnString"].ToString());
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
                    }
                    WithReference();
                    break;

                case "UpdateZeroDetail":

                    gv1.UpdateEdit();

                    string strError3 = Functions.Submitted(_Entity.DocNumber, "Inventory.Issuance", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError3))
                    {
                        cp.JSProperties["cp_message"] = strError3;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.IssuanceDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                        gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                        if (gv1.DataSource != null)
                        {
                            gv1.DataSource = null;
                        }
                        Validate();
                       // _Entity.DeleteGL(txtDocNumber.Text.Trim(), Request.QueryString["transtype"].ToString().Trim(), Session["ConnString"].ToString());
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
                    }
                    WithReference();
                    break;

                case "Delete":
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

                case "CallbackReference":
                    RequestQuery();
                    GetSelectedVal();
                    SamplesToChanging();
                    WithReference();
            
                    cp.JSProperties["cp_generated"] = true;
                    break;

                case "CallbackIssuedTo":
                    RequestQuery();
                    SamplesToChanging();
                    WithReference();
                    CascadeCostCenter();
                    Enabled();
                    break;

                
                case "CallbackWithReference":
                    cp.JSProperties["cp_clear"] = true;
                    RequestQuery();
                    WithReference();
                    //SamplesToChanging();
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    aglRefNum.Text = "";
                   
                      cp.JSProperties["cp_generated"] = true;
                    Enabled();
                    break;

                case "Reload":
                    RequestQuery();
                    cp.JSProperties["cp_generated"] = "Reload";
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            //if (Session["IssDatatable"] == "1" && check == true)
            //{
               ///
               //     var LineNumber = cntr++;
               //     var ItemCode = values.NewValues["ItemCode"];
               //     var FullDesc = values.NewValues["FullDesc"];
               //     var ColorCode = values.NewValues["ColorCode"];
               //     var ClassCode = values.NewValues["ClassCode"];
               //     var SizeCode = values.NewValues["SizeCode"];
               //     var IssuedQty = values.NewValues["IssuedQty"];
               //     var Unit = values.NewValues["Unit"];
               //     var IssuedBulkQty = values.NewValues["IssuedBulkQty"];
               //     var IssuedBulkUnit = values.NewValues["IssuedBulkUnit"];
               //     var RequestedQty = values.NewValues["RequestedQty"];
               //     var IsByBulk = values.NewValues["IsByBulk"];
               //     var AverageCost = values.NewValues["AverageCost"];
               //     var ItemPrice = values.NewValues["ItemPrice"];
               //     var ReturnedQty = values.NewValues["ReturnedQty"];
               //     var ReturnedBulkQty = values.NewValues["ReturnedBulkQty"];
               //     var ReturnedBulkUnit = values.NewValues["ReturnedBulkUnit"];
               //     var StatusCode = values.NewValues["StatusCode"];
               //     var BaseQty = values.NewValues["BaseQty"];
               //     var BarcodeNo = values.NewValues["BarcodeNo"];
               //     var UnitFactor = values.NewValues["UnitFactor"];
               //     var Field1 = values.NewValues["Field1"];
               //     var Field2 = values.NewValues["Field2"];
               //     var Field3 = values.NewValues["Field3"];
               //     var Field4 = values.NewValues["Field4"];
               //     var Field5 = values.NewValues["Field5"];
               //     var Field6 = values.NewValues["Field6"];
               //     var Field7 = values.NewValues["Field7"];
               //     var Field8 = values.NewValues["Field8"];
               //     var Field9 = values.NewValues["Field9"];
               //     var ExpDate = values.NewValues["ExpDate"];
               //     var MfgDate = values.NewValues["MfgDate"];
               //     var BatchNo = values.NewValues["BatchNo"];
               //     var LotNo = values.NewValues["LotNo"];
               //     var Cost = values.NewValues["Cost"];
               //     //source.Rows.Add(ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, IssuedQty, Unit, IssuedBulkQty, IssuedBulkUnit, RequestedQty, IsByBulk, Cost, ItemPrice, ReturnedQty, ReturnedBulkQty, ReturnedBulkUnit, StatusCode, BaseQty, BarcodeNo, UnitFactor, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
               //     source.Rows.Add(LineNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, IsByBulk, IssuedQty, Unit, IssuedBulkQty, IssuedBulkUnit, RequestedQty, AverageCost, ItemPrice, ReturnedQty, ReturnedBulkQty, ReturnedBulkUnit, StatusCode, BaseQty, BarcodeNo, UnitFactor, ExpDate, MfgDate, BatchNo, LotNo, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Cost);

               // }

               // foreach (ASPxDataUpdateValues values in e.UpdateValues)
               // {
               //     object[] keys = { values.NewValues["LineNumber"] };
               //     DataRow row = source.Rows.Find(keys);
               //     row["ItemCode"] = values.NewValues["ItemCode"];
               //     row["FullDesc"] = values.NewValues["FullDesc"];
               //     row["ColorCode"] = values.NewValues["ColorCode"];
               //     row["ClassCode"] = values.NewValues["ClassCode"];
               //     row["SizeCode"] = values.NewValues["SizeCode"];
               //     row["IssuedQty"] = values.NewValues["IssuedQty"];
               //     row["Unit"] = values.NewValues["Unit"];
               //     row["IssuedBulkQty"] = values.NewValues["IssuedBulkQty"];
               //     row["IssuedBulkUnit"] = values.NewValues["IssuedBulkUnit"];
               //     row["RequestedQty"] = values.NewValues["RequestedQty"];
               //     row["IsByBulk"] = values.NewValues["IsByBulk"];
               //     row["AverageCost"] = values.NewValues["AverageCost"];
               //     row["ItemPrice"] = values.NewValues["ItemPrice"];
               //     row["ReturnedQty"] = values.NewValues["ReturnedQty"];
               //     row["ReturnedBulkQty"] = values.NewValues["ReturnedBulkQty"];
               //     row["ReturnedBulkUnit"] = values.NewValues["ReturnedBulkUnit"];
               //     row["StatusCode"] = values.NewValues["StatusCode"];
               //     row["BaseQty"] = values.NewValues["BaseQty"];
               //     row["BarcodeNo"] = values.NewValues["BarcodeNo"];
               //     row["UnitFactor"] = values.NewValues["UnitFactor"];
               //     row["Field1"] = values.NewValues["Field1"];
               //     row["Field2"] = values.NewValues["Field2"];
               //     row["Field3"] = values.NewValues["Field3"];
               //     row["Field4"] = values.NewValues["Field4"];
               //     row["Field5"] = values.NewValues["Field5"];
               //     row["Field6"] = values.NewValues["Field6"];
               //     row["Field7"] = values.NewValues["Field7"];
               //     row["Field8"] = values.NewValues["Field8"];
               //     row["Field9"] = values.NewValues["Field9"];
               //     row["ExpDate"] = values.NewValues["ExpDate"];
               //     row["MfgDate"] = values.NewValues["MfgDate"];
               //     row["BatchNo"] = values.NewValues["BatchNo"];
               //     row["LotNo"] = values.NewValues["LotNo"];
               //     row["Cost"] = values.NewValues["Cost"];
               // }

               // foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
               // {
               //     _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
               //     _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
               //     _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
               //     _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
               //     _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
               //     _EntityDetail.IssuedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["IssuedQty"]) ? "0" : dtRow["IssuedQty"].ToString());
               //     _EntityDetail.Unit = dtRow["Unit"].ToString();
               //     _EntityDetail.IssuedBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["IssuedBulkQty"]) ? "0" : dtRow["IssuedBulkQty"].ToString());
               //     _EntityDetail.IssuedBulkUnit = dtRow["IssuedBulkUnit"].ToString();
               //     _EntityDetail.RequestedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["RequestedQty"]) ? "0" : dtRow["RequestedQty"].ToString());
               //     _EntityDetail.IsByBulk = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsByBulk"]) ? "0" : dtRow["IsByBulk"].ToString());
               //     _EntityDetail.AverageCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["AverageCost"]) ? "0" : dtRow["AverageCost"].ToString());
               //     _EntityDetail.ItemPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["ItemPrice"]) ? "0" : dtRow["ItemPrice"].ToString());
               //     _EntityDetail.ReturnedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedQty"]) ? "0" : dtRow["ReturnedQty"].ToString());
               //     _EntityDetail.ReturnedBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedBulkQty"]) ? "0" : dtRow["ReturnedBulkQty"].ToString());
               //     _EntityDetail.ReturnedBulkUnit = dtRow["ReturnedBulkUnit"].ToString();
               //     _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
               //     _EntityDetail.BaseQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BaseQty"]) ? "0" : dtRow["BaseQty"].ToString());
               //     _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
               //     _EntityDetail.UnitFactor = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFactor"]) ? "0" : dtRow["UnitFactor"].ToString());

               //     _EntityDetail.Cost = Convert.ToDecimal(Convert.IsDBNull(dtRow["Cost"]) ? "0" : dtRow["Cost"].ToString());
               //     _EntityDetail.Field1 = dtRow["Field1"].ToString();
               //     _EntityDetail.Field2 = dtRow["Field2"].ToString();
               //     _EntityDetail.Field3 = dtRow["Field3"].ToString();
               //     _EntityDetail.Field4 = dtRow["Field4"].ToString();
               //     _EntityDetail.Field5 = dtRow["Field5"].ToString();
               //     _EntityDetail.Field6 = dtRow["Field6"].ToString();
               //     _EntityDetail.Field7 = dtRow["Field7"].ToString();
               //     _EntityDetail.Field8 = dtRow["Field8"].ToString();
               //     _EntityDetail.Field9 = dtRow["Field9"].ToString();
               //     _EntityDetail.ExpDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["ExpDate"]) ? null : dtRow["ExpDate"]);
               //     _EntityDetail.MfgDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["MfgDate"]) ? null : dtRow["MfgDate"]);
               //     _EntityDetail.BatchNo = dtRow["BatchNo"].ToString();
               //     _EntityDetail.LotNo = dtRow["LotNo"].ToString();

               //     _EntityDetail.AddIssuanceDetail(_EntityDetail);
               // }
            //}
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["detail"] = null;
            //}

            //if (Session["detail"] != null)
            //{
            //    gv1.DataSourceID = sdsReqDetail.ID;
            //}
        }

        private DataTable GetSelectedVal()
        {
            //Session["IssDatatable"] = "0";
            //if (gv1.DataSourceID != "")
            //{
            //    gv1.DataSourceID = null;
            //}
            //gv1.DataBind();
            //DataTable dt = new DataTable();

            //if (chkIsWithRef.Checked == true)
            //{
            //    string[] selectedValues = aglRefNum.Text.Split(';');
            //    CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
            //    string RefSO = selectionCriteria.ToString();

            //    sdsReqDetail.SelectCommand = "SELECT DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode, B.FullDesc, ColorCode, ClassCode, SizeCode, ISNULL(RequestedQty,0) - ISNULL(IssuedQty,0) AS IssuedQty, Unit, ISNULL(a.RequestedBulkQty,0) as IssuedBulkQty, a.RequestedBulkUnit AS IssuedBulkUnit, RequestedBulkQty, "
            //    + " ISNULL(RequestedQty,0) - ISNULL(IssuedQty,0) AS RequestedQty, IsByBulk, ISNULL(AverageCost,0) AS AverageCost, ISNULL(ItemPrice,0) ItemPrice, 0.0000 AS ReturnedQty, 0.00 AS ReturnedBulkQty, '' AS ReturnedBulkUnit, '' AS StatusCode, 0.0000 AS BaseQty, ISNULL(A.BarcodeNo,'') AS BarcodeNo, ISNULL(A.UnitFactor,'0.0000') UnitFactor, "
            //    + " A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, A.ExpDate, A.MfgDate, A.BatchNo, A.LotNo, "
            //    + " '2' AS Version, 0.00 AS Cost FROM Inventory.RequestDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE DocNumber = '" + aglRefNum.Text + "' "
            //    + " AND ISNULL(RequestedQty,0) - ISNULL(IssuedQty,0) != 0 ";
            //}
            //else
            //{
            //    sdsReqDetail.SelectCommand = CascadeDetailJO(aglSpecific.Text, Session["IssStepCodeIssuance"].ToString());
            //}
            //gv1.DataSource = sdsReqDetail;
            //if (gv1.DataSourceID != "")
            //{
            //    gv1.DataSourceID = null;
            //}
            //gv1.DataBind();
            //Session["IssDatatable"] = "1";

            //foreach (GridViewColumn col in gv1.VisibleColumns)
            //{
            //    GridViewDataColumn dataColumn = col as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    dt.Columns.Add(dataColumn.FieldName);
            //}
            //for (int i = 0; i < gv1.VisibleRowCount; i++)
            //{
            //    DataRow row = dt.Rows.Add();
            //    foreach (DataColumn col in dt.Columns)
            //        row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            //}

            //dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["LineNumber"]};

            //return dt;


            
            gvRefC.DataSourceID = null;


            DataTable dt = new DataTable();
            DataTable getDetail = new DataTable();

            if (chkIsWithRef.Checked == true)
            {
                string[] selectedValues = aglRefNum.Text.Split(';');
                CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
                string RefSO = selectionCriteria.ToString();

                getDetail = Gears.RetriveData2("select B.Docnumber,B.LineNumber,B.ItemCode,C.FullDesc,B.ColorCode,B.ClassCode,B.SizeCode,C.ProductCategoryCode as MtlType,B.Field2 as BatchNo "+
                                            " ,RequestQty as RequestedQty,RequestQty as IssuedQty,B.UnitBase as Unit from Procurement.PurchaseRequest A " +
                                            " INNER JOIN Procurement.PurchaseRequestDetail B "+
                                            " ON A.Docnumber = B.Docnumber "+
                                            " INNER JOIN Masterfile.Item C "+
                                            " ON B.Itemcode = C.ItemCode "+
                                            " WHERE A.DocNumber = '" + aglRefNum.Text + "' ", Session["ConnString"].ToString());
            }
            else
            {
            }

            gvRefC.DataSource = getDetail;
            gvRefC.DataBind();
            gvRefC.KeyFieldName = "Docnumber;LineNumber";
            
            return dt;
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
        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
            FilterItem();

            //gridLookup.DataBind();
        }
        public void FilterItem()
        {
            
                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 ORDER BY ItemCode ASC";
            
        }

        public void FilterItemJO()
        {
            string itemcatJO = "";
            string finalcatJO = "";

     

            DataTable one = new DataTable();
            one = Gears.RetriveData2("SELECT REPLACE(Value,',',''',''') AS Value FROM IT.SystemSettings WHERE Code ='" + itemcatJO + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in one.Rows)
            {
                finalcatJO = dt["Value"].ToString();
            }

            Session["IssItemCategoryJO"] = finalcatJO;

                sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 ORDER BY ItemCode ASC";
            
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.QueryString["transtype"].ToString() == "INVJOI")
            {
                FilterItemJO();
            }
            else
            {
                FilterItem();
            }
            grid.DataSourceID = "sdsItem";
            grid.DataBind();

            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, "ItemCode") != null)
                    if (grid.GetRowValues(i, "ItemCode").ToString() == e.Parameters)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, "ItemCode").ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }
  

        protected void ForSpecificType_LookUp()
        {
            //if (!String.IsNullOrEmpty(aglMaterialType.Text))
            //{
            //    switch (aglMaterialType.Text)
            //    {
            //        case "DIRECT":
            //            sdsJOMat.SelectCommand = "SELECT DISTINCT Code, Description FROM IT.GenericLookup WHERE LookUpKey = 'JODIRECT'";
            //            aglSpecific.DataBind();
            //            break;
            //        case "INDIRECT":
            //            sdsJOMat.SelectCommand = "SELECT DISTINCT Code, Description FROM IT.GenericLookup WHERE LookUpKey = 'JOINDIRECT'";
            //            aglSpecific.DataBind();
            //            break;
            //    }
            //}
        }

        protected void WithReference()
        {
            if (chkIsWithRef.Checked == true || chkIsWithRef.Checked.ToString() == "True" || chkIsWithRef.Checked.ToString() == "true")
            {
                frmlayout1.FindItemOrGroupByName("ReferenceTrans").ClientVisible = true;
                Session["IssWithReference"] = "1";
            }
            else
            {
                frmlayout1.FindItemOrGroupByName("ReferenceTrans").ClientVisible = false;
                if (Request.QueryString["transtype"].ToString() == "INVJOI")
                {
                    Session["IssDatatable"] = Session["IssDataTableJO"];
                }
                else
                    Session["IssDatatable"] = "0";
                Session["IssWithReference"] = "0";
            }
        }

      

        protected void IssuedToBind()
        {
           
                    sdsIssuedTo.SelectCommand = "SELECT StepCode AS Code, Description AS Name FROM Masterfile.Step WHERE ISNULL(IsInactive,0)=0";
                    sdsIssuedTo.DataBind();
           

        }

        protected void RequestQuery()
        {

            UpdateRef();
        }

        protected void aglRefNum_Init(object sender, EventArgs e)
        {
            RequestQuery();
        }



        protected void UpdateRef()
        {

            sdsReference.SelectCommand = " SELECT DocNumber, CONVERT(Date,DocDate,101) AS DocDate, CostCenter as IssuedTo, Remarks FROM Procurement.PurchaseRequest WHERE ( ISNULL(SubmittedBy,'') != '' AND RequestType IN ('RM','SC','SP'))";
             sdsReference.DataBind();
            
        }


        protected void CascadeCostCenter()
        {
            string query = "";

            if (Session["IssSamplesTo"] != null)
            {
                switch (Session["IssSamplesTo"].ToString())
                {
                    case "Customer":
                        query = "SELECT ISNULL(CostCenterCode,'') AS CC FROM Masterfile.BPCustomerInfo WHERE BizPartnerCode = '" + aglIssuedTo.Text.Trim() + "'";
                        break;
                    case "Supplier":
                        query = "SELECT ISNULL(CostCenterCode,'') AS CC FROM Masterfile.BPSupplierInfo WHERE SupplierCode = '" + aglIssuedTo.Text.Trim() + "'";
                        break;
                    case "Employee":
                        query = "SELECT ISNULL(CostCenterCode,'') AS CC FROM Masterfile.BPEmployeeInfo WHERE EmployeeCode = '" + aglIssuedTo.Text.Trim() + "'";
                        break;
                }

            }
        }


        public void SamplesToChanging()
        {

            //sdsIssuedTo.SelectCommand = "SELECT EmployeeCode AS Code, LastName + ', ' + FirstName AS Name FROM Masterfile.BPEmployeeInfo WHERE ISNULL(IsInactive,0) = 0";

            sdsIssuedTo.SelectCommand = "SELECT StepCode AS Code, Description AS Name FROM Masterfile.Step WHERE ISNULL(IsInactive,0)=0";
            sdsIssuedTo.DataBind();
            
        }


        public void Enabled()
        {
            if (chkIsWithRef.Value.ToString() == "true" || chkIsWithRef.Value.ToString() == "True")
            {
                cp.JSProperties["cp_generated"] = true;
                        }
            else
            {
                cp.JSProperties["cp_cusgenerated"] = true;
            }            
        }

        protected void aglIssuedTo_Init(object sender, EventArgs e)
        {
            IssuedToBind();
        }

    

        protected void cmbSamplesTo_Init(object sender, EventArgs e)
        {
            SamplesToChanging();
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                //sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0)=0";
                FilterItem();
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


                DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                                                                     "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getColor.Rows.Count > 1)
                {
                    codes = "" + ";";
                    Session["ColorCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getColor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        Session["ColorCode"] = dt["ColorCode"].ToString();
                    }
                }

                DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                                                                         "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getClass.Rows.Count > 1)
                {
                    codes += "" + ";";
                    Session["ClassCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getClass.Rows)
                    {
                        codes += dt["ClassCode"].ToString() + ";";
                        Session["ClassCode"] = dt["ClassCode"].ToString();
                    }
                }

                DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                                                                             "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getSize.Rows.Count > 1)
                {
                    codes += "" + ";";
                    Session["SizeCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getSize.Rows)
                    {
                        codes += dt["SizeCode"].ToString() + ";";
                        Session["SizeCode"] = dt["SizeCode"].ToString();
                    }
                }


                DataTable getUnitBase = Gears.RetriveData2("SELECT DISTINCT UnitBase AS Unit FROM Masterfile.item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getUnitBase.Rows.Count == 0)
                {
                    codes += "" + ";";
                    Session["UnitBase"] = "";
                }
                else
                {
                    foreach (DataRow dt in getUnitBase.Rows)
                    {
                        codes += dt["Unit"].ToString() + ";";
                    }
                }

                DataTable getRequestedBulkUnit = Gears.RetriveData2("select distinct UnitBulk from Masterfile.Unit a inner join masterfile.Item b ON a.UnitCode = b.UnitBulk where ItemCode =  '" + itemcode + "'", Session["ConnString"].ToString());

                if (getRequestedBulkUnit.Rows.Count == 0)
                {
                    codes += "" + ";";
                    Session["UnitBulk"] = "";
                }
                else
                {
                    foreach (DataRow dt in getRequestedBulkUnit.Rows)
                    {
                        codes += dt["UnitBulk"].ToString() + ";";
                        Session["UnitBulk"] = dt["UnitBulk"].ToString();
                    }

                }

                DataTable getFullDesc = Gears.RetriveData2("select * from masterfile.Item where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                if (getFullDesc.Rows.Count == 0)
                {
                    codes += "" + ";";
                    Session["FullDesc"] = "";
                }
                else
                {
                    foreach (DataRow dt in getFullDesc.Rows)
                    {
                        codes += dt["FullDesc"].ToString() + ";";
                        Session["FullDesc"] = dt["FullDesc"].ToString();
                    }

                }

                DataTable getIsByBulk = Gears.RetriveData2("select ISNULL(IsByBulk,0) AS IsByBulk from masterfile.Item where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                if (getIsByBulk.Rows.Count == 0)
                {
                    codes += "" + ";";
                    Session["IsByBulk"] = "";
                }
                else
                {
                    foreach (DataRow dt in getFullDesc.Rows)
                    {
                        codes += dt["IsByBulk"].ToString() + ";";
                        Session["IsByBulk"] = dt["IsByBulk"].ToString();
                    }

                }

                DataTable getMaterialType = Gears.RetriveData2("SELECT Description AS MaterialType FROM Masterfile.Item A LEFT JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                if (getMaterialType.Rows.Count == 0)
                {
                    codes += "" + ";";
                    Session["MaterialType"] = "";
                }
                else
                {
                    foreach (DataRow dt in getMaterialType.Rows)
                    {
                        codes += dt["MaterialType"].ToString() + ";";
                        Session["MaterialType"] = dt["MaterialType"].ToString();
                    }

                }

                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
    }
}