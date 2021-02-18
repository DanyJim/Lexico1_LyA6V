using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexico2
{
    class Lexico : Token, IDisposable
    {
        StreamReader archivo;
        StreamWriter bitacora;
        public Lexico()
        {
            Console.WriteLine("Compilando el archivo Prueba.txt...");
            if (File.Exists("C:\\Archivos\\Prueba.txt"))
            {
                archivo = new StreamReader("C:\\Archivos\\Prueba.txt");
                bitacora = new StreamWriter("C:\\Archivos\\Prueba.log");
                bitacora.AutoFlush = true;
                bitacora.WriteLine("Archivo: Prueba.txt");
                bitacora.WriteLine("Directorio: C:\\Archivos");
            }
            else
            {
                throw new Exception("El archivo Prueba.txt no existe");
            }
        }
        //~Lexico()
        public void Dispose()
        {
            CerrarArchivos();
            Console.WriteLine("Finaliza compilacion de Prueba.txt");
        }
        private void CerrarArchivos()
        {
            archivo.Close();
            bitacora.Close();
        }
        public void NextToken()
        {
            char c;
            string palabra = "";

            setContenido(palabra);
            bitacora.WriteLine("Token = " + getContenido());
            bitacora.WriteLine("Clasificacion = " + getClasificacion());
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}
