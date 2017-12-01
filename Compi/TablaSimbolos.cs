using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManejoDeErrores;

namespace Tsimbolos
{
    public class TablaSimbolos
    {

        public List<MetaSimbolo> metaSimbolos;
        TE tablaErrorres;


        public TablaSimbolos()
        {
            this.metaSimbolos = new List<MetaSimbolo>();
            tablaErrorres = new TE();
        }

        public List<MetaSimbolo> TablaMetaSimbolos
        {
            get { return metaSimbolos; }
            set { metaSimbolos = value; }
        }

        public void reinicialista()
        {
            metaSimbolos.Clear();
        }

        #region Tabla de Simbolos , 51
        public void inicialista()
        {
            //string simb,string val,int nunlin,int tam,int ambit,int id_,string tip,string descrip
            MetaSimbolo ts = new MetaSimbolo("<</", "", -0, -0, -0, 0, "comentario", "inicio de un comentario de mas de una linea", "");
            metaSimbolos.Add(ts);
            MetaSimbolo ts1 = new MetaSimbolo("/>>", "", -0, -0, -0, 1, "comentario", "final de un comentario de mas de una linea", "");
            metaSimbolos.Add(ts1);
            MetaSimbolo ts2 = new MetaSimbolo("<<", "", -0, -0, -0, 2, "comentario", "inicio de un comentario de una linea", "");
            metaSimbolos.Add(ts2);
            MetaSimbolo ts3 = new MetaSimbolo(">>", "", -0, -0, -0, 3, "comentario", "final de un comentario de una linea", "");
            metaSimbolos.Add(ts3);
            MetaSimbolo ts4 = new MetaSimbolo("{", "", -0, -0, -0, 4, "bloque", "inicio de un bloque", "");
            metaSimbolos.Add(ts4);
            MetaSimbolo ts5 = new MetaSimbolo("}", "", -0, -0, -0, 5, "bloque", "final de un bloque", "");
            metaSimbolos.Add(ts5);
            MetaSimbolo ts6 = new MetaSimbolo("<#int", "", -0, -0, -0, 6, "palabra reservada", "numero entero", "");
            metaSimbolos.Add(ts6);
            MetaSimbolo ts7 = new MetaSimbolo("<#integer", "", -0, -0, -0, 7, "palabra reservada", "numero entero", "");
            metaSimbolos.Add(ts7);
            MetaSimbolo ts8 = new MetaSimbolo("<#double", "", -0, -0, -0, 8, "palabra reservada", "numero con decimales", "");
            metaSimbolos.Add(ts8);
            MetaSimbolo ts9 = new MetaSimbolo("<#string", "", -0, -0, -0, 9, "palabra reservada", "cadena de caracteres", "");
            metaSimbolos.Add(ts9);
            MetaSimbolo ts10 = new MetaSimbolo("<#bool", "", -0, -0, -0, 10, "palabra reservada", "booleano true o false", "");
            metaSimbolos.Add(ts10);
            MetaSimbolo ts11 = new MetaSimbolo("<#boolean", "", -0, -0, -0, 11, "palabra reservada", "booleano true o false", "");
            metaSimbolos.Add(ts11);
            MetaSimbolo ts12 = new MetaSimbolo(":", "", -0, -0, -0, 12, "asignacion", "simbolo de asignacion", "");
            metaSimbolos.Add(ts12);
            MetaSimbolo ts13 = new MetaSimbolo(";", "", -0, -0, -0, 13, "posicionador", "final de linea", "");
            metaSimbolos.Add(ts13);
            MetaSimbolo ts14 = new MetaSimbolo("'", "", -0, -0, -0, 14, "indicador de texto", "inicio y final de un texto", "");
            metaSimbolos.Add(ts14);
            MetaSimbolo ts15 = new MetaSimbolo("&/", "", -0, -0, -0, 15, "posicionador", "salto de linea en un texto", "");
            metaSimbolos.Add(ts15);
            MetaSimbolo ts16 = new MetaSimbolo("[", "", -0, -0, -0, 16, "arreglo", "inicio de asignacion de un arreglo", "");
            metaSimbolos.Add(ts16);
            MetaSimbolo ts17 = new MetaSimbolo("]", "", -0, -0, -0, 17, "arreglo", "final de asignacion de un arreglo", "");
            metaSimbolos.Add(ts17);
            MetaSimbolo ts18 = new MetaSimbolo("+", "", -0, -0, -0, 18, "operador", "suma", "");
            metaSimbolos.Add(ts18);
            //tabla_de_simbolos ts19 = new tabla_de_simbolos("+", "", -0, -0, -0, 19, "concatenador", "concatenador de elementos");
            //TSimbolos.Add(ts19);
            MetaSimbolo ts20 = new MetaSimbolo("-", "", -0, -0, -0, 20, "operador", "resta", "");
            metaSimbolos.Add(ts20);
            MetaSimbolo ts21 = new MetaSimbolo("*", "", -0, -0, -0, 21, "operador", "multiplicacion", "");
            metaSimbolos.Add(ts21);
            MetaSimbolo ts22 = new MetaSimbolo("/", "", -0, -0, -0, 22, "operador", "division", "");
            metaSimbolos.Add(ts22);
            MetaSimbolo ts23 = new MetaSimbolo("!", "", -0, -0, -0, 23, "signo relacionador", "negacion", "");
            metaSimbolos.Add(ts23);
            MetaSimbolo ts24 = new MetaSimbolo(">", "", -0, -0, -0, 24, "signo comparador", "mayor que", "");
            metaSimbolos.Add(ts24);
            MetaSimbolo ts25 = new MetaSimbolo("<", "", -0, -0, -0, 25, "signo comparador", "menor que", "");
            metaSimbolos.Add(ts25);
            MetaSimbolo ts26 = new MetaSimbolo(">:", "", -0, -0, -0, 26, "signo comparador", "mayor o igual que", "");
            metaSimbolos.Add(ts26);
            MetaSimbolo ts27 = new MetaSimbolo("<:", "", -0, -0, -0, 27, "signo comparador", "menor o igual que", "");
            metaSimbolos.Add(ts27);
            MetaSimbolo ts28 = new MetaSimbolo("&", "", -0, -0, -0, 28, "signo relacionador", "expresion y (and)", "");
            metaSimbolos.Add(ts28);
            MetaSimbolo ts29 = new MetaSimbolo("||", "", -0, -0, -0, 29, "signo relacionador", "expresion or", "");
            metaSimbolos.Add(ts29);
            MetaSimbolo ts30 = new MetaSimbolo("::", "", -0, -0, -0, 30, "signo comparador", "expresion igual", "");
            metaSimbolos.Add(ts30);
            MetaSimbolo ts31 = new MetaSimbolo("!:", "", -0, -0, -0, 31, "signo comparador", "expresion distinto", "");
            metaSimbolos.Add(ts31);
            MetaSimbolo ts32 = new MetaSimbolo("<<si_", "", -0, -0, -0, 32, "palabra reservada", "si tal condicion se cumple", "");
            metaSimbolos.Add(ts32);
            MetaSimbolo ts33 = new MetaSimbolo("<<ysi_", "", -0, -0, -0, 33, "palabra reservada", "y si tal condicion se cumple en vez de la anterior", "");
            metaSimbolos.Add(ts33);
            MetaSimbolo ts34 = new MetaSimbolo("<<sino", "", -0, -0, -0, 34, "palabra reservada", "si no se cumple la condicion", "");
            metaSimbolos.Add(ts34);
            MetaSimbolo ts35 = new MetaSimbolo("#ncasd", "", -0, -0, -0, 35, "palabra reservada", "opciones (casos)", "");
            metaSimbolos.Add(ts35);
            MetaSimbolo ts36 = new MetaSimbolo("casd", "", -0, -0, -0, 36, "palabra reservada", "caso", "");
            metaSimbolos.Add(ts36);
            MetaSimbolo ts37 = new MetaSimbolo("fcasd", "", -0, -0, -0, 37, "palabra reservada", "final del caso", "");
            metaSimbolos.Add(ts37);
            MetaSimbolo ts38 = new MetaSimbolo("#mintrs", "", -0, -0, -0, 38, "palabra reservada", "mientras", "");
            metaSimbolos.Add(ts38);
            MetaSimbolo ts39 = new MetaSimbolo("#pr", "", -0, -0, -0, 39, "palabra reservada", "for", "");
            metaSimbolos.Add(ts39);
            MetaSimbolo ts40 = new MetaSimbolo("#prlist", "", -0, -0, -0, 40, "palabra reservada", "foreach", "");
            metaSimbolos.Add(ts40);
            MetaSimbolo ts41 = new MetaSimbolo("#funcion", "", -0, -0, -0, 41, "palabra reservada", "expresion para determinar un metodo", "");
            metaSimbolos.Add(ts41);
            MetaSimbolo ts42 = new MetaSimbolo("#try", "", -0, -0, -0, 42, "palabra reservada", "intenta realizar el codigo contenido", "");
            metaSimbolos.Add(ts42);
            MetaSimbolo ts43 = new MetaSimbolo("#catch", "", -0, -0, -0, 43, "palabra reservada", "muestra una accion a seguir en caso de error", "");
            metaSimbolos.Add(ts43);
            MetaSimbolo ts44 = new MetaSimbolo("#mostrar", "", -0, -0, -0, 44, "palabra reservada", "muestra en consola", "");
            metaSimbolos.Add(ts44);
            MetaSimbolo ts45 = new MetaSimbolo("#leer", "", -0, -0, -0, 45, "palabra reservada", "pide en consola", "");
            metaSimbolos.Add(ts45);
            MetaSimbolo ts46 = new MetaSimbolo("#ejc", "", -0, -0, -0, 46, "palabra reservada", "ejecuta setencia", "");
            metaSimbolos.Add(ts46);
            MetaSimbolo ts47 = new MetaSimbolo("#clase", "", -0, -0, -0, 47, "palabra reservada", "clase", "");
            metaSimbolos.Add(ts47);
            MetaSimbolo ts48 = new MetaSimbolo("(", "", -0, -0, -0, 48, "parametro", "inicia peticion de parametro", "");
            metaSimbolos.Add(ts48);
            MetaSimbolo ts49 = new MetaSimbolo(")", "", -0, -0, -0, 49, "parametro", "termina peticion de parametro", "");
            metaSimbolos.Add(ts49);
            MetaSimbolo ts50 = new MetaSimbolo(",", "", -0, -0, -0, 50, "concatenador", "concatena variables", "");
            metaSimbolos.Add(ts50);

            //---------------------------------------------asi deberian entrar los datos nuevos encontrados-------------------------------------------------
            //tabla_de_simbolos ts51 = new tabla_de_simbolos("", "", -0, -0, -0, 51, "numero", "especifica un numero");
            //TSimbolos.Add(ts51);
            //tabla_de_simbolos ts52 = new tabla_de_simbolos("", "", -0, -0, -0, 52, "identificador", "especifica una palabra que identifica una variable");
            //TSimbolos.Add(ts52);
            //----------------------------------------------------------------------------------------------------------------------------------------------

        }
        #endregion




        public List<MetaSimbolo> llamatabla()
        {
            return metaSimbolos;
        }

        public void añadir_obj(MetaSimbolo Ts)
        {
            metaSimbolos.Add(Ts);
        }

        public bool compararAL(string argsplit)
        {
            bool bandera = false;
            foreach (var palabra in metaSimbolos)
            {

                if (palabra.simbolo == argsplit)
                {
                    bandera = true;
                    break;
                }
                else
                {
                    bandera = false;
                }

            }
            return bandera;
        }
        //----------------------------------------------------------------
        public bool revisar_duplicados()
        {
            bool flag = false;
            //int cont = 0;
            foreach (var sent1 in metaSimbolos)
            {
                foreach (var sent2 in metaSimbolos)
                {
                    if (sent1.ID == sent2.ID)//&& sent1.TipoVar == sent2.TipoVar
                    {

                        flag = true;


                        //cont += 1;
                    }
                }
            }
            return flag;
        }
        //----------------------------------------------------------------
        public void compararALsemantic()
        {

            foreach (var palabra in metaSimbolos)
            {
                foreach (var palabra2 in metaSimbolos)
                {
                    if (palabra.Simbolo == palabra2.Simbolo)
                    {
                        palabra2.TipoVar = palabra.TipoVar;
                        //break;
                    }
                }
            }

        }

        //----------------------------------------------------------------

        public int compararALRef(string argsplit)
        {
            int id = 0;
            foreach (var palabra in metaSimbolos)
            {

                if (palabra.simbolo == argsplit)
                {
                    id = palabra.id;
                    break;
                }
                else
                {
                    id = 0;
                }

            }
            return id;
        }

        //--------------------------------------------------------------
        public int contlineas()
        {
            int numid = 0;
            foreach (var nlinea in metaSimbolos)
            {
                numid = numid + 1;
            }
            return numid - 1;
        }




    }
}
