using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_De_Gestion
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            new FormUsuarios().ShowDialog();
        }      

        private void btnGestionProductos_Click_1(object sender, EventArgs e)
        {
            FormProductos productos = new FormProductos();
            productos.ShowDialog();

        }

        private void btnControlStock_Click(object sender, EventArgs e)
        {
            FormStock stock = new FormStock();
            stock.ShowDialog();

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
             FormReportes reportes = new FormReportes();
    reportes.ShowDialog();

        }
        private void FormAdmin_Load(object sender, EventArgs e)
        {             
            MessageBox.Show("Bienvenido al Panel de Administración de TIENDA SARLUDABLE.");
        }

        private void FormAdmin_Load_1(object sender, EventArgs e)
        {

        }
    }
}
