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
        DslToken operando1;
        DslToken operando2;
        DslToken operador;
        Resultado _resultado;

        public Cuadruplo(int index, DslToken operador = null, DslToken op1 = null, DslToken op2 = null)
        {
            this.id = Guid.NewGuid();
            this.operando1 = op1;
            this.operando2 = op2;
            this.operador = operador;
            this._resultado = new Resultado(this.id.ToString());
        }

        public Resultado resultado
        {
            get { return this._resultado; }
            set { this._resultado = value; }
        }

        public string name_resultado
        {
            get { return this._resultado.NameVar; }
            set { this._resultado.NameVar = value; }
        }

        public string value_resultado
        {
            get { return this._resultado.Value; }
            set { this._resultado.Value = value; }
        }
    }

    public struct Resultado
    {
        string _nameVar;
        string _value;

        public Resultado(string name)
        {
            this._nameVar = name;
            this._value = string.Empty;
        }

        public string NameVar
        {
            get { return this._nameVar; }
            set { this._nameVar = value; }
        }

        public string Value
        {
            get { return _value; }
            set { this._value = value; }
        }
    }
}
