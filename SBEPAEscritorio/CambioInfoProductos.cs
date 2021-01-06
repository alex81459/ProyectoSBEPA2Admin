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
    public partial class CambioInfoProductos : Form
    {
        public CambioInfoProductos()
        {
            InitializeComponent();
            CargarProductosCambioInfo();
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
                    String CantidadRegistrosDetectados = (BuscarRegistros.RellenarTabla1("SELECT COUNT(CambioInfoProducto) FROM sbepa2.cambioinfoproducto Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';").Rows[0][0].ToString());
                    txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbCambioProducto.DataSource = BuscarRegistros.RellenarTabla2("call sbepa2.BuscarCambioInfoProducto('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
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
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProductosCambioInfo();
            HacerInvisiblesyLimpiarCampos();
        }

        private void CargarProductosCambioInfo()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarRegistros = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarRegistros.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarRegistros.RellenarTabla1("SELECT COUNT(CambioInfoProducto) FROM sbepa2.cambioinfoproducto;").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                cargarRegistros.AbrirConexionBD1();
                dgbCambioProducto.DataSource = cargarRegistros.RellenarTabla1("SELECT * FROM sbepa2.vistacambioinfoproducto;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la tabla de los cambios de producto del sistema ERROR: " + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbCambioProducto.DataSource = SiguientePagina.RellenarTabla1("SELECT CambioInfoProducto,idProducto,idUsuario,NombreProducto,Marca,Envase,UnidadMedida,CantidadMedida,Id_subcategoria,DescripcionProducto,UPC,FechaEnvioCambio,Estado from cambioinfoproducto ORDER BY CambioInfoProducto DESC limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
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

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private void Barra_MouseUp(object sender, MouseEventArgs e)
        {
            //Si el se deja de dar click a la Barra, se deja de mover el Form
            mover = false;
        }

        private void txtOriginalNombreProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void txtOriginaMarcaProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtOrignalDescripcionProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void dgbCambioProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgbCambioProducto.Rows[e.RowIndex];
            String EstadoSolicitudCambio = Convert.ToString(fila.Cells["Estado"].Value);
            if (EstadoSolicitudCambio == "Sin Revisar")
            {
                // Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
                if (e.RowIndex >= 0)
                {
                    //Se activa el GroupBox de los datos especificos del productos porque ya esta registrado
                    gbDatosEnviadosCambiar.Enabled = true;
                    gbDatosOriginalesACambiar.Enabled = true;

                    //Se extraen los datos de el DataGridView

                    txtCambioIDInfoProducto.Text = Convert.ToString(fila.Cells["CambioInfoProducto"].Value);
                    txtCambioIDProducto.Text = Convert.ToString(fila.Cells["idProducto"].Value);
                    txtCambioIDUsuario.Text = Convert.ToString(fila.Cells["idUsuario"].Value);
                    txtCambioNombreProducto.Text = Convert.ToString(fila.Cells["NombreProducto"].Value);
                    txtCambioMarcaProducto.Text = Convert.ToString(fila.Cells["Marca"].Value);
                    txtCambioEnvaseProducto.Text = Convert.ToString(fila.Cells["Envase"].Value);
                    txtUnidadMedidaProducto.Text = Convert.ToString(fila.Cells["UnidadMedida"].Value);
                    txtCambioCantidadMedida.Text = Convert.ToString(fila.Cells["CantidadMedida"].Value);
                    txtCambioIDSubCategoria.Text = Convert.ToString(fila.Cells["Id_subcategoria"].Value);
                    txtCambioUPC.Text = Convert.ToString(fila.Cells["UPC"].Value);
                    txtCambioDescripcionProducto.Text = Convert.ToString(fila.Cells["DescripcionProducto"].Value);
                    txtFechaEnvio.Text = Convert.ToString(fila.Cells["FechaEnvioCambio"].Value);

                    btnRechazarCambio.Enabled = true;

                    //Se extraen los datos que no estan en la tabla
                    ComandosBDMySQL BuscarInfoCambio = new ComandosBDMySQL();
                    try
                    {
                        BuscarInfoCambio.AbrirConexionBD1();
                        txtCambioRutUsuario.Text = BuscarInfoCambio.RellenarTabla1("SELECT RutUsuario FROM sbepa2.usuarios where Id_usuario = '" + txtCambioIDUsuario.Text + "';").Rows[0]["RutUsuario"].ToString();
                        txtCambioNombreSubCategoria.Text = BuscarInfoCambio.RellenarTabla1("SELECT Nombre FROM sbepa2.subcategoria where idSubCategoria = '" + txtCambioIDSubCategoria.Text + "';").Rows[0]["Nombre"].ToString();

                        pbCambioImagenProducto.Image = BuscarInfoCambio.ExtraerImagen("SELECT ImagenProducto FROM sbepa2.cambioinfoproducto where CambioInfoProducto = '"+ txtCambioIDInfoProducto.Text+ "';");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar extraer los datos del producto, subCategoria, Fotografia o Precio asignados ERROR: " + ex.Message + "", "Error extraer Nombre de la Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        BuscarInfoCambio.CerrarConexionBD1();
                    }


                    //Se extraen los datos ya registrados del producto a cambiar
                    ComandosBDMySQL BuscarProductoOriginal = new ComandosBDMySQL();
                    BuscarProductoOriginal.AbrirConexionBD1();
                    DataTable DatosProductoOriginal = new DataTable();
                    DatosProductoOriginal = BuscarProductoOriginal.RellenarTabla1("SELECT * FROM sbepa2.productos where idProducto = '" + txtCambioIDProducto.Text + "'");
                    txtOriginalIDProducto.Text = txtCambioIDProducto.Text;
                    txtOriginalNombreProducto.Text = DatosProductoOriginal.Rows[0]["Nombre"].ToString();
                    txtOriginaMarcaProducto.Text = DatosProductoOriginal.Rows[0]["Marca"].ToString();
                    cmbEmvase.Text = DatosProductoOriginal.Rows[0]["Envase"].ToString();
                    cmbUnidadMedida.Text = DatosProductoOriginal.Rows[0]["UnidadMedida"].ToString();
                    NUDOriginalCantidadMedida.Value = Convert.ToInt32(DatosProductoOriginal.Rows[0]["CantidadMedida"].ToString());
                    txtOriginalIdSubCategoria.Text = DatosProductoOriginal.Rows[0]["Id_subcategoria"].ToString();
                    txtOriginalUPCProducto.Text = DatosProductoOriginal.Rows[0]["UPC"].ToString();
                    txtOrignalDescripcionProducto.Text = DatosProductoOriginal.Rows[0]["DescripcionProducto"].ToString();

                    //Se extraen los datos que no estan en la tabla
                    ComandosBDMySQL BuscarProductoOriginalDatos = new ComandosBDMySQL();
                    try
                    {
                        //Se extrae el nombre de la categoria del producto y la imagen original
                        BuscarProductoOriginalDatos.AbrirConexionBD1();
                        txtOrignalNombreSubCategoria.Text = BuscarProductoOriginalDatos.RellenarTabla1("SELECT Nombre FROM sbepa2.subcategoria where idSubCategoria = '"+ txtOriginalIdSubCategoria.Text+ "';").Rows[0]["Nombre"].ToString();

                        pbOriginalImagenProducto.Image = BuscarProductoOriginalDatos.ExtraerImagen("SELECT ImagenProducto FROM sbepa2.productos where idProducto = '" + txtOriginalIDProducto.Text + "';");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar extraer los datos del producto, subCategoria, Fotografia o Precio asignados ERROR: " + ex.Message + "", "Error extraer Nombre de la Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        BuscarProductoOriginalDatos.CerrarConexionBD1();
                    }
                }
            }
            else
            {
                MessageBox.Show("Esta solicitud de cambio de la informacion del producto ya fue revisada, solo puede gestionar solicitudes que no esten revisadas","Solicitud revisada",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void LimpiarCampos()
        {
            txtCambioIDInfoProducto.Text = "";
            txtFechaEnvio.Text = "";
            txtCambioIDProducto.Text = "";
            txtCambioIDUsuario.Text = "";
            txtCambioRutUsuario.Text = "";
            txtCambioNombreProducto.Text = "";
            txtCambioMarcaProducto.Text = "";
            txtCambioEnvaseProducto.Text = "";
            txtUnidadMedidaProducto.Text = "";
            txtCambioCantidadMedida.Text = "";
            txtCambioIDSubCategoria.Text = "";
            txtCambioNombreSubCategoria.Text = "";
            txtCambioUPC.Text = "";
            txtCambioDescripcionProducto.Text = "";
            pbCambioImagenProducto.Image = null;
            txtOriginalIDProducto.Text = "";
            txtOriginalNombreProducto.Text = "";
            txtOriginaMarcaProducto.Text = "";
            cmbEmvase.Text = "";
            cmbUnidadMedida.Text = "";
            NUDOriginalCantidadMedida.Value = 1;
            txtOriginalIdSubCategoria.Text = "";
            txtOrignalNombreSubCategoria.Text = "";
            txtOriginalUPCProducto.Text = "";
            txtOrignalDescripcionProducto.Text = "";
            pbOriginalImagenProducto.Image = null;
            btnRechazarCambio.Enabled = false;
            btnActualizarProducto.Enabled = false;
            gbDatosOriginalesACambiar.Enabled = false;
            gbDatosEnviadosCambiar.Enabled = false;
        }

        private void btnEnviarNombre_Click(object sender, EventArgs e)
        {
            txtOriginalNombreProducto.Text = txtCambioNombreProducto.Text;
            btnActualizarProducto.Enabled = true;
        }

        private void btnEnviarMarca_Click(object sender, EventArgs e)
        {
            txtOriginaMarcaProducto.Text = txtCambioMarcaProducto.Text;
            btnActualizarProducto.Enabled = true;
        }

        private void btnEnviarEnvase_Click(object sender, EventArgs e)
        {
            cmbEmvase.Text = txtCambioEnvaseProducto.Text;
            btnActualizarProducto.Enabled = true;
        }

        private void btnEnviarUnidadMedida_Click(object sender, EventArgs e)
        {
            cmbUnidadMedida.Text = txtUnidadMedidaProducto.Text;
            btnActualizarProducto.Enabled = true;
        }

        private void btnEnviarCantidadMedida_Click(object sender, EventArgs e)
        {
            NUDOriginalCantidadMedida.Value = Convert.ToInt32(txtCambioCantidadMedida.Text);
            btnActualizarProducto.Enabled = true;
        }

        private void btnEnviarSubCategoriaID_Click(object sender, EventArgs e)
        {
            txtOriginalIdSubCategoria.Text = txtCambioIDSubCategoria.Text;
            txtOrignalNombreSubCategoria.Text = txtCambioNombreSubCategoria.Text;
            btnActualizarProducto.Enabled = true;
        }

        private void btnEnviarUPC_Click(object sender, EventArgs e)
        {
            txtOriginalUPCProducto.Text = txtCambioUPC.Text;
            btnActualizarProducto.Enabled = true;
        }

        private void btnEnviarDescripcionProducto_Click(object sender, EventArgs e)
        {
            txtOrignalDescripcionProducto.Text = txtCambioDescripcionProducto.Text;
            btnActualizarProducto.Enabled = true;
        }

        private void brnEnviarImagenProducto_Click(object sender, EventArgs e)
        {
            pbCambioImagenProducto.Image = null;
            pbOriginalImagenProducto.Image = pbCambioImagenProducto.Image;
            btnActualizarProducto.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            if (txtOriginalNombreProducto.Text != "" && txtOriginaMarcaProducto.Text != "" && txtOriginalIdSubCategoria.Text !="" && txtOriginalUPCProducto.Text != "" && txtOrignalDescripcionProducto.Text != "")
            {
                if (txtOriginalIDProducto.Text != "")
                {
                    //Se envia mensaje para verificar la decision
                    DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que autorizar el cambio de informacion del producto?, una vez autorizada no podra volver a ser reutilizada", "¿Autorizar cambio informacion Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    //Si contesta que si
                    if (resultadoMensaje == DialogResult.Yes)
                    {
                        ComandosBDMySQL ActualizarProductoBD = new ComandosBDMySQL();
                        try
                        {
                            ActualizarProductoBD.AbrirConexionBD1();
                            //Se actualiza el producto
                            ActualizarProductoBD.IngresarImagen("call sbepa2.ActualizarProducto(" + txtOriginalIDProducto.Text + ", '" + txtOriginalNombreProducto.Text + "', '" + txtOriginaMarcaProducto.Text + "', '" + cmbEmvase.Text + "', '" + cmbUnidadMedida.Text + "', " + NUDOriginalCantidadMedida.ToString() + ", " + txtOriginalIdSubCategoria.Text + ", @imagen, '" + txtOrignalDescripcionProducto.Text + "', 'NO', '" + txtOriginalUPCProducto.Text + "');", pbOriginalImagenProducto.Image);
                            //Se actualiza el CambioInfoProducto
                            ActualizarProductoBD.IngresarConsulta1("call sbepa2.ActualizarCambioInfoProducto(" + txtCambioIDInfoProducto.Text + ", 'Aprobado');");
                            ActualizarProductoBD.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'CambioInfoProducto', 'Autorizar Cambio Producto', 'La solitud de cambio de la informacion de producto con el ID: " + txtCambioIDInfoProducto.Text + " a sido aprobada')");
                            LimpiarCampos();
                            CargarProductosCambioInfo();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar cambiar la informacion del producto ERROR: " + ex.Message + "", "Error Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            ActualizarProductoBD.CerrarConexionBD1();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un producto para guardar los cambios del mismo","Falta Producto",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Faltan datos para actualizar el producto, revise los campos Nombre Producto, Marca Producto, ID Sub Categoria Producto, UPC Producto y Descripcion del Producto","Faltan Datos",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnRechazarCambio_Click(object sender, EventArgs e)
        {
            if (txtOriginalIDProducto.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que rechazara el cambio de informacion del producto?, una vez rechazada no podra volver a ser reutilizada", "¿Rechazar cambio informacion Producto?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Si contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    ComandosBDMySQL RechazarSolitudCambio = new ComandosBDMySQL();
                    try
                    {
                        //Se registra el rechazo del cambio
                        RechazarSolitudCambio.AbrirConexionBD1();
                        RechazarSolitudCambio.IngresarConsulta1("call sbepa2.ActualizarCambioInfoProducto("+ txtCambioIDInfoProducto.Text+ ", 'Rechazado');");
                        //Se regista la Accion
                        RechazarSolitudCambio.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'CambioInfoProducto', 'Rechazar Cambio Producto', 'La solitud de cambio de la informacion de producto con el ID: " + txtCambioIDInfoProducto.Text + " a sido rechazada')");
                        LimpiarCampos();
                        CargarProductosCambioInfo();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar rechazar la solicitud de cambio del producto ERROR: " + ex.Message + "", "Error Rechazar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        RechazarSolitudCambio.CerrarConexionBD1();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una solicitud de producto a actualizar","No selecciono solicitud",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
    }
}
