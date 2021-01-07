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
    public partial class EstadisticasBuscarUsuarioyTienda : Form
    {
        public EstadisticasBuscarUsuarioyTienda()
        {
            InitializeComponent();
            CargarUsuariosYComunas();
            cmbBuscarEn.Text = "Id_usuario";
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

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();


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
                dgbUsuariosYTiendas.DataSource = cargarBusqueda.RellenarTabla1("call sbepa2.BuscarUsuarioTienda('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', 0, 9999999);");
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

        public void CargarUsuariosYComunas()
        {
            ComandosBDMySQL cargarUsuariosyComunas = new ComandosBDMySQL();
            try
            {
                cargarUsuariosyComunas.AbrirConexionBD1();
                dgbUsuariosYTiendas.DataSource = cargarUsuariosyComunas.RellenarTabla1("SELECT Id_usuario,RutUsuario,Nombres,Apellidos,Idtienda,nombre FROM sbepa2.usuarios inner join tienda on usuarios.Id_usuario = tienda.IdUsuario order by Id_usuario desc;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Extraer los Usuarios almacenados y su Tienda en el sistema ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarUsuariosyComunas.CerrarConexionBD1();
            }
        }

        private void dgbSucursalesBuscar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de la sucursal
                DataGridViewRow fila = dgbUsuariosYTiendas.Rows[e.RowIndex];
                String IDUsuario = Convert.ToString(fila.Cells["Id_usuario"].Value);
                String RUTUsuario = Convert.ToString(fila.Cells["RutUsuario"].Value);
                String IDTienda = Convert.ToString(fila.Cells["Idtienda"].Value);


                //Se crea una instancia especial para enviar los datos entre los 2 forms 
                Estadisticas f1 = Application.OpenForms.OfType<Estadisticas>().SingleOrDefault();
                f1.txtIDUsuario.Text = IDUsuario;
                f1.txtRUTUsuario.Text = RUTUsuario;
                f1.txtIDTienda.Text = IDTienda;
                f1.gbEstadisticasVisitasSucursales.Enabled = true;
                f1.gbEstadisticasVisitasTiendas.Enabled = true;
                f1.gbEstadisitcasOtras.Enabled = true;
                f1.CargarInfoComponentes();
                f1.gbTopProductos.Enabled = true;

                //Se extraen las sucursales para cargarse
                ComandosBDMySQL CargarSucursales = new ComandosBDMySQL();
                try
                {
                    CargarSucursales.AbrirConexionBD1();
                    DataTable Sucursales = new DataTable();
                    Sucursales = CargarSucursales.RellenarTabla1("SELECT idSucursales, Direccion FROM sbepa2.tienda inner join sucursales on tienda.idTienda = sucursales.idTienda where tienda.idTienda = " + IDTienda + ";");

                    //Se limpia el comboBOX
                    f1.cbSucursalSeleccionada.DataSource = null;
                    f1.cbSucursalSeleccionada.Items.Clear();

                    //Se recorre el datatable de sucursales
                    for (int i = 0; i < Sucursales.Rows.Count; i++)
                    {
                        f1.cbSucursalSeleccionada.Items.Add(Sucursales.Rows[i]["Direccion"].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar las Sucursales de la Tienda Seleccionada","Error Cargar Datos",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                finally
                {
                    CargarSucursales.CerrarConexionBD1();
                }


                //Se cargan los componentes de las estadisticas
                f1.CargarInfoComponentes();
                //Se cierra el formulario
                this.Close();
            }
        }
    }
}
