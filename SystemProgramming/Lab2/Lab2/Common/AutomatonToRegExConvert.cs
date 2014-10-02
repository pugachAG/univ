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
            while(regexAutomaton.GetAllStates().Any(st => !(st.IsFinish || st.IsStart)))
            {
                StateDescription state = regexAutomaton.GetAllStates().First(st => !(st.IsFinish || st.IsStart));
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
                                RegularExpression selfRegex = KleeneStarRegularExpression.Create(selfLabel.Regex);
                                lblRegex = ConcatenationRegularExpression.Create(lblRegex, selfRegex);
                            }
                            lblRegex = ConcatenationRegularExpression.Create(lblRegex, to.Regex);
                            RegexLabel lblFromTo = st1.GetLabelsToState(st2).FirstOrDefault() as RegexLabel;
                            if (lblFromTo != null)
                            {
                                lblRegex = AlternationRegularExpression.Create(lblFromTo.Regex, lblRegex);
                            }
                            st1.RemoveAllTransitionsTo(st2);
                            st1.AddNewTransition(new RegexLabel(lblRegex), st2);
                        }
                    }
                }
                regexAutomaton.RemoveState(state);
            }

            StateDescription startState = regexAutomaton.GetAllStates().Where(st => st.IsStart).First();
            StateDescription finishState = regexAutomaton.GetAllStates().Where(st => st.IsFinish).First();

            RegexLabel toFinish = startState.GetLabelsToState(finishState).FirstOrDefault() as RegexLabel;
            RegexLabel toStart = finishState.GetLabelsToState(startState).FirstOrDefault() as RegexLabel;
            RegexLabel selftStart = startState.GetLabelsToState(startState).FirstOrDefault() as RegexLabel;
            RegexLabel selfFinish = finishState.GetLabelsToState(finishState).FirstOrDefault() as RegexLabel;

            if (toFinish == null)
                return EmptySetRegularExpression.EmptySet;

            RegularExpression result = toFinish.Regex;
            if (selftStart != null || toStart != null)
            {
                RegularExpression current = selftStart != null ? selftStart.Regex : null;
                if (toStart != null)
                {
                    RegularExpression right = toFinish.Regex;
                    if (selfFinish != null)
                    {
                        right = ConcatenationRegularExpression.Create(right, KleeneStarRegularExpression.Create(selfFinish.Regex));
                    }
                    right = ConcatenationRegularExpression.Create(right, toStart.Regex);
                    current = current == null ?
                        right :
                        AlternationRegularExpression.Create(right, current);
                }
                result = ConcatenationRegularExpression.Create(KleeneStarRegularExpression.Create(current), result);
            }
            if (selfFinish != null)
            {
                result = ConcatenationRegularExpression.Create(result, KleeneStarRegularExpression.Create(selfFinish.Regex));
            }
            return result;
        }

        private static FiniteStateAutomaton CloneAutomaton(FiniteStateAutomaton automaton)
        {
            FiniteStateAutomaton regexAutomaton = new FiniteStateAutomaton();
            Dictionary<StateDescription, StateDescription> oldStatesToNew = new Dictionary<StateDescription, StateDescription>();
            foreach (var state in automaton.GetAllStates())
            {
                StateDescription newState = new StateDescription(state.Name);
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
                    StateDescription newSt = oldStatesToNew[st];
                    HashSet<SymbolBase> symbols = state.GetLabelsToState(st);
                    RegularExpression regex = null;
                    foreach (var sb in symbols)
                    {
                        RegularExpression symbolRegex = new SingleSymbolRegularExpression(sb);
                        regex = (regex == null) ? symbolRegex : AlternationRegularExpression.Create(regex, symbolRegex);
                    }
                    if (regex != null)
                    {
                        RegexLabel label = new RegexLabel(regex);
                        newState.AddNewTransition(label, newSt);
                    }
                }
            }
            return regexAutomaton;
        }
    }
}
