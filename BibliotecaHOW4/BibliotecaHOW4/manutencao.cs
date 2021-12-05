using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace BibliotecaHOW4
{
    public partial class manutencao : Form
    {
        private Thread nt;

        public manutencao()
        {
            InitializeComponent();
        }

        private MySqlConnectionStringBuilder conexaoBanco()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "biblioteca";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            conexaoBD.SslMode = 0;
            return conexaoBD;
        }


        private void loghome()
        {
            Application.Run(new home());
        }
        //RODAR PAGINA DO MENU PAGINA CADASTRO
        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(loghome);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }



        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dGmautencao.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dGmautencao.CurrentRow.Selected = true;
                //preenche os textbox com as células da linha selecionada
                tbNome.Text = dGmautencao.Rows[e.RowIndex].Cells["colNome"].FormattedValue.ToString();
                tbCategoria.Text = dGmautencao.Rows[e.RowIndex].Cells["colCategoria"].FormattedValue.ToString();
                tbDescricao.Text = dGmautencao.Rows[e.RowIndex].Cells["colDescricao"].FormattedValue.ToString();
                tbAutor.Text = dGmautencao.Rows[e.RowIndex].Cells["colAutor"].FormattedValue.ToString();
                tbEditora.Text = dGmautencao.Rows[e.RowIndex].Cells["colEditora"].FormattedValue.ToString();
                tbId.Text = dGmautencao.Rows[e.RowIndex].Cells["colId"].FormattedValue.ToString();
                tbStatus.Text = dGmautencao.Rows[e.RowIndex].Cells["colStatus"].FormattedValue.ToString();

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void limparCampos()
        {
            tbNome.Clear();
            tbAutor.Clear();
            tbEditora.Clear();
            tbCategoria.Clear();
            tbDescricao.Clear();
            tbId.Clear();
        }

        private void btnInativar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open(); //Abre a conexão com o banco

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand(); //Crio um comando SQL              
                comandoMySql.CommandText = "UPDATE livro SET statusLivro = 0 WHERE idLivro = " + tbId.Text + "";

                comandoMySql.ExecuteNonQuery();

                realizaConexacoBD.Close(); // Fecho a conexão com o banco
                MessageBox.Show("Inativado com sucesso"); //Exibo mensagem de aviso
                atualizaGrid();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO");
                Console.WriteLine(ex.Message);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open(); //Abre a conexão com o banco

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand(); //Crio um comando SQL
                comandoMySql.CommandText = " UPDATE livro SET nomeLivro = '" + tbNome.Text + "', " +
                    "autorLivro = '" + tbAutor.Text + "', " +
                    "editoraLivro = '" + tbEditora.Text + "', " +
                    "categoriaLivro = '" + tbCategoria.Text + "', " +
                    "descricaoLivro = '" + tbDescricao.Text + "', " +
                    "statusLivro = " + Convert.ToInt16(tbStatus.Text) +
                    " WHERE idLivro = " + tbId.Text + "";

                comandoMySql.ExecuteNonQuery();

                realizaConexacoBD.Close(); // Fecho a conexão com o banco

                MessageBox.Show("Atualizado com sucesso"); //Exibo mensagem de aviso
                atualizaGrid();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO");
                Console.WriteLine(ex.Message);
            }

        }
        //Atualiza o grid e BD
        private void manutencao_Load_1(object sender, EventArgs e)

        {
            atualizaGrid();
        }
        private void atualizaGrid()
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM livro WHERE statusLivro = 1";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dGmautencao.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dGmautencao.Rows[0].Clone();//FAZ UM CAST E CLONA A LINHA DA TABELA
                    row.Cells[0].Value = reader.GetInt32(0);//ID
                    row.Cells[1].Value = reader.GetString(1);//NOME
                    row.Cells[2].Value = reader.GetString(2);//AUTOR
                    row.Cells[3].Value = reader.GetString(3);//EDITORA
                    row.Cells[4].Value = reader.GetString(4);//CATEGORIA
                    row.Cells[5].Value = reader.GetString(5);//DESCRIÇÃO                  
                    row.Cells[6].Value = reader.GetString(6);//Status  
                    dGmautencao.Rows.Add(row);//ADICIONO A LINHA NA TABELA
                }

                realizaConexacoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
           

        }
    }
} 

