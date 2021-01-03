using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBEPAEscritorio
{
    public partial class RegistrosCambiosAdmin : Form
    {
        public RegistrosCambiosAdmin()
        {
            InitializeComponent();
            CargarInfoRegistrosAdmins();
        }

        String BuscarEn = "";

        private Point posicion = Point.Empty;
        private bool mover = false;

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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEn.Text == "")
            {
                MessageBox.Show("Debe ingresar algun dato a buscar en el campo 'Parametros a Buscar'", "Faltan Datos para la Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ComandosBDMySQL BuscarRegistrosUsuarios = new ComandosBDMySQL();
                try
                {
                    //Se cargan los datos necesarios para la busquedam y el ordenamiento de las paginas
                    BuscarRegistrosUsuarios.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarRegistrosUsuarios.RellenarTabla1("SELECT COUNT(idRegistrosCambiosAdministradores) FROM sbepa2.registroscambiosadministradores Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';").Rows[0][0].ToString());
                    txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                    txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                    nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                    ActivarControlpaginas();
                    dgbCambiosAdmins.DataSource = BuscarRegistrosUsuarios.RellenarTabla2("call sbepa2.BuscarRegistroCambioAdministradores('" + cmbBuscarEn.Text + "', '" + txtBuscarEn.Text + "', " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ", 50);");
                    lblRegistrosEncontrados.Visible = true;
                    txtRegistrosEncontradosSuperior.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar buscar los registros de los cambios de los Administradores buscadas ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarRegistrosUsuarios.CerrarConexionBD1();
                }
            }
        }

        private void ActivarControlpaginas()
        {
            nudPaginaActualBuscar.Visible = true;
            lblBuscarPor.Visible = true;
            txtPaginasDisponiblesBusqueda.Visible = true;
            lblPaginasDisponibles.Visible = true;
            lblPaginaActualBusqueda.Visible = true;
            lblPaginasDisponibles.Visible = true;
            label1.Visible = true;
        }

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            //Se avanza a la siguiente pagina
            ComandosBDMySQL SiguientePagina = new ComandosBDMySQL();
            try
            {
                SiguientePagina.AbrirConexionBD1();
                dgbCambiosAdmins.DataSource = SiguientePagina.RellenarTabla1("SELECT * FROM sbepa2.registroscambiosadministradores ORDER BY registroscambiosadministradores.idRegistroLoginAdministradores DESC limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema detectado al intentar cargar los datos de los registros de los cambios de los Usuarios ERROR: " + ex.Message + "", "Error Cargar Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                SiguientePagina.CerrarConexionBD1();
            }
        }

        private void cmbBuscarEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBusqueda();
        }

        private void ActivarBusqueda()
        {
            lblTitulo.Visible = true;
            txtBuscarEn.Visible = true;
            btnBuscar.Visible = true;
            picLupa.Visible = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarInfoRegistrosAdmins();
            HacerInvisiblesyLimpiarCampos();
        }

        private void CargarInfoRegistrosAdmins()
        {
            //Se crea la instancia para conectar con la BD, se extrae la tabla, si falla se muestra un mensaje y
            //siempre se cierra la conexion
            ComandosBDMySQL cargarSucursales = new ComandosBDMySQL();
            try
            {
                //Se verifica la cantidad de tiendas, se rellena la cantidad de paginas y sus opciones para avanzar a travez de los registros y se carga la tabla 
                cargarSucursales.AbrirConexionBD1();
                txtCantidadRegistro.Text = cargarSucursales.RellenarTabla1("SELECT COUNT(idRegistrosCambiosAdministradores) FROM sbepa2.registroscambiosadministradores;").Rows[0][0].ToString();
                txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
                nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
                nudPaginaActualBuscar.Refresh();

                cargarSucursales.AbrirConexionBD1();
                dgbCambiosAdmins.DataSource = cargarSucursales.RellenarTabla1("SELECT * FROM sbepa2.vistaregistrocambioadministradores;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar la tabla de Sucursales ERROR: " + ex.Message, "Error Tabla", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarSucursales.CerrarConexionBD1();
            }
        }

        private void HacerInvisiblesyLimpiarCampos()
        {
            txtBuscarEn.Visible = false;
            nudPaginaActualBuscar.Visible = false;
            txtPaginasDisponiblesBusqueda.Visible = false;
            txtBuscarEn.Text = "";
            nudPaginaActualBuscar.Value = 0;
            txtPaginasDisponiblesBusqueda.Text = "?????????";
            txtPaginasDisponiblesBusqueda.Visible = false;
            lblRegistrosEncontrados.Visible = false;
            txtBuscarEn.Visible = true;
            lnlParametrosABuscar.Visible = true;
            lblPaginaActualBusqueda.Visible = false;
            lblPaginasDisponibles.Visible = false;
            lblRegistrosEncontrados.Visible = false;
            txtRegistrosEncontradosSuperior.Visible = false;
        }

        private void dgbCambiosAdmins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dgbCambiosAdmins.Rows[e.RowIndex];
                txtIDRegistro.Text = Convert.ToString(fila.Cells["idRegistrosCambiosAdministradores"].Value);
                txtIDAdmin.Text = Convert.ToString(fila.Cells["IDadmin"].Value);
                txtLugar.Text = Convert.ToString(fila.Cells["Lugar"].Value);
                txtFechaCambio.Text = Convert.ToString(fila.Cells["FechaCambio"].Value);
                txtAccionEfectuada.Text = Convert.ToString(fila.Cells["AccionEfectuada"].Value);
                txtRegistrodelCambio.Text = Convert.ToString(fila.Cells["RegistroCambio"].Value);

                //Se extrae el rut del usuario
                ComandosBDMySQL BuscarRut = new ComandosBDMySQL();
                try
                {
                    BuscarRut.AbrirConexionBD1();
                    txtRUTAdmin.Text = BuscarRut.RellenarTabla1("SELECT RutAdmin FROM sbepa2.administradores where idAdministradores = " + txtIDAdmin.Text + ";").Rows[0]["RutAdmin"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar extraer el Rut del Administrador de los registros de cambios ERROR: " + ex.Message + "", "Error extraer RUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarRut.CerrarConexionBD1();
                }
            }
        }

        private void btnGuardarRegistro_Click(object sender, EventArgs e)
        {
            if (txtIDRegistro.Text != "")
            {
                //Se abre el SaveFileDialog para fijar la ubicacion de guardado y el nombre del mismo
                SaveFileDialog DialogoGuardar = new SaveFileDialog();
                DialogoGuardar.FileName = "RegistroCambioAdministrador" + txtIDRegistro.Text + ".pdf";

                if (DialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //Se configura el tipo de documento (letter) y donde cerra almacenado
                        Document doc = new Document(PageSize.LETTER);
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"" + DialogoGuardar.FileName + "", FileMode.Create));

                        //Se genera el titulo y la informacion del creador
                        doc.AddTitle("Registro de Cambio Realizado por el Administrador ID:" + txtIDRegistro.Text);
                        doc.AddCreator("Registor Cambio Administrador ©SBEPA");

                        doc.Open();
                        //Se configura las fuentes del doc
                        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        //Se agrega el cuerpo del documento
                        doc.Add(new Paragraph("Registro de Cambio Realizado por un Administrador en SBEPA     ID Cambio:" + txtIDRegistro.Text + "   Fecha Registro " + txtFechaCambio.Text));
                        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Application.StartupPath + @"\Logo.png");
                        imagen.BorderWidth = 0;
                        imagen.Alignment = Element.ALIGN_CENTER;
                        float percentage = 0.0f;
                        percentage = 100 / imagen.Width;
                        imagen.ScalePercent(percentage * 90);
                        doc.Add(imagen);
                        doc.Add(new Paragraph("-------N° de ID del Registro del Cambio-------"));
                        doc.Add(new Paragraph(txtIDRegistro.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------ID del Administrador que realizo el Cambio-------"));
                        doc.Add(new Paragraph(txtIDAdmin.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------RUT del Administrador que realizo el Cambio-------"));
                        doc.Add(new Paragraph(" " + txtRUTAdmin.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Lugar donde Realizo el Cambio-------"));
                        doc.Add(new Paragraph(" " + txtLugar.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Fecha y Hora del Cambio-------"));
                        doc.Add(new Paragraph(" " + txtFechaCambio.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Accion Efectuada por el ADministrador-------"));
                        doc.Add(new Paragraph(" " + txtAccionEfectuada.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Registro Completo del Cambio-------"));
                        doc.Add(new Paragraph("" + txtRegistrodelCambio.Text));

                        //Se cierra el documento y se escribe
                        doc.Close();
                        writer.Close();

                        //Se notifica el guardado
                        MessageBox.Show("Se realizo correctamente la Generacion del Documento", "Documento Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Se abre el documento con el visor de PDF preterminado
                        try
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = @"" + DialogoGuardar.FileName + "";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar abrir el documento Generado ERROR: " + ex.Message, "Error Abrir Documento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar realizar la generacion del Documento ERROR: " + ex.Message, "Error Creacion Docuemento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro para guardarlo en el PC", "Falta seleccionar registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
