using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class Cuadruplo
    {
        Guid id;
        string operando1;
        string operando2;
        string operador;
        string _nameVarResult;

        public Cuadruplo(string op1, string op2, string operador, int index)
        {
            this.id = Guid.NewGuid();
            this.operando1 = op1;
            this.operando2 = op2;
            this.operador = operador;
            _nameVarResult = "temp" + index.ToString();
        }

        public string nameVarResult {

            get {
                return this._nameVarResult;
            }

            set{
                this._nameVarResult = value;
            }

        }


    }
}
