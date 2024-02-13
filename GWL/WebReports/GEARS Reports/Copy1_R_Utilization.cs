using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.IO;
using GearsLibrary;
using System.Data;
using System.Security;
using Microsoft.SqlServer.Server;
using DevExpress.DataAccess.Sql;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_Utilization : DevExpress.XtraReports.UI.XtraReport
    {
        public R_Utilization()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            XtraReport report = (XtraReport)Report;
            CustomSqlQuery query = sqlDataSource1.Queries[2] as CustomSqlQuery;

            DataTable dtwh = Gears.RetriveData2("SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description,Address FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + HttpContext.Current.Session["WHCode"].ToString() + "') >0", HttpContext.Current.Session["ConnString"].ToString());
            if (dtwh.Rows.Count ==1)
            {
                query.Sql = "SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + HttpContext.Current.Session["WHCode"].ToString() + "') >0  ";
                sqlDataSource1.Fill();
                report.Parameters["WarehouseCode"].Value = dtwh.Rows[0][0].ToString();
                xrLabel18.Text = dtwh.Rows[0]["Address"].ToString();

            }
            else
            {
                query.Sql = "SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + HttpContext.Current.Session["WHCode"].ToString() + "') >0  UNION ALL SELECT 'ALL' AS WarehouseCode, 'ALL' AS Description ORDER BY WarehouseCode  ";
                sqlDataSource1.Fill();
            }

         
     

        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void xrPictureBox2_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtfullname = Gears.RetriveData2("Select Value from it.SystemSettings where Code='COMPIMAGE'", sqlDataSource1.Connection.ConnectionString);
            byte[] str = Convert.FromBase64String(dtfullname.Rows[0][0].ToString() as string);
            MemoryStream ms = new MemoryStream(str);
            ms.Seek(0, SeekOrigin.Begin);
            Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            ((XRPictureBox)sender).Image = bmp;
            ms.Close();
        }

        private void R_Utilization_DataSourceDemanded(object sender, EventArgs e)
        {
            CustomSqlQuery query = sqlDataSource1.Queries["Warehouse"] as CustomSqlQuery;
            query.Sql = "select 'test' AS WarehouseCode, 'test1' as Description";
            sqlDataSource1.Fill();


        }

        private void R_Utilization_DataSourceDemanded_1(object sender, EventArgs e)
        {

            XtraReport report = (XtraReport)Report;
            string plantcode = "";

            string[] Plant = report.Parameters["PlantCode"].Value as string[];
            //.Text = string.Empty;
            for (int i = 0; i < Plant.Length; i++)
            {
                plantcode += Plant[i].ToString();
                if (i < Plant.Length - 1)
                    plantcode += ",";
            }

            if (report.Parameters["PlantCode"].Value.ToString() == "System.String[]")
            {
                report.Parameters["PlantCode"].Value = plantcode;
            }


        }

    }
}
