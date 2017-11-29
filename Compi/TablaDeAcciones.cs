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
            this.acciones.Add(this.creaAccionInicial(estado0));
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
            int indEdo;
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
                        this.agregaAccion(this.creaAccion(ultimaAccion,ultimaAccion.getEdoActual(),estados[indEdo], caracter, cadenaEntrada));
                }
                else if (nuevAccion.Contains("R"))
                {

                }
            }

            return result;
        }


        private Accion creaAccion(Accion accionOrigen, EdoLR1 edoAnterior,EdoLR1 edoDestino, string token, string cadenaEntrada)
        {
            Accion accion = new Accion(accionOrigen, "");
            accion.agregaDesplazamiento(new Desplazamiento(edoAnterior, edoDestino, token));
            accion.CadenaEntrada = cadenaEntrada;

            return accion;
        }


        private Accion creaAccionInicial(EdoLR1 edo0)
        {
            Accion accionInicial = new Accion(null,"");
            accionInicial.agregaDesplazamiento(new Desplazamiento(null, edo0, "$"));
            return accionInicial;
        }
    }
}
