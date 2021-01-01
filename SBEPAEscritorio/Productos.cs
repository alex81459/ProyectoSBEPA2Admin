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
    public partial class Productos : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;
        FuncionesAplicacion VerificarCaracteres = new FuncionesAplicacion();

        public Productos()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbID, "El ID (Identificador) el cual se usara para identificar las diferentes Productos Registrados" + System.Environment.NewLine + "este se genera automático cada vez que se hace un registro de una sucursal" + System.Environment.NewLine + "NO PUEDE SER CAMBIADO POR MANUALMENTE");
            ttmensaje.SetToolTip(pbNombreProducto, "El Nombre con el cual será identificado el producto a registrar " + System.Environment.NewLine + "  cada producto debe tener un nombre único que los diferencie, NO SE PUEDE REPETIR");
            ttmensaje.SetToolTip(pbSucursal, "La Interfaz donde se seleccionara la sucursales a la" + System.Environment.NewLine + " cual le producto pertenecerá");
            ttmensaje.SetToolTip(pbMarca, "La marca que fabrica o produce el producto en cuestión o si es artesanal");
            ttmensaje.SetToolTip(pbEmbase, "El tipo de embace en el cual viene almacenado el producto para su venta");
            ttmensaje.SetToolTip(pbUnidadMedida, "La Unidad de Medida que utiliza el producto para establecer" + System.Environment.NewLine + " la cantidad de contenido que tiene");
            ttmensaje.SetToolTip(pbCantidadMedida, "La cantidad de contenido que tiene el producto según la " + System.Environment.NewLine + "unidad de medida seleccionada anteriormente");
            ttmensaje.SetToolTip(pbDescripcionProducto, "Una Descripción del producto de máximo 300 caracteres");
            ttmensaje.SetToolTip(pbCodigoProducto, "El Código Universal de Producto (UPC) es una simbología de código de barras" + System.Environment.NewLine + " que se utiliza ampliamente para identificar de forma única el producto");
            ttmensaje.SetToolTip(pbFechaRegistro, "La Fecha cuando el producto fue registrado por primera vez en el sistema" + System.Environment.NewLine + " esta se genera de forma automática cuando se registra por primera vez");
            ttmensaje.SetToolTip(pbSeleccioneCategoria, "La Interfaz donde se seleccionara la categoría del producto");
            ttmensaje.SetToolTip(pbProductoUnico, "Si el producto es unico, creado artesanalmente o NO es unico, fabricado y registrado con un codigo UPC");
            ttmensaje.SetToolTip(NUDPrecioProducto, "El precio con el cual se registrara el producto, se registrara cada vez que se cambie");
        }


        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresSoloNumeros(e);
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtDescripcionProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresDescripcion(e);
        }

        private void txtBuscarEn_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = VerificarCaracteres.RestringirCaracteresBuscar(e);
        }
    }
}
