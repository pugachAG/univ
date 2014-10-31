using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Common
{
    public static class GeneralHelper
    {
        public static List<string> GetAllWords(int k, HashSet<char> chars)
        {
            List<string> words = new List<string>();

            Action<int> go = null;
            Stack<char> current = new Stack<char>();
            go = n =>
                {
                    if (n == k)
                    {
                        words.Add(new string(current.ToArray()));
                    }
                    else
                    {
                        foreach (var ch in chars)
                        {
                            current.Push(ch);
                            go(n + 1);
                            current.Pop();
                        }
                    }
                };
            go(0);

            return words;
        }

    }
}
