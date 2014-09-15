﻿using Lab2.Automaton;
using Lab2.IO;
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
