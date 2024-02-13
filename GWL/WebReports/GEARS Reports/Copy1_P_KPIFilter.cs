using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Windows.Forms;
namespace GWL.WebReports.GEARS_Reports
{
    public partial class P_KPIFilter : DevExpress.XtraReports.UI.XtraReport
    {
        public P_KPIFilter()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }
        const string sShowDetail = "Show Detail";
        const string sHideDetail = "Hide Detail";

        // Create an array containing IDs of the categories being expanded. 
        ArrayList expandedValues = new ArrayList();
        bool ShouldShowDetail(int catID)
        {
            return expandedValues.Contains(catID);
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           // xrLabel2.Text = DetailReport.DrillDownExpanded ? "Hide Details" : "Show Details";
        }

        private void xrLabel2_PreviewClick(object sender, PreviewMouseEventArgs e)
        {

            ////Detail detailReport = new Detail();

            ////// Obtain the current category's ID and Name from the e.Brick.Value property, 
            ////// which stores an object assigned the label's Tag property. 
            ////detailReport.catId.Value = (int)((Detail)e.Brick.Value).Row["CategoryID"];
            ////detailReport.catName.Value = ((Detail)e.Brick.Value).Row["CategoryName"].ToString();

            //// Show the detail report in a new modal window. 
            //detailReport.ShowPreviewDialog();

            //// Obtain the category's ID stored in the label's Tag property. 
            //int index = (int)e.Brick.Value;

            //// Determine whether the current category's details are shown. 
            //bool showDetail = ShouldShowDetail(index);

            //// Toggle the visibility of the category's details. 
            //if (showDetail)
            //{
            //    expandedValues.Remove(index);
            //}
            //else
            //{
            //    expandedValues.Add(index);
            //}

            //// Apply the changes and create the document. 
            //CreateDocument();
        }

        private void xrLabel2_PreviewMouseMove(object sender, PreviewMouseEventArgs e)
        {
            e.PreviewControl.Cursor = Cursors.Hand;
        }



    }
}
