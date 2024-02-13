using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GWL.FactBox
{
    public partial class fbBizPartner : System.Web.UI.Page
    {
       

        private object BizPartnerCode;
       private object  Name;
       private object  Address;
        private object ContactPerson;
        private object IsSupplier;
        private object  IsCustomer;
       private object  IsEmployee;
       private object IsInactive;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BizPartnerCode = Request.QueryString["BizPartnerCode"];
              
                GetData();
                litText.Text = "Code: " + BizPartnerCode.ToString() +
                               "<br>Name: " + Name.ToString() +
                               "<br>Address: " + Address.ToString() +
                               "<br>Contact Person: " + ContactPerson.ToString() +
                               "<br> IsSupplier: " + IsSupplier.ToString() +
                                "<br> IsCustomer: " + IsCustomer.ToString() +
                                 "<br> IsEmployee: " + IsEmployee.ToString() +
                                  "<br> IsInactive: " + IsInactive.ToString() ;
            }
            catch (Exception)
            {
                return;
            }
        }
        protected void callbackPanel_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            //GetData(e.Parameter);
            //litText.Text = "ItemCode: " + ItemCode.ToString() + "<br> Onhand: " + Onhand.ToString();
        }

        private void GetData()
        {
            SqlDataSource1.ConnectionString = Session["ConnString"].ToString();
            //AccessDataSource ds = new AccessDataSource();
            SqlDataSource ds = SqlDataSource1;


            //ds.SelectCommand = string.Format(" SELECT BizPartnerCode,Name,Address,ContactPerson,case when IsSupplier='0' then 'False' else 'True' end as IsSupplier, "
            //             + "   case when IsCustomer='0' then 'False' else 'True' end as IsCustomer,case when IsEmployee ='0' then 'False' else 'True' end as IsEmployee,case when IsInactive='0' then 'False' else 'True' end as IsInactive FROM Masterfile.BizPartner where BizPartnerCode='" + BizPartnerCode + "'");


            ds.SelectCommand = string.Format(" SELECT BizPartnerCode,Name,Address,ContactPerson, IsSupplier, "
                       + " IsCustomer, IsEmployee, IsInactive FROM Masterfile.BizPartner where BizPartnerCode='" + BizPartnerCode + "'");


            DataView view = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            if (view.Count > 0)
            {
                BizPartnerCode = view[0][0];
                Name = view[0][1];
                Address = view[0][2];
                ContactPerson = view[0][3];
                IsSupplier = view[0][4];
                IsCustomer = view[0][5];
                IsEmployee = view[0][6];
                IsInactive = view[0][7];
            }
        }
    }
}