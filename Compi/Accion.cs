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
        string acciones = string.Empty;
        string accionDespReduc = string.Empty;

        public string CadenaEntrada { get { return this.cadenaEntrada; } set { this.cadenaEntrada = value; } }
        public List<Desplazamiento> Desplazamientos { get { return this.desplazamientos; } set { this.desplazamientos = value; } }


        public string AccionDespOReduc { get { return this.accionDespReduc; } set { this.accionDespReduc = value; } }

        public string Acciones
        {
            get
            {
                this.acciones = string.Empty;
                desplazamientos.ForEach(desp => this.acciones += (desp.Token + desp.EstadoActual.getId().ToString()));
                return this.acciones;
            }
            set
            {
                this.acciones = value;
            }
        }


        public Accion(Accion accionAnterior, string cadEntrada, string newMov)
        {
            this.acciones = string.Empty;
            cadenaEntrada = cadEntrada;
            this.accionDespReduc = newMov;
            this.desplazamientos = new List<Desplazamiento>();

            if (accionAnterior != null && accionAnterior.desplazamientos.Count > 0)
                this.desplazamientos.AddRange(accionAnterior.desplazamientos);
        }


        /// <summary>
        /// Elimina los últimos desplazamientos realizados
        /// </summary>
        /// <param name="cantTokensReduc">Cantidad de tokens que serán eliminados</param>
        /// <returns>Retorna el último desplazamiento eliminado, comenzando del último al principio de la lista</returns>
        public Desplazamiento reducirDesplazamientos(int cantTokensReduc, out string produccion)
        {
            List<Desplazamiento> despEliminar = new List<Desplazamiento>();

            // Obtenemos los desplzamientos que serán reducidos
            for (int canTokens = 1; canTokens <= cantTokensReduc; canTokens++)
                despEliminar.Add(this.desplazamientos[(this.desplazamientos.Count - canTokens)]);
            //Eliminamos de la lista los desplazamientos
            despEliminar.ForEach(accionaux => this.desplazamientos.Remove(accionaux));
            produccion = despEliminar[0].EstadoActual.getTokenDeLlegada();

            return despEliminar[(despEliminar.Count - 1)];
        }

        public void dameIndEdoReducido(EdoLR1 estadoOrigen, TablaDesplazamientos tablaDesplazamientos, string valCadReducido)
        {
            Desplazamiento primerDesplazamiento = null, nuevoDesplazamiento = null;
            int indEdoDestino = -1;

            string nuevoEdoCadena = tablaDesplazamientos.dameValor(estadoOrigen.getId(), valCadReducido);

            if (int.TryParse(nuevoEdoCadena, out indEdoDestino))
            {
                nuevoDesplazamiento = new Desplazamiento(primerDesplazamiento.EstadoOrigen, null, valCadReducido);
            }
        }


        /// <summary>
        /// Agrega un desplazamiento que representa una reducción
        /// </summary>
        /// <param name="ultDesplazamiento">Último desplazamiento eliminado de la lista</param>
        /// <param name="edoDestino">Estado al que se realizara el desplazamiento</param>
        /// <param name="valCadReducido">Nombre del token que representa la reducción</param>
        public void agregaDespReducido(Desplazamiento ultDesplazamiento, EdoLR1 edoDestino, string valCadReducido)
        {
            Desplazamiento nuevoDesplazamiento = null;

            if (ultDesplazamiento != null && edoDestino != null)
            {
                nuevoDesplazamiento = new Desplazamiento(ultDesplazamiento.EstadoOrigen, edoDestino, valCadReducido);
                this.desplazamientos.Add(nuevoDesplazamiento);
            }
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
