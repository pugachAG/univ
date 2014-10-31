using Lab2.Automata;
using Lab2.Common;
using Lab2.IO;
using Lab2.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        public const string FileName = @"D:\test.txt";

        static void Main(string[] args)
        {
            //Logger.NewStringEvent += Console.WriteLine;
            //Variant18();
            //Console.WriteLine();
            Variant9();
            Console.ReadLine();
        }

        static void Variant3()
        {
            FiniteStateAutomaton automaton = ReadAutomaton();
            while (true)
            {
                Console.WriteLine("Input string:");
                string str = Console.ReadLine();
                Console.WriteLine("Recognition result: {0}", automaton.CheckRecognizable(str));
            }
        }

        static void Variant18()
        {
            List<string> allRegexs = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                FiniteStateAutomaton automaton = ReadAutomaton(i);
                RegularExpression regex = AutomatonToRegExConvert.StateRemovalMethod(automaton);
                Console.WriteLine(regex.ToString());
                allRegexs.Add(regex.ToString());
            }
            File.WriteAllLines(@"D:\out.txt", allRegexs.ToArray());
        }

        static void Variant9()
        {
            FiniteStateAutomaton automaton = ReadAutomaton();
            int k;
            Console.WriteLine("Enter k:");
            bool isParsed = int.TryParse(Console.ReadLine(), out k);
            if (isParsed)
            {
                var words = GeneralHelper.GetAllWords(k, automaton.Alphabet);
                bool ok = true;
                foreach(var word in words)
                {
                    bool recognize = automaton.CheckRecognizable(word);
                    ok &= recognize;
                    Console.WriteLine("Check word {0}, result {1}", word, recognize);
                }
                Console.WriteLine("Automaton DO {0}RECOGNOZE all words with length equals {1}", ok ? "" : "NOT ", k);
            }
            else
                Console.WriteLine("Wrong!!!");
        }


        static FiniteStateAutomaton ReadAutomaton(int i = 0)
        {
            IAutomaton automaton = null;
            try
            {
                string fname = FileName;
                string[] lines = System.IO.File.ReadAllLines(fname);
                automaton = AutomatonReader.ReadAutomaton(lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return automaton as FiniteStateAutomaton;
        }

    }
}
