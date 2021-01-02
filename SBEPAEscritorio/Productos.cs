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
    public partial class Productos : Form
    {
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public Productos()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbID, "El ID (Identificador) el cual se usara para identificar las diferentes Productos Registrados" + System.Environment.NewLine + "este se genera automático cada vez que se hace un registro de una sucursal" + System.Environment.NewLine + "NO PUEDE SER CAMBIADO POR MANUALMENTE");
            ttmensaje.SetToolTip(pbNombreProducto, "El Nombre con el cual será identificado el producto a registrar " + System.Environment.NewLine + "  cada producto debe tener un nombre único que los diferencie");
            ttmensaje.SetToolTip(pbSucursal, "La Interfaz donde se seleccionara la sucursales a la" + System.Environment.NewLine + " cual le producto pertenecerá");
            ttmensaje.SetToolTip(pbMarca, "La marca que fabrica o produce el producto en cuestión o si es artesanal");
            ttmensaje.SetToolTip(pbEmbase, "El tipo de embace en el cual viene almacenado el producto para su venta");
            ttmensaje.SetToolTip(pbUnidadMedida, "La Unidad de Medida que utiliza el producto para establecer" + System.Environment.NewLine + " la cantidad de contenido que tiene");
            ttmensaje.SetToolTip(pbCantidadMedida, "La cantidad de contenido que tiene el producto según la " + System.Environment.NewLine + "unidad de medida seleccionada anteriormente");
            ttmensaje.SetToolTip(pbDescripcionProducto, "Una Descripción del producto de máximo 300 caracteres");
            ttmensaje.SetToolTip(pbCodigoProducto, "El Código Universal de Producto (UPC) es una simbología de código de barras" + System.Environment.NewLine + " que se utiliza ampliamente para identificar de forma única el producto");
            ttmensaje.SetToolTip(pbFechaRegistro, "La Fecha cuando el producto fue registrado por primera vez en el sistema" + System.Environment.NewLine + " esta se genera de forma automática cuando se registra por primera vez");
            ttmensaje.SetToolTip(pbSeleccioneCategoria, "La Interfaz donde se seleccionara la categoría del producto");
            ttmensaje.SetToolTip(pbProductoUnico, "Si el producto es unico, creado artesanalmente o NO es unico, fabricado y registrado con un codigo UPC");
            ttmensaje.SetToolTip(NUDPrecioProducto, "El precio con el cual se registrara el producto, se registrara cada vez que se cambie");
        }


        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            CargarInfoLoginUsuarios();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresSoloNumeros(e);
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtDescripcionProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtBuscarEn_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
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
                    //Se cargan los datos necesarios para la busquedam y el ordenamiento de las paginas
                    BuscarRegistros.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarRegistros.RellenarTabla1("SELECT COUNT(idProducto) FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';").Rows[0][0].ToString());
                    txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbProductos.DataSource = BuscarRegistros.RellenarTabla2("call sbepa2.BuscarProductos('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
                    lblRegistrosEncontrados.Visible = true;
                    txtRegistrosEncontradosSuperior.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar buscar los productos registrados en el sistema ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            label1.Visible = true;
        }

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbProductos.DataSource = SiguientePagina.RellenarTabla1("SELECT * FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema detectado al intentar cargar los datos de los productos registrados en el sistema ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                SiguientePagina.CerrarConexionBD1();
            }
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarInfoLoginUsuarios();
            HacerInvisiblesyLimpiarCampos();
        }

        private void CargarInfoLoginUsuarios()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarRegistros = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarRegistros.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarRegistros.RellenarTabla1("SELECT COUNT(idProducto) FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID;").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                cargarRegistros.AbrirConexionBD1();
                dgbProductos.DataSource = cargarRegistros.RellenarTabla1("SELECT * FROM sbepa2.vistaproductos;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la tabla de los productos del sistema ERROR: " + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void dgbProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se activa el GroupBox de los datos especificos del productos porque ya esta registrado
                gbDatosEspecificos.Enabled = true;
                cmbProductoUnico.Enabled = false;

                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbProductos.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["idProducto"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value);
                cmbProductoUnico.Text = Convert.ToString(fila.Cells["Unico"].Value);
                txtCodigoProducto.Text = Convert.ToString(fila.Cells["UPC"].Value);
                txtCantidadVisitasProducto.Text = Convert.ToString(fila.Cells["CantidadVisita"].Value);
                txtFechaRegistroProducto.Text = Convert.ToString(fila.Cells["FechaRegistro"].Value);
                txtIDSucursal.Text = Convert.ToString(fila.Cells["SucursalID"].Value);
                txtMarca.Text = Convert.ToString(fila.Cells["Marca"].Value);
                cmbEmbase.Text = Convert.ToString(fila.Cells["Envase"].Value);
                cmbUnidadMedida.Text = Convert.ToString(fila.Cells["UnidadMedida"].Value);
                NUDCantidadMedida.Text = Convert.ToString(fila.Cells["CantidadMedida"].Value);
                txtIDCategoriaSeleccionada.Text = Convert.ToString(fila.Cells["Id_subcategoria"].Value);
                txtDescripcionProducto.Text = Convert.ToString(fila.Cells["DescripcionProducto"].Value);

                /*
                //Se extrae el rut del usuario
                ComandosBDMySQL BuscarInfo = new ComandosBDMySQL();
                try
                {
                    BuscarInfo.AbrirConexionBD1();
                    txtRUTAdmin.Text = BuscarInfo.RellenarTabla1("select RutUsuario from usuarios INNER JOIN credencialesusuarios ON usuarios.Id_usuario= credencialesusuarios.idUsuario INNER JOIN registrologinusuarios ON credencialesusuarios.idCredencialesUsuarios = registrologinusuarios.idCredencialUsuario where registrologinusuarios.idRegistroLoginUsuarios = " + txtIDAdmin.Text + ";").Rows[0]["RuUsuario"].ToString();
                    txtIDAdmin.Text = BuscarInfo.RellenarTabla1("select Id_usuario from usuarios INNER JOIN credencialesusuarios ON usuarios.Id_usuario= credencialesusuarios.idUsuario INNER JOIN registrologinusuarios ON credencialesusuarios.idCredencialesUsuarios = registrologinusuarios.idCredencialUsuario where registrologinusuarios.idRegistroLoginUsuarios = " + txtIDAdmin.Text + ";").Rows[0]["Id_usuario"].ToString();
                    txtNombreUsuarioCredencial.Text = BuscarInfo.RellenarTabla1("select NombreUsuario from credencialesusuarios INNER JOIN registrologinusuarios ON credencialesusuarios.idCredencialesUsuarios = registrologinusuarios.idCredencialUsuario where registrologinusuarios.idRegistroLoginUsuarios = " + txtIDRegistro.Text + ";").Rows[0]["NombreUsuario"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer el Rut del Usuario de los registros de cambios ERROR: " + ex.Message + "", "Error extraer RUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarInfo.CerrarConexionBD1();
                }
                */
            }
        }

        private void btnNuevaProducto_Click(object sender, EventArgs e)
        {
            
        }
    }
}
