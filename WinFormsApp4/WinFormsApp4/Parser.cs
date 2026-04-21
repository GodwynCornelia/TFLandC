using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp4
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _index = 0;
        public List<Token> SyntaxErrors { get; } = new List<Token>();

        private readonly int[] _sequence = { 1, 2, 5, 3, 6, 7, 2, 7, 8 };

        public Parser(List<Token> tokens)
        {
            _tokens = tokens.Where(t => t.Code != 4).ToList();
        }

        public void Parse()
        {
            SyntaxErrors.Clear();
            _index = 0;

            while (_index < _tokens.Count)
            {
                bool constructionStarted = false;

                foreach (int expectedCode in _sequence)
                {
                    if (_index < _tokens.Count && _tokens[_index].Code == 1)
                        constructionStarted = true;

                    if (_index >= _tokens.Count)
                    {
                        if (expectedCode != 1)
                            AddError(null, $"Ожидалось {GetDesc(expectedCode)}, но достигнут конец файла");
                        continue;
                    }

                    if (!ProcessStep(expectedCode))
                    {
                        if (_index < _tokens.Count && _tokens[_index].Code == 1 && expectedCode != 1)
                            break;
                    }
                }

                if (!constructionStarted && _index >= _tokens.Count) break;
            }
        }

        private bool ProcessStep(int expectedCode)
        {
            if (_index >= _tokens.Count) return false;
            Token current = _tokens[_index];

            if (current.Code == expectedCode)
            {
                _index++;
                return true;
            }

            if (expectedCode == 2 && current.Code == 7)
            {
                return true;
            }

            if (IsFutureAnchor(current.Code, expectedCode))
            {
                AddError(current, $"Пропущено {GetDesc(expectedCode)}");
                return false;
            }
            AddError(current, $"Лишний элемент '{current.Lexeme}'. Ожидалось {GetDesc(expectedCode)}");
            _index++;

            return ProcessStep(expectedCode);
        }

        private bool IsFutureAnchor(int currentCode, int expectedCode)
        {
            int startIdx = Array.IndexOf(_sequence, expectedCode);
            if (startIdx == -1) return false;

            if (expectedCode == 3 && currentCode == 2) return false;

            if (expectedCode == 2 && currentCode == 7) return false;

            for (int i = startIdx + 1; i < _sequence.Length; i++)
            {
                if (_sequence[i] == currentCode) return true;
            }
            return false;
        }

        private string GetDesc(int code)
        {
            return code switch
            {
                1 => "const",
                2 => "Имя или Текст",
                5 => "':'",
                3 => "'&str'",
                6 => "'='",
                7 => "кавычка '\"'",
                8 => "';'",
                _ => "элемент"
            };
        }

        private void AddError(Token t, string message)
        {
            SyntaxErrors.Add(new Token
            {
                Lexeme = t?.Lexeme ?? " ",
                Line = t?.Line ?? (_tokens.LastOrDefault()?.Line ?? 1),
                StartPos = t?.StartPos ?? 0,
                EndPos = t?.EndPos ?? 0,
                Type = message,
                Code = 99
            });
        }
    }
}