using Lab2.Common;
using Lab2.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automata
{

    public class FiniteStateAutomaton : IAutomaton
    {
        private List<StateDescription> stateDescriptions = new List<StateDescription>();

        public void AddNewState(StateDescription newState)
        {
            if (newState == null)
                throw new ArgumentNullException();
            stateDescriptions.Add(newState);
        }

        public void RemoveState(StateDescription state)
        {
            if (state == null)
                throw new ArgumentNullException();
            stateDescriptions.Remove(state);
        }

        public List<StateDescription> GetAllStates()
        {
            return stateDescriptions;
        }
        
        public StateDescription FindByName(string name)
        {
            return stateDescriptions.FirstOrDefault(st => st.Name == name);
        }

        public StateDescription GetStart()
        {
            return stateDescriptions.FirstOrDefault(st => st.IsStart == true);
        }

        public List<StateDescription> GetFinishes()
        {
            return stateDescriptions.FindAll(st => st.IsFinish == true);
        }

        public bool CheckRecognizable(string word)
        {
            HashSet<StateDescription> currentStates = new HashSet<StateDescription>();
            StateDescription start = stateDescriptions.FirstOrDefault(st => st.IsStart);
            if (start == null)
                throw new InvalidAutomatonStructureException("Start State Is Missing");
            currentStates.UnionWith(start.StateClosure());
            Logger.AppendLine("Initial states set:");
            LogStates(currentStates);
            foreach (char ch in word)
            {
                Logger.AppendLine(string.Format("Processing symbol {0}:", ch));

                HashSet<StateDescription> newStates = new HashSet<StateDescription>();
                foreach (StateDescription st in currentStates)
                    newStates.UnionWith(st.FindNextStatesBySymbol(new CharSymbol(ch)));
                currentStates = newStates;

                Logger.AppendLine("New states set:");
                LogStates(newStates);
            }
            foreach (StateDescription st in currentStates)
            {
                if (st.IsFinish)
                    return true;
            }
            return false;
        }

        private void LogStates(IEnumerable<StateDescription> states)
        {
            StringBuilder statesStrBuilder = new StringBuilder();
            foreach (var state in states)
            {
                if (statesStrBuilder.Length > 0)
                    statesStrBuilder.Append("; ");
                statesStrBuilder.Append(state.Name);
            }
            if(statesStrBuilder.Length == 0)
            {
                statesStrBuilder.Append("Empty set");
            }
            Logger.AppendLine(statesStrBuilder.ToString());
        }
    }
}