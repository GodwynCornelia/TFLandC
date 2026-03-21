using System;
using System.Collections.Generic;

namespace WinFormsApp4
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        public List<Token> SyntaxErrors { get; } = new List<Token>();

        public Parser(List<Token> tokens)
        {
            _tokens = tokens.FindAll(t => t.Code != 4);
        }

        public void Parse()
        {
            SyntaxErrors.Clear();
            int i = 0;

            while (i < _tokens.Count)
            {
                try
                {
                    if (!Check(i, 1, "const", out i)) { i = Recovery(i); continue; }
                    if (!Check(i, 2, "идентификатор", out i)) { i = Recovery(i); continue; }
                    if (!Check(i, 5, "':'", out i)) { i = Recovery(i); continue; }
                    if (i < _tokens.Count && _tokens[i].Lexeme != "&str")
                    {
                        AddError(_tokens[i], "Ожидался тип '&str'");
                        i = Recovery(i);
                        continue;
                    }
                    if (!Check(i, 3, "&str", out i)) { i = Recovery(i); continue; }
                    if (!Check(i, 6, "'='", out i)) { i = Recovery(i); continue; }
                    if (!Check(i, 7, "строковый литерал", out i)) { i = Recovery(i); continue; }
                    if (!Check(i, 8, "';'", out i)) { i = Recovery(i); continue; }
                }
                catch { i = Recovery(i); }
            }
        }

        private bool Check(int index, int expectedCode, string description, out int nextIndex)
        {
            nextIndex = index;
            if (index >= _tokens.Count)
            {
                Token last = _tokens.Count > 0 ? _tokens[_tokens.Count - 1] : new Token { Line = 1, StartPos = 0 };
                SyntaxErrors.Add(new Token
                {
                    Lexeme = "EOF",
                    Line = last.Line,
                    StartPos = last.EndPos,
                    Type = $"Ожидалось {description}",
                    Code = 99
                });
                return false;
            }

            if (_tokens[index].Code != expectedCode)
            {
                AddError(_tokens[index], $"Ожидалось {description}");
                return false;
            }

            nextIndex++;
            return true;
        }

        private void AddError(Token t, string msg)
        {
            SyntaxErrors.Add(new Token
            {
                Lexeme = t.Lexeme,
                Line = t.Line,
                StartPos = t.StartPos,
                EndPos = t.EndPos,
                Type = msg,
                Code = 99
            });
        }

        private int Recovery(int currentIndex)
        {
            int i = currentIndex;
            while (i < _tokens.Count && _tokens[i].Code != 8) i++;
            return i + 1;
        }
    }
}