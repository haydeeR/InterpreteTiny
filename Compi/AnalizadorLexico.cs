using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Compi
{
    class AnalizadorLexico
    {
        /// <summary>
        /// Patron de los caracteres aceptables en la gramatica
        /// </summary>
        Regex patron = null;

        public AnalizadorLexico() {
            this.patron = new Regex(@"[A-Za-z!-~0-9]+");
        }



    }
}
