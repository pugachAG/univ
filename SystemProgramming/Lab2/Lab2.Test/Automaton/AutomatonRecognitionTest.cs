using Lab2.Automaton;
using Lab2.Test.Assets;
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
        [TestMethod]
        public void AutomatonRecognition()
        {
            string[] automatonDefsPaths = AssetsPathHepler.GetAssetsAutomatonDefinitionsPaths();
            string[] automatonTestsPaths = AssetsPathHepler.GetAssetsAutomatonTestsPaths();
            int testsCount = automatonDefsPaths.Length;
            for (int i = 0; i < testsCount; i++)
            {
                IAutomaton automaton = ReadAutomaton(automatonDefsPaths[i]);
                Dictionary<string, bool> tests = ReadTests(automatonTestsPaths[i]);
                foreach (var pair in tests)
                {
                    bool automatonResult = automaton.CheckRecognizable(pair.Key);
                    Assert.IsTrue(automatonResult == pair.Value, "Automaton word recognition result should be the same as in predefined tests");
                }
            }

        }


        private IAutomaton ReadAutomaton(string fname)
        {
            IIOAutomatonBuilder builder = AutomatonBuilderFactory.CreateAutomatonBuilder();
            string[] lines = File.ReadAllLines(fname);
            int[] finishStates = lines[0].Split(' ').Select(str => int.Parse(str.Trim())).ToArray();
            HashSet<int> states = new HashSet<int>();
            Action<int> addStateAction = k =>
                {
                    if (!states.Contains(k))
                    {
                        states.Add(k);
                        builder.AddState(k);
                    }
                };
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ').Select(str => str.Trim()).ToArray();
                int from = int.Parse(line[0]);
                addStateAction(from);
                int to = int.Parse(line[1]);
                addStateAction(to);
                char? label = line[2] == "eps" ? null : new char?(line[2][0]);
                builder.AddTransition(from, to, label);
            }
            return builder.GetAutomaton();
        }

        public Dictionary<string, bool> ReadTests(string fname)
        {
            string[] lines = File.ReadAllLines(fname);
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(' ').Select(str => str.Trim()).ToArray();
                bool val = lineParts[1] != "0";
                result[lineParts[0]] = val;
            }
            return result;
        }
    }
}
