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
                if (_tokens[_index].Code == 1 || _tokens[_index].Code == 2)
                {
                    ParseConstantDeclaration();
                }
                else
                {
                    AddError(_tokens[_index], $"Лишний фрагмент вне инструкции: '{_tokens[_index].Lexeme}'");
                    _index++;
                }
            }
        }

        private void ParseConstantDeclaration()
        {
            foreach (int code in _sequence)
            {
                if (_index >= _tokens.Count)
                {
                    AddError(null, $"Ожидалось {GetDesc(code)}, но достигнут конец файла");
                    break;
                }

                if (!Expect(code))
                {
                    if (_index < _tokens.Count && _tokens[_index].Code == 1 && code != 1) break;
                }
            }
        }

        private bool Expect(int expectedCode)
        {
            if (_index >= _tokens.Count) return false;

            Token current = _tokens[_index];

            if (current.Code == expectedCode)
            {
                _index++;
                return true;
            }

            AddError(current, $"Ожидалось {GetDesc(expectedCode)}, найдено '{current.Lexeme}'");

            int currentStepIdx = Array.IndexOf(_sequence, expectedCode);
            for (int i = currentStepIdx + 1; i < _sequence.Length; i++)
            {
                if (current.Code == _sequence[i])
                {
                    return false;
                }
            }

            if (current.Code != 8 && current.Code != 1)
            {
                _index++;
                return Expect(expectedCode);
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
                7 => "Строковое значение",
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