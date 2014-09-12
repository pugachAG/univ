using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Lab2.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string ss = "Assets//Automaton1Tests.txt";
            string ff = File.ReadAllText(ss);
        }
    }
}
