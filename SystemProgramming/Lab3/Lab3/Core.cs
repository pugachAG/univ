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
        Operator,
        Number,
        Char,
        Identifier,
        String,
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
           @"\+",
            "-",
           @"\*",
            "/",
            "%",
           @"\+\+",
            "--",
            "==",
            "!=",
            ">",
            "<",
            ">=",
            "<=",
           @"\&",
           @"\|",
            "!",
           @"\^",
            "~",
            "<<",
            ">>",
            "&&",
           @"\|\|",
           @"\+=",
            "-=",
           @"\*=",
            "/=",
            "%=",
            "<<=",
            ">>=",
            "&=",
           @"\^=",
           @"\|=", 
           @"\:",
           @"\;",
           @"\,", 
           @"\(",
           @"\)", 
           @"\[",
           @"\]", 
           @"\{",
           @"\}",
           @"->",
           @"="
        };

        public static string KeywordsPattern;
        public static string OperatorsPattern;
        public const string SpacesPattern = @"((\s|\n\r|\n|\t)+)";
        public const string CharPattern = @"^'.'$";
        public const string NumberPattern = @"^[0-9]+\.?[0-9]*$";
        public const string IdentifierPattern = @"^[_a-zA-Z][\d\w_]*$";
        public const string StringPattern = "^\".*\"$";

        static Core()
        {
            KeywordsPattern = "^(" + string.Join("|", Keywords) + ")$";
            OperatorsPattern = "(" + string.Join("|", Operators) + ")";
        }

        public List<Lexem> Process(string input)
        {
            input = Filter(input);            
            MatchCollection matches = Regex.Matches(input + " ", string.Join("|", SpacesPattern, OperatorsPattern));
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

                ProcessLexem(match.Value, match.Index);
            }

            return allLexems.OrderBy(x => x.Index).ToList();
        }

        enum FilterState
        {
            None,
            String,
            CommentOneLine,
            CommentMultiLine
        }

        private string Filter(string input)
        {
            StringBuilder builder = new StringBuilder();
            input += " ";
        
            FilterState state = FilterState.None;
            StringBuilder current = new StringBuilder();
            int startIndex = 0;
            char fake = '\0';
            for (int i = 0; i < input.Length - 1; i++)
            {
                switch (state)
                {
                    case FilterState.None:
                        bool change = false;
                        if(input[i] == '/' && input[i+1] == '*')
                        {
                            state = FilterState.CommentMultiLine;
                            change = true;
                        }
                        if (input[i] == '/' && input[i + 1] == '/')
                        {
                            state = FilterState.CommentOneLine;
                            change = true;
                        }
                        if (input[i] == '"')
                        {
                            state = FilterState.String;
                            change = true;
                        }
                        if (change)
                        {
                            startIndex = i;
                            current.Append(input[i]);
                        }
                        else
                            builder.Append(input[i]);
                        break;
                    case FilterState.String:
                        current.Append(input[i]);
                        if(input[i] == '"' && input[i-1] != '\\')
                        {
                            allLexems.Add(new Lexem(current.ToString(), LexemType.String, startIndex));
                            current.Clear();
                            state = FilterState.None;
                        }
                        break;
                    case FilterState.CommentOneLine:
                        current.Append(input[i]);
                        if (input[i] == '\r' || input[i] == '\n')
                        {
                            allLexems.Add(new Lexem(current.ToString(), LexemType.Comment, startIndex));
                            current.Clear();
                            state = FilterState.None;
                        }
                        break;
                    case FilterState.CommentMultiLine:
                        current.Append(input[i]); 
                        if (input[i-1] == '*' && input[i] == '/')
                        {
                            allLexems.Add(new Lexem(current.ToString(), LexemType.Comment, startIndex));
                            current.Clear();
                            state = FilterState.None;
                        }
                        break;
                    default:
                        break;
                }
                if (builder.Length != i + 1)
                    builder.Append(' ');
            }
            return builder.ToString();
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
            else if (Regex.IsMatch(text, SpacesPattern))
            {
                type = LexemType.Space;
            }
            else if (Regex.IsMatch(text, OperatorsPattern))
            {
                type = LexemType.Operator;
            }
            else if(Regex.IsMatch(text, CharPattern))
            {
                type = LexemType.Char;
            }
            else if (Regex.IsMatch(text, NumberPattern))
            {
                type = LexemType.Number;
            }
            else if(Regex.IsMatch(text, IdentifierPattern))
            {
                type = LexemType.Identifier;
            }
            

            allLexems.Add(new Lexem(text, type, indx));
        }
    }
}
