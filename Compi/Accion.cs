using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    /// <summary>
    /// Clase que guarda la acción que se realizará.
    /// </summary>
    class Accion
    {
        List<ParDatos> pares;
        string cadenaEntrada;
        string acciones;

        public string CadenaEntrada { get { return this.cadenaEntrada; } set { this.cadenaEntrada = value; } }
        public List<ParDatos> Pares { get { return this.pares; } set { this.pares = value; } }
        public string Acciones { get { return this.acciones; } set { this.acciones = value; } }


        public Accion()
        {
            this.pares = new List<ParDatos>();
            cadenaEntrada = string.Empty;
        }


        public int agregaPar(ParDatos par)
        {
            int index;

            this.pares.Add(par);
            index = this.pares.IndexOf(par);

            return index;
        }


        public int eliminaPar(ParDatos par)
        {
            int index;

            index = this.pares.IndexOf(par);
            this.pares.Remove(par);

            return index;
        }
    }
}
