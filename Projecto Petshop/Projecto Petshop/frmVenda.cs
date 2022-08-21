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
using System.Text.RegularExpressions;

namespace Projecto_Petshop
{
    public partial class frmVenda : Form
    {
        //Conection to the DB
        SqlConnection con = new SqlConnection(@"Data Source=KELSON-PC\SQLEXPRESS;Initial Catalog=dbPetshop;Integrated Security=True");
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataTable data;
        //Variavél que pega o id do produto
        int codigo;

        
        public frmVenda()
        {
            
            InitializeComponent();
            lblFunc.Text = frmLogin.func;
            txtprodName.Enabled = false;
            txtPreco.Enabled = false;
            ConsultarVendas();
            GetCustomers();
            ShowProduct();
           
          
        }

        private void ConsultarVendas()
        {
            con.Open();
            adapter = new SqlDataAdapter("spconsultar_Venda", con);
            data = new DataTable();
            adapter.Fill(data);
            con.Close();
            //Show Data in the DataGridView
            dgvListaVenda.DataSource = data;
        }

        private void ShowProduct() 
        {
            con.Open();
            adapter = new SqlDataAdapter("select id_Prod, Nome, Preco, Quantidade from tbProduto", con);
            data = new DataTable();
            adapter.Fill(data);
            con.Close();
            //Show Data in the DataGridView
            dgvProducts.DataSource = data;
            //Ocultar a Tabela Id
            dgvProducts.Columns[0].Visible = false;

        }

        private void GetCustomers()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select id_Cliente from tbCliente", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id_Cliente", typeof(int));
            dt.Load(Rdr);
            cmbId.ValueMember = "id_Cliente";
            cmbId.DataSource = dt;
            con.Close();
        }

        private void GetCustName()
        {
            
            con.Open();
            string Query = "Select * from tbCliente where id_Cliente = '" + cmbId.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtCliName.Text = dr["Nome"].ToString();
            }
            con.Close();
        }

        //Actualizar o Estoque
        private void UpdateStock()
        {
            try
            {
                int NewQty = stock - Convert.ToInt32(txtQtd.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Update tbProduto set Quantidade=@NewQtd where id_Prod=@Codigo", con);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@NewQtd", NewQty);
               
                cmd.ExecuteNonQuery();
                //MessageBox("Product Updated");
                con.Close();
                ShowProduct();
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Limpar()
        {
            txtprodName.Clear();
            txtQtd.Text = "";
            txtPreco.Clear();
        }

        int GrdTotal = 0, n = 0;

        private void btnAddVenda_Click(object sender, EventArgs e)
        {
         
        
            if (txtQtd.Text == "" || Convert.ToInt32(txtQtd.Text) > stock)
            {
                MessageBox.Show("Stock Insuficiente");
                Limpar();
            }
           
            else if (txtprodName.Text == "" || txtQtd.Text == "")
            {
                MessageBox.Show("Todos os Campos Precisam Esta Preenchidos");
            }
            else 
            {
                
                int total = Convert.ToInt32(txtPreco.Text) * Convert.ToInt32(txtQtd.Text);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(DGVenda);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = txtprodName.Text;
                newRow.Cells[2].Value = txtPreco.Text;
                newRow.Cells[3].Value = txtQtd.Text;
                newRow.Cells[4].Value = total;
                GrdTotal = GrdTotal + total;
                DGVenda.Rows.Add(newRow);
                n++;
                TotalLbl.Text = " " + GrdTotal;
                UpdateStock();
                Limpar();
                
               
            }
        }

        //Variavél que vai pegar a Quantidade em stock
        int stock = 0;
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                codigo = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells[0].Value);
                txtprodName.Text = dgvProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPreco.Text = dgvProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
                stock = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells[3].Value.ToString());


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void cmbId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        //Metodo Para Inserir Vendas
        private void InserirVenda()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("spInserir_Venda", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Data_venda", DateTime.Today.Date); 
                cmd.Parameters.AddWithValue("@id_Cliente", cmbId.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Total", GrdTotal);
                cmd.Parameters.AddWithValue("@NomeFunc", lblFunc.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Venda Concluida");
                con.Close();
                ConsultarVendas();
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            InserirVenda();
        }

        int prodId, prodQtd, prodPrice, Tottal, pos = 60;
        string prodname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Deda's Petshop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID, PRODUCT,PRICE,QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));

            foreach (DataGridViewRow row in DGVenda.Rows)
            {
                prodId = Convert.ToInt32(row.Cells[0].Value);
                prodname = "" + row.Cells[1].Value;
                prodPrice = Convert.ToInt32(row.Cells[2].Value);
                prodQtd = Convert.ToInt32(row.Cells[3].Value);
                Tottal = Convert.ToInt32(row.Cells[4].Value);

                e.Graphics.DrawString("" + prodId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodPrice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodQtd, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + Tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }

            e.Graphics.DrawString("Total: KZ " + GrdTotal, new Font("Century Gothic", 12, FontStyle.Italic), Brushes.Red, new Point(50,pos + 50));
            e.Graphics.DrawString("******Petshop*********", new Font("Century Gothic", 15, FontStyle.Italic), Brushes.Red, new Point(10,pos + 85));
            DGVenda.Rows.Clear();
            DGVenda.Refresh();
            pos = 100;
            
            n = 0;
        }

        private void txtQtd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
