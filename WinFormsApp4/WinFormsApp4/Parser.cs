using System;
using System.Collections.Generic;
using System.Linq;

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
                if (!Expect(ref i, 1, "Ключевое слово 'const'"))
                {
                    Recover(ref i, new int[] { 2, 5, 8 });
                    if (i < _tokens.Count && _tokens[i].Code == 1) continue;
                }

                if (!Expect(ref i, 2, "Идентификатор (имя константы)"))
                {
                    Recover(ref i, new int[] { 5, 3, 6, 8 });
                }

                if (!Expect(ref i, 5, "Разделитель ':'"))
                {
                    Recover(ref i, new int[] { 3, 6, 8 });
                }

                if (!Expect(ref i, 3, "Тип данных '&str'"))
                {
                    Recover(ref i, new int[] { 6, 8 });
                }

                if (!Expect(ref i, 6, "Оператор присваивания '='"))
                {
                    Recover(ref i, new int[] { 7, 8 });
                }

                if (!Expect(ref i, 7, "Строковый литерал в кавычках"))
                {
                    Recover(ref i, new int[] { 8 });
                }

                if (!Expect(ref i, 8, "Символ ';' в конце строки"))
                { 
                    Recover(ref i, new int[] { 1 });
                }
            }
        }

        private bool Expect(ref int index, int expectedCode, string desc)
        {
            if (index >= _tokens.Count)
            {
                AddError(null, $"Неожиданный конец кода. Ожидалось: {desc}");
                return false;
            }

            if (_tokens[index].Code != expectedCode)
            {
                AddError(_tokens[index], $"Ошибка синтаксиса: {desc}");
                return false;
            }

            index++;
            return true;
        }
        private void Recover(ref int index, int[] lookaheadCodes)
        {
            while (index < _tokens.Count)
            {
                if (_tokens[index].Code == 1) return;
                if (lookaheadCodes.Contains(_tokens[index].Code)) return;

                index++;
            }
        }

        private void AddError(Token t, string message)
        {
            SyntaxErrors.Add(new Token
            {
                Lexeme = t != null ? t.Lexeme : "EOF",
                Line = t != null ? t.Line : (_tokens.Count > 0 ? _tokens.Last().Line : 1),
                StartPos = t != null ? t.StartPos : 0,
                EndPos = t != null ? t.EndPos : 0,
                Type = message,
                Code = 99
            });
        }
    }
}