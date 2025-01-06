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

namespace cofdeluxProjectApp.formList
{
    public partial class frmProfil : KryptonForm
    {
        frmHome home;
        public frmProfil(frmHome home)
        {

            InitializeComponent();
            this.home = home;
        }
        public void bilgiAl(string tcKimlik = "",string ad="", string soyad = "", string sifre = "", int rol=0, string mail = "", string telefon = "", string adres = "", bool cinsiyet=true)
        {
            try
            {
                txtTc.Text = tcKimlik;
                txtName.Text = ad;
                txtRol.Text = rol.ToString();
                txtSoyad.Text = soyad;
                txtPass.Text = sifre;
                txtTel.Text = telefon;
                txtAdres.Text = adres;
                txtMail.Text = mail;
                txtRol.Text = cinsiyet.ToString();
                // Kod Çalışmıyor
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "bilgi");
            }
        }

        private void frmProfil_Load(object sender, EventArgs e)
        {
            
        }
    }
}
