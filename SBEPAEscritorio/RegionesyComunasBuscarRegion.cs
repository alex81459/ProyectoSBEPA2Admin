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
    public partial class RegionesyComunasBuscarRegion : Form
    {

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public RegionesyComunasBuscarRegion()
        {
            InitializeComponent();
            CargarRegiones();
            cmbBuscarEn.Text = "IdRegion";
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            //Se filtran los resultados de categorias
            ComandosBDMySQL cargarBusqueda = new ComandosBDMySQL();
            try
            {
                cargarBusqueda.AbrirConexionBD1();
                dgbSucursalesBuscar.DataSource = cargarBusqueda.RellenarTabla1("call sbepa2.BuscarRegiones('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', 0, 9999999);");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar filtrar los resultados de la busqueda ERROR: " + ex.Message + "", "Error busqueda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarBusqueda.CerrarConexionBD1();
            }
        }

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Se minimiza el form
            this.WindowState = FormWindowState.Minimized;
        }

        public void CargarRegiones()
        {
            ComandosBDMySQL cargarRegiones = new ComandosBDMySQL();
            try
            {
                cargarRegiones.AbrirConexionBD1();
                dgbSucursalesBuscar.DataSource = cargarRegiones.RellenarTabla1("SELECT * FROM sbepa2.regiones order by idRegion desc;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Extraer la Regiones almacenadas en el sistema ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarRegiones.CerrarConexionBD1();
            }
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

        private void Barra_MouseUp(object sender, MouseEventArgs e)
        {
            //Si el se deja de dar click a la Barra, se deja de mover el Form
            mover = false;
        }

        private void dgbSucursalesBuscar_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de la sucursal
                DataGridViewRow fila = dgbSucursalesBuscar.Rows[e.RowIndex];
                String IDRegion = Convert.ToString(fila.Cells["idRegion"].Value);
                String NombreRegion = Convert.ToString(fila.Cells["NombreRegion"].Value);

                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                RegionesyComunas f1 = Application.OpenForms.OfType<RegionesyComunas>().SingleOrDefault();
                f1.txtIDRegion.Text = IDRegion;
                f1.txtNombreRegion.Text = NombreRegion;
                f1.cbNuevaRegion.Checked = false;

                //Se cierra el formulario
                this.Close();
            }
        }
    }
}
