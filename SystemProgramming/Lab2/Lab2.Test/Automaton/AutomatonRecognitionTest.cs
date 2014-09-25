using Lab2.Automata;
using Lab2.Common;
using Lab2.IO;
using Lab2.RegularExpressions;
using Lab2.Test.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Test.Automata
{
    [TestClass]
    public class AutomatonRecognitionTest
    {
        [TestMethod]
        public void AutomatonRecognition()
        {
            string[] automatonDefsPaths = AssetsPathHepler.GetAssetsAutomatonDefinitionsPaths();
            string[] automatonTestsPaths = AssetsPathHepler.GetAssetsAutomatonTestsPaths();
            int testsCount = automatonDefsPaths.Length;
            for (int i = 0; i < testsCount; i++)
            {
                IAutomaton automaton = ReadAutomaton(automatonDefsPaths[i]);
                RegularExpression regex = AutomatonToRegExConvert.ConvertToRegularExpression((FiniteStateAutomaton)automaton); 
                Dictionary<string, bool> tests = ReadTests(automatonTestsPaths[i]);
                foreach (var pair in tests)
                {
                    bool automatonResult = automaton.CheckRecognizable(pair.Key);
                    if(automatonResult != pair.Value)
                    {
                        Assert.Fail("Automaton recognition failed on test {0}, test string {1}", i + 1, pair.Key);
                    }
                    bool regexResult = regex.IsMatch(pair.Key);
                    if (regexResult != pair.Value)
                    {
                        Assert.Fail("Regular Expression recognition failed on test {0}, test string {1}", i + 1, pair.Key);
                    }
                }
            }

        }


        private IAutomaton ReadAutomaton(string fname)
        {
            string[] lines = File.ReadAllLines(fname);
            return AutomatonReader.ReadAutomaton(lines);
        }

        public Dictionary<string, bool> ReadTests(string fname)
        {
            string[] lines = File.ReadAllLines(fname);
            return AutomatonReader.ReadTests(lines);
        }
    }
}
