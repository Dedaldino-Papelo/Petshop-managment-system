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
    public partial class frmProdutos : Form
    {
        //Conexão com o Banco de Dados
        SqlConnection conexão = new SqlConnection(@"Data Source=KELSON-PC\SQLEXPRESS;Initial Catalog=dbPetshop;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adaptador;
        DataTable dados;
        //Pegar o índice dos Items no ComboBox
        string codigo_item;

        public frmProdutos()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            fillcombo();
            Display();
        }

        private void Display()
        {
            conexão.Open();
            dados = new DataTable();
            adaptador = new SqlDataAdapter("spconsultar_Produto", conexão);
            adaptador.Fill(dados);
            conexão.Close();
            //Display the data in DataGridView
            dgvProdutos.DataSource = dados;
             
        }

        private void Clear()
        {
            txtCodigo.Text = "";
            txtNome.Clear();
            txtPreco.Text = "";
            txtQuant.Text = "";
            cmbCategoria.ResetText();
            txtNome.Focus();
        }

        //Metodo para trazer todas as categórias dentro da comboBox
        private void fillcombo()
        {
            conexão.Open();
            cmd = new SqlCommand("select Nome from tbCategoria", conexão);
            adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            cmd.Dispose();
            dados = new DataTable();
            adaptador.Fill(dados);
            conexão.Close();
            foreach (DataRow dr in dados.Rows)
            {
                cmbCategoria.Items.Add(dr["Nome"].ToString());
            }
        }



        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            
            if (txtNome.Text == "" || txtPreco.Text == "" || txtQuant.Text == "" || cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha Todos os Campos");
            }
            else
            {
                try
                {
                    conexão.Open();
                    cmd = new SqlCommand("spInserir_Produto", conexão);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Quantidade", txtQuant.Text);
                    cmd.Parameters.AddWithValue("@Preco", txtPreco.Text);
                    cmd.Parameters.AddWithValue("@id_Cat", codigo_item);
                    cmd.ExecuteNonQuery();
                    conexão.Close();
                    MessageBox.Show("Registro Inserido");
                    Display();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            conexão.Open();
            string query = "select id_Cat from tbCategoria where Nome = '" + cmbCategoria.SelectedItem + "'";
            cmd = new SqlCommand(query, conexão);
            dados = new DataTable();
            adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            adaptador.Fill(dados);
            foreach (DataRow dr in dados.Rows)
            {
                codigo_item = dr[0].ToString();
            }
            conexão.Close();
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdicionar.Enabled = false;
            txtCodigo.Text = dgvProdutos.Rows[e.RowIndex].Cells["id_Prod"].Value.ToString();
            txtNome.Text = dgvProdutos.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtQuant.Text = dgvProdutos.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPreco.Text = dgvProdutos.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbCategoria.Text = dgvProdutos.Rows[e.RowIndex].Cells["Categoria"].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "" || txtNome.Text == "" || txtPreco.Text == "" || txtQuant.Text == "" || cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    conexão.Open();
                    cmd = new SqlCommand("spActualizar_Produto", conexão);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Prod", txtCodigo.Text);
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Quantidade", txtQuant.Text);
                    cmd.Parameters.AddWithValue("@Preco", txtPreco.Text);
                    cmd.Parameters.AddWithValue("@id_Cat", codigo_item);
                    cmd.ExecuteNonQuery();
                    conexão.Close();
                    MessageBox.Show("Registro Actualizado");
                    btnAdicionar.Enabled = true;
                    Display();
                    Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text == "" || txtNome.Text == "" || txtPreco.Text == "" || txtQuant.Text == "" || cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    conexão.Open();
                    cmd = new SqlCommand("spExcluir_Produto", conexão);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Prod", txtCodigo.Text);
                    cmd.ExecuteNonQuery();
                    conexão.Close();
                    MessageBox.Show("Registro Excluido");
                    btnAdicionar.Enabled = true;
                    Display();
                    Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void txtQuant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
