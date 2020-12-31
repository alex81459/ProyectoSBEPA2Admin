using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBEPAEscritorio
{
    public partial class Login : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Se lanza el Form de los ajustes de la Conexion
            AjustesConexion abrirAjustes = new AjustesConexion();
            abrirAjustes.ShowDialog(); 
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            IniciarSesion();
        }

        private void IniciarSesion()
        {
            ComandosBDMySQL sistemaLogin = new ComandosBDMySQL();
            try
            {
                //Se inicia la conexion con la BD
                sistemaLogin.AbrirConexionBD1();
            }
            catch (Exception)
            {
                //Si la conexion falla se muestra un mensaje y se cierra el conector      
                MessageBox.Show("Error al Intentar Conectar a la Base de Datos, por favor revise que los ajustes de conexion son correctos", "Error al Conectar con la BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sistemaLogin.CerrarConexionBD1();
            }                   

            
            //se almacena en un datatable el resultado de la consulta del usuario 
            DataTable ResultadoConsulta = new DataTable();

            //Se Genera el Hash con la Clave ingresada por el usuario para ser enviada a la BD
            FuncionesAplicacion GenerarHash = new FuncionesAplicacion();
            String HashCalculadoClave = GenerarHash.TextoASha256(txtClave.Text);

            String externalip = "";
            try
            {
                //Se obtiene la IP publica
                externalip = new WebClient().DownloadString("http://icanhazip.com");
            }
            catch (Exception)
            {
                externalip = "Conexion local sin internet";
                MessageBox.Show("Error al intentar obtener la IP publica para iniciar sesion, revise su conexion a internet, quedara registrado como un inicio de sesion Conexion 'local sin internet'","Falla al conseguir IP Publica",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            try
            {
                //Se envia el usuario, el sha256 de la clave, la fecha actual y la ip publica
                ResultadoConsulta = sistemaLogin.RellenarTabla1("call sbepa2.IniciarSesionAdministrador('"+ txtUsuario.Text+ "', '"+ HashCalculadoClave + "', '"+ externalip + "');");

                
                if (ResultadoConsulta.Rows[0][0].ToString() != "Error Credenciales Invalidas")
                {
                    //Se las credenciales son invalidas se muestra un mensaje
                    if (ResultadoConsulta.Rows[0]["YEstado"].ToString() != "Activo")
                    {
                        MessageBox.Show("Su cuenta de Adminsitrador esta inactiva, contacte con el personal Actualmente a Cargo", "Cuenta Inactiva", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                            //Si las credenciales son validas  Se almacenan en variables compartidas el Rut, Usuario y el Tiempo de Inicio de Sesion
                            FuncionesAplicacion.NombreUsuario = ResultadoConsulta.Rows[0]["NombreUsuario"].ToString();
                            FuncionesAplicacion.RutAdmin = ResultadoConsulta.Rows[0]["RutAdmin"].ToString();
                            FuncionesAplicacion.FechaInicioSesion = ResultadoConsulta.Rows[0]["FechaInicioSesion"].ToString();
                            FuncionesAplicacion.IP = ResultadoConsulta.Rows[0]["IP"].ToString(); 
                            FuncionesAplicacion.IDadministrador = ResultadoConsulta.Rows[0]["YIDAdministrador"].ToString();

                            //Se limpian los campos, se muestra un mensaje y se lanza el menu
                            txtUsuario.Text = ""; txtClave.Text = "";
                            Menu lanzarMenu = new Menu();
                            lanzarMenu.Show();
                            this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("La clave o usuario ingresado son errores, por favor revise que sean correctamente escritos", "Error usuario o clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Text = "";
                    txtClave.Text = "";
                }
            }
            catch (Exception ex)
            {
                //se muestra en un mensaje si no puede iniciar sesion
                MessageBox.Show("Error al intentar iniciar sesion, por favor revise la configuracion de la conexion ERROR: "+ex.Message+" ", "Error al Iniciar Sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsuario.Text = ""; txtClave.Text = "";
            }
            finally
            {
                sistemaLogin.CerrarConexionBD1();
            }
        }


        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            AcercaDe abrirAcerca = new AcercaDe();
            abrirAcerca.ShowDialog();
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresClave(e);
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresUsuario(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "admin";txtClave.Text = "12345";
            IniciarSesion();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Se minimiza el form
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //Se cierra la Aplicacion totalmente
            Application.Exit();
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
