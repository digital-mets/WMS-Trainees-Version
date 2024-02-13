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
    public partial class frmSchedule : System.Web.UI.Page
    {
        public class Schedule
        {
            public string ScheduleCode { get; set; }
            public string TeamCode { get; set; }
            public string Date { get; set; }
            public string ShiftCode { get; set; }
            public string EmployeeCode { get; set; }

            public string Team { get; set; }
            public string FullName { get; set; }
            public string Color { get; set; }
            public string Description { get; set; }
            public string NoWorkingDay { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }

        }
        public static DataTable TableShift = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["ConnString"] as string) && string.IsNullOrEmpty(Session["CompSes"] as string))
            {
                Response.Redirect("~/Login/Index.aspx");
            }

            if (!IsPostBack)
            {
                GetAllData();
            }
        }

        #region Get Data
        private void GetAllData()
        {
            //TableTeam = Gears.RetriveData2("SELECT TeamCode,Team FROM " + HttpContext.Current.Session["CompSes"] + ".Teams", HttpContext.Current.Session["ConnString"].ToString());
            TableShift = Gears.RetriveData2(
                "SELECT '01' ShiftMaintenanceCode, '7700031 Bossing Regular' AS Shift, '#228B22' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '02' ShiftMaintenanceCode, '7700032 Bossing Jumbo' AS Shift, '#FA8072' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '03' ShiftMaintenanceCode, '7700033 Bossing Classic' AS Shift, '#87CEFA	' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '04' ShiftMaintenanceCode, '7700034 Pinoy Regular' AS Shift, '#FF7F50' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '05' ShiftMaintenanceCode, '7700035 Pinoy Jumbo' AS Shift, '#FF6347' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '06' ShiftMaintenanceCode, '7700036 Pinoy Regular' AS Shift, '#FF4500' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '07' ShiftMaintenanceCode, '7700037 Bossing Cheedog' AS Shift, '#FFD700' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '08' ShiftMaintenanceCode, '7700038 Bossing Regular' AS Shift, '#FFA500' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '09' ShiftMaintenanceCode, '7700039 Bossing Classic' AS Shift, '#FF8C00' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '10' ShiftMaintenanceCode, '7700040 Bossing Jumbo' AS Shift, '#F08080' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '11' ShiftMaintenanceCode, '7700041 Bossing Cheese' AS Shift, '#CD5C5C' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '12' ShiftMaintenanceCode, '7700042 Bossing Chicken' AS Shift, '#DC143C' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '13' ShiftMaintenanceCode, '7700043 Bossing Classic' AS Shift, '#B22222' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '14' ShiftMaintenanceCode, '7700044 Bossing Regular' AS Shift, '#FF6347' Color, '' AS NoWorkingDay UNION ALL " +
                "SELECT '15' ShiftMaintenanceCode, '7700045 Bossing Hotdog' AS Shift, '#DB7093	' Color, '' AS NoWorkingDay  "
                , HttpContext.Current.Session["ConnString"].ToString());
        }
        [WebMethod]
        public static List<Schedule> GetDataTeam()
        {
            List<Schedule> details = new List<Schedule>();
            using (SqlConnection con = new SqlConnection(HttpContext.Current.Session["ConnString"].ToString()))
            {
                con.Open();

                string query = "SELECT '101' TeamCode,'Grinder' Team UNION ALL " +
                    "SELECT '102' TeamCode,'Mixer' Team  UNION ALL " +
                    "SELECT '103' TeamCode,'Chopping' Team  UNION ALL " +
                    "SELECT '104' TeamCode,'Smoke House' Team  UNION ALL " +
                    "SELECT '105' TeamCode,'Packaging' Team ";
                    ;

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    details.Add(new Schedule
                    {
                        TeamCode = reader["TeamCode"].ToString(),
                        Team = reader["Team"].ToString(),
                    });
                }
                return details;
            }
        }
        [WebMethod]
        public static List<Schedule> GetDataEmpUnder()
        {
            List<Schedule> details = new List<Schedule>();
            using (SqlConnection con = new SqlConnection(HttpContext.Current.Session["ConnString"].ToString()))
            {
                con.Open();
                string query = "SELECT '101' TeamCode, 'Grinder 1' AS FullName, '101' EmployeeCode " +
                    " UNION ALL SELECT '101' TeamCode, 'Grinder 2' AS FullName, '101' EmployeeCode " +
                    " UNION ALL SELECT '101' TeamCode, 'Grinder 3' AS FullName, '101' EmployeeCode " +
                    " UNION ALL SELECT '101' TeamCode, 'Grinder 4' AS FullName, '101' EmployeeCode " +
                    " UNION ALL SELECT '101' TeamCode, 'Grinder 5' AS FullName, '101' EmployeeCode " +
                    " UNION ALL SELECT '101' TeamCode, 'Grinder 6' AS FullName, '101' EmployeeCode " +

                   " UNION ALL SELECT '102' TeamCode, 'Mixer 1' AS FullName, '102' EmployeeCode" +
                " UNION ALL SELECT '102' TeamCode, 'Mixer 2' AS FullName, '102' EmployeeCode" +
                 " UNION ALL SELECT '102' TeamCode, 'Mixer 3' AS FullName, '102' EmployeeCode" +
                  " UNION ALL SELECT '102' TeamCode, 'Mixer 4' AS FullName, '102' EmployeeCode" +

                " UNION ALL SELECT '103' TeamCode, 'Chopper 1' AS FullName, '102' EmployeeCode" +
                " UNION ALL SELECT '103' TeamCode, 'Chopper 2' AS FullName, '102' EmployeeCode" +
                " UNION ALL SELECT '104' TeamCode, 'Smokehouse 1' AS FullName, '102' EmployeeCode" +
                   " UNION ALL SELECT '104' TeamCode, 'Smokehouse 2' AS FullName, '102' EmployeeCode" +
                      " UNION ALL SELECT '104' TeamCode, 'Smokehouse 3' AS FullName, '102' EmployeeCode" +
                         " UNION ALL SELECT '104' TeamCode, 'Smokehouse 4' AS FullName, '102' EmployeeCode" +
                            " UNION ALL SELECT '104' TeamCode, 'Smokehouse 5' AS FullName, '102' EmployeeCode" +
                " UNION ALL SELECT '105' TeamCode, 'Packing' AS FullName, '102' EmployeeCode";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    details.Add(new Schedule
                    {
                        TeamCode = reader["TeamCode"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                    });
                }
                return details;
            }
        }

        [WebMethod]
        public static List<Schedule> GetShiftPerTeam(Schedule objCutOff)
        {
            List<Schedule> details = new List<Schedule>();
            using (SqlConnection con = new SqlConnection(HttpContext.Current.Session["ConnString"].ToString()))
            {
                con.Open();
                //string query = "SELECT A.ScheduleCode,A.TeamCode,A.Date,A.ShiftCode,A.EmployeeCode, B.Color,B.Description, " +
                //               "(CASE WHEN convert(varchar(10),NoWorkingDay) like " +
                //               "'%' + convert(varchar(10), DATEPART(weekday, A.date)) + '%' THEN 1 ELSE 0 END) as NoWorkingDay " +
                //               "FROM " + HttpContext.Current.Session["CompSes"] + ".Schedules A " +
                //               "INNER JOIN " + HttpContext.Current.Session["CompSes"] + ".ShiftMaintenance B " +
                //               "ON A.ShiftCode = B.ShiftMaintenanceCode WHERE (A.ShiftCode IS NOT NULL OR ISNULL(A.ShiftCode, '') ='') " +
                //               "AND CAST(A.Date AS DATE) BETWEEN  '" + objCutOff.StartDate + "' AND '" + objCutOff.EndDate + "' ";

                string query = "SELECT  101 ScheduleCode,'Grinder' TeamCode,'5/1/2021' Date,'Hotdog' ShiftCode,101 EmployeeCode, 'Green' Color,'Hotdog' Description,0 as NoWorkingDay Union all " +
                              "SELECT  102 ScheduleCode,'Mixing' TeamCode,'5/1/2021' Date,'Hotdog' ShiftCode,102 EmployeeCode, 'Green' Color,'Hotdog' Description,0 as NoWorkingDay  ";
          



                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    details.Add(new Schedule
                    {
                        ScheduleCode = reader["ScheduleCode"].ToString(),
                        TeamCode = reader["TeamCode"].ToString(),
                        Date = reader["Date"].ToString(),
                        ShiftCode = reader["ShiftCode"].ToString(),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        Color = reader["Color"].ToString(),
                        Description = reader["Description"].ToString(),
                        NoWorkingDay = reader["NoWorkingDay"].ToString(),
                    });
                }
                return details;
            }
        }
        #endregion

        [WebMethod]
        public static string RemoveShift(Schedule objShift)
        {
            DataTable checkDoubleEntry = new DataTable();
            try
            {
                Gears.RetriveData2("UPDATE " + HttpContext.Current.Session["CompSes"] + ".Schedules SET ShiftCode = 'NULL' WHERE ScheduleCode = '" + objShift.ScheduleCode + "'",
                                    HttpContext.Current.Session["ConnString"].ToString());

            }
            catch (Exception e)
            {
                return "Error RemoveShift:" + e;
            }
            return "Data Successfully Updated";
        }

        #region Action
        [WebMethod]
        public static string SaveSchedule(List<Schedule> objShift)
        {
            DataTable checkEntry = new DataTable();
            string docnumber = "";
            List<Schedule> retrieveSchedule = objShift;
            try
            {
                for (int i = 0; i <= retrieveSchedule.Count; i++)
                {
                    checkEntry = Gears.RetriveData2("SELECT ScheduleCode FROM " + HttpContext.Current.Session["CompSes"] + ".Schedules " +
                                       "WHERE TeamCode = '" + retrieveSchedule[i].TeamCode + "' AND Date = '" + retrieveSchedule[i].Date +
                                       "' AND EmployeeCode =  '" + retrieveSchedule[i].EmployeeCode + "' ",
                                       HttpContext.Current.Session["ConnString"].ToString());
                    if (checkEntry.Rows.Count > 0)
                    {
                        Gears.RetriveData2("UPDATE " + HttpContext.Current.Session["CompSes"] + ".Schedules " +
                                           "SET ShiftCode = '" + retrieveSchedule[i].ShiftCode + "' " +
                                           "WHERE TeamCode = '" + retrieveSchedule[i].TeamCode + "' AND Date = '" + retrieveSchedule[i].Date +
                                           "' AND EmployeeCode =  '" + retrieveSchedule[i].EmployeeCode + "' ",
                                           HttpContext.Current.Session["ConnString"].ToString());

                    }
                    else
                    {
                        docnumber = GetDocCode();
                        Gears.RetriveData2("INSERT INTO " + HttpContext.Current.Session["CompSes"] + ".Schedules " +
                                           "(ScheduleCode,TeamCode,Date,ShiftCode,EmployeeCode) SELECT '" + docnumber + "','" +
                                           retrieveSchedule[i].TeamCode + "','" + retrieveSchedule[i].Date + "','" +
                                           retrieveSchedule[i].ShiftCode + "','" + retrieveSchedule[i].EmployeeCode + "' ",
                                           HttpContext.Current.Session["ConnString"].ToString());
                        AddNewDocCode();
                    }
                }
            }
            catch (Exception e)
            {
                return "Error SaveRoute = " + e;
            }
            return "Data Successfully Save";
        }
        #endregion
        private static string GetDocCode()
        {
            string docnumber;
            DataTable count = Gears.RetriveData2("SELECT Prefix + RIGHT(REPLICATE('0',SeriesWidth)+SeriesNumber,SeriesWidth) NewDocnumber " +
                                                 "FROM " + HttpContext.Current.Session["CompSes"] + ".DocNumberSettings WHERE Module='SCHED' ",
                                                 HttpContext.Current.Session["ConnString"].ToString());
            docnumber = count.Rows[0][0].ToString();
            return docnumber;
        }
        private static void AddNewDocCode()
        {
            try
            {
                Gears.RetriveData2("DECLARE @CurrentSeries int = (SELECT SeriesNumber FROM " + HttpContext.Current.Session["CompSes"] + ".DocNumberSettings WHERE Module='SCHED') " +
                                   "UPDATE " + HttpContext.Current.Session["CompSes"] + ".DocNumberSettings SET SeriesNumber=@CurrentSeries+1 WHERE Module='SCHED' " +
                                   "SELECT * FROM " + HttpContext.Current.Session["CompSes"] + ".DocNumberSettings", HttpContext.Current.Session["ConnString"].ToString());
            }
            catch (Exception)
            {

            }
        }
    }
}