using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class Desplazamiento
    {
        int edoOrigen;
        int edoActual;
        string token;

        public int EstadoOrigen { get { return this.edoOrigen; } set { this.edoOrigen = value; } }
        public int EstadoActual { get { return this.edoActual; } set { this.edoActual = value; } }
        public string Token { get { return this.token; } set { this.token = value; } }


        public Desplazamiento(int edo0, int edo1, string tokenDesp)
        {
            this.edoOrigen = edo0;
            this.edoActual = edo1;
            this.token = tokenDesp;
        }
    }
}
