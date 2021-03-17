using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexico3
{
    //Requerimiento 1: Identificar Linea y Caracter en los Errores Lexicos
    //Requerimiento 2: Levantar la Excepcion cuando exista un Error e identificar el Tipo de Error
    class Lexico : Token, IDisposable
    {
        StreamReader archivo;
        StreamWriter bitacora;
        const int F = -1;
        const int E = -2;
        int[,] TRAND6V =
        {
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?, La
            {  0, 0, 1, 2,29, 1,19,20, 8, 9,11,12,13,14,17,18,22,22,22,24,26,28,29 },
            {  F, F, 1, 1, F, 1, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, 2, 3, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  E, E, E, 4, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            {  F, F, F, 4, F, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  E, E, E, 7, E, E, 6, 6, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            {  E, E, E, 7, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            {  F, F, F, 7, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F,10, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F,15, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F,15, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F,16, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F,21, F,21, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F,21,21, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F,23, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            { 24, E,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,25,24,24,24 },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            { 26, E,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,27,26,26 },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?, La
        };
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
            char c;
            string palabra = "";

            while (estado >= 0)
            {
                c = (char)archivo.Peek();
                estado = TRAND6V[estado,Columna(c)];
                Clasifica(estado);
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
        private int Columna(char t)
        {
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?, La
            if (FinArchivo())
                return 1;
            if (char.IsWhiteSpace(t))
                return 0;
            if (char.ToLower(t) == 'e')
                return 5;
            if (char.IsLetter(t))
                return 2;
            if (char.IsDigit(t))
                return 3;
            if (t == '.')
                return 4;
            if (t == '+')
                return 6;
            if (t == '-')
                return 7;
            if (t == '=')
                return 8;
            if (t == ':')
                return 9;
            if (t == ';')
                return 10;
            if (t == '&')
                return 11;
            if (t == '|')
                return 12;
            if (t == '!')
                return 13;
            if (t == '>')
                return 14;
            if (t == '<')
                return 15;
            if (t == '*')
                return 16;
            if (t == '/')
                return 17;
            if (t == '%')
                return 18;
            if (t == '"')
                return 19;
            if (t == '\'')
                return 20;
            if (t == '?')
                return 21;
            return 22;
            //WS,EF, L, D, ., E, +, -, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?, La
        }
        private void Clasifica(int estado)
        {
            switch (estado)
            {
                case 1:
                    setClasificacion(Clasificaciones.Identificador);
                    break;
                case 2:
                    setClasificacion(Clasificaciones.Numero);
                    break;
                case 8:
                    setClasificacion(Clasificaciones.Asignacion);
                    break;
                case 9:
                case 12:
                case 13:
                case 29:
                    setClasificacion(Clasificaciones.Caracter);
                    break;
                case 10:
                    setClasificacion(Clasificaciones.Inicializacion);
                    break;
                case 11:
                    setClasificacion(Clasificaciones.FinSentencia);
                    break;
                case 14:
                case 15:
                    setClasificacion(Clasificaciones.OperadorLogico);
                    break;
                case 16:
                case 17:
                case 18:
                    setClasificacion(Clasificaciones.OperadorRelacional);
                    break;
                case 19:
                case 20:
                    setClasificacion(Clasificaciones.OperadorTermino);
                    break;
                case 21:
                    setClasificacion(Clasificaciones.IncrementoTermino);
                    break;
                case 22:
                    setClasificacion(Clasificaciones.OperadorFactor);
                    break;
                case 23:
                    setClasificacion(Clasificaciones.IncrementoFactor);
                    break;
                case 24:
                case 26:
                    setClasificacion(Clasificaciones.Cadena);
                    break;
                case 28:
                    setClasificacion(Clasificaciones.Ternario);
                    break;
            }
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}
