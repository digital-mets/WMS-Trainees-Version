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
    public partial class P_QR : DevExpress.XtraReports.UI.XtraReport
    {
        public P_QR()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            //xrPictureBox1.ImageUrl = "~/Images/Signature/PA00030.png"; 



        }
    }
}
