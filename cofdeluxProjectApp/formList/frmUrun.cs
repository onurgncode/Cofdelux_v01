
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using DAL;
using System.Text.RegularExpressions;
namespace cofdeluxProjectApp.formList
{
    public partial class frmUrun : KryptonForm
    {
        user kullanici = new user();
        connect bgl = new connect();
        //KryptonButton btn;
        //List<KryptonButton> buttons = new List<KryptonButton>();
        //public void masaOlustur(int x)
        //{

        //    int lokasyonx = 440;
        //    int lokasyony = 170;




        //    for (int y = 1; y <= x; y++)
        //    {
        //        btn = new KryptonButton();
        //        btn.Location = new Point(lokasyonx, lokasyony);
        //        btn.Size = new Size(150, 140);
        //        btn.Text = "Masa" + y;
        //        btn.Name = "btnDinamik" + y;
        //        btn.StateCommon.BackStyle = masa1.StateCommon.BackStyle;
        //        btn.StateCommon.ContentStyle = masa1.StateCommon.ContentStyle;
        //        btn.StateCommon.BorderStyle = masa1.StateCommon.BorderStyle;
        //        btn.Click += (s, e) =>
        //        {

        //        };
        //        this.Controls.Add(btn);
        //        btn.BringToFront();
        //        lokasyonx += 200;
        //        if (y % 3 == 0)
        //        {
        //            lokasyony += 145;
        //            lokasyonx = 440;
        //        }
        //        buttons.Add(btn);
        //    }//for 


        //}

        frmHome urun;
        public frmUrun(frmHome urun)
        {
            InitializeComponent();
            
            this.urun = urun;
            
        }
        public frmUrun()
        {
            InitializeComponent();
            
        }
        frmVeriTkb takib;
        frmSepet sepet;
        public frmUrun(frmVeriTkb frm)
        {
            this.takib = frm;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            

        }

        private void labelHome_Click(object sender, EventArgs e)
        {

        }

        private async void frmUrun_Load(object sender, EventArgs e)
        {
            await LoadDataAndCheckButton();


        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonSplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //public void masaSil(int sil)
        //{
        //    try
        //    {
        //        string buttonName = "btnDinamik" + sil;
        //        if (sil == -1)
        //        {
        //            for (int i = 0; i < buttons.Count; i++)
        //            {
        //                this.Controls.Remove(buttons[i]);
        //                buttons.Clear();
        //            }
        //        }
        //        else
        //        {
        //            this.Controls.Remove(buttons[sil - 1]);
        //        }
        //    }
        //    catch(Exception e)
        //    {

        //        MessageBox.Show(e.Message, "Bilgi");
        //    }
        //}


        
        private async Task LoadDataAndCheckButton()
        {
            try
            {
               
                await Task.Delay(100);
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;"))
                {
                    await conn.OpenAsync(); // Asenkron bağlantı açılıyor

                    for (int i = 1; i <= 12; i++) // Masa 1'den 12'ye kadar kontrol ediyoruz
                    {
                        SqlCommand com = new SqlCommand($"SELECT tableID FROM Orders WHERE tableID = {i} AND Status = 0", conn);
                        var result = await com.ExecuteScalarAsync(); // Asenkron sorgu çalıştır

                        if (result != null) // Eğer masa aktifse
                        {
                            string buttonName = "masa" + i; // Örneğin: masa1, masa2, masa3...
                            Control[] foundControls = this.Controls.Find(buttonName, true);

                            if (foundControls.Length > 0 && foundControls[0] is KryptonButton)
                            {
                                KryptonButton foundButton = (KryptonButton)foundControls[0];
                                foundButton.StateCommon.Back.Color1 = Color.Red; 
                                    foundButton.StateCommon.Back.Color2 = Color.Red;
                                foundButton.StateCommon.Content.ShortText.Color1 = Color.White;
                            }
                            
                        }
                        else
                        {
                            string buttonName = "masa" + i; // Örneğin: masa1, masa2, masa3...
                            Control[] foundControls = this.Controls.Find(buttonName, true);
                            if (foundControls.Length > 0 && foundControls[0] is KryptonButton)
                            {
                                KryptonButton foundButton = (KryptonButton)foundControls[0];
                                foundButton.StateCommon.Back.Color1 = Color.White;
                                foundButton.StateCommon.Back.Color2 = Color.White;
                                foundButton.StateCommon.Content.ShortText.Color1 = Color.FromArgb(8, 142, 254);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
    
        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            urun.geriDon();
        }
        
        void adisyonGoster(int masa)// Datadan masa numarasını çekerken hata veriyor 
        {
            
            SqlDataAdapter komut = new SqlDataAdapter($"select Product.productName as [Sipariş Adı],Product.productPrice as [Sipariş Fiyatı] from Orders inner join Product on Orders.productID = Product.productID Where Orders.tableID ={masa} and Orders.Status =0", bgl.baglanti("on"));
            DataTable table = new DataTable();
            komut.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Width = 186;
            dataGridView1.Columns[1].Width = 186;
            
        }
        static int butonID;
        void butonTikla()
        {
            if (this.ActiveControl is KryptonButton button)
            {
                string buttonName = button.Name; // Butonun ismi
                string numberPartString = Regex.Match(buttonName, @"\d+").Value;
                int numberPart = int.Parse(numberPartString);
                adisyonGoster(numberPart);
                butonID = numberPart;
            }
        }
        private void kryptonButton1_MouseClick(object sender, MouseEventArgs e)
        {
            butonTikla();
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa4_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa5_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa6_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa7_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa8_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa9_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa10_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa11_Click(object sender, EventArgs e)
        {
            butonTikla();
        }

        private void masa12_Click(object sender, EventArgs e)
        {
            butonTikla();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void masa1_DoubleClick(object sender, EventArgs e)
        {

        }
        
        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {

            int userID = int.Parse(kullanici.userDogrula(this.urun.gonder(), "id"));
            takib = new frmVeriTkb(butonID,userID);
            
            takib.Show();
            takib.FormClosed += async (s, args) => await LoadDataAndCheckButton();


        }

        private void SiparisTamamla_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {

            int userID = int.Parse(kullanici.userDogrula(this.urun.gonder(), "id"));
            sepet = new frmSepet(butonID, userID, this.urun);
            sepet.Show();

            sepet.FormClosed += async (s, args) => await LoadDataAndCheckButton();
            sepet.FormClosed += (s, args) => dataGridView1.DataSource = null;
        }
    }
}
