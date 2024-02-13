using GearsLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.Web;
using DevExpress.Web.Data;
using System.Web;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GWL.MainMenu
{
    public partial class GEARSMainMenu : System.Web.UI.Page
    {
        string schemaname = "",
            username = "",
            compname = "";
        public static decimal Accept = 0;
        public static decimal Reject = 0;
        public static decimal Pending = 0;
        public static decimal Inbound = 0;
        public static decimal Outbound = 0;
        public static decimal Complete = 0;
        public static decimal RFPutAway = 0;
        //Picking
        public static decimal AcceptPicklist = 0;
        public static decimal RejectPicklist = 0;
        public static decimal PendingPicklist = 0;
        public static decimal CompletePick = 0;
        public static decimal CompleteLoadPick = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            schemaname = Request.QueryString["schemaname"].ToString();
            Session["schemaname"] = schemaname;

            Session["APIURL"] = Common.Common.SystemSetting("APIURL", Session["ConnString"].ToString());

            DataTable dtcheckaccess = Gears.RetriveData2("SELECT A.UserID from " + schemaname + ".Users A INNER JOIN " + schemaname + ".Usersdetail B ON A.UserID = B.UserID  INNER JOIN " + schemaname + ".UserrolesDetail C ON B.RoleID = C.RoleID AND C.ModuleID='ACTSOA2'  where Access like '%L%' AND A.UserID = '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
            if (dtcheckaccess.Rows.Count == 0)
            {
                getEmail.Visible = false;
            }

            string referer;
            try //Validation to restrict user to browse/type directly to browser's address
            {
                referer = Request.ServerVariables["http_referer"];
            }
            catch
            {
                referer = "";
            }

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }

            if (!IsPostBack)
            {

             //   string result = RCommon.RCommon.TourChecker(Session["userid"].ToString(), "MAINMENU", Session["ConnString"].ToString());

                

                //if ((string.IsNullOrEmpty(result.Trim() as string)))
                //{
                //    Session["TourDone"] = "0";
                //}
                //else {
                //    if (result.Trim() == "1" || result.Trim() == "True")
                //    {
                //        Session["TourDone"] = "1";
                //    }
                //    else
                //    {
                //        Session["TourDone"] = "0";
                //    }
                //}
               


                CheckUserStatus();
                gearsmetsit();

                DataTable Dtgetcomp = Gears.RetriveData2("Select Value from IT.SystemSettings where code = 'COMPNAME'", Session["ConnString"].ToString());
                compname = Dtgetcomp.Rows[0][0].ToString();

                DataTable Dtgetname = Gears.RetriveData2(
                    "SELECT A.UserName, ISNULL(EmailAddress,''), LEFT(ISNULL(A.Firstname,''),1) AS FAbbr, " +
                    "       ISNULL(A.Firstname,'') AS Firstname,ISNULL(A.FullName,'') AS FullName, " +
                    "	    C.Description,A.Field9 " +
                    "FROM " + schemaname + ".Users A " +
                    "INNER JOIN " + schemaname + ".UsersDetail B " +
                    "ON A.UserID = B.UserID " +
                    "INNER JOIN " + schemaname + ".UserRoles C " +
                    "ON B.RoleID = C.RoleID " +
                    "WHERE A.UserID = '" + Session["userid"].ToString() + "'",
                Session["ConnString"].ToString());

                username = Dtgetname.Rows[0][0].ToString();
                txtCompanyEmail.InnerHtml = Dtgetname.Rows[0][1].ToString();

                txtNameAbbr.InnerHtml = Dtgetname.Rows[0][2].ToString();
                txtFirstName.InnerHtml = Dtgetname.Rows[0][3].ToString();
                txtFullName.InnerHtml = Dtgetname.Rows[0][4].ToString();
                txtRole.InnerHtml = Dtgetname.Rows[0][5].ToString();
                UserImage.Attributes.Add("style", "background-image: url('" + Dtgetname.Rows[0][6].ToString() + "')");



                DateTime now = DateTime.Now;
                txtCompanyName.InnerHtml = compname + " | " + now.ToString("D");


            }
            DataTable AcceptCount = Gears.RetriveData2("select COUNT(*) AS AcceptdOldCount FROM WMS.Inbound WHERE ISNULL(AcceptBy,'')!='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());

            DataTable RejectCount = Gears.RetriveData2("select COUNT(*) AS RejectOldCount FROM WMS.Inbound WHERE ISNULL(RejectBy,'')!='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());

            DataTable PendingCount = Gears.RetriveData2("select COUNT(*) AS PendingOldCount from WMS.Inbound where DocumentationStaff=(select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "') and isnull(CheckerAssignedDate,'')!='' and DATEDIFF(SECOND, CheckerAssignedDate, GETDATE()) > 900 and ISNULL(AcceptDate,'') = ''and ISNULL(RejectDate,'') = ''and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());

            DataTable InboundCount = Gears.RetriveData2("SELECT count(*) as InboundOldCount from wms.Inbound where Field8='CP_RECEIVED' and ISNULL(Field9,'')!='' and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());

            DataTable OutboundCount = Gears.RetriveData2("SELECT count(*) as OutboundOldCount from wms.Picklist where Field8='CP_RECEIVED' and ISNULL(Field9,'')!='' and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());

            DataTable CompleteUnload = Gears.RetriveData2("select Count(*) as CompleteUnload from WMS.Inbound where field8 ='CP_RECEIVED' AND CompleteUnload !='1900-01-01 00:00:00.000' AND ISNULL(SubmittedBy,'')='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')", HttpContext.Current.Session["ConnString"].ToString());

            DataTable RFPutAwayNA = Gears.RetriveData2("select Count(*) as RFPutAwayBy from WMS.Inbound where field8 ='CP_RECEIVED' AND ISNULL(PutAwayBy,'')='' AND ISDATE(PutAwayDate)=0 AND ISNULL(RFPutAwayBy,'')!='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')", HttpContext.Current.Session["ConnString"].ToString());

            if (AcceptCount.Rows.Count > 0)
            {
                string oldInbound = AcceptCount.Rows[0]["AcceptdOldCount"].ToString();
                Accept = Convert.ToDecimal(AcceptCount.Rows[0]["AcceptdOldCount"]);
            }

            if (RejectCount.Rows.Count > 0)
            {
                string oldInbound = RejectCount.Rows[0]["RejectOldCount"].ToString();
                Reject = Convert.ToDecimal(RejectCount.Rows[0]["RejectOldCount"]);
            }

            if (PendingCount.Rows.Count > 0)
            {
                string oldInbound = PendingCount.Rows[0]["PendingOldCount"].ToString();
                Pending = Convert.ToDecimal(PendingCount.Rows[0]["PendingOldCount"]);
            }
            if (InboundCount.Rows.Count > 0)
            {
                string oldInbound = InboundCount.Rows[0]["InboundOldCount"].ToString();
                Inbound = Convert.ToDecimal(InboundCount.Rows[0]["InboundOldCount"]);
            }
            if (OutboundCount.Rows.Count > 0)
            {
                string oldInbound = OutboundCount.Rows[0]["OutboundOldCount"].ToString();
                Outbound = Convert.ToDecimal(OutboundCount.Rows[0]["OutboundOldCount"]);
            }
            if (CompleteUnload.Rows.Count > 0)
            {
                string oldInbound = CompleteUnload.Rows[0]["CompleteUnload"].ToString();
                Complete = Convert.ToDecimal(CompleteUnload.Rows[0]["CompleteUnload"]);
            }
            if (RFPutAwayNA.Rows.Count > 0)
            {
                string oldInbound = RFPutAwayNA.Rows[0]["RFPutAwayBy"].ToString();
                RFPutAway = Convert.ToDecimal(RFPutAwayNA.Rows[0]["RFPutAwayBy"]);
            }

            DataTable AccepPick = Gears.RetriveData2("select COUNT(*) AS AcceptpickOldCount FROM WMS.Picklist WHERE ISNULL(AcceptBy,'')!='' AND Addedby='" + HttpContext.Current.Session["userid"].ToString() + "' AND Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());
            if (AccepPick.Rows.Count > 0)
            {
                string oldInbound = AccepPick.Rows[0]["AcceptpickOldCount"].ToString();
                AcceptPicklist = Convert.ToDecimal(oldInbound);
            }
            DataTable RejectPick = Gears.RetriveData2("select COUNT(*) AS RejectpickOldCount FROM WMS.Picklist WHERE ISNULL(RejectBy,'')!='' AND Addedby='" + HttpContext.Current.Session["userid"].ToString() + "' AND Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());
            if (RejectPick.Rows.Count > 0)
            {
                string oldInbound = RejectPick.Rows[0]["RejectpickOldCount"].ToString();
                RejectPicklist = Convert.ToDecimal(oldInbound);
            }
            DataTable PendingPick = Gears.RetriveData2("select COUNT(*) AS PendingpickOldCount from WMS.Picklist where AddedBy='" + HttpContext.Current.Session["userid"].ToString() + "' and isnull(CheckerAssignedDate,'')!='' and DATEDIFF(SECOND, CheckerAssignedDate, GETDATE()) > 900 and ISNULL(AcceptDate,'') = ''and ISNULL(RejectDate,'') = '' and ISNULL(WarehouseChecker,'')!= '' AND Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')=''", HttpContext.Current.Session["ConnString"].ToString());
            if (PendingPick.Rows.Count > 0)
            {
                string oldInbound = PendingPick.Rows[0]["PendingpickOldCount"].ToString();
                PendingPicklist = Convert.ToDecimal(oldInbound);
            }
            DataTable CompletePicked = Gears.RetriveData2("select Count(*) as CompletePicked from WMS.Picklist where field8 ='CP_RECEIVED' AND ISNULL(RFPickDate,'')!='' AND ISNULL(SubmittedBy,'')='' AND AddedBy='" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            if (CompletePicked.Rows.Count > 0)
            {
                string oldInbound = CompletePicked.Rows[0]["CompletePicked"].ToString();
                CompletePick = Convert.ToDecimal(oldInbound);
            }
            DataTable CompleteloadPick1 = Gears.RetriveData2("select Count(*) as CompleteloadPicked from WMS.Outbound A Left Join WMS.Picklist B on A.DocNumber=B.DocNumber where A.Field8 = 'CP_RECEIVED' AND ISNULL(CompleteLoading, '') != '' AND ISNULL(A.SubmittedBy, '') = ''AND ISNULL(B.SubmittedBy, '') != '' AND A.AddedBy = '" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            if (CompleteloadPick1.Rows.Count > 0)
            {
                string oldInbound = CompleteloadPick1.Rows[0]["CompleteloadPicked"].ToString();
                CompleteLoadPick = Convert.ToDecimal(oldInbound);
            }
            

        }

        private static string date1 = "";
        private static string date2 = "";

        private DataTable gearsmetsit()
        {
            //12-02-2016 GC Changed query to get default date for translist's date from
            //DataTable dtable = Gears.RetriveData2("select value,getdate() as value2 from it.systemsettings where code = 'BOOKDATE'", Session["ConnString"].ToString());
            DataTable dtable = Gears.RetriveData2("DECLARE @TRANSDATEFROM varchar(5) "
                + " SELECT @TRANSDATEFROM = Value FROM " + HttpContext.Current.Session["schemaname"] + ".SystemSettings WHERE Code = 'TRANDATEFR' "
                + " SELECT CASE WHEN @TRANSDATEFROM = 'NO' THEN value ELSE GETDATE() END AS value, GETDATE() AS value2 FROM " + HttpContext.Current.Session["schemaname"] + ".SystemSettings WHERE Code = 'BOOKDATE'", Session["ConnString"].ToString());
            //end

            foreach (DataRow dtRow in dtable.Rows)
            {
                date1 = Convert.ToDateTime(dtRow["value"].ToString()).ToShortDateString();
                date2 = Convert.ToDateTime(dtRow["value2"].ToString()).ToShortDateString();
            }
            return dtable;
        }

        [WebMethod]
        public static string GetData()
        {
            string baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Uploads/" + HttpContext.Current.Session["CompSes"] + "/EmployeeImages/";

            DataTable TableDataTemp = new DataTable();
            string jsondata;
            TableDataTemp =
                Gears.RetriveData2("EXEC " + HttpContext.Current.Session["schemaname"] + ".[sp_creation_Menu] '" + HttpContext.Current.Session["userid"].ToString() + "' ",
                    HttpContext.Current.Session["ConnString"].ToString());

            //Creation datable for getting the final node target Start
            DataTable TableDataFinal = new DataTable();
            TableDataFinal.Columns.Add("id", typeof(int));
            TableDataFinal.Columns.Add("pid", typeof(int));
            TableDataFinal.Columns.Add("Prompt", typeof(String));
            TableDataFinal.Columns.Add("Icon", typeof(String));
            TableDataFinal.Columns.Add("FilePath", typeof(String));
            //Creation datable for getting the final node target End

            foreach (DataRow dr in TableDataTemp.Rows)
            {
                string sql = dr["SQLCommand"].ToString(),
                       ribbon = dr["Ribbon"].ToString(),
                       param = dr["Parameters"].ToString(),
                       frm = dr["CommandString"].ToString(),
                       transtype = dr["TransType"].ToString(),
                       moduleid = dr["ModuleID"].ToString(),
                       storedproc = dr["StoredProc"].ToString(),
                       access = dr["Access"].ToString(),
                       prompt = dr["Prompt"].ToString(),
                       glposting = dr["GLPosting"].ToString(),
                       funcg = dr["FuncGroupID"].ToString(),
                       redirect = "",
                       rep = "";

                DataRow row = TableDataFinal.NewRow();
                row["id"] = dr["id"].ToString();
                row["pid"] = dr["pid"].ToString();
                row["Prompt"] = dr["Prompt"].ToString();
                row["Icon"] = dr["Icon"].ToString();
                if (sql != "")
                {
                    redirect = Gears.Encrypt(dr["SQLCommand"].ToString(), "mets");
                    row["FilePath"] = "../Translist.aspx?val=~" + redirect + "&prompt=" + prompt + "&frm=" + frm + "&date1=" + date1 + "&date2=" + date2 + "&ribbon=" + ribbon + "&transtype=" + transtype + "&moduleid=" + moduleid + "&sp=" + storedproc + "&access=" + access + "&parameters=" + param + "&glpost=" + glposting + "&schemaname=" + HttpContext.Current.Session["schemaname"].ToString();
                }
                else if (prompt == "Dashboard")
                {
                    row["FilePath"] = string.Format("{0}", frm, Gears.Encrypt(HttpContext.Current.Session["userid"].ToString(), "mets"));
                }
                else if (param == "PView")
                {
                    row["FilePath"] = "../WebReports/ReportViewer.aspx?val=~" + frm;
                }
                else if (param == "Query")
                {
                    row["FilePath"] = frm;
                }
                else if (param == "SView")
                {
                    row["FilePath"] = "../WebReports/SSRSViewer.aspx?val=~" + frm;
                }
                else
                {
                    row["FilePath"] = "javascript:;";
                }
                TableDataFinal.Rows.Add(row);
            }

            jsondata = JsonConvert.SerializeObject(TableDataFinal);
            return jsondata;
        }

        [WebMethod]
        public static string[] GetContract()
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();
            string[] response = new string[3];
            try
            {
                DataTable dt = Gears.RetriveData2("select DocNumber,'EXPIRED' AS Status,CONVERT(VARCHAR(20),DateTo,107) as 'Expiration Date' from WMS.Contract WHERE DateTo between DATEADD(DAY, -7, GETDATE()) and GETDATE() UNION ALL " +
                    "select DocNumber, 'NEARLY EXPIRE' AS Status, CONVERT(VARCHAR(20), DateTo, 107) as 'Expiration Date' from WMS.Contract WHERE DateTo between GETDATE() and   DATEADD(DAY, +7, GETDATE())", Conn);

                response[0] = "";
                response[1] = JsonConvert.SerializeObject(dt);
                response[2] = "success";
                return response;
            }
            catch (Exception ex)
            {
                response[0] = "Error!";
                response[1] = ex.ToString();
                response[2] = "error";
            }
            return response;
        }

        [WebMethod]
        public static string[] GetNotify()
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();
            string[] response = new string[3];
            try
            {

                DataTable dt = Gears.RetriveData2("EXEC sp_GEARS_NOTIFICATION '" + HttpContext.Current.Session["userid"].ToString() + "' ", Conn);
                
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt);
                    response[2] = "success";
                    return response;
            }
            catch (Exception ex)
            {
                response[0] = "Error!";
                response[1] = ex.ToString();
                response[2] = "error";
            }
            return response;
        }

        //Dropdown getDropdownEmp 
        [WebMethod]
        public static string getWareHouse()
        {
            DataTable dtWarehouse = new DataTable();
            string jsondata;

            dtWarehouse = Gears.RetriveData2(
                "SELECT WarehouseCode, Description " +
                "FROM " + HttpContext.Current.Session["schemaname"] + ".[Warehouse] " +
                "WHERE ISNULL([IsInactive],0) = 0",
            HttpContext.Current.Session["ConnString"].ToString());

            jsondata = JsonConvert.SerializeObject(dtWarehouse);
            return jsondata;
        }
        [WebMethod]
        public static string[] GetNotifyMe()
        {
            //string Conn = HttpContext.Current.Session["ConnString"].ToString();
            string[] response = new string[4];
            decimal Accept_count = 0;
            decimal Reject_count = 0;
            decimal Pending_count = 0;
            decimal Inbound_Count = 0;
            decimal Outbound_Count = 0;
            decimal CompleteUnload = 0;
            decimal RFPutAway_Count = 0;
            //PICKING
            decimal Acceptpick_count = 0;
            decimal Rejectpick_count = 0;
            decimal Pendingpick_count = 0;
            decimal CompletedPicked = 0;
            decimal CompletedLoadPicked = 0;
            try
            {
                string Conn = HttpContext.Current.Session["ConnString"].ToString();

                DataTable dt = Gears.RetriveData2("SELECT COUNT(*) AS Accept_count FROM WMS.Inbound WHERE ISNULL(AcceptBy,'')!='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')  and ISNULL(SubmittedBy,'')=''", Conn);


                if (dt.Rows.Count > 0)
                {
                    string old = dt.Rows[0]["Accept_count"].ToString();
                    Accept_count = Convert.ToDecimal(dt.Rows[0]["Accept_count"]);
                }
                if (Accept_count > Accept)
                {
                    DataTable accept = Gears.RetriveData2("Select top 1 'Inbound' as TransType,FullName,CONVERT(VARCHAR(20), FORMAT(AcceptDate, 'MM-dd-yyyy %H:mm tt')) as AcceptDate,* from WMS.Inbound A Left Join IT.Users B ON A.AcceptBy=B.UserID WHERE ISNULL(AcceptBy,'')!='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "') order by A.AcceptDate desc", Conn);
                    Accept = Accept_count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt);
                    response[2] = JsonConvert.SerializeObject(accept);
                    response[3] = "success";
                    return response;
                }

                DataTable dt1 = Gears.RetriveData2("SELECT COUNT(*) AS Reject_count FROM WMS.Inbound WHERE ISNULL(RejectBy,'')!='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')  and ISNULL(SubmittedBy,'')='' ", Conn);


                if (dt1.Rows.Count > 0)
                {
                    string old = dt1.Rows[0]["Reject_count"].ToString();
                    Reject_count = Convert.ToDecimal(dt1.Rows[0]["Reject_count"]);
                }
                if (Reject_count > Reject)
                {
                    DataTable reject = Gears.RetriveData2("SELECT TOP 1 'Inbound' as TransType,FullName,CONVERT(VARCHAR(20), FORMAT(RejectDate, 'MM-dd-yyyy %H:mm tt')) as RejectDate,* FROM WMS.Inbound A Left Join IT.Users B ON A.RejectBy=B.UserID WHERE ISNULL(RejectBy,'')!=''  AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "') order by A.RejectDate desc", Conn);
                    Reject = Reject_count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt1);
                    response[2] = JsonConvert.SerializeObject(reject);
                    response[3] = "success";
                    return response;
                }

                DataTable dt2 = Gears.RetriveData2("select  COUNT(*) AS Pending_count from WMS.Inbound where isnull(CheckerAssignedDate,'')!='' and DATEDIFF(SECOND, CheckerAssignedDate, GETDATE()) > 900 and ISNULL(AcceptDate,'') = '' and ISNULL(RejectDate,'') = '' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "')and ISNULL(SubmittedBy,'')='' ", Conn);


                if (dt2.Rows.Count > 0)
                {
                    string old = dt2.Rows[0]["Pending_count"].ToString();
                    Pending_count = Convert.ToDecimal(dt2.Rows[0]["Pending_count"]);
                }
                if (Pending_count > Pending)
                {
                    DataTable pending = Gears.RetriveData2("select top 1 DocNumber,CheckerAssignedDate from WMS.Inbound where ISNULL(AcceptDate,'') = '' and ISNULL(RejectDate,'') = '' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "') order by CheckerAssignedDate desc", Conn);
                    Pending = Pending_count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt2);
                    response[2] = JsonConvert.SerializeObject(pending);
                    response[3] = "success";
                    return response;
                }

                DataTable dt3 = Gears.RetriveData2("SELECT count(*) as Inbound_Count from wms.Inbound where Field8 = 'CP_RECEIVED' and ISNULL(Field9,'') != '' and ISNULL(SubmittedBy,'')=''", Conn);

                if (dt3.Rows.Count > 0)
                {
                    string old = dt3.Rows[0]["Inbound_Count"].ToString();
                    Inbound_Count = Convert.ToDecimal(dt3.Rows[0]["Inbound_Count"]);
                }
                if (Inbound_Count > Inbound)
                {
                    Inbound = Inbound_Count;
                    DataTable inbounddetail = Gears.RetriveData2("SELECT top 1 FORMAT(DocDate, 'MM-d-yyyy') as DocDate,* FROM WMS.Inbound Where ISNULL(SubmittedBy,'')='' and ISNULL(WarehouseChecker,'')='' and ISNULL(ICNNumber,'')!='' order by Field9 desc", Conn);
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt3);
                    response[2] = JsonConvert.SerializeObject(inbounddetail);
                    response[3] = "success";
                    return response;
                }

                DataTable dt4 = Gears.RetriveData2("SELECT count(*) as Outbound_Count from wms.Picklist where Field8 = 'CP_RECEIVED' and ISNULL(Field9,'') != '' and ISNULL(SubmittedBy,'')=''", Conn);

                if (dt4.Rows.Count > 0)
                {
                    string old = dt4.Rows[0]["Outbound_Count"].ToString();
                    Outbound_Count = Convert.ToDecimal(dt4.Rows[0]["Outbound_Count"]);
                }
                if (Outbound_Count > Outbound)
                {
                    DataTable picklistdetail = Gears.RetriveData2("SELECT top 1 FORMAT(DocDate, 'MM-d-yyyy') as DocDate,* FROM WMS.Picklist Where ISNULL(SubmittedBy,'')='' and ISNULL(WarehouseChecker,'')='' order by Field9 desc", Conn);
                    Outbound = Outbound_Count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt4);
                    response[2] = JsonConvert.SerializeObject(picklistdetail);
                    response[3] = "success";
                    return response;
                }
                DataTable dt5 = Gears.RetriveData2("select Count(*) as CompleteUnload from WMS.Inbound where field8 ='CP_RECEIVED'ANd StartUnload!='1900-01-01 00:00:00.000' AND CompleteUnload !='1900-01-01 00:00:00.000' AND ISNULL(SubmittedBy,'')='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "') ", Conn);


                if (dt5.Rows.Count > 0)
                {
                    string old = dt5.Rows[0]["CompleteUnload"].ToString();
                    CompleteUnload = Convert.ToDecimal(dt5.Rows[0]["CompleteUnload"]);
                }
                if (CompleteUnload > Complete)
                {
                    DataTable completed = Gears.RetriveData2("select top 1 DocNumber,'Inbound' as TransType,CONVERT(VARCHAR(20), FORMAT(CompleteUnload, 'MM-dd-yyyy %H:mm tt')) as CompleteUnload,* from WMS.Inbound A where field8 ='CP_RECEIVED' AND (ISDATE(CompleteUnload)=1 or CompleteUnload !='1900-01-01 00:00:00.000') AND ISNULL(SubmittedBy,'')='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "') order by A.CompleteUnload desc", Conn);
                    Complete = CompleteUnload;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt5);
                    response[2] = JsonConvert.SerializeObject(completed);
                    response[3] = "success";
                    return response;
                }

                DataTable dt6 = Gears.RetriveData2("select Count(*) as RFPutAwayBy from WMS.Inbound where field8 ='CP_RECEIVED' AND ISNULL(PutAwayBy,'')='' AND ISDATE(PutAwayDate)=0 AND ISNULL(RFPutAwayBy,'')!='' AND DocumentationStaff = (select A.UserName from it.Users a left join WMS.Inbound B on A.UserID = B.DocumentationStaff where A.UserID = '" + HttpContext.Current.Session["userid"].ToString() + "') ", Conn);


                if (dt6.Rows.Count > 0)
                {
                    string old = dt6.Rows[0]["RFPutAwayBy"].ToString();
                    RFPutAway_Count = Convert.ToDecimal(dt6.Rows[0]["RFPutAwayBy"]);
                }
                if (RFPutAway_Count > RFPutAway)
                {
                    DataTable PutAway = Gears.RetriveData2("select top 1 'Inbound' as TransType,FullName,CONVERT(VARCHAR(20), FORMAT(RFPutAwayDate, 'MM-dd-yyyy %H:mm tt')) as RFPutAwayDate,* from WMS.Inbound A Left Join IT.Users B ON A.RFPutAwayBy=B.UserID where A.Field8 ='CP_RECEIVED' AND ISNULL(PutAwayBy,'')='' AND ISNULL(RFPutAwayBy,'')!='' order by A.RFPutAwayDate desc", Conn);
                    RFPutAway = RFPutAway_Count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt6);
                    response[2] = JsonConvert.SerializeObject(PutAway);
                    response[3] = "success";
                    return response;
                }
                //PICKING

                DataTable dt7 = Gears.RetriveData2("select COUNT(*) AS Accept_count FROM WMS.Picklist WHERE ISNULL(AcceptBy,'')!='' AND Addedby='" + HttpContext.Current.Session["userid"].ToString() + "' AND Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')=''", Conn);


                if (dt7.Rows.Count > 0)
                {
                    string old = dt7.Rows[0]["Accept_count"].ToString();
                    Acceptpick_count = Convert.ToDecimal(old);
                }
                if (Acceptpick_count > AcceptPicklist)
                {
                    DataTable accept = Gears.RetriveData2("SELECT TOP 1 FullName,CONVERT(VARCHAR(20), FORMAT(AcceptDate, 'MM-dd-yyyy %H:mm tt')) as AcceptDate,* FROM WMS.Picklist A Left Join IT.Users B ON A.AcceptBy=B.UserID WHERE ISNULL(AcceptBy,'')!='' AND A.Addedby='" + HttpContext.Current.Session["userid"].ToString() + "' AND A.Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')='' order by A.AcceptDate desc", Conn);
                    AcceptPicklist = Acceptpick_count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt7);
                    response[2] = JsonConvert.SerializeObject(accept);
                    response[3] = "success";
                    return response;
                }

                DataTable dt8 = Gears.RetriveData2("select COUNT(*) AS Reject_count FROM WMS.Picklist WHERE ISNULL(RejectBy,'')!='' AND Addedby='" + HttpContext.Current.Session["userid"].ToString() + "' AND Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')=''", Conn);


                if (dt8.Rows.Count > 0)
                {
                    string old = dt8.Rows[0]["Reject_count"].ToString();
                    Rejectpick_count = Convert.ToDecimal(old);
                }
                if (Rejectpick_count > RejectPicklist)
                {
                    DataTable reject = Gears.RetriveData2("SELECT TOP 1 FullName,CONVERT(VARCHAR(20), FORMAT(RejectDate, 'MM-dd-yyyy %H:mm tt')) as RejectDate,* FROM WMS.Picklist A Left Join IT.Users B ON A.RejectBy=B.UserID WHERE ISNULL(RejectBy,'')!='' AND A.Addedby='" + HttpContext.Current.Session["userid"].ToString() + "' AND A.Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')='' order by A.RejectDate desc", Conn);
                    RejectPicklist = Rejectpick_count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt8);
                    response[2] = JsonConvert.SerializeObject(reject);
                    response[3] = "success";
                    return response;
                }

                DataTable dt9 = Gears.RetriveData2("select COUNT(*) AS Pending_count FROM WMS.Picklist WHERE AddedBy='" + HttpContext.Current.Session["userid"].ToString() + "' and isnull(CheckerAssignedDate,'')!='' and DATEDIFF(SECOND, CheckerAssignedDate, GETDATE()) > 900 and ISNULL(AcceptDate,'') = ''and ISNULL(RejectDate,'') = '' and ISNULL(WarehouseChecker,'')!= '' AND Field8 = 'CP_RECEIVED' and ISNULL(SubmittedBy,'')=''", Conn);


                if (dt9.Rows.Count > 0)
                {
                    string old = dt9.Rows[0]["Pending_count"].ToString();
                    Pendingpick_count = Convert.ToDecimal(old);
                }
                if (Pendingpick_count > PendingPicklist)
                {
                    DataTable pending = Gears.RetriveData2("select top 1 DocNumber from WMS.Picklist where ISNULL(AcceptDate,'') = '' and ISNULL(RejectDate,'') = '' AND AddedBy = '" + HttpContext.Current.Session["userid"].ToString() + "' order by CheckerAssignedDate desc", Conn);
                    PendingPicklist = Pendingpick_count;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt9);
                    response[2] = JsonConvert.SerializeObject(pending);
                    response[3] = "success";
                    return response;
                }

                DataTable dt10 = Gears.RetriveData2("select Count(*) as RFPutAwayBy from WMS.Picklist where field8 ='CP_RECEIVED' AND ISNULL(RFPickBy,'')!='' AND ISNULL(RFPickDate,'')!='' AND ISNULL(SubmittedBy,'')='' AND AddedBy='" + HttpContext.Current.Session["userid"].ToString() + "'", Conn);


                if (dt10.Rows.Count > 0)
                {
                    string old = dt10.Rows[0]["RFPutAwayBy"].ToString();
                    CompletedPicked = Convert.ToDecimal(old);
                }
                if (CompletedPicked > CompletePick)
                {
                    DataTable pickcomplete = Gears.RetriveData2("select top 1 FullName,CONVERT(VARCHAR(20), FORMAT(RFPickDate, 'MM-dd-yyyy %H:mm tt')) as RFPickDate,* from WMS.Picklist A Left Join IT.Users B ON A.RFPickBy = B.UserID where A.field8 = 'CP_RECEIVED' and ISNULL(RFPickBy, '') != '' AND A.AddedBy = '" + HttpContext.Current.Session["userid"].ToString() + "' order by A.RFPickDate desc", Conn);
                    CompletePick = CompletedPicked;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt10);
                    response[2] = JsonConvert.SerializeObject(pickcomplete);
                    response[3] = "success";
                    return response;
                }

                DataTable dt11 = Gears.RetriveData2("select Count(*) as CompleteUnload from WMS.Outbound A Left Join WMS.Picklist B on A.DocNumber=B.DocNumber where A.Field8 = 'CP_RECEIVED' AND (ISNULL(CompleteLoading, '') != '' OR CompleteLoading!='1900-01-01 00:00:00.000') AND ISNULL(A.SubmittedBy, '') = ''AND ISNULL(B.SubmittedBy, '') != '' AND A.AddedBy = '" + HttpContext.Current.Session["userid"].ToString() + "'", Conn);


                if (dt11.Rows.Count > 0)
                {
                    string old = dt11.Rows[0]["CompleteUnload"].ToString();
                    CompletedLoadPicked = Convert.ToDecimal(old);
                }
                if (CompletedLoadPicked > CompleteLoadPick)
                {
                    DataTable completed = Gears.RetriveData2("select top 1 CONVERT(VARCHAR(20), FORMAT(CompleteLoading, 'MM-dd-yyyy %H:mm tt'))as CompleteLoading,* from WMS.Outbound A Left Join WMS.Picklist B on A.DocNumber=B.DocNumber where A.Field8 = 'CP_RECEIVED' AND (ISNULL(CompleteLoading, '') != '' OR CompleteLoading!='1900-01-01 00:00:00.000') AND ISNULL(A.SubmittedBy, '') = ''AND ISNULL(B.SubmittedBy, '') != '' AND A.AddedBy = '" + HttpContext.Current.Session["userid"].ToString() + "' order by A.CompleteLoading desc", Conn);
                    CompleteLoadPick = CompletedLoadPicked;
                    response[0] = "";
                    response[1] = JsonConvert.SerializeObject(dt11);
                    response[2] = JsonConvert.SerializeObject(completed);
                    response[3] = "success";
                    return response;
                }
                

            }
            catch (Exception ex)
            {
                response[0] = "Error!";
                response[1] = ex.ToString();
                response[2] = "error";
            }
            return response;
        }

        [WebMethod]
        public static string SetSessionValue(string value, string type)
        {
            string username = "";
            string password = "";
            string password2 = "";

            switch (type)
            {
                case "submitwd":
                    HttpContext.Current.Session["WorkDate"] = value;
                    break;

                case "submitwh":
                    HttpContext.Current.Session["Warehouse"] = value;
                    break;
            }
            return type == "submitwd" ? "WorkDate Succesfully Set" : "Warehouse Succesfully Set";

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("../Login/Login.aspx");
        }

        [WebMethod]
        public static Array btnGetEmail_Click()
        {
            object[] arr = new object[4];
            string output;
            string EMAILADD = Common.Common.SystemSetting("EMAILADD", HttpContext.Current.Session["ConnString"].ToString());
            string EMAILADDPASS = Common.Common.SystemSetting("EMAILADDPASS", HttpContext.Current.Session["ConnString"].ToString());
            string EMAILHOST = Common.Common.SystemSetting("EMAILHOST", HttpContext.Current.Session["ConnString"].ToString());
            string EMAILPORT = Common.Common.SystemSetting("EMAILPORT", HttpContext.Current.Session["ConnString"].ToString());

            arr[0] = Gears.Decrypt(EMAILADD, "9GearsMeUp9");
            arr[1] = Gears.Decrypt(EMAILADDPASS, "9GearsMeUp9");
            arr[2] = EMAILHOST;
            arr[3] = EMAILPORT;

            return arr;
        }

        protected string GenIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void CheckUserStatus()
        {

            //gen ip address
            Session["ipaddress"] = GenIPAddress();

            //check user id status
            DataTable checkuseridstatus = Gears.RetriveData2("Select * from " + Request.QueryString["schemaname"].ToString() + ".Login_Validator where userid= '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());

            //if existing user ID
            if (checkuseridstatus.Rows.Count > 0)
            {
                //if naka logged pa siya di pa nakalogout
                if (checkuseridstatus.Rows[0][2].ToString() == "True")
                {
                    //if same IP
                    if (checkuseridstatus.Rows[0][3].ToString() == Session["ipaddress"].ToString())
                    {

                        string token = Guid.NewGuid().ToString();

                        HttpContext.Current.Session.Remove("token");

                        //update
                        Gears.RetriveData2("UPDATE " + Request.QueryString["schemaname"].ToString() + ".Login_Validator SET sessionid= '" + token + "' " +
                            "WHERE userid= '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());

                        HttpContext.Current.Session["token"] = token;
                    }
                    //if not same IP and is Logged = 1
                    else
                    {
                        HttpContext.Current.Session.Remove("token");
                        //gen token
                        string token = Guid.NewGuid().ToString();

                        //update
                        Gears.RetriveData2("UPDATE " + Request.QueryString["schemaname"].ToString() + ".Login_Validator SET sessionid= '" + token + "',ipaddress= '" + Session["ipaddress"].ToString() + "'" +
                                "WHERE userid= '" + Session["userid"].ToString() + "' ", Session["ConnString"].ToString());

                        //session new token
                        HttpContext.Current.Session["token"] = token;
                    }
                }
                //if hindi naka logged update and gen ng token
                else
                {
                    string token = Guid.NewGuid().ToString();

                    //update
                    Gears.RetriveData2("UPDATE " + Request.QueryString["schemaname"].ToString() + ".Login_Validator SET sessionid= '" + token + "',isLogged = '1', ipaddress = '" + Session["ipaddress"].ToString() + "' " +
                                       "WHERE userid= '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());

                    //add to session the token
                    HttpContext.Current.Session["token"] = token;
                }
            }
            else
            {
                //if di existing add sa database
                string token = Guid.NewGuid().ToString();

                Gears.RetriveData2("INSERT INTO " + Request.QueryString["schemaname"].ToString() + ".Login_Validator (userid,sessionid,isLogged,ipaddress)" +
                        "VALUES ('" + Session["userid"].ToString() + "' ," +
                        "'" + token + "' ," +
                        "'1' ," +
                        "'" + Session["ipaddress"].ToString() + "')"
                        , Session["ConnString"].ToString());

                //session
                HttpContext.Current.Session["token"] = token;


            }
        }


        [WebMethod]
        public static string ChangePassword(string OldPass, string NewPass)
        {
            string username = "";
            string password = "";
            string password2 = "";
            string passChange = "";

            DataTable Dtgetname = Gears.RetriveData2("Select username,password from " + HttpContext.Current.Session["schemaname"] + ".users where userid = '" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            foreach (DataRow dtrow in Dtgetname.Rows)
            {
                username = dtrow[0].ToString();
                password = dtrow[1].ToString();
            }
            password2 = Gears.PasswordEncrypt(username, OldPass.ToString());
            passChange = Gears.PasswordEncrypt(username, NewPass.ToString());
            if (password == password2)
            {
                Gears.RetriveData2("Update " + HttpContext.Current.Session["schemaname"] + ".users set password = '" + passChange + "' where userid = '" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
                Gears.RetriveData2("Update GEARS.Users SET Password = '" + passChange + "' where UserID = '" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
                return "Succesfully Change!!!";
            }
            else
            {
                return "Password Not Match!!!";
            }
                

        }

        [WebMethod]
        public static string UpdateEmail(string Email, string Password, string Host, string Port)
        {
            if (EmailIsValid(Email))
            {
                string ForEmail2 = "";
                string ForPass2 = "";
                string EmailPortt = "";
                string EmailHostt = "";

                ForEmail2 = Gears.Encrypt(Email.ToString(), "9GearsMeUp9");
                ForPass2 = Gears.Encrypt(Password.ToString(), "9GearsMeUp9");
                EmailPortt = Port.ToString();
                EmailHostt = Host.ToString();

                Gears.RetriveData2("Update " + HttpContext.Current.Session["schemaname"].ToString() + ".SystemSettings set Value = '" + ForEmail2 + "' where Code = 'EMAILADD'", HttpContext.Current.Session["ConnString"].ToString());
                Gears.RetriveData2("Update " + HttpContext.Current.Session["schemaname"].ToString() + ".SystemSettings set Value = '" + ForPass2 + "' where Code = 'EMAILADDPASS'", HttpContext.Current.Session["ConnString"].ToString());
                Gears.RetriveData2("Update " + HttpContext.Current.Session["schemaname"].ToString() + ".SystemSettings set Value = '" + EmailPortt + "' where Code = 'EMAILPORT'", HttpContext.Current.Session["ConnString"].ToString());
                Gears.RetriveData2("Update " + HttpContext.Current.Session["schemaname"].ToString() + ".SystemSettings set Value = '" + EmailHostt + "' where Code = 'EMAILHOST'", HttpContext.Current.Session["ConnString"].ToString());

            return "Update Success";
            }
            else
            {
                return "Invalid Email Address";
            }
        }

        protected void gvMessage_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Date"] = Convert.ToString(DateTime.Now);
        }
        static Regex ValidEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        private static bool EmailIsValid(string Email)
        {
            bool isValid = (!string.IsNullOrEmpty(Email)) && ValidEmailRegex.IsMatch(Email);

            return isValid;
        }

        [WebMethod]
        public static string getBookDate()
        {
            DataTable dtgt = new DataTable();
            string jsondata;

            dtgt = Gears.RetriveData2("execute sp_SystemSetting 'GET','BOOKDATE','N/A'", HttpContext.Current.Session["ConnString"].ToString());

            jsondata = JsonConvert.SerializeObject(dtgt);
            return jsondata;
        }
    }
}