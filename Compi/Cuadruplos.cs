using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    // ##################################################################################
    // Métodos necesarios para generar los cuadruplos
    // ##################################################################################
    class Cuadruplos
    {

        private List<Cuadruplo> cuadruplos;

        public List<List<Cuadruplo>> recorreArboles(List<ArbolAS> arboles)
        {
            List<List<Cuadruplo>> listsArboles = new List<List<Cuadruplo>>();

            if (arboles != null)
                foreach (ArbolAS arbol in arboles)
                {
                    List<Cuadruplo> listaux = null;
                    if ((listaux = recorreArbol(arbol)) != null)
                        listsArboles.Add(listaux);
                }

            return listsArboles;
        }

        public List<Cuadruplo> recorreArbol(ArbolAS raiz)
        {
            cuadruplos = new List<Cuadruplo>();

            if (raiz.getNodoIzquierdo() != null)
                generaCuadruplo(raiz.getNodoIzquierdo());

            if (raiz.getNodoDerecho() != null)
                generaCuadruplo(raiz.getNodoDerecho());

            return cuadruplos;
        }



        private Cuadruplo generaCuadruplo(ArbolAS nodo)
        {
            Cuadruplo cuadruploGeneroIzq = null;
            Cuadruplo cuadruploGeneroDer = null;
            ArbolAS nodoIzquierdo = nodo.getNodoIzquierdo();
            ArbolAS nodoDerecho = nodo.getNodoDerecho();

            if (nodoIzquierdo == null && nodoDerecho == null)
                return null;

            //Navegamos en profundidad primero por la izquierda
            if (nodoIzquierdo != null)
                cuadruploGeneroIzq = generaCuadruplo(nodoIzquierdo);
            //Navegamos en profunidad por la derecha
            if (nodoDerecho != null)
                cuadruploGeneroDer = generaCuadruplo(nodoDerecho);

            // Primer caso, es un operador, y como nodos hijos solo tiene números sin nietos
            if (cuadruploGeneroIzq == null && cuadruploGeneroDer == null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken(),
                                            op1: (nodoIzquierdo != null ? nodoIzquierdo.getToken() : null),
                                            op2: (nodoDerecho != null ? nodoDerecho.getToken() : null));
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null && cuadruploGeneroDer != null)
            {
                //Cuadruplo aux = this.generaCuadruplo(nodo, cuadruploGeneroIzq, cuadruploGeneroDer);
                Cuadruplo aux = null;
                if ((aux = this.generaCuadruplo(nodo, cuadruploGeneroIzq, cuadruploGeneroDer)) != null)
                    cuadruplos.Add(aux);

                return aux;
            }
            else if (cuadruploGeneroIzq == null && nodoIzquierdo != null && cuadruploGeneroDer != null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken(),
                                                op1: nodoIzquierdo.getToken(),
                                                op2: cuadruploGeneroDer.resultado.tokenType);
                cuadruplos.Add(aux);
                return aux;
            }
            else if (cuadruploGeneroIzq != null && cuadruploGeneroDer == null && nodoDerecho != null)
            {
                Cuadruplo aux = new Cuadruplo(operador: nodo.getToken(),
                                                op1: cuadruploGeneroIzq.resultado.tokenType,
                                                op2: nodoDerecho.getToken());
                cuadruplos.Add(aux);
                return aux;
            }

            return null;
        }


        private Cuadruplo generaCuadruplo(ArbolAS nodo, Cuadruplo cuadruploGeneroIzq, Cuadruplo cuadruploGeneroDer)
        {
            Cuadruplo aux = null;

            DslToken dslToken = nodo.getToken();
            if (dslToken.TokenType != TokenType.KeyWord)
            {
                aux = new Cuadruplo(operador: nodo.getToken(),
                                                op1: cuadruploGeneroIzq.resultado.tokenType,
                                                op2: cuadruploGeneroDer.resultado.tokenType);
                cuadruplos.Add(aux);
            }
            else if (dslToken.TokenType == TokenType.KeyWord &&
                (dslToken.Value == "if" || dslToken.Value == "else" || dslToken.Value == "endif"))
            {
                aux = generaCuadruploIf(nodo, cuadruploGeneroIzq, cuadruploGeneroDer);
            }

            return aux;
        }



        private Cuadruplo generaCuadruploIf(ArbolAS nodo, Cuadruplo cuadruploGeneroIzq, Cuadruplo cuadruploGeneroDer)
        {
            Cuadruplo aux = null;

            aux = new Cuadruplo(new Resultado("GoTo", new DslToken(TokenType.Id)), operador: nodo.getToken(),
                                            op1: cuadruploGeneroIzq.resultado.tokenType, op2: (new DslToken(TokenType.Numero, "1")));
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

        private Cuadruplo generaCuadruploElse(ArbolAS nodo, Cuadruplo cuadruploGeneroIzq, Cuadruplo cuadruploGeneroDer)
        {


            return null;
        }
    }
}
