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
    public partial class FormStock : Form
    {
        public FormStock()
        {
            InitializeComponent();
        }

        // Este método es el que te estaba faltando
        private void FormStock_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bienvenido Al Modulo de STOCK de TIENDA SARLUDABLE");
        }
    }
}