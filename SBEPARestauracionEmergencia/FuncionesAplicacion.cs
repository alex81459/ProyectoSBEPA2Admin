using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SBEPARestauracionEmergencia
{
    class FuncionesAplicacion
    {

        public static string IDadministrador { get; set; } = string.Empty;
        public static string AdministradorSesion { get; set; } = string.Empty;
        public static string AdministradorRUT { get; set; } = string.Empty;
        public static string InicioSesionTiempo { get; set; } = string.Empty;

        public Boolean VerificarContraceñaSegura(String CLaveVerificar)
        {
            //Se verifica que las claves tengan minimo 8 caracteres
            if (CLaveVerificar.Length >= 10)
            {
                //Se verifica que la clave tenga almenos una MAYUSCULA, minuscula y un Numero
                //Si Tiene MAYUSCULAS && Minusculas && numeros
                if (CLaveVerificar.Any(c => char.IsUpper(c)) && CLaveVerificar.Any(c => char.IsLower(c)))
                {
                    if (CLaveVerificar.Any(c => char.IsNumber(c)))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("La Clave debe contener almenos un numero por seguridad", "Clave Insegura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
                else
                //Si la clave no tiene MAYUSCULA,minuscula o/y un numero
                {
                    MessageBox.Show("La Clave debe contener almenos una Letra MAYUSCULA y una letra minuscula por seguridad", "Clave Insegura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            //Si la clave tiene 7 o menos caracteres
            else
            {
                MessageBox.Show("La Clave debe estar compuesta por minimo 10 caracteres por seguridad", "Clave Insegura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public String ArchivoSHA256(FileStream stream)
        {
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }


        public String TextoASha256(String Texto)
        {
            //Para transformar el String de la clave en String SHA256
            StringBuilder HASHClave = new StringBuilder();
            SHA256 HASH = SHA256Managed.Create();
            Encoding codificacion = Encoding.UTF8;
            Byte[] result = HASH.ComputeHash(codificacion.GetBytes(Texto));
            foreach (Byte bits in result)
                HASHClave.Append(bits.ToString("x2"));
            return HASHClave.ToString();
        }
        public bool validarRut(string Stringrut)
        {
            //Proceso para verificar el rut
            bool validacioncorrecta = false;
            try
            //try... por si acaso
            {
                //Se pasan las letras a mayusculas,se quitan . y -
                Stringrut = Stringrut.ToUpper();
                Stringrut = Stringrut.Replace(".", "");
                Stringrut = Stringrut.Replace("-", "");
                int rutLimpio = int.Parse(Stringrut.Substring(0, Stringrut.Length - 1));
                //Se extrae el verificador
                char verificador = char.Parse(Stringrut.Substring(Stringrut.Length - 1, 1));
                //Se recorre el Rut y se calculan sus numeros, de atras en adelante multiplicandolos +1
                int m = 0, s = 1;
                for (; rutLimpio != 0; rutLimpio /= 10)
                {
                    s = (s + rutLimpio % 10 * (9 - m++ % 6)) % 11;
                }
                //Se revisa si el verificador final es correcto
                if (verificador == (char)(s != 0 ? s + 47 : 75))
                {
                    validacioncorrecta = true;
                }
            }
            catch (Exception)
            {//no hay respuesta si hay excepcion, porsiacaso
                validacioncorrecta = false;
            }
            return validacioncorrecta;
        }

        public Boolean VerificarCorreo(String correo)
        {
            //Se verifica el correo si es correcto con funcion Regex.IsMatch 
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(correo, expresion))
            {
                if (Regex.Replace(correo, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static byte[] HmacSHA256(String data, String key)
        {
            //Crea el HASH SHA256 de la clave, para cifrar el texto
            HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(key));
            return hmac.ComputeHash(encoding.GetBytes(data));
        }

        public static readonly Encoding encoding = Encoding.UTF8;
        public string EncriptarTextoAES256(string Texto, string Clave)
        {
            try
            {
                //se estrablecen los paremetors para cifrar el texto con Aes (Rijndael)
                RijndaelManaged CifradoAES = new RijndaelManaged();
                CifradoAES.KeySize = 256;
                CifradoAES.BlockSize = 128;
                CifradoAES.Padding = PaddingMode.PKCS7;
                CifradoAES.Mode = CipherMode.CBC;
                //se codifica a byts la clave y se genera el vector de iniciacion de AES
                CifradoAES.Key = encoding.GetBytes(Clave);
                CifradoAES.GenerateIV();
                //se define las operaciones basicas para AES clave cifrado (key) y vector de iniciacion (IV) y el texto se para a byts
                ICryptoTransform AESencriptrar = CifradoAES.CreateEncryptor(CifradoAES.Key, CifradoAES.IV);
                byte[] buffer = encoding.GetBytes(Texto);
                //Se convierte el texto a encriptar a base64 despues de ser encriptado con Aes
                string TextoEncriptado = Convert.ToBase64String(AESencriptrar.TransformFinalBlock(buffer, 0, buffer.Length));

                String mac = "";
                mac = BitConverter.ToString(HmacSHA256(Convert.ToBase64String(CifradoAES.IV) + TextoEncriptado, Clave)).Replace("-", "").ToLower();

                var keyValues = new Dictionary<string, object>
                {
                    { "iv", Convert.ToBase64String(CifradoAES.IV) },
                    { "value", TextoEncriptado },
                    { "mac", mac },
                };

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                return Convert.ToBase64String(encoding.GetBytes(serializer.Serialize(keyValues)));
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error al Encriptar con AES el Texto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error Encriptacion";
            }
        }

        public string DesencriptarTextoAES256(string Texto, string Clave)
        {
            try
            {
                RijndaelManaged CifradoAES = new RijndaelManaged();
                CifradoAES.KeySize = 256;
                CifradoAES.BlockSize = 128;
                CifradoAES.Padding = PaddingMode.PKCS7;
                CifradoAES.Mode = CipherMode.CBC;
                CifradoAES.Key = encoding.GetBytes(Clave);

                // Base 64 Descodificar
                byte[] base64Descodificado = Convert.FromBase64String(Texto);
                string base64DescodificadoString = encoding.GetString(base64Descodificado);

                // JSON Decode base64Str
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var payload = ser.Deserialize<Dictionary<string, string>>(base64DescodificadoString);

                CifradoAES.IV = Convert.FromBase64String(payload["iv"]);

                ICryptoTransform AESDesencriptado = CifradoAES.CreateDecryptor(CifradoAES.Key, CifradoAES.IV);
                byte[] buffer = Convert.FromBase64String(payload["value"]);

                return encoding.GetString(AESDesencriptado.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error al Desencriptar con AES el Texto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error Desencriptacion";
            }
        }


        private byte[] ValorSalt = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

        public string AES_Encriptacion(string entrada, string contraceña)
        {
            //Codifica los datos a UTF8 para normalizarlos, obtiene los datos encriptados llamando a la
            //funcion AES_Encriptacion la cual necesita de los BytsLimpios (UTF8) y la contracela, luego los 
            //devuelve codificados en Base64
            byte[] LimpiarBytes = System.Text.Encoding.UTF8.GetBytes(entrada);
            byte[] DatosEncriptados = AES_Encriptacion(LimpiarBytes, contraceña);
            return Convert.ToBase64String(DatosEncriptados);
        }

        public byte[] AES_Encriptacion(byte[] entrada, string contraceña)
        {
            //devuelve los datos a encriptar con la contraceña codificada a UTF8 para normalizar los datos
            String contraceñaHash = TextoASha256(contraceña);
            return AES_Encriptacion(entrada, Encoding.UTF8.GetBytes(contraceñaHash));
        }

        public byte[] AES_Encriptacion(byte[] entrada, byte[] contraceña)
        {
            //toma los datos a encriptar(entrada) y la contraceña en byts, deriva la contraceña añadiendole salt par aumentar la seguridad
            //devuelve los datos a encriptar(entrada), la clave derivada en en byts de 32 y 16 byts para empezar a encriptar los datos
            //Nota Salt usa bits pseudoaleatorios para fortificar la contraceña, el cual le agrega nuevos valores a la clave a nivel de bits,
            //lo cual permite de 2 claves iguales, terminen teniendo valores diferentes. 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(contraceña, ValorSalt);
            return AES_Encriptacion(entrada, pdb.GetBytes(32), pdb.GetBytes(16));
        }
        public byte[] AES_Encriptacion(byte[] DatosLimpios, byte[] Llave, byte[] IV)
        {
            //se establece el uso de MemoryStream y Rijndael (algoritmo cifrado AES), se le da la clave (Key)
            //la cual es la clave de 32 bits, se le da el vector de iniciacion (IV) usando la contraceña con 
            //salt de 16 byts, se incia el proceso de encriptar los byts del datos (CryptoStream) y devuelve
            //el array de byts cifrados
            //Nota el vector de iniciacion da las directrices de como se realizara el cifrado por bloques de 
            //AES (Rijndael) y permite que el resultado del cifrado sea independiente de otros cifrados 
            //hechos por la misma clave.
            byte[] DatosEncriptados = null;
            MemoryStream ms = new MemoryStream();
            Rijndael algoritmoCifrado = Rijndael.Create();
            algoritmoCifrado.KeySize = 256;
            algoritmoCifrado.BlockSize = 128;
            algoritmoCifrado.Key = Llave;
            algoritmoCifrado.IV = IV;
            CryptoStream cs = new CryptoStream(ms, algoritmoCifrado.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(DatosLimpios, 0, DatosLimpios.Length);
            cs.Close();
            DatosEncriptados = ms.ToArray();
            return DatosEncriptados;
        }

        public string AESdesencriptar(string entrada, string contraceña)
        {
            //usa la entrada de datos encriptados y la contraceña en texto, los transforma array de bits
            //llama a la funcion AESdesencriptar enviandole los bytsCifrados y la contraceña en texto
            //y retorna los datos encriptados en codificacion de UTF8
            byte[] BytesCifrado = Convert.FromBase64String(entrada);
            byte[] DatosEncriptados = AESdesencriptar(BytesCifrado, contraceña);
            return System.Text.Encoding.UTF8.GetString(DatosEncriptados);
        }

        public byte[] AESdesencriptar(byte[] entrada, string contraceña)
        {
            //toma los byts cifrados (entrada) y la contracela en texto
            //llama a la funcion AESdesencriptar para que retorne la entra en byts, y la clave
            //con salt en 32 y 16 bits
            String HashContraceña = TextoASha256(contraceña);
            return AESdesencriptar(entrada, Encoding.UTF8.GetBytes(HashContraceña));
        }

        public byte[] AESdesencriptar(byte[] entrada, byte[] contraceña)
        {
            //deriva la clave añadiendole salt, y devuelve la entra en byts (cifrado), la clave en bits con salt de 32 y 16
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(contraceña, ValorSalt);
            return AESdesencriptar(entrada, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        public byte[] AESdesencriptar(byte[] DatosDeCifrado, byte[] Llave, byte[] IV)
        {
            //Comienza el proceso de descifrado, MemoryStream y Rijndael (algoritmo cifrado AES), se establece la clave de cifrado (Key)
            //la cual se genero con la clave original ingresada por el usuario mas salt a 32 byts, y el vector de inicializacion el cual
            //se obtiene de la clave mas salt solo que a 16 byts, se establece el largo del los valores de byts a desencriptar, y se 
            //devuelve el desencriptado como un array de byts.
            byte[] DatosEncriptados = null;
            MemoryStream ms = new MemoryStream();
            Rijndael algortimoCifrado = Rijndael.Create();
            algortimoCifrado.KeySize = 256;
            algortimoCifrado.BlockSize = 128;
            algortimoCifrado.Key = Llave;
            algortimoCifrado.IV = IV;
            CryptoStream cs = new CryptoStream(ms, algortimoCifrado.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(DatosDeCifrado, 0, DatosDeCifrado.Length);
            cs.Close();
            DatosEncriptados = ms.ToArray();
            return DatosEncriptados;
        }

        public byte[] ComprimirDatos(byte[] datos)
        {
            //Comprime los datos a almanecar para que sean mas ligeros, usa el algortimo Deflate con compresion 
            //Optimal la mas fuerte, se demora las tiempo pero el resultaro es mas ligero (tamaño), se 
            //establece el tamaño y largo de los byts de compremira y se devuelve como Array de bits el resultado.
            MemoryStream salida = new MemoryStream();
            DeflateStream dstream = new DeflateStream(salida, CompressionLevel.Optimal);
            dstream.Write(datos, 0, datos.Length);
            return salida.ToArray();
        }

        public byte[] DescomprimirDatos(byte[] datos)
        {
            //Descomprime los datos a almanecar, usa el algortimo Deflate 
            //y se devuelve como Array de bits el resultado.
            MemoryStream entrada = new MemoryStream(datos);
            MemoryStream salida = new MemoryStream();
            DeflateStream dstream = new DeflateStream(entrada, CompressionMode.Decompress);
            dstream.CopyTo(salida);
            return salida.ToArray();
        }

        public Boolean RestringirCaracteresUsuario(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si se ingresa numeros, letras mayuculas, Ñ, minusculasn ñ y borrar se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresRUT(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 45) || (e.KeyChar == 107) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si se ingresa los numeros, el guion -, la k permite que se mantenga en el textbox, y borrar
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresTelefono(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 43) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar >= 40 && e.KeyChar <= 41))
            {
                //numeros, +, borrar y parentesis ( ) se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresClave (KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si se ingresa Si se ingresa los numeros, letras mayuculas, Ñ, minusculasn ñ y borrar se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresNombre(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculasn ñ, borrar y espacio se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresDescripcion(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros y , se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresNombreProducto(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros y , se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresSoloNumeros(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // numeros y borrar se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresDireccion(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros y , se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresHorario(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44) || (e.KeyChar == 58))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros ,(coma) y :(dos puntos) se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresBuscar(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 45 || (e.KeyChar == 44)))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros y , - se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean RestringirCaracteresCorreo(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 64) || (e.KeyChar == 46) || (e.KeyChar >= 48 && e.KeyChar <= 57))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, @, . y numeros se mantienen en el textbox
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
