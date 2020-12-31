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


namespace SBEPARestauracionEmergencia
{
    public partial class SBEPARestauracionEmergencia : Form
    {
        public SBEPARestauracionEmergencia()
        {
            InitializeComponent();
        }

        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private String ConexionCompletaBD = "";

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
                            gbParametrosBD.Visible = false;
                            gbParametrosBD.Refresh();
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

                            MessageBox.Show("Se realizo correctamente la restaurancion de los datos del programa, desde la Ubicacion : " + txtUbicacionArchivoRestauracion.Text, "Restauracion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();

                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "El relleno entre caracteres no es válido y no se puede quitar.")
                            {
                                MessageBox.Show("La clave ingresada para desencriptar los datos de la copia de seguridad no es correcta", "Error Restauracion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                MessageBox.Show("Ha ocurrido un error al intentar restaurar la copia de seguridad ERROR: " + ex.Message, "Error Restauracion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
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

        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            ConexionCompletaBD = "Server=" + txtIpServidor.Text + ";Port=" + txtPuertoServidor.Text + "; Database=sbepa;Uid="+ txtUsuarioBD.Text+ "; Pwd="+ txtClaveBD.Text+ "; SslMode = Required;";

            MySqlConnection databaseConnection = new MySqlConnection(ConexionCompletaBD);

            try
            {
                databaseConnection.Open();
                databaseConnection.Close();
                MessageBox.Show("Conexion Establecida Correctamente con la Base de Datos, ahora puede proceder al proceso de restablecer la Copia de Seguridad", "Conexion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gbRestaurarBD.Visible = true;
                gbParametrosBD.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar conectar con la Base de Datos ERROR: " + ex.Message + "", "Error Conexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
