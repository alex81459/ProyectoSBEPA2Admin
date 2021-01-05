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
        private Boolean UPCRegistrado = false;
        private String PrecioOrignal = "";
        private String idSucursalProductoSeleccionada = "";


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

        private string Vacio = "";

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            CargarProductos();
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
                dgbProductos.DataSource = SiguientePagina.RellenarTabla1("SELECT * FROM sbepa2.cambioinfoproducto ORDER BY CambioInfoProducto DESC limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
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
            CargarProductos();
            HacerInvisiblesyLimpiarCampos();
        }

        private void CargarProductos()
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
                txtIDProducto.Text = Convert.ToString(fila.Cells["idProducto"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value);
                cmbProductoUnico.Text = Convert.ToString(fila.Cells["Unico"].Value);
                txtCodigoUPC.Text = Convert.ToString(fila.Cells["UPC"].Value);
                txtCantidadVisitasProducto.Text = Convert.ToString(fila.Cells["CantidadVisita"].Value);
                txtFechaRegistroProducto.Text = Convert.ToString(fila.Cells["FechaRegistro"].Value);
                txtIDSucursal.Text = Convert.ToString(fila.Cells["SucursalID"].Value);
                txtMarca.Text = Convert.ToString(fila.Cells["Marca"].Value);
                cmbEmvase.Text = Convert.ToString(fila.Cells["Envase"].Value);
                cmbUnidadMedida.Text = Convert.ToString(fila.Cells["UnidadMedida"].Value);
                NUDCantidadMedida.Text = Convert.ToString(fila.Cells["CantidadMedida"].Value);
                txtIDCategoriaSeleccionada.Text = Convert.ToString(fila.Cells["Id_subcategoria"].Value);
                txtDescripcionProducto.Text = Convert.ToString(fila.Cells["DescripcionProducto"].Value);

                btnEliminarProducto.Enabled = true;


                //Se extrae el nombre de la subcategoria, la fotografia y el precio del producto
                ComandosBDMySQL BuscarInfo = new ComandosBDMySQL();
                try
                {
                    BuscarInfo.AbrirConexionBD1();
                    txtNombreSubCategoria.Text = BuscarInfo.RellenarTabla1("SELECT Nombre FROM sbepa2.subcategoria where idSubCategoria = "+ txtIDCategoriaSeleccionada.Text+ ";").Rows[0]["Nombre"].ToString();
                    pbImageProducto.Image = BuscarInfo.ExtraerImagen("SELECT ImagenProducto FROM sbepa2.productos where idProducto = '" + txtIDProducto.Text + "';");

                    NUDPrecioProducto.Value = Convert.ToInt32(BuscarInfo.RellenarTabla1("SELECT PrecioCLP FROM sbepa2.preciosproductos where idSucursalProducto = '" + Convert.ToString(fila.Cells["idSucursalesProductos"].Value) + "' ORDER BY idPreciosProductos desc LIMIT 1;").Rows[0]["PrecioCLP"].ToString());

                    PrecioOrignal = "";
                    PrecioOrignal = BuscarInfo.RellenarTabla1("SELECT PrecioCLP FROM sbepa2.preciosproductos where idSucursalProducto = '" + Convert.ToString(fila.Cells["idSucursalesProductos"].Value) + "' ORDER BY idPreciosProductos desc LIMIT 1;").Rows[0]["PrecioCLP"].ToString();

                    idSucursalProductoSeleccionada = Convert.ToString(fila.Cells["idSucursalesProductos"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer los datos del producto, subCategoria, Fotografia o Precio asignados ERROR: " + ex.Message + "", "Error extraer Nombre de la Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarInfo.CerrarConexionBD1();
                }

                //Se desactivan los campos que no se pueden modificar
                if (cmbProductoUnico.Text == "SI" && txtNombre.Text != "")
                {
                    //Si es un producto Unico y con Nombre
                    ActivarProductoUnicoConNombre();
                }
                if (cmbProductoUnico.Text == "NO" && txtNombre.Text != "")
                {
                    //Si es un producto NO Unico y con UPC
                    ActivarProductoNOUnicoConUPC();
                }
            }
        }

        private void ActivarProductoUnicoConNombre()
        {
            btnBuscarSucursal.Enabled = false;
            txtMarca.Enabled = true;
            cmbEmvase.Enabled = true;
            cmbUnidadMedida.Enabled = true;
            NUDCantidadMedida.Enabled = true;
            btnBuscarCategoria.Enabled = true;
            txtDescripcionProducto.Enabled = true;
            NUDPrecioProducto.Enabled = true;
            btnGuardarProducto.Enabled = true;
            btnEliminarProducto.Enabled = true;
            btnNuevaProducto.Enabled = true;
            txtNombre.Enabled = true;
            cmbProductoUnico.Enabled = false;
            txtCodigoUPC.Enabled = false;
            btnCorroborarProducto.Enabled = false;
        }

        private void ActivarProductoNOUnicoConUPC()
        {
            btnBuscarSucursal.Enabled = false;
            txtMarca.Enabled = false;
            cmbEmvase.Enabled = false;
            cmbUnidadMedida.Enabled = false;
            NUDCantidadMedida.Enabled = false;
            btnBuscarCategoria.Enabled = false;
            txtDescripcionProducto.Enabled = false;
            NUDPrecioProducto.Enabled = true;
            btnGuardarProducto.Enabled = true;
            btnEliminarProducto.Enabled = true;
            btnNuevaProducto.Enabled = true;
            txtNombre.Enabled = false;
            cmbProductoUnico.Enabled = false;
            txtCodigoUPC.Enabled = false;
            btnCorroborarProducto.Enabled = false;
        }

        private void btnNuevaProducto_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarProductos();
        }

        public void LimpiarCampos()
        {
            txtCantidadVisitasProducto.Text = "";
            txtIDProducto.Text = "";
            txtNombre.Text = "";
            cmbProductoUnico.Text = "";
            txtCodigoUPC.Text = "";
            btnCorroborarProducto.Enabled = true;
            gbDatosEspecificos.Enabled = false;
            txtFechaRegistroProducto.Text = "";
            txtMarca.Text = "";
            cmbEmvase.Text = "";
            cmbUnidadMedida.Text = "";
            NUDCantidadMedida.Value = 1;
            txtIDCategoriaSeleccionada.Text = "";
            txtNombreSubCategoria.Text = "";
            txtDescripcionProducto.Text = "";
            NUDPrecioProducto.Value = 1;
            btnEliminarProducto.Enabled = false;
            UPCRegistrado = false;
            gbDatosBase.Enabled = true;
            cmbProductoUnico.Enabled = true;
            txtCodigoUPC.Enabled = true;
            btnGuardarProducto.Enabled = true;
            pbImageProducto.Image = null;
            txtIDSucursal.Text = "";
            UPCRegistrado = false;
            PrecioOrignal = "";
            idSucursalProductoSeleccionada = "";
            btnBuscarSucursal.Enabled = true;
            btnBuscarCategoria.Enabled = true;
            txtNombre.Enabled = true;
        }

        private Boolean VerificarDatosProducto()
        {
            //Se verifica que los campos requeridos tengan contenido
            if (txtNombre.Text != "" && txtIDSucursal.Text != "" && txtMarca.Text != "" && txtIDCategoriaSeleccionada.Text != "" && txtDescripcionProducto.Text !="" && pbImageProducto.Image != null)
            {
                if (cmbProductoUnico.Text == "SI")
                {
                    //Si el producto es unico
                    return true;
                }
                else
                {
                    if (txtCodigoUPC.Text != "")
                    {
                        //si esta el UPC
                        MessageBox.Show("Los datos del Producto a Registrar son Correctos","Datos OK",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                //Si a uno de los campos le falta contenido se muestra una advertencia
                MessageBox.Show("Cada Producto a registrar o modificar debe tener un Nombre, una Sucursal Seleccionada, una marca, una Sub Categoria, una Descripcion del mismo producto y una imagen de el, verifique que estos datos estan ingresados", "Error Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }


        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            //Se verifica que esten todos los datos correcto
            if (VerificarDatosProducto() == true)
            {
                if (txtIDProducto.Text == "")
                {
                    //SI el ID del producto esta vacio se registra
                    if (cmbProductoUnico.Text == "SI")
                    {
                        ComandosBDMySQL GuardarProducto = new ComandosBDMySQL();

                        //Se envia mensaje para verificar la decision
                        DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que guardara el producto con los datos ingresados, su sucursal y precio?, al ser un producto UNICO podra editarlo directamente nuevamente si comedio un error", "¿Guardar Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        //Si contesta que si
                        if (resultadoMensaje == DialogResult.Yes)
                        {
                            try
                            {
                                GuardarProducto.AbrirConexionBD1();
                                //Si el producto es Unico
                                GuardarProducto.IngresarImagen("call sbepa2.InsertarProducto('" + txtNombre.Text + "', '" + txtMarca.Text + "', '" + cmbEmvase.Text + "', '" + cmbUnidadMedida.Text + "', " + NUDCantidadMedida.Value.ToString() + ", " + txtIDCategoriaSeleccionada.Text + ", '" + txtDescripcionProducto.Text + "', @imagen, '" + cmbProductoUnico.Text + "', '" + txtCodigoUPC.Text + "');", pbImageProducto.Image);
                                //Se extrae el ID el producto registrado y se registra en la tabla intermedia
                                String IDProductoRegistrado = GuardarProducto.RellenarTabla1("SELECT idProducto FROM sbepa2.productos where Nombre = '" + txtNombre.Text + "' and Marca = '" + txtMarca.Text + "' and Envase= '" + cmbEmvase.Text + "' and DescripcionProducto = '" + txtDescripcionProducto.Text + "' order by idProducto desc LIMIT 1;").Rows[0]["idProducto"].ToString();
                                GuardarProducto.IngresarConsulta1("call sbepa2.InsertarSucursalesProducto(" + txtIDSucursal.Text + ", " + IDProductoRegistrado + ");");
                                //Se extraer el idSucursalProducto para poder registar el precio del producto
                                String IDSucursalProducto = GuardarProducto.RellenarTabla1("SELECT idSucursalesProductos FROM sbepa2.sucursalesproductos where SucursalID = '" + txtIDSucursal.Text + "' and ProductosID = '" + IDProductoRegistrado + "' order by idSucursalesProductos desc LIMIT 1;").Rows[0]["idSucursalesProductos"].ToString();
                                GuardarProducto.IngresarConsulta1("call sbepa2.InsertarPreciosProductos(" + NUDPrecioProducto.Value.ToString() + ", " + IDSucursalProducto + ");");
                                //Se registra el cambio
                                GuardarProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Productos', 'Insertar', 'REGISTRO EL PRODUCTO: CON EL NOMBRE" + txtNombre.Text + ", CON LA MARCA: " + txtMarca.Text + ", CON EL EMVASE: " + cmbEmvase.Text + ", CON LA UNIDAD DE MEDIDA: " + cmbUnidadMedida.Text + ", CON LA CANTIDAD DE MEDIDA: " + NUDCantidadMedida.Value.ToString() + ", CON EL ID DE SUB CATEGORIA: " + txtIDCategoriaSeleccionada.Text + ", CON LA DESCRIPCION: " + txtDescripcionProducto.Text + ", CON EL CODIGO DEL PRODUCTO: " + txtCodigoUPC.Text + ", EL CUAL FUE REGISTRADO CON EL ID DE PRODUCTO: " + IDProductoRegistrado + " CON EL ID DE SUCURSAL-PRODUCTO: " + IDSucursalProducto + ", CON EL PRECIO DE PRODUCTO: " + NUDPrecioProducto.Value.ToString() + "');");
                                MessageBox.Show("El producto con nombre: " + txtNombre.Text + " y con codigo UPC: " + txtCodigoUPC.Text + " a sido correctamente registrado", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                                CargarProductos();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al intentar registrar el porducto en el sistema ERROR: " + ex.Message + "", "Error Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            finally
                            {
                                GuardarProducto.CerrarConexionBD1();
                            }
                        }
                    }
                    else
                    {
                        ComandosBDMySQL GuardarProducto = new ComandosBDMySQL();

                        //Se envia mensaje para verificar la decision
                        DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que guardara el producto con los datos ingresados, su sucursal y precio?, al ser un producto NO UNICO no podra editarlo directamente si comedio un error, debe ir a la seccion Cambio Info Producto en el Menu", "¿Guardar Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (cmbProductoUnico.Text == "NO" && UPCRegistrado == false)
                        {
                            //Si contesta que si
                            if (resultadoMensaje == DialogResult.Yes)
                            {
                                try
                                {
                                    GuardarProducto.AbrirConexionBD1();
                                    //Si el producto no es unico y no se encuentra registrado, se almacenan todos sus datos: producto, Sucursales-Productos y precio
                                    GuardarProducto.IngresarImagen("call sbepa2.InsertarProducto('" + txtNombre.Text + "', '" + txtMarca.Text + "', '" + cmbEmvase.Text + "', '" + cmbUnidadMedida.Text + "', " + NUDCantidadMedida.Value.ToString() + ", " + txtIDCategoriaSeleccionada.Text + ", '" + txtDescripcionProducto.Text + "', @imagen, '" + cmbProductoUnico.Text + "', '" + txtCodigoUPC.Text + "');", pbImageProducto.Image);
                                    //Se extrae el ID el producto registrado y se registra en la tabla intermedia
                                    String IDProductoRegistrado = GuardarProducto.RellenarTabla1("SELECT idProducto FROM sbepa2.productos where Nombre = '" + txtNombre.Text + "' and Marca = '" + txtMarca.Text + "' and Envase= '" + cmbEmvase.Text + "' and DescripcionProducto = '" + txtDescripcionProducto.Text + "' order by idProducto desc LIMIT 1;").Rows[0]["idProducto"].ToString();
                                    GuardarProducto.IngresarConsulta1("call sbepa2.InsertarSucursalesProducto(" + txtIDSucursal.Text + ", " + IDProductoRegistrado + ");");
                                    //Se extraer el idSucursalProducto para poder registar el precio del producto
                                    String IDSucursalProducto = GuardarProducto.RellenarTabla1("SELECT idSucursalesProductos FROM sbepa2.sucursalesproductos where SucursalID = '" + txtIDSucursal.Text + "' and ProductosID = '" + IDProductoRegistrado + "' order by idSucursalesProductos desc LIMIT 1;").Rows[0]["idSucursalesProductos"].ToString();
                                    GuardarProducto.IngresarConsulta1("call sbepa2.InsertarPreciosProductos(" + NUDPrecioProducto.Value.ToString() + ", " + IDSucursalProducto + ");");
                                    //Se registra el cambio
                                    GuardarProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Productos', 'Insertar', 'REGISTRO EL PRODUCTO CON UPC NO REGISTRADO CON EL NOMBRE: " + txtNombre.Text + ", CON LA MARCA: " + txtMarca.Text + ", CON EL EMVASE: " + cmbEmvase.Text + ", CON LA UNIDAD DE MEDIDA: " + cmbUnidadMedida.Text + ", CON LA CANTIDAD DE MEDIDA: " + NUDCantidadMedida.Value.ToString() + ", CON EL ID DE SUB CATEGORIA: " + txtIDCategoriaSeleccionada.Text + ", CON LA DESCRIPCION: " + txtDescripcionProducto.Text + ", CON EL CODIGO DEL PRODUCTO: " + txtCodigoUPC.Text + ", EL CUAL FUE REGISTRADO CON EL ID DE PRODUCTO: " + IDProductoRegistrado + " CON EL ID DE SUCURSAL-PRODUCTO: " + IDSucursalProducto + ", CON EL PRECIO DE PRODUCTO: " + NUDPrecioProducto.Value.ToString() + "');");
                                    MessageBox.Show("El producto con nombre: " + txtNombre.Text + " y con codigo UPC: " + txtCodigoUPC.Text + " a sido correctamente registrado", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LimpiarCampos();
                                    CargarProductos();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al intentar Registar el nuevo producto Unico sin UPC Registrado en el Sistema ERROR: "+ex.Message+"","Error al registrar nuevo producto",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                }
                                finally
                                {
                                    GuardarProducto.CerrarConexionBD1();
                                }
                            }
                        }
                    } 
                }
                else
                {
                    ComandosBDMySQL ActualizarProducto = new ComandosBDMySQL();

                    //Si el ID el producto existe se modifica sus datos, dependiendo del tipo
                    if (cmbProductoUnico.Text == "SI" && txtIDProducto.Text !="")
                    {
                        //Se envia mensaje para verificar la decision
                        DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que Actualizara el producto con los datos ingresados y su precio?, al ser un producto UNICO podra editarlo directamente nuevamente si comedio un error", "¿Actualizar Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        //Si contesta que si
                        if (resultadoMensaje == DialogResult.Yes)
                        {
                            try
                            {
                                //Si el producto es unico, se permite modificar los datos del producto y se registra un nuevo precio, pero la sucursal no se toca
                                ActualizarProducto.AbrirConexionBD1();
                                //Se actualizan los datos del producto
                                ActualizarProducto.IngresarImagen("call sbepa2.ActualizarProducto(" + txtIDProducto.Text + ", '" + txtNombre.Text + "', '" + txtMarca.Text + "', '" + cmbEmvase.Text + "', '" + cmbUnidadMedida.Text + "', " + NUDCantidadMedida.Value.ToString() + ", " + txtIDCategoriaSeleccionada.Text + ",  @imagen, '" + txtDescripcionProducto.Text + "', '" + cmbProductoUnico.Text + "', '" + txtCodigoUPC.Text + "');", pbImageProducto.Image);

                                //Se registra el nuevo precio del producto si es diferente al orignal
                                if (PrecioOrignal != NUDPrecioProducto.Value.ToString())
                                {
                                    //Se extraer el idSucursalProducto para poder registar el precio del producto
                                    String IDSucursalProducto = ActualizarProducto.RellenarTabla1("SELECT idSucursalesProductos FROM sbepa2.sucursalesproductos where SucursalID = '" + txtIDSucursal.Text + "' and ProductosID = '" + txtIDProducto.Text + "' order by idSucursalesProductos desc LIMIT 1;").Rows[0]["idSucursalesProductos"].ToString();
                                    //Se registra el nuevo precio
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarPreciosProductos(" + NUDPrecioProducto.Value.ToString() + ", " + IDSucursalProducto + ");");
                                    //Se registra el cambio
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Productos', 'Actualizar', 'ACTUALIZO EL PRODUCTO UNICO CON EL NOMBRE: " + txtNombre.Text + ", CON LA MARCA: " + txtMarca.Text + ", CON EL EMVASE: " + cmbEmvase.Text + ", CON LA UNIDAD DE MEDIDA: " + cmbUnidadMedida.Text + ", CON LA CANTIDAD DE MEDIDA: " + NUDCantidadMedida.Value.ToString() + ", CON EL ID DE SUB CATEGORIA: " + txtIDCategoriaSeleccionada.Text + ", CON LA DESCRIPCION: " + txtDescripcionProducto.Text + ", CON EL CODIGO DEL PRODUCTO: " + txtCodigoUPC.Text + ", EL CUAL FUE ACTUALIZADO CON EL ID DE PRODUCTO: " + txtIDProducto.Text + " SIN MODIFICAR LA SUCURSAL QUE TENIA ASIGNADA, CON EL NUEVO PRECIO DE PRODUCTO: " + NUDPrecioProducto.Value.ToString() + "');");
                                    MessageBox.Show("El producto con nombre: " + txtNombre.Text + " y con codigo UPC: " + txtCodigoUPC.Text + " a sido correctamente Actualizado", "Actualizacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LimpiarCampos();
                                    CargarProductos();
                                }
                                else
                                {
                                    //Solo se registra el cambio de datos del producto, pero el precio no
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Productos', 'Actualizar', 'ACTUALIZO EL PRODUCTO UNICO CON EL NOMBRE: " + txtNombre.Text + ", CON LA MARCA: " + txtMarca.Text + ", CON EL EMVASE: " + cmbEmvase.Text + ", CON LA UNIDAD DE MEDIDA: " + cmbUnidadMedida.Text + ", CON LA CANTIDAD DE MEDIDA: " + NUDCantidadMedida.Value.ToString() + ", CON EL ID DE SUB CATEGORIA: " + txtIDCategoriaSeleccionada.Text + ", CON LA DESCRIPCION: " + txtDescripcionProducto.Text + ", CON EL CODIGO DEL PRODUCTO: " + txtCodigoUPC.Text + ", EL CUAL FUE ACTUALIZADO CON EL ID DE PRODUCTO: " + txtIDProducto.Text + " SIN MODIFICAR LA SUCURSAL QUE TENIA ASIGNADA, SIN CAMBIAR EL PRECIO DEL PRODUCTO');");
                                    MessageBox.Show("El precio no ha sido actualizado, ya que no a sufrido cambios", "Precio no Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show("El producto con nombre: " + txtNombre.Text + " y con codigo UPC: " + txtCodigoUPC.Text + " a sido correctamente Actualizado", "Actualizacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LimpiarCampos();
                                    CargarProductos();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al intentar Actual9zar los datos del producto Unico: " + txtNombre.Text + " ERROR: " + ex.Message + "", "Error Actualizar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            finally
                            {
                                ActualizarProducto.CerrarConexionBD1();
                            }
                        }
                    } 

                    if (cmbProductoUnico.Text == "NO" && txtIDProducto.Text != "")
                    {
                        //Si el producto esta registrado pero la sucursal-producto y el precio no
                        if (txtIDProducto.Text != "" && btnBuscarSucursal.Enabled == true)
                        {
                            //Se envia mensaje para verificar la decision
                            DialogResult resultadoMensajes = MessageBox.Show("¿Esta seguro que añadira el producto actual a la sucursal seleccionada y precio con el precio ingresado?, al ser un producto NO UNICO no podra editarlo directamente nuevamente si comedio un error", "¿Guardar Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            //Si contesta que si
                            if (resultadoMensajes == DialogResult.Yes)
                            {
                                try
                                {
                                    //Si el producto no es unico y se encuentra registrado, no se almacenan los datos del producto
                                    ActualizarProducto.AbrirConexionBD1();
                                    //se guarda la Union SucursalProducto
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarSucursalesProducto(" + txtIDSucursal.Text + ", " + txtIDProducto.Text + ");");
                                    //Se extraer el idSucursalProducto para poder registar el precio del producto
                                    String IDSucursalProducto = ActualizarProducto.RellenarTabla1("SELECT idSucursalesProductos FROM sbepa2.sucursalesproductos where SucursalID = '" + txtIDSucursal.Text + "' and ProductosID = '" + txtIDProducto.Text + "' order by idSucursalesProductos desc LIMIT 1;").Rows[0]["idSucursalesProductos"].ToString();
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarPreciosProductos(" + NUDPrecioProducto.Value.ToString() + ", " + IDSucursalProducto + ");");
                                    //Se registra el cambio
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Productos', 'Insertar', 'REGISTRO EL PRODUCTO NO UNICO YA EXISTENTE EL UPC CON EL NOMBRE" + txtNombre.Text + ", CON LA MARCA: " + txtMarca.Text + ", CON EL EMVASE: " + cmbEmvase.Text + ", CON LA UNIDAD DE MEDIDA: " + cmbUnidadMedida.Text + ", CON LA CANTIDAD DE MEDIDA: " + NUDCantidadMedida.Value.ToString() + ", CON EL ID DE SUB CATEGORIA: " + txtIDCategoriaSeleccionada.Text + ", CON LA DESCRIPCION: " + txtDescripcionProducto.Text + ", CON EL CODIGO DEL PRODUCTO: " + txtCodigoUPC.Text + ", EL CUAL FUE REGISTRADO CON EL ID DE PRODUCTO: " + txtIDProducto.Text + " CON EL ID DE SUCURSAL-PRODUCTO: " + IDSucursalProducto + ", CON EL PRECIO DE PRODUCTO: " + NUDPrecioProducto.Value.ToString() + " EL CUAL YA ESTABA REGISTRADO AL SISTEMA PERO LO ASIGNO A UNA NUEVA SUCURSAL CON UN NUEVO PRECIO');");
                                    MessageBox.Show("El producto con nombre: " + txtNombre.Text + " y con codigo UPC: " + txtCodigoUPC.Text + " a sido correctamente registrado", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al intentar Guardar el producto No Unico con UPC Registrado en el Sistema ERROR: "+ex.Message+"","Errir Guardar Producto",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                }
                                finally
                                {
                                    ActualizarProducto.CerrarConexionBD1();
                                }
                                
                            }
                        }

                        //Si el producto NO es unico y esta registrado, se permite modificar solamente el precio
                        //Si el precio es distinto al original
                        //Se envia mensaje para verificar la decision
                        DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que actualizara el precio del producto seleccionado?, cada cambio que realize en el precio del producto quedara registrado", "¿Guardar Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        //Si contesta que si
                        if (resultadoMensaje == DialogResult.Yes)
                        {
                            try
                            {
                                if (PrecioOrignal != NUDPrecioProducto.Value.ToString() && btnBuscarSucursal.Enabled == false)
                                {
                                    ActualizarProducto.AbrirConexionBD1();
                                    //Se registra la Union de precio producto
                                    String IDSucursalProducto = ActualizarProducto.RellenarTabla1("SELECT idSucursalesProductos FROM sbepa2.sucursalesproductos where SucursalID = '" + txtIDSucursal.Text + "' and ProductosID = '" + txtIDProducto.Text + "' order by idSucursalesProductos desc LIMIT 1;").Rows[0]["idSucursalesProductos"].ToString();
                                    //Se registra el nuevo precio
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarPreciosProductos(" + NUDPrecioProducto.Value.ToString() + ", " + IDSucursalProducto + ");");
                                    //Se registra el cambio
                                    ActualizarProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Productos', 'Actualizar', 'ACTUALIZO EL PRODUCTO NO UNICO CON UPC CON EL NOMBRE: " + txtNombre.Text + ", CON EL ID DE SUB CATEGORIA: " + txtIDCategoriaSeleccionada.Text + ", CON EL CODIGO DEL PRODUCTO: " + txtCodigoUPC.Text + ", EL CUAL FUE ACTUALIZADO CON EL ID DE PRODUCTO: " + txtIDProducto.Text + " SIN MODIFICAR LA SUCURSAL QUE TENIA ASIGNADA, CON EL NUEVO PRECIO DE PRODUCTO ES: " + NUDPrecioProducto.Value.ToString() + "');");
                                    MessageBox.Show("El producto con nombre: " + txtNombre.Text + " y con codigo UPC: " + txtCodigoUPC.Text + " ha sido correctamente actualizado su precio", "Actualizacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                else if (PrecioOrignal == NUDPrecioProducto.Value.ToString() && btnBuscarSucursal.Enabled == false)
                                {
                                    //Si el precio es igual no se hace nada
                                    MessageBox.Show("El Producto no ha podido ser actualizado, ya que solamente se puede cambiar su precio, al estar registrado como NO UNICO y CON UPC", "No Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al intentar actualizar el precio del producto No Unico con Nombre : " + txtNombre.Text + ",  ERROR: " + ex.Message + "", "Error Actualizar Precio Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            finally
                            {
                                ActualizarProducto.CerrarConexionBD1();
                            }
                        }
                        LimpiarCampos();
                        CargarProductos();
                    }
                }
            }
        }

        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            //Se crea la instancia y se abre el editor de imagenes
            EditorImagen abrirEditorImagen = new EditorImagen();
            if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
            {
                //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                pbImageProducto.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
            }
        }

        private void btnCorroborarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductoUnico.Text !="")
            {
                if (cmbProductoUnico.Text == "SI")
                {
                    if (txtNombre.Text != "")
                    {
                        //se revisa si el nombre del producto unico existe en sistema
                        ComandosBDMySQL VerificarProducto = new ComandosBDMySQL();
                        VerificarProducto.AbrirConexionBD1();
                        Boolean ProductoRegistrado = VerificarProducto.VerificarExistenciaDato1("SELECT Nombre FROM sbepa2.productos where Nombre = '" + txtNombre.Text + "';");
                        if (ProductoRegistrado == true)
                        {
                            MessageBox.Show("Ya existe un producto registrado con este nombre, pero al ser unico lo puede registrar igualmente, SE RECOMIENDA USAR UN NOMBRE MAS AUTENTICO, por ejemplo agregarle el nombre de su tienda al final", "Nombre Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gbDatosEspecificos.Enabled = true;
                            gbDatosBase.Enabled = false;
                            txtMarca.Text = "Propia";
                        }
                        else
                        {
                            MessageBox.Show("NO existe un producto registrado con este nombre, el nombre es autentico", "Nombre Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gbDatosEspecificos.Enabled = true;
                            gbDatosBase.Enabled = false;
                            txtMarca.Text = "Propia";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe INGRESAR EL NOMBRE DEL PRODUCTO unico para ser verificado", "Falta Nombre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (cmbProductoUnico.Text == "NO")
                {
                    txtCodigoUPC.Enabled = true;
                    if (txtCodigoUPC.Text != "" && txtNombre.Text != "")
                    {
                        txtMarca.Enabled = true;
                        cmbEmvase.Enabled = true;
                        cmbUnidadMedida.Enabled = true;
                        NUDCantidadMedida.Enabled = true;
                        btnBuscarCategoria.Enabled = true;
                        txtNombreSubCategoria.Enabled = true;
                        txtDescripcionProducto.Enabled = true;
                        NUDPrecioProducto.Enabled = true;

                        //se revisa si el nombre del producto NO Unico existe en sistema
                        ComandosBDMySQL VerificarProducto = new ComandosBDMySQL();
                        VerificarProducto.AbrirConexionBD1();
                        Boolean ProductoRegistrado = VerificarProducto.VerificarExistenciaDato1("SELECT idProducto FROM sbepa2.productos where UPC = '" + txtCodigoUPC.Text + "';");
                        if (ProductoRegistrado == true)
                        {
                            MessageBox.Show("Ya existe un producto NO UNICO registrado con este nombre, se cargaran sus datos de forma automática, si considera que falta información del producto debe ser actualizada o modificada, debe dirigirse al menú principal y buscarlo en la sección ‘Cambios Info Producto’ para actualizar su información, la cual podrá ser aprobada por los administradores", "Nombre Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gbDatosEspecificos.Enabled = true;
                            UPCRegistrado = true;

                            //Se cargan los datos registrados del producto
                            DataTable DatosProductoNoUnico = new DataTable();
                            DatosProductoNoUnico = VerificarProducto.RellenarTabla1("SELECT * FROM sbepa2.productos where UPC = '" + txtCodigoUPC.Text + "';");
                            txtIDProducto.Text = DatosProductoNoUnico.Rows[0]["idProducto"].ToString();
                            txtNombre.Text = DatosProductoNoUnico.Rows[0]["Nombre"].ToString();
                            cmbProductoUnico.Text = "NO";
                            txtCodigoUPC.Text = DatosProductoNoUnico.Rows[0]["UPC"].ToString();
                            txtFechaRegistroProducto.Text = DatosProductoNoUnico.Rows[0]["FechaRegistro"].ToString();
                            txtMarca.Text = DatosProductoNoUnico.Rows[0]["Marca"].ToString();
                            cmbEmvase.Text = DatosProductoNoUnico.Rows[0]["Envase"].ToString();
                            cmbUnidadMedida.Text = DatosProductoNoUnico.Rows[0]["UnidadMedida"].ToString();
                            NUDCantidadMedida.Value = Convert.ToInt32(DatosProductoNoUnico.Rows[0]["CantidadMedida"].ToString());
                            txtIDCategoriaSeleccionada.Text = DatosProductoNoUnico.Rows[0]["Id_subcategoria"].ToString();
                            txtDescripcionProducto.Text = DatosProductoNoUnico.Rows[0]["DescripcionProducto"].ToString();

                            //Se extrae el nombre de la categoria
                            txtNombreSubCategoria.Text = VerificarProducto.RellenarTabla1("SELECT Nombre FROM sbepa2.subcategoria where idSubCategoria = '" + txtIDCategoriaSeleccionada.Text + "';").Rows[0]["Nombre"].ToString();

                            //Se extrae la foto del producto
                            try
                            {
                                VerificarProducto.AbrirConexionBD1();
                                DataTable VerificarLogo = new DataTable();
                                VerificarLogo = VerificarProducto.RellenarTabla1("SELECT ImagenProducto FROM sbepa2.productos where idProducto = '" + txtIDProducto.Text + "';");

                                if (VerificarLogo.Rows[0][0].ToString() != "")
                                {
                                    //Si la imagen del producto esta guardada
                                    pbImageProducto.Image = VerificarProducto.ExtraerImagen("SELECT ImagenProducto FROM sbepa2.productos where idProducto = '" + txtIDProducto.Text + "';");
                                }
                                else
                                {
                                    //Si la imagen del producto no esta guardado
                                    MessageBox.Show("El Producto Registrado con el nombre " + txtNombre.Text + " NO TIENE UNA IMAGEN ALMACENADA", "Imagen no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                VerificarProducto.CerrarConexionBD1();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("No se puedo Extraer la Imagen del Producto ERROR:" + ex.Message, "Error Cargar Imagen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            finally
                            {
                                VerificarProducto.CerrarConexionBD1();
                            }
                            gbDatosBase.Enabled = false;
                            btnBuscarSucursal.Enabled = true;
                            txtMarca.Enabled = false;
                            cmbEmvase.Enabled = false;
                            cmbUnidadMedida.Enabled = false;
                            NUDCantidadMedida.Enabled = false;
                            btnBuscarCategoria.Enabled = false;
                            txtNombreSubCategoria.Enabled = false;
                            txtDescripcionProducto.Enabled = false;
                            NUDPrecioProducto.Enabled = true;
                            txtIDSucursal.Text = "";
                            txtNombre.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("NO existe un producto registrado con este nombre, el nombre es autentico", "Nombre Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gbDatosEspecificos.Enabled = true;
                            UPCRegistrado = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar el Código Universal de Producto (UPC) y el Nombre del Producto", "Falta UPC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar si el Producto es Unico o no en el cuadro '¿Producto Unico?'","Falta seleccion",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void cmbProductoUnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductoUnico.Text == "SI")
            {
                txtCodigoUPC.Text = "Es Unico";
                txtCodigoUPC.Enabled = false;
            }
            else
            {
                txtCodigoUPC.Enabled = true;
                txtCodigoUPC.Text = "";
                txtNombre.Enabled = true;
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            ProductosBuscarCategoria abrirbuscar = new ProductosBuscarCategoria();
            abrirbuscar.ShowDialog();
        }

        private void btnBuscarSucursal_Click(object sender, EventArgs e)
        {
            ProductosBuscarSucursal abrirBuscar = new ProductosBuscarSucursal();
            abrirBuscar.ShowDialog();
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

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (txtIDProducto.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("ADVERTENCIA Borrara el producto selecionado con Nombre: "+txtNombre.Text+"", "¿Borrar Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Si contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                        //Si la clave es correcta se procede a eliminar el producto
                        ComandosBDMySQL EliminarProducto = new ComandosBDMySQL();
                        //Si es un producto Unico se elimina el producto, sucursal-producto y el precio
                        if (cmbProductoUnico.Text == "SI")
                        {
                            try
                            {
                                EliminarProducto.AbrirConexionBD1();
                                //Se elimina el precio de la Union Sucursal-Producto
                                EliminarProducto.IngresarConsulta1("call sbepa2.EliminarPreciosProductos(" + idSucursalProductoSeleccionada + ");");
                                //se elimina la union con sucursal-producto
                                EliminarProducto.IngresarConsulta1("call sbepa2.EliminarSucursalesProductos(" + txtIDSucursal.Text + ", " + txtIDProducto.Text + ");");
                                //Se elimina el producto
                                EliminarProducto.IngresarConsulta1("call sbepa2.EliminarProducto(" + txtIDProducto.Text + ");");
                                MessageBox.Show("El Producto con el Nombre: " + txtNombre.Text + ", con el UPC: " + txtCodigoUPC.Text + ", ha sido correctamente borrado del sistema, se borro todos sus precios de productos, su conexion con la sucursal y los datos del producto", "Producto Unico Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarCampos();
                                CargarProductos();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al intentar eliminar el Producto Unico con el Nombre: " + txtNombre.Text + " ERROR: " + ex.Message + "", "Error al borrar producto Unico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            {
                                EliminarProducto.CerrarConexionBD1();
                            }
                        }
                        //Si es un producto No unico, se elimina la sucursal-producto y el precio del producto 
                        if (cmbProductoUnico.Text == "NO")
                        {
                            try
                            {
                                EliminarProducto.AbrirConexionBD1();
                                //Se elimina el precio de la Union Sucursal-Producto
                                EliminarProducto.IngresarConsulta1("call sbepa2.EliminarPreciosProductos(" + idSucursalProductoSeleccionada + ");");
                                //se elimina la union con sucursal-producto
                                EliminarProducto.IngresarConsulta1("call sbepa2.EliminarSucursalesProductos(" + txtIDSucursal.Text + ", " + txtIDProducto.Text + ");");
                                MessageBox.Show("El Producto con el Nombre: " + txtNombre.Text + ", con el UPC: " + txtCodigoUPC.Text + ", ha sido correctamente borrado del sistema, se borro todos sus precios de productos, su conexion con la sucursal y Pero sus datos se mantiene ya que es un producto NO UNICO");
                                LimpiarCampos();
                                CargarProductos();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al intentar eliminar el Prodcuto No Unico con el Nombre: " + txtNombre.Text + " ERROR:" + ex.Message + "", "Error al borrar producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            finally
                            {
                                EliminarProducto.CerrarConexionBD1();
                            }
                        }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un Producto a Eliminar de la Tabla","Producto no seleccionado",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
    }
}
