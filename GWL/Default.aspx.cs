using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
using Entity;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Data;

namespace GWL
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
           


                string referer;
                try
                {
                    referer = Request.ServerVariables["http_referer"];
                }
                catch
                {
                    referer = "";
                }

                if (referer == null)
                {
                    Response.Redirect("DefaultSite.aspx");
                }

                //try
                //{
                //    string con = Request.QueryString["val"].ToString();
                //    Session["ConnString"] = Gears.Decrypt(con.Replace(" ", "+"), "mets");
                //    Session["userid"] = Gears.Decrypt(Request.QueryString["userid"].ToString().Replace(" ", "+"), "mets");

                //    // 2020-02-21 AC START Handling of Multiple Login 
                //    CheckUserStatus();
                //    // 2020-02-21 AC END Handling of Multiple Login 
                //}
                //catch (Exception)
                //{
                //    Response.Redirect("DefaultSite.aspx");
                //}
            }

        }

        //Generate Ip Address 
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
            DataTable checkuseridstatus = Gears.RetriveData2("Select * from Login_Validator where userid= '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());

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
                        Gears.RetriveData2("UPDATE Login_Validator SET sessionid= '" + token + "' " +
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
                        Gears.RetriveData2("UPDATE Login_Validator SET sessionid= '" + token + "',ipaddress= '" + Session["ipaddress"].ToString() + "'" +
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
                    Gears.RetriveData2("UPDATE Login_Validator SET sessionid= '" + token + "',isLogged = '1', ipaddress = '" + Session["ipaddress"].ToString() + "' " +
                                       "WHERE userid= '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());

                    //add to session the token
                    HttpContext.Current.Session["token"] = token;
                }
            }
            else
            {
                //if di existing add sa database
                string token = Guid.NewGuid().ToString();

                Gears.RetriveData2("INSERT INTO Login_Validator (userid,sessionid,isLogged,ipaddress)" +
                        "VALUES ('" + Session["userid"].ToString() + "' ," +
                        "'" + token + "' ," +
                        "'1' ," +
                        "'" + Session["ipaddress"].ToString() + "')"
                        , Session["ConnString"].ToString());

                //session
                HttpContext.Current.Session["token"] = token;


            }
        }
        // 2020-02-21 AC END Handling of Multiple Login 
    }
}