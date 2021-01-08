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
    public partial class BaneoUsuariosBuscarUsuario : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        String BuscarEn = "";

        public BaneoUsuariosBuscarUsuario()
        {
            InitializeComponent();
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


        private void pbActualizar_Click(object sender, EventArgs e)
        {
            CargarTabla();
            cmbBuscarEn.Text = "";
            txtBuscarEn.Text = "";
        }

        private void BaneoUsuariosBuscarUsuario_Load(object sender, EventArgs e)
        {
            CargarTabla();
            cmbBuscarEn.Text = "Usuario";
        }

        private void CargarTabla()
        {
            ComandosBDMySQL CargarUsuarios = new ComandosBDMySQL();
            try
            {
                CargarUsuarios.AbrirConexionBD1();
                dgbUsuarios.DataSource = CargarUsuarios.RellenarTabla1("SELECT * FROM usuarios order by Id_usuario desc;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar cargar los usuarios ERROR:" + ex.Message + "", "Error Extraer Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                CargarUsuarios.CerrarConexionBD1();
            }
        }

        private void dgbUsuarios_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de la sucursal
                DataGridViewRow fila = dgbUsuarios.Rows[e.RowIndex];
                String IDUsuario = Convert.ToString(fila.Cells["Id_usuario"].Value);
                String NombresUsuario = Convert.ToString(fila.Cells["Nombres"].Value);
                String ApellidosUsuario = Convert.ToString(fila.Cells["Apellidos"].Value);

                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                BaneoUsuarios f1 = Application.OpenForms.OfType<BaneoUsuarios>().SingleOrDefault();
                f1.txtIDUsuario.Text = IDUsuario;
                f1.txtNombresUsuario.Text = NombresUsuario+" "+ ApellidosUsuario;

                //Se cierra el formulario
                this.Close();
            }
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
                dgbUsuarios.DataSource = cargarBusqueda.RellenarTabla1("call sbepa2.BuscarUsuarios('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', 0, 9999999);");
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

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

    }
}

