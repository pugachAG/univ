using Lab2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Lab2.RegularExpressions
{
    public abstract class RegularExpression
    {
        public abstract bool IsMatch(string str);
    }

    
    public class SingleSymbolRegularExpression : RegularExpression
    {
        public SymbolBase Value { get; set; }

        public override bool IsMatch(string str)
        {
            throw new NotImplementedException();
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

        public override bool IsMatch(string str)
        {
            return false;
        }
    }


    public class ConcatenationRegularExpression : RegularExpression
    {
        private RegularExpression left = null;
        private RegularExpression right = null;

        public ConcatenationRegularExpression (RegularExpression left, RegularExpression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            this.left = left;
            this.right = right;

        }

        public override bool IsMatch(string str)
        {
            throw new NotImplementedException();
        }
    }

    
    public class AlternationRegularExpression : RegularExpression
    {
        private RegularExpression left = null;
        private RegularExpression right = null;

        public AlternationRegularExpression (RegularExpression left, RegularExpression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            this.left = left;
            this.right = right;

        }

        public override bool IsMatch(string str)
        {
            throw new NotImplementedException();
        }
    }


    public class KleeneStarRegularExpression : RegularExpression
    {
        private RegularExpression baseExpression;

        public KleeneStarRegularExpression(RegularExpression baseExpression)
        {
            if (baseExpression == null)
                throw new ArgumentNullException();
            this.baseExpression = baseExpression;
        }

        public override bool IsMatch(string str)
        {
            throw new NotImplementedException();
        }
    }
}
