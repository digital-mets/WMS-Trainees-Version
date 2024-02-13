using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using DevExpress.Web;

    namespace GWL {
        public class Global_asax : System.Web.HttpApplication {
            void Application_Start(object sender, EventArgs e) {
                DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            }

            void Application_End(object sender, EventArgs e) {
                //// Code that runs on application shutdown
                //if(Context.Response.StatusCode== 404)
                //{
                //    HttpContext.Current.RewritePath("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax");
                //}
            }

            void Application_Error(object sender, EventArgs e)
            {
                // Code that runs when an unhandled error occurs.

                // Get last error from the server
                Exception exc = Server.GetLastError();

                if (exc is HttpUnhandledException)
                {
                    if (exc.InnerException != null)
                    {
                        string a = exc.Message.ToString();
                        exc = new Exception(exc.InnerException.Message);
                       /// try{
                        //Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",true);
                        HttpContext.Current.RewritePath("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax");
                        //}
                          //  catch
                        //{

                        //}
                    }
                }

                if (exc is HttpException)
                {
                    if (((HttpException)exc).GetHttpCode() == 404)
                    {

                        // Log if wished.
                        Server.ClearError();
                        Server.Transfer("NOT_FOUND_PAGE_LOCATION", false);
                        return;
                    }
                }
            }

            void Session_Start(object sender, EventArgs e) {
                // Code that runs when a new session is started
            }

            void Session_End(object sender, EventArgs e) {
                // Code that runs when a session ends. 
                // Note: The Session_End event is raised only when the sessionstate mode
                // is set to InProc in the Web.config file. If session mode is set to StateServer 
                // or SQLServer, the event is not raised.
            }

        }
    }