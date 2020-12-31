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
    public partial class DirectorioArchivos : Form
    {
        public DirectorioArchivos()
        {
            InitializeComponent();
        }

        private void DirectorioArchivos_Load(object sender, EventArgs e)
        {
            CargarDirectoriosArchivos();
        }

        private void CargarDirectoriosArchivos(){
            //Se crea la instancia para cargar los datos desde la BD
            ComandosBDMySQL CargarDirectorios = new ComandosBDMySQL();
            try
            {
                CargarDirectorios.AbrirConexionBD1();

                //Se almacenando los datos en el datatable DirrectoriosArchivos
                DataTable DirrectoriosArchivos = new DataTable();
                DirrectoriosArchivos = CargarDirectorios.RellenarTabla1("SELECT * FROM sbepa.vista_directorios_archivos;");

                //Se extraen los Datos del Datatable DirrectoriosArchivos y se posicionan
                txtLogosTiendas.Text = DirrectoriosArchivos.Rows[0][1].ToString();
                txtImagenesSucursal.Text = DirrectoriosArchivos.Rows[0][2].ToString();
                txtImagenesProductos.Text = DirrectoriosArchivos.Rows[0][3].ToString();
                txtImagenesComprobantesProductos.Text = DirrectoriosArchivos.Rows[0][4].ToString();
                txtImagenesComprobantesActualizacionesPrecio.Text = DirrectoriosArchivos.Rows[0][5].ToString();
                txtImagenesProductosAñadidosUsuarios.Text = DirrectoriosArchivos.Rows[0][6].ToString();
                txtImagenesComprobantesProductosAñadidosUsuarios.Text = DirrectoriosArchivos.Rows[0][7].ToString();
                txtIPUbicacion.Text = Properties.Settings.Default.ConexionInicialServer;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al intentar cargar la informacion los Dirrectorios");
            }
            finally
            {
                CargarDirectorios.CerrarConexionBD1();
            }
        }


        private void btnModificarUbiaciones_Click(object sender, EventArgs e)
        {
            //Se hacen visibles los componentes necesarios para modificar las ubicaciones
            btnElegirLogosTiendas.Visible = true;
            btnElegirImagenesSucursal.Visible = true;
            btnElegirImagenesProductos.Visible = true;
            btnElegirComprobantesProductos.Visible = true;
            btnElegirComprobantesActualizacionPreciosProductos.Visible = true;
            btnElegirProductosAñadidosUsuario.Visible = true;
            btnElegirImagenesComprobantesProductosAñadidosUsuario.Visible = true;

            pictureBox1.Visible = false;
            btnModificarUbiaciones.Visible = false;
            btnGuardarUbicaciones.Visible = true;
            pictureBox2.Visible = true;

            txtLogosTiendas.ReadOnly = false;
            txtImagenesSucursal.ReadOnly = false;
            txtImagenesProductos.ReadOnly = false;
            txtImagenesComprobantesProductos.ReadOnly = false;
            txtImagenesComprobantesActualizacionesPrecio.ReadOnly = false;
            txtImagenesProductosAñadidosUsuarios.ReadOnly = false;
            txtImagenesComprobantesProductosAñadidosUsuarios.ReadOnly = false;
        }

        private void btnElegirLogosTiendas_Click(object sender, EventArgs e)
        {
            //Se abre un FolderBrowserDialog para que el usuario seleccione la ubicacion de la carpeta
            FolderBrowserDialog elegirLogosTiendas = new FolderBrowserDialog();
            elegirLogosTiendas.Description = "Seleccione el Directorio de los Logos de Las Tiendas";

            DialogResult dirSelected = elegirLogosTiendas.ShowDialog(this);

            if (dirSelected == DialogResult.OK)
            {
                txtLogosTiendas.Text = elegirLogosTiendas.SelectedPath;
            }
        }

        private void btnElegirImagenesSucursal_Click(object sender, EventArgs e)
        {
            //Se abre un FolderBrowserDialog para que el usuario seleccione la ubicacion de la carpeta
            FolderBrowserDialog elegirImagenesSucursal = new FolderBrowserDialog();
            elegirImagenesSucursal.Description = "Seleccione el Directorio de las Imagenes de las Sucursales";

            DialogResult dirSelected = elegirImagenesSucursal.ShowDialog(this);

            if (dirSelected == DialogResult.OK)
            {
                txtImagenesSucursal.Text = elegirImagenesSucursal.SelectedPath;
            }
        }

        private void btnElegirImagenesProductos_Click(object sender, EventArgs e)
        {
            //Se abre un FolderBrowserDialog para que el usuario seleccione la ubicacion de la carpeta
            FolderBrowserDialog elegirImagenesProductos = new FolderBrowserDialog();
            elegirImagenesProductos.Description = "Seleccione el Directorio de las Imagenes de los Productos";

            DialogResult dirSelected = elegirImagenesProductos.ShowDialog(this);

            if (dirSelected == DialogResult.OK)
            {
                txtImagenesProductos.Text = elegirImagenesProductos.SelectedPath;
            }
        }

        private void btnElegirComprobantesProductos_Click(object sender, EventArgs e)
        {
            //Se abre un FolderBrowserDialog para que el usuario seleccione la ubicacion de la carpeta
            FolderBrowserDialog elegirComprobantesProductos = new FolderBrowserDialog();
            elegirComprobantesProductos.Description = "Seleccione el Directorio de los Comprobantes de los Productos";

            DialogResult dirSelected = elegirComprobantesProductos.ShowDialog(this);

            if (dirSelected == DialogResult.OK)
            {
                txtImagenesComprobantesProductos.Text = elegirComprobantesProductos.SelectedPath;
            }
        }

        private void btnElegirComprobantesActualizacionPreciosProductos_Click(object sender, EventArgs e)
        {
            //Se abre un FolderBrowserDialog para que el usuario seleccione la ubicacion de la carpeta
            FolderBrowserDialog elegirComprobantesActualizacionPrecioProductos = new FolderBrowserDialog();
            elegirComprobantesActualizacionPrecioProductos.Description = "Seleccione el Directorio de los Comprobantes de la Actualizacion de los Precios de los Productos";

            DialogResult dirSelected = elegirComprobantesActualizacionPrecioProductos.ShowDialog(this);

            if (dirSelected == DialogResult.OK)
            {
                txtImagenesComprobantesActualizacionesPrecio.Text = elegirComprobantesActualizacionPrecioProductos.SelectedPath;
            }
        }

        private void btnElegirProductosAñadidosUsuario_Click(object sender, EventArgs e)
        {
            //Se abre un FolderBrowserDialog para que el usuario seleccione la ubicacion de la carpeta
            FolderBrowserDialog elegirProductosAñadidosUsuarios = new FolderBrowserDialog();
            elegirProductosAñadidosUsuarios.Description = "Seleccione el Directorio de las Imagenes de los Productos Añadidos Por los Usuarios";

            DialogResult dirSelected = elegirProductosAñadidosUsuarios.ShowDialog(this);

            if (dirSelected == DialogResult.OK)
            {
                txtImagenesProductosAñadidosUsuarios.Text = elegirProductosAñadidosUsuarios.SelectedPath;
            }
        }

        private void btnElegirImagenesComprobantesProductosAñadidosUsuario_Click(object sender, EventArgs e)
        {
            //Se abre un FolderBrowserDialog para que el usuario seleccione la ubicacion de la carpeta
            FolderBrowserDialog elegirComprobantesProductosAñadidosUsuarios = new FolderBrowserDialog();
            elegirComprobantesProductosAñadidosUsuarios.Description = "Seleccione el Directorio de los Comprobantes de los Productos Añadidos Por los Usuarios";

            DialogResult dirSelected = elegirComprobantesProductosAñadidosUsuarios.ShowDialog(this);

            if (dirSelected == DialogResult.OK)
            {
                txtImagenesComprobantesProductosAñadidosUsuarios.Text = elegirComprobantesProductosAñadidosUsuarios.SelectedPath;
            }
        }

        private void btnGuardarUbicaciones_Click(object sender, EventArgs e)
        {
            ClaveMaestra verificarClave = new ClaveMaestra();
            if (verificarClave.ShowDialog() == DialogResult.OK)
            {
                //Se verifica si la clave Maestra es Correcta
                ComandosBDMySQL GuardarDirrectorios = new ComandosBDMySQL();
                try
                {
                    GuardarDirrectorios.AbrirConexionBD1();

                    //Se cambia el simbolo \ por el simbolo \\, para que la base de datos pueda almacenar correctamente la ubicacion,
                    //ya que al ser almacenado transforma el \\ en \.
                    String LogosTiendas = txtLogosTiendas.Text;
                    LogosTiendas = LogosTiendas.Replace("\\", "\\\\");
                    String ImagenesSucursal = txtImagenesSucursal.Text;
                    ImagenesSucursal = ImagenesSucursal.Replace("\\", "\\\\");
                    String ImagenesProducto = txtImagenesProductos.Text;
                    ImagenesProducto = ImagenesProducto.Replace("\\", "\\\\");
                    String ImagenesComprobantesProductos = txtImagenesComprobantesProductos.Text;
                    ImagenesComprobantesProductos = ImagenesComprobantesProductos.Replace("\\", "\\\\");
                    String ImagenesComprobantesActualizacionesPrecio = txtImagenesComprobantesActualizacionesPrecio.Text;
                    ImagenesComprobantesActualizacionesPrecio = ImagenesComprobantesActualizacionesPrecio.Replace("\\", "\\\\");
                    String ImagenesProductosAñadidosUsuarios = txtImagenesProductosAñadidosUsuarios.Text;
                    ImagenesProductosAñadidosUsuarios = ImagenesProductosAñadidosUsuarios.Replace("\\", "\\\\");
                    String ImagenesComprobantesProductosAñadidosUsuarios = txtImagenesComprobantesProductosAñadidosUsuarios.Text;
                    ImagenesComprobantesProductosAñadidosUsuarios = ImagenesComprobantesProductosAñadidosUsuarios.Replace("\\", "\\\\");

                    //Se envian los datos a la BD
                    GuardarDirrectorios.IngresarConsulta1("call sbepa.actualizar_directorios_archivos('" + LogosTiendas + "', '" + ImagenesSucursal + "', '" + ImagenesProducto + "', '" + ImagenesComprobantesProductos + "', '" + ImagenesComprobantesActualizacionesPrecio + "', '" + ImagenesProductosAñadidosUsuarios + "', '" + ImagenesComprobantesProductosAñadidosUsuarios + "');");

                    //Se hacen invisibles los componentes inecesarios para almacenar los datos
                    btnElegirLogosTiendas.Visible = false;
                    btnElegirImagenesSucursal.Visible = false;
                    btnElegirImagenesProductos.Visible = false;
                    btnElegirComprobantesProductos.Visible = false;
                    btnElegirComprobantesActualizacionPreciosProductos.Visible = false;
                    btnElegirProductosAñadidosUsuario.Visible = false;
                    btnElegirImagenesComprobantesProductosAñadidosUsuario.Visible = false;
                    pictureBox1.Visible = true;
                    btnModificarUbiaciones.Visible = true;
                    btnGuardarUbicaciones.Visible = false;
                    pictureBox2.Visible = false;

                    txtLogosTiendas.ReadOnly = true;
                    txtImagenesSucursal.ReadOnly = true;
                    txtImagenesProductos.ReadOnly = true;
                    txtImagenesComprobantesProductos.ReadOnly = true;
                    txtImagenesComprobantesActualizacionesPrecio.ReadOnly = true;
                    txtImagenesProductosAñadidosUsuarios.ReadOnly = true;
                    txtImagenesComprobantesProductosAñadidosUsuarios.ReadOnly = true;

                    //se cargan los directorios recientemente actualizados y se muestra un mensaje de confirmacion
                    CargarDirectoriosArchivos();
                    MessageBox.Show("Cambios en los Directorios de los archivos Realizados Correctamente", "Cambios Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar almacnear los dirrectorios en la Base de Datos", "Error Almacenaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    GuardarDirrectorios.CerrarConexionBD1();
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar la Clave Maestra para continuar con el proceso","Necesaria Clave Maestra",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
    }
}
