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
            throw new NotImplementedException();
        }
    }
}