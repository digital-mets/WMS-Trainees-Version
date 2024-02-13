using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using System.Data;
using GearsLibrary;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_IssuanceSlipPackaging : DevExpress.XtraReports.UI.XtraReport
    {
        public P_IssuanceSlipPackaging()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            XtraReport report = (XtraReport)Report;

            //ADDED BY TA
            //string startmonth = "";
            //DataTable getmonth = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='CMONTH'", HttpContext.Current.Session["ConnString"].ToString());
            //foreach (DataRow dtRow in getmonth.Rows)
            //{
            //    startmonth = dtRow[0].ToString();
            //}
            //int year = DateTime.Now.Year;
            //DateTime firstDay = new DateTime(year, 1, 1);
            //DateTime answer = firstDay.AddMonths(Convert.ToInt16(startmonth.ToString()));
            //DateTime today = DateTime.Now;

            //report.Parameters["DateFrom"].Value = answer.ToShortDateString();
            //report.Parameters["DateTo"].Value = today.ToShortDateString();
        }

    }
}
