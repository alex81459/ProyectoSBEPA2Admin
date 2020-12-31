using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBEPAEscritorio
{
    public partial class Tienda : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public Tienda()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbID, "El ID (Identificador) el cual se usara para identificar las diferentes Tiendas" + System.Environment.NewLine + "este se genera automático cada vez que se hace un registro" + System.Environment.NewLine + "no puede ser cambiado manualmente");
            ttmensaje.SetToolTip(pbNombre, "El Nombre con el Cual se identificara la Tienda de las demás tiendas" + System.Environment.NewLine + "El nombre DEBE SER UNICO");
            ttmensaje.SetToolTip(pbInformacion, "La Información de la Tienda, información relevante o de interés acerca de la historia de la tienda");
            ttmensaje.SetToolTip(pbArchivoLogo, "El Logo de la Tienda, el cual será mostrado daba vez que se seleccione la tienda, el cual que debe ser buscado" + System.Environment.NewLine + "y recortado por el editor de imágenes del sistema SBEPA");
            ttmensaje.SetToolTip(pbIDUsuario, "El ID del usuario al cual pertence esta Tienda");
            ttmensaje.SetToolTip(pbCantidadVisitas, "La cantidad de visitas que ha tenido la informacion de la tienda");
        }

        private DataTable TablaTienda = new DataTable();

        private void CargarTiendas()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarTiendas = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarTiendas.AbrirConexionBD1();
                txtCantidadRegistro.Text = (cargarTiendas.RellenarTabla1("SELECT COUNT(idtienda) FROM sbepa2.tienda;").Rows[0][0].ToString());
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                dgbTiendas.DataSource = cargarTiendas.RellenarTabla1("SELECT * FROM sbepa2.vistatiendas;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar Las Tiendas ERORR: " + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarTiendas.CerrarConexionBD1();
            }
        }

        private void btnBuscarLogo_Click(object sender, EventArgs e)
        {
            //Se crea la instancia y se abre el editor de imagenes
            EditorImagen abrirEditorImagen = new EditorImagen();
            if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
            {
                //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                pbLogo.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                cbCambioLogo.Checked = true;
            }
        }

        private void ActivarBusqueda()
        {
            label7.Visible = true;
            txtBuscarEn.Visible = true;
            btnBuscar.Visible = true;
            picLupa.Visible = true;
        }

        private void Tienda_Load(object sender, EventArgs e)
        {
            //Al cargarse el formulario se llama al metodo CargarTiendas() y se se establece el combobox buscar 
            CargarTiendas();
            cmbBuscarEn.Text = "Idtienda";
        }

        private void ActivarNuevo()
        {
            //Se establecen los parametros para registrar una nueva Tienda
            txtIDTienda.Text = "";
            txtNombre.Text = "";
            txtInformacion.Text = "";
            pbLogo.Image = null;
            btnBuscarLogo.Visible = true;
            btnBuscarLogo.Text = "Buscar Logo...";
            txtxEliminarTienda.Visible = false;
            pbBorrar.Visible = false;
            txtIDUsuario.Text = "";
            txtCantidadVisitas.Text = "";
            btnBuscarUsuario.Enabled = true;
        }

        private void ActivarEditar()
        {
            //Se establecen los parametros para Editar una Tienda
            txtIDTienda.Text = "";
            txtNombre.Text = "";
            txtInformacion.Text = "";
            pbLogo.Image = null;
            btnBuscarLogo.Visible = true;
            btnBuscarLogo.Text = "Remplazar Logo";
            txtxEliminarTienda.Visible = true;
            pbBorrar.Visible = true;
            pbBorrar.Visible = true;
            btnBuscarUsuario.Enabled = false;
        }

        private void dgbTiendas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se activa la capacidad de modificar los datos
                ActivarEditar();
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbTiendas.Rows[e.RowIndex];
                txtIDTienda.Text = Convert.ToString(fila.Cells["Idtienda"].Value);
                txtIDUsuario.Text = Convert.ToString(fila.Cells["IdUsuario"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["nombre"].Value);
                txtInformacion.Text = Convert.ToString(fila.Cells["informacion"].Value);
                txtCantidadVisitas.Text = Convert.ToString(fila.Cells["CantidadVisita"].Value);

                cbCambioLogo.Visible = true;
                cbCambioLogo.Checked = false;

                //Se extrae la imagen
                ComandosBDMySQL ExtraerImagenLogo = new ComandosBDMySQL();
                try
                {
                    ExtraerImagenLogo.AbrirConexionBD1();
                    DataTable VerificarLogo = new DataTable();
                    VerificarLogo = ExtraerImagenLogo.RellenarTabla1("SELECT Archivo_logo FROM sbepa2.tienda where Idtienda = '" + txtIDTienda.Text + "';");

                    if (VerificarLogo.Rows[0][0].ToString() != "")
                    {
                        //Si el logo de la tienda esta guardado
                        pbLogo.Image = ExtraerImagenLogo.ExtraerImagen("SELECT Archivo_logo FROM sbepa2.tienda where Idtienda = " + txtIDTienda.Text + "");
                    }
                    else
                    {
                        //Si el logo de la tienda no esta guardado
                        MessageBox.Show("La Tienda Seleccionada con el nombre " + txtNombre.Text + " NO TIENE UN LOGO ALMACENADO", "Logo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ExtraerImagenLogo.CerrarConexionBD1();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se puedo Extraer el Logo de la Tienda ERROR:" + ex.Message, "Error Cargar Logo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    ExtraerImagenLogo.CerrarConexionBD1();
                }
            }
        }

        private Boolean VerificarDatosTienda()
        {
            //Se verifica que los campos requeridos tengan contenido
            if (txtNombre.Text != "" && txtInformacion.Text != "" && pbLogo.Image != null && txtIDUsuario.Text != "")
            {
                return true;
                //si tienen contenido se devuelve verdadero
            }
            else
            {
                //Si a uno de los campos le falta contenido se muestra una advertencia
                MessageBox.Show("La Tienda nueva a registrar debe tener un Nombre, una Informacio, Una Fotografia de su Logo y un Usuario asignado, verifique que estas caracteristicas estan ingresadas", "Error Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void btnNuevaTienda_Click(object sender, EventArgs e)
        {
            //Se activa la capacidad de registrar una neuva tienda
            ActivarNuevo();
        }

        private void txtInformacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void btnGuardarSupermercado_Click(object sender, EventArgs e)
        {
            //Se llama a la funcion para verificar los datos a registrar, si son correctos se continua
            if (VerificarDatosTienda() == true)
            {
                if (txtIDTienda.Text == "")
                {
                    //Si el ID esta vacio se registra una nueva tienda
                    //Se verifica que la tienda ya no este registrada
                    ComandosBDMySQL verificarExistenciaTienda = new ComandosBDMySQL();
                    try
                    {
                        verificarExistenciaTienda.AbrirConexionBD1();
                        Boolean TiendaRegistrada = verificarExistenciaTienda.VerificarExistenciaDato1("call sbepa2.VerificarExitenciaTienda('" + txtNombre.Text + "');");

                        if (TiendaRegistrada == false)
                        {
                            //si la tienda no esta registrada se continua con el registro
                            ComandosBDMySQL registrarTienda = new ComandosBDMySQL();
                            try
                            {
                                //Si todo el proceso de registro se ejecuta correctamente se carga los nuevos registros de las tiendas
                                registrarTienda.AbrirConexionBD1();
                                //Se registra la tienda y su logo
                                registrarTienda.IngresarConsulta1("call sbepa2.InsertarTienda(" + txtIDUsuario.Text + ", '" + txtNombre.Text + "', '" + txtInformacion.Text + "');");
                                //Se obtiene el ID de la tienda recien registrada
                                String IDTiendaRegistra = registrarTienda.RellenarTabla1("SELECT Idtienda FROM sbepa2.tienda where IdUsuario = '" + txtIDUsuario.Text + "' order by Idtienda desc").Rows[0][0].ToString();
                                //Se registra la imagen
                                registrarTienda.IngresarImagen("call sbepa2.ActualizarTiendaArchivoLogo(" + IDTiendaRegistra + ", @imagen);", pbLogo.Image);
                                //Se registra el cambio realizado por el administrador
                                registrarTienda.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Tienda', 'Insertar', 'REGISTRO LA TIENDA: " + txtNombre.Text + ", CON LA INFORMACION: " + txtInformacion.Text + ", AL USUARIO : " + txtIDUsuario.Text + " y SU LOGO');");
                                //Se muestra mensaje de confirmacion
                                MessageBox.Show("La Tienda " + txtNombre.Text + " ha sido correctamente Almacenada", "Guardo Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarTiendas();
                                ActivarNuevo();
                                cbCambioLogo.Checked = false;
                                cbCambioLogo.Visible = false;
                            }
                            catch (Exception ex)
                            {
                                //Se muestra un mensaje de error si llega a ocurrir una excepcion
                                MessageBox.Show("Error al Intentar Registrar Los Datos de la Nueva Tienda ERROR: " + ex.Message, "Error Registrar Tienda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            finally
                            {
                                registrarTienda.CerrarConexionBD1();
                            }
                        }
                        else
                        {
                            //si la tienda esta registrada, se muestra un mensaje
                            MessageBox.Show("La Tienda " + txtNombre.Text + " Ya se encuentra registrada en el Sistema, verifique el Nombre", "Tienda ya Registrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar verificar si la tienda ya se encuentra registrada en el sistema ERROR: " + ex.Message, "Error Verificar Tienda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        verificarExistenciaTienda.CerrarConexionBD1();
                    }
                }
                else
                {
                    //Si el ID no esta vacio se actualiza la Tienda
                    ComandosBDMySQL actualizarTienda = new ComandosBDMySQL();

                    //Se verfica si el nombre de la tienda ya esta registrado por el mismo usuario
                    actualizarTienda.AbrirConexionBD1();
                    Boolean TiendayUsuariosIguales = actualizarTienda.VerificarExistenciaDato1("SELECT nombre FROM sbepa2.tienda where Idtienda = '" + txtIDTienda.Text + "' and nombre = '" + txtNombre.Text + "';");
                    Boolean NombreTiendaRegistrado = actualizarTienda.VerificarExistenciaDato1("SELECT nombre FROM sbepa2.tienda where (nombre = '" + txtNombre.Text + "');");

                    //si el nombre de la tienda y el id de la tienda registrados son los mismos, se puede actualizar los 
                    //demas datos O si el nombre de tienda se encuentra disponible tambien se pueden actualizar
                    if (TiendayUsuariosIguales == true || NombreTiendaRegistrado == false)
                    {
                        try
                        {
                            //Se incia el proceso para actualizar los datos de la tienda
                            actualizarTienda.AbrirConexionBD1();
                            if (cbCambioLogo.Checked == true)
                            {
                                //Se registran los cambios de la tienda
                                actualizarTienda.IngresarConsulta1("call sbepa2.ActualizarTienda(" + txtIDTienda.Text + ", '" + txtNombre.Text + "', '" + txtInformacion.Text + "');");
                                //Se ingresa la nueva imagen
                                actualizarTienda.IngresarImagen("call sbepa2.ActualizarTiendaArchivoLogo(" + txtIDTienda.Text + ", @imagen);", pbLogo.Image);
                                //Se guarda el registro
                                actualizarTienda.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Tienda', 'Actualizar', 'MODIFICO LOS DATOS DE LA TIENDA CON EL ID: " + txtIDTienda.Text + " , ACTUALIZO EL NOMBRE DE LA TIENDA A: " + txtNombre.Text + ", Y ACTUALIZO LA INFORMACION DE LA TIENDA A: " + txtInformacion.Text + ", SE CAMBIO EL LOGO DE LA TIENDA');");
                            }
                            else
                            {
                                //Se guardan los cambios de la tienda
                                actualizarTienda.IngresarConsulta1("call sbepa2.ActualizarTienda(" + txtIDTienda.Text + ", '" + txtNombre.Text + "', '" + txtInformacion.Text + "');");
                                //Se guarda el registro
                                actualizarTienda.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Tienda', 'Actualizar', 'MODIFICO LOS DATOS DE LA TIENDA CON EL ID: " + txtIDTienda.Text + " , ACTUALIZO EL NOMBRE DE LA TIENDA A: " + txtNombre.Text + ", Y ACTUALIZO LA INFORMACION DE LA TIENDA A: " + txtInformacion.Text + ", NO SE CAMBIO EL LOGO DE LA TIENDA');");
                            }

                            MessageBox.Show("La Tienda " + txtNombre.Text + " ha sido correctamente Modificada", "Modificacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarTiendas();
                            ActivarNuevo();
                            cbCambioLogo.Checked = false;
                            cbCambioLogo.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            //Se muestra un mensaje de error si llega a ocurrir una excepcion
                            MessageBox.Show("Error al Intentar Actualiza Los Datos de la Tienda ERROR: " + ex.Message, "Error Actualizar Tienda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            actualizarTienda.CerrarConexionBD1();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El Nombre de la tienda ya se encuentra registrado intente con otro", "Nombre ya registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
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
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbTiendas.DataSource = SiguientePagina.RellenarTabla1("SELECT * FROM sbepa2.tienda limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema detectado al intentar cargar los datos de la siguiente pagina ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                SiguientePagina.CerrarConexionBD1();
            }
        }

        private void ActivarControlpaginas()
        {
            label5.Visible = true;
            nudPaginaActualBuscar.Visible = true;
            label9.Visible = true;
            txtPaginasDisponiblesBusqueda.Visible = true;
            label11.Visible = true;
            label6.Visible = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEn.Text == "")
            {
                MessageBox.Show("Debe ingresar algun dato a buscar en el campo 'Paremetros a Buscar'", "Faltan Datos para la Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ComandosBDMySQL BuscarTiendas = new ComandosBDMySQL();
                try
                {
                    //Se cargan los datos necesarios para la busquedam y el ordenamiento de las paginas
                    BuscarTiendas.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarTiendas.RellenarTabla1("SELECT COUNT(Idtienda) FROM sbepa2.tienda Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%'").Rows[0][0].ToString());
                    txtTiendasEncontrados.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbTiendas.DataSource = BuscarTiendas.RellenarTabla2("call sbepa2.BuscarTienda('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
                    label19.Visible = true;
                    txtTiendasEncontrados.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar obtener las tiendas buscadas ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarTiendas.CerrarConexionBD1();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTiendas();
            HacerInvisiblesyLimpiarCampos();
        }

        private void HacerInvisiblesyLimpiarCampos()
        {
            label7.Visible = false;
            txtBuscarEn.Visible = false;
            label5.Visible = false;
            nudPaginaActualBuscar.Visible = false;
            label9.Visible = false;
            txtPaginasDisponiblesBusqueda.Visible = false;
            txtBuscarEn.Text = "";
            nudPaginaActualBuscar.Value = 0;
            txtTiendasEncontrados.Text = "?????????";
            txtTiendasEncontrados.Visible = false;
            label19.Visible = false;
            txtBuscarEn.Visible = true;
            label8.Visible = true;
            label6.Visible = false;
            label11.Visible = false;
            label19.Visible = false;
        }

        private void txtxEliminarTienda_Click(object sender, EventArgs e)
        {
            if (txtIDTienda.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("ADVERTENCIA la tienda al ser eliminado no solo se borrara del sistema, sino tambien las sucursales, productos y toda informacion conectada a ella, ¿Desea Eliminar la Tienda Seleccionada?", "Tienda Borrada", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Si contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    //si la tienda no tiene registros se puede borrar
                    ClaveMaestra verificarEliminarClave = new ClaveMaestra();
                    ComandosBDMySQL EliminarTienda = new ComandosBDMySQL();
                    try
                    {
                        //Se verifica la clave maestra
                        if (verificarEliminarClave.ShowDialog() == DialogResult.OK)
                        {
                            //Si la clave es correcta se procede a eliminar la tienda del sistema
                            EliminarTienda.AbrirConexionBD1();
                            //Se elimina
                            EliminarTienda.IngresarConsulta1("call sbepa2.EliminarTienda(" + txtIDTienda.Text + ");");
                            //Se guarda el registro
                            EliminarTienda.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Tienda', 'Eliminar', 'ELIMINO LA TIENDA CON EL ID DE REGISTRO: " + txtIDTienda.Text + " LA CUAL TENIA POR NOMBRE: " + txtNombre.Text + "');");
                            MessageBox.Show("Tienda Eliminada Correctamente", "Proceso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ActivarNuevo();
                            CargarTiendas();
                        }
                        else
                        {
                            //Se muestra un mensaje para que el usuario ingrese la clave
                            MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Intentar Eliminar a la Tienda del Sistema ERROR:" + ex.Message, "Error Borrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        EliminarTienda.CerrarConexionBD1();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Tienda para Eliminar", "No hay Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            //Se abre el form para buscar el id del usuarios
            TiendaBuscarUsuario abrirBuscar = new TiendaBuscarUsuario();
            abrirBuscar.ShowDialog();
        }

        private void cmbBuscarEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBusqueda();
        }
    }
}
