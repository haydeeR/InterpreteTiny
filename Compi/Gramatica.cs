using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Compi
{
    [Serializable]
    public class Gramatica
    {
        /// <summary>
        /// Nombre de la gramatica como un identificador para 
        /// poder guardarla
        /// </summary>
        private string nombre;
        /// <summary>
        /// Lista de todos los simbolos terminales de la gramatica
        /// </summary>
        private List<string> lTerminales = null;
        /// <summary>
        /// Lista de todos los simbolos no terminales de la gramatica
        /// </summary>
        private List<string> lNoTerminales = null;
        /// <summary>
        /// Todas las producciones de la gramatica
        /// </summary>
        private List<string> producciones = null;
        /// <summary>
        /// Todas las producciones se agregan en una lista de tokens por produccion
        /// donde la clase contiene una variable string NTerminal y una lista de 
        /// string donde son los tokens
        /// </summary>
        private List<Produccion> tokensXProd = null;
        /// <summary>
        /// Indica en que numero de produccion va
        /// </summary>
        private int idProd = 0;
        /// <summary>
        /// La lista de estados que regresa el metodo lr1 a la gramatica
        /// </summary>
        private List<EdoLR1> listaEdos = null;

        public Gramatica(string nombre)
        {
            this.nombre = nombre;
            this.lTerminales = new List<string>();
            this.lNoTerminales = new List<string>();
            this.producciones = new List<string>();
            this.tokensXProd = new List<Produccion>();
        }
        
        /// <summary>
        /// Para agregar una gramatica se le debe asignar un nombre
        /// </summary>
        /// <param name="nombre">Nombre que se le asigna a la gramatica</param>
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        /// <summary>
        /// Se diferencia entre terminales y no terminales agregando en una lista todos los 
        /// tokens no terminales
        /// </summary>
        /// <param name="noTerminal">Token no terminal</param>
        public void setNoTerminal(string noTerminal)
        {
            if (lNoTerminales.Exists(element => element == noTerminal) == false)
            {
                this.lNoTerminales.Add(noTerminal);
            }
        }

        /// <summary>
        /// Se diferencia entre terminales y no terminales agregando en una lista todos los 
        /// tokens terminales
        /// </summary>
        /// <param name="terminal"></param>
        public void setTerminal(string terminal)
        {
            if (lTerminales.Exists(element => element == terminal) == false)
            {
                this.lTerminales.Add(terminal);
            }
        }

        /// <summary>
        /// Independientemente si es terminal o no lo es se agrega a una lista de tokens
        /// cada uno de los tokens que existen en una produccion
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ind"></param>
        public void setToken(string token, int ind)
        {
            this.tokensXProd[ind].setNuevoToken(token);
        }

        public void setProduccion(string produccion)
        {
            this.producciones.Add(produccion);
        }

        /// <summary>
        /// Hace que la produccion se divida en terminales y no terminales 
        /// y separa los tokens
        /// </summary>
        /// <param name="produccionIzq"></param>
        /// <param name="produccionDer"></param>
        public void setProduccionAumentada(string produccionIzq, string produccionDer)
        {
            Produccion nueva = new Produccion(produccionIzq);
            nueva.setNuevoToken(produccionDer);
            this.producciones.Insert(0, produccionIzq + "->" + produccionDer);
            this.lNoTerminales.Add(produccionIzq);
            this.tokensXProd.Insert(0, nueva);
            
        }
        /// <summary>
        /// Metodo que regresa el nombre de la gramatica
        /// </summary>
        /// <returns></returns>
        public string getNombre()
        {
            return this.nombre;
        }
        /// <summary>
        /// Metodo que regresa las producciones de la gramatica
        /// </summary>
        /// <returns></returns>
        public List<string> getProducciones()
        {
            return this.producciones;
        }
        /// <summary>
        /// Metodo que regresa una lista de producciones donde cada item
        /// es una produccion con un conjunto de tokenes.
        /// </summary>
        /// <returns></returns>
        public List<Produccion> getTokensXProd()
        {
            return this.tokensXProd;
        }
        /// <summary>
        /// Metodo que regresa una lista dxe no terminales
        /// </summary>
        /// <returns>List <String> </returns>
        public List<string> getNTerminal()
        {
            return this.lNoTerminales;
        }
        /// <summary>
        /// Metodo que regresa una lista cadenas que son los tokens termnales
        /// </summary>
        /// <returns></returns>
        public List<string> getTerminales()
        {
            return this.lTerminales;
        }
        /// <summary>
        /// Metodo que asigna la lista de estados del LR1
        /// </summary>
        /// <param name="listEdos"></param>
        public void setListaEdos(List<EdoLR1> listEdos)
        {
            this.listaEdos = listEdos;
        }
        /// <summary>
        /// Metodo que regresa la lista de estados del LR1
        /// </summary>
        /// <returns></returns>
        public List<EdoLR1> getListaEdos()
        {
            return this.listaEdos;
        }


        /// <summary>
        /// Verifica si un token es terminal o no 
        /// </summary>
        /// <param name="t">Token a evaluar</param>
        /// <returns>true si es token false si no lo es</returns>
        public bool esTerminal(string t)
        {
            bool res = false;
            if (this.lTerminales.Exists(element => element == t))
                res = true;
            return res;
        }
        /// <summary>
        /// Verifica si un token es no terminal 
        /// </summary>
        /// <param name="t">Token a evaluar</param>
        /// <returns>true si es un no terminal false si no lo es</returns>
        public bool esNTerminal(string t)
        {
            bool res = false;
            if (this.lNoTerminales.Exists(element => element == t))
                res = true;
            return res;
        }

        

        /// <summary>
        /// Evalua linea de texto para convertirla en producción
        /// </summary>
        /// <param name="produccion">Cadena de produccion</param>
        /// <returns></returns>
        public bool evaluaProduccion(string produccion)
        {
            bool res = true;
            string[] prod = null;
            string cadIzq = "", cadDer = "";
            int ind = 0;

            //Se busca el indice de los caracteres que parten la producción
            ind = produccion.IndexOf("->");
            if (ind == -1)
                res = false;
            else
            {
                cadIzq = produccion.Remove(ind);
                cadDer = produccion.Remove(0, ind + 2);
                cadIzq = cadIzq.Trim();
      //          MatchCollection cad = patron.Matches(cadIzq);
      //          if (cad.Count <= 0)
                    res = false;
            }
            prod = cadDer.Split('|');
            this.agregaProducciones(cadIzq, prod);
            this.addNoTerminal(cadIzq, true, 0);
            this.difTermYNTerm(prod);
            return res;
        }

        /// <summary>
        /// Método que analiza las partes de una producción y 
        /// define cual es terminal y cual es no termianl
        /// </summary>
        /// <param name="prods"></param>
        public void difTermYNTerm(string[] prods)
        {
            int ind = 0;
            bool res = true;
            string aux;

            foreach (string p in prods)
            {
                aux = p;
                aux = aux.Trim();
                ind = 0;
                while (ind < aux.Length && res)
                {
                    switch (aux[ind])
                    {
                        case '<':
                            if (this.evaluaMenorYMayorQue(aux))
                            {
                                ind = this.addNoTerminal(aux);
                                aux = aux.Remove(0, ind + 1);
                                ind = 0;
                            }
                        break;
                        case '\\':
                            ind = this.addTerminal(aux, 1);
                            aux = aux.Remove(0, ind);
                            ind = 0;
                            break;
                        default:
                            ind = this.addTerminal(aux);
                            aux = aux.Remove(0, ind);
                            ind = 0;
                            break;
                    }
                }
                this.idProd++;
            }
        }

        public void agregaProducciones(string cadIzq, string[] prods)
        { 
            int i = 0;
            for (i = 0; i < prods.Count(); i++ )
            {
                addProduccion(cadIzq, prods[i]);
                this.tokensXProd.Add(new Produccion(cadIzq));
            }
        }

        public void addProduccion(string cadIzq, string cadDer)
        {
            this.setProduccion(cadIzq + "->" + cadDer);
        }


        /// <summary>
        /// Metodo que es mandado llamar desde el LR1 cuando se detecta que la gramatica no 
        /// tiene una produccion aumentada
        /// </summary>
        /// <param name="cadIzq"></param>
        /// <param name="cadDer"></param>
        /// <param name="aumentada"></param>
        public void addProduccionAumentada(string cadIzq, string cadDer)
        {
            this.setProduccionAumentada(cadIzq, cadDer);
            
        }

        public int addTerminal(string prod, int indiceUntil = 0)
        {
            int indTemp = 0;
            string term = "";

            while (indTemp <= indiceUntil)
            {
                term += prod[indTemp];
                indTemp++;
            }
            this.setTerminal(term);
            this.setToken(term, this.idProd);
            return indTemp;
        }

        public int addNoTerminal(string prod, bool prim = false, int indiceAux = -1)
        {
            int indTemp = 1;
            bool flag = true;
            string nterm = "";
            if (prim)
                indTemp = 0;

            while(indTemp < prod.Length && flag)
            {
                switch(prod[indTemp])
                {
                    case '>':
                        flag = false;
                    break;
                }
                if (flag)
                {
                    nterm += prod[indTemp];
                    indTemp++;
                }
            }
            this.setNoTerminal(nterm);
            if(indiceAux == -1)
            this.setToken(nterm, this.idProd);
            return indTemp;
        }

        public bool evaluaMenorYMayorQue(string cad)
        {
            bool res = false;
            if (this.menorMayorQueCorrectos(cad) && !this.menorMayorQueVacios(cad))
                res = true;
            return res;
        }

        public bool menorMayorQueCorrectos(string prod)
        {
            bool res = false;
            int i = 0;
            int valAscci;
            int cont = 0;

            for (i = 0; i < prod.Length && cont >= 0; i++)
            {
                valAscci = (int)prod[i];
                switch (valAscci)
                {
                    case '<':
                        cont++;
                        break;
                    case '>':
                        cont--;
                        break;
                }
            }
            if (cont == 0) //Todo salio bien
                res = true;
            else
                res = false;
            return res;
        }

        public bool menorMayorQueVacios(string prod)
        {
            bool res = false;
            int valAscci = 0;
            int i = 0;

            for (i = 0; i < prod.Length && !res; i++)
            {
                valAscci = (int)prod[i];
                switch (valAscci)
                {
                    case '<':
                        if (i + 1 >= prod.Length || prod[i + 1] == '>')
                            res = true; //Parentesis vacios
                        break;
                }
            }
            return res;
        }
    }

}
