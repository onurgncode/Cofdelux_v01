using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DAL
{
    public class connect
    {
        static void Main(string[] args)
        {
            // Kodun buraya
        }
        public SqlConnection baglanti(string OnOf)
        {
            
            SqlConnection baglan = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;");
            if (OnOf == "On" || OnOf == "ON" || OnOf == "on")
            {
                baglan.Open();
                return baglan;
            }
            else
            {
                baglan.Close();
                return baglan;
            }
        }
        
    }
}
