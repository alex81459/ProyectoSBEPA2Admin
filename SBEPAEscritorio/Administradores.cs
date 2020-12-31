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
    public partial class Administradores : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public Administradores()
        {
            InitializeComponent();
            //Se Configuran los Mensajes de informacion de los campos por medio del objeto ToolTip
            ttmensaje.SetToolTip(pbID, "El ID (Numero Identificador) del Administrador, este se genera automáticamente cuando" + System.Environment.NewLine + "Se registra un nuevo administrador o se carga cuando se selecciona uno ya existente," + System.Environment.NewLine + "NO PUEDE SER MODIFICADO MANUALMENTE");
            ttmensaje.SetToolTip(pbUsuario, "El Nombre de Usuario con el cual será identificado el Administrador en el sistema" + System.Environment.NewLine + "Se usa para Iniciar Sesión en la Pestaña de Login del Sistema," + System.Environment.NewLine + " TODOS LOS NOMBRES DE USUARIOS DEBEN SER DIFERENTES");
            ttmensaje.SetToolTip(pbRut, "El RUT personal del administrador, el cual debe ser válido" + System.Environment.NewLine + "El Rut debe introducirse sin . (Punto) y Con – (Guion)");
            ttmensaje.SetToolTip(pbNombre, "El Nombre Real del Administrador," + System.Environment.NewLine + "SOLO PUEDEN SER INGRESADAS LETRAS");
            ttmensaje.SetToolTip(pbTelefono, "El Telefono Real del Administrador" + System.Environment.NewLine + "SOLO SE PUEDEN INGRESAR NUMEROS y SIMBOLO +");
            ttmensaje.SetToolTip(pbEstado, "El Estado Actual del Administrador el Cual puede ser ACTIVO o INACTIVO," + System.Environment.NewLine + "Activo Significa que el Administrador puede Ingresar y Trabajar Dentro del Sistema, e" + System.Environment.NewLine + "Inactivo hace que el Administrador No pueda acceder al sistema e interactuar con el mismo.");
            ttmensaje.SetToolTip(pbClaveNueva, "La Clave Con la Cual El Administrador Iniciara Sesión en el Login, " + System.Environment.NewLine + "por motivos de seguridad la clave no se muestra visualmente " + System.Environment.NewLine + "y debe de tener las siguientes características: " + System.Environment.NewLine + "-Mínimo una letra Mayúscula  " + System.Environment.NewLine + "-Mínimo una letra Minúscula  " + System.Environment.NewLine + "-Mínimo un Numero  " + System.Environment.NewLine + "-Mínimo 8 caracteres de Largo");
            ttmensaje.SetToolTip(pbReingreseClave, "Debe de reingresar la Clave del Administrador, para verificar si ambas son idénticas");
        }

        private void Administradores_Load(object sender, EventArgs e)
        {
            //Se extablecen los valores por defecto
            cbEstado.Text = "Activo";
            CargarAdmins();
            cmbBuscarEn.Text = "ID Admin";
        }

        private void CargarAdmins()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarAdmins = new ComandosBDMySQL();
            try
            {
                cargarAdmins.AbrirConexionBD1();
                dgbAdmins.DataSource = cargarAdmins.RellenarTabla1("SELECT * FROM sbepa.vista_login_administradores;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar cargar la tabla de Administradores ERROR: "+ ex.Message , "Error Informacion Administradores",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarAdmins.CerrarConexionBD1();
            }
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            //Se transforma la busqueda para que sea compatible con las columnas de la BD
            String BuscarEn;
            if (cmbBuscarEn.Text == "ID Admin")
            {
                BuscarEn = "idlogin_administradores";
            }
            else if (cmbBuscarEn.Text == "Usuario")
            {
                BuscarEn = "usuario";
            }
            else if (cmbBuscarEn.Text == "Rut")
            {
                BuscarEn = "rut";
            }
            else if (cmbBuscarEn.Text == "Nombre")
            {
                BuscarEn = "nombre";
            }
            else if (cmbBuscarEn.Text == "Telefono")
            {
                BuscarEn = "telefono";
            }
            else
            {
                BuscarEn = "estado";
            }

            //se crea la instancia para buscar en la tabla, se carga el resultado en el datagridview, y siempre se cierra la conexion 
            ComandosBDMySQL buscarTabla = new ComandosBDMySQL();
            try
            {
                buscarTabla.AbrirConexionBD1();
                dgbAdmins.DataSource = buscarTabla.RellenarTabla1("SELECT idlogin_administradores as 'ID Admin',usuario as 'Usuario',rut as 'Rut',nombre as 'Nombre',telefono as 'Telefono',estado as 'Estado' FROM sbepa.login_administradores Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%';");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar Buscar con los parametros ingresados ERROR:" +ex.Message, "Error busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buscarTabla.CerrarConexionBD1();
            }
        }

        private void imgRecargar_Click(object sender, EventArgs e)
        {
            //si se preciona el boton recargar se cargan denuevo los admins
            CargarAdmins();
        }

        private void ActivarModificar()
        {
            //Metodo para Activar la modificacion de los datos
            txtUsuario.ReadOnly = true;
            txtRut.ReadOnly = true;
            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
            cbEstado.Text = "Activo";
            cbCambiarClave.Visible = true;
            lblClaveNueva.Visible = false;
            txtClave1.Visible = false;
            lblReingreseClave.Visible = false;
            txtClave2.Visible = false;
            btnEliminar.Enabled = true;
            pbBorrar.Visible = true;
            btnEliminar.Visible = true;
            pbReingreseClave.Visible = false;
            pbClaveNueva.Visible = false;
        } 

        private void ActivarNuevo()
        {
            //Metodo para Activar el Registro de un nuevo Admin
            txtUsuario.ReadOnly = false;
            txtID.Text = "";
            txtUsuario.Text = "";
            txtRut.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            cbEstado.Text = "Activo";
            txtClave1.Text = "";
            txtClave2.Text = "";
            cbCambiarClave.Checked = false;
            cbCambiarClave.Visible = false;
            lblClaveNueva.Visible = true;
            txtClave1.Visible = true;
            lblReingreseClave.Visible = true;
            txtClave2.Visible = true;
            txtRut.ReadOnly = false;
            btnEliminar.Enabled = false;
            pbBorrar.Visible = false;
            btnEliminar.Visible = false;
            pbClaveNueva.Visible = true;
            pbReingreseClave.Visible = true;
        }

        private Boolean VerificarDatos()
        {
            //Se verifica si el los campos tiene contenido
            if (txtUsuario.Text == "" || txtRut.Text == "" || txtNombre.Text == "" || txtTelefono.Text == "")
            {
                MessageBox.Show("Verifique que todos los Campos de Datos esten Rellenados","Campo Vacio Detectado", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                //Se verifica si el Rut es correcto
                FuncionesAplicacion VerificarRut = new FuncionesAplicacion();
                if (VerificarRut.validarRut(txtRut.Text) == true)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("El RUT ingresado no es valido reingreselo por favor","Error Rut",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return false;
                }     
            }
        }

        private Boolean VerificarContraceña()
        {
            //Se verifica que las claves tengan contenido
            if (txtClave1.Text == "" || txtClave2.Text == "")
            {
                MessageBox.Show("Verifique que el Campo 'Clave Nueva' y 'Reingrese Clave' NO estan vacios","Clave Vacia", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                //Se verifica si las claves son identicas
                if (txtClave1.Text == txtClave2.Text)
                {
                    //Se verifica que la clave no sea igual al nombre de usuario
                    if (txtUsuario.Text != txtClave1.Text)
                    {
                        //Si la verificacion de la clave devuelve true significa que es segura
                        FuncionesAplicacion VerificarSeguridadClave = new FuncionesAplicacion();
                        return VerificarSeguridadClave.VerificarContraceñaSegura(txtClave1.Text);
                    }
                    else
                    //Si la clave es igual al nombre de usuario
                    {
                        MessageBox.Show("La clave no puede ser igual al nombre de Usuario", "Clave Insegura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }        
                }
                //Si las claces no son identicas
                else
                {
                    MessageBox.Show("La Clave Nueva y el Reingreso de Clave no Coincide por favor reingrese las claves y verifique que son identicas","Claves Incorrectas",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return false;
                }
            }
        }

        private void cbCambiarClave_CheckedChanged(object sender, EventArgs e)
        {      
            if (cbCambiarClave.Checked == true)
            {
                //si cambiar clave esta Seleccionado, se hacen visibles los campos de clave
                lblClaveNueva.Visible = true;
                txtClave1.Visible = true;
                lblReingreseClave.Visible = true;
                txtClave2.Visible = true;
                pbClaveNueva.Visible = true;
                pbReingreseClave.Visible = true;
            }
            else
            {
                //si cambiar clave no esta Seleccionado, se hacen invisibles los campos de clave
                lblClaveNueva.Visible = false;
                txtClave1.Visible = false;
                lblReingreseClave.Visible = false;
                txtClave2.Visible = false;
                pbClaveNueva.Visible = false;
                pbReingreseClave.Visible = false;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //SI el ID esta vacio se registran los datos
            if (txtID.Text == "")
            {
                if (VerificarContraceña() == true && VerificarDatos() == true)
                {
                    //Se envia mensaje para verificar la decision
                    DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea registrar al nuevo administrador y esta seguro que sus datos estan correcto", "Nuevo Administrador", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    //Se contesta que si
                    if (resultadoMensaje == DialogResult.Yes)
                    {
                        //Si la ID esta vacio se registra 
                        ComandosBDMySQL RegistrarAdmin = new ComandosBDMySQL();
                        RegistrarAdmin.AbrirConexionBD1();

                        FuncionesAplicacion calcularHash = new FuncionesAplicacion();
                        String HashDeClave = calcularHash.TextoASha256(txtClave1.Text);

                        //Se verifica siel usuario esta registrado
                        ComandosBDMySQL VerificarExistenciaUsuario = new ComandosBDMySQL();
                        try
                        {
                            VerificarExistenciaUsuario.AbrirConexionBD1();
                            Boolean ExisteUsuario = VerificarExistenciaUsuario.VerificarExistenciaDato1("SELECT * FROM sbepa.login_administradores WHERE usuario='" + txtUsuario.Text + "';");
                            VerificarExistenciaUsuario.CerrarConexionBD1();

                            //Si el usuario existe se muestra un mensaje de error
                            if (ExisteUsuario == true)
                            {
                                MessageBox.Show("El Usuario ya se encuentra registrado como Administrador, intente con otro nombre de usuario", "Usuario Registrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                // Se verifica si el rut se encuentra ingresado en el sistema
                                ComandosBDMySQL verificarRutRegistrado = new ComandosBDMySQL();
                                try
                                {
                                    verificarRutRegistrado.AbrirConexionBD1();
                                    Boolean ResultadoExistenciaRut = verificarRutRegistrado.VerificarExistenciaDato1("SELECT * FROM sbepa.login_administradores Where rut = '" + txtRut.Text + "';");

                                    if (ResultadoExistenciaRut == true)
                                    {
                                        //si el rut esta registrado se envia un mensaje
                                        MessageBox.Show("El Rut Ingresado ya esta registrado en un Usuario Administrador del sistema", "RUT Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        //si el rut no esta regsitrado se devuelve verdadero
                                        //Se debe de ingresar la Clave Maestra para continuar con la operacion
                                        ClaveMaestra verificarClave = new ClaveMaestra();

                                        if (verificarClave.ShowDialog() == DialogResult.OK)
                                        {
                                            //Si el usuario no existe se registra
                                            RegistrarAdmin.IngresarConsulta1("call sbepa.inserta_login_administradores('" + txtUsuario.Text + "', '" + HashDeClave + "', '" + txtRut.Text + "', '" + txtNombre.Text + "', '" + txtTelefono.Text + "', '" + cbEstado.Text + "');");
                                            RegistrarAdmin.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Administradores','Insertar','REGISTRO UN ADMINISTRADOR CON EL NOMBRE: " + txtUsuario.Text + " RUT: " + txtRut.Text + " NOMBRE: " + txtNombre.Text + " TELEFONO: " + txtTelefono.Text + " ESTADO: " + cbEstado.Text + "');");
                                            MessageBox.Show("Se ha registrado correctamente el nuevo Administrador", "Registro Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            ActivarNuevo();
                                            CargarAdmins();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("No se ha podido verificar si el RUT no esta registrado en el Sistema ERROR:" + ex.Message, "Error verificar RUT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                finally
                                {
                                    verificarRutRegistrado.CerrarConexionBD1();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar Registar el Nuevo Usuario ERROR:" + ex.Message, "Error al Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            RegistrarAdmin.CerrarConexionBD1();
                        }
                    }
                }  
            }
            //Si el ID tiene datos se actualizan
            else
            {
                //Se verifican los datos
                if (VerificarDatos() == true)
                {
                    //Si el ID tiene contenido Se Modifican los Datos
                    if (cbCambiarClave.Checked == true)
                    {
                        //Se veririca si se ha ingresado correctamente la nueva clave
                        if (VerificarContraceña() == true)
                        {
                            FuncionesAplicacion calcularHash = new FuncionesAplicacion();
                            String HashDeClave = calcularHash.TextoASha256(txtClave1.Text);

                            //Se debe de ingresar la Clave Maestra para continuar con la operacion
                            ClaveMaestra verificarClave = new ClaveMaestra();

                            if (verificarClave.ShowDialog() == DialogResult.OK)
                            {
                                //Si esta seleccionado cambiar clave
                                ComandosBDMySQL ActualizarAdminConClave = new ComandosBDMySQL();
                                try
                                {
                                    ActualizarAdminConClave.AbrirConexionBD1();
                                    ActualizarAdminConClave.IngresarConsulta1("call sbepa.actualizar_login_administradores_conclave(" + txtID.Text + ", '" + txtUsuario.Text + "', '" + HashDeClave + "', '" + txtRut.Text + "', '" + txtNombre.Text + "', '" + txtTelefono.Text + "', '" + cbEstado.Text + "');");
                                    ActualizarAdminConClave.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Administradores','Modificar y Cambio Clave','CAMBIO DE LA CLAVE Y LOS DATOS DEL ADMINISTRADOR CON ID: " + txtID.Text + " CON EL NOMBRE: " + txtUsuario.Text + " RUT: " + txtRut.Text + " NOMBRE: " + txtNombre.Text + " TELEFONO: " + txtTelefono.Text + " ESTADO: " + cbEstado.Text + "');");
                                    CargarAdmins();
                                    ActivarNuevo();
                                    MessageBox.Show("Datos del Administrador Actualizados Correctamente");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Intentar actualizar los datos del Administrador ERROR: "+ex.Message, "Error Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                finally
                                {
                                    ActualizarAdminConClave.CerrarConexionBD1();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        //Se debe de ingresar la Clave Maestra para continuar con la operacion
                        ClaveMaestra verificarClave = new ClaveMaestra();

                        if (verificarClave.ShowDialog() == DialogResult.OK)
                        {
                            //Si no esta seleccionado cambiar clave
                            ComandosBDMySQL ActualizarAdminSinClave = new ComandosBDMySQL();
                            try
                            {
                                ActualizarAdminSinClave.AbrirConexionBD1();
                                ActualizarAdminSinClave.IngresarConsulta1("call sbepa.actualizar_login_administradores_sinclave(" + txtID.Text + ", '" + txtUsuario.Text + "', '" + txtRut.Text + "', '" + txtNombre.Text + "', '" + txtTelefono.Text + "', '" + cbEstado.Text + "');");
                                ActualizarAdminSinClave.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Administradores','Modificar', 'CAMBIO LOS DATOS DEL ADMINISTRADOR CON ID: " + txtID.Text + " CON LOS SIGUIENTES DATOS, NOMBRE: " + txtUsuario.Text + " RUT: " + txtRut.Text + " NOMBRE: " + txtNombre.Text + " TELEFONO: " + txtTelefono.Text + " ESTADO: " + cbEstado.Text + "');");
                                CargarAdmins();
                                ActivarNuevo();
                                MessageBox.Show("Datos del Administrador Actualizados Correctamente");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al Intentar actualizar los datos del Administrador ERROR: "+ex.Message, "Error Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            finally
                            {
                                ActualizarAdminSinClave.CerrarConexionBD1();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                } 
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se restringuen los caracteres del usuario
            e.Handled = VerificarCaracteres.RestringirCaracteresUsuario(e);
        }

        private void txtRut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresRUT(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresTelefono(e);
        }

        private void txtClave1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }

        private void txtClave2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActivarNuevo();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresNombre(e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("El Administrador Solamente puede ser borrado si no hay ningun registro ligado a el, ¿Desea Eliminar al Administrador Seleccionado?", "Informacion Borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Se contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    //se verifica si el admin tiene registros en la BD
                    ComandosBDMySQL VerificarRegistrosAdmin = new ComandosBDMySQL();
                    Boolean AdminTieneRegistros = false;
                    try
                    {
                        VerificarRegistrosAdmin.AbrirConexionBD1();
                        AdminTieneRegistros = VerificarRegistrosAdmin.VerificarExistenciaDato1("Select id_administrador_login From registro_login_administradores Where id_administrador_login = '" + txtID.Text + "';");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Verificar si el Administrador puede ser Eliminado ERROR:"+ex.Message, "Error Verificacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        VerificarRegistrosAdmin.CerrarConexionBD1();
                    }

                    if (AdminTieneRegistros == false)
                    {
                        //si el admin no tiene registros se puede borrar
                        ClaveMaestra verificarEliminarClave = new ClaveMaestra();
                        ComandosBDMySQL EliminarAdmin = new ComandosBDMySQL();
                        try
                        {
                            //Se verifica la clave maestra
                            if (verificarEliminarClave.ShowDialog() == DialogResult.OK)
                            {
                                //Si la clave es correcta se procede a eliminarlo del sistema
                                EliminarAdmin.AbrirConexionBD1();
                                EliminarAdmin.IngresarConsulta1("call sbepa.eliminar_login_administradores('" + txtID.Text + "');");
                                EliminarAdmin.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Administradores','Eliminar', 'ELIMINO AL ADMINISTRADOR CON EL ID: " + txtID.Text + " , LA CUAL TENIA POR NOMBRE: " + txtNombre.Text + "');");
                                MessageBox.Show("Administrador Eliminado Correctamente", "Proceso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ActivarNuevo();
                                CargarAdmins();
                            }
                            else
                            {
                                //Se muestra un mensaje para que el usuario ingrese la clave
                                MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al Intentar Eliminar a el Administrador del Sistema ERROR: "+ex.Message, "Error Borrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            EliminarAdmin.CerrarConexionBD1();
                        }
                    }
                    else
                    {
                        //Se muestra mensaje si el Admin no se puede eliminar
                        MessageBox.Show("El Administrador que se ha intentado eliminar tiene registros en el sistema y no puede ser eliminado", "Error Eliminar Administrador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }         
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un Administrador para Eliminar", "No hay Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgbAdmins_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se activa la capacidad de modificar los datos
                ActivarModificar();
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbAdmins.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID Admin"].Value);
                txtUsuario.Text = Convert.ToString(fila.Cells["Usuario"].Value);
                txtRut.Text = Convert.ToString(fila.Cells["Rut"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value);
                txtTelefono.Text = Convert.ToString(fila.Cells["Telefono"].Value);
                cbEstado.Text = Convert.ToString(fila.Cells["Estado"].Value);
            }
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
    }
}
