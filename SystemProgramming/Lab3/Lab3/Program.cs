using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        const string inputFileName = "input.txt";
        static readonly Dictionary<LexemType, ConsoleColor> Colors = new Dictionary<LexemType, ConsoleColor>()
            {
                { LexemType.Unknown, ConsoleColor.Red },
                { LexemType.ReservedWord, ConsoleColor.Blue },
                { LexemType.Space, ConsoleColor.Black },
            };


        static void Main(string[] args)
        {
            string text = File.ReadAllText(inputFileName);
            var res = new Core().Process(text);
            foreach(var lexem in res)
            {
                Console.ForegroundColor = Colors[lexem.LexemType];
                Console.Write(lexem.Content);
                //Console.Write("\n\rASD");
            }
            Console.ReadLine();
        }
    }
}
