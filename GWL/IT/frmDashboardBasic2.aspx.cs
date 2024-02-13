using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using GearsLibrary;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Data.OleDb;
using GearsWarehouseManagement;
using System.Data.SqlClient;
using Entity;
using System.Configuration;

namespace GWL.IT
{
    public partial class frmDashboardBasic2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string[] Parameter() // Get parameters value
        {
            int maxItem = 7;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            string Conn = HttpContext.Current.Session["ConnString"].ToString();
            string companyCode = HttpContext.Current.Session["WHCode"].ToString();

            // Customer Code
            //if (companyCode == "MLI")
            //{
                dtbl = Gears.RetriveData2("SELECT BizPartnerCode AS Code, Name FROM Masterfile.BizPartner WHERE ISNULL(IsInactive,0)=0", Conn);
            //}
            //else
            //{
            //    //dtbl = Gears.RetriveData2("SELECT BizPartnerCode AS Code, Name FROM IT.Users A INNER JOIN IT.BizPartner B ON CHARINDEX(BizPartnerCode,A.CompanyCode)>0 WHERE UserID = '" + HttpContext.Current.Session["userid"].ToString() + "' AND ISNULL(B.IsInactive,0)=0", Conn);
            //    dtbl = Gears.RetriveData2("SELECT CustomerCode AS Code into #TEMP FROM " +
            //    "(SELECT Split.a.value('.', 'VARCHAR(MAX)') AS CustomerCode " +
            //    "FROM(SELECT CAST('<M>' + REPLACE((SELECT CompanyCode as Code FROM IT.Users WHERE UserID = '" + HttpContext.Current.Session["userid"].ToString() + "'), ',', '</M><M>') + '</M>' AS XML) AS String" +
            //    ") AS A CROSS APPLY String.nodes('/M') AS Split(a)) AS X " +

            //    "SELECT Code, B.Name FROM #TEMP A LEFT JOIN IT.BizPartner B ON A.Code = B.BizPartnerCode WHERE ISNULL(IsInactive,0)=0", Conn);
            //}
            output[0] = JsonConvert.SerializeObject(dtbl);

            // 7/25/2022 JCB Updated Code for fetching Area
            // Area Code
            dtbl = Gears.RetriveData2("select PlantCode as Code from Masterfile.Plant where WarehouseCode = '"+ companyCode + "'", Conn);
            //if (companyCode == "MLI")
            //{
            //    dtbl = Gears.RetriveData2("SELECT DISTINCT StorageLoc AS Code, Description AS Name FROM IT.BizPartnerDetail A LEFT JOIN IT.Area B ON A.StorageLoc = B.AreaCode " +
            //                           "WHERE('SELECT CAST((SELECT CompanyCode as Code FROM IT.Users WHERE UserID = " + HttpContext.Current.Session["userid"].ToString() + ") AS XML) AS String' = 'ALL' OR BizPartnerCode IN(SELECT CustomerCode FROM " +
            //                               "(SELECT Split.a.value('.', 'VARCHAR(MAX)') AS CustomerCode " +
            //                               "FROM(SELECT CAST('<M>' + REPLACE((SELECT STUFF((SELECT ',' + upper(left(BizPartnerCode,1)) + substring(BizPartnerCode,2,len(BizPartnerCode)) FROM IT.BizPartner FOR XML PATH('')) ,1,1,'') AS String ), ',', '</M><M>') + '</M>' AS XML) AS String " +
            //                               ") AS A CROSS APPLY String.nodes('/M') AS Split(a)) AS X)) " +
            //                           "AND('" + HttpContext.Current.Session["WHCode"].ToString() + "' = 'ALL' OR WarehouseCode IN(SELECT WarehouseCode FROM " +
            //                               "(SELECT Split.a.value('.', 'VARCHAR(MAX)') AS WarehouseCode " +
            //                               "FROM(SELECT CAST('<M>' + REPLACE('" + HttpContext.Current.Session["WHCode"].ToString() + "', ',', '</M><M>') + '</M>' AS XML) AS String " +
            //                               ") AS A CROSS APPLY String.nodes('/M') AS Split(a)) AS X))", Conn);

            //}
            //else
            //{
            //    dtbl = Gears.RetriveData2("SELECT DISTINCT StorageLoc AS Code, Description AS Name FROM IT.BizPartnerDetail A LEFT JOIN IT.Area B ON A.StorageLoc = B.AreaCode " +
            //                           "WHERE('SELECT CAST((SELECT CompanyCode as Code FROM IT.Users WHERE UserID = " + HttpContext.Current.Session["userid"].ToString() + ") AS XML) AS String' = 'ALL' OR BizPartnerCode IN(SELECT CustomerCode FROM " +
            //                               "(SELECT Split.a.value('.', 'VARCHAR(MAX)') AS CustomerCode " +
            //                               "FROM(SELECT CAST('<M>' + REPLACE('" + HttpContext.Current.Session["CompanyCode"].ToString() + "', ',', '</M><M>') + '</M>' AS XML) AS String " +
            //                               ") AS A CROSS APPLY String.nodes('/M') AS Split(a)) AS X)) " +
            //                           "AND('" + HttpContext.Current.Session["WHCode"].ToString() + "' = 'ALL' OR WarehouseCode IN(SELECT WarehouseCode FROM " +
            //                               "(SELECT Split.a.value('.', 'VARCHAR(MAX)') AS WarehouseCode " +
            //                               "FROM(SELECT CAST('<M>' + REPLACE('" + HttpContext.Current.Session["WHCode"].ToString() + "', ',', '</M><M>') + '</M>' AS XML) AS String " +
            //                               ") AS A CROSS APPLY String.nodes('/M') AS Split(a)) AS X))", Conn);
            //}
            output[1] = JsonConvert.SerializeObject(dtbl);

            //// Warehouse Code
            ////dtbl = Gears.RetriveData2("SELECT WarehouseCode AS Code, Description AS Name FROM IT.Warehouse WHERE ISNULL(IsInactive,0)=0", Conn);
            //dtbl = Gears.RetriveData2("SELECT B.WarehouseCode AS Code, Description as Name FROM IT.Users A LEFT JOIN IT.Warehouse B ON CHARINDEX(B.WarehouseCode,A.WarehouseCode)>0 WHERE UserID = '" + HttpContext.Current.Session["userid"].ToString() + "' AND ISNULL(B.IsInactive,0)=0", Conn);
            //output[2] = JsonConvert.SerializeObject(dtbl);

            //// Truck Type
            //dtbl = Gears.RetriveData2("SELECT TruckType AS Code, Description AS Name FROM IT.TruckType WHERE ISNULL(IsInactive,0)=0", Conn);
            //output[3] = JsonConvert.SerializeObject(dtbl);


            //// YEAR
            //dtbl = Gears.RetriveData2(";with YearList AS(SELECT 2000 AS Year UNION ALL SELECT YL.Year + 1 FROM YearList YL WHERE YL.Year + 1 <= Year (GetDate()) ) SELECT Year FROM YearList Order By Year Desc", Conn);
            //output[4] = JsonConvert.SerializeObject(dtbl);


            //// MONTH
            //dtbl = Gears.RetriveData2("; WITH MonthList AS (SELECT 1 AS Month, DATENAME (MONTH, cast (YEAR(GETDATE ()) * 100 + 1 AS VARCHAR) + '01') AS MonthName UNION ALL SELECT Month + 1, DATENAME (MONTH, CAST (YEAR (GETDATE ())* 100 + (Month + 1) AS VARCHAR) + '01') FROM MonthList WHERE Month < 12) SELECT Month, MonthName FROM MonthList", Conn);
            //output[5] = JsonConvert.SerializeObject(dtbl);

            //// MONTH
            //dtbl = Gears.RetriveData2("SELECT Distinct Owner as Code, Owner as Name From IT.INVENTORYCOUNT ORDER BY owner asc", Conn);
            //output[6] = JsonConvert.SerializeObject(dtbl);

            //// Default Site
            //dtbl = Gears.RetriveData2("select Site from it.UsersDetail where UserID = '" + HttpContext.Current.Session["userid"].ToString() + "'", Conn);
            //output[7] = JsonConvert.SerializeObject(dtbl);


            return output;
        }


        [WebMethod]
        public static string[] GetData(Dash _data) // Get data
        {
            string Conn = HttpContext.Current.Session["ConnString"].ToString();
       

            int ctr = 0;
            int maxItem = 20;
            string[] output = new string[maxItem + 1];
            DataTable dtbl = new DataTable();

            string[] action1 = { "Card1", "Card2", "Card3", "Card4", "Card5", "Card6", "Card7", "Table1", "Table2", "Chart1", "Chart2", "Chart3", "Cold", "Dry" ,"Aircon" ,"Chiller"};
            
            string[] action = new string[3];


            action = action1 ;

            foreach (string item in action)
            {
                dtbl = Gears.RetriveData2("EXEC sp_api_Dashboard'" + _data.DateStart + "','" + _data.DateEnd + "', '" + item + "','" + _data.TransType + "','" + _data.Customer + "','"+ _data.Warehouse + "','" + _data.Area + "'", Conn);
                output[ctr] = JsonConvert.SerializeObject(dtbl);
                ctr++;
            }

            return output;
        }

    }
}