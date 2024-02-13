using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
using DevExpress.Web;
using System.Globalization;
using System.Text.RegularExpressions;
namespace GWL
{
    public partial class frmChequeDesginer : System.Web.UI.Page
    {
        public static DataTable TableData = new DataTable(); 
       protected void Page_Load(object sender, EventArgs e)
       {
            //get request query
            BankID.Value = Request.QueryString["myParam1"].ToString();

            string payeenameDecode = Request.QueryString["PayeeName"].ToString();

            payeenameDecode = payeenameDecode.Replace('|', '&');

            PayeeName.Value = payeenameDecode;
            SupplierCode.Value = Request.QueryString["SupplierCode"].ToString();

            double amount = Convert.ToDouble(Request.QueryString["CheckAmount"].ToString());
            string formattedCheckAmount = String.Format("{0:N}", amount);

            CheckAmount.Value = formattedCheckAmount;

            DocNumber.Value = Request.QueryString["DocNumber"].ToString();
            CheckNumber.Value = Request.QueryString["CheckNumber"].ToString();
            var isCheck = Request.QueryString["IsCross"].ToString();

            string CheckDateValue = Request.QueryString["CheckDate"].ToString();


            DataTable dtdata = Gears.RetriveData2("SELECT Field1,Field2 FROM  MasterFile.BankAccount where BankAccountCode = '" + BankID.Value + "'", Session["ConnString"].ToString());

            DataTable checkFNT = Gears.RetriveData2("select Value from IT.SystemSettings where Code = 'CHKFNTNAME'", Session["ConnString"].ToString());

            DataTable checkFNTSZ = Gears.RetriveData2("select Value from IT.SystemSettings where Code = 'CHKFNTSIZE'", Session["ConnString"].ToString());

            if(checkFNT.Rows.Count > 0)
            {
                Font.Value = checkFNT.Rows[0][0].ToString();
            }
            else
            {
                Font.Value = "Lucida Fax";
            }

            if (checkFNTSZ.Rows.Count > 0)
            {
                FontSize.Value = checkFNTSZ.Rows[0][0].ToString();
            }
            else
            {
                Font.Value = "13";
            }

            var isChequeFormat = "";

            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0][1].ToString() != null || dtdata.Rows[0][1].ToString() != "")
                {
                    letterSpacing.Value = dtdata.Rows[0][1].ToString();
                }
                
                if(dtdata.Rows[0][0].ToString() != null || dtdata.Rows[0][0].ToString() != "")
                {
                    isChequeFormat = dtdata.Rows[0][0].ToString();
                }
            }

            //check if available or not
            if (isCheck == "True")
            {
                remarks.Visible = true;

            }
            else
            {
                remarks.Visible = false;
            }


           if (isChequeFormat == null || isChequeFormat == "")
                {
                    string checkDate = Convert.ToDateTime(CheckDateValue).ToString("MMMM dd,yyyy");
                    dateSpan.InnerText = checkDate.ToString();
                }
           else
           {
               DateTime temp;

               try
               {
                   DateTime.TryParseExact(CheckDateValue, isChequeFormat, null, DateTimeStyles.None, out temp);
                   string checkDate = Convert.ToDateTime(CheckDateValue).ToString(isChequeFormat);
                   string defaultcheckDate = checkDate.Replace("/", " ");
                   dateSpan.InnerText = defaultcheckDate.ToString();
               }
               catch (Exception)
               {
                   string checkDate = Convert.ToDateTime(CheckDateValue).ToString("MMMM dd, yyyy");
                   dateSpan.InnerText = checkDate.ToString();
               }
                   

               //if (DateTime.TryParseExact(CheckDateValue, isChequeFormat, null, DateTimeStyles.None, out temp) == true)
               //{
               //    string checkDate = Convert.ToDateTime(CheckDateValue).ToString(isChequeFormat);
               //    string defaultcheckDate = checkDate.Replace("/", " ");
               //    dateSpan.InnerText = defaultcheckDate.ToString();
               //}
               //else
               //{
               //    string checkDate = Convert.ToDateTime(CheckDateValue).ToString("MMMM dd, yyyy");
               //    dateSpan.InnerText = checkDate.ToString();
               //}
           }

            CheckAmountW.Value = NumberToWord(Convert.ToDecimal(CheckAmount.Value));
        }

       public static string NumChar(int UseNUM)
       {
           int NUM = UseNUM;

           int H = (NUM / 100);
           NUM = NUM - (H * 100);
           int T = (NUM / 10);
           NUM = NUM - (T * 10);
           int O = NUM;

           string HUNDREDS = "";
           string TENS = "";
           string ONES = "";

           switch (H)
           {
               case 1:
                   HUNDREDS = "one hundred ";
                   break;
               case 2:
                   HUNDREDS = "two hundred ";
                   break;
               case 3:
                   HUNDREDS = "three hundred ";
                   break;
               case 4:
                   HUNDREDS = "four hundred ";
                   break;
               case 5:
                   HUNDREDS = "five hundred ";
                   break;
               case 6:
                   HUNDREDS = "six hundred ";
                   break;
               case 7:
                   HUNDREDS = "seven hundred ";
                   break;
               case 8:
                   HUNDREDS = "eight hundred ";
                   break;
               case 9:
                   HUNDREDS = "nine hundred ";
                   break;
               default:
                   HUNDREDS = "";
                   break;
           }

           switch (T)
           {
               case 1:
                   if (O == 0)
                   {
                       TENS = "ten ";
                   }
                   else
                   {
                       switch (O)
                       {
                           case 1:
                               TENS = "eleven ";
                               break;
                           case 2:
                               TENS = "twelve ";
                               break;
                           case 3:
                               TENS = "thirteen ";
                               break;
                           case 4:
                               TENS = "fourteen ";
                               break;
                           case 5:
                               TENS = "fifteen ";
                               break;
                           case 6:
                               TENS = "sixteen ";
                               break;
                           case 7:
                               TENS = "seventeen ";
                               break;
                           case 8:
                               TENS = "eighteen ";
                               break;
                           case 9:
                               TENS = "nineteen ";
                               break;
                       }
                   }

                   break;
               case 2:
                   TENS = "twenty ";
                   break;
               case 3:
                   TENS = "thirty ";
                   break;
               case 4:
                   TENS = "forty ";
                   break;
               case 5:
                   TENS = "fifty ";
                   break;
               case 6:
                   TENS = "sixty ";
                   break;
               case 7:
                   TENS = "seventy ";
                   break;
               case 8:
                   TENS = "eighty ";
                   break;
               case 9:
                   TENS = "ninety ";
                   break;
               default:
                   TENS = "";
                   break;

           }

           if (T == 1)
           {
               ONES = "";
           }
           else
           {
               switch (O)
               {
                   case 1:
                       ONES = "one ";
                       break;
                   case 2:
                       ONES = "two ";
                       break;
                   case 3:
                       ONES = "three ";
                       break;
                   case 4:
                       ONES = "four ";
                       break;
                   case 5:
                       ONES = "five ";
                       break;
                   case 6:
                       ONES = "six ";
                       break;
                   case 7:
                       ONES = "seven ";
                       break;
                   case 8:
                       ONES = "eight ";
                       break;
                   case 9:
                       ONES = "nine ";
                       break;
                   default:
                       ONES = "";
                       break;
               }
           }
           return HUNDREDS + TENS + ONES;
       }

       public static string NumberToWord(decimal UsedNumber)
       {
           //123,456,789,012.45

           int intWhole = Convert.ToInt32(Math.Floor(UsedNumber));

           decimal dTemp = intWhole;
           int iBIL = Convert.ToInt32(Math.Floor(dTemp / 1000000000)); // Billion 1,000,000,000
           dTemp = dTemp % 1000000000; // Billion 1,000,000,000

           int iMIL = Convert.ToInt32(Math.Floor(dTemp / 1000000)); // Million 1,000,000
           dTemp = dTemp % 1000000; // Million 1,000,000

           int iTHO = Convert.ToInt32(Math.Floor(dTemp / 1000)); // Thousand 1,000
           dTemp = dTemp % 1000; // Thousand 1,000

           int iHUN = Convert.ToInt32(dTemp); // Hundred 100
           dTemp = dTemp - Convert.ToInt32(dTemp);

           int iCEN = Convert.ToInt32((UsedNumber - intWhole) * 100); // Centavo 1

           string CBILLIONS = "";
           string CMILLIONS = "";
           string CTHOUSANDS = "";
           string CHUNDREDS = "";
           string CENTAVOS = "";

           string TmpString = "";
           //billion
           TmpString = NumChar(iBIL);
           if (!string.IsNullOrEmpty(TmpString)) CBILLIONS = TmpString + "billion ";
           //million
           TmpString = NumChar(iMIL);
           if (!string.IsNullOrEmpty(TmpString)) CMILLIONS = TmpString + "million ";
           //thousand
           TmpString = NumChar(iTHO);
           if (!string.IsNullOrEmpty(TmpString)) CTHOUSANDS = TmpString + "thousand ";
           //hundred
           TmpString = NumChar(iHUN);
           if (!string.IsNullOrEmpty(TmpString)) CHUNDREDS = TmpString;
           //centavo
           TmpString = iCEN.ToString().Trim();
           if (!string.IsNullOrEmpty(TmpString) & TmpString.ToString().Trim() != "0") CENTAVOS = TmpString + "/100";

           string TmpN = "";

           if (string.IsNullOrEmpty(CENTAVOS))
           {
               TmpN = CBILLIONS + CMILLIONS + CTHOUSANDS + CHUNDREDS;
           }
           else
           {
               TmpN = CBILLIONS + CMILLIONS + CTHOUSANDS + CHUNDREDS + " and " + CENTAVOS;
           }

           TmpN = TmpN.Trim();
           if (string.IsNullOrEmpty(TmpN)) TmpN = "zero";

           string TmpN1 = "", TmpN2 = "";
           TmpN1 = (TmpN.Substring(0, 1)).ToUpper();
           TmpN2 = TmpN.Substring(1) + " only";

           return TmpN1 + TmpN2;
       }

        //Retrieve Coordinates FROM DB via BankAccountCode
        [WebMethod]
        public static Cheques[] GetData(string ID) 
        {
        var details = new List<Cheques>();

        DataTable dtdata = Gears.RetriveData2("SELECT * FROM [Masterfile].[BankAccount] where BankAccountCode = '" + ID + "'", HttpContext.Current.Session["ConnString"].ToString());

                details.AddRange(from DataRow dtrow in dtdata.Rows
                        select new Cheques  
                    {
                        ID = dtrow["BankAccountCode"].ToString().Trim(),
                        DateX = dtrow["DateX"].ToString().Trim(),
                        DateY = dtrow["DateY"].ToString().Trim(),
                        AmountWX = dtrow["AmountWX"].ToString().Trim(),
                        AmountWY = dtrow["AmountWY"].ToString().Trim(),
                        AmountNX = dtrow["AmountNX"].ToString().Trim(),
                        AmountNY = dtrow["AmountNY"].ToString().Trim(),
                        PayeeX = dtrow["PayeeX"].ToString().Trim(),
                        PayeeY = dtrow["PayeeY"].ToString().Trim(),
                        CheckWidth = dtrow["CheckWidth"].ToString().Trim(),
                        CheckHeight = dtrow["CheckHeight"].ToString().Trim(),
                        RemarksX = dtrow["RemarksX"].ToString().Trim(),
                        RemarksY = dtrow["RemarksY"].ToString().Trim(),
                    });

        return details.ToArray();  
        }

        public class Cheques
        {
            public string ID { get; set; }
            public string BankName { get; set; }
            public string DateX { get; set; }
            public string DateY { get; set; }
            public string AmountWX { get; set; }
            public string AmountWY { get; set; }
            public string AmountNX { get; set; }
            public string AmountNY { get; set; }
            public string PayeeX { get; set; }
            public string PayeeY { get; set; }
            public string CheckWidth { get; set; }
            public string CheckHeight { get; set; }
            public string RemarksX { get; set; }
            public string RemarksY { get; set; }
        }

        public class User
        {
            public string DocNumber { get; set; }
            public string CheckNumber { get; set; }
            public string BankID { get; set; }
        }

        [WebMethod]
        public static void SaveUser(User user)
        {
            DataTable dtUpdateIsPrinted = Gears.RetriveData2("UPDATE Accounting.CheckVoucherDetail set IsPrinted = 1 where DocNumber = '" + user.DocNumber + "' and BankAccount = '" + user.BankID + "' and CheckNumber = '" + user.CheckNumber + "'", HttpContext.Current.Session["ConnString"].ToString());
        }

    

        //UPDATE Coordinates
        protected void update_Click(object sender, EventArgs e)
        {
            DataTable dtUpdateIsPrinted2 = Gears.RetriveData2("UPDATE Masterfile.BankAccount SET Field3= '" + letterSpacing.Value + "', Field2= '" + letterSpacing.Value + "', PayeeX = '" + Math.Round(Convert.ToDouble(PayeeX.Value)) + "', PayeeY = '" + Math.Round(Convert.ToDouble(PayeeY.Value)) + "', AmountNX = '" + Math.Round(Convert.ToDouble(AmountNX.Value)) + "', AmountNY = '" + Math.Round(Convert.ToDouble(AmountNY.Value)) + "', AmountWX = '" + Math.Round(Convert.ToDouble(AmountWX.Value)) + "', AmountWY = '" + Math.Round(Convert.ToDouble(AmountWY.Value)) + "', DateX = '" + Math.Round(Convert.ToDouble(DateX.Value)) + "', DateY = '" + Math.Round(Convert.ToDouble(DateY.Value)) + "', RemarksX = '" + Math.Round(Convert.ToDouble(RemarksX.Value)) + "', RemarksY = '" + Math.Round(Convert.ToDouble(RemarksY.Value)) + "'  WHERE BankAccountCode = '" + BankID.Value + "'", HttpContext.Current.Session["ConnString"].ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Updated Successfully');", true);
        }

        //UPDATE isPrinted 
        protected void print_Click(object sender, EventArgs e)
        {
            DataTable dtUpdateIsPrinted = Gears.RetriveData2("UPDATE Accounting.CheckVoucherDetail set IsPrinted = 1 where DocNumber = '" + DocNumber.Value + "' and BankAccount = '" + BankID.Value + "' and CheckNumber = '" + CheckNumber.Value + "'", HttpContext.Current.Session["ConnString"].ToString());
        }

    }
}