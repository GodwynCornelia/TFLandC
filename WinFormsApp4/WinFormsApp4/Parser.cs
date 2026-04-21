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

        public List<ConstDeclStr> Parse()
        {
            SyntaxErrors.Clear();
            _index = 0;
            var astNodes = new List<ConstDeclStr>();

            while (_index < _tokens.Count)
            {
                ConstDeclStr currentNode = new ConstDeclStr();
                bool isValidLine = true;

                foreach (int expectedCode in _sequence)
                {
                    if (_index >= _tokens.Count)
                    {
                        if (expectedCode != 1)
                            AddError(null, $"Ожидалось {GetDesc(expectedCode)}, но достигнут конец файла");
                        isValidLine = false;
                        continue;
                    }

                    int oldIndex = _index;
                    bool matched = ProcessStep(expectedCode);

                    if (matched)
                    {
                        Token matchedToken = _tokens[oldIndex];
                        FillAstData(currentNode, matchedToken, expectedCode);
                    }
                    else
                    {
                        isValidLine = false;
                        if (_index < _tokens.Count && _tokens[_index].Code == 1 && expectedCode != 1)
                            break;
                    }
                }

                if (isValidLine && !string.IsNullOrEmpty(currentNode.Name))
                {
                    astNodes.Add(currentNode);
                }
            }
            return astNodes;
        }

        private void FillAstData(ConstDeclStr node, Token t, int code)
        {
            if (code == 1) { node.Line = t.Line; node.Position = t.StartPos; }
            if (code == 2 && node.Name == null)
            {
                node.Name = t.Lexeme;
                return;
            }
            if (code == 2 && node.Name != null)
            {
                if (node.Cases.Count == 0)
                    node.Cases.Add(new ConstDeclStr { Name = t.Lexeme });
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

            AddError(current, $"Лишний элемент '{current.Lexeme}'. Ожидалось {GetDesc(expectedCode)}");
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
                1 => "const",
                2 => "Идентификатор/Текст",
                5 => "':'",
                3 => "'&str'",
                6 => "'='",
                7 => "'\"'",
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