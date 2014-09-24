using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Common
{
    public abstract class SymbolBase
    {
    }

    public class CharSymbol : SymbolBase
    {
        public char Value { get; set; }

        public CharSymbol(char value)
        {
            this.Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CharSymbol ch = obj as CharSymbol;
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
        private static EpsilonSymbol instance = new EpsilonSymbol();
        
        public static EpsilonSymbol Instance
        {
            get
            {
                return instance;
            }
        }
        
        private EpsilonSymbol()
        {
        }
        
    }
}
