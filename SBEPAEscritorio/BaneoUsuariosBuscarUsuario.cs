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
                    dgbUsuarios.DataSource = BuscarUsuario.RellenarTabla1("SELECT id_usuario as 'ID', nombre_usuario as 'Usuario', estado as 'Estado', correo as 'Correo Electronico', ciudad_actual as 'Ciudad', rut as 'Rut', nombre as 'Nombre', apellido as 'Apeliido' FROM sbepa.usuarios  Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%' ORDER BY id_usuario DESC;");
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

        private void cmbBuscarEn_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBuscarEn.Text == "ID")
            {
                BuscarEn = "id_usuario";
            }
            else if (cmbBuscarEn.Text == "Usuario")
            {
                BuscarEn = "nombre_usuario";
            }
            else if (cmbBuscarEn.Text == "Rut")
            {
                BuscarEn = "rut";
            }
            else if (cmbBuscarEn.Text == "Estado")
            {
                BuscarEn = "estado";
            }
            else if (cmbBuscarEn.Text == "Correo Electronico")
            {
                BuscarEn = "correo";
            }
            else if (cmbBuscarEn.Text == "Ciudad")
            {
                BuscarEn = "ciudad_actual";
            }
            else if (cmbBuscarEn.Text == "Nombre")
            {
                BuscarEn = "nombre";
            }
            else
            {
                BuscarEn = "apellido";
            }
        }

        private void pbActualizar_Click(object sender, EventArgs e)
        {
            CargarTabla();
            cmbBuscarEn.Text = "Usuario";
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

        private void dgbUsuarios_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de la sucursal
                DataGridViewRow fila = dgbUsuarios.Rows[e.RowIndex];
                String IDUsuario = Convert.ToString(fila.Cells["ID"].Value);
                String NombreUsuario = Convert.ToString(fila.Cells["Usuario"].Value);

                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                BaneoUsuarios f1 = Application.OpenForms.OfType<BaneoUsuarios>().SingleOrDefault();
                f1.lblIDUsuario.Text = IDUsuario;
                f1.lblNombreUsuario.Text = NombreUsuario;

                //Se cierra el formulario
                this.Close();
            }
        }
    }
}

