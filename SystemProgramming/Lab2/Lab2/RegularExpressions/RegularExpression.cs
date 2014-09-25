using Lab2.Common;
using Lab2.Automata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Lab2.RegularExpressions
{
    public class RegularExpression
    {
        public bool IsMatch(string str)
        {
            FiniteStateAutomaton automaton = RegExtAutomatonConvertor.ConvertToAutomaton(this);
            return automaton.CheckRecognizable(str);
        }   

    }

    
    public class SingleSymbolRegularExpression : RegularExpression
    {
        public SymbolBase Value { get; set; }

        public SingleSymbolRegularExpression(SymbolBase value)
        {
            this.Value = value;
        }
    }

   
    public class EmptySetRegularExpression : RegularExpression
    {
        private static EmptySetRegularExpression emptySet = new EmptySetRegularExpression();
        
        public static EmptySetRegularExpression EmptySet
        {
            get
            {
                return emptySet;
            }
        }
        
        private EmptySetRegularExpression()
        {
        }
    }


    public class ConcatenationRegularExpression : RegularExpression
    {
        public RegularExpression Left { get; set; }
        public RegularExpression Right { get; set; }

        public ConcatenationRegularExpression (RegularExpression left, RegularExpression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            this.Left = left;
            this.Right = right;
        }
    }

    
    public class AlternationRegularExpression : RegularExpression
    {
        public RegularExpression Left { get; set; }
        public RegularExpression Right { get; set; }

        public AlternationRegularExpression (RegularExpression left, RegularExpression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            this.Left = left;
            this.Right = right;

        }
    }


    public class KleeneStarRegularExpression : RegularExpression
    {
        public RegularExpression BaseExpression { get; set; }

        public KleeneStarRegularExpression(RegularExpression baseExpression)
        {
            if (baseExpression == null)
                throw new ArgumentNullException();
            this.BaseExpression = baseExpression;
        }
    }
}
