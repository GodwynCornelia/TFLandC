using System;
using System.Collections.Generic;

namespace WinFormsApp4
{
    public enum TokenType { NUMBER, ID, OP, LPAREN, RPAREN, EOF }

    public struct Token
    {
        public TokenType Type;
        public string Value;
    }

    public class LabHandler
    {
        private List<Token> tokens = new List<Token>();
        private int index = 0;
        private int tempCount = 0;

        public List<string[]> Tetrads { get; } = new List<string[]>();
        public List<string> Poliz { get; } = new List<string>();

        public LabHandler(string input)
        {
            tokens = Lex(input);
        }

        private List<Token> Lex(string input)
        {
            var result = new List<Token>();
            int i = 0;
            while (i < input.Length)
            {
                char c = input[i];
                if (char.IsWhiteSpace(c)) { i++; continue; }

                if (char.IsDigit(c))
                {
                    string num = "";
                    while (i < input.Length && char.IsDigit(input[i])) num += input[i++];
                    result.Add(new Token { Type = TokenType.NUMBER, Value = num });
                }
                else if (char.IsLetter(c))
                {
                    string id = "";
                    while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_')) id += input[i++];
                    result.Add(new Token { Type = TokenType.ID, Value = id });
                }
                else if ("+-*/%".Contains(c))
                {
                    result.Add(new Token { Type = TokenType.OP, Value = c.ToString() });
                    i++;
                }
                else if (c == '(') { result.Add(new Token { Type = TokenType.LPAREN, Value = "(" }); i++; }
                else if (c == ')') { result.Add(new Token { Type = TokenType.RPAREN, Value = ")" }); i++; }
                else throw new Exception($"Лексическая ошибка: Недопустимый символ '{c}'");
            }
            result.Add(new Token { Type = TokenType.EOF, Value = "" });
            return result;
        }

        public Token Peek() => index < tokens.Count ? tokens[index] : tokens[tokens.Count - 1];
        public Token Read() => index < tokens.Count ? tokens[index++] : tokens[tokens.Count - 1];
        public bool HasMoreTokens() => Peek().Type != TokenType.EOF;
        private string GetTemp() => $"T{++tempCount}";

        public string ParseE()
        {
            string left = ParseT();
            return ParseA(left);
        }

        private string ParseA(string left)
        {
            Token t = Peek();
            if (t.Type == TokenType.OP && (t.Value == "+" || t.Value == "-"))
            {
                Read();
                string right = ParseT();
                string res = GetTemp();
                Tetrads.Add(new[] { t.Value, left, right, res });
                Poliz.Add(t.Value);
                return ParseA(res);
            }
            return left;
        }

        private string ParseT()
        {
            string left = ParseF();
            return ParseB(left);
        }

        private string ParseB(string left)
        {
            Token t = Peek();
            if (t.Type == TokenType.OP && (t.Value == "*" || t.Value == "/" || t.Value == "%"))
            {
                Read();
                string right = ParseF();
                string res = GetTemp();
                Tetrads.Add(new[] { t.Value, left, right, res });
                Poliz.Add(t.Value);
                return ParseB(res);
            }
            return left;
        }

        private string ParseF()
        {
            Token t = Read();

            if (t.Type == TokenType.LPAREN)
            {
                string res = ParseE();
                if (Read().Type != TokenType.RPAREN) throw new Exception("Синтаксическая ошибка: Ожидалась закрывающая скобка ')'");
                return res;
            }

            if (t.Type == TokenType.NUMBER || t.Type == TokenType.ID)
            {
                Poliz.Add(t.Value);
                return t.Value;
            }

            if (t.Type == TokenType.EOF) throw new Exception("Синтаксическая ошибка: Неожиданный конец выражения");

            throw new Exception($"Синтаксическая ошибка: Ожидался операнд, но найден '{t.Value}'");
        }

        public double EvaluatePoliz()
        {
            Stack<double> stack = new Stack<double>();
            foreach (string val in Poliz)
            {
                if (double.TryParse(val, out double num)) stack.Push(num);
                else
                {
                    if (stack.Count < 2) throw new Exception("Ошибка при вычислении: Недостаточно операндов");
                    double b = stack.Pop();
                    double a = stack.Pop();
                    switch (val)
                    {
                        case "+": stack.Push(a + b); break;
                        case "-": stack.Push(a - b); break;
                        case "*": stack.Push(a * b); break;
                        case "/":
                            if (b == 0) throw new Exception("Ошибка времени выполнения: Деление на ноль");
                            stack.Push(a / b); break;
                        case "%": stack.Push(a % b); break;
                    }
                }
            }
            return stack.Count > 0 ? stack.Pop() : 0;
        }
    }
}