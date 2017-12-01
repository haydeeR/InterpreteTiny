using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compi
{
    public class TokenDefinition
    {
        private Regex mRegex;
        private readonly TokenType tokenType;

        public TokenDefinition(TokenType mTokenType, string mRegexPattern)
        {
            mRegex = new Regex(mRegexPattern, RegexOptions.IgnoreCase);
            tokenType = mTokenType;
        }


        public TokenMatch Match(string inputString)
        {
            var match = mRegex.Match(inputString);

            if (match.Success)
            {
                string remainingText = string.Empty;
                if (match.Length != inputString.Length)
                    remainingText = inputString.Substring(match.Length);

                return new TokenMatch()
                {
                    isMatch = true,
                    RemainingText = remainingText,
                    mTokenType = tokenType,
                    Value = match.Value
                };
            }
            else
            {
                return new TokenMatch() { isMatch = false };
            }
        }
    }



    public class TokenMatch
    {
        public bool isMatch { get; set; }
        public TokenType mTokenType { get; set; }
        public string Value { get; set; }
        public string RemainingText { get; set; }
    }


    public class DslToken
    {
        private int prioridad;

        public TokenType TokenType { get; set; }
        public string Value { get; set; }
        public int Prioridad { get { return this.prioridad; } }


        public DslToken(TokenType tokenType)
        {
            TokenType = tokenType;
            Value = string.Empty;
            this.prioridad = this.setPrioridadToken();
        }


        public DslToken(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
            this.prioridad = this.setPrioridadToken();
        }


        private int setPrioridadToken()
        {
            int prioridad = 120;

            if (this.TokenType == TokenType.Id ||
                this.TokenType == TokenType.Cadena ||
                this.TokenType == TokenType.Numero)
            {
                prioridad = 100;
            }
            else if (this.TokenType == TokenType.SeparadorComa)
            {
                prioridad = 90;
            }
            else if (this.TokenType == TokenType.OperadorSuma)
            {
                prioridad = 80;
            }
            else if (this.TokenType == TokenType.OperadorMult)
            {
                prioridad = 75;
            }
            else if (this.TokenType == TokenType.OperadorPote)
            {
                prioridad = 70;
            }
            else if (this.TokenType == TokenType.OperadorComp ||
                this.TokenType == TokenType.OperadorAssign)
            {
                prioridad = 60;
            }
            else if (this.TokenType == TokenType.AbreParent ||
                this.TokenType == TokenType.CierraParent ||
                this.TokenType == TokenType.AbreLlaves ||
                this.TokenType == TokenType.CierraLlaves)
            {
                prioridad = 50;
            }
            if (this.TokenType == TokenType.KeyWord)
            {
                if (this.Value != null &&
                    (this.Value.Contains("read") ||
                     this.Value.Contains("write")))
                    prioridad = 60;
                else
                {
                    prioridad = 20;
                }
            }

            return prioridad;
        }


        public DslToken Clone()
        {
            return new DslToken(TokenType, Value);
        }
    }


    /// <summary>
    /// Define que tipo de token es
    /// </summary>
    public enum TokenType
    {
        Id,
        Cadena,
        Numero,
        KeyWord,
        OperadorAssign,
        OperadorComp,
        OperadorSuma,
        OperadorMult,
        OperadorPote,
        AbreParent,
        CierraParent,
        AbreLlaves,
        CierraLlaves,
        FinInstruccion,
        SeparadorComa,
    }
}
