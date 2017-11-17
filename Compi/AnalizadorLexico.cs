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
        List<TokenDefinition> tokenDefinitions;
        List<string> sentenceTypes;


        public AnalizadorLexico() {
            this.patron = new Regex(@"[A-Za-z!-~0-9]+");
            this.tokenDefinitions = new List<TokenDefinition>();
            this.AgregarPatrones();
        }


        private void AgregarPatrones() {
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Id, @"^_[a-zA-Z0-9]+"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Cadena, "^\"[a-zA-Z0-9]+\""));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Num, @"^\d+"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.KeyWord, @"^if|then|else|end|read|write|print|repeat|until|:=|Var|var"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorComp, @"^==|<|>|"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorSuma, @"^\+|-"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorMult, @"^\*|/"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorPote, @"^\^"));
        }
    }


    /// <summary>
    /// Define que tipo de sentencia es la línea
    /// </summary>
    public enum SentenceType
    {
        SentenciaSimpleIf,
        SentenciaComplexIf,
        SentenciaRepeat,
        SentenciaAssign,
        SentenciaRead,
        SentenciaWrite,
        SentenciaDeclara,
    }


    /// <summary>
    /// Define que tipo de token es
    /// </summary>
    public enum TokenType
    {
        Id,
        Cadena,
        Num,
        KeyWord,
        OperadorComp,
        OperadorSuma,
        OperadorMult,
        OperadorPote,
    }
}
