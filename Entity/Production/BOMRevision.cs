using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BOMRevision
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string ReferenceJO { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual IList<BOMRevisionDetail> Detail { get; set; }


        public class BOMRevisionDetail
        {
            public virtual BOMRevision Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string OldItemCode { get; set; }
            public virtual string OldColorCode { get; set; }
            public virtual string OldClassCode { get; set; }
            public virtual string OldSizeCode { get; set; }
            public virtual string ProductCode { get; set; }
            public virtual string ProductColor { get; set; }
            public virtual string ProductSize { get; set; }
            public virtual string NewItemCode { get; set; }
            public virtual string NewColorCode { get; set; }
            public virtual string NewClassCode { get; set; }
            public virtual string NewSizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual string Components { get; set; }
            public virtual decimal PerPieceConsumption { get; set; }
            public virtual decimal Consumption { get; set; }
            public virtual decimal AllowancePerc { get; set; }
            public virtual decimal AllowanceQty { get; set; }
            public virtual decimal RequiredQty { get; set; }
            public virtual decimal UnitCost { get; set; }
            public virtual bool IsMajorMaterial { get; set; }
            public virtual bool IsBulk { get; set; }
            public virtual bool IsExcluded { get; set; }
            public virtual bool IsRounded { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    //a = Gears.RetriveData2("SELECT * FROM Production.BOMRevisionDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    a = Gears.RetriveData2("SELECT DocNumber, LineNumber, StepCode, OldItemCode, OldColorCode, OldClassCode, OldSizeCode, ProductCode, ProductColor, ProductSize, "
                    + " NewItemCode, NewColorCode, NewClassCode, NewSizeCode, Unit, Components, PerPieceConsumption, Consumption, AllowancePerc, AllowanceQty, RequiredQty,"
                    + " UnitCost, IsMajorMaterial, IsBulk, IsExcluded, IsRounded, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, '2' AS Version  FROM Production.BOMRevisionDetail "
                    + " WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddBOMRevisionDetail(BOMRevisionDetail BOMRevisionDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT max(convert(int,LineNumber)) as LineNumber FROM Production.BOMRevisionDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "StepCode", BOMRevisionDetail.StepCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "OldItemCode", BOMRevisionDetail.OldItemCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "OldColorCode", BOMRevisionDetail.OldColorCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "OldClassCode", BOMRevisionDetail.OldClassCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "OldSizeCode", BOMRevisionDetail.OldSizeCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "ProductCode", BOMRevisionDetail.ProductCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "ProductColor", BOMRevisionDetail.ProductColor);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "ProductSize", BOMRevisionDetail.ProductSize);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "NewItemCode", BOMRevisionDetail.NewItemCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "NewColorCode", BOMRevisionDetail.NewColorCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "NewClassCode", BOMRevisionDetail.NewClassCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "NewSizeCode", BOMRevisionDetail.NewSizeCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Unit", BOMRevisionDetail.Unit);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Components", BOMRevisionDetail.Components);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "PerPieceConsumption", BOMRevisionDetail.PerPieceConsumption);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Consumption", BOMRevisionDetail.Consumption);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "AllowancePerc", BOMRevisionDetail.AllowancePerc);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "AllowanceQty", BOMRevisionDetail.AllowanceQty);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "RequiredQty", BOMRevisionDetail.RequiredQty);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "UnitCost", BOMRevisionDetail.UnitCost);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "IsMajorMaterial", BOMRevisionDetail.IsMajorMaterial);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "IsBulk", BOMRevisionDetail.IsBulk);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "IsExcluded", BOMRevisionDetail.IsExcluded);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "IsRounded", BOMRevisionDetail.IsRounded);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field1", BOMRevisionDetail.Field1);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field2", BOMRevisionDetail.Field2);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field3", BOMRevisionDetail.Field3);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field4", BOMRevisionDetail.Field4);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field5", BOMRevisionDetail.Field5);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field6", BOMRevisionDetail.Field6);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field7", BOMRevisionDetail.Field7);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field8", BOMRevisionDetail.Field8);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Field9", BOMRevisionDetail.Field9);
                DT1.Rows.Add("Production.BOMRevisionDetail", "0", "Version", "1");

                DT2.Rows.Add("Production.BOMRevision", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.BOMRevision", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateBOMRevisionDetail(BOMRevisionDetail BOMRevisionDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.BOMRevisionDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.BOMRevisionDetail", "cond", "LineNumber", BOMRevisionDetail.LineNumber);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "StepCode", BOMRevisionDetail.StepCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "OldItemCode", BOMRevisionDetail.OldItemCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "OldColorCode", BOMRevisionDetail.OldColorCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "OldClassCode", BOMRevisionDetail.OldClassCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "OldSizeCode", BOMRevisionDetail.OldSizeCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "ProductCode", BOMRevisionDetail.ProductCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "ProductColor", BOMRevisionDetail.ProductColor);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "ProductSize", BOMRevisionDetail.ProductSize);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "NewItemCode", BOMRevisionDetail.NewItemCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "NewColorCode", BOMRevisionDetail.NewColorCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "NewClassCode", BOMRevisionDetail.NewClassCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "NewSizeCode", BOMRevisionDetail.NewSizeCode);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Unit", BOMRevisionDetail.Unit);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Components", BOMRevisionDetail.Components);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "PerPieceConsumption", BOMRevisionDetail.PerPieceConsumption);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Consumption", BOMRevisionDetail.Consumption);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "AllowancePerc", BOMRevisionDetail.AllowancePerc);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "AllowanceQty", BOMRevisionDetail.AllowanceQty);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "RequiredQty", BOMRevisionDetail.RequiredQty);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "UnitCost", BOMRevisionDetail.UnitCost);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "IsMajorMaterial", BOMRevisionDetail.IsMajorMaterial);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "IsBulk", BOMRevisionDetail.IsBulk);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "IsExcluded", BOMRevisionDetail.IsExcluded);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "IsRounded", BOMRevisionDetail.IsRounded);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field1", BOMRevisionDetail.Field1);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field2", BOMRevisionDetail.Field2);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field3", BOMRevisionDetail.Field3);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field4", BOMRevisionDetail.Field4);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field5", BOMRevisionDetail.Field5);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field6", BOMRevisionDetail.Field6);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field7", BOMRevisionDetail.Field7);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field8", BOMRevisionDetail.Field8);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Field9", BOMRevisionDetail.Field9);
                DT1.Rows.Add("Production.BOMRevisionDetail", "set", "Version", "1");

                Gears.UpdateData(DT1, Conn);             
                 
            }
            public void DeleteBOMRevisionDetail(BOMRevisionDetail BOMRevisionDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.BOMRevisionDetail", "cond", "DocNumber", BOMRevisionDetail.DocNumber);
                DT1.Rows.Add("Production.BOMRevisionDetail", "cond", "LineNumber", BOMRevisionDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Production.BOMRevisionDetail WHERE DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.BOMRevision", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Production.BOMRevision", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public class JournalEntry
        {
            public virtual BOMRevision Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit, A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='PRDBRV' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public class RefTransaction
        {
            public virtual BOMRevision Parent { get; set; }
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
                    a = Gears.RetriveData2("select DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B on A.RMenuID = B.ModuleID inner join IT.MainMenu C on A.MenuID = C.ModuleID "
                                            + " where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDBRV' OR  A.TransType='PRDBRV') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public DataTable getdata(string DocNumber, string Conn)
        {                 
            DataTable a;
            a = Gears.RetriveData2("SELECT * FROM Production.BOMRevision WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                Type = dtRow["Type"].ToString();
                ReferenceJO = dtRow["ReferenceJO"].ToString();
                Remarks = dtRow["Remarks"].ToString();

                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();

                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();           
            }

            return a;
        }
        public void InsertData(BOMRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.BOMRevision", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.BOMRevision", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.BOMRevision", "0", "Type", _ent.Type);
            DT1.Rows.Add("Production.BOMRevision", "0", "ReferenceJO", _ent.ReferenceJO);
            DT1.Rows.Add("Production.BOMRevision", "0", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Production.BOMRevision", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.BOMRevision", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.BOMRevision", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.BOMRevision", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(BOMRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.BOMRevision", "cond", "DocNumber", Docnum);
            DT1.Rows.Add("Production.BOMRevision", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.BOMRevision", "set", "Type", _ent.Type);
            DT1.Rows.Add("Production.BOMRevision", "set", "ReferenceJO", _ent.ReferenceJO);
            DT1.Rows.Add("Production.BOMRevision", "set", "Remarks", _ent.Remarks);

            DT1.Rows.Add("Production.BOMRevision", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.BOMRevision", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Production.BOMRevision", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.BOMRevision", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDBRV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(BOMRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.BOMRevision", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Production.BOMRevisionDetail", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("PRDBRV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Production.BOMRevisionDetail WHERE DocNumber = '" + DocNumber + "' AND Version = '1'", Conn);
        }
    }
}
