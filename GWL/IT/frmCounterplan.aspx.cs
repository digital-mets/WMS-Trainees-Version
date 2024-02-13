using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GearsLibrary;
using System.IO;
using System.Data.OleDb;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace GWL.IT
{
    public partial class frmCounterplan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        [WebMethod]
        public static string[] Parameter(string UserID)
        {
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT DISTINCT ProductionSite from Production.MaterialOrder WHERE SubmittedDate is not null ORDER BY 1 DESC", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT DISTINCT Year from Production.MaterialOrder WHERE SubmittedDate is not null ORDER BY 1 DESC", HttpContext.Current.Session["ConnString"].ToString());
            output[1] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT DISTINCT WorkWeek from Production.MaterialOrder WHERE SubmittedDate is not null ORDER BY 1 DESC", HttpContext.Current.Session["ConnString"].ToString());
            output[2] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] ViewCounterPlan(string ProductionSite, string Year, string WorkWeek, string UserID, string Level, string Params)
        {

            int maxItem = 6;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();


            for (int i = 0; i <= maxItem; i++)
            {
                dtbl = Gears.RetriveData2("exec [dbo].[sp_View_CounterPlan] '" + ProductionSite + "','" + Year + "','" + WorkWeek + "','" + HttpContext.Current.Session["userid"].ToString() + "'," + i.ToString() + ",'" + Params + "'", HttpContext.Current.Session["ConnString"].ToString());
                output[i] = JsonConvert.SerializeObject(dtbl);
            }

            return output;

        }


        [WebMethod]
        public static string[] GenerateCounterPlan(string ProductionSite, string Year, string WorkWeek, string UserID)
        {
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();
            dtbl = Gears.RetriveData2("exec [dbo].[sp_Generate_CounterPlan] '" + ProductionSite + "','" + Year + "','" + WorkWeek + "','" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] Submit(string ProductionSite, string Year, string WorkWeek, string UserID)
        {
            
            string Docnumber = "CP-"+ProductionSite + Year + WorkWeek.PadLeft(2, '0');
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();
            dtbl = Gears.RetriveData2("exec [dbo].[sp_Submit_CounterPlan] '" + Docnumber + "','" + HttpContext.Current.Session["userid"].ToString() + "','1','PRDCTP'", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            return output;
        }


        [WebMethod]
        public static string Save(string ProductionSite, string Year, string WorkWeek, string UserID, string ItemStr)
        {
            string Docnumber = "CP-" + ProductionSite + Year + WorkWeek.PadLeft(2, '0');

            //List<Items> Item = JsonConvert.DeserializeObject<List<Items>>(ItemStr);
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(ItemStr, (typeof(DataTable)));

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].ToString()!="")
                { 
                string str = "UPDATE Production.CounterPlanDetail SET "+
                        " Day1='" + dr[1].ToString() + "', " +
                        " Day2='" + dr[2].ToString() + "', " +
                        " Day3='" + dr[3].ToString() + "', " +
                        " Day4='" + dr[4].ToString() + "', " +
                        " Day5='" + dr[5].ToString() + "', " +
                        " Day6='" + dr[6].ToString() + "', " +
                        " Day7='" + dr[7].ToString() + "' " +
                        " WHERE Docnumber ='" + Docnumber + "' and RecordID='" + dr[0].ToString() + "'";
                Gears.RetriveData2(str, HttpContext.Current.Session["ConnString"].ToString());
                }
                Gears.RetriveData2("UPDATE Production.CounterPlan SET LastEditedBy='" + HttpContext.Current.Session["userid"].ToString() + "', LastEditedDate=GETDATE(), MetricTon='"+ UserID + "' WHERE Docnumber='"+ Docnumber + "'", HttpContext.Current.Session["ConnString"].ToString());



            }



            return "Sucessfully Saved";
        }



        [WebMethod]
        public static string SaveNotes(string ProductionSite, string Year, string WorkWeek, string UserID, string Message)
        {
            string Docnumber = "CP-" + ProductionSite + Year + WorkWeek.PadLeft(2, '0');

          

           
                    string str = " INSERT INTO IT.Notes "+
                                " (TransType, DocNumber, DateTime, Message, FromUser, ToUser) "+
                                " SELECT 'PRDCTP','"+ Docnumber + "',GETDATE(),'"+ Message + "','"+ UserID + "','N/A'";
                    Gears.RetriveData2(str, HttpContext.Current.Session["ConnString"].ToString());


            return "Sucessfully Saved";
        }


        [WebMethod]
        public static string[] ViewNotes(string ProductionSite, string Year, string WorkWeek, string UserID, string Level, string Params)
        {

            int maxItem = 5;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();


            for (int i = 0; i <= maxItem; i++)
            {
                dtbl = Gears.RetriveData2("exec [dbo].[sp_View_CounterPlan] '" + ProductionSite + "','" + Year + "','" + WorkWeek + "','" + HttpContext.Current.Session["userid"].ToString() + "'," + i.ToString() + ",'" + Params + "'", HttpContext.Current.Session["ConnString"].ToString());
                output[i] = JsonConvert.SerializeObject(dtbl);
            }

            return output;

        }
    }
}