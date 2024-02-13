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

namespace GWL
{
    public partial class frmAssetDashboard : System.Web.UI.Page
    {
        public static string strSubmitted = "0", strUnSubmitted = "0", strUnposted = "0", strCancelled = "0";
        public static List<ValueData> details;
        public static List<LocationData> LocDetail;
        public static List<DashBoardData> DashboardDetail;
        string schemaname;
        protected void Page_Load(object sender, EventArgs e)
        {
            schemaname = Session["schemaname"].ToString();
            if (!IsPostBack)
            {
                DataTable dtbl;

                dtbl = Gears.RetriveData2("SELECT Code, Description from "+schemaname+".GenericLookup WHERE LookUpKey='DASHBOARD' ", Session["ConnString"].ToString());

                foreach (DataRow _row in dtbl.Rows)
                {

                    switch (_row["Code"].ToString())
                    {
                        case "ASSDIS": strUnposted = _row["Description"].ToString(); break;
                        case "ASSDEP": strCancelled = _row["Description"].ToString(); break;
                        case "ASSFST": strSubmitted = _row["Description"].ToString(); break;
                        case "TOTASS": strUnSubmitted = _row["Description"].ToString(); break;
                        //case "PERCC": closed = _row["Description"].ToString(); break;
                        //case "PERCH": overdue = _row["Description"].ToString(); break;
                        //case "PERCO": ongoing = _row["Description"].ToString(); break;
                        //case "PERCP": totalproj = _row["Description"].ToString(); break;
                    }

                }

                dtbl = Gears.RetriveData2(" select CASE WHEN ISNULL(Field3,'')='' THEN Description ELSE Field3 END Description,Field1 RunningBalance ,Field2 Completion  from "+ schemaname + ".ItemCategory where IsAsset = 1 and (ISNULL(Field1,'')!='' OR ISNULL(Field2,'')!='') order by field1 desc ", Session["ConnString"].ToString());
                var detailsX = new List<ValueData>();
                detailsX.AddRange(
                    from DataRow dtrow in dtbl.Rows
                                 select new ValueData
                                 {
                                     Description = dtrow["Description"].ToString().Trim(),
                                     RunningValue = dtrow["RunningBalance"].ToString().Trim(),
                                     Completion = dtrow["Completion"].ToString().Trim()
                                 }
                                 );
                details = detailsX;

                dtbl = Gears.RetriveData2("EXEC ["+ schemaname + "].[sp_dw_AssetperDept] '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
                var detailsN = new List<LocationData>();
                detailsN.AddRange(
                    from DataRow dtrow in dtbl.Rows
                    select new LocationData
                    {
                        Location = dtrow["Location"].ToString().Trim(),
                        Description = dtrow["Description"].ToString().Trim(),
                        RunningBookValue = dtrow["RunningBookValue"].ToString().Trim()
                    }
                                 );
                LocDetail = detailsN;


                dtbl = Gears.RetriveData2("exec "+schemaname+".sp_dw_AssetBreakdown '" +Session["userid"].ToString()+"'", Session["ConnString"].ToString());
                var detailsD = new List<DashBoardData>();
                detailsD.AddRange(
                    from DataRow dtrow in dtbl.Rows
                    select new DashBoardData
                    {
                        Category = dtrow["Category"].ToString().Trim(),
                        Code = dtrow["Code"].ToString().Trim(),
                        Value = dtrow["Description"].ToString().Trim()
                    }
                                 );
                DashboardDetail = detailsD;


            }

        }

        //protected void ASPxDashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs e)
        //{
        //    SqlServerConnectionParametersBase s = (SqlServerConnectionParametersBase)e.ConnectionParameters;
        //    s.ServerName = "192.168.180.9";
        //    s.DatabaseName = "GWL-TEMP";
        //    s.UserName = "dbo";
        //    s.Password = "IT4321$#@!";

        //    e.ConnectionParameters = s;
        //}

        public class ValueData
        {
            public string Description { get; set; }
            public string RunningValue { get; set; }
            public string Completion { get; set; }
        }

        public class DashBoardData
        {
            public string Category { get; set; }
            public string Code { get; set; }
            public string Value { get; set; }
        }

        public class LocationData
        {
            public string Location { get; set; }
            public string Description  { get; set; }
            public string RunningBookValue { get; set; }
        }

        [WebMethod]
        public static string GetValue(string name, string value)
        {
            string strReturn = "0";
            //switch (name)
            //{
            //    case "Submitted":
            //        strReturn = strSubmitted;
            //        break;
            //    case "Unsubmitted":
            //        strReturn = strUnSubmitted;
            //        break;
            //    case "Cancelled":
            //        strReturn = strCancelled;
            //        break;
            //    case "Unposted":
            //        strReturn = strUnposted;
            //        break;
            //}
            strReturn = strSubmitted + '|' + strUnSubmitted + '|' + strCancelled + '|' + strUnposted;// +'|' + totalproj + '|' + ongoing + '|' + overdue + '|' + closed;
            //strReturn = "10000|90|80|0";
            return strReturn;
        }

        [WebMethod]
        public static ValueData[] GetAssetInfo(string ID)
        {

            return details.ToArray();
        }

        [WebMethod]
        public static LocationData[] GetLocation(string ID)
        {

            return LocDetail.ToArray();
        }

        [WebMethod]
        public static DashBoardData[] GetDashboard(string ID)
        {

            return DashboardDetail.ToArray();
        }
    }
}