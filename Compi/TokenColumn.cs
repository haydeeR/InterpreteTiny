using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    /// <summary>
    /// Clase que permite almacenar los movimientos 
    /// que genera un token en un determinado estado
    /// </summary>
    class TokenColumn
    {

        string token;
        List<string> valores;

        public string Token { get { return this.token; } set { this.token = value; } }
        public List<string> Valores { get { return valores; } set { this.valores = value; } }

        public TokenColumn()
        {
            this.token = string.Empty;
            this.valores = new List<string>();
        }


        public TokenColumn(string theToken)
        {
            this.token = theToken;
            this.valores = new List<string>();
        }


        public int agregaValor(string valor)
        {
            this.valores.Add(valor);
            return this.valores.IndexOf(valor);
        }


        public int eliminaValor(string valor)
        {
            int index = this.valores.IndexOf(valor);
            this.valores.Remove(valor);
            return index;
        }

        public int cambiaValor(int numEdo, string valor)
        {
            if (numEdo < this.valores.Count)
            {
                this.valores[numEdo] = valor;
                return -1;
            }

            return numEdo;
        }


        public string dameValor(int numEdo)
        {
            string valor = numEdo < this.valores.Count ? this.valores[numEdo] : string.Empty;
            return valor;
        }

    }
}
