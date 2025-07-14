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
    public partial class Form1 : Form
    {         
        List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario("admin", "1234", "administrador"),
            new Usuario("vendedor", "7890", "vendedor"),
            new Usuario("caja", "4567", "caja")

        };

        private Usuario ValidarUsuario(string nombre, string pass)
        {
            return usuarios.FirstOrDefault(u => u.Nombre == nombre && u.Contraseña == pass);
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "CONTRASEÑA")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "CONTRASEÑA";
                txtPass.ForeColor = Color.DimGray;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim().ToLower();
            string contraseña = txtPass.Text.Trim();

            Usuario user = ValidarUsuario(usuario, contraseña);

            if (user != null)
            {
                this.Hide(); // Ocultamos el login

                Form formulario = null;

                switch (user.Rol)
                {
                    case "administrador":
                        formulario = new FormAdmin();
                        break;
                    case "vendedor":
                        formulario = new FormVendedor();
                        break;
                    case "caja":
                        formulario = new FormCaja();
                        break;
                }

                if (formulario != null)
                {
                    formulario.ShowDialog(); // Espera a que se cierre
                    this.Show();             // Vuelve al login
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
