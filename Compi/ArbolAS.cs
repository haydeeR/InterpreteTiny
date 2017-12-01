using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class ArbolAS
    {
        List<List<DslToken>> tokens;
        List<DslSentence> sentences;
        List<NodoArblAS> raices;
        Stack<NodoArblAS> pilaNodos;

        public ArbolAS(List<List<DslToken>> theTokens, List<DslSentence> theSentences)
        {
            this.tokens = theTokens;
            this.sentences = theSentences;

            this.raices = new List<NodoArblAS>();
            this.pilaNodos = new Stack<NodoArblAS>();
        }


        public void creaArboles()
        {

        }



    }
}
