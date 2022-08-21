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
    public partial class FrmMenu : Form
    {
        string Normaluser;
        
        public FrmMenu(string usertype)
        {
            InitializeComponent();
            Normaluser = usertype;
            if (Normaluser == "Admin")
            {
                MessageBox.Show("Bem Vindo");
            }
            else
            {
                button5.Hide();
                btnCategoria.Hide();
                //btnCliente.Hide();
                btnFuncionario.Hide();
                btnProdutos.Hide();
               // btnVenda.Hide();
            }
            
        }
        public void OpenChieldForm(object form)
        {
            if (this.PanelMain.Controls.Count > 0)
            this.PanelMain.Controls.RemoveAt(0);
            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.PanelMain.Controls.Add(f);
            this.PanelMain.Tag = f;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            OpenChieldForm(new frmCategoria());
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            OpenChieldForm(new frmProdutos());
        }

        private void btnFuncionario_Click(object sender, EventArgs e)
        {
            OpenChieldForm(new frmFuncionario());
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            OpenChieldForm(new frmCliente());
        }

        private void btnVenda_Click(object sender, EventArgs e)
        {
            OpenChieldForm(new frmVenda());
        }

        private void button17_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }
       
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChieldForm(new frmHome());
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
          
        }

        

    }
}
