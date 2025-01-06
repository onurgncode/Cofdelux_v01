using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cofdeluxProjectApp.formList
{
    public partial class ayarlar : KryptonForm
    {
        public frmLogin login;
        frmHome home;
        public ayarlar(frmHome home)
        {
            InitializeComponent();
           this.home = home;
        }
        public bool biBak;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            biBak = true;
            login = new frmLogin();
            login.ShowLoginForm();
            this.home.Visible = false;
            this.home = null;
            this.Dispose();
            
        }
        public void OpenWebsite(string url)
        {
            // Varsayılan tarayıcıyı kullanarak web sitesini açar
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenWebsite("https://github.com/onurgncode/cofdelux");
        }
    }
}
