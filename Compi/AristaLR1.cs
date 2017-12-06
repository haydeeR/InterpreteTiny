using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    [Serializable]
    public class AristaLR1
    {
        /// <summary>
        /// 
        /// </summary>
        private int indiceDeProduccion = -1;
        /// <summary>
        /// 
        /// </summary>
        private EdoLR1 edoOrigen = null;
        /// <summary>
        /// 
        /// </summary>
        private EdoLR1 edoDestino = null;
        /// <summary>
        /// 
        /// </summary>
        private string accion = "";

        public AristaLR1(EdoLR1 eOrigen, EdoLR1 eDestino, Produccion p)
        {
            this.edoOrigen = eOrigen;
            this.edoDestino = eDestino;
            this.accion = "S" + eDestino.getId();
        }

        public void setAccion(string accion)
        {
            this.accion = accion;
        }

        public EdoLR1 getEdoOrigen()
        {
            return this.edoOrigen;
        }

        public EdoLR1 getEdoDestino()
        {
            return this.edoDestino;
        }

        public int getIndProd()
        {
            return this.indiceDeProduccion;
        }

        public string getAccion()
        {
            return this.accion;
        }
    }
}
