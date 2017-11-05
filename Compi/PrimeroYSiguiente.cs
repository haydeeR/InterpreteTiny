using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class PrimeroYSiguiente
    {
        /// <summary>
        /// Todas las producciones se agregan en una lista de tokens por produccion
        /// donde la clase contiene una variable string NTerminal y una lista de 
        /// string donde son los tokens
        /// </summary>
        private List<Produccion> tokensXProd = null;
        /// <summary>
        /// Lista de todos los simbolos terminales de la gramatica
        /// </summary>
        private List<string> lTerminales = null;
        /// <summary>
        /// Lista de todos los simbolos no terminales de la gramatica
        /// </summary>
        private List<string> lNoTerminales = null;
        /// <summary>
        /// Lista que contiene el conjunto primero dependiendo de la produccion
        /// </summary>
        private List<List<String>> conjuntoPrimero = null;
        /// <summary>
        /// Lista que contiene el conjunto siguiente
        /// </summary>
        private List<List<String>> conjuntoSiguiente = null;

        public PrimeroYSiguiente(List<Produccion> tokensXProducc, List<string> LNTerm, List<string> LTerm)
        {
            this.tokensXProd = tokensXProducc;
            this.lTerminales = LTerm;
            this.lNoTerminales = LNTerm;
            this.conjuntoPrimero = null;
            this.creaConjuntoPrimero();
            this.evaluaConjuntoPrimero();
            this.creaConjuntoSiguiente();
            this.evaluaConjuntoSiguiente();
        }        

        public void creaConjuntoPrimero()
        {
            this.conjuntoPrimero = new List<List<String>>();
            foreach (string nT in this.lNoTerminales) 
            {
                this.conjuntoPrimero.Add(new List<string>());
            }
        }

        /// <summary>
        /// Evalua el conjunto primero por sus tres reglas
        /// </summary>
        public void evaluaConjuntoPrimero()
        {
            List<int> cambios = new List<int>();
            int inicio = 1;
            int indNoTermDelConjunto = 0;
            List<string> conjuntoPrimeroDNT = new List<string>();
            Boolean nulleable = false;

            while (cambios.Contains(1) || inicio == 1)
            {
                inicio = this.inicializaCambios(cambios);
                nulleable = false;
                foreach (Produccion p in tokensXProd)
                {
                    indNoTermDelConjunto = lNoTerminales.IndexOf(p.getNTerminal());
                    //Si A -> ep domde ep es la cadena vacia Añadir ep a priero de A
                    if ("~" == (p.getTokens()[p.getId()]))     //Si primero de A es un epsilon agregar el epsilon
                    {
                        if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains("~"))
                        {
                            this.conjuntoPrimero[indNoTermDelConjunto].Add("~");
                            cambios.Add(1);
                        }
                        else { cambios.Add(0); }
                    }
                    else if (this.lTerminales.Contains(p.getTokens()[p.getId()]))     //Si primero de A es un terminal
                    {
                        if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains(p.getTokens()[p.getId()]))
                        {
                            this.conjuntoPrimero[indNoTermDelConjunto].Add(p.getTokens()[p.getId()]);
                            cambios.Add(1);
                        }
                        else
                        {
                            cambios.Add(0);
                        }
                    }
                    else if (this.lNoTerminales.Contains(p.getTokens()[p.getId()]))     //Si primero de A es un no terminal
                    {
                        foreach (string token in p.getTokens())
                        {
                            if (this.lNoTerminales.Contains(token))
                            {
                                nulleable = false;
                                //se obtiene el conjunto primero del primer no terminal q hace la derivacion
                                conjuntoPrimeroDNT = this.conjuntoPrimero[this.lNoTerminales.IndexOf(token)];
                                foreach (string terminal in conjuntoPrimeroDNT)
                                {
                                    if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains(terminal) || terminal == "~")
                                    {
                                        if (terminal != "~")
                                        {
                                            this.conjuntoPrimero[indNoTermDelConjunto].Add(terminal);
                                            cambios.Add(1);
                                        }
                                        else
                                        {
                                            nulleable = true;
                                            cambios.Add(0);
                                        }
                                    }
                                    else
                                        cambios.Add(0);
                                }
                            }
                            else
                            {
                                this.conjuntoPrimero[indNoTermDelConjunto].Add(token);
                                cambios.Add(1);
                            }
                            if (!nulleable) 
                            {
                                //Metodo que me diga si el token es nulleable o
                                //no con respecto a la lista que tenemos
                                break;
                            }else if (p.getTokens().IndexOf(token) == p.getTokens().Count -1) // si el indice del token es igual al tamaño de los tokens -1 la produccion es nulleable
                            {
                                if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains("~"))
                                {
                                    this.conjuntoPrimero[indNoTermDelConjunto].Add("~");
                                    cambios.Add(1);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reserva memoria en las listas para el conjunto siguiente
        /// </summary>
        public void creaConjuntoSiguiente()
        {
            this.conjuntoSiguiente = new List<List<String>>();
            foreach (string nT in this.lNoTerminales)
            {
                this.conjuntoSiguiente.Add(new List<string>());
            }
        }
        /// <summary>
        /// Evalua el conjunto siguiente por sus tres reglas
        /// </summary>
        public void evaluaConjuntoSiguiente()
        {
            string siguiente = "";
            string primeroDeSiguiente = "";
            List<int> cambios = new List<int>();
            int posDeB = 1;
            int indNoTermDelConjunto = 0;
            List<string> conjuntoPrimeroDNT = new List<string>();

            foreach (Produccion p in tokensXProd)
            {
                indNoTermDelConjunto = lNoTerminales.IndexOf(p.getNTerminal());
                if (p == tokensXProd[0])  //Estado inicial primer  produccion añadir $ a siguiente de b
                    this.conjuntoSiguiente[indNoTermDelConjunto].Add("$");
                foreach (string token in p.getTokens())
                {
                    if(this.lNoTerminales.Contains(token)) //analizamos el siguiente de B
                    {
                        indNoTermDelConjunto = lNoTerminales.IndexOf(token);
                        posDeB = p.getTokens().IndexOf(token);
                        if((posDeB+1) < p.getTokens().Count)
                        {
                            siguiente = p.getTokens()[posDeB + 1];
                            if (this.lTerminales.Contains(siguiente))
                                this.conjuntoSiguiente[indNoTermDelConjunto].Add(siguiente);
                            else if (this.lNoTerminales.Contains(siguiente))
                            {
                                primeroDeSiguiente = "";
                                foreach (string t in this.conjuntoPrimero[indNoTermDelConjunto])
                                {
                                    if(t != "~")
                                        primeroDeSiguiente += " " + t + " ";
                                }
                                this.conjuntoSiguiente[indNoTermDelConjunto].Add(primeroDeSiguiente);
                            }
                        }
                    }
                }
            }
        }

        private int inicializaCambios(List<int> cambios)
        {
            int tam = cambios.Count;
            cambios.RemoveRange(0, tam);  
            return 0;
        }

        public List<List<string>> dameConjuntoPrimero()
        {
            return this.conjuntoPrimero;
        }

        public List<string> getConjuntoPrimero()
        {
            List<string> nuevoConjunto = new List<string>();
            int ind = 0;
            string conjuntoConcat = "";
            foreach(List<string> conjunto in this.conjuntoPrimero)
            {
                conjuntoConcat = "";
                conjuntoConcat = this.lNoTerminales[ind] + " { ";
                foreach(string token in conjunto)
                {
                    conjuntoConcat += token + " ";
                }
                conjuntoConcat += " }";
                nuevoConjunto.Add(conjuntoConcat);
                ind++;
            }
            return nuevoConjunto;
        }

        public List<string> getConjuntoSiguiente()
        {
            List<string> nuevoConjunto = new List<string>();
            int ind = 0;
            string conjuntoConcat = "";
            foreach (List<string> conjunto in this.conjuntoSiguiente)
            {
                conjuntoConcat = "";
                conjuntoConcat = this.lNoTerminales[ind] + " { ";
                foreach (string token in conjunto)
                {
                    conjuntoConcat += token + " ";
                }
                conjuntoConcat += " }";
                nuevoConjunto.Add(conjuntoConcat);
                ind++;
            }
            return nuevoConjunto;
        }
    }
}
