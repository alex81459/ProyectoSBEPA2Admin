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
    public partial class Menu : Form
    {
        private Point posicion = Point.Empty;
        private bool mover = false;

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            lblUsuarioActual.Text = FuncionesAplicacion.NombreUsuario;
            lblRut.Text = FuncionesAplicacion.RutAdmin;
            lblTiempoInicioSesion.Text = FuncionesAplicacion.FechaInicioSesion;
            lblID.Text = FuncionesAplicacion.IDadministrador;
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login RegresarLogin = new Login();
            RegresarLogin.Show();
        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            Productos abrirProductos = new Productos();
            abrirProductos.ShowDialog();
        }

        private void btnsupermercados_Click(object sender, EventArgs e)
        {
            Sucursales abrirSucursales = new Sucursales();
            abrirSucursales.ShowDialog();
        }

        private void btnsistemadeusuario_Click(object sender, EventArgs e)
        {
            Usuarios abrir_usuario = new Usuarios();
            abrir_usuario.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Tienda abrirTienda = new Tienda();
            abrirTienda.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Categorias abrirCategorias = new Categorias();
            abrirCategorias.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RegistrosLoginAdmin abrirregistros = new RegistrosLoginAdmin();
            abrirregistros.ShowDialog();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            Administradores abrirAdmin = new Administradores();
            abrirAdmin.ShowDialog();
        }


        private void button14_Click(object sender, EventArgs e)
        {
            CopiaSeguridad AbrirCopia = new CopiaSeguridad();
            AbrirCopia.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //Cuando se cierra el form se preguntara si esta seguro de cerrar sesion
            DialogResult CerrarAPP = MessageBox.Show("¿Esta Seguro que cerrara el programa incluyendo su Sesion?  ", "Cerrar Sesion", MessageBoxButtons.YesNo);

            if (CerrarAPP == DialogResult.Yes)
            {
                //Se borran los registros de inicio de sesion, se muestra mensaje y se devuelve al form Login
                FuncionesAplicacion.IDadministrador = "";
                FuncionesAplicacion.NombreUsuario = "";
                FuncionesAplicacion.RutAdmin = "";
                FuncionesAplicacion.FechaInicioSesion = "";
                FuncionesAplicacion.IP = "";


                MessageBox.Show("Su Sesion se ha cerrado", "Sesion Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();
            }
            if (CerrarAPP == DialogResult.No)
            {
                MessageBox.Show("Muy Bien Falsa Alarma", "Sesion No Cerrada", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void pictureBox22_Click(object sender, EventArgs e)
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

        private void button13_Click(object sender, EventArgs e)
        {
            RegistrosCambiosAdmin abrirRegistros = new RegistrosCambiosAdmin();
            abrirRegistros.ShowDialog();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            BaneoUsuarios abrirBaneo = new BaneoUsuarios();
            abrirBaneo.ShowDialog();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Tienda abrirTienda = new Tienda();
            abrirTienda.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Sucursales abrirSucursales = new Sucursales();
            abrirSucursales.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Categorias abrirCategorias = new Categorias();
            abrirCategorias.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Productos abrirProductos = new Productos();
            abrirProductos.ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            ActualizarPrecioProducto abrirPrecioProducto = new ActualizarPrecioProducto();
            abrirPrecioProducto.ShowDialog();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Administradores abrirAdmin = new Administradores();
            abrirAdmin.ShowDialog();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            RegistrosCambiosAdmin abrirRegistros = new RegistrosCambiosAdmin();
            abrirRegistros.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RegistrosCambiosUsuario abrirCambios = new RegistrosCambiosUsuario();
            abrirCambios.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RegistrosLoginUsuarios abrirRegistrosUsuario = new RegistrosLoginUsuarios();
            abrirRegistrosUsuario.ShowDialog();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            RegistrosCambiosUsuario abrirRegistrosUsuario = new RegistrosCambiosUsuario();
            abrirRegistrosUsuario.ShowDialog();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            BaneoUsuarios abrirBaneo = new BaneoUsuarios();
            abrirBaneo.ShowDialog();
        }

        private void btngestionarproductosdeusuario_Click(object sender, EventArgs e)
        {
            CambioInfoProductos abrirCambio = new CambioInfoProductos();
            abrirCambio.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RegionesyComunas abrirGestor = new RegionesyComunas();
            abrirGestor.ShowDialog();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            Estadisticas abrirEstadisticas = new Estadisticas();
            abrirEstadisticas.ShowDialog();
        }

        private void btnRegistrosPrecios_Click(object sender, EventArgs e)
        {
            ActualizarPrecioProducto abrirPreciosProductos = new ActualizarPrecioProducto();
            abrirPreciosProductos.ShowDialog();
        }
    }
}
