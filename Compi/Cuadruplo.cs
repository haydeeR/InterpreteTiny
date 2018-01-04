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

        int linea;

        public int Linea { get { return this.linea; } set { this.linea = value; } }

        public Cuadruplo(DslToken operador = null, DslToken op1 = null, DslToken op2 = null, int numLinea = -1)
        {
            this.id = Guid.NewGuid();
            this.operando1 = op1;
            this.operando2 = op2;
            this.operador = operador;
            this._resultado = new Resultado(this.id.ToString(), new DslToken(TokenType.IdTemporal, this.id.ToString()));
            this.linea = numLinea >= 0 ? numLinea : -1;
        }

        public Cuadruplo(Resultado resultado, DslToken operador = null, DslToken op1 = null, DslToken op2 = null, int numLinea = -1)
        {
            this.id = Guid.NewGuid();
            this.operando1 = op1;
            this.operando2 = op2;
            this.operador = operador;
            this._resultado = resultado;
            this.linea = numLinea >= 0 ? numLinea : -1;
        }


        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }


        public Resultado resultado
        {
            get { return this._resultado; }
            set { this._resultado = value; }
        }

        public string nameResult
        {
            get { return this._resultado.NameVar; }
            set { this._resultado.NameVar = value; }
        }

        public string valueResult
        {
            get { return this._resultado.Value; }
            set { this._resultado.Value = value; }
        }

        public DslToken Operando1
        {
            get { return this.operando1; }
            set { this.operando1 = value; }
        }

        public DslToken Operando2
        {
            get { return this.operando2; }
            set { this.operando2 = value; }
        }

        public DslToken Operador
        {
            get { return this.operador; }
            set { this.operador = value; }
        }

        public string strOperando1
        {
            get
            {
                if (this.Operando1 != null)
                    return this.operando1.Value;
                else
                    return string.Empty;
            }
        }

        public string strOperando2
        {
            get
            {
                if (this.Operando2 != null)
                    return this.operando2.Value;
                else
                    return string.Empty;
            }
        }

        public string strOperador
        {
            get
            {
                if (this.Operador != null)
                    return this.operador.Value;
                else
                    return string.Empty;
            }
        }

        public string strTempVar
        {
            get
            {
                if (this.resultado != null)
                    return this.resultado.NameVar;
                else
                    return string.Empty;
            }
        }
    }

    public class Resultado
    {
        string _nameVar;
        string _value;
        DslToken _tokenType;

        public Resultado(string name, DslToken tokenType)
        {
            this._nameVar = name;
            this._value = string.Empty;
            this._tokenType = tokenType;
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

        public DslToken tokenType
        {
            get { return this._tokenType; }
            set { this._tokenType = value; }
        }
    }
}
