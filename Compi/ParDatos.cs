using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class Desplazamiento
    {
        EdoLR1 edoOrigen;
        EdoLR1 edoActual;
        string token;

        public EdoLR1 EstadoOrigen { get { return this.edoOrigen; } set { this.edoOrigen = value; } }
        public EdoLR1 EstadoActual { get { return this.edoActual; } set { this.edoActual = value; } }
        public string Token { get { return this.token; } set { this.token = value; } }


        public Desplazamiento(EdoLR1 edo0, EdoLR1 edo1, string tokenDesp)
        {
            this.edoOrigen = edo0;
            this.edoActual = edo1;
            this.token = tokenDesp;
        }
    }
}
