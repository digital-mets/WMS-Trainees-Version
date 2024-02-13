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
using System.Data.SqlClient;

namespace GWL
{
    public partial class frmDashboardBasic : System.Web.UI.Page
    {
        public static string strSubmitted = "0", strUnSubmitted = "0", strUnposted = "0", strCancelled = "0";
        public static List<ValueData> details;
        public static List<NotesData> NotesDetail;
        public static List<DashBoardData> DashboardDetail;
        public static string strTotalProj;
        string schemaname;
        protected void Page_Load(object sender, EventArgs e)
        {
            schemaname = Session["schemaname"].ToString();

            if (!IsPostBack)
            {
                DataTable dtbl;

                dtbl = Gears.RetriveData2("SELECT ISNULL(Field1,0) Field1,ISNULL(Field2,0) Field2,ISNULL(Field3,0) Field3,ISNULL(Field4,0) Field4 FROM [" + schemaname + "].Users with (nolock) where UserID='" + Session["UserID"].ToString() + "'", Session["ConnString"].ToString());

                foreach (DataRow _row in dtbl.Rows)
                {
                    strUnSubmitted = _row["Field1"].ToString();
                    strSubmitted = _row["Field2"].ToString();
                    strCancelled = _row["Field3"].ToString();
                    strUnposted = _row["Field4"].ToString();
                }

                dtbl = Gears.RetriveData2(" select REPLACE(FuncGroupID,'/','') FuncGroupID ,dbo.ProperCase(ISNULL(FullName,'')) Head,ISNULL(CONVERT(VARCHAR(10),DateClosed, 111),NULL) DateClosed, CASE WHEN DateClosed is null THEN 'Pending' ELSE 'Complete' END as Status  " +
                                            " from ["+ schemaname + "].FuncGroup A with (nolock) "+
                                            " LEFT JOIN [" + schemaname + "].Users B with (nolock) " +
                                            " ON A.Head = B.UserID ", Session["ConnString"].ToString());
                var detailsX = new List<ValueData>();
                detailsX.AddRange(
                    from DataRow dtrow in dtbl.Rows
                                 select new ValueData
                                 {
                                     Code = dtrow["FuncGroupID"].ToString().Trim(),
                                     Value = dtrow["DateClosed"].ToString().Trim(),
                                     Head = dtrow["Head"].ToString().Trim(),
                                     Status = dtrow["Status"].ToString().Trim()
                                 }
                                 );
                details = detailsX;

                //2020-10-26 RA Added code joining to mainmenu table and get column commandstring to view transactions.
                dtbl = Gears.RetriveData2("select TOP 20 dbo.ProperCase(FullName) as FromUser,Docnumber+': '+Convert(varchar(max),Message) as Message, DateTime,'.'+C.CommandString  + '?entry=V&transtype=' + A.TranStype + '&parameters=&iswithdetail=true&docnumber=' + docnumber as CommandString " +
                                            " from [" + schemaname + "].Notes A with (nolock) " +
                                            " LEFT JOIN [" + schemaname + "].Users B with (nolock) " +
                                            " ON A.FromUser = B.UserID "+
                                            " INNER JOIN [" + schemaname + "].Mainmenu C with (nolock) " +
                                            " ON A.TransType = C.TransType " +
                                            " WHERE FromUser ='" + Session["UserID"].ToString() + "' OR ToUser='" + Session["UserID"].ToString() + "' ORDER BY DateTime desc", Session["ConnString"].ToString());
                var detailsN = new List<NotesData>();
                detailsN.AddRange(
                    from DataRow dtrow in dtbl.Rows
                    select new NotesData
                    {
                        Fromuser = dtrow["FromUser"].ToString().Trim(),
                        Message = dtrow["Message"].ToString().Trim(),
                        Date = dtrow["DateTime"].ToString().Trim(),
                        //2020-10-26 RA Added Column CommandString
                        CommandString = dtrow["CommandString"].ToString().Trim()
                    }
                                 );
                NotesDetail = detailsN;


                dtbl = Gears.RetriveData2("SELECT  Code, Description " +
                            " from [" + schemaname + "].GenericLookup A with (nolock) WHERE LookupKey='DASHBOARD' ", Session["ConnString"].ToString());
                var detailsD = new List<DashBoardData>();
                detailsD.AddRange(
                    from DataRow dtrow in dtbl.Rows
                    select new DashBoardData
                    {
                        Code = dtrow["Code"].ToString().Trim(),
                        Value = dtrow["Description"].ToString().Trim()
                    }
                                 );
                DashboardDetail = detailsD;


                //DataTable dtblPO = Gears.RetriveData2(" SELECT COUNT(DISTINCT Docnumber) OpenProj " +
                //                                    " FROM [" + schemaname + "].PurchaseOrder MS       " +
                //                                    " WHERE    " +
                //                                    " (ISNULL(MS.SubmittedBy,'') = '')"

                //, Session["ConnString"].ToString());
                //if (dtblPO.Rows.Count > 0)
                //{
                //    strTotalProj = dtblPO.Rows[0][0].ToString();
                //}
                //else
                //{
                //    strTotalProj = "0";
                //}

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
            public string Code { get; set; }
            public string Value { get; set; }
            public string Head { get; set; }
            public string Status { get; set; }
        }

        public class DashBoardData
        {
            public string Code { get; set; }
            public string Value { get; set; }
        }

        public class NotesData
        {
            public string Fromuser { get; set; }
            public string Message  { get; set; }
            public string Date { get; set; }
            public string CommandString { get; set; } //2020-10-26 Added variable for Module
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
            strReturn = strSubmitted + '|' + strUnSubmitted + '|' + strCancelled + '|' + strUnposted;

            return strReturn;
        }

        [WebMethod]
        public static ValueData[] GetFunc(string ID)
        {

            return details.ToArray();
        }

        [WebMethod]
        public static NotesData[] GetNote(string ID)
        {

            return NotesDetail.ToArray();
        }

        [WebMethod]
        public static DashBoardData[] GetDashboard(string ID)
        {

            return DashboardDetail.ToArray();
        }

        [WebMethod]
        public static string GetProjects(string name, string value)
        {
            string strReturn = "0";
            strReturn = strTotalProj;
            return strReturn;
        }
    }
}