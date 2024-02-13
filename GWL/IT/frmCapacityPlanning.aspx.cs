using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using GearsLibrary;
using System.Configuration;
using Entity;

namespace GWL.IT
{
    public partial class frmCapacityPlanning : System.Web.UI.Page
    {
        string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;
        Entity.CapacityPlanning _Entity = new CapacityPlanning();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Webmethod

        [WebMethod]
        public static string YearParameter()
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("SELECT DISTINCT Year FROM Production.CounterPlan WHERE Status IN ('F', 'S') ORDER BY Year DESC", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string WeekParameter()
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("SELECT DISTINCT WorkWeek FROM Production.CounterPlan WHERE Status IN ('F', 'S') ORDER BY WorkWeek DESC", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string Machine(string MachineType)
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("SELECT MachineName AS Code, Description FROM Masterfile.MachineMaster WHERE MachineCategory='" + MachineType + "'", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string SP_Call(int year, int workweek, string table, string dayno, string skucode, string generate, string stages)
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;

            DataTable dt = Gears.RetriveData2("EXEC sp_CapacityPlanning '" + year + "', '" + workweek + "', '" + table + "', '" + dayno + "', '" + skucode + "', '" + HttpContext.Current.Session["userid"].ToString() + "', '" + generate + "', '" + stages + "'", Conn);

            return JsonConvert.SerializeObject(dt);
        }

        [WebMethod]
        public static string Update(CapacityPlanning _cp, string table)
        {
            string Conn = ConfigurationManager.ConnectionStrings["GEARS-METSITConnectionString"].ConnectionString;
            string resp = "";

            try
            {
                switch (table) {
                    case "MachineH":
                        DataTable dt = Gears.RetriveData2("SELECT Year, WorkWeek, SKUCode FROM Production.CapacityPlanningMachine WHERE RecordID=" + _cp.RecordID, Conn);
                        Gears.RetriveData2("UPDATE Production.CapacityPlanningMachine SET SequenceDay=" + _cp.SequenceDay + " WHERE RecordID=" + _cp.RecordID, Conn);
                        Gears.RetriveData2("UPDATE Production.CapacityPlanningMachineDetail SET SequenceDay=" + _cp.SequenceDay + " WHERE Year=" + dt.Rows[0][0].ToString() + " AND WorkWeek=" + dt.Rows[0][1].ToString() + " AND SKUCode'" + dt.Rows[0][2].ToString() + "'", Conn);
                    break;

                    case "MachineD":
                        DataTable dt2 = Gears.RetriveData2("SELECT Field1 AS MachineDetails, CapacityQty AS CapacityHrPerBatch FROM Masterfile.Machine WHERE MachineCode='" + _cp.AvailableMachine + "'", Conn);
                        Gears.RetriveData2("UPDATE Production.CapacityPlanningMachineDetail SET AvailableMachine='" + _cp.AvailableMachine + "', MachineDetails='" + dt2.Rows[0][0].ToString() + "', CapacityHrPerBatch=" + dt2.Rows[0][1].ToString() + " WHERE RecordID=" + _cp.RecordID, Conn);
                        break;

                    case "ManpowerS":
                        Gears.RetriveData2("UPDATE Production.CapacityPlanningManpowerSpecific SET NoManpower=" + _cp.NoManpower + " WHERE RecordID=" + _cp.RecordID, Conn);
                        break;

                    case "ManpowerG":
                        Gears.RetriveData2("UPDATE Production.CapacityPlanningManpowerGeneral SET NoManpower=" + _cp.NoManpower + " WHERE RecordID=" + _cp.RecordID, Conn);
                        break;
                }

                Gears.RetriveData2("UPDATE Production.CapacityPlanning SET LastEditedBy=" + HttpContext.Current.Session["userid"].ToString() + ", LastEditedDate=GETDATE() WHERE Year=" + _cp.Year + " AND WorkWeek=" + _cp.WorkWeek, Conn);
                
                resp = "Updated Successfully";
            }
            catch (Exception ex)
            {
                resp = "Error, while updating the data: " + ex;
            }
            return resp;
        }

        #endregion
    }
}