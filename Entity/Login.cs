using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;
using System.Web;

namespace Entity
{
    public class LoginConnect
    {
        public virtual string Userid { get; set; }
        public virtual string Password { get; set; }
        public virtual string Domain { get; set; }
        public virtual string Compname { get; set; }

        public virtual string DBname { get; set; }
        public virtual string ConnectionString { get; set; }

        public void GetConnectionString(string Userid, string Domainname, string Password)
            {
                Gears.UseConnectionString("Data Source=MIT-INFRA10D;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=" + Userid + ";Password=" + Password + ";");
                DataTable gearsmetsit = Gears.RetriveData("select companyname from IT.Companies where DomainName = '"+Domainname+"'");

                foreach (DataRow dtRow in gearsmetsit.Rows)
                {
                    Domain = dtRow["DomainName"].ToString();
                    //Compname = dtRow["CompanyName"].ToString();
                }
                if (Userid != null && Domainname != null && Password != null)
                {
                    //Session["ConnString"] = Gears.VerifyUser(Userid, Password, Domain);

                    if (Domainname == "metsit.gears")
                        ConnectionString = "Data Source=MIT-INFRA10D;Initial Catalog=METS-TRITON;Persist Security Info=True;User ID=" + Userid + ";Password=" + Password + ";";
                        DBname = "METS-TRITON";
                        
                }
            }
        
    }
}
