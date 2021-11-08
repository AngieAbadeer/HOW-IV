using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace BibliotecaHOW4
{
    public partial class home : Form
    {
        Thread nt;
        public home()
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

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(logCadastro);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void logCadastro()
        {
            Application.Run(new CadastroLivro());
        }

        private void manutençãoAlterarInativarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(logmanutenao);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void logmanutenao()
        {
            Application.Run(new manutencao());
        }

        private void cadastarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
            nt = new Thread(logLeitor);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void logLeitor()
        {
            Application.Run (new Leitor());
        }
    }
}
