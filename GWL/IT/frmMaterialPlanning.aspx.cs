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
    public partial class frmMaterialPlanning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }


        [WebMethod]
        public static string[] ViewMRP(string ProductionSite, string Year, string WorkWeek, string UserID, string Level, string Params)
        {

            int maxItem = 7;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();


            for (int i = 0; i <= maxItem; i++)
            {
                dtbl = Gears.RetriveData2("exec [dbo].[sp_View_MRP] '" + ProductionSite + "','" + Year + "','" + WorkWeek + "','" + HttpContext.Current.Session["userid"].ToString() + "'," + i.ToString() +",'"+ Params + "'", HttpContext.Current.Session["ConnString"].ToString());
                output[i] = JsonConvert.SerializeObject(dtbl);
            }

            return output;

        }


        [WebMethod]
        public static string[]  GenerateMRP(string ProductionSite, string Year, string WorkWeek, string UserID)
        {
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();
            dtbl = Gears.RetriveData2("exec [dbo].[sp_Generate_MRP] '" + ProductionSite + "','" + Year + "','" + WorkWeek + "','" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            return output;
        }


        [WebMethod]
        public static string[] Parameter(string UserID)
        {
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            dtbl = Gears.RetriveData2("SELECT DISTINCT ProductionSite from Production.CounterPlan WHERE SubmittedDate is not null ORDER BY 1 DESC", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT DISTINCT Year from Production.CounterPlan WHERE SubmittedDate is not null ORDER BY 1 DESC", HttpContext.Current.Session["ConnString"].ToString());
            output[1] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT DISTINCT WorkWeek from Production.CounterPlan WHERE SubmittedDate is not null ORDER BY 1 DESC", HttpContext.Current.Session["ConnString"].ToString());
            output[2] = JsonConvert.SerializeObject(dtbl);

            dtbl = Gears.RetriveData2("SELECT DISTINCT ItemCode,ShortDesc from Masterfile.Item WHERE ISNULL(IsInactive,0)=0 ORDER BY 1 ASC", HttpContext.Current.Session["ConnString"].ToString());
            output[3] = JsonConvert.SerializeObject(dtbl);

            return output;
        }

        [WebMethod]
        public static string[] Submit(string ProductionSite, string Year, string WorkWeek, string UserID)
        {
           ;
            string Docnumber = ProductionSite + Year + WorkWeek.PadLeft(2, '0');
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();
            dtbl = Gears.RetriveData2("exec [dbo].[sp_Submit_MRP] '" + Docnumber + "','" + HttpContext.Current.Session["userid"].ToString() + "','1','PRDMRP'", HttpContext.Current.Session["ConnString"].ToString());
            output[0] = JsonConvert.SerializeObject(dtbl);

            return output;
        }


        [WebMethod]
        public static string[] ExtractMRPDetail(string ProductionSite, string Year, string WorkWeek, string UserID)
        {
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();
            string Docnumber = ProductionSite + Year + WorkWeek.PadLeft(2, '0');

            for (int i = 0; i <= maxItem; i++)
            {
                dtbl = Gears.RetriveData2("exec [dbo].[sp_extract_MRP] '"+ Docnumber + "','" + i.ToString() + "','" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
                output[i] = JsonConvert.SerializeObject(dtbl);
            }

            return output;
        }


        [WebMethod]
        public static string[] GeneratePR(string ProductionSite, string Year, string WorkWeek, string UserID, string Param)
        {
            int maxItem = 3;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();
            string Docnumber = ProductionSite + Year + WorkWeek.PadLeft(2, '0');

            
                dtbl = Gears.RetriveData2("exec [dbo].[sp_GeneratePR_MRP] '" + Docnumber + "','" + Param.ToString() + "','" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
                output[0] = JsonConvert.SerializeObject(dtbl);
            

            return output;
        }


        [WebMethod]
        public static string SaveNotes(string ProductionSite, string Year, string WorkWeek, string UserID, string Message)
        {
            string Docnumber = "" + ProductionSite + Year + WorkWeek.PadLeft(2, '0');




            string str = " INSERT INTO IT.Notes " +
                        " (TransType, DocNumber, DateTime, Message, FromUser, ToUser) " +
                        " SELECT 'PRDMRP','" + Docnumber + "',GETDATE(),'" + Message + "','" + UserID + "','N/A'";
            Gears.RetriveData2(str, HttpContext.Current.Session["ConnString"].ToString());


            return "Sucessfully Saved";
        }


        [WebMethod]
        public static string Item(string ProductionSite, string Year, string WorkWeek, string UserID, string olditemcode, string itemcode, string qty, string type)
        {
            string Docnumber = "" + ProductionSite + Year + WorkWeek.PadLeft(2, '0');


            string str = "";
            if (type == "Add")
            {

                if (olditemcode == "")
                {
                    str = " INSERT INTO Production.MRPRawMaterial " +
                " (Docnumber, ItemCode, Date, RequiredQty, ManualAdd) " +
                " SELECT '" + Docnumber + "','" + itemcode + "',GETDATE()," + qty + ",1";
                }else
                {
                    str = " UPDATE Production.MRPRawMaterial " +
                                   " SET ItemCode ='"+ itemcode + "', ManualAdd=1 " +
                                   " WHERE Docnumber ='"+ Docnumber + "'";
                }



            }
            else if(type=="Delete")
            {
                str = " DELETE Production.MRPRawMaterial WHERE Docnumber = '"+ Docnumber + "' AND ItemCode ='"+ itemcode + "'"; 
            }

            Gears.RetriveData2(str, HttpContext.Current.Session["ConnString"].ToString());


            return "Action Successful";
        }


        [WebMethod]
        public static string[] ViewNotes(string ProductionSite, string Year, string WorkWeek, string UserID, string Level, string Params)
        {

            int maxItem = 7;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();


            for (int i = 0; i <= maxItem; i++)
            {
                dtbl = Gears.RetriveData2("exec [dbo].[sp_View_MRP] '" + ProductionSite + "','" + Year + "','" + WorkWeek + "','" + HttpContext.Current.Session["userid"].ToString() + "'," + i.ToString() + ",'" + Params + "'", HttpContext.Current.Session["ConnString"].ToString());
                output[i] = JsonConvert.SerializeObject(dtbl);
            }

            return output;

        }
    }


}