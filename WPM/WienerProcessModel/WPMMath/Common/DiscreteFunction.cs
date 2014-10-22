using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMMath.Common
{
    public struct ArgValue
    {
        private decimal argument;
        private decimal value;

        public ArgValue(double arg, double val)
            : this((decimal)arg, (decimal)val)
        {
        }

        public ArgValue(decimal arg, decimal val)
        {
            argument = arg;
            value = val;
        }

        public decimal Argument { get { return argument; } }
        public decimal Value { get { return value; } }

    }

    public class DiscreteFunction
    {
        private List<ArgValue> data = new List<ArgValue>();

        public IEnumerable<ArgValue> Data
        {
            get
            {
                return data.OrderBy(p => p.Argument);
            }
        }

        public void AddValue(decimal x, decimal y)
        {
            data.Add(new ArgValue(x, y));
        }
    }
}
