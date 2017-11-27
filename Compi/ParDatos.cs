using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class ParDatos
    {
        string cadenaEntrada;
        string token;
        int indEstado;
        EdoLR1 estado;

        public string Cadena { get { return this.cadenaEntrada; } set { this.cadenaEntrada = value; } }
        public string Token { get { return this.token; } set { this.token = value; } }
        public int IndiceEstado { get { return this.indEstado; } set { this.indEstado = value; } }
        public EdoLR1 Estado { get { return this.estado; } set { this.estado = value; } }
    }
}
