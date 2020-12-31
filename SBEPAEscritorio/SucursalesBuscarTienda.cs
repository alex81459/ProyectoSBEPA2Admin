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
    public partial class SucursalesBuscarTienda : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        public SucursalesBuscarTienda()
        {
            InitializeComponent();
            cmbBuscarEn.Text = "Idtienda";
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            //se crea la instancia para buscar en la tabla, se carga el resultado en el datagridview, y siempre se cierra la conexion 
            ComandosBDMySQL buscarTabla = new ComandosBDMySQL();
            try
            {
                buscarTabla.AbrirConexionBD1();
                dgbTiendas.DataSource = buscarTabla.RellenarTabla1("call sbepa2.BuscarTienda('"+ cmbBuscarEn.Text+ "', '"+ txtBuscarEn.Text+ "', 0, 5000);");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar Buscar con los parametros ingresados ERROR: "+ex.Message+"", "Error busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buscarTabla.CerrarConexionBD1();
            }
        }

        private void SucursalesBuscarTienda_Load(object sender, EventArgs e)
        {
            CargarTiendas();
        }

        public void CargarTiendas()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarTiendas = new ComandosBDMySQL();
            try
            {
                cargarTiendas.AbrirConexionBD1();
                dgbTiendas.DataSource = cargarTiendas.RellenarTabla1("SELECT * FROM sbepa2.vistatiendas;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la tabla de Tiendas ERROR: "+ex.Message+"", "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarTiendas.CerrarConexionBD1();
            }
        }

        private void dgbTiendas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de la sucursal
                DataGridViewRow fila = dgbTiendas.Rows[e.RowIndex];
                String IDTienda = Convert.ToString(fila.Cells["Idtienda"].Value);
                String NombreTienda = Convert.ToString(fila.Cells["nombre"].Value);
                
                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                Sucursales f1 = Application.OpenForms.OfType<Sucursales>().SingleOrDefault();
                f1.txtTienda.Text = NombreTienda;
                f1.txtIDTienda.Text = IDTienda;

                //Se cierra el formulario
                this.Close();
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Se minimiza el form
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }
    }
}
