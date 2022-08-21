using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projecto_Petshop
{
    public partial class Splash : Form
    {
        int startpoint = 0;
        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            Progressive.Value = startpoint;
            lblPercentagem.Text = startpoint + "%";
            if (Progressive.Value == 100)
            {
                Progressive.Value = 0;
                timer1.Stop();
                frmLogin login = new frmLogin();
                this.Hide();
                login.Show();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
