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
            this.acciones.Add(accion);
            return this.acciones.IndexOf(accion);
        }

        public int eliminaAccion(Accion accion)
        {
            int index = this.acciones.IndexOf(accion);
            this.acciones.Remove(accion);
            return index;
        }

        public int agregaCaracter(string caracter)
        {
            Accion ultimaAccion = this.acciones[this.acciones.Count - 1];
            string nuevAccion;
            int ultEstado = -1;
            int result = 0;
            

            if(ultimaAccion != null)
            {
                ultEstado = ultimaAccion.getIndexUltEdoPares();
                nuevAccion = tablaDesplazamientos.dameValor(ultEstado, caracter);
                nuevAccion = nuevAccion.Split('#').ToString();

            }

            return result;
        }


        private Accion creaAccion(EdoLR1 edo, string token)
        {
            Accion accion = new Accion();
            //PilaDesplazamientos

            return null;
        }


        private Accion creaAccionInicial(EdoLR1 edo0)
        {
            Accion accionInicial = new Accion();

            PilaDesplazamientos parIniciales = new PilaDesplazamientos();
            parIniciales.Token = "$";
            parIniciales.IndiceEstado = 0;
            parIniciales.Estado = edo0;
            parIniciales.Cadena = string.Empty;

            accionInicial.Pares.Add(parIniciales);

            return accionInicial;
        }
    }
}
