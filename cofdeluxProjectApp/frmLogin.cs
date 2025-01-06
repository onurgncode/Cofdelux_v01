using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cofdeluxProjectApp.formList;
using ComponentFactory.Krypton.Toolkit;
using DAL;
namespace cofdeluxProjectApp
{
    public partial class frmLogin : KryptonForm
    {
        // Login sayfasını Açmak için kullanılır 
        private static frmLogin login;
        void kapla()
        {
            
            foreach (KryptonForm form in Application.OpenForms.Cast<KryptonForm>().ToList())
            {
                form.Close();
            }
        }
        public void ShowLoginForm()
        {
            if (login == null || login.IsDisposed)
            {
                login = this;
                login.FormClosed += (s, args) => kapla(); // Form kapandığında referansı temizle
                login.Show();
            }
            else
            {
                if (!login.Visible)
                {
                    login.Show(); // Gerekirse göster
                }
                login.BringToFront(); // Ön plana getir
            }
            
        }
        // Sign Sayfasını açmak için kullanılır
        static frmSignup sign;
        public void ShowSignupForm()
        {
            if (sign == null || sign.IsDisposed)
            {
                sign = new frmSignup(this); // Ana form referansını geçir
                sign.FormClosed += (s, args) => this.Close();
                sign.Show();
                this.Hide();
            }
            else
            {
                if (!sign.Visible)
                {
                    sign.Show(); // Gerekirse göster
                }
                sign.BringToFront(); // Ön plana getir
                this.Hide();
            }
        }
        public frmLogin()
        {
            InitializeComponent();
        }
        
        // Kayıt olma formunu açar
        
        private void btnSign_Click(object sender, EventArgs e)
        {
            ShowSignupForm();
        }
        static user Kullanici;
        static frmHome Home;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Kullanici = new user();
            
            if (txtPass.Text == Kullanici.userDogrula(txtEmail.Text,"sifre"))
            {
                
                Home = new frmHome(Kullanici.userDogrula(txtEmail.Text, "mail"));
                Home.Show();
                this.Hide();
                Home.FormClosed += (s, args) => kapla();
            }
            else
            {
                MessageBox.Show("Giriş Başarısız");
            }
            
        }

        

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            txtEmail.Text = "";
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "E mail Adresinizi giriniz";
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            txtPass.Text = "";
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Sifre"; 
            }

        }

        private void frmLogin_Load_1(object sender, EventArgs e)
        {
            this.ActiveControl = gizli_label; // Form açıldığında gizli `Label`a odaklan
        }

        private void frmLogin_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null; // Formun herhangi bir yerine tıklanabilir olmasını sağlarız.
        }        
    }
}