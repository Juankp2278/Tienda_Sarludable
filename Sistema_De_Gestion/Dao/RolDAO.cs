using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_De_Gestion.Models;  
using MySql.Data.MySqlClient;



namespace Sistema_De_Gestion.DAO
{
    public class RolDAO
    {
        
        private readonly string connectionString = "TU_CADENA_DE_CONEXION_AQUI"; 

        public List<Rol> ObtenerTodos()
        {
            List<Rol> roles = new List<Rol>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM roles";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Rol
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre")
                        });
                    }
                }
            }

            return roles;
        }

        public void Agregar(Rol rol)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO roles (nombre) VALUES (@nombre)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", rol.Nombre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM roles WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Rol rol)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE roles SET nombre = @nombre WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", rol.Nombre);
                    cmd.Parameters.AddWithValue("@id", rol.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}