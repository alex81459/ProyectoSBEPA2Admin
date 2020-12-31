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
    public partial class ProductosTienda : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        //Se instancian las tablas de datos
        //DT Tiendas Para guardar las sucursales sobtenidas de la BD 
        private DataTable DTTiendas = new DataTable();
        //DTSTiendasQuitadas para guardas las sucursales que son quitadas de la lista(cuando se pasan a dgbSucursalesSeleccionadas)
        private DataTable DTSTiendasQuitadas = new DataTable();
        //DTTiendasSeleccionadas las sucursales que fueron seleccionadas para ser asignadas a los productos
        private DataTable DTTiendasSeleccionadas = new DataTable();

        public ProductosTienda()
        {
            InitializeComponent();
            //Se agrega la info a los simbolos de pregunta de los campos
            ttmensaje.SetToolTip(pbInfoPrecioProducto, "El Precio que el Producto tendra en la Tienda");
            ttmensaje.SetToolTip(pbFinalOferta, "Si la oferta del producto tendra un final especificado");
            ttmensaje.SetToolTip(pbFechaFinal, "La Fecha de cuando terminara la oferta del producto");

            //Se agregan las columnas a el DataTable DTTiendas
            DTTiendas.Clear();
            DTTiendas.Columns.Add("ID Tienda");
            DTTiendas.Columns.Add("Nombre Tienda");

            //Se agregan las Columnas a el DataTable DTSTiendasQuitadas
            DTSTiendasQuitadas.Clear();
            DTSTiendasQuitadas.Columns.Add("ID Tienda");
            DTSTiendasQuitadas.Columns.Add("Nombre Tienda");

            //Se agregan las Columnas a el DataTable DTTiendas Seleccionados
            DTTiendasSeleccionadas.Clear();
            DTTiendasSeleccionadas.Columns.Add("IDTiendaProducto");
            DTTiendasSeleccionadas.Columns.Add("NombreTienda");
            DTTiendasSeleccionadas.Columns.Add("PrecioProducto");
            DTTiendasSeleccionadas.Columns.Add("Oferta");
            DTTiendasSeleccionadas.Columns.Add("FinalOferta");
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

        private void LimpiarCampos() {
            txtIDTienda.Text = "";
            txtListaNombreTienda.Text = "";
            nupListaPrecio.Value = 0;
            dtpFinalOferta.Text = "";
            txtFinalOfertaTexto.Text = "";

        }
      

        private void ProductosSucursales_Load(object sender, EventArgs e)
        {
            //Se cargan las Tiendas y se Establece el DateTimePicker en la fecha final de la oferta minima hoy
            CargarTiendas();
            dtpFinalOferta.MinDate = DateTime.Today;
        }

        private void CargarTiendas()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarSucursales = new ComandosBDMySQL();
            try
            {
                cargarSucursales.AbrirConexionBD1();
                DTTiendas = cargarSucursales.RellenarTabla1("SELECT * FROM sbepa.vista_productos_buscarcategoria;");
                dgbTienda.DataSource = DTTiendas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar los datos de las Sucursales ERROR:"+ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarSucursales.CerrarConexionBD1();
            }
        }

        private void cbPrecioOferta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrecioOferta.Checked == true)
            {
                //Si el checkbox precio oferta esta seleccionado se activa la oferta
                ActivarPrecioOferta();
            }
            else {
                //Si el checkbox precio oferta NO estaseleccionado se desactiva la oferta la oferta
                DesactivarPrecioOferta();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void btnAgregarSucursalProducto_Click(object sender, EventArgs e)
        {
            if (cbPrecioOferta.Checked == false)
            {
                //Si no esta seleccionado el precio oferta (precio normal)
                if (txtIDTienda.Text != "" && nupListaPrecio.Value != 0)
                {
                        //Ingresan los datos a dgbSucursalesSeleccionadas
                        DataRow rowañadir = DTTiendasSeleccionadas.NewRow();
                        rowañadir["IDTiendaProducto"] = txtIDTienda.Text;
                        rowañadir["NombreTienda"] = txtListaNombreTienda.Text;
                        rowañadir["PrecioProducto"] = Convert.ToString(nupListaPrecio.Value);
                        rowañadir["Oferta"] = "NO";
                        rowañadir["FinalOferta"] = "NO hay Oferta";
                        DTTiendasSeleccionadas.Rows.Add(rowañadir);

                    //Se recorre el DTTiendas para extraer los datos de la sucursasl para guardarlos en DTSTiendasQuitadas
                    String sIDBorrar = "";
                    foreach (DataRow row in DTTiendas.Rows)
                    {
                        if (row["ID Tienda"].ToString() == "" + txtIDTienda.Text + "")
                        {

                            //Se extraen los datos de DTTiendas
                            String IDTienda = row["ID Tienda"].ToString();
                            String NombreTienda = row["Nombre Tienda"].ToString();

                            //Se añade los campos a DTSTiendasQuitadas;
                            DataRow rowañadirquitados = DTSTiendasQuitadas.NewRow();
                            rowañadirquitados["ID Tienda"] = IDTienda;
                            rowañadirquitados["Nombre Tienda"] = NombreTienda;
                            DTSTiendasQuitadas.Rows.Add(rowañadirquitados);

                            //Se guarda la Sucursal a Borrar
                            sIDBorrar = row["ID Tienda"].ToString();
                        }
                    }

                    //Se eliminan las de DTSucursalesBorrar         
                    for (int i = DTTiendas.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = DTTiendas.Rows[i];
                        if (dr["ID Tienda"].ToString() == sIDBorrar)
                        {
                            dr.Delete();
                        }
                        DTTiendas.AcceptChanges();
                    }

                    //Se guardan los cambios del DataTable
                    DTTiendas.AcceptChanges();
                    DTSTiendasQuitadas.AcceptChanges();

                    //Se cargan los datos y se limpian los campos
                    dgbSucursalesSeleccionadas.DataSource = DTTiendasSeleccionadas;

                    //Se guardan los cambios del DataTable
                    DTTiendas.AcceptChanges();
                    DTSTiendasQuitadas.AcceptChanges();

                    //Se cargan los datos y se limpian los campos
                    dgbSucursalesSeleccionadas.DataSource = DTTiendasSeleccionadas;
                    LimpiarCampos();

                    MessageBox.Show("Se ha agregado el producto a la Sucursal","Sucursal Agregada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    //Se muestra mensaje si no selecciono una tienda y se le da un valor al producto
                    MessageBox.Show("Debe de Seleccionar una Tienda e Ingresar un Precio para el Producto","Faltan Datos",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                //Si esta seleccionado el precio oferta
                if (txtIDTienda.Text != "" && nupListaPrecio.Value != 0)
                {
                    String sOferta = ""; String sFinalOferta = "";Boolean IngresarDatos = false;
                    //Severifica si se selecciono una opcion de oferta
                    if (ckFechaFinal.Checked==true || ckFinalOferta.Checked == true)
                    {
                        if (ckFechaFinal.Checked == true)
                        {
                            if (ckFechaFinal.Checked == true)
                            {
                                IngresarDatos = true;
                                sOferta = "SI";
                                sFinalOferta = dtpFinalOferta.Text;
                            }
                        }

                        if (ckFinalOferta.Checked == true)
                        {
                            if (txtFinalOfertaTexto.Text != "")
                            {
                                IngresarDatos = true;
                                sOferta = "SI";
                                sFinalOferta = txtFinalOfertaTexto.Text;
                            }
                            else
                            {
                                IngresarDatos = false;
                                MessageBox.Show("Debe ingresar el Final de Oferta, NO puede estar vacio", "Falta Final Oferta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Seleccionar una opcion de Oferta, ya sea Fecha Final o Final Oferta","Faltan Datos",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }

                    //Si esta permitido ingresar datos
                    if (IngresarDatos == true)
                    {
                        //Ingresan los datos a dgbSucursalesSeleccionadas
                        DataRow rowañadir = DTTiendasSeleccionadas.NewRow();
                        rowañadir["IDTiendaProducto"] = txtIDTienda.Text;
                        rowañadir["NombreTienda"] = txtListaNombreTienda.Text;
                        rowañadir["PrecioProducto"] = Convert.ToString(nupListaPrecio.Value);
                        rowañadir["Oferta"] = sOferta;
                        rowañadir["FinalOferta"] = sFinalOferta;
                        DTTiendasSeleccionadas.Rows.Add(rowañadir);

                        //Se recorre el DTTiendas para extraer los datos de la sucursasl para guardarlos en DTSTiendasQuitadas
                        String sIDBorrar = "";
                        foreach (DataRow row in DTTiendas.Rows)
                        {
                            if (row["ID Tienda"].ToString() == "" + txtIDTienda.Text + "")
                            {

                                //Se extraen los datos de DTTiendas
                                String IDTienda = row["ID Tienda"].ToString();
                                String NombreTienda = row["Nombre Tienda"].ToString();

                                //Se añade los campos a DTSTiendasQuitadas;
                                DataRow rowañadirquitados = DTSTiendasQuitadas.NewRow();
                                rowañadirquitados["ID Tienda"] = IDTienda;
                                rowañadirquitados["Nombre Tienda"] = NombreTienda;
                                DTSTiendasQuitadas.Rows.Add(rowañadirquitados);

                                //Se guarda la Sucursal a Borrar
                                sIDBorrar = row["ID Tienda"].ToString();
                            }
                        }

                        //Se eliminan las de DTSucursalesBorrar         
                        for (int i = DTTiendas.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = DTTiendas.Rows[i];
                            if (dr["ID Tienda"].ToString() == sIDBorrar)
                            {
                                dr.Delete();
                            }
                            DTTiendas.AcceptChanges();
                        }

                        //Se guardan los cambios del DataTable
                        DTTiendas.AcceptChanges();
                        DTSTiendasQuitadas.AcceptChanges();

                        //Se cargan los datos y se limpian los campos
                        dgbSucursalesSeleccionadas.DataSource = DTTiendasSeleccionadas;
                        LimpiarCampos();

                        MessageBox.Show("Se ha agregado el producto a la Sucursal", "Sucursal Agregada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //Se muestra mensaje si no selecciono una sucursal y se le da un valor al producto
                    MessageBox.Show("Debe de Seleccionar una Sucursal e Ingresar un Precio para el Producto", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnQuitarSucursalSeleccionada_Click(object sender, EventArgs e)
        {
            //Se verifica si se ha seleccionado una sucursal
            if (txtIDTiendaSeleccionada.Text != "")
            {
                //Se recorre el DataTable Sucursales Quitados para recuperar los datos
                foreach (DataRow row in DTSTiendasQuitadas.Rows)
                {
                    if (row["ID Tienda"].ToString() == txtIDTiendaSeleccionada.Text)
                    {
                        //Ingresan los datos a dgbSucursalesSeleccionadas
                        DataRow rowrecuperar = DTTiendas.NewRow();
                        rowrecuperar["ID Tienda"] = row["ID Tienda"].ToString();
                        rowrecuperar["Nombre Tienda"] = row["Nombre Tienda"].ToString();

                        DTTiendas.Rows.Add(rowrecuperar);
                        //Se aceptan los cambios de la modificacion de la fila y se guardan
                    } 
                }
                DTSTiendasQuitadas.AcceptChanges();

                //Se eliminan los datos Restaurados de el DT Sucursales Quitados 
                for (int i = DTSTiendasQuitadas.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = DTSTiendasQuitadas.Rows[i];
                    if (dr["ID Tienda"].ToString() == txtIDTiendaSeleccionada.Text)
                        dr.Delete();
                }
                DTSTiendasQuitadas.AcceptChanges();

                //Se eliminan los datos de la DT Sucursales del Producto
                for (int i = DTTiendasSeleccionadas.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = DTTiendasSeleccionadas.Rows[i];
                    if (dr["IDTiendaProducto"].ToString() == txtIDTiendaSeleccionada.Text)
                        dr.Delete();
                }
                DTTiendasSeleccionadas.AcceptChanges();

                dgbTienda.DataSource = DTTiendas;
                MessageBox.Show("La Sucursal ha sido quitada del producto","Sucursal Quitada",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Sucursal del Producto para poder quitarla de la lista","Error Borrar Sucursal",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void dgbSucursalesSeleccionadas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgbSucursalesSeleccionadas.Rows[e.RowIndex];
                txtIDTiendaSeleccionada.Text = Convert.ToString(fila.Cells[0].Value);
                txtTiendaSeleccionada.Text = Convert.ToString(fila.Cells[1].Value);
            }
        }

        private void btnGuardarSucursales_Click(object sender, EventArgs e)
        {
            if (DTTiendasSeleccionadas.Rows.Count == 0)
            {
                MessageBox.Show("No ha seleccionado ninguna tienda para añadir al producto, mínimo debe de existir alguna tienda para que el producto sea vendido","Falta Seleccionar Tienda",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("Una vez haya seleccionado las tiendas en las cuales se vende el producto no podrá añadir nuevas, podrá añadir nuevas tiendas en la pestaña “Actualizar Precio Producto” en el Menú del Sistema cuando ya esté guardado el producto ¿Desea continuar? ", "¿Confirmar Tiendas Seleccionadas? ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resultadoMensaje == DialogResult.Yes)
                {
                    //Se crea una instancia especial para enviar los datos entre los 2 forms 
                    Productos f1 = Application.OpenForms.OfType<Productos>().SingleOrDefault();
                    f1.DTTiendasSeleccionadas.Clear();
                    f1.DTTiendasSeleccionadas = DTTiendasSeleccionadas.Clone();

                    //Se pasan los datos a la tabla 
                    foreach (DataRow row in DTTiendasSeleccionadas.Rows)
                    {
                        //Ingresan los datos a DTTiendasSeleccionas del Form Productos
                        DataRow TransferirRow = f1.DTTiendasSeleccionadas.NewRow();
                        TransferirRow["IDTiendaProducto"] = row["IDTiendaProducto"].ToString();
                        TransferirRow["NombreTienda"] = row["NombreTienda"].ToString();
                        TransferirRow["PrecioProducto"] = row["PrecioProducto"].ToString();
                        TransferirRow["Oferta"] = row["Oferta"].ToString();
                        TransferirRow["FinalOferta"] = row["FinalOferta"].ToString();

                        f1.DTTiendasSeleccionadas.Rows.Add(TransferirRow);
                        //Se aceptan los cambios de la modificacion de la fila y se guardan
                    }


                    f1.cbSeleccionadasTiendas.Checked = true;
                    f1.btnBuscarTienda.Enabled = false;

                    MessageBox.Show("Las Sucursales del Producto han sido Guardadas", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Se cierra el formulario
                    this.Close();
                }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgbTienda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgbTienda.Rows[e.RowIndex];
                txtIDTienda.Text = Convert.ToString(fila.Cells["IDTienda"].Value);
                txtListaNombreTienda.Text = Convert.ToString(fila.Cells["NombreTienda"].Value);
            }
        }
    }
    
}
