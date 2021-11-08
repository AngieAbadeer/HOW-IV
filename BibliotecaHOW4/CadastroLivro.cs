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
    public partial class CadastroLivro : Form
    {
        Thread nt;
        public CadastroLivro()
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

        //RODAR PAGINA DO MENU PAGINA HOME
        private void paginaInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(loghome);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void loghome()
        {
            Application.Run(new home());
        }
        //RODAR PAGINA DO MENU PAGINA MANUTENÇÃO
        private void manutençãoAlterarInativarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(logmanutenção);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
        
        private void logmanutenção()
        {
            Application.Run(new manutencao());
        }
        //INSERI CADASTRO DE NOVO LIVRO NO BANCO DE DADOS
        private void btnInserir_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();

                comandoMySql.CommandText = "INSERT INTO livro (nomeLivro,autorLivro,editoraLivro,categoriaLivro,descricaoLivro) " +
                    "VALUES ('" + tbNome.Text + "', '" + tbAutor.Text + "', '" + tbEditora.Text + "','" + tbCategoria.Text + "', '" + tbDescricao.Text + "')";
                comandoMySql.ExecuteNonQuery();

                realizaConexacoBD.Close();
                MessageBox.Show("Inserido com sucesso");
                atualizaGrid();
                limparCampos();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //LIMPAR DADOS DOS CAMPOS
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
            //tbID.Clear();
        }
        //ATUALIZAR O DATAGRID E BANCO DE DADOS
        private void CadastroLivro_Load(object sender, EventArgs e)
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

                dgBiblioteca.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgBiblioteca.Rows[0].Clone();//FAZ UM CAST E CLONA A LINHA DA TABELA
                    row.Cells[0].Value = reader.GetInt32(0);//ID
                    row.Cells[1].Value = reader.GetString(1);//NOME
                    row.Cells[2].Value = reader.GetString(2);//AUTOR
                    row.Cells[3].Value = reader.GetString(3);//EDITORA
                    row.Cells[4].Value = reader.GetString(4);//CATEGORIA
                    row.Cells[5].Value = reader.GetString(5);//DESCRIÇÃO                  
                    dgBiblioteca.Rows.Add(row);//ADICIONO A LINHA NA TABELA
                }

                realizaConexacoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }
        }
    }

        
    }
