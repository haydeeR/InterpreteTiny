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

        public TokenDefinition(TokenType mTokenType, string mRegexPattern) {
            mRegex = new Regex(mRegexPattern,RegexOptions.IgnoreCase);
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

                return new TokenMatch() {
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



    public class TokenMatch {
        public bool isMatch { get; set; }
        public TokenType mTokenType { get; set; }
        public string Value { get; set; }
        public string RemainingText { get; set; }
    }


    public class DslToken
    {
        public DslToken(TokenType tokenType)
        {
            TokenType = tokenType;
            Value = string.Empty;
        }

        public DslToken(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public TokenType TokenType { get; set; }
        public string Value { get; set; }

        public DslToken Clone()
        {
            return new DslToken(TokenType, Value);
        }
    }
}
