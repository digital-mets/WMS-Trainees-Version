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
    // 05 - 23 - 2016 LGE Add Page Header Band.
    public partial class R_StockMovement : DevExpress.XtraReports.UI.XtraReport
    {
        public R_StockMovement()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //DataTable getLogo = Gears.RetriveData2("SELECT Field1 AS Logo FROM IT.SystemSettings WHERE Code='COMPLOGO'", HttpContext.Current.Session["ConnString"].ToString());

            //byte[] str = Convert.FromBase64String(getLogo.Rows[0]["Logo"].ToString() as string);
            //MemoryStream ms = new MemoryStream(str);
            //ms.Seek(0, SeekOrigin.Begin);
            //Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            //((XRPictureBox)sender).Image = bmp;
            ////xrPictureBox2.Image = bmp;
            //ms.Close();
        }

    }
}
