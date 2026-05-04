using System;
using System.Collections.Generic;

namespace WinFormsApp4
{
    public class Token
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Lexeme { get; set; }
        public int Line { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }
    }

    public class Scanner
    {
        public List<Token> Analyze(string input)
        {
            var tokens = new List<Token>();
            int i = 0;
            int line = 1;
            int lineStart = 0;

            while (i < input.Length)
            {
                int startI = i;
                char ch = input[i];

                if (ch == ' ' || ch == '\t')
                {
                    tokens.Add(new Token { Code = 4, Type = "Пробел", Lexeme = ch.ToString(), Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 1 });
                    i++; continue;
                }
                if (ch == '\n') { line++; lineStart = i + 1; i++; continue; }
                if (ch == '\r') { i++; continue; }
                if (ch == '&')
                {
                    if (i + 3 < input.Length && input.Substring(i, 4) == "&str")
                    {
                        tokens.Add(new Token { Code = 3, Type = "Тип данных", Lexeme = "&str", Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 4 });
                        i += 4;
                    }
                    else
                    {
                        tokens.Add(new Token { Code = 99, Type = "Ошибка", Lexeme = "&", Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 1 });
                        i++;
                    }
                    continue;
                }

                if (char.IsLetter(ch) || ch == '_' || char.IsDigit(ch) || (ch != ':' && ch != '=' && ch != ';' && ch != '"' && ch != '&'))
                {
                    string word = "";
                    int wordStart = i;

                    while (i < input.Length)
                    {
                        char current = input[i];

                        if (char.IsLetterOrDigit(current) || current == '_')
                        {
                            word += current;
                            i++;
                        }
                        else if (current == '"' && i + 1 < input.Length && char.IsLetterOrDigit(input[i + 1]))
                        {
                            word += current;
                            i++;
                        }
                        else if (current != ':' && current != '=' && current != ';' && current != '"' && current != '&' && !char.IsWhiteSpace(current))
                        {
                            word += current;
                            i++;
                        }
                        else break;
                    }

                    if (word.Contains("\""))
                        tokens.Add(new Token { Code = 99, Type = "Ошибка", Lexeme = word, Line = line, StartPos = wordStart - lineStart, EndPos = i - lineStart });
                    else if (word == "const")
                        tokens.Add(new Token { Code = 1, Type = "Ключевое слово", Lexeme = word, Line = line, StartPos = wordStart - lineStart, EndPos = i - lineStart });
                    else
                        tokens.Add(new Token { Code = 2, Type = "Идентификатор/Текст", Lexeme = word, Line = line, StartPos = wordStart - lineStart, EndPos = i - lineStart });
                    continue;
                }

                if (ch == ':') { AddSimpleToken(tokens, 5, "Разделитель", ":", line, i - lineStart); i++; continue; }
                if (ch == '=') { AddSimpleToken(tokens, 6, "Оператор", "=", line, i - lineStart); i++; continue; }
                if (ch == ';') { AddSimpleToken(tokens, 8, "Конец строки", ";", line, i - lineStart); i++; continue; }
                if (ch == '"') { AddSimpleToken(tokens, 7, "Кавычка", "\"", line, i - lineStart); i++; continue; }

                i++;
            }
            return tokens;
        }

        private void AddSimpleToken(List<Token> list, int code, string type, string lex, int line, int pos)
        {
            list.Add(new Token { Code = code, Type = type, Lexeme = lex, Line = line, StartPos = pos, EndPos = pos + 1 });
        }
    }
}