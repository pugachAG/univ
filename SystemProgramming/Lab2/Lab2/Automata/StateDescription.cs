using Lab2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automata
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
            HashSet<StateDescription> res = new HashSet<StateDescription>();
            if (this.transitions.ContainsKey(symbol))
            {
                foreach (StateDescription st in this.transitions[symbol])
                    res.UnionWith(st.StateClosure());

            }
            return res;
        }

        public HashSet<StateDescription> StateClosure()
        {
            HashSet<StateDescription> stateClosure = new HashSet<StateDescription>();
            HashSet<StateDescription> currentLevelStates = new HashSet<StateDescription>();
            currentLevelStates.Add(this);

            while (currentLevelStates.Count != 0)
            {
                stateClosure.UnionWith(currentLevelStates);
                HashSet<StateDescription> newStates = new HashSet<StateDescription>();
                foreach (StateDescription st in currentLevelStates)
                {
                    if (st.transitions.ContainsKey(EpsilonSymbol.Instance))
                        newStates.UnionWith(st.transitions[EpsilonSymbol.Instance]);
                }
                newStates.ExceptWith(stateClosure);
                currentLevelStates = newStates;
            }
            return stateClosure;
        }

        public HashSet<SymbolBase> GetLabelsToState(StateDescription state)
        {
            HashSet<SymbolBase> result = new HashSet<SymbolBase>();
            foreach (var pair in transitions)
                foreach (var st in pair.Value)
                    if (st == state)
                    {
                        result.Add(pair.Key);
                    }
            return result;
        }

        public void RemoveAllTransitionsTo(StateDescription state)
        {
            foreach (var pair in transitions)
            {
                pair.Value.Remove(state);
            }
        }
    }
}
