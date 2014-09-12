using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Automaton
{
    public interface IAutomaton
    {
        /// <summary>
        /// True if automaton accepts input word
        /// </summary>
        /// <param name="word">input word</param>
        /// <returns></returns>
        bool CheckRecognizable(string word);
    }
}
