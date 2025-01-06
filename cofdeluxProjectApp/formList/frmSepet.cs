using ComponentFactory.Krypton.Toolkit;
using DAL;
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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
namespace cofdeluxProjectApp.formList
{
    public partial class frmSepet : KryptonForm
    {
        connect bgl = new connect();
        private frmHome home;
        public frmSepet(frmHome metod)
        {
           
            InitializeComponent();
            // Diğer Sayfadan Referans Almak
            home = metod;
        }
        int masaAl;
        int userAl;
        public frmSepet(int masa, int userID,frmHome home)
        {
            this.home = home;
            masaAl = masa;
            userAl = userID;
            InitializeComponent();
            AdisyonListele(masa);
            toplamTutar();
        }


        void AdisyonListele(int masaID)
        {
            SqlDataAdapter komut = new SqlDataAdapter($"select Product.productName AS Sipariş_Adı,Product.productPrice AS Sipariş_Fiyatı,COUNT(Orders.orderID) AS Sipariş_Sayısı from Orders inner join Product on Orders.productID = Product.productID Where Orders.tableID ={masaID}  and Orders.Status =0 group by Product.productName, Product.productPrice;", bgl.baglanti("on"));
            DataTable table = new DataTable();
            komut.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void toplamTutar()
        {
            SqlCommand com = new SqlCommand($"select SUM(Product.productPrice) AS Toplam_Tutar from Orders inner join Product on Orders.productID = Product.productID where Orders.tableID = {masaAl} and Orders.Status = 0", bgl.baglanti("on"));
            var obje = com.ExecuteScalar();
            if (obje != null && obje != DBNull.Value)
            {
                txtToplam.Text = obje.ToString() + " ₺";
            }
            else
            {
                txtToplam.Text = "0";
            }
            
        }
        



        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            home.geriDon();
        }

        private void frmSepet_Load(object sender, EventArgs e)
        {
            txtMasa.Text = "Masa " + masaAl;
            this.ActiveControl = Uye_gizlilabel;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            home.geriDon();
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            home.geriDon();
        }


        // pdf oluştur
        string filePath;
        bool onay;
        public void ExportToPdf(DataGridView dataGridView1,string odeme)
        {
            if (onay != true)
            {


                try
                {
                    // Tarih bazlı klasör yapısını oluştur
                    string baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PDF_Reports");
                    string yearFolder = Path.Combine(baseFolder, DateTime.Now.Year.ToString());
                    string monthFolder = Path.Combine(yearFolder, DateTime.Now.ToString("MMMM")); // Ay ismini kullanarak klasör adı oluştur

                    // Klasörleri oluştur
                    if (!Directory.Exists(yearFolder))
                        Directory.CreateDirectory(yearFolder);
                    if (!Directory.Exists(monthFolder))
                        Directory.CreateDirectory(monthFolder);

                    // PDF dosya yolu
                    filePath = Path.Combine(monthFolder, "Fatura_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");

                    // PDF Belgesi Oluştur
                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();

                    // Tabloyu Oluştur
                    PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                    pdfTable.WidthPercentage = 100;

                    // Sütun Başlıklarını Ekle
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                        headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfTable.AddCell(headerCell);
                    }

                    // Satırları Ekle
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow) // Yeni satır kontrolü
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string cellText = cell.Value?.ToString() ?? string.Empty;
                                PdfPCell pdfCell = new PdfPCell(new Phrase(cellText));
                                pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                pdfTable.AddCell(pdfCell);
                            }
                        }
                    }

                    // PDF'e Tabloyu ve Ek Bilgileri Ekle
                    doc.Add(new Paragraph("Masa Detayları"));
                    doc.Add(new Paragraph("*****************************"));
                    doc.Add(pdfTable); // Tabloyu ekle
                    doc.Add(new Paragraph("Toplam Tutar: " + txtToplam.Text));
                    doc.Add(new Paragraph("Ödeme Yöntemi: " + odeme));

                    // PDF'i Kapat
                    doc.Close();

                    // Veritabanı Güncellemesi
                    onay = true;
                    string times = DateTime.Now.ToString("yyyy-MM-dd");
                    string query = "UPDATE Orders SET Status = 1, dateKapan = @times, pay= @odeme WHERE tableID = @tableID AND Status = 0";
                    using (SqlCommand com = new SqlCommand(query, bgl.baglanti("on")))
                    {
                        com.Parameters.AddWithValue("@times", times);
                        com.Parameters.AddWithValue("@tableID", masaAl);
                        com.Parameters.AddWithValue("@odeme", odeme);
                        com.ExecuteNonQuery();
                    }

                    // Kullanıcıya Bilgi Ver
                    MessageBox.Show($"Fiş '{filePath}' konumuna kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    onay = false;
                }
            }//if
            else {
                MessageBox.Show("Ödemeyi Daha önce tamamladınız!", "hata", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }







        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (onay == true)
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show($"Hata: Önce Ödemeyi tamamlayınız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            
            ExportToPdf(dataGridView1,"Kredi");
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            
            ExportToPdf(dataGridView1, "Nakit");
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            this.Close();
            
            
        }
    }
}
