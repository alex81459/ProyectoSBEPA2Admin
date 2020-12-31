using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SBEPAEscritorio
{
    class EditorImagenClase
    {
        //se crea el Mapa de Bits de la Imagen (Bitmap) y La superficie de la imagen (Graphics)
        Bitmap imgBitm;
        Graphics g;
        //Se crea el dialogo para buscar el archivo de imagen
        OpenFileDialog DialogoBuscarImagen = new OpenFileDialog();
        //Se crea la linea de recorte de color verde con un ancho de 3 pixeles
        Pen crayon = new Pen(Color.GreenYellow, 3);
        //Se crean las variables de la posX (ubicacion posicion ancho), posY (ubicacion posicion largo), Ancho y Largo de la Imagen
        int posX, posY, ancho, largo;

        //se crean los metodos para obtener y cambiar el mapa de bits, PosX,PosY,Ancho y Largo de la Imagen
        public Bitmap ImgBitmap
        {
            get { return imgBitm; }
            set { imgBitm = value; }
        }

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public int Ancho
        {
            get { return ancho; }
            set { ancho = value; }
        }

        public int Largo
        {
            get { return largo; }
            set { largo = value; }
        }

        //Se Crea el metodo para abrir la imagen 
        public Boolean AbrirImagen(PictureBox picimg) {
            //Que solo se puedan seleccionar imagenes
            DialogoBuscarImagen.Filter = "Archivos de imagen(*.BMP;*.JPG;*.GIF;*PNG;*JPEG;)|*.BMP;*.JPG;*.GIF;*PNG;*JPEG;";
            DialogoBuscarImagen.Title = "Seleccione la Imagen a Cargar";
            if (DialogoBuscarImagen.ShowDialog() == DialogResult.OK)
            {
                picimg.Image = Bitmap.FromFile(DialogoBuscarImagen.FileName);
                picimg.Refresh();
                //Se crea los graficos para el recuadro del recorte
                g = picimg.CreateGraphics();
                //Se crea el cuadro de recorte con las propiedades del crayon y donde se creara,con tamaño 300x300
                g.DrawRectangle(crayon, posX, posY, ancho = 300, largo = 300);
                return true;
            }
            else
            {
                //Se retorna Falso si no se selecciono una imagen a abrir
                return false;
            }
        }

        public void MoverArriba(PictureBox picimg) {
            //Se refresca la imagen, se crea el rectangulo de recorte y se quita -10 a la posicion Y del rectangulo
            //Para que se mueva hacia arriba de la imagen
            picimg.Refresh();
            g = picimg.CreateGraphics();
            g.DrawRectangle(crayon, posX, posY = posY - 10, ancho, largo);
        }

        public void MoverAbajo(PictureBox picimg)
        {
            //Se refresca la imagen, se crea el rectangulo de recorte y se se añaden +10 a la posicion Y del rectangulo
            //Para que se mueva hacia abajo de la imagen
            picimg.Refresh();
            g = picimg.CreateGraphics();
            g.DrawRectangle(crayon, posX, posY = posY + 10, ancho, largo);
        }

        public void MoverIzquierda(PictureBox picimg)
        {
            //Se refresca la imagen, se crea el rectangulo de recorte y se quita -10 de la posicion X del rectangulo
            //Para que se mueva haciala izquierda de la imagen
            picimg.Refresh();
            g = picimg.CreateGraphics();
            g.DrawRectangle(crayon, posX = posX - 10, posY, ancho, largo);
        }

        public void MoverDerecha(PictureBox picimg)
        {
            //Se refresca la imagen, se crea el rectangulo de recorte y se le añade +10 de la posicion X del rectangulo
            //Para que se mueva hacia la Derecha de la imagen
            picimg.Refresh();
            g = picimg.CreateGraphics();
            g.DrawRectangle(crayon, posX = posX + 10, posY, ancho, largo);
        }

        public void MasAncho(PictureBox picimg)
        {
            //Se refresca la imagen,se crea el rectangulo de corte, se aumenta el ancho y largo del recorte
            picimg.Refresh();
            g = picimg.CreateGraphics();
            g.DrawRectangle(crayon, posX, posY, ancho = ancho + 10, largo = largo + 10);
        }

        public void MenosAncho(PictureBox picimg)
        {
            if (ancho >= 210 || largo >= 210)
            {
                //Se refresca la imagen,se crea el rectangulo de corte, se reduce el ancho y largo del recorte
                picimg.Refresh();
                g = picimg.CreateGraphics();
                g.DrawRectangle(crayon, posX, posY, ancho = ancho - 10, largo = largo - 10);
            }
        }

        public Boolean RecortarImagen(PictureBox img, PictureBox img2)
        {
            try
            {
                //Se obtiene las propiedades del rectangulo para el corte, se pasa la imagen a un mapa de bits con sus
                //propiedades, le clona lo que hay dentro del cuadrado de recorte y se devuelve la imagen
                Rectangle rect = new Rectangle(posX, posY, ancho, largo);
                imgBitm = new Bitmap(img.Image, img.Width, img.Height);
                img2.Image = imgBitm.Clone(rect, imgBitm.PixelFormat);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al intentar Generar el Recorte, por favor verifique que el recorte se encuentra dentro de los limites de la imagen", "Error al Recortar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public void GuardarImagen(PictureBox img)
        {
            //Se crea el dialogo para guardar la imagen, se configura para guardar la imagen en formato PNG
            //si se realiza correctamente el dialogo, se guarda la imagen.
            SaveFileDialog DialogoGuardar = new SaveFileDialog();
            Image imgg;
            DialogoGuardar.Filter = "Archivos de Imagen PNG(*.PNG)| *.PNG | All files(*.*) | *.*";
            DialogoGuardar.Title = "Seleccione la Ubicacion de Guardado de la Imagen y el Nombre";
            if (DialogoGuardar.ShowDialog() == DialogResult.OK)
            {
                imgg = img.Image;
                imgg.Save(DialogoGuardar.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public void GirarImagenIzquierda(PictureBox picimg)
        {
            //Se refresca la imagen, se gira 90 grados a la izquierda, y se generan los graficos
            picimg.Refresh();
            picimg.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
            g = picimg.CreateGraphics();
        }

        public void VoltearImagen(PictureBox picimg)
        {
            //Se refresca la imagen, se invierte, y se generan los graficos
            picimg.Refresh();
            picimg.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
            g = picimg.CreateGraphics();
        }

        public void GirarImagenDerecha(PictureBox picimg)
        {
            //Se refresca la imagen, se gira 90 grados a la derecha, y se generan los graficos
            picimg.Refresh();
            picimg.Image.RotateFlip(RotateFlipType.Rotate270FlipX);
            g = picimg.CreateGraphics();
        }
    }
}
