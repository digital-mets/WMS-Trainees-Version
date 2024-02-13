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
    public partial class R_BillingSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_BillingSummary()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            XtraReport report = (XtraReport)Report;
            //ADDED BY ERA
            string startmonth = "";
            DataTable getmonth = Gears.RetriveData2("select value from it.SystemSettings where code ='CMONTH'", HttpContext.Current.Session["ConnString"].ToString());
            foreach (DataRow dtRow in getmonth.Rows)
            {
                startmonth = dtRow[0].ToString();
            }
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime answer = firstDay.AddMonths(Convert.ToInt16(startmonth.ToString()));
            DateTime today = DateTime.Now;

            report.Parameters["DateFrom"].Value = answer.ToShortDateString();
            report.Parameters["DateTo"].Value = today.ToShortDateString();


        }

        private void R_BillingSummary_DataSourceDemanded(object sender, EventArgs e)
        {
            
        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            DataTable dtfullname = Gears.RetriveData2("Select Value from it.SystemSettings where Code='COMPIMAGE'", sqlDataSource1.Connection.ConnectionString);
            byte[] str = Convert.FromBase64String(dtfullname.Rows[0][0].ToString() as string);
            MemoryStream ms = new MemoryStream(str);
            ms.Seek(0, SeekOrigin.Begin);
            Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            ((XRPictureBox)sender).Image = bmp;
            ms.Close();


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
