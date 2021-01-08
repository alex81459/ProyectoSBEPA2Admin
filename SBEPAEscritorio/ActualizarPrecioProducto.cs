using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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


        private void ActualizarPrecioProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
            cmbBuscarEn.Text = "ID Producto";
            CargarProductosSucursales();
        }

        private void CargarProductos()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarProductosSucursales = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarProductosSucursales.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarProductosSucursales.RellenarTabla1("SELECT COUNT(idProducto) FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID inner join Sucursales on sucursalesproductos.SucursalID = sucursales.idSucursales ORDER BY productos.idProducto DESC LIMIT 50;").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                cargarProductosSucursales.AbrirConexionBD1();
                dgbProductosSucursal.DataSource = cargarProductosSucursales.RellenarTabla1("SELECT * FROM sbepa2.vistacambiospreciosproductos;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar los productos ERROR:" + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarProductosSucursales.CerrarConexionBD1();
            }
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = verificarCaracteres.RestringirCaracteresBuscar(e);
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
                    String CantidadRegistrosDetectados = (BuscarRegistros.RellenarTabla1("SELECT COUNT(idProducto) ,Nombre, Marca, UPC, sucursalesproductos.idSucursalesProductos, sucursalesproductos.SucursalID, sucursales.direccion  FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID inner join Sucursales on sucursalesproductos.SucursalID = sucursales.idSucursales Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%' ORDER BY productos.idProducto DESC LIMIT 50;").Rows[0][0].ToString());
                    txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbProductosSucursal.DataSource = BuscarRegistros.RellenarTabla2("call sbepa2.BuscarProductosSucursales('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
                    lblRegistrosEncontrados.Visible = true;
                    txtRegistrosEncontradosSuperior.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar Los productos y sus Sucursales en el sistema ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbProductosSucursal.DataSource = SiguientePagina.RellenarTabla1("SELECT idProducto,Nombre, Marca, UPC, sucursalesproductos.idSucursalesProductos, sucursalesproductos.SucursalID, sucursales.direccion  FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID inner join Sucursales on sucursalesproductos.SucursalID = sucursales.idSucursales ORDER BY productos.idProducto DESC limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema detectado al intentar cargar los datos los Producto-Sucursales del sistema ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                SiguientePagina.CerrarConexionBD1();
            }
        }

        private void CargarProductosSucursales()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarRegistros = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarRegistros.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarRegistros.RellenarTabla1("SELECT COUNT(idComuna) FROM sbepa2.comunas inner join sbepa2.regiones on comunas.idRegion = regiones.idRegion ").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                cargarRegistros.AbrirConexionBD1();
                dgbProductosSucursal.DataSource = cargarRegistros.RellenarTabla1("SELECT * FROM sbepa2.vistacambiospreciosproductos;");
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProductosSucursales();
            HacerInvisiblesyLimpiarCampos();
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

        private void dgbProductosSucursal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgbProductosSucursal.Rows[e.RowIndex];
            // Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de el DataGridView
                txtIDProducto.Text = Convert.ToString(fila.Cells["idProducto"].Value);
                txtUPC.Text = Convert.ToString(fila.Cells["UPC"].Value);
                txtSucursalID.Text = Convert.ToString(fila.Cells["SucursalID"].Value);
                txtNombreProducto.Text = Convert.ToString(fila.Cells["Nombre"].Value);


                ComandosBDMySQL CargarDatosFaltantes= new ComandosBDMySQL();
                //Se extraen los datos de los cambio de precio de producto en la sucursal
                try
                {
                    CargarDatosFaltantes.AbrirConexionBD1();
                    dgbPreciosCambios.DataSource = CargarDatosFaltantes.RellenarTabla1("SELECT FechaIngreso,PrecioCLP from PreciosProductos where idSucursalProducto = '" + Convert.ToString(fila.Cells["idSucursalesProductos"].Value) + " order by FechaIngreso desc';");

                    txtNombreTienda.Text = CargarDatosFaltantes.RellenarTabla1("SELECT nombre FROM sbepa2.tienda inner join sucursales on tienda.idTienda = sucursales.idTienda where sucursales.idSucursales = '"+ txtSucursalID.Text+ "';").Rows[0]["nombre"].ToString();
                    txtDireccionSucursal.Text = CargarDatosFaltantes.RellenarTabla1("SELECT Direccion FROM sbepa2.tienda inner join sucursales on tienda.idTienda = sucursales.idTienda where sucursales.idSucursales = '" + txtSucursalID.Text + "';").Rows[0]["Direccion"].ToString(); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer los datos de los cambios de precio del producto ERROR: "+ex.Message+"","Error Extraer Datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                finally
                {
                    CargarDatosFaltantes.CerrarConexionBD1();
                }
            }
        }

        private void txtBuscarEn_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            if (txtIDProducto.Text != "")
            {
                //Se abre el SaveFileDialog para fijar la ubicacion de guardado y el nombre del mismo
                SaveFileDialog DialogoGuardar = new SaveFileDialog();
                DialogoGuardar.FileName = "RegistroCambiosPrecioProducto" + txtIDProducto.Text + ".pdf";

                if (DialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //Se configura el tipo de documento (letter) y donde cerra almacenado
                        Document doc = new Document(PageSize.LETTER);
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"" + DialogoGuardar.FileName + "", FileMode.Create));

                        //Se genera el titulo y la informacion del creador
                        doc.AddTitle("Registro de Cambios de Precios de la ID del Producto: "+ txtIDProducto.Text+ "");
                        doc.AddCreator("Registro de Cambios de Precios de Producto ©SBEPA");

                        doc.Open();
                        //Se configura las fuentes del doc
                        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        //Se agrega el cuerpo del documento
                        doc.Add(new Paragraph("Registro de Cambios de Precios de un Producto   ID PRODUCTO: "+ txtIDProducto.Text+ "   SUCURSAL ID: "+ txtSucursalID.Text+ "    Fecha Registro: " + DateTime.Now.ToString() + ""));
                        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Application.StartupPath + @"\Logo.png");
                        imagen.BorderWidth = 0;
                        imagen.Alignment = Element.ALIGN_CENTER;
                        float percentage = 0.0f;
                        percentage = 100 / imagen.Width;
                        imagen.ScalePercent(percentage * 90);
                        doc.Add(imagen);
                        doc.Add(new Paragraph("------- ID del Producto-------"));
                        doc.Add(new Paragraph(txtIDProducto.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- UPC del Producto-------"));
                        doc.Add(new Paragraph(txtUPC.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- ID de la Sucursal asignada al Producto-------"));
                        doc.Add(new Paragraph(txtSucursalID.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- Nombre del Producto-------"));
                        doc.Add(new Paragraph(txtNombreProducto.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- Direccion de la Sucursal-------"));
                        doc.Add(new Paragraph(txtDireccionSucursal.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- Nombre de la Tienda a la que pertenece la Sucursal-------"));
                        doc.Add(new Paragraph(txtNombreTienda.Text));
                        doc.Add(new Paragraph(" "));

                        //Se genera la tabla
                        doc.Add(new Paragraph("------- Tabla de los Cambios de Precio del Producto-------"));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph(" "));

                        //se instancia el encabezado
                        Phrase objP = new Phrase();
                        //se genera el encabezado de la tabla
                        PdfPTable datatable = new PdfPTable(dgbPreciosCambios.ColumnCount);
                        for (int i = 0; i < dgbPreciosCambios.ColumnCount; i++)
                        {
                            objP = new Phrase(dgbPreciosCambios.Columns[i].HeaderText);
                            datatable.HorizontalAlignment = Element.ALIGN_CENTER;

                            datatable.AddCell(objP);
                        }

                        //se genera el cuerpo de la tabla
                        for (int i = 0; i < dgbPreciosCambios.RowCount; i++)
                        {
                            for (int j = 0; j < dgbPreciosCambios.ColumnCount; j++)
                            {
                                objP = new Phrase(dgbPreciosCambios[j, i].Value.ToString());
                                datatable.AddCell(objP);
                            }
                            datatable.CompleteRow();
                        }
                        doc.Add(datatable);
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph(" "));

                        //Se cierra el documento y se escribe
                        doc.Close();
                        writer.Close();

                        //Se notifica el guardado
                        MessageBox.Show("Se realizo correctamente la Generacion del Documento", "Documento Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Se abre el documento con el visor de PDF preterminado
                        try
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = @"" + DialogoGuardar.FileName + "";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar abrir el documento Generado ERROR: " + ex.Message, "Error Abrir Documento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar realizar la generacion del Documento ERROR: " + ex.Message, "Error Creacion Docuemento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto para poder almacenar sus cambios de precio", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
