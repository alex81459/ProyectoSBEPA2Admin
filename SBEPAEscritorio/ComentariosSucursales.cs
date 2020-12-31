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
    public partial class ComentariosSucursales : Form
    {
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        public ComentariosSucursales()
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

        private void ComentariosSucursales_Load(object sender, EventArgs e)
        {
            CargarSucursales();
        }

        private void CargarSucursales()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarSucursales = new ComandosBDMySQL();
            try
            {
                cargarSucursales.AbrirConexionBD1();
                dgbSucursal.DataSource = cargarSucursales.RellenarTabla1("SELECT * FROM sbepa.vista_sucursales;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la tabla de Sucursales ERROR: " + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarSucursales.CerrarConexionBD1();
            }
        }

        private void cmbBuscarEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se activa el textbox para realizar las busquedas
            txtBuscarEn.Visible = true;
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void cmbBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            //Se transforma la busqueda para que sea compatible con las columnas de la BD
            String BuscarEn;

            if (cmbBuscarEn.Text == "ID Sucursal")
            {
                BuscarEn = "idsucursal";
            }
            else if (cmbBuscarEn.Text == "ID Tienda Asociada")
            {
                BuscarEn = "id_tienda";
            }
            else if (cmbBuscarEn.Text == "Nombre Sucursal")
            {
                BuscarEn = "nombre";
            }
            else if (cmbBuscarEn.Text == "Direccion Tienda")
            {
                BuscarEn = "direccion";
            }
            else if (cmbBuscarEn.Text == "Coordenadas Tienda")
            {
                BuscarEn = "coordenadas";
            }
            else if (cmbBuscarEn.Text == "Telefono Tienda")
            {
                BuscarEn = "telefono";
            }
            else if (cmbBuscarEn.Text == "Descripcion Tienda")
            {
                BuscarEn = "descripcion";
            }
            else
            {
                BuscarEn = "horarios";
            }

            //se crea la instancia para buscar en la tabla, se carga el resultado en el datagridview, y siempre se cierra la conexion 
            ComandosBDMySQL buscarTabla = new ComandosBDMySQL();
            try
            {
                buscarTabla.AbrirConexionBD1();
                dgbSucursal.DataSource = buscarTabla.RellenarTabla1("SELECT  `idsucursal` AS `ID Sucursal`,`nombre` AS `Nombre`,`id_tienda` AS `Id Tienda`,`direccion` AS `Direccion`,`coordenadas` AS `Coordenadas`,`telefono` AS `Telefono`,`descripcion` AS `Descripcion`,`horarios` AS `Horarios`,`foto_sucursal` AS `Foto Sucursal` FROM`sucursales` Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%';");
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


        private void cmbBuscarEn_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Se activa el textbox para realizar las busquedas
            txtBuscarEn.Visible = true;
        }

        private void txtBuscarEn_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            //Se transforma la busqueda para que sea compatible con las columnas de la BD
            String BuscarEn;

            if (cmbBuscarEn.Text == "ID Sucursal")
            {
                BuscarEn = "idsucursal";
            }
            else if (cmbBuscarEn.Text == "ID Tienda Asociada")
            {
                BuscarEn = "id_tienda";
            }
            else if (cmbBuscarEn.Text == "Nombre Sucursal")
            {
                BuscarEn = "nombre";
            }
            else if (cmbBuscarEn.Text == "Direccion Tienda")
            {
                BuscarEn = "direccion";
            }
            else if (cmbBuscarEn.Text == "Coordenadas Tienda")
            {
                BuscarEn = "coordenadas";
            }
            else if (cmbBuscarEn.Text == "Telefono Tienda")
            {
                BuscarEn = "telefono";
            }
            else if (cmbBuscarEn.Text == "Descripcion Tienda")
            {
                BuscarEn = "descripcion";
            }
            else
            {
                BuscarEn = "horarios";
            }

            //se crea la instancia para buscar en la tabla, se carga el resultado en el datagridview, y siempre se cierra la conexion 
            ComandosBDMySQL buscarTabla = new ComandosBDMySQL();
            try
            {
                buscarTabla.AbrirConexionBD1();
                dgbSucursal.DataSource = buscarTabla.RellenarTabla1("SELECT  `idsucursal` AS `ID Sucursal`,`nombre` AS `Nombre`,`id_tienda` AS `Id Tienda`,`direccion` AS `Direccion`,`coordenadas` AS `Coordenadas`,`telefono` AS `Telefono`,`descripcion` AS `Descripcion`,`horarios` AS `Horarios`,`foto_sucursal` AS `Foto Sucursal` FROM`sucursales` Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%';");
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


        private void dgbSucursal_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgbSucursal.Rows[e.RowIndex];
                string IDSucursal = Convert.ToString(fila.Cells["IDSucursal"].Value);

                ComandosBDMySQL BuscarComentario = new ComandosBDMySQL();
                BuscarComentario.AbrirConexionBD1();
                dgbComentarios.DataSource = BuscarComentario.RellenarTabla1("call sbepa.buscar_comentarios_sucursales("+ IDSucursal + ");");
                BuscarComentario.CerrarConexionBD1();
            }
        }

        private void LimpiarCampos()
        {
            txtIDComentario.Text = "¿?";
            txtTituloComentario.Text = "";
            txtComentario.Text = "";
            txtPuntuacionComentario.Text = "¿?";
            txtIDUsuario.Text = "¿?";
            txtUsuario.Text = "¿?";
            txtNombreSucursal.Text = "¿?";
            txtFecha.Text = "¿?";
            txtBuscarEn.Text = "";
            cmbBuscarEn.Text = "ID Sucursal";
            DataTable dt = (DataTable)dgbComentarios.DataSource;
            dt.Clear();
        }

        private void dgbComentarios_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbComentarios.Rows[e.RowIndex];
                txtIDComentario.Text = Convert.ToString(fila.Cells["idcomentario_sucursal"].Value);
                txtTituloComentario.Text = Convert.ToString(fila.Cells["titulo"].Value);
                txtComentario.Text = Convert.ToString(fila.Cells["comentario"].Value);
                txtPuntuacionComentario.Text = Convert.ToString(fila.Cells["puntuacion_sucursal"].Value);
                txtIDUsuario.Text = Convert.ToString(fila.Cells["id_usuario"].Value);
                txtUsuario.Text = Convert.ToString(fila.Cells["nombre_usuario"].Value);
                txtNombreSucursal.Text = Convert.ToString(fila.Cells["nombre_sucursal"].Value);
                txtFecha.Text = Convert.ToString(fila.Cells["fecha_comentario"].Value);
            }
        }

        private void pbActualizar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarSucursales();
        }

        private void btnEliminarComentario_Click(object sender, EventArgs e)
        {
            if (txtIDComentario.Text != "¿?")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea eliminar el comentario seleccionado de la sucursal?, una vez borrado del sistema no podra ser recuperado", "Borrar Comentario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Se contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    ComandosBDMySQL borrarComenario = new ComandosBDMySQL();
                    try
                    {
                        borrarComenario.AbrirConexionBD1();
                        borrarComenario.IngresarConsulta1("call sbepa.eliminar_comentarios_sucursales(" + txtIDComentario.Text + ");");
                        borrarComenario.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Comentarios Sucursales','Eliminar', 'ELIMINO EL COMENTARIO CON EL ID: "+ txtIDComentario.Text+ " CON EL TITULO: "+ txtTituloComentario.Text+ " EL QUE DECIA: "+ txtComentario.Text+ " REALIZADO POR EL USUARIO: "+ txtUsuario.Text+ " A LA SUCURSAL: "+ txtNombreSucursal.Text+ "');");
                        MessageBox.Show("El comentario de la sucursal ha sido correctamente Eliminado del Sistema", "Comentario Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarSucursales();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar borrar el comentario de la sucursal ERROR: "+ex.Message+"","Error Borrar",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        borrarComenario.CerrarConexionBD1();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un comentario para eliminar del sistema", "No selecciono comentario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBanearUsuario_Click(object sender, EventArgs e)
        {
            if (txtIDComentario.Text != "¿?")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea banear el usuario porque el comentario que realizo?, sera enviado a la interfaz de Baneo de Usuario con los datos necesarios del mismo", "Banear Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Se contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    BaneoUsuarios abrirBanearUsuario = new BaneoUsuarios();
                    abrirBanearUsuario.lblIDUsuario.Text = txtIDUsuario.Text;
                    abrirBanearUsuario.lblNombreUsuario.Text = txtUsuario.Text;
                    abrirBanearUsuario.btnBuscarUsuario.Enabled = false;
                    abrirBanearUsuario.txtRazonBaneo.Text = "Realizo un comentario inapropiado en una sucursal porque: ";
                    abrirBanearUsuario.lblFechaBaneo.Text = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));
                    abrirBanearUsuario.Show();
                }   
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un comentario para poder banear al usuario que lo realizo","Falta selecionar comentario",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
