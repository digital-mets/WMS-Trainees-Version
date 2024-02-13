using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;

namespace Entity
{
    public class TRRSubsi
    {
        private static string Connection;
        private static string RefDocNumber;
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
        public virtual decimal UsedQty { get; set; } //RequiredQty
        public virtual string Width { get; set; } 
        public virtual string Shade { get; set; }
        public virtual decimal Shrinkage { get; set; }
        public virtual string CostTransType { get; set; } //TransType
        public virtual string CostTransDoc { get; set; } //TransDoc
        public virtual string CostTransLine { get; set; } //TransLine
        public virtual string CostLineNumber { get; set; } //LineNumber
        public virtual decimal CostBaseQty { get; set; } //RRQty
        public virtual string Warehouse { get; set; }
        public virtual decimal RemainingQty { get; set; }
        //public virtual DateTime RRDate { get; set; }


        public DataTable getdetail(string DocNumber, string LineNumber, string TransType, string Conn, string refdocnum, string ItemCode, string ColorCode, string ClassCode, string SizeCode)
        {
            Connection = Conn;
            RefDocNumber = refdocnum;
            DataTable a;
            try
            {
                if (!String.IsNullOrEmpty(refdocnum))
                {
                    a = Gears.RetriveData2("EXEC [sp_Generate_TRR] 1,'','','','" + refdocnum + "','" + ItemCode + "','" + ColorCode + "','" + ClassCode + "','" + SizeCode + "','','','','','',''", Conn);
                }
                else
                {
                    a = Gears.RetriveData2("EXEC [sp_Generate_TRR] 2,'" + DocNumber + "','" + LineNumber + "','" + TransType + "','','" + ItemCode + "','" + ColorCode + "','" + ClassCode + "','" + SizeCode + "','','','','','',''", Conn);
                }
                return a;
            }
            catch (Exception e)
            {
                a = null;
                return a;
            }
        }

        public void GetTableName(string tablename, string columnname, string Conn)
        {
            TableName = tablename;
            ColumnName = columnname;
            Connection = Conn;
        }

        public void UpdateTRRSubsi(TRRSubsi A)
        {
            
            if (A.UsedQty != 0 && !String.IsNullOrEmpty(RefDocNumber))
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", A.CostTransDoc);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", A.CostTransLine);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", A.CostTransType);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "LineNumber", A.CostLineNumber);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "ItemCode", A.ItemCode);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "ColorCode", A.ColorCode);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "ClassCode", A.ClassCode);
                DT1.Rows.Add("WMS.CountSheetSubsi", "cond", "SizeCode", A.SizeCode);
                DT1.Rows.Add("WMS.CountSheetSubsi", "set", "UsedQty", A.UsedQty);
                Gears.UpdateData(DT1, Connection);
            }
            else if (A.UsedQty != 0 && String.IsNullOrEmpty(RefDocNumber))
            {
                Gears.RetriveData2("DELETE B "
                + " FROM WMS.CountSheetSubsi B INNER JOIN " + TableName + " A "
                + " ON B.TransDoc = A.DocNumber "
                + " AND (RTRIM(LTRIM(B.ItemCode)) != RTRIM(LTRIM(A.ItemCode)) "
                + " OR RTRIM(LTRIM(B.ColorCode)) != RTRIM(LTRIM(A.ColorCode)) "
                + " OR RTRIM(LTRIM(B.ClassCode)) != RTRIM(LTRIM(A.ClassCode)) "
                + " OR RTRIM(LTRIM(B.SizeCode)) != RTRIM(LTRIM(A.SizeCode))) "
                + " AND B.TransLine = A.LineNumber"
                + " WHERE B.TransDoc = '" + A.TransDoc + "'", Connection);
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DataTable RT = Gears.RetriveData2("EXEC [sp_Generate_TRR] 3,'','','" + A.TransType + "','','" + A.ItemCode + "','" + A.ColorCode + "','" + A.ClassCode + "','" + A.SizeCode + "','" + A.TransDoc + "','" + A.TransLine + "','" + A.CostTransType + "','" + A.CostTransDoc + "','" + A.CostTransLine + "','" + A.CostLineNumber + "'", Connection);
                
                if (RT.Rows.Count > 0)
                {
                    foreach (DataRow i in RT.Rows)
                    {
                        string strLine2 = i.ToString().PadLeft(5, '0');
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "TransDoc", A.TransDoc);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", A.TransLine);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", A.TransType);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "ItemCode", A.ItemCode);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "ColorCode", A.ColorCode);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "ClassCode", A.ClassCode);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "SizeCode", A.SizeCode);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "CostTransType", A.CostTransType);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "CostTransDoc", A.CostTransDoc);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "CostTransLine", A.CostTransLine);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "cond", "CostLineNumber", A.CostLineNumber);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "set", "CostBaseQty", A.CostBaseQty);
                        DT2.Rows.Add("WMS.CountSheetSubsi", "set", "UsedQty", A.UsedQty);
                        Gears.UpdateData(DT2, Connection);
                    }
                }
                else
                {
                    DataTable RT1 = Gears.RetriveData2("EXEC [sp_Generate_TRR] 4,'','','','','" + A.ItemCode + "','" + A.ColorCode + "','" + A.ClassCode + "','" + A.SizeCode + "','" + A.TransDoc + "','','','','',''", Connection);
                    
                    string strLineNumber = (RT1.Rows.Count + 1).ToString().PadLeft(5, '0');
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "TransDoc", A.TransDoc);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "TransLine", A.TransLine);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "TransType", A.TransType);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "LineNumber", strLineNumber);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "ItemCode", A.ItemCode);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "ColorCode", A.ColorCode);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "ClassCode", A.ClassCode);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "SizeCode", A.SizeCode);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "UsedQty", A.UsedQty);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "CostTransType", A.CostTransType);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "CostTransDoc", A.CostTransDoc);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "CostTransLine", A.CostTransLine);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "CostLineNumber", A.CostLineNumber);
                    DT2.Rows.Add("WMS.CountSheetSubsi", "0", "CostBaseQty", A.CostBaseQty);
                    Gears.CreateData(DT2, Connection);
                    DT2.Rows.Clear();
                }
            }
            else if (A.UsedQty == 0)
            {
                Gears.RetriveData2("EXEC [sp_Generate_TRR] 5,'','','" + A.TransType + "','','" + A.ItemCode + "','" + A.ColorCode + "','" + A.ClassCode + "','" + A.SizeCode + "','" + A.TransDoc + "','" + A.TransLine + "','" + A.CostTransType + "','" + A.CostTransDoc + "','" + A.CostTransLine + "','" + A.CostLineNumber + "'", Connection);
            }
        }
    }
}
