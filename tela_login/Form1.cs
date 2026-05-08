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


namespace tela_login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadLogin_Click(object sender, EventArgs e)
        {
            string campoLogin = txtEmail.Text;
            string campoSenha = txtSenha.Text;

            MessageBox.Show(
                $"Login: {campoLogin}");


            int controleLinhasAfetadas = 0;

            string dadosConexao = "server=localhost;user=root;password=;database=ecoeficiencia";
            using (MySqlConnection conn = new MySqlConnection(dadosConexao))
            { //utilizo das informações
                conn.Open();
                string scriptInsert = "INSERT INTO tb_acesso (Login) VALUE (@Login)";

                using (MySqlCommand comando = new MySqlCommand(scriptInsert, conn))
                {
                    comando.Parameters.AddWithValue("@Login", campoLogin);

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
    }
}
