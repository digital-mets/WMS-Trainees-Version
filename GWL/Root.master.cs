using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.Data;
using System.Data;
using GearsLibrary;

namespace GWL {
    public partial class RootMaster : System.Web.UI.MasterPage {
        string username = "";
        string password = "";
        string password2 = "";
        protected void Page_Load(object sender, EventArgs e) {

            try
            {
                if (Session["ConnString"].ToString() == "")
                {
                    // 2020-08-03   TL  Avoid hardcoded IP
                    //Response.Redirect("http://192.168.180.7");
                    Response.Redirect("~/Login/Login.aspx");
                    // 2020-08-03   TL  (End)
                    //   Response.Redirect("~/Login/Login.aspx");
                }
            }
            catch (Exception)
            {
                // 2020-08-03   TL  Avoid hardcoded IP
                //Response.Redirect("http://192.168.180.7");
                Response.Redirect("~/Login/Login.aspx");
                // 2020-08-03   TL  (End)
                // Response.Redirect("~/Login/Login.aspx");
            }



            Gears.UseConnectionString(Session["ConnString"].ToString());

            string username = "";
            string compname = "";

        
            DataTable Dtgetcomp = Gears.RetriveData2("Select Value from it.SystemSettings where code = 'COMPNAME'", Session["ConnString"].ToString());
            foreach(DataRow dtrow in Dtgetcomp.Rows){
                compname = dtrow[0].ToString();
            }
            DataTable Dtgetver = Gears.RetriveData2("Select Value from it.SystemSettings where code = 'VERSION'", Session["ConnString"].ToString());
            foreach (DataRow dtrow in Dtgetver.Rows)
            {
                lblVersion.Text="Version Build: "+dtrow[0].ToString();

            }
            DataTable Dtgetname = Gears.RetriveData2("Select username from it.users where userid = '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
            foreach(DataRow dtrow in Dtgetname.Rows){
                username = dtrow[0].ToString();
            }
            DateTime now = DateTime.Now;
            lblName.Text = compname + " | " + now.ToString("D")+" | "+username;

            if(!IsPostBack)
            {
                Session["WorkDate"] = DateTime.Today.ToShortDateString();
                DataTable dtwarehouse = Gears.RetriveData2("Select Value from it.SystemSettings where code = 'MAINWH'", Session["ConnString"].ToString());
                foreach (DataRow dtrow in dtwarehouse.Rows)
                {

                    Session["Warehouse"] = dtrow[0].ToString();

                }

                //TLAV Added 08/04/2016
                if (Session["PrinterUsed"] == null || Session["PrinterUsed"] == "" || Session["PrinterUsed"] == "NORMAL")
                {
                    Session["PrinterUsed"] = "NORMAL";
                    glPrinter.Value = "NORMAL";
                }
                else
                {
                    Session["PrinterUsed"] = "DOTMATRIX";
                    glPrinter.Value = "DOTMATRIX";
                }
                //TLAV END
                if (Session["userid"].ToString() != "1828")
                    ASPxMenu1.Items.FindByName("message").Visible = false;

                DataTable checkCount = Gears.RetriveData2("Select * from IT.Message", Session["ConnString"].ToString());
                gvMessage.DataSourceID = checkCount.Rows.Count > 0 ? "odsMessage" : "sdsMessage";

            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }

        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            string username = "";
            string password = "";
            string password2 = "";
            switch (e.Parameter)
            {
                case "checkpw":
                    deWDDDate.Value = Convert.ToDateTime(Session["WorkDate"].ToString());
                    CheckPass();
                    break;

                case "confirmpw":
                    CheckPass2();
                    break;

                case "submit":
                    DataTable Dtgetname = Gears.RetriveData2("Select username,password from it.users where userid = '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
                    foreach (DataRow dtrow in Dtgetname.Rows)
                    {
                        username = dtrow[0].ToString();
                        password = dtrow[1].ToString();
                    }
                    if (CheckPass()==true && CheckPass2()==true)
                    {
                        password2 = Gears.PasswordEncrypt(username, txtPass2.Text);
                        Gears.RetriveData2("Update it.users set password = '" + password2 + "' where userid = '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
                        Gears.RetriveData2("exec sp_ConsoUsers '" + Session["userid"].ToString() + "','CHANGE'", Session["ConnString"].ToString());
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;
                case "submitwd":
                    Session["WorkDate"] = deWDDDate.Value;
                    break;

                case "submitwh":
                    Session["Warehouse"] = glWarehouseCode.Text;
                    break;

                case "prntbtn":
                    Session["PrinterUsed"] = glPrinter.Value;
                    break;

                case "gvMes":
                    gvMessage.DataSourceID = "odsMessage";
                    gvMessage.UpdateEdit();
                    break;
            }
        }

        Boolean CheckPass()
        {
            DataTable Dtgetname = Gears.RetriveData2("Select username,password from it.users where userid = '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());
            foreach (DataRow dtrow in Dtgetname.Rows)
            {
                username = dtrow[0].ToString();
                password = dtrow[1].ToString();
            }
            password2 = Gears.PasswordEncrypt(username, txtPass.Text);
            if (password == password2)
            {
                cp.JSProperties["cp_confirm"] = true;
                confirmpass.Text = "Correct Password!";
                confirmpass.ForeColor = System.Drawing.Color.Green;
                return true;
            }
            else
            {
                cp.JSProperties["cp_confirm"] = false;
                confirmpass.Text = "Incorrect Password!";
                confirmpass.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }

        Boolean CheckPass2()
        {
            if (txtPass2.Text != txtPass3.Text)
            {
                cp.JSProperties["cp_confirm2"] = false;
                confirmpass2.Text = "Password does not match!";
                confirmpass2.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else
            {
                cp.JSProperties["cp_confirm2"] = true;
                confirmpass2.Text = "Password match!";
                confirmpass2.ForeColor = System.Drawing.Color.Green;
                return true;
            }
        }

        protected void ASPxMenu1_ItemDataBound(object source, MenuItemEventArgs e)
        {
            string url = "";
            DataTable getVal = Gears.RetriveData2("select Value from it.systemsettings where Code = 'FRESHDESK'",Session["ConnString"].ToString());
            foreach (DataRow dt in getVal.Rows)
            {
                url = dt[0].ToString();
            }
            if (e.Item.Name == "support")
                e.Item.NavigateUrl = url;
        }

        protected void gvMessage_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Date"] = Convert.ToString(DateTime.Now);
        }

 

    }
}