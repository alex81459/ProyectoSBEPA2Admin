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
    public partial class RegionesyComunas : Form
    {
        public RegionesyComunas()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEn.Text == "")
            {
                MessageBox.Show("Debe ingresar algun dato a buscar en el campo 'Parametros a Buscar'", "Faltan Datos para la Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ComandosBDMySQL BuscarRegistros = new ComandosBDMySQL();
                try
                {
                    //Se cargan los datos necesarios para la busqueda y el ordenamiento de las paginas
                    BuscarRegistros.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarRegistros.RellenarTabla1("SELECT COUNT(idComuna) FROM sbepa2.comunas inner join sbepa2.regiones on comunas.idRegion = regiones.idRegion Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';").Rows[0][0].ToString());
                    txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbComunasRegiones.DataSource = BuscarRegistros.RellenarTabla2("call sbepa2.BuscarComunasRegiones('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
                    lblRegistrosEncontrados.Visible = true;
                    txtRegistrosEncontradosSuperior.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar buscar las comunas y regiones en el sistema ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarRegistros.CerrarConexionBD1();
                }
            }
        }
        private void ActivarControlpaginas()
        {
            nudPaginaActualBuscar.Visible = true;
            lblBuscarPor.Visible = true;
            txtPaginasDisponiblesBusqueda.Visible = true;
            lblPaginasDisponibles.Visible = true;
            lblPaginaActualBusqueda.Visible = true;
            lblPaginasDisponibles.Visible = true;
        }

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbComunasRegiones.DataSource = SiguientePagina.RellenarTabla1("SELECT idComuna,NombreComuna,regiones.idRegion,NombreRegion FROM sbepa2.comunas inner join sbepa2.regiones on comunas.idRegion = regiones.idRegion ORDER BY comunas.idComuna DESC limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema detectado al intentar cargar los datos de la Comunas-Regiones del sistema ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                SiguientePagina.CerrarConexionBD1();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarComunasRegiones();
            HacerInvisiblesyLimpiarCampos();
        }

        private void CargarComunasRegiones()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarRegistros = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarRegistros.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarRegistros.RellenarTabla1("SELECT COUNT(idComuna) FROM sbepa2.comunas inner join sbepa2.regiones on comunas.idRegion = regiones.idRegion ").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                cargarRegistros.AbrirConexionBD1();
                dgbComunasRegiones.DataSource = cargarRegistros.RellenarTabla1("SELECT * FROM sbepa2.vistacomunasregiones;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la tabla de los cambios de producto del sistema ERROR: " + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarRegistros.CerrarConexionBD1();
            }
        }

        private void HacerInvisiblesyLimpiarCampos()
        {
            txtBuscarEn.Visible = false;
            nudPaginaActualBuscar.Visible = false;
            txtPaginasDisponiblesBusqueda.Visible = false;
            txtBuscarEn.Text = "";
            nudPaginaActualBuscar.Value = 0;
            txtPaginasDisponiblesBusqueda.Text = "?????????";
            txtPaginasDisponiblesBusqueda.Visible = false;
            lblRegistrosEncontrados.Visible = false;
            txtBuscarEn.Visible = true;
            lnlParametrosABuscar.Visible = true;
            lblPaginaActualBusqueda.Visible = false;
            lblPaginasDisponibles.Visible = false;
            lblRegistrosEncontrados.Visible = false;
            txtRegistrosEncontradosSuperior.Visible = false;
        }

        private void cmbBuscarEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBusqueda();
        }

        private void ActivarBusqueda()
        {
            txtBuscarEn.Visible = true;
            btnBuscar.Visible = true;
            picLupa.Visible = true;
        }

        private void RegionesyComunas_Load(object sender, EventArgs e)
        {
            CargarComunasRegiones();
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void txtNombreComuna_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtNombreComunca_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        private void dgbComunasRegiones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgbComunasRegiones.Rows[e.RowIndex];
            // Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                Limpiar();

                //Se extraen los datos de el DataGridView
                txtIDComuna.Text = Convert.ToString(fila.Cells["idComuna"].Value);
                txtIDComuna.Enabled = true;
                txtNombreComuna.Text = Convert.ToString(fila.Cells["NombreComuna"].Value);
                txtNombreComuna.Enabled = true;
                cbNuevaComuna.Checked = false;
                txtIDRegion.Text = Convert.ToString(fila.Cells["idRegion"].Value);
                txtIDRegion.Enabled = true;
                txtNombreRegion.Text = Convert.ToString(fila.Cells["NombreRegion"].Value);
                txtNombreRegion.Enabled = true;
                cbNuevaRegion.Checked = false;
                btnEliminar.Enabled = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            cbNuevaComuna.Checked = true;
            cbNuevaRegion.Checked = true;
            txtIDComuna.Text = "";
            txtNombreComuna.Text = "";
            txtIDRegion.Text = "";
            txtNombreRegion.Text = "";
            btnBuscarRegion.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
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
    }

}
