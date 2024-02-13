using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using GearsLibrary;
using System.Configuration;
using Entity;

namespace GWL.IT
{
    public partial class frmManningSchedule : System.Web.UI.Page
    {
        string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Webmethod

        [WebMethod]
        public static string YearParameter()
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("SELECT DISTINCT Year FROM Production.CounterPlan A WHERE ISNULL(FinalizedBy, '') != ''", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string WeekParameter()
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("SELECT DISTINCT WorkWeek FROM Production.CounterPlan A WHERE ISNULL(FinalizedBy, '') != ''", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string DayNoParameter(ManningSchedule _ms)
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("SELECT DISTINCT A.DayNo " +
                                                "FROM Production.CapacityPlanning A " +
                                                "INNER JOIN Production.CapacityPlanningManpowerSpecific B " +
                                                "ON A.Year = B.Year AND A.WorkWeek = B.WorkWeek " +
                                                "WHERE Status IN ('F', 'S') AND A.Year = " + _ms.Year + " AND A.WorkWee = " + _ms.WorkWeek + " " +
                                                "ORDER BY A.DayNo DESC", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string SP_Call(ManningSchedule _ms)
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;
            string Dayno = _ms.DayNo == "" ? "" : _ms.DayNo;
            string Action = _ms.Action == "" ? "" : _ms.Action;
            string Filter = _ms.Filter == "" ? "" : _ms.Filter;
            string Userid = HttpContext.Current.Session["userid"].ToString();
            string Generate = _ms.Generate == "" ? "0" : _ms.Generate;
            string Shift = _ms.Shift == "" ? "" : _ms.Shift;
            string RecordID = _ms.RecordID == "" ? "" : _ms.RecordID;
            string Value = _ms.Value == "" ? "" : _ms.Value;

            DataTable dt = Gears.RetriveData2("EXEC sp_ManningSchedule '" + Action + "', '" + _ms.Year + "', '" + _ms.WorkWeek + "', '" + Dayno + "', '" + Filter + "', '" + Userid + "', '" + Generate + "', '" + Shift + "', '" + RecordID + "', '" + Value + "'", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static void Update(ManningSchedule _ms) {

            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            string[] ID = _ms.RecordID.Split('|');

            Gears.RetriveData2("UPDATE Production.ManningScheduleDetail SET ProcessCode='"+_ms.Position+ "', DPlan=" + _ms.DirectPlan + ", DActual=" + _ms.DirectActual + 
                               ", DPercentage=" + _ms.DirectPercent + ", APlan=" + _ms.AgencyPlan + ", AActual=" + _ms.AgencyActual + ", APercentage=" + _ms.AgencyPercent + 
                               " WHERE RecordID = '" + ID[1] + "'; " +
                               "UPDATE Production.ManningSchedule SET LastEditedBy='" + HttpContext.Current.Session["userid"].ToString() + "', LastEditedDate = GETDATE() WHERE DocNumber = '" + ID[0] + "'", Conn);
        }

        #endregion
    }
}