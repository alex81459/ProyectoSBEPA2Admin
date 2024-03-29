﻿using System;
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
    public partial class Categorias : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public Categorias()
        {
            InitializeComponent();
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

        private void Categorias_Load(object sender, EventArgs e)
        {
            cmbcategoria.Text = "NombreCategoria";
            CargarCategorias();
        }

        public void CargarCategorias()
        {
            ComandosBDMySQL cargarCategorias = new ComandosBDMySQL();
            try
            {
                cargarCategorias.AbrirConexionBD1();
                DGVcategorias.DataSource = cargarCategorias.RellenarTabla1("SELECT * FROM sbepa2.vistacategoriastodasinlimite;");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al cargar la tabla de de todas las categorias ERROR: "+ex.Message+"","Error Cargar Tabla",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                cargarCategorias.CerrarConexionBD1();
            }
        }

        private void txtbuscarcategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtbuscarcategoria.Text == "")
            {
                MessageBox.Show("Debe ingresar algun dato a buscar en el campo 'Parametros a Buscar'", "Faltan Datos para la Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ComandosBDMySQL BuscarRegistros = new ComandosBDMySQL();
                try
                {
                    //Se cargan los datos necesarios para la busqueda y el ordenamiento de las paginas
                    BuscarRegistros.AbrirConexionBD1();
                    String CantidadRegistrosDetectados = (BuscarRegistros.RellenarTabla1("SELECT COUNT(`categorias`.`idCategorias`) FROM ((`categorias` JOIN `categoriasimple` ON ((`categorias`.`idCategorias` = `categoriasimple`.`id_categorias`))) JOIN `subcategoria` ON ((`categoriasimple`.`idCategoriaSimple` = `subcategoria`.`idCategoriaSimple`)))").Rows[0][0].ToString());
                    DGVcategorias.DataSource = BuscarRegistros.RellenarTabla2("call sbepa2.BuscarCategoriasTodas('"+ cmbcategoria.Text+ "', '"+ txtbuscarcategoria.Text+ "', 0, 9999999);");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar buscar todas las categorias del sistema ERROR: " + ex.Message + "", "Error Detectado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BuscarRegistros.CerrarConexionBD1();
                }
            }
        }

        private void LimpiarCamposYCargarBusqueda()
        {
            txtbuscarcategoria.Text = "";
            CargarCategorias();
            cmbcategoria.Text = "Categoria";
            txtIDCategoria.Text = "";
            txtNombreCategoria.Text = "";
            txtNombreCategoria.Enabled = false;
            btnBuscarCategoria.Enabled = true;
            cbNuevaCategoria.Checked = false;
            txtIDCategoriaSimple.Text = "";
            txtNombreCategoriaSimple.Text = "";
            txtNombreCategoriaSimple.Enabled = false;
            btnBuscarCategoriaSimple.Enabled = true;
            cbNuevaCategoriaSimple.Checked = false;
            txtIDSubCategoria.Text = "";
            txtNombreSubCategoria.Text = "";
            txtNombreSubCategoria.Enabled =  false;
            cbNuevaSubCategoria.Checked = false;
            txtNombreSubCategoria.Text = "";
            txtNombreSubCategoria.Enabled = true;
        }


        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            LimpiarCamposYCargarBusqueda();
        }

        private void cbNuevaCategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNuevaCategoria.Checked == true)
            {
                btnBuscarCategoria.Enabled = false;
                btnBuscarCategoriaSimple.Enabled = false;
                cbNuevaCategoriaSimple.Checked = true;
                cbNuevaSubCategoria.Checked = true;
                txtNombreCategoria.Text = "";
                txtNombreCategoriaSimple.Text = "";
                txtNombreSubCategoria.Text = "";
                txtNombreCategoria.Enabled = true;
                txtNombreCategoriaSimple.Enabled = true;
                txtNombreSubCategoria.Enabled = true;
                txtIDCategoria.Text = "";
            }
            else
            {
                btnBuscarCategoria.Enabled = true;
                txtNombreCategoria.Text = "";
                cbNuevaCategoriaSimple.Checked = false;
                txtNombreCategoriaSimple.Text = "";
                btnBuscarCategoriaSimple.Enabled = true;
                cbNuevaSubCategoria.Checked = false;
                txtNombreSubCategoria.Text = "";
                txtNombreCategoria.Enabled = false;
                txtNombreCategoriaSimple.Enabled = false;
                txtNombreSubCategoria.Enabled = false;
            }
        }

        private void cbNuevaCategoriaSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNuevaCategoria.Checked == false)
            {
                if (cbNuevaCategoriaSimple.Checked == true)
                {
                    btnBuscarCategoriaSimple.Enabled = false;
                    txtNombreCategoriaSimple.Text = "";
                    cbNuevaSubCategoria.Checked = true;
                    txtNombreSubCategoria.Text = "";
                    txtNombreCategoriaSimple.Enabled = true;
                    txtNombreSubCategoria.Enabled = true;
                    txtIDCategoriaSimple.Text = "";
                }
                else
                {
                    txtNombreCategoriaSimple.Text = "";
                    btnBuscarCategoriaSimple.Enabled = true;
                    cbNuevaCategoriaSimple.Checked = false;
                    txtNombreSubCategoria.Text = "";
                    cbNuevaSubCategoria.Checked = false;
                    txtNombreCategoriaSimple.Enabled = false;
                    txtNombreSubCategoria.Enabled = false;
                }
            }
            else
            {
                cbNuevaCategoriaSimple.Checked = true;
            }
        }

        private void cbNuevaSubCategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNuevaCategoriaSimple.Checked == false)
            {
                if (cbNuevaSubCategoria.Checked == true)
                {
                    cbNuevaSubCategoria.Checked = true;
                    txtNombreSubCategoria.Text = "";
                    txtNombreSubCategoria.Enabled = true;
                    txtIDSubCategoria.Text = "";
                }
                else
                {
                    cbNuevaSubCategoria.Checked = false;
                    txtNombreSubCategoria.Text = "";
                    txtNombreSubCategoria.Enabled = false;
                }
            }
            else
            {
                cbNuevaSubCategoria.Checked = true;
            }
        }

        private Boolean VerificarExistenciaCategoria()
        {
            Boolean ResultadoVerificacion = true;
            ComandosBDMySQL verificarExistencia = new ComandosBDMySQL();
            try
            {
                verificarExistencia.AbrirConexionBD1();
                if (verificarExistencia.VerificarExistenciaDato1("SELECT * FROM sbepa2.categorias where Nombre= '"+ txtNombreCategoria.Text+ "';") == true)
                {
                    ResultadoVerificacion = true;
                    MessageBox.Show("La Categoria ya se encuentra registrada en el Sistema","Categoria Registrada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    ResultadoVerificacion = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar verificar si esta registrada la Categoria en el Sistema ERROR: "+ex.Message+"","Error Verificacion",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                verificarExistencia.CerrarConexionBD1();
            }
            return ResultadoVerificacion;
        }

        private Boolean VerificarExistenciaCategoriaSimple()
        {
            Boolean ResultadoVerificacion = true;
            ComandosBDMySQL verificarExistencia = new ComandosBDMySQL();
            try
            {
                verificarExistencia.AbrirConexionBD1();
                if (verificarExistencia.VerificarExistenciaDato1("SELECT * FROM sbepa2.categoriasimple where nombre = '"+ txtNombreCategoriaSimple.Text+ "';") == true)
                {
                    ResultadoVerificacion = true;
                    MessageBox.Show("La Categoria Simple ya se encuentra registrada en el Sistema", "Categoria Simple Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ResultadoVerificacion = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar verificar si esta registrada la Categoria Simple en el Sistema ERROR: "+ex.Message+"", "Error Verificacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                verificarExistencia.CerrarConexionBD1();
            }
            return ResultadoVerificacion;
        }

        private Boolean VerificarExistenciaSubCategoria()
        {
            Boolean ResultadoVerificacion = true;
            ComandosBDMySQL verificarExistencia = new ComandosBDMySQL();
            try
            {
                verificarExistencia.AbrirConexionBD1();
                if (verificarExistencia.VerificarExistenciaDato1("SELECT * FROM sbepa2.subcategoria where Nombre = '"+ txtNombreSubCategoria.Text+ "';") == true)
                {
                    ResultadoVerificacion = true;
                    MessageBox.Show("La Sub Categoria ya se encuentra registrada en el Sistema", "Sub Categoria Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ResultadoVerificacion = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar verificar si esta registrada la Sub Categoria en el Sistema ERROR: " + ex.Message + "", "Error Verificacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                verificarExistencia.CerrarConexionBD1();
            }
            return ResultadoVerificacion;
        }


        private void btnGuardarCategorias_Click(object sender, EventArgs e)
        {
            //Se envia mensaje para verificar la decision
            DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Guardara la informacion de las categorias del sistema actual y que sus datos son correctos?", "Categorias Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //Se contesta que si
            if (resultadoMensaje == DialogResult.Yes)
            {
                //Se registran todas la nuevas categorias (categoria, categoria simple y sub categoria)
                if (cbNuevaCategoria.Checked == true && cbNuevaCategoriaSimple.Checked == true && cbNuevaSubCategoria.Checked == true && txtIDCategoria.Text == "" && txtIDCategoriaSimple.Text == "" && txtIDSubCategoria.Text == "")
                {
                    //Se verifica si todas las categorias no estan registradas
                    if (VerificarExistenciaCategoria() == false && VerificarExistenciaCategoriaSimple() == false && VerificarExistenciaSubCategoria() == false)
                    {
                        //Se crea la instancia para trabajar con la BD
                        ComandosBDMySQL registrarCategorias = new ComandosBDMySQL();
                        try
                        {
                            registrarCategorias.AbrirConexionBD1();
                            //Se ingresa la consulta para registrar la nueva categoria
                            registrarCategorias.IngresarConsulta1("call sbepa2.InsertarCategoria('"+ txtNombreCategoria.Text+ "');");
                            //Se obtiene el ID de la categoria con la cual se registro
                            String IDCategoriaRegistro = registrarCategorias.RellenarTabla1("SELECT max(idcategorias) as 'UltimoRegistro' FROM sbepa2.categorias;").Rows[0]["UltimoRegistro"].ToString(); ;
                            //Se ingresa la consulta para registrar la nueva categoria simple
                            registrarCategorias.IngresarConsulta1("call sbepa2.InsertarCategoriaSimple('"+ txtNombreCategoriaSimple.Text+ "', "+IDCategoriaRegistro+");");
                            //Se obtiene el ID de la categoria simple que se registro
                            String IDCategoriaSimpleRegistro = registrarCategorias.RellenarTabla1("SELECT max(idCategoriaSimple) as 'UltimoRegistro' FROM sbepa2.categoriasimple;").Rows[0]["UltimoRegistro"].ToString(); ;
                            //Se ingresa la consulta para registrar la nueva sub categoria
                            registrarCategorias.IngresarConsulta1("call sbepa2.InsertarSubCategoria('"+ txtNombreSubCategoria.Text+ "', "+ IDCategoriaSimpleRegistro + ");");
                            //Se registra las categorias que agrego el administrador
                            registrarCategorias.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Categorias', 'Insertar', 'Registro la Categoria con el Nombre: "+ txtNombreCategoria.Text+ " y su Categoria Simple con el Nombre: "+ txtNombreCategoriaSimple.Text+ " y finalmente su Sub Categoria con el Nombre: "+ txtNombreSubCategoria.Text+ "');");
                            MessageBox.Show("Se Registraron correctamentes las categorias dentro del sistema", "Categorias Guardadas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCamposYCargarBusqueda();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar guardar las categoriasen el sistema ERROR: " + ex.Message + "", "Error Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            registrarCategorias.CerrarConexionBD1();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se puede Realizar el Guardado de las Categorias Datos NO Validos", "Datos no Validos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Se registra la categoria existente, la nueva categoria simple y la nueva subcategoria
                if (cbNuevaCategoria.Checked == false && cbNuevaCategoriaSimple.Checked == true && cbNuevaSubCategoria.Checked == true)
                {
                    //Se verifica si se selecciono una categoria
                    if (txtIDCategoria.Text == "")
                    {
                        MessageBox.Show("Debe de seleccionar una Categoria Existente para continuar con el proceso de registrar una nueva Categoria Simple y una Nueva SubCategoria", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Se verifica que la categoria simple ingresada no exista en el sistema
                        if (VerificarExistenciaCategoriaSimple() == true)
                        {
                            MessageBox.Show("La categoria simple nueva que ingreso ya se encuentra registrada en el sistema por lo tanto no se puede almacenar", "Categoria simple ya existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //Se verifica que la sub categoria no este ingresada en el sistema
                            if (VerificarExistenciaSubCategoria() == true)
                            {
                                MessageBox.Show("La Sub Categoria nueva que ingreso ya se encuentra registrada en el sistema por lo tanto no se puede almacenar", "Sub Categoria ya existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //se procede al registro de las categorias y su informacion
                                ComandosBDMySQL registrarCategoria = new ComandosBDMySQL();
                                try
                                {
                                    registrarCategoria.AbrirConexionBD1();
                                    //Se ingresa la consulta para registrar la nueva categoria simple
                                    registrarCategoria.IngresarConsulta1("call sbepa2.InsertarCategoriaSimple('" + txtNombreCategoriaSimple.Text + "', " + txtIDCategoria.Text + ");");
                                    //Se obtiene el ID de la categoria simple que se registro
                                    String IDCategoriaSimpleRegistro = registrarCategoria.RellenarTabla1("SELECT max(idCategoriaSimple) as 'UltimoRegistro' FROM sbepa2.categoriasimple;").Rows[0]["UltimoRegistro"].ToString();
                                    //Se ingresa la consulta para registrar la nueva sub categoria
                                    registrarCategoria.IngresarConsulta1("call sbepa2.InsertarSubCategoria('" + txtNombreSubCategoria.Text + "', " + IDCategoriaSimpleRegistro + ");");
                                    //Se registra las categorias que agrego el administrador
                                    registrarCategoria.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Categorias', 'Insertar', 'Registro  a la Categoria Existe con el Nombre: " + txtNombreCategoria.Text + " y su Categoria Simple con el Nombre: " + txtNombreCategoriaSimple.Text + " y finalmente su Sub Categoria con el Nombre: " + txtNombreSubCategoria.Text + "');");

                                    MessageBox.Show("Se registro correctamente la Categoria Existente: " + txtNombreCategoria.Text + " con las nueva Categoria Simple: " + txtNombreCategoriaSimple.Text + " y la nueva Sub Categoria: " + txtNombreSubCategoria.Text + "", "Comjunto de Categorias Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al intentar registrar el conjuento de nueva categoria ERROR: " + ex.Message + "", "Error al registrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                finally
                                {
                                    registrarCategoria.CerrarConexionBD1();
                                    LimpiarCamposYCargarBusqueda();
                                }
                            }
                        }
                    }
                }

                //Se registra la categoria existente, la categoria simple existente y la nueva categoria
                if (cbNuevaCategoria.Checked == false && cbNuevaCategoriaSimple.Checked == false && cbNuevaSubCategoria.Checked == true)
                {
                    //Se verifica si se selecciono una categoria
                    if (txtNombreCategoria.Text == "")
                    {
                        MessageBox.Show("Debe de seleccionar una Categoria Existente para continuar con el proceso de registrar una Categoria Simple y una Nueva Sub Categoria");
                    }
                    else
                    {
                        //Se verifica se se selecciono una CategoriaSimple
                        if (txtNombreCategoriaSimple.Text == "")
                        {
                            MessageBox.Show("Debe de seleccionar una Categoria Simple Existente para continuar con el proceso de registrar una Nueva Sub Categoria");
                        }
                        else
                        {
                            //Se verifica si esta registrada en el sistema la nueva sub Categoria ingresa
                            if (VerificarExistenciaSubCategoria() == true)
                            {
                                MessageBox.Show("La Sub Categoria nueva que ingreso ya se encuentra registrada en el sistema por lo tanto no se puede almacenar", "Sub Categoria ya existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //Se crea la instancia para trabajar con la BD
                                ComandosBDMySQL registrarConjuntoCategorias = new ComandosBDMySQL();
                                try
                                {
                                    registrarConjuntoCategorias.AbrirConexionBD1();
                                    //Se ingresa la consulta para registrar la nueva sub categoria
                                    registrarConjuntoCategorias.IngresarConsulta1("call sbepa2.InsertarSubCategoria('" + txtNombreSubCategoria.Text + "', " + txtIDCategoriaSimple.Text + ");");
                                    //Se registra las categorias que agrego el administrador
                                    registrarConjuntoCategorias.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Categorias', 'Insertar', 'Registro a la Categoria Existente con el Nombre: " + txtNombreCategoria.Text + " y su Categoria Simple Existente con el Nombre: " + txtNombreCategoriaSimple.Text + " y la Sub Categoria con el Nombre: " + txtNombreSubCategoria.Text + "');");
                                    MessageBox.Show("Se Registraron correctamentes el conjuento de categorias dentro del sistema", "Categorias Guardadas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al intentar guardar las categoriasen el sistema ERROR: " + ex.Message + "", "Error Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                finally
                                {
                                    registrarConjuntoCategorias.CerrarConexionBD1();
                                    LimpiarCamposYCargarBusqueda();
                                }
                            }
                        }
                    }
                }

                //Si todos los IDs estan seleccionados se cambia el nombre de las categorias
                if (txtIDCategoria.Text != "" && txtIDCategoriaSimple.Text != "" && txtIDSubCategoria.Text != "")
                {
                    //Se crea la instanacia para actualizar los nombres de las categorias
                    ComandosBDMySQL cambiarNombres = new ComandosBDMySQL();
                    try
                    {
                        //Debe ingresar la clave maestra la cambiar los nombres de las categorias
                        ClaveMaestra verificarCambiarRegistrosClave = new ClaveMaestra();
                        if (verificarCambiarRegistrosClave.ShowDialog() == DialogResult.OK)
                        {
                            cambiarNombres.AbrirConexionBD1();
                            if (VerificarExistenciaCategoria() == false)
                            {
                                // Se actualiza el nombre de la categoria si esque no existe en el sistema
                                cambiarNombres.IngresarConsulta1("call sbepa2.ActualizarCategoria("+txtIDCategoria.Text+", '"+ txtNombreCategoria.Text+ "');");
                                cambiarNombres.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Categorias', 'Actualizar', 'MODIFICO la Categoria con ID: "+txtIDCategoria.Text+" con el Nombre: "+txtNombreCategoria.Text+"');");
                                MessageBox.Show("El Nombre de la Categoria Seleccionada fue cambiado correctamente");
                            }
                            else
                            {
                                MessageBox.Show("El nuevo nombre para la Categoria ya se encuentra registro, por lo tanto no se ha cambiado, todos los nombres de las categorias deben ser diferentes", "Categoria Existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            if (VerificarExistenciaCategoriaSimple() == false)
                            {
                                //se actualiza el nombre de la categoria simple, si esque no existe en el sistema
                                cambiarNombres.IngresarConsulta1("call sbepa2.ActualizarCategoriaSimple("+txtIDCategoriaSimple.Text+", '"+ txtNombreCategoriaSimple.Text+ "');");
                                cambiarNombres.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Categorias', 'Actualizar', 'MODIFICO la Categoria Simple con ID: " + txtIDCategoriaSimple.Text + " con el Nombre: " + txtNombreCategoriaSimple.Text + "');");
                                MessageBox.Show("El Nombre de la Categoria Simple Seleccionada fue cambiado corractamente");
                            }
                            else
                            {
                                MessageBox.Show("El nuevo nombre para la Categoria Simple ya se encuentra registro, por lo tanto no se ha cambiado, todos los nombres de las categorias Simples deben ser diferentes", "Categoria Simple Existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            if (VerificarExistenciaSubCategoria() == false)
                            {
                                //se actualiza el nombre de la sub categoria, si esque no existen en el sistema
                                cambiarNombres.IngresarConsulta1("call sbepa2.ActualizarSubCategoria("+txtIDSubCategoria.Text+", '"+ txtNombreSubCategoria.Text+ "');");
                                cambiarNombres.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Categorias', 'Actualizar', 'MODIFICO la Categoria Simple con ID: " + txtIDSubCategoria.Text + " con el Nombre: " + txtNombreSubCategoria.Text+ "');");
                                MessageBox.Show("El Nombre de la Sub Categoria fue cambiado correctamente");
                            }
                            else
                            {
                                MessageBox.Show("El nuevo nombre para la Sub Categoria ya se encuentra registro, por lo tanto no se ha cambiado, todos los nombres de las SubCategorias deben ser diferentes", "Sub Categoria Existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar cambiar los nombres de las categorias del sistema ERROR: " + ex.Message + "", "Error Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        LimpiarCamposYCargarBusqueda();
                        cambiarNombres.CerrarConexionBD1();
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimpiarCamposYCargarBusqueda();
        }

        private void txtNombreCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresNombreProducto(e);
        }

        private void txtNombreCategoriaSimple_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresNombreProducto(e);
        }

        private void txtNombreSubCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresNombreProducto(e);
        }

        private void btnEliminarCategorias_Click(object sender, EventArgs e)
        {
            if (txtIDSubCategoria.Text != "")
            {
                //Se envia mensaje para verificar la decision
                DialogResult resultadoMensaje = MessageBox.Show("La Sub Categoria Solamente puede ser borrada si no hay ningun registro ligado a ella, ¿Desea Sub Categoria Seleccionada?", "Borrar Subcategoria", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Se contesta que si
                if (resultadoMensaje == DialogResult.Yes)
                {
                    //Se verifica que la categoria no este unida a un producto
                    ComandosBDMySQL verificarCategoriaProducto = new ComandosBDMySQL();
                    try
                    {
                        //se veririca si la subcategoira no esta siendo utilizada en ningun producto
                        verificarCategoriaProducto.AbrirConexionBD1();
                        Boolean ResultadoComprobacion = verificarCategoriaProducto.VerificarExistenciaDato1("SELECT * FROM sbepa2.subcategoria inner join productos on subcategoria.idSubCategoria = productos.Id_subcategoria where subcategoria.idSubCategoria = '"+txtIDSubCategoria.Text+"';");

                        if (ResultadoComprobacion == true)
                        {
                            MessageBox.Show("Existe un Producto ligado a esta categoria, no se puede borrar una categoria que ya tenga un producto", "Categoria Usada no se puede borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //Se necesita de la clave maestra para eliminar la categoria
                            ClaveMaestra verificarEliminarClave = new ClaveMaestra();
                            if (verificarEliminarClave.ShowDialog() == DialogResult.OK)
                            {
                                //Se elimina el producto
                                verificarCategoriaProducto.IngresarConsulta1("call sbepa2.EliminarSubCategoria("+txtIDSubCategoria.Text+");");
                                verificarCategoriaProducto.IngresarConsulta1("call sbepa2.InsertarRegistrosCambiosAdministradores(" + FuncionesAplicacion.IDadministrador + ", 'Categorias', 'Eliminar', 'Eliminio la Sub Categoria con el ID: "+txtIDSubCategoria.Text+" la cual tenia por Nombre: "+txtNombreSubCategoria.Text+"');");
                                MessageBox.Show("La SubCategoria ha sido correctamente eliminada del sistema", "Eliminacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Debe Ingresar La Clave Maestra para continuar con el Proceso", "Clave no ingresada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar borrar la Sub Categoria del sistema ERROR: " + ex.Message + "", "Error Borrar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        verificarCategoriaProducto.CerrarConexionBD1();
                        LimpiarCamposYCargarBusqueda();
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una Categoria a Eliminar", "No ha seleccionada algo que borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }  
        }


        private void DGVcategorias_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = DGVcategorias.Rows[e.RowIndex];
                txtIDCategoria.Text = Convert.ToString(fila.Cells["idCategorias"].Value);
                txtNombreCategoria.Text = Convert.ToString(fila.Cells["NombreCategoria"].Value);
                txtIDCategoriaSimple.Text = Convert.ToString(fila.Cells["idCategoriaSimple"].Value);
                txtNombreCategoriaSimple.Text = Convert.ToString(fila.Cells["NombreCategoriaSimple"].Value);
                txtIDSubCategoria.Text = Convert.ToString(fila.Cells["idSubCategoria"].Value);
                txtNombreSubCategoria.Text = Convert.ToString(fila.Cells["NombreSubCategoria"].Value);

                cbNuevaCategoria.Enabled = false;
                btnBuscarCategoria.Enabled = false;
                btnBuscarCategoriaSimple.Enabled = false;
                cbNuevaCategoriaSimple.Enabled = false;
                cbNuevaSubCategoria.Enabled = false;

                txtNombreCategoria.Enabled = true;
                txtNombreCategoriaSimple.Enabled = true;
                txtNombreSubCategoria.Enabled = true;
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            //se abre el form categorias para seleccionar alguna
            CategoriasBuscarCategoria abrirCategorias = new CategoriasBuscarCategoria();
            abrirCategorias.ShowDialog();
        }

        private void btnBuscarCategoriaSimple_Click(object sender, EventArgs e)
        {
            CategoriasBuscarCategoriaSimple abrirCategoriasSimples = new CategoriasBuscarCategoriaSimple();
            abrirCategoriasSimples.ShowDialog();
        }
    }
}
