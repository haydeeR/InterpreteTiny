using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsimbolos;
using ManejoDeErrores;

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
                    NodoArblAS nodo2a = Pilas.Stacks.popPAA();
                    NodoArblAS nodo2b = Pilas.Stacks.popPAA();

                    NodoArblAS nodo2c = new NodoArblAS(new DslToken(TokenType.FinInstruccion, ";"));
                    nodo2c.setNodo(nodo2b);
                    nodo2c.setNodo(nodo2a);

                    Pilas.Stacks.pushPAA(nodo2c);

                    numTokenReducir = 3;
                    valueToReturn = "secuencia-sent" + "@" + "nodo1=popPAA(); nodo2=popPAA(); nodo=CreaNodo(\";\", nodo2, nodo1); pushPAA(nodo);";
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
                    NodoArblAS nodoSent10a = Pilas.Stacks.popPAA();
                    NodoArblAS nodoExp10a = Pilas.Stacks.popPAA();

                    NodoArblAS nodoIf10a = new NodoArblAS(new DslToken(TokenType.KeyWord, "if"));
                    nodoIf10a.setNodo(nodoExp10a);
                    nodoIf10a.setNodo(nodoSent10a);
                    Pilas.Stacks.pushPAA(nodoIf10a);

                    numTokenReducir = 13;
                    valueToReturn = "sent-if" + "@" + "NodoSent=poppAA(); nodoExp=popPAA(); nodo=CreaNodo(“if”,NodoExp,NodoSent); pushPAA(nodo);";
                    break;
                case 11://sent-if->if(<exp>){<secuencia-sent>}else{<secuencia-sent>}endif
                    NodoArblAS nodoSentIF11a = Pilas.Stacks.popPAA();//Nodo Else
                    NodoArblAS nodoSentV11a = Pilas.Stacks.popPAA();
                    NodoArblAS nodoExpV11a = Pilas.Stacks.popPAA();



                    NodoArblAS nodoElse11a = new NodoArblAS(new DslToken(TokenType.KeyWord, "else"));
                    nodoElse11a.setNodo(nodoSentV11a);
                    nodoElse11a.setNodo(nodoSentIF11a);

                    NodoArblAS nodoIf11a = new NodoArblAS(new DslToken(TokenType.KeyWord, "if"));
                    nodoIf11a.setNodo(nodoExpV11a);
                    nodoIf11a.setNodo(nodoElse11a);

                    Pilas.Stacks.pushPAA(nodoIf11a);
                    numTokenReducir = 20;
                    valueToReturn = "sent-if" + "@" + "nodoSentF=poppAA(); NodoSentV=popPAA(); NodoExp=popPAA(); nodoElse=CreaNodo(“else”,NodoSentV,NodoSentF); nodo=Creanodo(“if”,nodoExp,nodoElse); pushPAA(nodo);";
                    break;
                case 12://sent-repeat->repeat{<secuencia-sent>}until(<exp>)
                    NodoArblAS nodoExp12a = Pilas.Stacks.popPAA();
                    NodoArblAS nodoSent12b = Pilas.Stacks.popPAA();
                    NodoArblAS nodosentRep12c = new NodoArblAS(new DslToken(TokenType.KeyWord, "repeat-until"));

                    nodosentRep12c.setNodo(nodoSent12b);
                    nodosentRep12c.setNodo(nodoExp12a);

                    Pilas.Stacks.pushPAA(nodosentRep12c);

                    numTokenReducir = 17;
                    valueToReturn = "sent-repeat" + "@" + "NodoExp=popPAA(); NodoSent=popPAA(); nodo=CreaNodo(“repeat-until”,NodoSent,NodoExp); pushPAA(nodo);";
                    break;
                case 13://sent-assign-><id>:=<exp>
                    string id13a = Pilas.Stacks.popPI();
                    if (TablaSimbolos.TS.existeSimbolo(id13a))
                    {
                        NodoArblAS nodo13a = new NodoArblAS(new DslToken(TokenType.Id, id13a));
                        NodoArblAS nodo13b = Pilas.Stacks.popPAA();
                        NodoArblAS nodo13c = new NodoArblAS(new DslToken(TokenType.OperadorAssign, ":="));
                        nodo13c.setNodo(nodo13a);
                        nodo13c.setNodo(nodo13b);
                        Pilas.Stacks.pushPAA(nodo13c);
                    }
                    else
                    {
                        Error e = new Error(Pilas.Stacks.NumeroLinea, "El simbolo " + id13a + " no esta definido", "El símbolo debe de ser declarado previamente.", "Todos los simbolos deben estar previamente definidos");
                        TablaErrores.InstanceTable.agregaError(e);
                    }
                    numTokenReducir = 4;
                    valueToReturn = "sent-assign" + "@" + "id=popPI(); if(ExisteEnTS(id){nodo1=CreaNodo(id,null,null); nodo2=popPAA(); nodo=CreaNodo(\":=\", nodo1, nodo2); pushPAA(nodo);}else{error()}";
                    break;
                case 14://sent-read->read(<id>)
                    string id14a = Pilas.Stacks.popPI();
                    if (TablaSimbolos.TS.existeSimbolo(id14a))
                    {
                        NodoArblAS nodo14a = new NodoArblAS(new DslToken(TokenType.KeyWord, "read"));
                        NodoArblAS nodo14b = new NodoArblAS(new DslToken(TokenType.Id, id14a));
                        nodo14a.setNodo(nodo14b);
                        Pilas.Stacks.pushPAA(nodo14a);
                    }
                    else
                    {
                        Error e = new Error(Pilas.Stacks.NumeroLinea, "El simbolo " + id14a + " no esta definido", "El símbolo debe de ser declarado previamente.", "Todos los simbolos deben estar previamente definidos");
                        TablaErrores.InstanceTable.agregaError(e);
                    }
                    numTokenReducir = 7;
                    valueToReturn = "sent-read" + "@" + "id=popPI(); if(ExisteEnTS(id){nodo=CreateNodo(“read”, id, null); pushPAA(nodo);}else{error()}";
                    break;
                case 15://sent-write->write(<print>)
                    numTokenReducir = 8;
                    valueToReturn = "sent-write";
                    break;
                case 16://print->"<cadena>",<identificadores>
                    string cad16a = Pilas.Stacks.popAllPS();
                    string cad16b = Pilas.Stacks.popAllPI();
                    string cad16c = "\"" + cad16b + cad16a + "\"";

                    NodoArblAS nodo16a = new NodoArblAS(new DslToken(TokenType.KeyWord, "write"));
                    NodoArblAS nodo16b = new NodoArblAS(new DslToken(TokenType.Cadena, cad16c));
                    NodoArblAS nodo16c = Pilas.Stacks.popPAA();

                    nodo16a.setNodo(nodo16b);
                    nodo16a.setNodo(nodo16c);
                    Pilas.Stacks.pushPAA(nodo16a);
                    numTokenReducir = 5;
                    valueToReturn = "print" + "@" + "a1=popPS(); a2=concat(“””,a1,”””); nodo=CreaNodo(“write”,a2,listaID); pushpAA(nodo);";
                    break;
                case 17://print->"<cadena>"
                    string cad17Aux = Pilas.Stacks.popAllPI();
                    string cad17a = Pilas.Stacks.popAllPS();
                    string cad17b = "\"" + cad17Aux + cad17a + "\"";
                    NodoArblAS nodo17a = new NodoArblAS(new DslToken(TokenType.KeyWord, "write"));
                    NodoArblAS nodo17b = new NodoArblAS(new DslToken(TokenType.Cadena, cad17b));
                    nodo17a.setNodoIzquierdo(nodo17b);
                    Pilas.Stacks.pushPAA(nodo17a);
                    numTokenReducir = 3;
                    valueToReturn = "print" + "@" + "a1=popPS(); a2=concat(“””,a1,”””); nodo=CreaNodo(“write”,a2,null); pushpAA(nodo);";
                    break;
                case 18://print-><identificadores>
                    NodoArblAS nodoWrite18a = new NodoArblAS(new DslToken(TokenType.KeyWord, "write"));
                    DslToken newTokenType18a = new DslToken(TokenType.Identificadores,
                                                        Pilas.Stacks.getListaIdsLikeString());
                    NodoArblAS nodoListaIDs18b = new NodoArblAS(newTokenType18a);
                    nodoWrite18a.setNodo(nodoListaIDs18b);
                    Pilas.Stacks.pushPAA(nodoWrite18a);
                    numTokenReducir = 1;
                    valueToReturn = "print" + "@" + "nodo=CreaNodo(“write”,listaID,null); pushpAA(nodo);";
                    break;
                case 19://cadena-><letra><cadena>
                    string cad19a = Pilas.Stacks.popPI();
                    string cad19b = Pilas.Stacks.popPS();
                    Pilas.Stacks.pushPS((cad19a + cad19b));
                    numTokenReducir = 2;
                    valueToReturn = "cadena" + "@" + "cad1=popPI(); cad2=popPS(); cad=concat(cad1, cad2); pushPS(cad);";
                    break;
                case 20://cadena-><otro><cadena>
                    string cad20a = Pilas.Stacks.popPI();
                    string cad20b = Pilas.Stacks.popPS();
                    Pilas.Stacks.pushPS((cad20a + cad20b));
                    numTokenReducir = 2;
                    valueToReturn = "cadena" + "@" + "cad1=popPS(); cad2=popPS(); cad=concat(cad2, cad1); pushPS(cad);";
                    break;
                case 21://cadena-><letra>
                    Pilas.Stacks.pushPS(Pilas.Stacks.popPI());
                    numTokenReducir = 1;
                    valueToReturn = "cadena" + "@" + "pushPS(popPI())";
                    break;
                case 22://cadena-><otro>
                    Pilas.Stacks.pushPS(Pilas.Stacks.popPI());
                    numTokenReducir = 1;
                    valueToReturn = "cadena";
                    break;
                case 23://otro->:
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@":");
                    valueToReturn = "otro" + "@" + "pushPI(\":\")";
                    break;
                case 24://otro->;
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@";");
                    valueToReturn = "otro" + "@" + "pushPI(\";\")";
                    break;
                case 25://otro->,
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@",");
                    valueToReturn = "otro" + "@" + "pushPI(\",\")";
                    break;
                case 26://otro->.
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@".");
                    valueToReturn = "otro" + "@" + "pushPI(\".\")";
                    break;
                case 27://otro->[
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"[");
                    valueToReturn = "otro" + "@" + "pushPI(\"[\")";
                    break;
                case 28://otro->]
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"]");
                    valueToReturn = "otro" + "@" + "pushPI(\"]\")";
                    break;
                case 29://otro->*
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"*");
                    valueToReturn = "otro" + "@" + "pushPI(\"*\")";
                    break;
                case 30://otro->+
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"+");
                    valueToReturn = "otro" + "@" + "pushPI(\"+\")";
                    break;
                case 31://otro->¿
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"¿");
                    valueToReturn = "otro" + "@" + "pushPI(\"¿\")";
                    break;
                case 32://otro->?
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"?");
                    valueToReturn = "otro" + "@" + "pushPI(\"?\")";
                    break;
                case 33://otro->¡
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"¡");
                    valueToReturn = "otro" + "@" + "pushPI(\"¡\")";
                    break;
                case 34://otro->!
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"!");
                    valueToReturn = "otro" + "@" + "pushPI(\"!\")";
                    break;
                case 35://otro->#
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"#");
                    valueToReturn = "otro" + "@" + "pushPI(\"#\")";
                    break;
                case 36://otro->%
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"%");
                    valueToReturn = "otro" + "@" + "pushPI(\"%\")";
                    break;
                case 37://otro->&
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"&");
                    valueToReturn = "otro" + "@" + "pushPI(\"&\")";
                    break;
                case 38://otro->/
                    numTokenReducir = 1;
                    Pilas.Stacks.pushPI(@"/");
                    valueToReturn = "otro" + "@" + "pushPI(\"/\")";
                    break;
                case 39://otro->\e
                    numTokenReducir = 2;
                    Pilas.Stacks.pushPI(@" ");
                    //Pilas.Stacks.pushPS(Pilas.Stacks.popAllPI() + " ");
                    valueToReturn = "otro" + "@" + "pushPI(\" \")";
                    break;
                case 40://sent-declara->Var{<Tipo><identificadores>}
                    Pilas.Stacks.TipoDato = "nodeclara";
                    numTokenReducir = 7;
                    valueToReturn = "sent-declara" + "@" + "TipoDato=\"nodeclara\"";
                    break;
                case 41://identificadores-><identificadores>,<id>
                    //string id41a = Pilas.Stacks.popId();
                    string id41a = Pilas.Stacks.popPI();
                    NodoArblAS nodo41a = Pilas.Stacks.popPAA();

                    if (Pilas.Stacks.TipoDato == "nodeclara")
                    {
                        if (TablaSimbolos.TS.existeSimbolo(id41a))
                        {
                            NodoArblAS nodo41b = new NodoArblAS(new DslToken(TokenType.Id, id41a));
                            NodoArblAS nodo41c = new NodoArblAS(new DslToken(TokenType.SeparadorComa, ","));
                            nodo41c.setNodo(nodo41a);
                            nodo41c.setNodo(nodo41b);
                            Pilas.Stacks.pushPAA(nodo41c);
                        }
                        else
                        {
                            Error e = new Error(Pilas.Stacks.NumeroLinea, "El simbolo " + id41a + " no esta definido", "El símbolo debe de ser declarado previamente.", "Todos los simbolos deben estar previamente definidos");
                            TablaErrores.InstanceTable.agregaError(e);
                        }
                    }

                    numTokenReducir = 3;
                    valueToReturn = "identificadores" + "@" + "If (tipo!=”nodeclara”) { id=popPI(); CrearTS(id,tipo); } " +
                                    "else{ id=popPI(); nodo1=popPAA(); " +
                                        "if(ExisteTS(id)){ " +
                                                "nodo2=CreaNodo(id); nodo3=CreaNodo(\",\", nodo1, nodo2); pushPAA(nodo3); }" +
                                        "else{error()}}";
                    break;
                case 42://identificadores-><id>
                    string id42a = Pilas.Stacks.popPI();
                    if (Pilas.Stacks.TipoDato == "nodeclara")
                    {
                        if (TablaSimbolos.TS.existeSimbolo(id42a))
                        {
                            //Pilas.Stacks.pushId(id42a);
                            NodoArblAS nodo42a = new NodoArblAS(new DslToken(TokenType.Id, id42a));
                            Pilas.Stacks.pushPAA(nodo42a);
                        }
                        else
                        {
                            Error e = new Error(Pilas.Stacks.NumeroLinea, "El simbolo " + id42a + " no esta definido", "El símbolo debe de ser declarado previamente.", "Todos los simbolos deben estar previamente definidos");
                            TablaErrores.InstanceTable.agregaError(e);
                        }
                    }
                    numTokenReducir = 1;
                    valueToReturn = "identificadores" + "@" + "If (tipo!=”nodeclara”) {id=popPI(); CrearTS(id,tipo); } else{ siExisteTS(id){ listaID=id; } else{ error() } }";
                    break;
                case 43://int
                    Pilas.Stacks.TipoDato = "int";
                    numTokenReducir = 3;
                    valueToReturn = "Tipo" + "@" + "TipoDato = \"int\"";
                    break;
                case 44://float
                    Pilas.Stacks.TipoDato = "float";
                    numTokenReducir = 5;
                    valueToReturn = "Tipo" + "@" + "TipoDato = \"float\"";
                    break;
                case 45://exp-><exp-simple><op-comparacion><exp-simple>
                    NodoArblAS nodo45a = Pilas.Stacks.popPAA();
                    NodoArblAS nodo45b = Pilas.Stacks.popPAA();
                    NodoArblAS nodo45c = new NodoArblAS(new DslToken(TokenType.OperadorComp, getOperadorComparacion()));
                    nodo45c.setNodoIzquierdo(nodo45b);
                    nodo45c.setNodoDerecho(nodo45a);
                    Pilas.Stacks.pushPAA(nodo45c);
                    numTokenReducir = 3;
                    valueToReturn = "exp" + "@" + "nodo1=popPAA(); nodo2=popPAA(); " +
                                                   "nodo3=new nodo(\"" + getOperadorComparacion() + "\", nodo2, nodo1); " +
                                                   "pushPAA(nodo3);";
                    break;
                case 46://exp-><exp-simple>
                    numTokenReducir = 1;
                    valueToReturn = "exp";
                    break;
                case 47://==
                    numTokenReducir = 2;
                    valueToReturn = "op-comparacion";
                    Pilas.Stacks.Comparador = 5;
                    break;
                case 48://>
                    numTokenReducir = 1;
                    valueToReturn = "op-comparacion";
                    Pilas.Stacks.Comparador = 6;
                    break;
                case 49://<
                    numTokenReducir = 1;
                    valueToReturn = "op-comparacion";
                    Pilas.Stacks.Comparador = 7;
                    break;
                case 50://exp-simple-><exp-simple><opsuma><term>
                    string operador50a = getOperador();
                    NodoArblAS nodo50a = Pilas.Stacks.popPAA();
                    NodoArblAS nodo50b = Pilas.Stacks.popPAA();
                    NodoArblAS nodo50c = new NodoArblAS(new DslToken(TokenType.OperadorSuma, operador50a));
                    nodo50c.setNodoIzquierdo(nodo50b);
                    nodo50c.setNodoDerecho(nodo50a);
                    Pilas.Stacks.pushPAA(nodo50c);
                    numTokenReducir = 3;
                    valueToReturn = "exp-simple" + "@" + "nodo1=popPAA(); nodo2=popPAA(); " +
                                                   "nodo3=new nodo(\"" + operador50a + "\", nodo2, nodo1); " +
                                                   "pushPAA(nodo3); pushPO(\"+\")";
                    break;
                case 51://exp-simple-><term>
                    numTokenReducir = 1;
                    valueToReturn = "exp-simple";
                    break;
                case 52://+
                    Pilas.Stacks.pushPO("+");
                    numTokenReducir = 1;
                    valueToReturn = "opsuma" + "@" + "pushPO(\"+\")";
                    break;
                case 53://-
                    Pilas.Stacks.pushPO("-");
                    numTokenReducir = 1;
                    valueToReturn = "opsuma" + "@" + "pushPO(\"-\")";
                    break;
                case 54://term-><term><opmult><potencia>
                    string operador54a = getOperador();
                    NodoArblAS nodo54a = Pilas.Stacks.popPAA();
                    NodoArblAS nodo54b = Pilas.Stacks.popPAA();
                    NodoArblAS nodo54c = new NodoArblAS(new DslToken(TokenType.OperadorMult, operador54a));
                    nodo54c.setNodoIzquierdo(nodo54b);
                    nodo54c.setNodoDerecho(nodo54a);
                    Pilas.Stacks.pushPAA(nodo54c);
                    numTokenReducir = 3;
                    valueToReturn = "term" + "@" + "nodo1=popPAA(); nodo2=popPAA(); " +
                                                   "nodo3=new nodo(\"" + operador54a + "\", nodo2, nodo1); " +
                                                   "pushPAA(nodo3)";
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
                    valueToReturn = "potencia" + "@" + "fact=popPAA(); poten=popPAA(); " +
                                                       "nodo=new nodo(\"^\", poten, fact); " +
                                                       "pushPAA(nodo)";
                    break;
                case 57://potencia-><factor>
                    numTokenReducir = 1;
                    valueToReturn = "potencia";
                    break;
                case 58://*
                    Pilas.Stacks.pushPO("*");
                    numTokenReducir = 1;
                    valueToReturn = "opmult" + "@" + "pushPO(\"*\")";
                    break;
                case 59:///
                    Pilas.Stacks.pushPO("/");
                    numTokenReducir = 1;
                    valueToReturn = "opmult" + "@" + "pushPO(\"/\")";
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
                    valueToReturn = "factor" + "@" + "pushPAA(new " + nodo61a.ToString() + ")";
                    break;
                case 62://factor-><id>
                    string id62a = Pilas.Stacks.popPI();

                    if (!TablaSimbolos.TS.existeSimbolo(id62a))
                    {
                        Error e = new Error(Pilas.Stacks.NumeroLinea, "El simbolo " + id62a + " no esta definido", "El símbolo debe de ser declarado previamente.", "Todos los simbolos deben estar previamente definidos");
                        TablaErrores.InstanceTable.agregaError(e);
                    }
                    else
                    {
                        NodoArblAS nodo62a = new NodoArblAS(new DslToken(TokenType.Id, id62a));
                        Pilas.Stacks.pushPAA(nodo62a);
                    }

                    numTokenReducir = 1;
                    valueToReturn = "factor" + "@" + "id=popPI(); If (existeTS(id)){ nodo = new nodo(id); pushPAA(nodo); } else{ error(); }";
                    //valueToReturn = "factor" + "@" + "pushPAA(new " + nodo62a.ToString() + ")";
                    break;
                // ##### ================================== Generación de identificadores
                case 63://id->_<id1>
                    string id63a = Pilas.Stacks.popPI();
                    id63a = "_" + id63a;
                    Pilas.Stacks.pushPI(id63a);
                    numTokenReducir = 2;
                    valueToReturn = "id" + "@" + "pushPI(\"" + id63a + "\")";
                    break;
                case 64://id1-><id1><letra>
                    string idStr1 = Pilas.Stacks.popPI();
                    string idStr2 = Pilas.Stacks.popPI();
                    Pilas.Stacks.pushPI((idStr2 + idStr1));
                    numTokenReducir = 2;
                    valueToReturn = "id1" + "@" + "pushPI(\"" + (idStr2 + idStr1) + "\")";
                    break;
                case 65://id1-><id1><digito>
                    string id1 = Pilas.Stacks.popPI();
                    string id2 = Pilas.Stacks.popPV().ToString();
                    Pilas.Stacks.pushPI((id1 + id2));
                    numTokenReducir = 2;
                    valueToReturn = "id1" + "@" + "pushPI(\"" + (id1 + id2) + "\")";
                    break;
                case 66://id1-><letra>
                    numTokenReducir = 1;
                    valueToReturn = "id1";
                    break;
                // ##### ================================== Caracteres de a-z A-Z _
                case 67://a
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"a\")";
                    Pilas.Stacks.pushPI("a");
                    break;
                case 68://b
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"b\")";
                    Pilas.Stacks.pushPI("b");
                    break;
                case 69://c
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"c\")";
                    Pilas.Stacks.pushPI("c");
                    break;
                case 70://d
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"d\")";
                    Pilas.Stacks.pushPI("d");
                    break;
                case 71://e
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"e\")";
                    Pilas.Stacks.pushPI("e");
                    break;
                case 72://f
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"f\")";
                    Pilas.Stacks.pushPI("f");
                    break;
                case 73://g
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"g\")";
                    Pilas.Stacks.pushPI("g");
                    break;
                case 74://h
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"h\")";
                    Pilas.Stacks.pushPI("h");
                    break;
                case 75://h
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"i\")";
                    Pilas.Stacks.pushPI("i");
                    break;
                case 76://j
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"j\")";
                    Pilas.Stacks.pushPI("j");
                    break;
                case 77://k
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"k\")";
                    Pilas.Stacks.pushPI("k");
                    break;
                case 78://l
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"l\")";
                    Pilas.Stacks.pushPI("l");
                    break;
                case 79://m
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"m\")";
                    Pilas.Stacks.pushPI("m");
                    break;
                case 80://n
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"n\")";
                    Pilas.Stacks.pushPI("n");
                    break;
                case 81://o
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"o\")";
                    Pilas.Stacks.pushPI("o");
                    break;
                case 82://p
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"p\")";
                    Pilas.Stacks.pushPI("p");
                    break;
                case 83://q
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"q\")";
                    Pilas.Stacks.pushPI("q");
                    break;
                case 84://r
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"r\")";
                    Pilas.Stacks.pushPI("r");
                    break;
                case 85://s
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"s\")";
                    Pilas.Stacks.pushPI("s");
                    break;
                case 86://t
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"t\")";
                    Pilas.Stacks.pushPI("t");
                    break;
                case 87://u
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"u\")";
                    Pilas.Stacks.pushPI("u");
                    break;
                case 88://v
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"v\")";
                    Pilas.Stacks.pushPI("v");
                    break;
                case 89://w
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"w\")";
                    Pilas.Stacks.pushPI("w");
                    break;
                case 90://x
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"x\")";
                    Pilas.Stacks.pushPI("x");
                    break;
                case 91://y
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"y\")";
                    Pilas.Stacks.pushPI("y");
                    break;
                case 92://z
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"z\")";
                    Pilas.Stacks.pushPI("z");
                    break;
                case 93://A
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"A\")";
                    Pilas.Stacks.pushPI("A");
                    break;
                case 94://B
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"B\")";
                    Pilas.Stacks.pushPI("B");
                    break;
                case 95://C
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"C\")";
                    Pilas.Stacks.pushPI("C");
                    break;
                case 96://D
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"D\")";
                    Pilas.Stacks.pushPI("D");
                    break;
                case 97://E
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"E\")";
                    Pilas.Stacks.pushPI("E");
                    break;
                case 98://F
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"F\")";
                    Pilas.Stacks.pushPI("F");
                    break;
                case 99://G
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"G\")";
                    Pilas.Stacks.pushPI("G");
                    break;
                case 100://H
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"H\")";
                    Pilas.Stacks.pushPI("H");
                    break;
                case 101://I
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"I\")";
                    Pilas.Stacks.pushPI("I");
                    break;
                case 102://J
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"J\")";
                    Pilas.Stacks.pushPI("J");
                    break;
                case 103://K
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"K\")";
                    Pilas.Stacks.pushPI("K");
                    break;
                case 104://L
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"L\")";
                    Pilas.Stacks.pushPI("L");
                    break;
                case 105://M
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"M\")";
                    Pilas.Stacks.pushPI("M");
                    break;
                case 106://N
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"N\")";
                    Pilas.Stacks.pushPI("N");
                    break;
                case 107://O
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"O\")";
                    Pilas.Stacks.pushPI("O");
                    break;
                case 108://P
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"P\")";
                    Pilas.Stacks.pushPI("P");
                    break;
                case 109://Q
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"Q\")";
                    Pilas.Stacks.pushPI("Q");
                    break;
                case 110://R
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"R\")";
                    Pilas.Stacks.pushPI("R");
                    break;
                case 111://S
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"S\")";
                    Pilas.Stacks.pushPI("S");
                    break;
                case 112://T
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"T\")";
                    Pilas.Stacks.pushPI("T");
                    break;
                case 113://U
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"U\")";
                    Pilas.Stacks.pushPI("U");
                    break;
                case 114://V
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"V\")";
                    Pilas.Stacks.pushPI("V");
                    break;
                case 115://W
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"W\")";
                    Pilas.Stacks.pushPI("W");
                    break;
                case 116://X
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"X\")";
                    Pilas.Stacks.pushPI("X");
                    break;
                case 117://Y
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"Y\")";
                    Pilas.Stacks.pushPI("Y");
                    break;
                case 118://Z
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"Z\")";
                    Pilas.Stacks.pushPI("Z");
                    break;
                case 119://_
                    numTokenReducir = 1;
                    valueToReturn = "letra" + "@" + "pushPI(\"_\")";
                    Pilas.Stacks.pushPI("_");
                    break;
                // ##### ================================== De Digito a Número
                case 120://num-><num><digito>
                    int numA = Pilas.Stacks.popPV();
                    int numB = Pilas.Stacks.popPV();
                    int numC = (numB * 10) + numA;
                    Pilas.Stacks.pushPV(numC.ToString());
                    numTokenReducir = 2;
                    valueToReturn = "num" + "@" + "pushPV(\"" + numC.ToString() + "\")";
                    break;
                case 121://num-><digito>
                    numTokenReducir = 1;
                    valueToReturn = "num";
                    break;
                // ##### ================================== Digitos del 0 al 9
                case 122://0 
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"0\")";
                    Pilas.Stacks.pushPV("0");
                    break;
                case 123://1
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"1\")";
                    Pilas.Stacks.pushPV("1");
                    break;
                case 124://2
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"2\")";
                    Pilas.Stacks.pushPV("2");
                    break;
                case 125://3
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"3\")";
                    Pilas.Stacks.pushPV("3");
                    break;
                case 126://4
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"4\")";
                    Pilas.Stacks.pushPV("4");
                    break;
                case 127://5
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"5\")";
                    Pilas.Stacks.pushPV("5");
                    break;
                case 128://6
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"6\")";
                    Pilas.Stacks.pushPV("6");
                    break;
                case 129://7
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"7\")";
                    Pilas.Stacks.pushPV("7");
                    break;
                case 130://8
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"8\")";
                    Pilas.Stacks.pushPV("8");
                    break;
                case 131://9
                    numTokenReducir = 1;
                    valueToReturn = "digito" + "@" + "pushPV(\"9\")";
                    Pilas.Stacks.pushPV("9");
                    break;
            }

            return valueToReturn;
        }


        private static string getOperador()
        {
            string valueToReturn = string.Empty;
            valueToReturn = Pilas.Stacks.popPO();
            return valueToReturn;

            //switch (Pilas.Stacks.Operador)
            //{
            //    case 1:
            //        valueToReturn = "+";
            //        break;
            //    case 2:
            //        valueToReturn = "-";
            //        break;
            //    case 3:
            //        valueToReturn = "*";
            //        break;
            //    case 4:
            //        valueToReturn = "/";
            //        break;
            //    default:
            //        valueToReturn = string.Empty;
            //        break;
            //}
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
