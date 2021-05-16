using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Lectura_de_documento
{
    class Program
    {

        #region constantes
        private const string _PATH_DOCUMENTO = "C:\\Users\\sebas\\OneDrive\\Documentos\\proyecto\\prueba1\\Documento\\documento.txt";
        private const string _PATH_DOCUMENTO_RESUELTO = "C:\\Users\\sebas\\OneDrive\\Documentos\\proyecto\\prueba1\\Documento\\Resultado1.txt";
        #endregion

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Se inicia el proceso de lectura de documentos.");
                // Se crea el documento para guardar el resultado            
                if (!File.Exists(_PATH_DOCUMENTO_RESUELTO))
                {
                    // Se crea el documento donde se va aguardar la información
                    var documentoRsuelto = File.Create(_PATH_DOCUMENTO_RESUELTO);
                    documentoRsuelto.Close();

                }
                // Valida si existe el documento a leer
                if (File.Exists(_PATH_DOCUMENTO))
                {
                    // Se obtiene toda la informacion de los documentos por parrafos
                    string[] parrafos = File.ReadAllLines(_PATH_DOCUMENTO);

                    guardarMensaje($"La cantidad de Parrafos es: {parrafos.Length}");
                    Console.WriteLine($"La cantidad de Parrafos es: {parrafos.Length}");

                    // Se inicializa los hilos.
                    Thread contadorParrafos = new Thread(() => EncontrarPalabraN(parrafos));
                    Thread encontrarOraciones = new Thread(() => EncontrarOraciones(parrafos));
                    Thread alfanumerico = new Thread(() => Alfanumerico(parrafos));
                    contadorParrafos.Start();
                    encontrarOraciones.Start();
                    alfanumerico.Start();
                }
                else
                {
                    guardarMensaje("No se encontro archivo");
                    Console.WriteLine("No se encontro archivo");
                }
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la funcion: 'Main' => {e}");
            }

        }

        /// <summary>
        ///  Se calcula las palabras alfanumericas que no tengan la letra n
        /// </summary>
        /// <param name="parrofos"></param>
        public static void Alfanumerico(string[] parrofos)
        {
            try
            {
                int contador = 0;
                foreach (var parrafo in parrofos)
                {
                    string[] palabras = Regex.Split(parrafo, " ");
                    foreach (var palabra in palabras)
                    {
                        if (!string.IsNullOrEmpty(palabra))
                        {
                            // Se todas las palabras que no tengan N
                            if (!palabra.ToUpper().Contains("N"))
                            {
                                contador++;
                            }
                        }
                    }
                }

                guardarMensaje($"Palabras que  no tienen la letra n: {contador}");
                Console.WriteLine($"Palabras que  no tienen la letra n: {contador}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la funcion: 'Alfanumerico' => {e}");
            }
        }

        /// <summary>
        /// Encontrar las parabras terminadas en n
        /// </summary>
        /// <param name="parrofos"></param>
        public static void EncontrarPalabraN(string[] parrofos)
        {
            try
            {
                int contador = 0;
                foreach (var parrafo in parrofos)
                {
                    string[] palabras = Regex.Split(parrafo, " ");
                    foreach (var palabra in palabras)
                    {
                        if (!string.IsNullOrEmpty(palabra))
                        {
                            // Se validad todas las palabras en n, si la ultima letra es "." se valida la posicion anterior
                            if (palabra.Substring(palabra.Length - 1) == "n" || (palabra.Substring(palabra.Length - 1) == "." && palabra.Substring(palabra.Length - 2, 1) == "n."))
                            {
                                contador++;
                            }
                        }
                    }
                }

                guardarMensaje($"Palabras terminadas con la letra n son: {contador}");
                Console.WriteLine($"Palabras terminadas con la letra n son: {contador}");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la funcion: 'EncontrarPalabraN' => {e}");
            }
        }

        /// <summary>
        /// Encontrar las oraciones de mas de 15 palabras
        /// </summary>
        /// <param name="parrofos"></param>
        public static void EncontrarOraciones(string[] parrafos)
        {
            try
            {
                int contador = 0;
                foreach (var parrafo in parrafos)
                {
                    string[] palabras = Regex.Split(parrafo, "\\.");
                    foreach (var palabra in palabras)
                    {
                        if (!string.IsNullOrEmpty(palabra))
                        {
                            if (palabra.Split(" ").Length > 15)
                            {
                                contador++;
                            }
                        }
                    }
                }

                guardarMensaje($"oraciones con mas de 15 palabras: {contador}");
                Console.WriteLine($"oraciones con mas de 15 palabras: {contador}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la funcion: 'EncontrarOraciones' => {e}");
            }
            
        }


        /// <summary>
        /// Se guarda el resultado de cada hilo en el documento
        /// </summary>
        /// <param name="texto"></param>
        public static void guardarMensaje(string texto)
        {
            try
            {
                using (StreamWriter file = File.AppendText(_PATH_DOCUMENTO_RESUELTO))
                {
                    file.WriteLine(texto);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la funcion: 'guardarMensaje' => {e}");
            }        
            
            
        }
    }
}
