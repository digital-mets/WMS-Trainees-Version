using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GWL
{
    public partial class DefaultSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //RA Please change narin yung root master yung sa part ng Logout 
                Response.Redirect("~/Login/Login.aspx");
                //Response.Redirect("http://192.168.201.115");
            }
        }
    }
}