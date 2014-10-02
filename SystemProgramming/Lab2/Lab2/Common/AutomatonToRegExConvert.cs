using Lab2.Automata;
using Lab2.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Common
{
    public static class AutomatonToRegExConvert
    {
        public static RegularExpression StateRemovalMethod(FiniteStateAutomaton automaton)
        {
            FiniteStateAutomaton regexAutomaton = CloneAutomaton(automaton);
            foreach (var state in regexAutomaton.GetAllStates().Where(st => !(st.IsFinish || st.IsStart)))
            {
                var currentStates = regexAutomaton.GetAllStates().Where(st => st != state);
                RegexLabel selfLabel = state.GetLabelsToState(state).FirstOrDefault() as RegexLabel;
                foreach (var st1 in currentStates)
                {
                    foreach (var st2 in currentStates)
                    {
                        RegexLabel from = st1.GetLabelsToState(state).FirstOrDefault() as RegexLabel;
                        RegexLabel to = state.GetLabelsToState(st2).FirstOrDefault() as RegexLabel;
                        if (from != null && to != null)
                        {
                            RegularExpression lblRegex = from.Regex;
                            if (selfLabel != null)
                            {
                                RegularExpression selfRegex = new KleeneStarRegularExpression(selfLabel.Regex);
                                lblRegex = new ConcatenationRegularExpression(lblRegex, selfRegex);
                            }
                            lblRegex = new ConcatenationRegularExpression(lblRegex, to.Regex);
                            RegexLabel lblFromTo = st1.GetLabelsToState(st2).FirstOrDefault() as RegexLabel;
                            if (lblFromTo != null)
                            {
                                lblRegex = new AlternationRegularExpression(lblFromTo.Regex, lblRegex);
                            }
                            st1.RemoveAllTransitionsTo(st2);
                            st1.AddNewTransition(new RegexLabel(lblRegex), st2);
                        }
                    }
                }
                regexAutomaton.RemoveState(state);
            }
            return null;
        }

        private static FiniteStateAutomaton CloneAutomaton(FiniteStateAutomaton automaton)
        {
            FiniteStateAutomaton regexAutomaton = new FiniteStateAutomaton();
            Dictionary<StateDescription, StateDescription> oldStatesToNew = new Dictionary<StateDescription, StateDescription>();
            foreach (var state in automaton.GetAllStates())
            {
                StateDescription newState = new StateDescription("");
                newState.IsStart = state.IsStart;
                newState.IsFinish = state.IsFinish;
                oldStatesToNew[state] = newState;
                regexAutomaton.AddNewState(newState);
            }

            foreach (var state in automaton.GetAllStates())
            {
                StateDescription newState = oldStatesToNew[state]; 
                foreach (var st in automaton.GetAllStates())
                {
                    HashSet<SymbolBase> symbols = state.GetLabelsToState(st);
                    RegularExpression regex = null;
                    foreach (var sb in symbols)
                    {
                        RegularExpression symbolRegex = new SingleSymbolRegularExpression(sb);
                        regex = (regex == null) ? symbolRegex : new AlternationRegularExpression(regex, symbolRegex);
                    }
                    RegexLabel label = new RegexLabel(regex);
                    StateDescription newSt = oldStatesToNew[st];
                    newState.AddNewTransition(label, newSt);
                }
            }
            return regexAutomaton;
        }
    }
}
