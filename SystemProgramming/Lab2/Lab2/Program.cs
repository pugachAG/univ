using Lab2.Automata;
using Lab2.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        public const string FileName = @"D:\Dev\univ\SystemProgramming\Lab2\Lab2.Test\Assets\Automaton1Definition.txt";

        static void Main(string[] args)
        {
            Variant3();
            Console.ReadLine();
        }

        static void Variant3()
        {
            IAutomaton automaton = null;
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileName);
                automaton = AutomatonReader.ReadAutomaton(lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            while (true)
            {
                Console.WriteLine("Input string:");
                string str = Console.ReadLine();
                Console.WriteLine("Recognition result: {0}", automaton.CheckRecognizable(str));
            }
        }

    }
}
