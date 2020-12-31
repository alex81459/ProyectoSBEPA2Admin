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
    public partial class SucursalesSubirImagenes : Form
    {
        public String IDSucursal;

        public SucursalesSubirImagenes()
        {
            InitializeComponent();
        }

        private void SucursalesSubirImagenes_Load(object sender, EventArgs e)
        {
            txtIDSucursal.Text = IDSucursal;
            CargarImagenesSucursales();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Se minimiza el form
            this.WindowState = FormWindowState.Minimized;
        }

        public void CargarImagenesSucursales()
        {
            ComandosBDMySQL extraerImagenes = new ComandosBDMySQL();
            try
            {
                extraerImagenes.AbrirConexionBD1();
                //Se crea un data table para verificar si las imagenes han sido subidas
                DataTable VerificarExistenciaImagenes = new DataTable();
                VerificarExistenciaImagenes = extraerImagenes.RellenarTabla1("SELECT Foto FROM sbepa2.sucursalesfotos where idSucursal = '" + txtIDSucursal.Text + "';");

                //Se verifica su la imagen 1 fue subida
                if (VerificarExistenciaImagenes.Rows.Count >= 1)
                {
                    //Si fue subida se extraer la imagen y su ID
                    extraerImagenes.AbrirConexionBD1();
                    pbimg1.Image = extraerImagenes.ExtraerImagen("SELECT foto FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 0,1;");
                    txtIDImagen1.Text = extraerImagenes.RellenarTabla1("SELECT idSucursalesFotos FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 0,1;").Rows[0]["idSucursalesFotos"].ToString();
                    //se activan los botones para remplazarla
                    btnSubirImagen1.Text = "Remplazar Imagen 1";
                    btnSubirImagen1.Enabled = true;
                }
                else
                {
                    btnSubirImagen1.Text = "Subir Imagen 1";
                    btnSubirImagen1.Enabled = true;
                }

                //Se verifica su la imagen 2 fue subida
                if (VerificarExistenciaImagenes.Rows.Count >= 2)
                {
                    //Si fue subida se extraer la imagen y su ID
                    extraerImagenes.AbrirConexionBD1();
                    pbimg2.Image = extraerImagenes.ExtraerImagen("SELECT foto FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 1,1;");
                    txtIDImagen2.Text = extraerImagenes.RellenarTabla1("SELECT idSucursalesFotos FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 1,1;").Rows[0]["idSucursalesFotos"].ToString();
                    //se activan los botones para remplazarla
                    btnSubirImagen2.Text = "Remplazar Imagen 2";
                    btnSubirImagen2.Enabled = true;
                }
                else
                {
                    btnSubirImagen2.Text = "Subir Imagen 2";
                    btnSubirImagen2.Enabled = true;
                }

                //Se verifica su la imagen 3 fue subida
                if (VerificarExistenciaImagenes.Rows.Count >= 3)
                {
                    //Si fue subida se extraer la imagen y su ID
                    extraerImagenes.AbrirConexionBD1();
                    pbimg3.Image = extraerImagenes.ExtraerImagen("SELECT foto FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 2,1;");
                    txtIDImagen3.Text = extraerImagenes.RellenarTabla1("SELECT idSucursalesFotos FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 2,1;").Rows[0]["idSucursalesFotos"].ToString();
                    //se activan los botones para remplazarla
                    btnSubirImagen3.Text = "Remplazar Imagen 3";
                    btnSubirImagen3.Enabled = true;
                }
                else
                {
                    btnSubirImagen3.Text = "Subir Imagen 3";
                    btnSubirImagen3.Enabled = true;
                }

                //Se verifica su la imagen 4 fue subida
                if (VerificarExistenciaImagenes.Rows.Count >= 4)
                {
                    //Si fue subida se extraer la imagen y su ID
                    extraerImagenes.AbrirConexionBD1();
                    pbimg4.Image = extraerImagenes.ExtraerImagen("SELECT foto FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 3,1;");
                    txtIDImagen4.Text = extraerImagenes.RellenarTabla1("SELECT idSucursalesFotos FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 3,1;").Rows[0]["idSucursalesFotos"].ToString();
                    //se activan los botones para remplazarla
                    btnSubirImagen4.Text = "Remplazar Imagen 4";
                    btnSubirImagen4.Enabled = true;
                }
                else
                {
                    btnSubirImagen4.Text = "Subir Imagen 4";
                    btnSubirImagen4.Enabled = true;
                }

                //Se verifica su la imagen 4 fue subida
                if (VerificarExistenciaImagenes.Rows.Count >= 5)
                {
                    //Si fue subida se extraer la imagen y su ID
                    extraerImagenes.AbrirConexionBD1();
                    pbimg5.Image = extraerImagenes.ExtraerImagen("SELECT foto FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 4,1;");
                    txtIDImagen5.Text = extraerImagenes.RellenarTabla1("SELECT idSucursalesFotos FROM sbepa2.sucursalesfotos where idSucursal = " + txtIDSucursal.Text + " limit 4,1;").Rows[0]["idSucursalesFotos"].ToString();
                    //se activan los botones para remplazarla
                    btnSubirImagen5.Text = "Remplazar Imagen 5";
                    btnSubirImagen5.Enabled = true;
                }
                else
                {
                    btnSubirImagen5.Text = "Subir Imagen 5";
                    btnSubirImagen5.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "No hay ninguna fila en la posición 0.")
                {
                    MessageBox.Show("Faltan Imagenes por subir de la sucursal", "Faltan Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Se ha producido un error al intentar cargar el sistema para subir las imagenes de la sucursal ERROR: " + ex.Message + "", "Error carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSubirImagen1_Click(object sender, EventArgs e)
        {
            //Se sube o se remplaza la Imagen 1 de la sucursal segun corresponda
            if (btnSubirImagen1.Text == "Subir Imagen 1")
            {
                //Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg1.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    ComandosBDMySQL GuardarImagenes = new ComandosBDMySQL();
                    try
                    {
                        GuardarImagenes.AbrirConexionBD1();
                        GuardarImagenes.IngresarImagen("call sbepa2.InsertarSucursalFoto(" + IDSucursal + ", @imagen);", pbimg1.Image);
                        GuardarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Insertar', 'Registro la Imagen 1 de la sucursal con el ID de imagen:" + txtIDImagen1.Text + "');");
                        MessageBox.Show("La Imagen 1 de la sucursal ha sido correctamente registrada", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada","Imagen demasiado grande",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        GuardarImagenes.CerrarConexionBD1();
                    }
                }
            }
            else if (btnSubirImagen1.Text == "Remplazar Imagen 1")
            {
                // Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg1.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    // si el cuadro dice Remplazar Imagen se actualiza la imagen
                    ComandosBDMySQL RemplazarImagenes = new ComandosBDMySQL();
                    try
                    {
                        RemplazarImagenes.AbrirConexionBD1();
                        RemplazarImagenes.IngresarImagen("call sbepa2.ActualizarSucursalFoto(" + txtIDImagen1.Text + "," + txtIDSucursal.Text + ", @imagen);", pbimg1.Image);
                        RemplazarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Actualizar', 'Actualizo la Imagen 1 de la sucursal con el ID de imagen:" + txtIDImagen1.Text + "');");
                        MessageBox.Show("La Imagen 1 de la sucursal ha sido correctamente remplazada", "Remplazo Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        RemplazarImagenes.CerrarConexionBD1();
                    }
                }
            }
        }

        private void btnSubirImagen2_Click(object sender, EventArgs e)
        {
            //Se sube o se remplaza la Imagen 2 de la sucursal segun corresponda
            if (btnSubirImagen2.Text == "Subir Imagen 2")
            {
                //Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg2.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    ComandosBDMySQL GuardarImagenes = new ComandosBDMySQL();
                    try
                    {
                        GuardarImagenes.AbrirConexionBD1();
                        GuardarImagenes.IngresarImagen("call sbepa2.InsertarSucursalFoto(" + IDSucursal + ", @imagen);", pbimg2.Image);
                        GuardarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Insertar', 'Registro la Imagen 2 de la sucursal con el ID de imagen:" + txtIDImagen2.Text + "');");
                        MessageBox.Show("La Imagen 2 de la sucursal ha sido correctamente registrada", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        GuardarImagenes.CerrarConexionBD1();
                    }
                }
            }
            else if (btnSubirImagen2.Text == "Remplazar Imagen 2")
            {
                // Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg2.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    // si el cuadro dice Remplazar Imagen se actualiza la imagen
                    ComandosBDMySQL RemplazarImagenes = new ComandosBDMySQL();
                    try
                    {
                        RemplazarImagenes.AbrirConexionBD1();
                        RemplazarImagenes.IngresarImagen("call sbepa2.ActualizarSucursalFoto(" + txtIDImagen2.Text + "," + txtIDSucursal.Text + ", @imagen);", pbimg2.Image);
                        RemplazarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Actualizar', 'Actualizo la Imagen 2 de la sucursal con el ID de imagen:" + txtIDImagen2.Text + "');");
                        MessageBox.Show("La Imagen 2 de la sucursal ha sido correctamente remplazada", "Remplazo Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        RemplazarImagenes.CerrarConexionBD1();
                    }
                }
            }
        }

        private void btnSubirImagen3_Click(object sender, EventArgs e)
        {
            //Se sube o se remplaza la Imagen 2 de la sucursal segun corresponda
            if (btnSubirImagen3.Text == "Subir Imagen 3")
            {
                //Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg3.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    ComandosBDMySQL GuardarImagenes = new ComandosBDMySQL();
                    try
                    {
                        GuardarImagenes.AbrirConexionBD1();
                        GuardarImagenes.IngresarImagen("call sbepa2.InsertarSucursalFoto(" + IDSucursal + ", @imagen);", pbimg3.Image);
                        GuardarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Insertar', 'Registro la Imagen 3 de la sucursal con el ID de imagen:" + txtIDImagen3.Text + "');");
                        MessageBox.Show("La Imagen 3 de la sucursal ha sido correctamente registrada", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        GuardarImagenes.CerrarConexionBD1();
                    }
                }
            }
            else if (btnSubirImagen3.Text == "Remplazar Imagen 3")
            {
                // Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg3.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    // si el cuadro dice Remplazar Imagen se actualiza la imagen
                    ComandosBDMySQL RemplazarImagenes = new ComandosBDMySQL();
                    try
                    {
                        RemplazarImagenes.AbrirConexionBD1();
                        RemplazarImagenes.IngresarImagen("call sbepa2.ActualizarSucursalFoto(" + txtIDImagen3.Text + "," + txtIDSucursal.Text + ", @imagen);", pbimg3.Image);
                        RemplazarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Actualizar', 'Actualizo la Imagen 3 de la sucursal con el ID de imagen:" + txtIDImagen3.Text + "');");
                        MessageBox.Show("La Imagen 3 de la sucursal ha sido correctamente remplazada", "Remplazo Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        RemplazarImagenes.CerrarConexionBD1();
                    }
                }
            }
        }

        private void btnSubirImagen4_Click(object sender, EventArgs e)
        {
            //Se sube o se remplaza la Imagen 2 de la sucursal segun corresponda
            if (btnSubirImagen4.Text == "Subir Imagen 4")
            {
                //Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg4.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    ComandosBDMySQL GuardarImagenes = new ComandosBDMySQL();
                    try
                    {
                        GuardarImagenes.AbrirConexionBD1();
                        GuardarImagenes.IngresarImagen("call sbepa2.InsertarSucursalFoto(" + IDSucursal + ", @imagen);", pbimg4.Image);
                        GuardarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Insertar', 'Registro la Imagen 4 de la sucursal con el ID de imagen:" + txtIDImagen4.Text + "');");
                        MessageBox.Show("La Imagen 4 de la sucursal ha sido correctamente registrada", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        GuardarImagenes.CerrarConexionBD1();
                    }
                }
            }
            else if (btnSubirImagen4.Text == "Remplazar Imagen 4")
            {
                // Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg4.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    // si el cuadro dice Remplazar Imagen se actualiza la imagen
                    ComandosBDMySQL RemplazarImagenes = new ComandosBDMySQL();
                    try
                    {
                        RemplazarImagenes.AbrirConexionBD1();
                        RemplazarImagenes.IngresarImagen("call sbepa2.ActualizarSucursalFoto(" + txtIDImagen4.Text + "," + txtIDSucursal.Text + ", @imagen);", pbimg4.Image);
                        RemplazarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Actualizar', 'Actualizo la Imagen 4 de la sucursal con el ID de imagen:" + txtIDImagen4.Text + "');");
                        MessageBox.Show("La Imagen 4 de la sucursal ha sido correctamente remplazada", "Remplazo Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        RemplazarImagenes.CerrarConexionBD1();
                    }
                }
            }
        }

        private void btnSubirImagen5_Click(object sender, EventArgs e)
        {
            //Se sube o se remplaza la Imagen 2 de la sucursal segun corresponda
            if (btnSubirImagen5.Text == "Subir Imagen 5")
            {
                //Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg5.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    ComandosBDMySQL GuardarImagenes = new ComandosBDMySQL();
                    try
                    {
                        GuardarImagenes.AbrirConexionBD1();
                        GuardarImagenes.IngresarImagen("call sbepa2.InsertarSucursalFoto(" + IDSucursal + ", @imagen);", pbimg5.Image);
                        GuardarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Insertar', 'Registro la Imagen 5 de la sucursal con el ID de imagen:" + txtIDImagen5.Text + "');");
                        MessageBox.Show("La Imagen 5 de la sucursal ha sido correctamente registrada", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        GuardarImagenes.CerrarConexionBD1();
                    }
                }
            }
            else if (btnSubirImagen5.Text == "Remplazar Imagen 5")
            {
                // Se crea la instancia y se abre el editor de imagenes
                EditorImagen abrirEditorImagen = new EditorImagen();
                if (abrirEditorImagen.ShowDialog() == DialogResult.OK)
                {
                    //Si al cerrar el Form devuelve ok, se extrae la imagen recortada del Form;
                    pbimg5.Image = abrirEditorImagen.ImagenRecortadaAenviar.Image;
                    //si el cuadro permite subir imagen se guarda por primera vez la imagen
                    // si el cuadro dice Remplazar Imagen se actualiza la imagen
                    ComandosBDMySQL RemplazarImagenes = new ComandosBDMySQL();
                    try
                    {
                        RemplazarImagenes.AbrirConexionBD1();
                        RemplazarImagenes.IngresarImagen("call sbepa2.ActualizarSucursalFoto(" + txtIDImagen5.Text + "," + txtIDSucursal.Text + ", @imagen);", pbimg5.Image);
                        RemplazarImagenes.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Sucursales Fotos', 'Actualizar', 'Actualizo la Imagen 5 de la sucursal con el ID de imagen:" + txtIDImagen5.Text + "');");
                        MessageBox.Show("La Imagen 5 de la sucursal ha sido correctamente remplazada", "Remplazo Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarImagenesSucursales();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "packets larger than max_allowed_packet are not allowed")
                        {
                            MessageBox.Show("El tamaño de la imagen supera el maximo termitido de 16 MB, redusca el tamaño de la imagen o comprimala para que sea menos pesada, la imagen no puede ser guardada", "Imagen demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Error al intentar registrar la nueva imagen en el sistema ERROR: " + ex.Message + "", "Error Registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    finally
                    {
                        RemplazarImagenes.CerrarConexionBD1();
                    }
                }
            }
        }
    }
}
