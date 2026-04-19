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
        private readonly int[] _sequence = { 1, 2, 5, 3, 6, 7, 8 };

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
                foreach (int expectedCode in _sequence)
                {
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

            if (IsFutureAnchor(current.Code, expectedCode))
            {
                AddError(current, $"Пропущено {GetDesc(expectedCode)}");
                return false;
            }

            AddError(current, $"Лишний фрагмент: '{current.Lexeme}'. Ожидалось {GetDesc(expectedCode)}");
            _index++;

            return ProcessStep(expectedCode);
        }

        private bool IsFutureAnchor(int currentCode, int expectedCode)
        {
            int startIdx = Array.IndexOf(_sequence, expectedCode);
            if (startIdx == -1) return false;
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
                1 => "Ключевое слово 'const'",
                2 => "Идентификатор (NAME)",
                5 => "Разделитель ':'",
                3 => "Тип данных '&str'",
                6 => "Оператор '='",
                7 => "Строковое значение BodyString",
                8 => "Завершающая ';'",
                _ => "неизвестный элемент"
            };
        }

        private void AddError(Token t, string message)
        {
            SyntaxErrors.Add(new Token
            {
                Lexeme = t?.Lexeme ?? "EOF",
                Line = t?.Line ?? (_tokens.LastOrDefault()?.Line ?? 1),
                StartPos = t?.StartPos ?? 0,
                EndPos = t?.EndPos ?? 0,
                Type = message,
                Code = 99
            });
        }
    }
}