using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexico3
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
            int estado = 0;
            const int f = -1;
            const int e = -2;
            char c;
            string palabra = "";

            while (estado >= 0)
            {
                c = (char)archivo.Peek();
                estado = MaquinaTuring(estado, c);
                if (estado >= 0)
                {
                    archivo.Read();
                    if (estado > 0)
                        palabra += c;
                }

            }
            setContenido(palabra);
            if (getClasificacion() == Clasificaciones.Identificador)
            {
                switch (getContenido())
                {
                    case "char":
                    case "int":
                    case "float":
                        setClasificacion(Clasificaciones.TipoDato);
                        break;
                    case "private":
                    case "protected":
                    case "public":
                        setClasificacion(Clasificaciones.Zona);
                        break;
                    case "if":
                    case "else":
                    case "switch":
                        setClasificacion(Clasificaciones.Condicion);
                        break;
                    case "for":
                    case "while":
                    case "do":
                        setClasificacion(Clasificaciones.Ciclo);
                        break;
                }
            }
            bitacora.WriteLine("Token = " + getContenido());
            bitacora.WriteLine("Clasificacion = " + getClasificacion());
        }
        private int MaquinaTuring(int estado,char transicion)
        {
            return estado;
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}
