using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using GearsLibrary;

namespace RCommon
{
    public class RCommon
    {
        public DataTable Months()
        {

            DataTable a = new DataTable();
            a.Columns.Add("Code");
            a.Columns.Add("Description");
            a.Rows.Add(1, "January");
            a.Rows.Add(2, "February");
            a.Rows.Add(3, "March");
            a.Rows.Add(4, "April");
            a.Rows.Add(5, "May");
            a.Rows.Add(6, "June");
            a.Rows.Add(7, "July");
            a.Rows.Add(8, "August");
            a.Rows.Add(9, "September");
            a.Rows.Add(10, "October");
            a.Rows.Add(11, "November");
            a.Rows.Add(12, "December");
            return a;
        }

        public static DataTable cgs;

        public static DataTable CG
        {
            get
            {
                return cgs;
            }
            set
            {

                cgs = value;
            }
        }

        public static void ExporttoExcel2(DataTable dt)
        {



            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=\"" + "TEST" + "\".csv");
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.ContentType = "application/text";


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }

            System.Web.HttpContext.Current.Response.Output.Write(sb.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();


        }


        public static string SystemSetting(string WhatCode, string ConnString)
        {
            string strValue = null;
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(ConnectionString()))
            //    {
            //        connection.Open();
            //        SqlCommand cmd = new SqlCommand("sp_SystemSetting", connection);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.Add(new SqlParameter("@DoWhat", "GET"));
            //        cmd.Parameters.Add(new SqlParameter("@WhatCode", WhatCode));
            //        cmd.Parameters.Add(new SqlParameter("@TheValue", "N/A"));
            //        String strMessage = (String)cmd.ExecuteScalar();
            //        connection.Close();
            //        if (!string.IsNullOrEmpty(strMessage))
            //        {
            //            strValue = strMessage;
            //        }
            //    }
            //}
            //catch
            //{
            //    strValue = "";
            //}

            DataTable GetValue = Gears.RetriveData2("exec sp_SystemSetting 'GET','" + WhatCode + "','N/A'", ConnString);
            foreach (DataRow dtrow in GetValue.Rows)
            {
                strValue = dtrow[0].ToString();
            }

            return strValue;
        }
        //glenn 123
        public static string strReportName;

        public static string RawReportName
        {
            get
            {
                return strReportName;
            }
            set
            {

                strReportName = value;
            }
        }

        public static void RawData(DevExpress.DataAccess.Sql.SqlDataSource source, string ReportName, string ConnString)
        {

            DataTable GetValue = Gears.RetriveData2("select StoredProc from IT.mainmenu where CommandString='" + ReportName + "'", ConnString);

            if (GetValue.Rows.Count > 0)
            {
                try
                {
                    DevExpress.DataAccess.Sql.DataApi.ITable table = source.Result[GetValue.Rows[0][0].ToString().Trim()];

                    DataTable dest = new DataTable();
                    foreach (DevExpress.DataAccess.Sql.DataApi.IColumn column in table.Columns)
                        dest.Columns.Add(column.Name, column.Type);
                    foreach (DevExpress.DataAccess.Sql.DataApi.IRow row in table)
                        dest.Rows.Add(row.ToArray());

                    RCommon.CG = dest;
                    RCommon.RawReportName = ReportName;
                }
                catch
                {

                }

            }
        }
        //glenn 123


        public static string TourChecker(string Userid, string Moduleid, string ConnString)
        {

            string tourdone = "0";
           
            DataTable GetValue = Gears.RetriveData2("SELECT DoNotShowTour FROM IT.Tour where UserID='" + Userid + "' AND ModuleID='" + Moduleid + "' ", ConnString);
            if (GetValue.Rows.Count > 0)
            {
                tourdone = GetValue.Rows[0][0].ToString();
            }
          

            return tourdone;
        }
    }
}
