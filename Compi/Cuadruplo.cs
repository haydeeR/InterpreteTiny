﻿using System;
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
        //DslToken
        Resultado _resultado;

        public Cuadruplo(DslToken operador = null, DslToken op1 = null, DslToken op2 = null)
        {
            this.id = Guid.NewGuid();
            this.operando1 = op1;
            this.operando2 = op2;
            this.operador = operador;
            this._resultado = new Resultado(this.id.ToString(), new DslToken(TokenType.Id, this.id.ToString()));
        }

        public Cuadruplo(Resultado resultado, DslToken operador = null, DslToken op1 = null, DslToken op2 = null)
        {
            this.id = Guid.NewGuid();
            this.operando1 = op1;
            this.operando2 = op2;
            this.operador = operador;
            this._resultado = resultado;
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
