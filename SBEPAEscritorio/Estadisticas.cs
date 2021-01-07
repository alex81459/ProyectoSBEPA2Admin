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
using System.Windows.Forms.DataVisualization.Charting;

namespace SBEPAEscritorio
{
    public partial class Estadisticas : Form
    {
        public Estadisticas()
        {
            InitializeComponent();
        }

        String IDSucursal = "";

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

        private void Barra_MouseUp(object sender, MouseEventArgs e)
        {
            //Si el se deja de dar click a la Barra, se deja de mover el Form
            mover = false;
        }

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            EstadisticasBuscarUsuarioyTienda abrirBuscar = new EstadisticasBuscarUsuarioyTienda();
            abrirBuscar.ShowDialog();
        }

        public void CargarInfoComponentes()
        {
            if (cbSucursalSeleccionada.Text != "")
            {
                panelBusqueda.Enabled = true;
                dgbComunasRegiones.Enabled = true;
                gbControlPaginas.Enabled = true;
                CargarProductosSucursal();
                CargarEstadisticasUsuario();
                GenerarGrafica();
            }
        }

        public void CargarEstadisticasUsuario()
        {
            ComandosBDMySQL CagarEstadisticasUsuarios = new ComandosBDMySQL();
            try
            {
                CagarEstadisticasUsuarios.AbrirConexionBD1();
                dgbVisitasSucursales.DataSource = CagarEstadisticasUsuarios.RellenarTabla1("call sbepa2.EstadisticaVisitasSucursal("+IDSucursal+");");
                dgbVisitaTienda.DataSource = CagarEstadisticasUsuarios.RellenarTabla1("call sbepa2.EstadisticaVisitasTienda("+ txtIDTienda.Text+ ");");
                DataTable DTOtrasEstadisitcas = new DataTable();
                DTOtrasEstadisitcas = CagarEstadisticasUsuarios.RellenarTabla1("call sbepa2.EstadisticaOtras("+txtIDUsuario.Text+", "+txtIDTienda.Text+");");
                txtVecesBaneado.Text = DTOtrasEstadisitcas.Rows[0]["XCantidadVecesBaneado"].ToString();
                txtInicioSesion.Text = DTOtrasEstadisitcas.Rows[0]["XCantidadInicioSesion"].ToString();
                txtProductosRegistrado.Text = DTOtrasEstadisitcas.Rows[0]["XCantidadProductosRegistradosTienda"].ToString();
                txtCambiosRealizados.Text = DTOtrasEstadisitcas.Rows[0]["XCantidadCambiosRealizados"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar cargas las estadisticas del Usuario y la Tienda ERROR: "+ex.Message+"","Error Cargar Estadisitcas",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                CagarEstadisticasUsuarios.CerrarConexionBD1();
            } 
        }

        public void GenerarGrafica()
        {


            ComandosBDMySQL ExtraerDatosTopProductos = new ComandosBDMySQL();
            try
            {
                //Se limpia el grafico
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }

                //Se extraen los datos y se cargan en el grafico
                ExtraerDatosTopProductos.AbrirConexionBD1();
                DataTable DTToppProducto = ExtraerDatosTopProductos.RellenarTabla1("SELECT CantidadVisita, Nombre, idProducto, UPC FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID WHERE SucursalID = "+IDSucursal+" order by CantidadVisita desc LIMIT 5;");

                chart1.Series["Series1"].LegendText = "T";

                Dictionary<string, int> dic = new Dictionary<string, int>();
                dic.Add("ID: " + DTToppProducto.Rows[0]["idProducto"].ToString() + "", Convert.ToInt32(DTToppProducto.Rows[0]["CantidadVisita"].ToString()));
                dic.Add("ID: " + DTToppProducto.Rows[1]["idProducto"].ToString() + "", Convert.ToInt32(DTToppProducto.Rows[1]["CantidadVisita"].ToString()));
                dic.Add("ID: " + DTToppProducto.Rows[2]["idProducto"].ToString() + "", Convert.ToInt32(DTToppProducto.Rows[2]["CantidadVisita"].ToString()));
                dic.Add("ID: " + DTToppProducto.Rows[3]["idProducto"].ToString() + "", Convert.ToInt32(DTToppProducto.Rows[3]["CantidadVisita"].ToString()));
                dic.Add("ID: " + DTToppProducto.Rows[4]["idProducto"].ToString() + "", Convert.ToInt32(DTToppProducto.Rows[4]["CantidadVisita"].ToString()));

                foreach (KeyValuePair<string, int> d in dic)
                {
                    chart1.Series["Series1"].Points.AddXY(d.Key, d.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar generar el grafico de barras del TOP de productos ERROR: "+ex.Message+"","Error Generar Grafico",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                ExtraerDatosTopProductos.CerrarConexionBD1();
            }
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
                    String CantidadRegistrosDetectados = (BuscarRegistros.RellenarTabla1("SELECT COUNT(CantidadVisita) FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%' and SucursalID = "+ IDSucursal+ ";").Rows[0][0].ToString());
                    txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbComunasRegiones.DataSource = BuscarRegistros.RellenarTabla2("call sbepa2.EstadisticaVisitasProductosBuscar("+ IDSucursal + ",'" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarProductosSucursal();
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

        private void CargarProductosSucursal()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarRegistros = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarRegistros.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarRegistros.RellenarTabla1("SELECT COUNT(CantidadVisita) FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID WHERE SucursalID = '"+IDSucursal+"'").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();
                cargarRegistros.AbrirConexionBD1();
                dgbComunasRegiones.DataSource = cargarRegistros.RellenarTabla1("SELECT CantidadVisita, Nombre, idProducto, UPC FROM sbepa2.productos inner join sucursalesproductos on productos.idProducto = sucursalesproductos.ProductosID WHERE SucursalID = '"+IDSucursal+"' order by CantidadVisita desc;");
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

        private void cbSucursalSeleccionada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se extrae el ID de la Sucursal Seleccionada
            ComandosBDMySQL ExtraerIDSucursal = new ComandosBDMySQL();
            try
            {
                //Si todo funciona bien, se carga la info de los demas compontentes de estadisticas
                ExtraerIDSucursal.AbrirConexionBD1();
                IDSucursal = ExtraerIDSucursal.RellenarTabla1("SELECT idSucursales, Direccion FROM sbepa2.tienda inner join sucursales on tienda.idTienda = sucursales.idTienda where sucursales.direccion = '"+ cbSucursalSeleccionada.Text+ "';").Rows[0]["idSucursales"].ToString();
                CargarInfoComponentes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar extrare el ID de la sucursal seleccionada para cargar sus datos ERROR: " + ex.Message + "", "Error extraer ID",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                ExtraerIDSucursal.CerrarConexionBD1();
            }
        }

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IDSucursal != "")
            {
                //Se abre el SaveFileDialog para fijar la ubicacion de guardado y el nombre del mismo
                SaveFileDialog DialogoGuardar = new SaveFileDialog();
                DialogoGuardar.FileName = "RegistroEstadisticaSucursal" + cbSucursalSeleccionada.Text + ".pdf";

                if (DialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //Se configura el tipo de documento (letter) y donde cerra almacenado
                        Document doc = new Document(PageSize.LETTER);
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"" + DialogoGuardar.FileName + "", FileMode.Create));

                        //Se genera el titulo y la informacion del creador
                        doc.AddTitle("Registro de Estadisiticas de Sucursl ID: "+IDSucursal+" Direccion: " + cbSucursalSeleccionada.Text);
                        doc.AddCreator("Registro de Estadisiticas ©SBEPA");

                        doc.Open();
                        //Se configura las fuentes del doc
                        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        //Se agrega el cuerpo del documento
                        doc.Add(new Paragraph("Registro de Estadisiticas de Sucursl ID: " + IDSucursal + " Direccion: " + cbSucursalSeleccionada.Text+" Fecha Registro: "+ DateTime.Now.ToString()+ ""));
                        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Application.StartupPath + @"\Logo.png");
                        imagen.BorderWidth = 0;
                        imagen.Alignment = Element.ALIGN_CENTER;
                        float percentage = 0.0f;
                        percentage = 100 / imagen.Width;
                        imagen.ScalePercent(percentage * 90);
                        doc.Add(imagen);
                        doc.Add(new Paragraph("------- ID del Usuario a Cargo de la Tienda-------"));
                        doc.Add(new Paragraph(txtIDUsuario.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- RUT del Usuario a Cargo de la Tienda-------"));
                        doc.Add(new Paragraph(txtRUTUsuario.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- Sucursal Seleccionada de la Tienda-------"));
                        doc.Add(new Paragraph(cbSucursalSeleccionada.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- Cantidad de Visitas de la Sucursal-------"));
                        doc.Add(new Paragraph(""+dgbVisitasSucursales.Rows[0].Cells[0].Value.ToString()+ " En la Sucursal con Direccion: " + dgbVisitasSucursales.Rows[0].Cells[1].Value.ToString() + ""));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- Cantidad de Visitas Total de Toda la Tienda-------"));
                        doc.Add(new Paragraph("" + dgbVisitaTienda.Rows[0].Cells[0].Value.ToString() + " En la Sucursal con Direccion: " + dgbVisitaTienda.Rows[0].Cells[1].Value.ToString() + ""));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("------- Otras Estadisticas del Usuario-------"));
                        doc.Add(new Paragraph("Cantidad de veces Baneado: "+ txtVecesBaneado.Text+ "   Cantidad de Inicios de Sesion: "+ txtInicioSesion.Text+ "   Todos los Productos Registrados de la Tienda: "+ txtProductosRegistrado.Text+ "   Todos los Cambios realizado por el Usuario: "+ txtCambiosRealizados.Text+ ""));
                        doc.Add(new Paragraph(" "));

                        //Se genera la tabla
                        doc.Add(new Paragraph("------- Tabla de las Visitas de los Productos-------"));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph(" "));

                        //se instancia el encabezado
                        Phrase objP = new Phrase();
                        //se genera el encabezado de la tabla
                        PdfPTable datatable = new PdfPTable(dgbComunasRegiones.ColumnCount);
                        for (int i = 0; i < dgbComunasRegiones.ColumnCount; i++)
                        {
                            objP = new Phrase(dgbComunasRegiones.Columns[i].HeaderText);
                            datatable.HorizontalAlignment = Element.ALIGN_CENTER;

                            datatable.AddCell(objP);
                        }

                        //se genera el cuerpo de la tabla
                        for (int i = 0; i < dgbComunasRegiones.RowCount - 1; i++)
                        {
                            for (int j = 0; j < dgbComunasRegiones.ColumnCount; j++)
                            {
                                objP = new Phrase(dgbComunasRegiones[j, i].Value.ToString());
                                datatable.AddCell(objP);
                            }
                            datatable.CompleteRow();
                        }
                        doc.Add(datatable);
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph(" "));

                        //Se añade el Grafico
                        doc.Add(new Paragraph("------- TOP 5 Productos mas Visitados-------"));
                        doc.Add(new Paragraph(""));
                        using (MemoryStream stream = new MemoryStream())
                        {
                            chart1.SaveImage(stream, ChartImageFormat.Png);
                            iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                            chartImage.ScalePercent(75f);
                            doc.Add(chartImage);
                        }



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
                MessageBox.Show("Debe seleccionar una Tienda y Luego su sucursal para Poder Guardar los datos de sus Estadisticas", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
