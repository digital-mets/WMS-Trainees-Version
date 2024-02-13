using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class DIS
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string DocNumber { get; set; }
        public virtual string DISNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string DueDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string PIS { get; set; }
        public virtual string Customer { get; set; }
        //public virtual string StyleNo { get; set; }
        public virtual string Color { get; set; }
        public virtual string OriginalDIS { get; set; }
        public virtual string JONumber { get; set; }
        public virtual string Brand { get; set; }
        public virtual string Category { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Fitting { get; set; }
        public virtual string Designer { get; set; }
        public virtual decimal DISQty { get; set; }
        public virtual decimal TotalDISDays { get; set; }
        public virtual string WashingInstruction { get; set; }
        public virtual string Specs { get; set; }
        public virtual string Shrinkage { get; set; }
        public virtual string Fabric { get; set; }
        public virtual string Remarks { get; set; }

        public virtual string StepTemplateCode { get; set; }
        public virtual decimal CSTLaborCost { get; set; }
        public virtual decimal CSTMaterialCost { get; set; }
        public virtual decimal CSTTotalDISCost { get; set; }

        public virtual string DateSent { get; set; }
        public virtual string ReturnedDate { get; set; }
        public virtual string LastDateSent { get; set; }
        public virtual string LastReturnedDate { get; set; }

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
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string StartDISBy { get; set; }
        public virtual string StartDISDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }

        //public virtual string PostedBy { get; set; }
        //public virtual string PostedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }


        public virtual IList<DISSizeBreakdown> SizeBreakdown { get; set; }
        public virtual IList<DISBillOfMaterial> BillOfMaterial { get; set; }
        public virtual IList<DISStep> Step { get; set; }

        #region Header

        public class RefTransaction
        {
            public virtual DIS Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='"+TransType+"' OR  A.TransType='"+TransType+"') ", Conn);
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

            a = Gears.RetriveData2("SELECT * FROM Production.DIS WHERE DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DISNumber = dtRow["DISNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                DueDate = dtRow["DueDate"].ToString();
                Type = dtRow["Type"].ToString();
                PIS = dtRow["PIS"].ToString();
                Customer = dtRow["Customer"].ToString();
                //StyleNo = dtRow["StyleNo"].ToString();
                Color = dtRow["Color"].ToString();
                OriginalDIS = dtRow["OriginalDIS"].ToString();
                JONumber = dtRow["JONumber"].ToString();
                Brand = dtRow["Brand"].ToString();
                Category = dtRow["Category"].ToString();
                Gender = dtRow["Gender"].ToString();
                Fitting = dtRow["Fitting"].ToString();
                Designer = dtRow["Designer"].ToString();
                DISQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["DISQty"]) ? 0 : dtRow["DISQty"]);
                TotalDISDays = Convert.ToInt32(Convert.IsDBNull(dtRow["TotalDISDays"]) ? 0 : dtRow["TotalDISDays"]);
                WashingInstruction = dtRow["WashingInstruction"].ToString();
                Specs = dtRow["Specs"].ToString();
                Shrinkage = dtRow["Shrinkage"].ToString();
                Fabric = dtRow["Fabric"].ToString();
                Remarks = dtRow["Remarks"].ToString();

                StepTemplateCode = dtRow["StepTemplateCode"].ToString();
                CSTLaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["CSTLaborCost"]) ? 0 : dtRow["CSTLaborCost"]);
                CSTMaterialCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["CSTMaterialCost"]) ? 0 : dtRow["CSTMaterialCost"]);
                CSTTotalDISCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["CSTTotalDISCost"]) ? 0 : dtRow["CSTTotalDISCost"]);

                DateSent = dtRow["DateSent"].ToString();
                LastDateSent = dtRow["LastDateSent"].ToString();
                ReturnedDate = dtRow["ReturnedDate"].ToString();
                LastReturnedDate = dtRow["LastReturnedDate"].ToString();

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
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                StartDISBy = dtRow["StartDISBy"].ToString();
                StartDISDate = dtRow["StartDISDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                //PostedBy = dtRow["PostedBy"].ToString();
                //PostedDate = dtRow["PostedDate"].ToString(); 
            }
            return a;
        }
        public void InsertData(DIS _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DIS", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.DIS", "0", "DISNumber", _ent.DISNumber);
            DT1.Rows.Add("Production.DIS", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.DIS", "0", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Production.DIS", "0", "Type", _ent.Type);
            DT1.Rows.Add("Production.DIS", "0", "PIS", _ent.PIS);
            DT1.Rows.Add("Production.DIS", "0", "Customer", _ent.Customer);
            //DT1.Rows.Add("Production.DIS", "0", "StyleNo", _ent.StyleNo);
            DT1.Rows.Add("Production.DIS", "0", "Color", _ent.Color);
            DT1.Rows.Add("Production.DIS", "0", "OriginalDIS", _ent.OriginalDIS);
            DT1.Rows.Add("Production.DIS", "0", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Production.DIS", "0", "Brand", _ent.Brand);
            DT1.Rows.Add("Production.DIS", "0", "Category", _ent.Category);
            DT1.Rows.Add("Production.DIS", "0", "Gender", _ent.Gender);
            DT1.Rows.Add("Production.DIS", "0", "Fitting", _ent.Fitting);
            DT1.Rows.Add("Production.DIS", "0", "Designer", _ent.Designer);
            DT1.Rows.Add("Production.DIS", "0", "DISQty", _ent.DISQty);
            DT1.Rows.Add("Production.DIS", "0", "TotalDISDays", _ent.TotalDISDays);
            DT1.Rows.Add("Production.DIS", "0", "WashingInstruction", _ent.WashingInstruction);
            DT1.Rows.Add("Production.DIS", "0", "Specs", _ent.Specs);
            DT1.Rows.Add("Production.DIS", "0", "Shrinkage", _ent.Shrinkage);
            DT1.Rows.Add("Production.DIS", "0", "Fabric", _ent.Fabric);
            DT1.Rows.Add("Production.DIS", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.DIS", "0", "StepTemplateCode", _ent.StepTemplateCode);
            DT1.Rows.Add("Production.DIS", "0", "CSTLaborCost", _ent.CSTLaborCost);
            DT1.Rows.Add("Production.DIS", "0", "CSTMaterialCost", _ent.CSTMaterialCost);
            DT1.Rows.Add("Production.DIS", "0", "CSTTotalDISCost", _ent.CSTTotalDISCost);
            DT1.Rows.Add("Production.DIS", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.DIS", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.DIS", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.DIS", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.DIS", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.DIS", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.DIS", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.DIS", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.DIS", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.DIS", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.DIS", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Production.DIS", "0", "IsWithDetail", "False");
            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(DIS _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.DIS", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.DIS", "set", "DISNumber", _ent.DISNumber);
            DT1.Rows.Add("Production.DIS", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.DIS", "set", "DueDate", _ent.DueDate);
            DT1.Rows.Add("Production.DIS", "set", "Type", _ent.Type);
            DT1.Rows.Add("Production.DIS", "set", "PIS", _ent.PIS);
            DT1.Rows.Add("Production.DIS", "set", "Customer", _ent.Customer);
            //DT1.Rows.Add("Production.DIS", "set", "StyleNo", _ent.StyleNo);
            DT1.Rows.Add("Production.DIS", "set", "Color", _ent.Color);
            DT1.Rows.Add("Production.DIS", "set", "OriginalDIS", _ent.OriginalDIS);
            DT1.Rows.Add("Production.DIS", "set", "JONumber", _ent.JONumber);
            DT1.Rows.Add("Production.DIS", "set", "Brand", _ent.Brand);
            DT1.Rows.Add("Production.DIS", "set", "Category", _ent.Category);
            DT1.Rows.Add("Production.DIS", "set", "Gender", _ent.Gender);
            DT1.Rows.Add("Production.DIS", "set", "Fitting", _ent.Fitting);
            DT1.Rows.Add("Production.DIS", "set", "Designer", _ent.Designer);
            DT1.Rows.Add("Production.DIS", "set", "DISQty", _ent.DISQty);
            DT1.Rows.Add("Production.DIS", "set", "TotalDISDays", _ent.TotalDISDays);
            DT1.Rows.Add("Production.DIS", "set", "WashingInstruction", _ent.WashingInstruction);
            DT1.Rows.Add("Production.DIS", "set", "Specs", _ent.Specs);
            DT1.Rows.Add("Production.DIS", "set", "Shrinkage", _ent.Shrinkage);
            DT1.Rows.Add("Production.DIS", "set", "Fabric", _ent.Fabric);
            DT1.Rows.Add("Production.DIS", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.DIS", "set", "StepTemplateCode", _ent.StepTemplateCode);
            DT1.Rows.Add("Production.DIS", "set", "CSTLaborCost", _ent.CSTLaborCost);
            DT1.Rows.Add("Production.DIS", "set", "CSTMaterialCost", _ent.CSTMaterialCost);
            DT1.Rows.Add("Production.DIS", "set", "CSTTotalDISCost", _ent.CSTTotalDISCost);
            DT1.Rows.Add("Production.DIS", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.DIS", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.DIS", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.DIS", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.DIS", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.DIS", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.DIS", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.DIS", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.DIS", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.DIS", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.DIS", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDDIS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(DIS _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.DIS", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT1, _ent.Connection);
            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Production.DISSizeBreakdown", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT2, _ent.Connection);
            Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            DT3.Rows.Add("Production.DISBillOfMaterial", "cond", "DocNumber", Docnum);
            Gears.DeleteData(DT3, _ent.Connection);
            Functions.AuditTrail("PRDDIS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        #endregion

        public class JournalEntry
        {
            public virtual DIS Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn, string TransType)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit, A.BizPartnerCode  FROM Production.GeneralLedger A "
                    + " INNER JOIN Production.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Production.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='"+TransType+"' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        #region Details
        public class DISSizeBreakdown
        {
            public virtual DIS Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Size { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.DISSizeBreakdown WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddDISSizeBreakdown(DISSizeBreakdown DISSizeBreakdown)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.DISSizeBreakdown WHERE DocNumber = '" + Docnum + "'", Conn);
                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }
                
                string strLine = linenum.ToString().PadLeft(5, '0');
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Size", DISSizeBreakdown.Size);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Qty", DISSizeBreakdown.Qty);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field1", DISSizeBreakdown.Field1);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field2", DISSizeBreakdown.Field2);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field3", DISSizeBreakdown.Field3);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field4", DISSizeBreakdown.Field4);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field5", DISSizeBreakdown.Field5);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field6", DISSizeBreakdown.Field6);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field7", DISSizeBreakdown.Field7);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field8", DISSizeBreakdown.Field8);
                DT1.Rows.Add("Production.DISSizeBreakdown", "0", "Field9", DISSizeBreakdown.Field9);
                DT2.Rows.Add("Production.DIS", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.DIS", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateDISSizeBreakdown(DISSizeBreakdown DISSizeBreakdown)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISSizeBreakdown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISSizeBreakdown", "cond", "LineNumber", DISSizeBreakdown.LineNumber);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Size", DISSizeBreakdown.Size);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Qty", DISSizeBreakdown.Qty);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field1", DISSizeBreakdown.Field1);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field2", DISSizeBreakdown.Field2);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field3", DISSizeBreakdown.Field3);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field4", DISSizeBreakdown.Field4);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field5", DISSizeBreakdown.Field5);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field6", DISSizeBreakdown.Field6);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field7", DISSizeBreakdown.Field7);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field8", DISSizeBreakdown.Field8);
                DT1.Rows.Add("Production.DISSizeBreakdown", "set", "Field9", DISSizeBreakdown.Field9);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteDISSizeBreakdown(DISSizeBreakdown DISSizeBreakdown)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISSizeBreakdown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISSizeBreakdown", "cond", "LineNumber", DISSizeBreakdown.LineNumber);
                Gears.DeleteData(DT1, Conn);
                DataTable count = Gears.RetriveData2("select * from Production.DISBillOfMaterial where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DataTable count2 = Gears.RetriveData2("select * from Production.DISSizeBreakdown where DocNumber = '" + Docnum + "'", Conn);
                    if (count2.Rows.Count < 1)
                    {
                        DataTable count3 = Gears.RetriveData2("select * from Production.DISStep where DocNumber = '" + Docnum + "'", Conn);
                        if (count3.Rows.Count < 1)
                        {
                            DT2.Rows.Add("Production.DIS", "cond", "DocNumber", Docnum);
                            DT2.Rows.Add("Production.DIS", "set", "IsWithDetail", "False");
                            Gears.UpdateData(DT2, Conn);
                        }
                    }
                }
            }
        }
        public class DISBillOfMaterial
        {
            public virtual DIS Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Step { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Consumption { get; set; }
            public virtual decimal Cost { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.DISBillOfMaterial WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddDISBillOfMaterial(DISBillOfMaterial DISBillOfMaterial)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.DISBillOfMaterial WHERE DocNumber = '" + Docnum + "'", Conn);

                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }

                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "DocNumber ", Docnum);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Step", DISBillOfMaterial.Step);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "ItemCode", DISBillOfMaterial.ItemCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "ColorCode", DISBillOfMaterial.ColorCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "ClassCode", DISBillOfMaterial.ClassCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "SizeCode", DISBillOfMaterial.SizeCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Consumption", DISBillOfMaterial.Consumption);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Cost", DISBillOfMaterial.Cost);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field1", DISBillOfMaterial.Field1);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field2", DISBillOfMaterial.Field2);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field3", DISBillOfMaterial.Field3);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field4", DISBillOfMaterial.Field4);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field5", DISBillOfMaterial.Field5);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field6", DISBillOfMaterial.Field6);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field7", DISBillOfMaterial.Field7);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field8", DISBillOfMaterial.Field8);
                DT1.Rows.Add("Production.DISBillOfMaterial", "0", "Field9", DISBillOfMaterial.Field9);
                DT2.Rows.Add("Production.DIS", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.DIS", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateDISBillOfMaterial(DISBillOfMaterial DISBillOfMaterial)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISBillOfMaterial", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISBillOfMaterial", "cond", "LineNumber", DISBillOfMaterial.LineNumber);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Step", DISBillOfMaterial.Step);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "ItemCode", DISBillOfMaterial.ItemCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "ColorCode", DISBillOfMaterial.ColorCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "ClassCode", DISBillOfMaterial.ClassCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "SizeCode", DISBillOfMaterial.SizeCode);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Consumption", DISBillOfMaterial.Consumption);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Cost", DISBillOfMaterial.Cost);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field1", DISBillOfMaterial.Field1);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field2", DISBillOfMaterial.Field2);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field3", DISBillOfMaterial.Field3);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field4", DISBillOfMaterial.Field4);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field5", DISBillOfMaterial.Field5);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field6", DISBillOfMaterial.Field6);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field7", DISBillOfMaterial.Field7);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field8", DISBillOfMaterial.Field8);
                DT1.Rows.Add("Production.DISBillOfMaterial", "set", "Field9", DISBillOfMaterial.Field9);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteDISBillOfMaterial(DISBillOfMaterial DISBillOfMaterial)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISBillOfMaterial", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISBillOfMaterial", "cond", "LineNumber", DISBillOfMaterial.LineNumber);
                Gears.DeleteData(DT1, Conn);
                DataTable count = Gears.RetriveData2("select * from Production.DISBillOfMaterial where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DataTable count2 = Gears.RetriveData2("select * from Production.DISSizeBreakdown where DocNumber = '" + Docnum + "'", Conn);
                    if (count2.Rows.Count < 1)
                    {
                        DataTable count3 = Gears.RetriveData2("select * from Production.DISStep where DocNumber = '" + Docnum + "'", Conn);
                        if (count3.Rows.Count < 1)
                        {
                            DT2.Rows.Add("Production.DIS", "cond", "DocNumber", Docnum);
                            DT2.Rows.Add("Production.DIS", "set", "IsWithDetail", "False");
                            Gears.UpdateData(DT2, Conn);
                        }
                    }
                }
            }
        }
        public class DISStep
        {
            public virtual DIS Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Step { get; set; }
            public virtual string WorkCenter { get; set; }
            public virtual decimal LaborCost { get; set; }
            public virtual DateTime TargetDateIN { get; set; }
            public virtual DateTime TargetDateOUT { get; set; }
            public virtual string SpecialInst { get; set; }
            public virtual DateTime DateIN { get; set; }
            public virtual DateTime DateOUT { get; set; }
            public virtual int Days { get; set; }
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

            public DataTable getDetail(string DocNumber, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Production.DISStep WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber ASC", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddDISStep(DISStep DISStep)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.DISStep WHERE DocNumber = '" + Docnum + "'", Conn);

                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }

                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.DISStep", "0", "DocNumber ", Docnum);
                DT1.Rows.Add("Production.DISStep", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.DISStep", "0", "Step", DISStep.Step);
                DT1.Rows.Add("Production.DISStep", "0", "WorkCenter", DISStep.WorkCenter);
                DT1.Rows.Add("Production.DISStep", "0", "LaborCost", DISStep.LaborCost);
                DT1.Rows.Add("Production.DISStep", "0", "TargetDateIN", DISStep.TargetDateIN);
                DT1.Rows.Add("Production.DISStep", "0", "TargetDateOUT", DISStep.TargetDateOUT);
                DT1.Rows.Add("Production.DISStep", "0", "SpecialInst", DISStep.SpecialInst);
                DT1.Rows.Add("Production.DISStep", "0", "Remarks", DISStep.Remarks);
                DT1.Rows.Add("Production.DISStep", "0", "Field1", DISStep.Field1);
                DT1.Rows.Add("Production.DISStep", "0", "Field2", DISStep.Field2);
                DT1.Rows.Add("Production.DISStep", "0", "Field3", DISStep.Field3);
                DT1.Rows.Add("Production.DISStep", "0", "Field4", DISStep.Field4);
                DT1.Rows.Add("Production.DISStep", "0", "Field5", DISStep.Field5);
                DT1.Rows.Add("Production.DISStep", "0", "Field6", DISStep.Field6);
                DT1.Rows.Add("Production.DISStep", "0", "Field7", DISStep.Field7);
                DT1.Rows.Add("Production.DISStep", "0", "Field8", DISStep.Field8);
                DT1.Rows.Add("Production.DISStep", "0", "Field9", DISStep.Field9);
                DT2.Rows.Add("Production.DIS", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Production.DIS", "set", "IsWithDetail", "True");
                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateDISStep(DISStep DISStep)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.DISStep", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISStep", "cond", "LineNumber", DISStep.LineNumber);
                DT1.Rows.Add("Production.DISStep", "set", "Step", DISStep.Step);
                DT1.Rows.Add("Production.DISStep", "set", "WorkCenter", DISStep.WorkCenter);
                DT1.Rows.Add("Production.DISStep", "set", "LaborCost", DISStep.LaborCost);
                DT1.Rows.Add("Production.DISStep", "set", "TargetDateIN", DISStep.TargetDateIN);
                DT1.Rows.Add("Production.DISStep", "set", "TargetDateOUT", DISStep.TargetDateOUT);
                DT1.Rows.Add("Production.DISStep", "set", "SpecialInst", DISStep.SpecialInst);
                DT1.Rows.Add("Production.DISStep", "set", "Remarks", DISStep.Remarks);
                DT1.Rows.Add("Production.DISStep", "set", "Field1", DISStep.Field1);
                DT1.Rows.Add("Production.DISStep", "set", "Field2", DISStep.Field2);
                DT1.Rows.Add("Production.DISStep", "set", "Field3", DISStep.Field3);
                DT1.Rows.Add("Production.DISStep", "set", "Field4", DISStep.Field4);
                DT1.Rows.Add("Production.DISStep", "set", "Field5", DISStep.Field5);
                DT1.Rows.Add("Production.DISStep", "set", "Field6", DISStep.Field6);
                DT1.Rows.Add("Production.DISStep", "set", "Field7", DISStep.Field7);
                DT1.Rows.Add("Production.DISStep", "set", "Field8", DISStep.Field8);
                DT1.Rows.Add("Production.DISStep", "set", "Field9", DISStep.Field9);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteDISStep(DISStep DISStep)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.DISStep", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.DISStep", "cond", "LineNumber", DISStep.LineNumber);
                Gears.DeleteData(DT1, Conn);
                DataTable count = Gears.RetriveData2("select * from Production.DISStep where DocNumber = '" + Docnum + "'", Conn);
                if (count.Rows.Count < 1)
                {
                    DataTable count2 = Gears.RetriveData2("select * from Production.DISSizeBreakdown where DocNumber = '" + Docnum + "'", Conn);
                    if (count2.Rows.Count < 1)
                    {
                        DataTable count3 = Gears.RetriveData2("select * from Production.DISBillOfMaterial where DocNumber = '" + Docnum + "'", Conn);
                        if (count3.Rows.Count < 1)
                        {
                            DT2.Rows.Add("Production.DIS", "cond", "DocNumber", Docnum);
                            DT2.Rows.Add("Production.DIS", "set", "IsWithDetail", "False");
                            Gears.UpdateData(DT2, Conn);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
