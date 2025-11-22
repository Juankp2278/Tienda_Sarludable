using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_De_Gestion.Models;
using MySql.Data.MySqlClient;
 
namespace Sistema_De_Gestion.DAO
{
    public class UsuarioDAO
    {
        private string connectionString = "server=localhost;user=root;password=;database=sarludables_db";

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM usuarios";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario(
                            reader.GetInt32("id"),
                            reader.GetString("nombre"),
                            reader.GetString("contrasenia"),
                            reader.GetString("rol")
));
                    }
                }
            }
            return lista;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO usuarios (nombre, contrasenia, rol) VALUES (@nombre, @contrasenia, @rol)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
                cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE usuarios SET nombre = @nombre, contrasenia = @contrasenia, rol = @rol WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
                cmd.Parameters.AddWithValue("@rol", usuario.Rol);                
                cmd.Parameters.AddWithValue("@id", usuario.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM usuarios WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
