using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BankAccount
    {

        private static string ID;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string BankAccountCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string BankCode { get; set; }
        public virtual string Branch { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string Type { get; set; }
        public virtual string Signatory1 { get; set; }
        public virtual string Signatory2 { get; set; }
        public virtual string Signatory3 { get; set; }
        public virtual string Signatory4 { get; set; }
        public virtual string DateOpen { get; set; }
        public virtual string GLCode { get; set; }
        public virtual decimal LastBalance { get; set; }
        public virtual string LastReconDate { get; set; }
        public virtual bool ShowCashPosition { get; set; }
        public virtual decimal CPRBegBal { get; set; }
        public virtual int DateX { get; set; }
        public virtual int DateY { get; set; }
        public virtual int PayeeX { get; set; }
        public virtual int PayeeY { get; set; }
        public virtual int AmountNX { get; set; }
        public virtual int AmountNY { get; set; }
        public virtual int AmountWX { get; set; }
        public virtual int AmountWY { get; set; }
        public virtual int RemarksX { get; set; }
        public virtual int RemarksY { get; set; }
        public virtual int CheckWidth { get; set; }
        public virtual int CheckHeight { get; set; }
        public virtual bool IsForPayeeVisible { get; set; }
        public virtual decimal MaintainingBalance { get; set; }
        public virtual decimal MinCheckNumber { get; set; }
        public virtual decimal MaxCheckNumber { get; set; }
        public virtual string NextCheckNumber { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public DataTable getdata(string BankAccount, string Conn)
        {
            DataTable a;


                a = Gears.RetriveData2("select * from Masterfile.BankAccount where BankAccountCode = '" + BankAccount +"'", Conn);

                foreach (DataRow dtRow in a.Rows)
                {

                BankAccountCode=dtRow["BankAccountCode"].ToString();
                Description= dtRow["Description"].ToString();
                BankCode= dtRow["BankCode"].ToString();
                Branch= dtRow["Branch"].ToString();
                AccountNumber=dtRow["AccountNumber"].ToString();
                AccountName=dtRow["AccountName"].ToString();
                Type=dtRow["Type"].ToString();
                Signatory1=dtRow["Signatory1"].ToString();
                Signatory2= dtRow["Signatory2"].ToString();
                Signatory3= dtRow["Signatory3"].ToString();
                Signatory4=dtRow["Signatory4"].ToString();
                DateOpen=dtRow["DateOpen"].ToString();
                GLCode=dtRow["GLCode"].ToString();
                LastBalance = Convert.ToDecimal(Convert.IsDBNull(dtRow["LastBalance"]) ? 0 : dtRow["LastBalance"]);
                LastReconDate=dtRow["LastReconDate"].ToString();
                ShowCashPosition = Convert.ToBoolean(Convert.IsDBNull(dtRow["ShowCashPosition"]) ? false : dtRow["ShowCashPosition"]);
                CPRBegBal = Convert.ToDecimal(Convert.IsDBNull(dtRow["CPRBegBal"]) ? 0 : dtRow["CPRBegBal"]);
                DateX = Convert.ToInt32(Convert.IsDBNull(dtRow["DateX"]) ? 0 : dtRow["DateX"]);
                DateY = Convert.ToInt32(Convert.IsDBNull(dtRow["DateY"]) ? 0 : dtRow["DateY"]);
                PayeeX = Convert.ToInt32(Convert.IsDBNull(dtRow["PayeeX"]) ? 0 : dtRow["PayeeX"]);
                PayeeY = Convert.ToInt32(Convert.IsDBNull(dtRow["PayeeY"]) ? 0 : dtRow["PayeeY"]);
                AmountNX = Convert.ToInt32(Convert.IsDBNull(dtRow["AmountNX"]) ? 0 : dtRow["AmountNX"]);
                AmountNY = Convert.ToInt32(Convert.IsDBNull(dtRow["AmountNY"]) ? 0 : dtRow["AmountNY"]);
                AmountWX = Convert.ToInt32(Convert.IsDBNull(dtRow["AmountWX"]) ? 0 : dtRow["AmountWX"]);
                AmountWY = Convert.ToInt32(Convert.IsDBNull(dtRow["AmountWY"]) ? 0 : dtRow["AmountWY"]);
                RemarksX = Convert.ToInt32(Convert.IsDBNull(dtRow["RemarksX"]) ? 0 : dtRow["RemarksX"]);
                RemarksY = Convert.ToInt32(Convert.IsDBNull(dtRow["RemarksY"]) ? 0 : dtRow["RemarksY"]);
                CheckWidth = Convert.ToInt32(Convert.IsDBNull(dtRow["CheckWidth"]) ? 0 : dtRow["CheckWidth"]);
                CheckHeight = Convert.ToInt32(Convert.IsDBNull(dtRow["CheckHeight"]) ? 0 : dtRow["CheckHeight"]);
                IsForPayeeVisible = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsForPayeeVisible"]) ? false : dtRow["IsForPayeeVisible"]);
                MaintainingBalance = Convert.ToDecimal(Convert.IsDBNull(dtRow["MaintainingBalance"]) ? 0 : dtRow["MaintainingBalance"]);
                MinCheckNumber = Convert.ToDecimal(Convert.IsDBNull(dtRow["MinCheckNumber"]) ? 0 : dtRow["MinCheckNumber"]);
                MaxCheckNumber = Convert.ToDecimal(Convert.IsDBNull(dtRow["MaxCheckNumber"]) ? 0 : dtRow["MaxCheckNumber"]);
                NextCheckNumber = dtRow["NextCheckNumber"].ToString();

                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                DeactivatedDate = dtRow["DeactivatedDate"].ToString();

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
        public void InsertData(BankAccount _ent)
        {
            ID = _ent.BankAccountCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.BankAccount", "0", "BankAccountCode", _ent.BankAccountCode);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "Description", _ent.Description);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "BankCode", _ent.BankCode);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "Branch", _ent.Branch);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "AccountNumber", _ent.AccountNumber);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "AccountName", _ent.AccountName);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "Type", _ent.Type);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "Signatory1", _ent.Signatory1);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "Signatory2", _ent.Signatory2);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "Signatory3", _ent.Signatory3);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "Signatory4", _ent.Signatory4);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "DateOpen", _ent.DateOpen);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "GLCode", _ent.GLCode);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "LastBalance", _ent.LastBalance);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "LastReconDate", _ent.LastReconDate);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "ShowCashPosition", _ent.ShowCashPosition);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "CPRBegBal", _ent.CPRBegBal);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "DateX", _ent.DateX);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "DateY", _ent.DateY);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "PayeeX", _ent.PayeeX);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "PayeeY", _ent.PayeeY);
                DT1.Rows.Add("Masterfile.BankAccount", "0", " AmountNX", _ent.AmountNX);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "AmountNY", _ent.AmountNY);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "AmountWX", _ent.AmountWX);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "AmountWY", _ent.AmountWY);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "RemarksX", _ent.RemarksX);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "RemarksY", _ent.RemarksY);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "CheckWidth", _ent.CheckWidth);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "CheckHeight", _ent.CheckHeight);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "IsForPayeeVisible", _ent.IsForPayeeVisible);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "MaintainingBalance", _ent.MaintainingBalance);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "MinCheckNumber", _ent.MinCheckNumber);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "MaxCheckNumber", _ent.MaxCheckNumber);
                DT1.Rows.Add("Masterfile.BankAccount", "0", "NextCheckNumber", _ent.NextCheckNumber);

                DT1.Rows.Add("Masterfile.BankAccount", "0", "IsInactive", _ent.IsInactive);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field1", _ent.Field1);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field2", _ent.Field2);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field3", _ent.Field3);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field4", _ent.Field4);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field5", _ent.Field5);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field6", _ent.Field6);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field7", _ent.Field7);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field8", _ent.Field8);
                DT1.Rows.Add("Masterfile.BankAccount","0","Field9", _ent.Field9);

                DT1.Rows.Add("Masterfile.BankAccount","0","AddedBy", _ent.AddedBy);
                DT1.Rows.Add("Masterfile.BankAccount","0","AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
          


            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(BankAccount _ent)
        {
            ID = _ent.BankAccountCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.BankAccount", "cond", "BankAccountCode", ID);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "BankCode", _ent.BankCode);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "Branch", _ent.Branch);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "AccountNumber", _ent.AccountNumber);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "AccountName", _ent.AccountName);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "Signatory1", _ent.Signatory1);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "Signatory2", _ent.Signatory2);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "Signatory3", _ent.Signatory3);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "Signatory4", _ent.Signatory4);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "DateOpen", _ent.DateOpen);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "GLCode", _ent.GLCode);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "LastBalance", _ent.LastBalance);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "LastReconDate", _ent.LastReconDate);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "ShowCashPosition", _ent.ShowCashPosition);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "CPRBegBal", _ent.CPRBegBal);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "DateX", _ent.DateX);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "DateY", _ent.DateY);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "PayeeX", _ent.PayeeX);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "PayeeY", _ent.PayeeY);
            DT1.Rows.Add("Masterfile.BankAccount", "set", " AmountNX", _ent.AmountNX);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "AmountNY", _ent.AmountNY);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "AmountWX", _ent.AmountWX);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "AmountWY", _ent.AmountWY);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "RemarksX", _ent.RemarksX);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "RemarksY", _ent.RemarksY);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "CheckWidth", _ent.CheckWidth);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "CheckHeight", _ent.CheckHeight);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "IsForPayeeVisible", _ent.IsForPayeeVisible);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "MaintainingBalance", _ent.MaintainingBalance);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "MinCheckNumber", _ent.MinCheckNumber);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "MaxCheckNumber", _ent.MaxCheckNumber);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "NextCheckNumber", _ent.NextCheckNumber);

            DT1.Rows.Add("Masterfile.BankAccount", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.BankAccount","set","Field9", _ent.Field9);

            DT1.Rows.Add("Masterfile.BankAccount", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.BankAccount", "set", "LastEditedDate", _ent.LastEditedDate);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFBAC", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"UPDATE", _ent.Connection);

        }
        public void DeleteData(BankAccount _ent)
        {
            ID = _ent.BankAccountCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.BankAccount", "cond", "BankAccountCode", ID);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFBAC", ID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"DELETE", _ent.Connection);
        }
    }
}
