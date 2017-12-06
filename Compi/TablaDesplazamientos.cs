using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    /// <summary>
    /// Esta clase almacenará cada estado con cada 
    /// uno de sus posibles desplazamientos o reducciones
    /// </summary>
    class TablaDesplazamientos
    {
        //List<EdoLR1> estados;
        List<TokenColumn> despXToken;

        //public List<EdoLR1> Estados { get { return this.estados; } set { this.estados = value; } }
        public List<TokenColumn> DespXToken { get { return this.despXToken; } set { this.despXToken = value; } }


        public TablaDesplazamientos()
        {
            //this.estados = new List<EdoLR1>();
            this.despXToken = new List<TokenColumn>();
        }


        public TablaDesplazamientos(List<EdoLR1> estados = null)
        {
            //this.estados = estados;
            despXToken = new List<TokenColumn>();
        }


        public int agregaColumna(string token)
        {
            var columna = this.despXToken.FirstOrDefault(x => x.Token.Equals(token));

            if (columna == null)
            {
                TokenColumn column = new TokenColumn(token);
                this.despXToken.Add(column);
                return this.despXToken.IndexOf(column);
            }

            return this.despXToken.IndexOf(columna);
        }


        public int agregaColumna(TokenColumn theTokenColumn)
        {
            this.despXToken.Add(theTokenColumn);
            return this.despXToken.IndexOf(theTokenColumn);
        }


        public int agregaValor(string token, string valor)
        {
            int index = -1;
            TokenColumn column = this.despXToken.First(x => x.Token.Equals(token));
            if (column != null)
            {
                column.agregaValor(valor);
                index = column.Valores.IndexOf(valor);
            }

            return index;
        }

        public void cambiaValor(int indEdo, string token, string nuevoValor)
        {
            TokenColumn column = this.despXToken.First(x => x.Token.Equals(token));
            if (column != null)
            {
                column.cambiaValor(indEdo, nuevoValor);
            }
        }


        public string dameValor(int numEstado, string token)
        {
            TokenColumn column = this.despXToken.FirstOrDefault(x => x.Token.Equals(token));
            string resultado = column != null ? column.dameValor(numEstado) : string.Empty;
            return resultado;
        }
    }
}
