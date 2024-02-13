using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;
using GearsLibrary;
using System.Configuration;
using Entity;

namespace GWL.Toll
{
    public partial class frmCapacityPlanningCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string SP_Call(CapacityPlanning _cp)
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;
            string filter = _cp.filter == "" ? "" : _cp.filter;
            string DActual = _cp.DActual == "" ? "" : _cp.DActual;
            string AActual = _cp.AActual == "" ? "" : _cp.AActual;

            DataTable dt = Gears.RetriveData2("EXEC sp_CapacityPlanningCalendar '" + _cp.action + "', '" + filter + "', '" + DActual + "', '" + AActual + "', '" + HttpContext.Current.Session["userid"].ToString() + "'", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string InitialTime()
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='SCALENDAR'", Conn);

            return JsonConvert.SerializeObject(dt);
        }
    }
}