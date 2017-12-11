using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class Pilas
    {
        private static Pilas instance;

        Stack<string> pSimbolos;
        Stack<int> pValores;
        Stack<string> pIdents;
        Stack<NodoArblAS> pAA;
        Stack<string> operadores;
        List<string> listaIDs;


        string tipo = "nodeclara";
        int comparador;
        //int operador;
        int numLinea = 1;

        public List<string> ListaIds { get { return this.listaIDs; } set { this.listaIDs = value; } }
        public string TipoDato { get { return tipo; } set { tipo = value; } }

        public int Comparador { get { return comparador; } set { comparador = value; } }
        //public int Operador { get { return operador; } set { operador = value; } }
        public int NumeroLinea { get { return this.numLinea; } set { this.numLinea = value; } }


        private Pilas()
        {
            pSimbolos = new Stack<string>();
            pValores = new Stack<int>();
            pIdents = new Stack<string>();
            pAA = new Stack<NodoArblAS>();
            listaIDs = new List<string>();
            operadores = new Stack<string>();
        }


        public static Pilas Stacks
        {
            get
            {
                if (instance == null)
                {
                    instance = new Pilas();
                }
                return instance;
            }
        }


        public void limpiarInstancia()
        {
            this.pSimbolos.Clear();
            this.pValores.Clear();
            this.pIdents.Clear();
            this.pAA.Clear();
            this.listaIDs.Clear();
            this.operadores.Clear();

            this.tipo = "nodeclara";
            this.comparador = 0;
            //this.operador = 0;
            this.numLinea = 1;
        }


        #region Operaciones para trabajar con la lista de Ids
        public void addOperador(string operador)
        {
            if (this.operadores == null)
                this.operadores = new Stack<string>();
            this.operadores.Push(operador);
        }


        public void pushPO(string operador)
        {
            if (this.operadores == null)
                this.operadores = new Stack<string>();
            this.operadores.Push(operador);
        }


        public string popPO()
        {
            if (this.operadores.Count > 0)
                return this.operadores.Pop();

            return string.Empty;
        }


        public string peekPO()
        {
            if (this.operadores.Count > 0)
                return this.operadores.Peek();

            return string.Empty;
        }
        #endregion


        #region Operaciones para trabajar con la lista de Ids
        public void addId(string id)
        {
            if (this.listaIDs == null)
                this.listaIDs = new List<string>();

            this.listaIDs.Add(id);
        }

        private void limpiaListaIds()
        {
            if (this.listaIDs != null)
                this.listaIDs.Clear();
            else
                this.listaIDs = new List<string>();
        }


        public string getListaIdsLikeString()
        {
            string theIds = string.Join(",", this.listaIDs);
            this.limpiaListaIds();
            return theIds;
        }


        #endregion


        #region Operaciones para trabajar con la pila del Arbol abstracto

        public void pushPAA(NodoArblAS nuevoNodo) { pAA.Push(nuevoNodo); }

        public NodoArblAS popPAA()
        {
            if (this.pAA.Count > 0)
                return pAA.Pop();

            return null;
        }


        public NodoArblAS peekPAA() { return pAA.Peek(); }

        #endregion


        #region Operaciones para trabajar con la pila de Simbolos

        public void pushPS(string valor)
        {
            pSimbolos.Push(valor);
        }

        public string popPS()
        {
            if (pSimbolos.Count > 0)
                return pSimbolos.Pop();

            return string.Empty;
        }

        public string peekPS()
        {
            return pSimbolos.Peek();
        }

        public string popAllPS()
        {
            List<string> listAux = this.pSimbolos.Reverse().ToList();
            string cadPS = string.Join("", listAux);
            this.pSimbolos.Clear();
            return cadPS;
        }

        #endregion


        #region Operaciones para trabajar con la pila de Valores

        public void pushPV(string valor)
        {
            int nValor;

            if (int.TryParse(valor, out nValor))
                pValores.Push(nValor);
        }

        public int popPV()
        {
            if (pValores.Count > 0)
                return pValores.Pop();

            return 0;
        }

        public int peekPV()
        {
            return pValores.Peek();
        }

        #endregion


        #region Operaciones para trabajar con la pila de Identificadores

        public void pushPI(string valor)
        {
            pIdents.Push(valor);
        }

        public string popPI()
        {
            if (pIdents.Count > 0)
                return pIdents.Pop();

            return string.Empty;
        }


        public string popAllPI()
        {
            if (this.pIdents.Count > 0)
            {
                List<String> listAux = this.pIdents.Reverse().ToList();
                string cadAux = string.Join("", listAux);
                this.pIdents.Clear();

                return cadAux;
            }

            return string.Empty;
        }

        public string peekPI()
        {
            return pIdents.Peek();
        }


        #endregion
    }
}
