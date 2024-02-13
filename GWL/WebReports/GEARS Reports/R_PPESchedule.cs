using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Data;
using GearsLibrary;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_PPESchedule : DevExpress.XtraReports.UI.XtraReport
    {
        public R_PPESchedule()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;

        }



        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_APAgingSummary_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
        //    XtraReport report = (XtraReport)Report;
        //    DataTable getAP = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'APACCT'");

        //    string[] APCode = { getAP.Rows[0]["Value"].ToString() };
        //    report.Parameters["GLAPAccount"].Value = APCode;

            XtraReport report = (XtraReport)Report;

            //string Data = "AccountablePerson,CostCenterCode,Department,Location";
            //string[] Infos = Data.Split(',');

            //report.Parameters["ShowInformation"].Value = Infos;



        }

        private void R_APAgingSummary_DataSourceDemanded(object sender, EventArgs e)
        {

            XtraReport report = (XtraReport)Report;
            
            
            string month = "";
            string glcode = "";
            string accountableperson = "";
            string department = "";
            string costcenter = "";
            string summarybyparent = "";
            string location = "";


            Header1.Visible = false;
            Header2.Visible = false;
            Header3.Visible = false;
            Header4.Visible = false;
            Header5.Visible = false;

            Group1.Visible = false;
            Group2.Visible = false;
            Group3.Visible = false;
            Group4.Visible = false;
            Group5.Visible = false;

            GroupF1.Visible = false;
            GroupF2.Visible = false;
            GroupF3.Visible = false;
            GroupF4.Visible = false;
            GroupF5.Visible = false;


            Detail1.Visible = false;
            Detail2.Visible = false;
            Detail3.Visible = false;
            Detail4.Visible = false;
            Detail5.Visible = false;
            


            //GL Account Code
            string[] paramValues = report.Parameters["GLCode"].Value as string[];
            //GLCodeCell.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                glcode += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    glcode += ",";
            }


            //Accountable Person
            string[] AccountablePersonValues = report.Parameters["AccountablePerson"].Value as string[];
            //.Text = string.Empty;
            for (int i = 0; i < AccountablePersonValues.Length; i++)
            {
                accountableperson += AccountablePersonValues[i].ToString();
                if (i < AccountablePersonValues.Length - 1)
                    accountableperson += ",";
            }


            //Cost Center Code
            string[] CostCenterValues = report.Parameters["CostCenterCode"].Value as string[];
            //CostCenterCodeCell.Text = string.Empty;
            for (int i = 0; i < CostCenterValues.Length; i++)
            {
                costcenter += CostCenterValues[i].ToString();
                if (i < CostCenterValues.Length - 1)
                    costcenter += ",";
            }

            //Department
            string[] DepartmentValues = report.Parameters["Department"].Value as string[];
            //DepartmentCell.Text = string.Empty;
            for (int i = 0; i < DepartmentValues.Length; i++)
            {
                department += DepartmentValues[i].ToString();
                if (i < DepartmentValues.Length - 1)
                    department += ",";
            }

            //Location
            string[] LocationValues = report.Parameters["Location"].Value as string[];
            //LocationCell.Text = string.Empty;
            for (int i = 0; i < LocationValues.Length; i++)
            {
                location += LocationValues[i].ToString();
                if (i < LocationValues.Length - 1)
                    location += ",";
            }


            //Passing Labels To Parameter Value
            if (report.Parameters["GLCode"].Value.ToString() == "System.String[]")
            {
                report.Parameters["GLCode"].Value = glcode;
            }

            if (report.Parameters["AccountablePerson"].Value.ToString() == "System.String[]")
            {
                report.Parameters["AccountablePerson"].Value = accountableperson;
            }

            if (report.Parameters["CostCenterCode"].Value.ToString() == "System.String[]")
            {
                report.Parameters["CostCenterCode"].Value = costcenter;
            }

            if (report.Parameters["Department"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Department"].Value = department;
            }

            if (report.Parameters["Location"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Location"].Value = location;
            }

            
            string x = report.Parameters["Month"].Value.ToString();
            switch (x)
            {
                case "1":
                    month = "January";
                    break;
                case "2":
                    month = "February";
                    break;
                case "3":
                    month = "March";
                    break;
                case "4":
                    month = "April";
                    break;
                case "5":
                    month = "May";
                    break;
                case "6":
                    month = "June";
                    break;
                case "7":
                    month = "July";
                    break;
                case "8":
                    month = "August";
                    break;
                case "9":
                    month = "September";
                    break;
                case "10":
                    month = "October";
                    break;
                case "11":
                    month = "November";
                    break;
                default:
                    month = "December";
                    break;
            }

            //xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessAccount:  " + businessaccount + " / Customer:  " + customer + " / Salesman:  " + salesman + " / GLARAccount:  " + glaraccount + " / SummaryBy:  [Parameters.SummaryBy]";
            xrLabel1.Text = "Month: " + month + " / Year: [Parameters.Year] / GLCode: " + glcode + " / Location: [Parameters.Location] / AccountablePerson: " + accountableperson + " / Department: " + department + " / CostCenter: " + costcenter + " / SortBy: [Parameters.SortBy] / SummaryByParent: [Parameters.SummaryByParent]";


            string[] data = report.Parameters["ShowInformation"].Value as string[];
            string holder = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                holder += data[i].ToString();
                if (i < data.Length - 1)
                    holder += ",";
            }
            bool[] truefalse = new bool[6];
            string[] Name = new string[6];
            string condition = "false";
            int position = 0;
            int length = 0;
            if (data == null || data.Length == 0)
            {
                position = 4 * 30;
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == "Summary")
                    {
                        length = data.Length - 1;
                    }
                }

                if (length != 0)
                    position = (4 - length) * 30;
                else
                {
                    if (data.Length == 1 && data[0] == "Summary")
                        length = 0;
                    else
                        length = data.Length;
                    position = (4 - length) * 30;
                }
            }

            this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(0F + position, 0F);
            this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001033147F + position, 0F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F + position , 0F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F + position, 0F);


            if (condition == "false")
            {
                for (int i = 0; i < data.Length; i++)
                {

                    if (data[i] == "AccountablePerson")
                    {
                        truefalse[i] = true;
                        Name[i] = "AccountablePerson";
                    }

                    else if (data[i] == "CostCenterCode")
                    {
                        truefalse[i] = true;
                        Name[i] = "CostCenter";
                    }

                    else if (data[i] == "Department")
                    {
                        truefalse[i] = true;
                        Name[i] = "Department";
                    }

                    else if (data[i] == "Location")
                    {
                        truefalse[i] = true;
                        Name[i] = "Location";
                    }
                }
                //TRY LANG
                //if(paramValues.Length > 0)
                //{
                for (int i = 0; i < length; i++)
                {
                    string NameHolder = "";
                    bool truefalseHolder;
                    if (Name[i] == null)
                    {
                        NameHolder = Name[i + 1];
                        Name[i + 1] = Name[i];
                        Name[i] = NameHolder;

                        truefalseHolder = truefalse[i + 1];
                        truefalse[i + 1] = truefalse[i];
                        truefalse[i] = truefalseHolder;
                    }
                }

                truefalse[length] = true;
                Name[length] = "Status";

                //-------
                for (int i = 0; i < 5; i++)
                {
                    switch (i)
                    {
                        case 0:	// Header1
                            Header1.Visible = truefalse[i];
                            Detail1.Visible = truefalse[i];
                            Group1.Visible = truefalse[i];
                            GroupF1.Visible = truefalse[i];
                            if (Name[i] == "AccountablePerson")
                            {
                                Header1.Text = "Acc.Person";
                            }
                            else
                            {
                                Header1.Text = Name[i];
                            }

                            this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_PPE."+Name[i])});
                            break;
                        case 1:	// Header2
                            Header2.Visible = truefalse[i];
                            Header2.Text = Name[i];
                            Detail2.Visible = truefalse[i];
                            Group2.Visible = truefalse[i];
                            GroupF2.Visible = truefalse[i];
                            this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_PPE."+Name[i])});
                            break;
                        case 2:	// Header3
                            Header3.Visible = truefalse[i];
                            Header3.Text = Name[i];
                            Detail3.Visible = truefalse[i];
                            Group3.Visible = truefalse[i];
                            GroupF3.Visible = truefalse[i];
                            this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_PPE."+Name[i])});
                            break;
                        case 3:
                            Header4.Visible = truefalse[i];
                            Header4.Text = Name[i];
                            Detail4.Visible = truefalse[i];
                            Group4.Visible = truefalse[i];
                            GroupF4.Visible = truefalse[i];

                            this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_PPE."+Name[i])});
                            break;

                        case 4:
                            Header5.Visible = truefalse[i];
                            Header5.Text = Name[i];
                            Detail5.Visible = truefalse[i];
                            Group5.Visible = truefalse[i];
                            GroupF5.Visible = truefalse[i];

                            this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_PPE."+Name[i])});
                            break;
                        default:	// Header7
                            Header5.Visible = truefalse[i];
                            Header5.Text = Name[i];
                            Detail5.Visible = truefalse[i];
                            Group5.Visible = truefalse[i];
                            GroupF5.Visible = truefalse[i];

                            this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_PPE."+Name[i])});
                            break;
                    }
                }
            } // End Of Else condition == false
        }

        private void Group1_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];
            
            
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == "Summary")
                    {
                        length = data.Length - 1;
                    }
                }

                if(length == 0)
                    e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
           
        }

        private void GroupF1_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "Summary")
                {
                    length = data.Length - 1;
                }
            }

            if (length == 0)
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void Group2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "Summary")
                {
                    length = data.Length - 1;
                }
            }

            if (length == 1)
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void GroupF2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "Summary")
                {
                    length = data.Length - 1;
                }
            }

            if (length == 1)
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void Group3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "Summary")
                {
                    length = data.Length - 1;
                }
            }

            if (length == 2)
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void GroupF3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "Summary")
                {
                    length = data.Length - 1;
                }
            }

            if (length == 2)
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void Group4_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "Summary")
                {
                    length = data.Length - 1;
                }
            }

            if (length == 3)
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void GroupF4_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            int length = 0;
            string[] data = report.Parameters["ShowInformation"].Value as string[];


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "Summary")
                {
                    length = data.Length - 1;
                }
            }

            if (length == 3)
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }


    }
}
