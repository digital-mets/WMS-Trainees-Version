using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;

namespace Entity
{
    public class CountSheetSetup
    {
        private static string Connection;
        public static decimal uQty { get; set; }
        public static string TableName { get; set; }
        public static string ColumnName { get; set; }
        public virtual string TransType { get; set; }
        public virtual string TransDoc { get; set; }
        public virtual string TransLine { get; set; }
        public virtual string LineNumber { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual bool ClassCode { get; set; }
        public virtual string SizeCode { get; set; }
        public virtual string PalletID { get; set; }
        public virtual string BatchNumber { get; set; }
        public virtual string Location { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime MfgDate { get; set; }
        public virtual DateTime RRdate { get; set; }
        public virtual DateTime PutawayDate { get; set; }
        public virtual decimal OriginalBulkQty { get; set; }
        public virtual decimal OriginalBaseQty { get; set; }
        public virtual string OriginalLocation { get; set; }
        public virtual decimal RemainingBulkQty { get; set; }
        public virtual decimal RemainingBaseQty { get; set; }
        public virtual decimal PickedBulkQty { get; set; }
        public virtual decimal PickedBaseQty { get; set; }
        public virtual decimal ReservedBulkQty { get; set; }
        public virtual decimal ReservedBaseQty { get; set; }
        public virtual decimal OriginalCost { get; set; }
        public virtual string UnitCost { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string BarcodeNo { get; set; }

        public DataTable getdetail(string DocNumber, string LineNumber, string TransType,string Conn)
        {

            DataTable a;
            try
            {
                if (LineNumber == "null")
                {
                    a = Gears.RetriveData2("select * from wms.countsheetsetup where TransDoc='" + DocNumber + "' and TransType = '" + TransType + "' order by LineNumber asc",Conn);
                }
                else
                {
                    a = Gears.RetriveData2("select * from wms.countsheetsetup where TransDoc='" + DocNumber + "' and TransLine = '" + LineNumber + "' and TransType = 'WMSINB' order by LineNumber asc",Conn);
                }
                    return a;
            }
            catch (Exception e)
            {
                a = null;
                return a;
            }
        }

        public void GetTableName(string tablename, string columnname,string Conn)
        {
            TableName = tablename;
            ColumnName = columnname;
            Connection = Conn;
        }

        public void UpdateCountSheetSetup(CountSheetSetup CountSheetSetup)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransDoc", CountSheetSetup.TransDoc);
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransLine", CountSheetSetup.TransLine);
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransType", CountSheetSetup.TransType);
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "LineNumber", CountSheetSetup.LineNumber);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "PalletID", CountSheetSetup.PalletID);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "OriginalBaseQty", CountSheetSetup.OriginalBaseQty);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "ExpirationDate", CountSheetSetup.ExpirationDate);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Location", CountSheetSetup.Location);
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "MfgDate", CountSheetSetup.MfgDate);

            Gears.UpdateData(DT1,Connection);

            DataTable getqty = GearsLibrary.Gears.RetriveData2("select SUM(isnull(OriginalBaseQty,0)) from wms.CountSheetSetup where TransDoc= " +
                "'" + CountSheetSetup.TransDoc + "' and TransLine = '" + CountSheetSetup.TransLine + "' and TransType = '" + CountSheetSetup.TransType + "'"
                ,Connection);
            foreach (DataRow dt in getqty.Rows)
            {
                uQty = Convert.ToDecimal(dt[0].ToString());
            }

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add(TableName, "cond", "Docnumber", CountSheetSetup.TransDoc);
            DT2.Rows.Add(TableName, "cond", "LineNumber", CountSheetSetup.TransLine);
            DT2.Rows.Add(TableName, "set", ColumnName, uQty);

            Gears.UpdateData(DT2,Connection);

            uQty = 0;
        }

        public void Generate(string DocNumber, string LineNumber, int from, int to, string pallet, string ExpDate, string MfgDate, string Qty, string Trans)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            int counter = (to - from);

                    for(int i = 0; i <= counter; i++){
                    string strLine2 = from.ToString().PadLeft(5, '0');

                    DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransDoc", DocNumber);
                    DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransLine", LineNumber);
                    DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransType", Trans);
                    DT1.Rows.Add("WMS.CountSheetSetup", "cond", "LineNumber", strLine2);
                    if (pallet != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSetup", "set", "PalletID", pallet);
                    }
                    if (Qty != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSetup", "set", "OriginalBaseQty", Qty);
                    }
                    if (ExpDate != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSetup", "set", "ExpirationDate", ExpDate);
                    }
                    if (MfgDate != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSetup", "set", "MfgDate", MfgDate);
                    }
                    from++;

                    Gears.UpdateData(DT1,Connection);
                    DT1.Rows.Clear();
                    }

                    DataTable getqty = GearsLibrary.Gears.RetriveData2("select SUM(isnull(OriginalBaseQty,0)) from wms.CountSheetSetup where TransDoc= " +
                        "'" + DocNumber + "' and TransLine = '" + LineNumber + "' and TransType = '" + Trans + "'"
                        ,Connection);
                    foreach (DataRow dt in getqty.Rows)
                    {
                        uQty = Convert.ToDecimal(dt[0].ToString());
                    }

                    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                    DT2.Rows.Add(TableName, "cond", "Docnumber", DocNumber);
                    DT2.Rows.Add(TableName, "cond", "LineNumber", LineNumber);
                    DT2.Rows.Add(TableName, "set", ColumnName, uQty);

                    Gears.UpdateData(DT2,Connection);

                    uQty = 0;

        }
    }
}
