using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab3
{
    public enum LexemType
    {
        Unknown,
        ReservedWord,
        Comment,
        Space,
    }

    public struct Lexem
    {
        public string Content { get; private set; }
        public int Index { get; private set; }
        public LexemType LexemType { get; private set; }

        public Lexem(string content, LexemType type, int index)
            : this()
        {
            this.Content = content;
            this.LexemType = type;
            this.Index = index;
        }

    }

    public class Core
    {
        private List<Lexem> allLexems = new List<Lexem>(); 

        public static readonly string[] Keywords = new string[] 
        {
            "auto",
            "break",
            "case",
            "char",
            "const",
            "continue",
            "default",
            "do",
            "double",
            "else",
            "enum",
            "extern",
            "float",
            "for",
            "goto",
            "if",
            "int",
            "long",
            "register",
            "return",
            "short",
            "signed",
            "sizeof",
            "static",
            "struct",
            "switch",
            "typedef",
            "union",
            "unsigned",
            "void",
            "volatile",
            "while" 
        };

        public static readonly string[] Operators = new string[] 
        {
            "+",
            "-",
            "*",
            "/",
            "%",
            "++",
            "--",
            "==",
            "!=",
            ">",
            "<",
            ">=",
            "<=",
            "&",
            "|",
            "!",
            "^",
            "~",
            "<<",
            ">>",
            "&&",
            "||",
            "+=",
            "-=",
            "*=",
            "/=",
            "%=",
            "<<=",
            ">>=",
            "&=",
            "^=",
            "|=",
            ":",
            ";",
            ",", 
        };

        public static string KeywordsPattern;
        public static string OperatorsPattern;
        public const string Spaces = @"(\s|\n\r|\n|\t)+";

        static Core()
        {
            KeywordsPattern = "^(" + string.Join("|", Keywords) + ")$";
            OperatorsPattern = string.Join("|", Operators);
        }

        public List<Lexem> Process(string input)
        {
            MatchCollection matches = Regex.Matches(input + " ", Spaces);
            
            StringBuilder currentLexemBuilder = new StringBuilder();
            int index = 0;
            foreach(Match match in matches)
            {
                while(index != match.Index)
                {
                    currentLexemBuilder.Append(input[index]);
                    index++;
                }

                ProcessLexem(currentLexemBuilder.ToString(), index);
                currentLexemBuilder.Clear();
                index += match.Value.Length;

                ProcessSpaces(match.Value, match.Index);
            }

            return allLexems;
        }

        private string Filter(string input)
        {
            return input;
        }

        private void ProcessSpaces(string text, int indx)
        {
            this.allLexems.Add(new Lexem(text, LexemType.Space, indx));
        }

        private void ProcessLexem(string text, int indx)
        {
            LexemType type = LexemType.Unknown;
            if(Regex.IsMatch(text, KeywordsPattern))
            {
                type = LexemType.ReservedWord;
            }
            allLexems.Add(new Lexem(text, type, indx));
        }
    }
}
