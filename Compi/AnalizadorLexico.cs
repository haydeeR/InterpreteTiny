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
        string id, cadena, numero, keywords, opComp, opSuma, opMult, opPote, abreParent, cierraParent;
        string identificadores;



        public AnalizadorLexico()
        {
            this.patron = new Regex(@"[A-Za-z!-~0-9]+");
            this.tokenDefinitions = new List<TokenDefinition>();
            this.sentenceDefinitions = new List<SentenceDefinition>();

            this.id = @"_[a-zA-Z0-9]+";
            this.cadena = "\\\"[a-zA-Z0-9:;,\\.\\[\\]\\*\\+\\?¿¡!#%&/~]+\\\"";
            this.numero = @"\d+";
            this.keywords = @"(if|then|else|end|read|write|print|repeat|until|:=|Var|var)";
            this.opComp = @"(==|<|>)";
            this.opSuma = @"(\+|-)";
            this.opMult = @"(\*|/)";
            this.opPote = @"\^";
            this.abreParent = @"\(";
            this.cierraParent = @"\)";
            this.identificadores = @"(\s)*" + this.id + @"(\s)*(,(\s)*" + this.id + @"(\s)*)*";

            this.agregaTokenDefinitions();
            this.agregaSentenceDefinitions();
        }


        private void agregaSentenceDefinitions()
        {
            string declarePattern = @"^(Var|var)\{(int|float)(\s)+" + this.id + @"(\s)*(,(\s)*(" + this.id + @"))*\}";
            string readPattern = @"^read\(" + this.id + @"\)";
            string printPattern = "^" + this.cadena + @"," + this.identificadores + "|" + this.cadena + "|" + this.identificadores;
            string writePattern = @"^write\((\s)*" + printPattern + @"(\s)*\)";
            string assignPattern = "^" + this.id + @"(\s)*:=(\s)*" + "";


            string factor = @"(" + this.numero + "|" + this.id + ")";
            string potenciaAux = @"(" + factor + @"(\s)*\^(\s)*" + factor + ")";
            string potencia = potenciaAux + @"|" + factor;
            string termAux = @"(" + potencia + @"(\s)*" + this.opMult + @"(\s)*" + potencia + @")";
            string term = @"" + termAux + @"|" + potencia;

            string expSimpleAux = @"(" + term + @"(\s)*" + this.opSuma + @"(\s)*" + term + @")";
            string expSimple = @"(" + expSimpleAux + "|" + term + ")";
            string exp = @"(" + expSimple + @"(\s)*" + this.opComp + @"(\s)*" + expSimple + @")|" + expSimple;


            string ifPattern = @"^if\(" + exp + @"\){";
            string elseIfPattern = @"else{";
            string endIfPattern = "}endif;";
            string repeatStart = "repeat{";
            string repeatEnd = @"}until\(" + exp + @"\)";

            string simpleIfSentence = "";
            string complexIfSentence = "";
            string repeatSentence = "";
            string assignSentence = "";
            string readSentence = "";
            string writeSentence = "";
            string declareSentence = "";

            string sentencia = (@"^" + simpleIfSentence + @"|" + complexIfSentence + @"|" + repeatSentence + @"|" + 
                assignSentence + @"|" + readSentence + @"|" + writeSentence + @"|" + declareSentence);

            string secuenciaSentenciaAux = @"" + sentencia + @";" + sentencia + @"";
            string secuenciaSentencia = @"" + secuenciaSentenciaAux + @"|" + sentencia;
             

            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaAssign, assignPattern));

            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaSimpleIf, ifPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaElse, elseIfPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaEndIf, endIfPattern));

            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaRepeatStart, repeatStart));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaRepeatEnd, repeatEnd));

            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaRead, readPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaWrite, writePattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaDeclara, declarePattern));
        }


        private void agregaTokenDefinitions()
        {
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Id, @"^" + this.id));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Cadena, @"^" + this.cadena));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Numero, @"^" + this.numero));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.KeyWord, @"^" + this.keywords));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorComp, @"^" + this.opComp));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorSuma, @"^" + this.opSuma));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorMult, @"^" + this.opMult));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorPote, @"^" + this.opPote));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.AbreParent, @"^" + this.abreParent));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.CierraParent, @"^" + this.cierraParent));
        }
    }

    /// <summary>
    /// Define que tipo de sentencia es la línea
    /// </summary>
    public enum SentenceType
    {
        SentenciaSimpleIf,
        SentenciaElse,
        SentenciaEndIf,
        SentenciaRepeatStart,
        SentenciaRepeatEnd,
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
