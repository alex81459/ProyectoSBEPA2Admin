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
    public partial class CategoriasBuscarCategoriaSimple : Form
    {
        public CategoriasBuscarCategoriaSimple()
        {
            InitializeComponent();
            cmbBuscarEn.Text = "idCategoriaSimple";
            CargarCategorias();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CargarCategorias()
        {
            ComandosBDMySQL cargarCategorias = new ComandosBDMySQL();
            try
            {
                cargarCategorias.AbrirConexionBD1();
                dgbCategorias.DataSource = cargarCategorias.RellenarTabla1("SELECT idCategoriaSimple,nombre FROM sbepa2.categoriasimple;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar cargar la tabla Categorias Simples ERROR: " + ex.Message + "", "Error al cargar tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarCategorias.CerrarConexionBD1();
            }
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            //se crea la instancia para buscar en la tabla, se carga el resultado en el datagridview, y siempre se cierra la conexion 
            ComandosBDMySQL buscarTabla = new ComandosBDMySQL();
            try
            {
                buscarTabla.AbrirConexionBD1();
                dgbCategorias.DataSource = buscarTabla.RellenarTabla1("SELECT idCategoriaSimple,nombre FROM sbepa2.categoriasimple; WHERE " + cmbBuscarEn.Text + " LIKE '%" + txtBuscarEn.Text + "%';");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar Buscar con los parametros ingresados ERROR: " + ex.Message + "", "Error busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buscarTabla.CerrarConexionBD1();
            }
        }

        private void dgbCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de la categoria
                DataGridViewRow fila = dgbCategorias.Rows[e.RowIndex];
                String IDCategoria = Convert.ToString(fila.Cells["idCategoriaSimple"].Value);
                String NombreTienda = Convert.ToString(fila.Cells["nombre"].Value);

                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                Categorias f1 = Application.OpenForms.OfType<Categorias>().SingleOrDefault();
                f1.txtIDCategoriaSimple.Text = IDCategoria;
                f1.txtNombreCategoriaSimple.Text = NombreTienda;

                //Se cierra el formulario
                this.Close();
            }
        }
    }
}
