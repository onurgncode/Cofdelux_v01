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
    public partial class frmDestek : KryptonForm
    {
        private frmHome home;
        public frmDestek(frmHome method)
        {
            InitializeComponent();
            home = method;
        }

        private void frmDestek_Load(object sender, EventArgs e)
        {

        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            home.geriDon();
        }
        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDestek_Load_1(object sender, EventArgs e)
        {

        }
    }
}
