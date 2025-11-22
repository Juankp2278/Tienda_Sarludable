using MySql.Data.MySqlClient;
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
    public partial class FormLogin : Form
    {         
        List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario("admin", "1234", "administrador"),
            new Usuario("vendedor", "7890", "vendedor"),
            new Usuario("caja", "4567", "caja")

        };

        private Usuario ValidarUsuario(string nombre, string pass)
        {
            ConexionBD conexion = new ConexionBD();
            using (var conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT id, nombre, contrasenia, rol FROM usuarios WHERE nombre = @nombre AND contrasenia = @pass";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario(
                                Convert.ToInt32(reader["id"]),
                                reader["nombre"].ToString(),
                                reader["contrasenia"].ToString(),
                                reader["rol"].ToString()
                            );
                        }
                    }
                }
            }
            return null;
        }



        public FormLogin()
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
                MessageBox.Show("El Usuario o la Contraseña son Incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
