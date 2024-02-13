using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
   
    public partial class R_TopSalesItem : DevExpress.XtraReports.UI.XtraReport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public R_TopSalesItem()
        {
            InitializeComponent();
            XtraReport report = new XtraReport();
            //string PrintSession = "dotmatrix";
            //if (PrintSession == "dotmatrix")
            //{
            //    int OldWidth = report.PageWidth;
            //    report.Landscape = !report.Landscape;
            //    report.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            //    report.Margins.Left = (report.PageWidth - OldWidth) / 2;
            //    report.Margins.Right = (report.PageWidth - OldWidth) / 2;
            //}
            //else if (PrintSession == "normal")
            //{
            //    report.Landscape = report.Landscape;
            //}

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }


    }
}
