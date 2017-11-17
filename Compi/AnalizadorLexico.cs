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
        List<SentenceDefinition> sentenceDefinitions;


        public AnalizadorLexico()
        {
            this.patron = new Regex(@"[A-Za-z!-~0-9]+");
            this.tokenDefinitions = new List<TokenDefinition>();
            this.sentenceDefinitions = new List<SentenceDefinition>();

            this.agregaTokenDefinitions();
            this.agregaSentenceDefinitions();
        }


        private void agregaTokenDefinitions()
        {
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Id, @"^_[a-zA-Z0-9]+"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Cadena, "^\"[a-zA-Z0-9]+\""));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Numero, @"^\d+"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.KeyWord, @"^if|then|else|end|read|write|print|repeat|until|:=|Var|var"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorComp, @"^==|<|>|"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorSuma, @"^\+|-"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorMult, @"^\*|/"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorPote, @"^\^"));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.AbreParent, @"^\("));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.CierraParent, @"^\)"));

        }


        private void agregaSentenceDefinitions()
        {
            string assignPattern = @"(Var|var)\{(int|float) _[A-Za-z0-9]+(\s)*(,(\s)*_[A-Za-z0-9]+)+\}";
            string exp = "";
            string exp_simple = "";
            string term = "";
            string potencia = "";
            string factor = @"^((<exp>)|\d+|_[A-Za-z0-9]+)";


            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaAssign, assignPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaSimpleIf, @"if()"));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaAssign, @""));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaAssign, @""));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaAssign, @""));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaAssign, @""));
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
        Numero,
        KeyWord,
        OperadorComp,
        OperadorSuma,
        OperadorMult,
        OperadorPote,
        AbreParent,
        CierraParent,
    }
}
