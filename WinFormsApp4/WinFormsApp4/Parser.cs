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
            int seqIdx = 0;

            while (seqIdx < _sequence.Length)
            {
                int expected = _sequence[seqIdx];

                if (_index >= _tokens.Count)
                {
                    AddError(null, $"Ожидалось {GetDesc(expected)}, но строка закончилась");
                    seqIdx++;
                    continue;
                }

                Token current = _tokens[_index];

                if (current.Code == 2 && current.Lexeme.Contains("\""))
                {
                    AddError(current, $"Найден лишний символ '\"' в токене '{current.Lexeme}'");
                    _index++;
                    if (expected == 2) seqIdx++;
                    continue;
                }

                if (current.Code == expected || (expected == 2 && current.Code == 1))
                {
                    _index++;
                    seqIdx++;
                    continue;
                }

                if (expected == 2 && seqIdx == 6 && current.Code == 7)
                {
                    seqIdx++;
                    continue;
                }

                if ((expected == 1 || expected == 3) && current.Code == 2)
                {
                    AddError(current, $"Неверное написание. Ожидалось {GetDesc(expected)}, найдено '{current.Lexeme}'");
                    _index++;
                    seqIdx++;
                    continue;
                }

                int nextExpected = (seqIdx + 1 < _sequence.Length) ? _sequence[seqIdx + 1] : -1;
                if (current.Code == nextExpected)
                {
                    AddError(current, $"Отсутствует обязательный символ {GetDesc(expected)}");
                    seqIdx++;
                    continue;
                }

                AddError(current, $"Лишний символ '{current.Lexeme}'");
                _index++;

                if (_index < _tokens.Count && _tokens[_index].Code == 2 && expected != 2)
                {
                    _index++;
                }
            }

            while (_index < _tokens.Count)
            {
                AddError(_tokens[_index], $"Лишний фрагмент '{_tokens[_index].Lexeme}' после завершения конструкции");
                _index++;
            }
        }

        private string GetDesc(int code)
        {
            return code switch
            {
                1 => "ключевое слово 'const'",
                2 => "идентификатор или текст",
                3 => "тип данных '&str'",
                5 => "разделитель ':'",
                6 => "оператор '='",
                7 => "кавычка '\"'",
                8 => "конец строки ';'",
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