using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2.Common;
using Lab2.RegularExpressions;

namespace Lab2.Test.RegularExpressions
{
    [TestClass]
    public class RegexMatchTester
    {
        private SymbolBase CreateSymbol(char ch)
        {
            return new CharSymbol(ch);
        }

        [TestMethod]
        public void TestRegexMatch1()
        {
            //(a+b)*c
            SingleSymbolRegularExpression regexA = new SingleSymbolRegularExpression(CreateSymbol('a'));
            SingleSymbolRegularExpression regexB = new SingleSymbolRegularExpression(CreateSymbol('b'));
            SingleSymbolRegularExpression regexC = new SingleSymbolRegularExpression(CreateSymbol('c'));
            AlternationRegularExpression unionAB = new AlternationRegularExpression(regexA, regexB);
            KleeneStarRegularExpression starUnionAB = new KleeneStarRegularExpression(unionAB);
            ConcatenationRegularExpression concat = new ConcatenationRegularExpression(starUnionAB, regexC);

            Func<string, bool> isMatch = new Func<string, bool>(str => concat.IsMatch(str));

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

            foreach(string str in acceptable)
                Assert.IsTrue(isMatch(str), str);

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

            foreach (string str in unacceptable)
                Assert.IsFalse(isMatch(str), str);
            
        }

        [TestMethod]
        public void TestRegexMatch2()
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

            foreach (string str in acceptable)
                Assert.IsTrue(isMatch(str), str);

            string[] unacceptable = new string[]
            {
                "aca",
                "abcda",
                "aaaaaaaaaabbbbbbbbbbccccccccccccccdb",
                "asadfldjfahsldkfjhasldkjfhsalkdjfhalskdjfhaslkdjfhaslkdjfhlaskdjfhlaskdfhaskljdfhlkc",
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae"
            };

            foreach (string str in unacceptable)
                Assert.IsFalse(isMatch(str), str);

        }
    }
}
