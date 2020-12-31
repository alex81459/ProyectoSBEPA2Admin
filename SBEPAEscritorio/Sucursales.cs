using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace SBEPAEscritorio
{
    public partial class Sucursales : Form
    {
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        //Se crea el marcador y capa de marcador para el mapa
        private GMarkerGoogle marker;
        private GMapOverlay markerOverlay;

        //Longitud y Latitus Inicial par el mapa
        private double LatIncial = -34.5925207129052;
        private double LngInicial = -70.992655903101;

        public Sucursales()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbID, "El ID (Identificador) el cual se usara para identificar las diferentes Sucursales" + System.Environment.NewLine + "este se genera automático cada vez que se hace un registro de una sucursal" + System.Environment.NewLine + "NO PUEDE SER CAMBIADO POR MANUALMENTE");
            ttmensaje.SetToolTip(pbNombre, "El Nombre con el cual se identificara la Sucursal de las demás" + System.Environment.NewLine + "Cada Sucursal debe tener un nombre que los diferencie, NO SE PUEDE REPETIR");
            ttmensaje.SetToolTip(pbDirreccion, "La Dirección Física de la Sucursal, el numero la dirección de la CALLE y la CIUDAD");
            ttmensaje.SetToolTip(pbComuna, "La Comuna donde se encuentra la sucursal, dentro de las registradas en el sistema");
            ttmensaje.SetToolTip(pbCorreoElectronico, "El Correo Electronico de la Sucursal para enctrar en contacto con ella");
            ttmensaje.SetToolTip(pbTelefono, "El Teléfono para entrar en contacto con la sucursal");
            ttmensaje.SetToolTip(pbDescripcion, "La Descripción de la Sucursal, la historia o información relevante para mostrar a los Visitantes");
            ttmensaje.SetToolTip(pbHorario, "El Horario de la tienda, que días abre, hora y cuando cierra");
            ttmensaje.SetToolTip(pbFotoSucursal, "Alguna foto de la imagen de la sucursal");
            ttmensaje.SetToolTip(pbCoordenadas, "Las Coordenadas Geográficas de la posición de la sucursal, las cuales se extraen del Mapa" + System.Environment.NewLine + "y no pueden ser rellenados manualmente, para extraer las coordenadas debe hacer doble " + System.Environment.NewLine + "click en la ubicacion en el Mapa");
            ttmensaje.SetToolTip(pbBuscarTienda, "Debe Seleccionar la Tienda a la cual estara conectada la sucursal que añadira o modificara");
        }

        private void CargarMapaLimpio()
        {
            //Se establece las propiedades del Mapa
            //El moton para mover el mapa
            gMapControl1.DragButton = MouseButtons.Left;
            //que se pueda mover el mapa
            gMapControl1.CanDragMap = true;
            //Se establece como proveedor de mapa GoogleMaps
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            //Las cordenadas de la posicion Inical
            gMapControl1.Position = new PointLatLng(LatIncial, LngInicial);
            //El Zoom, Minimo, el Maximo, y el Zoom Actual
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 20;
            gMapControl1.Zoom = 10;
            //que NO pueda realizar Scrooll con el maus sobre el mapa
            gMapControl1.AutoScroll = false;

            //Se configura la capa del marcador y el marcador
            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatIncial, LngInicial), GMarkerGoogleType.green);
            //Se agrega el marcador al Mapa
            markerOverlay.Markers.Add(marker);

            //se agrega el mapa y el amrcador al map control
            gMapControl1.Overlays.Add(markerOverlay);
        }

        private void Sucursales_Load(object sender, EventArgs e)
        {
            //al cargar el form se cargara el mapa limpio y las sucursales
            CargarMapaLimpio();
            CargarSucursales();
        }


        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //se obtienen los datos de la longitud y latitud del mapa haciendo doble click sobre alguna ubicacion
            double latitud = gMapControl1.FromLocalToLatLng(e.X,e.Y).Lat;
            double longitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            //Se envian los valores a los campos del form
            txtLatidud.Text = latitud.ToString();
            txtLongitud.Text = longitud.ToString();
            //Se agregan las coordenadas Unidas para la base de Datos
            txtCoordenadas.Text = latitud+"/"+longitud;

            //se crea al marcador para la nueva posicion seleccionada
            marker.Position = new PointLatLng(latitud,longitud);
            //se agrega el mensaje al marcador (tooltip)
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud:{0} \n Longitud:{1}", latitud, longitud);

            //Se agrega el mapa y el marcador al map control
            gMapControl1.Overlays.Add(markerOverlay);
    
        }

        private void btnSatelite_Click(object sender, EventArgs e)
        {
            //Se elige el tipo de mapa que se usara al seleccionar satelite
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
        }

        private void btnPlano_Click(object sender, EventArgs e)
        {
            //Se elige el tipo de mapa que se usara al seleccionar plano
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void CargarSucursales()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarSucursales = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarSucursales.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarSucursales.RellenarTabla1("SELECT COUNT(idsucursales) FROM sbepa2.sucursales;").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();

                cargarSucursales.AbrirConexionBD1();
                dgbSucursal.DataSource = cargarSucursales.RellenarTabla1("SELECT * FROM sbepa2.vistasucursales;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la tabla de Sucursales ERROR: "+ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarSucursales.CerrarConexionBD1();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackZoom.Value = Convert.ToInt32(gMapControl1.Zoom);
        }

        private void trackZoom_ValueChanged(object sender, EventArgs e)
        {
            //Se cambia la cantidad de zoom en el mapa con el trackBar
            gMapControl1.Zoom = trackZoom.Value;
        }

        private void btnNuevaSucursal_Click(object sender, EventArgs e)
        {
            //Se limpian los datos ingresados en los forms
            ActivarNuevo();
            LimpiarCamposForm();
        }

        private void LimpiarCamposForm()
        {
            //Se limpian los campos
            txtIDSucursal.Text = "";
            txtTienda.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtDescripcion.Text = "";
            txtHorario.Text = "";
            txtCoordenadas.Text = "";
            txtLatidud.Text = "";
            txtLongitud.Text = "";
            txtIDTienda.Text = "";
            txtTienda.Text = "";
            txtIDComuna.Text = "";
            txtNombreComuna.Text = "";
            txtCorreoElectronico.Text = "";
        }

        private void ActivarModificar()
        {
            pbBorrar.Visible = true;
            btnEliminarSucursal.Visible = true;
            btnBuscarTienda.Text = "Remplazar Tienda";
            btnBuscarImagen.Text = "Remplazar Imagen";
            btnBuscarComuna.Text = "Remplazar Comuna";
            btnGuardarSucursal.Text = "Guardar Modificacion";
        }

        private void ActivarNuevo()
        {
            pbBorrar.Visible = false;
            btnEliminarSucursal.Visible = false;
            btnBuscarTienda.Text = "Buscar Tienda";
            btnBuscarImagen.Text = "Subir Imagenes";
            btnBuscarComuna.Text = "Buscar Comuna";
            btnGuardarSucursal.Text = "Guardar Sucursal";
            btnBuscarImagen.Enabled = false;
        }

        private void btnBuscarTienda_Click(object sender, EventArgs e)
        {
            SucursalesBuscarTienda abrirTienda = new SucursalesBuscarTienda();
            abrirTienda.ShowDialog();
        }

        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            //Se envia al form para Buscar las Imagenes de la Sucursal
        }

        private Boolean VerificarContenidoCampos()
        {
            //Se verifica que todos los datos necesarios para el registro de la sucural esten ingresados
            if (txtIDTienda.Text == "" || txtIDComuna.Text == "" || txtDireccion.Text =="" || txtDescripcion.Text == "" || txtCoordenadas.Text =="")
            {
                MessageBox.Show("Verifique que todos los Datos necesarios para una sucursal estan introducidos, los campos deben de tener contenido, se debe de seleccionar ingresar La tienda a la que pertence, la comuna donde se ubica, la direccion fisica escrita y una descripcion", "Faltan Datos para realizar el registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnGuardarSucursal_Click(object sender, EventArgs e)
        {
           //Se verifica que los campos tengan contenido
            if (VerificarContenidoCampos() == true)
            {
                if (txtIDSucursal.Text == "")
                {
                    //Si el ID esta vacio se registra una nueva sucursal
                    //Se verifica que la sucursal no este registrada por medio de su direccion y coordenadas
                    ComandosBDMySQL verificarExistenciaSucursal = new ComandosBDMySQL();
                    try
                    {
                        verificarExistenciaSucursal.AbrirConexionBD1();
                        Boolean TiendaRegistrada = verificarExistenciaSucursal.VerificarExistenciaDato1("SELECT idSucursales FROM sbepa2.sucursales where direccion = '" + txtDireccion.Text + "' or Coordenadas = '" + txtCoordenadas.Text + "';");

                        if (TiendaRegistrada == false)
                        {
                            //si la sucursal no esta registrada se continua con el registro
                            /*try
                            {*/
                            //Si todo el proceso de registro se ejecuta correctamente se carga los nuevos registros de las tiendas
                            ComandosBDMySQL registrarSucursal = new ComandosBDMySQL();

                                    registrarSucursal.AbrirConexionBD1();
                                    //Se registra la sucursal
                                    registrarSucursal.IngresarConsulta1("call sbepa2.InsertarSucursal(" + txtIDTienda.Text + ", " + txtIDComuna.Text + ", '" + txtDireccion.Text + "', '" + txtCoordenadas.Text + "', '" + txtTelefono.Text + "', '" + txtCorreoElectronico.Text + "', '" + txtDescripcion.Text + "', '" + txtHorario.Text + "');");
                                    //Seregistra la accion
                                    registrarSucursal.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales', 'Insertar', 'REGISTRO LA SUCURSAL CON EL ID DE COMUNA: "+txtIDTienda.Text+", EL ID DE COMUNA: "+txtIDComuna.Text+", LA DIRECCION: "+ txtDireccion.Text+ ", EL TELFONO: "+ txtTelefono.Text + ", EL CORREO ELECTRONICO: "+txtCorreoElectronico.Text+", LA DESCRIPCION: "+txtDescripcion.Text+", El HORARIO: "+txtHorario.Text+", Y LAS COORDENADAS: "+txtCoordenadas.Text+"');");
                                    MessageBox.Show("La Sucursal ha sido correctamente registrada en el Sistema", "Sucursal Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarSucursales();
                                    LimpiarCamposForm();
                                    ActivarNuevo();
                            /*}
                            catch (Exception ex)
                            {
                                //Se muestra un mensaje de error si llega a ocurrir una excepcion
                                MessageBox.Show("Error al Intentar Registrar Los Datos de la Nueva Sucursal ERROR:"+ex.Message, "Error Registrar Sucursal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }*/
                        }
                        else
                        {
                            //si la tienda esta registrada, se muestra un mensaje
                            MessageBox.Show("La Sucursal  Ya se encuentra registrada en el Sistema, verifique el la direccion ingresada y las coordenadas de la misma", "Sucursal ya Registrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar verificar la existencia de la sucursal ERROR:"+ex.Message,"Error Verificacion",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        verificarExistenciaSucursal.CerrarConexionBD1();
                    }
                }
                else
                {
                    //Si el ID no esta vacio se actualiza la Tienda}
                    ComandosBDMySQL actualizarSucursal = new ComandosBDMySQL();
                    /*try
                    {*/
                    //Si todo el proceso de registro se ejecuta correctamente se carga los nuevos registros de las tiendas  
                        actualizarSucursal.AbrirConexionBD1();
                        actualizarSucursal.IngresarConsulta1("call sbepa2.ActualizarSucursales("+txtIDSucursal.Text+", "+txtIDTienda.Text+", "+txtIDComuna.Text+", '"+txtDireccion.Text+"', '"+txtCoordenadas.Text+"', '"+txtTelefono.Text+"', '"+txtCorreoElectronico.Text+"', '"+txtDescripcion.Text+"', '"+txtHorario.Text+"');");

                        actualizarSucursal.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales', 'Actualizar', 'ACTUALIZO LA SUCURSAL CON EL ID DE SUCURSAL: "+txtIDSucursal.Text+", DE COMUNA: " + txtIDTienda.Text + ", EL ID DE COMUNA: " + txtIDComuna.Text + ", LA DIRECCION: " + txtDireccion.Text + ", EL TELFONO: " + txtTelefono.Text + ", EL CORREO ELECTRONICO: " + txtCorreoElectronico.Text + ", LA DESCRIPCION: " + txtDescripcion.Text + ", El HORARIO: " + txtHorario.Text + ", Y LAS COORDENADAS: " + txtCoordenadas.Text + "');");

                        MessageBox.Show("La Sucursal ha sido correctamente Modificada en el Sistema", "Sucursal Modificada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarSucursales();
                        ActivarNuevo();
                        LimpiarCamposForm();
                   /* }
                    catch (Exception ex)
                    {
                        //Se muestra un mensaje de error si llega a ocurrir una excepcion
                        MessageBox.Show("Error al Intentar Actualiza Los Datos de la Tienda ERROR: "+ex.Message, "Error Actualizar Datos Tienda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        actualizarTienda.CerrarConexionBD1();
                    }*/
                }
            }
        }


        private void btnEliminarSucursal_Click(object sender, EventArgs e)
        {
            if (txtIDSucursal.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("Advertencia, Al borrar la Sucursal no solo se borrara la misma, tambien todos los productos asociados a ella ¿Desea Eliminar la Sucursal Seleccionada?", "Informacion Borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Se contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                        //si ola tienda no tiene registros se puede borrar
                        ClaveMaestra verificarEliminarClave = new ClaveMaestra();
                        ComandosBDMySQL EliminarSucursal = new ComandosBDMySQL();
                        /*try
                        {*/
                            //Se verifica la clave maestra
                            if (verificarEliminarClave.ShowDialog() == DialogResult.OK)
                            {
                                //Si la clave es correcta se procede a eliminarlo del sistema
                                EliminarSucursal.AbrirConexionBD1();
                                EliminarSucursal.IngresarConsulta1("call sbepa2.EliminarSucursal("+txtIDSucursal.Text+");");
                                EliminarSucursal.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales', 'Eliminar', 'ELIMINO LA SUCURSAL CON EL ID DE SUCURSAL: " + txtIDSucursal.Text + "');");
                                LimpiarCamposForm();
                                ActivarNuevo();
                                CargarSucursales();
                                MessageBox.Show("Sucursal Eliminada Correctamente", "Proceso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //Se muestra un mensaje para que el usuario ingrese la clave
                                MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        /*}
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al Intentar Eliminar a la Tienda del Sistema ERROR:"+ex.Message, "Error Borrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            EliminarSucursal.CerrarConexionBD1();
                        }*/
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Sucursal para Eliminar", "No hay Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgbTiendas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se activa la capacidad de modificar los datos
                ActivarModificar();
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbSucursal.Rows[e.RowIndex];
                txtIDSucursal.Text = Convert.ToString(fila.Cells["idSucursales"].Value);
                txtIDTienda.Text = Convert.ToString(fila.Cells["idTienda"].Value);
                txtIDComuna.Text = Convert.ToString(fila.Cells["idComuna"].Value);
                txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);
                txtTelefono.Text = Convert.ToString(fila.Cells["Telefono"].Value);
                txtCorreoElectronico.Text = Convert.ToString(fila.Cells["CorreoElectronico"].Value);
                txtDescripcion.Text = Convert.ToString(fila.Cells["Descripcion"].Value);
                txtHorario.Text = Convert.ToString(fila.Cells["Horarios"].Value);
                txtCoordenadas.Text = Convert.ToString(fila.Cells["Coordenadas"].Value);

                btnBuscarImagen.Enabled = true;

                //Se extrae el nombre de la tienda y comuna
                ComandosBDMySQL buscarNombres = new ComandosBDMySQL();
                try
                {
                    buscarNombres.AbrirConexionBD1();
                    txtTienda.Text = buscarNombres.RellenarTabla1("call sbepa2.BuscarTienda('Idtienda', '"+txtIDTienda.Text+"', 0, 1);").Rows[0]["nombre"].ToString();
                    txtNombreComuna.Text = buscarNombres.RellenarTabla1("call sbepa2.BuscarComunas('idComuna', '"+txtIDComuna.Text+"', 0, 1);").Rows[0]["NombreComuna"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar buscar los nombres de la tienda y comuna ERROR: "+ex.Message+"","Error al Cargar Tienda y Comuna",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                finally
                {
                    buscarNombres.CerrarConexionBD1();
                }

                try
                {
                    //Se separa la longitud de la latidud
                    String CoordendaSerparar = txtCoordenadas.Text;
                    //Se gaurdan las separacioned entro de un arreglo
                    String[] Separador = CoordendaSerparar.Split('/');

                    //Se extraen desde el arreglo
                    txtLatidud.Text = Separador[0];
                    txtLongitud.Text = Separador[1];

                    //Se posiciona la Ubicacion de la tienda
                    marker.Position = new PointLatLng(Convert.ToDouble(txtLatidud.Text), Convert.ToDouble(txtLongitud.Text));
                    marker.ToolTipText = string.Format("Sucursal: " + txtDireccion.Text + " Latitud: {0} \n Longitud: {1}", Convert.ToDouble(txtLatidud.Text), Convert.ToDouble(txtLongitud.Text));
                    gMapControl1.Position = new PointLatLng(Convert.ToDouble(txtLatidud.Text), Convert.ToDouble(txtLongitud.Text));
                    gMapControl1.Zoom = 17;
                    trackZoom.Value = 17;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer las coordenadas, procesarlas y cargarlas ERROR: " + ex.Message + "", "ERROR cargar cordenadas",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresNombre(e);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDireccion(e);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtHorario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresHorario(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresTelefono(e);
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

        private void btnBuscarUbicacion_Click(object sender, EventArgs e)
        {
            if (txtBuscarUbicacion.Text != "")
            {
                GeoCoderStatusCode statusCode;
                var pointLatLng = GoogleMapProvider.Instance.GetPoint(txtBuscarUbicacion.Text.Trim(), out statusCode);
                if (statusCode == GeoCoderStatusCode.G_GEO_SUCCESS)
                {
                    marker.Position = new PointLatLng(Convert.ToDouble(pointLatLng?.Lat.ToString()), Convert.ToDouble(pointLatLng?.Lng.ToString()));
                    marker.ToolTipText = string.Format("Ubicacion: " + txtBuscarUbicacion.Text + " Latitud: {0} \n Longitud: {1}", Convert.ToDouble(pointLatLng?.Lat.ToString()), Convert.ToDouble(pointLatLng?.Lng.ToString()));
                    gMapControl1.Position = new PointLatLng(Convert.ToDouble(pointLatLng?.Lat.ToString()), Convert.ToDouble(pointLatLng?.Lng.ToString()));
                    gMapControl1.Zoom = 17;
                    trackZoom.Value = 17;
                }
                else
                {
                    MessageBox.Show("No encontrado");
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el Nombre de un lugar a Buscar");
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

        private void txtCorreoElectronico_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresCorreo(e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEn.Text == "")
            {
                MessageBox.Show("Debe ingresar algun dato a buscar en el campo 'Paremetros a Buscar'", "Faltan Datos para la Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ComandosBDMySQL BuscarTiendas = new ComandosBDMySQL();
                try
                {
                    //Se cargan los datos necesarios para la busquedam y el ordenamiento de las paginas
                    BuscarTiendas.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarTiendas.RellenarTabla1("SELECT COUNT(idSucursales) FROM sbepa2.sucursales Where "+ cmbBuscarEn.Text+ " like '%"+ txtBuscarEn.Text+ "%';").Rows[0][0].ToString());
                    txtSucursalesEncontrados.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbSucursal.DataSource = BuscarTiendas.RellenarTabla2("call sbepa2.BuscarSucursales('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
                    label19.Visible = true;
                    txtSucursalesEncontrados.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar obtener las Sucursales buscadas ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarTiendas.CerrarConexionBD1();
                }
            }
        }

        private void ActivarControlpaginas()
        {
            label5.Visible = true;
            nudPaginaActualBuscar.Visible = true;
            label9.Visible = true;
            txtPaginasDisponiblesBusqueda.Visible = true;
            label11.Visible = true;
            label6.Visible = true;
            label26.Visible = true;
            label1.Visible = true;
        }

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbSucursal.DataSource = SiguientePagina.RellenarTabla1("SELECT * FROM sbepa2.sucursales limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema detectado al intentar cargar los datos de la siguiente pagina ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                SiguientePagina.CerrarConexionBD1();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarSucursales();
            HacerInvisiblesyLimpiarCampos();
        }

        private void HacerInvisiblesyLimpiarCampos()
        {
            label7.Visible = false;
            txtBuscarEn.Visible = false;
            label5.Visible = false;
            nudPaginaActualBuscar.Visible = false;
            label9.Visible = false;
            txtPaginasDisponiblesBusqueda.Visible = false;
            txtBuscarEn.Text = "";
            nudPaginaActualBuscar.Value = 0;
            txtSucursalesEncontrados.Text = "?????????";
            txtSucursalesEncontrados.Visible = false;
            label19.Visible = false;
            txtBuscarEn.Visible = true;
            label8.Visible = true;
            label6.Visible = false;
            label11.Visible = false;
            label19.Visible = false;
            label26.Visible = false;
            label1.Visible = false;
        }

        private void cmbBuscarEn_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ActivarBusqueda();
        }

        private void ActivarBusqueda()
        {
            label7.Visible = true;
            txtBuscarEn.Visible = true;
            btnBuscar.Visible = true;
            picLupa.Visible = true;
        }

        private void btnBuscarComuna_Click(object sender, EventArgs e)
        {
            SucursalesBuscarComuna abrirBuscarSucursal = new SucursalesBuscarComuna();
            abrirBuscarSucursal.ShowDialog();
        }

        private void btnBuscarImagen_Click_1(object sender, EventArgs e)
        {
            SucursalesSubirImagenes abrirSubir = new SucursalesSubirImagenes();
            abrirSubir.txtIDSucursal.Text = txtIDSucursal.Text;
            abrirSubir.IDSucursal = txtIDSucursal.Text;
            abrirSubir.ShowDialog();
        }
    }
}

