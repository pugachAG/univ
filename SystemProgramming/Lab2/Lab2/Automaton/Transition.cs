using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public class Transition
    {
        public SymbolBase Label { get; set; }
        public StateDescription Tale { get; set; }

        public Transition(SymbolBase label, StateDescription tale)
        {
            this.Label = label;
            this.Tale = tale;
        }
    }
}
