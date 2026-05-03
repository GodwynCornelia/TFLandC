using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp4
{
    public enum TokenType
    {
        NUMBER, ID,
        PLUS, MINUS, MULT, DIV, MOD,
        LPAREN, RPAREN,
        EOF
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
    }

    public class LabHandler
    {
        private List<Token> tokens;
        private int index;
        private int tempCount = 1;

        public List<string[]> Tetrads { get; private set; } = new List<string[]>();
        public List<string> Poliz { get; private set; } = new List<string>();

        public LabHandler(string input)
        {
            tokens = Lex(input);
            index = 0;
            tempCount = 1;
            Tetrads.Clear();
            Poliz.Clear();
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
                else if (c == '+') { result.Add(new Token { Type = TokenType.PLUS, Value = "+" }); i++; }
                else if (c == '-') { result.Add(new Token { Type = TokenType.MINUS, Value = "-" }); i++; }
                else if (c == '*') { result.Add(new Token { Type = TokenType.MULT, Value = "*" }); i++; }
                else if (c == '/') { result.Add(new Token { Type = TokenType.DIV, Value = "/" }); i++; }
                else if (c == '%') { result.Add(new Token { Type = TokenType.MOD, Value = "%" }); i++; }
                else if (c == '(') { result.Add(new Token { Type = TokenType.LPAREN, Value = "(" }); i++; }
                else if (c == ')') { result.Add(new Token { Type = TokenType.RPAREN, Value = ")" }); i++; }
                else throw new Exception($"Лексическая ошибка: Недопустимый символ '{c}'");
            }
            result.Add(new Token { Type = TokenType.EOF, Value = "" });
            return result;
        }

        public string ParseE()
        {
            string left = ParseT();
            return ParseA(left);
        }

        private string ParseA(string left)
        {
            Token t = Peek();
            if (t.Type == TokenType.PLUS || t.Type == TokenType.MINUS)
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
            if (t.Type == TokenType.MULT || t.Type == TokenType.DIV || t.Type == TokenType.MOD)
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
            Token t = Peek();
            if (t.Type == TokenType.NUMBER || t.Type == TokenType.ID)
            {
                Read();
                Poliz.Add(t.Value);
                return t.Value;
            }
            else if (t.Type == TokenType.LPAREN)
            {
                Read();
                string res = ParseE();
                if (Peek().Type != TokenType.RPAREN)
                    throw new Exception("Синтаксическая ошибка: Ожидалась закрывающая скобка ')'");
                Read();
                return res;
            }
            throw new Exception($"Синтаксическая ошибка: Ожидался операнд или '(', но встречено '{t.Value}'");
        }

        public double EvaluatePoliz()
        {
            Stack<double> stack = new Stack<double>();
            foreach (var item in Poliz)
            {
                if (double.TryParse(item, out double num))
                {
                    stack.Push(num);
                }
                else
                {
                    if (stack.Count < 2) continue;
                    double b = stack.Pop();
                    double a = stack.Pop();
                    switch (item)
                    {
                        case "+": stack.Push(a + b); break;
                        case "-": stack.Push(a - b); break;
                        case "*": stack.Push(a * b); break;
                        case "/":
                            if (b == 0) throw new Exception("Ошибка: Деление на ноль.");
                            stack.Push(a / b);
                            break;
                        case "%": stack.Push(a % b); break;
                    }
                }
            }
            return stack.Count > 0 ? stack.Pop() : 0;
        }

        public bool HasMoreTokens() => index < tokens.Count && tokens[index].Type != TokenType.EOF;

        private Token Read() => tokens[index++];

        public Token Peek() => index < tokens.Count ? tokens[index] : tokens[tokens.Count - 1];

        private string GetTemp() => "T" + (tempCount++);
    }
}