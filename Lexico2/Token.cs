using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexico2
{
    class Token
    {
        public enum Clasificaciones
        {
            Identificador, Numero, Asignacion, Inicializacion, FinSentencia,
            OperadorLogico, OperadorRelacional, OperadorTermino, OperadorFactor,
            IncrementoTermino, IncrementoFactor, Cadena, Ternario, Caracter,
            TipoDato, Zona, Condicion, Ciclo,
        }
        private string Contenido;
        private Clasificaciones Clasificacion;
        public void setContenido(string Contenido)
        {
            this.Contenido = Contenido;
        }
        public void setClasificacion(Clasificaciones Clasificacion)
        {
            this.Clasificacion = Clasificacion;
        }
        public string getContenido()
        {
            return Contenido;
        }
        public Clasificaciones getClasificacion()
        {
            return Clasificacion;
        }
    }
}
