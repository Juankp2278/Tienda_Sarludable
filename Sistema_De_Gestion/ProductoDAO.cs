using MySql.Data.MySqlClient;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_De_Gestion.Models; // Ruta de conexion a la carpeta Models donde se encuentra la clase Producto

namespace Sistema_De_Gestion.DAO
{
    internal class ProductoDAO
    {
        private readonly string connectionString = "server=localhost;user=root;password=;database=sarludables_db";

        // Obtener todos los productos
        public List<Producto> ObtenerTodos()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM productos";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Producto
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Descripcion = reader.GetString("descripcion"),
                            Precio = reader.GetDecimal("precio"),
                            Stock = reader.GetInt32("stock"),
                            FechaRegistro = reader.GetDateTime("fecha_registro")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener Productos de la BD: " + ex.Message);
            }

            return lista;
        }

        // Agregar un producto
        public void Agregar(Producto producto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO productos (nombre, descripcion, precio, stock) VALUES (@nombre, @descripcion, @precio, @stock)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Agregar El Producto en la BD: " + ex.Message);
            }
        }
       
        public void Actualizar(Producto producto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE productos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, stock = @stock WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@id", producto.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Actualizar El Producto en la BD: " + ex.Message);
            }
        }

        // Eliminar producto
        public void Eliminar(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM productos WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar El producto de la BD: " + ex.Message);
            }
        }

        // Buscar producto por ID
        public Producto BuscarPorId(int id)
        {
            Producto producto = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM productos WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Descripcion = reader.GetString("descripcion"),
                            Precio = reader.GetDecimal("precio"),
                            Stock = reader.GetInt32("stock"),
                            FechaRegistro = reader.GetDateTime("fecha_registro")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Buscar El Producto en la BD: " + ex.Message);
            }

            return producto;
        }
    }
}