using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PropertyTransfer
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string PropertyNumber { get; set; }
        public virtual string TransferType { get; set; }


        public virtual string ItemCode { get; set; }
        public virtual string FullDesc { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual string ClassCode { get; set; }
        public virtual string SizeCode { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual string NewLocation { get; set; }
        public virtual string Location { get; set; }
        public virtual string NewAccountablePerson { get; set; }
        public virtual string AccountablePerson { get; set; }
        public virtual string NewDepartment { get; set; }
        public virtual string Department { get; set; }
        public virtual string NewWarehouseCode { get; set; }
        public virtual string WarehouseCode { get; set; }

        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }


        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }



        public virtual IList<PropertyTransferDetail> Detail { get; set; }
        public class PropertyTransferDetail
        {
            public virtual PropertyTransfer Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PropertyNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }
            public virtual string Location { get; set; }
            public virtual string Department { get; set; }
            public virtual string AccountablePerson { get; set; }
            public virtual string WarehouseCode { get; set; }
            public virtual string NewLocation { get; set; }
            public virtual string NewDepartment { get; set; }
            public virtual string NewAccountablePerson { get; set; }
            public virtual string NewWarehouseCode { get; set; }
            public virtual string AccumulatedCostCenter { get; set; }
            public virtual string DepreciationCostCenter { get; set; }



            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.PropertyTransferDetail WHERE DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPropertyTransferDetail(PropertyTransferDetail PropertyTransferDetail)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.PropertyTransferDetail where DocNumber = '" + Docnum + "'", Conn);

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
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "PropertyNumber", PropertyTransferDetail.PropertyNumber);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "ItemCode", PropertyTransferDetail.ItemCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "ItemDescription", PropertyTransferDetail.ItemDescription);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "ColorCode", PropertyTransferDetail.ColorCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "ClassCode", PropertyTransferDetail.ClassCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "SizeCode", PropertyTransferDetail.SizeCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Qty", PropertyTransferDetail.Qty);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Location", PropertyTransferDetail.Location);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Department", PropertyTransferDetail.Department);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "AccountablePerson", PropertyTransferDetail.AccountablePerson);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "WarehouseCode", PropertyTransferDetail.WarehouseCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "NewLocation", PropertyTransferDetail.NewLocation);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "NewDepartment", PropertyTransferDetail.NewDepartment);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "NewAccountablePerson", PropertyTransferDetail.NewAccountablePerson);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "NewWarehouseCode", PropertyTransferDetail.NewWarehouseCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "AccumulatedCostCenter", PropertyTransferDetail.AccumulatedCostCenter);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "DepreciationCostCenter", PropertyTransferDetail.DepreciationCostCenter);


                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field1", PropertyTransferDetail.Field1);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field2", PropertyTransferDetail.Field2);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field3", PropertyTransferDetail.Field3);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field4", PropertyTransferDetail.Field4);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field5", PropertyTransferDetail.Field5);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field6", PropertyTransferDetail.Field6);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field7", PropertyTransferDetail.Field7);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field8", PropertyTransferDetail.Field8);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "0", "Field9", PropertyTransferDetail.Field9);


                DT2.Rows.Add("Accounting.PropertyTransfer", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.PropertyTransfer", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePropertyTransferDetail(PropertyTransferDetail PropertyTransferDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "cond", "LineNumber", PropertyTransferDetail.LineNumber);


                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "PropertyNumber", PropertyTransferDetail.PropertyNumber);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "ItemCode", PropertyTransferDetail.ItemCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "ItemDescription", PropertyTransferDetail.ItemDescription);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "ColorCode", PropertyTransferDetail.ColorCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "ClassCode", PropertyTransferDetail.ClassCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "SizeCode", PropertyTransferDetail.SizeCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Qty", PropertyTransferDetail.Qty);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Location", PropertyTransferDetail.Location);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Department", PropertyTransferDetail.Department);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "AccountablePerson", PropertyTransferDetail.AccountablePerson);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "WarehouseCode", PropertyTransferDetail.WarehouseCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "NewLocation", PropertyTransferDetail.NewLocation);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "NewDepartment", PropertyTransferDetail.NewDepartment);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "NewAccountablePerson", PropertyTransferDetail.NewAccountablePerson);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "NewWarehouseCode", PropertyTransferDetail.NewWarehouseCode);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "AccumulatedCostCenter", PropertyTransferDetail.AccumulatedCostCenter);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "DepreciationCostCenter", PropertyTransferDetail.DepreciationCostCenter);


                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field1", PropertyTransferDetail.Field1);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field2", PropertyTransferDetail.Field2);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field3", PropertyTransferDetail.Field3);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field4", PropertyTransferDetail.Field4);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field5", PropertyTransferDetail.Field5);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field6", PropertyTransferDetail.Field6);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field7", PropertyTransferDetail.Field7);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field8", PropertyTransferDetail.Field8);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "set", "Field9", PropertyTransferDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePropertyTransferDetail(PropertyTransferDetail PropertyTransferDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.PropertyTransferDetail", "cond", "LineNumber", PropertyTransferDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Accounting.PropertyTransferDetail where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.PropertyTransfer", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.PropertyTransfer", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }



        public class RefTransaction
        {
            public virtual PropertyTransfer Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTPRT' OR  A.TransType='ACTPRT') ", Conn);
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

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.PropertyTransfer where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {  
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                TransferType = dtRow["TransferType"].ToString();


                //PropertyNumber = dtRow["PropertyNumber"].ToString();
                
                //ItemCode = dtRow["ItemCode"].ToString();
                //FullDesc = dtRow["FullDesc"].ToString();
                //ClassCode = dtRow["ClassCode"].ToString();
                //ColorCode = dtRow["ColorCode"].ToString();
                //SizeCode = dtRow["SizeCode"].ToString();
                //Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                //NewLocation = dtRow["NewLocation"].ToString();
                //Location = dtRow["Location"].ToString();
                //NewDepartment = dtRow["NewDepartment"].ToString();
                //Department = dtRow["Department"].ToString();
                //NewAccountablePerson = dtRow["NewAccountablePerson"].ToString();
                //AccountablePerson = dtRow["AccountablePerson"].ToString();
                //NewWarehouseCode = dtRow["NewWarehouseCode"].ToString();
                //WarehouseCode = dtRow["WarehouseCode"].ToString();

                
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();


                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);


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
        public void InsertData(PropertyTransfer _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "DocDate", _ent.DocDate);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "PropertyNumber", _ent.PropertyNumber);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "TransferType", _ent.TransferType);



            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "ItemCode", _ent.ItemCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "FullDesc", _ent.FullDesc);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "ColorCode", _ent.ColorCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "ClassCode", _ent.ClassCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "SizeCode", _ent.SizeCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Qty", _ent.Qty);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "NewLocation", _ent.NewLocation);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Location", _ent.Location);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Location", _ent.Location);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "NewDepartment", _ent.NewDepartment);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Department", _ent.Department);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "AccountablePerson", _ent.AccountablePerson);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "NewWarehouseCode", _ent.NewWarehouseCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "0", "WarehouseCode", _ent.WarehouseCode);


            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "IsValidated", _ent.IsValidated);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "IsWithDetail", _ent.IsWithDetail);

            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.PropertyTransfer", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(PropertyTransfer _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.PropertyTransfer", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "DocDate", _ent.DocDate);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "PropertyNumber", _ent.PropertyNumber);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "TransferType", _ent.TransferType);


            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "ItemCode", _ent.ItemCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "FullDesc", _ent.FullDesc);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "ColorCode", _ent.ColorCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "ClassCode", _ent.ClassCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "SizeCode", _ent.SizeCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Qty", _ent.Qty);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "NewLocation", _ent.NewLocation);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Location", _ent.Location);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "NewDepartment", _ent.NewDepartment);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Department", _ent.Department);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "NewAccountablePerson", _ent.NewAccountablePerson);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "AccountablePerson", _ent.AccountablePerson);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "NewWarehouseCode", _ent.NewWarehouseCode);
            //DT1.Rows.Add("Accounting.PropertyTransfer", "set", "WarehouseCode", _ent.WarehouseCode);


            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "IsValidated", _ent.IsValidated);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.PropertyTransfer", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTPRT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(PropertyTransfer _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.PropertyTransfer", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTPRT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
