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
    public partial class Form1 : Form
    {
        Thread nt;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//Criado comando para verificação de login valido.
        {
            if (txbLogin.Text == "Admin" && txbSenha.Text == "1234")
            {
                //COMANDO PARA SIRRECIONAR AO FORM HOME.
                this.Close();
                nt = new Thread(logHome);
                nt.SetApartmentState(ApartmentState.STA);
                nt.Start();
            }
            else
            {
                MessageBox.Show("Login ou Senha Invalidos!");
            }
        }

        private void logHome() //PARA RODAR A APLICAÇÃO
        {
            Application.Run(new home());
        }

        private void button2_Click(object sender, EventArgs e)//COMANDO PARA SAIR DO SISTEMA
        {
            Application.Exit();
        }
    }
}
