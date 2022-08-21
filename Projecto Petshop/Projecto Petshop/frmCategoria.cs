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
    public partial class frmCategoria : Form
    {
        //Conexão com o Banco de Dados
        SqlConnection conexão = new SqlConnection(@"Data Source=KELSON-PC\SQLEXPRESS;Initial Catalog=dbPetshop;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adaptador;
        DataTable dados;

        public frmCategoria()
        {
            InitializeComponent();
            Display();
            txtCodigo.Enabled = false;
        }

        private void Display()
        {
            conexão.Open();
            dados = new DataTable();
            adaptador = new SqlDataAdapter("spConsultar_Categoria",conexão);
            adaptador.Fill(dados);
            conexão.Close();
            //Display the data in DataGridView
            dgvcategoria.DataSource = dados;
        }

        private void Clear() 
        {
            txtCodigo.Text = "";
            txtNome.Clear();
            txtNome.Focus();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            
            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha Todos os Campos");
            }
            else
            {
                try 
                {
                    conexão.Open();
                    cmd = new SqlCommand("spInserir_Categoria", conexão);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.ExecuteNonQuery();
                    conexão.Close();
                    MessageBox.Show("Registro Inserido");
                    Display();
                    Clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void dgvcategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdicionar.Enabled = false;
            txtCodigo.Text = dgvcategoria.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNome.Text = dgvcategoria.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtCodigo.Text == "")
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    conexão.Open();
                    cmd = new SqlCommand("spActualizar_Categoria", conexão);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Cat", txtCodigo.Text);
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.ExecuteNonQuery();
                    conexão.Close();
                    MessageBox.Show("Registro Actualizado");
                    btnAdicionar.Enabled = true;
                    Display();
                    Clear();
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtCodigo.Text == "")
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    conexão.Open();
                    cmd = new SqlCommand("spExcluir_Categoria", conexão);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Cat", txtCodigo.Text);
                    cmd.ExecuteNonQuery();
                    conexão.Close();
                    MessageBox.Show("Registro Eliminado");
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

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {

        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
              
        }
    }
}
