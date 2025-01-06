using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL.VeriKontrol
{
     public class ConKontrol
     {
        connect baglan;
        public string kontrolEt()
        {
            baglan = new connect();
            try
            {
                baglan.baglanti("On");
                return "Online";
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.Message,"hata" );
                return "offline";
            }
            
        }
        
            

     }
}

