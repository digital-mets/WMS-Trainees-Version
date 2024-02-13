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

        private XtraReport GetReportByName(string reportName, Boolean IsMulti)
  
        {
            Type CAType = Type.GetType("GWL.WebReports." + reportName);
            XtraReport report = (XtraReport)Activator.CreateInstance(CAType);

            // 2019-05-10   TL
            if (IsMulti == true)
            {
                if (report.Parameters["DocNumber"].MultiValue == false)
                {
                    if (HttpContext.Current.Session["cp_ForEmail"] == "FM")
                    {
                        XtraReport subreport = report;
                        report = (XtraReport)Activator.CreateInstance(Type.GetType("GWL.WebReports.GEARS_Printout.P_MultiPrintFM"));
                        report.Parameters["ReportName"].Value = "GWL.WebReports." + reportName;
                        report.PaperName = subreport.PaperName;
                        report.PaperKind = subreport.PaperKind;
                        report.PageSize = subreport.PageSize;
                        report.Landscape = subreport.Landscape;
                        report.Margins = subreport.Margins;
                        Session.Remove("cp_ForEmail");
                    }
                    else
                    {
                        XtraReport subreport = report;
                        report = (XtraReport)Activator.CreateInstance(Type.GetType("GWL.WebReports.GEARS_Printout.P_MultiPrint"));
                        report.Parameters["ReportName"].Value = "GWL.WebReports." + reportName;
                        report.PaperName = subreport.PaperName;
                        report.PaperKind = subreport.PaperKind;
                        report.PageSize = subreport.PageSize;
                        report.Landscape = subreport.Landscape;
                        report.Margins = subreport.Margins;
                    }

                }
            }
            // 2019-05-10
            if (Request.QueryString["transtype"] != "PRDJOB3" && Request.QueryString["transtype"] != "PRDJOB4"
                && Request.QueryString["transtype"] != "ACTCOS" && Request.QueryString["transtype"] != "SLSDRC2"
                && Request.QueryString["transtype"] != "CMMSPMC" && Request.QueryString["transtype"] != "REFMAC"
               )
            // && Request.QueryString["transtype"] != "CMMSWO"
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
            else if (Request.QueryString["transtype"] == "CMMSPMC")
            {
                string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');

                report.Parameters["DocNumber"].Value = docnum[0];
            }
            // 08-12-2021 TA Added Print for Checklist
            else if (Request.QueryString["transtype"] == "REFMAC" || Request.QueryString["transtype"] != "CMMSWO")
            {
                string[] docnum = Request.QueryString["docnumber"].ToString().Split(',');

                report.Parameters["DocNumber"].Value = docnum[0];
                report.Parameters["TransType"].Value = Request.QueryString["transtype"].ToString();
                report.Parameters["PMType"].Value = Request.QueryString["pmtype"].ToString();
            }
            return report;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string referer;

            //try
            //{
            //    referer = Request.ServerVariables["http_referer"];
            //}
            //catch
            //{
            //    referer = "";
            //}

            // 2019-04-04   TL
            Boolean IsMulti = (Request.QueryString["multiprint"] != null);


            if (Request.QueryString["param"] != null)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["val"]) && !string.IsNullOrEmpty(Request.QueryString["param"]))
                {
                    string rep = Request.QueryString["val"].ToString();
                    string param = Request.QueryString["param"].ToString();
                    string[] report = rep.Split('~');
                    if (report[1] != "" && param == "PView")
                    {
                        reportName = report[1];
                        GWLReportViewer.OpenReport(GetReportByName(reportName, IsMulti));
                        DataTable dtname = Gears.RetriveData2("Select top 1 Prompt from it.mainmenu where CommandString = '" + reportName + "'"
                         , Session["ConnString"].ToString());
                        if (dtname.Rows.Count > 0)
                        {
                            txtReport.Text = dtname.Rows[0][0].ToString();
                        }

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
                        GWLReportViewer.OpenReport(GetReportByName(reportName, IsMulti));
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

        protected void cp_Callback(object source, CallbackEventArgs e)
        {
            string tablename = "";
            DataTable menutable = Gears.RetriveData2("Select TableName from it.mainmenu where transtype = '" + Request.QueryString["transtype"].ToString() + "'"
                , Session["ConnString"].ToString());
            foreach (DataRow dtRow in menutable.Rows)
            {
                tablename = dtRow["TableName"].ToString();
            }

            string DocNum = Request.QueryString["docnumber"];
            DataTable check = Gears.RetriveData2("exec sp_CheckColumn '" + DocNum + "','" + tablename + "','1'"
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