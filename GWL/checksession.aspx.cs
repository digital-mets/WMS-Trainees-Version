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

namespace GWL
{
    public partial class checksession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable gettablecount = Gears.RetriveData2("select COUNT(*) from it.UserTrail  where TransType = '"
                + HttpContext.Current.Session["transtype"] + "'", Session["ConnString"].ToString());
            foreach(DataRow dtrow in gettablecount.Rows){
                Session["tablecount"] = dtrow[0].ToString();
            }
        }

        //[WebMethod]
        //[ScriptMethod]
        //public static bool check()//Refresh checker method
        //{
        //    if (HttpContext.Current.Session["userid"] == null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        [WebMethod]
        [ScriptMethod]
        public static string check()//Refresh checker method
        {

            //2020-02-21 AC START Handling of Multiple Login and Check Userid session
            string Status = "";

            if (HttpContext.Current.Session["ConnString"] == null || HttpContext.Current.Session["token"] == null || HttpContext.Current.Session["userid"] == null)
            {
                HttpContext.Current.Session.Remove("token");
                Status = "isExpired";
            }
            else
            {
                ////check user id status
                //DataTable checkuseridstatus = Gears.RetriveData2("Select sessionid from Login_Validator where userid= '" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());

                //DataTable usertype = Gears.RetriveData2("Select UserType from IT.Users where userid= '" + HttpContext.Current.Session["userid"].ToString() + "' AND ISNULL(UserType,'USER')!='DEV'", HttpContext.Current.Session["ConnString"].ToString());

                //if (usertype.Rows.Count > 0)
                //{

                //    if (HttpContext.Current.Session["userid"] == null)
                //    {
                //        HttpContext.Current.Session.Remove("token");
                //        Status = "isExpired";
                //    }
                //    else if (checkuseridstatus.Rows[0][0].ToString() != HttpContext.Current.Session["token"].ToString())
                //    {
                //        HttpContext.Current.Session.Remove("token");
                //        Status = "isUsed";
                //    }

                //}

                if (HttpContext.Current.Session["userid"] == null)
                {
                    HttpContext.Current.Session.Remove("token");
                    Status = "isExpired";
                }
            }
            return Status;

            //2020-02-21 AC END Handling of Multiple Login and Check Userid session
        }

        [WebMethod]
        [ScriptMethod]
        public static bool refresh()//Refresh method of for translist
        {
            if (HttpContext.Current.Session["userid"] != null)
            {
                string transtype = HttpContext.Current.Session["transtype"] == null ? "" : HttpContext.Current.Session["transtype"].ToString();
                string transtype2 = HttpContext.Current.Session["transtype2"] == null ? "" : HttpContext.Current.Session["transtype2"].ToString();

                DataTable gettablecount = Gears.RetriveData2("select COUNT(*) from it.UserTrail  where TransType = '"
                    + HttpContext.Current.Session["transtype"] + "' and userid = '" + HttpContext.Current.Session["userid"] + "'", HttpContext.Current.Session["ConnString"].ToString());

                foreach (DataRow dtrow in gettablecount.Rows)
                {
                    HttpContext.Current.Session["tablecount"] = dtrow[0].ToString();
                }

                string tablecount = HttpContext.Current.Session["tablecount"] == null ? "" : HttpContext.Current.Session["tablecount"].ToString();
                string tablecount2 = HttpContext.Current.Session["tablecount2"] == null ? "" : HttpContext.Current.Session["tablecount2"].ToString();

                if ((tablecount != tablecount2 && tablecount2 != "") && transtype == transtype2)
                {
                    HttpContext.Current.Session["tablecount2"] = HttpContext.Current.Session["tablecount"].ToString();
                    return true;
                }
                else
                {
                    HttpContext.Current.Session["tablecount2"] = HttpContext.Current.Session["tablecount"].ToString();
                    HttpContext.Current.Session["transtype2"] = HttpContext.Current.Session["transtype"].ToString();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string checkcon()//Refresh checker method
        {
          return HttpContext.Current.Session["ConnString"].ToString();
        }
    }
}