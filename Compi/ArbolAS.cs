using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class ArbolAS
    {
        List<List<DslToken>> tokens;
        List<DslSentence> sentences;
        List<NodoArblAS> raices;
        Stack<NodoArblAS> pilaNodos;
        Stack<NodoArblAS> pilaNodosAux;

        List<TokenType> tokensIgnorar;
        List<TokenType> tokensImportantes;
        List<TokenType> tokensNormales;

        int cantAbreParent, cantAbreLlaves;


        public ArbolAS(List<List<DslToken>> theTokens, List<DslSentence> theSentences)
        {
            this.tokens = theTokens;
            this.sentences = theSentences;

            this.raices = new List<NodoArblAS>();
            this.pilaNodos = new Stack<NodoArblAS>();

            this.llenaTokensIgnorables();
            this.llenaTokensNormales();
            this.llenaToknesImportantes();
        }


        private void llenaTokensIgnorables()
        {
            this.tokensIgnorar = new List<TokenType>();

            this.tokensIgnorar.Add(TokenType.AbreLlaves);
            this.tokensIgnorar.Add(TokenType.AbreParent);
            this.tokensIgnorar.Add(TokenType.CierraParent);
            this.tokensIgnorar.Add(TokenType.CierraLlaves);
            this.tokensIgnorar.Add(TokenType.FinInstruccion);
        }


        private void llenaTokensNormales()
        {
            this.tokensNormales = new List<TokenType>();

            this.tokensNormales.Add(TokenType.Id);
            this.tokensNormales.Add(TokenType.Cadena);
            this.tokensNormales.Add(TokenType.Numero);
        }


        private void llenaToknesImportantes()
        {
            this.tokensImportantes = new List<TokenType>();

            this.tokensImportantes.Add(TokenType.KeyWord);
            this.tokensImportantes.Add(TokenType.OperadorAssign);
            this.tokensImportantes.Add(TokenType.OperadorComp);
            this.tokensImportantes.Add(TokenType.OperadorSuma);
            this.tokensImportantes.Add(TokenType.OperadorMult);
            this.tokensImportantes.Add(TokenType.OperadorPote);
            this.tokensImportantes.Add(TokenType.SeparadorComa);
        }


        public void generaRamas()
        {
            NodoArblAS nodoAux;
            this.pilaNodosAux = new Stack<NodoArblAS>();
            //bool finDeLinea = false;

            for (int index = 0; index < this.sentences.Count; index++)
            {
                if (this.sentences[index].sentenceType != SentenceType.SentenciaDeclara)
                {
                    this.tokens[index].ForEach(token =>
                    {
                        //if (token.TokenType != TokenType.FinInstruccion)
                        //{
                        nodoAux = this.creaNodo(token);
                        if (nodoAux != null)
                            agregaNodoPila(nodoAux);
                        //}
                        //else
                        //{
                        //    finDeLinea = true;
                        //    nodoAux = this.reduceNodosFinDeLinea();
                        //    if (nodoAux != null)
                        //        this.raices.Add(nodoAux);
                        //}
                    });

                    //if (!finDeLinea)
                    //{
                    //    nodoAux = this.reduceNodosFinDeLinea();
                    //    if (nodoAux != null)
                    //        this.raices.Add(nodoAux);
                    //}
                    //else
                    //    finDeLinea = false;
                }
            }
        }



        public NodoArblAS creaNodo(DslToken dslToken)
        {
            return new NodoArblAS(dslToken);
        }



        public void agregaNodoPila(NodoArblAS nuevoNodo)
        {
            DslToken tokenAux = nuevoNodo.getToken();
            NodoArblAS ultimoNodoA;

            if (this.pilaNodos.Count == 0)
            {
                this.pilaNodos.Push(nuevoNodo);
                return;
            }
            else if (nuevoNodo.getToken().TokenType == TokenType.AbreParent ||
                nuevoNodo.getToken().TokenType == TokenType.AbreLlaves)
            {
                this.pilaNodos.Push(nuevoNodo);
                return;
            }
            else if (nuevoNodo.getToken().TokenType == TokenType.CierraParent)
            {
                this.reduceNodosParentesis();
            }
            else if (nuevoNodo.getToken().TokenType == TokenType.CierraLlaves)
            {
                this.reduceNodosLlaves();
            }
            else
            {
                ultimoNodoA = this.pilaNodos.Peek();
                if (nuevoNodo.tieneMayorPrioridad(ultimoNodoA) == 1)
                {
                    if (nuevoNodo.setNodo(ultimoNodoA))
                    {
                        this.pilaNodos.Pop();
                        this.pilaNodos.Push(nuevoNodo);
                    }
                }
                else
                {
                    this.pilaNodos.Push(nuevoNodo);
                }
            }
        }



        public void cuentaLLavesParentesis(NodoArblAS nuevoNodo)
        {
            TokenType theTokenType = nuevoNodo.getToken().TokenType;

            if (theTokenType == TokenType.AbreLlaves)
                this.cantAbreLlaves++;

            if (theTokenType == TokenType.AbreParent)
                this.cantAbreParent++;
        }



        public NodoArblAS reordenaPila(NodoArblAS nuevoNodoImportante)
        {
            NodoArblAS ultimoNodo;

            ultimoNodo = this.pilaNodos.Peek();
            while (ultimoNodo.tieneMayorPrioridad(nuevoNodoImportante) == -1)
            {
                ultimoNodo = this.pilaNodos.Pop();
                nuevoNodoImportante.setNodo(ultimoNodo);
                ultimoNodo = this.pilaNodos.Peek();
            }

            return nuevoNodoImportante;
        }



        public NodoArblAS reduceNodosParentesis()
        {
            NodoArblAS ultimoNodo = this.pilaNodos.Peek();
            NodoArblAS otroNodo = null;

            while (ultimoNodo.getToken().TokenType != TokenType.AbreParent)
            {
                ultimoNodo = this.pilaNodos.Pop();
                otroNodo = this.pilaNodos.Peek();

                if (otroNodo.getToken().TokenType == TokenType.AbreParent)
                {
                    this.pilaNodos.Pop();
                    this.pilaNodos.Push(ultimoNodo);
                    break;
                }

                if (ultimoNodo.tieneMayorPrioridad(otroNodo) == 1)
                {
                    if (ultimoNodo.setNodo(otroNodo))
                    {
                        this.pilaNodos.Pop();
                        this.pilaNodos.Push(ultimoNodo);
                    }
                }
                else
                {
                    otroNodo.setNodo(ultimoNodo);
                }

                ultimoNodo = this.pilaNodos.Peek();
            }


            return ultimoNodo;
        }



        public NodoArblAS reduceNodosLlaves()
        {
            NodoArblAS ultimoNodo = this.pilaNodos.Peek();
            NodoArblAS otroNodo = null;

            while (ultimoNodo.getToken().TokenType != TokenType.AbreLlaves)
            {
                ultimoNodo = this.pilaNodos.Pop();
                otroNodo = this.pilaNodos.Peek();

                if (otroNodo.getToken().TokenType == TokenType.AbreLlaves)
                {
                    this.pilaNodos.Pop();
                    this.pilaNodos.Push(ultimoNodo);
                    break;
                }

                if (ultimoNodo.tieneMayorPrioridad(otroNodo) == 1)
                {
                    if (ultimoNodo.setNodo(otroNodo))
                    {
                        this.pilaNodos.Pop();
                        this.pilaNodos.Push(ultimoNodo);
                    }
                }
                else
                {
                    otroNodo.setNodo(ultimoNodo);
                }

                ultimoNodo = this.pilaNodos.Peek();
            }


            return ultimoNodo;
        }


        public NodoArblAS reduceNodosFinDeLinea()
        {
            Stack<NodoArblAS> bufferAux = null;
            NodoArblAS ultimoNodo = null;
            NodoArblAS otroNodo = null;


            if (this.pilaNodos.Count == 1)
                return this.pilaNodos.Pop();

            bufferAux = new Stack<NodoArblAS>();
            ultimoNodo = this.pilaNodos.Peek();

            if (ultimoNodo.getToken().TokenType == TokenType.AbreLlaves ||
                ultimoNodo.getToken().TokenType == TokenType.AbreParent)
                return null;

            while (this.pilaNodos.Count > 1 &&
                (ultimoNodo.getToken().TokenType != TokenType.AbreLlaves ||
                ultimoNodo.getToken().TokenType != TokenType.AbreParent))
            {
                ultimoNodo = this.pilaNodos.Pop();
                otroNodo = this.pilaNodos.Peek();

                if (otroNodo.getToken().TokenType == TokenType.AbreLlaves ||
                otroNodo.getToken().TokenType == TokenType.AbreParent)
                {
                    this.pilaNodos.Push(ultimoNodo);
                    return null;
                }

                if (ultimoNodo.tieneMayorPrioridad(otroNodo) == 1)
                {
                    if (ultimoNodo.setNodo(otroNodo))
                    {
                        this.pilaNodos.Pop();
                        this.pilaNodos.Push(ultimoNodo);
                    }
                }
                else if (ultimoNodo.tieneMayorPrioridad(otroNodo) == -1)
                {
                    otroNodo.setNodo(ultimoNodo);
                    while (bufferAux.Count > 0)
                    {
                        if (otroNodo.setNodo(bufferAux.Peek()))
                            bufferAux.Pop();
                        else
                            break;
                    }
                }
                else
                {
                    bufferAux.Push(ultimoNodo);
                }

                ultimoNodo = this.pilaNodos.Peek();
            }


            return this.pilaNodos.Pop();
        }
    }
}
