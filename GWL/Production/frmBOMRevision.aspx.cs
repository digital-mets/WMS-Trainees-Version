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
using GearsProduction;

namespace GWL
{
    public partial class frmBOMRevision : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string filter = "";

        Entity.BOMRevision _Entity = new BOMRevision();//Calls entity odsHeader
        Entity.BOMRevision.BOMRevisionDetail _EntityDetail = new BOMRevision.BOMRevisionDetail();//Call entity sdsDetail

        #region page load/entry
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            string referer;
            //try
            //{
            //    referer = Request.ServerVariables["http_referer"];
            //}
            //catch
            //{
            //    referer = "";
            //}

            //if (referer == null)
            //{
            //    Response.Redirect("~/error.aspx");
            //}

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

            if (!IsPostBack)
            {
                Session["BRVFilterExpression"] = null;
                Session["BRVSteps"] = null;
                Session["BRVJONumber"] = null;
                Session["BRVType"] = null;
                Session["BRVOldItemExp"] = null;
                Session["BRVChecker"] = null;
                Session["BRVProductInfoExp"] = null;
                Session["BRVProductInfo"] = null;
                Session["BRVTypeValue"] = null;
                Session["BRVChange"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                }
                sdsReferenceJO.SelectCommand = "SELECT DocNumber, DocDate, DueDate, LeadTime, CustomerCode, TotalJOQty, TotalINQty, TotalFinalQty, Remarks FROM Production.JobOrder WHERE Status IN ('N','W') ORDER BY DocNumber ASC";
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglType.Value = _Entity.Type.ToString();
                aglReferenceJO.Value = _Entity.ReferenceJO.ToString();
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
                txtHApprovedBy.Text = _Entity.ApprovedBy;
                txtHApprovedDate.Text = _Entity.ApprovedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                gv1.KeyFieldName = "LineNumber;DocNumber";

                //V=View, E=Edit, N=New
                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Production.BOMRevisionDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

                Session["BRVJONumber"] = aglReferenceJO.Text;
                Session["BRVType"] = aglType.Value;
                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;
                CheckType();
                InitControls();
                
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDBRV";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProduction.GProduction.BOMRevision_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(ASPxEdit sender)
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void LookupLoad(ASPxEdit sender)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void CheckBoxLoad(ASPxEdit sender)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void ComboBoxLoad(ASPxEdit sender)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void MemoLoad(ASPxEdit sender)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void Date_Load(ASPxEdit sender)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(ASPxEdit sender)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            //if (e.ButtonType == ColumnCommandButtonType.Update)
            //    e.Visible = false;
            string[] test = new string[5];
            if (Request.Params["__CALLBACKID"] != null)
            {
                test = Request.Params["__CALLBACKID"].Split('$');
            }
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || (Request.Params["__CALLBACKID"].Contains(grid.ID)&&test.Count() < 5))
            {
                if (Request.QueryString["entry"] != "N" && Request.QueryString["entry"] != "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit ||
                        e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                }

                if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
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
        }
        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "N")
                {
                    if (e.ButtonID == "Details")
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
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                if (Session["BRVid"] != null)
                {
                    if (Session["BRVid"].ToString() == gridLookup.ID)
                    {
                        FilterLookUp();
                    }
                }
                //gridLookup.DataBind();
            }
            //if (IsCallback && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)
            //    && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
            //    && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            //{
            //    gridLookup.GridView.JSProperties["cp_closelookup"] = true;
            //}

        }
        protected void FilterLookUp()
        {
            if (Session["BRVFilterExpression"] != null)
            {
                string AddCode = Session["BRVFilterExpression"].ToString();

                if (Session["BRVChecker"] == "Color")
                {
                    OldColorQueries();
                    //Session["BRVChecker"] = null;
                }
                else if (Session["BRVChecker"] == "Class")
                {
                    OldClassQueries();
                    //Session["BRVChecker"] = null;
                }
                else if (Session["BRVChecker"] == "Size")
                {
                    OldSizeQueries();
                    //Session["BRVChecker"] = null;
                }
                else if (Session["BRVChecker"] == "NewColor")
                {
                    sdsColor.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE " + AddCode;
                    //Session["BRVChecker"] = null;
                }
                else if (Session["BRVChecker"] == "NewClass")
                {
                    sdsClass.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE " + AddCode;
                    //Session["BRVChecker"] = null;
                }
                else if (Session["BRVChecker"] == "NewSize")
                {
                    sdsSize.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail WHERE " + AddCode;
                    //Session["BRVChecker"] = null;
                }
                else
                {
                    sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, '' AS SizeCode, FullDesc "
                    + " FROM Production.JOBillOfMaterial WHERE " + AddCode;
                    //Session["BRVChecker"] = null;
                }
            }
        }
        protected void OldColorQueries()
        {
            switch (Session["BRVTypeValue"].ToString())
            {
                case "M":
                case "C":
                case "U":
                    sdsOldColor.SelectCommand = "SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, A.ColorCode, '' AS ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Color B ON A.ColorCode = B.ColorCode WHERE " + Session["BRVFilterExpression"].ToString();
                    break;
                case "D":
                    sdsOldColor.SelectCommand = "SELECT DocNumber, StepCode, ItemCode, ColorCode, ClassCode, SizeCode, Description FROM "
                    + " (SELECT DISTINCT A.DocNumber, A.StepCode, A.ItemCode, A.ColorCode, '' AS ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Color B ON A.ColorCode = B.ColorCode "
                    + " LEFT JOIN Production.JOMaterialMovement C ON A.DocNumber = C.DocNumber AND A.ItemCode = C.ItemCode AND A.ColorCode = C.ColorCode "
                    + " WHERE isnull(IssuedQty,0) <= 0) AS X WHERE " + Session["BRVFilterExpression"].ToString();
                    break;
                case "E":
                    sdsOldColor.SelectCommand = "SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, A.ColorCode, '' AS ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Color B ON A.ColorCode = B.ColorCode WHERE ISNULL(IsExcluded,0) = 0 AND " + Session["BRVFilterExpression"].ToString();
                    break;
                case "I":
                    sdsOldColor.SelectCommand = "SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, A.ColorCode, '' AS ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Color B ON A.ColorCode = B.ColorCode WHERE ISNULL(IsExcluded,0) = 1 AND " + Session["BRVFilterExpression"].ToString();
                    break;

            }
        }
        protected void OldClassQueries()
        {
            switch (Session["BRVTypeValue"].ToString())
            {
                case "M":
                case "C":
                case "U":
                    sdsOldClass.SelectCommand = "SELECT DocNumber, StepCode, ItemCode, ColorCode, ClassCode, SizeCode, Description FROM "
                    + " (SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, '' AS ColorCode, A.ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Class B ON A.ClassCode = B.ClassCode) AS X WHERE " + Session["BRVFilterExpression"].ToString();
                    break;
                case "D":
                    sdsOldClass.SelectCommand = "SELECT DocNumber, StepCode, ItemCode, ColorCode, ClassCode, SizeCode, Description FROM "
                    + " (SELECT DISTINCT A.DocNumber, A.StepCode, A.ItemCode, '' AS ColorCode, A.ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Class B ON A.ClassCode = B.ClassCode "
                    + " LEFT JOIN Production.JOMaterialMovement C ON A.DocNumber = C.DocNumber AND A.ItemCode = C.ItemCode AND A.ClassCode = C.ClassCode "
                    + " WHERE isnull(IssuedQty,0) <= 0) AS X WHERE " + Session["BRVFilterExpression"].ToString();
                    break;
                case "E":
                    sdsOldClass.SelectCommand = "SELECT DocNumber, StepCode, ItemCode, ColorCode, ClassCode, SizeCode, Description FROM "
                    + " (SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, '' AS ColorCode, A.ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Class B ON A.ClassCode = B.ClassCode WHERE ISNULL(IsExcluded,0) = 0) AS X WHERE " + Session["BRVFilterExpression"].ToString();
                    break;
                case "I":
                    sdsOldClass.SelectCommand = "SELECT DocNumber, StepCode, ItemCode, ColorCode, ClassCode, SizeCode, Description FROM "
                    + " (SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, '' AS ColorCode, A.ClassCode, '' AS SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Class B ON A.ClassCode = B.ClassCode WHERE ISNULL(IsExcluded,0) = 1) AS X WHERE " + Session["BRVFilterExpression"].ToString();
                    break;

            }
        }
        protected void OldSizeQueries()
        {
            switch (Session["BRVTypeValue"].ToString())
            {
                case "M":
                case "C":
                case "U":
                    sdsOldSize.SelectCommand = "SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, A.SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE " + Session["BRVFilterExpression"].ToString();
                    break;
                case "D":
                    sdsOldSize.SelectCommand = "SELECT DocNumber, StepCode, ItemCode, ColorCode, ClassCode, SizeCode, Description FROM "
                    + " (SELECT DISTINCT A.DocNumber, A.StepCode, A.ItemCode, '' AS ColorCode, '' AS ClassCode, A.SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode "
                    + " LEFT JOIN Production.JOMaterialMovement C ON A.DocNumber = C.DocNumber AND A.ItemCode = C.ItemCode AND A.SizeCode = C.SizeCode "
                    + " WHERE isnull(IssuedQty,0) <= 0) AS X WHERE " + Session["BRVFilterExpression"].ToString();
                    break;
                case "E":
                    sdsOldSize.SelectCommand = "SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, A.SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE ISNULL(IsExcluded,0) = 0 AND " + Session["BRVFilterExpression"].ToString();
                    break;
                case "I":
                    sdsOldSize.SelectCommand = "SELECT DISTINCT A.DocNumber, A.StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, A.SizeCode, B.Description "
                    + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Size B ON A.SizeCode = B.SizeCode WHERE ISNULL(IsExcluded,0) = 1 AND " + Session["BRVFilterExpression"].ToString();
                    break;

            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            //for color
            string colorcode = e.Parameters.Split('|')[3];//Set column value

            //for class
            string classcode = e.Parameters.Split('|')[4];//Set column value

            //for size
            string sizecode = e.Parameters.Split('|')[5];//Set column value

            //for identifier
            string identifier = e.Parameters.Split('|')[6];//Set column value

            //for identifier
            string stepcode = e.Parameters.Split('|')[7];//Set column value
           
            var itemlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("OldItemCode"))
            {
                if (identifier == "item")
                {
                    int cntclr = 0;
                    int cntcls = 0;
                    int cntsze = 0;

                    DataTable countitem = Gears.RetriveData2("SELECT DISTINCT A.ItemCode AS OldItemCode, ColorCode AS OldColorCode, ClassCode AS OldClassCode, SizeCode AS OldSizeCode, "
                    + " ProductCode, ProductColor, ProductSize, ISNULL(UnitCost,0) AS UnitCost, Components, ISNULL(PerPieceConsumption,0) AS PerPieceConsumption, ISNULL(Consumption,0) AS Consumption, "
                    + " B.UnitBase AS Unit, ISNULL(AllowancePerc,0) AS AllowancePerc, ISNULL(AllowanceQty,0) AS AllowanceQty, ISNULL(IsMajorMaterial,0) AS IsMajorMaterial, ISNULL(IsBulk,0) AS IsBulk, "
                    + " ISNULL(IsExcluded,0) AS IsExcluded, ISNULL(A.Components,'') AS Component FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode AS OldColorCode FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode AS OldClassCode FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode AS OldSizeCode FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable unit = Gears.RetriveData2("SELECT DISTINCT UnitBase AS Unit FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                    cntclr = countcolor.Rows.Count;
                    cntcls = countclass.Rows.Count;
                    cntsze = countsize.Rows.Count;

                    if (countitem.Rows.Count == 1)
                    {
                        foreach (DataRow dt in countitem.Rows)
                        {
                            codes = dt["OldColorCode"].ToString() + ";";
                            codes += dt["OldClassCode"].ToString() + ";";
                            codes += dt["OldSizeCode"].ToString() + ";";
                            codes += dt["ProductCode"].ToString() + ";";
                            codes += dt["ProductColor"].ToString() + ";";
                            codes += dt["ProductSize"].ToString() + ";";
                            codes += dt["UnitCost"].ToString() + ";";
                            codes += dt["Components"].ToString() + ";";
                            codes += dt["PerPieceConsumption"].ToString() + ";";
                            codes += dt["Consumption"].ToString() + ";";
                            codes += dt["Unit"].ToString() + ";";
                            codes += dt["AllowancePerc"].ToString() + ";";
                            codes += dt["AllowanceQty"].ToString() + ";";
                            codes += dt["IsMajorMaterial"].ToString() + ";";
                            codes += dt["IsBulk"].ToString() + ";";
                            codes += dt["IsExcluded"].ToString() + ";";
                            codes += dt["Component"].ToString() + ";";
                        }
                    }
                    else
                    {
                        if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                        {
                            codes = ";";
                            codes += countclass.Rows[0]["OldClassCode"].ToString() + ";";
                            codes += countsize.Rows[0]["OldSizeCode"].ToString() + ";";
                            codes += ";;;;;;;";
                            codes += unit.Rows[0]["Unit"].ToString() + ";";
                            codes += ";;;;;;";
                        }
                        else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                        {
                            codes = countcolor.Rows[0]["OldColorCode"].ToString() + ";";
                            codes += ";";
                            codes += countsize.Rows[0]["OldSizeCode"].ToString() + ";";
                            codes += ";;;;;;;";
                            codes += unit.Rows[0]["Unit"].ToString() + ";";
                            codes += ";;;;;;";
                        }
                        else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                        {
                            codes = countcolor.Rows[0]["OldColorCode"].ToString() + ";";
                            codes += countclass.Rows[0]["OldClassCode"].ToString() + ";";
                            codes += ";";
                            codes += ";;;;;;;";
                            codes += unit.Rows[0]["Unit"].ToString() + ";";
                            codes += ";;;;;;";
                        }
                        else if (cntclr > 1 && cntcls > 1 && cntsze == 1)
                        {
                            codes = ";;";
                            codes += countsize.Rows[0]["OldSizeCode"].ToString() + ";";
                            codes += ";;;;;;;";
                            codes += unit.Rows[0]["Unit"].ToString() + ";";
                            codes += ";;;;;;";

                        }
                        else if (cntclr > 1 && cntcls == 1 && cntsze > 1)
                        {
                            codes = ";";
                            codes += countclass.Rows[0]["OldClassCode"].ToString() + ";";
                            codes += ";";
                            codes += ";;;;;;;";
                            codes += unit.Rows[0]["Unit"].ToString() + ";";
                            codes += ";;;;;;";

                        }
                        else if (cntclr == 1 && cntcls > 1 && cntsze > 1)
                        {
                            codes = countcolor.Rows[0]["OldColorCode"].ToString() + ";";
                            codes += ";;";
                            codes += ";;;;;;;";
                            codes += unit.Rows[0]["Unit"].ToString() + ";";
                            codes += ";;;;;;";

                        }
                        else
                        {
                            codes = ";;;;;;;;;;";
                            codes += unit.Rows[0]["Unit"].ToString() + ";";
                            codes += ";;;;;;";
                        }

                    }
                }

                if (identifier == "size")
                {
                    DataTable p = Gears.RetriveData2("SELECT A.ItemCode AS OldItemCode, ColorCode AS OldColorCode, ClassCode AS OldClassCode, SizeCode AS OldSizeCode, "
                    + " ProductCode, ProductColor, ProductSize, ISNULL(UnitCost,0) AS UnitCost, Components, ISNULL(PerPieceConsumption,0) AS PerPieceConsumption, ISNULL(Consumption,0) AS Consumption, "
                    + " B.UnitBase AS Unit, ISNULL(AllowancePerc,0) AS AllowancePerc, ISNULL(AllowanceQty,0) AS AllowanceQty, ISNULL(IsMajorMaterial,0) AS IsMajorMaterial, ISNULL(IsBulk,0) AS IsBulk, "
                    + " ISNULL(IsExcluded,0) AS IsExcluded, ISNULL(A.Components,'') AS Component FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' "
                    + " AND ColorCode = '" + colorcode + "' AND ClassCode = '" + classcode + "' AND SizeCode = '" + sizecode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    foreach (DataRow dt in p.Rows)
                    {
                        codes = dt["ProductCode"].ToString() + ";";
                        codes += dt["ProductColor"].ToString() + ";";
                        codes += dt["ProductSize"].ToString() + ";";
                        codes += dt["UnitCost"].ToString() + ";";
                        codes += dt["Components"].ToString() + ";";
                        codes += dt["PerPieceConsumption"].ToString() + ";";
                        codes += dt["Consumption"].ToString() + ";";
                        codes += dt["Unit"].ToString() + ";";
                        codes += dt["AllowancePerc"].ToString() + ";";
                        codes += dt["AllowanceQty"].ToString() + ";";
                        codes += dt["IsMajorMaterial"].ToString() + ";";
                        codes += dt["IsBulk"].ToString() + ";";
                        codes += dt["IsExcluded"].ToString() + ";";
                        codes += dt["Component"].ToString() + ";";
                    }

                }

                itemlookup.JSProperties["cp_identifier"] = "item";
                itemlookup.JSProperties["cp_codes"] = codes;
                itemlookup.JSProperties["cp_valch"] = true;
            }
            else if (column == "Product")
            {
                string pcode = e.Parameters.Split('|')[8];
                string pcolor = e.Parameters.Split('|')[9];
                string psize = e.Parameters.Split('|')[10];
                DataTable onhand = Gears.RetriveData2("SELECT A.ItemCode AS OldItemCode, ColorCode AS OldColorCode, ClassCode AS OldClassCode, SizeCode AS OldSizeCode, "
                    + " ProductCode, ProductColor, ProductSize, ISNULL(UnitCost,0) AS UnitCost, Components, ISNULL(PerPieceConsumption,0) AS PerPieceConsumption, ISNULL(Consumption,0) AS Consumption, "
                    + " B.UnitBase AS Unit, ISNULL(AllowancePerc,0) AS AllowancePerc, ISNULL(AllowanceQty,0) AS AllowanceQty, ISNULL(IsMajorMaterial,0) AS IsMajorMaterial, ISNULL(IsBulk,0) AS IsBulk, "
                    + " ISNULL(IsExcluded,0) AS IsExcluded, ISNULL(A.Components,'') AS Component FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' "
                    + " AND ColorCode = '" + colorcode + "' AND ClassCode = '" + classcode + "' AND SizeCode = '" + sizecode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND isnull(ProductCode,'') = '"+pcode+"' AND isnull(ProductColor,'') = '"+pcolor+"'  AND isnull(ProductSize,'') = '"+psize+"'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                foreach (DataRow dt in onhand.Rows)
                {
                    codes = dt["ProductCode"].ToString() + ";";
                    codes += dt["ProductColor"].ToString() + ";";
                    codes += dt["ProductSize"].ToString() + ";";
                    codes += dt["UnitCost"].ToString() + ";";
                    codes += dt["Components"].ToString() + ";";
                    codes += dt["PerPieceConsumption"].ToString() + ";";
                    codes += dt["Consumption"].ToString() + ";";
                    codes += dt["Unit"].ToString() + ";";
                    codes += dt["AllowancePerc"].ToString() + ";";
                    codes += dt["AllowanceQty"].ToString() + ";";
                    codes += dt["IsMajorMaterial"].ToString() + ";";
                    codes += dt["IsBulk"].ToString() + ";";
                    codes += dt["IsExcluded"].ToString() + ";";
                    codes += dt["Component"].ToString() + ";";
                }

                codes = string.IsNullOrEmpty(codes) ? ";;;0;;0;0;;0;0;;;;" : codes;

                itemlookup.JSProperties["cp_identifier"] = "size";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("OldColorCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                var selectedValues = itemcode;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[5], "glColorCode");
                CriteriaOperator selection = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                CriteriaOperator selectionCriteria1 = new InOperator("StepCode", new string[] { stepcode });
                sdsOldColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selection, selectionCriteria, selectionCriteria1)).ToString();
                Session["BRVChecker"] = "Color";
                Session["BRVid"] = "glColorCode";
                Session["BRVFilterExpression"] = sdsOldColor.FilterExpression;
                FilterLookUp();
                grid.DataSourceID = "sdsOldColor";
                grid.DataBind();

                if(val != "")
                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "ColorCode") != null)
                        if (grid.GetRowValues(i, "ColorCode").ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "ColorCode").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }

                if (val == "")
                {
                    lookup.Value = null;
                }

                itemlookup.JSProperties["cp_identifier"] = "sku";

            }
            else if (e.Parameters.Contains("OldClassCode"))
            {
                ASPxGridView grid1 = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[6], "glClassCode");
                var selectedValues1 = itemcode;
                CriteriaOperator selection = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                CriteriaOperator selectionCriteria1 = new InOperator("StepCode", new string[] { stepcode });
                sdsOldClass.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selection, selectionCriteria, selectionCriteria1)).ToString();
                Session["BRVChecker"] = "Class";
                Session["BRVid"] = "glClassCode";
                Session["BRVFilterExpression"] = sdsOldClass.FilterExpression;
                FilterLookUp();
                grid1.DataSourceID = "sdsOldClass";
                grid1.DataBind();

                if (val != "")
                for (int i = 0; i < grid1.VisibleRowCount; i++)
                {
                    if (grid1.GetRowValues(i, "ClassCode") != null)
                        if (grid1.GetRowValues(i, "ClassCode").ToString() == val)
                        {
                            grid1.Selection.SelectRow(i);
                            string key = grid1.GetRowValues(i, "ClassCode").ToString();
                            grid1.MakeRowVisible(key);
                            break;
                        }
                }

                if (val == "")
                {
                    lookup.Value = null;
                }


                itemlookup.JSProperties["cp_identifier"] = "sku";
            }
            else if (e.Parameters.Contains("OldSizeCode"))
            {
                ASPxGridView grid2 = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[7], "glSizeCode");
                var selectedValues2 = itemcode;
                CriteriaOperator selection = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                CriteriaOperator selectionCriteria1 = new InOperator("StepCode", new string[] { stepcode });
                sdsOldSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selection, selectionCriteria, selectionCriteria1)).ToString();
                Session["BRVChecker"] = "Size";
                Session["BRVid"] = "glSizeCode";
                Session["BRVFilterExpression"] = sdsOldSize.FilterExpression;
                FilterLookUp();
                grid2.DataSourceID = "sdsOldSize";
                grid2.DataBind();

                if (val != "")
                for (int i = 0; i < grid2.VisibleRowCount; i++)
                {
                    if (grid2.GetRowValues(i, "SizeCode") != null)
                        if (grid2.GetRowValues(i, "SizeCode").ToString() == val)
                        {
                            grid2.Selection.SelectRow(i);
                            string key = grid2.GetRowValues(i, "SizeCode").ToString();
                            grid2.MakeRowVisible(key);
                            break;
                        }
                }

                if (val == "")
                {
                    lookup.Value = null;
                }

                itemlookup.JSProperties["cp_identifier"] = "sku";
            }
            else if (e.Parameters.Contains("NewItemCode"))
            {
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable newdata = Gears.RetriveData2("SELECT DISTINCT ItemCode, ColorCode, ClassCode, SizeCode FROM Masterfile.ItemDetail "
                                        + " WHERE ISNULL(IsInactive,0) = 0 AND ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                DataTable unit = Gears.RetriveData2("SELECT DISTINCT ISNULL(UnitBase,'') Unit, ISNULL(EstimatedCost,0) AS UnitCost, ISNULL(B.AllowancePerc,0) AS AllowancePerc FROM Masterfile.Item A LEFT JOIN Masterfile.ProductCategorySub B ON A.ProductSubCatCode = B.ProductSubCatCode WHERE ISNULL(A.IsInactive,0) = 0 AND A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode
                                                        + "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode
                                                        + "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode
                                                        + "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString()); ;

                cntsze = countsize.Rows.Count;

                if (newdata.Rows.Count == 1)
                {
                    foreach (DataRow dt in newdata.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }
                    else if (cntclr > 1 && cntcls > 1 && cntsze == 1)
                    {
                        codes = ";;";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else if (cntclr > 1 && cntcls == 1 && cntsze > 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else if (cntclr == 1 && cntcls > 1 && cntsze > 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";;";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else
                    {
                        codes = ";;;";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }

                }

                itemlookup.JSProperties["cp_identifier"] = "itemN";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("NewColorCode"))
            {
                ASPxGridView grid3 = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[12], "glNewColorCode");
                CriteriaOperator selection = new InOperator("ItemCode", new string[] { itemcode });
                sdsColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selection)).ToString();
                Session["BRVChecker"] = "NewColor";
                Session["BRVid"] = "glNewColorCode";
                Session["BRVFilterExpression"] = sdsColor.FilterExpression;
                FilterLookUp();
                grid3.DataSourceID = "sdsColor";
                grid3.DataBind();

                if (val != "")
                for (int i = 0; i < grid3.VisibleRowCount; i++)
                {
                    if (grid3.GetRowValues(i, "ColorCode") != null)
                        if (grid3.GetRowValues(i, "ColorCode").ToString() == val)
                        {
                            grid3.Selection.SelectRow(i);
                            string key = grid3.GetRowValues(i, "ColorCode").ToString();
                            grid3.MakeRowVisible(key);
                            break;
                        }
                }

                if (val == "")
                {
                    lookup.Value = null;
                }

                itemlookup.JSProperties["cp_identifier"] = "skuN";
            }
            else if (e.Parameters.Contains("NewClassCode"))
            {
                ASPxGridView grid4 = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[13], "glNewClassCode");
                CriteriaOperator selection = new InOperator("ItemCode", new string[] { itemcode });
                sdsClass.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selection)).ToString();
                Session["BRVChecker"] = "NewClass";
                Session["BRVid"] = "glNewClassCode";
                Session["BRVFilterExpression"] = sdsClass.FilterExpression;
                FilterLookUp();
                grid4.DataSourceID = "sdsClass";
                grid4.DataBind();

                if (val != "")
                for (int i = 0; i < grid4.VisibleRowCount; i++)
                {
                    if (grid4.GetRowValues(i, "ClassCode") != null)
                        if (grid4.GetRowValues(i, "ClassCode").ToString() == val)
                        {
                            grid4.Selection.SelectRow(i);
                            string key = grid4.GetRowValues(i, "ClassCode").ToString();
                            grid4.MakeRowVisible(key);
                            break;
                        }
                }

                if (val == "")
                {
                    lookup.Value = null;
                }

                itemlookup.JSProperties["cp_identifier"] = "skuN";
            }
            else if (e.Parameters.Contains("NewSizeCode"))
            {
                ASPxGridView grid5 = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[14], "glNewSizeCode");
                CriteriaOperator selection = new InOperator("ItemCode", new string[] { itemcode });
                sdsSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selection)).ToString();
                Session["BRVChecker"] = "NewSize";
                Session["BRVid"] = "glNewSizeCode";
                Session["BRVFilterExpression"] = sdsSize.FilterExpression;
                FilterLookUp();
                grid5.DataSourceID = "sdsSize";
                grid5.DataBind();

                if (val != "")
                for (int i = 0; i < grid5.VisibleRowCount; i++)
                {
                    if (grid5.GetRowValues(i, "SizeCode") != null)
                        if (grid5.GetRowValues(i, "SizeCode").ToString() == val)
                        {
                            grid5.Selection.SelectRow(i);
                            string key = grid5.GetRowValues(i, "SizeCode").ToString();
                            grid5.MakeRowVisible(key);
                            break;
                        }
                }

                if (val == "")
                {
                    lookup.Value = null;
                }

                itemlookup.JSProperties["cp_identifier"] = "skuN";
            }
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Type = String.IsNullOrEmpty(aglType.Text) ? null : aglType.Value.ToString();
            _Entity.ReferenceJO = String.IsNullOrEmpty(aglReferenceJO.Text) ? null : aglReferenceJO.Value.ToString();
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;

            //_Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            DataTable dt = new DataTable();

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

            switch (e.Parameter)
            {
                case "Add":

                    //gv1.UpdateEdit();

                    string strError = Functions.Submitted(_Entity.DocNumber, "Production.BOMRevision", 2, Session["ConnString"].ToString());
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
                        _Entity.UpdateData(_Entity);

                        //if (Session["BRVChange"] == "1")
                        //{
                        //    _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                        //}
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

                        //_Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                        //gv1.DataSource = dt;
                        //if (gv1.DataSourceID != null)
                        //{
                        //    gv1.DataSourceID = null;
                        //}
                        //gv1.UpdateEdit();

                        Validate();
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

                    CheckType();

                    break;

                case "Update":

                    //gv1.UpdateEdit();

                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Production.BOMRevision", 2, Session["ConnString"].ToString());
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
                        _Entity.UpdateData(_Entity);

                        //if (Session["BRVChange"] == "1")
                        //{
                        //    _Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                        //}

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

                        //_Entity.DeleteFirstData(txtDocNumber.Text, Session["ConnString"].ToString());
                        //gv1.DataSource = dt;
                        //if (gv1.DataSourceID != null)
                        //{
                        //    gv1.DataSourceID = null;
                        //}
                        //gv1.UpdateEdit();

                        Validate();
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

                    CheckType();

                    break;

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    CheckType();
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    //gv1.DataSource = null;
                    break;

                case "RefGrid":
                    gv1.DataBind();
                    CheckType();
                    break;

                case "CallbackReferenceJO":
                    //gv1.DataSourceID = "sdsDetail";
                    //gv1.DataSourceID = null;
                    //if (gv1.DataSource != null)
                    //{
                    //    gv1.DataSource = null;
                    //}
                    gv1.DataBind();
                    //Session["BRVJONumber"] = null;
                    Session["BRVJONumber"] = aglReferenceJO.Text;
                    CheckType();
                    cp.JSProperties["cp_resetdetail"] = true;
                    break;

                case "CallbackType":
                    Session["BRVChange"] = "1";
                    Session["BRVType"] = aglType.Value; 
                    //gv1.DataSourceID = "sdsDetail";
                    //if (gv1.DataSource != null)
                    //{
                    //    gv1.DataSource = null;
                    //}
                    gv1.DataBind();
                    CheckType();
                    cp.JSProperties["cp_resetdetail"] = true;
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
            //if (check == false)//Prevents updating of grid to enable validation
            //{
            //    e.Handled = true;
            //    e.InsertValues.Clear();
            //    e.UpdateValues.Clear();
            //    if (Session["BRVChange"] == "1")
            //    {
            //    e.DeleteValues.Clear();
            //    }
            //}
            
               
        }
        #endregion
        protected void Connection_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //Session["userid"] = "1828";
            //Session["ConnString"] = "Data Source=192.168.201.115;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;connection timeout=1800;";
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //}
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsReferenceJO.ConnectionString = Session["ConnString"].ToString();
            //sdsSteps.ConnectionString = Session["ConnString"].ToString();
            //sdsType.ConnectionString = Session["ConnString"].ToString();
            //sdsOldItem.ConnectionString = Session["ConnString"].ToString();
            //sdsOldColor.ConnectionString = Session["ConnString"].ToString();
            //sdsOldClass.ConnectionString = Session["ConnString"].ToString();
            //sdsOldSize.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsColor.ConnectionString = Session["ConnString"].ToString();
            //sdsClass.ConnectionString = Session["ConnString"].ToString();
            //sdsSize.ConnectionString = Session["ConnString"].ToString();
            //sdsProductCode.ConnectionString = Session["ConnString"].ToString();
            //sdsProductColor.ConnectionString = Session["ConnString"].ToString();
            //sdsProductSize.ConnectionString = Session["ConnString"].ToString();
            //sdsComponents.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsUnit.ConnectionString = Session["ConnString"].ToString();
            //sdsBulkUnit.ConnectionString = Session["ConnString"].ToString();
            
        }
        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID) && !Request.Params["__CALLBACKPARAM"].Contains("GLP_AC"))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                OldItemQueries();
                //gridLookup.DataBind();
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

                string stepcode = Session["Bomrevcurstep"].ToString();

                int cntclr = 0;
                    int cntcls = 0;
                    int cntsze = 0;

                    DataTable countitem = Gears.RetriveData2("SELECT DISTINCT A.ItemCode AS OldItemCode, ColorCode AS OldColorCode, ClassCode AS OldClassCode, SizeCode AS OldSizeCode, "
                    + " ProductCode, ProductColor, ProductSize, ISNULL(UnitCost,0) AS UnitCost, Components, ISNULL(PerPieceConsumption,0) AS PerPieceConsumption, ISNULL(Consumption,0) AS Consumption, "
                    + " B.UnitBase AS Unit, ISNULL(AllowancePerc,0) AS AllowancePerc, ISNULL(AllowanceQty,0) AS AllowanceQty, ISNULL(IsMajorMaterial,0) AS IsMajorMaterial, ISNULL(IsBulk,0) AS IsBulk, "
                    + " ISNULL(IsExcluded,0) AS IsExcluded, ISNULL(A.Components,'') AS Component FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode AS OldColorCode FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode AS OldClassCode FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode AS OldSizeCode FROM Production.JOBillOfMaterial A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND A.StepCode = '" + stepcode + "'"
                    + " AND A.DocNumber = '" + Session["BRVJONumber"].ToString() + "'", Session["ConnString"].ToString());

                    DataTable unit = Gears.RetriveData2("SELECT DISTINCT UnitBase AS Unit FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                    cntclr = countcolor.Rows.Count;
                    cntcls = countclass.Rows.Count;
                    cntsze = countsize.Rows.Count;

                    if (countitem.Rows.Count == 1)
                    {
                        foreach (DataRow dt in countitem.Rows)
                        {
                            codes = dt["OldColorCode"].ToString() + ";";
                            codes += dt["OldClassCode"].ToString() + ";";
                            codes += dt["OldSizeCode"].ToString() + ";";
                            codes += dt["ProductCode"].ToString() + ";";
                            codes += dt["ProductColor"].ToString() + ";";
                            codes += dt["ProductSize"].ToString() + ";";
                            codes += dt["UnitCost"].ToString() + ";";
                            codes += dt["Components"].ToString() + ";";
                            codes += dt["PerPieceConsumption"].ToString() + ";";
                            codes += dt["Consumption"].ToString() + ";";
                            codes += dt["Unit"].ToString() + ";";
                            codes += dt["AllowancePerc"].ToString() + ";";
                            codes += dt["AllowanceQty"].ToString() + ";";
                            codes += dt["IsMajorMaterial"].ToString() + ";";
                            codes += dt["IsBulk"].ToString() + ";";
                            codes += dt["IsExcluded"].ToString() + ";";
                            codes += dt["Component"].ToString() + ";";
                        }
                    }

                    gridLookup.GridView.JSProperties["cp_identifier"] = "item";
                    gridLookup.GridView.JSProperties["cp_codes"] = codes;
                    gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
        protected void OldItemQueries()
        {
            if (Session["BRVOldItemExp"] != null)
            {
                switch (Session["BRVTypeValue"].ToString())
                {
                    case "M":
                    case "C":
                    case "U":
                        sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, '' AS SizeCode, FullDesc "
                        + " FROM Production.JOBillOfMaterial WHERE " + Session["BRVOldItemExp"].ToString();
                        break;
                    case "D":
                        sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, StepCode, ItemCode,'' ColorCode,'' ClassCode,'' SizeCode,Description FullDesc FROM "
                        + " (SELECT DISTINCT A.DocNumber, A.StepCode, A.ItemCode, A.ColorCode, '' AS ClassCode, '' AS SizeCode, A.FullDesc AS Description "
                        + " FROM Production.JOBillOfMaterial A LEFT JOIN Masterfile.Color B ON A.ColorCode = B.ColorCode "
                        + " LEFT JOIN Production.JOMaterialMovement C ON A.DocNumber = C.DocNumber AND A.ItemCode = C.ItemCode AND A.ColorCode = C.ColorCode "
                        + " WHERE isnull(IssuedQty,0) <= 0) AS X WHERE " + Session["BRVOldItemExp"].ToString();
                        break;
                    case "E":
                        sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, '' AS SizeCode, FullDesc "
                        + " FROM Production.JOBillOfMaterial WHERE ISNULL(IsExcluded,0) = 0 AND " + Session["BRVOldItemExp"].ToString();
                        break;
                    case "I":
                        sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, '' AS SizeCode, FullDesc "
                        + " FROM Production.JOBillOfMaterial WHERE ISNULL(IsExcluded,0) = 1 AND " + Session["BRVOldItemExp"].ToString();
                        break;
                    //case "M":
                    //case "C":
                    //case "U":
                    //case "D":
                    //    sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, '' AS StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, '' AS SizeCode, FullDesc "
                    //    + " FROM Production.JOBillOfMaterial WHERE " + Session["BRVOldItemExp"].ToString();
                    //    break;
                    //case "E":
                    //    sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, '' AS StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, '' AS SizeCode, FullDesc "
                    //    + " FROM Production.JOBillOfMaterial WHERE ISNULL(IsExcluded,0) = 0 AND " + Session["BRVOldItemExp"].ToString();
                    //    break;
                    //case "I":
                    //    sdsOldItem.SelectCommand = "SELECT DISTINCT DocNumber, '' AS StepCode, ItemCode, '' AS ColorCode, '' AS ClassCode, '' AS SizeCode, FullDesc "
                    //    + " FROM Production.JOBillOfMaterial WHERE ISNULL(IsExcluded,0) = 1 AND " + Session["BRVOldItemExp"].ToString();
                    //    break;
                }
            }
        }
        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string step = e.Parameters.Split('|')[0];
            if (step.Contains("GLP_AIC") || step.Contains("GLP_AC") || step.Contains("GLP_F")) return;

            Session["Bomrevcurstep"] = step;
            ASPxGridView grid = sender as ASPxGridView;
            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[4], "glItemCode");
            CriteriaOperator selection = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
            CriteriaOperator selectionCriteria = new InOperator("StepCode", new string[] { step });
            sdsOldItem.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selection, selectionCriteria)).ToString();

            if (aglType.Value.ToString() == "M" || aglType.Value.ToString() == "C")
            {
                Session["BRVOldItemExp"] = sdsOldItem.FilterExpression + " and isnull(IsExcluded,0) = 0";
            }
            else
                Session["BRVOldItemExp"] = sdsOldItem.FilterExpression;
            
            OldItemQueries();
            grid.DataSourceID = "sdsOldItem";
            grid.DataBind();
            //grid.Selection.UnselectAll();
            string onsitem = e.Parameters.Split('|')[1];
            string glvalue = e.Parameters.Split('|')[2];

            if (onsitem != "null")
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, "ItemCode") != null)
                {
                    if (grid.GetRowValues(i, "ItemCode").ToString() == onsitem)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, "ItemCode").ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
                }
            }

            if (onsitem == "null" && glvalue == "null")
            {
                lookup.Value = null;
            }
            //var lookup = sender as ASPxGridView;
        }
        protected void glStepcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glStepcode_CustomCallback);
                Session["BRVSteps"] = "SELECT DISTINCT UPPER(A.StepCode) AS StepCode, Description FROM Production.JOStepPlanning A INNER JOIN Masterfile.Step B ON A.StepCode = B.StepCode WHERE DocNumber = '" + Session["BRVJONumber"].ToString() + "'";
                sdsSteps.SelectCommand = Session["BRVSteps"].ToString();
                //gridLookup.DataBind();
            }
        }
        public void glStepcode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = "sdsSteps";
            Session["BRVSteps"] = "SELECT DISTINCT A.StepCode, Description FROM Production.JOStepPlanning A LEFT JOIN Masterfile.Step B ON A.StepCode = B.StepCode WHERE DocNumber = '" + Session["BRVJONumber"].ToString() + "'";
            sdsSteps.SelectCommand = Session["BRVSteps"].ToString();
            grid.DataBind();

            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, "StepCode") != null)
                    if (grid.GetRowValues(i, "StepCode").ToString() == e.Parameters)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, "StepCode").ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }
        protected void glProductCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ProductCode_CustomCallback);
                Session["BRVProductInfo"] = "Code";
                FilterProducts();
                gridLookup.DataBind();
            }
        }
        public void ProductCode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string step = e.Parameters.Split('|')[0];
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            string item = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
            //if (item.Contains("GLP_AIC") || item.Contains("GLP_AC") || item.Contains("GLP_F")) return;

            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[8], "glProductCode");
            ASPxGridView grid = sender as ASPxGridView;
            CriteriaOperator JONumber = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
            CriteriaOperator StepCodeVal = new InOperator("StepCode", new string[] { step });
            CriteriaOperator ItemCodeVal = new InOperator("ItemCode", new string[] { item });
            //if (aglType.Value.ToString().Trim() != "A")
            //{
            //    sdsProductCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber, StepCodeVal, ItemCodeVal)).ToString();
            //}
            //else
            //{
            
            //}
            sdsProductCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber)).ToString();
            Session["BRVProductInfoExp"] = sdsProductCode.FilterExpression;
            sdsProductCode.FilterExpression = null;
            Session["BRVProductInfo"] = "Code";
            FilterProducts();
            grid.DataSourceID = "sdsProductCode";
            grid.DataBind();

            if (val != "")
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, "ProductCode") != null)
                    if (grid.GetRowValues(i, "ProductCode").ToString().Trim() == val)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, "ProductCode").ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }

            if (val == "")
            {
                lookup.Value = null;
            }
        }
        protected void glProductColor_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ProductColor_CustomCallback);
                Session["BRVProductInfo"] = "Color";
                FilterProducts();
                gridLookup.DataBind();
            }
        }
        public void ProductColor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string step = e.Parameters.Split('|')[0];
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            string item = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
            string color = e.Parameters.Split('|')[3];
            string classs = e.Parameters.Split('|')[4];
            string size = e.Parameters.Split('|')[5];
            string pcode = e.Parameters.Split('|')[6];
            string pcolor = e.Parameters.Split('|')[7];

            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[9], "glProductColor");
            ASPxGridView grid = sender as ASPxGridView;
            CriteriaOperator JONumber = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
            CriteriaOperator StepCodeVal = new InOperator("StepCode", new string[] { step });
            CriteriaOperator ItemCodeVal = new InOperator("ItemCode", new string[] { item });
            CriteriaOperator ColorCodeVal = new InOperator();
            CriteriaOperator ClassCodeVal = new InOperator();
            CriteriaOperator SizeCodeVal = new InOperator();
            CriteriaOperator PCodeVal = new InOperator();
            CriteriaOperator PColorVal = new InOperator();
            if (aglType.Value.ToString() != "A")
            {
                ColorCodeVal = new InOperator("ColorCode", new string[] { color });
                ClassCodeVal = new InOperator("ClassCode", new string[] { classs });
                SizeCodeVal = new InOperator("SizeCode", new string[] { size });
                PCodeVal = new InOperator("ProductCode", new string[] { pcode });
            }
            if (aglType.Value.ToString() != "A")
                sdsProductColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber, StepCodeVal, ItemCodeVal, ColorCodeVal, ClassCodeVal, SizeCodeVal, PCodeVal)).ToString();
            else
                sdsProductColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber)).ToString();

            Session["BRVProductInfoExp"] = sdsProductColor.FilterExpression;
            Session["BRVProductInfo"] = "Color";
            sdsProductColor.FilterExpression = null;
            FilterProducts();
            grid.DataSourceID = "sdsProductColor";
            grid.DataBind();

            if (val != "")
                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "ProductColor") != null)
                        if (grid.GetRowValues(i, "ProductColor").ToString().Trim() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "ProductColor").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }

            if (val == "")
            {
                lookup.Value = null;
            }

        }
        protected void glProductSize_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ProductSize_CustomCallback);
                Session["BRVProductInfo"] = "Size";
                FilterProducts();
                gridLookup.DataBind();
            }
        }
        public void ProductSize_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string step = e.Parameters.Split('|')[0];
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            string item = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
            string color = e.Parameters.Split('|')[3];
            string classs = e.Parameters.Split('|')[4];
            string size = e.Parameters.Split('|')[5];
            string pcode = e.Parameters.Split('|')[6];
            string pcolor = e.Parameters.Split('|')[7];

            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[10], "glProductSize");
            ASPxGridView grid = sender as ASPxGridView;
            CriteriaOperator JONumber = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
            CriteriaOperator StepCodeVal = new InOperator("StepCode", new string[] { step });
            CriteriaOperator ItemCodeVal = new InOperator("ItemCode", new string[] { item });
            CriteriaOperator ColorCodeVal = new InOperator();
            CriteriaOperator ClassCodeVal = new InOperator();
            CriteriaOperator SizeCodeVal = new InOperator();
            CriteriaOperator PCodeVal = new InOperator();
            CriteriaOperator PColorVal = new InOperator();
            if (aglType.Value.ToString() != "A")
            {
                ColorCodeVal = new InOperator("ColorCode", new string[] { color });
                ClassCodeVal = new InOperator("ClassCode", new string[] { classs });
                SizeCodeVal = new InOperator("SizeCode", new string[] { size });
                PCodeVal = new InOperator("ProductCode", new string[] { pcode });
                PColorVal = new InOperator("ProductColor", new string[] { pcolor });
            }
            //if (aglType.Value.ToString().Trim() != "A")
            //{
            //    sdsProductSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber, StepCodeVal, ItemCodeVal)).ToString();
            //}
            //else
            //{
            if (aglType.Value.ToString() != "A")
                sdsProductSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber, StepCodeVal, ItemCodeVal, ColorCodeVal, ClassCodeVal, SizeCodeVal, PCodeVal, PColorVal)).ToString();
            else
                sdsProductSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber)).ToString();
            //}
            Session["BRVProductInfoExp"] = sdsProductSize.FilterExpression;
            sdsProductSize.FilterExpression = null;
            Session["BRVProductInfo"] = "Size";
            FilterProducts();
            sdsProductSize.DataBind();
            //grid.DataSourceID = "sdsProductSize";
            grid.DataBind();

            if (val != "")
                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "ProductSize") != null)
                        if (grid.GetRowValues(i, "ProductSize").ToString().Trim() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "ProductSize").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }

            if (val == "")
            {
                lookup.Value = null;
            }
        }
        protected void FilterProducts()
        {
            if (Session["BRVProductInfoExp"] != null)
            {
                string ExpCode = Session["BRVProductInfoExp"].ToString();

                switch (Session["BRVProductInfo"].ToString())
                {
                    case "Code":
                        //if (Session["BRVType"].ToString() == "A")
                        //{
                        //    sdsProductCode.SelectCommand = "SELECT DISTINCT '' as DocNumber, '' AS StepCode, '' ItemCode, ItemCode AS ProductCode, '' AS ProductColor, "
                        //+ " '' AS ProductSize FROM masterfile.Item WHERE isnull(isinactive,0) = 0";
                        //}
                        //else
                        sdsProductCode.SelectCommand = "SELECT DISTINCT DocNumber, '' AS StepCode, '' AS ItemCode, ItemCode AS ProductCode, '' AS ProductColor, "
                        + " '' AS ProductSize FROM Production.JOProductOrder WHERE " + ExpCode;
                        break;
                    case "Color":
                        if (Session["BRVType"].ToString() == "A")
                        {
                            sdsProductColor.SelectCommand = "SELECT DISTINCT null as DocNumber, '' AS StepCode, '' AS ItemCode, '' AS ProductCode,'N/A' AS ProductColor, "
                        + " '' AS ProductSize"
                        + " UNION ALL SELECT DISTINCT DocNumber, '' AS StepCode, '' AS ItemCode, '' AS ProductCode, ColorCode AS ProductColor, "
                        + " '' AS ProductSize FROM Production.JOProductOrder WHERE " + ExpCode;
                        }
                        else
                            sdsProductColor.SelectCommand = "SELECT DISTINCT DocNumber, '' AS StepCode, '' AS ItemCode, '' AS ProductCode, ProductColor, "
                            + " '' AS ProductSize FROM Production.JOBillOfMaterial WHERE " + ExpCode;
                        break;
                    case "Size":
                        if (Session["BRVType"].ToString() == "A")
                        {
                            sdsProductSize.SelectCommand = "SELECT DISTINCT null as DocNumber, '' AS StepCode, '' AS ItemCode, '' AS ProductCode, '' AS ProductColor, "
                        + " 'N/A' AS ProductSize"
                        +" UNION ALL SELECT DISTINCT DocNumber, '' AS StepCode, '' AS ItemCode, '' AS ProductCode, '' AS ProductColor, "
                        + " SizeCode AS ProductSize FROM Production.JOProductOrder WHERE " + ExpCode;
                        }
                        else
                            sdsProductSize.SelectCommand = "SELECT DISTINCT DocNumber, '' AS StepCode, '' AS ItemCode, '' AS ProductCode, '' AS ProductColor, "
                            + " ProductSize FROM Production.JOBillOfMaterial WHERE " + ExpCode;
                        // "SELECT DISTINCT null as DocNumber, '' AS StepCode, '' AS ItemCode, '' AS ProductCode, '' AS ProductColor, "
                        //+ " 'N/A' AS ProductSize"
                        //+" UNION ALL 
                        break;
                    case "Comp":
                        if (Session["BRVType"].ToString() == "A")
                        {
                            sdsComponents.SelectCommand = "SELECT DISTINCT ComponentCode AS Components, Description FROM Masterfile.Component WHERE ISNULL(IsInactive,0) = 0";
                        }
                        else
                            sdsComponents.SelectCommand = "SELECT DISTINCT a.Components,Description FROM Production.JOBillOfMaterial a " +
                            " inner join Masterfile.Component b on a.Components = b.ComponentCode WHERE " + ExpCode;
                        break;
                }
            }
        }
        protected void glNewItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            //{
            //    gridLookup.GridView.DataSourceID = "sdsItem";
            //    sdsItem.SelectCommand = "SELECT DISTINCT ItemCode, FullDesc FROM Masterfile.[Item] A INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsAsset,0) = 0";
            //    //gridLookup.DataBind();
            //}
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glNewItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                sdsItem.SelectCommand = "SELECT DISTINCT ItemCode, FullDesc FROM Masterfile.[Item] A INNER JOIN Masterfile.ItemCategory B ON A.ItemCategoryCode = B.ItemCategoryCode WHERE ISNULL(A.IsInactive,0) = 0 AND ISNULL(B.IsAsset,0) = 0";
                //gridLookup.DataBind();
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glNewItemCode")
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

                DataTable newdata = Gears.RetriveData2("SELECT DISTINCT ItemCode, ColorCode, ClassCode, SizeCode FROM Masterfile.ItemDetail "
                                        + " WHERE ISNULL(IsInactive,0) = 0 AND ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                DataTable unit = Gears.RetriveData2("SELECT DISTINCT ISNULL(UnitBase,'') Unit, ISNULL(EstimatedCost,0) AS UnitCost, ISNULL(B.AllowancePerc,0) AS AllowancePerc FROM Masterfile.Item A LEFT JOIN Masterfile.ProductCategorySub B ON A.ProductSubCatCode = B.ProductSubCatCode WHERE ISNULL(A.IsInactive,0) = 0 AND A.ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode
                                                        + "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode
                                                        + "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode
                                                        + "' AND ISNULL(IsInActive,0) = 0", Session["ConnString"].ToString()); ;

                cntsze = countsize.Rows.Count;

                if (newdata.Rows.Count == 1)
                {
                    foreach (DataRow dt in newdata.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }
                    else if (cntclr > 1 && cntcls > 1 && cntsze == 1)
                    {
                        codes = ";;";
                        codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else if (cntclr > 1 && cntcls == 1 && cntsze > 1)
                    {
                        codes = ";";
                        codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                        codes += ";";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else if (cntclr == 1 && cntcls > 1 && cntsze > 1)
                    {
                        codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                        codes += ";;";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";

                    }
                    else
                    {
                        codes = ";;;";
                        codes += unit.Rows[0]["Unit"].ToString() + ";";
                        codes += unit.Rows[0]["UnitCost"].ToString() + ";";
                        codes += unit.Rows[0]["AllowancePerc"].ToString() + ";";
                    }

                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "itemN";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
        protected void CheckType()
        {
            if (!String.IsNullOrEmpty(aglType.Text))
            {
                Session["BRVTypeValue"] = aglType.Value.ToString().Trim();

                switch (aglType.Value.ToString().Trim())
                {
                    case "A":
                        gv1.Columns["glStepCode"].Width = 100;
                        gv1.Columns["glpItemCode"].Width = 0;
                        gv1.Columns["ColorCode"].Width = 0;
                        gv1.Columns["ClassCode"].Width = 0;
                        gv1.Columns["SizeCode"].Width = 0;
                        gv1.Columns["glpNewItemCode"].Width = 100;
                        gv1.Columns["NewColorCode"].Width = 100;
                        gv1.Columns["NewClassCode"].Width = 100;
                        gv1.Columns["NewSizeCode"].Width = 100;
                        break;
                    case "C":
                        gv1.Columns["glStepCode"].Width = 100;
                        gv1.Columns["glpItemCode"].Width = 100;
                        gv1.Columns["ColorCode"].Width = 100;
                        gv1.Columns["ClassCode"].Width = 100;
                        gv1.Columns["SizeCode"].Width = 100;
                        gv1.Columns["glpNewItemCode"].Width = 0;
                        gv1.Columns["NewColorCode"].Width = 0;
                        gv1.Columns["NewClassCode"].Width = 0;
                        gv1.Columns["NewSizeCode"].Width = 0;
                        break;
                    case "D":
                        gv1.Columns["glStepCode"].Width = 100;
                        gv1.Columns["glpItemCode"].Width = 100;
                        gv1.Columns["ColorCode"].Width = 100;
                        gv1.Columns["ClassCode"].Width = 100;
                        gv1.Columns["SizeCode"].Width = 100;
                        gv1.Columns["glpNewItemCode"].Width = 0;
                        gv1.Columns["NewColorCode"].Width = 0;
                        gv1.Columns["NewClassCode"].Width = 0;
                        gv1.Columns["NewSizeCode"].Width = 0;
                        break;
                    case "E":
                        gv1.Columns["glStepCode"].Width = 100;
                        gv1.Columns["glpItemCode"].Width = 100;
                        gv1.Columns["ColorCode"].Width = 100;
                        gv1.Columns["ClassCode"].Width = 100;
                        gv1.Columns["SizeCode"].Width = 100;
                        gv1.Columns["glpNewItemCode"].Width = 0;
                        gv1.Columns["NewColorCode"].Width = 0;
                        gv1.Columns["NewClassCode"].Width = 0;
                        gv1.Columns["NewSizeCode"].Width = 0;
                        break;
                    case "I":
                        gv1.Columns["glStepCode"].Width = 100;
                        gv1.Columns["glpItemCode"].Width = 100;
                        gv1.Columns["ColorCode"].Width = 100;
                        gv1.Columns["ClassCode"].Width = 100;
                        gv1.Columns["SizeCode"].Width = 100;
                        gv1.Columns["glpNewItemCode"].Width = 0;
                        gv1.Columns["NewColorCode"].Width = 0;
                        gv1.Columns["NewClassCode"].Width = 0;
                        gv1.Columns["NewSizeCode"].Width = 0;
                        break;
                    case "M":
                        gv1.Columns["glStepCode"].Width = 100;
                        gv1.Columns["glpItemCode"].Width = 100;
                        gv1.Columns["ColorCode"].Width = 100;
                        gv1.Columns["ClassCode"].Width = 100;
                        gv1.Columns["SizeCode"].Width = 100;
                        gv1.Columns["glpNewItemCode"].Width = 100;
                        gv1.Columns["NewColorCode"].Width = 100;
                        gv1.Columns["NewClassCode"].Width = 100;
                        gv1.Columns["NewSizeCode"].Width = 100;
                        break;
                    case "U":
                        gv1.Columns["glStepCode"].Width = 100;
                        gv1.Columns["glpItemCode"].Width = 100;
                        gv1.Columns["ColorCode"].Width = 100;
                        gv1.Columns["ClassCode"].Width = 100;
                        gv1.Columns["SizeCode"].Width = 100;
                        gv1.Columns["glpNewItemCode"].Width = 0;
                        gv1.Columns["NewColorCode"].Width = 0;
                        gv1.Columns["NewClassCode"].Width = 0;
                        gv1.Columns["NewSizeCode"].Width = 0;
                        break;
                }
            }
        }
        protected void gv1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Version"] = "2";
            e.NewValues["IsBulk"] = false;
            e.NewValues["IsMajorMaterial"] = false;
            e.NewValues["IsExcluded"] = false;
            e.NewValues["IsRounded"] = false;
        }
        protected void InitControls()
        {
            foreach (var item in frmlayout1.Items)
            {
                if (item is LayoutGroupBase)
                    (item as LayoutGroupBase).ForEach(GetNestedControls);
            }
        }
        protected void GetNestedControls(LayoutItemBase item)
        {
            if (item is LayoutItem)
                SetViewState(item as LayoutItem);

        }
        protected void SetViewState(LayoutItem c)
        {
            foreach (Control control in c.Controls)
            {
                ASPxEdit editor = control as ASPxEdit;

                if (editor != null)
                {
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxTextBox")
                    {
                        TextboxLoad(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup")
                    {
                        LookupLoad(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxDateEdit")
                    {
                        Date_Load(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxMemo")
                    {
                        MemoLoad(editor);
                    }
                }
            }
        }
        protected void aglReferenceJO_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
                if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                {
                    gridLookup.GridView.DataSourceID = "sdsReferenceJO";
                    sdsReferenceJO.SelectCommand = "SELECT DocNumber, DocDate, DueDate, LeadTime, CustomerCode, TotalJOQty, TotalINQty, TotalFinalQty, Remarks FROM Production.JobOrder WHERE Status IN ('N','W') "
                                                   +"and isnull(AllocSubmittedBy,'')!='' ORDER BY DocNumber ASC";
                    gridLookup.DataBind();
                }
        }

        protected void callbacker_Callback(object source, CallbackEventArgs e)
        {
            string size = e.Parameter.Split('|')[0];
            string jonum = e.Parameter.Split('|')[1];
            
            if(size != "N/A" && size != "null"){
                DataTable getsize1 = Gears.RetriveData2("SELECT case when ISNULL(INQty,0)=0 then ISNULL(JOQty,0) else INQty end as Qty" +
                                    " FROM Production.JOSizeBreakdown where StockSize = '" + size + "'" +
                                    " and DocNumber = '" + jonum + "'", Session["ConnString"].ToString());
                if (getsize1.Rows.Count == 0)
                {
                    getsize1 = Gears.RetriveData2("SELECT case when ISNULL(TotalINQty,0)=0 then ISNULL(TotalJOQty,0) else TotalINQty end as Qty" +
                                    " FROM Production.JOBOrder where DocNumber = '" + jonum + "'", Session["ConnString"].ToString());
                }

                foreach (DataRow dtrow in getsize1.Rows)
                {
                    callbacker.JSProperties["cp_qty"] = dtrow[0].ToString();
                }
            }
            else{
                DataTable getsize1 = Gears.RetriveData2("SELECT case when sum(ISNULL(INQty,0))=0 then sum(ISNULL(JOQty,0)) else sum(INQty) end as Qty "+
                                                        "FROM Production.JOSizeBreakdown where DocNumber = '" + jonum + "'", Session["ConnString"].ToString());
                foreach (DataRow dtrow in getsize1.Rows)
                {
                    callbacker.JSProperties["cp_qty"] = dtrow[0].ToString();
                }
            }
            
        }

        protected void glComponents_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(Components_CustomCallback);
                Session["BRVProductInfo"] = "Comp";
                FilterProducts();
                gridLookup.DataBind();
            }
        }

        public void Components_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string step = e.Parameters.Split('|')[0];
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            string item = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
            string color = e.Parameters.Split('|')[3];
            string classs = e.Parameters.Split('|')[4];
            string size = e.Parameters.Split('|')[5];
            string pcode = e.Parameters.Split('|')[6];

            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[16], "glComponents");
            ASPxGridView grid = sender as ASPxGridView;
            CriteriaOperator JONumber = new InOperator("DocNumber", new string[] { Session["BRVJONumber"].ToString() });
            CriteriaOperator StepCodeVal = new InOperator("StepCode", new string[] { step });
            CriteriaOperator ItemCodeVal = new InOperator("ItemCode", new string[] { item });
            CriteriaOperator ColorCodeVal = new InOperator();
            CriteriaOperator ClassCodeVal = new InOperator();
            CriteriaOperator SizeCodeVal = new InOperator();
            CriteriaOperator PCodeVal = new InOperator();
            if (aglType.Value.ToString() != "A")
            {
                ColorCodeVal = new InOperator("ColorCode", new string[] { color });
                ClassCodeVal = new InOperator("ClassCode", new string[] { classs });
                SizeCodeVal = new InOperator("SizeCode", new string[] { size });
                PCodeVal = new InOperator("ProductCode", new string[] { pcode });
            }
            //if (aglType.Value.ToString().Trim() != "A")
            //{
            //    sdsProductSize.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber, StepCodeVal, ItemCodeVal)).ToString();
            //}
            //else
            //{
            if (aglType.Value.ToString() != "A")
                sdsComponents.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber, StepCodeVal, ItemCodeVal, ColorCodeVal, ClassCodeVal, SizeCodeVal, PCodeVal)).ToString();
            else
                sdsComponents.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, JONumber)).ToString();
            //}
            Session["BRVProductInfoExp"] = sdsComponents.FilterExpression;
            sdsComponents.FilterExpression = null;
            Session["BRVProductInfo"] = "Comp";
            FilterProducts();
            sdsComponents.DataBind();
            //grid.DataSourceID = "sdsProductSize";
            grid.DataBind();

            if (val != "")
                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "Components") != null)
                        if (grid.GetRowValues(i, "Components").ToString().Trim() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "Components").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }

            if (val == "")
            {
                lookup.Value = null;
            }
        }
    }
}