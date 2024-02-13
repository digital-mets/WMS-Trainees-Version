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
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.Reflection;
using GearsWarehouseManagement;
using GearsProcurement;
using DevExpress.XtraReports.UI;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using GearsSales;
using GearsAccounting;
using GearsInventory;
using GearsProduction;
using DevExpress.XtraPrintingLinks;
using System.Web.Services;
using Newtonsoft.Json;

namespace GWL
{
    #region Changes (Please keep this Updated all the time!)
    //-- =============================================
    //-- Author:		Erastian Sy
    //-- CREATION  DATE: 05/07/2015
    //-- Description:	Translist Form

    //-- 12/11/2015 Luis Genel T. Edpao  Added Depreciation(Ribbon Group), RDepreciate(Ribbon)
    //-- 2015-12-15 	RAA 	Change Generate Physical Count fix Prompt Message
    //-- 2015-12-28 	ES 		Added Room Cascade Ribbon, WayBill Ribbon 
    //-- 2016-01-05 	ES 		Add new parameter to add new transaction SP
    //-- 2016-01-09 	ES 		Add Unprint Ribbon
    //-- 2016-01-18 	RAA 	Add Prod Trade TransType in RSubmission Ribbon
    //-- 2016-01-19 	GC  	Add Generate PR/JO callback function and Custom Export
    //-- 2016-01-22 	ES 		Add Multiprint Ribbon
    //-- 2016-01-23 	GC  	Added code for cancellation and generatePRJO
    //-- 2016-01-30 	GC  	Changed code for Dispatch and Forward AND added Date_Init
    //-- 2016-02-12 	GC  	Added code for IsPrinted tag
    //-- 2016-02-16 	Tony  	Added format for decimal field in translist grid
    //-- 2016-02-16 	Tony  	Added Button for Copy Loan and Extract F/S Data
    //-- 2016-02-18 	Tony  	Replace parsing of ribbon with a generic loop
    //-- 2016-02-18 	Tony  	Added parsing and replacement of {USERID} with actual user ID
    //-- 2016-02-18 	Tony  	Added ribbon group Service Billing with Item Unbilled and Unpaid
    //-- 2016-02-19 	Tony  	Added ribbon for import tickets (new/resolved/closed)
    //-- 2016-02-23 	Tony  	Added parsing and replacement of {DATERANGE}
    //-- 2016-02-23 	Tony  	Disable if update of Copy_Load table name is empty even if calendar is visible
    //-- 2016-02-27		GC		Added code for complimentary issuance submit 
    //-- 2016-03-03		ES		Added ribbon codes for Costing Submit
    //-- 2016-03-08		TLAV  	Revised code for Authorize ribbon
    //-- 2016-03-08		LGE   	Moved ACTAQT from Accounting to Procurement in RSubmission
    //-- 2016-03-18 	Tony  	Hide ribbon if no ribbon item to avoid missing translist
    //-- 2016-03-18 	Tony  	Added ribbon for KPI Generation and IS Projection
    //-- 2016-03-18		Tony  	Added utility function ExecuteSQL
    //-- 2016-03-30 	GC    	Added codes for Generate PCount(Inv) and tagging of IsFinal
    //-- 2016-03-31 	TLAV  	Added icon for Authorize Ribbon
    //-- 2016-04-11 	TLAV  	Added Ribbon for StartDIS
    //-- 2016-04-21		GC	  	Added code for Approve JO DueDate
    //-- 2016-04-27		Tony  	Added ribbon for Check Release
    //-- 2016-05-04		GC		Added transtype if statement on RNew ribbon and JOCosting Callback
    //-- 2016-05-06     ES      Added combobox for selection of select check box type
    //-- 2016-05-14     RA      Added code for Extraction 
    //-- 2016-05-16     ES      Added access check for printing
    //-- 2016-05-16		GC		Added codes for Preprinted printing for HR
    //-- 2016-05-27		GC		Added code fro Void DR
    //-- 2016-06-02		GC		Added code for ribbon RForSubmitColRep
    //-- 2016-06-01     RA      Added code for ribbon RForAutoSi,RForCost,RAutoSI,RAutoAPVouch,Row Count
    //-- 2016-06-07     TLAV    Added code for ribbon RUnreleaseCheck
    //-- 2016-06-22		GC		Changed code for sp_AddDocNumber
    //-- 2016-07-12		TLAV    Added ribbon for auto AI
    //-- =============================================
    #endregion
    public partial class Translist : System.Web.UI.Page
    {
        string frm = "";
        string moduleid = "";
        string transtype = "";
        string parameters = "";
        string docnumber = "";
        string CustomerCode = "";
        string sp = "";
        string glpost = "";
        string glposting = "";
        string prompt = "";
        public static string comidoc = "";
        Boolean view = false;//Boolean for view state

        bool extract = false;
        List<object> selectedValues;

        public static string keyfield = "";
        public static string cmbprint = "";
        private static bool copbool = false;
        public static string DocNum = "";
        #region Array input
        string[] iswithdetail = new string[] { "IsWithDetail" };//Put iswithdetail into array
        string[] issubmitted = new string[] { "SubmittedBy" };//Put issubmitted into array

        string[] isapproved = new string[] { "ApprovedBy" };//Put issubmitted into array

        string[] iscancelledby = new string[] { "CancelledBy" };//Put issubmitted into array

        string[] docnumarr = new string[] { "DocNumber" }; //new add
        string[] activityarr = new string[] { "Activity" }; //new add
        string[] putawayby = new string[] { "PutAwayBy" };
        string[] isprinted = new string[] { "IsPrinted" };

        string[] status = new string[] { "Status" };
        string[] generate = new string[] { "DocNumber", "WarehouseCode", "PlantCode" };//Put issubmitted into array
        string[] keycode = null;
        string[] itemcat = new string[] { "ItemCategoryCode" };

        string[] recordID = new string[] { "RecordID" }; //2016-06-07     TLAV
        #endregion

        #region DocNumber Settings
        protected DataTable series()
        {
            DataTable a = Gears.RetriveData2("SELECT Prefix,REPLICATE('0', Serieswidth - len(Seriesnumber+1)) + cast(Seriesnumber+1 as varchar) AS 'docnum',Module,SeriesNumber " +
                                                  "from it.docnumbersettings where module = '" + Request.QueryString["moduleid"].ToString() + "'" +
                                                  " and Prefix = '" + cmbDocNumber.Value.ToString() + "'", Session["ConnString"].ToString());
            return a;
        }
        protected DataTable series2(string param)
        {
            DataTable a = Gears.RetriveData2("SELECT Prefix,REPLICATE('0', Serieswidth - len(Seriesnumber+1)) + cast(Seriesnumber+1 as varchar) AS 'docnum',Module,SeriesNumber " +
                                                  "from it.docnumbersettings where module = '" + Request.QueryString["moduleid"].ToString() + "'" +
                                                  " and Prefix = '" + param + "'", Session["ConnString"].ToString());
            return a;
        }
        protected DataTable series3()
        {
            DataTable a = Gears.RetriveData2("SELECT Prefix,REPLICATE('0', Serieswidth - len(Seriesnumber+1)) + cast(Seriesnumber+1 as varchar) AS 'docnum' " +
                                                      "from it.docnumbersettings where module = '" + Request.QueryString["moduleid"].ToString() + "' and IsDefault = 1"
                                                      , Session["ConnString"].ToString());
            return a;
        }
        protected void cp2_Callback(object sender, CallbackEventArgsBase e)//Add Docnumber callback
        {

            string Prefix = "";
            string Module = "";
            string SeriesNumber = "";
            string param = e.Parameter;
            cp2.JSProperties["cp_frm"] = frm;

            switch (e.Parameter)
            {
                case "Add":
                    string tablename = "";

                    DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where moduleid = '" + Request.QueryString["moduleid"].ToString() + "'", Session["ConnString"].ToString());
                    foreach (DataRow dtRow in menutable.Rows)
                    {
                        tablename = dtRow["TableName"].ToString();
                    }


                    if (!String.IsNullOrEmpty(cmbDocNumber.Text))
                    {
                        series();
                        foreach (DataRow dtRow in series().Rows)
                        {
                            docnumber = dtRow["Prefix"].ToString() + dtRow["docnum"].ToString();
                            Prefix = dtRow["Prefix"].ToString();
                            Module = dtRow["Module"].ToString();
                            SeriesNumber = dtRow["SeriesNumber"].ToString();
                        }
                    }

                    if (!cbReadOnly.Checked)
                    {
                        docnumber = txtDocNumber.Text;
                    }
                    DataTable docexist = Gears.RetriveData2("Select docnumber from " + tablename + " where docnumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                    if (docexist.Rows.Count > 0)
                    {
                        cp2.JSProperties["cp_exist"] = true;
                        //GC 06/22/2016 Changed code
                        //Gears.RetriveData2("exec sp_AddDocNumber '" + Module + "','" + Prefix + "','" + SeriesNumber + "'", Session["ConnString"].ToString()); ;
                        Gears.RetriveData2("exec sp_AddDocNumber '" + Module + "','" + Prefix + "','" + txtDocNumber.Text + "'", Session["ConnString"].ToString()); ;
                        //end
                    }
                    else
                    {
                        cp2.JSProperties["cp_exist"] = false;
                        //Session["DocNumber"] = txtDocNumber.Text;
                        cp2.JSProperties["cp_iswithdetail"] = "false";
                        cp2.JSProperties["cp_docnumber"] = txtDocNumber.Text;
                        cp2.JSProperties["cp_transtype"] = transtype;
                        cp2.JSProperties["cp_parameters"] = parameters;
                        Gears.RetriveData2("exec sp_AddNewTransaction '" + txtDocNumber.Text + "','" + Session["userid"] + "','" + tablename + "','" + transtype + "' ", Session["ConnString"].ToString());
                        Gears.RetriveData2("exec sp_UpdateWorkDate '" + txtDocNumber.Text + "','" + Session["userid"] + "','" + tablename + "','" + transtype + "','" + Session["WorkDate"].ToString() + "' ", Session["ConnString"].ToString());
                        Gears.RetriveData2("exec sp_UpdateWarehouse '" + txtDocNumber.Text + "','" + Session["userid"] + "','" + tablename + "','" + transtype + "','" + Session["Warehouse"].ToString() + "' ", Session["ConnString"].ToString());

                        if (cbReadOnly.Checked)
                        {
                            //GC 06/22/2016 Changed code
                            //Gears.RetriveData2("exec sp_AddDocNumber '" + Module + "','" + Prefix + "','" + SeriesNumber + "'", Session["ConnString"].ToString()); ;
                            Gears.RetriveData2("exec sp_AddDocNumber '" + Module + "','" + Prefix + "','" + txtDocNumber.Text + "'", Session["ConnString"].ToString()); ;
                            //end
                        }
                        foreach (DataRow dtRow in series3().Rows)
                        {
                            cmbDocNumber.Value = dtRow["Prefix"].ToString();
                            cbReadOnly.Checked = true;
                        }
                    }

                    break;

                case "Close":
                    foreach (DataRow dtRow in series3().Rows)
                    {
                        docnumber = dtRow["Prefix"].ToString() + dtRow["docnum"].ToString();
                        cmbDocNumber.Value = dtRow["Prefix"].ToString();
                        txtDocNumber.Text = docnumber;
                        cbReadOnly.Checked = true;
                    }
                    break;

                default:
                    foreach (DataRow dtRow in series2(param).Rows)
                    {
                        if (cbReadOnly.Checked)
                        {
                            docnumber = dtRow["Prefix"].ToString() + dtRow["docnum"].ToString();
                            cmbDocNumber.JSProperties["cp_docnum"] = docnumber;
                            txtDocNumber.Text = docnumber;
                        }
                    }
                    break;
            }
        }
        #endregion
        protected void print_Callback(object sender, CallbackEventArgsBase e)//Print function Callback
        {
            switch (e.Parameter)
            {
                case "Print":
                    print_callback.JSProperties["cp_print"] = true;
                    GetSelectedValues();
                    foreach (object[] item in selectedValues)
                    {
                        print_callback.JSProperties["cp_docnumber"] = item[0].ToString();
                    }
                    DataTable printing = Gears.RetriveData2("select top 1 ReportName from masterfile.formlayout where formname = '" + Request.QueryString["transtype"].ToString() + "' and Text= '" + cmbprintsel.Text + "'", Session["ConnString"].ToString());
                    foreach (DataRow dtRow in printing.Rows)
                    {
                        print_callback.JSProperties["cp_report"] = dtRow["ReportName"].ToString();
                    }

                    DataTable dtreprint = Gears.RetriveData2("Select Reprint from masterfile.FormLayout where FormName = '" + Request.QueryString["transtype"].ToString() + "' " +
                                      "and ReportName = '" + print_callback.JSProperties["cp_report"] + "'", Session["ConnString"].ToString());
                    foreach (DataRow dtRow in dtreprint.Rows)
                    {
                        print_callback.JSProperties["cp_reprint"] = dtRow["Reprint"].ToString();
                    }
                    //Genrev Added code 02/12/2016
                    string tag = "";
                    List<object> printValues = Translistgrid.GetSelectedFieldValues(isprinted);
                    int cnt = 0;
                    foreach (bool print in printValues)
                    {
                        cnt++;
                        tag = print.ToString();
                        if (cnt > 1)
                        {
                            break;
                        }
                    }
                    print_callback.JSProperties["cp_isprinted"] = tag;
                    //end
                    print_callback.JSProperties["cp_transtype"] = transtype;
                    break;



            }
        }
        protected void copy_Callback(object sender, CallbackEventArgsBase e)//Copy function Callback
        {
            switch (e.Parameter)
            {
                case "Copy":
                    copy_callback.JSProperties["cp_copy"] = true;

                    DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where moduleid = '" + Request.QueryString["moduleid"].ToString() + "'", Session["ConnString"].ToString());

                    string tablename = "";
                    foreach (DataRow dtRow in menutable.Rows)
                    {
                        tablename = dtRow["TableName"].ToString();
                    }

                    string[] sql = tablename.Split('.');
                    string schema = sql[0];
                    string table = sql[1];

                    Gears.RetriveData2("exec sp_Copy_Transaction '" + copyfrom.Text + "','" + copyto.Text + "','" + Session["userid"].ToString() + "','" + tablename + "','" + table + "','" + schema + "'", Session["ConnString"].ToString());

                    break;

                case "databind":
                    copyfrom.DataBind();
                    copyto.DataBind();
                    break;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            #region Docnumber/Printing
            foreach (DataRow dtRow in series3().Rows)
            {
                if (cbReadOnly.Checked)
                {
                    docnumber = dtRow["Prefix"].ToString() + dtRow["docnum"].ToString();
                }
                else
                {
                    docnumber = txtDocNumber.Text;

                }
                if (!IsPostBack && !IsCallback)
                {
                    cmbDocNumber.Value = dtRow["Prefix"].ToString();
                    txtDocNumber.Text = docnumber;
                }
            }


            #endregion

            #region getConnstring/SP
            string connstring = Session["ConnString"].ToString();
            POmain.ConnectionString = connstring;
            //Gears.UseConnectionString(connstring);

            Translistgrid.SettingsBehavior.AutoFilterRowInputDelay = 300;
            frm = Request.QueryString["frm"].ToString();
            moduleid = Request.QueryString["moduleid"].ToString();
            transtype = Request.QueryString["transtype"].ToString();
            parameters = Request.QueryString["parameters"].ToString();
            sp = Request.QueryString["sp"].ToString();
            prompt = Request.QueryString["prompt"].ToString();
            glpost = Request.QueryString["glpost"].ToString();
            title.Text = prompt;
            #endregion

            if (!IsCallback)
            {
                Session["selectedcol"] = null;
                Session["selectedindex"] = null;
                Session["selectedhide"] = null;
                Session["Unfreeze"] = null;

                #region Ribbon
                string ribbon = Request.QueryString["ribbon"].ToString();
                string[] ribbonitem = ribbon.Split(':');

                // 2016-02-18  Tony
                /*
                #region File Tab
                var filetab = GWLRibbon.Tabs.FindByName("File");
                var gNew = filetab.Groups.FindByName("New");
                #endregion

                #region Action Tab
                var actiontab = GWLRibbon.Tabs.FindByName("Actions");
                var gSubmit = actiontab.Groups.FindByName("Submit");
                var gGenerate = actiontab.Groups.FindByName("Generate");
                var gApproved = actiontab.Groups.FindByName("Approved");
                var gAuthorize = actiontab.Groups.FindByName("Authorize");
                var gDispatch = actiontab.Groups.FindByName("DispatchForward");
                var gAsset = actiontab.Groups.FindByName("Asset");
                var gDepreciation = actiontab.Groups.FindByName("Depreciation");
                #endregion

                #region Print Tab
                var printtab = GWLRibbon.Tabs.FindByName("Print");
                var gPrint = printtab.Groups.FindByName("Print");
                #endregion

                #region Import Tab
                var importtab = GWLRibbon.Tabs.FindByName("Import");
                var gImport = importtab.Groups.FindByName("Import");
                #endregion

                for (int i = 0; i < ribbonitem.Count(); i++)
                {
                    var item1 = gNew.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item1 != null)
                    {
                        item1.Visible = true;
                        gNew.Visible = true;
                        filetab.Visible = true;
                    }

                    var item2 = gSubmit.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item2 != null)
                    {
                        item2.Visible = true;
                        gSubmit.Visible = true;
                        actiontab.Visible = true;
                    }

                    var item3 = gGenerate.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item3 != null)
                    {
                        item3.Visible = true;
                        gGenerate.Visible = true;
                        actiontab.Visible = true;
                    }

                    var item4 = gPrint.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item4 != null)
                    {
                        item4.Visible = true;
                        gPrint.Visible = true;
                        printtab.Visible = true;
                    }
                    
                    var item5 = gApproved.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item5 != null)
                    {
                        item5.Visible = true;
                        gApproved.Visible = true;
                        actiontab.Visible = true;
                    }

                    var item6 = gImport.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item6 != null)
                    {
                        item6.Visible = true;
                        gImport.Visible = true;
                        importtab.Visible = true;
                    }

                    var item7 = gDispatch.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item7 != null)
                    {
                        item7.Visible = true;
                        gDispatch.Visible = true;
                        actiontab.Visible = true;
                    }

                    var item8 = gAsset.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item8 != null)
                    {
                        item8.Visible = true;
                        gAsset.Visible = true;
                        actiontab.Visible = true;
                    }

                    var item9 = gDepreciation.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item9 != null)
                    {
                        item9.Visible = true;
                        gDepreciation.Visible = true;
                        actiontab.Visible = true;
                    }

                    var item10 = gAuthorize.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                    if (item10 != null)
                    {
                        item10.Visible = true;
                        gAuthorize.Visible = true;
                        actiontab.Visible = true;
                    }
                }
				*/
                RibbonButtonItem _RibbonItem;
                GWLRibbon.Visible = false;		// 2016-03-18  Tony
                for (int i = 0; i < ribbonitem.Count(); i++)
                {
                    Boolean _found = false;
                    foreach (RibbonTab _tab in GWLRibbon.Tabs)
                    {
                        foreach (RibbonGroup _group in _tab.Groups)
                        {
                            _RibbonItem = _group.Items.FindByName(ribbonitem[i]) as RibbonButtonItem;
                            if (_RibbonItem != null)
                            {
                                _RibbonItem.Visible = true;
                                _group.Visible = true;
                                _tab.Visible = true;
                                _found = true;
                                if (_RibbonItem.Name == "RCopy") { copbool = true; }
                                break;
                            }
                        }
                        if (_found) { GWLRibbon.Visible = true; break; }		// 2016-03-18  Tony
                    }
                }
                // 2016-02-18  Tony  End
                #endregion
            }

            string datefrom = "";
            string dateto = "";
            if (!IsPostBack)
            {
                selectAllMode.DataSource = Enum.GetValues(typeof(GridViewSelectAllCheckBoxMode));
                selectAllMode.DataBind();
                selectAllMode.SelectedIndex = 1;

                datefrom = Request.QueryString["date1"].ToString();
                dateto = Request.QueryString["date2"].ToString();
                ASPxDateEdit1.Text = datefrom;
                ASPxDateEdit2.Text = dateto;

                DataTable printing = Gears.RetriveData2("select top 1 Text from masterfile.formlayout where formname = '" + Request.QueryString["transtype"].ToString() + "'", Session["ConnString"].ToString());
                foreach (DataRow dtRow in printing.Rows)
                {
                    cmbprintsel.Value = dtRow["Text"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["val"]))
            {
                string sql = Request.QueryString["val"].ToString();
                string[] sqlcom = sql.Split('~');
                if (sqlcom[1] != "")
                {


                    string sqldec = Gears.Decrypt(sqlcom[1].Replace(" ", "+"), "mets");
                    string sqlout = sqldec.Replace("[DATERANGE]", "(docdate between @date1 and @date2)");
                    // 2016-02-18  Tony  Parsing of {USERID} parameter
                    sqlout = sqlout.Replace("{USERID}", "'" + Session["userid"].ToString() + "'");
                    // 2016-02-18  Tony  End
                    // 2016-02-23  Tony  Parsing of {DATERANGE} parameter
                    sqlout = sqlout.Replace("{DATERANGE}", " BETWEEN @date1 and @date2");
                    sqlout = sqlout.Replace("[WAREHOUSE]", " CHARINDEX(WarehouseCode,'" + Session["WHCode"].ToString() + "') >0  ");
                    // 2016-02-23  Tony  ENd
                    POmain.SelectParameters["date1"].DefaultValue = ASPxDateEdit1.Text;
                    POmain.SelectParameters["date2"].DefaultValue = ASPxDateEdit2.Text;
                    GetColumnSql.SelectParameters["date1"].DefaultValue = ASPxDateEdit1.Text;
                    GetColumnSql.SelectParameters["date2"].DefaultValue = ASPxDateEdit2.Text;

                    //if (Request.Params["__CALLBACKPARAM"] != null && IsCallback)
                    //    if (Session["firstcb"] == "0" && (Translistgrid.VisibleRowCount > 0 || Request.Params["__CALLBACKPARAM"].Contains("APPLYMULTI") || (Request.Params["__CALLBACKPARAM"].Contains("RNew") || Request.Params["__CALLBACKPARAM"].Contains("Add") || Request.Params["__CALLBACKPARAM"].Contains("Close"))))
                    //    POmain.SelectCommand = sqlout;

                    //if (!IsPostBack){
                    //    Session["firstcb"] = "1";
                    //    POmain.SelectCommand = sqlout.Replace("ORDER BY", "and 1=0 ORDER BY");
                    //}
                    //else if ((IsCallback && Session["firstcb"] == "1") || (Translistgrid.VisibleRowCount == 0 && Request.Params["__CALLBACKPARAM"].Contains("REFRESH")) || (Request.Params["__CALLBACKPARAM"].Contains("RNew") || Request.Params["__CALLBACKPARAM"].Contains("Add") || Request.Params["__CALLBACKPARAM"].Contains("Add")))
                    //{
                    //    Session["firstcb"] = "0";
                    //    POmain.SelectCommand = sqlout.Replace("ORDER BY", "and 1=0 ORDER BY"); ;
                    //}    

                    bool ordercon = sqlout.Contains("order by");

                    if (!IsPostBack)
                        POmain.SelectCommand = ReplaceLastOccurrence(sqlout, ordercon ? "order by" : "ORDER BY", "and 1=0 ORDER BY");
                    else
                        POmain.SelectCommand = sqlout;
                    // 2016-02-23  Tony 
                    // if (!sqldec.Contains("[DATERANGE]"))
                    if (!(sqldec.Contains("[DATERANGE]") || sqldec.Contains("{DATERANGE}")))
                    // 2016-02-23  Tony End
                    {
                        Calendar.Visible = false;
                    }
                    else
                    {
                        caltext.Text = "DateRange: " + datefrom + " - " + dateto;
                    }



                    SqlDataSource PO = GetColumnSql;

                    string tablename = "";

                    DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where moduleid = '" + Request.QueryString["moduleid"].ToString() + "'", Session["ConnString"].ToString());
                    foreach (DataRow dtRow in menutable.Rows)
                    {
                        tablename = dtRow["TableName"].ToString();
                    }
                    GetColumnSql.SelectCommand = POmain.SelectCommand;
                    DataView dw = (DataView)PO.Select(DataSourceSelectArguments.Empty);

                    string[] columnNames = dw.Table.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray();

                    Translistgrid.KeyFieldName = columnNames[0].ToString();
                    keyfield = Translistgrid.KeyFieldName;
                    keycode = new string[] { keyfield };

                }
            }
            else
            {
                return;
            }

            if (Session["userid"] == null)
            {
                Response.Redirect(@".\login\Login.aspx");
            }
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
        protected void Copy_Load(object sender, EventArgs e)
        {
            if (Calendar.Visible == true && copbool == true)
            {
                string tablename = "";

                DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where moduleid = '" + Request.QueryString["moduleid"].ToString() + "'", Session["ConnString"].ToString());
                foreach (DataRow dtRow in menutable.Rows)
                {
                    tablename = dtRow["TableName"].ToString();
                }

                // 2016-02-23  Tony  Disable if table name is empty
                if (tablename != "")
                {
                    Copysel.SelectParameters["date1"].DefaultValue = ASPxDateEdit1.Text;
                    Copysel.SelectParameters["date2"].DefaultValue = ASPxDateEdit2.Text;
                    Copysel.SelectCommand = "Select DocNumber from " + tablename + " where docdate between @date1 and @date2 and ISNULL(submittedby,'') = ''";
                }
            }
        }//Copy function load method

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
        protected void btnXlsxExport1_Click(object sender, EventArgs e)
        {

            ExtractedData.DataSource = Session["extract"];

            gridExtract.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
            Session["extract"] = null;
        }

        //added 1/2/2024
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (!IsPostBack)
            {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = view;
            }
        }

        protected void ASPxGridView1_DataBound(object sender, EventArgs e)//Creation of checkbox column on runtime
        {
            ASPxGridView grid = sender as ASPxGridView;
            GridViewCommandColumn col = new GridViewCommandColumn();
            if (grid.Columns.IndexOf(grid.Columns["CommandColumn"]) != -1)//Select all combobox - Era
            {
                grid.Columns.RemoveAt(grid.Columns.IndexOf(grid.Columns["CommandColumn"]));
            }
            col.Name = "CommandColumn";
            col.ShowSelectCheckbox = true;
            col.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;
            col.VisibleIndex = 0;
            col.Width = 30;
            col.FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns.Add(col);
            col.SelectAllCheckboxMode = (GridViewSelectAllCheckBoxMode)Enum.Parse(typeof(GridViewSelectAllCheckBoxMode), Session["checkall"] != null ? Session["checkall"].ToString() : selectAllMode.Text);

            //2016-02-16  Tony
            SqlDataSource PO = GetColumnSql;
            DataView dw = (DataView)PO.Select(DataSourceSelectArguments.Empty);


            // 2016 - 07 - 26 LGE

            foreach (GridViewDataColumn _col in grid.DataColumns)
            {
                if (dw.Table.Columns[_col.FieldName] == null) continue;
                if (dw.Table.Columns[_col.FieldName].DataType == typeof(Decimal))
                {
                    (_col as GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "#,0.00";
                }
                _col.Settings.AutoFilterCondition = AutoFilterCondition.Contains; //Auto filter condition to contains
            }

            // END OF 2016 - 07 - 26 LGE

            //foreach (GridViewDataColumn _col in grid.DataColumns)
            //{
            //    if (dw.Table.Columns[_col.FieldName].DataType == typeof(Decimal))
            //    {
            //        (_col as GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "#,0.00";
            //    }
            //    _col.Settings.AutoFilterCondition = AutoFilterCondition.Contains; //Auto filter condition to contains



            //}

            //2016-02-16  Tony  End
        }

        #region Contextmenu
        protected void ASPxGridView1_FillContextMenuItems(object sender, ASPxGridViewContextMenuEventArgs e)//context menu creation
        {

            if (e.MenuType == GridViewContextMenuType.Rows)//Create context menu items
            {
                var item = e.CreateItem("View", "View");
                var item2 = e.CreateItem("New", "New");
                var item3 = e.CreateItem("Edit", "Edit");
                var item4 = e.CreateItem("Delete", "Delete");
                var item5 = e.CreateItem("Freeze Columns", "Freeze Columns");
                var item6 = e.CreateItem("Hide Columns", "Hide Columns");
                //var item4 = e.CreateItem("Validate", "Validate");
                item.Image.IconID = "actions_show_16x16";
                item2.Image.IconID = "actions_addfile_16x16";
                item3.Image.IconID = "edit_edit_16x16";
                item4.Image.IconID = "edit_delete_16x16";
                item5.Image.IconID = "other_fixed_column_width_16x16gray";
                item6.Image.IconID = "actions_hide_16x16";
                item2.BeginGroup = true;
                item5.BeginGroup = true;
                e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.DeleteRow), item);
                e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.DeleteRow), item2);
                e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.DeleteRow), item3);
                e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.DeleteRow), item4);
                e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.DeleteRow), item5);
                e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.DeleteRow), item6);
                item5.Items.Add("Unfreeze Columns", "Unfreeze Columns");

                List<string> fieldNames = new List<string>();
                foreach (GridViewColumn column in Translistgrid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        GridViewDataColumn dataColumn = (GridViewDataColumn)column;
                        AddMenuSubItem(item5, dataColumn.FieldName, dataColumn.FieldName, "", true);
                        AddMenuSubItem(item6, dataColumn.FieldName, dataColumn.FieldName, "actions_apply_16x16office2013", true);
                    }
                }

                if (Session["selectedcol"] != null)
                {
                    e.Items.FindByName(Session["selectedcol"].ToString()).Image.IconID = "actions_apply_16x16office2013";
                }
                if (Session["selectedhide"] != null)
                {
                    string[] selectedhide = Session["selectedhide"].ToString().Split(';');

                    foreach (string selected in selectedhide)
                    {
                        if (selected != "")
                            e.Items.FindByName("Hide Columns").Items.FindByName(selected).Image.IconID = "";
                    }
                }
                if (Session["Unfreeze"] != null)
                {
                    e.Items.FindByName(Session["selectedcol"].ToString()).Image.IconID = "";
                    Session["Unfreeze"] = null;
                    Session["selectedcol"] = null;
                }
            }

        }
        private static void AddMenuSubItem(GridViewContextMenuItem parentItem, string text, string name, string imageUrl, bool isPostBack)//Add subitem in context menu function
        {
            var cols = parentItem.Items.Add(text, name);
            cols.Image.IconID = imageUrl;
        }

        protected void ASPxGridView1_ContextMenuItemClick(object sender, ASPxGridViewContextMenuItemClickEventArgs e)
        {
            Translistgrid.JSProperties["cp_frm"] = frm;
            Translistgrid.JSProperties["cp_transtype"] = transtype;
            Translistgrid.JSProperties["cp_parameters"] = parameters;
            if (e.Item.Name == "Delete")
            {
                foreach (string fieldName in keycode)
                {
                    Translistgrid.JSProperties["cp_docnumber"] = Translistgrid.GetRowValues(e.ElementIndex, fieldName);
                }
                foreach (string fieldName in iswithdetail)
                {
                    Translistgrid.JSProperties["cp_iswithdetail"] = Translistgrid.GetRowValues(e.ElementIndex, fieldName);
                }

                if (Calendar.Visible == false)
                {
                    Translistgrid.JSProperties["cp_redirect"] = "delete";
                }
                else
                {
                    Session["delete"] = "true";
                    foreach (string fieldName in issubmitted)
                    {
                        if (Translistgrid.GetRowValues(e.ElementIndex, fieldName) != null)
                        {
                            if (Convert.ToString(Translistgrid.GetRowValues(e.ElementIndex, fieldName)) != "")
                            {
                                Session["delete"] = "false";
                                Translistgrid.JSProperties["cp_redirect"] = "error";
                                Translistgrid.JSProperties["cp_errormes"] = "You cannot delete this document since it is already submitted/posted.";
                            }
                            else
                            {
                                if (Session["delete"] == "true")
                                    Translistgrid.JSProperties["cp_redirect"] = "delete";
                            }
                        }
                    }

                    foreach (string fieldName in isapproved)
                    {
                        if (Translistgrid.GetRowValues(e.ElementIndex, fieldName) != null)
                        {
                            if (Convert.ToString(Translistgrid.GetRowValues(e.ElementIndex, fieldName)) != "" && transtype != "WMSPICK" && transtype != "WMSPUT")
                            {
                                Session["delete"] = "false";
                                Translistgrid.JSProperties["cp_redirect"] = "error";
                                Translistgrid.JSProperties["cp_errormes"] = "You cannot delete this document since it is already approved.";
                            }
                            else
                            {
                                if (Session["delete"] == "true")
                                    Translistgrid.JSProperties["cp_redirect"] = "delete";
                            }
                        }
                    }



                    foreach (string fieldName in iscancelledby)
                    {
                        if (Translistgrid.GetRowValues(e.ElementIndex, fieldName) != null)
                        {
                            if (Convert.ToString(Translistgrid.GetRowValues(e.ElementIndex, fieldName)) != "")
                            {
                                Session["delete"] = "false";
                                Translistgrid.JSProperties["cp_redirect"] = "error";
                                Translistgrid.JSProperties["cp_errormes"] = "You cannot delete this document since it is already cancelled.";
                            }
                            else
                            {
                                if (Session["delete"] == "true")
                                    Translistgrid.JSProperties["cp_redirect"] = "delete";
                            }
                        }
                    }


                }

            }
            else if (e.Item.Name == "View")
            {
                foreach (string fieldName in keycode)
                {
                    Translistgrid.JSProperties["cp_docnumber"] = Translistgrid.GetRowValues(e.ElementIndex, fieldName);
                }
                foreach (string fieldName in iswithdetail)
                {
                    Translistgrid.JSProperties["cp_iswithdetail"] = Translistgrid.GetRowValues(e.ElementIndex, fieldName);
                }
                Translistgrid.JSProperties["cp_redirect"] = "view";

            }
            else if (e.Item.Name == "Edit")
            {
                foreach (string fieldName in keycode)
                {
                    Translistgrid.JSProperties["cp_docnumber"] = Translistgrid.GetRowValues(e.ElementIndex, fieldName);
                }
                foreach (string fieldName in iswithdetail)
                {
                    Translistgrid.JSProperties["cp_iswithdetail"] = Translistgrid.GetRowValues(e.ElementIndex, fieldName);
                }

                if (Calendar.Visible == false && transtype != "REFPDOH" && transtype != "PRDPIS")
                {
                    Translistgrid.JSProperties["cp_redirect"] = "edit";
                }
                else
                {
                    Session["edit"] = "true";
                    foreach (string fieldName in issubmitted)
                    {
                        if (Translistgrid.GetRowValues(e.ElementIndex, fieldName) != null)
                        {
                            if (Convert.ToString(Translistgrid.GetRowValues(e.ElementIndex, fieldName)) != "")
                            {
                                Session["edit"] = "false";
                                Translistgrid.JSProperties["cp_redirect"] = "error";
                                Translistgrid.JSProperties["cp_errormes"] = "You cannot edit this document anymore. Check if this is already submitted/posted.";
                            }
                            else
                            {
                                if (Session["edit"] == "true")
                                    Translistgrid.JSProperties["cp_redirect"] = "edit";
                            }
                        }
                    }
                    foreach (string fieldName in isapproved)
                    {
                        if (Translistgrid.GetRowValues(e.ElementIndex, fieldName) != null)
                        {
                            if (Convert.ToString(Translistgrid.GetRowValues(e.ElementIndex, fieldName)) != "" && transtype != "WMSPICK" && transtype != "WMSPUT")
                            {
                                Session["edit"] = "false";
                                Translistgrid.JSProperties["cp_redirect"] = "error";
                                Translistgrid.JSProperties["cp_errormes"] = "You cannot edit this document anymore. Check if this is already approved.";
                            }
                            else
                            {
                                if (Session["edit"] == "true")
                                    Translistgrid.JSProperties["cp_redirect"] = "edit";
                            }
                        }
                    }

                    foreach (string fieldName in iscancelledby)
                    {
                        if (Translistgrid.GetRowValues(e.ElementIndex, fieldName) != null)
                        {
                            if (Convert.ToString(Translistgrid.GetRowValues(e.ElementIndex, fieldName)) != "")
                            {
                                Session["edit"] = "false";
                                Translistgrid.JSProperties["cp_redirect"] = "error";
                                Translistgrid.JSProperties["cp_errormes"] = "You cannot edit this document anymore. Check if this is already cancelled.";
                            }
                            else
                            {
                                if (Session["edit"] == "true")
                                    Translistgrid.JSProperties["cp_redirect"] = "edit";
                            }
                        }
                    }

                }
            }
            else if (e.Item.Name == "New")
            {
                if (Calendar.Visible == false)
                {
                    Translistgrid.JSProperties["cp_masterfile"] = true;
                }
                else
                {
                    Translistgrid.JSProperties["cp_docnum"] = true;
                }
            }

            else if (e.Item.Name != "New" && e.Item.Name != "Edit" && e.Item.Name != "Delete" && e.Item.Name != "View" &&
                e.Item.Name != "Refresh" && e.Item.Name != "Freeze Columns" && e.Item.Name != "Unfreeze Columns"
                && e.Item.Parent.Name == "Freeze Columns")
            {//Freeze Columns
                Session["selectedcol"] = e.Item.Name;
                Session["selectedindex"] = e.Item.Index;

                int i = 0;
                List<string> fieldNames = new List<string>();
                foreach (GridViewColumn column in Translistgrid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        GridViewDataColumn dataColumn = (GridViewDataColumn)column;
                        dataColumn = GetColumnByFieldName(dataColumn.FieldName);
                        dataColumn.FixedStyle = GridViewColumnFixedStyle.Left;
                    }
                    i++;
                    if (i >= e.Item.Index)
                        break;
                }
            }
            else if (e.Item.Name != "New" && e.Item.Name != "Edit" && e.Item.Name != "Delete" && e.Item.Name != "View" &&
                e.Item.Name != "Refresh" && e.Item.Name != "Freeze Columns" && e.Item.Name != "Unfreeze Columns"
                && e.Item.Parent.Name == "Hide Columns")
            {//Hide Columns
                int i = 0;
                List<string> fieldNames = new List<string>();
                if (Session["selectedindex"] != null)
                {
                    foreach (GridViewColumn column in Translistgrid.Columns)
                    {
                        if (column is GridViewDataColumn)
                        {
                            GridViewDataColumn dataColumn = (GridViewDataColumn)column;
                            dataColumn = GetColumnByFieldName(dataColumn.FieldName);
                            dataColumn.FixedStyle = GridViewColumnFixedStyle.Left;
                        }
                        i++;
                        if (i >= Convert.ToInt32(Session["selectedindex"].ToString()))
                            break;
                    }
                }

                string selectedhide = "";
                if (Session["selectedhide"] != null)
                {
                    selectedhide = Session["selectedhide"].ToString();
                }
                Session["selectedhide"] = e.Item.Name + ";" + Session["selectedhide"];

                if (selectedhide.Contains(e.Item.Name))
                {
                    Session["selectedhide"] = selectedhide.Replace(e.Item.Name + ";", "");
                    GridViewDataColumn dataColumn = GetColumnByFieldName(e.Item.Name);
                    dataColumn.Visible = true;
                }
                else
                {
                    GridViewDataColumn dataColumn = GetColumnByFieldName(e.Item.Name);
                    dataColumn.Visible = false;
                }
            }
            else if (e.Item.Name == "Unfreeze Columns")
            {
                Session["Unfreeze"] = "true";
                int i = 0;
                List<string> fieldNames = new List<string>();
                foreach (GridViewColumn column in Translistgrid.Columns)
                {
                    if (column is GridViewDataColumn)
                    {
                        GridViewDataColumn dataColumn = (GridViewDataColumn)column;
                        dataColumn = GetColumnByFieldName(dataColumn.FieldName);
                        dataColumn.FixedStyle = GridViewColumnFixedStyle.None;
                    }
                    i++;
                    if (i >= Convert.ToInt32(Session["selectedindex"].ToString()))
                        break;
                }
                Session["selectedindex"] = null;
            }
        }//context menu item click function
        #endregion
        private GridViewDataColumn GetColumnByFieldName(string fieldName)//Get all column name function
        {
            IEnumerable<GridViewDataColumn> dataColumns = Translistgrid.Columns.OfType<GridViewDataColumn>().Where(c => c.FieldName == fieldName);
            if (dataColumns.Count() > 0)
                return dataColumns.First();
            return null;
        }
        private void GetSelectedValues()//Getting selected values in grid function
        {
            List<string> fieldNames = new List<string>();
            foreach (GridViewColumn column in Translistgrid.Columns)
                if (column is GridViewDataColumn)
                    fieldNames.Add(((GridViewDataColumn)column).FieldName);
            selectedValues = Translistgrid.GetSelectedFieldValues(fieldNames.ToArray());
        }


        #region importdata
        string FilePath
        {
            get { return Session["FilePath"] == null ? String.Empty : Session["FilePath"].ToString(); }
            set { Session["FilePath"] = value; }
        }

        public string Documenentationstaff { get; private set; }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FilePath = String.Empty;
        }
        protected void Upload_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            FilePath = Page.MapPath("~/App_Data/") + e.UploadedFile.FileName;
            e.UploadedFile.SaveAs(FilePath);
        }
        private DataTable GetTableFromExcel()//Method for reading the imported file
        {
            Workbook book = new Workbook();
            book.LoadDocument(FilePath);
            Worksheet sheet = book.Worksheets.ActiveWorksheet;
            Range range = sheet.GetUsedRange();
            DataTable table = sheet.CreateDataTable(range, true);
            for (int i = 2; i < table.Columns.Count; i++)
            {
                table.Columns[i].DataType = typeof(string);
            }
            DataTableExporter exporter = sheet.CreateDataTableExporter(range, table, true);
            exporter.Export();
            File.Delete(FilePath);
            return table;
        }

        void book_InvalidFormatException(object sender, SpreadsheetInvalidFormatExceptionEventArgs e)
        {
            Exception exception = new Exception();
            throw new Exception(e.Exception.Message, exception);
        }

        protected void import_cp_Callback(object sender, CallbackEventArgsBase e)
        {
            List<object> fieldValues = Translistgrid.GetSelectedFieldValues(keycode);

            foreach (string docnum in fieldValues)
            {
                GWarehouseManagement._connectionString = Session["ConnString"].ToString();
                string execute = GWarehouseManagement.ImportExcel(docnum, transtype, GetTableFromExcel());

                if (execute == "")
                {
                    import_cp.JSProperties["cp_execmes"] = "Successfully imported";
                }
                else
                {
                    import_cp.JSProperties["cp_execmes"] = execute;
                }

            }

            if (Session["import"].ToString() == "1")
            {
                GWarehouseManagement._connectionString = Session["ConnString"].ToString();
                string execute = GWarehouseManagement.ImportExcel(transtype, transtype, GetTableFromExcel());

                if (execute == "")
                {
                    import_cp.JSProperties["cp_execmes"] = "Successfully imported";
                }
                else
                {
                    import_cp.JSProperties["cp_execmes"] = execute;
                }
            }
        }
        #endregion


        public String ExecuteSQL(String strSQLCmd)
        {
            String strErrMssg = "";
            using (SqlConnection sqlconn = new SqlConnection(Session["ConnString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(strSQLCmd))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        sqlconn.Open();
                        cmd.Connection = sqlconn;
                        strErrMssg = cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {
                        strErrMssg = ex.ToString();
                    }
                    sqlconn.Close();
                }
            }

            return strErrMssg;
        }
        // 2016-03-09  Tony  End
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

        }


        protected void gridcp_Callback(object sender, CallbackEventArgsBase e)//Select all callback - Era
        {
            if (Session["extractdata"] == null)
            {
                Session["checkall"] = e.Parameter;
                //Translistgrid.Selection.UnselectAll();
                Translistgrid.DataBind();
            }
            else
            {
                DataTable source = Gears.RetriveData2("select SQLExtract,ColumnDate from it.mainmenu where TransType='" + transtype + "' ", Session["ConnString"].ToString());


                string SPsql = " " + source.Rows[0][0].ToString().Replace("[DATERANGE]", " " + source.Rows[0][1].ToString() + " BETWEEN '" + ASPxDateEdit1.Text + "' AND '" + ASPxDateEdit2.Text + "' ");
                SPsql = SPsql.Replace("[DATEFROM]", ASPxDateEdit1.Text);
                SPsql = SPsql.Replace("[DATETO]", ASPxDateEdit2.Text);




                DataTable dtsout = Gears.RetriveData2(SPsql, Session["ConnString"].ToString());

                Session["extract"] = dtsout;
                ExtractedData.DataSource = dtsout;



                ExtractedData.DataBind();
                gridcp.JSProperties["cp_extract2"] = true;
                Session["extractdata"] = null;

            }


        }

        protected void POmain_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (IsCallback)
                e.Cancel = Translistgrid.FilterExpression == string.Empty;
        }


        protected void Chargesgrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GetSelectedValues();
            string Customer = "";
            string trans = Request.QueryString["transtype"].ToString();

            foreach (object[] item in selectedValues)
            {

                Customer = item[1].ToString();
                DocNum = item[0].ToString();
                Chargesgrid.JSProperties["cp_chargesCusCode"] = Customer;

                CustomerCode = Customer;

            }

            if (trans == "WMSINB"){
                trans = "INBOUND";
            }
            else {
                trans = "OUTBOUND";

            }
            Chargessql.SelectParameters["CusCode"].DefaultValue = Customer;
            Chargesgrid.DataSourceID = "Chargessql";
            Chargessql.SelectParameters["Docn"].DefaultValue = DocNum;
            Chargesgrid.DataSourceID = "Chargessql";
            Chargessql.SelectParameters["Trans"].DefaultValue = trans;
            Chargesgrid.DataSourceID = "Chargessql";
            Chargesgrid.DataBind();

        }


        [WebMethod]
        public static string ChargesSubmit(List<ChargeData> Charges)
        {
            string resultMessage = "";
            try
            {
                foreach (ChargeData charge in Charges)
                {

                    DataTable UpdateCherges = Gears.RetriveData2("UPDAte WMS.Charges set ChargeFlag = '" + charge.ChargeFlag + "',ChargesQty='" + charge.ChargesQty + "' where RecordID='" + charge.RecordID + "'", HttpContext.Current.Session["ConnString"].ToString());

                }
            }
            catch (Exception ex)
            {
                resultMessage = "Error: " + ex.Message;
            }

            return resultMessage.Trim();
        }

        public class ChargeData
        {
            public string RecordID { get; set; }
            public string ChargeFlag { get; set; }
            public string ChargesQty { get; set; }
        }

        public class ReturnData
        {
            public string DocNumber { get; set; }
            public string LineNumber { get; set; }
            public string ItemReturn { get; set; }
        }

        [WebMethod]
        public static string ReturnSubmit(List<ReturnData> Returns)
        {
            string resultMessage = "";
            var DocNumber = "";
            try
            {
                DataTable GetDocNumber = Gears.RetriveData2("SELECT (Prefix + REPLICATE('0', Serieswidth - len(SeriesNumber+1)) + cast(SeriesNumber+1 as varchar)) AS DocNumber FROM IT.DocNumberSettings WHERE Module = 'WMSINB' AND Prefix = 'INBR'", HttpContext.Current.Session["ConnString"].ToString());

                var DocNum = Convert.ToString(GetDocNumber.Rows[0]["DocNumber"]);

                foreach (ReturnData returns in Returns)
                {
                    DocNumber = returns.DocNumber;

                    DataTable isExist = Gears.RetriveData2("SELECT * FROM WMS.Inbound WHERE DocNumber = '" + DocNum + "'", HttpContext.Current.Session["ConnString"].ToString());

                    if (isExist.Rows.Count == 0)
                    {
                        DataTable InsertInbound = Gears.RetriveData2("INSERT INTO [WMS-TEST].[WMS].Inbound(DocNumber,ICNNumber,DocDate,PlateNo,Driver,CustomerCode,WarehouseCode,Supplier,TruckNo,IsWithDetail,AddedBy,AddedDate,Field3,Field4,Field5,Field6,Field7,Field8,Field9) " +
                                             "SELECT '" + DocNum + "', '" + DocNum + "', GETDATE(), A.PlateNumber, A.Driver, A.Customer, A.WarehouseCode, B.SuppliedBy, A.TruckingCo, 1, '1828', A.AddedDate, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, 'CP_RECEIVED', CONVERT(varchar, GETDATE(), 25) FROM WMS.Outbound A " +
                                             "LEFT JOIN WMS.Picklist B ON A.DocNumber = B.DocNumber WHERE A.DocNumber = '" + DocNumber + "'", HttpContext.Current.Session["ConnString"].ToString());


                        DataTable UpdateSeries = Gears.RetriveData2("UPDATE IT.DocNumberSettings SET SeriesNumber = SeriesNumber + 1 WHERE Module = 'WMSINB' AND Prefix = 'INBR'", HttpContext.Current.Session["ConnString"].ToString());
                    }
                    int ItemReturn = Convert.ToInt32(returns.ItemReturn);

                    if (ItemReturn == 1)
                    {
                        DataTable UpdateCherges = Gears.RetriveData2("UPDATE WMS.OutboundDetail set ItemReturn = '" + returns.ItemReturn + "' where DocNumber = '" + returns.DocNumber + "' AND LineNumber = '" + returns.LineNumber + "'", HttpContext.Current.Session["ConnString"].ToString());

                        int linenum = 0;

                        DataTable count = Gears.RetriveData("SELECT COUNT(DocNumber) AS LineNumber FROM WMS.InboundDetail WHERE DocNumber = '" + DocNum + "'");

                        try
                        {
                            linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                        }
                        catch
                        {
                            linenum = 1;
                        }
                        string strLine = linenum.ToString().PadLeft(5, '0');

                        DataTable InsertInboundDetails = Gears.RetriveData2("INSERT INTO [WMS-TEST].WMS.InboundDetail(DocNumber, LineNumber,ItemCode,BatchNumber,LotID,ManufacturingDate,ExpiryDate,BulkQty,BulkUnit,BaseQty,ReceivedQty,ColorCode,ClassCode,SizeCode,Remarks,Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9) " +
                                                                            "SELECT '" + DocNum + "', '" + strLine + "', A.ItemCode, B.BatchNo, B.Location, B.Mkfgdate, B.ExpiryDate, Qty, B.BulkUnit, A.BaseQty, null, 'N/A', 'N/A', 'N/A', A.Remarks, A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9 FROM WMS.OutboundDetail A " +
                                                                            "LEFT JOIN WMS.PicklistDetail B ON A.DocNumber = B.DocNumber AND A.LineNumber = B.LineNumber WHERE A.DocNumber = '" + DocNumber + "' AND A.LineNumber = '" + returns.LineNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                resultMessage = "Error: " + ex.Message;
            }

            return resultMessage.Trim();
        }

        //protected void ASPxGridViewCharge_DataBound(object sender, EventArgs e)
        //{
        //    ASPxGridView grid = sender as ASPxGridView;

        //    if (grid.Columns.IndexOf(grid.Columns["CommandColumn1"]) != -1)
        //    {
        //        grid.Columns.RemoveAt(grid.Columns.IndexOf(grid.Columns["CommandColumn1"]));
        //    }

        //    GridViewDataTextColumn chargeFlagColumn = grid.Columns["ChargeFlag"] as GridViewDataTextColumn;

        //    if (chargeFlagColumn != null)
        //    {
        //        grid.Columns.Remove(chargeFlagColumn);

        //        GridViewDataCheckColumn col = new GridViewDataCheckColumn();
        //        col.Name = "ChargeFlag";
        //        col.Caption = "Charged Flag";
        //        col.FieldName = "ChargeFlag";
        //        col.VisibleIndex = 1;
        //        col.Width = 85;
        //        col.PropertiesCheckEdit.ValueChecked = true;
        //        col.PropertiesCheckEdit.ValueUnchecked = false;

        //        grid.Columns.Add(col);
        //    }

        //    SqlDataSource PO = GetColumnSql;
        //    DataView dw = (DataView)PO.Select(DataSourceSelectArguments.Empty);

        //    foreach (GridViewDataColumn _col in grid.DataColumns)
        //    {
        //        if (dw.Table.Columns[_col.FieldName] == null) continue;
        //        if (dw.Table.Columns[_col.FieldName].DataType == typeof(Decimal))
        //        {
        //            (_col as GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "#,0.00";
        //        }
        //        _col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
        //    }
        //}

        protected void MultiAssigngrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GetSelectedValues();

            if (e.Parameters == "Update")
            {

            }
            else if (e.Parameters == "Shown")
            {
                string User = HttpContext.Current.Session["userid"].ToString();
                string dateF = ASPxDateEdit1.Text;
                string dateT = ASPxDateEdit2.Text;
                string doc = "";
                foreach (object[] item in selectedValues)
                {
                    doc = item[0].ToString();
                }
                MultiAssignsql.SelectParameters["DocNumber"].DefaultValue = doc;
                MultiAssignsql.SelectParameters["User"].DefaultValue = User;
                MultiAssignsql.SelectParameters["dateF"].DefaultValue = dateF;
                MultiAssignsql.SelectParameters["dateT"].DefaultValue = dateT;
                MultiAssigngrid.DataSourceID = "MultiAssignsql";
                MultiAssigngrid.DataBind();
            }
        }

        protected void Assigngrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GetSelectedValues();



            if (e.Parameters == "Update")
            {

            }
            else if (e.Parameters == "Shown")
            {
                string User = HttpContext.Current.Session["userid"].ToString();
                string dateF = ASPxDateEdit1.Text;
                string dateT = ASPxDateEdit2.Text;
                string doc = "";
                foreach (object[] item in selectedValues)
                {
                    doc = item[0].ToString();
                }
                Assignsql.SelectParameters["DocNumber"].DefaultValue = doc;
                Assignsql.SelectParameters["User"].DefaultValue = User;
                Assignsql.SelectParameters["dateF"].DefaultValue = dateF;
                Assignsql.SelectParameters["dateT"].DefaultValue = dateT;
                Assigngrid.DataSourceID = "Assignsql";
                Assigngrid.DataBind();

            }
        }

        protected void Return_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            GetSelectedValues();
            string doc = "";
            string cust = "";
            foreach (object[] item in selectedValues)
            {
                cust = item[1].ToString();
                doc = item[0].ToString();
                CustomerCode = cust;

            }

            if (e.Parameters == "Update")
            {

            }
            else if (e.Parameters == "Shown")
            {
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc] FROM Masterfile.[Item] where isnull(IsInactive,'')=0 AND Customer IN ('" + CustomerCode + "') ";

                Returnsql.SelectParameters["DocNumber"].DefaultValue = doc;
                Returngrid.DataSourceID = "Returnsql";
                Returngrid.DataBind();

            }
        }

        protected void Rejectgrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GetSelectedValues();
            string doc = "";
            foreach (object[] item in selectedValues)
            {
                doc = item[0].ToString();
            }

            if (e.Parameters == "Shown")
            {
                Rejectsql.SelectParameters["DocNumber"].DefaultValue = doc;
                Rejectgrid.DataSourceID = "Rejectsql";
                Rejectgrid.DataBind();
            }
        }

        #region replenish
        public class Replenish
        {
            public string DocNumber { get; set; }
            public string ReferenceRecordID { get; set; }
            public string LineNumber { get; set; }
            public string WarehouseCode { get; set; }
            public string CustomerCode { get; set; }
            public string Location { get; set; }
            public string MinimumWeight { get; set; }
            public string CurrentWeight { get; set; }
            public string RemainingWeight { get; set; }
            public virtual bool Refresh { get; set; }
        }
         [WebMethod]
        public static string UpdateReplenish(List<Replenish> _replenish)
        {
            List<Replenish> replenishment = _replenish;
             string a = "";
             bool refO;
            
            

            //Request transtype = new Request();
            //transtype = Request.QueryString["transtype"].ToString();


            try
            {

                //replenishment detail data update or insert
                foreach (var data in replenishment)
                {
                    DataTable Replenish = Gears.RetriveData2("select * from wms.Replenishment where ReferenceRecordID = '" + data.ReferenceRecordID + "'", HttpContext.Current.Session["ConnString"].ToString());

                    if (Replenish.Rows.Count > 0)
                    {
                        foreach (DataRow dt in Replenish.Rows)
                        {
                     
                            refO = Convert.ToBoolean(dt["Refresh"]);
                            DataTable UpdateProperty = Gears.RetriveData2("Update wms.replenishment Set Refresh = '" + Convert.ToBoolean(data.Refresh) + "',LastEditedBy='"+ HttpContext.Current.Session["userid"].ToString()+"',LastEditedDate=GETDATE() where DocNumber = '" + dt["DocNumber"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
                            //insert logs when there's changes in each data
                            if (refO != data.Refresh)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + dt["DocNumber"].ToString() + "',' ','Refresh','" + refO + "','" + Convert.ToBoolean(data.Refresh) + "',' ',' ','WMSAUX(REP)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                           


                        }


                    }
                    else
                    {
                        if(Convert.ToBoolean(data.Refresh) == true )
                        {
                           
                            DataTable GetDocNumber = Gears.RetriveData2("SELECT (Prefix + REPLICATE('0', Serieswidth - len(SeriesNumber+1)) + cast(SeriesNumber+1 as varchar)) AS DocNumber FROM IT.DocNumberSettings WHERE Module = 'WMSREPL' AND Prefix = 'REP'", HttpContext.Current.Session["ConnString"].ToString());

                            var DocNum = Convert.ToString(GetDocNumber.Rows[0]["DocNumber"]);

                         
                            DataTable isExist = Gears.RetriveData2("SELECT * FROM WMS.Replenishment WHERE DocNumber = '" + DocNum + "'", HttpContext.Current.Session["ConnString"].ToString());

                            if (isExist.Rows.Count == 0)
                            {
                                //insert data if there's no value yet
                                DataTable InsertData = Gears.RetriveData2("insert into wms.replenishment (DocNumber,DocDate,ReferenceRecordID,WarehouseCode,Location,CustomerCode,CurrentWeight,MinWeight,RemainingWeight,Refresh,AddedBy,AddedDate) SELECT '" + DocNum + "',GETDATE(),'" + data.ReferenceRecordID + "','" + data.WarehouseCode + "','" + data.Location + "','" + data.CustomerCode + "','" + data.CurrentWeight + "','" + data.MinimumWeight + "','" + data.RemainingWeight + "','" + Convert.ToBoolean(data.Refresh) + "','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());

                                DataTable UpdateSeries = Gears.RetriveData2("UPDATE IT.DocNumberSettings SET SeriesNumber = SeriesNumber + 1 WHERE Module = 'WMSREPL' AND Prefix = 'REP'", HttpContext.Current.Session["ConnString"].ToString());
                            }

                         }
                    }
                }


            }
            catch (Exception err)
            {
                a = err.Message;
            }

            return a;

        }

        protected void Replenish_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            GetSelectedValues();
            string doc = "";
            
            //foreach (object[] item in selectedValues)
            //{
            //    doc = item[0].ToString();
            //}
          
            if (e.Parameters == "Update")
            {

            }
            else if (e.Parameters == "Search")
            {
                Replenishsql.SelectCommand = "select ISNULL(c.DocNumber,'') as DocNumber,ROW_NUMBER() OVER(order by a.Minweight) as LineNumber,ISNULL(c.Refresh,0) as Refresh,a.ReferenceRecordId as ReferenceRecordID,ISNULL(a.MinWeight,0) as MinimumWeight,ISNULL(b.CurrentWeight,0) as CurrentWeight,"
                                                + "CASE WHEN ISNULL(a.MinWeight,0) > ISNULL(b.CurrentWeight,0) THEN ISNULL(a.MinWeight,0) - ISNULL(b.CurrentWeight,0) ELSE ISNULL(b.CurrentWeight,0) - ISNULL(a.MinWeight,0) END AS RemainingWeight,"
                                                + "ISNULL(a.MaxBaseQty,0) as MaxWeight,b.Location,a.WarehouseCode,a.CustomerCode from masterfile.Location a"
                                                + " left join (select sum(RemainingBaseQty) as CurrentWeight,Location from wms.countsheetsetup group by Location) b on a.LocationCode = b.Location"
                                                + " left join (select Refresh,ReferenceRecordID,DocNumber from wms.replenishment) c on CONVERT(varchar, c.ReferenceRecordID) = a.ReferenceRecordID"
                                                + " where a.CustomerCode = '" + glStorerKey.Text + "' and a.WarehouseCode = '" + glWarehousecode.Text + "' and ISNULL(b.Location,'') != '' group by"
                                                + " b.Location,a.Minweight,b.CurrentWeight,a.WarehouseCode,a.CustomerCode,a.ReferenceRecordId,a.MaxBaseQty,a.replenish,c.DocNumber,c.Refresh";

                replenishGrid.DataSourceID = "Replenishsql";
                replenishGrid.DataBind();
            }
            else if (e.Parameters == "Shown")
            {
                    replenishGrid.DataSourceID = "Replenishsql";
                    replenishGrid.DataBind();
                

            }
        }

        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {

            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        #endregion replenish
        #region comi
        protected void Comi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)

        {

            GetSelectedValues();
            string doc = "";
            string cust = "";
            foreach (object[] item in selectedValues)
            {
                cust = item[1].ToString();
                doc = item[0].ToString();
                CustomerCode = cust;

            }

            if (e.Parameters == "Update")
            {

            }
            else if (e.Parameters == "Shown")
            {   
               
                Comisql.SelectParameters["DocNumber"].DefaultValue = doc;
                Comigrid.DataSourceID = "Comisql";
                Comigrid.DataBind();

            }
        }
        public class COMI
        {
            public string DocNumber { get; set; }
            public string LineNumber { get; set; }
            public string ItemCode { get; set; }
            public string Brand { get; set; }
            public string Origin { get; set; }
            public string MfgName { get; set; }
            public string EstablishNo { get; set; }
            public string VQMLICNo { get; set; }

            public string Batch { get; set; }
            public string Consignee { get; set; }
            public string Docnum { get; set; }

        }
        [WebMethod]
        public static string UpdateComi(List<COMI> _update, List<COMI> _updatehead)
        {
            List<COMI> changes = _update;
            List<COMI> header = _updatehead;
             string a = "";
             string BrandO = "";
             string OriginO = "";
             string MfgNameO = "";
             string EstablishNoO = "";
             string VQMLICNoO = "";
             string BatchO = "";
             string ConsigneeO = "";

            

            //Request transtype = new Request();
            //transtype = Request.QueryString["transtype"].ToString();


            try
            {
              
                  //comi data update or insert
                foreach (var data in header)
                {
                    data.Docnum = comidoc;
                    DataTable comiM = Gears.RetriveData2("select * from wms.comi where DocNumber = '" + data.Docnum + "'", HttpContext.Current.Session["ConnString"].ToString());
                    DataTable inbound = Gears.RetriveData2("select CustomerCode,WarehouseChecker,DocumentationStaff from wms.Inbound where DocNumber = '" + data.Docnum + "'", HttpContext.Current.Session["ConnString"].ToString());
              
           
                if (comiM.Rows.Count > 0)
                { 
                    foreach (DataRow dt in comiM.Rows)
                        {
                            BatchO = dt["Batch"].ToString();
                            ConsigneeO = dt["Consignee"].ToString();
                            DataTable UpdateProperty = Gears.RetriveData2("Update wms.comi Set Consignee = '" + data.Consignee + "',Batch = '" + data.Batch + "',LastEditedBy = '" + HttpContext.Current.Session["userid"].ToString() + "',LastEditedDate=GETDATE() where DocNumber = '" + data.Docnum + "'", HttpContext.Current.Session["ConnString"].ToString());
                            //insert logs when there's changes in each data
                             if (BatchO != data.Batch)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.Docnum + "','','Batch','" + BatchO + "','" + data.Batch + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(COMI)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                             if (ConsigneeO != data.Consignee)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.Docnum + "','','Consignee','" + ConsigneeO + "','" + data.Consignee + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(COMI)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                          
                        
                        }
                }
                else {
                    int linenum = 0;
                    try
                    {
                        //get the docnumber value from inbound to set the batch value

                        DataTable comiCount = Gears.RetriveData2("select top 1* from wms.[comi] order by AddedDate desc", HttpContext.Current.Session["ConnString"].ToString());
                        
                        if (comiCount.Rows.Count > 0)
                        {
                            linenum = Convert.ToInt32(comiCount.Rows[0]["Batch"].ToString().Split('-')[1]) + 1;

                        }
                        else
                        {
                            linenum = Convert.ToInt32(comiCount.Rows.Count.ToString()) + 1;
                        }


                    }
                    catch
                    {
                        linenum = 1;
                    }
                    string strLine = linenum.ToString().PadLeft(5, '0');


                    string genBatch = inbound.Rows[0]["CustomerCode"].ToString() + DateTime.Now.ToString("yyyy") + '-' + strLine;
                   

                    DataTable insertComi = Gears.RetriveData2("insert into wms.comi (DocNumber,Batch,Consignee,AddedBy,AddedDate) SELECT '" + data.Docnum + "','" + genBatch + "','" + data.Consignee + "','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                   
                
                }
                }
                //comi detail data update or insert
                foreach (var data in changes)
                {


                    DataTable comiDetail = Gears.RetriveData2("select * from wms.comidetail where DocNumber = '" + data.DocNumber + "' and LineNumber ='" + data.LineNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                    DataTable inbound = Gears.RetriveData2("select CustomerCode,WarehouseChecker,DocumentationStaff from wms.Inbound where DocNumber = '" + data.DocNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                    if (comiDetail.Rows.Count > 0)
                    {
                        foreach (DataRow dt in comiDetail.Rows)
                        {

                            //add a docnumber in Comisql and call it inbcomisql

                            BrandO = dt["Brand"].ToString();
                            OriginO = dt["Origin"].ToString();
                            MfgNameO = dt["MfgName"].ToString();
                            EstablishNoO = dt["EstablishNo"].ToString();
                            VQMLICNoO = dt["VQMLICNo"].ToString();
                            DataTable UpdateProperty = Gears.RetriveData2("Update wms.comidetail Set ItemCode = '"+data.ItemCode+"',Brand = '"+data.Brand+"',Origin = '"+data.Origin+"',MfgName = '"+data.MfgName+"',EstablishNo = '"+data.EstablishNo+"',VQMLICNo = '"+data.VQMLICNo+"' where DocNumber = '" + data.DocNumber + "' and LineNumber = '" + data.LineNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                            //insert logs when there's changes in each data
                            if (BrandO != data.Brand)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','Brand','" + BrandO + "','" + data.Brand + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(COMI)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                            if (OriginO != data.Origin)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','Origin','" + OriginO + "','" + data.Origin + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(COMI)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                            if (MfgNameO != data.MfgName)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','MfgName','" + MfgNameO + "','" + data.MfgName + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(COMI)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                            if (EstablishNoO != data.EstablishNo)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','EstablishNo','" + EstablishNoO + "','" + data.EstablishNo + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(COMI)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                            if (VQMLICNoO != data.VQMLICNo)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','VQMLICNo','" + VQMLICNoO + "','" + data.VQMLICNo + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(COMI)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }


                        }


                    }
                    else
                    {   
                            //update the comi there will be detail added inside the comidetail the same docnumber
                            if (comiDetail.Rows.Count == 0) {
                                DataTable UpdateComi = Gears.RetriveData2("Update wms.comi Set IsWithDetail = '1' where DocNumber = '"+data.DocNumber+"'", HttpContext.Current.Session["ConnString"].ToString());
                   
                            }
                            //insert data if there's no value yet
                            DataTable InsertLogs = Gears.RetriveData2("insert into wms.comidetail (DocNumber,LineNumber,ItemCode,Brand,Origin,MfgName,EstablishNo,VQMLICNo) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','" + data.ItemCode + "','" + data.Brand + "','" + data.Origin + "','" + data.MfgName + "','" + data.EstablishNo + "','" + data.VQMLICNo + "'", HttpContext.Current.Session["ConnString"].ToString());
                   
                    }
                }


            }
            catch (Exception err)
            {
                a = err.Message;
            }

            return a;

        }

        protected void comi_Callback(object sender, CallbackEventArgsBase e)
        {
            GetSelectedValues();
            string doc = "";
            
            foreach (object[] item in selectedValues)
            {
                doc = item[0].ToString();
            }

            if (e.Parameter == "Update")
            {

            }
            else if (e.Parameter == "Shown")
            {
                DataTable comidetails = Gears.RetriveData2("select DocNumber,Batch,Consignee,Origin from wms.comi where DocNumber='" + doc + "'", HttpContext.Current.Session["ConnString"].ToString());
                if (comidetails.Rows.Count > 0)
                {
                    foreach (DataRow dt in comidetails.Rows)
                    {

                        txtBatch.Value = dt["Batch"].ToString();
                        txtConsignee.Value = dt["Consignee"].ToString();
                      
                    }
                }
                else {
                    txtBatch.Value = " ";
                    txtConsignee.Value = " ";
                 
                }
                comidoc = doc;
                Comisql.SelectParameters["DocNumber"].DefaultValue = doc;
                Comigrid.DataSourceID = "Comisql";
                Comigrid.DataBind();


            }
            return;

        }
        #endregion comi
        protected void AfterBlast_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            GetSelectedValues();
            string doc = "";
            string cust = "";
            foreach (object[] item in selectedValues)
            {
                cust = item[1].ToString();
                doc = item[0].ToString();
                CustomerCode = cust;

            }

            if (e.Parameters == "Update")
            {

            }
            else if (e.Parameters == "Shown")
            {
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc] FROM Masterfile.[Item] where isnull(IsInactive,'')=0 AND Customer IN ('" + CustomerCode + "') ";
            
                AfterBlastsql.SelectParameters["DocNumber"].DefaultValue = doc;
                AfterBlastgrid.DataSourceID = "AfterBlastsql";
                AfterBlastgrid.DataBind();

            }
        }

        public class AfterBlast
        {
            public string DocNumber { get; set; }
            public string LineNumber { get; set; }
            public string Itemcode { get; set; }
            public string PalletID { get; set; }
            public string ReceivedQty { get; set; }
        }
        [WebMethod]
        public static string SubmitAfterBlast(List<AfterBlast> _changes)
        {
            List<AfterBlast> changes = _changes;
            string a = "";
            string DocNumber = "";
            string strError2 = "";

            if (changes != null && changes.Count > 0)
            {
                DocNumber = changes[0].DocNumber;
            }

            try
            {

             
                DataTable ChargeSubmission = Gears.RetriveData2("exec sp_submit_AfterBlast '" + DocNumber + "','" + HttpContext.Current.Session["userid"].ToString() + "','1','WMSINBBlast','BLASTFREEZE'", HttpContext.Current.Session["ConnString"].ToString());

                if (ChargeSubmission.Rows.Count > 1)
                {
                    foreach (DataRow dt1 in ChargeSubmission.Rows)
                    {
                        strError2 += "\r\n" + dt1["Msg"].ToString();
                    }
                }
                else
                {
                    strError2 = "";
                }

            }
            catch (Exception err)
            {
                strError2 = err.Message;
            }

            return strError2;

        }

        [WebMethod]
        public static string UpdateAfterBlast(List<AfterBlast> _changes)
        {
            List<AfterBlast> changes = _changes;
            string a = "";
            string PalletO = "";
            string ReceivedO = "";
            string ItemcodeO = "";


            //Request transtype = new Request();
            //transtype = Request.QueryString["transtype"].ToString();


            try
            {

                foreach (var data in changes)
                {

                    decimal received = Convert.ToDecimal(data.ReceivedQty);
                    DataTable inboundDetail = Gears.RetriveData2("select [DocNumber],[LineNumber],[ItemCode],[ReceivedQty],[PalletID] from wms.InboundDetail where DocNumber = '" + data.DocNumber + "' and LineNumber ='" + data.LineNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                    DataTable inbound = Gears.RetriveData2("select WarehouseChecker,DocumentationStaff from wms.Inbound where DocNumber = '" + data.DocNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                    if (inboundDetail.Rows.Count > 0)
                    {
                        foreach (DataRow dt in inboundDetail.Rows)
                        {



                            PalletO = dt["PalletID"].ToString();
                            ReceivedO = dt["ReceivedQty"].ToString();
                            ItemcodeO = dt["ItemCode"].ToString();
                            DataTable UpdateProperty = Gears.RetriveData2("Update [WMS].[InboundDetail] Set ReceivedQty ='" + data.ReceivedQty + "',ItemCode = '" + data.Itemcode + "',PalletID = '" + data.PalletID + "' where DocNumber = '" + data.DocNumber + "' and LineNumber = '" + data.LineNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                            //insert logs when there's a new value 
                            if (PalletO != data.PalletID)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','PalletID','" + PalletO + "','" + data.PalletID + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "', 'WMSINB(AB)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                            if (ReceivedO != received.ToString("0.00"))
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','ReceivedQty','" + ReceivedO + "','" + data.ReceivedQty + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(AB)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }
                            if (ItemcodeO != data.Itemcode)
                            {
                                DataTable InsertLogs = Gears.RetriveData2("insert into it.TrailLogs (DocNumber,LineNumber,FieldName,OldRecord,NewRecord,WarehouseChecker,DocumentationStaff,Module,UpdatedBy,UpdatedDate) SELECT '" + data.DocNumber + "','" + data.LineNumber + "','ItemCode','" + ItemcodeO + "','" + data.Itemcode + "','" + inbound.Rows[0]["WarehouseChecker"].ToString() + "','" + inbound.Rows[0]["DocumentationStaff"].ToString() + "','WMSINB(AB)','" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE()", HttpContext.Current.Session["ConnString"].ToString());
                            }

                        }



                    }
                }


            }
            catch (Exception err)
            {
                a = err.Message;
            }

            return a;

        }
        public class Assign
        {
            public string DocNumber { get; set; }
            public string WarehouseC { get; set; }
            public string CheckerChanged { get; set; }

        }

        [WebMethod]
        public static string UpdateAssign(List<Assign> _infos)
        {
            string strError2 = "";
            string docS = "";
            string docSP = "";

            try
            {
                List<Assign> infos = _infos;

                foreach (var obj in infos)
                {
                    DataTable CheckChecker = Gears.RetriveData2("Select WarehouseChecker,AddedBy from [WMS].[Inbound] Where DocNumber = '" + obj.DocNumber + "' ", HttpContext.Current.Session["ConnString"].ToString());

                    if (CheckChecker.Rows.Count > 0)
                    {
                        DataTable CountAccepted = Gears.RetriveData2("SELECT LTRIM(RTRIM(SplitValues.AcceptBy)) AS AcceptBy FROM WMS.Inbound CROSS APPLY (SELECT Split.a.value('.', 'VARCHAR(100)') AS AcceptBy FROM (SELECT CAST('<X>' + REPLACE(WMS.Inbound.AcceptBy, ',', '</X><X>') + '</X>' AS XML) AS AcceptBy ) AS A CROSS APPLY A.AcceptBy.nodes('/X') AS Split(a)) AS SplitValues WHERE DocNumber = '" + obj.DocNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                        DataTable CheckAccepted = Gears.RetriveData2("DECLARE @WarehouseC NVARCHAR(MAX) = '" + obj.WarehouseC + "'; WITH WarehouseCheckerCTE AS (SELECT LTRIM(RTRIM(Split.a.value('.', 'VARCHAR(100)'))) AS WarehouseChecker FROM (SELECT CAST('<X>' + REPLACE(@WarehouseC, ',', '</X><X>') + '</X>' AS XML) AS Value) AS A CROSS APPLY A.Value.nodes('/X') AS Split(a)) SELECT LTRIM(RTRIM(SplitValues.AcceptBy)) AS AcceptBy FROM WMS.Inbound CROSS APPLY (SELECT Split.a.value('.', 'VARCHAR(100)') AS AcceptBy FROM ( SELECT CAST('<X>' + REPLACE(WMS.Inbound.AcceptBy, ',', '</X><X>') + '</X>' AS XML) AS AcceptBy ) AS A CROSS APPLY A.AcceptBy.nodes('/X') AS Split(a)) AS SplitValues WHERE DocNumber = '" + obj.DocNumber + "' AND EXISTS ( SELECT 1 FROM WarehouseCheckerCTE WHERE LTRIM(RTRIM(SplitValues.AcceptBy)) = WarehouseChecker);", HttpContext.Current.Session["ConnString"].ToString());

                        if (CountAccepted.Rows.Count == CheckAccepted.Rows.Count)
                        {
                            foreach (DataRow dtrow in CheckChecker.Rows)
                            {
                                docS = dtrow["AddedBy"].ToString();
                            }
                            DataTable UpdateProperty = Gears.RetriveData2("Update [WMS].[Inbound] Set WarehouseChecker ='" + obj.WarehouseC + "',CheckerAssignedDate = GETDATE(),RejectBy=null,RejectDate=null, DocumentationStaff= (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')  where DocNumber = '" + obj.DocNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                            DataTable insertlogs = Gears.RetriveData2("insert into IT.Transactionlogs (DocNumber,WarehouseChecker,WarehouseCheckerAssignDate,DocumentationStaff,DocumentationStaffAssignDate,Action,ActionDate,Notification,Addedby,AddedDate,TransType ) SELECT '" + obj.DocNumber + "', '" + obj.WarehouseC + "', GETDATE(), '" + docS + "',  GETDATE(),'Assigned', GETDATE(),'1', '" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE(),'WMSINB'", HttpContext.Current.Session["ConnString"].ToString());
                        }
                        else
                        {
                            strError2 = "Accepted Cannot be Reassigned";
                        }
                    }

                    DataTable PickChecker = Gears.RetriveData2("Select WarehouseChecker,AddedBy from [WMS].[Picklist] Where DocNumber = '" + obj.DocNumber + "' ", HttpContext.Current.Session["ConnString"].ToString());

                    if (PickChecker.Rows.Count > 0)
                    {
                        DataTable CountAccepted = Gears.RetriveData2("SELECT LTRIM(RTRIM(SplitValues.AcceptBy)) AS AcceptBy FROM WMS.Picklist CROSS APPLY (SELECT Split.a.value('.', 'VARCHAR(100)') AS AcceptBy FROM (SELECT CAST('<X>' + REPLACE(WMS.Picklist.AcceptBy, ',', '</X><X>') + '</X>' AS XML) AS AcceptBy ) AS A CROSS APPLY A.AcceptBy.nodes('/X') AS Split(a)) AS SplitValues WHERE DocNumber = '" + obj.DocNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                        DataTable CheckAccepted = Gears.RetriveData2("DECLARE @WarehouseC NVARCHAR(MAX) = '" + obj.WarehouseC + "'; WITH WarehouseCheckerCTE AS (SELECT LTRIM(RTRIM(Split.a.value('.', 'VARCHAR(100)'))) AS WarehouseChecker FROM (SELECT CAST('<X>' + REPLACE(@WarehouseC, ',', '</X><X>') + '</X>' AS XML) AS Value) AS A CROSS APPLY A.Value.nodes('/X') AS Split(a)) SELECT LTRIM(RTRIM(SplitValues.AcceptBy)) AS AcceptBy FROM WMS.Picklist CROSS APPLY (SELECT Split.a.value('.', 'VARCHAR(100)') AS AcceptBy FROM ( SELECT CAST('<X>' + REPLACE(WMS.Picklist.AcceptBy, ',', '</X><X>') + '</X>' AS XML) AS AcceptBy ) AS A CROSS APPLY A.AcceptBy.nodes('/X') AS Split(a)) AS SplitValues WHERE DocNumber = '" + obj.DocNumber + "' AND EXISTS ( SELECT 1 FROM WarehouseCheckerCTE WHERE LTRIM(RTRIM(SplitValues.AcceptBy)) = WarehouseChecker);", HttpContext.Current.Session["ConnString"].ToString());

                        if (CountAccepted.Rows.Count == CheckAccepted.Rows.Count)
                        {
                            foreach (DataRow dtrow in PickChecker.Rows)
                            {
                                docSP = dtrow["AddedBy"].ToString();
                            }
                            DataTable UpdateProperty = Gears.RetriveData2("Update [WMS].[Picklist] Set WarehouseChecker ='" + obj.WarehouseC + "',CheckerAssignedDate = GETDATE(),RejectBy=null,RejectDate=null where DocNumber = '" + obj.DocNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
                            DataTable insertlogs = Gears.RetriveData2("insert into IT.Transactionlogs (DocNumber,WarehouseChecker,WarehouseCheckerAssignDate,DocumentationStaff,DocumentationStaffAssignDate,Action,ActionDate,Notification,Addedby,AddedDate,TransType ) SELECT '" + obj.DocNumber + "', '" + obj.WarehouseC + "', GETDATE(), '" + docSP + "',  GETDATE(),'Assigned', GETDATE(),'1', '" + HttpContext.Current.Session["userid"].ToString() + "',GETDATE(),'WMSPICK'", HttpContext.Current.Session["ConnString"].ToString());
                        }
                        else
                        {
                            strError2 = "Accepted Cannot be Reassigned";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                strError2 = ex.Message;
            }

            HttpContext.Current.Session["Tokenized"] = false; // 04-29-2021 TA Set Token Session to false
            return strError2;
        }

        protected void gvStepProcess_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            Chargessql.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            Chargessql.UpdateParameters["CFlag"].DefaultValue = e.NewValues["ChargeFlag"].ToString();
            Chargessql.UpdateParameters["CQty"].DefaultValue = e.NewValues["ChargesQty"].ToString();

            //Chargesgrid.JSProperties["cp_redirect"] = "success";
            //Chargesgrid.JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void glItemCode_Init(object sender, EventArgs e)
        {
            //10/5/2016 emc add filter by customer
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;

            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ItemCodeLookup_CustomCallback);
            if (Session["itemsql"] != null)
            {
                Masterfileitem.SelectCommand = Session["itemsql"].ToString();
                Masterfileitem.DataBind();
            }

            //gridLookup.GridView.DataSourceID = Masterfileitem.ID; 
            //gridLookup.GridView.DataSource = Masterfileitem;


        }
        
        public void ItemCodeLookup_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GetSelectedValues();
            string doc = "";
            string cust = "";
            foreach (object[] item in selectedValues)
            {
                cust = item[1].ToString();
                doc = item[0].ToString();
                CustomerCode = cust;

            }
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;//Traps the callback
            ASPxGridView ItemcodeList = sender as ASPxGridView;


            if (e.Parameters.Contains("ItemCodeDropDown"))
            {

                DataTable getCustomer = Gears.RetriveData2("SELECT ISNULL(Field1,'') as  Field1 FROM Masterfile.BizPartner WHERE ISNULL(IsInactive,0)=0 AND BizPartnerCode = '" + CustomerCode + "' AND ISNULL(Field1,'')!='' ", Session["ConnString"].ToString()); //ADD CONN

                //if (getCustomer.Rows.Count > 0)
                //{
                //    CustomerCode = getCustomer.Rows[0][0].ToString();
                //}


                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc],Customer FROM Masterfile.[Item] where isnull(IsInactive,'')=0 AND Customer IN ('" + CustomerCode + "') ";
                Session["itemsql"] = Masterfileitem.SelectCommand;
                //ItemcodeList.DataSource = Masterfileitem;
                ItemcodeList.DataBind();
            }
        }


    }
}