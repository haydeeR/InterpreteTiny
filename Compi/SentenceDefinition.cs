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
            var match = mRegex.Match(inputString);

            if (match.Success)
            {
                string remainingText = string.Empty;
                if (match.Length != inputString.Length)
                    remainingText = "";

                return new SentenceMatch()
                {
                    isMatch = true,
                    RemainingText = remainingText,
                    mSentenceType = sentenceType,
                    Value = inputString
                };
            }
            else
            {
                return new SentenceMatch() { isMatch = false };
            }
        }


    }



    public class SentenceMatch
    {
        public bool isMatch { get; set; }
        public SentenceType mSentenceType { get; set; }
        public string Value { get; set; }
        public string RemainingText { get; set; }
    }


    public class DslSentence
    {
        public SentenceType sentenceType { get; set; }
        public string value { get; set; }

        public DslSentence(SentenceType sentenceType)
        {
            this.sentenceType = sentenceType;
            this.value = string.Empty;
        }


        public DslSentence(SentenceType sentenceType, string value)
        {
            this.sentenceType = sentenceType;
            this.value = value;
        }


        public DslSentence Clone()
        {
            return new DslSentence(this.sentenceType, this.value);
        }
    }
}
