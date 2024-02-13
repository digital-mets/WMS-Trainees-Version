using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
using System.Data;
using System.Configuration;

namespace GWL
{
    public partial class PerformSender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        [WebMethod]
        [ScriptMethod]
        public static bool perfsend(string ModuleID,string Entry,string Pkey,string Interval)//Refresh method of for translist
        {
            if (HttpContext.Current.Session["ConnString"] != null)
            {
                Gears.RetriveData2(string.Format("Insert into IT.Perfmon(UserID,DateTime,ModuleID,EntryMode,Pkey,Interval) "+
                                   "values ('{0}',GETDATE(),'{1}','{2}','{3}',{4})", 
                    HttpContext.Current.Session["userid"],ModuleID,Entry,Pkey,Interval), HttpContext.Current.Session["ConnString"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }


        [WebMethod]
        [ScriptMethod]
        public static string SS(string Code)//Refresh method of for translist
        {
            if (HttpContext.Current.Session["ConnString"] != null)
            {
                string schemaname = "GEARS";
                if (HttpContext.Current.Session["schemaname"] != null)
                {
                    schemaname = HttpContext.Current.Session["schemaname"].ToString();
                }

                DataTable dtbl = Gears.RetriveData2("SELECT VALUE FROM IT.Systemsettings WHERE Code= '" + Code + "'", HttpContext.Current.Session["ConnString"].ToString());

                if (dtbl.Rows.Count > 0)
                {
                    return dtbl.Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}