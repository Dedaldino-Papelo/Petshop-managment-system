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
    public partial class frmFuncionario : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=KELSON-PC\SQLEXPRESS;Initial Catalog=dbPetshop;Integrated Security=True");
        SqlCommand comando;
        DataTable dados;
        SqlDataAdapter adaptador;

        public frmFuncionario()
        {
            InitializeComponent();
            txtcodigo.Enabled = false;
            Display();
        }

        private void Display()
        {
            con.Open();
           dados  = new DataTable();
           adaptador = new SqlDataAdapter("spconsultar_Funcionario", con);
           adaptador.Fill(dados);
            con.Close();
            //Display the data in DataGridView
            dgvFuncionario.DataSource = dados;
        }

        private void Clear() 
        {
            txtcodigo.Clear();
            txtnome.Clear();
            txtmorada.Clear();
            txtTelefone.Clear();
            txtSenha.Clear();
            txtnome.Focus();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            
        
            if (txtnome.Text == "" || txtmorada.Text == "" || txtTelefone.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("Preencha todos os campos");
            }
            else {
                try
                {
                    con.Open();
                    comando = new SqlCommand("spInserir_Funcionario", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nome", txtnome.Text);
                    comando.Parameters.AddWithValue("@Morada", txtmorada.Text);
                    comando.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                    comando.Parameters.AddWithValue("@Senha", txtSenha.Text);
                    comando.Parameters.AddWithValue("@Data_nascimento", DOBPicker.Value.Date);
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

        private void dgvFuncionario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdicionar.Enabled = false;
            txtcodigo.Text = dgvFuncionario.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtnome.Text = dgvFuncionario.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtmorada.Text = dgvFuncionario.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTelefone.Text = dgvFuncionario.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSenha.Text = dgvFuncionario.Rows[e.RowIndex].Cells[4].Value.ToString();
            DOBPicker.Text = dgvFuncionario.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (txtnome.Text == "" || txtmorada.Text == "" || txtTelefone.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    con.Open();
                    comando = new SqlCommand("spActualizar_Funcionario", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_Func", txtcodigo.Text);
                    comando.Parameters.AddWithValue("@Nome", txtnome.Text);
                    comando.Parameters.AddWithValue("@Morada", txtmorada.Text);
                    comando.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                    comando.Parameters.AddWithValue("@Senha", txtSenha.Text);
                    comando.Parameters.AddWithValue("@Data_nascimento", DOBPicker.Value.Date);
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

            if (txtnome.Text == "" || txtmorada.Text == "" || txtTelefone.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("Nenhum Registro Selecionado");
            }
            else
            {
                try
                {
                    con.Open();
                    comando = new SqlCommand("spExcluir_Funcionario", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_Func", txtcodigo.Text);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Eliminado");
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
