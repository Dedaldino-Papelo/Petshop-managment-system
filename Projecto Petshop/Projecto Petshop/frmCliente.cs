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
    public partial class frmCliente : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=KELSON-PC\SQLEXPRESS;Initial Catalog=dbPetshop;Integrated Security=True");
        SqlCommand comando;
        DataTable dados;
        SqlDataAdapter adaptador;

        public frmCliente()
        {
            InitializeComponent();
            Display();
            txtCodigo.Enabled = false;
        }

        private void Display()
        {
            con.Open();
            dados = new DataTable();
            adaptador = new SqlDataAdapter("spconsultar_Cliente", con);
            adaptador.Fill(dados);
            con.Close();
            //Display the data in DataGridView
            dgvCliente.DataSource = dados;
        }

        private void Clear()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtSobrenome.Clear();
            txtTelefone.Clear();
            txtNome.Focus();
        }


        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            
            if (txtNome.Text == "" || txtTelefone.Text == "" || txtSobrenome.Text == "")
            {
                MessageBox.Show("Preencha todos os campos");
            }
            else
            {
                try
                {
                    con.Open();
                    comando = new SqlCommand("spInserir_Cliente", con);

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nome", txtNome.Text);
                    comando.Parameters.AddWithValue("@Sobrenome", txtSobrenome.Text);
                    comando.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                  
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Inserido");
                    con.Close();
                    Display();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdicionar.Enabled = false;
            txtCodigo.Text = dgvCliente.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNome.Text = dgvCliente.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSobrenome.Text = dgvCliente.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTelefone.Text = dgvCliente.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtTelefone.Text == "" || txtSobrenome.Text == "")
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    con.Open();
                    comando = new SqlCommand("spActualizar_Cliente", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_Cliente", txtCodigo.Text);
                    comando.Parameters.AddWithValue("@Nome", txtNome.Text);
                    comando.Parameters.AddWithValue("@Sobrenome", txtSobrenome.Text);
                    comando.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Actualizado");
                    btnAdicionar.Enabled = true;
                    con.Close();
                    Display();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (txtNome.Text == "" || txtTelefone.Text == "" || txtSobrenome.Text == "")
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    con.Open();
                    comando = new SqlCommand("spExcluir_Cliente", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_Cliente", txtCodigo.Text);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Excluido");
                    btnAdicionar.Enabled = true;
                    con.Close();
                    Display();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
    }
}
