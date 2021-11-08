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

namespace BibliotecaHOW4
{
    public partial class manutencao : Form
    {
        private Thread nt;

        public manutencao()
        {
            InitializeComponent();
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
        //RODAR PAGINA DO MENU PAGINA CADASTRO
        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(loghome);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }
    }
}
