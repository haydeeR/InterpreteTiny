using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class Token
    {
        /// <summary>
        /// Variable que se le indica que tipo de token es terminal o no terminal
        /// </summary>
        private int tipoToken = 0;
        private string vallex = "";
        private string val = "";
        private string tipo = "";

        public Token(int isTerminal, string vallex)
        {
        }
    }
}
