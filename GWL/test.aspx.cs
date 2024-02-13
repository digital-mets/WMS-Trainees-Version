using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using GearsWarehouseManagement;
using GearsLibrary;

namespace GWL
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string fck = connection.Value.ToString();
                Gears.UseConnectionString(fck);
            }
            catch(Exception)
            {

            }
        }

        protected void connection_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                connection.Items.Add("Gears-METSIT", "Data Source=mit-infra10d;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*");
                connection.Items.Add("Gears-LOGISTICS", "Data Source=192.168.180.9;Initial Catalog=GWL-LOGISTICS;User id=sa;Password=IT4321$#@!");
                connection.Items.Add("Gears-MGD", "Data Source=192.168.180.9;Initial Catalog=GWL-MGD;User id=sa;Password=IT4321$#@!");
                connection.Items.Add("Gears-JEANOLOGY-LIVE", "Data Source=192.168.180.9;Initial Catalog=METS-JEANOLOGY;User id=sa;Password=IT4321$#@!");
            }
        }
        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "Gears-METSIT":
                    
                    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
                            gparam._DocNo = "1234556";
                            gparam._UserId = "1828";
                            gparam._TransType = "WMSICN";
                            string strresult = GWarehouseManagement.Inbound_Validate(gparam);
                            cp.JSProperties["cp_result"] = strresult;
                    break;
                case "Gears-LOGISTICS":
                    //Gears.UseConnectionString("Data Source=192.168.180.9;Initial Catalog=GWL-LOGISTICS;User id=sa;Password=IT4321$#@!");
                            gparam = new Gears.GearsParameter();
                            gparam._DocNo = "AFC16-2028";
                            gparam._UserId = "1828";
                            gparam._TransType = "WMSICN";
                            strresult = GWarehouseManagement.Inbound_Validate(gparam);
                            cp.JSProperties["cp_result"] = strresult;
                    break;

            }
        }

        




    }
}