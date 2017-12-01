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
            this.tokensImportantes.Add(TokenType.OperadorComp);
            this.tokensImportantes.Add(TokenType.OperadorSuma);
            this.tokensImportantes.Add(TokenType.OperadorMult);
            this.tokensImportantes.Add(TokenType.OperadorPote);
            this.tokensImportantes.Add(TokenType.SeparadorComa);
        }


        public void generaRamas()
        {
            NodoArblAS nodoAux;

            for (int index = 0; index < this.sentences.Count; index++)
            {
                if (this.sentences[index].sentenceType != SentenceType.SentenciaDeclara)
                {
                    this.tokens[index].ForEach(token =>
                    {
                        nodoAux = this.creaNodo(token);
                        if (nodoAux != null)
                            agregaNodoPila(nodoAux);
                    });
                }
            }
        }



        public NodoArblAS creaNodo(DslToken dslToken)
        {
            return new NodoArblAS(dslToken);
        }



        public void agregaNodoPila(NodoArblAS nodo)
        {
            DslToken tokenAux = nodo.getToken();

            if (this.pilaNodos.Count == 0)
            {
                this.pilaNodos.Push(nodo);
                return;
            }

            if (this.tokensNormales.Contains(tokenAux.TokenType))
            {
                this.pilaNodos.Push(nodo);
            }
            else if (this.tokensIgnorar.Contains(tokenAux.TokenType))
            {
                if (tokenAux.TokenType == TokenType.AbreParent ||
                    tokenAux.TokenType == TokenType.AbreLlaves)
                {
                    this.pilaNodos.Push(nodo);
                    this.cuentaLLavesParentesis(nodo);
                }
                else
                {
                    this.reduceNodos(nodo);
                }
            }
            else if (this.tokensImportantes.Contains(tokenAux.TokenType))
            {
                NodoArblAS nodoAux = this.reordenaPila(nodo);
                if (nodoAux != null)
                    this.pilaNodos.Push(nodoAux);
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
            while (this.tokensNormales.Contains(ultimoNodo.getToken().TokenType))
            {
                ultimoNodo = this.pilaNodos.Pop();
                nuevoNodoImportante.setNodo(ultimoNodo);
                ultimoNodo = this.pilaNodos.Peek();
            }

            return nuevoNodoImportante;
        }



        public void reduceNodos(NodoArblAS nuevoNodo)
        {
            DslToken nuevoToken = nuevoNodo.getToken();

            if (nuevoToken.TokenType == TokenType.CierraLlaves)
                this.reduceNodosLlaves(nuevoNodo);
            else if (nuevoToken.TokenType == TokenType.CierraParent)
                this.reduceNodosParentesis(nuevoNodo);
            else if (nuevoToken.TokenType == TokenType.FinInstruccion)
                this.reduceNodosFinDeLinea(nuevoNodo);

        }



        public NodoArblAS reduceNodosParentesis(NodoArblAS nuevoNodo)
        {

            return null;
        }



        public NodoArblAS reduceNodosLlaves(NodoArblAS nuevoNodo)
        {

            return null;
        }



        public NodoArblAS reduceNodosFinDeLinea(NodoArblAS nuevoNodo)
        {

            return null;
        }
    }
}
