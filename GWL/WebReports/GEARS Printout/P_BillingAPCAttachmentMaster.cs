using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using GearsLibrary;
using System.Data;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_BillingAPCAttachmentMaster : DevExpress.XtraReports.UI.XtraReport
    {
        public P_BillingAPCAttachmentMaster()
        {
            InitializeComponent();
            
            
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((XRSubreport)sender).ReportSource.FilterString = "[ID] = 1";
            //((XRSubreport)sender).ReportSource.ApplyFiltering();
        }

        private void P_BillingAPCAttachmentMaster_DataSourceDemanded(object sender, EventArgs e)
        {
            ProdNum.Text = DocNumber.Value.ToString() + " PRODUCTION: QUANTITY IN PALLET";
            DataTable getBSNum = Gears.RetriveData2("select BillingStatement from wms.Billing " +
            "where DocNumber in " +
            "(select DocNumber from wms.BillingDetail where ProdNum = '" + DocNumber.Value.ToString() + "')", "Data Source=192.168.201.115;Initial Catalog=GWL-MLI;User id=sa;Password=mets123*");
            
            foreach (DataRow dtrow in getBSNum.Rows)
            {
                BSNum.Text = "BS #:"+dtrow[0].ToString();
            }

            xrSubreport3.Visible = true;
            xrSubreport4.Visible = true;
            xrSubreport5.Visible = true;
            xrSubreport2.ReportSource = new P_BillingAPCAttachment2();
            xrSubreport3.ReportSource = new P_BillingAPCAttachment3();
            xrSubreport4.ReportSource = new P_BillingAPCAttachment4();
            xrSubreport5.ReportSource = new P_BillingAPCAttachmentSummary();

            DataTable checkProd = Gears.RetriveData2("exec sp_printout_BillingMagnoliaAPCDryAttachment '" + DocNumber.Value.ToString() + "','1828',2"
                   , "Data Source=192.168.201.115;Initial Catalog=GWL-MLI;User id=sa;Password=mets123*");
            DataTable checkProd2 = Gears.RetriveData2("exec sp_printout_BillingMagnoliaAPCDryAttachment '" + DocNumber.Value.ToString() + "','1828',3"
                  , "Data Source=192.168.201.115;Initial Catalog=GWL-MLI;User id=sa;Password=mets123*");
            DataTable checkProd3 = Gears.RetriveData2("exec sp_printout_BillingMagnoliaAPCDryAttachment '" + DocNumber.Value.ToString() + "','1828',4"
                  , "Data Source=192.168.201.115;Initial Catalog=GWL-MLI;User id=sa;Password=mets123*");
            if (checkProd.Rows.Count == 0)
            {
                xrSubreport2.ReportSource = new P_BillingAPCAttachmentSummary();
                xrSubreport3.Visible = false;
                xrSubreport4.Visible = false;
                xrSubreport5.Visible = false;
            }
            if (checkProd2.Rows.Count == 0)
            {
                xrSubreport3.ReportSource = new P_BillingAPCAttachmentSummary();
                xrSubreport4.Visible = false;
                xrSubreport5.Visible = false;
            }
            if (checkProd3.Rows.Count == 0)
            {
                xrSubreport4.ReportSource = new P_BillingAPCAttachmentSummary();
                xrSubreport5.Visible = false;
            }
        }

        private void P_BillingAPCAttachmentMaster_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

    }
}
