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
    public partial class ActualizarPrecioProductoBuscarTienda : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        public ActualizarPrecioProductoBuscarTienda()
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

        private void ActualizarPrecioProductoBuscarTienda_Load(object sender, EventArgs e)
        {
            ComandosBDMySQL CargarTiendas = new ComandosBDMySQL();
            try
            {
                CargarTiendas.AbrirConexionBD1();
                dgbTienda.DataSource = CargarTiendas.RellenarTabla1("SELECT * FROM sbepa.vista_productos_buscarcategoria;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar cargar las tiendas del sistema ERROR: "+ex.Message+"","Error Tiendas",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                CargarTiendas.CerrarConexionBD1();
            }
        }

        private void dgbTienda_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgbTienda.Rows[e.RowIndex];
                String IDTIenda = Convert.ToString(fila.Cells["IDTienda"].Value);
                String NombreTienda = Convert.ToString(fila.Cells["NombreTienda"].Value);

                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                ActualizarPrecioProducto f1 = Application.OpenForms.OfType<ActualizarPrecioProducto>().SingleOrDefault();
                f1.txtIDTienda.Text = IDTIenda;
                f1.txtListaNombreTienda.Text = NombreTienda;

                //Se cierra el formulario
                this.Close();
            }
        }
    }
}
