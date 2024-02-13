using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.Web;
using DevExpress.XtraReports.UI;
using System.Data;
using GearsLibrary;

using DevExpress.Web;

namespace GWL.WebReports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        string reportName;

        #region Audit Trail
        //-- GC 02/12/2016  Added code for IsPrinted tagging
        #endregion

        private XtraReport GetReportByName(string reportName)
        {            
            Type CAType = Type.GetType("GWL.WebReports." + reportName);
            XtraReport report = (XtraReport)Activator.CreateInstance(CAType);
            
            if (Request.QueryString["transtype"] != "PRDJOB3" && Request.QueryString["transtype"] != "PRDJOB4"
                && Request.QueryString["transtype"] != "ACTCOS" && Request.QueryString["transtype"] != "SLSDRC2")
            {
                //reportName = "GWL.WebReports." + rep[1] + "." + rep[2];

                try
                {
                    if (report.Parameters["DocNumber"].MultiValue == false)
                    {
                        string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');
                        report.Parameters["DocNumber"].Value = docnum[0];
                    }
                    else
                    {
                        string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');
                        report.Parameters["DocNumber"].Value = docnum;
                    }
                    report.Parameters["UserID"].Value = Session["userid"].ToString();
                    //Genrev Added code 02/12/2016
                    report.Parameters["IsPrinted"].Value = Request.QueryString["tag"].ToString();
                    //end
                }
                catch
                {

                }
            }
            else if (Request.QueryString["transtype"] == "PRDJOB3")
            {
                string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');

                string realdocnum = docnum[0].Split('|')[0];
                string step = docnum[0].Split('|')[1];

                report.Parameters["DocNumber"].Value = realdocnum;
                report.Parameters["Step"].Value = step;
                report.Parameters["UserID"].Value = Session["userid"].ToString();
                report.Parameters["IsPrinted"].Value = Request.QueryString["tag"].ToString();
            }
            else if (Request.QueryString["transtype"] == "PRDJOB4")
            {
                string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');

                string realdocnum = docnum[0].Split('|')[0];
                string workcntr = docnum[0].Split('|')[1];

                report.Parameters["DocNumber"].Value = realdocnum;
                report.Parameters["WorkCenter"].Value = workcntr;
                report.Parameters["UserID"].Value = Session["userid"].ToString();
                report.Parameters["IsPrinted"].Value = Request.QueryString["tag"].ToString();
            }
            else if (Request.QueryString["transtype"] == "ACTCOS")
            {
                string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');

                report.Parameters["DocNumber"].Value = docnum[0];
            }
            else if (Request.QueryString["transtype"] == "SLSDRC2")
            {
                string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');

                report.Parameters["DocNumber"].Value = docnum[0];
            }
            return report;

        }
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

            if (referer.Contains("param'"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["val"]) && !string.IsNullOrEmpty(Request.QueryString["param"]))
                {
                    string rep = Request.QueryString["val"].ToString();
                    string param = Request.QueryString["param"].ToString();
                    string[] report = rep.Split('~');
                    if (report[1] != "" && param == "PView")
                    {
                        reportName = report[1];
                        GWLReportViewer.Report = GetReportByName(reportName);
                    }
                    else if (rep == "~" || param != "PView")
                    {
                        Response.Redirect("./Blank.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Blank.aspx");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["val"]))
                {
                    string rep = Request.QueryString["val"].ToString();
                    string[] report = rep.Split('~');
                    if (report[1] != "")
                    {
                        reportName = report[1];
                        GWLReportViewer.Report = GetReportByName(reportName);
                    }
                    else if (rep == "~")
                    {
                        Response.Redirect("./Blank.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Blank.aspx");
                }
            }

            GWLReportViewer.ToolbarItems.Add(new ReportToolbarButton
            {
                Enabled = true,
                IconID = "print_printer_16x16",
                Name = "Print",
                Text = ""
            });

            if (Request.QueryString["transtype"] != null)
            {
                string rep = Request.QueryString["val"].ToString();
                string[] report = rep.Split('~');

                DataTable dtreprint = Gears.RetriveData2("Select Reprint from masterfile.FormLayout where FormName = '" + Request.QueryString["transtype"].ToString() + "' " +
                                      "and isnull(Reprint,0)=1 and ReportName = '" + report[1] + "'", Session["ConnString"].ToString());
                if (dtreprint.Rows.Count == 1)
                {
                    string tablename = "";
                    DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where transtype = '" + Request.QueryString["transtype"].ToString() + "'"
                        , Session["ConnString"].ToString());
                    foreach (DataRow dtRow in menutable.Rows)
                    {
                        tablename = dtRow["TableName"].ToString();
                    }

                    //--FUNCTON TO CHECK WHETHER THE PRINT FORM IS FOR MULTIPLE LOOKUP
                    DataTable dtreprint2 = Gears.RetriveData2("Select Reprint from masterfile.FormLayout where FormName = '" + Request.QueryString["transtype"].ToString() + "' " +
                                      "and isnull(Reprint,0)=1 and Text = 'MULTI' and ReportName = '" + report[1] + "'", Session["ConnString"].ToString());
                    string Holder = "";//
                    if (dtreprint2.Rows.Count == 1)
                    {
                        string[] paramValues = GWLReportViewer.Report.Parameters["DocNumber"].Value as string[];
                        //xrLabel1.Text = string.Empty;
                        for (int i = 0; i < paramValues.Length; i++)
                        {
                            Holder += paramValues[i].ToString();
                            if (i < paramValues.Length - 1)
                                Holder += ",";
                        }
                    }
                    else
                    {
                        Holder = GWLReportViewer.Report.Parameters["DocNumber"].Value.ToString();
                    }

                    //DataTable dtcheckprint = Gears.RetriveData2("exec sp_CheckTablePrint '" + tablename + "','" + Holder + "'");
                    
                    GWLReportViewer.ClientSideEvents.ToolbarItemClick =
                    "function (s,e){ " +
                    "if(e.item.name == 'NewTab'){ " +
                    "window.open(window.location.href, '_blank');} " +
                    "if(e.item.name == 'Print'){" +
                    "var r = confirm('Are you sure that you want to proceed with printing? " +
                    "This will be marked as Re-Printed after this process');" +
                    "if (r == true) { s.Print(); }}}";

                    if (!IsCallback)
                    {
                        if (Request.Params["__EVENTARGUMENT"] != null)
                        {
                            if (Request.Params["__EVENTARGUMENT"].Contains("showPrintDialog:true;"))
                            {
                                
                                string DocNum = Request.QueryString["docnumber"];
                                DataTable check = Gears.RetriveData2("exec sp_CheckColumn '"+DocNum+"','"+tablename+"','1'"
                                  , Session["ConnString"].ToString());

                                if (check.Rows.Count > 0)
                                {
                                    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
                                    gparam._DocNo = DocNum;
                                    gparam._Table = tablename;
                                    gparam._Factor = 1;
                                    gparam._Connection = Session["ConnString"].ToString();
                                    string strresult = GearsCommon.GCommon.IsPrinted_Tag(gparam);
                                }
                            }
                        }
                    }
                }
                else
                {
                    GWLReportViewer.ClientSideEvents.ToolbarItemClick =
                    "function (s,e){ " +
                    "if(e.item.name == 'NewTab'){ " +
                    "window.open(window.location.href, '_blank');} "+
                    "if(e.item.name == 'Print'){ s.Print();}" +
                    "}";
                }
            }
            else
            {
                GWLReportViewer.ClientSideEvents.ToolbarItemClick =
                     "function (s,e){ " +
                     "if(e.item.name == 'NewTab'){ " +
                     "window.open(window.location.href, '_blank');} " +
                     "if(e.item.name == 'Print'){ s.Print();}" +
                     "}";
            }

            //ASPxDocumentViewer1.Report = new XtraReport();
        }

        protected void ASPxDocumentViewer1_CustomizeParameterEditors(object sender, CustomizeParameterEditorsEventArgs e)
        {
            string name = e.Parameter.Name;
            if (name.Contains("Date") && e.Parameter.Type.Name == "String")
            {
                ASPxDateEdit dateEdit = new ASPxDateEdit();
                e.Editor = dateEdit;
            }
        }
    }
}