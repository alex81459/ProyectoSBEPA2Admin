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
                txtNombreComuna.Text = Convert.ToString(fila.Cells["NombreComuna"].Value);
                txtNombreComuna.Enabled = true;
                cbNuevaComuna.Checked = false;
                txtIDRegion.Text = Convert.ToString(fila.Cells["idRegion"].Value);
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
            btnBuscarRegionRegistrada.Enabled = true;
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

        private Boolean RevisarExitenciaComuna()
        {
            ComandosBDMySQL revisarComuna = new ComandosBDMySQL();
            try
            {
                //Se revisa si la comuna existe
                revisarComuna.AbrirConexionBD1();
                return revisarComuna.VerificarExistenciaDato1("SELECT * FROM sbepa2.comunas where NombreComuna = '" + txtNombreComuna.Text + "';");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al intentar revisar si el nombre de la comuna esta registrado ERROR: " + ex.Message + "", "Error Verificar Comuna", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            finally
            {
                revisarComuna.CerrarConexionBD1();
            }
        }

        private Boolean RevisarExistenciaRegion()
        {
            ComandosBDMySQL revisarRegion = new ComandosBDMySQL();
            try
            {
                //Se revisa si la comuna existe
                revisarRegion.AbrirConexionBD1();
                return revisarRegion.VerificarExistenciaDato1("SELECT * FROM sbepa2.regiones where NombreRegion = '" + txtNombreRegion.Text + "';");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al intentar revisar si el nombre de la region esta registrado ERROR: " + ex.Message + "", "Error Verificar Comuna", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            finally
            {
                revisarRegion.CerrarConexionBD1();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtIDComuna.Text == "" && txtIDRegion.Text == "")
            {
                //Si no hay comuna ni region registrada se revisan su existen en el sistema
                if (RevisarExitenciaComuna() == false && RevisarExistenciaRegion() == false)
                {
                    ComandosBDMySQL RegistrarComunaRegion = new ComandosBDMySQL();
                    try
                    {
                        RegistrarComunaRegion.AbrirConexionBD1();
                        RegistrarComunaRegion.IngresarConsulta1("call sbepa2.InsertarRegion('" + txtNombreRegion.Text + "');");
                        String IDRegionNueva = RegistrarComunaRegion.RellenarTabla1("SELECT idRegion FROM sbepa2.regiones where NombreRegion = '" + txtNombreRegion.Text + "' ORDER BY idRegion DESC limit 1;").Rows[0]["idRegion"].ToString();
                        RegistrarComunaRegion.IngresarConsulta1("call sbepa2.InsertarComuna(" + IDRegionNueva + ", '" + txtNombreComuna.Text + "');");
                        MessageBox.Show("La Nueva Region con el Nombre: " + txtNombreRegion.Text + " y la nueva primera Comuna con el nombre de: " + txtNombreComuna.Text + " Ha sido Registrada Correctamente", "Registro Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RegistrarComunaRegion.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Regiones y Comunas', 'Añadir', 'Añadio la Region: " + txtNombreRegion.Text + " y su primera Comuna: " + txtNombreComuna.Text + "');");
                        Limpiar();
                        CargarComunasRegiones();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar registrar la nueva Comuna y La Nueva Region ERORR: " + ex.Message + "", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        RegistrarComunaRegion.CerrarConexionBD1();
                    }
                }
                else
                {
                    MessageBox.Show("El nombre de la Comuna o Region ya se encuentra registrado en el Sistema, debe de ingresar uno autentico no pueden repetirse, si desea registar una comuna para una region exitente seleccionela con el Boton 'Buscar Region Registrada'", "Nombres Registrados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (txtIDComuna.Text == "" && txtIDRegion.Text != "")
            {
                //Se registra la nueva comuna para la region
                if (RevisarExitenciaComuna() == false)
                {
                    ComandosBDMySQL RegistrarComuna = new ComandosBDMySQL();
                    try
                    {
                        RegistrarComuna.AbrirConexionBD1();
                        RegistrarComuna.IngresarConsulta1("call sbepa2.InsertarComuna(" + txtIDRegion.Text + ", '" + txtNombreComuna.Text + "');");
                        RegistrarComuna.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Regiones y Comunas', 'Añadir', 'En la Region: " + txtNombreRegion.Text + " fue registrada la nueva Comuna: " + txtNombreComuna.Text + "');");
                        MessageBox.Show("En la Region: " + txtNombreRegion.Text + " fue registrada la nueva Comuna: " + txtNombreComuna.Text + " Ha sido Registrada Correctamente", "Registro Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        CargarComunasRegiones();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar registrar la nueva Comuna y La Nueva Region ERORR: " + ex.Message + "", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        RegistrarComuna.CerrarConexionBD1();
                    }
                }
                else
                {
                    MessageBox.Show("El nombre de la comuna ingresada para la region ya se encuentra registrado", "Nombres Registrados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            if (txtIDComuna.Text != "" && txtIDRegion.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea continuar, el nombre de la comuna selecciona y la region sera cambiado por los actuales?", "Cambio Nombres", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Si contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    //Se actualiza el nombre de la comuna y region
                    ComandosBDMySQL ActualizarComunaRegion = new ComandosBDMySQL();
                    try
                    {
                        ActualizarComunaRegion.AbrirConexionBD1();
                        ActualizarComunaRegion.IngresarConsulta1("call sbepa2.ActualizarRegion(" + txtIDRegion.Text + ", '" + txtNombreRegion.Text + "');");
                        ActualizarComunaRegion.IngresarConsulta1("call sbepa2.ActualizarComunas(" + txtIDComuna.Text + ", " + txtIDRegion.Text + ", '" + txtNombreComuna.Text + "');");
                        ActualizarComunaRegion.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Regiones y Comunas', 'Actualizar', 'La Comuna con ID " + txtIDComuna.Text + " a sido actualizado su nombre Por: " + txtNombreComuna.Text + " y el Nombre de la Region con ID " + txtIDRegion.Text + " fue actualizado Por: " + txtNombreRegion.Text + "');");
                        MessageBox.Show("El nombre de la Comuna y la Region han sido Actualizados Correctamente", "Actualizacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        CargarComunasRegiones();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar registrar la nueva Comuna y La Nueva Region ERORR: " + ex.Message + "", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        ActualizarComunaRegion.CerrarConexionBD1();
                    }
                }
            }
        }

        private void btnBuscarRegionRegistrada_Click(object sender, EventArgs e)
        {
            RegionesyComunasBuscarRegion BuscarRegion = new RegionesyComunasBuscarRegion();
            BuscarRegion.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtIDComuna.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que eliminara la Comuna Actual?, al Eliminar la COMUNA ACTUAL TAMBIEN SE ELIMINARAN LOS PRODUCTOS ASIGNADOS A ELLA", "ELIMINACION COMUNA", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Si contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    ClaveMaestra verificarEliminarClave = new ClaveMaestra();
                    ComandosBDMySQL EliminarComuna = new ComandosBDMySQL();
                    try
                    {
                        //Se verifica la clave maestra
                        if (verificarEliminarClave.ShowDialog() == DialogResult.OK)
                        {
                            //Si la clave es correcta se procede a eliminar la tienda del sistema
                            EliminarComuna.AbrirConexionBD1();
                            //Se elimina
                            EliminarComuna.IngresarConsulta1("call sbepa2.EliminarComuna(" + txtIDComuna.Text + ");");
                            //Se guarda el registro
                            EliminarComuna.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Regiones y Comunas', 'Eliminar', 'ELIMINO LA COMUNA CON EL ID:" + txtIDComuna.Text + " CON EL NOMBRE:" + txtNombreComuna.Text + "');");
                            MessageBox.Show("Comuna Eliminada Correctamente", "Proceso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Limpiar();
                            CargarComunasRegiones();
                        }
                        else
                        {
                            //Se muestra un mensaje para que el usuario ingrese la clave
                            MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Intentar Eliminar a la Comuna del Sistema ERROR:" + ex.Message, "Error Borrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        EliminarComuna.CerrarConexionBD1();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar alguna Comuna para ser eliminada", "Falta Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
