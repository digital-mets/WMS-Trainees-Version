using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;

namespace Entity
{
        public class CountSheetSubsi
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
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal UsedQty { get; set; }
            public virtual decimal SystemQty { get; set; }
            public virtual decimal VarianceQty { get; set; }
            public virtual string Location { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual DateTime ExpirationDate { get; set; }
            public virtual DateTime MfgDate { get; set; }
            public virtual DateTime RRdate { get; set; }
            public virtual string ToLoc { get; set; }
            public virtual string PalletID { get; set; }
            public virtual string ToPalletID { get; set; }
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
            public virtual DateTime DocDate { get; set; }
            public virtual string CostTransType { get; set; }
            public virtual string CostTransDoc { get; set; }
            public virtual string CostTransLine { get; set; }
            public virtual string CostLineNumber { get; set; }
            public virtual decimal CostBaseQty { get; set; }
            public virtual decimal CostBulkQty { get; set; }
            public virtual decimal DocBaseQty { get; set; }
            public virtual decimal DocBulkQty { get; set; }
            public virtual decimal UnitCost { get; set; }

            public DataTable getdetail(string DocNumber,string LineNumber, string TransType,string Conn)
            {
                DataTable a;
                try
                {
                    if (LineNumber == "null")
                    {
                        a = Gears.RetriveData2("select * from wms.countsheetsubsi where TransDoc='" + DocNumber + "' and TransType = '" + TransType + "' order by TransLine asc",Conn);
                    }
                    else
                    {
                        a = Gears.RetriveData2("select * from wms.countsheetsubsi where TransDoc='" + DocNumber + "' and TransLine = '" + LineNumber + "' and TransType = '" + TransType + "' order by LineNumber asc",Conn);
                    }
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void GetTableName(string tablename,string columnname,string Conn)
            {
                TableName = tablename;
                ColumnName = columnname;
                Connection = Conn;
            }

            public void UpdateCountSheetSubsi(CountSheetSubsi CountSheetSubsi)
            {
                int i = Convert.ToInt32(CountSheetSubsi.UsedQty);
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", CountSheetSubsi.TransDoc);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", CountSheetSubsi.TransLine);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", CountSheetSubsi.TransType);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "LineNumber", CountSheetSubsi.LineNumber);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "UsedQty", CountSheetSubsi.UsedQty);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "SystemQty", CountSheetSubsi.SystemQty);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "VarianceQty", CountSheetSubsi.VarianceQty);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "Location", CountSheetSubsi.Location);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "ExpirationDate", CountSheetSubsi.ExpirationDate);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "MfgDate", CountSheetSubsi.MfgDate);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "ToLoc", CountSheetSubsi.ToLoc);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "PalletID", CountSheetSubsi.PalletID);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "ToPalletID", CountSheetSubsi.ToPalletID);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "DocBulkQty", CountSheetSubsi.DocBulkQty);

                Gears.UpdateData(DT1,Connection);

                DataTable getqty = GearsLibrary.Gears.RetriveData2("select SUM(isnull(UsedQty,0)) from wms.CountSheetSubsi where TransDoc= " +
                "'" + CountSheetSubsi.TransDoc + "' and TransLine = '" + CountSheetSubsi.TransLine + "' and TransType = '" + CountSheetSubsi.TransType + "'"
                ,Connection);
                foreach (DataRow dt in getqty.Rows)
                {
                    uQty = Convert.ToDecimal(dt[0].ToString());
                }

                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT2.Rows.Add(TableName, "cond", "Docnumber", CountSheetSubsi.TransDoc);
                DT2.Rows.Add(TableName, "cond", "LineNumber", CountSheetSubsi.TransLine);
                DT2.Rows.Add(TableName, "set", ColumnName, uQty);

                Gears.UpdateData(DT2,Connection);

                uQty = 0;

                if (TransType == "WMSPICK")
                {
                    Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                    DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", CountSheetSubsi.TransDoc);
                    DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", CountSheetSubsi.TransType);
                    DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", CountSheetSubsi.TransLine);
                    DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "LineNumber", CountSheetSubsi.LineNumber);
                    DT3.Rows.Add("WMS.CountSheetSubsi", "set", "CosTranstype", "");
                    DT3.Rows.Add("WMS.CountSheetSubsi", "set", "CostTransdoc", "");
                    DT3.Rows.Add("WMS.CountSheetSubsi", "set", "CostTransLine", "");
                    DT3.Rows.Add("WMS.CountSheetSubsi", "set", "CostLineNumber", "");


                    Gears.UpdateData(DT3,Connection);
                }
            }

            public void Generate(string DocNumber, string LineNumber, int from, int to, string pallet, string ExpDate, string MfgDate, string Qty,  string Trans)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                int counter = (to - from);

                for (int i = 0; i <= counter; i++)
                {
                    string strLine2 = from.ToString().PadLeft(5, '0');

                    DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", DocNumber);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", LineNumber);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", Trans);
                    DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "LineNumber", strLine2);
                    if (pallet != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSubsi", "set", "PalletID", pallet);
                    }
                    if (Qty != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSubsi", "set", "UsedQty", Qty);
                    }
                    if (ExpDate != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSubsi", "set", "ExpirationDate", ExpDate);
                    }
                    if (MfgDate != "")
                    {
                        DT1.Rows.Add("WMS.CountSheetSubsi", "set", "MfgDate", MfgDate);
                    }
                    from++;

                    Gears.UpdateData(DT1,Connection);
                    DT1.Rows.Clear();
                }

                DataTable getqty = GearsLibrary.Gears.RetriveData2("select SUM(isnull(UsedQty,0)) from wms.CountSheetSubsi where TransDoc= " +
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
