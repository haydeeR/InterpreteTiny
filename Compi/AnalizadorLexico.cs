using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Compi
{
    class AnalizadorLexico
    {
        List<TokenDefinition> tokenDefinitions;
        List<SentenceDefinition> sentenceDefinitions;

        List<List<DslToken>> tokens;
        List<DslSentence> sentences;

        public List<List<DslToken>> TokenDefinitions { get { return this.tokens; } }
        public List<DslSentence> SentenceDefinitions { get { return this.sentences; } }

        /// <summary>
        /// Constructor de la clase
        /// </summary>

        public AnalizadorLexico()
        {
            this.tokenDefinitions = new List<TokenDefinition>();
            this.sentenceDefinitions = new List<SentenceDefinition>();

            this.agregaTokenDefinitions();
            this.agregaSentenceDefinitions();
        }

        /// <summary>
        /// Se agregan a la lista las definiciones de sentencias
        /// </summary>
        private void agregaSentenceDefinitions()
        {
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaDeclara, GRegex.declarePattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaAssign, GRegex.assignPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaRead, GRegex.readPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaWrite, GRegex.writePattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaSimpleIf, GRegex.ifPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaElse, GRegex.elseIfPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaEndIf, GRegex.endIfPattern));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaRepeatStart, GRegex.repeatStart));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaRepeatEnd, GRegex.repeatEnd));
            this.sentenceDefinitions.Add(new SentenceDefinition(SentenceType.SentenciaFinBloque, GRegex.endSentencesBlock));
        }

        /// <summary>
        /// Se agregan a la lista las definiciones de los tokens
        /// </summary>
        private void agregaTokenDefinitions()
        {
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Id, @"^" + GRegex.id));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Cadena, @"^" + GRegex.cadena));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.Numero, @"^" + GRegex.numero));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.KeyWord, @"^" + GRegex.keywords));

            this.tokenDefinitions.Add(new TokenDefinition(TokenType.TipoDato, @"^" + GRegex.tipoDato));

            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorAssign, @"^" + GRegex.opAssign));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorComp, @"^" + GRegex.opComp));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorSuma, @"^" + GRegex.opSuma));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorMult, @"^" + GRegex.opMult));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.OperadorPote, @"^" + GRegex.opPote));

            this.tokenDefinitions.Add(new TokenDefinition(TokenType.AbreParent, @"^" + GRegex.abreParent));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.CierraParent, @"^" + GRegex.cierraParent));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.AbreLlaves, @"^" + GRegex.abreLlaves));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.CierraLlaves, @"^" + GRegex.cierraLlaves));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.FinInstruccion, @"^" + GRegex.finInstruccion));
            this.tokenDefinitions.Add(new TokenDefinition(TokenType.SeparadorComa, @"^" + GRegex.separatorComma));
        }

        /// <summary>
        /// Recorre todas las sentencias que encuentra en el archivo 
        /// de la ruta que se recibe por parametro
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private List<DslSentence> clasificaSentencias(string fileName)
        {
            List<DslSentence> sentences = new List<DslSentence>();
            string line;
            int contLinea = 1;

            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        while (sr.Peek() >= 0)
                        {
                            line = sr.ReadLine().Trim().Replace(" ", "");
                            sentences.AddRange(getSentenceDefinitions(line, contLinea));
                            contLinea += 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return sentences;
        }


        private List<DslSentence> clasificaSentencias(List<string> lineasTiny)
        {
            List<DslSentence> sentences = new List<DslSentence>();
            string line;
            int contLinea = 1;

            try
            {
                lineasTiny.ForEach(linea =>
                {
                    if (linea.Trim() != string.Empty)
                    {
                        line = linea.Trim().Replace("\t", "").Replace("\r\n", "").Replace(" ", "");
                        sentences.AddRange(getSentenceDefinitions(line, contLinea));
                    }
                    contLinea++;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return sentences;
        }


        /// <summary>
        /// Tokeniza cada línea que fue clasificada
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void tokeniza(string fileName)
        {
            List<List<DslToken>> tokens = new List<List<DslToken>>();
            List<DslSentence> sentences = clasificaSentencias(fileName);

            if (sentences != null && sentences.Count > 0)
            {
                foreach (DslSentence sentencia in sentences)
                {
                    List<DslToken> tokensXLine = new List<DslToken>();

                    tokensXLine.AddRange(getTokens(sentencia.value.Replace(" ", "")));
                    tokens.Add(tokensXLine);
                }
                this.sentences = sentences;
                this.tokens = tokens;
            }

        }


        public void tokeniza(List<string> codigoTiny)
        {
            List<List<DslToken>> tokens = new List<List<DslToken>>();
            List<DslSentence> sentences = clasificaSentencias(codigoTiny);

            if (sentences != null && sentences.Count > 0)
            {
                foreach (DslSentence sentencia in sentences)
                {
                    List<DslToken> tokensXLine = new List<DslToken>();

                    tokensXLine.AddRange(getTokens(sentencia.value.Replace(" ", "")));
                    tokens.Add(tokensXLine);
                }
                this.sentences = sentences;
                this.tokens = tokens;
            }

        }

        // ##################################################################################
        // Match para tokenizar las expresiones 
        // ##################################################################################


        #region "Match por tokens"

        /// <summary>
        /// Tokeniza la linea que recibe por parametro
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<DslToken> getTokens(string line)
        {
            List<DslToken> tokens = new List<DslToken>();
            string remainingText = line.Trim();

            while (!string.IsNullOrWhiteSpace(remainingText))
            {
                var match = FindMatch(remainingText);
                if (match.isMatch)
                {
                    tokens.Add(new DslToken(match.mTokenType, match.Value));
                    remainingText = match.RemainingText;
                }
                else
                {
                    remainingText = remainingText.Substring(1);
                }
            }

            return tokens;
        }

        /// <summary>
        /// Valida por cada definición de token para encontrar coincidencia
        /// </summary>
        /// <param name="textLine"></param>
        /// <returns></returns>
        private TokenMatch FindMatch(string textLine)
        {
            foreach (var tokenDefinition in this.tokenDefinitions)
            {
                var match = tokenDefinition.Match(textLine);
                if (match.isMatch)
                    return match;
            }

            return new TokenMatch() { isMatch = false };
        }
        #endregion

        // ##################################################################################
        // Match para clasificar cada línea del código
        // ##################################################################################

        #region "Match por lineas"
        public List<DslSentence> getSentenceDefinitions(string line, int numLinea)
        {
            List<DslSentence> sentences = new List<DslSentence>();
            string remainingText = line.Trim();

            while (!string.IsNullOrWhiteSpace(remainingText))
            {
                var match = FindMatchLine(remainingText);
                if (match.isMatch)
                {
                    sentences.Add(new DslSentence(match.mSentenceType, match.Value, numLinea));
                    remainingText = match.RemainingText;
                }
                else
                {
                    remainingText = remainingText.Substring(1);
                }
            }

            //tokens.Add(new DslToken(TokenType.));

            return sentences;
        }


        private SentenceMatch FindMatchLine(string textLine)
        {
            foreach (var sentenceDefinition in this.sentenceDefinitions)
            {
                var match = sentenceDefinition.Match(textLine);
                if (match != null && match.isMatch)
                    return match;
            }

            return new SentenceMatch() { isMatch = false };
        }
        #endregion

    }
}
