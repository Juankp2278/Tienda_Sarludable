using Sistema_De_Gestion.DAO;
using Sistema_De_Gestion.Models; // Ruta de conexion a la carpeta Models donde se encuentra la clase Producto
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
    public partial class FormProductos : Form
    {
        private bool modoEdicion = false;
        private int? idSeleccionado = null;

        ProductoDAO productoDAO = new ProductoDAO();

        public FormProductos()
        {
            InitializeComponent();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bienvenido al Módulo de PRODUCTOS de TIENDA SARLUDABLE");

            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.AutoGenerateColumns = false;

            ConfigurarColumnasDataGridView();
            CargarProductos();

            dgvProductos.SelectionChanged += new EventHandler(dgvProductos_SelectionChanged);
        }

        private void ConfigurarColumnasDataGridView()
        {
            dgvProductos.Columns.Clear();
             
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Visible = false
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre"
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción"
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                DataPropertyName = "Precio",
                HeaderText = "Precio"
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                DataPropertyName = "Stock",
                HeaderText = "Stock"
            });
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productoDAO.ObtenerTodos();
            dgvProductos.ClearSelection();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Producto nuevoProducto = new Producto
                {
                    Nombre = txtNombreProducto.Text,
                    Descripcion = txtDescripcionProducto.Text,
                    Precio = decimal.Parse(txtPrecioProducto.Text),
                    Stock = int.Parse(txtStockProducto.Text),
                    FechaRegistro = DateTime.Now
                };

                productoDAO.Agregar(nuevoProducto);
                CargarProductos();
                LimpiarCampos();

                MessageBox.Show("El Producto fue Agregado Correctamente a la Base de Datos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Agregar Producto a la Base de Datos: " + ex.Message);
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var fila = dgvProductos.SelectedRows[0];

                txtNombreProducto.Text = fila.Cells["Nombre"].Value?.ToString();
                txtDescripcionProducto.Text = fila.Cells["Descripcion"].Value?.ToString();
                txtPrecioProducto.Text = fila.Cells["Precio"].Value?.ToString();
                txtStockProducto.Text = fila.Cells["Stock"].Value?.ToString();

                idSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                if (idSeleccionado != null)
                {
                    modoEdicion = true;
                    btnEditar.Text = "Confirmar Edición";
                    MessageBox.Show("Modifica los Campos que Desee y vuelve a presionar el Botón Editar confirmar Los Cambios.");
                }
                else
                {
                    MessageBox.Show("Selecciona el Producto que Deseas Editar de la Base de Datos.");
                }
            }
            else
            {
                try
                {
                    Producto producto = new Producto
                    {
                        Id = idSeleccionado.Value,
                        Nombre = txtNombreProducto.Text,
                        Descripcion = txtDescripcionProducto.Text,
                        Precio = decimal.Parse(txtPrecioProducto.Text),
                        Stock = int.Parse(txtStockProducto.Text)
                    };

                    productoDAO.Actualizar(producto);
                    CargarProductos();
                    LimpiarCampos();

                    MessageBox.Show("El Producto fue Editado y Actualizado correctamente en la Base de Datos.");

                    modoEdicion = false;
                    btnEditar.Text = "Editar Producto";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Editar y/o Actualizar Producto en la Base de Datos: " + ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado != null)
            {
                var confirm = MessageBox.Show("¿Estás Seguro de esta Accion? El Producto sera Eliminado de manera Permanente de la Base de Datos", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    productoDAO.Eliminar(idSeleccionado.Value);
                    CargarProductos();
                    LimpiarCampos();

                    MessageBox.Show("EL Producto fue Eliminado de manera Permanente de la Base de Datos, Accion Ejecutada correctamente.");
                }
            }
            else
            {
                MessageBox.Show("Seleccioná El Producto para Eliminar de la Base de Datos.");
            }
        }

        private void LimpiarCampos()
        {
            txtNombreProducto.Clear();
            txtDescripcionProducto.Clear();
            txtPrecioProducto.Clear();
            txtStockProducto.Clear();
            idSeleccionado = null;
            dgvProductos.ClearSelection();
            btnEditar.Text = "Editar Producto";
            modoEdicion = false;
        }
    }
}
