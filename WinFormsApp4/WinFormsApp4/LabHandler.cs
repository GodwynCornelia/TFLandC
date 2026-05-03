using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WinFormsApp4
{
    public class LabHandler
    {
        private List<string> tokens = new List<string>();
        private int index = 0;
        private int tempCount = 0;

        public List<string[]> Tetrads { get; } = new List<string[]>();
        public List<string> Poliz { get; } = new List<string>();

        public LabHandler(string input)
        {
            Tokenize(input);
        }

        private void Tokenize(string input)
        {
            string pattern = input.Replace("+", " + ")
                                  .Replace("-", " - ")
                                  .Replace("*", " * ")
                                  .Replace("/", " / ")
                                  .Replace("%", " % ")
                                  .Replace("(", " ( ")
                                  .Replace(")", " ) ");

            tokens = pattern.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string Peek() => index < tokens.Count ? tokens[index] : "";
        private string Read() => index < tokens.Count ? tokens[index++] : "";
        public bool HasMoreTokens() => index < tokens.Count;
        private string GetTemp() => $"T{++tempCount}";

        public string ParseE()
        {
            string left = ParseT();
            return ParseA(left);
        }

        private string ParseA(string left)
        {
            string op = Peek();
            if (op == "+" || op == "-")
            {
                Read();
                string right = ParseT();
                string res = GetTemp();
                Tetrads.Add(new[] { op, left, right, res });
                Poliz.Add(op);
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
            string op = Peek();
            if (op == "*" || op == "/" || op == "%")
            {
                Read();
                string right = ParseF();
                string res = GetTemp();
                Tetrads.Add(new[] { op, left, right, res });
                Poliz.Add(op);
                return ParseB(res);
            }
            return left;
        }

        private string ParseF()
        {
            string current = Read();

            if (string.IsNullOrEmpty(current))
                throw new Exception("Неожиданный конец выражения.");

            if (current == "(")
            {
                string res = ParseE();
                if (Read() != ")") throw new Exception("Ожидалась закрывающая скобка ')'.");
                return res;
            }
            if ("+-*/%".Contains(current))
                throw new Exception($"Ожидался операнд, но найден оператор '{current}'.");

            Poliz.Add(current);
            return current;
        }

        public double EvaluatePoliz()
        {
            Stack<double> stack = new Stack<double>();
            foreach (string token in Poliz)
            {
                if (double.TryParse(token, out double number)) stack.Push(number);
                else
                {
                    if (stack.Count < 2) throw new Exception("Ошибка стека при вычислении ПОЛИЗ.");
                    double b = stack.Pop();
                    double a = stack.Pop();
                    switch (token)
                    {
                        case "+": stack.Push(a + b); break;
                        case "-": stack.Push(a - b); break;
                        case "*": stack.Push(a * b); break;
                        case "/":
                            if (b == 0) throw new Exception("Деление на ноль.");
                            stack.Push(a / b); break;
                        case "%": stack.Push(a % b); break;
                    }
                }
            }
            return stack.Count > 0 ? stack.Pop() : 0;
        }
    }
}