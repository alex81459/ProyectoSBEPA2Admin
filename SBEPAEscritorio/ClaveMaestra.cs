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
    public partial class ClaveMaestra : Form
    {
        public ClaveMaestra()
        {
            InitializeComponent();
        }

        private void ClaveMaestra_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            String ClaveIngresada = txtClave.Text;

            FuncionesAplicacion calcularHASH = new FuncionesAplicacion();
            String ClaveIngresadaHASH = calcularHASH.TextoASha256(ClaveIngresada);

            ComandosBDMySQL VerificarClaveMaestra = new ComandosBDMySQL();
            try
            {
                VerificarClaveMaestra.AbrirConexionBD1();
                String VerificacionClave = VerificarClaveMaestra.RellenarTabla1("call sbepa2.AutorizarClaveMaestra('"+ClaveIngresadaHASH+"');").Rows[0][0].ToString();

                if (VerificacionClave == "Clave Maestra Correcta T93b5i@i3b5lsalw3YBFo")
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La Clave Ingresada no es correcta", "Error Clave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar autorizar el uso de la clave maestra  Error:" + ex.Message + "", "Error verificar clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                VerificarClaveMaestra.CerrarConexionBD1();
            }    
        }
    }
}
