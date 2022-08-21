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
    public partial class frmLogin : Form
    {
         string user= "";
         public static string func { get; set; }
        //Conexão com o Banco de Dados
        SqlConnection conexão = new SqlConnection(@"Data Source=KELSON-PC\SQLEXPRESS;Initial Catalog=dbPetshop;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adaptador;
        DataTable dados;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
          if (txtUsername.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("insira o Nome e a Senha");
            }
            else
            {
                if (RoleCh.SelectedIndex > -1)
                {
                    if (RoleCh.SelectedItem.ToString() == "Admin")
                    {
                        if (txtUsername.Text == "Admin" && txtSenha.Text  == "Admin")
                        {
                            //User = txtUsername.Text;
                            user = txtUsername.Text;
                            FrmMenu menu = new FrmMenu(user);
                            menu.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("os Dados Não Correspondem ao Admin");
                        }
                    }
                    else
                    {

                        try
                        {
                            conexão.Open();
                            adaptador = new SqlDataAdapter("select * from tbFuncionario where Nome=@nome AND Senha = @senha", conexão);
                            adaptador.SelectCommand.Parameters.AddWithValue("@nome", txtUsername.Text);
                            adaptador.SelectCommand.Parameters.AddWithValue("@senha", txtSenha.Text);
                            dados = new DataTable();
                            adaptador.Fill(dados);
                            conexão.Close();

                            //Verificar se as credenciais são válidas credenciais
                            if (dados.Rows.Count == 0)
                            {
                                MessageBox.Show("Dados Invalidos");
                                txtUsername.Clear();
                                txtSenha.Clear();
                                txtUsername.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Seja bem Vindo");
                                //User = txtUsername.Text;
                                user = txtUsername.Text;
                                FrmMenu menu = new FrmMenu(user);
                                menu.Show();
                                this.Hide();
                            }
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }
                     
                    }
                }

                else
                {
                    MessageBox.Show("Select a Role");
                }
            }
        }
      }
    }

