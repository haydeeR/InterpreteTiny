using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    class GRegex
    {
        public static readonly string id = @"_[a-zA-Z0-9]+";
        public static readonly string cadena = @"""([a-zA-Z0-9]|(:|;|,|\.|\[|\]|\*|\+|\?|¿|¡|!|#|%|&|/|~))+""";
        public static readonly string numero = @"\d+";
        public static readonly string keywords = @"(if|else|endif|read|write|repeat|until|:=|Var|var)";
        public static readonly string opComp = @"(==|<|>)";
        public static readonly string opSuma = @"(\+|-)";
        public static readonly string opMult = @"(\*|/)";
        public static readonly string opPote = @"\^";
        public static readonly string abreParent = @"\(";
        public static readonly string cierraParent = @"\)";
        public static readonly string abreLlaves = @"\{";
        public static readonly string cierraLlaves = @"\}";
        public static readonly string finInstruccion = @";";
        public static readonly string separatorComma = @",";
        public static readonly string identificadores = @"(\s)*" + id + @"(\s)*(,(\s)*" + id + @"(\s)*)*";



        public static readonly string declarePattern = @"^(((Var)|(var))\{(int|float)(\s)*" + id + @"(\s)*(,(\s)*(" + id + @"))*\};?)$";
        public static readonly string readPattern = @"^(read\(" + id + @"\);?)$";
        public static readonly string printPattern = @"(" + cadena + @"," + identificadores + @")|(" + cadena + @")|(" + identificadores + @")";
        public static readonly string writePattern = @"^(((write)\()(" + printPattern + @")(\);?))$";


        public static readonly string factor = @"((" + numero + @")|(" + id + @"))";
        public static readonly string potenciaAux = @"(" + factor + @"(\s)*\^(\s)*" + factor + @")";
        public static readonly string potencia = @"(" + potenciaAux + @")|(" + factor + @")";
        public static readonly string termAux = @"(" + potencia + @"(\s)*" + opMult + @"(\s)*" + potencia + @")";
        public static readonly string term = @"(" + termAux + @")|(" + potencia + @")";

        public static readonly string expSimpleAux = @"(" + term + @"(\s)*" + opSuma + @"(\s)*" + term + @")";
        public static readonly string expSimple = @"(" + expSimpleAux + @")|(" + term + @")";
        public static readonly string exp = @"(" + expSimple + @"(\s)*" + opComp + @"(\s)*" + expSimple + @")|(" + expSimple + @")";

        public static readonly string assignPattern = @"^((" + id + @"):=(" + exp + ");?)$";

        public static readonly string ifPattern = @"^((if\()(" + exp + @")(\)\{))$";
        public static readonly string elseIfPattern = @"^(\}?else\{)$";
        public static readonly string endIfPattern = @"^(\}?endif;?)$";
        public static readonly string repeatStart = @"^(repeat{)$";
        public static readonly string repeatEnd = @"^((\})?(until\()(" + exp + @")(\);?)" + @")$";
        public static readonly string endSentencesBlock = @"^" + cierraLlaves + "$";
    }
}
