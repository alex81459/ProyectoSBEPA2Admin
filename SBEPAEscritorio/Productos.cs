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
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        FuncionesAplicacion verificarCaracteres = new FuncionesAplicacion();
        public Productos()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbID, "El ID (Identificador) el cual se usara para identificar las diferentes Productos Registrados" + System.Environment.NewLine + "este se genera automático cada vez que se hace un registro de una sucursal" + System.Environment.NewLine + "NO PUEDE SER CAMBIADO POR MANUALMENTE");
            ttmensaje.SetToolTip(pbNombreProducto, "El Nombre con el cual será identificado el producto a registrar " + System.Environment.NewLine + "  cada producto debe tener un nombre único que los diferencie, NO SE PUEDE REPETIR");
            ttmensaje.SetToolTip(pbSeleccionarSucursal, "La Interfaz donde se seleccionara la sucursales a las" + System.Environment.NewLine + " cuales le producto pertenecerá");
            ttmensaje.SetToolTip(pbMarca, "La marca que fabrica o produce el producto en cuestión");
            ttmensaje.SetToolTip(pbEmbase, "El tipo de embace en el cual viene almacenado el producto para su venta");
            ttmensaje.SetToolTip(pbUnidadMedida, "La Unidad de Medida que utiliza el producto para establecer" + System.Environment.NewLine + " la cantidad de contenido que tiene");
            ttmensaje.SetToolTip(pbCantidadMedida, "La cantidad de contenido que tiene el producto según la " + System.Environment.NewLine + "unidad de medida seleccionada anteriormente");
            ttmensaje.SetToolTip(pbDescripcionProducto, "Una Descripción del producto de máximo 300 caracteres");
            ttmensaje.SetToolTip(pbCodigoProducto, "El Código Universal de Producto (UPC) es una simbología de código de barras" + System.Environment.NewLine + " que se utiliza ampliamente para identificar de forma única el producto");
            ttmensaje.SetToolTip(pbDuracionProducto, "La Duración Estimada del Producto en cuestión");
            ttmensaje.SetToolTip(pbFechaRegistro, "La Fecha cuando el producto fue registrado en el sistema" + System.Environment.NewLine + " esta se genera de forma automática");
            ttmensaje.SetToolTip(pbSeleccioneCategoria, "La Interfaz donde se seleccionara la categoría del producto");
            ttmensaje.SetToolTip(pbImagenProducto, "Una Imagen Referencial del producto para poder identificarlo");
            ttmensaje.SetToolTip(pbImagenPrueba, "Una Imagen de Prueba de las características del producto que verifica " + System.Environment.NewLine + " que sus datos son correctos como el precio");
        }

        public DataTable DTTiendasSeleccionadas = new DataTable();

        private void Productos_Load(object sender, EventArgs e)
        {
            CargarProductos();
            cmbEmbase.Text = "Bolsa Plastica";
            cmbUnidadMedida.Text = "Mililitros";
            txtFechaRegistro.Text = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));

            //Se agregan las Columnas a el DataTable DTSucursales Seleccionados
            DTTiendasSeleccionadas.Clear();
            DTTiendasSeleccionadas.Columns.Add("IDTiendaProducto");
            DTTiendasSeleccionadas.Columns.Add("NombreTienda");
            DTTiendasSeleccionadas.Columns.Add("PrecioProducto");
            DTTiendasSeleccionadas.Columns.Add("Oferta");
            DTTiendasSeleccionadas.Columns.Add("FinalOferta");
        }

        private void CargarProductos()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarProductos = new ComandosBDMySQL();
            try
            {
                cargarProductos.AbrirConexionBD1();
                dgbProductos.DataSource = cargarProductos.RellenarTabla1("SELECT * FROM sbepa.vista_productos;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar los productos ERROR:" + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarProductos.CerrarConexionBD1();
            }
        }

        private void cmbBuscarEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se verifica que campo fue seleccionado en el ComboBox
            if (cmbBuscarEn.Text == "Fecha Registro")
            {
                //Si se selecciono fecha registro se activan los campos necesarios
                txtBuscarEn.Visible = false;
                lblParametrosBuscar.Visible = false;
            }
            else
            {
                //Si se selecciono cualquier otro campo menos Fecha Registro se activan los campos necesarios
                txtBuscarEn.Visible = true;
                lblParametrosBuscar.Visible = true;
            }
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            String BuscarEn = "";

            if (cmbBuscarEn.Text == "ID Producto")
            {
                BuscarEn = "idproducto";
            }
            else if (cmbBuscarEn.Text == "Nombre Producto")
            {
                BuscarEn = "nombre";
            }
            else if (cmbBuscarEn.Text == "ID Sucursal del Producto")
            {
                BuscarEn = "id_sucursal";
            }
            else if (cmbBuscarEn.Text == "Marca del Producto")
            {
                BuscarEn = "marca";
            }
            else if (cmbBuscarEn.Text == "Tipo de Envase")
            {
                BuscarEn = "envase";
            }
            else if (cmbBuscarEn.Text == "Unidad de Medida")
            {
                BuscarEn = "unidad_medida";
            }
            else if (cmbBuscarEn.Text == "Cantidad Medida")
            {
                BuscarEn = "cantidad_medida";
            }
            else if (cmbBuscarEn.Text == "ID Sub Categoria")
            {
                BuscarEn = "id_subcategoria";
            }
            else if (cmbBuscarEn.Text == "Cantidad Visitas")
            {
                BuscarEn = "cantidad_visita";
            }
            else if (cmbBuscarEn.Text == "Descripcion Producto")
            {
                BuscarEn = "descripcion_producto";
            }
            else if (cmbBuscarEn.Text == "Codigo Producto")
            {
                BuscarEn = "codigo_producto";
            }
            else if (cmbBuscarEn.Text == "Duracion del Producto")
            {
                BuscarEn = "duracionProducto";
            }

            ComandosBDMySQL buscarTabla = new ComandosBDMySQL();
            try
            {
                buscarTabla.AbrirConexionBD1();
                dgbProductos.DataSource = buscarTabla.RellenarTabla1("SELECT `idproducto` AS `ID Producto`,`nombre` AS `Nombre Producto`,`fecharegistro` AS `Fecha Registro`,`marca` AS `Marca del Producto`,`envase` AS `Tipo de Envase`,`unidad_medida` AS `Unidad Medida`,`cantidad_medida` AS `Cantidad Medida`,`id_subcategoria` AS `ID Sub Categoria`,`cantidad_visita` AS `Cantidad Visitas`,`descripcion_producto` AS `Descripcion Producto`, `codigo_producto` AS `Codigo Producto`,`producto`.`duracionProducto` AS `Duracion del Producto` FROM `producto` Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%';");
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

        private void nudDias_ValueChanged(object sender, EventArgs e)
        {
            //Se tranforman segun las cantidad de dias en meses, y meses en años
            int cantidadDias = Convert.ToInt32(nudDias.Value);
            if (cantidadDias >= 30)
            {
                nudDias.Value = 0;
                nupMeses.Value = Convert.ToInt32(nupMeses.Value) + 1;
                if (Convert.ToInt32(nupMeses.Value) >= 12)
                {
                    nupMeses.Value = 0;
                    nupAños.Value = Convert.ToInt32(nupAños.Value) + 1;
                }
            }
        }

        private void nupMeses_ValueChanged(object sender, EventArgs e)
        {
            //Se convierte segun las cantidad de meses en años
            if (Convert.ToInt32(nupMeses.Value) >= 12)
            {
                nupMeses.Value = 0;
                nupAños.Value = Convert.ToInt32(nupAños.Value) + 1;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresNombreProducto(e);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresNombreProducto(e);
        }

        private void txtDescripcionProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresNombreProducto(e);
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresSoloNumeros(e);
        }

        private void btnBuscarTienda_Click(object sender, EventArgs e)
        {
            ProductosTienda abrirBuscarSucursales = new ProductosTienda();
            abrirBuscarSucursales.ShowDialog();
        }

        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            //Se crea la instancia y se abre el editor de imagenes
            EditorImagen abrirEditorImagen = new EditorImagen();
            if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
            {
                //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                pbImageProducto.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                cbImagenProductoSeleccionada.Checked = true;
                cbImagenPruebaSeleccionada.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Se crea la instancia y se abre el editor de imagenes
            EditorImagen abrirEditorImagen = new EditorImagen();
            if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
            {
                //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                pbImagePrueba.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
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

        public static string InvertirManualmente(string cadena)
        {
            string cadenaInvertida = "";
            // Recorrer cadena letra por letra
            foreach (char letra in cadena)
            {
                // Anteponer la letra a la cadena invertida
                cadenaInvertida = letra + cadenaInvertida;
            }
            return cadenaInvertida;
        }



        private void btnGuardarSucursal_Click(object sender, EventArgs e)
        {
            //Se envia mensaje para verificar la decision
            DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que el producto que se alamacenara es correcto y su informacion tambien lo es?", "Guardar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //Se contesta que si
            if (resultadoMensaje == DialogResult.Yes)
            {
                if (txtID.Text == "")
                {
                    if (VerificarCamposGuardar() == true)
                    {
                        ComandosBDMySQL registarProducto = new ComandosBDMySQL();
                        try
                        {
                            registarProducto.AbrirConexionBD1();
                            //Se registran los datos del producto
                            registarProducto.IngresarConsulta1("call sbepa.insertar_producto('" + txtNombre.Text + "', '" + txtFechaRegistro.Text + "', '" + txtMarca.Text + "', '" + cmbEmbase.Text + "', '" + cmbUnidadMedida.Text + "', '" + NUDCantidadMedida.Value.ToString() + "', " + lblIDCategoriaSeleccionada.Text + ", '" + txtDescripcionProducto.Text + "', '" + txtCodigoProducto.Text + "','Dias:" + nudDias.Value.ToString() + " Meses:" + nupMeses.Value.ToString() + " Años:" + nupAños.Value.ToString() + "');");
                            //Se extrae el ID con el cual fue registrado
                            String productoID = registarProducto.RellenarTabla1("SELECT Max(idproducto) FROM sbepa.producto;").Rows[0]["Max(idproducto)"].ToString();
                            //Se agregan las imagenes al producto con su ID
                            if (pbImageProducto.Image != null)
                            {
                                registarProducto.IngresarImagen("call sbepa.Insertar_Imagen_Producto(" + productoID + ", @imagen);", pbImageProducto.Image);
                            }
                            if (pbImagePrueba.Image != null)
                            {
                                registarProducto.IngresarImagen("call sbepa.Insertar_Imagen_Prueba_Producto(" + productoID + ", '@imagen');", pbImagePrueba.Image);
                            }
                            //Se registra el ID del producto, el ID de la tienda y los datos del precio recorriendo el DTTiendasSeleccionadas
                            for (int i = 0; i < DTTiendasSeleccionadas.Rows.Count; i++)
                            {
                                //Se extren los valores del datatable DTTiendasSeleccionadas
                                String IDTiendaProducto = DTTiendasSeleccionadas.Rows[i]["IDTiendaProducto"].ToString();
                                String NombreTienda = DTTiendasSeleccionadas.Rows[i]["NombreTienda"].ToString();
                                String PrecioProducto = DTTiendasSeleccionadas.Rows[i]["PrecioProducto"].ToString();
                                String Oferta = DTTiendasSeleccionadas.Rows[i]["Oferta"].ToString();
                                String FinalOferta = DTTiendasSeleccionadas.Rows[i]["FinalOferta"].ToString();

                                //Se ingresan los datos a la tabla Tienda_Productos
                                registarProducto.IngresarConsulta1("call sbepa.insertar_tienda_productos(" + IDTiendaProducto + ", " + productoID + ");");

                                //Se extrae el ID con la que se registro en la tabla tienda_productos
                                String IDTiendaProductos = registarProducto.RellenarTabla1("SELECT Max(idtienda_producto) FROM sbepa.tiendas_productos;").Rows[0]["Max(idtienda_producto)"].ToString();

                                //registra el precio del producto y su info
                                registarProducto.IngresarConsulta1("call sbepa.insertar_precios_productos('" + txtFechaRegistro.Text + "', '" + PrecioProducto + "', '" + Oferta + "', '" + FinalOferta + "', " + IDTiendaProductos + ");");
                            }
                            //Se registra que el administrador agrego el producto
                            registarProducto.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "', 'Productos', 'Insertar', 'REGISTRO EL PRODUCTO: " + txtNombre.Text + ", CON LA INFORMACION DE PRODUCTO SIGUIENTE MARCA: " + txtMarca.Text + " EMBASE: " + cmbEmbase.Text + " UNIDAD MEDIDA: " + cmbUnidadMedida.Text + " CANTIDAD MEDIDA: " + NUDCantidadMedida.Value.ToString() + " DESCRIPCION DEL PRODUCTO: " + txtDescripcionProducto.Text + " CODIGO UNIVERSAL DEL PRODUCTO: " + txtCodigoProducto.Text + " SUBCATEGORIA: " + lblIDCategoriaSeleccionada.Text + " " + lblNombreSubCategoria.Text + " DURACION DEL PRODUCTO Dias:" + nudDias.Value.ToString() + " Meses:" + nupMeses.Value.ToString() + " Años:" + nupAños.Value.ToString() + "'); ");
                            //Se limpian los campos y se muestra un mesaje de confirmacion
                            MessageBox.Show("Se guardo correctamente el producto, las tiendas donde se vende y su categoria", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarProductos();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar guardar el producto y la informacion del Producto " + ex.Message + "", "Error Guardar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            registarProducto.CerrarConexionBD1();
                        }
                    }
                }
                else
                {
                    //Se verifica que todos los campos necesarios esten rellenados
                    if (VerificarCamposModificar() == true)
                    {
                        // Si los datos tienen un ID significa que los datos del producto deben ser actualizados
                        ComandosBDMySQL ActualizarProducto = new ComandosBDMySQL();
                        try
                        {
                            //Se actualizan los datas del porducto
                            ActualizarProducto.AbrirConexionBD1();

                            ActualizarProducto.IngresarConsulta1("call sbepa.actualizar_producto(" + txtID.Text + ", '" + txtNombre.Text + "', '" + txtMarca.Text + "', '" + cmbEmbase.Text + "', '" + cmbUnidadMedida.Text + "', " + NUDCantidadMedida.Value.ToString() + ", " + lblIDCategoriaSeleccionada.Text + ", '" + txtDescripcionProducto.Text + "', '" + txtCodigoProducto.Text + "', 'Dias:" + nudDias.Value.ToString() + " Meses:" + nupMeses.Value.ToString() + " Años:" + nupAños.Value.ToString() + "');");
                            //Se agregan las imagenes al producto con su ID
                            if (pbImageProducto.Image != null)
                            {
                                ActualizarProducto.IngresarImagen("call sbepa.Insertar_Imagen_Producto(" + txtID.Text + ", @imagen);", pbImageProducto.Image);
                            }
                            if (pbImagePrueba.Image != null)
                            {
                                ActualizarProducto.IngresarImagen("call sbepa.Insertar_Imagen_Prueba_Producto(" + txtID.Text + ", '@imagen');", pbImagePrueba.Image);
                            }

                            //Se registra el cambio que realizo el administrador
                            ActualizarProducto.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "', 'Productos', 'Modificar', 'MODIFICO EL PRODUCTO CON EL ID " + txtID.Text + " CON EL NOMBRE: " + txtNombre.Text + ", CON LA INFORMACION DE PRODUCTO SIGUIENTE, MARCA: " + txtMarca.Text + " EMBASE: " + cmbEmbase.Text + " UNIDAD MEDIDA: " + cmbUnidadMedida.Text + " CANTIDAD MEDIDA: " + NUDCantidadMedida.Value.ToString() + " DESCRIPCION DEL PRODUCTO: " + txtDescripcionProducto.Text + " CODIGO UNIVERSAL DEL PRODUCTO: " + txtCodigoProducto.Text + " SUBCATEGORIA: " + lblIDCategoriaSeleccionada.Text + " " + lblNombreSubCategoria.Text + " DURACION DEL PRODUCTO Dias:" + nudDias.Value.ToString() + " Meses:" + nupMeses.Value.ToString() + " Años:" + nupAños.Value.ToString() + "'); ");
                            //Se muestra un mensaje de confirmacion
                            MessageBox.Show("Se han Actualizado Correctamente los datos del producto con ID " + txtID.Text + "", "Actualizacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProductos();
                            LimpiarCampos();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar guardas la actualizacion deel producto actual, ERROR: " + ex.Message + "", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            ActualizarProducto.CerrarConexionBD1();
                        }
                    }
                }
            }   
        }

        private Boolean VerificarCamposGuardar(){
            //Se verifica si lso campos necesarios tienen contenido
            if (txtNombre.Text != "" || txtMarca.Text != "" || txtDescripcionProducto.Text != "" || txtCodigoProducto.Text != "" || lblIDCategoriaSeleccionada.Text != "??")
            {
                //Se verifica si se han seleccionado las Tiendas y la Categoria del Producto
                if (cbSeleccionadasTiendas.Checked == true && cbCategoriaSeleccionada.Checked == true)
                {
                    //Se verifica si la imagen del producto a sido seleccionada y si la iamgen de prueba del precio a sido seleccionada
                    if (cbImagenProductoSeleccionada.Checked == true)
                    {
                        //Se verifica si el producto ya esta registrado en el sistema, verificando su nombre de producto y su codigo de producto
                        ComandosBDMySQL verificarProducto = new ComandosBDMySQL();
                        try
                        {
                            verificarProducto.AbrirConexionBD1();
                            if (verificarProducto.VerificarExistenciaDato1("SELECT * FROM sbepa.producto where nombre = '" + txtNombre.Text + "';") == false)
                            {
                                if (verificarProducto.VerificarExistenciaDato1("SELECT * FROM sbepa.producto where codigo_producto = '"+ txtCodigoProducto.Text+ "';") == false)
                                {
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show("El Codigo Universal del Producto ya esta registrado en el sistema, no pueden existir dos producto con el mismo codigo","Producto ya registrado",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("El Nombre del producto ya esta registrado dentro del sistema, no pueden existir dos productos con el mismo nombre", "Producto ya registrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("No se ha podido verificar si el producto esta registrado en el sistema ERROR: "+ex.Message+"","Error verificacion",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            return false;
                        }
                        finally
                        {
                            verificarProducto.CerrarConexionBD1();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Verifique que ha seleccionado una imagen del producto y una imagen de prueba del producto", "Faltan Datos para guardar el producto",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Verifique que ha seleccionado las tiendas donde se vendera el producto y la categoria del producto","Faltan Datos para guardar el producto",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Revise que los campos de informacion del producto Nombre, Marca, Descripcion del Producto y Código Universal de Producto tengan informacion en su interior","Faltan datos para guardar el producto",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
        }

        private Boolean VerificarCamposModificar()
        {
            //Se verifica si lso campos necesarios tienen contenido
            if (txtNombre.Text != "" || txtMarca.Text != "" || txtDescripcionProducto.Text != "" || txtCodigoProducto.Text != "" || lblIDCategoriaSeleccionada.Text != "??")
            {
                //Se verifica si se han seleccionadola Categoria del Producto
                if ( cbCategoriaSeleccionada.Checked == true)
                {
                    //Se verifica si la imagen del producto a sido seleccionada
                    if (cbImagenProductoSeleccionada.Checked == true )
                    {
                                    return true;
                    }
                    else
                    {
                        MessageBox.Show("Verifique que ha seleccionado una imagen del producto ", "Faltan Datos para guardar el producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Verifique que ha seleccionado la categoria del producto", "Faltan Datos para guardar el producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Revise que los campos de informacion del producto Nombre, Marca, Descripcion del Producto y Código Universal de Producto tengan informacion en su interior", "Faltan datos para guardar el producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void LimpiarCampos()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            cbSeleccionadasTiendas.Text = "";
            txtMarca.Text = "";
            cmbEmbase.Text = "Bolsa Plastica";
            cmbUnidadMedida.Text = "Mililitros";
            NUDCantidadMedida.Value = 1;
            txtDescripcionProducto.Text = "";
            txtCodigoProducto.Text = "";
            nudDias.Value = 1;
            nupMeses.Value = 0;
            nupAños.Value = 0;
            cbCategoriaSeleccionada.Checked = false;
            CargarProductos();
            cbImagenProductoSeleccionada.Checked = false;
            cbImagenPruebaSeleccionada.Checked = false;
            pbImageProducto.Image = null;
            pbImagePrueba.Image = null;
            btnBuscarTienda.Enabled = true;
            txtFechaRegistro.Text = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));
            cbSeleccionadasTiendas.Checked = false;
            cbCategoriaSeleccionada.Checked = false;
        }

        private void btnNuevaSucursal_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarProductos();
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            ProductosBuscarCategoria abrirBuscar = new ProductosBuscarCategoria();
            abrirBuscar.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProductos();
            LimpiarCampos();
        }

        private void ActivarEditar()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            txtNombre.Enabled = true;
            btnBuscarTienda.Enabled = false;
            txtMarca.Text = "";
            txtMarca.Enabled = true;
            cmbEmbase.Text = "Bolsa Plastica";
            cmbUnidadMedida.Text = "Mililitros";
            NUDCantidadMedida.Value = 1;
            txtDescripcionProducto.Text = "";
            txtCodigoProducto.Text = "";
            nudDias.Value = 1;
            nupMeses.Value = 0;
            nupAños.Value = 0;
            cbCategoriaSeleccionada.Checked = true;

        }

        private void dgbProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se activa la capacidad de modificar los datos
                ActivarEditar();
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbProductos.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["IDProducto"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["NombreProducto"].Value);
                txtMarca.Text = Convert.ToString(fila.Cells["MarcadelProducto"].Value);
                cmbEmbase.Text = Convert.ToString(fila.Cells["TipodeEnvase"].Value);
                cmbUnidadMedida.Text = Convert.ToString(fila.Cells["UnidadMedida"].Value);
                NUDCantidadMedida.Value = Convert.ToInt32(fila.Cells["CantidadMedida"].Value);
                txtDescripcionProducto.Text = Convert.ToString(fila.Cells["DescripcionProducto"].Value);
                txtCodigoProducto.Text = Convert.ToString(fila.Cells["CodigoProducto"].Value);
                txtFechaRegistro.Text = Convert.ToString(fila.Cells["FechaRegistro"].Value);
                lblIDCategoriaSeleccionada.Text = Convert.ToString(fila.Cells["IDSubCategoria"].Value);

                //Se extrae la duracion del producto y se adapta a los Campos NumericUpDown
                String DuracionProducto = "";
                DuracionProducto = Convert.ToString(fila.Cells["DuraciondelProducto"].Value);

                //Se guardan la separacion dentro de un arreglo
                String[] Separador = DuracionProducto.Split(' ');

                //Se extraen desde el arreglo
                String Dias = Separador[0];
                String Meses = Separador[1];
                String Años = Separador[2];

                //Se quitan los primeros caracteres de la fecha dividida para que solo quede el valor numerico
                char[] CaracteresQuitar = { 'D', 'i', 'a', 's', ':', 'M', 'e', 's', 'A', 'ñ', 'o', };
                nudDias.Value = Convert.ToInt32(Dias.TrimStart(CaracteresQuitar));
                nupMeses.Value = Convert.ToInt32(Meses.TrimStart(CaracteresQuitar));
                nupAños.Value = Convert.ToInt32(Años.TrimStart(CaracteresQuitar));

                //se extrae el nombre de la subcategoria
                String IDSubCategoria = Convert.ToString(fila.Cells["IDSubCategoria"].Value);
            
                ComandosBDMySQL BuscarNombreSubCategoria = new ComandosBDMySQL();
                try
                {
                    BuscarNombreSubCategoria.AbrirConexionBD1();
                    String NombreSubCategoria = BuscarNombreSubCategoria.RellenarTabla1("SELECT nombre_categoria FROM sbepa.sub_categoria where id_subcategoria = '" + IDSubCategoria + "';").Rows[0]["nombre_categoria"].ToString();
                    lblNombreSubCategoria.Text = NombreSubCategoria;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer el nombre de la categoria del producto seleccionado ERROR"+ex.Message+"","Error Extraer Dato",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                finally
                {
                    BuscarNombreSubCategoria.CerrarConexionBD1();
                }
               
                //Se extrae las imagenes
                ComandosBDMySQL ExtraerImagenes = new ComandosBDMySQL();
                try
                {
                    pbImageProducto.Image = ExtraerImagenes.ExtraerImagen("SELECT archivo_producto FROM sbepa.producto where idproducto = '"+ txtID.Text+ "';");
                    cbImagenProductoSeleccionada.Checked = true;
                    pbImagePrueba.Image = ExtraerImagenes.ExtraerImagen("SELECT archivo_prueba FROM sbepa.producto where idproducto = '" + txtID.Text + "';");
                    cbImagenPruebaSeleccionada.Checked = true;
                }
                catch (Exception)
                {
                }
                finally
                {
                    ExtraerImagenes.CerrarConexionBD1();
                }
            }
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresBuscar(e);
        }
    }
}
