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
        List<Desplazamiento> desplazamientos;
        string cadenaEntrada;
        string acciones;

        public string CadenaEntrada { get { return this.cadenaEntrada; } set { this.cadenaEntrada = value; } }
        public List<Desplazamiento> Desplazamientos { get { return this.desplazamientos; } set { this.desplazamientos = value; } }
        public string Acciones { get { return this.acciones; } set { this.acciones = value; } }


        public Accion(Accion accionAnterior, string cadEntrada)
        {
            cadenaEntrada = cadEntrada;
            this.desplazamientos = new List<Desplazamiento>();

            if (accionAnterior != null && accionAnterior.desplazamientos.Count > 0)
                this.desplazamientos.AddRange(accionAnterior.desplazamientos);
        }


        public int agregaDesplazamiento(Desplazamiento par)
        {
            int index;

            this.desplazamientos.Add(par);
            index = this.desplazamientos.IndexOf(par);

            return index;
        }


        public int eliminaDesplazamiento(Desplazamiento par)
        {
            int index;

            index = this.desplazamientos.IndexOf(par);
            this.desplazamientos.Remove(par);

            return index;
        }



        public int getIndexUltEdoPares()
        {
            int res = -1;
            if (this.desplazamientos != null && this.desplazamientos.Count > 0)
                res = this.desplazamientos[this.desplazamientos.Count - 1].EstadoActual.getId();
            return res;
        }



        public EdoLR1 getEdoActual()
        {
            if (this.desplazamientos != null && this.desplazamientos.Count > 0)
            {
                Desplazamiento ultDesp = this.desplazamientos[this.desplazamientos.Count - 1];
                return ultDesp.EstadoActual;
            }

            return null;
        }
    }
}
