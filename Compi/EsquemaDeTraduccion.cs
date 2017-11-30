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
                    numTokenReducir = 3;
                    valueToReturn = "print";
                    break;
                case 18://print-><identificadores>
                    numTokenReducir = 1;
                    valueToReturn = "print";
                    break;
                case 19://cadena-><letra><cadena>
                case 20://cadena-><otro><cadena>
                    numTokenReducir = 2;
                    valueToReturn = "cadena";
                    numTokenReducir = 2;
                    break;
                case 21://cadena-><letra>
                case 22://cadena-><otro>
                    numTokenReducir = 1;
                    valueToReturn = "cadena";
                    break;
                case 23://otro->:
                case 24://otro->;
                case 25://otro->,
                case 26://otro->.
                case 27://otro->[
                case 28://otro->]
                case 29://otro->*
                case 30://otro->+
                case 31://otro->¿
                case 32://otro->?
                case 33://otro->¡
                case 34://otro->!
                case 35://otro->#
                case 36://otro->%
                case 37://otro->&
                case 38://otro->/
                case 39://otro->\e
                    numTokenReducir = 1;
                    valueToReturn = "otro";
                    break;
                case 40://sent-declara->Var{<Tipo><identificadores>}
                    numTokenReducir = 7;
                    valueToReturn = "sent-declara";
                    break;
                case 41://identificadores-><identificadores>,<id>
                    numTokenReducir = 3;
                    valueToReturn = "identificadores";
                    break;
                case 42://identificadores-><id>
                    numTokenReducir = 1;
                    valueToReturn = "identificadores";
                    break;
                case 43://int
                    numTokenReducir = 3;
                    valueToReturn = "Tipo";
                    break;
                case 44://float
                    numTokenReducir = 5;
                    valueToReturn = "Tipo";
                    break;
                case 45://exp-><exp-simple><op-comparacion><exp-simple>
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
                    numTokenReducir = 3;
                    valueToReturn = "exp-simple";
                    break;
                case 51://exp-simple-><term>
                    numTokenReducir = 1;
                    valueToReturn = "exp-simple";
                    break;
                case 52://+
                case 53://-
                    numTokenReducir = 1;
                    valueToReturn = "opsuma";
                    break;
                case 54://term-><term><opmult><potencia>
                    numTokenReducir = 3;
                    valueToReturn = "term";
                    break;
                case 55://term-><potencia>
                    numTokenReducir = 1;
                    valueToReturn = "term";
                    break;
                case 56://potencia-><potencia>^<factor>
                    numTokenReducir = 3;
                    valueToReturn = "potencia";
                    break;
                case 57://potencia-><factor>
                    numTokenReducir = 1;
                    valueToReturn = "potencia";
                    break;
                case 58://*
                case 59:///
                    numTokenReducir = 1;
                    valueToReturn = "opmult";
                    break;
                case 60://factor->(<exp>)
                    numTokenReducir = 3;
                    valueToReturn = "factor";
                    break;
                case 61://factor-><num>
                case 62://factor-><id>
                    numTokenReducir = 1;
                    valueToReturn = "factor";
                    break;
                case 63://id->_<id1>
                    numTokenReducir = 2;
                    valueToReturn = "id";
                    break;
                case 64://id1-><id1><letra>
                case 65://id1-><id1><digito>
                    numTokenReducir = 2;
                    valueToReturn = "id1";
                    break;
                case 66://id1-><letra>
                    numTokenReducir = 1;
                    valueToReturn = "id1";
                    break;
                case 67://a
                case 68://b
                case 69://c
                case 70://d
                case 71://e
                case 72://f
                case 73://g
                case 74://h
                case 75://j
                case 76://k
                case 77://l
                case 78://m
                case 79://n
                case 80://o
                case 81://p
                case 82://q
                case 83://r
                case 84://s
                case 85://t
                case 86://u
                case 87://v
                case 88://w
                case 89://x
                case 90://y
                case 91://z
                case 92://A
                case 93://B
                case 94://C
                case 95://D
                case 96://E
                case 97://F
                case 98://G
                case 99://H
                case 100://I
                case 101://J
                case 102://K
                case 103://L
                case 104://M
                case 105://N
                case 106://O
                case 107://P
                case 108://Q
                case 109://R
                case 110://S
                case 111://T
                case 112://U
                case 113://V
                case 114://W
                case 115://X
                case 116://Y
                case 117://Z
                case 118://_
                    numTokenReducir = 1;
                    valueToReturn = "letra";
                    break;
                case 119://num-><num><digito>
                    numTokenReducir = 2;
                    valueToReturn = "num";
                    break;
                case 120://num-><digito>
                    numTokenReducir = 1;
                    valueToReturn = "num";
                    break;
                case 121://0 
                case 122://1
                case 123://2
                case 124://3
                case 125://4
                case 126://5
                case 127://6
                case 128://7
                case 129://8
                case 130://9
                    numTokenReducir = 1;
                    valueToReturn = "digito";
                    break;
            }

            return valueToReturn;
        }


    }
}
