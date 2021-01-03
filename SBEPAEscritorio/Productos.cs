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

        Boolean ProductoNoUnicoRegistrado = false;

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
                dgbProductos.DataSource = SiguientePagina.RellenarTabla1("SELECT * FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID ORDER BY productos.idProducto DESC limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
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
                txtID.Text = Convert.ToString(fila.Cells["idProducto"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value);
                cmbProductoUnico.Text = Convert.ToString(fila.Cells["Unico"].Value);
                txtCodigoProducto.Text = Convert.ToString(fila.Cells["UPC"].Value);
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

                //Se extrae el nombre de la subcategoria, la fotografia y el precio
                ComandosBDMySQL BuscarInfo = new ComandosBDMySQL();
                try
                {
                    BuscarInfo.AbrirConexionBD1();
                    txtNombreSubCategoria.Text = BuscarInfo.RellenarTabla1("SELECT Nombre FROM sbepa2.subcategoria where idSubCategoria = "+ txtIDCategoriaSeleccionada.Text+ ";").Rows[0]["Nombre"].ToString();
                    pbImageProducto.Image = BuscarInfo.ExtraerImagen("SELECT ImagenProducto FROM sbepa2.productos where idProducto = '" + txtID.Text + "';");

                    NUDPrecioProducto.Value = Convert.ToInt32(BuscarInfo.RellenarTabla1("SELECT PrecioCLP FROM sbepa2.preciosproductos where idSucursalProducto = '" + Convert.ToString(fila.Cells["idSucursalesProductos"].Value) + "' ORDER BY idPreciosProductos desc LIMIT 1;").Rows[0]["PrecioCLP"].ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer los datos del producto, subCategoria, Fotografia o Precio asignados ERROR: " + ex.Message + "", "Error extraer Nombre de la Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarInfo.CerrarConexionBD1();
                }
            }
        }

        private void btnNuevaProducto_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarProductos();
        }

        public void LimpiarCampos()
        {
            txtCantidadVisitasProducto.Text = "";
            txtID.Text = "";
            txtNombre.Text = "";
            cmbProductoUnico.Text = "";
            txtCodigoProducto.Text = "";
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
            ProductoNoUnicoRegistrado = false;
            gbDatosBase.Enabled = true;
            cmbProductoUnico.Enabled = true;
            txtCodigoProducto.Enabled = true;
            btnGuardarProducto.Enabled = true;
            pbImageProducto.Image = null;
            txtIDSucursal.Text = "";

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
                    if (txtCodigoProducto.Text != "")
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
                if (txtID.Text == "")
                {
                    //SI el ID del producto esta vacio se registra
                    if (cmbProductoUnico.Text == "SI")
                    {
                        ComandosBDMySQL GuardarProducto = new ComandosBDMySQL();
                        try
                        {
                            GuardarProducto.AbrirConexionBD1();
                            //Si el producto es Unico
                            GuardarProducto.IngresarImagen("call sbepa2.InsertarProducto('" + txtNombre.Text + "', '" + txtMarca.Text + "', '" + cmbEmvase.Text + "', '" + cmbUnidadMedida.Text + "', " + NUDCantidadMedida.Value.ToString() + ", " + txtIDCategoriaSeleccionada.Text + ", '" + txtDescripcionProducto.Text + "', @imagen, '" + cmbProductoUnico.Text + "', '" + txtCodigoProducto.Text + "');", pbImageProducto.Image);
                            //Se extrae el ID el producto registrado y se registra en la tabla intermedia
                            String IDProductoRegistrado = GuardarProducto.RellenarTabla1("SELECT idProducto FROM sbepa2.productos where Nombre = '" + txtNombre.Text + "' and Marca = '" + txtMarca.Text + "' and Envase= '" + cmbEmvase.Text + "' and DescripcionProducto = '" + txtDescripcionProducto.Text + "' order by idProducto desc LIMIT 1;").Rows[0]["idProducto"].ToString();
                            GuardarProducto.IngresarConsulta1("call sbepa2.InsertarSucursalesProducto(" + txtIDSucursal.Text + ", " + IDProductoRegistrado + ");");
                            //Se extraer el idSucursalProducto para poder registar el precio del producto
                            String IDSucursalProducto = GuardarProducto.RellenarTabla1("SELECT idSucursalesProductos FROM sbepa2.sucursalesproductos where SucursalID = '" + txtIDSucursal.Text + "' and ProductosID = '" + IDProductoRegistrado + "' order by idSucursalesProductos desc LIMIT 1;").Rows[0]["idSucursalesProductos"].ToString();
                            GuardarProducto.IngresarConsulta1("call sbepa2.InsertarPreciosProductos(" + NUDPrecioProducto.Value.ToString() + ", " + IDSucursalProducto + ");");
                            //Se registra el cambio
                            GuardarProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Productos', 'Insertar', 'REGISTRO EL PRODUCTO: CON EL NOMBRE" + txtNombre.Text + ", CON LA MARCA: " + txtMarca.Text + ", CON EL EMVASE: " + cmbEmvase.Text + ", CON LA UNIDAD DE MEDIDA: " + cmbUnidadMedida.Text + ", CON LA CANTIDAD DE MEDIDA: " + NUDCantidadMedida.Value.ToString() + ", CON EL ID DE SUB CATEGORIA: " + txtIDCategoriaSeleccionada.Text + ", CON LA DESCRIPCION: " + txtDescripcionProducto.Text + ", CON EL CODIGO DEL PRODUCTO: " + txtCodigoProducto.Text + ", EL CUAL FUE REGISTRADO CON EL ID DE PRODUCTO: " + IDProductoRegistrado + " CON EL ID DE SUCURSAL-PRODUCTO: " + IDSucursalProducto + ", CON EL PRECIO DE PRODUCTO: " + NUDPrecioProducto.Value.ToString() + "');");
                            MessageBox.Show("El producto con nombre: " + txtNombre.Text + " y con codigo UPC: " + txtCodigoProducto.Text + " a sido correctamente registrado", "Guarado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarProductos();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar registrar el porducto en el sistema ERROR: "+ex.Message+"","Error Guardar",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        finally
                        {
                            GuardarProducto.CerrarConexionBD1();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No tiene ID");
                        //Si el producto no es unico y no se encuentra registrado

                        //Si el producto no es unico y se encuentra registrado

                    } 
                }
                else
                {
                    //Si el ID el producto existe se modifica
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
                    txtCodigoProducto.Enabled = true;
                    if (txtCodigoProducto.Text != "" && txtNombre.Text != "")
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
                        Boolean ProductoRegistrado = VerificarProducto.VerificarExistenciaDato1("SELECT idProducto FROM sbepa2.productos where UPC = '" + txtCodigoProducto.Text + "';");
                        if (ProductoRegistrado == true)
                        {
                            MessageBox.Show("Ya existe un producto NO UNICO registrado con este nombre, se cargaran sus datos de forma automática, si considera que falta información del producto debe ser actualizada o modificada, debe dirigirse al menú principal y buscarlo en la sección ‘Cambios Info Producto’ para actualizar su información, la cual podrá ser aprobada por los administradores", "Nombre Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gbDatosEspecificos.Enabled = true;
                            ProductoNoUnicoRegistrado = true;

                            //Se cargan los datos registrados del producto
                            DataTable DatosProductoNoUnico = new DataTable();
                            DatosProductoNoUnico = VerificarProducto.RellenarTabla1("SELECT * FROM sbepa2.productos where UPC = '" + txtCodigoProducto.Text + "';");
                            txtID.Text = DatosProductoNoUnico.Rows[0]["idProducto"].ToString();
                            txtNombre.Text = DatosProductoNoUnico.Rows[0]["Nombre"].ToString();
                            cmbProductoUnico.Text = "NO";
                            txtCodigoProducto.Text = DatosProductoNoUnico.Rows[0]["UPC"].ToString();
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
                                VerificarLogo = VerificarProducto.RellenarTabla1("SELECT ImagenProducto FROM sbepa2.productos where idProducto = '" + txtID.Text + "';");

                                if (VerificarLogo.Rows[0][0].ToString() != "")
                                {
                                    //Si la imagen del producto esta guardada
                                    pbImageProducto.Image = VerificarProducto.ExtraerImagen("SELECT ImagenProducto FROM sbepa2.productos where idProducto = '" + txtID.Text + "';");
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
                        }
                        else
                        {
                            MessageBox.Show("NO existe un producto registrado con este nombre, el nombre es autentico", "Nombre Disponible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gbDatosEspecificos.Enabled = true;
                            ProductoNoUnicoRegistrado = false;
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
                txtCodigoProducto.Text = "Es Unico";
                txtCodigoProducto.Enabled = false;
            }
            else
            {
                txtCodigoProducto.Enabled = true;
                txtCodigoProducto.Text = "";
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
    }
}
