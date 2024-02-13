using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Data;
using GearsLibrary;
using System.IO;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_RemainingInvent_Comp : DevExpress.XtraReports.UI.XtraReport
    {
        public R_RemainingInvent_Comp()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            if (HttpContext.Current.Session["CustomerCode"] != null)
                Customer.Value = HttpContext.Current.Session["CustomerCode"].ToString();
        }

        private void xrLabel17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        //    XtraReport report = (XtraReport)Report;

        //    DataTable getLogo = Gears.RetriveData2("SELECT Field1 AS Logo FROM IT.SystemSettings WHERE Code='COMPLOGO'", HttpContext.Current.Session["ConnString"].ToString());

        //    byte[] str = Convert.FromBase64String(getLogo.Rows[0]["Logo"].ToString() as string);
        //    MemoryStream ms = new MemoryStream(str);
        //    ms.Seek(0, SeekOrigin.Begin);
        //    Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
        //    ((XRPictureBox)sender).Image = bmp;
        //    //xrPictureBox2.Image = bmp;
        //    ms.Close();
        //
        }

        private void R_RemainingInvent_Comp_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            report.Parameters["PostBack"].Value = false;
        }
    }
}
