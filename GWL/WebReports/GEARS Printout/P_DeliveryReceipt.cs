﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_DeliveryReceipt : DevExpress.XtraReports.UI.XtraReport
    {
        public P_DeliveryReceipt()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void P_DeliveryReceipt_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string Holder = "";//
            string[] paramValues = report.Parameters["DocNumber"].Value as string[];
            //xrLabel1.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                Holder += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    Holder += ",";
            }

            report.Parameters["DocHolder"].Value = Holder;
        }

    }
}
