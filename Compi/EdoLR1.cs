using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    [Serializable]
    public class EdoLR1
    {
        public bool visitado = false;
        /// <summary>
        /// Id hace referencia al indice del estado del
        /// analisis LR1
        /// </summary>
        private int id = -1;
        /// <summary>
        /// producciones es la lista de producciones que contiene 
        /// este estado
        /// </summary>
        private List<Produccion> producciones = null;
        /// <summary>
        /// Indica con quien se llego a ese estado pada despues poder hacer 
        /// la tabla de una forma mucho mas facil
        /// </summary>
        private string tokenDeLlegada = "";
        /// <summary>
        /// Indica cuando el estado ah sido nuevo o ya fue visitado
        /// </summary>
        public bool nuevo = true;
        /// <summary>
        /// Lista de arista que genera el estado
        /// </summary>
        public List<AristaLR1> listAristas = null;

        public string accion = "";

        public List<String> listReducciones = null;

        public EdoLR1()
        {
            this.producciones = new List<Produccion>();
            this.listAristas = new List<AristaLR1>();
            this.listReducciones = new List<String>();
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public void setProduccion(Produccion prod)
        {
            this.producciones.Add(prod);
        }

        public void setTokenDeLlegada(string token)
        {
            this.tokenDeLlegada = token;
        }

        public void setArista(EdoLR1 eDestino, Produccion p)
        {
            AristaLR1 a = new AristaLR1(this, eDestino, p);
            if (!this.existeArista(a))
                this.listAristas.Add(a);
        }

        public List<Produccion> getProducciones()
        {
            return this.producciones;
        }

        public string getTokenDeLlegada()
        {
            return this.tokenDeLlegada;
        }

        public List<AristaLR1> getListArista()
        {
            return this.listAristas;
        }

        public int getId()
        {
            return this.id;
        }

        public bool existeArista(AristaLR1 nArista)
        {
            bool res = false;
            foreach (AristaLR1 a in this.listAristas)
            {
                if ((a.getIndProd() == nArista.getIndProd())
                    && ((a.getEdoDestino() == nArista.getEdoDestino())
                        && (a.getEdoOrigen() == nArista.getEdoOrigen()))
                    )
                    return true;
            }
            return res;
        }

        public EdoLR1 getEdoDestConArista(int indP)
        {
            EdoLR1 aux = null;
            return aux;
        }

        public void setAccion(String accion)
        {
            this.accion = accion;
        }

        public string getAccion()
        {
            return this.accion;
        }

    }
}
