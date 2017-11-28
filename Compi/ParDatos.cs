using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class PilaDesplazamientos
    {
        EdoLR1 estado;
        string cadenaEntrada;
        string token;
        int indEstado;
        

        public string Cadena { get { return this.cadenaEntrada; } set { this.cadenaEntrada = value; } }
        public string Token { get { return this.token; } set { this.token = value; } }
        public int IndiceEstado { get { return this.indEstado; } set { this.indEstado = value; } }
        public EdoLR1 Estado { get { return this.estado; } set { this.estado = value; } }
    }
}
