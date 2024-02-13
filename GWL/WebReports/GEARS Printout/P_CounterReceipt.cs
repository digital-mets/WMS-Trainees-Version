using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using GearsCommon;
using GearsLibrary;
using System.Data;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_CounterReceipt : DevExpress.XtraReports.UI.XtraReport
    {
        public P_CounterReceipt()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void P_JOMaterialIssuance_AfterPrint(object sender, EventArgs e)
        {
            //After();
        }

        #region After Print
        private void After()
        {
            //string DocNum = DocNumber.Value.ToString();
            //DataTable check = new DataTable();
            //check = Gears.RetriveData2("SELECT * FROM Sales.DeliveryReceipt WHERE DocNumber = '" + DocNum + "' AND ISNULL(SubmittedBy,'') != ''");

            //if (check.Rows.Count > 0)
            //{
            //    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //    gparam._DocNo = DocNum;
            //    gparam._Table = "Sales.DeliveryReceipt";
            //    string strresult = GearsCommon.GCommon.IsPrinted_Tag(gparam);
            //}
        }
        #endregion
    }
}
