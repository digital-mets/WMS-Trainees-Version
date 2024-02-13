using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
namespace GWL.FactBox
{
    public partial class fbItem : System.Web.UI.Page
    {
        private object ItemCode;
        private object ColorCode;
        private object ClassCode;
        private object SizeCode;
        private object Onhand;
        private object Warehouse;
        private object warehouseQTY;
        private object OnOrderQTY;
        private object AllocatedQTY;
        private object LatestPrice;
        private object BaseUnit;
        //
        private object ItemCategory;
        private object ProductCategory;
        private object ProductSubCategory;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ItemCode = Request.QueryString["itemcode"];
                ColorCode = Request.QueryString["colorcode"];
                ClassCode = Request.QueryString["classcode"];
                SizeCode = Request.QueryString["sizecode"];
                Warehouse = Request.QueryString["Warehouse"];
                GetData();

                if (Warehouse.ToString() != "")
                {
                    litText.Text = "ItemCode: " + ItemCode.ToString() +
                              "<br>ColorCode: " + ColorCode.ToString() +
                              "<br>ClassCode: " + ClassCode.ToString() +
                              "<br>SizeCode: " + SizeCode.ToString() +
                              "<br>ItemCat: " + ItemCategory.ToString() +
                              "<br>ProductCat: " + ProductCategory.ToString() +
                              "<br>ProductSubCat: " + ProductSubCategory.ToString() +
                              "<br> Unit: " + BaseUnit +
                              "<br> Onhand: " + String.Format("{0:n}", Convert.ToDouble(Onhand.ToString() == "" ? "0" : Onhand.ToString())) +
                              "<br>" + Warehouse.ToString().Trim() + ": " + String.Format("{0:n}", Convert.ToDouble(warehouseQTY.ToString() == "" ? "0" : warehouseQTY.ToString())) +
                              "<br> OnOrder: " + String.Format("{0:n}", Convert.ToDouble(OnOrderQTY.ToString() == "" ? "0" : OnOrderQTY.ToString())) +
                              "<br> AllocatedQTY: " + String.Format("{0:n}", Convert.ToDouble(AllocatedQTY.ToString() == "" ? "0" : AllocatedQTY.ToString())) +
                              "<br> LatestPrice: " + String.Format("{0:n}", Convert.ToDouble(LatestPrice.ToString() == "" ? "0" : LatestPrice.ToString()));
                              
                }
                else
                {
                    litText.Text = "ItemCode: " + ItemCode.ToString() +
                             "<br>ColorCode: " + ColorCode.ToString() +
                             "<br>ClassCode: " + ClassCode.ToString() +
                             "<br>SizeCode: " + SizeCode.ToString() +
                              "<br>ItemCat: " + ItemCategory.ToString() +
                              "<br>ProductCat: " + ProductCategory.ToString() +
                              "<br>ProductSubCat: " + ProductSubCategory.ToString() +
                             "<br> Unit: " + BaseUnit +
                             "<br> Onhand: " + String.Format("{0:n}", Convert.ToDouble(Onhand.ToString() == "" ? "0" : Onhand.ToString())) +
                             "<br> OnOrder: " + String.Format("{0:n}", Convert.ToDouble(OnOrderQTY.ToString() == "" ? "0" : OnOrderQTY.ToString())) +
                             "<br> AllocatedQTY: " + String.Format("{0:n}", Convert.ToDouble(AllocatedQTY.ToString() == "" ? "0" : AllocatedQTY.ToString())) +
                             "<br> LatestPrice: " + String.Format("{0:n}", Convert.ToDouble(LatestPrice.ToString() == "" ? "0" : LatestPrice.ToString()));
                             
                }
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
            //ds.SelectCommand = string.Format("SELECT ItemCode,ColorCode,ClassCode,SizeCode,OnHand FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode 
            //+ "' and ColorCode = '"+ColorCode+"' and ClassCode = '"+ClassCode+"' and SizeCode = '"+SizeCode+"'");

            if (Warehouse.ToString() == "")
            {
                ds.SelectCommand = string.Format(" SELECT a.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode, "
              + "  b.OnHand,'' as warehousecode,'' as qty,b.OnOrder,b.OnAlloc,a.UpdatedPrice,a.UnitBase, ItemCategoryCode, ProductCategoryCode, ProductSubCatCode FROM "
             + "   Masterfile.[Item] a inner join  "
              + "  Masterfile.ItemDetail b on a.ItemCode=b.ItemCode "
              + "  left join "
              + "  Masterfile.itemwhdetail c on b.itemcode=c.itemcode and b.colorcode=c.colorcode and  "
              + "  b.sizecode=c.sizecode and b.classcode=c.classcode  where  "
                    + " A.itemcode='" + ItemCode + "' and b.Colorcode='" + ColorCode + "' AND b.ClassCode='" + ClassCode + "' AND b.SizeCode='" + SizeCode + "'");
            }

            else
            {
                ds.SelectCommand = string.Format(" SELECT a.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode,b.OnHand, "
            + "    c.warehousecode,c.qty,b.OnOrder,b.OnAlloc,a.UpdatedPrice,a.UnitBase, ItemCategoryCode, ProductCategoryCode, ProductSubCatCode FROM Masterfile.[Item] a inner join "
             + "   Masterfile.[ItemDetail] b on a.ItemCode=b.ItemCode "
              + "  left join "
              + "  Masterfile.itemwhdetail c on b.itemcode=c.itemcode and b.colorcode=c.colorcode and "
             + "   b.sizecode=c.sizecode and b.classcode=c.classcode  where  c.warehousecode='" + Warehouse + "'   AND "
                    + " A.itemcode='" + ItemCode + "' and b.Colorcode='" + ColorCode + "' AND b.ClassCode='" + ClassCode + "' AND b.SizeCode='" + SizeCode + "'");
            }




            DataView view = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            if (view.Count > 0)
            {
                ItemCode = view[0][0];
                ColorCode = view[0][1];
                ClassCode = view[0][2];
                SizeCode = view[0][3];
                Onhand = view[0][4];
                warehouseQTY = view[0][6];
                OnOrderQTY = view[0][7]; ;
                AllocatedQTY = view[0][8];
                LatestPrice = view[0][9];
                BaseUnit = view[0][10];
                ItemCategory = view[0][11];
                ProductCategory = view[0][12];
                ProductSubCategory = view[0][13];
            }
            else
            {
                if (Warehouse.ToString() != "")
                {
                    DataTable dtItem = Gears.RetriveData2(" SELECT a.ItemCode,b.ColorCode,b.ClassCode,b.SizeCode,b.OnHand, "
                + "   0 as WHQty, b.OnOrder,b.OnAlloc,a.UpdatedPrice,a.UnitBase, ItemCategoryCode, ProductCategoryCode, ProductSubCatCode FROM Masterfile.[Item] a inner join "
                 + "   Masterfile.[ItemDetail] b on a.ItemCode=b.ItemCode "
                      + " WHERE A.itemcode='" + ItemCode + "' and b.Colorcode='" + ColorCode + "' AND b.ClassCode='" + ClassCode + "' AND b.SizeCode='" + SizeCode + "'", Session["ConnString"].ToString());

                    if(dtItem.Rows.Count > 0)
                    {
                        ItemCode = dtItem.Rows[0][0];
                        ColorCode = dtItem.Rows[0][1];
                        ClassCode = dtItem.Rows[0][2];
                        SizeCode = dtItem.Rows[0][3];
                        Onhand = dtItem.Rows[0][4];
                        warehouseQTY = dtItem.Rows[0][5];
                        OnOrderQTY = dtItem.Rows[0][6]; ;
                        AllocatedQTY = dtItem.Rows[0][7];
                        LatestPrice = dtItem.Rows[0][8];
                        BaseUnit = dtItem.Rows[0][9];
                        ItemCategory = view[0][10];
                        ProductCategory = view[0][11];
                        ProductSubCategory = view[0][12];
                    }
                }
            }
        }
    }
}