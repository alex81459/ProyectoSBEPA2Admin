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
    public partial class RegistrosAccesos : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        public RegistrosAccesos()
        {
            InitializeComponent(); 
        }

        private String BuscarEn = "";

        private void RegistrosAccesos_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void CargarTabla()
        {
            nudPaginaActual.Value = 0;
            nudPaginaActual.Maximum = 0;

            ComandosBDMySQL CargarTabla = new ComandosBDMySQL();
            try
            {
                CargarTabla.AbrirConexionBD1();
                txtCantidadRegistro.Text = (CargarTabla.RellenarTabla1("SELECT COUNT(id_registro_login) as 'Cantidad' FROM sbepa.registro_login_administradores;").Rows[0]["Cantidad"].ToString());
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 100).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();

                dgbRegistros.DataSource = CargarTabla.RellenarTabla1("SELECT id_registro_login as 'N Registro',usuario as 'Usuario',rut as 'Rut',nombre as 'Nombre',fecha_inicio_sesion as 'Fecha Inicio Sesion',fecha_fin_sesion as 'Fecha Fin Sesion' FROM login_administradores inner join registro_login_administradores on login_administradores.idlogin_administradores = registro_login_administradores.id_administrador_login ORDER BY id_registro_login DESC limit 0,100;");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al intentar Cargar los registros de inicio de Sesion ERROR: "+ex.Message, "Error Cargar Registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CargarTabla.CerrarConexionBD1();
            }
        }

        public void ActivarParemetrosBuscar()
        {
            HacerInvisiblesyLimpiarCampos();
            txtBuscarEn.Visible = true;
            label2.Visible = true;
            picLupa.Visible = true;
            btnBuscar.Visible = true;
            dtpBuscarEn.Visible = false;
        }

        public void ActivarParemetrosFecha()
        {
            HacerInvisiblesyLimpiarCampos();
            label3.Visible = true;
            dtpBuscarEn.Visible = true;
            picLupa.Visible = true;
            btnBuscar.Visible = true;
        }

        private void cmbBuscarEn_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBuscarEn.Text == "Usuario")
            {
                BuscarEn = "usuario";
                ActivarParemetrosBuscar();
            }
            else if (cmbBuscarEn.Text == "Rut")
            {
                BuscarEn = "rut";
                ActivarParemetrosBuscar();
            }
            else if (cmbBuscarEn.Text == "Nombre")
            {
                BuscarEn = "nombre";
                ActivarParemetrosBuscar();
            }
            else if (cmbBuscarEn.Text == "Fecha Inicio Sesion")
            {
                BuscarEn = "fecha_inicio_sesion";
                ActivarParemetrosFecha();
            }
            else
            {
                BuscarEn = "fecha_fin_sesion";
                ActivarParemetrosFecha();
            }
        }

        private void imgRecargar_Click_1(object sender, EventArgs e)
        {
            CargarTabla();
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

        private void btnBuscarPagina_Click(object sender, EventArgs e)
        {
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbRegistros.DataSource = SiguientePagina.RellenarTabla1("SELECT id_registro_login as 'N Registro',usuario as 'Usuario',rut as 'Rut',nombre as 'Nombre',fecha_inicio_sesion as 'Fecha Inicio Sesion',fecha_fin_sesion as 'Fecha Fin Sesion' FROM login_administradores inner join registro_login_administradores on login_administradores.idlogin_administradores = registro_login_administradores.id_administrador_login ORDER BY id_registro_login DESC limit "+ Convert.ToInt32((nudPaginaActual.Value * 100)) + ",100;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema detectado al intentar cargar los datos de la siguiente pagina ERROR: "+ex.Message+"","Error Cargar Datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                SiguientePagina.CerrarConexionBD1();
            }
        }

        private void HacerInvisiblesyLimpiarCampos()
        {
            label2.Visible = false;
            txtBuscarEn.Visible = false;
            label3.Visible = false;
            dtpBuscarEn.Visible = false;
            txtPaginasDisponiblesBusqueda.Visible = false;
            label9.Visible = false;
            nudPaginaActualBuscar.Visible = false;
            label5.Visible = false;
            btnBuscar.Visible = false;
            picLupa.Visible = false;
            txtBuscarEn.Text = "";
            dtpBuscarEn.Text = "";
            nudPaginaActualBuscar.Value = 0;
        }

        private void ActivarControlpaginas()
        {
            label5.Visible = true;
            nudPaginaActualBuscar.Visible = true;
            label9.Visible = true;
            txtPaginasDisponiblesBusqueda.Visible = true;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEn.Visible == true)
            {
                if (txtBuscarEn.Text == "")
                {
                    MessageBox.Show("Debe ingresar algun dato en el cuadro de 'Parametros a Buscar' para realizar la busqueda en los registros de Accesos al Sistema","Falta Data a buscar",MessageBoxButtons.OK,MessageBoxIcon.Information); 
                }
                else
                {
                    ComandosBDMySQL BuscarRegistro = new ComandosBDMySQL();
                    try
                    {
                        BuscarRegistro.AbrirConexionBD1();
                        String CantidadRegistrosDetectados = (BuscarRegistro.RellenarTabla1("SELECT COUNT(id_registro_login) as 'Cantidad' FROM login_administradores inner join registro_login_administradores on login_administradores.idlogin_administradores = registro_login_administradores.id_administrador_login  Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%' ORDER BY id_registro_login DESC;").Rows[0]["Cantidad"].ToString());
                        txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 100).ToString();
                        nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 100);
                        ActivarControlpaginas();
                        dgbRegistros.DataSource = BuscarRegistro.RellenarTabla1("SELECT id_registro_login as 'N Registro',usuario as 'Usuario',rut as 'Rut',nombre as 'Nombre',fecha_inicio_sesion as 'Fecha Inicio Sesion',fecha_fin_sesion as 'Fecha Fin Sesion' FROM login_administradores inner join registro_login_administradores on login_administradores.idlogin_administradores = registro_login_administradores.id_administrador_login  Where " + BuscarEn + " like '%" + txtBuscarEn.Text + "%' ORDER BY id_registro_login DESC limit " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 100).ToString() + ",100;");   
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar obtener los registros buscados ERROR: "+ex.Message+"","Error Detectado",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    finally
                    {
                        BuscarRegistro.CerrarConexionBD1();
                    }
                }
            }
            if (dtpBuscarEn.Visible == true)
            {
                if (dtpBuscarEn.Text == "")
                {
                    MessageBox.Show("Debe ingresar alguna fecha en el cuadro de 'Fecha a Buscar' para realizar la busqueda en los registros de Accesos al Sistema", "Falta Data a buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ComandosBDMySQL BuscarRegistro = new ComandosBDMySQL();
                    try
                    {
                        BuscarRegistro.AbrirConexionBD1();
                        String CantidadRegistrosDetectados = (BuscarRegistro.RellenarTabla1("SELECT COUNT(id_registro_login) as 'Cantidad' FROM login_administradores inner join registro_login_administradores on login_administradores.idlogin_administradores = registro_login_administradores.id_administrador_login  Where " + BuscarEn + " like '%" + dtpBuscarEn.Text + "%' ORDER BY id_registro_login DESC;").Rows[0]["Cantidad"].ToString());
                        txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 100).ToString();
                        nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 100);
                        ActivarControlpaginas();
                        dgbRegistros.DataSource = BuscarRegistro.RellenarTabla1("SELECT id_registro_login as 'N Registro',usuario as 'Usuario',rut as 'Rut',nombre as 'Nombre',fecha_inicio_sesion as 'Fecha Inicio Sesion',fecha_fin_sesion as 'Fecha Fin Sesion' FROM login_administradores inner join registro_login_administradores on login_administradores.idlogin_administradores = registro_login_administradores.id_administrador_login  Where " + BuscarEn + " like '%" + dtpBuscarEn.Text + "%' ORDER BY id_registro_login DESC limit " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 100).ToString() + ",100;");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar obtener los registros buscados ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        BuscarRegistro.CerrarConexionBD1();
                    }
                }

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTabla();
            HacerInvisiblesyLimpiarCampos();
        }
    }
}
