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
            CargarTabla();
            lblFechaBaneo.Text = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));
            cmbBuscarEn.Text = "id_baneo";
        }

        private void CargarTabla()
        {
            ComandosBDMySQL cargarTabla = new ComandosBDMySQL();
            try
            {
                cargarTabla.AbrirConexionBD1();
                dgbUsuariosBaneados.DataSource = cargarTabla.RellenarTabla1("SELECT * FROM sbepa.vista_usuarios_baneados;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar Cargar la Tabla Usuarios Baneados ERROR: " + ex.Message, "Error Cargar Tabla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cargarTabla.CerrarConexionBD1();
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

        private void txtRazonBaneo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            CargarTabla();
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtBuscarEn.Text = "";
            cmbBuscarEn.Text = "id_baneo";
            lblIDUsuario.Text = "¿?";
            lblNombreUsuario.Text = "¿?";
            txtRazonBaneo.Text = "";
            lblFechaBaneo.Text = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));
            nudDias.Value = 1;
            lblIDBaneo.Text = "¿?";
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
            CargarTabla();
            LimpiarCampos();
        }

        private void dgbUsuariosBaneados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbUsuariosBaneados.Rows[e.RowIndex];
                lblIDBaneo.Text = Convert.ToString(fila.Cells["id_baneo"].Value);
                lblIDUsuario.Text = Convert.ToString(fila.Cells["id_usuario_baneado"].Value);
                lblNombreUsuario.Text = Convert.ToString(fila.Cells["nombre_usuario"].Value);
                txtRazonBaneo.Text = Convert.ToString(fila.Cells["razon_baneo"].Value);
                lblFechaBaneo.Text = Convert.ToString(fila.Cells["fecha"].Value);
                nudDias.Value = Convert.ToInt32(fila.Cells["dias_baneo"].Value);
                btnBuscarUsuario.Enabled = false;
            }
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            BaneoUsuariosBuscarUsuario abrirUsuarios = new BaneoUsuariosBuscarUsuario();
            abrirUsuarios.ShowDialog();
        }

        private void btnGuardarBaneo_Click(object sender, EventArgs e)
        {
            if (lblIDBaneo.Text != "¿?")
            {
                //Si es distinto el ID del baneo a Deconocido ¿? se actualiza el baneo
                if (lblIDUsuario.Text != "¿?" && txtRazonBaneo.Text != "" && nudDias.Value > 0)
                {
                    //Se envia mensaje para verificar la decision
                    DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea banear al usuario actual?, una vez ingresado no podra ser eliminado el baneo del usuario", "Baneo Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    //Se contesta que si
                    if (resultadoMensaje == DialogResult.Yes)
                    {
                        ComandosBDMySQL ActualizarBaneo = new ComandosBDMySQL();
                        try
                        {
                            ActualizarBaneo.AbrirConexionBD1();
                            ActualizarBaneo.IngresarConsulta1("call sbepa.actualizar_usuarios_baneados(" + lblIDBaneo.Text + ", '" + txtRazonBaneo.Text + "', " + nudDias.Value.ToString() + ");");
                            ActualizarBaneo.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Usuarios Baneados','Modificar','MODIFICO UN USUARIO BANEADO CON ID " + lblNombreUsuario.Text + ", CON LA INFORMACION NOMBRE: " + lblNombreUsuario.Text + " CON LA RAZON DE BANEO DE: " + txtRazonBaneo.Text + " CON LA CANTIDAD DE IDEAS DE: " + nudDias.Value.ToString() + "');");
                            ActualizarBaneo.CerrarConexionBD1();
                            MessageBox.Show("La actualizacion del baneo del usuario se realizo correctamente", "Actualizacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarTabla();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar actualizar el baneo del usuario ERROR:" + ex.Message + "", "Error actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            ActualizarBaneo.CerrarConexionBD1();
                        }
                    }  
                }
                else
                {
                    MessageBox.Show("Faltan Datos necesarios para banear a un usuario, revise que esta ingresado el ID del usuario a banear, la razon del baneo y el numero de dias del baneo","Faltan Datos Necesarios",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                //Si no hay ID de baneo (¿?) se guarda el baneo
                ComandosBDMySQL RegistrarBaneo = new ComandosBDMySQL();
                try
                {
                    RegistrarBaneo.AbrirConexionBD1();
                    RegistrarBaneo.IngresarConsulta1("call sbepa.insertar_usuarios_baneados('" + txtRazonBaneo.Text + "', '" + lblFechaBaneo.Text + "', " + nudDias.Value.ToString() + ", " + lblIDUsuario.Text + ");");
                    RegistrarBaneo.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Usuarios Baneados','Insertar','REGISTRO UN USUARIO BANEADO CON ID " + lblNombreUsuario.Text + ", CON LA INFORMACION NOMBRE: " + lblNombreUsuario.Text + " CON LA RAZON DE BANEO DE: " + txtRazonBaneo.Text + " CON LA CANTIDAD DE IDEAS DE: " + nudDias.Value.ToString() + "');");
                    MessageBox.Show("La registro del baneo del usuario se realizo correctamente", "Registro Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RegistrarBaneo.CerrarConexionBD1();
                    LimpiarCampos();
                    CargarTabla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar registra el baneo del usuario ERROR:" + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {

                }
            }
        }
    }
}
