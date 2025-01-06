using ComponentFactory.Krypton.Toolkit;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Input;

namespace cofdeluxProjectApp.formList
{
    public partial class frmStok : KryptonForm
    {
        connect bgl = new connect();
        private frmHome home;
        public frmStok(frmHome method)
        {
            InitializeComponent();
            home = method;
        }
        

        private void frmStok_Load(object sender, EventArgs e)
        {
            SqlDataAdapter cmd = new SqlDataAdapter($"select categoryID,categoryName from Category", bgl.baglanti("on"));
            DataTable table = new DataTable();
            cmd.Fill(table);
            txtKat.DataSource = table;
            txtKat.DisplayMember = "categoryName"; // Gösterilecek sütun
            txtKat.ValueMember = "categoryId";     // Arka planda saklanacak sütun
            
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            home.geriDon();
        }

        
        byte[] imageData;
        private void kryptonTextBox1_Enter(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Bir resim seçin",
                Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName; // Resmin tam yolu
                string fileName = Path.GetFileName(filePath); // Dosya adı
                
                txtFoto.Text = fileName;

                // Resmi yükle
                Image originalImage = Image.FromFile(filePath);

                // Resmi 100x100 boyutlarına yeniden boyutlandır
                Image resizedImage = new Bitmap(originalImage, new Size(100, 100));
                using (MemoryStream ms = new MemoryStream())
                {
                    resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // JPEG formatında kaydet
                    imageData = ms.ToArray(); // Byte dizisine dönüştür

                    // Bu imageData'yı veritabanına kaydedebilirsiniz ya da başka bir işlemde kullanabilirsiniz
                }





            }
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        void sil()
        {
            txtAd.Text = "Yemek İsmi";
            txtFiyat.Value = 0;
            txtFoto.Text = "Fotoraf Seçiniz";
            txtKat.Text = "Seçiniz";
        }
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            // Girdi verilerini oku
            string yAd = txtAd.Text;
            string fiyat = txtFiyat.Value.ToString();
            int selectedId = (int)txtKat.SelectedValue;

            // Resim verisini binary olarak al
            

            // SQL sorgusu (parametreli)
            string query = "INSERT INTO Product (image, productName, productPrice, catID) VALUES (@Image, @ProductName, @ProductPrice, @CatID)";
                using (SqlCommand cmd = new SqlCommand(query, bgl.baglanti("on")))
                {
                    // Parametreleri ekle
                    cmd.Parameters.AddWithValue("@Image", imageData); // Binary veri
                    cmd.Parameters.AddWithValue("@ProductName", yAd); // Ürün adı
                    cmd.Parameters.AddWithValue("@ProductPrice", fiyat); // Fiyat
                    cmd.Parameters.AddWithValue("@CatID", selectedId); // Kategori ID

                    // Bağlantıyı aç ve sorguyu çalıştır
                    bgl.baglanti("on");
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} kayıt başarıyla eklendi.");
                sil();
                }
        }
        void yemekGoster()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string query = "select productID as ID, image as Resim, productName as İsim,productPrice as Fiyat, categoryName as Kategori from Product inner join Category on Product.catID = Category.categoryID";
            using (SqlDataAdapter cmd = new SqlDataAdapter(query, bgl.baglanti("on")))
            {

                DataTable table = new DataTable();
                cmd.Fill(table);
                dataGridView1.DataSource = table;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dataGridView1.Columns[0].Width = 60;
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    row.Height = 90;
                }

            }
        }
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            yemekGoster();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki ilk sütunun değerini al
                var value = dataGridView1.SelectedRows[0].Cells[0].Value;

                // Değerin tipi genellikle string ya da uygun bir türde olur
                // Örneğin, veriyi string olarak almak
                string firstColumnValue = value.ToString();

                // Bu değeri kullanabilirsiniz, örneğin ekrana yazdırmak
                string del = firstColumnValue;
                string query = $"Delete from Product where productID = {del}";
                using (SqlCommand cmd = new SqlCommand(query, bgl.baglanti("on")))
                {
                    cmd.ExecuteNonQuery();
                    yemekGoster();
                }
            }
            else
            {
                MessageBox.Show("Hiçbir satır seçilmedi.");
            }
           
        }

        private void txtFiyat_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
