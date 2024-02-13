using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;
namespace Entity
{
    public class AssetDepreciationSetup
    {
        private static string propertynumber;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string PropertyNumber { get; set; }
        public virtual string TransType { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual string ClassCode { get; set; }
        public virtual string SizeCode { get; set; }
        public virtual string FullDesc { get; set; }
        public virtual string DateAcquired { get; set; }
        public virtual string DateRetired { get; set; }
        public virtual string StartOfDepreciation { get; set; }
        public virtual string Unit { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual string AssignedTo { get; set; }
        public virtual string Location { get; set; }
        public virtual decimal Life { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Department { get; set; }
        public virtual string ParentProperty { get; set; }
        public virtual decimal UnitCost { get; set; }
        public virtual string Status { get; set; }
        public virtual string DepreciationMethod { get; set; }


        public virtual decimal AcquisitionCost { get; set; }
        public virtual decimal SalvageValue { get; set; }
        public virtual decimal MonthlyDepreciation { get; set; }
        public virtual decimal AccumulatedDepreciation { get; set; }
        public virtual decimal AmountSold { get; set; }
        public virtual decimal BookValue { get; set; }


        public virtual string DepreciationAccountCode { get; set; }
        public virtual string DepreciationSubsiCode { get; set; }
        public virtual string DepreciationProfitCenter { get; set; }
        public virtual string DepreciationCostCenter { get; set; }
        public virtual string GainLossAccount { get; set; }
        public virtual string AccumulatedAccountCode { get; set; }
        public virtual string AccumulatedSubsiCode { get; set; }
        public virtual string AccumulatedProfitCenter { get; set; }
        public virtual string AccumulatedCostCenter { get; set; }


        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }

        //GC 09/02/2016 Added code
        public virtual bool IsParentSetup { get; set; }
        //end


        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }


        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }


        public virtual IList<AssetDepreciationSetupDetail> Detail { get; set; }
        public class AssetDepreciationSetupDetail
        {
            public virtual AssetDepreciationSetup Parent { get; set; }
            public virtual string PropertyNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual decimal Month { get; set; }
            public virtual decimal RunningBookValue { get; set; }
            public virtual decimal DepreciationAmount { get; set; }
            public virtual DateTime PostedDate { get; set; }
            public virtual string JournalVoucherNumber { get; set; }
            public virtual string Version { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string PropertyNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Accounting.AssetInvDetail where PropertyNumber='" + PropertyNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }


            public void AddAssetDepreciationSetupDetail(AssetDepreciationSetupDetail AssetDepreciationSetupDetail)
            {

                //int linenum = 0;
                int maxlinecount = 0;
                //int maxlinenum = 0;
                string date = "1/1/0001 12:00:00 AM";
                //bool isbybulk = false;



                DataTable maxline = Gears.RetriveData2("select min(convert(int,LineNumber)) as LineNumber from Accounting.AssetInvDetail where PropertyNumber = '" + propertynumber + "'", Conn);
                //DataTable maxcount = Gears.RetriveData2("select min(convert(int,Month)) As Month from Accounting.AssetInvDetail where PropertyNumber = '" + propertynumber + "'");
                //DataTable count = Gears.RetriveData2("select min(convert(int,LineNumber)) as LineNumber from Accounting.AssetInvDetail where PropertyNumber = '" + propertynumber + "'");

                //try
                //{
                //    maxlinenum = Convert.ToInt32(maxcount.Rows[0][0].ToString()) - 1;
                //}
                //catch
                //{
                //    maxlinenum = Convert.ToInt32(AssetDepreciationSetupDetail.Month);
                //}

                //if (maxlinenum != AssetDepreciationSetupDetail.Month)
                //{
                //    maxlinenum = Convert.ToInt32(AssetDepreciationSetupDetail.Month);
                //}


                // Setting Line Number
                try
                {
                    maxlinecount = Convert.ToInt32(maxline.Rows[0][0].ToString()) - 1;
                }
                catch
                {
                    maxlinecount = Convert.ToInt32(AssetDepreciationSetupDetail.Month);
                }

                if (maxlinecount != Convert.ToInt32(AssetDepreciationSetupDetail.Month))
                {
                    maxlinecount = Convert.ToInt32(AssetDepreciationSetupDetail.Month);
                    DataTable a = new DataTable();
                    a = Gears.RetriveData2("DELETE FROM Accounting.AssetInvDetail WHERE PropertyNumber = '" + propertynumber + "' AND Version = '1'", Conn);
                }

                //if (maxlinecount == maxlinenum)
                //{
                //    DataTable a = new DataTable();
                //    a = Gears.RetriveData2("DELETE FROM Accounting.AssetInvDetail WHERE PropertyNumber = '" + propertynumber + "' AND Version = '1'");
                //}

                // DataTable count = Gears.RetriveData2("select min(convert(int,LineNumber)) as LineNumber from Accounting.AssetInvDetail where PropertyNumber = '" + propertynumber + "'");

                // Set LineNumber
                //try
                //{
                //    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) - 1;
                //}
                //catch
                //{
                //    linenum = Convert.ToInt32(AssetDepreciationSetupDetail.Month);
                //}



                string strLine = maxlinecount.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();



                // date = (AssetDepreciationSetupDetail.PostedDate == Convert.ToDateTime(date)) ? null : AssetDepreciationSetupDetail.PostedDate;
                DT1.Rows.Add("Accounting.AssetInvDetail", "0", "PropertyNumber", propertynumber);
                DT1.Rows.Add("Accounting.AssetInvDetail", "0", "LineNumber", strLine);


                DT1.Rows.Add("Accounting.AssetInvDetail", "0", "Month", AssetDepreciationSetupDetail.Month);
                DT1.Rows.Add("Accounting.AssetInvDetail", "0", "RunningBookValue", AssetDepreciationSetupDetail.RunningBookValue);
                DT1.Rows.Add("Accounting.AssetInvDetail", "0", "DepreciationAmount", AssetDepreciationSetupDetail.DepreciationAmount);
                //DT1.Rows.Add("Accounting.AssetInvDetail", "0", "PostedDate", AssetDepreciationSetupDetail.PostedDate);
                //DT1.Rows.Add("Accounting.AssetInvDetail", "0", "JournalVoucherNumber", AssetDepreciationSetupDetail.JournalVoucherNumber);
                DT1.Rows.Add("Accounting.AssetInvDetail", "0", "Version", "1");


                DT2.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", propertynumber);
                DT2.Rows.Add("Accounting.AssetInv", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateAssetDepreciationSetupDetail(AssetDepreciationSetupDetail AssetDepreciationSetupDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.AssetInvDetail", "cond", "PropertyNumber", propertynumber);
                DT1.Rows.Add("Accounting.AssetInvDetail", "cond", "LineNumber", AssetDepreciationSetupDetail.LineNumber);


                DT1.Rows.Add("Accounting.AssetInvDetail", "set", "Month", AssetDepreciationSetupDetail.Month);
                DT1.Rows.Add("Accounting.AssetInvDetail", "set", "RunningBookValue", AssetDepreciationSetupDetail.RunningBookValue);
                DT1.Rows.Add("Accounting.AssetInvDetail", "set", "DepreciationAmount", AssetDepreciationSetupDetail.DepreciationAmount);
                //DT1.Rows.Add("Accounting.AssetInvDetail", "set", "PostedDate", AssetDepreciationSetupDetail.PostedDate);
                //DT1.Rows.Add("Accounting.AssetInvDetail", "set", "JournalVoucherNumber", AssetDepreciationSetupDetail.JournalVoucherNumber);
                DT1.Rows.Add("Accounting.AssetInvDetail", "set", "Version", "1");



                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteAssetDepreciationSetupDetail(AssetDepreciationSetupDetail AssetDepreciationSetupDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.AssetInvDetail", "cond", "DocNumber", AssetDepreciationSetupDetail.PropertyNumber);
                DT1.Rows.Add("Accounting.AssetInvDetail", "cond", "LineNumber", AssetDepreciationSetupDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Accounting.AssetInvDetail where PropertyNumber = '" + propertynumber + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", PropertyNumber);
                    DT2.Rows.Add("Accounting.AssetInv", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public class RefTransaction
        {
            public virtual AssetDepreciationSetup Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTADS' OR  A.TransType='ACTADS') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public DataTable getdata(string PropertyNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.AssetInv where PropertyNumber = '" + PropertyNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DataTable getlife = new DataTable();
                getlife = Gears.RetriveData2("select AssetLife from Masterfile.Item I Left Join Masterfile.ItemCategory IC ON I.ItemCategoryCode = IC.ItemCategoryCode WHERE ItemCode = '" + dtRow["ItemCode"].ToString() + "'", Conn);

                PropertyNumber = dtRow["PropertyNumber"].ToString();
                TransType = dtRow["TransType"].ToString();
                DocNumber = dtRow["DocNumber"].ToString();
                ItemCode = dtRow["ItemCode"].ToString();
                ColorCode = dtRow["ColorCode"].ToString();
                ClassCode = dtRow["ClassCode"].ToString();
                SizeCode = dtRow["SizeCode"].ToString();
                FullDesc = dtRow["Description"].ToString();
                DateAcquired = dtRow["DateAcquired"].ToString();
                DateRetired = dtRow["DateRetired"].ToString();
                StartOfDepreciation = dtRow["StartOfDepreciation"].ToString();
                Unit = dtRow["Unit"].ToString();
                Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                AssignedTo = dtRow["AssignedTo"].ToString();
                Location = dtRow["Location"].ToString();
                Life = Convert.ToDecimal(Convert.IsDBNull(dtRow["Life"]) ? getlife.Rows[0]["AssetLife"].ToString() : dtRow["Life"]);
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                Department = dtRow["Department"].ToString();
                ParentProperty = dtRow["ParentProperty"].ToString();
                UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                Status = dtRow["Status"].ToString();
                DepreciationMethod = dtRow["DepreciationMethod"].ToString();



                //AcquisitionCost = Convert.ToDecimal(dtRow["AcquisitionCost"].ToString());
                //SalvageValue = Convert.ToDecimal(dtRow["SalvageValue"].ToString());
                //MonthlyDepreciation = Convert.ToDecimal(dtRow["MonthlyDepreciation"].ToString());
                //AccumulatedDepreciation = Convert.ToDecimal(dtRow["AccumulatedDepreciation"].ToString());
                //AmountSold = Convert.ToDecimal(dtRow["AmountSold"].ToString());
                AcquisitionCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["AcquisitionCost"]) ? 0 : dtRow["AcquisitionCost"]);
                SalvageValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["SalvageValue"]) ? 0 : dtRow["SalvageValue"]);
                MonthlyDepreciation = Convert.ToDecimal(Convert.IsDBNull(dtRow["MonthlyDepreciation"]) ? 0 : dtRow["MonthlyDepreciation"]);
                AccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(dtRow["AccumulatedDepreciation"]) ? 0 : dtRow["AccumulatedDepreciation"]);
                AmountSold = Convert.ToDecimal(Convert.IsDBNull(dtRow["AmountSold"]) ? 0 : dtRow["AmountSold"]);
                BookValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["BooKValue"]) ? 0 : dtRow["BooKValue"]);


                DepreciationAccountCode = dtRow["DepreciationAccountCode"].ToString();
                DepreciationSubsiCode = dtRow["DepreciationSubsiCode"].ToString();
                DepreciationProfitCenter = dtRow["DepreciationProfitCenter"].ToString();
                DepreciationCostCenter = dtRow["DepreciationCostCenter"].ToString();
                GainLossAccount = dtRow["GainLossAccount"].ToString();
                AccumulatedAccountCode = dtRow["AccumulatedAccountCode"].ToString();
                AccumulatedSubsiCode = dtRow["AccumulatedSubsiCode"].ToString();
                AccumulatedProfitCenter = dtRow["AccumulatedProfitCenter"].ToString();
                AccumulatedCostCenter = dtRow["AccumulatedCostCenter"].ToString();


                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();


                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);
                IsParentSetup = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsParentSetup"]) ? false : dtRow["IsParentSetup"]);


                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();
            }

            return a;
        }
        public void InsertData(AssetDepreciationSetup _ent)
        {
            propertynumber = _ent.PropertyNumber;
            Conn = _ent.Connection;
            ////trans = _ent.TransType;
            ////ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AssetInv", "0", "PropertyNumber ", propertynumber);


            DT1.Rows.Add("Accounting.AssetInv", "0", "TransType ", _ent.TransType);
            DT1.Rows.Add("Accounting.AssetInv", "0", "DocNumber ", _ent.DocNumber);
            DT1.Rows.Add("Accounting.AssetInv", "0", "ItemCode ", _ent.ItemCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "ColorCode ", _ent.ColorCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "ClassCode ", _ent.ClassCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "SizeCode ", _ent.SizeCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Description ", _ent.FullDesc);
            DT1.Rows.Add("Accounting.AssetInv", "0", "DateAcquired ", _ent.DateAcquired);
            //DT1.Rows.Add("Accounting.AssetInv", "0", "DateRetired ", _ent.DateRetired);
            DT1.Rows.Add("Accounting.AssetInv", "0", "StartOfDepreciation ", _ent.StartOfDepreciation);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Qty  ", _ent.Qty);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AssignedTo  ", _ent.AssignedTo);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Location  ", _ent.Location);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Life  ", _ent.Life);
            DT1.Rows.Add("Accounting.AssetInv", "0", "WarehouseCode  ", _ent.WarehouseCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Department  ", _ent.Department);
            DT1.Rows.Add("Accounting.AssetInv", "0", "ParentProperty  ", _ent.ParentProperty);
            DT1.Rows.Add("Accounting.AssetInv", "0", "UnitCost  ", _ent.UnitCost);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Status  ", _ent.Status);
            DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationMethod  ", _ent.DepreciationMethod);




            DT1.Rows.Add("Accounting.AssetInv", "0", "AcquisitionCost   ", _ent.AcquisitionCost);
            DT1.Rows.Add("Accounting.AssetInv", "0", "SalvageValue   ", _ent.SalvageValue);
            DT1.Rows.Add("Accounting.AssetInv", "0", "MonthlyDepreciation   ", _ent.MonthlyDepreciation);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AccumulatedDepreciation   ", _ent.AccumulatedDepreciation);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AmountSold   ", _ent.AmountSold);
            DT1.Rows.Add("Accounting.AssetInv", "0", "BookValue   ", _ent.BookValue);


            DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationAccountCode    ", _ent.DepreciationAccountCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationSubsiCode    ", _ent.DepreciationSubsiCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationProfitCenter    ", _ent.DepreciationProfitCenter);
            DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationCostCenter    ", _ent.DepreciationCostCenter);
            DT1.Rows.Add("Accounting.AssetInv", "0", "GainLossAccount    ", _ent.GainLossAccount);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AccumulatedAccountCode    ", _ent.AccumulatedAccountCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AccumulatedSubsiCode    ", _ent.AccumulatedSubsiCode);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AccumulatedProfitCenter    ", _ent.AccumulatedProfitCenter);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AccumulatedCostCenter    ", _ent.AccumulatedCostCenter);
            DT1.Rows.Add("Accounting.AssetInv", "0", "IsParentSetup    ", _ent.IsParentSetup);

            DT1.Rows.Add("Accounting.AssetInv", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.AssetInv", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //DT1.Rows.Add("Accounting.AssetInv", "0", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetInv", "0", "IsWithDetail", "False");

            DT1.Rows.Add("Accounting.AssetInv", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AssetInv", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(AssetDepreciationSetup _ent)
        {
            propertynumber = _ent.PropertyNumber;
            Conn = _ent.Connection;
            //    //trans = _ent.TransType;
            //    //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber ", propertynumber);


            DT1.Rows.Add("Accounting.AssetInv", "set", "TransType ", _ent.TransType);
            DT1.Rows.Add("Accounting.AssetInv", "set", "DocNumber ", _ent.DocNumber);
            DT1.Rows.Add("Accounting.AssetInv", "set", "ItemCode ", _ent.ItemCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "ColorCode ", _ent.ColorCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "ClassCode ", _ent.ClassCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "SizeCode ", _ent.SizeCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Description ", _ent.FullDesc);
            DT1.Rows.Add("Accounting.AssetInv", "set", "DateAcquired ", _ent.DateAcquired);
            //DT1.Rows.Add("Accounting.AssetInv", "set", "DateRetired ", _ent.DateRetired);
            DT1.Rows.Add("Accounting.AssetInv", "set", "StartOfDepreciation ", _ent.StartOfDepreciation);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Qty  ", _ent.Qty);
            DT1.Rows.Add("Accounting.AssetInv", "set", "AssignedTo  ", _ent.AssignedTo);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Location  ", _ent.Location);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Life  ", _ent.Life);
            DT1.Rows.Add("Accounting.AssetInv", "set", "WarehouseCode  ", _ent.WarehouseCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Department  ", _ent.Department);
            DT1.Rows.Add("Accounting.AssetInv", "set", "ParentProperty  ", _ent.ParentProperty);
            DT1.Rows.Add("Accounting.AssetInv", "set", "UnitCost  ", _ent.UnitCost);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Status  ", _ent.Status);
            DT1.Rows.Add("Accounting.AssetInv", "set", "DepreciationMethod  ", _ent.DepreciationMethod);




            DT1.Rows.Add("Accounting.AssetInv", "set", "AcquisitionCost   ", _ent.AcquisitionCost);
            DT1.Rows.Add("Accounting.AssetInv", "set", "SalvageValue   ", _ent.SalvageValue);
            DT1.Rows.Add("Accounting.AssetInv", "set", "MonthlyDepreciation   ", _ent.MonthlyDepreciation);
            DT1.Rows.Add("Accounting.AssetInv", "set", "AccumulatedDepreciation   ", _ent.AccumulatedDepreciation);
            DT1.Rows.Add("Accounting.AssetInv", "set", "AmountSold   ", _ent.AmountSold);
            DT1.Rows.Add("Accounting.AssetInv", "set", "BookValue", _ent.BookValue);


            DT1.Rows.Add("Accounting.AssetInv", "set", "DepreciationAccountCode    ", _ent.DepreciationAccountCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "DepreciationSubsiCode    ", _ent.DepreciationSubsiCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "DepreciationProfitCenter    ", _ent.DepreciationProfitCenter);
            DT1.Rows.Add("Accounting.AssetInv", "set", "DepreciationCostCenter    ", _ent.DepreciationCostCenter);
            DT1.Rows.Add("Accounting.AssetInv", "set", "GainLossAccount    ", _ent.GainLossAccount);
            DT1.Rows.Add("Accounting.AssetInv", "set", "AccumulatedAccountCode    ", _ent.AccumulatedAccountCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "AccumulatedSubsiCode    ", _ent.AccumulatedSubsiCode);
            DT1.Rows.Add("Accounting.AssetInv", "set", "AccumulatedProfitCenter    ", _ent.AccumulatedProfitCenter);
            DT1.Rows.Add("Accounting.AssetInv", "set", "AccumulatedCostCenter    ", _ent.AccumulatedCostCenter);
            DT1.Rows.Add("Accounting.AssetInv", "set", "IsParentSetup    ", _ent.IsParentSetup);


            DT1.Rows.Add("Accounting.AssetInv", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.AssetInv", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            //DT1.Rows.Add("Accounting.AssetInv", "set", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetInv", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Accounting.AssetInv", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AssetInv", "set", "Field9", _ent.Field9);

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTADS", propertynumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(AssetDepreciationSetup _ent)
        {
            propertynumber = _ent.PropertyNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", _ent.PropertyNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTADS", propertynumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

        public void DeleteFirstData(string Number, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.AssetInvDetail WHERE PropertyNumber = '" + Number + "' AND Version = '1'", Conn);
        }
    }
}
