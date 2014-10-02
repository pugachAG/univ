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
            FiniteStateAutomaton automaton = RegExpAutomatonConverter.ConvertToAutomaton(this);
            return automaton.CheckRecognizable(str);
        } 
  
        public string ToStringWithParanteses()
        {
            return string.Format("({0})", ToString());
        }

    }

    
    public class SingleSymbolRegularExpression : RegularExpression
    {
        public SymbolBase Value { get; set; }

        public SingleSymbolRegularExpression(SymbolBase value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
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
        
        public override string ToString()
        {
            return "<0>";
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

        public override string ToString()
        {
            string left = this.Left.ToString();
            string right = this.Right.ToString();

            if (Left is AlternationRegularExpression)
                left = Left.ToStringWithParanteses();
            if (Right is AlternationRegularExpression)
                right = Right.ToStringWithParanteses();
            return left + right;
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

        public static RegularExpression Create(RegularExpression left, RegularExpression right)
        {
            if (left is EmptySetRegularExpression)
                return right;
            if (right is EmptySetRegularExpression)
                return left;
            return new AlternationRegularExpression(left, right);
        }
        
        public override string ToString()
        {
            string left = this.Left.ToString();
            string right = this.Right.ToString();
            return left + "+" + right;
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

        public override string ToString()
        {
            if (this.BaseExpression is SingleSymbolRegularExpression)
                return this.BaseExpression.ToString() + "*";
            if (this.BaseExpression is KleeneStarRegularExpression)
                return this.BaseExpression.ToString();
            return this.BaseExpression.ToStringWithParanteses() + "*";

        }
    }
}
