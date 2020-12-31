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
    public partial class EditorImagen : Form
    {
        //Se crean las variables de la posicion del Form y si esta activado mover
        private Point posicion = Point.Empty;
        private bool mover = false;

        //Se crea una instancia para la Clase EditorImagenClase
        EditorImagenClase objRecorte = new EditorImagenClase();

        //Se crea un objeto PictureBox publico el cual permitira enviar la imagen al formulario que llamo al editor de imagenes
        public PictureBox ImagenRecortadaAenviar { get; set; }

        public EditorImagen()
        {
            InitializeComponent();
            //se establecen los mensajes de informacion del Formulario
            ttmensaje.SetToolTip(pbGuardarImagen, "Se le solicitara donde guardara la imagen recortada y con qué nombre dentro del computador" + System.Environment.NewLine + " automáticamente el formato de la Imagen Sera PNG debido a su capacidad" + System.Environment.NewLine + "de guardar imágenes con transparencias y su sistema de compresión ligero.");
            ttmensaje.SetToolTip(pbMoverRecorte, "Puede utilizar las teclas de navegacion W (Arriba) S (Abajo) A (Izquierda) D (Derecha) las" + System.Environment.NewLine+ "permitiran mover el cuadro de recorte de la imagen, Las teclas Q (Girar Izquierda)" + System.Environment.NewLine + "y E (Girar Derecha) le permitiran voltear la imagen,la tecla X" + System.Environment.NewLine + "Le permitiran invertir la imagen, y finalmente la tecla M para aumentar el tamaño de recorte y N para reducirlo");
            ttmensaje.SetToolTip(pbRecortarYusar, "Se Recortara la Imagen y Se le enviara devuelta al Formulario con el que estaba trabajando");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Se envia el picture box que contendra la imagen original, el cual regresa un booleano si se cargo correctamente
            //La imagen, si se cargo correctamente se activan los controles de recorte
            gbEdicion.Enabled = objRecorte.AbrirImagen(pbImagenOriginal);
            //Se obtiene las propiedades de la imagen
            txtPosicionX.Text = objRecorte.PosX.ToString();
            txtPosicionY.Text = objRecorte.PosY.ToString();
            txtLargo.Text = objRecorte.Largo.ToString();
            txtAncho.Text = objRecorte.Ancho.ToString();
            //Se mueve el recorte un poco para que cargue el marco
            objRecorte.MoverAbajo(pbImagenOriginal);
            txtPosicionY.Text = objRecorte.PosY.ToString();
        }

        private void pbArriba_Click(object sender, EventArgs e)
        {
            //Se llama al metodo para mover el recuadro de recorte
            //Se le envia la imagen original y se obtiene la propiedad que fue afectado por moverlo
            objRecorte.MoverArriba(pbImagenOriginal);
            txtPosicionY.Text = objRecorte.PosY.ToString();
        }

        private void pbAbajo_Click(object sender, EventArgs e)
        {
            //Se llama al metodo para mover el recuadro de recorte
            objRecorte.MoverAbajo(pbImagenOriginal);
            txtPosicionY.Text = objRecorte.PosY.ToString();
        }

        private void pbIzquierda_Click(object sender, EventArgs e)
        {
            //Se llama al metodo para mover el recuadro de recorte
            objRecorte.MoverIzquierda(pbImagenOriginal);
            txtPosicionX.Text = objRecorte.PosX.ToString();
        }

        private void pbDerecha_Click(object sender, EventArgs e)
        {
            //Se llama al metodo para mover el recuadro de recorte
            objRecorte.MoverDerecha(pbImagenOriginal);
            txtPosicionX.Text = objRecorte.PosX.ToString();
        }

        private void picMas_Click(object sender, EventArgs e)
        {
            //Se llama al metodo que aumenta el tamaño del recuadro de recorte
            //y se obtiene las propiedades del recuadro
            objRecorte.MasAncho(pbImagenOriginal);
            txtAncho.Text = objRecorte.Ancho.ToString();
            txtLargo.Text = objRecorte.Largo.ToString();
        }

        private void picMenos_Click(object sender, EventArgs e)
        {
            //Se llama al metodo que aumenta el tamaño del recuadro de recorte
            //y se obtiene las propiedades del recuadro
            objRecorte.MenosAncho(pbImagenOriginal);
            txtAncho.Text = objRecorte.Ancho.ToString();
            txtLargo.Text = objRecorte.Largo.ToString();
        }

        private void btnRecortarImagen_Click(object sender, EventArgs e)
        {
            //Se llama a la funcion recortar imagen, la cual retorna si se recorto correctamente la imagen
            //y se activa el control para guardar la imagen
            Boolean ImagenRecortada = objRecorte.RecortarImagen(pbImagenOriginal, pbImagenRecortada);
            //Si la imagen se recorta correctamente se activa el grupo para guardar las imagenes
            gbImagenRecortada.Enabled = ImagenRecortada;

            if (ImagenRecortada)
            {
                //si la imagen se recorta correctamente se baja al final del panel
                panel2.VerticalScroll.Value = 500;
            }
        }

        private void btnGuardarRecorte_Click(object sender, EventArgs e)
        {
            //Llama al metodo para guarar la imagen en el PC en formato PNG
            objRecorte.GuardarImagen(pbImagenRecortada);
        }

        private void picGirarIzquieda_Click(object sender, EventArgs e)
        {
            //se llama a la funcione Girar Imagen
            objRecorte.GirarImagenIzquierda(pbImagenOriginal);
            objRecorte.MoverAbajo(pbImagenOriginal);
        }

        private void picGirarDerecha_Click(object sender, EventArgs e)
        {
            //se llama a la funcione Girar Imagen
            objRecorte.GirarImagenDerecha(pbImagenOriginal);
            objRecorte.MoverAbajo(pbImagenOriginal);
        }

        private void btnRecortaryUsar_Click(object sender, EventArgs e)
        {
            //Se extablece quela ImagenRecortadaAenviar (la que capturara el Form que instancio al Editor de Imagen)
            //enviara la imagen ya recortada
            ImagenRecortadaAenviar = pbImagenRecortada;
            //Se devuelve El Resultado del Dialogo como OK y se cierra el Form
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void picVoltearImagen_Click(object sender, EventArgs e)
        {
            objRecorte.VoltearImagen(pbImagenOriginal);
            objRecorte.MoverAbajo(pbImagenOriginal);
        }

        private void btnSeleccionarImagen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "W")
            {
                //Se llama a la funcion para subir el recorte
                objRecorte.MoverArriba(pbImagenOriginal);
                txtPosicionY.Text = objRecorte.PosY.ToString();
            }
            if (e.KeyCode.ToString() == "S")
            {
                //Se llama a la funcion para bajar el recorte
                objRecorte.MoverAbajo(pbImagenOriginal);
                txtPosicionY.Text = objRecorte.PosY.ToString();
            }
            if (e.KeyCode.ToString() == "A")
            {
                //Se llama a la funcion para mover a la izquierda el recorte
                objRecorte.MoverIzquierda(pbImagenOriginal);
                txtPosicionX.Text = objRecorte.PosX.ToString();
            }
            if (e.KeyCode.ToString() == "D")
            {
                //Se llama a la funcion para mover a la derecha el recorte
                objRecorte.MoverDerecha(pbImagenOriginal);
                txtPosicionX.Text = objRecorte.PosX.ToString();
            }
            if (e.KeyCode.ToString() == "Q")
            {
                //Se llama a la funcion para girar a la izquieda la imagen
                objRecorte.GirarImagenIzquierda(pbImagenOriginal);
                objRecorte.MoverAbajo(pbImagenOriginal);
            }
            if (e.KeyCode.ToString() == "E")
            {
                //Se llama a la funcion para girar la derecha la imagen
                objRecorte.GirarImagenDerecha(pbImagenOriginal);
                objRecorte.MoverAbajo(pbImagenOriginal);
            }
            if (e.KeyCode.ToString() == "X")
            {
                //Se llama a la funcion para invertir la imagen
                objRecorte.VoltearImagen(pbImagenOriginal);
                objRecorte.MoverAbajo(pbImagenOriginal);
            }
            if (e.KeyCode.ToString() == "M")
            {
                //Se llama a la funcion para aumentar el tamaño del recuadro de corte
                objRecorte.MasAncho(pbImagenOriginal);
                txtAncho.Text = objRecorte.Ancho.ToString();
                txtLargo.Text = objRecorte.Largo.ToString();
            }
            if (e.KeyCode.ToString() == "N")
            {
                //Se llama a la funcion para reducir el tamaño del recuadro de corte
                objRecorte.MenosAncho(pbImagenOriginal);
                txtAncho.Text = objRecorte.Ancho.ToString();
                txtLargo.Text = objRecorte.Largo.ToString();
            }

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
    }
}
