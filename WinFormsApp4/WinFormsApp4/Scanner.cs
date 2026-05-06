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

            bool afterEquals = false;

            while (i < input.Length)
            {
                char ch = input[i];

                if (char.IsWhiteSpace(ch))
                {
                    if (ch == '\n')
                    {
                        line++;
                        lineStart = i + 1;
                        afterEquals = false;
                    }
                    if (ch == ' ' || ch == '\t')
                    {
                        tokens.Add(new Token { Code = 4, Type = "Пробел", Lexeme = ch.ToString(), Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 1 });
                    }
                    i++; continue;
                }

                if (ch == '"')
                {
                    AddSimpleToken(tokens, 7, "Кавычка", "\"", line, i - lineStart);
                    i++;

                    if (!afterEquals)
                    {
                        continue;
                    }

                    int endLimit = i;
                    while (endLimit < input.Length && input[endLimit] != ';' && input[endLimit] != '\n')
                    {
                        endLimit++;
                    }

                    int lastQuoteIdx = endLimit - 1;
                    while (lastQuoteIdx >= i && input[lastQuoteIdx] != '"')
                    {
                        lastQuoteIdx--;
                    }

                    if (lastQuoteIdx >= i) 
                    {
                        string textVal = input.Substring(i, lastQuoteIdx - i);
                        if (textVal.Length > 0)
                        {
                            tokens.Add(new Token { Code = 2, Type = "Текст", Lexeme = textVal, Line = line, StartPos = i - lineStart, EndPos = lastQuoteIdx - lineStart });
                        }
                        AddSimpleToken(tokens, 7, "Кавычка", "\"", line, lastQuoteIdx - lineStart);
                        i = lastQuoteIdx + 1;
                    }
                    else
                    {
                        string textVal = input.Substring(i, endLimit - i);
                        if (textVal.Length > 0)
                        {
                            tokens.Add(new Token { Code = 2, Type = "Текст", Lexeme = textVal, Line = line, StartPos = i - lineStart, EndPos = endLimit - lineStart });
                        }
                        i = endLimit;
                    }
                    continue;
                }

                if (ch == '=')
                {
                    afterEquals = true; 
                    AddSimpleToken(tokens, 6, "Оператор", "=", line, i - lineStart);
                    i++; continue;
                }

                if (ch == ';')
                {
                    afterEquals = false;
                    AddSimpleToken(tokens, 8, "Конец строки", ";", line, i - lineStart);
                    i++; continue;
                }

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

                if (ch == ':') { AddSimpleToken(tokens, 5, "Разделитель", ":", line, i - lineStart); i++; continue; }

                if (char.IsLetter(ch) || ch == '_')
                {
                    string word = "";
                    int wordStart = i;

                    while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
                    {
                        word += input[i];
                        i++;
                    }

                    int code = (word == "const") ? 1 : 2;
                    tokens.Add(new Token { Code = code, Type = "Идентификатор", Lexeme = word, Line = line, StartPos = wordStart - lineStart, EndPos = i - lineStart });
                    continue;
                }
                tokens.Add(new Token { Code = 99, Type = "Неизвестный", Lexeme = ch.ToString(), Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 1 });
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