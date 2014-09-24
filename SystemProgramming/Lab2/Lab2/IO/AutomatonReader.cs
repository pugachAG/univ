using Lab2.Automata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.IO
{
    public static class AutomatonReader
    {
        public static IAutomaton ReadAutomaton(string[] lines)
        {
            IIOAutomatonBuilder builder = new AutomatonBuilder();
            HashSet<int> states = new HashSet<int>();
            Action<int> tryAddState = k =>
            {
                if (!states.Contains(k))
                {
                    states.Add(k);
                    builder.AddState(k);
                }
            };
            //ignore first 2 lines
            int lineIndex = 2;
            //set start state
            int startStateIndentifier = ParseInt(lines[lineIndex]);
            tryAddState(startStateIndentifier);
            builder.SetStartState(startStateIndentifier);
            //go to the next line
            lineIndex++;
            //set finish states
            int[] finishStatesDescription = lines[lineIndex].Split(' ').Select(str => ParseInt(str)).ToArray();
            for (int i = 1; i < finishStatesDescription.Length; i++)
            {
                tryAddState(finishStatesDescription[i]);
                builder.SetFinishState(finishStatesDescription[i]);
            }

            lineIndex++;
            for (int i = lineIndex; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ').Select(str => str.Trim()).ToArray();
                int from = ParseInt(line[0]);
                tryAddState(from);
                int to = ParseInt(line[1]);
                tryAddState(to);
                char? label = line[2] == "eps" ? null : new char?(line[2][0]);
                builder.AddTransition(from, to, label);
            }
            return builder.GetAutomaton();
        }

        public static Dictionary<string, bool> ReadTests(string[] lines)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(' ').Select(str => str.Trim()).ToArray();
                bool val = lineParts[1] != "0";
                result[lineParts[0]] = val;
            }
            return result;
        }

        private static int ParseInt(string str)
        {
            return int.Parse(str.Trim());
        }
    }
}
