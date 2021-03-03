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
            int estado = 0;
            const int f = -1;
            const int e = -2;
            char c;
            string palabra = "";

            while (estado >= 0)
            {
                c = (char)archivo.Peek();
                switch (estado)
                {
                    case 0:
                        if (char.IsWhiteSpace(c))
                        {
                            estado = 0;
                        }
                        else if(char.IsLetter(c))
                        {
                            estado = 1;
                        }
                        else if (char.IsDigit(c))
                        {
                            estado = 2;
                        }
                        else if (c == '=')
                        {
                            estado = 8;
                        }
                        else if (c == ':')
                        {
                            estado = 9;
                        }
                        else
                        {
                            estado = 29;
                        }
                        break;
                    case 1:
                        setClasificacion(Clasificaciones.Identificador);
                        if (char.IsLetterOrDigit(c))
                        {
                            estado = 1;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 2:
                        setClasificacion(Clasificaciones.Numero);
                        if (char.IsDigit(c))
                        {
                            estado = 2;
                        }
                        else if (c == '.')
                        {
                            estado = 3;
                        }
                        else if(char.ToLower(c) == 'e')
                        {
                            estado = 5;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 3:
                        if (char.IsDigit(c))
                        {
                            estado = 4;
                        }
                        else
                        {
                            throw new Exception("Error lexico: Se espera un digito");
                        }
                        break;
                    case 4:
                        if (char.IsDigit(c))
                        {
                            estado = 4;
                        }
                        else if (char.ToLower(c) == 'e')
                        {
                            estado = 5;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 5:
                        if (c == '+' || c == '-')
                        {
                            estado = 6;
                        }
                        else if (char.IsDigit(c))
                        {
                            estado = 7;
                        }
                        else
                        {
                            throw new Exception("Error lexico: Se espera un digito");
                        }
                        break;
                    case 6:
                        if (char.IsDigit(c))
                        {
                            estado = 7;
                        }
                        else
                        {
                            throw new Exception("Error lexico: Se espera un digito");
                        }
                        break;
                    case 7:
                        if (char.IsDigit(c))
                        {
                            estado = 7;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 8:
                        setClasificacion(Clasificaciones.Asignacion);
                        if (c == '=')
                        {
                            estado = 16;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 9:
                        setClasificacion(Clasificaciones.Caracter);
                        if (c == '=')
                        {
                            estado = 10;
                        }
                        else
                        {
                            estado = f;
                        }
                        break;
                    case 10:
                        setClasificacion(Clasificaciones.Inicializacion);
                        estado = f;
                        break;
                    case 16:
                        setClasificacion(Clasificaciones.OperadorRelacional);
                        estado = f;
                        break;
                    case 29:
                        setClasificacion(Clasificaciones.Caracter);
                        estado = f;
                        break;
                }
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
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}
