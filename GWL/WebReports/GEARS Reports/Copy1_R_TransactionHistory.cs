
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Data;
using GearsLibrary;
using System.IO;
using DevExpress.DataAccess.Sql;
namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_TransactionHistory : DevExpress.XtraReports.UI.XtraReport
    {
        public R_TransactionHistory()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            XtraReport report = (XtraReport)Report;
            CustomSqlQuery query = sqlDataSource1.Queries[4] as CustomSqlQuery;

            DataTable dtwh = Gears.RetriveData2("SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + HttpContext.Current.Session["WHCode"].ToString() + "') >0", HttpContext.Current.Session["ConnString"].ToString());
            if (dtwh.Rows.Count == 1)
            {
                query.Sql = "SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + HttpContext.Current.Session["WHCode"].ToString() + "') >0  ";
                sqlDataSource1.Fill();
                report.Parameters["WarehouseCode"].Value = dtwh.Rows[0][0].ToString();

            }
            else
            {
                query.Sql = "SELECT WarehouseCode, Description + '(' + WarehouseCode + ')' AS  Description FROM Masterfile.Warehouse WHERE ISNULL (IsInactive,  0) = 0 AND   CHARINDEX(WarehouseCode,'" + HttpContext.Current.Session["WHCode"].ToString() + "') >0  UNION ALL SELECT 'ALL' AS WarehouseCode, 'ALL' AS Description ORDER BY WarehouseCode  ";
                sqlDataSource1.Fill();
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //DataTable getLogo = Gears.RetriveData2("SELECT Field1 AS Logo FROM IT.SystemSettings WHERE Code='COMPLOGO'", HttpContext.Current.Session["ConnString"].ToString());

            //byte[] str = Convert.FromBase64String(getLogo.Rows[0]["Logo"].ToString() as string);
            //MemoryStream ms = new MemoryStream(str);
            //ms.Seek(0, SeekOrigin.Begin);
            //Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            //((XRPictureBox)sender).Image = bmp;
            ////xrPictureBox2.Image = bmp;
            //ms.Close();
        }

 

    }
}
