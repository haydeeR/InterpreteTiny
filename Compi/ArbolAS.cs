using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class ArbolAS
    {
        DslToken token = null;
        ArbolAS nodoDerecho = null;
        ArbolAS nodoIzquierdo = null;

        public ArbolAS(DslToken token)
        {
            this.token = token;
            this.nodoDerecho = null;
            this.nodoIzquierdo = null;
        }

        public ArbolAS(DslToken token,DslToken tokenIz, DslToken tokenDer)
        {
            this.token = token; 
            this.nodoIzquierdo = new ArbolAS(tokenIz);
            this.nodoDerecho = new ArbolAS(tokenDer);
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
                this.nodoIzquierdo = new ArbolAS(token);
        }

        public ArbolAS getNodoIzquierdo()
        {
            return this.nodoIzquierdo;
        }

        public void setNodoDerecho(DslToken token)
        {
            if (this.token != null)
                this.nodoDerecho.setToken(token);
            else
                this.nodoDerecho = new ArbolAS(token);
        }

        public ArbolAS getNodoDerecho()
        {
            return this.nodoDerecho;
        }
    }
}