using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using DevExpress.Printing;
using GearsSales;
using System.Windows.Forms;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_SalesOrder : DevExpress.XtraReports.UI.XtraReport
    {
        public P_SalesOrder()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }        
    }
}
