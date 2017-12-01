﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class NodoArblAS
    {
        DslToken token = null;
        NodoArblAS nodoDerecho = null;
        NodoArblAS nodoIzquierdo = null;

        public NodoArblAS(DslToken token)
        {
            this.token = token;
            this.nodoDerecho = null;
            this.nodoIzquierdo = null;
        }

        public NodoArblAS(DslToken token,DslToken tokenIz, DslToken tokenDer)
        {
            this.token = token; 
            this.nodoIzquierdo = new NodoArblAS(tokenIz);
            this.nodoDerecho = new NodoArblAS(tokenDer);
        }

        public void setToken(DslToken token)
        {
            this.token = token;
        }

        public DslToken getToken()
        {
            return this.token;
        }

        public void setNodoIzquierdo(DslToken token)
        {
            if (this.token != null)
                this.nodoIzquierdo.setToken(token);
            else
                this.nodoIzquierdo = new NodoArblAS(token);
        }

        public NodoArblAS getNodoIzquierdo()
        {
            return this.nodoIzquierdo;
        }

        public void setNodoDerecho(DslToken token)
        {
            if (this.token != null)
                this.nodoDerecho.setToken(token);
            else
                this.nodoDerecho = new NodoArblAS(token);
        }

        public NodoArblAS getNodoDerecho()
        {
            return this.nodoDerecho;
        }


        public void setNodoIzquierdo(NodoArblAS nodo)
        {
            this.nodoIzquierdo = nodo;
        }

        public void setNodoDerecho(NodoArblAS nodo)
        {
            this.nodoDerecho = nodo;
        }


        public bool setNodo(NodoArblAS nodo)
        {
            bool result = false;

            if (this.nodoIzquierdo == null)
            {
                this.nodoIzquierdo = nodo;
                result = true;
            }
            else if(this.nodoDerecho == null)
            {
                this.nodoDerecho = nodo;
                result = true;
            }

            return result;
        }

    }
}