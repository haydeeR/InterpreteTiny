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


        public Accion(Accion accionAnterior, string cadEntrada, int cantTokensReduc)
        {
            List<Desplazamiento> despEliminar = null;
            Desplazamiento ultDesplazamiento = null, primerDesplazamiento = null;

            this.cadenaEntrada = cadEntrada;
            this.desplazamientos = new List<Desplazamiento>();

            if (accionAnterior != null && accionAnterior.desplazamientos.Count > 0)
                this.desplazamientos.AddRange(accionAnterior.desplazamientos);

            // Obtenemos los desplzamientos que serán reducidos
            for (int canTokens = 1; canTokens <= cantTokensReduc; canTokens++)
                despEliminar.Add(this.desplazamientos[(this.desplazamientos.Count - canTokens)]);
            // Obtenemos el primer desplazamiento y el último
            ultDesplazamiento = despEliminar.Count > 0 ? despEliminar[0] : null;
            primerDesplazamiento = cantTokensReduc > 1 && despEliminar.Count > 0 ? despEliminar[despEliminar.Count - 1] : ultDesplazamiento;
            //Eliminamos de la lista los desplzamientos
            despEliminar.ForEach(accionaux => this.desplazamientos.Remove(accionaux));
        }


        public List<Desplazamiento> reducirDesplazamientos(int cantTokensReduc)
        {
            List<Desplazamiento> despEliminar = null;

            // Obtenemos los desplzamientos que serán reducidos
            for (int canTokens = 1; canTokens <= cantTokensReduc; canTokens++)
                despEliminar.Add(this.desplazamientos[(this.desplazamientos.Count - canTokens)]);

            //Eliminamos de la lista los desplazamientos
            despEliminar.ForEach(accionaux => this.desplazamientos.Remove(accionaux));

            return despEliminar;
        }

        public void dameIndEdoReducido(EdoLR1 estadoOrigen, TablaDesplazamientos tablaDesplazamientos, string valCadReducido)
        {
            Desplazamiento ultDesplazamiento = null, primerDesplazamiento = null, nuevoDesplazamiento = null;
            int indEdoDestino = -1;

            

            string nuevoEdoCadena = tablaDesplazamientos.dameValor(estadoOrigen.getId(),valCadReducido);

            if(int.TryParse(nuevoEdoCadena, out indEdoDestino))
            {
                nuevoDesplazamiento = new Desplazamiento(primerDesplazamiento.EstadoOrigen,null, valCadReducido);
            }
        }


        public void agregaEdoReducido(List<Desplazamiento> despEliminados, EdoLR1 edoDestino, string valCadReducido)
        {
            Desplazamiento ultDesplazamiento = null, primerDesplazamiento = null, nuevoDesplazamiento = null;
            //Obtenemos el primer desplazamiento y el último
            ultDesplazamiento = despEliminados != null && despEliminados.Count > 0 ? despEliminados[0] : null;
            primerDesplazamiento = despEliminados != null && despEliminados.Count > 0 ? despEliminados[despEliminados.Count - 1] : ultDesplazamiento;
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

        public EdoLR1 getEdoAnterior()
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
