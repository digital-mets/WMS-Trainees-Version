using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GearsLibrary;
using System.Data;
using DevExpress.Web;


namespace GWL
{
	//01/18/2017    GC  Added codes for Contract Expiration Msg
    public partial class MainMaster : System.Web.UI.MasterPage
    {

        GearsLibrary.Gears _Gears = new Gears();
		//01/18/2017    GC  Added codes for Contract Expiration Msg
        string ContractPromptQuery = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["ConnString"].ToString() == "")
                {
                    // 2020-08-03   TL  Avoid hardcoded IP
                    //Response.Redirect("http://192.168.180.7");
                    Response.Redirect("~/Login/Login.aspx");
                    // 2020-08-03   TL  (End)
                 //   Response.Redirect("~/Login/Login.aspx");
                }
            }
            catch (Exception)
            {
                // 2020-08-03   TL  Avoid hardcoded IP
                //Response.Redirect("http://192.168.180.7");
                Response.Redirect("~/Login/Login.aspx");
                // 2020-08-03   TL  (End)
                // Response.Redirect("~/Login/Login.aspx");
            }

            DataTable table = GetDataTable();
            if (!IsPostBack)
            {
                gearsmetsit();
                CreateTreeViewNodesRecursive(table, this.treeView.Nodes, "__SYSMENU");
				//01/18/2017    GC  Added codes for Contract Expiration Msg
                ContractPromptQuery = "SELECT A.BizPartnerCode, B.Name, DocNumber, ContractType, EffectivityDate, "
                    + " DATEDIFF(DAY,GETDATE(),DateTo) AS DaysLeft FROM WMS.Contract A LEFT JOIN Masterfile.BPCustomerInfo B ON A.BizPartnerCode = B.BizPartnerCode "
                    + " WHERE DATEDIFF(DAY,GETDATE(),DateTo) < 31 AND DATEDIFF(DAY,GETDATE(),DateTo) > 0 "
                    + " AND A.Status = 'ACTIVE' AND ISNULL(A.SubmittedBy, '') != ''";
                DataTable ContractPrompt = Gears.RetriveData2(ContractPromptQuery, Session["ConnString"].ToString());
                if (ContractPrompt.Rows.Count > 0)
                {
                    GetContractPrompt();
                    popContractPrompt.ShowOnPageLoad = true;
                }
            }
                Gears.UseConnectionString(Session["ConnString"].ToString());

                DataTable MessageInfo = Gears.RetriveData2("Select * from IT.Message where Status = 'Show'", "Data Source=192.168.180.23;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;");
                foreach (DataRow dtrow in MessageInfo.Rows)
                {
                    Messagepop.ShowOnPageLoad = true;
                    Messagepop.HeaderText = dtrow["Header"].ToString();
                    Messagepop.HeaderStyle.ForeColor = System.Drawing.Color.FromName(dtrow["HeaderFont"].ToString());
                    Messagetxt.Text = dtrow["Message"].ToString();
                }    
        }

        private static string date1 = "";
        private static string date2 = "";


        private DataTable gearsmetsit()
        {
            //12-02-2016 GC Changed query to get default date for translist's date from
            //DataTable dtable = Gears.RetriveData2("select value,getdate() as value2 from it.systemsettings where code = 'BOOKDATE'", Session["ConnString"].ToString());
            DataTable dtable = Gears.RetriveData2("DECLARE @TRANSDATEFROM varchar(5) "
                + " SELECT @TRANSDATEFROM = Value FROM IT.SystemSettings WHERE Code = 'TRANDATEFR' "
                + " SELECT CASE WHEN @TRANSDATEFROM = 'NO' THEN value ELSE GETDATE() END AS value, GETDATE() AS value2 FROM IT.SystemSettings WHERE Code = 'BOOKDATE'", Session["ConnString"].ToString());
            //end

            foreach (DataRow dtRow in dtable.Rows)
            {
                date1 = Convert.ToDateTime(dtRow["value"].ToString()).ToShortDateString();
                date2 = Convert.ToDateTime(dtRow["value2"].ToString()).ToShortDateString();
            }
            return dtable;
        }
	
		//01/18/2017    GC  Added codes for Contract Expiration Msg
        private DataTable GetContractPrompt()
        {
            gvContractPrompt.DataSourceID = null;
            gvContractPrompt.DataBind();
            DataTable dt = new DataTable();
            //ContractPromptQuery
            DataTable ContractExpiration = new DataTable();
            ContractExpiration = Gears.RetriveData2(ContractPromptQuery, Session["ConnString"].ToString());
            gvContractPrompt.DataSource = ContractExpiration;
            if (gvContractPrompt.DataSourceID != "")
            {
                gvContractPrompt.DataSourceID = null;
            }
            gvContractPrompt.DataBind();

            foreach (GridViewColumn col in gvContractPrompt.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvContractPrompt.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvContractPrompt.GetRowValues(i, col.ColumnName);
            }

            return dt;
        }
        
        private void CreateTreeViewNodesRecursive(DataTable table, DevExpress.Web.TreeViewNodeCollection nodesCollection, string parentID)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["MenuID"].ToString() == parentID)
                {

                    DevExpress.Web.TreeViewNode node = new DevExpress.Web.TreeViewNode(table.Rows[i]["Prompt"].ToString(), table.Rows[i]["CommandString"].ToString());
                    string sql = table.Rows[i]["SQLCommand"].ToString();
                    string redirect = "";
                    string ribbon = table.Rows[i]["Ribbon"].ToString();
                    string param = table.Rows[i]["Parameters"].ToString();
                    string frm = table.Rows[i]["CommandString"].ToString();
                    string transtype = table.Rows[i]["TransType"].ToString();
                    string moduleid = table.Rows[i]["ModuleID"].ToString();
                    string storedproc = table.Rows[i]["StoredProc"].ToString();
                    string access = table.Rows[i]["Access"].ToString();
                    string prompt = table.Rows[i]["Prompt"].ToString();
                    string glposting = table.Rows[i]["GLPosting"].ToString();
                    string funcg = table.Rows[i]["FuncGroupID"].ToString();
                    string rep = "";

                    if (sql != "")
                    {
                        redirect = Gears.Encrypt(table.Rows[i]["SQLCommand"].ToString(), "mets");
                        node.Target = "./Translist.aspx?val=~" + redirect + "&prompt=" + prompt + "&frm=" + frm + "&date1=" + date1 + "&date2=" + date2 + "&ribbon=" + ribbon + "&transtype=" + transtype + "&moduleid=" + moduleid + "&sp=" + storedproc + "&access=" + access + "&parameters=" + param + "&glpost=" + glposting;
                        //encsql = table.Rows[i]["SQLCommand"].ToString();
                    }
                    else if (prompt == "Dashboard" || prompt == "Subcon Summary (RDE)")
                    {
                        node.Target = frm;

                    }
                    else if (param == "PView")
                    {
                        rep = Gears.Encrypt(Session["ConnString"].ToString(), "mets");
                        // 2020-08-03   TL  Avoid hardcoded IP
                        //node.Target = string.Format("http://192.168.180.7:9040/WebReports/ReportViewer.aspx?val=~{0}&param={1}&rep={2}&transtype={3}&userid={4}", frm, param, rep, moduleid, Session["userid"].ToString());
                        node.Target = string.Format("http://"+Request.Url.Host+":9040/WebReports/ReportViewer.aspx?val=~{0}&param={1}&rep={2}&transtype={3}&userid={4}", frm, param, rep, moduleid, Session["userid"].ToString());
                        // 2020-08-03   TL  (End)
                        //node.Target = string.Format("http://192.168.180.23:9060/WebReports/ReportViewer.aspx?val=~{0}&param={1}&rep={2}&transtype={3}&userid={4}", frm, param, rep, moduleid, Session["userid"].ToString());
                        //node.Target = string.Format("http://192.168.180.7:9040/WebReports/ReportViewer.aspx?val=~{0}&param={1}&rep={2}&transtype={3}&userid={4}", frm, param, rep, moduleid, Session["userid"].ToString());
                        //node.Target = string.Format("http://192.168.201.115:8050/WebReports/ReportViewer.aspx?val=~{0}&param={1}&rep={2}&transtype={3}&userid={4}", frm, param, rep, moduleid, Session["userid"].ToString());
                        //node.Target = "./WebReports/ReportViewer.aspx?val=~" + frm + "&param=" + param;
                    }
                    // 2016-05-26  Tony
                    else if (param == "Query")
                    {
                        node.Target = frm;
                    }
                    else if (param == "SView")
                    {
                        node.Target = "./WebReports/SSRSViewer.aspx?val=~" + frm;
                    }
                    // 2016-05-26  Tony  (End)
                    else
                    {
                        node.Target = "./Blank.aspx";
                    }

                    node.TextStyle.Font.Size = 9;
                    node.NodeStyle.Paddings.PaddingTop = 0;
                    node.NodeStyle.Paddings.PaddingBottom = 5;
                    node.NodeStyle.Paddings.PaddingLeft = 0;
                    node.NodeStyle.BorderLeft.BorderStyle = 0;

                    if (!String.IsNullOrEmpty(table.Rows[i]["IconFileName"].ToString()))
                    {
                        node.Image.Url = "~/icons/" + table.Rows[i]["IconFileName"].ToString();
                    }
                    else
                    {
                        node.Image.Url = "~/icons/Modules.png";
                    }
                    nodesCollection.Add(node);


                    //node.Name = "./Translist.aspx";
                    CreateTreeViewNodesRecursive(table, node.Nodes, node.Name);
                }
            }
        }
        private DataTable GetDataTable()
        {
            string connstring = Session["ConnString"].ToString();
            Gears.UseConnectionString(connstring);
            DataTable menu = Gears.RetriveData2("exec uspCreateMenu '" + Session["userid"].ToString() + "'", Session["ConnString"].ToString());

            return menu;
        }

        protected void treeView_NodeClick(object source, DevExpress.Web.TreeViewNodeEventArgs e)
        {
            //Session["SQLCOMMAND"] = e.Node.Target;
        }

        protected void cp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
        }


        #region ref
        //string Scmenuid = "DashBoard";
        //string Scprompt = "Dash Board";
        //string Sccommand = "__SYSMENU";
        //string ScIconFileName = "DashBoard.png";
        //DevExpress.Web.NavBarGroup aa = new DevExpress.Web.NavBarGroup();
        //aa.Text = Scprompt;
        //GWLMenu.Groups.Add(aa);
        //int i = 0;

        //for (int ctr = 0; ctr <= menu.Rows.Count - 1; ctr++)
        //{

        //    Scmenuid = menu.Rows[ctr]["MenuID"].ToString();
        //    if (Scmenuid == "__SYSMENU")
        //    {
        //        Scprompt = menu.Rows[ctr]["Prompt"].ToString();
        //        Sccommand = menu.Rows[ctr]["CommandString"].ToString();
        //        ScIconFileName = menu.Rows[ctr]["IconFileName"].ToString() == "" ? "0" : menu.Rows[ctr]["IconFileName"].ToString();
        //        Scmenuid = Scprompt;
        //        //tmpN = TVMenu.Nodes.Add(Scprompt);
        //        //tmpN.SelectedImageKey = ScIconFileName;
        //        //tmpN.ImageKey = ScIconFileName;
        //        //tmpN.ToolTipText = Scprompt;
        //        //addchild(tmpN, Scmenuid, Scprompt, Sccommand);
        //        DevExpress.Web.NavBarGroup bb = new DevExpress.Web.NavBarGroup();
        //        bb.Text = Scprompt;
        //        GWLMenu.Groups.Add(bb);
        //        GWLMenu.Groups[ctr].Items.Add("test");
        //        //addchild(bb, Scmenuid, Scprompt, Sccommand);
        //        //GWLMenu.Groups[i].Items.Add("test");
        //        //Menu.Groups[i].Items.Add(Answer);


        //    }
        //}
        //}

        //   private void addchild(DevExpress.Web.NavBarGroup ParentNode, string Cmenuid, String Cprompt, String Ccommand)
        //   {
        //       if (Ccommand.Substring(0, 1) == "_")
        //       {
        //           addchild(ParentNode, Ccommand, Cprompt, Cmenuid);
        //       }
        //       else
        //       {

        //           if (menu.Rows.Count != 0)
        //           {
        //               for (int ctr = 0; ctr <= menu.Rows.Count - 1; ctr++)
        //               {
        //                   string strmenuid = menu.Rows[ctr]["MenuID"].ToString();
        //                   if (strmenuid == Cmenuid)
        //                   {
        //                       string strprompt = menu.Rows[ctr]["Prompt"].ToString();
        //                       string strcommand = menu.Rows[ctr]["CommandString"].ToString();
        //                       string ScIconFileName = menu.Rows[ctr]["IconFileName"].ToString() == "" ? "0" : menu.Rows[ctr]["IconFileName"].ToString();

        //                       if (strcommand.Substring(0, 1) == "_")
        //                       {

        //                           DevExpress.Web.NavBarGroup tmpN = ParentNode.NavBar.Groups.Add(strprompt);
        //                           //tmpN.SelectedImageKey = ScIconFileName;
        //                           //tmpN.ImageKey = ScIconFileName;
        //                           //tmpN.ToolTipText = strprompt;
        //                           //tmpN.ForeColor = Color.Blue;
        //                           addchild(tmpN, strcommand, strprompt, "PARENT");
        //                       }
        //                       else
        //                       {
        //                           DevExpress.Web.NavBarGroup tmpN = ParentNode.NavBar.Groups.Add(strprompt);
        //                           //tmpN.Tag = menu.Rows[ctr]["ModuleID"].ToString();
        //                           //tmpN.SelectedImageKey = ScIconFileName;
        //                           //tmpN.ImageKey = ScIconFileName;
        //                           if (strcommand.Substring(0, 1) == "/")
        //                           {
        //                               //tmpN.ToolTipText = strcommand;
        //                           }
        //                           else
        //                           {
        //                               //tmpN.ToolTipText = strprompt;
        //                           }
        //                       }
        //                   }
        //               }
        //           }
        //       }
        //   }
        #endregion
    }
}

