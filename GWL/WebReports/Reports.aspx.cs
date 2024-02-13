using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace GWL
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        protected void cb1_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "GWL.WebReports.GEARS_Reports");
                for (int i = 0; i < typelist.Length; i++)
                {
                    cb1.Items.Add(typelist[i].Name);
                }
            }
        }
        protected void cb2_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "GWL.WebReports.GEARS_Printout");
                for (int i = 0; i < typelist.Length; i++)
                {
                    cb2.Items.Add(typelist[i].Name);
                }
            }
        }
        protected void connection_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                connection.Items.Add("Gears-METSIT", "Data Source=mit-infra10d;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*");
                connection.Items.Add("Gears-LOGISTICS", "Data Source=192.168.180.23;Initial Catalog=GWL-LOGISTICS;User id=sa;Password=mets123*");
                connection.Items.Add("Gears-MLI", "Data Source=192.168.180.9;Initial Catalog=GWL-MLI;User id=sa;Password=IT4321$#@!");
                connection.Items.Add("Gears-MGD", "Data Source=192.168.180.9;Initial Catalog=GWL-MGD;User id=sa;Password=IT4321$#@!");
                connection.Items.Add("Gears-JEANOLOGY-LIVE", "Data Source=192.168.180.9;Initial Catalog=METS-JEANOLOGY;User id=sa;Password=IT4321$#@!");
                connection.Items.Add("Gears-THEMIS", "Data Source=192.168.180.9;Initial Catalog=GWL-THEMIS-PISA;User id=sa;Password=IT4321$#@!");
                connection.Items.Add("Gears-HAYAT", "Data Source=192.168.180.9;Initial Catalog=GWL-HAYAT-PISA2;User id=sa;Password=IT4321$#@!");
            }
        }
        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            cp.JSProperties["cp_connection"] = connection.SelectedItem.Value.ToString();
            if (cb1.SelectedItem != null)
            {
                cp.JSProperties["cp_report"] = "GEARS_Reports." + cb1.SelectedItem.Text;
                Session["ConnString"] = connection.SelectedItem.Value.ToString(); ;
            }

            if (cb2.SelectedItem != null)
            {
                cp.JSProperties["cp_report"] = "GEARS_Printout." + cb2.SelectedItem.Text;
                Session["ConnString"] = connection.SelectedItem.Value.ToString();
            }
            Session["userid"] = "1828";
        }

        




    }
}