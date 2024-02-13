using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PalletMaster
    {
        //03-10-2016 KMM    add connection

        private static string Pallet;
        public static string PalletString;
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string PalletID { get; set; }
        public virtual string AreaCode { get; set; }
        public virtual string Packaging { get; set; }
        public virtual decimal CaseTier { get; set; }
        public virtual decimal TierPallet { get; set; }
        public virtual decimal Width { get; set; }
        public virtual decimal Length { get; set; }
        public virtual decimal Height { get; set; }
        public virtual decimal UnitWeight { get; set; }
        public virtual string PalletType { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MidName { get; set; }
        public virtual string NickName { get; set; }
        public virtual decimal Age { get; set; }
        public virtual string BirthDay { get; set; }
        //public virtual string ActivatedBy { get; set; }
        //public virtual string ActivatedDate { get; set; }
        //public virtual string DeActivatedBy { get; set; }
        //public virtual string DeActivatedDate { get; set; }

        public virtual IList<PalletMaster> Detail { get; set; }

        public class PalletDetail
        {
            public virtual PalletMaster Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PalletID { get; set; }
            public virtual string PlateNumber { get; set; }
            public virtual string Item { get; set; }
            public virtual string Customer { get; set; }
            public virtual string UOM { get; set; }
            public virtual string Batch { get; set; }
            public virtual string lot { get; set; }
            public virtual string checker { get; set; }
            public virtual decimal weight { get; set; }
            public virtual decimal Quantity { get; set; }
            public virtual DateTime ExpiryDate { get; set; }

            public DataTable getdetail(string PalletID, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select [PalletID],[LineNumber],[PlateNumber],[Batch],[lot], " +
                        "[ExpiryDate],[UOM],[Customer],[Item],[Quantity],[weight],[checker]," +
                        "[Field1],[Field2],[Field3],[Field4],[Field5],[Field6],[Field7],[Field8],[Field9]" +    
                        "from WMS.PalletDetail where PalletID ='" + Pallet + "'"
                        , Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPalletDetails(PalletDetail PalletDetail)
            {
                int linenum = 0;
                int plt = 0;
                string pltLine;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from WMS.PalletDetail where PalletID = '" + Pallet + "'", Conn);
                DataTable lastVal = Gears.RetriveData2("select PlateNumber from WMS.PalletDetail where PalletID = '" + Pallet + "' order by LineNumber desc ", Conn);
                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                    plt = Convert.ToInt32(lastVal.Rows[0][0].ToString().Split('-')[1]) + 1;
                }
                catch
                {

                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                if (plt == 0)
                {
                    pltLine = plt.ToString().PadLeft(5, '0');
                }
                else
                {
                    pltLine = plt.ToString().TrimStart('0').PadLeft(5, '0');
                }

                string generated = "PLT-" + pltLine.ToString();

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("WMS.PalletDetail", "0", "PalletID", Pallet);
                DT1.Rows.Add("WMS.PalletDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("WMS.PalletDetail", "0", "PlateNumber", generated);
                DT1.Rows.Add("WMS.PalletDetail", "0", "Batch", PalletDetail.Batch);
                DT1.Rows.Add("WMS.PalletDetail", "0", "Customer", PalletDetail.Customer);
                DT1.Rows.Add("WMS.PalletDetail", "0", "Item", PalletDetail.Item);
                DT1.Rows.Add("WMS.PalletDetail", "0", "UOM", PalletDetail.UOM);
                DT1.Rows.Add("WMS.PalletDetail", "0", "lot", PalletDetail.lot);
                DT1.Rows.Add("WMS.PalletDetail", "0", "ExpiryDate", PalletDetail.ExpiryDate);
                DT1.Rows.Add("WMS.PalletDetail", "0", "checker", PalletDetail.checker);
                DT1.Rows.Add("WMS.PalletDetail", "0", "weight", PalletDetail.weight);
                DT1.Rows.Add("WMS.PalletDetail", "0", "Quantity", PalletDetail.Quantity);



                Gears.CreateData(DT1, Conn);
            }

            public void UpdatePalletDetails(PalletDetail PalletDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.PalletDetail", "cond", "PalletID", Pallet);
                DT1.Rows.Add("WMS.PalletDetail", "cond", "LineNumber", PalletDetail.LineNumber);
                DT1.Rows.Add("WMS.PalletDetail", "cond", "PlateNumber", PalletDetail.PlateNumber);
                DT1.Rows.Add("WMS.PalletDetail", "set", "Batch", PalletDetail.Batch);
                DT1.Rows.Add("WMS.PalletDetail", "set", "Customer", PalletDetail.Customer);
                DT1.Rows.Add("WMS.PalletDetail", "set", "Item", PalletDetail.Item);
                DT1.Rows.Add("WMS.PalletDetail", "set", "UOM", PalletDetail.UOM);
                DT1.Rows.Add("WMS.PalletDetail", "set", "lot", PalletDetail.lot);
                DT1.Rows.Add("WMS.PalletDetail", "set", "ExpiryDate", PalletDetail.ExpiryDate);
                DT1.Rows.Add("WMS.PalletDetail", "set", "checker", PalletDetail.checker);
                DT1.Rows.Add("WMS.PalletDetail", "set", "weight", PalletDetail.weight);
                DT1.Rows.Add("WMS.PalletDetail", "set", "Quantity", PalletDetail.Quantity);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePalletDetails(PalletDetail PalletDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("WMS.PalletDetail", "cond", "PalletID", PalletDetail.PalletID);
                DT1.Rows.Add("WMS.PalletDetail", "cond", "LineNumber", PalletDetail.LineNumber);

                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from WMS.PalletDetail where PalletID = '" + Pallet + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("WMS.PalletDetail", "cond", "PalletDetail", PalletID);
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        

        public DataTable getdata(string Pal, string Conn)//KMM add Conn
        {
            DataTable a;

            if (Pal != null)
            {   //2020-06-21 RA START Handling of Multiple Warehouse,Plant and Room in one Pallet
                Pallet = Pal;
                a = Gears.RetriveData2("select * from Masterfile.Pallet where PalletID = '" + Pal + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {

                    PalletID = dtRow["PalletID"].ToString();
                    AreaCode = dtRow["AreaCode"].ToString();
                    Packaging = dtRow["Packaging"].ToString();
                    CaseTier = String.IsNullOrEmpty(dtRow["CaseTier"].ToString()) ? 0 : Convert.ToDecimal(dtRow["CaseTier"].ToString());
                    TierPallet = String.IsNullOrEmpty(dtRow["TierPallet"].ToString()) ? 0 : Convert.ToDecimal(dtRow["TierPallet"].ToString());
                    Width = String.IsNullOrEmpty(dtRow["Width"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Width"].ToString());
                    Length = String.IsNullOrEmpty(dtRow["Length"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Length"].ToString());
                    Height = String.IsNullOrEmpty(dtRow["Height"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Height"].ToString());
                    UnitWeight = String.IsNullOrEmpty(dtRow["UnitWeight"].ToString()) ? 0 : Convert.ToDecimal(dtRow["UnitWeight"].ToString());
                    PalletType = dtRow["PalletType"].ToString();
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    FirstName = dtRow["FirstName"].ToString();
                    LastName = dtRow["LastName"].ToString();
                    MidName = dtRow["MidName"].ToString();
                    NickName = dtRow["NickName"].ToString();
                    Age = String.IsNullOrEmpty(dtRow["Age"].ToString()) ? 0 : Convert.ToDecimal(dtRow["Age"].ToString());
                    BirthDay = dtRow["BirthDay"].ToString();


                    //ActivatedBy = dtRow["ActivatedBy"].ToString();
                    //ActivatedDate = dtRow["ActivatedDate"].ToString();
                    //DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    //DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }

            }

            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
            return a;
        }
        public void InsertData(PalletMaster _ent)
        {
            Pallet = _ent.PalletID;
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.Pallet", "0", "PalletID", _ent.PalletID);
            DT1.Rows.Add("Masterfile.Pallet", "0", "AreaCode", _ent.AreaCode);
            DT1.Rows.Add("Masterfile.Pallet", "0", "Packaging", _ent.Packaging);
            DT1.Rows.Add("Masterfile.Pallet", "0", "CaseTier", _ent.CaseTier);
            DT1.Rows.Add("Masterfile.Pallet", "0", "TierPallet", _ent.TierPallet);
            DT1.Rows.Add("Masterfile.Pallet", "0", "Width", _ent.Width);
            DT1.Rows.Add("Masterfile.Pallet", "0", "Length", _ent.Length);
            DT1.Rows.Add("Masterfile.Pallet", "0", "Height", _ent.Height);
            DT1.Rows.Add("Masterfile.Pallet", "0", "UnitWeight", _ent.UnitWeight);
            DT1.Rows.Add("Masterfile.Pallet", "0", "PalletType", _ent.PalletType);
            DT1.Rows.Add("Masterfile.Pallet", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Pallet", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Pallet", "0", "FirstName", _ent.FirstName);
            DT1.Rows.Add("Masterfile.Pallet", "0", "LastName", _ent.LastName);
            DT1.Rows.Add("Masterfile.Pallet", "0", "MidName", _ent.MidName);
            DT1.Rows.Add("Masterfile.Pallet", "0", "NickName", _ent.NickName);
            DT1.Rows.Add("Masterfile.Pallet", "0", "Age", _ent.Age);
            DT1.Rows.Add("Masterfile.Pallet", "0", "BirthDay", _ent.BirthDay);


            Gears.CreateData(DT1, _ent.Connection);//KMM add Conn

            Functions.AuditTrail("REFPAL", _ent.PalletID, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);//KMM add Conn
        }

        public void UpdateData(PalletMaster _ent)
        {
            Pallet = _ent.PalletID; 
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            PalletString = _ent.PalletID;
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.Pallet", "cond", "PalletID", _ent.PalletID);
            DT1.Rows.Add("Masterfile.Pallet", "set", "AreaCode", _ent.AreaCode);
            DT1.Rows.Add("Masterfile.Pallet", "set", "Packaging", _ent.Packaging);
            DT1.Rows.Add("Masterfile.Pallet", "set", "CaseTier", _ent.CaseTier);
            DT1.Rows.Add("Masterfile.Pallet", "set", "TierPallet", _ent.TierPallet);
            DT1.Rows.Add("Masterfile.Pallet", "set", "Width", _ent.Width);
            DT1.Rows.Add("Masterfile.Pallet", "set", "Length", _ent.Length);
            DT1.Rows.Add("Masterfile.Pallet", "set", "Height", _ent.Height);
            DT1.Rows.Add("Masterfile.Pallet", "set", "UnitWeight", _ent.UnitWeight);
            DT1.Rows.Add("Masterfile.Pallet", "set", "PalletType", _ent.PalletType);
            DT1.Rows.Add("Masterfile.Pallet", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Pallet", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Pallet", "set", "FirstName", _ent.FirstName);
            DT1.Rows.Add("Masterfile.Pallet", "set", "LastName", _ent.LastName);
            DT1.Rows.Add("Masterfile.Pallet", "set", "MidName", _ent.MidName);
            DT1.Rows.Add("Masterfile.Pallet", "set", "NickName", _ent.NickName);
            DT1.Rows.Add("Masterfile.Pallet", "set", "BirthDay", _ent.BirthDay);
            DT1.Rows.Add("Masterfile.Pallet", "set", "Age", _ent.Age);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFPAL", PalletID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn
        }

        public void DeleteData(PalletMaster _ent)
        {
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Pallet", "cond", "PalletID", _ent.PalletID);
            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFPAL", PalletID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn

        }
    }
}
