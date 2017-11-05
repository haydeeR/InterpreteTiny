using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compi
{
    public class LR1
    {
        /// <summary>
        /// Gramatica a la que hace referencia
        /// </summary>
        Gramatica g = null;
        /// <summary>
        /// Form padre para poder modificar la interfaz
        /// </summary>
        editorGramatica padre = null;
        /// <summary>
        /// Lista de producciones para acceder a los tokens
        /// </summary>
        List<Produccion> producciones = null;
        /// <summary>
        /// Lista de cadenas para mostrar las producciones
        /// en pantalla
        /// </summary>
        List<string> prod = null;
        /// <summary>
        /// Lista de estados del metodo LR1 donde el Estado contiene 
        /// el indice del estado y la lista de producciones del estado
        /// y tambien un atributo tokenDeLlegada 
        /// </summary>
        List<EdoLR1> listEdos = null;
        /// <summary>
        /// Contador de estados que se estan realizando en el metodo LR1
        /// </summary>
        private int indEdo = 0;

        private List<List<string>> tablaLR1 = null;

        PrimeroYSiguiente firstAndNext = null;
        private List<List<string>> wholeSetFirst = null;

        public LR1(Gramatica g, editorGramatica padre)
        {
            this.g = g;
            this.padre = padre;
            this.prod = new List<string>();
            this.producciones = g.getTokensXProd();
            firstAndNext = new PrimeroYSiguiente(this.g.getTokensXProd(), this.g.getNTerminal(), this.g.getTerminales());
            wholeSetFirst = firstAndNext.dameConjuntoPrimero();
            this.prodAumentada();
            this.enumeraProducciones();
            // Metodo LR1 analisis sintactico ascendente
            this.analisisLR1();
            this.tablaLR1 = new List<List<string>>();
            this.regresaEdos();
        }

        /// <summary>
        /// Verifica que en la gramatica g exista 
        /// una produccion aumentada basada en la primera produccion
        /// </summary>
        public void prodAumentada()
        {
            Produccion aux = g.getTokensXProd()[0];
            string aumentada = aux.getNTerminal();
            int indProd;
            //Si no existe produccion aumentada se agrega
            if (aumentada[(aumentada.Length - 1)] != '\'')
                g.addProduccionAumentada(aumentada + "'", aumentada);
            indProd = 0;
            // Modifica el token de busqueda insertando un $
            this.producciones[indProd].setTokenBusqueda("$");
            this.inicializa();
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
                if (g.esTerminal(token[0]))
                {
                    conjuntoPrimero += token[0] + ",";
                }
                else
                {
                    for (int i = 0; i < token.Count(); i++)
                    {
                        indiceNTerm = g.getNTerminal().IndexOf(token[i]);
                        if (indiceNTerm != -1)
                        {
                            for(int j = 0; j < this.wholeSetFirst[indiceNTerm].Count(); j++)
                                conjuntoPrimero += this.wholeSetFirst[indiceNTerm][j] + ",";
                        }
                    }
                }
            }
            return conjuntoPrimero;
        }

        /// <summary>
        /// Analisis LR1 Rutina principal para construir el conjunto de elementos del LR1
        /// </summary>
        public void analisisLR1()
        {
            Produccion prodAumentada = this.producciones[0];
            EdoLR1 nEdo = new EdoLR1();
            List<EdoLR1> edosRecorridos = new List<EdoLR1>();
           
            this.listEdos = new List<EdoLR1>();
            nEdo.setId(this.indEdo);
            nEdo.setProduccion(prodAumentada);
            nEdo.setTokenDeLlegada("Inicio");
            this.listEdos.Add(this.cerradura(nEdo));
            genLR1();    
         }

        /// <summary>
        /// Metodo que genera el AFD del LR1
        /// </summary>
        public void genLR1()
        {
            EdoLR1 edo = null, nuevo = null, edoSiguiente = null;
            EdoLR1 aux = null;
            int maxEdos = this.listEdos.Count();
            int indToken = 0;
            string t = "";
            Boolean punto= false;

            for(int i = 0; i < maxEdos; i++)
            {
                edo = this.listEdos[i];
                foreach(Produccion p in edo.getProducciones())
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
                                edo.setAccion("R"+edo.getProducciones().IndexOf(p));
                            }
                        }
                        else
                        {
                            if (punto)
                            {
                                nuevo = this.irA(p, t, indToken);
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
                                    this.listEdos.Add(nuevo);
                                }
                                punto = false;
                            }
                        }
                        indToken++;
                    }
                    indToken = 0;
                }
                maxEdos = this.listEdos.Count();
                if(i+1 < maxEdos )
                {
                    edoSiguiente = this.listEdos[i + 1];
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
        public EdoLR1 irA(Produccion p, string token, int indtoken)
        {
            EdoLR1 nEdo = new EdoLR1();
            nEdo.setTokenDeLlegada(token);
            Produccion pAvanzada = new Produccion(p);
            pAvanzada.avanzaIndicadorDeProceso(indtoken);
            nEdo.setProduccion(pAvanzada);
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
            string terminal = "";

            for(int i = 0; i < maxTam; i++ )
            {
                p = edo.getProducciones()[i];   
                terminal = p.produccionLR();
                if (g.getNTerminal().Contains(terminal))
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
            foreach (EdoLR1 e in listEdos)
            {
                if (nuevo.getTokenDeLlegada() == e.getTokenDeLlegada())
                    if(this.produccionesIguales(nuevo.getProducciones()[0], e.getProducciones()[0]))
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
        /// Diferncia entre dos listas de estados 
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

            if ((indiceIndicador + 1) < p.getTokens().Count())
            {
                token = p.getTokens()[(indiceIndicador + 1)];
                foreach (Produccion pIni in this.producciones)
                {
                    pNueva = new Produccion("1");
                    if (pIni.getNTerminal().CompareTo(token) == 0)
                    {
                        pNueva.copiaP(pIni);
                        pNueva.setTokenBusqueda(conjuntoPrim);
                        if (!this.existe(pNueva, edo))
                            edo.setProduccion(pNueva);
                    }
                }
            }
        }


        /// <summary>
        /// Este metodo inserta el contador de secuencia de la produccion
        /// como un punto . Antes de empezar cualquier recorrido esto convierte 
        /// a todas estas producciones en elementos iniciales del metodo LR1
        /// </summary>
        public void inicializa()
        {
            int inicial = 0;
            foreach (Produccion p in this.producciones)
            {
                p.setNuevoToken(".", inicial);
            }
            this.switchProd();
            //this.padre.muestraProducciones(prod);
        }


        /// <summary>
        /// Metodo para pasar de la lista de producciones por tokens
        /// a una lista de cadenas para mostrarla en pantalla
        /// </summary>
        public void switchProd()
        {
            string nuevaP = "";
            string tokens = "";
            foreach (Produccion p in this.producciones)
            {
                nuevaP = tokens = "";
                foreach (string s in p.getTokens())
                {
                    tokens += s;
                }
                nuevaP = p.getNTerminal() + "->" + tokens + "," + p.getTokenBusqueda();
                this.prod.Add(nuevaP);
            }
        }


        public int clasificaAccion(EdoLR1 edo)
        {
            int res = -1;
            Produccion p = null;
            p = edo.getProducciones()[0];
            res = p.despORedu(this.g);

            return res;
        }


        public void llenarTablaLR1()
        {
            EdoLR1 edoAux;
            string tokenAux = "";

            foreach (EdoLR1 e in this.listEdos)
            {
                foreach (AristaLR1 a in e.getListArista())
                {
                    edoAux = a.getEdoDestino();
                    tokenAux = edoAux.getTokenDeLlegada();
                    if (g.esNTerminal(tokenAux))
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
                            break;
                        }
                        else
                            e.setAccion("R" + e.getProducciones()[0].getId().ToString());
                    }
                }
            }
            padre.muestraTermYNTerm();
        }

        public void enumeraProducciones()
        {
            int i = 0;
            foreach (Produccion p in this.producciones)
            {
                p.setId(i);
                i++;
            }
        }

        public void regresaEdos()
        {
            g.setListaEdos(this.listEdos);
        }
    }
}
