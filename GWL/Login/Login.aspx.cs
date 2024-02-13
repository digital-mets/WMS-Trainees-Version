using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using GearsLibrary;
using System.Data;

public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {


            HttpCookie cookie = Request.Cookies["ASP.NET_SessionId"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie);
            }
            
        }

    }

    static Regex ValidEmailRegex = CreateValidEmailRegex();

    private static Regex CreateValidEmailRegex()
    {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
    }
    private static bool EmailIsValid(string username)
    {
      
        bool isValid = (!string.IsNullOrEmpty(username));
        return isValid;
    }

    static Random random = new Random();
    public static string GetRandomHexNumber(int digits)
    {
        byte[] buffer = new byte[digits / 2];
        random.NextBytes(buffer);
        string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
        if (digits % 2 == 0)
            return result;
        return result + random.Next(16).ToString("X");
    }
    protected void submit_ServerClick(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

            string user = username.Value.ToString().Replace("'", "r");


            if (EmailIsValid(user))
            {
                string usern = username.Value;
              
                string pass = Uri.UnescapeDataString(password2.Value.ToString());
             
                GearsLibrary.Gears.CompanyList();
              

                if (usern != null && pass != null)
                {
                    try
                    {
                       

                        
                        Session["ConnString"] = "Data Source=192.168.180.22;Initial Catalog=WMS-TEST;Persist Security Info=True;User ID=sa;Password=mets123*;";
                       
                        string connect = Session["ConnString"].ToString();
                        Gears.UseConnectionString(connect);
                        DataTable dtuser = Gears.RetriveData2("select userid from IT.users where username = '" + usern + "' and Password = '" + pass + "'", connect);
                        if (dtuser.Rows.Count == 0)
                        {
                            Valid.ForeColor = System.Drawing.Color.Red;
                            Valid.Text = "Please check your login credentials";
                            Error.ShowOnPageLoad = true;
                            return;
                        }
                        else
                        {
                            foreach (DataRow dtRow in dtuser.Rows)
                            {
                                user = dtRow["userid"].ToString();
                            }
                        }

                    }
                    catch (Exception)
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

                        Valid.ForeColor = System.Drawing.Color.Red;
                        Valid.Text = "Please check your login credentials";
                        Error.ShowOnPageLoad = true;
                        return;
                    }
                }

               

                Session["login"] = "set";

                Session["userid"] = user;

                Session["username"] = usern;

                Session["CompDomain"] = "GWL-METS";

                DataTable dtwh = Gears.RetriveData2("SELECT CompanyCode FROM IT.Users  WHERE UseriD='" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());

                if (dtwh.Rows.Count == 1)
                {
                    Session["Warehouse"] = dtwh.Rows[0][0].ToString();
                }

                Session["WHCode"] = dtwh.Rows[0][0].ToString();
                Session["WorkDate"] = DateTime.Today.ToShortDateString();

                Response.Redirect("../MainMenu/GEARSMainMenu.aspx?schemaname=IT");
                // }

             


                //AC START GEN IP ADDRESS
                CheckUserStatus();
                //AC END GEN IP ADDRESS



            }
        }
    }


    //GEN IP ADDRESS
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

    //GEN IP ADDRESS

    //AC START GENERATE IP ADDRESS AND CHECK IF EXISTING SESSION
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
    //AC START GENERATE IP ADDRESS AND CHECK IF EXISTING SESSION

}