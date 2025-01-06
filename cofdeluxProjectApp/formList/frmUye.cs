using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cofdeluxProjectApp.formList
{
    public partial class frmUye : Form
    {
        private frmHome home;
        connect bgl = new connect();
        public frmUye(frmHome method)
        {
            InitializeComponent();
            home = method;
        }
        void uyeListele()
        {
            SqlDataAdapter com = new SqlDataAdapter("select * from Users", bgl.baglanti("on"));
            DataTable table = new DataTable();
            com.Fill(table);
            dataGridView1.DataSource = table;
           dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void frmUye_Load(object sender, EventArgs e)
        {
            this.ActiveControl = Uye_gizlilabel;
            uyeListele();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void kryptonSplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            // şimdilik boş
        }

        private void kryptonPalette1_PalettePaint(object sender, ComponentFactory.Krypton.Toolkit.PaletteLayoutEventArgs e)
        {
            // şimdilik boş
        }

        private void kryptonListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void Uye_isim_Click(object sender, EventArgs e)
        {
            //
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            home.geriDon();
        }

        private void Uye_bilgi_Tc_Enter(object sender, EventArgs e)
        {
            txtTc.Text = ""; 
        }

        private void Uye_bilgi_Tc_Leave(object sender, EventArgs e)
        {

        }

        private void Uye_bilgi_İsim_Enter(object sender, EventArgs e)
        {
            txtisim.Text = "";
        }

        private void Uye_bilgi_İsim_Leave(object sender, EventArgs e)
        {

        }

        private void Uye_bilgi_Soyad_Enter(object sender, EventArgs e)
        {
            txtsoy.Text = "";
        }

        private void Uye_bilgi_Soyad_Leave(object sender, EventArgs e)
        {

        }

        private void Uye_bilgi_Şifre_Enter(object sender, EventArgs e)
        {
           txtsifre.Text = "";
        }

        private void Uye_bilgi_Şifre_Leave(object sender, EventArgs e)
        {

        }




        private void Uye_bilgi_Mail_Enter(object sender, EventArgs e)
        {
            txtMail.Text = "";
        }

        private void Uye_bilgi_Mail_Leave(object sender, EventArgs e)
        {

        }

        private void Uye_bilgi_Telefon_Enter(object sender, EventArgs e)
        {
            Uye_bilgi_Telefon.Text = "";
        }

        private void Uye_bilgi_Telefon_Leave(object sender, EventArgs e)
        {
            if(Uye_bilgi_Telefon.Text == "")
            {
                Uye_bilgi_Telefon.Text = "Telefon";
            }
        }

        private void Uye_bilgi_Adres_Enter(object sender, EventArgs e)
        {
            txtAdres.Text = "";
        }

        private void Uye_bilgi_Adres_Leave(object sender, EventArgs e)
        {

        }

        private void frmUye_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }

        private void Uye_Ekle_Click(object sender, EventArgs e)
        {
            try
            {


                string rol = txtRol.Text;
                string cinsiyet = txtCinsiyet.Text;
                switch (rol)
                {
                    case "Admin":
                        rol = "1";
                        break;
                    case "Garson":
                        rol = "2";
                        break;
                    case "Kasa":
                        rol = "3";
                        break;
                    default:
                        MessageBox.Show("Lütfen Bir rol seçiniz", "hata");
                        break;
                }
                if (cinsiyet == "Erkek")
                {
                    cinsiyet = "1";
                }
                else
                {
                    cinsiyet = "2";
                }
                // SQL sorgusu (parametreli)
                string query = "INSERT INTO Users (tcNo, firstName, lastName, password, role,mail,phoneNo,adress,gender) VALUES (@tc,@isim,@soyisim,@sifre,@rol,@mail,@telefon,@adres,@cinsiyet)";
                using (SqlCommand cmd = new SqlCommand(query, bgl.baglanti("on")))
                {
                    // Parametreleri ekle
                    cmd.Parameters.AddWithValue("@tc", txtTc.Text);
                    cmd.Parameters.AddWithValue("@isim", txtisim.Text);
                    cmd.Parameters.AddWithValue("@soyisim", txtsoy.Text);
                    cmd.Parameters.AddWithValue("@sifre", txtsifre.Text);
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                    cmd.Parameters.AddWithValue("@telefon", txtTel.Text);
                    cmd.Parameters.AddWithValue("@adres", txtAdres.Text);
                    cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);


                    // Bağlantıyı aç ve sorguyu çalıştır
                    bgl.baglanti("on");
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} kayıt başarıyla eklendi.");
                    uyeListele();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "ID")
            {
                string rol = txtRol.Text;
                string cinsiyet = txtCinsiyet.Text;
                switch (rol)
                {
                    case "Admin":
                        rol = "1";
                        break;
                    case "Garson":
                        rol = "2";
                        break;
                    case "Kasa":
                        rol = "3";
                        break;
                    default:
                        MessageBox.Show("Lütfen Bir rol seçiniz", "hata");
                        break;
                }
                if (cinsiyet == "Erkek")
                {
                    cinsiyet = "1";
                }
                else
                {
                    cinsiyet = "2";
                }
                DialogResult dinle = MessageBox.Show("Güncellemek İstediğinize Eminmisiniz", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dinle == DialogResult.Yes)
                {
                    SqlCommand com = new SqlCommand($"UPDATE Users SET password = '{txtsifre.Text}',role = {rol},mail = '{txtMail.Text}',phoneNo = '{txtTel.Text}',adress = '{txtAdres.Text}' WHERE userID = {txtID.Text}", bgl.baglanti("on"));
                    com.ExecuteNonQuery();
                }
                uyeListele();
            }
            else
            {
                MessageBox.Show("Lütfen Tablodan bir kullanıcıya iki kere tıklayınız", "Hata");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Başlık kısmını kontrol et
            {
                DataGridViewRow clickedRow = dataGridView1.Rows[e.RowIndex];

                // Hücrelerden veri al
                try
                {


                    string ID = clickedRow.Cells["userID"].Value?.ToString() ?? "";
                    string tcno = clickedRow.Cells["tcNo"].Value?.ToString() ?? "";
                    string isim = clickedRow.Cells["firstName"].Value?.ToString() ?? "";
                    string soyad = clickedRow.Cells["lastName"].Value?.ToString() ?? "";
                    string sifre = clickedRow.Cells["password"].Value?.ToString() ?? "";
                    string rol = clickedRow.Cells["role"].Value?.ToString() ?? "";
                    string mail = clickedRow.Cells["mail"].Value?.ToString() ?? "";
                    string telefon = clickedRow.Cells["phoneNo"].Value?.ToString() ?? "";
                    string adres = clickedRow.Cells["adress"].Value?.ToString() ?? "";
                    bool cinsiyet = (clickedRow.Cells["gender"].Value as bool?) ?? false;

                    txtID.Text = ID;
                    txtTc.Text = tcno;
                    txtisim.Text = isim;
                    txtsoy.Text = soyad;
                    txtsifre.Text = sifre;
                    switch (rol)
                    {
                        case "1":
                            txtRol.Text = "Admin";
                            break;
                        case "2":
                            txtRol.Text = "Garson";
                            break;
                        case "3":
                            txtRol.Text = "Kasa";
                            break;
                    }
                    txtMail.Text = mail;
                    txtTel.Text = telefon;
                    txtAdres.Text = adres;
                    if (cinsiyet == true)
                    {
                        txtCinsiyet.Text = "Erkek";
                    }
                    else
                    {
                        txtCinsiyet.Text = "Kadın";
                    }
                    txtisim.Enabled = false; txtsoy.Enabled = false;
                    txtTc.Enabled = false; txtCinsiyet.Enabled = false;
                    txtCinsiyet.Enabled = false;
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message, "bilgi");
                }
                
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            txtID.Text = "ID";
            txtisim.Text = "İsim";
            txtisim.Enabled = true;
            txtsoy.Text = "Soyisim";
            txtsoy.Enabled = true;
            txtTc.Text = "Tc Kimlik No";
            txtTc.Enabled = true;
            txtTel.Text = "Telefon No";
            txtsifre.Text = "Şifre";
            txtRol.Text = "Rol Seç";
            txtCinsiyet.Text = "Cinsiyet Seç";
            txtCinsiyet.Enabled = true;
            txtMail.Text = "Mail";
            txtAdres.Text = "Adres";
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {

            if (txtID.Text != "ID")
            {
                string rol = txtRol.Text;
                string cinsiyet = txtCinsiyet.Text;
                switch (rol)
                {
                    case "Admin":
                        rol = "1";
                        break;
                    case "Garson":
                        rol = "2";
                        break;
                    case "Kasa":
                        rol = "3";
                        break;
                    default:
                        MessageBox.Show("Lütfen Bir rol seçiniz", "hata");
                        break;
                }
                if (cinsiyet == "Erkek")
                {
                    cinsiyet = "1";
                }
                else
                {
                    cinsiyet = "2";
                }
                DialogResult dinle = MessageBox.Show("Silmek İstediğinize Eminmisiniz", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dinle == DialogResult.Yes)
                {
                    SqlCommand com = new SqlCommand($"Delete From Users where UserID = {txtID.Text}", bgl.baglanti("on"));
                    com.ExecuteNonQuery();
                }
                uyeListele();
            }
            else
            {
                MessageBox.Show("Lütfen Tablodan bir kullanıcıya iki kere tıklayınız", "Hata");
            }
        }
    }
}
