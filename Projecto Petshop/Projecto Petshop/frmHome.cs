using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Projecto_Petshop
{
    public partial class frmHome : Form
    {
        SqlConnection conexão = new SqlConnection(@"Data Source=KELSON-PC\SQLEXPRESS;Initial Catalog=dbPetshop;Integrated Security=True");
        public frmHome()
        {
            InitializeComponent();
            TotalVendas();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //
        private void TotalVendas()
        {
            conexão.Open();
            SqlDataAdapter dt = new SqlDataAdapter("select sum(Total) Total from tbVenda", conexão);
            DataTable data = new DataTable();
            dt.Fill(data);
            lblTotall.Text = "KZ " + data.Rows[0][0].ToString();
            conexão.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
