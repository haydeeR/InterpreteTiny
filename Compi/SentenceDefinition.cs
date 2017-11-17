using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Compi
{
    public class SentenceDefinition
    {
        private Regex mRegex;
        private readonly SentenceType sentenceType;

        public SentenceDefinition(SentenceType mSentenceType, string mRegexPattern)
        {
            mRegex = new Regex(mRegexPattern, RegexOptions.IgnoreCase);
            sentenceType = mSentenceType;
        }


        public SentenceMatch Match(string inputString)
        {
            return null;
        }
    }



    public class SentenceMatch
    {
        public bool isMatch { get; set; }
        public SentenceType mSentenceType { get; set; }
        public string Value { get; set; }
        public string RemainingText { get; set; }
    }
}
