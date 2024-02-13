using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.IO;


namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_Trimcard : DevExpress.XtraReports.UI.XtraReport
    {
        public P_Trimcard()
        {
            InitializeComponent(); 
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }



        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        private void xrPictureBox1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(this.GetCurrentColumnValue("ItemImage").ToString() == ""))
            {
                byte[] str = Convert.FromBase64String(this.GetCurrentColumnValue("ItemImage") as string);
                MemoryStream ms = new MemoryStream(str);
                ms.Seek(0, SeekOrigin.Begin);
                Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
                ((XRPictureBox)sender).Image = bmp;
                ms.Close();
            }
        }
    }
}
