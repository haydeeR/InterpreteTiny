using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsimbolos;

namespace Compi
{
    // ##################################################################################
    // Métodos necesarios para generar los cuadruplos
    // ##################################################################################
    class Cuadruplos
    {
        private static Cuadruplos _instance;

        public static Cuadruplos Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Cuadruplos();

                return _instance;
            }
        }



        private List<Cuadruplo> cuadruplos;
        private List<DslToken> tokensNoGeneranCuadruplos;
        public List<Cuadruplo> LstCuadruplos { get { return this.cuadruplos; } }


        private Cuadruplos()
        {
            this.cuadruplos = new List<Cuadruplo>();
            this.llenaLstTokensNGC();
        }


        private void llenaLstTokensNGC()
        {
            this.tokensNoGeneranCuadruplos = new List<DslToken>();
            //this.tokensNoGeneranCuadruplos.Add(new DslToken(TokenType.KeyWord, "if"));
            //this.tokensNoGeneranCuadruplos.Add(new DslToken(TokenType.KeyWord, "else"));
            //this.tokensNoGeneranCuadruplos.Add(new DslToken(TokenType.KeyWord, "repeat-until"));
            this.tokensNoGeneranCuadruplos.Add(new DslToken(TokenType.FinInstruccion, ";"));
        }


        private bool existeEnTokensNGC(DslToken token)
        {
            bool result = false;
            DslToken tokenBuscado = this.tokensNoGeneranCuadruplos.FirstOrDefault(t => t.TokenType == token.TokenType && t.Value == token.Value);

            if (tokenBuscado != null)
                result = true;

            return result;
        }


        public List<List<Cuadruplo>> recorreArboles(List<NodoArblAS> arboles)
        {
            List<List<Cuadruplo>> listsArboles = new List<List<Cuadruplo>>();

            if (arboles != null)
                foreach (NodoArblAS arbol in arboles)
                {
                    List<Cuadruplo> listaux = null;
                    if ((listaux = recorreArbol(arbol)) != null)
                        listsArboles.Add(listaux);
                }

            return listsArboles;
        }
        /// <summary>
        /// Metodo que recorreArbol  
        /// </summary>
        /// <param name="raiz"></param>
        /// <returns></returns>
        public List<Cuadruplo> recorreArbol(NodoArblAS raiz)
        {
            cuadruplos = new List<Cuadruplo>();

            if (raiz.getNodoIzquierdo() != null)
                generaCuadruplo(raiz.getNodoIzquierdo());

            if (raiz.getNodoDerecho() != null)
                generaCuadruplo(raiz.getNodoDerecho());

            return cuadruplos;
        }


        /// <summary>
        /// Generacion de cuadruplos
        /// </summary>
        /// <param name="nodo">Recibe el arbol</param>
        /// <returns></returns>
        private Cuadruplo generaCuadruplo(NodoArblAS nodo)
        {
            Cuadruplo cuadruploGeneroIzq = null;
            Cuadruplo cuadruploGeneroDer = null;
            NodoArblAS nodoIzquierdo = nodo.getNodoIzquierdo();
            NodoArblAS nodoDerecho = nodo.getNodoDerecho();
            DslToken tokenNodo = nodo.getToken();

            if (nodoIzquierdo == null && nodoDerecho == null)
                return null;

            //Con esto generamos el cuadruplo antes de navegar en sus hijos
            if (tokenNodo.TokenType == TokenType.KeyWord &&
                (tokenNodo.Value == "if" || tokenNodo.Value == "repeat-until"))
            {
                Cuadruplo aux = new Cuadruplo(operador: tokenNodo, numLinea: nodo.Linea);
                cuadruplos.Add(aux);
            }


            //Navegamos en profundidad primero por la izquierda
            if (nodoIzquierdo != null)
            {
                cuadruploGeneroIzq = generaCuadruplo(nodoIzquierdo);
                if (tokenNodo.TokenType == TokenType.KeyWord && tokenNodo.Value == "else")
                {
                    Cuadruplo aux = new Cuadruplo(operador: tokenNodo, numLinea: nodo.Linea);
                    cuadruplos.Add(aux);
                }
            }
            //Navegamos en profunidad por la derecha
            if (nodoDerecho != null)
                cuadruploGeneroDer = generaCuadruplo(nodoDerecho);


            if (this.existeEnTokensNGC(tokenNodo))
                return null;

            // Primer caso, es un operador y tiene como hijos nodos hojas
            if (cuadruploGeneroIzq == null &&
                cuadruploGeneroDer == null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken(),
                                            op1: (nodoIzquierdo != null ? nodoIzquierdo.getToken() : null),
                                            op2: (nodoDerecho != null ? nodoDerecho.getToken() : null),
                                            numLinea: nodo.Linea);
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null &&
                        cuadruploGeneroDer != null)
            {
                //Cuadruplo aux = this.generaCuadruplo(nodo, cuadruploGeneroIzq, cuadruploGeneroDer);
                Cuadruplo aux = null;
                if ((aux = this.generaCuadruplo(nodo, cuadruploGeneroIzq, cuadruploGeneroDer)) != null)
                    cuadruplos.Add(aux);

                return aux;
            }
            else if (cuadruploGeneroIzq == null &&
                        nodoIzquierdo != null &&
                        cuadruploGeneroDer != null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken(),
                                                op1: nodoIzquierdo.getToken(),
                                                op2: cuadruploGeneroDer.resultado.tokenType,
                                                numLinea: nodo.Linea);
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null &&
                    cuadruploGeneroDer == null &&
                    nodoDerecho != null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken(),
                                                op1: cuadruploGeneroIzq.resultado.tokenType,
                                                op2: nodoDerecho.getToken(),
                                                numLinea: nodo.Linea);
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null &&
                    cuadruploGeneroDer == null &&
                    nodoDerecho == null)
            {
                if (nodo.getToken().TokenType == TokenType.FinInstruccion && !cuadruplos.Contains(cuadruploGeneroIzq))
                    cuadruplos.Add(cuadruploGeneroIzq);
            }

            return null;
        }


        private Cuadruplo generaCuadruplo(NodoArblAS nodo, Cuadruplo cuadruploGeneroIzq, Cuadruplo cuadruploGeneroDer)
        {
            Cuadruplo aux = null;

            DslToken dslToken = nodo.getToken();
            if (dslToken.TokenType != TokenType.KeyWord)
            {
                aux = new Cuadruplo(operador: nodo.getToken(),
                                                op1: cuadruploGeneroIzq.resultado.tokenType,
                                                op2: cuadruploGeneroDer.resultado.tokenType,
                                                numLinea: nodo.Linea);
                cuadruplos.Add(aux);
            }
            else if (dslToken.TokenType == TokenType.KeyWord &&
                (dslToken.Value == "if" || dslToken.Value == "else" || dslToken.Value == "endif"))
            {
                aux = generaCuadruploIf(nodo, cuadruploGeneroIzq, cuadruploGeneroDer);
            }

            return aux;
        }



        private Cuadruplo generaCuadruploIf(NodoArblAS nodo, Cuadruplo cuadruploGeneroIzq, Cuadruplo cuadruploGeneroDer)
        {
            Cuadruplo aux = null;

            aux = new Cuadruplo(new Resultado("GoTo", new DslToken(TokenType.Id)), operador: nodo.getToken(),
                                            op1: cuadruploGeneroIzq.resultado.tokenType, op2: (new DslToken(TokenType.Numero, "1")), numLinea: nodo.Linea);
            // Se le asigna el id del cuadruplo a su correspondiente DslToken
            aux.resultado.tokenType.Value = aux.Id.ToString();
            // Se le asigna el id del cruaduplo que continuará si
            // se cumple con la condición del "if"

            DslToken tokenTypCuaDer = cuadruploGeneroDer.resultado.tokenType;

            if (tokenTypCuaDer.Value == "else" || tokenTypCuaDer.Value == "endif")
            {

                //aux.resultado.Value = cuadruploGeneroDer.
            }
            else if (tokenTypCuaDer.TokenType != TokenType.KeyWord)
                aux.resultado.Value = cuadruploGeneroDer.Id.ToString();

            cuadruplos.Add(aux);

            return aux;
        }

        private Cuadruplo generaCuadruploElse(NodoArblAS nodo, Cuadruplo cuadruploGeneroIzq, Cuadruplo cuadruploGeneroDer)
        {


            return null;
        }

        public void ejecuta(int keyExecute)
        {
            //Ejecucion completa
            if (keyExecute == 0)
                this.allExecute();

        }

        public void allExecute()
        {
            TokenType op;
            foreach (Cuadruplo cuadruplo in this.cuadruplos)
            {
                op = cuadruplo.Operador.TokenType;
                switch (op)
                {
                    case TokenType.OperadorAssign://5:
                        MetaSimbolo simbolo = TablaSimbolos.TS.getMetaSimbolo(cuadruplo.Operando1.Value);
                        if (cuadruplo.Operando2.TokenType == TokenType.Id) { }
                        //Buscar en la lista de cuadruplos el id del temporal
                        else if (cuadruplo.Operando2.TokenType == TokenType.Numero)
                        {
                            simbolo.Valor = cuadruplo.Operando2.Value;
                        }
                        break;
                    case TokenType.OperadorComp://6
                                                //Si el operando 1 
                                                //Si es identificador buscar en la tabla de simbolos
                                                //Si es numero comparar directo
                                                //si es un simbolo temporal buscar en la lista de cuadruplos
                        break;
                    case TokenType.OperadorSuma://7 
                        break;
                    case TokenType.OperadorMult://8:
                        break;
                    case TokenType.OperadorPote://9:
                        break;
                    case TokenType.FinInstruccion://14:
                        break;
                }
            }
        }

    }
}
