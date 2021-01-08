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
    public partial class BaneoUsuarios : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public BaneoUsuarios()
        {
            InitializeComponent();
            CargarBaneosUsuarios();
            lblFechaBaneo.Text = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));
            cmbBuscarEn.Text = "idUsuarioBaneado";
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

        private void txtRazonBaneo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            CargarBaneosUsuarios();
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtBuscarEn.Text = "";
            cmbBuscarEn.Text = "id_baneo";
            txtIDUsuario.Text = "¿?";
            txtNombresUsuario.Text = "¿?";
            txtRazonBaneo.Text = "";
            lblFechaBaneo.Text = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));
            nudDias.Value = 1;
            txtIDBaneo.Text = "¿?";
            btnBuscarUsuario.Enabled = true;
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            ComandosBDMySQL buscarTabla = new ComandosBDMySQL();
            try
            {
                buscarTabla.AbrirConexionBD1();
                dgbUsuariosBaneados.DataSource = buscarTabla.RellenarTabla1("SELECT id_baneo,nombre_usuario, razon_baneo,fecha,dias_baneo,id_usuario_baneado  FROM sbepa.usuarios_baneados Inner Join usuarios on usuarios_baneados.id_usuario_baneado = usuarios.id_usuario Where "+ cmbBuscarEn.Text+ " = '"+ txtBuscarEn.Text+ "' ORDER BY id_baneo DESC;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar Buscar con los parametros ingresados ERROR: " + ex.Message, "Error busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buscarTabla.CerrarConexionBD1();
            }
        }

        private void pBActualizar_Click(object sender, EventArgs e)
        {
            CargarBaneosUsuarios();
            LimpiarCampos();
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            BaneoUsuariosBuscarUsuario abrirUsuarios = new BaneoUsuariosBuscarUsuario();
            abrirUsuarios.ShowDialog();
        }

        private void btnGuardarBaneo_Click(object sender, EventArgs e)
        {
            if (txtIDBaneo.Text == "¿?")
            {
                //Si es distinto el ID del baneo a Deconocido ¿? se actualiza el baneo
                if (txtIDUsuario.Text != "¿?" && txtRazonBaneo.Text != "" && nudDias.Value > 0)
                {
                    //Se envia mensaje para verificar la decision
                    DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea banear al usuario actual?, una vez ingresado no podra ser eliminado el baneo del usuario", "Baneo Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    //Se contesta que si
                    if (resultadoMensaje == DialogResult.Yes)
                    {
                        ComandosBDMySQL RegistrarBaneo = new ComandosBDMySQL();
                        try
                        {
                            RegistrarBaneo.AbrirConexionBD1();
                            RegistrarBaneo.IngresarConsulta1("call sbepa2.InsertarRegistroUsuariosBaneados('"+ txtRazonBaneo.Text+ "', "+ nudDias.Value.ToString()+ ", "+ txtIDUsuario.Text+ ");");
                            RegistrarBaneo.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Baneos Usaurios', 'Añadir', 'Baneo al Usuario con ID: "+txtIDUsuario.Text+" Que tiene por nombre: "+ txtNombresUsuario.Text+ " La razon del baneo fue: "+ txtRazonBaneo.Text+ " Una Cantidadad de: "+ nudDias.Value.ToString()+ " Dias');");
                            RegistrarBaneo.CerrarConexionBD1();
                            MessageBox.Show("Se registro correctamente le Baneo del Usuario con ID:"+txtIDUsuario.Text+"", "Registro Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarBaneosUsuarios();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar registar el baneo del usuario ERROR:" + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            RegistrarBaneo.CerrarConexionBD1();
                        }
                    }  
                }
                else
                {
                    MessageBox.Show("Faltan Datos necesarios para banear a un usuario, revise que este ingresado el ID del usuario a banear, la razon del baneo y el numero de dias del baneo","Faltan Datos Necesarios",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                //Si hay un ID de Baneo se actualiza
                ComandosBDMySQL ActualizarBaneo = new ComandosBDMySQL();
                try
                {
                    ActualizarBaneo.AbrirConexionBD1();
                    ActualizarBaneo.IngresarConsulta1("call sbepa2.ActualizarRegistroUsuarioBaneado("+txtIDBaneo.Text+", '"+txtRazonBaneo.Text+"', "+ nudDias.Value.ToString()+ ");");
                    ActualizarBaneo.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Baneos Usaurios', 'Actualizar', 'Actualizo el Baneo con ID: " + txtIDBaneo.Text + " con la razon del baneo: " + txtRazonBaneo.Text + " y modifico los dias A: " + nudDias.Value.ToString() +"');");
                    MessageBox.Show("La registro del baneo del usuario se realizo correctamente", "Registro Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarBaneo.CerrarConexionBD1();
                    LimpiarCampos();
                    CargarBaneosUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar actualizar el baneo del usuario ERROR:" + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    ActualizarBaneo.CerrarConexionBD1();
                }
            }
        }

        private void txtBuscarEn_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
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
                    String CantidadRegistrosDetectados = (BuscarRegistros.RellenarTabla1("SELECT COUNT(idRegistroUsuariosBaneados) FROM sbepa2.registrousuariosbaneados inner join usuarios on registrousuariosbaneados.idUsuarioBaneado = usuarios.Id_usuario Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';").Rows[0][0].ToString());
                    txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbUsuariosBaneados.DataSource = BuscarRegistros.RellenarTabla2("call sbepa2.BuscarUsuariosBaneados('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
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
            // Se avanza a la siguiente pagina
             ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbUsuariosBaneados.DataSource = SiguientePagina.RellenarTabla1("SELECT idRegistroUsuariosBaneados,RazonBaneo,Fecha,DiasBaneo,idUsuarioBaneado,RutUsuario,Nombres,Apellidos FROM sbepa2.registrousuariosbaneados inner join usuarios on registrousuariosbaneados.idUsuarioBaneado = usuarios.Id_usuario limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
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
            CargarBaneosUsuarios();
            HacerInvisiblesyLimpiarCampos();
        }

        private void CargarBaneosUsuarios()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarRegistros = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarRegistros.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarRegistros.RellenarTabla1("SELECT COUNT(idRegistroUsuariosBaneados) FROM sbepa2.registrousuariosbaneados inner join usuarios on registrousuariosbaneados.idUsuarioBaneado = usuarios.Id_usuario;").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                cargarRegistros.AbrirConexionBD1();
                dgbUsuariosBaneados.DataSource = cargarRegistros.RellenarTabla1("SELECT * FROM sbepa2.vistausuariosbaneados;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la Tabla de los registros de Baneos de Usuarios ERROR: " + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void dgbUsuariosBaneados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgbUsuariosBaneados.Rows[e.RowIndex];
            // Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                LimpiarCampos();

                //Se extraen los datos de el DataGridView
                txtIDBaneo.Text = Convert.ToString(fila.Cells["idRegistroUsuariosBaneados"].Value);
                txtIDUsuario.Text = Convert.ToString(fila.Cells["idUsuarioBaneado"].Value);
                txtNombresUsuario.Text = Convert.ToString(fila.Cells["Nombres"].Value)+" "+Convert.ToString(fila.Cells["Apellidos"].Value);
                txtRazonBaneo.Text = Convert.ToString(fila.Cells["RazonBaneo"].Value);
                lblFechaBaneo.Text = Convert.ToString(fila.Cells["Fecha"].Value);
                nudDias.Value = Convert.ToInt32(Convert.ToString(fila.Cells["DiasBaneo"].Value));
            }
        }
    }
}
