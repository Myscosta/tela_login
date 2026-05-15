using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using Org.BouncyCastle.Bcpg.OpenPgp;


namespace tela_login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const string DADOS_CONEXAO =
            "server=localhost;user=root;password=;database=ecoeficiencia";
        private void btnCadLogin_Click(object sender, EventArgs e)
        {
            string campoLogin = txtEmail.Text;
            string campoSenha = txtSenha.Text;
            string campoNome = txtNome.Text;

            MessageBox.Show($"Login: {campoNome}");

            int controleLinhasAfetadas = 0;

            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            { //utilizo das informações

                conn.Open();
                string scriptInsert = "INSERT INTO tb_dados (nome, login, senha) " +
                                        "VALUE (@nome, @login, @senha)";

                using (MySqlCommand comando = new MySqlCommand(scriptInsert, conn))
                {
                    comando.Parameters.AddWithValue("@Login", campoLogin);
                    comando.Parameters.AddWithValue("@Senha", campoSenha);
                    comando.Parameters.AddWithValue("@Nome", campoNome);

                    controleLinhasAfetadas = comando.ExecuteNonQuery();
                }
                conn.Close();
            }//MySqlConnection

            if (controleLinhasAfetadas > 0)
            {
                MessageBox.Show("Dados salvo com sucesso!");

            }
            else
            {
                MessageBox.Show("Ooops algo deu errado");
            }

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            string campoLogin = txtEmail.Text;
            string campoSenha = txtSenha.Text;
            string campoNome = txtNome.Text;

            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptConsultaIndividual = "";

                if (campoNome != "")
                {
                    scriptConsultaIndividual = "SELECT * FROM tb_dados WHERE nome = @nome";
                }
                else
                {
                    scriptConsultaIndividual = "SELECT * FROM tb_dados";
                }

                using (MySqlCommand comando = new MySqlCommand(scriptConsultaIndividual, conn))
                {
                    if (campoNome != "")
                    {
                        comando.Parameters.AddWithValue("@nome", campoNome);
                    }

                    MySqlDataAdapter resultadoConsultaMySql = new MySqlDataAdapter(comando);


                    DataTable dt = new DataTable();

                    resultadoConsultaMySql.Fill(dt);

                    dgvVisualizar.DataSource = dt;
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {

            string campoLogin = txtEmail.Text;
            string campoSenha = txtSenha.Text;
            int controleLinhasAfetadas = 0;

            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            { //utilizo das informações
                conn.Open();
                string scriptDelete = "DELETE FROM tb_acesso WHERE id= @id";

                using (MySqlCommand comando = new MySqlCommand(scriptDelete, conn))
                {
                    comando.Parameters.AddWithValue("@login", campoLogin);
                    comando.Parameters.AddWithValue("@senha", campoSenha);

                    controleLinhasAfetadas = comando.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            string campoLogin = txtEmail.Text;
            string campoSenha = txtSenha.Text;
            int controleLinhasAfetadas = 0;

            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            { //utilizo das informações
                conn.Open();
                string scriptUpdate = "UPDATE tb_acesso SET" + "nome = @nome WHERE id =@id";

                using (MySqlCommand comando = new MySqlCommand(scriptUpdate, conn))
                {
                    comando.Parameters.AddWithValue("@id", campoLogin);

                    controleLinhasAfetadas = comando.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}