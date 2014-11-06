using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Lab2.Common;

namespace Lab2.Test.Common
{
    [TestClass]
    public class GeneralHelperTester
    {
        [TestMethod]
        public void TestGetAllWords()
        {
            HashSet<char> all = new HashSet<char>() { 'a', 'b' };
            var res = GeneralHelper.GetAllWords(3, all);
            Assert.IsTrue(res.Contains("aab"));
            Assert.IsTrue(res.Contains("aba"));
            Assert.IsTrue(res.Contains("bba"));
        }
    }
}
