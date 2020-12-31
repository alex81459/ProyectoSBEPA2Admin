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
    public partial class ProductosFavoritos : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        private String BuscarEn = "";
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public ProductosFavoritos()
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEn.Text == "")
            {
                MessageBox.Show("Debe ingresar algun dato a buscar en el campo 'Paremetros a Buscar'", "Faltan Datos para la Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ComandosBDMySQL BuscarUsuario = new ComandosBDMySQL();
                try
                {
                    BuscarUsuario.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarUsuario.RellenarTabla1("SELECT COUNT(id_usuario) as 'Cantidad' FROM sbepa.usuarios Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%' ORDER BY id_usuario DESC;").Rows[0]["Cantidad"].ToString());
                    dgbUsuarios.DataSource = BuscarUsuario.RellenarTabla1("SELECT id_usuario as 'ID', nombre_usuario as 'Usuario', estado as 'Estado', correo as 'Correo Electronico', ciudad_actual as 'Ciudad', rut as 'Rut', nombre as 'Nombre', apellido as 'Apeliido' FROM sbepa.usuarios  Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%' ORDER BY id_usuario;");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar obtener los usuarios buscados ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarUsuario.CerrarConexionBD1();
                }
            }
        }


        private void CargarUsuarios()
        {
            ComandosBDMySQL CargarUsuarios = new ComandosBDMySQL();
            try
            {
                CargarUsuarios.AbrirConexionBD1();
                dgbUsuarios.DataSource = CargarUsuarios.RellenarTabla1("SELECT * FROM sbepa.vista_usuarios;");
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

        private void ActivarBusqueda()
        {
            label7.Visible = true;
            txtBuscarEn.Visible = true;
            btnBuscar.Visible = true;
        }

        private void cmbBuscarEn_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBuscarEn.Text == "ID")
            {
                BuscarEn = "id_usuario";
                ActivarBusqueda();
            }
            else if (cmbBuscarEn.Text == "Usuario")
            {
                BuscarEn = "nombre_usuario";
                ActivarBusqueda();
            }
            else if (cmbBuscarEn.Text == "Rut")
            {
                BuscarEn = "rut";
                ActivarBusqueda();
            }
            else if (cmbBuscarEn.Text == "Estado")
            {
                BuscarEn = "estado";
                ActivarBusqueda();
            }
            else if (cmbBuscarEn.Text == "Correo Electronico")
            {
                BuscarEn = "correo";
                ActivarBusqueda();
            }
            else if (cmbBuscarEn.Text == "Ciudad")
            {
                BuscarEn = "ciudad_actual";
                ActivarBusqueda();
            }
            else if (cmbBuscarEn.Text == "Nombre")
            {
                BuscarEn = "nombre";
                ActivarBusqueda();
            }
            else
            {
                BuscarEn = "apellido";
                ActivarBusqueda();
            }
        }

        private void ProductosFavoritos_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
            HacerInvisiblesyLimpiarCampos();
        }

        private void HacerInvisiblesyLimpiarCampos()
        {
            label7.Visible = false;
            txtBuscarEn.Visible = false;
            txtBuscarEn.Text = "";
        }

        private void dgbUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbUsuarios.Rows[e.RowIndex];
                String IDUsuario = Convert.ToString(fila.Cells["ID"].Value);

                ComandosBDMySQL cargarFavoritosUsuario = new ComandosBDMySQL();
                try
                {
                    cargarFavoritosUsuario.AbrirConexionBD1();
                    dgbProductosFavoritos.DataSource = cargarFavoritosUsuario.RellenarTabla1("call sbepa.buscar_productos_favoritos(" + IDUsuario + ");");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar la tabla con los porductos favoritos del usuario seleccionado", "Error cargar favoritos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    cargarFavoritosUsuario.CerrarConexionBD1();
                }
            }
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }
    }
}
