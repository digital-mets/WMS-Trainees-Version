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
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
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
    //-- 2016-07-27		GC		Added code to insert User Trail data after using VoidDR ribbon
    //-- 2016-08-10		GC		Changed code for Void DR
    //-- 2016-09-05     GC      Added code for Generate Depreciation Details
    //-- 2016-09-16     GC      Added the access codes for unsubmission
    //-- 2016-10-27		GC		Changed and added codes to check transtype
    //-- 2016-12-07		RA		Added code Open Ribbon for IA,IR,IT
    //-- =============================================
    #endregion
    public partial class Translist : System.Web.UI.Page
    {
        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)//Callback panel function for Ribbons
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            int count = 0;
            GearsLibrary.Gears.GearsParameter gp = new GearsLibrary.Gears.GearsParameter();
            List<object> fieldValues1 = Translistgrid.GetSelectedFieldValues(generate);//Generate PickListdetail Renats
            List<object> fieldValues = Translistgrid.GetSelectedFieldValues(keycode);
            List<object> itemcategory;
            gp._Connection = Session["ConnString"].ToString();
            string value = "";
            string strError = "";
            string strError2 = "";
            cp.JSProperties["cp_counttotal"] = Convert.ToString(fieldValues.Count);//Martzon added this
            switch (e.Parameter)
            {

                #region ribbon New
                case "RNew":
                    //Genrev 05/04/2016	Added transtype if statement
                    if (transtype != "WMSTRN")
                    {
                        if (Calendar.Visible == false)
                        {
                            cp.JSProperties["cp_masterfile"] = true;
                        }
                        else
                        {
                            cp.JSProperties["cp_docnum"] = true;
                        }
                    }
                    else
                    {
                        cp.JSProperties["cp_masterfile"] = true;
                    }
                    cp.JSProperties["cp_frm"] = frm;
                    cp.JSProperties["cp_transtype"] = transtype;
                    cp.JSProperties["cp_parameters"] = parameters;
                    break;
                #endregion

                #region ribbon Submit
                case "RSubmit":
                    cp.JSProperties["cp_onsubmit"] = true;
                    break;

                case "RSubmission":
                    DataTable access = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_POST'", Session["ConnString"].ToString());
                    foreach (DataRow dt in access.Rows)
                    {
                        value = dt["value"].ToString();
                    }


                    if (Request.QueryString["access"].Contains(value))
                    {
                        string tablename = "";
                        string prompt = "";
                        DataTable menutable = Gears.RetriveData2("Select TableName, ISNULL(Prompt,'') AS Prompt from it.mainmenu where transtype = '" + Request.QueryString["transtype"].ToString() + "'", Session["ConnString"].ToString());
                        foreach (DataRow dtRow in menutable.Rows)
                        {
                            tablename = dtRow["TableName"].ToString();
                            prompt = dtRow["Prompt"].ToString().ToUpper();
                        }
                        //GetSelectedValues();            
                        if (fieldValues == null) return;

                        foreach (var item in fieldValues)
                        {
                            gp._DocNo = item.ToString();
                            gp._TransType = Request.QueryString["transtype"].ToString();
                            gp._UserId = Session["userid"].ToString();
                            gp._Factor = 1;
                            gp._Table = tablename;

                            //Type TypeObj = typeof(GWarehouseManagement);
                            //Object MyObj = Activator.CreateInstance(TypeObj);
                            //strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));

                            Type TypeObj = null;
                            Object MyObj = null;

                            switch (gp._TransType)
                            {
                                case "WMSCON":
                                case "WMSBIL":
                                case "WMSBOS":
                                case "WMSTRN":
                                case "WMSNON":
                                case "WMSICN":
                                case "WMSINB":
                                case "WMSPUT":
                                case "WMSIA":
                                case "WMSREL":
                                case "WMSRSV":
                                case "WMSOCN":
                                case "WMSPICK":
                                case "WMSOUT":
                                case "WMSPHC":
                                case "WMSSTOR":
                                case "WMSSP":
                                case "WMSIT":
                                case "WMSPTC":

                                    TypeObj = typeof(GWarehouseManagement);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "PRCPOM":
                                case "PRCQUM":
                                case "PRCPRM":
                                case "PRCRCR":
                                case "PRCPCC":
                                case "PRPRT":
                                case "PRPOR":
                                case "PRCSCO":
                                case "PRCSOR":
                                case "PRCWIN":
                                case "PRCWOT":
                                case "ACTAQT": //LGE 03-08-2016
                                    TypeObj = typeof(GProcurement);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "SLSQUM":
                                case "SLSORD":
                                case "SLSDRC":
                                case "SLSINV":
                                case "SLSSRN":
                                case "SLSSOR":
                                case "SLSSOD":
                                case "SLSSFC":
                                    TypeObj = typeof(GSales);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "ACTRVL":
                                case "ACTPRT":
                                case "ACTJOV":
                                case "ACTRJS":
                                case "ACTBAL":
                                case "ACTJVT":
                                //case "ACTAQT": LGE 03-08-2016
                                case "ACTADS":
                                case "ACTADI":
                                case "ACTCOA":
                                case "ACTAMZ":
                                case "ACTBAS":
                                case "ACTALE":
                                case "ACTGLG":
                                case "ACTDES":
                                case "ACTCPV":
                                case "ACTPCI":
                                case "ACTBRC":
                                case "ACTCCI":
                                case "ACTCOC":
                                case "ACTCAD": //added
                                case "ACTLCA": //added
                                case "ACTCRB": //added
                                case "ACTCRP": //added
                                case "ACTAPV":
                                case "ACTAPM":
                                case "ACTEXP":
                                case "ACTCHV":
                                case "ACTSIN":
                                case "ACTSEB":
                                case "ACTCOS":
                                case "ACTARM":
                                case "ACTCOL":
                                    TypeObj = typeof(GAccounting);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "INVFRL":
                                case "INVFOR":
                                case "INVREV":
                                case "INVSTR":
                                case "INVADJT":
                                case "INVCNT":
                                case "INVMAN":
                                case "INVOSI":
                                case "INVASI":
                                case "INVJOI":
                                case "INVSAI":
                                case "INVCOI":
                                case "INVOSR":
                                case "INVASR":
                                case "INVJOR":
                                case "INVSAR":
                                case "INVCOR":
                                case "INVJON":
                                case "INVSAN":
                                    TypeObj = typeof(GInventory);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "PRDDIS":
                                case "PRDDIN":
                                case "PRDOUT":
                                case "REFPDOH":
                                    TypeObj = typeof(GProduction);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                            }


                            if (strError == "")
                            {
                                int i = count;
                                count++;
                                if (Request.QueryString["transtype"].ToString() == "WMSPICK")
                                {
                                    DataTable dtpick = Gears.RetriveData2("SELECT ErrorMsg FROM WMS.Picklist where ISNULL(ErrorMsg,'')!='' and Docnumber ='" + item + "'", Session["ConnString"].ToString());
                                    if (dtpick.Rows.Count > 0)
                                    {
                                        strError2 += "\r\n" + item + ": " + dtpick.Rows[0][0].ToString();
                                    }
                                }
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = prompt + "\r\n\r\nSubmitted: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Sufficient Access to do this action.";
                    }
                    break;
                #endregion

                #region ribbon Partial Submit
                case "RPartialSubmit":
                    cp.JSProperties["cp_onpartialsubmit"] = true;
                    break;

                case "RPartialSubmission":
                    access = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_POST'", Session["ConnString"].ToString());
                    foreach (DataRow dt in access.Rows)
                    {
                        value = dt["value"].ToString();
                    }
                    if (Request.QueryString["access"].Contains(value))
                    {
                        //GetSelectedValues();            
                        if (fieldValues == null) return;

                        foreach (string item in fieldValues)
                        {
                            gp._DocNo = item;
                            gp._TransType = Request.QueryString["transtype"].ToString();
                            gp._UserId = Session["userid"].ToString();
                            gp._Factor = 1;

                            //Type TypeObj = typeof(GWarehouseManagement);
                            //Object MyObj = Activator.CreateInstance(TypeObj);
                            //strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));

                            switch (gp._TransType)
                            {
                                case "WMSINB":
                                    strError = GWarehouseManagement.Inbound_Partial_Submit(gp);
                                    break;
                            }

                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Submitted: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Sufficient Access to do this action.";
                    }
                    break;
                #endregion

                #region ribbon Approved
                case "RApproved":
                    cp.JSProperties["cp_onapprove"] = true;
                    break;


                case "ROnApproved":
                    List<object> itemc = Translistgrid.GetSelectedFieldValues(itemcat);

                    DataTable accesss = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_APPV'", Session["ConnString"].ToString());
                    foreach (DataRow dt in accesss.Rows)
                    {
                        value = dt["value"].ToString();
                    }
                    if (Request.QueryString["access"].Contains(value))
                    {
                        strError = "";
                        strError2 = "";
                        if (fieldValues == null) return;

                        foreach (string item in fieldValues)
                        {
                            gp._DocNo = item;
                            gp._TransType = Request.QueryString["transtype"].ToString();
                            gp._UserId = Session["userid"].ToString();
                            gp._Factor = 1;


                            Type TypeObj = null;
                            Object MyObj = null;

                            switch (gp._TransType)
                            {
                                case "WMSPICK":
                                case "WMSPUT":
                                    strError = GearsWarehouseManagement.GWarehouseManagement.Approved(gp);
                                    break;



                            }



                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Approved: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Sufficient Access to do this action.";
                    }
                    break;
                #endregion

                #region ribbon Printout/Unprint
                case "RPrint":
                    access = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_PRINT'", Session["ConnString"].ToString());
                    foreach (DataRow dt in access.Rows)
                    {
                        value = dt["value"].ToString();
                    }
                    if (Request.QueryString["access"].Contains(value))
                    {
                        cp.JSProperties["cp_print"] = true;
                    }
                    else
                    {
                        cp.JSProperties["cp_redirect"] = "error";
                        cp.JSProperties["cp_errormes"] = "You don't have access for this specific action!";
                    }
                    break;
                case "RHRPrint":
                    cp.JSProperties["cp_HRprint"] = true;
                    break;
                case "RHRPrintSI":
                    cp.JSProperties["cp_HRPrintSI"] = true;
                    break;
                case "RJOCosting":
                    cp.JSProperties["cp_JOprint"] = true;
                    break;

                case "RUnprint":
                    cp.JSProperties["cp_unprint"] = true;

                    break;

                case "ROnUnprint":
                    List<object> IsPrint = Translistgrid.GetSelectedFieldValues(isprinted);

                    if (IsPrint.Count > 1)
                    {
                        cp.JSProperties["cp_redirect"] = "error";
                        cp.JSProperties["cp_errormes"] = "Multiple documents has been checked. Please select one document to unprint at a time.";
                    }
                    else
                    {
                        foreach (string item in fieldValues)
                        {
                            string tablename = "";
                            DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where transtype = '" + Request.QueryString["transtype"].ToString() + "'", Session["ConnString"].ToString());
                            foreach (DataRow dtRow in menutable.Rows)
                            {
                                tablename = dtRow["TableName"].ToString();
                            }
                            DataTable check = new DataTable();
                            check = Gears.RetriveData2("SELECT * FROM " + tablename + " WHERE DocNumber = '" + item + "' AND ISNULL(SubmittedBy,'') != ''", Session["ConnString"].ToString());

                            if (check.Rows.Count > 0)
                            {
                                gp._DocNo = item;
                                gp._Table = tablename;
                                gp._Factor = -1;
                                string strresult = GearsCommon.GCommon.IsPrinted_Tag(gp);
                            }

                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Unprinted: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    break;

                case "RMultiPrint":
                    cp.JSProperties["cp_multiprint"] = true;

                    break;

                case "ROnMultiPrint":
                    string multidocnum = "";

                    foreach (string item in fieldValues)
                    {
                        multidocnum += item + ",";
                    }
                    multidocnum = multidocnum.Remove(multidocnum.Length - 1);

                    DataTable printing = Gears.RetriveData2("select top 1 ReportName,isnull(Reprint,0) Reprint from masterfile.formlayout where formname = '" + transtype + "' and Text= 'MULTI'", Session["ConnString"].ToString());
                    foreach (DataRow dtRow in printing.Rows)
                    {
                        cp.JSProperties["cp_report"] = dtRow["ReportName"].ToString();
                        cp.JSProperties["cp_reprint"] = dtRow["Reprint"].ToString();
                    }
                    cp.JSProperties["cp_transtype"] = transtype;
                    cp.JSProperties["cp_docnumber"] = multidocnum;
                    cp.JSProperties["cp_multiprint2"] = true;


                    break;
                #endregion

                #region ribbon Putaway
                case "RPutaway":
                    List<object> putaway = Translistgrid.GetSelectedFieldValues(putawayby);
                    string Pby = "";

                    foreach (string put in putaway)
                    {
                        Pby = put;
                    }

                    if (!String.IsNullOrEmpty(Pby))
                    {
                        cp.JSProperties["cp_redirect"] = "error";
                        cp.JSProperties["cp_errormes"] = "Document has been already putaway.";
                    }
                    else if (putaway.Count > 1)
                    {
                        cp.JSProperties["cp_redirect"] = "error";
                        cp.JSProperties["cp_errormes"] = "Multiple documents has been checked. Please select one document to putaway at a time.";
                    }
                    else
                    {
                        foreach (string item in fieldValues)
                        {
                            cp.JSProperties["cp_docnumber"] = item;
                            cp.JSProperties["cp_redirect"] = "edit";
                            cp.JSProperties["cp_frm"] = frm;
                            cp.JSProperties["cp_transtype"] = transtype;
                            cp.JSProperties["cp_parameters"] = parameters;
                        }
                    }
                    break;
                #endregion

                #region ribbon Generate Physical Count
                //Genrev Added code
                case "RGeneratePCount":
                    //GetSelectedValues();
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();

                        strError = GWarehouseManagement.PhysicalCountWMS_Generate(gp);


                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;

                    //SubmitMsg.Text = strError2 + "!";
                    break;
                #endregion

                #region ribbon Generate Location
                case "RGenerateLocation":
                    //GetSelectedValues();
                    if (fieldValues == null) return;
                    strError = "";
                    strError2 = "";
                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;

                        strError = GearsWarehouseManagement.GWarehouseManagement.Picklist_GenerateLocation(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generate Location: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                #endregion

                #region ribbon Generate Picklist Detail
                case "RGeneratePicklistDetail":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        strError = GearsWarehouseManagement.GWarehouseManagement.Picklist_GenerateDetail(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                #endregion

                #region Generate Countsheet
                case "RGenerateCountsheet":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        strError = GearsWarehouseManagement.GWarehouseManagement.Inbound_GenerateDetail(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                #endregion

                #region ribbon Unsubmitted
                // 09/16/2016   GC  Added the access codes for unsubmission
                case "RLUnsubmitted":
                    cp.JSProperties["cp_onUnsubmit"] = true;
                    break;

                case "ExecRLUnsubmitted":
                    DataTable unsubmit = new DataTable();
                    if (Request.QueryString["transtype"].ToString() == "WMSTRN" ||
                        Request.QueryString["transtype"].ToString() == "WMSNON" || Request.QueryString["transtype"].ToString() == "WMSINB" || Request.QueryString["transtype"].ToString() == "WMSOUT")
                    {
                        unsubmit = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_UNSUBMIT'", Session["ConnString"].ToString());
                        foreach (DataRow dt in unsubmit.Rows)
                        {
                            value = dt["value"].ToString();
                        }
                    }
                    else
                    {
                        unsubmit = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_POST'", Session["ConnString"].ToString());
                        foreach (DataRow dt in unsubmit.Rows)
                        {
                            value = dt["value"].ToString();
                        }
                    }


                    if (Request.QueryString["access"].Contains(value))
                    {
                        strError = "";
                        strError2 = "";
                        if (fieldValues == null) return;

                        foreach (var item in fieldValues)
                        {
                            gp._DocNo = item.ToString();
                            gp._TransType = Request.QueryString["transtype"].ToString();
                            gp._UserId = Session["userid"].ToString();
                            gp._Factor = -1;
                            Type TypeObj = null;
                            Object MyObj = null;

                            switch (gp._TransType)
                            {
                                case "WMSPICK":
                                case "WMSOCN":
                                case "WMSINB":
                                case "WMSTRN":
                                case "WMSNON":
                                case "WMSICN":
                                case "WMSOUT":

                                    TypeObj = typeof(GWarehouseManagement);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;

                                case "PRDJOB":
                                    TypeObj = typeof(GProduction);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                            }

                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Unsubmit: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Sufficient Access to do this action.";
                    }
                    //end

                    //strError = "";
                    //strError2 = "";
                    //if (fieldValues == null) return;

                    //foreach (string item in fieldValues)
                    //{
                    //    gp._DocNo = item;
                    //    gp._TransType = Request.QueryString["transtype"].ToString();
                    //    gp._UserId = Session["userid"].ToString();
                    //    gp._Factor = -1;

                    //    Type TypeObj = typeof(GWarehouseManagement);
                    //    Object MyObj = Activator.CreateInstance(TypeObj);
                    //    strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));

                    //    if (strError == "")
                    //    {
                    //        int i = count;
                    //        count++;
                    //    }
                    //    else
                    //    {
                    //        strError2 += "\r\n" + item + ": " + strError;
                    //    }
                    //}
                    //cp.JSProperties["cp_submit"] = true;
                    //SubmitAlert.Text = "Unsubmit: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                #endregion

                #region ribbon unapproved
                case "RUnapproved":
                    cp.JSProperties["cp_onunapprove"] = true;
                    break;
                case "ROnUnapproved":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._TransType = Request.QueryString["transtype"].ToString();
                        gp._UserId = Session["userid"].ToString();
                        gp._Factor = -1;

                        Type TypeObj = null;
                        Object MyObj = null;

                        switch (gp._TransType)
                        {
                            case "ACTEXP":
                                strError = GearsAccounting.GAccounting.Approved(gp);
                                break;
                            case "PRDPIS":
                                TypeObj = typeof(GProduction);
                                MyObj = Activator.CreateInstance(TypeObj);
                                strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                break;
                            default:
                                TypeObj = typeof(GProcurement);
                                MyObj = Activator.CreateInstance(TypeObj);
                                strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                break;
                        }

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }

                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Unapprove: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                #endregion

                #region ribbon ImportCSheet
                case "RImportCSheet":
                    if (fieldValues.Count > 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "Cannot import to multiple documents!";
                    }
                    else if (fieldValues.Count < 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "Please select on table";
                    }
                    else
                    {
                        cp.JSProperties["cp_import"] = true;
                        Session["import"] = "0";
                    }
                    break;
                #endregion

                #region ribbon Import
                case "RImport":

                    cp.JSProperties["cp_import"] = true;
                    Session["import"] = "1";
                    break;
                #endregion

                #region ribbon ExportCSheet
                case "RExportCSheet":

                    if (fieldValues.Count > 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "Cannot export to multiple documents!";
                    }
                    else if (fieldValues.Count < 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "Please select on table";
                    }
                    else
                    {
                        cp.JSProperties["cp_onexport"] = true;
                        cp.JSProperties["cp_transtype"] = Request.QueryString["transtype"].ToString();
                        foreach (string item in fieldValues)
                        {
                            cp.JSProperties["cp_expdocnum"] = item;
                        }

                    }


                    break;
                #endregion

                #region ribbon Copy Transaction
                case "RCopyTrans":
                    cp.JSProperties["cp_masterfile"] = true;
                    cp.JSProperties["cp_redirect"] = "new";
                    cp.JSProperties["cp_frm"] = frm;
                    cp.JSProperties["cp_transtype"] = transtype;
                    cp.JSProperties["cp_parameters"] = "CopyTrans|" + fieldValues[0].ToString();

                    break;
                #endregion

                #region Generate Countsheet Subsi
                case "RGenerateCountsheetSubsi":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        strError = GearsWarehouseManagement.GWarehouseManagement.Pick_GenerateCountSheetSubsi(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                #endregion
                //ribbon for charges
                #region Charge
                case "RCharges":
                    cp.JSProperties["cp_Charges"] = true;
                    break;
                #endregion
                //ribbon for Blast
                #region AfterBlast
                case "RAfterBlast":
                    //sets the value of cp_afterblast to in the translist.aspx
                    cp.JSProperties["cp_AfterBlast"] = true;
                    break;
                case "RAfterBlastValidate":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        //gets the docnumber for selected docnumber in the list
                        gp._DocNo = item;
                        //select the docnumber with the column of submitted by and putaway by
                        DataTable inboundStatusSub = Gears.RetriveData2("SELECT DocNumber + ' is not yet submitted' FROM WMS.Inbound WHERE docnumber = '" + gp._DocNo + "' AND ISNULL(SubmittedBy, '') ='' ", Session["ConnString"].ToString());
                        DataTable inboundStatusPut = Gears.RetriveData2("SELECT DocNumber + ' is already putaway' FROM WMS.Inbound WHERE docnumber = '" + gp._DocNo + "' AND  ISNULL(PutAwayBy, '') != ''", Session["ConnString"].ToString());

                        if (inboundStatusSub.Rows.Count > 0)
                        {

                            //select the docnumber when the submitted by column is null then the error message will show
                            strError2 = inboundStatusSub.Rows[0][0].ToString();
                            //sets the value of cp_AfterBlastValidate and shows the submittedby error to in the translist.aspx

                            strError2 = "\n\n\n\nThe Docnumber " + strError2;
                        }
                        else if (inboundStatusPut.Rows.Count > 0)
                        {

                            //select the docnumber when the putaway by column is null then the error message will show
                            strError2 = inboundStatusPut.Rows[0][0].ToString();
                            //sets the value of cp_AfterBlastValidate and shows the putaway error in the translist.aspx

                            strError2 = "\n\n\n\nThe Docnumber " + strError2;
                        }
                        else
                        {
                            strError2 = "";
                            if (strError2 == "")
                            {
                                int i = count;
                                count++;
                            }
                            //sets the value of cp_ShowAB as true and shows the content of AfterBlast modal
                            cp.JSProperties["cp_ShowAB"] = true;



                        }

                    }
                    cp.JSProperties["cp_AfterBlastValidate"] = true;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;

                #endregion
                //ribbon for Submit Charges
                #region Submit Charge
                case "RSubmitCharges":
                    cp.JSProperties["cp_SubmitCharges"] = true;
                    break;
                case "RChargesSubmission":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._Factor = 1;
                        gp._TransType = Request.QueryString["transtype"].ToString();
                        DataTable ChargableServices = Gears.RetriveData2("SELECT ServiceType FROM wms.charges WHERE docnumber = '" + gp._DocNo + "' AND chargeflag = 1  AND Servicetype NOT IN ( SELECT ServiceType FROM wms.TransactionNonStorage WHERE ProdNum = '" + gp._DocNo + "' ) GROUP BY serviceType", Session["ConnString"].ToString());
                        if (ChargableServices.Rows.Count != 0)
                        {
                            foreach (DataRow dt in ChargableServices.Rows)
                            {
                                value = dt["ServiceType"].ToString();
                                DataTable ChargeSubmission = Gears.RetriveData2("exec sp_submit_Charges '" + gp._DocNo + "','" + HttpContext.Current.Session["userid"].ToString() + "','1','WMSINBCharges','" + value + "'", Session["ConnString"].ToString());

                                if (ChargeSubmission.Rows.Count > 1)
                                {
                                    foreach (DataRow dt1 in ChargeSubmission.Rows)
                                    {
                                        strError2 += "\r\n" + value + ": " + dt1["Msg"].ToString();
                                    }
                                }
                                else
                                {
                                    ;
                                    strError2 += "\r\n Successfully Charged:" + value + "";
                                }
                                int i = count;
                                count++;
                            }


                            break;
                        }
                        else
                        {
                            strError2 = "No Charges Submitted";
                        }


                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                //ribbon for MultiAssignChecker
                #region MultiAssign Checker
                case "RAssign":
                    cp.JSProperties["cp_RAssign"] = true;
                    break;
                case "RAssignProcess":
                    strError = "";
                    strError2 = "";

                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;

                        DataTable Checking = Gears.RetriveData2("EXEC [dbo].[sp_MULTI_ASSIGN_VALIDATION] 'INBOUND?','" + gp._DocNo + "'", Session["ConnString"].ToString());

                        if (Checking.Rows.Count > 0)
                        {
                            foreach (DataRow dt1 in Checking.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            DataTable CountAssign = Gears.RetriveData2("EXEC [dbo].[sp_MULTI_ASSIGN_VALIDATION] 'SPLIT','" + gp._DocNo + "'", Session["ConnString"].ToString());

                            if (CountAssign.Rows.Count > 1)
                            {
                                cp.JSProperties["cp_MultiAssign"] = true;
                            }
                            else
                            {
                                cp.JSProperties["cp_SingleAssign"] = true;
                            }
                        }
                        break;
                    }
                    cp.JSProperties["cp_RAssignProcess"] = true;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion
                //ribbon for COMI
                #region COMI
                case "RComi":
                    cp.JSProperties["cp_Comi"] = true;
                    break;
                #endregion
                //ribbon for Replenish
                #region Replenish
                case "RReplenish":
                    cp.JSProperties["cp_Replenish"] = true;
                    break;
                #endregion
                // 2023-09-18   RA  Extract Import Template       
                #region Extract Template base on save files
                case "RExtractTemplate":
                    cp.JSProperties["cp_templateextract"] = true;
                    break;
                #endregion
                // 2023-09-18   RA  Extract Import Template

                #region ribbon Copy
                case "RCopy":
                    cp.JSProperties["cp_copy"] = true;
                    break;
                #endregion

                #region Cancel
                //genrev changed and added code 
                case "RCancel":
                    cp.JSProperties["cp_onCancel"] = true;
                    break;

                case "RCancelExec":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._TransType = Request.QueryString["transtype"].ToString();
                        gp._UserId = Session["userid"].ToString();
                        strError = GearsCommon.GCommon.TagCancel(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Cancel: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                //end
                #endregion

                #region ForceClose
                //genrev changed code 01/23/2015
                case "RForceClose":
                    //int counttemp = 0;

                    //List<object> Lstatus = Translistgrid.GetSelectedFieldValues(status);
                    //if (
                    //    (
                    //        (Lstatus[0].ToString() == "P" && counttemp == 0 && (gp._TransType = Request.QueryString["transtype"].ToString()) == "PRCPRM")
                    //        || (Lstatus[0].ToString() == "P" && counttemp == 0 && (gp._TransType = Request.QueryString["transtype"].ToString()) == "PRCPOM")
                    //    ) || (Request.QueryString["transtype"].ToString() == "SLSORD") || (Request.QueryString["transtype"].ToString() == "SLSQUM") || (Request.QueryString["transtype"].ToString() == "PRCSCO")
                    //   )
                    //{
                    cp.JSProperties["cp_onFC"] = true;
                    //    if (Request.QueryString["transtype"].ToString() == "PRCPRM" || Request.QueryString["transtype"].ToString() == "PRCPOM")
                    //    {
                    //        counttemp = 1;
                    //    }
                    //}
                    break;
                #endregion

                #region ForceClosing
                case "RForceClosing":
                    DataTable ForceClose = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_FCLOSE'", Session["ConnString"].ToString());
                    foreach (DataRow dt in ForceClose.Rows)
                    {
                        value = dt["value"].ToString();
                    }

                    if (Request.QueryString["access"].Contains(value))
                    {
                        strError = "";
                        strError2 = "";
                        if (fieldValues == null) return;

                        foreach (string item in fieldValues)
                        {
                            gp._DocNo = item;
                            gp._TransType = Request.QueryString["transtype"].ToString();
                            gp._UserId = Session["userid"].ToString();
                            strError = GearsSales.GSales.ForceClose(gp);

                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Manual Closed: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Access to do this action.";
                    }
                    break;
                #endregion

                #region Monthend Closing
                case "RMonthEndClosing":
                    strError = "";
                    strError2 = "";
                    DataTable ME = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_MONTHEND'", Session["ConnString"].ToString());
                    foreach (DataRow dt in ME.Rows)
                    {
                        value = dt["value"].ToString();
                    }

                    if (Request.QueryString["access"].Contains(value))
                    {




                        if (fieldValues == null) return;

                        foreach (string item in fieldValues)
                        {
                            gp._DocNo = item;
                            gp._UserId = Session["userid"].ToString();
                            strError = GearsAccounting.GAccounting.MonthEndClosing(gp);

                            strError = strError.Replace("\r\n", "");
                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = String.IsNullOrEmpty(strError) ? "Month End closing successfull!" : strError;

                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Access to do this action.";
                    }
                    break;
                #endregion

                #region Generate Room Cascade
                case "RRoomCascade":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;

                        strError = GWarehouseManagement.RoomCascade(gp);
                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;

                    //SubmitMsg.Text = strError2 + "!";
                    break;
                #endregion

                #region Export Translist
                case "RExport":
                    cp.JSProperties["cp_exporting"] = true;
                    break;
                #endregion

                //GC Gen PC Variance
                #region Generate PCount Variance

                case "RGeneratePCVar":
                    DataTable PCVar = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code = 'ACC_GENPC'", Session["ConnString"].ToString());
                    foreach (DataRow dt in PCVar.Rows)
                    {
                        value = dt["Value"].ToString();
                    }

                    if (Request.QueryString["access"].Contains(value))
                    {
                        cp.JSProperties["cp_onPCVar"] = true;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Access to do this action.";
                    }
                    break;

                case "RGeneratePCVarExec":

                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._Connection = Session["ConnString"].ToString();
                        strError = GearsInventory.GInventory.PhysicalCount_GenVariance(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generated : " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;

                    break;
                #endregion

                //GC Update PC IsFinal
                #region PCount IsFinal

                case "RGeneratePCIsFinal":
                    Session["Isfinal"] = "1";
                    cp.JSProperties["cp_onIsFinal"] = true;
                    break;

                case "RRevertPCIsFinal":
                    Session["Isfinal"] = "-1";
                    cp.JSProperties["cp_onIsFinal"] = true;
                    break;

                case "RIsFinalInitiate":
                    DataTable PCIsFinal = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code = 'ACC_TAGFL'", Session["ConnString"].ToString());
                    foreach (DataRow dt in PCIsFinal.Rows)
                    {
                        value = dt["Value"].ToString();
                    }

                    if (Request.QueryString["access"].Contains(value))
                    {
                        cp.JSProperties["cp_onPCIsFinal"] = true;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Access to do this action.";
                    }
                    break;

                case "RGeneratePCIsFinalExec":

                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._Connection = Session["ConnString"].ToString();
                        gp._Factor = Convert.ToInt32(Session["Isfinal"].ToString());
                        strError = GearsInventory.GInventory.PhysicalCount_IsFinal(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Updated : " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;

                    break;
                #endregion

                #region Activate
                case "RActivate":
                    DataTable Act = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_ACTIVATE'", Session["ConnString"].ToString());
                    foreach (DataRow dt in Act.Rows)
                    {
                        value = dt["value"].ToString();
                    }

                    if (Request.QueryString["access"].Contains(value))
                    {
                        strError = "";
                        strError2 = "";
                        if (fieldValues == null) return;

                        DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where moduleid = '" + Request.QueryString["moduleid"].ToString() + "'", Session["ConnString"].ToString());
                        string tablename = "";
                        foreach (DataRow dtRow in menutable.Rows)
                        {
                            tablename = dtRow["TableName"].ToString();
                        }

                        foreach (string item in fieldValues)
                        {

                            gp._DocNo = item;
                            gp._UserId = Session["userid"].ToString();
                            gp._Table = tablename;
                            gp._Action = Translistgrid.KeyFieldName;
                            strError = GearsCommon.GCommon.Activate(gp);

                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Activate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Access to do this action.";
                    }
                    break;
                #endregion

                #region Deactivate
                case "RDeactivate":
                    DataTable Dec = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_DEACTIVATE'", Session["ConnString"].ToString());
                    foreach (DataRow dt in Dec.Rows)
                    {
                        value = dt["value"].ToString();
                    }

                    if (Request.QueryString["access"].Contains(value))
                    {
                        strError = "";
                        strError2 = "";
                        if (fieldValues == null) return;

                        DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where moduleid = '" + Request.QueryString["moduleid"].ToString() + "'", Session["ConnString"].ToString());
                        string tablename = "";
                        foreach (DataRow dtRow in menutable.Rows)
                        {
                            tablename = dtRow["TableName"].ToString();
                        }

                        foreach (string item in fieldValues)
                        {
                            gp._DocNo = item;
                            gp._UserId = Session["userid"].ToString();
                            gp._Table = tablename;
                            gp._Action = Translistgrid.KeyFieldName;
                            strError = GearsCommon.GCommon.Deactivate(gp);

                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Deactivate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Access to do this action.";
                    }
                    break;
                #endregion
                // GC Added code 02-27-2016
                #region ribbon Custom Export
                case "RCustomExport":

                    int cusexp = 0;

                    foreach (string item in fieldValues)
                    {
                        cusexp = cusexp + 1;
                    }

                    if (cusexp == 1)
                    {
                        foreach (string item in fieldValues)
                        {
                            cp.JSProperties["cp_customexport"] = true;
                            cp.JSProperties["cp_customexportitem"] = item;
                            cp.JSProperties["cp_customexporttrans"] = Request.QueryString["transtype"].ToString();
                        }
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Multiple transaction were selected!";
                    }
                    break;
                #endregion

                #region Connection String
                case "RConnstring":
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = Gears.Decrypt(GearsLibrary.Gears.strCoreConnectionDB, "8GearsMeUp8");
                    break;
                #endregion

                // 2016-03-18  Tony

                //2016-04-05 Kate
                #region FuncGroupClose
                case "RFuncGroupClose":

                    string pwcheck1 = "";
                    string pwinitial1 = "";
                    string UserName1 = "";
                    PW.Text = "";
                    cp.JSProperties["cp_InputPass"] = true;


                    break;


                case "RAuthorizeCheck":

                    string pwcheck = "";
                    string pwinitial = "";
                    string UserName = "";
                    DataTable accesscheck = Gears.RetriveData2("select Password,UserName from it.Users Where UserID = '" + Session["Userid"].ToString() + "'", Session["ConnString"].ToString());
                    foreach (DataRow dt in accesscheck.Rows)
                    {
                        pwinitial = dt["Password"].ToString();
                        UserName = dt["UserName"].ToString();
                        pwcheck = Gears.PasswordEncrypt(UserName, PW.Value.ToString());
                    }
                    if (pwcheck == pwinitial)
                    {
                        if (Request.QueryString["moduleid"].ToString() == "FUNCGRP")//KATY
                        {
                            cp.JSProperties["cp_funcauthorize"] = true;
                        }
                        else
                        {
                            cp.JSProperties["cp_onauthorize"] = true;
                        }

                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Wrong input password.";
                    }

                    break;
                #endregion
                //2016-04-05 Kate

                //2016-04-05 Kate
                #region FuncGroupClose
                case "RFuncClose":



                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._TransType = Request.QueryString["transtype"].ToString();
                        gp._UserId = Session["userid"].ToString();
                        strError = GearsCommon.GCommon.FuncGroupClose(gp);

                        if (strError.Replace("\r\n", "") == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Manual Closed: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;



                    break;
                #endregion
                //2016-04-05 Kate

                //2016-04-11 TLAV
                #region ribbon Submit
                case "RStartDIS":
                    cp.JSProperties["cp_onstartdis"] = true;
                    break;
                case "ROnStartDIS":
                    DataTable accessstartdis = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_STRTDIS'", Session["ConnString"].ToString());
                    foreach (DataRow dt in accessstartdis.Rows)
                    {
                        value = dt["value"].ToString();
                    }
                    if (Request.QueryString["access"].Contains(value))
                    {
                        if (fieldValues == null) return;
                        foreach (var item in fieldValues)
                        {
                            gp._DocNo = item.ToString();
                            gp._TransType = Request.QueryString["transtype"].ToString();
                            gp._UserId = Session["userid"].ToString();
                            gp._Factor = 1;
                            Type TypeObj = null;
                            Object MyObj = null;
                            switch (gp._TransType)
                            {
                                case "PRDDIS":
                                    strError = Convert.ToString(GProduction.DesignInforSheet_StartDIS(gp));
                                    break;
                            }
                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "StartDIS: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Sufficient Access to do this action.";
                    }
                    break;
                #endregion

                #region ribbon Extraction
                case "RExtract":
                    Session["extractdata"] = "1";
                    cp.JSProperties["cp_extract"] = true;



                    // ASPxGridView gvExportMetaData = new ASPxGridView();
                    // gvExportMetaData.ID = "Template" + " of " + transtype;

                    //gvExportMetaData.DataSource = dtsout;
                    //gvExportMetaData.FilterExpression = "1=0";
                    //gvExportMetaData.DataBind();

                    //ASPxGridViewExporter gvExporter = new ASPxGridViewExporter();
                    //gvExporter.ID = "gvExporter";
                    //gvExporter.GridViewID = "Template" + " of " + transtype;
                    //gvExporter.DataBind();

                    //// important!
                    //gvExportMetaData.ClientVisible = false;
                    //form1.Controls.Add(gvExportMetaData);
                    //form1.Controls.Add(gvExporter);

                    //gvExporter.WriteCsvToResponse();

                    break;
                #endregion
                // 2016-05-18  RA

                #region Posting
                case "RPost":

                    access = Gears.RetriveData2("select value From it.SystemSettings where Code = 'ACC_GLPOST'", Session["ConnString"].ToString());
                    foreach (DataRow dt in access.Rows)
                    {
                        value = dt["value"].ToString();
                    }
                    if (Request.QueryString["access"].Contains(value))
                    {
                        //GetSelectedValues();            
                        if (fieldValues == null) return;

                        foreach (var item in fieldValues)
                        {

                            string tablename = "";
                            DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where transtype = '" + Request.QueryString["transtype"].ToString() + "'", Session["ConnString"].ToString());
                            foreach (DataRow dtRow in menutable.Rows)
                            {
                                tablename = dtRow["TableName"].ToString();
                            }

                            gp._DocNo = item.ToString();
                            gp._TransType = Request.QueryString["transtype"].ToString();
                            gp._UserId = Session["userid"].ToString();
                            gp._Factor = 2;
                            gp._Table = tablename;
                            //Type TypeObj = typeof(GWarehouseManagement);
                            //Object MyObj = Activator.CreateInstance(TypeObj);
                            //strError = Convert.ToString(TypeObj.InvokeMember(sp, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));

                            Type TypeObj = null;
                            Object MyObj = null;

                            switch (gp._TransType)
                            {
                                case "WMSCON":
                                case "WMSBIL":
                                case "WMSBOS":
                                case "WMSTRN":
                                case "WMSNON":
                                case "WMSICN":
                                case "WMSINB":
                                case "WMSPUT":
                                case "WMSIA":
                                case "WMSREL":
                                case "WMSRSV":
                                case "WMSOCN":
                                case "WMSPICK":
                                case "WMSOUT":
                                case "WMSPHC":
                                    TypeObj = typeof(GWarehouseManagement);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(glpost, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "PRCPOM":
                                case "PRCQUM":
                                case "PRCPRM":
                                //case "PRCRCR":
                                //case "PRCPCC":
                                case "PRPRT":
                                case "PRPOR":
                                case "PRCSCO":
                                case "PRCSOR":
                                case "PRCWIN":
                                case "PRCWOT":
                                    //case "ACTAQT": //LGE 03-08-2016
                                    TypeObj = typeof(GProcurement);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(glpost, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "SLSQUM":
                                case "SLSORD":
                                case "SLSDRC":
                                case "SLSINV":
                                case "SLSSRN":
                                case "SLSSOR":
                                case "SLSSOD":
                                case "SLSSFC":
                                    TypeObj = typeof(GSales);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(glpost, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "ACTRVL":
                                case "ACTPRT":
                                case "ACTJOV":
                                case "ACTRJS":
                                case "ACTBAL":
                                case "ACTJVT":
                                //case "ACTAQT": LGE 03-08-2016
                                case "ACTADS":
                                case "ACTADI":
                                case "ACTCOA":
                                case "ACTAMZ":
                                case "ACTBAS":
                                case "ACTALE":
                                case "ACTGLG":
                                case "ACTDES":
                                case "ACTCPV":
                                case "ACTPCI":
                                case "ACTBRC":
                                case "ACTCCI":
                                case "ACTCOC":
                                case "ACTCAD": //added
                                case "ACTLCA": //added
                                case "ACTCRB": //added
                                case "ACTCRP": //added
                                case "ACTAPV":
                                case "ACTAPM":
                                case "ACTEXP":
                                case "ACTCHV":
                                case "ACTSIN":
                                case "ACTSEB":
                                case "ACTCOS":
                                case "ACTARM":
                                case "ACTCOL":
                                case "PRCPCC":
                                case "PRCRCR":
                                case "ACTAQT": //LGE 03-08-2016
                                    TypeObj = typeof(GAccounting);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(glpost, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "INVFRL":
                                case "INVFOR":
                                case "INVREV":
                                case "INVSTR":
                                case "INVADJT":
                                case "INVCNT":
                                case "INVMAN":
                                case "INVOSI":
                                case "INVASI":
                                case "INVJOI":
                                case "INVSAI":
                                case "INVCOI":
                                case "INVOSR":
                                case "INVASR":
                                case "INVJOR":
                                case "INVSAR":
                                case "INVCOR":
                                case "INVJON":
                                case "INVSAN":
                                    TypeObj = typeof(GInventory);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(glpost, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                                case "PRDDIS":
                                case "PRDDIN":
                                case "PRDOUT":
                                case "REFPDOH":
                                    TypeObj = typeof(GProduction);
                                    MyObj = Activator.CreateInstance(TypeObj);
                                    strError = Convert.ToString(TypeObj.InvokeMember(glpost, BindingFlags.InvokeMethod | BindingFlags.Default, null, MyObj, new Object[] { gp }));
                                    break;
                            }


                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Posted: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Sufficient Access to do this action.";
                    }

                    break;
                #endregion

                //inclusion of DOCUMENT TAB RIBBON 1/26/2024 SA
                #region Upload/Download Document
                case "RUpldDocs":
                    if (fieldValues.Count > 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "Cannot upload to multiple documents!";
                    }
                    else if (fieldValues.Count < 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "No selected document!";
                    }
                    else
                    {
                        String _TransType = Request.QueryString["transtype"].ToString();
                        String _DocNumber = fieldValues[0].ToString();

                        DataTable dtTemp = Gears.RetriveData2(
                            "SELECT FileList FROM IT.Documents " +
                            "WHERE TransType = '" + _TransType + "' AND DocNumber = '" + _DocNumber + "'"
                            , Session["ConnString"].ToString());
                        if (dtTemp.Rows.Count > 0) {
                            cp.JSProperties["cp_errorimport"] = "Document has already been uploaded!";
                        }
                        else {
                            cp.JSProperties["cp_uploaddocs"] = true;
                            cp.JSProperties["cp_uploadtrntyp"] = _TransType;
                            cp.JSProperties["cp_uploaddocnum"] = _DocNumber;
                        }
                        dtTemp.Dispose();
                    }
                    break;
                case "RDwnldDocs":
                    if (fieldValues.Count > 0)
                    {
                        if (!Directory.Exists(Page.MapPath("~/") + "TempDocuments"))
                        {
                            Directory.CreateDirectory(Page.MapPath("~/") + "TempDocuments");
                        }
                        String _TransType = Request.QueryString["transtype"].ToString();
                        String DocNumbers = "";
                        foreach (var item in fieldValues)
                        {
                            DocNumbers += ",'" + item.ToString() + "'";
                        }
                        String _DocPath = "";
                        DataTable dtTemp = Gears.RetriveData2(
                            "SELECT (SELECT Value FROM IT.SystemSettings WHERE Code = 'DOCSPATH')+'\\'+" +
                            "(SELECT Value FROM IT.SystemSettings WHERE Code = 'DOCSFOLDER') AS DocsPath"
                            , Session["ConnString"].ToString());
                        if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0][0] != DBNull.Value)
                        {
                            _DocPath = dtTemp.Rows[0][0].ToString();
                        }
                        if (_DocPath != "")
                        {
                            dtTemp = Gears.RetriveData2(
                                "SELECT FileList FROM IT.Documents " +
                                "WHERE TransType = '" + _TransType + "' AND DocNumber IN (" + DocNumbers.Substring(1) + ")"
                                , Session["ConnString"].ToString());
                            if (dtTemp.Rows.Count > 0)
                            {
                                string FileList = "";
                                foreach (DataRow _row in dtTemp.Rows)
                                {
                                    String[] _FileList = _row["FileList"].ToString().Split('|');
                                    for (int i = 0; i < _FileList.Length; i++)
                                    {
                                        if (File.Exists(Page.MapPath("~/") + "TempDocuments\\" + _FileList[i]))
                                        {
                                            File.Delete(Page.MapPath("~/") + "TempDocuments\\" + _FileList[i]);
                                        }
                                        File.Copy(_DocPath + "\\" + _FileList[i]
                                                  , Page.MapPath("~/") + "TempDocuments\\" + _FileList[i]);
                                    }
                                    FileList += "|" + _row["FileList"].ToString();
                                }
                                cp.JSProperties["cp_dwnlddocs"] = FileList.Substring(1);
                            }
                            else
                            {
                                cp.JSProperties["cp_errorimport"] = "No available file for download !";
                            }
                        }
                        else
                        {
                            cp.JSProperties["cp_errorimport"] = "Missing required system settings !";
                        }
                        dtTemp.Dispose();
                    }
                    else
                    {
                        cp.JSProperties["cp_errorimport"] = "Please select a transaction !";
                    }
                    break;
                #endregion
                #region Incomplete Document / Incorrect Document
                //case "RDocRmk":
                //case "RDocError":
                //    if (fieldValues.Count > 1)
                //    {
                //        cp.JSProperties["cp_errorimport"] = "Cannot update multiple documents at once!";
                //    }
                //    else if (fieldValues.Count < 1)
                //    {
                //        cp.JSProperties["cp_errorimport"] = "No selected document!";
                //    }
                //    else
                //    {
                //        List<object> _DocList = Translistgrid.GetSelectedFieldValues(new string[] { "DocNumber" });

                //        String _TransType, _DocNumber;
                //        if (transtype == "ACTDOC")
                //        {
                //            String _identifier = _DocList[0].ToString();
                //            int _index = _identifier.IndexOf('-');
                //            _TransType = _identifier.Substring(0, _index);
                //            _DocNumber = _identifier.Substring(_index+1);
                //        }
                //        else
                //        {
                //            _TransType = transtype;
                //            _DocNumber = _DocList[0].ToString();
                //        }

                //        DataTable dtTemp;
                //        if (e.Parameter == "RDocRmk")
                //        {
                //            dtTemp = Gears.RetriveData2(
                //                "SELECT CASE WHEN ISNULL(Complete,0) = 0 THEN 'Y' ELSE 'N' END AS Incomplete, ISNULL(MissingDocs,'') AS MissingDocs " +
                //                "FROM IT.Documents WHERE TransType = '" + _TransType + "' AND DocNumber = '" + _DocNumber + "' "
                //                , Session["ConnString"].ToString());
                //        }
                //        else
                //        {
                //            dtTemp = Gears.RetriveData2(
                //                "SELECT CASE WHEN ISNULL(Verified,0) = 0 THEN 'Y' ELSE 'N' END AS Unverified, ISNULL(MissingDocs,'') AS MissingDocs " +
                //                "FROM IT.Documents WHERE TransType = '" + _TransType + "' AND DocNumber = '" + _DocNumber + "' "
                //                , Session["ConnString"].ToString());
                //        }
                //        if (dtTemp.Rows.Count > 0)
                //        {
                //            if (dtTemp.Rows[0][0].ToString() == "Y")
                //            {
                //                cp.JSProperties["cp_docremarks"] = true;
                //                cp.JSProperties["cp_drmkrmktyp"] = (e.Parameter == "RDocRmk" ? "MISSING" : "INCORRECT");
                //                cp.JSProperties["cp_drmktrntyp"] = _TransType;
                //                cp.JSProperties["cp_drmkdocnum"] = _DocNumber;
                //                cp.JSProperties["cp_drmkrmrks"] = dtTemp.Rows[0][1].ToString();
                //                if (e.Parameter == "RDocRmk")
                //                {
                //                    dtTemp = Gears.RetriveData2(
                //                        "SELECT ISNULL(DocumentList,'') AS DocumentList FROM IT.TransTypeParam WHERE TransType = '" + _TransType + "'"
                //                        , Session["ConnString"].ToString());
                //                    if (dtTemp.Rows.Count > 0)
                //                    {
                //                        cp.JSProperties["cp_drmkdoclst"] = dtTemp.Rows[0][0].ToString();
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                cp.JSProperties["cp_errorimport"] = "Transaction has already been marked as " + (e.Parameter == "RDocRmk" ? "complete" : "verified");
                //            }
                //        }
                //        else
                //        {
                //            cp.JSProperties["cp_errorimport"] = "Transaction has no scanned document yet";
                //        }
                //        dtTemp.Dispose();
                //    }
                //    break;
                #endregion
                #region Append Document
                case "RAppnDocs":
                    if (fieldValues.Count > 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "Cannot append to multiple documents!";
                    }
                    else if (fieldValues.Count < 1)
                    {
                        cp.JSProperties["cp_errorimport"] = "No selected document!";
                    }
                    else
                    {
                        String _TransType, _DocNumber;
                        if (transtype == "ACTDOC")
                        {
                            List<object> _TransList = Translistgrid.GetSelectedFieldValues(new string[] { "TransType", "_DocNumber" });
                            object[] _Identifier = ((object[])_TransList[0]);
                            _TransType = _Identifier[0].ToString();
                            _DocNumber = _Identifier[1].ToString();
                        }
                        else
                        {
                            _TransType = transtype;
                            _DocNumber = fieldValues[0].ToString();
                        }

                        DataTable dtTemp = Gears.RetriveData2(
                            "SELECT CASE WHEN DateCompleted IS NULL THEN 0 ELSE ISNULL(Complete,0) END AS Complete" +
                            ", CASE WHEN DateVerified IS NULL THEN 0 ELSE ISNULL(Verified,0) END AS Verified " +
                            "FROM IT.Documents " +
                            "WHERE TransType = '" + _TransType + "' AND DocNumber = '" + _DocNumber + "'  AND ISNULL(Rejected,0) = 0"
                            , Session["ConnString"].ToString());
                        if (dtTemp.Rows.Count == 0)
                        {
                            cp.JSProperties["cp_errorimport"] = "Document not yet uploaded or has been rejected!";
                        }
                        else if (Convert.ToBoolean(dtTemp.Rows[0]["Complete"]))
                        {
                            cp.JSProperties["cp_errorimport"] = "Document already marked as complete!";
                        }
                        else if (Convert.ToBoolean(dtTemp.Rows[0]["Verified"]))
                        {
                            cp.JSProperties["cp_errorimport"] = "Document already marked as verified!";
                        }
                        else
                        {
                            cp.JSProperties["cp_uploaddocs"] = true;
                            cp.JSProperties["cp_uploadtrntyp"] = _TransType;
                            cp.JSProperties["cp_uploaddocnum"] = _DocNumber;
                            cp.JSProperties["cp_appenddocs"] = true;
                        }
                        dtTemp.Dispose();
                    }
                    break;
                #endregion
                #region Document Transmittal / Document Complete
                //case "RDocTrnsmtl":
                //case "RDocComplete":
                //case "RDocVerify":
                //    if (fieldValues.Count < 1)
                //    {
                //        cp.JSProperties["cp_errorimport"] = "Please select a transaction !";
                //    }
                //    else
                //    {
                //        string strDocList = "";
                //        if (transtype == "ACTDOC")
                //        {
                //            List<object> _DocList = Translistgrid.GetSelectedFieldValues(new string[] { "DocNumber" });
                //            foreach (var item in _DocList)
                //            {
                //                if (!String.IsNullOrEmpty(item.ToString()))
                //                {
                //                    strDocList += ",'" + item.ToString() + "'";
                //                }
                //            }
                //        }
                //        else
                //        {
                //            DataTable dtTemp = Gears.RetriveData2(
                //                "SELECT DISTINCT TableName FROM IT.MainMenu WHERE TransType = '" + transtype + "' AND ISNULL(TableName,'') != ''"
                //                , Session["ConnString"].ToString());
                //            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0][0] != DBNull.Value)
                //            {
                //                string strTableName = dtTemp.Rows[0][0].ToString();
                //                dtTemp.Dispose();

                //                List<object> _DocList = Translistgrid.GetSelectedFieldValues(new string[] { "DocNumber" });
                //                string _DocNumList = "";
                //                foreach (var item in _DocList)
                //                {
                //                    if (!String.IsNullOrEmpty(item.ToString()))
                //                    {
                //                        strDocList += ",'" + transtype + "-" + item.ToString() + "'";
                //                        _DocNumList += ",'" + item.ToString() + "'";
                //                    }
                //                }

                //                dtTemp = Gears.RetriveData2(
                //                    "SELECT TRN.DocNumber FROM " + strTableName + " TRN LEFT JOIN IT.Documents DOC ON DOC.TransType = '" + transtype + "' AND TRN.DocNumber = DOC.DocNumber " +
                //                    "WHERE TRN.DocNumber IN (" + _DocNumList.Substring(1) + ") AND DOC.DocNumber IS NULL"
                //                    , Session["ConnString"].ToString());
                //                if (dtTemp.Rows.Count > 0)
                //                {
                //                    _DocNumList = "";
                //                    foreach (DataRow _Row in dtTemp.Rows)
                //                    {
                //                        _DocNumList = _DocNumList + ", " + _Row[0].ToString();
                //                    }
                //                    cp.JSProperties["cp_errorimport"] = "Following transaction(s) have no scanned document: " + _DocNumList.Substring(2);
                //                    strDocList = "";
                //                }
                //            }
                //            else
                //            {
                //                cp.JSProperties["cp_errorimport"] = "Unable to check transmittal status !";
                //            }
                //            dtTemp.Dispose();
                //        }
                //        if (strDocList != "")
                //        {
                //            strDocList = strDocList.Substring(1);
                //            string strErrmssg;
                //            if (e.Parameter == "RDocTrnsmtl")
                //            {
                //                strErrmssg = ExecuteSQL(
                //                    "UPDATE IT.Documents SET Received = '-Y-', DateReceived = GETDATE() " +
                //                    "WHERE TransType+'-'+DocNumber IN (" + strDocList + ") AND Received = '-N-'");
                //                if (strErrmssg != "")
                //                {
                //                    cp.JSProperties["cp_errorimport"] = strErrmssg;
                //                }
                //                else
                //                {
                //                    cp.JSProperties["cp_submit"] = true;
                //                    SubmitAlert.HeaderText = "Document Transmittal";
                //                    SubmitAlert.Text = "Document(s) have been marked as received";
                //                    cp.JSProperties["cp_refresh"] = true;
                //                }
                //            }
                //            else if (e.Parameter == "RDocComplete")
                //            {
                //                strErrmssg = ExecuteSQL(
                //                    "UPDATE IT.Documents SET Complete = 1, DateCompleted = GETDATE(), MissingDocs = '' " +
                //                    "WHERE TransType+'-'+DocNumber IN (" + strDocList + ") AND ISNULL(Complete,0) = 0");
                //                if (strErrmssg != "")
                //                {
                //                    cp.JSProperties["cp_errorimport"] = strErrmssg;
                //                }
                //                else
                //                {
                //                    cp.JSProperties["cp_submit"] = true;
                //                    SubmitAlert.HeaderText = "Document Completion";
                //                    SubmitAlert.Text = "Document(s) have been marked as complete";
                //                    cp.JSProperties["cp_refresh"] = true;
                //                }
                //            }
                //            else if (e.Parameter == "RDocVerify")
                //            {
                //                strErrmssg = ExecuteSQL(
                //                    "UPDATE IT.Documents SET Verified = 1, DateVerified = GETDATE(), Rejected = 0, MissingDocs = '' " +
                //                    "WHERE TransType+'-'+DocNumber IN (" + strDocList + ") AND ISNULL(Verified,0) = 0");
                //                if (strErrmssg != "")
                //                {
                //                    cp.JSProperties["cp_errorimport"] = strErrmssg;
                //                }
                //                else
                //                {
                //                    cp.JSProperties["cp_submit"] = true;
                //                    SubmitAlert.HeaderText = "Document Verification";
                //                    SubmitAlert.Text = "Document(s) have been marked as verified";
                //                    cp.JSProperties["cp_refresh"] = true;
                //                }
                //            }
                //        }
                //    }
                //    break;
                #endregion
                //end inclusion of code 1/26/2024 SA

                #region For Submission
                case "RForSubmission":


                    cp.JSProperties["cp_forsubmission"] = true;

                    break;

                #endregion

                // 2016-05-18  RA END

                //end
                //2016-10-20  Jayr View
                #region View Open
                case "ROpen":


                    cp.JSProperties["cp_open"] = "true";
                    cp.JSProperties["cp_frm"] = frm.Replace(".aspx", "Open.aspx");
                    cp.JSProperties["cp_transtype"] = transtype;
                    cp.JSProperties["cp_parameters"] = parameters;



                    break;
                #endregion

                #region ribbon Putaway
                case "RTranOpen":
                    List<object> activity = Translistgrid.GetSelectedFieldValues(activityarr);
                    List<object> doctr = Translistgrid.GetSelectedFieldValues(docnumarr);
                    string act = "";

                    foreach (string vity in activity)
                    {
                        act = vity;
                    }

                    if (doctr.Count > 1)
                    {
                        cp.JSProperties["cp_redirect"] = "error";
                        cp.JSProperties["cp_errormes"] = "Multiple documents has been checked. Please select one document to adjust data at a time.";
                        return;
                    }
                    else if (doctr.Count == 0)
                    {
                        cp.JSProperties["cp_redirect"] = "error";
                        cp.JSProperties["cp_errormes"] = "No document selected.";
                        return;
                    }
                    else
                    {
                        foreach (string item in doctr)
                        {
                            if (act == "Inbound")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmInbound.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = parameters;
                            }
                            else if (act == "Putaway")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmPutaway.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = parameters;
                            }
                            else if (act == "OutBound")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmOutbound.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = parameters;

                            }
                            else if (act == "Picklist")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmPickList.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = parameters;

                            }
                            else if (act == "Item Adjustment")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmItemAdjustmentOpen.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = parameters;

                            }
                            else if (act == "Item Relocation")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmItemRelocationModuleOpen.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = "N";

                            }
                            else if (act == "Item Reservation")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmItemReservation.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = "R";

                            }
                            else if (act == "Internal Transfer")
                            {
                                cp.JSProperties["cp_docnumber"] = item;
                                cp.JSProperties["cp_redirect"] = "view";
                                cp.JSProperties["cp_frm"] = ".\\WMS\\frmInternalTransfer.aspx";
                                cp.JSProperties["cp_transtype"] = transtype;
                                cp.JSProperties["cp_parameters"] = parameters;

                            }
                            cp.JSProperties["cp_tropen"] = true;
                        }
                    }
                    break;
                #endregion

                #region ribbon StorageType
                case "RUpdateStorage":
                    cp.JSProperties["cp_storagetype"] = true;


                    break;
                #endregion

                #region ribbon StorageType
                case "RStorageReloc":
                    DataTable updtable = new DataTable();
                    updtable.Columns.Add("TransType", typeof(string));
                    updtable.Columns.Add("Location", typeof(string));
                    updtable.Columns.Add("UserID", typeof(string));
                    updtable.Columns.Add("StorageType", typeof(string));
                    updtable.Columns.Add("WarehouseCode", typeof(string));
                    foreach (string item in fieldValues)
                    {
                        updtable.Rows.Add("REFLOC", item, Session["userid"].ToString(), glStorageType.Text, Session["WHCode"].ToString());
                    }
                    GWarehouseManagement._connectionString = Session["ConnString"].ToString();
                    string message = GWarehouseManagement.OpenFunction("Storagetype", updtable);

                    if (!string.IsNullOrEmpty(message))
                    {

                        cp.JSProperties["cp_storagetypedone"] = true;
                        cp.JSProperties["cp_locmessage"] = message;

                    }
                    else
                    {
                        cp.JSProperties["cp_storagetypedone"] = true;
                        cp.JSProperties["cp_locmessage"] = "Successfully Updated!";
                        int i = count;
                        count++;

                    }



                    break;
                #endregion

                #region Generate Countsheet Subsi
                case "RGeneratePickData":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        strError = GearsWarehouseManagement.GWarehouseManagement.Out_GeneratePickData(gp);

                        if (strError == "")
                        {
                            int i = count;
                            count++;
                        }
                        else
                        {
                            strError2 += "\r\n" + item + ": " + strError;
                        }
                    }
                    cp.JSProperties["cp_submit"] = true;
                    cp.JSProperties["cp_submitalert"] = "Generate: " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    break;
                #endregion

                //end

                #region Generate Billing Info and Auto Bill 01/18/2017	GC

                case "RGenerateBillingInfo":
                    cp.JSProperties["cp_genbillinginfo"] = true;
                    break;

                case "RGenerateBillingInfoExec":

                    DataTable BillingInfo = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code = 'ACC_BILINFO'", Session["ConnString"].ToString());
                    foreach (DataRow dt in BillingInfo.Rows)
                    {
                        value = dt["value"].ToString();
                    }


                    if (Request.QueryString["access"].Contains(value))
                    {
                        if (fieldValues == null) return;
                        foreach (var item in fieldValues)
                        {
                            gp._DocNo = item.ToString();
                            gp._UserId = Session["userid"].ToString();

                            strError = GWarehouseManagement.GenerateBillingInfo(gp);


                            if (strError.Replace("\r\n", "") == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Generated Biling Info : " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Sufficient Access to do this action.";
                    }
                    break;

                #endregion

                //VINZ

                #region Include Exclude Billing

                case "RIncludeBilling":
                    Session["IncExc"] = "-1";
                    cp.JSProperties["cp_onIncExc"] = true;
                    break;

                case "RExcludeBilling":
                    Session["IncExc"] = "1";
                    cp.JSProperties["cp_onIncExc"] = true;
                    break;

                case "RExcludeBillingInitiate":
                    DataTable IncExec = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code = 'ACC_IXBILL'", Session["ConnString"].ToString());
                    foreach (DataRow dt in IncExec.Rows)
                    {
                        value = dt["Value"].ToString();
                    }

                    if (Request.QueryString["access"].Contains(value))
                    {
                        strError = "";
                        strError2 = "";
                        if (fieldValues == null) return;

                        foreach (var item in fieldValues)
                        {
                            gp._DocNo = item.ToString();
                            gp._UserId = Session["userid"].ToString();
                            gp._TransType = "WMSTRN";
                            gp._Connection = Session["ConnString"].ToString();
                            gp._Factor = Convert.ToInt32(Session["IncExc"].ToString());
                            //strError = GearsInventory.GInventory.PhysicalCount_IsFinal(gp);
                            strError = GearsWarehouseManagement.GWarehouseManagement.Exclude_Billing(gp);

                            if (strError == "")
                            {
                                int i = count;
                                count++;
                            }
                            else
                            {
                                strError2 += "\r\n" + item + ": " + strError;
                            }
                        }
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "Updated : " + Convert.ToString(count) + " of " + Convert.ToString(fieldValues.Count) + "\r\n\r\n" + strError2;
                    }
                    else
                    {
                        cp.JSProperties["cp_submit"] = true;
                        cp.JSProperties["cp_submitalert"] = "No Access to do this action.";
                    }
                    break;



                #endregion

                //ribbon for Truck Arrival
                #region Truck Arrival
                case "RTruckArrival":
                    cp.JSProperties["cp_TruckArrival"] = true;
                    break;
                case "RTruckArrivalSubmit":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();
                        DataTable TruckArrivalMsg = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction] 'A', 'TA', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "'", Session["ConnString"].ToString());

                        if (TruckArrivalMsg.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in TruckArrivalMsg.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "Truck Arrival Successful";
                            int i = count;
                            count++;
                        }
                        break;
                    }
                    cp.JSProperties["cp_Truck"] = true;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                //ribbon for Start RR Process
                #region Start RR Process
                case "RRRStart":
                    cp.JSProperties["cp_RRStart"] = true;
                    break;
                case "RRRStartProcess":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();

                        DataTable StartProcess = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction] 'SP', 'TSP', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "' ", Session["ConnString"].ToString());

                        if (StartProcess.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in StartProcess.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "RR Start Processing";
                            int i = count;
                            count++;
                        }
                        break;

                    }
                    cp.JSProperties["cp_RRStartProcess"] = true;
                    //SubmitAlert.Text = strError2;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                //ribbon for End RR Process
                #region End RR Process
                case "RRREnd":
                    cp.JSProperties["cp_RREnd"] = true;
                    break;
                case "RRREndProcess":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();

                        DataTable EndProcess = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction] 'EP', 'TEP', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "' ", Session["ConnString"].ToString());

                        if (EndProcess.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in EndProcess.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "RR End Processing";
                            int i = count;
                            count++;
                        }
                        break;
                    }
                    cp.JSProperties["cp_RREndProcess"] = true;
                    //SubmitAlert.Text = strError2;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                #region ribbon Departure
                case "RDepartureInbound":
                    cp.JSProperties["cp_departureInbound"] = true;
                    break;
                case "RDepartureProcessInbound":
                    strError = "";
                    strError2 = "";
                    //string WRSP = WRStart.Text;

                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();

                        DataTable EndProcess = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction] 'DEP', 'TDEP', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "' ", Session["ConnString"].ToString());

                        if (EndProcess.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in EndProcess.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "Departure Successful";
                            int i = count;
                            count++;
                        }
                        break;

                    }
                    cp.JSProperties["cp_RDepartureProcessInbound"] = true;
                    //SubmitAlert.Text = strError2;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                #region ribbon Start WR Process
                case "RWRStart":
                    cp.JSProperties["cp_wrstart"] = true;
                    break;
                case "RWRStartProcess":
                    strError = "";
                    strError2 = "";
                    //string WRSP = WRStart.Text;

                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();

                        DataTable StartProcess = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction_Outbound] 'SP', 'TSP', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "' ", Session["ConnString"].ToString());

                        if (StartProcess.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in StartProcess.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "WR Start Processing";
                            int i = count;
                            count++;
                        }
                        break;

                    }
                    cp.JSProperties["cp_RWRStartProcess"] = true;
                    //SubmitAlert.Text = strError2;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                #region ribbon End WR Process
                case "RWREnd":
                    cp.JSProperties["cp_wrend"] = true;
                    break;
                case "RWREndProcess":
                    strError = "";
                    strError2 = "";
                    //string WRSP = WRStart.Text;

                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();

                        DataTable EndProcess = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction_Outbound] 'EP', 'TEP', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "' ", Session["ConnString"].ToString());

                        if (EndProcess.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in EndProcess.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "WR End Processing";
                            int i = count;
                            count++;
                        }
                        break;
                    }
                    cp.JSProperties["cp_RWREndProcess"] = true;
                    //SubmitAlert.Text = strError2;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                #region ribbon Departure
                case "RDeparture":
                    cp.JSProperties["cp_departure"] = true;
                    break;
                case "RDepartureProcess":
                    strError = "";
                    strError2 = "";
                    //string WRSP = WRStart.Text;

                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();

                        DataTable EndProcess = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction_Outbound] 'DEP', 'TDEP', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "' ", Session["ConnString"].ToString());

                        if (EndProcess.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in EndProcess.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "Departure Successful";
                            int i = count;
                            count++;
                        }
                        break;

                    }
                    cp.JSProperties["cp_RDepartureProcess"] = true;
                    //SubmitAlert.Text = strError2;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                //ribbon for Truck Arrival
                #region Truck Arrival
                case "RTruckArrivalOutbound":
                    cp.JSProperties["cp_TruckArrivalOutbound"] = true;
                    break;
                case "RTruckArrivalOutboundSubmit":
                    strError = "";
                    strError2 = "";

                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        gp._DocNo = item;
                        gp._UserId = Session["userid"].ToString();
                        gp._TransType = Request.QueryString["transtype"].ToString();
                        DataTable TruckArrivalMsg = Gears.RetriveData2("exec [dbo].[sp_Truck_Trasaction_Outbound] 'A', 'TA', '" + gp._DocNo + "', '" + HttpContext.Current.Session["userid"].ToString() + "'", Session["ConnString"].ToString());

                        if (TruckArrivalMsg.Rows.Count > 1)
                        {
                            foreach (DataRow dt1 in TruckArrivalMsg.Rows)
                            {
                                strError2 += "\r\n" + dt1["Msg"].ToString();
                            }
                        }
                        else
                        {
                            strError2 = "Truck Arrival Successful";
                            int i = count;
                            count++;
                        }
                        break;
                    }
                    cp.JSProperties["cp_TruckArrivalOutboundSubmit"] = true;
                    //SubmitAlert.Text = strError2;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;
                #endregion

                //ribbon for Return
                #region Return
                case "RReturn":
                    //sets the value of cp_afterblast to in the translist.aspx
                    cp.JSProperties["cp_Return"] = true;
                    break;
                case "RReturnValidate":
                    strError = "";
                    strError2 = "";
                    if (fieldValues == null) return;

                    foreach (string item in fieldValues)
                    {
                        //gets the docnumber for selected docnumber in the list
                        gp._DocNo = item;
                        //select the docnumber with the column of submitted by and Return
                        DataTable OutboundStatusSub = Gears.RetriveData2("SELECT DocNumber + ' is already Return' FROM WMS.OutboundDetail WHERE DocNumber = '" + gp._DocNo + "' AND ItemReturn = 1 UNION ALL " +
                                                                         "SELECT DocNumber + ' is already Submitted' FROM WMS.Outbound WHERE docnumber = '" + gp._DocNo + "' AND ISNULL(SubmittedBy, '') != '' ", Session["ConnString"].ToString());

                        if (OutboundStatusSub.Rows.Count > 0)
                        {
                            //select the docnumber when the submitted by column is null then the error message will show
                            strError2 = OutboundStatusSub.Rows[0][0].ToString();

                            //sets the value of cp_AfterBlastValidate and shows the submittedby error to in the translist.aspx
                            strError2 = "\n\n\n\nThe Docnumber " + strError2;
                        }
                        else
                        {
                            strError2 = "";
                            if (strError2 == "")
                            {
                                int i = count;
                                count++;
                            }
                            //sets the value of cp_ShowAB as true and shows the content of AfterBlast modal
                            cp.JSProperties["cp_ShowReturn"] = true;
                        }

                    }
                    cp.JSProperties["cp_ReturnValidate"] = true;
                    cp.JSProperties["cp_submitalert"] = strError2;
                    break;

                    #endregion
            }
            cp.JSProperties["cp_countsubmited"] = Convert.ToString(count);//Martzon added this
        }
    }
}
