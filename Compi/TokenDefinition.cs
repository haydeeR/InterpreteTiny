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
            return null;
        }
    }



    public class TokenMatch {
        public bool isMatch { get; set; }
        public TokenType mTokenType { get; set; }
        public string Value { get; set; }
        public string RemainingText { get; set; }
    }
}
