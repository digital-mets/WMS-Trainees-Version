using GearsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Entity
{
    public class Functions
    {

        public static void AuditTrail(string TransType, string Docnumber, string UserID, string LogDate, string Action, string Conn)
       {
           GearsLibrary.Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
           DT1.Rows.Add("IT.Usertrail", "0", "TransType", TransType);
           DT1.Rows.Add("IT.Usertrail", "0", "Docnumber", Docnumber);
           DT1.Rows.Add("IT.Usertrail", "0", "UserID", UserID);
           DT1.Rows.Add("IT.Usertrail", "0", "LogDate", LogDate);
           DT1.Rows.Add("IT.Usertrail", "0", "Action", Action);
           Gears.CreateData(DT1, Conn);
       }
        public static DataTable Validation(string sp, string DocNumber, string TransType, string Conn)
        {
            DataTable dtresult = Gears.RetriveData2("exec " + sp + "'" + DocNumber + "','" + TransType + "'", Conn);
            return dtresult;
        }
        public static DataTable Submit(string sp, string DocNumber, string UserID, int Factor, string TransType, string Conn)
        {
            DataTable dtresult = Gears.RetriveData2("exec " + sp + "'" + DocNumber + "','" + UserID + "'," + Factor + ",'" + TransType + "'", Conn);
            return dtresult;
        }

        public static string Submitted(string DocNumber, string TableName, int Factor, string Conn)
        {
            string sub = "";
            DataTable dtresult = Gears.RetriveData2("exec sp_CheckColumn '" + DocNumber + "','" + TableName + "','" + Factor + "'", Conn);
            if (dtresult.Rows.Count == 1)
            {
                sub = "Cannot update! Document is either Approved, Submitted or Cancelled already!";
            }
            return sub;
        }

    }
}
