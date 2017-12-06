using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    /// <summary>
    /// Esquema de traducción para la gramatica Tiny.
    /// </summary>
    class EsquemaDeTraduccion
    {

        public static string dameProductor(int numReduccion, out int numTokenReducir)
        {
            string valueToReturn = string.Empty;
            numTokenReducir = 0;

            switch (numReduccion)
            {
                case 1://Programa-><secuencia-sent>
                    numTokenReducir = 1;
                    valueToReturn = "Programa";
                    break;
                case 2://secuencia-sent-><secuencia-sent>;<sentencia>
                    numTokenReducir = 3;
                    valueToReturn = "secuencia-sent";
                    break;
                case 3://secuencia-sent-><sentencia>
                    numTokenReducir = 1;
                    valueToReturn = "secuencia-sent";
                    break;
                case 4://sentencia-><sent-if>
                case 5://sentencia-><sent-repeat>
                case 6://sentencia-><sent-assign>
                case 7://sentencia-><sent-read>
                case 8://sentencia-><sent-write>
                case 9://sentencia-><sent-declara>
                    numTokenReducir = 1;
                    valueToReturn = "sentencia";
                    break;
                case 10://sent-if->if(<exp>){<secuencia-sent>}endif
                    numTokenReducir = 13;
                    valueToReturn = "sent-if";
                    break;
                case 11://sent-if->if(<exp>){<secuencia-sent>}else{<secuencia-sent>}endif
                    numTokenReducir = 20;
                    valueToReturn = "sent-if";
                    break;
                case 12://sent-repeat->repeat{<secuencia-sent>}until(<exp>)
                    numTokenReducir = 17;
                    valueToReturn = "sent-repeat";
                    break;
                case 13://sent-assign-><id>:=<exp>
                    numTokenReducir = 4;
                    valueToReturn = "sent-assign";
                    break;
                case 14://sent-read->read(<id>)
                    numTokenReducir = 7;
                    valueToReturn = "sent-read";
                    break;
                case 15://sent-write->write(<print>)
                    numTokenReducir = 8;
                    valueToReturn = "sent-write";
                    break;
                case 16://print->"<cadena>",<identificadores>
                    numTokenReducir = 5;
                    valueToReturn = "print";
                    break;
                case 17://print->"<cadena>"
                    //string cad17a = Pilas.Stacks.popPS();
                    //string cad17b = "\"" + cad17a + "\"";
                    //NodoArblAS nodo17a = new NodoArblAS(new DslToken(TokenType.KeyWord));
                    //Pilas.Stacks.pushPS(());
                    numTokenReducir = 3;
                    valueToReturn = "print";
                    break;
                case 18://print-><identificadores>
                    NodoArblAS nodo18a = new NodoArblAS(new DslToken(TokenType.KeyWord, "write"));
                    //Pilas.Stacks.pushPAA(nodo18a);
                    numTokenReducir = 1;
                    valueToReturn = "print";
                    break;
                case 19://cadena-><letra><cadena>
                    string cad19a = Pilas.Stacks.popPI();
                    string cad19b = Pilas.Stacks.popPS();
                    Pilas.Stacks.pushPS((cad19a + cad19b));
                    numTokenReducir = 2;
                    valueToReturn = "cadena";
                    break;
                case 20://cadena-><otro><cadena>
                    string cad20a = Pilas.Stacks.popPS();
                    string cad20b = Pilas.Stacks.popPS();
                    Pilas.Stacks.pushPS((cad20b + cad20a));
                    numTokenReducir = 2;
                    valueToReturn = "cadena";
                    break;
                case 21://cadena-><letra>
                case 22://cadena-><otro>
                    numTokenReducir = 1;
                    valueToReturn = "cadena";
                    break;
                case 23://otro->:
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@":");
                    valueToReturn = "otro";
                    break;
                case 24://otro->;
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@";");
                    valueToReturn = "otro";
                    break;
                case 25://otro->,
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@",");
                    valueToReturn = "otro";
                    break;
                case 26://otro->.
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@".");
                    valueToReturn = "otro";
                    break;
                case 27://otro->[
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"[");
                    valueToReturn = "otro";
                    break;
                case 28://otro->]
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"]");
                    valueToReturn = "otro";
                    break;
                case 29://otro->*
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"*");
                    valueToReturn = "otro";
                    break;
                case 30://otro->+
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"+");
                    valueToReturn = "otro";
                    break;
                case 31://otro->¿
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"¿");
                    valueToReturn = "otro";
                    break;
                case 32://otro->?
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"?");
                    valueToReturn = "otro";
                    break;
                case 33://otro->¡
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"¡");
                    valueToReturn = "otro";
                    break;
                case 34://otro->!
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"!");
                    valueToReturn = "otro";
                    break;
                case 35://otro->#
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"#");
                    valueToReturn = "otro";
                    break;
                case 36://otro->%
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"%");
                    valueToReturn = "otro";
                    break;
                case 37://otro->&
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"&");
                    valueToReturn = "otro";
                    break;
                case 38://otro->/
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"/");
                    valueToReturn = "otro";
                    break;
                case 39://otro->\e
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPS(@"\e");
                    valueToReturn = "otro";
                    break;
                case 40://sent-declara->Var{<Tipo><identificadores>}
                    Pilas.Stacks.TipoDato = "nodeclara";
                    numTokenReducir = 7;
                    valueToReturn = "sent-declara";
                    break;
                case 41://identificadores-><identificadores>,<id>
                    numTokenReducir = 3;
                    valueToReturn = "identificadores";
                    break;
                case 42://identificadores-><id>
                    if(Pilas.Stacks.TipoDato != "nodeclara")
                    {
                        string id = Pilas.Stacks.popPI();
                    }
                    else
                    {

                    }

                    numTokenReducir = 1;
                    valueToReturn = "identificadores";
                    break;
                case 43://int
                    Pilas.Stacks.TipoDato = "int";
                    numTokenReducir = 3;
                    valueToReturn = "Tipo";
                    break;
                case 44://float
                    Pilas.Stacks.TipoDato = "float";
                    numTokenReducir = 5;
                    valueToReturn = "Tipo";
                    break;
                case 45://exp-><exp-simple><op-comparacion><exp-simple>
                    NodoArblAS nodo45a = Pilas.Stacks.popPAA();
                    NodoArblAS nodo45b = Pilas.Stacks.popPAA();
                    NodoArblAS nodo45c = new NodoArblAS(new DslToken(TokenType.OperadorComp, getOperadorComparacion()));
                    nodo45c.setNodoIzquierdo(nodo45b);
                    nodo45c.setNodoDerecho(nodo45a);
                    Pilas.Stacks.pushPAA(nodo45c);
                    numTokenReducir = 3;
                    valueToReturn = "exp";
                    break;
                case 46://exp-><exp-simple>
                    numTokenReducir = 1;
                    valueToReturn = "exp";
                    break;
                case 47://==
                case 48://\>
                case 49://\<
                    numTokenReducir = 2;
                    valueToReturn = "op-comparacion";
                    break;
                case 50://exp-simple-><exp-simple><opsuma><term>
                    NodoArblAS nodo50a = Pilas.Stacks.popPAA();
                    NodoArblAS nodo50b = Pilas.Stacks.popPAA();
                    NodoArblAS nodo50c = new NodoArblAS(new DslToken(TokenType.OperadorSuma, getOperador()));
                    nodo50c.setNodoIzquierdo(nodo50b);
                    nodo50c.setNodoDerecho(nodo50a);
                    Pilas.Stacks.pushPAA(nodo50c);
                    numTokenReducir = 3;
                    valueToReturn = "exp-simple";
                    break;
                case 51://exp-simple-><term>
                    numTokenReducir = 1;
                    valueToReturn = "exp-simple";
                    break;
                case 52://+
                    Pilas.Stacks.Operador = 1;
                    numTokenReducir = 1;
                    valueToReturn = "opsuma";
                    break;
                case 53://-
                    Pilas.Stacks.Operador = 2;
                    numTokenReducir = 1;
                    valueToReturn = "opsuma";
                    break;
                case 54://term-><term><opmult><potencia>
                    NodoArblAS nodo54a = Pilas.Stacks.popPAA();
                    NodoArblAS nodo54b = Pilas.Stacks.popPAA();
                    NodoArblAS nodo54c = new NodoArblAS(new DslToken(TokenType.OperadorMult, getOperador()));
                    nodo54c.setNodoIzquierdo(nodo54b);
                    nodo54c.setNodoDerecho(nodo54a);
                    Pilas.Stacks.pushPAA(nodo54c);
                    numTokenReducir = 3;
                    valueToReturn = "term";
                    break;
                case 55://term-><potencia>
                    numTokenReducir = 1;
                    valueToReturn = "term";
                    break;
                case 56://potencia-><potencia>^<factor>
                    NodoArblAS fact = Pilas.Stacks.popPAA();
                    NodoArblAS poten = Pilas.Stacks.popPAA();
                    NodoArblAS nodo = new NodoArblAS(new DslToken(TokenType.OperadorPote, "^"));
                    nodo.setNodoIzquierdo(poten);
                    nodo.setNodoDerecho(fact);
                    Pilas.Stacks.pushPAA(nodo);
                    numTokenReducir = 3;
                    valueToReturn = "potencia";
                    break;
                case 57://potencia-><factor>
                    numTokenReducir = 1;
                    valueToReturn = "potencia";
                    break;
                case 58://*
                    Pilas.Stacks.Operador = 3;
                    numTokenReducir = 1;
                    valueToReturn = "opmult";
                    break;
                case 59:///
                    Pilas.Stacks.Operador = 4;
                    numTokenReducir = 1;
                    valueToReturn = "opmult";
                    break;
                // ##### ================================== Generación de factor
                case 60://factor->(<exp>)
                    numTokenReducir = 3;
                    valueToReturn = "factor";
                    break;
                case 61://factor-><num>
                    string id61a = Pilas.Stacks.popPV().ToString();
                    NodoArblAS nodo61a = new NodoArblAS(new DslToken(TokenType.Numero, id61a));
                    Pilas.Stacks.pushPAA(nodo61a);
                    numTokenReducir = 1;
                    valueToReturn = "factor";
                    break;
                case 62://factor-><id>
                    string id62a = Pilas.Stacks.popPI();
                    NodoArblAS nodo62a = new NodoArblAS(new DslToken(TokenType.Id, id62a));
                    Pilas.Stacks.pushPAA(nodo62a);
                    numTokenReducir = 1;
                    valueToReturn = "factor";
                    break;
                // ##### ================================== Generación de identificadores
                case 63://id->_<id1>
                    string id63a = Pilas.Stacks.popPI();
                    id63a = "_" + id63a;
                    Pilas.Stacks.pushPI(id63a);
                    numTokenReducir = 2;
                    valueToReturn = "id";
                    break;
                case 64://id1-><id1><letra>
                    string idStr1 = Pilas.Stacks.popPI();
                    string idStr2 = Pilas.Stacks.popPI();
                    Pilas.Stacks.pushPI((idStr1 + idStr2));
                    numTokenReducir = 2;
                    valueToReturn = "id1";
                    break;
                case 65://id1-><id1><digito>
                    string id1 = Pilas.Stacks.popPI();
                    id1 += (Pilas.Stacks.popPV().ToString());
                    Pilas.Stacks.pushPI(id1);
                    numTokenReducir = 2;
                    valueToReturn = "id1";
                    break;
                case 66://id1-><letra>
                    numTokenReducir = 1;
                    valueToReturn = "id1";
                    break;
                // ##### ================================== Caracteres de a-z A-Z _
                case 67://a
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("a");
                    break;
                case 68://b
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("b");
                    break;
                case 69://c
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("c");
                    break;
                case 70://d
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("d");
                    break;
                case 71://e
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("e");
                    break;
                case 72://f
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("f");
                    break;
                case 73://g
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("g");
                    break;
                case 74://h
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("h");
                    break;
                case 75://j
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("j");
                    break;
                case 76://k
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("k");
                    break;
                case 77://l
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("l");
                    break;
                case 78://m
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("m");
                    break;
                case 79://n
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("n");
                    break;
                case 80://o
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("o");
                    break;
                case 81://p
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("p");
                    break;
                case 82://q
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("q");
                    break;
                case 83://r
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("r");
                    break;
                case 84://s
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("s");
                    break;
                case 85://t
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("t");
                    break;
                case 86://u
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("u");
                    break;
                case 87://v
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("v");
                    break;
                case 88://w
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("w");
                    break;
                case 89://x
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("x");
                    break;
                case 90://y
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("y");
                    break;
                case 91://z
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("z");
                    break;
                case 92://A
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("A");
                    break;
                case 93://B
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("B");
                    break;
                case 94://C
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("C");
                    break;
                case 95://D
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("D");
                    break;
                case 96://E
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("E");
                    break;
                case 97://F
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("F");
                    break;
                case 98://G
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("G");
                    break;
                case 99://H
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("H");
                    break;
                case 100://I
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("I");
                    break;
                case 101://J
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("J");
                    break;
                case 102://K
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("K");
                    break;
                case 103://L
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("L");
                    break;
                case 104://M
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("M");
                    break;
                case 105://N
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("N");
                    break;
                case 106://O
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("O");
                    break;
                case 107://P
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("P");
                    break;
                case 108://Q
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("Q");
                    break;
                case 109://R
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("R");
                    break;
                case 110://S
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("S");
                    break;
                case 111://T
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("T");
                    break;
                case 112://U
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("U");
                    break;
                case 113://V
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("V");
                    break;
                case 114://W
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("W");
                    break;
                case 115://X
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("X");
                    break;
                case 116://Y
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("Y");
                    break;
                case 117://Z
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("Z");
                    break;
                case 118://_
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    Pilas.Stacks.pushPI("_");
                    break;
                // ##### ================================== De Digito a Número
                case 119://num-><num><digito>
                    int numA = Pilas.Stacks.popPV();
                    int numB = Pilas.Stacks.popPV();
                    int numC = (numA * 10) + numB;
                    Pilas.Stacks.pushPV(numC.ToString());
                    numTokenReducir = 2;
                    valueToReturn = "num";
                    break;
                case 120://num-><digito>
                    numTokenReducir = 1;
                    valueToReturn = "num";
                    break;
                // ##### ================================== Digitos del 0 al 9
                case 121://0 
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("0");
                    break;
                case 122://1
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("1");
                    break;
                case 123://2
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("2");
                    break;
                case 124://3
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("3");
                    break;
                case 125://4
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("4");
                    break;
                case 126://5
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("5");
                    break;
                case 127://6
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("6");
                    break;
                case 128://7
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("7");
                    break;
                case 129://8
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("8");
                    break;
                case 130://9
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    Pilas.Stacks.pushPV("9");
                    break;
            }

            return valueToReturn;
        }


        private static string getOperador()
        {
            string valueToReturn = string.Empty;

            switch (Pilas.Stacks.Operador)
            {
                case 1:
                    valueToReturn = "+";
                    break;
                case 2:
                    valueToReturn = "-";
                    break;
                case 3:
                    valueToReturn = "*";
                    break;
                case 4:
                    valueToReturn = "/";
                    break;
            }

            return valueToReturn;
        }


        private static string getOperadorComparacion()
        {
            string valueToReturn = string.Empty;

            switch (Pilas.Stacks.Comparador)
            {
                case 5:
                    valueToReturn = "==";
                    break;
                case 6:
                    valueToReturn = ">";
                    break;
                case 7:
                    valueToReturn = "<";
                    break;
            }

            return valueToReturn;
        }

    }
}
