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

namespace cofdeluxProjectApp
{
    public partial class frmSignup : KryptonForm
    {
        user User = new user();
        private frmLogin sign; 
        public frmSignup(frmLogin Form)
        {
            InitializeComponent();
            sign = Form; // login formunu referans alır
        }
        // Giriş Yapma sayfasını açar 
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            sign.ShowLoginForm();
            this.Hide(); // Signup formunu gizle
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            User.userEkle(txtName.Text,txtSirName.Text,txtPass.Text,txtEmail.Text,txtPhone.Text);
        }

        private void frmSignup_Load(object sender, EventArgs e)
        {
            this.ActiveControl = gizli_kayıt_label; // Form açıldığında gizli `Label`a odaklan


        }
        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.Text = "";
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
           if ( txtName.Text == "")
            {
                txtName.Text = "İsim";
            }
        }

        private void txtSirName_Enter(object sender, EventArgs e)
        {
            txtSirName.Text = "";
        }

        private void txtSirName_Leave(object sender, EventArgs e)
        {
            if(txtSirName.Text == "")
            {
                txtSirName.Text = "Soyisim";
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
                txtEmail.Text = "E mail";
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
                txtPass.Text = "Şifre";
            }
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            txtPhone.Text = ""; 
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text == "")
            {
                txtPhone.Text = "Telefon";
            }   
        }

        private void txtToken_Enter(object sender, EventArgs e)
        {
            txtToken.Text = ""; 
        }

        private void txtToken_Leave(object sender, EventArgs e)
        {
            if (txtToken.Text == "")
            {
                txtToken.Text = "Token";
            }
        }

        private void frmSignup_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null; // Formun herhangi bir yerine tıklandığında odaklanmayı kaldır.
        }
    }
    }

