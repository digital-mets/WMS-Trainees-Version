using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using GearsLibrary;
using System.Configuration;
using DevExpress.Web;
using System.Globalization;
using Newtonsoft.Json;


namespace GWL.IT
{
    public partial class frmDashboardIPC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int a  = basicdatatable.Rows.Count;
        }

        public class Items
        {
            public string ItemCode { get; set; }
            public string ColorCode { get; set; }
            public string ClassCode { get; set; }
            public string SizeCode { get; set; }
            public string RequestQty { get; set; }
        }

        [WebMethod]
        [ScriptMethod]
        public static string  GetDashboard(string ItemStr)
        {

            List<Items> Item = JsonConvert.DeserializeObject<List<Items>>(ItemStr);
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(ItemStr, (typeof(DataTable)));

            
            if (HttpContext.Current.Session["ConnString"] != null)
            {
                string schemaname = "GEARS";
                if (HttpContext.Current.Session["schemaname"] != null)
                {
                    schemaname = HttpContext.Current.Session["schemaname"].ToString();
                }


                DataTable dbtl = Gears.RetriveData2("select Prefix + RIGHT('0000000000' + CONVERT(varchar(max), SeriesNumber + 1), SeriesWidth) as Docnumber "+
                                                     " from "+ schemaname + ".DocNumberSettings WHERE Module = 'PRCPRM' and Prefix = 'PR'" 
                                                    , HttpContext.Current.Session["ConnString"].ToString());


                if (dbtl.Rows.Count > 0)
                {
                    string Docnumber = dbtl.Rows[0][0].ToString();

                    string strHeader = " INSERT INTO " + schemaname + ".PurchaseRequest " +
                                        " (DocNumber, DocDate, Status, Remarks, AddedBy, AddedDate, CustomerCode) " +
                                        " SELECT '" + Docnumber + "',GETDATE(),'N','Generated from Dashboard by User ID:" + HttpContext.Current.Session["userid"] + "','" + HttpContext.Current.Session["userid"] + "',GETDATE(),NULL ";
                    string strDetail = " INSERT INTO " + schemaname + ".PurchaseRequestDetail " +
                                        " (LineNumber, DocNumber, ItemCode, ColorCode, ClassCode, SizeCode, RequestQty, UnitBase) ";

                    int line = 0;
                    foreach (Items ItemData in Item)
                    {
                        if (ItemData.ItemCode != "")
                        {
                            line++;
                            strDetail += " SELECT '" + line.ToString().PadLeft(5, '0') + "','" + Docnumber + "','" + ItemData.ItemCode + "','" + ItemData.ColorCode + "','" + ItemData.ClassCode + "','" + ItemData.SizeCode + "','" + ItemData.RequestQty + "', UnitBase " +
                                         " FROM " + schemaname + ".Item WHERE ItemCode = '" + ItemData.ItemCode + "' ";

                            if(line!=Item.Count-1)
                            {
                                strDetail += " UNION ALL ";
                            }
                        }
                    }

                    Gears.RetriveData2(strHeader + strDetail, HttpContext.Current.Session["ConnString"].ToString());

                    Gears.RetriveData2(" UPDATE " + schemaname + ".DocNumberSettings SET SeriesNumber = SeriesNumber+1 WHERE Module = 'PRCPRM' and Prefix = 'PR'", HttpContext.Current.Session["ConnString"].ToString());

                    return "Purchase Request has been created, please refer on documnet number :"+ Docnumber;
                }
                else
                {
                    return "Can't create Document Number";
                }

            }
            else
            {
                return "Can't Connect to Web Service";
            }
            

        }


    }
}