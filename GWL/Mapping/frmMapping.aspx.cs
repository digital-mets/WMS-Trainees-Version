using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
using Entity;
using System.Data;
using GearsTrading;
using DevExpress.Data.Filtering;
using GearsWarehouseManagement;
using GearsProcurement;

namespace GWL
{
    public partial class frmMapping : System.Web.UI.Page
    {

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            //Gears.UseConnectionString(Session["ConnString"].ToString());

         
            //string referer;
            //try
            //{
            //    referer = Request.ServerVariables["http_referer"];
            //}
            //catch
            //{
            //    referer = "";
            //}

            //if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            //{
            //    Response.Redirect("~/error.aspx");
            //}


            //if (!IsPostBack)
            //{

            //    //Session["atccode"] = null;
            //    //Session["picklistdetail"] = null;
            //    //Session["customoutbound"] = null;
                
            //    //Session["pickfromdate"] = null;
            //    //Session["picktodate"] = null;

            //    if (Session["pickfromdate"] != null)
            //    {
            //        dtpdatefrom.Text = Session["pickfromdate"].ToString();
                    
            //    }

            //    if (Session["picktodate"] != null)
            //    {
            //        dtpdateto.Text  =  Session["picktodate"].ToString();
            //    }

                
            //    if (Session["pickwh"] != null)
            //    {
            //        txtwarehouse.Text = Session["pickwh"].ToString();
            //    }

            //    if (Session["pickcustomer"] != null)
            //    {
            //        txtcustomer.Text = Session["pickcustomer"].ToString();
            //    }

                

            //    //V=View, E=Edit, N=New
            //    switch (Request.QueryString["entry"].ToString())
            //    {
            //        case "N":
                       
            //            break;
            //        case "E":

            //            updateBtn.Text = "Update";
            //            break;
            //        case "V":
            //            view = true;//sets view mode for entry
            //            updateBtn.Text = "Close";
            //            glcheck.ClientVisible = false;

            //            break;
            //        case "D":
            //            view = true;
            //            updateBtn.Text = "Delete";
            //            break;
            //    }



            //    if (Request.QueryString["entry"].ToString() == "N")
            //    {
            //        gv1.DataSourceID = null;
            //        gv1.DataBind();
            //        popup.ShowOnPageLoad = false;
            //        //frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

             
                   
            //        //gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            //        //gv1.Settings.VerticalScrollableHeight = 200;
            //        //gv1.KeyFieldName = "LineNumber;PODocNumber";
            //    }
  
                

                

            //}
            //else
            //{
            //    Session["pickwh"] = null;
            //    Session["pickcustomer"] = null;
            //    Session["picktodate"] = null;
            //    Session["pickfromdate"] = null; 

            //}

            



        }

        #endregion

        #region Validation
        
        #endregion

        #region Set controls' state/behavior/etc...

        #endregion

        #region Lookup Settings

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
      
        #endregion

     



      

        
        
    }
}