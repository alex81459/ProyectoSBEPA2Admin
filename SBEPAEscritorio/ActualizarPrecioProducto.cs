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
    public partial class ActualizarPrecioProducto : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        FuncionesAplicacion verificarCaracteres = new FuncionesAplicacion();

        public ActualizarPrecioProducto()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbIDTienda, "Debe Seleccionar el ID (Identificador) de la Tienda en la que se actualizara el precio del producto " + System.Environment.NewLine + "o donde se añadira un nuevo producto a la tienda y su precio");
            ttmensaje.SetToolTip(pbPrecioProducto, "El Precio que el Producto tendra en la Tienda");
            ttmensaje.SetToolTip(pbFinalOferta, "Si la oferta del producto tendra un final especificado");
            ttmensaje.SetToolTip(pbFechaFinal, "La Fecha de cuando terminara la oferta del producto");
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

        private void ckFinalOferta_CheckedChanged(object sender, EventArgs e)
        {
            if (ckFinalOferta.Checked == true)
            {
                txtFinalOfertaTexto.Visible = true;
                pbFinalOferta.Visible = true;
                ckFechaFinal.Checked = false;
                dtpFinalOferta.Visible = false;
                pbFechaFinal.Visible = false;
            }
        }

        private void ckFechaFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (ckFechaFinal.Checked == true)
            {
                ckFinalOferta.Checked = false;
                txtFinalOfertaTexto.Visible = false;
                dtpFinalOferta.Visible = true;
                pbFechaFinal.Visible = true;
                pbFinalOferta.Visible = false;
            }
        }

        private void ActivarPrecioOferta()
        {
            ckFechaFinal.Visible = true;
            pbFechaFinal.Visible = true;
            dtpFinalOferta.Visible = true;
            ckFinalOferta.Visible = true;
            pbFinalOferta.Visible = true;
            txtFinalOfertaTexto.Visible = true;

        }
        private void DesactivarPrecioOferta()
        {
            ckFechaFinal.Visible = false;
            pbFechaFinal.Visible = false;
            dtpFinalOferta.Visible = false;
            ckFinalOferta.Visible = false;
            pbFinalOferta.Visible = false;
            txtFinalOfertaTexto.Visible = false;
        }

        private void cbPrecioOferta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrecioOferta.Checked == true)
            {
                //Si el checkbox precio oferta esta seleccionado se activa la oferta
                ActivarPrecioOferta();
            }
            else
            {
                //Si el checkbox precio oferta NO estaseleccionado se desactiva la oferta la oferta
                DesactivarPrecioOferta();
            }
        }

        private void ActualizarPrecioProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
            cmbBuscarEn.Text = "ID Producto";
            
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

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void txtFinalOfertaTexto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            CargarProductos();
            txtBuscarEn.Text = "";
            cmbBuscarEn.Text = "ID Producto";
            PanelNuevoPrecio.Enabled = false;
            lblIDProducto.Text = "¿?";
            lblNombreProducto.Text = "¿?";
            dgbPrecios.DataSource = null;
            txtIDTienda.Text = "";
            txtListaNombreTienda.Text = "";
            nupListaPrecio.Value = 1;
            txtFinalOfertaTexto.Text = "";
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


        private void dgbProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbProductos.Rows[e.RowIndex];
                PanelNuevoPrecio.Enabled = true;
                lblIDProducto.Text = Convert.ToString(fila.Cells["IDProducto"].Value);
                lblNombreProducto.Text = Convert.ToString(fila.Cells["NombreProducto"].Value);

                //Se extraen los precios del producto
                ComandosBDMySQL CargarPrecios = new ComandosBDMySQL();
                try
                {
                        CargarPrecios.AbrirConexionBD1();
                        dgbPrecios.DataSource = CargarPrecios.RellenarTabla1("call sbepa.MostrarTodosLosPreciosDelProducto(" + lblIDProducto.Text + ");");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar cargar la tabla de los precios del producto seleccionado ERROR: " + ex.Message + "", "Error Cargar Precios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    CargarPrecios.CerrarConexionBD1();
                }
            }
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscarTienda_Click(object sender, EventArgs e)
        {
            ActualizarPrecioProductoBuscarTienda AbrirBuscar = new ActualizarPrecioProductoBuscarTienda();
            AbrirBuscar.ShowDialog();
        }

        private Boolean VerificarDatosAGuardar()
        {
            if (txtIDTienda.Text != "")
            {
                if (nupListaPrecio.Value > 0)
                {
                    if (cbPrecioOferta.Checked == true)
                    {
                        if (txtFinalOfertaTexto.Text != "" || dtpFinalOferta.Text != "")
                        {
                            return true;
                        }
                        else {
                            MessageBox.Show("Debe Ingresar un final de ofeta del precio del producto o una fecha para cuando acabe la oferta","Falta Info de Oferta",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar un precio mayor a 0 para el producto seleccioando","Falta precio",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar Una Tienda en la cual se actualizara el precio del producto","Falta Tienda",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
        }


        private void btnAgregarPrecio_Click(object sender, EventArgs e)
        {
            if (VerificarDatosAGuardar() == true)
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea actualizar el precio del producto con los datos actualez?, una vez ingresado no podra ser cambiado ni eliminado el precio nuevo", "Precio Nuevo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Se contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    ComandosBDMySQL registrarNuevoPrecio = new ComandosBDMySQL();
                    try
                    {
                        registrarNuevoPrecio.AbrirConexionBD1();

                        //Se ingresan los datos a la tabla Tienda_Productos
                        registrarNuevoPrecio.IngresarConsulta1("call sbepa.insertar_tienda_productos(" + txtIDTienda.Text + ", " + lblIDProducto.Text + ");");

                        //Se extrae el ID con la que se registro en la tabla tienda_productos
                        String IDTiendaProductos = registrarNuevoPrecio.RellenarTabla1("SELECT Max(idtienda_producto) FROM sbepa.tiendas_productos;").Rows[0]["Max(idtienda_producto)"].ToString();

                        String FechaActual = (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss"));

                        String OfertaProducto = "";
                        if (cbPrecioOferta.Checked == true)
                        {
                            OfertaProducto = "SI";
                        }
                        else
                        {
                            OfertaProducto = "NO";
                        }

                        String FinalOFerta = "NO hay Oferta";
                        if (cbPrecioOferta.Checked == true)
                        {
                            if (ckFinalOferta.Checked == true)
                            {
                                FinalOFerta = txtFinalOfertaTexto.Text;
                            }
                            if (ckFechaFinal.Checked == true)
                            {
                                FinalOFerta = dtpFinalOferta.Text;
                            }
                        }
                        else
                        {
                            FinalOFerta = "NO hay Oferta";
                        }

                        //registra el precio del producto y su info
                        registrarNuevoPrecio.IngresarConsulta1("call sbepa.insertar_precios_productos('" + FechaActual + "', '" + nupListaPrecio.Value.ToString() + "', '" + OfertaProducto + "', '" + FinalOFerta + "', " + IDTiendaProductos + ");");

                        //Se registra que el administrador agrego un nuevo precio a un producto
                        registrarNuevoPrecio.IngresarConsulta1("call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "', 'Actualizar Precio Producto', 'Insertar', 'REGISTRO UN NUEVO PRECIO DE PRODUCTO PARA EL PRODUCTO: " + txtListaNombreTienda.Text + ", CON LA INFORMACION DE PRECIO SIGUIENTE PRECIO PRODUCTO: " + nupListaPrecio.Value.ToString() + " OFETA PRODUCTO: " + OfertaProducto + " FINAL OFERTA: " + FinalOFerta + "'); ");
                        //Se limpian los campos y se muestra un mesaje de confirmacion
                        MessageBox.Show("Se guardo correctamente el nuevo precio del producto", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar registrar el nuevo precio para el producto en la tienda elegida ERROR: " + ex.Message + "", "Error guardar nuevo precio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        registrarNuevoPrecio.CerrarConexionBD1();
                    }
                }   
            }
        }
    }
}
