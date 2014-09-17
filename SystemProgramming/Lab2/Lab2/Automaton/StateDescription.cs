using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public class StateDescription
    {
        private List<Transition> transitions = new List<Transition>();

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
            this.transitions.Add(new Transition(label, to));
        }

        public StateDescription FindNextStateBySymbol(SymbolBase symbol)
        {
            Transition labelTransition = transitions.FirstOrDefault(tr => tr.Label.Equals(symbol));
            if (labelTransition != null)
            {
                return labelTransition.Tale;
            }
            return null;
        }
    }
}
