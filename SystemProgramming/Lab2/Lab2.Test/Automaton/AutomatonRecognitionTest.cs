using Lab2.Automaton;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Test.Automaton
{
    [TestClass]
    public class AutomatonRecognitionTest
    {
        const string FiniteAutomatonTestDefinitionPath = "Assets//Automaton1Definition.txt";
        const string FiniteAutomatonTestTestsPath = "Assets//Automaton1Tests.txt";

        [TestMethod]
        public void TestFiniteStateAutomaton()
        {
            string[] automatonData = File.ReadAllLines(FiniteAutomatonTestDefinitionPath);

        }


        IAutomaton ReadAutomaton(string fname)
        {
            string[] automatonData = File.ReadAllLines(fname);

            return null;
        }
    }
}
