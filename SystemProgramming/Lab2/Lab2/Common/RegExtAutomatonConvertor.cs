using Lab2.Automata;
using Lab2.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Common
{
    public static class RegExtAutomatonConvertor
    {
        private static FiniteStateAutomaton KleeneStarAutomaton(FiniteStateAutomaton automaton)
        {
            StateDescription oldStart = automaton.GetStart();
            if (oldStart == null)
                throw new NullReferenceException();
            oldStart.IsStart = false;
            StateDescription start = new StateDescription(string.Empty);
            automaton.AddNewState(start);
            start.IsStart = true;
            start.AddNewTransition(EpsilonSymbol.Instance, oldStart);
            StateDescription finish = new StateDescription(string.Empty);
            automaton.AddNewState(finish);
            start.AddNewTransition(EpsilonSymbol.Instance, finish);
            if (automaton.GetFinishes() == null)
                throw new NullReferenceException();
            foreach (StateDescription st in automaton.GetFinishes())
            {
                st.AddNewTransition(EpsilonSymbol.Instance, finish);
                st.AddNewTransition(EpsilonSymbol.Instance, oldStart);
                st.IsFinish = false;
            }
            return automaton;
        }


        private static FiniteStateAutomaton ConcatenationAutomaton(FiniteStateAutomaton left, FiniteStateAutomaton right)
        {
            FiniteStateAutomaton resAutomaton = new FiniteStateAutomaton();
            StateDescription start = new StateDescription("start");
            resAutomaton.AddNewState(start);
            start.IsStart = true;
            StateDescription finish = new StateDescription("finish");
            resAutomaton.AddNewState(finish);
            finish.IsFinish = true;
            StateDescription rightStart = right.GetStart();
            if (rightStart == null)
                throw new NullReferenceException();
            resAutomaton.AddNewState(rightStart);
            rightStart.IsStart = false;
            foreach (StateDescription st in left.GetAllStates())
            {
                resAutomaton.AddNewState(st);
                if (st.IsStart)
                {
                    st.IsStart = false;
                    start.AddNewTransition(EpsilonSymbol.Instance, st);
                }
                if (st.IsFinish)
                {
                    st.IsFinish = false;
                    st.AddNewTransition(EpsilonSymbol.Instance, rightStart);
                }
            }
            foreach (StateDescription st in right.GetAllStates())
            {
                resAutomaton.AddNewState(st);
                if (st.IsFinish)
                {
                    st.IsFinish = false;
                    st.AddNewTransition(EpsilonSymbol.Instance, finish);
                }
            }
            return resAutomaton;
        }


        private static FiniteStateAutomaton AlternationAutomaton (FiniteStateAutomaton left, FiniteStateAutomaton right)
        {
            FiniteStateAutomaton resAutomaton = new FiniteStateAutomaton();
            StateDescription start = new StateDescription("start");
            resAutomaton.AddNewState(start);
            start.IsStart = true;
            StateDescription finish = new StateDescription("finish");
            resAutomaton.AddNewState(finish);
            finish.IsFinish = true;
            foreach (StateDescription st in left.GetAllStates())
            {
                resAutomaton.AddNewState(st);
                if (st.IsStart)
                {
                    st.IsStart = false;
                    start.AddNewTransition(EpsilonSymbol.Instance, st);
                }
                if (st.IsFinish)
                {
                    st.IsFinish = false;
                    st.AddNewTransition(EpsilonSymbol.Instance, finish);
                }
            }
            foreach (StateDescription st in right.GetAllStates())
            {
                resAutomaton.AddNewState(st);
                if (st.IsStart)
                {
                    st.IsStart = false;
                    start.AddNewTransition(EpsilonSymbol.Instance, st);
                }
                if (st.IsFinish)
                {
                    st.IsFinish = false;
                    st.AddNewTransition(EpsilonSymbol.Instance, finish);
                }
            }
            return resAutomaton;
        }

        private static FiniteStateAutomaton BasicAutomaton(RegularExpression regExpression)
        {
            StateDescription start = new StateDescription("start");
            start.IsStart = true;
            StateDescription finish = new StateDescription("finish");
            finish.IsFinish = true;
            if (regExpression is SingleSymbolRegularExpression)
            {
                SingleSymbolRegularExpression singleSymbolExpression = (SingleSymbolRegularExpression)regExpression;
                start.AddNewTransition(singleSymbolExpression.Value, finish);
            }
            FiniteStateAutomaton result = new FiniteStateAutomaton();
            result.AddNewState(start);
            result.AddNewState(finish);
            return result;
        }

        public static FiniteStateAutomaton ConvertToAutomaton(RegularExpression regExpression)
        {
            FiniteStateAutomaton resAutomaton = new FiniteStateAutomaton();
            if (regExpression is EmptySetRegularExpression || regExpression is SingleSymbolRegularExpression)
            {
                return BasicAutomaton(regExpression);
            }
            if (regExpression is ConcatenationRegularExpression)
            {
                ConcatenationRegularExpression concantenationExpression = (ConcatenationRegularExpression)regExpression;
                FiniteStateAutomaton automaton1 = ConvertToAutomaton(concantenationExpression.Left);
                FiniteStateAutomaton automaton2 = ConvertToAutomaton(concantenationExpression.Right);
                return ConcatenationAutomaton(automaton1, automaton2);
            }
            if (regExpression is AlternationRegularExpression)
            {
                AlternationRegularExpression alternationExpression = (AlternationRegularExpression)regExpression;
                FiniteStateAutomaton automaton1 = ConvertToAutomaton(alternationExpression.Left);
                FiniteStateAutomaton automaton2 = ConvertToAutomaton(alternationExpression.Right);
                return AlternationAutomaton(automaton1, automaton2);
            }
            if (regExpression is KleeneStarRegularExpression)
            {
                KleeneStarRegularExpression kleeneStarExpression = (KleeneStarRegularExpression)regExpression;
                FiniteStateAutomaton baseAutomaton = ConvertToAutomaton(kleeneStarExpression.BaseExpression);
                return KleeneStarAutomaton(baseAutomaton);
            }
            return resAutomaton;
        }


    }
}
