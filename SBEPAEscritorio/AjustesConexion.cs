using MySql.Data.MySqlClient;
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
    public partial class AjustesConexion : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        private Boolean VerificarConexion = false; private Boolean GuardarConfiguracion = false;
        public AjustesConexion()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbServidorIP, "La Dirección IP (versión 4) del Servidor que Contiene la Base de Datos del sistema SBEPA2," + System.Environment.NewLine + "si esta Aplicación esta ahora dentro del Servidor debe de usar la IP 127.0.0.1 " + System.Environment.NewLine + "para indicar al programa que está corriendo dentro del servidor.");
            ttmensaje.SetToolTip(pbPuertoServidor, "El Puerto para Conectar Con la base de datos del Servidor, Pueden Ir desde el Puerto 0 al 65535" + System.Environment.NewLine + "normalmente la Base de Datos trabajara por el Puerto 3306");
            ttmensaje.SetToolTip(pbUsuario, "El Usuario que se utilizara para Conectarse a la Base de Datos, el cual debe mantenerce en secreto por" + System.Environment.NewLine + "motivos de seguridad");
            ttmensaje.SetToolTip(pbClave, "La Clave del Usuario que se utilizara para Conectarse a la Base de Datos, el cual debe mantenerce en" + System.Environment.NewLine + "secreto por motivos de seguridad");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Al guardar la nueva configuracion de conexion a la BD
            //verificar si los campos estan rellenos
            string UsuarioBD = "", ClaveBD="";

            FuncionesAplicacion Encriptar = new FuncionesAplicacion();
            UsuarioBD = Encriptar.EncriptarTextoAES256(txtUsuario.Text, "4UTjPgXZzZCHkhxR2QCntoyXexTzvJr6");
            ClaveBD = Encriptar.EncriptarTextoAES256(txtClave.Text, "4UTjPgXZzZCHkhxR2QCntoyXexTzvJr6");

            Properties.Settings.Default.ConexionInicialPuerto = txtPuertoServer.Text;
            Properties.Settings.Default.ConexionInicialServer = txtDireccionServer.Text;
            Properties.Settings.Default.ConexionInicialUsuario = UsuarioBD;
            Properties.Settings.Default.ConexionInicialClave = ClaveBD;
            Properties.Settings.Default.Save();

            //Se muestra mensaje y se cierra el form
            MessageBox.Show("Se guardo correctamente la informacion de la conexion con el servidor", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Question);
            GuardarConfiguracion = true;
        }
        private void btnRestaurarValores_Click(object sender, EventArgs e)
        {
            txtDireccionServer.Text = "127.0.0.1";
            txtPuertoServer.Text = "3306";
            txtUsuario.Text = "EscritorioAccesot6bs52";
            txtClave.Text = "8nt-3jb_08tvce92";
        }
        private void btnVerificarConexion_Click(object sender, EventArgs e)
        {
            String ConexionCompletaBD  = "";
                ConexionCompletaBD = "Server=" + txtDireccionServer.Text + ";Port=" + txtPuertoServer.Text + "; Database=sbepa2;Uid=" + txtUsuario.Text + ";Pwd=" + txtClave.Text + "; SslMode = Required;";
                MySqlConnection conexion3;
                conexion3 = new MySqlConnection(ConexionCompletaBD);
                try
                {
                    conexion3.Open();
                    MySqlDataAdapter dataAdepterMySQL1 = new MySqlDataAdapter("show status like 'ssl_cipher';", conexion3);
                    DataTable ResultadoTabla1 = new DataTable();
                    dataAdepterMySQL1.Fill(ResultadoTabla1);
                    MessageBox.Show("Se establecio exitosamente la conexion con la Base de Datos", "Conexion Exitosa BD", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    MessageBox.Show("La Base de Datos a devuelto los siguientes Parametros de seguridad de SSL: "+ ResultadoTabla1.Rows[0]["Value"].ToString(),"Parametros SSL",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    VerificarConexion = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La conexion con la Base de datos no se pudo establecer ERROR: " + ex.Message, "Error Conexion BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conexion3.Close();
        }
        private void AjustesConexion_Load(object sender, EventArgs e)
        {
            txtDireccionServer.Text = Properties.Settings.Default.ConexionInicialServer;
            txtPuertoServer.Text = Properties.Settings.Default.ConexionInicialPuerto;

            FuncionesAplicacion Desencriptar = new FuncionesAplicacion();

            txtUsuario.Text = Desencriptar.DesencriptarTextoAES256(Properties.Settings.Default.ConexionInicialUsuario, "4UTjPgXZzZCHkhxR2QCntoyXexTzvJr6");
            txtClave.Text = Desencriptar.DesencriptarTextoAES256(Properties.Settings.Default.ConexionInicialClave, "4UTjPgXZzZCHkhxR2QCntoyXexTzvJr6");
        }

        private void txtDireccionServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 46) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si se ingresa los numeros, el punto . , y borrar
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPuertoServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si se ingresa los numeros y borrar permite que se mantenga en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
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
    }
}
