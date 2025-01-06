using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
namespace cofdeluxProjectApp.formList
{
    public partial class frmVeriTkb : KryptonForm
    {
        static user kullanici = new user();
        connect bgl = new connect();
        // iki kere tıklanınca 2 ci datagrid viewe veri çekme
        private bool isDoubleClicked = false;
        string menuGetir(int isim)
        {
                SqlCommand komut = new SqlCommand($"select categoryName from Category WHERE categoryID = {isim}", bgl.baglanti("on"));
                return komut.ExecuteScalar().ToString();
            
            

        }
        bool ekleme = false;
        void yemekleriCek()
        {
            if (isDoubleClicked == true)
            {
                if (this.ActiveControl is KryptonButton button)
                {
                    string buttonName = button.Name;
                    string numberPartString = Regex.Match(buttonName, @"\d+").Value;
                    int numberPart = int.Parse(numberPartString);
                    SqlDataAdapter komut = new SqlDataAdapter($"select image, productName, ProductPrice from Product Where catID  ={numberPart}", bgl.baglanti("on"));
                    DataTable table = new DataTable();
                    komut.Fill(table);
                    dataGridView2.DataSource = table;
                    dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        dataGridView2.Columns[0].Width = 100;
                        dataGridView2.Columns[1].Width = 150;
                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        row.Height = 90;
                    }
                    if(ekleme == false)
                    {
                        EkleCikarButonlari();
                        ekleme = true;
                    }
                    

                }
            }
        }
        // masanın ID sini alır
        static int masaID = 0;

        static int AlbilgiUser;
        public frmVeriTkb(int masa,int userID)
        {
            masaID = masa;
            AlbilgiUser = userID;
            InitializeComponent();
            masa1.Text = menuGetir(1);
            masa2.Text = menuGetir(2);
            masa3.Text = menuGetir(3);
            masa4.Text = menuGetir(4);
           

        }

        
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonSplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        








        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            /*
            masaListele();
            dataGridView2.DataSource = null;
            isDoubleClicked = false;
            masaID = 0;
            */
            this.Close();


        }
        void EkleCikarButonlari()
        {
            // "Ekle" Butonu Ekleniyor
            if (!dataGridView2.Columns.Contains("Ekle"))
            {
                DataGridViewButtonColumn ekleButtonColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Ekle",
                    Name = "EkleButton",
                    Text = "Ekle",
                    UseColumnTextForButtonValue = true,
                    Width = 25
                };
                dataGridView2.Columns.Add(ekleButtonColumn);
            }

            // "Çıkar" Butonu Ekleniyor
            if (!dataGridView2.Columns.Contains("Çıkar"))
            {
                DataGridViewButtonColumn cikarButtonColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Çıkar",
                    Name = "CikarButton",
                    Text = "Çıkar",
                    UseColumnTextForButtonValue = true,
                    Width = 25
                };
                dataGridView2.Columns.Add(cikarButtonColumn);
            }

            // Butonların Tıklama Olayını Tanımlıyoruz
            dataGridView2.CellClick += DataGridView2_CellClick;
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

           
            if (e.RowIndex < 0) return;

            // "Ekle" Butonuna Tıklanırsa
            if (dataGridView2.Columns[e.ColumnIndex].Name == "EkleButton")
            {
                string productName = dataGridView2.Rows[e.RowIndex].Cells["productName"].Value.ToString();

                // Veritabanına Ekleme İşlemi
                SqlCommand komut2 = new SqlCommand($"select productID from Product where productName = @p1", bgl.baglanti("on"));
                komut2.Parameters.AddWithValue("@p1", productName);
                int pID = int.Parse(komut2.ExecuteScalar().ToString());

                string times = DateTime.Now.ToString("yyyy-MM-dd");
                SqlCommand komut = new SqlCommand($"insert into Orders (productID, userID, tableID, Status, date) values (@p1, @p2, @p3, {0}, @p4)", bgl.baglanti("on"));
                komut.Parameters.AddWithValue("@p1", pID);
                komut.Parameters.AddWithValue("@p2", AlbilgiUser);
                komut.Parameters.AddWithValue("@p3", masaID);
                komut.Parameters.AddWithValue("@p4", times);
                komut.ExecuteNonQuery();
                siparisListele();
                MessageBox.Show("Ürün başarıyla eklendi!");
            }

            // "Çıkar" Butonuna Tıklanırsa
            if (dataGridView2.Columns[e.ColumnIndex].Name == "CikarButton")
            {
                string productName = dataGridView2.Rows[e.RowIndex].Cells["productName"].Value.ToString();

                // Veritabanından Silme İşlemi
                SqlCommand komut2 = new SqlCommand($"select productID from Product where productName = @p1", bgl.baglanti("on"));
                komut2.Parameters.AddWithValue("@p1", productName);
                int pID = int.Parse(komut2.ExecuteScalar().ToString());

                SqlCommand komut = new SqlCommand($"delete from Orders where orderID = (SELECT TOP(1) orderID FROM Orders WHERE productID = @p1 AND tableID = @p2 AND Status = 0 ORDER BY orderID ASC)", bgl.baglanti("on"));

                komut.Parameters.AddWithValue("@p1", pID);
                komut.Parameters.AddWithValue("@p2", masaID);
                komut.ExecuteNonQuery();
                siparisListele();
                MessageBox.Show("Ürün başarıyla çıkarıldı!");
            }
            }
            catch (Exception exa)
            {
                MessageBox.Show(exa.Message,"bilgi");
            }
        }






        string adisyonSil()
        {


            if (dataGridView1.SelectedRows.Count > 0)
            {
                // İlk seçili satırı al
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Satırdaki tüm hücrelerin değerlerini al
                string values = "";
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    values += cell.Value + " "; // Hücre değerlerini birleştir
                }

                return values; 
            }
            else
            {
                return "hata";
            }





            
        }




        string adisyonEkle()
        {
            

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                // CheckBox sütununun değerini kontrol et
                bool isChecked = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);

                if (isChecked)
                {
                    // productName sütunundaki değeri al
                    string productName = row.Cells["productName"].Value?.ToString();

                    if (!string.IsNullOrEmpty(productName)) // Eğer değer null değilse listeye ekle
                    {
                        return productName;
                    }
                }
            }
            return "hata";


        }


        private void masa1_Click(object sender, EventArgs e)
        {
            yemekleriCek();
        }

        void siparisListele()
        {
            SqlDataAdapter komut = new SqlDataAdapter($"select Product.productName AS Adı,Product.productPrice AS Fiyatı,COUNT(Orders.orderID) AS Sayısı from Orders inner join Product on Orders.productID = Product.productID Where Orders.tableID ={masaID}  and Orders.Status =0 group by Product.productName, Product.productPrice;", bgl.baglanti("on"));
            DataTable table = new DataTable();
            komut.Fill(table);
            dataGridView1.DataSource = table;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dataGridView1.Columns[0].Width = 70;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                row.Height = 65;
            }

            isDoubleClicked = true;
        }
        void yemekVer()
        {

            siparisListele();
        }
        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            if (isDoubleClicked == true)
            {
                SqlCommand komut2 = new SqlCommand($"select productID from Product where productName = @p1",bgl.baglanti("on"));
                komut2.Parameters.AddWithValue("@p1", adisyonEkle());
                int pID = int.Parse(komut2.ExecuteScalar().ToString());
                // ekleme yapılacak

                // int userID = int.Parse( kullanici.userDogrula(this.veri.gonder(), "id"));
                string times = DateTime.Now.ToString("yyyy-MM-dd");
                SqlCommand komut = new SqlCommand($"insert into Orders (productID,userID,tableID,Status,date) values (@p1,@p2,@p3,{0},@p4)",bgl.baglanti("on"));

                // dışardan gelirse albilgiuser içerden gelirse userID
                komut.Parameters.AddWithValue("@p1", pID);
                komut.Parameters.AddWithValue("@p2", AlbilgiUser);
                komut.Parameters.AddWithValue("@p3", masaID); 
                komut.Parameters.AddWithValue("@p4", times);
                komut.ExecuteNonQuery();
                yemekVer();
                
                
            }

        }

        private void masa2_Click(object sender, EventArgs e)
        {
            yemekleriCek();
            
        }

        private void masa3_Click(object sender, EventArgs e)
        {
            yemekleriCek();
            
        }

        private void masa4_Click(object sender, EventArgs e)
        {
            yemekleriCek();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (isDoubleClicked == true)
            {
                if (dataGridView1.SelectedRows.Count > 0) // Seçili satır olup olmadığını kontrol ediyoruz.
                {
                    // İlk seçili satırı alıyoruz.
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // İlk sütundaki değeri alıyoruz.
                    var firstColumnValue = selectedRow.Cells[0].Value;

                    // Değeri bir mesaj kutusunda gösterebilirsiniz.
                    MessageBox.Show(firstColumnValue?.ToString() + "Silindi" ?? "Değer yok");
                    // delete yapısı yapılacak
                    SqlCommand komut2 = new SqlCommand($"Delete from Orders where tableID = {masaID} and orderID = {firstColumnValue}", bgl.baglanti("on"));
                    komut2.Parameters.AddWithValue("@p1", adisyonSil());
                    komut2.ExecuteNonQuery();
                    siparisListele();
                }
                else
                {
                    MessageBox.Show("Lütfen bir satır seçin!");
                }
                

            }
        }

        private void frmVeriTkb_Load_1(object sender, EventArgs e)
        {
            siparisListele();
        }
    }
}
