using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{

    public class FiniteStateAutomaton : IAutomaton
    {
        private List<StateDescription> states = new List<StateDescription>();

        public void AddNewState(StateDescription newState)
        {
            if (newState == null)
                throw new ArgumentNullException();
            states.Add(newState);
        }
        
        public StateDescription FindByName(string name)
        {
            return states.FirstOrDefault(st => st.Name == name);
        }

        public bool CheckRecognizable(string word)
        {
            HashSet<StateDescription> currentStates = new HashSet<StateDescription>();
            StateDescription start = states.FirstOrDefault(st => st.IsStart);
            if (start == null)
                throw new InvalidAutomatonStructureException("Start State Is Missing");
            currentStates.UnionWith(start.StateClosure());
            foreach (char ch in word)
            {
                HashSet<StateDescription> newStates = new HashSet<StateDescription>();
                foreach (StateDescription st in currentStates)
                    newStates.UnionWith(st.FindNextStatesBySymbol(new CharSymbol(ch)));
                currentStates = newStates;
            }
            foreach (StateDescription st in currentStates)
            {
                if (st.IsFinish)
                    return true;
            }
            return false;
        }
    }
}