using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    [Serializable]
    public class Produccion
    {
        /// <summary>
        /// El No terminal que produce la cadena de produccion
        /// </summary>
        private string NTerminal = "";
        /// <summary>
        /// Todos los tokens que son producidos por la misma
        /// </summary>
        private List<String> tokens = null;
        /// <summary>
        /// El token de busqueda que tienen todas las producciones
        /// </summary>
        private string tokenBusqueda = "";
        /// <summary>
        /// es el conjunto para el cual se calculara el conjunto primero 
        /// si el indice(.)esta antes de un No terminal
        /// </summary>
        private List<string> gamma = null;
        /// <summary>
        /// Indice del token para obtener primero y siguiente 
        /// </summary>
        private int id = 0;

        public Produccion(string noTerminal)
        {
            this.NTerminal = noTerminal;
            this.tokens = new List<string>();
            this.gamma = new List<string>();
        }

        public Produccion(Produccion p)
        {
            this.tokens = new List<string>();
            this.gamma = new List<string>();
            this.copiaP(p);
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public void setNTerminal(string noTerminal)
        {
            this.NTerminal = noTerminal;
        }

        public void setNuevoToken(string nToken, int pos = -1)
        {
            if (pos == -1)
                this.tokens.Add(nToken);
            else
                this.tokens.Insert(pos, nToken);
        }

        public void setTokenBusqueda(string tokenBusqueda)
        {
            this.tokenBusqueda = tokenBusqueda;
        }

        public void setGamma(string token)
        {
            this.gamma.Add(token);
        }

        public int getId()
        {
            return this.id;
        }

        public string getNTerminal()
        {
            return this.NTerminal;
        }

        public List<string> getTokens()
        {
            return this.tokens;
        }

        public string getTokensAsString()
        {
            string aux = "";
            foreach(string s in this.tokens)
            {
                if(s != ".")
                    aux += s;
            }
            return aux;
        }
        
        public string getTokenBusqueda()
        {
            return this.tokenBusqueda;
        }

        public List<string> getGamma()
        {
            return this.gamma;
        }
        /// <summary>
        /// Metodo que forma las partes de una produccion para que sea de la forma 
        /// LR0 el nucleo del analisis sintactico LR1
        /// Esto se refiere que existe en la produccion un alpha que puede ser igual a epsilon
        /// unTerminal, un contador de avance(.), un conjunto de terminales no terminales o epsilon
        /// y metodo de busqueda hacia adelante
        /// </summary>
        public string produccionLR()
        {
            List<string> listaAux = this.tokens; 
            bool punto = false;
            int indicePunto = -1, indiceT = 0;
            int indiceBetta = -1;
            this.gamma.Clear();
            string x = "";
            
            foreach (string t in this.tokens)    //Si es de la forma A->alpha . X gamma ,tokenBusqueda
            {
                if (t.CompareTo(".") == 0)
                {
                    indicePunto = indiceT;
                    punto = true;
                    if (indicePunto + 1 < this.tokens.Count())
                    {
                        indiceBetta = indicePunto + 1;
                        x = listaAux[indiceBetta];
                    }
                }
                else {
                    if (punto) {
                        if (indicePunto + 2 < this.tokens.Count()  && indiceBetta < indiceT && indiceBetta != -1)
                        {
                            if (indiceBetta < indicePunto + 2)
                                this.gamma.Add(t);
                        }
                    }
                } 
                indiceT++;
            }
            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indiceIndicador"></param>
        /// <returns></returns>
        public Produccion avanzaIndicadorDeProceso(int indiceIndicador)
        {
            if (this.getTokens()[0] != "." && indiceIndicador > 0)
            {   
                this.tokens.RemoveAt(indiceIndicador - 1);
                this.tokens.Insert(indiceIndicador, ".");
            }
            else if(this.getTokens()[0] == ".")
            {
                this.tokens.RemoveAt(0);
                this.tokens.Insert(indiceIndicador, ".");
            }
            return this;
        }
        
        /// <summary>
        /// Copia los datos de la produccion vieja a la nueva
        /// </summary>
        /// <param name="vieja">Produccion antigua</param>
        public void copiaP(Produccion vieja)
        {
            string gamma = "";
            this.setNTerminal(vieja.getNTerminal());
            this.setId(vieja.getId());
            foreach (string g in vieja.getGamma())
                gamma += g;
            this.setGamma(gamma);
            foreach (string t in vieja.getTokens())
                this.setNuevoToken(t);
            this.setTokenBusqueda(vieja.getTokenBusqueda());
        }

        /// Volver a penasar este metodo buen metodo mala implementacion
        /// <summary>
        /// Dice si la produccion indica una accion de desplazamiento 
        /// o de reduccion segun sea el caso 
        /// </summary>
        /// <param name="g"></param>
        /// <returns>1 si es desplazamiento 2 si es reduccion -1 sin accion</returns>
        public int despORedu(Gramatica g)
        {
            int res = -1;    //Sin Accion
            int indiceP = -1;
            int indiceX = -1;
            string token = "";

            indiceP = this.tokens.IndexOf(".");
            indiceX = indiceP+1;
            if (indiceX < this.tokens.Count)
            {
                token = this.tokens[indiceX];
                if (g.getNTerminal().IndexOf(token) != -1)
                    res = 1;      //Accion desplazar
            }
            else
            {
                if (this.NTerminal[this.NTerminal.Count() - 1] == '\'')
                {
                    //Accion Aceptar
                    res = 3;
                }
                else   //Accion Reducir
                    res = 2;
            }
            return res;
        }

    }
}
