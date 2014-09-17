using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public abstract class SymbolBase
    {
    }

    public class CharSybmol : SymbolBase
    {
        public char Value { get; set; }

        public CharSybmol(char value)
        {
            this.Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CharSybmol ch = obj as CharSybmol;
            if(ch != null)
                return Value.Equals(ch.Value);
            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }

    public class EpsilonSymbol : SymbolBase
    {
    }
}
