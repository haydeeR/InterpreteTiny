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
        Stack<string> pilaIDs;


        Stack<int> bloqueIFCode;
        Stack<int> bloqueRepeatCode;

        string tipo = "nodeclara";
        int comparador;
        //int operador;
        int numLinea = 1;

        public Stack<string> ListaIds { get { return this.pilaIDs; } set { this.pilaIDs = value; } }
        public string TipoDato { get { return tipo; } set { tipo = value; } }

        public int Comparador { get { return comparador; } set { comparador = value; } }
        //public int Operador { get { return operador; } set { operador = value; } }
        public int NumeroLinea { get { return this.numLinea; } set { this.numLinea = value; } }

        public void incrementeNoLinea()
        {
            this.numLinea++;
        }


        public void decrementeNoLinea()
        {
            this.numLinea--;
        }

        private Pilas()
        {
            pSimbolos = new Stack<string>();
            pValores = new Stack<int>();
            pIdents = new Stack<string>();
            pAA = new Stack<NodoArblAS>();
            pilaIDs = new Stack<string>();
            operadores = new Stack<string>();

            bloqueIFCode = new Stack<int>();
            bloqueRepeatCode = new Stack<int>();
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
            this.pilaIDs.Clear();
            this.operadores.Clear();

            this.bloqueIFCode.Clear();
            this.bloqueRepeatCode.Clear();

            this.tipo = "nodeclara";
            this.comparador = 0;
            //this.operador = 0;
            this.numLinea = 1;
        }


        #region Operaciones para trabajar con la pila de Bloque de Codigo If
        public void pushIC(int numLinea)
        {
            this.bloqueIFCode.Push(numLinea);
        }



        public int peekIC()
        {
            int numLinea = -1;

            if (this.bloqueIFCode.Count > 0)
                numLinea = this.bloqueIFCode.Peek();

            return numLinea;
        }



        public int popIC()
        {
            int numLinea = -1;

            if (this.bloqueIFCode.Count > 0)
                numLinea = this.bloqueIFCode.Pop();

            return numLinea;
        }
        #endregion


        #region Operaciones para trabajar con la pila de Bloque de Codigo Repeat
        public void pushRUC(int numLinea)
        {
            this.bloqueRepeatCode.Push(numLinea);
        }



        public int peekRUC()
        {
            int numLinea = -1;

            if (this.bloqueRepeatCode.Count > 0)
                numLinea = this.bloqueRepeatCode.Peek();

            return numLinea;
        }



        public int popRUC()
        {
            int numLinea = -1;

            if (this.bloqueRepeatCode.Count > 0)
                numLinea = this.bloqueRepeatCode.Pop();

            return numLinea;
        }
        #endregion


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


        #region Operaciones para trabajar con la pila de Ids
        public void pushId(string id)
        {
            if (this.pilaIDs == null)
                this.pilaIDs = new Stack<string>();

            this.pilaIDs.Push(id);
        }

        public string popId()
        {
            if (pilaIDs.Count > 0)
                return this.pilaIDs.Pop();

            return string.Empty;
        }

        private void limpiaListaIds()
        {
            if (this.pilaIDs != null)
                this.pilaIDs.Clear();
            else
                this.pilaIDs = new Stack<string>();
        }


        public string getListaIdsLikeString()
        {
            string theIds = string.Join(",", this.pilaIDs);
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
