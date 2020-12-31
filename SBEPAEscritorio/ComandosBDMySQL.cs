using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SBEPAEscritorio
{
    class ComandosBDMySQL
    {
        //Se extraen los parametros de la conexion, se crea el String de conexion para inciar con el conector
        //para poder almacenar, editar, borrar, ver campos, se desencripta la clave

        private static String ServerUsuario = "",ServerClave = "";
        private String StringConexionCompletaBD;

        private void DesencriptarAcceso()
        {
            //Se extraen los strings de usuario y clave de la BD almacenados en Properties.Settings, se desencriptan y se establece el String de Conexion
            FuncionesAplicacion Desencriptar = new FuncionesAplicacion();
            ServerUsuario = Desencriptar.DesencriptarTextoAES256(Properties.Settings.Default.ConexionInicialUsuario, "4UTjPgXZzZCHkhxR2QCntoyXexTzvJr6");
            ServerClave = Desencriptar.DesencriptarTextoAES256(Properties.Settings.Default.ConexionInicialClave, "4UTjPgXZzZCHkhxR2QCntoyXexTzvJr6");
            StringConexionCompletaBD = "Server=" + Properties.Settings.Default.ConexionInicialServer + ";Port=" + Properties.Settings.Default.ConexionInicialPuerto + "; Database=sbepa2;Uid=" + ServerUsuario + ";Pwd=" + ServerClave + "; SslMode = REQUIRED;";    
        }
   
        MySqlConnection conexionBD1;
        public MySqlConnection conexionMySql1()
        {
            //Conexion para la base de datos Mysql
            conexionBD1 = new MySqlConnection(StringConexionCompletaBD);
            return conexionBD1;
        }
        public void AbrirConexionBD1()
        {
            DesencriptarAcceso();
            //Se abre la conexion
            conexionMySql1();
            conexionBD1.Open();
        }

        public void IngresarConsulta1(String IngresarConsultaMysql1)
        {
            MySqlCommand comandoSQL1 = conexionBD1.CreateCommand();
            comandoSQL1.CommandType = CommandType.Text;
            comandoSQL1.CommandText = IngresarConsultaMysql1;
            comandoSQL1.ExecuteNonQuery();
        }

        public DataTable RellenarTabla1(String InstrucionMysqlRT1)
        {
            String ComandoSQL1 = InstrucionMysqlRT1;
            MySqlDataAdapter dataAdepterMySQL1 = new MySqlDataAdapter(InstrucionMysqlRT1, conexionBD1);
            DataTable ResultadoTabla1 = new DataTable();
            dataAdepterMySQL1.Fill(ResultadoTabla1);
            return ResultadoTabla1;
        }

        public DataTable RellenarTabla2(String InstrucionMysqlRT2)
        {
            String ComandoSQL2 = InstrucionMysqlRT2;
            MySqlDataAdapter dataAdepterMySQL2 = new MySqlDataAdapter(InstrucionMysqlRT2, conexionBD1);
            DataTable ResultadoTabla2 = new DataTable();
            dataAdepterMySQL2.Fill(ResultadoTabla2);
            return ResultadoTabla2;
        }

        public Boolean VerificarExistenciaDato1(String InstrucionMysqlBED1)
        {
            MySqlCommand comandoSQLVED1 = conexionBD1.CreateCommand();
            comandoSQLVED1.CommandType = CommandType.Text;
            comandoSQLVED1.CommandText = InstrucionMysqlBED1;
            comandoSQLVED1.ExecuteNonQuery();
            DataTable dataTableverificardato1 = new DataTable();
            MySqlDataAdapter dataAdapter1 = new MySqlDataAdapter(comandoSQLVED1);
            dataAdapter1.Fill(dataTableverificardato1);

            int i = 0;
            i = Convert.ToInt32(dataTableverificardato1.Rows.Count.ToString());

            if (i != 0)
            {
                return true;//el dato buscado en la BD si existe
            }
            else
            {
                return false;//El dato no ha sido encontrado dentro de la BD
            }
        }

        public void CerrarConexionBD1()
        {
            try
            {
                conexionBD1.Close();
                conexionBD1.Dispose();
            }
            catch (Exception)
            {

            }
            
        }

        public void IngresarImagen(string ConsultaBD, Image img)
        {
            //El InstrucionBD en el lugar de la imagen en la consulta debe tener @imagen para poder ser identificada
            MySqlConnection conexionBD = new MySqlConnection(StringConexionCompletaBD);
            conexionBD.Open();

                MemoryStream MS = new MemoryStream();
                img.Save(MS, System.Drawing.Imaging.ImageFormat.Png);

                byte[] Imagenes = MS.GetBuffer();
                MySqlCommand cmd = new MySqlCommand(ConsultaBD, conexionBD);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@imagen", Imagenes);
                cmd.ExecuteNonQuery();

            conexionBD.Close();
        }

        public Image ExtraerImagen(String consulta)
        {
            DesencriptarAcceso();
            MySqlConnection ConexionBD = new MySqlConnection(StringConexionCompletaBD);
            try
            {
                ConexionBD.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(consulta, ConexionBD);
                DataSet ds = new DataSet();
                da.Fill(ds);

                byte[] datos = new byte[0];
                DataRow dr = ds.Tables[0].Rows[0];
                datos = (byte[])dr[0];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);

                return System.Drawing.Bitmap.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar cargar la imagen, la imagen se encuentra daña o no es un archivo de imagen ERROR:"+ex.Message+"","Error Imagen No compatible",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                ConexionBD.Close();
            }
        }
    }
}
