using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_De_Gestion.DAO;
using Sistema_De_Gestion.Models;



namespace Sistema_De_Gestion
{
    public partial class FormUsuarios : Form
    {
        UsuarioDAO usuarioDAO = new UsuarioDAO();
        int idSeleccionado = -1;

        public FormUsuarios()
        {
            InitializeComponent();  
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bienvenido al módulo de USUARIOS de TIENDA SARLUDABLE");
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.Rows.Clear();
            List<Usuario> lista = usuarioDAO.ObtenerUsuarios();
            foreach (var u in lista)
            {
                dgvUsuarios.Rows.Add(u.Id, u.Nombre, u.Rol);
            }
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario(
                    txtNombreUsuario.Text,
                    txtContrasenia.Text,
                    txtRolUsuario.Text
                );

                usuarioDAO.AgregarUsuario(usuario);
                MessageBox.Show("El Usuario fue Agregado Correctamente en la BD");
                LimpiarCampos();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Agregar El Usuario en la BD: " + ex.Message);
            }
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == -1)
            {
                MessageBox.Show("Selecciona El Usuario que Desea Modificar de la BD.");
                return;
            }

            Usuario usuario = new Usuario(
                txtNombreUsuario.Text,
                txtContrasenia.Text,
                txtRolUsuario.Text
            );
            usuario.Id = idSeleccionado;

            usuarioDAO.ActualizarUsuario(usuario);
            MessageBox.Show("El Usuario fue Modificado correctamente en la BD");
            LimpiarCampos();
            CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Modificar El Usuario de la BD: " + ex.Message);
            }
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Selecciona El Usuario que Desea Eliminar de la BD.");
                return;
            }

            usuarioDAO.EliminarUsuario(idSeleccionado);
            MessageBox.Show("Usuario fue Eliminado correctamente de la BD");
            LimpiarCampos();
            CargarUsuarios();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idSeleccionado = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells[0].Value);
                txtNombreUsuario.Text = dgvUsuarios.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRolUsuario.Text = dgvUsuarios.Rows[e.RowIndex].Cells[2].Value.ToString();
                
            }
        }

        private void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            txtContrasenia.Clear();
            txtRolUsuario.Clear();
            idSeleccionado = -1;
        }
    }
}