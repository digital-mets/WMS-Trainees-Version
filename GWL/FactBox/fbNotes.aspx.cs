using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
using System.Data;

namespace GWL.FactBox
{
    public partial class fbNotes : System.Web.UI.Page
    {
        string Message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string docnumber = Request.QueryString["docnumber"];
            DataTable Notes = Gears.RetriveData2("Select * from IT.Notes where Docnumber = '"+docnumber+"' order by DateTime desc", Session["ConnString"].ToString());
            try
            {

                foreach (DataRow dt in Notes.Rows)
                {
                    Message += "<tr><td><font name='Microsoft Sans Serif' size=2>From: " + dt[5].ToString() +
                             "<br>To: " + dt[6].ToString() +
                             "<br>Date: " + dt[3].ToString() +
                             "<br>" + dt[4].ToString() + "</font></td></tr>";
                }
            }
            catch (Exception)
            {
                return;
            }

            litText.Text = "<table class='TFtable'>" + Message + "</table>";
        }

        protected void callbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string strType = "";
            if (ToUser.Text == "Don't Notify")
            {
                strType = "0";
            }
            else if (ToUser.Text == "Notify")
            {
                strType = "1";
            }
            else
            {
                strType = "3";
            }
            Gears.RetriveData2(string.Format("Insert into IT.Notes(TransType,DocNumber,DateTime,Message,FromUser,ToUser,Notify,Type) "+
                "values('{0}','{1}',GETDATE(),'{2}','{3}','{4}','{5}',{6})", 
                 Request.QueryString[1], Request.QueryString[0], Memo.Text, Session["userid"], ToUser.Text, Type.Value.ToString(),strType)
                , Session["ConnString"].ToString());

        }
    }
}