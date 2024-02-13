using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ScrapRequest
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string ScrapRequestNumber { get; set; }
        public virtual string ScrapCodeRef { get; set; }
        public virtual string OCNRNumber { get; set; }
        public virtual string RequestingDeptCompany { get; set; }
        public virtual string TransDate { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string RequiredLoadingTime { get; set; }
        public virtual string ShipmentType { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from Production.ScrapRequest where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ScrapRequestNumber = dtRow["ScrapRequestNumber"].ToString();
                ScrapCodeRef = dtRow["ScrapCodeRef"].ToString();
                OCNRNumber = dtRow["OCNRNumber"].ToString();
                RequestingDeptCompany = dtRow["RequestingDeptCompany"].ToString();
                TransDate = dtRow["TransDate"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                RequiredLoadingTime = dtRow["RequiredLoadingTime"].ToString();
                ShipmentType = dtRow["ShipmentType"].ToString();
                
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
            }

            return a;
        }

        public class ScrapRequestDetail
        {

            public virtual ScrapRequest Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ScrapCode { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string ProcessSteps { get; set; }
            public virtual string OCNNumber { get; set; }
            public virtual string UOM { get; set; }
            public virtual string SHInstruction { get; set; }
            public virtual string Remarks { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual decimal TotalQty { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.ScrapRequestDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddScrapRequestDetail(ScrapRequestDetail ScrapRequestDetail)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.ScrapRequest where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "ScrapCode", ScrapRequestDetail.ScrapCode);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "ItemDescription", ScrapRequestDetail.ItemDescription);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "BatchNo", ScrapRequestDetail.BatchNo);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "ProcessSteps", ScrapRequestDetail.ProcessSteps);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "OCNNumber", ScrapRequestDetail.OCNNumber);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "Qty", ScrapRequestDetail.Qty);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "UOM", ScrapRequestDetail.UOM);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "SHInstruction", ScrapRequestDetail.SHInstruction);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "TotalQty", ScrapRequestDetail.TotalQty);
                DT1.Rows.Add("Production.ScrapRequestDetail", "0", "Remarks", ScrapRequestDetail.Remarks);
                


                //DT2.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.CuttingWorksheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }

            public void UpdateScrapRequestDetail(ScrapRequestDetail ScrapRequestDetail)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ScrapRequestDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.ScrapRequestDetail", "cond", "LineNumber", ScrapRequestDetail.LineNumber);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "ScrapCode", ScrapRequestDetail.ScrapCode);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "ItemDescription", ScrapRequestDetail.ItemDescription);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "BatchNo", ScrapRequestDetail.BatchNo);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "ProcessSteps", ScrapRequestDetail.ProcessSteps);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "OCNNumber", ScrapRequestDetail.OCNNumber);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "Qty", ScrapRequestDetail.Qty);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "UOM", ScrapRequestDetail.UOM);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "SHInstruction", ScrapRequestDetail.SHInstruction);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "TotalQty", ScrapRequestDetail.TotalQty);
                DT1.Rows.Add("Production.ScrapRequestDetail", "set", "Remarks", ScrapRequestDetail.Remarks);
                
                

                Gears.UpdateData(DT1, Conn);


            }

            public void DeleteScrapRequestDetail(ScrapRequestDetail ScrapRequestDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.ScrapRequestDetail", "cond", "DocNumber", ScrapRequestDetail.DocNumber);
                DT1.Rows.Add("Production.ScrapRequestDetail", "cond", "LineNumber", ScrapRequestDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);




            }
        }
        public class RefTransaction
        {
            public virtual ScrapRequest Parent { get; set; }
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
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDSCR' OR  A.TransType='PRDSCR') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public void InsertData(ScrapRequest _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();



            DT1.Rows.Add("Production.ScrapRequest", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.ScrapRequest", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ScrapRequest", "0", "ScrapRequestNumber", _ent.ScrapRequestNumber);
            DT1.Rows.Add("Production.ScrapRequest", "0", "ScrapCodeRef", _ent.ScrapCodeRef);
            DT1.Rows.Add("Production.ScrapRequest", "0", "OCNRNumber", _ent.OCNRNumber);
            DT1.Rows.Add("Production.ScrapRequest", "0", "RequestingDeptCompany", _ent.RequestingDeptCompany);
            DT1.Rows.Add("Production.ScrapRequest", "0", "TransDate", _ent.TransDate);
            DT1.Rows.Add("Production.ScrapRequest", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Production.ScrapRequest", "0", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Production.ScrapRequest", "0", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("Production.ScrapRequest", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.ScrapRequest", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ScrapRequest _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.ScrapRequest", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.ScrapRequest", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.ScrapRequest", "set", "ScrapRequestNumber", _ent.ScrapRequestNumber);
            DT1.Rows.Add("Production.ScrapRequest", "set", "ScrapCodeRef", _ent.ScrapCodeRef);
            DT1.Rows.Add("Production.ScrapRequest", "set", "OCNRNumber", _ent.OCNRNumber);
            DT1.Rows.Add("Production.ScrapRequest", "set", "RequestingDeptCompany", _ent.RequestingDeptCompany);
            DT1.Rows.Add("Production.ScrapRequest", "set", "TransDate", _ent.TransDate);
            DT1.Rows.Add("Production.ScrapRequest", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Production.ScrapRequest", "set", "RequiredLoadingTime", _ent.RequiredLoadingTime);
            DT1.Rows.Add("Production.ScrapRequest", "set", "ShipmentType", _ent.ShipmentType);
            DT1.Rows.Add("Production.ScrapRequest", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.ScrapRequest", "set", "LastEditedDate", _ent.LastEditedDate);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDSCR", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ScrapRequest _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.ScrapRequest", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDSCR", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }

    }
}
