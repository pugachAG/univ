using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    class InvalidAutomatonStructureException : Exception
    {
        public InvalidAutomatonStructureException() 
            : base()
        {
        }

        public InvalidAutomatonStructureException(string message)
            : base(message)

        {
        }

        public override string Message
        {
            get
            {
                return "Invalid Automaton Structure: " + base.Message;
            }
        }
    }
}
