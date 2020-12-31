using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBEPAEscritorio
{
    public partial class ProductosBuscarCategoria : Form
    {
        private DataTable DTCategorias = new DataTable();
        public ProductosBuscarCategoria()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Se minimiza el form
            this.WindowState = FormWindowState.Minimized;
        }

        private void ProductosBuscarCategoria_Load(object sender, EventArgs e)
        {    
            CargarCategorias();
            cmbBuscarEn.Text = "SubCategoria";
        }

        private void CargarCategorias()
        {
            ComandosBDMySQL cargarCategorias = new ComandosBDMySQL();
            try
            {
                cargarCategorias.AbrirConexionBD1();
                dgbCategoria.DataSource = cargarCategorias.RellenarTabla1("SELECT * FROM sbepa.vista_categorias_unidas;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar las categorias registradas en el sistema ERROR: "+ex.Message+"","Error Cargar Datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarCategorias.CerrarConexionBD1();
            }
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            //Se filtran los resultados de categorias
            ComandosBDMySQL cargarBusquedaCategoria = new ComandosBDMySQL();
            try
            {
                cargarBusquedaCategoria.AbrirConexionBD1();
                if (cmbBuscarEn.Text == "Categoria")
                {
                    dgbCategoria.DataSource = cargarBusquedaCategoria.RellenarTabla1("SELECT categorias.nombre as 'Categoria', categoria_simple.nombre_categoriasimple 'CategoriaSimple', sub_categoria.nombre_categoria as 'SubCategoria', sub_categoria.id_subcategoria as 'IDSubCategoria' FROM categorias inner join categoria_simple on categorias.idcategoria = categoria_simple.id_categorias inner join sub_categoria on categoria_simple.id_categoriasimple = sub_categoria.id_categoria_simple where categorias.nombre LIKE '%" + txtBuscarEn.Text + "%'; ");
                }
                else if (cmbBuscarEn.Text == "CategoriaSimple")
                {
                    dgbCategoria.DataSource = cargarBusquedaCategoria.RellenarTabla1("SELECT categorias.nombre as 'Categoria', categoria_simple.nombre_categoriasimple 'CategoriaSimple', sub_categoria.nombre_categoria as 'SubCategoria', sub_categoria.id_subcategoria as 'IDSubCategoria' FROM categorias inner join categoria_simple on categorias.idcategoria = categoria_simple.id_categorias inner join sub_categoria on categoria_simple.id_categoriasimple = sub_categoria.id_categoria_simple where categoria_simple.nombre_categoriasimple LIKE '%" + txtBuscarEn.Text + "%'; ");
                }
                else
                {
                    dgbCategoria.DataSource = cargarBusquedaCategoria.RellenarTabla1("SELECT categorias.nombre as 'Categoria', categoria_simple.nombre_categoriasimple 'CategoriaSimple', sub_categoria.nombre_categoria as 'SubCategoria', sub_categoria.id_subcategoria as 'IDSubCategoria' FROM categorias inner join categoria_simple on categorias.idcategoria = categoria_simple.id_categorias inner join sub_categoria on categoria_simple.id_categoriasimple = sub_categoria.id_categoria_simple where sub_categoria.nombre_categoria LIKE '%" + txtBuscarEn.Text + "%'; ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar filtrar los resultados de la busqueda ERROR: "+ex.Message+"","Error busqueda",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarBusquedaCategoria.CerrarConexionBD1();
            } 
        }

        private void dgbCategoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de la sucursal
                DataGridViewRow fila = dgbCategoria.Rows[e.RowIndex];
                String IDSubCategoria = Convert.ToString(fila.Cells["IDSubCategoria"].Value);
                String NombreSubCategoria = Convert.ToString(fila.Cells["SubCategoria"].Value);

                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                Productos f1 = Application.OpenForms.OfType<Productos>().SingleOrDefault();
                f1.lblIDCategoriaSeleccionada.Text = IDSubCategoria;
                f1.lblNombreSubCategoria.Text = NombreSubCategoria;
                f1.cbCategoriaSeleccionada.Checked = true;

                //Se cierra el formulario
                this.Close();
            }
        }
    }
}
