using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_WMS_DRSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_DRSummary()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            DateTime date = new DateTime();
            DRDateFrom.Value = new DateTime(date.Year, date.Month, 1);
            DRDateTo.Value = DateTime.Now;
            //SetupParam();
        }

        //private void SetupParam()
        //{
        //    // Create a parameter and specify its name.
        //    Parameter param1 = new Parameter();
        //    param1.Name = "SISCO";

        //    // Specify other parameter properties.
        //    param1.Type = typeof(System.String);
        //    param1.MultiValue = true;
        //    param1.Description = "Clients: ";

        //    DynamicListLookUpSettings lookupSettings = new DynamicListLookUpSettings();
        //    lookupSettings.DataSource = this.DataSource;
        //    lookupSettings.DataMember = "SisterCompanies";
        //    lookupSettings.DisplayMember = "Name";
        //    lookupSettings.ValueMember = "BizPartnerCode";

        //    param1.LookUpSettings = lookupSettings;
        //    param1.Visible = true;

        //    // Add the parameter to the report.
        //    //this.Parameters.Add(param1);

        //    // Specify the report's filter string.
        //    //this.FilterString = "[Client] In (?SISCO)";

        //    // Create a parameter and specify its name.
        //    Parameter param2 = new Parameter();
        //    param2.Name = "ITEM";

        //    // Specify other parameter properties.
        //    param2.Type = typeof(System.String);
        //    param2.MultiValue = false;
        //    param2.Description = "Item: ";

        //    DynamicListLookUpSettings lookupSettings2 = new DynamicListLookUpSettings();
        //    lookupSettings2.DataSource = this.DataSource;
        //    lookupSettings2.DataMember = "Item";
        //    lookupSettings2.DisplayMember = "ItemCode";
        //    lookupSettings2.ValueMember = "ItemCode";

        //    param2.LookUpSettings = lookupSettings2;
        //    param2.Visible = true;

        //    // Add the parameter to the report.
        //    //this.Parameters.Add(param2);

        //    //// Specify the report's filter string.
        //    //this.FilterString = "[ItemCode] = ?ITEM";

        //    // Create a parameter and specify its name.
        //    Parameter param3 = new Parameter();
        //    param3.Name = "ITEMDETAIL";

        //    // Specify other parameter properties.
        //    param3.Type = typeof(System.String);
        //    param3.MultiValue = false;
        //    param3.Description = "Size: ";

        //    DynamicListLookUpSettings lookupSettings3 = new DynamicListLookUpSettings();
        //    lookupSettings3.DataSource = this.DataSource;
        //    lookupSettings3.DataMember = "ItemDetail";
        //    lookupSettings3.DisplayMember = "SizeCode";
        //    lookupSettings3.ValueMember = "SizeCode";
        //    lookupSettings3.FilterString = "[ItemCode] = ?ITEM";
        //    param3.LookUpSettings = lookupSettings3;
        //    param3.Visible = true;

        //    // Create a parameter and specify its name.
        //    Parameter param4 = new Parameter();
        //    param4.Name = "ITEMCOLOR";

        //    // Specify other parameter properties.
        //    param4.Type = typeof(System.String);
        //    param4.MultiValue = false;
        //    param4.Description = "Color: ";

        //    DynamicListLookUpSettings lookupSettings4 = new DynamicListLookUpSettings();
        //    lookupSettings4.DataSource = this.DataSource;
        //    lookupSettings4.DataMember = "ItemColor";
        //    lookupSettings4.DisplayMember = "ColorCode";
        //    lookupSettings4.ValueMember = "ColorCode";
        //    lookupSettings4.FilterString = "[ItemCode] = ?ITEM";
        //    param4.LookUpSettings = lookupSettings4;
        //    param4.Visible = true;

        //    // Add the parameter to the report.
        //    this.Parameters.Add(param1);
        //    //this.Parameters.Add(param2);
        //    //this.Parameters.Add(param4);
        //    //this.Parameters.Add(param3);            
        //    this.FilterString = "[Client] In (?SISCO)";
        //}
    }
}
