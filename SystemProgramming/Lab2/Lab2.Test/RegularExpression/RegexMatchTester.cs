using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2.Common;
using Lab2.RegularExpressions;
using System.Collections.Generic;

namespace Lab2.Test.RegularExpressions
{
    [TestClass]
    public class RegexMatchTester
    {
        private class TestCase
        {
            public string StringValue { get; set; }
            public RegularExpression Regexp { get; set; }

            public List<string> AcceptableStrings { get; private set; }

            public List<string> UnacceptableStrings { get; private set; }

            public TestCase()
            {
                AcceptableStrings = new List<string>();
                UnacceptableStrings = new List<string>();
            }

            private static SymbolBase CreateSymbol(char ch)
            {
                return new CharSymbol(ch);
            }

            public static TestCase CreateTestCase1()
            {
                //(a+b)*c
                SingleSymbolRegularExpression regexA = new SingleSymbolRegularExpression(CreateSymbol('a'));
                SingleSymbolRegularExpression regexB = new SingleSymbolRegularExpression(CreateSymbol('b'));
                SingleSymbolRegularExpression regexC = new SingleSymbolRegularExpression(CreateSymbol('c'));
                AlternationRegularExpression unionAB = new AlternationRegularExpression(regexA, regexB);
                KleeneStarRegularExpression starUnionAB = new KleeneStarRegularExpression(unionAB);
                ConcatenationRegularExpression concat = new ConcatenationRegularExpression(starUnionAB, regexC);

                string[] acceptable = new string[]
                {
                    "c",
                    "ac",
                    "abc",
                    "aac",
                    "abac",
                    "abababbabababbbababbabababababababbabababac",
                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaac"
                };

                string[] unacceptable = new string[]
                {
                    "a",
                    "cc",
                    "aab",
                    "ababab",
                    "caaaaaaaac",
                    "asdasd",
                    "aaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
                    "ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc",
                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaac",
                    "asadfldjfahsldkfjhasldkjfhsalkdjfhalskdjfhaslkdjfhaslkdjfhlaskdjfhlaskdfhaskljdfhlkc"
                };

                TestCase result = new TestCase()
                {
                    Regexp = concat,
                    AcceptableStrings = new List<string>(acceptable),
                    UnacceptableStrings = new List<string>(unacceptable),
                    StringValue = "(a+b)*c"
                };

                return result;
            }

            public static TestCase CreateTestCase2()
            {
                //(a+b)*(c+d)*
                SingleSymbolRegularExpression regexA = new SingleSymbolRegularExpression(CreateSymbol('a'));
                SingleSymbolRegularExpression regexB = new SingleSymbolRegularExpression(CreateSymbol('b'));
                SingleSymbolRegularExpression regexC = new SingleSymbolRegularExpression(CreateSymbol('c'));
                SingleSymbolRegularExpression regexD = new SingleSymbolRegularExpression(CreateSymbol('d'));
                AlternationRegularExpression unionAB = new AlternationRegularExpression(regexA, regexB);
                AlternationRegularExpression unionCD = new AlternationRegularExpression(regexC, regexD);
                KleeneStarRegularExpression starUnionAB = new KleeneStarRegularExpression(unionAB);
                KleeneStarRegularExpression starUnionCD = new KleeneStarRegularExpression(unionCD);
                ConcatenationRegularExpression concat = new ConcatenationRegularExpression(starUnionAB, starUnionCD);

                Func<string, bool> isMatch = new Func<string, bool>(str => concat.IsMatch(str));

                string[] acceptable = new string[]
                {
                    "c",
                    "ac",
                    "abc",
                    "aac",
                    "abac",
                    "abababbabababbbababbabababababababbabababac",
                    "dddddddddddddddddddddddddd",
                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaac",
                    "abababababaaaaaaaaaaaabababababbbbbabbabababbababababbabababccccccdddcdcddddddcdcdcdcdccccdcdcdcd",
                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbccccccccccccccccccccccccccccccccccdddddddddddddcccccccccccccccddddddddddddddddddcccccccccccdcdcdcdcdccccc"

                };

                string[] unacceptable = new string[]
                {
                    "aca",
                    "abcda",
                    "aaaaaaaaaabbbbbbbbbbccccccccccccccdb",
                    "asadfldjfahsldkfjhasldkjfhsalkdjfhalskdjfhaslkdjfhaslkdjfhlaskdjfhlaskdfhaskljdfhlkc",
                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae"
                };

                TestCase result = new TestCase()
                {
                    Regexp = concat,
                    AcceptableStrings = new List<string>(acceptable),
                    UnacceptableStrings = new List<string>(unacceptable),
                    StringValue = "(a+b)*(c+d)*"
                };

                return result;
            }

            public static TestCase[] GetAllTestCases()
            {
                return new TestCase[]
                {
                    CreateTestCase1(),
                    CreateTestCase2()
                };
            }
        }

       

        [TestMethod]
        public void TestRegexMatch()
        {
            TestCase[] testCases = TestCase.GetAllTestCases();

            foreach(TestCase testCase in testCases)
            {
                foreach(string str in testCase.AcceptableStrings)
                    Assert.IsTrue(testCase.Regexp.IsMatch(str), "Failed match test on " + str);

                foreach (string str in testCase.UnacceptableStrings)
                    Assert.IsFalse(testCase.Regexp.IsMatch(str), "Failed does not match test on" + str);
            }
            
        }

        [TestMethod]
        public void TestRegexToSting()
        {
            
            TestCase[] testCases = TestCase.GetAllTestCases();

            foreach (TestCase testCase in testCases)
            {
                string actual = testCase.Regexp.ToString();
                string expected = testCase.StringValue;
                Assert.AreEqual(expected, actual, string.Format("Failed regex to string test {0}, got {1}", expected, actual));
            }
        }

    }
}
