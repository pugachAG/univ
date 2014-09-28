using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.IO
{
    public class IOFix
    {
        static string[] files = new string[]
        {
            "Automaton1Definition.txt",
            "Automaton2Definition.txt",
            "Automaton3Definition.txt",
        };

        static string prefix = @"D:\Dev\univ\SystemProgramming\Lab2\Lab2.Test\Assets\";

        public static void Fix()
        {
            foreach (string file in files)
            {
                string fullPath = prefix + file;
                string[] lines = File.ReadAllLines(fullPath);
                for (int i = 4; i < lines.Length; i++)
                {
                    string[] ln = lines[i].Split(' ');
                    lines[i] = string.Format("{0} {1} {2}", ln[0], ln[2], ln[1]);
                }
                File.WriteAllLines(fullPath, lines);
            }
        }
    }
}
