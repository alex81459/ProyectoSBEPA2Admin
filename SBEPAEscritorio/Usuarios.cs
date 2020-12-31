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
    public partial class Usuarios : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        private String BuscarEn = "";
        
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();
        public Usuarios()
        {
            InitializeComponent();
            //Se Configuran los Mensajes de informacion de los campos por medio del objeto ToolTip
            ttmensaje.SetToolTip(pbID, "El ID (Numero Identificador) del Usuario, este se genera automáticamente cuando" + System.Environment.NewLine + "se registra un nuevo administrador o se carga cuando se selecciona uno ya existente," + System.Environment.NewLine + "NO PUEDE SER MODIFICADO MANUALMENTE");
            ttmensaje.SetToolTip(pbUsuario, "El Usuario con el cual se registro, este le permite iniciar sesion junto con" + System.Environment.NewLine + "la contraceña, puede ser modificado en caso de olvido del usuario o otras necesidadades" + System.Environment.NewLine + "solo puede contener numeros, letras mayuculas y minusculas");
            ttmensaje.SetToolTip(pbEstado, "El Estado del Usuario el cual puede ser Activo o Suspendido este parametro no" + System.Environment.NewLine + "no puede ser editado, solo puede ser cambiado desde el Sistema de Usuarios Baneados");
            ttmensaje.SetToolTip(pbCorreoElectronico, "El Correo Electronico del Usuario, el cual puede ser cambiado " + System.Environment.NewLine + "se revisara al almacence si cumple con los estandares de un correo electronico");
            ttmensaje.SetToolTip(txtDireccion, "La direccion donde vive actualmente el usuario, asi de simple :)");
            ttmensaje.SetToolTip(txtRut, "El RUT (Rol Unico Tributario) del usuario el cual se verificara al ser registrado");
            ttmensaje.SetToolTip(txtNombre, "Los Nombres actuales del Usuario, asi de simple :)");
            ttmensaje.SetToolTip(txtApellido, "Los Apellidos actuales del Usuario, asi de simple :)");
            ttmensaje.SetToolTip(pbClaveNueva, "La Clave Con la Cual El Usuario Iniciara Sesión, " + System.Environment.NewLine + "por motivos de seguridad la clave no se muestra visualmente " + System.Environment.NewLine + "y debe de tener las siguientes características: " + System.Environment.NewLine + "-Mínimo una letra Mayúscula  " + System.Environment.NewLine + "-Mínimo una letra Minúscula  " + System.Environment.NewLine + "-Mínimo un Numero  " + System.Environment.NewLine + "-Mínimo 10 caracteres de Largo");
            ttmensaje.SetToolTip(pbReingreseClave, "Debe de reingresar la Clave del Usuario, para verificar si ambas son idénticas");
            ttmensaje.SetToolTip(pbTelefono, "El numero de telefono del usuario para contectarlo");
            cmbBuscarEn.Text = "RutUsuario";
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

        private void cbCambiarClave_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCambiarClave.Checked == true)
            {
                gbCambioClave.Visible = true;
            }
            else
            {
                gbCambioClave.Visible = false;
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresUsuario(e);
        }

        private void txtRut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresRUT(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresNombre(e);
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresNombre(e);
        }

        private void txtCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDireccion(e);
        }

        private void txtCorreoElectronico_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresCorreo(e);
        }

        private void txtClave1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }

        private void txtClave2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            ComandosBDMySQL CargarUsuarios = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de usuarios, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                CargarUsuarios.AbrirConexionBD1();
                txtCantidadRegistro.Text = (CargarUsuarios.RellenarTabla1("SELECT COUNT(id_usuario) FROM sbepa2.vistausuarios;").Rows[0][0].ToString());
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                dgbUsuarios.DataSource = CargarUsuarios.RellenarTabla1("SELECT * FROM sbepa2.vistausuarios;");
            }
            catch (Exception ex)
            {
                //Se atrapa el error
                MessageBox.Show("Error al intentar cargar los usuarios ERROR:" + ex.Message + "", "Error Extraer Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                //Se cierra la conexion
                CargarUsuarios.CerrarConexionBD1();
            }
        }

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbUsuarios.DataSource = SiguientePagina.RellenarTabla1("SELECT * FROM sbepa2.usuarios limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
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

        private void ActivarBusqueda()
        {
            label7.Visible = true;
            txtBuscarEn.Visible = true;
            btnBuscar.Visible = true;
            picLupa.Visible = true;
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
                    //Se cargan los datos necesarios para la busquedam y el ordenamiento de las paginas
                    BuscarUsuario.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarUsuario.RellenarTabla1("SELECT COUNT(id_usuario) FROM sbepa2.usuarios Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%' ORDER BY id_usuario DESC;").Rows[0][0].ToString());
                    txtUsuariosEncontrados.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbUsuarios.DataSource = BuscarUsuario.RellenarTabla1("call sbepa2.BuscarUsuarios('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
                    label19.Visible = true;
                    txtUsuariosEncontrados.Visible = true;
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
        private void ActivarControlpaginas()
        {
            label5.Visible = true;
            nudPaginaActualBuscar.Visible = true;
            label9.Visible = true;
            txtPaginasDisponiblesBusqueda.Visible = true;
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
            label5.Visible = false;
            nudPaginaActualBuscar.Visible = false;
            label9.Visible = false;
            txtPaginasDisponiblesBusqueda.Visible = false;
            txtBuscarEn.Text = "";
            nudPaginaActualBuscar.Value = 0;
            txtUsuariosEncontrados.Text = "?????????";
            txtUsuariosEncontrados.Visible = false;
            label19.Visible = false;
        }

        private void Limpiar()
        {
            txtID.Text = "";
            txtUsuario.Text = "";
            cbEstado.Text = "Prueba";
            txtCorreoElectronico.Text = "";
            txtDireccion.Text = "";
            txtRut.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtClave1.Text = "";
            txtClave2.Text = "";
            gbCambioClave.Visible = true;
            cbCambiarClave.Visible = false;
            pictureBox9.Visible = false;
            txtUsuario.ReadOnly = false;
            txtRut.ReadOnly = false;
            cbCambiarClave.Checked = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtUsuario.Enabled = false;
            txtTelefono.Enabled = true;
            txtTelefono.Text = "";
            txtUsuario.Enabled = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                //Si el ID esta vacio se registra el usuario
                if (txtUsuario.Text == "" || txtCorreoElectronico.Text == "" || txtDireccion.Text == "" || txtRut.Text == "" || txtNombre.Text == "" || txtApellido.Text == "" || txtClave1.Text == "" || txtClave2.Text == "" || txtTelefono.Text == "")
                {
                    MessageBox.Show("Por favor verifique que todos los datos necesarios para registrar un Usuario se encuentrasn rellenados: Usuario, Correo Electronico, Ciudad, RUT, Nombre, Apellido, la clave Nueva y su Reingreso", "Faltan Datos de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ComandosBDMySQL RegistrarUsuarioNuevo = new ComandosBDMySQL();
                    try
                    {
                        RegistrarUsuarioNuevo.AbrirConexionBD1();
                        Boolean ExisteUsuario = RegistrarUsuarioNuevo.VerificarExistenciaDato1("SELECT NombreUsuario FROM sbepa2.credencialesusuarios where NombreUsuario = '"+txtUsuario.Text+"';");
                        if (ExisteUsuario == true)
                        {
                            MessageBox.Show("Ya se encuentra registrado un Usuario: " + txtUsuario.Text + " con ese nombre, intente con otro nuevo", "Usuario Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Boolean ExistenciaRUT = RegistrarUsuarioNuevo.VerificarExistenciaDato1("call sbepa2.VerificarExitenciaUsuario('"+txtRut.Text+"');");
                            if (ExistenciaRUT == true)
                            {
                                MessageBox.Show("Ya se encuentra registrado un Usuario con el RUT: " + txtRut.Text + " intente con otro rut", "Rut Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                FuncionesAplicacion verificarCorreo = new FuncionesAplicacion();
                                Boolean CorreoCorrecto = verificarCorreo.VerificarCorreo(txtCorreoElectronico.Text);
                                if (CorreoCorrecto == false)
                                {
                                    MessageBox.Show("El Correo Electronico a Registrar no es correcto, por favor verifique que cumple con las estandares ejemplo correo1@doninioprueba.cl", "Correo Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    FuncionesAplicacion verificarRut = new FuncionesAplicacion();
                                    Boolean RutCorrecto = verificarRut.validarRut(txtRut.Text);

                                    if (RutCorrecto == false)
                                    {
                                        MessageBox.Show("El Rut ingresado no es correcto, verifique que esta correctamente escrito", "Error RUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        FuncionesAplicacion verificarClaveSegura = new FuncionesAplicacion();
                                        Boolean ClaveSegura = verificarClaveSegura.VerificarContraceñaSegura(txtClave1.Text);

                                        if (ClaveSegura == true)
                                        {
                                            if (txtClave1.Text != txtClave2.Text)
                                            {
                                                MessageBox.Show("Las claves ingresadas no son correctas, verifique que las dos sean iguales", "Claves Diferentes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                //Si todo esta correcto se registra al usuario con sus sha256 de clave y todos los demas datos
                                                FuncionesAplicacion hashClave = new FuncionesAplicacion();
                                                String Sha256deClave = hashClave.TextoASha256(txtClave1.Text);
                                                //Se registra el usuario
                                                RegistrarUsuarioNuevo.IngresarConsulta1("call sbepa2.InsertarUsuario('"+txtRut.Text+"', '"+txtNombre.Text+"', '"+txtApellido.Text+"', '"+txtCorreoElectronico.Text+"', '"+txtDireccion.Text+"', '"+txtTelefono.Text+"', '"+ cbEstado.Text+ "');");
                                                //Se registra la credencial
                                                String IDNuevoUsuario = RegistrarUsuarioNuevo.RellenarTabla1("SELECT Id_usuario FROM sbepa2.usuarios where RutUsuario = '"+txtRut.Text+"';").Rows[0]["Id_usuario"].ToString();
                                                RegistrarUsuarioNuevo.IngresarConsulta1("call sbepa2.InsertarCredencialesUsuarios('"+txtUsuario.Text+"', '"+ Sha256deClave + "', "+ IDNuevoUsuario + ");");
                                                //Se registran los cambios hechos por el administrador
                                                RegistrarUsuarioNuevo.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Usuarios', 'Insertar', 'REGISTRO EL USUARIO: " + txtNombre.Text + ", CON EL NOMBRE DE USUARIO: " + txtUsuario.Text + " CORREO ELECTRONICO: " + txtCorreoElectronico.Text + " CIUDAD: " + txtDireccion.Text + " RUT: " + txtRut.Text + " NOMBRE: " + txtNombre.Text + " APELLIDO: " + txtApellido.Text + " TELFONO: "+txtTelefono.Text+"');");

                                                MessageBox.Show("Nuevo Usuario: " + txtUsuario.Text + " a sido correctamente registrado en el sistema y sus credenciales fueron registradas", "Usuario Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                CargarUsuarios();
                                                Limpiar();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar registrar un usuario nuevo Error:" + ex.Message + "", "Error al Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        RegistrarUsuarioNuevo.CerrarConexionBD1();
                    }
                }
            }
            else
            {
                //Si el ID tiene contenido se actualiza el usuario
                if (txtID.Text != "")
                {
                    if (txtUsuario.Text == "" || txtCorreoElectronico.Text == "" || txtDireccion.Text == "" || txtRut.Text == "" || txtNombre.Text == "" || txtApellido.Text == "" || txtCorreoElectronico.Text == "")
                    {
                        MessageBox.Show("Por favor verifique que todos los datos necesarios para registrar un Usuario se encuentrasn rellenados: Usuario, Correo Electronico, Ciudad, RUT, Nombre, Apellido, la clave Nueva y su Reingreso, y el Telefono", "Faltan Datos de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ComandosBDMySQL ActualizarUsuario = new ComandosBDMySQL();
                        try
                        {
                            ActualizarUsuario.AbrirConexionBD1();
                            FuncionesAplicacion verificarCorreo = new FuncionesAplicacion();
                            Boolean CorreoCorrecto = verificarCorreo.VerificarCorreo(txtCorreoElectronico.Text);
                            if (CorreoCorrecto == false)
                            {
                                MessageBox.Show("El Correo Electronico a Registrar no es correcto, por favor verifique que cumple con las estandares ejemplo correo1@doninioprueba.cl", "Correo Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {  
                                if (cbCambiarClave.Checked == true)
                                {
                                    FuncionesAplicacion verificarClaveSegura = new FuncionesAplicacion();
                                    Boolean ClaveSegura = verificarClaveSegura.VerificarContraceñaSegura(txtClave1.Text);

                                    if (ClaveSegura == true)
                                    {
                                        if (txtClave1.Text != txtClave2.Text)
                                        {
                                            MessageBox.Show("Las claves ingresadas no son correctas, verifique que las dos sean iguales", "Claves Diferentes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            //Si todo esta correcto se actualiza sus datos y su clave con sha256
                                            FuncionesAplicacion hashClave = new FuncionesAplicacion();
                                            String Sha256deClave = hashClave.TextoASha256(txtClave1.Text);
                                            //Se actualiza el usuario
                                            ActualizarUsuario.IngresarConsulta1("call sbepa2.ActualizarUsuario("+txtID.Text+", '"+txtCorreoElectronico.Text+"', '"+txtDireccion.Text+"', '"+txtTelefono.Text+"', '"+ cbEstado.Text+ "');");
                                            //Se actualiza la contraceña del usuario
                                            String IDUsuario = ActualizarUsuario.RellenarTabla1("SELECT Id_usuario FROM sbepa2.usuarios where RutUsuario = '" + txtRut.Text + "';").Rows[0][0].ToString();
                                            ActualizarUsuario.IngresarConsulta1("call sbepa2.ActualizarCredencialesUsuario("+txtID.Text+", '"+txtUsuario.Text+"', '"+ Sha256deClave + "');");
                                            //Se registra el cambio
                                            ActualizarUsuario.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Usuarios', 'Actualizar', 'ACTUALIZO EL USUARIO: " + txtRut.Text + " CON EL CORREO: " + txtCorreoElectronico.Text + " LA CIUDAD: " + txtDireccion.Text + " EL NOMBRE: " + txtNombre.Text + " EL APELLIDO: " + txtApellido.Text + " EL TELEFONO: "+txtTelefono.Text+" Y REALIZO EL CAMBIO DE LA CONTRASEÑA DEL USUARIO');");

                                            MessageBox.Show("Actualizado Usuario: " + txtRut.Text + " a sido correctamente actualizado en el sistema", "Usuario Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            CargarUsuarios();
                                            Limpiar();
                                        }
                                    }
                                }
                                else
                                {
                                    //Si todo esta correcto se actualiza sus datos y no la clave
                                    FuncionesAplicacion hashClave = new FuncionesAplicacion();
                                    //Se actualiza el usuario
                                    ActualizarUsuario.IngresarConsulta1("call sbepa2.ActualizarUsuario(" + txtID.Text + ", '" + txtCorreoElectronico.Text + "', '" + txtDireccion.Text + "', '" + txtTelefono.Text + "', '" + cbEstado.Text + "');");
                                    //Se registra el cambio
                                    ActualizarUsuario.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Usuarios', 'Actualizar', 'ACTUALIZO EL USUARIO: " + txtRut.Text + " CON EL CORREO: " + txtCorreoElectronico.Text + " LA CIUDAD: " + txtDireccion.Text + " EL NOMBRE: " + txtNombre.Text + " EL APELLIDO: " + txtApellido.Text + " EL TELEFONO: " + txtTelefono.Text + "');");
                                    CargarUsuarios();
                                    Limpiar();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar actualiazar los datos del Usuario  Error:" + ex.Message + "", "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            ActualizarUsuario.CerrarConexionBD1();
                        }
                    }
                }
            }
        }

        private void dgbUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se activa la capacidad de modificar los datos
                ActivarModificar();
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbUsuarios.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID"].Value);
                cbEstado.Text = Convert.ToString(fila.Cells["Estado"].Value);
                txtCorreoElectronico.Text = Convert.ToString(fila.Cells["CorreoElectronico"].Value);
                txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);
                txtRut.Text = Convert.ToString(fila.Cells["RUT"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["Nombres"].Value);
                txtApellido.Text = Convert.ToString(fila.Cells["Apellidos"].Value);
                txtTelefono.Text = Convert.ToString(fila.Cells["Telefono"].Value);

                //Se busca el nombre de usuario en las credenciales
                ComandosBDMySQL BuscarUsuario = new ComandosBDMySQL();
                try
                {
                    BuscarUsuario.AbrirConexionBD1();
                    txtUsuario.Text = (BuscarUsuario.RellenarTabla1("SELECT NombreUsuario FROM sbepa2.credencialesusuarios where idUsuario = "+ txtID.Text + ";").Rows[0][0].ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer el nombre del usuario del sistema  Error:" + ex.Message + "", "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    BuscarUsuario.CerrarConexionBD1();
                }
            }
        }

        private void ActivarModificar()
        {
            txtUsuario.ReadOnly = true;
            txtRut.ReadOnly = true;
            cbCambiarClave.Visible = true;
            pictureBox9.Visible = true;
            gbCambioClave.Visible = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtUsuario.Enabled = false;
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void cmbBuscarEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBusqueda();
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresTelefono(e);
        }

        private void txtEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("ADVERTENCIA no solamente se borrara el usuario, se borrara su tienda, sucursales, productos y registros que existan ligados a el en el sistema, ¿Desea Eliminar el usuario Seleccionado?", "Borrar Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Se contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                        ClaveMaestra verificarEliminarClave = new ClaveMaestra();
                        ComandosBDMySQL EliminarUsuario = new ComandosBDMySQL();
                        try
                        {
                            //Se verifica la clave maestra
                            if (verificarEliminarClave.ShowDialog() == DialogResult.OK)
                            {
                                //Si la clave es correcta se procede a eliminarlo del sistema
                                EliminarUsuario.AbrirConexionBD1();
                                //Se elimina el usuario
                                EliminarUsuario.IngresarConsulta1("call sbepa2.EliminarUsuario("+txtID.Text+");");
                                //Se guarda el registro del borrado
                                EliminarUsuario.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Usuarios', 'Eliminar', 'ELIMINO AL USUARIO CON EL ID: " + txtID.Text + " EL CUAL TENIA POR RUT: " + txtRut.Text + "');");
                               
                                //Se notifica y se carga el sistema
                                MessageBox.Show("El Usuario fue Eliminado Correctamente del Sistema", "Usuario Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Limpiar();
                                CargarUsuarios();
                            } 
                            else
                            {
                                //Se muestra un mensaje para que el usuario ingrese la clave
                                MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al Intentar Eliminar al Usuario del Sistema ERROR:" + ex.Message, "Error Borrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            EliminarUsuario.CerrarConexionBD1();
                        }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Tienda para Eliminar", "No hay Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
