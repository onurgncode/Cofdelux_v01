using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DAL.VeriKontrol;
namespace cofdeluxProjectApp.formList
{
    public partial class frmHome : KryptonForm
    {
        static user kullanici = new user();
        ConKontrol dekon = new ConKontrol();
        ayarlar frmAyar;

        public frmHome()
        {
            InitializeComponent();
            
        }
        public string gonder()
        {
            return alBilgi;
        }
        static string alBilgi;
        public frmHome(string bilgi)
        {
            InitializeComponent();
            alBilgi = bilgi;
            // üye girişi ayarı roller 1=Admin,2=Kasa,3=Garson
            string rolAl = kullanici.userDogrula(bilgi, "rol");
            switch (rolAl)
            {
                case "2":
                    pictureBox8.Visible = false;
                    label7.Visible = false;
                    pictureBox3.Visible = false;
                    label3.Visible = false;
                    pictureBox4.Visible = false;
                    label4.Visible = false;
                    break;
                case "3":
                    pictureBox8.Visible = false;
                    label7.Visible = false;
                    pictureBox3.Visible = false;
                    label3.Visible = false;
                    pictureBox4.Visible = false;
                    label4.Visible = false;
                    break;
            }




        }
        public void geriDon()
        {
            panelHome.Visible = true;
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            
        }
        static frmSepet sepet;
        static frmStok stok;
        static frmUrun urun;
        static frmCari cari;
        static frmDestek destek;
        
        static frmUye uye;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            sepet = new frmSepet(this);
            sepet.MdiParent = this;
            sepet.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            urun = new frmUrun(this);
            urun.MdiParent = this;
            urun.Show();
        }

        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            stok = new frmStok(this);
            stok.MdiParent = this;
            stok.Show();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            frmAyar = new ayarlar(this);
            frmAyar.Show();
            
            
            

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panelHome.Visible=false;
            cari = new frmCari(this);
            cari.MdiParent = this;
            cari.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            destek = new frmDestek(this);
            destek.MdiParent = this;
            destek.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            destek = new frmDestek(this);
            destek.MdiParent = this;
            destek.Show();
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox10_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            uye = new frmUye(this);
            uye.MdiParent = this;
            uye.Show();
        }
    }
    }


