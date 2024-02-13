using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PreProductionOut

    {
        private static string Docnum;
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string Step { get; set; }
        public virtual string Remarks { get; set; }
    
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual IList<PreProductionOutDetail> Detail { get; set; }


        public class PreProductionOutDetail
        {

            public virtual PreProductionOut Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string JobOrder { get; set; }

            public virtual string ProductOrder { get; set; }

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
                    a = Gears.RetriveData2("select * from Production.PreProductionOutDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddPreProductionOutDetail(PreProductionOutDetail PreProductionOutDetail)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.PreProductionOutDetail where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "JobOrder", PreProductionOutDetail.JobOrder);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "ProductOrder", PreProductionOutDetail.ProductOrder);
    
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field1", PreProductionOutDetail.Field1);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field2", PreProductionOutDetail.Field2);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field3", PreProductionOutDetail.Field3);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field4", PreProductionOutDetail.Field4);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field5", PreProductionOutDetail.Field5);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field6", PreProductionOutDetail.Field6);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field7", PreProductionOutDetail.Field7);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field8", PreProductionOutDetail.Field8);
                DT1.Rows.Add("Production.PreProductionOutDetail", "0", "Field9", PreProductionOutDetail.Field9);



                //DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdatePreProductionOutDetail(PreProductionOutDetail PreProductionOutDetail)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.PreProductionOutDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.PreProductionOutDetail", "cond", "LineNumber", PreProductionOutDetail.LineNumber);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "JobOrder", PreProductionOutDetail.JobOrder);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "ProductOrder", PreProductionOutDetail.ProductOrder);
    

                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field1", PreProductionOutDetail.Field1);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field2", PreProductionOutDetail.Field2);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field3", PreProductionOutDetail.Field3);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field4", PreProductionOutDetail.Field4);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field5", PreProductionOutDetail.Field5);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field6", PreProductionOutDetail.Field6);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field7", PreProductionOutDetail.Field7);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field8", PreProductionOutDetail.Field8);
                DT1.Rows.Add("Production.PreProductionOutDetail", "set", "Field9", PreProductionOutDetail.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeletePreProductionOutDetail(PreProductionOutDetail PreProductionOutDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.PreProductionOutDetail", "cond", "DocNumber", PreProductionOutDetail.DocNumber);
                DT1.Rows.Add("Production.PreProductionOutDetail", "cond", "LineNumber", PreProductionOutDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);


                //DataTable count = Gears.RetriveData2("select * from Production.PreProductionOutDetail where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }





        }
        public class RefTransaction
        {
            public virtual PreProductionOut Parent { get; set; }
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
                    a = Gears.RetriveData2("select RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDPPO' OR  A.TransType='PRDPPO') ", Conn);
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


            a = Gears.RetriveData2("select * from Production.PreProductionOut where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                Type = dtRow["Type"].ToString();
                DocDate = dtRow["DocDate"].ToString();

                Step = dtRow["Step"].ToString();
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
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
            }

            return a;
        }
        public void InsertData(PreProductionOut _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.PreProductionOut", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.PreProductionOut", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Type", _ent.Type);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Step", _ent.Step);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.PreProductionOut", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.PreProductionOut", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.PreProductionOut", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(PreProductionOut _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;        //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.PreProductionOut", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.PreProductionOut", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Type", _ent.Type);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Step", _ent.Step);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Remarks", _ent.Remarks); 
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.PreProductionOut", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.PreProductionOut", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.PreProductionOut", "set", "LastEditedDate",_ent.LastEditedDate);


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDPPO", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(PreProductionOut _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.PreProductionOut", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDPPO", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
