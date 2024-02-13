using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_WMS_5THCOPYSMVDR : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_5THCOPYSMVDR()
        {
            InitializeComponent();

            //XtraReport report = new XtraReport();
            //string PrintSession = "dotmatrix";
            //if (PrintSession == "dotmatrix")
            //{
            //    int OldWidth = this.PageWidth;
            //    this.Landscape = !this.Landscape;
            //    this.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            //}
            //else if (PrintSession == "normal")
            //{
            //    this.Landscape = this.Landscape;
            //}

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }
    }
}
