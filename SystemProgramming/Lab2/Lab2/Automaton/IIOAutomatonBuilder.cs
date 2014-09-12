﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public interface IIOAutomatonBuilder
    {
        void AddState(int identifier);

        void AddTransition(int from, int to, char? label);

        IAutomaton GetAutomaton();
    }
}
