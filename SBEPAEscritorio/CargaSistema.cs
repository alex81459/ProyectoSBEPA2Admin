using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SBEPAEscritorio
{
    public partial class CargaSistema : Form
    {
        public CargaSistema()
        {
            InitializeComponent();
            revisarInstancia();
        }

        private void revisarInstancia()
        {
            if (Process.GetProcessesByName("SBEPAEscritorio").Length > 1)
            {
                //Se verifica si la aplicacion ya ha sido iniciada buscando el nombre del proceso, si es asi se muestra un mensaje y se cierra la APP
                MessageBox.Show("Solo se puede mantener abierta una instancia de la aplicacion a la vez", "Aplicacion ya abierta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                Application.Exit();
            }
            else
            {
                //Si la aplicacion no esta en ejecucion se verifican los archivos necesarios para operar(bibliotecas)
                FuncionesAplicacion VerificarHash = new FuncionesAplicacion();

                bool ContinuarInicio = true;
               
                if (File.Exists(Application.StartupPath+ @"\BouncyCastle.Crypto.dll") == false && "4A291236C5017BFDFB30FAD4C3DB4D0614C362C4B53409C610B12D483D7B26E6" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\BouncyCastle.Crypto.dll")))
                {
                    MessageBox.Show("Falta o Esta Dañada la libreria BouncyCastle.Crypto.dll la cual es necesaria para al encriptacion de datos", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\GMap.NET.Core.dll") == false && "58C755FCFC65CDDEA561023D736E8991F0AD69DA5E1378DEA59E98C5DB901B86" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\GMap.NET.Core.dll")))
                {
                    MessageBox.Show("Falta o Esta Dañada la libreria GMap.NET.Core.dll la cual es necesaria para la carga del mapa y el uso de coordenadas", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\GMap.NET.WindowsForms.dll") == false && "741E1A8F05863856A25D101BD35BF97CBA0B637F0C04ECB432C1D85A78EF1365" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\GMap.NET.WindowsForms.dll")))
                {
                    MessageBox.Show("Falta o Esta Dañada la libreria GMap.NET.WindowsForms.dll la cual es necesaria para graficar el mapa visualmente y trabajar con el", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\Google.Protobuf.dll") == false && "B6608721AFEC65FC5F6D130BE15F7C38DC376899FBDED42896C77911A89E8D13" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\Google.Protobuf.dll")))
                {
                    MessageBox.Show("Falta o Esta Dañada la libreria Google.Protobuf.dll la cual es necesaria para integrar el formato de intercambio de datos de Google (Necesario para trabajar con el mapa de Google)", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\MySql.Data.dll") == false && "83D5F32B402BF60B6AEABDB45CFEEAE292B2A590E9D351BF8072CF43658684AB" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\MySql.Data.dll")))
                {
                    MessageBox.Show("Falta o Esta Dañada la libreria MySql.Data.dll la cual es necesaria para trabajar con la Base de Datos", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\MySqlBackup.dll") == false && "F791D3E88E2E4D43F44D338B3B1A6AB05D124D08E85DE1588DB6474E01EED9C8" == VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\MySqlBackup.dll")))
                {
                    MessageBox.Show("Falta o Esta Dañada la libreria MySqlBackup.dll la cual es necesaria para crear y restaurar copias de seguridad de los datos", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\Renci.SshNet.dll") == false && "F6315ED146438F7702033681043F8FE9690ED39A2ABC9A192246424944A3A27A" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\Renci.SshNet.dll")))
                {
                    MessageBox.Show("Falta o Esta Dañada la libreria Renci.SshNet.dll la cual es necesaria para el paralelismo y soporte de SSH para trabajar con la base de datos con SSL", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\SBEPAEscritorio.exe.config") == false)
                {
                    MessageBox.Show("Falta o Esta Dañado el archivo SBEPAEscritorio.exe.config la cual es necesario, ya que contiene el archivo de configuracion de la APP, como los parametros para conectar con la Base de Datos", "Falta Libreria DDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\Ubiety.Dns.Core.dll") == false && "70DCBDF4F091A5B0A25CDCED5430205B8DDF6B4CCAE1CEE500CA151CE0F825E5" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\Ubiety.Dns.Core.dll")))
                {
                    MessageBox.Show("Falta la libreria Ubiety.Dns.Core.dll la cual es necesaria para trabajar con las copias de seguridad de la Base de Datos y mayor flexibilidad con las DNS","Falta Libreria DLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }
                if (File.Exists(Application.StartupPath + @"\itextsharp.dll ") == false && "5540BE0821D8303DC945FD2BD1C62082CDBA46260E5278985FA0866242DEADD2" != VerificarHash.ArchivoSHA256(File.OpenRead(Application.StartupPath + @"\itextsharp.dll")))
                {
                    MessageBox.Show("Falta la libreria itextsharp.dll la cual es necesaria para poder extraer datos del sistema y almacenarlos dentro de un documento de PDF", "Falta Libreria DLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ContinuarInicio = false;
                }

                ComandosBDMySQL ProbarConexion = new ComandosBDMySQL();
                try
                {
                    ProbarConexion.AbrirConexionBD1();
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Se ha detectado un Error al Intentar conectar con la Base de Datos del Programa, se recomienda revisar la configuracion de los parametros de conexion, en el menu de 'Login' en la opcion inferior 'Ajustes Conexion, si el problema continua a pesar de que los parametros estan correctos revise el Firewall y permita el acceso a la red","Error Conexion Base de Datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                finally
                {
                    ProbarConexion.CerrarConexionBD1();
                }

                //Si se no falta ningun archivo se continua con la carga
                if (ContinuarInicio)
                {
                    timer1.Start();
                }
                else
                {
                    //Si falta un archivo se cierra la APP
                    Application.Exit();
                }
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            //se espera unos segundos a que cargue el form login
            Login abrirLogin = new Login();
            timer1.Stop();
            abrirLogin.Show();
            this.Hide();
        }
    }
}
