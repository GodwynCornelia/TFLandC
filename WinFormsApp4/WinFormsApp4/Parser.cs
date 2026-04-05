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

        public Parser(List<Token> tokens)
        {
            _tokens = tokens.FindAll(t => t.Code != 4);
        }

        public void Parse()
        {
            SyntaxErrors.Clear();
            _index = 0;

            while (_index < _tokens.Count)
            {

                if (_tokens[_index].Code != 1)
                {
                    AddError(_tokens[_index], $"Лишний фрагмент вне инструкции: '{_tokens[_index].Lexeme}'");
                    while (_index < _tokens.Count && _tokens[_index].Code != 1)
                    {
                        _index++;
                    }
                    continue;
                }

                int startIdx = _index;
                ParseConstantDeclaration();
                if (_index == startIdx && _index < _tokens.Count)
                {
                    _index++;
                }
            }
        }

        private void ParseConstantDeclaration()
        {
            SyncExpect(1, "Ключевое слово 'const'", new int[] { 2, 5, 8 });
            SyncExpect(2, "Идентификатор (NAME)", new int[] { 5, 6, 8 });
            SyncExpect(5, "Разделитель ':'", new int[] { 3, 6, 8 });
            SyncExpect(3, "Тип данных '&str'", new int[] { 6, 7, 8 });
            SyncExpect(6, "Оператор '='", new int[] { 7, 8 });
            SyncExpect(7, "Строковое значение", new int[] { 8 });
            SyncExpect(8, "Завершающая ';'", new int[] { 1 });
        }
        private bool SyncExpect(int expectedCode, string description, int[] followAnchors)
        {
            if (_index < _tokens.Count && _tokens[_index].Code == expectedCode)
            {
                _index++;
                return true;
            }
            Token errT = _index < _tokens.Count ? _tokens[_index] : null;
            AddError(errT, $"Ожидалось {description}, найдено '{(errT?.Lexeme ?? "конец файла")}'");
            while (_index < _tokens.Count)
            {
                if (_tokens[_index].Code == 1 && expectedCode != 1) return false;
                if (_tokens[_index].Code == expectedCode)
                {
                    _index++;
                    return true;
                }

                if (followAnchors.Contains(_tokens[_index].Code))
                {
                    return false;
                }
                _index++;
            }

            return false;
        }

        private void AddError(Token t, string message)
        {
            SyntaxErrors.Add(new Token
            {
                Lexeme = t?.Lexeme ?? "EOF",
                Line = t?.Line ?? 1,
                StartPos = t?.StartPos ?? 0,
                EndPos = t?.EndPos ?? 0,
                Type = message,
                Code = 99
            });
        }
    }
}