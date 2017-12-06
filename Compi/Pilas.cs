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
        List<string> listaIDs;

        int comparador;
        int operador;
        string tipo = string.Empty;

        public List<string> ListaIds { get { return this.listaIDs; } set { this.listaIDs = value; } }
        public int Comparador { get { return comparador; } set { comparador = value; } }
        public int Operador { get { return operador; } set { operador = value; } }
        public string TipoDato { get { return tipo; } set { tipo = value; } }

        private Pilas()
        {
            pSimbolos = new Stack<string>();
            pValores = new Stack<int>();
            pIdents = new Stack<string>();
            pAA = new Stack<NodoArblAS>();
            listaIDs = new List<string>();
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


        #region Operaciones para trabajar con la lista de Ids
        public void addId(string id)
        {
            if (this.listaIDs == null)
                this.listaIDs = new List<string>();

            this.listaIDs.Add(id);
        }

        public void limpiaListaIds(string id)
        {
            if (this.listaIDs != null)
                this.listaIDs.Clear();
            else
                this.listaIDs = new List<string>();
        }


        public string getListaIdsLikeString()
        {
            return string.Join(",", this.listaIDs);
        }


        #endregion



        #region Operaciones para trabajar con la pila del Arbol abstracto

        public void pushPAA(NodoArblAS nuevoNodo) { pAA.Push(nuevoNodo); }

        public NodoArblAS popPAA() { return pAA.Pop(); }


        public NodoArblAS peekPAA() { return pAA.Peek(); }

        #endregion


        #region Operaciones para trabajar con la pila de Simbolos

        public void pushPS(string valor)
        {
            pSimbolos.Push(valor);
        }

        public string popPS()
        {
            return pSimbolos.Pop();
        }

        public string peekPS()
        {
            return pSimbolos.Peek();
        }

        #endregion


        #region Operaciones para trabajar con la pila de Valores

        public void pushPV(string valor)
        {
            int nValor;

            if (int.TryParse(valor, out nValor))
                pSimbolos.Push(valor);
        }

        public int popPV()
        {
            return pValores.Pop();
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
            return pIdents.Pop();
        }

        public string peekPI()
        {
            return pIdents.Peek();
        }

        #endregion
    }
}
