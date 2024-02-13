using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;
using System.Data.SqlClient;


namespace Common
{
    public class Common
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

        public static string SystemSetting(string WhatCode,string ConnString)
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

        public static string Import_Forecast(DataTable CP, string Year, string ItemCategoryCode, string Session, string ConnString)
        {
            string strResult = "";
            int ColCount = CP.Columns.Count;
            int AddColumn = 100 - ColCount;
            int Start = ColCount + 1;

            if (AddColumn != 0)
            {

                for (int i = Start; i <= 100; i++)
                {
                    CP.Columns.Add("C" + Convert.ToString(i), typeof(System.String));

                    foreach (DataRow dr in CP.Rows)
                    {
                        //need to set value to MyRow column
                        dr["C" + Convert.ToString(i)] = "";   // or set it to some other value
                    }

                }
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Import_Forecast", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Year", Year));
                    cmd.Parameters.Add(new SqlParameter("@ItemCategory", ItemCategoryCode));
                    cmd.Parameters.Add(new SqlParameter("@Session", Session));
                    cmd.Parameters.Add(new SqlParameter("@tvpConversion", CP));
                    SqlDataReader strMessage = cmd.ExecuteReader();

                    DataTable dtRetValue = new DataTable();

                    if (strMessage.HasRows)
                    {
                        dtRetValue.Load(strMessage);
                    }
                    connection.Close();

                    foreach (DataRow dt in dtRetValue.Rows)
                    {
                        if (!String.IsNullOrEmpty(dt[0].ToString()))
                        {
                            strResult += "\n" + Year + ": " + dt[0].ToString();
                        }
                    }


                    if (!Convert.IsDBNull(strResult) && String.IsNullOrEmpty(strResult))
                    {
                        strResult = "";
                    }

                }
            }
            catch (Exception ex)
            {
                strResult += "\n " + Year + ": " + ex.Message;
            }

            return strResult;
        }

     public static string GetAccess(string UserID, string Module, string ConnString)
        {
            string strValue = null;


            DataTable GetValue = Gears.RetriveData2("select DISTINCT Access from IT.Users A " +
                                                        " INNER JOIN IT.UsersDetail B " +
                                                        " ON A.UserID = B.UserID " +
                                                        " INNER JOIN IT.UserRolesDetail C " +
                                                        "ON B.RoleID = C.RoleID " +
                                                        " WHERE A.UserID = '" + UserID + "' and C.ModuleID = '" + Module + "'", ConnString);
            foreach (DataRow dtrow in GetValue.Rows)
            {
                strValue += dtrow[0].ToString();
            }

            return strValue;
        }

  

    }
}
