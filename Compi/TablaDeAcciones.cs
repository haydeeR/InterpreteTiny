using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class TablaDeAcciones
    {
        List<Accion> acciones;
        TablaDesplazamientos tablaDesplazamientos;


        public List<Accion> Acciones { get { return this.acciones; } }


        public TablaDeAcciones(TablaDesplazamientos tabDes, EdoLR1 estado0)
        {
            this.acciones = new List<Accion>();
            this.tablaDesplazamientos = tabDes;
            this.acciones.Add(this.creaAccion(estado0, string.Empty, "$"));
        }

        public int agregaAccion(Accion accion)
        {
            if (accion != null)
            {
                this.acciones.Add(accion);
                return this.acciones.IndexOf(accion);
            }

            return -1;
        }

        public int eliminaAccion(Accion accion)
        {
            int index = this.acciones.IndexOf(accion);
            this.acciones.Remove(accion);
            return index;
        }

        public bool agregaCaracter(string caracter, List<EdoLR1> estados, string cadenaEntrada, out bool desplazo)
        {
            Accion ultimaAccion = this.acciones.Count > 0 ? this.acciones[this.acciones.Count - 1] : null;
            string nuevStrAccion, copiaStrAccion;
            int indEdo, indRed;
            int ultEstado = -1;
            bool resultado = false;
            int ultimoIndAccion = -1;
            desplazo = false;


            if (ultimaAccion != null)
            {
                ultEstado = ultimaAccion.getIndexUltEdoPares();
                nuevStrAccion = tablaDesplazamientos.dameValor(ultEstado, caracter);
                copiaStrAccion = string.Copy(nuevStrAccion);

                if (nuevStrAccion.Contains("S"))
                {
                    nuevStrAccion = nuevStrAccion.Replace("S", "");
                    if (int.TryParse(nuevStrAccion, out indEdo))
                    {
                        ultimoIndAccion = this.agregaAccion(this.creaAccion(ultimaAccion, estados[indEdo], caracter, cadenaEntrada, copiaStrAccion));

                        desplazo = true;
                        resultado = true;
                    }
                }
                else if (nuevStrAccion.Contains("R"))
                {
                    nuevStrAccion = nuevStrAccion.Replace("R", "");
                    if (int.TryParse(nuevStrAccion, out indRed))
                    {
                        Accion nuevObjAccion = this.reduce(ultimaAccion, estados, caracter, cadenaEntrada, indRed, copiaStrAccion);
                        if (nuevObjAccion != null)
                        {
                            this.acciones.Add(nuevObjAccion);
                            desplazo = false;
                            resultado = true;
                        }
                    }
                }
            }

            return resultado;
        }


        private Accion reduce(Accion accionOrigen, List<EdoLR1> estados, string token, string cadenaEntrada, int indRed, string cadStrDesReduc)
        {
            Desplazamiento ultimoDespEliminado = null;
            Accion accion = null;
            EdoLR1 edoLR1Aux = null;
            string valReduccion = string.Empty, tokenOriginal;
            int cantTokensReduc = 0;

            valReduccion = EsquemaDeTraduccion.dameProductor(indRed, out cantTokensReduc);
            if (valReduccion != string.Empty && cantTokensReduc > 0)
            {
                //Creamos la nueva acción con el nuevo desplazamiento reducido
                accion = new Accion(accionOrigen, cadenaEntrada, cadStrDesReduc, token);
                ultimoDespEliminado = accion.reducirDesplazamientos(cantTokensReduc, out tokenOriginal);
                edoLR1Aux = this.dameNuevoEdoDestReduc(ultimoDespEliminado, estados, valReduccion);
                if (edoLR1Aux != null)
                {
                    cadStrDesReduc += ("\t " + edoLR1Aux.getTokenDeLlegada() + " -> " + tokenOriginal);
                    accion.agregaDespReducido(ultimoDespEliminado, edoLR1Aux, valReduccion);
                    accion.AccionDespOReduc = cadStrDesReduc;
                }
                else
                {
                    accion = null;
                }

            }

            return accion;
        }

        /// <summary>
        /// Obtiene el siguiente estado para desplazar en base a la reducción
        /// </summary>
        /// <param name="despEliminados">Desplazamientos eliminados y que serán reducidos</param>
        /// <param name="estados">Estados de la gramatica</param>
        /// <param name="valReduccion">Etiqueta de la reduccion "Productor".</param>
        /// <returns></returns>
        private EdoLR1 dameNuevoEdoDestReduc(Desplazamiento primerDesplazamiento, List<EdoLR1> estados, string valReduccion)
        {
            EdoLR1 sigEdoLR1 = null;
            string valorBuscado = string.Empty;
            int indEdoSig = -1;

            if (primerDesplazamiento != null)
            {
                valorBuscado = tablaDesplazamientos.dameValor(primerDesplazamiento.EstadoOrigen.getId(), valReduccion);
                if (int.TryParse(valorBuscado, out indEdoSig))
                    sigEdoLR1 = estados[indEdoSig];
            }

            return sigEdoLR1;
        }


        private Accion creaAccion(Accion accionOrigen, EdoLR1 edoDestino, string token, string cadenaEntrada, string newActionToStack)
        {
            Accion accion = new Accion(accionOrigen, cadenaEntrada, newActionToStack, token);
            accion.agregaDesplazamiento(new Desplazamiento(accion.getEdoActual(), edoDestino, token));
            return accion;
        }


        private Accion creaAccion(EdoLR1 edo0, string cadenaEntrada, string token)
        {
            Accion accionInicial = new Accion(null, cadenaEntrada, "", token);
            accionInicial.agregaDesplazamiento(new Desplazamiento(null, edo0, "$"));
            return accionInicial;
        }
    }
}
