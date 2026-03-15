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


                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == '_')
                {
                    string word = "";
                    while (i < input.Length && ((input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z') || (input[i] >= '0' && input[i] <= '9') || input[i] == '_'))
                    {
                        word += input[i];
                        i++;
                    }

                    if (word == "const")
                        tokens.Add(new Token { Code = 1, Type = "Ключевое слово (const)", Lexeme = word, Line = line, StartPos = startI - lineStart, EndPos = i - lineStart });
                    else if (word == "str")
                        tokens.Add(new Token { Code = 3, Type = "Тип данных (str)", Lexeme = word, Line = line, StartPos = startI - lineStart, EndPos = i - lineStart });
                    else
                        tokens.Add(new Token { Code = 2, Type = "Идентификатор (ID)", Lexeme = word, Line = line, StartPos = startI - lineStart, EndPos = i - lineStart });
                    continue;
                }

                if (ch == '&')
                {
                    if (i + 3 < input.Length && input.Substring(i, 4) == "&str")
                    {
                        tokens.Add(new Token { Code = 3, Type = "Тип данных (&str)", Lexeme = "&str", Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 4 });
                        i += 4;
                    }
                    else
                    {
                        tokens.Add(new Token { Code = 99, Type = "INVALID_TOKEN (Ожидалось 'str' после '&')", Lexeme = "&", Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 1 });
                        i++;
                    }
                    continue;
                }

                if (ch == ':') { AddSimpleToken(tokens, 5, "Разделитель типа", ":", line, i - lineStart); i++; continue; }
                if (ch == '=') { AddSimpleToken(tokens, 6, "Оператор присваивания", "=", line, i - lineStart); i++; continue; }
                if (ch == ';') { AddSimpleToken(tokens, 8, "Оператор конца строки", ";", line, i - lineStart); i++; continue; }

                if (ch == '"')
                {
                    string lit = "\"";
                    i++;
                    while (i < input.Length && input[i] != '"' && input[i] != '\n')
                    {
                        lit += input[i];
                        i++;
                    }
                    if (i < input.Length && input[i] == '"')
                    {
                        lit += "\"";
                        i++;
                        tokens.Add(new Token { Code = 7, Type = "Инициализатор строки", Lexeme = lit, Line = line, StartPos = startI - lineStart, EndPos = i - lineStart });
                    }
                    else
                    {
                        tokens.Add(new Token { Code = 99, Type = "INVALID_TOKEN (Незакрытая строка)", Lexeme = lit, Line = line, StartPos = startI - lineStart, EndPos = i - lineStart });
                    }
                    continue;
                }

                tokens.Add(new Token { Code = 99, Type = "INVALID_TOKEN (Запрещенный символ)", Lexeme = ch.ToString(), Line = line, StartPos = i - lineStart, EndPos = i - lineStart + 1 });
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