using ComponentFactory.Krypton.Toolkit;
using DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace cofdeluxProjectApp.formList
{
    public partial class frmCari : KryptonForm
    {

        private frmHome home;
        connect bgl = new connect();
        public frmCari(frmHome method)
        {
            InitializeComponent();
            home = method;
            
        }

        
        
            public async Task RunAsync()
            {
                string city = "Bursa"; // Şehir adı sabit veya lokasyon API'sinden alınabilir
                string weather = await GetWeatherAsync(city);
                string time = GetLocalTime("Turkey Standard Time"); // Türkiye için zaman dilimi
                //string dolar = await GetDollarRateAsync();
                txtSehir.Text = $"Şehir: {city}";
                txtSicak.Text = weather;
                ZamanTr.Text = time;
                //dolarKac.Text = dolar;
            }

            private async Task<string> GetWeatherAsync(string city)
            {
                string apiKey = "//"; // OpenWeatherMap API Key'inizi buraya yazın
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(apiUrl);
                    JObject json = JObject.Parse(response);
                    string weatherDescription = json["weather"][0]["description"].ToString();
                    string temperature = json["main"]["temp"].ToString();
                    return $"Hava durumu: {weatherDescription}, Sıcaklık: {temperature}°C";
                }
            }

            private string GetLocalTime(string timeZoneId)
            {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime localTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZone);
                return localTime.ToString("dd MMM yyy");
            }

            private async Task<string> GetDollarRateAsync()
            {
                //string apiKey = "//"; // Döviz kuru API Key'inizi buraya yazın
                string apiUrl = $"https://api.exchangerate-api.com/v4/latest/USD";

                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(apiUrl);
                    JObject json = JObject.Parse(response);
                    string usdToTry = json["rates"]["TRY"].ToString();
                    return usdToTry;
                }
            }
        









        string veriAl (string al)
        {
            SqlCommand bugun = new SqlCommand($"select SUM(Product.productPrice) from Orders inner join Product on Product.productID = Orders.productID where {al}", bgl.baglanti("on"));
            return bugun.ExecuteScalar().ToString();
        }
        decimal oranAl()
        {
            decimal oranAy = 0;

            SqlCommand bugun = new SqlCommand(@"
SELECT 
    (SELECT SUM(Product.productPrice) 
     FROM Orders 
     INNER JOIN Product ON Product.productID = Orders.productID 
     WHERE dateKapan = CAST(GETDATE() AS DATE)) AS Bugun, 
    (SELECT SUM(Product.productPrice) 
     FROM Orders 
     INNER JOIN Product ON Product.productID = Orders.productID 
     WHERE dateKapan = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE)) AS OncekiGun",
                bgl.baglanti("on"));

            SqlDataReader oku = bugun.ExecuteReader();

            try
            {
                if (oku.Read()) // Veri okuma işlemi başarılıysa
                {
                    decimal bugunSatis = oku["Bugun"] != DBNull.Value ? Convert.ToDecimal(oku["Bugun"]) : 0;
                    decimal oncekiGunSatis = oku["OncekiGun"] != DBNull.Value ? Convert.ToDecimal(oku["OncekiGun"]) : 0;

                    if (oncekiGunSatis > 0)
                    {
                        oranAy = ((bugunSatis - oncekiGunSatis) / oncekiGunSatis) * 100; // Artış/Azalış oranı
                    }
                    else if (bugunSatis > 0)
                    {
                        oranAy = 100; // Dünkü satış yoksa ve bugünkü satış varsa %100 artış varsayılır
                    }
                    else
                    {
                        oranAy = 0; // İkisi de 0 ise oran 0
                    }
                }
            }
            finally
            {
                // Kaynakları temizle
                oku.Close();
                bgl.baglanti("off");
            }

            return oranAy;
        }


        decimal aylikOranAl()
        {
            decimal aylikOran = 0;

            SqlCommand aylikSorgu = new SqlCommand(@"
SELECT 
    (SELECT SUM(Product.productPrice) 
     FROM Orders 
     INNER JOIN Product ON Product.productID = Orders.productID 
     WHERE YEAR(dateKapan) = YEAR(GETDATE()) AND MONTH(dateKapan) = MONTH(GETDATE())) AS BuAy, 
    (SELECT SUM(Product.productPrice) 
     FROM Orders 
     INNER JOIN Product ON Product.productID = Orders.productID 
     WHERE YEAR(dateKapan) = YEAR(DATEADD(MONTH, -1, GETDATE())) AND MONTH(dateKapan) = MONTH(DATEADD(MONTH, -1, GETDATE()))) AS OncekiAy",
                bgl.baglanti("on"));

            SqlDataReader oku = aylikSorgu.ExecuteReader();

            try
            {
                if (oku.Read()) // Veri okuma işlemi başarılıysa
                {
                    decimal buAySatis = oku["BuAy"] != DBNull.Value ? Convert.ToDecimal(oku["BuAy"]) : 0;
                    decimal oncekiAySatis = oku["OncekiAy"] != DBNull.Value ? Convert.ToDecimal(oku["OncekiAy"]) : 0;

                    if (oncekiAySatis > 0)
                    {
                        aylikOran = ((buAySatis - oncekiAySatis) / oncekiAySatis) * 100; // Artış/Azalış oranı
                    }
                    else if (buAySatis > 0)
                    {
                        aylikOran = 100; // Geçen ay satış yoksa ve bu ay satış varsa %100 artış varsayılır
                    }
                    else
                    {
                        aylikOran = 0; // İkisi de 0 ise oran 0
                    }
                }
            }
            finally
            {
                // Kaynakları temizle
                oku.Close();
                bgl.baglanti("off");
            }

            return aylikOran;
        }


        List<int> GetirProductID()
        {
                List<int> list = new List<int>();
                string query = "Select distinct productID From Orders where status  = 1";
                SqlCommand command = new SqlCommand(query, bgl.baglanti("on"));
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader.GetInt32(0)); // productID'yi listeye ekle
                }
                return list;
            
        }


        string dinamikSorgu(List<int> productID)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("Select ");

            foreach(int proID in productID)
            {
                SqlCommand com = new SqlCommand($"select productName from Orders inner join Product on Product.productID = Orders.productID WHERE Product.productID ={proID}", bgl.baglanti("on"));
                string yemekName = com.ExecuteScalar().ToString();
                bgl.baglanti("off");
                queryBuilder.AppendLine($"SUM(CASE WHEN O.productID = {proID} THEN P.productPrice ELSE 0 END) AS [{yemekName}],");
               

            }
            queryBuilder.Remove(queryBuilder.Length - 3, 2);
            queryBuilder.AppendLine("FROM Orders O");
            queryBuilder.AppendLine("INNER JOIN Product P ON O.productID = P.productID");
            queryBuilder.AppendLine("WHERE O.Status = 1");
            return queryBuilder.ToString();

        }
        


        void dinamikSorguCalistir(string query)
        {
            
                SqlCommand command = new SqlCommand(query, bgl.baglanti("on"));
                SqlDataReader reader = command.ExecuteReader();

                // Sonuçları yazdır
                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        chart1.Series["Yemek1"].Points.AddXY($"{reader.GetName(i)}: ",reader[i]);
                    }
                }
            
        }



        private void frmCari_Load_1(object sender, EventArgs e)
        {

            List<int> proID = GetirProductID();
            string query = dinamikSorgu(proID);
            dinamikSorguCalistir(query);



            bugunPara.Text = "₺" + veriAl("dateKapan = CAST(GETDATE() AS DATE)");
            aylikPara.Text = "₺" + veriAl("YEAR(dateKapan) = YEAR(GETDATE()) AND MONTH(dateKapan) = MONTH(GETDATE())");
            yillikPara.Text = "₺" + veriAl("YEAR(dateKapan) = YEAR(GETDATE())");
            nakitToplam.Text = "₺" + veriAl("Orders.pay = 'Nakit'");
            krediToplam.Text = "₺" + veriAl("Orders.pay = 'Kredi'");
            TumToplam.Text = "₺" + veriAl("Status = 1");
            BugunOran.Text = oranAl() + "% düne göre satış oranı";
            ayOran.Text = aylikOranAl() + "% geçen aya göre satış oranı";
            // await RunAsync();
        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            home.geriDon();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            

        }

        private void satisGor_Click(object sender, EventArgs e)
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Dinamik olarak "PDF_Reports" klasörünü aç
            string folderPath = Path.Combine(userFolder, "Documents", "PDF_Reports");

            // Klasörü aç
            Process.Start("explorer.exe", folderPath);

        }
    }
}
