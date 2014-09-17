using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public class AutomatonBuilder : IIOAutomatonBuilder
    {
        private FiniteStateAutomaton automaton = new FiniteStateAutomaton();
        

        public void AddState(int identifier)
        {
            StateDescription state = new StateDescription(identifier.ToString());
            automaton.AddNewState(state);
        }

        public void AddTransition(int from, int to, char? label)
        {
            StateDescription head = automaton.FindByName(from.ToString());
            StateDescription tale = automaton.FindByName(to.ToString());
            if (head == null || tale == null)
            {
                throw new ArgumentException();
            }
            SymbolBase symbol = null;
            
            if (label.HasValue)
            {
                symbol = new CharSymbol(label.Value);
            }
            else
            {
                symbol = new EpsilonSymbol();
            }
            head.AddNewTransition(symbol, tale);
        }

        public void SetStartState(int identifier)
        {
            StateDescription start = automaton.FindByName(identifier.ToString());
            if (start == null)
            {
                throw new ArgumentException();
            }
            start.IsStart = true;
        }

        public void SetFinishState(int identifier)
        {
            StateDescription finish = automaton.FindByName(identifier.ToString());
            if (finish == null)
            {
                throw new ArgumentException();
            }
            finish.IsFinish = true;
        }

        public IAutomaton GetAutomaton()
        {
            return automaton; 
        }

    }
}
