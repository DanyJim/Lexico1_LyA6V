using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexico1
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

            while (char.IsWhiteSpace(c = (char)archivo.Read()))
            {
            }
            palabra += c; //palabra = palabra + c;
            if (char.IsLetter(c))
            {
                setClasificacion(Clasificaciones.Identificador);
                while (char.IsLetterOrDigit(c = (char)archivo.Peek()))
                {
                    palabra += c;
                    archivo.Read();
                }
            }
            else if (char.IsDigit(c))
            {
                setClasificacion(Clasificaciones.Numero);
                while (char.IsDigit(c = (char)archivo.Peek()))
                {
                    palabra += c;
                    archivo.Read();
                }
                if (c == '.')
                {
                    palabra += c;
                    archivo.Read();
                    if (char.IsDigit(c = (char)archivo.Read()))
                    {
                        palabra += c;
                        while (char.IsDigit(c = (char)archivo.Peek()))
                        {
                            palabra += c;
                            archivo.Read();
                        }
                    }
                    else
                    {
                        throw new Exception("Error lexico: Se espera un digito");
                    }  
                }
                if (char.ToLower(c) == 'e')
                {
                    palabra += c;

                    archivo.Read();
                    if((c=(char)archivo.Peek()) == '+' || c == '-')
                    {
                        palabra += c;
                        archivo.Read();
                    }
                    Console.WriteLine(palabra);
                    if (char.IsDigit(c = (char)archivo.Read()))
                    {
                        palabra += c;
                        while (char.IsDigit(c = (char)archivo.Peek()))
                        {
                            palabra += c;
                            archivo.Read();
                        }
                        
                    }
                    else
                    {
                        throw new Exception("Error lexico: Se espera un digito");
                    }
                }

            }
            else if (c=='=')
            {
                setClasificacion(Clasificaciones.Asignacion);
                if ((c = (char)archivo.Peek()) == '=')
                {
                    palabra += c;
                    archivo.Read();
                    setClasificacion(Clasificaciones.OperadorRelacional);
                }
            }
            else if (c == ':')
            {
                setClasificacion(Clasificaciones.Caracter);
                if ((c = (char)archivo.Peek()) == '=')
                {
                    palabra += c;
                    archivo.Read();
                    setClasificacion(Clasificaciones.Inicializacion);
                }
            }
            else if (c == ';')
            {
                setClasificacion(Clasificaciones.FinSentencia);
            }
            else if (c == '!')
            {
                setClasificacion(Clasificaciones.OperadorLogico);
                if ((c = (char)archivo.Peek()) == '=')
                {
                    palabra += c;
                    archivo.Read();
                    setClasificacion(Clasificaciones.OperadorRelacional);
                }
            }
            else if (c == '&')
            {
                setClasificacion(Clasificaciones.Caracter);
                if ((c = (char)archivo.Peek()) == '&')
                {
                    palabra += c;
                    archivo.Read();
                    setClasificacion(Clasificaciones.OperadorLogico);
                }
            }
            else if (c == '|')
            {
                setClasificacion(Clasificaciones.Caracter);
                if ((c = (char)archivo.Peek()) == '|')
                {
                    palabra += c;
                    archivo.Read();
                    setClasificacion(Clasificaciones.OperadorLogico);
                }
            }
            else 
            {
                setClasificacion(Clasificaciones.Caracter);
            }
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
