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

            foreach (int expectedCode in _sequence)
            {
                if (_index >= _tokens.Count)
                {
                    AddError(null, $"Ожидалось {GetDesc(expectedCode)}, но строка закончилась");
                    continue;
                }

                ProcessStep(expectedCode);
                if (expectedCode == 8) break; // Завершаем разбор после ';'
            }

            // Обработка лишних хвостов после ';'
            while (_index < _tokens.Count)
            {
                AddError(_tokens[_index], $"Лишний элемент '{_tokens[_index].Lexeme}' после завершения конструкции");
                _index++;
            }
        }

        private bool ProcessStep(int expectedCode)
        {
            if (_index >= _tokens.Count) return false;
            Token current = _tokens[_index];

            // 1. Идеальное совпадение
            if (current.Code == expectedCode)
            {
                _index++;
                return true;
            }

            // 2. Обработка слов с лишней кавычкой внутри (склеенных лексером)
            if (current.Code == 99 && current.Lexeme.Contains("\""))
            {
                bool isLikelyTarget = false;
                if (expectedCode == 1 && current.Lexeme.Contains("const")) isLikelyTarget = true;
                if (expectedCode == 2) isLikelyTarget = true;
                if (expectedCode == 3 && (current.Lexeme.Contains("str") || current.Lexeme.Contains("&"))) isLikelyTarget = true;

                if (isLikelyTarget)
                {
                    AddError(current, $"Найден лишний символ '\"' в токене ");
                    _index++;
                    return true;
                }
            }

            // 3. Спец-кейсы (пустая строка)
            if (expectedCode == 2 && current.Code == 7) return true;

            // 4. Жадная проверка (пропуск лишнего символа)
            if (_index + 1 < _tokens.Count && _tokens[_index + 1].Code == expectedCode)
            {
                AddError(current, $"Лишний элемент '{current.Lexeme}'. Ожидалось {GetDesc(expectedCode)}");
                _index++;
                return ProcessStep(expectedCode);
            }

            // 5. Дефолтная ошибка
            AddError(current, $"Неверный элемент '{current.Lexeme}'. Ожидалось {GetDesc(expectedCode)}");
            _index++;
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