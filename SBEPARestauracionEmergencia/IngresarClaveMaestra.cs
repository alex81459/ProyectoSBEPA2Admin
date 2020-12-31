using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBEPARestauracionEmergencia
{
    public partial class ClaveMaestra : Form
    {
        public ClaveMaestra()
        {
            InitializeComponent();
        }

        String Valorclavemaestra = "1362fa377c40f370309e34ef9911b1703a1617c24af51d4e13ed18809ada6c71";
        //ym456th21vuw3f9

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            String ClaveIngresada = txtClave.Text;

            FuncionesAplicacion calcularHASH = new FuncionesAplicacion();
            String ClaveIngresadaHASH = calcularHASH.TextoASha256(ClaveIngresada);

            if (Valorclavemaestra == ClaveIngresadaHASH)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("La Clave Ingresada no es correcta","Error Clave",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
