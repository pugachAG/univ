using Lab2.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Common
{
    public class RegexLabel : SymbolBase
    {
        public RegularExpression Regex { get; private set; }

        public RegexLabel(RegularExpression regex)
        {
            this.Regex = regex;
        }

        public RegexLabel(SymbolBase symbol)
        {
            this.Regex = new SingleSymbolRegularExpression(symbol);
        }
    }
}
