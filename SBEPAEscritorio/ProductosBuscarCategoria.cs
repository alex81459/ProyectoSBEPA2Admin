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
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();
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
            cmbBuscarEn.Text = "Nombre Sub Categoria";
        }

        private void CargarCategorias()
        {
            ComandosBDMySQL cargarCategorias = new ComandosBDMySQL();
            try
            {
                cargarCategorias.AbrirConexionBD1();
                dgbCategoria.DataSource = cargarCategorias.RellenarTabla1("select idCategorias, categorias.nombre as 'NombreCategoria', categoriasimple.idCategoriaSimple, categoriasimple.Nombre as 'NombreCategoriaSimple',subcategoria.idSubCategoria, subcategoria.nombre as 'NombreSubCategoria' from categorias inner join CategoriaSimple on categorias.idCategorias = categoriasimple.id_categorias inner join subcategoria on categoriasimple.idCategoriaSimple = subcategoria.idCategoriaSimple ORDER BY subcategoria.idSubCategoria DESC;");
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
                dgbCategoria.DataSource = cargarBusquedaCategoria.RellenarTabla1("call sbepa2.BuscarCategoriasTodas('"+ cmbBuscarEn.Text+ "', '"+ txtBuscarEn.Text+ "', 0, 9999999);");
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
                String NombreSubCategoria = Convert.ToString(fila.Cells["NombreSubCategoria"].Value);

                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                Productos f1 = Application.OpenForms.OfType<Productos>().SingleOrDefault();
                f1.txtIDCategoriaSeleccionada.Text = IDSubCategoria;
                f1.txtNombreSubCategoria.Text = NombreSubCategoria;

                //Se cierra el formulario
                this.Close();
            }
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void Barra_MouseDown(object sender, MouseEventArgs e)
        {
            //Si puntero del maus esta sobre la barra y se da click continuado, se cambia la posicion y se activa mover
            posicion = new Point(e.X, e.Y);
            mover = true;
        }

        private void Barra_MouseMove(object sender, MouseEventArgs e)
        {
            //Si mover esta activado, se cambia la posicion del Form
            if (mover)
            {
                Location = new Point((this.Left + e.X - posicion.X), (this.Top + e.Y - posicion.Y));
            }
        }

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private void Barra_MouseUp(object sender, MouseEventArgs e)
        {
            //Si el se deja de dar click a la Barra, se deja de mover el Form
            mover = false;
        }
    }
}
