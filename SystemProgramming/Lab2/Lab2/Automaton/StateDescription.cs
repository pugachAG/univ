using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public class StateDescription
    {
        private Dictionary<SymbolBase, HashSet<StateDescription>> transitions = new Dictionary<SymbolBase, HashSet<StateDescription>>();

        public string Name { get; set; }
        public bool IsStart { get; set; }
        public bool IsFinish { get; set; }

        public StateDescription(string name)
        {
            this.Name = name;
            this.IsStart = false;
            this.IsFinish = false;
        }

        public void AddNewTransition(SymbolBase label, StateDescription to)
        {
            if (!this.transitions.ContainsKey(label))
                this.transitions[label] = new HashSet<StateDescription>();
            this.transitions[label].Add(to);
        }

        public HashSet<StateDescription> FindNextStatesBySymbol(SymbolBase symbol)
        {
            if (this.transitions.ContainsKey(symbol))
                return this.transitions[symbol];
            return new HashSet<StateDescription>();
        }
    }
}
