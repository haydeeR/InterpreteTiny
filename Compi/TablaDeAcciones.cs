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

        public TablaDeAcciones() {
            acciones = new List<Accion>();
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
    }
}
