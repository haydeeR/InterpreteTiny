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
        private TerminalWinForm terminal;

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
        private Stack<Cuadruplo> bloquesCuadruplos;


        private Cuadruplos()
        {
            this.cuadruplos = new List<Cuadruplo>();
            this.bloquesCuadruplos = new Stack<Cuadruplo>();
            this.llenaLstTokensNGC();
        }


        public TerminalWinForm Terminal
        {
            set
            {
                this.terminal = value;
            }
        }


        public static void limpiaInstancia()
        {
            _instance = null;
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
            if (cuadruplos == null)
                cuadruplos = new List<Cuadruplo>();
            else
                cuadruplos.Clear();

            if (raiz != null)
            {
                if (raiz.getToken().TokenType != TokenType.FinInstruccion)
                    generaCuadruplo(raiz);

                if (raiz.getNodoIzquierdo() != null)
                    generaCuadruplo(raiz.getNodoIzquierdo());

                if (raiz.getNodoDerecho() != null)
                    generaCuadruplo(raiz.getNodoDerecho());
            }

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
            Cuadruplo inicioBloqueCodigo = null;
            NodoArblAS nodoIzquierdo = nodo.getNodoIzquierdo();
            NodoArblAS nodoDerecho = nodo.getNodoDerecho();
            DslToken tokenNodo = nodo.getToken();

            if (nodoIzquierdo == null && nodoDerecho == null)
                return null;

            //Con esto generamos el cuadruplo antes de navegar en sus hijos
            if (tokenNodo.TokenType == TokenType.KeyWord &&
                (tokenNodo.Value == "if" || tokenNodo.Value == "repeat-until"))
            {
                if (tokenNodo.Value == "if")
                    inicioBloqueCodigo = new Cuadruplo(operador: tokenNodo.Clone(),
                                        numLinea: nodo.Linea,
                                        resultado: new Resultado(Guid.NewGuid().ToString(),
                                                                    new DslToken(TokenType.KeyWord, "GoTo")
                                                                )
                                        );
                else
                    inicioBloqueCodigo = new Cuadruplo(operador: tokenNodo.Clone(), numLinea: nodo.Linea);
                cuadruplos.Add(inicioBloqueCodigo);
            }


            //Navegamos en profundidad primero por la izquierda
            if (nodoIzquierdo != null)
            {
                cuadruploGeneroIzq = generaCuadruplo(nodoIzquierdo);
                if (tokenNodo.TokenType == TokenType.KeyWord && tokenNodo.Value == "else")
                {
                    inicioBloqueCodigo = new Cuadruplo(operador: tokenNodo, numLinea: nodo.Linea);
                    cuadruplos.Add(inicioBloqueCodigo);
                    bloquesCuadruplos.Push(inicioBloqueCodigo);
                }
            }
            //Navegamos en profunidad por la derecha
            if (nodoDerecho != null)
                cuadruploGeneroDer = generaCuadruplo(nodoDerecho);

            //Si es ; no genera cuadruplo
            if (this.existeEnTokensNGC(tokenNodo))
                return null;

            this.finalizaBloqueIfElse(inicioBloqueCodigo, cuadruploGeneroDer);

            // Primer caso, es un operador y tiene como hijos nodos hojas
            if (cuadruploGeneroIzq == null &&
                cuadruploGeneroDer == null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken().Clone(),
                                            op1: (nodoIzquierdo != null ? nodoIzquierdo.getToken() : null),
                                            op2: (nodoDerecho != null ? nodoDerecho.getToken() : null),
                                            numLinea: nodo.Linea);
                aux = this.esElMismoTokenDeRetorno(inicioBloqueCodigo, aux);
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null &&
                        cuadruploGeneroDer != null)
            {
                Cuadruplo aux = this.generaCuadruplo(nodo, cuadruploGeneroIzq, cuadruploGeneroDer);
                aux = this.esElMismoTokenDeRetorno(inicioBloqueCodigo, aux);

                return aux;
            }
            else if (cuadruploGeneroIzq == null &&
                        nodoIzquierdo != null &&
                        cuadruploGeneroDer != null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken().Clone(),
                                                op1: nodoIzquierdo.getToken(),
                                                op2: cuadruploGeneroDer.resultado.tokenType,
                                                numLinea: nodo.Linea);
                aux = this.esElMismoTokenDeRetorno(inicioBloqueCodigo, aux);
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null &&
                    cuadruploGeneroDer == null &&
                    nodoDerecho != null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken().Clone(),
                                                op1: cuadruploGeneroIzq.resultado.tokenType,
                                                op2: nodoDerecho.getToken(),
                                                numLinea: nodo.Linea);
                aux = this.esElMismoTokenDeRetorno(inicioBloqueCodigo, aux);
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null &&
                    cuadruploGeneroDer == null &&
                    nodoDerecho == null)
            {
                if (nodo.getToken().TokenType == TokenType.FinInstruccion && !cuadruplos.Contains(cuadruploGeneroIzq))
                {
                    cuadruploGeneroIzq = this.esElMismoTokenDeRetorno(inicioBloqueCodigo, cuadruploGeneroIzq);
                    cuadruplos.Add(cuadruploGeneroIzq);
                }
            }

            return null;
        }


        private void finalizaBloqueIfElse(Cuadruplo inicioBloqueCodigo, Cuadruplo cuadruploGeneroDer)
        {
            if (inicioBloqueCodigo != null && cuadruploGeneroDer != null &&
                inicioBloqueCodigo.Operador.TokenType == TokenType.KeyWord &&
                inicioBloqueCodigo.Operador.Value == "if")
            {
                if (cuadruploGeneroDer.Operador.TokenType == TokenType.KeyWord &&
                    cuadruploGeneroDer.Operador.Value.Contains("else") && this.bloquesCuadruplos.Count > 0)
                {
                    Cuadruplo auxElse = this.bloquesCuadruplos.Pop();
                    inicioBloqueCodigo.resultado.Value = auxElse.Id.ToString();
                }
            }
        }


        private Cuadruplo esElMismoTokenDeRetorno(Cuadruplo inicioBloqueCodigo, Cuadruplo finBloqueCodigo)
        {
            if (inicioBloqueCodigo != null && finBloqueCodigo != null &&
                inicioBloqueCodigo.Operador.TokenType == TokenType.KeyWord &&
                inicioBloqueCodigo.Operador.TokenType == finBloqueCodigo.Operador.TokenType &&
                inicioBloqueCodigo.Operador.Value == finBloqueCodigo.Operador.Value &&
                (inicioBloqueCodigo.Operador.Value == "if" ||
                inicioBloqueCodigo.Operador.Value == "else" ||
                inicioBloqueCodigo.Operador.Value == "repeat-until"))
            {
                finBloqueCodigo.Operador.Value = "end-" + finBloqueCodigo.Operador.Value;

                if (inicioBloqueCodigo.resultado.Value == null || inicioBloqueCodigo.resultado.Value == string.Empty)
                {
                    inicioBloqueCodigo.resultado.Value = finBloqueCodigo.Id.ToString();
                    finBloqueCodigo.resultado.Value = inicioBloqueCodigo.Id.ToString();
                }

            }
            return finBloqueCodigo;
        }


        private Resultado dameResultado(string resulName)
        {
            Resultado resultado = null;

            Cuadruplo cuadruploAux = this.dameCuadruploPorId(resulName);
            if (cuadruploAux != null)
                resultado = cuadruploAux.resultado;

            return resultado;
        }


        private Cuadruplo dameCuadruploPorIndice(int indice)
        {
            Cuadruplo cuadruploAux = null;

            if (indice >= 0 && indice < this.cuadruplos.Count)
                cuadruploAux = this.cuadruplos[indice];

            return cuadruploAux;
        }


        private Cuadruplo dameCuadruploPorId(string id)
        {
            Cuadruplo cuadruploAux = null;

            if (id.Trim() != string.Empty)
                cuadruploAux = this.cuadruplos.FirstOrDefault(c => c.Id.ToString() == id);

            return cuadruploAux;
        }


        private Cuadruplo generaCuadruplo(NodoArblAS nodo, Cuadruplo cuadruploGeneroIzq, Cuadruplo cuadruploGeneroDer)
        {
            Cuadruplo aux = null;

            DslToken dslToken = nodo.getToken();
            if (dslToken.TokenType != TokenType.KeyWord)
            {
                aux = new Cuadruplo(operador: nodo.getToken().Clone(),
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

            aux = new Cuadruplo(new Resultado("GoTo", new DslToken(TokenType.IdTemporal)), operador: nodo.getToken(),
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

        /// <summary>
        /// Ejecuta los cuadruplos dependiendo el tipo de ejecución.
        /// </summary>
        /// <param name="keyExecute"></param>
        public void ejecuta(int keyExecute, int siguiente = -1)
        {
            if (keyExecute == 0)
                this.allExecute();
            if (keyExecute == 1 && siguiente >= 0)
                this.executeLine(siguiente);
            if (keyExecute == 2 && siguiente >= 0)
                this.executeCuadruplo(siguiente);
        }



        public void executeCuadruplo(int cuadruploIndex)
        {
            if (cuadruploIndex < this.cuadruplos.Count)
            {
                Cuadruplo c = this.cuadruplos[cuadruploIndex];
                this.ejecutaCuadruplo(c);
            }
        }



        public void executeLine(int lineNumber)
        {
            List<Cuadruplo> listCuadruplo = this.cuadruplos.Where(x => x.Linea == lineNumber).ToList();
            listCuadruplo.ForEach(x => this.ejecutaCuadruplo(x));
        }



        public void allExecute()
        {
            foreach (Cuadruplo cuadruplo in this.cuadruplos)
            {
                this.ejecutaCuadruplo(cuadruplo);
            }
        }



        private void ejecutaCuadruplo(Cuadruplo c)
        {
            switch (c.Operador.TokenType)
            {
                case TokenType.KeyWord:
                    if (c.Operador.Value == "write")
                    {
                        this.executeWrite(c);
                    }
                    if (c.Operador.Value == "read")
                    {
                        this.executeRead(c);
                    }
                    if (c.Operador.Value == "if")
                    {
                        // this.executeIf(c);
                    }
                    if (c.Operador.Value == "repeat-until")
                    {
                        // this.executeRepeatUntil(c);
                    }
                    break;
                case TokenType.SeparadorComa:
                    this.executeSeparadorComa(c);
                    break;
                case TokenType.OperadorAssign://5:
                    this.executeOperadorAssign(c);
                    break;
                case TokenType.OperadorComp:
                    this.executeOperadorComp(c);
                    break;
                case TokenType.OperadorSuma://7 
                    this.executeOperadorSuma(c);
                    break;
                case TokenType.OperadorMult://8:
                    this.executeOperadorMult(c);
                    break;
                case TokenType.OperadorPote://9:
                    this.executeOperadorPote(c);
                    break;
                case TokenType.FinInstruccion://14:
                    break;
            }
        }

        private void executeSeparadorComa(Cuadruplo c)
        {
            string cadToWrite = string.Empty;

            if (c.Operando1 != null)
                cadToWrite += this.getOperandoValue(c.Operando1);

            if (c.Operando2 != null)
                cadToWrite += (", " + this.getOperandoValue(c.Operando2));

            c.resultado.Value = cadToWrite;
        }

        private void executeWrite(Cuadruplo c)
        {
            string cadToWrite = string.Empty;

            if (c.Operando1 != null)
                cadToWrite += this.getOperandoValue(c.Operando1);

            if (c.Operando2 != null)
                cadToWrite += this.getOperandoValue(c.Operando2);

            if (this.terminal != null)
                this.terminal.print(cadToWrite);
        }



        private void executeRead(Cuadruplo c)
        {
            ReadDatoDlg readDlg = new ReadDatoDlg();

            if (readDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int readValue = readDlg.ReadValue;

                if (c.Operando1 != null)
                {
                    this.setValueOperando(c.Operando1, readValue.ToString());
                    this.terminal.print("read: " + c.Operando1.Value + "\r\nvalue: " + readValue.ToString());
                }
                else if (c.Operando2 != null)
                {
                    this.setValueOperando(c.Operando2, readValue.ToString());
                    this.terminal.print("read: " + c.Operando2.Value + "\r\nvalue: " + readValue.ToString());
                }
            }
        }



        public void executeOperadorAssign(Cuadruplo c)
        {
            string op2 = "";
            op2 = this.getOperandoValue(c.Operando2);
            this.setValueOperando(c.Operando1, op2);
        }



        public void executeOperadorComp(Cuadruplo c)
        {
            string op1 = "", op2 = "";
            op1 = this.getOperandoValue(c.Operando1);
            op2 = this.getOperandoValue(c.Operando2);
            if (c.Operador.Value == ">")
                c.resultado.Value = (int.Parse(op1) > int.Parse(op2)) ? "True" : "False";
            if (c.Operador.Value == "<")
                c.resultado.Value = (int.Parse(op1) < int.Parse(op2)) ? "True" : "False";
            if (c.Operador.Value == "==")
                c.resultado.Value = (int.Parse(op1) == int.Parse(op2)) ? "True" : "False";
        }



        public void executeOperadorSuma(Cuadruplo c)
        {
            string op1 = "", op2 = "";
            op1 = this.getOperandoValue(c.Operando1);
            op2 = this.getOperandoValue(c.Operando2);
            if (c.Operador.Value == "+")
                c.resultado.Value = (int.Parse(op1) + int.Parse(op2)).ToString();
            else
                c.resultado.Value = (int.Parse(op1) - int.Parse(op2)).ToString();
        }



        public void executeOperadorMult(Cuadruplo c)
        {
            string op1 = "", op2 = "";
            op1 = this.getOperandoValue(c.Operando1);
            op2 = this.getOperandoValue(c.Operando2);
            if (c.Operador.Value == "*")
                c.resultado.Value = (int.Parse(op1) * int.Parse(op2)).ToString();
            else
                c.resultado.Value = (int.Parse(op1) / int.Parse(op2)).ToString();
        }



        public void executeOperadorPote(Cuadruplo c)
        {
            string op1 = "", op2 = "";
            op1 = this.getOperandoValue(c.Operando1);
            op2 = this.getOperandoValue(c.Operando2);
            c.resultado.Value = (Math.Pow(int.Parse(op1), int.Parse(op2))).ToString();
        }



        public string getOperandoValue(DslToken operando)
        {
            string value = "";
            if (operando != null)
            {
                //Temporal !! identificadores
                //Pendiente si el operando es un temporal
                if (operando.TokenType == TokenType.Id)
                {
                    value = operando.Value;
                    MetaSimbolo simbolo = TablaSimbolos.TS.getMetaSimbolo(value);
                    value = simbolo.Valor;
                }

                if (operando.TokenType == TokenType.IdTemporal)
                {
                    value = operando.Value;
                    Resultado rAux = this.dameResultado(value);
                    value = rAux.Value;
                }

                if (operando.TokenType == TokenType.Cadena || operando.TokenType == TokenType.Numero)
                {
                    value = operando.Value;
                }

            }

            return value;
        }



        public void setValueOperando(DslToken operando, string value)
        {
            if (operando.TokenType == TokenType.IdTemporal)
            {
                Resultado rAux = this.dameResultado(value);
                rAux.Value = value;
            }
            if (operando.TokenType == TokenType.Id)
            {
                MetaSimbolo simbolo = TablaSimbolos.TS.getMetaSimbolo(operando.Value);
                simbolo.valor = value;
            }
        }



    }
}
