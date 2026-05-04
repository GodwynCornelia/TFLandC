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

            for (int i = 0; i < _sequence.Length; i++)
            {
                int expectedCode = _sequence[i];

                if (_index >= _tokens.Count)
                {
                    AddError(null, $"Ожидалось {GetDesc(expectedCode)}, но строка закончилась");
                    continue;
                }

                int nextExpectedCode = (i + 1 < _sequence.Length) ? _sequence[i + 1] : -1;
                ProcessStep(expectedCode, nextExpectedCode);

                if (expectedCode == 8) break;
            }

            while (_index < _tokens.Count)
            {
                AddError(_tokens[_index], $"Лишний элемент '{_tokens[_index].Lexeme}' после завершения конструкции");
                _index++;
            }
        }

        private void ProcessStep(int expectedCode, int nextExpectedCode)
        {
            if (_index >= _tokens.Count) return;
            Token current = _tokens[_index];

            if (current.Code == expectedCode)
            {
                _index++;
                return;
            }

            if (expectedCode == 1)
            {
                AddError(current, $"Неверное начало конструкции. Ожидалось 'const', но найдено '{current.Lexeme}'");
                _index++;
                return;
            }

            if (current.Code == 99 && current.Lexeme.Contains("\""))
            {
                AddError(current, $"Найден лишний символ '\"' в токене '{current.Lexeme}'");
                _index++;
                return;
            }

            if (current.Code == nextExpectedCode)
            {
                AddError(current, $"Отсутствует символ {GetDesc(expectedCode)}");
                return;
            }

            if (expectedCode == 7)
            {
                if (current.Code == 2)
                {
                    AddError(current, "Отсутствует открывающая кавычка '\"' перед значением");
                    return;
                }
                if (current.Code == 8)
                {
                    AddError(current, "Отсутствует закрывающая кавычка '\"'");
                    return;
                }
            }

            if (expectedCode == 2 && current.Code == 7) return;

            if (_index + 1 < _tokens.Count && _tokens[_index + 1].Code == expectedCode)
            {
                AddError(current, $"Лишний элемент '{current.Lexeme}'. Ожидалось {GetDesc(expectedCode)}");
                _index++;
                ProcessStep(expectedCode, nextExpectedCode);
                return;
            }
            AddError(current, $"Неверный элемент '{current.Lexeme}'. Ожидалось {GetDesc(expectedCode)}");
            _index++;
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