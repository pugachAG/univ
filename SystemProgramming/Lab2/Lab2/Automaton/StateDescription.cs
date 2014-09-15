using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public class StateDescription
    {
        public string Name { get; set; }
        public List<Transition> Transitions { get; private set; }
        public bool IsStart { get; set; }
        public bool IsFinish { get; set; }

        public StateDescription(string name)
        {
            this.Name = name;
            this.Transitions = new List<Transition>();
            this.IsStart = false;
            this.IsFinish = false;
        }
    }
}
