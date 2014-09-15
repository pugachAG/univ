using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public abstract class SybmolBase
    {
    }

    public class CharSybmol : SybmolBase
    {
        public char Value { get; set; }

        public CharSybmol(char value)
        {
            this.Value = value;
        }
    }

    public class EpsilonSymbol : SybmolBase
    {
    }
}
