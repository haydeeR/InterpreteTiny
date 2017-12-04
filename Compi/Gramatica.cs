using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Compi
{
    [Serializable]
    public class Gramatica
    {
        /// <summary>
        /// Nombre de la gramatica como un identificador para 
        /// poder guardarla
        /// </summary>
        private string nombre;
        /// <summary>
        /// Lista de todos los simbolos terminales de la gramatica
        /// </summary>
        private List<string> lTerminales = null;
        /// <summary>
        /// Lista de todos los simbolos no terminales de la gramatica
        /// </summary>
        private List<string> lNoTerminales = null;
        /// <summary>
        /// Todas las producciones de la gramatica
        /// </summary>
        private List<string> producciones = null;
        /// <summary>
        /// Todas las producciones se agregan en una lista de tokens por produccion
        /// donde la clase contiene una variable string NTerminal y una lista de 
        /// string donde son los tokens
        /// </summary>
        private List<Produccion> tokensXProd = null;
        /// <summary>
        /// Indica en que numero de produccion va
        /// </summary>
        private int idProd = 0;
        /// <summary>
        /// La lista de estados que regresa el metodo lr1 a la gramatica
        /// </summary>
        private List<EdoLR1> listaEdos = null;
        /// <summary>
        /// Lista que contiene el conjunto primero dependiendo de la produccion
        /// </summary>
        private List<List<String>> conjuntoPrimero = null;
        /// <summary>
        /// Lista que contiene el conjunto siguiente
        /// </summary>
        private List<List<String>> conjuntoSiguiente = null;
        /// <summary>
        /// Form padre para poder modificar la interfaz
        /// </summary>
        MainWinForm padre = null;
        /// <summary>
        /// Lista de cadenas para mostrar las producciones
        /// en pantalla
        /// </summary>
        List<string> prod = null;
        /// <summary>
        /// Contador de estados que se estan realizando en el metodo LR1
        /// </summary>
        private int indEdo = 0;

        private List<List<string>> tablaLR1 = null;

        private List<List<string>> wholeSetFirst = null;

        public Gramatica(string nombre)
        {
            this.nombre = nombre;
            this.lTerminales = new List<string>();
            this.lNoTerminales = new List<string>();
            this.producciones = new List<string>();
            this.tokensXProd = new List<Produccion>();
        }

        /// <summary>
        /// Para agregar una gramatica se le debe asignar un nombre
        /// </summary>
        /// <param name="nombre">Nombre que se le asigna a la gramatica</param>
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        /// <summary>
        /// Se diferencia entre terminales y no terminales agregando en una lista todos los 
        /// tokens no terminales
        /// </summary>
        /// <param name="noTerminal">Token no terminal</param>
        public void setNoTerminal(string noTerminal)
        {
            if (lNoTerminales.Exists(element => element == noTerminal) == false)
            {
                this.lNoTerminales.Add(noTerminal);
            }
        }

        /// <summary>
        /// Se diferencia entre terminales y no terminales agregando en una lista todos los 
        /// tokens terminales
        /// </summary>
        /// <param name="terminal"></param>
        public void setTerminal(string terminal)
        {
            if (lTerminales.Exists(element => element == terminal) == false)
            {
                //El epsilon en este momento esta formado como un terminal
                this.lTerminales.Add(terminal);
            }
        }

        /// <summary>
        /// Independientemente si es terminal o no lo es se agrega a una lista de tokens
        /// cada uno de los tokens que existen en una produccion
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ind"></param>
        public void setToken(string token, int ind)
        {
            this.tokensXProd[ind].setNuevoToken(token);
        }

        public void setProduccion(string produccion)
        {
            this.producciones.Add(produccion);
        }

        /// <summary>
        /// Hace que la produccion se divida en terminales y no terminales 
        /// y separa los tokens
        /// </summary>
        /// <param name="produccionIzq"></param>
        /// <param name="produccionDer"></param>
        public void setProduccionAumentada(string produccionIzq, string produccionDer)
        {
            Produccion nueva = new Produccion(produccionIzq);
            nueva.setNuevoToken(produccionDer);
            this.producciones.Insert(0, produccionIzq + "->" + produccionDer);
            this.lNoTerminales.Add(produccionIzq);
            this.tokensXProd.Insert(0, nueva);

        }
        /// <summary>
        /// Metodo que regresa el nombre de la gramatica
        /// </summary>
        /// <returns></returns>
        public string getNombre()
        {
            return this.nombre;
        }
        /// <summary>
        /// Metodo que regresa las producciones de la gramatica
        /// </summary>
        /// <returns></returns>
        public List<string> getProducciones()
        {
            return this.producciones;
        }
        /// <summary>
        /// Metodo que regresa una lista de producciones donde cada item
        /// es una produccion con un conjunto de tokenes.
        /// </summary>
        /// <returns></returns>
        public List<Produccion> getTokensXProd()
        {
            return this.tokensXProd;
        }
        /// <summary>
        /// Metodo que regresa una lista dxe no terminales
        /// </summary>
        /// <returns>List <String> </returns>
        public List<string> getNTerminal()
        {
            return this.lNoTerminales;
        }
        /// <summary>
        /// Metodo que regresa una lista cadenas que son los tokens termnales
        /// </summary>
        /// <returns></returns>
        public List<string> getTerminales()
        {
            return this.lTerminales;
        }
        /// <summary>
        /// Metodo que asigna la lista de estados del LR1
        /// </summary>
        /// <param name="listaEdos"></param>
        public void setListaEdos(List<EdoLR1> listaEdos)
        {
            this.listaEdos = listaEdos;
        }
        /// <summary>
        /// Metodo que regresa la lista de estados del LR1
        /// </summary>
        /// <returns></returns>
        public List<EdoLR1> getListaEdos()
        {
            return this.listaEdos;
        }


        /// <summary>
        /// Verifica si un token es terminal o no 
        /// </summary>
        /// <param name="t">Token a evaluar</param>
        /// <returns>true si es token false si no lo es</returns>
        public bool esTerminal(string t)
        {
            bool res = false;
            if (this.lTerminales.Exists(element => element == t))
                res = true;
            return res;
        }
        /// <summary>
        /// Verifica si un token es no terminal 
        /// </summary>
        /// <param name="t">Token a evaluar</param>
        /// <returns>true si es un no terminal false si no lo es</returns>
        public bool esNTerminal(string t)
        {
            bool res = false;
            if (this.lNoTerminales.Exists(element => element == t))
                res = true;
            return res;
        }



        /// <summary>
        /// Evalua linea de texto para convertirla en producción
        /// </summary>
        /// <param name="produccion">Cadena de produccion</param>
        /// <returns></returns>
        public bool evaluaProduccion(string produccion)
        {
            bool res = true;
            string[] prod = null;
            string cadIzq = "", cadDer = "";
            int ind = 0;

            //Se busca el indice de los caracteres que parten la producción
            ind = produccion.IndexOf("->");
            if (ind == -1)
                res = false;
            else
            {
                cadIzq = produccion.Remove(ind);
                cadDer = produccion.Remove(0, ind + 2);
                cadIzq = cadIzq.Trim();
                //          MatchCollection cad = patron.Matches(cadIzq);
                //          if (cad.Count <= 0)
                res = false;
            }
            prod = cadDer.Split('|');
            this.agregaProducciones(cadIzq, prod);
            this.addNoTerminal(cadIzq, true, 0);
            this.difTermYNTerm(prod);
            return res;
        }

        /// <summary>
        /// Método que analiza las partes de una producción y 
        /// define cual es terminal y cual es no termianl
        /// </summary>
        /// <param name="prods"></param>
        public void difTermYNTerm(string[] prods)
        {
            int ind = 0;
            bool res = true;
            string aux;

            foreach (string p in prods)
            {
                aux = p;
                aux = aux.Trim();
                ind = 0;
                while (ind < aux.Length && res)
                {
                    switch (aux[ind])
                    {
                        case '<':
                            if (this.evaluaMenorYMayorQue(aux))
                            {
                                ind = this.addNoTerminal(aux);
                                aux = aux.Remove(0, ind + 1);
                                ind = 0;
                            }
                            break;
                        case '\\':
                            ind = this.addTerminal(aux, 1);
                            aux = aux.Remove(0, ind);
                            ind = 0;
                            break;
                        default:
                            ind = this.addTerminal(aux);
                            aux = aux.Remove(0, ind);
                            ind = 0;
                            break;
                    }
                }
                this.idProd++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadIzq"></param>
        /// <param name="prods"></param>
        public void agregaProducciones(string cadIzq, string[] prods)
        {
            for (int i = 0; i < prods.Count(); i++)
            {
                addProduccion(cadIzq, prods[i]);
                this.tokensXProd.Add(new Produccion(cadIzq));
            }
        }

        /// <summary>
        /// Se agrega una nueva produccion a la gramatica 
        /// se incrementa la prouduccion con flecha
        /// </summary>
        /// <param name="cadIzq"></param>
        /// <param name="cadDer"></param>
        public void addProduccion(string cadIzq, string cadDer)
        {
            this.setProduccion(cadIzq + "->" + cadDer);
        }

        /// <summary>
        /// Metodo que es mandado llamar desde el LR1 cuando se detecta que la gramatica no 
        /// tiene una produccion aumentada
        /// </summary>
        /// <param name="cadIzq"></param>
        /// <param name="cadDer"></param>
        /// <param name="aumentada"></param>
        public void addProduccionAumentada(string cadIzq, string cadDer)
        {
            this.setProduccionAumentada(cadIzq, cadDer);
        }

        /// <summary>
        /// Se agreta un simbolo terminal a la produccion
        /// </summary>
        /// <param name="prod"></param>
        /// <param name="indiceUntil"></param>
        /// <returns></returns>
        public int addTerminal(string prod, int indiceUntil = 0)
        {
            int indTemp = 0;
            string term = "";

            while (indTemp <= indiceUntil)
            {
                term += prod[indTemp];
                indTemp++;
            }
            this.setTerminal(term);
            this.setToken(term, this.idProd);
            return indTemp;
        }

        /// <summary>
        /// Se agrega un simbolo no terminal a la produccion
        /// </summary>
        /// <param name="prod"></param>
        /// <param name="prim"></param>
        /// <param name="indiceAux"></param>
        /// <returns></returns>
        public int addNoTerminal(string prod, bool prim = false, int indiceAux = -1)
        {
            int indTemp = 1;
            bool flag = true;
            string nterm = "";
            if (prim)
                indTemp = 0;

            while (indTemp < prod.Length && flag)
            {
                switch (prod[indTemp])
                {
                    case '>':
                        flag = false;
                        break;
                }
                if (flag)
                {
                    nterm += prod[indTemp];
                    indTemp++;
                }
            }
            this.setNoTerminal(nterm);
            if (indiceAux == -1)
                this.setToken(nterm, this.idProd);
            return indTemp;
        }

        /// <summary>
        /// Metodo que verifica si el numero de signos mayor y menor
        /// sean correctos
        /// </summary>
        /// <param name="cad"></param>
        /// <returns></returns>
        public bool evaluaMenorYMayorQue(string cad)
        {
            bool res = false;
            if (this.menorMayorQueCorrectos(cad) && !this.menorMayorQueVacios(cad))
                res = true;
            return res;
        }

        /// <summary>
        /// Metodo que verifica si el numero de signos menor sean correctos
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        public bool menorMayorQueCorrectos(string prod)
        {
            bool res = false;
            int valAscci;
            int cont = 0;

            for (int i = 0; i < prod.Length && cont >= 0; i++)
            {
                valAscci = (int)prod[i];
                switch (valAscci)
                {
                    case '<':
                        cont++;
                    break;
                    case '>':
                        cont--;
                    break;
                }
            }
            if (cont == 0) //Todo salio bien
                res = true;
            else
                res = false;
            return res;
        }

        public bool menorMayorQueVacios(string prod)
        {
            bool res = false;
            int valAscci = 0;
            
            for (int i = 0; i < prod.Length && !res; i++)
            {
                valAscci = (int)prod[i];
                switch (valAscci)
                {
                    case '<':
                        if (i + 1 >= prod.Length || prod[i + 1] == '>')
                            res = true; //Parentesis vacios
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// Metodo que se encarga de reservar la  memoria para la 
        /// estructura de listas del conjunto primero
        /// </summary>
        public void creaConjuntoPrimero()
        {
            this.conjuntoPrimero = new List<List<String>>();
            foreach (string nT in this.lNoTerminales)
            {
                this.conjuntoPrimero.Add(new List<string>());
            }
        }

        /// <summary>
        /// Evalua el conjunto primero por sus tres reglas
        /// </summary>
        public void evaluaConjuntoPrimero()
        {
            List<int> cambios = new List<int>();
            int inicio = 1;
            int indNoTermDelConjunto = 0;
            List<string> conjuntoPrimeroDNT = new List<string>();
            Boolean nulleable = false;

            while (cambios.Contains(1) || inicio == 1)
            {
                inicio = this.inicializaCambios(cambios);
                nulleable = false;
                foreach (Produccion p in tokensXProd)
                {
                    indNoTermDelConjunto = lNoTerminales.IndexOf(p.getNTerminal());
                    //Si A -> ep domde ep es la cadena vacia Añadir ep a priero de A
                    if ("~" == (p.getTokens()[p.getId()]))     //Si primero de A es un epsilon agregar el epsilon
                    {
                        if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains("~"))
                        {
                            this.conjuntoPrimero[indNoTermDelConjunto].Add("~");
                            cambios.Add(1);
                        }
                        else { cambios.Add(0); }
                    }
                    else if (this.lTerminales.Contains(p.getTokens()[p.getId()]))     //Si primero de A es un terminal
                    {
                        if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains(p.getTokens()[p.getId()]))
                        {
                            this.conjuntoPrimero[indNoTermDelConjunto].Add(p.getTokens()[p.getId()]);
                            cambios.Add(1);
                        }
                        else
                        {
                            cambios.Add(0);
                        }
                    }
                    else if (this.lNoTerminales.Contains(p.getTokens()[p.getId()]))     //Si primero de A es un no terminal
                    {
                        foreach (string token in p.getTokens())
                        {
                            if (this.lNoTerminales.Contains(token))
                            {
                                nulleable = false;
                                //se obtiene el conjunto primero del primer no terminal q hace la derivacion
                                conjuntoPrimeroDNT = this.conjuntoPrimero[this.lNoTerminales.IndexOf(token)];
                                foreach (string terminal in conjuntoPrimeroDNT)
                                {
                                    if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains(terminal) || terminal == "~")
                                    {
                                        if (terminal != "~")
                                        {
                                            this.conjuntoPrimero[indNoTermDelConjunto].Add(terminal);
                                            cambios.Add(1);
                                        }
                                        else
                                        {
                                            nulleable = true;
                                            cambios.Add(0);
                                        }
                                    }
                                    else
                                        cambios.Add(0);
                                }
                            }
                            else
                            {
                                this.conjuntoPrimero[indNoTermDelConjunto].Add(token);
                                cambios.Add(1);
                            }
                            if (!nulleable)
                            {
                                //Metodo que me diga si el token es nulleable o
                                //no con respecto a la lista que tenemos
                                break;
                            }
                            else if (p.getTokens().IndexOf(token) == p.getTokens().Count - 1) // si el indice del token es igual al tamaño de los tokens -1 la produccion es nulleable
                            {
                                if (!this.conjuntoPrimero[indNoTermDelConjunto].Contains("~"))
                                {
                                    this.conjuntoPrimero[indNoTermDelConjunto].Add("~");
                                    cambios.Add(1);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reserva memoria en las listas para el conjunto siguiente
        /// </summary>
        public void creaConjuntoSiguiente()
        {
            this.conjuntoSiguiente = new List<List<String>>();
            foreach (string nT in this.lNoTerminales)
            {
                this.conjuntoSiguiente.Add(new List<string>());
            }
        }

        /// <summary>
        /// Evalua el conjunto siguiente por sus tres reglas
        /// </summary>
        public void evaluaConjuntoSiguiente()
        {
            string siguiente = "";
            string primeroDeSiguiente = "";
            List<int> cambios = new List<int>();
            int posDeB = 1;
            int indNoTermDelConjunto = 0;
            List<string> conjuntoPrimeroDNT = new List<string>();

            foreach (Produccion p in tokensXProd)
            {
                indNoTermDelConjunto = lNoTerminales.IndexOf(p.getNTerminal());
                if (p == tokensXProd[0])  //Estado inicial primer  produccion añadir $ a siguiente de b
                    this.conjuntoSiguiente[indNoTermDelConjunto].Add("$");
                foreach (string token in p.getTokens())
                {
                    if (this.lNoTerminales.Contains(token)) //analizamos el siguiente de B
                    {
                        indNoTermDelConjunto = lNoTerminales.IndexOf(token);
                        posDeB = p.getTokens().IndexOf(token);
                        if ((posDeB + 1) < p.getTokens().Count)
                        {
                            siguiente = p.getTokens()[posDeB + 1];
                            if (this.lTerminales.Contains(siguiente))
                                this.conjuntoSiguiente[indNoTermDelConjunto].Add(siguiente);
                            else if (this.lNoTerminales.Contains(siguiente))
                            {
                                primeroDeSiguiente = "";
                                foreach (string t in this.conjuntoPrimero[indNoTermDelConjunto])
                                {
                                    if (t != "~")
                                        primeroDeSiguiente += " " + t + " ";
                                }
                                this.conjuntoSiguiente[indNoTermDelConjunto].Add(primeroDeSiguiente);
                            }
                        }
                    }
                }
            }
        }

        private int inicializaCambios(List<int> cambios)
        {
            int tam = cambios.Count;
            cambios.RemoveRange(0, tam);
            return 0;
        }

        public List<List<string>> dameConjuntoPrimero()
        {
            return this.conjuntoPrimero;
        }

        public List<string> getConjuntoPrimero()
        {
            this.conjuntoPrimero = null;
            this.creaConjuntoPrimero();
            this.evaluaConjuntoPrimero();
            
            List<string> nuevoConjunto = new List<string>();
            int ind = 0;
            string conjuntoConcat = "";
            foreach (List<string> conjunto in this.conjuntoPrimero)
            {
                conjuntoConcat = "";
                conjuntoConcat = this.lNoTerminales[ind] + " { ";
                foreach (string token in conjunto)
                {
                    conjuntoConcat += token + " ";
                }
                conjuntoConcat += " }";
                nuevoConjunto.Add(conjuntoConcat);
                ind++;
            }
            return nuevoConjunto;
        }

        public List<string> getConjuntoSiguiente()
        {
            this.conjuntoSiguiente = null;
            this.creaConjuntoSiguiente();
            this.evaluaConjuntoSiguiente();

            List<string> nuevoConjunto = new List<string>();
            int ind = 0;
            string conjuntoConcat = "";
            foreach (List<string> conjunto in this.conjuntoSiguiente)
            {
                conjuntoConcat = "";
                conjuntoConcat = this.lNoTerminales[ind] + " { ";
                foreach (string token in conjunto)
                {
                    conjuntoConcat += token + " ";
                }
                conjuntoConcat += " }";
                nuevoConjunto.Add(conjuntoConcat);
                ind++;
            }
            return nuevoConjunto;
        }
        

        /// <summary>
        /// Constructor del entorno del LR1
        /// </summary>
        /// <param name="padre"></param>
        public void constructorLR1(MainWinForm padre)
        {
            this.padre = padre;
            this.prod = new List<string>();
            wholeSetFirst = this.dameConjuntoPrimero();
            this.prodAumentada();
            this.enumeraProducciones();
            // Metodo LR1 analisis sintactico ascendente
            this.analisisLR1();
            this.tablaLR1 = new List<List<string>>();
        }

        /// <summary>
        /// Verifica que en la gramatica g exista 
        /// una produccion aumentada basada en la primera produccion
        /// </summary>
        public void prodAumentada()
        {
            Produccion aux = this.getTokensXProd()[0];
            string aumentada = aux.getNTerminal();
            int indProd;
            //Si no existe produccion aumentada se agrega
            if (aumentada[(aumentada.Length - 1)] != '\'')
                this.addProduccionAumentada(aumentada + "'", aumentada);
            indProd = 0;
            // Modifica el token de busqueda insertando un $
            this.tokensXProd[indProd].setTokenBusqueda("$");
            this.inicializa();
        }
        
        /// <summary>
        /// Este metodo inserta el contador de secuencia de la produccion
        /// como un punto . Antes de empezar cualquier recorrido esto convierte 
        /// a todas estas producciones en elementos iniciales del metodo LR1
        /// </summary>
        public void inicializa()
        {
            int inicial = 0;
            foreach (Produccion p in this.tokensXProd)
            {
                p.setNuevoToken(".", inicial);
            }
        }

        /// <summary>
        /// Metodo que le asigna un id a cada una de las producciones 
        /// </summary>
        public void enumeraProducciones()
        {
            int i = 0;
            foreach (Produccion p in this.tokensXProd)
            {
                p.setId(i);
                i++;
            }
        }

        /// <summary>
        /// Metodo para agregar el token de busqueda de una produccion a otra
        /// produccion inicial es decir sin token de busqueda
        /// </summary>
        /// <param name="indProd">identificador de la produccion</param>
        /// <param name="gamma">conjunto gamma</param>
        /// <param name="a">token de busqueda hacia adelante</param>
        public void agregaTokenBusqueda(Produccion p, List<string> gamma = null, string a = null)
        {
            string primera = this.getPrimera(p, gamma, a);
            p.setTokenBusqueda(primera);
        }
        
        /// <summary>
        /// Metodo para obtener el conjunto primero de un token 
        /// El conjunto primero del token de busqueda hacia adelante es el 
        /// token de busqueda hacia adelante
        /// </summary>
        /// <param name="token">token del que obtendremos el conjunto primero</param>
        /// <returns>Conjunto primero</returns>
        public string getPrimera(Produccion p, List<string> token, string tokenBusqueda)
        {
            //Guardar primera de la gramatica en un archivo serializable o una variable en memoria y obtenerla 
            //posteriormente en este metodo
            string conjuntoPrimero = "";
            int indiceNTerm = -1;

            if (token == null || token.Count() < 1)
            {
                conjuntoPrimero = tokenBusqueda;
            }
            else
            {
                if (this.esTerminal(token[0]))
                {
                    conjuntoPrimero += token[0] + ",";
                }
                else
                {
                    for (int i = 0; i < token.Count(); i++)
                    {
                        indiceNTerm = this.getNTerminal().IndexOf(token[i]);
                        if (indiceNTerm != -1)
                        {
                            for (int j = 0; j < this.wholeSetFirst[indiceNTerm].Count(); j++)
                                conjuntoPrimero += this.wholeSetFirst[indiceNTerm][j] + ",";
                        }
                    }
                }
            }
            return conjuntoPrimero;
        }

        /// <summary>
        /// Analisis LR1 Rutina principal para construir 
        /// el conjunto de elementos del analisis LR1
        /// </summary>
        public void analisisLR1()
        {
            Produccion prodAumentada = this.tokensXProd[0];
            EdoLR1 nEdo = new EdoLR1();

            this.listaEdos = new List<EdoLR1>();
            nEdo.setId(this.indEdo);
            nEdo.setProduccion(prodAumentada);
            nEdo.setTokenDeLlegada("Inicio");
            this.listaEdos.Add(this.cerradura(nEdo));
            genLR1();
        }

        /// <summary>
        /// Metodo que genera el AFD del LR1
        /// </summary>
        public void genLR1()
        {
            EdoLR1 edo = null, nuevo = null, edoSiguiente = null;
            EdoLR1 aux = null;
            List<Produccion> produccionesDetransicion = null;
            List<int> indicesDeProduccionesDeTransicion = null;
            int maxEdos = this.listaEdos.Count();
            int indToken = 0;
            string t = "";
            Boolean punto = false;

            for (int i = 0; i < maxEdos; i++)
            {
                edo = this.listaEdos[i];
                foreach (Produccion p in edo.getProducciones())
                {
                    punto = false;
                    while (indToken < p.getTokens().Count)
                    {
                        t = p.getTokens()[indToken];
                      
                        if (t == ".")
                        {
                            punto = true;
                            if (indToken + 1 == p.getTokens().Count()) //Estado terminado
                            {
                                edo.setAccion("R" + edo.getProducciones().IndexOf(p));
                            }
                        }
                        else
                        {
                            if (punto)
                            {
                                produccionesDetransicion = edo.getProduccionesDeTransicion(t);
                                indicesDeProduccionesDeTransicion = edo.getIndicesDeTokenDeTransicion(t);
                                nuevo = this.irA(produccionesDetransicion, t, indicesDeProduccionesDeTransicion);
                                aux = this.existe(nuevo);
                                if (aux != null)
                                {
                                    edo.setArista(aux, p);
                                    nuevo = null;
                                }
                                if (nuevo != null && (aux == null))
                                {
                                    nuevo.setId(++this.indEdo);
                                    edo.setArista(nuevo, p);
                                    this.listaEdos.Add(nuevo);
                                }
                                punto = false;
                            }
                        }
                        indToken++;
                    }
                    indToken = 0;
                }
                maxEdos = this.listaEdos.Count();
                if (i + 1 < maxEdos)
                {
                    edoSiguiente = this.listaEdos[i + 1];
                    cerradura(edoSiguiente);
                }
            }
        }

        /// <summary>
        /// Avnza el indicador de la entrada en un simbolo ya que lo he
        /// procesado y llama al metodo cerradura con un nuevo indicador 
        /// de la entrada.
        /// </summary>
        /// <param name="edo">Estado a evaluar</param>
        /// <param name="token">token con el que hace el avance</param>
        /// <returns>Un nuevo estado con el que se hizo la transicion</returns>
        public EdoLR1 irA(List<Produccion> producciones, string token, List<int> indtoken)
        {
            EdoLR1 nEdo = new EdoLR1();
            nEdo.setTokenDeLlegada(token);
            Produccion pAvanzada = null;
            foreach(Produccion p in producciones)
            {
                pAvanzada = new Produccion(p);
                pAvanzada.avanzaIndicadorDeProceso(indtoken[producciones.IndexOf(p)]);
                nEdo.setProduccion(pAvanzada);
            }
            return (nEdo);
        }

        /// <summary>
        /// Permite establecer el conjunto de simbolos de adelanto que nos indeca que
        /// reduccion es posible aplicar en presencia de un simbolo y en esto se basa 
        /// el aumento de precision del algoritmo lr1
        /// </summary>
        /// <param name="edo">Estado a aplicar la cerradura</param>
        /// <returns>Un estado terminado</returns>
        public EdoLR1 cerradura(EdoLR1 edo)
        {
            List<Produccion> prodAux = new List<Produccion>();
            Produccion p = null;
            string primero = "";
            int maxTam = edo.getProducciones().Count();
            string betta = "";

            for (int i = 0; i < maxTam; i++)
            {
                p = edo.getProducciones()[i];
                betta = p.produccionLR();
                if (this.getNTerminal().Contains(betta))
                {
                    primero = getPrimera(p, p.getGamma(), p.getTokenBusqueda());
                    //Agregar producciones iniciales del no terminal B, con primera de gamma
                    this.agregaProdIniciales(p, edo, primero);
                    maxTam = edo.getProducciones().Count();
                }
            }
            return edo;
        }
        /// <summary>
        /// Metodo que dice si el estado existe en una lista de estados.
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns>Estado LR1 si es que existe si no existe regresa null</returns>
        public EdoLR1 existe(EdoLR1 nuevo)
        {
            EdoLR1 res = null;
            foreach (EdoLR1 e in listaEdos)
            {
                if (nuevo.getTokenDeLlegada() == e.getTokenDeLlegada())
                    if (this.produccionesIguales(nuevo.getProducciones()[0], e.getProducciones()[0]))
                        return e;
            }
            return res;
        }
        /// <summary>
        /// Sobrecarga del metodo existe para tipo de dato Produccion
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns>true si existe la produccion en el estado</returns>
        public bool existe(Produccion nueva, EdoLR1 edo)
        {
            bool res = false;

            foreach (Produccion p in edo.getProducciones())
            {
                if ((nueva.getTokens().Count == p.getTokens().Count) && nueva.getTokenBusqueda() == p.getTokenBusqueda())
                {
                    if (this.tokensIguales(nueva.getTokens(), p.getTokens()))
                    {
                        return true;
                    }
                }

            }
            return res;
        }
        /// <summary>
        /// Diferencia entre dos listas de estados 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool diffEdos(List<EdoLR1> a, List<EdoLR1> b)
        {
            bool res = false;
            int i = 0;
            if ((a != null && b != null) && (a.Count == b.Count))
                while (i < a.Count)
                {
                    if (a[i].getTokenDeLlegada() != b[i].getTokenDeLlegada())
                        return false;
                    else
                        res = true;
                    i++;
                }
            return res;
        }

        public bool diffListas(List<Produccion> a, List<Produccion> b)
        {
            bool res = false;
            int i = 0;
            if ((a != null && b != null) && (a.Count == b.Count))
                while (i < a.Count)
                {
                    if (!produccionesIguales(a[i], b[i]))       //Produucciones diferentes
                        return true;
                    i++;
                }
            else           //Son diferentes
                res = true;
            return res;
        }

        public bool produccionesIguales(Produccion a, Produccion b)
        {
            bool res = false;
            if (atributosIguales(a, b) && tokensIguales(a.getTokens(), b.getTokens()) && a.getTokenBusqueda() == b.getTokenBusqueda())
                res = true;
            return res;
        }

        public bool atributosIguales(Produccion a, Produccion b)
        {
            bool res = false;
            if ((a.getNTerminal() == b.getNTerminal()) &&
                (a.getTokenBusqueda() == b.getTokenBusqueda()) &&
                (a.getTokens().Count == b.getTokens().Count))
                res = true;
            return res;
        }

        public bool tokensIguales(List<string> a, List<string> b)
        {
            bool res = true;
            int i = 0;
            while (res && i < a.Count)
            {
                if (a[i] != b[i])
                    res = false;
                i++;
            }
            return res;
        }

        /// <summary>
        /// Metodo que hace una copia  de la listas de una produccion 
        /// anterior a una nueva
        /// </summary>
        /// <param name="anterior"></param>
        /// <param name="nueva"></param>
        public void igualaListas(List<Produccion> anterior, List<Produccion> nueva)
        {
            anterior.Clear();
            foreach (Produccion p in nueva)
            {
                anterior.Add(p);
            }
        }

        public void igualaListas(List<EdoLR1> anterior, List<EdoLR1> nueva)
        {
            anterior.Clear();
            foreach (EdoLR1 p in nueva)
            {
                anterior.Add(p);
            }
        }

        public void agregaProdIniciales(Produccion p, EdoLR1 edo, string conjuntoPrim)
        {
            int indiceIndicador = p.getTokens().IndexOf(".");
            string token = "";
            Produccion pNueva = null;
            List<Produccion> listaProduccionesDelTerminal = null;

            //Si el punto no esta en la posicion final de la producción
            if ((indiceIndicador + 1) < p.getTokens().Count())
            {
                token = p.getTokens()[(indiceIndicador + 1)];
                
                listaProduccionesDelTerminal = this.tokensXProd.Where(x => x.getNTerminal() == token).ToList();

                foreach (Produccion pIni in listaProduccionesDelTerminal)
                {
                    pNueva = new Produccion("1");
                    pNueva.copiaP(pIni);
                    pNueva.setTokenBusqueda(conjuntoPrim);
                    if (!this.existe(pNueva, edo))
                        edo.setProduccion(pNueva);
                }
            }
        }
        
        public int clasificaAccion(EdoLR1 edo)
        {
            int res = -1;
            Produccion p = null;
            p = edo.getProducciones()[0];
            res = p.despORedu(this);
            
            return res;
        }
        
        public void llenarTablaLR1()
        {
            EdoLR1 edoAux;
            string tokenAux = "";

            foreach (EdoLR1 e in this.listaEdos)
            {
                foreach (AristaLR1 a in e.getListArista())
                {
                    edoAux = a.getEdoDestino();
                    tokenAux = edoAux.getTokenDeLlegada();
                    if (this.esNTerminal(tokenAux))
                    {
                        a.setAccion(a.getEdoDestino().getId().ToString());
                    }
                }
                foreach (Produccion p in e.getProducciones())
                {
                    if (p.getTokens()[(p.getTokens().Count - 1)] == ".")
                    {
                        if (p.getNTerminal()[(p.getNTerminal().Length - 1)] == '\'')
                        {
                            e.setAccion(" Aceptar ");
                        }
                        else
                        {
                            foreach (char tokenDeBusqueda in p.getTokenBusqueda())
                            {
                                e.listReducciones.Add(tokenDeBusqueda.ToString() +"# R" + e.getProducciones()[0].getId().ToString());
                            }
                        }
                    }
                }
            }
            padre.muestraTermYNTerm();
        }
    }
}