using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace SBEPAEscritorio
{
    public partial class CopiaSeguridad : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private String ConexionCompletaBD = "";

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();
    
        public CopiaSeguridad()
        {
            //Se crea la instancia para desencriptar los parametros de conexion, 
            InitializeComponent();
            FuncionesAplicacion Desencriptar = new FuncionesAplicacion();
            //String ServerUsuario = Desencriptar.DesencriptarTextoAES256(Properties.Settings.Default.ConexionInicialUsuario, ClaveCifrado);
            //String ServerClave = Desencriptar.DesencriptarTextoAES256(Properties.Settings.Default.ConexionInicialClave, ClaveCifrado);
            ConexionCompletaBD = "Server=" + Properties.Settings.Default.ConexionInicialServer + ";Port=" + Properties.Settings.Default.ConexionInicialPuerto + "; Database=sbepa;Uid=Copia6wjo835Seg_uridad83m575; Pwd=Y39_i5y2b8-83yjgtw_8hkja5nsg99; SslMode = Required;";
        }

        private void CopiaSeguridadFinalizada()
        {
            groupBox2.Visible = true;
            txtClave1CopiaSeguridad.Text = "";
            txtClave2CopiaSeguridad.Text = "";
            txtRealizandoCopia.Visible = false;
            pbRealizandoCopia.Visible = false;
            btnRestaurarBD.Visible = true;
            txtUbicacionCopiaSeguridad.Text = "";
        }

        private void RestauracionBDFinalizada()
        {
            groupBox1.Visible = true;
            txtRealizandoRestauracion.Visible = false;
            pbRealizandoRestauracion.Visible = false;
            btnRestaurarBD.Visible = true;
            txtUbicacionArchivoRestauracion.Text = "";
            txtClaveRestaurarClave.Text = "";
        }

        private void btnSeleccionarUbicacion_Click(object sender, EventArgs e)
        {
            SaveFileDialog DialogoGuardar = new SaveFileDialog();
            DialogoGuardar.Filter = "Archivo Base de Datos SBEPA(*.dbSBEPA)| *.dbSBEPA";
            DialogoGuardar.Title = "Seleccione la Ubicacion de Guardado de la Copia de Seguridad de la Base de Datos";

            DateTime fecha = DateTime.Now;
            String FechaCortaActual = fecha.ToShortDateString();
            String HoraActual = fecha.ToString("hh:mm:ss").Replace(':', '-');

            DialogoGuardar.FileName = FechaCortaActual +"-"+ HoraActual + "CopiaSeguridad" + ".dbSBEPA";

            if (DialogoGuardar.ShowDialog() == DialogResult.OK)
            { 
                String UbicacionGuardar = DialogoGuardar.FileName;
                txtUbicacionCopiaSeguridad.Text = DialogoGuardar.FileName;
                pictureBox4.Visible = true;
                btnGuardarCopiaSeguridad.Visible = true;
            }
        }

        private void brtnBuscarArchivoRestauracion_Click(object sender, EventArgs e)
        {
            OpenFileDialog BuscarCopiaSegurudad = new OpenFileDialog();

            BuscarCopiaSegurudad.Filter = "Archivo Base de Datos SBEPA(*.dbSBEPA)| *.dbSBEPA";
            BuscarCopiaSegurudad.Title = "Seleccione la Ubicacion de la Copia de Seguridad de la Base de Datos a Restaurar";
            if (BuscarCopiaSegurudad.ShowDialog() == DialogResult.OK)
            {
                String UbicacionBDRespaldo = BuscarCopiaSegurudad.FileName;
                txtUbicacionArchivoRestauracion.Text = UbicacionBDRespaldo;
                pictureBox6.Visible = true;
                btnRestaurarBD.Visible = true;
            }    
        }

        private void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            if (txtClaveRestaurarClave.Text != "")
            {
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea restaurar los datos del programa?, ADVERTENCIA todos los datos actuales seran eliminados y remplazados por los datos de la copia de seguridad que selecciono", "Confirmacion Restauracion", MessageBoxButtons.YesNo);

                if (resultadoMensaje == DialogResult.Yes)
                {
                    ClaveMaestra verificarHacerCopia = new ClaveMaestra();
                    if (verificarHacerCopia.ShowDialog() == DialogResult.OK)
                    {
                        MySqlConnection conexion = new MySqlConnection(ConexionCompletaBD);
                        try
                        {
                            FuncionesAplicacion DesencriptarBD = new FuncionesAplicacion();

                            txtRealizandoRestauracion.Visible = true;
                            txtRealizandoRestauracion.Refresh();
                            groupBox1.Visible = false;
                            groupBox1.Refresh();
                            pbRealizandoRestauracion.Visible = true;
                            pbRealizandoRestauracion.Value = 0;

                            //Se leen los byts del archivo
                            txtRealizandoRestauracion.Text = "Extrayendo Copia Seguridad...";
                            txtRealizandoRestauracion.Refresh();
                            pbRealizandoRestauracion.Value = 10;
                            pbRealizandoRestauracion.Refresh();
                            byte[] CopiaSeguridad = File.ReadAllBytes(txtUbicacionArchivoRestauracion.Text);

                            //Se desenciptan los datos del archivo con la clave
                            txtRealizandoRestauracion.Text = "Desencriptando Copia Seguridad...";
                            txtRealizandoRestauracion.Refresh();
                            pbRealizandoRestauracion.Value = 30;
                            pbRealizandoRestauracion.Refresh();
                            CopiaSeguridad = DesencriptarBD.AESdesencriptar(CopiaSeguridad, txtClaveRestaurarClave.Text);

                            //Se descomprimen los datos del archivo
                            txtRealizandoRestauracion.Text = "Descomprimiendo Copia Seguridad...";
                            txtRealizandoRestauracion.Refresh();
                            pbRealizandoRestauracion.Value = 60;
                            pbRealizandoRestauracion.Refresh();
                            CopiaSeguridad = DesencriptarBD.DescomprimirDatos(CopiaSeguridad);
                            MemoryStream ms = new MemoryStream(CopiaSeguridad);

                            //Se enviaron los datos de la Copia de Seguridad a la BD
                            txtRealizandoRestauracion.Text = "Restaurando Copia Seguridad...";
                            txtRealizandoRestauracion.Refresh();
                            pbRealizandoRestauracion.Value = 80;
                            pbRealizandoRestauracion.Refresh();
                            MySqlCommand comando = new MySqlCommand();
                            MySqlBackup respaldo = new MySqlBackup(comando);
                            comando.Connection = conexion;
                            conexion.Open();
                            respaldo.ImportFromMemoryStream(ms);
                            txtRealizandoRestauracion.Refresh();
                            pbRealizandoRestauracion.Value = 100;

                            MessageBox.Show("Se realizo correctamente la restaurancion de los datos del programa, desde la Ubicacion : "+ txtUbicacionArchivoRestauracion.Text, "Restauracion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestauracionBDFinalizada();

                            //Se guarda la informacion de quien realizo la Copia se Seguridad
                            ComandosBDMySQL guardarRegistro = new ComandosBDMySQL();
                            guardarRegistro.AbrirConexionBD1();
                            guardarRegistro.IngresarConsulta1(@"call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Copia de Seguridad','Restaurar Copia Seguridad','Copia de Seguridad Restaurada desde la ubicacion: " + txtUbicacionArchivoRestauracion.Text + "');");
                            guardarRegistro.CerrarConexionBD1();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "El relleno entre caracteres no es válido y no se puede quitar.")
                            {
                                MessageBox.Show("La clave ingresada para desencriptar los datos de la copia de seguridad no es correcta", "Error Restauracion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                RestauracionBDFinalizada();
                            }
                            else
                            {
                                MessageBox.Show("Ha ocurrido un error al intentar restaurar la copia de seguridad ERROR: " + ex.Message, "Error Restauracion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                RestauracionBDFinalizada();
                            }  
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Muy bien falsa alarma, los datos actuales se mantendran y no seran restaurados de una copia anterior", "Restauracion Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe Ingresar la clave de 'Clave de la Copia Seguridad', no puede estar vacia", "Falta Clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarCopiaSeguridad_Click(object sender, EventArgs e)
        {
            if (txtClave1CopiaSeguridad.Text != "" && txtClave2CopiaSeguridad.Text != "")
            {
                //Se verifica que la clave sea segura
                FuncionesAplicacion verificarCLave = new FuncionesAplicacion();
                if (verificarCLave.VerificarContraceñaSegura(txtClave1CopiaSeguridad.Text))
                {
                    if (txtClave1CopiaSeguridad.Text == txtClave2CopiaSeguridad.Text)
                    {
                        string archivoUbicacionGuardado = txtUbicacionCopiaSeguridad.Text;

                        MySqlConnection conexion = new MySqlConnection(ConexionCompletaBD);
                        try
                        {
                            ClaveMaestra verificarHacerCopia = new ClaveMaestra();

                            //Se verifica la clave maestra
                            if (verificarHacerCopia.ShowDialog() == DialogResult.OK)
                            {
                                //Se guarda la informacion de quien realizo la Copia se Seguridad
                                ComandosBDMySQL guardarRegistro = new ComandosBDMySQL();
                                guardarRegistro.AbrirConexionBD1();
                                guardarRegistro.IngresarConsulta1(@"call sbepa.registro_cambio_datos_administrador('" + FuncionesAplicacion.IDadministrador + "', '" + (DateTime.Now.ToString(@"yyyy-MM-dd") + " " + DateTime.Now.ToString(@"HH:mm:ss")) + "','Copia de Seguridad','Realizar Copia Seguridad','Copia de Seguridad Realizada la cual se almaceno en: "+ txtUbicacionCopiaSeguridad.Text+ "');");
                                guardarRegistro.CerrarConexionBD1();

                                FuncionesAplicacion EncriptarCopiaBD = new FuncionesAplicacion();

                                MessageBox.Show("Se comenzara la copia el proceso de copia de Seguridad, Mantenga el equipo conectado a una red estable de energia y red para que el proceso sea exitoso", "Comienza Copia BD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtRealizandoCopia.Visible = true;
                                txtRealizandoCopia.Refresh();
                                pbRealizandoCopia.Visible = true;
                                pbRealizandoCopia.Value = 10;
                                pbRealizandoCopia.Refresh();
                                groupBox2.Visible = false;
                                groupBox2.Refresh();
                                //Si la clave es correcta se procede a realizar la copia de seguridad
                                MySqlCommand comando = new MySqlCommand();
                                MySqlBackup respaldo = new MySqlBackup(comando);
                                MemoryStream ms = new MemoryStream();
                                comando.Connection = conexion;
                                conexion.Open();
                                pbRealizandoCopia.Value = 10;
                                txtRealizandoCopia.Text = "Conectando BD...";
                                txtRealizandoCopia.Refresh();
                                pbRealizandoCopia.Refresh();
                                //se extablece que el maximo largo de la consulta de SQL para el respaldo sea de 1024*1024 bits = 1MB para no sobrecargar la BD 
                                respaldo.ExportInfo.MaxSqlLength = 1024 * 1024;
                                respaldo.ExportToMemoryStream(ms);
                                pbRealizandoCopia.Value = 30;
                                pbRealizandoCopia.Refresh();
                                txtRealizandoCopia.Text = "Extrayendo Datos BD...";
                                txtRealizandoCopia.Refresh();

                                byte[] CopiaSeguridad = ms.ToArray();
                                //Se comprimen los datos del archivo
                                pbRealizandoCopia.Value = 50;
                                pbRealizandoCopia.Refresh();
                                txtRealizandoCopia.Text = "Comprimiendo Datos BD...";
                                txtRealizandoCopia.Refresh();

                                CopiaSeguridad = EncriptarCopiaBD.ComprimirDatos(CopiaSeguridad);
                                //Se encriptan los datos comprimidos
                                pbRealizandoCopia.Value = 70;
                                pbRealizandoCopia.Refresh();
                                txtRealizandoCopia.Text = "Encriptando Copia de Seguridad...";
                                txtRealizandoCopia.Refresh();
                                CopiaSeguridad = EncriptarCopiaBD.AES_Encriptacion(CopiaSeguridad, txtClave2CopiaSeguridad.Text);
                                //Se almacenan los datos en un archivo
                                txtRealizandoCopia.Text = "Guardando Copia de Seguridad...";
                                txtRealizandoCopia.Refresh();
                                pbRealizandoCopia.Value = 90;
                                pbRealizandoCopia.Refresh();
                                File.WriteAllBytes(archivoUbicacionGuardado, CopiaSeguridad);
                                txtRealizandoCopia.Text = "Copia Guardada";
                                txtRealizandoCopia.Refresh();
                                pbRealizandoCopia.Value = 100;
                                pbRealizandoCopia.Refresh();

                                MessageBox.Show("Copia de Seguridad Realizada Correctamente, la cual se almaceno en : " + txtUbicacionCopiaSeguridad.Text, "Copia Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CopiaSeguridadFinalizada();
                            }
                            else
                            {
                                //Se muestra un mensaje para que el usuario ingrese la clave
                                MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar realizar la copia de seguridad ERROR: " + ex.Message, "Error Copia Seguriad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            CopiaSeguridadFinalizada();
                        }
                        finally
                        {
                            conexion.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las Claves deben de ser iguales, reingreselas", "Claves Diferentes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Ingresar la clave de 'Clave de Copia de Seguridad' y 'Repetir Clave Copia Seguridad', no pueden estar vacias", "Faltan Claves", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //se cierra el form
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

        private void txtClave1CopiaSeguridad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }

        private void txtClave2CopiaSeguridad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }

        private void txtClaveRestaurarClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }
    }
}
