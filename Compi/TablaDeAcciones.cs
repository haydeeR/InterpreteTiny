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

        public TablaDeAcciones(TablaDesplazamientos tabDes, EdoLR1 estado0)
        {
            this.acciones = new List<Accion>();
            this.tablaDesplazamientos = tabDes;
            this.acciones.Add(this.creaAccionInicial(estado0, string.Empty));
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

        public int agregaCaracter(string caracter, List<EdoLR1> estados, string cadenaEntrada)
        {
            Accion ultimaAccion = this.acciones[this.acciones.Count - 1];
            string nuevAccion;
            int indEdo, indRed;
            int ultEstado = -1;
            int result = 0;

            if (ultimaAccion != null)
            {
                ultEstado = ultimaAccion.getIndexUltEdoPares();
                nuevAccion = tablaDesplazamientos.dameValor(ultEstado, caracter);

                if (nuevAccion.Contains("S"))
                {
                    nuevAccion = nuevAccion.Replace("S", "");
                    if (int.TryParse(nuevAccion, out indEdo))
                        this.agregaAccion(this.creaAccion(ultimaAccion, estados[indEdo], caracter, cadenaEntrada));
                }
                else if (nuevAccion.Contains("R"))
                {
                    nuevAccion = nuevAccion.Replace("R", "");
                    if (int.TryParse(nuevAccion, out indRed))
                    {
                        this.reduce(ultimaAccion, caracter, cadenaEntrada, indRed);
                    }
                }
            }

            return result;
        }


        private Accion reduce(Accion accionOrigen, string token, string cadenaEntrada, int indRed)
        {
            Accion accion = null;
            string valReduccion = string.Empty;
            int cantTokensReduc = 0;

            valReduccion = EsquemaDeTraduccion.dameProductor(indRed, out cantTokensReduc);
            if (valReduccion != string.Empty && cantTokensReduc > 0)
            {
                //Creamos la nueva acción con el nuevo desplazamiento reducido
                accion = new Accion(accionOrigen, cadenaEntrada);
                accion.dameIndEdoReducido(accion.reducirDesplazamientos(cantTokensReduc), tablaDesplazamientos, valReduccion);
                Desplazamiento nuevoDesp = new Desplazamiento(primerDesplazamiento.EstadoOrigen, null, valReduccion);
                string valorDesp = tablaDesplazamientos.dameValor(ultEstado, caracter);
                accion.CadenaEntrada = cadenaEntrada;
            }

            return accion;
        }


        private Accion creaAccion(Accion accionOrigen, EdoLR1 edoDestino, string token, string cadenaEntrada)
        {
            Accion accion = new Accion(accionOrigen, cadenaEntrada);
            accion.agregaDesplazamiento(new Desplazamiento(accion.getEdoActual(), edoDestino, token));

            return accion;
        }


        private Accion creaAccionInicial(EdoLR1 edo0, string cadenaEntrada)
        {
            Accion accionInicial = new Accion(null, cadenaEntrada);
            accionInicial.agregaDesplazamiento(new Desplazamiento(null, edo0, "$"));
            return accionInicial;
        }
    }
}
